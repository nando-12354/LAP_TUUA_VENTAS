using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.UTIL
{
    public static class Fecha
    {
        //Salida: Fecha Actual (dd/mm/yyyy)
        public static string getFechaActual(){ 
            return DateTime.Now.Day.ToString("00") + "/" +
                    DateTime.Now.Month.ToString("00") + "/" +
                    DateTime.Now.Year.ToString("0000");
        }
        //Salida: Fecha Actual (dd/mm/yyyy) mas una cantidad de dias 
        public static string getFechaCustom(int dias)
        {
            //DateTime dtFecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime dtFecha = System.DateTime.Now;
            dtFecha = dtFecha.AddDays(dias);
            return dtFecha.Day.ToString("00") + "/" +
                    dtFecha.Month.ToString("00") + "/" +
                    dtFecha.Year.ToString("0000");
        }

        //Entrada: Fecha (dd/mm/yyyy)
        //Salida : Fecha (yyyymmdd)
        public static string convertToFechaSQL( string fecha)
        {
            return fecha.Substring(6,4) + 
                   fecha.Substring(3,2) +
                   fecha.Substring(0,2);
        }

        //Entrada: Fecha (hh:mm:ss)
        //Salida : Fecha (hhmmss)
        public static string convertToHoraSQL(string hora)
        {
            if (hora != null && hora.Length == 8 && hora != "__:__:__")
            {

                return hora.Substring(0, 2) +
                       hora.Substring(3, 2) +
                       hora.Substring(6, 2);
            }
            else
            {
                return "";
            }            
        }

        //Entrada: Fecha (hh:mm)
        //Salida : Fecha (hhmmss)
        public static string convertToHoraSQL2(string hora)
        {
            if (hora != null && hora.Length == 5)
            {

                return hora.Substring(0, 2) +
                       hora.Substring(3, 2) +
                       "00";
            }
            else
            {
                return "";
            }
        }

        //katy

        //Entrada: Fecha (yyyymmdd)
        //Salida:  Fecha (dd/mm/yyyy) 

        public static string convertSQLToFecha3(string fecha)
        {
            if (fecha != null && fecha.Length == 8)
            {
                return fecha.Substring(6, 2) + "/" +
                       fecha.Substring(4, 2) + "/" +
                       fecha.Substring(0, 4) + " ";
                      

            }
            else
            {
                return "";
            }


        } //katy 13-01-2011


        //eochoa
        //Entrada: Fecha (yyyymmdd) or (yyyymmddhhmmss)
        //Salida:  Fecha (dd/mm/yyyy) or (dd/mm/yyyy hh:mm:ss)
        public static string formatoFechaExcel(string fecha)
        {
            if (fecha != null)
            {
                if (fecha.Length == 8)
                {
                    return fecha.Substring(6, 2) + "/" +
                           fecha.Substring(4, 2) + "/" +
                           fecha.Substring(0, 4) + " ";
                }
                else
                {
                    if (fecha.Length == 14)
                    {
                        return fecha.Substring(6, 2) + "/" +
                               fecha.Substring(4, 2) + "/" +
                               fecha.Substring(0, 4) + " " +
                               fecha.Substring(8, 2) + ":" +
                               fecha.Substring(10, 2) + ":" +
                               fecha.Substring(12, 2);
                    }
                    else
                        return null;

                }
            }
            else
                return "";
        }
       

        //Entrada: Fecha (dd/mm/yyyy)
        //Salida : Fecha (yyyymmdd)
        public static string convertToFechaSQL2(string fecha)
        {
            if (fecha != null && fecha.Length == 10)
            {

                return fecha.Substring(6, 4) +
                       fecha.Substring(3, 2) +
                       fecha.Substring(0, 2);
            }
            else {
                return "";
            }
        }

        //Entrada: Fecha (yyyymmdd) Hora (hhmmss)
        //Salida : Fecha (dd/mm/yyyy hh:mm:ss)
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

        //Entrada: Fecha (yyyymmdd) Hora (hhmmss)
        //Salida : Fecha (dd/mm/yyyy hh:mm:ss)
        public static string convertSQLToFecha2(string fecha)
        {
            if (fecha != null && fecha.Length > 0 && fecha.Length == 14)
            {
                return fecha.Substring(6, 2) + "/" +
                        fecha.Substring(4, 2) + "/" +
                       fecha.Substring(0, 4) + " " +
                       fecha.Substring(8, 2) + ":" +
                       fecha.Substring(10, 2) + ":" +
                       fecha.Substring(12, 2);

            }
            return "";

        }

        //Entrada: Hora (hhmmss)
        //Salida : Fecha (hh:mm:ss)
        public static string convertSQLToHora(string hora)
        {
            if (hora != null && hora.Length > 0)
            {
                return hora.Substring(0, 2) + ":" + hora.Substring(2, 2) + ":" + hora.Substring(4, 2);
            }
            else
            {
                return "";
            }
        }

        //Entrada: Fecha (dd/mm/yyyy) y Hora (hh:mm) en formato de cadenas
        //Salida: 1 OK  | 0 ERROR
        public static int getFechaCustom2(string fecha, string hora, out DateTime dateValue) {
            string dateString = fecha + " " + hora;
            int iRet = 0;
            if (DateTime.TryParse(dateString, out dateValue))
            {
                iRet = 1; 
            }
            return iRet;
        }

        public static int getFechaElapsed(string fecha_ini, string fecha_fin) {
            DateTime dt_ini = Function.ConvertToDateTime(fecha_ini);
            DateTime dt_fin = Function.ConvertToDateTime(fecha_fin);

            TimeSpan ts = dt_fin - dt_ini;

            int differenceInDays = ts.Days;

            return differenceInDays;
        }
    }
}
