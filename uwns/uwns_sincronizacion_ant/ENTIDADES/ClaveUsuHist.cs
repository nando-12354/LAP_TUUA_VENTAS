using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ClaveUsuHist
    {
        #region Fields

        private string sCodUsuario;
        private string sDsc_Clave;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;
        private int iNumContadorUsuario;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ClaveUsuHist class.
        /// </summary>
        public ClaveUsuHist()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ClaveUsuHist class.
        /// </summary>
        public ClaveUsuHist(string sCodUsuario, string sLogFechaMod, string sDsc_Clave, string sLogUsuarioMod, string sLogHoraMod, int iNumContadorUsuario)
        {
            this.sCodUsuario = sCodUsuario;
            this.sLogFechaMod = sLogFechaMod;
            this.sDsc_Clave = sDsc_Clave;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogHoraMod = sLogHoraMod;
            this.iNumContadorUsuario = iNumContadorUsuario;
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
        /// Gets or sets the sLogFechaMod value.
        /// </summary>
        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
        }

        /// <summary>
        /// Gets or sets the sDsc_Clave value.
        /// </summary>
        public string SDscClave
        {
            get { return sDsc_Clave; }
            set { sDsc_Clave = value; }
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
        /// Gets or sets the iNumContadorUsuario value.
        /// </summary>
        public int INumContadorUsuario
        {
            get { return iNumContadorUsuario; }
            set { iNumContadorUsuario = value; }
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
