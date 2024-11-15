using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using Define = LAP.TUUA.UTIL.Define;

public partial class Reh_TicketsMasivo : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();
    //private Hashtable htDscVuelo = new Hashtable();
    protected Hashtable htParametro;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        spnRehabilitar.Text = "";
        lblErrorMsg.Text = "";
        //btnAgregar.Attributes.Add("onclick", "javascript:alert('ALERT ALERT! ');");
        htParametro = (Hashtable)Session["htParametro"];
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                txtNroTicket.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                hdPortSerialLector.Value = (string)Property.htProperty[Define.COM_SERIAL_LECTOR];

                btnRehabilitar.Text = htLabels["rehabilitacionticketMasivo.btnRehabilitar"].ToString();
                lblNroTicket.Text = htLabels["rehabilitacionticketMasivo.lblNroTicket"].ToString();
                lblMotivo.Text = htLabels["rehabilitacionticketMasivo.lblMotivo"].ToString();
                lblTotalSeleccionados.Text = htLabels["rehabilitacionticketMasivo.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionticketMasivo.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionticketMasivo.lblConformidad"].ToString();

                DataTable dtActivarVueloReh = objBOConsultas.ListarParametros("VR");
                int activarVuelo = Int32.Parse(dtActivarVueloReh.Rows[0]["Valor"].ToString());
                if (activarVuelo == 1)
                {
                    trVuelo.Visible = true;
                    lblNumVuelo.Text = htLabels["rehabilitacionticketMasivo.lblNumVuelo"].ToString();
                }
                else
                {
                    trVuelo.Visible = false;
                }

                DataTable dtActivarConsRepreReh = objBOConsultas.ListarParametros("CR");
                int activarConsRepre = Int32.Parse(dtActivarConsRepreReh.Rows[0]["Valor"].ToString());
                if (activarConsRepre == 1)
                {
                    lnkRepresentante.Visible = true;
                    lblConsRepresentante.Text = htLabels["rehabilitacionticketMasivo.lblConsRepresentante"].ToString();
                }
                else
                {
                    lnkRepresentante.Visible = false;
                }

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    gwvRehabilitarPorTicket.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                //else el default es 10.
                //icano 15-06-10
                DataTable dtActivarExporExcel = objBOConsultas.ListarParametros("EE");
                int activarExporExcel = Int32.Parse(dtActivarExporExcel.Rows[0]["Valor"].ToString());
                if (activarExporExcel != 1)
                {
                    imgExportarExcel.Visible = false;
                    pnlExcel.Visible = false;
                }
                else
                {
                    pnlExcel.Visible = true;
                    imgExportarExcel.Visible = true;
                }
                //fin

                int maxRehabilitaciones = 0;
                dt_parametro = objBOConsultas.ListarParametros("RTM");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxRehabilitaciones = 800;
                }


                lblDescripcionLimite.Text = String.Format("Solo es posible Rehabilitar {0} TICKET por proceso", maxRehabilitaciones); 



            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }

            //DataTable dtTicketDetalle = objBOConsultas.ConsultaDetalleTicket_Reh(txtNroTicket.Text);
            DataTable dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
            gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
            gwvRehabilitarPorTicket.DataBind();

            gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
            gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

            //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");
            DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();   

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }
    #endregion

    #region btnAgregar_Click

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;
        int pageCount = gwvRehabilitarPorTicket.PageCount;
        String codigoTicket;

        #region Obtiene el nro de Ticket
        if (!String.IsNullOrEmpty(Request.Form["DataInput"]))
        {
            String script =
                "<script language=\"javascript\">" +
                "document.forms.item(0).MSCommObj.PortOpen = true;" +
                "document.forms.item(0).DataInput.value=\"\";strCad = \"\";ioPort = 0;" +
                "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
            //No funciona con ajax !!!
            //Response.Write("<script language=\"javascript\">document.forms.item(0).MSCommObj.PortOpen = true;</script>");        //}
            //Request.Form.Remove("DataInput");

            String strTrama = Request.Form["DataInput"].ToString();

            Reader reader = new Reader();
            Hashtable ht = reader.ParseString_Ticket(strTrama);
            if (ht == null)
            {
                lblErrorMsg.Text = "Error en la lectura del ticket";
                return;
            }
            codigoTicket = (String)ht[LAP.TUUA.CONVERSOR.Define.NroTicket];
        }
        else
        {
            codigoTicket = txtNroTicket.Text;
        }
        #endregion

        String numVuelo = txtNumVuelo.Text;
        txtNroTicket.Text = "";
        //txtNumVuelo.Text = "";
        if (codigoTicket.Equals(String.Empty))
        {
            lblErrorMsg.Text = "Error. El Ticket no puede ser vacío";
            return;
        }
        DataTable dtTicketDetalle = objBOConsultas.ConsultaDetalleTicket2_Reh(codigoTicket, "", "","0");
        if (dtTicketDetalle.Rows.Count == 0)
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") no existe";
            return;
        }
        String estado = dtTicketDetalle.Rows[0]["Tip_Estado_Actual"].ToString();
        String iEmitido = dtTicketDetalle.Rows[0]["Emitido"].ToString();

        //icano 17-06-10 validacion de ticket
        String motv_anu = dtTicketDetalle.Rows[0]["Dsc_Motivo"].ToString();
        String fch_venc = dtTicketDetalle.Rows[0]["Fecha_Venc"].ToString();
        String tip_anu = dtTicketDetalle.Rows[0]["Tip_Anulacion"].ToString();
        String log_fec_mod = dtTicketDetalle.Rows[0]["FechaModificacion"].ToString();
        String flag_conti = dtTicketDetalle.Rows[0]["Flg_Contingencia"].ToString();
        String dias_vencidos = dtTicketDetalle.Rows[0]["Di_Vncidos_Log_fec"].ToString();
        string sModalidadVenta = dtTicketDetalle.Rows[0]["Cod_Modalidad_Venta"].ToString();

        if (motv_anu == "")
            motv_anu = "MOTIVO NO REGISTRADO";

        if (estado.Trim().ToUpper().Equals("X") & tip_anu == "1") //anulado
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") esta ANULADO por " + motv_anu + ". Fecha Anulación: " + log_fec_mod; return;
        }
        else if (estado.Trim().ToUpper().Equals("X") & (tip_anu == "2" | tip_anu == "4")) //anulado por extornado
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") esta EXTORNADO. Fecha Extorno: " + log_fec_mod.Substring(0, 10); return;
        }
        else if (estado.Trim().ToUpper().Equals("X") & tip_anu == "3") //anulado por vencimiento
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ")  esta VENCIDO. Fecha Vencimiento: " + fch_venc.Substring(0, 10) + " - Días vencidos: " + dias_vencidos; return;
        }
        else if (flag_conti == "SI")
        {
            //kinzi 17-04-2011 Validacion de Tickets contingencia
            if (estado.Trim().ToUpper().Equals("P"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de CONTINGENCIA esta PREEMITIDO y aun no ha sido USADO"; return;
            }
            if (estado.Trim().ToUpper().Equals("U") & iEmitido.Trim().Equals("0"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de CONTINGENCIA esta USADO y aun no ha sido REGISTRADO su VENTA"; return;
            }
            if (estado.Trim().ToUpper().Equals("E"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de CONTINGENCIA no ha sido USADO, esta en estado EMITIDO. Fecha Emisión: " + log_fec_mod.Substring(0, 10); return;
            }
            if (estado.Trim().ToUpper().Equals("R"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de CONTINGENCIA esta REHABILITADO. Fecha Rehabilitación: " + log_fec_mod.Substring(0, 10); return;
            }
        }
        else if (sModalidadVenta != null && sModalidadVenta.Trim().ToUpper().Equals("M0005"))
        {
            //kinzi 17-04-2011 Validacion de Tickets ATM
            if (estado.Trim().ToUpper().Equals("P"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de ATM esta PREEMITIDO y aun no ha sido USADO"; return;
            }
            if (estado.Trim().ToUpper().Equals("U") & iEmitido.Trim().Equals("0"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de ATM esta USADO y aun no ha sido REGISTRADO su VENTA"; return;
            }
            if (estado.Trim().ToUpper().Equals("E"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de ATM no ha sido USADO, esta en estado EMITIDO. Fecha Emisión: " + log_fec_mod.Substring(0, 10); return;
            }
            if (estado.Trim().ToUpper().Equals("R"))
            {
                lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") de ATM esta REHABILITADO. Fecha Rehabilitación: " + log_fec_mod.Substring(0, 10); return;
            }
        }
        else if (estado.Trim().ToUpper().Equals("E"))
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") no ha sido USADO, esta en estado EMITIDO. Fecha Emisión: " + log_fec_mod.Substring(0, 10); return;
        }
        else if (estado.Trim().ToUpper().Equals("R"))
        {
            lblErrorMsg.Text = "Error. El Ticket (" + codigoTicket + ") esta REHABILITADO. Fecha Rehabilitación: " + log_fec_mod.Substring(0, 10); return;
        }
        //fin

        DataTable dtTicketRehabilitados;
        DataRow row;
        if (ViewState["tabla"] != null)
        {
            dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            #region Verifica si ya se ingreso
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                if (codigoTicket.ToUpper().Trim().Equals(dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString().ToUpper().Trim()))
                {
                    lblErrorMsg.Text = "Codigo de Ticket (" + codigoTicket + ") ya fue ingresado";
                    return;
                }
            }
            #endregion

            #region Guardo las selecciones del combo y checkbox
            pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }
            #endregion

            row = dtTicketRehabilitados.NewRow();
            row["Numero"] = dtTicketRehabilitados.Rows.Count + 1;
        }
        else
        {
            dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            //dtTicketRehabilitados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            //icano 14-06-10
            //inicio
            dtTicketRehabilitados.Columns.Add("Salida", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Vuelo_Venta", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Vuelo_Rehab", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Compania_Venta", System.Type.GetType("System.String"));
            //fin
            row = dtTicketRehabilitados.NewRow();
            row["Numero"] = "1";
        }

        String observaciones = "";
        //icano 14-06-10 : inicialmente estaba declarado dentro de una condicional if, necesito esta variable asi q declara fuera del bucle
        String numeroVuelo = "";
        String compania = "";
        //fin
        #region Observaciones
        DataTable dtParametroGeneral = objBOConsultas.ListarParametros("MR");
        int MaxRehabilitaciones = Int32.Parse(dtParametroGeneral.Rows[0]["Valor"].ToString());
        int numRehabilitaciones = Int32.Parse(dtTicketDetalle.Rows[0]["Num_Rehabilitaciones"].ToString());

        //icano 14-06-10 : obtengo la descripcion del numero de vuelo y compania
        numeroVuelo = dtTicketDetalle.Rows[0]["Dsc_Num_Vuelo"].ToString().Trim();
        compania = dtTicketDetalle.Rows[0]["Dsc_Compania"].ToString().Trim();
        //fin

        if (numRehabilitaciones >= MaxRehabilitaciones)
        {
            observaciones = numRehabilitaciones + " Rehabilitaciones";
        }
        //if (estado.Equals("B"))
        //{
        //    if (!observaciones.Equals(String.Empty))
        //        observaciones += " - ";
        //    observaciones += "Borrado";
        //}
        //else if (estado.Equals("X"))
        //{
        //    if (!observaciones.Equals(String.Empty))
        //        observaciones += " - ";
        //    observaciones += "Anulado";
        //}
        if (trVuelo.Visible && !numVuelo.Trim().Equals(String.Empty))
        {
            numeroVuelo = dtTicketDetalle.Rows[0]["Dsc_Num_Vuelo"].ToString().Trim();

            if (!numeroVuelo.Equals(numVuelo.Trim()))
            {
                if (!observaciones.Equals(String.Empty))
                    observaciones += " - ";
                observaciones += "Vuelo Ingresado: " + numVuelo.Trim() + " Vuelo Antiguo: " + numeroVuelo;
            }
        }
        if (observaciones.Equals(String.Empty))
        {
            observaciones = "-";
        }
        #endregion

        row["Check"] = false;
        row["Observacion"] = observaciones;
        row["Cod_Numero_Ticket"] = dtTicketDetalle.Rows[0]["Cod_Numero_Ticket"];
        //icano 14-06-10 : agregando nuevas columnas para el reporte en excel
        row["Vuelo_Venta"] = numeroVuelo;
        if (numVuelo.Trim() != "")
        {
            row["Vuelo_Rehab"] = numVuelo.Trim();
        }
        else
        {
            row["Vuelo_Rehab"] = "";
        }
        row["Compania_Venta"] = compania;
        //fin
        dtTicketRehabilitados.Rows.Add(row);
        dtTicketRehabilitados.AcceptChanges();
        ViewState["tabla"] = dtTicketRehabilitados;

        #region Define el PageIndex
        if (dtTicketRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorTicket.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorTicket.PageIndex = pageCount - 1;
        }
        #endregion

        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorTicket.PageIndex;

        for (int i = (pageIndex * pageSize), j = 0; j < dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            //cboMotivo.DataSource = dtCausalReh;
            //cboMotivo.DataTextField = "Dsc_Campo";
            //cboMotivo.DataValueField = "Cod_Campo";
            //cboMotivo.DataBind();
            if (i < dtTicketRehabilitados.Rows.Count - 1)
            {
                //cboMotivo.ClearSelection();
                //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
            }
        }
        #endregion

        #region Actualizar Resumen
        lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) + 1);
        #endregion     
    }

    #endregion

    #region gwvRehabilitarPorTicket_RowCommand

    protected void gwvRehabilitarPorTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageCount = gwvRehabilitarPorTicket.PageCount;

            #region Actualizar Resumen
            Boolean isChecked = ((CheckBox)(gwvRehabilitarPorTicket.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
            String observaciones = dtTicketRehabilitados.Rows[rowIndex]["Observacion"].ToString();
            #endregion
            dtTicketRehabilitados.Rows.RemoveAt(rowIndex);
            //dtTicketRehabilitados.AcceptChanges();

            #region Guardo las selecciones del combo y checkbox
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count + 1 - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
            }
            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                if (j != rowIndex - (pageIndex * pageSize))
                {
                    //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
                    dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                    //dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
                }
                else
                {
                    i--;
                }
            }
            #endregion
            for (int i=0; i<dtTicketRehabilitados.Rows.Count;i++)
            {
                dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
            }

            #region Define el PageIndex
            if (dtTicketRehabilitados.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
            {
                gwvRehabilitarPorTicket.PageIndex = pageIndex - 1;
            }
            #endregion

            if (dtTicketRehabilitados.Rows.Count == 0)
            {
                ViewState["tabla"] = null;
                #region Agregando fila vacia a la grilla por default
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());

                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                //gwvRehabilitarPorTicket.Rows[0].Cells[5].Controls.Clear();
                gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;
                //gwvRehabilitarPorTicket.Rows[0].FindControl("cboMotivo").Visible = false;
                #endregion
            }
            else
            {
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
                //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");

                pageIndex = gwvRehabilitarPorTicket.PageIndex;
                pageCount = gwvRehabilitarPorTicket.PageCount;

                if ((pageIndex + 1) < pageCount)
                {
                    limite = pageSize;
                }
                else
                {
                    limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
                }

                for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                {
                    //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                    //cboMotivo.DataSource = dtCausalReh;
                    //cboMotivo.DataTextField = "Dsc_Campo";
                    //cboMotivo.DataValueField = "Cod_Campo";
                    //cboMotivo.DataBind();

                    //cboMotivo.ClearSelection();
                    //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                }
                #endregion

            }

            #region Actualizar Resumen
            if(isChecked)
            {
                hdNumSelTotal.Value = "" + (Int32.Parse(hdNumSelTotal.Value) - 1);
                if (!observaciones.Equals("-"))
                    hdNumSelConObs.Value = "" + (Int32.Parse(hdNumSelConObs.Value) - 1);
                int normales = Int32.Parse(hdNumSelTotal.Value) - Int32.Parse(hdNumSelConObs.Value);
                lblTxtSeleccionados.Text = hdNumSelTotal.Value + " (" + hdNumSelConObs.Value + " Observaciones / " + normales + " Normales)";
            }
            lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) - 1);
            #endregion
        }
        else if (e.CommandName.Equals("ShowTicket"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
            String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString() + "-" + dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            //consTicket.Inicio(codTicket);
            ConsDetTicket.Inicio(codTicket);
        }
    }

    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;

        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        #region Guardo las selecciones del combo y checkbox
        pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        //DataTable dtCausalReh = objBOConsultas.ListaCamposxNombreOrderByDesc("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorTicket.PageIndex;

        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            //cboMotivo.DataSource = dtCausalReh;
            //cboMotivo.DataTextField = "Dsc_Campo";
            //cboMotivo.DataValueField = "Cod_Campo";
            //cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            //cboMotivo.ClearSelection();
            //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion
    }

    #endregion

    #region Colorear las Observaciones negativas - RowDataBound

    protected void gwvRehabilitarPorTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Observacion = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Observacion"));
            if (!Observacion.Equals("-"))
            {
                e.Row.Cells[2].ForeColor = Color.Red;
            }
        }

    }
    #endregion

    #region lnkRepresentante_Click
    protected void lnkRepresentante_Click(object sender, EventArgs e)
    {
        consRepre.Inicio();


    }

    #endregion

    #region btnRefresh_Click
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        if(dtTicketRehabilitados!=null && dtTicketRehabilitados.Rows.Count>0)
        {
            DataTable dtParametroGeneral = objBOConsultas.ListarParametros("MR");
            int MaxRehabilitaciones = Int32.Parse(dtParametroGeneral.Rows[0]["Valor"].ToString());
            String numVuelo = txtNumVuelo.Text;

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++ )
            {
                String observaciones = "";
                #region Observaciones
                String codigoTicket = dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString();
                DataTable dtTicketDetalle = objBOConsultas.ConsultaDetalleTicket_Reh(codigoTicket, "", "");
                
                int numRehabilitaciones = Int32.Parse(dtTicketDetalle.Rows[0]["Num_Rehabilitaciones"].ToString());

                String estado = dtTicketDetalle.Rows[0]["Tip_Estado_Actual"].ToString();

                if (numRehabilitaciones >= MaxRehabilitaciones)
                {
                    observaciones = numRehabilitaciones + " Rehabilitaciones";
                }
                if (estado.Equals("B"))
                {
                    if (!observaciones.Equals(String.Empty))
                        observaciones += " - ";
                    observaciones += "Borrado";
                }
                else if (estado.Equals("X"))
                {
                    if (!observaciones.Equals(String.Empty))
                        observaciones += " - ";
                    observaciones += "Anulado";
                }
                if (/*lblNumVuelo.Visible &&*/ !numVuelo.Trim().Equals(String.Empty))
                {
                    String numeroVuelo = dtTicketDetalle.Rows[0]["Dsc_Num_Vuelo"].ToString().Trim();

                    if (!numeroVuelo.Equals(numVuelo.Trim()))
                    {
                        if (!observaciones.Equals(String.Empty))
                            observaciones += " - ";
                        observaciones += "Vuelo Ingresado: " + numVuelo.Trim() + " Vuelo Antiguo: " + numeroVuelo;
                    }
                }
                if (observaciones.Equals(String.Empty))
                {
                    observaciones = " - ";
                }
                #endregion

                dtTicketRehabilitados.Rows[i]["Observacion"] = observaciones;
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();
            }
         
        }
    }
    #endregion

    #region gwvRehabilitarPorTicket_Sorting
    protected void gwvRehabilitarPorTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        if (dtTicketRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox
        int pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        //No es necesario...creo.
        //gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tabla"] = SortDataTable(dtTicketRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorTicket.DataSource = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorTicket.DataBind();

        #region Actualizo la selecciones del checkBox.
        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
            //cboMotivo.DataSource = dtCausalReh;
            //cboMotivo.DataTextField = "Dsc_Campo";
            //cboMotivo.DataValueField = "Cod_Campo";
            //cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            //cboMotivo.ClearSelection();
            //cboMotivo.Items.FindByText(dtTicketRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion

    }
    #endregion

    #region Metodo Generales para el Sort
    //Method that sorts data
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}", "LastName", "ASC");
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }


    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }
    #endregion

    #region btnRehabilitar_Click
    protected void btnRehabilitar_Click(object sender, EventArgs e)
    {
      
        if (ViewState["tabla"] == null)
        {
            //No hay filas en la grilla
        }
        else
        {
            htLabels = LabelConfig.htLabels;

            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RTM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;//
            }

            DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                //DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                //dtTicketRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Text;
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }
            #endregion

            int nroSeleccionados = dtTicketRehabilitados.Select("Check=true").Count();
            //int nroSeleccionados2 = Int32.Parse((String)Request.Form["hdNumSelTotal"]);
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);//demoran la app
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }

            String motivo = cboMotivo.SelectedItem.Value;

            String numVuelo = "";
            if (trVuelo.Visible && !txtNumVuelo.Text.Trim().Equals(""))
                numVuelo = txtNumVuelo.Text.Trim();

            StringBuilder cod_Numero_Tickets = new StringBuilder();
            int iNumTicket = 0;
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                if (checkedRehabilitar)
                {
                    iNumTicket++;
                    String codigoTicket = dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString();
                    cod_Numero_Tickets.Append(codigoTicket + "|");
                }
            }

            TicketEstHist ticketEstHist = new TicketEstHist();
            ticketEstHist.SCodNumeroTicket = cod_Numero_Tickets.ToString();
            ticketEstHist.SDscNumVuelo = numVuelo;
            ticketEstHist.SCausalRehabilitacion = motivo;
            ticketEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);

            objBORehabilitacion = new BO_Rehabilitacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            bool ret = objBORehabilitacion.registrarRehabilitacionTicket(ticketEstHist, 1, 4000);
            if (!ret)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = htLabels["rehabilitacionticket.lblMensajeError4.Text"].ToString();
                return;
            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000040", "005", IpClient, "1", "Alerta W0000040", "Proceso de Rehabilitacion de Ticket-Masivo, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

            int iTotalNoRehab = 0;

            //Quiere decir que hubieron tickets que estaban en estado != 'U'
            if (ticketEstHist.SCodNumeroTicket.Length > 0)
            {
                String ticketsNoRehabilitados = ticketEstHist.SCodNumeroTicket.Substring(0, ticketEstHist.SCodNumeroTicket.Length - 1);
                String[] ticketsNoReh = ticketsNoRehabilitados.Split(new char[] { '|' });
                for (int i = 0; i < ticketsNoReh.Length; i++)
                {
                    foreach (DataRow row in dtTicketRehabilitados.Rows)
                    {
                        if (ticketsNoReh[i].ToString().Substring(0, 16).Equals(row["Cod_Numero_Ticket"].ToString()))
                        {
                            string strObservacion = "";

                            switch (ticketsNoReh[i].ToString().Substring(16, 1))
                            {
                                case "U": strObservacion = htLabels["rehabilitacionticket.lblMensajeError2.Text"].ToString(); break;
                                case "R": strObservacion = htLabels["rehabilitacionticket.lblMensajeError1.Text"].ToString(); break;
                                default: strObservacion = htLabels["rehabilitacionticket.lblMensajeError3.Text"].ToString(); break;
                            }

                            row["Check"] = false;
                            row["Observacion"] = strObservacion;

                            iTotalNoRehab++;
                            break;
                        }
                    }
                }
            }

            //pnlPrincipal.Attributes.Add("style", "display:none");//Visible=false; generaba problemas con el script de checkbox.
            pnlPrincipal.Visible = false;
            //pnlConformidad.Attributes.Add("style", "display:inherit");
            pnlConformidad.Visible = true;
            tableRehabilitar.Visible = false;

            //See summary
            this.lblTotalRehab.Text = (iNumTicket - iTotalNoRehab) + "";
            //this.lblTotalNoRehab.Text = iTotalNoRehab + "";
            int iTotal = dtTicketRehabilitados.Rows.Count;
            this.lblTotalNoRehab.Text = (iTotal - (iNumTicket - iTotalNoRehab)) + "";

            System.Threading.Thread.Sleep(500);

            String script =
                "<script language=\"javascript\">" +
                "mostrarExcelBtn();" +
                "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
        }
        
        
        //String mensaje = "<script language=\"javascript\">\nalert(\"hola\");\n</script>";
        //Response.Write(mensaje);
    }
    #endregion

    #region btnReporte_click
    protected void btnReporte_click(object sender, ImageClickEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        String url = "ReporteREH/ReporteREH_Ticket.aspx?titulo=Rehabilitacion Tickets Masivo";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtTicketRehabilitados", dtTicketRehabilitados);
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(), "newWindow",
            string.Format("<script type='text/javascript'>window.open('{0}', 'yourWin', '{1}');</script>", url, winFeatures),
            false);
    }
    #endregion

    internal String replacePort(String mensaje)
    {
        mensaje = mensaje.Replace("<%=PORT%>", "COM" + hdPortSerialLector.Value);
        return mensaje;
    }

    #region "ExportarDataTable"
    public void ExportarDataTable(DataTable dt_General)
    {
        //DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        DataTable dtTicketRehabilitados = dt_General;
        DataTable dt = new DataTable();
        
        CheckBox chkSeleccionar;
        DataRow dr;
        
        bool flag;
        int numero_correlativo = 1;
        DateTime dt_fecha = DateTime.Now;
        String strMotivo;

        strMotivo = cboMotivo.SelectedItem.Text.Trim();

        dt.Columns.Add(new DataColumn("Tickets", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero", System.Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("Codigo Ticket", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Observaciones", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Motivo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Salida", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Rehabilitacion", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Usuario Logeado", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Compania", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Vuelo Venta", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Vuelo Rehabilitacion", System.Type.GetType("System.String")));

        if (dtTicketRehabilitados.Rows.Count > 0)
        {

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[i].FindControl("chkSeleccionar");
                flag = (bool)dtTicketRehabilitados.Rows[i]["Check"];

                if (chkSeleccionar.Checked)
                {
                    if (flag)
                    {
                        dr = dt.NewRow();
                        dr["Tickets"] = "Tickets Rehabilitados";
                        dr["Numero"] = numero_correlativo++; 
                        dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = strMotivo;
                        dr["Salida"] = "Ticket Rehabilitado";
                        dr["Fecha Rehabilitacion"] = dt_fecha.ToString();
                        dr["Usuario Logeado"] = Session["Cta_Usuario"];
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                        dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                        dr["Vuelo Rehabilitacion"] = "- " + txtNumVuelo.Text.Trim() + " -";
                        dt.Rows.Add(dr);
                    }
                }

            }

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[i].FindControl("chkSeleccionar");
                flag = (bool)dtTicketRehabilitados.Rows[i]["Check"];

                if (chkSeleccionar.Checked) //verifica los seleccionados para rehabilitar
                {
                    if (!flag) // pero que no han sido rehabilitados por "x" motivos
                    {
                        dr = dt.NewRow();
                        dr["Tickets"] = "Tickets No Rehabilitados";
                        dr["Numero"] = numero_correlativo++;
                        dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                        dr["Observaciones"] = "-";
                        dr["Motivo"] = strMotivo;
                        dr["Salida"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Fecha Rehabilitacion"] = "No aplica";
                        dr["Usuario Logeado"] = "No aplica";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                        dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                        dr["Vuelo Rehabilitacion"] = "- " + txtNumVuelo.Text.Trim() + " -";
                        dt.Rows.Add(dr);
                    }
                }
                if (!chkSeleccionar.Checked) //verifica los que no han sido seleccionados para rehabilitar
                {
                    dr = dt.NewRow();
                    dr["Tickets"] = "Tickets No Rehabilitados";
                    dr["Numero"] = numero_correlativo++;
                    dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                    dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                    dr["Motivo"] = strMotivo;
                    dr["Salida"] = "No seleccionado por DuttyOfficer";
                    dr["Fecha Rehabilitacion"] = "No aplica";
                    dr["Usuario Logeado"] = "No aplica";
                    dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                    dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                    dr["Vuelo Rehab"] = "- " + txtNumVuelo.Text.Trim() + " -";
                    dt.Rows.Add(dr);
                }
            }
            objBOConsultas.ExportarDataTableToExcel(dt, Response);
        }
    }
    #endregion

    #region "btnExportarExcel"
    protected void imgExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportarDataTable((DataTable)ViewState["tabla"]);
    }
    #endregion
    
}
