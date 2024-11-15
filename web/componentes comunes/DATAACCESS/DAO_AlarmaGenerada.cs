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
    public class DAO_AlarmaGenerada : DAO_BaseDatos
    {

        #region Fields
        public List<AlarmaGenerada> objListaAlarmaGenerada;
        #endregion


        #region Constructors
        public DAO_AlarmaGenerada(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaAlarmaGenerada = new List<AlarmaGenerada>();
        }

        public DAO_AlarmaGenerada(string sConfigSPPath,string strUsuario,string strModulo,string strSubModulo)
            : base(sConfigSPPath, strUsuario, strModulo, strSubModulo)
        {
            objListaAlarmaGenerada = new List<AlarmaGenerada>();
        }
        #endregion


        #region Methods

        /// <summary>
        /// Saves a record to the AlarmaGenerada table.
        /// </summary>
        public bool insertar(AlarmaGenerada objAlarmaGenerada)
        {
            try
            {

                Hashtable hsInsertUSP = (Hashtable)htSPConfig["AGSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["AGSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Alarma"], DbType.String, objAlarmaGenerada.SCodAlarma);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modulo"], DbType.String, objAlarmaGenerada.SCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Equipo"], DbType.String, objAlarmaGenerada.SDscEquipo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Importancia"], DbType.String, objAlarmaGenerada.STipImportancia);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Subject"], DbType.String, objAlarmaGenerada.SDscSubject);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Body"], DbType.String, objAlarmaGenerada.SDscBody);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objAlarmaGenerada.SLogUsuarioMod);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool insertarCrit(AlarmaGenerada objAlarmaGenerada)
        {
            try
            {

                //Hashtable hsInsertUSP = (Hashtable)htSPConfig["AGSP_INSERT"];
                //string sNombreSP = (string)hsInsertUSP["AGSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_alr_pcs_alarma_gen_ins_crit");
                helper.AddInParameter(objCommandWrapper, "Cod_Alarma", DbType.String, objAlarmaGenerada.SCodAlarma);
                helper.AddInParameter(objCommandWrapper, "Cod_Modulo", DbType.String, objAlarmaGenerada.SCodModulo);
                helper.AddInParameter(objCommandWrapper, "Dsc_Equipo", DbType.String, objAlarmaGenerada.SDscEquipo);
                helper.AddInParameter(objCommandWrapper, "Tip_Importancia", DbType.String, objAlarmaGenerada.STipImportancia);
                helper.AddInParameter(objCommandWrapper, "Dsc_Subject", DbType.String, objAlarmaGenerada.SDscSubject);
                helper.AddInParameter(objCommandWrapper, "Dsc_Body", DbType.String, objAlarmaGenerada.SDscBody);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objAlarmaGenerada.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objAlarmaGenerada.IdTxCritica);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Updates a record in the AlarmaGenerada table.
        /// </summary>
        public bool actualizar(AlarmaGenerada objAlarmaGenerada)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["AGSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["AGSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_AlarmaGenerada"], DbType.String, objAlarmaGenerada.ICodAlarmaGenerada);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objAlarmaGenerada.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Estado_Mail"], DbType.String, objAlarmaGenerada.SFlgEstadoMail);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Atencion"], DbType.String, objAlarmaGenerada.SDscAtencion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objAlarmaGenerada.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;


            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the AlarmaGenerada table.
        /// </summary>
        public List<AlarmaGenerada> listar()
        {
            try
            {
                IDataReader objResult;
                Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["AGSP_SELECTALL"];
                string sNombreSP = (string)hsSelectAllUSP["AGSP_SELECTALL"];
                objResult = base.listarDataReaderSP(sNombreSP, null);

                while (objResult.Read())
                {
                    objListaAlarmaGenerada.Add(poblar(objResult));
                }
                objResult.Dispose();
                objResult.Close();
                return objListaAlarmaGenerada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects all records from the AlarmaGenerada table
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAllAlarmaGenerada()
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AGSP_SELECTALL"];
                string sNombreSP = (string)hsSelectByIdUSP["AGSP_SELECTALL"];
                result = base.ListarDataTableSP(sNombreSP, null);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects all records from the AlarmaGenerada table
        /// </summary>
        /// <returns></returns>
        public DataTable ListarAlarmaGeneradaEnviadas()
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AGSP_SELEC_ENVIADAS"];
                string sNombreSP = (string)hsSelectByIdUSP["AGSP_SELEC_ENVIADAS"];
                result = base.ListarDataTableSP(sNombreSP, null);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AGSP_SELEC_FILTRO"];
                string sNombreSP = (string)hsSelectByIdUSP["AGSP_SELEC_FILTRO"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sModulo, sTipoAlarma, sEstado);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Selects all records from the AlarmaGenerada table by a foreign key.
        /// </summary>
        public AlarmaGenerada ObtenerAlarmaGenerada(string SCodAlarma, string SCodModulo, string SDscEquipo, string SLogUsuarioMod)
        {
            try
            {
                AlarmaGenerada objAlarmaGenerada = null;
                IDataReader result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AGSP_OBTENER_ALARMA_GENERADA"];
                string sNombreSP = (string)hsSelectByIdUSP["AGSP_OBTENER_ALARMA_GENERADA"];
                result = base.listarDataReaderSP(sNombreSP, SCodAlarma, SCodModulo, SDscEquipo, SLogUsuarioMod);

                while (result.Read())
                {
                    objAlarmaGenerada = poblar(result);
                }


                result.Dispose();
                result.Close();
                return objAlarmaGenerada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Selects all records from the AlarmaGenerada table by a foreign key.
        /// </summary>
        public AlarmaGenerada obtener(string sCodAlarmaGenerada)
        {
            try
            {
                AlarmaGenerada objAlarmaGenerada = null;
                IDataReader result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["AGSP_OBTENER_ALARMA_GENERADA"];
                string sNombreSP = (string)hsSelectByIdUSP["AGSP_OBTENER_ALARMA_GENERADA"];
                result = base.listarDataReaderSP(sNombreSP, sCodAlarmaGenerada);

                while (result.Read())
                {
                    objAlarmaGenerada = poblar(result);
                }


                result.Dispose();
                result.Close();
                return objAlarmaGenerada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      

        /// <summary>
        /// Creates a new instance of the AlarmaGenerada class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public AlarmaGenerada poblar(IDataReader dataReader)
        {
            try
            {
                Hashtable htSelectAllUSP = (Hashtable)htSPConfig["AGSP_SELECTALL"];
                AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();
                if (dataReader[(string)htSelectAllUSP["Cod_AlarmaGenerada"]] != DBNull.Value)
                    objAlarmaGenerada.ICodAlarmaGenerada = Convert.ToInt32(dataReader[(string)htSelectAllUSP["Cod_AlarmaGenerada"]]);
                if (dataReader[(string)htSelectAllUSP["Cod_Alarma"]] != DBNull.Value)
                    objAlarmaGenerada.SCodAlarma = (string)dataReader[(string)htSelectAllUSP["Cod_Alarma"]];
                if (dataReader[(string)htSelectAllUSP["Cod_Modulo"]] != DBNull.Value)
                    objAlarmaGenerada.SCodModulo = (string)dataReader[(string)htSelectAllUSP["Cod_Modulo"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Equipo"]] != DBNull.Value)
                    objAlarmaGenerada.SDscEquipo = (string)dataReader[(string)htSelectAllUSP["Dsc_Equipo"]];
                if (dataReader[(string)htSelectAllUSP["Fch_Generacion"]] != DBNull.Value)
                    objAlarmaGenerada.DtFchGeneracion = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Generacion"]]);
                if (dataReader[(string)htSelectAllUSP["Fch_Actualizacion"]] != DBNull.Value)
                    objAlarmaGenerada.DtFchActualizacion = Convert.ToDateTime(dataReader[(string)htSelectAllUSP["Fch_Actualizacion"]]);
                if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                    objAlarmaGenerada.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
                if (dataReader[(string)htSelectAllUSP["Tip_Importancia"]] != DBNull.Value)
                    objAlarmaGenerada.STipImportancia = (string)dataReader[(string)htSelectAllUSP["Tip_Importancia"]];
                if (dataReader[(string)htSelectAllUSP["Flg_Estado_Mail"]] != DBNull.Value)
                    objAlarmaGenerada.SFlgEstadoMail = (string)dataReader[(string)htSelectAllUSP["Flg_Estado_Mail"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Subject"]] != DBNull.Value)
                    objAlarmaGenerada.SDscSubject = (string)dataReader[(string)htSelectAllUSP["Dsc_Subject"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Body"]] != DBNull.Value)
                    objAlarmaGenerada.SDscBody = (string)dataReader[(string)htSelectAllUSP["Dsc_Body"]];
                if (dataReader[(string)htSelectAllUSP["Dsc_Atencion"]] != DBNull.Value)
                    objAlarmaGenerada.SDscAtencion = (string)dataReader[(string)htSelectAllUSP["Dsc_Atencion"]];
                if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                    objAlarmaGenerada.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
                return objAlarmaGenerada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable obtenerAlarmasGeneradasSinEnviar()
        {
            DataTable result;

            try
            {
                string sQuery = "SELECT Cod_AlarmaGenerada FROM dbo.TUA_AlarmaGenerada WHERE Flg_Estado_Mail = '0'";

                result = base.ListarDataTableQY(sQuery);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public bool verificarEstadoEnvio(string sIdAlarmaGen)
        {
             

            try
            {
                string sQuery = "SELECT Flg_Estado_Mail FROM dbo.TUA_AlarmaGenerada WHERE Cod_AlarmaGenerada = " + sIdAlarmaGen;

                DataTable dt = base.ListarDataTableQY(sQuery);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["Flg_Estado_Mail"] != DBNull.Value && dt.Rows[0]["Flg_Estado_Mail"].ToString().Trim().Equals("1"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool EnviarCorreo(string sIdAlarma)
        {
            try
            {
                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_alr_pcs_send_mail");
                helper.AddInParameter(objCommandWrapper, "sIdAlarma", DbType.String, sIdAlarma);
                
                return base.mantenerSPSinAuditoria(objCommandWrapper);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }

}
