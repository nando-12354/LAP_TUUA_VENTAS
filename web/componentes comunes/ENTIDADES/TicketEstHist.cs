using System;

namespace LAP.TUUA.ENTIDADES
{
	public class TicketEstHist
	{
		#region Fields

		private string sCodNumeroTicket;
        private int    intNum_Secuencial;
		private string sTipEstado;
		private string sDscTicketEstado;
		private string sCodEquipoMod;
		private string sDscNumVuelo;
		private string sLogUsuarioMod;
		private string sLogFechaMod;
		private string sLogHoraMod;
        private string sCausalRehabilitacion;
        private string sDscMotivo;
        private string sNroOperacion;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TicketEstHist class.
		/// </summary>
		public TicketEstHist()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TicketEstHist class.
		/// </summary>
        public TicketEstHist(string sCodNumeroTicket, string sTipEstado, string sDscTicketEstado, string sCodEquipoMod, string sDscNumVuelo, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod, string sCausalRehabilitacion)
		{
			this.sCodNumeroTicket = sCodNumeroTicket;
			this.sTipEstado = sTipEstado;
			this.sDscTicketEstado = sDscTicketEstado;
			this.sCodEquipoMod = sCodEquipoMod;
			this.sDscNumVuelo = sDscNumVuelo;
			this.sLogUsuarioMod = sLogUsuarioMod;
			this.sLogFechaMod = sLogFechaMod;
			this.sLogHoraMod = sLogHoraMod;
            this.sCausalRehabilitacion = sCausalRehabilitacion;
		}

        public TicketEstHist(string sCodNumeroTicket, string sTipEstado, string sDscTicketEstado, string sCodEquipoMod, string sDscNumVuelo, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod, string sCausalRehabilitacion,string sDscMotivo, string sNroOperacion)
        {
            this.sCodNumeroTicket = sCodNumeroTicket;
            this.sTipEstado = sTipEstado;
            this.sDscTicketEstado = sDscTicketEstado;
            this.sCodEquipoMod = sCodEquipoMod;
            this.sDscNumVuelo = sDscNumVuelo;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
            this.sCausalRehabilitacion = sCausalRehabilitacion;
            this.sDscMotivo = sDscMotivo;
            this.sNroOperacion = sNroOperacion;
        }

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the sCodNumeroTicket value.
		/// </summary>
		public string SCodNumeroTicket
		{
			get { return sCodNumeroTicket; }
			set { sCodNumeroTicket = value; }
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
		/// Gets or sets the sDscTicketEstado value.
		/// </summary>
		public string SDscTicketEstado
		{
			get { return sDscTicketEstado; }
			set { sDscTicketEstado = value; }
		}

		/// <summary>
		/// Gets or sets the sCodEquipoMod value.
		/// </summary>
		public string SCodEquipoMod
		{
			get { return sCodEquipoMod; }
			set { sCodEquipoMod = value; }
		}

		/// <summary>
		/// Gets or sets the sDscNumVuelo value.
		/// </summary>
		public string SDscNumVuelo
		{
			get { return sDscNumVuelo; }
			set { sDscNumVuelo = value; }
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
        /// Gets or sets the sLogFechaMod value.
        /// </summary>
        public string SLogFechaMod
        {
            get {return sLogFechaMod;}
            set {sLogFechaMod = value;}
        }

        /// <summary>
        /// Gets or sets the intNum_Secuencial value.
        /// </summary>
        public int IntNum_Secuencial
        {
            get{return intNum_Secuencial;}
            set{intNum_Secuencial = value;}
        }

        /// <summary>
        /// Gets or sets the sCausalRehabilitacion value.
        /// </summary>
        public string SCausalRehabilitacion
        {
            get { return sCausalRehabilitacion; }
            set { sCausalRehabilitacion = value; }
        }

        /// <summary>
        /// Gets or sets the sDscMotivo value.
        /// </summary>
        public string SDscMotivo
        {
            get { return sDscMotivo; }
            set { sDscMotivo = value; }
        }

        /// <summary>
        /// Gets or sets the sNroOperacion value.
        /// </summary>
        public string SNroOperacion
        {
            get { return sNroOperacion; }
            set { sNroOperacion = value; }
        }

		#endregion

       

	}
}
