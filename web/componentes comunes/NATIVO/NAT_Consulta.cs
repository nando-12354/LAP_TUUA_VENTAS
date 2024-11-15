using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONEXION;

namespace LAP.TUUA.NATIVO 
{
    public class NAT_Consulta: NAT_Conexion, IConsulta
    {
        protected string Dsc_PathSPConfig;
        DAO_Turno objDAOTurno;
        DAO_Usuario objDAOUsuario;
        DAO_Sincronizacion objDAOSincronizacion;
        DAO_UsuarioRol objDAOUsuarioRol;
        DAO_Compania objDAOCompania;
        DAO_ParameGeneral objDAOParameGeneral;
        DAO_Ticket objDAOTicket;
        DAO_ListaDeCampos objDAOListaDeCampos;
        DAO_Rol objDAORol;
        DAO_BoardingBcbp objDAOBoarding;
        DAO_TicketEstHist objDAOTicketEstHist;
        DAO_BoardingBcbpEstHist objDAOBoardingEstHist;
        DAO_BoardingBcbpErr objDAOBoardingErr;
        DAO_LogOperacion objDAOLogOperacion;
        DAO_VueloProgramado objDAOVueloProgramado;
        DAO_Auditoria objDAOAuditoria;
        DAO_TasaCambio objDAOTasaCambio;
        DAO_LogEstadistico objDAOLogEstadistico;
        DAO_Depuracion objDAODepuracion;

        public NAT_Consulta()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOTurno = new DAO_Turno(Dsc_PathSPConfig);
            objDAOUsuario = new DAO_Usuario(Dsc_PathSPConfig);
            objDAOSincronizacion = new DAO_Sincronizacion(Dsc_PathSPConfig);
            objDAODepuracion = new DAO_Depuracion(Dsc_PathSPConfig);
            objDAOUsuarioRol = new DAO_UsuarioRol(Dsc_PathSPConfig);
            objDAOCompania = new DAO_Compania(Dsc_PathSPConfig);
            objDAOParameGeneral = new DAO_ParameGeneral(Dsc_PathSPConfig);
            objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
            objDAOListaDeCampos = new DAO_ListaDeCampos(Dsc_PathSPConfig);
            objDAORol = new DAO_Rol(Dsc_PathSPConfig);
            objDAOBoarding = new DAO_BoardingBcbp(Dsc_PathSPConfig);
            objDAOTicketEstHist = new DAO_TicketEstHist(Dsc_PathSPConfig);
            objDAOBoardingEstHist = new DAO_BoardingBcbpEstHist(Dsc_PathSPConfig);
            objDAOBoardingErr = new DAO_BoardingBcbpErr(Dsc_PathSPConfig);
            objDAOLogOperacion = new DAO_LogOperacion(Dsc_PathSPConfig);
            objDAOVueloProgramado = new DAO_VueloProgramado(Dsc_PathSPConfig);
            objDAOAuditoria = new DAO_Auditoria(Dsc_PathSPConfig);
            objDAOTasaCambio = new DAO_TasaCambio(Dsc_PathSPConfig);
            objDAOLogEstadistico = new DAO_LogEstadistico(Dsc_PathSPConfig);

        }

