using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for WSReportes
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSReportes : System.Web.Services.WebService
{

    public WSReportes()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Ticket

    [WebMethod]
    public DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarMovimientoTicketContingencia(sFchDesde, sFchHasta, sEstado, sTipoTicket, sEstadoTicket, sRangoMinTicket, sRangoMaxTicket);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarResumenStockTicketContingencia(sTipoTicket, sFchAl, sTipoResumen);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
 	{
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarTicketBoardingUsados(sCodCompania, sNumVuelo, sTipoDocumento, sTipoTicket, sTipoFiltro, sFechaInicial, sFechaFinal, sTimeInicial, sTimeFinal, sDestino, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarResumenTicketVendidosCredito(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sCodPago);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }




    [WebMethod]
    public DataTable obtenerRecaudacionMensual(string sAnio)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarRecaudacionMensual(sAnio);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ConsultarLiquidVenta(strFecIni, strFecFin);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ConsultarUsoContingencia(strFecIni, strFecFin);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ConsultarTicketVencidos(strFecIni, strFecFin, strTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarDetalleVentaCompania(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarLiquidacionVenta(sFchDesde, sFchHasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarLiquidacionVentaResumen(sFchDesde, sFchHasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarTicketBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sDocumento, sTicket, sAerolinea, sVuelo, sMotivo);
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

    [WebMethod]
    public DataTable obtenerBoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni,string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen)
    {
        try
        {
            DAO_BoardingBcbp objListaBoardingLeidosMolinete = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objListaBoardingLeidosMolinete.BoardingLeidosMolinete(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoarding = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoarding.consultarBoardingRehabilitados(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sCompania, sMotivo, sTipoVuelo, sTipoPersona, sNumVuelo, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoarding = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoarding.consultarBoardingPassDiario(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoPasajero, sTipoVuelo, sTipoTrasbordo, sFechaVuelo, sNumVuelo, sPasajero, sNumAsiento, sCodIata, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    } 

    #endregion

    #region
    [WebMethod]
    public DataTable ObtenerEstadistico()
    {
        try
        {
            DAO_Auditoria objListaEstadistico = new DAO_Auditoria(HttpContext.Current.Server.MapPath("."));
            return objListaEstadistico.ObtenerEstadistico();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region Additional Methods - kinzi
    //ListarResumenCompaniaPagin - 05.10.2010
    [WebMethod]
    public System.Data.DataTable ListarResumenCompaniaPagin(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarDetalleVentaCompaniaPagin(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    //ListarTicketVencidosPagin - 21.10.2010
    [WebMethod]
    public DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarTicketVencidosPagin(strFecIni, strFecFin, strTipoTicket, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    //ListarBoardingLeidosMolinetePagin - 01.12.2010
    [WebMethod]
    public DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin,
                                      string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoarding = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoarding.consultarBoardingLeidosMolinetePagin(strCodCompania, strFechVuelo, strNum_Vuelo, strFechaLecturaIni, strFechaLecturaFin, strCodEstado, strNumBoarding, strFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    //ListarTicketBoardingUsadosDiaMesPagin - 02.12.2010
    [WebMethod]
    public DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes
                        , string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket
                        , string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarTicketBoardingUsadosDiaMesPagin(sFchDesde, sFchHasta, sFchMes
                        , sTipoDocumento, sCodCompania, sNumVuelo, sTipTicket
                        , sDestino, sTipReporte, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    //ListarResumenTicketVendidosCreditoPagin - 04.12.2010
    [WebMethod]
    public DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.consultarResumenTicketVendidosCreditoPagin(sFechaInicial, sFechaFinal, sTipoTicket, sNumVuelo, sAeroLinea, sCodPago, sFlagResumen, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion
}

