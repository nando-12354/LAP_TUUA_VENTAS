using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.DEPURACION
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
				new SVC_Depuracion() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
