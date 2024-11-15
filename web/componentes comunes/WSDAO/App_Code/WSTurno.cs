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
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSTurno : System.Web.Services.WebService
{
    public WSTurno () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public Usuario autenticar(string sCuenta, string sClave)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));
            return objDAOUsuario.autenticar(sCuenta, sClave);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool crearTurno(string as_cod_secuencial, string as_cod_usuario, string as_cod_equipo, ref string strTurnoError)
    {
        try
        {
            DAO_Turno objDAOTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objDAOTurno.crear(as_cod_secuencial, as_cod_usuario, as_cod_equipo, ref strTurnoError);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool ActualizarTurno(Turno objTurno)
    {
        try
        {
            DAO_Turno objDAOTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objDAOTurno.actualizar(objTurno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public Turno obtenerTurnoIniciado(string as_usuario)
    {
        try
        {
            DAO_Turno objDAOTurno = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            return objDAOTurno.obtenerTurnoIniciado(as_usuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool registrarTurnoMonto(List<TurnoMonto> listaMontos)
    {
        try
        {
            DAO_TurnoMonto objDaoTMonto = new DAO_TurnoMonto(HttpContext.Current.Server.MapPath("."));
            for (int i = 0; i < listaMontos.Count; i++)
            {
                if (!objDaoTMonto.insertar(listaMontos[i]))
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
    public bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos)
    {
        try
        {
            DAO_TurnoMonto objDaoTMonto = new DAO_TurnoMonto(HttpContext.Current.Server.MapPath("."));
            for (int i = 0; i < listaMontos.Count; i++)
            {
                if (!objDaoTMonto.actualizar(listaMontos[i]))
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
    public bool verificarTurnoCerradoxUsuario(string as_usuario)
    {
        try
        {
            DAO_Turno objDaoTMonto = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            if (objDaoTMonto.selectCountTurnoAbiertoxUsuario(as_usuario) > 0)
                return false;
            else return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool verificarTurnoCerradoxPtoVenta(string as_ptoventa)
    {
        try
        {
            DAO_Turno objDaoTMonto = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            if (objDaoTMonto.selectCountTurnoAbiertoxPtoVenta(as_ptoventa) > 0)
                return false;
            else return true;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<Moneda> ListarMonedas()
    {
        try
        {
            DAO_Moneda objDAOMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOMoneda.listar();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public List<Moneda> ListarMonedasxTipoTicket()
    {
        try
        {
            DAO_Moneda objDAOMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
            return objDAOMoneda.listarMonedasxTipoTicket();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }
    //public DataTable ListarMonedas()
    //{
    //    DAO_Moneda objDAOMoneda = new DAO_Moneda(HttpContext.Current.Server.MapPath("."));
    //    return objDAOMoneda.listar();
    //}

    [WebMethod]
    public List<TurnoMonto> ListarTurnoMontosPorTurno(string scodturno)
    {
        try
        {
            DAO_TurnoMonto objDAOTurnoMonto = new DAO_TurnoMonto(HttpContext.Current.Server.MapPath("."));
            return objDAOTurnoMonto.listar(scodturno);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarTasaCambio()
    {
        try
        {
            DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(HttpContext.Current.Server.MapPath("."));
            return objDAOTasaCambio.ListarTasaCambio();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
    {
        try
        {
            DataTable  isResult;
            DAO_Turno objDaoTMonto = new DAO_Turno(HttpContext.Current.Server.MapPath("."));
            isResult = objDaoTMonto.ListarCuadreTurno(strMoneda, strTurno, ref  decEfectivoIni, ref  intTicketInt, ref  intTicketNac, ref  decTicketInt, ref  decTicketNac, ref  intIngCaja, ref  decIngCaja, ref  intVentaMoneda, ref  decVentaMoneda, ref  intEgreCaja, ref  decEgreCaja, ref  intCompraMoneda, ref  decCompraMoneda, ref  decEfectivoFinal, ref  intAnulaInt, ref  intAnulaNac, ref  intInfanteInt, ref  intInfanteNac, ref  intCreditoInt, ref  intCreditoNac, ref  decCreditoInt, ref  decCreditoNac);
            return isResult;
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

}
