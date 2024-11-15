using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using System.Data;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LAP.TUUA.CONTROL
{
    public class BO_Consultas
    {
        protected Conexion objCnxConsulta;
        protected IConsulta oConsulta;

        public BO_Consultas()
        {
            objCnxConsulta = Resolver.ObtenerConexion(Define.CNX_05);
        }

        public BO_Consultas(string sKey)
        {
            oConsulta = (IConsulta)Resolver.ObtenerConexionObject(sKey);
            objCnxConsulta = Resolver.ObtenerConexion(Define.CNX_05);
        }


        #region Turno
        public DataTable ConsultaAllTurno(string sCodTurno)
        {
            return objCnxConsulta.ListarAllTurno(sCodTurno);
           
        }

        
        public DataTable CantidadMonedasTurno(string sCodTurno)
        {
            return objCnxConsulta.CantidadMonedasTurno(sCodTurno);
        }

        public DataTable DetalleMonedasTurno(string sCodTurno, string sMoneda, string sIdDetalle)
        {
            return objCnxConsulta.DetalladoCantidadMonedas(sCodTurno, sMoneda, sIdDetalle);
        }

        public DataTable ConsultaTurnoxFiltro(string sFchIni, string sFchFin, string sCodUsuario, string sPtoVta, string sHoraDesde, string sHoraHasta, string sFlgReporte)
        {
            return objCnxConsulta.ConsultaTurnoxFiltro(sFchIni, sFchFin, sCodUsuario, sPtoVta, sHoraDesde, sHoraHasta, sFlgReporte);
        }

        
        #endregion        
        
        #region Compañia
        public DataTable ConsultaCompaniaxFiltro(string sEstado, string sTipo, string sFiltro, string sOrdenacion)
        {
           return objCnxConsulta.ConsultaCompaniaxFiltro(sEstado, sTipo, sFiltro, sOrdenacion);
        }

         public DataTable listarAllCompania()
        {
            return objCnxConsulta.listarAllCompania();
        }

        #endregion
        
        #region ListadeCampos
        public DataTable ListaCamposxNombre(string sNombre)
        {
            return objCnxConsulta.ListaCamposxNombre(sNombre);

        }

        public DataTable ListaCamposxNombreOrderByDesc(string sNombre)
        {
            return objCnxConsulta.ListaCamposxNombreOrderByDesc(sNombre);

        }

      



        #endregion
        
        #region Ticket

        public DataTable ConsultaDetalleTicket(string sNumeroTicket,string sTicketDesde,string sTicketHasta)
        {
            return objCnxConsulta.ConsultaDetalleTicket(sNumeroTicket, sTicketDesde, sTicketHasta);
        }

        public DataTable ConsultaDetalleTicketPagin(string sNumeroTicket, string sTicketDesde, string sTicketHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxConsulta.ConsultaDetalleTicketPagin(sNumeroTicket, sTicketDesde, sTicketHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        public DataTable ConsultaDetalleTicket_Reh(string sNumeroTicket, string sTicketDesde, string sTicketHasta)
        {
            return objCnxConsulta.ConsultaDetalleTicket_Reh(sNumeroTicket, sTicketDesde, sTicketHasta);
        }


        public DataTable ConsultaDetalleTicket2_Reh(string sNumeroTicket, string sTicketDesde, string sTicketHasta, string sFlgTotal)
        {
            return objCnxConsulta.ConsultaDetalleTicket2_Reh(sNumeroTicket, sTicketDesde, sTicketHasta, sFlgTotal);
        }

        public DataTable ConsultaDetalleTicket_Ope(string sNumTicket, string sTicketDesde, string sTicketHasta, string stipoticket, string sTickets_Sel, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            //DataTable dtConsulta=objCnxConsulta.ConsultaDetalleTicket_Ope(sNumeroTicket, sTicketDesde, sTicketHasta, as_tipoticket);
            //for (int i = 0; i < dtConsulta.Rows.Count; i++)
            //{
            //    if (dtConsulta.Rows[i].ItemArray.GetValue(9).ToString().Trim() != "" && dtConsulta.Rows[i].ItemArray.GetValue(25).ToString().Trim()=="")
            //    {
            //        dtConsulta.Rows.RemoveAt(i--);
            //        continue;
            //    }
            //}
            //return dtConsulta;
            return objCnxConsulta.ConsultaDetalleTicket_Ope(sNumTicket, sTicketDesde, sTicketHasta, stipoticket, sTickets_Sel, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
        }

        public DataTable ListarTicketxFecha(string sFchDesde, string sFchHasta, string sCodCompania, string sTipoTicket, string sEstadoTicket, string sTipoPersona, string sFiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta,string sCodTurno)
        {
            return objCnxConsulta.ListarTicketxFecha(sFchDesde, sFchHasta, sCodCompania, sTipoTicket, sEstadoTicket, sTipoPersona, sFiltro, sOrdenacion, sHoraDesde, sHoraHasta, sCodTurno);
        }


        public DataTable ListarTicketxFechaPagin(string as_TipDoc,
            string as_FchDesde,
            string as_FchHasta,
            string as_HoraDesde,
            string as_HoraHasta,
            string as_CodCompania,
            string as_EstadoTicket,
            string as_TipoTicket,
            string as_TipoPersona,
            string as_TipoVuelo,
            string as_CodBoarding,
            string as_Turno,
            string as_FlgCobro,
            string as_FlgMasiva,
            string as_EstadoTurno,
            string as_Cajero,
            string as_MedioAnulacion,
            string sColumnSort,
            int iIniRows,
            int iMaxRows,
            string sTotalRows)
        {
            return objCnxConsulta.ListarTicketxFechaPagin(as_TipDoc,
                                                                    as_FchDesde,
                                                                    as_FchHasta,
                                                                    as_HoraDesde,
                                                                    as_HoraHasta,
                                                                    as_CodCompania,
                                                                    as_EstadoTicket,
                                                                    as_TipoTicket,
                                                                    as_TipoPersona,
                                                                    as_TipoVuelo,
                                                                    as_CodBoarding,
                                                                    as_Turno,
                                                                    as_FlgCobro,
                                                                    as_FlgMasiva,
                                                                    as_EstadoTurno,
                                                                    as_Cajero,
                                                                    as_MedioAnulacion,
                                                                    sColumnSort,
                                                                    iIniRows,
                                                                    iMaxRows,
                                                                    sTotalRows);
        }

        public DataTable ListarTicketxFecha_Reh(string sFchDesde, string sFchHasta, string sCodCompania, string sTipoTicket, string sEstadoTicket, string sTipoPersona, string sFiltro, string sOrdenacion, string sHoraDesde, string sHoraHasta, string sCodTurno)
        {
            return objCnxConsulta.ListarTicketxFecha_Reh(sFchDesde, sFchHasta, sCodCompania, sTipoTicket, sEstadoTicket, sTipoPersona, sFiltro, sOrdenacion, sHoraDesde, sHoraHasta, sCodTurno);
        }

        public DataTable obtenerCuadreTickesEmitidos(string sFchDesde, string sFchHasta,string sTipoDocumento, string sFlagAnulado)
        {
            return objCnxConsulta.obtenerCuadreTicketEmitidos(sFchDesde, sFchHasta, sTipoDocumento,sFlagAnulado);
        }

        public string obtenerFechaEstadistico(string sEstadistico)
        {
            return objCnxConsulta.obtenerFechaEstadistico(sEstadistico);
        }

        public DataTable obtenerTicketsAnulados(string sFchDesde, string sFchHasta)
        {
            return objCnxConsulta.obtenerTicketsAnulados(sFchDesde, sFchHasta);
        }

        public DataTable obtenerTicketsTablaTemporal()
        {
            return objCnxConsulta.obtenerTicketsTablaTemporal();
        }


        #endregion
        
        #region TicketEstHist
        public DataTable ListarTicketEstHist(string sNumeroTicket)
        {
            return objCnxConsulta.ListarTicketEstHist(sNumeroTicket);
        }

        public DataTable ListarTicketEstHist_Arch(string sNumeroTicket)
        {
            return objCnxConsulta.ListarTicketEstHist_Arch(sNumeroTicket);
        }


        public DataTable obtenerTicketBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado)
        {
            return objCnxConsulta.obtenerTicketBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }

        public DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal)
        {
            return objCnxConsulta.obtenerBoardingUsados(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sBoardingSel, sCodCompania, sNumVuelo, sNumAsiento, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotal);
        }

        #endregion

        #region Usuario
        public DataTable ConsultaUsuarioxFiltro(string sRol, string sEstado, string sGrupo, string sFiltro, string sOrdenacion)
        {
           return objCnxConsulta.ConsultaUsuarioxFiltro(sRol, sEstado, sGrupo, sFiltro, sOrdenacion);

        }

        public DataTable ListarAllUsuario()
        {
            return objCnxConsulta.ListarAllUsuario();

        }

        public DataTable ListarUsuarioxRol(string sCod_Rol) 
        {
            return objCnxConsulta.ListarUsuarioxRol(sCod_Rol);
        }

        #endregion

        #region Sincronizacion

        public DataTable ListarSincronizacion()
        {
            return objCnxConsulta.ListarSincronizacion();

        }

        public DataTable ListarFiltroSincronizacion(string smolinete, string sestado, 
            string sTipoSincronizacion, string sTablaSincronizacion,string strFchDesde, 
            string strFchHasta, string strHraDesde,string strHraHasta,
            string sFiltro, string sOrdenacion)
        {
            return objCnxConsulta.ListarFiltroSincronizacion(smolinete, sestado,
                sTipoSincronizacion, sTablaSincronizacion, strFchDesde, strFchHasta,
                strHraDesde, strHraHasta,
                sFiltro, sOrdenacion);

        }
       


        #endregion


        #region Depuracion


        public DataTable ListarFiltroDepuracion(string smolinete, string sestado,
             string sTablaSincronizacion, string strFchDesde,
            string strFchHasta, string strHraDesde, string strHraHasta,  string sFiltro, string sOrdenacion)
        {
            return objCnxConsulta.ListarFiltroDepuracion(smolinete, sestado,
                sTablaSincronizacion, strFchDesde, strFchHasta,
                strHraDesde, strHraHasta, sFiltro, sOrdenacion);

        }
       


        # endregion

        #region Parametros

        public DataTable ListarParametros(string sIdentificador)
        {
         
            return objCnxConsulta.ListarParametros(sIdentificador);
        }
        #endregion
        
        #region Rol

        public DataTable ListarRoles()
        {
     
            return objCnxConsulta.ListarRoles();
        }
        #endregion


        #region Molinete

        public DataTable Listarallmolinete(string strCodMolinete, string strDscIP)
        {

            return objCnxConsulta.ListarMolinetes(strCodMolinete, strDscIP);
        }
        #endregion
        
        #region BOarding


        public void IngresarDatosATemporalBoardingPass(string strTrama)
        {
            objCnxConsulta.IngresarDatosATemporalBoardingPass(strTrama);
        }

        public DataTable DetalleBoarding(string sCodCompania, string sNumVuelo, string sFechVuelo, string sNumAsiento, string sPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp)
        {
            return objCnxConsulta.DetalleBoarding(sCodCompania, sNumVuelo, sFechVuelo, sNumAsiento, sPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp);
        }

        public DataTable DetalleBoardingPagin(string sCodCompania, string sNumVuelo, string sFechVuelo, string sNumAsiento, string sPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxConsulta.DetalleBoardingPagin(sCodCompania, sNumVuelo, sFechVuelo, sNumAsiento, sPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        public DataTable DetalleBoarding_REH(string sCodCompania, string sNumVuelo, string sFechVuelo, string sNumAsiento, string sPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number)
        {
            return objCnxConsulta.DetalleBoarding_REH(sCodCompania, sNumVuelo, sFechVuelo, sNumAsiento, sPasajero, tipEstado, Cod_Unico_Bcbp, Num_Secuencial_Bcbp, Flag_Fch_Vuelo, Check_Seq_Number);
        }

        public DataTable DetalleBoardingArchivado(String Num_Secuencial_Bcbp)
        {
            return objCnxConsulta.DetalleBoardingArchivado(Num_Secuencial_Bcbp);
        }

        public DataTable ListarBoardingAsociados(String Num_Secuencial_Bcbp)
        {
            return objCnxConsulta.ListarBoardingAsociados(Num_Secuencial_Bcbp);
        }

        public string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase)
        {
            return objCnxConsulta.validarAsocBCBP(sNumAsiento, sNumVuelo, sFchVuelo, sNomPersona, sCompania, sCodBcbpBase);
        }

        public DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero)
        {
            return objCnxConsulta.obtenerDetalleWebBCBP(sCodCompania, sNroVuelo, sFchVuelo, sAsiento, sPasajero);
        }

        #endregion
        
        #region "FuncionExportarExcel"
        public void ExportarDataTableToExcel(System.Data.DataTable dt, System.Web.HttpResponse response)
        {
            response.Clear();
            response.Charset = "";
            //response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/vnd.ms-excel";
            response.ContentEncoding = Encoding.GetEncoding(1252);
            System.IO.StringWriter stringWrite = new StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            System.Web.UI.WebControls.DataGrid dg = new DataGrid();

            dg.DataSource = dt;

            dg.DataBind();

            dg.RenderControl(htmlWrite);

            response.Write(stringWrite.ToString());
            response.End();

        }
        #endregion

        #region BOardingErr
        public DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde,
            string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding,
            string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort,
            int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows)
        {
            return objCnxConsulta.ListarLogErroresMolinete(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIDError, sTipoError, sCompania, sCodMolinete, sTipoBoarding, sTipIngreso, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sMostrarResumen, sFlgTotalRows);
        }

        #endregion

        #region BOardingEstHist
        public DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp)
        {
            return objCnxConsulta.DetalleBoardingEstHist(Num_Secuencial_Bcbp);
        }

        public DataTable DetalleBoardingEstHist_Arch(String Num_Secuencial_Bcbp)
        {
            return objCnxConsulta.DetalleBoardingEstHist_Arch(Num_Secuencial_Bcbp);
        }
        #endregion
        
        #region LogOperacion
        public DataTable UsuarioxFechaOperacion(string as_FechaOperacion, string as_CodUsuario, string as_TipoOperacion, string as_CodMoneda)
        {
            return objCnxConsulta.obtenerUsuarioxFechaOperacion(as_FechaOperacion, as_CodUsuario, as_TipoOperacion, as_CodMoneda);
        }

       

        #endregion

        #region VueloProgramado
        public DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            return objCnxConsulta.ObtenerDetallexLineaVuelo(sFechaDesde, sFechaHasta, sCodCompania, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
        }
        #endregion
        
        #region Auditoria
        public DataTable FiltrosAuditorias(string strCodModulo, string strFlgSubModulo, string strTablaXml)
        {
            return objCnxConsulta.FiltrosAuditoria(strCodModulo, strFlgSubModulo, strTablaXml);
        }

        public DataTable obtenerconsultaAuditorias(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta)
        {
            return objCnxConsulta.ObtenerConsultaAuditoria(strTipOperacion, strTabla, 
                strCodModulo, strCodSubModulo, strCodUsuario,
                strFchDesde, strFchHasta, strHraDesde, strHraHasta);
        }

        public DataTable obtenerconsultaAuditoriasPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                        string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                        string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxConsulta.ObtenerConsultaAuditoriaPagin(strTipOperacion, strTabla, strCodModulo, strCodSubModulo, strCodUsuario, strFchDesde, strFchHasta, strHraDesde, strHraHasta, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        public DataTable obtenerconsultaAuditoriasPaginCrit(string strTipOperacion, string strTabla, 
            string strCodModulo, string strCodSubModulo, string strCodUsuario, string strFchDesde, 
            string strFchHasta, string strHraDesde, string strHraHasta, string sColumnSort, 
            int iIniRows, int iMaxRows, string sTotalRows)
        {
            return oConsulta.ObtenerConsultaAuditoriaPaginCrit(strTipOperacion, strTabla, strCodModulo, 
                strCodSubModulo, strCodUsuario, strFchDesde, strFchHasta, strHraDesde, strHraHasta, 
                sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }

        public DataTable obtenerdetalleAuditoria(string strNombreTabla, string strContador)
        {
            return objCnxConsulta.ObtenerDetalleAuditoria(strNombreTabla, strContador);
        }

        public DataTable obtenerdetalleAuditoriaCrit(string strNombreTabla, string strContador)
        {
            return oConsulta.ObtenerDetalleAuditoriaCrit(strNombreTabla, strContador);
        }

        #endregion

        #region Additional Methods - kinzi
        /// <summary>
        /// ConsultaTurnoxFiltro2 - kinzi 
        /// Usado en Consulta de Tickets Procesados
        /// </summary>
        /// <param name="sFchIni">Fecha inicial del turno</param>
        /// <param name="sFchFin">Fecha final del turno</param>
        /// <param name="sCodUsuario">Codigo dle cajero que atendio el turno</param>
        /// <param name="sCodTurno">Codigo del turno</param>
        /// <returns>Listado de turnos que cumplen la condicion de busqueda</returns>
        public DataTable ConsultaTurnoxFiltro2(string sFchIni, string sFchFin, string sCodUsuario, string sCodTurno)
        {
            return objCnxConsulta.ConsultaTurnoxFiltro2(sFchIni, sFchFin, sCodUsuario, sCodTurno);
        }
        /// <summary>
        /// ListarTicketProcesado - kinzi
        /// </summary>
        /// <param name="strCodTurno"></param>
        /// <returns></returns>
        public DataTable ListarTicketProcesado(string strCodTurno)
        {
            return objCnxConsulta.ListarTicketProcesado(strCodTurno);
        }
        /// <summary>
        /// ListarTicketVendido - kinzi
        /// </summary>
        /// <param name="strTipo"></param>
        /// <param name="strFecIni"></param>
        /// <param name="strFecFin"></param>
        /// <param name="strTurnoIni"></param>
        /// <param name="strTurnoFin"></param>
        /// <returns></returns>
        public DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objCnxConsulta.ListarTicketVendido(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        /// <summary>
        /// ListarResumenCompraVenta - kinzi
        /// </summary>
        /// <param name="strTipo"></param>
        /// <param name="strFecIni"></param>
        /// <param name="strHorIni"></param>
        /// <param name="strFecFin"></param>
        /// <param name="strHorFin"></param>
        /// <param name="strTurnoIni"></param>
        /// <param name="strTurnoFin"></param>
        /// <returns></returns>
        public DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objCnxConsulta.ListarResumenCompraVenta(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        /// <summary>
        /// ListarResumenTasaCambio - kinzi
        /// </summary>
        /// <param name="strTipo"></param>
        /// <param name="strFecIni"></param>
        /// <param name="strHorIni"></param>
        /// <param name="strFecFin"></param>
        /// <param name="strHorFin"></param>
        /// <param name="strTurnoIni"></param>
        /// <param name="strTurnoFin"></param>
        /// <returns></returns>
        public DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin)
        {
            return objCnxConsulta.ListarResumenTasaCambio(strTipo, strFecIni, strHorIni, strFecFin, strHorFin, strTurnoIni, strTurnoFin);
        }
        //ListarTicketBoardingUsadosPagin - 03.09.2010
        public DataTable ListarTicketBoardingUsadosPagin(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows)
        {
            return objCnxConsulta.ListarTicketBoardingUsadosPagin(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado, sColumnSort, iIniRows, iMaxRows, sTotalRows);
        }
        //ListarTicketBoardingUsadosResumen - 08.09.2010
        public DataTable ListarTicketBoardingUsadosResumen(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado)
        {
            return objCnxConsulta.ListarTicketBoardingUsadosResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }
        //ListarParametrosDefaultValue - 17.04.2011
        public DataTable ListarParametrosDefaultValue(string sIdentificador)
        {
            return objCnxConsulta.ListarParametrosDefaultValue(sIdentificador);
        }

        #endregion
    }
}
