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
    public class DAOLogAcceso : DAO_BaseDatos
    {
        #region Fields

        public List<LogAcceso> objListaLogAcceso;
        #endregion

        #region Constructors
        public DAOLogAcceso(string htSPConfig)
            : base(htSPConfig)
        {
            objListaLogAcceso = new List<LogAcceso>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(LogAcceso objLogAcceso)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Modulo"], DbType.String, objLogAcceso.SLogModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario"], DbType.String, objLogAcceso.SLogUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Rol"], DbType.String, objLogAcceso.SLogRol);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Proceso"], DbType.String, objLogAcceso.SLogProceso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Ejecucion"], DbType.DateTime, objLogAcceso.DtFchEjecucion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Tipo"], DbType.String, objLogAcceso.SLogTipo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Estado"], DbType.String, objLogAcceso.SLogEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Hra_Ejecucion"], DbType.String, objLogAcceso.SHoraEjecucion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objLogAcceso.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Windows"], DbType.String, objLogAcceso.SLogUsuarioWindows);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Dominio_Windows"], DbType.String, objLogAcceso.SLogDominioWindows);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Ip"], DbType.String, objLogAcceso.SLogIp);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the objPrecioTicket table.
        /// </summary>
        public bool actualizar(LogAcceso objLogAcceso)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Modulo"], DbType.String, objLogAcceso.SLogModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario"], DbType.String, objLogAcceso.SLogUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Rol"], DbType.String, objLogAcceso.SLogRol);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Proceso"], DbType.String, objLogAcceso.SLogProceso);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Ejecucion"], DbType.String, objLogAcceso.DtFchEjecucion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Tipo"], DbType.String, objLogAcceso.SLogTipo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Estado"], DbType.String, objLogAcceso.SLogEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Hra_Ejecucion"], DbType.String, objLogAcceso.SHoraEjecucion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objLogAcceso.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Windows"], DbType.String, objLogAcceso.SLogUsuarioWindows);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Dominio_Windows"], DbType.String, objLogAcceso.SLogDominioWindows);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Ip"], DbType.String, objLogAcceso.SLogIp);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the objPrecioTicket table by its primary key.
        /// </summary>
        public bool eliminar(string iNumSecuencial)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Secuencial"], DbType.Int32, iNumSecuencial);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Selects all records from the objPrecioTicket table.
        /// </summary>

        public List<LogAcceso> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaLogAcceso.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaLogAcceso;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public LogAcceso obtener(int iNumSecuencial)
        {

            LogAcceso objLogAcceso = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGACCESO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_LOGACCESO"];
            result = base.listarDataReaderSP(sNombreSP, iNumSecuencial);

            while (result.Read())
            {
                objLogAcceso = poblar(result);

            }
            
            result.Close();
            return objLogAcceso;
        }



        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public LogAcceso poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogAcceso objLogAcceso = new LogAcceso();
            objLogAcceso.INumSecuencial = (Int32)dataReader[(Int32)htSelectAllUSP["Num_Secuencial"]];
            objLogAcceso.SLogModulo = (string)dataReader[(string)htSelectAllUSP["Log_Modulo"]];
            objLogAcceso.SLogUsuario = (string)dataReader[(string)htSelectAllUSP["Log_Usuario"]];
            objLogAcceso.SLogRol = (string)dataReader[(string)htSelectAllUSP["Log_Rol"]];
            objLogAcceso.SLogProceso = (string)dataReader[(string)htSelectAllUSP["Log_Proceso"]];
            objLogAcceso.DtFchEjecucion = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Ejecucion"]]);
            objLogAcceso.SLogTipo = (string)dataReader[(string)htSelectAllUSP["Log_Tipo"]];
            objLogAcceso.SLogEstado = (string)dataReader[(string)htSelectAllUSP["Log_Estado"]];
            objLogAcceso.SHoraEjecucion = (string)dataReader[(string)htSelectAllUSP["Hra_Ejecucion"]];
            objLogAcceso.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
            objLogAcceso.SLogUsuarioWindows = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Windows"]];
            objLogAcceso.SLogDominioWindows = (string)dataReader[(string)htSelectAllUSP["Log_Dominio_Windows"]];
            objLogAcceso.SLogIp = (string)dataReader[(string)htSelectAllUSP["Log_Ip"]];
            return objLogAcceso;
        }

        #endregion
    }
}