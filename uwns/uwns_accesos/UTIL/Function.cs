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
        
        //Formatear a YYYYMMDD
        public static string FormatStringDate(string strFecha)
        {
            if (strFecha != null && strFecha.Trim() != "")
            {
                return strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
            }
            return null;
        }
    }


}
