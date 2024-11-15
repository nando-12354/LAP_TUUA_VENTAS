using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using System.Data;
using System.Collections;
using System.Timers;
using System.Globalization;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.SERVICIOS
{
      class INTZ_VueloTemporada : INTZ_ClienteWS
      {
            private DAO_VuelosTemporada Obj_DAOVueloTemporada;
            private List<VuelosTemporada> Lst_VueloTemporada ;
            private VuelosTemporada Obj_VueloTemporada;
            private INTZ_ProxyWS obj_Intz_ProxyWS;

            public void EjecutarServicio()
            {
                  try
                  {
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(AppDomain.CurrentDomain.BaseDirectory);
                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                        string strURLWebService = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "WT");
                        string strTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "TT");
                        string strMetodoRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "DT");
                        string strServicioRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "ST");
                        string strProtocoloSoap = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "PT");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("WT", strURLWebService);
                        HT_Propiedades.Add("TT", strTimeOut);
                        HT_Propiedades.Add("DT", strMetodoRemoto);
                        HT_Propiedades.Add("ST", strServicioRemoto);
                        HT_Propiedades.Add("PT", strProtocoloSoap);

                        if (Int32.Parse((string)HT_Propiedades["TT"]) > 0)
                        {
                              Timer tmVueloProgramado = new Timer();
                              tmVueloProgramado.Elapsed += new ElapsedEventHandler(ObtenerVueloTemporadaWS);
                              tmVueloProgramado.Interval = 1000 * Int32.Parse((string)HT_Propiedades["TT"]);
                              tmVueloProgramado.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Interfaz Vuelos Temporada - Iniciado###");
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
                        //System.Environment.Exit(0);
                  }
            }

            public void ObtenerVueloTemporadaWS(object source, ElapsedEventArgs e)
            {
                  //Obtener Ip
                  IPHostEntry IPs = Dns.GetHostByName("");
                  IPAddress[] Direcciones = IPs.AddressList;
                  String IpClient = Direcciones[0].ToString();

                  try
                  {
                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Vuelos Temporada ... En Ejecución");
                        Obj_DAOVueloTemporada = new DAO_VuelosTemporada(AppDomain.CurrentDomain.BaseDirectory);

                        CultureInfo culturaPeru = new CultureInfo("es-PE");
                        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
                        System.Xml.XmlNode xmlVueloTemporada = null;

                        try
                        {
                            //xmlVueloTemporada = (System.Xml.XmlNode)INTZ_ProxyWS.ObtenerWebService((string)HT_Propiedades["WT"],
                            //                                                     (string)HT_Propiedades["ST"], (string)HT_Propiedades["DT"], (string)HT_Propiedades["PT"],
                            //                                                      null);
                            obj_Intz_ProxyWS = new INTZ_ProxyWS();
                            object obj = obj_Intz_ProxyWS.ObtenerWebService((string)HT_Propiedades["WT"],
                                   (string)HT_Propiedades["ST"], (string)HT_Propiedades["DT"], (string)HT_Propiedades["PT"], null);

                            if (obj != null)
                                  xmlVueloTemporada = (System.Xml.XmlNode)obj;


                        }
                        catch (Exception execp)
                        {

                            //GeneraAlarma
                            GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000052", "S01", IpClient, "3", "Alerta W0000052", "Error en conexion de web service Vuelos por Temporada, Error: " + execp.Message, INTZ_Define.Cod_UServicio);

                            string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                            string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                            int intOcurrencia = execp.StackTrace.IndexOf("LAP");
                            String strSTrace = "";
                            if (intOcurrencia > 1)
                            {
                                strSTrace = execp.StackTrace.Substring(intOcurrencia);
                                intOcurrencia = strSTrace.IndexOf(")");
                                strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                            }

                            INTZ_Util.EscribirLog(this, strMessage);
                            INTZ_Util.EscribirLog(strSTrace, execp.GetType().Name);
                            INTZ_Util.EscribirLog(this, execp.GetBaseException().Message);
                        }

                        System.Xml.XmlNodeList xmlLista = xmlVueloTemporada.SelectNodes("Vuelo");

                        //obtener vuelos del XML
                        Lst_VueloTemporada = new List<VuelosTemporada>();
                        foreach (System.Xml.XmlElement xmlNodo in xmlLista)
                        {
                              Obj_VueloTemporada = new VuelosTemporada();

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

                                    Obj_VueloTemporada.Cod_Compania = objCompania.SCodCompania;
                                    Obj_VueloTemporada.Num_Vuelo = strNumeroVuelo;
                                    Obj_VueloTemporada.Fch_Vuelo = strFechaVuelo;
                                    Obj_VueloTemporada.Dsc_Vuelo = strDescripcion;
                                    Obj_VueloTemporada.Tip_Vuelo = strTipoVuelo;
                                    Obj_VueloTemporada.Tip_Estado = strEstado;
                                    Obj_VueloTemporada.Dsc_Destino = strDestino;
                                    Obj_VueloTemporada.Hor_Vuelo = strHorVuelo;
                                    Obj_VueloTemporada.Log_Usuario_Mod = INTZ_Define.Cod_UServicio;
                                    if (!ExisteVuelo(Obj_VueloTemporada,Lst_VueloTemporada))
                                          Lst_VueloTemporada.Add(Obj_VueloTemporada);
                              }
                        }

                        //using (System.Transactions.TransactionScope scope =
                        //           new System.Transactions.TransactionScope())
                        //{
                        //      //eliminar e insertar vuelos
                        //      Obj_DAOVueloTemporada.Limpiar();

                        //      foreach (VuelosTemporada obj in Lst_VueloTemporada)
                        //      {
                        //            Obj_DAOVueloTemporada.insertar(obj);
                        //      }
                        //      scope.Complete();
                        //      INTZ_Util.EscribirLog(this, "Servicio Interfaz Vuelos Temporada - Terminado.");
                        //}
                        Obj_DAOVueloTemporada.insertarlista(Lst_VueloTemporada);
                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Vuelos Temporada - Terminado.");


                  }
                  catch(Exception execp)
                  {

                        //GeneraAlarma
                      GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000053", "S01", IpClient, "3", "Alerta W0000053", "Error al Actualizar los Vuelos por Temporada, Error: " + execp.Message, INTZ_Define.Cod_UServicio);
                      
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = execp.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = execp.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, execp.GetType().Name);
                        INTZ_Util.EscribirLog(this, execp.GetBaseException().Message);
                  }

                  
            }

            bool ExisteVuelo(VuelosTemporada objVuelo, List<VuelosTemporada> lstVuelo)
            {
                  foreach (VuelosTemporada obj in lstVuelo)
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

      }
}
