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
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
using System.Collections.Generic;

public partial class Cns_StickersAnulados : System.Web.UI.Page
{
    BO_Administracion objAdministracion = new BO_Administracion();
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    
    protected Hashtable htLabels;
    bool Flg_Error;

    string FechaDesde;
    string FechaHasta;
    string sMaxGrilla;

    private string tipoDocumento = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                this.lblDesde.Text = htLabels["mconsultaDetalleturno.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaDetalleturno.lblHasta.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaDetalleturno.btnConsultar.Text"].ToString();
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

            SaveFiltros();
            BindPagingGrid();
            CargarDataResumen();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
            //CargarDataReporte();
        }
     }

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("FechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("FechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
       

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        FechaDesde = newFilterList[0].Valor;
        FechaHasta = newFilterList[1].Valor;
    }
    #endregion

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
        SaveFiltros();
        grvConsultaAnulados.PageIndex = 0;
        BindPagingGrid();
        CargarDataResumen();
    }
    
    public void CargarDataResumen()
    {

        cargarTotales();

        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, "", "2");

        if (dtReportResumen.Rows.Count == 0)
        {
            grvDataResumen.Visible = false;
            lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            /*DataTable dtReportResumen1 = new DataTable();

            dtReportResumen1.Columns.Add("Tipo_Documento");
            dtReportResumen1.Columns.Add("Cantidad");

            dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            dtReportResumen1.Rows[0][0] = "Ticket";
            dtReportResumen1.Rows[0][1] = dtReportResumen.Rows[0]["Ticket"];

            dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            dtReportResumen1.Rows[1][0] = "Boarding";
            dtReportResumen1.Rows[1][1] = dtReportResumen.Rows[0]["BP"];

            dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            dtReportResumen1.Rows[2][0] = "Total";
            dtReportResumen1.Rows[2][1] = dtReportResumen.Rows[0]["Total"];*/

            //grvDataResumen.DataSource = dtReportResumen1;
            
            tipoDocumento = dtReportResumen.Rows[0]["Tipo_Documento"].ToString();
            ViewState["Total_Documento"] = dtReportResumen;
            grvDataResumen.DataSource = dtReportResumen;
            grvDataResumen.DataBind();
           

        }
    }

    private void cargarTotales() {
        DataTable dt_total = new DataTable();
        try
        {
             dt_total = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, "", "3");
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

        ViewState["Total"] = dt_total;
    }

    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        DataTable dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, "", "1");
        grvConsultaAnulados.VirtualItemCount = dt_consulta.Rows.Count;
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
                this.lblFechaEstadistico.Text = "";
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
            grvConsultaAnulados.DataSource = null;
            grvConsultaAnulados.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
            grvDataResumen.Visible = false;
        }
        else
        {
            this.lblMensajeErrorData.Text = "";

            string fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");
            this.lblFechaEstadistico.Text = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;

            //this.lblFechaEstadistico.Text = "Prueba";
            DataTable dtTipoTicket = objAdministracion.ListaTipoTicket();

            //Para eliminar las columnas TipoTicket si existieran
            if (grvConsultaAnulados.Columns.Count > 4)
            {
                int col = grvConsultaAnulados.Columns.Count;
                for (int i = 4; i < col; i++)
                    grvConsultaAnulados.Columns.RemoveAt(4);
            }

            //Agregamos dinamicamente los TipoTicket a la grilla
            foreach (DataRow row in dtTipoTicket.Rows)
            {
                BoundField columna = new BoundField();
                columna.DataField = row["Cod_Tipo_Ticket"].ToString();
                columna.HeaderText = row["Dsc_Tipo_Ticket"].ToString();
                grvConsultaAnulados.Columns.Add(columna);

                int nroColumnas = grvConsultaAnulados.Columns.Count - 1;
                grvConsultaAnulados.Columns[nroColumnas].Visible = false;

                if (Convert.ToInt32(dt_consulta.Compute("Sum(" + row["Cod_Tipo_Ticket"] + ")", "")) > 0)
                    grvConsultaAnulados.Columns[nroColumnas].Visible = true;
            }

           
            grvConsultaAnulados.PageSize = Convert.ToInt32(sMaxGrilla);
            DataView dataView = new DataView(dt_consulta);        
            dataView.Sort = grvConsultaAnulados.OrderBy;
            lblTotalRows.Value = dt_consulta.Rows.Count.ToString();
            grvConsultaAnulados.DataSource = dataView;
            grvConsultaAnulados.DataBind();
            grvDataResumen.Visible = true;
        }
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objConsulta.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

   
    protected void grvConsultaAnulados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvConsultaAnulados.PageIndex = e.NewPageIndex;
        DataTable dt_Resumen = (DataTable)ViewState["Total_Documento"];
        grvDataResumen.DataSource = dt_Resumen;
        tipoDocumento = dt_Resumen.Rows[0]["Tipo_Documento"].ToString();
        grvDataResumen.DataBind();
        BindPagingGrid();
    }

    protected void grvDataResumen_RowCreated(object sender, GridViewRowEventArgs e)
    {
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

    protected void grvConsultaAnulados_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dt_Resumen = (DataTable)ViewState["Total_Documento"];
        grvDataResumen.DataSource = dt_Resumen;
        tipoDocumento = dt_Resumen.Rows[0]["Tipo_Documento"].ToString();
        grvDataResumen.DataBind();
        BindPagingGrid();
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tipoDocumento != drv["Tipo_Documento"].ToString())
            {
                // Calculamos los totales por documento
                DataTable dt_Total = (DataTable)ViewState["Total"];

                object total = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();

                    #region fila total
                    cell.ColumnSpan = this.grvDataResumen.Columns.Count - 3;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total"; //"Total";//Total documento
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

                    //Cantidad
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");
                    span = new HtmlGenericControl("span");
                    span.InnerHtml = total.ToString();
                    cell.Controls.Add(span);
                    row.Cells.Add(cell);


                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);

                    #endregion
                }
                tipoDocumento = drv["Tipo_Documento"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por documento
            
                DataTable dt_Total = (DataTable)ViewState["Total"];

                object total = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();

                    #region fila total
                    cell.ColumnSpan = this.grvDataResumen.Columns.Count - 3;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total"; //"Total";//Total documento
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

                    //Cantidad
                    cell = new TableCell();
                    cell.ColumnSpan = 1;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");
                    span = new HtmlGenericControl("span");
                    span.InnerHtml = total.ToString();
                    cell.Controls.Add(span);
                    row.Cells.Add(cell);


                    tbl.Rows.AddAt(tbl.Rows.Count-1, row);

                    #endregion
                }
        }
    }


    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=RptTicketBP_Anulados.xls");
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
        DataTable dt_resumen_u = new DataTable();
       // DataTable dtReportResumen1 = new DataTable();
        string fechaEstadistico = "";
        htLabels = LabelConfig.htLabels;

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta_u = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, "", "1");

            //DataTable dtReportResumen = new DataTable();
            dt_resumen_u = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, "", "2");

            fechaEstadistico = objConsulta.obtenerFechaEstadistico("0");


            //dtReportResumen1.Columns.Add("Tipo_Documento");
            //dtReportResumen1.Columns.Add("Cantidad",typeof(Int32));

            //dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            //dtReportResumen1.Rows[0][0] = "Ticket";
            //dtReportResumen1.Rows[0][1] = dt_resumen_u.Rows[0]["Ticket"];

            //dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            //dtReportResumen1.Rows[1][0] = "Boarding";
            //dtReportResumen1.Rows[1][1] = dt_resumen_u.Rows[0]["BP"];

            //dtReportResumen1.Rows.Add(dtReportResumen1.NewRow());
            //dtReportResumen1.Rows[2][0] = "Total";
            //dtReportResumen1.Rows[2][1] = dt_resumen_u.Rows[0]["Total"];

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

        #region Consulta Ticket/BP Anulados
        Excel.Worksheet Consulta = new Excel.Worksheet("Rpt_TicketBP_DiaMes");
        Consulta.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        string[] columnas = new string[16];
        columnas[0] = "Fecha Anul.";
        columnas[1] = "Modalidad";
        columnas[2] = "Usuario";
        columnas[3] = "Motivo";
        int[] tamano = new int[16];
        tamano[0] = 60;
        tamano[1] = 120; 
        tamano[2] = 80; 
        tamano[3] = 130;
        string[] datafields = new string[16];
        datafields[0] = "@Fch_Resumen";
        datafields[1] = "Nom_Modalidad";
        datafields[2] = "Cta_Usuario";
        datafields[3] = "Dsc_Motivo";
        int i=4;


        DataTable dtTipoTicket = objAdministracion.ListaTipoTicket();
        foreach (DataRow row in dtTipoTicket.Rows)
        {
            if (Convert.ToInt32(dt_consulta_u.Compute("Sum(" + row["Cod_Tipo_Ticket"] + ")", "")) > 0)
            {
                columnas[i] = row["Dsc_Tipo_Ticket"].ToString();
                tamano[i] = 100;
                datafields[i] = row["Cod_Tipo_Ticket"].ToString();
            }
            else
            {
                columnas[i] = "";
                tamano[i] = 0;
                datafields[i] = row["Cod_Tipo_Ticket"].ToString();
            }
            i++;
        }


        Consulta.Columns = columnas;
        Consulta.WidthColumns = tamano;
        Consulta.DataFields = datafields;
        Consulta.Source = dt_consulta_u;
        #endregion

        #region Resumen Tickets/BP Anulados
        /*Excel.Worksheet Resumen = new Excel.Worksheet("Resumen");
        Resumen.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        Resumen.Columns = new string[] { "Tipo Documento", "Cantidad" };
        Resumen.WidthColumns = new int[] { 100, 80 };
        Resumen.DataFields = new string[] { "Tipo_Documento", "Cantidad" };
        Resumen.Source = dtReportResumen1;*/
        
        DataTable dtResumenTotal = new DataTable();
        int rows = dt_resumen_u.Rows.Count;
        DataRow fila;

        // Calculamos los totales por documento
        DataTable dt_Total = (DataTable)ViewState["Total"];
        tipoDocumento = dt_resumen_u.Rows[0]["Tipo_Documento"].ToString();

        dtResumenTotal = dt_resumen_u.Clone();
        int cont = 0;

        for (int k = 0; k < rows; k++)
        {
            if (tipoDocumento != dt_resumen_u.Rows[k]["Tipo_Documento"].ToString())
            {
                if (k == rows - 1) //es la ultima posicion
                {
                    fila = dt_resumen_u.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    string documento = dt_resumen_u.Rows[k]["Tipo_Documento"].ToString();
                    object totalD = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Total"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k+2]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k+2]["Cantidad"] = totalD.ToString();
                }
                else {
                    //row = dt_resumen_u.Rows[i + 1];
                    //dtResumenTotal.Rows.Add(row);
                    if (cont == 0)
                    {
                        //adicionando subtotal documento previo
                        object totalDt = dt_Total.Select("Tip_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();
                        //total se encontrara en la posicion i dtResumenTotal.row(total)[i]

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k]["Tip_Vuelo"] = "Total";
                        dtResumenTotal.Rows[k]["Cantidad"] = totalDt.ToString();

                        //adicionando el documento actual
                        fila = dt_resumen_u.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }else{
                        //adicionando el documento actual
                        fila = dt_resumen_u.Rows[k];
                        dtResumenTotal.ImportRow(fila);   
                    }
                    cont++;
                }     
            }
            else {

                if (k == rows - 1)//ultima posicion en caso haya solo un tipo de documento
                {
                    fila = dt_resumen_u.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    string documento = dt_resumen_u.Rows[k]["Tipo_Documento"].ToString();
                    object totalD = dt_Total.Select("Tip_Documento = '" + documento + "'")[0]["Total"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Cantidad"] = totalD.ToString();
                }
                else {
                    fila = dt_resumen_u.Rows[k];
                    dtResumenTotal.ImportRow(fila);
                }
                
            }
            
        }

        Excel.Worksheet Resumen = new Excel.Worksheet("Resumen");
        Resumen.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        Resumen.Columns = new string[] { "Tipo Documento", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        Resumen.WidthColumns = new int[] { 100, 100, 100, 100, 80 };
        Resumen.DataFields = new string[] { "Tipo_Documento", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo", "Cantidad" };
        Resumen.Source = dtResumenTotal;

        #endregion

            Workbook.Worksheets = new Excel.Worksheet[] { Consulta, Resumen };

        return Workbook.Save();

    }
}
