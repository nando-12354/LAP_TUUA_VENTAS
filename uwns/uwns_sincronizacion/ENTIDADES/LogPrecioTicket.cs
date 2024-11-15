using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogPrecioTicket
    {
        #region Fields

        private int iNumSecuencial;
        private string sCodTipoTicket;
        private string sCodMoneda;
        private double dImpPrecioActual;
        private double dImpPrecioAnterior;
        private string sCodMonedaBase;
        private DateTime dtFchInicio;
        private DateTime dtFchFinal;
        private string sLogUsuarioMod;
        private DateTime dtLogFechaMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogPrecioTicket class.
        /// </summary>
        public LogPrecioTicket()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogPrecioTicket class.
        /// </summary>
        public LogPrecioTicket(string sCodTipoTicket, string sCodMoneda, double dImpPrecioActual, double dImpPrecioAnterior, string sCodMonedaBase, DateTime dtFchInicio, DateTime dtFchFinal, string sLogUsuarioMod, DateTime dtLogFechaMod)
        {
            this.sCodTipoTicket = sCodTipoTicket;
            this.sCodMoneda = sCodMoneda;
            this.dImpPrecioActual = dImpPrecioActual;
            this.dImpPrecioAnterior = dImpPrecioAnterior;
            this.sCodMonedaBase = sCodMonedaBase;
            this.dtFchInicio = dtFchInicio;
            this.dtFchFinal = dtFchFinal;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
        }

        /// <summary>
        /// Initializes a new instance of the LogPrecioTicket class.
        /// </summary>
        public LogPrecioTicket(int iNumSecuencial, string sCodTipoTicket, string sCodMoneda, double dImpPrecioActual, double dImpPrecioAnterior, string sCodMonedaBase, DateTime dtFchInicio, DateTime dtFchFinal, string sLogUsuarioMod, DateTime dtLogFechaMod)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sCodTipoTicket = sCodTipoTicket;
            this.sCodMoneda = sCodMoneda;
            this.dImpPrecioActual = dImpPrecioActual;
            this.dImpPrecioAnterior = dImpPrecioAnterior;
            this.sCodMonedaBase = sCodMonedaBase;
            this.dtFchInicio = dtFchInicio;
            this.dtFchFinal = dtFchFinal;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sCodTipoTicket value.
        /// </summary>
        public string SCodTipoTicket
        {
            get { return sCodTipoTicket; }
            set { sCodTipoTicket = value; }
        }

        /// <summary>
        /// Gets or sets the sCodMoneda value.
        /// </summary>
        public string SCodMoneda
        {
            get { return sCodMoneda; }
            set { sCodMoneda = value; }
        }

        /// <summary>
        /// Gets or sets the dImpPrecioActual value.
        /// </summary>
        public double DImpPrecioActual
        {
            get { return dImpPrecioActual; }
            set { dImpPrecioActual = value; }
        }

        /// <summary>
        /// Gets or sets the dImpPrecioAnterior value.
        /// </summary>
        public double DImpPrecioAnterior
        {
            get { return dImpPrecioAnterior; }
            set { dImpPrecioAnterior = value; }
        }

        /// <summary>
        /// Gets or sets the sCodMonedaBase value.
        /// </summary>
        public string SCodMonedaBase
        {
            get { return sCodMonedaBase; }
            set { sCodMonedaBase = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchInicio value.
        /// </summary>
        public DateTime DtFchInicio
        {
            get { return dtFchInicio; }
            set { dtFchInicio = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchFinal value.
        /// </summary>
        public DateTime DtFchFinal
        {
            get { return dtFchFinal; }
            set { dtFchFinal = value; }
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
        public DateTime DtLogFechaMod
        {
            get { return dtLogFechaMod; }
            set { dtLogFechaMod = value; }
        }

        #endregion
    }
}
