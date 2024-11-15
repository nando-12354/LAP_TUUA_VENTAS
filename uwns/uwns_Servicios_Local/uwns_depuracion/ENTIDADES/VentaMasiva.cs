using System;

namespace LAP.TUUA.ENTIDADES
{
    public class VentaMasiva
    {
        #region Fields

        private string sCodVentaMasiva;
        private string dtFchVenta;
        private int dCanVenta;
        private decimal dImpMontoVenta;
        private string sCodMoneda;
        private string iNumRangoInicial;
        private string iNumRangoFinal;
        private string sCodCompania;
        private string sCodUsuario;
        private string tip_Pago;
        private string num_Cheque_Trans;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the VentaMasiva class.
        /// </summary>
        public VentaMasiva()
        {
        }

        /// <summary>
        /// Initializes a new instance of the VentaMasiva class.
        /// </summary>
        public VentaMasiva(string sCodVentaMasiva, string dtFchVenta, int dCanVenta, decimal dImpMontoVenta, string sCodMoneda, string iNumRangoInicial, string iNumRangoFinal, string sCodCompania, string sCodUsuario)
        {
            this.sCodVentaMasiva = sCodVentaMasiva;
            this.dtFchVenta = dtFchVenta;
            this.dCanVenta = dCanVenta;
            this.dImpMontoVenta = dImpMontoVenta;
            this.sCodMoneda = sCodMoneda;
            this.iNumRangoInicial = iNumRangoInicial;
            this.iNumRangoFinal = iNumRangoFinal;
            this.sCodCompania = sCodCompania;
            this.sCodUsuario = sCodUsuario;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodVentaMasiva value.
        /// </summary>
        public string SCodVentaMasiva
        {
            get { return sCodVentaMasiva; }
            set { sCodVentaMasiva = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchVenta value.
        /// </summary>
        public string DtFchVenta
        {
            get { return dtFchVenta; }
            set { dtFchVenta = value; }
        }

        /// <summary>
        /// Gets or sets the dCanVenta value.
        /// </summary>
        public int DCanVenta
        {
            get { return dCanVenta; }
            set { dCanVenta = value; }
        }

        /// <summary>
        /// Gets or sets the dImpMontoVenta value.
        /// </summary>
        public decimal DImpMontoVenta
        {
            get { return dImpMontoVenta; }
            set { dImpMontoVenta = value; }
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
        /// Gets or sets the iNumRangoInicial value.
        /// </summary>
        public string INumRangoInicial
        {
            get { return iNumRangoInicial; }
            set { iNumRangoInicial = value; }
        }

        /// <summary>
        /// Gets or sets the iNumRangoFinal value.
        /// </summary>
        public string INumRangoFinal
        {
            get { return iNumRangoFinal; }
            set { iNumRangoFinal = value; }
        }

        /// <summary>
        /// Gets or sets the sCodCompania value.
        /// </summary>
        public string SCodCompania
        {
            get { return sCodCompania; }
            set { sCodCompania = value; }
        }

        /// <summary>
        /// Gets or sets the sCodUsuario value.
        /// </summary>
        public string Tip_Pago
        {
            get { return tip_Pago; }
            set { tip_Pago = value; }
        }

        public string SCodUsuario
        {
            get { return sCodUsuario; }
            set { sCodUsuario = value; }
        }

        public string Num_Cheque_Trans
        {
            get { return num_Cheque_Trans; }
            set { num_Cheque_Trans = value; }
        }

        #endregion
    }
}
