/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Mantenimiento de Tipo de Ticket
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

public partial class Modulo_VerTipoTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    bool flagError;
    DataTable dt_consulta;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                btnNuevo.Text = htLabels["mtticket.btnNuevo"].ToString();
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntCrearTipoTicket, Define.OPC_NUEVO);
                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntModificarTipoTicket, Define.OPC_MODIFICAR) == false)
                {
                    gwvTipoTicket.Columns[0].Visible = false;
                    gwvTipoTicket.Columns[1].Visible = true;
                }
                else
                {
                    gwvTipoTicket.Columns[0].Visible = true;
                    gwvTipoTicket.Columns[1].Visible = false;
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
                this.lblMensajeError.Text = htLabels["mtticket.lblMensajeError"].ToString();
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
        dt_consulta = objWBAdministracion.ListaTipoTicket();
        ViewState["ConsultaTipoTicket"] = dt_consulta;
        validagrilla(dt_consulta);
        this.gwvTipoTicket.DataSource = dt_consulta;
        this.gwvTipoTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvTipoTicket.DataBind();
    
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
      
            Response.Redirect("Mnt_CrearTipoTicket.aspx");
       
    }
  


    protected void gwvTipoTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gwvTipoTicket.DataSource = dwConsulta((DataTable)ViewState["ConsultaTipoTicket"], this.txtColumna.Text, txtOrdenacion.Text);
            this.gwvTipoTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvTipoTicket.PageIndex = e.NewPageIndex;
            gwvTipoTicket.DataBind();
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



    protected void gwvTipoTicket_Sorting(object sender, GridViewSortEventArgs e)
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
            this.gwvTipoTicket.DataSource = dwConsulta((DataTable)ViewState["ConsultaTipoTicket"], sortExpression, direction);
            this.gwvTipoTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvTipoTicket.DataBind();
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
