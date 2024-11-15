using System;

namespace LAP.TUUA.ENTIDADES
{
    public class UsuarioHist
    {
        #region Fields

        private string sCodUsuario;
        private string sTipEstado;
        private DateTime dtLogFechaMod;
        private string sLogUsuarioMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UsuarioHist class.
        /// </summary>
        public UsuarioHist()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UsuarioHist class.
        /// </summary>
        public UsuarioHist(string sCodUsuario, string sTipEstado, DateTime dtLogFechaMod, string sLogUsuarioMod, string sLogHoraMod)
        {
            this.sCodUsuario = sCodUsuario;
            this.sTipEstado = sTipEstado;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodUsuario value.
        /// </summary>
        public string SCodUsuario
        {
            get { return sCodUsuario; }
            set { sCodUsuario = value; }
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
        /// Gets or sets the dtLogFechaMod value.
        /// </summary>
        public DateTime DtLogFechaMod
        {
            get { return dtLogFechaMod; }
            set { dtLogFechaMod = value; }
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
