using System;
using System.Collections.Generic;
using System.Text;

namespace BMatic.DA
{
    public class Define
    {
        #region "Datos para acceder al registro de Windows"
        public static string HKEY_PATH_BMATIC = @"SOFTWARE\BMATICCOM+";			    //Nodo en el Registro para BMATIC
        public static string HKEY_PATH_HIPERCHANNEL = @"SOFTWARE\HIPERCHANNEL";		    //Nodo en el Registro para BMATIC
        public static string HKEY_VALUE_TIPOSERVERDB = "TipoServerDB";			    //DBMS. Los valores son:

        //para DB produccion
        public static string HKEY_VALUE_SERVERPROD = "ServerProd";				    //Servidor que hospeda el DBMS
        public static string HKEY_VALUE_DBPNAME = "DBPName";					    //Nombre de DB
        public static string HKEY_VALUE_DBPUSER = "DBPUser";					    //Nombre de usuario del DBMS
        public static string HKEY_VALUE_DBPPASSWORD = "DBPPassword";			    //Password de usuario del DBMS
        public static string HKEY_VALUE_DBPTIPOCNX = "Tipocnx";

        //para DB reporte
        public static string HKEY_VALUE_SERVERREPORT = "ServerReport";			    //Servidor que hospeda el DBMS
        public static string HKEY_VALUE_DBRNAME = "DBRName";					    //Nombre de DB
        public static string HKEY_VALUE_DBRUSER = "DBRUser";					    //Nombre de usuario del DBMS
        public static string HKEY_VALUE_DBRPASSWORD = "DBRPassword";			    //Password de usuario del DBMS
        public static string HKEY_VALUE_DBRTIPOCNX = "Tipocnx";

        public static string HKEY_VALUE_CARPETA_LOG_ERRORES = "LogHReporteNet";	    //guardará el Path para registrar errores
        public static string HKEY_VALUE_CONNECTION_POOLING = "ConnectionPooling";	//Flag que indica si se usará Connection Pooling en OLEDB
        //  1 -> Sí se usará Connection Pooling
        //  0 -> No se usará Connection Pooling
        #endregion

        public static string CADENA_DE_CONEXION_DBPROD = "";
        public static string CADENA_DE_CONEXION_DBREPORT = "";
        public static string NAME_TABLE_DEFAULT = "Resultado";

        //Para Encriptacion
        public static string HKEY_VALUE_TIPOLICENCIA = "TipoLicencia";              //Key 
        public static string HKEY_VALUE_ENCVECTOR = "EncVector";                    //Vector de encriptacion

        //para los valores fijos de las consultas
        public static string HHMMSSmmm_FIN_DIA = "235959000";

        //Count thTickResumen para operadorVentanilla (thTickResumen)
        public static string CANTIDAD_ATENDIDOS = "CantidadAtendidos";
        //Cantidad de abandonados en el thTickResumen
        public static string CANTIDAD_ABANDONADOS = "CantidadAbandonados";

        //Constante de TimeOut para todas las conexion de BMatic, en segundos aprox: 16.6 minutos.
        public static int TIME_OUT = 1000;

