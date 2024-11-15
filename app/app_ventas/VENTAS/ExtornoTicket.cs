using LAP.TUUA.ALARMAS;
using LAP.TUUA.CONTROL;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LAP.TUUA.VENTAS
{
    public partial class ExtornoTicket : Form
    {
        protected BO_Turno objBOTurno;
        protected BO_Operacion objBOOperacion;
        private string CodigoTurno = "";
        private int CantidadTicketsSeleccionados;
        private Hashtable listaParamConfig;
        private Hashtable listaParamImp;
        private XmlDocument xml;
        private Usuario objUsuario;
        private Print impresion;
        public ExtornoTicket(Usuario objUsuario, Turno objTurno, Hashtable listaParamConfig, Hashtable listaParamImp, XmlDocument xml, Print impresion, Principal formMyParent)
        {
            InitializeComponent();
            objBOTurno = new BO_Turno();
            objBOOperacion = new BO_Operacion();
            dgwExtorno.AutoGenerateColumns = false;
            dgwTickets.AutoGenerateColumns = false;
            this.listaParamConfig = listaParamConfig;
            this.listaParamImp = listaParamImp;
            this.xml = xml;
            this.impresion = impresion;
            this.objUsuario = objUsuario;
            CargarEmpresasRecaudadorasExtorno();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //string codigoCajero = txtCodigoCajero.Text; //FL.
            string codigoCajero = cbxCajero.Text;
            string codigoTurno = txtTurno.Text;
            string fechaInicio = dtpFechaIni.Value.ToString("yyyyMMdd");
            string fechaFin = dtpFechaFin.Value.ToString("yyyyMMdd");
            string estado = rbActivo.Checked ? "A" : "C";

            var turnos = objBOTurno.ListarTurnosExtornoTicket(estado, codigoCajero, codigoTurno, fechaInicio, fechaFin);

            this.dgwExtorno.DataSource = turnos;

            lblNumeroRegistros.Text = "Numero de Registros " + turnos.Rows.Count;

            if (turnos.Rows.Count == 0)
                MessageBox.Show("La consulta realizada no devuelve resultados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgwExtorno_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
                dgwExtorno.Cursor = Cursors.Hand;
            else
                dgwExtorno.Cursor = Cursors.Default;
        }

        private void dgwExtorno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                pnlSeleccion.Visible = true;
                pnlBusqueda.Visible = false;
                CodigoTurno = dgwExtorno.Rows[e.RowIndex].Cells[0].Value.ToString();
                btnBuscarTickets_Click(null, null);
            }
        }

        private void tipoFiltro_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNumeroTicket.Checked)
            {
                txtNumeroTicket.Enabled = true;
                txtRangoTicketInicio.Enabled = false;
                txtRangoTicketFin.Enabled = false;
                txtRangoTicketInicio.Text = "";
                txtRangoTicketFin.Text = "";
            }
            else
            {
                txtNumeroTicket.Enabled = false;
                txtRangoTicketInicio.Enabled = true;
                txtRangoTicketFin.Enabled = true;
                txtNumeroTicket.Text = "";
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlSeleccion.Visible = false;
            pnlBusqueda.Visible = true;

            rbNumeroTicket.Checked = true;
            txtNumeroTicket.Enabled = true;
            txtRangoTicketInicio.Enabled = false;
            txtRangoTicketFin.Enabled = false;
            txtNumeroTicket.Text = "";
            txtRangoTicketInicio.Text = "";
            txtRangoTicketFin.Text = "";
            txtMotivoExtorno.Text = "";
            CodigoTurno = "";
            lblNumeroRegistrosTickets.Text = "";
            lblNumeroRegistrosTicketsSeleccionados.Text = "";
            CantidadTicketsSeleccionados = 0;
            dgwTickets.DataSource = null;
        }

        private void btnBuscarTickets_Click(object sender, EventArgs e)
        {
            string rangoTicketHasta;
            string rangoTicketDesde;

            if (rbNumeroTicket.Checked)
            {
                rangoTicketDesde = txtNumeroTicket.Text;
                rangoTicketHasta = txtNumeroTicket.Text;

                //if (string.IsNullOrEmpty(rangoTicketDesde))
                //{
                //    MessageBox.Show("Debe ingresar el Numero de Ticket.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
            else
            {
                rangoTicketDesde = txtRangoTicketInicio.Text;
                rangoTicketHasta = txtRangoTicketFin.Text;

                //if (string.IsNullOrEmpty(rangoTicketDesde) || string.IsNullOrEmpty(rangoTicketHasta))
                //{
                //    MessageBox.Show("Debe completar el rango de tickets a buscar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }


            var tickets = objBOTurno.GetTicketsByFilter(rangoTicketDesde, rangoTicketHasta, CodigoTurno);
            dgwTickets.DataSource = tickets;

            lblNumeroRegistrosTickets.Text = "Numero de Tickets Encontrados " + tickets.Rows.Count;
            lblNumeroRegistrosTicketsSeleccionados.Text = "Numero de Tickets Seleccionados 0";

            if (tickets.Rows.Count == 0)
                MessageBox.Show("No se encontró resultado alguno.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }
        private void btnExtornar_Click(object sender, EventArgs e)
        {
            CantidadTicketsSeleccionados = 0;
            var seleccionados = new List<string>();

            for (int i = 0; i < dgwTickets.RowCount; i++)
            {
                if (Convert.ToBoolean(dgwTickets.Rows[i].Cells[0].EditedFormattedValue))
                {
                    CantidadTicketsSeleccionados++;
                    seleccionados.Add(dgwTickets.Rows[i].Cells[5].Value.ToString());
                }
            }

            if (CantidadTicketsSeleccionados == 0)
            {
                MessageBox.Show("Debe de seleccionar por lo menos un ticket.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string MotivoExtorno = txtMotivoExtorno.Text;

            if (string.IsNullOrEmpty(MotivoExtorno))
            {
                MessageBox.Show("Debe ingresar el motivo del extorno.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var message = "";
            var result = objBOOperacion.ExtornarTicket(string.Join("", seleccionados.ToArray()), CodigoTurno, CantidadTicketsSeleccionados, MotivoExtorno, "0", ref message);

            if (result)
            {
                Imprimir(seleccionados);
                objBOOperacion.ExtornarTicket(string.Join("", seleccionados.ToArray()), CodigoTurno, CantidadTicketsSeleccionados, MotivoExtorno, "1", ref message);

                MessageBox.Show("Se realizo el extorno de los tickets seleccionados correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCancelar_Click(null, null);
                btnBuscar_Click(null, null);
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgwTickets_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                CantidadTicketsSeleccionados = 0;

                for (int i = 0; i < dgwTickets.RowCount; i++)
                {
                    if (Convert.ToBoolean(dgwTickets.Rows[i].Cells[e.ColumnIndex].EditedFormattedValue))
                        CantidadTicketsSeleccionados++;
                }

                lblNumeroRegistrosTicketsSeleccionados.Text = "Numero de Tickets Seleccionados " + CantidadTicketsSeleccionados;
            }
            else if (e.ColumnIndex == 8)
            {
                string codigoTicket = dgwTickets.Rows[e.RowIndex].Cells[5].Value.ToString();
                CargarDetalle(codigoTicket);
            }
        }
        private void Imprimir(List<string> tickets)
        {
            bool flgErrorImprimir = false;
            string sMensaError = "";

            try
            {

                // obtener el nombre de la operacion extorno de ticket
                String nombre = Define.ID_PRINTER_DOCUM_EXTORNOTICKET; 
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);
                // obtiene la configuracion de la impresora a utilizar
                String configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoSticker != null && !Property.puertoSticker.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoSticker.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion(tickets);

                // obtiene la data a imprimir con la impresora de sticker y guardarla en una variable
                String dataVoucher = impresion.ObtenerDataFormateada(listaParamImp, nodo);

                String xmlFormatoVoucher = nodo.OuterXml;

                String[] parametros = new String[]
                    {
                      "flagImpSticker=0",
                      "flagImpVoucher=1",
                      //"configImpSticker=" + configImpSticker,
                      "configImpVoucher=" + configImpVoucher,
                      //"dataSticker=" + dataSticker,
                      "dataVoucher=" + dataVoucher,
                      //"copiasVoucher=" + copias,
                      "xmlFormatoVoucher=" + xmlFormatoVoucher
                      //"listaCodigoTickets=" + Lista_Tickets
                    };

                frmPrintNet frmPrintNet = new frmPrintNet(parametros, listaParamImp);
                frmPrintNet.ShowDialog(this);

                String resultado = frmPrintNet.Resultado;
                /*
                if (resultado.Contains("Impresion"))
                {
                    char[] sep = { ',' };
                    Num_Tickets_Impresos = Int32.Parse(resultado.Split(sep)[1].ToString());
                    Property.puertoSticker = frmPrintNet.PuertoSerial.getConfigImpSticker()[0];
                    Property.puertoVoucher = frmPrintNet.PuertoSerial.getConfigImpVoucher()[0];

                    //this.lblVueltoInter.Text = objBOOpera.Imp_VueltoIzq;
                    //this.lblVueltoNac.Text = objBOOpera.Imp_VueltoDer;
                    //ActualizarFavoritos();
                    //Limpiar();
                    //btnAceptar.Enabled = false;
                    //MessageBox.Show((string)LabelConfig.htLabels["normal.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado.Contains("Salir"))
                {
                    Num_Tickets_Impresos = 0;
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flgErrorImprimir = true;
                    sMensaError = (string)LabelConfig.htLabels["impresion.msgCancel"];
                }
                else
                {
                    Num_Tickets_Impresos = 0;
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    flgErrorImprimir = true;
                    sMensaError = (string)LabelConfig.htLabels["impresion.msgError"];
                }*/
                listaParamImp.Clear();
            }
            catch (Exception ex)
            {
                listaParamImp.Clear();
                //Num_Tickets_Impresos = 1;
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgData"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                flgErrorImprimir = true;
                sMensaError = (string)LabelConfig.htLabels["impresion.msgData"];
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgError"], ex.Message + "\n" + ex.StackTrace);
            }
            /*

            if (flgErrorImprimir)
            {
                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();

                if (Can_Tickets > 1)
                {
                    //GeneraAlarma - Error al imprimir varios Tickets
                    GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000069", "I20", IpClient, "2", "Alerta W0000069", "Error al imprimir varios Tickets, Error: " + sMensaError + ",  Usuario: " + Property.strUsuario + ", Equipo: " + IpClient, Property.strUsuario);
                }
                else
                {
                    if (Can_Tickets == 1)
                    {
                        //GeneraAlarma - Error al imprimir un Ticket 
                        GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000068", "I20", IpClient, "2", "Alerta W0000068", "Error al imprimir un Ticket, Error: " + sMensaError + ",  Usuario: " + Property.strUsuario + ", Equipo: " + IpClient, Property.strUsuario);
                    }
                }
            }*/
        }

        private void CargarDetalle(string sNumTicket)
        {
            var form = new DetalleTicket(objUsuario, null, sNumTicket);
            form.ShowDialog(this);
        }

        private void CargarParametrosImpresion(List<string> tickets)
        {
            // limpiar lista de parametros a imprimir
            listaParamImp.Clear();
            // nombre del cajero
            listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario);
            listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, this.CodigoTurno);
            
            listaParamImp.Add("cantidad_ticket", tickets.Count);
            listaParamImp.Add("imp_extorno", Function.FormatDecimal(0, Define.NUM_DECIMAL));//TODO:Falta

            int intLongTicket = Define.Longitud_Ticket;
            int contador = 0;
            int j = 0;
            for (int i = 0; i < tickets.Count;i++)
            {
                if ((i + 1) % 2 == 0)//Par
                {
                    listaParamImp["codigo_ticket_par" + "_" + j] = tickets[i];
                    contador += intLongTicket;
                    j++;
                }
                else//Impar
                {
                    listaParamImp["codigo_ticket_impar" + "_" + j] = tickets[i];
                    contador += intLongTicket;
                }
            }
        }

        private void dgwTickets_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
                dgwExtorno.Cursor = Cursors.Hand;
            else
                dgwExtorno.Cursor = Cursors.Default;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) //FL.
        {
            if (dgwTickets.Columns[e.ColumnIndex].Name == "Fch_Vuelo" && e.Value != null) //FL.
            {
                string fechaTexto = e.Value.ToString(); //FL.
                if (DateTime.TryParseExact(fechaTexto, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime fecha)) //FL.
                {
                    e.Value = fecha.ToString("dd/MM/yyyy"); //FL.
                    e.FormattingApplied = true; //FL.
                }
            }
        }
        private void CargarEmpresasRecaudadorasExtorno() //FL.
        {
            try
            {
                BO_ParameGeneral objBOParameGeneralEmpresa = new BO_ParameGeneral();
                DataTable empresas = objBOParameGeneralEmpresa.ObtenerEmpresaPorIdentificadorPadre("ER");

                cbxEmpresaRecaudadora.DataSource = empresas;
                cbxEmpresaRecaudadora.DisplayMember = "Descripcion";
                cbxEmpresaRecaudadora.ValueMember = "Identificador";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las empresas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
