using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Rol
    {
        #region Fields

        private string sCodRol;
        private string sCodPadreRol;
        private string sNomRol;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Rol class.
        /// </summary>
        public Rol()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Rol class.
        /// </summary>
        public Rol(string sCodRol, string sCodPadreRol, string sNomRol, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodRol = sCodRol;
            this.sCodPadreRol = sCodPadreRol;
            this.sNomRol = sNomRol;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodRol value.
        /// </summary>
        public string SCodRol
        {
            get { return sCodRol; }
            set { sCodRol = value; }
        }

        /// <summary>
        /// Gets or sets the sCodPadreRol value.
        /// </summary>
        public string SCodPadreRol
        {
            get { return sCodPadreRol; }
            set { sCodPadreRol = value; }
        }

        /// <summary>
        /// Gets or sets the sNomRol value.
        /// </summary>
        public string SNomRol
        {
            get { return sNomRol; }
            set { sNomRol = value; }
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
