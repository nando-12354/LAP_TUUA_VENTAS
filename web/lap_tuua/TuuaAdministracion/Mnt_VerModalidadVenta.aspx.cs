/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Mantenimiento de Modalidad de Venta
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/


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
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;

public partial class Mnt_VerModalidadVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Administracion objBOAdministracion = new BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
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
                btnNuevo.Text = htLabels["mmodalidadventa.btnNuevo"].ToString();
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
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntCrearModalidadVenta, Define.OPC_NUEVO);

                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntModificarModalidadVenta, Define.OPC_MODIFICAR) == false)
                {
                    this.gwvModalidaVenta.Columns[0].Visible = false;
                    gwvModalidaVenta.Columns[1].Visible = true;
                }
                else
                {
                    gwvModalidaVenta.Columns[0].Visible = true;
                    gwvModalidaVenta.Columns[1].Visible = false;
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
        //Globales.FlagPosPage = false;
        //Globales.FlagModal = false;
        eliminarGlobales();
    }


    public Sglobales obtenerGlobales()
    {
        Sglobales mGlobales;

        if (Session["SuperGlobal"] == null)
        {
            mGlobales = new Sglobales();
        }
        else
        {
            mGlobales = (Sglobales)Session["SuperGlobal"];
        }

        return mGlobales;
    }

    public void guardarGlobales(Sglobales mGlobales)
    {
        Session["SuperGlobal"] = mGlobales;
    }

    public void eliminarGlobales()
    {
        Session.Remove("SuperGlobal");
    }

    public void validagrilla(DataTable dt_consulta)
    {

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["mmodalidadventa.lblMensajeError"].ToString();
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
        dtConsulta = objBOAdministracion.ListarAllModalidadVenta();
        ViewState["ConsultaMV"] = dtConsulta;
        //validagrilla(dtConsulta);
        this.gwvModalidaVenta.DataSource = dtConsulta;
        this.gwvModalidaVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvModalidaVenta.DataBind();

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

    protected void gwvModalidaVenta_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gwvModalidaVenta.DataSource = dwConsulta((DataTable)ViewState["ConsultaMV"], this.txtColumna.Text, txtOrdenacion.Text);
            this.gwvModalidaVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvModalidaVenta.PageIndex = e.NewPageIndex;
            gwvModalidaVenta.DataBind();
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



    protected void gwvModalidaVenta_Sorting(object sender, GridViewSortEventArgs e)
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
            this.gwvModalidaVenta.DataSource = dwConsulta((DataTable)ViewState["ConsultaMV"], sortExpression, direction);
            this.gwvModalidaVenta.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvModalidaVenta.DataBind();
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


    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mnt_CrearModalidadVenta.aspx");
    }
}
