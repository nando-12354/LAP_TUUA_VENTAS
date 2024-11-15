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
    public class DAO_LogCompraVenta : DAO_BaseDatos
    {
        #region Fields

        public List<LogCompraVenta> objLogCompraVenta;
        #endregion

        #region Constructors
        public DAO_LogCompraVenta(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objLogCompraVenta = new List<LogCompraVenta>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the objPrecioTicket table.
        /// </summary>
        public bool insertar(LogCompraVenta objLogCompraVenta)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["LCVSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["LCVSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Operacion"], DbType.String, objLogCompraVenta.SCodTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objLogCompraVenta.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objLogCompraVenta.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objLogCompraVenta.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Tasa_Cambio"], DbType.Decimal, objLogCompraVenta.DImpTasaCambio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_ACambiar"], DbType.Decimal, objLogCompraVenta.DImpACambiar);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Cambiado"], DbType.Decimal, objLogCompraVenta.DImpCambiado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Ingreso"], DbType.Decimal, objLogCompraVenta.DImpIngreso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Egreso"], DbType.Decimal, objLogCompraVenta.DImpEgreso);
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
        public bool actualizar(LogCompraVenta objLogCompraVenta)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Operacion"], DbType.String, objLogCompraVenta.SNumOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Tasa_Cambio"], DbType.String, objLogCompraVenta.DImpTasaCambio);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_ACambiar"], DbType.String, objLogCompraVenta.DImpACambiar);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Cambiado"], DbType.String, objLogCompraVenta.DImpCambiado);
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
        public bool eliminar(string sNumOperacion)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Operacion"], DbType.String, sNumOperacion);


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

        public List<LogCompraVenta> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objLogCompraVenta.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objLogCompraVenta;
        }

        /// <summary>
        /// Selects a single record from the objPrecioTicket table.
        /// </summary>


        public LogCompraVenta obtener(string sNumOperacion)
        {

            LogCompraVenta objLogCompraVenta = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_LOGCOMPRAVENTA"];
                string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_LOGCOMPRAVENTA"];
                result = base.listarDataReaderSP(sNombreSP, sNumOperacion);

                while (result.Read())
                {
                    objLogCompraVenta = poblar(result);

                }

                result.Close();
                return objLogCompraVenta;
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// Creates a new instance of the objPrecioTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public LogCompraVenta poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            LogCompraVenta objLogCompraVenta = new LogCompraVenta();
            objLogCompraVenta.SNumOperacion = (string)dataReader[(string)htSelectAllUSP["Num_Operacion"]];
            objLogCompraVenta.DImpTasaCambio = (Int32)dataReader[(Int32)htSelectAllUSP["Imp_Tasa_Cambio"]];
            objLogCompraVenta.DImpACambiar = (Int32)dataReader[(Int32)htSelectAllUSP["Imp_ACambiar"]];
            objLogCompraVenta.DImpCambiado = (Int32)dataReader[(Int32)htSelectAllUSP["Imp_Cambiado"]];
            return objLogCompraVenta;
        }

        #endregion
    }
}