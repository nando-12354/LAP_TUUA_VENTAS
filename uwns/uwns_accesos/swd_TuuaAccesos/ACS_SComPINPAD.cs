using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Collections;
using System.IO.Ports;
using System.Threading;

namespace LAP.TUUA.ACCESOS
{
      class ACS_SComPINPAD
      {
            private System.IO.Ports.SerialPort serial;
            private Hashtable hsCampos;
            private int tOutMax;
            private int tOutMin;
            private static bool estado;
            //private System.Management.ManagementObjectSearcher searcher;

            //private  void HandleStateChanged(object sender, System.Timers.ElapsedEventArgs e)
            //{
            //      bool flag=false;
            //      searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
            //      foreach (System.Management.ManagementObject queryObj in searcher.Get())
            //      {
            //            if (serial.PortName.Equals(queryObj["PortName"]))
            //            {
            //                  flag = true;
            //                  break;
            //            }
            //      }
            //      estado = flag;

            //      Console.WriteLine(estado);
            //}

            public int TOutMax
            {
                  get { return tOutMax; }
                  set { tOutMax = value; }
            }

            public int TOutMin
            {
                  get { return tOutMin; }
                  set { tOutMin = value; }
            }

            public System.IO.Ports.SerialPort Serial
            {
                  get { return serial; }
                  set { serial = value; }
            }

            public ACS_SComPINPAD(string strPortName, int intBaudRate, int intParity, int intBitsData)
            {
                  serial = new System.IO.Ports.SerialPort();
                  serial.PortName = strPortName;
                  serial.BaudRate = intBaudRate;
                  serial.DtrEnable = true;
                  serial.ReadTimeout = 3000;
                  serial.DataBits = intBitsData;
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
                  estado = true;
                  this.hsCampos = new Hashtable();
            }

            public void Open() 
            {
                  serial.Open();
            }

            public void Clear()
            {
                  hsCampos.Clear();
            }

            public void SetField(string strCampo,string strValor)
            {
                  hsCampos.Add(strCampo,strValor);
            }
            public int GetFieldsFromRes(String rpta)
            {
                  try
                  {
                        int j;

                        hsCampos.Clear();

                        byte[] binScanData2 = new byte[rpta.Length];

                        for (j = 0; j < rpta.Length; j++)
                        {
                              binScanData2[j] = (byte)rpta[j];
                        }

                        for (j = 0; j < binScanData2.Length; j++)
                        {
                              if (binScanData2[j] == 2) break;//BUSCANDO STX
                        }
                        if (j == binScanData2.Length)
                        {
                              return 1;//NO EXISTE STX
                        }
                        j++;
                        ////int sizeresp = binScanData2[j] * 256 + binScanData2[j + 1];
                        ////if (sizeresp <= 52)
                        ////{
                        ////      //NO LLEGO NINGUNA RESPUESTA CAMPO=VALOR
                        ////      return 1;
                        ////}

                        int tamaniototal = j + 54 + 2;
                        String resp2 = rpta.Substring(j + 54);
                        do
                        {
                              int idx = resp2.IndexOf(',');
                              if (idx < 0) break;
                              string longitud = resp2.Substring(0, idx);
                              int lencampo = Int32.Parse(longitud);
                              int idxigual = resp2.IndexOf('=');//"campo=......."
                              if (idxigual < 0) break;
                              string nombrecampo = resp2.Substring(idx + 1, idxigual - (idx + 1));
                              string valuecampo = resp2.Substring(idxigual + 1, lencampo - (idxigual - idx));
                              //METEMOS A UN HASHTABLE
                              hsCampos.Add(nombrecampo, valuecampo);
                              resp2 = resp2.Substring(lencampo + idx + 1);

                              //tamaniototal += (lencampo + idx);
                              //if (tamaniototal >= sizeresp) break;
                              //idx = resp2.IndexOf('\x1c');
                              //if (idx >= 0) resp2 = resp2.Substring(idx + 1);

                              idx = resp2.IndexOf('\x1c');
                              if (idx >= 0) resp2 = resp2.Substring(idx + 1);
                              else break;

                        } while (resp2.Length > 0);
                        return 0;
                  }
                  catch (Exception e)
                  {
                        throw;
                  }
            }
            public int ExisteETX(string rpta)
            {
                  int etx = 0;
                  int j;
                  byte[] binScanData2 = new byte[rpta.Length];

                  for (j = 0; j < rpta.Length; j++)
                  {
                        binScanData2[j] = (byte)rpta[j];
                  }

                  //BUSCANDO CABECERA
                  for (j = 0; j < binScanData2.Length; j++)
                  {
                        if (binScanData2[j] == 'H') break;
                  }
                  for (; j < binScanData2.Length; j++)
                  {
                        if (binScanData2[j] == 3)
                        {
                              etx = 1; break;
                        }
                  }

                  if (etx == 1)
                  {
                        return 1;//EXISTE STX
                  }
                  return 0;
            }

