using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Moneda
    {
  
        #region Fields

        private string sCodMoneda;
        private string sDscMoneda;
        private string sDscSimbolo;
        private string sTipEstado;
        private string sLogUsuarioMod;
        private string dtLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Moneda class.
        /// </summary>
        public Moneda()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Moneda class.
        /// </summary>
        public Moneda(string sCodMoneda, string sDscMoneda, string sDscSimbolo, string sTipEstado, string sLogUsuarioMod, string dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodMoneda = sCodMoneda;
            this.sDscMoneda = sDscMoneda;
            this.sDscSimbolo = sDscSimbolo;
            this.sTipEstado = sTipEstado;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodMoneda value.
        /// </summary>
        public string SCodMoneda
        {
            get { return sCodMoneda; }
            set { sCodMoneda = value; }
        }

        /// <summary>
        /// Gets or sets the sDscMoneda value.
        /// </summary>
        public string SDscMoneda
        {
            get { return sDscMoneda; }
            set { sDscMoneda = value; }
        }

        /// <summary>
        /// Gets or sets the sDscSimbolo value.
        /// </summary>
        public string SDscSimbolo
        {
            get { return sDscSimbolo; }
            set { sDscSimbolo = value; }
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
        public string DtLogFechaMod
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

        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value;}
        }

    }
}
