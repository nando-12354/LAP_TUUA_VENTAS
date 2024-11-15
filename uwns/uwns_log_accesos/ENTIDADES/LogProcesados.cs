using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogProcesados
    {
        #region Fields

        private string strCodEquipoMod;
        private string strNomArchivo;
        private string strLogFechaMod;
        private string strLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BoardingBcbpErr class.
        /// </summary>
        public LogProcesados()
        {
        }

        #endregion

        #region Propiedades
        public string StrCodEquipoMod
        {
            get { return strCodEquipoMod; }
            set { strCodEquipoMod = value; }
        }
        public string StrNomArchivo
        {
            get { return strNomArchivo; }
            set { strNomArchivo = value; }
        }
        public string StrLogFechaMod
        {
            get { return strLogFechaMod; }
            set { strLogFechaMod = value; }
        }
        public string StrLogHoraMod
        {
            get { return strLogHoraMod; }
            set { strLogHoraMod = value; }
        }
        #endregion
    }
}
