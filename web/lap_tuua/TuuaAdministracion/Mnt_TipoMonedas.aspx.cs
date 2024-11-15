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

public partial class Modulo_Mantenimiento_TipoMonedas : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    DataTable dt_consulta = new DataTable();

    bool flagError;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            htLabels = LabelConfig.htLabels;
            try
            {
                this.btnNuevo.Text = (string)htLabels["madmTipoMoneda.btnNuevo.Text"];
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntCrearTipoMonedas, Define.OPC_NUEVO);
                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntModificarTipoMonedas, Define.OPC_MODIFICAR) == false)
                {
                    grvMoneda.Columns[0].Visible = false;
                    grvMoneda.Columns[1].Visible = true;
                }
                else
                {
                    grvMoneda.Columns[0].Visible = true;
                    grvMoneda.Columns[1].Visible = false;
                }
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
                this.lblMensajeError.Text = htLabels["madmTipoMoneda.lblMensajeError"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_CnsCompanias;
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    public void CargarGrilla()
    {
        dt_consulta = objWBAdministracion.listarAllMonedas();
        /*if (dt_consulta.Rows.Count > 0)
        {
            ViewState["ConsultaMoneda"] = dt_consulta;
            //validagrilla(dt_consulta);
            
            grvMoneda.DataSource = dt_consulta;
            grvMoneda.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvMoneda.DataBind();
        }
        else
        {
            lblMensajeError.Text = "No existen monedas registradas";
        }*/
        ViewState["ConsultaMoneda"] = dt_consulta;
        grvMoneda.DataSource = dt_consulta;
        grvMoneda.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvMoneda.DataBind();

        if (dt_consulta.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dt_consulta.Rows.Count;
        }
    }

   
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mnt_CrearTipoMonedas.aspx");

    }

    protected void grvMoneda_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.grvMoneda.DataSource = dwConsulta((DataTable)ViewState["ConsultaMoneda"], this.txtColumna.Text, txtOrdenacion.Text);
            this.grvMoneda.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvMoneda.PageIndex = e.NewPageIndex;
            grvMoneda.DataBind();
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



    protected void grvMoneda_Sorting(object sender, GridViewSortEventArgs e)
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
            this.grvMoneda.DataSource = dwConsulta((DataTable)ViewState["ConsultaMoneda"], sortExpression, direction);
            this.grvMoneda.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvMoneda.DataBind();
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
            //Soporte Ordenacion con varios campos
            string[] vec = sortExpression.Split(',');
            string sortExpressionMultiple = "";
            for (int i = 0; i <= vec.Length - 1; i++)
            {
                sortExpressionMultiple += vec[i].Trim() + " " + direction;   
                if ( i + 1 <= vec.Length - 1)
                    sortExpressionMultiple += ",";
            }
            dv.Sort = sortExpressionMultiple;            
        }
        return dv;
    }
}
