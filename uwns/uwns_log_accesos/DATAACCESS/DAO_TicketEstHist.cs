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
    public class DAO_TicketEstHist : DAO_BaseDatos
    {
        #region Fields

        public List<TicketEstHist> objListaTicketEstHist;

        #endregion

        #region Constructors

        public DAO_TicketEstHist(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTicketEstHist = new List<TicketEstHist>();
        }

        public DAO_TicketEstHist(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaTicketEstHist = new List<TicketEstHist>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTicketEstHist table.
        /// </summary>
        public bool insertar(TicketEstHist objTicketEstHist)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TICKETESTHIST_INSERT"];
                string sNombreSP = (string)hsInsertUSP["TICKETESTHIST_INSERT"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Numero_Ticket"], DbType.String, objTicketEstHist.SCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Estado"], DbType.String, objTicketEstHist.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Ticket_Estado"], DbType.String, objTicketEstHist.SDscTicketEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Equipo_Mod"], DbType.String, objTicketEstHist.SCodEquipoMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, objTicketEstHist.SDscNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTicketEstHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Fecha_Mod"], DbType.String, objTicketEstHist.SLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Hora_Mod"], DbType.String, objTicketEstHist.SLogHoraMod);


                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        //EAG
        #region Antiguo Metodo: insertarRehabilitacionTicket
        //public bool insertarRehabilitacionTicket(TicketEstHist objTicketEstHist)
        //{
        //    try
        //    {
        //        Hashtable hsInsertUSP = (Hashtable)htSPConfig["REHSP_TICKETINS"];
        //        string sNombreSP = (string)hsInsertUSP["REHSP_TICKETINS"];

        //        DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
        //        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Numero_Ticket"], DbType.String, objTicketEstHist.SCodNumeroTicket);
        //        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, objTicketEstHist.SDscNumVuelo);
        //        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTicketEstHist.SLogUsuarioMod);
        //        helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Causal_Rehabilitacion"], DbType.String, objTicketEstHist.SCausalRehabilitacion);

        //        isRegistrado = base.mantenerSP(objCommandWrapper);
        //        return isRegistrado;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        #endregion

        public bool insertarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["REHSP_TICKETINS"];
                string sNombreSP = (string)hsInsertUSP["REHSP_TICKETINS"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Numero_Tickets"], DbType.String, objTicketEstHist.SCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, (flag == 1 ? objTicketEstHist.SDscNumVuelo : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Causal_Rehabilitacion"], DbType.String, (flag == 1 ? objTicketEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelos"], DbType.String, (flag == 0 ? objTicketEstHist.SDscNumVuelo : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Causal_Rehabilitaciones"], DbType.String, (flag == 0 ? objTicketEstHist.SCausalRehabilitacion : null));
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Tipo"], DbType.String, flag.ToString());
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Log_Usuario_Mod"], DbType.String, objTicketEstHist.SLogUsuarioMod);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputListaTicket"], DbType.String, sizeOutput);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    objTicketEstHist.SCodNumeroTicket = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["OutputListaTicket"]);

                }
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }


        //EAG

        /// <summary>
        /// Updates a record in the DAOTicketEstHist table.
        /// </summary>
        public bool actualizar(TicketEstHist objTicketEstHist)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["USP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["USP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Numero_Ticket"], DbType.String, objTicketEstHist.SCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado"], DbType.String, objTicketEstHist.STipEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Ticket_Estado"], DbType.String, objTicketEstHist.SDscTicketEstado);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Equipo_Mod"], DbType.String, objTicketEstHist.SCodEquipoMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Num_Vuelo"], DbType.String, objTicketEstHist.SDscNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, objTicketEstHist.SLogUsuarioMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Fecha_Mod"], DbType.DateTime, objTicketEstHist.SLogFechaMod);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Hora_Mod"], DbType.String, objTicketEstHist.SLogHoraMod);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTicketEstHist table by its primary key.
        /// </summary>
        public bool eliminar(string sCodNumeroTicket, string sTipEstado)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Numero_Ticket"], DbType.String, sCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Tip_Estado"], DbType.String, sTipEstado);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Selects a single record from the DAOTicketEstHist table.
        /// </summary>
        public TicketEstHist obtener(string sCodNumeroTicket, string sTipEstado)
        {
            TicketEstHist objTicketEstHist = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_TICKET_EST_HIST"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_TICKET_EST_HIST"];
            result = base.listarDataReaderSP(sNombreSP, sCodNumeroTicket, sTipEstado);

            while (result.Read())
            {
                objTicketEstHist = poblar(result);

            }
            
            result.Close();
            return objTicketEstHist;
        }

        /// <summary>
        /// Selects all records from the DAOTicketEstHist table.
        /// </summary>
        public List<TicketEstHist> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaTicketEstHist.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaTicketEstHist;
        }

        /// <summary>
        /// Creates a new instance of the DAOTicketEstHist class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public TicketEstHist poblar(IDataReader dataReader)
        {

              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["THSP_SELECTALL"];
              TicketEstHist objTicketEstHist = new TicketEstHist();
              if (dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]] != DBNull.Value)
                    objTicketEstHist.SCodNumeroTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]];
              if (dataReader[(string)htSelectAllUSP["Tip_Estado"]] != DBNull.Value)
                    objTicketEstHist.STipEstado = (string)dataReader[(string)htSelectAllUSP["Tip_Estado"]];
              if (dataReader[(string)htSelectAllUSP["Dsc_Ticket_Estado"]] != DBNull.Value)
                    objTicketEstHist.SDscTicketEstado = (string)dataReader[(string)htSelectAllUSP["Dsc_Ticket_Estado"]];
              if (dataReader[(string)htSelectAllUSP["Cod_Equipo_Mod"]] != DBNull.Value)
                    objTicketEstHist.SCodEquipoMod = (string)dataReader[(string)htSelectAllUSP["Cod_Equipo_Mod"]];
              if (dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]] != DBNull.Value)
                    objTicketEstHist.SDscNumVuelo = (string)dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]];
              if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                    objTicketEstHist.SLogUsuarioMod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
              if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                    objTicketEstHist.SLogFechaMod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
              if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                    objTicketEstHist.SLogHoraMod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];

              return objTicketEstHist;
        }


        public DataTable listarTicketEstHist(string sNumTicket)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETESTHITSP_SELECT"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETESTHITSP_SELECT"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable obtenerTicketBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CNSP_TICKESBOARDINGUSADOS"];
                string sNombreSP = (string)hsSelectByIdUSP["CNSP_TICKESBOARDINGUSADOS"];
                result = base.ListarDataTableSP(sNombreSP, sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TICKETESTHITSP_ANULAR"];
                string sNombreSP = (string)hsUpdateUSP["TICKETESTHITSP_ANULAR"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Numero_Ticket"], DbType.String, sCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Motivo"], DbType.String, sDscMotivo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Log_Usuario_Mod"], DbType.String, sUsuarioMod);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TicketEstHist> listarxNumeroTicket(string Cod_Numero_Ticket)
        {
              IDataReader objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["THSP_SELECT"];
              string sNombreSP = (string)hsSelectAllUSP["THSP_SELECT"];
              objResult = base.listarDataReaderSP(sNombreSP, Cod_Numero_Ticket);

              while (objResult.Read())
              {
                    objListaTicketEstHist.Add(poblar(objResult));

              }
              
              objResult.Close();
              return objListaTicketEstHist;
        }

        #endregion
    }
}
