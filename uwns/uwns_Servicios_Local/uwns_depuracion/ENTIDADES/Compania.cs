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
        private string sTipEstado;
        private string sCodEspecialCompania;
        private string sDscRuc;
        private string sLogUsuarioMod;
        private string dtLogFechaMod;
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
        public Compania(string sCodCompania, string sTipCompania, string sDscCompania, DateTime dtFchCreacion, string sTipEstado, string sCodEspecialCompania, string sDscRuc, string sLogUsuarioMod, string dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodCompania = sCodCompania;
            this.sTipCompania = sTipCompania;
            this.sDscCompania = sDscCompania;
            this.dtFchCreacion = dtFchCreacion;
            this.sTipEstado = sTipEstado;
            this.sCodEspecialCompania = sCodEspecialCompania;
            this.sDscRuc = sDscRuc;
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
        /// Gets or sets the sTipEstado value.
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
