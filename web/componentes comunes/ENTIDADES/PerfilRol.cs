using System;

namespace LAP.TUUA.ENTIDADES
{
    public class PerfilRol
    {
        #region Fields

        private string sCodProceso;
        private string sDscProceso;
        private string sCodModulo;
        private string sCodRol;
        private string sFlgPermitido;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PerfilRol class.
        /// </summary>
        public PerfilRol()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PerfilRol class.
        /// </summary>
        public PerfilRol(string sCodProceso, string sCodModulo, string sCodRol, string sFlgPermitido, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodProceso = sCodProceso;
            this.sCodModulo = sCodModulo;
            this.sCodRol = sCodRol;
            this.sFlgPermitido = sFlgPermitido;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodProceso value.
        /// </summary>
        public string SCodProceso
        {
            get { return sCodProceso; }
            set { sCodProceso = value; }
        }

        /// <summary>
        /// Gets or sets the sCodModulo value.
        /// </summary>
        public string SCodModulo
        {
            get { return sCodModulo; }
            set { sCodModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sCodRol value.
        /// </summary>
        public string SCodRol
        {
            get { return sCodRol; }
            set { sCodRol = value; }
        }

        /// <summary>
        /// Gets or sets the sFlgPermitido value.
        /// </summary>
        public string SFlgPermitido
        {
            get { return sFlgPermitido; }
            set { sFlgPermitido = value; }
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

        /// <summary>
        /// Gets or sets the sDscProceso value.
        /// </summary>
        public string SDscProceso
        {
            get { return sDscProceso; }
            set { sDscProceso = value; }
        }

        #endregion

    }
}
