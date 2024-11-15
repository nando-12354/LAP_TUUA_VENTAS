using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection;

using Hiper.Net.Utilidades.Archivos;

namespace SrvEnvioCorreo
{
    /// <summary>
    /// Clase del Servicio de Asignación de Perfiles de Atención
    /// </summary>
    /// <Version>4.2.6.0</Version>
    /// <Autor>James Jayo</Autor>
    /// <Copyright>Copyright ( Copyright © HIPER S.A. )</Copyright>
    public partial class SrvEnvioCorreo : ServiceBase
    {
        private GestorCorreoBL oGestorCorreoBL; //Instancia del Modelo Inteligente
        private LogFile oLog; //Instancia de la clase utilitaria para escribir logs

        /// <summary>
        /// Contructor de la clase del Servicio.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        public SrvEnvioCorreo()
        {
            InitializeComponent();

            oLog = new LogFile(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Log", "SrvEnvioCorreo");
            oLog.WriteLine("=======================================================================================");
            oLog.WriteLine("Procediendo a iniciar el 'Servicio de envío de correo'");
            oLog.WriteLine("=======================================================================================");
        }

        /// <summary>
        /// Metodo que controla el evento de Inicio del Servicio.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        /// <param name="args">Parámetro que crea por defecto el Servicio.</param>
        protected override void OnStart(string[] args)
        {
            oGestorCorreoBL = new GestorCorreoBL();

            if (oGestorCorreoBL.ejecutarGestorCorreo())
            {
                oLog.WriteLine("Se inició el 'Servicio de envío de correo'");   
            }
            else
            {
                oLog.WriteLine("Se detendrá el 'Servicio de envío de correo'");
                this.Stop();
            }
        }

        /// <summary>
        /// Método que controla el evento de Finalización del Servicio.
        /// </summary>
        /// <Autor>J.Jayo</Autor>
        /// <FechaCreacion>21/08/2015</FechaCreacion>
        protected override void OnStop()
        {
            oLog.WriteLine("Deteniendo el 'Servicio de envío de correo'");
            oGestorCorreoBL.detenerGestorCorreo();
            oLog.WriteLine("Se detuvo el 'Servicio de envío de correo'");
        }
        
    }
}
