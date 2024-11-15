using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.AccesoMolinete;
using System.Threading;

namespace DemoAccesoMolinete
{
  public partial class Escenario : Form
  {
    private System.Windows.Forms.Timer counterTimer;
    private Molinete molinete;
    private PLC plc;
    public Escenario(Molinete molinete)
    {
      InitializeComponent();
      btnPasarTicketNormalOk.Enabled = false;
      btnPasarTicketEspecialOk.Enabled = false;
      btnPasarTicketNoOk.Enabled = false;
      btnIngresaPasajero.Enabled = false;
      counterTimer = new System.Windows.Forms.Timer();
      counterTimer.Tick += new System.EventHandler(this.counterTimer_Tick);
      this.molinete = molinete;
    }

    public void setPLC(PLC _plc)
    {
      this.plc = _plc;
    }

    private void btnPasarTicketNormalOk_Click(object sender, EventArgs e)
    {
      btnPasarTicketNormalOk.Enabled = false;
      btnPasarTicketEspecialOk.Enabled = false;
      btnPasarTicketNoOk.Enabled = false;
      new Thread(new ThreadStart(runPasarTicketNormalOk)).Start();
      btnIngresaPasajero.Enabled = true;
    }

    private void btnPasarTicketEspecialOk_Click(object sender, EventArgs e)
    {
      btnPasarTicketNormalOk.Enabled = false;
      btnPasarTicketEspecialOk.Enabled = false;
      btnPasarTicketNoOk.Enabled = false;
      new Thread(new ThreadStart(runPasarTicketEspecialOk)).Start();
      btnIngresaPasajero.Enabled = true;
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
      molinete.MolineteAprobadoNormal();
      SetEnableButtons();
    }

    public void runPasarTicketEspecialOk()
    {
      molinete.MolineteAprobadoEspecial();
      SetEnableButtons();
    }

    public void runPasarTicketNoOk()
    {
      molinete.MolineteDenegado();
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
          btnIngresaPasajero.Enabled = false;
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
        this.Invoke(new MSHandler(MostrarSemaforo), new object[] {color, status});
      else
        MostrarSemaforo(color, status);
    }

    private void MostrarSemaforo(int color, int status)
    {
      switch (color)
      {
        case 0://rojo
          if (status == (byte)plc.HtDefaultBits[Define.SEMAFORO_ROJO])
          {
            picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
          }
          else if (status == (~(byte)plc.HtDefaultBits[Define.SEMAFORO_ROJO] & 0x01))
          {
            picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/rojo.png";
          }
          //else
          //{
          //  picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
          //}
          break;
        case 1://ambar
          if (status == (byte)plc.HtDefaultBits[Define.SEMAFORO_AMBAR])
          {
            picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
          }
          else if (status == (~(byte)plc.HtDefaultBits[Define.SEMAFORO_AMBAR] & 0x01))
          {
            picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/ambar.png";
          }
          //else
          //{
          //  picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
          //}
          break;
        case 2://verde
          if (status == (byte)plc.HtDefaultBits[Define.SEMAFORO_VERDE])
          {
            picSemaforo.ImageLocation = AppDomain.CurrentDomain.BaseDirectory + "/resources/blanco.png";
          }
          else if (status == (~(byte)plc.HtDefaultBits[Define.SEMAFORO_VERDE] & 0x01))
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

    

    private void btnIngresaPasajero_Click(object sender, EventArgs e)
    {
      btnIngresaPasajero.Enabled = false;
      this.CounterProgressBar.Value = 0;
      counterTimer.Interval = 250;
      counterTimer.Enabled = true;
    }

    private void counterTimer_Tick(object sender, EventArgs e)
    {
      if (plc.GetCloseMolinete() == (~(byte)plc.HtDefaultBits[Define.CLOSE_MOLINETE] & 0x01))
      {
        counterTimer.Enabled = false;
        this.CounterProgressBar.Value = 0;
        return;
      }
      if (this.CounterProgressBar.Value == 100)
      {
        counterTimer.Enabled = false;
        this.CounterProgressBar.Value = 0;
        //plc.MolineteGirado();//Pulsar por 500 ms y luego dejar de pulsar el pulsador respectivo al Puerto A - Bit 0. //Comentarlo, Si se desea con el circiuto de forma completa.
        MessageBox.Show("Ingresó el Pasajero");
      }
      else
      {
        this.CounterProgressBar.Value += 10;
      }

    }

    private void Escenario_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }



  }
}
