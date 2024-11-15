using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.VENTAS
{
    public partial class Calculo : Form
    {
        protected int Tip_CompraVenta;
        protected decimal Imp_Monto;
        protected string Desc_Moneda;

        public Calculo(string strTipo,string strMoneda, string strTasaCambio,int intTipo)
        {
            InitializeComponent();
            lblTCVM.Text = strTipo;
            lblTC.Text = strTasaCambio;
            lblVerMoneda.Text = strMoneda;
            Tip_CompraVenta = intTipo;
            Desc_Moneda = strMoneda;
            if (Tip_CompraVenta == 0)
            {
                this.lblACambiar.Text = "Monto A Cambiar(SOL)";
                this.lblCambiado.Text = "Monto Cambiado(" + Desc_Moneda + ")";
            }
            else
            {
                this.lblACambiar.Text = "Monto A Cambiar(" + Desc_Moneda + ")";
                this.lblCambiado.Text = "Monto Cambiado(SOL)";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (Valido())
            {
                decimal decCambiado;
                if (Tip_CompraVenta == 0)
                {
                    this.lblACambiar.Text = "Monto A Cambiar(SOL)";
                    this.lblCambiado.Text = "Monto Cambiado(" + Desc_Moneda + ")";
                    decCambiado = Function.FormatDecimal(Imp_Monto/decimal.Parse(lblTC.Text), 2);
                    this.txtCambiado.Text = decCambiado.ToString();
                }
                else
                {
                    this.lblACambiar.Text = "Monto A Cambiar(" + Desc_Moneda + ")";
                    this.lblCambiado.Text = "Monto Cambiado(SOL)";
                    decCambiado =Function.FormatDecimal(Imp_Monto * decimal.Parse(lblTC.Text),2);
                    this.txtCambiado.Text = decCambiado.ToString();
                }
            }
        }

        private bool Valido()
        {
            if (txtCambiar.Text == "")
            {
                this.erpMonto.SetError(txtCambiar,"Ingrese un monto válido.");
                return false;
            }
            try
            {
               Imp_Monto= decimal.Parse(txtCambiar.Text);
            }
            catch (Exception e)
            {
                this.erpMonto.SetError(txtCambiar, "Ingrese un monto válido.");
                return false;
            }
            return true;
        }

        private void Calculo_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }

        private void txtCambiar_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Decimal))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}