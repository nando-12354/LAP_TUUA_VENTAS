package com.hiper.lap.print;

import java.io.IOException;
import java.io.StringReader;
import java.text.DecimalFormat;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;
import java.util.Locale;
import javax.swing.ImageIcon;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.SwingUtilities;
import netscape.javascript.*;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;

/**
 *
 * @author ESTEBAN ALIAGA GELDRES
 */
public class PrintWeb extends javax.swing.JApplet implements Runnable{

    private Rs232 puertoSerial;

    private String flagSticker;
    private String flagVoucher;

    private String[] textoSticker;
    private String[] textoVoucher;

    //private int nroImpresionesVoucher;
    
    private boolean enabledPrintSticker;
    private boolean enabledPrintVoucher;

    // <editor-fold defaultstate="collapsed" desc="init">
    @Override
    public void init() {
        
        String nombre_Cajero = getParameter(Variables.Nombre_Cajero);//puede ser nulo
        System.out.println("nombre_Cajero: " + nombre_Cajero);

        String dsc_Simbolo = getParameter(Variables.Dsc_Simbolo);//puede ser nulo
        System.out.println("dsc_Simbolo: " + dsc_Simbolo);

        try {

            puertoSerial = new Rs232();

            enabledPrintVoucher = false;
            enabledPrintSticker = false;

            flagSticker = "0";
            flagVoucher = "1";

            GetAndSetFlags();

            GetAndSetConfiguracionPuertosyTextosAImprimir();

            java.awt.EventQueue.invokeAndWait(new Runnable() {
                @Override
                public void run() {
                    initComponents();
                    initComponentsMessage();
                    //ShowAndSetMessage(Variables.MENSAJE_PROCESANDO);
                }
            });
            
            SetShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

            new Thread(this).start();
            
        } catch (Exception ex) {
            JOptionPane.showMessageDialog(null, ex.getMessage() , "El applet no se pudo cargar correctamente.", JOptionPane.ERROR_MESSAGE);
            ex.printStackTrace();
            //Llamar al JS
            ExitNotOk();
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="Para efectos de probar desde el NetBeans">
    //@Override
    public String getParameter2(String item){
        String retorno = null;
        if(item.equals(Variables.FlagImpresoraSticker)){
            retorno = "1";
        }
        if(item.equals(Variables.FlagImpresoraVoucher)){
            retorno = "1";
        }
        //Este ya no se usa
        if(item.equals(Variables.ListaCodigoTickets)){
            
            String sticker1  = "800000000001";
            String sticker2  = "800000000002";
            String sticker3  = "800000000003";
            String sticker4  = "800000000004";
            String sticker5  = "800000000005";
            String sticker6  = "800000000006";
            String sticker7  = "800000000007";
            String sticker8  = "800000000008";
            String sticker9  = "800000000009";
            String sticker10 = "800000000010";
            String sticker11 = "800000000011";
            String sticker12 = "800000000012";
            String sticker13 = "800000000013";
            String sticker14 = "800000000014";
            String sticker15 = "800000000015";
            String sticker16 = "800000000016";
            String sticker17 = "800000000017";
            String sticker18 = "800000000018";
            String sticker19 = "800000000019";
            String sticker20 = "800000000020";

            String stickers = sticker1 + sticker2 + sticker3 + sticker4 + sticker5 ;// + sticker6 + sticker7 + sticker8 + sticker9 + sticker10;

            retorno = stickers;
        }
        //Este ya no se usa
        if(item.equals(Variables.Tam_Ticket)){
            retorno = "12";
        }
        if(item.equals(Variables.ConfiguracionImpresoraSticker)){
            retorno = "COM9,9600,N,8,1";
        }
        if(item.equals(Variables.ConfiguracionImpresoraVoucher)){
            retorno = "COM4,9600,N,8,1";
        }
        if(item.equals(Variables.CopiasVoucher)){
            retorno = "1";
        }
        if(item.equals(Variables.DataSticker)){

            //Cabecera y 20 Stickers:
            String cabecera_sticker = "@" + "^XA^DFR:HIPERDOL.ZPL^FS^FO210,53^A0B,17,17^CI13^FR^FN1^FS^BY2^FO349,104^BCN,92,Y,N,N^FR^FN2^FS^FO230,164^A0N,49,42^CI13^FR^FN3^FS^FO230,208^A0N,19,21^CI13^FR^FN4^FS^FO230,228^A0N,19,21^CI13^FR^FN5^FS^XZ@";
            String sticker1 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000001^FS^FN3^FD01,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker2 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000002^FS^FN3^FD02,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker3 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000003^FS^FN3^FD03,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker4 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000004^FS^FN3^FD04,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker5 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000005^FS^FN3^FD05,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker6 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000006^FS^FN3^FD06,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker7 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000007^FS^FN3^FD07,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker8 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000008^FS^FN3^FD08,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker9 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000009^FS^FN3^FD09,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker10 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000010^FS^FN3^FD10,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker11 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000011^FS^FN3^FD11,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker12 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000012^FS^FN3^FD12,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker13 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000013^FS^FN3^FD13,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker14 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000014^FS^FN3^FD14,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker15 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000015^FS^FN3^FD15,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker16 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000016^FS^FN3^FD16,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker17 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000017^FS^FN3^FD17,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker18 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000018^FS^FN3^FD18,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker19 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000019^FS^FN3^FD19,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
            String sticker20 ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000020^FS^FN3^FD20,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";

            String sticker = cabecera_sticker +
                    sticker1 + sticker2 + sticker3 + sticker4 + sticker5 ;// + sticker6 + sticker7 + sticker8 + sticker9 + sticker10;

            retorno = sticker;
        }
        if(item.equals(Variables.DataVoucher)){
            retorno = "@           Extorno de Tickets           @ @Cajero : ADMIN ADMINNN 28/10/2009 04:19:52@ @Codigo Ticket @--------------- @700000400044    @700000100056    @700000100066    @ @Total ticket  : 3          @ @ @ @";
        }
        if(item.equals(Variables.XmlFormatoVoucher)){
            retorno = "<document name=\"VentaTicketsContingencia\" print=\"voucher\"><title>Venta de Tickets de Contingencia</title><line /><body><detail>=CONC(\"Cajero : \",ALIGNLEFT(nombre_cajero;13),ALIGNRIGHT(DDMMYYYY();10),ALIGNRIGHT(HHMMSS();10))</detail><line /><detail>=CONC(\"Tipo  Tick. \",\"Tick. vendidos   \",\"Precio Unit. \")</detail><detail>=CONC(\"----------- \",\"---------------- \",\"------------ \")</detail><detail>=CONC(ALIGNLEFT(descripcion_tipoticket;10),ALIGNRIGHT(cantidad_ticket;15),ALIGNRIGHT(precio_unitario;15))</detail><line /><detail>=CONC(\"Total a pagar : \",ALIGNLEFT(total_pagar;20))</detail><line /></body></document>";
        }
        if(item.equals(Variables.Nombre_Cajero)){
            retorno = "ADMIN ADMIN";
        }
        if(item.equals(Variables.Imp_Precio)){
            retorno = "11.45";
        }
        if(item.equals(Variables.Dsc_Simbolo)){
            retorno = "$";
        }
        if(item.equals(Variables.Descripcion_tipoticket)){
            retorno = "Infante Nacional Normal";
        }
        return retorno;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="GetNroTicketsImpresos - GetListaCodigoTicketsNoImpresos">
    private int GetNroTicketsImpresos(){
        return puertoSerial.getCantidadStickersImpresos();
    }

    //@Deprecated
    private String GetListaCodigoTicketsNoImpresos(){
        String listaCodigoTicketsNoImpresos = "";
        try{
            String listaCodigoTickets = getParameter(Variables.ListaCodigoTickets);
            String tam_Ticket = getParameter(Variables.Tam_Ticket);
            int Can_TicketImpresos = puertoSerial.getCantidadStickersImpresos();

            listaCodigoTicketsNoImpresos = listaCodigoTickets.substring(Can_TicketImpresos * Integer.parseInt(tam_Ticket));
        }
        catch(Exception ex){
            System.out.println("Error GetListaCodigoTicketsNoImpresos: " + ex.getMessage());
        }
        return listaCodigoTicketsNoImpresos;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="GetAndSetFlags">
    private void GetAndSetFlags(){
        //Flag impresora sticker
        String parametro = getParameter(Variables.FlagImpresoraSticker);
        System.out.println("Print.init() - FlagImpresoraSticker:" + parametro);
        if(parametro!=null){
            this.setFlagSticker(parametro);
        }

        //Flag impresora voucher
        parametro = getParameter(Variables.FlagImpresoraVoucher);
        System.out.println("Print.init() - FlagImpresoraVoucher:" + parametro);
        if(parametro!=null){
            this.setFlagVoucher(parametro);
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="SetVisiblePanel">
    private void SetSetVisiblePanel(final int tipo, final boolean display){
        try {
            javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    SetVisiblePanel(tipo, display);
                }
            });
        } catch (Exception ex) {
            System.err.println("SetVisiblePanel didn't successfully complete");
        }
    }

    private void SetVisiblePanel(int tipo, boolean display){
        if (tipo==Variables.FLAG_PRINTER_STICKER){
            pnlSticker.setVisible(display);
        }
        else if(tipo==Variables.FLAG_PRINTER_VOUCHER){
            pnlVoucher.setVisible(display);//pnlVoucher.setLocation(pnlVoucher.getX(), 100);//Es su location default
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="SetEnabledBtnImprimir">
    private void SetSetEnabledBtnImprimir(final boolean enabled){
        try {
            javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    SetEnabledBtnImprimir(enabled);
                }
            });
        } catch (Exception ex) {
            System.err.println("SetEnabledBtnImprimir didn't successfully complete");
        }
    }

    private void SetEnabledBtnImprimir(boolean enabled){
        btnImprimir.setEnabled(enabled);
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="GetAndSetConfiguracionPuertosyTextosAImprimir">
    private void GetAndSetConfiguracionPuertosyTextosAImprimir() throws Exception{
        String parametro;
        if(this.getFlagSticker().equals("1")){
            //Configuracion de la impresora de sticker
            parametro = getParameter(Variables.ConfiguracionImpresoraSticker);
            System.out.println("Print.init() - ConfiguracionImpresoraSticker:" + parametro);
            if (parametro == null || parametro.length() == 0) {
                throw new Exception("La impresora de sticker no tiene una configuración válida.");
            }
            puertoSerial.setConfigImpSticker(parametro);

            String dataSticker = getParameter(Variables.DataSticker).trim();
            System.out.println("DataSticker obtenido:" + dataSticker);
            dataSticker = dataSticker.substring(1);
            System.out.println("DataSticker real:" + dataSticker);
            this.setTextoSticker(dataSticker.split("\\@"));

            showTextosSticker(this.getTextoSticker());

            if(this.getFlagVoucher().equals("1")){
                //Configuracion de la impresora de voucher
                parametro = getParameter(Variables.ConfiguracionImpresoraVoucher);
                System.out.println("Print.init() - ConfiguracionImpresoraVoucher:" + parametro);
                if (parametro == null || parametro.length() == 0) {
                    throw new Exception("La impresora de voucher no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpVoucher(parametro);

//                parametro = getParameter(Variables.CopiasVoucher);
//                this.setNroImpresionesVoucher(Integer.parseInt(parametro) + 1);

                //Dinamicamente se genera la data para el voucher.
                 String dataVoucher = getParameter(Variables.DataVoucher).trim();//El getParameter hace el trim() implicitamente, pero porseacaso.
                 System.out.println("DataVoucher obtenido:" + dataVoucher);
                 dataVoucher = dataVoucher.substring(1);
                 System.out.println("DataVoucher real:" + dataVoucher);
                 this.setTextoVoucher(dataVoucher.split("\\@"));
                 showTextosVoucher(this.getTextoVoucher());
            }
        }
        else if(this.getFlagVoucher().equals("1")){
            //Configuracion de la impresora de voucher
            parametro = getParameter(Variables.ConfiguracionImpresoraVoucher);
            System.out.println("Print.init() - ConfiguracionImpresoraVoucher:" + parametro);
            if (parametro == null || parametro.length() == 0) {
                throw new Exception("La impresora de voucher no tiene una configuración válida.");
            }
            puertoSerial.setConfigImpVoucher(parametro);

//            parametro = getParameter(Variables.CopiasVoucher);
//            this.setNroImpresionesVoucher(Integer.parseInt(parametro) + 1);

            String dataVoucher = getParameter(Variables.DataVoucher).trim();//El getParameter hace el trim() implicitamente, pero porseacaso.
            System.out.println("DataVoucher obtenido:" + dataVoucher);
            dataVoucher = dataVoucher.substring(1);
            System.out.println("DataVoucher real:" + dataVoucher);
            this.setTextoVoucher(dataVoucher.split("\\@"));
            showTextosVoucher(this.getTextoVoucher());
        }
    }


    private void showTextosVoucher(String[] textos){
        for(int i=0; i<textos.length; i++){
            System.out.println("TextoVoucher " + i + ":" + textos[i]);
        }
    }

    private void showTextosSticker(String[] textos){
        for(int i=0; i<textos.length; i++){
            System.out.println("TextoSticker " + i + ":" + textos[i]);
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="run inicial">
    @Override
    public void run() {
        try {
            Thread.sleep(1000);//Que se vea el efecto de Procesando por al menos un segundo.

        } catch (Exception ex) {
        }

        if(ProcesarTesteoPrevioImprimir()){
            SetShowAndSetMessage(Variables.MENSAJE_IMPRIMIENDO);
            Imprimir();
            //Llamar al JS
            ExitImprimioOk();
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ProcesarTesteoPrevioImprimir">
    private boolean ProcesarTesteoPrevioImprimir(){
        if (this.getFlagSticker().equals("1")) {
            int retornoSticker = TestImpresora(Variables.FLAG_PRINTER_STICKER);

            if (this.getFlagVoucher().equals("1")) {
                int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                if(retornoVoucher != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE ||
                   retornoSticker != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE ){
                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_STICKER, retornoSticker);
                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, true);
                    //
                    if(retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
                       retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE ){
                        enabledPrintVoucher = true;
                    }
                    else{
                        enabledPrintVoucher = false;
                    }
                    //
                    if(retornoSticker == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE){
                        SetSetEnabledBtnImprimir(true);
                        enabledPrintSticker = true;
                    }
                    else{
                        SetSetEnabledBtnImprimir(false);
                        enabledPrintSticker = false;
                    }
                    SetShowConfigManualPuertos();
                    return false;
                }
                enabledPrintSticker = true;
                enabledPrintVoucher = true;
                return true;
            }
            else{
                //No se da este caso...Reservado para un futuro.
            }
        }
        else if (this.getFlagVoucher().equals("1")) {
            int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);
            if(retornoVoucher != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE){
                SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, false);
                if(retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END){
                    SetSetEnabledBtnImprimir(true);
                    enabledPrintVoucher = true;
                }
                else{
                    SetSetEnabledBtnImprimir(false);
                    enabledPrintVoucher = false;
                }
                SetShowConfigManualPuertos();
                return false;
            }
            enabledPrintVoucher = true;
            return true;
        }
        //Llamar al JS
        JSObject win = (JSObject) JSObject.getWindow(this);
        win.eval("PostImpresionError(\"" + Defines.Error_NoControlado + "\")");
        return false;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ProcesarActualizacionTesteo">
    private void ProcesarActualizacionTesteo(){
        if (this.getFlagSticker().equals("1")) {
            int retornoSticker = TestImpresora(Variables.FLAG_PRINTER_STICKER);

            if (this.getFlagVoucher().equals("1")) {
                int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_STICKER, retornoSticker);
                SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, true);
                //
                if(retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
                   retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE ){
                    enabledPrintVoucher = true;
                }
                else{
                    enabledPrintVoucher = false;
                }
                //
                if(retornoSticker == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE){
                    SetSetEnabledBtnImprimir(true);
                    enabledPrintSticker = true;
                }
                else{
                    SetSetEnabledBtnImprimir(false);
                    enabledPrintSticker = false;
                }
            }
        }
        else if (this.getFlagVoucher().equals("1")) {
            int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

            SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
            SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
            SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, false);
            if(retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
               retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE ){
                SetSetEnabledBtnImprimir(true);
                enabledPrintVoucher = true;
            }
            else{
                SetSetEnabledBtnImprimir(false);
                enabledPrintVoucher = false;
            }
        }
        SetShowConfigManualPuertos();
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="Imprimir">
    //Falta validar para retornar a la pagina valores reales........
    private void Imprimir(){
        if (this.getFlagSticker().equals("1") && enabledPrintSticker) {
            puertoSerial.escribirTexto(getTextoSticker(), Variables.FLAG_PRINTER_STICKER);
            if (this.getFlagVoucher().equals("1")  && enabledPrintVoucher) {
                if(puertoSerial.getCantidadStickersImpresos()>0){
//                    if(CargarDataVoucher()){
//                        for(int i=0;i<getNroImpresionesVoucher();i++){
                            puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
//                        }
//                    }
                }
            }
        }
        else if (this.getFlagVoucher().equals("1") && enabledPrintVoucher) {
//            for(int i=0;i<getNroImpresionesVoucher();i++){
                puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
//            }
        }
    }
    // </editor-fold>

    //Este metodo es cuando no se puede imprimir en la impresora de voucher, entonces debo mandar la data de voucher en forma de string a la pagina aspnet.
    //Esto con el proposito del string que se guarde en BD, para luego reimprimirlo.
    private String GetDataVoucherFormateada(){
        String dataVoucherFormateada = "";
        for(int i=0; i<this.getTextoVoucher().length; i++){
            dataVoucherFormateada += getTextoVoucher()[i] + "@";
        }
        return dataVoucherFormateada;
    }

    // <editor-fold defaultstate="collapsed" desc="CargarDataVoucher">
    private boolean CargarDataVoucher(){
        try{
            String xmlFormatoVoucher = getParameter(Variables.XmlFormatoVoucher);
            xmlFormatoVoucher = "<documents>" + xmlFormatoVoucher + "</documents>";
            Hashtable htPrintData = new Hashtable();
            CargarParametrosImpresion(htPrintData);

            SAXBuilder builder = new SAXBuilder(false);
            Document doc = null;
            try {
                doc = builder.build(new StringReader(xmlFormatoVoucher));
            } catch (JDOMException ex) {
                System.out.println("CargarDataVoucher - Error JDOMException: " + ex.getMessage());
            } catch (IOException ex) {
                System.out.println("CargarDataVoucher - Error IOException: " + ex.getMessage());
            }
            List<Element> nodos = doc.getRootElement().getChildren();
            Element nodo = nodos.get(0);

            Xml xml = new Xml();
            String[] textoVoucherPrint = xml.obtenerDocumento(htPrintData, nodo);
            this.setTextoVoucher(textoVoucherPrint);
            System.out.println("Dinamicamente construido el DataVoucher:");
            showTextosVoucher(this.getTextoVoucher());
        }
        catch(Exception ex){
            System.out.println("CargarDataVoucher - Error: " + ex.getMessage());
            return false;
        }
        return true;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="CargarParametrosImpresion">
    private void CargarParametrosImpresion(Hashtable htPrintData){
        Locale.setDefault(Locale.US);
//        Locale locale = Locale.getDefault();
//        Locale.setDefault(locale);

        String nombre_Cajero = getParameter(Variables.Nombre_Cajero);
        String imp_Precio = getParameter(Variables.Imp_Precio);
        String dsc_Simbolo = getParameter(Variables.Dsc_Simbolo);

        /*
        if(dsc_Simbolo.equals("EuroSymbol")){
            dsc_Simbolo = "€";
        }
        */

        String descripcion_tipoticket = getParameter(Variables.Descripcion_tipoticket);

        htPrintData.put(Defines.ID_PRINTER_PARAM_NOMBRE_CAJERO, nombre_Cajero);
        htPrintData.put(Defines.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET, descripcion_tipoticket);
        int Can_Ticket = puertoSerial.getCantidadStickersImpresos();
        htPrintData.put(Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET, String.valueOf(Can_Ticket) );
        float dImp_Precio;
        try{
            dImp_Precio = Float.valueOf(imp_Precio);
        }
        catch(Exception ex){
            System.err.println("Error al intentar Float.valueOf(): " + ex.getMessage());
            imp_Precio = imp_Precio.replace(',', '.');
            dImp_Precio = Float.valueOf(imp_Precio);
        }
        DecimalFormat df = new DecimalFormat("#.00");
        htPrintData.put(Defines.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET, df.format(dImp_Precio));// + " " + dsc_Simbolo);
        htPrintData.put(Defines.ID_PRINTER_PARAM_TOTAL_PAGAR, df.format(dImp_Precio * Can_Ticket));// + " " + dsc_Simbolo);
        htPrintData.put(Defines.ID_PRINTER_PARAM_SIMBOLO_MONEDA, dsc_Simbolo);

        //-- EAG 10/02/2010
        String listaCodigoTickets = getParameter(Variables.ListaCodigoTickets);
                
        int q1 = Can_Ticket/2;
        int q2 = Can_Ticket%2;
        if(q2!=0)
        {
            q1 = q1 + 1;
        }
        htPrintData.put(Defines.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, String.valueOf(q1));

        //htPrintData.put(Defines.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, String.valueOf(Can_Ticket));
        int intLongTicket = Variables.Longitud_Ticket;
        int contador = 0;
        // recorre cada codigo de ticket
        for (int i = 0, j = 0; i < Can_Ticket; i++)
        {
            
            /*
            // codigo de ticket
            htPrintData.put(Defines.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, listaCodigoTickets.substring(contador, contador + intLongTicket));
            contador += intLongTicket;
            */

            if((i+1)%2==0)//Par
            {
                htPrintData.put("codigo_ticket_par" + "_" + j, listaCodigoTickets.substring(contador, contador + intLongTicket));
                contador += intLongTicket;
                j++;
            }
            else//Impar
            {
                htPrintData.put("codigo_ticket_impar" + "_" + j, listaCodigoTickets.substring(contador, contador + intLongTicket));
                contador += intLongTicket;
            }

        }
        //cantidad de Tickets ya fue seteado.
        //-- EAG 10/02/2010

        //-- EAG 29/01/2010
        String compania = getParameter(Variables.Compania);
        String nrovuelo = getParameter(Variables.NroVuelo);

        System.out.println("LAP compania:" + compania);
        System.out.println("LAP nrovuelo:" + nrovuelo);

        if(compania!=null)
            htPrintData.put(Variables.Compania, compania);
        else
            htPrintData.put(Variables.Compania, "");

        if(nrovuelo!=null)
            htPrintData.put(Variables.NroVuelo, nrovuelo);
        else
            htPrintData.put(Variables.NroVuelo, "");

        //-- EAG 29/01/2010
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="UpdatePanelConfigManualPuertos">
    private void SetUpdatePanelConfigManualPuertos(final int tipo, final int retorno){
        try {
            javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    UpdatePanelConfigManualPuertos(tipo, retorno);
                }
            });
        } catch (Exception e) {
            System.err.println("UpdatePanelConfigManualPuertos didn't successfully complete");
        }
    }

    private void UpdatePanelConfigManualPuertos(int tipo, int retorno){
        if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
            String puertoAsignadoVoucher = puertoSerial.getConfigImpVoucher()[0].trim();
            cboPtosVoucher.removeAllItems();
            cboPtosVoucher.addItem("- Seleccione -");
            llenarCombo(cboPtosVoucher);
            switch(retorno){
                case Variables.FLAG_PRINTER_PORT_ERROR:
                    lblStatusImpresoraVoucher.setText(Defines.MsgPrinterPortError);
                    lblPuertoImpresoraVoucher.setText("Puerto asignado incorrectamente: " + puertoAsignadoVoucher);
                    chkPuertoVoucher.setSelected(true);
                    cboPtosVoucher.setEnabled(true);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE:
                    lblStatusImpresoraVoucher.setText(Defines.MsgVoucherNotOperative);
                    lblPuertoImpresoraVoucher.setText("Puerto asignado: " + puertoAsignadoVoucher);
                    chkPuertoVoucher.setSelected(true);
                    cboPtosVoucher.setSelectedItem(puertoAsignadoVoucher);
                    cboPtosVoucher.setEnabled(true);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE:
                    lblStatusImpresoraVoucher.setText(Defines.MsgVoucherOperative);
                    lblPuertoImpresoraVoucher.setText("Puerto asignado correctamente: " + puertoAsignadoVoucher);
                    chkPuertoVoucher.setSelected(false);
                    cboPtosVoucher.setSelectedItem(puertoAsignadoVoucher);
                    cboPtosVoucher.setEnabled(false);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathGoodStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END:
                    lblStatusImpresoraVoucher.setText(Defines.MsgVoucherPaperNearEnd);
                    lblPuertoImpresoraVoucher.setText("Puerto asignado correctamente: " + puertoAsignadoVoucher);
                    chkPuertoVoucher.setSelected(false);
                    cboPtosVoucher.setSelectedItem(puertoAsignadoVoucher);
                    cboPtosVoucher.setEnabled(false);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathWarningStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_END:
                    lblStatusImpresoraVoucher.setText(Defines.MsgVoucherPaperEnd);
                    lblPuertoImpresoraVoucher.setText("Puerto asignado correctamente: " + puertoAsignadoVoucher);
                    chkPuertoVoucher.setSelected(false);
                    cboPtosVoucher.setSelectedItem(puertoAsignadoVoucher);
                    cboPtosVoucher.setEnabled(false);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
            }
            
        }
        else if(tipo == Variables.FLAG_PRINTER_STICKER){
            String puertoAsignadoSticker = puertoSerial.getConfigImpSticker()[0].trim();
            cboPtosSticker.removeAllItems();
            cboPtosSticker.addItem("- Seleccione -");
            llenarCombo(cboPtosSticker);
            switch(retorno){
                case Variables.FLAG_PRINTER_PORT_ERROR:
                    lblStatusImpresoraSticker.setText(Defines.MsgPrinterPortError);
                    lblPuertoImpresoraSticker.setText("Puerto asignado incorrectamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(true);
                    cboPtosSticker.setEnabled(true);
                    lblStatusImageSticker.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerNotOperative);
                    lblPuertoImpresoraSticker.setText("Puerto asignado: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(true);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(true);
                    lblStatusImageSticker.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerOperative);
                    lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(false);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(false);
                    lblStatusImageSticker.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathGoodStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_HEADUP:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerHeadUp);
                    lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(false);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(false);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerPauseMode);
                    lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(false);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(false);
                    lblStatusImageVoucher.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_PAPER_OUT:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerPaperOut);
                    lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(false);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(false);
                    lblStatusImageSticker.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
                case Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT:
                    lblStatusImpresoraSticker.setText(Defines.MsgStickerRibbonOut);
                    lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: " + puertoAsignadoSticker);
                    chkPuertoSticker.setSelected(false);
                    cboPtosSticker.setSelectedItem(puertoAsignadoSticker);
                    cboPtosSticker.setEnabled(false);
                    lblStatusImageSticker.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathBadStatus)));
                    break;
            }
        }
    }
    // </editor-fold>
    
    // <editor-fold defaultstate="collapsed" desc="Metodos de OpenPort y Test -> Deprecados">
    private boolean OpenPortPrinter(int tipo){
        if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
            if(puertoSerial.StartPort(Variables.FLAG_PRINTER_VOUCHER)){
                return true;
            }
            else{
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                return false;
            }
        }
        else if(tipo == Variables.FLAG_PRINTER_STICKER){
            if(puertoSerial.StartPort(Variables.FLAG_PRINTER_STICKER)){
                return true;
            }
            else{
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                return false;
            }
        }
        return false;
    }

    private int TestPrinter(int tipo){
        int retorno = Variables.FLAG_PRINTER_ERROR;
        if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
            int reintentos = 0;
            while (true) {
                if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER)) != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE) {
                    reintentos++;
                    if (reintentos >= 2) {
                        System.out.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher en el reintento " + (reintentos - 1) + " - Retorno: " + retorno + ".");
                        if(retorno == Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE){
                            puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                        }
                        break;
                    }
                    System.out.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher - Retorno: " + retorno + ".");
                    try {
                        Thread.sleep(250);
                    } catch (InterruptedException ex) {
                    }
                } else {
                    System.out.println("Print.TestImpresora() - Se testeo correctamente la Impresora de Voucher.");
                    break;
                }
            }
        }
        else if(tipo == Variables.FLAG_PRINTER_STICKER){
            int reintentos = 0;
            while (true) {
                if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                    reintentos++;
                    if (reintentos >= 2) {
                        System.out.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker en el reintento " + (reintentos - 1) + " - Retorno: " + retorno + ".");
                        if(retorno == Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE){
                            puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                        }
                        break;
                    }
                    System.out.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " + retorno + ".");
                    try {
                        Thread.sleep(250);
                    } catch (InterruptedException ex) {
                    }
                } else {
                    System.out.println("Print.TestImpresora() - Se testeo correctamente la Impresora de Sticker.");
                    break;
                }
            }
        }
        return retorno;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="TestImpresora">
    private int TestImpresora(int tipo){
        int retorno = Variables.FLAG_PRINTER_ERROR;
        if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
            boolean opened = puertoSerial.StartPort(Variables.FLAG_PRINTER_VOUCHER);
            if(opened){
                int reintentos = 0;
                while (true) {
                    if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER)) != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE) {
                        reintentos++;
                        if (reintentos >= 2) {
                            System.err.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher en el reintento " + (reintentos - 1) + " - Retorno: " + retorno + ".");
                            if(retorno == Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE){
                                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                            }
                            break;
                        }
                        System.err.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher - Retorno: " + retorno + ".");
                        try {
                            Thread.sleep(250);
                        } catch (InterruptedException ex) {
                        }
                    } else {
                        System.out.println("Print.TestImpresora() - Se testeo correctamente la Impresora de Voucher.");
                        break;
                    }
                }
            }else{
                retorno = Variables.FLAG_PRINTER_PORT_ERROR;
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
            }
        }
        else if(tipo == Variables.FLAG_PRINTER_STICKER){
            boolean opened = puertoSerial.StartPort(Variables.FLAG_PRINTER_STICKER);
            if(opened){
                int reintentos = 0;
                while (true) {
                    if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                        reintentos++;
                        if (reintentos >= 2) {
                            System.err.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker en el reintento " + (reintentos - 1) + " - Retorno: " + retorno + ".");
                            if(retorno == Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE){
                                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                            }
                            break;
                        }
                        System.err.println("Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " + retorno + ".");
                        try {
                            Thread.sleep(250);
                        } catch (InterruptedException ex) {
                        }
                    } else {
                        System.out.println("Print.TestImpresora() - Se testeo correctamente la Impresora de Sticker.");
                        break;
                    }
                }
            }else{
                retorno = Variables.FLAG_PRINTER_PORT_ERROR;
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
            }
        }
        return retorno;
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="initComponentsMessage">
    private void initComponentsMessage() {
        pnlGifAnimado = new JPanel();
        pnlGifAnimado.setLayout(null);
        pnlGifAnimado.setBackground(new java.awt.Color(255, 255, 255));
        //pnlGifAnimado.setOpaque(true);

        lblGifAnimado = new JLabel();
        lblGifAnimado.setOpaque(false);//Hace que sea transparente. Es por default.
        ImageIcon image = new ImageIcon(getClass().getResource(Defines.PathImageLoading));
        lblGifAnimado.setIcon(image);
        pnlGifAnimado.add(lblGifAnimado);
        //lblGifAnimado.setBounds(getWidth() / 2 - image.getIconWidth() / 2, 0, getWidth(), getHeight());//Alineado al centro.
        lblGifAnimado.setBounds(450 / 2 - image.getIconWidth() / 2, 0, 450, 450);//Alineado al centro.

        lblMsgProc = new JLabel();
        //lblMsgProc.setText(mensaje);
        pnlGifAnimado.add(lblMsgProc);
        lblMsgProc.setBounds(190, 120, 130, 20);

        getContentPane().add(pnlGifAnimado);
        //pnlGifAnimado.setBounds(0, 0, getWidth(), getHeight());
        pnlGifAnimado.setBounds(0, 0, 450, 450);
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ShowAndSetMessage">
    private void SetShowAndSetMessage(final String message){
        if(SwingUtilities.isEventDispatchThread()){
            ShowAndSetMessage(message);
        }
        else{
            try {
                javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                    @Override
                    public void run() {
                        ShowAndSetMessage(message);
                    }
                });
            } catch (Exception ex) {
                System.err.println("ShowAndSetMessage didn't successfully complete");
            }
        }
    }

    private void ShowAndSetMessage(String message){
        pnlConfigManualPuertos.setVisible(false);
        pnlGifAnimado.setVisible(true);
        lblMsgProc.setText(message);
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ShowConfigManualPuertos">
    private void SetShowConfigManualPuertos(){
        try {
            javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    ShowConfigManualPuertos();
                }
            });
        } catch (Exception ex) {
            System.err.println("ShowConfigManualPuertos didn't successfully complete");
        }
    }

    private void ShowConfigManualPuertos(){
        pnlConfigManualPuertos.setVisible(true);
        pnlGifAnimado.setVisible(false);
    }
    // </editor-fold>

    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        pnlConfigManualPuertos = new javax.swing.JPanel();
        lblTituloConfig = new javax.swing.JLabel();
        lblPuertos = new javax.swing.JLabel();
        lblDisponibles = new javax.swing.JLabel();
        btnRefresh = new javax.swing.JButton();
        btnActualizar = new javax.swing.JButton();
        btnSalir = new javax.swing.JButton();
        btnImprimir = new javax.swing.JButton();
        pnlVoucher = new javax.swing.JPanel();
        sep1 = new javax.swing.JSeparator();
        lblStatusImageVoucher = new javax.swing.JLabel();
        lblVoucher = new javax.swing.JLabel();
        lblStatusImpresoraVoucher = new javax.swing.JLabel();
        lblPuertoImpresoraVoucher = new javax.swing.JLabel();
        chkPuertoVoucher = new javax.swing.JCheckBox();
        cboPtosVoucher = new javax.swing.JComboBox();
        pnlSticker = new javax.swing.JPanel();
        sep2 = new javax.swing.JSeparator();
        lblStatusImageSticker = new javax.swing.JLabel();
        lblSticker = new javax.swing.JLabel();
        lblStatusImpresoraSticker = new javax.swing.JLabel();
        lblPuertoImpresoraSticker = new javax.swing.JLabel();
        chkPuertoSticker = new javax.swing.JCheckBox();
        cboPtosSticker = new javax.swing.JComboBox();

        getContentPane().setLayout(null);

        pnlConfigManualPuertos.setBackground(new java.awt.Color(255, 255, 255));
        pnlConfigManualPuertos.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        pnlConfigManualPuertos.setLayout(null);

        lblTituloConfig.setFont(new java.awt.Font("Tahoma", 1, 12));
        lblTituloConfig.setForeground(new java.awt.Color(0, 102, 204));
        lblTituloConfig.setText("Informacion de Estado de Impresoras");
        pnlConfigManualPuertos.add(lblTituloConfig);
        lblTituloConfig.setBounds(100, 20, 230, 14);

        lblPuertos.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblPuertos.setText("Puertos");
        pnlConfigManualPuertos.add(lblPuertos);
        lblPuertos.setBounds(330, 60, 60, 14);

        lblDisponibles.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblDisponibles.setText("Disponibles");
        pnlConfigManualPuertos.add(lblDisponibles);
        lblDisponibles.setBounds(320, 80, 70, 14);

        btnRefresh.setIcon(new javax.swing.ImageIcon(getClass().getResource(Defines.PathRefreshPorts)));
        btnRefresh.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        btnRefresh.setFocusable(false);
        btnRefresh.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnRefreshActionPerformed(evt);
            }
        });
        pnlConfigManualPuertos.add(btnRefresh);
        btnRefresh.setBounds(390, 60, 25, 28);

        btnActualizar.setText("Actualizar");
        btnActualizar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnActualizarActionPerformed(evt);
            }
        });
        pnlConfigManualPuertos.add(btnActualizar);
        btnActualizar.setBounds(40, 360, 100, 23);

        btnSalir.setText("Salir");
        btnSalir.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnSalirActionPerformed(evt);
            }
        });
        pnlConfigManualPuertos.add(btnSalir);
        btnSalir.setBounds(190, 360, 80, 23);

        btnImprimir.setText("Imprimir");
        btnImprimir.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnImprimirActionPerformed(evt);
            }
        });
        pnlConfigManualPuertos.add(btnImprimir);
        btnImprimir.setBounds(310, 360, 90, 23);

        pnlVoucher.setBackground(new java.awt.Color(255, 255, 255));
        pnlVoucher.setLayout(null);
        pnlVoucher.add(sep1);
        sep1.setBounds(10, 5, 420, 2);
        pnlVoucher.add(lblStatusImageVoucher);
        lblStatusImageVoucher.setBounds(180, 10, 40, 30);

        lblVoucher.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblVoucher.setForeground(new java.awt.Color(0, 102, 204));
        lblVoucher.setText("IMPRESORA VOUCHER");
        pnlVoucher.add(lblVoucher);
        lblVoucher.setBounds(35, 15, 140, 14);

        lblStatusImpresoraVoucher.setFont(new java.awt.Font("Tahoma", 0, 10));
        lblStatusImpresoraVoucher.setText("Error en puerto asignado.");
        pnlVoucher.add(lblStatusImpresoraVoucher);
        lblStatusImpresoraVoucher.setBounds(35, 55, 240, 13);

        lblPuertoImpresoraVoucher.setFont(new java.awt.Font("Tahoma", 0, 10));
        lblPuertoImpresoraVoucher.setText("Puerto asignado incorrectamente: COM4.");
        pnlVoucher.add(lblPuertoImpresoraVoucher);
        lblPuertoImpresoraVoucher.setBounds(35, 75, 240, 13);

        chkPuertoVoucher.setBackground(new java.awt.Color(255, 255, 255));
        chkPuertoVoucher.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                chkPuertoVoucherActionPerformed(evt);
            }
        });
        pnlVoucher.add(chkPuertoVoucher);
        chkPuertoVoucher.setBounds(280, 70, 21, 21);

        pnlVoucher.add(cboPtosVoucher);
        cboPtosVoucher.setBounds(310, 70, 105, 22);

        pnlConfigManualPuertos.add(pnlVoucher);
        pnlVoucher.setBounds(5, 100, 440, 100);

        pnlSticker.setBackground(new java.awt.Color(255, 255, 255));
        pnlSticker.setLayout(null);
        pnlSticker.add(sep2);
        sep2.setBounds(10, 10, 420, 2);
        pnlSticker.add(lblStatusImageSticker);
        lblStatusImageSticker.setBounds(180, 15, 40, 32);

        lblSticker.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblSticker.setForeground(new java.awt.Color(0, 102, 204));
        lblSticker.setText("IMPRESORA STICKER");
        pnlSticker.add(lblSticker);
        lblSticker.setBounds(35, 20, 120, 14);

        lblStatusImpresoraSticker.setFont(new java.awt.Font("Tahoma", 0, 10));
        lblStatusImpresoraSticker.setText("Esta Inoperativa. Falta Papel o  Poco Papel. Revisar.");
        pnlSticker.add(lblStatusImpresoraSticker);
        lblStatusImpresoraSticker.setBounds(35, 55, 240, 13);

        lblPuertoImpresoraSticker.setFont(new java.awt.Font("Tahoma", 0, 10));
        lblPuertoImpresoraSticker.setText("Puerto asignado correctamente: COM4.");
        pnlSticker.add(lblPuertoImpresoraSticker);
        lblPuertoImpresoraSticker.setBounds(35, 75, 240, 13);

        chkPuertoSticker.setBackground(new java.awt.Color(255, 255, 255));
        chkPuertoSticker.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                chkPuertoStickerActionPerformed(evt);
            }
        });
        pnlSticker.add(chkPuertoSticker);
        chkPuertoSticker.setBounds(280, 70, 21, 21);

        pnlSticker.add(cboPtosSticker);
        cboPtosSticker.setBounds(310, 70, 105, 22);

        pnlConfigManualPuertos.add(pnlSticker);
        pnlSticker.setBounds(5, 210, 440, 100);

        getContentPane().add(pnlConfigManualPuertos);
        pnlConfigManualPuertos.setBounds(0, 0, 450, 450);
    }// </editor-fold>//GEN-END:initComponents

    // <editor-fold defaultstate="collapsed" desc="btnRefreshActionPerformed">
    private void btnRefreshActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnRefreshActionPerformed
        if(this.flagSticker.equals("1")){
            String puertoSelected = cboPtosSticker.getSelectedItem().toString();
            cboPtosSticker.removeAllItems();
            cboPtosSticker.addItem("- Seleccione -");
            llenarCombo(cboPtosSticker);
            cboPtosSticker.setSelectedItem(puertoSelected);
        }
        if(this.flagVoucher.equals("1")){
            String puertoSelected = cboPtosVoucher.getSelectedItem().toString();
            cboPtosVoucher.removeAllItems();
            cboPtosVoucher.addItem("- Seleccione -");
            llenarCombo(cboPtosVoucher);
            cboPtosVoucher.setSelectedItem(puertoSelected);
        }
}//GEN-LAST:event_btnRefreshActionPerformed
    // </editor-fold>
    
    // <editor-fold defaultstate="collapsed" desc="btnImprimirActionPerformed">
    private void btnImprimirActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnImprimirActionPerformed
        ShowAndSetMessage(Variables.MENSAJE_IMPRIMIENDO);
        new Thread(new Runnable() {

            @Override
            public void run() {
                Imprimir();
                //Llamar al JS
                ExitImprimioOk();
            }
        }).start();
}//GEN-LAST:event_btnImprimirActionPerformed
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="btnSalirActionPerformed">
    private void btnSalirActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnSalirActionPerformed
        String msg = "¿Desea salir del modulo de impresion?";
        if (JOptionPane.showConfirmDialog(this, msg, "Confirmar", JOptionPane.YES_NO_OPTION) == JOptionPane.NO_OPTION)
        {
            return;
        }
        //Llamar al JS
        SalirPorUsuario();
    }//GEN-LAST:event_btnSalirActionPerformed
    // </editor-fold>
    
    // <editor-fold defaultstate="collapsed" desc="btnActualizarActionPerformed">
    private void btnActualizarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnActualizarActionPerformed
        if(this.flagSticker.equals("1")){
            puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
            String puertoStickerSelected = null;
            if(chkPuertoSticker.isSelected()){
                if(cboPtosSticker.getSelectedIndex()>0){
                    puertoStickerSelected = cboPtosSticker.getSelectedItem().toString();
                }
                if(puertoStickerSelected==null){
                    JOptionPane.showMessageDialog(null, "Debe seleccionar un puerto \npara la impresora de sticker", "Impresión", JOptionPane.ERROR_MESSAGE);
                    return;
                }
            }
            else{
                puertoStickerSelected = puertoSerial.getConfigImpSticker()[0].trim();
            }
            puertoSerial.getConfigImpSticker()[0] = puertoStickerSelected;
        }
        if(this.flagVoucher.equals("1")){
            puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
            String puertoVoucherSelected = null;
            if(chkPuertoVoucher.isSelected()){
                if(cboPtosVoucher.getSelectedIndex()>0){
                    puertoVoucherSelected = cboPtosVoucher.getSelectedItem().toString();
                }
                if(puertoVoucherSelected==null){
                    JOptionPane.showMessageDialog(null, "Debe seleccionar un puerto \npara la impresora de voucher", "Impresión", JOptionPane.ERROR_MESSAGE);
                    return;
                }
            }
            else{
                puertoVoucherSelected = puertoSerial.getConfigImpVoucher()[0].trim();
            }
            puertoSerial.getConfigImpVoucher()[0] = puertoVoucherSelected;
        }
        
        ShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

        new Thread(new Runnable() {

            @Override
            public void run() {
                try {
                    Thread.sleep(1000);//Que se vea el efecto de Procesando por al menos un segundo.

                } catch (Exception ex) {
                }

                ProcesarActualizacionTesteo();
            }
        }).start();
    }//GEN-LAST:event_btnActualizarActionPerformed
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="chks ActionPerformed">
    private void chkPuertoStickerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_chkPuertoStickerActionPerformed
        if(chkPuertoSticker.isSelected()){
            cboPtosSticker.setEnabled(true);
        }
        else{
            cboPtosSticker.setEnabled(false);
        }
    }//GEN-LAST:event_chkPuertoStickerActionPerformed

    private void chkPuertoVoucherActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_chkPuertoVoucherActionPerformed
        if(chkPuertoVoucher.isSelected()){
            cboPtosVoucher.setEnabled(true);
        }
        else{
            cboPtosVoucher.setEnabled(false);
        }
    }//GEN-LAST:event_chkPuertoVoucherActionPerformed
    // </editor-fold>

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnActualizar;
    private javax.swing.JButton btnImprimir;
    private javax.swing.JButton btnRefresh;
    private javax.swing.JButton btnSalir;
    private javax.swing.JComboBox cboPtosSticker;
    private javax.swing.JComboBox cboPtosVoucher;
    private javax.swing.JCheckBox chkPuertoSticker;
    private javax.swing.JCheckBox chkPuertoVoucher;
    private javax.swing.JLabel lblDisponibles;
    private javax.swing.JLabel lblPuertoImpresoraSticker;
    private javax.swing.JLabel lblPuertoImpresoraVoucher;
    private javax.swing.JLabel lblPuertos;
    private javax.swing.JLabel lblStatusImageSticker;
    private javax.swing.JLabel lblStatusImageVoucher;
    private javax.swing.JLabel lblStatusImpresoraSticker;
    private javax.swing.JLabel lblStatusImpresoraVoucher;
    private javax.swing.JLabel lblSticker;
    private javax.swing.JLabel lblTituloConfig;
    private javax.swing.JLabel lblVoucher;
    private javax.swing.JPanel pnlConfigManualPuertos;
    private javax.swing.JPanel pnlSticker;
    private javax.swing.JPanel pnlVoucher;
    private javax.swing.JSeparator sep1;
    private javax.swing.JSeparator sep2;
    // End of variables declaration//GEN-END:variables

    //Variables para la ventana de Procesando... e Imprimiendo...
    private JPanel pnlGifAnimado;
    private JLabel lblGifAnimado;
    private JLabel lblMsgProc;

    // <editor-fold defaultstate="collapsed" desc="Propiedades">
    public String getFlagVoucher() {
        return flagVoucher;
    }

    public void setFlagVoucher(String flagVoucher) {
        this.flagVoucher = flagVoucher;
    }

    public String getFlagSticker() {
        return flagSticker;
    }

    public void setFlagSticker(String flagSticker) {
        this.flagSticker = flagSticker;
    }

    public String[] getTextoVoucher(){
        return textoVoucher;
    }

    public String[] getTextoSticker(){
        return textoSticker;
    }

    public void setTextoVoucher(String[] textoVoucher){
        this.textoVoucher = textoVoucher;
    }

    public void setTextoSticker(String[] textoSticker){
        this.textoSticker = textoSticker;
    }

