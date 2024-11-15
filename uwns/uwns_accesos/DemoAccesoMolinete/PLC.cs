using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using LAP.TUUA.AccesoMolinete;
using System.Threading;
using Timer=System.Timers.Timer;

namespace DemoAccesoMolinete
{
  public class PLC
  {
    private cConfigXml oAppConfig;
    private Hashtable htConfigWriters = new Hashtable();
    private Hashtable htConfigReaders = new Hashtable();

    private Hashtable htDefaultBits = new Hashtable();

    public Hashtable HtDefaultBits
    {
      get
      {
        return htDefaultBits;
      }
    }

    private byte statusSemRojo;
    private byte statusSemAmbar;
    private byte statusSemVerde;

    private Escenario escenario;

    public PLC(Escenario escenario)
    {
      this.escenario = escenario;
      String sCnfgFile = LAP.TUUA.AccesoMolinete.Util.ApplicationPath + Define.XML_CONFIG;
      oAppConfig = new cConfigXml(sCnfgFile);
      lecturaPuertos();
      setDefaultBits();
      setVariablesStatusSemDefaults();
    }
    private void setVariablesStatusSemDefaults()
    {
      statusSemRojo = (byte)htDefaultBits[Define.SEMAFORO_ROJO];
      statusSemAmbar = (byte)htDefaultBits[Define.SEMAFORO_AMBAR];
      statusSemVerde = (byte)htDefaultBits[Define.SEMAFORO_VERDE];
    }

    private void lecturaPuertos()
    {
      int iWriteSensors = int.Parse(oAppConfig.GetValue(Define.KEY_PARAMS, "WriteSensors"));
      int iReadSensors = int.Parse(oAppConfig.GetValue(Define.KEY_PARAMS, "ReadSensors"));
      for (int i = 0; i < iWriteSensors; i++)
      {
        string sCode = oAppConfig.GetValue(Define.KEY_WRITER + i, "Code");
        string sConfig = oAppConfig.GetValue(Define.KEY_WRITER + i, "Config");
        htConfigWriters.Add(sCode, sConfig);
      }
      for (int i = 0; i < iReadSensors; i++)
      {
        string sCode = oAppConfig.GetValue(Define.KEY_READER + i, "Code");
        string sConfig = oAppConfig.GetValue(Define.KEY_READER + i, "Config");
        htConfigReaders.Add(sCode, sConfig);
      }
    }

    private void setDefaultBits()
    {
      int iWriteSensors = int.Parse(oAppConfig.GetValue(Define.KEY_PARAMS, "WriteSensors"));
      int iReadSensors = int.Parse(oAppConfig.GetValue(Define.KEY_PARAMS, "ReadSensors"));
      for (int i = 0; i < iWriteSensors; i++)
      {
        string sCode = oAppConfig.GetValue(Define.KEY_WRITER + i, "Code");
        string defaultBit = oAppConfig.GetValue(Define.KEY_WRITER + i, "DefaultBit");
        htDefaultBits.Add(sCode, Byte.Parse(defaultBit));
      }
      for (int i = 0; i < iReadSensors; i++)
      {
        string sCode = oAppConfig.GetValue(Define.KEY_READER + i, "Code");
        string defaultBit = oAppConfig.GetValue(Define.KEY_READER + i, "DefaultBit");
        htDefaultBits.Add(sCode, Byte.Parse(defaultBit));
      }
    }

    public void Iniciar()
    {
      new Thread(new ThreadStart(runPLC)).Start();
      escenario.SetEnableButtons();
    }

    public void runPLC()
    {
      System.Timers.Timer tmrOpenMolinete = new Timer();
      tmrOpenMolinete.Interval = 250;
      tmrOpenMolinete.Elapsed += new System.Timers.ElapsedEventHandler(tmrOpenMolinete_Elapsed);

      System.Timers.Timer tmrCloseOrGiroMolinete = new Timer();
      tmrCloseOrGiroMolinete.Interval = 250;
      tmrCloseOrGiroMolinete.Elapsed += new System.Timers.ElapsedEventHandler(tmrCloseOrGiroMolinete_Elapsed);

      System.Timers.Timer tmrSemaforo = new Timer();
      tmrSemaforo.Interval = 50;
      tmrSemaforo.Elapsed += new System.Timers.ElapsedEventHandler(tmrSemaforo_Elapsed);

      tmrOpenMolinete.Enabled = true;
      tmrCloseOrGiroMolinete.Enabled = true;
      tmrSemaforo.Enabled = true;
    }

