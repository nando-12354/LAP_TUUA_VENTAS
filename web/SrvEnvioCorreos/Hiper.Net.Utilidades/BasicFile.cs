using System;
using System.Collections.Generic;
using System.Text;

namespace Hiper.Net.Utilidades.Archivos
{
    public class BasicFile
    {
        protected string path;
        protected string name;
        protected string ext;

        public string Path
        {
            get { return path; }
            set { setPath(value); }
        }

        public string Name
        {
            get { return name; }
            set { setName(name); }
        }

        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        public string CompleteName
        {
            get
            {
                return path + name + "." + ext;
            }
            set
            {
                char dirSep = '\\';
                if (!value.Contains("" + '\\'))
                {
                    dirSep = '/';
                }
                bool contNec = value.Contains("" + '.') && value.Contains("" + dirSep);
                int liPunto = value.LastIndexOf('.');
                int liDirSep = value.LastIndexOf('\\');
                if (contNec && liDirSep < liPunto)
                {
                    int iExt = liPunto + 1;
                    this.ext = value.Substring(iExt);

                    int iName = liDirSep + 1;
                    this.name = value.Substring(iName, iExt - iName - 1);
                    this.path = value.Substring(0, iName - 1);
                }
            }
        }

        protected void setName(string name)
        {
            this.name = name;
        }

        protected void setPath(string path)
        {
            char dirSep = '\\';
            if (path.IndexOf('\\') == -1)
            {
                dirSep = '/';
            }

            if (path.EndsWith("" + dirSep))
            {
                this.path = path;
            }
            else
            {
                this.path = path + dirSep;
            }
        }
    }
}
