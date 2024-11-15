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
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["EmpresaRecaudadora"], DbType.String, objTicket.SEmpresaRecaudadora);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_Contingencia"], DbType.String, objTicket.Flg_Contingencia);
                if (objTicket.Cant_Ticket > 1)
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.String, objTicket.Cant_Ticket);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Met_Pago"], DbType.String, objTicket.SMetPago);
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

        public bool insertar(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, 
                             string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, 
                             string strTipVuelo, int intTickets, string strFlagCont,string strNumRef, string strTipPago, string strEmpresa,
                             string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio, string strMetPago, string strEmpresaRecaudadora) //FL.
        {
            try
            {
                Hashtable hsInsertUSP = strFecVence == Define.VENTAS ? (Hashtable)htSPConfig["TICKETSP_INSERT"] : (Hashtable)htSPConfig["TICKETSP_INSERTOPEWEB"];
                string sNombreSP = strFecVence == Define.VENTAS? (string)hsInsertUSP["TICKETSP_INSERT"] : (string)hsInsertUSP["TICKETSP_INSERTOPEWEB"];
                string strTurnoWeb=null;
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
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Empresa"], DbType.String, strEmpresa);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Repte"], DbType.String, strRepte);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["EmpresaRecaudadora"], DbType.String, strEmpresaRecaudadora);



                if (strFecVence == Define.VENTAS)
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Referencia"], DbType.String, strNumRef);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Tip_Pago"], DbType.String, strTipPago);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Met_Pago"], DbType.String, strMetPago);
                }
                else
                {
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Ing"], DbType.String, strCodTurnoIng);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flg_CierreTurno"], DbType.String, strFlgCierreTurno);
                    helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Precio_Ticket"], DbType.String, strCodPrecio);
                    helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Web"], DbType.String, 6);
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
                    if (objCommandWrapper.Parameters.IndexOf("@Cod_Turno_Web") != -1)
                    {
                        strTurnoWeb=(string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Web"]);
                        if (strTurnoWeb.Trim() != "")
                        {
                            strFecVence += "#" + strTurnoWeb;
                        }
                    }
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
                
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool actualizarTransaccion(Ticket objTicket, bool transaccion)
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

                isActualizado = base.ejecutarTrxSP(objCommandWrapper, transaccion);
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
            result.Dispose();
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
            objResult.Dispose();
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
              objResult.Dispose();
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
        
              public DataTable consultarHistTicket(string CodNumeroTicket)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETESTHITSP_ARCH_SELECT"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETESTHITSP_ARCH_SELECT"];
                result = base.ListarDataTableSP(sNombreSP, CodNumeroTicket);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

        public DataTable consultarDetalleTicketPagin(string sNumTicket, string sTicketDesde, string sTicketHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLETICKETPAGIN"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLETICKETPAGIN"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket, sTicketDesde, sTicketHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable consultarDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_OPEDETALLETICKET"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_OPEDETALLETICKET"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket, sTicketDesde, sTicketHasta, stipoticket, sTickets_Sel, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable consultarDetalleTicket_Reh(string sNumTicket, string sTicketDesde, string sTicketHasta)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLETICKET_REH"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLETICKET_REH"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket, sTicketDesde, sTicketHasta);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable ConsultaDetalleTicket2_Reh(string sNumTicket, string sTicketDesde, string sTicketHasta, string sFlgTotal)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLETICKET_REH2"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLETICKET_REH2"];
                result = base.ListarDataTableSP(sNombreSP, sNumTicket, sTicketDesde, sTicketHasta, sFlgTotal);
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

        public DataTable ListarTicketsExtorno(string sCod_Ticket, string sTicket_Desde, string sTicket_Hasta, string sCod_Turno, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_TICKETXEXTORNO"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_TICKETXEXTORNO"];

                result = base.ListarDataTableSP(sNombreSP, sCod_Ticket, sTicket_Desde, sTicket_Hasta, sCod_Turno, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable consultarTicketxFechaPagin(string sTipoDoc,
                                                    string sFchDesde,
                                                    string sFchHasta,
                                                    string sHoraDesde,
                                                    string sHoraHasta,
                                                    string sCodCompania,
                                                    string sEstadoTicket,
                                                    string sTipoTicket,
                                                    string sTipoPersona,
                                                    string sTipoVuelo,
                                                    string sCadFiltro,
                                                    string strTurno,
                                                    string sFlgCobro,
                                                    string sFlgMasiva,
                                                    string sEstadoTurno,
                                                    string sCajero,
                                                    string sMedioAnulacion,
                                                    string sColumnSort,
                                                    int iIniRows,
                                                    int iMaxRows,
                                                    string sTotalRows
                                                    )
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_TICKETXFECHA_PAGIN"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_TICKETXFECHA_PAGIN"];
                result = base.ListarDataTableSP(sNombreSP,
                                                sTipoDoc,
                                                sFchDesde,
                                                sFchHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sEstadoTicket,
                                                sTipoTicket,
                                                sTipoPersona,
                                                sTipoVuelo,
                                                sCadFiltro,
                                                strTurno,
                                                sFlgCobro,
                                                sFlgMasiva,
                                                sEstadoTurno,
                                                sCajero,
                                                sMedioAnulacion,
                                                sColumnSort,
                                                iIniRows,
                                                iMaxRows,
                                                sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable consultarTicketxFecha_Reh(string sFchDesde, string sFchHasta, string sCodCompania, string sTipoTicket, string sEstadoTicket, string sTipoPersona, string sCadFiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta, string strTurno)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_TICKETXFECHA_REH"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_TICKETXFECHA_REH"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sCodCompania, sTipoTicket, sEstadoTicket, sTipoPersona, sCadFiltro, sOrdenacion, sHoraDesde, sHoraHasta, strTurno);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad,string strMotivo, string strUsuario, ref string strMessage)
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
                    strMessage = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]).ToString();
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

        public DataTable obtenerTicketsAnulados(string sFechaDesde, string sFechaHasta)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CNSP_TICKETSANULADOS"];
                string sNombreSP = (string)hsSelectByIdUSP["CNSP_TICKETSANULADOS"];
                result = base.ListarDataTableSP(sNombreSP, sFechaDesde, sFechaHasta);
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

        public DataTable ObtenerComprobanteSEAE(string sAnio, string sMes,string sTDocumento, string strTipVenta, string strFlgAero)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["COMPROBANTE_SEAE"];
                string sNombreSP = (string)hsSelectByIdUSP["COMPROBANTE_SEAE"];
                result = base.ListarDataTableSP(sNombreSP, sAnio, sMes,sTDocumento, strTipVenta, strFlgAero);
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
        public DataTable consultarResumenStockTicketContingencia(string sTipoTicket, string sFechaAl, string sTipoResumen)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENSTOCKTICKETCONTINGENCIA"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENSTOCKTICKETCONTINGENCIA"];
                result = base.ListarDataTableSP(sNombreSP, sTipoTicket, sFechaAl, sTipoResumen);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
              try
              {
                    DataTable result;
                    Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS"];
                    string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS"];
                    result = base.ListarDataTableSP(sNombreSP, sCodCompania, sNumVuelo, sTipoDocumento, sTipoTicket, sTipoFiltro, sFechaInicial, sFechaFinal, sTimeInicial, sTimeFinal, sDestino, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
                    return result;
              }
              catch (Exception ex)
              {
                    throw ex;
              }

        }

        //CMONTES PARA GENERAR EL REPORTE DE RESUMEN DE TICKET VENDIDOS AL CREDITO
        public DataTable consultarResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENTICKETVENDIDOSCREDITO"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENTICKETVENDIDOSCREDITO"];
                result = base.ListarDataTableSP(sNombreSP, sTipoTicket, sFechaInicial, sFechaFinal, sAeroLinea, sNumVuelo, sCodPago);
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

        public DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_USOCONTIUSADO"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_USOCONTIUSADO"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
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

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Ing"], DbType.String, strCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Flag_Cierre_Turno"], DbType.String, strFlagTurno);

                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Creado"], DbType.String, 6);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Error"], DbType.String, 2);

                isRegistrado = base.mantenerSP(objCommandWrapper);

                if (isRegistrado)
                {

                    if (helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Error"]) != null)
                    {
                        strCodError = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Error"]).ToString().Trim();
                    }
                    else
                    {
                        strCodError = null;
                    }

                    if (helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Creado"]) != null)
                    {
                        strCodTurnoCreado = (string)helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Turno_Creado"]).ToString().Trim();
                    }
                    else
                    {
                        strCodTurnoCreado = null;
                    }
                }

                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_CONTISEL"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_CONTISEL"];
                result = base.ListarDataTableSP(sNombreSP, strTipTikcet,strFlgConti, strNumIni,strNumFin, strUsuario);
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

        public bool AnularTuua(string strListaTicket, int intTicket)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_ANULATUUA_UPD"];
                string sNombreSP = (string)hsInsertUSP["USP_ANULATUUA_UPD"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.Int32, intTicket);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Tickets"], DbType.String, strListaTicket);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        
        #region Additional Methods - kinzi
        public DataTable obtenerTicketProcesado(string strCodTurno)
        {

            String strSQL;
            try
            {
                DataTable result = null;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_PROCESADOS"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_PROCESADOS"];

                if (strCodTurno != null && strCodTurno.Length > 0)
                {
                    result = base.ListarDataTableSP(sNombreSP, strCodTurno);
                    ////Begin Build Query --------------------
                    //strSQL = "SELECT tt.Cod_Numero_Ticket, tt.Cod_Tipo_Ticket, tt.Cod_Compania," +
                    //                "tt.Fch_Vuelo, tt.Dsc_Num_Vuelo, tt.Fch_Creacion, tt.Hor_Creacion, tt.Tip_Estado_Actual," +
                    //                "tp.Dsc_Tipo_Ticket, tc.Dsc_Compania, tl.Dsc_Campo" +
                    //        " FROM TUA_Ticket tt LEFT JOIN TUA_TipoTicket tp on tt.Cod_Tipo_Ticket = tp.Cod_Tipo_Ticket " +
                    //                            "LEFT JOIN TUA_Compania tc on tt.Cod_Compania = tc.Cod_Compania " +
                    //                            "LEFT JOIN TUA_ListaDeCampos tl on tt.Tip_Estado_Actual = tl.Cod_Campo and tl.Nom_Campo = 'EstadoTicket' " +
                    //        " WHERE tt.Cod_Turno ='" + strCodTurno.Trim() + "'" +
                    //        " ORDER BY tt.Cod_Numero_Ticket ASC";
                    ////End Build Query --------------------
                    //result = base.ListarDataTableQY(strSQL);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataTable obtenerTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            try
            {
                DataTable result = null;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENDIARIO"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENDIARIO"];
                if (strTipo != null && strTipo.Length > 0) { 
                    if (strTipo == "1" || strTipo == "3")
                        result = base.ListarDataTableSP(sNombreSP, strTipo, strFecIni, strHorIni, strFecFin, strHorFin, null, null);
                    else
                        result = base.ListarDataTableSP(sNombreSP, "2", null, null, null, null, strTurnoIni, strTurnoFin);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //consultarTicketBoardingUsadosPagin  - 03.09.2010
        public DataTable consultarTicketBoardingUsadosPagin(string sFechaDesde, 
                                                    string sFechaHasta, 
                                                    string sHoraDesde, 
                                                    string sHoraHasta, 
                                                    string sCodCompania, 
                                                    string sTipoVuelo, 
                                                    string sNumVuelo, 
                                                    string sTipoPasajero, 
                                                    string sTipoDocumento, 
                                                    string sTipoTrasbordo, 
                                                    string sFechaVuelo, 
                                                    string sEstado,
                                                    string sColumnSort,
                                                    int iIniRows,
                                                    int iMaxRows,
                                                    string sTotalRows
                                                    )
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CNSP_TICKESBOARDINGUSADOSPAGIN"];
                string sNombreSP = (string)hsSelectByIdUSP["CNSP_TICKESBOARDINGUSADOSPAGIN"];
                result = base.ListarDataTableSP(sNombreSP, 
                                                sFechaDesde, 
                                                sFechaHasta, 
                                                sHoraDesde, 
                                                sHoraHasta, 
                                                sCodCompania, 
                                                sTipoVuelo, 
                                                sNumVuelo, 
                                                sTipoPasajero, 
                                                sTipoDocumento, 
                                                sTipoTrasbordo, 
                                                sFechaVuelo, 
                                                sEstado,
                                                sColumnSort,
                                                iIniRows,
                                                iMaxRows,
                                                sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //consultarTicketBoardingUsadosResumen - 08.09.2010
        public DataTable consultarTicketBoardingUsadosResumen(string sFechaDesde,
                                                    string sFechaHasta,
                                                    string sHoraDesde,
                                                    string sHoraHasta,
                                                    string sCodCompania,
                                                    string sTipoVuelo,
                                                    string sNumVuelo,
                                                    string sTipoPasajero,
                                                    string sTipoDocumento,
                                                    string sTipoTrasbordo,
                                                    string sFechaVuelo,
                                                    string sEstado
                                                    )
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["CNSP_TICKESBOARDINGUSADOSRES"];
                string sNombreSP = (string)hsSelectByIdUSP["CNSP_TICKESBOARDINGUSADOSRES"];
                result = base.ListarDataTableSP(sNombreSP,
                                                sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoVuelo,
                                                sNumVuelo,
                                                sTipoPasajero,
                                                sTipoDocumento,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sEstado);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //consultarDetalleVentaCompaniaPagin - 05.10.2010
        public DataTable consultarDetalleVentaCompaniaPagin(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta,string sColumnSort,int iIniRows,int iMaxRows,string sTotalRows)
        {
            try
            {
                DataTable result;
                //Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["usp_rpt_pcs_listadetalleventacompaniapagin_sel"];
                //string sNombreSP = (string)hsSelectByIdUSP["usp_rpt_pcs_listadetalleventacompaniapagin_sel"];
                string sNombreSP = "usp_rpt_pcs_listadetalleventacompaniapagin_sel";
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta,sColumnSort,iIniRows,iMaxRows,sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //consultarTicketVencidosPagin - 21.10.2010
        public DataTable consultarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_VENCIDOSPAGIN"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_VENCIDOSPAGIN"];
                result = base.ListarDataTableSP(sNombreSP, strFecIni, strFecFin, strTipoTicket, sColumnSort, iIniRows, iMaxRows, sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //consultarTicketBoardingUsadosDiaMesPagin - 02.12.2010
        public DataTable consultarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes
                        , string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket
                        , string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS_PAGIN_SEL"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_CONSULTA_TICKET_BOARDING_USADOS_PAGIN_SEL"];
                result = base.ListarDataTableSP(sNombreSP, sFchDesde, sFchHasta, sFchMes
                        , sTipoDocumento, sCodCompania, sNumVuelo, sTipTicket
                        , sDestino, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        //consultarResumenTicketVendidosCreditoPagin - 04.12.2010
        public DataTable consultarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_RESUMENTICKETVENDIDOSCREDITOPAGIN"];
                string sNombreSP = (string)hsSelectByIdUSP["TICKETSP_RESUMENTICKETVENDIDOSCREDITOPAGIN"];
                result = base.ListarDataTableSP(sNombreSP, sTipoTicket, sFechaInicial, sFechaFinal, sAeroLinea, sNumVuelo, sCodPago, sFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// Obtains data from a parameter and then inserts it to a temporal table.
        /// </summary>
        public void IngresarDatosATemporalTicket(string strCodTicket)
        {
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_LLENAR_TEMPORAL_TICKET"];
            string sNombreSP = (string)hsSelectAllUSP["BPSP_LLENAR_TEMPORAL_TICKET"];
            base.IngresarDatosATemporalTickets(sNombreSP, strCodTicket);
        }

        public DataTable obtenerTicketsTablaTemporal()
        {
            string sNombreSP = "";
            try
            {                
                DataTable result;
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TICKETSP_DETALLETICKET"];
                sNombreSP = (string)hsSelectByIdUSP["TICKETSP_DETALLETICKET"];
                result = base.ListarDataTableSP(sNombreSP, null);
                return result;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            Hashtable hsSelectAllUSP = (Hashtable)htSPConfig["BPSP_LLENAR_TEMPORAL_BOARDING"];
            sNombreSP = (string)hsSelectAllUSP["BPSP_LLENAR_TEMPORAL_BOARDING"];
            base.IngresarDatosATemporalBoardingPass(sNombreSP, null);
        }

        public string ObtenerFechaActual()
        {
            try
            {
                string sNombreSP = "usp_ope_cns_sysdate_sel";
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddOutParameter(objCommandWrapper, "Fch_SysDate", DbType.String, 14);
                helper.ExecuteNonQuery(objCommandWrapper);
                return (string)helper.GetParameterValue(objCommandWrapper, "Fch_SysDate");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Extender(string strListaTickets, string strListaFechas,int intCantidad, ref string strMessage)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TICKETSP_EXTENDER"];
                string sNombreSP = (string)hsInsertUSP["TICKETSP_EXTENDER"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Num_Tickets"], DbType.String, strListaTickets);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Vencimiento"], DbType.String, strListaFechas);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Can_Tickets"], DbType.Int32, intCantidad);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"], DbType.String, 255);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    strMessage = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Dsc_Message"]).ToString();
                }
                return isRegistrado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //PARA LAS LISTAS DESPLEGABLES
        public DataTable GetTicketsByFilter(string strCodTicket, string strTicketDesde, string strTicketHasta, string strCodTurno, string strColumnaSort, int iStartRows, int iMaxRows, string strPaginacion, string strFlgTotalRows)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["EXTICK_SELECTFILTER"];
                string sNombreSP = (string)hsSelectByIdUSP["EXTICK_SELECTFILTER"];
                result = base.ListarDataTableSP(sNombreSP, strCodTicket, strTicketDesde, strTicketHasta, strCodTurno, strColumnaSort, iStartRows, iMaxRows, strPaginacion, strFlgTotalRows, '0','0','0');
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}