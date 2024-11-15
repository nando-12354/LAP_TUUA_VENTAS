using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ParameGeneral
    {
        #region Fields

        private string sIdentificador;
        private string sDescripcion;
        private string sTipoParametro;
        private string sTipoParametroDet;
        private string sTipoValor;
        private string sValor;
        private string sCampoLista;
        private string sIdentificadorPadre;
        private string sLog_Usuario_Mod;
        private string sLog_Fecha_Mod;
        private string sLog_Hora_Mod;
        private bool bFlag;
      

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ParameGeneral class.
        /// </summary>
        public ParameGeneral()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ParameGeneral class.
        /// </summary>
        public ParameGeneral(string sIdentificador, string sDescripcion, string sTipoParametro, string sTipoParametroDet, string sTipoValor, string sValor, string sCampoLista, string sIdentificadorPadre, string sLog_Usuario_Mod, string sLog_Fecha_Mod, string sLog_Hora_Mod)
        {
           this.sIdentificador = sIdentificador;
           this.sDescripcion=sDescripcion;
           this.sTipoParametro=sTipoParametro;
           this.sTipoParametroDet = sTipoParametroDet;
           this.sTipoValor = sTipoValor;
           this.sValor = sValor;
           this.sCampoLista = sCampoLista;
           this.sIdentificadorPadre = sIdentificadorPadre;
           this.sLog_Usuario_Mod = sLog_Usuario_Mod;
           this.sLog_Fecha_Mod = sLog_Fecha_Mod;
           this.sLog_Hora_Mod = sLog_Hora_Mod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sIdentificador value.
        /// </summary>
        public string SIdentificador
        {
            get { return sIdentificador; }
            set { sIdentificador = value; }
        }

        /// <summary>
        /// Gets or sets the sDescripcion value.
        /// </summary>
        public string SDescripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
        }

        /// <summary>
        /// Gets or sets the sTipoParametro value.
        /// </summary>
        public string STipoParametro
        {
            get { return sTipoParametro; }
            set { sTipoParametro = value; }
        }

        /// <summary>
        /// Gets or sets the sTipoParametroDet value.
        /// </summary>
        public string STipoParametroDet
        {
            get { return sTipoParametroDet; }
            set { sTipoParametroDet = value; }
        }

        /// <summary>
        /// Gets or sets the sTipoValor value.
        /// </summary>
        public string STipoValor
        {
            get { return sTipoValor; }
            set { sTipoValor = value; }
        }

        /// <summary>
        /// Gets or sets the sValor value.
        /// </summary>
        public string SValor
        {
            get { return sValor; }
            set { sValor = value; }
        }

        /// <summary>
        /// Gets or sets the sCampoLista value.
        /// </summary>
        public string SCampoLista
        {
            get { return sCampoLista; }
            set { sCampoLista = value; }
        }

        /// <summary>
        /// Gets or sets the sIdentificadorPadre value.
        /// </summary>
        public string SIdentificadorPadre
        {
            get { return sIdentificadorPadre; }
            set { sIdentificadorPadre = value; }
        }

        /// <summary>
        /// Gets or sets the sLog_Usuario_Mod value.
        /// </summary>
        public string SLog_Usuario_Mod
        {
            get { return sLog_Usuario_Mod; }
            set { sLog_Usuario_Mod = value; }
        }

        /// <summary>
        /// Gets or sets the sLog_Fecha_Mod value.
        /// </summary>
        public string SLog_Fecha_Mod
        {
            get { return sLog_Fecha_Mod; }
            set { sLog_Fecha_Mod = value; }
        }

        /// <summary>
        /// Gets or sets the sLog_Hora_Mod value.
        /// </summary>
        public string SLog_Hora_Mod
        {
            get { return sLog_Hora_Mod; }
            set { sLog_Hora_Mod = value; }
        }

        /// <summary>
        /// Gets or sets the bFlag value.
        /// </summary>
        public bool BFlag
        {
            get { return bFlag; }
            set { bFlag = value; }
        }
        #endregion
    }
}
