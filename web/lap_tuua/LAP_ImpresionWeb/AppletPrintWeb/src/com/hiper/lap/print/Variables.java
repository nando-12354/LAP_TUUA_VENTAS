package com.hiper.lap.print;

/**
 *
 * @author ggarcia/Esteban Aliaga Geldres
 */
public class Variables {

    public static int FLAG_PRINTER_VOUCHER = 0;
    public static int FLAG_PRINTER_STICKER = 1;

    //Error al intentar abrir el puerto de la impresora
    public final static int FLAG_PRINTER_PORT_ERROR = -1;
    //Estados de la impresora de sticker
    public final static int FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE = 0;
    public final static int FLAG_PRINTER_STATUS_STICKER_OPERATIVE = 1;
    public final static int FLAG_PRINTER_STATUS_STICKER_HEADUP = 2;//No imprime
    public final static int FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE = 3;//No imprime
    public final static int FLAG_PRINTER_STATUS_STICKER_PAPER_OUT = 4;
    public final static int FLAG_PRINTER_STATUS_STICKER_RIBON_OUT = 5;//Hace la finta de imprimir, pero no tiene tinta, ojo el unico caso en donde esta mal pero sigue el foco verde estatico.
    //Estados de la impresora de voucher
    public final static int FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE = 0;
    public final static int FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE = 1;
    public final static int FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END = 2;
    public final static int FLAG_PRINTER_STATUS_VOUCHER_PAPER_END = 3;
    //Error General
    public final static int FLAG_PRINTER_ERROR = 99;

    /** Comandos de las impresoras */
    // Comando test impresora voucher
    public static byte[] COMMAND_PRINTER_VOUCHER = {(byte)0x10, (byte)0x04, (byte)0x04};
    // Comando test impresora sticker
    public static byte[] COMMAND_PRINTER_STICKER = {(byte)0x7E, (byte)0x48, (byte)0x53};
    // Comando para averiguar modelo de impresora stickers
    public static byte[] COMMAND_MODEL_PRINTER_STICKER = { (byte)0x7E, (byte)0x48, (byte)0x49 };
    // Comandos cortar papel impresora voucher
    public static byte[] COMMAND_PRINTER_VOUCHER_CUT_PAPER1 = {(byte)0x1B, (byte)0x4A, (byte)0xFF};
    public static byte[] COMMAND_PRINTER_VOUCHER_CUT_PAPER2 = {(byte)0x1B, (byte)0x69};

    //mensajes
    public static String PRINTER_MESSAGE = "Espere mientras el Sistema imprime los documentos solicitados...";
    public final static String MENSAJE_PROCESANDO = "Procesando...";
    public final static String MENSAJE_IMPRIMIENDO = "Imprimiendo...";

    //Parametros de entrada de applet:
    //********************************
    public final static String FlagImpresoraSticker = "flagImpSticker";
    public final static String ConfiguracionImpresoraSticker = "configImpSticker";
    public final static String FlagImpresoraVoucher = "flagImpVoucher";
    public final static String ConfiguracionImpresoraVoucher = "configImpVoucher";
    public final static String CopiasVoucher = "copiasVoucher";
    public final static String DataSticker = "dataSticker";
    //Si solo se imprime Voucher.
    public final static String DataVoucher = "dataVoucher";
    //Si se envia que imprima Stickers.
    public final static String XmlFormatoVoucher = "xmlFormatoVoucher";
    public final static String Nombre_Cajero = "nombre_Cajero";
    public final static String Imp_Precio = "imp_Precio";
    public final static String Dsc_Simbolo = "dsc_Simbolo";
    public final static String Descripcion_tipoticket = "descripcion_tipoticket";

    public final static String ListaCodigoTickets = "listaCodigoTickets";
    public final static int Longitud_Ticket = 16;

    public final static String Compania = "compania";
    public final static String NroVuelo = "nrovuelo";

    public final static String Tam_Ticket = "tam_Ticket";


    public final static String Modelo_TLP2844Z = "TLP2844-Z";
    public final static String Modelo_GX420t = "GX420t";
    public final static String Modelo_105SL200 = "105SL-200";
}
