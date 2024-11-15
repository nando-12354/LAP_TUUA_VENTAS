using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace LAP.TUUA.UTIL
{
    public static class ErrorHandler
    {
        public static Hashtable htErrorType;
        public static string Desc_Mensaje;
        public static bool Flg_Error;
        public static string Cod_Error;
        public static string Desc_Info;


        public static bool CargarErrorTypes(string strPath)
        {

            XmlDocument xDoc = new XmlDocument();
            if (LoadXMLFile(strPath, xDoc))
            {
                try
                {
                    XmlNodeList xnProcedures = xDoc.GetElementsByTagName("ERRORTYPES");
                    XmlNodeList lista = ((XmlElement)xnProcedures[0]).GetElementsByTagName("ERRORTYPE");
                    Hashtable htErrorTypes = new Hashtable();
                    foreach (XmlElement nodo in lista)
                    {
                        string strCode = ((XmlElement)nodo.GetElementsByTagName("CODE")[0]).InnerText;
                        string strName = ((XmlElement)nodo.GetElementsByTagName("NAME")[0]).InnerText;
                        string strMessage = ((XmlElement)nodo.GetElementsByTagName("MESSAGE")[0]).InnerText;
                        Hashtable hsProcedure = new Hashtable();
                        hsProcedure.Add("NAME", strName);
                        hsProcedure.Add("MESSAGE", strMessage);
                        htErrorTypes.Add(strCode, hsProcedure);
                    }
                    htErrorType = htErrorTypes;
                    Flg_Error = false;
                    return true;
                }
                catch
                {
                    Desc_Mensaje = Define.ERR_FILEERRORTYPE;
                    Flg_Error = true;
                    return false;
                }
            }
            return false;
        }

        public static void Trace(string strTitulo, string strMessage)
        {
            LogEntry objLogEntry = new LogEntry();
            objLogEntry.TimeStamp = DateTime.Now;
            objLogEntry.Title = strTitulo;
            objLogEntry.Message = strMessage;
            Flg_Error = true;
            Logger.Write(objLogEntry);
        }

        public static void Trace(string strTitulo,Exception ex)
        {
            string strMessage = "";
            int intOcurrencia = ex.StackTrace.IndexOf("LAP");
            String strSTrace = ex.StackTrace.Substring(intOcurrencia);
            intOcurrencia = strSTrace.IndexOf(")");
            strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
            strMessage = "[" + strSTrace + "] " + ex.Message;
            LogEntry objLogEntry = new LogEntry();
            objLogEntry.TimeStamp = DateTime.Now;
            objLogEntry.Title = strTitulo;
            objLogEntry.Message = strMessage;
            Flg_Error = true;
            Logger.Write(objLogEntry);
        }

        public static string ObtenerCodigoExcepcion(string strName)
        {
            switch (strName)
            {
                case "ArgumentException":
                    Cod_Error = Define.ERR_007;
                    return Define.ERR_007;
                case "SqlException":
                    Cod_Error = Define.ERR_006;
                    return Define.ERR_006;
                case "InvalidOperationException":
                    Cod_Error = Define.ERR_005;
                    return Define.ERR_005;
                default:
                    Cod_Error = Define.ERR_000;
                    return Define.ERR_000;
            }
        }

        private static bool LoadXMLFile(string strPath, XmlDocument xDoc)
        {
            try
            {
                xDoc.Load(strPath + "/ErrorType.xml");
                return true;
            }
            catch
            {
                Desc_Mensaje = Define.ERR_FILEERRTYPENOTFOUND;
                Flg_Error = true;
                return false;
            }
        }

        /// <summary>
        /// Escribe en archivo log
        /// </summary>
        /// <param name="objClase">Instancia de Clase</param>
        /// <param name="strMensaje">Mensaje</param>
        public static void EscribirLog(Object objClase, String strMensaje)
        {
            try
            {
                LogEntry leMsg = new LogEntry();
                string hora = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff");
                leMsg.Title = hora;
                leMsg.ProcessName = objClase.ToString();
                leMsg.Message = strMensaje;
                Logger.Write(leMsg);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
