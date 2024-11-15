/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Modificación de Usuario
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
using LAP.TUUA.ALARMAS;

public partial class Modulo_Mantenimiento_MantenimientoUsuario : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Operacion objBOOperacion = new BO_Operacion();
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();

    private Cryptografia objCrypto = new Cryptografia();
    DataTable dt_parametro;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblCodigo.Text = htLabels["musuario.lblCodigo"].ToString();
                lblNombre.Text = htLabels["musuario.lblNombre"].ToString();
                lblApellido.Text = htLabels["musuario.lblApellido"].ToString();
                lblCuenta.Text = htLabels["musuario.lblCuenta"].ToString();
                lblClave.Text = htLabels["musuario.lblClave"].ToString();
                lblConfirmaClave.Text = htLabels["musuario.lblConfirmaClave"].ToString();
                lblFechaVigencia.Text = htLabels["musuario.lblFechaVigencia"].ToString();
                lblEstado.Text = htLabels["musuario.lblEstado"].ToString();
                lblGrupo.Text = htLabels["musuario.lblGrupo"].ToString();
                lblRoles.Text = htLabels["musuario.lblRoles"].ToString();
                lblRolesAsignados.Text = htLabels["musuario.lblRolesAsignados"].ToString();
                btnActualizar.Text = htLabels["musuario.btnActualizar"].ToString();
                btnAsignar.Text = htLabels["musuario.btnAsignar"].ToString();
                btnDesasignar.Text = htLabels["musuario.btnDesasignar"].ToString();
                rfvNombre.ErrorMessage = htLabels["musuario.rfvNombre"].ToString();
                rfvApellido.ErrorMessage = htLabels["musuario.rfvApellido"].ToString();
                rfvCuenta.ErrorMessage = htLabels["musuario.rfvCuenta"].ToString();
                cvdConfirmaClave.ErrorMessage = htLabels["musuario.cvdConfirmaClave"].ToString();
                revFechaVigencia.ErrorMessage = htLabels["musuario.revFechaVigencia"].ToString();
                rfvFechaVigencia.ErrorMessage = htLabels["musuario.rfvFechaVigencia"].ToString();
                chkFlagCambioClave.Text = htLabels["musuario.chkFlagCambioClave"].ToString();

                btnActualizar_ConfirmButtonExtender.ConfirmText = htLabels["musuario.cbeActualizar"].ToString();

                dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("KZ");

                if (dt_parametro.Rows.Count > 0)
                {
                    int maxClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                    this.txtClave.MaxLength = maxClave;
                    this.txtConfirmaClave.MaxLength = maxClave;
                }

                PoblarUsuario(Request.QueryString["Cod_Usuario"]);
                DeshabilitaCambioPassword();

                CargarComboGrupo(Request.QueryString["Cod_Grupo"]);

                ddlGrupo.Enabled = PermisoGrupo();

            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegModificarUsuario;
                Response.Redirect("PaginaError.aspx");
            }
            this.txtNombre.Focus();
            this.txtClave.Attributes.Add("value", txtClave.Text);
            this.txtConfirmaClave.Attributes.Add("value", txtConfirmaClave.Text);
        }


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

    void PoblarUsuario(string CodUsuario)
    {
        try
        {
            Usuario objUsuario = new Usuario();

            objUsuario = objBOSeguridad.obtenerUsuario(CodUsuario);
            txtCodigo.Text = CodUsuario;
            txtNombre.Text = objUsuario.SNomUsuario;
            txtApellido.Text = objUsuario.SApeUsuario;
            txtCuenta.Text = objUsuario.SCtaUsuario;
            lblCuentaReg.Text = txtCuenta.Text;

            if (objUsuario.SFlgCambioClave == "1")
                this.chkFlagCambioClave.Checked = true;
            else
                this.chkFlagCambioClave.Checked = false;

            txtClave.Text = "";
            // txtClave.Text = objUsuario.SPwdActualUsuario;
            // lblPassword.Text = txtClave.Text;
            txtConfirmaClave.Text = "";
            txtFechaVigencia.Text = objUsuario.DtFchVigencia.Date.ToString("dd/MM/yyyy");
            this.ddlEstado.DataSource = this.odsListarEstado;
            this.ddlEstado.DataTextField = "SDscCampo";
            this.ddlEstado.DataValueField = "SCodCampo";
            ddlEstado.DataBind();

            /*this.ddlGrupo.DataSource = this.odsListarGrupos;
            this.ddlGrupo.DataTextField = "SDscCampo";
            this.ddlGrupo.DataValueField = "SCodCampo";
            ddlGrupo.DataBind();
            */
            //List<Rol> lstSiRoles = (List<Rol>)this.odsListarRolesAsignados.Select();
            List<Rol> lstSiRoles =objBOSeguridad.listarRolesAsignados(Session["Cod_Usuario"].ToString());
            this.lsRolesAsignados.DataTextField = "SNomRol";
            this.lsRolesAsignados.DataValueField = "SCodRol";
            this.lsRolesAsignados.DataSource = this.odsListarRolesAsignados;
            lsRolesAsignados.DataBind();
            List<Rol> lstNoRoles = (List<Rol>)this.odsListarRolesNoAsignados.Select();
            //List<Rol> lstNoRoles = objBOSeguridad.listaDeRoles();
            this.lstRoles.DataTextField = "SNomRol";
            this.lstRoles.DataValueField = "SCodRol";
            if (Session["Cod_Usuario"].ToString().ToUpper() != Property.htProperty["COD_USR_ADMIN"].ToString().ToUpper())
            {
                MergerListaRol(lstNoRoles, (List<Rol>)this.odsListarRolesAsignados.Select());
                List<Rol> lstNoRolesOk=ObtenerRolesMenorPrivilegio(lstSiRoles, lstNoRoles);
                this.lstRoles.DataSource = lstNoRolesOk;
            }
            else
            {
                this.lstRoles.DataSource = lstNoRoles;
            }
            lstRoles.DataBind();


            ddlEstado.SelectedIndex = IndexListaCampo(objUsuario.STipoEstadoActual, ddlEstado);
            ddlGrupo.SelectedIndex = IndexListaCampo(objUsuario.STipoGrupo, ddlGrupo);

            this.lblEstadoNuevo.Text = ddlEstado.SelectedValue;

            this.chkDestrabe.Checked = objUsuario.BGenerarCodeBarDestrabe == "1" ? true : false;
            this.lblDestrabe.Text = objUsuario.SCodeBarDestrabe;
            this.chkMolinete.Checked = objUsuario.BGenerarCodeBarMolinete == "1" ? true : false;
            this.lblMolinete.Text = objUsuario.SCodeBarMolinete;

        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }

    int IndexListaCampo(string CodCampo, DropDownList ddlBox)
    {

        for (int i = 0; i < ddlBox.Items.Count; i++)
        {
            if (ddlBox.Items[i].Value == CodCampo)
            {

                return i;
            }
        }
        return 0;

    }


    protected void HabilitaCambioPassword()
    {
        this.txtClave.Text = "";
        this.txtConfirmaClave.Text = "";
        this.txtClave.Enabled = true;
        this.txtConfirmaClave.Enabled = true;
        txtClave.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        txtConfirmaClave.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        lblPassword.Text = "1";
        lblErrorMsg.Text = "";
        txtClave.Focus();
        this.lblHabilitar.Text = "Deshabilitar";
    }


    protected void DeshabilitaCambioPassword()
    {
        this.txtClave.Text = "";
        this.txtClave.Enabled = false;
        this.txtConfirmaClave.Text = "";
        this.txtConfirmaClave.Enabled = false;
        txtClave.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
        txtConfirmaClave.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
        lblPassword.Text = "0";
        lblErrorMsg.Text = "";
        txtNombre.Focus();
        this.lblHabilitar.Text = "Habilitar";
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        lblMensajeError.Text = "";

        if (objBOOperacion.verificarTurnoCerradoxUsuario(this.txtCodigo.Text.Trim()))
        {
            this.btnActualizar.Focus();
            if (valida() == true)
            {
                string sFlagClave = "0";
                if (chkFlagCambioClave.Checked == true)
                    sFlagClave = "1";
                else
                    sFlagClave = "0";

                Usuario objUsuario = new Usuario(this.txtCodigo.Text.Trim(), txtNombre.Text.Trim(), txtApellido.Text.Trim(), txtCuenta.Text.Trim(),
                        "", sFlagClave, "", Convert.ToDateTime(this.txtFechaVigencia.Text), ddlGrupo.SelectedItem.Value,
                        "", "", "", Convert.ToString(Session["Cod_Usuario"]), "", "", chkDestrabe.Checked ? "1" : "0",
                        lblDestrabe.Text, chkMolinete.Checked ? "1" : "0", lblMolinete.Text);

                try
                {
                    objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    if (validaCuenta(this.txtCuenta.Text.Trim()) == true)
                    {
                        if (objBOSeguridad.actualizarUsuario(objUsuario) == true)
                        {
                            if (validaContraseña(this.txtCodigo.Text.Trim(), this.txtClave.Text.Trim()) == true)
                            {
                                actualizaClave(objUsuario);
                                actualizaEstado(objUsuario);

                                for (int i = 0; i < this.lsRolesAsignados.Items.Count; i++)
                                {
                                    if (objBOSeguridad.obtenerRolUsuario(this.txtCodigo.Text.Trim(), Convert.ToString(lsRolesAsignados.Items[i].Value)) == null)
                                    {
                                        objBOSeguridad.registrarRolUsuario(new UsuarioRol(txtCodigo.Text, Convert.ToString(lsRolesAsignados.Items[i].Value), Convert.ToString(Session["Cod_Usuario"]), "", ""));
                                        //GeneraAlarma
                                        string IpClient = Request.UserHostAddress;
                                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000007", "001", IpClient, "1", "Alerta W0000007", "Se agrego al usuario " + txtCodigo.Text + " el rol " + Convert.ToString(lstCodRolesAsignados.Items[i].Text) + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                                    }
                                }

                                for (int i = 0; i < this.lstRoles.Items.Count; i++)
                                {
                                    if (objBOSeguridad.obtenerRolUsuario(this.txtCodigo.Text, Convert.ToString(lstRoles.Items[i].Value)) != null)
                                    {
                                        objBOSeguridad.eliminarRolUsuario(this.txtCodigo.Text, Convert.ToString(lstRoles.Items[i].Value));
                                    }
                                }
                                omb.ShowMessage("Usuario actualizado correctamente", "Modificar Usuario", "Seg_VerUsuario.aspx");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
        }
        else
        {
            this.lblMensajeError.Text = "La cuenta tiene un turno abierto, debe ser cerrado el turno antes de cualquier cambio";
        }

    }

    protected bool validaCuenta(string sCtaUsuario)
    {
        if (txtCuenta.Text != lblCuentaReg.Text)
        {
            if (objBOSeguridad.obtenerUsuarioxCuenta(this.txtCuenta.Text) != null)
            {
                this.lblMensajeError.Text = "La cuenta ya se encuentra registrada, verifique por favor";
                return false;
            }

        }
        return true;
    }

    protected bool validaContraseña(string sCodUsuario, string sContraseña)
    {
        if (validaCambioContraseña())
        {
            DataTable dtConsulta = new DataTable();
            string sKey = "";
            string sPwEncriptado = "";
            dtConsulta = objBOConsultas.ListarParametros("KI");
            if (dtConsulta.Rows.Count > 0)
            {
                sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                sPwEncriptado = objCrypto.Encriptar(sContraseña, sKey);
            }

            if (this.txtConfirmaClave.Text != "")
            {
                if (validaTamañoClave())
                {
                    if (validaTurnoUsuario(sCodUsuario) == true)
                    {
                        if (objBOSeguridad.obtenerClaveUsuHist(sCodUsuario, this.txtClave.Text) == true)
                        {
                            dtConsulta = new DataTable();
                            int iNumKeyHist = 0;
                            dtConsulta = objBOConsultas.ListarParametros("KH");
                            if (dtConsulta.Rows.Count > 0)
                            {
                                iNumKeyHist = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                            }
                            this.lblMensajeError.Text = "La contraseña se encuentra registrada dentro de los ultimos " + iNumKeyHist + " ingresos de Clave";
                            this.txtClave.Focus();
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


            }
        }
        else
        {
            return false;
        }
        return true;
    }


    protected bool validaCambioContraseña()
    {
        if (this.lblPassword.Text == "1")
        {
            if (this.txtClave.Text != "")
            {
                if (txtClave.Text != txtConfirmaClave.Text)
                {
                    this.txtClave.Attributes.Add("value", txtClave.Text);
                    this.lblErrorMsg.Text = "Confirmación de clave incorrecta";
                    return false;
                }
            }
            if (txtClave.Text.Trim() == "" && txtConfirmaClave.Text.Trim() == "")
            {
                this.lblErrorMsg.Text = "Ingrese una nueva Contraseña";
                return false;
            }
        }
        return true;
    }

    protected bool validaTurnoUsuario(string SCodUsuario)
    {

        bool TurnoCerrado = objBOOperacion.verificarTurnoCerradoxUsuario(SCodUsuario);

        if (TurnoCerrado == false)
        {
            this.lblMensajeError.Text = "El Usuario tiene asignado un turno en situación Abierto";
            this.txtClave.Focus();
            return false;
        }
        return true;

    }


    protected bool validaTamañoClave()
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

        if (minClave <= maxClave)
        {
            if (txtClave.Text.Length < minClave || txtClave.Text.Length > maxClave)
            {
                if (minClave != maxClave)
                {
                    this.lblMensajeError.Text = "La clave debe tener entre " + minClave + " y " + maxClave + " caracteres";
                }
                else
                {
                    this.lblMensajeError.Text = "La clave debe tener " + minClave + "caracteres";
                }
                return false;
            }
        }
        else
        {
            this.lblMensajeError.Text = "Error de Configuración de Longitud de Contraseña, informe al administrador del sistema";
            return false;
        }

        return true;
    }


    protected void actualizaClave(Usuario objUsuario)
    {
        if (this.txtConfirmaClave.Text != "")
        {
            DataTable dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KI");
            if (dtConsulta.Rows.Count > 0)
            {
                string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                string sPwEncriptado;

                sPwEncriptado = objCrypto.Encriptar(txtClave.Text, sKey);

                if (lblPassword.Text != sPwEncriptado)
                {
                    objBOSeguridad.actualizarContraseñaUsuario(objUsuario.SCodUsuario, txtClave.Text, Convert.ToString(Session["Cod_Usuario"]), Convert.ToDateTime(this.txtFechaVigencia.Text));
                }
            }
            //GeneraAlarma icano 30-07-10
            string IpClient = Request.UserHostAddress;
            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000003", "001", IpClient, "1", "Alerta W0000003", "Se modifico contraseña del usuario : "+Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

        }

    }

    protected void actualizaEstado(Usuario objUsuario)
    {
        string sCamClave = "";
        if (this.lblEstadoNuevo.Text != this.ddlEstado.SelectedValue)
        {
            if (this.chkFlagCambioClave.Checked)
                sCamClave = "1";
            else
                sCamClave = "0";

            objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, this.ddlEstado.SelectedValue, Convert.ToString(Session["Cod_Usuario"]), sCamClave);
            if (this.ddlEstado.SelectedValue == "X")
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000008", "001", IpClient, "1", "Alerta W0000008", "Se anulo al usuario del sistema " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

        }
    }

    bool valida()
    {

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
        return true;

    }

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        if (this.lstRoles.SelectedIndex != -1)
        {
            lsRolesAsignados.Items.Add(lstRoles.Items[lstRoles.SelectedIndex]);
            //lstCodRolesAsignados.Items.Add(this.lstCodRolSinAsignar.Items[lstRoles.SelectedIndex].Value);
            lstCodRolesAsignados.Items.Add(lstRoles.Items[lstRoles.SelectedIndex].Value);
            lstCodRolSinAsignar.Items.Remove(lstCodRolSinAsignar.Items[lstRoles.SelectedIndex]);
            lstRoles.Items.Remove(lstRoles.Items[lstRoles.SelectedIndex]);
            //kinzi
            lstRoles.SelectedIndex = -1;
            lsRolesAsignados.SelectedIndex = -1;
        }

    }

    protected void btnDesasignar_Click(object sender, EventArgs e)
    {
        if (this.lsRolesAsignados.SelectedIndex != -1)
        {
            lstRoles.Items.Add(lsRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
            lstCodRolSinAsignar.Items.Add(lstCodRolesAsignados.Items[lsRolesAsignados.SelectedIndex].Value);
            lstCodRolesAsignados.Items.Remove(lstCodRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
            lsRolesAsignados.Items.Remove(lsRolesAsignados.Items[lsRolesAsignados.SelectedIndex]);
            //kinzi
            lstRoles.SelectedIndex = -1;
            lsRolesAsignados.SelectedIndex = -1;
        }

    }


    protected void imbPassword_Click(object sender, EventArgs e)
    {
        if (lblPassword.Text == "0")
            HabilitaCambioPassword();
        else
        {
            if (lblPassword.Text == "1")
                DeshabilitaCambioPassword();
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

    private List<Rol> ObtenerRolesMenorPrivilegio(List<Rol> lstSiRoles, List<Rol> lstNoRoles)
    {
        string strRolPadre = "";
        List<Rol> lstOKNoRoles = new List<Rol>();
        for (int k = 0; k < lstSiRoles.Count; k++)
        {
            //strRolPadre = lstSiRoles[k].SCodPadreRol;
            strRolPadre = lstSiRoles[k].SCodRol;
            if (strRolPadre == null)
            {
                return (List<Rol>)this.odsListarRolesNoAsignados.Select();
            }
            for (int i = 0; i < lstNoRoles.Count; i++)
            {
                if (EsRolMenorPrivilegio(lstNoRoles, lstNoRoles[i].SCodRol, strRolPadre))
                {
                    if (EstaRolEnListaRoles((List<Rol>)this.odsListarRolesAsignados.Select(),lstNoRoles[i].SCodRol))
                    {
                        continue;
                    }
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

    private void MergerListaRol(List<Rol> lstRoles1, List<Rol> lstRole2)
    {
        for (int i = 0; i < lstRole2.Count; i++)
        {
            if (!EstaRolEnListaRoles(lstRoles1, lstRole2[i].SCodRol))
            {
                lstRoles1.Add(lstRole2[i]);
            }
        }
    }

    protected void btnGenerarCodeBar_Click(object sender, EventArgs e)
    {
        if (!chkDestrabe.Checked && !chkMolinete.Checked)
        {
            //Response.Write("<script>alert('No hay datos a generar');</script>");            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('No hay datos a generar')", true);
        }
        else
        {
            Response.Redirect("~/ExpCodeBar/rptCodeBarDestrabe.aspx?codDestrabe=" + (chkDestrabe.Checked ? ("*" + lblDestrabe.Text.Trim() + "*") : "") + "&codMolinete=" + (chkMolinete.Checked ? ("*" + lblMolinete.Text.Trim() + "*") : ""));
        }
    }
}
