using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Threading;
using System.Data;
using System.Data.Common;
using System.ServiceProcess;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Net;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.IO;

namespace LAP.TUUA.LOGS
{
    /// <summary>
    /// ACCESOS: Controlador de Dispositivos
    /// </summary>
    public class ACS_Controlador
    {
        private Hashtable Arr_PtoLectorConfig;
        private ACS_Util Obj_Util;
        public ACS_Resolver Obj_Resolver;
        private Thread thrLogs;
        private ACS_FormLogs frmLogs;

        /// <summary>
        /// Constructor
        /// </summary>
        public ACS_Controlador()
        {
            try
            {
                this.Arr_PtoLectorConfig = new Hashtable();
                this.Obj_Util = new ACS_Util();
                this.Obj_Resolver = new ACS_Resolver(Obj_Util);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        /// <summary>
        /// Inicia puertos del Lector y PinPad
        /// </summary>
        /// <returns>Estado de inicio de puertos</returns>

        public int IniciarConexiones()
        {
            try
            {
                //Iniciar SPs
                Obj_Util.EscribirLog(this, "Cargando Configuraciones...");
                Property.scargarSPConfig(AppDomain.CurrentDomain.BaseDirectory);

                //Carga Propiedades Generales
                Property.CargarPropiedades(AppDomain.CurrentDomain.BaseDirectory + "resources/");

                //Validacion tiempo=0
                int tiempo = Convert.ToInt32((string)Property.htProperty["TIEMPO_PROCESAMIENTO"]);

                if (tiempo > 0)
                {
                    //Carga Manual
                    bool carga_manual = Convert.ToBoolean((string)Property.htProperty["CARGA_MANUAL"]);
                    if (carga_manual)
                    {
                        Obj_Util.EscribirLog(this, "Carga Manual...");
                        VerificarConexion(null, null);
                    }
                    else
                    {
                        try
                        {
                            System.Timers.Timer Temporizador = new System.Timers.Timer();
                            Temporizador.Interval = 1000 * Convert.ToInt32((string)Property.htProperty["TIEMPO_PROCESAMIENTO"]);
                            Temporizador.Elapsed += new System.Timers.ElapsedEventHandler(VerificarConexion);
                            Temporizador.Start();
                        }
                        catch (Exception ex)
                        {
                            Obj_Util.EscribirLog(this, ex.Message);
                        }
                    }
                }
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void IniciarLectura(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (ACS_Property.BConexion)
                {
                    Obj_Util.EscribirLog(this, "Iniciando Lectura... ");

                    thrLogs = new Thread(new ThreadStart(run));
                    thrLogs.Start();
                }
                else
                {
                    Obj_Util.EscribirLog(this, "Conexion no establecida ");
                }
            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
            }
        }

        private void VerificarConexion(object source, System.Timers.ElapsedEventArgs e)
        {
            string sqlQuery = "select getdate()";
            DAO_BaseDatos objDAO_BaseDatos;
            IDataReader reader = null;
            try
            {
                if (ACS_Property.shelperLocal == null)
                {
                    objDAO_BaseDatos = new DAO_BaseDatos();
                    objDAO_BaseDatos.Conexion("tuuacnx");
                    ACS_Property.shelper = objDAO_BaseDatos.helper;
                }

                string aa = ACS_Property.shelper.ConnectionString;

                reader = ACS_Property.shelper.ExecuteReader(CommandType.Text, sqlQuery);
                reader.Close();
                if (!ACS_Property.BConexion)
                    Obj_Util.EscribirLog(this, "Conexion Activa ");
                ACS_Property.BConexion = true;

                //INICIAMOS EL PROCESAMIENTO DE DATOS
                IniciarLectura(null, null);
            }
            catch (Exception ex)
            {
                if ((ACS_Property.BConexion) || source == null)
                    Obj_Util.EscribirLog(this, "Conexion Inactiva ");
                ACS_Property.BConexion = false;
            }
        }
        public void Close()
        {
            try
            {
                frmLogs.Close();
                thrLogs.Abort();
            }
            catch (Exception)
            {

            }
        }

        public void run()
        {
            Application.ThreadException +=
            new ThreadExceptionEventHandler(excepcion);

            frmLogs = new ACS_FormLogs(Obj_Resolver);
            Application.Run(frmLogs);
        }

        public void excepcion(object sender,
        ThreadExceptionEventArgs excepcion)
        {
            MessageBox.Show("Se ha producido un error");
        }

    }
}
