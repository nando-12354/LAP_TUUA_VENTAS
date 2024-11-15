using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.PRINTER
{
    public class Defines
    {
        // parametros
        public const string ID_PRINTER_PARAM_CONFIG_IMPRESORA = "config_impresora";

        // parametros utilizados en el xml de impresion
        // codigo del cajero
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


        public const string ID_PRINTER_FUNCTION_DDMMYYYY = "DDMMYYYY()";

        //*****************
        //Retornos
        public const String Impresion_StickerConVoucher = "Impresion_StickerConVoucher";
        public const String Impresion_SoloSticker = "Impresion_SoloSticker";
        public const String Impresion_SoloVoucher = "Impresion_SoloVoucher";

        public const String Salir_StickerConVoucher = "Salir_StickerConVoucher";
        public const String Salir_SoloSticker = "Salir_SoloSticker";
        public const String Salir_SoloVoucher = "Salir_SoloVoucher";

        public const String Error_StickerConVoucher = "Error_StickerConVoucher";
        public const String Error_SoloSticker = "Error_SoloSticker";
        public const String Error_SoloVoucher = "Error_SoloVoucher";

        public const String Error_NoControlado = "Error_NoControlado";


        //Imagenes
        public const String PathBadStatus = "/resources/bad.jpg";
        public const String PathGoodStatus = "/resources/good.jpg";
        public const String PathWarningStatus = "/resources/Warning.jpg";
        public const String PathImageLoading = "/resources/ajax-loader.gif";
        public const String PathRefreshPorts = "/resources/Refresh.png";

        //Mensajes
        public const String MsgPrinterPortError = "Error en puerto asignado.";

        public const String MsgVoucherNotOperative = "Impresora inoperativa.";
        public const String MsgVoucherOperative = "Impresora operativa.";
        public const String MsgVoucherPaperNearEnd = "Impresora con poco papel.";
        public const String MsgVoucherPaperEnd = "Impresora sin papel.";

        public const String MsgStickerNotOperative = "Impresora inoperativa.";
        public const String MsgStickerOperative = "Impresora operativa.";
        public const String MsgStickerHeadUp = "Cabecera de Impresora abierta.";
        public const String MsgStickerPauseMode = "Impresora en modo Pause.";
        public const String MsgStickerPaperOut = "Impresora sin papel.";
        public const String MsgStickerRibbonOut = "Impresora sin Ribbon (papel de tinta).";

        public const String EQ = "EQ";
        public const String NE = "NE";
        public const String LE = "LE";
        public const String GE = "GE";
        public const String LT = "LT";
        public const String GT = "GT";
    }
}
