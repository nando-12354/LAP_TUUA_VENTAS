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
using System.Collections.Generic;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;

public partial class Modulo_Seguridad_Rol_VerRol : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    DataTable dtConsultaRoles;
    bool flagError;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                btnNuevo.Text = htLabels["mrol.btnNuevo"].ToString();
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_SegCrearRol, Define.OPC_NUEVO);
                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_SegModificarRol, Define.OPC_MODIFICAR) == false)
                {
                    gwvRol.Columns[0].Visible = false;
                    gwvRol.Columns[1].Visible = true;
                }
                else
                {
                    gwvRol.Columns[0].Visible = true;
                    gwvRol.Columns[1].Visible = false;
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


            dtConsultaRoles = new DataTable();
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
    
    public void ValidarGrilla(DataTable dt_consulta)
    {
        if (dt_consulta.Rows.Count < 1)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["mrol.lblMensajeError"].ToString();
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
        dtConsultaRoles = objBOConsultas.ListarRoles();

        //Set Security User
        SetSecurityUser(Session["Cod_Usuario"].ToString());

        ViewState["ConsultaRoles"] = dtConsultaRoles;
        ValidarGrilla(dtConsultaRoles);
        this.gwvRol.DataSource = dtConsultaRoles;
        this.gwvRol.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvRol.DataBind();
        if (dtConsultaRoles.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dtConsultaRoles.Rows.Count;
        }

    }

    #region Events
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Seg_CrearRol.aspx");
    }
    #endregion

    #region Sorting and PageIndexChanging
    protected void gwvRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            this.gwvRol.DataSource = dwConsulta((DataTable)ViewState["ConsultaRoles"], this.txtColumna.Text, txtOrdenacion.Text);
            this.gwvRol.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text); ;
            gwvRol.PageIndex = e.NewPageIndex;
            gwvRol.DataBind();
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

    protected void gwvRol_Sorting(object sender, GridViewSortEventArgs e)
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
            this.gwvRol.DataSource = dwConsulta((DataTable)ViewState["ConsultaRoles"], sortExpression, direction);
            this.gwvRol.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvRol.DataBind();
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

        if (txtOrdenacion.Text.CompareTo("")!=0)
        {
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }
    #endregion

    #region Upgrade Seguridad
    private void SetSecurityUser(string sCodUsuario)
    {
        List<Rol> lstRolUsrSession = new List<Rol>();
        List<Rol> lstRolSistema = new List<Rol>();
        lstRolUsrSession = new List<Rol>(objBOSeguridad.listarRolesAsignados(Session["Cod_Usuario"].ToString()));
        lstRolSistema = new List<Rol>(objBOSeguridad.listaDeRoles());

        if (dtConsultaRoles.Rows.Count > 0)
        {
            foreach (DataRow rowUsuario in dtConsultaRoles.Rows)
            {
                List<Rol> lstRolUsrLista = new List<Rol>();
                lstRolUsrLista.Add(objBOSeguridad.obtenerRolxcodigo((String)rowUsuario["Cod_Rol"]));

                if (!(IsPermisoModificacionValido(lstRolSistema, lstRolUsrLista, lstRolUsrSession)))
                {
                    rowUsuario["Flg_Sec"] = "1";
                }
            }
        }
    }
    private bool IsPermisoModificacionValido(List<Rol> lstRoles, List<Rol> lstRolesMenor, List<Rol> lstRolesMayor)
    {
        foreach (Rol objLRol in lstRolesMenor)
        {
            bool bIsMenor = true;
            foreach (Rol objHRol in lstRolesMayor)
            {
                if (EsRolMenorPrivilegio(lstRoles, objLRol.SCodRol, objHRol.SCodRol))
                {
                    bIsMenor = false;
                    break;
                }
                /*else {
                    if (!EstaRolEnListaRoles(lstRolesMayor, objLRol.SCodRol))
                    {
                        return false;
                    }                    
                }*/
            }
            if (bIsMenor)
            {
                return false;
            }
        }
        return true;
    }
    private bool EsRolMenorPrivilegio(List<Rol> lstRoles, string strRolMenor, string strRolMayor)
    {
        for (int i = 0; i < lstRoles.Count; i++)
        {
            if (lstRoles[i].SCodRol == strRolMenor)
            {
                if (lstRoles[i].SCodPadreRol == strRolMayor || lstRoles[i].SCodRol == strRolMayor)
                {
                    return true;
                }
                else
                {
                    return EsRolMenorPrivilegio(lstRoles, lstRoles[i].SCodPadreRol, strRolMayor);
                }
            }
        }
        return false;
    }
    private bool EstaRolEnListaRoles(List<Rol> lstRoles, string strRol)
    {
        for (int i = 0; i < lstRoles.Count; i++)
        {
            if (lstRoles[i].SCodRol == strRol)
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
