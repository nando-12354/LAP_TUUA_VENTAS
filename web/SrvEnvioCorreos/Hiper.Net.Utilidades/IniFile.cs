using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Hiper.Net.Utilidades.Archivos
{
    /// <summary>
    /// Clase que representa un archivo de configuración INI.
    /// Proporciona los métodos para leer y escribir valores en el archivo.
    /// </summary>
    public class IniFile : BasicFile
    {
        private const int MAXLINESIZE = 1000;

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        private static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        new private void setName(string name)
        {
            if (name.EndsWith(".ini")) // Han proporcionado el nombre del archivo con extensión
            {
                this.name = name.Substring(0, name.Length - 4);
            }
            else
            {
                this.name = name;
            }
        }

        public IniFile(string path, string name)
        {
            setPath(path);
            setName(name);
            this.ext = "ini";
        }

        public string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(MAXLINESIZE);
            int i = GetPrivateProfileString(Section, Key, "", temp, MAXLINESIZE, CompleteName);
            return temp.ToString();
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            int i = WritePrivateProfileString(Section, Key, Value, CompleteName);
        }
    }
}
