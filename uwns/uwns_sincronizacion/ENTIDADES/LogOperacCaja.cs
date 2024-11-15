using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogOperacCaja: LogOperacion
    {
        #region Fields

        private int iNumSecuencial;
        private string sCodMoneda;
        private decimal dImpOperacion;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogOperacCaja class.
        /// </summary>
        public LogOperacCaja(): base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogOperacCaja class.
        /// </summary>
        public LogOperacCaja(string sNumOperacion, int iNumSecuencial, string sCodMoneda, decimal dImpOperacion, string sCodTipoOperacion, DateTime dtFchProceso, string sCodTurno, string sCodUsuario)
            : base(sNumOperacion,sCodTipoOperacion,dtFchProceso,sCodTurno,sCodUsuario)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sCodMoneda = sCodMoneda;
            this.dImpOperacion = dImpOperacion;
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
        /// Gets or sets the dImpOperacion value.
        /// </summary>
        public decimal DImpOperacion
        {
            get { return dImpOperacion; }
            set { dImpOperacion = value; }
        }

        #endregion
    }
}
