using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;

namespace LAP.TUUA.SERVICIOS
{
      class INTZ_Log
      {
            public INTZ_Log()
            { 
            }

            public LogWriter GetLogWriter(string LogPath)  
            {
                  try
                  {
                        TextFormatter formatter = new TextFormatter ("{title}  {processName}   {message}");
                        FlatFileTraceListener listener =new FlatFileTraceListener(LogPath,"","",formatter);
                        LogSource mainLogSource = new LogSource("MainLogSource", SourceLevels.All);
                        mainLogSource.Listeners.Add(listener);
                        LogSource unusedLogSource = new LogSource("Empty");
                        IDictionary<string, LogSource> traceSources = new Dictionary<string, LogSource>();
                        traceSources.Add("Error", mainLogSource);
                        traceSources.Add("Debug", mainLogSource);
                        System.Collections.Generic.List<Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter> filterCollection = new System.Collections.Generic.List<Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter>();
                        Microsoft.Practices.EnterpriseLibrary.Logging.Filters.ILogFilter filter = new Microsoft.Practices.EnterpriseLibrary.Logging.Filters.PriorityFilter("defaultFilter", 0);
                        filterCollection.Add(filter);
                        LogWriter _writer = new LogWriter(filterCollection,traceSources,unusedLogSource,unusedLogSource,mainLogSource,"Error",false,true);
                        return _writer;
                  }
                  catch( Exception e)
                  {
                        throw;
                  }
            }
      }


}
