using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Linq;


namespace LAP.TUUA.LOGS
{
    [RunInstaller(true)]
    public partial class SVC_Installer : Installer
    {
        public SVC_Installer()
        {
            InitializeComponent();
        }
    }
}
