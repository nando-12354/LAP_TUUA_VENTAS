using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

public partial class DesbloqueoAdmin : System.Web.UI.Page
{

    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    bool flagError;
    DataTable dt_parametro;
    protected BO_Seguridad objBOSeguridad;
    protected BO_Consultas objBOConsultas;



    public DesbloqueoAdmin()
    {
        try
        {
            Property.CargarPropiedades(AppDomain.CurrentDomain.BaseDirectory + "resources/");
            //carga path de recursos
            if (!Property.htProperty.ContainsKey("PATHRECURSOS"))
            {
                Property.htProperty.Add("PATHRECURSOS", HttpContext.Current.Server.MapPath("."));
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

        objBOSeguridad = new BO_Seguridad();
        objBOConsultas = new BO_Consultas();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //carga de parametros
            CargarParametros();
            ErrorHandler.CargarErrorTypes(AppDomain.CurrentDomain.BaseDirectory + "resources");

            htSPConfig = new Hashtable();
            SPConfigXml objSPConfig = new SPConfigXml();
            try
            {
                objSPConfig.cargarSPConfig(HttpContext.Current.Server.MapPath(""));
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_002;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }

            htSPConfig = objSPConfig.HtSPConfig;
            Session["htSPConfig"] = htSPConfig;

            try
            {
                LabelConfig.LoadData();
                htLabels = LabelConfig.htLabels;

                this.txtUsuario.Focus();
                this.lblUsuario.Text = htLabels["DesbloqAdmin.lblUsuario"].ToString();
                this.lblPassword.Text = htLabels["DesbloqAdmin.lblPassword"].ToString();
                this.lblEmpresaTuua.Text = htLabels["DesbloqAdmin.lblEmpresaTuua"].ToString();
                this.lblDerechoTuua.Text = htLabels["DesbloqAdmin.lblDerechoTuua"].ToString();
                this.btnActualizar.Text = htLabels["DesbloqAdmin.btnActualizar"].ToString();
                this.btnCancelar.Text = htLabels["DesbloqAdmin.btnCancelar"].ToString();
                this.txtUsuario.Text = "ADMIN";
                lblDesbloqueoAdmin.Text = htLabels["DesbloqAdmin.lblBienvenida"].ToString(); 
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

                dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("KZ");

                if (dt_parametro.Rows.Count > 0)
                {
                    int maxClave = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                    this.txtPassword.MaxLength = maxClave;
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

    private void CargarParametros()
    {
        BO_Configuracion objBOConfigura = new BO_Configuracion();
        dt_parametro = objBOConfigura.ListarAllParametroGenerales(null);
        Hashtable htParametro = new Hashtable();
        for (int i = 0; i < dt_parametro.Rows.Count; i++)
        {
            htParametro.Add(dt_parametro.Rows[i].ItemArray.GetValue(0).ToString().Trim(), dt_parametro.Rows[i].ItemArray.GetValue(5).ToString().Trim());
        }
        Session["htParametro"] = htParametro;
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        dt_parametro = new DataTable();
        dt_parametro = objBOConsultas.ListarParametros("PWD");
        string Clave = "";

        if (dt_parametro.Rows.Count > 0)
        {
            Clave = dt_parametro.Rows[0].ItemArray.GetValue(4).ToString();
        }

        if (Clave != "")
        {
            if (Clave.ToUpper().Equals(txtPassword.Text.Trim().ToUpper()))
            {
                //Desbloquear Admin y Retornar a Login.aspx
                objBOSeguridad = new BO_Seguridad("U000001", "ADM", "L01");
                Usuario objUsuario = new Usuario();
                objUsuario = objBOSeguridad.obtenerUsuario("U000001");
                objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "V", objUsuario.SCodUsuario, "0");
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMensajeError.Text = "Clave Incorrecta";
            }
        }

    }
}
