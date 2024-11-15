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

namespace LAP.TUUA.DEPURACION
{
    public partial class SVC_Depuracion : ServiceBase
    {
        private string IP_SERVIDOR = "";
        private string DBName = "";
        private string Dsc_Ip = "";
        private int numero = 0;
        //private int numero = 0;
        private string Dsc_Path = "";
     


        public SVC_Depuracion()
        {
            InitializeComponent();
        }

       #region Ips

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

#endregion

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

                IP_SERVIDOR = (string)Property.htProperty[Define.IP_SERVIDOR];
                DBName = (string)Property.htProperty[Define.DBNAME];
                numero = Int32.Parse((string)Property.htProperty[Define.numero]);
                Dsc_Ip = LocalIPAddress();

                boOkTime = true;
                 //TIMEOUT
                int intTime_TIMEOUT = Int32.Parse((string)Property.htProperty[Define.TIMEOUT]);           
            
                Timer tmService = new Timer();                
            
               
              
                tmService.Elapsed += new ElapsedEventHandler(ExecuteLocalToCenterTicketService);
                tmService.Interval = intTime_TIMEOUT;
                tmService.Start();
                Property.Trace("SERVICIO LOCAL", "INICIADO.");

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

        // Se Modifica funcion para sincronizacion Ticket's de LOCAL a CENTRAL Solamente

        public void ExecuteLocalToCenterTicketService(object source, ElapsedEventArgs e)
        {
            bool isBloqueo = true;

            if (isBloqueo)
            {
                isBloqueo = false;
                Dsc_Path = AppDomain.CurrentDomain.BaseDirectory;
                Property.Trace("SERVICIO INICIADO", "DEPURACIÓN: ");
              
                
                try
                {
                    string strMessage = "";
                    string strInformation = "";

                    DateTime inicio = DateTime.Now;

                    DAO_Depuracion objDAODepuracion = new DAO_Depuracion(Dsc_Path);

                    if (IP_SERVIDOR != "")
                    {
                        Property.Trace("PROCESANDO..", "DEPURACIÓN");
                        Property.Trace("IP A PROCESAR..", Dsc_Ip);
                        
                        objDAODepuracion.DepuracionLocal(IP_SERVIDOR, Dsc_Ip, DBName,numero, ref strMessage, ref strInformation);

                        if (strMessage.Trim() != "")
                        {
                            Property.Trace("SERVICIO DEPURACIÓN", strMessage);
                        }
                        else
                        {
                            TimeSpan duracion_clt = DateTime.Now - inicio;
                            double segundos_clt = duracion_clt.TotalSeconds;

                            PintarInformacion("SERVICIO DEPURACIÓN", strInformation);
                            Property.Trace("DEPURACIÓN FINALIZADO", " Duracion : " + segundos_clt.ToString());


                        }
                    }
                    TimeSpan duracion = DateTime.Now - inicio;
                    double segundosTotales = duracion.TotalSeconds;
                    Property.Trace("SERVICIO FINALIZADO", "LOCAL" + " Duracion Total: " + segundosTotales.ToString());
                }
                catch (Exception ex)
                {
                    ErrorHandler.Trace("ERROR-SERVICIO DEPURACIÓN", ex);
                }
                finally
                {
                    isBloqueo = true;
                }
            }
        }
           

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
