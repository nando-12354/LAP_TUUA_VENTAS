using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LAP.TUUA.LOGS
{
    static class ACS_Property
    {
        private static bool bConexion;

        public static bool BConexion
        {
            get { return ACS_Property.bConexion; }
            set { ACS_Property.bConexion = value; }
        }
        //private static bool bConLocal;

        //public static bool BConLocal
        //{
        //      get { return ACS_Property.bConLocal; }
        //      set { ACS_Property.bConLocal = value;}
        //}
        //public static IDataReader IDRMolinete;
        //public static Hashtable shtMolinete;

        public static Database shelper;
        public static Database shelperLocal;
        //public static bool estadoLector;
        //public static bool estadoPinPad;
        //public static bool estadoMolinete;
        //public static bool modoContingencia;
        //public static bool estadoFlujoPinPad;

        public static bool bWriteErrorLog;

        //public static void scargarMolinete()
        //{
        //      shtMolinete = new Hashtable();
        //      shtMolinete.Add("Cod_Molinete", "");
        //      shtMolinete.Add("Dsc_Ip", "");
        //      shtMolinete.Add("Dsc_Molinete", "");
        //      shtMolinete.Add("Tip_Documento", "");
        //      shtMolinete.Add("Tip_Acceso", "");
        //      shtMolinete.Add("Tip_Estado", "");
        //      shtMolinete.Add("Est_Master", "");
        //      shtMolinete.Add("Dsc_DBName", "");
        //      shtMolinete.Add("Dsc_DBUser", "");
        //      shtMolinete.Add("Dsc_DBPassword", "");
        //      shtMolinete.Add("Tip_Vuelo", "");
        //      shtMolinete.Add("Tip_Molinete", "");
        //      shtMolinete.Add("Est_PinPad", "FALSE");

        //}
    }
}