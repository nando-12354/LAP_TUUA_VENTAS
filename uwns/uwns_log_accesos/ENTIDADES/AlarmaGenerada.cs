using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class AlarmaGenerada
    {
        #region Fields

		private int iCodAlarmaGenerada;
		private string sCodAlarma;
		private string sCodModulo;
        private string sDscEquipo;
		private DateTime dtFchGeneracion;
		private DateTime dtFchActualizacion;
		private string sTipEstado;
		private string sTipImportancia;
		private string sFlgEstadoMail;
		private string sDscSubject;
		private string sDscBody;
        private string sDscAtencion;
        private string sLogUsuarioMod;


		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the TUA_AlarmaGenerada class.
		/// </summary>
		public AlarmaGenerada()
		{
		}

		/// <summary>
		/// Initializes a new instance of the TUA_AlarmaGenerada class.
		/// </summary>
        public AlarmaGenerada(int iCodAlarmaGenerada, string sCodAlarma, string sCodModulo, DateTime dtFchGeneracion, DateTime dtFchActualizacion, string sTipEstado, string sTipImportancia, string sFlgEstadoMail, string sDscSubject, string sDscBody, string sLogUsuarioMod)
		{
			this.iCodAlarmaGenerada = iCodAlarmaGenerada;
			this.sCodAlarma = sCodAlarma;
			this.sCodModulo = sCodModulo;
			this.dtFchGeneracion = dtFchGeneracion;
			this.dtFchActualizacion = dtFchActualizacion;
			this.sTipEstado = sTipEstado;
			this.sTipImportancia = sTipImportancia;
			this.sFlgEstadoMail = sFlgEstadoMail;
			this.sDscSubject = sDscSubject;
			this.sDscBody = sDscBody;
            this.sLogUsuarioMod = sLogUsuarioMod;
		}

		#endregion

		#region Properties
		/// <summary>
        /// Gets or sets the iCodAlarmaGenerada value.
		/// </summary>
		public int ICodAlarmaGenerada
		{
			get { return iCodAlarmaGenerada; }
			set { iCodAlarmaGenerada = value; }
		}

		/// <summary>
        /// Gets or sets the sCodAlarma value.
		/// </summary>
		public  string SCodAlarma
		{
			get { return sCodAlarma; }
			set { sCodAlarma = value; }
		}

		/// <summary>
        /// Gets or sets the sCodModulo value.
		/// </summary>
		public  string SCodModulo
		{
			get { return sCodModulo; }
			set { sCodModulo = value; }
		}

        /// <summary>
        /// Gets or sets the sDscEquipo value.
        /// </summary>
        public string SDscEquipo
        {
            get { return sDscEquipo; }
            set { sDscEquipo = value; }
        }

		/// <summary>
        /// Gets or sets the dtFchGeneracion value.
		/// </summary>
		public  DateTime DtFchGeneracion
		{
			get { return dtFchGeneracion; }
			set { dtFchGeneracion = value; }
		}

		/// <summary>
        /// Gets or sets the dtFchActualizacion value.
		/// </summary>
		public  DateTime DtFchActualizacion
		{
			get { return dtFchActualizacion; }
			set { dtFchActualizacion = value; }
		}

		/// <summary>
        /// Gets or sets the sTipEstado value.
		/// </summary>
		public  string STipEstado
		{
			get { return sTipEstado; }
			set { sTipEstado = value; }
		}

		/// <summary>
        /// Gets or sets the sTipImportancia value.
		/// </summary>
		public  string STipImportancia
		{
			get { return sTipImportancia; }
			set { sTipImportancia = value; }
		}

		/// <summary>
        /// Gets or sets the sFlgEstadoMail value.
		/// </summary>
		public  string SFlgEstadoMail
		{
			get { return sFlgEstadoMail; }
			set { sFlgEstadoMail = value; }
		}

		/// <summary>
        /// Gets or sets the sDscSubject value.
		/// </summary>
		public  string SDscSubject
		{
			get { return sDscSubject; }
			set { sDscSubject = value; }
		}

		/// <summary>
        /// Gets or sets the sDscBody value.
		/// </summary>
		public  string SDscBody
		{
			get { return sDscBody; }
			set { sDscBody = value; }
		}


        /// <summary>
        /// Gets or sets the sDscAtencion value.
        /// </summary>
        public string SDscAtencion
        {
            get { return sDscAtencion; }
            set { sDscAtencion = value; }
        }

        /// Gets or sets the sLogUsuarioMod value.
        /// </summary>
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

		#endregion
	}
    
}
