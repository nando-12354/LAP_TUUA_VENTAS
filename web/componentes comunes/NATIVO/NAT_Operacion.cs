using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Data;
using LAP.TUUA.CONEXION;

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

        public override TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            try
            {
                DAO_TipoTicket objDAOTipoTicket = new DAO_TipoTicket(Dsc_PathSPConfig);
                return objDAOTipoTicket.validarTipoTicket(strTipoVuelo, strTipoPas, strTipoTrans);
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

        public override DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            try
            {
                DAO_VueloProgramado objDAOVueloProg = new DAO_VueloProgramado(Dsc_PathSPConfig);
                return objDAOVueloProg.ListarVuelosxCompania(strCompania, strFecha,null);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets)
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
                strListaTickets = listaTickets[0].SCodNumeroTicket;
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
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarTurnosAbiertos()
        {
            try
            {
                DAO_Turno objDAOTurno = new DAO_Turno(Dsc_PathSPConfig);
                return objDAOTurno.ListarTurnosAbiertos();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool ActualizarTicket(List<Ticket> listaTickets)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                for (int i = 0; i < listaTickets.Count; i++)
                {
                    if (!objDAOTicket.actualizar(listaTickets[i]))
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

        public override bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage)
        {
            try
            {
                DAO_LogOperacion objDAOLogOpera = new DAO_LogOperacion(Dsc_PathSPConfig);
                objDAOLogOpera.Cod_Modulo = Cod_Modulo;
                objDAOLogOpera.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOLogOpera.Cod_Usuario = Cod_Usuario;
                if (!objDAOLogOpera.ExtornarCompraVenta(strCodOpera, strTurno, intCantidad, ref strMessage))
                {
                    return false;
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

        public override bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo, ref string strMessage)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                if (!objDAOTicket.ExtornarTicket(strListaTickets, strTurno, intCantidad, strUsuario, strMotivo, ref strMessage))
                {
                    return false;
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

        public override bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion)
        {
            try
            {
                string strNumTicket = "";
                int intLongTicket = strListaTickets.Length / intCantidad;
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                for (int i = 0; i < intCantidad; i++)
                {
                    strNumTicket = strListaTickets.Substring(i * intLongTicket, intLongTicket);
                    Ticket objTicket = new Ticket(strNumTicket, null, null, null, null, strEstado, null, null, strUsuario, 0, null, null, null, 0, null);

                    if (!transaccion)
                    {
                        if (!objDAOTicket.actualizarTransaccion(objTicket, false))
                            return false;
                    }
                    else
                    {
                        if (!objDAOTicket.actualizar(objTicket))
                        {
                            return false;
                        }
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

        public override bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strMessage)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;

                if (!objDAOTicket.Extender(strListaTickets, strListaFechas,strListaFechas.Trim().Length/8,ref strMessage))
                {
                   return false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
            return true;
        }

        public override bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod)
        {
            try
            {
                DAO_TicketEstHist objDAO_TicketEstHist = new DAO_TicketEstHist(Dsc_PathSPConfig);
                objDAO_TicketEstHist.Cod_Modulo = Cod_Modulo;
                objDAO_TicketEstHist.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAO_TicketEstHist.Cod_Usuario = Cod_Usuario;

                if (!objDAO_TicketEstHist.AnularTicket(sCodNumeroTicket, sDscMotivo, sUsuarioMod))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(Dsc_PathSPConfig);
                return objDAOBoardingBcbp.actualizarEstado(objBoardingBcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool AnularBCBP(BoardingBcbp objBoardingBcbp)
        {
            try
            {
                DAO_BoardingBcbp objDAOBoardingBcbp = new DAO_BoardingBcbp(Dsc_PathSPConfig);
                objDAOBoardingBcbp.Cod_Modulo = Cod_Modulo;
                objDAOBoardingBcbp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOBoardingBcbp.Cod_Usuario = Cod_Usuario;
                return objDAOBoardingBcbp.AnularBCBP(objBoardingBcbp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, 
                                             string strUsuario, decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, 
                                             string strTipVuelo, int intTickets, string strFlagCont, string strNumRef, string strTipPago, string strEmpresa,
                                             string strRepte, ref string strFecVence, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTicket.insertar(strCompania, strVentaMasiva, strNumVuelo, strFecVuelo, strTurno, strUsuario, decPrecio, strMoneda, strModVenta, 
                                             strTipTicket, strTipVuelo, intTickets, strFlagCont, strNumRef, strTipPago, strEmpresa, strRepte, ref  strFecVence,
                                             ref strListaTickets, strCodTurnoIng, strFlgCierreTurno, strCodPrecio);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                return objDAOTicket.ObtenerVentaTicket(strFecIni, strFecFin, strTipVenta, strFlgAero);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento,string strTipVenta, string strFlgAero)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                return objDAOTicket.ObtenerComprobanteSEAE(sAnio, sMes,sTDocumento, strTipVenta, strFlgAero);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTicket.RegistrarVentaContingencia(strCompania, strNumVuelo,  strUsuario,  strMoneda,  strTipTicket,  strFecVenta,  intTickets,  strListaTickets, strCodTurno, strFlagTurno, ref strCodTurnoCreado, ref strCodError);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                objDAOTicket.Cod_Modulo = Cod_Modulo;
                objDAOTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTicket.ListarContingencia(strTipTikcet,strFlgConti, strNumIni,  strNumFin,  strUsuario);
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

        #region Molinete

        public override System.Data.DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            try
            {
                DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_PathSPConfig);
                return objDAOMolinete.listarAllMolinete(strCodMolinete, strDscIp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool actualizarMolinete(Molinete objMolinete)
        {
            try
            {
                DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_PathSPConfig);
                objDAOMolinete.Cod_Modulo = Cod_Modulo;
                objDAOMolinete.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOMolinete.Cod_Usuario = Cod_Usuario;
                return objDAOMolinete.actualizar(objMolinete);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool actualizarUnMolinete(Molinete objMolinete)
        {
            try
            {
                DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_PathSPConfig);
                objDAOMolinete.Cod_Modulo = Cod_Modulo;
                objDAOMolinete.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOMolinete.Cod_Usuario = Cod_Usuario;
                return objDAOMolinete.actualizarUnMolinete(objMolinete);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable ListarAllMolinetes()
        {
            try
            {
                DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_PathSPConfig);
                return objDAOMolinete.listarAllMolinete();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override System.Data.DataTable obtenerMolinete(String strMolinete)
        {
            try
            {
                DAO_Molinete objDAOMolinete = new DAO_Molinete(Dsc_PathSPConfig);
                return objDAOMolinete.obtenerMolinete(strMolinete);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        public override string ObtenerFechaActual()
        {
            try
            {
                DAO_Ticket objDAOTicket = new DAO_Ticket(Dsc_PathSPConfig);
                return objDAOTicket.ObtenerFechaActual();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override string obtenerFechaEstadistico(string sEstadistico)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
        {
            try
            {
                DAO_BoardingBcbp objDAOBcbp = new DAO_BoardingBcbp(Dsc_PathSPConfig);
                return objDAOBcbp.ListarBcbpxConciliar(sCodCompania, strFchVuelo, strNumVuelo, strNumAsiento, strPasajero, strFecUsoIni, strFecUsoFin, strFlg);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
        {
            try
            {
                DAO_BoardingBcbp objDAOBcbp = new DAO_BoardingBcbp(Dsc_PathSPConfig);
                objDAOBcbp.Cod_Modulo = Cod_Modulo;
                objDAOBcbp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOBcbp.Cod_Usuario = Cod_Usuario;
                return objDAOBcbp.RegistrarBcbpxConciliar(sBcbpBase, sBcbpUlt, sBcbpAsoc);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            try
            {
                DAO_Turno objDAOTurno = new DAO_Turno(Dsc_PathSPConfig);
                return objDAOTurno.ObtenerDetalleTurnoActual (strCodUsuario, ref strCantTickets, ref strCodTurno, ref strFecHorTurno);
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

        public override DataTable ListarUsuarioxRol(string sCodRol)
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

        public override DataTable listarAllCompania()
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

        public override bool actualizarContraseñaUsuario(string sCodUsuario, string sContraseña, string SLogUsuarioMod, DateTime DtFchVigencia)
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

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
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

        public override DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle)
        {
            throw new NotImplementedException();
        }       

        public override DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion)
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

        public override bool insertarCompania(Compania objCompania)
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

        public override List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
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

        public override Compania obtenerCompañiaxcodigo(string sCodigoCompania)
        {
            throw new NotImplementedException();
        }

        public override Compania obtenerCompañiaxnombre(string sNombreCompania)
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

        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
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

        public override DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
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

        public override DataTable ListaCamposxNombreOrderByDesc(string as_nombre)
        {
            throw new NotImplementedException();
        }
        public override DataTable DetalleBoarding_REH(string as_Cod_Compania, string as_Num_Vuelo, string as_Fech_Vuelo, string as_Num_Asiento, string as_Pasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
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

        public override bool actualizarHoras(ListaDeCampo objLDeCampos)
        {
            throw new NotImplementedException();
        }

        public override bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            throw new NotImplementedException();
        }

        public override DataTable ListarFiltroDepuracion(string as_molinete, string as_estado, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde, string strHraHasta,  string sfiltro, string sOrdenacion)
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
