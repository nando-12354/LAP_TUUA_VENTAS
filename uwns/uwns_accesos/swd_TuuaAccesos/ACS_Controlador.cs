///File: ACS_Controlador.cs
///Proposito:Inicia,Carga y Ejecuta Interfaces de Lector y PinPad
///Metodos: 
///int Iniciar()
///int CargarFormatoBoarding()
///int IniciarPuertos()
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:15/07/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:20/07/2009

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
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
using LAP.TUUA.ALARMAS;
using System.Net;

using System.IO;



namespace LAP.TUUA.ACCESOS
{
    /// <summary>
    /// ACCESOS: Controlador de Dispositivos
    /// </summary>
    public class ACS_Controlador
    {
        private ACS_InterfazPinPad Obj_IntzPinPad;
        private ACS_InterfazLector Obj_IntzLector;
        private ACS_SincronizaLectura Obj_SincLectura;
        private Hashtable Arr_PtoLectorConfig;
        private ACS_SComPINPAD Obj_PinPad;
        private ACS_Util Obj_Util;
        private ACS_SCom Obj_SCom;
        private ACS_Resolver Obj_Resolver;
        private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
        private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitado;
        private ACS_FormContingencia frmContingencia;
        private Thread thrContingencia;
        private ACS_SinContingencia Obj_SincContingencia;

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
        /// Inicia Controlador del Lector y PinPad 
        /// </summary>
        /// <returns></returns>
        public int Iniciar()
        {
            try
            {
                //Inicia conexiones de SPs, BD, Lector, PINPAD, Molinete
                
                IniciarConexiones();

                Obj_SincLectura = new ACS_SincronizaLectura();
                Obj_IntzPinPad = new ACS_InterfazPinPad(Obj_SincLectura, Obj_PinPad, Obj_SCom, Obj_Resolver,
                                                        Obj_Molinete, Obj_MolineteDiscapacitado, frmContingencia);
                Obj_IntzLector = new ACS_InterfazLector(Obj_SincLectura, Arr_PtoLectorConfig, Obj_PinPad, Obj_IntzPinPad,
                                                        Obj_SCom, Obj_Resolver, Obj_Molinete, Obj_MolineteDiscapacitado,
                                                        frmContingencia, Obj_SincContingencia);


                if (ACS_Property.estadoPinPad)
                    IniciarTiemposPinPad();
                if (ACS_Property.shtMolinete["Tip_Estado"].ToString().Trim() == "A")
                {
                    Obj_Util.EscribirLog(this, "Iniciando ...Modulo Lector");
                    Obj_Util.EscribirMensaje("Iniciando ...Modulo Lector");

                    Obj_IntzLector.Obj_HiloInterfazLector.Start();

                    Obj_Util.EscribirLog(this, "Iniciando ...Modulo Pin Pad");
                    Obj_Util.EscribirMensaje("Iniciando ...Modulo Pin Pad");

                    Obj_Util.EscribirLog(this, "Iniciando ...Modulo Semaforo Molinete");
                    Obj_Util.EscribirMensaje("Iniciando ...Modulo Semaforo Molinete");

                    Obj_IntzPinPad.obj_HiloInterfazPinpad.Start();
                }
                else
                {
                    Obj_Util.EscribirLog(this, "Estado de Molinete INACTIVO " + ACS_Property.shtMolinete["Tip_Estado"].ToString().Trim());
                    Obj_Util.EscribirMensaje("Estado de Molinete INACTIVO " + ACS_Property.shtMolinete["Tip_Estado"].ToString().Trim());
                }
                return 1;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Carga Tiempo de formularios del PINPAD
        /// </summary>
        private void IniciarTiemposPinPad()
        {
            Property.CargarPropiedades(ACS_Define.Dsc_SPConfig + "resources/");
            string strT1 = "MNUPRINCIP=" + (string)Property.htProperty["MNUPRINCIP"] + ";" +
                           "DATAAUTHENTICAT=" + (string)Property.htProperty["DATAAUTHENTICAT"] + ";" +
                           "MNUMODACCESO=" + (string)Property.htProperty["MNUMODACCESO"] + ";" +
                           "MNUMODACCESO2=" + (string)Property.htProperty["MNUMODACCESO2"] + ";" +

                           "DATACONTICKET=" + (string)Property.htProperty["DATACONTICKET"] + ";" +
                           "CONTKRESUMEN=" + (string)Property.htProperty["CONTKRESUMEN"] + ";" +
                           "CONTKERROR=" + (string)Property.htProperty["CONTKERROR"] + ";" +

                           "DATAREGTICKET=" + (string)Property.htProperty["DATAREGTICKET"] + ";" +
                           "REGTKRESUMEN=" + (string)Property.htProperty["REGTKRESUMEN"] + ";" +
                           "REGTKOK=" + (string)Property.htProperty["REGTKOK"] + ";" +
                           "REGTKERROR=" + (string)Property.htProperty["REGTKERROR"] + ";" +

                           "DATACON_FECHA=" + (string)Property.htProperty["DATACON_FECHA"] + ";" +
                           "DATACON_NASIE=" + (string)Property.htProperty["DATACON_NASIE"] + ";" +
                           "DATACON_NVUELO=" + (string)Property.htProperty["DATACON_NVUELO"] + ";" +
                           "DATACON_PASAJ=" + (string)Property.htProperty["DATACON_PASAJ"] + ";" +
                           "CONBPRESUMEN=" + (string)Property.htProperty["CONBPRESUMEN"] + ";" +
                           "CONBPOK=" + (string)Property.htProperty["CONBPOK"] + ";" +
                           "CONBPERROR=" + (string)Property.htProperty["CONBPERROR"];

            string strT2 = "DATABP_NASIENTO=" + (string)Property.htProperty["DATABP_NASIENTO"] + ";" +
                           "DATABP_PASAJERO=" + (string)Property.htProperty["DATABP_PASAJERO"] + ";" +
                           "DATABP_NVUELO=" + (string)Property.htProperty["DATABP_NVUELO"] + ";" +
                           "DATABP_FECHA=" + (string)Property.htProperty["DATABP_FECHA"] + ";" +
                           "REGBPRESUMEN=" + (string)Property.htProperty["REGBPRESUMEN"] + ";" +
                           "REGBPOK=" + (string)Property.htProperty["REGBPOK"] + ";" +
                           "REGBPERROR=" + (string)Property.htProperty["REGBPERROR"];

            Obj_IntzPinPad.MostrarMsgPINPad(ACS_Define.Cod_TiemIniFormPnd, strT1, strT2);
        }

        /// <summary>
        /// Verifica cambios en el molinete
        /// </summary>
        private void IniciarPropiedades(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string strCodMolinete = " SELECT Cod_Molinete,Dsc_Ip,Dsc_Molinete,Tip_Documento,Tip_Vuelo,Tip_Acceso, " +
                " Tip_Estado,Est_Master, Dsc_DBName,Dsc_DBUser,Dsc_DBPassword ,l.Dsc_Campo Tip_Molinete " +
                " FROM TUA_Molinete m,TUA_ListaDeCampos l " +
                " WHERE m.Cod_Molinete='" + (string)Property.htProperty["CODMOLINETE"] + "' and l.Nom_Campo='TipAccesoMolinete' and l.cod_Campo=m.Tip_Acceso ";

                if (ACS_Property.BConLocal)
                {
                    ACS_Property.IDRMolinete = ACS_Property.shelperLocal.ExecuteReader(CommandType.Text, strCodMolinete);
                    ACS_Property.IDRMolinete.Read();
                    ACS_Property.shtMolinete["Tip_Vuelo"] = (string)ACS_Property.IDRMolinete["Tip_Vuelo"];
                    ACS_Property.shtMolinete["Est_Master"] = (string)ACS_Property.IDRMolinete["Est_Master"];
                    ACS_Property.shtMolinete["Tip_Molinete"] = (string)ACS_Property.IDRMolinete["Tip_Molinete"];
                    ACS_Property.shtMolinete["Cod_Molinete"] = (string)ACS_Property.IDRMolinete["Cod_Molinete"];
                    ACS_Property.shtMolinete["Tip_Documento"] = (string)ACS_Property.IDRMolinete["Tip_Documento"];
                    ACS_Property.shtMolinete["Tip_Estado"] = (string)ACS_Property.IDRMolinete["Tip_Estado"];
                    ACS_Property.shtMolinete["Tip_Vuelo"] = (string)ACS_Property.IDRMolinete["Tip_Vuelo"];


                    ACS_Property.IDRMolinete.Close();
                }
                else
                {
                    if (ACS_Property.BConRemota)
                    {
                        ACS_Property.IDRMolinete = ACS_Property.shelper.ExecuteReader(CommandType.Text, strCodMolinete);
                        ACS_Property.IDRMolinete.Read();
                        ACS_Property.shtMolinete["Tip_Vuelo"] = (string)ACS_Property.IDRMolinete["Tip_Vuelo"];
                        ACS_Property.shtMolinete["Est_Master"] = (string)ACS_Property.IDRMolinete["Est_Master"];
                        ACS_Property.shtMolinete["Tip_Molinete"] = (string)ACS_Property.IDRMolinete["Tip_Molinete"];
                        ACS_Property.shtMolinete["Cod_Molinete"] = (string)ACS_Property.IDRMolinete["Cod_Molinete"];
                        ACS_Property.shtMolinete["Tip_Documento"] = (string)ACS_Property.IDRMolinete["Tip_Documento"];
                        ACS_Property.shtMolinete["Tip_Estado"] = (string)ACS_Property.IDRMolinete["Tip_Estado"];
                        ACS_Property.shtMolinete["Tip_Vuelo"] = (string)ACS_Property.IDRMolinete["Tip_Vuelo"];

                        ACS_Property.IDRMolinete.Close();
                    }
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        private void LongitudTicket(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                string strLong = "SELECT valor FROM dbo.TUA_ParameGeneral WHERE Identificador = 'LNT'";

                if (ACS_Property.BConLocal)
                {
                    ACS_Property.IDRMolinete = ACS_Property.shelperLocal.ExecuteReader(CommandType.Text, strLong);
                    ACS_Property.IDRMolinete.Read();
                    ACS_Property.shtMolinete["Long_Ticket"] = (string)ACS_Property.IDRMolinete["valor"];
                    ACS_Property.IDRMolinete.Close();
                }
                else
                {
                    if (ACS_Property.BConRemota)
                    {
                        ACS_Property.IDRMolinete = ACS_Property.shelper.ExecuteReader(CommandType.Text, strLong);
                        ACS_Property.IDRMolinete.Read();
                        ACS_Property.shtMolinete["Long_Ticket"] = (string)ACS_Property.IDRMolinete["valor"];
                        ACS_Property.IDRMolinete.Close();
                    }
                }
            }
            catch (Exception E)
            {
                throw E;
            }
        }

        /// <summary>
        /// Verifica conexion de BD 
        /// </summary>
        private void VerificarConexionRemota(object source, System.Timers.ElapsedEventArgs e)
        {
            DAO_BaseDatos objDAO_BaseDatos;
            string sqlQuery = "select getdate()";
            IDataReader reader = null;
            try
            {
                if (ACS_Property.shelper == null)
                {

                    objDAO_BaseDatos = new DAO_BaseDatos();
                    objDAO_BaseDatos.Conexion("tuuacnx");
                    ACS_Property.shelper = objDAO_BaseDatos.helper;

                }

                reader = ACS_Property.shelper.ExecuteReader(CommandType.Text, sqlQuery);
                if (!ACS_Property.BConRemota)
                    Obj_Util.EscribirLog(this, "Conexion Remota Activa");
                reader.Close();
                ACS_Property.BConRemota = true;
            }
            catch (Exception)
            {
                if ((ACS_Property.BConRemota) || source == null)
                    Obj_Util.EscribirLog(this, "Conexion Remota Inactiva");
                ACS_Property.BConRemota = false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void VerificarConexionLocal(object source, System.Timers.ElapsedEventArgs e)
        {
            //if (!Convert.ToBoolean((string)Property.htProperty["CNXLOCAL"]))
            //{
            //    Obj_Util.EscribirLog(this, "Conexion Local Inactiva ");
            //    return;
            //}
            //Thread.Sleep(20000);
            string sqlQuery = "select getdate()";
            DAO_BaseDatos objDAO_BaseDatos;
            IDataReader reader = null;
            try
            {
                if (ACS_Property.shelperLocal == null)
                {
                    objDAO_BaseDatos = new DAO_BaseDatos();
                    objDAO_BaseDatos.Conexion("tuuacnxlocal");
                    ACS_Property.shelperLocal = objDAO_BaseDatos.helper;
                }

                string aa = ACS_Property.shelperLocal.ConnectionString;

                reader = ACS_Property.shelperLocal.ExecuteReader(CommandType.Text, sqlQuery);
                reader.Close();
                if (!ACS_Property.BConLocal)
                    Obj_Util.EscribirLog(this, "Conexion Local Activa ");
                ACS_Property.BConLocal = true;
            }
            catch (Exception)
            {
                if ((ACS_Property.BConLocal) || source == null)
                    Obj_Util.EscribirLog(this, "Conexion Local Inactiva ");
                ACS_Property.BConLocal = false;

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
                XmlDocument xmlScom;
                string strPortName;
                string strParity;
                string strBitsData;
                string strStopBits;
                System.Xml.XmlNodeList xmlnPortName;
                System.Xml.XmlNodeList xmlnBaudRate;
                System.Xml.XmlNodeList xmlnParity;
                System.Xml.XmlNodeList xmlnBitsData;
                string strBaudRate;

                //Iniciar SPs
                Property.scargarSPConfig(AppDomain.CurrentDomain.BaseDirectory);
                
                //Carga Propiedades Generales
                Property.CargarPropiedades(AppDomain.CurrentDomain.BaseDirectory + "resources/");

                //esilva - 04-05-2010 - Carga Test de Pruebas BP
                ACS_Property.bModeTest = Convert.ToBoolean((string)Property.htProperty["MODE_TEST"]);
                ACS_Property.iTimeOutTest = Convert.ToInt32((string)Property.htProperty["TIMEOUT_TEST"]);
                if (ACS_Property.bModeTest)
                {
                    BPConfig.LoadData(AppDomain.CurrentDomain.BaseDirectory + "resources/");
                }

                //eochoa - 17-05-2011 - Prueba del nuevo modo de lectura
                ACS_Property.bModeLecturaNueva = Convert.ToBoolean((string)Property.htProperty["MODE_LECTURA_NUEVA"]);

                //esilva - 31-07-2010 - Cargar Flag indicador de escritura de log de error
                ACS_Property.bWriteErrorLog = Convert.ToBoolean((string)Property.htProperty["WRITE_ERROR_LOG"]);

                //Modo de Operacion del Molinete
                ACS_Property.modoContingencia = Convert.ToBoolean((string)Property.htProperty["MODOCONTINGENCIA"]);
                if (ACS_Property.modoContingencia)
                {
                    thrContingencia = new Thread(new ThreadStart(run));
                    thrContingencia.Start();

                    ACS_Property.estadoLector = false;
                    ACS_Property.estadoPinPad = false;
                    ACS_Property.estadoMolinete = false;
                }
                //Inicia Molinete
                try
                {
                    if ((!ACS_Property.modoContingencia) && Convert.ToBoolean((string)Property.htProperty["ESTADOMOLINETE"]))
                    {
                        this.Obj_Molinete = new LAP.TUUA.AccesoMolinete.Molinete();
                        this.Obj_MolineteDiscapacitado = new LAP.TUUA.AccesoMolinete.MolineteDiscapacitados();
                        this.Obj_Molinete.OpenDriverMolinetes();
                        ACS_Property.estadoMolinete = Convert.ToBoolean((string)Property.htProperty["ESTADOMOLINETE"]);
                    }
                }
                catch (Exception e)
                {
                    Obj_Util.EscribirLog(this, e.Message);
                    ACS_Property.estadoMolinete = false;
                }
                finally
                {
                    ACS_Property.scargarMolinete();
                }

                //Inicia y Apertura LectorCB
                xmlScom = new XmlDocument();
                try
                {
                    if ((!ACS_Property.modoContingencia) && Convert.ToBoolean((string)Property.htProperty["ESTADOLECTOR"]))
                    {
                        xmlScom.Load(ACS_Define.Dsc_SCOMLectorConfig);
                        xmlnPortName = xmlScom.GetElementsByTagName("PortName");
                        strPortName = xmlnPortName[0].InnerText;

                        xmlnBaudRate = xmlScom.GetElementsByTagName("BaudRate");
                        strBaudRate = xmlnBaudRate[0].InnerText;

                        xmlnParity = xmlScom.GetElementsByTagName("Parity");
                        strParity = xmlnParity[0].InnerText;

                        xmlnBitsData = xmlScom.GetElementsByTagName("BitsData");
                        strBitsData = xmlnBitsData[0].InnerText;

                        xmlnBitsData = xmlScom.GetElementsByTagName("StopBits");
                        strStopBits = xmlnBitsData[0].InnerText;

                        Obj_SCom = new ACS_SCom(strPortName, Int32.Parse(strBaudRate),
                                                     Int32.Parse(strParity), Int32.Parse(strBitsData), Int32.Parse(strStopBits));
                        Obj_SCom.Open();

                        ACS_Property.estadoLector = Convert.ToBoolean((string)Property.htProperty["ESTADOLECTOR"]);

                    }
                }
                catch (Exception e)
                {
                    Obj_Util.EscribirLog(this, e.Message);
                    ACS_Property.estadoLector = false;
                }


                //Inicia y Apertura PINPAD
                try
                {
                    if ((!ACS_Property.modoContingencia) && Convert.ToBoolean((string)Property.htProperty["ESTADOPINPAD"]))
                    {
                        XmlDocument xmlSCOMPINPAD = new XmlDocument();
                        xmlScom.Load(ACS_Define.Dsc_SCOMPinpadConfig);
                        xmlnPortName = xmlScom.GetElementsByTagName("PortName");
                        strPortName = xmlnPortName[0].InnerText;

                        xmlnBaudRate = xmlScom.GetElementsByTagName("BaudRate");
                        strBaudRate = xmlnBaudRate[0].InnerText;

                        xmlnParity = xmlScom.GetElementsByTagName("Parity");
                        strParity = xmlnParity[0].InnerText;

                        xmlnBitsData = xmlScom.GetElementsByTagName("BitsData");
                        strBitsData = xmlnBitsData[0].InnerText;

                        System.Xml.XmlNodeList xmlnMaxTout = xmlScom.GetElementsByTagName("MaxTout");
                        string strMaxTout = xmlnMaxTout[0].InnerText;

                        System.Xml.XmlNodeList xmlnMinTout = xmlScom.GetElementsByTagName("MinTout");
                        string strMinTout = xmlnMinTout[0].InnerText;

                        Obj_PinPad = new ACS_SComPINPAD(strPortName, Int32.Parse(strBaudRate), Int32.Parse(strParity), Int32.Parse(strBitsData));

                        Obj_PinPad.TOutMax = Int32.Parse(strMaxTout);
                        Obj_PinPad.TOutMin = Int32.Parse(strMinTout);
                        //JAC
                        //Obj_PinPad.Open();
                        ACS_Property.estadoPinPad = Convert.ToBoolean((string)Property.htProperty["ESTADOPINPAD"]);
                    }

                }
                catch (Exception e)
                {
                    Obj_Util.EscribirLog(this, e.Message);
                    ACS_Property.estadoPinPad = false;
                }

                //Inicia conexion BD Remota
                VerificarConexionRemota(null, null);

                //Inicia conexion BD Local
                if (Convert.ToBoolean((string)Property.htProperty["CNXLOCAL"]))
                {
                    //Obj_Util.EscribirLog(this, "Conexion Local Inactiva ");
                    VerificarConexionLocal(null, null);
                }
                

                //inicia cambios en  molinete
                IniciarPropiedades(null, null);
                LongitudTicket(null, null);

                if (Convert.ToBoolean((string)Property.htProperty["CNXLOCAL"]))
                {
                    System.Timers.Timer tmConexionLocal = new System.Timers.Timer();
                    tmConexionLocal.Interval = 3000;
                    tmConexionLocal.Elapsed += new System.Timers.ElapsedEventHandler(VerificarConexionLocal);
                    tmConexionLocal.Start();
                }

                System.Timers.Timer tmConexionRemota = new System.Timers.Timer();
                tmConexionRemota.Interval = 1000;
                tmConexionRemota.Elapsed += new System.Timers.ElapsedEventHandler(VerificarConexionRemota);
                tmConexionRemota.Start();

                System.Timers.Timer tiempo = new System.Timers.Timer();
                tiempo.Interval = 3000;
                tiempo.Elapsed += new System.Timers.ElapsedEventHandler(IniciarPropiedades);
                tiempo.Start();


                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                Obj_IntzPinPad.Flg_Ejecutar = false;
                Obj_IntzLector.Flg_Ejecutar = false;

                if (Obj_SCom.serial.IsOpen)
                    Obj_SCom.serial.Close();

                if (ACS_Property.modoContingencia)
                {
                    frmContingencia.Close();
                    thrContingencia.Abort();
                }
            }
            catch (Exception)
            {

            }
        }

        public void run()
        {
            Application.ThreadException +=
            new ThreadExceptionEventHandler(excepcion);

            this.Obj_SincContingencia = new ACS_SinContingencia();
            frmContingencia = new ACS_FormContingencia(Obj_SincContingencia, Convert.ToBoolean((string)Property.htProperty["PEGAR_TRAMA"]));

            Application.Run(frmContingencia);
        }

        public void excepcion(object sender,
        ThreadExceptionEventArgs excepcion)
        {
            MessageBox.Show("Se ha producido un error");
        }

    }
}
