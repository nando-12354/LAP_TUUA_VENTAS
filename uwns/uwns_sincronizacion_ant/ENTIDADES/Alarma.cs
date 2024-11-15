using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Alarma
    {
        #region Fields

        private int iCodAlarma;
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
        public Alarma(int iCodAlarma, string sDscNombre, string sCodModulo)
        {
            this.iCodAlarma = iCodAlarma;
            this.sDscNombre = sDscNombre;
            this.sCodModulo = sCodModulo;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the iCodAlarma value.
        /// </summary>
        public int ICodAlarma
        {
            get { return iCodAlarma; }
            set { iCodAlarma = value; }
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
