using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using System.Collections;

namespace LAP.TUUA.VENTAS
{
    partial class Efectivo : Form
    {
        protected Venta frmVenta;
        protected int Flg_Venta;
        protected decimal Imp_Monto;
        protected decimal Imp_TotPagxMoneda;
        protected string Cod_Moneda;
        protected TasaCambio objTasaCambio;
        protected BO_Operacion objBOOpera;
        protected Moneda objMoneda;
        protected List<Cambio> Lista_Cambio;
        protected int Can_Tickets;
        protected TipoTicket objTipoTicket;
        private string Cod_Moneda_Sol;
        private Hashtable Lista_Tasas;
        private decimal Imp_IngresoInt;
        private decimal Imp_IngresoNac;
        private decimal Imp_EgresoNac;

        public Efectivo(Venta frmVenta, List<Moneda> listaMonedas, string strMoneda, decimal decMonto, int intFlg)
        {
            InitializeComponent();
            this.frmVenta = frmVenta;
            Cod_Moneda_Sol = Property.htProperty[Define.MONEDANAC].ToString();
            Lista_Cambio = new List<Cambio>();
            Lista_Tasas = new Hashtable();

            this.objTasaCambio = frmVenta.objTasaCambio;
            this.objBOOpera = frmVenta.objBOOpera;
            this.Can_Tickets = frmVenta.Can_Tickets;
            this.objTipoTicket = frmVenta.objTipoTicket;

            cbxMoneda.DataSource = listaMonedas;
            Cod_Moneda = strMoneda;
            Imp_Monto = decMonto;
            Imp_TotPagxMoneda = decMonto;
            CargarDatosPago(listaMonedas, strMoneda, decMonto);
            txtMonto.Text = Function.FormatDecimal(Imp_Monto,Define.NUM_DECIMAL).ToString();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            //frmVenta.ActualizarVuelto();
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!EsValido())
            {
                return;
            }
            IdentificarConversion();

            frmVenta.CargarLineaResumen(Define.TIP_PAGO_EFECTIVO, ((Moneda)cbxMoneda.SelectedItem).SCodMoneda, Imp_Monto, 2);
            frmVenta.Imp_Dialogo_Pagar = Imp_TotPagxMoneda;
            frmVenta.Imp_Dialogo_Pagado = Imp_Monto;
            frmVenta.Lista_Dialogo_Cambio = Lista_Cambio;
            frmVenta.Lista_Dialogo_Tasa = Lista_Tasas;
            frmVenta.Cod_Moneda_Dialogo = objMoneda.SCodMoneda;
            frmVenta.ConvertirTarjetaContado();
            Close();
        }

        protected bool EsValido()
        {
            if (txtMonto.Text == "")
            {
                this.erpMonto.SetError(txtMonto, "Ingrese monto válido");
                return false;
            }
            try
            {
                Imp_Monto = decimal.Parse(txtMonto.Text);
            }
            catch
            {
                this.erpMonto.SetError(txtMonto, "Ingrese monto válido.");
                return false;
            }
            return true;
        }

        public void VerificarKeys(KeyEventArgs e)
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

        private void txtMonto_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void CargarDatosPago(List<Moneda> listaMonedas, string strMoneda, decimal decMonto)
        {
            for (int i = 0; i < listaMonedas.Count; i++)
            {
                if (listaMonedas[i].SCodMoneda == strMoneda)
                {
                    objMoneda = listaMonedas[i];
                    lblSimbAPagar.Text = listaMonedas[i].SDscSimbolo;
                    cbxMoneda.SelectedIndex = i;
                    lblImpApagar.Text = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    break;
                }
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            decimal decMonto = 0;
            decimal decTasaCom = 0;
            decimal decTasaVen = 0;
            try
            {
                decMonto = decimal.Parse(txtMonto.Text);
                decTasaCom = objBOOpera.ObtenerTasaCambioPorMoneda(Cod_Moneda, Define.TC_COMPRA).DImpCambioActual;
                decTasaVen = objBOOpera.ObtenerTasaCambioPorMoneda(Cod_Moneda, Define.TC_VENTA).DImpCambioActual;
            }
            catch
            {
                decMonto = 0;
            }
            frmVenta.SetearCheckRadioButtonVuelto(Define.centavos.TODO, objMoneda);
            frmVenta.MostrarVueltoCentavos(decMonto - Imp_TotPagxMoneda, decTasaCom, decTasaVen, Define.centavos.TODO);
        }

        private void cbxMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            objMoneda = (Moneda)this.cbxMoneda.SelectedItem;
            frmVenta.Centavos(objMoneda.SCodMoneda);
            ActualizarMontoAPagar();
        }

