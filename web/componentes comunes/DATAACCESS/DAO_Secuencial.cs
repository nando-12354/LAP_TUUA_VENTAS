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
    public class DAO_Secuencial : DAO_BaseDatos
    {
        #region Fields

        public IList<Secuencial> objListaSecuencial;

        #endregion

        #region Constructors

          public DAO_Secuencial(string htSPConfig)
                : base(htSPConfig)
        {
            objListaSecuencial = new List<Secuencial>();
            //this.htSPConfig = htSPConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_Secuencial table.
        /// </summary>
        public bool insertar(Secuencial objSecuencial)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["SSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["SSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Secuencial"], DbType.String, objSecuencial.SCodSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Ultimo_Valor"], DbType.String, objSecuencial.SDscUltimoValor);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_Secuencial table.
        /// </summary>
        public bool actualizar(Secuencial objSecuencial)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["SSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["SSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Secuencial"], DbType.String, objSecuencial.SCodSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Ultimo_Valor"], DbType.String, objSecuencial.SDscUltimoValor);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects a single record from the TUA_Secuencial table.
        /// </summary>
        public Secuencial obtener(string sCodSecuencial)
        {

            Secuencial objSecuencial = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["SSP_OBTENER_SECUENCIAL"];
            string sNombreSP = (string)hsSelectByIdUSP["SSP_OBTENER_SECUENCIAL"];
            result = base.listarDataReaderSP(sNombreSP, sCodSecuencial);

            while (result.Read())
            {
                objSecuencial = poblar(result);
            }
            result.Dispose();
            result.Close();
            return objSecuencial;
        }

        /// <summary>
        /// Creates a new instance of the TUA_Secuencial class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Secuencial poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["SSP_SELECTALL"];
            Secuencial objSecuencial = new Secuencial();
            objSecuencial.SCodSecuencial = (string)dataReader[(string)htSelectAllUSP["Cod_Secuencial"]];
            objSecuencial.SDscUltimoValor = (string)dataReader[(string)htSelectAllUSP["Dsc_Ultimo_Valor"]];

            return objSecuencial;
        }
        #endregion
    }
}
