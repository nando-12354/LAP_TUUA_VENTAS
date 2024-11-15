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
using System.Globalization;

public partial class Rpt_StockTicketContingencia : System.Web.UI.Page
{
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
    bool Flg_Error;

    //Filtros
    string sFechaStock;
    string sTipoTicket;
    bool isFull;

    //Summary
    decimal dNac = 0, dInt = 0, dResTotal = 0;
    int qNac = 0, qInt = 0, qResTotal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblFecha.Text = (String)htLabels["reporteStockTicketContingencia.lblFechaAl.Text"];
                this.lblTipoTicket.Text = (String)htLabels["reporteStockTicketContingencia.lblTipoTicket.Text"];
                this.btnConsultar.Text = htLabels["reporteStockTicketContingencia.btnConsultar.Text"].ToString();
                
                // carga combo Tipo ticket
                BO_Administracion objListaTipoTicket = new BO_Administracion();
                DataTable dt_tipoticket = new DataTable();
                dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
                UIControles objCargaComboTipoTicket = new UIControles();
                objCargaComboTipoTicket.LlenarCombo(ddlTipoTicket, dt_tipoticket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true,false);
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
            this.txtFecha.Text = DateTime.Now.ToShortDateString();

            //CargarDataReporte();
            SaveFiltros();
            BindPagingGrid();
            if (isFull) { CargarDataResumen(); }
        }
        
    }

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        sFechaStock = Fecha.convertToFechaSQL2(this.txtFecha.Text);
        sTipoTicket = ddlTipoTicket.SelectedValue;        
    }
    #endregion


    #region Dynamic data query
    private void BindPagingGrid()
    {
        DataTable dt_consulta = objReporte.obtenerResumenStockTicketContingencia(sTipoTicket, sFechaStock, "0");

        htLabels = LabelConfig.htLabels;
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
                this.lblFechaEstadistico.Text = "";
                this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
            grvStockTicketContingencia.DataSource = null;
            grvStockTicketContingencia.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            string fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");
            this.lblFechaEstadistico.Text = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
            //grvTicketUsados.Visible = true;
            grvStockTicketContingencia.DataSource = dt_consulta;
            grvStockTicketContingencia.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.isFull = true;
        }
    }

    #endregion

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        SaveFiltros();
        BindPagingGrid();
        if (isFull) { CargarDataResumen(); }
    }

    public void CargarDataReporte()
    {
        /*try
        {
            string FechaDesde = Fecha.convertToFechaSQL2(txtFecha.Text);
            string sCodTipoTicket = ddlTipoTicket.SelectedValue;

            dt_consulta = objReporte.obtenerResumenStockTicketContingencia(sCodTipoTicket, FechaDesde);

            if (dt_consulta.Rows.Count == 0)
            {
                crptvStockTicketContingencia.Visible = false;
                this.lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                lblMensajeErrorData.Text = "";
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptStockTicketContingencia.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFecha", txtFecha.Text);
                string sTipoTicket = (sCodTipoTicket == "0") ? "Todos" : ddlTipoTicket.SelectedItem.Text;
                obRpt.SetParameterValue("pTipoTicket", sTipoTicket);
                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                crptvStockTicketContingencia.Visible = true;
                crptvStockTicketContingencia.ReportSource = obRpt;
                crptvStockTicketContingencia.DataBind();
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

    protected void grvDataResumen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 3;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    public void CargarDataResumen()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objReporte.obtenerResumenStockTicketContingencia(sTipoTicket, sFechaStock, "1");
        
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
    protected void grvStockTicketContingencia_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell th = new TableHeaderCell();
            th.ColumnSpan = 4;
            th.BackColor = System.Drawing.Color.White;
            row.Cells.Add(th);

            th = new TableCell();    
            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 2;
            th.Style.Add("font-size", "larger");
            th.Style.Add("font-family", "Verdana");
            th.Style.Add("font-weight", "bold");
            th.Style.Add("color", "#000000");
            th.Text = "Cantidad";
            row.Cells.Add(th);

            th = new TableCell();
            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 2;
            th.Style.Add("font-size", "larger");
            th.Style.Add("font-family", "Verdana");
            th.Style.Add("font-weight", "bold");
            th.Style.Add("color", "#000000");
            th.Text = "Precio";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }
    protected void grvStockTicketContingencia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // add the UnitPrice and QuantityTotal to the running total variables
            qNac += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Num_Ticket_Nac"));
            qInt += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Num_Ticket_Int"));
            dNac += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Monto_Nac"));
            dInt += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Monto_Int"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Totales:";
            // for the Footer, display the running totals
            e.Row.Cells[4].Text = qNac.ToString("d");
            e.Row.Cells[5].Text = qInt.ToString("d");
            e.Row.Cells[6].Text = dNac.ToString("c", CultureInfo.GetCultureInfo("en-US"));
            e.Row.Cells[7].Text = dInt.ToString("c", CultureInfo.GetCultureInfo("en-US"));
            
            e.Row.Cells[4].HorizontalAlign = e.Row.Cells[5].HorizontalAlign =
            e.Row.Cells[6].HorizontalAlign = e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // add the UnitPrice and QuantityTotal to the running total variables
            qResTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Num_Ticket"));
            dResTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Monto"));            
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            // for the Footer, display the running totals
            e.Row.Cells[1].Text = qResTotal.ToString("d");
            e.Row.Cells[2].Text = dResTotal.ToString("c", CultureInfo.GetCultureInfo("en-US"));

            e.Row.Cells[1].HorizontalAlign = e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
}
