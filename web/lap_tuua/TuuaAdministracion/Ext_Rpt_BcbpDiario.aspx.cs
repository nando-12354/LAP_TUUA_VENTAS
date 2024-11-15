﻿using System;
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

public partial class Ext_Rpt_BcbpDiario : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected bool Flg_Error;
    //Filtros
    string sFechaDesde, sFechaHasta;
    string sHoraDesde, sHoraHasta;
    string sFechaVuelo;
    string sTipoVuelo;
    string sTipoPasajero;
    string sTipoTrasbordo;
    string sNumVuelo;
    string sNumAsiento;
    string sNomPasajero;
    string sCodCompania, sCodIata;

    bool isFullUsado;
    bool isFullRehab;
    bool isFullAnul;

    //Summary Resumen
    int qResUNac = 0;
    int qResUInt = 0;

    
    BO_Reportes objListarBPDiarios = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Consultas objParametro = new BO_Consultas();

    Int32 valorMaxGrilla;
    DataTable dt_consulta = new DataTable();
    DataTable dt_resumen = new DataTable();
    DataTable dt_parametroTurno = new DataTable();
    DataTable dt_allcompania = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                lblFechaTitulo.Text = htLabels["rptExtBoardingDiario.lblFechaTitulo.Text"].ToString();
                lblFechaDesde.Text = htLabels["rptExtBoardingDiario.lblFechaDesde.Text"].ToString();
                lblFechaHasta.Text = htLabels["rptExtBoardingDiario.lblFechaHasta.Text"].ToString();
                lblFchVuelo.Text = htLabels["rptExtBoardingDiario.lblFchVuelo.Text"].ToString();
                lblTipoVuelo.Text = htLabels["rptExtBoardingDiario.lblTipoVuelo.Text"].ToString();
                lblTipoPersona.Text = htLabels["rptExtBoardingDiario.lblTipoPersona.Text"].ToString();
                lblTipoTrasbordo.Text = htLabels["rptExtBoardingDiario.lblTipoTrasbordo.Text"].ToString();
                lblNumVuelo.Text = htLabels["rptExtBoardingDiario.lblNumVuelo.Text"].ToString();
                lblNumAsiento.Text = htLabels["rptExtBoardingDiario.lblNumAsiento.Text"].ToString();
                lblNomPasajero.Text = htLabels["rptExtBoardingDiario.lblNomPasajero.Text"].ToString();
                lblAerolinea.Text = htLabels["rptExtBoardingDiario.lblAerolinea.Text"].ToString();

                btnConsultar.Text = htLabels["rptExtBoardingDiario.btnConsultar.Text"].ToString();


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

            this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
            this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();

            CargarCombos();
        }
    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo persona
            DataTable dt_tipopersona = new DataTable();
            dt_tipopersona = objConsulta.ListaCamposxNombre("TipoPasajero");
            UIControles objCargaComboPersona = new UIControles();
            objCargaComboPersona.LlenarCombo(ddlTipoPersona, dt_tipopersona, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga combo Tipo Vuelo
            DataTable dt_tipovuelo = new DataTable();
            dt_tipovuelo = objConsulta.ListaCamposxNombre("TipoVuelo");
            UIControles objCargaComboVuelo = new UIControles();
            objCargaComboVuelo.LlenarCombo(ddlTipoVuelo, dt_tipovuelo, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga combo Compañia
            UIControles objCargaCompania = new UIControles();
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaCompania.LlenarCombo(ddlAerolinea, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            //Carga combo Tipo Trasbordo
            DataTable dt_tipoTrasbordo = new DataTable();
            dt_tipoTrasbordo = objConsulta.ListaCamposxNombre("TipoTrasbordo");
            UIControles objCargaTipoTrasbordo = new UIControles();
            objCargaTipoTrasbordo.LlenarCombo(ddlTipoTrasbordo, dt_tipoTrasbordo, "Cod_Campo", "Dsc_Campo", true, false);

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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["dtResumenRehab"] = null;
            ViewState["dtResumenAnul"] = null;
            ViewState["dtResumenUsados"] = null;
            ViewState["dtResumenNetos"] = null;

            this.lblMensajeError.Text = "";

            ValidarTamanoGrilla();
            SaveFiltros();
            int iMaxReporte = GetMaximoReporte();

            if (getFechaElapsed(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text)) <= iMaxReporte || iMaxReporte < 0)
            {
                
                RecuperarFiltros();

                BindPagingGridUsado();
                if (isFullUsado) { CargarDataResumenUsado(); }
                BindPagingGridRehab();
                if (isFullRehab) { CargarDataResumenRehab(); }
                BindPagingGridAnul();
                if (isFullAnul) { CargarDataResumenAnul(); }

                if (isFullUsado || isFullRehab || isFullAnul) { CargarDataResumenNetos(); }

                lblMaxRegistros.Value = GetMaximoExcel().ToString();
                lblTotalRows.Value = lblMaxRegistros.Value;
            }
            else
            {
                this.lblMensajeError.Text = "Error. El periodo máximo de días a consultar el reporte es " + iMaxReporte + " días.";

                this.lblTitleUsado.Text = "";
                this.lblMensajeErrorDataUsado.Text = "";
                this.lblTotalUsado.Text = "";
                this.lblTotalRows.Value = "";

                grvBoardingUsado.DataSource = null;
                grvBoardingUsado.DataBind();
                grvResumenUsado.DataSource = null;
                grvResumenUsado.DataBind();

                this.lblTitleRehab.Text = "";
                this.lblMensajeErrorDataRehab.Text = "";
                this.lblTotalRehab.Text = "";
                grvBoardingRehab.DataSource = null;
                grvBoardingRehab.DataBind();
                grvResumenRehab.DataSource = null;
                grvResumenRehab.DataBind();

                this.lblTitleAnul.Text = "";
                this.lblMensajeErrorDataAnul.Text = "";
                this.lblTotalAnul.Text = "";
                grvBoardingAnul.DataSource = null;
                grvBoardingAnul.DataBind();
                grvResumenAnul.DataSource = null;
                grvResumenAnul.DataBind();

                grvResumenGeneral.DataSource = null;
                grvResumenGeneral.DataBind();
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            string strTipError = ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            string msgErr = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];
            ErrorHandler.Trace(msgErr, ex.StackTrace);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }  
    }

    #region Dynamic data query
    private void BindPagingGridUsado()
    {
        ValidarTamanoGrilla();
        htLabels = LabelConfig.htLabels;

        grvBoardingUsado.VirtualItemCount = GetRowCountUsado();
        DataTable dt_consulta = GetDataPageUsado(grvBoardingUsado.PageIndex, grvBoardingUsado.PageSize, grvBoardingUsado.OrderBy);

        lblTitleUsado.Text = (String)htLabels["rptExtBoardingDiario.lblTitleUsado.Text"];

        if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataUsado.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
                this.lblTotalUsado.Text = "";
                this.lblTotalRows.Value = "";
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
            grvBoardingUsado.DataSource = null;
            grvBoardingUsado.DataBind();
            grvResumenUsado.DataSource = null;
            grvResumenUsado.DataBind();

            grvResumenGeneral.DataSource = null;
            grvResumenGeneral.DataBind();
        }
        else
        {
            //grvTicketUsados.Visible = true;
            grvBoardingUsado.DataSource = dt_consulta;
            grvBoardingUsado.PageSize = Convert.ToInt32(valorMaxGrilla);
            grvBoardingUsado.DataBind();

            this.lblTotalUsado.Text = ((htLabels["rptExtBoardingDiario.lblTotalUsado"] == null) ? "" : htLabels["rptExtBoardingDiario.lblTotalUsado"].ToString()) + " " + grvBoardingUsado.VirtualItemCount;

            lblTotalRows.Value = grvBoardingUsado.VirtualItemCount.ToString();
            //this.lblMensajeError.Text = "";
            this.lblMensajeErrorDataUsado.Text = "";
            this.isFullUsado = true;
        }
    }

    private void BindPagingGridRehab()
    {
        ValidarTamanoGrilla();
        htLabels = LabelConfig.htLabels;

        grvBoardingRehab.VirtualItemCount = GetRowCountRehab();
        DataTable dt_consulta = GetDataPageRehab(grvBoardingRehab.PageIndex, grvBoardingRehab.PageSize, grvBoardingRehab.OrderBy);

        lblTitleRehab.Text = (String)htLabels["rptExtBoardingDiario.lblTitleRehab.Text"];

        if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataRehab.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
                this.lblTotalRehab.Text = "";
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
            grvBoardingRehab.DataSource = null;
            grvBoardingRehab.DataBind();
            grvResumenRehab.DataSource = null;
            grvResumenRehab.DataBind();

            grvResumenGeneral.DataSource = null;
            grvResumenGeneral.DataBind();
        }
        else
        {
            //grvTicketUsados.Visible = true;
            grvBoardingRehab.DataSource = dt_consulta;
            grvBoardingRehab.PageSize = Convert.ToInt32(valorMaxGrilla);
            grvBoardingRehab.DataBind();

            this.lblTotalRehab.Text = ((htLabels["rptExtBoardingDiario.lblTotalRehab"] == null) ? "" : htLabels["rptExtBoardingDiario.lblTotalRehab"].ToString()) + " " + grvBoardingRehab.VirtualItemCount;
            this.lblMensajeErrorDataRehab.Text = "";
            this.isFullRehab = true;
        }
    }

    private void BindPagingGridAnul()
    {
        ValidarTamanoGrilla();
        htLabels = LabelConfig.htLabels;

        grvBoardingAnul.VirtualItemCount = GetRowCountAnul();
        DataTable dt_consulta = GetDataPageAnul(grvBoardingAnul.PageIndex, grvBoardingAnul.PageSize, grvBoardingAnul.OrderBy);

        lblTitleAnul.Text = (String)htLabels["rptExtBoardingDiario.lblTitleAnul.Text"];

        if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataAnul.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
                this.lblTotalAnul.Text = "";
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
            grvBoardingAnul.DataSource = null;
            grvBoardingAnul.DataBind();
            grvResumenAnul.DataSource = null;
            grvResumenAnul.DataBind();
        }
        else
        {
            //grvTicketUsados.Visible = true;
            grvBoardingAnul.DataSource = dt_consulta;
            grvBoardingAnul.PageSize = Convert.ToInt32(valorMaxGrilla);
            grvBoardingAnul.DataBind();

            this.lblTotalAnul.Text = ((htLabels["rptExtBoardingDiario.lblTotalAnul"] == null) ? "" : htLabels["rptExtBoardingDiario.lblTotalAnul"].ToString()) + " " + grvBoardingAnul.VirtualItemCount;
            this.lblMensajeErrorDataAnul.Text = "";
            this.isFullAnul = true;
        }
    }

    private int GetRowCountUsado()
    {
        int count = 0;
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                            "0", null, 0, 0, "1");

        if (dt_consulta != null && dt_consulta.Columns.Contains("TotRows"))
        {
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
        }
        return count;
    }

    private DataTable GetDataPageUsado(int pageIndex, int pageSize, string sortExpression)
    {
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                            "0", sortExpression, pageIndex, Convert.ToInt32(valorMaxGrilla), "0");
        return dt_consulta;
    }

    private int GetRowCountRehab()
    {
        int count = 0;
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                            "1", null, 0, 0, "1");

        if (dt_consulta != null && dt_consulta.Columns.Contains("TotRows"))
        {
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
        }
        return count;
    }

    private DataTable GetDataPageRehab(int pageIndex, int pageSize, string sortExpression)
    {
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                             "1", sortExpression, pageIndex, Convert.ToInt32(valorMaxGrilla), "0");
        return dt_consulta;
    }

    private int GetRowCountAnul()
    {
        int count = 0;
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                            "5", null, 0, 0, "1");

        if (dt_consulta != null && dt_consulta.Columns.Contains("TotRows"))
        {
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
        }
        return count;
    }

    private DataTable GetDataPageAnul(int pageIndex, int pageSize, string sortExpression)
    {
        DataTable dt_consulta = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                             "5", sortExpression, pageIndex, Convert.ToInt32(valorMaxGrilla), "0");
        return dt_consulta;
    }
    #endregion

    #region Cargar/Guardas Filtros de Consulta
    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtFechaDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtFechaHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(this.txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(this.txtHoraHasta.Text)));
        filterList.Add(new Filtros("sFechaVuelo", Fecha.convertToFechaSQL2(this.txtFechaVuelo.Text)));

        filterList.Add(new Filtros("sTipoVuelo", this.ddlTipoVuelo.SelectedValue));
        filterList.Add(new Filtros("sTipoPasajero", this.ddlTipoPersona.SelectedValue));
        filterList.Add(new Filtros("sTipoTrasbordo", this.ddlTipoTrasbordo.SelectedValue));
        filterList.Add(new Filtros("sCodCompania", this.ddlAerolinea.SelectedValue));

        filterList.Add(new Filtros("sNumVuelo", this.txtNumVuelo.Text));
        filterList.Add(new Filtros("sNumAsiento", this.txtNumAsiento.Text));
        filterList.Add(new Filtros("sNomPasajero", this.txtNomPasajero.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;
        sFechaVuelo = newFilterList[4].Valor;
        
        sTipoVuelo = newFilterList[5].Valor;
        sTipoPasajero = newFilterList[6].Valor;
        sTipoTrasbordo = newFilterList[7].Valor;
        sCodCompania = newFilterList[8].Valor;

        sNumVuelo = newFilterList[9].Valor;
        sNumAsiento = newFilterList[10].Valor;
        sNomPasajero = newFilterList[11].Valor;

    }
    #endregion

    #region Parametros Generales
    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general              
        dt_parametroTurno = objParametro.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected Int64 GetMaximoExcel()
    {
        Int64 iMaxExcel = 0;
        DataTable dt_max = objParametro.ListarParametros("LE");

        if (dt_max.Rows.Count > 0)
            iMaxExcel = Convert.ToInt64(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxExcel;
    }

    protected int GetMaximoReporte()
    {
        int iMaxReporte = 0;
        DataTable dt_max = objParametro.ListarParametrosDefaultValue("LR");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt32(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }
    #endregion

    public int getFechaElapsed(DateTime fecha_ini, DateTime fecha_fin)
    {
        TimeSpan ts = fecha_fin - fecha_ini;

        int differenceInDays = ts.Days;

        return differenceInDays;
    }

    protected void grvBoardingUsado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecuperarFiltros();
        grvBoardingUsado.PageIndex = e.NewPageIndex;
        BindPagingGridUsado();
    }

    protected void grvBoardingUsado_Sorting(object sender, GridViewSortEventArgs e)
    {
        RecuperarFiltros();
        BindPagingGridUsado();
    }

    protected void grvBoardingUsado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            GridViewRow row = grvBoardingUsado.Rows[rowIndex];
            LinkButton addButton = (LinkButton)row.Cells[1].FindControl("Num_Secuencial_Bcbp");

            CnsDetBoarding1.CargarDetalleBoarding(addButton.Text);
        }
    }
   

    protected void grvResumenUsado_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 6;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen Boarding Pass Usados:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Int64 totalNac = 0;
            Int64 totalInt = 0;

            //AGREGAMOS EL TOTAL                
            DataTable dtResumenUsados = (DataTable)ViewState["dtResumenUsados"];

            foreach (DataRow row in dtResumenUsados.Rows)
            {
                string sTip_Vuelo = System.Convert.ToString(row["Dsc_Vuelo"].ToString());
                if ("NACIONAL".Equals(sTip_Vuelo))
                {
                    totalNac += Convert.ToInt64(row["Cantidad"]);
                }
                if ("INTERNACIONAL".Equals(sTip_Vuelo))
                {
                    totalInt += Convert.ToInt64(row["Cantidad"]);
                }
            }

            GridView oGridView_ = (GridView)sender;

            GridViewRow row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            TableCell th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Usados Nacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalNac);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);

            row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Usados Internacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalInt);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);
        }
    }

    //Rehabilitado
    protected void grvBoardingRehab_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecuperarFiltros();
        grvBoardingRehab.PageIndex = e.NewPageIndex;
        BindPagingGridRehab();
    }

    protected void grvBoardingRehab_Sorting(object sender, GridViewSortEventArgs e)
    {
        RecuperarFiltros();
        BindPagingGridRehab();
    }

    protected void grvBoardingRehab_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            GridViewRow row = grvBoardingRehab.Rows[rowIndex];

            LinkButton addButton = (LinkButton)row.Cells[1].FindControl("Num_Secuencial_Bcbp");
            CnsDetBoarding1.CargarDetalleBoarding(addButton.Text);
        }
    }
    
    //Anulado
    protected void grvBoardingAnul_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecuperarFiltros();
        grvBoardingAnul.PageIndex = e.NewPageIndex;
        BindPagingGridAnul();
    }
   
    protected void grvBoardingAnul_Sorting(object sender, GridViewSortEventArgs e)
    {
        RecuperarFiltros();
        BindPagingGridAnul();
    }

    protected void grvBoardingAnul_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            GridViewRow row = grvBoardingAnul.Rows[rowIndex];

            LinkButton addButton = (LinkButton)row.Cells[1].FindControl("Num_Secuencial_Bcbp");
            CnsDetBoarding1.CargarDetalleBoarding(addButton.Text);
        }
    }

    protected void grvResumenRehab_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 6;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen Boarding Pass Rehabilitados:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Int64 totalNac = 0;
            Int64 totalInt = 0;

            //AGREGAMOS EL TOTAL     
            DataTable dtResumenUsados = (DataTable)ViewState["dtResumenRehab"];

            foreach (DataRow row in dtResumenUsados.Rows)
            {
                string sTip_Vuelo = System.Convert.ToString(row["Dsc_Vuelo"].ToString());
                if ("NACIONAL".Equals(sTip_Vuelo))
                {
                    totalNac += Convert.ToInt64(row["Cantidad"]);
                }
                if ("INTERNACIONAL".Equals(sTip_Vuelo))
                {
                    totalInt += Convert.ToInt64(row["Cantidad"]);
                }
            }

            GridView oGridView_ = (GridView)sender;

            GridViewRow row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            TableCell th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Rehabilitados Nacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalNac);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);

            row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Rehabilitados Internacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalInt);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);
        }
    }

    protected void grvResumenAnul_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 6;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen Boarding Pass Anulados:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Int64 totalNac = 0;
            Int64 totalInt = 0;

            //AGREGAMOS EL TOTAL     
            DataTable dtResumenAnulados = (DataTable)ViewState["dtResumenAnul"];

            foreach (DataRow row in dtResumenAnulados.Rows)
            {
                string sTip_Vuelo = System.Convert.ToString(row["Dsc_Vuelo"].ToString());
                if ("NACIONAL".Equals(sTip_Vuelo))
                {
                    totalNac += Convert.ToInt64(row["Cantidad"]);
                }
                if ("INTERNACIONAL".Equals(sTip_Vuelo))
                {
                    totalInt += Convert.ToInt64(row["Cantidad"]);
                }
            }

            GridView oGridView_ = (GridView)sender;

            GridViewRow row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            TableCell th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Anulados Nacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalNac);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);

            row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 5;
            th_.Font.Bold = true;
            th_.Text = "Total Anulados Internacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalInt);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);
        }
    }

    public void CargarDataResumenUsado()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                             "2", null, 0, 0, "0");
        if (dtReportResumen.Rows.Count == 0)
        {
            grvResumenUsado.Visible = false;
            //lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            ViewState["dtResumenUsados"] = dtReportResumen;

            grvResumenUsado.DataSource = dtReportResumen;
            grvResumenUsado.DataBind();
        }
    }

    public void CargarDataResumenRehab()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                             "4", null, 0, 0, "0");
        if (dtReportResumen.Rows.Count == 0)
        {
            grvResumenRehab.Visible = false;
            //lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            ViewState["dtResumenRehab"] = dtReportResumen;

            grvResumenRehab.DataSource = dtReportResumen;
            grvResumenRehab.DataBind();
        }
    }

    public void CargarDataResumenAnul()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                            sFechaHasta,
                                            sHoraDesde,
                                            sHoraHasta,
                                            sCodCompania,
                                            sTipoPasajero,
                                            sTipoVuelo,
                                            sTipoTrasbordo,
                                            sFechaVuelo,
                                            sNumVuelo,
                                            sNomPasajero,
                                            sNumAsiento,
                                            sCodIata,
                                             "5", null, 0, 0, "2");
        if (dtReportResumen.Rows.Count == 0)


        {
            grvResumenAnul.Visible = false;
            //lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            ViewState["dtResumenAnul"] = dtReportResumen;

            grvResumenAnul.DataSource = dtReportResumen;
            grvResumenAnul.DataBind();
        }
    }

    public void CargarDataResumenNetos()
    {
        DataTable dtResumenUsados = null;
        if (ViewState["dtResumenUsados"] != null)
        {
            dtResumenUsados = ((DataTable)ViewState["dtResumenUsados"]).Copy();
        }

        DataTable dtResumenReh = null;
        if (ViewState["dtResumenRehab"] != null)
            dtResumenReh = ((DataTable)ViewState["dtResumenRehab"]).Copy();

        DataTable dtResumenAnul = null;
        if (ViewState["dtResumenAnul"] != null)
            dtResumenAnul = ((DataTable)ViewState["dtResumenAnul"]).Copy();

        if (dtResumenUsados != null || dtResumenReh != null || dtResumenAnul != null)
        {
            DataTable dt = dtResumenUsados != null ? dtResumenUsados : (dtResumenReh != null ? dtResumenReh.Clone() : dtResumenAnul.Clone());
            dt.Columns.Add("Tipo");

            int i = dt.Rows.Count;
            if (dtResumenReh != null)
            {
                foreach (DataRow row in dtResumenReh.Rows)
                {
                    //dtReportResumen.NewRow();
                    dt.Rows.Add(dt.NewRow());
                    /*row["Cantidad"] = "-" + row["Cantidad"];
                    dtReportResumen.Rows.Add(row);*/
                    dt.Rows[i][0] = row[0];
                    dt.Rows[i][1] = row[1];
                    dt.Rows[i][2] = row[2];
                    dt.Rows[i][3] = row[3];
                    dt.Rows[i][4] = row[4];
                    dt.Rows[i][5] = "-" + row["Cantidad"];
                    dt.Rows[i][6] = "(Rehab.)";
                    i++;
                }
            }

            //agregamos los anulados
            i = dt.Rows.Count;
            if (dtResumenAnul != null)
            {
                foreach (DataRow row in dtResumenAnul.Rows)
                {
                    dt.Rows.Add(dt.NewRow());
                    dt.Rows[i][0] = row[0];
                    dt.Rows[i][1] = row[1];
                    dt.Rows[i][2] = row[2];
                    dt.Rows[i][3] = row[3];
                    dt.Rows[i][4] = row[4];
                    dt.Rows[i][5] = "-" + row["Cantidad"];
                    dt.Rows[i][6] = "(Anul.)";
                    i++;
                }
            }

            if (dt.Rows.Count == 0)
            {
                grvResumenGeneral.Visible = false;
            }
            else
            {
                DataView dv = dt.DefaultView;
                dv.Sort = "Fch_Vuelo, Num_Vuelo, Dsc_Vuelo, Tip_Pasajero, Dsc_Trasbordo";

                ViewState["dtResumenNetos"] = dv.ToTable();

                grvResumenGeneral.DataSource = dv.Table;
                grvResumenGeneral.DataBind();
            }
        }
        else
        {
            grvResumenGeneral.DataSource = null;
            grvResumenGeneral.DataBind();
        }
    }

    protected void grvResumenGeneral_RowCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 7;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "RESUMEN BOARDING NETOS:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Int64 totalNac = 0;
            Int64 totalInt = 0;

            //AGREGAMOS EL TOTAL             
            DataTable dtResumenUsados = (DataTable)ViewState["dtResumenNetos"];

            foreach (DataRow row in dtResumenUsados.Rows)
            {
                string sTip_Vuelo = System.Convert.ToString(row["Dsc_Vuelo"].ToString());
                if ("NACIONAL".Equals(sTip_Vuelo))
                {
                    totalNac += Convert.ToInt64(row["Cantidad"]);
                }
                if ("INTERNACIONAL".Equals(sTip_Vuelo))
                {
                    totalInt += Convert.ToInt64(row["Cantidad"]);
                }
            }

            GridView oGridView_ = (GridView)sender;

            GridViewRow row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            TableCell th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 6;
            th_.Font.Bold = true;
            th_.Text = "Total Neto Nacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalNac);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);

            row_ = new GridViewRow(0, -1, DataControlRowType.Footer, DataControlRowState.Normal);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 6;
            th_.Font.Bold = true;
            th_.Text = "Total Neto Internacional:";
            row_.Cells.Add(th_);

            th_ = new TableCell();
            th_.HorizontalAlign = HorizontalAlign.Right;
            th_.ColumnSpan = 1;
            th_.Font.Bold = true;
            th_.Text = Convert.ToString(totalInt);
            row_.Cells.Add(th_);

            oGridView_.Controls[0].Controls.Add(row_);
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=BPDiarios.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta_u = new DataTable();
        DataTable dt_consulta_r = new DataTable();
        DataTable dt_consulta_a = new DataTable();
        DataTable dt_consulta_u_res = new DataTable();
        DataTable dt_consulta_r_res = new DataTable();
        DataTable dt_consulta_a_res = new DataTable();

        DataTable dt_resumen_neto = (DataTable)ViewState["dtResumenNetos"];
        if (dt_resumen_neto == null)
        {
            dt_resumen_neto = new DataTable();
        }

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta_u = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sCodCompania,
                                                                            sTipoPasajero,
                                                                            sTipoVuelo,
                                                                            sTipoTrasbordo,
                                                                            sFechaVuelo,
                                                                            sNumVuelo,
                                                                            sNomPasajero,
                                                                            sNumAsiento,
                                                                            sCodIata,
                                                                            "0", null, 0, 0, "0");

            dt_consulta_u_res = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                            sFechaHasta,
                                                            sHoraDesde,
                                                            sHoraHasta,
                                                            sCodCompania,
                                                            sTipoPasajero,
                                                            sTipoVuelo,
                                                            sTipoTrasbordo,
                                                            sFechaVuelo,
                                                            sNumVuelo,
                                                            sNomPasajero,
                                                            sNumAsiento,
                                                            sCodIata,
                                                            "2", null, 0, 0, "0");


            dt_consulta_r = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sCodCompania,
                                                                            sTipoPasajero,
                                                                            sTipoVuelo,
                                                                            sTipoTrasbordo,
                                                                            sFechaVuelo,
                                                                            sNumVuelo,
                                                                            sNomPasajero,
                                                                            sNumAsiento,
                                                                            sCodIata,
                                                                             "1", null, 0, 0, "0");

            dt_consulta_r_res = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                            sFechaHasta,
                                                            sHoraDesde,
                                                            sHoraHasta,
                                                            sCodCompania,
                                                            sTipoPasajero,
                                                            sTipoVuelo,
                                                            sTipoTrasbordo,
                                                            sFechaVuelo,
                                                            sNumVuelo,
                                                            sNomPasajero,
                                                            sNumAsiento,
                                                            sCodIata,
                                                            "4", null, 0, 0, "0");

            dt_consulta_a = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sCodCompania,
                                                                            sTipoPasajero,
                                                                            sTipoVuelo,
                                                                            sTipoTrasbordo,
                                                                            sFechaVuelo,
                                                                            sNumVuelo,
                                                                            sNomPasajero,
                                                                            sNumAsiento,
                                                                            sCodIata,
                                                                             "5", null, 0, 0, "0");

            dt_consulta_a_res = objListarBPDiarios.consultarBoardingPassDiario(sFechaDesde,
                                                            sFechaHasta,
                                                            sHoraDesde,
                                                            sHoraHasta,
                                                            sCodCompania,
                                                            sTipoPasajero,
                                                            sTipoVuelo,
                                                            sTipoTrasbordo,
                                                            sFechaVuelo,
                                                            sNumVuelo,
                                                            sNomPasajero,
                                                            sNumAsiento,
                                                            sCodIata,
                                                            "5", null, 0, 0, "2");
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


        #region Reporte BP Diario USADO
        Excel.Worksheet resumenDiarioUsado = new Excel.Worksheet("Reporte BP Diario USADO");
        resumenDiarioUsado.Columns = new string[] { "Nro. Correlativo", "Secuencial", "Tipo Documento", "Aerolinea","Pasajero", 
                                                    "Nro. Asiento", "Fecha Vuelo", "Nro. Vuelo", "Fecha Uso", "Tipo Vuelo", 
                                                    "Tipo Pasajero", "Tipo Trasbordo", "Estado Actual", "Nro. Boarding", "ETicket" };
        resumenDiarioUsado.WidthColumns = new int[] { 60, 75, 80, 170, 160, 50, 60, 60, 100, 90, 62, 70, 74, 60, 85 };
        resumenDiarioUsado.DataFields = new string[] { "Num_Secuencial_Bcbp", "Correlativo", "Tip_Documento", "Dsc_Compania", "Nom_Pasajero",
                                                        "Num_Asiento", "@Fch_Vuelo", "Num_Vuelo", "@Fch_Uso", "Dsc_Vuelo", 
                                                        "Tip_Pasajero", "Dsc_Trasbordo", "Dsc_Tip_Estado", "Nro_Boarding", "Cod_ETicket" };
        resumenDiarioUsado.Source = dt_consulta_u;
        #endregion

        #region BP Diario USADO RESUMEN
        Excel.Worksheet resumenUsado = new Excel.Worksheet("BP Diario USADO RESUMEN");
        resumenUsado.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenUsado.WidthColumns = new int[] { 60, 60, 80, 80, 100, 70 };
        resumenUsado.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Cantidad" };
        resumenUsado.Source = dt_consulta_u_res;
        #endregion

        #region Reporte BP Diario REHABILITADO
        Excel.Worksheet resumenDiarioRehab = new Excel.Worksheet("Reporte BP Diario REHAB");
        resumenDiarioRehab.Columns = new string[] { "Nro. Correlativo", "Secuencial", "Tipo Documento", "Aerolinea","Pasajero", 
                                                    "Nro. Asiento", "Fecha Vuelo", "Nro. Vuelo", "Fecha Uso", "Fecha Rehabilitación",
                                                    "Motivo","Nro. Rehabilitación", "Tipo Vuelo", 
                                                    "Tipo Pasajero", "Tipo Trasbordo", "Estado Actual", "Nro. Boarding", "ETicket" };
        resumenDiarioRehab.WidthColumns = new int[] { 60, 75, 80, 170, 160, 50, 60, 60, 100, 100, 150, 100, 90, 62, 70, 74, 60, 85 };
        resumenDiarioRehab.DataFields = new string[] { "Num_Secuencial_Bcbp", "Correlativo", "Tip_Documento", "Dsc_Compania", "Nom_Pasajero",
                                                        "Num_Asiento", "@Fch_Vuelo", "Num_Vuelo", "@Fch_Uso","@Fch_Rehab",
                                                        "Dsc_Causal_Rehab","Num_Proceso_Rehab", "Dsc_Vuelo", 
                                                        "Tip_Pasajero", "Dsc_Trasbordo", "Dsc_Tip_Estado", "Nro_Boarding", "Cod_ETicket" };
        resumenDiarioRehab.Source = dt_consulta_r;
        #endregion

        #region BP Diario REHABILITADO RESUMEN
        Excel.Worksheet resumenRehab = new Excel.Worksheet("BP Diario REHAB RESUMEN");
        resumenRehab.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenRehab.WidthColumns = new int[] { 60, 60, 80, 80, 100, 70 };
        resumenRehab.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Cantidad" };
        resumenRehab.Source = dt_consulta_r_res;
        #endregion

        #region Reporte BP Diario ANULADO
        Excel.Worksheet resumenDiarioAnul = new Excel.Worksheet("Reporte BP Diario ANUL");
        resumenDiarioAnul.Columns = new string[] { "Nro. Correlativo", "Secuencial", "Tipo Documento", "Aerolinea","Pasajero", 
                                                    "Nro. Asiento", "Fecha Vuelo", "Nro. Vuelo", "Fecha Uso", "Fecha Anulación",
                                                    "Motivo", "Tipo Vuelo", 
                                                    "Tipo Pasajero", "Tipo Trasbordo", "Estado Actual", "Nro. Boarding", "ETicket" };
        resumenDiarioAnul.WidthColumns = new int[] { 60, 75, 80, 170, 160, 50, 60, 60, 100, 100, 150, 90, 62, 70, 74, 60, 85 };
        resumenDiarioAnul.DataFields = new string[] { "Num_Secuencial_Bcbp", "Correlativo", "Tip_Documento", "Dsc_Compania", "Nom_Pasajero",
                                                        "Num_Asiento", "@Fch_Vuelo", "Num_Vuelo", "@Fch_Uso", "@Fch_Anul",
                                                        "Dsc_Motivo", "Dsc_Vuelo", 
                                                        "Tip_Pasajero", "Dsc_Trasbordo", "Dsc_Tip_Estado", "Nro_Boarding", "Cod_ETicket" };
        resumenDiarioAnul.Source = dt_consulta_a;
        #endregion.

        #region BP Diario ANULADO RESUMEN
        Excel.Worksheet resumenAnul = new Excel.Worksheet("BP Diario ANUL RESUMEN");
        resumenAnul.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenAnul.WidthColumns = new int[] { 60, 60, 80, 80, 100, 70 };
        resumenAnul.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Cantidad" };
        resumenAnul.Source = dt_consulta_a_res;
        #endregion

        #region BP Mensual NETO
        Excel.Worksheet resumenNeto = new Excel.Worksheet("BP Mensual NETO");
        resumenNeto.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "", "Cantidad" };
        resumenNeto.WidthColumns = new int[] { 60, 60, 80, 80, 100, 50, 70 };
        resumenNeto.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Tipo", "Cantidad" };
        resumenNeto.Source = dt_resumen_neto;
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { resumenDiarioUsado, resumenUsado, resumenDiarioRehab, resumenRehab, resumenDiarioAnul, resumenAnul, resumenNeto };

        return Workbook.Save();

    }
    
    
}