/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Cambiar Contraseña
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
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

public partial class CambiarContrasena : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Seguridad objBOSeguridad = new BO_Seguridad();
    protected BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Operacion objBOOperacion = new BO_Operacion();
    protected Usuario objUsuario = new Usuario();
    bool flagError;
    DataTable dt_parametro;
    private bool bActualizarEstado = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["est"] != null)
            {
                bActualizarEstado = Request.QueryString["est"].ToString().Trim().Equals("V") ? true : false;
            }

            LabelConfig.LoadData();
            htLabels = LabelConfig.htLabels;


            objUsuario = objBOSeguridad.obtenerUsuario(Convert.ToString(Session["Cod_Usuario"]));

            this.txtUsuario.Focus();
            this.lblUsuario.Text = htLabels["CambiarContrasena.lblUsuario"].ToString();
            this.lblPassword.Text = htLabels["CambiarContrasena.lblPassword"].ToString();
            this.lblConfPassword.Text = htLabels["CambiarContrasena.lblConfPassword"].ToString();
            this.lblEmpresaTuua.Text = htLabels["CambiarContrasena.lblEmpresaTuua"].ToString();
            this.lblDerechoTuua.Text = htLabels["CambiarContrasena.lblDerechoTuua"].ToString();
            this.lblCambiarContraseña.Text = htLabels["CambiarContrasena.lblCambiarContraseña"].ToString();
            this.btnActualizar.Text = htLabels["CambiarContrasena.btnActualizar"].ToString();
            this.btnCancelar.Text = htLabels["CambiarContrasena.btnCancelar"].ToString();
            rfvClave.ErrorMessage = htLabels["CambiarContrasena.rfvClave"].ToString();
            rfvConfirmaClave.ErrorMessage = htLabels["CambiarContrasena.rfvConfirmaClave"].ToString();
            cvdConfirmaClave.ErrorMessage = htLabels["CambiarContrasena.cvdConfirmaClave"].ToString();
            cbeAceptar.ConfirmText = htLabels["CambiarContrasena.cbeAceptar"].ToString();
            
            DataTable dtFechaHoy = new DataTable();

            dtFechaHoy = objBOSeguridad.obtenerFecha();

            string[] sFechaHora = new string[2];

            if (dtFechaHoy.Rows.Count > 0)
            {
                sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
            }

            string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);
            DateTime hoy = Convert.ToDateTime(sFecha);
            this.lblFecha.Text = hoy.ToString("dddd") + ", " + hoy.Day.ToString() + " de " + hoy.ToString("MMMM") + " de " + hoy.ToString("yyyy");
            this.txtUsuario.Text = objUsuario.SCtaUsuario;
            this.txtPassword.Focus();
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
            dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarParametros("KZ");

            if (dt_parametro.Rows.Count > 0)
            {
                int maxClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                this.txtPassword.MaxLength = maxClave;
                this.txtConfPassword.MaxLength = maxClave;
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

        int s_timeout = (int)Session["TimeInactivo"] * 1000;
        string str_Script = "StartTime(" + s_timeout.ToString() + ");";
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CheckSessionOut", str_Script, true);


    }
    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], "ADM", "L01");
            if (validaContraseña(objUsuario.SCodUsuario, this.txtPassword.Text))
            {
                if (objBOSeguridad.actualizarContraseñaUsuario(objUsuario.SCodUsuario, this.txtPassword.Text, objUsuario.SCodUsuario, objUsuario.DtFchVigencia) == true)
                {
                    if (bActualizarEstado)
                    {
                        objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "V", objUsuario.SCodUsuario, "0");
                    }
                    //if (objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "V", objUsuario.SCodUsuario, "0"))
                    //{
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000003", "001", IpClient, "1", "Alerta W0000003", "Cambio de Contraseña, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                        omb.ShowMessage("Contraseña actualizada correctamente", "Cambio de Contraseña", "Login.aspx");
                    //}
                }
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


    protected bool validaContraseña(string sCodUsuario, string sContraseña)
    {
        if (validaTamañoClave())
        {
            if (validaTurnoUsuario(sCodUsuario) == true)
            {
                if (objBOSeguridad.obtenerClaveUsuHist(sCodUsuario, sContraseña) == true)
                {
                    DataTable dtConsulta = new DataTable();
                    int iNumKeyHist = 0;
                    dtConsulta = objBOConsultas.ListarParametros("KH");
                    if (dtConsulta.Rows.Count > 0)
                    {
                        iNumKeyHist = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                    }
                    this.lblMensajeError.Text = "La contraseña se encuentra registrada dentro de los ultimos " + iNumKeyHist + " ingresos de Clave";
                    this.txtUsuario.Focus();
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
        return true;

    }

    protected bool validaTurnoUsuario(string SCodUsuario)
    {

        bool TurnoCerrado = objBOOperacion.verificarTurnoCerradoxUsuario(SCodUsuario);

        if (TurnoCerrado == false)
        {
            this.lblMensajeError.Text = "El Usuario tiene asignado un turno en situación Abierto";
            this.txtUsuario.Focus();
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
            if (txtPassword.Text.Length < minClave || txtPassword.Text.Length > maxClave)
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



    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}
