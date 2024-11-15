///V.1.4.6.0
///Luz Huaman
///Copyright ( Copyright © HIPER S.A. )
///
using System;
using System.Collections.Generic;

namespace LAP.TUUA.ENTIDADES
{
    public class Sincronizacion
    {

        #region Fields

        
        private string sTablaSincronizacion;
        private string sTip_Descrip;
        private string sDscMolinete;
        private string sTipoEstado;
        private string sDscEstado;
        private DateTime dtFchRegistro;
        private DateTime dtFchRegistro1;
        private string sTipoSincronizacion;
        private string sDscSincron;
        private string sNumRegistro;
        private string sCodigoSincronizacion;
       
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Sincronizacion class.
        /// </summary>
        public Sincronizacion()
        {
        }
        // Initializes a new instance of the Sincronizacion class.
        /// </summary>
        public Sincronizacion(string sTablaSincronizacion, 
            string sTip_Descrip, string sDscMolinete,
            string sTipoEstado, string sDscEstado, DateTime dtFchRegistro, 
            DateTime dtFchRegistro1, string sFchInicio, string sFchFin, 
            string sTipoSincronizacion, string sDscSincron, string sNumRegistro,
            string sCodigoSincronizacion)
        {
            
            this.sTablaSincronizacion = sTablaSincronizacion;
            this.sTip_Descrip = sTip_Descrip;
            this.sDscMolinete = sDscMolinete;
            this.sTipoEstado = sTipoEstado;
            this.sDscEstado = sDscEstado;
            this.dtFchRegistro = dtFchRegistro;
            this.dtFchRegistro = dtFchRegistro1;
            this.sTipoSincronizacion = sTipoSincronizacion;
            this.sDscSincron = sDscSincron;
            this.sNumRegistro = sNumRegistro;
            this.sCodigoSincronizacion = sCodigoSincronizacion;
            
     
        }

        #endregion

        #region Properties

      

        public string STablaSincronizacion
        {
            get { return sTablaSincronizacion; }
            set { sTablaSincronizacion = value; }
        }
          public string STip_Descrip
        {
            get { return sTip_Descrip; }
            set { sTip_Descrip = value; }
        }



        public string SDscMolinete
        {
            get { return sDscMolinete; }
            set { sDscMolinete = value; }
        }

        public string STipoEstado
        {
            get { return sTipoEstado; }
            set { sTipoEstado = value; }
        }
        public string SDscEstado
        {
            get { return sDscEstado; }
            set { sDscEstado = value; }
        }

        public DateTime DtFchRegistro
        {
            get { return dtFchRegistro; }
            set { dtFchRegistro = value; }
        }

        public DateTime DtFchRegistro1
        {
            get { return dtFchRegistro; }
            set { dtFchRegistro = value; }
        }


        public string STipoSincronizacion
        {
            get { return sTipoSincronizacion; }
            set { sTipoSincronizacion = value; }
        }
        public string SDscSincron
        {
            get { return sDscSincron; }
            set { sDscSincron = value; }
        }

        public string SNumRegistro
        {
            get { return sNumRegistro; }
            set { sNumRegistro = value; }
        }

        public string SCodigoSincronizacion
        {
            get { return sCodigoSincronizacion; }
            set { sCodigoSincronizacion = value; }
        }

        #endregion


    }


}
