/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Proceso de gestión de Seguridad.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using System.Data;
namespace LAP.TUUA.CONTROL
{
    public class BO_Seguridad
    {

        private Conexion objCnxSeguridad;
        private BO_Consultas objBOConsultas;
        private Cryptografia objCrypto;

        public BO_Seguridad()
        {
            try
            {
                objCnxSeguridad = Resolver.ObtenerConexion(Define.CNX_01);
            }
            catch (Exception ex)
            {
                throw;
            }
            objCrypto = new Cryptografia();
            objBOConsultas = new BO_Consultas();
        }

        public BO_Seguridad(string strUsuario, string strModulo, string strSubModulo)
        {
            try
            {
                objCnxSeguridad = Resolver.ObtenerConexion(Define.CNX_01);
                objCnxSeguridad.Cod_Usuario = strUsuario;
                objCnxSeguridad.Cod_Modulo = strModulo;
                objCnxSeguridad.Cod_Sub_Modulo = strSubModulo;
            }
            catch (Exception ex)
            {
                throw;
            }
            objCrypto = new Cryptografia();
            objBOConsultas = new BO_Consultas();
        }

        #region Arbol Modulo


        public DataTable LLenarMenu(string sCodUsuario)
        {

            DataTable DTArbolModulo;


            DTArbolModulo = objCnxSeguridad.LLenarMenu(sCodUsuario);
            return DTArbolModulo;

        }

        public DataTable LLenarArbolRoles(string sCodRol)
        {

            DataTable DTArbolModulo;

            DTArbolModulo = objCnxSeguridad.LLenarArbolRoles(sCodRol);
            return DTArbolModulo;

        }

        public ArbolModulo ObtenerArbolModulo(string sCodModulo, string sCodProceso, string sCodRol)
        {

            return objCnxSeguridad.ObtenerArbolModulo(sCodModulo, sCodProceso, sCodRol);

        }

        #endregion

        #region Usuario

        public Usuario autenticar(string sCuenta, string sClave)
        {
            Usuario objUsuario = new Usuario();
            string sKey;
            string sPwEncriptado;
            objUsuario = objCnxSeguridad.obtenerUsuarioxCuenta(sCuenta);
            DataTable dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KI");
            if (dtConsulta.Rows.Count > 0)
            {
                sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());

                sPwEncriptado = objCrypto.Encriptar(sClave, sKey);

                if (objUsuario.SPwdActualUsuario == sPwEncriptado)
                {
                    return objUsuario;
                }
            }
            return null;

        }

        public bool registrarUsuario(Usuario objUsuario)
        {
            string sPwEncriptado;
            DataTable dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KI");
            string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());

