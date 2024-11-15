using System;
using System.Collections.Generic;

namespace LAP.TUUA.ENTIDADES
{
    public class Usuario
    {
        #region Fields

        private string sCodUsuario;
        private string sNomUsuario;
        private string sApeUsuario;
        private string sCtaUsuario;
        private string sPwdActualUsuario;
        private string sFlgCambioClave;
        private string sTipoEstadoActual;
        private string sDscEstadoActual;
        private DateTime dtFchVigencia;
        private string sTipoGrupo;
        private string sDscGrupo;
        private string sFchCreacion;
        private string sHoraCreacion;
        private string sCodUsuarioCreacion;
        private string sLogUsuarioMod;
        private string sLogFechaMod;
        private string sLogHoraMod;
        private string sFlgUsuarioPinPad;
        private string sPwdUsuarioPinpad;
        private string bGenerarCodeBarDestrabe;
        private string sCodeBarDestrabe;
        private string bGenerarCodeBarMolinete;
        private string sCodeBarMolinete;
        private string sEmp_Recaudadora;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Usuario class.
        /// </summary>
        public Usuario()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Usuario class.
        /// </summary>
        public Usuario(string sCodUsuario, string sNomUsuario, string sApeUsuario, string sCtaUsuario, string sPwdActualUsuario, string sFlgCambioClave, string sTipoEstadoActual, DateTime dtFchVigencia, string sTipoGrupo, string sFchCreacion, string sHoraCreacion, string sCodUsuarioCreacion, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod, string sEmpRecaudadora)
        {
            this.sCodUsuario = sCodUsuario;
            this.sNomUsuario = sNomUsuario;
            this.sApeUsuario = sApeUsuario;
            this.sCtaUsuario = sCtaUsuario;
            this.sPwdActualUsuario = sPwdActualUsuario;
            this.sFlgCambioClave = sFlgCambioClave;
            this.sTipoEstadoActual = sTipoEstadoActual;
            this.dtFchVigencia = dtFchVigencia;
            this.sTipoGrupo = sTipoGrupo;
            this.sFchCreacion = sFchCreacion;
            this.sHoraCreacion = sHoraCreacion;
            this.sCodUsuarioCreacion = sCodUsuarioCreacion;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
            this.Emp_Recaudadora = sEmpRecaudadora;
        }

        public Usuario(string sCodUsuario, string sNomUsuario, string sApeUsuario, string sCtaUsuario, 
            string sPwdActualUsuario, string sFlgCambioClave, string sTipoEstadoActual, 
            DateTime dtFchVigencia, string sTipoGrupo, string sFchCreacion, string sHoraCreacion, 
            string sCodUsuarioCreacion, string sLogUsuarioMod, string sLogFechaMod, string sLogHoraMod,
            string bGenerarCodeBarDestrabe, string sCodeBarDestrabe, string bGenerarCodeBarMolinete,
            string sCodeBarMolinete)
        {
            this.sCodUsuario = sCodUsuario;
            this.sNomUsuario = sNomUsuario;
            this.sApeUsuario = sApeUsuario;
            this.sCtaUsuario = sCtaUsuario;
            this.sPwdActualUsuario = sPwdActualUsuario;
            this.sFlgCambioClave = sFlgCambioClave;
            this.sTipoEstadoActual = sTipoEstadoActual;
            this.dtFchVigencia = dtFchVigencia;
            this.sTipoGrupo = sTipoGrupo;
            this.sFchCreacion = sFchCreacion;
            this.sHoraCreacion = sHoraCreacion;
            this.sCodUsuarioCreacion = sCodUsuarioCreacion;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.sLogFechaMod = sLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
            this.bGenerarCodeBarDestrabe = bGenerarCodeBarDestrabe;
            this.sCodeBarDestrabe = sCodeBarDestrabe;
            this.bGenerarCodeBarMolinete = bGenerarCodeBarMolinete;
            this.sCodeBarMolinete = sCodeBarMolinete;
        }
        #endregion

        #region Properties
        public string SCodUsuario
        {
            get { return sCodUsuario; }
            set { sCodUsuario = value; }
        }

        public string SNomUsuario
        {
            get { return sNomUsuario; }
            set { sNomUsuario = value; }
        }

        public string SApeUsuario
        {
            get { return sApeUsuario; }
            set { sApeUsuario = value; }
        }

        public string SCtaUsuario
        {
            get { return sCtaUsuario; }
            set { sCtaUsuario = value; }
        }

        public string SPwdActualUsuario
        {
            get { return sPwdActualUsuario; }
            set { sPwdActualUsuario = value; }
        }

        public string SFlgCambioClave
        {
            get { return sFlgCambioClave; }
            set { sFlgCambioClave = value; }
        }

        public string STipoEstadoActual
        {
            get { return sTipoEstadoActual; }
            set { sTipoEstadoActual = value; }
        }

        public DateTime DtFchVigencia
        {
            get { return dtFchVigencia; }
            set { dtFchVigencia = value; }
        }

        public string STipoGrupo
        {
            get { return sTipoGrupo; }
            set { sTipoGrupo = value; }
        }

        public string SDscEstadoActual
        {
            get { return sDscEstadoActual; }
            set { sDscEstadoActual = value; }
        }
        public string SDscGrupo
        {
            get { return sDscGrupo; }
            set { sDscGrupo = value; }
        }

        public string SFchCreacion
        {
            get { return sFchCreacion; }
            set { sFchCreacion = value; }
        }

        public string SHoraCreacion
        {
            get { return sHoraCreacion; }
            set { sHoraCreacion = value; }
        }

        public string SCodUsuarioCreacion
        {
            get { return sCodUsuarioCreacion; }
            set { sCodUsuarioCreacion = value; }
        }

        public string SLogUsuarioMod
        {
            get { return sLogUsuarioMod; }
            set { sLogUsuarioMod = value; }
        }

        public string SLogFechaMod
        {
            get { return sLogFechaMod; }
            set { sLogFechaMod = value; }
        }

        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
        }

        public string SFlgUsuarioPinPad
        {
              get
              {
                    return sFlgUsuarioPinPad;
              }
              set
              {
                    sFlgUsuarioPinPad = value;
              }
        }

        public string SPwdUsuarioPinpad
        {
              get
              {
                    return sPwdUsuarioPinpad;
              }
              set
              {
                    sPwdUsuarioPinpad = value;
              }
        }

        public string BGenerarCodeBarDestrabe
        {
            get
            {
                return bGenerarCodeBarDestrabe;
            }
            set
            {
                bGenerarCodeBarDestrabe = value;
            }
        }

        public string SCodeBarDestrabe
        {
            get
            {
                return sCodeBarDestrabe;
            }
            set
            {
                sCodeBarDestrabe = value;
            }
        }

        public string BGenerarCodeBarMolinete
        {
            get
            {
                return bGenerarCodeBarMolinete;
            }
            set
            {
                bGenerarCodeBarMolinete = value;
            }
        }

        public string SCodeBarMolinete
        {
            get
            {
                return sCodeBarMolinete;
            }
            set
            {
                sCodeBarMolinete = value;
            }
        }

        public string Emp_Recaudadora
        {
            get
            {
                return sEmp_Recaudadora;
            }
            set
            {
                sEmp_Recaudadora = value;
            }
        }

        #endregion

    }
}