//    public int getNroImpresionesVoucher() {
//        return nroImpresionesVoucher;
//    }
//
//    public void setNroImpresionesVoucher(int nroImpresionesVoucher) {
//        this.nroImpresionesVoucher = nroImpresionesVoucher;
//    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="stop, destroy methods, KillPorts">
    @Override
    public void stop() {
        try {
            KillPorts();
        } catch (Exception ex) {
            System.out.println("Print.stop() Error:" + ex.getMessage());
        }
    }

    @Override
    public void destroy() {
        try {
            KillPorts();
        } catch (Exception ex) {
            System.out.println("Print.destroy() Error:" + ex.getMessage());
        }
    }

    private void KillPorts() {
        if (this.getFlagSticker().equals("1")) {
            puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
        }
        if (this.getFlagVoucher().equals("1")) {
            puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="llenarCombo">
    public void llenarCombo(JComboBox cmb) {
        try {
            ArrayList<String> portList = Rs232.GetPortIdentifiers();
            for (String puerto : portList) {
                cmb.addItem(puerto);
            }
        } catch (Exception ex) {
            System.out.println(ex.toString());
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ExitImprimioOk">
    private void ExitImprimioOk(){
        JSObject win = (JSObject) JSObject.getWindow(PrintWeb.this);
        if(getFlagSticker().equals("1")){
            int nroTicketsImpresos = GetNroTicketsImpresos();
            if(getFlagVoucher().equals("1")){
                //win.eval("PostImpresion(\"" + Defines.Impresion_StickerConVoucher + "\",\"" + nroTicketsImpresos +"\")");
                win.eval("PostImpresion(\"" + Defines.Impresion_StickerConVoucher + "\",\"" + nroTicketsImpresos + "\",\"" +
                        puertoSerial.getConfigImpSticker()[0].trim() + "," + puertoSerial.getConfigImpVoucher()[0].trim() + "\")");
            }
            else{
                //win.eval("PostImpresion(\"" + Defines.Impresion_SoloSticker + "\",\"" + nroTicketsImpresos +"\")");
                win.eval("PostImpresion(\"" + Defines.Impresion_SoloSticker + "\",\"" + nroTicketsImpresos + "\",\"" +
                        puertoSerial.getConfigImpSticker()[0].trim() + "\")");
            }
        }
        else if(getFlagVoucher().equals("1")){
            //win.eval("PostImpresion(\"" + Defines.Impresion_SoloVoucher + "\")");
            win.eval("PostImpresion(\"" + Defines.Impresion_SoloVoucher + "\",\"" + puertoSerial.getConfigImpVoucher()[0].trim() + "\")");
        }
        else{
            win.eval("PostImpresionError(\"" + Defines.Error_NoControlado + "\")");
        }
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="ExitNotOk">
    private void ExitNotOk(){
        JSObject win = (JSObject) JSObject.getWindow(this);
        if(this.getFlagSticker().equals("1")){
            int nroTicketsImpresos = GetNroTicketsImpresos();
            if(this.getFlagVoucher().equals("1")){
                win.eval("PostImpresionNoImprimio(\"" + Defines.Error_StickerConVoucher + "\",\"" + nroTicketsImpresos +"\")");
            }
            else{
                win.eval("PostImpresionNoImprimio(\"" + Defines.Error_SoloSticker + "\",\"" + nroTicketsImpresos +"\")");
            }
        }
        else if(this.getFlagVoucher().equals("1")){
            win.eval("PostImpresionNoImprimio(\"" + Defines.Error_SoloVoucher + "\")");
        }
        else{
            win.eval("PostImpresionError(\"" + Defines.Error_NoControlado + "\")");
        }
        //System.exit(0);//Simula fin de applet
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="SalirPorUsuario">
    private void SalirPorUsuario(){
        JSObject win = (JSObject) JSObject.getWindow(this);
        if(this.getFlagSticker().equals("1")){
            int nroTicketsImpresos = GetNroTicketsImpresos();
            if(this.getFlagVoucher().equals("1")){
                win.eval("PostImpresionNoImprimio(\"" + Defines.Salir_StickerConVoucher + "\",\"" + nroTicketsImpresos +"\")");
            }
            else{
                win.eval("PostImpresionNoImprimio(\"" + Defines.Salir_SoloSticker + "\",\"" + nroTicketsImpresos +"\")");
            }
        }
        else if(this.getFlagVoucher().equals("1")){
            win.eval("PostImpresionNoImprimio(\"" + Defines.Salir_SoloVoucher + "\")");
        }
        else{
            win.eval("PostImpresionError(\"" + Defines.Error_NoControlado + "\")");
        }
        //System.exit(0);//Simula fin de applet
    }
    // </editor-fold>

}
