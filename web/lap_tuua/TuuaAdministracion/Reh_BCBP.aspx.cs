using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using LAP.TUUA.PRINTER;
using System.Xml;
using Define = LAP.TUUA.UTIL.Define;
using Fecha = LAP.TUUA.UTIL.Fecha;

public partial class Reh_BCBP : System.Web.UI.Page
{

    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        spnRehabilitar.Text = "";
        lblErrorMsg.Text = "";
        

        if (!IsPostBack)
        {
            Session["tabla"] = null;
            Session["BcbpResultado"] = null;
            htLabels = LabelConfig.htLabels;
            try
            {
                hdPortSerialLector.Value = (string)Property.htProperty[Define.COM_SERIAL_LECTOR];

                btnRehabilitar.Text = htLabels["rehabilitacionBoarding.btnRehabilitar"].ToString();
                lblCia.Text = htLabels["rehabilitacionBoarding.lblCia"].ToString();
                lblFechaVuelo.Text = htLabels["rehabilitacionBoarding.lblFechaVuelo"].ToString();
                lblVuelo.Text = htLabels["rehabilitacionBoarding.lblVuelo"].ToString();
                lblAsiento.Text = htLabels["rehabilitacionBoarding.lblAsiento"].ToString();
                lblPersona.Text = htLabels["rehabilitacionBoarding.lblPersona"].ToString();
                lblTotalSeleccionados.Text = htLabels["rehabilitacionBoarding.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionBoarding.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionBoarding.lblConformidad"].ToString();
                rowDesc_Cia.Value = htLabels["rehabilitacionBoarding.rowDesc_Cia"].ToString();
                rowDesc_NumVuelo.Value = htLabels["rehabilitacionBoarding.rowDesc_NumVuelo"].ToString();
                rowDesc_FechaVuelo.Value = htLabels["rehabilitacionBoarding.rowDesc_FechaVuelo"].ToString();
                rowDesc_Asiento.Value = htLabels["rehabilitacionBoarding.rowDesc_Asiento"].ToString();
                rowDesc_Pasajero.Value = htLabels["rehabilitacionBoarding.rowDesc_Pasajero"].ToString();

                DataTable dtActivarConsRepreReh = objBOConsultas.ListarParametros("CR");
                int activarConsRepre = Int32.Parse(dtActivarConsRepreReh.Rows[0]["Valor"].ToString());
                if (activarConsRepre == 1)
                {
                    lnkRepresentante.Visible = true;
                    lblConsRepresentante.Text = htLabels["rehabilitacionBoarding.lblConsRepresentante"].ToString();

                    //lblbusquedaBoardingAntiguo.Text = htLabels["rehabilitacionBoarding.lblbusquedaBoardingAntiguo"].ToString();
                    //lblbusquedaBoardingAntiguo.Visible = true;
                }
                else
                {
                    lnkRepresentante.Visible = false;
                    //lblbusquedaBoardingAntiguo.Visible = false;
                }





                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    //gwvRehabilitarPorBCBP.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                    gwvRehabilitarPorBCBP.PageSize = 9000000;
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

                dt_parametro = objBOConsultas.ListarParametros("AA");
                if (dt_parametro.Rows.Count > 0) {
                    if (Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()) == 1)
                    {
                        gwvRehabilitarPorBCBP.Columns[4].Visible = true;
                        gwvRehabilitarPorBCBP.Columns[5].Visible = true;
                        hdMostrarAsociacion.Value = "2";
                    }
                    else
                    {
                        gwvRehabilitarPorBCBP.Columns[4].Visible = false;
                        gwvRehabilitarPorBCBP.Columns[5].Visible = false;
                        hdMostrarAsociacion.Value = "0";
                    }
                }

                int maxRehabilitaciones = 0;
                dt_parametro = objBOConsultas.ListarParametros("RM");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxRehabilitaciones = 800;
                }


                lblDescripcionLimite.Text = String.Format("Solo es posible Rehabilitar {0} BCBP por proceso", maxRehabilitaciones); 
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

            #region Llenando combos de busqueda: Lleno las companias y vacio el cboVuelo
            DataTable dtCompanias = objBOConsultas.listarAllCompania();
            cboCompanias.DataSource = dtCompanias;
            cboCompanias.DataTextField = "Dsc_Compania";
            cboCompanias.DataValueField = "Cod_Compania";
            cboCompanias.DataBind();
            cboCompanias.Items.Insert(0, "Seleccionar");

            cboVuelo.Items.Insert(0, "Seleccionar");
            #endregion

            #region Agregando fila vacia a la grilla por default

            AddingEmptyRow();

            #endregion

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
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int pageCount = gwvRehabilitarPorBCBP.PageCount;

        String compania;
        String fechaVuelo;
        String nroVuelo;
        String asiento;
        String persona;

        String Flag_Fch_Vuelo;

        htLabels = LabelConfig.htLabels;

        #region Obtiene los campos que identifican unicamente a un solo boarding.
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
            //eochoa
            strTrama = HttpUtility.UrlDecode(strTrama, System.Text.Encoding.ASCII);

