﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace LAP.TUUA.SERVICIOS
{
      static class SVC_Main
      {
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            static void Main()
            {
                  ServiceBase[] ServicesToRun;
                  ServicesToRun = new ServiceBase[] 
			{ 
				new SVC_Base() 
			};
                  ServiceBase.Run(ServicesToRun);
            }
      }
}