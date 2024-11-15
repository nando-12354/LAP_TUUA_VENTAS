using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.UTIL
{
    public class Function
    {
        public static decimal FormatDecimal(decimal decNumero,int intCantDecimal){
           return decimal.Round(decNumero, intCantDecimal);
        }

        public static String ConvertirDosDigitos(int intValor)
        {
              String strRpta = "";
              if (intValor < 10)
                    strRpta = "0" + intValor.ToString();
              else
                    strRpta = intValor.ToString();

              return strRpta;
        }
        
        //parametro DD/MM/YYYY
        //Formatear a YYYYMMDD
        public static string FormatStringDate(string strFecha)
        {
            if (strFecha != null && strFecha.Trim() != "")
            {
                return strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
            }
            return null;
        }

        //parametro YYYYMMDD
        public static DateTime ConvertToDateTime(string strFecha)
        {
            DateTime dtFecha = new DateTime(Int32.Parse(strFecha.Substring(0, 4)), Int32.Parse(strFecha.Substring(4, 2)), Int32.Parse(strFecha.Substring(6, 2)));
            return dtFecha;
        }
    }


}