        #region "Tablas"
        public static string tbl_TMARCHIVO = "tmArchivo";
        public static string tbl_TPTICKET = "tpTicket";
        public static string tbl_TPTICKOPERACION = "tpTickOperacion";
        public static string tbl_TMTVENTANILLA = "tmTVentanilla";
        public static string tbl_TMVENTANILLA = "tmVentanilla";
        public static string tbl_TXCONSOLIDALOG = "txConsolidaLog";
        public static string tbl_TMPAIS = "tmPais";
        public static string tbl_TMREGION = "tmRegion";
        public static string tbl_TMDEPARTAMENTO = "tmDepartamento";
        public static string tbl_TMZONA = "tmZona";
        public static string tbl_TMTIPOAGENCIA = "tmTipoAgencia";
        public static string tlb_TMGRUPOAGENCIA = "tmGrupoAgencia";
        public static string tlb_TMTAMANHOAGENCIA = "tmTamanhoAgencia";
        public static string tbl_TMAGENCIA = "tmAgencia";
        public static string tbl_TMSECTOR = "tmSector";
        public static string tbl_TMTTICKET = "tmTTicket";
        public static string tbl_TMCATCLIENTE = "tmCatCliente";
        public static string tbl_TMUSUARIO = "tmUsuario";
        public static string tbl_TMOPERACION = "tmOperacion";
        public static string tbl_TMMOTIVOS = "tmMotivos";
        public static string tbl_TXPARAMMETAS = "txParamMetas";
        public static string tbl_TMTIPODIA = "tmTipoDia";
        public static string tbl_TMCALIFICACION = "tmCalificacion";
        public static string tbl_TMCALIFICACIONZONA = "tmCalificacionZona";
        public static string tbl_TMCALIFICACIONGENERAL = "tmCalificacionGeneral";
        public static string tbl_TXMETASPARTICULARES = "txMetasParticulares";
        public static string tbl_TXMETASZONA = "txMetasZona";
        public static string tbl_TXTIEMPOESTANDAR = "txTiempoEstandar";
        public static string tbl_TMCLIENTEMARKETING = "tmClienteMarketing";
        public static string tbl_TMTIEMPO = "tmTiempo";
        public static string tbl_TPOPERADORVENTANILLA = "tpOperadorVentanilla";
        public static string tbl_TPOPERADORVENTANILLALOG = "tpOperadorVentanillaLog";
        public static string tbl_TPTICKOPERACIONLOG = "tpTickOperacionLog";
        public static string tbl_TPTICKETLOG = "tpTicketLog";
        public static string tbl_TPTICKTRANSACCION = "tpTickTransaccion";
        public static string tbl_TPTICKTRANSACCIONLOG = "tpTickTransaccionLog";
        public static string tbl_TPPROGRAMACION = "tpProgramacion";
        public static string tbl_TPPROGRAMACIONLOG = "tpProgramacionLog";
        public static string tbl_TXPARAMGENERAL = "txParamGeneral";
        public static string tbl_TMSERVERCENTRAL = "tmServerCentral";
        public static string tbl_TPPERFILSERVICIOS = "tpPerfilServicios";
        public static string tbl_TAPERFILAGENCIA = "taPerfilAgencia";
        public static string tbl_TPCONSOLIDA = "tpConsolida";
        public const string tbl_TXREGISTRO = "txRegistro";
        public const string tbl_TXREGISTROCAMPO = "txRegistroCampo";
        public const string tbl_THTICKRESUMEN = "thTickResumen";
        public const string tbl_THTICKANULA = "thTickAnula";
        public const string tbl_THTICKOPERACION = "thTickOperacion";
        public const string tbl_THTICKTRANSACCION = "thTickTransaccion";
        public const string tbl_THOPERADORVENTANILLA = "thOperadorVentanilla";
        public const string tbl_TMDISPLAY = "tmDisplay";
        public const string tbl_THPROGRESUMEN = "thProgResumen";
        public const string tbl_TPCONSOLIDAAGENCIA = "tpConsolidaAgencia";
        public const string tbl_THOPRENDIMIENTO = "thOpRendimiento";
        public const string tbl_THMOTIVOOCIO = "thMotivoOcio";
        public const string tbl_TXREPLICA = "txReplica";
        public const string tbl_TPTICKETADDIN = "tpTicketAddin";
        #endregion

