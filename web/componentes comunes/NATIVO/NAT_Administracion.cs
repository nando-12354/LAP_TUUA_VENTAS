using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONEXION;

namespace LAP.TUUA.NATIVO 
{
    public class NAT_Administracion : NAT_Conexion
    {

        protected string Dsc_PathSPConfig;
        DAO_Moneda objDAOMoneda;
        DAO_TipoTicket objDAOTipoTicket;
        DAO_EstacionPtoVta objDAOEstacionPtoVta;
        DAO_ListaDeCampos objDAOListaDeCampos;
        DAO_ModalidadVenta objDAOModVta;
        DAO_TasaCambio objDAOTasaCambio;
        DAO_TasaCambioHist objDAOTasaCambioHist;
        DAO_PrecioTicket objDAOPrecioTicket;
        DAO_PrecioTicketHist objDAOPrecioTicketHist;
        DAO_ParameGeneral objDAOParameGeneral;
        DAO_ModalidadAtrib objDAOModalidadAtrib;
        DAO_Compania objDAOCompania;
        DAO_Representante objDAORepresentante;
        DAO_ModVentaComp objDAO_ModVentaComp;
        DAO_ModVentaCompAtr objDAOModVentaCompAtr;
        DAO_TemporalBoardingPass objDAOTemporalBoardingPass;
        DAO_TemporalTicket objDAOTemporalTicket;


        public NAT_Administracion()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOMoneda = new DAO_Moneda(Dsc_PathSPConfig);
            objDAOTipoTicket = new DAO_TipoTicket(Dsc_PathSPConfig);
            objDAOEstacionPtoVta = new DAO_EstacionPtoVta(Dsc_PathSPConfig);
            objDAOListaDeCampos = new DAO_ListaDeCampos(Dsc_PathSPConfig);
            objDAOModVta = new DAO_ModalidadVenta(Dsc_PathSPConfig);
            objDAOTasaCambio = new DAO_TasaCambio(Dsc_PathSPConfig);
            objDAOTasaCambioHist = new DAO_TasaCambioHist(Dsc_PathSPConfig);
            objDAOPrecioTicket = new DAO_PrecioTicket(Dsc_PathSPConfig);
            objDAOPrecioTicketHist = new DAO_PrecioTicketHist(Dsc_PathSPConfig);
            objDAOParameGeneral = new DAO_ParameGeneral(Dsc_PathSPConfig);
            objDAOModalidadAtrib = new DAO_ModalidadAtrib(Dsc_PathSPConfig);
            objDAOCompania = new DAO_Compania(Dsc_PathSPConfig);
            objDAORepresentante = new DAO_Representante(Dsc_PathSPConfig);
            objDAO_ModVentaComp = new DAO_ModVentaComp(Dsc_PathSPConfig);
            objDAOModVentaCompAtr = new DAO_ModVentaCompAtr(Dsc_PathSPConfig);
            objDAOTemporalBoardingPass = new DAO_TemporalBoardingPass(Dsc_PathSPConfig);
            objDAOTemporalTicket = new DAO_TemporalTicket(Dsc_PathSPConfig);
        }

        #region Moneda

        public override DataTable listarAllMonedas()
        {
            try
            {
                return objDAOMoneda.listarAllMonedas();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }



        }


        public override DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            try
            {
                return objDAOMoneda.obtenerDetalleMoneda(sCodMoneda);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }


