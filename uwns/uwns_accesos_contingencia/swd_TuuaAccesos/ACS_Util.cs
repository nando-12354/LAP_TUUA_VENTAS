///File: ACS_Util.cs
///Proposito:Define Metodos de uso general
///Metodos: 
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:15/07/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:20/07/2009


using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.Design.TraceListeners;
using System.Data;

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using System.Globalization;

namespace LAP.TUUA.ACCESOS
{
    /// <summary>
    /// ACCESOS: Util
    /// </summary>
    class ACS_Util
    {
        private DateTime dt_Fecha;

        public String ObtenerDscListaDeCampos(string strCodCampo, List<ListaDeCampo> Lst_ListaDeCampo,
                                       string strNomCampo)
        {
            string strDscListaDeCampos = "";

            foreach (ListaDeCampo objListaDeCampo in Lst_ListaDeCampo)
            {
                if (objListaDeCampo.SCodCampo.Trim().Equals(strCodCampo.Trim()) &&
                    objListaDeCampo.SNomCampo.Trim().Equals(strNomCampo)
                   )
                {
                    strDscListaDeCampos = objListaDeCampo.SDscCampo;
                    break;
                }
            }

            return strDscListaDeCampos;

        }

        /// <summary>
        /// Valida Fecha de acceso
        /// </summary>
        /// <param name="strFecha"></param>
        /// <returns></returns>
        public String ValidarAcceso(string strFecha)
        {
            string strRpta = "";
            if (strFecha != null && strFecha.Length == 8)
            {
                string strFechaHoy = DateTime.Now.Year.ToString()
                                      + ConvertirDosDigitos(DateTime.Now.Month)
                                      + ConvertirDosDigitos(DateTime.Now.Day);
                if (strFechaHoy.CompareTo(strFecha) > 0)
                {
                    strRpta = "Fecha de Vuelo Expirada";
                }
            }
            return strRpta;
        }

