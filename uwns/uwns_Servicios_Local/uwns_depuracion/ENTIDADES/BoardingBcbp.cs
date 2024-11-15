using System;

namespace LAP.TUUA.ENTIDADES
{
    public class BoardingBcbp
    {
        #region Fields 

        private int iNumSecuencial;
        private string sCodCompania;
        private string sNumVuelo;
        private string strFchVuelo;
        private string strNumAsiento;
        private string strNomPasajero;
        private string strTrama;

        private string strLogUsuarioMod;
        private string strLogFechaMod;
        private string strLogHoraMod;
        private string strTipIngreso;

        private DateTime dtFchVuelo;
       
        


        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BoardingBcbp class.
        /// </summary>
        public BoardingBcbp()
        {
        }

        /// <summary>
        /// Initializes a new instance of the BoardingBcbp class.
        /// </summary>
        public BoardingBcbp(string sCodCompania, string sNumVuelo, DateTime dtFchVuelo)
        {
            this.sCodCompania = sCodCompania;
            this.sNumVuelo = sNumVuelo;
            this.dtFchVuelo = dtFchVuelo;
        }

        /// <summary>
        /// Initializes a new instance of the BoardingBcbp class.
        /// </summary>
        public BoardingBcbp(int iNumSecuencial, string sCodCompania, string sNumVuelo, DateTime dtFchVuelo)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sCodCompania = sCodCompania;
            this.sNumVuelo = sNumVuelo;
            this.dtFchVuelo = dtFchVuelo;
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
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string SCodCompania
        {
            get { return sCodCompania; }
            set { sCodCompania = value; }
        }

        /// <summary>
        /// Gets or sets the sNumVuelo value.
        /// </summary>
        public string SNumVuelo
        {
            get { return sNumVuelo; }
            set { sNumVuelo = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchVuelo value.
        /// </summary>
        public DateTime DtFchVuelo
        {
            get { return dtFchVuelo; }
            set { dtFchVuelo = value; }
        }


        #endregion

        public string StrFchVuelo
        {
              get
              {
                    return strFchVuelo;
              }
              set
              {
                    strFchVuelo = value;
              }
        }

        public string StrNumAsiento
        {
              get
              {
                    return strNumAsiento;
              }
              set
              {
                    strNumAsiento = value;
              }
        }

        public string StrNomPasajero
        {
              get
              {
                    return strNomPasajero;
              }
              set
              {
                    strNomPasajero = value;
              }
        }

   

        public string StrTrama
        {
              get
              {
                    return strTrama;
              }
              set
              {
                    strTrama = value;
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
