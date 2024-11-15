///V.1.4.6.0
///Luz Huaman
///Copyright ( Copyright © HIPER S.A. )
///
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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.Text;

public partial class Cns_BoardingUsados : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;

    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    DataTable dt_Consulta = new DataTable();

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sTipoDocumento;
    string sEstado;
    string sIdCompania;
    string sTipoPasajero;
    string sTipoVuelo;
    string sTipoTrasbordo;
    string sNumVuelo;
    string sFechaVuelo;

    #region Event - Handlers
    //<sumarry> Durante la carga, si la solicitud actual es una devolución de datos, <sumarry> 
    //<sumarry>  las propiedades del control se cargan con información recuperada del estado de vista y del estado del control.<sumarry> 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblDesde.Text = htLabels["mconsultaTicketFecha.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaTicketFecha.lblHasta.Text"].ToString();
                this.lblCompania.Text = htLabels["mconsultaBoardingUsados.lblCompania.Text"].ToString();
                this.lblTipoVuelo.Text = htLabels["mconsultaBoardingUsados.lblTipoVuelo.Text"].ToString();
                this.lblNumVuelo.Text = htLabels["mconsultaBoardingUsados.lblNumVuelo.Text"].ToString();
                this.lblTipoPasajero.Text = htLabels["mconsultaBoardingUsados.lblTipoPasajero.Text"].ToString();
                this.lblTipoDocumento.Text = htLabels["mconsultaBoardingUsados.lblTipoDocumento.Text"].ToString();
                this.lblTipoTrasbordo.Text = htLabels["mconsultaBoardingUsados.lblTipoTrasbordo.Text"].ToString();
                this.lblFechaVuelo.Text = htLabels["mconsultaBoardingUsados.lblFechaVuelo.Text"].ToString();
                this.lblEstado.Text = htLabels["mconsultaBoardingUsados.lblEstado.Text"].ToString();

                this.btnConsultar.Text = htLabels["mconsultaTicketFecha.btnConsultar.Text"].ToString();

                
                Session["Data_TicketBPUsado"] = null;
                Session["Flg_DataTBPUso"] = "0";
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
          
            CargarFiltros();

         
            DataTable dt_parametro = objBOConsultas.ListarParametros("AA");

            if (dt_parametro.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()) == 0)
                {

                    grvTicketUsados.Columns[15].Visible = false;
                   
                }

            }

            this.lblMensajeErrorData.Text = "";
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = "";
            this.lblTotalRows.Value = "";

            SaveFiltros();
            BindPagingGrid();
        }
    }

    //<sumarry> se produce cuando se hace clic en un botón de un control GridView.<sumarry> 
    protected void grvTicketUsados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTipoDocumento")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            // <sumarry> Retrieve the row that contains the button clicked <sumarry> 
            // <sumarry> by the user from the Rows collection.<sumarry> 
            GridViewRow row = grvTicketUsados.Rows[rowIndex];

            string codDocumento = row.Cells[2].Text;
            LinkButton addButton = (LinkButton)row.Cells[0].FindControl("codTipoDocumento");

            if (codDocumento == "Ticket")
            {
                ConsDetTicket1.Inicio(addButton.Text + "-Cns");
            }
            else
            {
                string sCodBase = ((Label)row.Cells[16].FindControl("Num_Secuencial_Bcbp_Rel")).Text;
                if (sCodBase != null && sCodBase != String.Empty && sCodBase != "0")
                    CnsDetBoarding1.CargarDetalleBoarding(((Label)row.Cells[16].FindControl("Num_Secuencial_Bcbp_Rel")).Text);
                else
                    CnsDetBoarding1.CargarDetalleBoarding(((Label)row.Cells[16].FindControl("Num_Secuencial_Bcbp")).Text);
            }
        }
    }

    // <sumarry> se produce cuando una fila de datos se enlaza a los datos de un control GridView.
    protected void grvTicketUsados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Estado"].ToString().TrimEnd() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Estado"].ToString().TrimEnd() == "R")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
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

    // <sumarry> Se produce cuando se hace clic en el control Button
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        //falta validar formulario
        SaveFiltros();
        BindPagingGrid();
        lblMaxRegistros.Value = GetMaximoExcel().ToString();
    }

    // <sumarry> se produce cuando la selección del control de lista cambia entre cada envío al servidor.
    protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        DataTable dt_estado_cmb = (DataTable)ViewState["Estado"];
        ddlEstado.DataSource = dt_estado_cmb;
        dt_estado_cmb.DefaultView.Sort = dt_estado_cmb.Columns["Dsc_Campo"].ColumnName.ToString();
        ddlEstado.SelectedValue = "0";
        ddlEstado.DataBind();

   
    }
    #endregion

    #region Cargar/Guardas Filtros de Consulta
    public void CargarFiltros()
    {
        try
        {
            //Carga filtro Compañia            
            DataTable dt_allcompania = objBOConsultas.listarAllCompania();
            objCargaCombo.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            //Carga filtro Tipo Vuelo
            DataTable dt_tipovuelo = objBOConsultas.ListaCamposxNombre("TipoVuelo");
            objCargaCombo.LlenarCombo(ddlTipoVuelo, dt_tipovuelo, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga filtro Tipo Pasajero          
            DataTable dt_tipopasajero = objBOConsultas.ListaCamposxNombre("TipoPasajero");
            objCargaCombo.LlenarCombo(ddlTipoPasajero, dt_tipopasajero, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga filtro Tipo Documento                        
            DataTable dt_tipodocumento = objBOConsultas.ListaCamposxNombre("TipoDocumento");
            objCargaCombo.LlenarCombo(ddlTipoDocumento, dt_tipodocumento, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga filtro Tipo Trasbordo                   
            DataTable dt_tipotrasbordo = objBOConsultas.ListaCamposxNombre("TipoTrasbordo");
            objCargaCombo.LlenarCombo(ddlTipoTrasbordo, dt_tipotrasbordo, "Cod_Campo", "Dsc_Campo", true, false);

            CargarFiltroEstados();
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

    protected void CargarFiltroEstados()
    {
        //Carga filtro Estado Ticket
        //BO_Consultas objListaCampos = new BO_Consultas();
        DataTable dt_estado = new DataTable();
        DataRow newEstado;
        DataRowCollection rows;
        //string strTipDoc = ddlTipoDocumento.SelectedValue.ToString();
       /* if (strTipDoc == Define.TIP_DOC_BOARDING)
        {
            dt_estado = objListaCampos.ListaCamposxNombre("EstadoBcbp");
        }*/
        //else
        //{
           // dt_estado = objListaCampos.ListaCamposxNombre("EstadoBcbp");                
      //  }

        dt_estado.Columns.Add("Cod_Campo");
        dt_estado.Columns.Add("Dsc_Campo");

        rows = dt_estado.Rows;

        newEstado = dt_estado.NewRow();
        newEstado["Cod_Campo"] = "U";
        newEstado["Dsc_Campo"] = "USADO";
        rows.Add(newEstado);
 
        newEstado = dt_estado.NewRow();
        newEstado["Cod_Campo"] = "R";
        newEstado["Dsc_Campo"] = "REHABILITADO";
        rows.Add(newEstado);

        //Agregamos a la tabla estado el tipo VENCIDO
        newEstado = dt_estado.NewRow();

        newEstado["Cod_Campo"] = "1";
        newEstado["Dsc_Campo"] = "VENCIDO";
        rows.Add(newEstado);

        dt_estado.Rows.Add(rows);

        dt_estado.Rows.RemoveAt(3);
        //Cargar combo Estado de ticket
        UIControles objCargaComboEstTicket = new UIControles();
        objCargaComboEstTicket.LlenarCombo(ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);
        ViewState["Estado"] = dt_estado;
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sIdCompania", ddlCompania.SelectedValue));
        filterList.Add(new Filtros("sTipoVuelo", ddlTipoVuelo.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", txtNumVuelo.Text));
        filterList.Add(new Filtros("sTipoPasajero", ddlTipoPasajero.SelectedValue));
        filterList.Add(new Filtros("sTipoDocumento", ddlTipoDocumento.SelectedValue));

        filterList.Add(new Filtros("sTipoTrasbordo", ddlTipoTrasbordo.SelectedValue));
        filterList.Add(new Filtros("sFechaVuelo", Fecha.convertToFechaSQL2(txtFechaVuelo.Text)));
        filterList.Add(new Filtros("sEstado", ddlEstado.SelectedValue));
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));

        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        //sFechaDesde = newFilterList[0].Valor;
        sIdCompania = newFilterList[0].Valor;
        sTipoVuelo = newFilterList[1].Valor;
        sNumVuelo = newFilterList[2].Valor;
        sTipoPasajero = newFilterList[3].Valor;
        sTipoDocumento = newFilterList[4].Valor;
        sTipoTrasbordo = newFilterList[5].Valor;
        sFechaVuelo = newFilterList[6].Valor;
        sEstado = newFilterList[7].Valor;

        sFechaDesde = newFilterList[8].Valor;
        sFechaHasta = newFilterList[9].Valor;
        sHoraDesde = newFilterList[10].Valor;
        sHoraHasta = newFilterList[11].Valor;

    }


    #endregion

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvTicketUsados.VirtualItemCount = GetRowCount();
        DataTable dt_consulta = GetDataPage(grvTicketUsados.PageIndex, grvTicketUsados.PageSize, grvTicketUsados.OrderBy);

        htLabels = LabelConfig.htLabels;
        if (dt_consulta.Rows.Count < 1)
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
            grvTicketUsados.DataSource = null;
            grvTicketUsados.DataBind();
            this.lblTotal.Text = "";
            this.lblTotalRows.Value = "";

            Session["Flg_DataTBPUso"] = "0";
            this.hlinkResumen.Disabled = true;
            this.hlinkResumen.InnerHtml = "";
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            //grvTicketUsados.Visible = true;
            grvTicketUsados.DataSource = dt_consulta;
            grvTicketUsados.PageSize = Convert.ToInt32(this.txtValorMaximoGrilla.Text);
            grvTicketUsados.DataBind();

            Session["Flg_DataTBPUso"] = "1";
            hlinkResumen.Disabled = false;

            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + grvTicketUsados.VirtualItemCount;
            this.lblTotalRows.Value = grvTicketUsados.VirtualItemCount.ToString();
            this.hlinkResumen.InnerHtml = " * Ver Resumen";
        }
    }

    private int GetRowCount()
    {
        int count = 0;

        DataTable dt_consulta = objBOConsultas.ListarTicketBoardingUsadosPagin(sFechaDesde, 
            sFechaHasta, sHoraDesde, sHoraHasta, sIdCompania, sTipoVuelo, sNumVuelo, 
            sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, null, 0, 0, "1");
        if (dt_consulta.Columns.Contains("TotRows"))
            count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
        else
            lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        //ValidarTamanoGrilla();
        Session["sortExpressionTicketBPXFecha"] = sortExpression;

        DataTable dt_consulta = objBOConsultas.ListarTicketBoardingUsadosPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIdCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, sortExpression, pageIndex, Convert.ToInt32(this.txtValorMaximoGrilla.Text), "0");

        ViewState["tablaTicket"] = dt_consulta;

        return dt_consulta;
    }
    #endregion

    void ValidarTamanoGrilla()
    {
        //<sumarry>Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales<sumarry>
        DataTable dt_parametrogeneral = objBOConsultas.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.txtValorMaximoGrilla.Text = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    #region Paginacion
    // se produce cuando se hace clic en uno de los botones de paginación, pero antes de que el control GridView se ocupe de la operación de paginación.
    protected void grvTicketUsados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
        grvTicketUsados.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }
    #endregion

    #region Ordenacion
    // se produce cuando se hace clic en el hipervínculo para ordenar una columna, pero antes de que el control GridView se ocupe de la operación de ordenación.
    protected void grvTicketUsados_Sorting(object sender, GridViewSortEventArgs e)
    {
        
        BindPagingGrid();
    }
    #endregion

    protected void crvResTipoDocumento_Unload(object sender, EventArgs e)
    {
      
    }

    protected void hlinkResumen_Click(object sender, EventArgs e)
    {
        string strFlgData = (string)Session["Flg_DataTBPUso"];
        if (strFlgData.Trim() == "1")
        {
            RecuperarFiltros();
            this.ResumenTicketBPUsados1.Inicio(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIdCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
          
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=BPDiarios.xls");
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

        RecuperarFiltros();

        
        dt_consulta = objBOConsultas.ListarTicketBoardingUsadosPagin(sFechaDesde,
            sFechaHasta, sHoraDesde, sHoraHasta, sIdCompania, sTipoVuelo, sNumVuelo,
            sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, null, 0, 0, "0");

        System.IO.StringWriter excelDoc;

        excelDoc = new System.IO.StringWriter();
        StringBuilder ExcelXML = new StringBuilder();

        #region CABECERA DEL ARCHIVO
        ExcelXML.Append("<?xml version=\"1.0\"?>\n");
        ExcelXML.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
        ExcelXML.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
        ExcelXML.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n");
        ExcelXML.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n");
        ExcelXML.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
        ExcelXML.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
        ExcelXML.Append("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\n");
        ExcelXML.Append("<Author>Eder Ochoa</Author>\n");
        ExcelXML.Append("<LastAuthor>Eder Ochoa</LastAuthor>\n");
        ExcelXML.Append("<Created>2011-01-10T16:34:47Z</Created>\n");
        ExcelXML.Append("<Version>12.00</Version>\n");
        ExcelXML.Append(" </DocumentProperties>\n");
        ExcelXML.Append(" <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
        ExcelXML.Append("<WindowHeight>7935</WindowHeight>\n");
        ExcelXML.Append("<WindowWidth>20055</WindowWidth>\n");
        ExcelXML.Append("<WindowTopX>240</WindowTopX>\n");
        ExcelXML.Append("<WindowTopY>75</WindowTopY>\n");
        ExcelXML.Append(" <ProtectStructure>False</ProtectStructure>\n");
        ExcelXML.Append("<ProtectWindows>False</ProtectWindows>\n");
        ExcelXML.Append("</ExcelWorkbook>\n");
        ExcelXML.Append(" <Styles>\n");
        ExcelXML.Append("<Style ss:ID=\"s66\">\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
        ExcelXML.Append(" ss:Underline=\"Single\"/>\n");
        ExcelXML.Append("</Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s76\">\n");
        ExcelXML.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>\n");
        ExcelXML.Append("<Borders>\n");
        ExcelXML.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("</Borders>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
        ExcelXML.Append(" ss:Bold=\"1\" ss:Underline=\"Single\"/>\n");
        ExcelXML.Append("<Interior ss:Color=\"#D8D8D8\" ss:Pattern=\"Solid\"/>\n");
        ExcelXML.Append("</Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s77\">\n");
        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
        ExcelXML.Append("<Borders/>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
        ExcelXML.Append("<Interior ss:Color=\"#F2F2F2\" ss:Pattern=\"Solid\"/>\n");
        ExcelXML.Append("<NumberFormat/>\n");
        ExcelXML.Append("<Protection/>\n");
        ExcelXML.Append(" </Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s78\">\n");
        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
        ExcelXML.Append("<Borders/>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
        ExcelXML.Append("<Interior />\n");
        ExcelXML.Append("<NumberFormat/>\n");
        ExcelXML.Append("<Protection/>\n");
        ExcelXML.Append(" </Style>\n");
        ExcelXML.Append(" </Styles>\n");
        #endregion

        string startExcelXML = ExcelXML.ToString();

        excelDoc.Write(startExcelXML);
   
            #region Consulta Ticket
            excelDoc.Write("<Worksheet ss:Name=\"Cns Ticket_BP Usado\">\n");
            excelDoc.Write("<Table ss:ExpandedColumnCount=\"" + dt_consulta.Columns.Count + "\" ss:ExpandedRowCount=\"" + (dt_consulta.Rows.Count + 1) + "\" x:FullColumns=\"1\"\n");
            excelDoc.Write(" x:FullRows=\"1\" ss:DefaultColumnWidth=\"60\" ss:DefaultRowHeight=\"15\">\n");

            #region TAMAÑO DE LAS COLUMNAS
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"70\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"70\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"60\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            #endregion

            #region CABECERA
            excelDoc.Write("<Row ss:AutoFitHeight=\"0\" ss:Height=\"30\">\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Documento</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Secuencial</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Documento</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Destino</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Modalidad Venta</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Aerolínea</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fecha Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fecha Uso</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Asiento</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Persona</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Trasbordo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Estado Actual</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Secuencia</Data></Cell>\n");
        
        
            excelDoc.Write("</Row>\n");
            #endregion

            //DATA
            int fila = 0;
            string estilo = "";
            int num = 0;

            foreach (DataRow row in dt_consulta.Rows)
            {
                excelDoc.Write("<Row ss:AutoFitHeight=\"0\">");

                if (fila % 2 != 0)
                    estilo = "s78";
                else
                    estilo = "s77";

                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Codigo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Correlativo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Documento"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Destino"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Nom_Modalidad"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Compania"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Num_Vuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + Fecha.convertSQLToFecha3(dt_consulta.Rows[fila]["Fch_Vuelo"].ToString()) + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + Fecha.convertSQLToFecha(dt_consulta.Rows[fila]["Log_Fecha_Mod"].ToString(),dt_consulta.Rows[fila]["Log_Hora_Mod"].ToString()) + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["NroAsiento"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_TipoVuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_TipoPasajero"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Trasbordo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Estado"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Num_Secuencial"] + "</Data></Cell>");
                excelDoc.Write("</Row>");

                fila++;
            }

            excelDoc.Write("</Table>\n");
            excelDoc.Write("</Worksheet>\n");
            #endregion
        

        excelDoc.Write("</Workbook>\n");

        return excelDoc;
    }

    protected void grvTicketUsados_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
