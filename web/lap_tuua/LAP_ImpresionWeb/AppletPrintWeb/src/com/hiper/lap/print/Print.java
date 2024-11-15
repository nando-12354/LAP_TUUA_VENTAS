package com.hiper.lap.print;


import java.awt.Color;
import java.awt.Component;
import java.awt.Container;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;
import java.io.StringReader;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Hashtable;
import java.util.List;
import javax.swing.ImageIcon;
import javax.swing.JApplet;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import netscape.javascript.*;
import org.jdom.Document;
import org.jdom.Element;
import org.jdom.JDOMException;
import org.jdom.input.SAXBuilder;

/**
 *
 * @author ggarcia/ESTEBAN ALIAGA GELDRES
 */
public class Print extends JApplet implements Runnable {

    private Rs232 puertoSerial;

    private String flagSticker;
    private String flagVoucher;

    private String[] textoVoucher;
    private String[] textoSticker;

    // <editor-fold defaultstate="collapsed" desc="init">
    @Override
    public void init() {
        System.out.println("Inicio Init : " + new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").format(new Date()));

        try {
            puertoSerial = new Rs232();

            //Flag impresora sticker
            String parametro = getParameter(Variables.FlagImpresoraSticker);
            System.out.println("Print.init() - FlagImpresoraSticker:" + parametro);
            this.setFlagSticker(parametro);

            //Flag impresora voucher
            parametro = getParameter(Variables.FlagImpresoraVoucher);
            System.out.println("Print.init() - FlagImpresoraVoucher:" + parametro);
            this.setFlagVoucher(parametro);

            if(this.getFlagSticker().equals("1")){
                //Configuracion de la impresora de sticker
                parametro = getParameter(Variables.ConfiguracionImpresoraSticker);
                System.out.println("Print.init() - ConfiguracionImpresoraSticker:" + parametro);
                if (parametro == null || parametro.length() == 0) {
                    throw new Exception("La impresora de sticker no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpSticker(parametro);

                String dataSticker = getParameter(Variables.DataSticker);
                this.setTextoSticker(dataSticker.split("\\@"));

                if(this.getFlagVoucher().equals("1")){
                    //Configuracion de la impresora de voucher
                    parametro = getParameter(Variables.ConfiguracionImpresoraVoucher);
                    System.out.println("Print.init() - ConfiguracionImpresoraVoucher:" + parametro);
                    if (parametro == null || parametro.length() == 0) {
                        throw new Exception("La impresora de voucher no tiene una configuración válida.");
                    }
                    puertoSerial.setConfigImpVoucher(parametro);

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

                String dataVoucher = getParameter(Variables.DataVoucher);
                this.setTextoVoucher(dataVoucher.split("\\@"));
            }

            GuiShowMessage(Variables.MENSAJE_PROCESANDO);
            new Thread(this).start();

            //Thread.sleep(500);//Para efectos de que se vea el jpanel de procesando apenas cargue el applet y no se vea el efecto del applet plomo.
           
        } catch (ExceptionInInitializerError e1) {
            System.out.println("Print.init() Error1 " + e1.getMessage());
            //JOptionPane.showMessageDialog(null, "Verifique que los archivos RXTXcomm.jar y rxtxSeria.dll se encuentren en su PC." , "Impresión", JOptionPane.ERROR_MESSAGE);
            System.err.println("JOptionPane.showMessageDialog 1");
        } catch (Exception e2) {
            System.out.println("Print.init() Error2 " + e2.getMessage());
            //JOptionPane.showMessageDialog(null, e2.getMessage() , "Impresión", JOptionPane.ERROR_MESSAGE);
            System.err.println("JOptionPane.showMessageDialog 2");
        }
        System.out.println("Saliendo del Init: " + new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").format(new Date()));
    }
    // </editor-fold>

    @Override
    public void run() {
//        //El sgte sleep es para probar como se ve el applet cuando se termina el init(), se ve plomo.
//        //Luego con el GuiProcesando(), se actualiza (anteriormente se necesitaba tambien el repaint() al final de este metodo. ya no es necesario.)
//        try{
//            Thread.sleep(1000);
//
//        }
//        catch(Exception ex){
//
//        }

        //GuiProcesando();

        try {
            Thread.sleep(1000);//Que se vea el efecto de Procesando por al menos un segundo.

        } catch (Exception ex) {
        }

        TestearPrinters();
    }

    private void TestearPrinters() {

        boolean estadoImpresoraVoucher = true;
        boolean estadoImpresoraSticker = true;
        if (this.getFlagVoucher().equals("1")) {
            if (puertoSerial.StartPort(Variables.FLAG_PRINTER_VOUCHER)) {
                System.out.println("Print.TestearPrinters() - Se abrio el puerto de la Impresora de Voucher.");
                int reintentos = 0;
                int resultadoTesteo;
                while (true) {
                    if ((resultadoTesteo = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER)) != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE) {
                        reintentos++;
                        if (reintentos >= 2) {
                            estadoImpresoraVoucher = false;
                            System.err.println("Print.TestearPrinters() - Se testeo incorrectamente la Impresora de Voucher en el reintento " + reintentos + " - Retorno: " + resultadoTesteo + ".");
                            puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                            break;
                        }
                        System.err.println("Print.TestearPrinters() - Se testeo incorrectamente la Impresora de Voucher - Retorno: " + resultadoTesteo + ".");
                        try {
                            Thread.sleep(250);
                        } catch (InterruptedException ex) {
                        }
                    } else {
                        System.out.println("Print.TestearPrinters() - Se testeo correctamente la Impresora de Voucher.");
                        break;
                    }
                }

            } else {
                estadoImpresoraVoucher = false;
                System.err.println("Print.TestearPrinters() - No se logro abrir el puerto de la Impresora de Voucher.");
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
            }
        }
        if (this.getFlagSticker().equals("1")) {
            if (puertoSerial.StartPort(Variables.FLAG_PRINTER_STICKER)) {
                System.out.println("Print.TestearPrinters() - Se abrio el puerto de la Impresora de Sticker.");
                int reintentos = 0;
                int resultadoTesteo;
                while (true) {
                    if ((resultadoTesteo = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                        reintentos++;
                        if (reintentos >= 2) {
                            estadoImpresoraSticker = false;
                            System.err.println("Print.TestearPrinters() - Se testeo incorrectamente la Impresora de Sticker en el reintento " + reintentos + " - Retorno: " + resultadoTesteo + ".");
                            puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                            break;
                        }
                        System.err.println("Print.TestearPrinters() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " + resultadoTesteo + ".");
                        try {
                            Thread.sleep(250);
                        } catch (InterruptedException ex) {
                        }
                    } else {
                        System.out.println("Print.TestearPrinters() - Se testeo correctamente la Impresora de Sticker.");
                        break;
                    }
                }

            } else {
                estadoImpresoraSticker = false;
                System.err.println("Print.TestearPrinters() - No se logro abrir el puerto de la Impresora de Sticker.");
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
            }
        }

        if (estadoImpresoraVoucher == false && estadoImpresoraSticker == false) {
            javax.swing.SwingUtilities.invokeLater(new Runnable() {

                @Override
                public void run() {
                    crateGuiConfiguracionManualPuertos(true, true);
                    JOptionPane.showMessageDialog(null, "Debe configurar manualmente los puertos \npara las impresoras de voucher y sticker", "Impresión", JOptionPane.ERROR_MESSAGE);
                }
            });
        } else if (estadoImpresoraVoucher == false) {
            javax.swing.SwingUtilities.invokeLater(new Runnable() {

                @Override
                public void run() {
                    crateGuiConfiguracionManualPuertos(true, false);
                    JOptionPane.showMessageDialog(null, "Debe configurar manualmente el puerto \npara la impresora de voucher", "Impresión", JOptionPane.ERROR_MESSAGE);
                }
            });

        //Si al fin se abrio y se testeo correctamente el puerto para las respectivas impresoras, procedemos a enviar a imprimir.
        } else{
            GuiShowMessage(Variables.MENSAJE_IMPRIMIENDO);
            if (this.getFlagSticker().equals("1")) {
                puertoSerial.escribirTexto(getTextoSticker(), Variables.FLAG_PRINTER_STICKER);

                if(this.getFlagVoucher().equals("1")){


                    
                    puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);




                    
                }
            }
            else if (this.getFlagVoucher().equals("1")){
                puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
            }
        }
    }

    private void CargarDataVoucher(){
        String xmlFormatoVoucher = getParameter(Variables.XmlFormatoVoucher);
        xmlFormatoVoucher = "<documents>" + xmlFormatoVoucher + "</documents>";
        Hashtable htPrintData = new Hashtable();
        CargarParametrosImpresion(htPrintData);

        SAXBuilder builder = new SAXBuilder(false);
        Document doc = null;
        try {
            doc = builder.build(new StringReader(xmlFormatoVoucher));
        } catch (JDOMException ex) {
        } catch (IOException ex) {
        }
        List<Element> nodos = doc.getRootElement().getChildren();
        Element nodo = nodos.get(0);

        Xml xml = new Xml();
        String[] textoVoucherPrint = xml.obtenerDocumento(htPrintData, nodo);
        this.setTextoVoucher(textoVoucherPrint);
    }

    //Este metodo es cuando no se puede imprimir en la impresora de voucher, entonces debo mandar la data de voucher en forma de string a la pagina aspnet.
    //Esto con el proposito del string que se guarde en BD, para luego reimprimirlo.
    private String GetDataVoucherFormateada(){
        String dataVoucherFormateada = "";
        for(int i=0; i<this.getTextoVoucher().length; i++){
            dataVoucherFormateada += getTextoVoucher()[i] + "@";
        }
        return dataVoucherFormateada;
    }

    private String GetListaCodigoTicketsNoImpresos(){
        String listaCodigoTicketsNoImpresos = "";
        String listaCodigoTickets = getParameter(Variables.ListaCodigoTickets);
        String tam_Ticket = getParameter(Variables.Tam_Ticket);
        int Can_TicketImpresos = puertoSerial.getCantidadStickersImpresos();

        listaCodigoTicketsNoImpresos = listaCodigoTickets.substring(Can_TicketImpresos * Integer.parseInt(tam_Ticket));
        return listaCodigoTicketsNoImpresos;
    }

    // <editor-fold defaultstate="collapsed" desc="CargarParametrosImpresion">
    private void CargarParametrosImpresion(Hashtable htPrintData){
        String nombre_Cajero = getParameter(Variables.Nombre_Cajero);
        String imp_Precio = getParameter(Variables.Imp_Precio);
        String dsc_Simbolo = getParameter(Variables.Dsc_Simbolo);
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
            System.err.println("Error: " + ex.getMessage());
            imp_Precio = imp_Precio.replace(',', '.');
            dImp_Precio = Float.valueOf(imp_Precio);
        }
        DecimalFormat df = new DecimalFormat("#.00");
        htPrintData.put(Defines.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET, df.format(dImp_Precio) + " " + dsc_Simbolo);
        htPrintData.put(Defines.ID_PRINTER_PARAM_TOTAL_PAGAR, df.format(dImp_Precio * Can_Ticket) + " " + dsc_Simbolo);
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="crateGuiConfiguracionManualPuertos">
    private void crateGuiConfiguracionManualPuertos(final boolean flagVoucher, final boolean flagSticker) {
        getContentPane().removeAll();

        getContentPane().setLayout(null);

        JPanel pnlConfigManualPuertos = new javax.swing.JPanel();
        JLabel lblTituloConfig = new javax.swing.JLabel();
        JButton btnAceptar = new javax.swing.JButton();
        JButton btnSalir = new javax.swing.JButton();
        JButton btnRefresh = new javax.swing.JButton();
        JLabel lblPuertos = new javax.swing.JLabel();
        JLabel lblDisponibles = new javax.swing.JLabel();
        
        pnlConfigManualPuertos.setBackground(new java.awt.Color(255, 255, 255));
        pnlConfigManualPuertos.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        pnlConfigManualPuertos.setLayout(null);

        lblTituloConfig.setFont(new java.awt.Font("Tahoma", 1, 12));
        lblTituloConfig.setForeground(new java.awt.Color(0, 102, 204));
        lblTituloConfig.setText("Configuracion Manual de Puertos");
        pnlConfigManualPuertos.add(lblTituloConfig);
        lblTituloConfig.setBounds(40, 20, 230, 14);

        lblPuertos.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblPuertos.setText("Puertos");
        pnlConfigManualPuertos.add(lblPuertos);
        lblPuertos.setBounds(180, 62, 60, 14);

        lblDisponibles.setFont(new java.awt.Font("Tahoma", 1, 11));
        lblDisponibles.setText("Disponibles");
        pnlConfigManualPuertos.add(lblDisponibles);
        lblDisponibles.setBounds(170, 80, 70, 14);

        btnAceptar.setText("Aceptar");
        btnAceptar.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {
                btnAceptarActionPerformed(e, flagVoucher, flagSticker);
            }
        });
        pnlConfigManualPuertos.add(btnAceptar);
        btnAceptar.setBounds(50, 220, 80, 23);

        btnSalir.setText("Salir");
        btnSalir.addActionListener(new ActionListener() {

            @Override
            public void actionPerformed(ActionEvent e) {
                btnSalirActionPerformed(e);
            }
        });

        pnlConfigManualPuertos.add(btnSalir);
        btnSalir.setBounds(160, 220, 80, 23);

        btnRefresh.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)));
        btnRefresh.setFocusable(false);

