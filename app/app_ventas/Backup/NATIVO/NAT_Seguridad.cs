using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;


namespace LAP.TUUA.NATIVO
{
    public class NAT_Seguridad : NAT_Conexion
    {
        protected string Dsc_PathSPConfig;
        protected DAO_Usuario objDAOUsuario;
        protected DAO_ArbolModulo objDAOArbolModulo;
        protected DAO_UsuarioRol objDAOUsuarioRol;
        protected DAO_Rol objDAORol;
        protected DAO_PerfilRol objDAOPerfilRol;
        protected DAO_ClaveUsuHist objDAOClaveUsuHist;


        public NAT_Seguridad()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOUsuario = new DAO_Usuario(Dsc_PathSPConfig);
            objDAOArbolModulo = new DAO_ArbolModulo(Dsc_PathSPConfig);
            objDAOUsuarioRol = new DAO_UsuarioRol(Dsc_PathSPConfig);
            objDAORol = new DAO_Rol(Dsc_PathSPConfig);
            objDAOPerfilRol = new DAO_PerfilRol(Dsc_PathSPConfig);
            objDAOClaveUsuHist = new DAO_ClaveUsuHist(Dsc_PathSPConfig);
        }


        #region Usuario

        public override Usuario autenticar(string sCuenta, string sClave)
        {
            try
            {
                return objDAOUsuario.autenticar(sCuenta, sClave);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override bool CambiarClave(Usuario objUsuario)
        {
            try
            {
                objDAOUsuario.Cod_Modulo = Cod_Modulo;
                objDAOUsuario.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOUsuario.Cod_Usuario = Cod_Usuario;
                return objDAOUsuario.actualizar(objUsuario);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override bool registrarUsuario(Usuario objUsuario)
        {
            try
            {
                return objDAOUsuario.insertar(objUsuario);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override bool actualizarUsuario(Usuario objUsuario)
        {
            try
            {

                return objDAOUsuario.actualizar(objUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod,DateTime dtFchVigencia)
        {
            try
            {
                objDAOUsuario.Cod_Modulo = Cod_Modulo;
                objDAOUsuario.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOUsuario.Cod_Usuario = Cod_Usuario;
                return objDAOUsuario.actualizarContraseña(sCodUsuario, sContraseña, SLogUsuarioMod, dtFchVigencia);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod,string strFlgCambioClave)
        {
            try
            {
                objDAOUsuario.Cod_Modulo = Cod_Modulo;
                objDAOUsuario.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOUsuario.Cod_Usuario = Cod_Usuario;
                return objDAOUsuario.actualizarEstado(sCodUsuario, sEstado, SLogUsuarioMod, strFlgCambioClave);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            try
            {

                return objDAOUsuario.eliminar(sCodUsuario, LogUsuarioMod);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override Usuario obtenerUsuario(string sCodUsuario)
        {
            try
            {
                return objDAOUsuario.obtener(sCodUsuario);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            try
            {
                return objDAOUsuario.obtenerxCuenta(sCuentaUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override DataTable ListarAllUsuario()
        {

            DataTable DTListarAllUsuario;
            try
            {
                DTListarAllUsuario = objDAOUsuario.ListarAllUsuario();


            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



            return DTListarAllUsuario;

        }
        #endregion

        #region Arbol Modulo


        public override DataTable LLenarMenu(string sCodUsuario)
        {
            DataTable DTArbolModulo;
            try
            {
                DTArbolModulo = objDAOArbolModulo.listar(sCodUsuario);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


            return DTArbolModulo;

        }

        public override DataTable LLenarArbolRoles(string sCodRol)
        {
            DataTable DTArbolModulo;
            try
            {
                DTArbolModulo = objDAOArbolModulo.listarArbolModuloxRol(sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


            return DTArbolModulo;

        }

        public override ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            try
            {
                return objDAOArbolModulo.obtener(sCodModulo, sCodProceso, sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            try
            {
                return objDAOArbolModulo.ListarPerfilVenta(strUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region Usuario Rol



        public override bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {
            try
            {
                return objDAOUsuarioRol.insertar(objRolUsuarioRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            try
            {
                return objDAOUsuarioRol.eliminar(sCodUsuario, sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            try
            {
                return objDAOUsuarioRol.obtenerxcodrol(sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            try
            {
                return objDAOUsuarioRol.ListarUsuarioRolxCodUsuario(sCodUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }


        public override UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            try
            {
                return objDAOUsuarioRol.obtener(sCodUsuario, sCodRol);

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


        public override bool registrarRol(Rol objRol)
        {
            try
            {
                return objDAORol.insertar(objRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override bool actualizarRol(Rol objRol)
        {
            try
            {
                return objDAORol.actualizar(objRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override bool eliminarRol(string sCodRol)
        {
            try
            {
                return objDAORol.eliminar(sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override Rol obtenerRolxnombre(string sNomRol)
        {
            try
            {
                return objDAORol.obtenerRolxNombre(sNomRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override Rol obtenerRolxcodigo(string sCodRol)
        {
            try
            {
                return objDAORol.obtener(sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<Rol> listaDeRoles()
        {
            try
            {
                return objDAORol.listar();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override List<Rol> listarRolesAsignados(string sCodUsuario)
        {
            try
            {
                return objDAORol.listarAsignados(sCodUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            try
            {
                return objDAORol.listarSinAsignar(sCodUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion

        #region Perfil Rol



        public override bool registrarPerfilRol(PerfilRol objPerfilRol)
        {
            try
            {
                return objDAOPerfilRol.insertar(objPerfilRol);


            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }

        public override bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {
            try
            {
                return objDAOPerfilRol.actualizar(objPerfilRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }

        public override DataTable ListarPerfilRolxRol(string sCodRol)
        {
            DataTable objWSPerfilRol;
            try
            {
                objWSPerfilRol = objDAOPerfilRol.listarPerfilRolxRol(sCodRol);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



            return objWSPerfilRol;

        }


        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            try
            {
                return objDAOPerfilRol.ListarxRol(sCodRol);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario,string sCodRol, string sDscArchivo, string sOpcion)
        {
            try
            {
                return objDAOPerfilRol.FlagPerfilRolxOpcion(sCodUsuario, sCodRol,sDscArchivo, sOpcion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Clave Usuario Historico

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            try
            {
                return objDAOClaveUsuHist.obtener(sCodUsuario, SDscClave, iNum);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion




        public override DataTable ListarAllParametroGenerales(string strParam)
        {
            try
            {
                DAO_ParameGeneral objDAOParameGeneral = new DAO_ParameGeneral(Dsc_PathSPConfig);
                return objDAOParameGeneral.ListarAllParametroGeneral(strParam);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo,string strCodUsuario, string strTipOperacion)
        {
            try
            {
                DAO_Auditoria objDAOAuditoria = new DAO_Auditoria(Dsc_PathSPConfig);
                return objDAOAuditoria.insertar(strCodModulo, strCodSubModulo, strCodUsuario, strTipOperacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable obtenerFecha()
        {
            try
            {
                return objDAOUsuario.obtenerFecha();


            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            try
            {
                return objDAOClaveUsuHist.obtenerUsuarioHist(sCodUsuario);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<Moneda> ListarMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool verificarTurnoCerradoxPtoVenta(string strEquipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo, ref string strTurnoError)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Turno ObtenerTurnoIniciado(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurno(Turno objTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOperacion(LogOperacion objOperacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOpeCaja(List<LogOperacCaja> objLista)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasInter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool IsError()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetErrorCode()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoTicket(TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable CantidadMonedasTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllCompania()
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public override DataTable ListarParametros(string as_identificador)
        {
            try
            {
                DAO_ParameGeneral objDAOParameGeneral = new DAO_ParameGeneral(Dsc_PathSPConfig);
                return objDAOParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ConsultaDetalleTicket(string as_numeroticket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaCamposxNombre(string as_nombre)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable listarAllPuntoVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TipoTicket> ListarAllTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool insertarModalidadVenta(ModalidadVenta objModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarModalidadVenta(ModalidadVenta objModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ModalidadVenta> listarModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int intTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }
    }
}
