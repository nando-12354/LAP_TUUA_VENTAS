using System;
using System.Collections.Generic;
using System.Text;
using wsError = LAP.TUUA.CLIENTEWS.WSError;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Error:CWS_Conexion
    {
        public override bool IsError()
        {
            wsError.WSError objWSError = new wsError.WSError();
            return objWSError.IsError();
        }

        public override string GetErrorCode()
        {
            wsError.WSError objWSError = new wsError.WSError();
            return objWSError.GetErrorCode();
        }

        public override LAP.TUUA.ENTIDADES.Usuario autenticar(string sCuenta, string sClave)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CambiarClave(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.Moneda> ListarMonedas()
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

        public override LAP.TUUA.ENTIDADES.Turno ObtenerTurnoIniciado(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTurnoMonto(List<LAP.TUUA.ENTIDADES.TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurno(LAP.TUUA.ENTIDADES.Turno objTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarTurnoMonto(List<LAP.TUUA.ENTIDADES.TurnoMonto> listaMontos)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.Moneda> ListarMonedasInter()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.Limite> ListarLimitesPorOperacion(string stipoope)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOperacion(LAP.TUUA.ENTIDADES.LogOperacion objOperacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarOpeCaja(List<LAP.TUUA.ENTIDADES.LogOperacCaja> objLista)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarCompraVenta(LAP.TUUA.ENTIDADES.LogCompraVenta objCompraVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
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

        public override bool RegistrarTicket(List<LAP.TUUA.ENTIDADES.Ticket> listaTickets)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override bool registrarUsuario(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarUsuario(LAP.TUUA.ENTIDADES.Usuario objUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarUsuario(string sCodUsuario, string LogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.Usuario obtenerUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarAllUsuario()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable LLenarMenu(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable LLenarArbolRoles(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override bool registrarRolUsuario(LAP.TUUA.ENTIDADES.UsuarioRol objRolUsuarioRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListaRol()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarRol(LAP.TUUA.ENTIDADES.Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarRol(LAP.TUUA.ENTIDADES.Rol objRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.Rol obtenerRolxnombre(string sNomRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.Rol obtenerRolxcodigo(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPerfilRol(LAP.TUUA.ENTIDADES.PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPerfilRol(LAP.TUUA.ENTIDADES.PerfilRol objPerfilRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable listarAllMonedas()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoMoneda(LAP.TUUA.ENTIDADES.Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoMoneda(LAP.TUUA.ENTIDADES.Moneda objTipoMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListaTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarTipoTicket(LAP.TUUA.ENTIDADES.TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarTipoTicket(LAP.TUUA.ENTIDADES.TipoTicket TipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarAllTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable CantidadMonedasTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public override System.Data.DataTable ListarParametros(string as_identificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultaDetalleTicket(string as_numeroticket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListaCamposxNombre(string as_nombre)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable listarAllCompania()
        {
            throw new Exception("The method or operation is not implemented.");
        }

       
        public override System.Data.DataTable listarAllPuntoVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool registrarPuntoVenta(LAP.TUUA.ENTIDADES.EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarPuntoVenta(LAP.TUUA.ENTIDADES.EstacionPtoVta objPuntoVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.ArbolModulo ObtenerArbolModulo(string sCodProceso, string sCodModulo, string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<LAP.TUUA.ENTIDADES.Rol> listaDeRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.Rol> listarRolesAsignados(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.Rol> listarRolesSinAsignar(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<LAP.TUUA.ENTIDADES.RepresentantCia> ListarRepteCia(string strCia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarVentaMasiva(LAP.TUUA.ENTIDADES.VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ActualizarVentaMasiva(LAP.TUUA.ENTIDADES.VentaMasiva objVentaMasiva)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override System.Data.DataTable ListarAllParametroGenerales(string strParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime dtFchVigencia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new Exception("The method or operation is not implemented.");
        }



        public override string FlagPerfilRolxOpcion(string sCodUsuario,string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ArbolModulo> ListarPerfilVenta(string strUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.TipoTicket> ListarAllTipoTicket()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool insertarModalidadVenta(LAP.TUUA.ENTIDADES.ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarModalidadVenta(LAP.TUUA.ENTIDADES.ModalidadVenta objModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override LAP.TUUA.ENTIDADES.ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarAllModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<LAP.TUUA.ENTIDADES.ModalidadVenta> listarModalidadVenta()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarListaDeCampo(LAP.TUUA.ENTIDADES.ListaDeCampo objListaCampo, int intTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambio(LAP.TUUA.ENTIDADES.TasaCambio objTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTasaCambioHist(LAP.TUUA.ENTIDADES.TasaCambioHist objTasaCambioHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicket(LAP.TUUA.ENTIDADES.PrecioTicket objPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarPrecioTicketHist(LAP.TUUA.ENTIDADES.PrecioTicketHist objPrecioTicketHist)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
        {
            throw new NotImplementedException();
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

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerFecha()
        {
            throw new NotImplementedException();
        }

        public override LAP.TUUA.ENTIDADES.ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTasaCambio()
        {
            throw new NotImplementedException();
        }
    }
}
