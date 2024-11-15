using System;

namespace LAP.TUUA.ENTIDADES
{
    public class TasaCambio
    {
        #region Fields

        private string sCodTasaCambio;
        private string sTipCambio;
        private string sCodMoneda;
        private decimal dImpCambioActual;

        private string sTipIngreso;
        private string sTipEstado;        
        private DateTime dtFchProgramacion;        
        private DateTime dtFchProceso;

        private string sCodRetorno;

        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;



      


        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the TasaCambio class.
        /// </summary>
        public TasaCambio()
        {
        }

        #endregion

        #region Properties
        
        public string STipIngreso
        {
              get { return sTipIngreso; }
              set { sTipIngreso = value; }
        }

        public string STipEstado
        {
              get { return sTipEstado; }
              set { sTipEstado = value; }
        }

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
        public decimal DImpCambioActual
        {
            get { return dImpCambioActual; }
            set { dImpCambioActual = value; }
        }
        public DateTime DtFchProceso
        {
            get { return dtFchProceso; }
            set { dtFchProceso = value; }
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
        public DateTime DtFchProgramacion
        {
            get { return dtFchProgramacion; }
            set { dtFchProgramacion = value; }
        }

        public string SCodRetorno
        {
              get { return sCodRetorno; }
              set { sCodRetorno = value; }
        } 

   
        #endregion
    }
}
