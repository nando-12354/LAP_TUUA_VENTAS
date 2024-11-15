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
    public class DAO_CnfgAlarma : DAO_BaseDatos
    {
        #region Fields
        public List<CnfgAlarma> objListaCnfgAlarma;
        #endregion


        #region Constructors
        public DAO_CnfgAlarma(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaCnfgAlarma = new List<CnfgAlarma>();
        }

        public DAO_CnfgAlarma(string sConfigSPPath, string strUsuario, string strModulo, string strSubModulo)
            : base(sConfigSPPath, strUsuario, strModulo, strSubModulo)
        {
            objListaCnfgAlarma = new List<CnfgAlarma>();
        }
        #endregion


        #region Methods

        /// <summary>
        /// Saves a record to the CnfgAlarma table.
        /// </summary>
        public bool insertar(CnfgAlarma objCnfgAlarma)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["ACSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["ACSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Alarma"], DbType.String, objCnfgAlarma.SCodAlarma);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modulo"], DbType.String, objCnfgAlarma.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Fin_Mensaje"], DbType.String, objCnfgAlarma.SDscFinMensaje);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Xml_Destinatarios"], DbType.String, objCnfgAlarma.SDscDestinatarios);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objCnfgAlarma.SLogUsuarioMod);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a record in the CnfgAlarma table.
        /// </summary>
        public bool actualizar(CnfgAlarma objCnfgAlarma)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["ACSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["ACSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Alarma"], DbType.String, objCnfgAlarma.SCodAlarma);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modulo"], DbType.String, objCnfgAlarma.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Fin_Mensaje"], DbType.String, objCnfgAlarma.SDscFinMensaje);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Xml_Destinatarios"], DbType.String, objCnfgAlarma.SDscDestinatarios);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objCnfgAlarma.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;


            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Deletes a record from the CnfgAlarma table by a foreign key.
        /// </summary>
        public bool eliminar(string sCodAlarma, string sCodModulo)
        {
            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["ACSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["ACSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Alarma"], DbType.String, sCodAlarma);
                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Modulo"], DbType.String, sCodModulo);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        /// <summary>
        /// Selects all records from the CnfgAlarma table.
        /// </summary>
        public List<CnfgAlarma> listar()
        {

            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["ACSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["ACSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaCnfgAlarma.Add(poblar(objResult));
            }
            
            objResult.Close();
            return objListaCnfgAlarma;
        }

        /// <summary>
        /// Selects all records from the CnfgAlarma table
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAllCnfgAlarma()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ACSP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["ACSP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);

            return result;
        }

        /// <summary>
        /// Selects all records from the CnfgAlarma table by a foreign key.
        /// </summary>
        public CnfgAlarma obtener(string sCodAlarma, string sCodModulo)
        {

            CnfgAlarma objCnfgAlarma = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["ACSP_OBTENER_ALARMA"];
            string sNombreSP = (string)hsSelectByIdUSP["ACSP_OBTENER_ALARMA"];
            result = base.listarDataReaderSP(sNombreSP, sCodAlarma, sCodModulo);

            while (result.Read())
            {
                objCnfgAlarma = poblar(result);
            }


            result.Close();
            return objCnfgAlarma;
        }

        /// <summary>
        /// Creates a new instance of the CnfgAlarma class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public CnfgAlarma poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["ACSP_SELECTALL"];
            CnfgAlarma objCnfgAlarma = new CnfgAlarma();
            if (dataReader[(string)htSelectAllUSP["Cod_Alarma"]] != DBNull.Value)
                objCnfgAlarma.SCodAlarma = (string)dataReader[(string)htSelectAllUSP["Cod_Alarma"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
                objCnfgAlarma.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Fin_Mensaje"]] != DBNull.Value)
                objCnfgAlarma.SDscFinMensaje = (string)dataReader[(string)htSelectAllUSP["Dsc_Fin_Mensaje"]];
            if (dataReader[(string)htSelectAllUSP["Xml_Destinatarios"]] != DBNull.Value)
                objCnfgAlarma.SDscDestinatarios = (string)dataReader[(string)htSelectAllUSP["Xml_Destinatarios"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objCnfgAlarma.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objCnfgAlarma.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objCnfgAlarma.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            return objCnfgAlarma;
        }

        #endregion
    }
}
