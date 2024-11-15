using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Data;
using System.Collections;
using System.Timers;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.SERVICIOS
{
      public class SVC_VencimientoBoarding : INTZ_ClienteWS
      {
            private DAO_BoardingBcbp Obj_DAOBoarding;
            private DAO_ModVentaCompAtr Obj_DAOModVentaCompAtr;
            private DAO_ModalidadAtrib Obj_DAOModalidadAtrib;
            private DAO_ModalidadVenta Obj_DAOModVenta;
            private List<BoardingBcbp> Lst_Boarding;
            private List<ModVentaCompAtr> Lst_ModVentaCompAtr;
            private List<ModalidadAtrib> Lst_ModalidadAtrib;
            private Object thisLock = new Object();

            public SVC_VencimientoBoarding() 
            { 

            }

            private void ActualizarBoarding(object source, ElapsedEventArgs e)
            {
                  bool isBoardingRehab = false;
                  //Obtener Ip
                  IPHostEntry IPs = Dns.GetHostByName("");
                  IPAddress[] Direcciones = IPs.AddressList;
                  String IpClient = Direcciones[0].ToString();
                  try
                  {
                      lock (thisLock)
                      {
                          INTZ_Util.EscribirLog(this, "Servicio Vencimiento Boarding ... En Ejecución");
                          Obj_DAOBoarding = new DAO_BoardingBcbp(INTZ_Define.Dsc_SPConfig);
                          Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);

                          //obtener  boardings  rehabilitados
                          Lst_Boarding = Obj_DAOBoarding.listarxEstado();

                          foreach (BoardingBcbp obj in Lst_Boarding)
                          {
                              DataTable dtFecha = Obj_DAOParameGeneral.obtenerFecha();
                              string strFecha = dtFecha.Rows[0].ItemArray.GetValue(0).ToString();
                              DateTime dtHoy = new DateTime(Int32.Parse(strFecha.Substring(0, 4)),
                                                            Int32.Parse(strFecha.Substring(4, 2)),
                                                            Int32.Parse(strFecha.Substring(6, 2)),
                                                            Int32.Parse(strFecha.Substring(8, 2)),
                                                            Int32.Parse(strFecha.Substring(10, 2)),
                                                            Int32.Parse(strFecha.Substring(12, 2)));

                              DateTime dtVencimiento = obj.StrFch_Vencimiento;

                              if (dtVencimiento < dtHoy && dtVencimiento.Year != 1)
                              {
                                  if (obj.StrTip_Estado == INTZ_Define.Cod_Rehabilitado)
                                  {
                                      isBoardingRehab = true;
                                  }
                                  obj.StrTip_Estado = INTZ_Define.Cod_Anulado; //Anular
                                  obj.StrDsc_Boarding_Estado = INTZ_Define.Dsc_Anulado;
                                  obj.StrLogUsuarioMod = INTZ_Define.Cod_UServicio;
                                  obj.StrCod_Equipo_Mod = INTZ_Define.Cod_EServicio;
                                  obj.StrFlg_Sincroniza = "0";
                                  Obj_DAOBoarding.Cod_Modulo = INTZ_Define.Cod_MServicio;
                                  Obj_DAOBoarding.Cod_Sub_Modulo = INTZ_Define.Cod_SVencimientoBCBP;   
                                  Obj_DAOBoarding.actualizarEstado(obj);
                              }
                              isBoardingRehab = false;
                          }
                          INTZ_Util.EscribirLog(this, "Servicio Vencimiento Boarding - Terminado");
                      }
                  }
                       
                         
                  
                  catch (Exception ex)
                  {
                        if (isBoardingRehab)
                        {
                            //GeneraAlarma
                            GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000058", "S01", IpClient, "3", "Alerta W0000058", "Error asociado al vencer BCBP Rehabilitados, Error: " + ex.Message, INTZ_Define.Cod_UServicio);
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
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);

                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                        string strdtTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "FB");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("FB", strdtTimeOut);

                        if (Int32.Parse((string)HT_Propiedades["FB"]) > 0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ActualizarBoarding);
                              tmTasaCambio.Interval = 1000 * Int32.Parse((string)HT_Propiedades["FB"]);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Vencimiento Boarding - Iniciado###");
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
