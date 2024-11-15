using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using tuua_api.Dao;
using tuua_api.Entidades;
using tuua_api.Util;

namespace tuua_api.BL
{
    public class Controlador
    {
        public async Task ProcesarTrama(string trama, TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            Reader reader = new Reader();
            CompaniaDao companiaDao = new CompaniaDao();
            BoardingDao boardingDao = new BoardingDao();

            TUA_BoardingBcbp boarding;
            //determinar si es boardingpass, ticket, llave de destrabe y realizar validaciones
            if (!string.IsNullOrEmpty(trama.Trim()))
            {
                // convertir trama a mayusculas
                trama = trama.ToUpper();
                loger.Log("Iniciando lectura de trama", "info");
                loger.Log("Trama Recibida:   " + trama, "info");

                //procesar trama

                //FUENTE: ACS_Interfazlector.cs
                loger.Log("Inicio de conversion de trama", "info");
                Hashtable htBoarding = reader.ParseString_ACS("    " + trama,
                    ConfigurationManager.AppSettings["CODIGOSUPERVISOR"],
                    ConfigurationManager.AppSettings["PREFDESTRABE"],
                    ConfigurationManager.AppSettings["PREFCAMBIMOLINETE"]);
                loger.Log("Fin de conversion de trama", "info");

                if (htBoarding != null)
                {
                    loger.Log("Inicio de validaciones internas", "info");
                    string strTipDocumento = "0"; // todos los tipos de documento
                    string strFormatCode = (string)htBoarding["format_code"];
                    string strPrefijo = ConfigurationManager.AppSettings["CODIGOSUPERVISOR"];
                    switch (strFormatCode)
                    {
                        case "M": //trama multiple
                            ArrayList arrLst = (ArrayList)htBoarding["flight_information"];
                            AnalizarSegmentosBCBP(arrLst);
                            foreach (Hashtable objHT in arrLst)
                            {
                                string strNom = (string)htBoarding["passenger_name"];
                                strNom = strNom.Replace('/', ' ');
                                strNom = strNom.Replace('-', ' ');
                                strNom = strNom.Replace('&', ' ');

                                htBoarding["passenger_name"] = strNom;
                                objHT.Add("passenger_name", htBoarding["passenger_name"]);
                                objHT["date_flight"] =
                                    ConvertirJulianoCalendario(Int32.Parse((string)objHT["date_flight"]));

                                reader.ParseHashtable(objHT);
                                //obtener compania por codigo iata
                                TUA_Compania compania =
                                    await companiaDao.getCompaniaIATA((string)objHT["airline_designator"]);
                                if (compania == null)
                                {
                                    throw new Exception("Aerolínea no registrada");
                                }

                                string strCodCompania = compania.Cod_Compania;

                                objHT["passenger_description"] = (string)htBoarding["passenger_description"];
                                objHT.Add(Define.DESTINATION, objHT[Define.TO_CITY_AIRPORT_CODE]);
                                objHT.Add(Define.BAR_CODE_FORMAT, Define.FORM_BCBP_LAN2D);

                                String strChekingNumber = "0";
                                if (objHT["checkin_sequence_number"] != null &&
                                    (string)objHT["checkin_sequence_number"] != String.Empty)
                                {
                                    strChekingNumber = (string)objHT["checkin_sequence_number"];
                                }

                                //obtener boardingpass
                                boarding = await boardingDao.getBoarding(strCodCompania, (string)objHT["flight_number"],
                                    (string)objHT["date_flight"], (string)objHT["seat_number"],
                                    (string)htBoarding["passenger_name"], strChekingNumber);

                                await ValidarBCBP(boarding, objHT, trama, molinete);
                            }

                            break;
                        case "T": // ticket
                            await ProcesarTramaTicket((string)htBoarding["NroTicket"], molinete);
                            break;
                        case "D":
                            //llave de destrabe
                            //verificar que la llave de destrabe exista,verificar que la cuenta se encuentre activa
                            await ProcesarTramaDestrabe(trama, molinete);

                            //registrar uso de llave y retornar ok
                            //AUDITDESTRAB_INSERT

                            break;
                        case "":
                            throw new Exception("Trama Ilegible");
                    }
                }
                else
                {
                    throw new Exception("Trama Ilegible");
                }
            }
            else
            {
                throw new Exception("Trama Ilegible");
            }
        }

