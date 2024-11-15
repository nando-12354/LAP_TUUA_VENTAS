﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.PRINTER
{
    public partial class frmPrintNet : Form
    {
        private Rs232 puertoSerial;

        public Rs232 PuertoSerial
        {
            get
            {
                return puertoSerial;
            }
        }


        private String flagSticker;
        private String flagVoucher;

        private String[] textoSticker;
        private String[] textoVoucher;

        //private int nroImpresionesVoucher;

        private bool enabledPrintSticker;
        private bool enabledPrintVoucher;

        //c#
        private Hashtable htParametros;

        private Hashtable listaParamImp;

        public frmPrintNet(String[] parametros, Hashtable listaParamImp)
        {
            this.listaParamImp = listaParamImp;

            InitializeComponent();
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            resultado = "";

            init(parametros);
        }

        public frmPrintNet(String[] parametros)
        {
            InitializeComponent();
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);

            resultado = "";

            init(parametros);
        }

        public void setParameter(String item, String valor)
        {
            htParametros.Add(item, valor);
        }

        public String getParameter(String item)
        {
            String valor = (String)htParametros[item];
            if (valor == null)
                valor = "";
            return valor;
        }

        #region init

        public void init(String[] parametros)
        {
            try
            {
                htParametros = new Hashtable();
                for (int i = 0; i < parametros.Length; i++)
                {
                    String parametro = parametros[i];
                    String param_key = parametro.Split(new char[] { '=' }, 2)[0];
                    String param_valor = parametro.Split(new char[] { '=' }, 2)[1];
                    setParameter(param_key, param_valor);
                }

                puertoSerial = new Rs232();

                enabledPrintVoucher = false;
                enabledPrintSticker = false;

                flagSticker = "0";
                flagVoucher = "1";

                GetAndSetFlags();

                GetAndSetConfiguracionPuertosyTextosAImprimir();

                initComponentsMessage();

                SetShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

                new Thread(new ThreadStart(run)).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("El Modulo de Impresion ha fallado. Error: " + ex.Message, "Error en la Impresion.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.StackTrace);

                ExitNotOk();
            }
        }

        #endregion


        #region Para efectos de probar

        public String getParameter2(String item)
        {
            String retorno = null;
            if (item.Equals(Variables.FlagImpresoraSticker))
            {
                retorno = "1";
            }
            if (item.Equals(Variables.FlagImpresoraVoucher))
            {
                retorno = "1";
            }
            if (item.Equals(Variables.ConfiguracionImpresoraSticker))
            {
                retorno = "COM4,9600,N,8,1";
            }
            if (item.Equals(Variables.ConfiguracionImpresoraVoucher))
            {
                retorno = "COM9,9600,N,8,1";
            }
            if (item.Equals(Variables.CopiasVoucher))
            {
                retorno = "1";
            }
            if (item.Equals(Variables.DataSticker))
            {

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
                String sticker10 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000010^FS^FN3^FD10,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker11 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000011^FS^FN3^FD11,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker12 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000012^FS^FN3^FD12,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker13 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000013^FS^FN3^FD13,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker14 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000014^FS^FN3^FD14,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker15 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000015^FS^FN3^FD15,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker16 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000016^FS^FN3^FD16,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker17 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000017^FS^FN3^FD17,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker18 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000018^FS^FN3^FD18,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker19 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000019^FS^FN3^FD19,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";
                String sticker20 = "^XA^MD10^XFR:HIPERDOL.ZPL^FS^FN1^FDValido hasta el 08/11/2009^FS^FN2^FD>;800000000020^FS^FN3^FD20,00^FS^FN4^FDDOLARES^FS^FN5^FDAMERICANOS^FS^XZ@";

                String sticker = cabecera_sticker +
                        sticker1 + sticker2 + sticker3 + sticker4 + sticker5;// + sticker6 + sticker7 + sticker8 + sticker9 + sticker10;

                retorno = sticker;
            }
            if (item.Equals(Variables.DataVoucher))
            {
                retorno = "@           Extorno de Tickets           @ @Cajero : ADMIN ADMINNN 28/10/2009 04:19:52@ @Codigo Ticket @--------------- @700000400044    @700000100056    @700000100066    @ @Total ticket  : 3          @ @ @ @";
            }
            if (item.Equals(Variables.XmlFormatoVoucher))
            {
                retorno = "<document name=\"VentaTicketsContingencia\" print=\"voucher\"><title>Venta de Tickets de Contingencia</title><line /><body><detail>=CONC(\"Cajero : \",ALIGNLEFT(nombre_cajero;13),ALIGNRIGHT(DDMMYYYY();10),ALIGNRIGHT(HHMMSS();10))</detail><line /><detail>=CONC(\"Tipo  Tick. \",\"Tick. vendidos   \",\"Precio Unit. \")</detail><detail>=CONC(\"----------- \",\"---------------- \",\"------------ \")</detail><detail>=CONC(ALIGNLEFT(descripcion_tipoticket;10),ALIGNRIGHT(cantidad_ticket;15),ALIGNRIGHT(precio_unitario;15))</detail><line /><detail>=CONC(\"Total a pagar : \",ALIGNLEFT(total_pagar;20))</detail><line /></body></document>";
            }
            //if (item.Equals(Variables.Nombre_Cajero))
            //{
            //    retorno = "ADMIN ADMIN";
            //}
            //if (item.Equals(Variables.Imp_Precio))
            //{
            //    retorno = "11.45";
            //}
            //if (item.Equals(Variables.Dsc_Simbolo))
            //{
            //    retorno = "$";
            //}
            //if (item.Equals(Variables.Descripcion_tipoticket))
            //{
            //    retorno = "Infante Nacional Normal";
            //}
            return retorno;
        }

        #endregion


        #region GetNroTicketsImpresos

        private int GetNroTicketsImpresos()
        {
            return puertoSerial.getCantidadStickersImpresos();
        }

        #endregion


        #region GetAndSetFlags

        private void GetAndSetFlags()
        {
            //Flag impresora sticker
            String parametro = getParameter(Variables.FlagImpresoraSticker);
            Console.WriteLine("Print.init() - FlagImpresoraSticker:" + parametro);
            if (parametro != null)
            {
                this.setFlagSticker(parametro);
            }

            //Flag impresora voucher
            parametro = getParameter(Variables.FlagImpresoraVoucher);
            Console.WriteLine("Print.init() - FlagImpresoraVoucher:" + parametro);
            if (parametro != null)
            {
                this.setFlagVoucher(parametro);
            }
        }

        #endregion


        #region SetVisiblePanel

        public delegate void SVPHandler(int tipo, bool display);

        private void SetSetVisiblePanel(int tipo, bool display)
        {
            if (this.InvokeRequired)
                this.Invoke(new SVPHandler(SetVisiblePanel), new object[] { tipo, display });
            else
                SetVisiblePanel(tipo, display);
        }

        private void SetVisiblePanel(int tipo, bool display)
        {
            if (tipo == Variables.FLAG_PRINTER_STICKER)
            {
                pnlSticker.Visible = display;
            }
            else if (tipo == Variables.FLAG_PRINTER_VOUCHER)
            {
                pnlVoucher.Visible = display;//pnlVoucher.setLocation(pnlVoucher.getX(), 100);//Es su location default
            }
        }

        #endregion


        #region SetEnabledBtnImprimir

        public delegate void SEBIHandler(bool enabled);

        private void SetSetEnabledBtnImprimir(bool enabled)
        {
            if (this.InvokeRequired)
                this.Invoke(new SEBIHandler(SetEnabledBtnImprimir), new object[] { enabled });
            else
                SetEnabledBtnImprimir(enabled);
        }

        private void SetEnabledBtnImprimir(bool enabled)
        {
            btnImprimir.Enabled = enabled;
        }

        #endregion


        #region GetAndSetConfiguracionPuertosyTextosAImprimir

        private void GetAndSetConfiguracionPuertosyTextosAImprimir()
        {
            String parametro;
            if (this.getFlagSticker().Equals("1"))
            {
                //Configuracion de la impresora de sticker
                parametro = getParameter(Variables.ConfiguracionImpresoraSticker);
                Console.WriteLine("Print.init() - ConfiguracionImpresoraSticker:" + parametro);
                if (parametro == null || parametro.Length == 0)
                {
                    throw new Exception("La impresora de sticker no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpSticker(parametro);

                String dataSticker = getParameter(Variables.DataSticker).Trim();
                Console.WriteLine("DataSticker obtenido:" + dataSticker);
                dataSticker = dataSticker.Substring(1, dataSticker.Length - 2);//dataSticker.Substring(1) = dataSticker.Substring(1, dataSticker.Length - 1) --> dataSticker.Substring(1, dataSticker.Length - 2), pues se elimina el ultimo @, pues el split de c# es diferente al de Java.
                Console.WriteLine("DataSticker real:" + dataSticker);
                this.setTextoSticker(dataSticker.Split('@'));

                showTextosSticker(this.getTextoSticker());
            }
            if (this.getFlagVoucher().Equals("1"))
            {
                //Configuracion de la impresora de voucher
                parametro = getParameter(Variables.ConfiguracionImpresoraVoucher);
                Console.WriteLine("Print.init() - ConfiguracionImpresoraVoucher:" + parametro);
                if (parametro == null || parametro.Length == 0)
                {
                    throw new Exception("La impresora de voucher no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpVoucher(parametro);

                //parametro = getParameter(Variables.CopiasVoucher);
                //this.setNroImpresionesVoucher(Int32.Parse(parametro) + 1);

                String dataVoucher = getParameter(Variables.DataVoucher).Trim();
                //El getParameter hace el trim() implicitamente, pero porseacaso.
                Console.WriteLine("DataVoucher obtenido:" + dataVoucher);
                dataVoucher = dataVoucher.Substring(1, dataVoucher.Length - 2);
                Console.WriteLine("DataVoucher real:" + dataVoucher);
                this.setTextoVoucher(dataVoucher.Split('@'));
                showTextosVoucher(this.getTextoVoucher());
            }
        }


        private void showTextosVoucher(String[] textos)
        {
            for (int i = 0; i < textos.Length; i++)
            {
                Console.WriteLine("TextoVoucher " + i + ":" + textos[i]);
            }
        }

        private void showTextosSticker(String[] textos)
        {
            for (int i = 0; i < textos.Length; i++)
            {
                Console.WriteLine("TextoSticker " + i + ":" + textos[i]);
            }
        }

        #endregion


        #region run inicial

        public void run()
        {
            Thread.Sleep(1000); //Que se vea el efecto de Procesando por al menos un segundo.
            if (ProcesarTesteoPrevioImprimir())
            {
                SetShowAndSetMessage(Variables.MENSAJE_IMPRIMIENDO);
                Imprimir();

                ExitImprimioOk();
            }
        }

        #endregion


        #region ProcesarTesteoPrevioImprimir

        private bool ProcesarTesteoPrevioImprimir()
        {
            if (this.getFlagSticker().Equals("1"))
            {
                int retornoSticker = TestImpresora(Variables.FLAG_PRINTER_STICKER);

                if (this.getFlagVoucher().Equals("1"))
                {
                    int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                    if (retornoVoucher != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE ||
                        retornoSticker != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE)
                    {
                        SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_STICKER, retornoSticker);
                        SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                        SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                        SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, true);
                        //
                        if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
                            retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                        {
                            enabledPrintVoucher = true;
                        }
                        else
                        {
                            enabledPrintVoucher = false;
                        }
                        //
                        if (retornoSticker == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE)
                        {
                            SetSetEnabledBtnImprimir(true);
                            enabledPrintSticker = true;
                        }
                        else
                        {
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
                else
                {
                    //No se da este caso...Reservado para un futuro.
                }
            }
            else if (this.getFlagVoucher().Equals("1"))
            {
                int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);
                if (retornoVoucher != Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                {
                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, false);
                    if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END)
                    {
                        SetSetEnabledBtnImprimir(true);
                        enabledPrintVoucher = true;
                    }
                    else
                    {
                        SetSetEnabledBtnImprimir(false);
                        enabledPrintVoucher = false;
                    }
                    SetShowConfigManualPuertos();
                    return false;
                }
                enabledPrintVoucher = true;
                return true;
            }

            Resultado = Defines.Error_NoControlado;

            //c#
            SetSetClose();

            return false;
        }

        #endregion


        #region ProcesarActualizacionTesteo

        private void ProcesarActualizacionTesteo()
        {
            if (this.getFlagSticker().Equals("1"))
            {
                int retornoSticker = TestImpresora(Variables.FLAG_PRINTER_STICKER);

                if (this.getFlagVoucher().Equals("1"))
                {
                    int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_STICKER, retornoSticker);
                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, true);
                    //
                    if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
                       retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                    {
                        enabledPrintVoucher = true;
                    }
                    else
                    {
                        enabledPrintVoucher = false;
                    }
                    //
                    if (retornoSticker == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE)
                    {
                        SetSetEnabledBtnImprimir(true);
                        enabledPrintSticker = true;
                    }
                    else
                    {
                        SetSetEnabledBtnImprimir(false);
                        enabledPrintSticker = false;
                    }
                }
            }
            else if (this.getFlagVoucher().Equals("1"))
            {
                int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, false);
                if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END ||
                   retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                {
                    SetSetEnabledBtnImprimir(true);
                    enabledPrintVoucher = true;
                }
                else
                {
                    SetSetEnabledBtnImprimir(false);
                    enabledPrintVoucher = false;
                }
            }
            SetShowConfigManualPuertos();
        }

        #endregion


        #region Imprimir

        private void Imprimir()
        {
            if (this.getFlagSticker().Equals("1") && enabledPrintSticker)
            {
                puertoSerial.escribirTexto(getTextoSticker(), Variables.FLAG_PRINTER_STICKER);
                if (this.getFlagVoucher().Equals("1") && enabledPrintVoucher)
                {
                    //----- EAG 28-12-2009
                    if((getTextoSticker().Length - 1) == puertoSerial.getCantidadStickersImpresos())
                    {//Imprimio todos los stickers
                        //for (int i = 0; i < getNroImpresionesVoucher(); i++)
                        //{
                            puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
                        //}                        
                    }
                    else
                    {
                        if(puertoSerial.getCantidadStickersImpresos()==0)
                            return;

                        if (CargarDataVoucher())
                        {
                            //for (int i = 0; i < getNroImpresionesVoucher(); i++)
                            //{
                                puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
                            //}
                        }
                    }
                    //-----

                    /*
                    if (puertoSerial.getCantidadStickersImpresos() > 0)
                    {
                        if (CargarDataVoucher())
                        {
                            for (int i = 0; i < getNroImpresionesVoucher(); i++)
                            {
                                puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
                            }
                        }
                    }
                    */
                }
            }
            else if (this.getFlagVoucher().Equals("1") && enabledPrintVoucher)
            {
                //for (int i = 0; i < getNroImpresionesVoucher(); i++)
                //{
                    puertoSerial.escribirTexto(getTextoVoucher(), Variables.FLAG_PRINTER_VOUCHER);
                //}
            }
        }

        #endregion


        #region CargarDataVoucher

        private bool CargarDataVoucher()
        {
            try
            {
                String xmlFormatoVoucher = getParameter(Variables.XmlFormatoVoucher);
                xmlFormatoVoucher = "<documents>" + xmlFormatoVoucher + "</documents>";

                //Llamada para obtener el nuevo voucher....
                Function.RehacerVoucherTuua(listaParamImp, puertoSerial.getCantidadStickersImpresos(), getParameter(Define.ListaCodigoTickets));
                //CargarParametrosImpresion(listaParamImp, puertoSerial.getCantidadStickersImpresos());

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlFormatoVoucher);

                XmlElement nodo = (XmlElement) doc.ChildNodes[0].ChildNodes[0];

                Xml xml = new Xml();
                String[] textoVoucherPrint = xml.obtenerDocumento(listaParamImp, nodo);
                this.setTextoVoucher(textoVoucherPrint);
                showTextosVoucher(this.getTextoVoucher());
            }
            catch (Exception ex)
            {
                Console.WriteLine("CargarDataVoucher - Error: " + ex.Message);
                return false;
            }
            return true;
        }

        #endregion


        #region CargarParametrosImpresion
/*
        private void CargarParametrosImpresion(Hashtable htPrintData)
        {
            //System.Globalization.CultureInfo.CurrentCulture = new CultureInfo("en-US", true);

            String nombre_Cajero = getParameter(Variables.Nombre_Cajero);
            String imp_Precio = getParameter(Variables.Imp_Precio);
            String dsc_Simbolo = getParameter(Variables.Dsc_Simbolo);
            String descripcion_tipoticket = getParameter(Variables.Descripcion_tipoticket);

            htPrintData.Add(Defines.ID_PRINTER_PARAM_NOMBRE_CAJERO, nombre_Cajero);
            htPrintData.Add(Defines.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET, descripcion_tipoticket);
            int Can_Ticket = puertoSerial.getCantidadStickersImpresos();
            htPrintData.Add(Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Ticket.ToString());
            float dImp_Precio;
            try
            {
                dImp_Precio = float.Parse(imp_Precio);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar Float.valueOf(): " + ex.Message);
                imp_Precio = imp_Precio.Replace(',', '.');
                dImp_Precio = float.Parse(imp_Precio);
            }
            htPrintData.Add(Defines.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET,
                            String.Format(new CultureInfo("en-US", true), "{0:0.00}", dImp_Precio) + " " + dsc_Simbolo);
            htPrintData.Add(Defines.ID_PRINTER_PARAM_TOTAL_PAGAR,
                            String.Format(new CultureInfo("en-US", true), "{0:0.00}", dImp_Precio*Can_Ticket) + " " + dsc_Simbolo);
        }
*/
        #endregion


        #region UpdatePanelConfigManualPuertos

        public delegate void UPCMPHandler(int tipo, int retorno);

        private void SetUpdatePanelConfigManualPuertos(int tipo, int retorno)
        {
            if (this.InvokeRequired)
                this.Invoke(new UPCMPHandler(UpdatePanelConfigManualPuertos), new object[] { tipo, retorno });
            else
                UpdatePanelConfigManualPuertos(tipo, retorno);
        }

        private void UpdatePanelConfigManualPuertos(int tipo, int retorno)
        {
            if (tipo == Variables.FLAG_PRINTER_VOUCHER)
            {
                String puertoAsignadoVoucher = puertoSerial.getConfigImpVoucher()[0].Trim();
                cboPtosVoucher.Items.Clear();
                cboPtosVoucher.Items.Add("- Seleccione -");
                llenarCombo(cboPtosVoucher);
                FileStream fsImagen;
                Image imagen;
                switch (retorno)
                {
                    case Variables.FLAG_PRINTER_PORT_ERROR:
                        lblStatusImpresoraVoucher.Text = Defines.MsgPrinterPortError;
                        lblPuertoImpresoraVoucher.Text = "Puerto asignado incorrectamente: " + puertoAsignadoVoucher;
                        chkPuertoVoucher.Checked = true;
                        cboPtosVoucher.Enabled = true;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageVoucher.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE:
                        lblStatusImpresoraVoucher.Text = Defines.MsgVoucherNotOperative;
                        lblPuertoImpresoraVoucher.Text = "Puerto asignado: " + puertoAsignadoVoucher;
                        chkPuertoVoucher.Checked = true;
                        cboPtosVoucher.SelectedItem = puertoAsignadoVoucher;
                        cboPtosVoucher.Enabled = true;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageVoucher.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE:
                        lblStatusImpresoraVoucher.Text = Defines.MsgVoucherOperative;
                        lblPuertoImpresoraVoucher.Text = "Puerto asignado correctamente: " + puertoAsignadoVoucher;
                        chkPuertoVoucher.Checked = false;
                        cboPtosVoucher.SelectedItem = puertoAsignadoVoucher;
                        cboPtosVoucher.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathGoodStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageVoucher.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END:
                        lblStatusImpresoraVoucher.Text = Defines.MsgVoucherPaperNearEnd;
                        lblPuertoImpresoraVoucher.Text = "Puerto asignado correctamente: " + puertoAsignadoVoucher;
                        chkPuertoVoucher.Checked = false;
                        cboPtosVoucher.SelectedItem = puertoAsignadoVoucher;
                        cboPtosVoucher.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathWarningStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageVoucher.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_END:
                        lblStatusImpresoraVoucher.Text = Defines.MsgVoucherPaperEnd;
                        lblPuertoImpresoraVoucher.Text = "Puerto asignado correctamente: " + puertoAsignadoVoucher;
                        chkPuertoVoucher.Checked = false;
                        cboPtosVoucher.SelectedItem = puertoAsignadoVoucher;
                        cboPtosVoucher.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageVoucher.Image = imagen;
                        break;
                }
                //c#
                if (cboPtosVoucher.SelectedIndex < 0)
                    cboPtosVoucher.SelectedIndex = 0;
            }
            else if (tipo == Variables.FLAG_PRINTER_STICKER)
            {
                String puertoAsignadoSticker = puertoSerial.getConfigImpSticker()[0].Trim();
                cboPtosSticker.Items.Clear();
                cboPtosSticker.Items.Add("- Seleccione -");
                llenarCombo(cboPtosSticker);
                FileStream fsImagen;
                Image imagen;
                switch (retorno)
                {
                    case Variables.FLAG_PRINTER_PORT_ERROR:
                        lblStatusImpresoraSticker.Text = Defines.MsgPrinterPortError;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado incorrectamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = true;
                        cboPtosSticker.Enabled = true;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerNotOperative;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = true;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = true;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerOperative;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = false;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathGoodStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_HEADUP:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerHeadUp;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = false;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerPauseMode;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = false;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_PAPER_OUT:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerPaperOut;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = false;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                    case Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT:
                        lblStatusImpresoraSticker.Text = Defines.MsgStickerRibbonOut;
                        lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: " + puertoAsignadoSticker;
                        chkPuertoSticker.Checked = false;
                        cboPtosSticker.SelectedItem = puertoAsignadoSticker;
                        cboPtosSticker.Enabled = false;
                        fsImagen = new FileStream(Application.StartupPath + Defines.PathBadStatus, FileMode.Open, FileAccess.Read);
                        imagen = Image.FromStream(fsImagen);
                        fsImagen.Close();
                        lblStatusImageSticker.Image = imagen;
                        break;
                }
                //c#
                if (cboPtosSticker.SelectedIndex < 0)
                    cboPtosSticker.SelectedIndex = 0;
            }
        }

        #endregion


        #region TestImpresora

        private int TestImpresora(int tipo)
        {
            int retorno = Variables.FLAG_PRINTER_ERROR;
            if (tipo == Variables.FLAG_PRINTER_VOUCHER)
            {
                bool opened = puertoSerial.StartPort(Variables.FLAG_PRINTER_VOUCHER);
                if (opened)
                {
                    int reintentos = 0;
                    while (true)
                    {
                        if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_VOUCHER)) !=
                            Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                        {
                            reintentos++;
                            if (reintentos >= 2)
                            {
                                Console.WriteLine(
                                    "Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher en el reintento " +
                                    (reintentos - 1) + " - Retorno: " + retorno + ".");
                                if (retorno == Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE)
                                {
                                    puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                                }
                                break;
                            }
                            Console.WriteLine(
                                "Print.TestImpresora() - Se testeo incorrectamente la Impresora de Voucher - Retorno: " +
                                retorno + ".");
                            Thread.Sleep(250);
                        }
                        else
                        {
                            Console.WriteLine("Print.TestImpresora() - Se testeo correctamente la Impresora de Voucher.");
                            break;
                        }
                    }
                }
                else
                {
                    retorno = Variables.FLAG_PRINTER_PORT_ERROR;
                    puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                }
            }
            else if (tipo == Variables.FLAG_PRINTER_STICKER)
            {
                bool opened = puertoSerial.StartPort(Variables.FLAG_PRINTER_STICKER);
                if (opened)
                {
                    int reintentos = 0;
                    while (true)
                    {
                        if ((retorno = puertoSerial.verificarEstadoImpresora(Variables.FLAG_PRINTER_STICKER)) !=
                            Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE)
                        {
                            reintentos++;
                            if (reintentos >= 2)
                            {
                                Console.WriteLine(
                                    "Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker en el reintento " +
                                    (reintentos - 1) + " - Retorno: " + retorno + ".");
                                if (retorno == Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE)
                                {
                                    puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                                }
                                break;
                            }
                            Console.WriteLine(
                                "Print.TestImpresora() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " +
                                retorno + ".");
                            Thread.Sleep(250);
                        }
                        else
                        {
                            Console.WriteLine("Print.TestImpresora() - Se testeo correctamente la Impresora de Sticker.");
                            break;
                        }
                    }
                }
                else
                {
                    retorno = Variables.FLAG_PRINTER_PORT_ERROR;
                    puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                }
            }
            return retorno;
        }

        #endregion


        #region initComponentsMessage

        private void initComponentsMessage()
        {
            pnlGifAnimado = new Panel();
            pnlGifAnimado.BackColor = Color.FromArgb(255, 255, 255);

            lblGifAnimado = new PictureBox();
            FileStream fsImagen = new FileStream(Application.StartupPath + Defines.PathImageLoading, FileMode.Open, FileAccess.Read);
            Image imagen = Image.FromStream(fsImagen);
            //fsImagen.Close();
            lblGifAnimado.Image = imagen;
            lblGifAnimado.SizeMode = PictureBoxSizeMode.AutoSize;
            pnlGifAnimado.Controls.Add(lblGifAnimado);
            lblGifAnimado.Location = new Point(450 / 2 - lblGifAnimado.Width / 2, 450 / 2 - lblGifAnimado.Height / 2);

            lblMsgProc = new Label();
            pnlGifAnimado.Controls.Add(lblMsgProc);
            lblMsgProc.SetBounds(175, 120, 130, 20);//c#
            lblMsgProc.Font = new Font("Verdana", 10, FontStyle.Bold);//c#

            this.Controls.Add(pnlGifAnimado);
            pnlGifAnimado.SetBounds(0, 0, 450, 450);
        }

        #endregion


        #region ShowAndSetMessage

        public delegate void SASMHandler(String message);

        private void SetShowAndSetMessage(String message)
        {
            if (this.InvokeRequired)
                this.Invoke(new SASMHandler(ShowAndSetMessage), new object[] { message });
            else
                ShowAndSetMessage(message);
        }

        private void ShowAndSetMessage(String message)
        {
            pnlConfigManualPuertos.Visible = false;
            pnlGifAnimado.Visible = true;
            lblMsgProc.Text = message;
        }

        #endregion


        #region ShowConfigManualPuertos

        public delegate void SCMPHandler();

        private void SetShowConfigManualPuertos()
        {
            if (this.InvokeRequired)
                this.Invoke(new SCMPHandler(ShowConfigManualPuertos), new object[] { });
            else
                ShowConfigManualPuertos();
        }

        private void ShowConfigManualPuertos()
        {
            pnlConfigManualPuertos.Visible = true;
            pnlGifAnimado.Visible = false;
        }

        #endregion


        #region btnRefresh_Click

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (this.flagSticker.Equals("1"))
            {
                String puertoSelected = cboPtosSticker.SelectedItem.ToString();
                cboPtosSticker.Items.Clear();
                cboPtosSticker.Items.Add("- Seleccione -");
                llenarCombo(cboPtosSticker);
                cboPtosSticker.SelectedItem = puertoSelected;
                //c#
                if (cboPtosSticker.SelectedIndex < 0)
                {
                    cboPtosSticker.SelectedIndex = 0;
                    cboPtosSticker.Enabled = true;
                    chkPuertoSticker.Checked = true;
                }
            }
            if (this.flagVoucher.Equals("1"))
            {
                String puertoSelected = cboPtosVoucher.SelectedItem.ToString();
                cboPtosVoucher.Items.Clear();
                cboPtosVoucher.Items.Add("- Seleccione -");
                llenarCombo(cboPtosVoucher);
                cboPtosVoucher.SelectedItem = puertoSelected;
                //c#
                if (cboPtosVoucher.SelectedIndex < 0)
                {
                    cboPtosVoucher.SelectedIndex = 0;
                    cboPtosVoucher.Enabled = true;
                    chkPuertoVoucher.Checked = true;
                }
            }
        }

        #endregion


        #region btnImprimir_Click

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            ShowAndSetMessage(Variables.MENSAJE_IMPRIMIENDO);
            new Thread(new ThreadStart(run3)).Start();
        }

        public void run3()
        {
            Imprimir();

            ExitImprimioOk();
        }

        #endregion


        #region btnSalir_Click

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del modulo de impresion?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            SalirPorUsuario();
        }

        #endregion


        #region btnActualizar_Click

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (this.flagSticker.Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                String puertoStickerSelected = null;
                if (chkPuertoSticker.Checked)
                {
                    if (cboPtosSticker.SelectedIndex > 0)
                    {
                        puertoStickerSelected = cboPtosSticker.SelectedItem.ToString();
                    }
                    if (puertoStickerSelected == null)
                    {
                        MessageBox.Show("Debe seleccionar un puerto \npara la impresora de sticker", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    puertoStickerSelected = puertoSerial.getConfigImpSticker()[0].Trim();
                }
                puertoSerial.getConfigImpSticker()[0] = puertoStickerSelected;
            }
            if (this.flagVoucher.Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                String puertoVoucherSelected = null;
                if (chkPuertoVoucher.Checked)
                {
                    if (cboPtosVoucher.SelectedIndex > 0)
                    {
                        puertoVoucherSelected = cboPtosVoucher.SelectedItem.ToString();
                    }
                    if (puertoVoucherSelected == null)
                    {
                        MessageBox.Show("Debe seleccionar un puerto \npara la impresora de voucher", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    puertoVoucherSelected = puertoSerial.getConfigImpVoucher()[0].Trim();
                }
                puertoSerial.getConfigImpVoucher()[0] = puertoVoucherSelected;
            }

            ShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

            Thread t = new Thread(new ThreadStart(run2));
            t.Start();
        }

        public void run2()
        {
            Thread.Sleep(1000); //Que se vea el efecto de Procesando por al menos un segundo.
            ProcesarActualizacionTesteo();
        }

        #endregion


        #region chks CheckedChanged

        private void chkPuertoVoucher_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPuertoVoucher.Checked)
            {
                cboPtosVoucher.Enabled = true;
            }
            else
            {
                cboPtosVoucher.Enabled = false;
            }
        }

        private void chkPuertoSticker_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPuertoSticker.Checked)
            {
                cboPtosSticker.Enabled = true;
            }
            else
            {
                cboPtosSticker.Enabled = false;
            }
        }

        #endregion


        //Variables para la ventana de Procesando... e Imprimiendo...
        private Panel pnlGifAnimado;
        private PictureBox lblGifAnimado;
        private Label lblMsgProc;


        #region Propiedades

        public String getFlagVoucher()
        {
            return flagVoucher;
        }

        public void setFlagVoucher(String flagVoucher)
        {
            this.flagVoucher = flagVoucher;
        }

        public String getFlagSticker()
        {
            return flagSticker;
        }

        public void setFlagSticker(String flagSticker)
        {
            this.flagSticker = flagSticker;
        }

        public String[] getTextoVoucher()
        {
            return textoVoucher;
        }

        public String[] getTextoSticker()
        {
            return textoSticker;
        }

        public void setTextoVoucher(String[] textoVoucher)
        {
            this.textoVoucher = textoVoucher;
        }

        public void setTextoSticker(String[] textoSticker)
        {
            this.textoSticker = textoSticker;
        }

        //public int getNroImpresionesVoucher()
        //{
        //    return nroImpresionesVoucher;
        //}

        //public void setNroImpresionesVoucher(int nroImpresionesVoucher)
        //{
        //    this.nroImpresionesVoucher = nroImpresionesVoucher;
        //}

        #endregion


        #region SetClose(), frmPrint_FormClosing, KillPorts

        public delegate void SCHandler();

        private void SetSetClose()
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new SCHandler(SetClose), new object[] {});
            }
            else
            {
                //SetClose();
                Close();
            }
        }

        private void SetClose()
        {
            Close();
        }

        private void frmPrint_FormClosing(object sender, FormClosingEventArgs e)
        {
            KillPorts();
        }

        private void KillPorts()
        {
            if (this.getFlagSticker().Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
            }
            if (this.getFlagVoucher().Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
            }
        }
        #endregion


        #region llenarCombo
        public void llenarCombo(ComboBox cmb)
        {
            try
            {
                ArrayList portList = Rs232.GetPortIdentifiers();
                foreach (String puerto in portList)
                {
                    cmb.Items.Add(puerto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion


        #region ExitImprimioOk

        private void ExitImprimioOk()
        {
            if (getFlagSticker().Equals("1"))
            {
                int nroTicketsImpresos = GetNroTicketsImpresos();
                if (getFlagVoucher().Equals("1"))
                {
                    Resultado = Defines.Impresion_StickerConVoucher + "," + nroTicketsImpresos;
                }
                else
                {
                    Resultado = Defines.Impresion_SoloSticker + "," + nroTicketsImpresos;
                }
            }
            else if (getFlagVoucher().Equals("1"))
            {
                Resultado = Defines.Impresion_SoloVoucher;
            }
            else
            {
                Resultado = Defines.Error_NoControlado;
            }

            //c#
            SetSetClose();
        }

        #endregion


        #region ExitNotOk

        private void ExitNotOk()
        {
            if (this.getFlagSticker().Equals("1"))
            {
                int nroTicketsImpresos = GetNroTicketsImpresos();
                if (this.getFlagVoucher().Equals("1"))
                {
                    Resultado = Defines.Error_StickerConVoucher + "," + nroTicketsImpresos;
                }
                else
                {
                    Resultado = Defines.Error_SoloSticker + "," + nroTicketsImpresos;
                }
            }
            else if (this.getFlagVoucher().Equals("1"))
            {
                Resultado = Defines.Error_SoloVoucher;
            }
            else
            {
                Resultado = Defines.Error_NoControlado;
            }

            //c#
            SetSetClose();
        }

        #endregion


        #region SalirPorUsuario

        private void SalirPorUsuario()
        {
            if (this.getFlagSticker().Equals("1"))
            {
                int nroTicketsImpresos = GetNroTicketsImpresos();
                if (this.getFlagVoucher().Equals("1"))
                {
                    Resultado = Defines.Salir_StickerConVoucher + "," + nroTicketsImpresos;
                }
                else
                {
                    Resultado = Defines.Salir_SoloSticker + "," + nroTicketsImpresos;
                }
            }
            else if (this.getFlagVoucher().Equals("1"))
            {
                Resultado = Defines.Salir_SoloVoucher;
            }
            else
            {
                Resultado = Defines.Error_NoControlado;
            }

            //c#
            SetSetClose();
        }

        #endregion


        private String resultado = "";

        public string Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }




        //------------- EAG 11/02/2010
        public frmPrintNet(String[] parametros, bool valida)
        {
            InitializeComponent();
            //Estamos en el Main Thread por lo que no es necesario el Invoke.
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar2_Click);
            this.btnSalir.Click += new System.EventHandler(this.btnSalir2_Click);
            this.btnImprimir.Visible = false;

            resultado = "";

            init2(parametros);
        }

        public void init2(String[] parametros)
        {
            try
            {
                htParametros = new Hashtable();
                for (int i = 0; i < parametros.Length; i++)
                {
                    String parametro = parametros[i];
                    String param_key = parametro.Split(new char[] { '=' }, 2)[0];
                    String param_valor = parametro.Split(new char[] { '=' }, 2)[1];
                    setParameter(param_key, param_valor);
                }

                puertoSerial = new Rs232();

                enabledPrintVoucher = true;
                enabledPrintSticker = true;

                flagSticker = "0";
                flagVoucher = "0";

                GetAndSetFlags();

                GetAndSetConfiguracionPuertos();

                initComponentsMessage();

                SetShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

                new Thread(new ThreadStart(run4)).Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show("El Modulo de Impresion ha fallado. Error: " + ex.Message, "Error en la Impresion.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.StackTrace);

                //ExitNotOk();
                throw ex;
            }
        }

        public void run4()
        {
            Thread.Sleep(1000); //Que se vea el efecto de Procesando por al menos un segundo.
            if (ProcesarTesteo(true))
            {
                Resultado = "1";
                SetSetClose();
            }
        }

        private void btnActualizar2_Click(object sender, EventArgs e)
        {
            if (this.flagSticker.Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_STICKER);
                String puertoStickerSelected = null;
                if (chkPuertoSticker.Checked)
                {
                    if (cboPtosSticker.SelectedIndex > 0)
                    {
                        puertoStickerSelected = cboPtosSticker.SelectedItem.ToString();
                    }
                    if (puertoStickerSelected == null)
                    {
                        MessageBox.Show("Debe seleccionar un puerto \npara la impresora de sticker", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    puertoStickerSelected = puertoSerial.getConfigImpSticker()[0].Trim();
                }
                puertoSerial.getConfigImpSticker()[0] = puertoStickerSelected;
            }
            if (this.flagVoucher.Equals("1"))
            {
                puertoSerial.KillPort(Variables.FLAG_PRINTER_VOUCHER);
                String puertoVoucherSelected = null;
                if (chkPuertoVoucher.Checked)
                {
                    if (cboPtosVoucher.SelectedIndex > 0)
                    {
                        puertoVoucherSelected = cboPtosVoucher.SelectedItem.ToString();
                    }
                    if (puertoVoucherSelected == null)
                    {
                        MessageBox.Show("Debe seleccionar un puerto \npara la impresora de voucher", "Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    puertoVoucherSelected = puertoSerial.getConfigImpVoucher()[0].Trim();
                }
                puertoSerial.getConfigImpVoucher()[0] = puertoVoucherSelected;
            }

            ShowAndSetMessage(Variables.MENSAJE_PROCESANDO);

            Thread t = new Thread(new ThreadStart(run5));
            t.Start();
        }

        public void run5()
        {
            Thread.Sleep(1000); //Que se vea el efecto de Procesando por al menos un segundo.
            ProcesarTesteo(false);
        }

        private void GetAndSetConfiguracionPuertos()
        {
            String parametro;
            if (this.getFlagSticker().Equals("1"))
            {
                //Configuracion de la impresora de sticker
                parametro = getParameter(Variables.ConfiguracionImpresoraSticker);
                Console.WriteLine("Print.init() - ConfiguracionImpresoraSticker:" + parametro);
                if (parametro == null || parametro.Length == 0)
                {
                    throw new Exception("La impresora de sticker no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpSticker(parametro);
            }
            if (this.getFlagVoucher().Equals("1"))
            {
                //Configuracion de la impresora de voucher
                parametro = getParameter(Variables.ConfiguracionImpresoraVoucher);
                Console.WriteLine("Print.init() - ConfiguracionImpresoraVoucher:" + parametro);
                if (parametro == null || parametro.Length == 0)
                {
                    throw new Exception("La impresora de voucher no tiene una configuración válida.");
                }
                puertoSerial.setConfigImpVoucher(parametro);
            }
        }


        private bool ProcesarTesteo(bool primeraVez)
        {
            if (this.getFlagSticker().Equals("1"))
            {
                int retornoSticker = TestImpresora(Variables.FLAG_PRINTER_STICKER);

                if (this.getFlagVoucher().Equals("1"))
                {
                    int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                    if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                    {
                        enabledPrintVoucher = true;
                    }
                    else
                    {
                        enabledPrintVoucher = false;
                    }
                    //
                    if (retornoSticker == Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE)
                    {
                        enabledPrintSticker = true;
                    }
                    else
                    {
                        enabledPrintSticker = false;
                    }

                    if (!primeraVez || !enabledPrintVoucher || !enabledPrintSticker)
                    {
                        SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_STICKER, retornoSticker);
                        SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                        SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                        SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, true);
                        SetShowConfigManualPuertos();
                        return false;
                    }
                }
                else
                {
                    //No se da este caso...Reservado para un futuro.
                }
            }
            else if (this.getFlagVoucher().Equals("1"))
            {
                int retornoVoucher = TestImpresora(Variables.FLAG_PRINTER_VOUCHER);

                if (retornoVoucher == Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE)
                {
                    enabledPrintVoucher = true;
                }
                else
                {
                    enabledPrintVoucher = false;
                }

                if (!primeraVez || !enabledPrintVoucher)
                {
                    SetUpdatePanelConfigManualPuertos(Variables.FLAG_PRINTER_VOUCHER, retornoVoucher);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_VOUCHER, true);
                    SetSetVisiblePanel(Variables.FLAG_PRINTER_STICKER, false);
                    SetShowConfigManualPuertos();
                    return false;
                }
            }
            return true;
        }

        private void btnSalir2_Click(object sender, EventArgs e)
        {
            if (enabledPrintVoucher && enabledPrintSticker)
            {
                Resultado = "1";
            }
            else
            {
                /*
                if (MessageBox.Show("¿Desea salir del modulo de impresion?", "Confirmar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                }
                */

                Resultado = "0";
            }
            SetSetClose();
        }

        //------------- EAG 11/02/2010
    }
}