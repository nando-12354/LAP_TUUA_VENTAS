///V.1.4.6.0
///Enrique Montes Balbuena
///Copyright ( Copyright © HIPER S.A. )
///

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Timers;

namespace LAP.TUUA.SINCRONIZADOR
{
    public partial class SVC_Sincronizer : ServiceBase
    {
        private string IP_SERVIDOR = "";
        private string DBName = "";
        private string Dsc_Ip = "";
        private int Inter_reg = 0;
        private string Dsc_Path = "";
        private int Modo_SIN = 0;

        public SVC_Sincronizer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Descripcion : Captura la IP del Servidor Local
        /// </summary>
        /// <returns>string</returns>
        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }

        /// <summary>
        /// Descripcion : Procedimiento de Inicio del Servicio
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {

            bool boOkTime = false;
            try
            {
                string Dsc_Path = AppDomain.CurrentDomain.BaseDirectory + "resources/";
                
                if (!Property.CargarPropiedades(Dsc_Path))
                {
                    return;
                }

                Modo_SIN = Int32.Parse((string)Property.htProperty[Define.MODO_SINCRONISMO]);
                int intTime_Timeout_CL = Int32.Parse((string)Property.htProperty[Define.TIMEOUT_CL]);
                int intTime_Timeout_LC = Int32.Parse((string)Property.htProperty[Define.TIMEOUT_LC]);
                boOkTime = true;

                Timer tmService_CL = new Timer();
                Timer tmService_LC = new Timer();

                if ((string)Property.htProperty[Define.TIPOSERVICIO] == Define.CENTER_LOCAL_SERVICE)
                {
                    tmService_CL.Elapsed += new ElapsedEventHandler(ExecuteCenterToLocalAtributoService);
                    tmService_CL.Interval = intTime_Timeout_CL;
                    tmService_CL.Start();
                    Property.Trace("SERVICO CENTRAL A LOCAL", "INICIADO.");

                }
                else if ((string)Property.htProperty[Define.TIPOSERVICIO] == Define.LOCAL_CENTER_SERVICE)
                {
                    Inter_reg = Int32.Parse((string)Property.htProperty[Define.INTERVALO_TRANSACCION]);
                    IP_SERVIDOR = (string)Property.htProperty[Define.IP_SERVIDOR];
                    DBName = (string)Property.htProperty[Define.DBNAME];
                    Dsc_Ip = LocalIPAddress();
                    tmService_LC.Elapsed += new ElapsedEventHandler(ExecuteLocalToCenterTicketService);

                    tmService_LC.Interval = intTime_Timeout_LC;
                    tmService_LC.Start();
                    Property.Trace("SERVICO LOCAL A CENTRAL", "INICIADO.");
                }
                else
                {
                    Property.Trace("Error en configuracion Archivo INI", "TIPO SERVICIO INVALIDO.");
                    return;
                }

            }
            catch (Exception ex)
            {
                if (!boOkTime)
                {
                    Property.Trace("Error en configuracion Archivo INI", "TIMEOUT INVALIDO.");
                }
                else
                {
                    ErrorHandler.Trace("ERROR", ex);
                }
            }
        }

        protected override void OnStop()
        {
        }


        public void DetenerServicio()
        {
            ServiceController servicioWindows = new ServiceController("TUUASincro");
            //int timeoutMilliseconds = 5000;
            try
            {
                //TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                if (servicioWindows != null && servicioWindows.Status == ServiceControllerStatus.Running)
                {
                    servicioWindows.Stop();
                }
                //servicioWindows.WaitForStatus(ServiceControllerStatus.Running, timeout);
                Property.Trace("Servicio Sincronismo detenido correctamente ...", "SERVICIO DETENIDO.");
                servicioWindows.WaitForStatus(ServiceControllerStatus.Stopped); 
                servicioWindows.Close();
                //MessageBox.Show("Servicio detenido correctamente, Verificar Modo de Sincronismo incorrecto ...");
            }
            catch (Exception ex)
            {
                Property.Trace("Error al detener el servicio Sincronismo ...", "ERROR SERVICIO DETENIDO.");
                ErrorHandler.Trace("ERROR", ex);
                //MessageBox.Show("Error al detener el servicio: " + ex.Message);
            } 

        }

