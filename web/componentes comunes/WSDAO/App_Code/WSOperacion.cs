/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Proceso de gestión de turnos.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/
using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Collections.Generic;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Data;

/// <summary>
/// Summary description for WSOperacion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSOperacion : System.Web.Services.WebService
{

    public WSOperacion()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool RegistrarOperacion(LogOperacion objOperacion)
    {
        try
        {
            DAO_LogOperacion objDAOOperacion = new DAO_LogOperacion(HttpContext.Current.Server.MapPath("."));
            return objDAOOperacion.insertar(objOperacion);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool RegistrarOpeCaja(List<LogOperacCaja> objListaOperaCaja)
    {
        try
        {
            DAO_LogOperacCaja objDAOOperacion = new DAO_LogOperacCaja(HttpContext.Current.Server.MapPath("."));
            for (int i = 0; i < objListaOperaCaja.Count; i++)
            {
                if (!objDAOOperacion.insertar(objListaOperaCaja[i]))
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
    {
        try
        {
            DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            return objDAOTasaCambio.ObtenerUltimoPorMoneda(strMoneda, strTipo);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<Moneda> ListarMonedasInter()
    {
        try
        {
            DAO_Moneda objDAOMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOMoneda.ListarMonedasInter();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
    {
        try
        {
            DAO_LogCompraVenta objDAOCompVenta = new DAO_LogCompraVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOCompVenta.insertar(objCompraVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<Limite> ListarLimitesPorOperacion(string stipoope)
    {
        try
        {
            DAO_Limite objDAOLimite = new DAO_Limite(HttpContext.Current.Server.MapPath("."));
            return objDAOLimite.ListarPorOperacion(stipoope);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
    {
        try
        {
            DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoTicket.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public TipoTicket ValidarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
    {
        try
        {
            DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoTicket.validarTipoTicket(strTipoVuelo, strTipoPas, strTipoTrans);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.consultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
    {
        try
        {
            DAO_VueloProgramado objDAOVueloProg = new DAO_VueloProgramado(HttpContext.Current.Server.MapPath("."));
            return objDAOVueloProg.ListarVuelosxCompania(strCompania, strFecha, strTipVuelo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            for (int i = 0; i < listaTickets.Count; i++)
            {
                if (!objDAOTicket.insertar(listaTickets[i]))
                {
                    return false;
                }
            }
            strListaTickets = listaTickets[0].SCodNumeroTicket;
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool GenerarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, 
                              string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, 
                              string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa,
                              string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.insertar(strCompania, strVentaMasiva, strNumVuelo, strFecVuelo, strTurno, strUsuario, decPrecio, strMoneda, strModVenta, 
                                         strTipTicket, strTipVuelo, intTickets, strFlagCont, strNumRef, strTipPago, strEmpresa, strRepte, ref  strFecVence,
                                         ref strListaTickets, strCodTurnoIng, strFlgCierreTurno, strCodPrecio);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<RepresentantCia> ListarRepteCia(string strCia)
    {
        try
        {
            DAO_Representante objRepteCia = new DAO_Representante(HttpContext.Current.Server.MapPath("."));
            return objRepteCia.ListarReptexCia(strCia);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
    {
        try
        {
            DAO_VentaMasiva objDAOVentaMasiva = new DAO_VentaMasiva(HttpContext.Current.Server.MapPath("."));
            return objDAOVentaMasiva.insertar(objVentaMasiva);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
    {
        try
        {
            DAO_VentaMasiva objDAOVentaMasiva = new DAO_VentaMasiva(HttpContext.Current.Server.MapPath("."));
            return objDAOVentaMasiva.actualizar(objVentaMasiva);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
    {
        try
        {
            DAO_ModVentaComp objDAOModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaComp.ListarCompaniaxModVenta(strCodModVenta, strTipComp);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModAtrib.ListarAtributosxModVenta(strCodModVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModAtrib.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<TipoTicket> ListarAllTipoTicket()
    {
        try
        {
            DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoTicket.listar();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
    {
        try
        {
            DAO_LogOperacion objDAOLogOpera = new DAO_LogOperacion(HttpContext.Current.Server.MapPath("."));
            if (!objDAOLogOpera.ExtornarCompraVenta(strCodOpera, strTurno, intCantidad, ref strMessage))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            if (!objDAOTicket.ExtornarTicket(strListaTickets, strTurno, intCantidad, strUsuario, strMotivo, ref strMessage))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
    {
        try
        {
            string strNumTicket = "";
            int intLongTicket = strListaTickets.Length / intCantidad;
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            for (int i = 0; i < intCantidad; i++)
            {
                strNumTicket = strListaTickets.Substring(i * intLongTicket, intLongTicket);
                Ticket objTicket = new Ticket(strNumTicket, null, null, null, null, strEstado, null, null, strUsuario, 0, null, null, null, 0, null);
                if (!objDAOTicket.actualizar(objTicket))
                {
                    return false;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario, string strMessage)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            if (!objDAOTicket.Extender(strListaTickets, strListaFechas, strListaFechas.Trim().Length / 8, ref strMessage))
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ActualizarTicket(List<Ticket> listaTickets)
    {
        DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
        for (int i = 0; i < listaTickets.Count; i++)
        {
            if (!objDAOTicket.actualizar(listaTickets[i]))
            {
                return false;
            }
        }
        return true;
    }

    [WebMethod]
    public bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
    {
        DAO_TicketEstHist objDAO_TicketEstHist = new DAO_TicketEstHist(HttpContext.Current.Server.MapPath("."));
        if (!objDAO_TicketEstHist.AnularTicket(sCodNumeroTicket, sDscMotivo, sUsuarioMod))
        {
            return false;
        }
        return true;
    }

    [WebMethod]
    public bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.actualizarEstado(objBoardingBcbp);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool AnularBCBP(BoardingBcbp objBoardingBcbp)
    {
        try
        {
            DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbp.AnularBCBP(objBoardingBcbp);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ObtenerVentaTicket(strFecIni, strFecFin, strTipVenta, strFlgAero);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ObtenerComprobanteSEAE(string sAnio, string sMes,string sTDocumento, string strTipVenta, string strFlgAero)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento, strTipVenta, strFlgAero);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool AnularTuua(string strListaTicket, int intTicket)
    {
        DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
        if (!objDAOTicket.AnularTuua(strListaTicket, intTicket))
        {
            return false;
        }
        return true;
    }

    #region Molinete

    [WebMethod]
    public DataTable ListarAllMolinete(string strCodMolinete, string strDscIp)
    {
        try
        {
            DAO_Molinete objListaAllMolinete = new DAO_Molinete(HttpContext.Current.Server.MapPath("."));
            return objListaAllMolinete.listarAllMolinete(strCodMolinete, strDscIp);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ActualizarMolinete(Molinete objMolinete)
    {
        try
        {
            DAO_Molinete objDAOMolinete = new DAO_Molinete(HttpContext.Current.Server.MapPath("."));
            return objDAOMolinete.actualizar(objMolinete);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ActualizarUnMolinete(Molinete objMolinete)
    {
        try
        {
            DAO_Molinete objDAOMolinete = new DAO_Molinete(HttpContext.Current.Server.MapPath("."));
            return objDAOMolinete.actualizarUnMolinete(objMolinete);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarAllMolinetes()
    {
        try
        {
            DAO_Molinete objListaAllMolinete = new DAO_Molinete(HttpContext.Current.Server.MapPath("."));
            return objListaAllMolinete.listarAllMolinete();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerMolinete(String strCodMolinete)
    {
        try
        {
            DAO_Molinete objListaAllMolinete = new DAO_Molinete(HttpContext.Current.Server.MapPath("."));
            return objListaAllMolinete.obtenerMolinete(strCodMolinete);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    [WebMethod]
    public bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.RegistrarVentaContingencia(strCompania, strNumVuelo, strUsuario, strMoneda, strTipTicket, strFecVenta, intTickets, strListaTickets, strCodTurno, strFlagTurno, ref strCodTurnoCreado, ref strCodError);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ListarContingencia(strTipTikcet, strFlgConti, strNumIni, strNumFin, strUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    [WebMethod]
    public string ObtenerFechaActual()
    {
        try
        {
            DAO_Ticket objDAOTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objDAOTicket.ObtenerFechaActual();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
    {
        try
        {
            DAO_BoardingBcbp objDAOBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBcbp.ListarBcbpxConciliar(sCodCompania, strFchVuelo, strNumVuelo, strNumAsiento, strPasajero, strFecUsoIni, strFecUsoFin, strFlg);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
    {
        try
        {
            DAO_BoardingBcbp objDAOBcbp = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objDAOBcbp.RegistrarBcbpxConciliar(sBcbpBase, sBcbpUlt, sBcbpAsoc);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
    {
        try
        {
            DAO_Turno objDAOTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objDAOTurno.ObtenerDetalleTurnoActual(strCodUsuario, ref strCantTickets, ref strCodTurno, ref strFecHorTurno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

}

