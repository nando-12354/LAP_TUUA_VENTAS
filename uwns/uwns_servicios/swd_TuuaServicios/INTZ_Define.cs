using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.SERVICIOS
{
     public sealed class  INTZ_Define
      {
           //constantes tasa Cambio
           public static string Cod_Compra       = "C";
           public static string Cod_Venta        = "V";
           public static string Cod_TGuardaLog   = "1";
           public static string Cod_FGuardaLog   = "0";

           //rutas de configuracion
           public static string Dsc_Config       = AppDomain.CurrentDomain.BaseDirectory + "resources/config.xml";

           public static string Dsc_ErrorConfig  = AppDomain.CurrentDomain.BaseDirectory + "/resources";
           public static string Dsc_SPConfig     = AppDomain.CurrentDomain.BaseDirectory;

           //constantes aerolinea
           public static string Cod_TipAerolinea = "1";
           public static string Cod_TipEstado    = "1";

           //constantes booleanas
           public static string Cod_Activo       = "1";
           public static string Cod_Inactivo     = "0";

           //Estados
           public static string Cod_Anulado      = "X";
           public static string Dsc_Anulado      = "ANULADO";
           public static string Cod_Emitido      = "E";
           public static string Dsc_Emitido      = "EMITIDO";
           public static string Cod_Rehabilitado = "R";
           public static string Dsc_Rehabilitado = "REHABILITADO";


           //Usuario ,Equipo 
           public static string Cod_UServicio    = "USR_SVC";
           public static string Cod_EServicio    = "EQP_SVC";

           //Modulo
           public static string Cod_MServicio          = "S01";
           public static string Cod_ICompania          = "E9002";
           public static string Cod_ITasaCambio        = "E9003";
           public static string Cod_IVuleloProgramado  = "E9004";
           public static string Cod_IVuleloTemporada   = "E9005";
           public static string Cod_SClaveUsuario      = "E9006";
           public static string Cod_SPermisoUsuario    = "E9007";
           public static string Cod_SPrecioTicket      = "E9008";
           public static string Cod_STasaCambio        = "E9009";
           public static string Cod_SVencimientoBCBP   = "E9010";
           public static string Cod_SVencimientoTicket = "E9011";


           //Tip Anulacion
           public static string Tip_Anulacion    = "3";

      }
}
