/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Mantenimiento de Usuario
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
using System.Collections.Generic;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;

public partial class Modulo_Seguridad_Usuario_VerUsuario : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objControles = new UIControles();
    DataTable dtConsultaUsuario;
    bool flagError;
    public string orden = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!Page.IsPostBack)
        {   
            try
            {
                btnNuevo.Text = htLabels["musuario.btnNuevo"].ToString();
                lblCuenta.Text = htLabels["musuario.lblCuenta"].ToString();
                lblNombre.Text = htLabels["musuario.lblNombre"].ToString();
                this.lblRol.Text = htLabels["musuario.lblRol"].ToString();
                lblEstado.Text = htLabels["musuario.lblEstado"].ToString();
                btnConsultar.Text = htLabels["musuario.btnConsultar"].ToString();
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

            dtConsultaUsuario = new DataTable();
            try
            {
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_SegCrearUsuario, Define.OPC_NUEVO);

                if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_SegModificarUsuario, Define.OPC_MODIFICAR) == false)
                {
                    gwvUsuario.Columns[0].Visible = false;
                    gwvUsuario.Columns[1].Visible = true;
                }
                else
                {
                    gwvUsuario.Columns[0].Visible = true;
                    gwvUsuario.Columns[1].Visible = false;
                }
                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                }

                CargarCombos();
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

    protected void CargarCombos()
    {
        try
        {
            //Carga combo EstadoUsuario
            DataTable dt_EstadoUsuario = new DataTable();
            dt_EstadoUsuario = objBOConsultas.ListaCamposxNombre("EstadoUsuario");
            objControles.LlenarCombo(this.ddlEstado, dt_EstadoUsuario, "Cod_Campo", "Dsc_Campo", true,false);

            //Carga data table para el listado de Rol
            DataTable dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarRoles();
            objControles.LlenarCombo(ddlRol, dt_parametro, "Cod_Rol", "Nom_Rol", true,false);

        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

    }

    protected void validagrilla(DataTable dt_consulta)
    {

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["musuario.lblMensajeError"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_CnsCompanias;
                Response.Redirect("PaginaError.aspx");
            }
        }
        else
        {
            this.lblMensajeError.Text = "";
        }
    }

    protected void CargarGrilla()
    {
        Usuario objUsuario = new Usuario();
        objUsuario=objBOSeguridad.obtenerUsuario(Convert.ToString(Session["Cod_Usuario"]));

        ViewState["Grupo"] = objUsuario.STipoGrupo;
        string sFiltro = "";
        string sOrdenacion = "ASC";
        string sGrupo;

        if ((string)ViewState["Grupo"] != "T")
            sGrupo = (string)ViewState["Grupo"];
        else
            sGrupo = "0";

        dtConsultaUsuario = objBOConsultas.ConsultaUsuarioxFiltro("0", "V", sGrupo, sFiltro, sOrdenacion);

        //Set Security User
        SetSecurityUser(Session["Cod_Usuario"].ToString());


        this.ddlEstado.SelectedValue = "V";
        ViewState["ConsultaUsuario"] = dtConsultaUsuario;
        validagrilla(dtConsultaUsuario);
        
        if (dtConsultaUsuario.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null)?"":htLabels["tuua.general.lblTotal"].ToString()) + " " + dtConsultaUsuario.Rows.Count;
        }
        
        this.gwvUsuario.DataSource = dtConsultaUsuario;
        this.gwvUsuario.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvUsuario.DataBind();
    }

    #region Upgrade Seguridad
    private void SetSecurityUser(string sCodUsuario) {
        List<Rol> lstRolUsrSession = new List<Rol>();
        List<Rol> lstRolSistema = new List<Rol>();
        lstRolUsrSession = new List<Rol>(objBOSeguridad.listarRolesAsignados(Session["Cod_Usuario"].ToString()));
        lstRolSistema = new List<Rol>(objBOSeguridad.listaDeRoles());

        if (dtConsultaUsuario.Rows.Count > 0)
        {
            List<object> lstJerarquiaRoles = new List<object>();
            lstJerarquiaRoles = OrdenarRolesPorJerarquia(lstRolSistema);
            int iNivelJerarquiaUsr = obtenerNivelJerarquiaUsuarioSesion(lstJerarquiaRoles, lstRolUsrSession);

            foreach (DataRow rowUsuario in dtConsultaUsuario.Rows)
            {
                List<Rol> lstRolUsrLista = new List<Rol>();
                lstRolUsrLista = new List<Rol>(objBOSeguridad.listarRolesAsignados((String)rowUsuario["Cod_Usuario"]));

                /*if (!(IsPermisoModificacionValido(lstRolSistema, lstRolUsrLista, lstRolUsrSession)))
                {
                    rowUsuario["Flg_Sec"] = "1";
                }*/
                int iNivelJerarquiaEval = obtenerNivelJerarquiaUsuarioSesion(lstJerarquiaRoles, lstRolUsrLista);
                if (iNivelJerarquiaUsr > iNivelJerarquiaEval)  //if (iNivelJerarquiaUsr >= iNivelJerarquiaEval)
                {
                    rowUsuario["Flg_Sec"] = "1";
                }
            }
        }
    }

    private int obtenerNivelJerarquiaUsuarioSesion(List<object> lstJerarquiaRoles, List<Rol> lstRolUsrSession)
    {
        int iNivelJerarquiaMinimoSuperior = 99999;

        foreach (Rol oRolUsr in lstRolUsrSession)
        {
            int i = 0;
            foreach (object oLstRoles in lstJerarquiaRoles)
            {
                i++;
                Rol oRolBuscado = ((List<Rol>)oLstRoles).Find(delegate(Rol oRol) { return oRol.SCodRol.Equals(oRolUsr.SCodRol); });
                
                if (oRolBuscado != null)
                {
                    if (i < iNivelJerarquiaMinimoSuperior)
                    {
                        iNivelJerarquiaMinimoSuperior = i;
                    }
                }
            }
        }

        return iNivelJerarquiaMinimoSuperior;
    }

    private List<object> OrdenarRolesPorJerarquia(List<Rol> lstRolesSistema)
    {
        List<object> lstJerarquiaRoles = new List<object>();
        List<Rol> lstRoles = new List<Rol>();

        Rol oRolSupremo = lstRolesSistema.Find(delegate(Rol oRol) { return string.IsNullOrEmpty(oRol.SCodPadreRol); });

        lstRoles.Add(oRolSupremo);

        lstJerarquiaRoles.Add(lstRoles);

        ConstruirJerarquiaRoles(lstRoles, lstRolesSistema, lstJerarquiaRoles);

        return lstJerarquiaRoles;
    }

    private void ConstruirJerarquiaRoles(List<Rol> lstRoles, List<Rol> lstRolesSistema, List<object> lstJerarquiaRoles)
    {
        List<Rol> lstSiguientesRoles = new List<Rol>();
        List<Rol> lstRolesHijosParcial = new List<Rol>();

        foreach (Rol oRolPadre in lstRoles)
        {
            lstRolesHijosParcial = lstRolesSistema.FindAll(delegate(Rol oRol) { return string.IsNullOrEmpty(oRol.SCodPadreRol) ? false : oRol.SCodPadreRol.Equals(oRolPadre.SCodRol); });

            if (lstRolesHijosParcial.Count > 0)
            {
                lstSiguientesRoles.AddRange(lstRolesHijosParcial);
            }            
        }

        if (lstSiguientesRoles.Count > 0)
        {
            lstJerarquiaRoles.Add(lstSiguientesRoles);

            ConstruirJerarquiaRoles(lstSiguientesRoles, lstRolesSistema, lstJerarquiaRoles);
        }
    }

    private bool IsPermisoModificacionValido(List<Rol> lstRoles, List<Rol> lstRolesMenor, List<Rol> lstRolesMayor) {
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
            if (bIsMenor) {
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

    private DataTable FiltrarUsuario(DataTable dtConsultaUsuario, string sCuenta, string sNombre)
    {
        DataTable dest = new DataTable("Result" + dtConsultaUsuario.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Cod_Usuario";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Usuario";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Ape_Usuario";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cta_Usuario";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Fch_Vigencia";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Estado";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Grupo";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Fch_Creacion";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "CtaUsuarioCreacion";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Usuario_Creacion";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Usuario_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Usuario_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Fch_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Hor_Mod";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Nom_Rol";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Hor_Creacion";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Cod_Grupo_Padre";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Flg_Sec";
        dest.Columns.Add(dc);
        dc = new DataColumn("FH_Creacion");
        dest.Columns.Add(dc);
        dc = new DataColumn("FH_Proceso");
        dest.Columns.Add(dc);

        string query = "";

        if (sCuenta != "")
            query = "Cta_Usuario like '" + sCuenta + "*'";

        if (sNombre != "")
        {
            if (sCuenta != "")
            {
                query = query + " and Nom_Usuario like '" + sNombre + "*'";
            }
            else
            {
                query = "Nom_Usuario like '" + sNombre + "*'";
            }
        }

        DataRow[] foundRowTipoTicket = dtConsultaUsuario.Select(query);

        if (foundRowTipoTicket != null && foundRowTipoTicket.Length > 0)
        {
            for (int i = 0; i < foundRowTipoTicket.Length; i++)
            {
                dest.Rows.Add(dest.NewRow());
                dest.Rows[i][0] = foundRowTipoTicket[i]["Cod_Usuario"].ToString();
                dest.Rows[i][1] = foundRowTipoTicket[i]["Nom_Usuario"].ToString();
                dest.Rows[i][2] = foundRowTipoTicket[i]["Ape_Usuario"].ToString();
                dest.Rows[i][3] = foundRowTipoTicket[i]["Cta_Usuario"].ToString();
                dest.Rows[i][4] = foundRowTipoTicket[i]["Fch_Vigencia"].ToString();
                dest.Rows[i][5] = foundRowTipoTicket[i]["Nom_Estado"].ToString();
                dest.Rows[i][6] = foundRowTipoTicket[i]["Nom_Grupo"].ToString();
                dest.Rows[i][7] = foundRowTipoTicket[i]["Fch_Creacion"].ToString();
                dest.Rows[i][8] = foundRowTipoTicket[i]["CtaUsuarioCreacion"].ToString();
                dest.Rows[i][9] = foundRowTipoTicket[i]["Nom_Usuario_Creacion"].ToString();
                dest.Rows[i][10] = foundRowTipoTicket[i]["Cod_Usuario_Mod"].ToString();
                dest.Rows[i][11] = foundRowTipoTicket[i]["Nom_Usuario_Mod"].ToString();
                dest.Rows[i][12] = foundRowTipoTicket[i]["Fch_Mod"].ToString();
                dest.Rows[i][13] = foundRowTipoTicket[i]["Hor_Mod"].ToString();
                dest.Rows[i][14] = foundRowTipoTicket[i]["Nom_Rol"].ToString();
                dest.Rows[i][15] = foundRowTipoTicket[i]["Hor_Creacion"].ToString();
                dest.Rows[i][16] = foundRowTipoTicket[i]["Cod_Grupo_Padre"].ToString();
                dest.Rows[i][17] = foundRowTipoTicket[i]["Flg_Sec"].ToString();
                dest.Rows[i][18] = foundRowTipoTicket[i]["FH_Creacion"].ToString();
                dest.Rows[i][19] = foundRowTipoTicket[i]["FH_Proceso"].ToString();
            }
        }


        dest.AcceptChanges();
        return dest;
    }

    #region Events
    protected void btnNuevo_Click(object sender, EventArgs e)
    {

        Response.Redirect("Seg_CrearUsuario.aspx");
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        string sFiltro = "";
        string sOrdenacion = "ASC";
        string sEstado;
        string sRol;
        string sGrupo;

        if ((string)ViewState["Grupo"] != "T")
            sGrupo = (string)ViewState["Grupo"];
        else
            sGrupo = "0";

        sRol = ddlRol.SelectedValue;
        sEstado = ddlEstado.SelectedValue;

        dtConsultaUsuario = objBOConsultas.ConsultaUsuarioxFiltro(sRol, sEstado, sGrupo, sFiltro, sOrdenacion);

        //Set Security User
        SetSecurityUser(Session["Cod_Usuario"].ToString());

        ViewState["ConsultaUsuario"] = FiltrarUsuario(dtConsultaUsuario, this.txtCuenta.Text.TrimEnd(), txtNombre.Text.TrimEnd());
        dtConsultaUsuario = (DataTable)ViewState["ConsultaUsuario"];
        validagrilla(dtConsultaUsuario);
        this.gwvUsuario.DataSource = dtConsultaUsuario;

        if (dtConsultaUsuario.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dtConsultaUsuario.Rows.Count;
        }

        this.gwvUsuario.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvUsuario.DataBind();

    }

    protected void gwvUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "BLOQUEADO" ||

                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "ANULADO")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }
    }
    #endregion    

    #region Sorting
    protected void gwvUsuario_Sorting(object sender, GridViewSortEventArgs e)
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
        this.gwvUsuario.DataSource = dwConsulta((DataTable)ViewState["ConsultaUsuario"], sortExpression, direction);
        this.gwvUsuario.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvUsuario.DataBind();

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
    #endregion

    #region PageIndexChanging
    protected void gwvUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.gwvUsuario.DataSource = dwConsulta((DataTable)ViewState["ConsultaUsuario"], this.txtColumna.Text, txtOrdenacion.Text);
        this.gwvUsuario.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvUsuario.PageIndex = e.NewPageIndex;
        gwvUsuario.DataBind();

    }
    #endregion
}
