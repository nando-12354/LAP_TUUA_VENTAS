using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using wsAdministracion = LAP.TUUA.CLIENTEWS.WSAdministracion;
using System.Data;

namespace LAP.TUUA.CLIENTEWS
{
    public class CWS_Administracion : CWS_Conexion
    {
        protected wsAdministracion.WSAdministracion objWSAdministracion;

        public CWS_Administracion()
        {
            objWSAdministracion = new wsAdministracion.WSAdministracion();
        }

        #region Moneda

        public override DataTable listarAllMonedas()
        {
            DataTable DTListaAllMonedas;

            DTListaAllMonedas = objWSAdministracion.listarAllMonedas();

            return DTListaAllMonedas;

        }



        public override DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            DataTable DTDetalleMoneda;

            DTDetalleMoneda = objWSAdministracion.obtenerDetalleMoneda(sCodMoneda);

            return DTDetalleMoneda;

        }


        public override bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            wsAdministracion.Moneda objWSTipoMoneda = new wsAdministracion.Moneda();

            objWSTipoMoneda.SCodMoneda = objTipoMoneda.SCodMoneda;
            objWSTipoMoneda.SDscMoneda = objTipoMoneda.SDscMoneda;
            objWSTipoMoneda.SDscSimbolo = objTipoMoneda.SDscSimbolo;
            objWSTipoMoneda.STipEstado = objTipoMoneda.STipEstado;
            objWSTipoMoneda.SLogUsuarioMod = objTipoMoneda.SLogUsuarioMod;
            objWSTipoMoneda.DtLogFechaMod = objTipoMoneda.DtLogFechaMod;
            objWSTipoMoneda.SLogHoraMod = objTipoMoneda.SLogHoraMod;

