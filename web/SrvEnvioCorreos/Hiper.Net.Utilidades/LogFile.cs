using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hiper.Net.Utilidades.Archivos
{
    /// <summary>
    /// Summary description for LogFile.
    /// </summary>
    public class LogFile : TextFile
    {
        new private void setName(string name)
        {
            if (name.EndsWith(".log")) // Han proporcionado el nombre del archivo con extensión
            {
                this.name = name.Substring(0, name.Length - 4);
            }
            else
            {
                this.name = name;
            }
        }

        public LogFile(string path, string name): base(path, name)
        {
            this.ext = "log";
        }

        new public void WriteLine(string strTexto)
        {
            string strFecha, strHora, strFullName;
            try
            {
                strFecha = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00");
                //strHora = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
                strHora = DateTime.Now.ToString("HH:mm:ss fff");
                strFullName = this.Path + "\\" + this.Name + "_" + strFecha + "." + this.Ext;
                StreamWriter writer = File.AppendText(strFullName);
                writer.WriteLine(strHora + ": " + strTexto);
                writer.Close();
            }
            catch //(Exception ex)
            {
            }
        }
    }
}
