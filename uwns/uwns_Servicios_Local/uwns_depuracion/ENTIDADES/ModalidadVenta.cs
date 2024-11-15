using System;

namespace LAP.TUUA.ENTIDADES
{
    public class ModalidadVenta
    {
        #region Fields

        private string sCodModalidadVenta;
        private string sNomModalidad;
        private string sTipModalidad;
        private string sTipEstado;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ModalidadVenta class.
        /// </summary>
        public ModalidadVenta()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ModalidadVenta class.
        /// </summary>
        public ModalidadVenta(string sCodModalidadVenta, string sNomModalidad, string sTipModalidad, string sTipEstado,string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod)
        {
            this.sCodModalidadVenta = sCodModalidadVenta;
            this.sNomModalidad = sNomModalidad;
            this.sTipModalidad = sTipModalidad;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
            this.sTipEstado = sTipEstado;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the SCodModalidadVenta value.
        /// </summary>
        public string SCodModalidadVenta
        {
            get { return sCodModalidadVenta; }
            set { sCodModalidadVenta = value; }
        }

        /// <summary>
        /// Gets or sets the sNomModalidad value.
        /// </summary>
        public string SNomModalidad
        {
            get { return sNomModalidad; }
            set { sNomModalidad = value; }
        }

        /// <summary>
        /// Gets or sets the sTipModalidad value.
        /// </summary>
        public string STipModalidad
        {
            get { return sTipModalidad; }
            set { sTipModalidad = value; }
        }

        /// <summary>
        /// Gets or sets the sLogUsuarioMod value.
        /// </summary>
        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogFechaMod value.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the sTipEstado value.
        /// </summary>
        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
        }

        #endregion
    }
}
