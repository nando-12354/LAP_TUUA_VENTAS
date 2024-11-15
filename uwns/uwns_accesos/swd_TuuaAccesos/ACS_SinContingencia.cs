using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ACCESOS
{
      public class ACS_SinContingencia
      {
            private bool bLectura;
            private string strTrama;

            public ACS_SinContingencia() 
            {
                  bLectura = false;
            }

            public string StrTrama
            {
                  get { return strTrama; }
                  set { strTrama = value; }
            }

            public bool BLectura
            {
                  get { return bLectura; }
                  set { bLectura = value; }
            }

      }
}
