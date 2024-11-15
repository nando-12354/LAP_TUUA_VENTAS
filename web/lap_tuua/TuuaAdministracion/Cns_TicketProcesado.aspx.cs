using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using Define = LAP.TUUA.UTIL.Define;

public partial class Cns_TicketProcesado : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    string sFechaDesde;
    string sFechaHasta;
    string sCajero;
    string sTurno;

    protected BO_Consultas objBOConsulta = new BO_Consultas();

    Int32 valorMaxGrilla;
    DataTable dt_parametroGeneral = new DataTable();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //SetSizeH(Int32.Parse( (string )Session["Page_Height"] ));            

            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblFechaTitulo.Text = htLabels["consTicketProcesado.lblFechaTitulo"].ToString();
                this.lblFechaDesde.Text = htLabels["consTicketProcesado.lblFechaInicial"].ToString();
                this.lblFechaHasta.Text = htLabels["consTicketProcesado.lblFechaFinal"].ToString();
                this.lblCajero.Text = htLabels["consTicketProcesado.lblCajero"].ToString();
                this.lblTurno.Text = htLabels["consTicketProcesado.lblTurno"].ToString();
                this.btnConsultar8.Text = htLabels["consTicketProcesado.btnConsultar.Text"].ToString();

                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();

                this.lblMensajeError.Visible = false;
                this.lblMensajeErrorData.Visible = false;

                CargarCombos();

                string sRet = Request.QueryString["ret"];
                RestoreFiltro(sRet);

                CargarGrillaTurnos();
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

    private void RestoreFiltro(string strFlag)
    {
        if (strFlag != null && strFlag == "1")
        {
            Hashtable htFiltro = (Hashtable)Session["E4110"];
            if (htFiltro != null)
            {
                if (htFiltro["FechaIni"] != null)
                    this.txtFechaDesde.Text = (string)htFiltro["FechaIni"];
                if (htFiltro["FechaFin"] != null)
                    this.txtFechaHasta.Text = (string)htFiltro["FechaFin"];
                if (htFiltro["Cajero"] != null)
                    this.ddlCajero.SelectedValue = (string)htFiltro["Cajero"];
                if (htFiltro["Turno"] != null)
                    this.txtTurno.Text = (string)htFiltro["Turno"];
            }
        }
        else
        {
            Session.Remove("E4110");
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
        CargarGrillaTurnos();        
    }

    /// <summary>
    /// Carga la grilla de resultados 
    /// </summary>
    protected void CargarGrillaTurnos()
    {
        try
        {
            htLabels = LabelConfig.htLabels;
            formatearvalores();
            DataTable dt_consulta = objBOConsulta.ConsultaTurnoxFiltro2(sFechaDesde, sFechaHasta, sCajero, sTurno);
            if (dt_consulta.Rows.Count < 1)
            {
                try
                {
                    this.lblTotal.Text = "";
                    this.lblTotalRows.Value = "";
                    this.lblMensajeError.Visible = false;
                    this.lblMensajeErrorData.Visible = true;
                    this.lblMensajeErrorData.Text = htLabels["consTicketProcesado.lblMensajeError.Text"].ToString();
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
                grvTurno.DataSource = null;
                grvTurno.DataBind();
            }
            else
            {
                ViewState["tablaTicket"] = dt_consulta;
                ValidarTamanoGrilla();
                lblMaxRegistros.Value = GetMaximoExcel().ToString();

                //Cargar datos en la grilla
                this.lblMensajeError.Text = "";
                this.lblTotal.Text = htLabels["consTicketProcesado.lblTotal"].ToString() + " " + dt_consulta.Rows.Count;
                this.lblTotalRows.Value = dt_consulta.Rows.Count.ToString();
                this.grvTurno.DataSource = dt_consulta;
                this.grvTurno.AllowPaging = true;
                this.grvTurno.PageSize = valorMaxGrilla;
                this.grvTurno.DataBind();
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

    /// <summary>
    /// Setea valores de los filtros de la consulta
    /// </summary>
    void formatearvalores()
    {
        sFechaDesde = txtFechaDesde.Text;
        sFechaHasta = txtFechaHasta.Text;
        sCajero = ddlCajero.SelectedValue;
        sTurno = txtTurno.Text;

        //Carga en session los filtros ingresados
        Hashtable htFiltro = new Hashtable();
        htFiltro.Add("FechaIni", sFechaDesde);
        htFiltro.Add("FechaFin", sFechaHasta);
        htFiltro.Add("Cajero", sCajero);
        htFiltro.Add("Turno", sTurno);
        Session["E4110"] = htFiltro;

        int result = DateTime.Compare(Convert.ToDateTime(this.txtFechaDesde.Text), Convert.ToDateTime(this.txtFechaHasta.Text));
        if (result == 1)
        {
            this.lblMensajeError.Visible = true;
            this.lblMensajeErrorData.Visible = false;
            this.lblMensajeError.Text = htLabels["consTicketProcesado.lblMensajeError2.Text"].ToString();
        }
        else if (result != 1)
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeErrorData.Visible = false;
            this.lblMensajeError.Text = "";
        }

        if (sFechaDesde != "")
        {
            string[] wordsFechaDesde = sFechaDesde.Split('/');
            sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { sFechaDesde = ""; }

        if (sFechaHasta != "")
        {
            string[] wordsFechaHasta = sFechaHasta.Split('/');
            sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        else { sFechaHasta = ""; }
    }

    /// <summary>
    /// //Traer valor de tamaño de la grilla desde parametro general    
    /// </summary>
    void ValidarTamanoGrilla()
    {
        dt_parametroGeneral = objBOConsulta.ListarParametros(Define.ID_PARAM_LIMITE_GRILLA);
        if (dt_parametroGeneral.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroGeneral.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    /// <summary>
    /// Carga los combobox de la interface.
    /// </summary>
    public void CargarCombos()
    {
        try
        {
            //Cargar combobox cajeros
            objBOConsulta = new BO_Consultas();
            DataTable dtUsuariose = objBOConsulta.ListarAllUsuario();
            //ListarAllUsuariol();
            DataRow dtrTodos = dtUsuariose.NewRow();
            dtrTodos["Usuario"] = Define.ID_FILTRO_TODOS;
            dtUsuariose.Rows.InsertAt(dtrTodos, 0);

            ddlCajero.DataSource = dtUsuariose;
            ddlCajero.DataTextField = "Usuario";
            ddlCajero.DataValueField = "Cod_Usuario";
            ddlCajero.DataBind();
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


    #region Ordenacion y Cambio de Pagina
    protected void grvTurno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = grvTurno.PageSize;
        DataTable dtTurnos = (DataTable)ViewState["tablaTicket"];

        grvTurno.PageIndex = e.NewPageIndex;
        grvTurno.DataSource = dtTurnos;
        grvTurno.DataBind();

    }

    protected void grvTurno_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTurnos = (DataTable)ViewState["tablaTicket"];
        if (dtTurnos == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;
        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tablaTicket"] = SortDataTable(dtTurnos, false).ToTable();
        //reactualizo la tabla
        dtTurnos = (DataTable)ViewState["tablaTicket"];
        grvTurno.DataSource = (DataTable)ViewState["tablaTicket"];
        grvTurno.DataBind();
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
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
    #endregion


    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TicketProcesados.xls");
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

        //RecuperarFiltros();
        formatearvalores();
        #region Consultas
        try
        {
            dt_consulta = objBOConsulta.ConsultaTurnoxFiltro2(sFechaDesde,sFechaHasta,sCajero,sTurno);
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

        #region Ticket Procesados
        Excel.Worksheet ticketProcesados;

            ticketProcesados = new Excel.Worksheet("Ticket Procesados");
            ticketProcesados.Columns = new string[] { "Turno", "Cajero", "Equipo", "Fecha de Apertura","Nro. Ticket Vendidos" };

            ticketProcesados.WidthColumns = new int[] { 70, 150, 100, 100, 80};
            ticketProcesados.DataFields = new string[] { "Cod_Turno", "Nom_Usuario_Format", "Dsc_Estacion", "Fch_Inicio_Format", "Ticket_Proc" };
            ticketProcesados.Source = dt_consulta;
       
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { ticketProcesados };

        return Workbook.Save();
    }

    
}
