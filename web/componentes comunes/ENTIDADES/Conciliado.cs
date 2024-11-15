using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class Conciliado
    {
        private string sBcbpBase;
        private string sBcbpAsociados;
        private string sBcbpUlt;

        public string SBcbpUlt
        {
            get { return sBcbpUlt; }
            set { sBcbpUlt = value; }
        }

        public string SBcbpAsociados
        {
            get { return sBcbpAsociados; }
            set { sBcbpAsociados = value; }
        }
        
        public string SBcbpBase
        {
            get { return sBcbpBase; }
            set { sBcbpBase = value; }
        }
        
    }
}
