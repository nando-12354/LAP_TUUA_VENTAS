using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace SrvEnvioCorreo
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.SrvEnvioCorreo.DisplayName = "TUUA - Servicio de envío de correo - Versión: 1.0.0.0";
        }
    }
}
