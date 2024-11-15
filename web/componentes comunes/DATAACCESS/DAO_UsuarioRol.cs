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
    public class DAO_UsuarioRol : DAO_BaseDatos
    {
        #region Fields
        public List<UsuarioRol> objListaUsuarioRol;

        #endregion

        #region Constructors

        public DAO_UsuarioRol(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaUsuarioRol = new List<UsuarioRol>();
            this.htSPConfig = htSPConfig;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOUsuarioRol table.
        /// </summary>
        public bool insertar(UsuarioRol objUsuarioRol)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["URSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["URSP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objUsuarioRol.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Rol"], DbType.String, objUsuarioRol.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objUsuarioRol.SLogUsuarioMod);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOUsuarioRol table.
        /// </summary>
        public bool actualizar(UsuarioRol objUsuarioRol)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["URSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["URSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objUsuarioRol.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Rol"], DbType.String, objUsuarioRol.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objUsuarioRol.SLogUsuarioMod);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOUsuarioRol table by its primary key.
        /// </summary>
        public bool eliminar(string sCodUsuario, string sCodRol)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["URSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["URSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Usuario"], DbType.String, sCodUsuario);
                    helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Rol"], DbType.String, sCodRol);
                };

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Selects a single record from the DAOUsuarioRol table.
        /// </summary>
        public UsuarioRol obtener(string sCodUsuario, string sCodRol)
        {
            UsuarioRol objUsuarioRol = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["URSP_OBTENER_USUARIO_ROL"];
            string sNombreSP = (string)hsSelectByIdUSP["URSP_OBTENER_USUARIO_ROL"];
            result = base.listarDataReaderSP(sNombreSP, sCodUsuario, sCodRol);

            while (result.Read())
            {
                objUsuarioRol = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objUsuarioRol;
        }

        /// <summary>
        /// Selects a single record from the DAOUsuarioRol table.
        /// </summary>
        public UsuarioRol obtenerxcodrol(string sCodRol)
        {
            UsuarioRol objUsuarioRol = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["URSP_OBTENER_USUARIO_ROL_BY_CODROL"];
            string sNombreSP = (string)hsSelectByIdUSP["URSP_OBTENER_USUARIO_ROL_BY_CODROL"];
            result = base.listarDataReaderSP(sNombreSP, sCodRol);

            while (result.Read())
            {
                objUsuarioRol = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objUsuarioRol;
        }

        /// <summary>
        /// Selects all records from the DAOUsuarioRol table.
        /// </summary>
        public List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            objListaUsuarioRol = new List<UsuarioRol>();
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["URSP_OBTENER_USUARIO_ROL_BY_CODUSUARIO"];
            string sNombreSP = (string)hsSelectAllUSP["URSP_OBTENER_USUARIO_ROL_BY_CODUSUARIO"];
            objResult = base.listarDataReaderSP(sNombreSP, sCodUsuario);

            while (objResult.Read())
            {
                objListaUsuarioRol.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaUsuarioRol;
        }


        /// <summary>
        /// Selects all records from the DAOUsuarioRol table.
        /// </summary>
        public List<UsuarioRol> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["URSP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["URSP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaUsuarioRol.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaUsuarioRol;
        }


        /// <summary>
        /// Creates a new instance of the DAOUsuarioRol class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public UsuarioRol poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["URSP_SELECTALL"];
            UsuarioRol objUsuarioRol = new UsuarioRol();
            if (dataReader[(string)htSelectAllUSP["Cod_Usuario"]] != DBNull.Value)
                objUsuarioRol.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Rol"]] != DBNull.Value)
                objUsuarioRol.SCodRol = (string)dataReader[(string)htSelectAllUSP["Cod_Rol"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objUsuarioRol.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objUsuarioRol.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objUsuarioRol.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objUsuarioRol;
        }

        public DataTable ListarUsuarioRol(string sCod_Rol)
 
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["OBTENER_USUARIOS_ROL"];
            string sNombreSP = (string)hsSelectByIdUSP["OBTENER_USUARIOS_ROL"];
            result = base.ListarDataTableSP(sNombreSP, sCod_Rol);
            return result;
        }

        #endregion
    }
}
