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

public partial class Cfg_VerListaCampo : System.Web.UI.Page
{
    //private DataTable dtListaCampo;
    protected bool Flg_Error;
    protected Hashtable htLabels;
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objParametro = new BO_Consultas();
    DataTable dt_parametroTurno = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.btnNuevo.Text = htLabels["mmodalidadventa.btnNuevo"].ToString();

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


            btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_CfgCrearListaCampo, Define.OPC_NUEVO);
            if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_CfgModificarListaCampo, Define.OPC_MODIFICAR) == false)
            {
                grvListaCampo.Columns[0].Visible = false;
                grvListaCampo.Columns[1].Visible = true;
            }
            else
            {
                grvListaCampo.Columns[0].Visible = true;
                grvListaCampo.Columns[1].Visible = false;
            }

            CargarGrilla();
        }
    }



    private DataTable GetListaCampoTable()
    {     
        return null;
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cfg_CrearListaCampo.aspx");
    }
    protected void CargarGrilla()
    {       
        dt_parametroTurno = objParametro.ListarParametros("LG");
        if (dt_parametroTurno.Rows.Count > 0)
        {
            this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }

        DataTable dtListaCampo = new DataTable();
        dtListaCampo = objBOConfiguracion.ObtenerListaDeCampo("","");
        ViewState["ConsultaListaCampo"] = dtListaCampo;
        this.grvListaCampo.DataSource = dtListaCampo;
        this.grvListaCampo.AllowPaging = true;
        this.grvListaCampo.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvListaCampo.DataBind();

        if (dtListaCampo.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dtListaCampo.Rows.Count;
        }
    }

    protected void grvListaCampo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grvListaCampo.DataSource = dwConsulta((DataTable)ViewState["ConsultaListaCampo"], this.txtColumna.Text, txtOrdenacion.Text);
        this.grvListaCampo.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvListaCampo.PageIndex = e.NewPageIndex;
        grvListaCampo.DataBind();      

    }

    protected void grvListaCampo_Sorting(object sender, GridViewSortEventArgs e)
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
            LAP.TUUA.UTIL.UIControles objConversor = new UIControles();
            ViewState["ConsultaListaCampo"]= objConversor.ConvertDataTable(dwConsulta((DataTable)ViewState["ConsultaListaCampo"], sortExpression, direction));

            //ViewState["ConsultaListaCampo"] =  dwConsulta((DataTable)ViewState["ConsultaListaCampo"], sortExpression, direction);
            this.grvListaCampo.DataSource = (DataTable)ViewState["ConsultaListaCampo"];
            this.grvListaCampo.AllowPaging = true;
            this.grvListaCampo.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);            
            this.grvListaCampo.DataBind();

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

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }




    protected void grvListaCampo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowCampo")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleListaDeCampos = (DataTable)ViewState["ConsultaListaCampo"];
            string p_sNomCampo = dtDetalleListaDeCampos.Rows[rowIndex]["Nom_Campo"].ToString();
            string p_sCodCampo = dtDetalleListaDeCampos.Rows[rowIndex]["Cod_Campo"].ToString();
            CfgEditListaCampo1.CargarFormulario(p_sNomCampo, p_sCodCampo);// ConsDetTicket1.Inicio(codTicket);
        }
    }
}
