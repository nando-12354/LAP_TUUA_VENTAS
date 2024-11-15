using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;


namespace LAP.TUUA.SERVICIOS
{
      public class INTZ_ClienteWS 
      {
            protected DAO_ParameGeneral Obj_DAOParameGeneral;
            protected DAO_Usuario Obj_DAO_Usuario;
            protected List<ParameGeneral> Lst_ParameGeneral;


            protected  Hashtable HT_Propiedades;
         

            public INTZ_ClienteWS() 
            {
                  HT_Propiedades = new Hashtable();
            }
            public void CargarConfigXML()
            {
                  

                  System.Xml.XmlDocument xmldConfiguracion = new System.Xml.XmlDocument();
                  xmldConfiguracion.Load(INTZ_Define.Dsc_Config);

                  System.Xml.XmlNodeList xmlnlParametros = xmldConfiguracion.GetElementsByTagName("WT");
                  HT_Propiedades.Add(xmlnlParametros[0].Name, xmlnlParametros[0].InnerText);

                  xmlnlParametros = xmldConfiguracion.GetElementsByTagName("TT");
                  HT_Propiedades.Add(xmlnlParametros[0].Name, xmlnlParametros[0].InnerText);

                  xmlnlParametros = xmldConfiguracion.GetElementsByTagName("FT");
                  HT_Propiedades.Add(xmlnlParametros[0].Name, xmlnlParametros[0].InnerText);
            }



      }
}
