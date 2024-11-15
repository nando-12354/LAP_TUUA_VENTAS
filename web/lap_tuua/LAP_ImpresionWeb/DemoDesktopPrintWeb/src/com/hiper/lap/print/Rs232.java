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
import javax.swing.JOptionPane;

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
                System.err.println("Rs232.KillPort(" + tipo +") - Error al reintentar: " + ex.getMessage());
            }
            return false;
        }
        return true;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="serialEvent">
    public void serialEvent(SerialPortEvent event) {
        switch (event.getEventType()) {

            case SerialPortEvent.DATA_AVAILABLE:// we get here if data has been received
                try {

                    // si el evento es del imputStreamSticker
                    if (inputStreamSticker != null && inputStreamSticker.available() > 0) {
                        // <editor-fold defaultstate="collapsed" desc="inputStreamSticker.available()">

                        ByteBuffer buffer = ByteBuffer.allocate(TAM_PAQ_SERIAL);
                        int c = -1;

                        //METODO CON PROTOCOLO
                        while(!bTimeOutSticker){
                            c = inputStreamSticker.read();
                            if(c == -1 || (c != STX)){
                                continue;
                            }
                            buffer.put((byte) c);
                            break;
                        }
                        if(c==STX /*&& !bTimeOutSticker*/){
                            int numLF = 0;
                            while(!bTimeOutSticker && numLF<3){
                                c = inputStreamSticker.read();
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
                        //Al testearla, la impresora de voucher solo retorna un byte.
                        lectorBuffer = new byte[1];
                        inputStreamVoucher.read(lectorBuffer);
                        timerVoucher.cancel();
                        isReceivedDataVoucher = true;
                    }

                } catch (IOException ex) {
                    System.err.println("Rs232.serialEvent() - Error: " + ex.getMessage());
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
                    outputStreamSticker.write(textoImp[i].getBytes());

                    try{
                        Thread.sleep(2000);//Esto para que no envie los comandos a la impresora, sin siquiera que haya terminado de imprimir el primer ticket.
                        //Es un retardo para poder hacer un test objetivo.
                    }
                    catch(InterruptedException ex){

                    }

                    int reintentos = 0;
                    int resultadoTesteo;
                    boolean estadoImpresoraSticker = false;
                    while (true) {
                        if ((resultadoTesteo = verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
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
                JOptionPane.showMessageDialog(null, "Nro de Stickers impresos:" + i, "Impresión", JOptionPane.INFORMATION_MESSAGE);
            } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                String texto;
                for (int i = 0; i < textoImp.length; i++) {
                    texto = textoImp[i] + "\n";
                    System.out.println("TextoVoucher " + i + ":" + textoImp[i]);
                    outputStreamVoucher.write(texto.getBytes());
                }
                cortarPapel();
            }
        } catch (Exception ex) {
            System.err.println("Rs232.escribirTexto(" + tipo + ") - Error: " + ex.getMessage());
        }
    }

        public void escribirTexto2(String[] textoImp, int tipo) {
            try {
                if (tipo == Variables.FLAG_PRINTER_STICKER) {
                    int i = 0;
                    //Recordar que el primer elemento a escribir al puerto serial de la impresora de stickers es la cabecera,
                    //y no ninguno de los stickers en si, que seria el detalle (Nro de lineas a escribir al puerto serial = 1 + Nro. Stickers a imprimir).
                    for (; i < textoImp.length; i++) {
                        System.out.println("TextoSticker " + i + ":" + textoImp[i]);
                        outputStreamSticker.write(textoImp[i].getBytes());

                        try{
                            Thread.sleep(2000);//Esto para que no envie los comandos a la impresora, sin siquiera que haya terminado de imprimir el primer ticket.
                            //Es un retardo para poder hacer un test objetivo.
                        }
                        catch(InterruptedException ex){

                        }
                        int reintentos = 0;
                        int resultadoTesteo;
                        while (true) {
                            if ((resultadoTesteo = verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
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
                                break;
                            }
                        }
                    }
                    JOptionPane.showMessageDialog(null, "Nro de Stickers supuestamente impresos:" + (i - 1), "Impresión", JOptionPane.INFORMATION_MESSAGE);
                } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                    String texto;
                    for (int i = 0; i < textoImp.length; i++) {
                        texto = textoImp[i] + "\n";
                        System.out.println("TextoVoucher " + i + ":" + textoImp[i]);
                        outputStreamVoucher.write(texto.getBytes());
                    }
                    cortarPapel();
                }
            } catch (Exception ex) {
                System.err.println("Rs232.escribirTexto(" + tipo + ") - Error: " + ex.getMessage());
            }
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
     *      2  = IMPRESORA STICKER SIN PAPEL
     *      3  = IMPRESORA STICKER SIN RIBON
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
                lectorBuffer = null;
                bTimeOutSticker = false;
                isReceivedDataSticker = false;
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
            System.err.println(ex.getStackTrace());
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
                System.out.println("lectorBuffer1:" + getStringFromLectorBuffer(lectorBuffer));
                System.out.println("lectorBuffer2:" + getBytesFromLectorBuffer(lectorBuffer));

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
                } else if(lectorBuffer[45] == (byte) 48){
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT;
                }
                else if((byte) lectorBuffer[5] == (byte) 48 && lectorBuffer[43] == (byte) 48 &&
                        lectorBuffer[7] == (byte) 48 && lectorBuffer[45] == (byte) 49){
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
                System.out.println("lectorBuffer:" + getBytesDistinguishedFromLectorBuffer(lectorBuffer));
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
}
