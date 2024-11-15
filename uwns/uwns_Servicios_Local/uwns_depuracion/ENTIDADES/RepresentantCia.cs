using System;

namespace LAP.TUUA.ENTIDADES
{
    public class RepresentantCia
    {
        #region Fields

        private string sCodCompania;
        private int iNumSecuencial;
        private string sNomRepresentante;
        private string sApeRepresentante;
        private string sTipEstado;
        private string sLogUsuarioMod;
        private string dtLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RepresentantCia class.
        /// </summary>
        public RepresentantCia()
        {
        }

        /// <summary>
        /// Initializes a new instance of the RepresentantCia class.
        /// </summary>
        public RepresentantCia(string sCodCompania, string sNomRepresentante, string sApeRepresentante, string sTipEstado, string sLogUsuarioMod, string dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodCompania = sCodCompania;
            this.sNomRepresentante = sNomRepresentante;
            this.sApeRepresentante = sApeRepresentante;
            this.sTipEstado = sTipEstado;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        /// <summary>
        /// Initializes a new instance of the RepresentantCia class.
        /// </summary>
        public RepresentantCia(string sCodCompania, int iNumSecuencial, string sNomRepresentante, string sApeRepresentante, string sTipEstado, string sLogUsuarioMod, string dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodCompania = sCodCompania;
            this.iNumSecuencial = iNumSecuencial;
            this.sNomRepresentante = sNomRepresentante;
            this.sApeRepresentante = sApeRepresentante;
            this.sTipEstado = sTipEstado;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string SCodCompania
        {
            get { return sCodCompania; }
            set { sCodCompania = value; }
        }

        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sNomRepresentante value.
        /// </summary>
        public string SNomRepresentante
        {
            get { return sNomRepresentante; }
            set { sNomRepresentante = value; }
        }

        /// <summary>
        /// Gets or sets the sApeRepresentante value.
        /// </summary>
        public string SApeRepresentante
        {
            get { return sApeRepresentante; }
            set { sApeRepresentante = value; }
        }

        /// <summary>
        /// Gets or sets the sTipEstado value.
        /// </summary>
        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
        }

        /// <summary>
        /// Gets or sets the sLogUsuarioMod value.
        /// </summary>
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

        /// <summary>
        /// Gets or sets the dtLogFechaMod value.
        /// </summary>
        public string DtLogFechaMod
        {
            get { return dtLogFechaMod; }
            set { dtLogFechaMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogHoraMod value.
        /// </summary>
        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
        }

        #endregion
    }
}

