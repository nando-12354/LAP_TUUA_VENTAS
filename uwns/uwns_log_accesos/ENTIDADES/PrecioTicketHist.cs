using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class PrecioTicketHist
    {
        #region Fields

        private string sCodPrecioTicket;
        private string sCodTipoTicket;
        private string sCodMoneda;
        private decimal dImpPrecio;
        private string sCodMoneda2;
        private decimal dImpPrecio2;
        private DateTime dtFchIni;
        private DateTime dtFchFin;
        
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PrecioTicketHist class.
        /// </summary>
        public PrecioTicketHist()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PrecioTicketHist class.
        /// </summary>
        public PrecioTicketHist(string sCodPrecioTicket, string sCodTipoTicket, string sCodMoneda, decimal dImpPrecio,
                            string sCodMoneda2, decimal dImpPrecio2, DateTime dtFchIni, DateTime dtFchFin,
                            string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodPrecioTicket = sCodPrecioTicket;
            this.sCodTipoTicket = sCodTipoTicket;
            this.sCodMoneda = sCodMoneda;
            this.dImpPrecio = dImpPrecio;
            this.sCodMoneda2 = sCodMoneda2;
            this.dImpPrecio2 = dImpPrecio2;
            this.dtFchIni = dtFchIni;
            this.dtFchFin = dtFchFin;

            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;            
            this.sLogHoraMod = sLogHoraMod;
        }
        #endregion

        #region Properties
        public string SCodPrecioTicket
        {
            get { return sCodPrecioTicket; }
            set { sCodPrecioTicket = value; }
        }
        public string SCodTipoTicket
        {
            get { return sCodTipoTicket; }
            set { sCodTipoTicket = value; }
        }
        public string SCodMoneda
        {
            get { return sCodMoneda; }
            set { sCodMoneda = value; }
        }
        public decimal DImpPrecio
        {
            get { return dImpPrecio; }
            set { dImpPrecio = value; }
        }
        public string SCodMoneda2
        {
            get { return sCodMoneda2; }
            set { sCodMoneda2 = value; }
        }
        public decimal DImpPrecio2
        {
            get { return dImpPrecio2; }
            set { dImpPrecio2 = value; }
        }
        public DateTime DtFchIni
        {
            get { return dtFchIni; }
            set { dtFchIni = value; }
        }
        public DateTime DtFchFin
        {
            get { return dtFchFin; }
            set { dtFchFin = value; }
        }
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }
        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
        }
        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
        }
        #endregion
    }
}
