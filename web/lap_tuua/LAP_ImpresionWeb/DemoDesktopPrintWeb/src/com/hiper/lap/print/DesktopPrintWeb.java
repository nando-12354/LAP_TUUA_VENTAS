package com.hiper.lap.print;

import javax.swing.JOptionPane;

/**
 *
 * @author Esteban Aliaga Geldres
 */
public class DesktopPrintWeb extends javax.swing.JFrame {

    private Rs232 puertoSerial;

    private String[] textoVoucher;
    private String[] textoSticker;

    public DesktopPrintWeb() {
        initComponents();
        this.jBtnVerEstadoImpresoraVoucher.setEnabled(false);
        this.jBtnVerEstadoImpresoraSticker.setEnabled(false);
        this.jBtnImprimirImpresoraVoucher.setEnabled(false);
        this.jBtnImprimirImpresoraSticker.setEnabled(false);
    }

    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jBtnInicializa = new javax.swing.JButton();
        jBtnVerEstadoImpresoraVoucher = new javax.swing.JButton();
        jBtnVerEstadoImpresoraSticker = new javax.swing.JButton();
        jBtnImprimirImpresoraVoucher = new javax.swing.JButton();
        jBtnImprimirImpresoraSticker = new javax.swing.JButton();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("Demo Desktop PrintWeb");
        getContentPane().setLayout(null);

