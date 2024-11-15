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
/// Summary description for WSAlarmas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WSAlarmas : System.Web.Services.WebService
{

    public WSAlarmas()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region ConfigAlarma

    [WebMethod]
    public bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma)
    {
        try
        {
            DAO_CnfgAlarma objDAOCnfgAlarma = new DAO_CnfgAlarma(HttpContext.Current.Server.MapPath("."));
            return objDAOCnfgAlarma.insertar(objCnfgAlarma);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma)
    {
        try
        {
            DAO_CnfgAlarma objDAOCnfgAlarma = new DAO_CnfgAlarma(HttpContext.Current.Server.MapPath("."));
            return objDAOCnfgAlarma.actualizar(objCnfgAlarma);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
    {
        try
        {
            DAO_CnfgAlarma objDAOCnfgAlarma = new DAO_CnfgAlarma(HttpContext.Current.Server.MapPath("."));
            return objDAOCnfgAlarma.eliminar(sCodAlarma, sCodModulo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarAllCnfgAlarma()
    {
        try
        {
            DAO_CnfgAlarma objDAOCnfgAlarma = new DAO_CnfgAlarma(HttpContext.Current.Server.MapPath("."));
            return objDAOCnfgAlarma.ListarAllCnfgAlarma();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
    {
        try
        {
            DAO_CnfgAlarma objDAOCnfgAlarma = new DAO_CnfgAlarma(HttpContext.Current.Server.MapPath("."));
            return objDAOCnfgAlarma.obtener(sCodAlarma, sCodModulo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    #endregion

    #region Alarmas Generadas

    [WebMethod]
    public bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.insertar(objAlarmaGenerada);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.actualizar(objAlarmaGenerada);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public DataTable ListarAllAlarmaGenerada()
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.ListarAllAlarmaGenerada();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    [WebMethod]
    public AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.obtener(sCodAlarmaGenerada);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarAlarmaGeneradaEnviadas()
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.ListarAlarmaGeneradaEnviadas();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
    {
        try
        {
            DAO_AlarmaGenerada objDAOAlarmaGenerada = new DAO_AlarmaGenerada(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarmaGenerada.ConsultaAlarmaGenerada(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sModulo, sTipoAlarma, sEstado);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region Alarmas

    [WebMethod]
    public DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
    {
        try
        {
            DAO_Alarma objDAOAlarma = new DAO_Alarma(HttpContext.Current.Server.MapPath("."));
            return objDAOAlarma.ObtenerAlarmaxCodModulo(sCodModulo);
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

