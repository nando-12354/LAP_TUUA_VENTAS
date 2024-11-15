using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Timer=System.Timers.Timer;

namespace LAP.TUUA.PRINTER
{
    public class Rs232
    {
        // configuracion impresora de voucher [ejemplo: COM1,9600,N,8,1]
        private String[] configImpVoucher;
        // configuracion impresora de sticker [ejemplo: COM3,9600,N,8,1]
        private String[] configImpSticker;

        private SerialPort serialPortVoucher;
        private Timer timerVoucher;
        private bool bTimeOutVoucher;
        private bool isReceivedDataVoucher;

        private SerialPort serialPortSticker;
        private Timer timerSticker;
        private bool bTimeOutSticker;
        private bool isReceivedDataSticker;

        /** Variables comunes */
        private byte[] lectorBuffer;

        private const int ETX = 3;
        private const int STX = 2;
        private const int LF = 10;
        private const int CR = 13;

        //---- EAG 12/02/2010 
        private int Max_LF;
        //Valores por Defecto de la impresora modelo TLP2844-Z
        private int timeForPrintSticker = 2000;
        private int RibbonOutByteValue = 48;
        private int RibbonOkByteValue = 49;
        //---- EAG 12/02/2010

        private const int TAM_PAQ_SERIAL = 1024;

        private static int timeout_lectura_voucher = 5000;
        private static int timeout_lectura_sticker = 5000;


        public Rs232()
        {
            configImpVoucher = null;
            configImpSticker = null;
        }

        #region StartPort

