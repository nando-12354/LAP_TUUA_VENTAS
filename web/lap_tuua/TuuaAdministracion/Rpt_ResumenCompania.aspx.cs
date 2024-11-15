using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text;

public partial class Rpt_ResumenCompania : System.Web.UI.Page
{
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Administracion objListaTipoTicket = new BO_Administracion();
    DataTable dt_tipoticket = new DataTable();
    DataTable dt_allcompania = new DataTable();
    DataTable dt_consulta = new DataTable();

    UIControles objCargaCombo = new UIControles();
    protected Hashtable htLabels;
    bool Flg_Error;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sMaxGrilla;
    bool isFull;

    #region Event - Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblFechaInicial.Text = (String)htLabels["reporteDetalleCompania.lblFechaInicial.Text"];
                this.lblFechaFinal.Text = (String)htLabels["reporteDetalleCompania.lblFechaFinal.Text"];
                this.btnConsultar.Text = (String)htLabels["reporteDetalleCompania.btnConsultar.Text"];
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
            this.txtFechaInicio.Text = DateTime.Now.ToShortDateString();
            this.txtFechaFin.Text = DateTime.Now.ToShortDateString();

            //CargarDataReporte();
            SaveFiltros();
            BindPagingGrid();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
            if (isFull) { CargarDataResumen(); }
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
        //CargarDataReporte();
        //falta validar formulario
        SaveFiltros();
        BindPagingGrid();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
        if (isFull) { CargarDataResumen(); }
    }
    #endregion

    public void CargarDataReporte()
    {
        /*try
        {
            string FechaDesde = Fecha.convertToFechaSQL2(this.txtFechaInicio.Text);
            string FechaHasta = Fecha.convertToFechaSQL2(this.txtFechaFin.Text);
            string sHoraDesde = Fecha.convertToHoraSQL(this.txtHoraDesde.Text);
            string sHoraHasta = Fecha.convertToHoraSQL(this.txtHoraHasta.Text);

            dt_consulta = objReporte.obtenerDetalleVentaCompania(FechaDesde, FechaHasta, sHoraDesde, sHoraHasta);

            if (dt_consulta.Rows.Count == 0)
            {
                crptvResumenCompania.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                lblMensajeErrorData.Text = "";
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptDetalleVentaCompania.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pHoraDesde", this.txtHoraDesde.Text);
                obRpt.SetParameterValue("pHoraHasta", this.txtHoraHasta.Text);
                obRpt.SetParameterValue("pFechaDesde", this.txtFechaInicio.Text);
                obRpt.SetParameterValue("pFechaHasta", this.txtFechaFin.Text);

                crptvResumenCompania.Visible = true;
                crptvResumenCompania.ReportSource = obRpt;
                crptvResumenCompania.DataBind();
            }
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
        }*/
    }

    public void CargarDataResumen()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "2");

        if (dtReportResumen.Rows.Count == 0)
        {
            grvDataResumen.Visible = false;
            lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            grvDataResumen.DataSource = dtReportResumen;
            grvDataResumen.DataBind();
        }
    }

    //------------------------------------------------------------------------------
    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtFechaInicio.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtFechaFin.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(this.txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(this.txtHoraHasta.Text)));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;
    }
    #endregion

    #region Dynamic data query
    private void BindPagingGrid()
    {
        this.lblFechaEstadistico.Text = "";
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvResumenCompania.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvResumenCompania.PageIndex, grvResumenCompania.PageSize, grvResumenCompania.OrderBy);

        htLabels = LabelConfig.htLabels;
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
                this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
            grvResumenCompania.DataSource = null;
            grvResumenCompania.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            string fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");
            this.lblFechaEstadistico.Text = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
            //grvTicketUsados.Visible = true;
            grvResumenCompania.DataSource = dt_consulta;
            grvResumenCompania.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvResumenCompania.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.isFull = true;
        }
    }

    private int GetRowCount()
    {
        int count = 0;

        DataTable dt_consulta = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "1");
        if (dt_consulta.Columns.Contains("TotRows"))
        {
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
            lblTotalRows.Value = count.ToString();
        }
        else
            lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        //ValidarTamanoGrilla();
        //Session["sortExpressionTicketBPXFecha"] = sortExpression;
        DataTable dt_consulta = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "0");
        ViewState["tablaTicket"] = dt_consulta;
        return dt_consulta;
    }
    #endregion

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objConsulta.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    #region Paginacion
    protected void grvResumenCompania_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //SaveFiltros();
        grvResumenCompania.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }
    #endregion
    protected void grvDataResumen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 2;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen por Cantidades Vendidas:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
    
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=ResumenCompania.xls");
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
        DataTable dt_resumen = new DataTable();
        string fechaEstadistico = "";
        htLabels = LabelConfig.htLabels;
        RecuperarFiltros();

        try
        {
            dt_consulta = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "0");
            dt_resumen = objReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "2");
            fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");
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

        Excel Workbook = new Excel();
        Excel.Worksheet resumenCompania = new Excel.Worksheet("Reporte Resumen Compania");
        resumenCompania.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        resumenCompania.Columns = new string[] { "Fecha Venta", "Aerolinea", "Nro. Vuelo", "Tipo Documento", "Vendido", "Usado", "Emitido", "Rehabilitado", "Vencido" };
        resumenCompania.WidthColumns = new int[] { 70, 220, 70, 70, 70, 70, 70, 80, 70, 70 };
        resumenCompania.DataFields = new string[] { "Fecha_Venta", "Dsc_Compania", "Num_Vuelo", "Tipo_Documento", "Vendido", "Usado", "Emitido", "Rehabilitado", "Vencido" };
        resumenCompania.Source = dt_consulta;

        Excel.Worksheet resumen = new Excel.Worksheet("Resumen");
        resumen.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        resumen.Columns = new string[] { "Tipo Documento", "Cantidad" };
        resumen.WidthColumns = new int[] { 100, 100 };
        resumen.DataFields = new string[] { "Dsc_Campo", "Cantidad", };
        resumen.Source = dt_resumen;

        Workbook.Worksheets = new Excel.Worksheet[] { resumenCompania, resumen };

        return Workbook.Save();
    }
}

