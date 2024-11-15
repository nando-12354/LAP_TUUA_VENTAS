using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;

namespace LAP.TUUA.UTIL
{
    public static class Globales
    {

        //Modalidad de Venta
        public static ArrayList ListaControles;
        public static ArrayList ListaRegistradosMV;
        public static Hashtable ListaRegistradosTT;
        public static ArrayList ListaEliminadosMV;
        public static Hashtable ListaEliminadosTT;
        public static ArrayList ListaSeleccionados;
        public static Hashtable ListaActualesMV;
        public static Hashtable ListaActualesTT;
        public static string CodModalidadVenta;
        public static string DscModalidadVenta;
        public static string DscTipoModalidadVenta;
        public static Int32 IndxTipoModalidad;
        public static Int32 IndxEstadoModalidad;
        public static string MjeError;
        public static string TTSeleccionado;
        public static bool FlagModal = false;
        public static bool FlagPosPage = false;


        //Reprsentante

        public static ArrayList RListaControles;
        public static Hashtable RSeleccionado;
        public static Hashtable RRegistrados;

        //Compañía
        public static ArrayList CListaControles;
        public static Hashtable MVRegistrados;
        public static Hashtable MVEliminados;
        public static Hashtable ValoresActuales;
        public static string txtCodCompañia;
        public static string txtNombre;
        public static string txtIATA;
        public static string txtOACI;
        public static string txtRuc;
        public static string txtCodigoEspecial;
        public static string txtAerolinea;
        public static string txtSAP;
        public static Int32 IndxTipoCompañia;
        public static Int32 IndxEstadoCompania;
        public static string MjeErrorRepres;

    }
}
