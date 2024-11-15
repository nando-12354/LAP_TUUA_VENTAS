﻿using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.VENTAS
{
   public partial class Masivo : Venta
    //public partial class Masivo : Form
    {
        //protected BO_Operacion objBOOpera;
        protected BO_Turno objBOTurno;
        //protected TipoTicket objTipoTicket;
        protected string Tip_Vuelo;
        protected string Tip_Pasajero;
        protected string Tip_Transbordo;
        protected Compania objAerolinea;
        protected bool Flg_Inicio;
        protected decimal Imp_Monto;
        protected string Num_Vuelo;
        protected string Fec_Vuelo;
        protected string Hora_Vuelo;
        protected string Dsc_Destino;
        protected string Dsc_Aerolinea;
        //protected TasaCambio objTasaCambio;
        protected Usuario objUsuario;
        protected Turno objTurno;
        protected int Can_Tickets;
        protected List<VueloProgramado> favoritos;
        protected Principal frmPrincipal;
        protected string Cod_ModVenta;
        protected BO_Error objBOError;
        protected bool Flg_Error;
        protected decimal Imp_TotPagBase;
        protected decimal Imp_TotPagxMoneda;
        protected List<Moneda> listaMonedas;
        protected DateTimePicker dtpFechaVuelo;
        protected string Tip_Pago;
        protected string Lista_Tickets;
        protected string Fec_Vencimiento;
        protected VentaMasiva objVentaMasiva;
        protected decimal Imp_TCPagado;
        protected decimal Imp_MontoSol;

        // parametros de impresion (GGarcia-20090907)
        // lista parametros de configuracion
        private Hashtable listaParamConfig;
        // lista parametros a imprimir
        private Hashtable listaParamImp;
        // xml
        private XmlDocument xml;
        // instancia de impresion
        private Print impresion;
        private int Can_Max_Ticket;
        private int Long_Ticket;
        private int Num_Tickets_Impresos;
        private List<Cambio> listaCambio;
        private Cambio objCambioSol;
        private decimal Imp_IngresoInt;
        private decimal Imp_IngresoNac;
        private decimal Imp_EgresoNac;
        private bool Flg_TipoTicket;
        private decimal Imp_TCParche;
        private int Num_TabIndex;
        private Hashtable Lista_Tasas;

        private Define.centavos Flg_Centavo;

        //private int cont_favoritos;
        //private static int cont_favoritos_v;
        //private int cont_favoritos_v;
        
        //protected List<int> favoritos_v;
        // se agregan parametros de impresion (GGarcia-20090907)
        public Masivo(Usuario objUsuario, Turno objTurno, Principal frmPrincipal, Hashtable listaParamConfig, Hashtable listaParamImp, XmlDocument xml, Print impresion)
        {
            InitializeComponent();
            Cod_Moneda_Sol = Property.htProperty[Define.MONEDANAC].ToString();
            Cod_Moneda_Dol = "DOL";
            Lista_Dialogo_Cambio = new List<Cambio>();
            Lista_Cambio_Vuelto = new List<Cambio>();
            Lista_Flujo_Importe = new Hashtable();
            Lista_Dialogo_Tasa = new Hashtable();
            InicilizarPropiedadesDialogo();
            CargarFormaPago();
            Flg_Error = true;
            Num_TabIndex = 0;
            this.rbNacional.Select();
            this.rbNacional.Focus();
            this.rbNacional.TabIndex = 0;
            this.rbNacional.TabStop = true;

            this.objUsuario = objUsuario;
            this.objTurno = objTurno;
            Flg_Inicio = true;
            Flg_Error = false;

            //cargando vuelos favoritos
            this.frmPrincipal = frmPrincipal;
            favoritos = frmPrincipal.listaFavoritos;
            this.dgvFavoritos.DataSource = favoritos;

            objBOOpera = new BO_Operacion();
            objBOError = new BO_Error();
            if (!MostrarPrecioTicketDefault())
            {
                Close();
                return;
            }

            CargarAerolineas();

            //vuelos programados
            objAerolinea = new Compania();
            objAerolinea.SCodCompania = this.cbxAerolinea.SelectedValue.ToString();
            objAerolinea.SDscCompania = this.cbxAerolinea.SelectedText;

            //fecha actual en formato YYYYMMDD
            string strFecha = DateTime.Now.ToShortDateString();
            strFecha = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
            this.dgvVuelos.DataSource = objBOOpera.ListarVuelosxCompania(objAerolinea.SCodCompania, strFecha, Tip_Vuelo, Define.MODO_MASIVO_CONT);

            objBOTurno = new BO_Turno();

            //asignar valor label
            decimal importe = nudCantidad.Value * objTipoTicket.DImpPrecio;
            lblTotPagBaseVal.Text = importe.ToString();

            //combo monedas
            CargarMonedas();

            //Tasa de cambio
            ActualizarTasaCambio();
            //Tipos de tickets
            MostrarTipoTickets();

            Cod_ModVenta = Property.htProperty[Define.MOD_VENTA_MAS_CONT].ToString();

            this.dtpFecha.MinDate = new System.DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 0, 0, 0, 0);
            HabilitarContado();

            // inicializar parametros de impresion (GGarcia-20090907)
            // lista parametros de config
            this.listaParamConfig = listaParamConfig;
            // lista parametros a imprimir
            this.listaParamImp = listaParamImp;
            // xml
            this.xml = xml;
            // instancia de impresion
            this.impresion = impresion;

            Flg_TipoTicket = true;
            Long_Ticket = Convert.ToInt32((string)Property.htParametro[Define.ID_PARAM_LONG_TICKET]);
            Can_Max_Ticket = Define.STRING_SIZE / Long_Ticket;
            DeshabilitarMontoSol();
            FiltrarFavoritos();
            SetearLabels();
            LimpiarResumen(0);
            SetearDefaultVuelto();
           // Limpiar();
            nudCantidad.Maximum = (decimal)Can_Max_Ticket;
        }

        private void CargarMonedas()
        {
            listaMonedas = objBOTurno.ListarMonedas();
            if (!(listaMonedas != null && listaMonedas.Count > 0))
            {
                MessageBox.Show("No se dispone de monedas o tasas de cambio registrados en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
                return;
            }

            this.cbxMoneda.DataSource = listaMonedas;
            SetDefaultMoneda();
        }

        private void SetDefaultMoneda()
        {
            for (int i = 0; i < listaMonedas.Count; i++)
            {
                if (listaMonedas[i].SCodMoneda == objTipoTicket.SCodMoneda)
                {
                    lblSimbInter.Text = objTipoTicket.Dsc_Simbolo;
                    cbxMoneda.SelectedIndex = i;
                    SetearCheckRadioButtonVuelto(Define.centavos.TODO, listaMonedas[i]);
                    return;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            this.Close();
        }

        private void rbNacional_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNacional.Checked)
            {
                Tip_Vuelo = Define.NACIONAL;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
                CargarVuelos();
                FiltrarFavoritos();
            }
        }

        private void rbInter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInter.Checked)
            {
                Tip_Vuelo = Define.INTERNACIONAL;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
                CargarVuelos();
                FiltrarFavoritos();
            }
        }

        private void rbAdulto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAdulto.Checked)
            {
                Tip_Pasajero = Define.ADULTO;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
            }
        }

        private void rbInfante_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInfante.Checked)
            {
                Tip_Pasajero = Define.INFANTE;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
            }
        }

        private void rbNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNormal.Checked)
            {
                Tip_Transbordo = Define.NORMAL;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
            }
        }

        private void rnTrans_CheckedChanged(object sender, EventArgs e)
        {
            if (rnTrans.Checked)
            {
                Tip_Transbordo = Define.TRANSFERENCIA;
                MostrarPrecioTicket(Tip_Vuelo, Tip_Pasajero, Tip_Transbordo);
            }
        }

        private void MostrarPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            if (!Flg_TipoTicket)
            {
                return;
            }
            objTipoTicket = objBOOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objTipoTicket != null)
            {
                //Tasa de cambio
                if (objTipoTicket.SCodMoneda == Cod_Moneda_Sol)
                {
                    objTasaCambio = new TasaCambio();
                    objTasaCambio.SCodMoneda = objTipoTicket.SCodMoneda;
                    objTasaCambio.DImpCambioActual = 1 * Define.FACTOR_DECIMAL * Define.FACTOR_DECIMAL;
                }
                else
                {
                    objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA);
                }
                if (objTasaCambio == null)
                {
                    MessageBox.Show("Tasa Cambio No DISPONIBLE EN SISTEMA para tipo de ticket seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Limpiar();
                }
                else
                {
                    //this.lblTCValue.Text = Function.FormatDecimal(objTasaCambio.DImpCambioActual, 4).ToString();
                    this.lblPrecioValue.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
                    if (nudCantidad.Value > 0)
                    {
                        try
                        {
                            Imp_TotPagBase = nudCantidad.Value * objTipoTicket.DImpPrecio;
                            lblTotPagBaseVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
                        }
                        catch
                        {
                            lblTotPagBaseVal.Text = "0.00";
                        }
                        ActualizarMontoAPagar();
                    }
                }
            }
            else
            {
                MessageBox.Show("Tipo Ticket No DISPONIBLE EN SISTEMA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        private void cbxAerolinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Flg_Inicio)
            {
                //objAerolinea.SCodCompania = this.cbxAerolinea.SelectedValue.ToString();
                //objAerolinea.SDscCompania = this.cbxAerolinea.SelectedText;
                //string strFecha = this.dtpFecha.Text.Substring(6, 4) + this.dtpFecha.Text.Substring(3, 2) + this.dtpFecha.Text.Substring(0, 2);
                //this.dgvVuelos.DataSource = objBOOpera.ListarVuelosxCompania(objAerolinea.SCodCompania, strFecha, Tip_Vuelo);
                ModoSeleccion();
            }
            Flg_Inicio = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private bool Validar()
        {
            LimpiarErrProvider();
            if (objTipoTicket == null)
            {
                this.erpCantidad.SetError(rbNacional, "Seleccione tipo ticket válido.");
                return false;
            }
            if (nudCantidad.Value == 0)
            {
                this.erpCantidad.SetError(nudCantidad, "Ingrese cantidad de tickets válido.");
                return false;
            }
            try
            {
                Can_Tickets = (Int32)nudCantidad.Value;
                if (Can_Tickets == 0)
                {
                    this.erpCantidad.SetError(nudCantidad, "Ingrese cantidad de tickets válido.");
                    return false;
                }
            }
            catch
            {
                this.erpCantidad.SetError(nudCantidad, "Ingrese cantidad de tickets válido.");
                return false;
            }
            if (txtCia.Text == "")
            {
                erpCia.SetError(txtCia, (string)LabelConfig.htLabels["masivo.erpCia"]);
                return false;
            }
            if (txtRepte.Text == "")
            {
                erpRepte.SetError(txtRepte, (string)LabelConfig.htLabels["masivo.erpRepte"]);
                return false;
            }
            if (txtMonto.Text == "")
            {
                this.erpMonto.SetError(txtMonto, "Ingrese monto válido");
                return false;
            }
            try
            {
                string strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
                Imp_Monto = decimal.Parse(txtMonto.Text);
                if (Cod_Moneda_Sol != strMonPago)
                {
                    TasaCambio objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, strMonPago != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                    decimal decTCPagado = objTCPagado.DImpCambioActual;
                    if (strMonPago != objTipoTicket.SCodMoneda)
                    {
                        //OBS-25
                        if (Imp_Monto == Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, Define.NUM_DECIMAL))
                        {
                            Imp_Monto = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, 3);
                        }
                    }
                }
            }
            catch
            {
                this.erpMonto.SetError(txtMonto, "Ingrese monto válido.");
                return false;
            }

            if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
            {
                if (txtNumReferencia.Text == "")
                {
                    this.erpMonto.SetError(txtNumReferencia, "Ingrese número de referencia.");
                    return false;
                }
                if (txtNumReferencia.Text.Length != 10)
                {
                    this.erpMonto.SetError(txtNumReferencia, "Ingrese número de referencia de longitud 10.");
                    return false;
                }
            }

            if (rbSeleccionar.Checked && dgvVuelos.SelectedRows.Count == 0)
            {
                this.erpVuelo.SetError(dgvVuelos, "Ingrese un vuelo válido.");
                return false;
            }
            if (rbFavorito.Checked && dgvFavoritos.SelectedRows.Count == 0)
            {
                this.erpVuelo.SetError(rbFavorito, "Ingrese un vuelo válido.");
                return false;
            }
            try
            {
                string strFecha = "";
                if (rbSeleccionar.Checked)
                {
                    Dsc_Aerolinea = dgvVuelos.SelectedRows[0].Cells[1].Value.ToString();
                    Num_Vuelo = dgvVuelos.SelectedRows[0].Cells[2].Value.ToString();
                    Dsc_Destino = dgvVuelos.SelectedRows[0].Cells[3].Value.ToString();
                    Hora_Vuelo = dgvVuelos.SelectedRows[0].Cells[4].Value.ToString();
                    strFecha = dgvVuelos.SelectedRows[0].Cells[5].Value.ToString();
                    Fec_Vuelo = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                    objAerolinea.SCodCompania = dgvVuelos.SelectedRows[0].Cells[6].Value.ToString();
                    objAerolinea.SDscCompania = dgvVuelos.SelectedRows[0].Cells[1].Value.ToString();
                }
                else if (rbFavorito.Checked)
                {
                    Dsc_Aerolinea = dgvFavoritos.SelectedRows[0].Cells[2].Value.ToString();
                    Num_Vuelo = dgvFavoritos.SelectedRows[0].Cells[3].Value.ToString();
                    Dsc_Destino = dgvFavoritos.SelectedRows[0].Cells[4].Value.ToString();
                    Hora_Vuelo = dgvFavoritos.SelectedRows[0].Cells[5].Value.ToString();
                    strFecha = dgvFavoritos.SelectedRows[0].Cells[6].Value.ToString();
                    Fec_Vuelo = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                    objAerolinea.SCodCompania = dgvFavoritos.SelectedRows[0].Cells[7].Value.ToString();
                    objAerolinea.SDscCompania = dgvFavoritos.SelectedRows[0].Cells[2].Value.ToString();
                }
                else
                {
                    if (cbxAerolinea.SelectedIndex == 0)
                    {
                        this.erpAero.SetError(cbxAerolinea, "Seleccione una Aerolinea.");
                        return false;
                    }
                    objAerolinea.SCodCompania = ((ModVentaComp)cbxAerolinea.SelectedItem).SCodCompania;
                    objAerolinea.SDscCompania = ((ModVentaComp)cbxAerolinea.SelectedItem).Dsc_Compania;
                    Dsc_Aerolinea = dgvVuelos.Rows[0].Cells[1].Value.ToString();
                    Num_Vuelo = dgvVuelos.Rows[0].Cells[2].Value.ToString();
                    Dsc_Destino = dgvVuelos.Rows[0].Cells[3].Value.ToString();
                    Hora_Vuelo = dgvVuelos.Rows[0].Cells[4].Value.ToString();
                    strFecha = dgvVuelos.Rows[0].Cells[5].Value.ToString();
                    Fec_Vuelo = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                }
            }
            catch
            {
                this.erpVuelo.SetError(dgvVuelos, "Ingrese un vuelo válido.");
                return false;
            }
            return true;
        }

        private void cbxMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetearCheckRadioButtonVuelto(Define.centavos.TODO, (Moneda)this.cbxMoneda.SelectedItem);
            Centavos(((Moneda)this.cbxMoneda.SelectedItem).SCodMoneda);
            ActualizarTasaCambio();
            ActualizarMontoAPagar();
        }

        private void DoAfterCurrencySelection()
        {
            try
            {
                Moneda objMoneda = (Moneda)this.cbxMoneda.SelectedItem;
                this.lblSimbInter.Text = objMoneda.SDscSimbolo;
                if (objMoneda.SCodMoneda == objTipoTicket.SCodMoneda)
                {
                    lblTotPagarVal.Text = lblTotPagBaseVal.Text;
                    if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                    {
                        txtMonto.Text = this.lblTotPagarVal.Text;
                    }
                    return;
                }

                if (objMoneda.SCodMoneda == Cod_Moneda_Sol)
                {
                    Imp_TotPagxMoneda = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual, 2);
                    lblTotPagarVal.Text = Imp_TotPagxMoneda.ToString();
                    if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                    {
                        txtMonto.Text = this.lblTotPagarVal.Text;
                    }
                    return;
                }

                TasaCambio objTC = this.objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, Define.TC_VENTA);
                Imp_TotPagxMoneda = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / objTC.DImpCambioActual, 2);
                if (Imp_TotPagxMoneda == 0)
                {
                    lblTotPagarVal.Text = "0.00";
                }
                else
                {
                    lblTotPagarVal.Text = Imp_TotPagxMoneda.ToString();
                }
                if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                {
                    txtMonto.Text = this.lblTotPagarVal.Text;
                }
            }
            catch { }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            string strFecha = this.dtpFecha.Text.Substring(6, 4) + this.dtpFecha.Text.Substring(3, 2) + this.dtpFecha.Text.Substring(0, 2);
            this.dgvVuelos.DataSource = objBOOpera.ListarVuelosxCompania(objAerolinea.SCodCompania, strFecha, Tip_Vuelo, Define.MODO_MASIVO_CONT);
            if (rbIngresar.Checked)
            {
                this.dgvVuelos.Rows[0].Cells[5].Value = strFecha;
            }
        }

        private void rbIngresar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIngresar.Checked)
            {
                ModoIngreso();
            }
        }


        private void dgvVuelos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvVuelos.Focused && this.dgvVuelos.CurrentCell.ColumnIndex == 1)
            {
                this.dgvVuelos.CurrentCell.Value = dtpFechaVuelo.Text;
            }
        }


        private void rbSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSeleccionar.Checked)
            {
                ModoSeleccion();
                this.dgvVuelos.Select();
                this.dgvVuelos.Focus();
            }
        }

        private void ActualizarFavoritos()
        { 
            if (rbSeleccionar.Checked)
            {
                VueloProgramado objVueloPrg = new VueloProgramado();

                // la primera asignacion
                if (favoritos.Count == 0)
                {
                    //cont_favoritos = 1;
                    if (Tip_Vuelo == "N")
                    {
                        frmPrincipal.cont_favoritos_nac = this.frmPrincipal.cont_favoritos_nac + 1;
                        objVueloPrg.Numerador = frmPrincipal.cont_favoritos_nac;
                    }
                    else
                    {
                        frmPrincipal.cont_favoritos_int = this.frmPrincipal.cont_favoritos_int + 1;
                        objVueloPrg.Numerador = frmPrincipal.cont_favoritos_int;
                    }
                    //objVueloPrg.Numerador = cont_favoritos;
                    objVueloPrg.Dsc_Compania = Dsc_Aerolinea;
                    objVueloPrg.Dsc_Destino = Dsc_Destino;
                    objVueloPrg.Fch_Vuelo = Function.FormatFecha(Fec_Vuelo);
                    objVueloPrg.Hor_Vuelo = Hora_Vuelo;//.Substring(0, 2) + ":" + Hora_Vuelo.Substring(2, 2) + ":" + Hora_Vuelo.Substring(4, 2);
                    objVueloPrg.Num_Vuelo = Num_Vuelo;
                    objVueloPrg.Cod_Compania = objAerolinea.SCodCompania;
                    objVueloPrg.Tip_Vuelo = Tip_Vuelo;
                    favoritos.Add(objVueloPrg);
                    frmPrincipal.listaFavoritos = favoritos;
                    bdsrcFavoritos.Add(objVueloPrg);
                    this.dgvFavoritos.DataSource = bdsrcFavoritos;
                    FiltrarFavoritos();

                    return;
                }
                
                for (int i = 0; i < favoritos.Count; i++)
                {
                    if (favoritos[i].Num_Vuelo == Num_Vuelo)
                    {
                        return;
                    }
                    if ((i + 1) == favoritos.Count) //ultimo objeto
                    {
                        /*if (favoritos[i].Tip_Vuelo == Tip_Vuelo)
                        {
                            //se mantiene la enumeracion
                            cont_favoritos = favoritos[i].Numerador + 1;
                            objVueloPrg.Numerador = cont_favoritos;
                        }
                        else
                        {
                            //nueva enumeracion
                            frmPrincipal.cont_favoritos_v = this.frmPrincipal.cont_favoritos_v + 1;
                            objVueloPrg.Numerador = frmPrincipal.cont_favoritos_v;   
                        }*/

                        if (Tip_Vuelo == "N")
                        {
                            //se mantiene la enumeracion
                            frmPrincipal.cont_favoritos_nac = this.frmPrincipal.cont_favoritos_nac + 1;
                            objVueloPrg.Numerador = frmPrincipal.cont_favoritos_nac;
                        }
                        else
                        {
                            //nueva enumeracion
                            frmPrincipal.cont_favoritos_int = this.frmPrincipal.cont_favoritos_int + 1;
                            objVueloPrg.Numerador = frmPrincipal.cont_favoritos_int;
                        } 
                    }
                }

                //objVueloPrg.Numerador = cont_favoritos;
                objVueloPrg.Dsc_Compania = Dsc_Aerolinea;
                objVueloPrg.Dsc_Destino = Dsc_Destino;
                objVueloPrg.Fch_Vuelo = Function.FormatFecha(Fec_Vuelo);
                objVueloPrg.Hor_Vuelo = Hora_Vuelo;//.Substring(0, 2) + ":" + Hora_Vuelo.Substring(2, 2) + ":" + Hora_Vuelo.Substring(4, 2);
                objVueloPrg.Num_Vuelo = Num_Vuelo;
                objVueloPrg.Cod_Compania = objAerolinea.SCodCompania;
                objVueloPrg.Tip_Vuelo = Tip_Vuelo;
                favoritos.Add(objVueloPrg);
                frmPrincipal.listaFavoritos = favoritos;
                bdsrcFavoritos.Add(objVueloPrg);
                this.dgvFavoritos.DataSource = bdsrcFavoritos;
                FiltrarFavoritos();
            }
        }

        private void rbFavorito_CheckedChanged(object sender, EventArgs e)
        {
            dgvFavoritos.Select();
            dgvFavoritos.Focus();
        }

        private void CheckRBEfectivo()
        {
            txtMonto.Enabled = true;
            txtMonto.Text = "";
            txtNumReferencia.Enabled = false;
            chkSol.Enabled = true;
        }

        private void HabilitarTarjeta()
        {
            txtMonto.Enabled = true;
            txtMonto.Text = this.lblTotPagarVal.Text;
            txtNumReferencia.Enabled = true;
            DeshabilitarSoles();
        }

        private void HabilitarContado()
        {
            txtMonto.Enabled = true;
            txtNumReferencia.Text = "";
            txtNumReferencia.Enabled = false;
            Tip_Pago = Define.TIP_PAGO_EFECTIVO;
            chkSol.Enabled = true;
            HabilitarSoles();
            btnAgregar.Enabled = false;
        }

       /* private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }*/

        private void nudCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        public void SoloNumeros(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
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

        private void txtNumReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarSoloNumeros(e);
        }

        private void txtMonto_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void MostrarTipoTickets()
        {
            List<TipoTicket> lista = objBOOpera.ListarTipoTickets();
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].STipTrasbordo == Define.TRANSFERENCIA)
                {
                    pnlTipTrans.Visible = true;
                    return;
                }
            }
            pnlTipTrans.Visible = false;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            Flg_TipoTicket = false;
            //nudCantidad.Value = 0;
            //nudCantidad.Value = 0;
           // txtCantidad.Text = "";
            //txtMonto.Text = "";
            txtNumReferencia.Text = "";
            lblVueltoInter.Text = "0.00";
            lblVueltoNac.Text = "0.00";
            lblVueltoDol.Text = "0.00";
            lblTotPagarVal.Text = "0.00";
            lblTotPagBaseVal.Text = "0.00";
            btnAceptar.Enabled = true;
            LimpiarErrProvider();
            rbNormal.Checked = true;
            rbNacional.Checked = true;
            rbAdulto.Checked = true;
            txtRepte.Text = "";
            txtCia.Text = "";
            MostrarPrecioTicketDefault();
            nudCantidad.Value = 0;
            rbSeleccionar.Checked = true;
            CheckRBEfectivo();
            txtMontoSol.Text = "";
            SetDefaultMoneda();
            ActualizarTasaCambio();
            HabilitarContado();
            VueltosCero();
            Flg_TipoTicket = true;
            //Inicializacion de propiedades de la clase Normal
            Imp_Monto = 0;
            Imp_TotPagxMoneda = 0;
            SetearDefaultVuelto();
            Lista_Cambio_Vuelto = new List<Cambio>();
            SeleccionarPagoEfectivo(0);
            InicilizarPropiedadesDialogo();
            LimpiarResumen();
            DeshabilitarSoles();
        }

        /*private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Imp_TotPagBase = decimal.Parse(txtCantidad.Text) * objTipoTicket.DImpPrecio;
                lblTotPagBaseVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
            }
            catch
            {
                lblTotPagBaseVal.Text = "0.00";
                txtMonto.Text = lblTotPagBaseVal.Text;
                Limpiar();
            }
            lblTotPagarVal.Text = lblTotPagBaseVal.Text;
            if (!txtMonto.Enabled)
            {
                txtMonto.Text = this.lblTotPagarVal.Text;
            }
            ActualizarMontoAPagar();
        }*/

        private void nudCantidad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Imp_TotPagBase = nudCantidad.Value * objTipoTicket.DImpPrecio;
                lblTotPagBaseVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
            }
            catch
            {
                lblTotPagBaseVal.Text = "0.00";
                txtMonto.Text = lblTotPagBaseVal.Text;
                Limpiar();
            }
            lblTotPagarVal.Text = lblTotPagBaseVal.Text;
            if (!txtMonto.Enabled)
            {
                txtMonto.Text = this.lblTotPagarVal.Text;
            }
            ActualizarMontoAPagar();
        }

        private void Normal_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }

        private bool MostrarPrecioTicketDefault()
        {
            Tip_Vuelo = Define.NACIONAL;
            Tip_Pasajero = Define.ADULTO;
            Tip_Transbordo = Define.NORMAL;
            objTipoTicket = objBOOpera.ObtenerPrecioTicket(Define.NACIONAL, Define.ADULTO, Define.NORMAL);
            if (objTipoTicket == null)
            {
                MessageBox.Show("Tipo Ticket No DISPONIBLE EN SISTEMA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            this.lblPrecioValue.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
            return true;
        }

        private void VerificarSoloNumeros(KeyEventArgs e)
        {
            if ((e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9) || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9) || (e.KeyCode == Keys.Back))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvVuelos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvVuelos.EditingControl.KeyPress += new KeyPressEventHandler(EditingControl_KeyPress);
        }

        private void EditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        
        private void dgvVuelos_KeyDown(object sender, KeyEventArgs e)
        {
            if (rbIngresar.Checked || rbFavorito.Checked)
            {
                return;
            }
            else
            {
                if (e.Control)
                {
                    if (e.KeyCode == Keys.S)
                    {
                        return;
                    }
                }

                if (Char.IsLetterOrDigit(((Char)e.KeyCode)))
                {
                    switch (e.KeyCode)
                    {
                        case Keys.F2: e.SuppressKeyPress = false; return;
                        case Keys.F3: e.SuppressKeyPress = false; return;
                        case Keys.F4: e.SuppressKeyPress = false; return;
                        case Keys.F5: e.SuppressKeyPress = false; return;
                        case Keys.F6: e.SuppressKeyPress = false; return;
                        case Keys.F7: e.SuppressKeyPress = false; return;
                        case Keys.F8: e.SuppressKeyPress = false; return;
                        case Keys.F9: e.SuppressKeyPress = false; return;
                        case Keys.F10: e.SuppressKeyPress = false; return;
                        case Keys.F11: e.SuppressKeyPress = false; return;
                        case Keys.F12: e.SuppressKeyPress = false; return;
                    }

                    char ch = ((Char)e.KeyCode);
                    dgvVuelos_Busqueda(ch.ToString());
                }else
                {
                    return;
                }
            }
           
        }

        string aux = "";
        private void dgvVuelos_Busqueda(string teclas)
        {
            if (dgvVuelos.Rows.Count == 0)
            {
                return;
            }

            int indice = -1;
            if (aux == "")
                aux = teclas;
            else
            {
                if (aux == teclas)
                {
                    //posicion
                    indice = dgvVuelos.SelectedRows[0].Index;
                }
                else {

                    aux = teclas;
                    //posicion
                    indice = dgvVuelos.SelectedRows[0].Index;
                }
            }

            bool encontro = false;
            foreach (DataGridViewRow dgvRow in dgvVuelos.Rows)
            {
                if (dgvRow.Cells["numVueloDataGridViewTextBoxColumn"].FormattedValue.ToString().StartsWith(teclas))
                {
                    if (dgvRow.Index != indice && dgvRow.Index > indice)
                    {
                        //dgvRow.Selected = true;
                        dgvVuelos.CurrentCell = dgvVuelos.Rows[dgvRow.Index].Cells[2];//mover a la celda
                        int valor = dgvRow.Index;

                        dgvVuelos.FirstDisplayedScrollingRowIndex = valor;
                        encontro = true;
                        break;
                    }
                }
            }
            if (!encontro)
            {
                indice = -1;
                foreach (DataGridViewRow dgvRow2 in dgvVuelos.Rows)
                {
                    if (dgvRow2.Cells["numVueloDataGridViewTextBoxColumn"].FormattedValue.ToString().StartsWith(teclas))
                    {
                        if (dgvRow2.Index != indice && dgvRow2.Index > indice)
                        {
                            //dgvRow2.Selected = true;
                            dgvVuelos.CurrentCell = dgvVuelos.Rows[dgvRow2.Index].Cells[2];//mover a la celda
                            int valor = dgvRow2.Index;

                            dgvVuelos.FirstDisplayedScrollingRowIndex = valor;
                            encontro = true;
                            break;
                        }
                    }
                }
            }
        }

        private void dgvFavoritos_KeyDown(object sender, KeyEventArgs e)
        {
            char ch = (Char)e.KeyCode;

            switch (e.KeyCode)
            {
                case Keys.F2: e.SuppressKeyPress = false; return;
                case Keys.F3: e.SuppressKeyPress = false; return;
                case Keys.F4: e.SuppressKeyPress = false; return;
                case Keys.F5: e.SuppressKeyPress = false; return;
                case Keys.F6: e.SuppressKeyPress = false; return;
                case Keys.F7: e.SuppressKeyPress = false; return;
                case Keys.F8: e.SuppressKeyPress = false; return;
                case Keys.F9: e.SuppressKeyPress = false; return;
                case Keys.F10: e.SuppressKeyPress = false; return;
                case Keys.F11: e.SuppressKeyPress = false; return;
                case Keys.F12: e.SuppressKeyPress = false; return;

                case Keys.NumPad0: ch = '0'; break;
                case Keys.NumPad1: ch = '1'; break;
                case Keys.NumPad2: ch = '2'; break;
                case Keys.NumPad3: ch = '3'; break;
                case Keys.NumPad4: ch = '4'; break;
                case Keys.NumPad5: ch = '5'; break;
                case Keys.NumPad6: ch = '6'; break;
                case Keys.NumPad7: ch = '7'; break;
                case Keys.NumPad8: ch = '8'; break;
                case Keys.NumPad9: ch = '9'; break;
                //default: break;
            }

            if (Char.IsNumber(ch))
            {
                dgvFavoritos_Busqueda(ch.ToString());
                e.SuppressKeyPress = true;
            }
            else {
                return;
            }
        }
        string aux_fav = "";
        private void dgvFavoritos_Busqueda(string teclas)
        {
            int indice = -1;
            if (aux_fav == "")
                aux_fav = teclas;
            else
            {
                if (aux_fav == teclas)
                {
                    //posicion
                    indice = dgvFavoritos.SelectedRows[0].Index;
                }
                else
                {
                    aux_fav = teclas;
                    //posicion
                    indice = dgvFavoritos.SelectedRows[0].Index;
                }
            }

            bool encontro = false;
            foreach (DataGridViewRow dgvRow in dgvFavoritos.Rows)
            {
                if (dgvRow.Cells[0].FormattedValue.ToString().StartsWith(teclas))
                {
                    if (dgvRow.Index != indice && dgvRow.Index > indice)
                    {
                        //dgvRow.Selected = true;
                        dgvFavoritos.CurrentCell = dgvFavoritos.Rows[dgvRow.Index].Cells[0];//mover a la celda
                        int valor = dgvRow.Index;

                        dgvFavoritos.FirstDisplayedScrollingRowIndex = valor;
                        encontro = true;
                        break;
                    }
                }
            }
            if (!encontro)
            {
                indice = -1;
                foreach (DataGridViewRow dgvRow2 in dgvFavoritos.Rows)
                {
                    if (dgvRow2.Cells[0].FormattedValue.ToString().StartsWith(teclas))
                    {
                        if (dgvRow2.Index != indice && dgvRow2.Index > indice)
                        {
                            //dgvRow2.Selected = true;
                            dgvFavoritos.CurrentCell = dgvFavoritos.Rows[dgvRow2.Index].Cells[0];//mover a la celda
                            int valor = dgvRow2.Index;

                            dgvFavoritos.FirstDisplayedScrollingRowIndex = valor;
                            encontro = true;
                            break;
                        }
                    }
                }
            }
        }


        private void txtCia_KeyPress(object sender, KeyPressEventArgs e)
        {
            VerificarKeys(e);
        }

        private void txtRepte_KeyPress(object sender, KeyPressEventArgs e)
        {
            VerificarKeys(e);
        }

        public void VerificarKeys(KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void pruebaCasa()
        {

            // obtener el nombre de la operacion venta ticket stickers soles[/dolares/euros 
            string nombre = obtenerOperacion(0);

            // obtiene el nodo segun el nombre
            XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);

            // obtiene la configuracion de la impresora a utilizar
            string configuracion = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

            // carga la lista de parametros a imprimir
            CargarParametrosImpresion(0);

            // obtiene la estructura del documento a imprimir
            string[] data = impresion.ObtenerData(listaParamImp, nodo);

            // configura la impresora a utilizar 
            impresion.configurarImpresora(configuracion);

            // obtener el nombre de la operacion venta ticket voucher soles/dolares/euros 
            nombre = obtenerOperacion(1);

            // obtiene el nodo segun el nombre de la operacion
            nodo = impresion.ObtenerNodo(this.xml, nombre);

            // obtiene la configuracion de la impresora a utilizar
            configuracion = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

            // carga la lista de parametros a imprimir
            CargarParametrosImpresion(1);

            // obtiene la estructura del documento a imprimir
            data = impresion.ObtenerData(listaParamImp, nodo);

            // configura la impresora a utilizar 
            impresion.configurarImpresora(configuracion);

            impresion.imprimir(Define.ID_PRINTER_VOUCHER, data);
        }

        /// <summary>
        /// Metodo que obtiene el nombre de la operacion a realizar
        /// <param name="vez">flag que indica si se imprimira un ticket=0 o un voucher=1</param>
        /// </summary>
        private string obtenerOperacion(int vez)
        {
            string codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETVOUCHER;

            // recorrer la lista parametros de configuracion para obtener la lista de los codigos de monedas
            string[] listaCodigoMonedas = null;
            IDictionaryEnumerator iteraccion = this.listaParamConfig.GetEnumerator();
            while (iteraccion.MoveNext())
            {
                if (iteraccion.Key.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                {
                    listaCodigoMonedas = ((string)iteraccion.Value).Split(',');
                    break;
                }
            }
            if (listaCodigoMonedas == null || listaCodigoMonedas.Length == 0)
            {
                throw new Exception("La lista de Parametros Configuración no tiene las lista de Codigos Monedas.");
            }


            // buscar el codigo de moneda elegido por el usuario
            int indice;
            for (int i = 0; i < listaCodigoMonedas.Length; i++)
            {
                indice = listaCodigoMonedas[i].IndexOf('-');
                if (listaCodigoMonedas[i].Substring(indice + 1, listaCodigoMonedas[i].Length - indice - 1).Equals(this.cbxMoneda.SelectedValue.ToString()))
                {
                    // obtener llave 
                    string llave = listaCodigoMonedas[i].Substring(0, indice);
                    if (llave.Equals("0"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKERSOLES;

                        }

                    }
                    else if (llave.Equals("1"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKERDOLARES;

                        }


                    }
                    else if (llave.Equals("2"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKEREUROS;
                        }

                    }

                    break;
                }
            }

            return codigoMoneda;
        }

        private void Imprimir()
        {
            bool flgErrorImprimir = false;
            string sMensaError = "";

            try
            {
                // 1ro: ************************************** Imprimir STICKER **************************************

                // obtener el nombre de la operacion venta ticket stickers soles[/dolares/euros 
                String nombre = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKER; //obtenerOperacion(0);
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);
                // obtiene la configuracion de la impresora a utilizar
                String configImpSticker = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoSticker != null && !Property.puertoSticker.Trim().Equals(String.Empty))
                {
                    configImpSticker = Property.puertoSticker.Trim() + "," + configImpSticker.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion(0);

                // obtiene la data a imprimir con la impresora de sticker y guardarla en una variable
                String dataSticker = impresion.ObtenerDataFormateada(listaParamImp, nodo);

                // 2do: ************************************** Imprimir VOUCHER **************************************

                // obtener el nombre de la operacion venta ticket voucher soles/dolares/euros 
                nombre = Define.ID_PRINTER_DOCUM_VENTATICKETSMASIVACONTADO; //obtenerOperacion(1);
                // obtiene el nodo segun el nombre de la operacion
                nodo = impresion.ObtenerNodo(this.xml, nombre);
                // obtiene la configuracion de la impresora a utilizar
                String configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion(1);

                // obtiene la estructura del documento a imprimir
                String dataVoucher = impresion.ObtenerDataFormateada(listaParamImp, nodo);
                //-------
                //int copias = impresion.ObtenerCopiasVoucher(nodo);

                String xmlFormatoVoucher = nodo.OuterXml;

                String[] parametros = new String[]
                    {
                      "flagImpSticker=1",
                      "flagImpVoucher=1",
                      "configImpSticker=" + configImpSticker,
                      "configImpVoucher=" + configImpVoucher,
                      "dataSticker=" + dataSticker,
                      "dataVoucher=" + dataVoucher,
                      //"copiasVoucher=" + copias,
                      "xmlFormatoVoucher=" + xmlFormatoVoucher,
                      "listaCodigoTickets=" + Lista_Tickets
                    };

                frmPrintNet frmPrintNet = new frmPrintNet(parametros, listaParamImp);
                frmPrintNet.ShowDialog(this);

                String resultado = frmPrintNet.Resultado;

                if (resultado.Contains("Impresion"))
                {
                    char[] sep = { ',' };
                    Num_Tickets_Impresos = Int32.Parse(resultado.Split(sep)[1].ToString());
                    Property.puertoSticker = frmPrintNet.PuertoSerial.getConfigImpSticker()[0];
                    Property.puertoVoucher = frmPrintNet.PuertoSerial.getConfigImpVoucher()[0];

                    //this.lblVueltoInter.Text = objBOOpera.Imp_VueltoIzq;
                    //this.lblVueltoNac.Text = objBOOpera.Imp_VueltoDer;
                    ActualizarFavoritos();
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
                }
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
            }
        }




        /// <summary>
        /// Metodo que carga la lista de parametros a imprimir.
        /// </summary>
        private void CargarParametrosImpresion(int flag)
        {
            // limpiar lista de parametros a imprimir
            listaParamImp.Clear();
            // si la impresion es de sticker
            if (flag == 0)
            {
                string strDscPsjero = Property.htListaCampos[Define.CAMPO_TIPOPSJERO + objTipoTicket.STipPasajero].ToString();
                //listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Tickets.ToString());
                listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, Can_Tickets.ToString());

                //--EAG
                Fec_Vencimiento = Fec_Vencimiento.Substring(6, 2) + "/" + Fec_Vencimiento.Substring(4, 2) + "/" + Fec_Vencimiento.Substring(0, 4);
                //--EAG

                // recorre cada codigo de ticket
                for (int i = 0; i < Can_Tickets; i++)
                {
                    // fecha de vencimiento
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i, Fec_Vencimiento);

                    // codigo de ticket
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, Lista_Tickets.Substring(i * Convert.ToInt32((string)Property.htParametro[Define.ID_PARAM_LONG_TICKET]), Convert.ToInt32((string)Property.htParametro[Define.ID_PARAM_LONG_TICKET])));

                    // monto 
                    listaParamImp.Add(Define.ID_PRINTER_PARAM_MONTO_PAGADO + "_" + i, Function.FormatDecimal(objTipoTicket.DImpPrecio));

                    //EAG 29/12/2009
                    listaParamImp.Add("desc_moneda_1" + "_" + i, "");
                    listaParamImp.Add("desc_moneda_2" + "_" + i, objTipoTicket.Dsc_Moneda);
                    //EAG
                    listaParamImp.Add("dsc_pasajero_" + i, strDscPsjero);
                }

            }
            // si la impresion es de voucher
            else
            {
                // nombre del cajero
                listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario);

                //---------------------
                listaParamImp.Add("compania", objAerolinea.SDscCompania);
                listaParamImp.Add("nrovuelo", Num_Vuelo);

                //---------------------EAG 10/02/2010 ------------
                String forma_pago = Property.htListaCampos[Define.CAMPO_TIPOPAGO + Tip_Pago].ToString();
                listaParamImp.Add("forma_pago", forma_pago);
                //---------------------

                this.listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, this.objTurno.SCodTurno);

                // descripcion del tipo de ticket
                listaParamImp.Add(Define.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET, objTipoTicket.SNomTipoTicket);

                // cantidad de tickets vendidos
                listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, nudCantidad.Value.ToString());

                // precio unitrio del ticket
                listaParamImp.Add(Define.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET, Function.FormatDecimal(objTipoTicket.DImpPrecio));// + " " + objTipoTicket.Dsc_Simbolo);

                // total a pagar
                listaParamImp.Add(Define.ID_PRINTER_PARAM_TOTAL_PAGAR, Function.FormatDecimal(Imp_TotPagBase, Define.NUM_DECIMAL));// + " " + lblSimbInter.Text);

                // monto pagado
                listaParamImp.Add("monto_pagado_primero", Function.FormatDecimal(decimal.Parse(txtMonto.Text)));// + " " + lblSimbInter.Text);
                //Guardando parametros para Recalculo
                listaParamImp.Add("objTipoTicket", objTipoTicket);
                listaParamImp.Add("Imp_TCPagado", Imp_TCPagado);
                listaParamImp.Add("Imp_TasaCambio", objTasaCambio.DImpCambioActual);
                listaParamImp.Add("Imp_MontoPagado", Imp_Monto);
                listaParamImp.Add("Imp_TCParche", Imp_TCParche);
                listaParamImp.Add("Mon_Pagado", cbxMoneda.SelectedValue.ToString());
                listaParamImp.Add("Can_Tickets_Ini", Can_Tickets);
                listaParamImp.Add("listaCambio", listaCambio);
                listaParamImp.Add("Tip_Pago", Tip_Pago);
                listaParamImp.Add("Imp_MontoSol", Imp_MontoSol);
                listaParamImp.Add("Dsc_MonPago", ((Moneda)cbxMoneda.SelectedItem).SDscMoneda);
                listaParamImp.Add("Dsc_MonSimbolo", ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo);
                listaParamImp.Add("Lista_Tasas", Lista_Tasas);
                listaParamImp.Add("Lista_Flujo_Importe", Lista_Flujo_Importe);
                listaParamImp.Add("Flg_Centavo", Flg_Centavo);
                listaParamImp.Add("Cod_Moneda_Dol", Cod_Moneda_Dol);
                listaParamImp.Add("listaMonedas", listaMonedas);
                listaParamImp.Add("Imp_TotPagxMoneda", Imp_TotPagxMoneda);
                //Fin Guardando parametros para Recalculo
                //EAG 30/12/2009
                listaParamImp.Add("simbolo_moneda_primero", ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo);
                listaParamImp.Add("tip_pago_primero", ObtenerDescripcionPagoLiquidacion());
                listaParamImp.Add("simbolo_tuua", objTipoTicket.Dsc_Simbolo);

                //EAG 29/01/2010
                // vuelto
                FillVoucherVuelto();
                //EAG 29/01/2010

                //-- EAG 10/02/2010
                int cantidadTickets = (Int32)nudCantidad.Value;
                int q1 = cantidadTickets / 2;
                int q2 = cantidadTickets % 2;
                if (q2 != 0)
                {
                    q1 = q1 + 1;
                }
                listaParamImp[Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL] = q1;//Para que soporte 2 columnas de Codigo de Tickets

                //listaParamImp[Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL] = txtCantidad.Text;
                int intLongTicket = Define.Longitud_Ticket;
                int contador = 0;
                // recorre cada codigo de ticket
                for (int i = 0, j = 0; i < (Int32)nudCantidad.Value; i++)
                {

                    /*
                    // codigo de ticket
                    listaParamImp[Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i] = Lista_Tickets.Substring(contador, intLongTicket);
                    contador += intLongTicket;
                    */

                    if ((i + 1) % 2 == 0)//Par
                    {
                        listaParamImp["codigo_ticket_par" + "_" + j] = Lista_Tickets.Substring(contador, intLongTicket);
                        contador += intLongTicket;
                        j++;
                    }
                    else//Impar
                    {
                        listaParamImp["codigo_ticket_impar" + "_" + j] = Lista_Tickets.Substring(contador, intLongTicket);
                        contador += intLongTicket;
                    }


                }
                //cantidad de Tickets ya fue seteado.
                //-- EAG 10/02/2010


                //******************* COMPRA/VENTA MONEDA INTERNACIONAL
                switch (listaCambio.Count)
                {
                    case 0:
                        listaParamImp.Add("CV_1", "0");
                        listaParamImp.Add("CV_2", "0");
                        listaParamImp.Add("CV_3", "0");
                        listaParamImp.Add("CV_4", "0");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        break;
                    case 1:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "0");
                        listaParamImp.Add("CV_3", "0");
                        listaParamImp.Add("CV_4", "0");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        FillCompraVenta(0);

                        break;
                    case 2:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "1");
                        listaParamImp.Add("CV_3", "0");
                        listaParamImp.Add("CV_4", "0");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        FillCompraVenta(0);
                        FillCompraVenta(1);
                        break;
                    case 3:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "1");
                        listaParamImp.Add("CV_3", "1");
                        listaParamImp.Add("CV_4", "0");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        FillCompraVenta(0);
                        FillCompraVenta(1);
                        FillCompraVenta(2);
                        break;
                    case 4:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "1");
                        listaParamImp.Add("CV_3", "1");
                        listaParamImp.Add("CV_4", "1");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        FillCompraVenta(0);
                        FillCompraVenta(1);
                        FillCompraVenta(2);
                        FillCompraVenta(3);
                        break;
                    case 5:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "1");
                        listaParamImp.Add("CV_3", "1");
                        listaParamImp.Add("CV_4", "1");
                        listaParamImp.Add("CV_5", "1");
                        listaParamImp.Add("CV_6", "0");
                        FillCompraVenta(0);
                        FillCompraVenta(1);
                        FillCompraVenta(2);
                        FillCompraVenta(3);
                        FillCompraVenta(4);
                        break;
                    case 6:
                        listaParamImp.Add("CV_1", "1");
                        listaParamImp.Add("CV_2", "1");
                        listaParamImp.Add("CV_3", "1");
                        listaParamImp.Add("CV_4", "1");
                        listaParamImp.Add("CV_5", "1");
                        listaParamImp.Add("CV_6", "1");
                        FillCompraVenta(0);
                        FillCompraVenta(1);
                        FillCompraVenta(2);
                        FillCompraVenta(3);
                        FillCompraVenta(4);
                        FillCompraVenta(5);
                        break;
                    default:
                        listaParamImp.Add("CV_1", "0");
                        listaParamImp.Add("CV_2", "0");
                        listaParamImp.Add("CV_3", "0");
                        listaParamImp.Add("CV_4", "0");
                        listaParamImp.Add("CV_5", "0");
                        listaParamImp.Add("CV_6", "0");
                        break;
                }
                //******************* COMPRA/VENTA MONEDA INTERNACIONAL


                //JCISNEROS DEBE COMPLETAR -----
                if (Imp_MontoSol != 0 || Imp_Dialogo_Pagado != 0)
                {
                    forma_pago = Property.htListaCampos[Define.CAMPO_TIPOPAGO + Define.TIP_PAGO_EFECTIVO].ToString();
                    decimal decImpPag = Imp_MontoSol + Imp_Dialogo_Pagado;
                    string strSimMoneda = Imp_MontoSol != 0 ? ObtenerMoneda(Cod_Moneda_Sol).SDscSimbolo : ObtenerMoneda(Cod_Moneda_Dialogo).SDscSimbolo;
                    listaParamImp.Add("monto_pagado_segundo", Function.FormatDecimal(decImpPag * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL));

                    //eochoa
                    //reemplazo en vez de esta linea      listaParamImp.Add("simbolo_moneda_segundo", strSimMoneda);
                    //15/03/11 Parche agregado por la venta y vuelto (pago con tarjeta credito + efectivo ) en la misma moneda extranjera pero diferente de soles y dolares
                    string strMonPago = Imp_Dialogo_Pagado == 0 ? this.cbxMoneda.SelectedValue.ToString() : Cod_Moneda_Dialogo;
                    strMonPago = chkSol.Checked ? Cod_Moneda_Sol : strMonPago;
                    if (Cod_Moneda_Sol.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()) || Cod_Moneda_Dol.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()))
                        listaParamImp.Add("simbolo_moneda_segundo", strSimMoneda);
                    //fin parche
                    listaParamImp.Add("tip_pago_segundo", Tip_Pago != Define.TIP_PAGO_EFECTIVO ? forma_pago : "");
                }
                else
                {
                    listaParamImp.Add("monto_pagado_segundo", "");
                }

            }

        }

        private void FillVoucherVuelto()
        {
            string strMonPago = Imp_Dialogo_Pagado == 0 ? this.cbxMoneda.SelectedValue.ToString() : Cod_Moneda_Dialogo;
            strMonPago = chkSol.Checked ? Cod_Moneda_Sol : strMonPago;

            if (Cod_Moneda_Sol.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()) || Cod_Moneda_Dol.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()))
            {
                listaParamImp.Add("moneda_internacional", "");
                if (Cod_Moneda_Sol.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()))
                {
                    listaParamImp.Add("vuelto_Nac", Function.FormatDecimal(decimal.Parse(lblVueltoInter.Text)));
                    listaParamImp.Add("vuelto_Dol", Function.FormatDecimal(decimal.Parse(lblVueltoDol.Text)));
                }
                else
                {
                    listaParamImp.Add("vuelto_Nac", Function.FormatDecimal(decimal.Parse(lblVueltoNac.Text)));
                    listaParamImp.Add("vuelto_Dol", Function.FormatDecimal(decimal.Parse(lblVueltoInter.Text)));
                }
            }
            else
            {
                listaParamImp.Add("simbolo_moneda_segundo", ObtenerMoneda(strMonPago).SDscSimbolo);

                listaParamImp.Add("moneda_internacional", ObtenerMoneda(strMonPago).SDscMoneda);
                listaParamImp.Add("vuelto_Nac", Function.FormatDecimal(decimal.Parse(lblVueltoNac.Text)));

                listaParamImp.Add("vuelto_Int", Function.FormatDecimal(decimal.Parse(lblVueltoInter.Text)));
                listaParamImp.Add("vuelto_Dol", Function.FormatDecimal(decimal.Parse(lblVueltoDol.Text)));
            }
        }

        private void FillCompraVenta(int i)
        {

            //Compra o Venta
            if (listaCambio[i].Tip_Cambio.Equals(Define.COMPRA_MONEDA))
                listaParamImp.Add("tipo_compraventa_" + (i + 1), "Compra");
            else
                listaParamImp.Add("tipo_compraventa_" + (i + 1), "Venta");
            listaParamImp.Add("Numero_Operacion_" + (i + 1), listaCambio[i].Num_Operacion);
            listaParamImp.Add("moneda_internacional_" + (i + 1), listaCambio[i].Dsc_MonedaInt);
            listaParamImp.Add("monto_inter_" + (i + 1), listaCambio[i].Imp_MontoInt);
            listaParamImp.Add("simbolo_mon_internacional_" + (i + 1), listaCambio[i].Dsc_SimboloInt);
            listaParamImp.Add("monto_soles_" + (i + 1), listaCambio[i].Imp_MontoNac);
            listaParamImp.Add("tipo_cambio_" + (i + 1), listaCambio[i].Imp_TasaCambio);
            listaParamImp.Add("tipo_pago_" + (i + 1), Property.htListaCampos[Define.CAMPO_TIPOPAGO + listaCambio[i].Tip_Pago].ToString());
        }

        private void CargarVuelos()
        {
            objAerolinea.SCodCompania = this.cbxAerolinea.SelectedValue.ToString();
            objAerolinea.SDscCompania = this.cbxAerolinea.SelectedText;
            //string strFecha = this.dtpFecha.Text.Substring(6, 4) + this.dtpFecha.Text.Substring(3, 2) + this.dtpFecha.Text.Substring(0, 2);
            //this.dgvVuelos.DataSource = objBOOpera.ListarVuelosxCompania(objAerolinea.SCodCompania, strFecha, Tip_Vuelo);
            ModoSeleccion();
        }

        private void ModoSeleccion()
        {
            rbSeleccionar.Checked = true;
            rbIngresar.Checked = false;
            rbFavorito.Checked = false;
            this.AeroDataGridViewTextBoxColumn.ReadOnly = true;
            this.numVueloDataGridViewTextBoxColumn.ReadOnly = true;
            this.dscDestinoDataGridViewTextBoxColumn.ReadOnly = true;
            this.horVueloDataGridViewTextBoxColumn.ReadOnly = true;
            this.fchVueloDataGridViewTextBoxColumn.ReadOnly = true;
            this.dgvVuelos.AllowUserToAddRows = false;
            this.dgvVuelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            objAerolinea.SCodCompania = this.cbxAerolinea.SelectedValue.ToString();
            objAerolinea.SDscCompania = this.cbxAerolinea.SelectedText;
            string strFecha = this.dtpFecha.Text.Substring(6, 4) + this.dtpFecha.Text.Substring(3, 2) + this.dtpFecha.Text.Substring(0, 2);
            this.dgvVuelos.DataSource = objBOOpera.ListarVuelosxCompania(objAerolinea.SCodCompania, strFecha, Tip_Vuelo, Define.MODO_MASIVO_CONT);
        }

        private void ActualizarMontoAPagar()
        {
            try
            {
                Moneda objMoneda = (Moneda)this.cbxMoneda.SelectedItem;
                this.lblSimbInter.Text = objMoneda.SDscSimbolo;
                Imp_TotPagBase = nudCantidad.Value * objTipoTicket.DImpPrecio;
                if (objMoneda.SCodMoneda == objTipoTicket.SCodMoneda)
                {
                    lblTotPagarVal.Text = lblTotPagBaseVal.Text;
                    Imp_TotPagxMoneda = Imp_TotPagBase;
                    txtMonto.Text = this.lblTotPagarVal.Text;
                    return;
                }

                if (objMoneda.SCodMoneda == Cod_Moneda_Sol)
                {
                    Imp_TotPagxMoneda = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
                    lblTotPagarVal.Text = Imp_TotPagxMoneda.ToString();
                    if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                    {
                        txtMonto.Text = this.lblTotPagarVal.Text;
                    }
                    txtMonto.Text = this.lblTotPagarVal.Text;
                    return;
                }

                TasaCambio objTC = this.objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, objMoneda.SCodMoneda != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                Imp_TotPagxMoneda = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / objTC.DImpCambioActual, Define.NUM_DECIMAL);
                if (Imp_TotPagxMoneda == 0)
                {
                    lblTotPagarVal.Text = "0.00";
                }
                else
                {
                    lblTotPagarVal.Text = Imp_TotPagxMoneda.ToString();
                }
                if (Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                {
                    txtMonto.Text = this.lblTotPagarVal.Text;
                }
                txtMonto.Text = this.lblTotPagarVal.Text;
            }
            catch { }
        }

        private void Grabar()
        {
            frmPrincipal.VerificarTurnoActivo();
            string strNumRef = null;
            string strModVenta = (string)Property.htProperty[Define.MOD_VENTA_MAS_CONT];
            bool FlgVentaMasiva = false;
            string sMensaError = "";
            int intTickets = Can_Tickets;
            bool boExito = true;
            string strTicketsAnular = "";

            if (!Validar())
            {
                return;
            }
            if (MessageBox.Show("¿Está seguro de realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (Can_Tickets > Can_Max_Ticket)
            {
                MessageBox.Show((string)LabelConfig.htLabels["masivo.msgMaxTicket"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ExisteTasaVentaMEBase())
            {
                return;
            }
            if (ExisteVariacionTasa())
            {
                return;
            }
            AgregarImporteCaja(Cod_Moneda_Dialogo, Define.TIP_PAGO_EFECTIVO, "+", Imp_Dialogo_Pagar);
            JuntarListaCambio();
            //INI problema acumulacion de operaciones
            if (listaCambio.Count > 6)
            {
                Limpiar();
                return;
            }
            //FIN
            JuntarListaTasa();
            AgregarFlujoImpVuelto();
            try
            {
                objBOError = new BO_Error();

                strNumRef = txtNumReferencia.Text;
                objVentaMasiva = new VentaMasiva();
                objVentaMasiva.DCanVenta = Can_Tickets;
                objVentaMasiva.Tip_Pago = Tip_Pago;
                objVentaMasiva.SCodUsuario = objUsuario.SCodUsuario;
                objVentaMasiva.SCodCompania = objAerolinea.SCodCompania;
                objVentaMasiva.SCodMoneda = this.cbxMoneda.SelectedValue.ToString();
                if (objBOOpera.RegistrarVentaMasiva(Lista_Flujo_Importe, ObtenerImporteDialogoIngresado(), strModVenta, objVentaMasiva, objTurno.SCodTurno, objTipoTicket, Imp_Monto, strNumRef, objTasaCambio.DImpCambioActual, Num_Vuelo, Fec_Vuelo, Tip_Pago, txtCia.Text, txtRepte.Text, Imp_MontoSol, listaCambio, ref Fec_Vencimiento, ref Lista_Tickets, ref Imp_TCPagado))
                {
                    boExito = false;
                    Num_Tickets_Impresos = 0;
                    ValidarCambios();
                    objBOOpera.CrearCodigoOperacion(listaCambio, this.objTurno.SCodTurno, this.objUsuario.SCodUsuario);
                    Imprimir();
                    boExito = true;
                    if (Num_Tickets_Impresos < Can_Tickets)
                    {
                        Num_Tickets_Impresos = Tip_Pago == Define.TIP_PAGO_EFECTIVO ? Num_Tickets_Impresos : 0;
                        strTicketsAnular = Lista_Tickets.Substring(Num_Tickets_Impresos * Long_Ticket);
                        objBOOpera.AnularTuua(strTicketsAnular, Can_Tickets - Num_Tickets_Impresos);
                    }
                    if (Num_Tickets_Impresos > 0)
                    {
                        if (Num_Tickets_Impresos < Can_Tickets)
                        {
                            //RecalculoCambio(Num_Tickets_Impresos);
                            ValidarCambios();
                            listaCambio = (List<Cambio>)Property.htParametro["listaCambio"];
                            Lista_Flujo_Importe = (Hashtable)Property.htParametro["Lista_Flujo_Importe"];
                        }
                        objBOOpera.ActualizarMontosTurno(Lista_Flujo_Importe, this.cbxMoneda.SelectedValue.ToString(), Imp_IngresoInt, Imp_IngresoNac, Imp_EgresoNac);
                        objBOOpera.RegistrarCambio(listaCambio, this.objTurno.SCodTurno, this.objUsuario.SCodUsuario);
                        MessageBox.Show((string)LabelConfig.htLabels["normal.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    Limpiar();
                }
                else
                {
                    FlgVentaMasiva = true;
                    sMensaError = objBOOpera.Dsc_Message;
                    MessageBox.Show(objBOOpera.Dsc_Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                if (!boExito)
                {
                    strTicketsAnular = Lista_Tickets.Substring(Num_Tickets_Impresos * Long_Ticket);
                    objBOOpera.AnularTuua(strTicketsAnular, Can_Tickets - Num_Tickets_Impresos);
                }
                FlgVentaMasiva = true;
                sMensaError = ex.Message;
                //objBOError.IsError();
                Flg_Error = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                ErrorHandler.Trace("PROCESO VENTA MASIVA", "TURNO: " + this.objTurno.SCodTurno + " TUUAS SOLICITADOS:" + intTickets.ToString() + "\n" + "TUUAS GENERADOS EN BD:" + Lista_Tickets + "\n" + "TUUAS IMPRESOS:" + Num_Tickets_Impresos.ToString());
                if (FlgVentaMasiva)
                {
                    IPHostEntry IPs = Dns.GetHostByName("");
                    IPAddress[] Direcciones = IPs.AddressList;
                    String IpClient = Direcciones[0].ToString();
                    //GeneraAlarma - Error al vender Tickets Masivos
                    GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000071", "I20", IpClient, "2", "Alerta W0000071", "Error al vender Tickets Masivos, Error: " + sMensaError + ",  Usuario: " + Property.strUsuario + ", Equipo: " + IpClient, Property.strUsuario);
                }

                if (Flg_Error)
                {
                    MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void Masivo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        Grabar();
                        break;
                    default: break;
                }
                if ((e.Control) && (!e.Alt) && (!e.Shift))
                {
                    if (e.KeyCode == Keys.S)
                    {
                        ModoSeleccion();
                    }
                    else if (e.KeyCode == Keys.I)
                    {
                        ModoIngreso();
                    }
                    else if (e.KeyCode == Keys.F)
                    {
                        ModoFavoritos();
                    }
                }
                if ((!e.Control) && (e.Alt) && (!e.Shift))
                {
                    char chrKey = Convert.ToChar(e.KeyCode);
                    if (Convert.ToInt32(chrKey) - 49 >= 0 && Convert.ToInt32(chrKey) - 49 <= 4)
                    {
                        SeleccionarFormaPago(Convert.ToInt32(chrKey) - 49);
                        SeleccionarPagoEfectivo(Convert.ToInt32(chrKey) - 49);
                    }
                }
            }
            catch { }
        }

        private void ModoIngreso()
        {
            List<VueloProgramado> listaVuelosPgr = new List<VueloProgramado>();
            listaVuelosPgr.Add(new VueloProgramado());
            this.dgvVuelos.AllowUserToAddRows = true;
            this.dgvVuelos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.dgvVuelos.DataSource = listaVuelosPgr;
            this.AeroDataGridViewTextBoxColumn.ReadOnly = true;
            this.numVueloDataGridViewTextBoxColumn.ReadOnly = false;
            this.dscDestinoDataGridViewTextBoxColumn.ReadOnly = true;
            this.horVueloDataGridViewTextBoxColumn.ReadOnly = true;
            string strFecha = this.dtpFecha.Text;//.Substring(6, 4) + this.dtpFecha.Text.Substring(3, 2) + this.dtpFecha.Text.Substring(0, 2);
            this.fchVueloDataGridViewTextBoxColumn.ReadOnly = true;

            ModVentaComp objCia = ((ModVentaComp)cbxAerolinea.SelectedItem);
            this.dgvVuelos.Rows[0].Cells[1].Value = objCia.Dsc_Compania;
            this.dgvVuelos.Rows[0].Cells[3].Value = (string)LabelConfig.htLabels["normal.destino"];
            this.dgvVuelos.Rows[0].Cells[5].Value = strFecha;
            this.dgvVuelos.Rows[0].Cells[4].Value = "00:00:00";
            rbIngresar.Checked = true;
            rbSeleccionar.Checked = false;
            rbFavorito.Checked = false;
            dgvVuelos.CurrentCell = dgvVuelos[2, 0];
            dgvVuelos.Select();
            dgvVuelos.Focus();
        }

        private void ModoFavoritos()
        {
            rbIngresar.Checked = false;
            rbSeleccionar.Checked = false;
            rbFavorito.Checked = true;
            dgvFavoritos.Select();
            dgvFavoritos.Focus();
        }

        public override void ActualizarVuelto()
        {
            decimal decMonto = 0;
            decimal decMontoSol = 0;
            string strMonPago = "";
            decimal decTasaTuua;
            decimal decVueltoTotNac = 0;
            decimal decVueltoInter = 0;
            decimal decVueltNac;
            decimal decTCPagado;
            decimal decEgresoNac;
            decimal decNacional;
            //Importes de compra-venta
            decimal decImpNac = 0;
            decimal decImpInt = 0;

            objCambioSol = null;
            Can_Tickets = 0;
            Imp_EgresoNac = 0;
            Imp_IngresoInt = 0;
            Imp_IngresoNac = 0;
            Imp_MontoSol = 0;
            Imp_TCParche = 0;
            listaCambio = new List<Cambio>();
            Lista_Tasas = new Hashtable();
            Lista_Flujo_Importe = new Hashtable();
            InicilizarPropiedadesDialogo();
            if (nudCantidad.Value > 0)
            {
                Can_Tickets = (Int32)nudCantidad.Value;
            }
            else { return; }

            decMonto = LeerMontoIngresado();
            Imp_Monto = decMonto;
            decMontoSol = LeerMontoSolIngresado();
            Imp_MontoSol = decMontoSol;
            strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
            decTasaTuua = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA).DImpCambioActual;
            Lista_Tasas.Remove(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA);
            Lista_Tasas.Add(objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA, decTasaTuua);

            if (strMonPago == objTipoTicket.SCodMoneda && strMonPago != Cod_Moneda_Sol)
            {
                //parche de ultimo momento-errror en tasa cambio compra
                decimal decTCParche = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, Define.TC_COMPRA).DImpCambioActual;
                Imp_TCParche = decTCParche;
                Lista_Tasas.Remove(strMonPago + "#" + Define.TC_COMPRA);
                Lista_Tasas.Add(strMonPago + "#" + Define.TC_COMPRA, decTCParche);
                decTCParche = decTasaTuua;
                if (decMontoSol > 0)
                {
                    decImpInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * Can_Tickets - decMonto, Define.NUM_DECIMAL);
                    decImpNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * Can_Tickets - decMonto) * decTasaTuua, Define.NUM_DECIMAL);
                    AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, decTasaTuua, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac, Tip_Pago);
                    AgregarImporteCaja(Cod_Moneda_Sol, Tip_Pago, "+", decImpNac);

                }
                AgregarImporteCaja(strMonPago, Tip_Pago, "+", decMonto < Imp_TotPagBase ? decMonto : Imp_TotPagBase);

                decNacional = decTCParche * decMonto - decTCParche * objTipoTicket.DImpPrecio * Can_Tickets;
                decVueltoTotNac = decNacional + decMontoSol;
                if (decMontoSol == 0)
                {
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional / decTCParche), Define.NUM_DECIMAL);
                }
                else
                {
                    Imp_MontoSol = decMontoSol;
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional < 0 ? 0 : decNacional / decTCParche), Define.NUM_DECIMAL);
                }

                decVueltNac = decVueltoTotNac - (decVueltoInter * decTCParche);
                decEgresoNac = Function.FormatDecimal(decVueltNac * Define.FACTOR_DECIMAL, 2);
                //Mostrar Vuelto
                MostrarVueltoCentavos(Imp_Monto - Imp_TotPagxMoneda, decVueltoInter, decEgresoNac, Flg_Centavo);
                //Resumen Forma Pago
                CargarLineaResumen(Tip_Pago, ((Moneda)cbxMoneda.SelectedItem).SCodMoneda, decMonto, 1);
                CargarLineaResumen(Tip_Pago, Cod_Moneda_Sol, decMontoSol, 2);
                return;
            }

            if (Cod_Moneda_Sol != strMonPago)
            {
                string strTipCambio = strMonPago != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA;
                TasaCambio objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, strTipCambio);
                decTCPagado = objTCPagado.DImpCambioActual;
                Lista_Tasas.Remove(strMonPago + "#" + strTipCambio);
                Lista_Tasas.Add(strMonPago + "#" + strTipCambio, decTCPagado);
                //OBS-25
                if (decMonto == Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, Define.NUM_DECIMAL))
                {
                    decMonto = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, 3);
                }

                if (objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua < decTCPagado * decMonto)
                {
                    decImpInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua / decTCPagado, Define.NUM_DECIMAL);
                    decImpNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua, Define.NUM_DECIMAL);
                    Imp_IngresoInt = Imp_TotPagxMoneda;
                }
                else
                {
                    decImpInt = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                    decImpNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
                    Imp_IngresoInt = decImpInt;
                }

                AgregarCambio(((Moneda)cbxMoneda.SelectedItem).SCodMoneda, Define.COMPRA_MONEDA, ((Moneda)cbxMoneda.SelectedItem).SDscMoneda, decTCPagado, ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo, decImpInt, decImpNac, Tip_Pago);

                if (objTipoTicket.SCodMoneda != Cod_Moneda_Sol)
                {
                    if (objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua < decTCPagado * decMonto)
                    {
                        decImpInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * Can_Tickets, Define.NUM_DECIMAL);
                        decImpNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua, Define.NUM_DECIMAL);
                    }
                    else
                    {
                        decImpInt = Function.FormatDecimal(decMonto * decTCPagado / decTasaTuua, Define.NUM_DECIMAL);
                        decImpNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
                    }
                    AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, decTasaTuua, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac, Tip_Pago);
                }
                AgregarImporteCaja(strMonPago, Tip_Pago, "+", Imp_IngresoInt);

                if (decMontoSol == 0)
                {
                    decVueltoTotNac = (objTCPagado.DImpCambioActual * decMonto) - (decTasaTuua * objTipoTicket.DImpPrecio * Can_Tickets);
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decVueltoTotNac / objTCPagado.DImpCambioActual), Define.NUM_DECIMAL);
                }
                else
                {
                    if (Cod_Moneda_Sol != objTipoTicket.SCodMoneda)
                    {
                        if (objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua > decTCPagado * decMonto)
                        {
                            decImpInt = Function.FormatDecimal(((objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua) - (decMonto * decTCPagado)) / decTasaTuua, Define.NUM_DECIMAL);
                            decImpNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua) - (decMonto * decTCPagado), Define.NUM_DECIMAL);
                            Imp_IngresoNac = decImpNac;
                            AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, decTasaTuua, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac, Tip_Pago);
                            AgregarImporteCaja(Cod_Moneda_Sol, Tip_Pago, "+", decImpNac);
                        }
                    }
                    else
                    {
                        Imp_IngresoNac = decTasaTuua * objTipoTicket.DImpPrecio * Can_Tickets - objTCPagado.DImpCambioActual * decMonto;
                        Imp_IngresoNac = Imp_IngresoNac > 0 ? Imp_IngresoNac : 0;
                        AgregarImporteCaja(Cod_Moneda_Sol, Tip_Pago, "+", Imp_IngresoNac);
                    }
                    decNacional = objTCPagado.DImpCambioActual * decMonto - decTasaTuua * objTipoTicket.DImpPrecio * Can_Tickets;
                    decVueltoTotNac = decNacional + decMontoSol;
                    Imp_MontoSol = decMontoSol;
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional < 0 ? 0 : decNacional / objTCPagado.DImpCambioActual), Define.NUM_DECIMAL);
                }
                decVueltNac = decVueltoTotNac - (decVueltoInter * objTCPagado.DImpCambioActual);

                decEgresoNac = Function.FormatDecimal(decVueltNac * Define.FACTOR_DECIMAL, 2);
                MostrarVueltoCentavos(Imp_Monto - Imp_TotPagxMoneda, decVueltoInter, decEgresoNac, Flg_Centavo);
            }
            else
            {
                if (objTipoTicket.SCodMoneda != Cod_Moneda_Sol)
                {
                    decImpInt = Function.FormatDecimal(Imp_TotPagxMoneda <= decMonto ? objTipoTicket.DImpPrecio * Can_Tickets : decMonto / decTasaTuua, Define.NUM_DECIMAL);
                    decImpNac = Function.FormatDecimal(Imp_TotPagxMoneda <= decMonto ? objTipoTicket.DImpPrecio * Can_Tickets * decTasaTuua : decMonto, Define.NUM_DECIMAL);
                    AgregarCambio(objTipoTicket.SCodMoneda, Define.VENTA_MONEDA, objTipoTicket.Dsc_Moneda, decTasaTuua, objTipoTicket.Dsc_Simbolo, decImpInt, decImpNac, Tip_Pago);
                }
                Imp_IngresoInt = Imp_TotPagxMoneda <= decMonto ? decTasaTuua * objTipoTicket.DImpPrecio * Can_Tickets : decMonto;
                AgregarImporteCaja(Cod_Moneda_Sol, Tip_Pago, "+", Imp_IngresoInt);
                MostrarVueltoCentavos(Imp_Monto - Imp_TotPagxMoneda, decVueltoTotNac, 0, Flg_Centavo);
            }
            CargarLineaResumen(Tip_Pago, ((Moneda)cbxMoneda.SelectedItem).SCodMoneda, decMonto, 1);
            CargarLineaResumen(Tip_Pago, Cod_Moneda_Sol, decMontoSol, 2);
        }

        private void HabilitarSoles()
        {
            string strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
            if (Cod_Moneda_Sol == strMonPago)
            {
                chkSol.Enabled = false;
                txtMontoSol.Text = "";
                txtMontoSol.Enabled = false;
            }
            else
            {
                if (Tip_Pago == Define.TIP_PAGO_EFECTIVO)
                {
                    chkSol.Checked = false;
                    chkSol.Enabled = true;
                }
            }
        }


        private void chkSol_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSol.Checked)
            {
                txtMontoSol.Enabled = true;
                SetearCheckRadioButtonVuelto(Define.centavos.TODO, ObtenerMoneda(Cod_Moneda_Sol));
                Centavos(Cod_Moneda_Sol);
                SetMontoNacional();
                ActualizarVuelto();
            }
            else
            {
                DeshabilitarMontoSol();
                Centavos(((Moneda)cbxMoneda.SelectedItem).SCodMoneda);
                SetearCheckRadioButtonVuelto(Flg_Centavo, (Moneda)cbxMoneda.SelectedItem);
            }
        }

        private void DeshabilitarMontoSol()
        {
            chkSol.Checked = false;
            txtMontoSol.Text = "";
            Imp_MontoSol = 0;
            txtMontoSol.Enabled = false;
        }

        private void DeshabilitarSoles()
        {
            chkSol.Checked = false;
            chkSol.Enabled = false;
            txtMontoSol.Text = "";
            Imp_MontoSol = 0;
            txtMontoSol.Enabled = false;
        }

        private void txtMontoSol_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            ActualizarVuelto();

            //eochoa 29/03/2011 Validacion del texto de la descripcion de la moneda en soles de vuelto en efectivo
            double monto = txtMontoSol.Text == "" ? 0 : Convert.ToDouble(txtMontoSol.Text);
            if (txtMontoSol.Text == "" || monto == 0)
                rbTodo.Text = (string)LabelConfig.htLabels["normal.rbTodo"] + " " + ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            else
                rbTodo.Text = (string)LabelConfig.htLabels["normal.rbTodo"] + " " + ObtenerMoneda(Cod_Moneda_Sol).SDscMoneda;
        }

        private void txtMontoSol_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            if (txtMonto.Text != "" && nudCantidad.Value > 0)
            {
                decimal decMonto = decimal.Parse(txtMonto.Text);
                if (decMonto >= Imp_TotPagxMoneda)
                {
                    if (Tip_Pago == Define.TIP_PAGO_CREDITO || Tip_Pago == Define.TIP_PAGO_DEBITO)
                    {
                        txtMonto.Text = Imp_TotPagxMoneda.ToString();
                    }
                    else if (Tip_Pago == Define.TIP_PAGO_EFECTIVO)
                    {
                        DeshabilitarSoles();
                    }
                    DesHabilitarTarjetaEfectivo();
                }
                else
                {
                    if (Tip_Pago == Define.TIP_PAGO_EFECTIVO)
                    {
                        HabilitarSoles();
                    }
                    HabilitarTarjetaEfectivo();
                }
            }
            else if (txtMonto.Text == "")
            {
                DeshabilitarSoles();
            }
            ActualizarVuelto();
        }

        private void FiltrarFavoritos()
        {
            List<VueloProgramado> lista = new List<VueloProgramado>();
            for (int i = 0; i < favoritos.Count; i++)
            {
                if (favoritos[i].Tip_Vuelo == Tip_Vuelo)
                {
                    lista.Add(favoritos[i]);
                }
            }
            dgvFavoritos.DataSource = lista;
        }

        private void VueltosCero()
        {
            lblVueltoInter.Text = "0.00";
            lblVueltoNac.Text = "0.00";
        }

        private void LimpiarErrProvider()
        {
            erpCantidad.Clear();
            erpMonto.Clear();
            erpVuelo.Clear();
            erpAero.Clear();
            erpRepte.Clear();
            erpCia.Clear();
        }

        private void ActualizarTasaCambio()
        {
            //Tasa de cambio
            objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA);
            if (objTasaCambio == null)
            {
                MessageBox.Show("No se dispone de tasas de cambio registrados para la moneda del TUUA.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispose();
                return;
            }
        }

        private void RecalculoCambio(int intTicket)
        {
            //decimal decMonto = Imp_Monto;
            //decimal decMontoSol = Imp_MontoSol;
            //string strMonPago = "";
            //string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            //decimal decTasaTuua = objTasaCambio.DImpCambioActual;
            //decimal decTCPagado;

            //Imp_IngresoNac = 0;
            //Imp_IngresoInt = 0;
            //Imp_EgresoNac = 0;
            //objCambioSol = null;
            //listaCambio = new List<Cambio>();

            //strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;


            //if (intTicket > 0 && strMonPago == objTipoTicket.SCodMoneda && strMonPago != strMonNac)
            //{
            //    //parche de ultimo momento-errror en tasa cambio compra
            //    decimal decTCParche = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, Define.TC_COMPRA).DImpCambioActual;
            //    if (Tip_Pago == Define.TIP_PAGO_EFECTIVO)
            //    {
            //        if (objTipoTicket.DImpPrecio * intTicket * decTCParche < Function.FormatDecimal(decTCParche * decMonto, Define.NUM_DECIMAL))
            //        {

            //            Cambio objCambioCM = new Cambio();
            //            objCambioCM.Cod_Moneda = strMonPago;
            //            objCambioCM.Tip_Cambio = Define.COMPRA_MONEDA;
            //            objCambioCM.Dsc_MonedaInt = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            //            objCambioCM.Imp_TasaCambio = decTCParche;
            //            objCambioCM.Dsc_SimboloInt = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            //            objCambioCM.Imp_MontoInt = Function.FormatDecimal(decMonto - (objTipoTicket.DImpPrecio * intTicket) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket)), Define.NUM_DECIMAL);
            //            objCambioCM.Imp_MontoNac = Function.FormatDecimal((decMonto - (objTipoTicket.DImpPrecio * intTicket) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket))) * decTCParche, Define.NUM_DECIMAL);
            //            Imp_EgresoNac = objCambioCM.Imp_MontoNac;
            //            objCambioSol = new Cambio();
            //            objCambioSol.Cod_Moneda = strMonNac;
            //            objCambioSol.Tip_Cambio = Define.COMPRA_MONEDA;
            //            objCambioSol.Dsc_MonedaInt = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            //            objCambioSol.Imp_TasaCambio = decTCParche;
            //            objCambioSol.Dsc_SimboloInt = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            //            if (objTipoTicket.SCodMoneda == strMonNac && strMonPago != strMonNac)
            //            {
            //                objCambioSol.Imp_MontoNac = objTipoTicket.DImpPrecio * intTicket + objCambioCM.Imp_MontoNac;
            //            }
            //            else
            //            {
            //                objCambioSol.Imp_MontoNac = objCambioCM.Imp_MontoNac;
            //            }
            //            objCambioSol.Imp_MontoInt = objCambioSol.Imp_MontoNac;
            //            Imp_IngresoInt = decMonto - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket));
            //            listaCambio.Add(objCambioCM);

            //            Imp_MontoSol = decMontoSol;
            //            return;
            //        }
            //    }
            //}

            //if (intTicket > 0 && Tip_Pago == Define.TIP_PAGO_EFECTIVO)
            //{
            //    if (strMonNac != strMonPago)
            //    {
            //        TasaCambio objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, strMonPago != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
            //        decTCPagado = objTCPagado.DImpCambioActual;

            //        //para voucher cambio moneda
            //        if (strMonPago != objTipoTicket.SCodMoneda)
            //        {
            //            Cambio objCambio = new Cambio();
            //            objCambio.Tip_Cambio = Define.COMPRA_MONEDA;
            //            objCambio.Cod_Moneda = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
            //            objCambio.Dsc_MonedaInt = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            //            objCambio.Imp_TasaCambio = decTCPagado;
            //            objCambio.Dsc_SimboloInt = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            //            objCambio.Imp_TotNac = objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual;
            //            objCambio.Imp_PagadoNac = decTCPagado * decMonto;
            //            objCambio.Flg_Cambio = 1;
            //            if (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual < decTCPagado * decMonto)
            //            {
            //                objCambio.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual / decTCPagado, Define.NUM_DECIMAL);
            //                objCambio.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
            //            }
            //            else
            //            {
            //                objCambio.Imp_MontoInt = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
            //                objCambio.Imp_MontoNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
            //            }
            //            Imp_IngresoInt = objCambio.Imp_MontoInt;

            //            listaCambio.Add(objCambio);

            //            if (objTipoTicket.SCodMoneda != strMonNac)
            //            {
            //                Cambio objCambioVM = new Cambio();
            //                objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
            //                objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
            //                objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //                objCambioVM.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //                objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //                objCambioVM.Flg_Cambio = 1;
            //                if (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual < decTCPagado * decMonto)
            //                {
            //                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket, Define.NUM_DECIMAL);
            //                    objCambioVM.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
            //                }
            //                else
            //                {
            //                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(decMonto * decTCPagado / objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
            //                    objCambioVM.Imp_MontoNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
            //                }
            //                listaCambio.Add(objCambioVM);
            //            }
            //            else//replicar
            //            {
            //                objCambioSol = new Cambio();
            //                objCambioSol.Cod_Moneda = strMonNac;
            //                objCambioSol.Tip_Cambio = Define.VENTA_MONEDA;
            //                objCambioSol.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //                objCambioSol.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //                objCambioSol.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //                objCambioSol.Imp_MontoInt = objTipoTicket.DImpPrecio * intTicket;
            //                objCambioSol.Imp_MontoNac = objTipoTicket.DImpPrecio * intTicket;
            //            }
            //        }

            //        if (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual < Function.FormatDecimal(decTCPagado * decMonto, Define.NUM_DECIMAL))
            //        {
            //            Cambio objCambioCM = new Cambio();
            //            objCambioCM.Cod_Moneda = strMonPago;
            //            objCambioCM.Tip_Cambio = Define.COMPRA_MONEDA;
            //            objCambioCM.Dsc_MonedaInt = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            //            objCambioCM.Imp_TasaCambio = decTCPagado;
            //            objCambioCM.Dsc_SimboloInt = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            //            objCambioCM.Flg_Cambio = 5;
            //            objCambioCM.Imp_Pagado = decMonto;
            //            objCambioCM.Imp_TotNac = objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual;
            //            objCambioCM.Imp_MontoInt = Function.FormatDecimal(decMonto - (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual / decTCPagado) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual / decTCPagado)), Define.NUM_DECIMAL);
            //            objCambioCM.Imp_MontoNac = Function.FormatDecimal((decMonto - (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual / decTCPagado) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual / decTCPagado))) * decTCPagado, Define.NUM_DECIMAL);
            //            Imp_EgresoNac = objCambioCM.Imp_MontoNac;
            //            objCambioSol = new Cambio();
            //            objCambioSol.Cod_Moneda = strMonNac;
            //            objCambioSol.Tip_Cambio = Define.COMPRA_MONEDA;
            //            objCambioSol.Dsc_MonedaInt = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            //            objCambioSol.Imp_TasaCambio = decTCPagado;
            //            objCambioSol.Dsc_SimboloInt = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            //            if (objTipoTicket.SCodMoneda == strMonNac && strMonPago != strMonNac)
            //            {
            //                objCambioSol.Imp_MontoNac = objTipoTicket.DImpPrecio * intTicket + objCambioCM.Imp_MontoNac;
            //            }
            //            else
            //            {
            //                objCambioSol.Imp_MontoNac = objCambioCM.Imp_MontoNac;
            //            }
            //            objCambioSol.Imp_MontoInt = objCambioSol.Imp_MontoNac;
            //            Imp_IngresoInt = decMonto - decimal.Truncate(((objTCPagado.DImpCambioActual * decMonto) - (decTasaTuua * objTipoTicket.DImpPrecio * intTicket)) / objTCPagado.DImpCambioActual);
            //            listaCambio.Add(objCambioCM);
            //        }
            //        else
            //        {
            //            Imp_IngresoInt = decMonto;
            //        }

            //        if (decMontoSol > 0)
            //        {
            //            if (strMonNac != objTipoTicket.SCodMoneda)
            //            {
            //                if (objTipoTicket.DImpPrecio * intTicket * decTasaTuua > decTCPagado * decMonto)
            //                {
            //                    Cambio objCambioVM = new Cambio();
            //                    objCambioSol = new Cambio();
            //                    objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
            //                    objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
            //                    objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //                    objCambioVM.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //                    objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //                    objCambioSol.Cod_Moneda = strMonNac;
            //                    objCambioSol.Tip_Cambio = Define.VENTA_MONEDA;
            //                    objCambioSol.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //                    objCambioSol.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //                    objCambioSol.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //                    if (strMonPago == objTipoTicket.SCodMoneda)
            //                    {
            //                        objCambioVM.Flg_Cambio = 2;
            //                        objCambioVM.Imp_TotPagar = objTipoTicket.DImpPrecio * intTicket;
            //                        objCambioVM.Imp_Pagado = decMonto;
            //                        objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket - decMonto, Define.NUM_DECIMAL);
            //                        objCambioVM.Imp_MontoNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * intTicket - decMonto) * objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
            //                        objCambioSol.Imp_MontoInt = objCambioVM.Imp_MontoNac;
            //                        objCambioSol.Imp_MontoNac = objCambioVM.Imp_MontoNac;
            //                    }
            //                    else
            //                    {
            //                        objCambioVM.Flg_Cambio = 3;
            //                        objCambioVM.Imp_TotNac = objTipoTicket.DImpPrecio * intTicket * decTasaTuua;
            //                        objCambioVM.Imp_PagadoNac = decMonto * decTCPagado;
            //                        objCambioVM.Imp_MontoInt = Function.FormatDecimal(((objTipoTicket.DImpPrecio * intTicket * decTasaTuua) - (decMonto * decTCPagado)) / objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);
            //                        objCambioVM.Imp_MontoNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * intTicket * decTasaTuua) - (decMonto * decTCPagado), Define.NUM_DECIMAL);
            //                        objCambioSol.Imp_MontoInt = objCambioVM.Imp_MontoNac;
            //                        objCambioSol.Imp_MontoNac = objCambioVM.Imp_MontoNac;
            //                    }
            //                    Imp_IngresoNac = objCambioVM.Imp_MontoNac;
            //                    objCambioVM.Imp_Pagado = decMonto;
            //                    listaCambio.Add(objCambioVM);
            //                }
            //            }
            //            else
            //            {
            //                Imp_IngresoNac = decTasaTuua * objTipoTicket.DImpPrecio * intTicket - objTCPagado.DImpCambioActual * decMonto;
            //                Imp_IngresoNac = Imp_IngresoNac > 0 ? Imp_IngresoNac : 0;
            //                if (Imp_IngresoNac > 0)
            //                {
            //                    objCambioSol = new Cambio();
            //                    objCambioSol.Cod_Moneda = strMonNac;
            //                    objCambioSol.Tip_Cambio = Define.COMPRA_MONEDA;
            //                    objCambioSol.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //                    objCambioSol.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //                    objCambioSol.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //                    objCambioSol.Imp_MontoInt = objTCPagado.DImpCambioActual * decMonto;
            //                    objCambioSol.Imp_MontoNac = objTCPagado.DImpCambioActual * decMonto;
            //                }
            //            }
            //            Imp_MontoSol = decMontoSol;
            //        }
            //    }
            //    else
            //    {
            //        if (objTipoTicket.SCodMoneda != strMonNac)
            //        {
            //            Cambio objCambioVM = new Cambio();
            //            objCambioSol = new Cambio();
            //            objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
            //            objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
            //            objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //            objCambioVM.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //            objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //            objCambioVM.Flg_Cambio = 4;
            //            objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket, Define.NUM_DECIMAL);
            //            objCambioVM.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * objTasaCambio.DImpCambioActual, Define.NUM_DECIMAL);

            //            objCambioSol.Cod_Moneda = strMonNac;
            //            objCambioSol.Tip_Cambio = Define.VENTA_MONEDA;
            //            objCambioSol.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
            //            objCambioSol.Imp_TasaCambio = objTasaCambio.DImpCambioActual;
            //            objCambioSol.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
            //            objCambioSol.Imp_MontoInt = objCambioVM.Imp_MontoNac;
            //            objCambioSol.Imp_MontoNac = objCambioVM.Imp_MontoNac;
            //            listaCambio.Add(objCambioVM);
            //        }
            //        Imp_IngresoInt = decTasaTuua * objTipoTicket.DImpPrecio * intTicket;
            //    }
            //}
        }

        private bool ExisteTasaVentaMEBase()
        {
            //Tasa de cambio
            string strMonExt = Property.htProperty["COD_DOLAR"].ToString();
            TasaCambio objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(strMonExt, Define.TC_VENTA);
            if (objTasaCambio == null)
            {
                MessageBox.Show((string)LabelConfig.htLabels["turno.msgTasaDolar"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ValidarCambios()
        {
            for (int i = 0; i < listaCambio.Count; i++)
            {
                listaCambio[i].Imp_MontoInt = Function.FormatDecimal(listaCambio[i].Imp_MontoInt * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                listaCambio[i].Imp_MontoNac = Function.FormatDecimal(listaCambio[i].Imp_MontoNac * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                if (listaCambio[i].Imp_MontoInt == 0)
                {
                    listaCambio.Remove(listaCambio[i--]);
                }
            }
        }

        private void Masivo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    //case Keys.Enter:
                    //    Grabar();
                    //    break;
                    case Keys.Tab:
                        SetTabIndex();
                        break;
                    case Keys.S:
                        if (chkSol.Focused && !chkSol.Checked)
                        {
                            chkSol.Checked = true;
                        }
                        break;
                    case Keys.N:
                        if (chkSol.Focused && chkSol.Checked)
                        {
                            chkSol.Checked = false;
                        }
                        break;
                    default: break;
                }
            }
            catch { }
        }

        private void SetTabIndex()
        {
            switch (Num_TabIndex)
            {
                case 0:
                    this.rbNacional.Select();
                    this.rbNacional.Focus();
                    this.rbNacional.TabIndex = 0;
                    this.rbNacional.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 3:
                    this.nudCantidad.Select();
                    this.nudCantidad.Focus();
                    this.nudCantidad.TabIndex = Num_TabIndex;
                    this.nudCantidad.TabStop = true;
                    /*this.txtCantidad.Select();
                    this.txtCantidad.Focus();
                    this.txtCantidad.TabIndex = Num_TabIndex;
                    this.txtCantidad.TabStop = true;*/
                    Num_TabIndex++;
                    break;
                case 4:
                    this.dtpFecha.Select();
                    this.dtpFecha.Focus();
                    this.dtpFecha.TabIndex = Num_TabIndex;
                    this.dtpFecha.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 5:
                    this.cbxAerolinea.Select();
                    this.cbxAerolinea.Focus();
                    this.cbxAerolinea.TabIndex = Num_TabIndex;
                    this.cbxAerolinea.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 6:
                    SelectVueloByTab();
                    Num_TabIndex++;
                    break;
                case 7:
                    this.dgvVuelos.SelectNextControl(txtCia, true, false, true, false);
                    this.txtCia.Select();
                    this.txtCia.Focus();
                    this.txtCia.TabIndex = Num_TabIndex;
                    this.txtCia.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 8:
                    this.txtRepte.Select();
                    this.txtRepte.Focus();
                    this.txtRepte.TabIndex = Num_TabIndex;
                    this.txtRepte.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 9:
                    this.cbxMoneda.Select();
                    this.cbxMoneda.Focus();
                    this.cbxMoneda.TabIndex = Num_TabIndex;
                    this.cbxMoneda.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 10:
                    this.txtMonto.Select();
                    this.txtMonto.Focus();
                    this.txtMonto.TabIndex = Num_TabIndex;
                    this.txtMonto.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 11:
                    if (chkSol.Enabled)
                    {
                        this.chkSol.Select();
                        this.chkSol.Focus();
                        this.chkSol.TabIndex = Num_TabIndex;
                        this.chkSol.TabStop = true;
                        Num_TabIndex++;
                    }
                    else
                    {
                        Num_TabIndex++;
                        SetTabIndex();
                    }
                    break;
                case 12:
                    if (txtMontoSol.Enabled)
                    {
                        this.txtMontoSol.Select();
                        this.txtMontoSol.Focus();
                        this.txtMontoSol.TabIndex = Num_TabIndex;
                        this.txtMontoSol.TabStop = true;
                        Num_TabIndex++;
                    }
                    else
                    {
                        Num_TabIndex++;
                        SetTabIndex();
                    }
                    break;
                case 13:
                    if (txtNumReferencia.Enabled)
                    {
                        this.txtNumReferencia.Select();
                        this.txtNumReferencia.Focus();
                        this.txtNumReferencia.TabIndex = Num_TabIndex;
                        this.txtNumReferencia.TabStop = true;
                        Num_TabIndex++;
                    }
                    else
                    {
                        Num_TabIndex++;
                        SetTabIndex();
                    }
                    break;
                case 14:
                    this.rbTodo.Select();
                    this.rbTodo.Focus();
                    this.rbTodo.TabIndex = Num_TabIndex;
                    this.rbTodo.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 15:
                    this.btnAceptar.Select();
                    this.btnAceptar.Focus();
                    this.btnAceptar.TabIndex = Num_TabIndex;
                    this.btnAceptar.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 16:
                    this.btnCancelar.Select();
                    this.btnCancelar.Focus();
                    this.btnCancelar.TabIndex = Num_TabIndex;
                    this.btnCancelar.TabStop = true;
                    Num_TabIndex++;
                    break;
                case 17:
                    this.btnLimpiar.Select();
                    this.btnLimpiar.Focus();
                    this.btnLimpiar.TabIndex = Num_TabIndex;
                    this.btnLimpiar.TabStop = true;
                    Num_TabIndex = 0;
                    break;
                default:
                    Num_TabIndex++;
                    break;
            }
        }

        private void SelectVueloByTab()
        {
            this.dgvVuelos.Select();
            this.dgvVuelos.Focus();
            this.dgvVuelos.TabIndex = Num_TabIndex;
            this.dgvVuelos.TabStop = true;
        }

        private void SetMontoNacional()
        {
            decimal decMonto = 0;
            decimal decMontoSol = 0;
            string strMonPago = "";
            decimal decTasaTuua;
            decimal decVueltoTotNac = 0;
            decimal decVueltoInter = 0;
            decimal decVueltNac;
            decimal decTCPagado;
            decimal decEgresoNac;
            decimal decNacional;

            Can_Tickets = 0;
            Imp_TCParche = 0;

            if (nudCantidad.Value > 0)
            {
                Can_Tickets = (Int32)nudCantidad.Value;
            }
            decMonto = LeerMontoIngresado();
            decMontoSol = LeerMontoSolIngresado();

            strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
            decTasaTuua = objBOOpera.ObtenerTasaCambioPorMoneda(objTipoTicket.SCodMoneda, Define.TC_VENTA).DImpCambioActual;

            if (strMonPago == objTipoTicket.SCodMoneda && strMonPago != Cod_Moneda_Sol)
            {
                //parche de ultimo momento-errror en tasa cambio compra
                decimal decTCParche = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, Define.TC_COMPRA).DImpCambioActual;
                if (objTipoTicket.DImpPrecio * Can_Tickets * decTCParche < Function.FormatDecimal(decTCParche * decMonto, Define.NUM_DECIMAL))
                {
                    txtMontoSol.Text = "0.00";
                    Imp_MontoSol = 0;
                    return;
                }
            }

            if (Cod_Moneda_Sol != strMonPago)
            {
                TasaCambio objTCPagado = objBOOpera.ObtenerTasaCambioPorMoneda(strMonPago, strMonPago != objTipoTicket.SCodMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                decTCPagado = objTCPagado.DImpCambioActual;
                //OBS-25
                if (decMonto == Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, Define.NUM_DECIMAL))
                {
                    decMonto = Function.FormatDecimal(Imp_TotPagBase * this.objTasaCambio.DImpCambioActual / decTCPagado, 3);
                }
                decNacional = objTCPagado.DImpCambioActual * decMonto - decTasaTuua * objTipoTicket.DImpPrecio * Can_Tickets;
                decVueltoTotNac = decNacional + decMontoSol;
                decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional < 0 ? 0 : decNacional / objTCPagado.DImpCambioActual), Define.NUM_DECIMAL);

                decVueltNac = decVueltoTotNac - (decVueltoInter * objTCPagado.DImpCambioActual);

                decEgresoNac = Function.FormatDecimal(-1 * decVueltNac * Define.FACTOR_DECIMAL, 2) > 0 ? Function.FormatDecimal(-1 * decVueltNac * Define.FACTOR_DECIMAL, 2) : 0;
                Imp_MontoSol = decEgresoNac;
                txtMontoSol.Text = decEgresoNac == 0 ? "0.00" : decEgresoNac.ToString();
            }
        }

        private void SetearLabels()
        {
            string strMonPago = ((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            rbTodo.Text = (string)LabelConfig.htLabels["normal.rbTodo"] + " " + strMonPago;
            rbDol.Text = (string)LabelConfig.htLabels["normal.rbDol"];
            rbCentavo.Text = (string)LabelConfig.htLabels["normal.rbCentavo"];
        }

        private bool ExisteVariacionTasa()
        {
            string strMoneda = "";
            string strTipCambio = "";
            string strMontoRec = "";
            object[] keys = new object[Lista_Tasas.Count];
            Lista_Tasas.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                strMoneda = keys[i].ToString().Split('#')[0];
                strTipCambio = keys[i].ToString().Split('#')[1];
                TasaCambio objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(strMoneda, strTipCambio);
                if (decimal.Parse(Lista_Tasas[keys[i]].ToString()) != objTasaCambio.DImpCambioActual)
                {
                    MessageBox.Show(strMoneda + "-" + strTipCambio + ": " + Lista_Tasas[keys[i]].ToString() + "(Antiguo)," + objTasaCambio.DImpCambioActual.ToString() + "(Vigente)", (string)LabelConfig.htLabels["normal.msgVarTasa"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ActualizarTasaCambio();
                    strMontoRec = txtMonto.Text;
                    ActualizarMontoAPagar();
                    txtMonto.Text = strMontoRec;
                    ActualizarVuelto();
                    return true;
                }
            }
            return false;
        }

        private void CargarAerolineas()
        {
            //aerolineas
            //DataTable dtCompanias = objBOOpera.ConsultarCompaniaxFiltro(Define.TIPO_AEROLINEA, Define.COMPANIA_ACTIVO, "", "ASC");

            List<ModVentaComp> listaCompania = objBOOpera.ListarCompaniaxModVenta((string)Property.htProperty[Define.MOD_VENTA_MAS_CONT], null);
            if (!(listaCompania != null && listaCompania.Count > 0))
            {
                MessageBox.Show("Compañías NO REGISTRADAS PARA VENTA NORMAL CONTADO.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            ModVentaComp objModVenCia = new ModVentaComp();
            objModVenCia.Dsc_Compania = "-Todos-";
            objModVenCia.SCodCompania = "";
            listaCompania.Insert(0, objModVenCia);
            this.cbxAerolinea.DataSource = listaCompania;
            this.cbxAerolinea.DisplayMember = "Dsc_Compania";
            this.cbxAerolinea.ValueMember = "SCodCompania";
            this.cbxAerolinea.SelectedIndex = 0;
        }

        public override void Centavos(string strMoneda)
        {
            if (strMoneda == "DOL")
            {
                rbDol.Enabled = false;
                rbCentavo.Enabled = true;
            }
            else if (strMoneda == "SOL")
            {
                rbCentavo.Enabled = false;
                rbDol.Enabled = false;
            }
            else
            {
                rbCentavo.Enabled = true;
                rbDol.Enabled = true;
            }
            rbTodo.Checked = true;
        }

        public override void MostrarVueltoCentavos(decimal decVuelTotal, decimal decVuelIzq, decimal decVuelCen, Define.centavos intCentavo)
        {
            decimal decVuelto = 0;
            decimal decTasaCompra;
            decimal decTasaVenta;
            Moneda objMoneda = Imp_Dialogo_Pagado == 0 ? (Moneda)this.cbxMoneda.SelectedItem : ObtenerMoneda(Cod_Moneda_Dialogo);
            Lista_Cambio_Vuelto = new List<Cambio>();
            Lista_Flujo_Vuelto = new Hashtable();
            if (Tip_Pago == Define.TIP_PAGO_CHEQUE || Tip_Pago == Define.TIP_PAGO_TRANSF)
            {
                decVuelTotal = 0;
            }
            switch (intCentavo)
            {
                case Define.centavos.TODO:
                    if (Imp_MontoSol == 0)
                    {
                        lblVueltoInter.Text = Function.FormatDecimal(decVuelTotal * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    }
                    else
                    {
                        lblVueltoInter.Text = decVuelCen == 0 ? "0.00" : Function.FormatDecimal(decVuelCen * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    }
                    lblVueltoNac.Text = "0.00";
                    lblVueltoDol.Text = "0.00";
                    break;
                case Define.centavos.SOL:
                    decTasaCompra = this.objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, Define.TC_COMPRA).DImpCambioActual;
                    decVuelto = (decVuelTotal - decimal.Truncate(decVuelTotal)) * decTasaCompra;

                    lblVueltoInter.Text = decVuelTotal == 0 ? "0.00" : Function.FormatDecimal(decimal.Truncate(decVuelTotal) * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    lblVueltoNac.Text = decVuelto == 0 ? "0.00" : Function.FormatDecimal(decVuelto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();

                    lblVueltoDol.Text = "0.00";
                    Lista_Cambio_Vuelto.Add(AgregarCambioVuelto(objMoneda.SCodMoneda, Define.COMPRA_MONEDA, objMoneda.SDscMoneda, decTasaCompra, objMoneda.SDscSimbolo, decVuelTotal - decimal.Truncate(decVuelTotal), decVuelto, Define.TIP_PAGO_EFECTIVO));
                    AgregarImporteVuelto(objMoneda.SCodMoneda, Define.TIP_PAGO_EFECTIVO, "+", decVuelTotal - decimal.Truncate(decVuelTotal));
                    AgregarImporteVuelto(Cod_Moneda_Sol, Define.TIP_PAGO_EFECTIVO, "-", decVuelto);
                    break;
                case Define.centavos.DOL:
                    decTasaCompra = this.objBOOpera.ObtenerTasaCambioPorMoneda(objMoneda.SCodMoneda, Define.TC_COMPRA).DImpCambioActual;
                    decTasaVenta = this.objBOOpera.ObtenerTasaCambioPorMoneda(Cod_Moneda_Dol, Define.TC_VENTA).DImpCambioActual;
                    decVuelto = ((decVuelTotal - decimal.Truncate(decVuelTotal)) * decTasaCompra) / decTasaVenta;
                    lblVueltoInter.Text = Function.FormatDecimal(decimal.Truncate(decVuelTotal) * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    lblVueltoNac.Text = "0.00";
                    lblVueltoDol.Text = Function.FormatDecimal(decVuelto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                    Lista_Cambio_Vuelto.Add(AgregarCambioVuelto(objMoneda.SCodMoneda, Define.COMPRA_MONEDA, objMoneda.SDscMoneda, decTasaCompra, objMoneda.SDscSimbolo, decVuelTotal - decimal.Truncate(decVuelTotal), (decVuelTotal - decimal.Truncate(decVuelTotal)) * decTasaCompra, Define.TIP_PAGO_EFECTIVO));
                    AgregarImporteVuelto(objMoneda.SCodMoneda, Define.TIP_PAGO_EFECTIVO, "+", decVuelTotal - decimal.Truncate(decVuelTotal));
                    objMoneda = ObtenerMoneda(Cod_Moneda_Dol);
                    Lista_Cambio_Vuelto.Add(AgregarCambioVuelto(objMoneda.SCodMoneda, Define.VENTA_MONEDA, objMoneda.SDscMoneda, decTasaVenta, objMoneda.SDscSimbolo, decVuelto, (decVuelTotal - decimal.Truncate(decVuelTotal)) * decTasaCompra, Define.TIP_PAGO_EFECTIVO));
                    AgregarImporteVuelto(Cod_Moneda_Dol, Define.TIP_PAGO_EFECTIVO, "-", decVuelto);
                    break;
                default:
                    break;
            }
        }

        private void CargarFormaPago()
        {
            //cambios configurable forma de pago
            if (Property.htProperty["DEBITO"].ToString() != Define.TIP_ACTIVO)
            {
                olkbPago.Buttons[2].Enabled = false;
            }
            else
            {
                olkbPago.Buttons[2].Enabled = true;
            }
            if (Property.htProperty["CREDITO"].ToString() != Define.TIP_ACTIVO)
            {
                olkbPago.Buttons[1].Enabled = false;
            }
            else
            {
                olkbPago.Buttons[1].Enabled = true;
            }
            if (Property.htProperty["TRANSFERENCIA"].ToString() != Define.TIP_ACTIVO)
            {
                olkbPago.Buttons[3].Enabled = false;
            }
            else
            {
                olkbPago.Buttons[3].Enabled = true;
            }
            if (Property.htProperty["CHEQUE"].ToString() != Define.TIP_ACTIVO)
            {
                olkbPago.Buttons[4].Enabled = false;
            }
            else
            {
                olkbPago.Buttons[4].Enabled = true;
            }
            //fin configurable forma de pago
        }
        private void olkbPago_Click(object sender, OutlookStyleControls.OutlookBar.ButtonClickEventArgs e)
        {
            int idx = olkbPago.Buttons.IndexOf(e.SelectedButton);
            SeleccionarFormaPago(idx);
        }

        private void SeleccionarFormaPago(int idx)
        {
            InicilizarPropiedadesDialogo();
            switch (idx)
            {
                case 0: // Efectivo
                    Tip_Pago = Define.TIP_PAGO_EFECTIVO;
                    HabilitarContado();
                    break;
                case 1: // Credito
                    Tip_Pago = Define.TIP_PAGO_CREDITO;
                    HabilitarTarjeta();
                    break;
                case 2: // Debito
                    Tip_Pago = Define.TIP_PAGO_DEBITO;
                    HabilitarTarjeta();
                    break;
                case 3: // Transf
                    Tip_Pago = Define.TIP_PAGO_TRANSF;
                    txtMonto.Text = this.lblTotPagarVal.Text;
                    txtMonto.Enabled = false;
                    txtNumReferencia.Enabled = true;
                    break;
                case 4: // Cheque
                    Tip_Pago = Define.TIP_PAGO_CHEQUE;
                    txtMonto.Text = this.lblTotPagarVal.Text;
                    txtMonto.Enabled = false;
                    txtNumReferencia.Enabled = true;
                    break;
                default:
                    break;
            }
            DeshabilitarSoles();
            DesHabilitarTarjetaEfectivo();
            ActualizarVuelto();
        }


        private void LimpiarResumen(int intFlg)
        {
            switch (intFlg)
            {
                case 1:
                    lblFP1.Text = "";
                    lblIP1.Text = "";
                    lblMP1.Text = "";
                    break;
                case 2:
                    lblFP2.Text = "";
                    lblIP2.Text = "";
                    lblMP2.Text = "";
                    break;
                default:
                    lblFP1.Text = "";
                    lblFP2.Text = "";
                    lblIP1.Text = "";
                    lblIP2.Text = "";
                    lblMP1.Text = "";
                    lblMP2.Text = "";
                    break;
            }
        }

        public override void CargarLineaResumen(string strTipPago, string strMoneda, decimal decMonto, int intFlg)
        {
            strTipPago = strTipPago == Define.TIP_PAGO_CREDEFEC ? Define.TIP_PAGO_CREDITO : (strTipPago == Define.TIP_PAGO_DEBIEFEC ? Define.TIP_PAGO_DEBITO : strTipPago);
            string strDscTipPago = Property.htListaCampos[Define.CAMPO_TIPOPAGO + strTipPago].ToString().Substring(0, 4);
            if (decMonto == 0)
            {
                LimpiarResumen(intFlg);
                return;
            }
            if (intFlg == 1)
            {
                lblFP1.Text = strDscTipPago;
                lblIP1.Text = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                lblMP1.Text = strMoneda;
            }
            else
            {
                lblFP2.Text = strDscTipPago;
                lblIP2.Text = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL).ToString();
                lblMP2.Text = strMoneda;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal decRestante = ObtenerMontoRestante();
            Imp_Dialogo_Restante = decRestante;
            Efectivo frmEfectivo = new Efectivo(this, listaMonedas, objTipoTicket.SCodMoneda, decRestante, 0);
            frmEfectivo.ShowDialog(this);
        }

        private void HabilitarTarjetaEfectivo()
        {
            if ((Tip_Pago == Define.TIP_PAGO_CREDEFEC || Tip_Pago == Define.TIP_PAGO_DEBIEFEC || Tip_Pago == Define.TIP_PAGO_CREDITO || Tip_Pago == Define.TIP_PAGO_DEBITO) && txtMonto.Text != "")
            {
                btnAgregar.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = false;
            }
        }

        private void DesHabilitarTarjetaEfectivo()
        {
            btnAgregar.Enabled = false;
        }

        public override void SetearCheckRadioButtonVuelto(Define.centavos intCentavo, Moneda objMoneda)
        {
            switch (intCentavo)
            {
                case Define.centavos.TODO:
                    rbTodo.Text = (string)LabelConfig.htLabels["normal.rbTodo"] + " " + objMoneda.SDscMoneda;
                    lblSimbInter.Text = objMoneda.SDscSimbolo;
                    rbTodo.Checked = true;
                    break;
                case Define.centavos.SOL:
                    rbCentavo.Enabled = false;
                    rbDol.Enabled = false;
                    break;
                case Define.centavos.DOL:
                    rbDol.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private decimal ObtenerVueltoTotal()
        {
            decimal decVuelTotal = 0;
            decVuelTotal = Imp_Dialogo_Pagado == 0 ? Imp_Monto - Imp_TotPagxMoneda : Imp_Dialogo_Pagado - Imp_Dialogo_Pagar;
            return decVuelTotal;
        }

        private void rbTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTodo.Checked)
            {
                Flg_Centavo = Define.centavos.TODO;
                if (EsTarjetaEfectivo())
                {
                    MostrarVueltoCentavos(Imp_Dialogo_Pagado - Imp_Dialogo_Pagar, 0, 0, Flg_Centavo);
                }
                else
                {
                    ActualizarVuelto();
                }
            }
        }

        private void rbCentavo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCentavo.Checked)
            {
                Flg_Centavo = Define.centavos.SOL;
                if (EsTarjetaEfectivo())
                {
                    MostrarVueltoCentavos(Imp_Dialogo_Pagado - Imp_Dialogo_Pagar, 0, 0, Flg_Centavo);
                }
                else
                {
                    ActualizarVuelto();
                }
            }
        }

        private void rbDol_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDol.Checked)
            {
                Flg_Centavo = Define.centavos.DOL;
                if (EsTarjetaEfectivo())
                {
                    MostrarVueltoCentavos(Imp_Dialogo_Pagado - Imp_Dialogo_Pagar, 0, 0, Flg_Centavo);
                }
                else
                {
                    ActualizarVuelto();
                }
            }
        }

        private bool EsTarjetaEfectivo()
        {
            return Imp_Dialogo_Pagado + Imp_Dialogo_Pagar != 0 ? true : false;
        }

        private void InicilizarPropiedadesDialogo()
        {
            Imp_Dialogo_Pagado = 0;
            Imp_Dialogo_Pagar = 0;
            Imp_Dialogo_Restante = 0;
            Lista_Flujo_Importe = new Hashtable();
            Lista_Dialogo_Cambio = new List<Cambio>();
            Lista_Flujo_Vuelto = new Hashtable();
        }

        private void SetearDefaultVuelto()
        {
            rbTodo.Checked = true;
            Flg_Centavo = Define.centavos.TODO;
        }

        private void AgregarCambio(string strMonPago, string strTipCambio, string strDscMonInt, decimal decTasa, string strSimbInt, decimal decImpInt, decimal decImpNac, string strTipPago)
        {
            if (decImpInt > 0)
            {
                Cambio objCambio = new Cambio();
                objCambio.Cod_Moneda = strMonPago;
                objCambio.Tip_Cambio = strTipCambio;
                objCambio.Dsc_MonedaInt = strDscMonInt;
                objCambio.Imp_TasaCambio = decTasa;
                objCambio.Dsc_SimboloInt = strSimbInt;
                objCambio.Tip_Pago = strTipPago;
                objCambio.Flg_TarjetaEfec = false;
                objCambio.Imp_MontoInt = Function.FormatDecimal(decImpInt * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                objCambio.Imp_MontoNac = Function.FormatDecimal(decImpNac * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                listaCambio.Add(objCambio);
            }
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

        private decimal LeerMontoSolIngresado()
        {
            decimal decMontoSol;
            try
            {
                decMontoSol = decimal.Parse(txtMontoSol.Text);
            }
            catch
            {
                decMontoSol = 0;
            }
            return decMontoSol;
        }

        private Moneda ObtenerMoneda(string strMoneda)
        {
            for (int i = 0; i < listaMonedas.Count; i++)
            {
                if (listaMonedas[i].SCodMoneda == strMoneda)
                {
                    return listaMonedas[i];
                }
            }
            return null;
        }

        private void JuntarListaCambio()
        {
            for (int i = 0; i < Lista_Dialogo_Cambio.Count; i++)
            {
                listaCambio.Add(Lista_Dialogo_Cambio[i]);
            }
            for (int i = 0; i < Lista_Cambio_Vuelto.Count; i++)
            {
                listaCambio.Add(Lista_Cambio_Vuelto[i]);
            }
        }

        private void JuntarListaTasa()
        {
            if (!(Lista_Dialogo_Tasa != null && Lista_Dialogo_Tasa.Count > 0))
            {
                return;
            }
            object[] keys = new object[Lista_Dialogo_Tasa.Count];
            Lista_Dialogo_Tasa.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                if (Lista_Tasas[keys[i]] == null)
                {
                    Lista_Tasas.Add(keys[i], Lista_Dialogo_Tasa[keys[i]]);
                }
                else
                {
                    Lista_Tasas[keys[i]] = Lista_Dialogo_Tasa[keys[i]];
                }
            }
        }

        private void AgregarImporteCaja(string strMoneda, string strTipPago, string strTipoOpe, decimal decImporte)
        {
            if (decImporte == 0)
            {
                return;
            }
            if (Lista_Flujo_Importe[strTipoOpe + "#" + strTipPago + "#" + strMoneda] == null)
            {
                Lista_Flujo_Importe.Add(strTipoOpe + "#" + strTipPago + "#" + strMoneda, decImporte);
            }
            else
            {
                Lista_Flujo_Importe[strTipoOpe + "#" + strTipPago + "#" + strMoneda] = decImporte;
            }
        }

        private decimal ObtenerImporteDialogoIngresado()
        {
            decimal decTasaVenta = 0;
            decimal decTasaCompra = 0;
            if (Cod_Moneda_Dialogo == objTipoTicket.SCodMoneda || Imp_Dialogo_Pagado == 0)
            {
                return Imp_Dialogo_Pagado;
            }
            if (Cod_Moneda_Dialogo == Cod_Moneda_Sol)
            {
                decTasaVenta = decimal.Parse(Lista_Tasas[objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA].ToString());
                return Imp_Dialogo_Pagado / decTasaVenta;
            }
            if (objTipoTicket.SCodMoneda == Cod_Moneda_Sol)
            {
                decTasaCompra = decimal.Parse(Lista_Tasas[Cod_Moneda_Dialogo + "#" + Define.TC_COMPRA].ToString());
                return Imp_Dialogo_Pagado * decTasaCompra;
            }
            if (Cod_Moneda_Dialogo != objTipoTicket.SCodMoneda)
            {
                decTasaCompra = decimal.Parse(Lista_Tasas[Cod_Moneda_Dialogo + "#" + Define.TC_COMPRA].ToString());
                decTasaVenta = decimal.Parse(Lista_Tasas[objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA].ToString());
                return (Imp_Dialogo_Pagado * decTasaCompra) / decTasaVenta;
            }
            return 0;
        }

        public override void ConvertirTarjetaContado()
        {
            object[] keys = new object[Lista_Flujo_Importe.Count];
            decimal decImporte = 0;
            Lista_Flujo_Importe.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                string[] arrFlujo = keys[i].ToString().Split('#');
                decImporte = decimal.Parse(Lista_Flujo_Importe[keys[i]].ToString());
                switch (arrFlujo[1])
                {
                    case Define.TIP_PAGO_CREDITO:
                        Lista_Flujo_Importe.Remove(keys[i]);
                        Lista_Flujo_Importe.Add(arrFlujo[0] + "#" + Define.TIP_PAGO_CREDEFEC + "#" + arrFlujo[2], decImporte);
                        Tip_Pago = Define.TIP_PAGO_CREDEFEC;
                        break;
                    case Define.TIP_PAGO_DEBITO:
                        Lista_Flujo_Importe.Remove(keys[i]);
                        Lista_Flujo_Importe.Add(arrFlujo[0] + "#" + Define.TIP_PAGO_DEBIEFEC + "#" + arrFlujo[2], decImporte);
                        Tip_Pago = Define.TIP_PAGO_DEBIEFEC;
                        break;
                    default:
                        break;
                }
            }
            for (int i = 0; i < listaCambio.Count; i++)
            {
                switch (listaCambio[i].Tip_Pago)
                {
                    case Define.TIP_PAGO_CREDITO:
                        listaCambio[i].Tip_Pago = Define.TIP_PAGO_CREDEFEC;
                        listaCambio[i].Flg_TarjetaEfec = true;
                        break;
                    case Define.TIP_PAGO_DEBITO:
                        listaCambio[i].Tip_Pago = Define.TIP_PAGO_DEBIEFEC;
                        listaCambio[i].Flg_TarjetaEfec = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SeleccionarPagoEfectivo(int idx)
        {
            this.olkbPago.SelectedButton = olkbPago.Buttons[idx];
        }

        private string ObtenerDescripcionPagoLiquidacion()
        {
            switch (Tip_Pago)
            {
                case Define.TIP_PAGO_CREDEFEC:
                    return Property.htListaCampos[Define.CAMPO_TIPOPAGO + Define.TIP_PAGO_CREDITO].ToString();
                case Define.TIP_PAGO_DEBIEFEC:
                    return Property.htListaCampos[Define.CAMPO_TIPOPAGO + Define.TIP_PAGO_DEBITO].ToString();
                default:
                    return "";
            }
        }

        private decimal ObtenerMontoRestante()
        {
            string strMonPago = ((Moneda)cbxMoneda.SelectedItem).SCodMoneda;
            decimal decTasaCompra = 0;
            decimal decTasaVenta = 0;
            if (strMonPago == objTipoTicket.SCodMoneda)
            {
                return Imp_TotPagBase - Imp_Monto;
            }
            if (objTipoTicket.SCodMoneda == Cod_Moneda_Sol)
            {
                decTasaCompra = decimal.Parse(Lista_Tasas[strMonPago + "#" + Define.TC_COMPRA].ToString());
                return Imp_TotPagBase - (Imp_Monto * decTasaCompra);
            }
            if (strMonPago == Cod_Moneda_Sol)
            {
                decTasaVenta = decimal.Parse(Lista_Tasas[objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA].ToString());
                return Imp_TotPagBase - (Imp_Monto / decTasaVenta);
            }
            decTasaCompra = decimal.Parse(Lista_Tasas[strMonPago + "#" + Define.TC_COMPRA].ToString());
            decTasaVenta = decimal.Parse(Lista_Tasas[objTipoTicket.SCodMoneda + "#" + Define.TC_VENTA].ToString());
            return Imp_TotPagBase - ((Imp_Monto * decTasaCompra) / decTasaVenta);
        }

        private Cambio AgregarCambioVuelto(string strMonPago, string strTipCambio, string strDscMonInt, decimal decTasa, string strSimbInt, decimal decImpInt, decimal decImpNac, string strTipPago)
        {
            Cambio objCambio = new Cambio();
            if (decImpInt > 0)
            {

                objCambio.Cod_Moneda = strMonPago;
                objCambio.Tip_Cambio = strTipCambio;
                objCambio.Dsc_MonedaInt = strDscMonInt;
                objCambio.Imp_TasaCambio = decTasa;
                objCambio.Dsc_SimboloInt = strSimbInt;
                objCambio.Tip_Pago = strTipPago;
                objCambio.Flg_TarjetaEfec = false;
                objCambio.Imp_MontoInt = Function.FormatDecimal(decImpInt * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                objCambio.Imp_MontoNac = Function.FormatDecimal(decImpNac * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
            }
            return objCambio;
        }

        private void AgregarFlujoImpVuelto()
        {
            //if (Imp_Flujo_Vuelto != "")
            //{
            //    decimal decImporte = Lista_Flujo_Importe["+#" + Define.TIP_PAGO_EFECTIVO + "#" + Imp_Flujo_Vuelto.Split('#')[0]] != null ? decimal.Parse(Lista_Flujo_Importe["+#" + Define.TIP_PAGO_EFECTIVO + "#" + Imp_Flujo_Vuelto.Split('#')[0]].ToString()) : 0;
            //    decImporte += decimal.Parse(Imp_Flujo_Vuelto.Split('#')[1]);
            //    AgregarImporteCaja(Imp_Flujo_Vuelto.Split('#')[0], Define.TIP_PAGO_EFECTIVO, "+", decImporte);
            //}

            if (!(Lista_Flujo_Vuelto != null && Lista_Flujo_Vuelto.Count > 0))
            {
                return;
            }
            object[] keys = new object[Lista_Flujo_Vuelto.Count];
            Lista_Flujo_Vuelto.Keys.CopyTo(keys, 0);
            for (int i = 0; i < keys.Length; i++)
            {
                if (Lista_Flujo_Importe[keys[i]] == null)
                {
                    Lista_Flujo_Importe.Add(keys[i], Lista_Flujo_Vuelto[keys[i]]);
                }
                else
                {
                    decimal decImporte = decimal.Parse(Lista_Flujo_Vuelto[keys[i]].ToString());
                    decImporte += decimal.Parse(Lista_Flujo_Importe[keys[i]].ToString());
                    Lista_Flujo_Importe[keys[i]] = decImporte;
                }
            }
        }

        private void AgregarImporteVuelto(string strMoneda, string strTipPago, string strTipoOpe, decimal decImporte)
        {
            if (decImporte == 0)
            {
                return;
            }
            if (Lista_Flujo_Vuelto[strTipoOpe + "#" + strTipPago + "#" + strMoneda] == null)
            {
                Lista_Flujo_Vuelto.Add(strTipoOpe + "#" + strTipPago + "#" + strMoneda, decImporte);
            }
            else
            {
                Lista_Flujo_Vuelto[strTipoOpe + "#" + strTipPago + "#" + strMoneda] = decImporte;
            }
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

        private void LimpiarResumen()
        {
            lblFP1.Text = "";
            lblFP2.Text = "";
            lblIP1.Text = "";
            lblIP2.Text = "";
            lblMP1.Text = "";
            lblMP2.Text = "";
        }

        private void nudCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (ActiveControl.Text == "")
            {
                ActiveControl.Text = "0";
                nudCantidad.Value = nudCantidad.Minimum;
                nudCantidad.Select(0, 1);
            }
            else {
                try
                {
                    Imp_TotPagBase = nudCantidad.Value * objTipoTicket.DImpPrecio;
                    lblTotPagBaseVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
                }
                catch
                {
                    lblTotPagBaseVal.Text = "0.00";
                    txtMonto.Text = lblTotPagBaseVal.Text;
                    Limpiar();
                }
                lblTotPagarVal.Text = lblTotPagBaseVal.Text;
                if (!txtMonto.Enabled)
                {
                    txtMonto.Text = this.lblTotPagarVal.Text;
                }
                ActualizarMontoAPagar();
            }
        }

        private void rbSeleccionar_Click(object sender, EventArgs e)
        {
            this.dgvVuelos.Focus();
        }
    }
}