    private void tmrSemaforo_Elapsed(object sender, ElapsedEventArgs e)
    {
      byte statusActualSemRojo = GetSemaforoRojo();
      if (statusActualSemRojo != statusSemRojo)
      {
        statusSemRojo = statusActualSemRojo;
        escenario.SetMostrarSemaforo(0, statusSemRojo);
        return;
      }
      byte statusActualSemAmbar = GetSemaforoAmbar();
      if (statusActualSemAmbar != statusSemAmbar)
      {
        statusSemAmbar = statusActualSemAmbar;
        escenario.SetMostrarSemaforo(1, statusSemAmbar);
        return;
      }
      byte statusActualSemVerde = GetSemaforoVerde();
      if (statusActualSemVerde != statusSemVerde)
      {
        statusSemVerde = statusActualSemVerde;
        escenario.SetMostrarSemaforo(2, statusSemVerde);
        return;
      }
    }

    private byte GetSemaforoRojo()
    {
      String[] sCnfg = ((String)htConfigReaders[Define.SEMAFORO_ROJO]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    private byte GetSemaforoAmbar()
    {
      String[] sCnfg = ((String)htConfigReaders[Define.SEMAFORO_AMBAR]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    private byte GetSemaforoVerde()
    {
      String[] sCnfg = ((String)htConfigReaders[Define.SEMAFORO_VERDE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    private byte GetOpenMolinete()
    {
      String[] sCnfg = ((String)htConfigReaders[Define.OPEN_MOLINETE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    public byte GetCloseMolinete()
    {
      String[] sCnfg = ((String)htConfigReaders[Define.CLOSE_MOLINETE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    private void tmrCloseOrGiroMolinete_Elapsed(object sender, ElapsedEventArgs e)
    {
      //Para efectos de cambiar solo la visualizacion de la pantalla.
      if (GetCloseMolinete() == (~(byte)htDefaultBits[Define.CLOSE_MOLINETE] & 0x01) ||
        GetGiroMolinete() == (~(byte)htDefaultBits[Define.GIRO_MOLINETE] & 0x01))
      {
        escenario.SetChangeStatusMolinete(0);
      }
    }

    private void tmrOpenMolinete_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      //Para efectos de cambiar solo la visualizacion de la pantalla.
      if (GetOpenMolinete() == (~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01))
      {
        escenario.SetChangeStatusMolinete(1);
      }
    }

    private byte GetGiroMolinete()
    {
      String[] sCnfg = ((String)htConfigWriters[Define.GIRO_MOLINETE]).Split(':');//Uso htConfigWriters para lectura, pues es solo para la demo. En la vida real obtiene un impulso electrico (que lo simula el timer)
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.ReadPuerto(iPort, iBit);
    }

    private int SetGiroMolinete(int accion)
    {
      String[] sCnfg = ((String)htConfigWriters[Define.GIRO_MOLINETE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.WritePuerto(iPort, iBit, accion);
    }

    //Para efectos de simular que el pasajero justo ha pasado el molinete.
    public void MolineteGirado()
    {
      SetGiroMolinete(~(byte)htDefaultBits[Define.GIRO_MOLINETE] & 0x01);//Pulsar el pulsador respectivo al Puerto A - Bit 0.
      new Thread(setDefaultGiroMolinete).Start();
    }

    private void setDefaultGiroMolinete()
    {
      Thread.Sleep(500);
      SetGiroMolinete((byte)htDefaultBits[Define.GIRO_MOLINETE]);//Soltar el pulsador (o dejo de Pulsar) respectivo al Puerto A - Bit 0.
    }



  }



}
