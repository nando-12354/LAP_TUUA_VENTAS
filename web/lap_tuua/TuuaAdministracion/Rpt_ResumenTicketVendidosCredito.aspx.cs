using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

public partial class Rpt_ResumenTicketVendidosCredito : System.Web.UI.Page
{
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Configuracion objCampos = new BO_Configuracion();
    BO_Administracion objListaTipoTicket = new BO_Administracion();

    DataTable dt_tipoticket = new DataTable();
    DataTable dt_allcompania = new DataTable();
    DataTable dt_tipopago = new DataTable();
    DataTable dt_reporte = new DataTable();

    UIControles objCargaCombo = new UIControles();

    protected Hashtable htLabels;
    bool Flg_Error;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sTipoTicket;
    string sTipoPago;
    string sCompania;
    string sVuelo;
    string sMaxGrilla;
    bool isFull;

    //Summary
    decimal dMonTotal = 0;
    int iNumTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblFechaInicial.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblFechaInicial.Text"];
                lblFechaFinal.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblFechaFinal.Text"];
                lblTipoTicket.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblTipoTicket.Text"];
                lblTipoPago.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblTipoPago.Text"];
                this.btnConsultar.Text = htLabels["reporteResumenTicketsVendidosCredito.btnConsultar.Text"].ToString();
                lblNumeroVuelo.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblNumeroVuelo.Text"];
                lblTipoAerolinea.Text = (String)htLabels["reporteResumenTicketsVendidosCredito.lblTipoAerolinea.Text"];

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

            CargarCombo();

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

    public void CargarCombo()
    {
        try
        {
            dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
            objCargaCombo.LlenarCombo(ddlTipoTicket, dt_tipoticket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true, false);
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaCombo.LlenarCombo(ddlTipoAerolinea, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);
            dt_tipopago = objCampos.ObtenerListaDeCampo("TipoPago", "");

            //ListItem caract1 = new ListItem();
            //ListItem caract2 = new ListItem();
            //ListItem caract3 = new ListItem();
            //caract1.Text="Efectivo";
            //caract1.Value = "E"; 
            //caract2.Text = "Credito";
            //caract2.Value = "X";
            //caract3.Text = "<Todos>";
            //caract3.Value = "0";
            //ddlTipoPago.Items.Add(caract1);
            //ddlTipoPago.Items.Add(caract2);
            //ddlTipoPago.Items.Add(caract3);
            //ddlTipoPago.SelectedValue = "0";

            objCargaCombo.LlenarCombo(ddlTipoPago, dt_tipopago, "Cod_Campo", "Dsc_Campo", true, false);
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

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtFechaInicio.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtFechaFin.Text)));
        filterList.Add(new Filtros("sTipoTicket", this.ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sTipoPago", this.ddlTipoPago.SelectedValue));
        filterList.Add(new Filtros("sCompania", this.ddlTipoAerolinea.SelectedValue));
        filterList.Add(new Filtros("sVuelo", this.txtNumeroVuelo.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sTipoTicket = newFilterList[2].Valor;
        sTipoPago = newFilterList[3].Valor;
        sCompania = newFilterList[4].Valor;
        sVuelo = newFilterList[5].Valor;
    }

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvResumenCredito.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvResumenCredito.PageIndex, grvResumenCredito.PageSize, grvResumenCredito.OrderBy);

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
            grvResumenCredito.DataSource = null;
            grvResumenCredito.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            //grvTicketUsados.Visible = true;
            grvResumenCredito.DataSource = dt_consulta;
            grvResumenCredito.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvResumenCredito.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.isFull = true;
        }
    }

    private int GetRowCount()
    {
        int count = 0;

        DataTable dt_consulta = objReporte.ListarResumenTicketVendidosCreditoPagin(sFechaDesde, sFechaHasta, sTipoTicket, sVuelo, sCompania, sTipoPago, "0", null, 0, 0, "1");

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
        DataTable dt_consulta = objReporte.ListarResumenTicketVendidosCreditoPagin(sFechaDesde, sFechaHasta, sTipoTicket, sVuelo, sCompania, sTipoPago, "0", sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "0");
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

    public void CargarDataResumen()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objReporte.ListarResumenTicketVendidosCreditoPagin(sFechaDesde, sFechaHasta, sTipoTicket, sVuelo, sCompania, sTipoPago, "1", null, 0, 0, "2");
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

    #region Paginacion
    protected void grvResumenCredito_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvResumenCredito.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvResumenCredito_Sorting(object sender, GridViewSortEventArgs e)
    {
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
            th.ColumnSpan = 7;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            iNumTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
            dMonTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Monto"));

        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total:";
            // for the Footer, display the running totals
            e.Row.Cells[5].Text = iNumTotal.ToString("d");
            e.Row.Cells[6].Text = dMonTotal.ToString("c", CultureInfo.GetCultureInfo("en-US"));


            e.Row.Cells[5].HorizontalAlign = e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TicketsVendidosCredito.xls");
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

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objReporte.ListarResumenTicketVendidosCreditoPagin(sFechaDesde,
                                                                sFechaHasta,
                                                                sTipoTicket,
                                                                sVuelo,
                                                                sCompania,
                                                                sTipoPago,
                                                                "0",
                                                                null, 0, 0, "0");

            dt_resumen = objReporte.ListarResumenTicketVendidosCreditoPagin(sFechaDesde, 
                                                                            sFechaHasta, 
                                                                            sTipoTicket, 
                                                                            sVuelo, 
                                                                            sCompania, 
                                                                            sTipoPago, 
                                                                            "1", null, 0, 0, "2");
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

        #region Consulta
        Excel.Worksheet Consulta = new Excel.Worksheet("ResumenTicketCredito");
        Consulta.Columns = new string[] { "Fecha Venta", "Aerolínea", "Nro. Vuelo", "Tipo Ticket", "Cantidad","Código Venta Masiva", "Representante" };
        Consulta.WidthColumns = new int[] { 80, 150, 60, 130, 55,100, 120 };
        Consulta.DataFields = new string[] { "@Fecha_Venta", "Dsc_Compania", "Dsc_Num_Vuelo", "Dsc_Tipo_Ticket", "Can_Venta","Cod_Venta_Masiva", "Dsc_Repte" };
        Consulta.Source = dt_consulta;
        #endregion

        #region Resumen Consulta
        Excel.Worksheet Resumen = new Excel.Worksheet("Resumen");
        Resumen.Columns = new string[] { "Aerolínea","Tipo Venta","Tipo Vuelo","Tipo Pasajero","Tipo Trasbordo", "Cantidad", "Importe Vendido" };
        Resumen.WidthColumns = new int[] { 150,100,100,100,100,60, 80 };
        Resumen.DataFields = new string[] { "Dsc_Compania", "Tip_Venta","Tip_Vuelo","Tip_Pasajero","Tip_Trasbordo","Cantidad", "Monto" };
        Resumen.Source = dt_resumen;
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { Consulta, Resumen };

        return Workbook.Save();

    }


    
}
