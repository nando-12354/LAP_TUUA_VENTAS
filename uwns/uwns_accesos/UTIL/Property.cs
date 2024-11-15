using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LAP.TUUA.UTIL
{
    public static class Property
    {
        public static Hashtable htProperty;
        public static DataTable dtMenu;
        public static DataTable dtMapSite;
        public static string Dsc_Path;

        public static string strUsuario;
        public static string strModulo;
        public static string strSubModulo;


        //public static bool bConRemota;
        //public static bool bConLocal;
        //public static IDataReader IDRMolinete;
        public static Hashtable shtSPConfig;
        //public static Hashtable shtMolinete;
        public static XmlDocument sxDoc;

        //public static Database shelper;
        //public static Database shelperLocal; 
        //public static bool estadoLector ;
        //public static bool estadoPinPad ;
        //public static bool estadoMolinete;


        public static bool CargarPropiedades(string strPath)
        {
            string linea;
            htProperty = new Hashtable();
            try
            {
                string PathLabel =strPath+"config.properties";
                StreamReader sr = new StreamReader(PathLabel, System.Text.Encoding.UTF7);//Si no pongo este ultimo parametro no me muestra las tildes.
                while ((linea = sr.ReadLine()) != null)
                {
                    char[] sep = { '=' };
                    string[] v = linea.Split(sep, 2);
                    if (v.Length > 1)
                    {
                        string campo = v[0].Trim();
                        if (campo.Length == 0 || (campo.Length > 0 && campo.ToCharArray()[0].Equals('#')))
                        {
                            continue;
                        }
                        string valor = v[1].TrimEnd();
                        htProperty.Add(campo, valor);
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_001])["MESSAGE"];
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace(Define.VENTAS,strMessage);
                return false;
            }
            return true;
        }

        public static void scargarSPConfig(string sConfigSPPath)
        {
              shtSPConfig = new Hashtable();
              if (sCargarXMLDoc(sConfigSPPath))
              {
                    XmlNodeList xnProcedures = sxDoc.GetElementsByTagName("procedures");
                    XmlNodeList lista = ((XmlElement)xnProcedures[0]).GetElementsByTagName("SP");
                    foreach (XmlElement nodo in lista)
                    {
                          string sNombre = nodo.GetAttribute("nombre");
                          string sAlias = nodo.GetAttribute("alias");
                          XmlNodeList nArgumentos = ((XmlElement)nodo.GetElementsByTagName("argumentos")[0]).GetElementsByTagName("argumento");
                          Hashtable hsProcedure = new Hashtable();
                          hsProcedure.Add(sAlias, sNombre);
                          foreach (XmlElement subnodo in nArgumentos)
                          {
                                string sArgNombre = subnodo.GetAttribute("nombre");
                                string sAliasArg = subnodo.GetAttribute("alias");
                                hsProcedure.Add(sAliasArg, sArgNombre);
                          }
                          shtSPConfig.Add(sAlias, hsProcedure);
                    }
              }
        }

        private static bool sCargarXMLDoc(string sConfigSPPath)
        {
              sxDoc = new XmlDocument();
              try
              {
                    ErrorHandler.Flg_Error = false;
                    sxDoc.Load(sConfigSPPath + "/resources/spconfig.xml");
              }
              catch (Exception)
              {
                    ErrorHandler.Cod_Error = Define.ERR_002;
                    string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_002])["MESSAGE"];
                    ErrorHandler.Desc_Mensaje = strMessage;
                    ErrorHandler.Trace(Define.VENTAS, strMessage);
                    return false;
              }
              return true;
        }

        //public static void scargarMolinete()
        //{
        //      shtMolinete = new Hashtable();
        //      shtMolinete.Add("Cod_Molinete", "");
        //      shtMolinete.Add("Dsc_Ip", "");
        //      shtMolinete.Add("Dsc_Molinete", "");
        //      shtMolinete.Add("Tip_Documento", "");
        //      shtMolinete.Add("Tip_Acceso", "");
        //      shtMolinete.Add("Tip_Estado", "");
        //      shtMolinete.Add("Est_Master", "");
        //      shtMolinete.Add("Dsc_DBName", "");
        //      shtMolinete.Add("Dsc_DBUser", "");
        //      shtMolinete.Add("Dsc_DBPassword", "");
        //      shtMolinete.Add("Tip_Vuelo", "");
        //      shtMolinete.Add("Tip_Molinete", "");
        //      shtMolinete.Add("Est_PinPad", "FALSE");

        //}

        

    }

}
