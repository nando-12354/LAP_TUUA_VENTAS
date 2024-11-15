using System;

namespace LAP.TUUA.ENTIDADES
{
    public class EstacionPtoVta
    {
        #region Fields

        private string sCodEquipo;
        private string sNumIpEquipo;
        private string sFlgEstado;
        private string sCodUsuarioCreacion;
        private string sLogUsuarioMod;
        private string dtLogFechaMod;
        private string sLogHoraMod;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the EstacionPtoVta class.
        /// </summary>
        public EstacionPtoVta()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EstacionPtoVta class.
        /// </summary>
        public EstacionPtoVta(string sCodEquipo, string sNumIpEquipo, string sFlgEstado, string sCodUsuarioCreacion, string sLogUsuarioMod, string dtLogFechaMod, string sLogHoraMod)
        {
            this.sCodEquipo = sCodEquipo;
            this.sNumIpEquipo = sNumIpEquipo;
            this.sFlgEstado = sFlgEstado;
            this.sCodUsuarioCreacion = sCodUsuarioCreacion;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodEquipo value.
        /// </summary>
        public string SCodEquipo
        {
            get { return sCodEquipo; }
            set { sCodEquipo = value; }
        }

        #endregion

        public string SNumIpEquipo
        {
            get
            {
                return sNumIpEquipo;
            }
            set
            {
                sNumIpEquipo = value;
            }
        }

        public string SFlgEstado
        {
            get
            {
                return sFlgEstado;
            }
            set
            {
                sFlgEstado = value;
            }
        }

        public string SCodUsuarioCreacion
        {
            get
            {
                return sCodUsuarioCreacion;
            }
            set
            {
                sCodUsuarioCreacion = value;
            }
        }

        public string SLogUsuarioMod
        {
            get
            {
                return sLogUsuarioMod;
            }
            set
            {
                sLogUsuarioMod = value;
            }
        }

        public string DtLogFechaMod
        {
            get
            {
                return dtLogFechaMod;
            }
            set
            {
                dtLogFechaMod = value;
            }
        }

        public string SLogHoraMod
        {
            get
            {
                return sLogHoraMod;
            }
            set
            {
                sLogHoraMod = value;
            }
        }

    }
}
