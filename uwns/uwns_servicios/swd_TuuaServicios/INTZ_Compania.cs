using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Collections;
using System.Timers;
using System.Globalization;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.SERVICIOS
{
      public class INTZ_Compania : INTZ_ClienteWS
      {
            private DAO_Compania Obj_DAOCompania;
            private List<Compania> Lst_Compania;
            private List<Compania> Lst_CompaniaIns;
            private Compania objCompania;
            private INTZ_ProxyWS obj_Intz_ProxyWS;
            private int flg_Error;

            public INTZ_Compania() :base()
            { 

            }

           
            private void ObtenerCompaniaWS(object source, ElapsedEventArgs e) 
            {
                  try
                  {
                        //obtener instancia web service
                        bool flagEncontrado;
                        int i;
                        int k;

                        INTZ_Util.EscribirLog(this, "Servicio Interfaz Compañia ... En ejecución");
                        obj_Intz_ProxyWS = new INTZ_ProxyWS();
                        System.Xml.XmlNode xmlTasa = (System.Xml.XmlNode)obj_Intz_ProxyWS.ObtenerWebService((string)HT_Propiedades["WN"],
                                                     (string)HT_Propiedades["SN"], (string)HT_Propiedades["DN"], (string)HT_Propiedades["PN"], null);

                        Lst_Compania= Obj_DAOCompania.listar();
                        Lst_CompaniaIns= new List<Compania>();
                        k = 0;
                        if (xmlTasa != null)
                        {
                              System.Xml.XmlNodeList xmlLista = xmlTasa.SelectNodes("Compania");
                              //procesar companias, obtenido del XML
                              foreach (System.Xml.XmlElement xmlNodo in xmlLista)
                              {
                                    flagEncontrado=false;
                                    i = 0;
                                    //obtener compania
                                    k++;
                                    System.Xml.XmlNodeList xmlCodigo = xmlNodo.GetElementsByTagName("CodigoAerolinea");
                                    string strCodigo = xmlCodigo[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlCodAerolinea = xmlNodo.GetElementsByTagName("CodigoInterno");
                                    string strCodigoAerolinea = INTZ_Util.Completar(xmlCodAerolinea[0].InnerText.Trim(),"0");

                                    System.Xml.XmlNodeList xmlCodSAP = xmlNodo.GetElementsByTagName("CodigoSAP");
                                    string strCodigoSAP = INTZ_Util.Completar(xmlCodSAP[0].InnerText.Trim(),"0");

                                    System.Xml.XmlNodeList xmlnNombre = xmlNodo.GetElementsByTagName("Nombre");
                                    string strNombre = xmlnNombre[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnRuc = xmlNodo.GetElementsByTagName("Ruc");
                                    string strRuc = xmlnRuc[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnCodigoOACI = xmlNodo.GetElementsByTagName("CodigoOACI");
                                    string strCodigoOACI = xmlnCodigoOACI[0].InnerText.Trim();

                                    System.Xml.XmlNodeList xmlnCodigoIATA = xmlNodo.GetElementsByTagName("CodigoIATA");
                                    string strCodigoIATA = xmlnCodigoIATA[0].InnerText.Trim();

                                  
                                    //Buscar Compania
                                    while (!flagEncontrado && i<Lst_Compania.Count )
                                    {
                                          if (INTZ_Util.Compara(strCodigo, Lst_Compania[i].SCodEspecialCompania) ||
                                              INTZ_Util.Compara(strCodigoAerolinea,Lst_Compania[i].SCodAerolinea) ||
                                              INTZ_Util.Compara(strNombre,Lst_Compania[i].SDscCompania) ||
                                              INTZ_Util.Compara(strCodigoSAP, Lst_Compania[i].SCodSAP) ||
                                              INTZ_Util.Compara(strCodigoOACI, Lst_Compania[i].SCodOACI) ||
                                              INTZ_Util.Compara(strCodigoIATA, Lst_Compania[i].SCodIATA) ||

                                              (strCodigo.Length<1)          ||
                                              (strCodigoAerolinea.Length<1) ||
                                              (strNombre.Length<1)          ||
                                              (strCodigoIATA.Length<1) 
                                            )
                                          {
                                                flagEncontrado=true;
                                                break;
                                          }
                                          i++;
                                    }
                                    if (!flagEncontrado)
                                    {
                                          objCompania = new Compania();
                                          objCompania.SCodEspecialCompania = strCodigo;
                                          objCompania.SDscRuc = strRuc;
                                          objCompania.SDscCompania = strNombre;
                                          objCompania.STipCompania = INTZ_Define.Cod_TipAerolinea;
                                          objCompania.STipEstado = INTZ_Define.Cod_TipEstado;
                                          objCompania.DtFchCreacion = DateTime.Now;
                                          objCompania.SLogUsuarioMod = INTZ_Define.Cod_UServicio;
                                          objCompania.SCodSAP = strCodigoSAP;
                                          objCompania.SCodAerolinea = strCodigoAerolinea;
                                          objCompania.SCodIATA = strCodigoIATA;
                                          objCompania.SCodOACI = strCodigoOACI;
                                          long number;
                                          bool canConvert = long.TryParse(strRuc.Trim(), out number);
                                          //RUC de 11 digitos y verifica que no se inserte doble codigo especial 
                                          if (((canConvert && strRuc.Trim().Length==11)|| (strRuc.Trim().Length<1)) &&
                                             (!BuscaCompania(strCodigo, strCodigoAerolinea, strCodigoSAP, strNombre, strRuc, strCodigoOACI, strCodigoIATA))
                                              ) 
                                                Lst_CompaniaIns.Add(objCompania);
                                    }
                              }
                        }

                        int j = 0;
                        //using (System.Transactions.TransactionScope scope =
                        //            new System.Transactions.TransactionScope())
                        //{
                              //Insertar nuevas companias
                              foreach (Compania obj in Lst_CompaniaIns)
                              {
                                    Obj_DAOCompania.Cod_Modulo = INTZ_Define.Cod_MServicio;
                                    Obj_DAOCompania.Cod_Sub_Modulo = INTZ_Define.Cod_ICompania;
                                    Obj_DAOCompania.insertar(obj);
                                    j++;
                              }
                              ////scope.Complete();
                              //INTZ_Util.EscribirLog(this, "Servicio Interfaz Compañia - Leidas del Web Service:" + k.ToString());
                              //INTZ_Util.EscribirLog(this, "Servicio Interfaz Compañia - Ingresadas como nuevas:"+j.ToString());
                              INTZ_Util.EscribirLog(this, "Servicio Interfaz Compañia - Terminado.");
                        //}
                         Lst_CompaniaIns.Clear();
                  }
                  catch (Exception ex)
                  {
                      Flg_Error = 1;
                        //Obtener Ip
                        IPHostEntry IPs = Dns.GetHostByName("");
                        IPAddress[] Direcciones = IPs.AddressList;
                        String IpClient = Direcciones[0].ToString();
                        //GeneraAlarma
                        GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000059", "S01", IpClient, "3", "Alerta W0000059", "Error asociado al proceso de cargar Aerolineas al TUUA, Error: " + ex.Message, INTZ_Define.Cod_UServicio);

                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                        string strSTrace = "";
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

            private bool BuscaCompania(string strCodEspecial)
            {
                  int i = 0;
                  while (i < Lst_CompaniaIns.Count)
                  {
                        if (strCodEspecial.Trim().ToUpper().Equals(Lst_CompaniaIns[i].SCodEspecialCompania.Trim().ToUpper())
                             
                           )
                        {
                              return true;
                        }
                        i++;
                  }
                  return false;
            }

            private bool BuscaCompania(string strCodigo, string strCodigoAerolinea, string strCodigoSAP,string  strNombre, 
                                       string strRuc, string strCodigoOACI, string strCodigoIATA)
            {
                  int i = 0;
                  while (i < Lst_CompaniaIns.Count)
                  {
                        if (INTZ_Util.Compara(strCodigo,Lst_CompaniaIns[i].SCodEspecialCompania) ||
                            INTZ_Util.Compara(strCodigoAerolinea,Lst_CompaniaIns[i].SCodAerolinea)      ||
                            INTZ_Util.Compara(strCodigoSAP,Lst_CompaniaIns[i].SCodSAP)                  ||
                            INTZ_Util.Compara(strCodigoOACI,Lst_CompaniaIns[i].SCodOACI)                ||  
                            INTZ_Util.Compara(strCodigoIATA,Lst_CompaniaIns[i].SCodIATA)                ||
                            INTZ_Util.Compara(strNombre,Lst_CompaniaIns[i].SDscCompania)
                           )
                        {
                              return true;
                        }
                        i++;
                  }
                  return false;
            }


            public void EjecutarServicio()
            {
                  try
                  {
                      Flg_Error = 0;
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOCompania = new DAO_Compania(INTZ_Define.Dsc_SPConfig);
    
                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);

                        Lst_ParameGeneral = Obj_DAOParameGeneral.listar();

                        string strURLWebService = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "WN");
                        string strTimeOut = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "TN");
                        string strMetodoRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "DN");
                        string strServicioRemoto = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "SN");
                        string strProtocoloSoap = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "PN");

                        HT_Propiedades = new Hashtable();
                        HT_Propiedades.Add("WN", strURLWebService);
                        HT_Propiedades.Add("TN", strTimeOut);
                        HT_Propiedades.Add("DN", strMetodoRemoto);
                        HT_Propiedades.Add("SN", strServicioRemoto);
                        HT_Propiedades.Add("PN", strProtocoloSoap);


                        if (Int32.Parse((string)HT_Propiedades["TN"]) > 0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ObtenerCompaniaWS);
                              tmTasaCambio.Interval = 1000 * Int32.Parse((string)HT_Propiedades["TN"]);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Interfaz Compañia - Iniciado###");
                        }
                  }
                  catch (Exception e)
                  {
                      Flg_Error = 2;
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = e.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia>1)
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
