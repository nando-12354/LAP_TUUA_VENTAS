using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using Define = LAP.TUUA.UTIL.Define;
using System.Linq;
using System.Xml.Linq;

public partial class Ope_Conciliacion : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool flagError;
    private BO_Operacion objBO_Operacion;
    private List<Conciliado> lstConciliado;
    private string sCadenaSelec;
    private int sCantNDBcbp;
    private int sSizePage;
    protected Hashtable htParametro;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        htParametro = (Hashtable)Session["htParametro"];
        sSizePage = Int32.Parse((string)htParametro["LG"]);
        sCantNDBcbp = Int32.Parse((string)htParametro["UU"]);

        objBO_Operacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

        if (!IsPostBack)
        {
            //Limpiamos selecciones anteriores.
            LimpiarSession();
            htLabels = LabelConfig.htLabels;
            try
            {

                lblCia.Text = htLabels["rehabilitacionBoarding.lblCia"].ToString();
                lblFechaVuelo.Text = htLabels["rehabilitacionBoarding.lblFechaVuelo"].ToString();
                lblVuelo.Text = htLabels["rehabilitacionBoarding.lblVuelo"].ToString();
                lblAsiento.Text = htLabels["rehabilitacionBoarding.lblAsiento"].ToString();
                lblPersona.Text = htLabels["rehabilitacionBoarding.lblPersona"].ToString();
                btnConciliar.Text = "Enlazar";
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


            CargarCompanias();


            //Buscar("", "", "", "", "", "", "");

            DataTable dtValores = (DataTable)Session["tabla"];

            if (dtValores == null || dtValores.Rows.Count == 0)
            {
                lblErrorMsg.Text = "No existen Boarding en los ultimos " + sCantNDBcbp.ToString() + " dias para conciliar.";
            }

            hBandera.Value = "0";
        }



        hFechaActual.Value = DateTime.Now.ToShortDateString();

    }

    protected bool Buscar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin)
    {
        DataTable dtBcbpUsados = objBO_Operacion.ListarBcbpxConciliar(sCodCompania, strFchVuelo, strNumVuelo, strNumAsiento, strPasajero, strFecUsoIni, strFecUsoFin, Define.ESTADO_TICKET_USADO);
        
        if (hBandera.Value == "0")
        {
            if (dtBcbpUsados.Rows.Count == 0)
            {
                lblErrorMsg.Text = "Error. El Boarding Pass ingresado no existe o no se encuentra usado.";
                return false;
            }
            DataTable dtResult = (DataTable)Session["dtBcbpUsadosConci"];
            if (dtResult != null)
            {
                DataTable dtConcilia = ObtenerDataTableConcilia();
                if (dtConcilia.Select("Num_Secuencial_Bcbp=" + dtBcbpUsados.Rows[0]["Num_Secuencial_Bcbp"].ToString()).Length > 0)
                {
                    lblErrorMsg.Text = "Error. El Boarding Pass ya se encuentra ingresado";
                    return false;
                }
                DataTable dtRehPistola = objBO_Operacion.ListarBcbpxConciliar(dtBcbpUsados.Rows[0]["num_vuelo"].ToString().Substring(0, 2), dtBcbpUsados.Rows[0]["Num_Secuencial_Bcbp_Rel"].ToString(), "", "", ValidarNombrePasajero(dtBcbpUsados.Rows[0]["persona"].ToString()), dtBcbpUsados.Rows[0]["Num_Secuencial_Bcbp"].ToString(), dtBcbpUsados.Rows[0]["Log_Fecha_Mod"].ToString(), Define.ESTADO_TICKET_REHABILITADO);
                VerificarBcbcpUsoRelacionados2(dtBcbpUsados, dtRehPistola);
                if (dtRehPistola.Rows.Count == 0)
                {
                    lblErrorMsg.Text = "Error. El Boarding Pass ingresado no se puede conciliar.";
                    return false;
                }
                dtResult.Merge(dtBcbpUsados, false);
                AgregarHashTable(dtRehPistola, dtBcbpUsados.Rows[0]["Num_Secuencial_Bcbp"].ToString());
                ConstruirConcilia2(dtBcbpUsados, dtRehPistola);
            }
            else
            {
                dtResult = dtBcbpUsados;
            }

            Session["dtBcbpUsadosConci"] = dtResult;
        }
        else
        {
            if (dtBcbpUsados.Rows.Count == 0)
            {
                LimpiarGrilla();
                lblErrorMsg.Text = "Error. La busqueda no retorna resultado.";
                return false;
            }
            Session["dtBcbpUsadosConci"] = dtBcbpUsados;
        }


        ConstruirTablaConcilia(0);
        return true;
    }

    protected DataTable ObtenerDataTableConcilia()
    {
        DataTable dtConcilia = Session["tabla"] == null ? DefinirEstructuraConcilia() : (DataTable)Session["tabla"];
        return dtConcilia;
    }

    protected void ConstruirTablaConcilia(int iPageIndex)
    {
        int iSize = sSizePage;

        DataTable dtBcbpUsados = (DataTable)Session["dtBcbpUsadosConci"];
        DataTable dtConcilia = new DataTable();
        Hashtable htBcbpReh;
        if (dtBcbpUsados.Rows.Count == 0)
        {
            Session["tabla"] = null;

            lblErrorMsg.Text = "Su conculta no entrego ningun resultado.";
            gvwConciliaBcbcp.DataSource = null;
            gvwConciliaBcbcp.DataBind();

        }
        else
        {
            dtConcilia = Session["tabla"] == null ? ConstruirConcilia(dtBcbpUsados) : (DataTable)Session["tabla"];
            if (dtConcilia.Rows.Count == 0)
            {
                lblErrorMsg.Text = "Error. El Boarding Pass ingresado no se puede conciliar.";
                LimpiarGrilla();
                return;
            }
            htBcbpReh = (Hashtable)Session["htBcbpReh"];
            //Obtenemos la lista de bcbp a conciliar.
            List<Conciliado> lstConsil = (List<Conciliado>)Session["Seleccion"];

            gvwConciliaBcbcp.PageIndex = iPageIndex;
            gvwConciliaBcbcp.PageSize = iSize;
            gvwConciliaBcbcp.DataSource = dtConcilia;
            gvwConciliaBcbcp.DataBind();

            int iCursor = iPageIndex * iSize;

            for (int i = 0; i < gvwConciliaBcbcp.Rows.Count; i++)
            {
                DataTable dtBcbpxConci = (DataTable)htBcbpReh[dtConcilia.Rows[iCursor]["Num_Secuencial_Bcbp"].ToString()];
                CheckBoxList chkbxlsBcbp = (CheckBoxList)gvwConciliaBcbcp.Rows[i].FindControl("CheckBoxList1");
                chkbxlsBcbp.DataSource = dtBcbpxConci;
                chkbxlsBcbp.DataTextField = "dsc_bcbp_reh";
                chkbxlsBcbp.DataValueField = "Num_Secuencial_Bcbp";
                chkbxlsBcbp.DataBind();
                for (int j = 0; j < dtBcbpxConci.Rows.Count; j++)
                {
                    if (dtConcilia.Rows[iCursor]["Num_Secuencial_Bcbp_Rel"].ToString() == dtBcbpxConci.Rows[j]["Num_Secuencial_Bcbp"].ToString())
                    {
                        chkbxlsBcbp.Items[j].Selected = true;
                        chkbxlsBcbp.Items[j].Enabled = false;
                    }

                    //Buscar en la lista de bcbp chekeados para conciliar
                    if (lstConsil != null && lstConsil.Count > 0)
                    {
                        string sSecuenAcon = dtBcbpxConci.Rows[j]["Num_Secuencial_Bcbp"].ToString();

                        foreach (Conciliado objCon in lstConsil)
                        {
                            string[] arrSec = objCon.SBcbpAsociados.Split('|');
                            if (arrSec.Length > 0)
                            {
                                foreach (string sConSec in arrSec)
                                {
                                    if (sSecuenAcon == sConSec && dtConcilia.Rows[iCursor]["Num_Secuencial_Bcbp"].ToString() == objCon.SBcbpUlt)
                                    {
                                        chkbxlsBcbp.Items[j].Selected = true;
                                    }
                                    else if (sSecuenAcon == objCon.SBcbpBase && dtConcilia.Rows[iCursor]["Num_Secuencial_Bcbp"].ToString() == objCon.SBcbpUlt)
                                    {
                                        chkbxlsBcbp.Items[j].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                }

                iCursor = iCursor + 1;
            }
        }
    }

    private void CargarCompanias()
    {
        #region Llenando combos de busqueda
        BO_Consultas objBOConsultas = new BO_Consultas();
        DataTable dtCompanias = objBOConsultas.listarAllCompania();
        cboCompanias.DataSource = dtCompanias;
        cboCompanias.DataTextField = "Dsc_Compania";
        cboCompanias.DataValueField = "Cod_Compania";
        cboCompanias.DataBind();
        cboCompanias.Items.Insert(0, new ListItem("Seleccionar", ""));
        #endregion
    }

    private void ProcesarTrama(string strTrama)
    {
        Reader reader = new Reader();
        strTrama = "0?5" + strTrama;
        Hashtable ht = reader.ParseString_Boarding(strTrama);
        txtPersona.Text = "";
        if (ht == null)
        {
            lblErrorMsg.Text = "Error. El Boarding Pass ingresado tiene formato inválido.";
            //lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError1.Text"].ToString();
            return;
        }
        string fechaVuelo;
        string compania;
        String companiaAux = (String)ht[LAP.TUUA.CONVERSOR.Define.Compania];
        String fechaVueloAux = (String)ht[LAP.TUUA.CONVERSOR.Define.FechaVuelo];
        string nroVuelo = (String)ht[LAP.TUUA.CONVERSOR.Define.NroVuelo];
        string asiento = (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento];
        string persona = (String)ht[LAP.TUUA.CONVERSOR.Define.Persona];

        if (String.IsNullOrEmpty(companiaAux) || String.IsNullOrEmpty(fechaVueloAux) ||
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
        BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();
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
        if (!Buscar(compania, fechaVuelo, nroVuelo, (String)ht[LAP.TUUA.CONVERSOR.Define.Asiento], persona, "", ""))
        {
            //lblErrorMsg.Text = "Error. El Boarding Pass ingresado no se puede conciliar.";
        }

    }


    protected void btnConciliar_Click(object sender, EventArgs e)
    {
        //Cargar datos en grilla

        lstConciliado = this.CargarDatosGrilla();
        //Conciliar en BD
        if (lstConciliado.Count > 0)
        {
            this.ConciliacionBD(lstConciliado);

        }
        else
        {
            lblErrorMsg.Text = "No hay boardings seleccionados para conciliar.";
        }
    }

    public List<Conciliado> CargarDatosGrilla()
    {

        if (Session["Seleccion"] != null)
        {
            lstConciliado = (List<Conciliado>)Session["Seleccion"];
        }
        else
        {
            lstConciliado = new List<Conciliado>();
        }

        Conciliado objConciliado;
        DataTable dtConcilia = (DataTable)Session["tabla"];
        for (int i = 0; i < gvwConciliaBcbcp.Rows.Count; i++)
        {
            CheckBoxList chkbxlsBcbp = (CheckBoxList)gvwConciliaBcbcp.Rows[i].FindControl("CheckBoxList1");
            HiddenField hControl = (HiddenField)gvwConciliaBcbcp.Rows[i].FindControl("hSecuencia");

            string sCadenaSeleccion = string.Empty;
            sCadenaSelec = string.Empty;

            for (int f = 0; f < chkbxlsBcbp.Items.Count; f++)
            {
                if (chkbxlsBcbp.Items[f].Selected && chkbxlsBcbp.Items[f].Enabled)
                {
                    if (sCadenaSeleccion == string.Empty)
                    {
                        sCadenaSeleccion = chkbxlsBcbp.Items[f].Value;
                    }
                    else
                    {
                        sCadenaSeleccion += "|" + chkbxlsBcbp.Items[f].Value;
                    }
                }
            }
            if (sCadenaSeleccion != string.Empty)
            {
                objConciliado = new Conciliado();
                objConciliado.SBcbpUlt = string.Empty;
                objConciliado.SBcbpUlt = hControl.Value;

                sCadenaSelec = sCadenaSeleccion;

                string strCodBcbpRel = dtConcilia.Select("Num_Secuencial_Bcbp='" + objConciliado.SBcbpUlt + "'")[0]["Num_Secuencial_Bcbp_Rel"].ToString();

                objConciliado.SBcbpBase = strCodBcbpRel == "0" ? ObtenerSecuenciaBase(sCadenaSelec) : strCodBcbpRel;


                objConciliado.SBcbpAsociados = sCadenaSelec;

                bool isExisten = false;

                foreach (Conciliado objconsil in lstConciliado)
                {
                    if (objconsil.SBcbpUlt == hControl.Value)
                    {
                        objconsil.SBcbpBase = objConciliado.SBcbpBase;
                        objconsil.SBcbpAsociados = objConciliado.SBcbpAsociados;
                        isExisten = true;
                    }
                }

                if (!isExisten)
                {
                    lstConciliado.Add(objConciliado);
                }
            }
            else
            {
                List<Conciliado> lstNewConsiliado = new List<Conciliado>();
                foreach (Conciliado objconsil in lstConciliado)
                {
                    if (objconsil.SBcbpUlt != hControl.Value)
                    {
                        lstNewConsiliado.Add(objconsil);
                    }
                }
                lstConciliado = lstNewConsiliado;
            }
        }

        Session["Seleccion"] = lstConciliado;
        return lstConciliado;

    }

    public string ObtenerSecuenciaBase(string sCadenaSecuencias)
    {
        string sBase = string.Empty;
        string[] arrSecuencias = sCadenaSecuencias.Split('|');
        int iTot = 0;
        Int64 iMenor = 0;

        while (iTot < arrSecuencias.Length)
        {
            string sSec = arrSecuencias[iTot].Trim();
            if (sSec != string.Empty)
            {
                if (iMenor == 0)
                {
                    iMenor = Convert.ToInt64(sSec);
                }
                else
                {
                    if (iMenor > Convert.ToInt64(sSec))
                    {
                        iMenor = Convert.ToInt64(sSec);
                    }
                }
            }
            iTot++;
        }

        //Depurando la secuencia base.
        iTot = 0;
        string sNewCadena = string.Empty;

        while (iTot < arrSecuencias.Length)
        {
            string sSec = arrSecuencias[iTot].Trim();
            if (iMenor.ToString() != sSec && sSec != string.Empty)
            {
                if (sNewCadena == string.Empty)
                {
                    sNewCadena = sSec;
                }
                else
                {
                    sNewCadena += "|" + sSec;
                }
            }
            iTot++;
        }

        sCadenaSelec = sNewCadena;
        sBase = iMenor.ToString();
        return sBase;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        lblMensajeError.Text = "";
        if (hBandera.Value == "0")
        {
            //String strTrama = txtPersona.Text;
            String strTrama = hCodigoBarra.Value;
            hCodigoBarra.Value = string.Empty;
            if (strTrama != "")
            {
                String script =
                   "<script language=\"javascript\">" +
                   "LimpiarPersona();" +
                   "</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
                ProcesarTrama(strTrama);
            }
            else
            {
                lblErrorMsg.Text = "Error. El Boarding Pass ingresado tiene formato inválido.";
            }
        }
        else
        {

            //Limpiamos selecciones anteriores.
            LimpiarSession();
            string sFechaDesde = string.Empty;
            string sFechaHasta = string.Empty;
            string sFechaVuelo = string.Empty;
            string sAsiento = string.Empty;
            string sNroVuelo = string.Empty;
            string sCompania = string.Empty;
            string sPasajero = string.Empty;

            if (chkFechaUso.Checked)
            {
                sFechaDesde = txtDesde.Text;
                sFechaHasta = txtHasta.Text;
            }

            sFechaVuelo = txtFechaVuelo.Text;
            sAsiento = txtAsiento.Text;
            sNroVuelo = txtNroVuelo.Text;
            sCompania = cboCompanias.SelectedValue;
            sPasajero = txtPersona.Text;

            //Formateamos Fechas
            if (sFechaVuelo != string.Empty)
            {
                sFechaVuelo = sFechaVuelo.Split('/')[2] + sFechaVuelo.Split('/')[1] + sFechaVuelo.Split('/')[0];

            }
            if (sFechaDesde != string.Empty)
            {
                sFechaDesde = sFechaDesde.Split('/')[2] + sFechaDesde.Split('/')[1] + sFechaDesde.Split('/')[0];
            }

            if (sFechaHasta != string.Empty)
            {
                sFechaHasta = sFechaHasta.Split('/')[2] + sFechaHasta.Split('/')[1] + sFechaHasta.Split('/')[0];
            }

            this.Buscar(sCompania, sFechaVuelo, sNroVuelo, sAsiento, sPasajero, sFechaDesde, sFechaHasta);
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        DataTable dtConcilia = (DataTable)Session["tabla"];
        bool isEstado = ((CheckBox)gvwConciliaBcbcp.HeaderRow.FindControl("chkAll")).Checked;

        if (dtConcilia != null)
        {
            List<Conciliado> lstConsolidado;
            if (!isEstado)
            {
                lstConsolidado = new List<Conciliado>();

                for (int i = 0; i < dtConcilia.Rows.Count; i++)
                {
                    DataTable dtBcbpReh = (DataTable)((Hashtable)Session["htBcbpReh"])[dtConcilia.Rows[i]["Num_Secuencial_Bcbp"].ToString()];//objBO_Operacion.ListarBcbpxConciliar(dtConcilia.Rows[i]["num_vuelo"].ToString().Substring(0, 2), dtConcilia.Rows[i]["Num_Secuencial_Bcbp_Rel"].ToString(), "", "", ValidarNombrePasajero(dtConcilia.Rows[i]["persona"].ToString()), dtConcilia.Rows[i]["Num_Secuencial_Bcbp"].ToString(), dtConcilia.Rows[i]["Log_Fecha_Mod"].ToString(), Define.ESTADO_TICKET_REHABILITADO);
                    if (dtBcbpReh.Rows.Count > 0)
                    {
                        Conciliado objConsol = new Conciliado();
                        int iCont = 0;
                        string sCadena = string.Empty;
                        string sBcbpUlt = dtConcilia.Rows[i]["Num_Secuencial_Bcbp"].ToString();
                        string sBcbpBase = string.Empty;

                        while (iCont < dtBcbpReh.Rows.Count)
                        {
                            if (sCadena.Length > 0)
                            {
                                sCadena += "|" + dtBcbpReh.Rows[iCont]["Num_Secuencial_Bcbp"];
                            }
                            else
                            {
                                sCadena += dtBcbpReh.Rows[iCont]["Num_Secuencial_Bcbp"];
                            }
                            iCont++;
                        }

                        sBcbpBase = this.ObtenerSecuenciaBase(sCadena);
                        objConsol.SBcbpUlt = sBcbpUlt;
                        objConsol.SBcbpBase = sBcbpBase;
                        objConsol.SBcbpAsociados = sCadenaSelec;
                        lstConsolidado.Add(objConsol);
                    }
                }
            }
            else
            {
                lstConsolidado = null;
            }
            Session["Seleccion"] = lstConsolidado;

            ConstruirTablaConcilia(0);

            CheckBox chkAll = (CheckBox)gvwConciliaBcbcp.HeaderRow.FindControl("chkAll");
            chkAll.Checked = !isEstado;
            chkAll.DataBind();
        }
    }

    public void ConciliacionBD(List<Conciliado> lstConciliados)
    {
        bool isResult = false;
        int iResult = 0;

        foreach (Conciliado objConciliado in lstConciliado)
        {
            isResult = objBO_Operacion.RegistrarBcbpxConciliar(objConciliado.SBcbpBase,
                                                               objConciliado.SBcbpUlt,
                                                               objConciliado.SBcbpAsociados);
            if (isResult)
            {
                iResult = iResult + 1;
            }
        }

        lblMensajeError.Text = "Proceso ejecutado correctamente, se pudieron conciliar " + iResult.ToString() + " asociaciones";
        gvwConciliaBcbcp.DataSource = null;
        gvwConciliaBcbcp.DataBind();
        LimpiarSession();

    }

    #region Paginacion

    protected void gvwConciliaBcbcp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = gvwConciliaBcbcp.PageSize;
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        #region Guardo las selecciones del checkbox y comboBox

        bool isEstado = ((CheckBox)gvwConciliaBcbcp.HeaderRow.FindControl("chkAll")).Checked;
        lstConciliado = this.CargarDatosGrilla();

        #endregion

        ConstruirTablaConcilia(e.NewPageIndex);
        CheckBox chkAll = (CheckBox)gvwConciliaBcbcp.HeaderRow.FindControl("chkAll");
        chkAll.Checked = isEstado;
        chkAll.DataBind();
    }

    #endregion

    private string ValidarNombrePasajero(string strPasajero)
    {
        try
        {
            Int32.Parse(strPasajero.Trim());
            //ES LAN1D
            return "";
        }
        catch
        {
            return strPasajero;
        }
    }

    private DataTable ObtenerBcbcpRehxConciliar(DataTable dtBcbpUsados, int i)
    {
        if (Session["htBcbpReh"] != null)
        {
            Hashtable htBcbpReh = (Hashtable)Session["htBcbpReh"];
            DataTable dtBcbpReh = (DataTable)htBcbpReh[dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp"].ToString()];
            return dtBcbpReh == null ? new DataTable() : dtBcbpReh;
        }
        else
        {
            return objBO_Operacion.ListarBcbpxConciliar(dtBcbpUsados.Rows[i]["num_vuelo"].ToString().Substring(0, 2), dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp_Rel"].ToString(), "", "", ValidarNombrePasajero(dtBcbpUsados.Rows[i]["persona"].ToString()), dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp"].ToString(), dtBcbpUsados.Rows[i]["Log_Fecha_Mod"].ToString(), Define.ESTADO_TICKET_REHABILITADO);
        }
    }

    protected DataTable ConstruirConcilia(DataTable dtBcbpUsados)
    {
        DataTable dtConcilia = DefinirEstructuraConcilia();
        int intInd = 1;
        Hashtable htBcbpReh = new Hashtable();
        for (int i = 0; i < dtBcbpUsados.Rows.Count; i++)
        {
            DataTable dtBcbpReh = ObtenerBcbcpRehxConciliar(dtBcbpUsados, i);//objBO_Operacion.ListarBcbpxConciliar(dtBcbpUsados.Rows[i]["num_vuelo"].ToString().Substring(0, 2), dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp_Rel"].ToString(), "", "", ValidarNombrePasajero(dtBcbpUsados.Rows[i]["persona"].ToString()), dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp"].ToString(), dtBcbpUsados.Rows[i]["Log_Fecha_Mod"].ToString(), Define.ESTADO_TICKET_REHABILITADO);
            VerificarBcbcpUsoRelacionados(dtConcilia, dtBcbpReh);
            if (dtBcbpReh.Rows.Count > 0)
            {
                AgregarDataRowConcilia(dtConcilia, dtBcbpUsados, i, intInd++);
                if (htBcbpReh[dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp"].ToString()] == null)
                {
                    htBcbpReh.Add(dtBcbpUsados.Rows[i]["Num_Secuencial_Bcbp"].ToString(), dtBcbpReh);
                }
            }
        }
        //guardamos en session los rehabilitados que se pueden conciliar
        Session["htBcbpReh"] = htBcbpReh;
        if (dtConcilia.Rows.Count > 0)
        {
            Session["tabla"] = dtConcilia;
        }
        return dtConcilia;
    }

    protected void VerificarBcbcpUsoRelacionados(DataTable dtConcilia, DataTable dtBcbpReh)
    {
        for (int i = 0; i < dtBcbpReh.Rows.Count; i++)
        {
            if (dtBcbpReh.Rows.Count > 0 && dtBcbpReh.Rows.Count == dtBcbpReh.Select("Tip_Estado='U'").Length)
            {
                dtBcbpReh.Rows.RemoveAt(i--);
                continue;
            }
            var query = from dtBcbp in dtConcilia.AsEnumerable()
                        where Evaluar(dtBcbp.Field<String>("num_vuelo").Substring(0, 2), dtBcbpReh.Rows[i]["num_vuelo"].ToString().Substring(0, 2), dtBcbp.Field<String>("Num_Secuencial_Bcbp"), dtBcbpReh.Rows[i]["Num_Secuencial_Bcbp"].ToString(),
                        dtBcbpReh.Rows[i]["persona"].ToString(), dtBcbp.Field<String>("persona"))
                        select dtBcbp;

            //Create a table from the query.
            if (query.Count() > 0)
            {
                dtBcbpReh.Rows.RemoveAt(i--);
            }
        }

    }

    protected bool Evaluar(string strIata1, string strIata2, string strNumSec1, string strNumSec2, string strPer1, string strPer2)
    {
        if (strIata1 == strIata2 && Int64.Parse(strNumSec1.Trim()) > Int64.Parse(strNumSec2.Trim()) && (strPer1.Contains(strPer2) || strPer2.Contains(strPer1)))
            return true;
        return false;
    }

    private void AgregarHashTable(DataTable dtReh, string strNumBcbpSec)
    {
        Hashtable htReh = new Hashtable();
        htReh = Session["htBcbpReh"] == null ? htReh : (Hashtable)Session["htBcbpReh"];
        htReh.Add(strNumBcbpSec, dtReh);
    }

    protected void VerificarBcbcpUsoRelacionados2(DataTable dtConcilia, DataTable dtBcbpReh)
    {
        for (int i = 0; i < dtBcbpReh.Rows.Count; i++)
        {
            if (dtBcbpReh.Rows.Count > 0 && dtBcbpReh.Rows.Count == dtBcbpReh.Select("Tip_Estado='U'").Length)
            {
                dtBcbpReh.Rows.RemoveAt(i--);
                continue;
            }
        }
    }

    protected DataTable DefinirEstructuraConcilia()
    {
        DataTable dtConcilia = new DataTable();
        dtConcilia.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("compania", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("fecha_vuelo", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("num_vuelo", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("asiento", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("persona", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("fecha_uso", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("Num_Secuencial_Bcbp_Rel", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("Log_Fecha_Mod", System.Type.GetType("System.String"));
        dtConcilia.Columns.Add("CheckBoxList1", new CheckBoxList().GetType());
        return dtConcilia;
    }

    protected void AgregarDataRowConcilia(DataTable dtConcilia, DataTable dtBcbpUsados, int intRow, int intConciliaIndex)
    {
        DataRow row;
        row = dtConcilia.NewRow();
        row["Numero"] = intConciliaIndex;
        row["compania"] = dtBcbpUsados.Rows[intRow]["compania"];
        row["fecha_vuelo"] = dtBcbpUsados.Rows[intRow]["fecha_vuelo"];
        row["num_vuelo"] = dtBcbpUsados.Rows[intRow]["num_vuelo"];
        row["asiento"] = dtBcbpUsados.Rows[intRow]["asiento"];
        row["persona"] = dtBcbpUsados.Rows[intRow]["persona"];
        row["fecha_uso"] = dtBcbpUsados.Rows[intRow]["fecha_uso"];
        row["Num_Secuencial_Bcbp"] = dtBcbpUsados.Rows[intRow]["Num_Secuencial_Bcbp"];
        row["Num_Secuencial_Bcbp_Rel"] = dtBcbpUsados.Rows[intRow]["Num_Secuencial_Bcbp_Rel"];
        row["Log_Fecha_Mod"] = dtBcbpUsados.Rows[intRow]["Log_Fecha_Mod"];
        dtConcilia.Rows.Add(row);
    }

    protected DataTable ConstruirConcilia2(DataTable dtBcbpUsados, DataTable dtBcbpReh)
    {
        DataTable dtConcilia = DefinirEstructuraConcilia();
        DataTable dtResult = null;
        AgregarDataRowConcilia(dtConcilia, dtBcbpUsados, 0, gvwConciliaBcbcp.Rows.Count + 1);
        if (dtConcilia.Rows.Count > 0)
        {
            dtResult = Session["tabla"] != null ? (DataTable)Session["tabla"] : new DataTable();
            dtResult.Merge(dtConcilia, false);
            Session["tabla"] = dtResult;
        }
        return dtResult;
    }

    protected void LimpiarGrilla()
    {
        DataTable dtConcilia = DefinirEstructuraConcilia();
        gvwConciliaBcbcp.PageIndex = 0;
        gvwConciliaBcbcp.DataSource = dtConcilia;
        gvwConciliaBcbcp.DataBind();
    }


    protected void LimpiarSession()
    {
        Session.Remove("Seleccion");
        Session.Remove("tabla");
        Session.Remove("dtBcbpUsadosConci");
        Session.Remove("htBcbpReh");
    }
}
