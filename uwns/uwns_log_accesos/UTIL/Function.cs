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

        //Entrada: Fecha (dd/mm/yyyy)
        //Salida : Fecha (yyyymmdd)
        public static string convertToFechaSQL(string fecha)
        {
            return fecha.Substring(6, 4) +
                   fecha.Substring(3, 2) +
                   fecha.Substring(0, 2);
        }

        //Entrada: Fecha (hh:mm:ss:nn)
        //Salida : Fecha (hhmmssnn)
        public static string convertToHoraSQL(string hora)
        {
            if (hora != null)
            {
                if (hora.Length == 12)
                {
                    return hora.Substring(0, 2) +
                           hora.Substring(3, 2) +
                           hora.Substring(6, 2) +
                           hora.Substring(9, 3);
                }
                else
                {
                    return "0" + hora.Substring(0, 1) +
                           hora.Substring(2, 2) +
                           hora.Substring(5, 2) +
                           hora.Substring(8, 3);
                }
            }
            else
            {
                return "";
            }
        }

        //Entrada: Fecha (yyyymmdd) Hora (hhmmssnn)
        //Salida : Fecha (dd/mm/yyyy hh:mm:ss:nn)
        public static string convertSQLToFecha(string fecha, string hora)
        {
            if (fecha != null && fecha.Length > 0)
            {
                if (hora != null && hora.Length > 0)
                {
                    return fecha.Substring(6, 2) + "/" +
                        fecha.Substring(4, 2) + "/" +
                       fecha.Substring(0, 4) + " " +
                       hora.Substring(0, 2) + ":" +
                       hora.Substring(2, 2) + ":" +
                       hora.Substring(4, 2) + ":" +
                       hora.Substring(6, 3);
                }
                else
                {
                    return fecha.Substring(6, 2) + "/" +
                        fecha.Substring(4, 2) + "/" +
                       fecha.Substring(0, 4);
                }
            }
            return "";

        }
    }


}
