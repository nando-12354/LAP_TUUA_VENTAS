using System;

namespace LAP.TUUA.ENTIDADES
{
    public class TipoOperacion
    {
        #region Fields

        private string sCodTipoOperacion;
        private string sNomTipoOperacion;
        private string sLogUsuarioMod;
        private DateTime dtLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TipoOperacion class.
        /// </summary>
        public TipoOperacion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TipoOperacion class.
        /// </summary>
        public TipoOperacion(string sCodTipoOperacion, string sNomTipoOperacion, string sLogUsuarioMod, DateTime dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodTipoOperacion = sCodTipoOperacion;
            this.sNomTipoOperacion = sNomTipoOperacion;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodTipoOperacion value.
        /// </summary>
        public string SCodTipoOperacion
        {
            get { return sCodTipoOperacion; }
            set { sCodTipoOperacion = value; }
        }

        /// <summary>
        /// Gets or sets the sNomTipoOperacion value.
        /// </summary>
        public string SNomTipoOperacion
        {
            get { return sNomTipoOperacion; }
            set { sNomTipoOperacion = value; }
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
        public DateTime DtLogFechaMod
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
