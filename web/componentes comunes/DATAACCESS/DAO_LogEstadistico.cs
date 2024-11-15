/*
Sistema		     : TUUA
Aplicación		 : WEB ADMINISTRACION
Objetivo		 : Describir el DAO LogEstadistico.
Especificaciones :
Fecha Creacion	 : 23/09/2011
Programador		 :	KOMONTE
Observaciones	 :	--
*/ 

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_LogEstadistico : DAO_BaseDatos
    {
        #region Fields

        public List<LogEstadistico> objLogEstadistico;
        #endregion


         public DAO_LogEstadistico(string sConfigSPPath): base(sConfigSPPath)
        {
            objLogEstadistico = new List<LogEstadistico>();
        }

        //metodo que obtiene la fecha de ejecucion optima del estadistico
        public string obtenerFecha(string sEstadistico)
        {
            LogEstadistico objLogEstadistico= null;
            DataTable result;

            try
            {
                Hashtable hsSelectFecha = (Hashtable)htSPConfig["SP_LOGESTADISTICO_FECHA"];
                string sNombreSP = (string)hsSelectFecha["SP_LOGESTADISTICO_FECHA"];
                result = base.ListarDataTableSP(sNombreSP, sEstadistico);

                return result.Rows[0][0].ToString();

            }catch(Exception){
                throw;
            }

        }
    }
}
