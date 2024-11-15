using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class PrecioTicket
    {
        #region Fields

        private string sCodPrecioTicket;
        private string sCodTipoTicket;
        private string sCodMoneda;        
        private decimal dImpPrecio;        
        private DateTime dtFchCreacion;

        private string sLogUsuarioMod;       
        private string sLogFechaMod;        
        private string sLogHoraMod;        

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PrecioTicket class.
        /// </summary>
        public PrecioTicket()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PrecioTicket class.
        /// </summary>
        public PrecioTicket(string sCodPrecioTicket, string sCodTipoTicket, string sCodMoneda, decimal dImpPrecio,
                            string sLogFechaMod, string sLogUsuarioMod, string sLogHoraMod)
        {
            this.sCodPrecioTicket = sCodPrecioTicket;
            this.sCodTipoTicket = sCodTipoTicket;
            this.sCodMoneda = sCodMoneda;
            this.dImpPrecio = dImpPrecio;
            
            this.sLogFechaMod = sLogFechaMod;
            this.sLogUsuarioMod = sLogUsuarioMod;
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
        public DateTime DtFchCreacion
        {
            get { return dtFchCreacion; }
            set { dtFchCreacion = value; }
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
