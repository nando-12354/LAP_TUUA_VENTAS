using System;

namespace LAP.TUUA.ENTIDADES
{
    public class Turno
    {
        #region Fields

        private string sCodTurno;
        private string dtFchInicio;
        private string sHoraInicio;
        private string dtFchFin;
        private string sHoraFin;
        private string sCodUsuarioCierre;
        private string sCodUsuario;
        private string sCodEquipo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Turno class.
        /// </summary>
        public Turno()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Turno class.
        /// </summary>
        public Turno(string sCodTurno, string dtFchInicio, string dtFchFin, string sCodUsuarioCierre, string sCodUsuario, string sCodEquipo, string sHoraInicio, string sHoraFin)
        {
            this.sCodTurno = sCodTurno;
            this.dtFchInicio = dtFchInicio;
            this.dtFchFin = dtFchFin;
            this.sCodUsuarioCierre = sCodUsuarioCierre;
            this.sCodUsuario = sCodUsuario;
            this.sCodEquipo = sCodEquipo;
            this.sHoraInicio = sHoraInicio;
            this.sHoraFin = sHoraFin;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the sCodTurno value.
        /// </summary>
        public string SCodTurno
        {
            get { return sCodTurno; }
            set { sCodTurno = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchInicio value.
        /// </summary>
        public string DtFchInicio
        {
            get { return dtFchInicio; }
            set { dtFchInicio = value; }
        }

        /// <summary>
        /// Gets or sets the dtFchFin value.
        /// </summary>
        public string DtFchFin
        {
            get { return dtFchFin; }
            set { dtFchFin = value; }
        }

        /// <summary>
        /// Gets or sets the sCodUsuarioCierre value.
        /// </summary>
        public string SCodUsuarioCierre
        {
            get { return sCodUsuarioCierre; }
            set { sCodUsuarioCierre = value; }
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
        /// Gets or sets the sCodEquipo value.
        /// </summary>
        public string SCodEquipo
        {
            get { return sCodEquipo; }
            set { sCodEquipo = value; }
        }

        #endregion

        public string SHoraInicio
        {
            get
            {
                return sHoraInicio;
            }
            set
            {
                sHoraInicio = value;
            }
        }

        public string SHoraFin
        {
            get
            {
                return sHoraFin;
            }
            set
            {
                sHoraFin = value;
            }
        }

    }
}
