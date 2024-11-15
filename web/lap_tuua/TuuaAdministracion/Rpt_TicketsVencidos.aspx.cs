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
using System.Collections.Generic;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class Rpt_TicketsVencidos : System.Web.UI.Page
{
    //Capa Control
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Administracion objAdministracion = new BO_Administracion();
    //DropDownList
    UIControles objControles = new UIControles();

    DataTable dt_consulta = new DataTable();

    //Labels
    protected Hashtable htLabels;
    //Error
    bool Flg_Error;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sTipoTicket;
    //Capacidad Grilla
    string sMaxGrilla;

    #region Event - Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;

        if (!IsPostBack)
        {
            try
            {
                lblTipoTicket.Text = htLabels["rptTicketVencidos.lblTipoTicket.Text"].ToString();
                lblDesde.Text = htLabels["rptTicketVencidos.lblDesde.Text"].ToString();
                lblHasta.Text = htLabels["rptTicketVencidos.lblHasta.Text"].ToString();
                btnConsultar.Text = htLabels["rptTicketVencidos.btnConsultar.Text"].ToString();
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

            //CargarDataReporte();
            SaveFiltros();
            BindPagingGrid();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
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
        //Agregar
        SaveFiltros();
        BindPagingGrid();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
    }

    protected void grvResumenVencidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTipoDocumento")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            // Retrieve the row that contains the button clicked 
            // by the user from the Rows collection.
            GridViewRow row = this.grvResumenVencidos.Rows[rowIndex];

            LinkButton addButton = (LinkButton)row.Cells[0].FindControl("codTipoDocumento");
            //ConsDetTicket1.Inicio(addButton.Text + "-Cns");
            ConsDetTicket1.Inicio(addButton.Text);
        }
    }


    #endregion

    public void CargarCombo()
    {
        try
        {
            DataTable dt_TipoTicket = new DataTable();
            dt_TipoTicket = objAdministracion.ListaTipoTicket();
            objControles.LlenarCombo(ddlTipoTicket, dt_TipoTicket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true, false);
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

    public void CargarDataReporte()
    {
        /*try
        {
            string FechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            string FechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
            string sTipoTicket = this.ddlTipoTicket.SelectedValue;
            
            dt_consulta = objReporte.ConsultarTicketVencidos(FechaDesde, FechaHasta, sTipoTicket);
            

            if (dt_consulta.Rows.Count == 0)
            {
                this.crptvTicketVencidos.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                //---------BUSCANDO DATA EN ARCHIVAMIENTO----------
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                    crptvTicketVencidos.ReportSource = null;
                    crptvTicketVencidos.DataBind();
                }
                else
                {
                    lblMensajeErrorData.Text = "";
                    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    //Pintar el reporte                 
                    obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptTicketVencidos.rpt");
                    //Poblar el reporte con el datatable
                    obRpt.SetDataSource(dt_consulta);
                    obRpt.SetParameterValue("sFechaInicio", txtDesde.Text);
                    obRpt.SetParameterValue("sFechaFinal", txtHasta.Text);
                    obRpt.SetParameterValue("sTipoTicket", this.ddlTipoTicket.SelectedItem.Text);
                    //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                    this.crptvTicketVencidos.Visible = true;
                    this.crptvTicketVencidos.ReportSource = obRpt;
                    this.crptvTicketVencidos.DataBind();
                }
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

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvResumenVencidos.VirtualItemCount = GetRowCount();
        DataTable dt_reporte = GetDataPage(grvResumenVencidos.PageIndex, grvResumenVencidos.PageSize, grvResumenVencidos.OrderBy);

        htLabels = LabelConfig.htLabels;
        if (dt_reporte.Rows.Count < 1)
        {
            try
            {
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
            grvResumenVencidos.DataSource = null;
            grvResumenVencidos.DataBind();

            this.grvDataResumen.DataSource = null;
            this.grvDataResumen.DataBind();

            this.lblTotal.Text = "";
            this.lblTotalRows.Value = "";
        }
        else
        {
            if (dt_reporte.Columns[0].ColumnName == "MsgError")
            {
                this.lblMensajeErrorData.Text = dt_reporte.Rows[0]["Descripcion"].ToString();
                lblTotal.Text = "";
                lblTotalRows.Value = "";
                grvResumenVencidos.DataSource = null;
                grvResumenVencidos.DataBind();
                return;
            }
            htLabels = LabelConfig.htLabels;
            //grvTicketUsados.Visible = true;
            grvResumenVencidos.DataSource = dt_reporte;
            grvResumenVencidos.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvResumenVencidos.DataBind();
            this.lblMensajeErrorData.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + grvResumenVencidos.VirtualItemCount;
            this.lblTotalRows.Value = grvResumenVencidos.VirtualItemCount.ToString();

            Resumen();
        }
    }

    private int GetRowCount()
    {
        int count = 0;

        DataTable dt_consulta = objReporte.ListarTicketVencidosPagin(sFechaDesde, sFechaHasta, sTipoTicket, null, 0, 0, "1");
        if (dt_consulta.Columns.Contains("TotRows"))
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
        else
            this.lblMensajeErrorData.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        //ValidarTamanoGrilla();
        //Session["sortExpressionTicketBPXFecha"] = sortExpression;
        DataTable dt_consulta = objReporte.ListarTicketVencidosPagin(sFechaDesde, sFechaHasta, sTipoTicket, sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "0");
        ViewState["tablaTicket"] = dt_consulta;
        return dt_consulta;
    }

    private void Resumen() {
        DataTable dt_resumen = objReporte.ListarTicketVencidosPagin(sFechaDesde, sFechaHasta, sTipoTicket, "", 0, 0, "2");

        //string total = lblTotalRows.Value;
      
        //DataRow fila = dt_resumen.NewRow();

        //object[] rowArray = new object[5];
        //rowArray[0] = "Total";
        //rowArray[4] = total;

        //fila.ItemArray = rowArray;
        //dt_resumen.Rows.Add(fila);   
        this.grvDataResumen.DataSource = dt_resumen;
        grvDataResumen.DataBind();
    }

    #endregion

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtHasta.Text)));
        filterList.Add(new Filtros("sTipoTicket", this.ddlTipoTicket.SelectedValue));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sTipoTicket = newFilterList[2].Valor;
    }
    #endregion

    #region Paginacion
    protected void grvResumenVencidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvResumenVencidos.PageIndex = e.NewPageIndex;
        BindPagingGrid();
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

    protected void grvDataResumen_RowCreated(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 5;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            // for the Footer, display the running totals
            e.Row.Cells[4].Text = lblTotalRows.Value;

            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TicketVencidos.xls");
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
            dt_consulta = objReporte.ListarTicketVencidosPagin(sFechaDesde, sFechaHasta, sTipoTicket, null, 0, 0, "0");

            dt_resumen = objReporte.ListarTicketVencidosPagin(sFechaDesde, sFechaHasta, sTipoTicket, null, 0, 0, "2");
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

        #region Reporte Tickets Vencidos
        Excel.Worksheet ticketVencidos = new Excel.Worksheet("Reporte Tickets Vencidos");

        ticketVencidos.Columns = new string[] { "Nro. Ticket", "Tipo Ticket", "Aerolinea", "Fecha Vencimiento", "Dias Vencido" };
        ticketVencidos.WidthColumns = new int[] { 120, 160, 220, 110, 80 };
        ticketVencidos.DataFields = new string[] { "Cod_Numero_Ticket", "TipoTicket", "Dsc_Compania", "Fch_Vencimiento", "DiasVencidos" };

        ticketVencidos.Source = dt_consulta;
        #endregion

        #region RESUMEN
        Excel.Worksheet resumenTV = new Excel.Worksheet("RESUMEN");
        resumenTV.Columns = new string[] {  "Aerolínea", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenTV.WidthColumns = new int[] {  100, 100, 100, 100, 100 };
        resumenTV.DataFields = new string[] {  "Dsc_Compania", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo", "Cantidad" };

        string total = lblTotalRows.Value;

        DataRow fila = dt_resumen.NewRow();

        object[] rowArray = new object[5];
        rowArray[0] = "Total";
        rowArray[4] = total;

        fila.ItemArray = rowArray;
        dt_resumen.Rows.Add(fila);

        resumenTV.Source = dt_resumen;

        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { ticketVencidos, resumenTV };

        return Workbook.Save();
    }

}
