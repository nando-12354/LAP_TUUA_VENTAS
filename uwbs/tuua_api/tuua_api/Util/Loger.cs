using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace tuua_api.Util
{
    public class Loger
    {

        public Loger()
        {
            this.RutaCarpeta = ConfigurationManager.AppSettings["ruta_log"];

        }

        private String _rutaCarpeta;

        public String RutaCarpeta
        {
            get { return _rutaCarpeta; }
            set { _rutaCarpeta = value; }
        }

        public void Log(String mensaje, String tipo)
        {
            String archivo = _rutaCarpeta + String.Format("{0}{1}.txt", "TUUA_API_LOG", DateTime.Now.ToString("yyyy-MM-dd")); ;

            try
            {

                if (!File.Exists(archivo))
                {
                    // crear
                    StreamWriter sw = File.CreateText(archivo);
                    sw.Dispose();
                }
                using (StreamWriter sw = File.AppendText(archivo))
                {

                    sw.WriteLine(String.Format("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), tipo);
                    sw.WriteLine(mensaje);
                    sw.WriteLine("--------------------------------------------------------------------------");
                    sw.Dispose();
                }

            }
            catch (Exception)
            {


                //throw;
            }


        }
    }
}