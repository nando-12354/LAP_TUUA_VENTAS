using System;

namespace LAP.TUUA.ENTIDADES
{
    public class TurnoMonto
    {
        #region Fields

        private decimal dImpMontoInicial;
        private decimal dImpMontoFinal;
        private decimal dImpMontoActual;
        private string sCodTurno;
        private string sCodMoneda;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TurnoMonto class.
        /// </summary>
        public TurnoMonto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TurnoMonto class.
        /// </summary>
        public TurnoMonto(decimal dImpMontoInicial, decimal dImpMontoFinal, decimal dImpMontoActual, string sCodTurno, string sCodMoneda)
        {
            this.dImpMontoInicial = dImpMontoInicial;
            this.dImpMontoFinal = dImpMontoFinal;
            this.dImpMontoActual = dImpMontoActual;
            this.sCodTurno = sCodTurno;
            this.sCodMoneda = sCodMoneda;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the dImpMontoInicial value.
        /// </summary>
        public decimal DImpMontoInicial
        {
            get { return dImpMontoInicial; }
            set { dImpMontoInicial = value; }
        }

        /// <summary>
        /// Gets or sets the dImpMontoFinal value.
        /// </summary>
        public decimal DImpMontoFinal
        {
            get { return dImpMontoFinal; }
            set { dImpMontoFinal = value; }
        }

        /// <summary>
        /// Gets or sets the dImpMontoActual value.
        /// </summary>
        public decimal DImpMontoActual
        {
            get { return dImpMontoActual; }
            set { dImpMontoActual = value; }
        }

        /// <summary>
        /// Gets or sets the sCodTurno value.
        /// </summary>
        public string SCodTurno
        {
            get { return sCodTurno; }
            set { sCodTurno = value; }
        }

        /// <summary>
        /// Gets or sets the sCodMoneda value.
        /// </summary>
        public string SCodMoneda
        {
            get { return sCodMoneda; }
            set { sCodMoneda = value; }
        }

        #endregion
    }
}
