using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.EXTRANET.UTIL
{
    public class Define
    {
        //códigos de tipos de errores

        public const string ERR_000 = "000";

        public const string ERR_DEFAULT = "Solicitud no completada, informe al Administrador del Sistema";
        public const string ERR_FILEERRORTYPE = "Error en configuracion de archivo de manejo de errores";
        public const string ERR_FILEERRTYPENOTFOUND = "Archivo de manejo de errores no encontrado.";
        public const string ERR_001 = "001";
        public const string ERR_002 = "002";
        public const string ERR_003 = "003";
        public const string ERR_004 = "004";
        public const string ERR_005 = "005";
        public const string ERR_006 = "006";
        public const string ERR_007 = "007";
        public const string ERR_008 = "008";
        public const string ERR_009 = "009";
        public const string ERR_010 = "010";

        public const string ERR_700 = "700";
        public const string ERR_701 = "701";

        //CÓDIGOS DE MODULOS Y PROCESOS
        public const string VEN_OPERA = "VEN_OPERA";
        public const string VEN_TURNO = "VEN_TURNO";
        public const string VEN_PROC_ING = "VEN_PROC_ING";
        public const string VEN_PROC_EGR = "VEN_PROC_EGR";
        public const string VEN_PROC_CVM = "VEN_PROC_CVM";
        public const string VEN_PROC_VTN = "VEN_PROC_VTN";
        public const string VEN_PROC_VTM = "VEN_PROC_VTM";
        public const string VEN_PROC_CUA = "VEN_PROC_CUA";
        public const string VEN_PROC_CIE = "VEN_PROC_CIE";
        public const string VEN_PROC_CLV = "VEN_PROC_CLV";

        //módulos
        public const string VENTAS = "VENTAS";
        public const string ADMIN = "ADM";

        //códigos de conexiones
        public const string CONEXION = "conexion";
        public const string CLIENTEWS = "CWS";
        public const string NATIVO = "NAT";

        public const string CNX_01 = "01";
        public const string CNX_02 = "02";
        public const string CNX_03 = "03";
        public const string CNX_04 = "04";
        public const string CNX_05 = "05";
        public const string CNX_06 = "06";
        public const string CNX_07 = "07";
        public const string CNX_08 = "08";
        public const string CNX_09 = "09";
        public const string CNX_10 = "10";

        //Identificadores de operacion
        public const string INICIO_TURNO = "IT";
        public const string INGRESO_CAJA = "IC";
        public const string EGRESO_CAJA = "EC";
        public const string CIERRE_TURNO = "CT";
        public const string COMPRA_MONEDA = "CM";
        public const string VENTA_MONEDA = "VM";
        public const string TIPO_BANCO = "6";
        public const string MARGEN_CAJA = "MC";

        //Tipo vuelo
        public const string NACIONAL = "N";
        public const string INTERNACIONAL = "I";
        //Tipo Pasajero
        public const string ADULTO = "A";
        public const string INFANTE = "I";
        //Tipo Transbordo
        public const string NORMAL = "N";
        public const string TRANSFERENCIA = "T";

        //campos bd
        public const string COD_MODULO = "Cod_Modulo";


        //archivo paginas

        public const string ASPX_Login = "Login.aspx";
        public const string ASPX_index = "Principal.aspx";
        public const string ASPX_CerrarSesion = "CerrarSesion.aspx";
        public const string ASPX_CabeceraMenu = "CabeceraMenu.ascx";
        public const string ASPX_PiedePagina = "PiePagina.ascx";
        public const string ASPX_CambiarContrasena = "CambiarContrasena.aspx";
        public const string ASPX_SegVerUsuario = "Seg_VerUsuario.aspx";
        public const string ASPX_SegCrearUsuario = "Seg_CrearUsuario.aspx";
        public const string ASPX_SegModificarUsuario = "Seg_ModificarUsuario.aspx";
        public const string ASPX_SegVerRol = "Seg_VerRol.aspx";
        public const string ASPX_SegCrearRol = "Seg_CrearRol.aspx";
        public const string ASPX_SegModificarRol = "Seg_ModificarRol.aspx";
        public const string ASPX_SegEliminarRol = "Seg_EliminarRol.aspx";
        public const string ASPX_MntVerTipoTicket = "Mnt_VerTipoTicket.aspx";
        public const string ASPX_MntCrearTipoTicket = "Mnt_CrearTipoTicket.aspx";
        public const string ASPX_MntModificarTipoTicket = "Mnt_ModificarTipoTicket.aspx";
        public const string ASPX_MntPuntoVenta = "Mnt_PuntoVenta.aspx";
        public const string ASPX_MntCrearPuntoVenta = "Mnt_CrearPuntoVenta.aspx";
        public const string ASPX_MntModificarPuntoVenta = "Mnt_ModificarPuntoVenta.aspx";
        public const string ASPX_SegModalidadVenta = "Mnt_VerModalidadVenta.aspx";
        public const string ASPX_MntCrearModalidadVenta = "Mnt_CrearModalidadVenta.aspx";
        public const string ASPX_MntModificarModalidadVenta = "Mnt_ModificarModalidadVenta.aspx";
        public const string ASPX_MntTipoMonedas = "Mnt_TipoMonedas.aspx";
        public const string ASPX_MntCrearTipoMonedas = "Mnt_CrearTipoMonedas.aspx";
        public const string ASPX_MntModificarTipoMonedas = "Mnt_ModificarTipoMonedas.aspx";
        public const string ASPX_MntCrearCompania = "Mnt_CrearCompania.aspx";
        public const string ASPX_MntModificarCompania = "Mnt_ModificarCompania.aspx";
        public const string ASPX_CfgModificarListaCampo = "Cfg_EditarListaCampo.aspx";
        public const string ASPX_CfgCrearListaCampo = "Cfg_CrearListaCampo.aspx";
        public const string ASPX_CnsUsuarios = "Cns_Usuarios.aspx";
        public const string ASPX_CnsTurnos = "Cns_Turnos.aspx";
        public const string ASPX_CnsTicketxFecha = "Cns_TicketxFecha.aspx";
        public const string ASPX_CnsDetalleTurnos = "Cns_DetalleTurnos.aspx";
        public const string ASPX_CnsDetalleTicket = "Cns_DetalleTicket.aspx";
        public const string ASPX_CnsCompanias = "Cns_Companias.aspx";
        public const string ASPX_OpeCrearPrecioTicket = "Ope_CrearPrecioTicket.aspx";
        public const string ASPX_OpeCrearTasaCambio = "Ope_CrearTasaCambio.aspx";
        public const string ASPX_AlrModificarAlarma = "Alr_ModificarAlarma.aspx";
        public const string ASPX_AlrCrearAlarma = "Alr_CrearAlarma.aspx";
        public const string ASPX_AlrMonitorearAlarma = "Alr_MonitorearAlarma.aspx";
        public const string ASPX_AlrVerAlarma = "Alr_VerAlarma.aspx";
        public const string ASPX_AlrConsultarAlarma = "Alr_ConsultarAlarma.aspx";
        public const string ASPX_AlrEliminarAlarma = "Alr_EliminarAlarma.aspx";

        //Privados LAP

        public const string LAP_lblEmpresa = "Lima Airport Partners S.R.L. - Departamento de Sistemas";
        public const string LAP_lblDerechos = "Derechos Reservados - Copyright © 2009";

        //compania
        public const string TIPO_AEROLINEA = "1";
        public const string COMPANIA_ACTIVO = "1";
        //Tipo Cambio
        public const string TC_COMPRA = "C";
        public const string TC_VENTA = "V";

        //MONEDA NACIONAL
        public const string MONEDANAC = "MONEDANAC";
        public const string EST_MONEDA_ACTIVO = "EST_MONEDA_ACTIVO";

        //TIPO PAGO
        public const string TIP_PAGO_EFECTIVO = "E";
        public const string TIP_PAGO_CREDITO = "C";
        public const string TIP_PAGO_DEBITO = "D";
        public const string TIP_PAGO_TRANSF = "T";
        public const string TIP_PAGO_CHEQUE = "Q";
        public const string TIP_PAGO_CREDEFEC = "A";
        public const string TIP_PAGO_DEBIEFEC = "B";

        //Seguridad
        public const string PERMITIDO = "1";

        //Opciones
        public const string OPC_NUEVO = "Nuevo";
        public const string OPC_MODIFICAR = "Modificar";
        public const string OPC_ELIMINAR = "Eliminar";

        //campos properties
        public const string MOD_VENTA_NORMAL = "MOD_VENTA_NORMAL";
        public const string MOD_VENTA_MAS_CONT = "MOD_VENTA_MAS_CONT";
        public const string MOD_VENTA_MAS_CRED = "MOD_VENTA_MAS_CRED";
        public const string MOD_VENTA_BOARDING = "MOD_VENTA_BOARDING";
        public const string ATR_LIM_VENTA = "ATR_LIM_VENTA";
        public const string ATR_NRO_MIN_TICKET = "ATR_NRO_MIN_TICKET";
        public const string ATR_NRO_MAX_TICKET = "ATR_NRO_MAX_TICKET";
        public const string TIP_COMPANIA = "TIP_COMPANIA";
        public const string IP_PTO_VENTA = "IP_PTO_VENTA";
        public const string ID_PARAM_KEY = "ID_PARAM_KEY";
        public const string ID_PARAM_MIN_CLAVE = "ID_PARAM_MIN_CLAVE";
        public const string ID_PARAM_MAX_CLAVE = "ID_PARAM_MAX_CLAVE";
        public const string ID_PARAM_MAX_INTENTOS = "ID_PARAM_MAX_INTENTOS";
        public const string ID_PARAM_INACTIVIDAD = "ID_PARAM_INACTIVIDAD";
        public const string ID_PARAM_LONG_TICKET = "LNT";
        public const string COD_PTOVENTA_WEB = "COD_PTOVENTA_WEB";
        public const string ATR_NRO_MIN_CONTINGENCIA = "ATR_NRO_MIN_CONTINGENCIA";
        public const string ATR_NRO_MAX_CONTINGENCIA = "ATR_NRO_MAX_CONTINGENCIA";
        public const string SERIE_CONTINGENCIA = "SERIE_CONTINGENCIA";
        public const string ESTADO_TICKET_PREEMITIDO = "P";
        public const string COD_ROL_SUPERVISOR = "COD_ROL_SUPERVISOR";
        public const string ID_PARAM_CIERRE_TURNO = "ID_PARAM_CIERRE_TURNO";
        public const string ID_PARAM_MAX_DESCUADRE = "ID_PARAM_MAX_DESCUADRE";
        public const string ID_PARAM_DIAS_VIGENCIA = "ID_PARAM_DIAS_VIGENCIA";
        public const string ESTADO_TICKET_EMITIDO = "E";
        public const string ATR_NRO_MIN_ATM = "ATR_NRO_MIN_ATM";
        public const string ATR_NRO_MAX_ATM = "ATR_NRO_MAX_ATM";
        public const string MOD_VENTA_ATM = "MOD_VENTA_ATM";
        public const string ATR_KEY_ENCRIPTA_ATM = "ATR_KEY_ENCRIPTA_ATM";
        public const string ESTADO_TICKET_EXTORNADO = "X";
        public const string ESTADO_TICKET_REHABILITADO = "R";
        public const string ESTADO_TICKET_USADO = "U";
        public const string ESTADO_TICKET_ANULADO = "X";
        public const string TIP_ANULA_VENCIDO = "3";
        public const string TIP_COMPRA_MONEDA = "TIP_COMPRA_MONEDA";
        public const string TIP_VENTA_MONEDA = "TIP_VENTA_MONEDA";
        public const string ESTADO_TICKET_VENCIDO = "ESTADO_TICKET_VENCIDO";
        public const string ID_PARAM_IMPRESION = "ID_PARAM_IMPRESION";      // Identificador de impresion (GGarcia-20090907)
        public const string ID_PARAM_MAX_CLAVE_HIST = "ID_PARAM_MAX_CLAVE_HIST";
        public const string TAM_TICKET = "TAM_TICKET";
        // Tamaño del ticket (GGarcia-20090924)

        //mensajes labels
        public const string opeVentaCredito_msgLimMinTicket = "opeVentaCredito.msgLimMinTicket";
        public const string opeVentaCredito_msgLimMaxTicket = "opeVentaCredito.msgLimMaxTicket";
        public const string opeVentaCredito_msgTrxFail = "opeVentaCredito.msgTrxFail";
        public const string opeCierreTurno_msgTrxFail = "opeCierreTurno.msgTrxFail";
        public const string opeGenContingencia_msgTrxFail = "opeGenContingencia.msgTrxFail";
        public const string opeGenContingencia_msgMinTicket = "opeGenContingencia.msgMinTicket";
        public const string opeGenContingencia_msgMaxTicket = "opeGenContingencia.msgMaxTicket";

        //Agregado Eacuna
        public const string ASPX_Cfg_VerListaCampo = "Cfg_VerListaCampo.aspx";
        public const string ASPX_Ope_VerTasaCambio = "Ope_VerTasaCambio.aspx";
        public const string ASPX_Ope_VerPrecioTicket = "Ope_VerPrecioTicket.aspx";
        public const string OPC_INGRESAR = "Ingresar";


        //Logeo
        public const int TIME_INACTIVO = 120;
        public const int NUM_MAX_TICKET = 300;


        // parametros impresion
        public const string ID_PRINTER_FUNCTION_DDMMYYYY = "DDMMYYYY()";

        // Constantes de Impresion (GGarcia-20090907)        

        // Documentos de impresion
        public const string ID_PRINTER_DOCUM_CIERRETURNO_MN = "CUADRE_MN";
        public const string ID_PRINTER_DOCUM_CIERRETURNO_ME = "CUADRE_ME";

        // Voucher Ingreso de Caja
        public const string ID_PRINTER_DOCUM_INGRESOCAJA = "IngresoCaja";
        // Voucher Egreso de Caja
        public const string ID_PRINTER_DOCUM_EGRESOCAJA = "EgresoCaja";

        public const string ID_PRINTER_DOCUM_INICIOTURNO = "InicioTurno";

        //Voucher Venta Moneda
        public const string ID_PRINTER_DOCUM_VENTAMONEDA = "VentaMoneda";

        // Voucher Venta de Dolares
        public const string ID_PRINTER_DOCUM_VENTADOLARES = "VentaDolares";
        // Voucher Venta de Euros
        public const string ID_PRINTER_DOCUM_VENTAEUROS = "VentaEuros";

        //Voucher Compra Moneda
        public const string ID_PRINTER_DOCUM_COMPRAMONEDA = "CompraMoneda";

        // Voucher Compra de Dolares
        public const string ID_PRINTER_DOCUM_COMPRADOLARES = "CompraDolares";
        // Voucher Compra de Euros
        public const string ID_PRINTER_DOCUM_COMPRAEUROS = "CompraEuros";
        // Voucher Venta de Tickets
        public const string ID_PRINTER_DOCUM_VENTATICKETVOUCHER = "VentaTicketsVoucher";
        // Voucher Venta de Tickets Masiva a credito (utilizado en la Web)
        public const string ID_PRINTER_DOCUM_VENTATICKETMASIVACREDITO = "VentaTicketsMasivaCredito";
        // Voucher Venta de Tickets Masiva a credito (utilizado en la Web)
        public const string ID_PRINTER_DOCUM_VENTATICKETCONTINGENCIA = "VentaTicketsContingencia";
        // Voucher Extorno de Tickets
        public const string ID_PRINTER_DOCUM_EXTORNOTICKET = "ExtornoTicket";

        public const string ID_PRINTER_DOCUM_EXTORNOREHABILITACION = "ExtornoRehabilitacion";

        // Voucher Extorno de operaciones
        public const string ID_PRINTER_DOCUM_EXTORNOOPERACIONES = "ExtornoOperaciones";

        public const string ID_PRINTER_DOCUM_VENTATICKETSNORMAL = "VentaTicketsNormal";

        public const string ID_PRINTER_DOCUM_VENTATICKETSMASIVACONTADO = "VentaTicketsMasivaContado";

        // Sticker de Venta Tickets (moneda configurable)
        public const string ID_PRINTER_DOCUM_VENTATICKETSTICKER = "VentaTicketSticker";

        public const string ID_PRINTER_DOCUM_VENTATICKETCONTINGENCIASTICKER = "VentaTicketContingenciaSticker";

        // Sticker de Venta Tickets (soles)
        public const string ID_PRINTER_DOCUM_VENTATICKETSTICKERSOLES = "VentaTicketStickerSoles";
        // Sticker de Venta Tickets (dolares)
        public const string ID_PRINTER_DOCUM_VENTATICKETSTICKERDOLARES = "VentaTicketStickerDolares";
        // Sticker de Venta Tickets (euros)
        public const string ID_PRINTER_DOCUM_VENTATICKETSTICKEREUROS = "VentaTicketStickerEuros";

        //Voucher de Rehabilitacion Individual
        public const string ID_PRINTER_DOCUM_REHABILITACIONINDIVIDUAL = "RehabilitacionIndividual";

        //Voucher de Rehabilitacion Masiva
        public const string ID_PRINTER_DOCUM_REHABILITACIONMASIVA = "RehabilitacionMasiva";


        // parametros
        public const string ID_PRINTER_PARAM_CONFIG_IMPRESORA = "config_impresora";
        public const string ID_PRINTER_PARAM_CODIGO_CAJERO = "codigo_cajero";
        public const string ID_PRINTER_PARAM_NOMBRE_CAJERO = "nombre_cajero";
        public const string ID_PRINTER_PARAM_CODIGO_TURNO = "codigo_turno";
        public const string ID_PRINTER_PARAM_MONTO_DOLARES = "monto_dolares";
        public const string ID_PRINTER_PARAM_MONTO_SOLES = "monto_soles";
        public const string ID_PRINTER_PARAM_MONTO_EUROS = "monto_euros";
        public const string ID_PRINTER_PARAM_TIPO_CAMBIO = "tipo_cambio";
        public const string ID_PRINTER_PARAM_VUELTO = "vuelto";
        public const string ID_PRINTER_PARAM_FECHA_VENCIMIENTO = "fecha_vencimiento";
        public const string ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET = "descripcion_tipoticket";
        public const string ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET = "precio_unitario";
        public const string ID_PRINTER_PARAM_TOTAL_PAGAR = "total_pagar";
        public const string ID_PRINTER_PARAM_MONTO_PAGADO = "monto_pagado";
        public const string ID_PRINTER_PARAM_CODIGO_TICKET = "codigo_ticket";
        public const string ID_PRINTER_PARAM_CANTIDAD_TICKET = "cantidad_ticket";
        public const string ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL = "cantidad_subdetail";
        public const string ID_PRINTER_PARAM_FLAG_TICKET = "flag_ticket";
        public const string ID_ARCHIVO_VENTA = "NAV";
        public const string ID_PARAM_SEP_VENTAS = "SAV";

        public const string ListaCodigoTickets = "listaCodigoTickets";
        public const int Longitud_Ticket = 16;

        // llaves
        public const string ID_PRINTER_KEY_VOUCHER = "voucher";
        public const string ID_PRINTER_KEY_STICKER = "sticker";
        public const string ID_PRINTER_KEY_CODMONEDA = "codMoneda";

        // id impresora
        public const int ID_PRINTER_VOUCHER = 0;
        public const int ID_PRINTER_STICKER = 1;

        //Compañia

        public const int DigitosRuc = 11;
        public const int DigitosDni = 8;
        //public const int LONG_NUM_TICKET = 15;

        public const String COM_SERIAL_LECTOR = "COM_SERIAL_LECTOR";

        //Alarmas
        public const string ALR_EstadoAtendido = "2";

        //ARCHIVO VENTAS
        public const string TIP_VENTA_CAJA = "CON";
        public const string TIP_VENTA_CREDITO = "CRE";
        public const string TIP_VENTA_USO = "CRU";
        public const string TIP_VENTA_ATM = "ATM";
        public const string TIP_VENTA_BCBP = "BRD";
        public const string ID_PARAM_PATH_VENTAS = "ID_PARAM_PATH_VENTAS";

        //Mensajes Post Impresion
        public const String Impresion_StickerConVoucher = "Impresion_StickerConVoucher";
        public const String Impresion_SoloSticker = "Impresion_SoloSticker";
        public const String Impresion_SoloVoucher = "Impresion_SoloVoucher";

        public const String Salir_StickerConVoucher = "Salir_StickerConVoucher";
        public const String Salir_SoloSticker = "Salir_SoloSticker";
        public const String Salir_SoloVoucher = "Salir_SoloVoucher";

        public const String Error_StickerConVoucher = "Error_StickerConVoucher";
        public const String Error_SoloVoucher = "Error_SoloVoucher";
        public const String Error_SoloSticker = "Error_SoloSticker";

        public const String Error_NoControlado = "Error_NoControlado";

        public const string IP_LOCAL_HOST = "127.0.0.1";
        //Lista de Campos
        public const String NOM_CAMPO_LISTA_CAMPOS_LISTAPERIODO = "NOM_LC_LISTA_PERIODO";

        public const String ID_DELAY_MESES_ARCHIVAMIENTO = "ID_DELAY_MESES_ARCHIVAMIENTO";
        public const String CULTURENAME_FOR_MONTHSABREVIATED = "CULTURENAME_FOR_MONTHSABREVIATED";
        public const String NOM_LC_CODETAPA_ARCH = "NOM_LC_CODETAPA_ARCH";
        public const String ID_CONFIG_CONEXION_ARCHIVAMIENTO = "ID_CONFIG_CONEXION_ARCHIVAMIENTO";


        public const string ID_PARAM_VUELO_DEFAULT = "ID_PARAM_VUELO_DEFAULT";
        public const string COD_COMPANIA_DEFAULT = "COD_COMPANIA_DEFAULT";

        #region Additional Methods - kinzi
        //IDENTIFICADOR DE PARAMETROS GENERALES
        public const string ID_PARAM_LIMITE_GRILLA = "LG";
        public const string ID_FILTRO_TODOS = "< Todos >";
        public const string ID_TIPO_MODALIDAD_MASIVA_CREDITO = "4";

        public const string ERR_401 = "401";
        public const string ERR_402 = "402";
        public const string ERR_405 = "405";
        public const string ERR_510 = "510";
        public const string ERR_511 = "511";
        #endregion


        public const string ID_PROC_CAMBIO_GRUPO = "001E0003";

        public const string MODO_NORMAL_CONT = "0";
        public const string MODO_MASIVO_CONT = "1";

        public const decimal FACTOR_DECIMAL = 1.00M;
        public const string OPE_VENTA_VAL00 = "00";
        public const string OPE_VENTA_VAL01 = "01";
        public const int STRING_SIZE = 4000;
        public const int NUM_DECIMAL = 2;
        public const string TIP_ACTIVO = "1";
        public const string TIP_DOC_TICKET = "T";
        public const string TIP_DOC_BOARDING = "B";
        public const string BACK_SLASH = "\\";
        public const string TIP_OPE_MAX_DESCUADRE = "MD";
        public const string CAMPO_TIPOPAGO = "TipoPago";
        public const string CAMPO_TIPOPSJERO = "TipoPasajero";

        public enum centavos
        {
            TODO = 0,
            SOL = 1,
            DOL = 2
        }
    }
}
