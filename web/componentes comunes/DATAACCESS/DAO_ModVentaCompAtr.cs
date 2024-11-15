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
    public class DAO_ModVentaCompAtr : DAO_BaseDatos
    {
        #region Fields

        public List<ModVentaCompAtr> objListaModVentaCompAtr;

        #endregion

        #region Constructors

        public DAO_ModVentaCompAtr(string htSPConfig)
            : base(htSPConfig)
        {

              objListaModVentaCompAtr = new List<ModVentaCompAtr>();

            objListaModVentaCompAtr = new List<ModVentaCompAtr>();

            
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the TUA_ModVentaCompAtr table.
        /// </summary>
        public bool insertar(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["MODCOMATRSP_INSERT"];
                string sNombreSP = (string)htInsertUSP["MODCOMATRSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Modalidad_Venta"], DbType.String, objRModCompAtr.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Dsc_Valor"], DbType.String, objRModCompAtr.SDscValor);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Compania"], DbType.String, objRModCompAtr.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Atributo"], DbType.String, objRModCompAtr.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Log_Usuario_Mod"], DbType.String, objRModCompAtr.SLogUsuarioMod);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool insertarCrit(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                //Hashtable htInsertUSP = (Hashtable)htSPConfig["MODCOMATRSP_INSERT"];
                //string sNombreSP = (string)htInsertUSP["MODCOMATRSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_mventa_comp_atrib_ins_crit");
                helper.AddInParameter(objCommandWrapper, "Cod_Modalidad_Venta", DbType.String, objRModCompAtr.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, "Dsc_Valor", DbType.String, objRModCompAtr.SDscValor);
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objRModCompAtr.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Cod_Atributo", DbType.String, objRModCompAtr.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objRModCompAtr.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objRModCompAtr.IdTxCritica);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates a record in the TUA_ModVentaCompAtr table.
        /// </summary>
        public bool actualizar(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                Hashtable htUpdateUSP = (Hashtable)htSPConfig["MODCOMPATRSP_UPDATE"];
                string sNombreSP = (string)htUpdateUSP["MODCOMPATRSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objRModCompAtr.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Dsc_Valor"], DbType.String, objRModCompAtr.SDscValor);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Compania"], DbType.String, objRModCompAtr.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Cod_Atributo"], DbType.String, objRModCompAtr.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, (string)htUpdateUSP["Log_Usuario_Mod"], DbType.String, objRModCompAtr.SLogUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool actualizarCrit(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                //Hashtable htUpdateUSP = (Hashtable)htSPConfig["MODCOMPATRSP_UPDATE"];
                //string sNombreSP = (string)htUpdateUSP["MODCOMPATRSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_mventa_comp_atrib_upd_crit");
                helper.AddInParameter(objCommandWrapper, "Cod_Modalidad_Venta", DbType.String, objRModCompAtr.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, "Dsc_Valor", DbType.String, objRModCompAtr.SDscValor);
                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, objRModCompAtr.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Cod_Atributo", DbType.String, objRModCompAtr.SCodAtributo);
                helper.AddInParameter(objCommandWrapper, "Log_Usuario_Mod", DbType.String, objRModCompAtr.SLogUsuarioMod);

                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, objRModCompAtr.IdTxCritica);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ModVentaCompAtr> listar()
        {
              IDataReader objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["MVCASP_SELECT"];
              string sNombreSP = (string)hsSelectAllUSP["MVCASP_SELECT"];
              objResult = base.listarDataReaderSP(sNombreSP, null);

              while (objResult.Read())
              {
                    objListaModVentaCompAtr.Add(poblarsv(objResult));

              }
              objResult.Dispose();
              objResult.Close();
              return objListaModVentaCompAtr;
        }

        public ModVentaCompAtr poblarsv(IDataReader dataReader)
        {
              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MVCASP_SELECT"];
              ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr();
              if (dataReader.FieldCount == 0)
              {
                    return null;
              }
              else
              {
                    try
                    {
                          if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)
                                objModVentaCompAtr.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];

                          if (dataReader[(string)htSelectAllUSP["Cod_Atributo"]] != DBNull.Value)
                                objModVentaCompAtr.SCodAtributo = (string)dataReader[(string)htSelectAllUSP["Cod_Atributo"]];

                          if (dataReader[(string)htSelectAllUSP["Tip_Atributo"]] != DBNull.Value)
                                objModVentaCompAtr.STipAtributo = (string)dataReader[(string)htSelectAllUSP["Tip_Atributo"]];

                          if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                                objModVentaCompAtr.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];

                          if (dataReader[(string)htSelectAllUSP["Dsc_Valor"]] != DBNull.Value)
                                objModVentaCompAtr.SDscValor = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor"]];

                          if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                                objModVentaCompAtr.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];


                    }
                    catch (Exception e)
                    {
                          return null;
                    }
              }
              return objModVentaCompAtr;
        }
        /// <summary>
        /// Deletes a record from the TUA_ModVentaCompAtr table by its primary key.
        /// </summary>
        //public virtual void Delete(string cod_Modalidad_Venta, string cod_Compania, string cod_Atributo, string cod_Tipo_Ticket)
        //{
        //    SqlParameter[] parameters = new SqlParameter[]
        //    {
        //        new SqlParameter("@Cod_Modalidad_Venta", cod_Modalidad_Venta),
        //        new SqlParameter("@Dsc_Valor", dsc_Valor),
        //        new SqlParameter("@Log_Fecha_Mod", log_Fecha_Mod),
        //        new SqlParameter("@Log_Usuario_Mod", log_Usuario_Mod)
        //    };


        /// <summary>
        /// Deletes a record from the TUA_ModVentaComp table by its primary key.
        /// </summary>
        public bool eliminar(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["MODCOMPATRSP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["MODCOMPATRSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Compania"], DbType.String, sCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Modalidad_Venta"], DbType.String, sCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Cod_Atributo"], DbType.String, CodAtributo);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool eliminarCrit(ModVentaCompAtr oModVentaCompAtr)
        {
            try
            {
                //Hashtable htDeleteUSP = (Hashtable)htSPConfig["MODCOMPATRSP_DELETE"];
                //string sNombreSP = (string)htDeleteUSP["MODCOMPATRSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand("usp_adm_pcs_mventa_comp_atrib_del_crit");

                helper.AddInParameter(objCommandWrapper, "Cod_Compania", DbType.String, oModVentaCompAtr.SCodCompania);
                helper.AddInParameter(objCommandWrapper, "Cod_Modalidad_Venta", DbType.String, oModVentaCompAtr.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, "Cod_Atributo", DbType.String, oModVentaCompAtr.SCodAtributo);

                helper.AddInParameter(objCommandWrapper, "IdTxCritica", DbType.Int64, oModVentaCompAtr.IdTxCritica);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        // <summary>
        /// Realiza la obtención de TUA_ModVentaComp que se encuentran activas en el sistema.
        /// </summary>
        public List<ModVentaCompAtr> ObtenerModVentaCompAtr(string Cod_Compania, string Cod_Modalidad_Venta)
        {
            objListaModVentaCompAtr = new List<ModVentaCompAtr>();
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["MODCOMPATRSP_OBTENER"];
            string sNombreSP = (string)hsSelectAllUSP["MODCOMPATRSP_OBTENER"];
            objResult = base.listarDataReaderSP(sNombreSP, Cod_Compania, Cod_Modalidad_Venta);

            while (objResult.Read())
            {
                objListaModVentaCompAtr.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaModVentaCompAtr;
        }


        /// <summary>
        /// Selects all records from the TUA_RepresentantCia table.
        /// </summary>
        /// <summary>
        /// Creates a new instance of the TUA_RepresentantCia class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public ModVentaCompAtr poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["MODCOMPATRSP_SELECTALL"];
            ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr();
            if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)
                objModVentaCompAtr.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Valor"]] != DBNull.Value)
                objModVentaCompAtr.SDscValor = (string)dataReader[(string)htSelectAllUSP["Dsc_Valor"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objModVentaCompAtr.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objModVentaCompAtr.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objModVentaCompAtr.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                objModVentaCompAtr.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Atributo"]] != DBNull.Value)
                objModVentaCompAtr.SCodAtributo = (string)dataReader[(string)htSelectAllUSP["Cod_Atributo"]];
            if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                objModVentaCompAtr.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];

            return objModVentaCompAtr;

        }

        public int validarSerieTicketCompa(int SerieIni, int SerieFin, string modalidad, string compania)
        {
            int valEstado = 1;
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["VALIDACION_SERIETICKETCOMPA"];
            string sNombreSP = (string)hsSelectAllUSP["VALIDACION_SERIETICKETCOMPA"];
            objResult = base.listarDataReaderSP(sNombreSP, SerieIni, SerieFin, modalidad, compania);

            if (objResult.Read())
            {
                valEstado = (int)objResult[(string)hsSelectAllUSP["EstadoVal"]];
            }
            objResult.Dispose();
            objResult.Close();
            return valEstado;
        }

        #endregion
    }
}
