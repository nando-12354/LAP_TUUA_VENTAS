using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ModVentaCompAtr
    {
        #region Fields

        private string sCodModalidadVenta;
        private string sDscValor;
        private DateTime dtLogFechaMod;
        private string sLogUsuarioMod;
        private string sLogHoraMod;
        private string sCodCompania;
        private string sCodAtributo;
        private string sCodTipoTicket;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ModVentaCompAtr class.
        /// </summary>
        public ModVentaCompAtr()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ModVentaCompAtr class.
        /// </summary>
        public ModVentaCompAtr(string SCodModalidadVenta, string sDscValor, DateTime dtLogFechaMod, string sLogUsuarioMod, string sLogHoraMod, string sCodCompania, string sCodAtributo, string sCodTipoTicket)
        {
            this.SCodModalidadVenta = SCodModalidadVenta;
            this.sDscValor = sDscValor;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogHoraMod = sLogHoraMod;
            this.sCodCompania = sCodCompania;
            this.sCodAtributo = sCodAtributo;
            this.sCodTipoTicket = sCodTipoTicket;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodModalidadVenta value.
        /// </summary>
        public string SCodModalidadVenta
        {
            get { return sCodModalidadVenta; }
            set { sCodModalidadVenta = value; }
        }

        /// <summary>
        /// Gets or sets the sDscValor value.
        /// </summary>
        public string SDscValor
        {
            get { return sDscValor; }
            set { sDscValor = value; }
        }

        /// <summary>
        /// Gets or sets the dtLogFechaMod value.
        /// </summary>
        public DateTime DtLogFechaMod
        {
            get { return dtLogFechaMod; }
            set { dtLogFechaMod = value; }
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
        /// Gets or sets the sLogHoraMod value.
        /// </summary>
        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
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
        /// Gets or sets the sCodAtributo value.
        /// </summary>
        public string SCodAtributo
        {
            get { return sCodAtributo; }
            set { sCodAtributo = value; }
        }

        /// <summary>
        /// Gets or sets the sCodTipoTicket value.
        /// </summary>
        public string SCodTipoTicket
        {
            get { return sCodTipoTicket; }
            set { sCodTipoTicket = value; }
        }

        #endregion
    }
}
