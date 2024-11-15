using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace LAP.TUUA.ACCESOS
{
      [RunInstaller(true)]
      public partial class ACS_Installer : Installer
      {
            private ServiceInstaller serviceInstaller1;
            private ServiceProcessInstaller processInstaller; 
            public ACS_Installer()
            {
                  InitializeComponent();

                  processInstaller = new ServiceProcessInstaller();
                  serviceInstaller1 = new ServiceInstaller();

                  processInstaller.Account = ServiceAccount.LocalSystem;

                  serviceInstaller1.StartType = ServiceStartMode.Manual;

                  serviceInstaller1.ServiceName = "TUUAAccesos";
                  Installers.Add(serviceInstaller1);
                  Installers.Add(processInstaller);
            }
      }
}
