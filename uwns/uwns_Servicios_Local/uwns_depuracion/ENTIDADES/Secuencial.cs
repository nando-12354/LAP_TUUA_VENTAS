using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Secuencial
    {
        #region Fields

        private string sCodSecuencial;
        private string sDscUltimoValor;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Secuencial class.
        /// </summary>
        public Secuencial()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Secuencial class.
        /// </summary>
        public Secuencial(string sCodSecuencial, string sDscUltimoValor)
        {
            this.sCodSecuencial = sCodSecuencial;
            this.sDscUltimoValor = sDscUltimoValor;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodSecuencial value.
        /// </summary>
        public string SCodSecuencial
        {
            get { return sCodSecuencial; }
            set { sCodSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sDscUltimoValor value.
        /// </summary>
        public string SDscUltimoValor
        {
            get { return sDscUltimoValor; }
            set { sDscUltimoValor = value; }
        }

        #endregion
    }
}
