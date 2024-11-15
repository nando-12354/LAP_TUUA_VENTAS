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
    public class DAO_LogOperacCaja : DAO_BaseDatos
    {
        #region Fields

        public List<LogOperacCaja> objLogOperacCaja;
        #endregion

        #region Constructors
        public DAO_LogOperacCaja(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objLogOperacCaja = new List<LogOperacCaja>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(LogOperacCaja objLogOperacCaja)
        {
            try
            {   
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["OPCAJASP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["OPCAJASP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Operacion"], DbType.String, objLogOperacCaja.SCodTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objLogOperacCaja.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objLogOperacCaja.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Secuencial"], DbType.Int32, objLogOperacCaja.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objLogOperacCaja.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Operacion"], DbType.Decimal, objLogOperacCaja.DImpOperacion);
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
        public bool actualizar(LogOperacCaja objLogOperacCaja)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Operacion"], DbType.String, objLogOperacCaja.SNumOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Secuencial"], DbType.Int32, objLogOperacCaja.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Moneda"], DbType.String, objLogOperacCaja.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Operacion"], DbType.Decimal, objLogOperacCaja.DImpOperacion);
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

        public List<LogOperacCaja> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objLogOperacCaja.Add(poblar(objResult));

            }

            objResult.Close();
            return objLogOperacCaja;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public LogOperacCaja obtener(string sNumOperacion, int iNumSecuencial)
        {

            LogOperacCaja objLogOperacCaja = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGOPERACCAJA"];
                string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_LOGOPERACCAJA"];
                result = base.listarDataReaderSP(sNombreSP, sNumOperacion, iNumSecuencial);

                while (result.Read())
                {
                    objLogOperacCaja = poblar(result);

                }

                result.Close();
                return objLogOperacCaja;
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public LogOperacCaja poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogOperacCaja objLogOperacCaja = new LogOperacCaja();
            objLogOperacCaja.SNumOperacion = (string)dataReader[(string)htSelectAllUSP["Num_Operacion"]];
            objLogOperacCaja.INumSecuencial = (Int32)dataReader[(Int32)htSelectAllUSP["Num_Secuencial"]];
            objLogOperacCaja.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
            objLogOperacCaja.DImpOperacion = (Int32)dataReader[(Int32)htSelectAllUSP["Imp_Operacion"]];
            return objLogOperacCaja;
        }

        #endregion
    }
}