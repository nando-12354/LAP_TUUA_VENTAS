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
    public class DAO_Turno : DAO_BaseDatos  
    {
        #region Fields  
        public List<Turno> objListaTurno;

        #endregion

        #region Constructors

        public DAO_Turno(string sConfigSPPath)
            : base(sConfigSPPath)
        {
            objListaTurno = new List<Turno>();

        }
        #endregion

        #region Methods

        /// <summary>
        /// Saves a record to the DAOTurno table.
        /// </summary>
        public bool crear(string as_cod_secuencial, string as_cod_usuario, string as_cod_equipo, ref string strTurnoError)
        {
            try
            {
                Hashtable htInsertUSP = (Hashtable)htSPConfig["TUR_CREATE"];
                string sNombreSP = (string)htInsertUSP["TUR_CREATE"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Secuencial"], DbType.String, as_cod_secuencial);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Usuario"], DbType.String, as_cod_usuario);
                helper.AddInParameter(objCommandWrapper, (string)htInsertUSP["Cod_Equipo"], DbType.String, as_cod_equipo);
                helper.AddOutParameter(objCommandWrapper, "Cod_Turno_Creado", DbType.String, 6);
                isRegistrado = base.mantenerSP(objCommandWrapper);
                if (isRegistrado)
                {
                    if (!Convert.IsDBNull(helper.GetParameterValue(objCommandWrapper, "Cod_Turno_Creado")))
                    {
                        strTurnoError = (string)helper.GetParameterValue(objCommandWrapper, "Cod_Turno_Creado");
                    }
                    else strTurnoError = null;
                }
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool insertar(Turno objTurno)
        {

            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["USP_INSERT"];
                string sNombreSP = (string)hsInsertUSP["USP_INSERT"];


                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, objTurno.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Inicio"], DbType.DateTime, objTurno.DtFchInicio);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Fch_Fin"], DbType.DateTime, objTurno.DtFchFin);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario_Cierre"], DbType.String, objTurno.SCodUsuarioCierre);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, objTurno.SCodUsuario);
                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Equipo"], DbType.String, objTurno.SCodEquipo);

                isRegistrado = base.mantenerSP(objCommandWrapper);
                return isRegistrado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Updates a record in the DAOTurno table.
        /// </summary>
        public bool actualizar(Turno objTurno)
        {

            try
            {
                Hashtable hsUpdateUSP = (Hashtable)htSPConfig["TURSP_UPDATE"];
                string sNombreSP = (string)hsUpdateUSP["TURSP_UPDATE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Turno"], DbType.String, objTurno.SCodTurno);
                helper.AddInParameter(objCommandWrapper, (string)hsUpdateUSP["Cod_Usuario_Cierre"], DbType.String, objTurno.SCodUsuarioCierre);
                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Deletes a record from the DAOTurno table by its primary key.
        /// </summary>
        public bool eliminar(string sCodTurno)
        {

            try
            {

                Hashtable hsDeleteUSP = (Hashtable)htSPConfig["TURSP_DELETE"];
                string sNombreSP = (string)hsDeleteUSP["TURSP_DELETE"];

                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsDeleteUSP["Cod_Turno"], DbType.String, sCodTurno);


                isActualizado = base.mantenerSP(objCommandWrapper);
                return isActualizado;
            }
            catch (Exception)
            {

                throw;
            }

        }


        /// <summary>
        /// Lista los turno deacuerdo al filtro por fechas, usuario y pto de venta.
        /// </summary>
        /// <param name="sFchIni"></param>
        /// <param name="sFchFin"></param>
        /// <param name="sCodUsuario"></param>
        /// <param name="sPtoVenta"></param>
        /// <returns></returns>
        public DataTable consultarTurnoxFiltro(string sFchIni, string sFchFin, string sCodUsuario, string sPtoVenta, string sHoraDesde, string sHoraHasta, string sFlgReporte)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_LISTARTURNOFILTRO"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_LISTARTURNOFILTRO"];
                result = base.ListarDataTableSP(sNombreSP, sFchIni, sFchFin, sCodUsuario, sPtoVenta, sHoraDesde, sHoraHasta, sFlgReporte);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable listarAllTurno(string sCodTurno)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_LISTARALLTURNO"];
            string sNombreSP = (string)hsSelectByIdUSP["TURSP_LISTARALLTURNO"];
            result = base.ListarDataTableSP(sNombreSP, sCodTurno);
            return result;
        }

        /// <summary>
        /// Define la cantidad de monedas para un turno
        /// </summary>
        public DataTable cantidadmonedaxTurno(string sCodTurno)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNONUMMDA_SELECT"];
                string sNombreSP = (string)hsSelectByIdUSP["TURNONUMMDA_SELECT"];
                result = base.ListarDataTableSP(sNombreSP, sCodTurno);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Define el detallado de operaciones por turno/moneda
        /// </summary>
        /// <param name="sCodTurno"></param>
        /// <param name="sMoneda"></param>
        /// <returns></returns>
        public DataTable detallemonedaxTurno(string sCodTurno, string sMoneda, string sIdDetalle)
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNODETALLADONUMMDA_SELECT"];
            string sNombreSP = (string)hsSelectByIdUSP["TURNODETALLADONUMMDA_SELECT"];
            result = base.ListarDataTableSP(sNombreSP, sCodTurno, sMoneda, sIdDetalle);


            return result;
        }


        /// <summary>
        /// Selects a single record from the DAOTurno table.
        /// </summary>
        public Turno obtener(string sCodTurno)
        {
            Turno objTurno = null;
            IDataReader result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["USP_OBTENER_TURNO"];
            string sNombreSP = (string)hsSelectByIdUSP["USP_OBTENER_TURNO"];
            result = base.listarDataReaderSP(sNombreSP, sCodTurno);

            while (result.Read())
            {
                objTurno = poblar(result);

            }
            result.Dispose();
            result.Close();
            return objTurno;
        }

        public Turno obtenerTurnoIniciado(string as_usuario)
        {
            Turno objTurno = null;
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_INICIADO"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_INICIADO"];
                result = base.listarDataReaderSP(sNombreSP, as_usuario);

                while (result.Read())
                {
                    objTurno = poblar(result);

                }
                result.Dispose();
                result.Close();
                return objTurno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int selectCountTurnoAbiertoxUsuario(string as_usuario)
        {
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_SELECTCOUNTUSER"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_SELECTCOUNTUSER"];
                result = base.listarDataReaderSP(sNombreSP, as_usuario);
                while (result.Read())
                {
                    return (Int32)result[(string)hsSelectByIdUSP["cant_turnos"]];
                }
                return 0;
            }catch (Exception)
            {

                throw;
            }
        }

        public int selectCountTurnoAbiertoxPtoVenta(string as_ptoventa)
        {
            IDataReader result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURSP_SELECTCOUNTPTOVTA"];
                string sNombreSP = (string)hsSelectByIdUSP["TURSP_SELECTCOUNTPTOVTA"];
                result = base.listarDataReaderSP(sNombreSP, as_ptoventa);
                while (result.Read())
                {
                    return (Int32)result[(string)hsSelectByIdUSP["cant_turnos"]];
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            try
            {
                Hashtable hsInsertUSP = (Hashtable)htSPConfig["TURNOSP_OBTENERDETALLE"];
                string sNombreSP = (string)hsInsertUSP["TURNOSP_OBTENERDETALLE"];
                DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

                helper.AddInParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Usuario"], DbType.String, strCodUsuario);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cant_Ticket"], DbType.String, 8);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"], DbType.String, 6);
                helper.AddOutParameter(objCommandWrapper, (string)hsInsertUSP["FecHor_Turno"], DbType.String, 14);
                isRegistrado = base.mantenerSPSinAuditoria(objCommandWrapper);

                if (isRegistrado)//Ejecuto el Store Procedure satisfactoriamente
                {                    
                    if (helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cant_Ticket"]) != null)
                    {
                        strCantTickets = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cant_Ticket"]).ToString().Trim();
                    }
                    else
                    {
                        strCantTickets = null;
                    }

                    if (helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"]) != null)
                    {
                        strCodTurno = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["Cod_Turno"]).ToString().Trim();
                    }
                    else
                    {
                        strCodTurno = null;
                    }

                    if (helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["FecHor_Turno"]) != null)
                    {
                        strFecHorTurno = helper.GetParameterValue(objCommandWrapper, (string)hsInsertUSP["FecHor_Turno"]).ToString().Trim();
                    }
                    else
                    {
                        strFecHorTurno = null;
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
        /// Selects all records from the DAOTurno table.
        /// </summary>
        public DataTable ListarTurnosAbiertos()
        {
            DataTable result;
            Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNOSP_SELECALLABIERTO"];
            string sNombreSP = (string)hsSelectByIdUSP["TURNOSP_SELECALLABIERTO"];
            result = base.ListarDataTableSP(sNombreSP,null);
            return result;
        }

        public DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            DataTable result;
            //Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNOSP_SELECALLABIERTO"];
            string sNombreSP = "usp_cns_pcs_cuadre_sel";//(string)hsSelectByIdUSP["TURNOSP_SELECALLABIERTO"];

            //DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);
            //helper.AddInParameter(objCommandWrapper, "Cod_Turno", DbType.String, strTurno);
            //helper.AddInParameter(objCommandWrapper, "Cod_Moneda", DbType.String, strMoneda);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Efec_Ini", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ticket_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ticket_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ticket_Int", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ticket_Nac", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ingreso_Caja", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ingreso_Caja", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Venta_Moneda", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Venta_Moneda", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Egreso_Caja", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Egreso_Caja", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Compra_Moneda", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Compra_Moneda", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Efectivo_Final", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Anul_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Anul_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Infante_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Infante_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Credito_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Credito_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Credito_Int", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Credito_Nac", DbType.Decimal, 0);
            //base.mantenerSP(objCommandWrapper);
            //decEfectivoIni = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Efec_Ini");

            result = base.ListarDataTableSP(sNombreSP, strTurno, strMoneda);
            return result;
        }


        public DataTable ListarCuadreTurnoVentas(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, 
                                            ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, 
                                            ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda,
                                            ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, 
                                            ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            //bool isResult;
            //Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNOSP_SELECALLABIERTO"];
            //string sNombreSP = (string)hsSelectByIdUSP["TURNOSP_SELECALLABIERTO"];

            //DbCommand objCommandWrapper = helper.GetStoredProcCommand(sNombreSP);

            //helper.AddInParameter(objCommandWrapper, "Cod_Turno", DbType.String, strTurno);
            //helper.AddInParameter(objCommandWrapper, "Cod_Moneda", DbType.String, strMoneda);

            //helper.AddOutParameter(objCommandWrapper, "Imp_Efec_Ini", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ticket_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ticket_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ticket_Int", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ticket_Nac", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Ingreso_Caja", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Ingreso_Caja", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Venta_Moneda", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Venta_Moneda", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Egreso_Caja", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Egreso_Caja", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Compra_Moneda", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Compra_Moneda", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Efectivo_Final", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Can_Anul_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Anul_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Infante_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Infante_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Credito_Int", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Can_Credito_Nac", DbType.Int32, 4);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Credito_Int", DbType.Decimal, 0);
            //helper.AddOutParameter(objCommandWrapper, "Imp_Credito_Nac", DbType.Decimal, 0);

            //isResult = base.mantenerSPSinAuditoria(objCommandWrapper);

            //decEfectivoIni = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Efec_Ini");
            //intTicketInt = (int)helper.GetParameterValue(objCommandWrapper, "Can_Ticket_Int");
            //intTicketNac = (int)helper.GetParameterValue(objCommandWrapper, "Can_Ticket_Nac");
            //decTicketInt = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Ticket_Int");
            //decTicketNac = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Ticket_Nac");
            //intIngCaja = (int)helper.GetParameterValue(objCommandWrapper, "Can_Ingreso_Caja");
            //decIngCaja = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Ingreso_Caja");
            //intVentaMoneda = (int)helper.GetParameterValue(objCommandWrapper, "Can_Venta_Moneda");
            //decVentaMoneda = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Venta_Moneda");
            //decEfectivoFinal = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Efectivo_Final");
            //intAnulaInt = (int)helper.GetParameterValue(objCommandWrapper, "Can_Anul_Int");
            //intAnulaNac = (int)helper.GetParameterValue(objCommandWrapper, "Can_Anul_Nac");
            //intInfanteInt = (int)helper.GetParameterValue(objCommandWrapper, "Can_Infante_Int");
            //intInfanteNac = (int)helper.GetParameterValue(objCommandWrapper, "Can_Infante_Nac");
            //intEgreCaja = (int)helper.GetParameterValue(objCommandWrapper, "Can_Egreso_Caja");
            //decEgreCaja = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Egreso_Caja");
            //intCompraMoneda = (int)helper.GetParameterValue(objCommandWrapper, "Can_Compra_Moneda");
            //decCompraMoneda = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Compra_Moneda");
            //intCreditoInt = (int)helper.GetParameterValue(objCommandWrapper, "Can_Credito_Int");
            //intCreditoNac = (int)helper.GetParameterValue(objCommandWrapper, "Can_Credito_Nac");
            //decCreditoInt = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Credito_Int");
            //decCreditoNac = (decimal)helper.GetParameterValue(objCommandWrapper, "Imp_Credito_Nac");

            //return isResult;


            DataTable result;            
            string sNombreSP = "usp_cns_pcs_cuadre_sel";
            result = base.ListarDataTableSP(sNombreSP, strTurno, strMoneda);
            return result;

        }

        /// <summary>
        /// Creates a new instance of the DAOTurno class and populates it with data from the specified SqlDataReader.

        /// </summary>
        public Turno poblar(IDataReader dataReader)
        {

            Hashtable htSelectAllUSP = (Hashtable)htSPConfig["TURSP_SELECTALL"];
            Turno objTurno = new Turno();
            if (dataReader.FieldCount == 0)
            {
                return null;
            }
            try
            {
                if (dataReader[(string)htSelectAllUSP["Cod_Turno"]] != DBNull.Value)
                    objTurno.SCodTurno = ((string)dataReader[(string)htSelectAllUSP["Cod_Turno"]]).Trim();
                if (dataReader[(string)htSelectAllUSP["Fch_Inicio"]] != DBNull.Value)
                    objTurno.DtFchInicio = (string)dataReader[(string)htSelectAllUSP["Fch_Inicio"]];
                if (dataReader[(string)htSelectAllUSP["Hor_Inicio"]] != DBNull.Value)
                    objTurno.SHoraInicio = (string)dataReader[(string)htSelectAllUSP["Hor_Inicio"]];
                if (!Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Fch_Fin"]]))
                    objTurno.DtFchFin = (string)dataReader[(string)htSelectAllUSP["Fch_Fin"]];
                objTurno.SHoraFin = !Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Hor_Fin"]]) ? (string)dataReader[(string)htSelectAllUSP["Hor_Fin"]] : "";
                objTurno.SCodUsuarioCierre = !Convert.IsDBNull(dataReader[(string)htSelectAllUSP["Cod_Usuario_Cierre"]]) ? (string)dataReader[(string)htSelectAllUSP["Cod_Usuario_Cierre"]] : "";
                if (dataReader[(string)htSelectAllUSP["Cod_Usuario"]] != DBNull.Value)
                    objTurno.SCodUsuario = ((string)dataReader[(string)htSelectAllUSP["Cod_Usuario"]]).Trim();
                if (dataReader[(string)htSelectAllUSP["Cod_Equipo"]] != DBNull.Value)
                    objTurno.SCodEquipo = (string)dataReader[(string)htSelectAllUSP["Cod_Equipo"]];
            }
            catch (Exception e)
            {
                return null;
            }
            return objTurno;
        }

        #endregion

        #region Additional Methods - kinzi
        public DataTable obtener(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
        {
            DataTable result;
            try
            {
                Hashtable hsSelectByIdUSP = (Hashtable)htSPConfig["TURNOSP_LISTARTURNOXPROC"];
                string sNombreSP = (string)hsSelectByIdUSP["TURNOSP_LISTARTURNOXPROC"];
                result = base.ListarDataTableSP(sNombreSP, strFchIni, strFchFin, strCodUsuario, strCodTurno);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
