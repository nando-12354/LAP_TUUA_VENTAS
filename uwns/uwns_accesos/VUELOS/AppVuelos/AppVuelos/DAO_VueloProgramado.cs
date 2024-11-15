using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LAP.TUUA.DAO
{
    public class DAO_VueloProgramado : DAO_BaseDatos
    {

        #region Constructors

        public DAO_VueloProgramado()
            : base()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOVueloProgramado table.
        /// </summary>
        public bool insertar(string strNumVuelo, string strFechaVuelo)
        {
            try
            {
                string sNombreSP = "usp_acs_pcs_vuelosuper_ins";

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, "Num_Vuelo", DbType.String, strNumVuelo);
                helper.AddInParameter(objCommandWrapper, "Fch_Vuelo", DbType.String, strFechaVuelo);
        
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        #endregion
    }
}
