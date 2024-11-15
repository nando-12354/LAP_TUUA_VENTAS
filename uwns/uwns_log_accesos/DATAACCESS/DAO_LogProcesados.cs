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
    public class DAO_LogProcesados : DAO_BaseDatos
    {
        #region Fields

        public List<LogProcesados> objListaLogProcesados;

        #endregion

        #region Constructors

           
        public DAO_LogProcesados(string sConfigSPPath): base(sConfigSPPath)
        {
            objListaLogProcesados = new List<LogProcesados>();
        }

        public DAO_LogProcesados(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
            objListaLogProcesados = new List<LogProcesados>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOLogProcesados table.
        /// </summary>

        public bool insertar(LogProcesados objLogProcesados)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["LOGPROCESADOS_INSERT"];
                string sNombreSP = (string)hsInsertUSP["LOGPROCESADOS_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Equipo_Mod"], DbType.String, objLogProcesados.StrCodEquipoMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Nom_Archivo"], DbType.String, objLogProcesados.StrNomArchivo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objLogProcesados.StrLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objLogProcesados.StrLogHoraMod);

                isRegistrado = base.mantenerSPSinAuditoria(objCommandWrapper);

                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a record in the DAOLogProcesados table.
        /// </summary>

        public bool actualizar(LogProcesados objLogProcesados)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.String, objLogProcesados.StrLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objLogProcesados.StrLogHoraMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a record from the DAOLogProcesados table by its primary key.
        /// </summary>
        public bool eliminar(string strCodEquipoMod, string strNomArchivo )
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Equipo_Mod"], DbType.String, strCodEquipoMod);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Nom_Archivo"], DbType.String, strNomArchivo);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Selects a single record from the DAOLogProcesados table.
        /// </summary>

        protected List<LogProcesados> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaLogProcesados.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaLogProcesados;
        }



        /// <summary>
        /// Selects all records from the DAOLogProcesados table.
        /// </summary>

        protected LogProcesados obtener(string strCodEquipoMod)
        {

            LogProcesados objLogProcesados = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGPROCESADOS"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_LOGPROCESADOS"];
            result = base.listarDataReaderSP(sNombreSP, strCodEquipoMod);

            while (result.Read())
            {
                objLogProcesados = poblar(result);

            }
            
            result.Close();
            return objLogProcesados;
        }


        public DataTable ArchivoProcesado(string strCodEquipoMod, string strNomArchivo)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_ARCHIVO_PROCESADO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_ARCHIVO_PROCESADO"];
            result = base.ListarDataTableSP(sNombreSP, strCodEquipoMod, strNomArchivo);
            return result;
        }

        public DataTable UltimoArchivoProcesado(string strCodEquipoMod)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_ULTIMO_ARCHIVO_PROCESADO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_ULTIMO_ARCHIVO_PROCESADO"];
            result = base.ListarDataTableSP(sNombreSP, strCodEquipoMod);
            return result;
        }

        
        /// <summary>
        /// Creates a new instance of the DAOLogProcesados class and populates it with data from the specified SqlDataReader.
        /// </summary>

        protected LogProcesados poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogProcesados objLogProcesados = new LogProcesados();
            objLogProcesados.StrCodEquipoMod = (string)dataReader[(string)htSelectAllUSP["Cod_Equipo_Mod"]];
            return objLogProcesados;
        }


        #endregion
    }
}
