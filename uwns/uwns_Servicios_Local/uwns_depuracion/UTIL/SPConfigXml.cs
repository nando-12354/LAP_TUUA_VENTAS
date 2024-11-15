using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;

namespace LAP.TUUA.UTIL
{
    public class SPConfigXml
    {
        private Hashtable htSPConfig;
        private XmlDocument xDoc;
        

        public SPConfigXml() {
            htSPConfig = new Hashtable();
        }

        public void cargarSPConfig(string sConfigSPPath)
        {
            if (CargarXMLDoc(sConfigSPPath))
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

        private bool CargarXMLDoc(string sConfigSPPath)
        {
            xDoc = new XmlDocument();
            try
            {
                ErrorHandler.Flg_Error = false;
                xDoc.Load(sConfigSPPath + "/resources/spconfig.xml");
            }
            catch(Exception ex){
                ErrorHandler.Cod_Error = Define.ERR_002;
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_002])["MESSAGE"];
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace(Define.VENTAS, strMessage);
                return false;
            }
            return true;
        }        

        public Hashtable HtSPConfig
        {
            get
            {
                return htSPConfig;
            }
            set
            {
                htSPConfig = value;
            }
        }

    }
}
