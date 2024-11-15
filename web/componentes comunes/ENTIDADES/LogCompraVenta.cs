using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogCompraVenta : LogOperacion
    {
        #region Fields

        private string sCodMoneda;
        private long iCodTipoCambio;
        private decimal dImpTasaCambio;
        private decimal dImpACambiar;
        private decimal dImpCambiado;
        private decimal dImpIngreso;
        private decimal dImpEgreso;
        private decimal dImpEntregarNac;
        private decimal dImpEntregarInter;
        private string tip_Pago;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogCompraVenta class.
        /// </summary>
        public LogCompraVenta()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogCompraVenta class.
        /// </summary>
        public LogCompraVenta(string sNumOperacion, decimal dImpTasaCambio, decimal dImpACambiar, decimal dImpCambiado, string sCodTipoOperacion, DateTime dtFchProceso, string sCodTurno, string sCodUsuario)
            : base(sNumOperacion, sCodTipoOperacion, dtFchProceso, sCodTurno, sCodUsuario)
        {
            this.dImpTasaCambio = dImpTasaCambio;
            this.dImpACambiar = dImpACambiar;
            this.dImpCambiado = dImpCambiado;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the dImpTasaCambio value.
        /// </summary>
        public decimal DImpTasaCambio
        {
            get { return dImpTasaCambio; }
            set { dImpTasaCambio = value; }
        }

        /// <summary>
        /// Gets or sets the dImpACambiar value.
        /// </summary>
        public decimal DImpACambiar
        {
            get { return dImpACambiar; }
            set { dImpACambiar = value; }
        }

        /// <summary>
        /// Gets or sets the dImpCambiado value.
        /// </summary>
        public decimal DImpCambiado
        {
            get { return dImpCambiado; }
            set { dImpCambiado = value; }
        }

        #endregion

        public string SCodMoneda
        {
            get
            {
                return sCodMoneda;
            }
            set
            {
                sCodMoneda = value;
            }
        }

        public long ICodTipoCambio
        {
            get
            {
                return iCodTipoCambio;
            }
            set
            {
                iCodTipoCambio = value;
            }
        }

        public decimal DImpIngreso
        {
            get
            {
                return dImpIngreso;
            }
            set
            {
                dImpIngreso = value;
            }
        }

        public decimal DImpEgreso
        {
            get
            {
                return dImpEgreso;
            }
            set
            {
                dImpEgreso = value;
            }
        }

        public decimal DImpEntregarNac
        {
            get
            {
                return dImpEntregarNac;
            }
            set
            {
                dImpEntregarNac = value;
            }
        }

        public decimal DImpEntregarInter
        {
            get
            {
                return dImpEntregarInter;
            }
            set
            {
                dImpEntregarInter = value;
            }
        }

        public string Tip_Pago
        {
            get
            {
                return tip_Pago;
            }
            set
            {
                tip_Pago = value;
            }
        }

    }
}
