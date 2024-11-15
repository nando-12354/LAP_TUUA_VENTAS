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

public partial class Rpt_TicketBoardingUsadosDiaMes : System.Web.UI.Page
{
    private BO_Consultas objBOConsulta = new BO_Consultas();
    private BO_Administracion objBOAdministracion = new BO_Administracion();
    private BO_Reportes objBOReporte = new BO_Reportes();

    protected DataTable dt_reporte = new DataTable();
    protected DataTable dt_resumen = new DataTable();

    UIControles objCargaCombo = new UIControles();
    protected Hashtable htLabels;
    bool Flg_Error;
    private string strTipoDocumento = "";

    //Filtros
    string sFechaDesde, sFechaHasta;
    string sMes, sAnnio;
    string sTDocumento;
    string sIdCompania;
    string sDestino;
    string sTipoTicket;
    string sNumVuelo;
    string sTipReporte;

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
                lblFecha.Text = htLabels["rptTicketBoadingUsados.lblFecha"].ToString();
                lblAerolinea.Text = htLabels["rptTicketBoadingUsados.lblAerolinea"].ToString();
                lblDestino.Text = htLabels["rptTicketBoadingUsados.lblDestino"].ToString();
                lblNumeroVuelo.Text = htLabels["rptTicketBoadingUsados.lblNumeroVuelo"].ToString();
                lblTipoDocumento.Text = htLabels["rptTicketBoadingUsados.lblTipoDocumento"].ToString();
                lblTipoTicket.Text = htLabels["rptTicketBoadingUsados.lblTipoTicket"].ToString();
                rbtnDesde.Text = htLabels["rptTicketBoadingUsados.rbtnDesde"].ToString();
                lblHasta.Text = htLabels["rptTicketBoadingUsados.lblHasta"].ToString();
                rbtnMes.Text = htLabels["rptTicketBoadingUsados.rbtnMes"].ToString();
                chkbTicket.Text = htLabels["rptTicketBoadingUsados.chkbTicket"].ToString();
                chkbBP.Text = htLabels["rptTicketBoadingUsados.chkbBP"].ToString();

               

            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = LAP.TUUA.UTIL.Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[LAP.TUUA.UTIL.Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                    Response.Redirect("PaginaError.aspx");

            }
            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();

