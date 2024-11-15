using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

using System.Globalization;
public partial class Ope_CrearPrecioTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    BO_Administracion objBOAdministracion = new BO_Administracion(Define.CNX_13); //new BO_Administracion();
    
    private DataTable dtPrecioTicket;
    private DataTable dtTipoTicket;
    private DataTable dtResultPrecio;
    private DataTable monedaTbl;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            dtTipoTicket = objBOAdministracion.ListaTipoTicket();
            dtPrecioTicket = objBOAdministracion.ObtenerPrecioTicket("");
            monedaTbl = GetMonedaTable();
            if (!IsPostBack)
            {
                rbtnFchAct.Checked = true;                
                dtResultPrecio = ResultTable(dtTipoTicket, dtPrecioTicket);
                LoadPrecioTicket();
            }
            BloquearMonedaTurnoPendiente();
        }        
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }
    
    private DataTable GetMonedaTable()
    {
        DataTable dt_consulta = new DataTable();
        dt_consulta = objBOAdministracion.listarAllMonedas();
        return dt_consulta;
    }
    
    private DataTable ResultTable(DataTable dtTipoTicket, DataTable dtPrecioTicket)
    {
        DataTable dest = new DataTable("Result" + dtTipoTicket.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.Caption = "Tasa de Cambio Actual";
        dc.ColumnName = "Cod_Precio_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Moneda";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Imp_Precio";
        dest.Columns.Add(dc);

        DataRow[] foundRowTipoTicket = dtTipoTicket.Select("Tip_Estado = '1'");
        DataRow[] foundRowMoneda = monedaTbl.Select("Tip_Estado = 'Vigente'");

        if (foundRowMoneda != null && foundRowMoneda.Length > 0)
        {
            if (foundRowTipoTicket != null && foundRowTipoTicket.Length > 0)
            {
                for (int i = 0; i < foundRowTipoTicket.Length; i++)
                    dest.Rows.Add(dest.NewRow());

                try
                {
                    int j = 0;
                    DataRow[] foundRowPrecioTicket;
                    string row_filter;
                    foreach (DataRow r in foundRowTipoTicket)
                    {
                        dest.Rows[j][1] = r["Cod_Tipo_Ticket"].ToString();

                        row_filter = "Cod_Tipo_Ticket ='" + r["Cod_Tipo_Ticket"].ToString() + "' AND Tip_Estado = '1'";

                        foundRowPrecioTicket = dtPrecioTicket.Select(row_filter);

                        if (foundRowPrecioTicket != null && foundRowPrecioTicket.Length > 0)
                        {
                            dest.Rows[j][0] = foundRowPrecioTicket[0]["Cod_Precio_Ticket"].ToString();
                            dest.Rows[j][2] = foundRowPrecioTicket[0]["Dsc_Tipo_Ticket"].ToString();
                            dest.Rows[j][3] = foundRowPrecioTicket[0]["Cod_Moneda"].ToString();
                            dest.Rows[j][4] = foundRowPrecioTicket[0]["Imp_Precio"].ToString();

                        }
                        else
                        {
                            dest.Rows[j][0] = "";
                            dest.Rows[j][2] = r["Dsc_Tipo_Ticket"].ToString();
                            dest.Rows[j][3] = foundRowMoneda[0]["Cod_Moneda"];
                            dest.Rows[j][4] = "0.00";
                        }
                        j++;
                    }
                }
                catch (IndexOutOfRangeException iore)
                {

                }
            }
            else
            {
                this.lblMensajeError.Text = "No existen Tipo de Tickets vigentes";
                grvPrecioTicket.DataSource = null;
                grvPrecioTicket.DataBind();
            }
        }
        else {
            this.lblMensajeError.Text = "No existen Tipos de Monedas vigentes.";
            grvPrecioTicket.DataSource = null;
            grvPrecioTicket.DataBind();
        }

        dest.AcceptChanges();
        return dest;
    }

    protected void LoadPrecioTicket()
    {
        this.grvPrecioTicket.DataSource = dtResultPrecio;
        this.grvPrecioTicket.DataBind();
    }

    public bool ValidateFecha(string pFchProga, string pHrProga)
    {
        string pFchActual = DateTime.Now.ToShortDateString();
        string pHrActual = DateTime.Now.ToLongTimeString();

        DateTime DateProgram = new DateTime();
        if (Fecha.getFechaCustom2(pFchProga, pHrProga, out DateProgram) > 0)
        {
        }

        int valor = DateTime.Compare(DateProgram, DateTime.Now);
        if (valor > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private decimal ValidateImporte(string strImporte)
    {
        decimal dImporte = 0;
        try
        {
            dImporte = decimal.Parse(strImporte);

            if (!(dImporte >= 0 && dImporte < 10000000000))
            {
                dImporte = -1;
            }
        }
        catch (System.FormatException efe)
        {
            dImporte = -1;
        }
        catch (System.OverflowException eoe)
        {
            dImporte = -1;
        }
        catch (System.ArgumentNullException eae)
        {
            dImporte = -1;
        }
        finally
        {
        }
        return dImporte;
    }

    private void UpdatePrecio(DataRow[] foundRow, bool bCheckProg, string strPrecio, string strMoneda, Hashtable htSuccess, 
        Hashtable htErrors, Int64 idTxCritica)
    {
        string strMessage = "";
        objBOAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"],
            (string)Session["Cod_SubModulo"], Define.CNX_13);
        PrecioTicket objPrecioTicket = new PrecioTicket();
        objPrecioTicket.SCodTipoTicket = (string)foundRow[0]["Cod_Tipo_Ticket"];
        objPrecioTicket.SCodMoneda = strMoneda;
        objPrecioTicket.DImpPrecio = decimal.Parse(strPrecio);
        objPrecioTicket.SLogUsuarioMod = Session["Cod_Usuario"].ToString();
        objPrecioTicket.STipIngreso = "1";//INGRESO MANUAL
        objPrecioTicket.IdTxCritica = idTxCritica;

        if (!bCheckProg) //Precio Ticket actual
        {
            objPrecioTicket.STipEstado = "1";//ACTIVO
            objPrecioTicket.SCodPrecioTicket = (string)foundRow[0]["Cod_Precio_Ticket"];//Historico
        }
        else //Precio Ticket programado
        {
            objPrecioTicket.STipEstado = "0";//INACTIVO

            DateTime dateValue = DateTime.MinValue;
            if (Fecha.getFechaCustom2(txtFechaProg.Text, txtHoraProg.Text, out dateValue) > 0)
                objPrecioTicket.DtFchProgramacion = dateValue;
        }
        if (objBOAdministracion.RegistrarPrecioTicketCrit(objPrecioTicket))
        {
            if (objPrecioTicket.SCodRetorno != null && objPrecioTicket.SCodRetorno == "000")
            {
                //Registro Satisfactorio
                if (bCheckProg)
                    strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError2.Text"].ToString(), objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, objPrecioTicket.DtFchProgramacion) + "<br>";
                else
                    strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError1.Text"].ToString(), objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio) + "<br>";
                htSuccess.Add(htSuccess.Count + 1, strMessage);
            }
            else
            {
                //Error al registrar la Tasa de Cambio
                object[] objParams = { bCheckProg, objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, txtFechaProg.Text, txtHoraProg.Text };
                SetError(objPrecioTicket.SCodRetorno, objParams, htErrors);
            }
        }
        else
        {
            //Error al registrar la Tasa de Cambio
            object[] objParams = { bCheckProg, objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, txtFechaProg.Text, txtHoraProg.Text };
            SetError(Define.ERR_402, objParams, htErrors);
        }
    }

    private void InsertPrecio(string strTipoTicket, string strMoneda, bool bCheckProg, string strPrecio, Hashtable htSuccess, Hashtable htErrors)
    {
        string strMessage = "";

        PrecioTicket objPrecioTicket = new PrecioTicket();
        objPrecioTicket.SCodTipoTicket = strTipoTicket;
        objPrecioTicket.SCodMoneda = strMoneda;
        objPrecioTicket.DImpPrecio = decimal.Parse(strPrecio);
        objPrecioTicket.SLogUsuarioMod = Session["Cod_Usuario"].ToString();
        objPrecioTicket.STipIngreso = "1";//INGRESO MANUAL

        if (bCheckProg)
        {
            objPrecioTicket.STipEstado = "0";//INACTIVO

            DateTime dateValue = DateTime.MinValue;
            if (Fecha.getFechaCustom2(txtFechaProg.Text, txtHoraProg.Text, out dateValue) > 0)
                objPrecioTicket.DtFchProgramacion = dateValue;
        }
        else
        {
            objPrecioTicket.STipEstado = "1";//ACTIVO
        }

        if (objBOAdministracion.RegistrarPrecioTicket(objPrecioTicket))
        {
            if (objPrecioTicket.SCodRetorno != null && objPrecioTicket.SCodRetorno == "000")
            {
                //Registro Satisfactorio
                if (bCheckProg)
                    strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError2.Text"].ToString(), objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, objPrecioTicket.DtFchProgramacion) + "<br>";
                else
                    strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError1.Text"].ToString(), objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio) + "<br>";
                htSuccess.Add(htSuccess.Count + 1, strMessage);
            }
            else
            {
                //Error al registrar la Tasa de Cambio
                object[] objParams = { bCheckProg, objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, txtFechaProg.Text, txtHoraProg.Text };
                SetError(objPrecioTicket.SCodRetorno, objParams, htErrors);
            }
        }
        else
        {
            //Error al registrar la tasa de cambio
            object[] objParams = { bCheckProg, objPrecioTicket.SCodTipoTicket, objPrecioTicket.SCodMoneda, objPrecioTicket.DImpPrecio, txtFechaProg.Text, txtHoraProg.Text };
            SetError(Define.ERR_402, objParams, htErrors);
        }
    }

    private void SetError(string strIdError, object[] objParams, Hashtable htErrors)
    {
        string strMessage = "";
        strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strIdError])["MESSAGE"] + "<br>";
        if ((bool)objParams[0])
        {
            DateTime dateValue = DateTime.MinValue;
            int iOk = Fecha.getFechaCustom2((string)objParams[4], (string)objParams[5], out dateValue);
            strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError2.Text"].ToString(), (string)objParams[1] ?? "", (string)objParams[2], objParams[3] ?? "", (iOk > 0) ? dateValue + "" : "") + "<br>";
        }
        else
            strMessage += String.Format(htLabels["opePrecioTicket.lblMensajeError1.Text"].ToString(), (string)objParams[1] ?? "", (string)objParams[2], objParams[3] ?? "") + "<br>";
        htErrors.Add(htErrors.Count + 1, strMessage);
    }

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        int totalCell = 0;
        bool bIsValid = true;
        bool bCheckProg = false;
        
        string strMessageA = "";
        string strMessage = "";

        Hashtable htSuccess = new Hashtable();
        Hashtable htErrors = new Hashtable();

        htLabels = LabelConfig.htLabels;
        
        try
        {
            bCheckProg = this.rbtnFchProg.Checked;

            if (bCheckProg)
            {
                if (!ValidateFecha(txtFechaProg.Text, txtHoraProg.Text))
                {
                    bIsValid = false;
                    lblMensajeError.Text = "La fecha y/o hora de programacion debe ser mayor a la fecha y/o hora actual";
                }
            }

            if (bIsValid)
            {
                Int64 idTxCritica = 0;
                idTxCritica = objBOAdministracion.obtenerIdTransaccionCritica();

                for (int i = 0; i < grvPrecioTicket.Rows.Count; i++)
                {
                    Label lblTmpCodPrecio = (Label)grvPrecioTicket.Rows[i].FindControl("lblITCodigo");
                    CheckBox chk_Precio = (CheckBox)grvPrecioTicket.Rows[i].FindControl("chkPrecio");

                    HiddenField hdTipoTicket = (HiddenField)grvPrecioTicket.Rows[i].FindControl("idTipoTicket");
                    HiddenField hdMoneda = (HiddenField)grvPrecioTicket.Rows[i].FindControl("idMoneda");
                    
                    string strCodigo = "";
                    DataRow[] foundRow = null;
                    decimal dbPrecio;

                    if (chk_Precio.Checked)
                    {
                        totalCell++;
                        strCodigo = lblTmpCodPrecio.Text ?? "";
                        
                        if (strCodigo.Length > 0)
                            foundRow = dtPrecioTicket.Select("Cod_Precio_Ticket = '" + strCodigo + "'");

                        TextBox txtTmpPrecio = (TextBox)grvPrecioTicket.Rows[i].FindControl("idPrecio");
                        DropDownList ddlTmpMoneda = (DropDownList)grvPrecioTicket.Rows[i].FindControl("ddlMoneda");

                        dbPrecio = ValidateImporte(txtTmpPrecio.Text);

                        if (dbPrecio >= 0)
                        {
                            if (foundRow != null && foundRow.Length > 0) //Reemplazar porque ya existe uno con las mismas caracteristicas
                                UpdatePrecio(foundRow, bCheckProg, txtTmpPrecio.Text, ddlTmpMoneda.SelectedValue, htSuccess, 
                                    htErrors, idTxCritica);
                            else // Nueva tasa de cambio
                                InsertPrecio(hdTipoTicket.Value.ToString(), ddlTmpMoneda.SelectedValue, bCheckProg, txtTmpPrecio.Text, htSuccess, htErrors);
                        }
                        else
                        {
                            object[] objParams = { bCheckProg, hdMoneda.Value.ToString(), "C", txtTmpPrecio.Text, txtFechaProg.Text, txtHoraProg.Text };
                            SetError(Define.ERR_405, objParams, htErrors);
                        }
                    }
                }
                strMessage = "";
                if (totalCell > 0)
                {
                    if (htSuccess.Count > 0 || htErrors.Count > 0)
                    {
                        if (htSuccess.Count > 0)
                        {
                            strMessage += "Registros Exitosos : " + htSuccess.Count + "<br>";
                            foreach (DictionaryEntry item in htSuccess)
                                strMessage += " " + item.Value.ToString();
                            strMessageA = strMessage;

                            string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));

                            if (bCheckProg)
                            {
                                //GeneraAlarma
                                string IpClient = Request.UserHostAddress;                                
                                GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000019", "004", 
                                    IpClient, "1", "Alerta W0000019", "<br/>" + strMessageA + "<br/> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), 
                                    Convert.ToString(Session["Cod_Usuario"]),idTxCritica, "TipoTicket", pathMap);

                            }
                            else
                            {
                                //GeneraAlarma
                                string IpClient = Request.UserHostAddress;
                                GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000018", "004", 
                                    IpClient, "1", "Alerta W0000018", "<br/>" + strMessageA + "<br/> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), 
                                    Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "TipoTicket", pathMap);
                            }
                        }
                        if (htErrors.Count > 0)
                        {
                            strMessage += "Registros Fallidos : " + htErrors.Count + "<br>";
                            foreach (DictionaryEntry item in htErrors)
                                strMessage += " " + item.Value.ToString();
                        }
                    }
                    else
                        strMessage = "No se pudo registrar ningun precio de ticket";
                    omb.ShowMessage(strMessage, "Creación de Precio de Ticket", "Ope_VerPrecioTicket.aspx");
                }
                else
                    lblMensajeError.Text = "No ha ingresado ninguna precio de ticket.";
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {

            /*if (hPrecioManual)
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000018", "004", IpClient, "1", "Alerta W0000018", sManual + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

            if (hPrecioProg)
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000019", "004", IpClient, "1", "Alerta W0000019", sProgramado + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }
            */
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
        //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnGrabar');", true);
    }

    private string getPathMap(SiteMapNode oSMN)
    {
        string pathMap = string.Empty;

        if (oSMN.ParentNode == null)
        {
            pathMap = oSMN.Title;
        }
        else
        {
            pathMap = getPathMap(oSMN.ParentNode) + " : " + oSMN.Title;
        }
        return pathMap;
    }

    protected void CheckP_Changed(object sender, EventArgs e)
    {
        CheckBox thisCheckBox = (CheckBox)sender;
        GridViewRow thisGridViewRow = (GridViewRow)thisCheckBox.Parent.Parent;
        TextBox tb = (TextBox)thisGridViewRow.FindControl("idPrecio");
        if (thisCheckBox.Checked)
        {
            tb.ReadOnly = false;
            int row = thisGridViewRow.RowIndex;
            tb.BackColor = System.Drawing.Color.LightYellow;
        }
        else
        {
            tb.ReadOnly = true;
            tb.BackColor = System.Drawing.Color.White;
        }
        //thisTextBox.ForeColor = System.Drawing.Color.Red;        
    }

    private void BloquearMonedaTurnoPendiente()
    {
        BO_Operacion objBOOpera=new BO_Operacion();
        DataTable dtResultado = objBOOpera.ListarTurnosAbiertos(string.Empty);
        if (dtResultado.Rows.Count > 0)
        {
            if (rbtnFchAct.Checked)
            {
                for (int i = 0; i < grvPrecioTicket.Rows.Count; i++)
                {
                    DropDownList ddlTmpMoneda = (DropDownList)grvPrecioTicket.Rows[i].FindControl("ddlMoneda");
                    if (ddlTmpMoneda != null)
                    {
                        lblMensajeError.Text = "PRECAUCIÓN: Existen Turnos Pendientes.";
                        ddlTmpMoneda.Enabled = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < grvPrecioTicket.Rows.Count; i++)
                {
                    DropDownList ddlTmpMoneda = (DropDownList)grvPrecioTicket.Rows[i].FindControl("ddlMoneda");
                    if (ddlTmpMoneda != null)
                    {
                        lblMensajeError.Text = "";
                        ddlTmpMoneda.Enabled = true;
                    }
                }
            }
        }
    }
}
