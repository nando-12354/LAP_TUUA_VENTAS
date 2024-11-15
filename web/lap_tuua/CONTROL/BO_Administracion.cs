/*
Sistema		 :   TUUA
Aplicación	 :   Administracion
Objetivo		 :   Proceso de gestión de Administración.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Data;

namespace LAP.TUUA.CONTROL
{
    public class BO_Administracion
    {

        private Conexion objCnxAdministracion;
        private IAdministracion oAdministracion;
        private IAuditoria oAuditoria;
        protected BO_Operacion objBOOperacion;
        protected BO_Consultas objBOConsultas;

        public BO_Administracion()
        {
            objCnxAdministracion = Resolver.ObtenerConexion(Define.CNX_06);
            objBOOperacion = new BO_Operacion();
            objBOConsultas = new BO_Consultas();
        }

        public BO_Administracion(string keyClass)
        {
            objCnxAdministracion = Resolver.ObtenerConexion(Define.CNX_06);
            objBOOperacion = new BO_Operacion();
            objBOConsultas = new BO_Consultas();
            oAdministracion = (IAdministracion)Resolver.ObtenerConexionObject(keyClass);
            oAuditoria = (IAuditoria)Resolver.ObtenerConexionObject(Define.CNX_11);
        }

        public BO_Administracion(string strUsuario, string strModulo, string strSubModulo)
        {
            objCnxAdministracion = Resolver.ObtenerConexion(Define.CNX_06);
            objCnxAdministracion.Cod_Usuario = strUsuario;
            objCnxAdministracion.Cod_Modulo = strModulo;
            objCnxAdministracion.Cod_Sub_Modulo = strSubModulo;
            objBOOperacion = new BO_Operacion();
            objBOConsultas = new BO_Consultas();
        }

        public BO_Administracion(string strUsuario, string strModulo, string strSubModulo, string keyClass)
        {
            objCnxAdministracion = Resolver.ObtenerConexion(Define.CNX_06);
            objCnxAdministracion.Cod_Usuario = strUsuario;
            objCnxAdministracion.Cod_Modulo = strModulo;
            objCnxAdministracion.Cod_Sub_Modulo = strSubModulo;
            objBOOperacion = new BO_Operacion();
            objBOConsultas = new BO_Consultas();
            oAdministracion = (IAdministracion)Resolver.ObtenerConexionObject(keyClass);
            oAdministracion.Cod_Usuario = strUsuario;
            oAdministracion.Cod_Modulo = strModulo;
            oAdministracion.Cod_Sub_Modulo = strSubModulo;
            oAuditoria = (IAuditoria)Resolver.ObtenerConexionObject(Define.CNX_11);
        }

        #region Moneda

        public DataTable listarAllMonedas()
        {
            DataTable DTListaAllMonedas;

            DTListaAllMonedas = objCnxAdministracion.listarAllMonedas();

            return DTListaAllMonedas;
        }

        public DataTable obtenerDetalleMoneda(string sCodMoneda)
        {
            DataTable DTDetalleMoneda;

            DTDetalleMoneda = objCnxAdministracion.obtenerDetalleMoneda(sCodMoneda);

            return DTDetalleMoneda;

        }


        public bool registrarTipoMoneda(Moneda objTipoMoneda)
        {
            return objCnxAdministracion.registrarTipoMoneda(objTipoMoneda);
        }


        public bool actualizarTipoMoneda(Moneda objTipoMoneda)
        {
            return objCnxAdministracion.actualizarTipoMoneda(objTipoMoneda);
        }


        public bool eliminarTipoMoneda(string sCodMoneda)
        {
            return objCnxAdministracion.eliminarTipoMoneda(sCodMoneda);
        }

        #endregion

        #region Punto de Venta

        public DataTable listarAllPuntoVenta()
        {
            DataTable DTListaAllPuntoVenta;

            DTListaAllPuntoVenta = objCnxAdministracion.listarAllPuntoVenta();

            return DTListaAllPuntoVenta;

        }


        public DataTable obtenerDetallePuntoVenta(string sCodEquipo, string sDireccionIP)
        {
            DataTable DTDetallePuntoVenta;

            DTDetallePuntoVenta = objCnxAdministracion.obtenerDetallePuntoVenta(sCodEquipo, sDireccionIP);


            return DTDetallePuntoVenta;


        }


        public bool registrarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            return objCnxAdministracion.registrarPuntoVenta(objPuntoVenta);
        }



        public bool actualizarPuntoVenta(EstacionPtoVta objPuntoVenta)
        {
            return objCnxAdministracion.actualizarPuntoVenta(objPuntoVenta);
        }



        public bool eliminarPuntoVenta(string sCodEquipo, string sLogUsuarioMod)
        {
            return objCnxAdministracion.eliminarPuntoVenta(sCodEquipo, sLogUsuarioMod);
        }

        #endregion

        #region Tipo de Ticket

        public DataTable ListaTipoTicket()
        {
            DataTable DTTipoTicket;
            DTTipoTicket = objCnxAdministracion.ListaTipoTicket();
            return DTTipoTicket;
        }

        public bool registrarTipoTicket(TipoTicket objTipoTicket)
        {
            return objCnxAdministracion.registrarTipoTicket(objTipoTicket);

        }

        public bool actualizarTipoTicket(TipoTicket objTipoTicket)
        {
            return objCnxAdministracion.actualizarTipoTicket(objTipoTicket);
        }


        public TipoTicket obtenerTipoTicket(string sCodTipoTicket)
        {
            return objCnxAdministracion.obtenerTipoTicket(sCodTipoTicket);
        }

        public bool ValidarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            strTipoVuelo = strTipoVuelo + "#MANT";
            TipoTicket objTipoTicket = objBOOperacion.ValidarTipoTicket(strTipoVuelo, strTipoPas, strTipoTrans);

            if (objTipoTicket != null)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region Lista de Campos

        public List<ListaDeCampo> obtenerListadeCampo(string sNomCampo)
        {
            return objCnxAdministracion.obtenerListadeCampo(sNomCampo);

        }

        #endregion

        #region Modalidad de Venta

        public bool insertarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            return objCnxAdministracion.insertarModalidadVenta(objModalidadVenta);
        }

        public bool actualizarModalidadVenta(ModalidadVenta objModalidadVenta)
        {
            return objCnxAdministracion.actualizarModalidadVenta(objModalidadVenta);
        }

        public ModalidadVenta obtenerModalidadVentaxCodigo(string sCodModalidadVenta)
        {
            return objCnxAdministracion.obtenerModalidadVentaxCodigo(sCodModalidadVenta);
        }

        public ModalidadVenta obtenerModalidadVentaxNombre(string sNomModalidadVenta)
        {
            return objCnxAdministracion.obtenerModalidadVentaxNombre(sNomModalidadVenta);
        }

        public DataTable ListarAllModalidadVenta()
        {
            return objCnxAdministracion.ListarAllModalidadVenta();
        }

        public List<ModalidadVenta> listarModalidadVenta()
        {
            return objCnxAdministracion.listarModalidadVenta();
        }

        public int validarAnulacionModalidad(string sModalidad, string sCompania)
        {
            return objCnxAdministracion.validarAnulacionModalidad (sModalidad, sCompania);
        }

        #endregion

        #region Atributos Modalidad de Venta

        public bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            return objCnxAdministracion.insertarModVentaAtributo(objModalidadAtrib);
        }


        public bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib)
        {
            return objCnxAdministracion.actualizarModVentaAtributo(objModalidadAtrib);
        }

        public bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket)
        {
            return objCnxAdministracion.eliminarModVentaAtributo(sCodModalidadVenta, sCodAtributo, sCodTipoTicket);
        }

        public List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket)
        {
            return objCnxAdministracion.ListarAtributosxModVentaTipoTicket(strCodModVenta, strTipoTicket);
        }

        public int validarSerieTicket(int serieInicial, int serieFinal, string modalidad)
        {
            return objCnxAdministracion.validarSerieTicket(serieInicial, serieFinal, modalidad); 
        }

        #endregion

        #region Parametros Generales

        public List<ParameGeneral> listarAtributosGenerales()
        {
            return objCnxAdministracion.listarAtributosGenerales();
        }

        #endregion

        #region Compañia

        public  bool insertarCompania(Compania objCompania)
        {
            return objCnxAdministracion.insertarCompania(objCompania);
        }

        public bool insertarCompaniaCrit(Compania objCompania)
        {
            return (bool)oAdministracion.Registrar(objCompania, "Compania");
        }

        public bool actualizarCompania(Compania objCompania)
        {
            bool boRetorno=objCnxAdministracion.actualizarCompania(objCompania);
            if(objCompania.SLogFechaMod.Trim()!="")
            {
                return false;
            }
            return boRetorno;
        }

        public bool actualizarCompaniaCrit(Compania objCompania)
        {
            bool boRetorno = (bool)oAdministracion.Actualizar(objCompania, "Compania");
            if (objCompania.SLogFechaMod.Trim() != "")
            {
                return false;
            }
            return boRetorno;
        }

        public bool validarRucCompañia(string SRuc)
        {
            DataTable dtListaCompañia = new DataTable();
            dtListaCompañia = objBOConsultas.listarAllCompania();

            DataRow[] foundRowCompania = dtListaCompañia.Select("Dsc_Ruc = '" + SRuc + "'");

            if (foundRowCompania != null && foundRowCompania.Length > 0)
            {
                return false;
            }
            return true; 
        }


        public Compania obtenerCompañiaxcodigo(string sCodigoCompania)
        {
            return objCnxAdministracion.obtenerCompañiaxcodigo(sCodigoCompania);
        }

        public Compania obtenerCompañiaxnombre(string sNombreCompania)
        {
            return objCnxAdministracion.obtenerCompañiaxnombre(sNombreCompania);
        }

        public int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento)
        {
            return objCnxAdministracion.validarDocumento(sNombre, sApellido, sTpDocumento, sNroDocumento);
        }



        #endregion

        #region Representante

        public  bool insertarRepresentante(RepresentantCia objRepresentante)
        {
            return objCnxAdministracion.insertarRepresentante(objRepresentante);
        }

        public bool insertarRepresentanteCrit(RepresentantCia objRepresentante)
        {
            return (bool)oAdministracion.Registrar(objRepresentante, "RepresentantCia");
        }

        public  bool actualizarRepresentante(RepresentantCia objRepresentante)
        {
            return objCnxAdministracion.actualizarRepresentante(objRepresentante);
        }

        public bool actualizarRepresentanteCrit(RepresentantCia objRepresentante)
        {
            return (bool)oAdministracion.Actualizar(objRepresentante, "RepresentantCia");
        }

        #endregion

        #region Modalidad Venta - Compañia

        public  bool insertarModVentaComp(ModVentaComp objModComp)
        {
            return objCnxAdministracion.insertarModVentaComp(objModComp);
        }

        public bool insertarModVentaCompCrit(ModVentaComp objModComp)
        {
            return (bool)oAdministracion.Registrar(objModComp, "ModVentaComp");
        }

        public bool actualizarModVentaCompCrit(ModVentaComp objModComp)
        {
            return (bool)oAdministracion.Actualizar(objModComp, "ModVentaComp");
        }

        public bool insertarSecuenciaModVentaComp(string codCompania)
        {
            return objCnxAdministracion.insertarSecuenciaModVentaComp(codCompania);
        }

        public  bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta)
        {
            return objCnxAdministracion.eliminarModVentaComp(sCodCompania, sCodModalidadVenta);
        }

        public bool eliminarModVentaCompCrit(ModVentaComp oModVentaComp)
        {
            return (bool)oAdministracion.Eliminar(oModVentaComp, "ModVentaComp");
        }

        public List<ModVentaComp> ListarModVentaCompxCompañia(string sCodCompania)
        {
            return objCnxAdministracion.ListarModVentaCompxCompañia(sCodCompania);
        }


        #endregion

        #region Modalidad Venta - Compañia - Atributos

        public  bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            return objCnxAdministracion.insertarModVentaCompAtr(objRModCompAtr);
        }

        public bool insertarModVentaCompAtrCrit(ModVentaCompAtr objRModCompAtr)
        {
            return (bool)oAdministracion.Registrar(objRModCompAtr, "ModVentaCompAtr");
        }

        public  bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr)
        {
            return objCnxAdministracion.actualizarModVentaCompAtr(objRModCompAtr);
        }

        public bool actualizarModVentaCompAtrCrit(ModVentaCompAtr objRModCompAtr)
        {
            return (bool)oAdministracion.Actualizar(objRModCompAtr, "ModVentaCompAtr");
        }

        public  bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo)
        {
            return objCnxAdministracion.eliminarModVentaCompAtr(sCodCompania, sCodModalidadVenta, CodAtributo);
        }

        public bool eliminarModVentaCompAtrCrit(ModVentaCompAtr objRModCompAtr)
        {
            return (bool)oAdministracion.Eliminar(objRModCompAtr, "ModVentaCompAtr");
        }

        public  List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta)
        {
            return objCnxAdministracion.ObtenerModVentaCompAtr(sCodCompania, sCodModalidadVenta);
        }

        public int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania)
        {
            return objCnxAdministracion.validarSerieTicketCompa(serieInicial, serieFinal, modalidad, compania);
        }


        #endregion

        #region esilva
        public Int64 obtenerIdTransaccionCritica() 
        {
            return oAuditoria.obtenerIdTransaccionCritica();
        }
        // TasaCambio
        public bool RegistrarTasaCambio(TasaCambio objTasaCambio)
        {
            return objCnxAdministracion.RegistrarTasaCambio(objTasaCambio);
            
        }
        public bool RegistrarTasaCambioCrit(TasaCambio objTasaCambio)
        {
            return (bool)oAdministracion.Registrar(objTasaCambio);
        }
        public bool EliminarTasaCambio(string strCodTasaCambio)
        {
            return objCnxAdministracion.EliminarTasaCambio(strCodTasaCambio);
        }
        public bool EliminarTasaCambioCrit(TasaCambio objTasaCambio)
        {
            return (bool)oAdministracion.Eliminar(objTasaCambio);
        }
        public DataTable ObtenerTasaCambio(string strCodTasaCambio)
        {
            return objCnxAdministracion.ObtenerTasaCambio(strCodTasaCambio);
        }
        // TasaCambioHist
        public bool RegistrarTasaCambioHist(TasaCambioHist objTasaCambioHist)
        {
            return objCnxAdministracion.RegistrarTasaCambioHist(objTasaCambioHist);
        }
        public DataTable ObtenerTasaCambioHist(string strCodTasaCambio)
        {
            return objCnxAdministracion.ObtenerTasaCambioHist(strCodTasaCambio);
        }
        // PrecioTicket
        public bool RegistrarPrecioTicket(PrecioTicket objPrecioTicket)
        {
            return objCnxAdministracion.RegistrarPrecioTicket(objPrecioTicket);
        }

        public bool RegistrarPrecioTicketCrit(PrecioTicket objPrecioTicket)
        {
            return (bool)oAdministracion.Registrar(objPrecioTicket);
        }

        public bool EliminarPrecioTicket(string strCodPrecioTicket)
        {
            return objCnxAdministracion.EliminarPrecioTicket(strCodPrecioTicket);
        }

        public bool EliminarPrecioTicketCrit(PrecioTicket objPrecioTicket)
        {
            return (bool)oAdministracion.Eliminar(objPrecioTicket);
        }

        public DataTable ObtenerPrecioTicket(string strCodPrecioTicket)
        {
            return objCnxAdministracion.ObtenerPrecioTicket(strCodPrecioTicket);
        }
        // PrecioTicketHist
        public bool RegistrarPrecioTicketHist(PrecioTicketHist objPrecioTicketHist)
        {
            return objCnxAdministracion.RegistrarPrecioTicketHist(objPrecioTicketHist);
        }
        public bool EliminarPrecioTicketHist(string strCodPrecioTicket)
        {
            return objCnxAdministracion.EliminarPrecioTicketHist(strCodPrecioTicket);
        }
        public DataTable ObtenerPrecioTicketHist(string strCodPrecioTicket)
        {
            return objCnxAdministracion.ObtenerPrecioTicketHist(strCodPrecioTicket);
        }
        #endregion
    }
}
