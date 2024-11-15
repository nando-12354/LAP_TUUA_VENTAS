using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Reflection;

using Hiper.Net.Utilidades.Archivos;
using System.Net.Mail;
using LAP.TUUA.CONTROL;
using System.Data;
using LAP.TUUA.UTIL;


namespace SrvEnvioCorreo
{
    public class PerfilServicioCorreoBE
    {
        private DateTime? _dtHoraInicio;
        public DateTime? dtHoraInicio
        {
            get { return _dtHoraInicio; }
            set { _dtHoraInicio = value; }
        }

        private DateTime? _dtHoraFin;
        public DateTime? dtHoraFin
        {
            get { return _dtHoraFin; }
            set { _dtHoraFin = value; }
        }

        private int? _iFrecuencia;
        public int? iFrecuencia
        {
            get { return _iFrecuencia; }
            set { _iFrecuencia = value; }
        }
    }
    /// <summary>
    /// Clase del Modelo Inteligente de Asignación de Perfiles de Atención
    /// </summary>
    /// <Version>4.2.6.0</Version>
    /// <Autor>James Jayo</Autor>
    /// <Copyright>Copyright ( Copyright © HIPER S.A. )</Copyright>
    public class GestorCorreoBL
    {
        LogFile oLog; //Instancia de la clase utilitaria para escribir logs
        BO_Alarmas oBO_Alarmas;

        #region "Variables"
        private bool bEjecutarGestorCorreo; //Flag para indicar a los hilos si deben seguir ejecutándose
        private Thread hiloGestorCorreo; //Hilo principal del modelo inteligente
        private PerfilServicioCorreoBE oPerfilServicioCorreoBE_old = new PerfilServicioCorreoBE(); //Conservará el perfil de servicio anterior para compararlo con el perfil de servicio nuevo y notificar si hubieron cambios
        private PerfilServicioCorreoBE oPerfilServicioCorreoBE_new; //Almacenará el perfil de servicio actual

        public static Object oLock = new Object();
        #endregion

        #region "Constructor"
        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        public GestorCorreoBL()
        {
            oLog = new LogFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log", "SrvEnvioCorreo");
            
            string strPath = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "resources/");
            
            if (!ErrorHandler.CargarErrorTypes(strPath))
            {
                oLog.WriteLine("Err(Typ): " + ErrorHandler.Desc_Mensaje);
            }

            if (!Property.CargarPropiedades(strPath))
            {
                oLog.WriteLine("Err(Prp): " + ErrorHandler.Desc_Mensaje);
            }
            
            Property.htProperty.Add("PATHRECURSOS", System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            
            oBO_Alarmas = new BO_Alarmas();
            
        }
        #endregion

        /// <summary>
        /// Metodo en el cual se inicia la ejecución de los hilos del Servicio.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        public bool ejecutarGestorCorreo()
        {
            try
            {
                bEjecutarGestorCorreo = true;

                oLog.WriteLine("Se iniciará el hilo principal del 'Servicio de envío de correo'");
                hiloGestorCorreo = new Thread(new ThreadStart(ejecutarProcesoGestorCorreo));                    
                hiloGestorCorreo.Start();
                oLog.WriteLine("Se inició el hilo principal del 'Servicio de envío de correo'");                
            }
            catch (Exception e)
            {
                oLog.WriteLine("Error al iniciar la ejecución del Servicio de Asignación de Perfiles de Atención Inteligente: " + e.Message);
                return false;
            }
            return true;
        }

