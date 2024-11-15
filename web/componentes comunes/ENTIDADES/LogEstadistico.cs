/*
Sistema		     : TUUA
Aplicación		 : WEB ADMINISTRACION
Objetivo		 : Describir la entidad LogEstadistico.
Especificaciones :
Fecha Creacion	 : 23/09/2011
Programador		 :	KOMONTE
Observaciones	 :	--
*/ 

using System;

namespace LAP.TUUA.ENTIDADES
{
    public class LogEstadistico
    {
        #region Fields
        private int num_secuencial;
        private string fch_ejecucion;
        private string estado_ejecucion;
        private string des_error;
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the LogOperacion class.
        /// </summary>
        public LogEstadistico()
        {
        }
        #endregion

        #region Properties

        public int Num_secuencial
        {
            get { return num_secuencial; }
            set { num_secuencial = value; }
        }

        public string Fch_ejecucion
        {
            get { return fch_ejecucion; }
            set { fch_ejecucion = value; }
        }

        public string Estado_ejecucion
        {
            get { return estado_ejecucion; }
            set { estado_ejecucion = value; }
        }

        public string Des_error
        {
            get { return des_error; }
            set { des_error = value; }
        }
        #endregion
    }
}
