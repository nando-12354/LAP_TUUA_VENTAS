using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogAcceso
    {
        #region Fields

        private int iNumSecuencial;
        private string sLogModulo;
        private string sLogUsuario;
        private string sLogRol;
        private string sLogProceso;
        private DateTime dtFchEjecucion;
        private string sLogTipo;
        private string sLogEstado;
        private string sHoraEjecucion;
        private string sCodUsuario;
        private string sLogUsuarioWindows;
        private string sLogDominioWindows;
        private string sLogIp;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the LogAcceso class.
        /// </summary>
        public LogAcceso()
        {
        }

        /// <summary>
        /// Initializes a new instance of the LogAcceso class.
        /// </summary>
        public LogAcceso(string sLogModulo, string sLogUsuario, string sLogRol, string sLogProceso, DateTime dtFchEjecucion, string sLogTipo, string sLogEstado, string sHoraEjecucion, string sCodUsuario, string sLogUsuarioWindows, string sLogDominioWindows, string sLogIp)
        {
            this.sLogModulo = sLogModulo;
            this.sLogUsuario = sLogUsuario;
            this.sLogRol = sLogRol;
            this.sLogProceso = sLogProceso;
            this.dtFchEjecucion = dtFchEjecucion;
            this.sLogTipo = sLogTipo;
            this.sLogEstado = sLogEstado;
            this.sHoraEjecucion = sHoraEjecucion;
            this.sCodUsuario = sCodUsuario;
            this.sLogUsuarioWindows = sLogUsuarioWindows;
            this.sLogDominioWindows = sLogDominioWindows;
            this.sLogIp = sLogIp;
        }

        /// <summary>
        /// Initializes a new instance of the LogAcceso class.
        /// </summary>
        public LogAcceso(int iNumSecuencial, string sLogModulo, string sLogUsuario, string sLogRol, string sLogProceso, DateTime dtFchEjecucion, string sLogTipo, string sLogEstado, string sHoraEjecucion, string sCodUsuario, string sLogUsuarioWindows, string sLogDominioWindows, string sLogIp)
        {
            this.iNumSecuencial = iNumSecuencial;
            this.sLogModulo = sLogModulo;
            this.sLogUsuario = sLogUsuario;
            this.sLogRol = sLogRol;
            this.sLogProceso = sLogProceso;
            this.dtFchEjecucion = dtFchEjecucion;
            this.sLogTipo = sLogTipo;
            this.sLogEstado = sLogEstado;
            this.sHoraEjecucion = sHoraEjecucion;
            this.sCodUsuario = sCodUsuario;
            this.sLogUsuarioWindows = sLogUsuarioWindows;
            this.sLogDominioWindows = sLogDominioWindows;
            this.sLogIp = sLogIp;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the iNumSecuencial value.
        /// </summary>
        public int INumSecuencial
        {
            get { return iNumSecuencial; }
            set { iNumSecuencial = value; }
        }

        /// <summary>
        /// Gets or sets the sLogModulo value.
        /// </summary>
        public string SLogModulo
        {
            get { return sLogModulo; }
            set { sLogModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sLogUsuario value.
        /// </summary>
        public string SLogUsuario
        {
            get { return sLogUsuario; }
            set { sLogUsuario = value; }
        }

        /// <summary>
        /// Gets or sets the sLogRol value.
        /// </summary>
        public string SLogRol
        {
            get { return sLogRol; }
            set { sLogRol = value; }
        }

        /// <summary>
        /// Gets or sets the sLogProceso value.
        /// </summary>
        public string SLogProceso
        {
            get { return sLogProceso; }
            set { sLogProceso = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchEjecucion value.
        /// </summary>
        public DateTime DtFchEjecucion
        {
            get { return dtFchEjecucion; }
            set { dtFchEjecucion = value; }
        }

        /// <summary>
        /// Gets or sets the sLogTipo value.
        /// </summary>
        public string SLogTipo
        {
            get { return sLogTipo; }
            set { sLogTipo = value; }
        }

        /// <summary>
        /// Gets or sets the sLogEstado value.
        /// </summary>
        public string SLogEstado
        {
            get { return sLogEstado; }
            set { sLogEstado = value; }
        }

        /// <summary>
        /// Gets or sets the sHoraEjecucion value.
        /// </summary>
        public string SHoraEjecucion
        {
            get { return sHoraEjecucion; }
            set { sHoraEjecucion = value; }
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
        /// Gets or sets the sLogUsuarioWindows value.
        /// </summary>
        public string SLogUsuarioWindows
        {
            get { return sLogUsuarioWindows; }
            set { sLogUsuarioWindows = value; }
        }

        /// <summary>
        /// Gets or sets the sLogDominioWindows value.
        /// </summary>
        public string SLogDominioWindows
        {
            get { return sLogDominioWindows; }
            set { sLogDominioWindows = value; }
        }

        /// <summary>
        /// Gets or sets the sLogIp value.
        /// </summary>
        public string SLogIp
        {
            get { return sLogIp; }
            set { sLogIp = value; }
        }

        #endregion
    }
}
