using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace LAP.EXTRANET.UTIL
{
    public static class LabelConfig
    {
        public static Hashtable htLabels;
        public static String pathLabel;

        public static string NameUsuario = "admin";
        public static string CuentaUsuario = "admin";


        public static bool LoadData()
        {
            // TODO:  Add FormularioReader.LoadData implementation
            htLabels = new Hashtable();
            string linea;
            try
            {
                pathLabel = AppDomain.CurrentDomain.BaseDirectory + "resources\\labels.properties";
                pathLabel = pathLabel.TrimEnd();
                StreamReader sr = new StreamReader(pathLabel, System.Text.Encoding.UTF7);//Si no pongo este ultimo parametro no me muestra las tildes.
                while ((linea = sr.ReadLine()) != null)
                {
                    char[] sep = { '=' };
                    string[] v = linea.Split(sep, 2);
                    if (v.Length > 1)
                    {
                        string campo = v[0].Trim();
                        if (campo.Length == 0 || (campo.Length > 0 && campo.ToCharArray()[0].Equals('#')))
                        {
                            continue;
                        }
                        string valor = v[1].TrimEnd();
                        htLabels.Add(campo, valor);
                    }
                }
                sr.Close();
                return true;
            }
            catch (Exception e)
            {
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_004])["MESSAGE"];
                ErrorHandler.Desc_Mensaje = strMessage;
                ErrorHandler.Trace("ADMINISTRACION WEB", strMessage);
                return false;
            }
        }
    }
}
