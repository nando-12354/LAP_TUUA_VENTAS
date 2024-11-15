using System;

namespace LAP.TUUA.ENTIDADES
{
	public class Ticket
	{
		#region Fields

		private string sCodNumeroTicket;
		private string sCodCompania;
		private string sCodVentaMasiva;
		private string sDscNumVuelo;
		private string dtFchVuelo;
		private string sTipEstadoActual;
		private string sFchCreacion;
		private string sCodTurno;
		private string sCodUsuarioVenta;
		private decimal dImpPrecio;
		private string sCodMoneda;
		private string dtFchVencimiento;
		private string sCodModalidadVenta;
		private int iNumRehabilitaciones;
		private string sCodTipoTicket;
        private string tip_Vuelo;
        private string flg_Contingencia;
        private string num_Referencia;
        private int cant_Ticket;
        private string num_TicketsGenerados;
        private string sHor_Creacion;
        private string tip_Anula;
        private string cod_Pto_Venta;
        private string dsc_Motivo;

        private string sLog_Fecha_Mod;

        private string sLog_Hora_Mod;

        private string sLog_Usuario_Mod;

        private string sMetPago;

		private string sEmpresaRecaudadora;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        public Ticket()
		{
		}

		/// <summary>
		/// Initializes a new instance of the Ticket class.
		/// </summary>
		public Ticket(string sCodNumeroTicket, string sCodCompania, string sCodVentaMasiva, string sDscNumVuelo, string dtFchVuelo, string sTipEstadoActual, string dtFchCreacion, string sCodTurno, string sCodUsuarioVenta, decimal dImpPrecio, string sCodMoneda, string dtFchVencimiento, string sCodModalidadVenta, int iNumRehabilitaciones, string sCodTipoTicket, string sMetPago, string sEmpresaRecaudadora) //FL.
		{
			this.sCodNumeroTicket = sCodNumeroTicket;
			this.sCodCompania = sCodCompania;
			this.sCodVentaMasiva = sCodVentaMasiva;
			this.sDscNumVuelo = sDscNumVuelo;
			this.dtFchVuelo = dtFchVuelo;
			this.sTipEstadoActual = sTipEstadoActual;
			this.sFchCreacion = dtFchCreacion;
			this.sCodTurno = sCodTurno;
			this.sCodUsuarioVenta = sCodUsuarioVenta;
			this.dImpPrecio = dImpPrecio;
			this.sCodMoneda = sCodMoneda;
			this.dtFchVencimiento = dtFchVencimiento;
			this.sCodModalidadVenta = sCodModalidadVenta;
			this.iNumRehabilitaciones = iNumRehabilitaciones;
			this.sCodTipoTicket = sCodTipoTicket;
			this.sMetPago = sMetPago;
			this.sEmpresaRecaudadora = sEmpresaRecaudadora;
		}

		#endregion

		#region Properties

        public string SLog_Usuario_Mod
        {
              get { return sLog_Usuario_Mod; }
              set { sLog_Usuario_Mod = value; }
        }

        public string SLog_Hora_Mod
        {
              get { return sLog_Hora_Mod; }
              set { sLog_Hora_Mod = value; }
        }

        public string SLog_Fecha_Mod
        {
              get { return sLog_Fecha_Mod; }
              set { sLog_Fecha_Mod = value; }
        }

        public string SHor_Creacion
        {
              get { return sHor_Creacion; }
              set { sHor_Creacion = value; }
        }

		/// <summary>
		/// Gets or sets the sCodNumeroTicket value.
		/// </summary>
		public string SCodNumeroTicket
		{
			get { return sCodNumeroTicket; }
			set { sCodNumeroTicket = value; }
		}

		/// <summary>
		/// Gets or sets the sCodCompania value.
		/// </summary>
		public string SCodCompania
		{
			get { return sCodCompania; }
			set { sCodCompania = value; }
		}

