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
    public class DAO_VuelosTemporada : DAO_BaseDatos
    {
        #region Fields
        public List<VuelosTemporada> objListaVuelosTemporada;
        #endregion
        #region Constructors

        public DAO_VuelosTemporada(string htSPConfig)
            : base(htSPConfig)
        {
            objListaVuelosTemporada = new List<VuelosTemporada>();

        }

          #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOVuelosTemporada table.
        /// </summary>
        public bool insertar(VuelosTemporada objVuelosTemporada)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["VTSP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["VTSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objVuelosTemporada.Cod_Compania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Vuelo"], DbType.String, objVuelosTemporada.Num_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objVuelosTemporada.Fch_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Hor_Vuelo"], DbType.String, objVuelosTemporada.Hor_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Vuelo"], DbType.String, objVuelosTemporada.Dsc_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objVuelosTemporada.Tip_Vuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objVuelosTemporada.Tip_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Destino"], DbType.String, objVuelosTemporada.Dsc_Destino);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objVuelosTemporada.Log_Usuario_Mod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objVuelosTemporada.Log_Fecha_Mod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objVuelosTemporada.Log_Hora_Mod);



                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public  int insertarlista(List<VuelosTemporada> Lst_VuelosTemporada)
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
                          Hashtable hsUpdateUSP = (Hashtable)htSPConfig["VTSP_DELETE"];
                          string sNombreSPDel = (string)hsUpdateUSP["VTSP_DELETE"];

                          objCommandWrapper = helper.GetStoredProcCommand(sNombreSPDel);
                          helper.ExecuteNonQuery(objCommandWrapper,transaction);

                          //insert
                          Hashtable hsInsertUSP = (Hashtable)htSPConfig["VTSP_INSERT"];
                          string sNombreSP = (string)hsInsertUSP["VTSP_INSERT"];
                          foreach (VuelosTemporada objVuelosTemporada in Lst_VuelosTemporada)
                          {
                                objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objVuelosTemporada.Cod_Compania);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Vuelo"], DbType.String, objVuelosTemporada.Num_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objVuelosTemporada.Fch_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Hor_Vuelo"], DbType.String, objVuelosTemporada.Hor_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Vuelo"], DbType.String, objVuelosTemporada.Dsc_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objVuelosTemporada.Tip_Vuelo);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objVuelosTemporada.Tip_Estado);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Destino"], DbType.String, objVuelosTemporada.Dsc_Destino);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objVuelosTemporada.Log_Usuario_Mod);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objVuelosTemporada.Log_Fecha_Mod);
                                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objVuelosTemporada.Log_Hora_Mod);

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
        /// Updates a record in the DAOVuelosTemporada table.
        /// </summary>
        public bool actualizar(VuelosTemporada objVuelosTemporada)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

               /* helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Vuelo_Temporada"], DbType.String, objVuelosTemporada.SCodVueloTemporada);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Vuelo"], DbType.String, objVuelosTemporada.SNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Inicio"], DbType.String, objVuelosTemporada.DtFchInicio);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Fin"], DbType.String, objVuelosTemporada.DtFchFin);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Vuelo"], DbType.String, objVuelosTemporada.SDscVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Hor_Vuelo"], DbType.String, objVuelosTemporada.SHorVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dia_Vuelo"], DbType.String, objVuelosTemporada.SDiaVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_vuelo"], DbType.String, objVuelosTemporada.STipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objVuelosTemporada.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Destino"], DbType.String, objVuelosTemporada.SDscDestino);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Tiempo_Vuelo"], DbType.String, objVuelosTemporada.SDscTiempoVuelo);*/


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOVuelosTemporada table by its primary key.
        /// </summary>
        public bool eliminar(string sCodVueloTemporada)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Vuelo_Temporada"], DbType.String, sCodVueloTemporada);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Selects a single record from the DAOVuelosTemporada table.
        /// </summary>
        public VuelosTemporada obtener(string sCodVueloTemporada)
        {
            VuelosTemporada objVuelosTemporada = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_VUELOS_TEMPORADA"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_VUELOS_TEMPORADA"];
            result = base.listarDataReaderSP(sNombreSP, sCodVueloTemporada);

            while (result.Read())
            {
                objVuelosTemporada = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objVuelosTemporada;
        }

        /// <summary>
        /// Selects all records from the DAOVuelosTemporada table.
        /// </summary>
        public List<VuelosTemporada> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaVuelosTemporada.Add(poblar(objResult));

            }
            objResult.Dispose();
            objResult.Close();
            return objListaVuelosTemporada;
        }

        /// <summary>
        /// Creates a new instance of the DAOVuelosTemporada class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public VuelosTemporada poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            VuelosTemporada objVuelosTemporada = new VuelosTemporada();
            objVuelosTemporada.Cod_Compania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            objVuelosTemporada.Num_Vuelo = (string)dataReader[(string)htSelectAllUSP["Num_Vuelo"]];
            objVuelosTemporada.Fch_Vuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
            objVuelosTemporada.Hor_Vuelo = (string)dataReader[(string)htSelectAllUSP["Hor_Vuelo"]];
            objVuelosTemporada.Dsc_Vuelo = (string)dataReader[(string)htSelectAllUSP["Dsc_Vuelo"]];
            objVuelosTemporada.Tip_Vuelo = (string)dataReader[(string)htSelectAllUSP["Tip_Vuelo"]];
            objVuelosTemporada.Tip_Estado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            objVuelosTemporada.Dsc_Destino = (string)dataReader[(string)htSelectAllUSP["Dsc_Destino"]];
            objVuelosTemporada.Log_Usuario_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            objVuelosTemporada.Log_Fecha_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            objVuelosTemporada.Log_Hora_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

            return objVuelosTemporada;
        }

        public bool Limpiar()
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["VTSP_DELETE"];
                    string sNombreSP = (string)hsUpdateUSP["VTSP_DELETE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    isActualizado = base.ejecutarTrxSP(objCommandWrapper);
                    return isActualizado;

                    //DbCommand objCommandWrapper = helper.GetSqlStringCommand("delete TUA_VuelosTemporada");
                    //isActualizado = base.mantenerSP(objCommandWrapper);
                    //return isActualizado;
              }
              catch (Exception)
              {
                    throw;
              }
        }

        #endregion
    }
}
