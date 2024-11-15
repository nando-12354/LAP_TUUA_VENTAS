using System;

namespace LAP.TUUA.ENTIDADES
{
    public class BoardingBcbpErr
    {
        #region Fields

        private int iNumSecuencial;
        private string sDscTramaBcbp;
        private string cod_Tip_Error;

        private string strLogUsuarioMod;
        private string strLogFechaMod;
        private string strLogHoraMod;
        private string strTipIngreso;

        //campos para el log BCBP
        private string cod_Equipo_Mod;
        private string tip_Boarding;
        private string cod_Compania;
        private string num_Vuelo;
        private string fch_Vuelo;
        private string num_Asiento;
        private string nom_Pasajero;
        private string log_Error;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BoardingBcbpErr class.
        /// </summary>
        public BoardingBcbpErr()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BoardingBcbpErr class.
        /// </summary>
        public BoardingBcbpErr(string dsc_Trama_Bcbp)
        {
            this.sDscTramaBcbp = dsc_Trama_Bcbp;
        }

        /// <summary>
        /// Initializes a new instance of the BoardingBcbpErr class.
        /// </summary>
        public BoardingBcbpErr(int iNumSecuencial, string dsc_Trama_Bcbp)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sDscTramaBcbp = dsc_Trama_Bcbp;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sDscTramaBcbp value.
        /// </summary>
        public string SDscTramaBcbp
        {
            get { return sDscTramaBcbp; }
            set { sDscTramaBcbp = value; }
        }

        #endregion

        public string Cod_Tip_Error
        {
              get
              {
                    return cod_Tip_Error;
              }
              set
              {
                    cod_Tip_Error = value;
              }
        }

        public string StrLogUsuarioMod
        {
              get
              {
                    return strLogUsuarioMod;
              }
              set
              {
                    strLogUsuarioMod = value;
              }
        }

        public string StrLogFechaMod
        {
              get
              {
                    return strLogFechaMod;
              }
              set
              {
                    strLogFechaMod = value;
              }
        }

        public string StrLogHoraMod
        {
              get
              {
                    return strLogHoraMod;
              }
              set
              {
                    strLogHoraMod = value;
              }
        }

        public string StrTipIngreso
        {
              get
              {
                    return strTipIngreso;
              }
              set
              {
                    strTipIngreso = value;
              }
        }

        public string Cod_Equipo_Mod
        {
            get
            {
                return cod_Equipo_Mod;
            }
            set
            {
                cod_Equipo_Mod = value;
            }
        }

        public string Tip_Boarding
        {
            get
            {
                return tip_Boarding;
            }
            set
            {
                tip_Boarding = value;
            }
        }

        public string Cod_Compania
        {
            get
            {
                return cod_Compania;
            }
            set
            {
                cod_Compania = value;
            }
        }

        public string Num_Vuelo
        {
            get
            {
                return num_Vuelo;
            }
            set
            {
                num_Vuelo = value;
            }
        }

        public string Fch_Vuelo
        {
            get
            {
                return fch_Vuelo;
            }
            set
            {
                fch_Vuelo = value;
            }
        }

        public string Num_Asiento
        {
            get
            {
                return num_Asiento;
            }
            set
            {
                num_Asiento = value;
            }
        }

        public string Nom_Pasajero
        {
            get
            {
                return nom_Pasajero;
            }
            set
            {
                nom_Pasajero = value;
            }
        }

        public string Log_Error
        {
            get
            {
                return log_Error;
            }
            set
            {
                log_Error = value;
            }
        }

    }
}
