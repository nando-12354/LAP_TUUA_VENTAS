using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using System.Data;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_Reportes
    {
        protected Conexion objCnxReporte;

        public BO_Reportes()
        {
            objCnxReporte = Resolver.ObtenerConexion(Define.CNX_10);
        }

        #region Ticket

        //CMONTES
        public DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            return objCnxReporte.obtenerMovimientoTicketContingencia(sFchDesde, sFchHasta, sEstado, sTipoTicket, sEstadoTicket, sRangoMinTicket, sRangoMaxTicket);
        }

        //CMONTES
        public DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
        {
            return objCnxReporte.obtenerResumenStockTicketContingencia(sTipoTicket, sFchAl, sTipoResumen);
        }

        //CMONTES
        public DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
        {
            return objCnxReporte.obtenerResumenTicketVendidosCredito(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sCodPago);
        }

        //CMONTES
        public DataTable obtenerRecaudacionMensual(string sAnio)
        {
            return objCnxReporte.obtenerRecaudacionMensual(sAnio);
        }

        public DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            return objCnxReporte.consultarTicketBoardingUsados(sCodCompania, sNumVuelo, sTipoDocumento, sTipoTicket, sTipoFiltro, sFechaInicial, sFechaFinal, sTimeInicial, sTimeFinal, sDestino, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }

        public DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            return objCnxReporte.obtenerDetalleVentaCompania(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);
        }

        public DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)       
        {
            return objCnxReporte.ConsultarTicketVencidos(strFecIni, strFecFin, strTipoTicket);        
        }        
        #endregion


        public DataTable ConsultarLiquidacionVenta(string strFecIni, string strFecFin)
        {
            return objCnxReporte.ConsultarLiquidVenta(strFecIni, strFecFin);
        }

        public DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            return objCnxReporte.ConsultarUsoContingencia(strFecIni, strFecFin);
        }

        public DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            return objCnxReporte.ConsultarUsoContingenciaUsado(strFecIni, strFecFin);
        }

        public DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
        {
            return objCnxReporte.obtenerLiquidacionVenta(sFchDesde, sFchHasta);
        }

        public DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {
            return objCnxReporte.obtenerLiquidacionVentaResumen(sFchDesde, sFchHasta);
        }

        public DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            return objCnxReporte.obtenerTicketBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sDocumento, sTicket, sAerolinea, sVuelo, sMotivo);
        }

        #region Boarding

        public DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen)
        {
            return objCnxReporte.BoardingLeidosMolinete(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen);
        }
        #endregion


        public DataTable ObtenerEstadistico()
        {
            return objCnxReporte.ObtenerEstadistico();
        }

        public DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            return objCnxReporte.obtenerBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sCompania, sMotivo, sTipoVuelo, sTipoPersona, sNumVuelo, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }

        public DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.consultarBoardingPassDiario(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoPasajero, sTipoVuelo, sTipoTrasbordo, sFechaVuelo, sNumVuelo, sPasajero, sNumAsiento, sCodIata, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        #region Additional Methods - kinzi
        //ListarResumenCompaniaPagin - 05.10.2010
        public DataTable ListarResumenCompaniaPagin(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.ListarResumenCompaniaPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
                
        //ListarTicketVencidosPagin - 21.10.2010
        public DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.ListarTicketVencidosPagin(strFecIni, strFecFin, strTipoTicket, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        //ListarBoardingLeidosMolinetePagin - 01.12.2010
        public DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.ListarBoardingLeidosMolinetePagin(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        //ListarTicketBoardingUsadosDiaMesPagin - 02.12.2010
        public DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes
                        , string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket
                        , string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.ListarTicketBoardingUsadosDiaMesPagin(sFchDesde, sFchHasta, sFchMes
                        , sTipoDocumento, sCodCompania, sNumVuelo, sTipTicket
                        , sDestino, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        //ListarResumenTicketVendidosCreditoPagin - 04.12.2010
        public DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxReporte.ListarResumenTicketVendidosCreditoPagin(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sCodPago, sFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        #endregion

    }
}