        private PerfilServicioCorreoBE obtenerPerfilServicio()
        {
            PerfilServicioCorreoBE oPerfilServicioCorreoBE = new PerfilServicioCorreoBE();
             
            try
            {
                string hIni = ConfigurationManager.AppSettings["HoraInicio"].ToString();

                if (string.IsNullOrEmpty(hIni))
                {
                    oPerfilServicioCorreoBE.dtHoraInicio = null;
                }
                else
                {
                    //string hInicio = hIni.Substring(0, 2) + hIni.Substring(3, 2) + hIni.Substring(6, 2);
                    oPerfilServicioCorreoBE.dtHoraInicio = DateTime.ParseExact(hIni, "HHmmss", null);
                }
            }
            catch (Exception ex)
            {
                oPerfilServicioCorreoBE.dtHoraInicio = null;
                oLog.WriteLine("Excepcion(HoraInicio): " + ex.Message);
            }

            try
            {
                string hFin = ConfigurationManager.AppSettings["HoraFin"].ToString();

                if (string.IsNullOrEmpty(hFin))
                {
                    oPerfilServicioCorreoBE.dtHoraFin = null;
                }
                else
                {
                    //string hInicio = hFin.Substring(0, 2) + hFin.Substring(3, 2) + hFin.Substring(6, 2);
                    oPerfilServicioCorreoBE.dtHoraFin = DateTime.ParseExact(hFin, "HHmmss", null);
                }
            }
            catch (Exception ex)
            {
                oPerfilServicioCorreoBE.dtHoraFin = null;
                oLog.WriteLine("Excepcion(HoraFin): " + ex.Message);
            }

            try
            {
                string sFrecuencia = ConfigurationManager.AppSettings["Frecuencia"].ToString();

                if (string.IsNullOrEmpty(sFrecuencia))
                {
                    oPerfilServicioCorreoBE.iFrecuencia = null;
                }
                else
                {
                    oPerfilServicioCorreoBE.iFrecuencia = Convert.ToInt32(sFrecuencia);
                }
            }
            catch (Exception ex)
            {
                oPerfilServicioCorreoBE.iFrecuencia = null;
                oLog.WriteLine("Excepcion(Frecuencia): " + ex.Message);
            }

            return oPerfilServicioCorreoBE;
        }

        /// <summary>
        /// Valida la correcta configuración del Perfil de Servicio.
        /// </summary>
        /// <param name="strCodAgencia">Código de agencia.</param>
        /// <returns>Retorna True en caso estén correctas las configuraciones y False en caso contrario.</returns>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>14/08/2013</FechaCreacion> 
        private bool validarPerfilServicios()
        {
            bool Rpta = true;
            try
            {
                oPerfilServicioCorreoBE_new = obtenerPerfilServicio();
            }
            catch
            {
                oLog.WriteLine("Hubo una exception al tratar de obtener los parámetros del perfil de servicio.");
                return false;
            }
            if (oPerfilServicioCorreoBE_new == null)
            {
                oLog.WriteLine("Error al obtener los parámetros del perfil de servicio. No existe ningun perfil de servicio configurado.");
                return false;
            }
            if (oPerfilServicioCorreoBE_new.dtHoraInicio == null || oPerfilServicioCorreoBE_new.dtHoraFin == null ||
                oPerfilServicioCorreoBE_new.iFrecuencia == null)
            {
                oLog.WriteLine("Error: Existen valores null en los parámetros del perfil de servicio.");
                return false;
            }
            salvarPerfilServicio();

            return Rpta;
        }

        /// <summary>
        /// Salva en una variable auxiliar el perfil de servicio actual.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion> 
        private void salvarPerfilServicio()
        {
            if (oPerfilServicioCorreoBE_old.dtHoraInicio == null && oPerfilServicioCorreoBE_old.dtHoraFin == null &&
                oPerfilServicioCorreoBE_old.iFrecuencia == null)
            {
                oPerfilServicioCorreoBE_old.dtHoraInicio = oPerfilServicioCorreoBE_new.dtHoraInicio;
                oPerfilServicioCorreoBE_old.dtHoraFin = oPerfilServicioCorreoBE_new.dtHoraFin;
                oPerfilServicioCorreoBE_old.iFrecuencia = oPerfilServicioCorreoBE_new.iFrecuencia;
            }
        }

