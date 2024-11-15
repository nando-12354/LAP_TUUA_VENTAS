using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.LOGS
{
    /// <summary>
    /// ACCESOS : Define
    /// </summary>
    public class ACS_Define
    {
        //TIPOS DE ERROR EN LOGS
        public static string ERROR_PINPAD = (string)Property.htProperty["ERROR_PINPAD"];
        public static string ERROR_TIME_OUT = (string)Property.htProperty["ERROR_TIME_OUT"];
        public static string ERROR_TIME_OUT_ = (string)Property.htProperty["ERROR_TIME_OUT_"];
        public static string ERROR_SERVICIO_ACCESOS = (string)Property.htProperty["ERROR_SERVICIO_ACCESOS"];
        public static string ERROR_SERVICIO_ACCESOS_ = (string)Property.htProperty["ERROR_SERVICIO_ACCESOS_"];
        public static string ERROR_SQL_EXCEPTION = (string)Property.htProperty["ERROR_SQL_EXCEPTION"];
        public static string ERROR_LECTURA = (string)Property.htProperty["ERROR_LECTURA"];
        public static string ERROR_TRAMA_VACIA = (string)Property.htProperty["ERROR_TRAMA_VACIA"];
        public static string DESTRABE_MOLINETE = (string)Property.htProperty["DESTRABE_MOLINETE"];
        public static string ERROR_NO_IDENTIFICADO = (string)Property.htProperty["ERROR_NO_IDENTIFICADO"];
        //END 

        public static String Cod_Response = "response_code";
        public static String Dsc_Message = "message";
        public static String Cod_Authent = "cod_authent";

        public static String Dsc_SPConfig = AppDomain.CurrentDomain.BaseDirectory;
        public static String Dsc_SCOMLectorConfig = Dsc_SPConfig + "resources/ptolector.xml";
        public static String Dsc_SCOMPinpadConfig = Dsc_SPConfig + "resources/ppinpad.xml";

        public static String Cod_Aplicacion = "ecr_aplicacion";
        public static String Cod_Transaccion = "ecr_transaccion";
        public static String Dsc_Data_Adicional = "ecr_data_adicional";
        public static String Dsc_Data_Track2 = "ecr_track2";


        public static String Cod_NumeroTicket = "numticket";


        public static String Cod_NumeroVuelo = "numvuelo";
        public static String Cod_Empresa = "codigoempresa";

        public static Char Cod_FinTrama = '\x3';


        public static String Cod_TramaMultiple = "M";
        public static String Cod_TramaSimple = "S";

        //Mensajes de Error
        public static string Dsc_ErrComPINPAD = "Error de Comunicación con el Pin Pad ";
        public static string Dsc_ErrToutPINPAD = "Error de Tiempo de espera con el Pin Pad ";


        //Codigo de Alertas PINPAD
        public static int Cod_Com_OK_PINPAD = 0;
        public static int Cod_Com_Err_PINPAD = 1;


        //#PINPAD#
        public static int Long_DatAdicional = 99;
        public static int Long_Track2 = 86;

        //Mensajes PINPad
        public static String Dsc_KEYPRESS = "KEYPRESS";
        public static String Dsc_CONSULTATICKET = "CONSULTATICKET";
        public static String Dsc_REGISTROTICKET = "REGISTROTICKET";
        public static String Dsc_CONSULTABOARDING = "CONSULTABOARDING";
        public static String Dsc_REGISTROBOARDING = "REGISTROBOARDING";
        public static String Dsc_CANCELADO = "CANCELADO";

        public static String Msg_CANCELADO = "CANCELADO";


        //Transacciones PINPAD
        public static string Cod_IniComPnd = "01";  //Inicia comunicacion con el pinpad
        public static string Cod_IniModAccesoPnd = "02";  //Inicia Menu Acceso en el pinpad
        public static string Cod_MsgPnd = "03";  //msg pinpad
        public static string Cod_MsgDetPnd = "04";  //msg detalle pinpad
        public static string Cod_ConBoarding = "05";  //msg detalle BCBP
        public static string Cod_TiemIniFormPnd = "06";  //msg tiempo frm pinpad inicial 
        public static string Cod_TiemConFormPnd = "07";  //msg tiempo frm pinpad final 
        public static string Cod_ConBoardingErr = "08";  //msg cons bcbp error
        public static string Cod_RegBCBPErr = "09";  //msg reg bcbp error
        public static string Cod_ConTickErr = "10";  //msg cons ticket error
        public static string Cod_RegTickErr = "11";  //msg reg ticket error
        public static string Cod_RegBCBPOk = "12";  //msg reg bcbp ok
        public static string Cod_RegTickOk = "13";  //msg reg ticket ok


        //Mensajes transacciones PINPAD
        public static string Msg_RegBCBPOk = ";SATISFACTORIO;;PASE ADELANTE";  //msg reg bcbp ok
        public static string Msg_RegTickOk = ";SATISFACTORIO;;PASE ADELANTE";  //msg reg ticket ok

        public static string Dsc_HeadTrans = "HECRQ04000000000010001NODO           FORMULARIO     18,ecr_aplicacion=LAP";
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

        //Codigo tipo errores boarding
        public static string Cod_ErrBCBPReg = "1";
        public static string Cod_ErrBCBPAero = "2";
        public static string Cod_ErrBCBPFecha = "3";
        public static string Cod_ErrBCBPMod = "4";
        public static string Cod_ErrBCBPNoReg = "5";
        public static string Cod_ErrBCBPIleg = "6";
        public static string Cod_ErrBCBPTip = "7";
        public static string Cod_ErrBCBVuelo = "8";
        public static string Cod_ErrBCBPNoValido = "9";

        //Codigo tipo de ingreso
        public static string Cod_TipIngAuto = "A";
        public static string Cod_TipIngMan = "M";

        //Nomb Modalida Venta BCBP

        public static string Dsc_ModBCBP = "BCBP";

        //Codigo tipo errores ticket
        public static string Cod_ErrTicketReg = "1";
        public static string Cod_ErrTicketNoReg = "4";
        public static string Cod_ErrTicketTip = "5";

        ////Ticket
        public const String NroTicket = "NroTicket";

        //Boarding
        public const String Compania = "airline_designator";
        public const String FechaVuelo = "date_flight";
        public const String NroVuelo = "flight_number";
        public const String Asiento = "seat_number";
        public const String Persona = "passenger_name";
        public const String Format_Code = "format_code";
        public const String DESTINATION = "destination";
        public const String TO_CITY_AIRPORT_CODE = "to_city_airport_code";
        public const String CHECKIN_SEQUENCE_NUMBER = "checkin_sequence_number";
        public const String ELECTRONIC_TICKET_INDICATOR = "electronic_ticket_indicator";

        //Usuario,Modulo,SubModulo accesos
        public const string Usr_Acceso = "USR_ACS";
        public const string Usr_Acceso_Cntg = "USR_CON";
        public static string Cod_Modulo = "M01";
        public static string Cod_SModRegTicket = "E9001";
        public static string Cod_SModRegBoarding = "E9000";
        public const string Rol_Acceso = "";

        //Colores
        public static int Cod_Verde = 0; //verde
        public static int Cod_Amarillo = 1; //amarillo
        public static int Cod_Rojo = 2; //rojo
        public static int Cod_Blanco = 3; //blanco

        //Tipo de Lectura
        public static int Tip_Compuesta = 0; //BCBP compuesta
        public static int Tip_Simple = 1; //BCBP simple
        public static int Tip_Ticket = 2; //ticket


        //segmento opcional de boarding multiple
        public const string NO_OPTIONAL_SEGMENT = "00";
        public const string TYPE_PASS_CHILD = "3";
        public const string TYPE_PASS_INFANT = "4";
        public const string PREFSUPER = "SUP";
        public static int LONG_COD_SUPER = 9;

        //BCBP RELACIONADO
        public static string FLG_BCBP_BASE = "0";
        public static string FLG_BCBP_REL = "1";
        public static string TIP_USR_VIGENTE = "V";

        //LAN1D
        public static string COD_AERO_LP = "LP";
        public static string COD_AERO_LA = "LA";
        public static string COD_EMPTY_SEAT_NUMBER = "00";
        public static int SIZE_SEAT_NUMBER = 4;

        //CAMPOS DE RETORNO WEB SERVICE
        public static string FLG_SI_PAGO_TUUA = "1";
        public static string FLG_NO_PAGO_TUUA = "0";
        public static string FLG_ES_TRANSITO = "1";
        public static string FLG_NO_TRANSITO = "0";

        public static string ID_VAL_WS = "W3";
        public static string ID_VAL_TRANSITO = "W2";
        public static string ID_VAL_PAGO_TUUA = "W1";
        public static string ID_NOM_SERVICIO = "W4";
        public static string ID_NOM_METODO = "W5";
        public static string ID_NOM_PROTOCOLO = "W6";
        public static string ID_NOM_RUTA = "W7";

        public static string KEY_WS_RESPONSE = "KEY_WS_RESPONSE";
        public static string KEY_WS_ERROR_MSG = "KEY_WS_ERROR_MSG";
        public static string KEY_WS_TRAMA_RESPONSE = "KEY_WS_TRAMA_RESPONSE";
        public static string KEY_PASSANGER_NAME_WS = "KEY_PASSANGER_NAME_WS";
        public static string FLG_ACTIVO = "1";
        public static string FLG_INACTIVO = "0";
    }
}
