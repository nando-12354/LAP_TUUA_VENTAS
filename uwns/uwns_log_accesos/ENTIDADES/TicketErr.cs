using System;


namespace LAP.TUUA.ENTIDADES
{
      public class TicketErr
      {
            #region Fields

            private long iNumSecuencial;
            private string sCodNumeroTicket;
            private string sCodMolinete;
            private string sCodTipoTicket;
            private string sDscError;
            private string sTipIngreso;
            private string sTipError;
            private string sLogUsuarioMod;
            private string sLogFechaMod;
            private string sLogHoraMod;

            #endregion

            #region Constructors

		    /// <summary>
		    /// Initializes a new instance of the TUA_TicketErr class.
		    /// </summary>
		    public TicketErr()
		    {
		    }
            #endregion


            public long INumSecuencial
            {
                  get
                  {
                        return iNumSecuencial;
                  }
                  set
                  {
                        iNumSecuencial = value;
                  }
            }

            public string SCodNumeroTicket
            {
                  get
                  {
                        return sCodNumeroTicket;
                  }
                  set
                  {
                        sCodNumeroTicket = value;
                  }
            }

            public string SCodMolinete
            {
                  get
                  {
                        return sCodMolinete;
                  }
                  set
                  {
                        sCodMolinete = value;
                  }
            }

            public string SCodTipoTicket
            {
                  get
                  {
                        return sCodTipoTicket;
                  }
                  set
                  {
                        sCodTipoTicket = value;
                  }
            }

            public string SDscError
            {
                  get
                  {
                        return sDscError;
                  }
                  set
                  {
                        sDscError = value;
                  }
            }

            public string STipIngreso
            {
                  get
                  {
                        return sTipIngreso;
                  }
                  set
                  {
                        sTipIngreso = value;
                  }
            }

            public string STipError
            {
                  get
                  {
                        return sTipError;
                  }
                  set
                  {
                        sTipError = value;
                  }
            }

            public string SLogUsuarioMod
            {
                  get
                  {
                        return sLogUsuarioMod;
                  }
                  set
                  {
                        sLogUsuarioMod = value;
                  }
            }

            public string SLogFechaMod
            {
                  get
                  {
                        return sLogFechaMod;
                  }
                  set
                  {
                        sLogFechaMod = value;
                  }
            }

            public string SLogHoraMod
            {
                  get
                  {
                        return sLogHoraMod;
                  }
                  set
                  {
                        sLogHoraMod = value;
                  }
            }

      }
}
