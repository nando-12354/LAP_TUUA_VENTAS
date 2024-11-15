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

public partial class ReporteCNS_rptCnsTicketUsados : System.Web.UI.Page
{
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    private int valorMaximo;


    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sIdCompania;
    string sTipoVuelo;
    string sNumVuelo;
    string sTipoPasajero;
    string sTipoDocumento;
    string sTipoTrasbordo;
    string sFechaVuelo;
    string sEstado;

    protected void Page_Load(object sender, EventArgs e)
    {
        sFechaDesde = Request.QueryString["sDesde"];
        sFechaHasta = Request.QueryString["sHasta"];
        sHoraDesde = Request.QueryString["idHoraDesde"];
        sHoraHasta = Request.QueryString["idHoraHasta"];
        sIdCompania = Request.QueryString["idCompania"];
        sTipoVuelo = Request.QueryString["idTipoVuelo"];
        sNumVuelo = Request.QueryString["idNumVuelo"];
        sTipoPasajero = Request.QueryString["idTipoPasajero"];
        sTipoDocumento = Request.QueryString["idTipoDocumento"];
        sTipoTrasbordo = Request.QueryString["idTipoTrasbordo"];
        sFechaVuelo = Request.QueryString["idFechaVuelo"];
        sEstado = Request.QueryString["idEstado"];


        if (sHoraDesde != null)
        {
            string[] wordsDesde = sHoraDesde.Split(':');
            sHoraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        }
        else { sHoraDesde = ""; }

        if (sHoraHasta != null)
        {
            string[] wordsHasta = sHoraHasta.Split(':');
            sHoraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
        }
        else { sHoraHasta = ""; }




        if (sFechaDesde != null)
        {
            string[] wordsFechaDesde = sFechaDesde.Split('/');
            sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { sFechaDesde = ""; }

        if (sFechaHasta != null)
        {
            string[] wordsFechaHasta = sFechaHasta.Split('/');
            sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        else { sFechaHasta = ""; }


        if (sFechaVuelo != "" )
        {
            string[] wordsFechaVuelo = sFechaVuelo.Split('/');
            sFechaVuelo = wordsFechaVuelo[2] + "" + wordsFechaVuelo[1] + "" + wordsFechaVuelo[0];
        }
        else { sFechaVuelo = ""; }


        cargarDatatable();
       
        //dt_consulta = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(FechaDesde, FechaHasta, idUsuario, idPtoVta, HoraDesde, HoraHasta);
        //dt_consulta = objBOConsultas.obtenerTicketBoardingUsados(FechaDesde, FechaHasta, HoraDesde, HoraHasta,
        //    idCompania, idTipoVuelo, idNumVuelo, idTipoPasajero,
        //    idTipoDocumento, idTipoTrasbordo, idFechaVuelo);


        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {

            //grvTicketUsados.DataSource = dt_consulta;
            //grvTicketUsados.AllowPaging = true;
            ////grvTicketUsados.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            //grvTicketUsados.DataBind();

            grvTicketUsadosRpt.DataSource = dt_consulta;
            grvTicketUsadosRpt.AllowPaging = true;
            //this.grvTicketUsados.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            //grvTicketUsadosRpt.PageIndex = 0;
            grvTicketUsadosRpt.DataBind();


            CargarResumen();



        }
    }


    public void CargarResumen()
    {
        //Pintar el reporte 
        CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        obRpt.Load(Server.MapPath("").ToString() + @"\rptCnsTicketUsados.rpt");
        //Poblar el reporte con el datatable
        obRpt.SetDataSource(dt_consulta);
        crvResumenTipoDocumento.ReportSource = obRpt;
    }


    void cargarDatatable()
    {
        //sFechaDesde = Request.QueryString["sDesde"];
        //sFechaHasta =Request.QueryString["sHasta"];
        //sHoraDesde = Request.QueryString["idHoraDesde"];
        //sHoraHasta = Request.QueryString["idHoraHasta"];
        //sIdCompania = Request.QueryString["idCompania"];
        //sTipoVuelo = Request.QueryString["idTipoVuelo"];
        //sNumVuelo = Request.QueryString["idNumVuelo"];
        //sTipoPasajero = Request.QueryString["idTipoPasajero"];
        //sTipoDocumento = Request.QueryString["idTipoDocumento"];
        //sTipoTrasbordo = Request.QueryString["idTipoTrasbordo"];
        //sFechaVuelo = Request.QueryString["idFechaVuelo"];


        //if (sHoraDesde == "__:__:__") { sHoraDesde = ""; }
        //if (sHoraHasta == "__:__:__") { sHoraHasta = ""; }

        //if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        //{
        //    string pNewHraDesde = txtHoraDesde.Text;
        //    string pNewHraHasta = txtHoraHasta.Text;
        //    pNewHraDesde = "23:59:59";
        //    pNewHraHasta = "23:59:59";

        //    ValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));
        //}
        //else
        //{
        //    ValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + sHoraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + sHoraHasta));
        //}

        //if (ValiFechas == 1)
        //{
        //    this.lblMensajeError.Text = "Filtro de fecha invalido";
        //    lblMensajeErrorData.Text = "";
        //    this.txtHasta.Text = "";
        //    grvTicketUsados.DataSource = null;
        //    grvTicketUsados.DataBind();
        //}
        //else
        //{
        //    if (ValiFechas != 1)
        //    {
        //        this.lblMensajeError.Text = "";
        //        //CargarGrillaDinamica();
        //    }
        //}



        //int result = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text), Convert.ToDateTime(this.txtHasta.Text));
        //if (result == 1)
        //{
        //    this.lblMensajeError.Text = "Filtro de fecha invalido";
        //    this.txtHasta.Text = "";
        //}
        //else if (result != 1)
        //    this.lblMensajeError.Text = "";



        //if (sHoraDesde != "")
        //{
        //    string[] wordsDesde = sHoraDesde.Split(':');
        //    sHoraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        //}
        //else { sHoraDesde = ""; }



        //if (sHoraHasta != "")
        //{
        //    string[] wordsHasta = sHoraHasta.Split(':');
        //    sHoraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
        //}
        //else { sHoraHasta = ""; }



        //if (sFechaDesde != "")
        //{
        //    string[] wordsFechaDesde = sFechaDesde.Split('/');
        //    sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        //}
        //else { sFechaDesde = ""; }



        //if (sFechaHasta != "")
        //{
        //    string[] wordsFechaHasta = sFechaHasta.Split('/');
        //    sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        //}
        //else { sFechaHasta = ""; }


        //if (sFechaVuelo != "")
        //{
        //    string[] wordsFechaHasta = sFechaVuelo.Split('/');
        //    sFechaVuelo = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        //}
        //else { sFechaVuelo = ""; }


        dt_consulta = objBOConsultas.obtenerTicketBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIdCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        ViewState["tablaTipoDocumento"] = dt_consulta;
    }


    //protected void grvTicketUsados_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    cargarDatatable();
    //    this.grvTicketUsados.DataSource = dwConsulta(dt_consulta, this.txtColumna.Text, txtOrdenacion.Text);
    //    this.grvTicketUsados.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
    //    grvTicketUsados.PageIndex = e.NewPageIndex;
    //    grvTicketUsados.DataBind();
    //}

    //protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    //{
    //    DataView dv = new DataView(dtConsulta);

    //    if (txtOrdenacion.Text.CompareTo("") != 0)
    //    {
    //        dv.Sort = sortExpression + " " + direction;
    //    }

    //    return dv;
    //}



    //protected void grvTicketUsados_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    string sortExpression = e.SortExpression;

    //    if (GridViewSortDirection == SortDirection.Ascending)
    //    {
    //        GridViewSortDirection = SortDirection.Descending;

    //        this.txtOrdenacion.Text = "DESC";
    //        SortGridView(sortExpression, "DESC");

    //    }
    //    else
    //    {
    //        GridViewSortDirection = SortDirection.Ascending;

    //        this.txtOrdenacion.Text = "ASC";
    //        SortGridView(sortExpression, "ASC");

    //    }
    //    this.txtColumna.Text = sortExpression;

    //}



    //public SortDirection GridViewSortDirection
    //{
    //    get
    //    {
    //        if (ViewState["SortDirection"] == null)
    //        {
    //            ViewState["SortDirection"] = SortDirection.Ascending;
    //        }
    //        return (SortDirection)ViewState["SortDirection"];

    //    }
    //    set
    //    {
    //        ViewState["SortDirection"] = value;
    //    }
    //}


    //private void SortGridView(string sortExpression, String direction)
    //{
    //    try
    //    {
    //        cargarDatatable();
    //        //Session["dt_consultaCompania"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
    //        this.grvTicketUsados.DataSource = dwConsulta(dt_consulta, sortExpression, direction);
    //        this.grvTicketUsados.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
    //        grvTicketUsados.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Flg_Error = true;
    //        ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
    //    }
    //    finally
    //    {
    //        if (Flg_Error)
    //            Response.Redirect("PaginaError.aspx");

    //    }

    //}


    //protected void crvResumenTipoDocumento_Unload(object sender, EventArgs e)
    //{
    //    crvResumenTipoDocumento.Dispose();
    //}
    //protected void grvTicketUsadosRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    cargarDatatable();

    //    this.grvTicketUsadosRpt.DataSource = dwConsulta((DataTable)ViewState["tablaTipoDocumento"], this.txtColumna.Text, txtOrdenacion.Text);
    //    this.grvTicketUsadosRpt.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
    //    grvTicketUsadosRpt.PageIndex = e.NewPageIndex;
    //    grvTicketUsadosRpt.DataBind();

    //    CargarResumen(); 
    //}


    protected void grvTicketUsadosRpt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTipoDocumento")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTipoDocumento = (DataTable)ViewState["tablaTipoDocumento"];
            String codDocumento = dtTipoDocumento.Rows[rowIndex]["Dsc_Documento"].ToString();
            if (codDocumento == "Ticket")
            {
                ConsDetTicket2.Inicio(dtTipoDocumento.Rows[rowIndex]["Codigo"].ToString() + "-Cns");
            }
            else
            {
                CnsDetBoarding2.CargarDetalleBoarding(dtTipoDocumento.Rows[rowIndex]["Codigo"].ToString());
            }

            CargarResumen(); 
            ////Pintar el reporte resumen
            //CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //obRpt.Load(Server.MapPath("").ToString() + @"\ReporteCNS\rptCnsTicketUsados.rpt");
            ////Poblar el reporte con el datatable
            //obRpt.SetDataSource((DataTable)ViewState["tablaTipoDocumento"]);
            ////crvResTipoDocumento.ReportSource = obRpt;

        }
    }
    protected void grvTicketUsadosRpt_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");

        }
        else
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");

        }
        this.txtColumna.Text = sortExpression;

        //CargarResumen(); 
    }


    public System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Ascending;
            }
            return (System.Web.UI.WebControls.SortDirection)ViewState["SortDirection"];

        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }

    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            cargarDatatable();
            ValidarTamanoGrilla();
            ViewState["tablaTipoDocumento"] = objCargaCombo.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            dt_consulta = (DataTable)ViewState["tablaTipoDocumento"];
            this.grvTicketUsadosRpt.DataSource = dt_consulta;
            this.grvTicketUsadosRpt.PageSize = valorMaximo;
            grvTicketUsadosRpt.DataBind();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
                Response.Redirect("PaginaError.aspx");

        }
    }


    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general 
        DataTable dt_parametrogeneral = new DataTable();
        dt_parametrogeneral = objBOConsultas.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            valorMaximo = Convert.ToInt32(dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }


    protected void grvTicketUsadosRpt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}
