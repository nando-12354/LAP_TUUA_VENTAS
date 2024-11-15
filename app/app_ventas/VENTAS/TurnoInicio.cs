using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Xml;

namespace LAP.TUUA.VENTAS
{
    public partial class TurnoInicio : Form
    {
        protected Usuario objUsuario;
        protected Logueo MyParent;
        protected BO_Turno objBOTurno;
        protected BO_Error objBOError;
        protected bool boError;
        protected List<ArbolModulo> listaPerfil;
        protected string Cod_PtoVenta;

        private Hashtable listaParamImp; //FL.
        private Print impresion; //FL.
        private XmlDocument xml; //FL.
        private Hashtable listaParamConfig; //FL.

        public TurnoInicio(Usuario objUsuario, Logueo MyParent, List<ArbolModulo> listaPerfil, string strPtoVenta)
        {
            InitializeComponent();
            CargarLabels();
            this.objUsuario = objUsuario;
            this.MyParent = MyParent;
            objBOTurno = new BO_Turno();
            objBOError = new BO_Error();
            boError = false;
            this.listaPerfil = listaPerfil;
            Cod_PtoVenta = strPtoVenta;

            
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Grabar();
        }

        public bool valido()
        {
            for (int i = 0; i < dgwIniMontos.Rows.Count; i++)
            {
                try
                {
                    double dblMonto = Double.Parse((string)dgwIniMontos.Rows[i].Cells[2].Value);
                    if (dblMonto < 0)
                    {
                        erpMontos.SetError(dgwIniMontos, (string)LabelConfig.htLabels["turno.erpMontos"]);
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    erpMontos.SetError(dgwIniMontos, (string)LabelConfig.htLabels["turno.erpMontos"]);
                    return false;
                }
            }
            return true;
        }

        private void GUITurno_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
            List<Moneda> lista = objBOTurno.ListarMonedas();
            if (lista != null && lista.Count > 0)
            {
                this.dgwIniMontos.DataSource = lista;
                this.MyParent.Hide();
            }
            else
            {
                MessageBox.Show((string)LabelConfig.htLabels["turno.msgMoneda"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.MyParent.Show();
            this.Close();
        }

        private void IniciarTurno(List<TurnoMonto> lobjListaMontos)
        {
            try
            {
                // obtener parametros de impresion (GGarcia-20090907)
                BO_ParameGeneral objBOParameGeneral = new BO_ParameGeneral();
                XmlDocument xml = new XmlDocument();
                Hashtable listaParamConfig = new Hashtable();
                objBOParameGeneral.ObtenerParametrosImpresion(listaParamConfig, xml);

                //-----EAG 11/02/2010

                //-----EAG 26/02/2010
                if (Property.htProperty["ConfigVoucher"] != null && !Property.htProperty["ConfigVoucher"].ToString().Trim().Equals(""))
                {
                    String configVoucher = Property.htProperty["ConfigVoucher"].ToString().Trim();
                    String[] arrConfigVoucher = configVoucher.Split(',');
                    if(arrConfigVoucher.Length == 5)
                    {
                        listaParamConfig["voucher"] = configVoucher;
                    }
                }
                if (Property.htProperty["ConfigSticker"] != null && !Property.htProperty["ConfigSticker"].ToString().Trim().Equals(""))
                {
                    String configSticker = Property.htProperty["ConfigSticker"].ToString().Trim();
                    String[] arrConfigVoucher = configSticker.Split(',');
                    if (arrConfigVoucher.Length == 5)
                    {
                        listaParamConfig["sticker"] = configSticker;
                    }
                }
                //-----EAG 26/02/2010


                //Valores devueltos:
                //  0: Fallo la verificacion
                //  1: Ok la verificacion
                int resVerficacionImpresora = VerificacionImpresora(xml, listaParamConfig);
                
                Console.WriteLine("VerificacionImpresora : " + resVerficacionImpresora);
                //JCisneros debe de decidir la logica a donde ira
                if (resVerficacionImpresora == 0)
                {
                    return;
                }
                //----- CARGA LA EMPRESA RECAUDADORA POR EL IDENTIFICADOR PADRE -----\\
                BO_ParameGeneral objBOEmpresa = new BO_ParameGeneral();
                DataTable dtEmpresaRecaudadora = objBOEmpresa.ObtenerEmpresaPorIdentificadorPadre("ER");

                if (dtEmpresaRecaudadora.Rows.Count > 0)
                {
                    // Filtra la empresa que esta asociada al usuario
                    DataRow[] rows = dtEmpresaRecaudadora.Select($"Identificador = '{objUsuario.Emp_Recaudadora}'");
                    if (rows.Length > 0)
                    {
                        objBOEmpresa.ObjEmpresa = new ParameGeneral
                        {
                            SIdentificador = objUsuario.Emp_Recaudadora,
                            SDescripcion = rows[0]["Descripcion"].ToString()
                        };
                    }
                    else
                    {
                        MessageBox.Show("El usuario no pertenece a una Empresa Recaudadora.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron empresas recaudadoras.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //--------------------------------------------------------------------\\
                //-----EAG 11/02/2010

                if (objBOTurno.IniciarTurno(lobjListaMontos, objUsuario.SCodUsuario, Cod_PtoVenta))
                {
                    Imprimir(xml, listaParamConfig, lobjListaMontos);

                    MessageBox.Show("Se abrió el turno [" + objBOTurno.ObjTurno.SCodTurno + "]correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    this.MyParent.Hide();

                    // se agrega parametros de impresion (GGarcia-20090907)
                    //Principal formVentas = new Principal(this.objUsuario, objBOTurno.ObjTurno, this.MyParent, listaPerfil);
                    MyParent.formPrincipal = new Principal(this.objUsuario, objBOTurno.ObjTurno, this.MyParent, listaPerfil, listaParamConfig, xml, objBOEmpresa.ObjEmpresa);
                    MyParent.formPrincipal.Show();
                }
                else
                {
                    MessageBox.Show(objBOTurno.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowErrorHandler(ex.Message);
                return;
            }
        }

        //EAG 11/02/2010
        private int VerificacionImpresora(XmlDocument xml, Hashtable listaParamConfig)
        {
            Print impresion = new Print();
            try
            {
                // 1ro: ************************************** Testear VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(xml, Define.ID_PRINTER_DOCUM_INICIOTURNO);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, listaParamConfig, Define.ID_PRINTER_DOCUM_INICIOTURNO);

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
                Console.WriteLine("Excepcion TurnoInicio.VerificacionImpresora() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], ex);
            }
            return 0;
        }
        //EAG 11/02/2010

        //--EAG
        private void Imprimir(XmlDocument xml, Hashtable listaParamConfig, List<TurnoMonto> lobjListaMontos)
        {
            Hashtable listaParamImp = new Hashtable();
            Print impresion = new Print();
            try
            {
                // 1ro: ************************************** Imprimir VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(xml, Define.ID_PRINTER_DOCUM_INICIOTURNO);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, listaParamConfig, Define.ID_PRINTER_DOCUM_INICIOTURNO);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                // carga la lista de parametros a imprimir
                CargarParametrosImpresion(listaParamConfig, listaParamImp, lobjListaMontos);

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

                    //MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MostrarVistaPrevia(dataVoucher); //FL.
                }
                else if (resultado.Contains("Salir"))
                {
                    MessageBox.Show((string)LabelConfig.htLabels["inicioturno.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion TurnoInicio.Imprimir() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgError"], ex);
            }
            listaParamImp.Clear();
        }

        ////-------METODO PARA VISUALIZAR EL VOUCHER EN PANTALLA -------\\
        //private void MostrarVistaPrevia(string dataVoucher)
        //{
        //    Form vistaPreviaForm = new Form();
        //    vistaPreviaForm.Text = "Vista previa de impresión";
        //    vistaPreviaForm.Size = new Size(350, 700);
        //    vistaPreviaForm.StartPosition = FormStartPosition.CenterScreen;
        //    vistaPreviaForm.MaximizeBox = false;
        //    vistaPreviaForm.MinimizeBox = false;

        //    Panel stickerPanel = new Panel
        //    {
        //        Dock = DockStyle.Fill,
        //        BackColor = Color.White,
        //        BorderStyle = BorderStyle.FixedSingle,
        //        AutoScroll = true
        //    };

        //    // Formatear los datos para mostrar
        //    string formattedVoucher = FormatearVoucher(dataVoucher);

        //    Label voucherLabel = new Label
        //    {
        //        Text = $"VOUCHER:\n{formattedVoucher}",
        //        AutoSize = true,
        //        Font = new Font("Arial", 9),
        //        Dock = DockStyle.Top,
        //        Padding = new Padding(10)
        //    };

        //    stickerPanel.Controls.Add(voucherLabel);

        //    vistaPreviaForm.Controls.Add(stickerPanel);

        //    vistaPreviaForm.ShowDialog();

        //    if (vistaPreviaForm.DialogResult == DialogResult.OK)
        //    {
        //        ImprimirDocumento(dataVoucher);
        //    }
        //}
        //private string FormatearVoucher(string dataVoucher)
        //{
        //    return dataVoucher.Replace("@", "\n").Replace(":", ": ").Replace("  ", " ");
        //}
        //private void ImprimirDocumento(string dataVoucher)
        //{
        //    try
        //    {
        //        // Imprime el voucher
        //        if (!string.IsNullOrEmpty(dataVoucher))
        //        {
        //            // Obtener la configuración de la impresora de voucher
        //            String turnoVoucher = Define.ID_PRINTER_DOCUM_INICIOTURNO;
        //            XmlElement nodoVoucher = impresion.ObtenerNodo(this.xml, turnoVoucher);
        //            String configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodoVoucher, this.listaParamConfig, turnoVoucher);

        //            if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
        //            {
        //                configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        //            }

        //            // Enviar data del voucher a la impresora
        //            String[] parametrosVoucher = new String[]
        //            {
        //                "configImpVoucher=" + configImpVoucher,
        //                "dataVoucher=" + dataVoucher
        //            };

        //            // Llamada para imprimir el voucher
        //            frmPrintNet frmPrintVoucher = new frmPrintNet(parametrosVoucher, listaParamImp);
        //            frmPrintVoucher.ShowDialog(this);

        //            if (!frmPrintVoucher.Resultado.Contains("Impresion"))
        //            {
        //                throw new Exception("Error al imprimir el voucher.");
        //            }
        //        }

        //        MessageBox.Show("Impresión completada con éxito.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error al imprimir: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        ////----------------------------------------------------------------------------------\\

        /// <summary>
        /// Metodo que carga la lista de parametros a imprimir.
        /// </summary>
        private void CargarParametrosImpresion(Hashtable listaParamConfig, Hashtable listaParamImp, List<TurnoMonto> lobjListaMontos)
        {
            listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, this.objUsuario.SNomUsuario + " " + this.objUsuario.SApeUsuario);
            listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, objBOTurno.ObjTurno.SCodTurno);

            string[] listaCodigoMonedas = null;
            // recorrer la lista parametros de configuracion y obtener la lista de codigos
            IDictionaryEnumerator iteraccion = listaParamConfig.GetEnumerator();
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

            listaParamImp.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, lobjListaMontos.Count);

            for (int j = 0; j < lobjListaMontos.Count; j++)
            {
                //indice = listaCodigoMonedas[j].IndexOf("-");
                //codigo = listaCodigoMonedas[j].Substring(indice + 1, listaCodigoMonedas[j].Length - indice - 1);
                //llave = listaCodigoMonedas[j].Substring(0, indice);
                //for (int z = 0; z < listaCaja.Count; z++)
                //{
                //if (listaCaja[z].SCodMoneda.Equals(codigo))
                //{

                string numero = Function.FormatDecimal(lobjListaMontos[j].DImpMontoInicial);
                listaParamImp.Add("monto_moneda" + "_" + j, numero);// + " " + listaCaja[j].Dsc_Simbolo);

                // llave de soles
                //if (llave.Equals("0"))
                //{
                listaParamImp.Add("desc_moneda" + "_" + j, lobjListaMontos[j].DscMoneda);
                listaParamImp.Add("simbolo_moneda" + "_" + j, lobjListaMontos[j].DscSimbolo);
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

        //--EAG

        private void ShowErrorHandler(string strExMessage)
        {
            try
            {
                objBOError.IsError();
                string strMessage = ErrorHandler.Desc_Mensaje == null ? Define.ERR_DEFAULT : ErrorHandler.Desc_Mensaje;
                MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.Trace(strMessage, strExMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Define.ERR_DEFAULT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.Trace(Define.ERR_DEFAULT, ex.Message);
            }
        }

        private void dgwIniMontos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgwIniMontos.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
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

        private void CargarLabels()
        {
            this.Text = (string)LabelConfig.htLabels["turno.lblTitulo"];
        }

        private void TurnoInicio_KeyDown(object sender, KeyEventArgs e)
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
            if (!valido())
            {
                return;
            }
            if (MessageBox.Show((string)LabelConfig.htLabels["turno.msgConfirm"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            if (objUsuario == null)
            {
                MessageBox.Show("Inicie sesion nuevamente");
                return;
            }
            try
            {
                List<TurnoMonto> lobjListaMontos = new List<TurnoMonto>();
                for (int i = 0; i < dgwIniMontos.Rows.Count; i++)
                {
                    TurnoMonto objMonMoneda = new TurnoMonto();
                    objMonMoneda.SCodMoneda = (string)dgwIniMontos.Rows[i].Cells[0].Value;
                    objMonMoneda.DImpMontoInicial = Decimal.Parse((string)dgwIniMontos.Rows[i].Cells[2].Value);
                    objMonMoneda.DImpMontoActual = objMonMoneda.DImpMontoInicial;
                    objMonMoneda.DscMoneda = (string)dgwIniMontos.Rows[i].Cells[1].Value;
                    objMonMoneda.DscSimbolo = (string)dgwIniMontos.Rows[i].Cells[3].Value;
                    lobjListaMontos.Add(objMonMoneda);
                }
                IniciarTurno(lobjListaMontos);
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