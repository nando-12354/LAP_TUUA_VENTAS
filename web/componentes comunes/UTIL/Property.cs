using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using System.Xml;

namespace LAP.TUUA.UTIL
{
    public static class Property
    {
        public static Hashtable htProperty;
        public static DataTable dtMenu;
        public static DataTable dtMapSite;
        public static Hashtable htSPConfig;
        public static Hashtable htParametro;
        public static string strUsuario;
        public static string strModulo;
        public static string strSubModulo;
        public static Hashtable htListaCampos;

        //EAG
        public static String puertoSticker = null;
        public static String puertoVoucher = null;
        //EAG

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

        public static void cargarSPConfig(string sConfigSPPath)
        {
            XmlDocument xDoc = new XmlDocument();
            htSPConfig = new Hashtable();
            if (CargarXMLDoc(sConfigSPPath, xDoc))
            {
                XmlNodeList xnProcedures = xDoc.GetElementsByTagName("procedures");
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
                    htSPConfig.Add(sAlias, hsProcedure);
                }
            }
        }

        private static bool CargarXMLDoc(string sConfigSPPath, XmlDocument xDoc)
        {
            try
            {
                ErrorHandler.Flg_Error = false;
                xDoc.Load(sConfigSPPath + "/resources/spconfig.xml");
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_002;
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_002])["MESSAGE"];
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace(Define.VENTAS, strMessage);
                return false;
            }
            return true;
        }

    }

}
