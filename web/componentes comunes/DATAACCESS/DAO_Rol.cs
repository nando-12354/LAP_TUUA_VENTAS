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
    public class DAO_Rol : DAO_BaseDatos
    {
        #region Fields

        public List<Rol> objListaRoles;

        #endregion

        #region Constructors

        public DAO_Rol(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaRoles = new List<Rol>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_Rol table.
        /// </summary>
        public bool insertar(Rol objRol)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["RSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["RSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Padre_Rol"], DbType.String, objRol.SCodPadreRol);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Nom_Rol"], DbType.String, objRol.SNomRol);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objRol.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_Rol table.
        /// </summary>
        public bool actualizar(Rol objRol)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["RSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["RSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Rol"], DbType.String, objRol.SCodRol);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Nom_Rol"], DbType.String, objRol.SNomRol);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Usuario_Mod"], DbType.String, objRol.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the TUA_Rol table by its primary key.
        /// </summary>
        public bool eliminar(string sCodRol)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["RSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["RSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Rol"], DbType.String, sCodRol);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// Selects a single record from the TUA_Rol table.
        /// </summary>
        public Rol obtener(string sCodRol)
        {

            Rol objRol = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["RSP_OBTENER_ROL"];
            string sNombreSP = (string)hsSelectByIdUSP["RSP_OBTENER_ROL"];
            result = base.listarDataReaderSP(sNombreSP, sCodRol);

            while (result.Read())
            {
                objRol = poblar(result);
            }
            result.Dispose();
            result.Close();
            return objRol;
        }

        /// <summary>
        /// Selects a single record from the TUA_Rol table.
        /// </summary>
        public bool obtenerHijo(string sCodRol)
        {
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["RSP_OBTENERHIJO_ROL"];
            string sNombreSP = (string)hsSelectByIdUSP["RSP_OBTENERHIJO_ROL"];
            result = base.listarDataReaderSP(sNombreSP, sCodRol);

            while (result.Read())
            {
                return true;
            }
            result.Dispose();
            result.Close();
            return false;
        }


        /// <summary>
        /// Selects a single record from the TUA_Rol table.
        /// </summary>
        public Rol obtenerRolxNombre(string sNomRol)
        {
            Rol objRol = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["RSP_OBTENER_ROL_NOMBRE"];
            string sNombreSP = (string)hsSelectByIdUSP["RSP_OBTENER_ROL_NOMBRE"];
            result = base.listarDataReaderSP(sNombreSP, sNomRol);

            while (result.Read())
            {
                objRol = poblar(result);
            }
            result.Dispose();
            result.Close();
            return objRol;
        }


        //<summary>
        //Selects all records from the TUA_Rol table.
        //</summary>
        public List<Rol> listar()
        {
            IDataReader objResult;
            try
            {
                List<Rol> objLista = new List<Rol>();
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["RSP_SELECTALL"];
                string sNombreSP = (string)htSelectAllUSP["RSP_SELECTALL"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                    objLista.Add(poblar(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objLista;
            }
            catch (Exception)
            {
                throw;
            }
        }
    
        //<summary>
        //Selects all records from the TUA_Rol table.
        //</summary>
        public List<Rol> listarAsignados(string sCodUsuario)
        {
            IDataReader objResult;
            try
            {
                List<Rol> objLista = new List<Rol>();
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["RSP_SELECTALLASIG"];
                string sNombreSP = (string)htSelectAllUSP["RSP_SELECTALLASIG"];
                objResult = base.listarDataReaderSP(sNombreSP, sCodUsuario);

                while (objResult.Read())
                {
                    objLista.Add(poblar(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objLista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //<summary>
        //Selects all records from the TUA_Rol table.
        //</summary>
        public List<Rol> listarSinAsignar(string sCodUsuario)
        {
            IDataReader objResult;
            try
            {
                List<Rol> objLista = new List<Rol>();
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["RSP_SELECTALLNOASIG"];
                string sNombreSP = (string)htSelectAllUSP["RSP_SELECTALLNOASIG"];
                objResult = base.listarDataReaderSP(sNombreSP, sCodUsuario);

                while (objResult.Read())
                {
                    objLista.Add(poblar(objResult));

                }
                objResult.Dispose();
                objResult.Close();
                return objLista;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Selects all records from the TUA_Rol in DataTable .
        /// </summary>
        public DataTable obtenerALLRol()
        {
            DataTable result;
             Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["RSP_SELECTALL"];
            string sNombreSP = (string)hsSelectByIdUSP["RSP_SELECTALL"];
            result = base.ListarDataTableSP(sNombreSP, null);

            return result;
        }



        /// <summary>
        /// Creates a new instance of the TUA_Rol class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Rol poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["RSP_SELECTALL"];
            Rol objRol = new Rol();
            if (dataReader[(string)htSelectAllUSP["Cod_Rol"]] != DBNull.Value)
                objRol.SCodRol = (string)dataReader[(string)htSelectAllUSP["Cod_Rol"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Padre_Rol"]] != DBNull.Value)
                objRol.SCodPadreRol = (string)dataReader[(string)htSelectAllUSP["Cod_Padre_Rol"]];
            if (dataReader[(string)htSelectAllUSP["Nom_Rol"]] != DBNull.Value)
                objRol.SNomRol = (string)dataReader[(string)htSelectAllUSP["Nom_Rol"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objRol.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objRol.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objRol.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objRol;
        }

   
        #endregion
    }
}
