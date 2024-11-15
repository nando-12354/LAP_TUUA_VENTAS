using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace LAP.TUUA.UTIL
{
    public static class BPConfig
    {
        public static Hashtable htLabels;
        public static String pathLabel;

        public static string NameUsuario = "admin";
        public static string CuentaUsuario = "admin";


        public static int LoadData(string strPath)
        {
            // TODO:  Add FormularioReader.LoadData implementation
            htLabels = new Hashtable();
            string linea;
            try
            {
                pathLabel = strPath + "testupload.properties";
                pathLabel = pathLabel.TrimEnd();
                StreamReader sr = new StreamReader(pathLabel, System.Text.Encoding.UTF7);//Si no pongo este ultimo parametro no me muestra las tildes.
                int i = 0;
                while ((linea = sr.ReadLine()) != null)
                {
                    //char[] sep = { '=' };
                    //string[] v = linea.Split(sep, 2);
                    //if (v.Length > 1)
                    //{
                        /*string campo = v[0].Trim();
                        if (campo.Length == 0 || (campo.Length > 0 && campo.ToCharArray()[0].Equals('#')))
                        {
                            continue;
                        }*/
                        string valor = linea.TrimEnd();
                        string campo = i + "";
                        htLabels.Add(campo, valor);
                        i++;
                    //}
                }
                sr.Close();
                return 1;
            }
            catch (Exception e){
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_004])["MESSAGE"];
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace("ADMINISTRACION WEB", strMessage);
                return 0;
            }
        } 
    }
}
