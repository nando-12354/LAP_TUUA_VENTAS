using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using wsTurno = LAP.TUUA.CLIENTEWS.WSTurno;
using wsAdmin=LAP.TUUA.CLIENTEWS.WSAdministracion;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Turno : CWS_Conexion
    {
        public override List<Moneda> ListarMonedas()
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            wsTurno.Moneda[] lista = service.ListarMonedas();
            List<Moneda> listaMonedas = new List<Moneda>();
            for (int i = 0; i < lista.Length; i++)
            {
                Moneda objMoneda = new Moneda();
                objMoneda.SCodMoneda = lista[i].SCodMoneda;
                objMoneda.SDscMoneda = lista[i].SDscMoneda;
                objMoneda.SDscSimbolo = lista[i].SDscSimbolo;
                listaMonedas.Add(objMoneda);
            }
            return listaMonedas;
        }

        public override bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            return service.verificarTurnoCerradoxUsuario(strUsuario);
        }

        public override bool verificarTurnoCerradoxPtoVenta(string strEquipo)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            return service.verificarTurnoCerradoxPtoVenta(strEquipo);
        }

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo, ref string strTurnoError)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            return service.crearTurno(strSec, strUsuario, strEquipo, ref strTurnoError);
        }

        public override Turno ObtenerTurnoIniciado(string strUsuario)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();

            wsTurno.Turno objWSTurno = service.obtenerTurnoIniciado(strUsuario);
            if (objWSTurno != null)
            {
                Turno objTurno = new Turno();
                objTurno.DtFchFin = objWSTurno.DtFchFin;
                objTurno.DtFchInicio = objWSTurno.DtFchInicio;
                objTurno.SCodEquipo = objWSTurno.SCodEquipo;
                objTurno.SCodTurno = objWSTurno.SCodTurno;
                objTurno.SCodUsuario = objWSTurno.SCodUsuario;
                objTurno.SHoraFin = objWSTurno.SHoraFin;
                objTurno.SHoraInicio = objWSTurno.SHoraInicio;
                return objTurno;
            }
            return null;
        }

        public override bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            List<wsTurno.TurnoMonto> lista = new List<WSTurno.TurnoMonto>();
            for (int i = 0; i < listaMontos.Count; i++)
            {
                wsTurno.TurnoMonto objWSTurnoMonto = new WSTurno.TurnoMonto();
                objWSTurnoMonto.DImpMontoActual = listaMontos[i].DImpMontoActual;
                objWSTurnoMonto.DImpMontoFinal = listaMontos[i].DImpMontoFinal;
                objWSTurnoMonto.DImpMontoInicial = listaMontos[i].DImpMontoInicial;
                objWSTurnoMonto.SCodMoneda = listaMontos[i].SCodMoneda;
                objWSTurnoMonto.SCodTurno = listaMontos[i].SCodTurno;
                lista.Add(objWSTurnoMonto);
            }
            return service.registrarTurnoMonto(lista.ToArray());
        }

        public override bool ActualizarTurno(Turno objTurno)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            wsTurno.Turno objWSTurno = new WSTurno.Turno();
            objWSTurno.DtFchFin = objTurno.DtFchFin;
            objWSTurno.DtFchInicio = objTurno.DtFchInicio;
            objWSTurno.SCodEquipo = objTurno.SCodEquipo;
            objWSTurno.SCodTurno = objTurno.SCodTurno;
            objWSTurno.SCodUsuario = objTurno.SCodUsuario;
            objWSTurno.SHoraFin = objTurno.SHoraFin;
            objWSTurno.SCodUsuarioCierre = objTurno.SCodUsuarioCierre;
            objWSTurno.SHoraInicio = objTurno.SHoraInicio;
            return service.ActualizarTurno(objWSTurno);
        }

        public override bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            List<wsTurno.TurnoMonto> lista = new List<WSTurno.TurnoMonto>();
            for (int i = 0; i < listaMontos.Count; i++)
            {
                wsTurno.TurnoMonto objWSTurnoMonto = new WSTurno.TurnoMonto();
                objWSTurnoMonto.DImpMontoActual = listaMontos[i].DImpMontoActual;
                objWSTurnoMonto.DImpMontoFinal = listaMontos[i].DImpMontoFinal;
                objWSTurnoMonto.Tip_Pago = listaMontos[i].Tip_Pago;
                objWSTurnoMonto.DImpMontoInicial = listaMontos[i].DImpMontoInicial;
                
                objWSTurnoMonto.SCodMoneda = listaMontos[i].SCodMoneda;
                objWSTurnoMonto.SCodTurno = listaMontos[i].SCodTurno;
                lista.Add(objWSTurnoMonto);
            }
            return service.ActualizarTurnoMonto(lista.ToArray());
        }

        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            wsTurno.WSTurno service = new wsTurno.WSTurno();
            wsTurno.TurnoMonto[] lista = service.ListarTurnoMontosPorTurno(strTurno);
            List<TurnoMonto> listaTurnoMonto = new List<TurnoMonto>();
            for (int i = 0; i < lista.Length; i++)
            {
                TurnoMonto objTurnoMonto = new TurnoMonto();
                objTurnoMonto.DImpMontoActual = lista[i].DImpMontoActual;
                objTurnoMonto.DImpMontoFinal = lista[i].DImpMontoFinal;
                objTurnoMonto.DImpMontoInicial = lista[i].DImpMontoInicial;
                objTurnoMonto.Tip_Pago = lista[i].Tip_Pago;
                objTurnoMonto.SCodMoneda = lista[i].SCodMoneda;
                objTurnoMonto.SCodTurno = lista[i].SCodTurno;
                listaTurnoMonto.Add(objTurnoMonto);
            }
            return listaTurnoMonto;
        }

        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            try
            {
                wsAdmin.WSAdministracion service = new wsAdmin.WSAdministracion();
                return service.obtenerDetallePuntoVenta(sCodEquipo, strIP);
            }
            catch
            {
                throw;
            }
        }

        public override DataTable ListarTasaCambio()
        {
            try
            {
                wsTurno.WSTurno service = new wsTurno.WSTurno();
                return service.ListarTasaCambio();
            }
            catch
            {
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

        public override DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
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

     
        public override DataTable listarAllCompania()
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

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string strFlgCambioClave)
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

        public override ClaveUsuHist obtenerUsuarioHist(string sCodUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
