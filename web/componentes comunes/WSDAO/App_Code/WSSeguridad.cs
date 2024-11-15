using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Collections.Generic;


/// <summary>
/// Descripción breve de WSSeguridad
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class WSSeguridad : System.Web.Services.WebService
{

    public WSSeguridad()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    #region Usuario
    [WebMethod]
    public bool registrarUsuario(Usuario objUsuario)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.insertar(objUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }

    [WebMethod]
    public bool actualizarUsuario(Usuario objUsuario)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.actualizar(objUsuario);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.actualizarContraseña(sCodUsuario, sContraseña, SLogUsuarioMod, DtFchVigencia);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.actualizarEstado(sCodUsuario, sEstado, SLogUsuarioMod, sFlagCambPw);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool eliminarUsuario(string sCod_Usuario, string Log_Usuario_Mod)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.eliminar(sCod_Usuario, Log_Usuario_Mod);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public List<Usuario> listarUsuario()
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.listar();

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }


    [WebMethod]
    public Usuario obtenerUsuario(string sCodUsuario)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.obtener(sCodUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuario.obtenerxCuenta(sCuentaUsuario);
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
    public DataTable obtenerFecha()
    {
        try
        {
            DAO_Usuario objDAOUsuario = new DAO_Usuario(HttpContext.Current.Server.MapPath("."));
            return objDAOUsuario.obtenerFecha();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }


    #endregion

    #region UsuarioRol
    [WebMethod]
    public bool registrarUsuarioRol(UsuarioRol objUsuarioRol)
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuarioRol.insertar(objUsuarioRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool eliminarUsuarioRol(string sCod_Usuario, string sCod_Rol)
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuarioRol.eliminar(sCod_Usuario, sCod_Rol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public UsuarioRol obtenerUsuarioRol(string sCod_Usuario, string sCod_Rol)
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuarioRol.obtener(sCod_Usuario, sCod_Rol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public UsuarioRol obtenerUsuarioRolxCodRol(string sCod_Rol)
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuarioRol.obtenerxcodrol(sCod_Rol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
    {
        try
        {
            DAO_UsuarioRol objDAOUsuarioRol = new DAO_UsuarioRol(HttpContext.Current.Server.MapPath("."));

            return objDAOUsuarioRol.ListarUsuarioRolxCodUsuario(sCodUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }
    #endregion

    #region ArbolModulo

    [WebMethod]
    public ArbolModulo ObtenerArbolModulo(string sCodModulo, string sCodProceso, string sCodRol)
    {
        try
        {
            DAO_ArbolModulo objDAOArbolModulo = new DAO_ArbolModulo(HttpContext.Current.Server.MapPath("."));

            return objDAOArbolModulo.obtener(sCodModulo, sCodProceso, sCodRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable listarArbolModulo(string sCodigo)
    {
        try
        {
            DAO_ArbolModulo objDAOArbolModulo = new DAO_ArbolModulo(HttpContext.Current.Server.MapPath("."));

            return objDAOArbolModulo.listar(sCodigo);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public DataTable listarArbolModuloxRol(string sCodRol)
    {
        try
        {
            DAO_ArbolModulo objDAOArbolModulo = new DAO_ArbolModulo(HttpContext.Current.Server.MapPath("."));

            return objDAOArbolModulo.listarArbolModuloxRol(sCodRol);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public List<ArbolModulo> ListarPerfilVenta(string strUsuario)
    {
        try
        {
            DAO_ArbolModulo objDAOArbol = new DAO_ArbolModulo(HttpContext.Current.Server.MapPath("."));
            return objDAOArbol.ListarPerfilVenta(strUsuario);
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
    public bool registrarRol(Rol objRol)
    {
        try
        {
            DAO_Rol objDAORol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objDAORol.insertar(objRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public bool actualizarRol(Rol objRol)
    {
        try
        {
            DAO_Rol objDAORol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objDAORol.actualizar(objRol);

        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }

    }

    [WebMethod]
    public bool eliminarRol(string sCod_Rol)
    {
        try
        {
            DAO_Rol objDAORol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objDAORol.eliminar(sCod_Rol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

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


    [WebMethod]
    public List<Rol> listaDeRoles()
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.listar();
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public List<Rol> listarRolesAsignados(string sCodUsuario)
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.listarAsignados(sCodUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public List<Rol> listarRolesSinAsignar(string sCodUsuario)
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.listarSinAsignar(sCodUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public Rol obtenerRolxNombre(string sNomRol)
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.obtenerRolxNombre(sNomRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public Rol obtenerRolxCodigo(string sCodRol)
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.obtener(sCodRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public bool obtenerRolHijo(string sCodRol)
    {
        try
        {
            DAO_Rol objRol = new DAO_Rol(HttpContext.Current.Server.MapPath("."));
            return objRol.obtenerHijo(sCodRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }



    #endregion

    #region PerfilRol

    [WebMethod]
    public bool registrarPerfilRol(PerfilRol objPerfilRol)
    {
        try
        {
            DAO_PerfilRol objDAOPerfilRol = new DAO_PerfilRol(HttpContext.Current.Server.MapPath("."));
            return objDAOPerfilRol.insertar(objPerfilRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }



    }

    [WebMethod]
    public bool actualizarPerfilRol(PerfilRol objPerfilRol)
    {
        try
        {
            DAO_PerfilRol objDAOPerfilRol = new DAO_PerfilRol(HttpContext.Current.Server.MapPath("."));
            return objDAOPerfilRol.actualizar(objPerfilRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }


    [WebMethod]
    public DataTable ListarPerfilRolxRol(string sCodRol)
    {
        try
        {
            DAO_PerfilRol objDAOPerfilRol = new DAO_PerfilRol(HttpContext.Current.Server.MapPath("."));
            return objDAOPerfilRol.listarPerfilRolxRol(sCodRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }


    }

    [WebMethod]
    public List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
    {
        try
        {
            DAO_PerfilRol objDAOPerfilRol = new DAO_PerfilRol(HttpContext.Current.Server.MapPath("."));
            return objDAOPerfilRol.ListarxRol(sCodRol);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
    {
        try
        {
            DAO_PerfilRol objDAOPerfilRol = new DAO_PerfilRol(HttpContext.Current.Server.MapPath("."));
            return objDAOPerfilRol.FlagPerfilRolxOpcion(sCodUsuario, sCodRol, sDscArchivo, sOpcion);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region UsuarioClaveHistorico

    [WebMethod]
    public bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
    {
        try
        {
            DAO_ClaveUsuHist objDAOClaveUsuHist = new DAO_ClaveUsuHist(HttpContext.Current.Server.MapPath("."));
            return objDAOClaveUsuHist.obtener(sCodUsuario, SDscClave, iNum);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    [WebMethod]
    public ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
    {
        try
        {
            DAO_ClaveUsuHist objDAOClaveUsuHist = new DAO_ClaveUsuHist(HttpContext.Current.Server.MapPath("."));
            return objDAOClaveUsuHist.obtenerUsuarioHist(sCodUsuario);
        }
        catch (Exception ex)
        {
            ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
            ErrorHandler.Flg_Error = true;
            throw;
        }
    }

    #endregion

    #region Modulo

    [WebMethod]
    public DataTable ListarAllModulo()
    {
        try
        {
            DAO_Modulo objDAOModulo = new DAO_Modulo(HttpContext.Current.Server.MapPath("."));
            return objDAOModulo.ListarAllModulo();
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