        /// <summary>
        /// Metodo en el cual se ejecuta el hilo principal del gestor de correo.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion> 
        public void ejecutarProcesoGestorCorreo()
        {
            bool bEsHoraDeEjecucion = true;

            while (this.bEjecutarGestorCorreo)
            {
                try
                {
                    
                    if (validarPerfilServicios())
                    {
                        actualizarParametrosPerfilServicio();

                        if (DateTime.Now.TimeOfDay.CompareTo(oPerfilServicioCorreoBE_new.dtHoraInicio.Value.TimeOfDay) >= 0 && DateTime.Now.TimeOfDay.CompareTo(oPerfilServicioCorreoBE_new.dtHoraFin.Value.TimeOfDay) <= 0)
                        {
                            try
                            {
                                if (!bEsHoraDeEjecucion)
                                {
                                    oLog.WriteLine("------------------------------------------------------------------------------------------------------------>\n" +
                                     "              Se ejecutará el 'Servicio de envío de correo' pues entró en horario configurado.\n" +
                                     "              <-----------------------------------------------------------------------------------------------------------");
                                }
                                bEsHoraDeEjecucion = true;

                                {
                                    ProcesarEnvioCorreo();
                                }
                            }
                            catch (Exception e1)
                            {
                                oLog.WriteLine("Exception(ejecutarProcesoGestorCorreo): " + e1.Message);
                            }
                        }
                        else
                        {
                            if (bEsHoraDeEjecucion)
                            {
                                bEsHoraDeEjecucion = false;

                                oLog.WriteLine("----------------------------------------------------------------------------------------------------------------------->\n" +
                                 "              No se ejecutará el 'Servicio de envío de correo' por estar fuera del horario configurado.\n" +
                                 "              <----------------------------------------------------------------------------------------------------------------------");
                            }
                        }
                    }
                }
                catch(Exception e2)
                {
                    oLog.WriteLine("Exception(ejecutarProcesoGestorCorreo): " + e2.Message);
                }

                #region "Frecuencia"
                int iMultiploFrecuencia = 1000;
                int iContador = 1;
                int iFrecuencia = 0;
                if (oPerfilServicioCorreoBE_new.iFrecuencia.HasValue)
                {
                    iFrecuencia = oPerfilServicioCorreoBE_new.iFrecuencia.Value;
                }
                else
                {
                    iFrecuencia = 90;
                    oLog.WriteLine("Se usará por defecto una frecuencia de 90 segundos.");
                }

                while (bEjecutarGestorCorreo && (iMultiploFrecuencia * iContador <= iFrecuencia * 1000))
                {
                    System.Threading.Thread.Sleep(iMultiploFrecuencia);
                    iContador++;
                }
                #endregion
            }
            oLog.WriteLine("Se salió del bucle del hilo del 'Servicio de envío de correo'");
        }

        private void ProcesarEnvioCorreo()
        {
            oLog.WriteLine("Verificando pendientes de envio de correo...");
            
            DataTable dt = oBO_Alarmas.obtenerAlarmasGeneradasSinEnviar();

            foreach (DataRow dr in dt.Rows)
            {
                oLog.WriteLine("Enviando correo de la alarma: " + dr["Cod_AlarmaGenerada"].ToString().Trim());
                if (oBO_Alarmas.EnviarCorreo(dr["Cod_AlarmaGenerada"].ToString().Trim()))
                {
                    oLog.WriteLine("Correo enviado.");
                }
                else
                {
                    oLog.WriteLine("Correo no enviado... se reintentará dentro de poco.");
                }

            }
        }

        /*private void ProcesarEnvioCorreo()
        {
            DataTable dt = oBO_Alarmas.ListarAllAlarmaGenerada();
            string sUsuario = string.Empty; //"CUENTA_PRUEBA";
            string sCorreo = string.Empty; //txtNom.Text;
            string sPass = string.Empty; //txtPasw.Text;
            string sSubject = string.Empty; //txtAsunto.Text;
            string sBody = string.Empty; //txtMsj.Text;
            string[] sTo = null; //txtDestino.Text; //emailTo;
            string sFrom = string.Empty; //txtNom.Text;
            string sSMTPServer = string.Empty; //txtDom.Text;
            int sPort = 0; //Convert.ToInt32(txtPort.Text);
            bool bSeguridadSSL = false; //chkSSL.Checked;

            SendMail(sUsuario, sCorreo, sPass, sSubject, sBody, sTo, sFrom, sSMTPServer, sPort, bSeguridadSSL);
        }

        private void SendMail(string sUsuario, string sCorreo,
                            string sPass, string sSubject,
                            string sBody, string[] sTo,
                            string sFrom, string sSMTPServer, int iPort, bool bSSL)
        {

            try
            {
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(sFrom, sUsuario);
                foreach (string mail in sTo)
                {
                    correo.To.Add(mail);
                    //correo.To.Add(sTo);
                }
                correo.Subject = sSubject;
                correo.Body = sBody;
                correo.IsBodyHtml = false;
                correo.Priority = System.Net.Mail.MailPriority.Normal;
                System.Net.Mail.SmtpClient Smtp = new System.Net.Mail.SmtpClient();
                Smtp.Host = sSMTPServer;

                Smtp.Port = iPort;

                Smtp.UseDefaultCredentials = false;
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(sCorreo, sPass);
                Smtp.Credentials = basicAuthenticationInfo;

                Smtp.EnableSsl = bSSL;

                Smtp.Send(correo);

                oLog.WriteLine("Envio correo satisfactoriamente.");


                //return "1";


            }
            catch (Exception e)
            {
                //return e.InnerException.ToString();
                oLog.WriteLine("Error al enviar correo:" + e.Message);
                //return e.InnerException.ToString();
            }

        }*/

