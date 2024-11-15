using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.AccesoMolinete;

namespace DemoAccesoMolinete
{
    public partial class OptionForm : Form
    {
        public OptionForm()
        {
            InitializeComponent();
        }

        private void btnMolineteNormal_Click(object sender, EventArgs e)
        {
            Molinete molinete = new Molinete();
            molinete.OpenDriverMolinetes();
            Escenario escenario = new Escenario(molinete);
            PLC plc = new PLC(escenario);
            plc.Iniciar();
            escenario.setPLC(plc);
            escenario.ShowDialog(this);
            //Application.Run(escenario);
            //this.Close();
        }

        private void btnMolineteDiscapacitados_Click(object sender, EventArgs e)
        {
            MolineteDiscapacitados molineteDiscapacitados = new MolineteDiscapacitados();
            molineteDiscapacitados.OpenDriverMolinetes();
            EscenarioDiscapacitados escenarioDiscapacitados = new EscenarioDiscapacitados(molineteDiscapacitados);
            PLCDiscapacitados plcDiscapacitados = new PLCDiscapacitados(escenarioDiscapacitados);
            plcDiscapacitados.Iniciar();
            escenarioDiscapacitados.setPLCDiscapacitados(plcDiscapacitados);
            escenarioDiscapacitados.ShowDialog(this);
            //Application.Run(escenarioDiscapacitados);
            //this.Close();
        }
    }
}
