using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.CONVERSOR
{
    public class Define
    {
        public static readonly string Dsc_SPConfig = AppDomain.CurrentDomain.BaseDirectory;
        public const String FolderResources = "resources/";
        public const String FileName_FormatoBoarding_Single = "FormatoBoardingSingle.xml";
        public const String FileName_FormatoBoarding_Multiple = "FormatoBoardingMultiple.xml";
        

        public const String Tipo = "Tipo";
        public const String Boarding_Multiple = "M";
        public const String Boarding_Single = "S";
        public const String Ticket = "T";
        public const String Destrabe = "D";
        public const String Supervisor = "P";

        //Ticket
        public const String NroTicket = "NroTicket";
        public const int TamanoTicket = 16;
        public static int LongTicket = 16;
        public static int LongSupervisor = 8;
        public static int LongDestrabe = 9;
        public static int LongBcbpLan1D = 17;
        public const string FORM_BCBP_LAN1D = "1D";
        public const string COD_EMPTY_SEAT_NUMBER = "00";
        public const int SIZE_SEAT_NUMBER = 4;
        public const int SIZE_FLIGHT_NUMBER = 4;
        public const int SIZE_CHECKIN_NUMBER = 5;
        public const string COD_AERO_LA = "LA";
        public const string COD_AERO_LP = "LP";

        //Boarding
        public const String Compania = "airline_designator";
        public const String FechaVuelo = "date_flight";
        public const String NroVuelo = "flight_number";
        public const String Asiento = "seat_number";
        public const String Persona = "passenger_name";
        public const String Format_Code = "format_code";
        public const String CHECKIN_SEQUENCE_NUMBER = "checkin_sequence_number";
        public const String FROM_CITY_AIRPORT_CODE = "from_city_airport_code";
        public const String DEFAULT_CHECKIN_SEQUENCE_NUMBER = "00000";

    }
}
