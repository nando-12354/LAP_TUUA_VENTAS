using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using LAP.TUUA.AccesoMolinete;

namespace DemoAccesoMolinete
{
    public partial class EscenarioDiscapacitados : Form
    {

        private MolineteDiscapacitados molineteDiscapacitados;
        private PLCDiscapacitados plcDiscapacitados;

        private System.Windows.Forms.Timer paletaTimer;

        public EscenarioDiscapacitados(MolineteDiscapacitados molineteDiscapacitados)
        {
            InitializeComponent();
            btnPasarTicketNormalOk.Enabled = false;
            btnPasarTicketEspecialOk.Enabled = false;
            btnPasarTicketNoOk.Enabled = false;
            this.molineteDiscapacitados = molineteDiscapacitados;

            paletaTimer = new System.Windows.Forms.Timer();
            paletaTimer.Interval = 250;
            paletaTimer.Tick += new System.EventHandler(this.paletaTimer_Tick);
            PaletaProgressBar.Maximum = 144;
            PaletaProgressBar.Value = 0;
        }

        public void setPLCDiscapacitados(PLCDiscapacitados _plcDiscapacitados)
        {
            this.plcDiscapacitados = _plcDiscapacitados;
        }

        private void btnPasarTicketNormalOk_Click(object sender, EventArgs e)
        {
            btnPasarTicketNormalOk.Enabled = false;
            btnPasarTicketEspecialOk.Enabled = false;
            btnPasarTicketNoOk.Enabled = false;
            new Thread(new ThreadStart(runPasarTicketNormalOk)).Start();
        }

        private void btnPasarTicketEspecialOk_Click(object sender, EventArgs e)
        {
            btnPasarTicketNormalOk.Enabled = false;
            btnPasarTicketEspecialOk.Enabled = false;
            btnPasarTicketNoOk.Enabled = false;
            new Thread(new ThreadStart(runPasarTicketEspecialOk)).Start();
        }

        private void btnPasarTicketNoOk_Click(object sender, EventArgs e)
        {
            btnPasarTicketNormalOk.Enabled = false;
            btnPasarTicketEspecialOk.Enabled = false;
            btnPasarTicketNoOk.Enabled = false;
            new Thread(new ThreadStart(runPasarTicketNoOk)).Start();
        }

        public void runPasarTicketNormalOk()
        {
            molineteDiscapacitados.MolineteDiscapacitadosAprobadoNormal();
            Thread.Sleep(3500);
            SetEnableButtons();
        }

        public void runPasarTicketEspecialOk()
        {
            molineteDiscapacitados.MolineteDiscapacitadosAprobadoEspecial();
            Thread.Sleep(3500);
            SetEnableButtons();
        }

        public void runPasarTicketNoOk()
        {
            molineteDiscapacitados.MolineteDiscapacitadosDenegado();
            SetEnableButtons();
        }

        public delegate void CHSMHandler(int status);

        public void SetChangeStatusMolinete(int status)
        {
            if (this.InvokeRequired)
                this.Invoke(new CHSMHandler(ChangeStatusMolinete), new object[] { status });
            else
                ChangeStatusMolinete(status);
        }

        public void ChangeStatusMolinete(int status)
        {
            switch (status)
            {
                case 0:
                    lblStatusMolinete.ForeColor = Color.Red;
                    lblStatusMolinete.Text = Define.TEXTOCLOSED;
                    break;
                case 1:
                    lblStatusMolinete.ForeColor = Color.MediumBlue;
                    lblStatusMolinete.Text = Define.TEXTOOPENED;
                    break;
            }

        }

        public delegate void EBHandler();

        public void SetEnableButtons()
        {
            if (this.InvokeRequired)
                this.Invoke(new EBHandler(EnableButtons), new object[] { });
            else
                EnableButtons();
        }

        private void EnableButtons()
        {
            btnPasarTicketNormalOk.Enabled = true;
            btnPasarTicketEspecialOk.Enabled = true;
            btnPasarTicketNoOk.Enabled = true;
        }

        public delegate void MSHandler(int color, int status);

        public void SetMostrarSemaforo(int color, int status)
        {
            if (this.InvokeRequired)
                this.Invoke(new MSHandler(MostrarSemaforo), new object[] { color, status });
            else
                MostrarSemaforo(color, status);
        }

        private void MostrarSemaforo(int color, int status)
        {
            switch (color)
            {
                case 0://rojo
                    if (status == (byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_ROJO])
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    }
                    else if (status == (~(byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_ROJO] & 0x01))
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/rojo.png";
                    }
                    //else
                    //{
                    //  picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    //}
                    break;
                case 1://ambar
                    if (status == (byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_AMBAR])
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    }
                    else if (status == (~(byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_AMBAR] & 0x01))
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/ambar.png";
                    }
                    //else
                    //{
                    //  picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    //}
                    break;
                case 2://verde
                    if (status == (byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_VERDE])
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    }
                    else if (status == (~(byte)plcDiscapacitados.HtDefaultBits[Define.SEMAFORO_VERDE] & 0x01))
                    {
                        picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/verde.png";
                    }
                    //else
                    //{
                    //  picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
                    //}
                    break;

            }
        }

        public delegate void EPTHandler();

        public void SetEnablePaletaTimer()
        {
            if (this.InvokeRequired)
                this.Invoke(new EPTHandler(EnablePaletaTimer), new object[] {});
            else
                EnablePaletaTimer();
        }

        private void EnablePaletaTimer()
        {
            paletaTimer.Enabled = true;
        }

        private void paletaTimer_Tick(object sender, EventArgs e)
        {
            int getOpenMolinete = plcDiscapacitados.GetOpenMolinete();
            if (getOpenMolinete == (~(byte)plcDiscapacitados.HtDefaultBits[Define.OPEN_MOLINETE] & 0x01))
            {

                if (this.PaletaProgressBar.Value == 144)
                {
                    paletaTimer.Enabled = false;
                    //this.PaletaProgressBar.Value = 0;
                    return;                        
                }
                this.PaletaProgressBar.Value += 12;
            }
            else if (getOpenMolinete == (byte)plcDiscapacitados.HtDefaultBits[Define.OPEN_MOLINETE])
            {
                if (this.PaletaProgressBar.Value == 0)
                {
                    paletaTimer.Enabled = false;
                    //this.PaletaProgressBar.Value = 0;
                    return;
                }
                this.PaletaProgressBar.Value -= 12;                
            }
        }

        private void EscenarioDiscapacitados_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
