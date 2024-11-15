using System;
using System.Collections.Generic;
using System.Text;
using wsOperacion = LAP.TUUA.CLIENTEWS.WSOperacion;
using LAP.TUUA.ENTIDADES;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Operacion : CWS_Conexion
    {
        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.Limite[] lista = service.ListarLimitesPorOperacion(stipoope);
            List<Limite> listaLimites = new List<Limite>();
            if (lista == null)
            {
                return null;
            }
            for (int i = 0; i < lista.Length; i++)
            {
                Limite objLimite = new Limite();
                objLimite.Cod_Moneda = lista[i].Cod_Moneda;
                objLimite.Cod_TipoOpera = lista[i].Cod_TipoOpera;
                objLimite.Imp_LimMaximo = lista[i].Imp_LimMaximo;
                objLimite.Imp_LimMinimo = lista[i].Imp_LimMinimo;
                objLimite.Imp_MargenCaja = lista[i].Imp_MargenCaja;
                listaLimites.Add(objLimite);
            }
            return listaLimites;
        }

        public override bool RegistrarOperacion(LogOperacion objOperacion)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.LogOperacion objWSOperacion = new wsOperacion.LogOperacion();
            objWSOperacion.SCodTipoOperacion = objOperacion.SCodTipoOperacion;
            objWSOperacion.SCodUsuario = objOperacion.SCodUsuario;
            objWSOperacion.SCodTurno = objOperacion.SCodTurno;
            return service.RegistrarOperacion(objWSOperacion);
        }

        public override bool RegistrarOpeCaja(List<LogOperacCaja> objListaCaja)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            List<wsOperacion.LogOperacCaja> objLista = new List<WSOperacion.LogOperacCaja>();
            for (int i = 0; i < objListaCaja.Count; i++)
            {
                wsOperacion.LogOperacCaja objCaja = new wsOperacion.LogOperacCaja();
                objCaja.INumSecuencial = objListaCaja[i].INumSecuencial;
                objCaja.SCodMoneda = objListaCaja[i].SCodMoneda;
                objCaja.DImpOperacion = objListaCaja[i].DImpOperacion;
                objCaja.SCodTipoOperacion = objListaCaja[0].SCodTipoOperacion;
                objCaja.SCodUsuario = objListaCaja[i].SCodUsuario;
                objCaja.SCodTurno = objListaCaja[i].SCodTurno;
                objLista.Add(objCaja);
            }
            return service.RegistrarOpeCaja(objLista.ToArray());
        }

        public override TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.TasaCambio wsobjTasaCambio = service.ObtenerTasaCambioPorMoneda(strMoneda, strTipo);
            if (wsobjTasaCambio != null)
            {
                TasaCambio objTasaCambio = new TasaCambio();
                objTasaCambio.SCodMoneda = wsobjTasaCambio.SCodMoneda;
                //revisar
                decimal dec = wsobjTasaCambio.DImpCambioActual;
               
                objTasaCambio.DImpCambioActual = dec;
                return objTasaCambio;
            }
            return null;
        }

        public override bool RegistrarCompraVenta(LogCompraVenta objCompraVenta)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.LogCompraVenta wsobjCompraVenta = CargarObjetoLogCompraVenta(objCompraVenta);
            return service.RegistrarCompraVenta(wsobjCompraVenta);
        }

        public override TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.TipoTicket objWSTipoTicket = service.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objWSTipoTicket == null)
            {
                return null;
            }
            TipoTicket objTipoTicket = new TipoTicket();
            if (objWSTipoTicket != null)
            {
                objTipoTicket.DImpPrecio = objWSTipoTicket.DImpPrecio;
                objTipoTicket.Dsc_Simbolo = objWSTipoTicket.Dsc_Simbolo;
                objTipoTicket.SCodMoneda = objWSTipoTicket.SCodMoneda;
                objTipoTicket.SCodTipoTicket = objWSTipoTicket.SCodTipoTicket;
                objTipoTicket.STipPasajero = objWSTipoTicket.STipPasajero;
                objTipoTicket.STipTrasbordo = objWSTipoTicket.STipTrasbordo;
                objTipoTicket.STipVuelo = objWSTipoTicket.STipVuelo;
                objTipoTicket.STipEstado = objWSTipoTicket.STipEstado;
                
            }
            return objTipoTicket;
        }

        public override DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.ConsultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
        }

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.ListarVuelosxCompania(strCompania, strFecha, strTipVuelo);
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            try
            {
                List<RepresentantCia> listaRepteCia = new List<RepresentantCia>();
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.RepresentantCia[] lista = service.ListarRepteCia(strCia);
                return listaRepteCia;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public override bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.VentaMasiva objWSVentaMas = new WSOperacion.VentaMasiva();
                objWSVentaMas.DCanVenta = objVentaMasiva.DCanVenta;
                objWSVentaMas.DImpMontoVenta = objVentaMasiva.DImpMontoVenta;
                objWSVentaMas.DtFchVenta = objVentaMasiva.DtFchVenta;
                objWSVentaMas.Num_Cheque_Trans = objVentaMasiva.Num_Cheque_Trans;
                objWSVentaMas.SCodCompania = objVentaMasiva.SCodCompania;
                objWSVentaMas.SCodMoneda = objVentaMasiva.SCodMoneda;
                objWSVentaMas.SCodUsuario = objVentaMasiva.SCodUsuario;
                objWSVentaMas.Tip_Pago = objVentaMasiva.Tip_Pago;
                return service.RegistrarVentaMasiva(objWSVentaMas);
            }
            catch
            {
                throw;
            }
        }

        public override bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.VentaMasiva objWSVentaMas = new WSOperacion.VentaMasiva();
                objWSVentaMas.SCodVentaMasiva = objVentaMasiva.SCodVentaMasiva;
                objWSVentaMas.INumRangoInicial = objVentaMasiva.INumRangoInicial;
                objWSVentaMas.INumRangoFinal = objVentaMasiva.INumRangoFinal;
                return service.ActualizarVentaMasiva(objWSVentaMas);
            }
            catch
            {
                throw;
            }
        }

        public override List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.ModVentaComp[] lista = service.ListarCompaniaxModVenta(strCodModVenta, strTipComp);
                List<ModVentaComp> listaModVenComp = new List<ModVentaComp>();
                if (lista == null)
                {
                    return null;
                }
                for (int i = 0; i < lista.Length; i++)
                {
                    ModVentaComp objModVenComp = new ModVentaComp();
                    objModVenComp.Dsc_Compania = lista[i].Dsc_Compania;
                    objModVenComp.Dsc_Ruc = lista[i].Dsc_Ruc;
                    objModVenComp.SCodCompania = lista[i].SCodCompania;
                    objModVenComp.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                    objModVenComp.SDscValorAcumulado = lista[i].SDscValorAcumulado;
                    listaModVenComp.Add(objModVenComp);
                }
                return listaModVenComp;
            }
            catch
            {
                throw;
            }
        }

        public override List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.ModalidadAtrib[] lista = service.ListarAtributosxModVenta(strCodModVenta);
                List<ModalidadAtrib> listaModAtrib = new List<ModalidadAtrib>();
                if (lista == null)
                {
                    return null; 
                }
                for (int i = 0; i < lista.Length; i++)
                {
                    ModalidadAtrib objModAtrib = new ModalidadAtrib();
                    objModAtrib.SCodAtributo = lista[i].SCodAtributo;
                    objModAtrib.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                    objModAtrib.SCodTipoTicket = lista[i].SCodTipoTicket;
                    objModAtrib.SDscValor = lista[i].SDscValor;
                    objModAtrib.Tip_Atributo = lista[i].Tip_Atributo;
                    listaModAtrib.Add(objModAtrib);
                }
                return listaModAtrib;
            }
            catch
            {
                throw;
            }
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.ModalidadAtrib[] lista = service.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
                if (lista == null)
                {
                    return null;
                }
                List<ModalidadAtrib> listaModAtrib = new List<ModalidadAtrib>();
                for (int i = 0; i < lista.Length; i++)
                {
                    ModalidadAtrib objModAtrib = new ModalidadAtrib();
                    objModAtrib.SCodAtributo = lista[i].SCodAtributo;
                    objModAtrib.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                    objModAtrib.SCodTipoTicket = lista[i].SCodTipoTicket;
                    objModAtrib.SDscValor = lista[i].SDscValor;
                    objModAtrib.Tip_Atributo = lista[i].Tip_Atributo;
                    listaModAtrib.Add(objModAtrib);
                }
                return listaModAtrib;
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

        public wsOperacion.LogCompraVenta CargarObjetoLogCompraVenta(LogCompraVenta objCompraVenta)
        {
            wsOperacion.LogCompraVenta wsobjCompraVenta = new wsOperacion.LogCompraVenta();
            wsobjCompraVenta.DImpACambiar = objCompraVenta.DImpACambiar;
            wsobjCompraVenta.DImpCambiado = objCompraVenta.DImpCambiado;
            wsobjCompraVenta.DImpEgreso = objCompraVenta.DImpEgreso;
            wsobjCompraVenta.DImpEntregarInter = objCompraVenta.DImpEntregarInter;
            wsobjCompraVenta.DImpEntregarNac = objCompraVenta.DImpEntregarNac;
            wsobjCompraVenta.DImpIngreso = objCompraVenta.DImpIngreso;
            wsobjCompraVenta.DImpTasaCambio = objCompraVenta.DImpTasaCambio;
            wsobjCompraVenta.ICodTipoCambio = objCompraVenta.ICodTipoCambio;
            wsobjCompraVenta.INumSecuencial = objCompraVenta.INumSecuencial;
            wsobjCompraVenta.Tip_Pago = objCompraVenta.Tip_Pago;
            wsobjCompraVenta.SCodMoneda = objCompraVenta.SCodMoneda;
            wsobjCompraVenta.SCodTipoOperacion = objCompraVenta.SCodTipoOperacion;
            wsobjCompraVenta.SCodTurno = objCompraVenta.SCodTurno;
            wsobjCompraVenta.SCodUsuario = objCompraVenta.SCodUsuario;
            return wsobjCompraVenta;

        }


        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override List<Moneda> ListarMonedasInter()
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.Moneda[] lista = service.ListarMonedasInter();
            if (lista == null)
            {
                return null;
            }
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

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio, string strMetPago, string strEmpresaRecaudadora)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.GenerarTicket(strCompania, strVentaMasiva, strNumVuelo, strFecVuelo, strTurno, strUsuario, decPrecio, strMoneda, strModVenta, strTipTicket, strTipVuelo, intTickets, strFlagCont, strNumRef, strTipPago, strEmpresa, strRepte, ref  strFecVence, ref strListaTickets, strMetPago, strEmpresaRecaudadora);
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.AnularTuua(strListaTicket, intTicket);
            }
            catch
            {
                throw;
            }
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

        public override System.Data.DataTable ListaRol()
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

        public override System.Data.DataTable ListaTipoTicket()
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

        public override System.Data.DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
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

        public override DataTable ObtenerEmpresaPorIdentificadorPadre(string identificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<TipoTicket> ListarAllTipoTicket()
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.TipoTicket[] lista = service.ListarAllTipoTicket();
            if (lista == null)
            {
                return null;
            }
            List<TipoTicket> listaTipoTicket = new List<TipoTicket>();
            for (int i = 0; i < lista.Length; i++)
            {
                TipoTicket objTipoTicket = new TipoTicket();
                objTipoTicket.DImpPrecio = lista[i].DImpPrecio;
                objTipoTicket.Dsc_Simbolo = lista[i].Dsc_Simbolo;
                objTipoTicket.SCodMoneda = lista[i].SCodMoneda;
                objTipoTicket.SCodTipoTicket = lista[i].SCodTipoTicket;
                objTipoTicket.STipEstado = lista[i].STipEstado;
                objTipoTicket.STipPasajero = lista[i].STipPasajero;
                objTipoTicket.STipTrasbordo = lista[i].STipTrasbordo;
                objTipoTicket.STipVuelo = lista[i].STipVuelo;
                listaTipoTicket.Add(objTipoTicket);
            }
            return listaTipoTicket;
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

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string flagReporte = "0")
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

        public override DataTable GetTicketsByFilter(string strTicketDesde, string strTicketHasta, string strCodTurno)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad , string strMotivo, string strUsuario, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarDetalleTicket(string sNumTicket, string sTicketDesde, string sTicketHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarHistTicket(string sNumTicket)
        {
            throw new NotImplementedException();
        }
    }
}
