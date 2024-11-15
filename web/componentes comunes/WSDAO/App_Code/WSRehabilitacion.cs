using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;

/// <summary>
/// Summary description for WSRehabilitacion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSRehabilitacion : System.Web.Services.WebService
{

    public WSRehabilitacion()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable ConsultarRepresXRehabilitacionYCia(string strCia)
    {
        try
        {
            DAO_Representante objRepresentante = new DAO_Representante(HttpContext.Current.Server.MapPath("."));
            return objRepresentante.consultarRepresXRehabilitacionYCia(strCia);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool registrarRehabilitacionTicket(ref TicketEstHist objTicketEstHist, int flag, int sizeOutput)
    {
        try
        {
            DAO_TicketEstHist objDAOTicketEstHist = new DAO_TicketEstHist(HttpContext.Current.Server.MapPath("."));
            return objDAOTicketEstHist.insertarRehabilitacionTicket(objTicketEstHist, flag, sizeOutput);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable consultarVuelosTicketPorCiaFecha(String sCompania, String fechaVuelo)
    {
        try
        {
            DAO_Ticket objTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objTicket.consultarVuelosTicketPorCiaFecha(sCompania, fechaVuelo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public DataTable consultarTicketsPorVuelo(String sCompania, String fechaVuelo, String dsc_Num_Vuelo)
    {
        try
        {
            DAO_Ticket objTicket = new DAO_Ticket(HttpContext.Current.Server.MapPath("."));
            return objTicket.consultarTicketsPorVuelo(sCompania, fechaVuelo, dsc_Num_Vuelo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public DataTable consultarVuelosBCBPPorCiaFecha(String sCompania, String fechaVuelo)
    {
        try
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objBCBP.consultarVuelosBCBPPorCiaFecha(sCompania, fechaVuelo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool registrarRehabilitacionBCBP(ref BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
    {
        try
        {
            DAO_BoardingBcbpEstHist objDAOBoardingBcbpEstHist = new DAO_BoardingBcbpEstHist(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbpEstHist.insertarRehabilitacionBCBP(boardingBcbpEstHist, flag, sizeOutput);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public bool registrarRehabilitacionBCBPAmpliacion(ref BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
    {
        try
        {
            DAO_BoardingBcbpEstHist objDAOBoardingBcbpEstHist = new DAO_BoardingBcbpEstHist(HttpContext.Current.Server.MapPath("."));
            return objDAOBoardingBcbpEstHist.insertarRehabilitacionBCBP_Ampliacion(boardingBcbpEstHist, flag, sizeOutput);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }


    [WebMethod]
    public DataTable listarCompania_xCodigoEspecial(String codigoEspecial)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.listarCompania_xCodigoEspecial(codigoEspecial);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
    {
        try
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath("."));
            return objBCBP.obteneterBoardingsByRangoFechas(sCompania, sFchDesde, sFchHasta, sHoraDesde, sHoraHasta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

}