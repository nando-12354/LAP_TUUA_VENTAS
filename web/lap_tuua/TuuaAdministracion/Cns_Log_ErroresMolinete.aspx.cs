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
using System.Collections.Generic;
using Define = LAP.TUUA.UTIL.Define;

public partial class Rpt_TicketBoardingRehabilitados : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sIDError;
    string sTipoError;
    string sCompania;
    string sCodMolinete; 
    string sTipoBoarding;
    string sTipIngreso;
    string sFchVuelo; 
    string sNumVuelo; 
    string sNumAsiento; 
    string sNomPasajero; 
    
    
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Operacion objOperacion = new BO_Operacion();

    BO_Consultas objParametro = new BO_Consultas();

    Int32 valorMaxGrilla;
    DataTable dt_consulta = new DataTable();
    DataTable dt_resumen = new DataTable();
    DataTable dt_parametroTurno = new DataTable();
    //DataTable dt_allcompania = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblTitulo.Text = htLabels["consLogErroresMolinete.lblTitulo.Text"].ToString();
                this.lblDesde.Text = htLabels["consLogErroresMolinete.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["consLogErroresMolinete.lblHasta.Text"].ToString();
                this.lblAsiento.Text = htLabels["consLogErroresMolinete.lblAsiento.Text"].ToString();
                this.lblTipoError.Text = htLabels["consLogErroresMolinete.lblTipoError.Text"].ToString();
                this.lblError.Text = htLabels["consLogErroresMolinete.lblError.Text"].ToString();
                this.lblPasajero.Text = htLabels["consLogErroresMolinete.lblPasajero.Text"].ToString();
                this.lblMolinete.Text = htLabels["consLogErroresMolinete.lblMolinete.Text"].ToString();
                this.lblAerolinea.Text = htLabels["consLogErroresMolinete.lblAerolinea.Text"].ToString();
                this.lblTipoBP.Text = htLabels["consLogErroresMolinete.lblTipoBP.Text"].ToString();
                this.lblFechVuelo.Text = htLabels["consLogErroresMolinete.lblFechVuelo.Text"].ToString();
                this.lblNumVuelo.Text = htLabels["consLogErroresMolinete.lblNumVuelo.Text"].ToString();
                this.lblTipoIngreso.Text = htLabels["consLogErroresMolinete.lblTipoIngreso.Text"].ToString();

                this.txtDesde.Text = DateTime.Now.ToShortDateString(); ;
                this.txtHasta.Text = DateTime.Now.ToShortDateString();
                
                CargarCombos();
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

    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo Error
            DataTable dt_tipoError = new DataTable();
            dt_tipoError = objConsulta.ListaCamposxNombre("LogErrorBCBCP");
            UIControles objCargaComboTipoError = new UIControles();
            objCargaComboTipoError.LlenarCombo(ddlTipoError, dt_tipoError, "Cod_Campo", "Dsc_Campo", true, false);

            // carga combo Moliente
            DataTable dt_allMolinete = new DataTable();
            dt_allMolinete = objOperacion.ListarMolinetes(null, null);
            UIControles objCargaComboMolinete = new UIControles();
            objCargaComboMolinete.LlenarCombo(ddlMolinete, dt_allMolinete, "Cod_Molinete", "Dsc_Molinete", true, false);

            // carga combo Compañia 
            DataTable dt_allcompania = new DataTable();
            UIControles objCargaCompania = new UIControles();
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaCompania.LlenarCombo(ddlAerolinea, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            //Carga combo Tipo BP
            DataTable dt_tipoBP = new DataTable();
            dt_tipoBP = objConsulta.ListaCamposxNombre("TipoBP");
            UIControles objCargaComboTipoBP = new UIControles();
            objCargaComboTipoBP.LlenarCombo(ddlTipoBP, dt_tipoBP, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga combo Tipo Ingreso
            DataTable dt_tipoIngreso = new DataTable();
            dt_tipoIngreso = objConsulta.ListaCamposxNombre("TipoIngreso");
            UIControles objCargaComboTipoIngreso = new UIControles();
            objCargaComboTipoIngreso.LlenarCombo(ddlTipoIngreso, dt_tipoIngreso, "Cod_Campo", "Dsc_Campo", true, false);
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

    protected Int64 GetMaximoExcel()
    {
        Int64 iMaxReporte = 0;
        BO_Consultas objParametro = new BO_Consultas();
        DataTable dt_max = objParametro.ListarParametros("LE");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt64(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (IsFechaValida())
        {
            SaveFiltros();
            BindPagingGrid();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
        }
    }

    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();

        if (sIDError == "1")
        {
            grvErrorNoFuncional.VirtualItemCount = GetRowCount();

            dt_consulta = GetDataPage(grvErrorNoFuncional.PageIndex, grvErrorNoFuncional.PageSize, grvErrorNoFuncional.OrderBy);

            if (dt_consulta.Rows.Count == 0)
            {
                grvErrorNoFuncional.Visible = false;
                grvErrorFuncional.Visible = false;
                lblMensajeErrorData.Text = "La búsqueda no retorna resultado";
                lblTotal.Text = "";
                lblTotalRows.Value = "";
            }
            else
            {
                lblMensajeErrorData.Text = "";

                grvErrorNoFuncional.Visible = true;
                grvErrorFuncional.Visible = false;
                grvErrorNoFuncional.DataSource = dt_consulta;
                grvErrorNoFuncional.PageSize = valorMaxGrilla;
                grvErrorNoFuncional.DataBind();

                lblTotal.Text = "Total de Registros:" + grvErrorNoFuncional.VirtualItemCount;
                lblTotalRows.Value = grvErrorNoFuncional.VirtualItemCount.ToString();
            }
        }
        else
        {
            if (sIDError == "2")
            {
                grvErrorFuncional.VirtualItemCount = GetRowCount();

                dt_consulta = GetDataPage(grvErrorFuncional.PageIndex, grvErrorFuncional.PageSize, grvErrorFuncional.OrderBy);

                if (dt_consulta.Rows.Count == 0)
                {
                    grvErrorNoFuncional.Visible = false;
                    grvErrorFuncional.Visible = false;
                    lblMensajeErrorData.Text = "La búsqueda no retorna resultado";
                    lblTotal.Text = "";
                    lblTotalRows.Value = "";
                }
                else
                {
                    lblMensajeErrorData.Text = "";

                    grvErrorNoFuncional.Visible = false;
                    grvErrorFuncional.Visible = true;
                    grvErrorFuncional.DataSource = dt_consulta;
                    grvErrorFuncional.PageSize = valorMaxGrilla;
                    grvErrorFuncional.DataBind();

                    lblTotal.Text = "Total de Registros:" + grvErrorFuncional.VirtualItemCount;
                    lblTotalRows.Value = grvErrorFuncional.VirtualItemCount.ToString();
                }
            }
        }
    }

    #region Dynamic data query
    private int GetRowCount()
    {
        int count = 0;
        try
        {
            dt_resumen = objConsulta.ListarLogErroresMolinete(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sHoraDesde,
                                                                    sHoraHasta,
                                                                    sIDError,
                                                                    sTipoError,
                                                                    sCompania,
                                                                    sCodMolinete,
                                                                    sTipoBoarding,
                                                                    sTipIngreso,
                                                                    sFchVuelo,
                                                                    sNumVuelo,
                                                                    sNumAsiento,
                                                                    sNomPasajero,
                                                                    null,0,0,"0","0","1");

            if (dt_resumen.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_resumen.Rows[0].ItemArray.GetValue(1).ToString();

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
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        try
        {
            dt_consulta = objConsulta.ListarLogErroresMolinete(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sHoraDesde,
                                                                    sHoraHasta,
                                                                    sIDError,
                                                                    sTipoError,
                                                                    sCompania,
                                                                    sCodMolinete,
                                                                    sTipoBoarding,
                                                                    sTipIngreso,
                                                                    sFchVuelo,
                                                                    sNumVuelo,
                                                                    sNumAsiento,
                                                                    sNomPasajero,
                                                                    sortExpression, pageIndex, valorMaxGrilla, "1", "0", "0");
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

        return dt_consulta;
    }
    #endregion

    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sIDError", rbError.SelectedValue));
        filterList.Add(new Filtros("sTipoError", ddlTipoError.SelectedValue));
        filterList.Add(new Filtros("sCompania", ddlAerolinea.SelectedValue));
        filterList.Add(new Filtros("sCodMolinete", ddlMolinete.SelectedValue));
        filterList.Add(new Filtros("sTipoBoarding", ddlTipoBP.SelectedValue));
        filterList.Add(new Filtros("sTipIngreso", ddlTipoIngreso.SelectedValue));
        filterList.Add(new Filtros("sFchVuelo", Fecha.convertToFechaSQL2(txtFechVuelo.Text)));
        filterList.Add(new Filtros("sNumVuelo", txtNumVuelo.Text));
        filterList.Add(new Filtros("sNumAsiento", txtAsiento.Text));
        filterList.Add(new Filtros("sNomPasajero", txtPasajero.Text));

        ViewState.Add("Filtros", filterList);
    }

    public void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;

        sIDError = newFilterList[4].Valor;
        sTipoError = newFilterList[5].Valor;
        sCompania = newFilterList[6].Valor;
        sCodMolinete = newFilterList[7].Valor;
        sTipoBoarding = newFilterList[8].Valor;
        sTipIngreso = newFilterList[9].Valor;
        sFchVuelo = newFilterList[10].Valor;
        sNumVuelo = newFilterList[11].Valor;
        sNumAsiento = newFilterList[12].Valor;
        sNomPasajero = newFilterList[13].Valor;
    }

    private bool IsFechaValida()
    {
        int iValiFechas;
        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;
            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";

            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));
        }
        else
        {
            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + sHoraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + sHoraHasta));
        }

        if (iValiFechas == 1)
        {
            lblMensajeError.Text = "Filtro de fecha invalido";
            lblMensajeErrorData.Text = "";
            grvErrorNoFuncional.Visible = false;
            lblTotal.Text = "";
            lblTotalRows.Value = "";
            return false;
        }
        else
        {
            this.lblMensajeError.Text = "";
            return true;
        }
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general              
        dt_parametroTurno = objParametro.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=LogErrores.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objConsulta.ListarLogErroresMolinete(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sHoraDesde,
                                                                    sHoraHasta,
                                                                    sIDError,
                                                                    sTipoError,
                                                                    sCompania,
                                                                    sCodMolinete,
                                                                    sTipoBoarding,
                                                                    sTipIngreso,
                                                                    sFchVuelo,
                                                                    sNumVuelo,
                                                                    sNumAsiento,
                                                                    sNomPasajero,
                                                                    null, 0, 0, "1", "0", "0");
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

        #endregion

        Excel Workbook = new Excel();

        #region Log Errores de Molinetes
        Excel.Worksheet logErrores;

        if (sIDError == "1")
        {

            logErrores = new Excel.Worksheet("Log Errores Moliente");
            logErrores.Columns = new string[] { "Identificador", "Fecha Error", "Molinete", "Descripcion Error","Tipo Ingreso", 
                                                    "Tipo BP", "Aerolinea", "Fecha Vuelo", "Nro Vuelo", "Nro Asiento", "Pasajero" };

            logErrores.WidthColumns = new int[] { 70, 100, 110, 180, 45, 35, 140, 70, 45, 50, 145 };
            logErrores.DataFields = new string[] { "Num_Secuencial", "@Fecha", "Dsc_Molinete", "Dsc_Error", "Dsc_TipoIngreso", 
                                                        "Tip_Boarding", "Dsc_Compania", "@Fch_Vuelo", "Num_Vuelo", "Num_Asiento", "Nom_Pasajero" };
            logErrores.Source = dt_consulta;
        }
        else
        {
            logErrores = new Excel.Worksheet("Log Errores Moliente");
            logErrores.Columns = new string[] { "Identificador", "Fecha Error", "Molinete", "Descripcion Error","Tipo Ingreso", 
                                                    "Tipo BP", "Aerolinea", "Fecha Vuelo", "Nro Vuelo", "Nro Asiento", "Pasajero","Log Error" };

            logErrores.WidthColumns = new int[] { 70, 100, 110, 180, 45, 35, 140, 70, 45, 50, 145, 130 };
            logErrores.DataFields = new string[] { "Num_Secuencial", "@Fecha", "Dsc_Molinete", "Dsc_Campo", "Dsc_TipoIngreso", 
                                                        "Tip_Boarding", "Dsc_Compania", "@Fch_Vuelo", "Num_Vuelo", "Num_Asiento", "Nom_Pasajero","Log_Error" };
            logErrores.Source = dt_consulta;
        }
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { logErrores };

        return Workbook.Save();
    }

    protected void rbError_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbError.SelectedValue == "1")
        {
            //Carga combo Tipo Error
            DataTable dt_tipoError = new DataTable();
            dt_tipoError = objConsulta.ListaCamposxNombre("LogErrorBCBCP");
            UIControles objCargaComboTipoError = new UIControles();
            objCargaComboTipoError.LlenarCombo(ddlTipoError, dt_tipoError, "Cod_Campo", "Dsc_Campo", true, false);

        }
        else
        {
            //Carga combo Tipo Error
            DataTable dt_tipoError = new DataTable();
            dt_tipoError = objConsulta.ListaCamposxNombre("ErrorBCBP");
            UIControles objCargaComboTipoError = new UIControles();
            objCargaComboTipoError.LlenarCombo(ddlTipoError, dt_tipoError, "Cod_Campo", "Dsc_Campo", true, false);
        }
    }

    protected void grvErrorNoFuncional_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvErrorNoFuncional.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvErrorNoFuncional_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }

    protected void grvErrorNoFuncional_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowLog")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            string strLog = grvErrorNoFuncional.DataKeys[rowIndex].Value.ToString();
            CnsLogMolinete1.CargarLog(strLog);
        }
    }

    protected void grvErrorFuncional_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvErrorFuncional.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvErrorFuncional_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }
}
