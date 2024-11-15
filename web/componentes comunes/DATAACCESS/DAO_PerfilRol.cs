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
    public class DAO_PerfilRol : DAO_BaseDatos
    {
        #region Fields

        public List<PerfilRol> objListaPerfilRol;

        #endregion

        #region Constructors

        public DAO_PerfilRol(String htSPConfig)
            : base(htSPConfig)
        {
            objListaPerfilRol = new List<PerfilRol>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_PerfilRol table.
        /// </summary>
        public bool insertar(PerfilRol objPerfilRol)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["PRSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["PRSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Proceso"], DbType.String, objPerfilRol.SCodProceso);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Modulo"], DbType.String, objPerfilRol.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Rol"], DbType.String, objPerfilRol.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objPerfilRol.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_PerfilRol table.
        /// </summary>
        public bool actualizar(PerfilRol objPerfilRol)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["PRSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["PRSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Proceso"], DbType.String, objPerfilRol.SCodProceso);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modulo"], DbType.String, objPerfilRol.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Rol"], DbType.String, objPerfilRol.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Permitido"], DbType.String, objPerfilRol.SFlgPermitido);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objPerfilRol.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the TUA_PerfilRol table.
        /// </summary>
        public DataTable listarPerfilRolxRol(string sCodRol)
        {
            DataTable objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["PRSP_SELECTALL_BY_CODROL"];
            string sNombreSP = (string)hsSelectAllUSP["PRSP_SELECTALL_BY_CODROL"];
            objResult = base.ListarDataTableSP(sNombreSP, sCodRol);
            objResult.Dispose();

            return objResult;
        }

        /// <summary>
        /// Selects all records from the TUA_PerfilRol table.
        /// </summary>
        public string FlagPerfilRolxOpcion(string sCodUsuario,string sCodRol, string sDscArchivo, string sOpcion)
        {
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PRSP_SELECT_FLAG_OPCION"];
                string sNombreSP = (string)hsSelectByIdUSP["PRSP_SELECT_FLAG_OPCION"];
                result = base.listarDataReaderSP(sNombreSP, sCodUsuario, sCodRol,sDscArchivo, sOpcion);
                while (result.Read())
                {
                    return (string)result[(string)hsSelectByIdUSP["Flg_Permitido"]];
                }
                return "";
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Selects a single record from the TUA_PerfilRol table.
        /// </summary>
        public PerfilRol obtener(string sCodProceso, string sCodModulo, string sCodRol)
        {
            PerfilRol objPerfilRol = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["PRSP_OBTENER_PERFILROL"];
            string sNombreSP = (string)hsSelectByIdUSP["PRSP_OBTENER_PERFILROL"];
            result = base.listarDataReaderSP(sNombreSP, sCodProceso, sCodModulo, sCodRol);

            while (result.Read())
            {
                objPerfilRol = poblar(result);
            }
            result.Dispose();
            result.Close();
            return objPerfilRol;
        }

        /// <summary>
        /// Selects all records from the TUA_PerfilRol table.
        /// </summary>
        public List<PerfilRol> ListarxRol(string CodRol)
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PRSP_SELECTALL_BY_CODROL"];
            string sNombreSP = (string)htSelectAllUSP["PRSP_SELECTALL_BY_CODROL"];
            objResult = base.listarDataReaderSP(sNombreSP, CodRol);

            while (objResult.Read())
            {
                objListaPerfilRol.Add(poblar(objResult));
            }
            objResult.Dispose();
            objResult.Close();
            return objListaPerfilRol;
        }



        /// <summary>
        /// Selects all records from the TUA_PerfilRol table.
        /// </summary>
        public List<PerfilRol> listar()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PRSP_SELECTALL"];
            string sNombreSP = (string)htSelectAllUSP["PRSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaPerfilRol.Add(poblar(objResult));
            }
            objResult.Dispose();
            objResult.Close();
            return objListaPerfilRol;
        }

        /// <summary>
        /// Creates a new instance of the TUA_PerfilRol class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public PerfilRol poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["PRSP_SELECTALL"];
            PerfilRol objPerfilRol = new PerfilRol();
            if (dataReader[(string)htSelectAllUSP["Cod_Proceso"]] != DBNull.Value)
                objPerfilRol.SCodProceso = (string)dataReader[(string)htSelectAllUSP["Cod_Proceso"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
                objPerfilRol.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Rol"]] != DBNull.Value)
                objPerfilRol.SCodRol = (string)dataReader[(string)htSelectAllUSP["Cod_Rol"]];
            if (dataReader[(string)htSelectAllUSP["Flg_Permitido"]] != DBNull.Value)
                objPerfilRol.SFlgPermitido = (string)dataReader[(string)htSelectAllUSP["Flg_Permitido"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objPerfilRol.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objPerfilRol.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objPerfilRol.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objPerfilRol;
        }

        #endregion
    }
}
