using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Collections;
using System.Timers;
using LAP.TUUA.ALARMAS;
using System.Net;


namespace LAP.TUUA.SERVICIOS
{
      public class SVC_PrecioTicket:INTZ_ClienteWS
      {
            private DAO_PrecioTicket Obj_DAOPrecioTicket;
            public SVC_PrecioTicket() 
            { 

            }

            private void ActualizarPrecioTicket(object source, ElapsedEventArgs e)
            {
                  try
                  {
                        INTZ_Util.EscribirLog(this, "Servicio de Programación Precio del Ticket ... En Ejecución");
                        Obj_DAOPrecioTicket.actualizar2(INTZ_Define.Cod_UServicio,INTZ_Define.Cod_MServicio,INTZ_Define.Cod_SPrecioTicket);
                        INTZ_Util.EscribirLog(this, "Servicio de Programación Precio del Ticket - Terminado.");
                  }
                  catch (Exception ex)
                  {
                        //Obtener Ip
                        IPHostEntry IPs = Dns.GetHostByName("");
                        IPAddress[] Direcciones = IPs.AddressList;
                        String IpClient = Direcciones[0].ToString();
                        //GeneraAlarma
                        GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000061", "S01", IpClient, "3", "Alerta W0000061", "Error asociado al proceso de actualizacion de Precio de Ticket Programada, Error: " + ex.Message, INTZ_Define.Cod_UServicio);

                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = ex.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              if (intOcurrencia>1)
                              strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, ex.GetType().Name);
                        INTZ_Util.EscribirLog(this, ex.GetBaseException().Message);
                  }
            } 

            public void EjecutarServicio()
            {
                  try
                  {
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);
                        Obj_DAOPrecioTicket = new DAO_PrecioTicket(INTZ_Define.Dsc_SPConfig);

                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                        string strdtTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "FT");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("FT", strdtTimeOut);

                        if (Int32.Parse((string)HT_Propiedades["FT"]) > 0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ActualizarPrecioTicket);
                              tmTasaCambio.Interval = 1000 * Int32.Parse((string)HT_Propiedades["FT"]);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio de Programación Precio del Ticket - Iniciado###");
                        }
                  }
                  catch (Exception e)
                  {
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = e.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = e.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              if (intOcurrencia>1)
                                    strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, e.GetType().Name);
                        INTZ_Util.EscribirLog(this, e.GetBaseException().Message);
                  }
            }

      }
}
