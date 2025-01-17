///File: ACS_InterfazLector.cs
///Proposito:Inicia,Lee y procesa tramas provenientes del Lector
///Metodos: 
///void Ejecutar()
///int ProcesarTrama()
///int LeerLectorCB()
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:15/07/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:20/07/2009


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;
using System.Xml;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;
using LAP.TUUA.CONVERSOR;
using System.IO;
using LAP.TUUA.ACCESOS;
using System.Data;

namespace LAP.TUUA.ACCESOS
{
    /// <summary>
    /// ACCESOS: InterfazLector
    /// </summary>
    class ACS_InterfazLector
    {
        #region Fields
        private ACS_SincronizaLectura Obj_SincLector;
        private ACS_SCom Obj_SCom;
        private Hashtable Arr_PtoLectorConfig;
        private ACS_Util Obj_Util;
        private BoardingBcbp Obj_BoardingBcbp;
        private ACS_SComPINPAD Obj_PinPad;

        private Ticket Obj_Ticket;
        private ACS_InterfazPinPad Obj_IntzPinPad;
        private ACS_Resolver Obj_Resolver;

        private List<Compania> Lst_Compania;
        private List<ListaDeCampo> Lst_ListaDeCampo;
        private List<TipoTicket> Lst_TipoTicket;
        private List<Usuario> Lst_Usuario;

        public Thread Obj_HiloInterfazLector;
        private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
        private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados;

        private ACS_FormContingencia frmContingencia;
        private ACS_SinContingencia Obj_SincContingencia;
        //jortega
        private ACS_DestrabeMolinete Obj_DestrabeMolinete;

        //LAP-TUUA-9163 - 11-06-2012 (RS) CMONTES - Begin
        private ACS_SincronizaLectura Obj_SincLectorCierre;
        public Thread Obj_HiloInterfazCierreLector;
        public bool isProcesoClose;
        public bool isProcesoEjecucionCierre;
        //LAP-TUUA-9163 - 11-06-2012 (RS) CMONTES - End
        
        public bool Flg_Ejecutar;
        public Hashtable Lst_WSBcbp;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obj_Sinc"></param>
        public ACS_InterfazLector(ACS_SincronizaLectura Obj_Sinc, Hashtable Arr_Pto, ACS_SComPINPAD Obj_PinPad,
                                  ACS_InterfazPinPad Obj_IntzPinPad, ACS_SCom Obj_SCom, ACS_Resolver Obj_Resolver,
                                  LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete, LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados,
                                  ACS_FormContingencia frmContingencia, ACS_SinContingencia Obj_SincContingencia)
        {
            this.Obj_Util = new ACS_Util();
            this.Obj_SCom = Obj_SCom;
            this.Obj_HiloInterfazLector = new Thread(new ThreadStart(Ejecutar));
             //LAP-TUUA-9163 - 11-06-2012 (RS) CMONTES

            this.Obj_SincLector = Obj_Sinc;
            this.Arr_PtoLectorConfig = Arr_Pto;
            this.Obj_PinPad = Obj_PinPad;
            this.Obj_Resolver = Obj_Resolver;
            this.Obj_IntzPinPad = Obj_IntzPinPad;
            this.Obj_Molinete = Obj_Molinete;
            this.Obj_MolineteDiscapacitados = Obj_MolineteDiscapacitados;
            this.frmContingencia = frmContingencia;
            this.Obj_SincContingencia = Obj_SincContingencia;
            this.Flg_Ejecutar = true;
            this.Obj_DestrabeMolinete = new ACS_DestrabeMolinete(Obj_Util, Obj_Molinete, Obj_MolineteDiscapacitados, Obj_Resolver);

            //LAP-TUUA-9163 - 08-06-2012 (RS) CMONTES - Begin
            this.Obj_SincLectorCierre = new ACS_SincronizaLectura();
            this.Obj_HiloInterfazCierreLector = new Thread(new ThreadStart(ValidarCierre));
            isProcesoClose = false;
            isProcesoEjecucionCierre = false;
            //LAP-TUUA-9163 - 08-06-2012 (RS) CMONTES - End
        }
        #endregion

        #region Methods

