using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;
using System.Data;

namespace LAP.EXTRANET.DAO
{
    public class DAO_ListaDeCampo : DAO_BaseDatos
    {
        public DAO_ListaDeCampo(string sConfigSPPath)
            : base(sConfigSPPath)            
        { 
        }

        public DataTable obtenerListaxNombre(string sNomCampo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LCSP_OBTENER_LISTACAMPONOMBRE"];
                string sNombreSP = (string)hsSelectByIdUSP["LCSP_OBTENER_LISTACAMPONOMBRE"];
                result = base.ListarDataTableSP(sNombreSP, sNomCampo);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PARAMETROS GENERALES
        public DataTable ObtenerParametroGeneral(string sIdentificador)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PARAMGEN_OBTENERVALORPARAMGENER"];
                string sNombreSP = (string)hsSelectByIdUSP["PARAMGEN_OBTENERVALORPARAMGENER"];
                result = base.ListarDataTableSP(sNombreSP, sIdentificador);


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
