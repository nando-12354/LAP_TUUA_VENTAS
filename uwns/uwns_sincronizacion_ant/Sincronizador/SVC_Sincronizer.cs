using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private string Cod_Master = "";
        private string Lista_Host = "";
        private int Can_Slaves = 0;
        private string Dsc_Path = "";

        public SVC_Sincronizer()
        {
            InitializeComponent();
        }

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
                int intTime_Atributos = Int32.Parse((string)Property.htProperty[Define.TIMEOUT_ATRIBUTOS]);
                int intTime_Ticket = Int32.Parse((string)Property.htProperty[Define.TIMEOUT_TICKET]);
                int intTime_BP = Int32.Parse((string)Property.htProperty[Define.TIMEOUT_BP]);
                boOkTime = true;
                Timer tmServiceTicket = new Timer();
                Timer tmServiceBcbp = new Timer();
                Timer tmServiceAtr = new Timer();
                if ((string)Property.htProperty[Define.TIPOSERVICIO] == Define.CENTER_LOCAL_SERVICE)
                {
                    ObtenerMolinetes(true);
                    tmServiceAtr.Elapsed += new ElapsedEventHandler(ExecuteCenterToLocalAtributoService);
                    tmServiceTicket.Elapsed += new ElapsedEventHandler(ExecuteCenterToLocalTicketService);
                    tmServiceBcbp.Elapsed += new ElapsedEventHandler(ExecuteCenterToLocalBcbpService);
                }
                else if ((string)Property.htProperty[Define.TIPOSERVICIO] == Define.MASTER_SLAVES_SERVICE)
                {
                    ObtenerMolinetes(false);
                    tmServiceTicket.Elapsed += new ElapsedEventHandler(ExecuteMasterSlavesTicketService);
                    tmServiceBcbp.Elapsed += new ElapsedEventHandler(ExecuteMasterSlavesBcbpService);
                }
                else
                {
                    Property.Trace("Error en configuracion Archivo INI", "TIPO SERVICIO INVALIDO.");
                    return;
                }
                tmServiceAtr.Interval = 1000 * intTime_Atributos;
                tmServiceTicket.Interval = 1000 * intTime_Ticket;
                tmServiceBcbp.Interval = 1000 * intTime_BP;
                
                tmServiceAtr.Start();
                tmServiceTicket.Start();
                tmServiceBcbp.Start();
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

        public void ExecuteCenterToLocalTicketService(object source, ElapsedEventArgs e)
        {
            Property.Trace("SERVICIO INICIADO", "CENTRAL-LOCAL-TICKET");
            
            try
            {
                string strMessage = "";
                string strInformation = "";
                
                DateTime inicio = DateTime.Now;

                DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                if (Cod_Master != "")
                {
                    Property.Trace("PROCESANDO..", "CENTRAL-LOCAL-TICKET");
                    objDAOSincroniza.SincronizarCentralToLocalTicket(Cod_Master, Lista_Host, Can_Slaves, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-CENTRAL-LOCAL-TICKET", strMessage);
                    }
                    else
                    {
                        TimeSpan duracion_clt = DateTime.Now - inicio;
                        double segundos_clt = duracion_clt.TotalSeconds;

                        PintarInformacion("CENTRAL-LOCAL-TICKET", strInformation);
                        Property.Trace("CENTRAL-LOCAL-TICKET FINALIZADO", " Duracion : " + segundos_clt.ToString());
                        Property.Trace("PROCESANDO..", "LOCAL-CENTRAL-TICKET");

                        DateTime final_clt = DateTime.Now;
                        objDAOSincroniza.SincronizarLocalToCentralTicket(Cod_Master,ref strMessage, ref strInformation);
                        if (strMessage.Trim() != "")
                        {
                            Property.Trace("ERROR-LOCAL-CENTRAL-TICKET", strMessage);
                        }
                        else
                        {
                            TimeSpan duracion_lct = DateTime.Now - final_clt;
                            double segundos_lct = duracion_lct.TotalSeconds;

                            PintarInformacion("LOCAL-CENTRAL-TICKET", strInformation);

                            Property.Trace("LOCAL-CENTRAL-TICKET FINALIZADO", " Duracion : " + segundos_lct.ToString());
                        }
                    }
                }
                TimeSpan duracion = DateTime.Now - inicio;
                double segundosTotales = duracion.TotalSeconds;
                Property.Trace("SERVICIO FINALIZADO", "CENTRAL-LOCAL-TICKET" + " Duracion Total: " + segundosTotales.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace("ERROR-CENTRAL-LOCAL-TICKET", ex);
            }
        }

        public void ExecuteCenterToLocalAtributoService(object source, ElapsedEventArgs e)
        {
            Property.Trace("SERVICIO INICIADO", "CENTRAL-LOCAL-ATRIBUTOS");

            try
            {
                string strMessage = "";
                string strInformation = "";
                DateTime inicio = DateTime.Now;

                DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                if (Cod_Master != "")
                {
                    Property.Trace("PROCESANDO..", "CENTRAL-LOCAL-ATRIBUTOS");
                    objDAOSincroniza.SincronizarCentralToLocalAtr(Cod_Master, Lista_Host, Can_Slaves, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-CENTRAL-LOCAL-ATRIBUTOS", strMessage);
                    }
                    else
                    {
                        PintarInformacion("CENTRAL-LOCAL-ATRIBUTOS", strInformation);
                    }
                }
                DateTime final = DateTime.Now;
                TimeSpan duracion = final - inicio;
                double segundosTotales = duracion.TotalSeconds;
                Property.Trace("SERVICIO FINALIZADO", "CENTRAL-LOCAL-ATRIBUTOS" + " Duracion Total: " + segundosTotales.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace("ERROR-CENTRAL-LOCAL-ATRIBUTOS", ex);
            }
        }

        public void ExecuteCenterToLocalBcbpService(object source, ElapsedEventArgs e)
        {
            Property.Trace("SERVICIO INICIADO", "CENTRAL-LOCAL-BCBP");

            try
            {
                string strMessage = "";
                string strInformation = "";
                DateTime inicio = DateTime.Now;

                DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                if (Cod_Master != "")
                {
                    Property.Trace("PROCESANDO..", "CENTRAL-LOCAL-BCBP");
                    objDAOSincroniza.SincronizarCentralToLocalBcbp(Cod_Master, Lista_Host, Can_Slaves, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-CENTRAL-LOCAL-BCBP", strMessage);
                    }
                    else
                    {
                        TimeSpan duracion_clt = DateTime.Now - inicio;
                        double segundos_clt = duracion_clt.TotalSeconds;

                        PintarInformacion("CENTRAL-LOCAL-BCBP", strInformation);
                        Property.Trace("CENTRAL-LOCAL-BCBP FINALIZADO", " Duracion : " + segundos_clt.ToString());
                        Property.Trace("PROCESANDO..", "LOCAL-CENTRAL-BCBP");
                        DateTime final_clt = DateTime.Now;
                        objDAOSincroniza.SincronizarLocalToCentralBcbp(Cod_Master, ref strMessage, ref strInformation);
                        if (strMessage.Trim() != "")
                        {
                            Property.Trace("ERROR-LOCAL-CENTRAL-BCBP", strMessage);
                        }
                        else
                        {
                            TimeSpan duracion_lct = DateTime.Now - final_clt;
                            double segundos_lct = duracion_lct.TotalSeconds;

                            PintarInformacion("LOCAL-CENTRAL-BCBP", strInformation);

                            Property.Trace("LOCAL-CENTRAL-BCBP FINALIZADO", " Duracion : " + segundos_lct.ToString());
                        }
                    }
                }
                TimeSpan duracion = DateTime.Now - inicio;
                double segundosTotales = duracion.TotalSeconds;
                Property.Trace("SERVICIO FINALIZADO", "CENTRAL-LOCAL-BCBP" + " Duracion Total: " + segundosTotales.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace("ERROR-CENTRAL-LOCAL-BCBP", ex);
            }
        }

        public void ExecuteMasterSlavesTicketService(object source, ElapsedEventArgs e)
        {
            Property.Trace("SERVICIO INICIADO", "MASTER-SLAVES-TICKET");

            try
            {
                string strMessage = "";
                string strInformation = "";
                string strFechaSincro = "";
                string strUltimaVez = "";

                DateTime inicio = DateTime.Now;

                DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                if (Cod_Master != "")
                {
                    Property.Trace("PROCESANDO..", "SLAVES-MASTER-TICKET");
                    objDAOSincroniza.SincronizarSlavesToMasterTicket(Cod_Master, Lista_Host, Can_Slaves, ref strFechaSincro, ref strUltimaVez, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-SLAVES-MASTER-TICKET", strMessage);
                        return;
                    }
                    TimeSpan duracion_clt = DateTime.Now - inicio;
                    double segundos_clt = duracion_clt.TotalSeconds;

                    PintarInformacion("SLAVES-MASTER-TICKET", strInformation);
                    Property.Trace("SLAVES-MASTER-TICKET FINALIZADO", " Duracion : " + segundos_clt.ToString());
                        
                    Property.Trace("PROCESANDO..", "MASTER-SLAVES-TICKET");
                    DateTime final_clt = DateTime.Now;
                    objDAOSincroniza.SincronizarMasterToSlavesTicket(Cod_Master, Lista_Host, Can_Slaves, strFechaSincro, strUltimaVez, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-MASTER-SLAVES-TICKET", strMessage);
                        return;
                    }
                    TimeSpan duracion_lct = DateTime.Now - final_clt;
                    double segundos_lct = duracion_lct.TotalSeconds;
                    PintarInformacion("MASTER-SLAVES-TICKET", strInformation);
                    Property.Trace("MASTER-SLAVES-TICKET FINALIZADO", " Duracion : " + segundos_lct.ToString());
                }
                TimeSpan duracion = DateTime.Now - inicio;
                double segundosTotales = duracion.TotalSeconds;
                Property.Trace("SERVICIO FINALIZADO", "MASTER-SLAVES-TICKET" + " Duracion Total: " + segundosTotales.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace("ERROR-MASTER-SLAVES-TICKET", ex);
            }
        }

        public void ExecuteMasterSlavesBcbpService(object source, ElapsedEventArgs e)
        {
            Property.Trace("SERVICIO INICIADO", "MASTER-SLAVES-BCBP");

            try
            {
                string strMessage = "";
                string strInformation = "";
                string strFechaSincro = "";
                string strUltimaVez = "";

                DateTime inicio = DateTime.Now;

                DAO_Sincroniza objDAOSincroniza = new DAO_Sincroniza(Dsc_Path);
                if (Cod_Master != "")
                {
                    Property.Trace("PROCESANDO..", "SLAVES-MASTER-BCBP");
                    objDAOSincroniza.SincronizarSlavesToMasterBcbp(Cod_Master, Lista_Host, Can_Slaves, ref strFechaSincro, ref strUltimaVez, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-SLAVES-MASTER-BCBP", strMessage);
                        return;
                    }
                    TimeSpan duracion_clt = DateTime.Now - inicio;
                    double segundos_clt = duracion_clt.TotalSeconds;

                    PintarInformacion("SLAVES-MASTER-BCBP", strInformation);
                    Property.Trace("SLAVES-MASTER-BCBP FINALIZADO", " Duracion : " + segundos_clt.ToString());
                    
                    Property.Trace("PROCESANDO..", "MASTER-SLAVES-BCBP");
                    DateTime final_clt = DateTime.Now;
                    objDAOSincroniza.SincronizarMasterToSlavesBcbp(Cod_Master, Lista_Host, Can_Slaves, strFechaSincro, strUltimaVez, ref strMessage, ref strInformation);
                    if (strMessage.Trim() != "")
                    {
                        Property.Trace("ERROR-MASTER-SLAVES-BCBP", strMessage);
                        return;
                    }
                    TimeSpan duracion_lct = DateTime.Now - final_clt;
                    double segundos_lct = duracion_lct.TotalSeconds;
                    PintarInformacion("MASTER-SLAVES-BCBP",strInformation);
                    Property.Trace("MASTER-SLAVES-BCBP FINALIZADO", " Duracion : " + segundos_lct.ToString());
                }
                TimeSpan duracion = DateTime.Now - inicio;
                double segundosTotales = duracion.TotalSeconds;
                Property.Trace("SERVICIO FINALIZADO", "MASTER-SLAVES-BCBP" + " Duracion Total: " + segundosTotales.ToString());
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace("ERROR-MASTER-SLAVES-BCBP", ex);
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

        private void ObtenerMolinetes(bool boTodos)
        {
            string strBlanco = "        ";
            Dsc_Path = AppDomain.CurrentDomain.BaseDirectory;
            DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_Path);
            DataTable dtMolinetes = objDAOMolinete.listarAllMolinete();
            
            Cod_Master = "";
            Lista_Host = "";
            Can_Slaves = 0;

            for (int i = 0; i < dtMolinetes.Rows.Count; i++)
            {
                if (dtMolinetes.Rows[i].ItemArray.GetValue(6).ToString() == "A")
                {
                    if (dtMolinetes.Rows[i].ItemArray.GetValue(11).ToString() == "1")
                    {
                        Cod_Master = dtMolinetes.Rows[i].ItemArray.GetValue(0).ToString();
                        if (boTodos)
                        {
                            Lista_Host += (dtMolinetes.Rows[i].ItemArray.GetValue(1) + strBlanco).Substring(0, 15);
                            Can_Slaves++;
                        }
                    }
                    else
                    {
                        Lista_Host += (dtMolinetes.Rows[i].ItemArray.GetValue(1) + strBlanco).Substring(0, 15);
                        Can_Slaves++;
                    }
                }
            }
        }

    }
}
