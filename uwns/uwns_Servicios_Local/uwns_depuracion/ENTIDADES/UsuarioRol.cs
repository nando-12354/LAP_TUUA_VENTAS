using System;

namespace LAP.TUUA.ENTIDADES
{
    public class UsuarioRol
    {
        #region Fields

        private string sCodUsuario;
        private string sCodRol;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the UsuarioRol class.
        /// </summary>
        public UsuarioRol()
        {
        }

        /// <summary>
        /// Initializes a new instance of the UsuarioRol class.
        /// </summary>
        public UsuarioRol(string sCodUsuario, string sCodRol, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodUsuario = sCodUsuario;
            this.sCodRol = sCodRol;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
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
        /// Gets or sets the sCodRol value.
        /// </summary>
        public string SCodRol
        {
            get { return sCodRol; }
            set { sCodRol = value; }
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

        #endregion
    }
}
