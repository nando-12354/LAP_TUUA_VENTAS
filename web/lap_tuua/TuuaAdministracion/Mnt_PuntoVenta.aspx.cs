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


public partial class Mnt_PuntoVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected bool Flg_Error;
    protected BO_Error objError;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            htLabels = LabelConfig.htLabels;
            try
            {
                this.btnNuevo.Text = (string)htLabels["madmPuntoVenta.btnNuevo.Text"];
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntCrearPuntoVenta, Define.OPC_NUEVO);
                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntModificarPuntoVenta, Define.OPC_MODIFICAR) == false)
                {
                    grvPuntoVenta.Columns[0].Visible = false;
                    grvPuntoVenta.Columns[1].Visible = true;
                }
                else
                {
                    grvPuntoVenta.Columns[0].Visible = true;
                    grvPuntoVenta.Columns[1].Visible = false;
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
            try
            {
                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

                CargarGrilla();
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
    }

    public void validagrilla(DataTable dt_consulta)
    {

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["madmEditarPuntoVenta.lblMensajeErrorData"].ToString();
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
    }

    public void CargarGrilla()
    {
        
            dt_consulta = objWBAdministracion.listarAllPuntoVenta();
            if (dt_consulta.Rows.Count > 0)
            {
                ViewState["ConsultaPtoVenta"] = dt_consulta;
                grvPuntoVenta.DataSource = dt_consulta;
                validagrilla(dt_consulta);
                grvPuntoVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                grvPuntoVenta.DataBind();

                this.lblMensajeError.Visible = false;
                this.lblMensajeError.Text = "";
                this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dt_consulta.Rows.Count;
            }
            else
            {
                this.lblTotal.Text = "";
                this.lblMensajeError.Visible = true;
                this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
            }
        
    }

    protected void btnNuevo_Click(object sender, EventArgs e) 
    {
        Response.Redirect("Mnt_CrearPuntoVenta.aspx");
    }

   

    protected void grvPuntoVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvPuntoVenta.DataSource = dwConsulta((DataTable)ViewState["ConsultaPtoVenta"], this.txtColumna.Text, txtOrdenacion.Text);
        this.grvPuntoVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvPuntoVenta.PageIndex = e.NewPageIndex;
        grvPuntoVenta.DataBind();
    }



    protected void grvPuntoVenta_Sorting(object sender, GridViewSortEventArgs e)
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
        this.grvPuntoVenta.DataSource = dwConsulta((DataTable)ViewState["ConsultaPtoVenta"], sortExpression, direction);
        this.grvPuntoVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvPuntoVenta.DataBind();

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
