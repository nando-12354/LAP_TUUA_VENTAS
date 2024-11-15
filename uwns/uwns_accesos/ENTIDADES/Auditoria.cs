using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Auditoria
    {
        #region Fields
		//prueba svn
        private int iCodContador;
        private string sCodModulo;
        private string sCodSubModulo;
        private string sCodUsuario;
        private DateTime dtFchRegistro;
        private string sCodRol;
        private string sTipOperacion;
        private string sLogRegOrig;
        private string sLogRegNuevo;
        private string sLogTablaMod;
        private string sLogColumMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Auditoria class.
        /// </summary>
        public Auditoria()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Auditoria class.
        /// </summary>
        public Auditoria(string sCodModulo, string sCodSubModulo, string sCodUsuario, DateTime dtFchRegistro, string sCodRol, string sTipOperacion, string sLogRegOrig, string sLogRegNuevo, string sLogTablaMod, string sLogColumMod, string sLogHoraMod)
        {
            this.sCodModulo = sCodModulo;
            this.sCodSubModulo = sCodSubModulo;
            this.sCodUsuario = sCodUsuario;
            this.dtFchRegistro = dtFchRegistro;
            this.sCodRol = sCodRol;
            this.sTipOperacion = sTipOperacion;
            this.sLogRegOrig = sLogRegOrig;
            this.sLogRegNuevo = sLogRegNuevo;
            this.sLogTablaMod = sLogTablaMod;
            this.sLogColumMod = sLogColumMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        /// <summary>
        /// Initializes a new instance of the Auditoria class.
        /// </summary>
        public Auditoria(int cod_Contador, string sCodModulo, string sCodSubModulo, string sCodUsuario, DateTime dtFchRegistro, string sCodRol, string sTipOperacion, string sLogRegOrig, string sLogRegNuevo, string sLogTablaMod, string sLogColumMod, string sLogHoraMod)
        {
            this.iCodContador = cod_Contador;
            this.sCodModulo = sCodModulo;
            this.sCodSubModulo = sCodSubModulo;
            this.sCodUsuario = sCodUsuario;
            this.dtFchRegistro = dtFchRegistro;
            this.sCodRol = sCodRol;
            this.sTipOperacion = sTipOperacion;
            this.sLogRegOrig = sLogRegOrig;
            this.sLogRegNuevo = sLogRegNuevo;
            this.sLogTablaMod = sLogTablaMod;
            this.sLogColumMod = sLogColumMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the iCodContador value.
        /// </summary>
        public int ICodContador
        {
            get { return iCodContador; }
            set { iCodContador = value; }
        }

        /// <summary>
        /// Gets or sets the iCodModulo value.
        /// </summary>
        public string SCodModulo
        {
            get { return sCodModulo; }
            set { sCodModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sCodSubModulo value.
        /// </summary>
        public string SCodSubModulo
        {
            get { return sCodSubModulo; }
            set { sCodSubModulo = value; }
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
        /// Gets or sets the dtFchRegistro value.
        /// </summary>
        public DateTime DtFchRegistro
        {
            get { return dtFchRegistro; }
            set { dtFchRegistro = value; }
        }

        /// <summary>
        /// Gets or sets the sCodRol value.
        /// </summary>
        public string SCodRol
        {
            get { return sCodRol; }
            set { sCodRol = value; }
        }

        /// <summary>
        /// Gets or sets the sTipOperacion value.
        /// </summary>
        public string STipOperacion
        {
            get { return sTipOperacion; }
            set { sTipOperacion = value; }
        }

        /// <summary>
        /// Gets or sets the sLogRegOrig value.
        /// </summary>
        public string SLogRegOrig
        {
            get { return sLogRegOrig; }
            set { sLogRegOrig = value; }
        }

        /// <summary>
        /// Gets or sets the sLogRegNuevo value.
        /// </summary>
        public string SLogRegNuevo
        {
            get { return sLogRegNuevo; }
            set { sLogRegNuevo = value; }
        }

        /// <summary>
        /// Gets or sets the sLogTablaMod value.
        /// </summary>
        public string SLogTablaMod
        {
            get { return sLogTablaMod; }
            set { sLogTablaMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogColumMod value.
        /// </summary>
        public string SLogColumMod
        {
            get { return sLogColumMod; }
            set { sLogColumMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogHoraMod value.
        /// </summary>
        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
        }

        #endregion
    }
}
