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
    public class DAO_Alarma : DAO_BaseDatos
    {
        #region Fields
        public List<Alarma> objListaAlarma;
        #endregion


        #region Constructors
        public DAO_Alarma(string sConfigSPPath, string strUsuario, string strModulo, string strSubModulo)
            : base(sConfigSPPath, strUsuario, strModulo, strSubModulo)
        {
            objListaAlarma = new List<Alarma>();
        }
        public DAO_Alarma(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaAlarma = new List<Alarma>();
        }
        #endregion


        #region Methods

        /// <summary>
        /// Selects all records from the Alarma table.
        /// </summary>
        public List<Alarma> listar()
        {

            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["ALSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaAlarma.Add(poblar(objResult));
            }
            objResult.Dispose();
            objResult.Close();
            return objListaAlarma;
        }

        /// <summary>
        /// Selects all records from the Alarma table by a foreign key.
        /// </summary>
        public Alarma obtener(string sCodAlarma)
        {

            Alarma objAlarma = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ALP_OBTENER_ALARMA"];
            string sNombreSP = (string)hsSelectByIdUSP["ALP_OBTENER_ALARMA"];
            result = base.listarDataReaderSP(sNombreSP, sCodAlarma);

            while (result.Read())
            {
                objAlarma = poblar(result);
            }


            result.Dispose();
            result.Close();
            return objAlarma;
        }

        /// <summary>
        /// Selects all records from the TUA_Modulo table.
        /// </summary>
        /// <returns></returns>
        public DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ALP_OBTENER_ALARMA_BYMODULO"];
            string sNombreSP = (string)hsSelectByIdUSP["ALP_OBTENER_ALARMA_BYMODULO"];
            result = base.ListarDataTableSP(sNombreSP, sCodModulo);

            return result;
        }

        /// <summary>
        /// Creates a new instance of the Alarma class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Alarma poblar(IDataReader dataReader)
        {
            Alarma objAlarma = new Alarma();
            objAlarma.SCodAlarma = (string)dataReader["Cod_Alarma"];
            objAlarma.SDscNombre = (string)dataReader["Dsc_Nombre"];
            objAlarma.SCodModulo = (string)dataReader["Cod_Modulo"];
            return objAlarma;
        }

        #endregion
    }
}
