using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Molinete : DAO_BaseDatos
    {
        #region Fields

        #endregion


        #region Constructors
        public DAO_Molinete(string sConfigSPPath)
            : base(sConfigSPPath)
        {

        }
        #endregion

        #region Methods

        public DataTable obtenerMolinetexCod(string strCodigo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MSP_OBTENERMOLINETEXCODIGO"];
            string sNombreSP = (string)hsSelectByIdUSP["MSP_OBTENERMOLINETEXCODIGO"];
            result = base.ListarDataTableSP(sNombreSP, strCodigo);
            return result;
        }
        
        

        public DataTable listarAllMolinete()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MSP_LISTARALLMOLINETE"];
            string sNombreSP = (string)hsSelectByIdUSP["MSP_LISTARALLMOLINETE"];
            result = base.ListarDataTableSP(sNombreSP, null,null);
            return result;
        }


        #endregion
    }
}
