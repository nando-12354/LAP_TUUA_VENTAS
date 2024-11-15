using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Collections;

namespace LAP.TUUA.DAO
{
    public class DAO_Ticket : DAO_BaseDatos
    {
        #region Fields

        public List<Ticket> objListaTicket;

        #endregion

        #region Constructors

        public DAO_Ticket(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTicket = new List<Ticket>();
        }

        public DAO_Ticket(Database vhelper, Database vhelperLocal, Hashtable vhtSPConfig)
              : base(vhelper, vhelperLocal, vhtSPConfig)
        {
              objListaTicket = new List<Ticket>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTicket table.
        /// </summary>
        public bool insertar(Ticket objTicket)
        {
            try
            {
                Hashtable hsInsertUSP = objTicket.Cant_Ticket == 0 ? (Hashtable)htSPConfig["TICKETSP_INSERT"] : (Hashtable)htSPConfig["TICKETSP_INSERTOPEWEB"];
                string sNombreSP = objTicket.Cant_Ticket == 0 ? (string)hsInsertUSP["TICKETSP_INSERT"] : (string)hsInsertUSP["TICKETSP_INSERTOPEWEB"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, objTicket.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Venta_Masiva"], DbType.String, objTicket.SCodVentaMasiva);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, objTicket.SDscNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, objTicket.DtFchVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objTicket.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Venta"], DbType.String, objTicket.SCodUsuarioVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio"], DbType.Decimal, objTicket.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, objTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modalidad_Venta"], DbType.String, objTicket.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, objTicket.SCodTipoTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, objTicket.Tip_Vuelo);
                
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Contingencia"], DbType.String, objTicket.Flg_Contingencia);
                if (objTicket.Cant_Ticket > 1)
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.String, objTicket.Cant_Ticket);
                }
                else
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Referencia"], DbType.String, objTicket.Num_Referencia);
                }

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputTicket"], DbType.String, objTicket.Cant_Ticket == 0 ? 10 : 4000);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["FechaVence"], DbType.String, 8);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputIdTicket"], DbType.String, 2);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    objTicket.DtFchVencimiento = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["FechaVence"]);
                    objTicket.SCodNumeroTicket = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["OutputTicket"]);
                }
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool insertar(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont,string strNumRef, string strTipPago,string strEmpresa,string strRepte, ref string strFecVence, ref string strListaTickets)
        {
            try
            {
                Hashtable hsInsertUSP = strFecVence == Define.VENTAS ? (Hashtable)htSPConfig["TICKETSP_INSERT"] : (Hashtable)htSPConfig["TICKETSP_INSERTOPEWEB"];
                string sNombreSP = strFecVence == Define.VENTAS? (string)hsInsertUSP["TICKETSP_INSERT"] : (string)hsInsertUSP["TICKETSP_INSERTOPEWEB"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, strCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Venta_Masiva"], DbType.String, strVentaMasiva);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, strNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vuelo"], DbType.String, strFecVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, strTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Venta"], DbType.String, strUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Imp_Precio"], DbType.Decimal, decPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, strMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Modalidad_Venta"], DbType.String, strModVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, strTipTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Vuelo"], DbType.String, strTipVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Contingencia"], DbType.String, strFlagCont);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.String, intTickets);
               
                if (strFecVence == Define.VENTAS)
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Referencia"], DbType.String, strNumRef);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Pago"], DbType.String, strTipPago);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Empresa"], DbType.String, strEmpresa);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Repte"], DbType.String, strRepte);
                }
                strFecVence = "";
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputTicket"], DbType.String,4000);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["FechaVence"], DbType.String, 8);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["OutputIdTicket"], DbType.String, 2);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    strFecVence = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["FechaVence"]);
                    strListaTickets = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["OutputTicket"]);
                }
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Updates a record in the DAOTicket table.
        /// </summary>
        public bool actualizar(Ticket objTicket)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TICKET_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TICKET_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Numero_Ticket"], DbType.String, objTicket.SCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objTicket.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Venta_Masiva"], DbType.String, objTicket.SCodVentaMasiva);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Num_Vuelo"], DbType.String, objTicket.SDscNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vuelo"], DbType.String, objTicket.DtFchVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado_Actual"], DbType.String, objTicket.STipEstadoActual);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Creacion"], DbType.String, objTicket.DtFchCreacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objTicket.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario_Venta"], DbType.String, objTicket.SCodUsuarioVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Precio"], DbType.Double, objTicket.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Moneda"], DbType.String, objTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vencimiento"], DbType.String, objTicket.DtFchVencimiento);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objTicket.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rehabilitaciones"], DbType.Int32, objTicket.INumRehabilitaciones);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Tipo_Ticket"], DbType.String, objTicket.SCodTipoTicket);
                
                helper.AddInParameter(objCommandWrapper, "Dsc_Motivo", DbType.String, objTicket.Dsc_Motivo);
                helper.AddInParameter(objCommandWrapper, "Tip_Anula", DbType.String, objTicket.Tip_Anula);
                helper.AddInParameter(objCommandWrapper, "Cod_Pto_Venta", DbType.String, objTicket.Cod_Pto_Venta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Sincroniza"], DbType.String, objTicket.Flg_Sincroniza);

                isActualizado = base.mantenerSP2(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Updates a record in the DAOTicket table.
        /// </summary>
        public bool actualizarOffLine(Ticket objTicket)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TICKET_UPDATE_OFFLINE"];
                string sNombreSP = (string)hsUpdateUSP["TICKET_UPDATE_OFFLINE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Numero_Ticket"], DbType.String, objTicket.SCodNumeroTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Compania"], DbType.String, objTicket.SCodCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Venta_Masiva"], DbType.String, objTicket.SCodVentaMasiva);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Dsc_Num_Vuelo"], DbType.String, objTicket.SDscNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vuelo"], DbType.String, objTicket.DtFchVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Tip_Estado_Actual"], DbType.String, objTicket.STipEstadoActual);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Creacion"], DbType.String, objTicket.DtFchCreacion);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objTicket.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario_Venta"], DbType.String, objTicket.SCodUsuarioVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Imp_Precio"], DbType.Double, objTicket.DImpPrecio);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Moneda"], DbType.String, objTicket.SCodMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Fch_Vencimiento"], DbType.String, objTicket.DtFchVencimiento);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Modalidad_Venta"], DbType.String, objTicket.SCodModalidadVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Num_Rehabilitaciones"], DbType.Int32, objTicket.INumRehabilitaciones);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Tipo_Ticket"], DbType.String, objTicket.SCodTipoTicket);

                helper.AddInParameter(objCommandWrapper, "Dsc_Motivo", DbType.String, objTicket.Dsc_Motivo);
                helper.AddInParameter(objCommandWrapper, "Tip_Anula", DbType.String, objTicket.Tip_Anula);
                helper.AddInParameter(objCommandWrapper, "Cod_Pto_Venta", DbType.String, objTicket.Cod_Pto_Venta);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Flg_Sincroniza"], DbType.String, objTicket.Flg_Sincroniza);

                isActualizado = base.mantenerSP2(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        /// <summary>
        /// Deletes a record from the DAOTicket table by its primary key.
        /// </summary>
        public bool eliminar(string sCodNumeroTicket)
        {

            try
            {
                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["USP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["USP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Numero_Ticket"], DbType.String, sCodNumeroTicket);

                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Selects a single record from the DAOTicket table.
        /// </summary>
        public Ticket obtener(string sCodNumeroTicket)
        {
            Ticket objTicket = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TSP_OBTENER_TICKET"];
            string sNombreSP = (string)hsSelectByIdUSP["TSP_OBTENER_TICKET"];
            result = base.listarDataReaderSP(sNombreSP, sCodNumeroTicket);

            while (result.Read())
            {
                objTicket = poblar(result);

            }
            
            result.Close();
            return objTicket;
        }

        /// <summary>
        /// Selects all records from the DAOTicket table.
        /// </summary>
        public List<Ticket> listar()
        {
            IDataReader objResult;
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["USP_SELECTALL"];
            string sNombreSP = (string)hsSelectAllUSP["USP_SELECTALL"];
            objResult = base.listarDataReaderSP(sNombreSP, null);

            while (objResult.Read())
            {
                objListaTicket.Add(poblar(objResult));

            }
            
            objResult.Close();
            return objListaTicket;
        }


        public List<Ticket> listarxEstado()
        {
              IDataReader objResult;
              Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["TSP_SELECT"];
              string sNombreSP = (string)hsSelectAllUSP["TSP_SELECT"];
              objResult = base.listarDataReaderSP(sNombreSP, null);

              while (objResult.Read())
              {
                    objListaTicket.Add(poblarxEstado(objResult));

              }
              
              objResult.Close();
              return objListaTicket;
        }


        public Ticket poblarxEstado(IDataReader dataReader)
        {
              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TSP_SELECT"];
              Ticket objTicket = new Ticket();
              if (dataReader.FieldCount == 0)
              {
                    return null;
              }
              else
              {
                    try
                    {
                          if (dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]] != DBNull.Value)
                                objTicket.SCodNumeroTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                                objTicket.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Venta_Masiva"]] != DBNull.Value)
                                objTicket.SCodVentaMasiva = (string)dataReader[(string)htSelectAllUSP["Cod_Venta_Masiva"]];
                          if (dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]] != DBNull.Value)
                                objTicket.SDscNumVuelo = (string)dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vuelo"]] != DBNull.Value)
                                objTicket.DtFchVuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]] != DBNull.Value)
                                objTicket.STipEstadoActual = (string)dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Creacion"]] != DBNull.Value)
                                objTicket.DtFchCreacion = (string)dataReader[(string)htSelectAllUSP["Fch_Creacion"]];
                          if (dataReader[(string)htSelectAllUSP["Hor_Creacion"]] != DBNull.Value)
                                objTicket.SHor_Creacion = (string)dataReader[(string)htSelectAllUSP["Hor_Creacion"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Turno"]] != DBNull.Value)
                                objTicket.SCodTurno = (string)dataReader[(string)htSelectAllUSP["Cod_Turno"]];
                          if (dataReader[(string)htSelectAllUSP["Imp_Precio"]] != DBNull.Value)
                                objTicket.DImpPrecio = (decimal)dataReader[(string)htSelectAllUSP["Imp_Precio"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                                objTicket.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]] != DBNull.Value)
                                objTicket.DtFchVencimiento = (string)dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)
                                objTicket.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
                          if (dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]] != DBNull.Value)
                                objTicket.INumRehabilitaciones = (Int32)dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                                objTicket.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];
                    }
                    catch (Exception e)
                    {
                          return null;
                    }
              }
              return objTicket;
        }

        public DataTable consultarDetalleTicket(string sNumTicket, string sTicketDesde, string sTicketHasta)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLETICKET"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLETICKET"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket, sTicketDesde, sTicketHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public DataTable consultarTicketxFecha(string sFchDesde, string sFchHasta, string sCodCompania, string sTipoTicket, string sEstadoTicket, string sTipoPersona, string sCadFiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta, string strTurno)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_TICKETXFECHA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_TICKETXFECHA"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sCodCompania, sTipoTicket, sEstadoTicket, sTipoPersona, sCadFiltro, sOrdenacion, sHoraDesde, sHoraHasta, strTurno);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad,string strUsuario,string strMotivo, ref string strMessage)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TICKETSP_EXTORNAR"];
                string sNombreSP = (string)hsInsertUSP["TICKETSP_EXTORNAR"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Tickets"], DbType.String, strListaTickets);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, strTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.Int32, intCantidad);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, strUsuario);
                helper.AddInParameter(objCommandWrapper, "Dsc_Motivo", DbType.String, strMotivo);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    strMessage = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]);
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a new instance of the DAOTicket class and populates it with data from the specified SqlDataReader.
        /// </summary>
        public Ticket poblar(IDataReader dataReader)
        {

              Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TICKETSP_SELECTALL"];
              Ticket objTicket = new Ticket();
              if (dataReader.FieldCount == 0)
              {
                    return null;
              }
              else
              {
                    try
                    {
                          if (dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]] != DBNull.Value)
                                objTicket.SCodNumeroTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Numero_Ticket"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Compania"]] != DBNull.Value)
                                objTicket.SCodCompania = (string)dataReader[(string)htSelectAllUSP["Cod_Compania"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Venta_Masiva"]] != DBNull.Value)
                                objTicket.SCodVentaMasiva = (string)dataReader[(string)htSelectAllUSP["Cod_Venta_Masiva"]];
                          if (dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]] != DBNull.Value)
                                objTicket.SDscNumVuelo = (string)dataReader[(string)htSelectAllUSP["Dsc_Num_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vuelo"]] != DBNull.Value)
                                objTicket.DtFchVuelo = (string)dataReader[(string)htSelectAllUSP["Fch_Vuelo"]];
                          if (dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]] != DBNull.Value)
                                objTicket.STipEstadoActual = (string)dataReader[(string)htSelectAllUSP["Tip_Estado_Actual"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Creacion"]] != DBNull.Value)
                                objTicket.DtFchCreacion = (string)dataReader[(string)htSelectAllUSP["Fch_Creacion"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Creacion"]] != DBNull.Value)
                              objTicket.DtFchCreacion = (string)dataReader[(string)htSelectAllUSP["Fch_Creacion"]];
                          if (dataReader[(string)htSelectAllUSP["Hor_Creacion"]] != DBNull.Value)
                              objTicket.SHor_Creacion = (string)dataReader[(string)htSelectAllUSP["Hor_Creacion"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Turno"]] != DBNull.Value)
                                objTicket.SCodTurno = (string)dataReader[(string)htSelectAllUSP["Cod_Turno"]];
                          if (dataReader[(string)htSelectAllUSP["Imp_Precio"]] != DBNull.Value)
                                objTicket.DImpPrecio = (decimal)dataReader[(string)htSelectAllUSP["Imp_Precio"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Moneda"]] != DBNull.Value)
                                objTicket.SCodMoneda = (string)dataReader[(string)htSelectAllUSP["Cod_Moneda"]];
                          if (dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]] != DBNull.Value)
                                objTicket.DtFchVencimiento = (string)dataReader[(string)htSelectAllUSP["Fch_Vencimiento"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]] != DBNull.Value)
                                objTicket.SCodModalidadVenta = (string)dataReader[(string)htSelectAllUSP["Cod_Modalidad_Venta"]];
                          if (dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]] != DBNull.Value)
                                objTicket.INumRehabilitaciones = (Int32)dataReader[(string)htSelectAllUSP["Num_Rehabilitaciones"]];
                          if (dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]] != DBNull.Value)
                                objTicket.SCodTipoTicket = (string)dataReader[(string)htSelectAllUSP["Cod_Tipo_Ticket"]];

                          if (dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]] != DBNull.Value)
                                objTicket.SLog_Usuario_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Usuario_Mod"]];
                          if (dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]] != DBNull.Value)
                                objTicket.SLog_Fecha_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Fecha_Mod"]];
                          if (dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]] != DBNull.Value)
                                objTicket.SLog_Hora_Mod = (string)dataReader[(string)htSelectAllUSP["Log_Hora_Mod"]];
                          if (dataReader["Dsc_Ticket_Estado"] != DBNull.Value)
                              objTicket.Dsc_Estado_Actual = (string)dataReader["Dsc_Ticket_Estado"];


                    }
                    catch (Exception e)
                    {
                          return null;
                    }
              }
              return objTicket;
        }

        public DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["REHSP_CONSTICKETVUELOS"];
                string sNombreSP = (string)hsSelectByIdUSP["REHSP_CONSTICKETVUELOS"];
                result = base.ListarDataTableSP(sNombreSP, sCompania, fechaVuelo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["REHSP_CONSTICKETSXVUELO"];
                string sNombreSP = (string)hsSelectByIdUSP["REHSP_CONSTICKETSXVUELO"];
                result = base.ListarDataTableSP(sNombreSP, sCompania, fechaVuelo, dsc_Num_Vuelo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataTable obtenerCuadreTicketsEmitidos(string sFechaDesde, string sFechaHasta,string sTipoDocumento, string sFlagAnulados)
        {
            try
            {   DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CNSP_CUADRETICKETEMITIDOS"];
                string sNombreSP = (string)hsSelectByIdUSP["CNSP_CUADRETICKETEMITIDOS"];
                result = base.ListarDataTableSP(sNombreSP, sFechaDesde, sFechaHasta, sTipoDocumento,sFlagAnulados);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKET_VENTA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKET_VENTA"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin,strTipVenta,strFlgAero);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CMONTES PARA GENERAR EL REPORTE DE MOVIMIENTO DE TICKET CONTINGENCIA
        public DataTable consultarMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_MOVIMIENTOTICKETCONTINGENCIA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_MOVIMIENTOTICKETCONTINGENCIA"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sEstado, sTipoTicket, sEstadoTicket, sRangoMinTicket, sRangoMaxTicket);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //CMONTES PARA GENERAR EL REPORTE DE RESUMEN DE STOCK DE TICKET CONTINGENCIA
        public DataTable consultarResumenStockTicketContingencia(string sTipoTicket, string sFechaAl)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENSTOCKTICKETCONTINGENCIA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENSTOCKTICKETCONTINGENCIA"];
                result = base.ListarDataTableSP(sNombreSP, sTipoTicket, sFechaAl);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket)
        {
              try
              {
                    DataTable result;
                    Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS"];
                    string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS"];
                    result = base.ListarDataTableSP(sNombreSP, sCodCompania, sNumVuelo, sTipoDocumento, sTipoTicket);
                    return result;
              }
              catch (Exception ex)
              {
                    throw ex;
              }

        }

        //CMONTES PARA GENERAR EL REPORTE DE RESUMEN DE TICKET VENDIDOS AL CREDITO
        public DataTable consultarResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENTICKETVENDIDOSCREDITO"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENTICKETVENDIDOSCREDITO"];
                result = base.ListarDataTableSP(sNombreSP, sTipoTicket, sFechaInicial, sFechaFinal, sAeroLinea, sNumVuelo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CMONTES PARA GENERAR EL REPORTE DE RESUMEN DE RECAUDACION MENSUAL
        public DataTable consultarRecaudacionMensual(string sAnio)
        {
              try
              {
                    DataTable result;
                    Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENRECAUDACIONMENSUAL"];
                    string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENRECAUDACIONMENSUAL"];
                    result = base.ListarDataTableSP(sNombreSP, sAnio);
                    return result;
              }
              catch (Exception ex)
              {
                    throw ex;
              }
        }

        public DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_LIQUIDVENTA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_LIQUIDVENTA"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin,string strTipoTicket)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_VENCIDOS"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_VENCIDOS"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin, strTipoTicket);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //CMONTES
        public DataTable consultarDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLEVENTACOMPANIA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLEVENTACOMPANIA"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_USOCONTI"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_USOCONTI"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TICKETSP_VENTACONTI"];
                string sNombreSP = (string)hsInsertUSP["TICKETSP_VENTACONTI"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Compania"], DbType.String, strCompania);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Num_Vuelo"], DbType.String, strNumVuelo);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Venta"], DbType.String, strUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Moneda"], DbType.String, strMoneda);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Tipo_Ticket"], DbType.String, strTipTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Venta"], DbType.String, strFecVenta);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.Int32, intTickets);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Tickets"], DbType.String, strListaTickets);
                
                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ListarContingencia(string strTipTikcet,string strNumIni,string strNumFin,string strUsuario)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_CONTISEL"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_CONTISEL"];
                result = base.ListarDataTableSP(sNombreSP, strTipTikcet, strNumIni,strNumFin, strUsuario);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //CMONTES
        public DataTable consultarLiquidacionVenta(string sFchDesde, string sFchHasta)
        {

            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_LIQUIDACIONVENTA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_LIQUIDACIONVENTA"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //CMONTES
        public DataTable consultarLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {

            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_LIQUIDACIONVENTARESUMEN"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_LIQUIDACIONVENTARESUMEN"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable consultarTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETBOARDINGSP_REHABILITACION"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETBOARDINGSP_REHABILITACION"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sDocumento, sTicket, sAerolinea, sVuelo, sMotivo);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
    }
}