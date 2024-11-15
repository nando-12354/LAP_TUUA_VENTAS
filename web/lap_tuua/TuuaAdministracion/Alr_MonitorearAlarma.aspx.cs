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
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ALARMAS;


public partial class Alr_MonitorearAlarma : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Alarmas objBOAlarmas;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblMensajeTotalAlarmas.Text = htLabels["malarma.lblMensajeTotalAlarmas"].ToString();
                this.lblMensajeHoraAlarmas.Text=htLabels["malarma.lblMensajeHoraAlarmas"].ToString();
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }


            try
            {
                DataTable dt_Tiempo = objBOConsultas.ListarParametros("TA");
                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.gwvMonitorearAlarma.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                    this.lblMensajeTiempoAlarmas.Text = Convert.ToString(dt_Tiempo.Rows[0].ItemArray.GetValue(4).ToString());
                }

                CargarGrilla();
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }

        }
       
    }

    protected void CargarGrilla()
    {
        DataTable dtAlarmaGenerada = new DataTable();
        dtAlarmaGenerada = objBOAlarmas.ListarAlarmaGeneradaEnviadas();
        //ViewState["CAlarmaGenerada"] = dtAlarmaGenerada;
        Session["CAlarmaGenerada"] = dtAlarmaGenerada;
        this.gwvMonitorearAlarma.DataSource = dtAlarmaGenerada;
        this.txtTotalAlarmas.Text = Convert.ToString(dtAlarmaGenerada.Rows.Count);
        gwvMonitorearAlarma.DataBind();
    }


    #region Paginacion

    protected void gwvMonitorearAlarma_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //DataTable dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
        DataTable dtAlarmaGenerada = (DataTable)Session["CAlarmaGenerada"];
        gwvMonitorearAlarma.PageIndex = e.NewPageIndex;
        gwvMonitorearAlarma.DataSource = dtAlarmaGenerada;
        gwvMonitorearAlarma.DataBind();
    }

    #endregion

    #region gwvAnularBCBP_Sorting
    protected void gwvMonitorearAlarma_Sorting(object sender, GridViewSortEventArgs e)
    {
        //DataTable dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
        DataTable dtAlarmaGenerada = (DataTable)Session["CAlarmaGenerada"];
        if (dtAlarmaGenerada == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        //ViewState["CAlarmaGenerada"] = SortDataTable(dtAlarmaGenerada, false).ToTable();
        Session["CAlarmaGenerada"] = SortDataTable(dtAlarmaGenerada, false).ToTable();
        //reactualizo la tabla
        //dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
        dtAlarmaGenerada = (DataTable)Session["CAlarmaGenerada"];

        //gwvMonitorearAlarma.DataSource = (DataTable)ViewState["CAlarmaGenerada"];
        gwvMonitorearAlarma.DataSource = (DataTable)Session["CAlarmaGenerada"];

        gwvMonitorearAlarma.DataBind();

    }
    #endregion

    #region Metodo Generales para el Sort
    //Method that sorts data
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
    #endregion

    protected void gwvMonitorearAlarma_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AtencionAlarma"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            //DataTable dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
            DataTable dtAlarmaGenerada = (DataTable)Session["CAlarmaGenerada"];
            String codAlarmaGenerada = dtAlarmaGenerada.Rows[rowIndex]["Cod_AlarmaGenerada"].ToString();
            this.mpeAtencionAlarma.CargarFormulario(codAlarmaGenerada);
        }
    }
    protected void btnPrueba_Click(object sender, EventArgs e)
    {
        string IpClient = Request.UserHostAddress;
        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000001", "001", IpClient, "1", "Alerta Ususario Bloqueado", "La Cuenta ha sido Bloqueada", Convert.ToString(Session["Cod_Usuario"]));
        //GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000001", "I20", "192.168.1.32", "1", "Alerta", "Esta es una Prueba del Modulo de Alarma", Convert.ToString(Session["Cod_Usuario"]));
    }
}
