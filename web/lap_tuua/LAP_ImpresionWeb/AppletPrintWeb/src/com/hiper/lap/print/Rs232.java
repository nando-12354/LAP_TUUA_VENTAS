package com.hiper.lap.print;

import gnu.io.CommPortIdentifier;
import gnu.io.NoSuchPortException;
import gnu.io.PortInUseException;
import gnu.io.SerialPort;
import gnu.io.SerialPortEvent;
import gnu.io.SerialPortEventListener;
import gnu.io.UnsupportedCommOperationException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.Timer;
import java.util.TimerTask;
import java.util.TooManyListenersException;

/**
 *
 * @author GGARCIA/ESTEBAN ALIAGA GELDRES
 */
public class Rs232 implements SerialPortEventListener {

    // configuracion impresora de voucher [ejemplo: COM1,9600,N,8,1]
    private String[] configImpVoucher;
    // configuracion impresora de sticker [ejemplo: COM3,9600,N,8,1]
    private String[] configImpSticker;

    /** Variables impresora de voucher */
    private SerialPort serialPortVoucher;
    private OutputStream outputStreamVoucher;
    private InputStream inputStreamVoucher;
    private Timer timerVoucher;
    private boolean bTimeOutVoucher;
    private boolean isReceivedDataVoucher;

    /** Variables impresora de sticker */
    private SerialPort serialPortSticker;
    private OutputStream outputStreamSticker;
    private InputStream inputStreamSticker;
    private Timer timerSticker;
    private boolean bTimeOutSticker;
    private boolean isReceivedDataSticker;

    /** Variables comunes */
    private byte[] lectorBuffer;

    private static final int ETX = 3;
    private static final int STX = 2;
    private static final int LF = 10;
    private static final int CR = 13;

    //---- EAG 12/02/2010
    private int Max_LF;
    //Valores por Defecto de la impresora modelo TLP2844-Z
    private int timeForPrintSticker = 2000;
    private int RibbonOutByteValue = 48;
    private int RibbonOkByteValue = 49;
    //---- EAG 12/02/2010

    private static final int TAM_PAQ_SERIAL = 1024;

    private static int timeout_lectura_voucher = 5000;
    private static int timeout_lectura_sticker = 5000;

    public Rs232() {
        configImpVoucher = null;
        configImpSticker = null;
    }

