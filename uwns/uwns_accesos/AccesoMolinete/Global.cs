using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LAP.TUUA.AccesoMolinete
{
  public class Global
  {

    //1) Con el circuito electrico. 

    //En el metodo counterTimer_Tick (clase Escenario), la linea plc.MolineteGirado(); debe estar comentada.
    //y utilizar manualmente el pulsador apenas salga el MessageBox.Show("Ingresó el Pasajero"); --> Ojo pulsar y soltar por 500 ms y no tan rapido (pues se efecto de no actualizar ya sea el semaforo y/o el lblstatus)
    //En el metodo GetGiroMolinete (clase PLC), el retorno debe ser: return Util.ReadPuerto(iPort, iBit);
    //En el metodo SetGiroMolinete (clase PLC), el retorno debe ser: return Util.WritePuerto(iPort, iBit, accion);
    //En el metodo MolineteAprobadoNormal (clase Molinete), debe ser el bucle: while (Util.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)
    //En el metodo MolineteAprobadoEspecial (clase Molinete), debe ser el bucle: while (Util.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)
    
    //Clase PLCDiscapacitados, metodo tmrOpenMolinete_Elapsed, comentar las 2 lineas: Set_OUTPUTSIGN_STATUS_Molinete

    #region Metodo 1 Real
    

    [DllImport("HDriverNT.dll")]
    public static extern int OpenDriver();

    [DllImport("HDriverNT.dll")]
    public static extern int WritePort(int iPort, int iSet);

    [DllImport("HDriverNT.dll")]
    public static extern int ReadPort(int iPort);
    
    public static int WritePortVirtual(int iPort, int iSet)
    {
      throw new NotImplementedException();
    }

    public static int ReadPortVirtual(int iPort)
    {
      throw new NotImplementedException();
    }

    
    #endregion

    //2) Pruebas con el circuito electrico pero con el puerto virtual de giro de molinete (en lugar de usar pulsador)

    //En el metodo counterTimer_Tick (clase Escenario), la linea plc.MolineteGirado(); debe estar descomentada.
    //En el metodo GetGiroMolinete (clase PLC), el retorno debe ser: return UtilPLC.ReadPuerto(iPort, iBit);
    //En el metodo SetGiroMolinete (clase PLC), el retorno debe ser: return UtilPLC.WritePuerto(iPort, iBit, accion);
    //En el metodo MolineteAprobadoNormal (clase Molinete), debe ser el bucle: while (UtilPLC.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)
    //En el metodo MolineteAprobadoEspecial (clase Molinete), debe ser el bucle: while (UtilPLC.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)

    #region Metodo 2

    /*
    private static byte[] ports;

    static Global()
    {
      ports = new byte[]{
                            0, 0, 0, 0, 0, 0, 0, 0, //PORT A    ==>  1, 0, 1, 0, 0, 0, 0, 0 = 00000101 = 5
                            0, 0, 0, 0, 0, 0, 0, 0, //PORT B
                            0, 0, 0, 0, //PORT CL   ==>  1,0,0,0 = 0001
                            0, 0, 0, 0  //PORT CH
                          };      
    }

    [DllImport("HDriverNT.dll")]
    public static extern int OpenDriver();

    [DllImport("HDriverNT.dll")]
    public static extern int WritePort(int iPort, int iSet);

    public static int WritePortVirtual(int iPort, int iSet)
    {
      if (iSet != 0 && iSet != 1)
        return 0;
      if (iPort >= 0 && iPort < 24)
      {
        ports[iPort] = (byte)iSet;
      }
      else
      {
        return 0;
      }
      return 1;
    }

    [DllImport("HDriverNT.dll")]
    public static extern int ReadPort(int iPort);

    public static int ReadPortVirtual(int iPort)
    {
      int byteNumber = 0;

      switch (iPort)
      {
        case 0:
          byteNumber = getBytePort(0, 8);
          break;
        case 1:
          byteNumber = getBytePort(8, 8);
          break;
        case 2:
          byteNumber = getBytePort(16, 4);
          break;
        case 3:
          byteNumber = getBytePort(20, 4);
          break;
      }
      return byteNumber;
    }

    private static int getBytePort(int iStart, int iLength)
    {
      String strbyte = "";
      for (int i = iStart; i < (iStart + iLength); i++)
      {
        strbyte = ports[i] + strbyte;
      }
      return System.Convert.ToByte(strbyte, 2);
    }
    */

    #endregion


    //3) Sin el circuito electrico

    //En el metodo counterTimer_Tick (clase Escenario), la linea plc.MolineteGirado(); debe estar descomentada.
    //Lo siguiente da igual usar Util o UtilPLC, pero para hacerlo mas parecido al producto final, usar Util.
    //En el metodo GetGiroMolinete (clase PLC), el retorno debe ser: return Util.ReadPuerto(iPort, iBit);
    //En el metodo SetGiroMolinete (clase PLC), el retorno debe ser: return Util.WritePuerto(iPort, iBit, accion);
    //En el metodo MolineteAprobadoNormal (clase Molinete), debe ser el bucle: while (Util.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)
    //En el metodo MolineteAprobadoEspecial (clase Molinete), debe ser el bucle: while (Util.ReadPuerto(iPort, iBit) == 0 && !forcedClosedMolinete)

    //Clase PLCDiscapacitados, metodo tmrOpenMolinete_Elapsed, descomentar las 2 lineas: Set_OUTPUTSIGN_STATUS_Molinete

    #region Metodo 3

    /*
    private static byte[] ports;

    public static int OpenDriver()
    {
      ports = new byte[]{
                            0, 0, 0, 0, 0, 0, 0, 0, //PORT A    ==>  1, 0, 1, 0, 0, 0, 0, 0 = 00000101 = 5
                            0, 0, 0, 0, 0, 0, 0, 0, //PORT B
                            0, 0, 0, 0, //PORT CL   ==>  1,0,0,0 = 0001
                            0, 0, 0, 0  //PORT CH
                          };
      return 1;
    }

    public static int WritePort(int iPort, int iSet)
    {
      //Probando sin el circuito.
      return WritePortVirtual(iPort, iSet);
    }

    public static int WritePortVirtual(int iPort, int iSet)
    {
      if (iSet != 0 && iSet != 1)
        return 0;
      if (iPort >= 0 && iPort < 24)
      {
        ports[iPort] = (byte)iSet;
      }
      else
      {
        return 0;
      }
      return 1;
    }

    public static int ReadPort(int iPort)
    {
      //Probando sin el circuito.
      return ReadPortVirtual(iPort);
    }

    public static int ReadPortVirtual(int iPort)
    {
      int byteNumber=0;

      switch (iPort)
      {
        case 0:
          byteNumber = getBytePort(0, 8);
          break;
        case 1:
          byteNumber = getBytePort(8, 8);
          break;
        case 2:
          byteNumber = getBytePort(16, 4);
          break;
        case 3:
          byteNumber = getBytePort(20, 4);
          break;
      }
      return byteNumber;
    }

    private static int getBytePort(int iStart, int iLength)
    {
      String strbyte = "";
      for (int i = iStart; i < (iStart + iLength); i++)
      {
        strbyte = ports[i] + strbyte;
      }
      return System.Convert.ToByte(strbyte, 2);
    }
    */

    #endregion

  }

}
