///File: INTZ_TasaCambio.cs
///Proposito:Carga periodicamente la tasa de cambio
///Metodos: 
///void CargarConfig()
///void EjecutarServicio() 
///void ObtenerTasaCambio(object source, ElapsedEventArgs e)
///int ProcesarTasaCambio(string strMoneda, string strCambio,string strImporte ) 
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:31/08/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:31/08/2009

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Timers;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Globalization;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.SERVICIOS
{
      class INTZ_TasaCambio:INTZ_ClienteWS
      {
            private DAO_TasaCambio Obj_DAOTasaCambio;
            private DAO_TasaCambioHist Obj_DAOTasaCambioHist;
            private DAO_LogTasaCambio Obj_DAOLogTasaCambio;
            private DAO_Moneda Obj_DAOMoneda;
            private INTZ_ProxyWS obj_Intz_ProxyWS;
            private List<TasaCambio> Lst_TasaCambioIns;
            private List<TasaCambioHist> Lst_TasaCambioHist;
            private List<LogTasaCambio> Lst_TasaCambioLog;
            
            public INTZ_TasaCambio():base()
            {
                  
            }

            /// <summary>
            /// Procesa tasa de cambio
            /// </summary>
            /// <param name="strMoneda">codigo de moneda</param>
            /// <param name="strCambio">tipo de cambio</param>
            /// <param name="strImporte">importe</param>
            /// <returns>tipo de proceso realizado
            /// 0:Tasa no existe
            /// </returns>

            private int  ProcesarTasaCambioWS(string strMoneda, string strCambio,string strImporte ) 
            {
                  string strCodTasaCambio, strTipCambio, strCodMoneda, strImpCambAct, strTipEstado;
                  DateTime dtFchIni;
                  int intI=0;
                  bool blnFlagExiste = false;

                  DataTable dtTasaCambio = Obj_DAOTasaCambio.obtener2("");//optimizar

                  LogTasaCambio  objLogTasaCambio  = new LogTasaCambio();
                  TasaCambioHist objTasaCambioHist = new TasaCambioHist();
                  TasaCambio     objTasaCambio     = new TasaCambio();

                  CultureInfo culturaPeru = new CultureInfo("es-PE");
                  System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

                  while (dtTasaCambio != null && dtTasaCambio.Rows.Count > 0 && intI < dtTasaCambio.Rows.Count)
                  {
                        strCodTasaCambio = dtTasaCambio.Rows[intI].ItemArray.GetValue(0).ToString();
                        strTipCambio = dtTasaCambio.Rows[intI].ItemArray.GetValue(1).ToString();
                        strCodMoneda = dtTasaCambio.Rows[intI].ItemArray.GetValue(2).ToString();
                        strImpCambAct = dtTasaCambio.Rows[intI].ItemArray.GetValue(3).ToString();
                        dtFchIni = (DateTime)dtTasaCambio.Rows[intI].ItemArray.GetValue(4);
                        strTipEstado = dtTasaCambio.Rows[intI].ItemArray.GetValue(11).ToString();
                        
                        if  (strMoneda.Trim().Equals(strCodMoneda.Trim()) && strTipCambio.Trim().Equals(strCambio) &&
                             strTipEstado.Trim().Equals(INTZ_Define.Cod_Activo)
                            )
                        {
                              if (decimal.Parse(strImporte.Trim()) != decimal.Parse(strImpCambAct.Trim()))
                              {
                                    //Grabar Tasa Historial
                                    objTasaCambioHist.SCodTasaCambio = strCodTasaCambio;
                                    objTasaCambioHist.STipCambio = strTipCambio;
                                    objTasaCambioHist.SCodMoneda = strCodMoneda;
                                    objTasaCambioHist.DImpValor = double.Parse(strImpCambAct);
                                    objTasaCambioHist.DtFchFin = DateTime.Now;
                                    objTasaCambioHist.DtFchIni = dtFchIni;
                                    objTasaCambioHist.SCodMoneda2 = strMoneda;
                                    objTasaCambioHist.DImpValor2 = double.Parse(strImporte);
                                    objTasaCambioHist.SLogUsuarioMod = INTZ_Define.Cod_UServicio;
                                    Lst_TasaCambioHist.Add(objTasaCambioHist);
                                    //Eliminar de Tasa Cambio
                                    //Grabar en Tasa de Cambio
                                    blnFlagExiste = false;
                                    break;
                              }
                              else
                              {
                                    blnFlagExiste = true;
                                    break;
                              }
                        }
                        intI++;
                  }

                  //verifica busqueda de tasa de cambio
                  if (!blnFlagExiste)
                  {
                        objTasaCambio.SCodMoneda = strMoneda;
                        objTasaCambio.STipCambio = strCambio;
                        objTasaCambio.DImpCambioActual = decimal.Parse(strImporte);
                        objTasaCambio.DtFchProceso = DateTime.Now;
                        objTasaCambio.STipEstado = INTZ_Define.Cod_Activo;
                        objTasaCambio.STipIngreso = INTZ_Define.Cod_Inactivo;
                        objTasaCambio.SLogUsuarioMod = INTZ_Define.Cod_UServicio;
                        Lst_TasaCambioIns.Add(objTasaCambio);

                  }
                  return 0;
            }

            /// <summary>
            /// Obtiene tasa de cambio en un intervalo de tiempo
            /// </summary>
            /// <param name="source"></param>
            /// <param name="e"></param>
            private void ObtenerTasaCambioWS(object source, ElapsedEventArgs e)
            {
                  try
                  {
                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Tasa Cambio ... En Ejecución");
                        CultureInfo culturaPeru = new CultureInfo("es-PE");
                        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

                        //obtener instancia web service
                        LogTasaCambio objLogTasaCambio;
                        System.Xml.XmlNode xmlTasa=null;
                        try
                        {
                              
                              //xmlTasa = (System.Xml.XmlNode)INTZ_ProxyWS.ObtenerWebService((string)HT_Propiedades["WC"],
                              //          (string)HT_Propiedades["SC"], (string)HT_Propiedades["DC"], (string)HT_Propiedades["PC"], null);
                            obj_Intz_ProxyWS = new INTZ_ProxyWS();
                            object obj = obj_Intz_ProxyWS.ObtenerWebService((string)HT_Propiedades["WC"],
                                        (string)HT_Propiedades["SC"], (string)HT_Propiedades["DC"], (string)HT_Propiedades["PC"], null);

                              if (obj !=null)
                              xmlTasa = (System.Xml.XmlNode)obj;
                        }
                        catch(Exception ex )
                        {
                              IPHostEntry IPs = Dns.GetHostByName("");
                              IPAddress[] Direcciones = IPs.AddressList;
                              String IpClient = Direcciones[0].ToString();
                              //GeneraAlarma
                              GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000048", "S01", IpClient, "3", "Alerta W0000048", "Error en conexion de web service tasa cambio, Error: " + ex.Message, INTZ_Define.Cod_UServicio);

                              string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                              string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                              int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                              String strSTrace = "";
                              if (intOcurrencia > 1)
                              {
                                    strSTrace = ex.StackTrace.Substring(intOcurrencia);
                                    intOcurrencia = strSTrace.IndexOf(")");
                                    if (intOcurrencia > 1)
                                          strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                              }

                              INTZ_Util.EscribirLog(this, strMessage);
                              INTZ_Util.EscribirLog(strSTrace, ex.GetType().Name);
                              INTZ_Util.EscribirLog(this, ex.GetBaseException().Message);

                              return;

                        }

                        if (xmlTasa != null)
                        {
                              System.Xml.XmlNodeList xmlLista = xmlTasa.SelectNodes("Tipo_Moneda");
                              //procesar tasa de cambio, obtenido del XML
                              foreach (System.Xml.XmlElement xmlNodo in xmlLista)
                              {
                                    //obtener tasa de cambio
                                    System.Xml.XmlNodeList xmlnId = xmlNodo.GetElementsByTagName("Id");
                                    string strId = xmlnId[0].InnerText.Trim();
                                    System.Xml.XmlNodeList xmlnCompra = xmlNodo.GetElementsByTagName("Compra");
                                    string strCompra = xmlnCompra[0].InnerText.Trim();
                                    System.Xml.XmlNodeList xmlnVenta = xmlNodo.GetElementsByTagName("Venta");
                                    string strVenta = xmlnVenta[0].InnerText.Trim();

                                    //procesar tasas de cambio
                                    DataTable dtMoneda = Obj_DAOMoneda.obtenerMoneda(strId);
                                    if (dtMoneda.Rows.Count>0)
                                    {
                                          int intRpta1 = ProcesarTasaCambioWS(strId, INTZ_Define.Cod_Compra, strCompra);
                                          int intRpta2 = ProcesarTasaCambioWS(strId, INTZ_Define.Cod_Venta, strVenta);
                                    }

                                    //grabar Tasa Log
                                    string strFlag = (String)HT_Propiedades["FC"];
                                    if (strFlag.Trim().Equals(INTZ_Define.Cod_TGuardaLog))
                                    {
                                          objLogTasaCambio = new LogTasaCambio();
                                          objLogTasaCambio.SCodMoneda = strId;
                                          objLogTasaCambio.DImpCompra = decimal.Parse(strCompra);
                                          objLogTasaCambio.DImpVenta = decimal.Parse(strVenta);
                                          objLogTasaCambio.SLogUsuarioMod = INTZ_Define.Cod_UServicio;
                                          Lst_TasaCambioLog.Add(objLogTasaCambio);
                                    }
                              }

                              foreach (TasaCambioHist obj in Lst_TasaCambioHist)
                              {
                                    Obj_DAOTasaCambioHist.insertar(obj);
                                    Obj_DAOTasaCambio.eliminar(obj.SCodTasaCambio);
                              }
                              
                              //foreach (TasaCambio obj in Lst_TasaCambioIns)
                              //{
                              //      Obj_DAOTasaCambio.insertar(obj);
                              //}
                              
                              //foreach (LogTasaCambio obj in Lst_TasaCambioLog)
                              //{
                              //      Obj_DAOLogTasaCambio.insertar(obj);
                              //}
                              Obj_DAOTasaCambio.insertarlista(Lst_TasaCambioHist, Lst_TasaCambioIns, Lst_TasaCambioLog, INTZ_Define.Cod_UServicio);
                              Lst_TasaCambioLog.Clear();
                              Lst_TasaCambioIns.Clear();
                              Lst_TasaCambioHist.Clear();
                              INTZ_Util.EscribirLog(this, "Servicio Interfaz Tasa Cambio - Terminado.");
                              
                              IPHostEntry IPs = Dns.GetHostByName("");
                              IPAddress[] Direcciones = IPs.AddressList;
                              String IpClient = Direcciones[0].ToString();
                              //GeneraAlarma
                              GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000049", "S01", IpClient, "3", "Alerta W0000049", "Servicio de actualizacion de tasa de cambio terminado con exito ", INTZ_Define.Cod_UServicio);

                        }
                        
                  }
                  catch (Exception ex)
                  {
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                        String strSTrace="";
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
            /// <summary>
            /// Ejecuta Servicio
            /// </summary>
            public void EjecutarServicio() 
            {
                  try
                  {
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOTasaCambio = new DAO_TasaCambio(INTZ_Define.Dsc_SPConfig);
                        Obj_DAOTasaCambioHist = new DAO_TasaCambioHist(INTZ_Define.Dsc_SPConfig);
                        Obj_DAOLogTasaCambio = new DAO_LogTasaCambio(INTZ_Define.Dsc_SPConfig);
                        Obj_DAOMoneda = new DAO_Moneda(INTZ_Define.Dsc_SPConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);
                        Lst_TasaCambioHist = new List<TasaCambioHist>();
                        Lst_TasaCambioIns = new List<TasaCambio>();
                        Lst_TasaCambioLog = new List<LogTasaCambio>();

                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();

                        string strURLWebService =INTZ_Util.ObtenerParamGral(Lst_ParameGeneral,"WC");
                        string strTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "TC");
                        string strFlagGuardaLog = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "FC");
                        string strMetodoRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "DC");
                        string strServicioRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "SC");
                        string strProtocoloSoap = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "PC");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("WC", strURLWebService);
                        HT_Propiedades.Add("TC", strTimeOut);
                        HT_Propiedades.Add("FC", strFlagGuardaLog);
                        HT_Propiedades.Add("DC", strMetodoRemoto);
                        HT_Propiedades.Add("SC", strServicioRemoto);
                        HT_Propiedades.Add("PC", strProtocoloSoap);

                        if (Int32.Parse((string)HT_Propiedades["TC"]) > 0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ObtenerTasaCambioWS);
                              tmTasaCambio.Interval = 1000 * Int32.Parse((string)HT_Propiedades["TC"]);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Interfaz Tasa Cambio - Iniciado###");
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
                  }
            }
      }
}