        public override bool registrarTipoMoneda(Moneda objMoneda)
        {
            try
            {
                objDAOMoneda.Cod_Modulo = Cod_Modulo;
                objDAOMoneda.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOMoneda.Cod_Usuario = Cod_Usuario;
                return objDAOMoneda.insertar(objMoneda);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


        public override bool actualizarTipoMoneda(Moneda objMoneda)
        {
            try
            {
                objDAOMoneda.Cod_Modulo = Cod_Modulo;
                objDAOMoneda.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOMoneda.Cod_Usuario = Cod_Usuario;
                return objDAOMoneda.actualizar(objMoneda);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }


        public override bool eliminarTipoMoneda(string sCodMoneda)
        {
            try
            {
                objDAOMoneda.Cod_Modulo = Cod_Modulo;
                objDAOMoneda.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOMoneda.Cod_Usuario = Cod_Usuario;
                return objDAOMoneda.eliminar(sCodMoneda);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion

        #region TipoTicket


        public override bool registrarTipoTicket(TipoTicket objTipoTicket)
        {
            try
            {
                objDAOTipoTicket.Cod_Modulo = Cod_Modulo;
                objDAOTipoTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTipoTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTipoTicket.insertar(objTipoTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }


        public override bool actualizarTipoTicket(TipoTicket objTipoTicket)
        {
            try
            {
                objDAOTipoTicket.Cod_Modulo = Cod_Modulo;
                objDAOTipoTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTipoTicket.Cod_Usuario = Cod_Usuario;
                return objDAOTipoTicket.actualizar(objTipoTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }


        }



        public override DataTable ListaTipoTicket()
        {
            try
            {
                return objDAOTipoTicket.listarAll();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            try
            {
                return objDAOTipoTicket.obtener(sCodTipoTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Punto de Venta


        public override DataTable listarAllPuntoVenta()
        {
            try
            {
                return objDAOEstacionPtoVta.listarAllPuntoVenta();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override DataTable obtenerDetallePuntoVenta(string sCodEquipo, string strIP)
        {
            try
            {

                return objDAOEstacionPtoVta.obtenerDetallePuntoVenta(sCodEquipo, strIP);

            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            try
            {
                objDAOEstacionPtoVta.Cod_Modulo = Cod_Modulo;
                objDAOEstacionPtoVta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOEstacionPtoVta.Cod_Usuario = Cod_Usuario;
                return objDAOEstacionPtoVta.insertar(objPuntoVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            try
            {
                objDAOEstacionPtoVta.Cod_Modulo = Cod_Modulo;
                objDAOEstacionPtoVta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOEstacionPtoVta.Cod_Usuario = Cod_Usuario;
                return objDAOEstacionPtoVta.actualizar(objPuntoVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            try
            {
                objDAOEstacionPtoVta.Cod_Modulo = Cod_Modulo;
                objDAOEstacionPtoVta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOEstacionPtoVta.Cod_Usuario = Cod_Usuario;
                return objDAOEstacionPtoVta.eliminar(sCodEquipo, sLogUsuarioMod);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Lista de Campo


        public override List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            try
            {
                return objDAOListaDeCampos.obtenerListadeCampo(sNomCampo);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        #endregion

        #region Modalidad de Venta


        public override bool insertarModalidadVenta(ModalidadVenta objModalidad)
        {
            try
            {
                objDAOModVta.Cod_Modulo = Cod_Modulo;
                objDAOModVta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModVta.Cod_Usuario = Cod_Usuario;
                return objDAOModVta.insertar(objModalidad);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool actualizarModalidadVenta(ModalidadVenta objModalidad)
        {
            try
            {
                objDAOModVta.Cod_Modulo = Cod_Modulo;
                objDAOModVta.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModVta.Cod_Usuario = Cod_Usuario;
                return objDAOModVta.actualizar(objModalidad);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidad)
        {
            try
            {
                return objDAOModVta.obtenerxCodigo(sCodModalidad);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidad)
        {
            try
            {
                return objDAOModVta.obtenerxNombre(sNomModalidad);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override DataTable ListarAllModalidadVenta()
        {
            try
            {
                return objDAOModVta.ListarAllModalidadVenta();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override List<ModalidadVenta> listarModalidadVenta()
        {
            try
            {
                return objDAOModVta.listar();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }



        #endregion

        #region Atributos Modalidad de Venta

        public override bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            try
            {
                objDAOModalidadAtrib.Cod_Modulo = Cod_Modulo;
                objDAOModalidadAtrib.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModalidadAtrib.Cod_Usuario = Cod_Usuario;
                return objDAOModalidadAtrib.insertar(objModalidadAtrib);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        public override bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            try
            {
                objDAOModalidadAtrib.Cod_Modulo = Cod_Modulo;
                objDAOModalidadAtrib.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModalidadAtrib.Cod_Usuario = Cod_Usuario;
                return objDAOModalidadAtrib.actualizar(objModalidadAtrib);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            try
            {
                objDAOModalidadAtrib.Cod_Modulo = Cod_Modulo;
                objDAOModalidadAtrib.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModalidadAtrib.Cod_Usuario = Cod_Usuario;
                return objDAOModalidadAtrib.eliminar(sCodModalidadVenta, sCodAtributo, sCodTipoTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            try
            {
                return objDAOModalidadAtrib.ListarAtributosxModVentaTipoTicket(strCodModVenta, strTipoTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            try
            {
                return objDAOModalidadAtrib.validarSerieTicket(serieInicial, serieFinal, modalidad);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            try
            {
                return objDAO_ModVentaComp.validarAnulacionModalidad(sModalidad, sCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }


        #endregion

        #region Parametros Generales

        public override List<ParameGeneral> listarAtributosGenerales()
        {
            try
            {
                return objDAOParameGeneral.listarAtributosGenerales();
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Compañia

        public override bool insertarCompania(Compania objCompania)
        {
            try
            {
                objDAOCompania.Cod_Modulo = Cod_Modulo;
                objDAOCompania.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOCompania.Cod_Usuario = Cod_Usuario;
                return objDAOCompania.insertar(objCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool actualizarCompania(Compania objCompania)
        {
            try
            {
                objDAOCompania.Cod_Modulo = Cod_Modulo;
                objDAOCompania.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOCompania.Cod_Usuario = Cod_Usuario;
                return objDAOCompania.actualizar(objCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override Compania obtenerCompañiaxcodigo(string sCodigoCompania)
        {
            try
            {
                return objDAOCompania.obtenerxcodigo(sCodigoCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override Compania obtenerCompañiaxnombre(string sNombreCompania)
        {
            try
            {
                return objDAOCompania.obtenerxnombre(sNombreCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            try
            {
                return objDAORepresentante.ValidarDocumento(sNombre, sApellido, sTpDocumento, sNroDocumento);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        #endregion

        #region Representante

        public override bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            try
            {
                objDAORepresentante.Cod_Modulo = Cod_Modulo;
                objDAORepresentante.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAORepresentante.Cod_Usuario = Cod_Usuario;
                return objDAORepresentante.insertar(objRepresentante);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            try
            {
                objDAORepresentante.Cod_Modulo = Cod_Modulo;
                objDAORepresentante.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAORepresentante.Cod_Usuario = Cod_Usuario;
                return objDAORepresentante.actualizar(objRepresentante);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Modalidad Venta - Compañia

        public override bool insertarModVentaComp(ModVentaComp objModComp)
        {
            try
            {
                objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                return objDAO_ModVentaComp.insertar(objModComp);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool insertarSecuenciaModVentaComp(string codCompania)
        {
            try
            {
                objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                return objDAO_ModVentaComp.insertarSecuencia(codCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            try
            {
                objDAO_ModVentaComp.Cod_Modulo = Cod_Modulo;
                objDAO_ModVentaComp.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAO_ModVentaComp.Cod_Usuario = Cod_Usuario;
                return objDAO_ModVentaComp.eliminar(sCodCompania, sCodModalidadVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
        {
            try
            {
                return objDAO_ModVentaComp.ListarModVentaCompxCompañia(sCodCompania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region Modalidad Venta - Compañia - Atributos

        public override bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                return objDAOModVentaCompAtr.insertar(objRModCompAtr);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        public override bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            try
            {
                objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                return objDAOModVentaCompAtr.actualizar(objRModCompAtr);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            try
            {
                objDAOModVentaCompAtr.Cod_Modulo = Cod_Modulo;
                objDAOModVentaCompAtr.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOModVentaCompAtr.Cod_Usuario = Cod_Usuario;
                return objDAOModVentaCompAtr.eliminar(sCodCompania, sCodModalidadVenta, CodAtributo);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            try
            {
                return objDAOModVentaCompAtr.ObtenerModVentaCompAtr(sCodCompania, sCodModalidadVenta);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }

        }

        public override int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            try
            {
                return objDAOModVentaCompAtr.validarSerieTicketCompa(serieInicial, serieFinal, modalidad, compania);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }

        #endregion

        #region esilva
        // TasaCambio
        public override bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            try
            {
                objDAOTasaCambio.Cod_Modulo = Cod_Modulo;
                objDAOTasaCambio.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTasaCambio.Cod_Usuario = Cod_Usuario;
                return objDAOTasaCambio.insertar(objTasaCambio);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override bool EliminarTasaCambio(string strCodTasaCambio)
        {
            try
            {
                objDAOTasaCambio.Cod_Modulo = Cod_Modulo;
                objDAOTasaCambio.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTasaCambio.Cod_Usuario = Cod_Usuario;
                return objDAOTasaCambio.eliminar(strCodTasaCambio);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            try
            {
                return objDAOTasaCambio.obtener(strCodTasaCambio);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        // TasaCambioHist
        public override bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            try
            {
                objDAOTasaCambioHist.Cod_Modulo = Cod_Modulo;
                objDAOTasaCambioHist.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOTasaCambioHist.Cod_Usuario = Cod_Usuario;
                return objDAOTasaCambioHist.insertar(objTasaCambioHist);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            try
            {
                return objDAOTasaCambioHist.obtener(strCodTasaCambio);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        // PrecioTicket
        public override bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            try
            {
                DAO_PrecioTicket objDAOPrcTicket = new DAO_PrecioTicket(Dsc_PathSPConfig);
                objDAOPrcTicket.Cod_Modulo = Cod_Modulo;
                objDAOPrcTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrcTicket.Cod_Usuario = Cod_Usuario;
                return objDAOPrcTicket.insertar(objPrecioTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            try
            {
                objDAOPrecioTicket.Cod_Modulo = Cod_Modulo;
                objDAOPrecioTicket.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrecioTicket.Cod_Usuario = Cod_Usuario;
                return objDAOPrecioTicket.eliminar(strCodPrecioTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            try
            {
                return objDAOPrecioTicket.obtener(strCodPrecioTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        // PrecioTicketHist
        public override bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            try
            {
                objDAOPrecioTicketHist.Cod_Modulo = Cod_Modulo;
                objDAOPrecioTicketHist.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrecioTicketHist.Cod_Usuario = Cod_Usuario;
                return objDAOPrecioTicketHist.insertar(objPrecioTicketHist);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            try
            {
                objDAOPrecioTicketHist.Cod_Modulo = Cod_Modulo;
                objDAOPrecioTicketHist.Cod_Sub_Modulo = Cod_Sub_Modulo;
                objDAOPrecioTicketHist.Cod_Usuario = Cod_Usuario;
                return objDAOPrecioTicketHist.eliminar(strCodPrecioTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
        public override DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            try
            {
                return objDAOPrecioTicketHist.obtener(strCodPrecioTicket);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
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

        public override List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<PerfilRol> ListadoPerfilRolxRol(string sCodRol)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion)
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

        public override DataTable ListarAllParametroGenerales(string strParametro)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override DataTable DetalleParametroGeneralxId(string sIdentificador)
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

        public override Usuario obtenerUsuarioxCuenta(string sCuentaUsuario)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave, int iNum)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override string FlagPerfilRolxOpcion(string sCodUsuario, string sCodRol, string sDscArchivo, string sOpcion)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override List<ArbolModulo> ListarPerfilVenta(string strUsuario)
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

        public override DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen)
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

        public override DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario)
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
