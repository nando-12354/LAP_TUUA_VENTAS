using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.SINCRONIZADOR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new SVC_Sincronizer() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