        /// <summary>
        /// Inicia la ejecucion del hilo Lector de Cierre
        /// LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
        /// </summary>
        public void ValidarCierre()
        {
            while (true)
            {
                if (isProcesoEjecucionCierre)
                {
                    try
                    {
                        if (Obj_MolineteDiscapacitados != null)
                        {
                            isProcesoClose = Obj_MolineteDiscapacitados.IsEstadoOpen;
                            if (isProcesoClose)
                            {
                                //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Molinete Discapacitado Instanciado, Estado Gate: "
                                //                           + Obj_MolineteDiscapacitados.IsEstadoOpen.ToString());
                            }
                        }

                        if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                        {
                            if (LAP.TUUA.UTIL.Define.FLG_GATE_OPEN == "1")
                            {
                                //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - ESTADO OPEN MOLINETE");
                                //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - VALIDO CORRECTAMENTE EL OPEN MOLINETE: " + Obj_MolineteDiscapacitados.IsEstadoOpen.ToString());
                                if (ACS_Property.estadoLector)
                                {
                                    //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - VALIDO ESTADO MOLINETE: " + ACS_Property.estadoLector);
                                    if (!Obj_SCom.serial.IsOpen)
                                    {
                                        //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - COM SE ENCUENTRA EN CLOSE");
                                        Obj_SCom.serial.Open();
                                        //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - APERTURO EL COM");
                                    }

                                    Thread.Sleep(250);
                                    int iConect = Obj_SCom.Conectado() ? 1 : 0;
                                    if (iConect > 0)
                                    {
                                        //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Estamos conectados al COM listos para leer Trama");
                                        //Leer Trama
                                        //Validar que sea Supervisor
                                        //Ejecutar Logica de Close Molinete Discapacitado
                                        LeerLectorEspecialCierre();
                                        //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Finalizo lectura de Trama");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Obj_Util.EscribirLog(this, "Error en Hilo Cierre: " + ex.Message);
                    }
                    finally
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Inicia la ejecucion del hilo Lector
        /// </summary>
        public void Ejecutar()
        {
            if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
            {                
                this.Obj_HiloInterfazCierreLector.Start();
            }

            do
            {
                try
                { 
                    Monitor.Enter(Obj_SincLector);
                    this.isProcesoEjecucionCierre = true;                    

                    if (!isProcesoClose)
                    {
                        if (Obj_SincLector.Flg_LecturaScaner)
                        {
                            if (ACS_Property.estadoLector)
                            {
                                int intRpta = Obj_SCom.Conectado() ? 1 : 0;
                                if (intRpta > 0)
                                {
                                    LeerLectorCB();                                    
                                }
                                else
                                {
                                    Obj_Util.EscribirLog(this, "LECTOR NO RESPONDE");
                                    //Obj_SCom.serial.Close();
                                    if (!Obj_SCom.serial.IsOpen)
                                    {
                                        Obj_SCom.serial.Open();
                                    }
                                }
                            }
                            else
                            {
                                if (ACS_Property.modoContingencia)
                                {
                                    LeerLectorManualCB();
                                }
                                else
                                {
                                    Thread.Sleep(500);
                                }
                            }

                            Obj_SincLector.Flg_LecturaScaner = false;
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    string strTipError = ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                    String strSTrace = "";
                    int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                    if (intOcurrencia > 0)
                    {
                        strSTrace = ex.StackTrace.Substring(intOcurrencia);
                        intOcurrencia = strSTrace.IndexOf(")");
                        strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                    }

                    //Obj_Util.EscribirLog(strSTrace, ex.Message);
                    Obj_Util.EscribirLog(this, ex.GetBaseException().Message);
                    Obj_Util.EscribirLog(this, ex.StackTrace);
                    if (ACS_Property.modoContingencia)
                    {
                        Obj_SincContingencia.StrTrama = "";
                        frmContingencia.Limpiar();
                    }

                    Obj_SincLector.Flg_LecturaScaner = false;
                }
                finally
                {
                    this.isProcesoEjecucionCierre = false;
                    Monitor.Exit(Obj_SincLector);
                }

            } while ((true) && Flg_Ejecutar);

        }


        
        /// <summary>
        /// Lee el lector de codigo de barras para cierre de molinete
        /// LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES 
        /// </summary>
        public void LeerLectorEspecialCierre()
        {
            String strTrama;
            strTrama = Obj_SCom.Leer();
            //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Leyendo del COM");
            if (Property.htProperty["SIMULARTRAMA"] != null)
                if (bool.Parse((string)Property.htProperty["SIMULARTRAMA"]))
                {
                    strTrama = LeerTramaArchivo();
                }

            if (strTrama.Length > 0)
            {
                Obj_SCom.serial.Close();
                Obj_Util.EscribirLog(this, "=========================================");
                Obj_Util.EscribirLog(this, "Inicia Flujo Automatico del Lector para validar cierre de molinete".ToUpper());
                Obj_Util.EscribirLog(this, "Trama Recibida:" + strTrama);
                ProcesarTramaEspecialCierre(strTrama);
                Obj_Util.EscribirLog(this, "Finaliza Flujo Automatico del Lector para validar cierre de molinete".ToUpper());
                Obj_Util.EscribirLog(this, "=========================================");
                strTrama = "";
                Obj_SCom.strTrama = "";
                
                if (!Obj_SCom.serial.IsOpen)
                {
                    Obj_SCom.serial.Open();
                }
            }
        }

        /// <summary>
        /// Procesa trama leida desde el lector para el cierre 
        /// del molinte de discapacitados.
        /// LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES 
        /// </summary>
        public void ProcesarTramaEspecialCierre(String strTrama)
        {
            //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Se Inicia el procesar Trama para cierre");
            Reader objReader = new Reader();
            if (ACS_Property.estadoLector)
            {
                char[] cs = strTrama.ToCharArray();
                byte[] binScanData = new byte[cs.Length];
                int j;

                for (j = 0; j < cs.Length; j++)
                {
                    binScanData[j] = (byte)cs[j];
                }

                int VAnterior = -1;
                for (j = 0; j < cs.Length; j++)
                {
                    if (binScanData[j] == 2)
                    {
                        //0?
                        j++;
                        VAnterior = 0;
                        if ((binScanData[j + 0] == '0' && binScanData[j + 1] == '?')
                              || (binScanData[j + 0] == '0' && binScanData[j + 1] == 146)
                              || (binScanData[j + 0] == '0' && binScanData[j + 1] == 131)
                            )
                        {
                            VAnterior = 1;
                        }
                        break;
                    }
                }
                if (VAnterior == -1) return;//MENSAJE DE ERROR NO STX
                if (VAnterior == 0)
                {
                    // binScanData.ToString( 
                    strTrama = "   " + strTrama.Substring(j);
                }
            }
            //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Inicio conversion trama para cierre de molinete");
            Hashtable htBoarding = objReader.ParseString_ACS(strTrama, (string)Property.htProperty["CODIGOSUPERVISOR"],
                                                                       (string)Property.htProperty["PREFCLOSE"], String.Empty);
            //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - Fin conversion trama para cierre de molinete");  
            if (htBoarding != null)
            {
                Obj_Util.EscribirLog(this, "Inicio Validaciones para cierre de molinete");
                string strFormatCode = (string)htBoarding["format_code"];
                string strPrefijo = (string)Property.htProperty["CODIGOSUPERVISOR"];
                //Obj_Util.EscribirLog(this, "CLOSE MOLINETE - FormatCode: " + strFormatCode);
                if (strFormatCode == "D")
                {
                    //Trama Close Molinete Discapacitados
                    string strTramaNueva, strCodUsuario;
                    int PosTramaIni;
                    long nRpta;
                    PosTramaIni = strTrama.IndexOf((string)Property.htProperty["PREFCLOSE"]);
                    strTramaNueva = strTrama.Substring(PosTramaIni, 9);
                    strCodUsuario = strTramaNueva.Substring(3, 6);
                    nRpta = 0;
                    if (long.TryParse(strCodUsuario, out nRpta))
                    {
                        if (strCodUsuario.Equals((string)Property.htProperty["CODPREFCLOSE"]))
                        {
                            try
                            {
                                Obj_Util.EscribirLog(this, "Se validaron todas las condiciones para el cierre forzado.");
                                Obj_DestrabeMolinete.CierreForzadoMolinete();
                            }
                            catch (Exception ex)
                            {
                                Obj_Util.EscribirLog(this, "Error: " + ex.Message);
                            }
                        }
                        else
                        {
                            Obj_Util.EscribirLog(this, "Codigo cierre de molinete no es valido: " + strCodUsuario);
                        }
                    }
                    else
                    {
                        Obj_Util.EscribirLog(this, "No se pudo leer el codigo para cierre de molinete." + strCodUsuario);
                    }
                }
                else
                {
                    Obj_Util.EscribirLog(this, "Tipo de Codigo cierre de molinete no es valido.");
                }
            }
            else
            {
                Obj_Util.EscribirLog(this, "Formato de codigo cierre de molinete no es valido.");                
            }
        }

        public void LeerLectorManualCB()
        {
            String strTrama;
            try
            {
                Monitor.Enter(Obj_SincContingencia);
                strTrama = Obj_SincContingencia.StrTrama;
                if (Obj_SincContingencia.BLectura && strTrama.Length > 0)
                {
                    //VerificarConexionLocal(null, null);
                    //VerificarConexionRemota(null, null);

                    frmContingencia.LimpiarSemaforo();
                    strTrama = "    " + strTrama;
                    if (ACS_Property.estadoLector)
                        Obj_SCom.serial.Close();
                    Obj_Util.EscribirLog(this, "Inicia Flujo Automatico desde Lector".ToUpper());
                    Obj_Util.EscribirLog(this, "Trama Recibida:" + strTrama);
                    ProcesarTrama(strTrama);
                    Obj_Util.EscribirLog(this, "Finaliza Flujo Automatico desde Lector".ToUpper());
                    Obj_Util.EscribirLog(this, "=========================================");

                    frmContingencia.AlmacenarDetalle();
                    frmContingencia.Limpiar();
                    Obj_SincContingencia.StrTrama = "";
                    Obj_SincContingencia.BLectura = false;
                }
                strTrama = "";
            }
            finally
            {
                Monitor.Exit(Obj_SincContingencia);
            }
        }


        private string LeerTramaArchivo()
        {
            try
            {
                string strLectura = "";

                string PathLabel = AppDomain.CurrentDomain.BaseDirectory + "boarding/" + ((string)Property.htProperty["ARCHIVO"]);
                StreamReader objReader = new StreamReader(PathLabel, System.Text.Encoding.UTF7);//Si no pongo este ultimo parametro no me muestra las tildes.
                string sLine = "";
                sLine = objReader.ReadLine();
                if (sLine != null)
                {
                    strLectura = sLine;
                }
                objReader.Close();
                return strLectura;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Lee el lector de codigo de barras
        /// </summary>
        public void LeerLectorCB()
        {
            String strTrama;
            strTrama = Obj_SCom.Leer();

            //strTrama = "0?5DES000003";
            //strTrama = "10000000048943751000000004894375100";
            //strTrama = "6M1BASSANCHEZFORTUN/JUAEHH0XZ  LIMIQQLA 0941 246V011L0059 13d>10b0  9245BLA 290000000000000 1                           11";
            //prueba
            //  System.Threading.ThreadStart starter = delegate { MostrarMsgConcurrente("Usuario NO Valido"); };
            //fin prueba 

            if (Property.htProperty["SIMULARTRAMA"] != null)
                if (bool.Parse((string)Property.htProperty["SIMULARTRAMA"]))
                {
                    strTrama = LeerTramaArchivo();
                }

            if (strTrama.Length > 0)
            {
                //ss++;
                Obj_SCom.serial.Close();

                //VerificarConexionLocal(null, null);
                Obj_Util.EscribirLog(this, "Inicia Flujo Automatico desde Lector".ToUpper());
                Obj_Util.EscribirLog(this, "Trama Recibida:" + strTrama);
                VerificarConexionRemota(null, null);
                //Obj_Util.EscribirLog(this, "Procesando Trama..");
                ProcesarTrama(strTrama);
                Obj_Util.EscribirLog(this, "Finaliza Flujo Automatico desde Lector".ToUpper());
                Obj_Util.EscribirLog(this, "=========================================");
                strTrama = "";
                Obj_SCom.strTrama = "";
                if (!Obj_SCom.serial.IsOpen)
                {
                    Obj_SCom.serial.Open();
                }
            }
        }

        /// <summary>
        /// Procesa trama leida desde el lector
        /// </summary>
        public void ProcesarTrama(String strTrama)
        {
            //Thread.Sleep(20000);
            //strTrama = "0?52007111719A116211";
            //TRAMA AVIANCA
            //strTrama = "0?6M1TRIANA LINA          3Z8Y3C LIMBOGAV 24   347C02C 149   16134020404010508090801080 030 0";
            //TRAMA SKY
            //strTrama = "0?6M1HUAMANLOVERA-ANDREOSEM3CTK  LIMSCLH2 0801 330Q24A 1    13B:10B0WW0328BH2 2A";
            //strTrama = "0?5TUUA0001";
            //strTrama = "0?6M1CABRERA OMANARI/MIGU LSRIFE LIMVVN2I 0100 173Y008B0005 127>3180CO1173B2I              0Dxf%·%·$% ";
            //strTrama = "0?6M1CABRERA OMANARI/MIGU LSRIFE LIMVVN2I 0100 173Y008B0005 127>3180CO1173B2I              0Dxf%·%·$% ";
            //strTrama = "0?4M1BORAGLIO/EMILIANOJAVE6Z5PCN LIMEZEAV 0967 261Y014E0036 65C>2180 J    B   01342046610012913447617008370                           *30601023K09         ";
            CargarTablasTemp();
            Reader objReader = new Reader();
            if (ACS_Property.estadoLector)
            {
                char[] cs = strTrama.ToCharArray();
                byte[] binScanData = new byte[cs.Length];
                int j;
                //Obs: ¿Necesario?
                for (j = 0; j < cs.Length; j++)
                {
                    binScanData[j] = (byte)cs[j];
                }

                int VAnterior = -1;
                for (j = 0; j < cs.Length; j++)
                {
                    if (binScanData[j] == 2)
                    {
                        //0?
                        j++;
                        VAnterior = 0;
                        if ((binScanData[j + 0] == '0' && binScanData[j + 1] == '?')
                              || (binScanData[j + 0] == '0' && binScanData[j + 1] == 146)
                              || (binScanData[j + 0] == '0' && binScanData[j + 1] == 131)
                            )
                        {
                            VAnterior = 1;
                        }
                        break;
                    }
                }
                if (VAnterior == -1) return;//MENSAJE DE ERROR NO STX
                if (VAnterior == 0)
                {
                    // binScanData.ToString( 
                    strTrama = "   " + strTrama.Substring(j);
                }
            }
            Obj_Util.EscribirLog(this, "Inicio Conversion Trama");
            //Deprecado para agregar campo de Cambio de Molinete
            //Hashtable htBoarding = objReader.ParseString_ACS(strTrama, (string)Property.htProperty["CODIGOSUPERVISOR"], (string)Property.htProperty["PREFSUPER"]);
            Hashtable htBoarding = objReader.ParseString_ACS(strTrama, (string)Property.htProperty["CODIGOSUPERVISOR"], (string)Property.htProperty["PREFDESTRABE"], (string)Property.htProperty["PREFCAMBIMOLINETE"]);
            Obj_Util.EscribirLog(this, "Fin Conversion Trama");

            int i = 1;
            if (htBoarding != null)
            {
                Obj_Util.EscribirLog(this, "Inicio Validaciones Internas");
                string strTipDocumento = (string)ACS_Property.shtMolinete["Tip_Documento"];
                string strFormatCode = (string)htBoarding["format_code"];

                string strPrefijo = (string)Property.htProperty["CODIGOSUPERVISOR"];
                if (strFormatCode.Equals("T"))
                    if (((string)htBoarding["NroTicket"]).Substring(0, strPrefijo.Length).Equals(strPrefijo))
                    {
                        strTipDocumento = "0";
                    }

                if (!strTipDocumento.Equals("0") &&
                    !(strTipDocumento.Equals("T") && strFormatCode.Equals("T")) &&
                    !((strTipDocumento.Equals("B") && strFormatCode.Equals("M")) || (strTipDocumento.Equals("B") && strFormatCode.Equals("S")))
                   )
                    strFormatCode = "";

                switch (strFormatCode)
                //switch ((string)htBoarding["format_code"])
                {

                    case "M"://trama multiple
                        ArrayList arrLst = (ArrayList)htBoarding["flight_information"];
                        foreach (Hashtable objHT in arrLst)
                        {
                            if (i == 1)
                            {
                               // System.Threading.Thread thWSBcbp = null;
                                try
                                {
                                    AnalizarSegmentosBCBP(arrLst);
                                    Obj_Resolver.CrearDAOModVentaComp();
                                    objHT.Add(ACS_Define.KEY_PASSANGER_NAME_WS, (string)htBoarding["passenger_name"]);
                                    DataTable dtAtributos = Obj_Resolver.ListarAtributosWSAerolinea((string)objHT["airline_designator"]);
                                    Hashtable htWSBcbp = CargarParametrosRequestWSBcbp(objHT, dtAtributos);
                                   // thWSBcbp = new System.Threading.Thread(new System.Threading.ThreadStart(ConsumirWebService));
                                   // thWSBcbp.Start();

                                    TimeSpan tsStart = new TimeSpan(DateTime.Now.Ticks);
                                    //formato de nombre "AP/Nom" a "AP Nom"
                                    string strNom = (string)htBoarding["passenger_name"];
                                    strNom = strNom.Replace('/', ' ');
                                    strNom = strNom.Replace('-', ' ');
                                    strNom = strNom.Replace('&', ' ');
                                    htBoarding["passenger_name"] = strNom;
                                    objHT.Add("passenger_name", htBoarding["passenger_name"]);
                                    objHT["date_flight"] = Obj_Util.ConvertirJulianoCalendario(Int32.Parse((string)objHT["date_flight"]));

                                    objReader.ParseHashtable(objHT);

                                    MostrarLecturaContingencia(ACS_Define.Tip_Compuesta, objHT);

                                    string strCodCompania = Obj_Util.ObtenerCodCompania((string)objHT["airline_designator"], Lst_Compania);

                                    objHT["passenger_description"] = (string)htBoarding["passenger_description"];
                                    objHT.Add(ACS_Define.DESTINATION, objHT[ACS_Define.TO_CITY_AIRPORT_CODE]);
                                    objHT.Add(ACS_Define.BAR_CODE_FORMAT, LAP.TUUA.CONVERSOR.Define.FORM_BCBP_LAN2D);

                                    Obj_Resolver.CrearDAOBoardingBcbp();

                                    String strChekingNumber = ACS_Define.VALOR_DEFT_CHECKIN_SEQNUMBER;
                                    if (objHT["checkin_sequence_number"] != null && (string)objHT["checkin_sequence_number"] != String.Empty)
                                    {
                                        strChekingNumber = (string)objHT["checkin_sequence_number"];
                                    }

                                    Obj_BoardingBcbp = Obj_Resolver.obtenerDAOBoardingBcbp(strCodCompania, (string)objHT["flight_number"],
                                                                           (string)objHT["date_flight"], (string)objHT["seat_number"],
                                                                           (string)htBoarding["passenger_name"], null, strChekingNumber);

                                    ACS_Boarding objACSBoarding = new ACS_Boarding(objHT, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad,
                                                                                   Obj_Util, Obj_Resolver, Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                                    objACSBoarding.Lst_WSBcbp = htWSBcbp;
                                    objACSBoarding.Time_Start = tsStart;
                                    objACSBoarding.ValidarBCBP(Obj_BoardingBcbp, strTrama);
                                }
                                catch { }
                                
                            }
                            break;
                        }
                        break;
                    case "S"://trama simple 
                        break;
                    case "T"://ticket
                        MostrarLecturaContingencia(ACS_Define.Tip_Ticket, htBoarding);
                        //if (VerificarUsuarioPINPAD((string)htBoarding["NroTicket"]))
                        //{
                        //    Obj_IntzPinPad.IniciarPinPad();
                        //}
                        //else
                        //{
                        ProcesarTramaTicket((string)htBoarding["NroTicket"]);
                        //}
                        break;
                    case LAP.TUUA.CONVERSOR.Define.PinPad://pinpad
                        if (VerificarUsuarioPINPAD((string)htBoarding["NroTicket"]))
                        {
                            Obj_IntzPinPad.IniciarPinPad();
                        }
                        break;
                    case "D":
                        //Trama Supervisor Molinete Discapacitados
                        string strTramaNueva, strCodUsuario, strCodUsu;
                        int PosTramaIni;
                        long nRpta;
                        //PosTramaIni = strTrama.IndexOf(ACS_Define.PREFSUPER);
                        PosTramaIni = strTrama.IndexOf((string)Property.htProperty["PREFDESTRABE"]);
                        strTramaNueva = strTrama.Substring(PosTramaIni, 9);
                        strCodUsuario = strTramaNueva.Substring(3, 6);
                        nRpta = 0;
                        string strCuenta = "";
                        if (long.TryParse(strCodUsuario, out nRpta))
                        {
                            if (VerificarSupervisor(strTramaNueva))
                            {
                                if (ValidarSupervisor(strTramaNueva, ref strCuenta))
                                {
                                    strCodUsu = "U" + strTramaNueva.Substring(3, strTramaNueva.Length - 3);
                                    //Validar indicador de estado de llave  de destrabe
                                    System.Threading.ThreadStart starter = null;
                                    System.Threading.Thread thMsg = null;

                                    try
                                    {
                                        starter = delegate { MostrarMsgConcurrente("03", "Usuario autorizado"); };
                                        thMsg = new System.Threading.Thread(starter);
                                        thMsg.Start();

                                        Obj_DestrabeMolinete.Cod_Acceso = strTramaNueva;
                                        Obj_DestrabeMolinete.Cod_Molinete = (string)ACS_Property.shtMolinete["Cod_Molinete"];
                                        Obj_DestrabeMolinete.Cod_Web = strCuenta;
                                        Obj_DestrabeMolinete.DestrabeMolinete();

                                        //Registrar Auditoria de Cambio de Molinete
                                        Obj_Resolver.CrearDAOAuditoria();
                                        Obj_Resolver.InsertarAuditoriaDestrabeMolinete((String)ACS_Property.shtMolinete["Cod_Molinete"], strCodUsu, ACS_Define.ID_OPER_DESTRAB_OK);

                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    finally
                                    {
                                        thMsg.Join();
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        strCodUsu = "U" + strTramaNueva.Substring(3, strTramaNueva.Length - 3);
                                        Obj_Resolver.CrearDAOAuditoria();
                                        //Auditoria
                                        Obj_Resolver.InsertarAuditoriaDestrabeMolinete((String)ACS_Property.shtMolinete["Cod_Molinete"], strCodUsu, ACS_Define.ID_OPER_DESTRAB_ERROR);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                        }
                        else
                        {
                            //trama ilegible
                            string strDscErr = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPIleg, Lst_ListaDeCampo, "ErrorTicket");
                            ACS_Boarding objACSBoarding = new ACS_Boarding(null, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad, Obj_Util, Obj_Resolver,
                                                                                 Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                            objACSBoarding.MostrarError(strTrama, ACS_Define.Cod_ErrBCBPIleg, strDscErr);
                            Obj_Util.EscribirLog(this, strDscErr);
                        }
                        System.Threading.Thread.Sleep(1500);
                        break;

                    case "C":
                        //Para cambiar Tipo de Vuelo en el Molinete
                        string strTramaNueva2, strCodUsuario2, strCodUsu2, strTipoVuelo;
                        int PosTramaIni2;
                        long nRpta2;
                        //PosTramaIni = strTrama.IndexOf(ACS_Define.PREFSUPER);
                        PosTramaIni2 = strTrama.IndexOf((string)Property.htProperty["PREFCAMBIMOLINETE"]);
                        strTramaNueva2 = strTrama.Substring(PosTramaIni2, 9);
                        strCodUsuario2 = strTramaNueva2.Substring(3, 6);
                        strTipoVuelo = (String)ACS_Property.shtMolinete["Tip_Vuelo"];
                        nRpta2 = 0;
                        string strCuenta2 = "";
                        if (long.TryParse(strCodUsuario2, out nRpta))
                        {
                            if (VerificarSupervisor(strTramaNueva2))
                            {
                                if (ValidarSupervisorCambioTVMolinete(strTramaNueva2, ref strCuenta2)) // Mismo validador que valide que puede hacer cambio de molinete - CMONTES 31/08/2015
                                {
                                    strCodUsu2 = "U" + strTramaNueva2.Substring(3, strTramaNueva2.Length - 3);
                                    //Validar indicador de estado de llave  de cambio de Tipo de Vuelo de Molinete
                                    System.Threading.ThreadStart starter = null;
                                    System.Threading.Thread thMsg = null;

                                    try
                                    {
                                        starter = delegate { MostrarMsgConcurrente("03", "Cambio Tipo de Molinete autorizado"); };
                                        thMsg = new System.Threading.Thread(starter);
                                        thMsg.Start();

                                        if (ACS_Property.shtMolinete["Tip_Vuelo"] != null && ((String)ACS_Property.shtMolinete["Tip_Vuelo"]).Equals("I"))
                                        {
                                            strTipoVuelo = "N";
                                        }
                                        else
                                        {
                                            strTipoVuelo = "I";
                                        }


                                        starter = delegate { MostrarMsgConcurrente("03", "Cambio Tipo de Molinete autorizado: " + (String)ACS_Property.shtMolinete["Tip_Vuelo"]); };
                                        thMsg = new System.Threading.Thread(starter);
                                        thMsg.Start();
                                        //Registrar el cambio de Tipo de Molinete y poner el estado de por sincronizar
                                        //Se modifica para servidor local y remoto
                                        ACS_Property.shtMolinete["Tip_Vuelo"] = strTipoVuelo;
                                        Obj_Resolver.CrearDAOMolinete();

                                        Obj_Resolver.ActualizarTipoVueloMolinete((String)ACS_Property.shtMolinete["Cod_Molinete"], strTipoVuelo, strCodUsu2, ACS_Define.ID_OPER_CAMBMOL_OK);
                                        ACS_Property.shtMolinete["Tip_Vuelo"] = strTipoVuelo;
                                        //Registrar Auditoria de Cambio de Molinete
                                        //Obj_Resolver.InsertarAuditoriaModificarTipoMolinete((String)ACS_Property.shtMolinete["Cod_Molinete"], (String)ACS_Property.shtMolinete["Tip_Vuelo"], strCuenta2);

                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    finally
                                    {
                                        thMsg.Join();
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        strCodUsu2 = "U" + strTramaNueva2.Substring(3, strTramaNueva2.Length - 3);
                                        Obj_Resolver.CrearDAOMolinete();
                                        Obj_Resolver.ActualizarTipoVueloMolinete((String)ACS_Property.shtMolinete["Cod_Molinete"], strTipoVuelo, strCodUsu2, ACS_Define.ID_OPER_CAMBMOL_ERROR);
                                    }
                                    catch (Exception ex)
                                    {
                                    }                             
                                }
                            }
                        }
                        else
                        {
                            //trama ilegible
                            string strDscErr = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPIleg, Lst_ListaDeCampo, "ErrorTicket");
                            ACS_Boarding objACSBoarding = new ACS_Boarding(null, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad, Obj_Util, Obj_Resolver,
                                                                                 Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                            objACSBoarding.MostrarError(strTrama, ACS_Define.Cod_ErrBCBPIleg, strDscErr);
                            Obj_Util.EscribirLog(this, strDscErr);
                        }
                        System.Threading.Thread.Sleep(1500);
                        break;
                    case "":

                        //trama ilegible
                        string strDesDocumento = ""; ;
                        if (strTipDocumento.Equals("T"))
                            strDesDocumento = "Ticket";
                        if (strTipDocumento.Equals("B"))
                            strDesDocumento = "Boarding";
                        string strDscError = "Documento debe ser " + strDesDocumento;
                        ACS_Boarding objACSBoardings = new ACS_Boarding(null, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad, Obj_Util, Obj_Resolver,
                                                                             Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                        objACSBoardings.MostrarError(strTrama, ACS_Define.Cod_ErrBCBPIleg, strDscError);
                        Obj_Util.EscribirLog(this, strDscError);
                        // Thread.Sleep(30000);
                        break;

                    case LAP.TUUA.CONVERSOR.Define.FORM_BCBP_LAN1D:
                        System.Threading.Thread thWSBcbp1D = null;
                        try
                        {
                            //INICIO CAMPOS NUEVOS:NRO BOARDING,ETICKET,DESTINO
                            htBoarding.Add(ACS_Define.KEY_PASSANGER_NAME_WS, "");
                            htBoarding.Add(ACS_Define.CHECKIN_SEQUENCE_NUMBER, "0" + (string)htBoarding["passenger_name"]);
                            htBoarding.Add(ACS_Define.DESTINATION, "");
                            htBoarding.Add(ACS_Define.ELECTRONIC_TICKET_INDICATOR, "");
                            htBoarding.Add(ACS_Define.BAR_CODE_FORMAT, LAP.TUUA.CONVERSOR.Define.FORM_BCBP_LAN1D);
                            htBoarding.Add(ACS_Define.FROM_CITY_AIRPORT_CODE, (string)Property.htProperty[ACS_Define.FROM_CITY_AIRPORT_CODE.ToUpper()]); //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES

                            //FIN CAMPOS NUEVOS
                            Obj_Resolver.CrearDAOModVentaComp();
                            DataTable dtAtributos = Obj_Resolver.ListarAtributosWSAerolinea((string)htBoarding["airline_designator"]);
                            Hashtable htWSBcbp = CargarParametrosRequestWSBcbp(htBoarding, dtAtributos);
                            thWSBcbp1D = new System.Threading.Thread(new System.Threading.ThreadStart(ConsumirWebService));
                            thWSBcbp1D.Start();

                            htBoarding["date_flight"] = Obj_Util.ConvertirJulianoCalendario(Int32.Parse((string)htBoarding["date_flight"]));

                            //si es blanco campo asiento entonces se asume como infante
                            if ((string)htBoarding["seat_number"] == "")
                            {
                                htBoarding.Add("passenger_description", ACS_Define.TYPE_PASS_INFANT);
                            }

                            objReader.ParseHashtable(htBoarding);
                            MostrarLecturaContingencia(ACS_Define.Tip_Compuesta, htBoarding);

                            Obj_Resolver.CrearDAOBoardingBcbp();
                            Obj_BoardingBcbp = Obj_Resolver.obtenerDAOBoardingBcbp("0", (string)htBoarding["flight_number"],
                                                                   (string)htBoarding["date_flight"], (string)htBoarding["seat_number"],
                                                                   (string)htBoarding["passenger_name"], null, null);
                            ACS_Boarding objACSBcbp = new ACS_Boarding(htBoarding, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad,
                                                                           Obj_Util, Obj_Resolver, Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                            objACSBcbp.Lst_WSBcbp = htWSBcbp;
                            objACSBcbp.ValidarBCBP(Obj_BoardingBcbp, strTrama);
                        }
                        catch { }
                        finally
                        {
                            thWSBcbp1D.Join();
                        }
                        break;
                }
            }
            else
            {
                //trama ilegible
                string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPIleg, Lst_ListaDeCampo, "ErrorTicket");
                ACS_Boarding objACSBoarding = new ACS_Boarding(null, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad, Obj_Util, Obj_Resolver,
                                                                     Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                objACSBoarding.MostrarError(strTrama, ACS_Define.Cod_ErrBCBPIleg, strDscError);
                Obj_Util.EscribirLog(this, strDscError);
            }
            //else
            //{
            //    //Trama Supervisor Molinete Discapacitados
            //  string  strTramaNueva, strCodUsuario;
            //  int PosTramaIni;
            //  long nRpta;
            //  //PosTramaIni = strTrama.IndexOf(ACS_Define.PREFSUPER);
            //  PosTramaIni = strTrama.IndexOf((string)Property.htProperty["PREFSUPER"]);
            //  strTramaNueva = strTrama.Substring(PosTramaIni, 9);
            //  strCodUsuario = strTramaNueva.Substring(3, 6);
            //  nRpta = 0;
            //  string strCuenta = "";
            //  if (long.TryParse(strCodUsuario, out nRpta))
            //     {
            //       if (VerificarSupervisor(strTramaNueva))
            //        {
            //             if (ValidarSupervisor(strTramaNueva,ref strCuenta))
            //            {
            //                Obj_DestrabeMolinete.Cod_Acceso = strTramaNueva;
            //                Obj_DestrabeMolinete.Cod_Molinete = (string)ACS_Property.shtMolinete["Cod_Molinete"];
            //                Obj_DestrabeMolinete.Cod_Web = strCuenta;
            //                Obj_DestrabeMolinete.DestrabeMolinete();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //trama ilegible
            //        string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPIleg, Lst_ListaDeCampo, "ErrorTicket");
            //        ACS_Boarding objACSBoarding = new ACS_Boarding(null, Lst_Compania, Lst_ListaDeCampo, Obj_IntzPinPad, Obj_Util, Obj_Resolver,
            //                                                             Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
            //        objACSBoarding.MostrarError(strTrama, ACS_Define.Cod_ErrBCBPIleg, strDscError);
            //        Obj_Util.EscribirLog(this, strDscError);
            //    }
            //}
        }

        public bool VerificarUsuarioPINPAD(string strCodigo)
        {
            string strPrefijo = (string)Property.htProperty["CODIGOSUPERVISOR"];
            if (strCodigo.Substring(0, strPrefijo.Length).Equals(strPrefijo))
            {
                Obj_Resolver.CrearDAOUsuario();
                Usuario Obj_Usuario = Obj_Resolver.autenticar(strCodigo);
                if (Obj_Usuario != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void MostrarLecturaContingencia(int tipTrama, Hashtable htBoarding)
        {
            if (ACS_Property.modoContingencia)
            {
                switch (tipTrama)
                {
                    case 0: //BCBP multiple simple

                        frmContingencia.txtbVuelo.Text = (string)htBoarding["flight_number"];
                        frmContingencia.txtbFechaVuelo.Text = (string)htBoarding["date_flight"];
                        frmContingencia.txtbNroAsiento.Text = (string)htBoarding["seat_number"];
                        frmContingencia.txtbPasajero.Text = (string)htBoarding["passenger_name"];
                        frmContingencia.txtbAerolinea.Text = (string)htBoarding["airline_designator"];

                        break;
                    case 1: //BCBP simple  
                        break;
                    case 2: //ticket
                        frmContingencia.txtbTicket.Text = (string)(htBoarding["NroTicket"]);
                        break;
                }
            }
        }
        /// <summary>
        /// Procesa Trama Ticket
        /// </summary>
        /// <param name="strTicket">Numero de Ticket</param>
        public void ProcesarTramaTicket(String strTicket)
        {
            try
            {
                Obj_Resolver.CrearDAOTicket();
                Obj_Ticket = Obj_Resolver.obtenerTicket(strTicket);
                ACS_Ticket objACSTicket = new ACS_Ticket(Obj_IntzPinPad, Lst_ListaDeCampo, Lst_TipoTicket, Obj_Resolver,
                                                         Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);
                objACSTicket.Tip_Ingreso = ACS_Define.Cod_TipIngAuto;

                int iLongTicket = 16;
                if (ACS_Property.shtMolinete["Long_Ticket"] != null)
                {
                    iLongTicket = Convert.ToInt32(ACS_Property.shtMolinete["Long_Ticket"]);
                }

                if (strTicket != null && iLongTicket == 16)
                {

                    if (ACS_Property.BConRemota)
                    {
                        objACSTicket.ValidarTicket(Obj_Ticket, strTicket);
                    }
                    else
                    {
                        objACSTicket.ProcesarTicketOffLine(Obj_Ticket, strTicket);
                    }
                }
                else
                {
                    string msgRpta = "ERROR DE FORMATO;;" + strTicket + ";;NO ES VALIDO";
                    int Rpta = EnviarMsgPINPAD(ACS_Define.Cod_ConTickErr, msgRpta, "");
                    if (Rpta != 0)
                    {
                        if (Rpta == 2)
                        {
                            //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                            if (ACS_Property.bWriteErrorLog)
                                Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                        }

                        if (Rpta == 1)
                        {
                            //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                            if (ACS_Property.bWriteErrorLog)
                                Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                        }
                    }
                    Obj_Util.EscribirLog(this, msgRpta);
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public int EnviarMsgPINPAD(String strCodTransaccion, String strMsgDataAdicional,
                                    String strMsgDataTrack2)
        {
            try
            {
                Obj_PinPad.Clear();
                Obj_PinPad.SetField(ACS_Define.Cod_Aplicacion, "LAP");
                Obj_PinPad.SetField(ACS_Define.Cod_Transaccion, strCodTransaccion);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Adicional, strMsgDataAdicional);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Track2, strMsgDataTrack2);
                if (Obj_PinPad.SendTran() == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarTablasTemp()
        {
            try
            {
                Obj_Resolver.CrearDAOListaDeCampos();
                Obj_Resolver.CrearDAOCompania();
                Obj_Resolver.CrearDAOTipoTicket();
                //Agregado x jortega
                Obj_Resolver.CrearDAOUsuario();

                this.Lst_ListaDeCampo = Obj_Resolver.listarListaDeCampos();
                this.Lst_Compania = Obj_Resolver.listarCompania();
                this.Lst_TipoTicket = Obj_Resolver.listarTipoTicket();
                //Agregado por jortega
                this.Lst_Usuario = Obj_Resolver.listarUsuario();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool VerificarSupervisor(String strTicket)
        {
            if ((strTicket.Length != ACS_Define.LONG_COD_SUPER) && (strTicket.Substring(0, 2) != ACS_Define.PREFSUPER))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool ValidarSupervisor(String strTicket, ref string strCuenta)
        {

            string strCorrelativo = strTicket.Substring(3, strTicket.Length - 3); //000001<-SUP - SUP000001
            string strCodUsuario = "U" + strCorrelativo;//U000001
            string strCodRolSup = (string)Property.htProperty["CODROLSUPERVISOR"];
            //Validar que el supervisor este activado para poder hacer destrabe, definir campo indicador - CMONTES 31/08/2015
            bool boRpta = Obj_Util.VerificarUsuarioDestrabe(strCodUsuario, Obj_Resolver.ListarSupervisor(strCodRolSup, ACS_Define.TIP_USR_VIGENTE), ref strCuenta, ACS_Property.BConRemota);



            if (boRpta)
            {
                //MostrarMsgConcurrente("03", "Usuario autorizado");
                Obj_Util.EscribirLog(this, "Usuario autorizado, Destrabe Molinete");
                return boRpta;
            }
            else
            {
                //MostrarMsgConcurrente("03", "Usuario no autorizado");
                Obj_Util.EscribirLog(this, "Usuario no autorizado, No se Destraba Molinete");
                return boRpta;
            }
        }

        public bool ValidarSupervisorCambioTVMolinete(String strTicket, ref string strCuenta)
        {

            string strCorrelativo = strTicket.Substring(3, strTicket.Length - 3); //000001<-SUP - SUP000001
            string strCodUsuario = "U" + strCorrelativo;//U000001
            string strCodRolSup = (string)Property.htProperty["CODROLSUPERVISOR"];
            //Validar que el supervisor este activado para poder hacer destrabe, definir campo indicador - CMONTES 31/08/2015
            bool boRpta = Obj_Util.VerificarUsuarioCambioTVMolinete(strCodUsuario, Obj_Resolver.ListarSupervisor(strCodRolSup, ACS_Define.TIP_USR_VIGENTE), ref strCuenta, ACS_Property.BConRemota);



            if (boRpta)
            {
                //MostrarMsgConcurrente("03", "Usuario autorizado");
                Obj_Util.EscribirLog(this, "Usuario autorizado, cambia el tipo de vuelo molinete");
                return boRpta;
            }
            else
            {
                //MostrarMsgConcurrente("03", "Usuario no autorizado");
                Obj_Util.EscribirLog(this, "Usuario no autorizado, no cambia el tipo de vuelo molinete");
                return boRpta;
            }
        }

        private void MostrarMsgConcurrente(string strCodTrans, string strMsg)
        {
            if (ACS_Property.estadoPinPad)
            {
                int Rpta = Obj_IntzPinPad.MostrarMsgPINPad(strCodTrans, strMsg.ToUpper(), "");
                if (Rpta == 2)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                    Obj_IntzPinPad.Obj_PinPad.Serial.Close();
                    Obj_IntzPinPad.Obj_PinPad.Serial.Open();
                }

                if (Rpta == 1)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                }
            }
        }

        //Verifica conexion remota y local
        /// <summary>
        /// Verifica conexion de BD 
        /// </summary>
        private void VerificarConexionRemota(object source, System.Timers.ElapsedEventArgs e)
        {
            DAO_BaseDatos objDAO_BaseDatos;
            string sqlQuery = "select getdate()";

            System.Data.IDataReader reader = null;
            try
            {
                if (ACS_Property.shelper == null)
                {

                    objDAO_BaseDatos = new DAO_BaseDatos();
                    objDAO_BaseDatos.Conexion("tuuacnx");
                    ACS_Property.shelper = objDAO_BaseDatos.helper;

                }

                reader = ACS_Property.shelper.ExecuteReader(System.Data.CommandType.Text, sqlQuery);
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

            //Thread.Sleep(20000);
            string sqlQuery = "select getdate()";
            DAO_BaseDatos objDAO_BaseDatos;
            System.Data.IDataReader reader = null;
            try
            {
                if (ACS_Property.shelperLocal == null)
                {
                    objDAO_BaseDatos = new DAO_BaseDatos();
                    objDAO_BaseDatos.Conexion("tuuacnxlocal");
                    ACS_Property.shelperLocal = objDAO_BaseDatos.helper;
                }

                string aa = ACS_Property.shelperLocal.ConnectionString;

                reader = ACS_Property.shelperLocal.ExecuteReader(System.Data.CommandType.Text, sqlQuery);
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



        #endregion

        private Hashtable CargarParametrosRequestWSBcbp(Hashtable htBcbp, DataTable dtAtributos)
        {
            Hashtable htWSBcbp = new Hashtable();
            htWSBcbp.Add("date_flight", htBcbp["date_flight"]);
            htWSBcbp.Add("airline_designator", htBcbp["airline_designator"]);
            htWSBcbp.Add("flight_number", htBcbp["flight_number"]);
            htWSBcbp.Add(ACS_Define.CHECKIN_SEQUENCE_NUMBER, htBcbp[ACS_Define.CHECKIN_SEQUENCE_NUMBER].ToString().Trim());
            htWSBcbp.Add("tuua_manager_airport", "LIM");
            htWSBcbp.Add("tuua_character", (string)Property.htProperty["WS_CARACTER_TUUA"]);
            htWSBcbp.Add("flg_transfer_passenger", "");
            htWSBcbp.Add("flg_paid_tuua", "");
            htWSBcbp.Add("NroTicket", "");
            htWSBcbp.Add(ACS_Define.KEY_WS_RESPONSE, false);
            htWSBcbp.Add(ACS_Define.KEY_WS_ERROR_MSG, "");
            htWSBcbp.Add(ACS_Define.KEY_WS_TRAMA_RESPONSE, "");
            htWSBcbp.Add(ACS_Define.KEY_PASSANGER_NAME_WS, htBcbp[ACS_Define.KEY_PASSANGER_NAME_WS]);
            //parametros WS
            for (int i = 0; i < dtAtributos.Rows.Count; i++)
            {
                htWSBcbp.Add(dtAtributos.Rows[i].ItemArray.GetValue(0), dtAtributos.Rows[i].ItemArray.GetValue(1));
            }
            Lst_WSBcbp = htWSBcbp;
            CrearWSErrorMensaje(5, null);
            return htWSBcbp;
        }

        private String ConstruirTramaRequestWSBcbp()
        {
            string strNroBcbp = Lst_WSBcbp[ACS_Define.CHECKIN_SEQUENCE_NUMBER].ToString().Trim();
            if (strNroBcbp.Length > 4)
            {
                strNroBcbp = strNroBcbp.Substring(strNroBcbp.Length - 4);
            }
            return
            Lst_WSBcbp["date_flight"].ToString() + Lst_WSBcbp["airline_designator"].ToString().Trim().Substring(0, 2) + Lst_WSBcbp["flight_number"].ToString().Trim().Substring(0, 4) +
            strNroBcbp + Lst_WSBcbp["tuua_manager_airport"].ToString() + Lst_WSBcbp["tuua_character"].ToString() + Lst_WSBcbp[ACS_Define.KEY_PASSANGER_NAME_WS].ToString();
        }

        private void ConsumirWebService()
        {
            string strNomServicio = "";
            string strMetodo = "";
            string strProtocolo = "";
            string strRuta = "";
            int intTipError = 0;
            if (Lst_WSBcbp[ACS_Define.ID_VAL_WS] != null && Lst_WSBcbp[ACS_Define.ID_VAL_WS].ToString().Trim().Equals(ACS_Define.FLG_SI_PAGO_TUUA))
            {
                try
                {
                    String strResponse = "";
                    strNomServicio = Lst_WSBcbp[ACS_Define.ID_NOM_SERVICIO].ToString();
                    strMetodo = Lst_WSBcbp[ACS_Define.ID_NOM_METODO].ToString();
                    strProtocolo = Lst_WSBcbp[ACS_Define.ID_NOM_PROTOCOLO].ToString();
                    strRuta = Lst_WSBcbp[ACS_Define.ID_NOM_RUTA].ToString();
                    object[] arArg = new object[1];
                    arArg[0] = ConstruirTramaRequestWSBcbp();

                    //WSBCBP.TUUAService objWSBCBP = new LAP.TUUA.ACCESOS.WSBCBP.TUUAService();
                    //INTZ_WSBCBP objWSBCBP = new INTZ_WSBCBP(strRuta, strNomServicio, strMetodo, strProtocolo, arArg);
                    Int32 intSleep = Int32.Parse((string)Property.htProperty["TIMEOUT_WSLAN"]);
                    //objWSBCBP.Timeout = intSleep;
                    //object obj = objWSBCBP.PagoTUUA(arArg[0].ToString());

                    object obj = INTZ_ProxyWS.ObtenerWebService(strRuta, strNomServicio, strMetodo, strProtocolo, arArg, intSleep,ref intTipError);
                    //object obj = INTZ_ProxyWS.ObtenerWebService("http://www.lp.com.pe//SITUUA/TUUAService.asmx?wsdl", "TUUAService", "PagoTUUA", "Soap", arArg);

                    if (obj != null)
                    {
                        strResponse = (String)obj;
                        Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", strResponse);
                        Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", "Fin WEB SERVICE BP");
                        ProcesarResponse(strResponse);
                    }
                    else
                    {
                        CrearWSErrorMensaje(intTipError, null);
                    }


                }
                catch (Exception exc)
                {
                    Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", strNomServicio + " " + strMetodo + " " + strProtocolo + " " + strRuta);
                    Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", exc.Message);
                    Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", exc.StackTrace);
                    CrearWSErrorMensaje(intTipError, null);
                }
            }
        }

        private void ProcesarResponse(String strResponse)
        {
            if (!strResponse.Equals("") && strResponse.Length == 15)
            {
                Lst_WSBcbp[ACS_Define.KEY_WS_RESPONSE] = true;
                Lst_WSBcbp["flg_paid_tuua"] = strResponse.Substring(1, 1);
                Lst_WSBcbp["flg_transfer_passenger"] = strResponse.Substring(0, 1);
                Lst_WSBcbp["NroTicket"] = strResponse.Substring(2, 13);
                Lst_WSBcbp[ACS_Define.KEY_WS_TRAMA_RESPONSE] = strResponse;
            }
            else
            {
                CrearWSErrorMensaje(2, strResponse);
            }
        }

        private void CrearWSErrorMensaje(int intTipo, string strTrama)
        {
            switch (intTipo)
            {
                case 0:
                    Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG] = "ERROR - WS NO DISPONIBLE O SIN CONEXION";
                    break;
                case 1:
                    Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG] = "ERROR - WS TRAMA VACIA";
                    break;
                case 2:
                    Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG] = "ERROR - WS TRAMA INVALIDA - " + strTrama;
                    break;
                case 3:
                    Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG] = "WEB SERVICE - " + strTrama;
                    break;
                default:
                    Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG] = "ERROR - WS TIEMPO ESPERA AGOTADO";
                    break;
            }
        }

        private void AnalizarSegmentosBCBP(ArrayList arrLst)
        {
            string strFecVuelo = "";
            string strNumVuelo = "";
            string strSeatNumber = "";
            string strAirDesignator = "";
            string strPassStatus = "";
            string strFromCityAirportCode = string.Empty; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
            for (int i = 0; i < arrLst.Count; i++)
            {
                Hashtable htSegmento = (Hashtable)arrLst[i];
                if (htSegmento[ACS_Define.FROM_CITY_AIRPORT_CODE].Equals((string)Property.htProperty[ACS_Define.FROM_CITY_AIRPORT_CODE.ToUpper()]))
                {
                    strFecVuelo = (string)htSegmento[ACS_Define.DATE_FLIGHT];
                    strNumVuelo = (string)htSegmento[ACS_Define.FLIGHT_NUMBER];
                    strSeatNumber = (string)htSegmento[ACS_Define.SEAT_NUMBER];
                    strAirDesignator = (string)htSegmento[ACS_Define.AIRLINE_DESIGNATOR];
                    strPassStatus = (string)htSegmento[ACS_Define.PASSENGER_STATUS];
                    strFromCityAirportCode = (string)htSegmento[ACS_Define.FROM_CITY_AIRPORT_CODE]; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
                    ((Hashtable)arrLst[0])[ACS_Define.DATE_FLIGHT] = strFecVuelo;
                    ((Hashtable)arrLst[0])[ACS_Define.FLIGHT_NUMBER] = strNumVuelo;
                    ((Hashtable)arrLst[0])[ACS_Define.SEAT_NUMBER] = strSeatNumber;
                    ((Hashtable)arrLst[0])[ACS_Define.AIRLINE_DESIGNATOR] = strAirDesignator;
                    ((Hashtable)arrLst[0])[ACS_Define.PASSENGER_STATUS] = strPassStatus;
                    ((Hashtable)arrLst[0])[ACS_Define.FROM_CITY_AIRPORT_CODE] = strFromCityAirportCode; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
                    return;
                }                                                   
            }
        }

    }
}


