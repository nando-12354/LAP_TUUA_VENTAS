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
/// Summary description for WSConsultas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSConsultas : System.Web.Services.WebService
{

    public WSConsultas()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    #region Turno
    /// <summary>
    /// Obtener todos los campos del turno segun el codigo
    /// </summary>
    /// <param name="as_codturno"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable ListarAllTurno(string as_codturno)
    {
        try
        {
            DAO_Turno objListaTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objListaTurno.listarAllTurno(as_codturno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }
    /// <summary>
    /// Obtiene la cantidad de monedas por un turno
    /// </summary>
    /// <param name="as_codturno"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable CantidadMonedasTurno(string as_codturno)
    {
        try
        {
            DAO_Turno objCantidadMonedasTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objCantidadMonedasTurno.cantidadmonedaxTurno(as_codturno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    /// <summary>
    /// Define el detalle de monedas generados en un turno
    /// </summary>
    /// <param name="as_codturno"></param>
    /// <param name="as_codmoneda"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_iddetalle)
    {
        try
        {
            DAO_Turno objDetalleCantidadMonedasTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objDetalleCantidadMonedasTurno.detallemonedaxTurno(as_codturno, as_codmoneda, as_iddetalle);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }

    [WebMethod]
    public DataTable listarTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_HoraDesde, string as_HoraHasta,string as_FlgReporte)
    {
        try
        {
            DAO_Turno objListaTurnoFiltro = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objListaTurnoFiltro.consultarTurnoxFiltro(as_fchIni, as_fchFin, as_codusuario, as_ptoventa, as_HoraDesde, as_HoraHasta,"0");
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

    [WebMethod]
    public DataTable ConsultaUsuariosxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
    {
        try
        {
            DAO_Usuario objConsultaUsuarioxFiltro = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));
            return objConsultaUsuarioxFiltro.consultarUsuarioxFiltro(as_rol, as_estado, as_grupo, sfiltro, sOrdenacion);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable listarAllUsuario()
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));
            return objDAOUsuario.ListarAllUsuario();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable ListarUsuarioxRol(string sCod_Rol) 
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));
            return objDAOUsuarioRol.ListarUsuarioRol(sCod_Rol);
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
    [WebMethod]
    public DataTable listarSincronizacion()
    {
        try
        {
            DAO_Sincronizacion objDAOSincronizacion = new DAO_Sincronizacion(HttpContext.Current.Server.MapPath("."));
            return objDAOSincronizacion.ListarSincronizacion();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }
    [WebMethod]
    public DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, 
        string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sfiltro, string sOrdenacion)
    {
        try
        {
            DAO_Sincronizacion objDAOSincronizacion = new DAO_Sincronizacion(HttpContext.Current.Server.MapPath("."));
            return objDAOSincronizacion.ListarFiltroSincronizacion(as_molinete, as_estado,
                as_TipoSincronizacion, as_TablaSincronizacion, strFchDesde, strFchHasta, strHraDesde, 
                strHraHasta,sfiltro, sOrdenacion);
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

    [WebMethod]
    public DataTable ListarFiltroDepuracion(string as_molinete, string as_estado,
         string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sfiltro, string sOrdenacion)
    {
        try
        {
            DAO_Depuracion objDAODepuracion = new DAO_Depuracion(HttpContext.Current.Server.MapPath("."));
            return objDAODepuracion.ListarFiltroDepuracion(as_molinete, as_estado,
                 as_TablaSincronizacion, strFchDesde, strFchHasta, strHraDesde,
                strHraHasta, 
                sfiltro, sOrdenacion);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }




    #endregion

    #region Compania
    [WebMethod]
    public DataTable ListarAllCompanias()
    {
        try
        {
            DAO_Compania objListaAllCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objListaAllCompania.listarAllCompania();

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }



    [WebMethod]
    public DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
    {
        try
        {
            DAO_Compania objConsultaConpaniaxFiltro = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objConsultaConpaniaxFiltro.consultarCompaniaxFiltro(strEstado, strTipo, sfiltro, sOrdenacion);

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
    [WebMethod]
    public DataTable ListarParametrosGeneral(string as_identificador)
    {
        try
        {
            DAO_ParameGeneral objListaParametros = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objListaParametros.ObtenerParametroGeneral(as_identificador);

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
    [WebMethod]
    public DataTable ListarDetalleTicket(string as_numeroticket, string as_ticketdesde, string as_tickethasta)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarDetalleTicket(as_numeroticket, as_ticketdesde, as_tickethasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable ListarDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_tickethasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarDetalleTicketPagin(as_numeroticket, as_ticketdesde, as_tickethasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    [WebMethod]
    public DataTable ListarDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickethasta)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarDetalleTicket_Reh(as_numeroticket, as_ticketdesde, as_tickethasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_tickethasta, string as_flgtotal)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.ConsultaDetalleTicket2_Reh(as_numeroticket, as_ticketdesde, as_tickethasta, as_flgtotal);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarDetalleTicket_Ope(sNumTicket, sTicketDesde, sTicketHasta, stipoticket, sTickets_Sel, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarTicketxFecha(as_FchDesde, as_FchHasta, as_CodCompania, as_TipoTicket, as_EstadoTicket, as_TipoPersona, as_filtro, as_Ordenacion, as_HoraDesde, as_HoraHasta, strTurno);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarTicketxFechaPagin(
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
        string sTotalRows
        )
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarTicketxFechaPagin(as_TipDoc,
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

    [WebMethod]
    public DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.consultarTicketxFecha_Reh(as_FchDesde, as_FchHasta, as_CodCompania, as_TipoTicket, as_EstadoTicket, as_TipoPersona, as_filtro, as_Ordenacion, as_HoraDesde, as_HoraHasta, strTurno);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta, string as_TipoDocumento,string as_FlagAnulado)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.obtenerCuadreTicketsEmitidos(as_FchDesde, as_FchHasta, as_TipoDocumento, as_FlagAnulado);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta)
    {
        try
        {
            DAO_Ticket objListaDetalleTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaDetalleTicket.obtenerTicketsAnulados(as_FchDesde, as_FchHasta);

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

    [WebMethod]
    public DataTable ListarTicketEstHist(string as_numeroticket)
    {
        try
        {
            DAO_TicketEstHist objListaTicketEstHist = new DAO_TicketEstHist(HttpContext.Current.Server.MapPath("."));
            return objListaTicketEstHist.listarTicketEstHist(as_numeroticket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarTicketEstHist_Arch(string as_numeroticket)
    {
        try
        {
            DAO_TicketEstHist objListaTicketEstHist = new DAO_TicketEstHist(HttpContext.Current.Server.MapPath("."));
            return objListaTicketEstHist.listarTicketEstHist_Arch(as_numeroticket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable obtenerTicketBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo,string sEstado)
    {
        try
        {
            DAO_TicketEstHist objListaTicketEstHist = new DAO_TicketEstHist(HttpContext.Current.Server.MapPath("."));
            return objListaTicketEstHist.obtenerTicketBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
    {
        try
        {
            DAO_BoardingBcbp objListaBoarding = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objListaBoarding.obtenerBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sBoardingSel, sCodCompania, sNumVuelo, sNumAsiento, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotal);
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

    [WebMethod]
    public DataTable ListarCampoxNombre(string as_nombre)
    {
        try
        {
            DAO_ListaDeCampos objListaCampoxNombre = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            return objListaCampoxNombre.obtenerListaxNombre(as_nombre);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable ListarCampoxNombreOrderByDesc(string as_nombre)
    {
        try
        {
            DAO_ListaDeCampos objListaCampoxNombre = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            return objListaCampoxNombre.obtenerListaxNombreOrderByDesc(as_nombre);
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
    [WebMethod]
    public DataTable ListarRoles()
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.obtenerALLRol();
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
    public DataTable DetalleBoarding(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.DetalleBoarding(strCodCompania, strNumVuelo, strFechVuelo, strNumAsiento, strPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable DetalleBoardingPagin(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.DetalleBoardingPagin(strCodCompania, strNumVuelo, strFechVuelo, strNumAsiento, strPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, sColumnSort, iIniRows, iMaxRows, sTotalRows);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable DetalleBoarding_REH(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string CheckSeqNumber)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.DetalleBoarding_REH(strCodCompania, strNumVuelo, strFechVuelo, strNumAsiento, strPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, Flag_Fch_Vuelo, CheckSeqNumber);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable DetalleBoardingArchivado(String Num_Secuencial_Bcbp)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.DetalleBoardingArchivado(Num_Secuencial_Bcbp);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarBoardingAsociados(String Num_Secuencial_Bcbp)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.ListarBoardingAsociados(Num_Secuencial_Bcbp);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.validarAsocBCBP(sNumAsiento, sNumVuelo, sFchVuelo, sNomPersona, sCompania, sCodBcbpBase);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.obtenerDetalleWebBCBP(sCodCompania, sNroVuelo, sFchVuelo, sAsiento, sPasajero);
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

    [WebMethod]
    public DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde,
        string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding,
        string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort,
        int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
    {
        try
        {
            DAO_BoardingBcbpErr objDAOBoardingErr = new DAO_BoardingBcbpErr(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingErr.ListarLogErroresMolinete(sFechaDesde, sFechaHasta, sHoraHasta, sHoraHasta, sIDError, sTipoError, sCompania, sCodMolinete, sTipoBoarding, sTipIngreso, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
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

    [WebMethod]
    public DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
    {
        try
        {
            DAO_BoardingBcbpEstHist objDAOBoardingEstHist = new DAO_BoardingBcbpEstHist(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingEstHist.DetalleBoardingEstHist(Num_Secuencial_Bcbp);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable DetalleBoardingEstHist_Arch(String Num_Secuencial_Bcbp)
    {
        try
        {
            DAO_BoardingBcbpEstHist objDAOBoardingEstHist = new DAO_BoardingBcbpEstHist(HttpContext.Current.Server.MapPath("."));
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

    #region LogOperacion

    [WebMethod]
    public DataTable ObtenerUsuarioxFechaOperacion(string strFechaOperacion, string strCodUsuario, string strTipoOperacion, string strCodMoneda)
    {
        try
        {
            DAO_LogOperacion objDAOLogOperacion = new DAO_LogOperacion(HttpContext.Current.Server.MapPath("."));
            return objDAOLogOperacion.obtenerUsuarioxFechaOperacion(strFechaOperacion, strCodUsuario, strTipoOperacion, strCodMoneda);

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

    [WebMethod]
    public DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
    {
        try
        {
            DAO_VueloProgramado objDAOVueloProgramado = new DAO_VueloProgramado(HttpContext.Current.Server.MapPath("."));
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

    [WebMethod]
    public DataTable FiltrosAuditorias(string strCodModulo, string strFlgSubModulo, string strTablaXml)
    {
        try
        {
            DAO_Auditoria objDAOFiltroAuditoria = new DAO_Auditoria(HttpContext.Current.Server.MapPath("."));
            return objDAOFiltroAuditoria.FiltrosAuditoria(strCodModulo, strFlgSubModulo, strTablaXml);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable ObtenerConsultaAuditorias(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta)
    {
        try
        {
            DAO_Auditoria objDAOFiltroAuditoria = new DAO_Auditoria(HttpContext.Current.Server.MapPath("."));
            return objDAOFiltroAuditoria.obtenerconsultaAuditoria(strTipOperacion, 
                strTabla, strCodModulo, strCodSubModulo, strCodUsuario, 
                strFchDesde, strFchHasta,strHraDesde,strHraHasta);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ObtenerConsultaAuditoriasPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
    {
        try
        {
            DAO_Auditoria objDAOFiltroAuditoria = new DAO_Auditoria(HttpContext.Current.Server.MapPath("."));
            return objDAOFiltroAuditoria.obtenerconsultaAuditoriaPagin(strTipOperacion, strTabla, strCodModulo, strCodSubModulo, strCodUsuario, strFchDesde, strFchHasta, strHraDesde, strHraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    [WebMethod]
    public DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador)
    {
        try
        {
            DAO_Auditoria objDAOFiltroAuditoria = new DAO_Auditoria(HttpContext.Current.Server.MapPath("."));
            return objDAOFiltroAuditoria.obtenerdetalleAuditoria(strNombreTabla, strContador);

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
    /// <summary>
    /// ListarTurnoxFiltro2 - kinzi
    /// </summary>
    /// <param name="strFchIni"></param>
    /// <param name="strFchFin"></param>
    /// <param name="strCodUsuario"></param>
    /// <param name="strCodTurno"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable ListarTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
    {
        try
        {
            DAO_Turno objListaTurnoFiltro = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objListaTurnoFiltro.obtener(strFchIni, strFchFin, strCodUsuario, strCodTurno);
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
    [WebMethod]
    public DataTable ListarTicketProcesado(string strCodTurno)
    {
        try
        {
            DAO_Ticket objListaTicketProc = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaTicketProc.obtenerTicketProcesado(strCodTurno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    /// <summary>
    /// ListarTicketVendido - kinzi
    /// </summary>
    /// <param name="strTipo"></param>
    /// <param name="strFecIni"></param>
    /// <param name="strHorIni"></param>
    /// <param name="strFecFin"></param>
    /// <param name="strHorFin"></param>
    /// <param name="strTurnoIni"></param>
    /// <param name="strTurnoFin"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
    {
        try
        {
            DAO_Ticket objListaTicketProc = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objListaTicketProc.obtenerTicketVendido(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    /// <summary>
    /// ListarResumenCompraVenta - kinzi
    /// </summary>
    /// <param name="strTipo"></param>
    /// <param name="strFecIni"></param>
    /// <param name="strHorIni"></param>
    /// <param name="strFecFin"></param>
    /// <param name="strHorFin"></param>
    /// <param name="strTurnoIni"></param>
    /// <param name="strTurnoFin"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
    {
        try
        {
            DAO_LogOperacion objListaResumen = new DAO_LogOperacion(HttpContext.Current.Server.MapPath("."));
            return objListaResumen.obtenerOperacionCompraVenta(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    /// <summary>
    /// ListarResumenTasaCambio - kinzi
    /// </summary>
    /// <param name="strTipo"></param>
    /// <param name="strFecIni"></param>
    /// <param name="strHorIni"></param>
    /// <param name="strFecFin"></param>
    /// <param name="strHorFin"></param>
    /// <param name="strTurnoIni"></param>
    /// <param name="strTurnoFin"></param>
    /// <returns></returns>
    [WebMethod]
    public DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
    {
        try
        {
            DAO_TasaCambio objListaResumen = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            return objListaResumen.obtenerResumenTasaCambio(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    //ListarTicketBoardingUsadosPagin - 03.09.2010
    [WebMethod]
    public DataTable ListarTicketBoardingUsadosPagin(string sFechaDesde,string sFechaHasta,string sHoraDesde,string sHoraHasta,string sCodCompania,string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado, string sColumnSort,int iIniRows,int iMaxRows,string sTotalRows)
    {
        try
        {
            DAO_Ticket objTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objTicket.consultarTicketBoardingUsadosPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    //ListarTicketBoardingUsadosResumen - 08.09.2010
    [WebMethod]
    public DataTable ListarTicketBoardingUsadosResumen(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado)
    {
        try
        {
            DAO_Ticket objTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objTicket.consultarTicketBoardingUsadosResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public DataTable ListarParametrosGeneralDefaultValue(string as_identificador)
    {
        try
        {
            DAO_ParameGeneral objListaParametros = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objListaParametros.ObtenerParametroGeneralDefaultValue(as_identificador);
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

