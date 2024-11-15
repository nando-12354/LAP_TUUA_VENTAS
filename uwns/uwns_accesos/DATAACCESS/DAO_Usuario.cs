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
    public class DAO_Usuario : DAO_BaseDatos
    {
        #region Fields

        public List<Usuario> objListaUsuario;
        #endregion

        #region Constructors

        public DAO_Usuario(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaUsuario = new List<Usuario>();
        }

        public DAO_Usuario(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaUsuario = new List<Usuario>();
        }

        public DAO_Usuario(Hashtable htSPConfig)
            :base(htSPConfig)
        {
            objListaUsuario = new List<Usuario>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_Usuario table.
        /// </summary>
        public bool insertar(Usuario objUsuario)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Nom_Usuario"], DbType.String, objUsuario.SNomUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Ape_Usuario"], DbType.String, objUsuario.SApeUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cta_Usuario"], DbType.String, objUsuario.SCtaUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Pwd_Actual_Usuario"], DbType.String, objUsuario.SPwdActualUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vigencia"], DbType.DateTime, objUsuario.DtFchVigencia);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Grupo"], DbType.String, objUsuario.STipoGrupo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objUsuario.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }





        /// <summary>
        /// Updates a record in the TUA_Usuario table.
        /// </summary>
        public bool actualizar(Usuario objUsuario)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objUsuario.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Usuario"], DbType.String, objUsuario.SNomUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Ape_Usuario"], DbType.String, objUsuario.SApeUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cta_Usuario"], DbType.String, objUsuario.SCtaUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vigencia"], DbType.DateTime, objUsuario.DtFchVigencia);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Grupo"], DbType.String, objUsuario.STipoGrupo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Cambio_Clave"], DbType.String, objUsuario.SFlgCambioClave);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objUsuario.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }


       public bool actualizarContraseña(int NumDias )
       {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["UCSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["UCSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["NumDias"], DbType.Int32, NumDias);
                    isActualizado = base.ejecutarTrxSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception)
              {
                    throw;
              }
        }
        public bool actualizarContrasena(int NumDias )
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["UCSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["UCSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["NumDias"], DbType.Int32, NumDias);
                    isActualizado = base.ejecutarTrxSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception)
              {
                    throw;
              }
        }

        public bool actualizarPermiso()
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["UPSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["UPSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    isActualizado = base.ejecutarTrxSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception)
              {
                    throw;
              }
        }

        /// <summary>
        /// Updates a record in the TUA_Usuario table.
        /// </summary>
        public bool actualizarContraseña(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE_CONTRASEÑA"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE_CONTRASEÑA"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, sCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Pwd_Actual_Usuario"], DbType.String, sContraseña);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vigencia"], DbType.DateTime, DtFchVigencia);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_Usuario table.
        /// </summary>
        public bool actualizarEstado(string sCodUsuario, string sEstado, string SLogUsuarioMod,string sFlagCambPw)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE_ESTADO"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE_ESTADO"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, sCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado_Actual"], DbType.String, sEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Cambio_Clave"], DbType.String, sFlagCambPw);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Deletes a record from the TUA_Usuario table by its primary key.
        /// </summary>
        public bool eliminar(string sCodUsuario, string sLogUsuarioMod)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Usuario"], DbType.String, sCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Log_Usuario_Mod"], DbType.String, sLogUsuarioMod);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Selects all records from the TUA_Usuario table.
        /// </summary>

        public List<Usuario> listar()
        {
            IDataReader objResult;
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)htSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaUsuario.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaUsuario;
        }





        /// <summary>
        /// Listar todos los usuarios segun filtro
        /// </summary>
        /// <param name="sRol"></param>
        /// <param name="sEstado"></param>
        /// <param name="sGrupo"></param>
        /// <param name="iStarRows"></param>
        /// <param name="iMaxRows"></param>
        /// <returns></returns>
        public DataTable consultarUsuarioxFiltro(string sRol, string sEstado, string sGrupo, string CadFiltro, string sOrdenacion)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["LC_CONSULTARUSUARIOS"];
            string sNombreSP = (string)hsSelectByIdUSP["LC_CONSULTARUSUARIOS"];
            result = base.ListarDataTableSP(sNombreSP, sRol, sEstado, sGrupo, CadFiltro, sOrdenacion);
            return result;
        }



        /// <summary>
        /// Listar todos los usuarios
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAllUsuario()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);


            return result;
        }



        /// <summary>
        /// Selects a single record from the TUA_Usuario table.
        /// </summary>


        public Usuario obtener(string sCodUsuario)
        {

            Usuario objUsuario = null;
            IDataReader result;
            Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_USUARIO"];
            string sNombreSP = (string)htSelectByIdUSP["USP_OBTENER_USUARIO"];
            result = base.listarDataReaderSP(sNombreSP, sCodUsuario);

            while (result.Read())
            {
                objUsuario = poblar(result);

            }
            
            result.Close();
            return objUsuario;
        }

        public Usuario obtenerxCuenta(string sCuentaUsuario)
        {

            Usuario objUsuario = null;
            IDataReader result;
            Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_USUARIO_BYCUENTA"];
            string sNombreSP = (string)htSelectByIdUSP["USP_OBTENER_USUARIO_BYCUENTA"];
            result = base.listarDataReaderSP(sNombreSP, sCuentaUsuario);

            while (result.Read())
            {
                objUsuario = poblar(result);

            }
            
            result.Close();
            return objUsuario;
        }

        public Usuario autenticar(string sCuenta, string sClave)
        {

            Usuario objUsuario = null;
            IDataReader result;
            Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["USP_AUTENTICAR"];
            string sNombreSP = (string)htSelectByIdUSP["USP_AUTENTICAR"];

            result = base.listarDataReaderSP(sNombreSP, sCuenta, sClave);
            int iCounter = 0;
            if (result != null)
            {
                while (result.Read())
                {
                    objUsuario = poblar(result);
                    iCounter++;
                }
                
                result.Close();
            }

            if (iCounter == 1)
                return objUsuario;
            else return null;

        }

        public Usuario autenticar(string sClave)
        {

            Usuario objUsuario = null;
            IDataReader result;
            Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["USP_AUTENTICAR_PINPAD"];
            string sNombreSP = (string)htSelectByIdUSP["USP_AUTENTICAR_PINPAD"];
            result = base.listarDataReaderSP(sNombreSP, sClave);
            int iCounter = 0;
            if (result != null)
            {
                while (result.Read())
                {
                    objUsuario = poblar(result);
                    iCounter++;
                }
                
                result.Close();
            }

            if (iCounter == 1)
                return objUsuario;
            else return null;
        }


  
        public DataTable obtenerFecha()
        {
            DataTable result;

            //Begin Build Query --------------------
            String strSQL;
            strSQL = "select dbo.NTPFunction() as FechaSistema";
            //End Build Query --------------------

            result = base.ListarDataTableQY(strSQL);
            return result;
        }



        /// <summary>
        /// Creates a new instance of the TUA_Usuario class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Usuario poblar(IDataReader dataReader)
        {
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
                Usuario objUsuario = new Usuario();
                if (dataReader[(string)htSelectAllUSP["Cod_Usuario"]] != DBNull.Value)
                    objUsuario.SCodUsuario = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]];
                if (dataReader[(string)htSelectAllUSP["Nom_Usuario"]] != DBNull.Value)
                    objUsuario.SNomUsuario = (string)dataReader[(string)htSelectAllUSP["Nom_Usuario"]];
                if (dataReader[(string)htSelectAllUSP["Ape_Usuario"]] != DBNull.Value)
                    objUsuario.SApeUsuario = (string)dataReader[(string)htSelectAllUSP["Ape_Usuario"]];
                if (dataReader[(string)htSelectAllUSP["Cta_Usuario"]] != DBNull.Value)
                    objUsuario.SCtaUsuario = (string)dataReader[(string)htSelectAllUSP["Cta_Usuario"]];
                if (dataReader[(string)htSelectAllUSP["Pwd_Actual_Usuario"]] != DBNull.Value)
                    objUsuario.SPwdActualUsuario = (string)dataReader[(string)htSelectAllUSP["Pwd_Actual_Usuario"]];
                if (dataReader[(string)htSelectAllUSP["Flg_Cambio_Clave"]] != DBNull.Value)
                    objUsuario.SFlgCambioClave = (string)dataReader[(string)htSelectAllUSP["Flg_Cambio_Clave"]];
                if (dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]] != DBNull.Value)
                    objUsuario.STipoEstadoActual = (string)dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_EstadoActual"]] != DBNull.Value)
                    objUsuario.SDscEstadoActual = (string)dataReader[(string)htSelectAllUSP["Dsc_EstadoActual"]];
                //if (dataReader[(string)htSelectAllUSP["Fch_Vigencia"]] != DBNull.Value)
                //    objUsuario.DtFchVigencia = (DateTime)dataReader[(string)htSelectAllUSP["Fch_Vigencia"]];
                    //objUsuario.SApeUsuario = (string)dataReader[(string)htSelectAllUSP["Fch_Vigencia"]];

                if (dataReader[(string)htSelectAllUSP["Tip_Grupo"]] != DBNull.Value)
                    objUsuario.STipoGrupo = (string)dataReader[(string)htSelectAllUSP["Tip_Grupo"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Grupo"]] != DBNull.Value)
                    objUsuario.SDscGrupo = (string)dataReader[(string)htSelectAllUSP["Dsc_Grupo"]];
                if (dataReader[(string)htSelectAllUSP["Fch_Creacion"]] != DBNull.Value)
                    objUsuario.SFchCreacion = (string)dataReader[(string)htSelectAllUSP["Fch_Creacion"]];
                if (dataReader[(string)htSelectAllUSP["Hor_Creacion"]] != DBNull.Value)
                    objUsuario.SHoraCreacion = (string)dataReader[(string)htSelectAllUSP["Hor_Creacion"]];
                if (dataReader[(string)htSelectAllUSP["Cod_Usuario_Creacion"]] != DBNull.Value)
                    objUsuario.SCodUsuarioCreacion = (string)dataReader[(string)htSelectAllUSP["Cod_Usuario_Creacion"]];
                if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                    objUsuario.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
                if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                    objUsuario.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
                if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                    objUsuario.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

                return objUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
