using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Timer = System.Timers.Timer;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.AccesoMolinete
{
    public class Molinete
    {
        private cConfigXml oAppConfig;

        private Hashtable htConfigWriters = new Hashtable();
        private Hashtable htConfigReaders = new Hashtable();

        private Hashtable htDefaultBits = new Hashtable();

        private int timeSemaforoRojo;
        private int timeoutSemaforoAmbar;
        private int timeoutSemaforoVerde;
        private int timeoutSemaforoAmbar2;

        private String soundFileSemaforoRojo;
        private String soundFileSemaforoAmbar;
        private String soundFileSemaforoVerde;
        private String soundFileSemaforoAmbar2;

        private int typeSoundSemaforoRojo;
        private int typeSoundSemaforoAmbar;
        private int typeSoundSemaforoVerde;
        private int typeSoundSemaforoAmbar2;

        private bool forcedClosedMolinete;

        private System.Media.SoundPlayer myPlayer = new SoundPlayer();

        public Molinete()
        {
            try
            {
                String sCnfgFile = Util.ApplicationPath + Define.XML_CONFIG;
                oAppConfig = new cConfigXml(sCnfgFile);
                lecturaPuertos();
                lecturaTiempos();
                lecturaSonidos();
                setDefaultBits();
                //OpenDriverMolinetes();
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

        public Molinete(int timeSemaforoRojo, int timeoutSemaforoAmbar, int timeoutSemaforoVerde)
            : this(timeSemaforoRojo, timeoutSemaforoAmbar, timeoutSemaforoVerde, null, null, null)
        {
        }

        public Molinete(String soundFileSemaforoRojo, String soundFileSemaforoAmbar, String soundFileSemaforoVerde)
            : this(0, 0, 0, soundFileSemaforoRojo, soundFileSemaforoAmbar, soundFileSemaforoVerde)
        {
        }

        public Molinete(int timeSemaforoRojo, int timeoutSemaforoAmbar, int timeoutSemaforoVerde,
            String soundFileSemaforoRojo, String soundFileSemaforoAmbar, String soundFileSemaforoVerde)
            : this(timeSemaforoRojo, timeoutSemaforoAmbar, timeoutSemaforoVerde, soundFileSemaforoRojo, soundFileSemaforoAmbar, soundFileSemaforoVerde, 0, 0, 0)
        {
        }

        public Molinete(int timeSemaforoRojo, int timeoutSemaforoAmbar, int timeoutSemaforoVerde,
            String soundFileSemaforoRojo, String soundFileSemaforoAmbar, String soundFileSemaforoVerde,
            int typeSoundSemaforoRojo, int typeSoundSemaforoAmbar, int typeSoundSemaforoVerde)
        {
            try
            {
                String sCnfgFile = Util.ApplicationPath + Define.XML_CONFIG;
                oAppConfig = new cConfigXml(sCnfgFile);
                lecturaPuertos();
                setDefaultBits();

                this.timeSemaforoRojo = timeSemaforoRojo;
                this.timeoutSemaforoAmbar = timeoutSemaforoAmbar;
                this.timeoutSemaforoVerde = timeoutSemaforoVerde;

                this.soundFileSemaforoRojo = soundFileSemaforoRojo;
                this.soundFileSemaforoAmbar = soundFileSemaforoRojo;
                this.soundFileSemaforoVerde = soundFileSemaforoRojo;

                this.typeSoundSemaforoRojo = typeSoundSemaforoRojo;
                this.typeSoundSemaforoAmbar = typeSoundSemaforoAmbar;
                this.typeSoundSemaforoVerde = typeSoundSemaforoVerde;

                //OpenDriverMolinetes();

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
            timeoutSemaforoAmbar = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "Timeout")));
            timeoutSemaforoVerde = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_VERDE, "Timeout")));
            timeoutSemaforoAmbar2 = (int.Parse(oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "Timeout")));
        }

        private void lecturaSonidos()
        {
            soundFileSemaforoRojo = oAppConfig.GetValue(Define.SEMAFORO_ROJO, "SoundFile");
            soundFileSemaforoAmbar = oAppConfig.GetValue(Define.SEMAFORO_AMBAR, "SoundFile");
            soundFileSemaforoVerde = oAppConfig.GetValue(Define.SEMAFORO_VERDE, "SoundFile");
            soundFileSemaforoAmbar = oAppConfig.GetValue(Define.SEMAFORO_AMBAR2, "SoundFile");

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
            if (SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]) != 1)
            {
                ret = false;
            }
            if (SetSemaforoLuzRojo((byte)htDefaultBits[Define.SEMAFORO_ROJO]) != 1)
            {
                ret = false;
            }
            if (SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]) != 1)
            {
                ret = false;
            }
            if (SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE]) != 1)
            {
                ret = false;
            }
            if (SetCloseMolinete((byte)htDefaultBits[Define.CLOSE_MOLINETE]) != 1)
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

        private int SetCloseMolinete(int accion)
        {
            String[] sCnfg = ((String)htConfigWriters[Define.CLOSE_MOLINETE]).Split(':');
            int iPort = int.Parse(sCnfg[0]);
            int iBit = int.Parse(sCnfg[1]);
            return Util.WritePuerto(iPort, iBit, accion);
        }

        private int GetGiroMolinete()
        {
            String[] sCnfg = ((String)htConfigReaders[Define.GIRO_MOLINETE]).Split(':');
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

        public void setDefaultOpenMolinete()
        {
            Thread.Sleep(500);
            SetOpenMolinete((byte)htDefaultBits[Define.OPEN_MOLINETE]);
        }

        public bool MolineteAprobadoNormal()
        {
            try
            {
                int intRetorno;
                Monitor.Enter(this);
                forcedClosedMolinete = false;
                SetOpenMolinete(~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01);
                
                new Thread(setDefaultOpenMolinete).Start();

                intRetorno = SetSemaforoLuzVerde(~(byte)htDefaultBits[Define.SEMAFORO_VERDE] & 0x01);

                String soundLocation = Util.ApplicationPath + soundFileSemaforoVerde;//no hay problema si soundFileSemaforoVerde=null
                if (File.Exists(soundLocation))
                {
                    myPlayer.SoundLocation = soundLocation;
                    if (typeSoundSemaforoVerde == 0)
                        myPlayer.Play();
                    else
                        myPlayer.PlayLooping();
                }

                Timer timerSemaforoVerde = new Timer();
                if (timeoutSemaforoVerde > 0)
                {
                    timerSemaforoVerde.AutoReset = false;
                    timerSemaforoVerde.Interval = timeoutSemaforoVerde;
                    timerSemaforoVerde.Elapsed += new System.Timers.ElapsedEventHandler(timer_SemaforoVerdeAmbar_Elapsed);
                    timerSemaforoVerde.Start();
                }

                String[] sCnfg = ((String)htConfigReaders[Define.GIRO_MOLINETE]).Split(':');
                int iPort = int.Parse(sCnfg[0]);
                int iBit = int.Parse(sCnfg[1]);
                int defaultGiroMolinete = (byte)htDefaultBits[Define.GIRO_MOLINETE];
                int pasajeroPasoElMolinete;
                int intSleep = Int32.Parse((string)Property.htProperty["SLEEP_MOLINETE"]);
                int intSleepGiro = Int32.Parse((string)Property.htProperty["SLEEP_GIRO"]);
                int intTotSleep = 0;
                Thread.Sleep(intSleep);
                while ((pasajeroPasoElMolinete = Util.ReadPuerto(iPort, iBit)) == defaultGiroMolinete && !forcedClosedMolinete)
                {
                    intTotSleep = +intSleep;
                    Thread.Sleep(intSleep);
                }
                //jcisneros
                if (intTotSleep <= intSleep && intSleepGiro > intSleep)
                {
                    Thread.Sleep(intSleepGiro - intTotSleep);
                }
                //jcisneros
                timerSemaforoVerde.Enabled = false;
                //if (!forcedClosedMolinete)
                //{
                //  timerSemaforoVerde.Enabled = false;
                //}

                //if(forcedClosedMolinete)
                //{
                //    Thread.Sleep(250);
                //}

                myPlayer.Stop();
                //JAC
                intRetorno = SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE]);
                //JAC
                //if (pasajeroPasoElMolinete == defaultGiroMolinete)
                //{
                //  return false;
                //}else
                //{
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Monitor.Exit(this);
            }
            return true;
            //}
            //JAC
        }

        private void timer_SemaforoVerdeAmbar_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int intSleep = Int32.Parse((string)Property.htProperty["SLEEP_SEMAFORO"]);
            SetCloseMolinete(~(byte)htDefaultBits[Define.CLOSE_MOLINETE] & 0x01);
            //forcedClosedMolinete = true;  //Anteriormente aqui
            Thread.Sleep(intSleep);
            forcedClosedMolinete = true;    //Ahora aqui, para asegurar que salga del bucle por el forceClosedMolinete, y que el pasajero no haya ingresado en el interin de mandar la senal de forzar cerrar.
            SetCloseMolinete((byte)htDefaultBits[Define.CLOSE_MOLINETE]);
            //int intRetorno = SetSemaforoLuzVerde((byte)htDefaultBits[Define.SEMAFORO_VERDE]);
            //ErrorHandler.EscribirLog(this, "APAGAR VERDE " + intRetorno.ToString());
        }

        public bool MolineteAprobadoEspecial()
        {
            forcedClosedMolinete = false;
            SetOpenMolinete(~(byte)htDefaultBits[Define.OPEN_MOLINETE] & 0x01);

            new Thread(setDefaultOpenMolinete).Start();

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

            Timer timerSemaforoAmbar = new Timer();
            if (timeoutSemaforoAmbar > 0)
            {
                timerSemaforoAmbar.AutoReset = false;
                timerSemaforoAmbar.Interval = timeoutSemaforoAmbar;
                timerSemaforoAmbar.Elapsed += new System.Timers.ElapsedEventHandler(timer_SemaforoVerdeAmbar_Elapsed);
                timerSemaforoAmbar.Start();
            }

            String[] sCnfg = ((String)htConfigReaders[Define.GIRO_MOLINETE]).Split(':');
            int iPort = int.Parse(sCnfg[0]);
            int iBit = int.Parse(sCnfg[1]);
            int defaultGiroMolinete = (byte)htDefaultBits[Define.GIRO_MOLINETE];
            int pasajeroPasoElMolinete;
            while ((pasajeroPasoElMolinete = Util.ReadPuerto(iPort, iBit)) == defaultGiroMolinete && !forcedClosedMolinete)
            {
                Thread.Sleep(250);
            }

            timerSemaforoAmbar.Enabled = false;

            //if (forcedClosedMolinete)
            //{
            //    Thread.Sleep(250);
            //}

            myPlayer.Stop();

            SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]);

            if (pasajeroPasoElMolinete == defaultGiroMolinete)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void MolineteDenegado()
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

        public void MolineteDenegadoPasajeroTransito()
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
            System.Threading.Thread.Sleep(timeoutSemaforoAmbar2);
            myPlayer.Stop();            
            SetSemaforoLuzAmbar((byte)htDefaultBits[Define.SEMAFORO_AMBAR]);
        }






    }


}