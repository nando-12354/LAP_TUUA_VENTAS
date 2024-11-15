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
using System.Collections.Generic;
using System.Text;

public partial class Rpt_DetalleLineaVuelo : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    DataTable dt_resumen = new DataTable();
    DataTable dt_allcompania = new DataTable();
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_parametroTurno = new DataTable();
    UIControles objCargaComboCompania = new UIControles();
    Int32 valorMaxGrilla;

    private string tmpCompania = "";
    private string tipoDocumento = "";

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sCompania;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                this.lblDesde.Text = htLabels["mconsultaTicketFecha.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaTicketFecha.lblHasta.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaDetalleturno.btnConsultar.Text"].ToString();
                this.lblCompania.Text = htLabels["mconsultaTicketFecha.lblCompania.Text"].ToString();
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
            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();

            CargarCombo();

            SaveFiltros();
            BindPagingGrid();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
            cargarDataResumen();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        //CargarDataReporte();

        SaveFiltros();
        //cargarSubTotales();
        BindPagingGrid();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
        cargarDataResumen();
    }


    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sCompania", ddlCompania.SelectedValue));

        ViewState.Add("Filtros", filterList);
    }

    public void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sCompania = newFilterList[2].Valor;
    }


    protected int GetMaximoReporte()
    {
        int iMaxReporte = 0;    
        DataTable dt_max = objConsulta.ListarParametros("LR");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt32(dt_max.Rows[0].ItemArray.GetValue(4).ToString());            
        
        return iMaxReporte;
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

    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general    
        BO_Consultas objParametro = new BO_Consultas();  
        dt_parametroTurno = objParametro.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    private void cargarSubTotales()
    {
        try
        {
            //RecuperarFiltros();
            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "1", "1");
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

        ViewState["SubTotales"] = dt_consulta;
    }

    private void cargarSubTotalesResumen()
    {
        try
        {
            //RecuperarFiltros();
            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "2", "2");
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

        ViewState["SubTotalesResumen"] = dt_consulta;
    }

    private void cargarDataResumen()
    {
        RecuperarFiltros();

        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "0", "0");
        if (dtReportResumen.Rows.Count == 0)
        {
            grvResumen.Visible = false;
            //lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            grvResumen.Visible = true;
            //ViewState["dtResumen"] = dtReportResumen;
            tipoDocumento = dtReportResumen.Rows[0]["Tip_Documento"].ToString();
            ViewState["Resumen"] = dtReportResumen;
            grvResumen.DataSource = dtReportResumen;
            grvResumen.DataBind();
        }
    }

    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        cargarSubTotales();
        cargarSubTotalesResumen();
        grvDetalleLineaVuelo.VirtualItemCount = GetRowCount();

        dt_consulta = GetDataPage(grvDetalleLineaVuelo.PageIndex, grvDetalleLineaVuelo.PageSize, grvDetalleLineaVuelo.OrderBy);

        if (dt_consulta.Rows.Count == 0)
        {
            grvDetalleLineaVuelo.Visible = false;
            this.lblFechaEstadistico.Text = "";
            lblMensajeErrorData.Text = "La búsqueda no retorna resultado";
            lblTotal.Text = "";
            lblTotalRows.Value = "";
        }
        else
        {
            lblMensajeErrorData.Text = "";

            string fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");
            this.lblFechaEstadistico.Text = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico; 
            grvDetalleLineaVuelo.Visible = true;
            grvDetalleLineaVuelo.DataSource = dt_consulta;
            tmpCompania = dt_consulta.Rows[0]["Dsc_Compania"].ToString();
            grvDetalleLineaVuelo.PageSize = valorMaxGrilla;
            grvDetalleLineaVuelo.DataBind();

            lblTotal.Text = "Total de Registros:" + grvDetalleLineaVuelo.VirtualItemCount;
            lblTotalRows.Value = grvDetalleLineaVuelo.VirtualItemCount.ToString();
        }
    }

    #region Dynamic data query
    private int GetRowCount()
    {
        int count = 0;
        try
        {
            dt_resumen = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde, sFechaHasta, sCompania,null,0,0,"0","1");

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
            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    sortExpression,
                                                                    pageIndex, Convert.ToInt32(valorMaxGrilla), "1", "0");
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

    //public void CargarDataReporte()
    //{
    //    try
    //    {
    //        string FechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
    //        string FechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
    //        string CodCompania = this.ddlCompania.SelectedValue.Trim();
    //        int iMaxReporte = GetMaximoReporte();

    //        if (Fecha.getFechaElapsed(FechaDesde, FechaHasta) <= iMaxReporte || iMaxReporte < 0) //Validacion Rango Fecha
    //        {
    //            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(FechaDesde, FechaHasta, CodCompania);

    //            if (dt_consulta.Rows.Count == 0)
    //            {
    //                crvDetalleLineaVuelo.Visible = false;
    //                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
    //            }
    //            else
    //            {
    //                lblMensajeErrorData.Text = "";
    //                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    //                //Pintar el reporte                 
    //                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\DetallexLineaVueloImpeso.rpt");
    //                //Poblar el reporte con el datatable
    //                obRpt.SetDataSource(dt_consulta);
    //                obRpt.SetParameterValue("sFechaInicio", txtDesde.Text);
    //                obRpt.SetParameterValue("sFechaFinal", txtHasta.Text);
    //                obRpt.SetParameterValue("sCompania", this.ddlCompania.SelectedItem.Text);
    //                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
    //                crvDetalleLineaVuelo.Visible = true;
    //                crvDetalleLineaVuelo.ReportSource = obRpt;
    //                crvDetalleLineaVuelo.DataBind();
    //            }
    //        }
    //        else {
    //            crvDetalleLineaVuelo.Visible = false;
    //            lblMensajeErrorData.Text = String.Format(htLabels["tuua.general.lblMensajeError1.Text"].ToString(), iMaxReporte);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Flg_Error = true;
    //        ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
    //    }
    //    finally
    //    {
    //        if (Flg_Error)
    //        {
    //            Response.Redirect("PaginaError.aspx");
    //        }
    //    }
    //}

    public void CargarCombo()
    {
        try
        {           
            //Carga combo compañia
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaComboCompania.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);
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
   
    protected void grvDetalleLineaVuelo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtResumenLineaVuelo = (DataTable)ViewState["Resumen"];
        grvDetalleLineaVuelo.PageIndex = e.NewPageIndex;
        tipoDocumento = dtResumenLineaVuelo.Rows[0]["Tip_Documento"].ToString();
        grvResumen.DataSource = dtResumenLineaVuelo;
        grvResumen.DataBind();
        BindPagingGrid();
    }

    protected void grvDetalleLineaVuelo_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }

    protected void grvDetalleLineaVuelo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tmpCompania != drv["Dsc_Compania"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtSubTotales = (DataTable)ViewState["SubTotales"];

                object totalUtil = dtSubTotales.Select("Dsc_Compania = '" + tmpCompania + "'")[0]["Cnt_Utilizada"].ToString();
                object totalVend = dtSubTotales.Select("Dsc_Compania = '" + tmpCompania + "'")[0]["Cnt_Vendida"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila SubTotal
                    cell.ColumnSpan = this.grvDetalleLineaVuelo.Columns.Count - 2;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = tmpCompania;//"TOTAL AEROLÍNEA:";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    //UTILIZADOS
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalUtil = totalUtil.ToString() == "" ? "0" : totalUtil.ToString();
                    span.InnerHtml = totalUtil.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    //VENDIDOS
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");
                    span = new HtmlGenericControl("span");

                    totalVend = totalVend.ToString() == "" ? "0" : totalVend.ToString();
                    span.InnerHtml = totalVend.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);


                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }

                tmpCompania = drv["Dsc_Compania"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtSubTotales = (DataTable)ViewState["SubTotales"];

            object totalUtil = dtSubTotales.Select("Dsc_Compania = '" + tmpCompania + "'")[0]["Cnt_Utilizada"].ToString();
            object totalVend = dtSubTotales.Select("Dsc_Compania = '" + tmpCompania + "'")[0]["Cnt_Vendida"].ToString();
            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila SubTotal
                cell.ColumnSpan = this.grvDetalleLineaVuelo.Columns.Count - 2;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = tmpCompania;//"TOTAL AEROLÍNEA:";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                //UTILIZADOS
                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalUtil = totalUtil.ToString() == "" ? "0" : totalUtil.ToString();
                span.InnerHtml = totalUtil.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                //VENDIDOS
                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");
                span = new HtmlGenericControl("span");

                totalVend = totalVend.ToString() == "" ? "0" : totalVend.ToString();
                span.InnerHtml = totalVend.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);


                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                #endregion
            }
        }
    }

    protected void grvResumen_RowCreated(object sender, GridViewRowEventArgs e)
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
            th.Text = "Resumen:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }

        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //AGREGAMOS EL TOTAL                
        //    try
        //    {
        //        DataTable dt_Total = new DataTable();
        //        dt_Total = (DataTable)ViewState["dtResumen"];
        //        Int64 totalUtil = Convert.ToInt64(dt_Total.Compute("Sum(Cnt_Utilizada)", "").ToString());
        //        Int64 totalVend = Convert.ToInt64(dt_Total.Compute("Sum(Cnt_Vendida)", "").ToString());
        //        e.Row.Cells[0].Text = "TOTAL";
        //        e.Row.Cells[1].Text = totalUtil.ToString();
        //        e.Row.Cells[2].Text = totalVend.ToString();
        //    }
        //    catch { }
        //}
    }

    protected void grvResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tipoDocumento != drv["Tip_Documento"].ToString())
            {
                // Calculamos los totales por dia
                DataTable dtSubTotales = (DataTable)ViewState["SubTotalesResumen"];

                object totalUtil = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Utilizada"].ToString();
                object totalVend = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Vendida"].ToString();

                // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                    TableCell cell = new TableCell();

                    #region Fila SubTotal
                    cell.ColumnSpan = this.grvDetalleLineaVuelo.Columns.Count - 5;
                    //cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total";
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Style.Add("background-color", "#CCCCCC");
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.Style.Add("background-color", "#CCCCCC");
                    row.Cells.Add(cell);

                    //UTILIZADOS
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    //cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    span = new HtmlGenericControl("span");

                    totalUtil = totalUtil.ToString() == "" ? "0" : totalUtil.ToString();
                    span.InnerHtml = totalUtil.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                   
                    //VENDIDOS
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                   // cell.Width = Unit.Percentage(100);
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");
                    span = new HtmlGenericControl("span");

                    totalVend = totalVend.ToString() == "" ? "0" : totalVend.ToString();
                    span.InnerHtml = totalVend.ToString();
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);


                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
                    #endregion
                }

                tipoDocumento = drv["Tip_Documento"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por dia
            DataTable dtSubTotales = (DataTable)ViewState["SubTotalesResumen"];

            object totalUtil = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Utilizada"].ToString();
            object totalVend = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Vendida"].ToString();

            // Get a reference to the current row's Parent, which is the Gridview (which happens to be a table)
            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);

                TableCell cell = new TableCell();

                #region Fila SubTotal
                cell.ColumnSpan = this.grvDetalleLineaVuelo.Columns.Count - 5;
                //cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total";
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Style.Add("background-color", "#CCCCCC");
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.ColumnSpan = 1;
                cell.Style.Add("background-color", "#CCCCCC");
                row.Cells.Add(cell);

                //UTILIZADOS
                cell = new TableCell();
                cell.ColumnSpan = 1;
               // cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                span = new HtmlGenericControl("span");

                totalUtil = totalUtil.ToString() == "" ? "0" : totalUtil.ToString();
                span.InnerHtml = totalUtil.ToString();
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                //VENDIDOS
                cell = new TableCell();
                cell.ColumnSpan = 1;
                //cell.Width = Unit.Percentage(100);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");
                span = new HtmlGenericControl("span");

                totalVend = totalVend.ToString() == "" ? "0" : totalVend.ToString();
                span.InnerHtml = totalVend.ToString();
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
        Response.AddHeader("Content-Disposition", "attachment; filename=DetalleLineaVuelo.xls");
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
        DataTable dtReportResumen = new DataTable();
        string fechaEstadistico="";

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "1", "0");

            dtReportResumen = objConsulta.ObtenerDetallexLineaVuelo(sFechaDesde,
                                                                    sFechaHasta,
                                                                    sCompania,
                                                                    null,
                                                                    0, 0, "0", "0");

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

        #endregion

        Excel Workbook = new Excel();

        #region Reporte Detalle Linea Vuelo
        Excel.Worksheet detalleLineaVuelo = new Excel.Worksheet("Reporte Detalle Linea Vuelo");


        detalleLineaVuelo.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        detalleLineaVuelo.Columns= new string[] { "Aerolinea", "Fecha Vuelo", "Nro. Vuelo", "Tipo Documento", "Tipo Ticket", "Utilizados", "Vendidos" };
        

        detalleLineaVuelo.WidthColumns = new int[] { 230, 100, 80, 100, 150, 60, 60 };
        detalleLineaVuelo.DataFields = new string[] { "Dsc_Compania", "Fch_Vuelo", "Dsc_Num_Vuelo", "Tip_Documento", "Dsc_Tipo_Ticket", "Cnt_Utilizada", "Cnt_Vendida" };
        detalleLineaVuelo.Source = dt_consulta;
        #endregion

        #region Resumen Detalle Linea Vuelo
        Excel.Worksheet resumen = new Excel.Worksheet("Resumen");
        resumen.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        resumen.Columns = new string[] { "Tipo Documento", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Utilizados", "Vendidos" };
        resumen.DataFields = new string[] { "Tip_Documento", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo", "Cnt_Utilizada", "Cnt_Vendida" };

        DataTable dtResumenTotal = new DataTable();
        dtResumenTotal = dtReportResumen.Clone();
        int rows = dtReportResumen.Rows.Count;
        DataRow fila;

        // Calculamos los totales por documento
        DataTable dt_Total = (DataTable)ViewState["SubTotalesResumen"];
        tipoDocumento = dtReportResumen.Rows[0]["Tip_Documento"].ToString();
        int cont = 0;
        string documento = "";
        object totalUtil = null;
        object totalVend = null;

        for (int k = 0; k < rows; k++)
        {
            if (tipoDocumento != dtReportResumen.Rows[k]["Tip_Documento"].ToString())
            {
                if (k == rows - 1) //es la ultima posicion
                {
                    fila = dtReportResumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    documento = dtReportResumen.Rows[k]["Tip_Documento"].ToString();
                    totalUtil = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Utilizada"].ToString();
                    totalVend = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Vendida"].ToString();

                    //object totalUtil = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Utilizada"].ToString();
                    //object totalVend = dtSubTotales.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Vendida"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 2]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k + 2]["Cnt_Utilizada"] = totalUtil.ToString();
                    dtResumenTotal.Rows[k + 2]["Cnt_Vendida"] = totalVend.ToString();
                }
                else
                {
                    if (cont == 0) //si es por primera vez
                    {
                        //adicionando subtotal documento previo
                        totalUtil = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Utilizada"].ToString();
                        totalVend = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Cnt_Vendida"].ToString();

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k]["Tip_Vuelo"] = "Total";
                        dtResumenTotal.Rows[k]["Cnt_Utilizada"] = totalUtil.ToString();
                        dtResumenTotal.Rows[k]["Cnt_Vendida"] = totalVend.ToString();

                        //adicionando el documento actual
                        fila = dtReportResumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }
                    else
                    {
                        //adicionando el documento actual
                        fila = dtReportResumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }
                    cont++;
                }
            }
            else
            {
                if (k == rows - 1)//ultima posicion en caso haya solo un tipo de documento
                {
                    fila = dtReportResumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    documento = dtReportResumen.Rows[k]["Tip_Documento"].ToString();
                    totalUtil = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Utilizada"].ToString();
                    totalVend = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Cnt_Vendida"].ToString();

                    //total = dt_Total.Select("Tipo_Documento = '" + documento + "'")[0]["Total"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Cnt_Utilizada"] = totalUtil.ToString();
                    dtResumenTotal.Rows[k + 1]["Cnt_Vendida"] = totalVend.ToString();
                }
                else
                {
                    fila = dtReportResumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);
                }
            }
        }

        resumen.Source = dtResumenTotal;

        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { detalleLineaVuelo, resumen };

        return Workbook.Save();
    }
  
}
