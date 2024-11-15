using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogOperacion
    {
        #region Fields

        private string sNumOperacion;
        private string sCodTipoOperacion;
        private DateTime dtFchProceso;
        private string sCodTurno;
        private string sCodUsuario;
        private int iNumSecuencial;
        private decimal imp_Mon_Nac;
        private decimal imp_Mon_Int;
        private decimal imp_Tasa_Cambio;
        private string simb_Moneda;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogOperacion class.
        /// </summary>
        public LogOperacion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogOperacion class.
        /// </summary>
        public LogOperacion(string sNumOperacion, string sCodTipoOperacion, DateTime dtFchProceso, string sCodTurno, string sCodUsuario)
        {
            this.sNumOperacion = sNumOperacion;
            this.sCodTipoOperacion = sCodTipoOperacion;
            this.dtFchProceso = dtFchProceso;
            this.sCodTurno = sCodTurno;
            this.sCodUsuario = sCodUsuario;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sNumOperacion value.
        /// </summary>
        public string SNumOperacion
        {
            get { return sNumOperacion; }
            set { sNumOperacion = value; }
        }

        /// <summary>
        /// Gets or sets the sCodTipoOperacion value.
        /// </summary>
        public string SCodTipoOperacion
        {
            get { return sCodTipoOperacion; }
            set { sCodTipoOperacion = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchProceso value.
        /// </summary>
        public DateTime DtFchProceso
        {
            get { return dtFchProceso; }
            set { dtFchProceso = value; }
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
        /// Gets or sets the sCodUsuario value.
        /// </summary>
        public string SCodUsuario
        {
            get { return sCodUsuario; }
            set { sCodUsuario = value; }
        }

        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        #endregion

        public decimal Imp_Mon_Nac
        {
            get
            {
                return imp_Mon_Nac;
            }
            set
            {
                imp_Mon_Nac = value;
            }
        }

        public decimal Imp_Mon_Int
        {
            get
            {
                return imp_Mon_Int;
            }
            set
            {
                imp_Mon_Int = value;
            }
        }

        public decimal Imp_Tasa_Cambio
        {
            get
            {
                return imp_Tasa_Cambio;
            }
            set
            {
                imp_Tasa_Cambio = value;
            }
        }

        public string Simb_Moneda
        {
            get
            {
                return simb_Moneda;
            }
            set
            {
                simb_Moneda = value;
            }
        }

    }
}