        public async Task ProcesarTramaTicket(string strTicket, TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            TicketDao ticketDao = new TicketDao();
            TUA_Ticket ticket = await ticketDao.obtenerTicket(strTicket);
            if (ticket == null)
            {
                throw new Exception("El Ticket no ha sido emitido");
            }

            int iLongTicket = Define.LongTicket;
            if (!string.IsNullOrEmpty(strTicket) && strTicket.Length == iLongTicket)
            {
                await ValidarTicket(ticket, strTicket, molinete, ticketDao);
            }
            else
            {
                string msgRpta = "Formato de Ticket no válido";
                loger.Log(msgRpta, "error");
                throw new Exception(msgRpta);
            }
        }

        public async Task ProcesarTramaDestrabe(string trama, TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            UsuariosDao dao = new UsuariosDao();

            List<TUA_Destrabe> ls = await dao.getLlavesDestrabe();

            if (ls != null && ls.Count > 0)
            {
                TUA_Destrabe des = ls.Where(t => t.Cod_Destrabe.Trim().ToLower() == trama.Trim().ToLower())
                    .FirstOrDefault();
                if (des == null)
                {
                    string msgRpta = "Llave de destrabe no encontrada";
                    loger.Log(msgRpta, "error");
                    throw new Exception(msgRpta);
                }

                //guardar uso de llave de destrabe

                await dao.registrarUsoDestrabe(des, molinete);
            }
            else
            {
                string msgRpta = "No se encontraron tramas en el servidor";
                loger.Log(msgRpta, "error");
                throw new Exception(msgRpta);
            }
        }


        public async Task ValidarTicket(TUA_Ticket ticket, string strTicket, TUA_Molinete molinete, TicketDao ticketDao)
        {
            Loger loger = new Loger();
            if (ticket == null)
            {
                throw new Exception("El ticket no ha sido emitido.");
            }

            List<TUA_TipoTicket> tipos = await ticketDao.obtenerTiposTicket();
            //validar tipo de vuelo de ticket
            string strTipoVuelo = tipos.Where(t => t.Cod_Tipo_Ticket == ticket.Cod_Tipo_Ticket).Select(t => t.Tip_Vuelo)
                .FirstOrDefault();
            if (!strTipoVuelo.Equals(molinete.Tip_Vuelo))
            {
                throw new Exception("Tipo de vuelo incorrecto, ingrese por otro molinete");
            }

            //validar estado de ticket
            if (ticket.Tip_Estado_Actual.Trim() != Define.Cod_EstTicEmitido
                && ticket.Tip_Estado_Actual.Trim() != Define.Cod_EstTicRehabilit &&
                ticket.Tip_Estado_Actual.Trim() != Define.Cod_EstTicPremitido)
            {
                throw new Exception("El ticket ya ha sido usado");
            }

            // validar fecha programada de ticket, insertar ticket en bd
            DateTime fechaProgramada =
                DateTime.ParseExact(ticket.Fch_Vencimiento, "yyyyMMdd", CultureInfo.InvariantCulture);

            if (DateTime.Today > fechaProgramada && ticket.Tip_Estado_Actual.Trim() != Define.Cod_EstTicPremitido)
            {
                // el ticket está vencido y ademas no ha sido preemitido
                throw new Exception("El ticket se encuentra vencido");
            }

            // guardar ticket
            ticket.Tip_Estado_Actual = Define.Cod_EstTicUsado;
            ticket.Log_Usuario_Mod = Define.Usr_Acceso;
            ticket.Cod_Equipo_Mod = molinete.Cod_Molinete;

            await ticketDao.actualizarTicket(ticket);
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
                if (htSegmento[Define.FROM_CITY_AIRPORT_CODE]
                    .Equals(ConfigurationManager.AppSettings[Define.FROM_CITY_AIRPORT_CODE.ToUpper()]))
                {
                    strFecVuelo = (string)htSegmento[Define.DATE_FLIGHT];
                    strNumVuelo = (string)htSegmento[Define.FLIGHT_NUMBER];
                    strSeatNumber = (string)htSegmento[Define.SEAT_NUMBER];
                    strAirDesignator = (string)htSegmento[Define.AIRLINE_DESIGNATOR];
                    strPassStatus = (string)htSegmento[Define.PASSENGER_STATUS];
                    strFromCityAirportCode =
                        (string)htSegmento[Define.FROM_CITY_AIRPORT_CODE]; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
                    ((Hashtable)arrLst[0])[Define.DATE_FLIGHT] = strFecVuelo;
                    ((Hashtable)arrLst[0])[Define.FLIGHT_NUMBER] = strNumVuelo;
                    ((Hashtable)arrLst[0])[Define.SEAT_NUMBER] = strSeatNumber;
                    ((Hashtable)arrLst[0])[Define.AIRLINE_DESIGNATOR] = strAirDesignator;
                    ((Hashtable)arrLst[0])[Define.PASSENGER_STATUS] = strPassStatus;
                    ((Hashtable)arrLst[0])[Define.FROM_CITY_AIRPORT_CODE] =
                        strFromCityAirportCode; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES
                    return;
                }
            }
        }

