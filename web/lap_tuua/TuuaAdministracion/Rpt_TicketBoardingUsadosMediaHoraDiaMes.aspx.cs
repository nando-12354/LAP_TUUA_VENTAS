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

public partial class Rpt_TicketBoardingUsadosMediaHoraDiaMes : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Reportes objBOReportes = new BO_Reportes();
    private BO_Administracion objBOAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();

    string sMaxGrilla;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sDestino;
    string sTipoDocumento;
    string sTipoRango;
    string sAerolinea;
    string sTipoTicket;
    string sNumVuelo;

    //data
    string tipoDocumento = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblAerolinea.Text = htLabels["rptTicketBoadingUsados.lblAerolinea"].ToString();
                lblDestino.Text = htLabels["rptTicketBoadingUsados.lblDestino"].ToString();
                lblNumeroVuelo.Text = htLabels["rptTicketBoadingUsados.lblNumeroVuelo"].ToString();
                lblTipoDocumento.Text = htLabels["rptTicketBoadingUsados.lblTipoDocumento"].ToString();
                lblTipoTicket.Text = htLabels["rptTicketBoadingUsados.lblTipoTicket"].ToString();
                lblHasta.Text = htLabels["rptTicketBoadingUsados.lblHasta"].ToString();
                lblDesde.Text = htLabels["rptTicketBoadingUsados.rbtnDesde"].ToString();
                chkbTicket.Text = htLabels["rptTicketBoadingUsados.chkbTicket"].ToString();
                chkbBP.Text = htLabels["rptTicketBoadingUsados.chkbBP"].ToString();
                lblTipoRango.Text = htLabels["rptTicketBoadingUsados.lblTipoRango"].ToString();

                this.txtDesde.Text = DateTime.Now.ToShortDateString();
                this.txtHasta.Text = DateTime.Now.ToShortDateString();

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
                    Response.Redirect("PaginaError.aspx");

            }

            CargarCombos();
        }
    }

   
    protected void CargarCombos()
    {

        try
        {
            //Carga combo Tipo Ticket
            DataTable dt_tipoTicket = new DataTable();
            dt_tipoTicket = objBOAdministracion.ListaTipoTicket();
            objCargaCombo.LlenarCombo(this.ddlTipoTicket, dt_tipoTicket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket_Larga", true, false);

            //Carga combo Aerolineas

            DataTable dt_allcompania = new DataTable();
            dt_allcompania = objBOConsultas.listarAllCompania();
            objCargaCombo.LlenarCombo(this.ddlAerolinea, FiltrarAerolinea(dt_allcompania), "Cod_Compania", "Dsc_Compania", true, false);

        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

    }

    private DataTable FiltrarAerolinea(DataTable dtCompania)
    {
        DataTable dest = new DataTable("Result" + dtCompania.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Cod_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);


        DataRow[] foundRowAerolinea = dtCompania.Select("Tip_Compania = '1'");

        if (foundRowAerolinea != null && foundRowAerolinea.Length > 0)
        {
            for (int i = 0; i < foundRowAerolinea.Length; i++)
            {
                dest.Rows.Add(dest.NewRow());
                dest.Rows[i][0] = foundRowAerolinea[i]["Cod_Compania"].ToString();
                dest.Rows[i][1] = foundRowAerolinea[i]["Dsc_Compania"].ToString();
            }
        }

        dest.AcceptChanges();
        return dest;
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

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        SaveFiltros();
        BindPagingGrid();
        CargarDataResumen();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL2(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL2(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sDestino", txtDestino.Text));
        if (chkbTicket.Checked)
        {
            if (chkbBP.Checked) filterList.Add(new Filtros("sTipoDocumento", "O"));
            else filterList.Add(new Filtros("sTipoDocumento", "T"));
        }
        else
        {
            if (chkbBP.Checked) filterList.Add(new Filtros("sTipoDocumento", "B"));
            else filterList.Add(new Filtros("sTipoDocumento", "N"));
        }
        
        filterList.Add(new Filtros("sTipoRango", ddlTipoRango.SelectedValue));
        filterList.Add(new Filtros("sAerolinea", ddlAerolinea.SelectedValue));
        filterList.Add(new Filtros("sTipoTicket", ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", txtNroVuelo.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;
        sDestino = newFilterList[4].Valor;
        sTipoDocumento = newFilterList[5].Valor;
        sTipoRango = newFilterList[6].Valor;
        sAerolinea = newFilterList[7].Valor;
        sTipoTicket = newFilterList[8].Valor;
        sNumVuelo = newFilterList[9].Valor;
    }

    public string convertToHoraSQL(string hora)
    {
        if (hora != null && hora.Length == 5 && hora != "__:__")
        {

            return hora.Substring(0, 2) +
                   hora.Substring(3, 2) +
                   "00";
        }
        else
        {
            return "";
        }
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objBOConsultas.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    public void CargarDataResumen()
    {
        //obteniendo el total por tipo de documento
        DataTable dtTotal = new DataTable();
        dtTotal = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                            sNumVuelo,
                                                                            sTipoDocumento,
                                                                            sTipoTicket,
                                                                            sTipoRango,
                                                                            sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sDestino,
                                                                            null, 0, 0, "0", "1", "0");

        Int64 registros = Convert.ToInt64(dtTotal.Compute("Sum(Total)", ""));
        
        if (registros == 0)
        {
            lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else { 
        //creamos una vista del Total
            ViewState["Total"] = dtTotal;

        //obteniendo Resumen
         DataTable dtResumen = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                                sNumVuelo,
                                                                                sTipoDocumento,
                                                                                sTipoTicket,
                                                                                sTipoRango,
                                                                                sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sDestino,
                                                                                null, 0, 0, "0", "2", "0");

         tipoDocumento = dtResumen.Rows[0]["Tipo_Documento"].ToString();
         ViewState["Resumen"] = dtResumen;
            grvDataResumen.DataSource = dtResumen;
            grvDataResumen.DataBind();
        }

        

        //Int64 registros = Convert.ToInt64(dtReportResumen.Compute("Sum(Total)", ""));
       /* if (registros == 0)
        {
            lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }*/
       /* else
        {
            if (sTipoDocumento == "T")
            {
                int filas = dtReportResumen.Rows.Count;
                dtReportResumen.Rows.Add(dtReportResumen.NewRow());
                dtReportResumen.Rows[filas]["Tipo_Documento"] = "BCBP";
                dtReportResumen.Rows[filas]["Total"] = "0";
            }
            else {
                if (sTipoDocumento == "B")
                {
                    int filas = dtReportResumen.Rows.Count;
                    dtReportResumen.Rows.Add(dtReportResumen.NewRow());
                    dtReportResumen.Rows[filas]["Tipo_Documento"] = "Ticket";
                    dtReportResumen.Rows[filas]["Total"] = "0";
                }
            }
            ViewState["dtResumen"] = dtReportResumen;
            DataView dv = dtReportResumen.DefaultView;
            dv.Sort = "Tipo_Documento ASC";
            grvDataResumen.DataSource = dv;
            grvDataResumen.DataBind();
        }*/
    }

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvTicketBoardingMes.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvTicketBoardingMes.PageIndex, grvTicketBoardingMes.PageSize, grvTicketBoardingMes.OrderBy);

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
            grvTicketBoardingMes.DataSource = null;
            grvTicketBoardingMes.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            grvTicketBoardingMes.DataSource = dt_consulta;
            grvTicketBoardingMes.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvTicketBoardingMes.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
        }
    }

    private int GetRowCount()
    {
        int count = 0;
        try
        {
            DataTable dt_consulta = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                                sNumVuelo,
                                                                                sTipoDocumento,
                                                                                sTipoTicket,
                                                                                sTipoRango,
                                                                                sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sDestino,
                                                                                null, 0, 0, "0", "0", "1");
            if (dt_consulta.Columns.Contains("TotRows"))
            {
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
                lblTotalRows.Value = count.ToString();
            }
            else
                lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();


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
        DataTable dt_consulta = new DataTable();
        try
        {
            dt_consulta = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                                sNumVuelo,
                                                                                sTipoDocumento,
                                                                                sTipoTicket,
                                                                                sTipoRango,
                                                                                sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sDestino,
                                                                                sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "1", "0", "0");
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

    protected void grvTicketBoardingMes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtResumenHoraMes = (DataTable)ViewState["Resumen"];
        tipoDocumento = dtResumenHoraMes.Rows[0]["Tipo_Documento"].ToString();
        grvTicketBoardingMes.PageIndex = e.NewPageIndex;
        grvDataResumen.DataSource = dtResumenHoraMes;
        grvDataResumen.DataBind();
        BindPagingGrid();
    }

    protected void grvTicketBoardingMes_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }

    protected void grvDataResumen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    //AGREGAMOS EL TOTAL                
        //    try
        //    {
        //        DataTable dt_Total = new DataTable();
        //        dt_Total = (DataTable)ViewState["dtResumen"];
        //        Int64 total = Convert.ToInt64(dt_Total.Compute("Sum(Total)", "").ToString());
        //        e.Row.Cells[0].Text = "TOTAL";
        //        e.Row.Cells[1].Text = total.ToString();
        //    }
        //    catch { }
        //}

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
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e) {
        if (e.Row.RowType == DataControlRowType.DataRow) {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tipoDocumento != drv["Tipo_Documento"].ToString())
            {
                // Calculamos los totales por documento
                DataTable dt_Total = (DataTable)ViewState["Total"];
                object total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();

                    #region fila total
                    cell.ColumnSpan = this.grvDataResumen.Columns.Count - 4;
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

        if (e.Row.RowType == DataControlRowType.Footer) {
            // Calculamos los totales por documento

            DataTable dt_Total = (DataTable)ViewState["Total"];

            object total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();

                #region fila total
                cell.ColumnSpan = this.grvDataResumen.Columns.Count - 4;
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
        }
    }

    protected void grvTicketBoardingMes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell th = new TableHeaderCell();
            th.ColumnSpan = 7;
            th.BackColor = System.Drawing.Color.White;
            row.Cells.Add(th);

            th = new TableCell();
            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 3;
            th.Style.Add("font-size", "larger");
            th.Style.Add("font-family", "Verdana");
            th.Style.Add("font-weight", "bold");
            th.Style.Add("color", "#000000");
            th.Text = "Cantidad";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }


    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TicketBP_DiaMesHora.xls");
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
        DataTable dt_resumen = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta_u = objBOReportes.consultarTicketBoardingUsados(sAerolinea, sNumVuelo, sTipoDocumento, sTipoTicket,
                      sTipoRango, sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDestino, null, 0,
                      0, "1", "0", "0");

          /*  dt_resumen = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                               sNumVuelo,
                                                                               sTipoDocumento,
                                                                               sTipoTicket,
                                                                               sTipoRango,
                                                                               sFechaDesde,
                                                                               sFechaHasta,
                                                                               sHoraDesde,
                                                                               sHoraHasta,
                                                                               sDestino,
                                                                               null, 0, 0, "0", "1", "0");*/
            dt_resumen = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                                sNumVuelo,
                                                                                sTipoDocumento,
                                                                                sTipoTicket,
                                                                                sTipoRango,
                                                                                sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sDestino,
                                                                                null, 0, 0, "0", "2", "0");
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


        #region Consulta Boarding
        Excel.Worksheet resumenUsado = new Excel.Worksheet("RptTicketBPDiaMesHora");
        resumenUsado.Columns = new string[] { "Fecha Uso", "Tipo Documento", "Tipo Ticket", "Aerolinea", "Nro. Vuelo", "Hora Inicio", "Hora Fin", "BP", "Ticket", "Total" };
        resumenUsado.WidthColumns = new int[] { 60, 80,130,80,70,60,60,50,50,50 };
        resumenUsado.DataFields = new string[] { "Log_Fecha_Mod", "Tipo_Documento", "Dsc_Tipo_Ticket", "Dsc_Compania", "Num_Vuelo", "HoraInicio","HoraFin","Total_BCBP","Total_Ticket","Total" };
        resumenUsado.Source = dt_consulta_u;
        #endregion

        #region Consulta Boarding Resumen
        Excel.Worksheet resumenRehab = new Excel.Worksheet("Resumen");
        resumenRehab.Columns = new string[] { "Tipo Documento", "Tipo Venta", "Tipo Vuelo", "Tipo Pasajero" , "Tipo Trasbordo", "Cantidad"};
        resumenRehab.WidthColumns = new int[] { 60, 100, 100, 100, 100, 80};
        resumenRehab.DataFields = new string[] { "Tipo_Documento", "Tip_Venta", "Tip_Vuelo", "Tip_Pasajero" , "Tip_Trasbordo","Total"};
        //resumenRehab.Source = dt_resumen;

        DataTable dtResumenTotal = new DataTable();
        dtResumenTotal = dt_resumen.Clone();
        int rows = dt_resumen.Rows.Count;
        DataRow fila;

        // Calculamos los totales por documento
        DataTable dt_Total = (DataTable)ViewState["Total"];
        tipoDocumento = dt_resumen.Rows[0]["Tipo_Documento"].ToString();
        int cont = 0;
        string documento = "";
        object total = null;

        for (int k = 0; k < rows; k++)
        {
            if (tipoDocumento != dt_resumen.Rows[k]["Tipo_Documento"].ToString())
            {
                if (k == rows - 1) //es la ultima posicion
                {
                    fila = dt_resumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    documento = dt_resumen.Rows[k]["Tipo_Documento"].ToString();
                    total = dt_Total.Select("Tipo_Documento = '" + documento + "'")[0]["Total"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Venta"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Total"] = total.ToString();
                }
                else
                {
                    if (cont == 0) //si es por primera vez
                    {
                        //adicionando subtotal documento previo
                        total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k]["Tip_Venta"] = "Total";
                        dtResumenTotal.Rows[k]["Total"] = total.ToString();

                        //adicionando el documento actual
                        fila = dt_resumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }
                    else
                    {
                        //adicionando el documento actual
                        fila = dt_resumen.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                    }
                    cont++;
                }
            }
            else 
            {
                if (k == rows - 1)//ultima posicion en caso haya solo un tipo de documento
                {
                    fila = dt_resumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    documento = dt_resumen.Rows[k]["Tipo_Documento"].ToString();
                    total = dt_Total.Select("Tipo_Documento = '" + documento + "'")[0]["Total"].ToString();

                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Venta"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Total"] = total.ToString();
                }
                else
                {
                    fila = dt_resumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);
                }
            }
        }

        resumenRehab.Source = dtResumenTotal;

        #endregion

        
        Workbook.Worksheets = new Excel.Worksheet[] { resumenUsado, resumenRehab };

        return Workbook.Save();
       
    }
}