		/// <summary>
		/// Gets or sets the sCodVentaMasiva value.
		/// </summary>
		public string SCodVentaMasiva
		{
			get { return sCodVentaMasiva; }
			set { sCodVentaMasiva = value; }
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
		/// Gets or sets the dtFchVuelo value.
		/// </summary>
		public string DtFchVuelo
		{
			get { return dtFchVuelo; }
			set { dtFchVuelo = value; }
		}

		/// <summary>
		/// Gets or sets the sTipEstadoActual value.
		/// </summary>
		public string STipEstadoActual
		{
			get { return sTipEstadoActual; }
			set { sTipEstadoActual = value; }
		}

		/// <summary>
		/// Gets or sets the dtFchCreacion value.
		/// </summary>
		public string DtFchCreacion
		{
			get { return sFchCreacion; }
			set { sFchCreacion = value; }
		}

		/// <summary>
		/// Gets or sets the sCodTurno value.
		/// </summary>
		public string SCodTurno
		{
			get { return sCodTurno; }
			set { sCodTurno = value; }
		}

		/// <summary>
		/// Gets or sets the sCodUsuarioVenta value.
		/// </summary>
		public string SCodUsuarioVenta
		{
			get { return sCodUsuarioVenta; }
			set { sCodUsuarioVenta = value; }
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
		/// Gets or sets the sCodMoneda value.
		/// </summary>
		public string SCodMoneda
		{
			get { return sCodMoneda; }
			set { sCodMoneda = value; }
		}

		/// <summary>
		/// Gets or sets the dtFchVencimiento value.
		/// </summary>
		public string DtFchVencimiento
		{
			get { return dtFchVencimiento; }
			set { dtFchVencimiento = value; }
		}

		/// <summary>
		/// Gets or sets the sCodModalidadVenta value.
		/// </summary>
		public string SCodModalidadVenta
		{
			get { return sCodModalidadVenta; }
			set { sCodModalidadVenta = value; }
		}

		/// <summary>
		/// Gets or sets the iNumRehabilitaciones value.
		/// </summary>
		public int INumRehabilitaciones
		{
			get { return iNumRehabilitaciones; }
			set { iNumRehabilitaciones = value; }
		}

		/// <summary>
		/// Gets or sets the sCodTipoTicket value.
		/// </summary>
		public string SCodTipoTicket
		{
			get { return sCodTipoTicket; }
			set { sCodTipoTicket = value; }
		}

		#endregion

        public string Tip_Vuelo
        {
            get
            {
                return tip_Vuelo;
            }
            set
            {
                tip_Vuelo = value;
            }
        }

        public string Flg_Contingencia
        {
            get
            {
                return flg_Contingencia;
            }
            set
            {
                flg_Contingencia = value;
            }
        }

        public string Num_Referencia
        {
            get
            {
                return num_Referencia;
            }
            set
            {
                num_Referencia = value;
            }
        }

        public int Cant_Ticket
        {
            get
            {
                return cant_Ticket;
            }
            set
            {
                cant_Ticket = value;
            }
        }

        public string Num_TicketsGenerados
        {
            get
            {
                return num_TicketsGenerados;
            }
            set
            {
                num_TicketsGenerados = value;
            }
        }

        public string Tip_Anula
        {
            get
            {
                return tip_Anula;
            }
            set
            {
                tip_Anula = value;
            }
        }

        public string Cod_Pto_Venta
        {
            get
            {
                return cod_Pto_Venta;
            }
            set
            {
                cod_Pto_Venta = value;
            }
        }

        public string Dsc_Motivo
        {
            get
            {
                return dsc_Motivo;
            }
            set
            {
                dsc_Motivo = value;
            }
        }
        public string SMetPago
        {
            get { return sMetPago; }
            set { sMetPago = value; }
        }
        public string SEmpresaRecaudadora
        {
            get { return sEmpresaRecaudadora; }
            set { sEmpresaRecaudadora = value; }
        }
    }
}
