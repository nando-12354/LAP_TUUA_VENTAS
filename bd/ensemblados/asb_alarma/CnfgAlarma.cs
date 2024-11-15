using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ALARMASCLR
{
    public class CnfgAlarma
    {
        #region Fields

        private string sCodAlarma;
        private string sCodModulo;
        private string sDscFinMensaje;
        private string sDscDestinatarios;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Alarma class.
        /// </summary>
        public CnfgAlarma()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Alarma class.
        /// </summary>
        public CnfgAlarma(string sCodAlarma, string sCodModulo, string sDscFinMensaje, string sDscDestinatarios, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodAlarma = sCodAlarma;
            this.sDscFinMensaje = sDscFinMensaje;
            this.sCodModulo = sCodModulo;
            this.sDscDestinatarios = sDscDestinatarios;
            this.sLogUsuarioMod=sLogUsuarioMod;
            this.sLogFechaMod=sLogFechaMod;
            this.sLogHoraMod=sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodAlarma value.
        /// </summary>
        public string SCodAlarma
        {
            get { return sCodAlarma; }
            set { sCodAlarma = value; }
        }

        /// <summary>
        /// Gets or sets the sDscFinMensaje value.
        /// </summary>
        public string SDscFinMensaje
        {
            get { return sDscFinMensaje; }
            set { sDscFinMensaje = value; }
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
        /// Gets or sets the sDscDestinatarios value.
        /// </summary>
        public string SDscDestinatarios
        {
            get { return sDscDestinatarios; }
            set { sDscDestinatarios = value; }
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