        private void ActualizarMontoAPagar()
        {
            try
            {
                TasaCambio objTC = null;
                if (objMoneda.SCodMoneda == Cod_Moneda)
                {
                    Imp_TotPagxMoneda = Function.FormatDecimal(Imp_Monto,Define.NUM_DECIMAL);
                    txtMonto.Text = Imp_TotPagxMoneda.ToString();
                    return;
                }

                if (objMoneda.SCodMoneda == Cod_Moneda_Sol)
                {
                    objTC = this.objBOOpera.ObtenerTasaCambioPorMoneda(Cod_Moneda, Define.TC_VENTA);
                    Imp_TotPagxMoneda = Function.FormatDecimal(Imp_Monto * objTC.DImpCambioActual, Define.NUM_DECIMAL);
                    txtMonto.Text = Imp_TotPagxMoneda.ToString();
                    return;
                }

                objTC = this.objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, objMoneda.SCodMoneda != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                Imp_TotPagxMoneda = Function.FormatDecimal(Imp_Monto * this.objTasaCambio.DImpCambioActual / objTC.DImpCambioActual, Define.NUM_DECIMAL);
                txtMonto.Text = Imp_TotPagxMoneda.ToString();
            }
            catch { }
        }

        private decimal LeerMontoIngresado()
        {
            decimal decMonto;
            try
            {
                decMonto = decimal.Parse(txtMonto.Text);
            }
            catch
            {
                decMonto = 0;
            }
            return decMonto;
        }

        private void IdentificarConversion()
        {
            TasaCambio objTCPagado;
            //Importes de compra-venta
            decimal decImpNac = 0;
            decimal decImpInt = 0;

            //objCambioSol = null;
            Imp_IngresoInt = 0;
            Imp_IngresoNac = 0;
            Lista_Cambio = new List<Cambio>();
            Lista_Tasas = new Hashtable();

            if (objMoneda.SCodMoneda != objTipoTicket.SCodMoneda)
            {
                if (objMoneda.SCodMoneda == Cod_Moneda_Sol)
                {
                    objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA);
                    Lista_Tasas.Remove(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA);
                    Lista_Tasas.Add(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA, objTCPagado.DImpCambioActual);
                    decImpNac = Function.FormatDecimal(Imp_TotPagxMoneda, Define.NUM_DECIMAL);
                    decImpInt = Function.FormatDecimal(Imp_TotPagxMoneda / objTCPagado.DImpCambioActual, Define.NUM_DECIMAL);
                    AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, objTCPagado.DImpCambioActual, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac);
                }
                else
                {
                    if (objTipoTicket.SCodMoneda == Cod_Moneda_Sol)
                    {
                        objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, Define.TC_COMPRA);
                        Lista_Tasas.Remove(objMoneda.SCodMoneda + "#" + Define.TC_COMPRA);
                        Lista_Tasas.Add(objMoneda.SCodMoneda + "#" + Define.TC_COMPRA, objTCPagado.DImpCambioActual);
                        decImpNac = Function.FormatDecimal(Imp_TotPagxMoneda * objTCPagado.DImpCambioActual, Define.NUM_DECIMAL);
                        decImpInt = Function.FormatDecimal(Imp_TotPagxMoneda, Define.NUM_DECIMAL);
                        AgregarCambio(objMoneda.SCodMoneda, Define.COMPRA_MONEDA, objMoneda.SDscMoneda, objTCPagado.DImpCambioActual, objMoneda.SDscSimbolo, decImpInt, decImpNac);
                    }
                    else
                    {
                        objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, Define.TC_COMPRA);
                        Lista_Tasas.Remove(objMoneda.SCodMoneda + "#" + Define.TC_COMPRA);
                        Lista_Tasas.Add(objMoneda.SCodMoneda + "#" + Define.TC_COMPRA, objTCPagado.DImpCambioActual);
                        decImpNac = Function.FormatDecimal(Imp_TotPagxMoneda * objTCPagado.DImpCambioActual, Define.NUM_DECIMAL);
                        AgregarCambio(objMoneda.SCodMoneda, Define.COMPRA_MONEDA, objMoneda.SDscMoneda, objTCPagado.DImpCambioActual, objMoneda.SDscSimbolo, Imp_TotPagxMoneda, decImpNac);
                        objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA);
                        Lista_Tasas.Remove(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA);
                        Lista_Tasas.Add(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA, objTCPagado.DImpCambioActual);
                        decImpInt = Function.FormatDecimal(decImpNac / objTCPagado.DImpCambioActual, Define.NUM_DECIMAL);
                        AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, objTCPagado.DImpCambioActual, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac);
                    }
                }
            }
            Imp_IngresoInt = Imp_TotPagxMoneda;
        }

        private void AgregarCambio(string strMonPago, string strTipCambio, string strDscMonInt, decimal decTasa, string strSimbInt, decimal decImpInt, decimal decImpNac)
        {
            Cambio objCambio = new Cambio();
            objCambio.Cod_Moneda = strMonPago;
            objCambio.Tip_Cambio = strTipCambio;
            objCambio.Dsc_MonedaInt = strDscMonInt;
            objCambio.Tip_Pago = Define.TIP_PAGO_EFECTIVO;
            objCambio.Imp_TasaCambio = decTasa;
            objCambio.Dsc_SimboloInt = strSimbInt;
            objCambio.Imp_MontoInt = Function.FormatDecimal(decImpInt * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
            objCambio.Imp_MontoNac = Function.FormatDecimal(decImpNac * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
            Lista_Cambio.Add(objCambio);
        }
        private void ValidarTextBoxImporte(TextBox tbxObject)
        {
            try
            {
                decimal.Parse(tbxObject.Text);
            }
            catch
            {
                tbxObject.Text = "";
            }
        }
    }
}