        ImageIcon image = new ImageIcon(getClass().getResource("/resources/Refresh.png"));
        btnRefresh.setIcon(image);
        btnRefresh.addActionListener(new java.awt.event.ActionListener() {
            
            @Override
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnRefreshActionPerformed(evt);
            }
        });
        pnlConfigManualPuertos.add(btnRefresh);
        btnRefresh.setBounds(240, 63, 25, 28);

        getContentPane().add(pnlConfigManualPuertos);
        pnlConfigManualPuertos.setBounds(0, 0, 300, 300);

        if (flagVoucher) {
            JComboBox cboPtosVoucher = new javax.swing.JComboBox();
            cboPtosVoucher.setName("cboPtosVoucher");
            JLabel lblVoucher = new javax.swing.JLabel();
            lblVoucher.setFont(new java.awt.Font("Tahoma", 1, 11));
            lblVoucher.setText("Impresora Voucher");
            pnlConfigManualPuertos.add(lblVoucher);
            lblVoucher.setBounds(30, 120, 120, 14);
            pnlConfigManualPuertos.add(cboPtosVoucher);
            llenarCombo(cboPtosVoucher);
            cboPtosVoucher.setBounds(175, 115, 85, 22);
        }
        if (flagSticker) {
            JComboBox cboPtosSticker = new javax.swing.JComboBox();
            cboPtosSticker.setName("cboPtosSticker");
            JLabel lblSticker = new javax.swing.JLabel();
            lblSticker.setFont(new java.awt.Font("Tahoma", 1, 11));
            lblSticker.setText("Impresora Sticker");
            pnlConfigManualPuertos.add(lblSticker);
            lblSticker.setBounds(30, 165, 120, 14);
            pnlConfigManualPuertos.add(cboPtosSticker);
            llenarCombo(cboPtosSticker);
            cboPtosSticker.setBounds(175, 160, 85, 22);
        }
    }

    private void btnRefreshActionPerformed(ActionEvent e){
        Component[] controles = ((Container)getContentPane().getComponent(0)).getComponents();
        for(Component c : controles){
            if(c instanceof JComboBox){
                JComboBox cboPtos = (JComboBox)c;
                cboPtos.removeAllItems();
                llenarCombo(cboPtos);
                //System.out.println("Refrescando la lista de puertos en el combobox.");
                break;
            }
        }
    }

    public void llenarCombo(JComboBox cmb) {
        try {
            ArrayList<String> portList = Rs232.GetPortIdentifiers();
            for (String puerto : portList) {
                cmb.addItem(puerto);
            }
        } catch (Exception e) {
            System.out.println(e.toString());
        }
    }

    private void btnAceptarActionPerformed(ActionEvent e, boolean flagVoucher, boolean flagSticker ) {
        String puertoVoucherSelected = null;
        String puertoStickerSelected = null;
        if(flagVoucher){
            Component[] controles = ((Container)getContentPane().getComponent(0)).getComponents();
            for(Component c : controles){
                if(c.getName()!=null && c.getName().equals("cboPtosVoucher")){
                    JComboBox cboPtosVoucher = (JComboBox)c;
                    if(cboPtosVoucher.getSelectedIndex()>-1){
                        puertoVoucherSelected = cboPtosVoucher.getSelectedItem().toString();
                    }
                    break;
                }
            }
            if(puertoVoucherSelected==null){
                JOptionPane.showMessageDialog(null, "Debe seleccionar un puerto \npara la impresora de voucher", "Impresión", JOptionPane.ERROR_MESSAGE);
                return;
            }
        }
        if(flagSticker){
            Component[] controles = ((Container)getContentPane().getComponent(0)).getComponents();
            for(Component c : controles){
                if(c.getName()!=null && c.getName().equals("cboPtosSticker")){
                    JComboBox cboPtosSticker = (JComboBox)c;
                    if(cboPtosSticker.getSelectedIndex()>-1){
                        puertoStickerSelected = cboPtosSticker.getSelectedItem().toString();
                    }
                    break;
                }
            }
            if(puertoStickerSelected==null){
                JOptionPane.showMessageDialog(null, "Debe seleccionar un puerto \npara la impresora de sticker", "Impresión", JOptionPane.ERROR_MESSAGE);
                return;
            }
        }
        if(flagSticker && flagVoucher){
            if(puertoVoucherSelected.equals(puertoStickerSelected)){
                JOptionPane.showMessageDialog(null, "Debe seleccionar puertos distintos \npara la impresora de sticker y voucher", "Impresión", JOptionPane.ERROR_MESSAGE);
                return;
            }
        }
        if(flagVoucher){
            puertoSerial.getConfigImpVoucher()[0] = puertoVoucherSelected;
        }
        if(flagSticker){
            puertoSerial.getConfigImpSticker()[0] = puertoStickerSelected;
        }

        createGUIShowMessage(Variables.MENSAJE_PROCESANDO);
        new Thread(this).start();
    }

    private void btnSalirActionPerformed(ActionEvent e) {
        //Confirmacion

        //Envio la lista de tickets.
        
        //Llamo a una funcion javascript.
    }
    // </editor-fold>

    // <editor-fold defaultstate="collapsed" desc="stop, destroy methods">
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

    // <editor-fold defaultstate="collapsed" desc="GuiShowMessage">
    public void GuiShowMessage(final String mensaje) {
        try {
            javax.swing.SwingUtilities.invokeAndWait(new Runnable() {

                @Override
                public void run() {
                    createGUIShowMessage(mensaje);
                }
            });
            //System.out.println("Saliendo del GuiShowMessage: " + new SimpleDateFormat("yyyy/MM/dd HH:mm:ss").format(new Date()));
        } catch (Exception e) {
            System.err.println("createGUIShowMessage didn't successfully complete");
        }
        
    }

    public void createGUIShowMessage(final String mensaje) {
        getContentPane().removeAll();

        getContentPane().setLayout(null);

        //getContentPane().setBackground(Color.WHITE);

        JPanel pnlGifAnimado = new JPanel();
        pnlGifAnimado.setLayout(null);
        getContentPane().add(pnlGifAnimado);
        pnlGifAnimado.setBackground(Color.WHITE);
        pnlGifAnimado.setBounds(0, 0, getWidth(), getHeight());
        //pnlGifAnimado.setOpaque(true);

        JLabel lblGifAnimado = new JLabel();
        pnlGifAnimado.add(lblGifAnimado);
        lblGifAnimado.setOpaque(false);//Hace que sea transparente. Es por default.
        ImageIcon image = new ImageIcon(getClass().getResource("/resources/ajax-loader.gif"));
//        System.out.println("getClass().getName():" + getClass().getName());
//        System.out.println("getPath():" + getClass().getResource("resources/ajax-loader.gif").getPath());
//        System.out.println("getQuery():" + getClass().getResource("resources/ajax-loader.gif").getQuery());
//        System.out.println("getFile():" + getClass().getResource("resources/ajax-loader.gif").getFile());
//        System.out.println("getHost():" + getClass().getResource("resources/ajax-loader.gif").getHost());
        //ImageIcon image = new ImageIcon("resources/ajax-loader.gif");

        lblGifAnimado.setIcon(image);

//        System.out.println("Long. lblGifAnimado:" + lblGifAnimado.getWidth());
//        System.out.println("Long. image:" + image.getIconWidth());
//        System.out.println("Long. Applet:" + getWidth()) ;
        lblGifAnimado.setBounds(getWidth() / 2 - image.getIconWidth() / 2, 0, getWidth(), getHeight());//Alineado al centro.

        JLabel lblMsgProc = new JLabel(mensaje);
        pnlGifAnimado.add(lblMsgProc);
        lblMsgProc.setBounds(120, 80, 130, 20);

//        System.out.println("Long. lblGifAnimado:" + lblGifAnimado.getWidth());

        //Lo siguiente ya no es necesario, por el truco del setBounds.
        //repaint();//Es necesario porseacaso GuiProcesando(); se ejecute antes que el hilo init() acabe, pues este pone el applet en plomo, y no cambia.
    //No funciona con lo que sigue...
//        lblGifAnimado.setVisible(true);
//        lblMsgProc.setVisible(true);
//        pnlGifAnimado.setVisible(true);
//        getContentPane().setVisible(true);
//        System.out.println("Ya son visbiles");

    }
    // </editor-fold>

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
    // </editor-fold>


//    public int respuesta;
//    public boolean isFinished = false;
//
//    public int SeAbrieronLosPuertos(){
//
//        while(!isFinished){
//            try{
//                Thread.sleep(5000);
//                System.out.println("Thread current:" + Thread.currentThread().getName());
//            }
//            catch(Exception ex){
//
//            }
//        }
//        return respuesta;
//    }
}
