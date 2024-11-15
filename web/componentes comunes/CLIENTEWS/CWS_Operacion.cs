using System;
using System.Collections.Generic;
using System.Text;
using wsOperacion = LAP.TUUA.CLIENTEWS.WSOperacion;
using LAP.TUUA.ENTIDADES;
using System.Data;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CLIENTEWS
{

    public class CWS_Operacion : CWS_Conexion
    {
        protected WSOperacion.WSOperacion objWSOperacion;

        public override List<Limite> ListarLimitesPorOperacion(string stipoope)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.Limite[] lista = service.ListarLimitesPorOperacion(stipoope);
            List<Limite> listaLimites = new List<Limite>();
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
            TipoTicket objTipoTicket = null;
            if (objWSTipoTicket != null)
            {
                objTipoTicket = new TipoTicket();
                objTipoTicket.DImpPrecio = objWSTipoTicket.DImpPrecio;
                objTipoTicket.Dsc_Simbolo = objWSTipoTicket.Dsc_Simbolo;
                objTipoTicket.SCodMoneda = objWSTipoTicket.SCodMoneda;
                objTipoTicket.SCodTipoTicket = objWSTipoTicket.SCodTipoTicket;
                objTipoTicket.STipPasajero = objWSTipoTicket.STipPasajero;
                objTipoTicket.STipTrasbordo = objWSTipoTicket.STipTrasbordo;
                objTipoTicket.STipVuelo = objWSTipoTicket.STipVuelo;

            }
            return objTipoTicket;
        }

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.TipoTicket objWSTipoTicket = service.ValidarTipoTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            TipoTicket objTipoTicket = null;
            if (objWSTipoTicket != null)
            {
                objTipoTicket = new TipoTicket();
                objTipoTicket.DImpPrecio = objWSTipoTicket.DImpPrecio;
                objTipoTicket.Dsc_Simbolo = objWSTipoTicket.Dsc_Simbolo;
                objTipoTicket.SCodMoneda = objWSTipoTicket.SCodMoneda;
                objTipoTicket.SCodTipoTicket = objWSTipoTicket.SCodTipoTicket;
                objTipoTicket.STipPasajero = objWSTipoTicket.STipPasajero;
                objTipoTicket.STipTrasbordo = objWSTipoTicket.STipTrasbordo;
                objTipoTicket.STipVuelo = objWSTipoTicket.STipVuelo;

            }
            return objTipoTicket;
        }

        public override DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.ConsultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
        }

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.ListarVuelosxCompania(strCompania, strFecha, null);
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            List<wsOperacion.Ticket> lista = new List<WSOperacion.Ticket>();
            for (int i = 0; i < listaTickets.Count; i++)
            {
                wsOperacion.Ticket objTicket = new wsOperacion.Ticket();
                objTicket.DImpPrecio = listaTickets[i].DImpPrecio;
                objTicket.DtFchVuelo = listaTickets[i].DtFchVuelo;
                objTicket.Flg_Contingencia = listaTickets[i].Flg_Contingencia;
                objTicket.Num_Referencia = listaTickets[i].Num_Referencia;
                objTicket.SCodCompania = listaTickets[i].SCodCompania;
                objTicket.SCodModalidadVenta = listaTickets[i].SCodModalidadVenta;
                objTicket.SCodMoneda = listaTickets[i].SCodMoneda;
                objTicket.SCodTipoTicket = listaTickets[i].SCodTipoTicket;
                objTicket.SCodTurno = listaTickets[i].SCodTurno;
                objTicket.SCodUsuarioVenta = listaTickets[i].SCodUsuarioVenta;
                objTicket.SCodVentaMasiva = listaTickets[i].SCodVentaMasiva;
                objTicket.SDscNumVuelo = listaTickets[i].SDscNumVuelo;
                objTicket.Tip_Vuelo = listaTickets[i].Tip_Vuelo;
                objTicket.Cant_Ticket = listaTickets[i].Cant_Ticket;
                lista.Add(objTicket);
            }
            return service.RegistrarTicket(lista.ToArray(), ref strListaTickets);
        }


        public override List<RepresentantCia> ListarRepteCia(string strCia)
        {
            try
            {
                List<RepresentantCia> listaRepteCia = new List<RepresentantCia>();
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                wsOperacion.RepresentantCia[] lista = service.ListarRepteCia(strCia);
                for (int i = 0; i < lista.Length; i++)
                {
                    RepresentantCia objRepteCia = new RepresentantCia();
                    objRepteCia.SApeRepresentante = lista[i].SApeRepresentante;
                    objRepteCia.SCargoRepresentante = lista[i].SApeRepresentante;
                    objRepteCia.SLogFechaMod = lista[i].SLogFechaMod;
                    objRepteCia.SLogHoraMod = lista[i].SLogHoraMod;
                    objRepteCia.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                    objRepteCia.SCodCompania = lista[i].SCodCompania;
                    objRepteCia.SNomRepresentante = lista[i].SNomRepresentante;
                    objRepteCia.STipEstado = lista[i].STipEstado;
                    objRepteCia.INumSecuencial = lista[i].INumSecuencial;
                    objRepteCia.STDocRepresentante = lista[i].STDocRepresentante;
                    objRepteCia.SNDocRepresentante = lista[i].SNDocRepresentante;
                    listaRepteCia.Add(objRepteCia);
                }
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

        public override bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ExtornarCompraVenta(strCodOpera, strTurno, intCantidad, ref strMessage);
            }
            catch
            {
                throw;
            }
        }


        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ExtornarTicket(strListaTickets, strTurno, intCantidad, strUsuario, strMotivo, ref strMessage);
            }
            catch
            {
                throw;
            }
        }

        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ExtornoRehabilitacion(strListaTickets, intCantidad, strUsuario, strEstado,transaccion);
            }
            catch
            {
                throw;
            }
        }


        public override List<TipoTicket> ListarAllTipoTicket()
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.TipoTicket[] lista = service.ListarAllTipoTicket();
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

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.AnularTicket(sCodNumeroTicket, sDscMotivo, sUsuarioMod);
            }
            catch
            {
                throw;
            }
        }

        public override bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.BoardingBcbp objWSBoardingBcbp = new wsOperacion.BoardingBcbp();

            objWSBoardingBcbp.INumSecuencial = objBoardingBcbp.INumSecuencial;
            objWSBoardingBcbp.SCodCompania = objBoardingBcbp.SCodCompania;
            objWSBoardingBcbp.SNumVuelo = objBoardingBcbp.SNumVuelo;
            objWSBoardingBcbp.StrFchVuelo = objBoardingBcbp.StrFchVuelo;
            objWSBoardingBcbp.StrNumAsiento = objBoardingBcbp.StrNumAsiento;
            objWSBoardingBcbp.StrNomPasajero = objBoardingBcbp.StrNomPasajero;
            objWSBoardingBcbp.StrTrama = objBoardingBcbp.StrTrama;
            objWSBoardingBcbp.StrLogUsuarioMod = objBoardingBcbp.StrLogUsuarioMod;
            objWSBoardingBcbp.StrLogFechaMod = objBoardingBcbp.StrLogFechaMod;
            objWSBoardingBcbp.StrLogHoraMod = objBoardingBcbp.StrLogHoraMod;
            objWSBoardingBcbp.StrTipIngreso = objBoardingBcbp.StrTipIngreso;
            objWSBoardingBcbp.StrTip_Estado = objBoardingBcbp.StrTip_Estado;

            return service.ActualizarEstadoBCBP(objWSBoardingBcbp);
        }

        public override bool AnularBCBP(BoardingBcbp objBoardingBcbp)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            wsOperacion.BoardingBcbp objWSBoardingBcbp = new wsOperacion.BoardingBcbp();

            objWSBoardingBcbp.SCodCompania = objBoardingBcbp.SCodCompania;
            objWSBoardingBcbp.SNumVuelo = objBoardingBcbp.SNumVuelo;
            objWSBoardingBcbp.StrFchVuelo = objBoardingBcbp.StrFchVuelo;
            objWSBoardingBcbp.StrNumAsiento = objBoardingBcbp.StrNumAsiento;
            objWSBoardingBcbp.StrNomPasajero = objBoardingBcbp.StrNomPasajero;
            objWSBoardingBcbp.StrLogUsuarioMod = objBoardingBcbp.StrLogUsuarioMod;
            objWSBoardingBcbp.StrMotivo = objBoardingBcbp.StrMotivo;

            return service.AnularBCBP(objWSBoardingBcbp);
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, 
                                             string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, 
                                             string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa,
                                             string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
            return service.GenerarTicket(strCompania, strVentaMasiva, strNumVuelo, strFecVuelo, strTurno, strUsuario, decPrecio, strMoneda, 
                                         strModVenta, strTipTicket, strTipVuelo, intTickets, strFlagCont, strNumRef, strTipPago, strEmpresa,
                                         strRepte, ref  strFecVence, ref strListaTickets, strCodTurnoIng, strFlgCierreTurno, strCodPrecio);
        }

        public override DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ObtenerVentaTicket(strFecIni, strFecFin, strTipVenta, strFlgAero);
            }
            catch
            {
                throw;
            }
        }

        public override DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento, string strTipVenta, string strFlgAero)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento, strTipVenta, strFlgAero);
            }
            catch
            {
                throw;
            }
        }

        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.RegistrarVentaContingencia(strCompania, strNumVuelo, strUsuario, strMoneda, strTipTicket, strFecVenta, intTickets, strListaTickets, strCodTurno, strFlagTurno, ref strCodTurnoCreado, ref strCodError);
            }
            catch
            {
                throw;
            }
        }

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ListarContingencia(strTipTikcet, strFlgConti, strNumIni, strNumFin, strUsuario);
            }
            catch
            {
                throw;
            }
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

        #region Molinete

        public override System.Data.DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            try
            {
                wsOperacion.WSOperacion objWSOperacion = new wsOperacion.WSOperacion();
                return objWSOperacion.ListarAllMolinete(strCodMolinete, strDscIp);
            }
            catch
            {
                throw;
            }
        }


        public override bool actualizarMolinete(Molinete objMolinete)
        {
            try
            {
                wsOperacion.WSOperacion objWSOperacionMolinete = new wsOperacion.WSOperacion();
                wsOperacion.Molinete objWSMolinete = new LAP.TUUA.CLIENTEWS.WSOperacion.Molinete();

                objWSMolinete.SCodMolinete = objMolinete.SCodMolinete;
                objWSMolinete.SDscIp = objMolinete.SDscIp;
                objWSMolinete.SDscMolinete = objMolinete.SDscMolinete;
                objWSMolinete.STipDocumento = objMolinete.STipDocumento;
                objWSMolinete.STipVuelo = objMolinete.STipVuelo;
                objWSMolinete.STipAcceso = objMolinete.STipAcceso;
                objWSMolinete.STipEstado = objMolinete.STipEstado;
                objWSMolinete.SLogUsuarioMod = objMolinete.SLogUsuarioMod;
                objWSMolinete.DtLogFechaMod = objMolinete.DtLogFechaMod;
                objWSMolinete.SLogHoraMod = objMolinete.SLogHoraMod;
                objWSMolinete.SEstMaster = objMolinete.SEstMaster;
                objWSMolinete.SDscDBName = objMolinete.SDscDBName;
                objWSMolinete.SDscDBUser = objMolinete.SDscDBUser;
                objWSMolinete.SDscDBPassword = objMolinete.SDscDBPassword;
                objWSMolinete.ICantidad = objMolinete.ICantidad;

                return objWSOperacionMolinete.ActualizarMolinete(objWSMolinete);
            }
            catch
            {
                throw;
            }
        }


        public override bool actualizarUnMolinete(Molinete objMolinete)
        {
            try
            {
                wsOperacion.WSOperacion objWSOperacionMolinete = new wsOperacion.WSOperacion();
                wsOperacion.Molinete objWSMolinete = new LAP.TUUA.CLIENTEWS.WSOperacion.Molinete();

                objWSMolinete.SCodMolinete = objMolinete.SCodMolinete;
                objWSMolinete.SDscIp = objMolinete.SDscIp;
                objWSMolinete.SDscMolinete = objMolinete.SDscMolinete;
                objWSMolinete.STipDocumento = objMolinete.STipDocumento;
                objWSMolinete.STipVuelo = objMolinete.STipVuelo;
                objWSMolinete.STipAcceso = objMolinete.STipAcceso;
                objWSMolinete.STipEstado = objMolinete.STipEstado;
                objWSMolinete.SLogUsuarioMod = objMolinete.SLogUsuarioMod;
                objWSMolinete.DtLogFechaMod = objMolinete.DtLogFechaMod;
                objWSMolinete.SLogHoraMod = objMolinete.SLogHoraMod;
                objWSMolinete.SEstMaster = objMolinete.SEstMaster;
                objWSMolinete.SDscDBName = objMolinete.SDscDBName;
                objWSMolinete.SDscDBUser = objMolinete.SDscDBUser;
                objWSMolinete.SDscDBPassword = objMolinete.SDscDBPassword;
                
                return objWSOperacionMolinete.ActualizarUnMolinete(objWSMolinete);
            }
            catch
            {
                throw;
            }
        }


        public override System.Data.DataTable ListarAllMolinetes()
        {
            try
            {
                wsOperacion.WSOperacion objWSOperacion = new wsOperacion.WSOperacion();
                return objWSOperacion.ListarAllMolinetes();
            }
            catch
            {
                throw;
            }
        }

        public override System.Data.DataTable obtenerMolinete(String strCodMolinete)
        {
            try
            {
                wsOperacion.WSOperacion objWSOperacion = new wsOperacion.WSOperacion();
                return objWSOperacion.obtenerMolinete(strCodMolinete);
            }
            catch
            {
                throw;
            }
        }

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                string strEstado = Define.ESTADO_TICKET_EMITIDO;
                return service.ExtenderVigenciaTicket(strListaTickets, strListaFechas, strUsuario, strEstado);
            }
            catch
            {
                throw;
            }
        }

        public override bool ActualizarTicket(List<Ticket> listaTickets)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                List<wsOperacion.Ticket> lista = new List<WSOperacion.Ticket>();
                for (int i = 0; i < listaTickets.Count; i++)
                {
                    wsOperacion.Ticket objTicket = new wsOperacion.Ticket();
                    objTicket.DImpPrecio = listaTickets[i].DImpPrecio;
                    objTicket.DtFchVuelo = listaTickets[i].DtFchVuelo;
                    objTicket.Flg_Contingencia = listaTickets[i].Flg_Contingencia;
                    objTicket.Num_Referencia = listaTickets[i].Num_Referencia;
                    objTicket.SCodCompania = listaTickets[i].SCodCompania;
                    objTicket.SCodModalidadVenta = listaTickets[i].SCodModalidadVenta;
                    objTicket.SCodMoneda = listaTickets[i].SCodMoneda;
                    objTicket.SCodTipoTicket = listaTickets[i].SCodTipoTicket;
                    objTicket.SCodTurno = listaTickets[i].SCodTurno;
                    objTicket.SCodUsuarioVenta = listaTickets[i].SCodUsuarioVenta;
                    objTicket.SCodVentaMasiva = listaTickets[i].SCodVentaMasiva;
                    objTicket.SDscNumVuelo = listaTickets[i].SDscNumVuelo;
                    objTicket.Tip_Vuelo = listaTickets[i].Tip_Vuelo;
                    objTicket.Cant_Ticket = listaTickets[i].Cant_Ticket;
                    objTicket.STipEstadoActual = listaTickets[i].STipEstadoActual;
                    objTicket.SCodNumeroTicket = listaTickets[i].SCodNumeroTicket;
                    lista.Add(objTicket);
                }
                return service.ActualizarTicket(lista.ToArray());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        public override string ObtenerFechaActual()
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ObtenerFechaActual();
            }
            catch
            {
                throw;
            }
        }

        public override string obtenerFechaEstadistico(string sEstadistico)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ListarBcbpxConciliar(sCodCompania, strFchVuelo, strNumVuelo, strNumAsiento, strPasajero, strFecUsoIni, strFecUsoFin, strFlg);
            }
            catch
            {
                throw;
            }
        }


        public override bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            try
            {
                wsOperacion.WSOperacion service = new wsOperacion.WSOperacion();
                return service.ObtenerDetalleTurnoActual(strCodUsuario, ref strCantTickets, ref strCodTurno, ref strFecHorTurno);
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

        public override List<Moneda> ListarMonedasxTipoTicket()
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

        public override bool CrearTurno(string strSec, string strUsuario, string strEquipo)
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

        public override System.Data.DataTable ListarAllUsuario()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarUsuarioxRol(string sCodRol)
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


        public override System.Data.DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable ListarParametros(string as_identificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        public override DataTable DetalleBoardingEstHist_Arch(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
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

        public override bool actualizarContrase�aUsuario(string sCodUsuario, string sContrase�a, string SLogUsuarioMod, DateTime DtFchVigencia)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw)
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

        public override System.Data.DataTable ListarAllParametroGenerales(string strParametro)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override System.Data.DataTable DetalleParametroGeneralxId(string sIdentificador)
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

        public override DataTable ConsultaDetalleTicket(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }



        public override DataTable ConsultaDetalleTicket(string sNumeroTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketEstHist(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketEstHist_Arch(string as_numeroticket)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override System.Data.DataTable ConsultarRepresXRehabilitacionYCia(String strCia)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTurnosAbiertos()
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoarding(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override List<ParameGeneral> listarAtributosGenerales()
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }

        public override bool obtenerRolHijo(string sCodRol)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }


        public override DataTable obtenerUsuarioxFechaOperacion(string sFechaOperacion, string sCodUsuario, string sTipoOperacion, string sCodMoneda)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            throw new NotImplementedException();
        }


        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle)
        {
            throw new NotImplementedException();
        }


        public override DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }



        public override bool insertarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }
        public override bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCompania(Compania objCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaComp(ModVentaComp objModComp)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaComp> ListarModVentaCompxCompa�ia(string sCodCompania)
        {
            throw new NotImplementedException();
        }

        public override bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            throw new NotImplementedException();
        }

        public override List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompa�iaxcodigo(string sCodigoCompania)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompa�iaxnombre(string sNombreCompania)
        {
            throw new NotImplementedException();
        }



        public override DataTable listarCompania_xCodigoEspecial(string codigoEspecial)
        {
            throw new NotImplementedException();
        }

        public override DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
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

        public override bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            throw new NotImplementedException();
        }

        public override bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllCnfgAlarma()
        {
            throw new NotImplementedException();
        }

        public override CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllAlarmaGenerada()
        {
            throw new NotImplementedException();
        }

        public override AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAllModulo()
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarAlarmaGeneradaEnviadas()
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            throw new NotImplementedException();
        }

        public override bool EliminarListaDeCampo(string strNomCampo, string strCodCampo)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerRecaudacionMensual(string anio)
        {
            throw new NotImplementedException();
        }



        public override DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta, string as_TipoDocumento, string as_FlagAnulado)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta)
        {
            throw new NotImplementedException();
        }


        public override DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador)
        {
            throw new NotImplementedException();
        }

        public override bool insertarSecuenciaModVentaComp(string codModalidad)
        {
            throw new NotImplementedException();
        }

        public override int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            throw new NotImplementedException();
        }


        public override int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            throw new NotImplementedException();
        }


        public override DataTable ConsultaTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno)
        {
            throw new NotImplementedException();
        }


        public override DataTable ListarTicketProcesado(string strCodTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }
        public override DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }
        public override DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }

        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }


        public override DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
        {
            throw new NotImplementedException();
        }

        public override int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            throw new NotImplementedException();
        }

        public override int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa, string as_horadesde, string as_horahasta, string as_FlgReporte)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerEstadistico()
        {
            throw new NotImplementedException();
        }
        public override DataTable ListaCamposxNombreOrderByDesc(string sNombre)
        {
            throw new NotImplementedException();
        }
        public override DataTable DetalleBoarding_REH(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ConsultaDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_ColumnSort, int i_IniRows, int i_MaxRows, string as_TotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingPagin(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerConsultaAuditoriaPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }


        public override DataTable ConsultaDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_flgtotal)
        {
            throw new NotImplementedException();
        }



        public override DataTable ListarTicketxFechaPagin(string as_TipDoc, string as_FchDesde, string as_FchHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_EstadoTicket, string as_TipoTicket, string as_TipoPersona, string as_TipoVuelo, string as_CodBoarding, string as_Turno, string as_FlgCobro, string as_FlgMasiva, string as_EstadoTurno, string as_Cajero, string as_MedioAnulacion, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalleBoardingArchivado(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingAsociados(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
        {
            throw new NotImplementedException();
        }

        public override string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosResumen(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompaniaPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
        {
            throw new NotImplementedException();
        }


        public override bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes, string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket, string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding, string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketsExtorno(string as_Cod_Ticket, string as_Ticket_Desde, string as_Ticket_Hasta, string as_Cod_Turno, string as_ColumnaSort, int as_iStartRows, int as_iMaxRows, string as_Paginacion, string as_FlgTotalRows)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarParametrosDefaultValue(string as_identificador)
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

        public override DataTable ListarSincronizacion()
        {
            throw new NotImplementedException();
        }





        public override DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta,  string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarHoras(ListaDeCampo objListaCampo)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarFiltroDepuracion(string as_molinete, string as_estado, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sfiltro, string sOrdenacion)
        {
            throw new NotImplementedException();
        }

        public override void IngresarDatosATemporalBoardingPass(string strTrama)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketsTablaTemporal()
        {
            throw new NotImplementedException();
        }
    }

}