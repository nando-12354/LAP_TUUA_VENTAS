using LAP.TUUA.CONTROL;
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

namespace LAP.TUUA.VENTAS
{
    public partial class CompraVenta : Form
    {
        protected List<Moneda> listaMoneda;
        protected BO_Operacion objBOOpera;
        protected decimal Imp_RecibioNac;
        protected decimal Imp_RecibidoInt;
        protected decimal Imp_ACambiar;
        protected LogCompraVenta objCompraVenta;
        protected TasaCambio objTasaCambio;
        protected Usuario objUsuario;
        protected Turno objTurno;
        protected BO_Error objBOError;
        protected bool boError;
        protected Moneda objMonedaNac;
        protected Principal formMyParent;

        // parametros de impresion (GGarcia-20090907)
        // lista parametros de configuracion
        private Hashtable listaParamConfig;
        // lista parametros a imprimir
        private Hashtable listaParamImp;
        // xml 
        private XmlDocument xml;
        // impresion
        private Print impresion;

        // se agregan parametros de impresion (GGarcia-20090907)
        public CompraVenta(Usuario objUsuario, Turno objTurno, Hashtable listaParamConfig, Hashtable listaParamImp, XmlDocument xml, Print impresion, Principal formMyParent)
        {
            InitializeComponent();

            boError = false;
            objBOOpera = new BO_Operacion();
            this.objUsuario = objUsuario;
            this.objTurno = objTurno;
            objCompraVenta = new LogCompraVenta();
            listaMoneda = objBOOpera.ListarMonedasInter();
            if (!(listaMoneda != null && listaMoneda.Count > 0))
            {
                MessageBox.Show((string)LabelConfig.htLabels["compra.msgMonedas"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            objMonedaNac = objBOOpera.ObtenerMoneda((string)Property.htProperty[Define.MONEDANAC]);
            if (objMonedaNac==null)
            {
                MessageBox.Show((string)LabelConfig.htLabels["compra.msgMonedaNacional"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            this.cbxMoneda.DataSource = listaMoneda;
            objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(listaMoneda[0].SCodMoneda, "V");
            this.lblTC.Text = Function.FormatDecimal(objTasaCambio.DImpCambioActual,4).ToString();

            //labels
            this.lblRecibidoI.Text = "Monto Recibido(" + listaMoneda[0].SCodMoneda + ")";
            this.lblACambiar.Text = "Monto a Cambiar(" + listaMoneda[0].SCodMoneda + ")";
            this.lblEntregarI.Text = "Monto a Entregar(" + listaMoneda[0].SCodMoneda + ")";

            // inicializar parametros de impresion (GGarcia-20090907)
            // lista parametros de configuracion
            this.listaParamConfig = listaParamConfig;
            // lista paramnetros a imprimir
            this.listaParamImp = listaParamImp;
            // xml 
            this.xml = xml;
            // impresion
            this.impresion = impresion;
            EnabledTBXCompra();
            this.formMyParent = formMyParent;
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

        private void txtNacional_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void txtInter_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void txtMontoCambio_KeyDown(object sender, KeyEventArgs e)
        {
            VerificarKeys(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private bool valido()
        {
            if (this.rbVenta.Checked)
            {
                if (txtNacional.Text == "")
                {
                    this.erpMontoN.SetError(txtNacional, "Ingrese monto válido.");
                    return false;
                }
                if (txtInterCambio.Text == "")
                {
                    this.erpMontoI.SetError(txtInterCambio, "Ingrese monto válido.");
                    return false;
                }
                try
                {
                    Imp_RecibioNac = decimal.Parse(txtNacional.Text);
                    if (Imp_RecibioNac <= 0)
                    {
                        this.erpMontoN.SetError(txtNacional, "Ingrese monto válido.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    this.erpMontoN.SetError(txtNacional, "Ingrese monto válido.");
                    return false;
                }
                try
                {
                    Imp_ACambiar = decimal.Parse(txtInterCambio.Text);
                    if (Imp_ACambiar <= 0)
                    {
                        this.erpMontoN.SetError(txtInterCambio, "Ingrese monto válido.");
                        return false;
                    }

                }
                catch (Exception e)
                {
                    this.erpMontoN.SetError(txtInterCambio, "Ingrese monto válido.");
                    return false;
                }

                return true;
            }
            if (this.rbCompra.Checked)
            {
                if (txtInter.Text == "")
                {
                    this.erpMontoI.SetError(txtInter, "Ingrese monto válido.");
                    return false;
                }
                if (txtInterCambio.Text == "")
                {
                    this.erpMontoI.SetError(txtInterCambio, "Ingrese monto válido.");
                    return false;
                }
                try
                {
                    Imp_RecibidoInt = decimal.Parse(txtInter.Text);
                    if (Imp_RecibidoInt <= 0)
                    {
                        this.erpMontoI.SetError(txtInter, "Ingrese monto válido.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    this.erpMontoI.SetError(txtInter, "Ingrese monto válido.");
                    return false;
                }
                try
                {
                    Imp_ACambiar = decimal.Parse(txtInterCambio.Text);
                    if (Imp_ACambiar <= 0)
                    {
                        this.erpMontoI.SetError(txtInterCambio, "Ingrese monto válido.");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    this.erpMontoI.SetError(txtInterCambio, "Ingrese monto válido.");
                    return false;
                }
                return true;
            }
            return true;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            string strTipo = "";
            int intTipo;
            if (rbVenta.Checked)
            {
                strTipo = "Venta Moneda";
                intTipo = 0;
            }
            else
            {
                intTipo = 1;
                strTipo = "Compra Moneda";
            }

            Calculo formCalculo = new Calculo(strTipo, this.cbxMoneda.SelectedValue.ToString(), this.lblTC.Text, intTipo);
            formCalculo.Show();
        }

        private void rbCompra_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCompra.Checked)
            {
                EnabledTBXVenta();
                ActualizarTasa(Define.TC_COMPRA);
            }
        }

        //private void rbVentaT_CheckedChanged(object sender, EventArgs e)
        //{
        //    objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(this.cbxMoneda.SelectedValue.ToString(), "V");
        //    if (objTasaCambio != null)
        //    {
        //        this.lblTC.Text = Function.FormatDecimal(objTasaCambio.DImpCambioActual, 2).ToString();
        //    }
        //    else
        //    {
        //        MessageBox.Show((string)LabelConfig.htLabels["compra.msgTasaCambio"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Limpiar();
        //    }
        //}

        private void rbVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (rbVenta.Checked)
            {
                EnabledTBXCompra();
                ActualizarTasa(Define.TC_VENTA);
            }
        }

        private void cbxMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            //labels
            this.lblRecibidoI.Text = "Monto Recibido(" + listaMoneda[cbxMoneda.SelectedIndex].SCodMoneda + ")";
            this.lblACambiar.Text = "Monto a Cambiar(" + listaMoneda[cbxMoneda.SelectedIndex].SCodMoneda + ")";
            this.lblEntregarI.Text = "Monto a Entregar(" + listaMoneda[cbxMoneda.SelectedIndex].SCodMoneda + ")";
            Limpiar();
            lblEntreInter.Text = ((Moneda)cbxMoneda.Items[cbxMoneda.SelectedIndex]).SDscSimbolo + " 0.00";
        }

        private void CompraVenta_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }

        private void Limpiar()
        {
            erpMontoI.Clear();
            erpMontoN.Clear();
            rbVenta.Checked = true;
            rbCompra.Checked = false;
            txtInter.Text = "";
            txtInterCambio.Text = "";
            txtNacional.Text = "";
            btnAceptar.Enabled = true;
            lblEntreNac.Text = objMonedaNac.SDscSimbolo+" 0.00";
            lblEntreInter.Text = ((Moneda)cbxMoneda.Items[0]).SDscSimbolo + " 0.00";
            DefaultVenta();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void DefaultVenta()
        {
            objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(this.cbxMoneda.SelectedValue.ToString(), Define.TC_VENTA);
            if (objTasaCambio != null)
            {
                this.lblTC.Text = Function.FormatDecimal(objTasaCambio.DImpCambioActual, 4).ToString();
            }
            else
            {
                MessageBox.Show((string)LabelConfig.htLabels["compra.msgTasaCambio"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComprarMoneda()
        {
            objCompraVenta.SCodTipoOperacion = Define.VENTA_MONEDA;
            objCompraVenta.DImpTasaCambio = decimal.Parse(lblTC.Text);
            objCompraVenta.SCodMoneda = cbxMoneda.SelectedValue.ToString();


            //-----EAG 11/02/2010

            //Valores devueltos:
            //  0: Fallo la verificacion
            //  1: Ok la verificacion
            int resVerficacionImpresora = VerificacionImpresora(Define.ID_PRINTER_DOCUM_VENTAMONEDA);

            //Console.WriteLine("VerificacionImpresora : " + resVerficacionImpresora);
            //JCisneros debe de decidir la logica a donde ira
            if (resVerficacionImpresora == 0)
            {
                return;
            }
            //-----EAG 11/02/2010


            if (objBOOpera.RegistrarCompraMoneda(objCompraVenta, Imp_RecibioNac, Imp_ACambiar) == 0)
            {   
                // obtener el nombre de la operacion compra dolares/compra euros e imprimir (GGarcia-20090909)
                Imprimir(Define.ID_PRINTER_DOCUM_VENTAMONEDA);
                //Imprimir(Define.ID_PRINTER_DOCUM_VENTAMONEDA);
                //Limpiar();
                // comentado pues se utiliza en l arutina de impresion (GGarcia-20090909)
                //lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarNac, 2).ToString();
                //lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarInter, 2).ToString();
                //MessageBox.Show((string)LabelConfig.htLabels["compra.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //btnAceptar.Enabled = false;
                EnabledTBXCompra();
                //Limpiar();
            }
            else
            {
                MessageBox.Show(objBOOpera.Dsc_Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VenderMoneda()
        {
            objCompraVenta.SCodTipoOperacion = Define.COMPRA_MONEDA;
            objCompraVenta.DImpTasaCambio = decimal.Parse(lblTC.Text);
            objCompraVenta.DImpACambiar = Imp_ACambiar;
            objCompraVenta.SCodMoneda = cbxMoneda.SelectedValue.ToString();

            //-----EAG 11/02/2010

            //Valores devueltos:
            //  0: Fallo la verificacion
            //  1: Ok la verificacion
            int resVerficacionImpresora = VerificacionImpresora(Define.ID_PRINTER_DOCUM_COMPRAMONEDA);

            Console.WriteLine("VerificacionImpresora : " + resVerficacionImpresora);
            //JCisneros debe de decidir la logica a donde ira
            if (resVerficacionImpresora == 0)
            {
                return;
            }
            //-----EAG 11/02/2010
            if (this.Imp_RecibidoInt - this.Imp_ACambiar < 0)
            {
                MessageBox.Show("El monto Recibido no es suficiente para efectuar la operación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (objBOOpera.RegistrarVentaMoneda(objCompraVenta, Imp_RecibidoInt) == 0)
            {
                // obtener el nombre de la operacion venta dolares/venta euros e imprimir (GGarcia-20090909)
                Imprimir(Define.ID_PRINTER_DOCUM_COMPRAMONEDA);
                //Imprimir(Define.ID_PRINTER_DOCUM_COMPRAMONEDA);
                //Limpiar();
                // comentado pues se utiliza en l arutina de impresion (GGarcia-20090909)
                //lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarNac, 2).ToString();
                //lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarInter, 2).ToString();
                //MessageBox.Show((string)LabelConfig.htLabels["compra.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //btnAceptar.Enabled = false;
                EnabledTBXCompra();
                //Limpiar();
            }
            else
            {
                MessageBox.Show(objBOOpera.Dsc_Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //EAG 11/02/2010
        private int VerificacionImpresora(string nombre)
        {
            try
            {
                // 1ro: ************************************** Testear VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(xml, nombre);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, listaParamConfig, nombre);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                String[] parametros = new String[]
                  {
                      "flagImpSticker=0",
                      "flagImpVoucher=1",
                      "configImpVoucher=" + configImpVoucher
                  };

                frmPrintNet frmPrintNet = new frmPrintNet(parametros, true);
                frmPrintNet.ShowDialog(this);

                //********************************
                String resultado = frmPrintNet.Resultado;

                if (resultado.Equals("1"))
                {
                    Property.puertoVoucher = frmPrintNet.PuertoSerial.getConfigImpVoucher()[0];
                    return 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion CompraVenta.VerificacionImpresora() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], ex);
            }
            return 0;
        }
        //EAG 11/02/2010

        private void Imprimir(string nombre)
        {
            try
            {
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                cargarParametrosImpresion(nombre);

                //int copias = impresion.ObtenerCopiasVoucher(nodo);

                // obtiene la estructura del documento a imprimir
                String dataVoucher = impresion.ObtenerDataFormateada(listaParamImp, nodo);

                String[] parametros = new String[]
                  {
                      "flagImpSticker=0",
                      "flagImpVoucher=1",
                      "dataVoucher=" + dataVoucher,
                      "configImpVoucher=" + configImpVoucher
                      //"copiasVoucher=" + copias
                  };

                frmPrintNet frmPrintNet = new frmPrintNet(parametros);
                frmPrintNet.ShowDialog(this);

                //********************************
                String resultado = frmPrintNet.Resultado;

                if (resultado.Contains("Impresion"))
                {
                    Property.puertoVoucher = frmPrintNet.PuertoSerial.getConfigImpVoucher()[0];

                    //lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarNac, 2).ToString();
                    //lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarInter, 2).ToString();
                    MessageBox.Show((string)LabelConfig.htLabels["compra.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado.Contains("Salir"))
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Limpiar();
            }
            catch(Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion CompraVenta.Imprimir() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgError"], ex);
            }
            listaParamImp.Clear();
            
        }



        /// <summary>
        /// Metodo que carga la lista de parametros a imprimir.
        /// <param name="nombre">nombre de la operacion</param>
        /// </summary>
        private void cargarParametrosImpresion(string nombre)
        {
            string strMonedaInter;
            string strSimbMonInter;
            listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario);
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, this.objTurno.SCodTurno);

            string numero = Function.FormatDecimal(this.Imp_ACambiar);

            //if (nombre.Equals(Define.ID_PRINTER_DOCUM_COMPRADOLARES) || nombre.Equals(Define.ID_PRINTER_DOCUM_VENTADOLARES))
            //{
            //    this.listaParamImp.Add(Define.ID_PRINTER_PARAM_MONTO_DOLARES, numero);
            //}
            //else if (nombre.Equals(Define.ID_PRINTER_DOCUM_COMPRAEUROS) || nombre.Equals(Define.ID_PRINTER_DOCUM_VENTAEUROS))
            //{
            //    this.listaParamImp.Add(Define.ID_PRINTER_PARAM_MONTO_EUROS, numero);

            //}
            strMonedaInter=((Moneda)cbxMoneda.SelectedItem).SDscMoneda;
            strSimbMonInter = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo;
            this.listaParamImp.Add("moneda_internacional", strMonedaInter);
            this.listaParamImp.Add("monto_internacional", Function.FormatDecimal(this.Imp_ACambiar));// + " " + strSimbMonInter);
            this.listaParamImp.Add("simbolo_mon_internacional", strSimbMonInter);
            this.listaParamImp.Add("Numero_Operacion", objCompraVenta.SNumOperacion);

            numero = Function.FormatDecimal(this.Imp_ACambiar * decimal.Parse(this.lblTC.Text));
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_MONTO_SOLES, numero);

            numero = Function.FormatDecimal(decimal.Parse(this.lblTC.Text)*Define.FACTOR_DECIMAL,4).ToString();
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_TIPO_CAMBIO, numero);

            if (nombre.Equals(Define.ID_PRINTER_DOCUM_VENTAMONEDA))
            {
                numero = Function.FormatDecimal(this.Imp_RecibioNac - this.Imp_ACambiar * decimal.Parse(this.lblTC.Text));

            }
            else if (nombre.Equals(Define.ID_PRINTER_DOCUM_COMPRAMONEDA))
            {
                numero = Function.FormatDecimal(this.Imp_RecibidoInt - this.Imp_ACambiar);// +" " + strSimbMonInter;

            }
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_VUELTO, numero);

        }

        #region (obtenerOperacion) Usado anteriormente
        /// <summary>
        /// Metodo que obtiene la operacion a realizar
        /// <param name="tipo">tipo de la operacion [compra dolares/euros o venta dolares/euros]</param>
        /// </summary>
        private string obtenerOperacion(int tipo)
        {
            string codigoMoneda = "";
            int indice;
            string[] codigos = null;

            // recorrer la lista parametros de configuracion
            IDictionaryEnumerator iteraccion = this.listaParamConfig.GetEnumerator();
            while (iteraccion.MoveNext())
            {
                // si es igual al codigo de monedas
                if (iteraccion.Key.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                {
                    codigos = ((string)iteraccion.Value).Split(',');
                }
            }

            if (codigos == null || codigos.Equals(""))
            {
                throw new Exception("");
            }

            // buscar el codigo de moneda
            for (int i = 0; i < codigos.Length; i++)
            {
                indice = codigos[i].IndexOf('-');
                if (codigos[i].Substring(indice + 1, codigos[i].Length - indice - 1).Equals(this.objCompraVenta.SCodMoneda))
                {
                    // obtener llave 
                    string llave = codigos[i].Substring(0, indice);
                    if (llave.Equals("1"))
                    {
                        if (tipo == 0)
                        {

                            codigoMoneda = Define.ID_PRINTER_DOCUM_COMPRADOLARES;

                        }
                        else if (tipo == 1)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTADOLARES;

                        }

                    }
                    else if (llave.Equals("2"))
                    {
                        if (tipo == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_COMPRAEUROS;

                        }
                        else if (tipo == 1)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTAEUROS;

                        }


                    }
                    break;

                }
            }

            return codigoMoneda;
        }
        #endregion

        #region Ex-Metodo Imprimir
        /// <summary>
        /// Metodo que realiza la impresion.
        /// </summary>
        private void Imprimir2(string nombre)
        {
            string[] data = null;

            try
            {

                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);

                // obtiene la configuracion de la impresora a utilizar
                string configuracion = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                // carga la lista de parametros a imprimir
                cargarParametrosImpresion(nombre);

                // obtiene la estructura del documento a imprimir
                data = impresion.ObtenerData(listaParamImp, nodo);

                // configura la impresora a utilizar
                impresion.configurarImpresora(configuracion);

                if (data != null || !data.Equals(""))
                {
                    // verifica el estado de la impresora a utilizar
                    int estado = impresion.verificarEstadoImpresora(Define.ID_PRINTER_VOUCHER);
                    switch (estado)
                    {
                        // impresora no operativa
                        case 0:
                            MessageBox.Show((string)LabelConfig.htLabels["impresion.msgImpresora"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        // impresora operativa 
                        case 1:
                            estado = impresion.imprimir(Define.ID_PRINTER_VOUCHER, data);
                            if (estado == 1)
                            {
                                lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarNac, 2).ToString();
                                lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(objCompraVenta.DImpEntregarInter, 2).ToString();
                                MessageBox.Show((string)LabelConfig.htLabels["compra.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            else
                            {
                                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            break;
                        // impresora no tiene papel
                        case 2:
                            MessageBox.Show((string)LabelConfig.htLabels["impresion.msgNoPapel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgData"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                listaParamImp.Clear();

            }
            catch (Exception e)
            {
                listaParamImp.Clear();

            }

        }
        #endregion

        private void EnabledTBXCompra()
        {
            txtNacional.Enabled = true;
            txtInter.Text = "";
            txtInter.Enabled = false;
        }

        private void EnabledTBXVenta()
        {
            txtNacional.Text = "";
            txtNacional.Enabled = false;
            txtInter.Enabled = true;
        }

        private void CompraVenta_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grabar()
        {
            formMyParent.VerificarTurnoActivo();
            if (!valido())
            {
                return;
            }
            if (MessageBox.Show((string)LabelConfig.htLabels["compra.msgConfirm"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                objBOError = new BO_Error();
                objCompraVenta.SCodTurno = this.objTurno.SCodTurno;
                objCompraVenta.SCodUsuario = this.objUsuario.SCodUsuario;
                this.lblEntreInter.Text = "";
                this.lblEntreNac.Text = "";
                if (ExisteVariacionTasa())
                {
                    return;
                }
                if (rbVenta.Checked)
                {
                    ComprarMoneda();
                    //VenderMoneda();
                }
                else
                {
                    VenderMoneda();
                    //ComprarMoneda();
                   
                }
            }
            catch (Exception ex)
            {
                objBOError.IsError();
                boError = true;
                ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
            }
            finally
            {
                if (boError)
                {
                    MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ActualizarVuelto()
        {
            decimal decEntregarNac;
            decimal decEntregarInt;
            decimal decTasaCambio = decimal.Parse(lblTC.Text);

            Imp_ACambiar = txtInterCambio.Text!=""?decimal.Parse(txtInterCambio.Text):0;
            if (rbVenta.Checked)
            {
                Imp_RecibioNac = txtNacional.Text!=""?decimal.Parse(txtNacional.Text):0;
                decEntregarNac = Imp_RecibioNac - Imp_ACambiar * decTasaCambio;
                decEntregarInt = Imp_ACambiar * Define.FACTOR_DECIMAL;

                lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(decEntregarNac, 2).ToString();
                lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(decEntregarInt, 2).ToString();
            }
            else
            {
                Imp_RecibidoInt = txtInter.Text!=""?decimal.Parse(txtInter.Text):0;
                decEntregarNac = Imp_ACambiar * decTasaCambio;
                decEntregarInt = (Imp_RecibidoInt - Imp_ACambiar) * Define.FACTOR_DECIMAL;

                lblEntreNac.Text = objMonedaNac.SDscSimbolo + " " + Function.FormatDecimal(decEntregarNac, 2).ToString();
                lblEntreInter.Text = ((Moneda)cbxMoneda.SelectedItem).SDscSimbolo + " " + Function.FormatDecimal(decEntregarInt, 2).ToString();
            }
        }

        private void txtNacional_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            ActualizarVuelto();
        }

        private void txtInter_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            ActualizarVuelto();
        }

        private void txtInterCambio_TextChanged(object sender, EventArgs e)
        {
            ValidarTextBoxImporte((TextBox)sender);
            ActualizarVuelto();
        }

        private void ActualizarTasa(string strTipCambio)
        {
            objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(this.cbxMoneda.SelectedValue.ToString(), strTipCambio);
            if (objTasaCambio != null)
            {
                this.lblTC.Text = Function.FormatDecimal(objTasaCambio.DImpCambioActual, 4).ToString();
                ActualizarVuelto();
            }
            else
            {
                MessageBox.Show((string)LabelConfig.htLabels["compra.msgTasaCambio"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Limpiar();
            }
        }

        private bool ExisteVariacionTasa()
        {
            TasaCambio objTasaVigente = objBOOpera.ObtenerTasaCambioPorMoneda(objTasaCambio.SCodMoneda, objTasaCambio.STipCambio);
            if (objTasaVigente.DImpCambioActual != objTasaCambio.DImpCambioActual)
            {
                MessageBox.Show(objTasaCambio.SCodMoneda + "-" + objTasaCambio.STipCambio + ": " + objTasaCambio.DImpCambioActual.ToString() + "(Antiguo)," + objTasaVigente.DImpCambioActual.ToString() + "(Vigente)", (string)LabelConfig.htLabels["normal.msgVarTasa"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ActualizarTasa(objTasaCambio.STipCambio);
                return true;
            }
            return false;
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