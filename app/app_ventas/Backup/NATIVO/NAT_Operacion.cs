using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Data;

namespace LAP.TUUA.NATIVO
{
    public class NAT_Operacion : NAT_Conexion
    {
        protected string Dsc_PathSPConfig;

        public NAT_Operacion()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
        }

        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            try
            {
                DAO_Limite objDAOLimite = new DAO_Limite(Dsc_PathSPConfig);
                return objDAOLimite.ListarPorOperacion(stipoope);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarOperacion(LogOperacion objOperacion)
        {
            try
            {
                DAO_LogOperacion objDAOOpera = new DAO_LogOperacion(Dsc_PathSPConfig);
                objDAOOpera.Cod_Modulo=Cod_Modulo;
                objDAOOpera.Cod_Sub_Modulo=Cod_Sub_Modulo;
                objDAOOpera.Cod_Usuario = Cod_Usuario;
                return objDAOOpera.insertar(objOperacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarOpeCaja(List<LogOperacCaja> objLista)
        {
            try
            {
                DAO_LogOperacCaja objDAOOperaCaja = new DAO_LogOperacCaja(Dsc_PathSPConfig);
                objDAOOperaCaja.Cod_Modulo = Cod_Modulo;
                objDAOOperaCaja.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOOperaCaja.Cod_Usuario = Cod_Usuario;
                for (int i = 0; i < objLista.Count; i++)
                {
                    if (!objDAOOperaCaja.insertar(objLista[i]))
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

        public override TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            try
            {
                DAO_TasaCambio objDAOTasaCambio = new DAO_TasaCambio(Dsc_PathSPConfig);
                return objDAOTasaCambio.ObtenerUltimoPorMoneda(strMoneda, strTipo);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
        {
            try
            {
                DAO_LogCompraVenta objDAOCompraVenta = new DAO_LogCompraVenta(Dsc_PathSPConfig);
                objDAOCompraVenta.Cod_Modulo = Cod_Modulo;
                objDAOCompraVenta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOCompraVenta.Cod_Usuario = Cod_Usuario;
                return objDAOCompraVenta.insertar(objCompraVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            try
            {
                DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(Dsc_PathSPConfig);
                return objDAOTipoTicket.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            try
            {
                DAO_Compania objDAOCompania = new DAO_Compania(Dsc_PathSPConfig);
                return objDAOCompania.consultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
        {
            try
            {
                DAO_VueloProgramado objDAOVueloProg = new DAO_VueloProgramado(Dsc_PathSPConfig);
                return objDAOVueloProg.ListarVuelosxCompania(strCompania, strFecha, strTipVuelo);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                for (int i = 0; i < listaTickets.Count; i++)
                {
                    if (!objDAOTicket.insertar(listaTickets[i]))
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

        public override List<Moneda> ListarMonedasInter()
        {
            try
            {
                DAO_Moneda objDAOMoneda = new DAO_Moneda(Dsc_PathSPConfig);
                return objDAOMoneda.ListarMonedasInter();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            try
            {
                DAO_Representante objRepteCia = new DAO_Representante(Dsc_PathSPConfig);
                return objRepteCia.ListarReptexCia(strCia);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            try
            {
                DAO_VentaMasiva objDAOVentaMasiva = new DAO_VentaMasiva(Dsc_PathSPConfig);
                objDAOVentaMasiva.Cod_Modulo = Cod_Modulo;
                objDAOVentaMasiva.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOVentaMasiva.Cod_Usuario = Cod_Usuario;
                return objDAOVentaMasiva.insertar(objVentaMasiva);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            try
            {
                DAO_VentaMasiva objDAOVentaMasiva = new DAO_VentaMasiva(Dsc_PathSPConfig);
                objDAOVentaMasiva.Cod_Modulo = Cod_Modulo;
                objDAOVentaMasiva.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOVentaMasiva.Cod_Usuario = Cod_Usuario;
                return objDAOVentaMasiva.actualizar(objVentaMasiva);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<TipoTicket> ListarAllTipoTicket()
        {
            try
            {
                DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(Dsc_PathSPConfig);
                return objDAOTipoTicket.listar();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            try
            {
                DAO_ModVentaComp objDAOModVentaComp = new DAO_ModVentaComp(Dsc_PathSPConfig);
                return objDAOModVentaComp.ListarCompaniaxModVenta(strCodModVenta, strTipComp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            try
            {
                DAO_ModalidadAtrib objDAOModAtrib = new DAO_ModalidadAtrib(Dsc_PathSPConfig);
                return objDAOModAtrib.ListarAtributosxModVenta(strCodModVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            try
            {
                DAO_ModalidadAtrib objDAOModAtrib = new DAO_ModalidadAtrib(Dsc_PathSPConfig);
                return objDAOModAtrib.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
            }
            catch(Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTicket.insertar( strCompania,  strVentaMasiva,  strNumVuelo,  strFecVuelo,  strTurno,  strUsuario,  decPrecio,  strMoneda,  strModVenta,  strTipTicket,  strTipVuelo,  intTickets, strFlagCont, strNumRef, strTipPago,strEmpresa,strRepte,ref  strFecVence,ref strListaTickets,null,null,null);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTicket.AnularTuua(strListaTicket, intTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override Usuario autenticar(string sCuenta, string sClave)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CambiarClave(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
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


        public override bool IsError()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string GetErrorCode()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarUsuario(Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllUsuario()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable LLenarMenu(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable LLenarArbolRoles(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRolUsuario(UsuarioRol objRolUsuarioRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarRol(Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxnombre(string sNomRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Rol obtenerRolxcodigo(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPerfilRol(PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarPerfilRolxRol(string sCodRol)
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
            throw new Exception("The method or operation is not implemented.");
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

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime dtFchVigencia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listaDeRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesAsignados(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllParametroGenerales(string strParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool insertarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta)
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

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac)
        {
            throw new NotImplementedException();
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerFecha()
        {
            throw new NotImplementedException();
        }

        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }
    }
}
