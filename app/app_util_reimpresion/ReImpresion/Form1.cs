using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Control;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;

namespace REImpresion
{
    public partial class frmREImpresion : Form
    {
        private Print impresion;
        private BO_Consultas objBOConsultas;
        private XmlDocument xml;
        private Hashtable listaParamConfig;
        private Hashtable listaParamImp;

        private String Lista_Tickets;
        private int Can_Tickets;
        private String Fec_Vencimiento;
        private String Dsc_Moneda;
        private decimal DImpPrecio;

        private String LONG_TICKET = "16";

        public frmREImpresion()
        {
            InitializeComponent();
            calendar.Visible = false;
            
            impresion = new Print();
            objBOConsultas = new BO_Consultas();
            xml = new XmlDocument();
            listaParamConfig = new Hashtable();
            listaParamImp = new Hashtable();

            objBOConsultas.ObtenerParametrosImpresion(listaParamConfig, xml);

        }

        private void txtCodigoTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("e.KeyChar: " + (int)e.KeyChar);
            if (e.KeyChar < (char)Keys.D0 || e.KeyChar > (char)Keys.D9)
            {
                if (e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back)
                {
                    Console.WriteLine("e.Handled = true;");
                    e.Handled = true;
                }
                if (e.KeyChar == (char)Keys.Enter )
                {
                    btnAgregar.PerformClick();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtCodigoTicket.Text.Length != 16)
            {
                txtCodigoTicket.Focus();
                return;
            }
            gvTickets.Rows.Add(txtCodigoTicket.Text);
            txtCodigoTicket.Text = "";
            txtCodigoTicket.Focus();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if(txtImporte.Text.Trim().Equals("") || txtDescMoneda.Text.Trim().Equals("") || txtFechaVencimiento.Text.Trim().Equals("") || gvTickets.Rows.Count <2)
            {
                MessageBox.Show("Complete los campos requeridos", "Informacion");
                return;
            }
            Dsc_Moneda = txtDescMoneda.Text;
            try
            {
                DImpPrecio = Decimal.Parse(txtImporte.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ingrese un importe correcto", "Informacion");
                return;
            }

            Lista_Tickets = "";
            for(int i=0;i<gvTickets.Rows.Count-1;i++)
            {
                Lista_Tickets = Lista_Tickets + gvTickets.Rows[i].Cells[0].Value;
            }
            Can_Tickets = gvTickets.Rows.Count-1;
            Fec_Vencimiento = txtFechaVencimiento.Text;

            try
            {
                String nombre = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKER;
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);
                // obtiene la configuracion de la impresora a utilizar
                String configImpSticker = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoSticker != null && !Property.puertoSticker.Trim().Equals(String.Empty))
                {
                    configImpSticker = Property.puertoSticker.Trim() + "," +
                                       configImpSticker.Split(new char[] {','}, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion();

                // obtiene la data a imprimir con la impresora de sticker y guardarla en una variable
                String dataSticker = impresion.ObtenerDataFormateada(listaParamImp, nodo);

                String[] parametros = new String[]
                                          {
                                              "configImpSticker=" + configImpSticker,
                                              "dataSticker=" + dataSticker,
                                              "listaCodigoTickets=" + Lista_Tickets
                                          };

                frmPrintNet frmPrintNet = new frmPrintNet(parametros, listaParamImp);
                frmPrintNet.ShowDialog(this);

                String resultado = frmPrintNet.Resultado;

                Console.WriteLine("Resultado de Impresion: " + resultado);

                if (resultado.Contains("Impresion"))
                {
                    Property.puertoSticker = frmPrintNet.PuertoSerial.getConfigImpSticker()[0];
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error no controlado: " + ex.Message, "Informacion");
            }
            
            gvTickets.Rows.Clear();
            txtCodigoTicket.Text = "";
            txtImporte.Text = "";
            txtDescMoneda.Text = "";
            txtFechaVencimiento.Text = "";
            calendar.SetDate(DateTime.Today);
        }

        /// <summary>
        /// Metodo que carga la lista de parametros a imprimir.
        /// </summary>
        private void CargarParametrosImpresion()
        {
            // limpiar lista de parametros a imprimir
            listaParamImp.Clear();
            // si la impresion es de sticker
                // flag de tickets (se utiliza para que lea el nodo de manera diferente a la de voucher)
                //listaParamImp.Add(Define.ID_PRINTER_PARAM_FLAG_TICKET, "T");

                // cantidad de ticket
                //listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Tickets.ToString());
                listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, Can_Tickets.ToString());

                ////--EAG
                //Fec_Vencimiento = Fec_Vencimiento.Substring(6, 2) + "/" + Fec_Vencimiento.Substring(4, 2) + "/" +
                //                  Fec_Vencimiento.Substring(0, 4);
                ////--EAG

                // recorre cada codigo de ticket
                for (int i = 0; i < Can_Tickets; i++)
                {
                    // fecha de vencimiento
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i, Fec_Vencimiento);

                    // codigo de ticket
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i,
                                      Lista_Tickets.Substring(
                                          i*Convert.ToInt32(LONG_TICKET),
                                          Convert.ToInt32(LONG_TICKET)));

                    // monto 
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_MONTO_PAGADO + "_" + i,
                                      Function.FormatDecimal(DImpPrecio));

                    //EAG 29/12/2009
                    listaParamImp.Add("desc_moneda_1" + "_" + i, "");
                    listaParamImp.Add("desc_moneda_2" + "_" + i, Dsc_Moneda);
                    //EAG
                }


        }

        private void txtFechaVencimiento_Click(object sender, EventArgs e)
        {
            if (!calendar.Visible)
                calendar.Visible = true;
            else
                calendar.Visible = false;
        }

        private void txtFechaVencimiento_Leave(object sender, EventArgs e)
        {

        }

        private void calendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            String fechaPeruanaInicial = e.Start.ToShortDateString();
            String fechaFormateada = fechaPeruanaInicial.Substring(0, 2) + "/" + fechaPeruanaInicial.Substring(3, 2) + "/" + fechaPeruanaInicial.Substring(6, 4);
            txtFechaVencimiento.Text = fechaFormateada;
            txtFechaVencimiento.SelectionStart = txtFechaVencimiento.Text.Length;
            calendar.Visible = false;
            txtImporte.Focus();
        }

        private void calendar_Leave(object sender, EventArgs e)
        {
            calendar.Visible = false;
        }


    }
}