            sPwEncriptado = objCrypto.Encriptar(objUsuario.SPwdActualUsuario, sKey);
            objUsuario.SPwdActualUsuario = sPwEncriptado;
            return objCnxSeguridad.registrarUsuario(objUsuario);

        }

        public bool actualizarUsuario(Usuario objUsuario)
        {

            return objCnxSeguridad.actualizarUsuario(objUsuario);
        }

        public bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            string sPwEncriptado;
            DataTable dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KI");
            string sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            sPwEncriptado = objCrypto.Encriptar(sContraseña, sKey);
            return objCnxSeguridad.actualizarContraseñaUsuario(sCodUsuario, sPwEncriptado, SLogUsuarioMod, DtFchVigencia);
        }


        public bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
        {

            return objCnxSeguridad.actualizarEstadoUsuario(sCodUsuario, sEstado, SLogUsuarioMod, sFlagCambPw);
        }

        public bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {

            return objCnxSeguridad.eliminarUsuario(sCodUsuario, LogUsuarioMod);

        }

        public Usuario obtenerUsuario(string sCodUsuario)
        {
            return objCnxSeguridad.obtenerUsuario(sCodUsuario);
        }

        public Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {

            return objCnxSeguridad.obtenerUsuarioxCuenta(sCuentaUsuario);
        }

        public DataTable obtenerFecha()
        {
            return objCnxSeguridad.obtenerFecha();
        }
        #endregion

        #region Sincronizacion
        public DataTable ListarSincronizacion()
        {

            DataTable DTListarSincronizacion;

            DTListarSincronizacion = objCnxSeguridad.ListarSincronizacion();

            return DTListarSincronizacion;

        }



        #endregion


        #region Rol

        public DataTable ListaRol()
        {

            DataTable DTListaRol;

            DTListaRol = objCnxSeguridad.ListaRol();

            return DTListaRol;

        }

        public bool registrarRol(Rol objRol)
        {

            return objCnxSeguridad.registrarRol(objRol);

        }

        public bool actualizarRol(Rol objRol)
        {
            return objCnxSeguridad.actualizarRol(objRol);

        }

        public bool eliminarRol(string sCodRol)
        {

            return objCnxSeguridad.eliminarRol(sCodRol);

        }

        public Rol obtenerRolxnombre(string sNomRol)
        {
            return objCnxSeguridad.obtenerRolxnombre(sNomRol);
        }

        public Rol obtenerRolxcodigo(string sCodRol)
        {

            return objCnxSeguridad.obtenerRolxcodigo(sCodRol);
        }

        public List<Rol> listaDeRoles()
        {
            return objCnxSeguridad.listaDeRoles();
        }

        public List<Rol> listarRolesAsignados(string sCodUsuario)
        {
            return objCnxSeguridad.listarRolesAsignados(sCodUsuario);
        }

        public List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            return objCnxSeguridad.listarRolesSinAsignar(sCodUsuario);
        }

        public bool obtenerRolHijo(string sCodRol)
        {
            return objCnxSeguridad.obtenerRolHijo(sCodRol);
        }

        #endregion

        #region Usuario Rol

        public bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {
            return objCnxSeguridad.registrarRolUsuario(objRolUsuarioRol);
        }

        public bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            return objCnxSeguridad.eliminarRolUsuario(sCodUsuario, sCodRol);
        }

        public UsuarioRol obtenerRolUsuarioxCodRol(string sCodRol)
        {
            return objCnxSeguridad.obtenerUsuarioRolxCodRol(sCodRol);
        }


        public UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {

            return objCnxSeguridad.obtenerRolUsuario(sCodUsuario, sCodRol);
        }
        #endregion

        #region Perfil Rol

        public bool registrarPerfilRol(PerfilRol objPerfilRol)
        {

            return objCnxSeguridad.registrarPerfilRol(objPerfilRol);

        }

        public bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {

            return objCnxSeguridad.actualizarPerfilRol(objPerfilRol);

        }

        public DataTable ListarPerfilRolxRol(string sCodRol)
        {

            DataTable objWSPerfilRol;
            objWSPerfilRol = objCnxSeguridad.ListarPerfilRolxRol(sCodRol);

            return objWSPerfilRol;

        }

        public List<PerfilRol> AccesosPerfilUsuario(string sCodUsuario)
        {
            Hashtable htListadeRol = new Hashtable();
            Hashtable htListadePerfilRol = new Hashtable();

            List<PerfilRol> objListPerfilRol = new List<PerfilRol>();
            PerfilRol objPerfilRol = new PerfilRol();
            ArrayList alPerfilRol = new ArrayList();
            string rol_actual;
            string rol_padre;

            List<UsuarioRol> listaUsuarioRol = objCnxSeguridad.ListarUsuarioRolxCodUsuario(sCodUsuario);

            int iTotalRoles = listaUsuarioRol.Count;

            for (int i = 0; i < iTotalRoles; i++)
            {
                htListadeRol.Add(listaUsuarioRol[i].SCodRol, listaUsuarioRol[i]);
            }

            for (int i = 0; i < iTotalRoles; i++)
            {
                rol_actual = objCnxSeguridad.ListarUsuarioRolxCodUsuario(sCodUsuario)[i].SCodRol;
                rol_padre = "";

                while (rol_actual != null)
                {
                    if (htListadeRol[rol_actual] != null)
                    {
                        List<PerfilRol> listaPerfilRol = objCnxSeguridad.ListadoPerfilRolxRol(rol_actual);
                        int iTotalPerfilRol = listaPerfilRol.Count;

                        for (int j = 0; j < iTotalPerfilRol; j++)
                        {
                            objPerfilRol = new PerfilRol(listaPerfilRol[j].SCodProceso, listaPerfilRol[j].SCodModulo,
                            listaPerfilRol[j].SCodRol, listaPerfilRol[j].SFlgPermitido, listaPerfilRol[j].SLogUsuarioMod,
                            listaPerfilRol[j].SLogFechaMod, listaPerfilRol[j].SLogHoraMod);


                            string CodPerfilRol = objPerfilRol.SCodModulo + objPerfilRol.SCodProceso;

                            if (htListadePerfilRol[CodPerfilRol] == null)
                            {
                                htListadePerfilRol.Add(CodPerfilRol, objPerfilRol);
                            }
                            else
                            {
                                htListadePerfilRol.Remove(CodPerfilRol);
                                htListadePerfilRol.Add(CodPerfilRol, objPerfilRol);
                            }
                        }
                    }

                    rol_padre = objCnxSeguridad.obtenerRolxcodigo(rol_actual).SCodPadreRol;

                    rol_actual = rol_padre;
                }
            }

            object[] keys = new object[htListadePerfilRol.Keys.Count];
            htListadePerfilRol.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                objListPerfilRol.Add((PerfilRol)htListadePerfilRol[keys[i]]);
            }
            return objListPerfilRol;

        }


        public DataTable PermisoRolesxRol(string sCodRol)
        {
            DataTable dtMenu = new DataTable();
            DataRow drMenu;
            DataColumn dcMenu = new DataColumn();

            dtMenu.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("sequence", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("flag_permitido", System.Type.GetType("System.String")));

            string sCodModulo = "";
            string sCodProceso = "";
            ArbolModulo objArbolModulo = new ArbolModulo();
            ArbolModulo objSubArbolModulo = new ArbolModulo();
            List<ArbolModulo> objListArbolModulo = new List<ArbolModulo>();
            List<PerfilRol> objLsPerfilRol = objCnxSeguridad.ListadoPerfilRolxRol(sCodRol);
            int iTotalPerfilRol = objLsPerfilRol.Count;

            objListArbolModulo = ListaOpcionesRol(objLsPerfilRol);

            for (int i = 0; i < objListArbolModulo.Count; i++)
            {
                objArbolModulo = objListArbolModulo[i];

                if (objArbolModulo.SCodModulo != sCodModulo && objArbolModulo.ITipNivel == 0)
                {
                    bool banderaReg = true;
                    if (objArbolModulo.SFlgPermitido == "0")
                    {
                        for (int f = 0; f < objListArbolModulo.Count; f++)
                        {
                            objSubArbolModulo = objListArbolModulo[f];
                            if (objSubArbolModulo.SCodModulo == objArbolModulo.SCodModulo && objSubArbolModulo.ITipNivel == 0 && objSubArbolModulo.SFlgPermitido == "1")
                            {
                               banderaReg = false;
                            }
                        }
                    }

                    if (banderaReg)
                    {
                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo;
                        drMenu["title"] = objArbolModulo.SDscModulo;
                        drMenu["sequence"] = objArbolModulo.INumPosModulo;
                        drMenu["flag_permitido"] = objArbolModulo.SFlgPermitido;
                        dtMenu.Rows.Add(drMenu);
                        sCodModulo = objArbolModulo.SCodModulo;
                    }

                    drMenu = dtMenu.NewRow();
                    drMenu["id"] = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProceso;
                    drMenu["parent_id"] = objArbolModulo.SCodModulo;
                    drMenu["title"] = objArbolModulo.SDscProceso;
                    drMenu["sequence"] = objArbolModulo.INumPosProceso;
                    drMenu["flag_permitido"] = objArbolModulo.SFlgPermitido;
                    dtMenu.Rows.Add(drMenu);
                    if (objArbolModulo.SCodProceso != null)
                        sCodProceso = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProceso;
                    else
                        sCodProceso = objArbolModulo.SCodModulo;
                }
                else
                {
                    if ((objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProcesoPadre) != sCodProceso)
                    {
                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProceso;
                        if (objArbolModulo.SCodProcesoPadre != null)
                            drMenu["parent_id"] = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProcesoPadre;
                        else
                            drMenu["parent_id"] = objArbolModulo.SCodModulo;

                        drMenu["title"] = objArbolModulo.SDscProceso;
                        drMenu["sequence"] = objArbolModulo.INumPosProceso;
                        drMenu["flag_permitido"] = objArbolModulo.SFlgPermitido;
                        dtMenu.Rows.Add(drMenu);
                        if (objArbolModulo.SCodProcesoPadre != null)
                            sCodProceso = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProcesoPadre;
                        else
                            sCodProceso = objArbolModulo.SCodModulo;
                    }
                    else
                    {
                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo + "|" + objArbolModulo.SCodProceso;
                        drMenu["parent_id"] = sCodProceso;
                        drMenu["title"] = objArbolModulo.SDscProceso;
                        drMenu["sequence"] = objArbolModulo.INumPosProceso;
                        drMenu["flag_permitido"] = objArbolModulo.SFlgPermitido;
                        dtMenu.Rows.Add(drMenu);
                    }
                }

            }

            return SortDataTable(dtMenu, "id ASC, sequence ASC");
        }


        public bool FlagPerfilUsuarioOpcion(DataTable dtMapSite, string sDscArchivo, string sOpcion)
        {
            foreach (DataRow drMapSite in dtMapSite.Rows)
            {
                if ((Convert.ToString(drMapSite["url"]) == sDscArchivo && (Convert.ToString(drMapSite["title"]) == sOpcion)))
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable LlenarDatosMenu(string SCodUsuario)
        {
            List<ArbolModulo> objListArbolModulo = new List<ArbolModulo>();
            List<PerfilRol> objLsPerfilRol = new List<PerfilRol>();
            objLsPerfilRol = AccesosPerfilUsuario(SCodUsuario);
            ArbolModulo objArbolModulo = new ArbolModulo();
            Hashtable hsCodPadre = new Hashtable();

            DataTable dtMenu = new DataTable();
            DataRow drMenu;
            DataColumn dcMenu = new DataColumn();

            dtMenu.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("image", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("modulo", System.Type.GetType("System.Int32")));
            dtMenu.Columns.Add(new DataColumn("sequence", System.Type.GetType("System.Int32")));
            dtMenu.Columns.Add(new DataColumn("style", System.Type.GetType("System.String")));
            

            objListArbolModulo = ListaOpcionesMenu(objLsPerfilRol);

            string sCodModulo = "";
            string sCodProceso = "";
            for (int i = 0; i < objListArbolModulo.Count; i++)
            {
                objArbolModulo = objListArbolModulo[i];

                if (objArbolModulo.SFlgPermitido == "1" && (objArbolModulo.ITipNivel == 0 || objArbolModulo.ITipNivel == 2))
                {
                    if (objArbolModulo.SCodModulo != sCodModulo)
                    {
                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo;
                        drMenu["title"] = objArbolModulo.SDscModulo;
                        drMenu["modulo"] = objArbolModulo.INumPosModulo;
                        drMenu["sequence"] = 0;
                        dtMenu.Rows.Add(drMenu);
                        sCodModulo = objArbolModulo.SCodModulo;

                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProceso;
                        drMenu["parent_id"] = sCodModulo + objArbolModulo.SCodProcesoPadre;
                        drMenu["title"] = objArbolModulo.SDscProceso;
                        drMenu["url"] = objArbolModulo.SDscArchivo;
                        drMenu["image"] = objArbolModulo.SDscIcono;
                        drMenu["modulo"] = objArbolModulo.INumPosModulo;
                        drMenu["sequence"] = objArbolModulo.INumPosProceso;
                        dtMenu.Rows.Add(drMenu);
                        sCodProceso = objArbolModulo.SCodModulo + objArbolModulo.SCodProceso;

                    }
                    else
                    {
                        if ((objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre) != sCodProceso)
                        {
                            drMenu = dtMenu.NewRow();
                            drMenu["id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProceso;
                            drMenu["parent_id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre;
                            drMenu["title"] = objArbolModulo.SDscProceso;
                            drMenu["url"] = objArbolModulo.SDscArchivo;
                            drMenu["image"] = objArbolModulo.SDscIcono;
                            drMenu["modulo"] = objArbolModulo.INumPosModulo;
                            drMenu["sequence"] = objArbolModulo.INumPosProceso;
                            dtMenu.Rows.Add(drMenu);
                            sCodProceso = objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre;
                        }
                        else
                        {
                            drMenu = dtMenu.NewRow();
                            drMenu["id"] = sCodModulo + objArbolModulo.SCodProceso;
                            drMenu["parent_id"] = sCodProceso;
                            drMenu["title"] = objArbolModulo.SDscProceso;
                            drMenu["url"] = objArbolModulo.SDscArchivo;
                            drMenu["image"] = objArbolModulo.SDscIcono;
                            drMenu["modulo"] = objArbolModulo.INumPosModulo;
                            drMenu["sequence"] = objArbolModulo.INumPosProceso;
                            dtMenu.Rows.Add(drMenu);
                        }
                    } 
                }
            }

            return SortDataTable(dtMenu, "modulo ASC, sequence ASC"); ;
        }

        public DataTable LlenarDatosMapSite(string SCodUsuario)
        {
            List<ArbolModulo> objListArbolModulo = new List<ArbolModulo>();
            List<PerfilRol> objLsPerfilRol = new List<PerfilRol>();
            objLsPerfilRol = AccesosPerfilUsuario(SCodUsuario);
            ArbolModulo objArbolModulo = new ArbolModulo();
            Hashtable hsCodPadre = new Hashtable();

            DataTable dtMenu = new DataTable();
            DataRow drMenu;
            DataColumn dcMenu = new DataColumn();

            dtMenu.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("url", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("image", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("sequence", System.Type.GetType("System.String")));
            dtMenu.Columns.Add(new DataColumn("modulo", System.Type.GetType("System.Int32")));
            dtMenu.Columns.Add(new DataColumn("style", System.Type.GetType("System.String")));

            objListArbolModulo = ListaOpcionesMenu(objLsPerfilRol);

            string sCodModulo = "";
            string sCodProceso = "";
            for (int i = 0; i < objListArbolModulo.Count; i++)
            {
                objArbolModulo = objListArbolModulo[i];

                if (objArbolModulo.SFlgPermitido == "1")
                {
                    if (objArbolModulo.SCodModulo != sCodModulo && objArbolModulo.ITipNivel == 0)
                    {
                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo;
                        drMenu["title"] = objArbolModulo.SDscModulo;
                        drMenu["modulo"] = objArbolModulo.INumPosModulo;
                        drMenu["sequence"] = 0;
                        dtMenu.Rows.Add(drMenu);
                        sCodModulo = objArbolModulo.SCodModulo;

                        drMenu = dtMenu.NewRow();
                        drMenu["id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProceso;
                        drMenu["parent_id"] = sCodModulo + objArbolModulo.SCodProcesoPadre; ;
                        drMenu["title"] = objArbolModulo.SDscProceso;
                        drMenu["url"] = objArbolModulo.SDscArchivo;
                        drMenu["image"] = objArbolModulo.SDscIcono;
                        drMenu["modulo"] = objArbolModulo.INumPosModulo;
                        if (objArbolModulo.SCodProcesoPadre != null)
                            sCodProceso = objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre;
                        else
                            sCodProceso = objArbolModulo.SCodModulo;
                        drMenu["sequence"] = sCodProceso + Convert.ToString(objArbolModulo.INumPosProceso);
                        dtMenu.Rows.Add(drMenu);

                    }
                    else
                    {
                        if ((objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre) != sCodProceso)
                        {
                            drMenu = dtMenu.NewRow();
                            drMenu["id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProceso;
                            drMenu["parent_id"] = objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre;
                            drMenu["title"] = objArbolModulo.SDscProceso;
                            drMenu["url"] = objArbolModulo.SDscArchivo;
                            drMenu["image"] = objArbolModulo.SDscIcono;
                            drMenu["modulo"] = objArbolModulo.INumPosModulo;
                            if (objArbolModulo.SCodProcesoPadre != null)
                                sCodProceso = objArbolModulo.SCodModulo + objArbolModulo.SCodProcesoPadre;
                            else
                                sCodProceso = objArbolModulo.SCodModulo;
                            drMenu["sequence"] = sCodProceso + Convert.ToString(objArbolModulo.INumPosProceso);
                            dtMenu.Rows.Add(drMenu);
                        }
                        else
                        {
                            drMenu = dtMenu.NewRow();
                            drMenu["id"] = sCodModulo + objArbolModulo.SCodProceso;
                            drMenu["parent_id"] = sCodProceso;
                            drMenu["title"] = objArbolModulo.SDscProceso;
                            drMenu["url"] = objArbolModulo.SDscArchivo;
                            drMenu["image"] = objArbolModulo.SDscIcono;
                            drMenu["modulo"] = objArbolModulo.INumPosModulo;
                            drMenu["sequence"] = sCodProceso + Convert.ToString(objArbolModulo.INumPosProceso);
                            dtMenu.Rows.Add(drMenu);
                        }
                    }
                }
            }

            DataTable th = new DataTable();

            th = SortDataTable(dtMenu, "id ASC,modulo ASC,parent_id ASC, sequence ASC");
            return th;

        }


        private DataTable SortDataTable(DataTable GetDataTable, string sort)
        {
            DataTable _NewDataTable = GetDataTable.Clone();
            int rowCount = GetDataTable.Rows.Count;

            DataRow[] foundRows = GetDataTable.Select(null, sort);
            // Sort with Column name 
            for (int i = 0; i < rowCount; i++)
            {
                object[] arr = new object[GetDataTable.Columns.Count];
                for (int j = 0; j < GetDataTable.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
                DataRow data_row = _NewDataTable.NewRow();
                data_row.ItemArray = arr;
                _NewDataTable.Rows.Add(data_row);
            }

            //Clear the incoming GetDataTable 
            GetDataTable.Rows.Clear();

            for (int i = 0; i < _NewDataTable.Rows.Count; i++)
            {
                object[] arr = new object[GetDataTable.Columns.Count];
                for (int j = 0; j < GetDataTable.Columns.Count; j++)
                {
                    arr[j] = _NewDataTable.Rows[i][j];
                }

                DataRow data_row = GetDataTable.NewRow();
                data_row.ItemArray = arr;
                GetDataTable.Rows.Add(data_row);
            }
            return _NewDataTable;
        }


        protected List<ArbolModulo> ListaOpcionesMenu(List<PerfilRol> objListPerfilRol)
        {
            List<ArbolModulo> objListArbolModulo = new List<ArbolModulo>();

            for (int i = 0; i < objListPerfilRol.Count; i++)
            {
                ArbolModulo objArbolModulo = new ArbolModulo();
                objArbolModulo = ObtenerArbolModulo(objListPerfilRol[i].SCodModulo, objListPerfilRol[i].SCodProceso, objListPerfilRol[i].SCodRol);
                if (objArbolModulo != null)
                {
                    if (objArbolModulo.STipModulo == "01")
                        objListArbolModulo.Add(objArbolModulo);
                }
            }

            if (objListArbolModulo.Count > 0)
            {
                SortListIndiceModulo(objListArbolModulo);
            }


            return objListArbolModulo;
        }

        protected List<ArbolModulo> ListaOpcionesRol(List<PerfilRol> objListPerfilRol)
        {
            List<ArbolModulo> objListArbolModulo = new List<ArbolModulo>();

            for (int i = 0; i < objListPerfilRol.Count; i++)
            {
                ArbolModulo objArbolModulo = new ArbolModulo();
                objArbolModulo = ObtenerArbolModulo(objListPerfilRol[i].SCodModulo, objListPerfilRol[i].SCodProceso, objListPerfilRol[i].SCodRol);
                if (objArbolModulo != null)
                {
                    objListArbolModulo.Add(objArbolModulo);
                }
            }

            if (objListArbolModulo.Count > 0)
            {
                SortListIndiceModulo(objListArbolModulo);
            }


            return objListArbolModulo;
        }




        protected List<ArbolModulo> SortListIndiceModulo(List<ArbolModulo> objListArbolModulo)
        {
            objListArbolModulo.Sort(
                delegate(ArbolModulo x, ArbolModulo c)
                {
                    return x.INumPosModulo.CompareTo(c.INumPosModulo);
                }
                );
            return objListArbolModulo;
        }
      

        #endregion

        #region Clave Usuario Histórico

        public bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave)
        {
            string sKey = "";
            int sNumKeyHist = 0;
            string sPwEncriptado;
            DataTable dtConsulta;
            dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KI");
            if (dtConsulta.Rows.Count > 0)
            {
                sKey = Convert.ToString(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            }

            dtConsulta = new DataTable();
            dtConsulta = objBOConsultas.ListarParametros("KH");
            if (dtConsulta.Rows.Count > 0)
            {
                sNumKeyHist = Convert.ToInt32(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            }

            if (sNumKeyHist > 0 && sKey != "")
            {
                sPwEncriptado = objCrypto.Encriptar(SDscClave, sKey);

                return objCnxSeguridad.obtenerClaveUsuHist(sCodUsuario, sPwEncriptado, sNumKeyHist);
            }
            return false;
        }

        public ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            return objCnxSeguridad.obtenerUsuarioHist(sCodUsuario);

        }

        #endregion

        #region Modulo

        public DataTable ListarAllModulo()
        {
            return objCnxSeguridad.ListarAllModulo();
        }
        #endregion

        #region  Auditoria
        public bool InsertarAuditoria(string strCodModulo, string strCodSubModulo,string strCodUsuario, string strTipOperacion)
        {
              return objCnxSeguridad.InsertarAuditoria(strCodModulo,strCodSubModulo,strCodUsuario,strTipOperacion);
        }
        #endregion
    }
}
