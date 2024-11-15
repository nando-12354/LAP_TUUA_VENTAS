using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class BoardingBcbpEstHist
    {
        #region Fields 

        private int iNumSecuencial;
        private int iNumSecuencialBoarding;
        private string sTipEstado;
        private string sDscBoardingEstado;
        private string sCodEquipoMod;
        private string sDscNumVuelo;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion


        #region Constructors

		/// <summary>
		/// Initializes a new instance of the TicketEstHist class.
		/// </summary>
		public BoardingBcbpEstHist()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TicketEstHist class.
		/// </summary>
        public BoardingBcbpEstHist(int iNumSecuencial, int iNumSecuencialBoarding, string sTipEstado,
            string sDscBoardingEstado, string sCodEquipoMod, string sDscNumVuelo, string sLogUsuarioMod, 
            string sLogFechaMod, string sLogHoraMod)
		{
            this.iNumSecuencial = iNumSecuencial;
            this.iNumSecuencialBoarding = iNumSecuencialBoarding;
            this.sTipEstado = sTipEstado;
            this.sDscBoardingEstado = sDscBoardingEstado;
            this.sCodEquipoMod = sCodEquipoMod;
            this.sDscNumVuelo = sDscNumVuelo;
			this.sLogUsuarioMod = sLogUsuarioMod;
			this.sLogFechaMod = sLogFechaMod;
			this.sLogHoraMod = sLogHoraMod;
		}

		#endregion

        #region Properties

        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        public int INumSecuencialBoarding
        {
            get { return iNumSecuencialBoarding; }
            set { iNumSecuencialBoarding = value; }
        }


        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
        }


        public string SDscBoardingEstado
        {
            get { return sDscBoardingEstado; }
            set { sDscBoardingEstado = value; }
        }


        public string SCodEquipoMod
        {
            get { return sCodEquipoMod; }
            set { sCodEquipoMod = value; }
        }


        public string SDscNumVuelo
        {
            get { return sDscNumVuelo; }
            set { sDscNumVuelo = value; }
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
