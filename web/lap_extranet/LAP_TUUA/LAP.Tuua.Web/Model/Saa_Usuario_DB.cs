using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAP.Tuua.Web.Model
{
    public class Saa_Usuario_DB
    {
        public int AAUS_CODIGO { get; set; }
        public string AAUS_LOGIN { get; set; }
        public string AAUS_PASSWORD { get; set; }

        public string AAUS_APELLIDOS { get; set; }
        public string AAUS_EMAIL { get; set; }
        public bool AAUS_ACTIVO { get; set; }
        public bool AAUS_EXTERNO { get; set; }


    }
}