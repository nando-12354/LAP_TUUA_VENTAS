using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Modulo
    {
        #region Fields

        private string sCodModulo;
        private string sDscModulo;
        private string sIdModulo;
        private string sDscIcono;
        private string sDscIcono_Out;
        private string sTipModulo;
        private string sDscArchivo;
        private string sDscTextoAyuda;
        private int iNumPosicion;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Modulo class.
        /// </summary>
        public Modulo()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Modulo class.
        /// </summary>
        public Modulo(string sCodModulo, string sDscModulo, string sIdModulo, string sDscIcono, string sDscIcono_Out, string sTipModulo, string sDscArchivo, string sDscTextoAyuda, int iNumPosicion, string sLogUsuarioMod, DateTime dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodModulo = sCodModulo;
            this.sDscModulo = sDscModulo;
            this.sIdModulo = sIdModulo;
            this.sDscIcono = sDscIcono;
            this.sDscIcono_Out = sDscIcono_Out;
            this.sTipModulo = sTipModulo;
            this.sDscArchivo = sDscArchivo;
            this.sDscTextoAyuda = sDscTextoAyuda;
            this.iNumPosicion = iNumPosicion;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodModulo value.
        /// </summary>
        public string SCodModulo
        {
            get { return sCodModulo; }
            set { sCodModulo = value; }
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
        /// Gets or sets the sIdModulo value.
        /// </summary>
        public string SIdModulo
        {
            get { return sIdModulo; }
            set { sIdModulo = value; }
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
        /// Gets or sets the sDscIcono_Out value.
        /// </summary>
        public string SDscIcono_Out
        {
            get { return sDscIcono_Out; }
            set { sDscIcono_Out = value; }
        }

        /// <summary>
        /// Gets or sets the sTipModulo value.
        /// </summary>
        public string STipModulo
        {
            get { return sTipModulo; }
            set { sTipModulo = value; }
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
        /// Gets or sets the sDscTextoAyuda value.
        /// </summary>
        public string SDscTextoAyuda
        {
            get { return sDscTextoAyuda; }
            set { sDscTextoAyuda = value; }
        }

        /// <summary>
        /// Gets or sets the iNumPosicion value.
        /// </summary>
        public int INumPosicion
        {
            get { return iNumPosicion; }
            set { iNumPosicion = value; }
        }
 
        #endregion
    }
}
