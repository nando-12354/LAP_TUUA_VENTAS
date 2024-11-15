using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class Molinete
    {
        #region Fields

        private string sCodMolinete;      
        private string sDscIp;       
        private string sDscMolinete;       
        private string sTipDocumento;        
        private string sTipVuelo;                
        private string sTipAcceso;
        private string sTipEstado;       
        private string sLogUsuarioMod;
        private string dtLogFechaMod;
        private string sLogHoraMod;
        private string sEstMaster;
        private string sDscDBName;        
        private string sDscDBUser;        
        private string sDscDBPassword;
        private int iCantidad;
        private string sTipMolinete;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Compania class.
        /// </summary>
        public Molinete()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Compania class.
        /// </summary>
        public Molinete(string sCodMolinete, string sDscIp, string sDscMolinete, string sTipDocumento, 
                        string sTipVuelo, string sTipAcceso, string sTipEstado, string sLogUsuarioMod, 
                        string dtLogFechaMod, string sLogHoraMod, string sEstMaster, string sDscDBName, 
                        string sDscDBUser,string sDscDBPassword,int iCantidad, string sTipoMolinete)
        {
            this.sCodMolinete = sCodMolinete;
            this.sDscIp = sDscIp;
            this.sDscMolinete = sDscMolinete;
            this.sTipDocumento = sTipDocumento;
            this.sTipVuelo = sTipVuelo;
            this.sTipAcceso = sTipAcceso;
            this.sTipMolinete = sTipoMolinete;
            this.sTipEstado = sTipEstado;
            this.sLogUsuarioMod = sLogUsuarioMod;
            this.dtLogFechaMod = dtLogFechaMod;
            this.sLogHoraMod = sLogHoraMod;
            this.sEstMaster = sEstMaster;
            this.sDscDBName = sDscDBName; 
            this.sDscDBUser = sDscDBUser;
            this.sDscDBPassword = sDscDBPassword;
            this.iCantidad = iCantidad;
        }

        #endregion

        #region Properties

        public string SCodMolinete
        {
            get { return sCodMolinete; }
            set { sCodMolinete = value; }
        }

        public string SDscIp
        {
            get { return sDscIp; }
            set { sDscIp = value; }
        }

        public string SDscMolinete
        {
            get { return sDscMolinete; }
            set { sDscMolinete = value; }
        }


        public string STipDocumento
        {
            get { return sTipDocumento; }
            set { sTipDocumento = value; }
        }


        public string STipVuelo
        {
            get { return sTipVuelo; }
            set { sTipVuelo = value; }
        }


        public string STipAcceso
        {
            get { return sTipAcceso; }
            set { sTipAcceso = value; }
        }

        public string STipMolinete
        {
            get { return sTipMolinete; }
            set { sTipMolinete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string STipEstado
        {
            get { return sTipEstado; }
            set { sTipEstado = value; }
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
        /// Gets or sets the dtLogFechaMod value.
        /// </summary>
        public string DtLogFechaMod
        {
            get { return dtLogFechaMod; }
            set { dtLogFechaMod = value; }
        }

        /// <summary>
        /// Gets or sets the sLogHoraMod value.
        /// </summary>
        public string SLogHoraMod
        {
            get { return sLogHoraMod; }
            set { sLogHoraMod = value; }
        }


        public string SEstMaster
        {
            get { return sEstMaster; }
            set { sEstMaster = value; }
        }


        public string SDscDBName
        {
            get { return sDscDBName; }
            set { sDscDBName = value; }
        }


        public string SDscDBUser
        {
            get { return sDscDBUser; }
            set { sDscDBUser = value; }
        }


        public string SDscDBPassword
        {
            get { return sDscDBPassword; }
            set { sDscDBPassword = value; }
        }

        public int ICantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }

        #endregion
    }
}
