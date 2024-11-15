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
    public class DAO_TipoOperacion : DAO_BaseDatos
    {
        #region Fields

        public List<TipoOperacion> objListaTipoOperacion;

        #endregion

        #region Constructors

        public DAO_TipoOperacion(string htSPConfig)
            : base(htSPConfig)
        {
            objListaTipoOperacion = new List<TipoOperacion>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTipoOperacion table.
        /// </summary>
        public bool insertar(TipoOperacion objTipoOperacion)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Operacion"], DbType.String, objTipoOperacion.SCodTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Nom_Tipo_Operacion"], DbType.String, objTipoOperacion.SNomTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTipoOperacion.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.DateTime, objTipoOperacion.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objTipoOperacion.SLogHoraMod);


                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOTipoOperacion table.
        /// </summary>
        public bool actualizar(TipoOperacion objTipoOperacion)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Tipo_Operacion"], DbType.String, objTipoOperacion.SCodTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Tipo_Operacion"], DbType.String, objTipoOperacion.SNomTipoOperacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objTipoOperacion.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objTipoOperacion.DtLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objTipoOperacion.SLogHoraMod);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTipoOperacion table by its primary key.
        /// </summary>
        public bool eliminar(string sCodTipoOperacion)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Tipo_Operacion"], DbType.String, sCodTipoOperacion);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Selects a single record from the DAOTipoOperacion table.
        /// </summary>
        public TipoOperacion obtener(string sCodTipoOperacion)
        {
            TipoOperacion objTipoOperacion = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_TIPO_OPERACION"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_TIPO_OPERACION"];
            result = base.listarDataReaderSP(sNombreSP, sCodTipoOperacion);

            while (result.Read())
            {
                objTipoOperacion = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objTipoOperacion;
        }

        /// <summary>
        /// Selects all records from the DAOTipoOperacion table.
        /// </summary>
        public List<TipoOperacion> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaTipoOperacion.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaTipoOperacion;
        }

        /// <summary>
        /// Creates a new instance of the DAOTipoOperacion class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public TipoOperacion poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            TipoOperacion objTipoOperacion = new TipoOperacion();
            objTipoOperacion.SCodTipoOperacion = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Operacion"]];
            objTipoOperacion.SNomTipoOperacion = (string)dataReader[(string)htSelectAllUSP["Nom_Tipo_Operacion"]];
            objTipoOperacion.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            objTipoOperacion.DtLogFechaMod = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]]);
            objTipoOperacion.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objTipoOperacion;
        }

        #endregion
    }
}
