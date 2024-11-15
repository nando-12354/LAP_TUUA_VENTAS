using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Hiper.Net.Utilidades.Archivos
{
    /// <summary>
    /// Summary description for TextFile.
    /// </summary>
    public class TextFile : BasicFile
    {
        new private void setName(string name)
        {
            if (name.EndsWith(".txt")) // Han proporcionado el nombre del archivo con extensión
            {
                this.name = name.Substring(0, name.Length - 4);
            }
            else
            {
                this.name = name;
            }
        }

        public TextFile(string path, string name)
        {
            setPath(path);
            setName(name);
            this.ext = "txt";
        }

        public void WriteLine(string strTexto)
        {
            StreamWriter writer = File.AppendText(this.CompleteName);
            writer.WriteLine(strTexto);
            writer.Close();
        }
    }
}
