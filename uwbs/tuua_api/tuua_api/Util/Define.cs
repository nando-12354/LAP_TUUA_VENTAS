using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Util
{
    public class Define
    {
        public static readonly string Dsc_SPConfig = AppDomain.CurrentDomain.BaseDirectory;
        public const string FolderResources = "resources/";
        public const string FileName_FormatoBoarding_Single = "FormatoBoardingSingle.xml";
        public const string FileName_FormatoBoarding_Multiple = "FormatoBoardingMultiple.xml";
                     
                     
        public const string Tipo = "Tipo";
        public const string Boarding_Multiple = "M";
        public const string Boarding_Single = "S";
        public const string Ticket = "T";
        public const string Destrabe = "D";
        public const string CambioMolinete = "C";
        public const string Supervisor = "P";
        public const string PinPad = "A";
                     
        //Ticket     
        public const string NroTicket = "NroTicket";
        public const int TamanoTicket = 16;
        public static int LongTicket = 16;
        public static int LongSupervisor = 8;
        public static int LongDestrabe = 9;
        public static int LongCambioMolinete = 9;
        public static int LongBcbpLan1D = 17;
        public const string FORM_BCBP_LAN1D = "1D";
        public const string FORM_BCBP_LAN2D = "2D";
        public const string COD_EMPTY_SEAT_NUMBER = "INF";
        public const int SIZE_SEAT_NUMBER = 4;
        public const int SIZE_FLIGHT_NUMBER = 4;
        public const int SIZE_CHECKIN_NUMBER = 5;
        public const string COD_AERO_LA = "LA";
        public const string COD_AERO_LP = "LP";

        //Boarding
        public const string Compania = "airline_designator";
        public const string FechaVuelo = "date_flight";
        public const string NroVuelo = "flight_number";
        public const string Asiento = "seat_number";
        public const string Persona = "passenger_name";
       
        public const string AIRLINE_DESIGNATOR = "airline_designator";
        public const string DATE_FLIGHT = "date_flight";
        public const string FLIGHT_NUMBER = "flight_number";
        public const string SEAT_NUMBER = "seat_number";
        public const string PASSENGER_NAME = "passenger_name";
        public const string Format_Code = "format_code";
        public const string DESTINATION = "destination";
        public const string TO_CITY_AIRPORT_CODE = "to_city_airport_code";
        public const string CHECKIN_SEQUENCE_NUMBER = "checkin_sequence_number";
        public const string ELECTRONIC_TICKET_INDICATOR = "electronic_ticket_indicator";
        public const string PASSENGER_STATUS = "passenger_status";
        public const string AIRLINE_NUMERIC_CODE = "airline_numeric_code";
        public const string DOCUMENT_FORM_SERIAL_NUMBER = "document_form_serial_number";
        public const string BAR_CODE_FORMAT = "bar_code_format";
        public const string FROM_CITY_AIRPORT_CODE = "from_city_airport_code";

        public static String DiaCambioAnio = "1231";
        public static String DiaInicioAnio = "0101";

        //Longituds tramas

        public static int Long_MinBCBP = 64;
        public static int Long_MinTicket = 10;

        //Tipos de vuelo
        public static string Cod_TipVuelInt = "I";
        public static string Cod_TipoVuelNac = "N";
        public static string Cod_TipoVuelTodo = "0";

        //Estado Ticket
        public static string Cod_EstTicBorrado = "B";
        public static string Cod_EstTicEmitido = "E";
        public static string Cod_EstTicRehabilit = "R";
        public static string Cod_EstTicUsado = "U";
        public static string Cod_EstTicUsadoVenta = "V";
        public static string Cod_EstTicAnulado = "X";
        public static string Cod_EstTicPremitido = "P";
        public static string Cod_EstTicReusado = "S";

        //Des Estados

        public static string Dsc_EstTicUsado = "USADO";
        public static string Dsc_EstTicReusado = "REUSADO";

        //Codigo tipo Psajero
        public static string Cod_TipPasajAdulto = "A";
        public static string Cod_TipPasajInfante = "I";

        public const string TYPE_PASS_CHILD = "3";
        public const string TYPE_PASS_INFANT = "4";


        public static string FLG_ACTIVO = "1";
        public static string FLG_INACTIVO = "0";

        public static string Dsc_ModBCBP = "BCBP";

        public const string Usr_Acceso = "USR_ACS";
        public const string Usr_Acceso_Cntg = "USR_CON";

        public const string NORMAL = "N";
        public const string TRANSFERENCIA = "T";
        public const string TRANSITO = "R";

        public static string ID_OPER_DESTRAB_OK = "5";
        public static string ID_OPER_DESTRAB_ERROR = "7";
        public static string ID_OPER_CAMBMOL_OK = "6";
        public static string ID_OPER_CAMBMOL_ERROR = "8";


    }
}