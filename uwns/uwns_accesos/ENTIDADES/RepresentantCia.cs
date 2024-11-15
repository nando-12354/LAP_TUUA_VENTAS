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
        private string sCargoRepresentante;
        private string sTDocRepresentante;
        private string sNDocRepresentante;
        private string sPermRepresentante;
        private string sTipEstado;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
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
        public RepresentantCia(string sCodCompania, int iNumSecuencial, string sNomRepresentante, string sApeRepresentante, string sCargoRepresentante,string sTipEstado, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod,string sTDocRepresentante,string sNDocRepresentante,string sPermRepresentante)
        {
            this.sCodCompania = sCodCompania;
            this.iNumSecuencial = iNumSecuencial;
            this.sNomRepresentante = sNomRepresentante;
            this.sApeRepresentante = sApeRepresentante;
            this.sCargoRepresentante = sCargoRepresentante;
            this.sTDocRepresentante = sTDocRepresentante;
            this.sNDocRepresentante = sNDocRepresentante;
            this.sPermRepresentante = sPermRepresentante;
            this.sTipEstado = sTipEstado;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
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
        /// Gets or sets the sCargoRepresentante value.
        /// </summary>

        public string SCargoRepresentante
        {
            get { return sCargoRepresentante; }
            set { sCargoRepresentante = value; }
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
        /// Gets or sets the sTDocRepresentante value.
        /// </summary>
        public string STDocRepresentante
        {
            get { return sTDocRepresentante; }
            set { sTDocRepresentante = value; }
        }

        /// <summary>
        /// Gets or sets the sLogHoraMod value.
        /// </summary>
        public string SNDocRepresentante
        {
            get { return sNDocRepresentante; }
            set { sNDocRepresentante = value; }
        }


        /// <summary>
        /// Gets or sets the sPermRepresentante value.
        /// </summary>
        public string SPermRepresentante
        {
            get { return sPermRepresentante; }
            set { sPermRepresentante = value; }
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
        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
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