        #region Additional Methods - kinzi
        /// <summary>
        /// ConsultaTurnoxFiltro2 - kinzi
        /// </summary>
        /// <param name="strFchIni"></param>
        /// <param name="strFchFin"></param>
        /// <param name="strCodUsuario"></param>
        /// <param name="strCodTurno"></param>
        /// <returns></returns>
        public override System.Data.DataTable ConsultaTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
        {
            try
            {
                return objDAOTurno.obtener(strFchIni, strFchFin, strCodUsuario, strCodTurno);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        /// <summary>
        /// ListarTicketProcesado - kinzi
        /// </summary>
        /// <param name="strCodTurno"></param>
        /// <returns></returns>
        public override System.Data.DataTable ListarTicketProcesado(string strCodTurno)
        {
            return objDAOTicket.obtenerTicketProcesado(strCodTurno);
        }
        /// <summary>
        /// ListarTicketVendido - kinzi
        /// </summary>
        /// <param name="strTipo"></param>
        /// <param name="strFecIni"></param>
        /// <param name="strFecFin"></param>
        /// <param name="strTurnoIni"></param>
        /// <param name="strTurnoFin"></param>
        /// <returns></returns>
        public override System.Data.DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objDAOTicket.obtenerTicketVendido(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        public override System.Data.DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objDAOLogOperacion.obtenerOperacionCompraVenta(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        public override System.Data.DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objDAOTasaCambio.obtenerResumenTasaCambio(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        //ListarTicketBoardingUsadosPagin - 03.09.2010
        public override System.Data.DataTable ListarTicketBoardingUsadosPagin(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objDAOTicket.consultarTicketBoardingUsadosPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        //ListarTicketBoardingUsadosResumen - 08.09.2010
        public override System.Data.DataTable ListarTicketBoardingUsadosResumen(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado)
        {
            return objDAOTicket.consultarTicketBoardingUsadosResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }

        public override System.Data.DataTable ListarTicketsExtorno(string as_Cod_Ticket, string as_Ticket_Desde, string as_Ticket_Hasta, string as_Cod_Turno, string as_ColumnaSort, int as_iStartRows, int as_iMaxRows, string as_Paginacion, string as_FlgTotalRows)
        {
            try
            {
                return objDAOTicket.ListarTicketsExtorno(as_Cod_Ticket, as_Ticket_Desde, as_Ticket_Hasta, as_Cod_Turno, as_ColumnaSort, as_iStartRows, as_iMaxRows, as_Paginacion, as_FlgTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        //ListarParametrosDefaultValue - 17.04.2011
        public override System.Data.DataTable ListarParametrosDefaultValue(string as_identificador)
        {
            try
            {
                //return objDAOParameGeneral.ObtenerParametroGeneralDefaultValue(as_identificador);
                return objDAOParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region Turno
        public override System.Data.DataTable ListarAllTurno(string as_codturno)
        {
            try
            {
                return objDAOTurno.listarAllTurno(as_codturno);
             }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override System.Data.DataTable CantidadMonedasTurno(string as_codturno)
        {
            try
            {
                return objDAOTurno.cantidadmonedaxTurno(as_codturno);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override System.Data.DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_iddetalle)
        {
            try
            {
                return objDAOTurno.detallemonedaxTurno(as_codturno, as_codmoneda, as_iddetalle);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override System.Data.DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string as_FlgReporte)
        {
            try
            {
                return objDAOTurno.consultarTurnoxFiltro(as_fchIni, as_fchFin, as_codusuario, as_ptoventa, as_horadesde, as_horahasta, as_FlgReporte);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        

        #endregion

        
        #region Usuarios


        public override System.Data.DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            try
            {
                return objDAOUsuario.consultarUsuarioxFiltro(as_rol, as_estado, as_grupo, sfiltro, sOrdenacion);
             }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override System.Data.DataTable ListarAllUsuario()
        {
            try
            {
                return objDAOUsuario.ListarAllUsuario();
             }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion


        #region Sincronizacion

        public override System.Data.DataTable ListarSincronizacion()
        {
            try
            {
                return objDAOSincronizacion.ListarSincronizacion();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override System.Data.DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, 
            string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sfiltro, string sOrdenacion)
        {
            try
            {
                return objDAOSincronizacion.ListarFiltroSincronizacion(as_molinete, as_estado,
                    as_TipoSincronizacion, as_TablaSincronizacion,strFchDesde,
                    strFchHasta, strHraDesde, strHraHasta, sfiltro, sOrdenacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }




        #endregion


        #region Depuracion

        public override System.Data.DataTable ListarFiltroDepuracion(string as_molinete, string as_estado,
             string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sfiltro, string sOrdenacion)
        {
            try
            {
                return objDAODepuracion.ListarFiltroDepuracion(as_molinete, as_estado,as_TablaSincronizacion, strFchDesde,
                    strFchHasta, strHraDesde, strHraHasta, sfiltro, sOrdenacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


        # endregion

        #region UsuarioRol

        public override DataTable ListarUsuarioxRol(string sCodRol)
        {
            try 
            {
                return objDAOUsuarioRol.ListarUsuarioRol(sCodRol);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion


        #region Compañía


        public override System.Data.DataTable listarAllCompania()
        {
            try
            {
                return objDAOCompania.listarAllCompania();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override System.Data.DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            try
            {
                return objDAOCompania.consultarCompaniaxFiltro(strEstado, strTipo, sfiltro, sOrdenacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        
        }

        #endregion
        

        #region ParametrosGeneral

       
        public override System.Data.DataTable ListarParametros(string as_identificador)
        {
            try
            {

                return objDAOParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        #endregion
        
        
        #region Ticket


        public override System.Data.DataTable ConsultaDetalleTicket(string as_numeroticket, string as_ticketdesde, string as_ticket_hasta)
        {
            try
            {
                return objDAOTicket.consultarDetalleTicket(as_numeroticket,as_ticketdesde,as_ticket_hasta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ConsultaDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_ticket_hasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                return objDAOTicket.consultarDetalleTicketPagin(as_numeroticket, as_ticketdesde, as_ticket_hasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_ticket_hasta)
        {
            try
            {
                return objDAOTicket.consultarDetalleTicket_Reh(as_numeroticket, as_ticketdesde, as_ticket_hasta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ConsultaDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_ticket_hasta, string as_flg_total)
        {
            try
            {
                return objDAOTicket.ConsultaDetalleTicket2_Reh(as_numeroticket, as_ticketdesde, as_ticket_hasta, as_flg_total);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ConsultaDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            try
            {
                return objDAOTicket.consultarDetalleTicket_Ope(sNumTicket, sTicketDesde, sTicketHasta, stipoticket, sTickets_Sel, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override System.Data.DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta, string strTurno)
        {
            try
            {
                return objDAOTicket.consultarTicketxFecha(as_FchDesde, as_FchHasta, as_CodCompania, as_TipoTicket, as_EstadoTicket, as_TipoPersona, sfiltro, sOrdenacion, sHoraDesde, sHoraHasta, strTurno);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        
        }

        public override System.Data.DataTable ListarTicketxFechaPagin(
            string as_TipDoc,
            string as_FchDesde,
            string as_FchHasta,
            string as_HoraDesde,
            string as_HoraHasta,
            string as_CodCompania,
            string as_EstadoTicket,
            string as_TipoTicket,
            string as_TipoPersona,
            string as_TipoVuelo,
            string as_CodBoarding,
            string as_Turno,
            string as_FlgCobro,
            string as_FlgMasiva,
            string as_EstadoTurno,
            string as_Cajero,
            string as_MedioAnulacion,
            string sColumnSort,
            int iIniRows,
            int iMaxRows,
            string sTotalRows)
        {
            try
            {
                return objDAOTicket.consultarTicketxFechaPagin(as_TipDoc,
                                                            as_FchDesde,
                                                            as_FchHasta,
                                                            as_HoraDesde,
                                                            as_HoraHasta,
                                                            as_CodCompania,
                                                            as_EstadoTicket,
                                                            as_TipoTicket,
                                                            as_TipoPersona,
                                                            as_TipoVuelo,
                                                            as_CodBoarding,
                                                            as_Turno,
                                                            as_FlgCobro,
                                                            as_FlgMasiva,
                                                            as_EstadoTurno,
                                                            as_Cajero,
                                                            as_MedioAnulacion,
                                                            sColumnSort,
                                                            iIniRows,
                                                            iMaxRows,
                                                            sTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override System.Data.DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta, string strTurno)
        {
            try
            {
                return objDAOTicket.consultarTicketxFecha_Reh(as_FchDesde, as_FchHasta, as_CodCompania, as_TipoTicket, as_EstadoTicket, as_TipoPersona, sfiltro, sOrdenacion, sHoraDesde, sHoraHasta, strTurno);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


        public override System.Data.DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta, string as_TipoDocumento, string as_FlagAnulado)
        {
            try
            {
                return objDAOTicket.obtenerCuadreTicketsEmitidos(as_FchDesde, as_FchHasta, as_TipoDocumento, as_FlagAnulado);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override string obtenerFechaEstadistico(string sEstadistico)
        {
            try
            {
                return objDAOLogEstadistico.obtenerFecha(sEstadistico);
            }
            catch(Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta)
        {
            try
            {
                return objDAOTicket.obtenerTicketsAnulados(as_FchDesde, as_FchHasta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion


        #region TicketEstHist

        public override System.Data.DataTable ListarTicketEstHist(string as_numeroticket)
        {
            try
            {
                return objDAOTicketEstHist.listarTicketEstHist(as_numeroticket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ListarTicketEstHist_Arch(string as_numeroticket)
        {
            try
            {
                return objDAOTicketEstHist.listarTicketEstHist_Arch(as_numeroticket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override System.Data.DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
        {
            try
            {
                return objDAOTicketEstHist.obtenerTicketBoardingUsados(as_FechaDesde, as_FechaHasta, as_HoraDesde, as_HoraHasta, as_CodCompania, as_TipoVuelo, as_NumVuelo, as_TipoPasajero, as_TipoDocumento, as_TipoTrasbordo, as_FechaVuelo, as_Estado);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
        {
            try
            {
                return objDAOBoarding.obtenerBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sBoardingSel, sCodCompania, sNumVuelo, sNumAsiento, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotal);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion
        

        #region ListaDeCampos

        
        public override System.Data.DataTable ListaCamposxNombre(string as_nombre)
        {
            try
            {
                return objDAOListaDeCampos.obtenerListaxNombre(as_nombre);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        
        }

        public override System.Data.DataTable ListaCamposxNombreOrderByDesc(string as_nombre)
        {
            try
            {
                return objDAOListaDeCampos.obtenerListaxNombreOrderByDesc(as_nombre);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion

        
        #region Rol
       
        public override System.Data.DataTable ListarRoles()
        {
            try
            {
                return objDAORol.obtenerALLRol();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion


        #region Boarding

        public override System.Data.DataTable DetalleBoarding(string as_Cod_Compania, string as_Num_Vuelo, string as_Fech_Vuelo, string as_Num_Asiento, string as_Pasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp)
        {
            try
            {
                return objDAOBoarding.DetalleBoarding(as_Cod_Compania, as_Num_Vuelo, as_Fech_Vuelo, as_Num_Asiento, as_Pasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable DetalleBoardingPagin(string as_Cod_Compania, string as_Num_Vuelo, string as_Fech_Vuelo, string as_Num_Asiento, string as_Pasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                return objDAOBoarding.DetalleBoardingPagin(as_Cod_Compania, as_Num_Vuelo, as_Fech_Vuelo, as_Num_Asiento, as_Pasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, sColumnSort, iIniRows, iMaxRows, sTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable DetalleBoarding_REH(string as_Cod_Compania, string as_Num_Vuelo, string as_Fech_Vuelo, string as_Num_Asiento, string as_Pasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
        {
            try
            {
                return objDAOBoarding.DetalleBoarding_REH(as_Cod_Compania, as_Num_Vuelo, as_Fech_Vuelo, as_Num_Asiento, as_Pasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, Flag_Fch_Vuelo, Check_Seq_Number);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable DetalleBoardingArchivado(String Num_Secuencial_Bcbp)
        {            
            try
            {
                return objDAOBoarding.DetalleBoardingArchivado(Num_Secuencial_Bcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarBoardingAsociados(String Num_Secuencial_Bcbp)
        {
            try
            {
                return objDAOBoarding.ListarBoardingAsociados(Num_Secuencial_Bcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
        {
            try
            {
                return objDAOBoarding.obtenerDetalleWebBCBP(sCodCompania, sNroVuelo, sFchVuelo, sAsiento, sPasajero);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        #endregion


        #region BoardingEstHist

        public override System.Data.DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            try
            {
                return objDAOBoardingEstHist.DetalleBoardingEstHist(Num_Secuencial_Bcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable DetalleBoardingEstHist_Arch(String Num_Secuencial_Bcbp)
        {
            try
            {
                return objDAOBoardingEstHist.DetalleBoardingEstHist_Arch(Num_Secuencial_Bcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region BoardingErr

        public override System.Data.DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde,
            string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding,
            string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort,
            int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            try
            {
                return objDAOBoardingErr.ListarLogErroresMolinete(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIDError, sTipoError, sCompania, sCodMolinete, sTipoBoarding, sTipIngreso, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region LogOperación

        public override System.Data.DataTable obtenerUsuarioxFechaOperacion(string as_FechaOperacion, string as_CodUsuario, string as_TipoOperacion, string as_CodMoneda)
        {
            try
            {
                return objDAOLogOperacion.obtenerUsuarioxFechaOperacion(as_FechaOperacion, as_CodUsuario, as_TipoOperacion,as_CodMoneda);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region VueloProgramado

        public override System.Data.DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            try
            {
                return objDAOVueloProgramado.DetallexLineaVuelo(sFechaDesde, sFechaHasta, sCodCompania, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion


        #region Auditoria

        public override System.Data.DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            try
            {
                return objDAOAuditoria.FiltrosAuditoria(strCodModulo, strFlgSubModulo, strTablaXml);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ObtenerConsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta)
        {
            try
            {
                return objDAOAuditoria.obtenerconsultaAuditoria(strTipOperacion, 
                    strTabla, strCodModulo, strCodSubModulo, 
                    strCodUsuario, strFchDesde, strFchHasta, strHraDesde, strHraHasta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ObtenerConsultaAuditoriaPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                        string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                        string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                return objDAOAuditoria.obtenerconsultaAuditoriaPagin(strTipOperacion, strTabla, strCodModulo, strCodSubModulo, strCodUsuario, strFchDesde, strFchHasta, strHraDesde, strHraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public System.Data.DataTable ObtenerConsultaAuditoriaPaginCrit(string strTipOperacion, 
            string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, 
            string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, 
            string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            try
            {
                return objDAOAuditoria.obtenerconsultaAuditoriaPaginCrit(strTipOperacion, strTabla, 
                    strCodModulo, strCodSubModulo, strCodUsuario, strFchDesde, strFchHasta, 
                    strHraDesde, strHraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador)
        {
            try
            {
                return objDAOAuditoria.obtenerdetalleAuditoria(strNombreTabla, strContador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public System.Data.DataTable ObtenerDetalleAuditoriaCrit(string strNombreTabla, string strContador)
        {
            try
            {
                return objDAOAuditoria.obtenerdetalleAuditoriaCrit(strNombreTabla, strContador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion


     
        public override bool CambiarClave(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasxTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxPtoVenta(string strEquipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Turno ObtenerTurnoIniciado(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurno(Turno objTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasInter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOperacion(LogOperacion objOperacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOpeCaja(List<LogOperacCaja> objLista)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsError()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetErrorCode()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override System.Data.DataTable LLenarMenu(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable LLenarArbolRoles(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");

        }

        public override UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxnombre(string sNomRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxcodigo(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable listarAllMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListaTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable listarAllPuntoVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<Rol> listaDeRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesAsignados(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public override System.Data.DataTable ListarAllParametroGenerales(string strParametro)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario,string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool insertarModalidadVenta(ModalidadVenta objModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarModalidadVenta(ModalidadVenta objModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarAllModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadVenta> listarModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TipoTicket> ListarAllTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new Exception("The method or operation is not implemented.");
        }

    
        public override bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int intTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario autenticar(string sCuenta, string sClave)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaDetalleTicket(string sNumeroTicket)
        {
            throw new NotImplementedException();
        }


        public override System.Data.DataTable ConsultarRepresXRehabilitacionYCia(String strCia)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarTurnosAbiertos()
        {
            throw new NotImplementedException();
        }

        public override List<ParameGeneral> listarAtributosGenerales()
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarTicket(List<Ticket> listaTickets)
        {
            throw new NotImplementedException();
        }


        public override bool obtenerRolHijo(string sCodRol)
        {
            throw new NotImplementedException();
        }



        public override bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            throw new NotImplementedException();
        }

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool insertarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaComp(ModVentaComp objModComp)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompañiaxcodigo(string sCodigoCompania)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompañiaxnombre(string sNombreCompania)
        {
            throw new NotImplementedException();
        }

     
        public override bool actualizarMolinete(Molinete objMolinete)
        {
            throw new NotImplementedException();
        }

      

        public override DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            throw new NotImplementedException();
        }
        public override DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override DataTable listarCompania_xCodigoEspecial(string codigoEspecial)
        {
            throw new NotImplementedException();
        }

        public override DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override bool AnularBCBP(BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerFecha()
        {
            throw new NotImplementedException();
        }

        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllCnfgAlarma()
        {
            throw new NotImplementedException();
        }

        public override CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllAlarmaGenerada()
        {
            throw new NotImplementedException();
        }

        public override AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllModulo()
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAlarmaGeneradaEnviadas()
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            throw new NotImplementedException();
        }


        public override DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento, string strTipVenta, string strFlgAero)
        {
            throw new NotImplementedException();
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerRecaudacionMensual(string anio)
        {
            throw new NotImplementedException();
        }      

        public override DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }



        public override DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override bool insertarSecuenciaModVentaComp(string codModalidad)
        {
            throw new NotImplementedException();
        }

        public override int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            throw new NotImplementedException();
        }

        public override int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            throw new NotImplementedException();
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
              throw new NotImplementedException();
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            throw new NotImplementedException();
        }



        public override int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            throw new NotImplementedException();
        }

        public override int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerEstadistico()
        {
            throw new NotImplementedException();
        }

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new NotImplementedException();
        }

        public override string ObtenerFechaActual()
        {
            throw new NotImplementedException();
        }
        
        public override bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
        {
            try
            {
                return objDAOBoarding.validarAsocBCBP(sNumAsiento, sNumVuelo, sFchVuelo, sNomPersona, sCompania, sCodBcbpBase);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


        public override DataTable ListarResumenCompaniaPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes, string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket, string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }



        public override bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }



        public override bool actualizarHoras(ListaDeCampo objLDeCampos)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllMolinetes()
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMolinete(string strMolinete)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarUnMolinete(Molinete objMolinete)
        {
            throw new NotImplementedException();
        }

        public override void IngresarDatosATemporalBoardingPass(string strTrama)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsTablaTemporal()
        {
            throw new NotImplementedException();
        }
    }
}
