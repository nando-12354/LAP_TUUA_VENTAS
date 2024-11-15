using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LAP.TUUA.ACCESOS
{
      static class  ACS_Property
      {
            private static bool bConRemota;

            public static bool BConRemota
            {
                  get { return ACS_Property.bConRemota; }
                  set { ACS_Property.bConRemota = value; }
            }
            private static bool bConLocal;

            public static bool BConLocal
            {
                  get { return ACS_Property.bConLocal; }
                  set { ACS_Property.bConLocal = value;}
            }
            public static IDataReader IDRMolinete;
            public static Hashtable shtMolinete;

            public static Database shelper;
            public static Database shelperLocal;
            public static bool estadoLector;
            public static bool estadoPinPad;
            public static bool estadoMolinete;
            public static bool modoContingencia;
            public static bool estadoFlujoPinPad;

            //esilva - 31-07-2010 - Flag indicador de escritura de log de error
            public static bool bWriteErrorLog;

            //esilva - 04-05-2011 - Flag indicador de Modo Testeo de Accesos Contingencia
            public static bool bModeTest;
            public static int iTimeOutTest;
            
            //eochoa - 17-05-2011 - Prueba del nuevo modo de lectura
            public static bool bModeLecturaNueva;

            public static void scargarMolinete()
            {
                  shtMolinete = new Hashtable();
                  shtMolinete.Add("Cod_Molinete", "");
                  shtMolinete.Add("Dsc_Ip", "");
                  shtMolinete.Add("Dsc_Molinete", "");
                  shtMolinete.Add("Tip_Documento", "");
                  shtMolinete.Add("Tip_Acceso", "");
                  shtMolinete.Add("Tip_Estado", "");
                  shtMolinete.Add("Est_Master", "");
                  shtMolinete.Add("Dsc_DBName", "");
                  shtMolinete.Add("Dsc_DBUser", "");
                  shtMolinete.Add("Dsc_DBPassword", "");
                  shtMolinete.Add("Tip_Vuelo", "");
                  shtMolinete.Add("Tip_Molinete", "");
                  shtMolinete.Add("Est_PinPad", "FALSE");

            }
      }
}