            return objWSAdministracion.registrarTipoMoneda(objWSTipoMoneda);
        }

        public override bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            wsAdministracion.Moneda objWSTipoMoneda = new wsAdministracion.Moneda();

            objWSTipoMoneda.SCodMoneda = objTipoMoneda.SCodMoneda;
            objWSTipoMoneda.SDscMoneda = objTipoMoneda.SDscMoneda;
            objWSTipoMoneda.SDscSimbolo = objTipoMoneda.SDscSimbolo;
            objWSTipoMoneda.STipEstado = objTipoMoneda.STipEstado;
            objWSTipoMoneda.SLogUsuarioMod = objTipoMoneda.SLogUsuarioMod;
            objWSTipoMoneda.DtLogFechaMod = objTipoMoneda.DtLogFechaMod;
            objWSTipoMoneda.SLogHoraMod = objTipoMoneda.SLogHoraMod;

            return objWSAdministracion.actualizarTipoMoneda(objWSTipoMoneda);
        }



        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            return objWSAdministracion.eliminarTipoMoneda(sCodMoneda);
        }

        #endregion

        #region Tipo Ticket

        public override DataTable ListaTipoTicket()
        {

            DataTable DTTipoTicket;

            DTTipoTicket = objWSAdministracion.ListarTipoTicket();
            return DTTipoTicket;
        }

        public override bool registrarTipoTicket(TipoTicket objTipoTicket)
        {
            wsAdministracion.TipoTicket objWSTipoTicket = new wsAdministracion.TipoTicket();

            objWSTipoTicket.SCodTipoTicket = objTipoTicket.SCodTipoTicket;
            objWSTipoTicket.SNomTipoTicket = objTipoTicket.SNomTipoTicket;
            objWSTipoTicket.STipVuelo = objTipoTicket.STipVuelo;
            objWSTipoTicket.STipPasajero = objTipoTicket.STipPasajero;
            objWSTipoTicket.STipTrasbordo = objTipoTicket.STipTrasbordo;
            objWSTipoTicket.STipEstado = objTipoTicket.STipEstado;
            objWSTipoTicket.SCodMoneda = objTipoTicket.SCodMoneda;
            objWSTipoTicket.DImpPrecio = objTipoTicket.DImpPrecio;
            objWSTipoTicket.SLogUsuarioMod = objTipoTicket.SLogUsuarioMod;
            objWSTipoTicket.SLogHoraMod = objTipoTicket.SLogHoraMod;
            objWSTipoTicket.SLogFechaMod = objTipoTicket.SLogFechaMod;

            return objWSAdministracion.registrarTipoTicket(objWSTipoTicket);

        }

        public override bool actualizarTipoTicket(TipoTicket objTipoTicket)
        {

            wsAdministracion.TipoTicket objWSTipoTicket = new wsAdministracion.TipoTicket();

            objWSTipoTicket.SCodTipoTicket = objTipoTicket.SCodTipoTicket;
            objWSTipoTicket.SNomTipoTicket = objTipoTicket.SNomTipoTicket;
            objWSTipoTicket.STipVuelo = objTipoTicket.STipVuelo;
            objWSTipoTicket.STipPasajero = objTipoTicket.STipPasajero;
            objWSTipoTicket.STipTrasbordo = objTipoTicket.STipTrasbordo;
            objWSTipoTicket.STipEstado = objTipoTicket.STipEstado;
            objWSTipoTicket.SCodMoneda = objTipoTicket.SCodMoneda;
            objWSTipoTicket.DImpPrecio = objTipoTicket.DImpPrecio;
            objWSTipoTicket.SLogUsuarioMod = objTipoTicket.SLogUsuarioMod;
            objWSTipoTicket.SLogHoraMod = objTipoTicket.SLogHoraMod;
            objWSTipoTicket.SLogFechaMod = objTipoTicket.SLogFechaMod;
            return objWSAdministracion.actualizarTipoTicket(objWSTipoTicket);
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            wsAdministracion.TipoTicket objWSTipoTicket = null;
            objWSTipoTicket = objWSAdministracion.obtenerTipoTicket(sCodTipoTicket);
            if (objWSTipoTicket != null)
            {
                return new TipoTicket(objWSTipoTicket.SCodTipoTicket, objWSTipoTicket.SNomTipoTicket,
                    objWSTipoTicket.STipVuelo, objWSTipoTicket.DImpPrecio, objWSTipoTicket.STipEstado,
                    objWSTipoTicket.SCodMoneda, objWSTipoTicket.STipPasajero, objWSTipoTicket.STipTrasbordo,
                    objWSTipoTicket.SLogFechaMod, objWSTipoTicket.SLogUsuarioMod,
                    objWSTipoTicket.SLogHoraMod);
            }
            else return null;
        }

        #endregion


        #region Punto de Venta

        public override DataTable listarAllPuntoVenta()
        {
            DataTable DTListaAllPuntoVenta;

            DTListaAllPuntoVenta = objWSAdministracion.listarAllPuntoVenta();

            return DTListaAllPuntoVenta;

        }

        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            DataTable DTDetallePuntoVenta;

            DTDetallePuntoVenta = objWSAdministracion.obtenerDetallePuntoVenta(sCodEquipo, strIP);


            return DTDetallePuntoVenta;

        }


        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            wsAdministracion.EstacionPtoVta objWSPuntoVenta = new wsAdministracion.EstacionPtoVta();

            objWSPuntoVenta.SCodEquipo = objPuntoVenta.SCodEquipo;
            objWSPuntoVenta.SNumIpEquipo = objPuntoVenta.SNumIpEquipo;
            objWSPuntoVenta.SFlgEstado = objPuntoVenta.SFlgEstado;
            objWSPuntoVenta.SCodUsuarioCreacion = objPuntoVenta.SCodUsuarioCreacion;
            objWSPuntoVenta.SLogUsuarioMod = objPuntoVenta.SLogUsuarioMod;
            objWSPuntoVenta.DtLogFechaMod = objPuntoVenta.DtLogFechaMod;
            objWSPuntoVenta.SLogHoraMod = objPuntoVenta.SLogHoraMod;
            objWSPuntoVenta.SDscEstacion = objPuntoVenta.SDscEstacion;

            return objWSAdministracion.registrarPuntoVenta(objWSPuntoVenta);
        }



        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            wsAdministracion.EstacionPtoVta objWSPuntoVenta = new wsAdministracion.EstacionPtoVta();

            objWSPuntoVenta.SCodEquipo = objPuntoVenta.SCodEquipo;
            objWSPuntoVenta.SNumIpEquipo = objPuntoVenta.SNumIpEquipo;
            objWSPuntoVenta.SFlgEstado = objPuntoVenta.SFlgEstado;
            objWSPuntoVenta.SCodUsuarioCreacion = objPuntoVenta.SCodUsuarioCreacion;
            objWSPuntoVenta.SLogUsuarioMod = objPuntoVenta.SLogUsuarioMod;
            objWSPuntoVenta.DtLogFechaMod = objPuntoVenta.DtLogFechaMod;
            objWSPuntoVenta.SLogHoraMod = objPuntoVenta.SLogHoraMod;
            objWSPuntoVenta.SDscEstacion = objPuntoVenta.SDscEstacion;

            return objWSAdministracion.actualizarPuntoVenta(objWSPuntoVenta);
        }



        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            return objWSAdministracion.eliminarPuntoVenta(sCodEquipo, sLogUsuarioMod);
        }

        #endregion

        #region Lista de Campo

        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            wsAdministracion.ListaDeCampo[] lista = objWSAdministracion.obtenerListadeCampo(sNomCampo);
            List<ListaDeCampo> listaListaDeCampo = new List<ListaDeCampo>();
            for (int i = 0; i < lista.Length; i++)
            {
                ListaDeCampo objListaDeCampo = new ListaDeCampo();

                objListaDeCampo.SNomCampo = lista[i].SNomCampo;
                objListaDeCampo.SCodCampo = lista[i].SCodCampo;
                objListaDeCampo.SCodRelativo = lista[i].SCodRelativo;
                objListaDeCampo.SDscCampo = lista[i].SDscCampo;
                objListaDeCampo.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objListaDeCampo.SLogFechaMod = lista[i].SLogFechaMod;
                objListaDeCampo.SLogHoraMod = lista[i].SLogHoraMod;

                listaListaDeCampo.Add(objListaDeCampo);
            }

            return listaListaDeCampo;
        }

        #endregion

        #region Modalidad de Venta


        public override bool insertarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = new wsAdministracion.ModalidadVenta();

            objWSModalidadVenta.SNomModalidad = objModalidadVenta.SNomModalidad;
            objWSModalidadVenta.STipModalidad = objModalidadVenta.STipModalidad;
            objWSModalidadVenta.SLogUsuarioMod = objModalidadVenta.SLogUsuarioMod;

            return objWSAdministracion.insertarModalidadVenta(objWSModalidadVenta);

        }


        public override bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = new wsAdministracion.ModalidadVenta();

            objWSModalidadVenta.SCodModalidadVenta = objModalidadVenta.SCodModalidadVenta;
            objWSModalidadVenta.SNomModalidad = objModalidadVenta.SNomModalidad;
            objWSModalidadVenta.STipModalidad = objModalidadVenta.STipModalidad;
            objWSModalidadVenta.STipEstado = objModalidadVenta.STipEstado;
            objWSModalidadVenta.SLogUsuarioMod = objModalidadVenta.SLogUsuarioMod;

            return objWSAdministracion.actualizarModalidadVenta(objWSModalidadVenta);

        }


        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = null;
            objWSModalidadVenta = objWSAdministracion.obtenerModalidadVentaxCodigo(sCodModalidad);
            if (objWSModalidadVenta != null)
            {
                return new ModalidadVenta(objWSModalidadVenta.SCodModalidadVenta, objWSModalidadVenta.SNomModalidad,
                    objWSModalidadVenta.STipModalidad, objWSModalidadVenta.STipEstado, objWSModalidadVenta.SLogUsuarioMod,
                    objWSModalidadVenta.SLogFechaMod, objWSModalidadVenta.SLogHoraMod);
            }
            else return null;

        }


        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            wsAdministracion.ModalidadVenta objWSModalidadVenta = null;
            objWSModalidadVenta = objWSAdministracion.obtenerModalidadVentaxNombre(sNomModalidad);
            if (objWSModalidadVenta != null)
            {
                return new ModalidadVenta(objWSModalidadVenta.SCodModalidadVenta, objWSModalidadVenta.SNomModalidad,
                    objWSModalidadVenta.STipModalidad, objWSModalidadVenta.STipEstado, objWSModalidadVenta.SLogUsuarioMod,
                    objWSModalidadVenta.SLogFechaMod, objWSModalidadVenta.SLogHoraMod);
            }
            else return null;

        }


        public override DataTable ListarAllModalidadVenta()
        {
            DataTable DTModalidadVenta;

            DTModalidadVenta = objWSAdministracion.ListarAllModalidadVenta();
            return DTModalidadVenta;
        }


        public override List<ModalidadVenta> listarModalidadVenta()
        {
            wsAdministracion.ModalidadVenta[] lista = objWSAdministracion.listarModalidadVenta();
            List<ModalidadVenta> listaModalidadVenta = new List<ModalidadVenta>();
            for (int i = 0; i < lista.Length; i++)
            {
                ModalidadVenta objModalidadVenta = new ModalidadVenta();

                objModalidadVenta.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                objModalidadVenta.SNomModalidad = lista[i].SNomModalidad;
                objModalidadVenta.STipModalidad = lista[i].STipModalidad;
                objModalidadVenta.STipEstado = lista[i].STipEstado;
                objModalidadVenta.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objModalidadVenta.SLogFechaMod = lista[i].SLogFechaMod;
                objModalidadVenta.SLogHoraMod = lista[i].SLogHoraMod;

                listaModalidadVenta.Add(objModalidadVenta);
            }

            return listaModalidadVenta;

        }



        #endregion

        #region Atributos Modalidad de Venta

        public override bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            wsAdministracion.ModalidadAtrib objWSModalidadAtrib = new wsAdministracion.ModalidadAtrib();

            objWSModalidadAtrib.SCodAtributo = objModalidadAtrib.SCodAtributo;
            objWSModalidadAtrib.SCodModalidadVenta = objModalidadAtrib.SCodModalidadVenta;
            objWSModalidadAtrib.Tip_Atributo = objModalidadAtrib.Tip_Atributo;
            objWSModalidadAtrib.SCodTipoTicket = objModalidadAtrib.SCodTipoTicket;
            objWSModalidadAtrib.SDscValor = objModalidadAtrib.SDscValor;
            objWSModalidadAtrib.SLogUsuarioMod = objModalidadAtrib.SLogUsuarioMod;
            objWSModalidadAtrib.SLogHoraMod = objModalidadAtrib.SLogHoraMod;
            objWSModalidadAtrib.SLogFechaMod = objModalidadAtrib.SLogFechaMod;

            return objWSAdministracion.insertarModVentaAtributo(objWSModalidadAtrib);
        }


        public override bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            wsAdministracion.ModalidadAtrib objWSModalidadAtrib = new wsAdministracion.ModalidadAtrib();

            objWSModalidadAtrib.SCodAtributo = objModalidadAtrib.SCodAtributo;
            objWSModalidadAtrib.SCodModalidadVenta = objModalidadAtrib.SCodModalidadVenta;
            objWSModalidadAtrib.SCodTipoTicket = objModalidadAtrib.SCodTipoTicket;
            objWSModalidadAtrib.SDscValor = objModalidadAtrib.SDscValor;
            objWSModalidadAtrib.SLogUsuarioMod = objModalidadAtrib.SLogUsuarioMod;
            objWSModalidadAtrib.SLogHoraMod = objModalidadAtrib.SLogHoraMod;
            objWSModalidadAtrib.SLogFechaMod = objModalidadAtrib.SLogFechaMod;

            return objWSAdministracion.actualizarModVentaAtributo(objWSModalidadAtrib);
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            return objWSAdministracion.eliminarModVentaAtributo(sCodModalidadVenta, sCodAtributo, sCodTipoTicket);
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {

            wsAdministracion.ModalidadAtrib[] lista = objWSAdministracion.ListarAtributosxModVentaTipoTicket(strCodModVenta, strTipoTicket);
            List<ModalidadAtrib> listaModalidadAtrib = new List<ModalidadAtrib>();
            for (int i = 0; i < lista.Length; i++)
            {
                ModalidadAtrib objModalidadAtrib = new ModalidadAtrib();

                objModalidadAtrib.SCodAtributo = lista[i].SCodAtributo;
                objModalidadAtrib.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                objModalidadAtrib.SCodTipoTicket = lista[i].SCodTipoTicket;
                objModalidadAtrib.SDscValor = lista[i].SDscValor;
                objModalidadAtrib.SLogUsuarioMod = lista[i].SLogUsuarioMod;
                objModalidadAtrib.SLogHoraMod = lista[i].SLogHoraMod;
                objModalidadAtrib.SLogFechaMod = lista[i].SLogFechaMod;

                listaModalidadAtrib.Add(objModalidadAtrib);
            }

            return listaModalidadAtrib;

        }

        public override int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            return objWSAdministracion.ValidarSerieTicket(serieInicial, serieFinal, modalidad);
        }

        #endregion

        #region Parametros Generales

        public override List<ParameGeneral> listarAtributosGenerales()
        {
            wsAdministracion.ParameGeneral[] lista = objWSAdministracion.listarAtributosGenerales();
            List<ParameGeneral> listaParameGeneral = new List<ParameGeneral>();
            for (int i = 0; i < lista.Length; i++)
            {
                ParameGeneral objParameGeneral = new ParameGeneral();

                objParameGeneral.SIdentificador = lista[i].SIdentificador;
                objParameGeneral.SDescripcion = lista[i].SDescripcion;
                objParameGeneral.STipoParametro = lista[i].STipoParametro;
                objParameGeneral.STipoParametroDet = lista[i].STipoParametroDet;
                objParameGeneral.STipoValor = lista[i].STipoValor;
                objParameGeneral.SValor = lista[i].SValor;
                objParameGeneral.SCampoLista = lista[i].SCampoLista;
                objParameGeneral.SIdentificadorPadre = lista[i].SIdentificadorPadre;
                objParameGeneral.SLog_Usuario_Mod = lista[i].SLog_Usuario_Mod;
                objParameGeneral.SLog_Fecha_Mod = lista[i].SLog_Fecha_Mod;
                objParameGeneral.SLog_Hora_Mod = lista[i].SLog_Hora_Mod;

                listaParameGeneral.Add(objParameGeneral);

            }

            return listaParameGeneral;

        }

        #endregion

        #region Compa�ia

        public override bool insertarCompania(Compania objCompania)
        {
            wsAdministracion.Compania objWSCompania = new wsAdministracion.Compania();

            objWSCompania.STipCompania = objCompania.STipCompania;
            objWSCompania.SDscCompania = objCompania.SDscCompania;
            objWSCompania.SCodEspecialCompania = objCompania.SCodEspecialCompania;
            objWSCompania.SCodAerolinea = objCompania.SCodAerolinea;
            objWSCompania.SCodSAP = objCompania.SCodSAP;
            objWSCompania.SCodOACI = objCompania.SCodOACI;
            objWSCompania.SCodIATA = objCompania.SCodIATA;
            objWSCompania.SDscRuc = objCompania.SDscRuc;
            objWSCompania.SLogUsuarioMod = objCompania.SLogUsuarioMod;

            return objWSAdministracion.insertarCompania(objWSCompania);
        }

        public override bool actualizarCompania(Compania objCompania)
        {
            wsAdministracion.Compania objWSCompania = new wsAdministracion.Compania();

            objWSCompania.SCodCompania = objCompania.SCodCompania;
            objWSCompania.STipCompania = objCompania.STipCompania;
            objWSCompania.STipEstado = objCompania.STipEstado;
            objWSCompania.SDscCompania = objCompania.SDscCompania;
            objWSCompania.SCodEspecialCompania = objCompania.SCodEspecialCompania;
            objWSCompania.SCodAerolinea = objCompania.SCodAerolinea;
            objWSCompania.SCodSAP = objCompania.SCodSAP;
            objWSCompania.SCodOACI = objCompania.SCodOACI;
            objWSCompania.SCodIATA = objCompania.SCodIATA;
            objWSCompania.SDscRuc = objCompania.SDscRuc;
            objWSCompania.SLogUsuarioMod = objCompania.SLogUsuarioMod;

            return objWSAdministracion.actualizarCompania(objWSCompania);
        }


        public override Compania obtenerCompa�iaxcodigo(string sCodigoCompania)
        {
            wsAdministracion.Compania objWSCompania = null;
            objWSCompania = objWSAdministracion.obtenerCompa�iaxcodigo(sCodigoCompania);
            if (objWSCompania != null)
            {
                return new Compania(objWSCompania.SCodCompania, objWSCompania.STipCompania,
                    objWSCompania.SDscCompania, objWSCompania.DtFchCreacion, objWSCompania.STipEstado,
                    objWSCompania.SCodEspecialCompania, objWSCompania.SDscRuc, objWSCompania.SCodAerolinea,
                    objWSCompania.SCodSAP, objWSCompania.SCodOACI, objWSCompania.SCodIATA, objWSCompania.SLogUsuarioMod,
                    objWSCompania.SLogFechaMod, objWSCompania.SLogHoraMod);
            }
            else return null;

        }


        public override Compania obtenerCompa�iaxnombre(string sNombreCompania)
        {
            wsAdministracion.Compania objWSCompania = null;
            objWSCompania = objWSAdministracion.obtenerCompa�iaxnombre(sNombreCompania);
            if (objWSCompania != null)
            {
                return new Compania(objWSCompania.SCodCompania, objWSCompania.STipCompania,
                    objWSCompania.SDscCompania, objWSCompania.DtFchCreacion, objWSCompania.STipEstado,
                    objWSCompania.SCodEspecialCompania, objWSCompania.SDscRuc, objWSCompania.SCodAerolinea,
                    objWSCompania.SCodSAP, objWSCompania.SCodOACI, objWSCompania.SCodIATA, objWSCompania.SLogUsuarioMod,
                    objWSCompania.SLogFechaMod, objWSCompania.SLogHoraMod);
            }
            else return null;
        }

        public override int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            return objWSAdministracion.validarDocumento(sNombre, sApellido, sTpDocumento, sNroDocumento);
        }


        #endregion

        #region Representante

        public override bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            wsAdministracion.RepresentantCia objWSRepresentantCia = new wsAdministracion.RepresentantCia();

            objWSRepresentantCia.SCodCompania = objRepresentante.SCodCompania;
            objWSRepresentantCia.INumSecuencial = objRepresentante.INumSecuencial;
            objWSRepresentantCia.SNomRepresentante = objRepresentante.SNomRepresentante;
            objWSRepresentantCia.SApeRepresentante = objRepresentante.SApeRepresentante;
            objWSRepresentantCia.SCargoRepresentante = objRepresentante.SCargoRepresentante;
            objWSRepresentantCia.STDocRepresentante = objRepresentante.STDocRepresentante;
            objWSRepresentantCia.SNDocRepresentante = objRepresentante.SNDocRepresentante;
            objWSRepresentantCia.SPermRepresentante = objRepresentante.SPermRepresentante;
            objWSRepresentantCia.SLogUsuarioMod = objRepresentante.SLogUsuarioMod;

            return objWSAdministracion.insertarRepresentante(objWSRepresentantCia);
        }

        public override bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            wsAdministracion.RepresentantCia objWSRepresentantCia = new wsAdministracion.RepresentantCia();
            objWSRepresentantCia.SCodCompania = objRepresentante.SCodCompania;
            objWSRepresentantCia.INumSecuencial = objRepresentante.INumSecuencial;
            objWSRepresentantCia.SNomRepresentante = objRepresentante.SNomRepresentante;
            objWSRepresentantCia.SApeRepresentante = objRepresentante.SApeRepresentante;
            objWSRepresentantCia.SCargoRepresentante = objRepresentante.SCargoRepresentante;
            objWSRepresentantCia.STDocRepresentante = objRepresentante.STDocRepresentante;
            objWSRepresentantCia.SNDocRepresentante = objRepresentante.SNDocRepresentante;
            objWSRepresentantCia.STipEstado = objRepresentante.STipEstado;
            objWSRepresentantCia.SPermRepresentante = objRepresentante.SPermRepresentante;
            objWSRepresentantCia.SLogUsuarioMod = objRepresentante.SLogUsuarioMod;

            return objWSAdministracion.actualizarRepresentante(objWSRepresentantCia);
        }

        #endregion

        #region Modalidad Venta - Compa�ia

        public override bool insertarModVentaComp(ModVentaComp objModComp)
        {
            wsAdministracion.ModVentaComp objWSModVentaComp = new wsAdministracion.ModVentaComp();

            objWSModVentaComp.SCodCompania = objModComp.SCodCompania;
            objWSModVentaComp.SCodModalidadVenta = objModComp.SCodModalidadVenta;
            objWSModVentaComp.SDscValorAcumulado = objModComp.SDscValorAcumulado;

            return objWSAdministracion.insertarModVentaComp(objWSModVentaComp);
        }


        public override bool insertarSecuenciaModVentaComp(string codCompania)
        {
            wsAdministracion.ModVentaComp objWSModVentaComp = new wsAdministracion.ModVentaComp();
            return objWSAdministracion.insertarSecuenciaModVentaComp(codCompania);
        }


        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            return objWSAdministracion.eliminarModVentaComp(sCodCompania, sCodModalidadVenta);
        }

        public override List<ModVentaComp> ListarModVentaCompxCompa�ia(string sCodCompania)
        {
            wsAdministracion.ModVentaComp[] lista = objWSAdministracion.ListarModVentaCompxCompa�ia(sCodCompania);
            List<ModVentaComp> listaModVentaComp = new List<ModVentaComp>();
            for (int i = 0; i < lista.Length; i++)
            {
                ModVentaComp objModVentaComp = new ModVentaComp();

                objModVentaComp.SCodCompania = lista[i].SCodCompania;
                objModVentaComp.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                objModVentaComp.SDscValorAcumulado = lista[i].SDscValorAcumulado;

                listaModVentaComp.Add(objModVentaComp);
            }

            return listaModVentaComp;
        }


        #endregion

        #region Modalidad Venta - Compa�ia - Atributos

        public override bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            wsAdministracion.ModVentaCompAtr objWSModVentaCompAtr = new wsAdministracion.ModVentaCompAtr();

            objWSModVentaCompAtr.SCodModalidadVenta = objRModCompAtr.SCodModalidadVenta;
            objWSModVentaCompAtr.SCodCompania = objRModCompAtr.SCodCompania;
            objWSModVentaCompAtr.SCodAtributo = objRModCompAtr.SCodAtributo;
            objWSModVentaCompAtr.SCodTipoTicket = objRModCompAtr.SCodTipoTicket;
            objWSModVentaCompAtr.SDscValor = objRModCompAtr.SDscValor;
            objWSModVentaCompAtr.SLogUsuarioMod = objRModCompAtr.SLogUsuarioMod;

            return objWSAdministracion.insertarModVentaCompAtr(objWSModVentaCompAtr);
        }

        public override bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {

            wsAdministracion.ModVentaCompAtr objWSModVentaCompAtr = new wsAdministracion.ModVentaCompAtr();

            objWSModVentaCompAtr.SCodModalidadVenta = objRModCompAtr.SCodModalidadVenta;
            objWSModVentaCompAtr.SCodCompania = objRModCompAtr.SCodCompania;
            objWSModVentaCompAtr.SCodAtributo = objRModCompAtr.SCodAtributo;
            objWSModVentaCompAtr.SCodTipoTicket = objRModCompAtr.SCodTipoTicket;
            objWSModVentaCompAtr.SDscValor = objRModCompAtr.SDscValor;
            objWSModVentaCompAtr.SLogUsuarioMod = objRModCompAtr.SLogUsuarioMod;

            return objWSAdministracion.actualizarModVentaCompAtr(objWSModVentaCompAtr);

        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            return objWSAdministracion.eliminarModVentaCompAtr(sCodCompania, sCodModalidadVenta, CodAtributo);

        }

        public override List<ModVentaCompAtr> ObtenerModVentaCompAtr(string Cod_Compania, string Cod_Modalidad_Venta)
        {
            wsAdministracion.ModVentaCompAtr[] lista = objWSAdministracion.ObtenerModVentaCompAtr(Cod_Compania, Cod_Modalidad_Venta);
            List<ModVentaCompAtr> listaModVentaCompAtr = new List<ModVentaCompAtr>();
            for (int i = 0; i < lista.Length; i++)
            {
                ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr();

                objModVentaCompAtr.SCodModalidadVenta = lista[i].SCodModalidadVenta;
                objModVentaCompAtr.SCodCompania = lista[i].SCodCompania;
                objModVentaCompAtr.SCodAtributo = lista[i].SCodAtributo;
                objModVentaCompAtr.SCodTipoTicket = lista[i].SCodTipoTicket;
                objModVentaCompAtr.SDscValor = lista[i].SDscValor;
                objModVentaCompAtr.SLogUsuarioMod = lista[i].SLogUsuarioMod;

                listaModVentaCompAtr.Add(objModVentaCompAtr);
            }

            return listaModVentaCompAtr;

        }

        public override int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            return objWSAdministracion.ValidarSerieTicketCompa(serieInicial, serieFinal, modalidad, compania);
        }


        public override int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            return objWSAdministracion.validarAnulacionModalidad(sModalidad, sCompania);
        }

        #endregion

        #region esilva
        // TasaCambio
        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            //Casting
            wsAdministracion.TasaCambio objWSTasaCambio = new wsAdministracion.TasaCambio();
            objWSTasaCambio.SCodTasaCambio = objTasaCambio.SCodTasaCambio;
            objWSTasaCambio.STipCambio = objTasaCambio.STipCambio;
            objWSTasaCambio.SCodMoneda = objTasaCambio.SCodMoneda;
            objWSTasaCambio.DImpCambioActual = objTasaCambio.DImpCambioActual;
            objWSTasaCambio.SLogUsuarioMod = objTasaCambio.SLogUsuarioMod;
            objWSTasaCambio.STipIngreso = objTasaCambio.STipIngreso;
            objWSTasaCambio.STipEstado = objTasaCambio.STipEstado;
            objWSTasaCambio.DtFchProgramacion = objTasaCambio.DtFchProgramacion;

            return objWSAdministracion.RegistrarTasaCambio(objWSTasaCambio);
        }
        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            return objWSAdministracion.EliminarTasaCambio(strCodTasaCambio);
        }
        public override DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            DataTable dtTasaCambio;
            dtTasaCambio = objWSAdministracion.ObtenerTasaCambio(strCodTasaCambio);
            return dtTasaCambio;
        }
        // TasaCambioHist
        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            //Casting
            wsAdministracion.TasaCambioHist objWSTasaCambioHist = new wsAdministracion.TasaCambioHist();
            objWSTasaCambioHist.SCodTasaCambio = objTasaCambioHist.SCodTasaCambio;
            objWSTasaCambioHist.STipCambio = objTasaCambioHist.STipCambio;
            objWSTasaCambioHist.SCodMoneda = objTasaCambioHist.SCodMoneda;
            objWSTasaCambioHist.DImpValor = objTasaCambioHist.DImpValor;
            objWSTasaCambioHist.SCodMoneda2 = objTasaCambioHist.SCodMoneda2;
            objWSTasaCambioHist.DImpValor2 = objTasaCambioHist.DImpValor2;
            objWSTasaCambioHist.DtFchIni = objTasaCambioHist.DtFchIni;
            objWSTasaCambioHist.DtFchFin = objTasaCambioHist.DtFchFin;
            objWSTasaCambioHist.SLogUsuarioMod = objTasaCambioHist.SLogUsuarioMod;

            return objWSAdministracion.RegistrarTasaCambioHist(objWSTasaCambioHist);
        }
        public override DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            DataTable dtTasaCambioHist;
            dtTasaCambioHist = objWSAdministracion.ObtenerTasaCambioHist(strCodTasaCambio);
            return dtTasaCambioHist;
        }
        // PrecioTicket
        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            //Casting
            wsAdministracion.PrecioTicket objWSPrecioTicket = new wsAdministracion.PrecioTicket();
            objWSPrecioTicket.SCodPrecioTicket = objPrecioTicket.SCodPrecioTicket;
            objWSPrecioTicket.SCodTipoTicket = objPrecioTicket.SCodTipoTicket;
            objWSPrecioTicket.SCodMoneda = objPrecioTicket.SCodMoneda;
            objWSPrecioTicket.DImpPrecio = objPrecioTicket.DImpPrecio;
            objWSPrecioTicket.SLogUsuarioMod = objPrecioTicket.SLogUsuarioMod;
            objWSPrecioTicket.STipIngreso = objPrecioTicket.STipIngreso;
            objWSPrecioTicket.STipEstado = objPrecioTicket.STipEstado;
            objWSPrecioTicket.DtFchProgramacion = objPrecioTicket.DtFchProgramacion;

            return objWSAdministracion.RegistrarPrecioTicket(objWSPrecioTicket);
        }
        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            return objWSAdministracion.EliminarPrecioTicket(strCodPrecioTicket);
        }
        public override DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            DataTable dtPrecioTicket;
            dtPrecioTicket = objWSAdministracion.ObtenerPrecioTicket(strCodPrecioTicket);
            return dtPrecioTicket;
        }
        // PrecioTicketHist
        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            //Casting
            wsAdministracion.PrecioTicketHist objWSPrecioTicketHist = new wsAdministracion.PrecioTicketHist();
            objWSPrecioTicketHist.SCodPrecioTicket = objPrecioTicketHist.SCodPrecioTicket;
            objWSPrecioTicketHist.SCodTipoTicket = objPrecioTicketHist.SCodTipoTicket;
            objWSPrecioTicketHist.SCodMoneda = objPrecioTicketHist.SCodMoneda;
            objWSPrecioTicketHist.DImpPrecio = objPrecioTicketHist.DImpPrecio;
            objWSPrecioTicketHist.SCodMoneda2 = objPrecioTicketHist.SCodMoneda2;
            objWSPrecioTicketHist.DImpPrecio2 = objPrecioTicketHist.DImpPrecio2;
            objWSPrecioTicketHist.DtFchIni = objPrecioTicketHist.DtFchIni;
            objWSPrecioTicketHist.DtFchFin = objPrecioTicketHist.DtFchFin;
            objWSPrecioTicketHist.SLogUsuarioMod = objPrecioTicketHist.SLogUsuarioMod;

            return objWSAdministracion.RegistrarPrecioTicketHist(objWSPrecioTicketHist);
        }
        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            return objWSAdministracion.EliminarPrecioTicketHist(strCodPrecioTicket);
        }
        public override DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            DataTable dtPrecioTicketHist;
            dtPrecioTicketHist = objWSAdministracion.ObtenerPrecioTicketHist(strCodPrecioTicket);
            return dtPrecioTicketHist;
        }
        #endregion



        public override Usuario autenticar(string sCuenta, string sClave)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool CambiarClave(Usuario objUsuario)
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

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
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

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
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

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
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

        public override List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<Moneda> ListarMonedasInter()
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

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
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

        public override DataTable ListarAllTurno(string as_codturno)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable CantidadMonedasTurno(string as_codturno)
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

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllUsuario()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarUsuarioxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarParametros(string as_identificador)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleBoardingEstHist_Arch(string Num_Secuencial_Bcbp)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListaCamposxNombre(string sNombre)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarRoles()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ListarAllParametroGenerales(string strParametro)
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

        public override bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int intTipo)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
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

        public override bool ActualizarTicket(List<Ticket> listaTickets)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno)
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

        public override ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            throw new NotImplementedException();
        }


        public override bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
        {
            throw new NotImplementedException();
        }


        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            throw new NotImplementedException();
        }

        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle)
        {
            throw new NotImplementedException();
        }

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
        {
            throw new NotImplementedException();
        }

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario)
        {
            throw new NotImplementedException();
        }


        public override bool actualizarMolinete(Molinete objMolinete)
        {
            throw new NotImplementedException();
        }


        public override DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            throw new NotImplementedException();
        }
        public override DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo)
        {
            throw new NotImplementedException();
        }

        public override bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput)
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

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            throw new NotImplementedException();
        }

        public override bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
        {
            throw new NotImplementedException();
        }

        public override bool AnularBCBP(BoardingBcbp objBoardingBcbp)
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

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
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
        public override DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            throw new NotImplementedException();
        }

        public override DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento, string strTipVenta, string strFlgAero)
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

        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
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

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
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
        public override DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta)
        {
            throw new NotImplementedException();
        }
        public override bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion)
        {
            throw new NotImplementedException();
        }

        public override bool AnularTuua(string strListaTicket, int intTicket)
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado)
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

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
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

        public override string ObtenerFechaActual()
        {
            throw new NotImplementedException();
        }

        public override string obtenerFechaEstadistico(string sEstadistico)
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

        public override DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
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

        public override bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
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





        public override DataTable ListarFiltroSincronizacion(string as_molinete, string as_estado, string as_TipoSincronizacion, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta, string sfiltro, string sOrdenacion)
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

        public override DataTable ListarAllMolinetes()
        {
            throw new NotImplementedException();
        }

        public override DataTable obtenerMolinete(string strMolinete)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarUnMolinete(Molinete objMolinete)
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

