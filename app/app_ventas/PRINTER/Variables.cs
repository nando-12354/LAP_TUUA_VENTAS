using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.PRINTER
{
    public class Variables
    {
    public static int FLAG_PRINTER_VOUCHER = 0;
    public static int FLAG_PRINTER_STICKER = 1;

    //Error al intentar abrir el puerto de la impresora
    public const int FLAG_PRINTER_PORT_ERROR = -1;
    //Estados de la impresora de sticker
    public const int FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE = 0;
    public const int FLAG_PRINTER_STATUS_STICKER_OPERATIVE = 1;
    public const int FLAG_PRINTER_STATUS_STICKER_HEADUP = 2;//No imprime
    public const int FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE = 3;//No imprime
    public const int FLAG_PRINTER_STATUS_STICKER_PAPER_OUT = 4;
    public const int FLAG_PRINTER_STATUS_STICKER_RIBON_OUT = 5;//Hace la finta de imprimir, pero no tiene tinta, ojo el unico caso en donde esta mal pero sigue el foco verde estatico.
    //Estados de la impresora de voucher
    public const int FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE = 0;
    public const int FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE = 1;
    public const int FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END = 2;
    public const int FLAG_PRINTER_STATUS_VOUCHER_PAPER_END = 3;
    //Error General
    public const int FLAG_PRINTER_ERROR = 99;

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
    public const String MENSAJE_PROCESANDO = "Procesando...";
    public const String MENSAJE_IMPRIMIENDO = "Imprimiendo...";

    //Parametros de entrada de applet:
    //********************************
    public const String FlagImpresoraSticker = "flagImpSticker";
    public const String FlagImpresoraVoucher = "flagImpVoucher";
    public const String ConfiguracionImpresoraSticker = "configImpSticker";
    public const String ConfiguracionImpresoraVoucher = "configImpVoucher";
    public const String DataSticker = "dataSticker";
    public const String DataVoucher = "dataVoucher";
    public const String CopiasVoucher = "copiasVoucher";
    public const String XmlFormatoVoucher = "xmlFormatoVoucher";

    //public const String Nombre_Cajero = "nombre_cajero";
    //public const String Imp_Precio = "imp_Precio";
    //public const String Dsc_Simbolo = "dsc_Simbolo";
    //public const String Descripcion_tipoticket = "descripcion_tipoticket";
    
    ////Para poder retornar los stickers que no pudieron ser impresos.//YA NO SE USAN
    //public const String ListaCodigoTickets = "listaCodigoTickets";
    //public const String Tam_Ticket = "tam_Ticket";

    public const String Modelo_TLP2844Z = "TLP2844-Z";
    public const String Modelo_GX420t = "GX420t";
    public const String Modelo_105SL200 = "105SL-200";

    }

}
