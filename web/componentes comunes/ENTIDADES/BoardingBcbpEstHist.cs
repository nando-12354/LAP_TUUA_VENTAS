﻿using System;
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

        private string sDesCompania;
        private string sDesMotivo;

        private string sCausalRehabilitacion;
        private string sListaBCBPs;

        //CMONTES REH ASOCIADOS
        private string sEstadoAsoc;
        private string sCompaniaAsoc;
        private string sFechaVueloAsoc;
        private string sNroVueloAsoc;
        private string sNroAsientoAsoc;
        private string sPasajeroAsoc;

        //JORTEGA
        private string lst_Bloqueados;

        //CMONTES 20-12-2010
        private string sListaOperBcbp;
        private string sFechaRehab;
        public string Cod_SubModulo_Mod { get; set; }
        public string Cod_Modulo_Mod { get; set; }
        

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

        public string SCausalRehabilitacion
        {
            get { return sCausalRehabilitacion; }
            set { sCausalRehabilitacion = value; }
        }

        public string SListaBcbPs
        {
            get { return sListaBCBPs; }
            set { sListaBCBPs = value; }
        }

        public string SEstadoAsoc
        {
            get { return sEstadoAsoc; }
            set { sEstadoAsoc = value; }
        }

        public string SCompaniaAsoc
        {
            get { return sCompaniaAsoc; }
            set { sCompaniaAsoc = value; }
        }

        public string SFechaVueloAsoc
        {
            get { return sFechaVueloAsoc; }
            set { sFechaVueloAsoc = value; }
        }


        public string SNroVueloAsoc
        {
            get { return sNroVueloAsoc; }
            set { sNroVueloAsoc = value; }
        }

        public string SNroAsientoAsoc
        {
            get { return sNroAsientoAsoc; }
            set { sNroAsientoAsoc = value; }
        }


        public string SPasajeroAsoc
        {
            get { return sPasajeroAsoc; }
            set { sPasajeroAsoc = value; }
        }

        public string Lst_Bloqueados
        {
            get { return lst_Bloqueados; }
            set { lst_Bloqueados = value; }
        }

        public string SListaOperBcbp
        {
            get { return sListaOperBcbp; }
            set { sListaOperBcbp = value; }
        }

        public string SFechaRehab
        {
            get { return sFechaRehab; }
            set { sFechaRehab = value; }
        }

        public string SDesCompania
        {
            get { return sDesCompania; }
            set { sDesCompania = value; }
        }

        public string SDesMotivo
        {
            get { return sDesMotivo; }
            set { sDesMotivo = value; }
        }


        #endregion
    }
}
