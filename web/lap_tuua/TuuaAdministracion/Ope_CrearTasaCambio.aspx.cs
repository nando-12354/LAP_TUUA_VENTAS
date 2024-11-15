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
public partial class Ope_CrearTasaCambio : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    private DataTable monedaTbl;
    private DataTable listacampoTbl;
    private DataTable pivotTbl;
    private DataTable tasacambioTbl;
    //private DataTable dtResultMoneda;

    BO_Administracion objBOAdministracion = new BO_Administracion(Define.CNX_12); //new BO_Administracion();
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            monedaTbl = GetMonedaTable();
            listacampoTbl = GetListaCampoTable("TipoTasaCambio");
            tasacambioTbl = GetTasaCambioTable();
            
		    if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                rbtnFchAct.Checked = true;
                pivotTbl = PivotTable(monedaTbl, listacampoTbl);
                
                grvTasaCambio.DataSource = pivotTbl;
                grvTasaCambio.DataBind();
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    /*private DataTable ResultTable(DataTable dtTipoMoneda, DataTable dtTasaCambio)
    {
        DataTable dest = new DataTable("Result" + dtTipoMoneda.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.Caption = "Tasa de Cambio Actual";
        dc.ColumnName = "Cod_Tasa_Cambio";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Moneda";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tip_Cambio";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Moneda";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Imp_Cambio_Actual";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Campo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Simbolo";
        dest.Columns.Add(dc);

        DataRow[] foundRowTipoMoneda = dtTipoMoneda.Select("Tip_Estado = 'Vigente'");

        if (foundRowTipoMoneda != null && foundRowTipoMoneda.Length > 0)
        {
            for (int i = 0; i < foundRowTipoMoneda.Length; i++)
                dest.Rows.Add(dest.NewRow());

            try
            {
                int j = 0;
                DataRow[] foundRowTasaCambio;
                string row_filter;
                foreach (DataRow r in foundRowTipoMoneda)
                {
                    dest.Rows[j][1] = r["Cod_Moneda"].ToString();

                    row_filter = "Cod_Moneda ='" + r["Cod_Moneda"].ToString() + "'";

                    foundRowTasaCambio = dtTasaCambio.Select(row_filter);

                    if (foundRowTasaCambio != null && foundRowTasaCambio.Length > 0)
                    {
                        dest.Rows[j][0] = foundRowTasaCambio[0]["Cod_Tasa_Cambio"].ToString();
                        dest.Rows[j][2] = foundRowTasaCambio[0]["Tip_Cambio"].ToString();
                        dest.Rows[j][3] = foundRowTasaCambio[0]["Dsc_Moneda"].ToString();
                        dest.Rows[j][4] = foundRowTasaCambio[0]["Imp_Cambio_Actual"].ToString();
                        dest.Rows[j][5] = foundRowTasaCambio[0]["Dsc_Campo"].ToString();
                        dest.Rows[j][6] = foundRowTasaCambio[0]["Dsc_Simbolo"].ToString();

                    }
                    else
                    {
                        dest.Rows[j][0] = "";
                        dest.Rows[j][2] = "C";
                        dest.Rows[j][3] = r["Dsc_Moneda"].ToString();
                        dest.Rows[j][4] = "0.00";
                        dest.Rows[j][5] = "Compra";
                        dest.Rows[j][6] = r["Dsc_Simbolo"].ToString(); 
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
            this.lblMensajeError.Text = "No existen Tipo de Monedas vigentes";
            grvTasaCambio2.DataSource = null;
            grvTasaCambio2.DataBind();
        }
       
        

        dest.AcceptChanges();
        return dest;
    }*/

    private DataTable GetMonedaTable()
    {
        DataTable dt_consulta = new DataTable();
        dt_consulta = objBOAdministracion.listarAllMonedas();
        //DataRow[] foundRow = dt_consulta.Select("Tip_Estado = 'Vigente'");
        
        //foundRow.Length
        //dt_consulta.Rows.Clear();
        //foreach (DataRow dr in foundRow)
        //{   
       //     dt_consulta.Rows.Add(dr);            
       // }
        return dt_consulta;
    }

    private DataTable GetListaCampoTable(string strCampo)
    {
        DataTable dt_consulta = new DataTable();
        dt_consulta = objBOConfiguracion.ObtenerListaDeCampo(strCampo,"");
        return dt_consulta;
    }

    private DataTable GetTasaCambioTable()
    {
        DataTable dt_consulta = new DataTable();
        dt_consulta = objBOAdministracion.ObtenerTasaCambio("");

        return dt_consulta;
    }

    private DataTable PivotTable(DataTable srcMoneda, DataTable srcListaCampo)
    {
        DataTable dest = new DataTable("Pivoted" + srcMoneda.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.Caption = "Tasa de Cambio Actual";
        dc.ColumnName = "Dsc_Moneda";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Moneda";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Simbolo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Tasa_Cambio_Compra";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tip_Cambio_Compra";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Imp_Cambio_Actual_Compra";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Tasa_Cambio_Venta";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tip_Cambio_Venta";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Imp_Cambio_Actual_Venta";
        dest.Columns.Add(dc);

        DataRow[] foundRowMoneda = srcMoneda.Select("Tip_Estado = 'Vigente' AND Cod_Moneda <> 'SOL'");

        for (int i = 0; i < foundRowMoneda.Length; i++)
            dest.Rows.Add(dest.NewRow());

        try
        {
            int j = 0;
            string row_filter;
            DataRow[] foundRowTasaCambio;
            foreach (DataRow r in foundRowMoneda)
            {
                dest.Rows[j][0] = r["Dsc_Moneda"].ToString();
                dest.Rows[j][1] = r["Cod_Moneda"].ToString();
                dest.Rows[j][2] = r["Dsc_Simbolo"].ToString();
                //Compra
                row_filter = "Cod_Moneda ='" + r["Cod_Moneda"].ToString() + "' AND Tip_Cambio = 'C' AND Tip_Estado = '1'";
                foundRowTasaCambio = tasacambioTbl.Select(row_filter);

                if (foundRowTasaCambio != null && foundRowTasaCambio.Length > 0)
                {
                    dest.Rows[j][3] = foundRowTasaCambio[0]["Cod_Tasa_Cambio"].ToString();
                    dest.Rows[j][4] = foundRowTasaCambio[0]["Tip_Cambio"].ToString();
                    dest.Rows[j][5] = foundRowTasaCambio[0]["Imp_Cambio_Actual"].ToString();
                }
                else
                {
                    dest.Rows[j][3] = "";
                    dest.Rows[j][4] = "C";
                    dest.Rows[j][5] = "0.0000";                    
                }
                //Venta
                row_filter = "Cod_Moneda ='" + r["Cod_Moneda"].ToString() + "' AND Tip_Cambio = 'V' AND Tip_Estado = '1'";
                foundRowTasaCambio = tasacambioTbl.Select(row_filter);

                if (foundRowTasaCambio != null && foundRowTasaCambio.Length > 0)
                {
                    dest.Rows[j][6] = foundRowTasaCambio[0]["Cod_Tasa_Cambio"].ToString();
                    dest.Rows[j][7] = foundRowTasaCambio[0]["Tip_Cambio"].ToString();
                    dest.Rows[j][8] = foundRowTasaCambio[0]["Imp_Cambio_Actual"].ToString();
                }
                else
                {
                    dest.Rows[j][6] = "";
                    dest.Rows[j][7] = "V";
                    dest.Rows[j][8] = "0.0000";
                }
                j++;
            }            
        }
        catch (IndexOutOfRangeException iore)
        {

        }
        dest.AcceptChanges();
        return dest;
    }

    private void BindHorzGrid()
    {
        grvTasaCambio.AutoGenerateColumns = false;
        grvTasaCambio.Columns.Clear();
        int i = 0;
        foreach (DataColumn col in pivotTbl.Columns)
        {
            if (col.ColumnName != "cCode")
            {
                TemplateField bfield = new TemplateField();
                bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, col.Caption);

                if (i == 0)
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.Item, col.ColumnName);
                else
                {
                    bfield.ItemTemplate = new GridViewTemplate(ListItemType.EditItem, col.ColumnName);
                    bfield.ControlStyle.Width = Unit.Percentage(90);
                }
                grvTasaCambio.Columns.Add(bfield);
                i++;
            }
        }
        grvTasaCambio.DataSource = pivotTbl;
        grvTasaCambio.DataBind();
    }

    /// <summary>
    /// ValidarFecha
    /// </summary>
    /// <param name="pFchProga"></param>
    /// <param name="pHrProga"></param>
    /// <returns></returns>
    public bool ValidarFecha(string pFchProga, string pHrProga)
    {
        string pFchActual = DateTime.Now.ToShortDateString();
        string pHrActual = DateTime.Now.ToLongTimeString();

        DateTime DateProgram = new DateTime();
        if (Fecha.getFechaCustom2(pFchProga, pHrProga, out DateProgram)>0)
        {                                    
        }

        int valor=DateTime.Compare(DateProgram, DateTime.Now);
        if (valor > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ValidarImporte
    /// </summary>
    /// <param name="strImporte"></param>
    /// <returns></returns>
    private decimal ValidarImporte(string strImporte)
    {
        decimal dImporte = 0;
        try
        {
            dImporte = decimal.Parse(strImporte);

            if (!(dImporte >= 0 && dImporte < 10000))
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

    private void UpdateTasaCambio(DataRow[] foundRow, bool bCheckProg, string strPrecio, Hashtable htSuccess, Hashtable htErrors, Int64 idTxCritica)
    {
        string strMessage = "";
        //objBOAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        objBOAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], 
            (string)Session["Cod_SubModulo"], Define.CNX_12);
        TasaCambio objTasaCambio = new TasaCambio();
        objTasaCambio.STipCambio = (string)foundRow[0]["Tip_Cambio"];
        objTasaCambio.SCodMoneda = (string)foundRow[0]["Cod_Moneda"];
        objTasaCambio.DImpCambioActual = decimal.Parse(strPrecio);
        objTasaCambio.SLogUsuarioMod = Session["Cod_Usuario"].ToString();
        objTasaCambio.STipIngreso = "1";//INGRESO MANUAL
        objTasaCambio.IdTxCritica = idTxCritica;

        if (!bCheckProg) //Tasa de Cambio actual
        {
            objTasaCambio.STipEstado = "1";//ACTIVO
            objTasaCambio.SCodTasaCambio = (string)foundRow[0]["Cod_Tasa_Cambio"];//Historico
        }
        else //Tasa de Cambio programado
        {
            objTasaCambio.STipEstado = "0";//INACTIVO
            
            DateTime dateValue = DateTime.MinValue;
            if (Fecha.getFechaCustom2(txtFechaProg.Text, txtHoraProg.Text, out dateValue) > 0)
                objTasaCambio.DtFchProgramacion = dateValue;
        }
        if (objBOAdministracion.RegistrarTasaCambioCrit(objTasaCambio))
        {
            if (objTasaCambio.SCodRetorno != null && objTasaCambio.SCodRetorno == "000")
            {
                //Registro Satisfactorio
                if (bCheckProg)
                    strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError2.Text"].ToString(), objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, objTasaCambio.DtFchProgramacion) + "<br>";
                else
                    strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError1.Text"].ToString(), objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual) + "<br>";    
                htSuccess.Add(htSuccess.Count + 1, strMessage);
            }
            else
            {
                //Error al registrar la Tasa de Cambio
                object[] objParams = { bCheckProg, objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, txtFechaProg.Text, txtHoraProg.Text };
                SetError(objTasaCambio.SCodRetorno, objParams, htErrors);
            }
        }
        else
        {
            //Error al registrar la Tasa de Cambio
            object[] objParams = { bCheckProg, objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, txtFechaProg.Text, txtHoraProg.Text };
            SetError(Define.ERR_402, objParams, htErrors);
        } 
    }

    /// <summary>
    /// InsertTasaCambio
    /// </summary>
    /// <param name="strMoneda"></param>
    /// <param name="bCheckProg"></param>
    /// <param name="strPrecio"></param>
    /// <param name="strTipoCambio"></param>
    /// <param name="htSuccess"></param>
    /// <param name="htErrors"></param>
    private void InsertTasaCambio(string strMoneda, bool bCheckProg, string strPrecio, string strTipoCambio, Hashtable htSuccess, Hashtable htErrors) {
        string strMessage = "";
        //objBOAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        objBOAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], 
            (string)Session["Cod_SubModulo"], Define.CNX_12);
        TasaCambio objTasaCambio = new TasaCambio();
        objTasaCambio.SCodMoneda = strMoneda;
        objTasaCambio.STipCambio = strTipoCambio;
        objTasaCambio.DImpCambioActual = decimal.Parse(strPrecio);
        objTasaCambio.SLogUsuarioMod = Session["Cod_Usuario"].ToString();
        objTasaCambio.STipIngreso = "1";//MANUAL
        if (bCheckProg)
        {
            objTasaCambio.STipEstado = "0";//INACTIVO
            
            DateTime dateValue = DateTime.MinValue;
            if (Fecha.getFechaCustom2(txtFechaProg.Text, txtHoraProg.Text, out dateValue) > 0)
                objTasaCambio.DtFchProgramacion = dateValue;
        }
        else
        {
            objTasaCambio.STipEstado = "1";//ACTIVO
        }

        if (objBOAdministracion.RegistrarTasaCambio(objTasaCambio))
        {
            if (objTasaCambio.SCodRetorno != null && objTasaCambio.SCodRetorno == "000")
            {
                //Registro Satisfactorio
                if (bCheckProg)
                    strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError2.Text"].ToString(), objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, objTasaCambio.DtFchProgramacion) + "<br>";
                else
                    strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError1.Text"].ToString(), objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual) + "<br>";
                htSuccess.Add(htSuccess.Count + 1, strMessage);
            }
            else
            {
                //Error al registrar la Tasa de Cambio
                object[] objParams = { bCheckProg, objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, txtFechaProg.Text, txtHoraProg.Text };
                SetError(objTasaCambio.SCodRetorno, objParams, htErrors);
            }
        }
        else
        {
            //Error al registrar la tasa de cambio
            object[] objParams = { bCheckProg, objTasaCambio.SCodMoneda, objTasaCambio.STipCambio, objTasaCambio.DImpCambioActual, txtFechaProg.Text, txtHoraProg.Text };
            SetError(Define.ERR_402, objParams, htErrors);
        }
    }

    /// <summary>
    /// SetError
    /// </summary>
    /// <param name="strIdError"></param>
    /// <param name="objParams"></param>
    /// <param name="htErrors"></param>
    private void SetError(string strIdError, object[] objParams, Hashtable htErrors)
    {        
        string strMessage = "";
        strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strIdError])["MESSAGE"] + "<br>";
        if ((bool)objParams[0])
        {
            DateTime dateValue = DateTime.MinValue;
            int iOk = Fecha.getFechaCustom2((string)objParams[4], (string)objParams[5], out dateValue);                
            strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError2.Text"].ToString(), (string)objParams[1] ?? "", (string)objParams[2], objParams[3] ?? "", (iOk > 0)? dateValue + "" : "") + "<br>";
        }
        else
            strMessage += String.Format(htLabels["opeTasaCambio.lblMensajeError1.Text"].ToString(), (string)objParams[1] ?? "", (string)objParams[2], objParams[3] ?? "") + "<br>";
        htErrors.Add(htErrors.Count + 1, strMessage);
    }

    /// <summary>
    /// btnGrabar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        int totalRows = 0;
        int totalCell = 0;
        bool bIsValid = true;        
        bool bCheckProg = false;
        bool bFlagError = false;
        string strMessageA = "";
        string strMessage = "";

        Hashtable htSuccess = new Hashtable();
        Hashtable htErrors = new Hashtable();

        htLabels = LabelConfig.htLabels;

        Int64 idTxCritica = 0;

        try
        {
            bCheckProg = this.rbtnFchProg.Checked;
            totalRows = grvTasaCambio.Rows.Count;

            if (bCheckProg) {
                if (!ValidarFecha(txtFechaProg.Text, txtHoraProg.Text)) {
                    bIsValid = false;
                    lblMensajeError.Text = "La fecha y/o hora de programacion debe ser mayor a la fecha y/o hora actual";
                }                
            }

            if (bIsValid)
            {
                for (int i = 0; i < totalRows; i++)
                {
                    HiddenField hd1 = (HiddenField)grvTasaCambio.Rows[i].FindControl("idTasaCambioCompra");
                    HiddenField hd2 = (HiddenField)grvTasaCambio.Rows[i].FindControl("idTasaCambioVenta");
                    CheckBox chk_Compra = (CheckBox)grvTasaCambio.Rows[i].FindControl("chkCompra");
                    CheckBox chk_Venta = (CheckBox)grvTasaCambio.Rows[i].FindControl("chkVenta");

                    string strCodigo = "";
                    DataRow[] foundRow = null;
                    decimal dbCompra = 0, dbVenta = 0;

                    idTxCritica = objBOAdministracion.obtenerIdTransaccionCritica();

                    if (chk_Compra.Checked)
                    {
                        totalCell++;
                        //Compra
                        strCodigo = hd1.Value.ToString() ?? "";
                        foundRow = null;

                        if (strCodigo.Length > 0)
                            foundRow = tasacambioTbl.Select("Cod_Tasa_Cambio = '" + strCodigo + "'");
                        
                        TextBox txtTmpPrecio = (TextBox)grvTasaCambio.Rows[i].FindControl("idCompra");
                        HiddenField hdMoneda = (HiddenField)grvTasaCambio.Rows[i].FindControl("idMoneda");

                        dbCompra = ValidarImporte(txtTmpPrecio.Text);
                        
                        // TASA CAMBIO DE COMPRA
                        if (dbCompra >= 0)
                        {
                            if (foundRow != null && foundRow.Length > 0) //Reemplazar porque ya existe uno con las mismas caracteristicas
                            {
                                UpdateTasaCambio(foundRow, bCheckProg, txtTmpPrecio.Text, htSuccess, htErrors, idTxCritica);
                            }
                            else // Nueva tasa de cambio
                            {
                                InsertTasaCambio(hdMoneda.Value.ToString(), bCheckProg, txtTmpPrecio.Text, "C", htSuccess, htErrors);
                            }
                        }
                        else {
                            object[] objParams = { bCheckProg, hdMoneda.Value.ToString(), "C", txtTmpPrecio.Text, txtFechaProg.Text, txtHoraProg.Text};
                            SetError(Define.ERR_405, objParams, htErrors);
                        }
                    }

                    if (chk_Venta.Checked)
                    {
                        totalCell++;
                        strCodigo = "";
                        foundRow = null;
                        //Venta
                        strCodigo = hd2.Value.ToString() ?? "";
                        if (strCodigo.Length > 0)
                            foundRow = tasacambioTbl.Select("Cod_Tasa_Cambio = '" + strCodigo + "'");
                        
                        TextBox txtTmpPrecioVenta = (TextBox)grvTasaCambio.Rows[i].FindControl("idVenta");
                        HiddenField hdMoneda = (HiddenField)grvTasaCambio.Rows[i].FindControl("idMoneda");
                                
                        dbVenta = ValidarImporte(txtTmpPrecioVenta.Text);
                        
                        //TASA CAMBIO DE VENTA
                        if (dbVenta >= 0)
                        {
                            if (foundRow != null && foundRow.Length > 0)
                            {
                                UpdateTasaCambio(foundRow, bCheckProg, txtTmpPrecioVenta.Text, htSuccess, htErrors, idTxCritica);
                            }
                            else
                            {
                                InsertTasaCambio(hdMoneda.Value.ToString(), bCheckProg, txtTmpPrecioVenta.Text, "V", htSuccess, htErrors);
                            }
                        }
                        else
                        {
                            object[] objParams = { bCheckProg, hdMoneda.Value.ToString(), "V", txtTmpPrecioVenta.Text, txtFechaProg.Text, txtHoraProg.Text };
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
                        }
                        if (htErrors.Count > 0)
                        {
                            strMessage += "Registros Fallidos : " + htErrors.Count + "<br>";
                            foreach (DictionaryEntry item in htErrors)
                                strMessage += " " + item.Value.ToString();                            
                        }
                    }
                    else
                        strMessage = "No se pudo registrar ninguna tasa de cambio";
                    omb.ShowMessage(strMessage, "Creacion de Tasa de Cambio", "Ope_VerTasaCambio.aspx");
                }
                else 
                    lblMensajeError.Text = "No ha ingresado ninguna tasa de cambio.";
            }
        }
        catch (Exception ex)
        {
            bFlagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (strMessageA != "")
            {
                string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));
        //        string ctaModificador = htLabels["CabeceraMenu.lblUsuario"].ToString();
        //        string fchModificacion,
        //string TipoCambio, string moneda, string tasaOld, string tasaNew, string fchIniVigenciaOld,
        //string fchIniVigenciaNew, string usuarioOld, string usuarioNew
        //        FormatearBodyMessage(pathMap, );
                if (!bCheckProg)
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    //GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000016", "004", IpClient, "1", "Alerta W0000016", strMessageA + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                    GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000016", "004", IpClient, "1", 
                        "Alerta W0000016", strMessageA + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), 
                        Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "TasaCambio", pathMap);
                }
                else
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    //GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000017", "004", IpClient, "1", "Alerta W0000017", strMessageA + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                    GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000017", "004", IpClient, "1", 
                        "Alerta W0000017", strMessageA + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]), 
                        Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "TasaCambio", pathMap);
                }
            }
            if (bFlagError)
                Response.Redirect("PaginaError.aspx");            
        }
    }

    /*protected void TextBoxC_TextChanged(object sender, EventArgs e)
    {
        TextBox thisTextBox = (TextBox)sender;
        thisTextBox.ForeColor = System.Drawing.Color.Red;
        GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
        int row = thisGridViewRow.RowIndex;
    }
    protected void TextBoxV_TextChanged(object sender, EventArgs e)
    {
        TextBox thisTextBox = (TextBox)sender;
        thisTextBox.ForeColor = System.Drawing.Color.Red;
        GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
        int row = thisGridViewRow.RowIndex;
    }*/

    protected void CheckC_Changed(object sender, EventArgs e)
    {
        CheckBox thisCheckBox = (CheckBox)sender;
        GridViewRow thisGridViewRow = (GridViewRow)thisCheckBox.Parent.Parent;
        TextBox tb = (TextBox)thisGridViewRow.FindControl("idCompra");
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
    }
    protected void CheckV_Changed(object sender, EventArgs e)
    {
        CheckBox thisCheckBox = (CheckBox)sender;
        GridViewRow thisGridViewRow = (GridViewRow)thisCheckBox.Parent.Parent;
        TextBox tb = (TextBox)thisGridViewRow.FindControl("idVenta");
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

 }
