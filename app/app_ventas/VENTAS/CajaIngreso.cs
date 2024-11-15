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
    public partial class CajaIngreso : Form
    {
        protected Usuario objUsuario;
        protected Turno objTurno;
        protected BO_Turno objBOTurno;
        protected BO_Error objBOError;
        protected bool boError;
        protected List<LogOperacCaja> listaCaja;
        protected Principal formMyParent;

        // parametros de impresion (GGarcia-20090907)
        // lista parametros de configuracion
        private Hashtable listaParamConfig;
        // lista parametros a imprimir
        private Hashtable listaParamImp;
        // xml
        private XmlDocument xml;
        // instancia de impresion
        private Print impresion;

        // se agregan parametros de impresion (GGarcia-20090907)
        public CajaIngreso(Usuario objUsuario, Turno objTurno, Hashtable listaParamConfig, Hashtable listaParamImp, XmlDocument xml, Print impresion, Principal formMyParent)
        {
            InitializeComponent();
            gbxIngCaja.Text = (string)LabelConfig.htLabels["ingreso.gbxIngCaja"];
            boError = false;
            objBOTurno = new BO_Turno();
            this.dgvIngreso.DataSource = objBOTurno.ListarMonedas();
            this.objUsuario = objUsuario;
            this.objTurno = objTurno;

            // inicializar parametros de impresion (GGarcia-20090907)
            // lista parametros de config
            this.listaParamConfig = listaParamConfig;
            // lista parametros a imprimir
            this.listaParamImp = listaParamImp;
            // xml
            this.xml = xml;
            // instancia de impresion
            this.impresion = impresion;
            this.formMyParent = formMyParent;
        }

        private void dgwIngCaja_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        private bool valido()
        {
            int intContador = 0;
            listaCaja = new List<LogOperacCaja>();
            for (int i = 0; i < dgvIngreso.Rows.Count; i++)
            {
                try
                {
                    if (dgvIngreso.Rows[i].Cells[2].Value == null)
                    {
                        continue;
                    }
                    decimal dMonto = decimal.Parse((string)dgvIngreso.Rows[i].Cells[2].Value);
                    if (dMonto > 0)
                    {
                        LogOperacCaja objOpeCaja = new LogOperacCaja();
                        objOpeCaja.DImpOperacion = dMonto;
                        objOpeCaja.SCodMoneda = (string)dgvIngreso.Rows[i].Cells[0].Value;
                        objOpeCaja.Dsc_Moneda = (string)dgvIngreso.Rows[i].Cells[1].Value;
                        objOpeCaja.Dsc_Simbolo = (string)dgvIngreso.Rows[i].Cells[3].Value;
                        listaCaja.Add(objOpeCaja);
                        intContador++;
                    }
                    else if (dMonto < 0)
                    {
                        erpMontos.SetError(dgvIngreso, (string)LabelConfig.htLabels["ingreso.erpMontos"]);
                        return false;
                    }
                }
                catch
                {
                    erpMontos.SetError(dgvIngreso, (string)LabelConfig.htLabels["ingreso.erpMontos"]);
                    return false;
                }
            }
            if (intContador == 0)
            {
                erpMontos.SetError(dgvIngreso, (string)LabelConfig.htLabels["ingreso.erpMontos"]);
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvIngreso_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvIngreso.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
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

        private void CajaIngreso_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }

        private void Limpiar()
        {
            listaCaja = null;
            erpMontos.Clear();
            for (int i = 0; i < dgvIngreso.Rows.Count; i++)
            {
                dgvIngreso.Rows[i].Cells[2].Value = null;
                //dgvIngreso.Rows.Clear();
            }
        }
        
        //EAG 11/02/2010
        private int VerificacionImpresora()
        {
            try
            {
                // 1ro: ************************************** Testear VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(xml, Define.ID_PRINTER_DOCUM_INGRESOCAJA);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, listaParamConfig, Define.ID_PRINTER_DOCUM_INGRESOCAJA);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] {','}, 2)[1];
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
            catch(Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion CajaIngreso.VerificacionImpresora() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], ex);
            }
            return 0;
        }
        //EAG 11/02/2010


        private void Imprimir()
        {
            try
            {
                // 1ro: ************************************** Imprimir VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, Define.ID_PRINTER_DOCUM_INGRESOCAJA);
                
                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, Define.ID_PRINTER_DOCUM_INGRESOCAJA);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion();

                //int copias = impresion.ObtenerCopiasVoucher(nodo);

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

                    MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado.Contains("Salir"))
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion CajaIngreso.Imprimir() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgError"], ex);
            }
            listaParamImp.Clear();
        }


        /// <summary>
        /// Metodo que carga la lista de parametros a imprimir.
        /// </summary>
        private void CargarParametrosImpresion()
        {
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, this.objUsuario.SNomUsuario + " " + this.objUsuario.SApeUsuario);
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, this.objTurno.SCodTurno);

            string[] listaCodigoMonedas = null;
            // recorrer la lista parametros de configuracion y obtener la lista de codigos
            IDictionaryEnumerator iteraccion = this.listaParamConfig.GetEnumerator();
            while (iteraccion.MoveNext())
            {
                // si es igual al codigo de monedas
                if (iteraccion.Key.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                {
                    listaCodigoMonedas = ((string)iteraccion.Value).Split(',');
                    break;
                }
            }
            
            string llave;
            string codigo;
            int indice;

            listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, listaCaja.Count);

            for (int j = 0; j < listaCaja.Count; j++)
            {
                //indice = listaCodigoMonedas[j].IndexOf("-");
                //codigo = listaCodigoMonedas[j].Substring(indice + 1, listaCodigoMonedas[j].Length - indice - 1);
                //llave = listaCodigoMonedas[j].Substring(0, indice);
                //for (int z = 0; z < listaCaja.Count; z++)
                //{
                //if (listaCaja[z].SCodMoneda.Equals(codigo))
                //{

                string numero = Function.FormatDecimal(listaCaja[j].DImpOperacion);
                this.listaParamImp.Add("monto_moneda" + "_" + j, numero);// + " " + listaCaja[j].Dsc_Simbolo);

                // llave de soles
                //if (llave.Equals("0"))
                //{
                this.listaParamImp.Add("desc_moneda" + "_" + j, listaCaja[j].Dsc_Moneda);
                this.listaParamImp.Add("simbolo_moneda" + "_" + j, listaCaja[j].Dsc_Simbolo);
                //    break;
                //} // llave de dolares
                //else if (llave.Equals("1"))
                //{
                //    this.listaParamImp.Add("desc_moneda" + "_" + j, "Dolares");
                //    this.listaParamImp.Add("simbolo_moneda" + "_" + j, "$");
                //    break;
                //} // llave de euros
                //else if (llave.Equals("2"))
                //{
                //    this.listaParamImp.Add("desc_moneda" + "_" + j, "Euros");
                //    this.listaParamImp.Add("simbolo_moneda" + "_" + j, "e");
                //    break;
                //}
                //}
                //}
            }

        }

        #region Ex-Metodo Imprimir
        /// <summary>
        /// Metodo que realiza la impresion.
        /// </summary>
        private void Imprimir2()
        {
            string[] data = null;

            try
            {
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(this.xml, Define.ID_PRINTER_DOCUM_INGRESOCAJA);

                // obtiene la configuracion de la impresora a utilizar
                string configuracion = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, Define.ID_PRINTER_DOCUM_INGRESOCAJA);

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion();

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
                                MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void CajaIngreso_KeyDown(object sender, KeyEventArgs e)
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
            }
            catch { }
        }

        private void Grabar()
        {
            formMyParent.VerificarTurnoActivo();
            if (!valido())
            {
                return;
            }
            if (MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgConfirm"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {

                objBOError = new BO_Error();
                BO_Operacion objBOOpera = new BO_Operacion();
                if (objUsuario != null)
                {
                    for (int i = 0; i < listaCaja.Count; i++)
                    {
                        listaCaja[i].SCodUsuario = objUsuario.SCodUsuario;
                        listaCaja[i].SCodTurno = objTurno.SCodTurno;
                        listaCaja[i].SCodTipoOperacion = "IC";
                    }


                    //-----EAG 11/02/2010

                    //Valores devueltos:
                    //  0: Fallo la verificacion
                    //  1: Ok la verificacion
                    int resVerficacionImpresora = VerificacionImpresora();

                    //Console.WriteLine("VerificacionImpresora : " + resVerficacionImpresora);
                    //JCisneros debe de decidir la logica a donde ira
                    if (resVerficacionImpresora == 0)
                    {
                        return;
                    }
                    //-----EAG 11/02/2010



                    if (objBOOpera.RegistrarOperacionCaja(listaCaja))
                    {

                        // ruttina de impresion (GGarcia-20090909)
                        Imprimir();

                        // comentado pues se usa en la rutina de impresion (GGarcia-20090909)
                        //MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Limpiar();
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show(objBOOpera.Dsc_Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Inicie sesion nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}