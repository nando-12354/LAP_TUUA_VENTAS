using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ws = LAP.TUUA.CLIENTEWS.WSReportes;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Reportes : CWS_Conexion
    {
        protected ws.WSReportes objWSReportes;

        public CWS_Reportes()
        {
            objWSReportes = new WSReportes.WSReportes();
        }

        #region Ticket

        //CMONTES
        public override DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            return objWSReportes.obtenerMovimientoTicketContingencia(sFchDesde, sFchHasta, sEstado, sTipoTicket, sEstadoTicket, sRangoMinTicket, sRangoMaxTicket);
        }

        //CMONTES
        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
        {
            return objWSReportes.obtenerResumenStockTicketContingencia(sTipoTicket, sFchAl, sTipoResumen);
        }


        public override DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            return objWSReportes.consultarTicketBoardingUsados(sCodCompania, sNumVuelo, sTipoDocumento, sTipoTicket, sTipoFiltro, sFechaInicial, sFechaFinal, sTimeInicial, sTimeFinal, sDestino, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }

        //CMONTES
        public override DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sTipoPago)
        {
            return objWSReportes.obtenerResumenTicketVendidosCredito(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sTipoPago);
        }

        //CMONTES
        public override DataTable obtenerRecaudacionMensual(string sAnio)
        {
            return objWSReportes.obtenerRecaudacionMensual(sAnio);
        }

        //CMONTES        
        public override DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            return objWSReportes.obtenerDetalleVentaCompania(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);
        }

        public override DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)
        {
            return objWSReportes.ConsultarTicketVencidos(strFecIni, strFecFin, strTipoTicket);
        }

        public override DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
        {
            return objWSReportes.ConsultarLiquidVenta(strFecIni, strFecFin);
        }

        public override DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            return objWSReportes.ConsultarUsoContingencia(strFecIni, strFecFin);
        }

        public override DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            return objWSReportes.ConsultarUsoContingencia(strFecIni, strFecFin);
        }

        public override DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
        {
            return objWSReportes.obtenerLiquidacionVenta(sFchDesde, sFchHasta);
        }

        public override DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {
            return objWSReportes.obtenerLiquidacionVentaResumen(sFchDesde, sFchHasta);
        }

        public override DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            return objWSReportes.obtenerTicketBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sDocumento, sTicket, sAerolinea, sVuelo, sMotivo);
        }

        #endregion

        #region Boarding

        public override DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen)
        {
            return objWSReportes.obtenerBoardingLeidosMolinete(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen);
        }

        public override DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            return objWSReportes.obtenerBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sCompania, sMotivo, sTipoVuelo, sTipoPersona, sNumVuelo, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }

        public override DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.consultarBoardingPassDiario(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoPasajero, sTipoVuelo, sTipoTrasbordo, sFechaVuelo, sNumVuelo, sPasajero, sNumAsiento, sCodIata, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        #endregion

        #region Estadistico

        public override DataTable ObtenerEstadistico()
        {
            return objWSReportes.ObtenerEstadistico();
        }

        #endregion

        #region Additional Methods - kinzi
        //ListarTicketBoardingUsadosPagin - 03.09.2010
        public override System.Data.DataTable ListarResumenCompaniaPagin(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        //ListarTicketVencidosPagin - 21.10.2010
        public override System.Data.DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.ListarTicketVencidosPagin(strFecIni, strFecFin, strTipoTicket, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        //ListarBoardingLeidosMolinetePagin - 01.12.2010
        public override System.Data.DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.ListarBoardingLeidosMolinetePagin(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        //ListarTicketBoardingUsadosDiaMesPagin - 02.12.2010
        public override System.Data.DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes
                        , string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket
                        , string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.ListarTicketBoardingUsadosDiaMesPagin(sFchDesde, sFchHasta, sFchMes
                        , sTipoDocumento, sCodCompania, sNumVuelo, sTipTicket
                        , sDestino, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        //ListarResumenTicketVendidosCreditoPagin - 04.12.2010
        public override System.Data.DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objWSReportes.ListarResumenTicketVendidosCreditoPagin(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sCodPago, sFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        #endregion

        public override LAP.TUUA.ENTIDADES.Usuario autenticar(string sCuenta, string sClave)
        {
            throw new NotImplementedException();
        }

        public override bool CambiarClave(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool registrarUsuario(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarUsuario(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Usuario obtenerUsuario(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable obtenerFecha()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable LLenarMenu(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable LLenarArbolRoles(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRolUsuario(LAP.TUUA.ENTIDADES.UsuarioRol objRolUsuarioRol)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override bool obtenerRolHijo(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListaRol()
        {
            throw new NotImplementedException();
        }

        public override bool registrarRol(LAP.TUUA.ENTIDADES.Rol objRol)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarRol(LAP.TUUA.ENTIDADES.Rol objRol)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarRol(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Rol obtenerRolxnombre(string sNomRol)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Rol obtenerRolxcodigo(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Rol> listaDeRoles()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Rol> listarRolesAsignados(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool registrarPerfilRol(LAP.TUUA.ENTIDADES.PerfilRol objPerfilRol)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarPerfilRol(LAP.TUUA.ENTIDADES.PerfilRol objPerfilRol)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new NotImplementedException();
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllModulo()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Moneda> ListarMonedas()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Moneda> ListarMonedasxTipoTicket()
        {
            throw new NotImplementedException();
        }

        public override bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool verificarTurnoCerradoxPtoVenta(string strEquipo)
        {
            throw new NotImplementedException();
        }

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Turno ObtenerTurnoIniciado(string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTurnoMonto(List<LAP.TUUA.ENTIDADES.TurnoMonto> listaMontos)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarTurno(LAP.TUUA.ENTIDADES.Turno objTurno)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarTurnoMonto(List<LAP.TUUA.ENTIDADES.TurnoMonto> listaMontos)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Moneda> ListarMonedasInter()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.Limite> ListarLimitesPorOperacion(string stipoope)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarOperacion(LAP.TUUA.ENTIDADES.LogOperacion objOperacion)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarOpeCaja(List<LAP.TUUA.ENTIDADES.LogOperacCaja> objLista)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarCompraVenta(LAP.TUUA.ENTIDADES.LogCompraVenta objCompraVenta)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTicket(List<LAP.TUUA.ENTIDADES.Ticket> listaTickets, ref string strListaTickets)
        {
            throw new NotImplementedException();
        }

        public override bool IsError()
        {
            throw new NotImplementedException();
        }

        public override string GetErrorCode()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.RepresentantCia> ListarRepteCia(string strCia)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarVentaMasiva(LAP.TUUA.ENTIDADES.VentaMasiva objVentaMasiva)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarVentaMasiva(LAP.TUUA.ENTIDADES.VentaMasiva objVentaMasiva)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.TipoTicket> ListarAllTipoTicket()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarTurnosAbiertos()
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarTicket(List<LAP.TUUA.ENTIDADES.Ticket> listaTickets)
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

        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            throw new NotImplementedException();
        }

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarMolinete(LAP.TUUA.ENTIDADES.Molinete objMolinete)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarEstadoBCBP(LAP.TUUA.ENTIDADES.BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override bool AnularBCBP(LAP.TUUA.ENTIDADES.BoardingBcbp objBoardingBcbp)
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

        public override System.Data.DataTable listarAllMonedas()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            throw new NotImplementedException();
        }

        public override bool registrarTipoMoneda(LAP.TUUA.ENTIDADES.Moneda objTipoMoneda)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarTipoMoneda(LAP.TUUA.ENTIDADES.Moneda objTipoMoneda)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListaTipoTicket()
        {
            throw new NotImplementedException();
        }

        public override bool registrarTipoTicket(LAP.TUUA.ENTIDADES.TipoTicket TipoTicket)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarTipoTicket(LAP.TUUA.ENTIDADES.TipoTicket TipoTicket)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable listarAllPuntoVenta()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new NotImplementedException();
        }

        public override bool registrarPuntoVenta(LAP.TUUA.ENTIDADES.EstacionPtoVta objPuntoVenta)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarPuntoVenta(LAP.TUUA.ENTIDADES.EstacionPtoVta objPuntoVenta)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModalidadVenta(LAP.TUUA.ENTIDADES.ModalidadVenta objModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModalidadVenta(LAP.TUUA.ENTIDADES.ModalidadVenta objModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllModalidadVenta()
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadVenta> listarModalidadVenta()
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaAtributo(LAP.TUUA.ENTIDADES.ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaAtributo(LAP.TUUA.ENTIDADES.ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ParameGeneral> listarAtributosGenerales()
        {
            throw new NotImplementedException();
        }

        public override bool insertarCompania(LAP.TUUA.ENTIDADES.Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCompania(LAP.TUUA.ENTIDADES.Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Compania obtenerCompañiaxcodigo(string sCodigoCompania)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.Compania obtenerCompañiaxnombre(string sNombreCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarRepresentante(LAP.TUUA.ENTIDADES.RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarRepresentante(LAP.TUUA.ENTIDADES.RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaComp(LAP.TUUA.ENTIDADES.ModVentaComp objModComp)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaCompAtr(LAP.TUUA.ENTIDADES.ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaCompAtr(LAP.TUUA.ENTIDADES.ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            throw new NotImplementedException();
        }

        public override List<LAP.TUUA.ENTIDADES.ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllTurno(string as_codturno)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable CantidadMonedasTurno(string as_codturno)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable listarAllCompania()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllUsuario()
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarUsuarioxRol(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarParametros(string as_identificador)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaDetalleTicket(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaDetalleTicket(string sNumeroTicket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }


        public override System.Data.DataTable ListarTicketEstHist(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketEstHist_Arch(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable DetalleBoarding(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable DetalleBoardingEstHist(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingEstHist_Arch(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListaCamposxNombre(string sNombre)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarRoles()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable obtenerUsuarioxFechaOperacion(string sFechaOperacion, string sCodUsuario, string sTipoOperacion, string sCodMoneda)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }


        public override System.Data.DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            throw new NotImplementedException();
        }

        public override bool insertarCnfgAlarma(LAP.TUUA.ENTIDADES.CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCnfgAlarma(LAP.TUUA.ENTIDADES.CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllCnfgAlarma()
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override bool insertarAlarmaGenerada(LAP.TUUA.ENTIDADES.AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarAlarmaGenerada(LAP.TUUA.ENTIDADES.AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAllAlarmaGenerada()
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ListarAlarmaGeneradaEnviadas()
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarListaDeCampo(LAP.TUUA.ENTIDADES.ListaDeCampo objListaCampo, int intTipo)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTasaCambio(LAP.TUUA.ENTIDADES.TasaCambio objTasaCambio)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarTasaCambioHist(LAP.TUUA.ENTIDADES.TasaCambioHist objTasaCambioHist)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarPrecioTicket(LAP.TUUA.ENTIDADES.PrecioTicket objPrecioTicket)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            throw new NotImplementedException();
        }

        public override bool RegistrarPrecioTicketHist(LAP.TUUA.ENTIDADES.PrecioTicketHist objPrecioTicketHist)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultarRepresXRehabilitacionYCia(string strCia)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionTicket(LAP.TUUA.ENTIDADES.TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBP(LAP.TUUA.ENTIDADES.BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable listarCompania_xCodigoEspecial(string codigoEspecial)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllParametroGenerales(string strParametro)
        {
            throw new NotImplementedException();
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
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

        public override DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta, string as_TipoDocumento, string as_FlagAnulado)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta)
        {
            throw new NotImplementedException();
        }


        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            throw new NotImplementedException();
        }

        public override DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            throw new NotImplementedException();
        }


        public override DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador)
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

        public override DataTable ConsultaTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
        {
            throw new NotImplementedException();
        }


        public override DataTable ListarTicketProcesado(string strCodTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }
        public override DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
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

        public override DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
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

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string as_FlgReporte)
        {
            throw new NotImplementedException();
        }
        public override DataTable ListaCamposxNombreOrderByDesc(string sNombre)
        {
            throw new NotImplementedException();
        }
        public override DataTable DetalleBoarding_REH(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new NotImplementedException();
        }


        public override DataTable ConsultaDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_ColumnSort, int i_IniRows, int i_MaxRows, string as_TotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingPagin(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoriaPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override string ObtenerFechaActual()
        {
            throw new NotImplementedException();
        }

        public override string obtenerFechaEstadistico(string sEstadistico)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_flgtotal)
        {
            throw new NotImplementedException();
        }



        public override DataTable ListarTicketxFechaPagin(string as_TipDoc, string as_FchDesde, string as_FchHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_EstadoTicket, string as_TipoTicket, string as_TipoPersona, string as_TipoVuelo, string as_CodBoarding, string as_Turno, string as_FlgCobro, string as_FlgMasiva, string as_EstadoTurno, string as_Cajero, string as_MedioAnulacion, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingArchivado(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingAsociados(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosResumen(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
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

        public override DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding, string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketsExtorno(string as_Cod_Ticket, string as_Ticket_Desde, string as_Ticket_Hasta, string as_Cod_Turno, string as_ColumnaSort, int as_iStartRows, int as_iMaxRows, string as_Paginacion, string as_FlgTotalRows)
        {
            throw new NotImplementedException();
        }
        public override DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarParametrosDefaultValue(string as_identificador)
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

        public override DataTable ListarSincronizacion()
        {
            throw new NotImplementedException();
        }





        public override DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarHoras(ListaDeCampo objListaCampo)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarFiltroDepuracion(string as_molinete, string as_estado, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sfiltro, string sOrdenacion)
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
