using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.UTIL
{
    public static class Fecha
    {
        public static string getFechaActual(){
            return DateTime.Now.Day.ToString("00") + "/" +
                    DateTime.Now.Month.ToString("00") + "/" +
                    DateTime.Now.Year.ToString("0000");
        }
        public static string getFechaCustom(int dias)
        {
            //DateTime dtFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dtFecha = System.DateTime.Now;
            dtFecha = dtFecha.AddDays(dias);
            return dtFecha.Day.ToString("00") + "/" +
                    dtFecha.Month.ToString("00") + "/" +
                    dtFecha.Year.ToString("0000");
        }
        public static string convertToFechaSQL( string fecha)
        {
            return fecha.Substring(6,4) + 
                   fecha.Substring(3,2) +
                   fecha.Substring(0,2);
        }
        public static string convertSQLToFecha(string fecha, string hora)
        {
            if (fecha != null && fecha.Length > 0)
            {
                if (hora != null && hora.Length > 0)
                {
                    return fecha.Substring(6, 2) + "/" +
                        fecha.Substring(4, 2) + "/" +
                       fecha.Substring(0, 4) + " "+
                       hora.Substring(0, 2) + ":" +
                       hora.Substring(2, 2) + ":" +
                       hora.Substring(4, 2);
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