    // <editor-fold defaultstate="collapsed" desc="StartPort">
    public boolean StartPort(int tipo) {
        String puerto = "";

        // <editor-fold defaultstate="collapsed" desc="VOUCHER">
        if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
            puerto = getConfigImpVoucher()[0];// se obtiene el puerto [ejemplo: COM3]
            // Buscar el puerto de la impresora Vocuher
            CommPortIdentifier portIdVoucher;
            try {
                portIdVoucher = CommPortIdentifier.getPortIdentifier(puerto);
            } catch (NoSuchPortException ex) {
                System.err.println("Error al intentar obtener el puerto serial para la impresora de voucher. Puerto no encontrado.");
                return false;
            }
            //Abre el puerto de la impresora Voucher - Lo USA.
            try {
                serialPortVoucher = (SerialPort) portIdVoucher.open("PrintVoucher", 1000);//timeout_conexion_puerto=1 seg.
            } catch (PortInUseException ex) {
                System.err.println("Error al intentar abrir el puerto serial para la impresora de voucher. Puerto en uso.");
                return false;
            }
            try {
                // get the outputstream and inputstream para el puerto de la impresora voucher
                outputStreamVoucher = serialPortVoucher.getOutputStream();
                inputStreamVoucher = serialPortVoucher.getInputStream();
            } catch (IOException ex) {
                System.err.println("Error al obtener el input/output del puerto serial para la impresora de voucher.");
                return false;
            }

            // agregar evento listener
            try {
                serialPortVoucher.addEventListener(this);
            } catch (TooManyListenersException ex2) {
                System.err.println("Error al asignar el EventListener del puerto serial para la impresora de voucher.");
                return false;
            }
            try {
                // activa eventos de notificacion

                // notificador DATA_AVAILABLE
                serialPortVoucher.notifyOnDataAvailable(true);

                // notificador OUTPUT_BUFFER_EMPTY
                serialPortVoucher.notifyOnOutputEmpty(true);
            } catch (Exception ex3) {
                System.err.println("Error al activar eventos de notificacion del puerto serial para la impresora de voucher.");
                return false;
            }

            try {
                // set port parameters
                serialPortVoucher.setSerialPortParams(
                        Integer.parseInt(getConfigImpVoucher()[1]),
                        Integer.parseInt(getConfigImpVoucher()[3]),
                        Integer.parseInt(getConfigImpVoucher()[4]),
                        SerialPort.PARITY_NONE);
            } catch (UnsupportedCommOperationException ex4) {
                System.err.println("Error al setear los parametros del puerto serial para la impresora de voucher. UnsupportedCommOperationException.");
                return false;
            }

            //timerVoucher = new Timer();
        } // </editor-fold>
        // <editor-fold defaultstate="collapsed" desc="STICKER">
        else if (tipo == Variables.FLAG_PRINTER_STICKER) {
            puerto = getConfigImpSticker()[0];// se obtiene el puerto [ejemplo: COM3]
            // Buscar el puerto de la impresora Sticker
            CommPortIdentifier portIdSticker;
            try {
                portIdSticker = CommPortIdentifier.getPortIdentifier(puerto);
            } catch (NoSuchPortException ex) {
                System.err.println("Error al intentar obtener el puerto serial para la impresora de sticker. Puerto no encontrado.");
                return false;
            }
            //Abre el puerto de la impresora Sticker - Lo USA.
            try {
                serialPortSticker = (SerialPort) portIdSticker.open("PrintSticker", 1000);//timeout_conexion_puerto=1 seg.
            } catch (PortInUseException ex) {
                System.err.println("Error al intentar abrir el puerto serial para la impresora de sticker. Puerto en uso.");
                return false;
            }
            try {
                // get the outputstream and inputstream para el puerto de la impresora sticker
                outputStreamSticker = serialPortSticker.getOutputStream();
                inputStreamSticker = serialPortSticker.getInputStream();
            } catch (IOException ex) {
                System.err.println("Error al obtener el input/output del puerto serial para la impresora de sticker.");
                return false;
            }

            // agregar evento listener
            try {
                serialPortSticker.addEventListener(this);
            } catch (TooManyListenersException ex2) {
                System.err.println("Error al asignar el EventListener del puerto serial para la impresora de sticker.");
                return false;
            }
            try {
                // activa eventos de notificacion

                // notificador DATA_AVAILABLE
                serialPortSticker.notifyOnDataAvailable(true);

                // notificador OUTPUT_BUFFER_EMPTY
                serialPortSticker.notifyOnOutputEmpty(true);
            } catch (Exception ex3) {
                System.err.println("Error al activar eventos de notificacion del puerto serial para la impresora de sticker.");
                return false;
            }

            try {
                // set port parameters
                serialPortSticker.setSerialPortParams(
                        Integer.parseInt(getConfigImpSticker()[1]),
                        Integer.parseInt(getConfigImpSticker()[3]),
                        Integer.parseInt(getConfigImpSticker()[4]),
                        SerialPort.PARITY_NONE);
            } catch (UnsupportedCommOperationException ex4) {
                System.err.println("Error al setear los parametros del puerto serial para la impresora de sticker. UnsupportedCommOperationException.");
                return false;
            }

            //timerSticker = new Timer();
        } // </editor-fold>
        else {
            return false;
        }
        return true;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="KillPort">
    public boolean KillPort(int tipo) {
        try {
            if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                if (inputStreamVoucher != null) {
                    inputStreamVoucher.close();
                }
                if (outputStreamVoucher != null) {
                    outputStreamVoucher.close();
                }
                if (serialPortVoucher != null) {
                    serialPortVoucher.removeEventListener();
                    serialPortVoucher.close();
                }
            } else if (tipo == Variables.FLAG_PRINTER_STICKER) {
                if (inputStreamSticker != null) {
                    inputStreamSticker.close();
                }
                if (outputStreamSticker != null) {
                    outputStreamSticker.close();
                }
                if (serialPortSticker != null) {
                    serialPortSticker.removeEventListener();
                    serialPortSticker.close();
                }
            }
        } catch (Exception ex) {
            System.err.println("Rs232.KillPort(" + tipo +") - Error: " + ex.getMessage());
            ex.printStackTrace();
            try {
                if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                    if (serialPortVoucher != null) {
                        serialPortVoucher.close();
                    }
                } else if (tipo == Variables.FLAG_PRINTER_STICKER) {
                    if (serialPortSticker != null) {
                        serialPortSticker.close();
                    }
                }
            } catch (Exception ex2) {
                System.err.println("Rs232.KillPort(" + tipo +") - Error al reintentar: " + ex2.getMessage());
                ex2.printStackTrace();
            }
            return false;
        }
        return true;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="serialEvent">
    public  void serialEvent(SerialPortEvent event) {
        switch (event.getEventType()) {

            case SerialPortEvent.DATA_AVAILABLE:// we get here if data has been received
                try {

                    // si el evento es del imputStreamSticker
                    if (inputStreamSticker != null && inputStreamSticker.available() > 0) {
                        // <editor-fold defaultstate="collapsed" desc="inputStreamSticker.available()">
//System.out.println("serialEvent Sticker: " + Thread.currentThread().getName());
                        ByteBuffer buffer = ByteBuffer.allocate(TAM_PAQ_SERIAL);
                        int c = -1;

                        //METODO CON PROTOCOLO
                        while(!bTimeOutSticker){
                            c = inputStreamSticker.read();
//System.out.println("Byte leido 1: " + c);
                            if(c == -1 || (c != STX)){
                                continue;
                            }
                            buffer.put((byte) c);
                            break;
                        }
                        if(c==STX /*&& !bTimeOutSticker*/){
                            int numLF = 0;
                            while(!bTimeOutSticker && numLF<Max_LF){
                                c = inputStreamSticker.read();
//System.out.println("Byte leido 2: " + c);
                                if(c==-1){
                                    continue;
                                }
                                else if(c==LF){
                                    numLF++;
                                }
                                buffer.put((byte) c);
                            }
                            if(!bTimeOutSticker){
                                timerSticker.cancel();
                                lectorBuffer = new byte[buffer.position()];
                                buffer.position(0);
                                buffer.get(lectorBuffer);
                                isReceivedDataSticker = true;
                            }
                        }
                        //METODO SIN PROTOCOLO (No esta probado)
                        /*
                        while(!bTimeOutSticker){
                            c = inputStreamSticker.read();
                            if(c == -1){
                                break;
                            }
                            buffer.put((byte) c);
                        }
                        if(!bTimeOutSticker){
                            timerSticker.cancel();
                            lectorBuffer = new byte[buffer.position()];
                            buffer.position(0);
                            buffer.get(lectorBuffer);
                            isReceivedDataSticker = true;
                        }
                        */

                        // </editor-fold>
                    }

                    // si el evento es del imputStreamVoucher
                    if (inputStreamVoucher != null && inputStreamVoucher.available() > 0) {
//System.out.println("serialEvent Voucher: " + Thread.currentThread().getName());
                        //Al testearla, la impresora de voucher solo retorna un byte.
                        lectorBuffer = new byte[1];
                        inputStreamVoucher.read(lectorBuffer);
//System.out.println("Byte leido:" + lectorBuffer[0]);
                        timerVoucher.cancel();
                        isReceivedDataVoucher = true;
                    }

                } catch (IOException ex) {
                    System.err.println("Rs232.serialEvent() - Error: " + ex.getMessage());
                    ex.printStackTrace();
                }

                break;
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="escribirPuerto">

    /* Metodo que previo al envio a imprimir testea la impresora, retornando dicho valor.
     * Deprecated.
     */
    public int escribirPuerto(String[] textoImp, int tipo){
        int respuesta = Variables.FLAG_PRINTER_ERROR;
        try {
            if (tipo == Variables.FLAG_PRINTER_STICKER) {
                respuesta = verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER);
                System.out.println("Estado impresora de sticker:" + respuesta);
                if(respuesta == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE){
                    escribirTexto(textoImp, Variables.FLAG_PRINTER_STICKER);
                }
            } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                respuesta = verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER);
                System.out.println("Estado impresora de voucher:" + respuesta);
                if(respuesta == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE){
                    escribirTexto(textoImp, Variables.FLAG_PRINTER_VOUCHER);
                }
            }
        } catch (Exception ex) {
            System.out.println("Rs232.escribirPuerto(" + tipo + ") Error " + ex.getMessage());
            respuesta = Variables.FLAG_PRINTER_ERROR;
        }
        return respuesta;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="escribirTexto">
    private int cantidadStickersImpresos = 0;

    public int getCantidadStickersImpresos(){
        return this.cantidadStickersImpresos;
    }

    public void setCantidadStickersImpresos(int cantidadStickersImpresos){
        this.cantidadStickersImpresos = cantidadStickersImpresos;
    }

    public void escribirTexto(String[] textoImp, int tipo) {
        try {
            if (tipo == Variables.FLAG_PRINTER_STICKER) {
                int i = 0;
                //Recordar que el primer elemento a escribir al puerto serial de la impresora de stickers es la cabecera,
                //y no ninguno de los stickers en si, que seria el detalle (Nro de lineas a escribir al puerto serial = 1 + Nro. Stickers a imprimir).
                for (; i < textoImp.length; i++) {
                    System.out.println("TextoSticker " + i + ":" + textoImp[i]);

                    /*
                    //---EAG 09/02/2010
                    //Impresora de Sticker a primer instancia no soporta caracteres extraños
                    textoImp[i] = textoImp[i].replaceAll("á", "a");
                    textoImp[i] = textoImp[i].replaceAll("é", "e");
                    textoImp[i] = textoImp[i].replaceAll("í", "i");
                    textoImp[i] = textoImp[i].replaceAll("ó", "o");
                    textoImp[i] = textoImp[i].replaceAll("ú", "u");

                    textoImp[i] = textoImp[i].replaceAll("Á", "A");
                    textoImp[i] = textoImp[i].replaceAll("É", "E");
                    textoImp[i] = textoImp[i].replaceAll("Í", "I");
                    textoImp[i] = textoImp[i].replaceAll("Ó", "O");
                    textoImp[i] = textoImp[i].replaceAll("Ú", "U");

                    textoImp[i] = textoImp[i].replaceAll("ü", "u");
                    textoImp[i] = textoImp[i].replaceAll("Ü", "U");

                    textoImp[i] = textoImp[i].replaceAll("ñ", "n");
                    textoImp[i] = textoImp[i].replaceAll("Ñ", "N");

                    textoImp[i] = textoImp[i].replaceAll("€", "E");
                    textoImp[i] = textoImp[i].replaceAll("¥", "Y");
                    textoImp[i] = textoImp[i].replaceAll("£", "L");
                    //---EAG 09/02/2010
                    */

                    //outputStreamSticker.write(textoImp[i].getBytes());


                    //---EAG 09/02/2010 Mejorado
                    byte[] dataEnvioTexto = textoImp[i].getBytes("ISO-8859-1");

                    String hexEnvioTexto = ByteArrayToHexString(dataEnvioTexto);

                    hexEnvioTexto = hexEnvioTexto.replaceAll("FC ", "81 ");// ü
                    hexEnvioTexto = hexEnvioTexto.replaceAll("DC ", "9A ");// Ü
                    hexEnvioTexto = hexEnvioTexto.replaceAll("A3 ", "9C ");// £
                    hexEnvioTexto = hexEnvioTexto.replaceAll("A2 ", "BD ");// ¢
                    hexEnvioTexto = hexEnvioTexto.replaceAll("A5 ", "BE ");// ¥
                    //--
                    hexEnvioTexto = hexEnvioTexto.replaceAll("A1 ", "AD ");// ¡
                    hexEnvioTexto = hexEnvioTexto.replaceAll("BF ", "A8 ");// ¿
                    //--
                    hexEnvioTexto = hexEnvioTexto.replaceAll("E1 ", "A0 ");// á
                    hexEnvioTexto = hexEnvioTexto.replaceAll("E9 ", "82 ");// é
                    hexEnvioTexto = hexEnvioTexto.replaceAll("ED ", "A1 ");// í
                    hexEnvioTexto = hexEnvioTexto.replaceAll("F3 ", "A2 ");// ó
                    hexEnvioTexto = hexEnvioTexto.replaceAll("FA ", "A3 ");// ú
                    hexEnvioTexto = hexEnvioTexto.replaceAll("C1 ", "B5 ");// Á
                    hexEnvioTexto = hexEnvioTexto.replaceAll("C9 ", "90 ");// É
                    hexEnvioTexto = hexEnvioTexto.replaceAll("CD ", "D6 ");// Í
                    hexEnvioTexto = hexEnvioTexto.replaceAll("D3 ", "E0 ");// Ó
                    hexEnvioTexto = hexEnvioTexto.replaceAll("DA ", "E9 ");// Ú
                    hexEnvioTexto = hexEnvioTexto.replaceAll("F1 ", "A4 ");// ñ
                    hexEnvioTexto = hexEnvioTexto.replaceAll("D1 ", "A5 ");// Ñ
                    
                    //hexEnvioTexto = hexEnvioTexto.replaceAll("3F ", "D5 ");   //En .NET es "80", "D5"   ---> No imprime el €, imprime un palito.
                    hexEnvioTexto = hexEnvioTexto.replaceAll("3F ", "45 ");     //En .NET es "80", "45"   ---> Imprime E

                    //hexEnvioTexto = "1B 74 13 " + hexEnvioTexto;

                    byte[] dataAEnviar = HexStringToByteArray(hexEnvioTexto);

                    outputStreamSticker.write(dataAEnviar);
                    //---EAG 09/02/2010 Mejorado

                    try{
                        Thread.sleep(timeForPrintSticker);//Esto para que no envie los comandos a la impresora, sin siquiera que haya terminado de imprimir el primer ticket.
                        //Es un retardo para poder hacer un test objetivo.
                    }
                    catch(InterruptedException ex){

                    }

                    int reintentos = 0;
                    int resultadoTesteo;
                    boolean estadoImpresoraSticker = false;
                    while (true) {
                        if ((resultadoTesteo = verificarEstadoImpresoraRT(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                            reintentos++;
                            if (reintentos >= 2) {
                                System.out.println("RS232.escribirTexto() - Se testeo incorrectamente la Impresora de Sticker en el reintento " + (reintentos - 1) + " - Retorno: " + resultadoTesteo + ".");
                                break;
                            }
                            System.out.println("RS232.escribirTexto() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " + resultadoTesteo + ".");
                            try {
                                Thread.sleep(250);
                            } catch (InterruptedException ex) {
                            }
                        } else {
                            System.out.println("RS232.escribirTexto() - Se testeo correctamente la Impresora de Sticker.");
                            estadoImpresoraSticker = true;
                            break;
                        }
                    }
                    if(!estadoImpresoraSticker){
                        //no seguir imprimiendo.
                        if(i>0){//Aunque el comando de cabecera no afecta pero porseacaso. Osea aqui nunca va a entrar con i=0 (siempre y cuando testee previo impresion, e impida impresion si testeo da como fallido impresora).
                            if(resultadoTesteo != Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT){
                                i = i-1;//Aqui entra se supone por el unico error de falta de papel. Logicamente nadie deberia alzar la cabecera o ponerlo en modo pause.
                            }
                        }
                        break;
                    }
                }
                if(i >= textoImp.length){
                    i = i-1;
                }
                //setear la cantidad de stickers impresos.
                setCantidadStickersImpresos(i);
                System.out.println("Tickets Impresos: " + i);
            } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                String texto;
                for (int i = 0; i < textoImp.length; i++) {
                    System.out.println("TextoVoucher " + i + ":" + textoImp[i]);

                    if (textoImp[i].length()==4 && textoImp[i].substring(0, 4).equals("{CP}"))
                        cortarPapel();
                    else{

                        texto = textoImp[i] + "\n";

                        //outputStreamVoucher.write(texto.getBytes());

                        /*
                        //---PARA QUE SOPORTE EL INGRESO DEL SIMBOLO DE EURO
                        if(texto.contains("€")){
                            byte[] data = HexStringToByteArray("1B 74 13");
                            outputStreamVoucher.write(data);
                            int indexEuro = -1;

                            while((indexEuro = texto.indexOf("€"))>=0)
                            {
                                outputStreamVoucher.write(texto.substring(0, indexEuro).getBytes());
                                data = HexStringToByteArray("D5");
                                outputStreamVoucher.write(data);
                                texto = texto.substring(indexEuro + 1);
                            }

                            outputStreamVoucher.write(texto.getBytes());

                        }
                        else{
                            outputStreamVoucher.write(texto.getBytes());
                        }
                        //---
                        */

                        byte[] dataEnvioTexto = texto.getBytes("ISO-8859-1");

                        String hexEnvioTexto = ByteArrayToHexString(dataEnvioTexto);

                        hexEnvioTexto = hexEnvioTexto.replaceAll("FC ", "81 ");// ü
                        hexEnvioTexto = hexEnvioTexto.replaceAll("DC ", "9A ");// Ü
                        hexEnvioTexto = hexEnvioTexto.replaceAll("A3 ", "9C ");// £
                        hexEnvioTexto = hexEnvioTexto.replaceAll("A2 ", "BD ");// ¢
                        hexEnvioTexto = hexEnvioTexto.replaceAll("A5 ", "BE ");// ¥
                        //--
                        hexEnvioTexto = hexEnvioTexto.replaceAll("A1 ", "AD ");// ¡
                        hexEnvioTexto = hexEnvioTexto.replaceAll("BF ", "A8 ");// ¿
                        //--
                        hexEnvioTexto = hexEnvioTexto.replaceAll("E1 ", "A0 ");// á
                        hexEnvioTexto = hexEnvioTexto.replaceAll("E9 ", "82 ");// é
                        hexEnvioTexto = hexEnvioTexto.replaceAll("ED ", "A1 ");// í
                        hexEnvioTexto = hexEnvioTexto.replaceAll("F3 ", "A2 ");// ó
                        hexEnvioTexto = hexEnvioTexto.replaceAll("FA ", "A3 ");// ú
                        hexEnvioTexto = hexEnvioTexto.replaceAll("C1 ", "B5 ");// Á
                        hexEnvioTexto = hexEnvioTexto.replaceAll("C9 ", "90 ");// É
                        hexEnvioTexto = hexEnvioTexto.replaceAll("CD ", "D6 ");// Í
                        hexEnvioTexto = hexEnvioTexto.replaceAll("D3 ", "E0 ");// Ó
                        hexEnvioTexto = hexEnvioTexto.replaceAll("DA ", "E9 ");// Ú
                        hexEnvioTexto = hexEnvioTexto.replaceAll("F1 ", "A4 ");// ñ
                        hexEnvioTexto = hexEnvioTexto.replaceAll("D1 ", "A5 ");// Ñ
                        hexEnvioTexto = hexEnvioTexto.replaceAll("3F ", "D5 ");// €     //En .NET es "80", "D5"

                        hexEnvioTexto = "1B 74 13 " + hexEnvioTexto;

                        byte[] dataAEnviar = HexStringToByteArray(hexEnvioTexto);

                        outputStreamVoucher.write(dataAEnviar);

                    }
                }
                //cortarPapel();
            }
        } catch (Exception ex) {
            System.err.println("Rs232.escribirTexto(" + tipo + ") - Error: " + ex.getMessage());
            ex.printStackTrace();
        }
    }

    /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
    /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
    /// <returns> Returns an array of bytes. </returns>
    private byte[] HexStringToByteArray(String s)
    {
      s = s.replace(" " , "");
      byte[] buffer = new byte[s.length() / 2];
      for (int i = 0; i < s.length(); i += 2){
		//buffer[i / 2] = (byte)Byte.parseByte(s.substring(i, i+2), 16);//Falla por:byte max = Byte.MAX_VALUE; cuando son valores ejem: A0, FF, EC

          int g = Integer.parseInt(s.substring(i, i+2),16); //parses hex data into integers. If tried with Byte.parseByte exception is thrown.
            //if(g>127)
            //g = g - 256;//convert to -127->128 values
          String gg = Integer.toHexString(Integer.parseInt(s.substring(i, i+2), 16));
			//buffer[i / 2] = (byte)(g & 0xff);
			buffer[i / 2] = (byte)(g);//Ojo: (byte)255 = (byte)(-1) = -1
			//buffer[i / 2] = (byte)Integer.parseInt(s.substring(i, i+2), 16);//Metodo Directo

			//byte bb = Byte.parseByte("dd", 16);

			byte[] byteArray = { (byte)255, (byte)254, (byte)253, (byte)252, (byte)251, (byte)250 };

      }
      return buffer;
    }

    /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
    /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
    /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
    private String ByteArrayToHexString(byte[] data)
    {
      StringBuilder sb = new StringBuilder(data.length * 3);
		for (byte b : data){
			//sb.append(PadRight(PadLeft( Integer.toString(b, 16), 2, "0"), 3, " "));
			sb.append(PadRight(PadLeft( Integer.toString(b & 0xff, 16), 2, "0"), 3, " "));
			//sb.append(PadRight(PadLeft( Integer.toString((b & 0xff) + 0x100), 2, "0"), 3, " "));
			//sb.append(PadRight(PadLeft( Integer.toString((b & 0xff) + 0x100, 16).substring(1), 2, "0"), 3, " "));

			//sb.append(PadRight(PadLeft( Integer.toHexString(b, 16), 2, "0"), 3, " "));
      }
      return sb.toString().toUpperCase();
    }

    private String PadLeft(String str, int totalWidth, String paddingChar){
        while(str.length()<totalWidth){
            str = paddingChar + str;
        }
        return str;
    }

    private String PadRight(String str, int totalWidth, String paddingChar){
        while(str.length()<totalWidth){
            str = str + paddingChar;
        }
        return str;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="cortarPapel">
    /**
     * Metodo que corta el papel (Usado en la impresora de voucher).
     */
    private void cortarPapel() {

        try {
            outputStreamVoucher.write(Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER1);
            outputStreamVoucher.write(Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER2);

        } catch (Exception ex) {
            System.err.println("Rs232.cortarPapel - Error: " + ex.getMessage());
            ex.printStackTrace();
        }

    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="verificarEstadoImpresora">
    /**
     * Metodo que verifica el estado de la impresora.
     * @param tipo  tipo de la impresora [Voucher/Sticker]
     * @return
     *      tipo: Sticker
     *      0  = IMPRESORA STICKER NO OPERATIVA
     *      1  = IMPRESORA STICKER OPERATIVA
     *      2  = IMPRESORA STICKER HEAD UP
     *      3  = IMPRESORA STICKER PAUSE MODE
     *      4  = IMPRESORA STICKER SIN PAPEL
     *      5  = IMPRESORA STICKER SIN RIBON
     *      99 = ERROR GENERAL
     *
     *      tipo: Voucher
     *      0  = IMPRESORA VOUCHER NO OPERATIVA
     *      1  = IMPRESORA VOUCHER OPERATIVA
     *      2  = IMPRESORA VOUCHER CON POCO PAPEL
     *      3  = IMPRESORA VOUCHER SIN PAPEL
     *      99 = ERROR GENERAL
     */
    public int verificarEstadoImpresora(int tipo) {
        int respuesta = Variables.FLAG_PRINTER_ERROR;

        try {
            if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                lectorBuffer = null;
                bTimeOutVoucher = false;
                isReceivedDataVoucher = false;
                outputStreamVoucher.write(Variables.COMMAND_PRINTER_VOUCHER);// enviar comando de verificacion de estado

                // activar timer voucher
                timerVoucher = new Timer();
                timerVoucher.schedule(new TimerTask() {

                    @Override
                    public void run() {
                        ejecutarTimerVoucher();
                    }
                }, timeout_lectura_voucher);

                // sale cuando recibe respuesta de impresora de voucher, o cuando se cumple el timeout
                while (!bTimeOutVoucher && !isReceivedDataVoucher) {
                    try{
                        Thread.sleep(10);
                    }
                    catch(InterruptedException ex){

                    }
                }

                if(isReceivedDataVoucher){
                    respuesta = interpretarRespuesta(tipo);
                }
                else {//Salio por Timeout.
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                }

            } else if (tipo == Variables.FLAG_PRINTER_STICKER) {
                //--- 12/02/2010
                int getModeloPrinter = GetModeloImpresora();
                if(getModeloPrinter==0)
                {
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                    return respuesta;
                }
                //--- 12/02/2010
                lectorBuffer = null;
                bTimeOutSticker = false;
                isReceivedDataSticker = false;
                Max_LF = 3;
                outputStreamSticker.write(Variables.COMMAND_PRINTER_STICKER);// enviar comando de verificacion de estado

                // activar timer sticker
                timerSticker = new Timer();
                timerSticker.schedule(new TimerTask() {

                    @Override
                    public void run() {
                        ejecutarTimerSticker();
                    }
                }, timeout_lectura_sticker);

                // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
                while (!bTimeOutSticker && !isReceivedDataSticker) {
                    try{
                        Thread.sleep(10);
                    }
                    catch(InterruptedException ex){

                    }
                }
                if(isReceivedDataSticker){
                    respuesta = interpretarRespuesta(tipo);
                }
                else {//Salio por Timeout.
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                }
            }

        } catch (Exception ex) {
            System.err.println("Rs232.verificarEstadoImpresora(" + tipo + ") - Error: " + ex.getMessage());
            ex.printStackTrace();
        }
        return respuesta;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="timers">
    private void ejecutarTimerSticker() {
        timerSticker.cancel();
        bTimeOutSticker = true;
    }

    private void ejecutarTimerVoucher() {
        timerVoucher.cancel();
        bTimeOutVoucher = true;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="interpretarRespuesta">
    private int interpretarRespuesta(int tipo) {
        int respuesta = Variables.FLAG_PRINTER_ERROR;

        if (tipo == Variables.FLAG_PRINTER_STICKER) {

            // Cuando la impresora esta apagada no devuelve data [impresora no operativa]
            if (lectorBuffer == null || lectorBuffer.length == 0) {
                respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;

            } else {
                System.out.println("VerificarEstadoImpresoraSticker -> lectorBuffer1:" + getStringFromLectorBuffer(lectorBuffer));
                System.out.println("VerificarEstadoImpresoraSticker -> lectorBuffer2:" + getBytesFromLectorBuffer(lectorBuffer));

                // [impresora sin papel]
                if ((byte) lectorBuffer[5] == (byte) 49) {
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_PAPER_OUT;

                // [impresora en Head Up]//Cuando esta rojo parpadeando (la cabecera esta levantada)
                } else if(lectorBuffer[43] == (byte) 49){
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_HEADUP;

                // [impresora en Pause mode]//Cuando esta verde parpadeando.
                } else if(lectorBuffer[7] == (byte) 49){
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE;

                // [impresora sin Ribbon]
                } else if(lectorBuffer[45] == (byte) RibbonOutByteValue){
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT;
                }
                else if((byte) lectorBuffer[5] == (byte) 48 && lectorBuffer[43] == (byte) 48 &&
                        lectorBuffer[7] == (byte) 48 && lectorBuffer[45] == (byte) RibbonOkByteValue){
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE;
                }
                else{
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                }
            }
        } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {

            // Cuando la impresora esta apagada no devuelve data [impresora no operativa]
            if (lectorBuffer == null || lectorBuffer.length == 0) {
                respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;

            } else {
                System.out.println("VerificarEstadoImpresoraVoucher -> lectorBuffer:" + getBytesDistinguishedFromLectorBuffer(lectorBuffer));
                // NOTA:
                // Para testear es necesario que cierre la puerta de la impresora para que se actualize su status.
                //
                // [impresora operativa]
                if ((byte) lectorBuffer[0] == 18) {
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE;
                // [se esta acabando papel]
                } else if(lectorBuffer[0] == 30){
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END;
                // [fin de papel]
                } else if (lectorBuffer[0] == 114 || lectorBuffer[0] == 126) {
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_END;
                // [impresora inoperativa]
                } else {
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                }
            }
        }
        return respuesta;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="Util Buffer">
    private String getBytesFromLectorBuffer(byte[] buffer){
        String ret = "";
        for(int i=0; i<buffer.length;i++){
            ret += buffer[i];
        }
        return ret;
    }

    private String getBytesDistinguishedFromLectorBuffer(byte[] buffer){
        String ret = "";
        for(int i=0; i<buffer.length;i++){
            ret += "[" + buffer[i] + "]";
        }
        return ret;
    }

    private String getStringFromLectorBuffer(byte[] buffer){
        return new String(buffer);
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="GetPortIdentifiers">
    public static ArrayList<String> GetPortIdentifiers() throws Exception {

        Enumeration ports;
        CommPortIdentifier portId;
        boolean allPorts = true;
        ArrayList<String> portList = new ArrayList<String>();
        try {
            if (allPorts) {
                ports = CommPortIdentifier.getPortIdentifiers();
                if (ports == null) {
                    System.out.println("No comm ports found!");
                    return null;
                }
                while (ports.hasMoreElements()) {
                    portId = (CommPortIdentifier) ports.nextElement();

                    if (portId.getPortType() == CommPortIdentifier.PORT_SERIAL && !portId.isCurrentlyOwned()) {
                        portList.add(portId.getName());
                    }
                }
            }
        } catch (Exception e) {
            throw e;

        }
        return portList;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="Propiedades">
    public String[] getConfigImpVoucher() {
        return configImpVoucher;
    }

    public String[] getConfigImpSticker() {
        return configImpSticker;
    }

    public void setConfigImpVoucher(String configImpVoucher) {
        this.configImpVoucher = configImpVoucher.split(",");
    }

    public void setConfigImpSticker(String configImpSticker) {
        this.configImpSticker = configImpSticker.split(",");
    }
    // </editor-fold>

    
    private int GetModeloImpresora()
    {
        try{
            lectorBuffer = null;
            bTimeOutSticker = false;
            isReceivedDataSticker = false;
            Max_LF = 1;
            outputStreamSticker.write(Variables.COMMAND_MODEL_PRINTER_STICKER);// enviar comando de ver modelo de impresora

            // activar timer sticker
            timerSticker = new Timer();
            timerSticker.schedule(new TimerTask() {

                @Override
                public void run() {
                    ejecutarTimerSticker();
                }
            }, timeout_lectura_sticker);

            // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
            while (!bTimeOutSticker && !isReceivedDataSticker)
            {
                try{
                    Thread.sleep(10);
                }
                catch(InterruptedException ex){

                }
            }
            if (isReceivedDataSticker)
            {
                String str_buffer = getStringFromLectorBuffer(lectorBuffer);
                System.out.println("GetModeloImpresoraSticker -> lectorBuffer1:" + str_buffer);
                System.out.println("GetModeloImpresoraSticker -> lectorBuffer2:" + getBytesFromLectorBuffer(lectorBuffer));
                int pos_1stComma = str_buffer.indexOf(",");
                if(pos_1stComma<1)
                    return 0;

                str_buffer = str_buffer.substring(1, pos_1stComma);

                //if(str_buffer.indexOf(Variables.Modelo_TLP2844Z)>-1)
                if(str_buffer.contains(Variables.Modelo_TLP2844Z))
                {
                    timeForPrintSticker = 2000;
                    RibbonOutByteValue = 48;
                    RibbonOkByteValue = 49;
                    return 1;
                }
                //else if (str_buffer.indexOf(Variables.Modelo_GX420t)>-1)
                else if (str_buffer.contains(Variables.Modelo_GX420t))
                {
                    timeForPrintSticker = 700;
                    RibbonOutByteValue = 49;
                    RibbonOkByteValue = 48;
                    return 1;
                }
                else if (str_buffer.contains(Variables.Modelo_105SL200))
                {
                    timeForPrintSticker = 450;
                    RibbonOutByteValue = 49;
                    RibbonOkByteValue = 48;
                    return 1;
                }
                else
                {
                    //No es un modelo correcto. (Se pone como inoperativo)
                }
            }
            else
            {
                //No devolvio data... impresora inoperativa!
            }
        }
        catch(Exception ex){
            System.err.println("Rs232.GetModeloImpresora - Error: " + ex.getMessage());
            ex.printStackTrace();
        }
        return 0;
    }


    // <editor-fold defaultstate="collapsed" desc="verificarEstadoImpresoraRT">
    /**
     * Metodo que verifica el estado de la impresora.
     * @param tipo  tipo de la impresora [Voucher/Sticker]
     * @return
     *      tipo: Sticker
     *      0  = IMPRESORA STICKER NO OPERATIVA
     *      1  = IMPRESORA STICKER OPERATIVA
     *      2  = IMPRESORA STICKER HEAD UP
     *      3  = IMPRESORA STICKER PAUSE MODE
     *      4  = IMPRESORA STICKER SIN PAPEL
     *      5  = IMPRESORA STICKER SIN RIBON
     *      99 = ERROR GENERAL
     *
     *      tipo: Voucher
     *      0  = IMPRESORA VOUCHER NO OPERATIVA
     *      1  = IMPRESORA VOUCHER OPERATIVA
     *      2  = IMPRESORA VOUCHER CON POCO PAPEL
     *      3  = IMPRESORA VOUCHER SIN PAPEL
     *      99 = ERROR GENERAL
     */
    public int verificarEstadoImpresoraRT(int tipo) {
        int respuesta = Variables.FLAG_PRINTER_ERROR;

        try {
            if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                lectorBuffer = null;
                bTimeOutVoucher = false;
                isReceivedDataVoucher = false;
                outputStreamVoucher.write(Variables.COMMAND_PRINTER_VOUCHER);// enviar comando de verificacion de estado

                // activar timer voucher
                timerVoucher = new Timer();
                timerVoucher.schedule(new TimerTask() {

                    @Override
                    public void run() {
                        ejecutarTimerVoucher();
                    }
                }, timeout_lectura_voucher);

                // sale cuando recibe respuesta de impresora de voucher, o cuando se cumple el timeout
                while (!bTimeOutVoucher && !isReceivedDataVoucher) {
                    try{
                        Thread.sleep(10);
                    }
                    catch(InterruptedException ex){

                    }
                }

                if(isReceivedDataVoucher){
                    respuesta = interpretarRespuesta(tipo);
                }
                else {//Salio por Timeout.
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                }

            } else if (tipo == Variables.FLAG_PRINTER_STICKER) {
                lectorBuffer = null;
                bTimeOutSticker = false;
                isReceivedDataSticker = false;
                Max_LF = 3;
                outputStreamSticker.write(Variables.COMMAND_PRINTER_STICKER);// enviar comando de verificacion de estado

                // activar timer sticker
                timerSticker = new Timer();
                timerSticker.schedule(new TimerTask() {

                    @Override
                    public void run() {
                        ejecutarTimerSticker();
                    }
                }, timeout_lectura_sticker);

                // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
                while (!bTimeOutSticker && !isReceivedDataSticker) {
                    try{
                        Thread.sleep(10);
                    }
                    catch(InterruptedException ex){

                    }
                }
                if(isReceivedDataSticker){
                    respuesta = interpretarRespuesta(tipo);
                }
                else {//Salio por Timeout.
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                }
            }

        } catch (Exception ex) {
            System.err.println("Rs232.verificarEstadoImpresoraRT(" + tipo + ") - Error: " + ex.getMessage());
            ex.printStackTrace();
        }
        return respuesta;
    }
    // </editor-fold>

}
