using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.AccesoMolinete
{
  public class Util
  {
    #region Escritura de Puerto
    public static int WritePuerto(int iPort, int iBit, int iSet)
    {
      //Port A : 0
      //Port B : 1
      //Port C : 2
      //Valor absoluto de puerto = Port X * iBit

        if (iPort < 0 || iPort > 3)
        {
            return 0;
        }
        int portNumber;
        if(iPort==3)
        {
            if (iBit < 0 || iBit > 3)
                return 0;
            portNumber = (iPort * 8) - 4 + (iBit);
        }   
        else if(iPort==2)
        {
          if (iBit < 0 || iBit > 3)
            return 0;
          portNumber = (iPort * 8) + (iBit);                
        }
        else
        {
            if (iBit < 0 || iBit > 7)
                return 0;
            portNumber = (iPort * 8) + (iBit);      
        }
       
      int res = Global.WritePort(portNumber, iSet);

      return res;
    }
    #endregion

    #region Lectura de Puerto
    public static byte ReadPuerto(int iPort, int iBit)
    {
      byte ReadByte;
      string sBytes;
      byte b = 0;

      ReadByte = (byte)Global.ReadPort(iPort);

      if (ReadByte >= 0 && ReadByte <= 255)
      {
        sBytes = Convert.ToString(ReadByte, 2);

        //se completa los n bits faltantes
        sBytes = sBytes.PadLeft(8, '0');

        switch (iBit)
        {
          case 0:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 0
            break;
          case 1:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 1
            break;
          case 2:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 2
            break;
          case 3:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 3
            break;
          case 4:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 4
            break;
          case 5:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 5
            break;
          case 6:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 6
            break;
          case 7:
            b = Convert.ToByte(sBytes.Substring((sBytes.Length - iBit - 1), 1)); //bit 7
            break;
        }
      }

      return b;
    }
    #endregion

    #region Util
    //Función para obtener ruta donde se ejecuta la aplicación
    public static string ApplicationPath
    {
      get
      {
        return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\";
        //return AppDomain.CurrentDomain.BaseDirectory;
      }
    }
    #endregion
  }
}
