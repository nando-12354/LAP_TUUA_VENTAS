/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Creación de Usuario
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
using System.Text;

public partial class Modulo_Mantenimiento_MantenimientoUsuario : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    ArrayList alCodigoRol = new ArrayList();
    DataTable dt_parametro;
    bool flagError;
    int DiasVencimiento;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblNombre.Text = htLabels["musuario.lblNombre"].ToString();
                lblApellido.Text = htLabels["musuario.lblApellido"].ToString();
                lblCuenta.Text = htLabels["musuario.lblCuenta"].ToString();
                lblClave.Text = htLabels["musuario.lblClave"].ToString();
                lblConfirmaClave.Text = htLabels["musuario.lblConfirmaClave"].ToString();
                lblFechaVigencia.Text = htLabels["musuario.lblFechaVigencia"].ToString();
                lblGrupo.Text = htLabels["musuario.lblGrupo"].ToString();
                lblRoles.Text = htLabels["musuario.lblRoles"].ToString();
                lblRolesAsignados.Text = htLabels["musuario.lblRolesAsignados"].ToString();
                btnAceptar.Text = htLabels["musuario.btnAceptar"].ToString();
                btnAsignar.Text = htLabels["musuario.btnAsignar"].ToString();
                btnDesasignar.Text = htLabels["musuario.btnDesasignar"].ToString();
                rfvNombre.ErrorMessage = htLabels["musuario.rfvNombre"].ToString();
                rfvApellido.ErrorMessage = htLabels["musuario.rfvApellido"].ToString();
                rfvCuenta.ErrorMessage = htLabels["musuario.rfvCuenta"].ToString();
                rfvClave.ErrorMessage = htLabels["musuario.rfvClave"].ToString();
                rfvConfirmaClave.ErrorMessage = htLabels["musuario.rfvConfirmaClave"].ToString();
                cvdConfirmaClave.ErrorMessage = htLabels["musuario.cvdConfirmaClave"].ToString();
               //revFechaVigencia.ErrorMessage = htLabels["musuario.revFechaVigencia"].ToString();
               rfvFechaVigencia.ErrorMessage = htLabels["musuario.rfvFechaVigencia"].ToString();
               btnAceptar_ConfirmButtonExtender.ConfirmText = htLabels["musuario.cbeAceptar"].ToString();

               CargarComboGrupo((string)Session["Cod_Grupo"]);
               ddlGrupo.Enabled = PermisoGrupo();
               CargarRoles();
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


            limpiar();
            this.txtNombre.Focus();
        }
        this.txtClave.Attributes.Add("value", txtClave.Text);
        this.txtConfirmaClave.Attributes.Add("value", txtConfirmaClave.Text);

        //Page.ClientScript.GetPostBackClientHyperlink(this.btnAceptar, String.Empty);
        //StringBuilder sb = new StringBuilder();
        //sb.Append(Page.ClientScript.GetPostBackEventReference(this.btnAceptar, null));
        //sb.Append(";");
        //btnAceptar.Attributes.Add("onclick",sb.ToString());
    }


    public void CargarComboGrupo(string strGrupo)
    {
        DataTable dt_grupo = new DataTable();

        if (strGrupo == "T")
            dt_grupo = objBOConfiguracion.ObtenerListaDeCampo("GrupoUsuario", "");
        else if (strGrupo == "L")
            dt_grupo = objBOConfiguracion.ObtenerListaDeCampo("GrupoUsuario", "L");
        else if (strGrupo == "R")
            dt_grupo = objBOConfiguracion.ObtenerListaDeCampo("GrupoUsuario", "R");
        else
            dt_grupo = objBOConfiguracion.ObtenerListaDeCampo("GrupoUsuario", "");

        UIControles objCargaComboUsuario = new UIControles();
        objCargaComboUsuario.LlenarCombo(ddlGrupo, dt_grupo, "Cod_Campo", "Dsc_Campo", false, false);
        ddlGrupo.SelectedValue = strGrupo;
    }

    void Page_Init(Object o, EventArgs e)
    {
        try
        {
            this.lstRoles.DataSource = this.odsListarRoles;
            this.lstRoles.DataTextField = "SNomRol";
            this.lstRoles.DataValueField = "SCodRol";
            lstRoles.DataBind();
            this.ddlGrupo.DataSource = this.odsListarGrupos;
            this.ddlGrupo.DataTextField = "SDscCampo";
            this.ddlGrupo.DataValueField = "SCodCampo";
            ddlGrupo.DataBind();

            dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarParametros("KZ");

            if (dt_parametro.Rows.Count > 0)
            {
                int maxClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                this.txtClave.MaxLength = maxClave;
                this.txtConfirmaClave.MaxLength = maxClave;
            }
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

    void limpiar()
    {

        txtNombre.Text = "";
        txtApellido.Text = "";
        txtCuenta.Text = "";
        txtClave.Text = "";
        txtConfirmaClave.Text = "";

        DataTable dt_parametro = new DataTable();
        dt_parametro = objBOConsultas.ListarParametros("FV");

        if (dt_parametro.Rows.Count > 0)
        {
            DiasVencimiento = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }

        DataTable dtFecha = new DataTable();

        dtFecha = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFecha.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFecha.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);
        DateTime diaV = hoy.AddDays(+DiasVencimiento);

        txtFechaVigencia.Text = diaV.Day.ToString() + "/" + diaV.Month.ToString() + "/" + diaV.Year.ToString();
        lstRoles.Items.Clear();
        lstRoles.DataBind();
        lsRolesAsignados.Items.Clear();

    }

    bool valida()
    {
        int minClave = 0;
        int maxClave = 0;
        DataTable dt_parametro = new DataTable();
        dt_parametro = objBOConsultas.ListarParametros("KA");

        if (dt_parametro.Rows.Count > 0)
        {
            minClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }

        dt_parametro = new DataTable();
        dt_parametro = objBOConsultas.ListarParametros("KZ");

        if (dt_parametro.Rows.Count > 0)
        {
            maxClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }

        if (txtClave.Text.Length < minClave || txtClave.Text.Length > maxClave)
        {
            this.lblMensajeError.Text = "La clave debe tener entre " + minClave + " y " + maxClave + " caracteres";
            return false;
        }


        if (lsRolesAsignados.Items.Count <= 0)
        {
            this.lblMensajeError.Text = "Asigne como mínimo un rol al Usuario";
            return false;
        }

        DataTable dtFecha = new DataTable();

        dtFecha = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFecha.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFecha.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);
        DateTime diaV = Convert.ToDateTime(this.txtFechaVigencia.Text);


        if (diaV <= hoy)
        {
            this.lblMensajeError.Text = "La Fecha de Vigencia no puede ser menor o igual a la fecha actual";
            return false;
        }

        this.lblMensajeError.Text = "";
        return true;
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (valida() == true)
        {
            try
            {
                objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                if (objBOSeguridad.obtenerUsuarioxCuenta(this.txtCuenta.Text.Trim()) == null)
                {

                    Usuario objUsuario = new Usuario("", txtNombre.Text.Trim(), txtApellido.Text.Trim(), txtCuenta.Text.Trim(),
                        txtClave.Text.Trim(), "", "", Convert.ToDateTime(this.txtFechaVigencia.Text), ddlGrupo.SelectedItem.Value,
                        "", "","", Convert.ToString(Session["Cod_Usuario"]), "", "", chkDestrabe.Checked ? "1" : "0", "", 
                        chkMolinete.Checked ? "1" : "0", "");

                    if (objBOSeguridad.registrarUsuario(objUsuario) == true)
                    {
                        string sCodigo;
                        sCodigo = objBOSeguridad.autenticar(txtCuenta.Text, txtClave.Text).SCodUsuario;

                        for (int i = 0; i < this.lsRolesAsignados.Items.Count; i++)
                        {
                            objBOSeguridad.registrarRolUsuario(new UsuarioRol(sCodigo, Convert.ToString(lstCodRolesAsignados.Items[i].Text), Convert.ToString(Session["Cod_Usuario"]), "", ""));
                        }
                        omb.ShowMessage("Usuario registrado correctamente", "Creacion de Usuario", "Seg_VerUsuario.aspx");
                    }
                }
                else
                {
                    this.lblMensajeError.Text = "La cuenta ya se encuentra registrada, verifique por favor";
                }
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

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        if (this.lstRoles.SelectedIndex != -1)
        {
            lsRolesAsignados.Items.Add(lstRoles.Items[lstRoles.SelectedIndex]);
            this.lstCodRolesAsignados.Items.Add(lstRoles.Items[lstRoles.SelectedIndex].Value);
            
            lstRoles.Items.Remove(lstRoles.Items[lstRoles.SelectedIndex]);

        }
    }


    protected void btnDesasignar_Click(object sender, EventArgs e)
    {
        if (this.lsRolesAsignados.SelectedIndex != -1)
        {
            lstRoles.Items.Add(lsRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
            lstCodRolesAsignados.Items.Remove(lsRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
            lsRolesAsignados.Items.Remove(lsRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
        }
    }

    protected bool PermisoGrupo()
    {
        DataTable dtPermisos = (DataTable)Session["DataMapSite"];
        for (int i = 0; i < dtPermisos.Rows.Count; i++)
        {
            if (dtPermisos.Rows[i].ItemArray[0].ToString().Trim() == Define.ID_PROC_CAMBIO_GRUPO)
            {
                return true;
            }
        }
        return false;
    }

    private void CargarRoles()
    {
        List<Rol> lstRolUsrSession = objBOSeguridad.listarRolesAsignados(Session["Cod_Usuario"].ToString());
        List<Rol> lstRoles = (List<Rol>)this.odsListarRoles.Select();
       
        this.lstRoles.DataTextField = "SNomRol";
        this.lstRoles.DataValueField = "SCodRol";
        if (Session["Cod_Usuario"].ToString().ToUpper() != Property.htProperty["COD_USR_ADMIN"].ToString().ToUpper())
        {
            List<Rol> lstNoRolesOk = ObtenerRolesMenorPrivilegio(lstRolUsrSession, lstRoles);
            this.lstRoles.DataSource = lstNoRolesOk;
        }
        else
        {
            this.lstRoles.DataSource = lstRoles;
        }
        this.lstRoles.DataBind();
    }

    private List<Rol> ObtenerRolesMenorPrivilegio(List<Rol> lstSiRoles, List<Rol> lstNoRoles)
    {
        string strRolPadre = "";
        List<Rol> lstOKNoRoles = new List<Rol>();
        for (int k = 0; k < lstSiRoles.Count; k++)
        {
            strRolPadre = lstSiRoles[k].SCodRol;
            if (strRolPadre == null)
            {
                return lstNoRoles;
            }
            for (int i = 0; i < lstNoRoles.Count; i++)
            {
                if (EsRolMenorPrivilegio(lstNoRoles, lstNoRoles[i].SCodRol, strRolPadre))
                {
                    if (!EstaRolEnListaRoles(lstOKNoRoles, lstNoRoles[i].SCodRol))
                    {
                        lstOKNoRoles.Add(lstNoRoles[i]);
                    }
                }
            }
        }
        return lstOKNoRoles;
    }

    private bool EsRolMenorPrivilegio(List<Rol> lstNoRoles, string strRol1, string strRol2)
    {
        for (int i = 0; i < lstNoRoles.Count; i++)
        {
            if (lstNoRoles[i].SCodRol == strRol1)
            {
                if (lstNoRoles[i].SCodPadreRol == strRol2 || lstNoRoles[i].SCodRol == strRol2)
                {
                    return true;
                }
                else
                {
                    return EsRolMenorPrivilegio(lstNoRoles, lstNoRoles[i].SCodPadreRol, strRol2);
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
}
