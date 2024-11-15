using System;
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
using Define = LAP.TUUA.UTIL.Define;
using System.Collections.Generic;

public partial class Rpt_TicketBoardingRehabilitados : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected bool Flg_Error;
    string sTipoTicket;
    string sAerolinea;
    string sHoraDesde;
    string sHoraHasta;
    string sFechaDesde;
    string sFechaHasta;
    string sMotivo;
    string sNumVuelo;
    string sDocumento;
   

    DataTable dt_consulta = new DataTable();
    BO_Reportes objListarTicketContingenciaxFecha = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_allcompania = new DataTable();

    BO_Consultas objParametro = new BO_Consultas();
    Int32 valorMaxGrilla;
    DataTable dt_parametroTurno = new DataTable();

    string tipoDocumento = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblFechaDesde.Text = htLabels["reporteTicketBoardingRehabilitado.lblFechaDesde.Text"].ToString();
                this.lblFechaHasta.Text = htLabels["reporteTicketBoardingRehabilitado.lblFechaHasta.Text"].ToString();
                this.lblTipoTicket.Text = htLabels["reporteTicketBoardingRehabilitado.lblTipoTicket.Text"].ToString();
                this.btnConsultar.Text = htLabels["reporteTicketBoardingRehabilitado.btnConsultar.Text"].ToString();
                this.lblAerolinea.Text = htLabels["reporteTicketBoardingRehabilitado.lblAerolinea.Text"].ToString();
                this.lblMotivo.Text = htLabels["reporteTicketBoardingRehabilitado.lblMotivo.Text"].ToString();
                this.lblVuelo.Text = htLabels["reporteTicketBoardingRehabilitado.lblVuelo.Text"].ToString();
                this.lblFecha.Text = htLabels["reporteTicketBoardingRehabilitado.lblFecha.Text"].ToString();
                this.lblDocumento.Text = htLabels["reporteTicketBoardingRehabilitado.lblDocumento.Text"].ToString();

                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
                Session["TicketBoardingRehabil"] = null;
                CargarCombos();
            }
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
    }

    public void CargarCombos()
    {
        try
        {
            // carga combo Estado ticket
            BO_Consultas objListaCampos = new BO_Consultas();
            DataTable dt_estado = new DataTable();
            dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");
            DataTable dtCausalReh = objListaCampos.ListaCamposxNombre("CausalRehabilitacion");

            // carga combo Tipo ticket
            BO_Administracion objListaTipoTicket = new BO_Administracion();
            DataTable dt_tipoticket = new DataTable();
            UIControles objCargaCombo = new UIControles();

            dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
            objCargaCombo.LlenarCombo(ddlTipoTicket, dt_tipoticket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true, false);

            dt_allcompania = objConsulta.listarAllCompania();
            objCargaCombo.LlenarCombo(ddlTipoAerolinea, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            objCargaCombo.LlenarCombo(ddlMotivo, dtCausalReh, "Cod_Campo", "Dsc_Campo", true, false);
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
        CargarGrillaTicketContigencia();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
    }

    protected void CargarGrillaTicketContigencia()
    {
        try
        {
            htLabels = LabelConfig.htLabels;
            if (validarDatos())
            {
                RecuperarFiltros();
                dt_consulta = objListarTicketContingenciaxFecha.obtenerTicketBoardingRehabilitados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDocumento, sTipoTicket, sAerolinea, sNumVuelo, sMotivo);

                if (dt_consulta.Rows.Count < 1)
                {
                    htLabels = LabelConfig.htLabels;
                    try
                    {
                        //this.lblTotal.Text = "";
                        //this.lblMensajeError.Visible = false;
                        this.lblMensajeErrorData.Visible = true;
                        this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                        this.lblTotalRows.Value = "";
                        grvResumen.DataSource = null;
                        grvResumen.DataBind();
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
                    grvTicketBoardingRehabilita.DataSource = null;
                    grvTicketBoardingRehabilita.DataBind();
                }
                else
                {
                    /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                    if (dt_consulta.Columns[0].ColumnName == "MsgError")
                    {
                        //this.lblTotal.Text = "";
                        this.lblMensajeError.Visible = false;
                        this.lblMensajeErrorData.Visible = true;
                        this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                        this.lblTotalRows.Value = "";
                        grvTicketBoardingRehabilita.DataSource = null;
                        grvTicketBoardingRehabilita.DataBind();

                    }
                    else
                    {
                        ViewState["tablaTicket"] = dt_consulta;
                        //DataTable prueba = groupby();
                        //ViewState["Resumen"] = groupby();
                        cargarResumen();
                        ValidarTamanoGrilla();

                        //Cargar datos en la grilla
                        this.lblMensajeErrorData.Text = "";
                        //this.lblTotal.Text = htLabels["consTicketProcesado.lblTotal"].ToString() + " " + dt_consulta.Rows.Count;
                        this.lblTotalRows.Value = dt_consulta.Rows.Count.ToString();
                        this.grvTicketBoardingRehabilita.DataSource = dt_consulta;
                        this.grvTicketBoardingRehabilita.AllowPaging = true;
                        this.grvTicketBoardingRehabilita.PageSize = valorMaxGrilla;
                        this.grvTicketBoardingRehabilita.DataBind();
                        //cargarResumen();
                    }
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
        }

    }


    public DataTable groupby()
    {
        DataView dv = new DataView(dt_consulta);

        string[] groupcolumns = new string[]{"DesDocument", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo" };

        DataTable dtGroup = dv.ToTable(true, groupcolumns);
        string sCondicion;
        dtGroup.Columns.Add("Cantidad", typeof(int));

        foreach (DataRow dr in dtGroup.Rows)
        { 
            sCondicion = "";
            for(int i=0; i< groupcolumns.Length;i++){
            sCondicion += groupcolumns[i] + "= '" + dr[groupcolumns[i]] + "'";
                if(i < groupcolumns.Length - 1){
                sCondicion += " AND ";
                }
            }

            dr["Cantidad"] = dt_consulta.Compute("Count(Cod_Numero_Ticket)",sCondicion);
        }

        return dtGroup;
    }

    public void cargarResumen()
    {
        /*DataView dv = new DataView();
        

        DataRow[] dtTicket = dt_consulta.Select("DesDocument = 'TICKET'");
        int iCantTicket = dtTicket.Length;
        DataRow[] dtBoarding = dt_consulta.Select("DesDocument = 'BOARDING' ");
        int iCantBoarding = dtBoarding.Length;

        DataTable dtReportResumen = new DataTable();
        DataColumn dc;
        dc = new DataColumn("TIPO DOCUMENTO", System.Type.GetType("System.String"));
        dtReportResumen.Columns.Add(dc);
        dc = new DataColumn("CANTIDAD", System.Type.GetType("System.Int32"));
        dtReportResumen.Columns.Add(dc);

        DataRow nRow = dtReportResumen.NewRow();
        nRow["TIPO DOCUMENTO"] = "TICKET";
        nRow["CANTIDAD"] = iCantTicket;
        dtReportResumen.Rows.Add(nRow);

        nRow = dtReportResumen.NewRow();
        nRow["TIPO DOCUMENTO"] = "BOARDING";
        nRow["CANTIDAD"] = iCantBoarding;
        dtReportResumen.Rows.Add(nRow);

        nRow = dtReportResumen.NewRow();
        nRow["TIPO DOCUMENTO"] = "TOTALES";
        nRow["CANTIDAD"] = iCantBoarding + iCantTicket;
        dtReportResumen.Rows.Add(nRow);*/
        

        DataTable dtResumen = groupby();
        tipoDocumento = dtResumen.Rows[0]["DesDocument"].ToString();
        ViewState["Resumen"] = dtResumen;
        cargarSubTotal();
        grvResumen.DataSource = dtResumen;
        grvResumen.DataBind();
    }

    public void cargarSubTotal() {
        
        DataView dv = new DataView();

        //dt_consulta = ((DataTable)ViewState["tablaTicket"]).Copy();
        DataTable dt = dt_consulta.Copy();

        DataRow[] dtTicket = dt.Select("DesDocument = 'TICKET'");
        int iCantTicket = dtTicket.Length;
        DataRow[] dtBoarding = dt.Select("DesDocument = 'BOARDING' ");
        int iCantBoarding = dtBoarding.Length;

        DataTable dtReportResumen = new DataTable();
        DataColumn dc;
        dc = new DataColumn("Tipo_Documento", System.Type.GetType("System.String"));
        dtReportResumen.Columns.Add(dc);
        dc = new DataColumn("Total", System.Type.GetType("System.Int32"));
        dtReportResumen.Columns.Add(dc);

        DataRow nRow = dtReportResumen.NewRow();
        nRow["Tipo_Documento"] = "TICKET";
        nRow["Total"] = iCantTicket;
        dtReportResumen.Rows.Add(nRow);

        nRow = dtReportResumen.NewRow();
        nRow["Tipo_Documento"] = "BOARDING";
        nRow["Total"] = iCantBoarding;
        dtReportResumen.Rows.Add(nRow);

        //nRow = dtReportResumen.NewRow();
        //nRow["TIPO DOCUMENTO"] = "TOTALES";
        //nRow["CANTIDAD"] = iCantBoarding + iCantTicket;
        //dtReportResumen.Rows.Add(nRow);

        ViewState["SubTotal"] = dtReportResumen; 
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtFechaDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtFechaHasta.Text)));
        filterList.Add(new Filtros("sTipoTicket", this.ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sAerolinea", this.ddlTipoAerolinea.SelectedValue));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sMotivo", this.ddlMotivo.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", this.txtNumVuelo.Text));
        
        if (ckTicket.Checked)     sDocumento = "1"; //Ticket
        if (ckBoarding.Checked)   sDocumento = "2"; //Boarding
        if (ckTicket.Checked && ckBoarding.Checked)  sDocumento = "0";

        filterList.Add(new Filtros("sDocumento", sDocumento));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sTipoTicket = newFilterList[2].Valor;
        sAerolinea = newFilterList[3].Valor;
        sHoraDesde = newFilterList[4].Valor;
        sHoraHasta = newFilterList[5].Valor;
        sMotivo = newFilterList[6].Valor;
        sNumVuelo = newFilterList[7].Valor;
        sDocumento = newFilterList[8].Valor;
    }

    public bool validarDatos()
    {
        int result = DateTime.Compare(Convert.ToDateTime(this.txtFechaDesde.Text), Convert.ToDateTime(this.txtFechaHasta.Text));
        if (result == 1)
        {
            this.lblMensajeError.Text = "Filtro de fecha invalido";
            //this.txtFechaHasta.Text = "";
            return false;
        }
        else
        {
            this.lblMensajeError.Text = "";
            return true;
        }
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general              
        dt_parametroTurno = objParametro.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }


    protected void grvTicketBoardingRehabilita_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = grvTicketBoardingRehabilita.PageSize;
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tablaTicket"];
        DataTable dtResumenRehabilitados = (DataTable)ViewState["Resumen"];

        tipoDocumento = dtResumenRehabilitados.Rows[0]["DesDocument"].ToString();

        grvTicketBoardingRehabilita.PageIndex = e.NewPageIndex;
        grvTicketBoardingRehabilita.DataSource = dtTicketRehabilitados;
        grvResumen.DataSource = dtResumenRehabilitados;
        grvResumen.DataBind();
        grvTicketBoardingRehabilita.DataBind();
    }


    protected void grvTicketBoardingRehabilita_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tablaTicket"];
        if (dtTicketRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;
        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tablaTicket"] = SortDataTable(dtTicketRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtTicketRehabilitados = (DataTable)ViewState["tablaTicket"];
        grvTicketBoardingRehabilita.DataSource = (DataTable)ViewState["tablaTicket"];
        grvTicketBoardingRehabilita.DataBind();

    }


    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}", "LastName", "ASC");
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }


    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }


    protected void grvTicketBoardingRehabilita_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleTicket = (DataTable)ViewState["tablaTicket"];// ViewState["tablaTicket"];


            String desDocumento = dtDetalleTicket.Rows[rowIndex]["DesDocument"].ToString();
            if (desDocumento == "TICKET")
            {
                String codigo = dtDetalleTicket.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
                ConsDetTicket1.Inicio(codigo);
            }
            else
            {
                String codigo = dtDetalleTicket.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
                CnsDetBoarding1.CargarDetalleBoarding(codigo);
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
            th.ColumnSpan = 5;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen:";
            row.Cells.Add(th);

            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }

    protected void grvResumen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;

            if (tipoDocumento != drv["DesDocument"].ToString())
            {
                // Calculamos los totales por documento
                DataTable dt_Total = (DataTable)ViewState["SubTotal"];
                object total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

                Table tbl = e.Row.Parent as Table;

                if (tbl != null)
                {
                    GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                    TableCell cell = new TableCell();

                    #region fila total
                    cell.ColumnSpan = this.grvResumen.Columns.Count - 3;
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Font.Bold = true;
                    cell.Style.Add("background-color", "#CCCCCC");

                    HtmlGenericControl span = new HtmlGenericControl("span");
                    span.InnerHtml = "Total"; //"Total";//Total documento
                    cell.Controls.Add(span);

                    row.Cells.Add(cell);

                    //cell = new TableCell();
                    //cell.ColumnSpan = 1;
                    //cell.Style.Add("background-color", "#CCCCCC");
                    //row.Cells.Add(cell);

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
                tipoDocumento = drv["DesDocument"].ToString();
            }
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            // Calculamos los totales por documento

            DataTable dt_Total = (DataTable)ViewState["SubTotal"];

            object total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();

            Table tbl = e.Row.Parent as Table;

            if (tbl != null)
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                TableCell cell = new TableCell();

                #region fila total
                cell.ColumnSpan = this.grvResumen.Columns.Count - 3;
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.Font.Bold = true;
                cell.Style.Add("background-color", "#CCCCCC");

                HtmlGenericControl span = new HtmlGenericControl("span");
                span.InnerHtml = "Total"; //"Total";//Total documento
                cell.Controls.Add(span);

                row.Cells.Add(cell);

                //cell = new TableCell();
                //cell.ColumnSpan = 1;
                //cell.Style.Add("background-color", "#CCCCCC");
                //row.Cells.Add(cell);

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


    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TickeBPRehabilitados.xls");
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
            dt_consulta = objListarTicketContingenciaxFecha.obtenerTicketBoardingRehabilitados(sFechaDesde,
                                                                                                sFechaHasta,
                                                                                                sHoraDesde,
                                                                                                sHoraHasta,
                                                                                                sDocumento,
                                                                                                sTipoTicket,
                                                                                                sAerolinea,
                                                                                                sNumVuelo,
                                                                                                sMotivo);

            //DataRow[] dtTicket = dt_consulta.Select("DesDocument = 'TICKET'");
            //int iCantTicket = dtTicket.Length;
            //DataRow[] dtBoarding = dt_consulta.Select("DesDocument = 'BOARDING'");
            //int iCantBoarding = dtBoarding.Length;

            //DataColumn dc;
            //dc = new DataColumn("TIPO_DOCUMENTO", System.Type.GetType("System.String"));
            //dt_resumen.Columns.Add(dc);
            //dc = new DataColumn("CANTIDAD", System.Type.GetType("System.Int32"));
            //dt_resumen.Columns.Add(dc);

            //DataRow nRow = dt_resumen.NewRow();
            //nRow["TIPO_DOCUMENTO"] = "TICKET";
            //nRow["CANTIDAD"] = iCantTicket;
            //dt_resumen.Rows.Add(nRow);

            //nRow = dt_resumen.NewRow();
            //nRow["TIPO_DOCUMENTO"] = "BOARDING";
            //nRow["CANTIDAD"] = iCantBoarding;
            //dt_resumen.Rows.Add(nRow);

            //nRow = dt_resumen.NewRow();
            //nRow["TIPO_DOCUMENTO"] = "TOTALES";
            //nRow["CANTIDAD"] = iCantBoarding + iCantTicket;
            //dt_resumen.Rows.Add(nRow);

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
        Excel.Worksheet Consulta = new Excel.Worksheet("TickeBPRehabilitados");
        Consulta.Columns = new string[] { "Fecha Venta", "Fecha Rehab", "Hora Rehab", "Compañía", "Nro Vuelo", "Motivo", "Documento", "Nro. Documento", "Secuencial" };
        Consulta.WidthColumns = new int[] { 70, 80, 70, 160, 65, 130, 70, 100, 80 };
        Consulta.DataFields = new string[] { "@FechaVenta", "@FechaRehab", "HoraRehab", "Dsc_Compania", "NumVuelo", "DesMotivo", "DesDocument", "Cod_Numero_Ticket", "Secuencial" };
        Consulta.Source = dt_consulta;
        #endregion
        
        #region Resumen Consulta
        Excel.Worksheet Resumen = new Excel.Worksheet("Resumen");
        Resumen.Columns = new string[] { "Tipo Documento", "Tipo Vuelo", "Tipo Pasajero", "Tipo Trasbordo", "Cantidad" };
        Resumen.WidthColumns = new int[] { 100, 100, 100, 100, 80 };
        Resumen.DataFields = new string[] { "DesDocument", "Tip_Vuelo", "Tip_Pasajero", "Tip_Trasbordo", "Cantidad" };

        DataTable dtReportResumen = (DataTable)ViewState["Resumen"];
        DataTable dtResumenTotal = dtReportResumen.Clone();
        int rows = dtReportResumen.Rows.Count;
        DataRow fila;

        // Calculamos los totales por documento

        DataTable dt_Total = (DataTable)ViewState["SubTotal"];
        tipoDocumento = dtReportResumen.Rows[0]["DesDocument"].ToString();
        int cont = 0;
        string documento = "";
        object total = null;
  
        for (int k = 0; k < rows; k++)
        {
            if (tipoDocumento != dtReportResumen.Rows[k]["DesDocument"].ToString())
            {
                if (k == rows - 1) //es la ultima posicion
                {
                    fila = dtReportResumen.Rows[k];
                    dtResumenTotal.ImportRow(fila);

                    documento = dtReportResumen.Rows[k]["DesDocument"].ToString();
                    total = dt_Total.Select("Tipo_Documento = '" + documento + "'")[0]["Total"].ToString();

                    
                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 2]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k + 2]["Cantidad"] = total.ToString();
                }
                else
                {
                    if (cont == 0) //si es por primera vez
                    {
                        //adicionando subtotal documento previo
                        total = dt_Total.Select("Tipo_Documento = '" + tipoDocumento + "'")[0]["Total"].ToString();
                        
                        dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                        dtResumenTotal.Rows[k]["Tip_Vuelo"] = "Total";
                        dtResumenTotal.Rows[k]["Cantidad"] = total.ToString();
        
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

                    documento = dtReportResumen.Rows[k]["DesDocument"].ToString();
                    total = dt_Total.Select("Tipo_Documento = '" + documento + "'")[0]["Total"].ToString();
                    
                    dtResumenTotal.Rows.Add(dtResumenTotal.NewRow());
                    dtResumenTotal.Rows[k + 1]["Tip_Vuelo"] = "Total";
                    dtResumenTotal.Rows[k + 1]["Cantidad"] = total.ToString();                   
                }
                else
                {
                    fila = dtReportResumen.Rows[k];
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
