using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LAP.TUUA.ACCESOS
{
      public class ACS_SCom
      {
            public  System.IO.Ports.SerialPort serial;
            public string strTrama="";
            public ACS_SCom(string strPortName,int  intBaudRate,  int intParity, int intBitsData ) 
            {
                  serial = new System.IO.Ports.SerialPort();
                  serial.PortName = strPortName;
                  serial.BaudRate = intBaudRate;
                  serial.DtrEnable = true;
                  serial.ReadTimeout = -1;
                  serial.DataBits = intBitsData;
                 // serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(OnComm);
                  switch (intParity)
                  {
                        case 1:
                              serial.Parity = System.IO.Ports.Parity.Odd;
                              break;
                        case 2:
                              serial.Parity = System.IO.Ports.Parity.Even;
                              break;
                        case 3:
                              serial.Parity = System.IO.Ports.Parity.Mark;
                              break;
                        case 4:
                              serial.Parity = System.IO.Ports.Parity.Space;
                              break;
                        default:
                              serial.Parity = System.IO.Ports.Parity.None;
                              break;
                  }
            }

            public ACS_SCom(string strPortName, int intBaudRate, int intParity, int intBitsData, int intStopBits)
            {
                  serial = new System.IO.Ports.SerialPort();
                  serial.PortName = strPortName;
                  serial.BaudRate = intBaudRate;
                  serial.DtrEnable = true;
                  serial.ReadTimeout = -1;
                  serial.DataBits = intBitsData;
                  // serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(OnComm);
                  switch (intParity)
                  {
                        case 1:
                              serial.Parity = System.IO.Ports.Parity.Odd;
                              break;
                        case 2:
                              serial.Parity = System.IO.Ports.Parity.Even;
                              break;
                        case 3:
                              serial.Parity = System.IO.Ports.Parity.Mark;
                              break;
                        case 4:
                              serial.Parity = System.IO.Ports.Parity.Space;
                              break;
                        default:
                              serial.Parity = System.IO.Ports.Parity.None;
                              break;
                  }

                  switch (intStopBits)
                  {
                        case 1:
                              serial.StopBits = System.IO.Ports.StopBits.One;
                              break;
                        case 2:
                              serial.StopBits = System.IO.Ports.StopBits.Two;
                              break;
                        case 3:
                              serial.StopBits = System.IO.Ports.StopBits.OnePointFive;
                              break;
                        default:
                              serial.StopBits = System.IO.Ports.StopBits.None;
                              break;
                  }

            }


            public void  Open()
            {
                  serial.Open();
            }
            public bool Conectar()
            {
                  serial.Open();
                  return serial.IsOpen;
            }

            public void Desconectar()
            {
                  serial.Close();
            }
            public void Reset() 
            {
                  Byte[] st = { 193 };
                  serial.Write(st, 0, 1);
               
            }

            public bool Conectado() 
            {
                  return serial.IsOpen;
            }

            public  string  Leer()
            {
                if (!ACS_Property.bModeLecturaNueva)
                {
                    string strAux = "";
                    string tmp;

                    Thread.Sleep(1);
                    strAux = serial.ReadExisting();

                    //Thread.Sleep(200);

                    // strAux = serial.ReadLine();
                    // ACS_Util.Escribir(DateTime.Now.ToLongDateString(),"Ingreso");
                    if (strAux.Length > 0)
                    {


                        do
                        {
                            Thread.Sleep(1);
                            tmp = serial.ReadExisting();
                            if (tmp.Length > 0) strAux += tmp;
                        }
                        while (tmp.Length > 0);

                        //      //Byte[] st = { 193 };
                        //      //serial.Write(st, 0, 1);
                        //      //serial.Close();
                        //      //serial.Dispose();
                        //Thread.Sleep(400);
                        //      //serial.Open();
                    }

                    return strAux;
                }
                else
                {
                    string strAux = "";

                    Thread.Sleep(100);
                    strAux += serial.ReadExisting();

                    int Inicio = strAux.IndexOf("\x02"); // Posicion de Inicio de trama
                    int Final = strAux.IndexOf("\x03");  // Posicion de Final de trama

                    if ((Inicio > -1) & (Final > Inicio))
                    {
                        serial.DiscardInBuffer();
                        return strAux.Substring(Inicio, Final);
                    }
                    else
                    // De no cumplirse las condiciones de Inicio/Fin
                    {
                        if (strAux != "")
                        {
                            Thread.Sleep(150); //Agrega un tiempo de espera por si la trama es larga
                            strAux += serial.ReadExisting();
                            serial.DiscardInBuffer();
                            Inicio = strAux.IndexOf("\x02"); // Posicion de Inicio de trama
                            Final = strAux.IndexOf("\x03");  // Posicion de Final de trama

                            if ((Inicio > -1) & (Final > Inicio))
                                return strAux.Substring(Inicio, Final);
                            //else
                            //  serial.DiscardInBuffer();

                        }
                        //return strAux;
                        return "";
                    }
                }
            }


            //private void OnComm(object sender, EventArgs e)
            //{
            //      Thread.Sleep(1);
            //      strTrama = serial.ReadExisting();
                  

            //}
      }
}