        public bool StartPort(int tipo)
        {
            String puerto = "";

            if (tipo == Variables.FLAG_PRINTER_VOUCHER)
            {
                try
                {
                    puerto = getConfigImpVoucher()[0];
                    serialPortVoucher = new SerialPort(puerto, Int32.Parse(getConfigImpVoucher()[1]), Parity.None, Int32.Parse(getConfigImpVoucher()[3]), StopBits.One);
                    if (serialPortVoucher.IsOpen) serialPortVoucher.Close();
                    serialPortVoucher.ReadTimeout = 1000;
                    // When data is recieved through the port, call this method
                    serialPortVoucher.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                    serialPortVoucher.Open();

                    timerVoucher = new System.Timers.Timer();
                    timerVoucher.AutoReset = false;
                    timerVoucher.Elapsed += new ElapsedEventHandler(ejecutarTimerVoucher);
                    timerVoucher.Interval = timeout_lectura_voucher;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            else if (tipo == Variables.FLAG_PRINTER_STICKER)
            {
                try
                {
                    puerto = getConfigImpSticker()[0];
                    serialPortSticker = new SerialPort(puerto, Int32.Parse(getConfigImpSticker()[1]), Parity.None, Int32.Parse(getConfigImpSticker()[3]), StopBits.One);
                    if (serialPortSticker.IsOpen) serialPortSticker.Close();
                    serialPortSticker.ReadTimeout = 1000;

                    //---- EAG 12/02/2010
                    serialPortSticker.DtrEnable = true;
                    //---- EAG 12/02/2010

                    // When data is recieved through the port, call this method
                    serialPortSticker.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                    serialPortSticker.Open();

                    timerSticker = new System.Timers.Timer();
                    timerSticker.AutoReset = false;
                    timerSticker.Elapsed += new ElapsedEventHandler(ejecutarTimerSticker);
                    timerSticker.Interval = timeout_lectura_sticker;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion

        #region KillPort

        public bool KillPort(int tipo)
        {
            try
            {
                if (tipo == Variables.FLAG_PRINTER_VOUCHER)
                {
                    if (serialPortVoucher != null)
                    {
                        serialPortVoucher.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                        serialPortVoucher.Close();
                    }
                }
                else if (tipo == Variables.FLAG_PRINTER_STICKER)
                {
                    if (serialPortSticker != null)
                    {
                        serialPortSticker.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
                        serialPortSticker.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Rs232.KillPort(" + tipo +") - Error: " + ex.Message);
                try{
                    if (tipo == Variables.FLAG_PRINTER_VOUCHER){
                        if (serialPortVoucher != null){
                            serialPortVoucher.Close();
                        }
                    }
                    else if (tipo == Variables.FLAG_PRINTER_STICKER){
                        if (serialPortSticker != null){
                            serialPortSticker.Close();
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rs232.KillPort(" + tipo + ") - Error al reintentar: " + ex2.Message);
                }
                return false;
            }
            return true;
        }

        #endregion

        #region port_DataReceived
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPortSticker != null && serialPortSticker.IsOpen && serialPortSticker.BytesToRead > 0)//EAG 15/01/2010 Added: && serialPortSticker.IsOpen
                {
                    //serialPortSticker.ReadTimeout = 1000;

                    int c = -1;
                    int i = 0;
                    byte[] bArray = new byte[TAM_PAQ_SERIAL];
                    while (!bTimeOutSticker)
                    {
                        c = serialPortSticker.ReadByte();
                        if (c == -1 || (c != STX))
                        {
                            continue;
                        }
                        bArray.SetValue((byte)c, i);
                        i++;
                        break;
                    }
                    if (c == STX /*&& !bTimeOutSticker*/)
                    {
                        int numLF = 0;
                        while (!bTimeOutSticker && numLF < Max_LF)
                        {
                            c = serialPortSticker.ReadByte();
                            if (c == -1)
                            {
                                continue;
                            }
                            else if (c == LF)
                            {
                                numLF++;
                            }
                            bArray.SetValue((byte)c, i);
                            i++;
                        }
                        if (!bTimeOutSticker)
                        {
                            timerSticker.Enabled = false;
                            lectorBuffer = new byte[i];
                            System.Array.Copy(bArray, 0, lectorBuffer, 0, i);
                            isReceivedDataSticker = true;
                        }
                    }
                }

                if (serialPortVoucher != null && serialPortVoucher.IsOpen && serialPortVoucher.BytesToRead > 0)//EAG 15/01/2010 Added: && serialPortVoucher.IsOpen
                {
                    lectorBuffer = new byte[1];
                    serialPortVoucher.Read(lectorBuffer, 0, 1);
                    timerVoucher.Enabled = false;
                    isReceivedDataVoucher = true;
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Excepcion en port_DataReceived: " + ex.Message);
            }
        }
        #endregion

        #region escribirTexto

        private int cantidadStickersImpresos = 0;

        public int getCantidadStickersImpresos()
        {
            return this.cantidadStickersImpresos;
        }

        public void setCantidadStickersImpresos(int cantidadStickersImpresos)
        {
            this.cantidadStickersImpresos = cantidadStickersImpresos;
        }

        public void escribirTexto(String[] textoImp, int tipo) {
            try {
                if (tipo == Variables.FLAG_PRINTER_STICKER) {
                    int i = 0;
                    //Recordar que el primer elemento a escribir al puerto serial de la impresora de stickers es la cabecera,
                    //y no ninguno de los stickers en si, que seria el detalle (Nro de lineas a escribir al puerto serial = 1 + Nro. Stickers a imprimir).
                    for (; i < textoImp.Length; i++) {
                        Console.WriteLine("TextoSticker " + i + ":" + textoImp[i]);

                        /*
                        //---EAG 09/02/2010
                        //Impresora de Sticker a primer instancia no soporta caracteres extraños
                        textoImp[i] = textoImp[i].Replace("á", "a");
                        textoImp[i] = textoImp[i].Replace("é", "e");
                        textoImp[i] = textoImp[i].Replace("í", "i");
                        textoImp[i] = textoImp[i].Replace("ó", "o");
                        textoImp[i] = textoImp[i].Replace("ú", "u");

                        textoImp[i] = textoImp[i].Replace("Á", "A");
                        textoImp[i] = textoImp[i].Replace("É", "E");
                        textoImp[i] = textoImp[i].Replace("Í", "I");
                        textoImp[i] = textoImp[i].Replace("Ó", "O");
                        textoImp[i] = textoImp[i].Replace("Ú", "U");

                        textoImp[i] = textoImp[i].Replace("ü", "u");
                        textoImp[i] = textoImp[i].Replace("Ü", "U");

                        textoImp[i] = textoImp[i].Replace("ñ", "n");
                        textoImp[i] = textoImp[i].Replace("Ñ", "N");

                        textoImp[i] = textoImp[i].Replace("€", "E");
                        textoImp[i] = textoImp[i].Replace("¥", "Y");
                        textoImp[i] = textoImp[i].Replace("£", "L");
                        //---EAG 09/02/2010
                        */

                        //serialPortSticker.Write(textoImp[i]);


                        //---EAG 09/02/2010 Mejorado
                        byte[] dataEnvioTexto = Encoding.Default.GetBytes(textoImp[i]);

                        String hexEnvioTexto = ByteArrayToHexString(dataEnvioTexto);

                        hexEnvioTexto = hexEnvioTexto.Replace("FC ", "81 ");// ü
                        hexEnvioTexto = hexEnvioTexto.Replace("DC ", "9A ");// Ü
                        hexEnvioTexto = hexEnvioTexto.Replace("A3 ", "9C ");// £
                        hexEnvioTexto = hexEnvioTexto.Replace("A2 ", "BD ");// ¢
                        hexEnvioTexto = hexEnvioTexto.Replace("A5 ", "BE ");// ¥
                        //--
                        hexEnvioTexto = hexEnvioTexto.Replace("A1 ", "AD ");// ¡
                        hexEnvioTexto = hexEnvioTexto.Replace("BF ", "A8 ");// ¿
                        //--
                        hexEnvioTexto = hexEnvioTexto.Replace("E1 ", "A0 ");// á
                        hexEnvioTexto = hexEnvioTexto.Replace("E9 ", "82 ");// é
                        hexEnvioTexto = hexEnvioTexto.Replace("ED ", "A1 ");// í
                        hexEnvioTexto = hexEnvioTexto.Replace("F3 ", "A2 ");// ó
                        hexEnvioTexto = hexEnvioTexto.Replace("FA ", "A3 ");// ú
                        hexEnvioTexto = hexEnvioTexto.Replace("C1 ", "B5 ");// Á
                        hexEnvioTexto = hexEnvioTexto.Replace("C9 ", "90 ");// É
                        hexEnvioTexto = hexEnvioTexto.Replace("CD ", "D6 ");// Í
                        hexEnvioTexto = hexEnvioTexto.Replace("D3 ", "E0 ");// Ó
                        hexEnvioTexto = hexEnvioTexto.Replace("DA ", "E9 ");// Ú
                        hexEnvioTexto = hexEnvioTexto.Replace("F1 ", "A4 ");// ñ
                        hexEnvioTexto = hexEnvioTexto.Replace("D1 ", "A5 ");// Ñ

                        //hexEnvioTexto = hexEnvioTexto.Replace("80 ", "D5 ");  //En Java es "3F", "D5"  ---> No imprime el €, imprime un palito.
                        hexEnvioTexto = hexEnvioTexto.Replace("80 ", "45 ");    //En Java es "3F", "45"  ---> Imprime E

                        //hexEnvioTexto = "1B 74 13 " + hexEnvioTexto;

                        byte[] dataAEnviar = HexStringToByteArray(hexEnvioTexto);
                        
                        serialPortSticker.Write(dataAEnviar, 0, dataAEnviar.Length);
                        //---EAG 09/02/2010 Mejorado
                        
                        Thread.Sleep(timeForPrintSticker);//Esto para que no envie los comandos a la impresora, sin siquiera que haya terminado de imprimir el primer ticket.
                        //Es un retardo para poder hacer un test objetivo.

                        int reintentos = 0;
                        int resultadoTesteo;
                        bool estadoImpresoraSticker = false;
                        while (true) {
                            if ((resultadoTesteo = verificarEstadoImpresoraRT(Variables.FLAG_PRINTER_STICKER)) != Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE) {
                                reintentos++;
                                if (reintentos >= 2) {
                                    Console.WriteLine("RS232.escribirTexto() - Se testeo incorrectamente la Impresora de Sticker en el reintento " + (reintentos - 1) + " - Retorno: " + resultadoTesteo + ".");
                                    break;
                                }
                                Console.WriteLine("RS232.escribirTexto() - Se testeo incorrectamente la Impresora de Sticker - Retorno: " + resultadoTesteo + ".");
                                Thread.Sleep(250);
                            } else {
                                Console.WriteLine("RS232.escribirTexto() - Se testeo correctamente la Impresora de Sticker.");
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
                    if(i >= textoImp.Length){
                        i = i-1;
                    }
                    //setear la cantidad de stickers impresos.
                    setCantidadStickersImpresos(i);
                    Console.WriteLine("Tickets Impresos: " + i);
                } else if (tipo == Variables.FLAG_PRINTER_VOUCHER) {
                    String texto;
                    for (int i = 0; i < textoImp.Length; i++) {
                        Console.WriteLine("TextoVoucher " + i + ":" + textoImp[i]);
                        if (textoImp[i].Length==4 && textoImp[i].Substring(0, 4).Equals("{CP}"))
                            cortarPapel();
                        else
                        {
                            texto = textoImp[i] + "\n";

                            //serialPortVoucher.Write(texto);

                            /*
                            //---PARA QUE SOPORTE EL INGRESO DEL SIMBOLO DE EURO
                            if (texto.Contains("€"))
                            {
                                byte[] data = HexStringToByteArray("1B 74 13");
                                serialPortVoucher.Write(data, 0, data.Length);
                                int indexEuro = -1;

                                while ((indexEuro = texto.IndexOf("€")) >= 0)
                                {
                                    serialPortVoucher.Write(texto.Substring(0, indexEuro));
                                    data = HexStringToByteArray("D5");
                                    serialPortVoucher.Write(data, 0, data.Length);
                                    texto = texto.Substring(indexEuro + 1);
                                }
                                serialPortVoucher.Write(texto);
                            }
                            else
                            {
                                serialPortVoucher.Write(texto);
                            }
                            //---
                            */

                            byte[] dataEnvioTexto = Encoding.Default.GetBytes(texto);

                            String hexEnvioTexto = ByteArrayToHexString(dataEnvioTexto);

                            hexEnvioTexto = hexEnvioTexto.Replace("FC ", "81 ");// ü
                            hexEnvioTexto = hexEnvioTexto.Replace("DC ", "9A ");// Ü
                            hexEnvioTexto = hexEnvioTexto.Replace("A3 ", "9C ");// £
                            hexEnvioTexto = hexEnvioTexto.Replace("A2 ", "BD ");// ¢
                            hexEnvioTexto = hexEnvioTexto.Replace("A5 ", "BE ");// ¥
                            //--
                            hexEnvioTexto = hexEnvioTexto.Replace("A1 ", "AD ");// ¡
                            hexEnvioTexto = hexEnvioTexto.Replace("BF ", "A8 ");// ¿
                            //--
                            hexEnvioTexto = hexEnvioTexto.Replace("E1 ", "A0 ");// á
                            hexEnvioTexto = hexEnvioTexto.Replace("E9 ", "82 ");// é
                            hexEnvioTexto = hexEnvioTexto.Replace("ED ", "A1 ");// í
                            hexEnvioTexto = hexEnvioTexto.Replace("F3 ", "A2 ");// ó
                            hexEnvioTexto = hexEnvioTexto.Replace("FA ", "A3 ");// ú
                            hexEnvioTexto = hexEnvioTexto.Replace("C1 ", "B5 ");// Á
                            hexEnvioTexto = hexEnvioTexto.Replace("C9 ", "90 ");// É
                            hexEnvioTexto = hexEnvioTexto.Replace("CD ", "D6 ");// Í
                            hexEnvioTexto = hexEnvioTexto.Replace("D3 ", "E0 ");// Ó
                            hexEnvioTexto = hexEnvioTexto.Replace("DA ", "E9 ");// Ú
                            hexEnvioTexto = hexEnvioTexto.Replace("F1 ", "A4 ");// ñ
                            hexEnvioTexto = hexEnvioTexto.Replace("D1 ", "A5 ");// Ñ
                            hexEnvioTexto = hexEnvioTexto.Replace("80 ", "D5 ");// €

                            hexEnvioTexto = "1B 74 13 " + hexEnvioTexto;

                            byte[] dataAEnviar = HexStringToByteArray(hexEnvioTexto);
                            serialPortVoucher.Write(dataAEnviar, 0, dataAEnviar.Length);

                        }
                        
                    }
                    //cortarPapel();
                }
            } catch (Exception ex) {
                Console.WriteLine("Rs232.escribirTexto(" + tipo + ") - Error: " + ex.Message);
            }
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
        #endregion

        #region cortarPapel
        /**
         * Metodo que corta el papel (Usado en la impresora de voucher).
         */
        private void cortarPapel()
        {
            try
            {
                serialPortVoucher.Write(Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER1, 0, Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER1.Length);
                serialPortVoucher.Write(Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER2, 0, Variables.COMMAND_PRINTER_VOUCHER_CUT_PAPER2.Length);

            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region verificarEstadoImpresora
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
        public int verificarEstadoImpresora(int tipo)
        {
            int respuesta = Variables.FLAG_PRINTER_ERROR;

            try
            {
                if (tipo == Variables.FLAG_PRINTER_VOUCHER)
                {
                    lectorBuffer = null;
                    bTimeOutVoucher = false;
                    isReceivedDataVoucher = false;
                    serialPortVoucher.Write(Variables.COMMAND_PRINTER_VOUCHER, 0,
                                            Variables.COMMAND_PRINTER_VOUCHER.Length);// enviar comando de verificacion de estado

                    // activar timer voucher
                    timerVoucher.Enabled = true;

                    // sale cuando recibe respuesta de impresora de voucher, o cuando se cumple el timeout
                    while (!bTimeOutVoucher && !isReceivedDataVoucher)
                    {
                        Thread.Sleep(10);
                    }

                    if (isReceivedDataVoucher)
                    {
                        respuesta = interpretarRespuesta(tipo);
                    }
                    else
                    {
                        //Salio por Timeout.
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                    }

                }
                else if (tipo == Variables.FLAG_PRINTER_STICKER)
                {
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
                    serialPortSticker.Write(Variables.COMMAND_PRINTER_STICKER, 0,
                                            Variables.COMMAND_PRINTER_STICKER.Length);// enviar comando de verificacion de estado

                    // activar timer sticker
                    timerSticker.Enabled = true;

                    // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
                    while (!bTimeOutSticker && !isReceivedDataSticker)
                    {
                        Thread.Sleep(10);
                    }
                    if (isReceivedDataSticker)
                    {
                        respuesta = interpretarRespuesta(tipo);
                    }
                    else
                    {
                        //Salio por Timeout.
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Rs232.verificarEstadoImpresora(" + tipo + ") - Error: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return respuesta;
        }

        #endregion

        #region timers

        private void ejecutarTimerSticker(object source, ElapsedEventArgs e)
        {
            timerSticker.Enabled = false;
            bTimeOutSticker = true;
        }

        private void ejecutarTimerVoucher(object source, ElapsedEventArgs e)
        {
            timerVoucher.Enabled = false;
            bTimeOutVoucher = true;
        }

        #endregion

        #region interpretarRespuesta

        private int interpretarRespuesta(int tipo)
        {
            int respuesta = Variables.FLAG_PRINTER_ERROR;

            if (tipo == Variables.FLAG_PRINTER_STICKER)
            {

                // Cuando la impresora esta apagada no devuelve data [impresora no operativa]
                if (lectorBuffer == null || lectorBuffer.Length == 0)
                {
                    respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;

                }
                else
                {
                    Console.WriteLine("lectorBuffer1:" + getStringFromLectorBuffer(lectorBuffer));
                    Console.WriteLine("lectorBuffer2:" + getBytesFromLectorBuffer(lectorBuffer));

                    // [impresora sin papel]
                    if ((byte) lectorBuffer[5] == (byte) 49)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_PAPER_OUT;
                    }// [impresora en Head Up]//Cuando esta rojo parpadeando (la cabecera esta levantada)
                    else if (lectorBuffer[43] == (byte) 49)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_HEADUP;
                    }// [impresora en Pause mode]//Cuando esta verde parpadeando.
                    else if (lectorBuffer[7] == (byte) 49)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_PAUSE_MODE;
                    }// [impresora sin Ribbon]
                    else if (lectorBuffer[45] == (byte)RibbonOutByteValue)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_RIBON_OUT;
                    }
                    else if ((byte) lectorBuffer[5] == (byte) 48 && lectorBuffer[43] == (byte) 48 &&
                             lectorBuffer[7] == (byte) 48 && lectorBuffer[45] == (byte)RibbonOkByteValue)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_OPERATIVE;
                    }
                    else
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                    }
                }
            }
            else if (tipo == Variables.FLAG_PRINTER_VOUCHER)
            {

                // Cuando la impresora esta apagada no devuelve data [impresora no operativa]
                if (lectorBuffer == null || lectorBuffer.Length == 0)
                {
                    respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;

                }
                else
                {
                    Console.WriteLine("lectorBuffer:" + getBytesDistinguishedFromLectorBuffer(lectorBuffer));
                    // NOTA:
                    // Para testear es necesario que cierre la puerta de la impresora para que se actualize su status.
                    //
                    // [impresora operativa]
                    if ((byte) lectorBuffer[0] == 18)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_OPERATIVE;
                    }// [se esta acabando papel]
                    else if (lectorBuffer[0] == 30)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_NEAR_END;
                    }// [fin de papel]
                    else if (lectorBuffer[0] == 114 || lectorBuffer[0] == 126)
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_PAPER_END;
                    }// [impresora inoperativa]
                    else
                    {
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                    }
                }
            }
            return respuesta;
        }

        #endregion

        #region Util Buffer
        private String getBytesFromLectorBuffer(byte[] buffer)
        {
            String ret = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                ret += buffer[i];
            }
            return ret;
        }

        private String getBytesDistinguishedFromLectorBuffer(byte[] buffer)
        {
            String ret = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                ret += "[" + buffer[i] + "]";
            }
            return ret;
        }

        private String getStringFromLectorBuffer(byte[] buffer)
        {
            return System.Text.Encoding.Default.GetString(buffer);
            //return new String(buffer);
        }
        #endregion

        #region GetPortIdentifiers

        public static ArrayList GetPortIdentifiers()
        {
            ArrayList portList = new ArrayList();
            foreach (string s in SerialPort.GetPortNames())
                portList.Add(s);
            return portList;
        }

        #endregion

        #region Propiedades
        public String[] getConfigImpVoucher()
        {
            return configImpVoucher;
        }

        public String[] getConfigImpSticker()
        {
            return configImpSticker;
        }

        public void setConfigImpVoucher(String configImpVoucher)
        {
            this.configImpVoucher = configImpVoucher.Split(',');
        }

        public void setConfigImpSticker(String configImpSticker)
        {
            this.configImpSticker = configImpSticker.Split(',');
        }
        #endregion


        private int GetModeloImpresora()
        {
            try
            {
                lectorBuffer = null;
                bTimeOutSticker = false;
                isReceivedDataSticker = false;
                Max_LF = 1;
                serialPortSticker.Write(Variables.COMMAND_MODEL_PRINTER_STICKER, 0, 
                                        Variables.COMMAND_MODEL_PRINTER_STICKER.Length);// enviar comando de ver modelo de impresora

                // activar timer sticker
                timerSticker.Enabled = true;

                // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
                while (!bTimeOutSticker && !isReceivedDataSticker)
                {
                    Thread.Sleep(10);
                }
                if (isReceivedDataSticker)
                {
                    String str_buffer = getStringFromLectorBuffer(lectorBuffer);
                    int pos_1stComma = str_buffer.IndexOf(",");
                    if (pos_1stComma < 1)
                        return 0;

                    str_buffer = str_buffer.Substring(1, pos_1stComma - 1);

                    if (str_buffer.Contains(Variables.Modelo_TLP2844Z))
                    {
                        timeForPrintSticker = 2000;
                        RibbonOutByteValue = 48;
                        RibbonOkByteValue = 49;
                        return 1;
                    }
                    else if (str_buffer.Contains(Variables.Modelo_GX420t))
                    {
                        timeForPrintSticker = 700;
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
            catch(Exception ex)
            {
                Console.WriteLine("Rs232.GetModeloImpresora - Error: " + ex.Message);
                Console.WriteLine(ex.StackTrace);                
            }
            return 0;
        }

        #region verificarEstadoImpresoraRT
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
        public int verificarEstadoImpresoraRT(int tipo)
        {
            int respuesta = Variables.FLAG_PRINTER_ERROR;

            try
            {
                if (tipo == Variables.FLAG_PRINTER_VOUCHER)
                {
                    lectorBuffer = null;
                    bTimeOutVoucher = false;
                    isReceivedDataVoucher = false;
                    serialPortVoucher.Write(Variables.COMMAND_PRINTER_VOUCHER, 0,
                                            Variables.COMMAND_PRINTER_VOUCHER.Length);// enviar comando de verificacion de estado

                    // activar timer voucher
                    timerVoucher.Enabled = true;

                    // sale cuando recibe respuesta de impresora de voucher, o cuando se cumple el timeout
                    while (!bTimeOutVoucher && !isReceivedDataVoucher)
                    {
                        Thread.Sleep(10);
                    }

                    if (isReceivedDataVoucher)
                    {
                        respuesta = interpretarRespuesta(tipo);
                    }
                    else
                    {
                        //Salio por Timeout.
                        respuesta = Variables.FLAG_PRINTER_STATUS_VOUCHER_NOT_OPERATIVE;
                    }

                }
                else if (tipo == Variables.FLAG_PRINTER_STICKER)
                {
                    lectorBuffer = null;
                    bTimeOutSticker = false;
                    isReceivedDataSticker = false;
                    Max_LF = 3;
                    serialPortSticker.Write(Variables.COMMAND_PRINTER_STICKER, 0,
                                            Variables.COMMAND_PRINTER_STICKER.Length);// enviar comando de verificacion de estado

                    // activar timer sticker
                    timerSticker.Enabled = true;

                    // sale cuando recibe respuesta de impresora de sticker, o cuando se cumple el timeout
                    while (!bTimeOutSticker && !isReceivedDataSticker)
                    {
                        Thread.Sleep(10);
                    }
                    if (isReceivedDataSticker)
                    {
                        respuesta = interpretarRespuesta(tipo);
                    }
                    else
                    {
                        //Salio por Timeout.
                        respuesta = Variables.FLAG_PRINTER_STATUS_STICKER_NOT_OPERATIVE;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Rs232.verificarEstadoImpresoraRT(" + tipo + ") - Error: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return respuesta;
        }

        #endregion



    }
}
