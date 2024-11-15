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
    public class Log
    {
        public void Trace(string strTitulo, string strMessage)
        {
            Monitor.Enter(this);
            SetTraceLogPath();
            LogEntry objLogEntry = new LogEntry();
            objLogEntry.TimeStamp = DateTime.Now;
            objLogEntry.Title = strTitulo;
            objLogEntry.Message = strMessage;
            Logger.Write(objLogEntry);
            Monitor.Exit(this);
        }

        public void SetTraceLogPath()
        {
            string ConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + "log/";
            string strFecha = DateTime.Now.ToShortDateString();
            try
            {
                //string strFileName = "";
                strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                //ConfigurationFileMap objConfigPath = new ConfigurationFileMap();
                //objConfigPath.MachineConfigFilename = ConfigFilePath;

                Configuration entLibConfig = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                LoggingSettings loggingSettings = (LoggingSettings)entLibConfig.GetSection(LoggingSettings.SectionName);

                FlatFileTraceListenerData objFlatFileTraceListenerData = loggingSettings.TraceListeners.Get("FlatFile TraceListener") as FlatFileTraceListenerData;
                //strFileName = objFlatFileTraceListenerData.FileName;
                //strFileName = strFileName.Substring(0, strFileName.Length - 4) + strFecha + strFileName.Substring(strFileName.Length - 4);
                objFlatFileTraceListenerData.FileName = ConfigFilePath + "Sincroniza" + strFecha + ".log";
                //objFlatFileTraceListenerData.FileName = strFileName;
                entLibConfig.Save();
            }
            catch { }
        }
    }
}
