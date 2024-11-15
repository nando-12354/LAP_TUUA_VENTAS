using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Data;

namespace LAP.TUUA.CONEXION
{
    public abstract class Conexion
    {
        public string Cod_Usuario;
        public string Cod_Modulo;
        public string Cod_Sub_Modulo;

        #region Seguridad

        //Usuario
        public abstract Usuario autenticar(string sCuenta, string sClave);

        public abstract bool CambiarClave(Usuario objUsuario);

        public abstract bool registrarUsuario(Usuario objUsuario);

        public abstract bool actualizarUsuario(Usuario objUsuario);

        public abstract bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod);

        public abstract Usuario obtenerUsuario(string sCodUsuario);

        public abstract Usuario obtenerUsuarioxCuenta(string sCuentaUsuario);

        public abstract bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime dtFchVigencia);

        public abstract bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave);



        //Arbol Modulo

        public abstract DataTable LLenarMenu(string sCodUsuario);

        public abstract DataTable LLenarArbolRoles(string sCodRol);

        public abstract ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol);

        public abstract List<ArbolModulo> ListarPerfilVenta(string strUsuario);

        //Usuario Rol

        public abstract bool registrarRolUsuario(UsuarioRol objRolUsuarioRol);

        public abstract bool eliminarRolUsuario(string sCodUsuario, string sCodRol);

        public abstract UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol);

        public abstract List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario);

        public abstract UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol);

        //Rol

        public abstract DataTable ListaRol();

        public abstract bool registrarRol(Rol objRol);

        public abstract bool actualizarRol(Rol objRol);

        public abstract bool eliminarRol(string sCodRol);

        public abstract Rol obtenerRolxnombre(string sNomRol);

        public abstract Rol obtenerRolxcodigo(string sCodRol);

        public abstract List<Rol> listaDeRoles();

        public abstract List<Rol> listarRolesAsignados(string sCodUsuario);

        public abstract List<Rol> listarRolesSinAsignar(string sCodUsuario);

        //Perfil Rol

        public abstract bool registrarPerfilRol(PerfilRol objPerfilRol);

        public abstract bool actualizarPerfilRol(PerfilRol objPerfilRol);

        public abstract DataTable ListarPerfilRolxRol(string sCodRol);

        public abstract List<PerfilRol> ListadoPerfilRolxRol(string sCodRol);

        public abstract string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion);

        //Clave Usuario Historico

        public abstract bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum);

        #endregion


        #region Turno


        public abstract List<Moneda> ListarMonedas();

        public abstract bool verificarTurnoCerradoxUsuario(string strUsuario);

        public abstract bool verificarTurnoCerradoxPtoVenta(string strEquipo);

        public abstract bool CrearTurno(string strSec, string strUsuario, string strEquipo, ref string strTurnoError);

        public abstract Turno ObtenerTurnoIniciado(string strUsuario);

        public abstract bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos);

        public abstract bool ActualizarTurno(Turno objTurno);

        public abstract bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos);

        public abstract List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno);

        #endregion

        #region Operaciones

        public abstract List<Moneda> ListarMonedasInter();

        public abstract List<Limite> ListarLimitesPorOperacion(string stipoope);

        public abstract bool RegistrarOperacion(LogOperacion objOperacion);

        public abstract bool RegistrarOpeCaja(List<LogOperacCaja> objLista);

        public abstract TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo);

        public abstract bool RegistrarCompraVenta(LogCompraVenta objCompraVenta);

        public abstract TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans);

        public abstract DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion);

        public abstract DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo);

        public abstract bool RegistrarTicket(List<Ticket> listaTickets);

        public abstract bool IsError();

        public abstract string GetErrorCode();

        public abstract List<RepresentantCia> ListarRepteCia(string strCia);

        public abstract bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva);

        public abstract bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva);

        public abstract List<TipoTicket> ListarAllTipoTicket();

        public abstract List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp);

        public abstract List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta);

        public abstract List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania);

        public abstract bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio, string strMetPago, string strEmpresaRecaudadora);

        #endregion

        #region Administracion

        //Moneda

        public abstract DataTable listarAllMonedas();

        public abstract DataTable obtenerDetalleMoneda(string sCodMoneda);

        public abstract bool registrarTipoMoneda(Moneda objTipoMoneda);

        public abstract bool actualizarTipoMoneda(Moneda objTipoMoneda);

        public abstract bool eliminarTipoMoneda(string sCodMoneda);


        //Tipo Ticket

        public abstract DataTable ListaTipoTicket();

        public abstract bool registrarTipoTicket(TipoTicket TipoTicket);

        public abstract bool actualizarTipoTicket(TipoTicket TipoTicket);

        public abstract TipoTicket obtenerTipoTicket(string sCodTipoTicket);

        // Punto de Venta

        public abstract DataTable listarAllPuntoVenta();

        public abstract DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP);

        public abstract bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta);

        public abstract bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta);

        public abstract bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod);

        //Lista de Campo

        public abstract List<ListaDeCampo> obtenerListadeCampo(string sNomCampo);

        //Modalidad de Venta

        public abstract bool insertarModalidadVenta(ModalidadVenta objModalidadVenta);

        public abstract bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta);

        public abstract ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta);

        public abstract ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad);

        public abstract DataTable ListarAllModalidadVenta();

        public abstract List<ModalidadVenta> listarModalidadVenta();


        #endregion

        #region Consultas

        // Turno

        public abstract DataTable ListarAllTurno(string as_codturno);

        public abstract DataTable CantidadMonedasTurno(string as_codturno);

        public abstract DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda);

        public abstract DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string flagReporte = "0");


        // Compania

        public abstract DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion);

        public abstract DataTable listarAllCompania();

        // Usuarios

        public abstract DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion);

        public abstract DataTable ListarAllUsuario();

        // ParametrosGeneral

        public abstract DataTable ListarParametros(string as_identificador);

        // Ticket

        public abstract DataTable ConsultaDetalleTicket(string as_numeroticket);

        public abstract DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion);

        //ListaDeCampos

        public abstract DataTable ListaCamposxNombre(string sNombre);

        // Rol

        public abstract DataTable ListarRoles();

        #endregion


        #region Configuracion

        // Parametros generales

        public abstract DataTable ListarAllParametroGenerales(string strParam);

        public abstract DataTable DetalleParametroGeneralxId(string sIdentificador);

        public abstract bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla);

        public abstract DataTable ObtenerEmpresaPorIdentificadorPadre(string identificador);

        #endregion

        #region esilva
        // Lista de Campos
        public abstract bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int intTipo);
        public abstract DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo);
        // TasaCambio
        public abstract bool RegistrarTasaCambio(TasaCambio objTasaCambio);
        public abstract bool EliminarTasaCambio(string strCodTasaCambio);
        public abstract DataTable ObtenerTasaCambio(string strCodTasaCambio);
        // TasaCambioHist
        public abstract bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist);
        public abstract DataTable ObtenerTasaCambioHist(string strCodTasaCambio);
        // PrecioTicket
        public abstract bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket);
        public abstract bool EliminarPrecioTicket(string strCodPrecioTicket);
        public abstract DataTable ObtenerPrecioTicket(string strCodPrecioTicket);
        // PrecioTicketHist
        public abstract bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist);
        public abstract bool EliminarPrecioTicketHist(string strCodPrecioTicket);
        public abstract DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket);
        #endregion

        public abstract bool AnularTuua(string strListaTicket, int intTicket);

        public abstract bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad , string strMotivo, string strUsuario, ref string strMessage);

        public abstract DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac);

        public abstract bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion);

        public abstract DataTable obtenerFecha();

        public abstract ClaveUsuHist obtenerUsuarioHist(string sCodUsuario);

        public abstract DataTable ListarTasaCambio();

        public abstract DataTable GetTicketsByFilter(string strTicketDesde, string strTicketHasta, string strCodTurno);
        public abstract DataTable consultarDetalleTicket(string sNumTicket, string sTicketDesde, string sTicketHasta);
        public abstract DataTable consultarHistTicket(string sNumTicket);
        
    }
}
