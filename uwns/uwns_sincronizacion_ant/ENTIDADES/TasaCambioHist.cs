using System;

namespace LAP.TUUA.ENTIDADES
{
    public class TasaCambioHist
    {
        #region Fields

        private string sCodTasaCambio;        
        private string sTipCambio;        
        private string sCodMoneda;
        private double dImpValor;
        private string sCodMoneda2;
        private double dImpValor2;
        private DateTime dtFchIni;
        private DateTime dtFchFin;

        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TasaCambioHist class.
        /// </summary>
        public TasaCambioHist()
        {
        }

        #endregion

        #region Properties
        public string SCodTasaCambio
        {
            get { return sCodTasaCambio; }
            set { sCodTasaCambio = value; }
        }
        public string STipCambio
        {
            get { return sTipCambio; }
            set { sTipCambio = value; }
        }
        public string SCodMoneda
        {
            get { return sCodMoneda; }
            set { sCodMoneda = value; }
        }
        public double DImpValor
        {
            get { return dImpValor; }
            set { dImpValor = value; }
        }
        public string SCodMoneda2
        {
            get { return sCodMoneda2; }
            set { sCodMoneda2 = value; }
        }
        public double DImpValor2
        {
            get { return dImpValor2; }
            set { dImpValor2 = value; }
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
