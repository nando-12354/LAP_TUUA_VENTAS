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
        private string sLogFechaMod;
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
        public ModalidadAtrib(string sCodModalidadVenta, string sCodAtributo, string sDscValor, string sCodTipoTicket, string tip_Atributo,string sLogHoraMod, string sLogUsuarioMod, string sLogFechaMod)
        {
            this.sCodModalidadVenta = sCodModalidadVenta;
            this.sCodAtributo = sCodAtributo;
            this.tip_Atributo = tip_Atributo;
            this.sDscValor = sDscValor;
            this.sCodTipoTicket = sCodTipoTicket;
            this.sLogHoraMod = sLogHoraMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            
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
        /// Gets or sets the sLogFechaMod value.
        /// </summary>
        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
        }

        /// <summary>
        /// Gets or sets the tip_Atributo value.
        /// </summary>
        

        public string Tip_Atributo
        {
            get{ return tip_Atributo;}
            set{tip_Atributo = value;}
        }

        #endregion
    }
}
