package com.hiper.lap.print;

/**
 *
 * @author Esteban Aliaga Geldres
 */
public class Defines {

    public final static String ID_PRINTER_PARAM_NOMBRE_CAJERO = "nombre_cajero";
    public final static String ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET = "descripcion_tipoticket";
    public final static String ID_PRINTER_PARAM_CANTIDAD_TICKET = "cantidad_ticket";
    public final static String ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL = "cantidad_subdetail";
    public final static String ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET = "precio_unitario";
    public final static String ID_PRINTER_PARAM_SIMBOLO_MONEDA = "simbolo_moneda";
    public final static String ID_PRINTER_PARAM_TOTAL_PAGAR = "total_pagar";

    public final static String ID_PRINTER_PARAM_CODIGO_TICKET = "codigo_ticket";

    //Retornos
    public final static String Impresion_StickerConVoucher = "Impresion_StickerConVoucher";
    public final static String Impresion_SoloSticker = "Impresion_SoloSticker";
    public final static String Impresion_SoloVoucher = "Impresion_SoloVoucher";

    public final static String Salir_StickerConVoucher = "Salir_StickerConVoucher";
    public final static String Salir_SoloSticker = "Salir_SoloSticker";
    public final static String Salir_SoloVoucher = "Salir_SoloVoucher";

    public final static String Error_StickerConVoucher = "Error_StickerConVoucher";
    public final static String Error_SoloSticker = "Error_SoloSticker";
    public final static String Error_SoloVoucher = "Error_SoloVoucher";

    public final static String Error_NoControlado = "Error_NoControlado";

    //Imagenes
    public final static String PathBadStatus = "/resources/bad.jpg";
    public final static String PathGoodStatus = "/resources/good.jpg";
    public final static String PathWarningStatus = "/resources/Warning.jpg";
    public final static String PathImageLoading = "/resources/ajax-loader.gif";
    public final static String PathRefreshPorts = "/resources/Refresh.png";

    //Mensajes
    public final static String MsgPrinterPortError = "Error en puerto asignado.";
    
    public final static String MsgVoucherNotOperative = "Impresora inoperativa.";
    public final static String MsgVoucherOperative = "Impresora operativa.";
    public final static String MsgVoucherPaperNearEnd = "Impresora con poco papel.";
    public final static String MsgVoucherPaperEnd = "Impresora sin papel.";

    public final static String MsgStickerNotOperative = "Impresora inoperativa.";
    public final static String MsgStickerOperative = "Impresora operativa.";
    public final static String MsgStickerHeadUp = "Cabecera de Impresora abierta.";
    public final static String MsgStickerPauseMode = "Impresora en modo Pause.";
    public final static String MsgStickerPaperOut = "Impresora sin papel.";
    public final static String MsgStickerRibbonOut = "Impresora sin Ribbon (papel de tinta).";

    public final static String EQ = "EQ";
    public final static String NE = "NE";
    public final static String LE = "LE";
    public final static String GE = "GE";
    public final static String LT = "LT";
    public final static String GT = "GT";

}