        /// <summary>
        /// Actualiza los parámetros de configuración del perfil de servicio en los asignadores de perfiles.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>14/08/2013</FechaCreacion>
        private void actualizarParametrosPerfilServicio()
        {
            if (verificarCambiosPerfilServicio()) 
            {
                oLog = new LogFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log", "SrvEnvioCorreo");
                
                oPerfilServicioCorreoBE_old.dtHoraInicio = oPerfilServicioCorreoBE_new.dtHoraInicio;
                oPerfilServicioCorreoBE_old.dtHoraFin = oPerfilServicioCorreoBE_new.dtHoraFin;
                oPerfilServicioCorreoBE_old.iFrecuencia = oPerfilServicioCorreoBE_new.iFrecuencia;                                            
            }           
        }

        /// <summary>
        /// Verifica si hubo cambios en la configuración del perfil de servicio.
        /// </summary>
        /// <returns>Retorna True en caso hubieran cambios y False en caso contrario</returns>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion> 
        private bool verificarCambiosPerfilServicio()
        {
            bool Rpta = false;
            if (oPerfilServicioCorreoBE_new.dtHoraInicio != oPerfilServicioCorreoBE_old.dtHoraInicio ||
                oPerfilServicioCorreoBE_new.dtHoraFin != oPerfilServicioCorreoBE_old.dtHoraFin ||
                oPerfilServicioCorreoBE_new.iFrecuencia != oPerfilServicioCorreoBE_old.iFrecuencia)
            {
                Rpta = true;
                oLog.WriteLine("Hubo cambios en la configuración del Perfil Servicio:");
                oLog.WriteLine("Perfil Servicio anterior: Hora Inicio: " + oPerfilServicioCorreoBE_old.dtHoraInicio.Value.ToString("HH:mm:ss"));
                oLog.WriteLine("Perfil Servicio anterior: Hora Fin: " + oPerfilServicioCorreoBE_old.dtHoraFin.Value.ToString("HH:mm:ss"));
                oLog.WriteLine("Perfil Servicio anterior: Frecuencia: " + oPerfilServicioCorreoBE_old.iFrecuencia.Value.ToString());

                oLog.WriteLine("Perfil Servicio actual: Hora Inicio: " + oPerfilServicioCorreoBE_new.dtHoraInicio.Value.ToString("HH:mm:ss"));
                oLog.WriteLine("Perfil Servicio actual: Hora Fin: " + oPerfilServicioCorreoBE_new.dtHoraFin.Value.ToString("HH:mm:ss"));
                oLog.WriteLine("Perfil Servicio actual: Frecuencia: " + oPerfilServicioCorreoBE_new.iFrecuencia.Value.ToString());
            }
            return Rpta;
        }

        /// <summary>
        /// Manda a detener el hilo principal.
        /// </summary>
        /// <returns>Retorna True en caso se hayan detenido satisfactoriamente; False en caso contrario.</returns>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        public bool detenerGestorCorreo()
        {
            bool Rpta = true;
            try
            {
                try
                {
                    bEjecutarGestorCorreo = false;

                    hiloGestorCorreo.Join(10 * 1000);
                    hiloGestorCorreo.Abort();
                }
                catch (Exception e1)
                {
                    Rpta = false;
                    oLog.WriteLine("Info: Detiendo hilo del 'Servicio de envío de correo': " + e1.Message);
                }                
            }
            catch (Exception e)
            {
                Rpta = false;
            }
            return Rpta;
        }

    }
}
