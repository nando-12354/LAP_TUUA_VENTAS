/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Logeo
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/

using System;
using System.Linq;
using System.Xml.Linq;
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
using System.Xml;
using LAP.TUUA.ALARMAS;

public partial class Logeo : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    protected BO_Seguridad objBOSeguridad;
    protected BO_Consultas objBOConsultas;

    bool flagError;
    DataTable dt_parametro;

    public Logeo()
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

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        autenticar("Principal.aspx", false);
    }


    protected void autenticar(string strPaginaDestino, bool cambContraseña)
    {
        Usuario objUsuario = null;

        Usuario objCuentaUsuario = validaCuenta(this.txtUsuario.Text.Trim(), cambContraseña);

        if (objCuentaUsuario != null)
        {
            //Begin - kinzi - 300310 - CorrigeObservacion 5 de LAP
            htLabels = LabelConfig.htLabels;
            DataTable dtMenuCheck = new DataTable();
            try{
                dtMenuCheck = objBOSeguridad.LlenarDatosMenu(objCuentaUsuario.SCodUsuario);
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
            bool flg_MenuCheck = true;
            if (!(dtMenuCheck.Rows.Count > 0)) 
            {
                flg_MenuCheck = false;
            }
            //End

            if (flg_MenuCheck)
            {

                try
                {
                    objUsuario = objBOSeguridad.autenticar(this.txtUsuario.Text, this.txtPassword.Text);
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

                if (objUsuario != null)
                {
                    //carga de parametros
                    CargarParametros();
                    // carga los parametros de impresion (GGarcia-20090923)
                    CargarParamImpresion();
                    CargarListaCampoPasajero();
                    //carga de parametro Maximo Grilla
                    DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                    Session["GridViewRows"] = dt_parametro.Rows[0].ItemArray.GetValue(4).ToString();

                    //Obtenemos Tiempo Restante de Vigencia de la Cuenta
                    if (objUsuario.SCodUsuario != "U000001")
                    {
                        obtenerTiempoVigenciaRestante(objUsuario);
                    }
                    else
                    {
                        Session["TimeLife"] = null;
                        Session["TimeLifeHour"] = null;
                        Session["TimeLifeMin"] = null;
                        Session["TimeLifeSeg"] = null;
                    }

                    if (cambContraseña == false)
                    {
                        if (validaUsuario(objUsuario) == true)
                        {
                            FormsAuthentication.SetAuthCookie(this.txtUsuario.Text, false);
                            Session["Cod_Usuario"] = objUsuario.SCodUsuario;
                            // guarda el nombre del usuario en sesion (GGarcia-20090924)
                            Session["Nombre_Usuario"] = objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario;
                            Property.dtMenu = objBOSeguridad.LlenarDatosMenu(objUsuario.SCodUsuario);
                            Session["DataMenu"] = Property.dtMenu;
                            Property.dtMapSite = objBOSeguridad.LlenarDatosMapSite(objUsuario.SCodUsuario);
                            Session["DataMapSite"] = Property.dtMapSite;
                            Session["TimeInactivo"] = TiempoInactivo();
                            Session.Timeout = TiempoInactivo() / 60;
                            Session["Cod_Grupo"] = objUsuario.STipoGrupo;
                            objBOSeguridad.InsertarAuditoria("ADM", "L01", objUsuario.SCodUsuario, "1");
                            //kinzi
                            Session["Page_Height"] = this.hdSizeH.Value;
                            Session["Cta_Usuario"] = objUsuario.SCtaUsuario;
                            Response.Redirect(strPaginaDestino);
                        }
                    }
                    else
                    {
                        if (validaEstadoUsuario(objUsuario) == true)
                        {
                            Session["TimeInactivo"] = TiempoInactivo();
                            Session.Timeout = TiempoInactivo()/60;
                            Session["Cod_Usuario"] = objUsuario.SCodUsuario;
                            objBOSeguridad.InsertarAuditoria("L01", "E9012", objUsuario.SCodUsuario, "1");
                            if (verificaVigenciaUsuario(objUsuario) && !verificaVigenciaClave(objUsuario))
                            {
                                Response.Redirect(strPaginaDestino + "?est=V");
                            }
                            else
                            {
                                Response.Redirect(strPaginaDestino);
                            }
                        }
                    }
                }
                else
                {
                    this.lblMensajeError.Text = "Contraseña Incorrecta";
                    validaNumeroIntentos(objCuentaUsuario);
                    this.lblNumIntentos.Text = Convert.ToString(Convert.ToInt32(lblNumIntentos.Text) + 1);
                    this.txtPassword.Focus();
                }

            }
            else {
                this.lblMensajeError.Text = htLabels["login.lblMensajeError1.Text"].ToString();
                validaNumeroIntentos(objCuentaUsuario);
                this.lblNumIntentos.Text = Convert.ToString(Convert.ToInt32(lblNumIntentos.Text) + 1);
                this.txtUsuario.Focus();
            }
        }
    }


    //METODO USADO CUANDO SE QUIERE CAMBIAR CONTRASENIA
    protected bool validaEstadoUsuario(Usuario objUsuario)
    {
        string sEstado = objUsuario.STipoEstadoActual;
        string SFlagCambioClave = objUsuario.SFlgCambioClave;

        if (sEstado.CompareTo("V") != 0)
        {
            switch (sEstado)
            {
                case "B":

                    if (!objUsuario.SCodUsuario.Equals("U000001") && verificaVigenciaUsuario(objUsuario) && !verificaVigenciaClave(objUsuario))
                    {
                        break;
                    }
                    else
                    {
                        this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado";

                        if (objUsuario.SCodUsuario.Equals("U000001"))
                        {
                            Response.Redirect("DesbloqueoAdmin.aspx");
                        }

                        this.txtUsuario.Focus();
                        return false;
                    }
                case "X":
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                    this.txtUsuario.Focus();
                    return false;

                default:
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                    this.txtUsuario.Focus();
                    return false;
            }
        }
        return true;
    }


    protected int TiempoInactivo()
    {
        Hashtable htParametro = (Hashtable)Session["htParametro"];
        if (htParametro != null && htParametro["TM"] != null)
        {
            return Int32.Parse((string)htParametro["TM"]);
        }
        //DataTable dtConsulta = new DataTable();
        //dtConsulta = objBOConsultas.ListarParametros("TM");
        //if (dtConsulta.Rows.Count > 0)
        //{
        //    Int32 iTemInact = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
        //    return iTemInact;
        //}
        return Define.TIME_INACTIVO;
    }

    protected void validaNumeroIntentos(Usuario objUsuario)
    {
        bool isDesbloqueo = false;

        try
        {
            bool accion;
            DataTable dtMaxIntentos = new DataTable();
            dtMaxIntentos = objBOConsultas.ListarParametros("NI");

            if (dtMaxIntentos.Rows.Count > 0)
            {
                Int32 iMaxIntentos = Convert.ToInt32(dtMaxIntentos.Rows[0].ItemArray.GetValue(4).ToString());

                if (Convert.ToUInt32(this.lblNumIntentos.Text) >= iMaxIntentos)
                {
                    accion = objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, objUsuario.SFlgCambioClave);
                    
                    if (accion)
                    {
                        if (objUsuario.SCodUsuario == "U000001")
                        {
                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000002", "001", IpClient, "1", "Alerta W0000002", "Cuenta de Super usuario (Admin) Bloqueada, Ip Terminal: " + IpClient, objUsuario.SCodUsuario);
                            isDesbloqueo = true;                                                   
                        }
                        else
                        {
                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000001", "001", IpClient, "1", "Alerta W0000001", "Cuenta Bloqueada, Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                        }
                    }
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
            if (!flagError)
            {
                if (isDesbloqueo)
                {
                    //Response.Redirect("DesbloqueoAdmin.aspx");   
                }
            }
            else
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

    }

    protected Usuario validaCuenta(string strCuenta, bool CambioPw)
    {
        Usuario objUsuario = new Usuario();

        try
        {

            objUsuario = objBOSeguridad.obtenerUsuarioxCuenta(strCuenta);
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

        if (objUsuario != null)
        {
            string sEstado = objUsuario.STipoEstadoActual;
            string SFlagCambioClave = objUsuario.SFlgCambioClave;

            if (sEstado.CompareTo("V") != 0)
            {
                switch (sEstado)
                {
                    case "B":
                        if (!CambioPw)
                        {
                            if (!objUsuario.SCodUsuario.Equals("U000001") && verificaVigenciaUsuario(objUsuario) && !verificaVigenciaClave(objUsuario))
                            {
                                this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado. Su contraseña a caducado, por favor cambie de contraseña";
                            }
                            else
                            {
                                this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado";
                            }

                            if (objUsuario.SCodUsuario.Equals("U000001"))
                            {
                                if (objBOSeguridad.autenticar(this.txtUsuario.Text, this.txtPassword.Text) != null)
                                {
                                    Response.Redirect("DesbloqueoAdmin.aspx");
                                }
                            }

                            this.txtUsuario.Focus();
                        }
                        
                        break;
                    case "X":
                        this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                        this.txtUsuario.Focus();
                        break;
                    default:
                        this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                        this.txtUsuario.Focus();
                        break;
                }

                if (!CambioPw)
                {
                    return null;
                }
                else
                {
                    return objUsuario;
                }
            }
            else
            {
                return objUsuario;
            }
        }
        else
        {
            this.lblMensajeError.Text = "Usuario no registrado";
            this.txtUsuario.Focus();
            return null;
        }

    }

    protected bool validaUsuario(Usuario objUsuario)
    {

        string sEstado = objUsuario.STipoEstadoActual;
        string SFlagCambioClave = objUsuario.SFlgCambioClave;

        if (sEstado.CompareTo("V") != 0)
        {
            switch (sEstado)
            {
                case "B":
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado";
                       
                    if (objUsuario.SCodUsuario.Equals("U000001"))
                    {
                       Response.Redirect("DesbloqueoAdmin.aspx"); 
                    }
                   
                    this.txtUsuario.Focus();
                    break;
                case "X":
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                    this.txtUsuario.Focus();
                    break;
                default:
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Anulado";
                    this.txtUsuario.Focus();
                    break;
            }
            return false;
        }
        else
        {
            if (SFlagCambioClave == "1")
            {
                Session["TimeInactivo"] = TiempoInactivo();
                Session["Cod_Usuario"] = objUsuario.SCodUsuario;
                Response.Redirect("CambiarContrasena.aspx");
                return false;
            }

            if (objUsuario.SCodUsuario != "U000001")
            {
                if (validaVigenciaUsuario(objUsuario) == true)
                {
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado"; ;
                    this.txtUsuario.Focus();
                    return false;
                }

                if (validaVigenciaClave(objUsuario) == true)
                {
                    //Session["TimeInactivo"] = TiempoInactivo();
                    //Session["Cod_Usuario"] = objUsuario.SCodUsuario;
                    //Response.Redirect("CambiarContrasena.aspx");
                    //return false;
                    this.lblMensajeError.Text = "El Usuario se encuentra en situacion Bloqueado. Su contraseña a caducado, por favor cambie de contraseña"; ;
                    this.txtUsuario.Focus();
                    return false;
                }
            }

        }
        return true;
    }

    protected bool validaVigenciaUsuario(Usuario objUsuario)
    {
        DateTime dtFecha = objUsuario.DtFchVigencia;
        string sEstado = objUsuario.STipoEstadoActual;


        DataTable dtFechaHoy = new DataTable();

        dtFechaHoy = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFechaHoy.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);

        if (hoy > dtFecha)
        {
            if (sEstado == "V")
            {
                if (objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, "0") == true)
                {
                    ////GeneraAlarma
                    //string IpClient = Request.UserHostAddress;
                    //GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000001", "001", IpClient, "1", "Alerta W0000001", "Cuenta Bloqueada, Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                    return true;
                }
            }
        }
        return false;
    }

    
    protected bool validaVigenciaClave(Usuario objUsuario)
    {
        DateTime dtFecha = objUsuario.DtFchVigencia;
        string sEstado = objUsuario.STipoEstadoActual;


        DataTable dtFechaHoy = new DataTable();

        dtFechaHoy = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFechaHoy.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);


        ClaveUsuHist objClaveUsuHist = new ClaveUsuHist();
        objClaveUsuHist = objBOSeguridad.obtenerUsuarioHist(objUsuario.SCodUsuario);

        if (objClaveUsuHist != null)
        {
            string sFechaClave = Fecha.convertSQLToFecha(objClaveUsuHist.SLogFechaMod, objClaveUsuHist.SLogHoraMod);

            DateTime dtFechaClave = Convert.ToDateTime(sFechaClave);

            int DiasVencimiento = 0;
            DataTable dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarParametros("VC");

            if (dt_parametro.Rows.Count > 0)
            {
                DiasVencimiento = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
            }

            DateTime diaV = dtFechaClave.AddDays(+DiasVencimiento);


            if (hoy >= diaV)
            {
                if (objUsuario.STipoEstadoActual == "V")
                {
                    if (objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, "1") == true)
                    {
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000004", "001", IpClient, "1", "Alerta W0000004", "Clave de Cuenta Caducada, Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                        return true;
                    }
                }
            }
        }
        return false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LabelConfig.LoadData();
            htLabels = LabelConfig.htLabels;

            this.txtUsuario.Focus();
            this.lblUsuario.Text = htLabels["login.lblUsuario"].ToString();
            this.lblPassword.Text = htLabels["login.lblPassword"].ToString();
            this.lkbCambiarContraseña.Text = htLabels["login.lkbCambiarContraseña"].ToString();
            this.lblEmpresaTuua.Text = htLabels["login.lblEmpresaTuua"].ToString();
            this.lblDerechoTuua.Text = htLabels["login.lblDerechoTuua"].ToString();
            this.lblBienvenida.Text = htLabels["login.lblBienvenida"].ToString();
            this.btnIngresar.Text = htLabels["login.btnIngresar"].ToString();
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
        if (!Page.IsPostBack)
        {
            this.lblNumIntentos.Text = "1";
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

    protected void lkbCambiarContraseña_Click(object sender, EventArgs e)
    {
        autenticar("CambiarContrasena.aspx", true);
    }

    /// <summary>
    /// Metodo que carga los parametros de impresion.
    /// </summary>
    protected void CargarParamImpresion()
    {
        BO_ParamImpresion objParamImp = new BO_ParamImpresion();
        Hashtable htParamImp = new Hashtable();
        XmlDocument xmlDoc = new XmlDocument();

        objParamImp.ObtenerParametrosImpresion(htParamImp, xmlDoc);

        // guardar en sesion la lista de parametros de configuracion y el documento xml
        Session["xmlDoc"] = xmlDoc;
        Session["htParamImp"] = htParamImp;
    }


    private void CargarParametros()
    {
        BO_Configuracion objBOConfigura = new BO_Configuracion();
        dt_parametro = objBOConfigura.ListarAllParametroGenerales(null);
        Hashtable htParametro = new Hashtable();
        for (int i = 0; i < dt_parametro.Rows.Count; i++)
        {
            htParametro.Add(dt_parametro.Rows[i].ItemArray.GetValue(0).ToString().Trim(),Convert.ToString(dt_parametro.Rows[i].ItemArray.GetValue(5)));
        }
        Session["htParametro"] = htParametro;
    }

    
    protected void obtenerTiempoVigenciaRestante(Usuario objUsuario)
    {
        
        Session["TimeLife"] = null;
        Session["TimeLifeHour"] = null;
        Session["TimeLifeMin"] = null;
        Session["TimeLifeSeg"] = null;

        DateTime dtFecha = objUsuario.DtFchVigencia;
        string sEstado = objUsuario.STipoEstadoActual;


        DataTable dtFechaHoy = new DataTable();

        dtFechaHoy = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFechaHoy.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);


        ClaveUsuHist objClaveUsuHist = new ClaveUsuHist();
        objClaveUsuHist = objBOSeguridad.obtenerUsuarioHist(objUsuario.SCodUsuario);

        if (objClaveUsuHist != null)
        {
            string sFechaClave = Fecha.convertSQLToFecha(objClaveUsuHist.SLogFechaMod, objClaveUsuHist.SLogHoraMod);

            DateTime dtFechaClave = Convert.ToDateTime(sFechaClave);

            int DiasVencimiento = 0;
            DataTable dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarParametros("VC");

            if (dt_parametro.Rows.Count > 0)
            {
                DiasVencimiento = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
            }

            DateTime diaV = dtFechaClave.AddDays(+DiasVencimiento);

            int UmbralAlertDias = 0;
            DataTable dt_parametro_vence = new DataTable();
            dt_parametro_vence = objBOConsultas.ListarParametros("AE");

            if (dt_parametro.Rows.Count > 0)
            {
                UmbralAlertDias = Convert.ToInt32(dt_parametro_vence.Rows[0].ItemArray.GetValue(4).ToString());
            }

            if (hoy <= diaV)
            {

                // Diferencia de Dias, Horas, Minutos y Segundos.
                TimeSpan tsDiff = diaV - hoy;
                // Diferencia de Dias
                int differenceInDays = tsDiff.Days;
                

                if (differenceInDays >= 0)
                {
                    if (UmbralAlertDias >= differenceInDays)
                    {
                        Session["TimeLife"] = differenceInDays;
                        Session["TimeLifeHour"] = tsDiff.Hours;
                        Session["TimeLifeMin"] = tsDiff.Minutes;
                        Session["TimeLifeSeg"] = tsDiff.Seconds;
                    }
                }
            }
        }        
    }

    private void CargarListaCampoPasajero()
    {
        DataTable dtCampos = objBOConsultas.ListaCamposxNombre("TipoPasajero");
        Hashtable htCampos = new Hashtable();
        for (int i = 0; i < dtCampos.Rows.Count; i++)
        {
            htCampos.Add(dtCampos.Rows[i].ItemArray.GetValue(0).ToString() + dtCampos.Rows[i].ItemArray.GetValue(1).ToString(), dtCampos.Rows[i].ItemArray.GetValue(3).ToString());
        }
        Session["Lista_Campo_Psjero"] = htCampos;
    }

    protected bool verificaVigenciaUsuario(Usuario objUsuario)
    {
        DateTime dtFecha = objUsuario.DtFchVigencia;
        string sEstado = objUsuario.STipoEstadoActual;


        DataTable dtFechaHoy = new DataTable();

        dtFechaHoy = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFechaHoy.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);

        if (hoy > dtFecha)
        {
            return false; //vencido fecha vigencia
            /*if (sEstado == "V")
            {
                if (objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, "0") == true)
                {
                    ////GeneraAlarma
                    //string IpClient = Request.UserHostAddress;
                    //GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000001", "001", IpClient, "1", "Alerta W0000001", "Cuenta Bloqueada, Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                    return true;
                }
            }*/
        }
        return true; //cuenta aun vigente
    }


    protected bool verificaVigenciaClave(Usuario objUsuario)
    {
        DateTime dtFecha = objUsuario.DtFchVigencia;
        string sEstado = objUsuario.STipoEstadoActual;


        DataTable dtFechaHoy = new DataTable();

        dtFechaHoy = objBOSeguridad.obtenerFecha();

        string[] sFechaHora = new string[2];

        if (dtFechaHoy.Rows.Count > 0)
        {
            sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
        }

        string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

        DateTime hoy = Convert.ToDateTime(sFecha);


        ClaveUsuHist objClaveUsuHist = new ClaveUsuHist();
        objClaveUsuHist = objBOSeguridad.obtenerUsuarioHist(objUsuario.SCodUsuario);

        if (objClaveUsuHist != null)
        {
            string sFechaClave = Fecha.convertSQLToFecha(objClaveUsuHist.SLogFechaMod, objClaveUsuHist.SLogHoraMod);

            DateTime dtFechaClave = Convert.ToDateTime(sFechaClave);

            int DiasVencimiento = 0;
            DataTable dt_parametro = new DataTable();
            dt_parametro = objBOConsultas.ListarParametros("VC");

            if (dt_parametro.Rows.Count > 0)
            {
                DiasVencimiento = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
            }

            DateTime diaV = dtFechaClave.AddDays(+DiasVencimiento);


            if (hoy >= diaV)
            {
                return false; //contrasenia vencida
                /*if (objUsuario.STipoEstadoActual == "V")
                {
                    if (objBOSeguridad.actualizarEstadoUsuario(objUsuario.SCodUsuario, "B", objUsuario.SCodUsuario, "1") == true)
                    {
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000004", "001", IpClient, "1", "Alerta W0000004", "Clave de Cuenta Caducada, Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                        return true;
                    }
                }*/
            }
        }
        return true; //contrasenia vigente
    }

}
