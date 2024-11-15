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

public partial class Mnt_Molinete : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Administracion objBOAdministracion = new BO_Administracion();
    BO_Operacion objBOOperacion = new BO_Operacion();
    BO_Consultas objBOConsultas = new BO_Consultas();
    bool flagError;
    DataTable dtConsulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {                
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

    protected void CargarGrilla()
    {
        dtConsulta = objBOOperacion.ListarAllMolinetes();
        ViewState["ConsultaMV"] = dtConsulta;
        this.gwvMolinete.DataSource = dtConsulta;
        this.gwvMolinete.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvMolinete.DataBind();

        if (dtConsulta.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dtConsulta.Rows.Count;
        }
    }


    protected void gwvMolinete_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gwvMolinete.DataSource = dwConsulta((DataTable)ViewState["ConsultaMV"], this.txtColumna.Text, txtOrdenacion.Text);
            this.gwvMolinete.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvMolinete.PageIndex = e.NewPageIndex;
            gwvMolinete.DataBind();
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



    protected void gwvMolinete_Sorting(object sender, GridViewSortEventArgs e)
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
        try
        {
            this.gwvMolinete.DataSource = dwConsulta((DataTable)ViewState["ConsultaMV"], sortExpression, direction);
            this.gwvMolinete.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvMolinete.DataBind();
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
