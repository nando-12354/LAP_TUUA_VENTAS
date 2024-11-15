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

public partial class Alr_VerAlarma : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Alarmas objBOAlarmas;
    DataTable dtConsultaAlarma;
    bool flagError;
    public string orden = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                btnNuevo.Text = htLabels["malarma.btnNuevo"].ToString();
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

            dtConsultaAlarma = new DataTable();
            try
            {
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_AlrCrearAlarma, Define.OPC_NUEVO);

                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_AlrModificarAlarma, Define.OPC_MODIFICAR) == false)
                {
                    gwvAlarma.Columns[0].Visible = false;
                    gwvAlarma.Columns[1].Visible = true;
                }
                else
                {
                    gwvAlarma.Columns[0].Visible = true;
                    gwvAlarma.Columns[1].Visible = false;
                }
                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
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

    public void validagrilla(DataTable dt_consulta)
    {

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["malarma.lblMensajeError"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_CnsCompanias;
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected void CargarGrilla()
    {
        string sFiltro = "";
        string sOrdenacion = "ASC";
        dtConsultaAlarma = objBOAlarmas.ListarAllCnfgAlarma();
        ViewState["ConsultaAlarmas"] = dtConsultaAlarma;
        validagrilla(dtConsultaAlarma);
        this.gwvAlarma.DataSource = dtConsultaAlarma;
        this.gwvAlarma.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvAlarma.DataBind();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {

        Response.Redirect(Define.ASPX_AlrCrearAlarma);
    }

    protected void gwvAlarma_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gwvAlarma.DataSource = dwConsulta((DataTable)ViewState["ConsultaAlarmas"], this.txtColumna.Text, txtOrdenacion.Text);
        this.gwvAlarma.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvAlarma.PageIndex = e.NewPageIndex;
        gwvAlarma.DataBind();

    }



    protected void gwvAlarma_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");

        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");

        }
        this.txtColumna.Text = sortExpression;

    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["SortDirection"];

        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }



    private void SortGridView(string sortExpression, String direction)
    {
        this.gwvAlarma.DataSource = dwConsulta((DataTable)ViewState["ConsultaAlarmas"], sortExpression, direction);
        this.gwvAlarma.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvAlarma.DataBind();

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
    
}
