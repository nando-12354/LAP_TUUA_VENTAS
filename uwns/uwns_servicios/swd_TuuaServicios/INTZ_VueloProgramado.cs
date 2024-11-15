using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Data;
using System.Collections;
using System.Timers;
using System.Globalization;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.ServiceProcess;

namespace LAP.TUUA.SERVICIOS
{
      public class INTZ_VueloProgramado:INTZ_ClienteWS
      {
            private DAO_VueloProgramado Obj_DAOVueloProgramado;
            private List<VueloProgramado> Lst_VueloProgramado;
            private VueloProgramado Obj_VueloProgramado;
            private INTZ_ProxyWS obj_Intz_ProxyWS;
            private int flg_Error;

            public INTZ_VueloProgramado() : base() 
            {
                flg_Error = 0;
            }

            /// <summary>
            /// 
            /// </summary>
            public void EjecutarServicio() 
            {
                  try
                  {
                      Flg_Error = 0;
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(AppDomain.CurrentDomain.BaseDirectory);
                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                        string strURLWebService = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral,"WP");
                        string strTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "TP");
                        string strMetodoRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "DP");
                        string strServicioRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "SP");
                        string strProtocoloSoap = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "PP");

                        obj_Intz_ProxyWS = new INTZ_ProxyWS();
                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("WP", strURLWebService);
                        HT_Propiedades.Add("TP", strTimeOut);
                        HT_Propiedades.Add("DP", strMetodoRemoto);
                        HT_Propiedades.Add("SP", strServicioRemoto);
                        HT_Propiedades.Add("PP", strProtocoloSoap);

                        if (Int32.Parse((string)HT_Propiedades["TP"]) > 0)
                        {
                              Timer tmVueloProgramado = new Timer();
                              tmVueloProgramado.Elapsed += new ElapsedEventHandler(ObtenerVueloProgramadoWS);
                              tmVueloProgramado.Interval = 1000 * Int32.Parse((string)HT_Propiedades["TP"]);
                              tmVueloProgramado.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Interfaz Vuelos  Programados - Iniciado###");
                        }

                  }
                  catch (Exception e)
                  {
                      Flg_Error = 3;
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];
                        String strSTrace = "";
                        int intOcurrencia = e.StackTrace.IndexOf("LAP");
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
                        //System.Environment.Exit(0);
                  }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="source"></param>
            /// <param name="e"></param>
            public void ObtenerVueloProgramadoWS(object source, ElapsedEventArgs e) 
            {
                  //Obtener Ip
                  IPHostEntry IPs = Dns.GetHostByName("");
                  IPAddress[] Direcciones = IPs.AddressList;
                  String IpClient = Direcciones[0].ToString();

                  try
                  {
                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Vuelos Programados ... En Ejecución.");
                        Obj_DAOVueloProgramado = new DAO_VueloProgramado(AppDomain.CurrentDomain.BaseDirectory);

                        CultureInfo culturaPeru = new CultureInfo("es-PE");
                        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

                        System.Xml.XmlNode xmlTasa = null;

                        try
                        {
                            //xmlTasa = (System.Xml.XmlNode)INTZ_ProxyWS.ObtenerWebService((string)HT_Propiedades["WP"],
                            //                         (string)HT_Propiedades["SP"], (string)HT_Propiedades["DP"], (string)HT_Propiedades["PP"],
                            //                          null);


                            object obj = obj_Intz_ProxyWS.ObtenerWebService((string)HT_Propiedades["WP"],
                                     (string)HT_Propiedades["SP"], (string)HT_Propiedades["DP"], (string)HT_Propiedades["PP"], null);

                            if (obj != null)
                                  xmlTasa = (System.Xml.XmlNode)obj;

                        }
                        catch (Exception exc)
                        {
                            Flg_Error = 1;
                            //GeneraAlarma
                            GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000050", "S01", IpClient, "3", "Alerta W0000050", "Error en conexion de web service Vuelo Programado, Error: " + exc.Message, INTZ_Define.Cod_UServicio);

                            string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                            string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                            int intOcurrencia = exc.StackTrace.IndexOf("LAP");
                            String strSTrace = "";
                            if (intOcurrencia > 1)
                            {
                                strSTrace = exc.StackTrace.Substring(intOcurrencia);
                                intOcurrencia = strSTrace.IndexOf(")");
                                if (intOcurrencia>1)
                                strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                            }

                            INTZ_Util.EscribirLog(this, strMessage);
                            INTZ_Util.EscribirLog(strSTrace, exc.GetType().Name);
                            INTZ_Util.EscribirLog(this, exc.GetBaseException().Message);
                        }



                        System.Xml.XmlNodeList xmlLista = xmlTasa.SelectNodes("Vuelo");

                        //Obtener vuelos del XML
                        Lst_VueloProgramado = new List<VueloProgramado>();
                        foreach (System.Xml.XmlElement xmlNodo in xmlLista)
                        {
                              Obj_VueloProgramado = new VueloProgramado();

                              System.Xml.XmlNodeList xmlnCodEmpresa = xmlNodo.GetElementsByTagName("CodEmpresa");
                              string strCodEmpresa = xmlnCodEmpresa[0].InnerText.Trim();
                              DAO_Compania objDAOCompania = new DAO_Compania(AppDomain.CurrentDomain.BaseDirectory);
                              Compania objCompania = objDAOCompania.obtener(strCodEmpresa);
                              if (objCompania != null)
                              {
                                    System.Xml.XmlNodeList xmlnNumeroVuelo = xmlNodo.GetElementsByTagName("NumeroVuelo");
                                    string strNumeroVuelo = xmlnNumeroVuelo[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnFechaVuelo = xmlNodo.GetElementsByTagName("FechaVuelo");
                                    string strFechaVuelo = xmlnFechaVuelo[0].InnerText.Trim();
                                    DateTime dtFechaVuelo = Convert.ToDateTime(strFechaVuelo);
                                    strFechaVuelo = Function.ConvertirDosDigitos(dtFechaVuelo.Year)
                                                    + Function.ConvertirDosDigitos(dtFechaVuelo.Month)
                                                    + Function.ConvertirDosDigitos(dtFechaVuelo.Day);

                                    string strHorVuelo = Function.ConvertirDosDigitos(dtFechaVuelo.Hour)
                                                         + Function.ConvertirDosDigitos(dtFechaVuelo.Minute)
                                                         + Function.ConvertirDosDigitos(dtFechaVuelo.Second);

                                    System.Xml.XmlNodeList xmlnDescripcion = xmlNodo.GetElementsByTagName("Descripcion");
                                    string strDescripcion = xmlnDescripcion[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnTipoVuelo = xmlNodo.GetElementsByTagName("TipoVuelo");
                                    string strTipoVuelo = xmlnTipoVuelo[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnEstado = xmlNodo.GetElementsByTagName("Estado");
                                    string strEstado = xmlnEstado[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnDestino = xmlNodo.GetElementsByTagName("Destino");
                                    string strDestino = xmlnDestino[0].InnerText.Trim();

                                    Obj_VueloProgramado.Cod_Compania = objCompania.SCodCompania;
                                    Obj_VueloProgramado.Num_Vuelo = strNumeroVuelo;
                                    Obj_VueloProgramado.Fch_Vuelo = strFechaVuelo;
                                    Obj_VueloProgramado.Dsc_Vuelo = strDescripcion;
                                    Obj_VueloProgramado.Tip_Vuelo = strTipoVuelo;
                                    Obj_VueloProgramado.Tip_Estado = strEstado;
                                    Obj_VueloProgramado.Dsc_Destino = strDestino;
                                    Obj_VueloProgramado.Hor_Vuelo = strHorVuelo;
                                    Obj_VueloProgramado.Log_Usuario_Mod = INTZ_Define.Cod_UServicio;
                                    //valida compañia,vuelo,fecha,hora
                                    if (!ExisteVuelo(Obj_VueloProgramado,Lst_VueloProgramado))
                                          Lst_VueloProgramado.Add(Obj_VueloProgramado);
                                    
                              }
                        }

                        Obj_DAOVueloProgramado.insertarlista(Lst_VueloProgramado);
                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Vuelos  Programados - Terminado.");

                        

                  }
                  catch(Exception exc)
                  {
                        //GeneraAlarma
                      Flg_Error = 2;
                      GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000051", "S01", IpClient, "3", "Alerta W0000051", "Error al actualizar los Vuelo Programados, Error: " + exc.Message, INTZ_Define.Cod_UServicio);

                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = exc.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = exc.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              if (intOcurrencia>1)
                              strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, exc.GetType().Name);
                        INTZ_Util.EscribirLog(this, exc.GetBaseException().Message);
                  }

                  
            }

            bool ExisteVuelo(VueloProgramado objVuelo,List<VueloProgramado> lstVuelo) 
            {
                    foreach(VueloProgramado obj in lstVuelo)
                  {
                        if (objVuelo.Cod_Compania.Equals(obj.Cod_Compania) && 
                            objVuelo.Fch_Vuelo.Equals(obj.Fch_Vuelo) &&
                            objVuelo.Hor_Vuelo.Equals(obj.Hor_Vuelo) &&
                            objVuelo.Num_Vuelo.Equals(obj.Num_Vuelo) 
                            )
                        {
                              return true;
                        }

                  }
                  return false;
            }

            public int Flg_Error
            {
                get
                {
                    return flg_Error;
                }
                set
                {
                    flg_Error = value;
                }
            }

      }
}
