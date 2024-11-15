using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;
using LAP.TUUA.AccesoMolinete;

namespace DemoAccesoMolinete
{
    public class PLCDiscapacitados
    {

        private cConfigXml oAppConfig;
        private Hashtable htConfigWriters = new Hashtable();
        private Hashtable htConfigReaders = new Hashtable();

        private Hashtable htDefaultBits = new Hashtable();

        private System.Timers.Timer tmrOpenMolinete;
        bool isOpenPressed;
        bool isStatusMolActive;
        private System.Timers.Timer tmr_STATUS_Molinete;

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

        private EscenarioDiscapacitados escenarioDiscapacitados;

        public PLCDiscapacitados(EscenarioDiscapacitados escenarioDiscapacitados)
    {
      this.escenarioDiscapacitados = escenarioDiscapacitados;
      String sCnfgFile = LAP.TUUA.AccesoMolinete.Util.ApplicationPath + Define.XML_CONFIG_DISCAPACITADOS;
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
        isOpenPressed = false;
        isStatusMolActive = false;
        new Thread(new ThreadStart(runPLC)).Start();
        escenarioDiscapacitados.SetEnableButtons();
    }

    public void runPLC()
    {
        tmrOpenMolinete = new Timer();
        tmrOpenMolinete.Interval = 250;
        tmrOpenMolinete.Elapsed += new System.Timers.ElapsedEventHandler(tmrOpenMolinete_Elapsed);

        tmr_STATUS_Molinete = new Timer();
        tmr_STATUS_Molinete.Interval = 250;
        tmr_STATUS_Molinete.Elapsed += new System.Timers.ElapsedEventHandler(tmr_STATUS_Molinete_Elapsed);

        System.Timers.Timer tmrSemaforo = new Timer();
        tmrSemaforo.Interval = 50;
        tmrSemaforo.Elapsed += new System.Timers.ElapsedEventHandler(tmrSemaforo_Elapsed);

        tmrOpenMolinete.Enabled = true;
        tmr_STATUS_Molinete.Enabled = true;
        tmrSemaforo.Enabled = true;
    }

    private void tmrSemaforo_Elapsed(object sender, ElapsedEventArgs e)
    {
        byte statusActualSemRojo = GetSemaforoRojo();
        if (statusActualSemRojo != statusSemRojo)
        {
            statusSemRojo = statusActualSemRojo;
            escenarioDiscapacitados.SetMostrarSemaforo(0, statusSemRojo);
            return;
        }
        byte statusActualSemAmbar = GetSemaforoAmbar();
        if (statusActualSemAmbar != statusSemAmbar)
        {
            statusSemAmbar = statusActualSemAmbar;
            escenarioDiscapacitados.SetMostrarSemaforo(1, statusSemAmbar);
            return;
        }
        byte statusActualSemVerde = GetSemaforoVerde();
        if (statusActualSemVerde != statusSemVerde)
        {
            statusSemVerde = statusActualSemVerde;
            escenarioDiscapacitados.SetMostrarSemaforo(2, statusSemVerde);
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

    public byte GetOpenMolinete()
    {
        String[] sCnfg = ((String)htConfigReaders[Define.OPEN_MOLINETE]).Split(':');
        int iPort = int.Parse(sCnfg[0]);
        int iBit = int.Parse(sCnfg[1]);
        return Util.ReadPuerto(iPort, iBit);
    }


    private void tmr_STATUS_Molinete_Elapsed(object sender, ElapsedEventArgs e)
    {
        //Para efectos de cambiar solo la visualizacion de la pantalla.
        int get_OUTPUTSIGN_STATUS_Molinete = Get_OUTPUTSIGN_STATUS_Molinete();
        if (get_OUTPUTSIGN_STATUS_Molinete == (byte)htDefaultBits[Define.OUTPUTSIGN_STATUS_MOLINETE] && isStatusMolActive)
        {
            isStatusMolActive = false;
            escenarioDiscapacitados.SetEnablePaletaTimer();
            Thread.Sleep(3000);
            escenarioDiscapacitados.SetChangeStatusMolinete(0);
        }
        else if (get_OUTPUTSIGN_STATUS_Molinete == (~(byte)htDefaultBits[Define.OUTPUTSIGN_STATUS_MOLINETE] & 0x01) && !isStatusMolActive)
        {
            isStatusMolActive = true;
        }
    }

    private void tmrOpenMolinete_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //Para efectos de cambiar solo la visualizacion de la pantalla.
        int getOpenMolinete = GetOpenMolinete();
        if (getOpenMolinete == (~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01) && !isOpenPressed)
        {
            isOpenPressed = true;
            escenarioDiscapacitados.SetEnablePaletaTimer();
            Thread.Sleep(3000);
            //Si la sgte linea es comentada, utilizar el pulsador (no es un pulso sino que es permanente)
            //Set_OUTPUTSIGN_STATUS_Molinete(~(byte)htDefaultBits[Define.OUTPUTSIGN_STATUS_MOLINETE] & 0x01);
            escenarioDiscapacitados.SetChangeStatusMolinete(1);
        }
        else if(getOpenMolinete == (byte)htDefaultBits[Define.OPEN_MOLINETE] && isOpenPressed)
        {
            isOpenPressed = false;
            Thread.Sleep(50);
            //Si la sgte linea es comentada, utilizar el pulsador (no es un pulso sino que es permanente)
            //Set_OUTPUTSIGN_STATUS_Molinete((byte)htDefaultBits[Define.OUTPUTSIGN_STATUS_MOLINETE]);
        }
    }

    private byte Get_OUTPUTSIGN_STATUS_Molinete()
    {
        String[] sCnfg = ((String)htConfigWriters[Define.OUTPUTSIGN_STATUS_MOLINETE]).Split(':');//Uso htConfigWriters para lectura, pues es solo para la demo. En la vida real obtiene un impulso electrico (que lo simula el timer)
        int iPort = int.Parse(sCnfg[0]);
        int iBit = int.Parse(sCnfg[1]);
        return Util.ReadPuerto(iPort, iBit);
    }

    private int Set_OUTPUTSIGN_STATUS_Molinete(int accion)
    {
        String[] sCnfg = ((String)htConfigWriters[Define.OUTPUTSIGN_STATUS_MOLINETE]).Split(':');
        int iPort = int.Parse(sCnfg[0]);
        int iBit = int.Parse(sCnfg[1]);
        return Util.WritePuerto(iPort, iBit, accion);
    }

    }
}
