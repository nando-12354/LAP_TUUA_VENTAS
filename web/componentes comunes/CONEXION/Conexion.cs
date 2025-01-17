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

        public abstract bool actualizarContrase�aUsuario(string sCodUsuario, string sContrase�a, string SLogUsuarioMod, DateTime DtFchVigencia);

        public abstract bool actualizarEstadoUsuario(string sCodUsuario, string sEstado, string SLogUsuarioMod, string sFlagCambPw);

        public abstract DataTable obtenerFecha();

        public abstract DataTable ObtenerVentaTicket(string strFecIni, string strFecFin,string strTipVenta,string strFlgAero);

        public abstract DataTable ObtenerComprobanteSEAE(string sAnio, string sMes, string sTDocumento,string strTipVenta, string strFlgAero);

        //Arbol Modulo

        public abstract DataTable LLenarMenu(string sCodUsuario);

        public abstract DataTable LLenarArbolRoles(string sCodRol);

        public abstract ArbolModulo ObtenerArbolModulo(string sCodModulo, string sCodProceso, string sCodRol);

        public abstract List<ArbolModulo> ListarPerfilVenta(string strUsuario);

        //Usuario Rol

        public abstract bool registrarRolUsuario(UsuarioRol objRolUsuarioRol);

        public abstract bool eliminarRolUsuario(string sCodUsuario, string sCodRol);

        public abstract UsuarioRol obtenerUsuarioRolxCodRol(string sCodRol);

        public abstract List<UsuarioRol> ListarUsuarioRolxCodUsuario(string sCodUsuario);

        public abstract UsuarioRol obtenerRolUsuario(string sCodUsuario, string sCodRol);

        public abstract bool obtenerRolHijo(string sCodRol);

        public abstract DataTable ListarUsuarioxRol(string sCodRol);

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

        public abstract bool obtenerClaveUsuHist(string sCodUsuario, string SDscClave,int iNum);
        
        public abstract ClaveUsuHist obtenerUsuarioHist(string sCodUsuario);

        //Modulo

        public abstract DataTable ListarAllModulo();

        #endregion

        #region Turno


        public abstract List<Moneda> ListarMonedas();

        public abstract List<Moneda> ListarMonedasxTipoTicket();

        public abstract bool verificarTurnoCerradoxUsuario(string strUsuario);

        public abstract bool verificarTurnoCerradoxPtoVenta(string strEquipo);

        public abstract bool CrearTurno(string strSec, string strUsuario, string strEquipo);

        public abstract Turno ObtenerTurnoIniciado(string strUsuario);

        public abstract bool RegistrarTurnoMonto(List<TurnoMonto> listaMontos);

        public abstract bool ActualizarTurno(Turno objTurno);

        public abstract bool ActualizarTurnoMonto(List<TurnoMonto> listaMontos);

        public abstract List<TurnoMonto> ListarTurnoMontosPorTurno(string strTurno);

        public abstract bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno);

        #endregion

        #region Operaciones

        public abstract List<Moneda> ListarMonedasInter();

        public abstract List<Limite> ListarLimitesPorOperacion(string stipoope);

        public abstract bool RegistrarOperacion(LogOperacion objOperacion);

        public abstract bool RegistrarOpeCaja(List<LogOperacCaja> objLista);

        public abstract TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo);

        public abstract bool RegistrarCompraVenta(LogCompraVenta objCompraVenta);

        public abstract TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans);

        public abstract TipoTicket validarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans);

        public abstract DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion);

        public abstract DataTable ListarVuelosxCompania(string strCompania, string strFecha);

        public abstract bool RegistrarTicket(List<Ticket> listaTickets, ref string strListaTickets);

        public abstract bool IsError();

        public abstract string GetErrorCode();

        public abstract List<RepresentantCia> ListarRepteCia(string strCia);

        public abstract bool RegistrarVentaMasiva(VentaMasiva objVentaMasiva);

        public abstract bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva);

        public abstract List<TipoTicket> ListarAllTipoTicket();

        public abstract List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp);

        public abstract List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta);

        public abstract List<ModalidadAtrib> ListarAtributosxModVentaCompania(string strCodModVenta, string strCompania);

        public abstract DataTable ListarTurnosAbiertos();

        public abstract bool ActualizarTicket(List<Ticket> listaTickets);

        public abstract bool ExtornarCompraVenta(string strCodOpera, string strTurno, int intCantidad, ref string strMessage);

        public abstract bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario,string strMotivo, ref string strMessage);

        public abstract bool ExtornoRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, string strEstado, bool transaccion);

        public abstract bool ExtenderVigenciaTicket(string strListaTickets, string strListaFechas, string strUsuario);

        public abstract DataTable ListarMolinetes(string strCodMolinete,string strDscIp);

        public abstract DataTable ListarAllMolinetes();

        public abstract DataTable obtenerMolinete(String strMolinete);

        public abstract bool actualizarMolinete(Molinete objMolinete);

        public abstract bool actualizarUnMolinete(Molinete objMolinete);

        public abstract bool AnularTicket(string sCodNumeroTicket, string sDscMotivo, string sUsuarioMod);

        public abstract bool ActualizarEstadoBCBP(BoardingBcbp objBoardingBcbp);

        public abstract bool AnularBCBP(BoardingBcbp objBoardingBcbp);

        public abstract bool EliminarListaDeCampo(string strNomCampo, string strCodCampo);

        public abstract bool RegistrarTicket(string strCompania, string strVentaMasiva, string strNumVuelo, string strFecVuelo, string strTurno, string strUsuario, 
                                             decimal decPrecio, string strMoneda, string strModVenta, string strTipTicket, string strTipVuelo, int intTickets, 
                                             string strFlagCont, string strNumRef, string strTipPago, string strEmpresa, string strRepte, ref string strFecVence, 
                                             ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno, string strCodPrecio);

        public abstract bool RegistrarVentaContingencia(string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, int intTickets, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError);

        public abstract DataTable ListarContingencia(string strTipTikcet, string strFlgConti, string strNumIni, string strNumFin, string strUsuario);

        public abstract bool AnularTuua(string strListaTicket,int intTicket);

        public abstract DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg);

        public abstract bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc);
        
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

        //Atributos Modalidad de Venta

        public abstract bool insertarModVentaAtributo(ModalidadAtrib objModalidadAtrib);
        
        public abstract bool actualizarModVentaAtributo(ModalidadAtrib objModalidadAtrib);
        
        public abstract bool eliminarModVentaAtributo(string sCodModalidadVenta, string sCodAtributo, string sCodTipoTicket);

        public abstract List<ModalidadAtrib> ListarAtributosxModVentaTipoTicket(string strCodModVenta, string strTipoTicket);

        public abstract int validarSerieTicket(int serieInicial, int serieFinal, string modalidad);
        
        //Parametros Generales

        public abstract List<ParameGeneral> listarAtributosGenerales();

        // Compa�ia

        public abstract bool insertarCompania(Compania objCompania);

        public abstract bool actualizarCompania(Compania objCompania);

        public abstract Compania obtenerCompa�iaxcodigo(string sCodigoCompania);

        public abstract Compania obtenerCompa�iaxnombre(string sNombreCompania);

        public abstract int validarDocumento(string sNombre, string sApellido, string sTpDocumento, string sNroDocumento);

        // Representante

        public abstract bool insertarRepresentante(RepresentantCia objRepresentante);

        public abstract bool actualizarRepresentante(RepresentantCia objRepresentante);
        

        // Modalidad Venta - Compa�ia

        public abstract bool insertarModVentaComp(ModVentaComp objModComp);

        public abstract bool eliminarModVentaComp(string sCodCompania, string sCodModalidadVenta);

        public abstract List<ModVentaComp> ListarModVentaCompxCompa�ia(string sCodCompania);

        public abstract int validarAnulacionModalidad(string sModalidad, string sCompania);
        

        // Modalidad Venta - Compa�ia - Atributos

        public abstract bool insertarModVentaCompAtr(ModVentaCompAtr objRModCompAtr);

        public abstract bool actualizarModVentaCompAtr(ModVentaCompAtr objRModCompAtr);

        public abstract bool eliminarModVentaCompAtr(string sCodCompania, string sCodModalidadVenta, string CodAtributo);

        public abstract List<ModVentaCompAtr> ObtenerModVentaCompAtr(string sCodCompania, string sCodModalidadVenta);

        public abstract bool insertarSecuenciaModVentaComp(string codModalidad);

        public abstract int validarSerieTicketCompa(int serieInicial, int serieFinal, string modalidad, string compania);
        
        #endregion

        #region Consultas

        // Turno

        public abstract DataTable obtenerTicketsTablaTemporal();

        public abstract DataTable ListarAllTurno(string as_codturno);

        public abstract DataTable CantidadMonedasTurno(string as_codturno);

        public abstract DataTable DetalladoCantidadMonedas(string as_codturno, string as_codmoneda, string as_idDetalle);

        public abstract DataTable ConsultaTurnoxFiltro(string as_fchIni, string as_fchFin, string as_codusuario, string as_ptoventa,string as_horadesde, string as_horahasta,string as_FlgReporte);

                
        // Compania

        public abstract DataTable ConsultaCompaniaxFiltro(string strEstado, string strTipo, string sfiltro, string sOrdenacion);

        public abstract DataTable listarAllCompania();

        // Usuarios

        public abstract DataTable ConsultaUsuarioxFiltro(string as_rol, string as_estado, string as_grupo, string sfiltro, string sOrdenacion);

        public abstract DataTable ListarAllUsuario();

        //Sincronizacion

        public abstract DataTable ListarSincronizacion();

        public abstract DataTable ListarFiltroSincronizacion(string as_molinete, 
            string as_estado, string as_TipoSincronizacion, 
            string as_TablaSincronizacion,string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta, string sfiltro, string sOrdenacion);

        //Depuracion

        public abstract DataTable ListarFiltroDepuracion(string as_molinete,
            string as_estado, string as_TablaSincronizacion, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta,  string sfiltro, string sOrdenacion);

        // ParametrosGeneral

        public abstract DataTable ListarParametros(string as_identificador);

        // Ticket

        public abstract DataTable ConsultaDetalleTicket(string as_numeroticket,string as_ticketdesde,string as_tickerhasta);
        public abstract DataTable ConsultaDetalleTicketPagin(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_ColumnSort, int i_IniRows, int i_MaxRows, string as_TotalRows);
        public abstract DataTable ConsultaDetalleTicket_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta);

        public abstract DataTable ConsultaDetalleTicket2_Reh(string as_numeroticket, string as_ticketdesde, string as_tickerhasta, string as_flgtotal);

        public abstract DataTable ConsultaDetalleTicket_Ope(string as_numeroticket, string as_ticketdesde, string as_ticket_hasta, string as_tipoticket, string as_Tickets_Sel, string as_sColumnaSort, int as_iStartRows, int as_iMaxRows, string as_sPaginacion, string as_sFlgTotalRows);
        public abstract DataTable ConsultaDetalleTicket(string sNumeroTicket);
        public abstract DataTable ListarTicketxFecha(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion,string as_HoraDesde,string as_HoraHasta,string strTurno);
        public abstract DataTable ListarTicketsExtorno(string as_Cod_Ticket, string as_Ticket_Desde, string as_Ticket_Hasta, string as_Cod_Turno, string as_ColumnaSort, int as_iStartRows, int as_iMaxRows, string as_Paginacion, string as_FlgTotalRows);
        public abstract DataTable ListarTicketxFechaPagin(
            string as_TipDoc,
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
            string sTotalRows); 
        public abstract DataTable ListarTicketxFecha_Reh(string as_FchDesde, string as_FchHasta, string as_CodCompania, string as_TipoTicket, string as_EstadoTicket, string as_TipoPersona, string as_filtro, string as_Ordenacion, string as_HoraDesde, string as_HoraHasta, string strTurno);
        public abstract DataTable obtenerCuadreTicketEmitidos(string as_FchDesde, string as_FchHasta,string as_TipoDocumento, string as_FlagAnulado);
        public abstract DataTable obtenerTicketsAnulados(string as_FchDesde, string as_FchHasta);
        public abstract DataTable ConsultarTicketVencidos(string strFecIni, string strFecFin, string strTipoTicket);

        // TicketEstHist

        public abstract DataTable ListarTicketEstHist(string as_numeroticket);
        public abstract DataTable ListarTicketEstHist_Arch(string as_numeroticket);
        public abstract DataTable obtenerTicketBoardingUsados(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo,string as_Estado);



        //Boarding

        public abstract DataTable obtenerBoardingUsados(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sBoardingSel, string sCodCompania, string sNumVuelo, string sNumAsiento, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotal);
        public abstract void IngresarDatosATemporalBoardingPass(string strTrama);
        public abstract DataTable DetalleBoarding(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp);
        public abstract DataTable DetalleBoardingPagin(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, string Cod_Unico_Bcbp, string Num_Secuencial_Bcbp, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows);
        public abstract DataTable DetalleBoarding_REH(string strCodCompania, string strNumVuelo, string strFechVuelo, string strNumAsiento, string strPasajero, string tipEstado, String Cod_Unico_Bcbp, String Num_Secuencial_Bcbp, string Flag_Fch_Vuelo, string Check_Seq_Number);
        public abstract DataTable DetalleBoardingArchivado(String Num_Secuencial_Bcbp);
        public abstract DataTable ListarBoardingAsociados(String Num_Secuencial_Bcbp);
        public abstract string validarAsocBCBP(string sNumAsiento, string sNumVuelo, string sFchVuelo, string sNomPersona, string sCompania, string sCodBcbpBase);
        public abstract DataTable obtenerDetalleWebBCBP(string sCodCompania, string sNroVuelo, string sFchVuelo, string sAsiento, string sPasajero);

        //Boarding Error

        public abstract DataTable ListarLogErroresMolinete(string sFechaDesde, string sFechaHasta, string sHoraDesde,
            string sHoraHasta, string sIDError, string sTipoError, string sCompania, string sCodMolinete, string sTipoBoarding,
            string sTipIngreso, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sColumnaSort,
            int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows);

        //BoardingEsthist

        public abstract DataTable DetalleBoardingEstHist(String Num_Secuencial_Bcbp);
        public abstract DataTable DetalleBoardingEstHist_Arch(String Num_Secuencial_Bcbp);



        //Molinete

        //public abstract DataTable ListarMolinete();

        //ListaDeCampos
        public abstract DataTable ListaCamposxNombre(string sNombre);

        public abstract DataTable ListaCamposxNombreOrderByDesc(string sNombre);

        // Rol

        public abstract DataTable ListarRoles();

        //LogOperacion

        public abstract DataTable obtenerUsuarioxFechaOperacion(string sFechaOperacion, string sCodUsuario, string sTipoOperacion, string sCodMoneda);        


        //Vuelo Programado

        public abstract DataTable ObtenerDetallexLineaVuelo(string sFechaDesde, string sFechaHasta, string sCodCompania, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows);


        //Auditoria
        public abstract DataTable FiltrosAuditoria(string strCodModulo, string strFlgSubModulo, string strTablaXml);
        public abstract DataTable ObtenerConsultaAuditoria(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                                string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                                string strHraHasta);
        public abstract DataTable ObtenerConsultaAuditoriaPagin(string strTipOperacion, string strTabla, string strCodModulo, string strCodSubModulo,
                                        string strCodUsuario, string strFchDesde, string strFchHasta, string strHraDesde,
                                        string strHraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows);

        public abstract DataTable ObtenerDetalleAuditoria(string strNombreTabla, string strContador);

        #endregion

        #region Configuracion

        // Parametros generales

        public abstract DataTable ListarAllParametroGenerales(string strParametro);

        public abstract DataTable DetalleParametroGeneralxId(string sIdentificador);

        public abstract bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta);

        public abstract ParameGeneral obtenerParametroGeneral(string sCodParam);

        public abstract bool actualizarHoras(ListaDeCampo objListaCampo);

        public abstract bool actualizarestado(Sincronizacion objListaSincronizacion);

        #endregion

        #region Alarmas

        // Configuracion de Alarmas

        public abstract bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma);

        public abstract bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma);

        public abstract bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo);

        public abstract DataTable ListarAllCnfgAlarma();

        public abstract CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo);


        //Alarmas Generadas

        public abstract bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada);

        public abstract bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada);

        public abstract DataTable ListarAllAlarmaGenerada();
            
        public abstract AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada);

        public abstract DataTable ListarAlarmaGeneradaEnviadas();
        
        public abstract DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado);

        //Alarmas

        public abstract DataTable ObtenerAlarmaxCodModulo(string sCodModulo);

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
        // Parametros Generales
        public abstract DataTable ListarParametrosDefaultValue(string as_identificador);
        #endregion

        #region ealiaga

        public abstract DataTable ConsultarRepresXRehabilitacionYCia(String strCia);
        public abstract bool registrarRehabilitacionTicket(TicketEstHist objTicketEstHist, int flag, int sizeOutput);
        public abstract DataTable consultarVuelosTicketPorCiaFecha(string sCompania, string fechaVuelo);
        public abstract DataTable consultarTicketsPorVuelo(string sCompania, string fechaVuelo, string dsc_Num_Vuelo);

        public abstract DataTable consultarVuelosBCBPPorCiaFecha(string sCompania, string fechaVuelo);
        public abstract bool registrarRehabilitacionBCBP(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput);
        public abstract bool registrarRehabilitacionBCBPAmpliacion(BoardingBcbpEstHist boardingBcbpEstHist, int flag, int sizeOutput);

        public abstract DataTable listarCompania_xCodigoEspecial(String codigoEspecial);
        public abstract DataTable obteneterBoardingsByRangoFechas(string sCompania, string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta);

        #endregion 

        #region Reportes
            public abstract DataTable obtenerMovimientoTicketContingencia(string sFchDesde, string sFchHasta, string sEstado, string sTipoTicket, string sEstadoTicket, string sRangoMinTicket, string sRangoMaxTicket);
            public abstract DataTable obtenerResumenStockTicketContingencia(string sTipoTicket, string sFchAl, string sTipoResumen);
            public abstract DataTable consultarTicketBoardingUsados(string sCodCompania, string sNumVuelo, string sTipoDocumento, string sTipoTicket, string sTipoFiltro, string sFechaInicial, string sFechaFinal, string sTimeInicial, string sTimeFinal, string sDestino, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows);
            public abstract DataTable obtenerResumenTicketVendidosCredito(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago);
            public abstract DataTable obtenerRecaudacionMensual(string anio);
            public abstract DataTable BoardingLeidosMolinete(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen);
            public abstract DataTable obtenerDetalleVentaCompania(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta);
            public abstract DataTable ConsultarLiquidVenta(string strFecIni, string strFecFin);
            public abstract DataTable ConsultarUsoContingencia(string strFecIni, string strFecFin);
            public abstract DataTable ConsultarUsoContingenciaUsado(string strFecIni, string strFecFin);
            public abstract DataTable obtenerLiquidacionVenta(string sFchDesde, string sFchHasta);
            public abstract DataTable obtenerLiquidacionVentaResumen(string sFchDesde, string sFchHasta);
            public abstract DataTable obtenerTicketBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sDocumento, string sTicket, string sAerolinea, string sVuelo, string sMotivo);
            public abstract DataTable obtenerBoardingRehabilitados(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sMostrarResumen, string sFlgTotalRows);
            public abstract DataTable consultarBoardingPassDiario(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoPasajero, string sTipoVuelo, string sTipoTrasbordo, string sFechaVuelo, string sNumVuelo, string sPasajero, string sNumAsiento, string sCodIata, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows);
        #endregion

        #region Additional Methods - kinzi
        public abstract DataTable ConsultaTurnoxFiltro2(string strFchIni, string strFchFin, string strCodUsuario, string strCodTurno);
        public abstract DataTable ListarTicketProcesado(string strCodTurno);
        public abstract DataTable ListarTicketVendido(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin);
        public abstract DataTable ListarResumenCompraVenta(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin);
        public abstract DataTable ListarResumenTasaCambio(string strTipo, string strFecIni, string strHorIni, string strFecFin, string strHorFin, string strTurnoIni, string strTurnoFin);
        public abstract DataTable ListarTicketBoardingUsadosPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado, string sColumnSort,int iIniRows, int iMaxRows, string sTotalRows); //03.09.2010
        public abstract DataTable ListarTicketBoardingUsadosResumen(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string as_CodCompania, string as_TipoVuelo, string as_NumVuelo, string as_TipoPasajero, string as_TipoDocumento, string as_TipoTrasbordo, string as_FechaVuelo, string as_Estado); //08.09.2010
        public abstract DataTable ListarResumenCompaniaPagin(string as_FechaDesde, string as_FechaHasta, string as_HoraDesde, string as_HoraHasta, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows); //05.10.2010
        public abstract DataTable ListarTicketVencidosPagin(string strFecIni, string strFecFin, string strTipoTicket, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows); //21.10.2010
        public abstract DataTable ListarBoardingLeidosMolinetePagin(string strCodCompania, string strFechVuelo, string strNum_Vuelo, string strFechaLecturaIni, string strFechaLecturaFin, string strCodEstado, string strNumBoarding, string strFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows); //01.12.2010
        public abstract DataTable ListarTicketBoardingUsadosDiaMesPagin(string sFchDesde, string sFchHasta, string sFchMes
                        , string sTipoDocumento, string sCodCompania, string sNumVuelo, string sTipTicket
                        , string sDestino, string sTipReporte, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows);
        public abstract DataTable ListarResumenTicketVendidosCreditoPagin(string sFechaInicial, string sFechaFinal, string sTipoTicket, string sNumVuelo, string sAeroLinea, string sCodPago, string sFlagResumen, string sColumnSort, int iIniRows, int iMaxRows, string sTotalRows);
        #endregion
         
        #region Auditoria
        public abstract bool InsertarAuditoria(string strCodModulo, string strCodSubModulo, string strCodUsuario, string strTipOperacion);
        #endregion

        #region Estadistico
        public abstract DataTable ObtenerEstadistico();
        #endregion

        public abstract string ObtenerFechaActual();
        public abstract string obtenerFechaEstadistico(string sEstadistico);
        public abstract DataTable ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac);
        public abstract DataTable ListarTasaCambio();

    }
}
