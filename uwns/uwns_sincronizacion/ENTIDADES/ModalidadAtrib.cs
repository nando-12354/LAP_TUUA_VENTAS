using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ModalidadAtrib
    {
        #region Fields

        private string sCodModalidadVenta;
        private string sCodAtributo;
        private string sDscValor;
        private string sCodTipoTicket;
        private string sLogHoraMod;
        private string sLogUsuarioMod;
        private DateTime dtLogFechaMod;
        private string tip_Atributo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ModalidadAtrib class.
        /// </summary>
        public ModalidadAtrib()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ModalidadAtrib class.
        /// </summary>
        public ModalidadAtrib(string sCodModalidadVenta, string sCodAtributo, string sDscValor, string sCodTipoTicket, string sLogHoraMod, string sLogUsuarioMod, DateTime dtLogFechaMod)
        {
            this.sCodModalidadVenta = sCodModalidadVenta;
            this.sCodAtributo = sCodAtributo;
            this.sDscValor = sDscValor;
            this.sCodTipoTicket = sCodTipoTicket;
            this.sLogHoraMod = sLogHoraMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
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
        /// Gets or sets the sCodAtributo value.
        /// </summary>
        public string SCodAtributo
        {
            get { return sCodAtributo; }
            set { sCodAtributo = value; }
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
        /// Gets or sets the sCodTipoTicket value.
        /// </summary>
        public string SCodTipoTicket
        {
            get { return sCodTipoTicket; }
            set { sCodTipoTicket = value; }
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

        public string Tip_Atributo
        {
            get
            {
                return tip_Atributo;
            }
            set
            {
                tip_Atributo = value;
            }
        }

    }
}
