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
    public class DAO_BoardingBcbp : DAO_BaseDatos   
    {
        #region Fields

        public List<BoardingBcbp> objListaBoardingBcbp;

        #endregion

        #region Constructors

         public DAO_BoardingBcbp(string sConfigSPPath)
               : base(sConfigSPPath)
        {
            objListaBoardingBcbp = new List<BoardingBcbp>();
        }

         public DAO_BoardingBcbp(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
               : base( vhelper,vhelperLocal,  vhtSPConfig)
         {
               objListaBoardingBcbp = new List<BoardingBcbp>();
         }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOBoardingBcbp table.
        /// </summary>

        public bool insertar(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["BOARDINGSP_INSERT"];
                
                  string sNombreSP = (string)hsInsertUSP["BOARDINGSP_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objBoardingBcbp.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Vuelo"], DbType.String, objBoardingBcbp.SNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objBoardingBcbp.StrFchVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Asiento"], DbType.String, objBoardingBcbp.StrNumAsiento);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Nom_Pasajero"], DbType.String, objBoardingBcbp.StrNomPasajero);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Trama_Bcbp"], DbType.String, objBoardingBcbp.StrTrama);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objBoardingBcbp.StrLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objBoardingBcbp.StrLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objBoardingBcbp.StrLogHoraMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Ingreso"], DbType.String, objBoardingBcbp.StrTipIngreso);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objBoardingBcbp.StrTip_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Equipo_Mod"], DbType.String, objBoardingBcbp.StrCod_Equipo_Mod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Boarding_Estado"], DbType.String, objBoardingBcbp.StrDsc_Boarding_Estado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modulo_Mod"], DbType.String, objBoardingBcbp.StrCodModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_SubModulo_Mod"], DbType.String, objBoardingBcbp.StrCodSubModulo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Rol"], DbType.String, objBoardingBcbp.StrRol);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Unico_Bcbp"], DbType.String, objBoardingBcbp.StrCodUnicoBcbp);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Unico_Bcbp_Rel"], DbType.String, objBoardingBcbp.StrCodUnicoBcbpRel);
                helper.AddInParameter(objCommandWrapper, "Flg_Sincroniza", DbType.String, objBoardingBcbp.StrFlg_Sincroniza);
                helper.AddInParameter(objCommandWrapper, "Tip_Pasajero", DbType.String, objBoardingBcbp.Tip_Pasajero);
                helper.AddInParameter(objCommandWrapper, "Tip_Vuelo", DbType.String, objBoardingBcbp.Tip_Vuelo);
                helper.AddInParameter(objCommandWrapper, "Tip_Transferencia", DbType.String, objBoardingBcbp.Tip_Transferencia);
                helper.AddInParameter(objCommandWrapper, "Dsc_Destino", DbType.String, objBoardingBcbp.Dsc_Destino);
                helper.AddInParameter(objCommandWrapper, "Cod_Eticket", DbType.String, objBoardingBcbp.Cod_Eticket);
                helper.AddInParameter(objCommandWrapper, "Nro_Boarding", DbType.String, objBoardingBcbp.Nro_boarding);

                helper.AddInParameter(objCommandWrapper, "Flg_WSError", DbType.String, objBoardingBcbp.Flg_WSError);
                helper.AddInParameter(objCommandWrapper, "Flg_Incluye_Tuua", DbType.String, objBoardingBcbp.Flg_Pago);
                helper.AddInParameter(objCommandWrapper, "Dsc_Observacion", DbType.String, objBoardingBcbp.Dsc_Observacion);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<BoardingBcbp> listarxEstado()
        {
              IDataReader objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BSP_SELECT"];
              string sNombreSP = (string)hsSelectAllUSP["BSP_SELECT"];
              objResult = base.listarDataReaderSP(sNombreSP, null);

              while (objResult.Read())
              {
                    objListaBoardingBcbp.Add(poblarxEstado(objResult));

              }
              
              objResult.Close();
              return objListaBoardingBcbp;
        }

        public BoardingBcbp poblarxEstado(IDataReader dataReader)
        {
              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["BSP_SELECT"];
              BoardingBcbp objBoarding = new BoardingBcbp();
              if (dataReader.FieldCount == 0)
              {
                    return null;
              }
              else
              {
                    try
                    {
                          if (dataReader[(string)htSelectAllUSP["Num_Secuencial_Bcbp"]] != DBNull.Value)
                                objBoarding.INumSecuencial = Int32.Parse(dataReader[(string)htSelectAllUSP["Num_Secuencial_Bcbp"]].ToString());
                          if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                                objBoarding.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
                          if (dataReader[(string)htSelectAllUSP["Num_Vuelo"]] != DBNull.Value)
                                objBoarding.SNumVuelo = (string)dataReader[(string)htSelectAllUSP["Num_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vuelo"]] != DBNull.Value)
                                objBoarding.StrFchVuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Num_Asiento"]] != DBNull.Value)
                                objBoarding.StrNumAsiento = (string)dataReader[(string)htSelectAllUSP["Num_Asiento"]];
                          if (dataReader[(string)htSelectAllUSP["Nom_Pasajero"]] != DBNull.Value)
                                objBoarding.StrNomPasajero = (string)dataReader[(string)htSelectAllUSP["Nom_Pasajero"]];
                          if (dataReader[(string)htSelectAllUSP["Dsc_Trama_Bcbp"]] != DBNull.Value)
                                objBoarding.StrTrama = (string)dataReader[(string)htSelectAllUSP["Dsc_Trama_Bcbp"]];
                          if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                                objBoarding.StrLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
                          if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                                objBoarding.StrLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
                          if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                                objBoarding.StrLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
                          if (dataReader[(string)htSelectAllUSP["Tip_Ingreso"]] != DBNull.Value)
                                objBoarding.StrTipIngreso = (string)dataReader[(string)htSelectAllUSP["Tip_Ingreso"]];
                          if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                                objBoarding.StrTip_Estado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]] != DBNull.Value)
                                objBoarding.StrFch_Vencimiento = (DateTime)dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]];

                    }
                    catch (Exception e)
                    {
                          return null;
                    }
              }
              return objBoarding;
        }
        /// <summary>
        /// Updates a record in the DAOBoardingBcbp table.
        /// </summary>

        public bool actualizar(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Secuencial"], DbType.Int32, objBoardingBcbp.INumSecuencial);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objBoardingBcbp.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Vuelo"], DbType.String, objBoardingBcbp.SNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vuelo"], DbType.String, objBoardingBcbp.StrFchVuelo);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool actualizarEstado(BoardingBcbp objBoardingBcbp)
        {
              try
              {
                    Hashtable hsUpdateUSP = (Hashtable)htSPConfig["BSP_UPDATE"];
                    string sNombreSP = (string)hsUpdateUSP["BSP_UPDATE"];

                    DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Secuencial_Bcbp"], DbType.Int32, objBoardingBcbp.INumSecuencial);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objBoardingBcbp.SCodCompania);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Vuelo"], DbType.String, objBoardingBcbp.SNumVuelo);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vuelo"], DbType.String, objBoardingBcbp.StrFchVuelo);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Asiento"], DbType.String, objBoardingBcbp.StrNumAsiento);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Pasajero"], DbType.String, objBoardingBcbp.StrNomPasajero);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Trama_Bcbp"], DbType.String, objBoardingBcbp.StrTrama);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objBoardingBcbp.StrLogUsuarioMod);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Ingreso"], DbType.String, objBoardingBcbp.StrTipIngreso);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objBoardingBcbp.StrTip_Estado);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Equipo_Mod"], DbType.String, objBoardingBcbp.StrCod_Equipo_Mod);
                    helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Boarding_Estado"], DbType.String, objBoardingBcbp.StrDsc_Boarding_Estado);
                    helper.AddInParameter(objCommandWrapper, "Flg_Sincroniza", DbType.String, objBoardingBcbp.StrFlg_Sincroniza);

                    isActualizado = base.mantenerSP(objCommandWrapper);
                    return isActualizado;
              }
              catch (Exception)
              {
                    throw;
              }
        }


        /// <summary>
        /// Deletes a record from the DAOBoardingBcbp table by its primary key.
        /// </summary>

        public bool eliminar(int iNumSecuencial)
        {
            try
            {
                Hashtable htDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)htDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)htDeleteUSP["Num_Secuencial"], DbType.Int32, iNumSecuencial);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }




        /// <summary>
        /// Selects all records from the objPrecioTicket table.
        /// </summary>
        public List<BoardingBcbp> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaBoardingBcbp.Add(poblar(objResult));

            }

            objResult.Close();
            return objListaBoardingBcbp;
        }


        /// <summary>
        /// Selects a single record from the DAOBoardingBcbp table.
        /// </summary>
       public BoardingBcbp obtener(string strCod_Compania, string strNum_Vuelo, string strFechVuelo,string strNumeroAsiento,
                                      string strNomPasajero, string strtipEstado, string strCodUnicoBcbp, string strNumSecuencialBcbp)
        {

            BoardingBcbp objBoardingBcbp = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BPSP_OBTENER_BOARDING"];
            string strNombreSP = (string)hsSelectByIdUSP["BPSP_OBTENER_BOARDING"];
            result = base.listarDataReaderSP(strNombreSP, strCod_Compania, strNum_Vuelo,strFechVuelo,strNumeroAsiento,strNomPasajero,
                                             strtipEstado,strCodUnicoBcbp,strNumSecuencialBcbp);

            while (result.Read())
            {
                objBoardingBcbp = poblar(result);

            }
            
            result.Close();
            return objBoardingBcbp;
        }         

  
        public BoardingBcbp obtener(String strCod_Compania, String strNum_Vuelo, String strFechVuelo,String strNumeroAsiento,
                                      String strNomPasajero)
        {

            BoardingBcbp objBoardingBcbp = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BPSP_OBTENER_BOARDING"];
            string strNombreSP = (string)hsSelectByIdUSP["BPSP_OBTENER_BOARDING"];
            result = base.listarDataReaderSP(strNombreSP, strCod_Compania, strNum_Vuelo,strFechVuelo,strNumeroAsiento,strNomPasajero,null,null);

            while (result.Read())
            {
                objBoardingBcbp = poblar(result);

            }
            
            result.Close();
            return objBoardingBcbp;
        }

        public BoardingBcbp obtener(string strCodCompania, string strCodUnicoBCBP)
        {

              BoardingBcbp objBoardingBcbp = null;
              IDataReader result;
              Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BPUNICOSP_OBTENER_BOARDING"];
              string strNombreSP = (string)hsSelectByIdUSP["BPUNICOSP_OBTENER_BOARDING"];
              result = base.listarDataReaderSP(strNombreSP, strCodCompania, strCodUnicoBCBP);

              while (result.Read())
              {
                    objBoardingBcbp = poblar(result);

              }
              
              result.Close();
              return objBoardingBcbp;
        }


          /// <summary>
          /// Selects a single record from the DAOBoardingBcbp table.
          /// </summary>

          public DataTable DetalleBoarding(string strCod_Compania, string strNum_Vuelo, string strFechVuelo, string strNumeroAsiento,
                                      string strNomPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp) 
          {
              DataTable objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_OBTENER_BOARDING"];
              string sNombreSP = (string)hsSelectAllUSP["BPSP_OBTENER_BOARDING"];
              objResult = base.ListarDataTableSP(sNombreSP, strCod_Compania, strNum_Vuelo, strFechVuelo, strNumeroAsiento, strNomPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp);
              return objResult;
          }


          /// <summary>
          /// Selects a single record from the DAOBoardingBcbp table.
          /// </summary>

          public DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni,string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding,string strFlagResumen)
          {
              DataTable objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_BOARDING_LEIDOS_MOLINETE"];
              string sNombreSP = (string)hsSelectAllUSP["BPSP_BOARDING_LEIDOS_MOLINETE"];
              objResult = base.ListarDataTableSP(sNombreSP,strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni,strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen);
              return objResult;
          }



          public bool AnularBCBP(BoardingBcbp objBoardingBcbp)
          {
              try
              {
                  Hashtable hsUpdateUSP = (Hashtable)htSPConfig["BPSP_ANULAR"];
                  string sNombreSP = (string)hsUpdateUSP["BPSP_ANULAR"];

                  DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objBoardingBcbp.SCodCompania);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Vuelo"], DbType.String, objBoardingBcbp.SNumVuelo);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vuelo"], DbType.String, objBoardingBcbp.StrFchVuelo);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Asiento"], DbType.String, objBoardingBcbp.StrNumAsiento);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Nom_Pasajero"], DbType.String, objBoardingBcbp.StrNomPasajero);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objBoardingBcbp.StrLogUsuarioMod);
                  helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Motivo"], DbType.String, objBoardingBcbp.StrMotivo);
                  isActualizado = base.mantenerSP(objCommandWrapper);
                  return isActualizado;
              }
              catch (Exception ex)
              {
                  throw ex;
              }
          }


        /// <summary>
        /// Creates a new instance of the DAOBoardingBcbp class and populates it with data from the specified SqlDataReader.
        /// </summary>

        public BoardingBcbp poblar(IDataReader dataReader)
        {
            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["BOARDINGSP_SELECTALL"];
            BoardingBcbp objBoardingBcbp = new BoardingBcbp();
            if (dataReader[(string)htSelectAllUSP["Num_Secuencial_Bcbp"]] != DBNull.Value)
                  objBoardingBcbp.INumSecuencial = Int32.Parse(dataReader[(string)htSelectAllUSP["Num_Secuencial_Bcbp"]].ToString());
            if (dataReader[(string)htSelectAllUSP["Cod_Compania"]]!=DBNull.Value) 
                  objBoardingBcbp.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
            if (dataReader[(string)htSelectAllUSP["Num_Vuelo"]] != DBNull.Value) 
                  objBoardingBcbp.SNumVuelo = (string)dataReader[(string)htSelectAllUSP["Num_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Fch_Vuelo"]] != DBNull.Value) 
                  objBoardingBcbp.StrFchVuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
            if (dataReader[(string)htSelectAllUSP["Num_Asiento"]] != DBNull.Value) 
                  objBoardingBcbp.StrNumAsiento = (string)dataReader[(string)htSelectAllUSP["Num_Asiento"]];
            if (dataReader[(string)htSelectAllUSP["Nom_Pasajero"]] != DBNull.Value) 
                  objBoardingBcbp.StrNomPasajero = (string)dataReader[(string)htSelectAllUSP["Nom_Pasajero"]];
            if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                  objBoardingBcbp.StrLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                  objBoardingBcbp.StrLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                  objBoardingBcbp.StrLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Ingreso"]] != DBNull.Value)
                  objBoardingBcbp.StrTipIngreso = (string)dataReader[(string)htSelectAllUSP["Tip_Ingreso"]];
            if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                  objBoardingBcbp.StrTip_Estado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
            if (dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]] != DBNull.Value)
                  objBoardingBcbp.INum_Rehabilitaciones = Int32.Parse(dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]].ToString());
            if (dataReader[(string)htSelectAllUSP["Cod_Unico_Bcbp"]] != DBNull.Value)
                  objBoardingBcbp.StrCodUnicoBcbp = (string)dataReader[(string)htSelectAllUSP["Cod_Unico_Bcbp"]];
            if (dataReader["Dsc_BCBP_Estado"] != DBNull.Value)
                objBoardingBcbp.Dsc_Bcbp_Estado = (string)dataReader["Dsc_BCBP_Estado"];
            if (dataReader["Tip_Vuelo"] != DBNull.Value)
                objBoardingBcbp.Tip_Vuelo = (string)dataReader["Tip_Vuelo"];
            if (dataReader["Flg_Tipo_Bcbp"] != DBNull.Value)
                objBoardingBcbp.Flg_Tipo = (string)dataReader["Flg_Tipo_Bcbp"];
            if (dataReader["Num_Secuencial_Bcbp_Rel"] != DBNull.Value)
                objBoardingBcbp.Num_Secuencial_Bcbp_Rel = Int32.Parse(dataReader["Num_Secuencial_Bcbp_Rel"].ToString());
            if (dataReader["Num_Secuencial_Bcbp_Rel_Sec"] != DBNull.Value)
                objBoardingBcbp.Num_Secuenial_Rel_Sec = Int32.Parse(dataReader["Num_Secuencial_Bcbp_Rel_Sec"].ToString());
            if (dataReader["Num_Vuelo_Rel"] != DBNull.Value)
                objBoardingBcbp.Num_Vuelo_Rel = (string)dataReader["Num_Vuelo_Rel"];

            if (dataReader["Tip_Pasajero"] != DBNull.Value)
                objBoardingBcbp.Tip_Pasajero = (string)dataReader["Tip_Pasajero"];
            if (dataReader["Tip_Transferencia"] != DBNull.Value)
                objBoardingBcbp.Tip_Transferencia = (string)dataReader["Tip_Transferencia"];
            if (dataReader["Dsc_Destino"] != DBNull.Value)
                objBoardingBcbp.Dsc_Destino = (string)dataReader["Dsc_Destino"];
            if (dataReader["Cod_Eticket"] != DBNull.Value)
                objBoardingBcbp.Cod_Eticket = (string)dataReader["Cod_Eticket"];
            if (dataReader["Nro_Boarding"] != DBNull.Value)
                objBoardingBcbp.Nro_boarding = (string)dataReader["Nro_Boarding"];
            if (dataReader["Flg_Bloqueado"] != DBNull.Value)
                objBoardingBcbp.Flg_Bloqueado = (string)dataReader["Flg_Bloqueado"];
            return objBoardingBcbp;
        }

        public DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["REHSP_CONSBCBPVUELOS"];
                string sNombreSP = (string)hsSelectByIdUSP["REHSP_CONSBCBPVUELOS"];
                result = base.ListarDataTableSP(sNombreSP, sCompania, fechaVuelo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable obteneterBoardingsByRangoFechas(string sFchDesde, string sFchHasta)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BCBPSP_SELBYFECHA"];
                string sNombreSP = (string)hsSelectByIdUSP["BCBPSP_SELBYFECHA"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public BoardingBcbp obtener(String strNum_Vuelo, String strFechVuelo, String strNumeroAsiento,
                                  String strNomPasajero)
        {

              BoardingBcbp objBoardingBcbp = null;
              IDataReader result;
              Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["BPSP_OBTENER_BOARDING"];
              string strNombreSP = (string)hsSelectByIdUSP["BPSP_OBTENER_BOARDING"];
              result = base.listarDataReaderSP(strNombreSP, null, strNum_Vuelo, strFechVuelo, strNumeroAsiento, strNomPasajero, null, null,null);

              while (result.Read())
              {
                    objBoardingBcbp = poblar(result);

              }
              
              result.Close();
              return objBoardingBcbp;
        }
        #endregion

        
  }
}