        /// <summary>
        /// Obtiene el numero de ticket
        /// </summary>
        /// <param name="strTrama">trama</param>
        /// <returns>numero ticket</returns>
        public String ObtenerTicket(string strTrama)
        {
            try
            {
                String strTicket = "";
                long lngTicket;

                if (strTrama != null && strTrama.Trim().Length >= ACS_Define.Long_MinTicket)
                {
                    strTrama = strTrama.Substring(4, 10);
                    if (long.TryParse(strTrama, out lngTicket) && strTrama.Trim().Length == 10)
                    {
                        strTicket = strTrama.Trim();

                    }
                }
                return strTicket;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Escribe en archivo log
        /// </summary>
        /// <param name="objClase">Instancia de Clase</param>
        /// <param name="strMensaje">Mensaje</param>
        public void EscribirLog(Object objClase, String strMensaje)
        {
            try
            {
                //SetTraceLogPath();
                LogEntry leMsg = new LogEntry();
                string hora = DateTime.Now.ToString("dd/MM/yyyy H:mm:ss:fff");
                leMsg.Title = hora;
                leMsg.ProcessName = objClase.ToString();
                leMsg.Message = strMensaje;
                Logger.Write(leMsg);
            }
            catch (Exception)
            {
            }
        }

        public void SetTraceLogPath()
        {
            CultureInfo culturaPeru = new CultureInfo("es-PE");
            System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
            string ConfigFilePath = AppDomain.CurrentDomain.BaseDirectory + "log/";
            string strFecha = DateTime.Now.ToShortDateString();
            //string strFileName = "";
            strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
            //ConfigurationFileMap objConfigPath = new ConfigurationFileMap();
            //objConfigPath.MachineConfigFilename = ConfigFilePath;

            Configuration entLibConfig = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            LoggingSettings loggingSettings = (LoggingSettings)entLibConfig.GetSection(LoggingSettings.SectionName);

            FlatFileTraceListenerData objFlatFileTraceListenerData = loggingSettings.TraceListeners.Get("Flat File Destination") as FlatFileTraceListenerData;
            //strFileName = objFlatFileTraceListenerData.FileName;
            //strFileName = strFileName.Substring(0, strFileName.Length - 4) + strFecha + strFileName.Substring(strFileName.Length - 4);
            if (!objFlatFileTraceListenerData.FileName.Equals(ConfigFilePath + "accesos" + strFecha + ".log"))
            {
                System.Threading.Monitor.Enter(entLibConfig);
                objFlatFileTraceListenerData.FileName = ConfigFilePath + "accesos" + strFecha + ".log";
                //objFlatFileTraceListenerData.FileName = strFileName;
                entLibConfig.Save();
                System.Threading.Monitor.Exit(entLibConfig);
            }
        }

        public static void Escribir(Object objClase, String strMensaje)
        {
            try
            {
                LogEntry leMsg = new LogEntry();
                string hora = DateTime.Now.TimeOfDay.ToString();
                if (hora.Length >= 12)
                {
                    leMsg.Title = hora.Substring(0, 12);
                    leMsg.ProcessName = objClase.ToString();
                    leMsg.Message = strMensaje;
                    Logger.Write(leMsg);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Escribe Mensaje en Consola
        /// </summary>
        public void EscribirMensaje(String strMensaje)
        {
            dt_Fecha = DateTime.Now;
            Console.WriteLine(GenerarFormato(dt_Fecha.Hour) + ":" + GenerarFormato(dt_Fecha.Minute) + ":" + GenerarFormato(dt_Fecha.Second) + " " + strMensaje);
        }

        /// <summary>
        /// Da formato a dos digitos
        /// </summary>
        /// <param name="intValor">Valor entero</param>
        /// <returns>Cadena de 2 digitos</returns>
        private String GenerarFormato(int intValor)
        {
            String strValor = intValor.ToString();
            if (intValor < 10)
            {
                strValor = "0" + intValor.ToString();

            }
            return strValor;
        }


        public String ObtenerDscCompania(String strCodCompania, List<Compania> lstCompania)
        {
            String strDscCompania = "";

            foreach (Compania objCompania in lstCompania)
            {
                if (objCompania.SCodCompania.Equals(strCodCompania.Trim()))
                {
                    strDscCompania = objCompania.SDscCompania;

                }
            }

            return strDscCompania;

        }

        /// <summary>
        /// Obtiene el codigo de la compañia 
        /// </summary>
        /// <param name="strAirlineDesign">Codigo de aerolina del BCBP</param>
        /// <param name="lstCompania">Lista de compañias </param>
        /// <returns>codigo de compañia</returns>
        public String ObtenerCodCompania(String strAirlineDesign, List<Compania> lstCompania)
        {
            String strCodCompania = "";

            foreach (Compania objCompania in lstCompania)
            {
                if (strAirlineDesign != null && (objCompania.SCodEspecialCompania.Trim().Equals(strAirlineDesign.Trim()) || objCompania.SCodCompania.Trim().Equals(strAirlineDesign.Trim())))
                {
                    strCodCompania = objCompania.SCodCompania;
                }
            }

            return strCodCompania;

        }

        /// <summary>
        /// Obtiene el tipo de vuelo
        /// </summary>
        /// <param name="strCodTipTicket"></param>
        /// <param name="Lst_TipoTicket"></param>
        /// <returns></returns>
        public string ObtenerTipoVuelo(string strCodTipTicket, List<TipoTicket> Lst_TipoTicket)
        {
            try
            {
                string strTipVuelo = "";

                foreach (TipoTicket objTipoTicket in Lst_TipoTicket)
                {
                    if (objTipoTicket.SCodTipoTicket.Equals(strCodTipTicket))
                    {
                        strTipVuelo = objTipoTicket.STipVuelo;
                        break;

                    }
                }

                return strTipVuelo;
            }
            catch (Exception e)
            {
                throw e;

            }

        }

        public string ObtenerDscTipTicket(string strCodTipTicket, List<TipoTicket> Lst_TipoTicket)
        {
            string strTipTicket = "";
            foreach (TipoTicket objTipoTicket in Lst_TipoTicket)
            {
                if (objTipoTicket.SCodTipoTicket.Equals(strCodTipTicket))
                {
                    if (objTipoTicket.STipVuelo.Trim().Equals("N"))
                        strTipTicket = "Nacional";
                    else
                        if (objTipoTicket.STipVuelo.Trim().Equals("I"))
                            strTipTicket = "Internacional";
                        else
                            strTipTicket = "Otros";

                    break;
                }
            }
            return strTipTicket;

        }


        public string ObtenerPasajTipTicket(string strCodTipTicket, List<TipoTicket> Lst_TipoTicket)
        {
            string strTipTicket = "";
            foreach (TipoTicket objTipoTicket in Lst_TipoTicket)
            {
                if (objTipoTicket.SCodTipoTicket.Equals(strCodTipTicket))
                {
                    if (objTipoTicket.STipPasajero.Trim().ToUpper().Equals("I"))
                        strTipTicket = "Infante";
                    else
                        if (objTipoTicket.STipPasajero.Trim().ToUpper().Equals("A"))
                            strTipTicket = "Adulto";
                        else
                            strTipTicket = "Otros";
                    break;
                }
            }
            return strTipTicket;

        }



        /// <summary>
        /// Obtiene tipo de pasajero
        /// </summary>
        /// <param name="strCodTipTicket">Cod. Tipo Ticket</param>
        /// <param name="Lst_TipoTicket">Lista Tipo Ticket</param>
        /// <returns>Tipo de Pasajero</returns>
        public string ObtenerTipoPasajero(string strCodTipTicket, List<TipoTicket> Lst_TipoTicket)
        {
            try
            {
                string strTiPasajero = "";

                foreach (TipoTicket objTipoTicket in Lst_TipoTicket)
                {
                    if (objTipoTicket.SCodTipoTicket.Equals(strCodTipTicket))
                    {
                        strTiPasajero = objTipoTicket.STipPasajero;
                        break;

                    }
                }

                return strTiPasajero;
            }
            catch (Exception e)
            {
                throw e;

            }

        }


        /// <summary>
        /// Obtiene descripcion del estado del ticket
        /// </summary>
        /// <param name="strTipEstado">Tipo de Estado</param>
        /// <param name="Lst_ListaDeCampo">Lista de Campos Generales</param>
        /// <returns>Descripcion del estado de ticket</returns>
        public string ObtenerEstadoTicket(string strTipEstado, List<ListaDeCampo> Lst_ListaDeCampo)
        {
            try
            {
                string strDesEstadoTicket = "";

                foreach (ListaDeCampo objListaDeCampo in Lst_ListaDeCampo)
                {
                    if (objListaDeCampo.SCodCampo.Trim().Equals(strTipEstado.Trim()) &&
                        objListaDeCampo.SNomCampo.Trim().Equals("EstadoTicket")
                       )
                    {

                        strDesEstadoTicket = objListaDeCampo.SDscCampo;
                        break;

                    }
                }

                return strDesEstadoTicket;
            }
            catch (Exception)
            {
                throw;
            }

        }



        /// <summary>
        /// Obtiene el codigo del tipo de error
        /// </summary>
        /// <param name="strTipoError"></param>
        /// <param name="Lst_ListaDeCampo"></param>
        /// <returns>Codigo de campo </returns>
        public String ObtenerTipoError(String strTipoError, List<ListaDeCampo> Lst_ListaDeCampo)
        {
            String strCodCampo = "";

            foreach (ListaDeCampo objListaDeCampo in Lst_ListaDeCampo)
            {
                if (objListaDeCampo.SDscCampo.Trim().Equals(strTipoError.Trim()))
                {
                    strCodCampo = objListaDeCampo.SCodCampo;

                }
            }
            return strCodCampo;
        }

        /// <summary>
        /// Genera formato de boarding a mostrar en PINPad
        /// </summary>
        /// <param name="objTicket"></param>
        /// <returns></returns>
        public string[] GenerarDetalleTicket(Ticket objTicket, List<TipoTicket> Lst_TipoTicket, List<Compania> Lst_Compania, DataTable dtTicketEstHist)
        {
            /*"NRO TICKET:TIPO TICKET:TIPO PERSONA:EMPRESA:NRO VUELO:ESTADO:FECHA VCTO:
              FECHA VENTA*/
            //string msgResp = "111111111111111;INTER;ADULT;AMERICAN AIR;AAA11111;USAD;25/12/09 12:00;25/12/09 12:00;;;REHA 24/02/0";
            //string strTrack2 = "6 56:30;REHA 24/02/09 12:30;REHA 24/02/09 12:30;USAD 24/02/09 12:30;REHA 24/02/09 12:30";

            string strDataAdic = "";
            string strTrack2 = "";
            string strNroTicket = objTicket.SCodNumeroTicket.Trim();
            string strTipTicket = ObtenerDscTipTicket(objTicket.SCodTipoTicket.Trim(), Lst_TipoTicket).Trim().ToUpper();
            string strTipPersona = ObtenerPasajTipTicket(objTicket.SCodTipoTicket.Trim(), Lst_TipoTicket).Trim().ToUpper();
            string strCompania = ObtenerDscCompania(objTicket.SCodCompania, Lst_Compania).Trim();
            string strNroVuelo = objTicket.SDscNumVuelo;
            string strEstado = objTicket.STipEstadoActual;
            string strFechVcto = objTicket.DtFchVencimiento;
            string strFechCrea = objTicket.DtFchCreacion;

            string strSeparador = ";;";
            if (strNroTicket.Trim().Length > 16)
                strNroTicket = strNroTicket.Trim().Substring(0, 16);
            if (strTipTicket.Trim().Length > 5)
                strTipTicket = strTipTicket.Trim().Substring(0, 5);
            if (strTipPersona.Length > 5)
                strTipPersona = strTipPersona.Substring(0, 5);
            if (strCompania.Length > 12)
                strCompania = strCompania.Substring(0, 12);
            if (strNroVuelo.Length > 8)
                strNroVuelo = strNroVuelo.Substring(0, 8);
            if (strFechVcto.Trim() != "")
            {
                strFechVcto = ACS_Util.ConvertirFecha(strFechVcto) + " 23:59";
            }
            strFechCrea = ACS_Util.ConvertirFecha(strFechCrea) + " " + ACS_Util.ConvertirHora(objTicket.SHor_Creacion);

            strDataAdic = strNroTicket + ";" + strTipTicket + ";" + strTipPersona + ";" + strCompania + ";" + strNroVuelo + ";" +
                          strEstado + ";" + strFechVcto + ";" + strFechCrea + strSeparador;


            int i = 1;
            foreach (DataRow dr in dtTicketEstHist.Rows)
            {
                if (i < 6)
                {
                    string strEst = dr["Dsc_Ticket_Estado"].ToString().Trim();
                    if (strEst.Trim().Length > 4)
                        strEst = strEst.Substring(0, 4);

                    string strFecha = ACS_Util.ConvertirFecha(dr["Log_Fecha_Mod"].ToString().Trim());
                    string strHora = ACS_Util.ConvertirHora(dr["Log_Hora_Mod"].ToString().Trim());
                    if ((strDataAdic.Length + 1) <= ACS_Define.Long_DatAdicional)
                    {
                        strDataAdic = strDataAdic + ";" + strEst + " " + strFecha + " " + strHora;
                        if (strDataAdic.Length > ACS_Define.Long_DatAdicional)
                        {
                            strTrack2 = strDataAdic.Substring(ACS_Define.Long_DatAdicional);
                            strDataAdic = strDataAdic.Substring(0, ACS_Define.Long_DatAdicional);
                        }
                    }
                    else
                    {
                        strTrack2 = strTrack2 + ";" + strEst + " " + strFecha + " " + strHora;
                    }

                    i++;
                }
                else
                {
                    break;
                }
            }

            if (strTrack2.Length > ACS_Define.Long_Track2)
                strTrack2 = strTrack2.Substring(0, ACS_Define.Long_Track2);
            string[] arrRpta = new string[2] { strDataAdic, strTrack2 };

            return arrRpta;
        }


        /// <summary>
        /// Invierte fecha 
        /// </summary>
        /// <param name="strFecha">yyyyMMdd</param>
        /// <returns></returns>
        public static string ConvertirFecha(String strFecha)
        {
            try
            {
                String strFech = "";
                strFech = strFecha.Substring(6, 2) + "/" + strFecha.Substring(4, 2) + "/" + strFecha.Substring(2, 2);
                return strFech;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Da Formato a Hora 
        /// </summary>
        /// <param name="strFecha">HHMMSS</param>
        /// <returns>HH:MM</returns>
        public static string ConvertirHora(String strHora)
        {
            try
            {
                String strFech = "";
                strFech = strHora.Substring(0, 2) + ":" + strHora.Substring(2, 2);
                return strFech;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Invierte fecha 
        /// </summary>
        /// <param name="strFecha">yyyyMMdd</param>
        /// <returns></returns>
        public String InvertirFecha(String strFecha)
        {
            try
            {
                String strFech = "";
                strFech = strFecha.Substring(6, 2) + strFecha.Substring(4, 2) + strFecha.Substring(0, 4);
                return strFech;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Covierte a Dos Digitos
        /// </summary>
        /// <param name="intValor"></param>
        /// <returns></returns>
        public static string ConvertirDigitos(int intValor)
        {
            String strRpta = "";
            if (intValor < 10)
                strRpta = "0" + intValor.ToString();
            else
                strRpta = intValor.ToString();

            return strRpta;
        }

        /// <summary>
        /// Convierte valor a Dos Digitos
        /// </summary>
        /// <param name="intValor"></param>
        /// <returns>valor de dos digitos cc</returns>

        public String ConvertirDosDigitos(int intValor)
        {
            String strRpta = "";
            if (intValor < 10)
                strRpta = "0" + intValor.ToString();
            else
                strRpta = intValor.ToString();

            return strRpta;
        }

        /// <summary>
        /// Obteniene fecha actual
        /// </summary>
        /// <returns>YYYYmmDD</returns>
        public String ObtenerFecha()
        {
            String strFecha = "";

            strFecha = DateTime.Now.Year.ToString() + ConvertirDosDigitos(DateTime.Now.Month) +
                     ConvertirDosDigitos(DateTime.Now.Day);
            return strFecha;
        }


        /// <summary>
        /// Obtiene la Hora del sistema
        /// </summary>
        /// <returns>hhmmss</returns>

        public String ObtenerHora()
        {
            String strHora = "";

            strHora = ConvertirDosDigitos(DateTime.Now.Hour) + ConvertirDosDigitos(DateTime.Now.Minute) +
                      ConvertirDosDigitos(DateTime.Now.Second);

            return strHora;
        }

        /// <summary>
        /// Convierte fecha jualiana a fecha calendario
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>

        public String ConvertirJulianoCalendario(int jd)
        {
            try
            {
                string strRpta = "";                             
                int intAnio = DateTime.Today.Year;
                Hashtable htMes = new Hashtable();

                htMes.Add("1", 31);
                if ((intAnio % 4) == 0)
                    htMes.Add("2", 29);
                else
                    htMes.Add("2", 28);
                htMes.Add("3", 31);
                htMes.Add("4", 30);
                htMes.Add("5", 31);
                htMes.Add("6", 30);
                htMes.Add("7", 31);
                htMes.Add("8", 31);
                htMes.Add("9", 30);
                htMes.Add("10", 31);
                htMes.Add("11", 30);
                htMes.Add("12", 31);

                int intMes = 0;
                int intDia = 0;
                do
                {
                    intMes++;
                    intDia = jd;
                    jd = jd - (int)htMes[intMes.ToString()];
                } while (jd > 0);

                strRpta = DateTime.Today.Year.ToString() + ConvertirDosDigitos(intMes) +
                          ConvertirDosDigitos(intDia);

                //LAP-TUUA-9163 - 21-06-2012 (RS) CMONTES - BEGIN 
                string strFechaHoy = ConvertirDosDigitos(DateTime.Now.Month) + ConvertirDosDigitos(DateTime.Now.Day);

                if (strFechaHoy.Equals(ACS_Define.DiaCambioAnio) &&
                    strRpta.Substring(4,4).Equals(ACS_Define.DiaInicioAnio))
                {
                    strRpta = (DateTime.Today.Year + 1).ToString() + strRpta.Substring(4, 4);
                }
                //LAP-TUUA-9163 - 21-06-2012 (RS) CMONTES - BEGIN 

                return strRpta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene Valor segun identificador
        /// </summary>
        /// <param name="lstParameGeneral">Tabla Parametro General</param>
        /// <param name="strID">Id Parametro General</param>
        /// <returns>Valor</returns>
        public static string ObtenerParamGral(List<ParameGeneral> lstParameGeneral, string strID)
        {
            string strRpta = "";
            foreach (ParameGeneral obj in lstParameGeneral)
            {
                if (strID.Equals(obj.SIdentificador))
                {
                    strRpta = obj.SValor;
                    break;
                }
            }
            return strRpta;
        }
        //Agreago por jortega 01/06/2010
        /// <summary>
        /// Obtiene confirmación de usuario valido  
        /// </summary>
        /// <param name="strAirlineDesign">Codigo de Usauario</param>
        /// <param name="lstCompania">Lista de Usarios </param>
        /// <returns>true en caso que exista el Usuario</returns>
        //public bool VerificarUsuario(string strCodUsuario, List<Usuario> lstUsuario)
        public bool VerificarUsuario(string strCodUsuario, DataTable dtSupervisor, ref string strCuenta)
        {
            //bool bEstado=false;
            if (dtSupervisor == null)
            {
                return false;
            }
            for (int i = 0; i < dtSupervisor.Rows.Count; i++)
            {
                if (dtSupervisor.Rows[i].ItemArray.GetValue(1).ToString() == strCodUsuario)
                {
                    strCuenta = dtSupervisor.Rows[i].ItemArray.GetValue(9).ToString();
                    return true;
                }
            }
            return false;
        }

        public String ValidarVueloProgramado(VueloProgramado objVueloProg, string strUmbralHora,string strTipDemorado)
        {
            string strRpta = "";
            string strFecVuelo = "";
            double dUmbral = 0;
            string strVueloSup = "";
            string strFechaHoy = "";
            DateTime dtVuelo = new DateTime();
            DateTime dtVueloSup = new DateTime();
            if (objVueloProg.Fch_Vuelo == null)
            {
                return strRpta;
            }
            //el humbral de horas debe ser independiente del estado del vuelo
            //if (objVueloProg.Tip_Estado.Equals(strTipDemorado))
            //{
            strFecVuelo = objVueloProg.Fch_Vuelo;
            if (objVueloProg.Hor_Vuelo.Trim().Equals(""))
            {
                strFecVuelo = strFecVuelo + "0000";
            }
            else
            {
                strFecVuelo = strFecVuelo + objVueloProg.Hor_Vuelo.Trim();
            }
            dUmbral = Double.Parse(strUmbralHora);
            dtVuelo = DateTime.ParseExact(strFecVuelo, "yyyyMMddHHmm", CultureInfo.InvariantCulture);

            dtVueloSup = dtVuelo.AddHours(dUmbral);

            strVueloSup = dtVueloSup.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture);
            //}
            //else
            //{
            //strVueloSup = objVueloProg.Fch_Vuelo;
            //}

            strFechaHoy = DateTime.Now.Year.ToString() + ConvertirDosDigitos(DateTime.Now.Month)
                                  + ConvertirDosDigitos(DateTime.Now.Day);

            if (strFechaHoy.CompareTo(strVueloSup) > 0)
            {
                strRpta = "Fecha de Vuelo Expirada";
            }
            return strRpta;
        }

        /// <summary>
        /// Validacion de fecha de vuelo para el modo OffLine
        /// Solo se valida la expiracion de la fecha ya que no podemos validar el vuelo programado.
        /// </summary>
        /// <param name="strFechaVuelo"></param>
         /// <returns></returns>
        public bool ValidarVueloExpiradoOffline(String strFechaVuelo)
        {
            bool isRpta = true;
            string strVueloSup = String.Empty;
            string strFechaHoy = String.Empty;
            if (strFechaVuelo != null)
            {
                strVueloSup = strFechaVuelo;


                strFechaHoy = DateTime.Now.Year.ToString() + ConvertirDosDigitos(DateTime.Now.Month)
                                      + ConvertirDosDigitos(DateTime.Now.Day);

                if (strFechaHoy.CompareTo(strVueloSup) > 0)
                {
                    isRpta = false;
                }
            }
            else
            {
                isRpta = false;
            }


            return isRpta;
        }


    }
}
