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

public partial class Ext_Rpt_BcbpMensual : System.Web.UI.Page
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
    string sCodCompania, sCodIata, sPassword;

    private string tmpFechaVueloUsado = "";
    private string tmpFechaVueloRehab = "";
    private string tmpFechaVueloAnul = "";
    private string tmpFechaVueloNeto = "";

    string sMaxGrilla;

    bool isFullUsado;
    bool isFullRehab;
    bool isFullAnul;

    Int32 valorMaxGrilla;
    BO_Reportes objListarBPMensual = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Consultas objParametro = new BO_Consultas();

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
                lblFechaTitulo.Text = htLabels["rptExtBoardingMensual.lblFechaTitulo.Text"].ToString();
                lblFechaDesde.Text = htLabels["rptExtBoardingMensual.lblFechaDesde.Text"].ToString();
                lblFechaHasta.Text = htLabels["rptExtBoardingMensual.lblFechaHasta.Text"].ToString();
                lblFchVuelo.Text = htLabels["rptExtBoardingMensual.lblFchVuelo.Text"].ToString();
                lblTipoVuelo.Text = htLabels["rptExtBoardingMensual.lblTipoVuelo.Text"].ToString();
                lblTipoPersona.Text = htLabels["rptExtBoardingMensual.lblTipoPersona.Text"].ToString();
                lblTipoTrasbordo.Text = htLabels["rptExtBoardingMensual.lblTipoTrasbordo.Text"].ToString();
                lblNumVuelo.Text = htLabels["rptExtBoardingMensual.lblNumVuelo.Text"].ToString();
                lblAerolinea.Text = htLabels["rptExtBoardingDiario.lblAerolinea.Text"].ToString();

                btnConsultar.Text = htLabels["rptExtBoardingMensual.btnConsultar.Text"].ToString();

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
            ViewState["dtResumenUsados"] = null;
            ViewState["dtResumenAnul"] = null;
            ViewState["dtResumenNetos"] = null;

            this.lblMensajeError.Text = "";

            ValidarTamanoGrilla();
            SaveFiltros();
            int iMaxReporte = GetMaximoReporte();


            if (getFechaElapsed(Convert.ToDateTime(txtFechaDesde.Text), Convert.ToDateTime(txtFechaHasta.Text)) <= iMaxReporte || iMaxReporte < 0)
            {
                
                RecuperarFiltros();

                CargarDataResumenUsado();
                CargarDataResumenRehab();
                CargarDataResumenAnul();

                if (isFullUsado || isFullRehab || isFullAnul) { CargarDataResumenNetos(); }

                lblMaxRegistros.Value = GetMaximoExcel().ToString();
                lblTotalRows.Value = lblMaxRegistros.Value;
            }
            else
            {
                this.lblMensajeError.Text = "Error. El periodo máximo de días a consultar el reporte es " + iMaxReporte + " días.";

                this.lblTitleUsado.Text = "";
                this.lblMensajeErrorDataUsado.Text = "";
                this.lblTotalRows.Value = "";

                grvResumenUsado.DataSource = null;
                grvResumenUsado.DataBind();


                this.lblTitleRehab.Text = "";
                this.lblMensajeErrorDataRehab.Text = "";
                grvResumenRehab.DataSource = null;
                grvResumenRehab.DataBind();

                this.lblTitleAnul.Text = "";
                this.lblMensajeErrorDataAnul.Text = "";
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

        filterList.Add(new Filtros("sMaxGrilla", this.sMaxGrilla));

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

    public void CargarDataResumenUsado()
    {
        htLabels = LabelConfig.htLabels;
        lblTitleUsado.Text = (String)htLabels["rptExtBoardingMensual.lblTitleUsado.Text"];

        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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
        if (!(dtReportResumen != null && dtReportResumen.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataUsado.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
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

            grvResumenUsado.DataSource = null;
            grvResumenUsado.DataBind();

            grvResumenGeneral.DataSource = null;
            grvResumenGeneral.DataBind();
        }
        else
        {
            lblTotalRows.Value = dtReportResumen.Rows.Count.ToString();

            this.lblMensajeErrorDataUsado.Text = "";
            ViewState["dtResumenUsados"] = dtReportResumen;
            tmpFechaVueloUsado = dtReportResumen.Rows[0]["Fch_Vuelo"].ToString();
            grvResumenUsado.DataSource = dtReportResumen;
            grvResumenUsado.DataBind();
            this.isFullUsado = true;
        }
    }

    public void CargarDataResumenRehab()
    {
        htLabels = LabelConfig.htLabels;
        lblTitleRehab.Text = (String)htLabels["rptExtBoardingMensual.lblTitleRehab.Text"];

        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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

        if (!(dtReportResumen != null && dtReportResumen.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataRehab.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
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
            grvResumenRehab.DataSource = null;
            grvResumenRehab.DataBind();

            grvResumenGeneral.DataSource = null;
            grvResumenGeneral.DataBind();
        }
        else
        {
            this.lblMensajeErrorDataRehab.Text = "";
            ViewState["dtResumenRehab"] = dtReportResumen;
            tmpFechaVueloRehab = dtReportResumen.Rows[0]["Fch_Vuelo"].ToString();
            grvResumenRehab.DataSource = dtReportResumen;
            grvResumenRehab.DataBind();
            this.isFullRehab = true;
        }
    }

    public void CargarDataResumenAnul()
    {
        htLabels = LabelConfig.htLabels;
        lblTitleAnul.Text = (String)htLabels["rptExtBoardingMensual.lblTitleAnul.Text"];

        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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

        if (!(dtReportResumen != null && dtReportResumen.Rows.Count != 0))
        {
            try
            {
                this.lblMensajeErrorDataAnul.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
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
            grvResumenAnul.DataSource = null;
            grvResumenAnul.DataBind();
        }
        else
        {
            this.lblMensajeErrorDataAnul.Text = "";
            ViewState["dtResumenAnul"] = dtReportResumen;
            tmpFechaVueloAnul = dtReportResumen.Rows[0]["Fch_Vuelo"].ToString();
            grvResumenAnul.DataSource = dtReportResumen;
            grvResumenAnul.DataBind();
            this.isFullAnul = true;
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
                    dt.Rows.Add(dt.NewRow());
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
                //lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {

                DataView dv = dt.DefaultView;
                dv.Sort = "Fch_Vuelo, Num_Vuelo, Dsc_Vuelo, Tip_Pasajero, Dsc_Trasbordo";

                ViewState["dtResumenNetos"] = dv.ToTable();
                tmpFechaVueloNeto = dv.Table.Rows[0]["Fch_Vuelo"].ToString();
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
    }
    protected void grvResumenUsado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tmpFechaVueloUsado != drv["Fch_Vuelo"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtResumenRehabilitados = (DataTable)ViewState["dtResumenUsados"];

                object totalNacional = dtResumenRehabilitados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloUsado + "' AND Dsc_Vuelo='NACIONAL'");
                object totalInternacional = dtResumenRehabilitados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloUsado + "' AND Dsc_Vuelo='INTERNACIONAL'");

                tmpFechaVueloUsado = drv["Fch_Vuelo"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila Rehabilitado Total
                    cell.ColumnSpan = this.grvResumenRehab.Columns.Count - 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total Usados Nacional: <br />Total Usados Internacional:";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                    totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                    span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtResumenRehabilitados = (DataTable)ViewState["dtResumenUsados"];

            object totalNacional = dtResumenRehabilitados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloUsado + "' AND Dsc_Vuelo='NACIONAL'");
            object totalInternacional = dtResumenRehabilitados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloUsado + "' AND Dsc_Vuelo='INTERNACIONAL'");

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila Rehabilitado Total
                cell.ColumnSpan = this.grvResumenRehab.Columns.Count - 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total Usados Nacional: <br />Total Usados Internacional:";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                #endregion
            }
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
    }
    protected void grvResumenRehab_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tmpFechaVueloRehab != drv["Fch_Vuelo"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtResumenUsados = (DataTable)ViewState["dtResumenRehab"];

                object totalNacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloRehab + "' AND Dsc_Vuelo='NACIONAL'");
                object totalInternacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloRehab + "' AND Dsc_Vuelo='INTERNACIONAL'");

                tmpFechaVueloRehab = drv["Fch_Vuelo"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila Rehabilitado Total
                    cell.ColumnSpan = this.grvResumenRehab.Columns.Count - 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total Rehabilitados Nacional: <br />Total Rehabilitados Internacional:";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                    totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                    span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtResumenUsados = (DataTable)ViewState["dtResumenRehab"];

            object totalNacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloRehab + "' AND Dsc_Vuelo='NACIONAL'");
            object totalInternacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloRehab + "' AND Dsc_Vuelo='INTERNACIONAL'");

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila Rehabilitado Total
                cell.ColumnSpan = this.grvResumenRehab.Columns.Count - 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total Rehabilitados Nacional: <br />Total Rehabilitados Internacional:";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                #endregion
            }
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
    }
    protected void grvResumenAnul_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tmpFechaVueloAnul != drv["Fch_Vuelo"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtResumenUsados = (DataTable)ViewState["dtResumenAnul"];

                object totalNacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloAnul + "' AND Dsc_Vuelo='NACIONAL'");
                object totalInternacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloAnul + "' AND Dsc_Vuelo='INTERNACIONAL'");

                tmpFechaVueloAnul = drv["Fch_Vuelo"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila Anulado Total
                    cell.ColumnSpan = this.grvResumenAnul.Columns.Count - 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total Anulados Nacional: <br />Total Anulados Internacional:";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                    totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                    span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtResumenUsados = (DataTable)ViewState["dtResumenAnul"];

            object totalNacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloAnul + "' AND Dsc_Vuelo='NACIONAL'");
            object totalInternacional = dtResumenUsados.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloAnul + "' AND Dsc_Vuelo='INTERNACIONAL'");

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila Anulado Total
                cell.ColumnSpan = this.grvResumenAnul.Columns.Count - 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total Anulados Nacional: <br />Total Anulados Internacional:";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                #endregion
            }
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
    }
    protected void grvResumenGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tmpFechaVueloNeto != drv["Fch_Vuelo"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtResumenNetos = (DataTable)ViewState["dtResumenNetos"];

                object totalNacional = dtResumenNetos.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloNeto + "' AND Dsc_Vuelo='NACIONAL'");
                object totalInternacional = dtResumenNetos.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloNeto + "' AND Dsc_Vuelo='INTERNACIONAL'");

                tmpFechaVueloNeto = drv["Fch_Vuelo"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila Neto Total
                    cell.ColumnSpan = this.grvResumenGeneral.Columns.Count-1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total Neto Nacional: <br />Total Neto Internacional:";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                    totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                    span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtResumenNetos = (DataTable)ViewState["dtResumenNetos"];

            object totalNacional = dtResumenNetos.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloNeto + "' AND Dsc_Vuelo='NACIONAL'");
            object totalInternacional = dtResumenNetos.Compute("Sum(Cantidad)", "Fch_Vuelo = '" + tmpFechaVueloNeto + "' AND Dsc_Vuelo='INTERNACIONAL'");

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila Neto Total
                cell.ColumnSpan = this.grvResumenGeneral.Columns.Count - 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total Neto Nacional: <br />Total Neto Internacional:";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalNacional = totalNacional.ToString() == "" ? "0" : totalNacional.ToString();
                totalInternacional = totalInternacional.ToString() == "" ? "0" : totalInternacional.ToString();
                span.InnerHtml = totalNacional.ToString() + "<br />" + totalInternacional.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                #endregion
            }
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=BPMensual.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
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
            dt_consulta_u_res = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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

            dt_consulta_r_res = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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

            dt_consulta_a_res = objListarBPMensual.consultarBoardingPassDiario(sFechaDesde,
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


        #region BP Mensual USADOS
        Excel.Worksheet resumenUsado = new Excel.Worksheet("BP Mensual USADOS");
        resumenUsado.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenUsado.WidthColumns = new int[] { 60, 60, 80, 80, 100, 70 };
        resumenUsado.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Cantidad" };
        resumenUsado.Source = dt_consulta_u_res;
        #endregion

        #region BP Mensual REHABILITADOS
        Excel.Worksheet resumenRehab = new Excel.Worksheet("BP Mensual REHABILITADOS");
        resumenRehab.Columns = new string[] { "Fecha Vuelo", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenRehab.WidthColumns = new int[] { 60, 60, 80, 80, 100, 70 };
        resumenRehab.DataFields = new string[] { "Fch_Vuelo", "Num_Vuelo", "Dsc_Vuelo", "Tip_Pasajero", "Dsc_Trasbordo", "Cantidad" };
        resumenRehab.Source = dt_consulta_r_res;
        #endregion

        #region BP Mensual ANULADOS
        Excel.Worksheet resumenAnul = new Excel.Worksheet("BP Mensual ANULADOS");
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

        Workbook.Worksheets = new Excel.Worksheet[] { resumenUsado, resumenRehab, resumenAnul, resumenNeto };

        return Workbook.Save();

    }
}

