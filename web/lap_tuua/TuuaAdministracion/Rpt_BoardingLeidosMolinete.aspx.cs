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

public partial class Rpt_BoardingLeidosMolinete : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Reportes objReporte = new BO_Reportes();

    DataTable dt_consulta = new DataTable();
    DataTable dt_allcompania = new DataTable();
    DataTable dt_estadoBcbp = new DataTable();
    UIControles objCargaComboCompania = new UIControles();

    //Filtros
    string FechaDesde;
    string FechaHasta;
    string idCompania;
    string sFechaVuelo;
    string sNumVuelo;
    string idEstado;
    string sNumBoarding;

    string sMaxGrilla;
    bool isFull;

    //Summary
    int qResTotal = 0;

    #region Event - Handlers
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblCompania.Text = htLabels["rptBoardingMolinete.lblCompania.Text"].ToString();
                this.lblFechaVuelo.Text = htLabels["rptBoardingMolinete.lblFechaVuelo.Text"].ToString();
                this.lblNumVuelo.Text = htLabels["rptBoardingMolinete.lblNumVuelo.Text"].ToString();
                this.lblFechaLecturaIni.Text = htLabels["rptBoardingMolinete.lblFechaLecturaIni.Text"].ToString();
                this.lblFechaLecturaFin.Text = htLabels["rptBoardingMolinete.lblFechaLecturaFin.Text"].ToString();
                //this.lblEstado.Text = htLabels["rptBoardingMolinete.lblEstado.Text"].ToString();
                this.lblNumBoarding.Text = htLabels["rptBoardingMolinete.lblNumBoarding.Text"].ToString();
                this.btnConsultar.Text = htLabels["rptBoardingMolinete.btnConsultar.Text"].ToString();
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
            txtFechaLecturaIni.Text = DateTime.Now.ToShortDateString();
            txtFechaLecturaFin.Text = DateTime.Now.ToShortDateString();

            CargarCombo();
            this.lblMensajeError.Text = "";
            //CargarDataReporte();
            SaveFiltros();
            BindPagingGrid();
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
        SaveFiltros();
        BindPagingGrid();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
        if (isFull) { CargarDataResumen(); }
    }
    #endregion

    public void CargarCombo()
    {
        try
        {
            //Carga combo compañia
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaComboCompania.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            //Carga combo estado BCBP            
            dt_estadoBcbp = objConsulta.ListaCamposxNombre("EstadoBcbp");

            DataRow newEstado = dt_estadoBcbp.NewRow();

            newEstado["Cod_Campo"] = "1";
            newEstado["Dsc_Campo"] = "VENCIDO";

            dt_estadoBcbp.Rows.Add(newEstado);

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

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("FechaDesde", Fecha.convertToFechaSQL2(txtFechaLecturaIni.Text)));
        filterList.Add(new Filtros("FechaHasta", Fecha.convertToFechaSQL2(txtFechaLecturaFin.Text)));
        filterList.Add(new Filtros("idCompania", this.ddlCompania.SelectedItem.Value));
        filterList.Add(new Filtros("sFechaVuelo", Fecha.convertToFechaSQL2(this.txtFechaVuelo.Text)));
        filterList.Add(new Filtros("sNumVuelo", this.txtNumVuelo.Text.Trim()));
        filterList.Add(new Filtros("sNumBoarding", this.txtNumBoarding.Text.Trim()));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        FechaDesde = newFilterList[0].Valor;
        FechaHasta = newFilterList[1].Valor;
        idCompania = newFilterList[2].Valor;
        sFechaVuelo = newFilterList[3].Valor;
        sNumVuelo = newFilterList[4].Valor;
        sNumBoarding = newFilterList[5].Valor;
    }
    #endregion

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvBoardingMolinete.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvBoardingMolinete.PageIndex, grvBoardingMolinete.PageSize, grvBoardingMolinete.OrderBy);

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
            grvBoardingMolinete.DataSource = null;
            grvBoardingMolinete.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            //grvTicketUsados.Visible = true;
            grvBoardingMolinete.DataSource = dt_consulta;
            grvBoardingMolinete.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvBoardingMolinete.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.isFull = true;
        }
    }

    private int GetRowCount()
    {
        int count = 0;

        DataTable dt_consulta = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, sFechaVuelo, sNumVuelo, FechaDesde, FechaHasta, idEstado, sNumBoarding, "0", null, 0, 0, "1");
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
        DataTable dt_consulta = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, sFechaVuelo, sNumVuelo, FechaDesde, FechaHasta, idEstado, sNumBoarding, "0", sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "0");
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

    public void CargarDataReporte()
    {
        /*try
        {
            string FechaDesde = Fecha.convertToFechaSQL2(txtFechaLecturaIni.Text);
            string FechaHasta = Fecha.convertToFechaSQL2(txtFechaLecturaFin.Text);
            string idCompania = this.ddlCompania.SelectedItem.Value;
            string sFechaVuelo = Fecha.convertToFechaSQL2(this.txtFechaVuelo.Text);
            string sNumVuelo = this.txtNumVuelo.Text.Trim();
            string idEstado = this.ddlEstado.SelectedValue.Trim();
            string sNumBoarding = this.txtNumBoarding.Text.Trim();

            dt_consulta = objReporte.BoardingLeidosMolinete(idCompania, sFechaVuelo, sNumVuelo, FechaDesde, FechaHasta, idEstado, sNumBoarding, "0");

            if (dt_consulta.Rows.Count == 0)
            {
                crvBoardingMolinete.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else if (dt_consulta.Columns[0].ColumnName == "MsgError")
            {
                this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                crvBoardingMolinete.ReportSource = null;
                crvBoardingMolinete.DataBind();
            }
            else
            {
                lblMensajeErrorData.Text = "";
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptBoardingLeidosMolinete.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("sCompania", this.ddlCompania.SelectedItem.Value == "0" ? "Todos" : this.ddlCompania.SelectedItem.Text);
                obRpt.SetParameterValue("sFechaVuelo", this.txtFechaVuelo.Text == "" ? "Sin Filtro" : this.txtFechaVuelo.Text);
                obRpt.SetParameterValue("sNumVuelo", this.txtNumVuelo.Text == "" ? "Sin Filtro" : this.txtNumVuelo.Text);
                obRpt.SetParameterValue("sFechaLecturaIni", this.txtFechaLecturaIni.Text);
                obRpt.SetParameterValue("sFechaLecturaFin", this.txtFechaLecturaFin.Text);
                obRpt.SetParameterValue("sEstado", this.ddlEstado.SelectedItem.Value == "0" ? "Todos" : this.ddlEstado.SelectedItem.Text);
                obRpt.SetParameterValue("sNumBoarding", this.txtNumBoarding.Text == "" ? "Sin Filtro" : this.txtNumBoarding.Text);
                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                crvBoardingMolinete.Visible = true;
                crvBoardingMolinete.ReportSource = obRpt;
                crvBoardingMolinete.DataBind();
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

    #region Paginacion
    protected void grvBoardingMolinete_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //RecuperarFiltros();
        grvBoardingMolinete.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }
    #endregion

    public void CargarDataResumen()
    {
        DataTable dtReportResumen = new DataTable();
        dtReportResumen = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, sFechaVuelo, sNumVuelo, FechaDesde, FechaHasta, idEstado, sNumBoarding, "1", "", 0, 0, "0");

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
            th.Text = "Resumen";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    protected void grvDataResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            qResTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Cantidad"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total";
            // for the Footer, display the running totals
            e.Row.Cells[6].Text = qResTotal.ToString("d");

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=BPLeidosMolinete.xls");
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
        DataTable dt_consulta_res = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, 
                                                                            sFechaVuelo, 
                                                                            sNumVuelo, 
                                                                            FechaDesde, 
                                                                            FechaHasta, 
                                                                            "0", 
                                                                            sNumBoarding, 
                                                                            "0", null, 0, 0, "0");

            dt_consulta_res = objReporte.ListarBoardingLeidosMolinetePagin(idCompania,
                                                                            sFechaVuelo,
                                                                            sNumVuelo,
                                                                            FechaDesde,
                                                                            FechaHasta,
                                                                            idEstado,
                                                                            sNumBoarding,
                                                                            "1", null, 0, 0, "0");

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


        #region Reporte BP Leidos Molinete
        Excel.Worksheet BPLeidosMolinete = new Excel.Worksheet("Reporte BP Leidos Molinete");
        BPLeidosMolinete.Columns = new string[] { "Nro. Correlativo", "Nro. Boarding", "Aerolínea", "Nro. Vuelo", "Fecha Vuelo", "Nro. Asiento", "Nro. Molinete", "Fecha Uso", "Estado" };
        BPLeidosMolinete.WidthColumns = new int[] { 60, 100, 200, 80, 80, 80, 140, 100, 80 };
        BPLeidosMolinete.DataFields = new string[] { "Num_Secuencial_Bcbp", "Cod_Numero_Bcbp", "Dsc_Compania", "Num_Vuelo", "Fch_Vuelo",
                                                        "Num_Asiento", "NroMolinete", "FechaUso", "Dsc_Campo" };
        BPLeidosMolinete.Source = dt_consulta;
        #endregion

        #region RESUMEN
        Excel.Worksheet resumenBP = new Excel.Worksheet("RESUMEN");
        resumenBP.Columns = new string[] { "Fecha Lectura", "Aerolínea", "Nro. Vuelo", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        resumenBP.WidthColumns = new int[] { 100, 200, 80, 100, 100, 100, 100 };
        resumenBP.DataFields = new string[] { "FechaUso", "Dsc_Compania", "Num_Vuelo", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo", "Cantidad" };
        resumenBP.Source = dt_consulta_res;
        #endregion



        Workbook.Worksheets = new Excel.Worksheet[] { BPLeidosMolinete, resumenBP };

        return Workbook.Save();

    }
}