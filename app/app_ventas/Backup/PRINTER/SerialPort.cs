using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

namespace LAP.TUUA.PRINTER
{
    public class PuertoSerial
    {
        private SerialPort comPort;
        private bool lector;
        private string dataInput;
        private System.Timers.Timer tiempoLector;

        private int tipo_ = 0;

        public PuertoSerial()
        {

        }

        public void Configurar(string configuracion)
        {

            string[] datos = configuracion.Split(',');
            comPort = new SerialPort(datos[0], Convert.ToInt32(datos[1]), Parity.None, Convert.ToInt32(datos[3]), StopBits.One);

            // When data is recieved through the port, call this method
            comPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);

            tiempoLector = new System.Timers.Timer();
            tiempoLector.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
            tiempoLector.Interval = 10000;

        }

        public int VerEstadoImpresora(int tipo, string data)
        {
            tipo_ = tipo;
            int estado = 0;

            // si el puerto esta abierto hay que cerrarlo
            if (comPort.IsOpen)
            {
                comPort.Close();
            }
            // abrir puerto
            comPort.Open();

            // setear variables de control
            lector = false;
            tiempoLector.Start();

            // escribir en puerto y esperar hasta obtener respuesta
            comPort.Write(data);
            while (!lector) { }

            // validar la respuesta segun el tipo de impresora
            switch (tipo)
            {
                case 0: // voucher

                    // impresora inoperativa
                    if (dataInput == null || dataInput.Length == 0)
                    {
                        estado = 0;
                    }
                    else
                    {
                        // impresora operativa
                        byte byteInput = Asc(char.Parse(dataInput));
                        if (byteInput == 18 || byteInput == 30)
                        {
                            estado = 1;
                        }
                        // no hay papel
                        else if (byteInput == 114 || byteInput == 126)
                        {
                            estado = 2;
                        }

                    }

                    break;
                case 1: // sticker
                    // impresora inoperativa
                    if (dataInput == null || dataInput.Length == 0)
                    {
                        estado = 0;
                    }
                    else
                    {
                        // impresora operativa
                        try
                        {
                            if (dataInput.Substring(5, 1).Equals("1"))
                            {
                                estado = 2;
                            }
                            else if (dataInput.Substring(5, 1).Equals("0") &&
                                dataInput.Substring(43, 1).Equals("0") &&
                                dataInput.Substring(7, 1).Equals("0") &&
                                dataInput.Substring(45, 1).Equals("1"))
                            {
                                estado = 1;
                            }
                        }
                        catch(Exception ex)
                        {
                            Console.Out.WriteLine("Excepcion al analizar la data de sticker:" + ex.Message);
                        }

                    }
                    break;

            }

            // cerrar puerto
            comPort.Close();


            return estado;
        }

        public void DisplayTimeEvent(object source, ElapsedEventArgs e)
        {
            dataInput = "";
            lector = true;
            tiempoLector.Enabled = false;

        }

        private const int STX = 2;
        private const int LF = 10;

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // This method will be called when there is data waiting in the port's buffer

            // Read all the data waiting in the buffer          
            comPort.ReadTimeout = 20000;

            if(tipo_==0)
            {
                dataInput = comPort.ReadExisting();
            }
            else if(tipo_==1)
            {
                byte[] bArray = new byte[100];
                int c = -1;
                int i = 0;
                int numLF = 0;
                while (numLF < 3)
                {
                    c = comPort.ReadByte();
                    bArray.SetValue((byte)c, i);
                    if (c == LF)
                        numLF++;
                    i++;
                }

                /*
                dataInput = comPort.ReadExisting();
                */
                StringBuilder sb = new StringBuilder();
                foreach (byte b in bArray)
                {
                    sb.Append(Convert.ToString(b));
                }

                dataInput = System.Text.Encoding.Default.GetString(bArray);
                
            }
            Console.Out.WriteLine("dataInput: " + dataInput);                
            lector = true;
            tiempoLector.Enabled = false;

        }

        public static byte Asc(char src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src +
            "")[0]);
        }

        public int EnviarData(int tipo, string[] data)
        {
            // si el puerto esta abierto hay que cerrarlo
            if (comPort.IsOpen)
            {
                comPort.Close();
            }
            // abrir puerto
            comPort.Open();

            // si el tipo de impresora es voucher
            if (tipo == 0)
            {
                // imprime cada linea 
                for (int i = 0; i < data.Length; i++)
                {
                    //comPort.WriteLine(data[i]);
                    comPort.Write(data[i] + "\n");

                }

            }
            else 
            {
                // imprime cad linea con un delay de 2 seg
                for (int i = 0; i < data.Length; i++)
                {
                    comPort.WriteLine(data[i]);
                    System.Threading.Thread.Sleep(2000);

                }
            
            }

            // si el tipo de impresora es voucher cortar papel
            switch (tipo)
            {
                case 0:
                    cortarPapel();
                    break;
            }

            // cerrar puerto
            comPort.Close();

            return 1;

        }

        private void cortarPapel()
        {
            comPort.Write(char.ConvertFromUtf32(27) + "J" + char.ConvertFromUtf32(255));
            comPort.Write(char.ConvertFromUtf32(27) + "i");


        }

    }
}
