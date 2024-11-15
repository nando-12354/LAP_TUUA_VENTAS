using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Threading;

namespace LAP.TUUA.UTIL
{
    public static class Property
    {
        public static Hashtable htProperty;
        public static DataTable dtMenu;
        public static DataTable dtMapSite;

        public static bool 
            CargarPropiedades(string strPath)
        {
            string linea;
            htProperty = new Hashtable();
            try
            {
                string PathLabel = strPath + "Depuración.ini";
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
                string strMessage ="Error en configuracion Archivo INI.";
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace("ERROR",e);
                return false;
            }
            return true;
        }

        public static void Trace(string strTitulo, string strMessage)
        {
            LogEntry objLog = new LogEntry();
            //Monitor.Enter(objLog);
            objLog.Title = strTitulo;
            objLog.Message = strMessage;
            Logger.Write(objLog);
            //Monitor.Exit(objLog);
        }
    }

}
