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

  }
}