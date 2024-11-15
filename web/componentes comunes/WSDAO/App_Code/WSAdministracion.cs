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
/// Summary description for WSAdministracion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSAdministracion : System.Web.Services.WebService
{

    public WSAdministracion()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    #region Moneda
    [WebMethod]
    public DataTable listarAllMonedas()
    {
        try
        {
            DAO_Moneda objDAOMonedas = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));

            return objDAOMonedas.listarAllMonedas();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }

    [WebMethod]
    public DataTable obtenerDetalleMoneda(string sCodMoneda)
    {
        try
        {
            DAO_Moneda objDAODetalleMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));

            return objDAODetalleMoneda.obtenerDetalleMoneda(sCodMoneda);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public bool registrarTipoMoneda(Moneda objMoneda)
    {
        try
        {
            DAO_Moneda objDAOTipoMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoMoneda.insertar(objMoneda);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool actualizarTipoMoneda(Moneda objMoneda)
    {
        try
        {
            DAO_Moneda objDAOTipoMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoMoneda.actualizar(objMoneda);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool eliminarTipoMoneda(string sCodMoneda)
    {
        try
        {
            DAO_Moneda objDAOTipoMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoMoneda.eliminar(sCodMoneda);
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
    public List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
    {
        try
        {
            DAO_ListaDeCampos objListadeCampos = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            return objListadeCampos.obtenerListadeCampo(sNomCampo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public ListaDeCampo obtenerDatosListadeCampo(string sNomCampo, string sCodCampo)
    {
        try
        {
            DAO_ListaDeCampos objListadeCampos = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            return objListadeCampos.obtener(sNomCampo, sCodCampo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }

    #endregion

    #region TipoTicket

    [WebMethod]
    public bool registrarTipoTicket(TipoTicket objTipoTicket)
    {
        try
        {
            DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoTicket.insertar(objTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public bool actualizarTipoTicket(TipoTicket objTipoTicket)
    {
        try
        {
            DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOTipoTicket.actualizar(objTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public List<TipoTicket> listaDeTipoTicket()
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
    public DataTable ListarTipoTicket()
    {
        try
        {
            DAO_TipoTicket objTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objTipoTicket.listarAll();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public TipoTicket obtenerTipoTicket(string sCodTipoTicket)
    {
        try
        {
            DAO_TipoTicket objTipoTicket = new DAO_TipoTicket(HttpContext.Current.Server.MapPath("."));
            return objTipoTicket.obtener(sCodTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    #endregion

    #region Punto de Venta

    [WebMethod]
    public DataTable listarAllPuntoVenta()
    {
        try
        {
            DAO_EstacionPtoVta objDAOPuntoVenta = new DAO_EstacionPtoVta(HttpContext.Current.Server.MapPath("."));
            return objDAOPuntoVenta.listarAllPuntoVenta();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
    {
        try
        {
            DAO_EstacionPtoVta objDAODetallePuntoVenta = new DAO_EstacionPtoVta(HttpContext.Current.Server.MapPath("."));

            return objDAODetallePuntoVenta.obtenerDetallePuntoVenta(sCodEquipo, strIP);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
    {
        try
        {
            DAO_EstacionPtoVta objDAOPuntoVenta = new DAO_EstacionPtoVta(HttpContext.Current.Server.MapPath("."));
            return objDAOPuntoVenta.insertar(objPuntoVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
    {
        try
        {
            DAO_EstacionPtoVta objDAOPtoVta = new DAO_EstacionPtoVta(HttpContext.Current.Server.MapPath("."));
            return objDAOPtoVta.actualizar(objPuntoVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
    {
        try
        {
            DAO_EstacionPtoVta objDAOPtoVta = new DAO_EstacionPtoVta(HttpContext.Current.Server.MapPath("."));
            return objDAOPtoVta.eliminar(sCodEquipo, sLogUsuarioMod);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region Modalidad de Venta

    [WebMethod]
    public bool insertarModalidadVenta(ModalidadVenta objModalidad)
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.insertar(objModalidad);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool actualizarModalidadVenta(ModalidadVenta objModalidad)
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.actualizar(objModalidad);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.obtenerxCodigo(sCodModalidad);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.obtenerxNombre(sNomModalidad);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarAllModalidadVenta()
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.ListarAllModalidadVenta();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<ModalidadVenta> listarModalidadVenta()
    {
        try
        {
            DAO_ModalidadVenta objDAOModVta = new DAO_ModalidadVenta(HttpContext.Current.Server.MapPath("."));
            return objDAOModVta.listar();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    #endregion

    #region Atributos Modalidad de Venta
    [WebMethod]
    public bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModalidadAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModalidadAtrib.insertar(objModalidadAtrib);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModalidadAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModalidadAtrib.actualizar(objModalidadAtrib);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public  bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModalidadAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModalidadAtrib.eliminar(sCodModalidadVenta, sCodAtributo, sCodTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public  List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModalidadAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModalidadAtrib.ListarAtributosxModVentaTipoTicket(strCodModVenta, strTipoTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public int ValidarSerieTicket(int SerieIni, int SerieFin, string modalidad)
    {
        try
        {
            DAO_ModalidadAtrib objDAOModalidadAtrib = new DAO_ModalidadAtrib(HttpContext.Current.Server.MapPath("."));
            return objDAOModalidadAtrib.validarSerieTicket(SerieIni, SerieFin, modalidad);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region PrecioTicket
    [WebMethod]
    public DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
    {
        try
        {
            DAO_PrecioTicket objDAOPrecioTicket = new DAO_PrecioTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOPrecioTicket.obtener(strCodPrecioTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
    {
        bool bResult = false;
        try
        {
            DAO_PrecioTicket objDAOPrecioTicket = new DAO_PrecioTicket(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOPrecioTicket.insertar(objPrecioTicket);//insert
            return bResult;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool EliminarPrecioTicket(string strCodPrecioTicket)
    {
        try
        {
            DAO_PrecioTicket objDAOPrecioTicket = new DAO_PrecioTicket(HttpContext.Current.Server.MapPath("."));
            return objDAOPrecioTicket.eliminar(strCodPrecioTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region PrecioTicketHist
    [WebMethod]
    public DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
    {
        try
        {
            DAO_PrecioTicketHist objDAOPrecioTicketHist = new DAO_PrecioTicketHist(HttpContext.Current.Server.MapPath("."));
            return objDAOPrecioTicketHist.obtener(strCodPrecioTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
    {
        bool bResult = false;
        try
        {
            DAO_PrecioTicketHist objDAOPrecioTicketHist = new DAO_PrecioTicketHist(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOPrecioTicketHist.insertar(objPrecioTicketHist);//insert
            return bResult;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool EliminarPrecioTicketHist(string strCodPrecioTicket)
    {
        try
        {
            DAO_PrecioTicketHist objDAOPrecioTicketHist = new DAO_PrecioTicketHist(HttpContext.Current.Server.MapPath("."));
            return objDAOPrecioTicketHist.eliminar(strCodPrecioTicket);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region TasaCambio
    [WebMethod]
    public DataTable ObtenerTasaCambio(string strCodTasaCambio)
    {
        try
        {
            DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            return objDAOTasaCambio.obtener(strCodTasaCambio);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool RegistrarTasaCambio(TasaCambio objTasaCambio)
    {
        bool bResult = false;
        try
        {
            DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOTasaCambio.insertar(objTasaCambio);//insert
            return bResult;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool EliminarTasaCambio(string strCodTasaCambio)
    {
        try
        {
            DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            return objDAOTasaCambio.eliminar(strCodTasaCambio);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region TasaCambioHist
    [WebMethod]
    public DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
    {
        try
        {
            DAO_TasaCambioHist objDAOTasaCambioHist = new DAO_TasaCambioHist(HttpContext.Current.Server.MapPath("."));
            return objDAOTasaCambioHist.obtener(strCodTasaCambio);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    [WebMethod]
    public bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
    {
        bool bResult = false;
        try
        {
            DAO_TasaCambioHist objDAOTasaCambioHist = new DAO_TasaCambioHist(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOTasaCambioHist.insertar(objTasaCambioHist);//insert
            return bResult;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region Parametros Generales

    [WebMethod]
    public List<ParameGeneral> listarAtributosGenerales()
    {
        try
        {
            DAO_ParameGeneral objParameGeneral = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objParameGeneral.listarAtributosGenerales();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    #endregion

    #region Compañia
    
    [WebMethod]
    public bool insertarCompania(Compania objCompania)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.insertar(objCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    
    [WebMethod]
    public bool actualizarCompania(Compania objCompania)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.actualizar(objCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public Compania obtenerCompañiaxcodigo(string sCodigoCompania)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.obtenerxcodigo(sCodigoCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public Compania obtenerCompañiaxnombre(string sNombreCompania)
    {
        try
        {
            DAO_Compania objDAOCompania = new DAO_Compania(HttpContext.Current.Server.MapPath("."));
            return objDAOCompania.obtenerxnombre(sNombreCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
    {
        try
        {
            DAO_Representante objDAORepresentante = new DAO_Representante(HttpContext.Current.Server.MapPath("."));
            return objDAORepresentante.ValidarDocumento(sNombre, sApellido, sTpDocumento, sNroDocumento);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    #endregion

    #region Representante
    
    [WebMethod]
    public bool insertarRepresentante(RepresentantCia objRepresentante)
    {
        try
        {
            DAO_Representante objDAORepresentante = new DAO_Representante(HttpContext.Current.Server.MapPath("."));
            return objDAORepresentante.insertar(objRepresentante);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }
    
    [WebMethod]
    public bool actualizarRepresentante(RepresentantCia objRepresentante)
    {
        try
        {
            DAO_Representante objDAORepresentante = new DAO_Representante(HttpContext.Current.Server.MapPath("."));
            return objDAORepresentante.actualizar(objRepresentante);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region Modalidad Venta - Compañia
    
    [WebMethod]
    public bool insertarModVentaComp(ModVentaComp objModComp)
    {
        try
        {
            DAO_ModVentaComp objDAO_ModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAO_ModVentaComp.insertar(objModComp);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    
    [WebMethod]
    public bool insertarSecuenciaModVentaComp(string codCompania)
    {
        try
        {
            DAO_ModVentaComp objDAO_ModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAO_ModVentaComp.insertarSecuencia(codCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
    {
        try
        {
            DAO_ModVentaComp objDAO_ModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAO_ModVentaComp.eliminar(sCodCompania, sCodModalidadVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    
    [WebMethod]
    public List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
    {
        try
        {
            DAO_ModVentaComp objDAO_ModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAO_ModVentaComp.ListarModVentaCompxCompañia(sCodCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public int validarAnulacionModalidad(string sModalidad, string sCompania)
    {
        try
        {
            DAO_ModVentaComp objDAO_ModVentaComp = new DAO_ModVentaComp(HttpContext.Current.Server.MapPath("."));
            return objDAO_ModVentaComp.validarAnulacionModalidad(sModalidad, sCompania);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    #endregion

    #region Modalidad Venta - Compañia - Atributos
    
    [WebMethod]
    public bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
    {

        try
        {
            DAO_ModVentaCompAtr objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaCompAtr.insertar(objRModCompAtr);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    
    [WebMethod]
    public bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
    {

        try
        {
            DAO_ModVentaCompAtr objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaCompAtr.actualizar(objRModCompAtr);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }
    
    [WebMethod]
    public bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
    {
        try
        {
            DAO_ModVentaCompAtr objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaCompAtr.eliminar(sCodCompania, sCodModalidadVenta, CodAtributo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }
   
    [WebMethod]
    public List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
    {
        try
        {
            DAO_ModVentaCompAtr objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaCompAtr.ObtenerModVentaCompAtr(sCodCompania, sCodModalidadVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public int ValidarSerieTicketCompa(int SerieIni, int SerieFin, string modalidad, string compania)
    {
        try
        {
            DAO_ModVentaCompAtr objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(HttpContext.Current.Server.MapPath("."));
            return objDAOModVentaCompAtr.validarSerieTicketCompa(SerieIni, SerieFin, modalidad, compania);
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

