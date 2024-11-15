using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using Timer=System.Timers.Timer;

namespace LAP.TUUA.AccesoMolinete
{
  public class MolineteDiscapacitados
  {
    private cConfigXml oAppConfig;

    private Hashtable htConfigWriters = new Hashtable();
    private Hashtable htConfigReaders = new Hashtable();

    private Hashtable htDefaultBits = new Hashtable();

    private int timeSemaforoRojo;
    private int timeSemaforoAmbar;
    private int timeSemaforoVerde;
    private int timeSemaforoAmbar2;

    private int timeoutOpenAmbar;
    private int timeoutCloseAmbar;
    private int timeoutOpenVerde;
    private int timeoutCloseVerde;

    private String soundFileSemaforoRojo;
    private String soundFileSemaforoAmbar;
    private String soundFileSemaforoVerde;
    private String soundFileSemaforoAmbar2;

    private int typeSoundSemaforoRojo;
    private int typeSoundSemaforoAmbar;
    private int typeSoundSemaforoVerde;
    private int typeSoundSemaforoAmbar2;

    private System.Media.SoundPlayer myPlayer = new SoundPlayer();
    private bool notReceivedInputSignal;

    //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN
    private bool isEstadoOpen; 
    private bool isActivoOpenEspecialDiscap; 
    private bool isCierreForsado; 
    //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END


