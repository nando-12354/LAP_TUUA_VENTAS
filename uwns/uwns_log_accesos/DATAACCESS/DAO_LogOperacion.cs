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
    public class DAO_LogOperacion : DAO_BaseDatos
    {
        #region Fields

        public List<LogOperacion> objLogOperacion;
        #endregion

        #region Constructors
        public DAO_LogOperacion(string sConfigSPPath): base(sConfigSPPath)
        {
            objLogOperacion = new List<LogOperacion>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(LogOperacion objLogOperacion)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["OPESP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["OPESP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Operacion"], DbType.String, objLogOperacion.SCodTipoOperacion);
                //helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Proceso"], DbType.DateTime, objLogOperacion.DtFchProceso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objLogOperacion.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objLogOperacion.SCodUsuario);
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
        public bool actualizar(LogOperacion objLogOperacion)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Operacion"], DbType.String, objLogOperacion.SNumOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Tipo_Operacion"], DbType.String, objLogOperacion.SCodTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Proceso"], DbType.String, objLogOperacion.DtFchProceso);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objLogOperacion.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objLogOperacion.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Secuencial"], DbType.Int32, objLogOperacion.INumSecuencial);
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
        public bool eliminar(string sNumOperacion, int iNumSecuencial)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Operacion"], DbType.String, sNumOperacion);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Secuencial"], DbType.String, iNumSecuencial);


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

        public List<LogOperacion> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objLogOperacion.Add(poblar(objResult));

            }

            objResult.Close();
            return objLogOperacion;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public LogOperacion obtener(string sNumOperacion)
        {

            LogOperacion objLogOperacion = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGOPERACION"];
                string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_LOGOPERACION"];
                result = base.listarDataReaderSP(sNombreSP, sNumOperacion);

                while (result.Read())
                {
                    objLogOperacion = poblar(result);

                }

                result.Close();
                return objLogOperacion;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ExtornarCompraVenta(string strCodOpera,string strTurno,int intCantidad,ref string strMessage)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["OPESP_EXTORNAR"];
                string sNombreSP = (string)hsInsertUSP["OPESP_EXTORNAR"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Operacion"], DbType.String, strCodOpera);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, strTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Operaciones"], DbType.Int32, intCantidad);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public LogOperacion poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogOperacion objLogOperacion = new LogOperacion();
            objLogOperacion.SNumOperacion = (string)dataReader[(string)htSelectAllUSP["Num_Operacion"]];
            objLogOperacion.SCodTipoOperacion = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Operacion"]];
            objLogOperacion.DtFchProceso = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Proceso"]]);
            objLogOperacion.SCodTurno = (string)dataReader[(string)htSelectAllUSP["Cod_Turno"]];
            objLogOperacion.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
            objLogOperacion.INumSecuencial = (Int32)dataReader[(Int32)htSelectAllUSP["Num_Secuencial"]];
            return objLogOperacion;
        }


        public DataTable obtenerUsuarioxFechaOperacion(string sFechaOperacion,string sCodUsuario, string sTipoOperacion, string sCodMoneda)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LOSP_USUARIOXFECHAOPERACION"];
                string sNombreSP = (string)hsSelectByIdUSP["LOSP_USUARIOXFECHAOPERACION"];
                result = base.ListarDataTableSP(sNombreSP, sFechaOperacion, sCodUsuario, sTipoOperacion, sCodMoneda);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion
    }
}