        /// <summary>
        /// Descripcion : Sincronizacion de Ticket's y Boarding Pass de LOCAL a CENTRAL
        /// </summary>
        public void ExecuteLocalToCenterTicketService(object source, ElapsedEventArgs e)
        {
            bool isBloqueo = true;

            if (isBloqueo)
            {
                isBloqueo = false;
                Dsc_Path = AppDomain.CurrentDomain.BaseDirectory;
                Property.Trace("SERVICIO INICIADO", "LOCAL-CENTRAL-TICKET-BOARDING: ");

                try
                {
                    string strMessage = "";
                    string strInformation = "";

                    DateTime inicio = DateTime.Now;

                    DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                    //if (DBName != "")
                    //{
                        Property.Trace("PROCESANDO..", "LOCAL-CENTRAL-TICKET-BOARDING");
                        Property.Trace("IP DE MOLINETE A PROCESAR..", Dsc_Ip);
                        objDAOSincroniza.SincronizarLocalToCentralTicket(IP_SERVIDOR, DBName, Dsc_Ip, Inter_reg, Modo_SIN, ref strMessage, ref strInformation);
                        if (strMessage.Trim() != "")
                        {
                            Property.Trace("ERROR-LOCAL-CENTRAL-TICKET-BOARDING", strMessage);
                            int? num = Inter_reg;
                            if (num.HasValue == false)
                            {
                                string[] modo = { "1", "2" };
                                foreach (string mode1 in modo)
                                {
                                    if (!Inter_reg.ToString().Equals(mode1))
                                        DetenerServicio();
                                }   
                            
                            }

                            //if (!Inter_reg.ToString().Equals(null) )
                            //{
                            //    if (!Inter_reg.Equals(1) || !Inter_reg.Equals(2) )
                            //    {
                            //        DetenerServicio();
                            //    }
                            //}
                        }
                        else
                        {
                            //TimeSpan duracion_clt = DateTime.Now - inicio;
                            //double segundos_clt = duracion_clt.TotalSeconds;
                            PintarInformacion("LOCAL-CENTRAL-TICKET-BOARDING", strInformation);
                            //Property.Trace("LOCAL-CENTRAL-TICKET-BOARDING FINALIZADO", " Duracion : " + segundos_clt.ToString());

                        }
                    //}
                    TimeSpan duracion = DateTime.Now - inicio;
                    double segundosTotales = duracion.TotalSeconds;
                    Property.Trace("SERVICIO FINALIZADO", "LOCAL-CENTRAL-TICKET-BOARDING" + " Duracion Total: " + segundosTotales.ToString());
                }
                catch (Exception ex)
                {
                    ErrorHandler.Trace("ERROR-LOCAL-CENTRAL-TICKET-BOARDING", ex);
                }
                finally
                {
                    isBloqueo = true;
                }
            }
        }

        /// <summary>
        /// Descripcion : Sincronizacion de Tablas Maestras de Central a Local
        /// </summary>
        public void ExecuteCenterToLocalAtributoService(object source, ElapsedEventArgs e)
        {
            bool isBloqueo = true;

            if (isBloqueo)
            {
                isBloqueo = false;
                Dsc_Path = AppDomain.CurrentDomain.BaseDirectory;
                Property.Trace("SERVICIO INICIADO", "CENTRAL-LOCAL-TABLAS");

                try
                {
                    string strMessage = "";
                    string strInformation = "";
                    DateTime inicio = DateTime.Now;

                    DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                    Property.Trace("PROCESANDO..", "CENTRAL-LOCAL-TABLAS");
                    objDAOSincroniza.SincronizarCentralToLocalAtr(ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-CENTRAL-LOCAL-TABLAS", strMessage);
                    }
                    else
                    {
                        PintarInformacion("CENTRAL-LOCAL-TABLAS", strInformation);
                    }
                    DateTime final = DateTime.Now;
                    TimeSpan duracion = final - inicio;
                    double segundosTotales = duracion.TotalSeconds;
                    Property.Trace("SERVICIO FINALIZADO", "CENTRAL-LOCAL-TABLAS" + " Duracion Total: " + segundosTotales.ToString());
                }
                catch (Exception ex)
                {
                    ErrorHandler.Trace("ERROR-CENTRAL-LOCAL-TABLAS", ex);
                }
                finally
                {
                    isBloqueo = true;
                }
            }
        }

        /// <summary>
        /// Descripcion : Muestras los mensajes en el archivo LOG
        /// </summary>
        public void PintarInformacion(string strTitulo,string strInformation)
        {
            char[] sep = { '#' };
            string[] arrInfo = strInformation.Split(sep);
            string strInfo = "\n";
            for (int i = 0; i < arrInfo.Length; i++)
            {
                strInfo += i < arrInfo.Length - 1 ? arrInfo[i] + "\n" : arrInfo[i];
            }
            Property.Trace(strTitulo, strInfo);
        }

    }
}
