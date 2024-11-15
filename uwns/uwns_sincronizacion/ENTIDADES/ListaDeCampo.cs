using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ListaDeCampo
    {
        #region Fields

        private string sNomCampo;
        private string sCodCampo;
        private string sCodRelativo;
        private string sDscCampo;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ListaDeCampo class.
        /// </summary>
        public ListaDeCampo()
        {
        }


        /// <summary>
        /// Initializes a new instance of the ListaDeCampo class.
        /// </summary>
        public ListaDeCampo(string sNomCampo, string sCodCampo, string sCodRelativo, string sDscCampo,string sLogUsuarioMod,string sLogFechaMod,string sLogHoraMod)
        {
            this.sNomCampo = sNomCampo;
            this.sCodCampo = sCodCampo;
            this.sCodRelativo = sCodRelativo;
            this.sDscCampo = sDscCampo;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sNomCampo value.
        /// </summary>
        public string SNomCampo
        {
            get { return sNomCampo; }
            set { sNomCampo = value; }
        }

        /// <summary>
        /// Gets or sets the sCodCampo value.
        /// </summary>
        public string SCodCampo
        {
            get { return sCodCampo; }
            set { sCodCampo = value; }
        }

        /// <summary>
        /// Gets or sets the sCodRelativo value.
        /// </summary>
        public string SCodRelativo
        {
            get { return sCodRelativo; }
            set { sCodRelativo = value; }
        }

        /// <summary>
        /// Gets or sets the sDscCampo value.
        /// </summary>
        public string SDscCampo
        {
            get { return sDscCampo; }
            set { sDscCampo = value; }
        }

        /// <summary>
        /// Gets or sets the sLogUsuarioMod value.
        /// </summary>
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

        ///// <summary>
        ///// Gets or sets the sLogFechaMod value.
        ///// </summary>
        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
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
