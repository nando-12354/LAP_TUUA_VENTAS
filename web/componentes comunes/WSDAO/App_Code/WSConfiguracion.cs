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
/// Summary description for WSConfiguracion
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSConfiguracion : System.Web.Services.WebService
{

    public WSConfiguracion()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Parametros Generales

    [WebMethod]
    public DataTable ListarAllParametrosGenerales(string strParametro)
    {
        try
        {
            DAO_ParameGeneral objListarAllParameGeneral = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objListarAllParameGeneral.ListarAllParametroGeneral(strParametro);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable DetalleParametroGeneralxId(string sIdentificador)
    {
        try
        {
            DAO_ParameGeneral objListarAllParameGeneral = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objListarAllParameGeneral.DetalleParametroGeneralxIdentificador(sIdentificador);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
    {
        try
        {
            DAO_ParameGeneral objDAOParametroGene = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objDAOParametroGene.grabarParametroGeneral(sValoresFormulario, sValoresGrilla, sParametroVenta);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public ParameGeneral obtenerParametroGeneral(string sCodParam)
    {
        try
        {
            DAO_ParameGeneral objDAOParametroGene = new DAO_ParameGeneral(HttpContext.Current.Server.MapPath("."));
            return objDAOParametroGene.obtener(sCodParam);
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
    //esilva
    [WebMethod]
    public bool RegistrarListaDeCampo(ListaDeCampo objListaDeCampo, int intTipo)
    {
        bool bResult = false;
        try
        {
            DAO_ListaDeCampos objDAOListaDeCampo = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOListaDeCampo.insertar(objListaDeCampo, intTipo);//insert
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
    public bool actualizarHoras(ListaDeCampo objListaDeCampo)
    {
        bool bResult = false;
        try
        {
            DAO_ListaDeCampos objDAOListaDeCampo = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOListaDeCampo.actualizarHoras(objListaDeCampo);//update
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
    public bool actualizarestado(Sincronizacion objListaSincronizacion)
    {
        bool bResult = false;
        try
        {
            DAO_Sincronizacion objDAOSincronizacion = new DAO_Sincronizacion(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOSincronizacion.actualizarestado(objListaSincronizacion);//update
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
    public DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
    {
        try
        {
            DAO_ListaDeCampos objDAOListaDeCampos = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            return objDAOListaDeCampos.obtenerLista(strNomCampo, strCodCampo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public bool EliminarListaDeCampo(string strNomCampo, string strCodCampo)
    {
        bool bResult = false;
        try
        {
            DAO_ListaDeCampos objDAOListaDeCampo = new DAO_ListaDeCampos(HttpContext.Current.Server.MapPath("."));
            bResult = objDAOListaDeCampo.eliminar(strNomCampo, strCodCampo);//insert
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

}

