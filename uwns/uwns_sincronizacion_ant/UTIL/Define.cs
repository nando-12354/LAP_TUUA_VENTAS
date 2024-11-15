using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.UTIL
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

        //Identificadores de operacion
        public const string INICIO_TURNO = "IT";
        public const string INGRESO_CAJA = "IC";
        public const string EGRESO_CAJA = "EC";
        public const string CIERRE_TURNO = "CT";
        public const string COMPRA_MONEDA = "CM";
        public const string VENTA_MONEDA = "VM";

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
        public const string ASPX_CambiarContraseña = "CambiarContraseña.aspx";
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
        public const string EFECTIVO = "E";
        public const string TRANSAFERENCIA = "T";
        public const string CHEQUE = "C";

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
        public const string TIP_PAGO_CREDITO = "TIP_PAGO_CREDITO";
        public const string COD_PTOVENTA_WEB = "COD_PTOVENTA_WEB";
        public const string ATR_NRO_MIN_CONTINGENCIA = "ATR_NRO_MIN_CONTINGENCIA";
        public const string ATR_NRO_MAX_CONTINGENCIA = "ATR_NRO_MAX_CONTINGENCIA";
        public const string SERIE_CONTINGENCIA = "SERIE_CONTINGENCIA";
        public const string ESTADO_TICKET_PREEMITIDO = "ESTADO_TICKET_PREEMITIDO";
        public const string COD_ROL_SUPERVISOR = "COD_ROL_SUPERVISOR";
        public const string ID_PARAM_CIERRE_TURNO = "ID_PARAM_CIERRE_TURNO";
        public const string ID_PARAM_MAX_DESCUADRE = "ID_PARAM_MAX_DESCUADRE";
        public const string ESTADO_TICKET_EMITIDO = "ESTADO_TICKET_EMITIDO";
        public const string ATR_NRO_MIN_ATM = "ATR_NRO_MIN_ATM";
        public const string ATR_NRO_MAX_ATM = "ATR_NRO_MAX_ATM";
        public const string MOD_VENTA_ATM = "MOD_VENTA_ATM";
        public const string ATR_KEY_ENCRIPTA_ATM = "ATR_KEY_ENCRIPTA_ATM";
		public const string ESTADO_TICKET_EXTORNADO="ESTADO_TICKET_EXTORNADO";
        public const string ESTADO_TICKET_REHABILITADO = "ESTADO_TICKET_REHABILITADO";
        public const string TIP_COMPRA_MONEDA = "TIP_COMPRA_MONEDA";
        public const string TIP_VENTA_MONEDA = "TIP_VENTA_MONEDA";
        public const string ESTADO_TICKET_VENCIDO = "ESTADO_TICKET_VENCIDO";

        //mensajes labels
        public const string opeVentaCredito_msgLimMinTicket = "opeVentaCredito.msgLimMinTicket";
        public const string opeVentaCredito_msgLimMaxTicket = "opeVentaCredito.msgLimMaxTicket";
        public const string opeVentaCredito_msgTrxFail="opeVentaCredito.msgTrxFail";
        public const string opeCierreTurno_msgTrxFail = "opeCierreTurno.msgTrxFail";
        public const string opeGenContingencia_msgTrxFail="opeGenContingencia.msgTrxFail";
        public const string opeGenContingencia_msgMinTicket="opeGenContingencia.msgMinTicket";
        public const string opeGenContingencia_msgMaxTicket="opeGenContingencia.msgMaxTicket";

        //Agregado Eacuna
        public const string ASPX_Cfg_VerListaCampo = "Cfg_VerListaCampo.aspx";
        public const string ASPX_Ope_VerTasaCambio = "Ope_VerTasaCambio.aspx";
        public const string ASPX_Ope_VerPrecioTicket = "Ope_VerPrecioTicket.aspx";
        public const string OPC_INGRESAR = "Ingresar";

        //Logeo
        public const int TIME_INACTIVO = 120;
        public const int NUM_MAX_TICKET = 800;
        //SINCRONIZACION
        public const string TIMEOUT = "TIMEOUT";
        public const string TIPOSERVICIO = "TIPOSERVICIO";
        public const string CENTER_LOCAL_SERVICE = "1";
        public const string MASTER_SLAVES_SERVICE = "2";
        //KINZI
        public const string TIMEOUT_ATRIBUTOS = "TIMEOUT_ATRIBUTOS";
        public const string TIMEOUT_TICKET = "TIMEOUT_TICKET";
        public const string TIMEOUT_BP = "TIMEOUT_BP";
    }
}