            /// <summary>
            /// Envia una transaccion al PINPAD
            /// </summary>
            /// <returns>0:Envio Correcto,1:Excedio Time Out de Espera,2: Excepcion de Escritura </returns>
            public int SendTran() 
            {
                  int iLRC, intRpta, j;
                  ACS_Util Obj_Util=new ACS_Util();
                  try
                  {
                      String s = ACS_Define.Dsc_HeadTrans;
                      //JAC
                      Monitor.Enter(this);
                      Serial.Open();
                      //JAC
                      if (hsCampos[ACS_Define.Cod_Transaccion] != null)
                      {
                          s += '\x1c';
                          s += FieldAndValue(ACS_Define.Cod_Transaccion, (string)hsCampos[ACS_Define.Cod_Transaccion]);
                      }
                      if (hsCampos[ACS_Define.Dsc_Data_Adicional] != null)
                      {
                          s += '\x1c';
                          s += FieldAndValue(ACS_Define.Dsc_Data_Adicional, (string)hsCampos[ACS_Define.Dsc_Data_Adicional]);
                      }

                      if (hsCampos[ACS_Define.Dsc_Data_Track2] != null)
                      {
                          s += '\x1c';
                          s += FieldAndValue(ACS_Define.Dsc_Data_Track2, (string)hsCampos[ACS_Define.Dsc_Data_Track2]);
                      }

                      char[] cs = s.ToCharArray();
                      byte[] binScanData = new byte[cs.Length + 5];
                      binScanData[0] = 2;//STX
                      binScanData[binScanData.Length - 2] = 3;//ETX
                      for (j = 0; j < cs.Length; j++)
                      {
                          binScanData[j + 3] = (byte)cs[j];
                      }
                      binScanData[1] = (byte)(s.Length / 256);
                      binScanData[2] = (byte)(s.Length & 255);
                      iLRC = (byte)binScanData[1];
                      for (j = 2; j <= binScanData.Length - 2; j++)
                      {
                          iLRC ^= binScanData[j];
                      }
                      binScanData[binScanData.Length - 1] = (byte)iLRC;

                      string rpta;
                      intRpta = 1;
                      long startTick;
                      long seconds;
                      int tOut;

                      if (((string)hsCampos[ACS_Define.Cod_Transaccion]).Equals(ACS_Define.Cod_IniComPnd) ||
                           ((string)hsCampos[ACS_Define.Cod_Transaccion]).Equals(ACS_Define.Cod_TiemIniFormPnd)
                            )
                          tOut = TOutMin;
                      else
                          tOut = TOutMax;

                      serial.Write(binScanData, 0, binScanData.Length);
                      //USB
                      int val = serial.ReadByte();

                      startTick = DateTime.Now.Ticks;
                      rpta = "";
                      string rpta2;
                      do
                      {
                          rpta2 = serial.ReadExisting();
                          if (rpta2.Length > 0)
                          {
                              rpta += rpta2;
                              if (ExisteETX(rpta) == 1)
                              {
                                  intRpta = GetFieldsFromRes(rpta);
                                  serial.Write("\x6");
                                  break;
                              }
                          }
                          long endTick = DateTime.Now.Ticks;
                          long tick = endTick - startTick;
                          seconds = tick / TimeSpan.TicksPerSecond;
                      } while ((seconds <= tOut));

                      if ((rpta.Length < 1))
                      {
                          rpta = serial.ReadExisting();
                          if (rpta.Length > 5)
                          {
                              intRpta = GetFieldsFromRes(rpta);
                          }
                          serial.Write("\x6");
                      }
                      return intRpta;
                  }
                  catch (System.ServiceProcess.TimeoutException ex)
                  {
                      //Obj_Util.EscribirLog(this, ex.StackTrace);
                      //Obj_Util.EscribirLog(this, ex.Source);
                      //Obj_Util.EscribirLog(this, ex.Message);
                      return 2;

                  }
                  catch (Exception ex)
                  {
                      //Obj_Util.EscribirLog(this, ex.StackTrace);
                      //Obj_Util.EscribirLog(this, ex.Source);
                      //Obj_Util.EscribirLog(this, ex.Message);
                      return 2;
                  } 
                  //JAC
                  finally
                  {
                      Serial.Close();
                      Monitor.Exit(this);
                  }
                  //JAC

            }
            public void GetField(string strCampo, out string strValor)
            {
                  strValor = (string)hsCampos[strCampo];
            }
            private static string FieldAndValue(string nameField, string Value)
            {
                  int len = nameField.Length + 1 + Value.Length;
                  string ret = len.ToString();
                  ret += ",";
                  ret += nameField;
                  ret += "=";
                  ret += Value;
                  return ret;
            }

            
           
      }
}
