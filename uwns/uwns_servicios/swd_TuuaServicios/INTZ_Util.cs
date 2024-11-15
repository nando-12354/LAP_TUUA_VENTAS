using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Design.TraceListeners;
using LAP.TUUA.ENTIDADES;

using System.Globalization;
using System.Configuration;


namespace LAP.TUUA.SERVICIOS
{
      public static class INTZ_Util
      {

          public static void EscribirLog(Object objClase, String strMensaje)
          {
              LogEntry objLog = new LogEntry();
              //Monitor.Enter(objLog);
              objLog.ProcessName = objClase.ToString();
              objLog.Message = strMensaje;
              Logger.Write(objLog);
              //Monitor.Exit(objLog);
          }

          /// <summary>
          /// Escribe en archivo log
          /// </summary>
          /// <param name="objClase">Instancia de Clase</param>
          /// <param name="strMensaje">Mensaje</param>
          //public static void EscribirLog2(Object objClase, String strMensaje)
          //{
          //    try
          //    {
          //        SetTraceLogPath();
          //        LogEntry leMsg = new LogEntry();
          //        string hora = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff");
          //        leMsg.Title = hora;
          //        leMsg.ProcessName = objClase.ToString();
          //        leMsg.Message = strMensaje;
          //        Logger.Write(leMsg);
          //    }
          //    catch (Exception)
          //    {
          //        //throw;
          //    }
          //}

          //public static void SetTraceLogPath()
          //{
          //    CultureInfo culturaPeru = new CultureInfo("es-PE");
          //    System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
          //    string ConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + "log/";
          //    string strFecha = DateTime.Now.ToShortDateString();
          //    //string strFileName = "";
          //    strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
          //    //ConfigurationFileMap objConfigPath = new ConfigurationFileMap();
          //    //objConfigPath.MachineConfigFilename = ConfigFilePath;

          //    Configuration entLibConfig = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
          //    LoggingSettings loggingSettings = (LoggingSettings)entLibConfig.GetSection(LoggingSettings.SectionName);

          //    FlatFileTraceListenerData objFlatFileTraceListenerData = loggingSettings.TraceListeners.Get("Flat File Destination") as FlatFileTraceListenerData;
          //    //strFileName = objFlatFileTraceListenerData.FileName;
          //    //strFileName = strFileName.Substring(0, strFileName.Length - 4) + strFecha + strFileName.Substring(strFileName.Length - 4);
          //    objFlatFileTraceListenerData.FileName = ConfigFilePath + "Servicios" + strFecha + ".log";
          //    //objFlatFileTraceListenerData.FileName = strFileName;
          //    entLibConfig.Save();
          //}

          ///// <summary>
          ///// Escribe en archivo log
          ///// </summary>
          ///// <param name="objClase">Instancia de Clase</param>
          ///// <param name="strMensaje">Mensaje</param>
          //public static void EscribirLog(Object objClase, String strMensaje)
          //{
          //    try
          //    {

          //        //string strPath = AppDomain.CurrentDomain.BaseDirectory + "swd_TuuaServicios.exe.config";
          //        //System.Xml.XmlDocument xml = new System.Xml.XmlDocument();
          //        //xml.Load(strPath);
          //        //System.Xml.XmlNode xmlPath = xml.SelectSingleNode("//configuration/loggingConfiguration/listeners").FirstChild;
          //        //string strFolder = xmlPath.Attributes["fileName"].Value;


          //        LogEntry leMsg = new LogEntry();
          //        //string hora = DateTime.Now.TimeOfDay.ToString();
          //        //if (hora != null && hora.Length > 11)
          //        //leMsg.Title = hora.Substring(0, 12);
          //        //leMsg.ProcessName = objClase.ToString();
          //        //leMsg.Message = strMensaje;
          //        //Logger.Write(leMsg);


          //        string hora = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff");
          //        leMsg.Title = hora;
          //        leMsg.ProcessName = objClase.ToString();
          //        leMsg.Message = strMensaje;
          //        Logger.Write(leMsg);



          //        //string archivo = "SVC_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
          //        //INTZ_Log log = new INTZ_Log();
          //        //LogWriter MyLogWriter = log.GetLogWriter(AppDomain.CurrentDomain.BaseDirectory +"log/" +archivo);
          //        //LogEntry logEntry = new LogEntry();
          //        //string hora = DateTime.Now.TimeOfDay.ToString();

          //        //if (hora !=null &&  hora.Length>0)
          //        //      logEntry.Title = hora.Substring(0, 12);

          //        //logEntry.ProcessName = objClase.ToString();
          //        //logEntry.Message = strMensaje;
          //        //MyLogWriter.Write(logEntry);
          //        //MyLogWriter.Dispose();


          //    }
          //    catch (Exception)
          //    {
          //        throw;
          //    }
          //}

            /// <summary>
            /// Obtiene Valor segun identificador
            /// </summary>
            /// <param name="lstParameGeneral">Tabla Parametro General</param>
            /// <param name="strID">Id Parametro General</param>
            /// <returns>Valor</returns>
            public static string ObtenerParamGral(List<ParameGeneral> lstParameGeneral, string strID) 
            {
                  string strRpta="";
                  foreach (ParameGeneral obj in lstParameGeneral)
                  {
                        if (  strID.Equals(obj.SIdentificador)  )
                        {
                              strRpta = obj.SValor;
                              break;
                        }
                  }
                  return strRpta;
            }

            public static string Completar(string valor, string caracter)
            {
                  long val=0;
                  bool bValor = long.TryParse(valor.Trim(),out val);
                  while (valor.Trim().Length > 0 && valor.Trim().Length < 10 && bValor)
                  {
                        valor = caracter + valor;
                  }

                  if (bValor == false)
                        valor = null;
                  return valor;
            }

            public static bool Compara(string strVal1, string strVal2)
            {
                  if (strVal1 != null)
                  {
                        if (strVal2 != null)
                        {
                              if (strVal1.Trim().Equals(strVal2.Trim()) && strVal1.Length>0)
                                    return true;
                              else
                                    return false;
                        }
                        else 
                        {
                              return false;
                        }
                  }

                  if (strVal1 == null && strVal2 == null) 
                  {
                             return false;
                  }

                  return false;
            }

            
      }
}
