using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;


namespace LAP.TUUA.DEPURACION
{
    [RunInstaller(true)]
    public partial class SVC_Installer : Installer
    {
        private ServiceInstaller serviceInstaller1;
        private ServiceProcessInstaller processInstaller;
        public SVC_Installer()
        {
            InitializeComponent();
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller1 = new ServiceInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller1.StartType = ServiceStartMode.Manual;

            serviceInstaller1.ServiceName = "TUUADepuracion";
            Installers.Add(serviceInstaller1);
            Installers.Add(processInstaller);
        }
    }
}
