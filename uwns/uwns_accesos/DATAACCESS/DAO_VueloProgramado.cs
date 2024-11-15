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
    public class DAO_VueloProgramado : DAO_BaseDatos
    {
        #region Fields
        public List<VueloProgramado> objListaVueloProgramado;
        #endregion

        #region Constructors

        public DAO_VueloProgramado(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaVueloProgramado = new List<VueloProgramado>();
        }

        public DAO_VueloProgramado(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaVueloProgramado = new List<VueloProgramado>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOVueloProgramado table.
        /// </summary>
        public bool insertar(VueloProgramado objVueloProgramado)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["VPSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["VPSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objVueloProgramado.Cod_Compania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Vuelo"], DbType.String, objVueloProgramado.Num_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objVueloProgramado.Fch_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Hor_Vuelo"], DbType.String, objVueloProgramado.Hor_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Vuelo"], DbType.String, objVueloProgramado.Dsc_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objVueloProgramado.Tip_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objVueloProgramado.Tip_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Destino"], DbType.String, objVueloProgramado.Dsc_Destino);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objVueloProgramado.Log_Usuario_Mod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objVueloProgramado.Log_Fecha_Mod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objVueloProgramado.Log_Hora_Mod);
        
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int insertarlista(List<VueloProgramado> Lst_VueloProgramado) 
        {
              int nInsert = 0;
              DbCommand objCommandWrapper;
              using (System.Data.Common.DbConnection connection = helper.CreateConnection())
              {
                    connection.Open();
                    System.Data.Common.DbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                          //limpiar
                          Hashtable hsUpdateUSP = (Hashtable)htSPConfig["VPSP_DELETE"];
                          string sNombreSPDel = (string)hsUpdateUSP["VPSP_DELETE"];

                          objCommandWrapper = helper.GetStoredProcCommand(sNombreSPDel);
                          helper.ExecuteNonQuery(objCommandWrapper,transaction);

                          //insert
                          Hashtable hsInsertUSP = (Hashtable)htSPConfig["VPSP_INSERT"];
                          string sNombreSP = (string)hsInsertUSP["VPSP_INSERT"];
                          foreach (VueloProgramado objVueloProgramado in Lst_VueloProgramado)
                          {
                                objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objVueloProgramado.Cod_Compania);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Vuelo"], DbType.String, objVueloProgramado.Num_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objVueloProgramado.Fch_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Hor_Vuelo"], DbType.String, objVueloProgramado.Hor_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Vuelo"], DbType.String, objVueloProgramado.Dsc_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objVueloProgramado.Tip_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objVueloProgramado.Tip_Estado);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Destino"], DbType.String, objVueloProgramado.Dsc_Destino);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objVueloProgramado.Log_Usuario_Mod);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objVueloProgramado.Log_Fecha_Mod);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objVueloProgramado.Log_Hora_Mod);

                                if (helper.ExecuteNonQuery(objCommandWrapper, transaction) != 0)
                                      nInsert++;
                          }
                          transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                          transaction.Rollback();
                          throw ex;
                    }
                    finally
                    {
                          connection.Close();
                    }
              }
              return nInsert;
        }
      

        /// <summary>
        /// Updates a record in the DAOVueloProgramado table.
        /// </summary>
        public bool actualizar(VueloProgramado objVueloProgramado)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                /*helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Vuelo_Programado"], DbType.String, objVueloProgramado.Cod_Vuelo_Programado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Vuelo"], DbType.String, objVueloProgramado.Num_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Inicio"], DbType.String, objVueloProgramado.Fch_Inicio);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Ffin"], DbType.String, objVueloProgramado.Fch_Fin);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Vuelo"], DbType.String, objVueloProgramado.Dsc_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Hor_Vuelo"], DbType.String, objVueloProgramado.Hor_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dia_Vuelo"], DbType.String, objVueloProgramado.Dia_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Dia"], DbType.String, objVueloProgramado.Tip_Dia);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Vuelo"], DbType.String, objVueloProgramado.Tip_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objVueloProgramado.Tip_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Destino"], DbType.String, objVueloProgramado.Dsc_Destino);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Tiempo_Vuelo"], DbType.String, objVueloProgramado.Dsc_Tiempo_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objVueloProgramado.Cod_Compania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario"], DbType.String, objVueloProgramado.Cod_Usuario);


                isActualizado = base.mantenerSP(objCommandWrapper);*/
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOVueloProgramado table by its primary key.
        /// </summary>
        public bool eliminar(string sCodVueloProgramado)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Vuelo_Programado"], DbType.String, 
                                                          sCodVueloProgramado);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Limpiar()
        {
              try
              {
                    //Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                    //string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                    //DbCommand objCommandWrapper = helper.GetSqlStringCommand("delete TUA_VueloProgramado");
                    //isActualizado = base.mantenerSP(objCommandWrapper);
                    //return isActualizado;

                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["VPSP_DELETE"];
                    string sNombreSP = (string)hsUpdateUSP["VPSP_DELETE"];

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
        /// Selecciona vuelo para una compañia en una fecha determinada
        /// </summary>
          public VueloProgramado obtener(string sCodCompania, string FchInicio, string NumVuelo)
        {
              VueloProgramado objVueloProgramado = null;
              IDataReader result;
              Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["NUMVUELOPROGRAMADO_SELECT"];
              string sNombreSP = (string)hsSelectByIdUSP["NUMVUELOPROGRAMADO_SELECT"];
              result = base.listarDataReaderSP(sNombreSP, sCodCompania, FchInicio, NumVuelo);

              while (result.Read())
              {
                    objVueloProgramado = poblar(result);

              }
              
              result.Close();
              return objVueloProgramado;
        }

        /// <summary>
        /// Selects a single record from the DAOVueloProgramado table.
        /// </summary>
        public VueloProgramado obtener(string sCodVueloProgramado)
        {
            VueloProgramado objVueloProgramado = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_VUELO_PROGRAMADO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_VUELO_PROGRAMADO"];
            result = base.listarDataReaderSP(sNombreSP, sCodVueloProgramado);

            while (result.Read())
            {
                objVueloProgramado = poblar(result);

            }
            
            result.Close();
            return objVueloProgramado;
        }

        public DataTable ListarVuelosxCompania(string strCompania, string strFecha,string strTipVuelo)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["VUEPROGSP_LISTARXCOMP"];
                string sNombreSP = (string)hsSelectByIdUSP["VUEPROGSP_LISTARXCOMP"];
                result = base.ListarDataTableSP(sNombreSP, strCompania, strFecha, strTipVuelo);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["VUEPROGSP_LISTARXCOMP"];
                string sNombreSP = (string)hsSelectByIdUSP["VUEPROGSP_LISTARXCOMP"];
                result = base.ListarDataTableSP(sNombreSP, strCompania, strFecha);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects all records from the DAOVueloProgramado table.
        /// </summary>
        public List<VueloProgramado> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaVueloProgramado.Add(poblar(objResult));
            }
            
            objResult.Close();
            return objListaVueloProgramado;
        }


        /// <summary>
        /// Creates a new instance of the DAOVueloProgramado class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public VueloProgramado poblar(IDataReader dataReader)
        {

              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["NUMVUELOPROGRAMADO_SELECTALL"];
            VueloProgramado objVueloProgramado = new VueloProgramado();

            if (dataReader[(string)htSelectAllUSP["Cod_Compania"]]!= DBNull.Value)
                objVueloProgramado.Cod_Compania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Num_Vuelo"]] != DBNull.Value)
                objVueloProgramado.Num_Vuelo = (string)dataReader[(string)htSelectAllUSP["Num_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Fch_Vuelo"]] != DBNull.Value)
                objVueloProgramado.Fch_Vuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Hor_Vuelo"]] != DBNull.Value)
                objVueloProgramado.Hor_Vuelo = (string)dataReader[(string)htSelectAllUSP["Hor_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Vuelo"]] != DBNull.Value)
                objVueloProgramado.Dsc_Vuelo = (string)dataReader[(string)htSelectAllUSP["Dsc_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Vuelo"]] != DBNull.Value)
                objVueloProgramado.Tip_Vuelo = (string)dataReader[(string)htSelectAllUSP["Tip_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                objVueloProgramado.Tip_Estado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Dsc_Destino"]] != DBNull.Value)
                objVueloProgramado.Dsc_Destino = (string)dataReader[(string)htSelectAllUSP["Dsc_Destino"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                objVueloProgramado.Log_Usuario_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                objVueloProgramado.Log_Fecha_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                objVueloProgramado.Log_Hora_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            if (dataReader["Num_Puerta"] != DBNull.Value)
                objVueloProgramado.Num_Puerta = (string)dataReader["Num_Puerta"];

            if (dataReader["Fch_Est"] != DBNull.Value)
                objVueloProgramado.Fch_Est = (DateTime)dataReader["Fch_Est"];

            if (dataReader["Fch_Prog"] != DBNull.Value)
                objVueloProgramado.Fch_Prog = (DateTime)dataReader["Fch_Prog"];

            if (dataReader["Fch_Real"] != DBNull.Value)
                objVueloProgramado.Fch_Real = (DateTime)dataReader["Fch_Real"];

            if (dataReader["Dsc_Estado"] != DBNull.Value)
                objVueloProgramado.Dsc_Estado = (string)dataReader["Dsc_Estado"];
            

            return objVueloProgramado;
        }


        public DataTable DetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["VPSP_VUELOPROGRAMADO_SEL"];
                string sNombreSP = (string)hsSelectByIdUSP["VPSP_VUELOPROGRAMADO_SEL"];
                result = base.ListarDataTableSP(sNombreSP, sFechaDesde, sFechaHasta, sCodCompania);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public VueloProgramado ObtenerVueloxNum(string FchInicio, string NumVuelo)
        {
              VueloProgramado objVueloProgramado = null;
              IDataReader objResult;
              Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["VUELOPROGRAMADO_SELECT"];
              string sNombreSP = (string)hsSelectByIdUSP["VUELOPROGRAMADO_SELECT"];
              objResult = base.listarDataReaderSP(sNombreSP, FchInicio, NumVuelo);

              if (objResult.Read())
              {
                  objVueloProgramado=poblar(objResult);
              }

              objResult.Close();
              return objVueloProgramado;
        }


        #endregion
    }
}
