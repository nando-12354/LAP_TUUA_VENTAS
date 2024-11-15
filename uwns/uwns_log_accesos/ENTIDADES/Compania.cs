using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Compania
    {
        #region Fields

        private string sCodCompania;
        private string sTipCompania;
        private string sDscCompania;
        private DateTime dtFchCreacion;
        private string sCodEspecialCompania;
        private string sCodAerolinea;
        private string sCodSAP;
        private string sCodOACI;
        private string sCodIATA;
        private string sTipEstado;

         private string sDscRuc;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        
        

        

        #endregion

      
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Compania class.
        /// </summary>
        public Compania()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Compania class.
        /// </summary>
        public Compania(string sCodCompania, string sTipCompania, string sDscCompania, DateTime dtFchCreacion, string sTipEstado, string sCodEspecialCompania, string sDscRuc, string sCodAerolinea, string sCodSAP, string sCodOACI, string sCodIATA,string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodCompania = sCodCompania;
            this.sTipCompania = sTipCompania;
            this.sDscCompania = sDscCompania;
            this.dtFchCreacion = dtFchCreacion;
            this.sTipEstado = sTipEstado;
            this.sCodEspecialCompania = sCodEspecialCompania;
            this.sCodAerolinea = sCodAerolinea;
            this.sCodSAP = sCodSAP;
            this.sCodOACI = sCodOACI;
            this.sCodIATA = sCodIATA;
            this.sDscRuc = sDscRuc;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties

        public string SCodAerolinea
        {
              get { return sCodAerolinea; }
              set { sCodAerolinea = value; }
        }


        public string SCodSAP
        {
              get { return sCodSAP; }
              set { sCodSAP = value; }
        }


        public string SCodOACI
        {
              get { return sCodOACI; }
              set { sCodOACI = value; }
        }


        public string SCodIATA
        {
              get { return sCodIATA; }
              set { sCodIATA = value; }
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
        /// Gets or sets the sTipCompania value.
        /// </summary>
        public string STipCompania
        {
            get { return sTipCompania; }
            set { sTipCompania = value; }
        }

        /// <summary>
        /// Gets or sets the sDscCompania value.
        /// </summary>
        public string SDscCompania
        {
            get { return sDscCompania; }
            set { sDscCompania = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchCreacion value.
        /// </summary>
        public DateTime DtFchCreacion
        {
            get { return dtFchCreacion; }
            set { dtFchCreacion = value; }
        }

        /// <summary>
        /// Gets or sets the STipEstado1 value.
        /// </summary>
        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
        }

        /// <summary>
        /// Gets or sets the sCodEspecialCompania value.
        /// </summary>
        public string SCodEspecialCompania
        {
            get { return sCodEspecialCompania; }
            set { sCodEspecialCompania = value; }
        }

        /// <summary>
        /// Gets or sets the sDscRuc value.
        /// </summary>
        public string SDscRuc
        {
            get { return sDscRuc; }
            set { sDscRuc = value; }
        }

        /// Gets or sets the sLogUsuarioMod value.
        /// </summary>
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogFechaMod value.
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