        jBtnInicializa.setText("Inicializa");
        jBtnInicializa.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jBtnInicializaActionPerformed(evt);
            }
        });
        getContentPane().add(jBtnInicializa);
        jBtnInicializa.setBounds(20, 20, 75, 23);

        jBtnVerEstadoImpresoraVoucher.setText("Ver Estado Impresora Voucher");
        jBtnVerEstadoImpresoraVoucher.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jBtnVerEstadoImpresoraVoucherActionPerformed(evt);
            }
        });
        getContentPane().add(jBtnVerEstadoImpresoraVoucher);
        jBtnVerEstadoImpresoraVoucher.setBounds(130, 60, 181, 23);

        jBtnVerEstadoImpresoraSticker.setText("Ver Estado Impresora Sticker");
        jBtnVerEstadoImpresoraSticker.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jBtnVerEstadoImpresoraStickerActionPerformed(evt);
            }
        });
        getContentPane().add(jBtnVerEstadoImpresoraSticker);
        jBtnVerEstadoImpresoraSticker.setBounds(130, 20, 180, 23);

        jBtnImprimirImpresoraVoucher.setText("Imprimir Impresora Voucher");
        jBtnImprimirImpresoraVoucher.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jBtnImprimirImpresoraVoucherActionPerformed(evt);
            }
        });
        getContentPane().add(jBtnImprimirImpresoraVoucher);
        jBtnImprimirImpresoraVoucher.setBounds(340, 60, 170, 23);

        jBtnImprimirImpresoraSticker.setText("Imprimir Impresora Sticker");
        jBtnImprimirImpresoraSticker.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jBtnImprimirImpresoraStickerActionPerformed(evt);
            }
        });
        getContentPane().add(jBtnImprimirImpresoraSticker);
        jBtnImprimirImpresoraSticker.setBounds(340, 20, 170, 23);

        java.awt.Dimension screenSize = java.awt.Toolkit.getDefaultToolkit().getScreenSize();
        setBounds((screenSize.width-550)/2, (screenSize.height-129)/2, 550, 129);
    }// </editor-fold>//GEN-END:initComponents

    private void jBtnInicializaActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jBtnInicializaActionPerformed
        try{
            puertoSerial = new Rs232();

            //Cabecera y 20 Stickers:
            String cabecera_sticker = "^XA^DFR:HIPERDOL.ZPL^FS^FO210,53^A0B,17,17^CI13^FR^FN1^FS^BY2^FO349,104^BCN,92,Y,N,N^FR^FN2^FS^FO230,164^A0N,49,42^CI13^FR^FN3^FS^FO230,208^A0N,19,21^CI13^FR^FN4^FS^FO230,228^A0N,19,21^CI13^FR^FN5^FS^XZ@";
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

            String stickerCon13Digitos ="^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>:8000000020123^FS^FN3^FD20,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";


            String stickerX = cabecera_sticker +
                    sticker1 + sticker2 + sticker3 + sticker4 + sticker5 + sticker6 + sticker7 + sticker8 + sticker9 + sticker10 +
                    sticker11 + sticker12 + sticker13 + sticker14 + sticker15 + sticker16 + sticker17 + sticker18 + sticker19 + sticker20;

            String sticker = cabecera_sticker +
                    stickerCon13Digitos;

            //Cabecera y un solo Sticker:
            //String cabecera_sticker = "^XA^DFR:HIPERDOL.ZPL^FS^FO210,53^A0B,17,17^CI13^FR^FN1^FS^BY2^FO349,104^BCN,92,Y,N,N^FR^FN2^FS^FO230,164^A0N,49,42^CI13^FR^FN3^FS^FO230,208^A0N,19,21^CI13^FR^FN4^FS^FO230,228^A0N,19,21^CI13^FR^FN5^FS^XZ@";
            //String sticker1 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;8000000001^FS^FN3^FD01,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";

            //String sticker = cabecera_sticker + sticker1;
            
            String voucher = "hola mundo@El breve instante que es la vida...@En que momento de mi largo caminar@Billie Jean que buen tema, bien Michael aunque thriller no se queda atrás...@La frase del día... Vive la vida y no dejes que la vida te viva!!!!...@Mañana nadie viene... asi no se puede chambear.....@";


            puertoSerial.setConfigImpVoucher("COM4,9600,N,8,1");
            puertoSerial.setConfigImpSticker("COM3,9600,N,8,1");
            this.setTextoVoucher(voucher.split("\\@"));
            this.setTextoSticker(sticker.split("\\@"));

            boolean startedPort_ImpresoraVoucher = puertoSerial.StartPort(Variables.FLAG_PRINTER_VOUCHER);
            boolean startedPort_ImpresoraSticker = puertoSerial.StartPort(Variables.FLAG_PRINTER_STICKER);

            if(startedPort_ImpresoraVoucher)
                this.jBtnVerEstadoImpresoraVoucher.setEnabled(true);
            else
                JOptionPane.showMessageDialog(null, "Debe configurar manualmente el puerto \npara la impresora de voucher", "Impresión", JOptionPane.ERROR_MESSAGE);
            if(startedPort_ImpresoraSticker)
                this.jBtnVerEstadoImpresoraSticker.setEnabled(true);
            else
                JOptionPane.showMessageDialog(null, "Debe configurar manualmente el puerto \npara la impresora de sticker", "Impresión", JOptionPane.ERROR_MESSAGE);
        }catch(Exception ex){
            JOptionPane.showMessageDialog(null, ex.getMessage() , "Error No especificado", JOptionPane.ERROR_MESSAGE);
        }

}//GEN-LAST:event_jBtnInicializaActionPerformed

    private void jBtnVerEstadoImpresoraVoucherActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jBtnVerEstadoImpresoraVoucherActionPerformed
        int resultadoTesteo;
        try{
            resultadoTesteo = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER);
            if (resultadoTesteo == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE) {
                this.jBtnImprimirImpresoraVoucher.setEnabled(true);
            }
            else{
                this.jBtnImprimirImpresoraVoucher.setEnabled(true);//volverlo a false
                JOptionPane.showMessageDialog(null, "Testeo incorrecto Voucher : " + resultadoTesteo , "Error testeo", JOptionPane.ERROR_MESSAGE);
            }

        }catch(Exception ex){
            JOptionPane.showMessageDialog(null, ex.getMessage() , "Error No especificado", JOptionPane.ERROR_MESSAGE);

        }
    }//GEN-LAST:event_jBtnVerEstadoImpresoraVoucherActionPerformed

    private void jBtnVerEstadoImpresoraStickerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jBtnVerEstadoImpresoraStickerActionPerformed
        int resultadoTesteo;
        try{
            resultadoTesteo = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER);
            if (resultadoTesteo == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                this.jBtnImprimirImpresoraSticker.setEnabled(true);
            }
            else{
                this.jBtnImprimirImpresoraSticker.setEnabled(true);//volverlo a false
                JOptionPane.showMessageDialog(null, "Testeo incorrecto Sticker : " + resultadoTesteo , "Error testeo", JOptionPane.ERROR_MESSAGE);
            }
        }catch(Exception ex){
            JOptionPane.showMessageDialog(null, ex.getMessage() , "Error No especificado", JOptionPane.ERROR_MESSAGE);
        }

    }//GEN-LAST:event_jBtnVerEstadoImpresoraStickerActionPerformed

    private void jBtnImprimirImpresoraVoucherActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jBtnImprimirImpresoraVoucherActionPerformed
        this.jBtnImprimirImpresoraVoucher.setEnabled(false);
        try{
            puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
        }catch(Exception ex){
            JOptionPane.showMessageDialog(null, ex.getMessage() , "Error No especificado", JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_jBtnImprimirImpresoraVoucherActionPerformed

    private void jBtnImprimirImpresoraStickerActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jBtnImprimirImpresoraStickerActionPerformed
        this.jBtnImprimirImpresoraSticker.setEnabled(false);
        try{
            System.out.println("\nImprimiendo...\n");
            puertoSerial.escribirTexto2(getTextoSticker(), Variables.FLAG_PRINTER_STICKER);
        }catch(Exception ex){
            JOptionPane.showMessageDialog(null, ex.getMessage() , "Error No especificado", JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_jBtnImprimirImpresoraStickerActionPerformed

    public static void main(String args[]) {
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new DesktopPrintWeb().setVisible(true);
            }
        });
    }

    // <editor-fold defaultstate="collapsed" desc="Propiedades">
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

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton jBtnImprimirImpresoraSticker;
    private javax.swing.JButton jBtnImprimirImpresoraVoucher;
    private javax.swing.JButton jBtnInicializa;
    private javax.swing.JButton jBtnVerEstadoImpresoraSticker;
    private javax.swing.JButton jBtnVerEstadoImpresoraVoucher;
    // End of variables declaration//GEN-END:variables

}