            CargarCombos();
            ValidarRadioButton();

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

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtHasta.Text)));
        filterList.Add(new Filtros("sMes", this.hfMes.Value));
        filterList.Add(new Filtros("sAnnio", this.hfAnnio.Value));

        sTDocumento = "";
        if (this.chkbTicket.Checked == true && this.chkbBP.Checked == false)
            sTDocumento = "T";
        else if (this.chkbTicket.Checked == false && this.chkbBP.Checked == true)
            sTDocumento = "B";
        else if (this.chkbTicket.Checked == true && this.chkbBP.Checked == true)
            sTDocumento = "0";
        filterList.Add(new Filtros("sTDocumento", sTDocumento));

        filterList.Add(new Filtros("sIdCompania", this.ddlAerolinea.SelectedValue));
        filterList.Add(new Filtros("sDestino", this.txtDestino.Text.ToUpper()));
        filterList.Add(new Filtros("sTipoTicket", this.ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", this.txtNroVuelo.Text.Trim()));
        
        if (this.rbtnMes.Checked == true)
            sTipReporte = "1";

        filterList.Add(new Filtros("sTipReporte", sTipReporte));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sMes = newFilterList[2].Valor;
        sAnnio = newFilterList[3].Valor;
        sTDocumento = newFilterList[4].Valor;
        sIdCompania = newFilterList[5].Valor;
        sDestino = newFilterList[6].Valor;
        sTipoTicket = newFilterList[7].Valor;
        sNumVuelo = newFilterList[8].Valor;
        sTipReporte = newFilterList[9].Valor;
    }
    #endregion

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvTicketBPUsadosDiaMes.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvTicketBPUsadosDiaMes.PageIndex, grvTicketBPUsadosDiaMes.PageSize, grvTicketBPUsadosDiaMes.OrderBy);

        htLabels = LabelConfig.htLabels;
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
                ErrorHandler.Cod_Error = LAP.TUUA.UTIL.Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            grvTicketBPUsadosDiaMes.DataSource = null;
            grvTicketBPUsadosDiaMes.DataBind();
            grvDataResumen.DataSource = null;
            grvDataResumen.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            string fechaEstadistico = objBOConsulta.obtenerFechaEstadistico("0");
            this.lblFechaEstadistico.Text = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
            //grvTicketUsados.Visible = true;
            grvTicketBPUsadosDiaMes.DataSource = dt_consulta;
            grvTicketBPUsadosDiaMes.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvTicketBPUsadosDiaMes.DataBind();
            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.isFull = true;
        }
    }

    private int GetRowCount()
    {
        int count = 0;
        try{
            dt_resumen = objBOReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes
                            , sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                            , sDestino, sTipReporte, null, 0, 0, "1");

            if (dt_resumen.Columns.Contains("TotRows"))
            {
                count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
                lblTotalRows.Value = count.ToString();
                ViewState["Total"] = dt_resumen;
            }
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
        DataTable dt_consulta = new DataTable();
        try
        {
            dt_consulta = objBOReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde
                       , sFechaHasta, sAnnio + sMes, sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                       , sDestino, sTipReporte, sortExpression, pageIndex, Convert.ToInt32(sMaxGrilla), "0");
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

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objBOConsulta.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    public void CargarDataResumen()
    {
        DataTable dtReportResumen = new DataTable();
        //dtReportResumen = objBOReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, null, 0, 0, "2");

        dtReportResumen = objBOReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes
                            , sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                            , sDestino, sTipReporte, null, 0, 0, "2");

        if (dtReportResumen == null || dtReportResumen.Rows.Count == 0)
        {
            grvDataResumen.Visible = false;
            lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            //DataTable dest = new DataTable("Pivoted");

            //DataColumn dc = new DataColumn("Tip_Documento");
            //dest.Columns.Add(dc);
            //dc = new DataColumn("Cantidad");
            //dest.Columns.Add(dc);

            //dest.Rows.Add(dest.NewRow());
            //dest.Rows[0][0] = "BP";
            //dest.Rows[0][1] = Convert.ToInt32((dt_resumen.Rows[0]["TotBoarding"] == null)?"0":dt_resumen.Rows[0]["TotBoarding"].ToString());
            //dest.Rows.Add(dest.NewRow());
            //dest.Rows[1][0] = "Ticket";
            //dest.Rows[1][1] = Convert.ToInt32((dt_resumen.Rows[0]["TotTicket"] == null)?"0":dt_resumen.Rows[0]["TotTicket"].ToString());
            //dest.Rows.Add(dest.NewRow());
            //dest.Rows[2][0] = "Total";
            //dest.Rows[2][1] = Convert.ToInt32((dt_resumen.Rows[0]["Total"] == null)?"0":dt_resumen.Rows[0]["Total"].ToString());
            //dest.AcceptChanges();
            //Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());

            //grvDataResumen.DataSource = dest;
            strTipoDocumento = dtReportResumen.Rows[0]["Tip_Documento"].ToString();
            ViewState["Resumen"] = dtReportResumen;
            grvDataResumen.DataSource = dtReportResumen;
            grvDataResumen.DataBind();
        }
    }

    public void CargarDataReporte()
    {
        /*try
        {
            string sFechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            string sFechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
            string sMes = this.hfMes.Value;
            string sAnnio = this.hfAnnio.Value; 

            string sTDocumento = "";
            if (this.chkbTicket.Checked == true && this.chkbBP.Checked == false)
                sTDocumento = "T";
            else if (this.chkbTicket.Checked == false && this.chkbBP.Checked == true)
                sTDocumento = "B";
            else if (this.chkbTicket.Checked == true && this.chkbBP.Checked == true)
                sTDocumento = "O"; 
            else
                sTDocumento = "";

            string sIdCompania = this.ddlAerolinea.SelectedValue;
            string sDesCompania = this.ddlAerolinea.SelectedItem.Text;
            this.sDestino = this.txtDestino.Text.ToUpper();
            string sTipoTicket = this.ddlTipoTicket.SelectedValue;
            string sDesTipoTicket = this.ddlTipoTicket.SelectedItem.Text;
            string sNumVuelo = this.txtNroVuelo.Text.Trim();

            string sTipoFiltro = "TIME";
            if (this.rbtnMes.Checked == true) {
                sTipoFiltro = "MES";
            }

            dt_reporte = CrossTab(FiltraFechas(FiltrarFechaUso(objBOReporte.consultarTicketBoardingUsados(sIdCompania, sNumVuelo, sTDocumento, sTipoTicket, sTipoFiltro, sFechaDesde, sFechaHasta, "", "", sAnnio + sMes)), sFechaDesde, sFechaHasta, sMes, sAnnio));

            if (dt_reporte.Rows.Count == 0)
            {
                crvrptTicketBPUsadosDiaMes.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                lblMensajeErrorData.Text = "";
                obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptTicketBPUsadosDiaMes.rpt");

                obRpt.SetDataSource(dt_reporte);
                obRpt.SetParameterValue("pDesde", FormatearFecha(sFechaDesde));
                obRpt.SetParameterValue("pHasta", FormatearFecha(sFechaHasta));
                if (sMes != "")
                    obRpt.SetParameterValue("pMes", this.ddlMes.Items[Convert.ToInt32(sMes) - 1].Text);
                else
                    obRpt.SetParameterValue("pMes", " - ");

                obRpt.SetParameterValue("pAño", sAnnio);

                string sTiDoc = "";
                if (sTDocumento == "O")
                    sTiDoc = "Ticket - BP";
                if (sTDocumento == "T")
                    sTiDoc = "Ticket";
                if (sTDocumento == "B")
                    sTiDoc = "Boarding Pass";

                obRpt.SetParameterValue("pTipoDocumento", sTiDoc);
                obRpt.SetParameterValue("pAerolinea", sDesCompania);
                obRpt.SetParameterValue("pNroVuelo", sNumVuelo);
                obRpt.SetParameterValue("pDestino", sDestino);
                obRpt.SetParameterValue("pTipoTicket", sDesTipoTicket);

                crvrptTicketBPUsadosDiaMes.Visible = true;
                crvrptTicketBPUsadosDiaMes.ReportSource = obRpt;
                crvrptTicketBPUsadosDiaMes.DataBind();
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
            dt_allcompania = objBOConsulta.listarAllCompania();
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

    protected void ValidarRadioButton()
    {
        if (rbtnDesde.Checked)
        {
            txtDesde.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txtHasta.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txtMes.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            this.imbCalDesde.Enabled = true;
            this.imbCalHasta.Enabled = true;
            this.imgbtnCalendarMes.Enabled = false;
        }

        if (this.rbtnMes.Checked)
        {
            txtDesde.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            txtHasta.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            txtMes.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            this.imbCalDesde.Enabled = false;
            this.imbCalHasta.Enabled = false;
            this.imgbtnCalendarMes.Enabled = true;
        }

    }

    #region cmontes
    /*private DataTable FiltrarFechaUso(DataTable dtReporteDetalle)
    {
        DataTable dest = new DataTable("Result" + dtReporteDetalle.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_BCP";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dest.Columns.Add(dc);


        DataTable dtTickets = new DataTable();

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Codigo";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "FlagCobro";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Secuencial";
        dtTickets.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dtTickets.Columns.Add(dc);


        if (dtReporteDetalle.Rows.Count > 0)
        {

            DataRow[] foundRowTicket = dtReporteDetalle.Select("Tipo_Documento = 'Ticket'");

            if (foundRowTicket != null && foundRowTicket.Length > 0)
            {
                for (int i = 0; i < foundRowTicket.Length; i++)
                {
                    dtTickets.Rows.Add(dtTickets.NewRow());
                    dtTickets.Rows[i][0] = foundRowTicket[i]["Log_Fecha_Mod"].ToString();
                    dtTickets.Rows[i][1] = foundRowTicket[i]["Log_Hora_Mod"].ToString();
                    dtTickets.Rows[i][2] = foundRowTicket[i]["Tipo_Documento"].ToString();
                    dtTickets.Rows[i][3] = foundRowTicket[i]["Dsc_Tipo_Ticket"].ToString();
                    dtTickets.Rows[i][4] = foundRowTicket[i]["Dsc_Compania"].ToString();
                    dtTickets.Rows[i][5] = foundRowTicket[i]["Num_Vuelo"].ToString();
                    dtTickets.Rows[i][6] = foundRowTicket[i]["Destino"].ToString();
                    dtTickets.Rows[i][7] = foundRowTicket[i]["Total_Ticket"].ToString();
                    dtTickets.Rows[i][8] = foundRowTicket[i]["Codigo"].ToString();
                    dtTickets.Rows[i][9] = foundRowTicket[i]["FlagCobro"].ToString();
                    dtTickets.Rows[i][10] = foundRowTicket[i]["Secuencial"].ToString();
                    dtTickets.Rows[i][11] = foundRowTicket[i]["Dsc_Trama_Bcbp"].ToString();
                }
            }


            DataTable dtCodTicket = new DataTable();
            dtCodTicket = dtTickets;
            //          dtCodTicket = SelectDistinct(dtTickets, "Codigo");

            for (int i = 0; i < dtCodTicket.Rows.Count; i++)
            {
                DataRow[] foundRowCodTicket = dtTickets.Select("Codigo = '" + dtCodTicket.Rows[i][8] + "' AND Secuencial = '" + dtCodTicket.Rows[i][10] + "'");
                DataTable dtFechaTickets = new DataTable();
                dc = new DataColumn();
                dc.ColumnName = "Secuencial";
                dtFechaTickets.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "Log_Fecha_Mod";
                dtFechaTickets.Columns.Add(dc);
                dc = new DataColumn();
                dc.ColumnName = "Log_Hora_Mod";
                dtFechaTickets.Columns.Add(dc);

                if (foundRowCodTicket != null && foundRowCodTicket.Length > 0)
                {
                    for (int j = 0; j < foundRowCodTicket.Length; j++)
                    {
                        dtFechaTickets.Rows.Add(dtFechaTickets.NewRow());
                        dtFechaTickets.Rows[j][0] = foundRowCodTicket[j]["Secuencial"].ToString();
                        dtFechaTickets.Rows[j][1] = foundRowCodTicket[j]["Log_Fecha_Mod"].ToString();
                        dtFechaTickets.Rows[j][2] = foundRowCodTicket[j]["Log_Hora_Mod"].ToString();
                    }
                }

                DataRow[] foundRowFechTicket = null;

                if (foundRowCodTicket[0]["FlagCobro"].ToString() == "0")
                {
                    foundRowFechTicket = dtFechaTickets.Select("Secuencial = Max(Secuencial)");
                }
                else
                {
                    if (foundRowCodTicket[0]["FlagCobro"].ToString() == "1")
                    {
                        foundRowFechTicket = dtFechaTickets.Select("Secuencial = Min(Secuencial)");
                    }
                }

                if (foundRowFechTicket != null)
                {
                    dest.Rows.Add(dest.NewRow());
                    dest.Rows[i][0] = foundRowFechTicket[0]["Log_Fecha_Mod"].ToString();
                    dest.Rows[i][1] = foundRowFechTicket[0]["Log_Hora_Mod"].ToString();
                    dest.Rows[i][2] = foundRowCodTicket[0]["Tipo_Documento"].ToString();
                    dest.Rows[i][3] = foundRowCodTicket[0]["Dsc_Tipo_Ticket"].ToString();
                    dest.Rows[i][4] = foundRowCodTicket[0]["Dsc_Compania"].ToString();
                    dest.Rows[i][5] = foundRowCodTicket[0]["Num_Vuelo"].ToString();
                    dest.Rows[i][6] = foundRowCodTicket[0]["Destino"].ToString();
                    dest.Rows[i][7] = foundRowCodTicket[0]["Total_Ticket"].ToString();
                    dest.Rows[i][8] = "0";
                    dest.Rows[i][9] = foundRowCodTicket[0]["Total_Ticket"].ToString();
                    dest.Rows[i][10] = foundRowCodTicket[0]["Dsc_Trama_Bcbp"].ToString();
                }
            }

            DataRow[] foundRowBP = dtReporteDetalle.Select("Tipo_Documento = 'BCBP'");


            if (foundRowBP != null && foundRowBP.Length > 0)
            {
                int iTotalRows = dest.Rows.Count;

                if (iTotalRows > 0)
                {
                    for (int i = iTotalRows; i < iTotalRows + foundRowBP.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        dest.Rows[i][0] = foundRowBP[i - iTotalRows]["Log_Fecha_Mod"].ToString();
                        dest.Rows[i][1] = foundRowBP[i - iTotalRows]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowBP[i - iTotalRows]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowBP[i - iTotalRows]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowBP[i - iTotalRows]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowBP[i - iTotalRows]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowBP[i - iTotalRows]["Destino"].ToString();
                        dest.Rows[i][7] = "0";
                        dest.Rows[i][8] = foundRowBP[i - iTotalRows]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowBP[i - iTotalRows]["Total_BCP"].ToString();
                        dest.Rows[i][10] = foundRowBP[i - iTotalRows]["Dsc_Trama_Bcbp"].ToString();
                    }
                }
                else
                {
                    for (int i = 0; i < foundRowBP.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        dest.Rows[i][0] = foundRowBP[i]["Log_Fecha_Mod"].ToString();
                        dest.Rows[i][1] = foundRowBP[i]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowBP[i]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowBP[i]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowBP[i]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowBP[i]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowBP[i]["Destino"].ToString();
                        dest.Rows[i][7] = "0";
                        dest.Rows[i][8] = foundRowBP[i]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowBP[i]["Total_BCP"].ToString();
                        dest.Rows[i][10] = foundRowBP[i]["Dsc_Trama_Bcbp"].ToString();
                    }

                }
            }
        }
        dest.AcceptChanges();
        return dest;
    }
    private DataTable FiltraFechas(DataTable dtReporteDetalle, string sFechaDesde, string sFechaHasta, string sMes, string sAño)
    {

        DataTable dest = new DataTable("Result" + dtReporteDetalle.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Log_Hora_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total_BCP";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Total";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Trama_Bcbp";
        dest.Columns.Add(dc);


        string sSeleccion = "";

        if (sFechaDesde != "")
        {
            sSeleccion = "Log_Fecha_Mod >= '" + sFechaDesde + "'";
            if (sFechaHasta != "")
                sSeleccion = sSeleccion + " AND Log_Fecha_Mod <= '" + sFechaHasta + "'";
        }
        else
        {
            if (sFechaHasta != "")
                sSeleccion = "Log_Fecha_Mod <= '" + sFechaHasta + "'";
        }

        if (sMes != "")
            sSeleccion = "SUBSTRING(Log_Fecha_Mod,5,2) = '" + sMes + "' AND SUBSTRING(Log_Fecha_Mod,1,4)= '" + sAño + "' ";

        if (sSeleccion != "")
        {
            if (dtReporteDetalle.Rows.Count > 0)
            {
                DataRow[] foundRowFiltroFecha = dtReporteDetalle.Select(sSeleccion);

                if (foundRowFiltroFecha != null && foundRowFiltroFecha.Length > 0)
                {
                    for (int i = 0; i < foundRowFiltroFecha.Length; i++)
                    {
                        dest.Rows.Add(dest.NewRow());
                        if (sMes == "")
                            dest.Rows[i][0] = Fecha.convertSQLToFecha(foundRowFiltroFecha[i]["Log_Fecha_Mod"].ToString(), "");
                        else
                            dest.Rows[i][0] = ddlMes.Items[Convert.ToInt32(sMes) - 1].Text + " - " + sAño;
                        dest.Rows[i][1] = foundRowFiltroFecha[i]["Log_Hora_Mod"].ToString();
                        dest.Rows[i][2] = foundRowFiltroFecha[i]["Tipo_Documento"].ToString();
                        dest.Rows[i][3] = foundRowFiltroFecha[i]["Dsc_Tipo_Ticket"].ToString();
                        dest.Rows[i][4] = foundRowFiltroFecha[i]["Dsc_Compania"].ToString();
                        dest.Rows[i][5] = foundRowFiltroFecha[i]["Num_Vuelo"].ToString();
                        dest.Rows[i][6] = foundRowFiltroFecha[i]["Destino"].ToString();
                        dest.Rows[i][7] = foundRowFiltroFecha[i]["Total_Ticket"].ToString();
                        dest.Rows[i][8] = foundRowFiltroFecha[i]["Total_BCP"].ToString();
                        dest.Rows[i][9] = foundRowFiltroFecha[i]["Total"].ToString();
                        dest.Rows[i][10] = foundRowFiltroFecha[i]["Dsc_Trama_Bcbp"].ToString();
                    }
                }
            }
            dest.AcceptChanges();

            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Log_Fecha_Mod");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Tipo_Documento");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Dsc_Compania");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Dsc_Tipo_Ticket");
            dest = dsHelper.SelectGroupByInto("Reporte", dest, "Log_Fecha_Mod,Log_Hora_Mod,Tipo_Documento,Dsc_Tipo_Ticket,Dsc_Compania,Num_Vuelo,Destino,sum(Total_Ticket) Total_Ticket,sum(Total_BCP) Total_BCP,sum(Total) Total,Dsc_Trama_Bcbp", "", "Num_Vuelo");


            return dest;
        }
        else
            return dtReporteDetalle;

    }
    private DataTable CrossTab(DataTable dtSourceTable)
    {

        DataTable dest = new DataTable("Result" + dt_reporte.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Log_Fecha_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Tipo_Documento";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Tipo_Ticket";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Num_Vuelo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Destino";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "TipoValor";
        dest.Columns.Add(dc);
        dest.Columns.Add("Valor", System.Type.GetType("System.Int32"));

        int i = 0;
        int j = 0;
        while (i < dtSourceTable.Rows.Count)
        {

            string sTramaBcbp = dtSourceTable.Rows[i]["Dsc_Trama_Bcbp"].ToString();
            Reader reader = new Reader();
            Hashtable ht;
            string sDestinoList;
            bool isAceptar = false;
            if (this.sDestino == string.Empty)
            {
                isAceptar = true;
            }
            else if (dtSourceTable.Rows[i]["Tipo_Documento"].ToString() == "Ticket")
            {
                isAceptar = false;
            }
            else
            {
                ht = reader.ParseString_Boarding(sTramaBcbp);

                if (ht != null)
                {
                    sDestinoList = (String)ht["to_city_airport_code"];

                    if (sDestinoList != null && sDestinoList.Length > 0)
                    {
                        if (this.sDestino.Contains(sDestinoList))
                        {
                            isAceptar = true;
                        }
                    }
                }
            }

            if (isAceptar)
            {
                dest.Rows.Add(dest.NewRow());
                dest.Rows[j]["Log_Fecha_Mod"] = dtSourceTable.Rows[i]["Log_Fecha_Mod"].ToString();
                dest.Rows[j]["Tipo_Documento"] = dtSourceTable.Rows[i]["Tipo_Documento"].ToString();
                dest.Rows[j]["Dsc_Tipo_Ticket"] = dtSourceTable.Rows[i]["Dsc_Tipo_Ticket"].ToString();
                dest.Rows[j]["Dsc_Compania"] = dtSourceTable.Rows[i]["Dsc_Compania"].ToString();
                dest.Rows[j]["Num_Vuelo"] = dtSourceTable.Rows[i]["Num_Vuelo"].ToString();
                dest.Rows[j]["Destino"] = dtSourceTable.Rows[i]["Destino"].ToString();
                dest.Rows[j]["TipoValor"] = "Ticket";
                dest.Rows[j]["Valor"] = Convert.ToInt32(dtSourceTable.Rows[i]["Total_Ticket"].ToString());

                dest.Rows.Add(dest.NewRow());
                dest.Rows[j + 1]["Log_Fecha_Mod"] = dtSourceTable.Rows[i]["Log_Fecha_Mod"].ToString();
                dest.Rows[j + 1]["Tipo_Documento"] = dtSourceTable.Rows[i]["Tipo_Documento"].ToString();
                dest.Rows[j + 1]["Dsc_Tipo_Ticket"] = dtSourceTable.Rows[i]["Dsc_Tipo_Ticket"].ToString();
                dest.Rows[j + 1]["Dsc_Compania"] = dtSourceTable.Rows[i]["Dsc_Compania"].ToString();
                dest.Rows[j + 1]["Num_Vuelo"] = dtSourceTable.Rows[i]["Num_Vuelo"].ToString();
                dest.Rows[j + 1]["Destino"] = dtSourceTable.Rows[i]["Destino"].ToString();
                dest.Rows[j + 1]["TipoValor"] = "BP";
                dest.Rows[j + 1]["Valor"] = Convert.ToInt32(dtSourceTable.Rows[i]["Total_BCP"].ToString());
                j = j + 2;
            }

            i++;
        }

        dest.AcceptChanges();
        return dest;
    }*/
    #endregion

    #region Paginacion
    protected void grvTicketBPUsadosDiaMes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtResumenHoraMes = (DataTable)ViewState["Resumen"];
        grvTicketBPUsadosDiaMes.PageIndex = e.NewPageIndex;
        strTipoDocumento = dtResumenHoraMes.Rows[0]["Tip_Documento"].ToString();
        grvDataResumen.DataSource = dtResumenHoraMes;
        grvDataResumen.DataBind();
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
            th.ColumnSpan = 6;
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
            DataRowView drv = (DataRowView)e.Row.DataItem;
            string strTotal = "";
            if (strTipoDocumento != drv["Tip_Documento"].ToString()) {
                // Calculamos los totales por documento
                DataTable dt_Total = (DataTable)ViewState["Total"];

                if (strTipoDocumento == "Ticket")
                {
                    strTotal = dt_Total.Rows[0]["TotTicket"].ToString();
                }
                else {
                    strTotal = dt_Total.Rows[0]["TotBoarding"].ToString();
                }

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
                    span.InnerHtml = strTotal;
                    cell.Controls.Add(span);
                    row.Cells.Add(cell);


                    tbl.Rows.AddAt(tbl.Rows.Count - 1, row);

                    #endregion
                }
                strTipoDocumento = drv["Tip_Documento"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por documento
            DataTable dt_Total = (DataTable)ViewState["Total"];
            string total = "";

            if (strTipoDocumento == "Ticket")
            {
                total = dt_Total.Rows[0]["TotTicket"].ToString();
            }
            else
            {
                total = dt_Total.Rows[0]["TotBoarding"].ToString();
            }

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
                span.InnerHtml = total;
                cell.Controls.Add(span);
                row.Cells.Add(cell);


                tbl.Rows.AddAt(tbl.Rows.Count - 1, row);

                #endregion
            }
        }
    }
    protected void grvTicketBPUsadosDiaMes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Nothing
    }
    protected void grvTicketBPUsadosDiaMes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell th = new TableHeaderCell();
            th.ColumnSpan = 5;
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
        Response.AddHeader("Content-Disposition", "attachment; filename=RptTicketBP_DiaMes.xls");
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
        DataTable dest = new DataTable();
        string fechaEstadistico = "";
        htLabels = LabelConfig.htLabels;
       

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta_u = objBOReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde
                   , sFechaHasta, sAnnio + sMes, sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                   , sDestino, sTipReporte, null, 0, 0, "0");

            dt_resumen_u = objBOReporte.ListarTicketBoardingUsadosDiaMesPagin(sFechaDesde, sFechaHasta, sAnnio + sMes
                            , sTDocumento, sIdCompania, sNumVuelo, sTipoTicket
                            , sDestino, sTipReporte, null, 0, 0, "2");

            fechaEstadistico = objBOConsulta.obtenerFechaEstadistico("0");

            /*dest = new DataTable("Pivoted");

            DataColumn dc = new DataColumn("Tip_Documento");
            dest.Columns.Add(dc);
            dc = new DataColumn("Cantidad");
            dc.DataType = System.Type.GetType("System.Int32");
            dest.Columns.Add(dc);

            dest.Rows.Add(dest.NewRow());
            dest.Rows[0][0] = "BP";
            dest.Rows[0][1] = Convert.ToInt32((dt_resumen_u.Rows[0]["TotBoarding"] == null) ? "0" : dt_resumen_u.Rows[0]["TotBoarding"].ToString());
            dest.Rows.Add(dest.NewRow());
            dest.Rows[1][0] = "Ticket";
            dest.Rows[1][1] = Convert.ToInt32((dt_resumen_u.Rows[0]["TotTicket"] == null) ? "0" : dt_resumen_u.Rows[0]["TotTicket"].ToString());
            dest.Rows.Add(dest.NewRow());
            dest.Rows[2][0] = "Total";
            dest.Rows[2][1] = Convert.ToInt32((dt_resumen_u.Rows[0]["Total"] == null) ? "0" : dt_resumen_u.Rows[0]["Total"].ToString());
            dest.AcceptChanges();*/
          
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
        Excel.Worksheet Consulta= new Excel.Worksheet("Ticket BP Dia Mes");
        Consulta.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        Consulta.Columns = new string[] { "Fecha Uso", "Tipo Documento", "Tipo Ticket", "Aerolinea", "Nro. Vuelo", "BP", "Ticket", "Total" };
        Consulta.WidthColumns = new int[] { 60, 80, 130, 220, 70, 35, 35, 40 };
        Consulta.DataFields = new string[] { "Fecha_Uso", "Tipo_Documento", "Dsc_Tipo_Ticket", "Dsc_Compania", "Num_Vuelo", "BP", "Ticket", "Total" };
        Consulta.Source = dt_consulta_u;
        #endregion

        #region Resumen
        Excel.Worksheet Resumen = new Excel.Worksheet("Resumen");
        Resumen.FechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
        Resumen.Columns = new string[] { "Tipo Documento", "Tipo Venta" , "Tipo Vuelo" , "Tipo Pasajero" , "Tipo Trasbordo", "Cantidad" };
        Resumen.WidthColumns = new int[] { 60, 100,100,100,100,80 };
        Resumen.DataFields = new string[] { "Tip_Documento", "Tip_Venta","Tip_Vuelo","Tip_Pasajero","Tip_Trasbordo","Cantidad" };
        //Resumen.Source = dest;

        DataTable dtResumenTotal = new DataTable();
        dtResumenTotal = dt_resumen_u.Clone();
        int rows = dt_resumen_u.Rows.Count;
        DataRow fila;

        // Calculamos los totales por documento
        DataTable dt_Total = (DataTable)ViewState["Total"];
        strTipoDocumento = dt_resumen_u.Rows[0]["Tip_Documento"].ToString();
        int cont = 0;

        for (int k = 0; k < rows; k++)
        {
            if (strTipoDocumento != dt_resumen_u.Rows[k]["Tip_Documento"].ToString())
            {
                if (k == rows - 1) //es la ultima posicion
                {
                        fila = dt_resumen_u.Rows[k];
                        dtResumenTotal.ImportRow(fila);

                        string documento = dt_resumen_u.Rows[k]["Tip_Documento"].ToString();
                        string total = "";
                        if (documento == "Ticket")
                        {
                            total = dt_Total.Rows[0]["TotTicket"].ToString();
                        }
                        else
                        {
                            total = dt_Total.Rows[0]["TotBoarding"].ToString();
                        }
                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k + 2]["Tip_Venta"] = "Total";
                        dtResumenTotal.Rows[k + 2]["Cantidad"] = total;
                  
                }
                else
                {
                    string total = "";
                   // cont = 1;
                    if (cont == 0)
                    {
                        //adicionando subtotal documento previo
                        if (strTipoDocumento == "Ticket")
                        {
                            total = dt_Total.Rows[0]["TotTicket"].ToString();
                        }
                        else
                        {
                            total = dt_Total.Rows[0]["TotBoarding"].ToString();
                        }

                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k]["Tip_Venta"] = "Total";
                        dtResumenTotal.Rows[k]["Cantidad"] = total;

                        //adicionando el documento actual
                        fila = dt_resumen_u.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                        //cont++;
                    }
                    else
                    {
                        //adicionando el documento actual
                        fila = dt_resumen_u.Rows[k];
                        dtResumenTotal.ImportRow(fila);
                        //cont++;
                    }

                    cont++;
                 }
            }
            else
            {

                if (k == rows - 1)//ultima posicion en caso haya solo un tipo de documento
                {
                    fila = dt_resumen_u.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    string documento = dt_resumen_u.Rows[k]["Tip_Documento"].ToString();
                    string total = "";
                    if (documento == "Ticket")
                    {
                        total = dt_Total.Rows[0]["TotTicket"].ToString();
                    }
                    else
                    {
                        total = dt_Total.Rows[0]["TotBoarding"].ToString();
                    }
                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Venta"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Cantidad"] = total;
                }
                else
                {
                    fila = dt_resumen_u.Rows[k];
                    dtResumenTotal.ImportRow(fila);
                }

            }
        }

        Resumen.Source = dtResumenTotal;

        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { Consulta, Resumen };

        return Workbook.Save();
    }
}