    public MolineteDiscapacitados()
    {
      try
      {
        String sCnfgFile = Util.ApplicationPath + Define.XML_CONFIG_DISCAPACITADOS;
        oAppConfig = new cConfigXml(sCnfgFile);
        lecturaPuertos();
        lecturaTiempos();
        lecturaSonidos();
        setDefaultBits();
        //OpenDriverMolinetes();

        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN
        this.isCierreForsado = false;
        this.isActivoOpenEspecialDiscap = false;
        this.isEstadoOpen = false;
        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

      public void OpenDriverMolinetes()
      {
          if (Global.OpenDriver() != 1)//Abro el puerto para escribir. No es necesario si solo voy a leer.
          {
              throw new Exception("Error al intentar abrir el Driver.");
          }
          if (!setDefaults())
          {
              throw new Exception("Error al intentar poner a default el Driver.");
          }          
      }

    public MolineteDiscapacitados(int timeSemaforoRojo, int timeSemaforoAmbar, int timeSemaforoVerde,
        String soundFileSemaforoRojo, String soundFileSemaforoAmbar, String soundFileSemaforoVerde,
        int typeSoundSemaforoRojo, int typeSoundSemaforoAmbar, int typeSoundSemaforoVerde)
    {
      try
      {
        String sCnfgFile = Util.ApplicationPath + Define.XML_CONFIG_DISCAPACITADOS;
        oAppConfig = new cConfigXml(sCnfgFile);
        lecturaPuertos();
        setDefaultBits();

        this.timeSemaforoRojo = timeSemaforoRojo;
        this.timeSemaforoAmbar = timeSemaforoAmbar;
        this.timeSemaforoVerde = timeSemaforoVerde;

        this.soundFileSemaforoRojo = soundFileSemaforoRojo;
        this.soundFileSemaforoAmbar = soundFileSemaforoRojo;
        this.soundFileSemaforoVerde = soundFileSemaforoRojo;

        this.typeSoundSemaforoRojo = typeSoundSemaforoRojo;
        this.typeSoundSemaforoAmbar = typeSoundSemaforoAmbar;
        this.typeSoundSemaforoVerde = typeSoundSemaforoVerde;

        //OpenDriverMolinetes();

        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN
        this.isCierreForsado = false;
        this.isActivoOpenEspecialDiscap = false;
        this.isEstadoOpen = false;
        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END

      }
      catch (Exception ex)
      {
        throw ex;
      }
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

    private void lecturaTiempos()
    {
      timeSemaforoRojo = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_ROJO, "Time")));
      timeSemaforoAmbar = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "Time")));
      timeSemaforoVerde = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_VERDE, "Time")));
      timeSemaforoAmbar2 = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "Time")));

      timeoutOpenAmbar = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "TimeoutOpen")));
      timeoutCloseAmbar = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "TimeoutClose")));
      timeoutOpenVerde = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_VERDE, "TimeoutOpen")));
      timeoutCloseVerde = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_VERDE, "TimeoutClose")));

    }

    private void lecturaSonidos()
    {
      soundFileSemaforoRojo = oAppConfig.GetValue(Define.SEMAFORO_ROJO, "SoundFile");
      soundFileSemaforoAmbar = oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "SoundFile");
      soundFileSemaforoVerde = oAppConfig.GetValue(Define.SEMAFORO_VERDE, "SoundFile");
      soundFileSemaforoAmbar2 = oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "SoundFile");

      typeSoundSemaforoRojo = oAppConfig.GetValue(Define.SEMAFORO_ROJO, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYNORMAL) ? 0 : (oAppConfig.GetValue(Define.SEMAFORO_ROJO, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYLOOP) ? 1 : 0);
      typeSoundSemaforoAmbar = oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYNORMAL) ? 0 : (oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYLOOP) ? 1 : 0);
      typeSoundSemaforoVerde = oAppConfig.GetValue(Define.SEMAFORO_VERDE, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYNORMAL) ? 0 : (oAppConfig.GetValue(Define.SEMAFORO_VERDE, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYLOOP) ? 1 : 0);
      typeSoundSemaforoAmbar2 = oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYNORMAL) ? 0 : (oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "TypeSound").ToUpper().Equals(Define.SOUNDPLAYLOOP) ? 1 : 0);
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

    private bool setDefaults()
    {
      bool ret = true;
      if(SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE])!=1)
      {
        ret = false;
      }
      if(SetSemaforoLuzRojo((byte)htDefaultBits[Define.SEMAFORO_ROJO])!=1)
      {
        ret = false;
      }
      if(SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR])!=1)
      {
        ret = false;
      }
      if(SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE])!=1)
      {
        ret = false;
      }
      return ret;
    }

    private int SetOpenMolinete(int accion)
    {
      String[] sCnfg = ((String)htConfigWriters[Define.OPEN_MOLINETE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.WritePuerto(iPort, iBit, accion);
    }

    private int GetInputSignStatusMolinete()
    {
        String[] sCnfg = ((String)htConfigReaders[Define.INPUTSIGN_STATUS_MOLINETE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      int res = Util.ReadPuerto(iPort, iBit);
      return res;
    }

    private int SetSemaforoLuzRojo(int accion)
    {
      String[] sCnfg = ((String)htConfigWriters[Define.SEMAFORO_ROJO]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.WritePuerto(iPort, iBit, accion);
    }

    private int SetSemaforoLuzVerde(int accion)
    {
      String[] sCnfg = ((String)htConfigWriters[Define.SEMAFORO_VERDE]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.WritePuerto(iPort, iBit, accion);
    }

    private int SetSemaforoLuzAmbar(int accion)
    {
      String[] sCnfg = ((String)htConfigWriters[Define.SEMAFORO_AMBAR]).Split(':');
      int iPort = int.Parse(sCnfg[0]);
      int iBit = int.Parse(sCnfg[1]);
      return Util.WritePuerto(iPort, iBit, accion);
    }
      
    //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN
    public bool IsEstadoOpen
    {
        get { return isEstadoOpen; }
        set { isEstadoOpen = value; }
    }

    public bool IsActivoOpenEspecialDiscap
    {
        get { return isActivoOpenEspecialDiscap; }
        set { isActivoOpenEspecialDiscap = value; }
    }

    public bool IsCierreForsado
    {
        get { return isCierreForsado; }
        set { isCierreForsado = value; }
    }
    
    public String CierreMolineteDiscapacitado()
    {
        String[] sCnfg = ((String)htConfigReaders[Define.INPUTSIGN_STATUS_MOLINETE]).Split(':');
        int iPort = int.Parse(sCnfg[0]);
        int iBit = int.Parse(sCnfg[1]);
        int defaultInputSignStatusMolinete = (byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE];
        int notDefaultInputSignStatusMolinete = ~(byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE] & 0x01;
        int bitInputSignStatusMolinete;
        String SLoger = String.Empty;
        Timer timerWaitForInputSignal;

        try
        {

            SLoger = SLoger + "// - Validamos el estado de apertura del molinete " + this.IsEstadoOpen.ToString();
            if (this.IsEstadoOpen)
            {
                int iTiempo;
                SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
                SLoger = SLoger + "// - Cerramos Molinete ";
                this.IsEstadoOpen = false;
                LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "03";

                SLoger = SLoger + "// - Validamos de que tipo de luz aparece Ambar o Verde ";
                if (this.IsActivoOpenEspecialDiscap)
                {
                    SLoger = SLoger + "// - Close de la Luz Ambar ";
                    SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]);
                    iTiempo = timeoutCloseAmbar;
                }
                else
                {
                    SLoger = SLoger + "// - Close de la Luz Verde ";
                    SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE]);
                    iTiempo = timeoutCloseVerde;
                }

                SLoger = SLoger + "// - Apagamos Musica ";
                myPlayer.Stop();
                this.IsCierreForsado = true;

                SLoger = SLoger + "// - Iniciamos Timer de Cierre de Molinete. " + this.IsCierreForsado.ToString();
                //Timer Standar - Asociar otros Datos
                notReceivedInputSignal = false;
                timerWaitForInputSignal = new Timer();
                if (timeoutCloseVerde > 0)
                {

                    timerWaitForInputSignal.AutoReset = false;
                    timerWaitForInputSignal.Interval = iTiempo;
                    timerWaitForInputSignal.Elapsed += new System.Timers.ElapsedEventHandler(timerWaitForInputSignal_Elapsed);
                    timerWaitForInputSignal.Start();
                }

                while ((bitInputSignStatusMolinete = Util.ReadPuerto(iPort, iBit)) == notDefaultInputSignStatusMolinete && !notReceivedInputSignal)
                {
                    Thread.Sleep(250);
                }
                //Fin Timer de espera de cierre
                this.IsCierreForsado = false;
                SLoger = SLoger + "// - Finalizamos Timer de Cierre de Molinete. " + this.IsCierreForsado.ToString();
            }
        }
        catch (Exception ex)
        {
            SLoger = SLoger + "= ERROR : " + ex.Message;
        }
        finally
        {
            this.IsCierreForsado = false;
            this.IsEstadoOpen = false;
            LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "03";
        }

        return  SLoger;
    }
    //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END

    public bool MolineteDiscapacitadosAprobadoNormal()
    {
        String[] sCnfg = ((String)htConfigReaders[Define.INPUTSIGN_STATUS_MOLINETE]).Split(':');
        int iPort = int.Parse(sCnfg[0]);
        int iBit = int.Parse(sCnfg[1]);
        int defaultInputSignStatusMolinete = (byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE];
        int notDefaultInputSignStatusMolinete = ~(byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE] & 0x01;

        int bitInputSignStatusMolinete;
        this.IsActivoOpenEspecialDiscap = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES          

      SetOpenMolinete(~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01);

      notReceivedInputSignal = false;
      Timer timerWaitForInputSignal = new Timer();
      if (timeoutOpenVerde > 0)
      {
          timerWaitForInputSignal.AutoReset = false;
          timerWaitForInputSignal.Interval = timeoutOpenVerde;
          timerWaitForInputSignal.Elapsed += new System.Timers.ElapsedEventHandler(timerWaitForInputSignal_Elapsed);
          timerWaitForInputSignal.Start();
      }

      while ((bitInputSignStatusMolinete = Util.ReadPuerto(iPort, iBit)) == defaultInputSignStatusMolinete && !notReceivedInputSignal)
      {
          Thread.Sleep(250);
      }

        timerWaitForInputSignal.Enabled = false;

        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES: COMENTADO PARA EVITAR CASOS DE TRUNCAMIENTO DEL PROCESO DE APERTURA
        //if(bitInputSignStatusMolinete == defaultInputSignStatusMolinete)
        //{
        //    SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
        //    this.IsEstadoOpen = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
        //    LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "02"; 
        //    return false;//Esta bien que de false
        //}

        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN
        this.IsEstadoOpen = true; //Donde definimos donde inicia la apertura de lectura para cierre
        LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "1";
        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END
        
        SetSemaforoLuzVerde(~(byte)htDefaultBits[Define.SEMAFORO_VERDE] & 0x01);

        String soundLocation = Util.ApplicationPath + soundFileSemaforoVerde;//no hay problema si soundFileSemaforoVerde=null
        if (File.Exists(soundLocation))
        {
            myPlayer.SoundLocation = soundLocation;
            if (typeSoundSemaforoVerde == 0)
                myPlayer.Play();
            else
                myPlayer.PlayLooping();
        }


        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - BEGIN                   
        int iCont = 0;

        while (iCont < 100)
        {
            if (this.IsCierreForsado)
            {
                iCont = 100;
            }
            else
            {
                Thread.Sleep(timeSemaforoVerde / 100);
                iCont = iCont + 1;
            }
        }

        if (!this.IsCierreForsado)
        {
            SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
            this.IsEstadoOpen = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
            LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "02"; 
            myPlayer.Stop();
            SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE]);

            notReceivedInputSignal = false;
            timerWaitForInputSignal = new Timer();
            if (timeoutCloseVerde > 0)
            {

                timerWaitForInputSignal.AutoReset = false;
                timerWaitForInputSignal.Interval = timeoutCloseVerde;
                timerWaitForInputSignal.Elapsed += new System.Timers.ElapsedEventHandler(timerWaitForInputSignal_Elapsed);
                timerWaitForInputSignal.Start();
            }

            while ((bitInputSignStatusMolinete = Util.ReadPuerto(iPort, iBit)) == notDefaultInputSignStatusMolinete && !notReceivedInputSignal)
            {
                Thread.Sleep(250);
            }

            timerWaitForInputSignal.Enabled = false;

            if (bitInputSignStatusMolinete == notDefaultInputSignStatusMolinete)
            {
                //SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
                return false;//Controlarlo no deberia dar false.
            }
        }
        //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES - END

        return true;
    }

      private void timerWaitForInputSignal_Elapsed(object sender, ElapsedEventArgs e)
      {
          notReceivedInputSignal = true;
      }

      public bool MolineteDiscapacitadosAprobadoEspecial()
      {
          String[] sCnfg = ((String)htConfigReaders[Define.INPUTSIGN_STATUS_MOLINETE]).Split(':');
          int iPort = int.Parse(sCnfg[0]);
          int iBit = int.Parse(sCnfg[1]);
          int defaultInputSignStatusMolinete = (byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE];
          int notDefaultInputSignStatusMolinete = ~(byte)htDefaultBits[Define.INPUTSIGN_STATUS_MOLINETE] & 0x01;

          int bitInputSignStatusMolinete;
          this.IsActivoOpenEspecialDiscap = true; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES

          SetOpenMolinete(~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01);

          notReceivedInputSignal = false;
          Timer timerWaitForInputSignal = new Timer();
          if (timeoutOpenAmbar > 0)
          {
              timerWaitForInputSignal.AutoReset = false;
              timerWaitForInputSignal.Interval = timeoutOpenAmbar;
              timerWaitForInputSignal.Elapsed += new System.Timers.ElapsedEventHandler(timerWaitForInputSignal_Elapsed);
              timerWaitForInputSignal.Start();
          }

          while ((bitInputSignStatusMolinete = Util.ReadPuerto(iPort, iBit)) == defaultInputSignStatusMolinete && !notReceivedInputSignal)
          {
              Thread.Sleep(250);
          }

          timerWaitForInputSignal.Enabled = false;

          //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES: COMENTADO PARA EVITAR CASOS DE TRUNCAMIENTO DEL PROCESO DE APERTURA
          //if (bitInputSignStatusMolinete == defaultInputSignStatusMolinete)
          //{
          //    SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
          //    this.IsEstadoOpen = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
          //    LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "02"; 
          //    this.IsActivoOpenEspecialDiscap = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
          //    return false;//Esta bien que de false
          //}

          //LAP-TUUA-9163 - 12-06-2012 (RS) CMONTES - BEGIN
          this.IsEstadoOpen = true;
          LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "1";
          //LAP-TUUA-9163 - 12-06-2012 (RS) CMONTES - END

          SetSemaforoLuzAmbar(~(byte)htDefaultBits[Define.SEMAFORO_AMBAR] & 0x01);

          String soundLocation = Util.ApplicationPath + soundFileSemaforoAmbar;
          if (File.Exists(soundLocation))
          {
              myPlayer.SoundLocation = soundLocation;
              if (typeSoundSemaforoAmbar == 0)
                  myPlayer.Play();
              else
                  myPlayer.PlayLooping();
          }

          //LAP-TUUA-9163 - 12-06-2012 (RS) CMONTES - BEGIN
          int iCont = 0;

          while (iCont < 100)
          {
              if (IsCierreForsado)
              {
                  iCont = 100;
              }
              else
              {
                  Thread.Sleep(timeSemaforoAmbar / 100);
                  iCont = iCont + 1;
              }
          }

          if (!this.IsCierreForsado)
          {
              SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
              this.IsEstadoOpen = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
              LAP.TUUA.UTIL.Define.FLG_GATE_OPEN = "02"; 
              this.IsActivoOpenEspecialDiscap = false; //LAP-TUUA-9163 - 07-06-2012 (RS) CMONTES
              myPlayer.Stop();
              SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]);

              notReceivedInputSignal = false;
              timerWaitForInputSignal = new Timer();
              if (timeoutCloseAmbar > 0)
              {

                  timerWaitForInputSignal.AutoReset = false;
                  timerWaitForInputSignal.Interval = timeoutCloseAmbar;
                  timerWaitForInputSignal.Elapsed += new System.Timers.ElapsedEventHandler(timerWaitForInputSignal_Elapsed);
                  timerWaitForInputSignal.Start();
              }

              while ((bitInputSignStatusMolinete = Util.ReadPuerto(iPort, iBit)) == notDefaultInputSignStatusMolinete && !notReceivedInputSignal)
              {
                  Thread.Sleep(250);
              }

              timerWaitForInputSignal.Enabled = false;

              if (bitInputSignStatusMolinete == notDefaultInputSignStatusMolinete)
              {
                  //SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
                  return false;//Controlarlo no deberia dar false.
              }
          }
          //LAP-TUUA-9163 - 12-06-2012 (RS) CMONTES - END

          return true;
      }

    public void MolineteDiscapacitadosDenegado()
    {
      SetSemaforoLuzRojo(~(byte)htDefaultBits[Define.SEMAFORO_ROJO] & 0x01);
      String soundLocation = Util.ApplicationPath + soundFileSemaforoRojo;
      if (File.Exists(soundLocation))
      {
        myPlayer.SoundLocation = soundLocation;
        if (typeSoundSemaforoRojo == 0)
          myPlayer.Play();
        else
          myPlayer.PlayLooping();
      }
      System.Threading.Thread.Sleep(timeSemaforoRojo);
      myPlayer.Stop();
      SetSemaforoLuzRojo((byte)htDefaultBits[Define.SEMAFORO_ROJO]);
    }

    public void MolineteDiscapacitadosDenegadoTransito()
    {
        SetSemaforoLuzAmbar(~(byte)htDefaultBits[Define.SEMAFORO_AMBAR] & 0x01);
        String soundLocation = Util.ApplicationPath + soundFileSemaforoAmbar2;
        if (File.Exists(soundLocation))
        {
            myPlayer.SoundLocation = soundLocation;
            if (typeSoundSemaforoAmbar2 == 0)
                myPlayer.Play();
            else
                myPlayer.PlayLooping();
        }
        System.Threading.Thread.Sleep(timeSemaforoAmbar2);
        myPlayer.Stop();
        SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]);
    }






  }


}