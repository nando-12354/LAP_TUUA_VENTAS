using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Data;
using System.Collections;
using System.Timers;
using LAP.TUUA.ENTIDADES;
using System.Threading;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.SERVICIOS
{
      public class SVC_VencimientoTicket : INTZ_ClienteWS
      {
            private DAO_Ticket Obj_DAOTicket;
            private DAO_ModVentaCompAtr Obj_DAOModVentaCompAtr;
            private DAO_ModalidadAtrib Obj_DAOModalidadAtrib;
            private List<Ticket> Lst_Ticket;
            private List<ModVentaCompAtr> Lst_ModVentaCompAtr;
            private List<ModalidadAtrib> Lst_ModalidadAtrib;
            private Object thisLock = new Object();

            public SVC_VencimientoTicket() 
            { 

            }


            private void ActualizarTicket(object source, ElapsedEventArgs e)
            {
                bool isTicketRehab = false;
                bool isTicketEmit = false;
                //Obtener Ip
                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();

                try
                {

                      lock (thisLock)
                      {
                          INTZ_Util.EscribirLog(this, "Servicio Vencimiento Ticket ... En Ejecución");
                          Obj_DAOTicket = new DAO_Ticket(INTZ_Define.Dsc_SPConfig);
                          Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);
                          Lst_Ticket = Obj_DAOTicket.listarxEstado();
                          foreach (Ticket obj in Lst_Ticket)
                          {
                              DataTable dtFecha = Obj_DAOParameGeneral.obtenerFecha();
                              string strFecha = dtFecha.Rows[0].ItemArray.GetValue(0).ToString();
                              DateTime dtHoy = new DateTime(Int32.Parse(strFecha.Substring(0, 4)),
                                                            Int32.Parse(strFecha.Substring(4, 2)),
                                                            Int32.Parse(strFecha.Substring(6, 2)),
                                                            Int32.Parse(strFecha.Substring(8, 2)),
                                                            Int32.Parse(strFecha.Substring(10, 2)),
                                                            Int32.Parse(strFecha.Substring(12, 2)));

                              DateTime dtVencimiento = new DateTime(Int32.Parse(obj.DtFchVencimiento.Substring(0, 4)),
                                                                    Int32.Parse(obj.DtFchVencimiento.Substring(4, 2)),
                                                                    Int32.Parse(obj.DtFchVencimiento.Substring(6, 2)),
                                                                    Int32.Parse(strFecha.Substring(8, 2)),
                                                                    Int32.Parse(strFecha.Substring(10, 2)),
                                                                    Int32.Parse(strFecha.Substring(12, 2))
                                                                   );

                              if (dtVencimiento < dtHoy)
                              {
                                  if (obj.STipEstadoActual == INTZ_Define.Cod_Emitido)
                                  {
                                      isTicketEmit = true;
                                  }
                                  if (obj.STipEstadoActual == INTZ_Define.Cod_Rehabilitado)
                                  {
                                      isTicketRehab = true;
                                  }

                                  obj.STipEstadoActual = INTZ_Define.Cod_Anulado; //Anular
                                  obj.SCodUsuarioVenta = INTZ_Define.Cod_UServicio;
                                  obj.Tip_Anula = INTZ_Define.Tip_Anulacion;
                                  obj.Flg_Sincroniza = "0";                                 
                                  Obj_DAOTicket.Cod_Modulo = INTZ_Define.Cod_MServicio;
                                  Obj_DAOTicket.Cod_Sub_Modulo = INTZ_Define.Cod_SVencimientoTicket;                                  
                                  Obj_DAOTicket.actualizar(obj);
                              }
                              isTicketEmit = false;
                              isTicketRehab = false;
                          }
                          INTZ_Util.EscribirLog(this, "Servicio Vencimiento Ticket - Terminado.");
                      }
                  }
                  catch (Exception ex)
                  {
                      if (isTicketEmit)
                      {
                          //GeneraAlarma
                          GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000056", "S01", IpClient, "3", "Alerta W0000056", "Error asociado al vencer Ticket Emitidos, Error: " + ex.Message, INTZ_Define.Cod_UServicio);
                      }

                      if (isTicketRehab)
                      {
                          //GeneraAlarma
                          GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000057", "S01", IpClient, "3", "Alerta W0000057", "Error asociado al vencer Ticket Rehabilitados, Error: " + ex.Message, INTZ_Define.Cod_UServicio);
                      }

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
                        //Thread.Sleep(10000);
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);

                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                        string strdtTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "FK");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("FK", strdtTimeOut);

                        if (Int32.Parse((string)HT_Propiedades["FK"]) > 0)
                        {
                              System.Timers.Timer tmTasaCambio = new System.Timers.Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ActualizarTicket);
                              tmTasaCambio.Interval = 1000 * Int32.Parse((string)HT_Propiedades["FK"]);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Vencimiento Ticket - Iniciado###");
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
                              strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, e.GetType().Name);
                        INTZ_Util.EscribirLog(this, e.GetBaseException().Message);
                        //System.Environment.Exit(0);
                  }
            }
      }
}