        #region "Atributos de las tablas"
        /*tmArchivo*/
        public static string fld_CARCHIVO = "cArchivo";
        public static string fld_DARCHNOMBRE = "dArchNombre";
        public static string fld_NARCHTAMANO = "nArchTamano";
        public static string fld_NARCHDURACION = "nArchDuracion";
        public static string fld_DARCHFORMATO = "dArchFormato";
        public static string fld_FARCHEXPIRACION = "fArchExpiracion";
        public static string fld_CARCHCLIENTEMARK = "cArchClienteMark";
        public static string fld_BARCHCONSOLIDA = "bArchConsolida";
        public static string fld_NARCHPARTES = "nArchPartes";
        public static string fld_BARCHESTADO = "bArchEstado";
        public static string fld_FARCHCREACION = "fArchCreacion";
        /*tpTicket*/
        public static string fld_CTICKET = "cTicket";
        public static string fld_CTKTVENTANILLA = "cTkTVentanilla";
        public static string fld_CTKVENTANILLA = "cTkVentanilla";
        public static string fld_CTKAGENCIA = "cTkAgencia";
        public static string fld_CTKTTICKET = "cTkTTicket";
        public static string fld_FTKGENERADO = "fTkGenerado";
        public static string fld_HTKGENERADO = "hTkGenerado";
        public static string fld_HTKGENDERIV = "hTkGenDeriv";
        public static string fld_HTKASIGNADO = "hTkAsignado";
        public static string fld_HTKINIATENCION = "hTkIniAtencion";
        public static string fld_HTKFINATENCION = "hTkFinAtencion";
        public static string fld_HTKINIATENCION2 = "hTkIniAtencion2";
        public static string fld_HTKFINATENCION2 = "hTkFinAtencion2";
        public static string fld_CTKOPERADOR = "cTkOperador";
        public static string fld_BTKESTADO = "bTkEstado";
        public static string fld_DTKCODIGO = "dTkCodigo";
        public static string fld_NTKNUMCLIENTE = "nTkNumCliente";
        public static string fld_DTKNOMCLIENTE = "dTkNomCliente";
        public static string fld_HTKESPERAMINIMA = "hTkEsperaMinima";
        public static string fld_CTKDERIVADO = "cTkDerivado";
        public static string fld_BTKDERIVADO = "bTkDerivado";
        public static string fld_CTKSECTOR = "cTkSector";
        public static string fld_CTKTICKETERA = "cTkTicketera";
        public static string fld_NTKRELLAMADA = "nTkRellamada";
        public static string fld_BTKINTERNO = "bTkInterno";
        public static string fld_CTKGRUPO = "cTkGrupo";
        public static string fld_CTKCCLIENTE = "cTkCCliente";
        public static string fld_HTKDISPONIBLE = "hTkDisponible";
        public static string fld_DTKCODASOCIADO = "dTkCodAsociado";
        public static string fld_CTKREPLICACION = "cTkReplicacion";
        public static string fld_NTKDOCIDCLIENTE = "nTkDocIdCliente";
        public static string fld_CTKEQUIVALENCIA = "cTkEquivalencia";
        /*tpTickOperacion*/
        public static string fld_CTOTICKET = "cTOTicket";
        public static string fld_CTOAGENCIA = "cTOAgencia";
        public static string fld_FTOGENERADO = "fTOGenerado";
        public static string fld_CTONUMTRANS = "cTONumTrans";
        public static string fld_BTOESTADO = "bTOEstado";
        public static string fld_CTOOPERACION = "cTOOperacion";
        public static string fld_HTOINICIO = "hTOInicio";
        public static string fld_HTOFIN = "hTOFin";
        /*tpOperadorVentanilla*/
        public static string fld_COVVENTANILLA = "cOVVentanilla";
        public static string fld_COVSESION = "cOVSesion";
        public static string fld_COVAGENCIA = "cOVAgencia";
        public static string fld_FOVSESION = "fOVSesion";
        public static string fld_HOVINISESION = "hOVIniSesion";
        public static string fld_HOVFINSESION = "hOVFinSesion";
        public static string fld_BOVESTADO = "bOVEstado";
        public static string fld_COVOPERADOR = "cOVOperador";
        public static string fld_BOVTIPOSALIDA = "bOVTipoSalida";
        public static string fld_COVMOTIVO = "cOVMotivo";
        /*tmVentanilla*/
        public static string fld_CVENTANILLA = "cVentanilla";
        public static string fld_CVAGENCIA = "cVAgencia";
        public static string fld_CVTVENTANILLA = "cVTVentanilla";
        public static string fld_DVNOMBRE = "dVNombre";
        public static string fld_CVSECTOR = "cVSector";
        public static string fld_BVTASIGNATICKET = "bVTAsignaTicket";
        public static string fld_DVIP = "dVIP";
        public static string fld_BVCONSOLIDA = "bVConsolida";
        /*tmTVentanilla*/
        public static string fld_CTVENTANILLA = "cTVentanilla";
        public static string fld_DTVNOMBRE = "dTVNombre";
        public static string fld_BTVCONSOLIDA = "bTVConsolida";
        public static string fld_HTVSALIDA = "hTVSalida";       
        /*tmPais*/
        public static string fld_CPAIS = "cPais";
        public static string fld_DPNOMBRE = "dPNombre";
        public static string fld_BPCONSOLIDA = "bPConsolida";
        /*tmRegion*/
        public static string fld_CREGION = "cRegion";
        public static string fld_CRPAIS = "cRPais";
        public static string fld_DRNOMBRE = "dRNombre";
        public static string fld_BRCONSOLIDA = "bRConsolida";
        /*tmDepartamento*/
        public static string fld_CDEPARTAMENT = "cDepartament";
        public static string fld_CDEREGION = "cDeRegion";
        public static string fld_DDENOMBRE = "dDeNombre";
        public static string fld_BDECONSOLIDA = "bDeConsolida";
        /*tmZona*/
        public static string fld_CZONA = "cZona";
        public static string fld_CZODEPARTAMENT = "cZoDepartament";
        public static string fld_DZONOMBRE = "dZoNombre";
        public static string fld_BZOCONSOLIDA = "bZoConsolida";
        /*tmTipoAgencia*/
        public static string fld_CTAGENCIA = "cTAgencia";
        public static string fld_DTAGDESCRIPCION = "dTAgDescripcion";
        public static string fld_BTAGCONSOLIDA = "bTAgConsolida";
        /*tmGrupoAgencia*/
        public static string fld_CGAGENCIA = "cGAgencia";
        public static string fld_DGAGDESCRIPCION = "dGAgDescripcion";
        public static string fld_BGAGCONSOLIDA = "bGAgConsolida";
        /*tmTamanhoAgencia*/
        public static string fld_CTMAGENCIA = "cTMAgencia";
        public static string fld_DTMAGDESCRIPCION = "dTMAgDescripcion";
        public static string fld_BTMAGCONSOLIDA = "bTMAgConsolida";
        /*tmAgencia*/
        public static string fld_CAGENCIA = "cAgencia";
        public static string fld_CAGNOMBRE = "cAgNombre";
        public static string fld_DAGCODIGO = "dAgCodigo";
        public static string fld_CAGREGION = "cAgRegion";
        public static string fld_DAGDIRECCION = "dAgDireccion";
        public static string fld_DAGNOMBRE = "dAgNombre";
        public static string fld_BAGCONSOLIDA = "bAgConsolida";
        public static string fld_CAGPAIS = "cAgPais";
        public static string fld_CAGDEPARTAMENT = "cAgDepartament";
        public static string fld_CAGZONA = "cAgZona";
        public static string fld_BAGVENTINTEGRA = "bAgVentIntegra";
        public static string fld_NAGSEMAFORO = "nAgSemaforo";
        public static string fld_NAGRETATENCION = "nAgRetAtencion";
        public static string fld_DAGRUTAVIDEO = "dAgRutaVideo";
        public static string fld_HAGVALOPTESP = "hAgValOptEsp";
        public static string fld_HAGVALOPTATE = "hAgValOptAte";
        public static string fld_CAGTAGENCIA = "cAgTAgencia";
        public static string fld_CAGGAGENCIA = "cAgGAgencia";
        public static string fld_CAGTMAGENCIA = "cAgTmAgencia";
        /*tmSector*/
        public static string fld_CSAGENCIA = "cSAgencia";
        public static string fld_CSECTOR = "cSector";
        public static string fld_DSNOMBRE = "dSNombre";
        public static string fld_HSRETARDOATEN = "hSRetardoAten";
        public static string fld_BSCONSOLIDA = "bSConsolida";
        /*tmTTicket*/
        public static string fld_CTTICKET = "cTTicket";
        public static string fld_DTTNOMBRE = "dTTNombre";
        public static string fld_DTTTEXTO = "dTTTexto";
        public static string fld_HTTVALOPTESP = "hTTValOptEsp";
        public static string fld_HTTVALOPTATE = "hTTValOptAte";
        public static string fld_BTTCONSOLIDA = "bTTConsolida";
        public static string fld_CTTTVENTANILLA = "cTTTVentanilla";
        /*tmCatCliente*/
        public static string fld_CCCLIENTE = "cCCliente";
        public static string fld_DCCNOMBRE = "dCCNombre";
        public static string fld_BCCSINID = "bCCSinID";
        public static string fld_BCCCONSOLIDA = "bCCConsolida";
        /*tmUsuario*/
        public static string fld_CUSUARIO = "cUsuario";
        public static string fld_DUNOMBRE = "dUNombre";
        public static string fld_DUAPELLIDO = "dUApellido";
        public static string fld_DUCUENTA = "dUCuenta";
        public static string fld_BUCONSOLIDA = "bUConsolida";
        /*tmOperacion*/
        public static string fld_COPERACION = "cOperacion";
        public static string fld_DOCODIGO = "dOCodigo";
        public static string fld_DONOMBRE = "dONombre";
        public static string fld_COTVENTANILLA = "cOTVentanilla";
        public static string fld_BOCONSOLIDA = "bOConsolida";
        public static string fld_HOVALOPTESP = "hOValOptEsp";
        public static string fld_HOVALOPTATE = "hOValOptAte";
        public static string fld_NONIVEL = "nONivel";
        public static string FLD_COPERACIONPADRE = "cOperacionPadre";
        /*tmMotivos*/
        public static string fld_CMOTIVO = "cMotivo";
        public static string fld_DMODESCRIPCION = "dMoDescripcion";
        public static string fld_BMOCONSOLIDA = "bMoConsolida";
        /*txParamGeneral*/
        public static string fld_NPMPORCRAPIDEZ = "nPMPorcRapidez";
        public static string fld_NPMPORCTIEMPOACTIVO = "nPMPorcTiempoActivo";
        public static string fld_NPMFACTORTIEMPOACTIVO = "nPMFactorTiempoActivo";
        public static string fld_NPMFACTORRAPIDEZ = "nPMFactorRapidez";
        public static string fld_NPMMETAPRODUCTIVIDAD = "nPMMetaProductividad";
        public static string fld_NPMMETAATENCION = "nPMMetaAtencion";
        public static string fld_NPMCONSOLIDA = "bPMConsolida";
        public static string fld_NPGTIEMMINATEN = "nPGTiemMinAten";
        /*tmTipoDia*/
        public static string fld_CTIPODIA = "cTipoDia";
        public static string fld_FTDFECHA = "fTDFecha";
        public static string fld_BTDTIPO = "bTDTipo";
        public static string fld_BTDCONSOLIDA = "bTDConsolida";
        /*tmCalificacion*/
        public static string fld_CCALIFICACION = "cCalificacion";
        public static string fld_CCAGENCIA = "cCAgencia";
        public static string fld_CCZONA = "cCZona";
        public static string fld_DCDESCRIPCION = "dCDescripcion";
        public static string fld_NCPORCENTAJE = "nCPorcentaje";
        public static string fld_NCPORCMAX = "nCPorcMax";
        public static string fld_BCCONSOLIDA = "bCConsolida";
        /*txMetasParticulares*/
        public static string fld_CMPAGENCIA = "cMPAgencia";
        public static string fld_NMPPORCRAPIDEZ = "nMPPorcRapidez";
        public static string fld_NMPPORCTIEMPOACTIVO = "nMPPorcTiempoActivo";
        public static string fld_NMPPRODUCTIVIDAD = "nMPProductividad";
        public static string fld_NMPATENCION = "nMPAtencion";
        public static string fld_BMPCONSOLIDA = "bMPConsolida";
        /*txMetasZona*/
        public static string fld_CMZONA = "cMZona";
        public static string fld_NMZPORCRAPIDEZ = "nMZPorcRapidez";
        public static string fld_NMZPORCTIEMPOACTIVO = "nMZPorcTiempoActivo";
        public static string fld_NMZPRODUCTIVIDAD = "nMZProductividad";
        public static string fld_NMZATENCION = "nMZAtencion";
        public static string fld_NMZCONSOLIDA = "bMZConsolida";
        /*txTiempoEstandar*/
        public static string fld_CTIEMPOESTANDAR = "cTiempoEstandar";
        public static string fld_CTETAGENCIA = "cTETAgencia";
        public static string fld_DTECODTX = "dTECodTx";
        public static string fld_NTETIEMPO = "nTETiempo";
        public static string fld_BTECONSOLIDA = "bTEConsolida";
        /*tmClienteMarketing*/
        public static string fld_CCLIENTEMARK = "cClienteMark";
        public static string fld_DCMNOMBRE = "dCMNombre";
        public static string fld_BCMCONSOLIDA = "bCMConsolida";
        /*tmTiempo*/
        public static string fld_FTFECHA = "fTFecha";
        public static string fld_CTIEMPO = "cTiempo";
        public static string fld_DTDIA = "dTDia";
        public static string fld_DTMES = "dTMes";
        public static string fld_DTANO = "dTAno";
        public static string fld_NTDIADEMES = "nTDiaDeMes";
        public static string fld_NTSEMDEANO = "nTSemDeAno";
        public static string fld_NTMESDEANO = "nTMesDeAno";
        /*tpTickTransaccion*/
        public static string fld_CTTNUMTRANS = "cTTNumTrans";
        public static string fld_CTTAGENCIA = "cTTAgencia";
        public static string fld_FTTGENERADO = "fTTGenerado";
        public static string fld_CTTTICKET = "cTTTicket";
        public static string fld_DTTTICKET = "dTTTicket";
        public static string fld_NTTTICKET = "nTTTicket";
        public static string fld_CTTOPERACION = "cTTOperacion";
        public static string fld_HTTINICIO = "hTTInicio";
        public static string fld_HTTFIN = "hTTFin";
        public static string fld_NTTTIEMPO = "nTTTiempo";
        public static string fld_CTTUSUARIO = "cTTUsuario";
        public static string fld_CTTREPLICACION = "cTTReplicacion";
        /*tpProgramacionLog*/
        public static string fld_CPROGRAMACION = "cProgramacion";
        public static string fld_CPAGENCIA = "cPAgencia";
        public static string fld_CPPROGRAMDIA = "cPProgramDia";
        public static string fld_FPAPARICION = "fPAparicion";
        public static string fld_HPINIAPARICION = "hPIniAparicion";
        public static string fld_HPFINAPARICION = "hPFinAparicion";
        public static string fld_BPTPROGRAMDIA = "bPTProgramDia";
        public static string fld_BPCAPACITACION = "bPCapacitacion";
        public static string fld_CPARCHIVO = "cPArchivo";
        public static string fld_CPSECUENCIA = "cPSecuencia";
        public static string fld_CPUSUARIO = "cPUsuario";
        public static string fld_CPREPLICACION = "cPReplicacion";
        public static string fld_DPCANAL = "dPCanal";
        /*tpProgramacion*/
        public static string fld_CPDISPLAY = "cPDisplay";
        public static string fld_NPORDEN = "nPOrden";
        /*txParamGeneral*/
        public static string fld_BPGTIPOSERVIDOR = "bPGTipoServidor";
        /*tmSeverCentral*/
        public static string fld_HSCCDINICIO = "hSCCDInicio";
        public static string fld_BSCCDTIPOPROG = "bSCCDTipoProg";
        public static string fld_DSCCRUTACONSOLIDAEXE = "dSCCDRutaConsolidaExe";
        public static string fld_DSCCRUTALOGCONSOLIDADOR = "dSCCDRutaLogConsolidador";
        /*tpPerfilServicios*/
        public static string fld_CPERFILSERVICIO = "cPerfilServicio";
        public static string fld_DPSNOMBRE = "dPSNombre";
        public static string fld_NPSMFRECUENCIAMONITOREO = "nPSMFrecuenciaMonitoreo";
        public static string fld_HPSMINICIO = "hPSMInicio";
        public static string fld_HPSMFIN = "hPSMFin";
        public static string fld_NPSMDISPLAYTIEMPO = "nPSMdisplayTiempo";
        public static string fld_DPSMRUTALOGMONITOREO = "dPSMRutaLogMonitoreo";
        public static string fld_DPSRRUTASERVER = "dPSRRutaServer";
        public static string fld_NPSRFRECUENCIAREPLICACION = "nPSRFrecuenciaReplicacion";
        public static string fld_DPSRHORASAREPLICAR = "dPSRHorasAReplicar";
        public static string fld_DPSRRUTALOGREPLICACION = "dPSRRutaLogReplicacion";
        public static string fld_HPSCDINICIO = "hPSCDInicio";
        public static string fld_BPSCDTIPOPROG = "bPSCDTipoProg";
        public static string fld_DPSCDRUTACONSOLIDAEXE = "dPSCDRutaConsolidaExe";
        public static string fld_DPSCDRUTALOGCONSOLIDADOR = "dPSCDRutaLogConsolidador";
        public static string fld_DPSACARSEPARATOR = "dPSACarSeparator";
        public static string fld_NPSAFRECUENCIAALARMA = "nPSAFrecuenciaAlarma";
        public static string fld_DPSARUTALOGALARMA = "dPSARutaLogAlarma";
        public static string fld_DPSCSERVSMTP = "dPSCServSMTP";
        public static string fld_NPSCPUERTOCORREO = "nPSCPuertoCorreo";
        public static string fld_DPSCUSUARIO = "dPSCUsuario";
        public static string fld_DPSCPASSWORD = "dPSCPassword";
        public static string fld_DPSCCUENTAALARMA = "dPSCCuentaAlarma";
        public static string fld_NPSCFRECUENCIACORREO = "nPSCFrecuenciaCorreo";
        public static string fld_DPSCRUTALOGCORREO = "dPSCRutaLogCorreo";
        public static string fld_DPSCSUBJECT = "dPSCSubject";
        public static string fld_BPSCBODY = "bPSCBody";
        public static string fld_DPSTAHORAS = "dPSTAHoras";
        public static string fld_DPSTARUTALOGTRANSFERENCIA = "dPSTARutaLogTransferencia";
        /*taPerfilAgencia*/
        public static string fld_CPAPERFILSERVICIO = "cPAPerfilServicio";
        public static string fld_CPAAGENCIA = "cPAAgencia";
        /*tpConsolida*/
        public static string fld_FPROCESO = "fProceso";
        public static string fld_BESTADO = "bEstado";
        public static string fld_DDESCRIPCION = "dDescripcion";
        /*txRegistro*/
        public const string fld_CREGISTRO = "cRegistro";
        public const string fld_DREGNOMBRE = "dRegNombre";
        public const string fld_DREGCHARCLAVE = "dRegCharClave";
        public const string fld_DREGULTIMACLAVE = "dRegUltimaClave";
        /*txRegistroCampo*/
        public const string fld_CRCAMPO = "cRCampo";
        public const string fld_CRCREGISTRO = "cRCRegistro";
        public const string fld_DRCNOMBRE = "dRCNombre";
        public const string fld_NRCLONGITUD = "nRCLongitud";
        public const string fld_BRCPRIMARIO = "bRCPrimario";
        /*thTickResumen*/
        public const string fld_CTRAGENCIA = "cTRAgencia";
        public const string fld_CTRUSUARIO = "cTRUsuario";
        public const string fld_CTRTVENTANILLA = "cTRTVentanilla";
        public const string fld_CTRVENTANILLA = "cTRVentanilla";
        public const string fld_CTRTICKET = "cTRTicket";
        public const string fld_CTRTIEMPO = "cTRTiempo";
        public const string fld_DTRCODIGO = "dTRCodigo";
        public const string fld_FTRGENERADO = "fTRGenerado";
        public const string fld_BTRESTADO = "bTREstado";
        public const string fld_HTRGENERADO = "hTRGenerado";
        public const string fld_HTRASIGNADO = "hTRAsignado";
        public const string fld_HTRINIATENCION = "hTRIniAtencion";
        public const string fld_HTRFINATENCION = "hTRFinAtencion";
        public const string fld_HTRINIATENCION2 = "hTRIniAtencion2";
        public const string fld_HTRFINATENCION2 = "hTRFinAtencion2";
        public const string fld_NTRNUMTARJETA = "nTRNumTarjeta";
        public const string fld_DTRNOMCLIENTE = "dTRNomCliente";
        public const string fld_BTRDERIVADO = "bTRDerivado";
        public const string fld_CTRDERIVADO = "cTRDerivado";
        public const string fld_NTRTIEMPOESPE = "nTRTiempoEspe";
        public const string fld_NTRTIEMPOATEN = "nTRTiempoAten";
        public const string fld_NTRTIEMPOSERV = "nTRTiempoServ";
        public const string fld_NTRINTEHORAGENE = "nTRInteHoraGene";
        public const string fld_NTRINTEHORAASIG = "nTRInteHoraAsig";
        public const string fld_NTRINTEHORAINIC = "nTRInteHoraInic";
        public const string fld_NTRINTEHORAFIN = "nTRInteHoraFin";
        public const string fld_CTRTTICKET = "cTRTTicket";
        public const string fld_BTRINTERNO = "bTRInterno";
        public const string fld_DTRUSUARIO = "dTRUsuario";
        public const string fld_CTRGRUPO = "cTRGrupo";
        public const string fld_CTRCCLIENTE = "cTRCCliente";
        public const string fld_CTRTICKETERA = "cTRTicketera";
        public const string fld_DTRCODASOCIADO = "dTRCodAsociado";
        public const string fld_CTRSECTOR = "cTRSector";
        public const string fld_HTRESPERAMINIMA = "hTREsperaMinima";
        /*thTickAnula*/
        public const string fld_CTAAGENCIA = "cTAAgencia";
        public const string fld_CTATVENTANILLA = "cTATVentanilla";
        public const string fld_CTATICKET = "cTATicket";
        public const string fld_DTACODIGO = "dTACodigo";
        public const string fld_CTATIEMPO = "cTATiempo";
        public const string fld_CTAUSUARIO = "cTAUsuario";
        public const string fld_CTAVENTANILLA = "cTAVentanilla";
        public const string fld_CTATTICKET = "cTATTicket";
        public const string fld_DTAUSUARIO = "dTAUsuario";
        public const string fld_FTAGENERADO = "fTAGenerado";
        public const string fld_BTAESTADO = "bTAEstado";
        public const string fld_HTAGENERADO = "hTAGenerado";
        public const string fld_HTAASIGNADO = "hTAAsignado";
        public const string fld_HTAINIATENCION = "hTAIniAtencion";
        public const string fld_HTAFINATENCION = "hTAFinAtencion";
        public const string fld_HTAINIATENCION2 = "hTAIniAtencion2";
        public const string fld_HTAFINATENCION2 = "hTAFinAtencion2";
        public const string fld_NTANUMTARJETA = "nTANumTarjeta";
        public const string fld_DTANOMCLIENTE = "dTANomCliente";
        public const string fld_HTAESPERAMINIMA = "hTAEsperaMinima";
        public const string fld_BTADERIVADO = "bTADerivado";
        public const string fld_CTADERIVADO = "cTADerivado";
        public const string fld_NTATIEMPOESPE = "nTATiempoEspe";
        public const string fld_NTATIEMPOATEN = "nTATiempoAten";
        public const string fld_NTATIEMPOSERV = "nTATiempoServ";
        public const string fld_NTAINTEHORAGENE = "nTAInteHoraGene";
        public const string fld_NTAINTEHORAASIG = "nTAInteHoraAsig";
        public const string fld_NTAINTEHORAINIC = "nTAInteHoraInic";
        public const string fld_NTAINTEHORAFIN = "nTAInteHoraFin";
        public const string fld_BTAINTERNO = "bTAInterno";
        public const string fld_CTAGRUPO = "cTAGrupo";
        public const string fld_CTACCLIENTE = "cTACCliente";
        public const string fld_CTATICKETERA = "cTATicketera";
        public const string fld_DTACODASOCIADO = "dTACodAsociado";
        public const string fld_CTASECTOR = "cTASector";
        /*thTickOperacion*/
        public const string fld_CTOHTICKET = "cTOHTicket";
        public const string fld_CTOHAGENCIA = "cTOHAgencia";
        public const string fld_FTOHGENERADO = "fTOHGenerado";
        public const string fld_CTOHNUMTRANS = "cTOHNumTrans";
        public const string fld_CTOHOPERACION = "cTOHOperacion";
        public const string fld_HTOHINICIO = "hTOHInicio";
        public const string fld_HTOHFIN = "hTOHFin";
        public const string fld_CTOHUSUARIO = "cTOHUsuario";
        public const string fld_NTOHTIEMPOATEN = "nTOHTiempoAten";
        /*thOperadorVentanilla*/
        public const string fld_CHOVAGENCIA = "cHOVAgencia";
        public const string fld_CHOVSESION = "cHOVSesion";
        public const string fld_NHOVINTEHORA = "nHOVInteHora";
        public const string fld_FHOVSESION = "fHOVSesion";
        public const string fld_CHOVVENTANILLA = "cHOVVentanilla";
        public const string fld_CHOVOPERADOR = "cHOVOperador";
        public const string fld_HHOVINISESION = "hHOVIniSesion";
        public const string fld_HHOVFINSESION = "hHOVFinSesion";
        public const string fld_BHOVTIPOSALIDA = "bHOVTipoSalida";
        public const string fld_CHOVMOTIVO = "cHOVMotivo";
        /*tmDisplay*/
        public const string fld_CDISPLAY = "cDisplay";
        public const string fld_DDNOMBRE = "dDNombre";
        /*thProgResumen*/
        public const string fld_CPRAGENCIA = "cPRAgencia";
        public const string fld_CPRPROGRAMACION = "cPRProgramacion";
        public const string fld_FPRAPARICION = "fPRAparicion";
        public const string fld_HPRINIAPARICION = "hPRIniAparicion";
        public const string fld_HPRFINAPARICION = "hPRFinAparicion";
        public const string fld_NPRTIEMPO = "nPRTiempo";
        public const string fld_BPRTPROGRAMDIA = "bPRTProgramDia";
        public const string fld_DPRCANAL = "dPRCanal";
        public const string fld_BPRCAPACITACION = "bPRCapacitacion";
        public const string fld_CPRARCHIVO = "cPRArchivo";
        public const string fld_CPRCLIENTEMARK = "cPRClienteMark";
        public const string fld_CPRUSUARIO = "cPRUsuario";
        public const string fld_DPRDISPLAY = "dPRDisplay";
        public const string fld_FPRARCHEXPIRACION = "fPRArchExpiracion";
        /*thOpRendimiento*/
        public const string fld_CORAGENCIA = "cORAgencia";
        public const string fld_CORTVENTANILLA = "cORTVentanilla";
        public const string fld_FORDIA = "fORDia";
        public const string fld_COROPERADOR = "cOROperador";
        public const string fld_HORINICIOSESION = "hORInicioSesion";
        public const string fld_HORFINSESION = "hORFinSesion";
        public const string fld_NORCANTATEN = "nORCantAten";
        public const string fld_HORTIEMACTIVO = "hORTiemActivo";
        public const string fld_HORTIEMEFECTIVO = "hORTiemEfectivo";
        public const string fld_NORPORCEFECTIVO = "nORPorcEfectivo";
        public const string fld_NORTIEMOCIO = "nORTiemOcio";
        public const string fld_HORTIEMPOSUSPENDIDO = "hORTiempoSuspendido";
        public const string fld_HORTIEMPOFUERA = "hORTiempoFuera";
        public const string fld_DORVENTANILLA = "dORVentanilla";
        public const string fld_CORVENTANILLA = "cORVentanilla";
        public const string fld_NORCANTABAN = "nORCantAban";
        public const string fld_HORTIEMMAXIMO = "hORTiemMaximo";
        public const string fld_NORTIEMLIBRE = "nORTiemLibre";
        /*thMotivoOcio*/
        public const string fld_CMOAGENCIA = "cMOAgencia";
        public const string fld_CMOOPERADOR = "cMOOperador";
        public const string fld_CMOVENTANILLA = "cMOVentanilla";
        public const string fld_CMOTVENTANILLA = "cMOTVentanilla";
        public const string fld_FMODIA = "fMODia";
        public const string fld_CMOMOTIVO = "cMOMotivo";
        public const string fld_HMOTIEMOCIO = "hMOTiemOcio";
        public const string fld_NMOCANTIDAD = "nMOCantidad";
        /*txReplica*/
        public const string fld_DREPTABLE = "dRepTable";
        public const string fld_CREPKEY = "cRepKey";
        public const string fld_BCDTIPOPROG = "CONSOLIDATIPOPROG";
        /*txConsolidaLog*/
        public const string fld_FCLOGPROCESO = "fCLogProceso";
        public const string fld_HCLOGPROCESO = "hCLogProceso";
        public const string fld_DCLOGPROCESO = "dCLogProceso";
        public const string fld_NCLOGREGLEIDO = "nCLogRegLeido";
        public const string fld_NCLOGREGRESULT = "nCLogRegResult";
        public const string fld_BCLOGERROR = "bCLogError";
        public const string fld_DCLOGERROR = "dCLogError";
        public const string fld_BCLOGCORREGIDO = "bCLogCorregido";
        public const string fld_FCLOGEJECUCION = " fCLogEjecucion";
        public const string fld_CCLOGAGENCIA = " cCLogAgencia";

        #endregion

        /*tmCnfgAlarma*/
        public static string tlb_TMCNFGALARMA = "tmCnfgAlarma"; //JJ 20/12/2011
        public static string tlb_TMALARMA = "tmAlarma"; //JJ 20/12/2011

        public const string fld_CCAAGENCIA = "cCAAgencia"; //JJ
        public const string fld_CCAALARMA = "cCAAlarma"; //JJ
        public const string fld_CALARMA = "cAlarma"; //JJ
        public const string fld_DALAALARMA = "dALAlarma"; //JJ

    }
}
