using System;

namespace LAP.TUUA.ALARMASCLR
{
    public class Alarma
    {
        #region Fields

        private string sCodAlarma;
        private string sDscNombre;
        private string sCodModulo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Alarma class.
        /// </summary>
        public Alarma()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Alarma class.
        /// </summary>
        public Alarma(string sCodAlarma, string sDscNombre, string sCodModulo)
        {
            this.sCodAlarma = sCodAlarma;
            this.sDscNombre = sDscNombre;
            this.sCodModulo = sCodModulo;
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
        /// Gets or sets the sDscNombre value.
        /// </summary>
        public string SDscNombre
        {
            get { return sDscNombre; }
            set { sDscNombre = value; }
        }

        /// <summary>
        /// Gets or sets the sCodModulo value.
        /// </summary>
        public string SCodModulo
        {
            get { return sCodModulo; }
            set { sCodModulo = value; }
        }

        #endregion
    }
}
