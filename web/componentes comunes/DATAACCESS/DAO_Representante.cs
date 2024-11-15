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
    public class DAO_Representante : DAO_BaseDatos
    {
        #region Fields

        public List<RepresentantCia> objListaRoles;

        #endregion

        #region Constructors

        public DAO_Representante(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaRoles = new List<RepresentantCia>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_RepresentantCia table.
        /// </summary>
        public bool insertar(RepresentantCia objRepresentante)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["REPSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["REPSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Compania"], DbType.String, objRepresentante.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Nom_Representante"], DbType.String, objRepresentante.SNomRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Ape_Representante"], DbType.String, objRepresentante.SApeRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cargo_Representante"], DbType.String, objRepresentante.SCargoRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["TDoc_Representante"], DbType.String, objRepresentante.STDocRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["NDoc_Representante"], DbType.String, objRepresentante.SNDocRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Perm_Representante"], DbType.String, objRepresentante.SPermRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objRepresentante.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool insertarCrit(RepresentantCia objRepresentante)
        {
            try
            {
                //Hashtable htInsertUSP = (Hashtable)htSPConfig["REPSP_INSERT"];
                //string sNombreSP = (string)htInsertUSP["REPSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_representante_ins_crit");
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objRepresentante.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Nom_Representante", DbType.String, objRepresentante.SNomRepresentante);
                helper.AddInParameter(objCommandWrapper, "Ape_Representante", DbType.String, objRepresentante.SApeRepresentante);
                helper.AddInParameter(objCommandWrapper, "Cargo_Representante", DbType.String, objRepresentante.SCargoRepresentante);
                helper.AddInParameter(objCommandWrapper, "TDoc_Representante", DbType.String, objRepresentante.STDocRepresentante);
                helper.AddInParameter(objCommandWrapper, "NDoc_Representante", DbType.String, objRepresentante.SNDocRepresentante);
                helper.AddInParameter(objCommandWrapper, "Perm_Representante", DbType.String, objRepresentante.SPermRepresentante);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objRepresentante.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objRepresentante.IdTxCritica);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_RepresentantCia table.
        /// </summary>
        public bool actualizar(RepresentantCia objRepresentante)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["REPSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["REPSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Num_Secuencia"], DbType.Int32, objRepresentante.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Compania"], DbType.String, objRepresentante.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Nom_Representante"], DbType.String, objRepresentante.SNomRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Ape_Representante"], DbType.String, objRepresentante.SApeRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cargo_Representante"], DbType.String, objRepresentante.SCargoRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["TDoc_Representante"], DbType.String, objRepresentante.STDocRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["NDoc_Representante"], DbType.String, objRepresentante.SNDocRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Perm_Representante"], DbType.String, objRepresentante.SPermRepresentante);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Tip_Estado"], DbType.String, objRepresentante.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Usuario_Mod"], DbType.String, objRepresentante.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool actualizarCrit(RepresentantCia objRepresentante)
        {
            try
            {
                //Hashtable htUpdateUSP = (Hashtable)htSPConfig["REPSP_UPDATE"];
                //string sNombreSP = (string)htUpdateUSP["REPSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_representante_upd_crit");
                helper.AddInParameter(objCommandWrapper, "Num_Secuencia", DbType.Int32, objRepresentante.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objRepresentante.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Nom_Representante", DbType.String, objRepresentante.SNomRepresentante);
                helper.AddInParameter(objCommandWrapper, "Ape_Representante", DbType.String, objRepresentante.SApeRepresentante);
                helper.AddInParameter(objCommandWrapper, "Cargo_Representante", DbType.String, objRepresentante.SCargoRepresentante);
                helper.AddInParameter(objCommandWrapper, "TDoc_Representante", DbType.String, objRepresentante.STDocRepresentante);
                helper.AddInParameter(objCommandWrapper, "NDoc_Representante", DbType.String, objRepresentante.SNDocRepresentante);
                helper.AddInParameter(objCommandWrapper, "Perm_Representante", DbType.String, objRepresentante.SPermRepresentante);
                helper.AddInParameter(objCommandWrapper, "Tip_Estado", DbType.String, objRepresentante.STipEstado);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objRepresentante.SLogUsuarioMod);

                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objRepresentante.IdTxCritica);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes a record from the TUA_RepresentantCia table by its primary key.
        /// </summary>
        public bool eliminar(int iRepId)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["REPSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["REPSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Secuencia"], DbType.Int32, iRepId);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// Selects a single record from the TUA_RepresentantCia table.
        /// </summary>
        public List<RepresentantCia> ListarReptexCia(string strCia)
        {
            try
            {
                List<RepresentantCia> lista = new List<RepresentantCia>();
                IDataReader result;
                Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["REPSP_OBTENER_REPTEXCIA"];
                string sNombreSP = (string)htSelectByIdUSP["REPSP_OBTENER_REPTEXCIA"];
                result = base.listarDataReaderSP(sNombreSP, strCia);

                while (result.Read())
                {
                    lista.Add(poblar(result));
                }
                result.Dispose();
                result.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Validacion del Documento para un representante
        /// </summary>

        public int ValidarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNumDocumento)
        {
            try
            {
                int ivalidar;
                IDataReader result;
                Hashtable htSelectByIdUSP = (Hashtable)htSPConfig["VALIDACION_DOCUMENTO"];
                string sNombreSP = (string)htSelectByIdUSP["VALIDACION_DOCUMENTO"];
                result = base.listarDataReaderSP(sNombreSP, sNombre, sApellido, sNumDocumento, sTpDocumento);

                result.Read();
                ivalidar = (Int32)result[(string)htSelectByIdUSP["EstadoVal"]];
                result.Dispose();
                result.Close();
                return ivalidar;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Selects all records from the TUA_RepresentantCia table.
        /// </summary>
        /// <summary>
        /// Creates a new instance of the TUA_RepresentantCia class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public RepresentantCia poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["REPTESP_SELECTALL"];
            RepresentantCia objRepte = new RepresentantCia();
            if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                objRepte.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Ape_Representante"]] != DBNull.Value)
                objRepte.SApeRepresentante = (string)dataReader[(string)htSelectAllUSP["Ape_Representante"]];
            if (dataReader[(string)htSelectAllUSP["Nom_Representante"]] != DBNull.Value)
                objRepte.SNomRepresentante = (string)dataReader[(string)htSelectAllUSP["Nom_Representante"]];
            if (dataReader[(string)htSelectAllUSP["Cargo_Representante"]] != DBNull.Value)
                objRepte.SCargoRepresentante = (string)dataReader[(string)htSelectAllUSP["Cargo_Representante"]];
            if (dataReader[(string)htSelectAllUSP["TDoc_Representante"]] != DBNull.Value)
                objRepte.STDocRepresentante = (string)dataReader[(string)htSelectAllUSP["TDoc_Representante"]];
            if (dataReader[(string)htSelectAllUSP["NDoc_Representante"]] != DBNull.Value)
                objRepte.SNDocRepresentante = (string)dataReader[(string)htSelectAllUSP["NDoc_Representante"]];
            if (dataReader[(string)htSelectAllUSP["Perm_Representante"]] != DBNull.Value)
                objRepte.SPermRepresentante = (string)dataReader[(string)htSelectAllUSP["Perm_Representante"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                objRepte.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Num_Secuencia"]] != DBNull.Value)
                objRepte.INumSecuencial = (Int32)dataReader[(string)htSelectAllUSP["Num_Secuencia"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objRepte.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objRepte.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objRepte.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objRepte;

        }
        /// <summary>
        /// Selects all records from the TUA_RepresentantCia table by a foreign key.
        /// </summary>

        #endregion

        public DataTable consultarRepresXRehabilitacionYCia(string strCia)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["REHSP_REPRXREHYCIA"];
                string sNombreSP = (string)hsSelectByIdUSP["REHSP_REPRXREHYCIA"];
                result = base.ListarDataTableSP(sNombreSP, strCia);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}