        public String ObtenerHora()
        {
            String strHora = "";

            strHora = ConvertirDosDigitos(DateTime.Now.Hour) + ConvertirDosDigitos(DateTime.Now.Minute) +
                      ConvertirDosDigitos(DateTime.Now.Second);

            return strHora;
        }

        private string ObtenerTipoPasajero(string strTipPasajero)
        {
            switch (strTipPasajero)
            {
                case Define.TYPE_PASS_CHILD: return Define.Cod_TipPasajInfante;
                case Define.TYPE_PASS_INFANT: return Define.Cod_TipPasajInfante;
                default: return Define.Cod_TipPasajAdulto;
            }
        }

        public String ValidarVueloProgramado(TUA_VueloProgramado objVueloProg, string strUmbralHora,
            string strTipDemorado)
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

        private void VerificarPassengerStatus(TUA_BoardingBcbp objBoarding, Hashtable Ht_Boarding)
        {
            String strStatusTransito = ConfigurationManager.AppSettings["STATUS_TRANSITO"];
            String strPassStatus = (String)Ht_Boarding[Define.PASSENGER_STATUS];
            objBoarding.Tip_Trasbordo = Define.NORMAL;

            if (strPassStatus.Equals(
                    strStatusTransito)) //Modificacion por problema de registro manual de boarding en pinpad 24/03/2011 eochoa
            {
                objBoarding.Tip_Trasbordo = Define.TRANSITO;
            }
        }