            Reader reader = new Reader();
            Hashtable ht = reader.ParseString_Boarding(strTrama);
            if (ht == null)
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }
            String companiaAux = (String)ht[LAP.TUUA.CONVERSOR.Define.Compania];
            String fechaVueloAux = (String) ht[LAP.TUUA.CONVERSOR.Define.FechaVuelo];
            nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
            asiento = (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento];
            persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];
            
            if(String.IsNullOrEmpty(companiaAux) || String.IsNullOrEmpty(fechaVueloAux) || 
                String.IsNullOrEmpty(nroVuelo) || String.IsNullOrEmpty(asiento) || String.IsNullOrEmpty(persona))
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;                    
            }

            //-------- EAG 21/01/2010
            reader.ParseHashtable(ht);
            persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];
            nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
            //-------- EAG 21/01/2010

            DataTable dtCompania = objBORehabilitacion.listarCompania_xCodigoEspecial(companiaAux);
            try
            {
                compania = dtCompania.Rows[0]["Cod_Compania"].ToString();
                fechaVuelo = Function.ConvertirJulianoCalendario(Int32.Parse(fechaVueloAux));
            }
            catch(Exception ex)
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return; 
            }
            Flag_Fch_Vuelo = "0";
        }
        else
        {
            compania = cboCompanias.SelectedItem.Value;
            String fechaVueloAux = txtFechaVuelo.Text;
            fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);
            nroVuelo = cboVuelo.SelectedValue;
            asiento = txtAsiento.Text.PadLeft(4, '0');  //EAG 21/01/2010
            persona = txtPersona.Text;

            Flag_Fch_Vuelo = "1";
        }
        #endregion

        DataTable dtBoardingDetalle = objBOConsultas.DetalleBoarding_REH(compania, nroVuelo, fechaVuelo, asiento, persona, "U", null, null, Flag_Fch_Vuelo, null);

        if (dtBoardingDetalle.Rows.Count == 0)
        {
            lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError2.Text"].ToString();
            return;
        }
        //icano 17-06-10 Validacion de Boarding Pass
        String cod_numero_bcbp = dtBoardingDetalle.Rows[0]["Cod_Numero_Bcbp"].ToString();
        String estado = dtBoardingDetalle.Rows[0]["Cod_Numero_Bcbp"].ToString();
        String tip_anulacion = dtBoardingDetalle.Rows[0]["Tip_Anulacion"].ToString();
        String dsc_motivo = dtBoardingDetalle.Rows[0]["dsc_motivo"].ToString();
        String Log_Fec_mod = Fecha.convertSQLToFecha(dtBoardingDetalle.Rows[0]["Log_Fecha_Mod"].ToString(), dtBoardingDetalle.Rows[0]["Log_Hora_Mod"].ToString());
        //String Log_Fec_mod = dtBoardingDetalle.Rows[0]["Log_Fec_modi"].ToString();
        String fec_vencimiento = dtBoardingDetalle.Rows[0]["Fch_Vencimiento"].ToString();
        String Di_Vncidos_Log_fec = dtBoardingDetalle.Rows[0]["Di_Vncidos_Log_fec"].ToString();

        if (dsc_motivo == "")
            dsc_motivo = "MOTIVO NO REGISTRADO";

        if (estado.Trim().ToUpper().Equals("X") & tip_anulacion == "1")
        {
            lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta anulado por " + dsc_motivo + " Fecha Anulacion: " + Log_Fec_mod.Substring(0, 10); return;
        }
        else if (estado.Trim().ToUpper().Equals("X") & tip_anulacion == "3")
        {
            lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta vencido. Fecha Vencimiento: " + fec_vencimiento.Substring(0,10) + " - Dias vencidos: " + Di_Vncidos_Log_fec; return;
        }
        else if (estado.Trim().ToUpper().Equals("R"))
        {
            lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta REHABILITADO. Fecha Rehabilitacion: " + Log_Fec_mod.Substring(0,10); return;
        }
        //fin


        
        String Num_Secuencial_Bcbp = dtBoardingDetalle.Rows[0]["Num_Secuencial_Bcbp"].ToString();

        DataTable dtBCBPRehabilitados;
        DataRow row;
        if (Session["tabla"] != null)
        {
            dtBCBPRehabilitados = (DataTable)Session["tabla"];

            #region Verifica si ya se ingreso
            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                if (Num_Secuencial_Bcbp.ToUpper().Trim().Equals(dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString().ToUpper().Trim()))
                {
                    lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError3.Text"].ToString();
                    return;
                }
            }
            #endregion

            #region Guardo las selecciones del combo y checkbox
            pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                HiddenField hDescripXpistola = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPistola");
                HiddenField hDescripXpoppup = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPopPup");
                string sTramaAsociada = string.Empty;
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;            
                
            }
            #endregion

            row = dtBCBPRehabilitados.NewRow();
            row["Numero"] = dtBCBPRehabilitados.Rows.Count + 1;
        }
        else
        {
            dtBCBPRehabilitados = new DataTable();

            dtBCBPRehabilitados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.String"));

            dtBCBPRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));

            dtBCBPRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Check", System.Type.GetType("System.Int32"));
            dtBCBPRehabilitados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            //Cmontes 18-06-2010 
            dtBCBPRehabilitados.Columns.Add("Dsc_Compania_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Nro_Vuelo_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Fch_Vuelo_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Nro_Asiento_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("Pasajero_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("TramaPoppup_Asoc", System.Type.GetType("System.String"));
            dtBCBPRehabilitados.Columns.Add("TramaPistola_Asoc", System.Type.GetType("System.String"));

            //icano 14-06-10 agregando una columna para guardar el nuevo campo de boarding pass
            dtBCBPRehabilitados.Columns.Add("Cod_Numero_Bcbp", System.Type.GetType("System.String"));
            //fin
            row = dtBCBPRehabilitados.NewRow();
            row["Numero"] = "1";
        }

        row["Num_Secuencial_Bcbp"] = Num_Secuencial_Bcbp;

        row["Num_Vuelo"] = dtBoardingDetalle.Rows[0]["Num_Vuelo"];
        row["Fch_Vuelo"] = dtBoardingDetalle.Rows[0]["Fch_Vuelo"];
        row["Num_Asiento"] = dtBoardingDetalle.Rows[0]["Num_Asiento"];
        row["Nom_Pasajero"] = dtBoardingDetalle.Rows[0]["Nom_Pasajero"];

        row["Dsc_Compania"] = dtBoardingDetalle.Rows[0]["Dsc_Compania"];
        row["Cod_Compania"] = dtBoardingDetalle.Rows[0]["Cod_Compania"];

        row["Observacion"] = dtBoardingDetalle.Rows[0]["Observacion"];
        row["Check"] = false;   //false=0
        //icano 14-06-10 asignando Cod_Numero_Bcbp
        row["Cod_Numero_Bcbp"] = dtBoardingDetalle.Rows[0]["Cod_Numero_Bcbp"];
        //fin
        dtBCBPRehabilitados.Rows.Add(row);
        dtBCBPRehabilitados.AcceptChanges();
        Session["tabla"] = dtBCBPRehabilitados;

        #region Define el PageIndex
        if (dtBCBPRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
        }
        #endregion

        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        #region Lleno el combo y actualizo la seleccion del combo.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        for (int i = (pageIndex * pageSize), j = 0; j < dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            if (i < dtBCBPRehabilitados.Rows.Count - 1)
            {
                cboMotivo.ClearSelection();
                cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
                //Comentado pues se hace en el aspx para lo que es el checkbox
                //CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                //chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar
            }

            if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
            {
                gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
            }
            
        }
        #endregion

        #region Actualizar Resumen
        lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) + 1);
        #endregion

        txtAsiento.Text = "";
        txtPersona.Text = "";
    }

    #endregion

    #region btnAgregar_Click2

    protected void btnAgregar_Click2(object sender, EventArgs e)
    {
        try
        {

            int pageIndex;
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageCount = gwvRehabilitarPorBCBP.PageCount;

            String compania;
            String fechaVuelo = string.Empty;
            String nroVuelo;
            String asiento = string.Empty;
            String persona;
            String numCheckNumb = null;
            String Flag_Fch_Vuelo;

            htLabels = LabelConfig.htLabels;

            #region Obtiene los campos que identifican unicamente a un solo boarding.
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
                //eochoa
                strTrama = HttpUtility.UrlDecode(strTrama, System.Text.Encoding.ASCII);

                Reader reader = new Reader();
                Hashtable ht = reader.ParseString_Boarding(strTrama);
               
                if (ht == null)
                {
                    lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                    return;
                }
                string strFormatCode = (string)ht["format_code"];
                if (strFormatCode.Equals("M"))
                {
                    ArrayList arrLst = (ArrayList)ht["flight_information"];
                    reader.AnalizarSegmentosBCBP(arrLst, (string)Property.htProperty["FROM_CITY_AIRPORT_CODE"]);
                    Hashtable htAux = (Hashtable)arrLst[0];
                    ht.Add(LAP.TUUA.CONVERSOR.Define.Compania, (String)htAux[LAP.TUUA.CONVERSOR.Define.Compania]);
                    ht.Add(LAP.TUUA.CONVERSOR.Define.NroVuelo, (String)htAux[LAP.TUUA.CONVERSOR.Define.NroVuelo]);
                    ht.Add(LAP.TUUA.CONVERSOR.Define.Asiento, (String)htAux[LAP.TUUA.CONVERSOR.Define.Asiento]);
                    ht.Add(LAP.TUUA.CONVERSOR.Define.FechaVuelo, (String)htAux[LAP.TUUA.CONVERSOR.Define.FechaVuelo]);
                    ht.Add(LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER, htAux[LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER]);
                }

                if (ht[LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER] == null ||
                   ((String)ht[LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER]).Trim() == String.Empty)
                {
                    ht.Add(LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER, LAP.TUUA.CONVERSOR.Define.DEFAULT_CHECKIN_SEQUENCE_NUMBER);
                }

                reader.ParseHashtable(ht);

                String companiaAux = (String)ht[LAP.TUUA.CONVERSOR.Define.Compania];
                String fechaVueloAux = (String)ht[LAP.TUUA.CONVERSOR.Define.FechaVuelo];

                nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
                asiento = (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento];
                persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];
                numCheckNumb = (String)ht[LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER];

                if (String.IsNullOrEmpty(companiaAux) || String.IsNullOrEmpty(fechaVueloAux) ||
                    String.IsNullOrEmpty(nroVuelo) || String.IsNullOrEmpty(asiento) || String.IsNullOrEmpty(persona))
                {
                    lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                    return;
                }

                //-------- EAG 21/01/2010

                persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];
                nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
                numCheckNumb = (String)ht[LAP.TUUA.CONVERSOR.Define.CHECKIN_SEQUENCE_NUMBER];
                //-------- EAG 21/01/2010

                DataTable dtCompania = objBORehabilitacion.listarCompania_xCodigoEspecial(companiaAux);
                try
                {
                    compania = dtCompania.Rows[0]["Cod_Compania"].ToString();
                    fechaVuelo = Function.ConvertirJulianoCalendario(Int32.Parse(fechaVueloAux));
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                    return;
                }
                Flag_Fch_Vuelo = "0";
            }
            else
            {
                compania = cboCompanias.SelectedItem.Value;
                String fechaVueloAux = txtFechaVuelo.Text;
                if (txtFechaVuelo.Text.Trim() != string.Empty)
                {
                    fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);
                }
                //nroVuelo = cboVuelo.SelectedValue;
                nroVuelo = txtNroVuelo.Text;
                if (txtAsiento.Text.Trim() != string.Empty)
                {
                    asiento = txtAsiento.Text.PadLeft(4, '0');  //EAG 21/01/2010
                }
                persona = txtPersona.Text;
                Flag_Fch_Vuelo = "1";
                numCheckNumb = null;
            }
            #endregion

            DataTable dtBoardingDetalle = objBOConsultas.DetalleBoarding_REH(compania, nroVuelo, fechaVuelo, asiento, persona, null, null, null, Flag_Fch_Vuelo, numCheckNumb);

            if (dtBoardingDetalle.Rows.Count == 0)
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError2.Text"].ToString();
                return;
            }

            //Validar fecha de vigencia
            var query = from dtBcbp in dtBoardingDetalle.AsEnumerable()
                        where (dtBcbp.Field<DateTime>("Fch_Vencimiento1") >= dtBcbp.Field<DateTime>("Fch_Now"))
                        select dtBcbp;

            //Create a table from the query.
            if (query.Count() > 0)
            {
                //DataTable boundTable = query.CopyToDataTable();
                dtBoardingDetalle = null;
                dtBoardingDetalle = query.CopyToDataTable();
            }
            else
            {
                dtBoardingDetalle = new DataTable();
                lblErrorMsg.Text = "El bcbp se encuentra vencido.";
                return;
            }

            //icano 17-06-10 Validacion de Boarding Pass
            if (dtBoardingDetalle.Rows.Count == 1)
            {
                String cod_numero_bcbp = dtBoardingDetalle.Rows[0]["Cod_Numero_Bcbp"].ToString();
                String estado = dtBoardingDetalle.Rows[0]["Tip_Estado"].ToString();
                String tip_anulacion = dtBoardingDetalle.Rows[0]["Tip_Anulacion"].ToString();
                String dsc_motivo = dtBoardingDetalle.Rows[0]["dsc_motivo"].ToString();
                String Log_Fec_mod =  Fecha.convertSQLToFecha(dtBoardingDetalle.Rows[0]["Log_Fecha_Mod"].ToString(), dtBoardingDetalle.Rows[0]["Log_Hora_Mod"].ToString());
                //String Log_Fec_mod = dtBoardingDetalle.Rows[0]["Log_Fec_modi"].ToString();
                String fec_vencimiento = dtBoardingDetalle.Rows[0]["Fch_Vencimiento"].ToString();
                String Di_Vncidos_Log_fec = dtBoardingDetalle.Rows[0]["Di_Vncidos_Log_fec"].ToString();

                if (dsc_motivo == "")
                    dsc_motivo = "MOTIVO NO REGISTRADO";

                if (estado.Trim().ToUpper().Equals("X") & tip_anulacion == "1")
                {
                    lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta anulado por " + dsc_motivo + " Fecha Anulacion: " + Log_Fec_mod.Substring(0, 10); return;
                }
                else if (estado.Trim().ToUpper().Equals("X") & tip_anulacion == "3")
                {
                    lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta vencido. Fecha Vencimiento: " + fec_vencimiento.Substring(0, 10) + " - Dias vencidos: " + Di_Vncidos_Log_fec; return;
                }
                else if (estado.Trim().ToUpper().Equals("R"))
                {
                    lblErrorMsg.Text = "El Boarding Pass " + cod_numero_bcbp + " esta rehabilitado. Fecha Rehabilitacion: " + Log_Fec_mod.Substring(0, 10); return;
                }
            }
            //fin
            else
            {
                //Filtramos los bcbp
                var query2 = from dtBcbp in dtBoardingDetalle.AsEnumerable()
                             where (dtBcbp.Field<String>("Tip_Estado") != "X" && dtBcbp.Field<String>("Tip_Estado") != "R")
                             select dtBcbp;
                //Create a table from the query.
                if (query2.Count() > 0)
                {
                    dtBoardingDetalle = null;
                    dtBoardingDetalle = query2.CopyToDataTable();
                }
                else
                {
                    dtBoardingDetalle = new DataTable();
                    lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError2.Text"].ToString();
                    return;
                }
            }

            DataTable dtBCBPRehabilitados;

            if (Session["tabla"] != null)
            {

                dtBCBPRehabilitados = (DataTable)Session["tabla"];

                #region Guardo las selecciones del combo y checkbox
                pageIndex = gwvRehabilitarPorBCBP.PageIndex;
                int limite;
                if ((pageIndex + 1) < pageCount)
                {
                    limite = pageSize;
                }
                else
                {
                    limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
                }

                for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                {
                    DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                    HiddenField hDescripXpistola = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPistola");
                    HiddenField hDescripXpoppup = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPopPup");
                    string sTramaAsociada = string.Empty;
                    dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                    dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                    dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;

                }
                #endregion
            }
            else
            {
                dtBCBPRehabilitados = new DataTable();

                dtBCBPRehabilitados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Check", System.Type.GetType("System.Int32"));
                dtBCBPRehabilitados.Columns.Add("Motivo", System.Type.GetType("System.String"));
                //Cmontes 18-06-2010 
                dtBCBPRehabilitados.Columns.Add("Dsc_Compania_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Nro_Vuelo_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Fch_Vuelo_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Nro_Asiento_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Pasajero_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("TramaPoppup_Asoc", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("TramaPistola_Asoc", System.Type.GetType("System.String"));
                //icano 14-06-10 agregando una columna para guardar el nuevo campo de boarding pass
                dtBCBPRehabilitados.Columns.Add("Cod_Numero_Bcbp", System.Type.GetType("System.String"));
                //fin
                //jcisneros
                dtBCBPRehabilitados.Columns.Add("Bloquear", System.Type.GetType("System.Int32"));
                //cmontes
                dtBCBPRehabilitados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Des_Tipo_Vuelo", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Des_Motivo", System.Type.GetType("System.String"));
                dtBCBPRehabilitados.Columns.Add("Fec_Uso", System.Type.GetType("System.String"));

                Session["tabla"] = dtBCBPRehabilitados;

            }

            int iIni = 0;

            while (iIni < dtBoardingDetalle.Rows.Count)
            {

                bool isNRegistrado = true;
                String Num_Secuencial_Bcbp = dtBoardingDetalle.Rows[iIni]["Num_Secuencial_Bcbp"].ToString();
                DataRow row;

                #region Verifica si ya se ingreso
                for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
                {
                    if (Num_Secuencial_Bcbp.ToUpper().Trim().Equals(dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString().ToUpper().Trim()))
                    {
                        lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError3.Text"].ToString();
                        isNRegistrado = false;
                    }
                }
                #endregion


                if (isNRegistrado)
                {
                    row = dtBCBPRehabilitados.NewRow();
                    row["Numero"] = dtBCBPRehabilitados.Rows.Count + 1;

                    row["Num_Secuencial_Bcbp"] = dtBoardingDetalle.Rows[iIni]["Num_Secuencial_Bcbp"].ToString();

                    row["Num_Vuelo"] = dtBoardingDetalle.Rows[iIni]["Num_Vuelo"];
                    row["Fch_Vuelo"] = dtBoardingDetalle.Rows[iIni]["Fch_Vuelo"];
                    row["Num_Asiento"] = dtBoardingDetalle.Rows[iIni]["Num_Asiento"];
                    row["Nom_Pasajero"] = dtBoardingDetalle.Rows[iIni]["Nom_Pasajero"];

                    row["Dsc_Compania"] = dtBoardingDetalle.Rows[iIni]["Dsc_Compania"];
                    row["Cod_Compania"] = dtBoardingDetalle.Rows[iIni]["Cod_Compania"];

                    row["Observacion"] = dtBoardingDetalle.Rows[iIni]["Observacion"];
                    row["Check"] = false;   //false=0
                    //icano 14-06-10 asignando Cod_Numero_Bcbp
                    row["Cod_Numero_Bcbp"] = dtBoardingDetalle.Rows[iIni]["Cod_Numero_Bcbp"];
                    //fin
                    row["Bloquear"] = false;

                    //cmontes
                    row["Tipo_Vuelo"] = dtBoardingDetalle.Rows[iIni]["Tip_Vuelo"];
                    row["Des_Tipo_Vuelo"] = dtBoardingDetalle.Rows[iIni]["Des_Tip_Vuelo"];
                    row["Fec_Uso"] = dtBoardingDetalle.Rows[iIni]["Log_Fecha_Mod"].ToString() +
                                     dtBoardingDetalle.Rows[iIni]["Log_Hora_Mod"].ToString();

                    dtBCBPRehabilitados.Rows.Add(row);


                }
                iIni++;
            }

            dtBCBPRehabilitados.AcceptChanges();
            Session["tabla"] = dtBCBPRehabilitados;


            #region Define el PageIndex
            if (dtBCBPRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
            {
                gwvRehabilitarPorBCBP.PageIndex = pageCount;
            }
            else
            {
                gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
            }
            #endregion

            gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
            gwvRehabilitarPorBCBP.DataBind();

            #region Lleno el combo y actualizo la seleccion del combo.
            DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

            pageIndex = gwvRehabilitarPorBCBP.PageIndex;

            for (int i = (pageIndex * pageSize), j = 0; j < dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                cboMotivo.DataSource = dtCausalReh;
                cboMotivo.DataTextField = "Dsc_Campo";
                cboMotivo.DataValueField = "Cod_Campo";
                cboMotivo.DataBind();
                if (i < dtBCBPRehabilitados.Rows.Count - 1)
                {
                    cboMotivo.ClearSelection();

                    if (dtBCBPRehabilitados.Rows[i]["Motivo"] != null && dtBCBPRehabilitados.Rows[i]["Motivo"].ToString() != string.Empty)
                    {
                        cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
                    }

                }

                if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                    dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
                {
                    gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
                }

            }
            #endregion

            #region Actualizar Resumen
            //lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) + 1);
            lblTxtIngresados.Text = "" + dtBCBPRehabilitados.Rows.Count;
            #endregion

            txtAsiento.Text = "";
            txtPersona.Text = "";

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    #endregion

    #region AddingEmptyRow
    private void AddingEmptyRow()
    {
        DataTable dtBCBPRehabilitados = new DataTable();
        dtBCBPRehabilitados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Dsc_Compania_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Nro_Vuelo_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Fch_Vuelo_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Nro_Asiento_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Pasajero_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("TramaPoppup_Asoc", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("TramaPistola_Asoc", System.Type.GetType("System.String"));
        

        dtBCBPRehabilitados.Columns.Add("Check", System.Type.GetType("System.Int32"));
        //jcisneros
        dtBCBPRehabilitados.Columns.Add("Bloquear", System.Type.GetType("System.Int32"));
        //No es necesario poner para "Motivo"

        //Para evitar poner: Eval("Check")!=DBNull.Value &&
        DataRow row = dtBCBPRehabilitados.NewRow();
        row["Check"] = 0;
        //jcisneros
        row["Bloquear"] = 0;

        dtBCBPRehabilitados.Rows.Add(row);
        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        gwvRehabilitarPorBCBP.Rows[0].FindControl("descBCBP").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("chkBloquear").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("cboMotivo").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("btnAdicionarManual").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("btnAdicionarPistola").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminarAsociado").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("tblDatosAsociados").Visible = false;

        //
        lblTxtIngresados.Text = "0";
        lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
        hdNumSelConObs.Value = "0";
        hdNumSelTotal.Value = "0";
    }
    #endregion

    #region cboCompanias_SelectedIndexChanged
    protected void cboCompanias_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Comentado pues se va agregando a la grilla, no es una busqueda como un rango.
        //#region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
        //AddingEmptyRow();
        //Session["tabla"] = null;//Elimino la tabla real.
        //#endregion
        #endregion

        //Borro la fecha ingresada
        //txtFechaVuelo.Text = "";

        #region Vaciando el combo de vuelos
        cboVuelo.Items.Clear();
        cboVuelo.Items.Insert(0, "Seleccionar");
        #endregion
        
        //txtAsiento.Text = "";
        //txtPersona.Text = "";

    }
    #endregion

    #region txtFecha_Vuelo_TextChanged
    protected void txtFechaVuelo_TextChanged(object sender, EventArgs e)
    {
		/*
        if (cboCompanias.SelectedIndex > 0 && txtFechaVuelo.Text.Length > 0)
        {
            String sCompania = cboCompanias.SelectedItem.Value;
            String fechaVueloAux = txtFechaVuelo.Text;
            String fechaVuelo = fechaVueloAux.Substring(6, 4) + fechaVueloAux.Substring(3, 2) + fechaVueloAux.Substring(0, 2);

            DataTable dtVuelos = objBORehabilitacion.consultarVuelosBCBPPorCiaFecha(sCompania, fechaVuelo);
            cboVuelo.DataSource = dtVuelos;
            cboVuelo.DataTextField = "Num_Vuelo";
            //cboVuelo.DataValueField = "";
            cboVuelo.DataBind();

            cboVuelo.Items.Insert(0, "Seleccionar");

            #region Comentado pues se va agregando a la grilla, no es una busqueda como un rango.
            //#region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
            //AddingEmptyRow();
            //Session["tabla"] = null;//Elimino la tabla real.
            //#endregion
            #endregion

            //txtAsiento.Text = "";
            //txtPersona.Text = "";
        }
		*/
    }
    #endregion

    #region cboVuelo_OnSelectedIndexChanged

    protected void cboVuelo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboCompanias.SelectedIndex > 0 && txtFechaVuelo.Text.Length > 0 /*&& cboVuelo.SelectedIndex > 0*/)
        {
            #region Comentado pues se va agregando a la grilla, no es una busqueda como un rango.
            //#region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
            //AddingEmptyRow();
            //Session["tabla"] = null;//Elimino la tabla real.
            //#endregion
            #endregion

            txtAsiento.Text = "";
            txtPersona.Text = "";
        }
    }
    #endregion

    #region gwvRehabilitarPorBCBP_RowDataBound
    protected void gwvRehabilitarPorBCBP_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region gwvRehabilitarPorBCBP_RowCommand
    protected void gwvRehabilitarPorBCBP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];
            
            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageCount = gwvRehabilitarPorBCBP.PageCount;

            #region Actualizar Resumen
            Boolean isChecked = ((CheckBox)(gwvRehabilitarPorBCBP.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
            String observaciones = dtBCBPRehabilitados.Rows[rowIndex]["Observacion"].ToString();
            #endregion
            dtBCBPRehabilitados.Rows.RemoveAt(rowIndex);
            //dtBCBPRehabilitados.AcceptChanges();

            #region Guardo las selecciones del combo y checkbox
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count + 1 - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
            }
            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                if (j != rowIndex - (pageIndex * pageSize))
                {
                    DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                    CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
                    dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                    dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                    dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                    dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
                    //dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
                }
                else
                {
                    i--;
                }
            }
            #endregion
            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                dtBCBPRehabilitados.Rows[i]["Numero"] = i + 1;
            }

            #region Define el PageIndex
            if (dtBCBPRehabilitados.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
            {
                gwvRehabilitarPorBCBP.PageIndex = pageIndex - 1;
            }
            #endregion


            if (dtBCBPRehabilitados.Rows.Count == 0)
            {
                #region Agregando fila vacia a la grilla por default
                //Para evitar poner: Eval("Check")!=DBNull.Value &&
                DataRow rowTemp = dtBCBPRehabilitados.NewRow();
                rowTemp["Check"] = 0;
                rowTemp["Bloquear"] = 0;

                dtBCBPRehabilitados.Rows.Add(rowTemp);

                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                gwvRehabilitarPorBCBP.Rows[0].FindControl("descBCBP").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkBloquear").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("cboMotivo").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnAdicionarManual").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnAdicionarPistola").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminarAsociado").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("tblDatosAsociados").Visible = false;


                lblTxtIngresados.Text = "0";
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";

                #endregion

                Session["tabla"] = null;
            }
            else
            {
                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                #region Lleno el combo y actualizo la seleccion del combo.
                DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

                pageIndex = gwvRehabilitarPorBCBP.PageIndex;
                pageCount = gwvRehabilitarPorBCBP.PageCount;

                if ((pageIndex + 1) < pageCount)
                {
                    limite = pageSize;
                }
                else
                {
                    limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
                }

                for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                {
                    DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                    cboMotivo.DataSource = dtCausalReh;
                    cboMotivo.DataTextField = "Dsc_Campo";
                    cboMotivo.DataValueField = "Cod_Campo";
                    cboMotivo.DataBind();

                    cboMotivo.ClearSelection();
                    cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;

                    //Comentado pues se hace en el aspx para lo que es el checkbox
                    //CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                    //chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar

                    if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
                    {
                        gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
                    }



                }
                #endregion

                #region Actualizar Resumen
                if (isChecked)
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

        }
        else if (e.CommandName.Equals("ShowBCBP"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];
            String Num_Secuencial_Bcbp = dtBCBPRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            CnsDetBoarding1.CargarDetalleBoarding(Num_Secuencial_Bcbp);
        }
        else if (e.CommandName.Equals("btnAdicionarManual"))
        {
            //GuardoDescripcionAsociacion();
            string sID = e.CommandArgument.ToString();
            IngBoarding1.CargarFormulario(sID);
            
        }
    }
    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorBCBP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;

        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        #region Guardo las selecciones del checkbox y comboBox
        pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
            dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
            dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
            dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            //jcisneros
            dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
            //dtBCBPRehabilitados.Rows[i]["Asociado_Bcbp"] = chkSeleccionar.Checked;
            //Asociado_Bcbp
        }
        #endregion

        gwvRehabilitarPorBCBP.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            cboMotivo.ClearSelection();
            cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;

            //Comentado pues se hace en el aspx para lo que es el checkbox
            //CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            //chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar
        }
        #endregion


    }

    #endregion

    #region lnkRepresentante_Click
    protected void lnkRepresentante_Click(object sender, EventArgs e)
    {
        consRepre.Inicio();
    }
    #endregion

    #region gwvRehabilitarPorBCBP_Sorting
    protected void gwvRehabilitarPorBCBP_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];
        if (dtBCBPRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox y comboBox
        int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
            dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
            dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
            dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
        }
        #endregion

        //No es necesario...creo.
        //gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        Session["tabla"] = SortDataTable(dtBCBPRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtBCBPRehabilitados = (DataTable)Session["tabla"];

        gwvRehabilitarPorBCBP.DataSource = (DataTable)Session["tabla"];

        gwvRehabilitarPorBCBP.DataBind();

        #region Lleno el combo. Actualizo la selecciones del checkBox y combobox.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();

            //Aqui no hay condicion pues no he agregado ninguna fila.
            cboMotivo.ClearSelection();
            cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;

            //Comentado pues se hace en el aspx para lo que es el checkbox
            //CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            //chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar


            if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
            {
                gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
            }


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
        if (Session["tabla"] == null)
        {
            //No hay filas en la grilla
        }
        else
        {
            htLabels = LabelConfig.htLabels;

            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;
            }

            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

            #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }

            Session["tabla"] = dtBCBPRehabilitados;
            #endregion

            int nroSeleccionados = dtBCBPRehabilitados.Select("Check=1").Count();
            //int nroSeleccionados2 = Int32.Parse((String)Request.Form["hdNumSelTotal"]);
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }

            

            StringBuilder listaBCBPs = new StringBuilder();
            StringBuilder causal_Rehabilitaciones = new StringBuilder();

            int iNumBoarding = 0;

            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Check"].ToString()) == 1 ? true : false;
                if (checkedRehabilitar)
                {
                    iNumBoarding++;

                    String Num_Secuencial_Bcbp = dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString();
                    listaBCBPs.Append(Num_Secuencial_Bcbp + "|");

                    String motivo = dtBCBPRehabilitados.Rows[i]["Motivo"].ToString();
                    causal_Rehabilitaciones.Append(motivo + "|");

                }
            }

            BoardingBcbpEstHist boardingBcbpEstHist = new BoardingBcbpEstHist();
            boardingBcbpEstHist.SListaBcbPs = listaBCBPs.ToString();
            boardingBcbpEstHist.SCausalRehabilitacion = causal_Rehabilitaciones.ToString();
            boardingBcbpEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);

            objBORehabilitacion = new BO_Rehabilitacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            bool ret = objBORehabilitacion.registrarRehabilitacionBCBP(boardingBcbpEstHist, 0, listaBCBPs.Length + iNumBoarding * 2);
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
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000044", "005", IpClient, "1", "Alerta W0000044", "Proceso de Rehabilitacion de Boarding-Normal, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

            }

            int iTotalNoRehab = 0;

            //Quiere decir que hubieron tickets que estaban en estado != 'U'
            if (boardingBcbpEstHist.SListaBcbPs.Length > 0)
            {
                String bcbpsNoRehabilitados = boardingBcbpEstHist.SListaBcbPs.Substring(0, boardingBcbpEstHist.SListaBcbPs.Length - 1);
                String[] bcbpsNoReh = bcbpsNoRehabilitados.Split(new char[] { '|' });
                for (int i = 0; i < bcbpsNoReh.Length; i++)
                {
                    string strData = bcbpsNoReh[i];
                    string[] strDataBCBP = strData.Split(new char[] { ',' });
                    foreach (DataRow row in dtBCBPRehabilitados.Rows)
                    {
                        if (strDataBCBP[0].Equals(row["Num_Secuencial_Bcbp"].ToString()))
                        {
                            string strObservacion = "";

                            switch (strDataBCBP[1])
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

            //dtBCBPRehabilitados Query consulta

            //var query = from dtBcbp in dtBCBPRehabilitados.AsEnumerable()
            //            where (dtBcbp.Field<String>("Check") == "0")
            //            select dtBcbp;

            



            //See summary
            this.lblTotalRehab.Text = (iNumBoarding - iTotalNoRehab) + "";
            //this.lblTotalNoRehab.Text = iTotalNoRehab + "";
            int iTotal = dtBCBPRehabilitados.Rows.Count;
            this.lblTotalNoRehab.Text = (iTotal - (iNumBoarding - iTotalNoRehab)) + "";

            System.Threading.Thread.Sleep(500);

            
        }
    }
    #endregion


    /**
     * Metodo agregado para el proceso de rehabilitacion con los nuevas
     * especificaciones, para la asociacion de BCBP
     */
    #region btnRehabilitarExpancion_Click
    protected void btnRehabilitarExpancion_Click(object sender, EventArgs e)
    {
        if (!(Session["tabla"] == null))
        {

            htLabels = LabelConfig.htLabels;

            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;
            }

            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

            #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
            }
            #endregion

            Session["tabla"] = dtBCBPRehabilitados;

            int nroSeleccionados = dtBCBPRehabilitados.Select("Check=1").Count();
            //int nroSeleccionados2 = Int32.Parse((String)Request.Form["hdNumSelTotal"]);
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }



            StringBuilder listaBCBPs = new StringBuilder();
            StringBuilder causal_Rehabilitaciones = new StringBuilder();
            StringBuilder estado_asoc = new StringBuilder();
            StringBuilder compania_asoc = new StringBuilder();
            StringBuilder fechavuelo_asoc = new StringBuilder();
            StringBuilder nrovuelo_asoc = new StringBuilder();
            StringBuilder asiento_asoc = new StringBuilder();
            StringBuilder pasajero_asoc = new StringBuilder();
            StringBuilder strLstBloqueados = new StringBuilder();
            bool checkedBloquear = false;
            int iNumBoarding = 0;

            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Check"].ToString()) == 1 ? true : false;
                if (checkedRehabilitar)
                {
                    iNumBoarding++;
                    checkedBloquear = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Bloquear"].ToString()) == 1 ? true : false;
                    strLstBloqueados.Append(checkedBloquear ? "1|" : "0|");
                    String Num_Secuencial_Bcbp = dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString();
                    listaBCBPs.Append(Num_Secuencial_Bcbp + "|");

                    String motivo = dtBCBPRehabilitados.Rows[i]["Motivo"].ToString();
                    causal_Rehabilitaciones.Append(motivo + "|");

                    String sEstadoAsoc = string.Empty;
                    if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() != string.Empty ||
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() != string.Empty)
                    {
                        sEstadoAsoc = "1";
                    }
                    else
                    {
                        sEstadoAsoc = "0";
                    }
                    estado_asoc.Append(sEstadoAsoc + "|");

                    String sCompaniaAsoc = dtBCBPRehabilitados.Rows[i]["Dsc_Compania_Asoc"].ToString();
                    compania_asoc.Append(sCompaniaAsoc + "|");

                    String sFechaVueloAsoc = dtBCBPRehabilitados.Rows[i]["Fch_Vuelo_Asoc"].ToString();
                    fechavuelo_asoc.Append(sFechaVueloAsoc + "|");

                    String sNroVueloAsoc = dtBCBPRehabilitados.Rows[i]["Nro_Vuelo_Asoc"].ToString();
                    nrovuelo_asoc.Append(sNroVueloAsoc + "|");

                    String sNroAsientoAsoc = dtBCBPRehabilitados.Rows[i]["Nro_Asiento_Asoc"].ToString();
                    asiento_asoc.Append(sNroAsientoAsoc + "|");

                    String sPasajeroAsoc = dtBCBPRehabilitados.Rows[i]["Pasajero_Asoc"].ToString();
                    pasajero_asoc.Append(sPasajeroAsoc + "|");
                }
            }

            BoardingBcbpEstHist boardingBcbpEstHist = new BoardingBcbpEstHist();
            boardingBcbpEstHist.SListaBcbPs = listaBCBPs.ToString();
            boardingBcbpEstHist.SCausalRehabilitacion = causal_Rehabilitaciones.ToString();
            boardingBcbpEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);

            boardingBcbpEstHist.SEstadoAsoc = estado_asoc.ToString();
            boardingBcbpEstHist.SCompaniaAsoc = compania_asoc.ToString();
            boardingBcbpEstHist.SFechaVueloAsoc = fechavuelo_asoc.ToString();
            boardingBcbpEstHist.SNroVueloAsoc = nrovuelo_asoc.ToString();
            boardingBcbpEstHist.SNroAsientoAsoc = asiento_asoc.ToString();
            boardingBcbpEstHist.SPasajeroAsoc = pasajero_asoc.ToString();
            boardingBcbpEstHist.Lst_Bloqueados = strLstBloqueados.ToString();

            objBORehabilitacion = new BO_Rehabilitacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            bool ret = objBORehabilitacion.registrarRehabilitacionBCBPAmpliacion(boardingBcbpEstHist, 0, listaBCBPs.Length + iNumBoarding * 2);
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
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000044", "005", IpClient, "1", "Alerta W0000044", "Proceso de Rehabilitacion de Boarding-Normal, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

            Session["BcbpResultado"] = boardingBcbpEstHist;

            int iTotalNoRehab = 0;

            //Quiere decir que hubieron tickets que estaban en estado != 'U'
            if (boardingBcbpEstHist.SListaBcbPs.Length > 0)
            {
                String bcbpsNoRehabilitados = boardingBcbpEstHist.SListaBcbPs.Substring(0, boardingBcbpEstHist.SListaBcbPs.Length - 1);
                String[] bcbpsNoReh = bcbpsNoRehabilitados.Split(new char[] { '|' });
                for (int i = 0; i < bcbpsNoReh.Length; i++)
                {
                    string strData = bcbpsNoReh[i];
                    string[] strDataBCBP = strData.Split(new char[] { ',' });
                    foreach (DataRow row in dtBCBPRehabilitados.Rows)
                    {
                        if (strDataBCBP[0].Equals(row["Num_Secuencial_Bcbp"].ToString()))
                        {
                            string strObservacion = "";

                            if (strDataBCBP.Length == 1)
                            {
                                strObservacion = htLabels["rehabilitacionticket.lblMensajeError3.Text"].ToString();
                            }
                            else
                            {
                                switch (strDataBCBP[1])
                                {
                                    case "U": strObservacion = htLabels["rehabilitacionticket.lblMensajeError2.Text"].ToString(); break;
                                    case "R": strObservacion = htLabels["rehabilitacionticket.lblMensajeError1.Text"].ToString(); break;
                                    default: strObservacion = htLabels["rehabilitacionticket.lblMensajeError3.Text"].ToString(); break;
                                }
                            }

                             if (boardingBcbpEstHist.SListaOperBcbp.Length > 0)
                            {
                                //hubo operacion de rehabilitacion
                                row["Check"] = true;
                            }
                            else {
                                row["Check"] = false;
                                row["Bloquear"] = false;
                                iTotalNoRehab++; 
                            }
                            row["Observacion"] = strObservacion;

                            break;
                        }
                    }
                }
            }

  
            pnlPrincipal.Visible = false;

            pnlConformidad.Visible = true;
            tableRehabilitar.Visible = false;

            //Actualizamos sumario
            this.lblTotalRehab.Text = (iNumBoarding - iTotalNoRehab) + "";
            //this.lblTotalNoRehab.Text = iTotalNoRehab + "";
            int iTotal = dtBCBPRehabilitados.Rows.Count;
            this.lblTotalNoRehab.Text = (iTotal - (iNumBoarding - iTotalNoRehab)) + "";

            System.Threading.Thread.Sleep(500);
            int iTotalProcesados = iNumBoarding - iTotalNoRehab;

            String script = string.Empty;

            if (iTotalProcesados == 0)
            {
                 script = "<script language=\"javascript\">" +
                          "mostrarExcelBtn();" +
                          "</script>";
            }
            else
            {
                script = "<script language=\"javascript\">" +
                         "mostrarTodosBtn();" +
                         "</script>";
            }            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
        }
    }
    #endregion


    #region btnReporte_click
    protected void btnReporte_click(object sender, ImageClickEventArgs e)
    {
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        String url = "ReporteREH/ReporteREH_BCBP.aspx?titulo=Rehabilitacion BCBP";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtBCBPRehabilitados", dtBCBPRehabilitados);
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

    #region "btnExportarExcel"
    protected void imgExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportarDataTable((DataTable)Session["tabla"]);
    }
    #endregion

    #region "ExportarDataTable"
    public void ExportarDataTable(DataTable dt_General)
    {
        //DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];
        DataTable dtTicketRehabilitados = dt_General;
        DataTable dt = new DataTable();
        DropDownList cboMotivo;
        CheckBox chkSeleccionar;
        DataRow dr;

        Int32 flag;
        int numero_correlativo = 1;
        DateTime dt_fecha = DateTime.Now;
        string str_fecha;

        #region "Crear Tabla"
        dt.Columns.Add(new DataColumn("Boarding", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero", System.Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("Numero Boarding", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Compania", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero Vuelo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Vuelo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero Asiento", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Pasajero", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Observaciones", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Motivo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Salida", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Rehabilitacion", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Usuario Logeado", System.Type.GetType("System.String")));
        #endregion

        if (dtTicketRehabilitados.Rows.Count > 0)
        {

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {

                cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[i].FindControl("chkSeleccionar");
                flag = Convert.ToInt32(dtTicketRehabilitados.Rows[i]["Check"]);
                str_fecha = dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(6, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(4, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(0, 4);

                if (chkSeleccionar.Checked)
                {
                    if (flag == 1)
                    {
                        dr = dt.NewRow();
                        dr["Boarding"] = "Boarding Pass Rehabilitados";
                        dr["Numero"] = numero_correlativo++; //dtTicketRehabilitados.Rows[i]["Numero"].ToString();
                        dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                        dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                        dr["Fecha Vuelo"] = str_fecha;
                        dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                        dr["Pasajero"] = "- " + dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString() + " -";
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = "Boarding Pass Rehabilitado";
                        dr["Fecha Rehabilitacion"] = dt_fecha.ToString();
                        dr["Usuario Logeado"] = Session["Cta_Usuario"];
                        
                        dt.Rows.Add(dr);
                    }
                }

            }

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[i].FindControl("chkSeleccionar");
                flag = Convert.ToInt32(dtTicketRehabilitados.Rows[i]["Check"]);
                str_fecha = dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(6, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(4, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(0, 4);

                if (chkSeleccionar.Checked) //verifica los seleccionados para rehabilitar
                {
                    if (flag != 1) // pero que no han sido rehabilitados por "x" motivos
                    {
                        dr = dt.NewRow();
                        dr["Boarding"] = "Boarding Pass No Rehabilitados";
                        dr["Numero"] = numero_correlativo++;
                        dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                        dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                        dr["Fecha Vuelo"] = str_fecha;
                        dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                        dr["Pasajero"] = dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString();
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = "Boarding Pass No Rehabilitado";
                        dr["Fecha Rehabilitacion"] = "No Aplica";
                        dr["Usuario Logeado"] = "No Aplica";
                        dt.Rows.Add(dr);
                    }
                }
                if (!chkSeleccionar.Checked) //verifica los que no han sido seleccionados para rehabilitar
                {
                    dr = dt.NewRow();
                    dr["Boarding"] = "Boarding Pass No Rehabilitados";
                    dr["Numero"] = numero_correlativo++;
                    dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                    dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                    dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                    dr["Fecha Vuelo"] = str_fecha;
                    dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                    dr["Pasajero"] = dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString();
                    dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                    dr["Motivo"] = cboMotivo.SelectedItem.Text;
                    dr["Salida"] = "No seleccionado por DuttyOfficer";
                    dr["Fecha Rehabilitacion"] = "No Aplica";
                    dr["Usuario Logeado"] = "No Aplica";
                    dt.Rows.Add(dr);
                }
            }
            objBOConsultas.ExportarDataTableToExcel(dt, Response);
        }
    }
    #endregion

    protected void btnCargarDatosPistola_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int pageCount = gwvRehabilitarPorBCBP.PageCount;
        String compania;
        String fechaVuelo;
        String nroVuelo;
        String asiento;
        String persona;
        String Flag_Fch_Vuelo;
        String strTrama = string.Empty;

        //Agregado para las validaciones
        String strBaseSecuencialBCBP = string.Empty;
        String strBaseCompania = string.Empty;

        strBaseSecuencialBCBP = Request.Form["hSecuencialBCBP"].ToString();
        strBaseCompania = Request.Form["hCompaniaBCBP"].ToString();


        htLabels = LabelConfig.htLabels;

        //Obtiene los campos que identifican unicamente a un solo boarding.
        if (!String.IsNullOrEmpty(Request.Form["DataInput"]))
        {
            String script =
                "<script language=\"javascript\">" +
                "document.forms.item(0).MSCommObj.PortOpen = true;" +
                "document.forms.item(0).DataInput.value=\"\";strCad = \"\";ioPort = 0;" +
                "document.forms.item(0).hSecuencialBCBP.value=\"\";document.forms.item(0).hCompaniaBCBP.value=\"\";" +
                "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);

            strTrama = Request.Form["DataInput"].ToString();
            //eochoa
            strTrama = HttpUtility.UrlDecode(strTrama, System.Text.Encoding.ASCII);

            Reader reader = new Reader();
            Hashtable ht = reader.ParseString_Boarding(strTrama);
            if (ht == null)
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }
            reader.ParseHashtable(ht);
            String companiaAux = (String)ht[LAP.TUUA.CONVERSOR.Define.Compania];
            String fechaVueloAux = (String)ht[LAP.TUUA.CONVERSOR.Define.FechaVuelo];
            nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
            asiento = (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento];
            persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];

            if (String.IsNullOrEmpty(companiaAux) || String.IsNullOrEmpty(fechaVueloAux) ||
                String.IsNullOrEmpty(nroVuelo) || String.IsNullOrEmpty(asiento) || String.IsNullOrEmpty(persona))
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }

            persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];
            nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];


            DataTable dtCompania = objBORehabilitacion.listarCompania_xCodigoEspecial(companiaAux);
            try
            {
                compania = dtCompania.Rows[0]["Cod_Compania"].ToString();
                fechaVuelo = Function.ConvertirJulianoCalendario(Int32.Parse(fechaVueloAux));
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }
            Flag_Fch_Vuelo = "0";

            persona = persona.Trim().ToUpper();
            nroVuelo = nroVuelo.Trim().ToUpper();
            fechaVuelo = fechaVuelo.Trim();
            asiento = asiento.Trim().ToUpper();
            compania = compania.Trim().ToUpper();
            strTrama = nroVuelo + ";" + fechaVuelo + ";" + asiento + ";" + persona + ";" + compania;
        }
        else
        {
            lblErrorMsg.Text = "Error en lectura de datos.";
            return;
        }
        
        //Verificar que la Compañia del asociado sea el mismo del Base
        if (!strBaseCompania.Equals(compania))
        {
            lblErrorMsg.Text = "No se puede asociar un Boarding de otra compañia";
            return;
        }


        //Validar Bcbp asociado
        string sResultValidacion = objBOConsultas.validarAsocBCBP(asiento, nroVuelo, fechaVuelo, persona, compania, strBaseSecuencialBCBP);

        if (!sResultValidacion.Equals("0"))
        {
            if (sResultValidacion.Equals("1"))
            {
                lblErrorMsg.Text = "Este Boarding ya se encuentra registrado";
            }
            else if (sResultValidacion.Equals("2"))
            {
                lblErrorMsg.Text = "Se ha alcansado el limite de asociacion para este Bcbp base";
            }
            else if (sResultValidacion.Equals("3"))
            {
                lblErrorMsg.Text = "El numero y fecha de vuelo del Bcbp no se encuentran programados";
            }
            else if (sResultValidacion.Equals("4"))
            {
                lblErrorMsg.Text = "No se puede asociar un Boarding de otra compañia";
            }
            else
            {
                lblErrorMsg.Text = "No se puede asociar este Bcbp";
            }

            return;
        }


        DataTable dtBCBPRehabilitados = new DataTable();        
        if (Session["tabla"] != null)
        {
            dtBCBPRehabilitados = (DataTable)Session["tabla"];

            //Verifica si ya se asocio el boarding
            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                if (strTrama.Equals(dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString()) ||
                    strTrama.Equals(dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString()))
                {
                    lblErrorMsg.Text = "Este boarding ya se encuentra asociado";
                    return;
                }
            }

            //Guardo las selecciones del combo y checkbox
            pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");

                HiddenField hDescripXpistola = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPistola");
                HiddenField hDescripXpoppup = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPopPup");


                HiddenField hSeleccion = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hSeleccion");

                string sTramaAsociada = string.Empty;
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;


                if (hSeleccion.Value == "1")
                {
                    if (hDescripXpistola.Value != string.Empty)
                    {
                        //Depresiado - sTramaAsociada = hDescripXpistola.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = strTrama;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = string.Empty;
                    }
                    else if (hDescripXpoppup.Value != string.Empty)
                    {
                        //Depresiado - sTramaAsociada = hDescripXpoppup.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = strTrama;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = string.Empty;
                    }

                    if (strTrama != string.Empty)
                    {
                        //Obtener Descripcion Company
                        dtBCBPRehabilitados.Rows[i]["Dsc_Compania_Asoc"] = compania;
                        dtBCBPRehabilitados.Rows[i]["Nro_Vuelo_Asoc"] = nroVuelo;
                        //Formatear Fecha
                        dtBCBPRehabilitados.Rows[i]["Fch_Vuelo_Asoc"] = fechaVuelo;
                        dtBCBPRehabilitados.Rows[i]["Nro_Asiento_Asoc"] = asiento;
                        dtBCBPRehabilitados.Rows[i]["Pasajero_Asoc"] = persona;
                    }
                }
            }
        }

        Session["tabla"] = dtBCBPRehabilitados;                             

        //Define el PageIndex
        if (dtBCBPRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
        }


        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        //Lleno el combo y actualizo la seleccion del combo.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        for (int i = (pageIndex * pageSize), j = 0; j < dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            if (i < dtBCBPRehabilitados.Rows.Count - 1)
            {
                cboMotivo.ClearSelection();
                cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            }

            if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
            {
                gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
            }
        }
    }

    protected void btnCargarDatosPopPup_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int pageCount = gwvRehabilitarPorBCBP.PageCount;
        String compania;
        String fechaVuelo;
        String nroVuelo;
        String asiento;
        String persona;
        String Flag_Fch_Vuelo;
        String strTrama = string.Empty;

        //Agregado para las validaciones
        String strBaseSecuencialBCBP = string.Empty;
        String strBaseCompania = string.Empty;

        strBaseSecuencialBCBP = Request.Form["hSecuencialBCBP"].ToString();
        strBaseCompania = Request.Form["hCompaniaBCBP"].ToString();

        htLabels = LabelConfig.htLabels;


        if (!String.IsNullOrEmpty(Request.Form["DataInput"]))
        {
            String script =
                "<script language=\"javascript\">" +
                "document.forms.item(0).DataInput.value=\"\";strCad = \"\";hBanderaLlegadaValores = 0;" +
                "document.forms.item(0).hSecuencialBCBP.value=\"\";document.forms.item(0).hCompaniaBCBP.value=\"\";" +
                "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);

            strTrama = Request.Form["DataInput"].ToString();
            //eochoa
            strTrama = HttpUtility.UrlDecode(strTrama, System.Text.Encoding.ASCII);

            nroVuelo = strTrama.Split(';')[0];
            fechaVuelo = strTrama.Split(';')[1];
            asiento = strTrama.Split(';')[2];
            persona = strTrama.Split(';')[3];
            compania = strTrama.Split(';')[4];

            if (!(nroVuelo.Length > 2))
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }

            Reader reader = new Reader();
            Hashtable htBcbp = new Hashtable();
            htBcbp.Add(LAP.TUUA.CONVERSOR.Define.Persona, persona);
            htBcbp.Add(LAP.TUUA.CONVERSOR.Define.Asiento, asiento);
            htBcbp.Add(LAP.TUUA.CONVERSOR.Define.Compania, nroVuelo.Substring(0,2));
            htBcbp.Add(LAP.TUUA.CONVERSOR.Define.NroVuelo, nroVuelo);
            reader.ParseHashtable(htBcbp);
            asiento = (string)htBcbp[LAP.TUUA.CONVERSOR.Define.Asiento];
            persona = (string)htBcbp[LAP.TUUA.CONVERSOR.Define.Persona];
            if (String.IsNullOrEmpty(compania) || String.IsNullOrEmpty(fechaVuelo) ||
                String.IsNullOrEmpty(nroVuelo) || String.IsNullOrEmpty(persona))
            {
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
                return;
            }

            //Formatenado la Fecha
            fechaVuelo = fechaVuelo.Split('/')[2] + fechaVuelo.Split('/')[1] + fechaVuelo.Split('/')[0];


            persona = persona.Trim().ToUpper();
            nroVuelo = nroVuelo.Trim().ToUpper();
            fechaVuelo = fechaVuelo.Trim();
            asiento = asiento.Trim().ToUpper();
            compania = compania.Trim().ToUpper();
            strTrama = nroVuelo + ";" + fechaVuelo + ";" + asiento + ";" + persona + ";" + compania;

        }
        else
        {
            lblErrorMsg.Text = "Error en lectura de datos.";
            return;
        }


        //Validar Bcbp asociado
        string sResultValidacion = objBOConsultas.validarAsocBCBP(asiento, nroVuelo, fechaVuelo, persona, compania, strBaseSecuencialBCBP);

        if (!sResultValidacion.Equals("0"))
        {
            if (sResultValidacion.Equals("1"))
            {
                lblErrorMsg.Text = "Este Boarding ya se encuentra registrado";
            }
            else if (sResultValidacion.Equals("2"))
            {
                lblErrorMsg.Text = "Se ha alcansado el limite de asociacion para este Bcbp base";
            }
            else if (sResultValidacion.Equals("3"))
            {
                lblErrorMsg.Text = "El numero y fecha de vuelo del Bcbp no se encuentran programados";
            }
            else if (sResultValidacion.Equals("4"))
            {
                lblErrorMsg.Text = "No se puede asociar un Boarding de otra compañia";
            }
            else
            {
                lblErrorMsg.Text = "No se puede asociar este Bcbp";
            }

            return;
        }
        

        DataTable dtBCBPRehabilitados = new DataTable();
        if (Session["tabla"] != null)
        {
            dtBCBPRehabilitados = (DataTable)Session["tabla"];

            //Verifica si ya se asocio el boarding
            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                if (strTrama.Equals(dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString()) ||
                    strTrama.Equals(dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString()))
                {
                    lblErrorMsg.Text = "Este boarding ya se encuentra asociado";
                    return;
                }
            }

            //Guardo las selecciones del combo y checkbox
            pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                HiddenField hDescripXpistola = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPistola");
                HiddenField hDescripXpoppup = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPopPup");


                HiddenField hSeleccion = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hSeleccion");

                string sTramaAsociada = string.Empty;
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;


                if (hSeleccion.Value == "1")
                {
                    if (hDescripXpistola.Value != string.Empty)
                    {
                        //Depresiado - sTramaAsociada = hDescripXpistola.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = strTrama;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = string.Empty;
                    }
                    else if (hDescripXpoppup.Value != string.Empty)
                    {
                        //Depresiado - sTramaAsociada = hDescripXpoppup.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = strTrama;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = string.Empty;
                    }

                    if (strTrama != string.Empty)
                    {
                        //Obtener Descripcion Company
                        dtBCBPRehabilitados.Rows[i]["Dsc_Compania_Asoc"] = compania;
                        dtBCBPRehabilitados.Rows[i]["Nro_Vuelo_Asoc"] = nroVuelo;
                        //Formatear Fecha
                        dtBCBPRehabilitados.Rows[i]["Fch_Vuelo_Asoc"] = fechaVuelo;
                        dtBCBPRehabilitados.Rows[i]["Nro_Asiento_Asoc"] = asiento;
                        dtBCBPRehabilitados.Rows[i]["Pasajero_Asoc"] = persona;
                    }
                }
            }
        }

        Session["tabla"] = dtBCBPRehabilitados;

        //Define el PageIndex
        if (dtBCBPRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
        }


        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        //Lleno el combo y actualizo la seleccion del combo.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        for (int i = (pageIndex * pageSize), j = 0; j < dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            if (i < dtBCBPRehabilitados.Rows.Count - 1)
            {
                cboMotivo.ClearSelection();
                cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            }

            if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
            {
                gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
            }
        }
    }

    /**
     * Encargado de la desagregacion de un boarding
     * Asociado     
     */
    protected void btnEliminarDatosAsociadoBCBP_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int pageCount = gwvRehabilitarPorBCBP.PageCount;
        String strTrama = string.Empty;

        htLabels = LabelConfig.htLabels;

        DataTable dtBCBPRehabilitados = new DataTable();
        if (Session["tabla"] != null)
        {
            dtBCBPRehabilitados = (DataTable)Session["tabla"];

            //Guardo las selecciones del combo y checkbox
            pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                HiddenField hDescripXpistola = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPistola");
                HiddenField hDescripXpoppup = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hTramaPopPup");


                HiddenField hSeleccion = (HiddenField)gwvRehabilitarPorBCBP.Rows[j].FindControl("hSeleccion");

                string sTramaAsociada = string.Empty;
                dtBCBPRehabilitados.Rows[i]["Motivo"] = cboMotivo.SelectedItem.Value;
                dtBCBPRehabilitados.Rows[i]["Des_Motivo"] = cboMotivo.SelectedItem.Text;
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;


                if (hSeleccion.Value == "1")
                {
                    if (hDescripXpistola.Value != string.Empty)
                    {
                        sTramaAsociada = hDescripXpistola.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = string.Empty;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = string.Empty;
                    }
                    else if (hDescripXpoppup.Value != string.Empty)
                    {
                        sTramaAsociada = hDescripXpoppup.Value;
                        dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"] = string.Empty;
                        dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"] = string.Empty;
                    }

                    if (sTramaAsociada != string.Empty)
                    {
                        //Obtener Descripcion Company
                        dtBCBPRehabilitados.Rows[i]["Dsc_Compania_Asoc"] = string.Empty;
                        dtBCBPRehabilitados.Rows[i]["Nro_Vuelo_Asoc"] = string.Empty;
                        //Formatear Fecha
                        dtBCBPRehabilitados.Rows[i]["Fch_Vuelo_Asoc"] = string.Empty;
                        dtBCBPRehabilitados.Rows[i]["Nro_Asiento_Asoc"] = string.Empty;
                        dtBCBPRehabilitados.Rows[i]["Pasajero_Asoc"] = string.Empty;
                    }
                }
            }
        }

        Session["tabla"] = dtBCBPRehabilitados;

        //Define el PageIndex
        if (dtBCBPRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
        }
        
        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        //Lleno el combo y actualizo la seleccion del combo.
        DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");

        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        for (int i = (pageIndex * pageSize), j = 0; j < dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize); i++, j++)
        {
            DropDownList cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[j].FindControl("cboMotivo");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            if (i < dtBCBPRehabilitados.Rows.Count - 1)
            {
                cboMotivo.ClearSelection();
                cboMotivo.Items.FindByValue(dtBCBPRehabilitados.Rows[i]["Motivo"].ToString()).Selected = true;
            }

            
            if (dtBCBPRehabilitados.Rows[i]["TramaPoppup_Asoc"].ToString() == string.Empty &&
                dtBCBPRehabilitados.Rows[i]["TramaPistola_Asoc"].ToString() == string.Empty)
            {
                gwvRehabilitarPorBCBP.Rows[j].FindControl("tblDatosAsociados").Visible = false;
            }
        }

    }


    protected void btnImprimirBoucher_Click(object sender, ImageClickEventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        List<Hashtable> lstDataFormateada = ObtenerListadoBoucher((DataTable)Session["tabla"], (BoardingBcbpEstHist)Session["BcbpResultado"]);
        Imprimir(lstDataFormateada);

    }

    private List<Hashtable> ObtenerListadoBoucher(DataTable dtBCBPRehabilitados, BoardingBcbpEstHist objBcbp)
    {
        List<Hashtable> lstDataFormato = new List<Hashtable>();
        int iNumBoarding = 0;
        String sFechaRehab = DateTime.Now.ToShortDateString();

        for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
        {
            bool checkedRehabilitar = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Check"].ToString()) == 1 ? true : false;
            if (checkedRehabilitar)
            {

                Hashtable hsTabla = new Hashtable();
                //hsTabla.Add("nro_rehabi", ObtenerCodigoOperacion(objBcbp.SListaOperBcbp,
                                                                 //dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString()));
                hsTabla.Add("nro_rehabi", objBcbp.SListaOperBcbp);

                hsTabla.Add("nombre_pasajero", dtBCBPRehabilitados.Rows[i]["Nom_Pasajero"].ToString());
                hsTabla.Add("nombre_aerolinea", dtBCBPRehabilitados.Rows[i]["Dsc_Compania"].ToString());
                hsTabla.Add("numero_vuelo", dtBCBPRehabilitados.Rows[i]["Num_Vuelo"].ToString());
                hsTabla.Add("tipo_vuelo", dtBCBPRehabilitados.Rows[i]["Des_Tipo_Vuelo"].ToString());
                String sFecha = dtBCBPRehabilitados.Rows[i]["Fch_Vuelo"].ToString();
                sFecha = sFecha.Substring(6, 2) + "/" + sFecha.Substring(4, 2) + "/" + sFecha.Substring(0, 4);
                hsTabla.Add("fecha_vuelo", sFecha);
                hsTabla.Add("nro_asiento", dtBCBPRehabilitados.Rows[i]["Num_Asiento"].ToString());
                String sFechaUso = dtBCBPRehabilitados.Rows[i]["Fec_Uso"].ToString();
                sFechaUso = sFechaUso.Substring(6, 2) + "/" + 
                            sFechaUso.Substring(4, 2) + "/" + 
                            sFechaUso.Substring(0, 4) + " " +
                            sFechaUso.Substring(8, 2) + ":" +
                            sFechaUso.Substring(10, 2) + ":" +
                            sFechaUso.Substring(12, 2);
                hsTabla.Add("fecha_tuua", sFechaUso);
                string sDesMotivo = dtBCBPRehabilitados.Rows[i]["Des_Motivo"].ToString().Substring(0, 21);
                string sDesMotivo1 = dtBCBPRehabilitados.Rows[i]["Des_Motivo"].ToString().Substring(21);
                hsTabla.Add("motivo_rehab", sDesMotivo);
                hsTabla.Add("motivo_rehab1", sDesMotivo1);
                String sFechaHoraRehab = objBcbp.SLogFechaMod.Substring(6, 2) + "/" +
                                         objBcbp.SLogFechaMod.Substring(4, 2) + "/" +
                                         objBcbp.SLogFechaMod.Substring(0, 4) + " " +
                                         objBcbp.SLogFechaMod.Substring(8, 2) + ":" +
                                         objBcbp.SLogFechaMod.Substring(10, 2) + ":" +
                                         objBcbp.SLogFechaMod.Substring(12, 2);

                hsTabla.Add("fecha_hora_rehab", sFechaHoraRehab);

                lstDataFormato.Add(hsTabla);
                iNumBoarding++;

            }
        }

        return lstDataFormato;
    }

    private string ObtenerCodigoOperacion(string sCadena, string sParam)
    {
        string sResultado = string.Empty;
        string[] arrSecuencias = sCadena.Split('|');
        foreach (string sValor in arrSecuencias)
        {
            if (sValor.Split('*')[0] == sParam)
            {
                sResultado = sValor.Split('*')[1];
                break;
            }
        }

        return sResultado;
    }

    private void Imprimir(List<Hashtable> lstDataFormateada)
    {
        // instanciar objeto 
        Print objPrint = new Print();

        // obtiene el nodo segun el nombre del voucher
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], Define.ID_PRINTER_DOCUM_REHABILITACIONINDIVIDUAL);

        // configuracion de la impresora a utilizar
        string configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], Define.ID_PRINTER_DOCUM_REHABILITACIONINDIVIDUAL);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // obtiene la data a imprimir con la impresora de voucher y guardarla en una variable de sesion
        string dataVoucher = string.Empty;

        foreach (Hashtable htPrintData in lstDataFormateada)
        {
            dataVoucher = dataVoucher + objPrint.ObtenerDataFormateada(htPrintData, nodo);
        }

        //int copias = objPrint.ObtenerCopiasVoucher(nodo);
        // guarda data a imprimir en sesion
        Session["dataSticker"] = "";
        Session["dataVoucher"] = dataVoucher;

        Response.Redirect("Ope_Impresion.aspx?" +
            "flagImpSticker=0" +
            "&" + "flagImpVoucher=1" +
            //"&" + "copiasVoucher=" + copias +
            "&" + "configImpSticker=" + "" +
            "&" + "configImpVoucher=" + configImpVoucher +
            "&" + "Pagina_PreImpresion=Reh_BCBP.aspx", false);

    }

}
