using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ArbolModulo
    {
        #region Fields

        private string sCodProceso;
        private string sCodProcesoPadre;
        private string sCodModulo;
        private string sIdProceso;
        private string sDscProceso;
        private int iTipNivel;
        private string sTipEstado;
        private string sFlgPermitido;
        private string sDscArchivo;
        private string sDscIcono;
        private string sDscTextoAyuda;
        private string sDscIndCritic;
        private string sDscColorCritic;
        private string sDscTabFiltro;
        private string sDscLicencia;
        private int iNumPosModulo;
        private int iNumPosProceso;
        private string sDscModulo;
        private string sNomRol;
        private string sTipModulo;

        

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ArbolModulo class.
        /// </summary>
        public ArbolModulo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ArbolModulo class.
        /// </summary>
        public ArbolModulo(string sCodProceso, string sCodProcesoPadre, string sCodModulo, string sIdProceso, string sDscProceso, int iTipNivel, string sTipEstado, string sFlgPermitido, string sDscArchivo, string sDscIcono, string sDscTextoAyuda, string sDscIndCritic, string sDscColorCritic, string sDscTabFiltro, string sDscLicencia, int iNumPosProceso, string sDscModulo, string sNomRol, int iNumPosModulo,string sTipModulo)
        {
            this.sCodProceso = sCodProceso;
            this.sCodProcesoPadre = sCodProcesoPadre;
            this.sCodModulo = sCodModulo;
            this.sIdProceso = sIdProceso;
            this.sDscProceso = sDscProceso;
            this.iTipNivel = iTipNivel;
            this.sTipEstado = sTipEstado;
            this.sFlgPermitido = sFlgPermitido;
            this.sDscArchivo = sDscArchivo;
            this.sDscIcono = sDscIcono;
            this.sDscTextoAyuda = sDscTextoAyuda;
            this.sDscIndCritic = sDscIndCritic;
            this.sDscColorCritic = sDscColorCritic;
            this.sDscTabFiltro = sDscTabFiltro;
            this.sDscLicencia = sDscLicencia;
            this.iNumPosProceso = iNumPosProceso;
            this.iNumPosModulo = iNumPosModulo;
            this.sDscModulo = sDscModulo;
            this.sNomRol = sNomRol;
            this.sTipModulo = sTipModulo;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodProceso value.
        /// </summary>
        public string SCodProceso
        {
            get { return sCodProceso; }
            set { sCodProceso = value; }
        }

        /// <summary>
        /// Gets or sets the sCodProcesoPadre value.
        /// </summary>
        public string SCodProcesoPadre
        {
            get { return sCodProcesoPadre; }
            set { sCodProcesoPadre = value; }
        }

        /// <summary>
        /// Gets or sets the sCodModulo value.
        /// </summary>
        public string SCodModulo
        {
            get { return sCodModulo; }
            set { sCodModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sIdProceso value.
        /// </summary>
        public string SIdProceso
        {
            get { return sIdProceso; }
            set { sIdProceso = value; }
        }

        /// <summary>
        /// Gets or sets the sDscProceso value.
        /// </summary>
        public string SDscProceso
        {
            get { return sDscProceso; }
            set { sDscProceso = value; }
        }

        /// <summary>
        /// Gets or sets the sTipNivel value.
        /// </summary>
        public int ITipNivel
        {
            get { return iTipNivel; }
            set { iTipNivel = value; }
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
        /// Gets or sets the sFlgPermitido value.
        /// </summary>
        public string SFlgPermitido
        {
            get { return sFlgPermitido; }
            set { sFlgPermitido = value; }
        }

        /// <summary>
        /// Gets or sets the sDscArchivo value.
        /// </summary>
        public string SDscArchivo
        {
            get { return sDscArchivo; }
            set { sDscArchivo = value; }
        }

        /// <summary>
        /// Gets or sets the sDscIcono value.
        /// </summary>
        public string SDscIcono
        {
            get { return sDscIcono; }
            set { sDscIcono = value; }
        }

        /// <summary>
        /// Gets or sets the sDscTextoAyuda value.
        /// </summary>
        public string SDscTextoAyuda
        {
            get { return sDscTextoAyuda; }
            set { sDscTextoAyuda = value; }
        }

        /// <summary>
        /// Gets or sets the sDscIndCritic value.
        /// </summary>
        public string SDscIndCritic
        {
            get { return sDscIndCritic; }
            set { sDscIndCritic = value; }
        }

        /// <summary>
        /// Gets or sets the sDscColorCritic value.
        /// </summary>
        public string SDscColorCritic
        {
            get { return sDscColorCritic; }
            set { sDscColorCritic = value; }
        }

        /// <summary>
        /// Gets or sets the sDscTabFiltro value.
        /// </summary>
        public string SDscTabFiltro
        {
            get { return sDscTabFiltro; }
            set { sDscTabFiltro = value; }
        }

        /// <summary>
        /// Gets or sets the sDscLicencia value.
        /// </summary>
        public string SDscLicencia
        {
            get { return sDscLicencia; }
            set { sDscLicencia = value; }
        }
              

        /// <summary>
        /// Gets or sets the sDscModulo value.
        /// </summary>

        public string SDscModulo
        {
            get { return sDscModulo; }
            set { sDscModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sNomRol value.
        /// </summary>
        public string SNomRol
        {
            get { return sNomRol; }
            set { sNomRol = value; }
        }

        /// <summary>
        /// Gets or sets the iNumPosProceso value.
        /// </summary>
        public int INumPosProceso
        {
            get { return iNumPosProceso; }
            set { iNumPosProceso = value; }
        }

        /// <summary>
        /// Gets or sets the iNumPosModulo value.
        /// </summary>
        public int INumPosModulo
        {
            get { return iNumPosModulo; }
            set { iNumPosModulo = value; }
        }

        /// <summary>
        /// Gets or sets the sTipModulo value.
        /// </summary>
        public string STipModulo
        {
            get { return sTipModulo; }
            set { sTipModulo = value; }
        }
        #endregion
    }
}