        public async Task ValidarBCBP(TUA_BoardingBcbp Obj_BoardingBcbp, Hashtable Ht_Boarding, String strTrama,
            TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            VueloProgramadoDao vueloProgramadoDao = new VueloProgramadoDao();
            CompaniaDao companiaDao = new CompaniaDao();
            BoardingDao boardingdao = new BoardingDao();
            MolineteDao molineteDao = new MolineteDao();
            string strTipVueloMol = molinete.Tip_Vuelo;
            bool flagRehab = false;
            string strCodComp = "";
            string strTipVuelo = "";
            string strAirDesig = "";
            string strOrigenAerop = String.Empty; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES

            if (Obj_BoardingBcbp != null &&
                !Obj_BoardingBcbp.Tip_Estado.Trim()
                    .Equals(Define.Cod_EstTicRehabilit)) //LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
            {
                string strDscError = "Boardingpass ya ha sido usado";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            if (Obj_BoardingBcbp == null)
            {
                Obj_BoardingBcbp = new TUA_BoardingBcbp();
                Obj_BoardingBcbp.Tip_Pasajero = ObtenerTipoPasajero((string)Ht_Boarding["passenger_description"]);
                Obj_BoardingBcbp.Fch_Vuelo = (string)Ht_Boarding["date_flight"];
                Obj_BoardingBcbp.Num_Vuelo = (string)Ht_Boarding["flight_number"];
                Obj_BoardingBcbp.Cod_Eticket = null;
                Obj_BoardingBcbp.Dsc_Destino = (string)Ht_Boarding[Define.DESTINATION];
                Obj_BoardingBcbp.Num_Checkin = (string)Ht_Boarding[Define.CHECKIN_SEQUENCE_NUMBER];
                Obj_BoardingBcbp.Num_Airline_Code = (string)Ht_Boarding[Define.AIRLINE_NUMERIC_CODE];
                Obj_BoardingBcbp.Num_Document_Form = (string)Ht_Boarding[Define.DOCUMENT_FORM_SERIAL_NUMBER];

                //CMONTES Nuevo campo para validar la unicidad para infante-padre
                Obj_BoardingBcbp.Num_Checkin = "0";
                if (Ht_Boarding["checkin_sequence_number"] != null &&
                    (string)Ht_Boarding["checkin_sequence_number"] != String.Empty)
                {
                    Obj_BoardingBcbp.Num_Checkin = (string)Ht_Boarding["checkin_sequence_number"];
                }
            }

            strOrigenAerop = (String)Ht_Boarding[Define.FROM_CITY_AIRPORT_CODE];

            loger.Log("Aeropuerto: " + (string)Ht_Boarding[Define.FROM_CITY_AIRPORT_CODE], "info");
            if (strOrigenAerop == string.Empty ||
                strOrigenAerop != (string)ConfigurationManager.AppSettings["FROM_CITY_AIRPORT_CODE"])
            {
                string strDscError = "AEROPUERTO NO VALIDO";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            loger.Log("AEROPUERTO VALIDO", "info");

            //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES - END

            //INICIO validacion de bloqueo de BCBP
            if (Obj_BoardingBcbp.Flg_Bloqueado == Define.FLG_ACTIVO)
            {
                string strDscError = "BP BLOQUEADO";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }
            //FIN validacion de bloqueo de BCBP

            TUA_VueloProgramado ObjVueloProgramado =
                await vueloProgramadoDao.getVueloProgramado(Obj_BoardingBcbp.Fch_Vuelo,
                    (string)Ht_Boarding["flight_number"]);

            if (ObjVueloProgramado == null) //LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
            {
                string strDscError = "Vuelo no Programado";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            strTipVuelo = ObjVueloProgramado.Tip_Vuelo;

            loger.Log("TIPO MOL:" + strTipVueloMol, "info");
            loger.Log("TIPO VUELO:" + strTipVuelo + " - ObjvueloProgram:" + ObjVueloProgramado.Tip_Vuelo,
                "info");

            if (strTipVueloMol != strTipVuelo)
            {
                string strDscError = "TIPO DE VUELO INCORRECTO";
                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            Obj_BoardingBcbp.Tip_Vuelo = strTipVuelo;
            strAirDesig = flagRehab ? Obj_BoardingBcbp.Cod_Compania : (String)Ht_Boarding["airline_designator"];


            //DC:  Validar estado de vuelo

            if (ObjVueloProgramado.Dsc_Estado.Trim().ToUpper() == "CANCELADO" && !flagRehab)
            {
                string strDscError = "VUELO CANCELADO";
                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }


            //VALIDAR GRUPO DE MOLINETE
            if (!string.IsNullOrEmpty(ObjVueloProgramado.Num_Puerta) &&
                !string.IsNullOrEmpty(molinete.cod_grupo))
            {
                //validar puerta de embarque
                //buscar numero de puerta en grupos de puertas de embarque
                TUA_Puerta_Grupo grupo = await molineteDao.getGrupoPorNumPuerta(ObjVueloProgramado.Num_Puerta);
                if (grupo != null)
                {
                    if (grupo.cod_grupo.Trim().ToLower() != molinete.cod_grupo.Trim().ToLower())
                    {
                        string strDscError = "PUERTA DE EMBARQUE INCORRECTA.";
                        loger.Log(strDscError, "error");
                        throw new Exception(strDscError);
                    }
                }
            }

            //DC 2022: validar ventana de tiempo de 12 horas con respecto a mejor fecha (fech_real, fch_estimada, fch_programada)

            //obtener mejor hora
            DateTime? mejorHoraProgramada = (ObjVueloProgramado.Fch_Real ?? ObjVueloProgramado.Fch_Est) ??
                                            ObjVueloProgramado.Fch_Prog;

            if (mejorHoraProgramada != null)
            {
                DateTime horaActual = DateTime.Now;
                //validar si el vuelo ha cerrado
                if (ObjVueloProgramado.Dsc_Estado == "FIN EMBARQ" &&
                    mejorHoraProgramada.Value < horaActual.AddHours(-2) && !flagRehab)
                {
                    string strDscError = "VUELO CERRADO";
                    loger.Log(strDscError, "error");
                    throw new Exception(strDscError);
                }

                if (mejorHoraProgramada.Value < horaActual.AddHours(-12) &&
                    ObjVueloProgramado.Dsc_Estado != "DEMORADO" && !flagRehab)
                {
                    //fecha de vuelo expirada
                    string strDscError = "HORA DE VUELO EXPIRADA (-12H).";
                    loger.Log(strDscError, "error");
                    throw new Exception(strDscError);
                }

                if (mejorHoraProgramada.Value > horaActual.AddHours(12))
                {
                    //fecha de vuelo expirada
                    string strDscError = "HORA DE VUELO FUERA DE RANGO (12H).";
                    loger.Log(strDscError, "error");
                    throw new Exception(strDscError);
                }
            }
            //FIN validación de ventana de tiempo


            TUA_Compania compania = await companiaDao.getCompaniaIATA(strAirDesig);
            strCodComp = compania.Cod_Compania;
            if (strCodComp.Length <= 0)
            {
                string strDscError = "Aerolínea no registrada";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            TUA_ModVentaComp ObjModaVentaComp =
                await companiaDao.getModalidadCompania(strCodComp, Define.Dsc_ModBCBP);
            if (ObjModaVentaComp == null)
            {
                string strDscError = "Aerolínea sin contrato BCBP";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            string strFechaProgramado = string.Empty;
            strFechaProgramado = ValidarVueloProgramado(ObjVueloProgramado,
                ConfigurationManager.AppSettings["UMBRAL_HORAS"],
                ConfigurationManager.AppSettings["ESTADO_DEMORADO"]);

            if ((strFechaProgramado.Trim().Length >= 1) && !flagRehab &&
                !ValidarVueloExpiradoOffline(Obj_BoardingBcbp
                    .Fch_Vuelo)) //LAP-TUUA-9163 - 04-10-2012 (RS) CMONTES
            {
                string strDscError = "Fecha de Vuelo Expirada";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            bool bRptaMolinete = true;
            Obj_BoardingBcbp.Cod_Compania = strCodComp.Trim();
            Obj_BoardingBcbp.Num_Vuelo = (String)Ht_Boarding["flight_number"];
            Obj_BoardingBcbp.Nom_Pasajero = (String)Ht_Boarding["passenger_name"];
            Obj_BoardingBcbp.Num_Asiento = (String)Ht_Boarding["seat_number"];
            Obj_BoardingBcbp.Dsc_Trama_Bcbp = strTrama;
            Obj_BoardingBcbp.Log_Fecha_Mod = Obj_BoardingBcbp.Fch_Vuelo;
            Obj_BoardingBcbp.Log_Hora_Mod = ObtenerHora();
            Obj_BoardingBcbp.Tip_Estado = Define.Cod_EstTicUsado;
            Obj_BoardingBcbp.Log_Usuario_Mod = Define.Usr_Acceso;
            Obj_BoardingBcbp.Tip_Ingreso = "A";


            VerificarPassengerStatus(Obj_BoardingBcbp, Ht_Boarding);
            if (Obj_BoardingBcbp.Tip_Trasbordo == "R")
            {
                string strDscError = "PASAJERO EN TRANSITO";
                if (strTipVuelo == "N")
                {
                    strDscError = "PASAJERO EN TRANSITO DOM";
                }

                //guardar registro de pax en transito
                await boardingdao.registrarPaxTransito(Obj_BoardingBcbp, molinete);

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            loger.Log("Fin Validaciones Internas", "info");
            loger.Log("Inicio Registro BP", "info");
            await boardingdao.registrarBoarding(Obj_BoardingBcbp, molinete);
            loger.Log("BP registrado", "info");
        }

        public String ConvertirDosDigitos(int intValor)
        {
            String strRpta = "";
            if (intValor < 10)
                strRpta = "0" + intValor.ToString();
            else
                strRpta = intValor.ToString();

            return strRpta;
        }

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

                if (strFechaHoy.Equals(Define.DiaCambioAnio) &&
                    strRpta.Substring(4, 4).Equals(Define.DiaInicioAnio))
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

        public async Task ProcesarTramaContingencia(string trama, TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            Reader reader = new Reader();
            CompaniaDao companiaDao = new CompaniaDao();
            BoardingDao boardingDao = new BoardingDao();

            TUA_BoardingBcbp boarding;
            //determinar si es boardingpass, ticket, llave de destrabe y realizar validaciones
            if (!string.IsNullOrEmpty(trama.Trim()))
            {
                // convertir trama a mayusculas
                trama = trama.ToUpper();
                loger.Log("Iniciando lectura de trama contingencia", "info");
                loger.Log("Trama Recibida:   " + trama, "info");

                //procesar trama

                //FUENTE: ACS_Interfazlector.cs
                loger.Log("Inicio de conversion de trama", "info");
                Hashtable htBoarding = reader.ParseString_ACS("    " + trama,
                    ConfigurationManager.AppSettings["CODIGOSUPERVISOR"],
                    ConfigurationManager.AppSettings["PREFDESTRABE"],
                    ConfigurationManager.AppSettings["PREFCAMBIMOLINETE"]);
                loger.Log("Fin de conversion de trama", "info");

                if (htBoarding != null)
                {
                    loger.Log("Inicio de validaciones internas", "info");
                    string strTipDocumento = "0"; // todos los tipos de documento
                    string strFormatCode = (string)htBoarding["format_code"];
                    string strPrefijo = ConfigurationManager.AppSettings["CODIGOSUPERVISOR"];
                    switch (strFormatCode)
                    {
                        case "M": //trama multiple
                            ArrayList arrLst = (ArrayList)htBoarding["flight_information"];
                            AnalizarSegmentosBCBP(arrLst);
                            foreach (Hashtable objHT in arrLst)
                            {
                                string strNom = (string)htBoarding["passenger_name"];
                                strNom = strNom.Replace('/', ' ');
                                strNom = strNom.Replace('-', ' ');
                                strNom = strNom.Replace('&', ' ');

                                htBoarding["passenger_name"] = strNom;
                                objHT.Add("passenger_name", htBoarding["passenger_name"]);
                                objHT["date_flight"] =
                                    ConvertirJulianoCalendario(Int32.Parse((string)objHT["date_flight"]));

                                reader.ParseHashtable(objHT);
                                //obtener compania por codigo iata
                                TUA_Compania compania =
                                    await companiaDao.getCompaniaIATA((string)objHT["airline_designator"]);
                                if (compania == null)
                                {
                                    throw new Exception("Aerolínea no registrada");
                                }

                                string strCodCompania = compania.Cod_Compania;

                                objHT["passenger_description"] = (string)htBoarding["passenger_description"];
                                objHT.Add(Define.DESTINATION, objHT[Define.TO_CITY_AIRPORT_CODE]);
                                objHT.Add(Define.BAR_CODE_FORMAT, Define.FORM_BCBP_LAN2D);

                                String strChekingNumber = "0";
                                if (objHT["checkin_sequence_number"] != null &&
                                    (string)objHT["checkin_sequence_number"] != String.Empty)
                                {
                                    strChekingNumber = (string)objHT["checkin_sequence_number"];
                                }

                                //obtener boardingpass
                                boarding = await boardingDao.getBoarding(strCodCompania, (string)objHT["flight_number"],
                                    (string)objHT["date_flight"], (string)objHT["seat_number"],
                                    (string)htBoarding["passenger_name"], strChekingNumber);

                                await ValidarBCBPContingencia(boarding, objHT, trama, molinete);
                            }

                            break;
                        case "T": // ticket
                            await ProcesarTramaTicket((string)htBoarding["NroTicket"], molinete);
                            break;
                        case "D":
                            //llave de destrabe
                            //verificar que la llave de destrabe exista,verificar que la cuenta se encuentre activa
                            await ProcesarTramaDestrabe(trama, molinete);

                            //registrar uso de llave y retornar ok
                            //AUDITDESTRAB_INSERT

                            break;
                        case "":
                            throw new Exception("Trama Ilegible");
                    }
                }
                else
                {
                    throw new Exception("Trama Ilegible");
                }
            }
            else
            {
                throw new Exception("Trama Ilegible");
            }
        }

        private async Task ValidarBCBPContingencia(TUA_BoardingBcbp Obj_BoardingBcbp, Hashtable Ht_Boarding,
            string strTrama, TUA_Molinete molinete)
        {
            Loger loger = new Loger();
            VueloProgramadoDao vueloProgramadoDao = new VueloProgramadoDao();
            CompaniaDao companiaDao = new CompaniaDao();
            BoardingDao boardingdao = new BoardingDao();
            MolineteDao molineteDao = new MolineteDao();
            string strTipVueloMol = molinete.Tip_Vuelo;
            bool flagRehab = false;
            string strCodComp = "";
            string strTipVuelo = "";
            string strAirDesig = "";
            string strOrigenAerop = String.Empty; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES

            if (Obj_BoardingBcbp != null &&
                !Obj_BoardingBcbp.Tip_Estado.Trim()
                    .Equals(Define.Cod_EstTicRehabilit)) //LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
            {
                string strDscError = "Boardingpass ya ha sido usado";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            if (Obj_BoardingBcbp == null)
            {
                Obj_BoardingBcbp = new TUA_BoardingBcbp();
                Obj_BoardingBcbp.Tip_Pasajero = ObtenerTipoPasajero((string)Ht_Boarding["passenger_description"]);
                Obj_BoardingBcbp.Fch_Vuelo = (string)Ht_Boarding["date_flight"];
                Obj_BoardingBcbp.Num_Vuelo = (string)Ht_Boarding["flight_number"];
                Obj_BoardingBcbp.Cod_Eticket = null;
                Obj_BoardingBcbp.Dsc_Destino = (string)Ht_Boarding[Define.DESTINATION];
                Obj_BoardingBcbp.Num_Checkin = (string)Ht_Boarding[Define.CHECKIN_SEQUENCE_NUMBER];
                Obj_BoardingBcbp.Num_Airline_Code = (string)Ht_Boarding[Define.AIRLINE_NUMERIC_CODE];
                Obj_BoardingBcbp.Num_Document_Form = (string)Ht_Boarding[Define.DOCUMENT_FORM_SERIAL_NUMBER];

                //CMONTES Nuevo campo para validar la unicidad para infante-padre
                Obj_BoardingBcbp.Num_Checkin = "0";
                if (Ht_Boarding["checkin_sequence_number"] != null &&
                    (string)Ht_Boarding["checkin_sequence_number"] != String.Empty)
                {
                    Obj_BoardingBcbp.Num_Checkin = (string)Ht_Boarding["checkin_sequence_number"];
                }
            }

            strOrigenAerop = (String)Ht_Boarding[Define.FROM_CITY_AIRPORT_CODE];

            loger.Log("Aeropuerto: " + (string)Ht_Boarding[Define.FROM_CITY_AIRPORT_CODE], "info");
            if (strOrigenAerop == string.Empty ||
                strOrigenAerop != (string)ConfigurationManager.AppSettings["FROM_CITY_AIRPORT_CODE"])
            {
                string strDscError = "AEROPUERTO NO VALIDO";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            loger.Log("AEROPUERTO VALIDO", "info");

            //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES - END

            //INICIO validacion de bloqueo de BCBP
            if (Obj_BoardingBcbp.Flg_Bloqueado == Define.FLG_ACTIVO)
            {
                string strDscError = "BP BLOQUEADO";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }
            //FIN validacion de bloqueo de BCBP

            TUA_VueloProgramado ObjVueloProgramado =
                await vueloProgramadoDao.getVueloProgramado(Obj_BoardingBcbp.Fch_Vuelo,
                    (string)Ht_Boarding["flight_number"]);

            if (ObjVueloProgramado == null)
            {
                string strDscError = "Vuelo no Programado";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            strTipVuelo = ObjVueloProgramado.Tip_Vuelo;

            loger.Log("TIPO MOL:" + strTipVueloMol, "info");
            loger.Log("TIPO VUELO:" + strTipVuelo + " - ObjvueloProgram:" + ObjVueloProgramado.Tip_Vuelo, "info");

            if (strTipVueloMol != strTipVuelo)
            {
                string strDscError = "TIPO DE VUELO INCORRECTO";
                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            Obj_BoardingBcbp.Tip_Vuelo = strTipVuelo;
            strAirDesig = flagRehab ? Obj_BoardingBcbp.Cod_Compania : (String)Ht_Boarding["airline_designator"];


            TUA_Compania compania = await companiaDao.getCompaniaIATA(strAirDesig);
            strCodComp = compania.Cod_Compania;

            if (string.IsNullOrEmpty(strCodComp))
            {
                string strDscError = "Aerolínea no registrada";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            TUA_ModVentaComp ObjModaVentaComp =
                await companiaDao.getModalidadCompania(strCodComp, Define.Dsc_ModBCBP);
            if (ObjModaVentaComp == null)
            {
                string strDscError = "Aerolínea sin contrato BCBP";

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            //no se valida fecha de vuelo expirado en contingencia

            bool bRptaMolinete = true;
            Obj_BoardingBcbp.Cod_Compania = strCodComp.Trim();
            Obj_BoardingBcbp.Num_Vuelo = (String)Ht_Boarding["flight_number"];
            Obj_BoardingBcbp.Nom_Pasajero = (String)Ht_Boarding["passenger_name"];
            Obj_BoardingBcbp.Num_Asiento = (String)Ht_Boarding["seat_number"];
            Obj_BoardingBcbp.Dsc_Trama_Bcbp = strTrama;
            Obj_BoardingBcbp.Log_Fecha_Mod = Obj_BoardingBcbp.Fch_Vuelo;
            Obj_BoardingBcbp.Log_Hora_Mod = ObtenerHora();
            Obj_BoardingBcbp.Tip_Estado = Define.Cod_EstTicUsado;
            Obj_BoardingBcbp.Log_Usuario_Mod = Define.Usr_Acceso;
            Obj_BoardingBcbp.Tip_Ingreso = "A";


            VerificarPassengerStatus(Obj_BoardingBcbp, Ht_Boarding);
            if (Obj_BoardingBcbp.Tip_Trasbordo == "R")
            {
                string strDscError = "PASAJERO EN TRANSITO";
                if (strTipVuelo == "N")
                {
                    strDscError = "PASAJERO EN TRANSITO DOM";
                }

                //guardar registro de pax en transito
                await boardingdao.registrarPaxTransito(Obj_BoardingBcbp, molinete);

                loger.Log(strDscError, "error");
                throw new Exception(strDscError);
            }

            loger.Log("Fin Validaciones Internas", "info");
            loger.Log("Inicio Registro BP", "info");
            await boardingdao.registrarBoarding(Obj_BoardingBcbp, molinete);
            loger.Log("BP registrado", "info");
        }
    }
}