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
    public class DAO_ModalidadVenta : DAO_BaseDatos
    {
        #region Fields

        public List<ModalidadVenta> objListaModalidadVenta;

        #endregion

        #region Constructors

        public DAO_ModalidadVenta(string  htSPConfig)
              : base(htSPConfig)
        {
            objListaModalidadVenta = new List<ModalidadVenta>();
           // this.htSPConfig = htSPConfig;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_ModalidadVenta table.
        /// </summary>
        public bool insertar(ModalidadVenta objModalidad)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["MVSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["MVSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Nom_Modalidad"], DbType.String, objModalidad.SNomModalidad);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Tip_Modalidad"], DbType.String, objModalidad.STipModalidad);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objModalidad.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        /// <summary>
        /// Updates a record in the TUA_ModalidadVenta table.
        /// </summary>
        public bool actualizar(ModalidadVenta objModalidad)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["MVSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["MVSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objModalidad.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Modalidad"], DbType.String, objModalidad.SNomModalidad);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Modalidad"], DbType.String, objModalidad.STipModalidad);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objModalidad.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objModalidad.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the TUA_ModalidadVenta table by its primary key.
        /// </summary>
        public bool eliminar(string sCodModalidad)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["MVSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["MVSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Modalidad_Venta"], DbType.String, sCodModalidad);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Selects a single record from the TUA_ModalidadVenta table.
        /// </summary>
        public ModalidadVenta obtenerxCodigo(string sCodModalidad)
        {

            ModalidadVenta objModVenta = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MVSP_OBTENER_MODALIDAD_CODIGO"];
            string sNombreSP = (string)hsSelectByIdUSP["MVSP_OBTENER_MODALIDAD_CODIGO"];
            result = base.listarDataReaderSP(sNombreSP, sCodModalidad);

            while (result.Read())
            {
                objModVenta = poblar(result);
            }
            
            result.Close();
            return objModVenta;
        }


        /// <summary>
        /// Selects a single record from the TUA_ModalidadVenta table.
        /// </summary>
        public ModalidadVenta obtenerxNombre(string sNomModalidad)
        {

            ModalidadVenta objModVenta = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MVSP_OBTENER_MODALIDAD_NOMBRE"];
            string sNombreSP = (string)hsSelectByIdUSP["MVSP_OBTENER_MODALIDAD_NOMBRE"];
            result = base.listarDataReaderSP(sNombreSP, sNomModalidad);

            while (result.Read())
            {
                objModVenta = poblar(result);
            }
            
            result.Close();
            return objModVenta;
        }

        /// <summary>
        /// Selects all records from the TUA_ModalidadVenta table.
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAllModalidadVenta()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["MVSP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["MVSP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);

            return result;
        }

        /// <summary>
        /// Selects all records from the TUA_ModalidadVenta table.
        /// </summary>
        public List<ModalidadVenta> listar()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MVSP_SELECTALL"];
            string sNombreSP = (string)htSelectAllUSP["MVSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaModalidadVenta.Add(poblar(objResult));
            }
            
            objResult.Close();
            return objListaModalidadVenta;
        }

        /// <summary>
        /// Creates a new instance of the TUA_ModalidadVenta class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public ModalidadVenta poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MVSP_SELECTALL"];
            ModalidadVenta objModalidadVenta = new ModalidadVenta();

            if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value) 


            if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)

            objModalidadVenta.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];

            if (dataReader[(string)htSelectAllUSP["Nom_Modalidad"]] != DBNull.Value) 

            if (dataReader[(string)htSelectAllUSP["Nom_Modalidad"]] != DBNull.Value)

            objModalidadVenta.SNomModalidad = (string)dataReader[(string)htSelectAllUSP["Nom_Modalidad"]];

            if (dataReader[(string)htSelectAllUSP["Tip_Modalidad"]] != DBNull.Value) 

            if (dataReader[(string)htSelectAllUSP["Tip_Modalidad"]] != DBNull.Value)

            objModalidadVenta.STipModalidad = (string)dataReader[(string)htSelectAllUSP["Tip_Modalidad"]];

            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value) 

            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
            objModalidadVenta.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];

            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
            objModalidadVenta.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];

            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value) 

            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)

            objModalidadVenta.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];

            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value) 

            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)

            objModalidadVenta.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objModalidadVenta;
        }

        #endregion
    }
}
