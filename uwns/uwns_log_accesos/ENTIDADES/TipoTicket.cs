using System;

namespace LAP.TUUA.ENTIDADES
{
    public class TipoTicket
    {
        #region Fields

        private string sCodTipoTicket;
        private string sNomTipoTicket;
        private string sTipVuelo;
        private decimal dImpPrecio;
        private string sTipEstado;
        private string sCodMoneda;
        private string sTipPasajero;
        private string sTipTrasbordo;
        private string sLogFechaMod;
        private string sLogUsuarioMod;
        private string sLogHoraMod;
        private string dsc_Simbolo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TipoTicket class.
        /// </summary>
        public TipoTicket()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TipoTicket class.
        /// </summary>
        public TipoTicket(string sCodTipoTicket,string sNomTipoTicket, string sTipVuelo, decimal dImpPrecio, string sTipEstado, string sCodMoneda, string sTipPasajero, string sTipTrasbordo, string sLogFechaMod, string sLogUsuarioMod, string sLogHoraMod)
        {
            this.sCodTipoTicket = sCodTipoTicket;
            this.sNomTipoTicket = sNomTipoTicket;
            this.sTipVuelo = sTipVuelo;
            this.dImpPrecio = dImpPrecio;
            this.sTipEstado = sTipEstado;
            this.sCodMoneda = sCodMoneda;
            this.sTipPasajero = sTipPasajero;
            this.sTipTrasbordo = sTipTrasbordo;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodTipoTicket value.
        /// </summary>
        public string SCodTipoTicket
        {
            get { return sCodTipoTicket; }
            set { sCodTipoTicket = value; }
        }

        /// <summary>
        /// Gets or sets the STipVuelo value.
        /// </summary>
        public string STipVuelo
        {
            get { return sTipVuelo; }
            set { sTipVuelo = value; }
        }

        /// <summary>
        /// Gets or sets the dImpPrecio value.
        /// </summary>
        public decimal DImpPrecio
        {
            get { return dImpPrecio; }
            set { dImpPrecio = value; }
        }

        /// <summary>
        /// Gets or sets the STipEstado1 value.
        /// </summary>
        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
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
        /// Gets or sets the sTipPasajero value.
        /// </summary>
        public string STipPasajero
        {
            get { return sTipPasajero; }
            set { sTipPasajero = value; }
        }

        /// <summary>
        /// Gets or sets the sTipTrasbordo value.
        /// </summary>
        public string STipTrasbordo
        {
            get { return sTipTrasbordo; }
            set { sTipTrasbordo = value; }
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
        /// Gets or sets the dsc_Simbolo value.
        /// </summary>


        public string Dsc_Simbolo
        {
            get{return dsc_Simbolo;}
            set{dsc_Simbolo = value;}
        }

        /// <summary>
        /// Gets or sets the sNomTipoTicket value.
        /// </summary>
        public string SNomTipoTicket
        {
            get { return sNomTipoTicket; }
            set { sNomTipoTicket = value; }
        }
        #endregion

        

    }
}
