using System;
using System.IO;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;
using System.Net;
using LAP.TUUA.PRINTER;
using System.Collections;
using System.Xml;

namespace LAP.TUUA.VENTAS
{
    public partial class Cierre : Form
    {
        protected Usuario objUsuario;
        protected Turno objTurno;
        protected Principal formMyParent;
        protected Logueo formLogueo;
        protected BO_Turno objBOTurno;
        protected BO_Error objBOError;
        protected BO_Seguridad seguridad;
        protected List<TurnoMonto> lobjListaMontos;
        protected List<CuadreTurno> listaCuadre;
        protected List<Moneda> listaMoneda;
        protected bool boError;
        public bool Flg_OKCierreTurno;

        protected decimal Imp_EfectivoIni;
        protected int Can_TicketInt;
        protected int Can_TicketNac;
        protected decimal Imp_TicketInt;
        protected decimal Imp_TicketNac;
        protected int Can_IngCaja;
        protected decimal Imp_IngCaja;
        protected int Can_VentaMoneda;
        protected decimal Imp_VentaMoneda;
        protected int Can_EgreCaja;
        protected decimal Imp_EgreCaja;
        protected int Can_CompraMoneda;
        protected decimal Imp_CompraMoneda;
        protected decimal Imp_EfectivoFinal;
        protected int Can_AnulaInt;
        protected int Can_AnulaNac;
        protected int Can_InfanteInt;
        protected int Can_InfanteNac;
        protected int Can_CreditoInt;
        protected int Can_CreditoNac;
        protected decimal Imp_CreditoInt;
        protected decimal Imp_CreditoNac;

        //--EAG 30/01/2010
        // parametros de impresion
        // lista parametros de configuracion
        private Hashtable listaParamConfig;
        // lista parametros a imprimir
        private Hashtable listaParamImp;
        // xml
        private XmlDocument xml;
        // instancia de impresion
        private Print impresion;

        public Cierre(Turno objTurno, Hashtable listaParamConfig, Hashtable listaParamImp, XmlDocument xml, Print impresion, Principal formMyParent, Logueo formLogueo)
        {
            InitializeComponent();
            CargarLabels();
            txtUserCode.Select();
            txtUserCode.Focus();
            boError = false;
            objBOTurno = new BO_Turno();
            seguridad = new BO_Seguridad();
            listaMoneda = objBOTurno.ListarMonedas();
            this.dgwCierre.DataSource = listaMoneda;
            this.objTurno = objTurno;
            this.formMyParent = formMyParent;
            this.formLogueo = formLogueo;
            Flg_OKCierreTurno = false;

            //--EAG 30/01/2010
            // inicializar parametros de impresion
            // lista parametros de config
            this.listaParamConfig = listaParamConfig;
            // lista parametros a imprimir
            this.listaParamImp = listaParamImp;
            // xml
            this.xml = xml;
            // instancia de impresion
            this.impresion = impresion;
            //--EAG 30/01/2010
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool FlgCierreDescuadre = false;

            formMyParent.VerificarTurnoActivo();
            if (!valido())
            {
                return;
            }

            try
            {
                objBOError = new BO_Error();
                BO_Turno turno = new BO_Turno();

                //turno.ListarCuadreTurno("DOL", objTurno.SCodTurno, ref  Imp_EfectivoIni, ref  Can_TicketInt, ref  Can_TicketNac, ref  Imp_TicketInt, ref  Imp_TicketNac, ref  Can_IngCaja, ref  Imp_IngCaja, ref  Can_VentaMoneda, ref  Imp_VentaMoneda, ref  Can_EgreCaja, ref  Imp_EgreCaja, ref  Can_CompraMoneda, ref  Imp_CompraMoneda, ref  Imp_EfectivoFinal, ref Can_AnulaInt, ref  Can_AnulaNac, ref Can_InfanteInt, ref  Can_InfanteNac, ref  Can_CreditoInt, ref  Can_CreditoNac, ref  Imp_CreditoInt, ref  Imp_CreditoNac);

                objUsuario = seguridad.autenticar(txtUserCode.Text, txtClave.Text);
                if (objUsuario == null)
                {
                    MessageBox.Show(seguridad.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (seguridad.Flg_Bloqueado)
                    {
                        formMyParent.Flg_Bloqueado = true;
                        formMyParent.Close();
                        formLogueo.Limpiar();
                        formLogueo.Show();
                    }
                    return;
                }
                if (objUsuario.SCodUsuario != this.objTurno.SCodUsuario)
                {
                    MessageBox.Show("Usuario [" + objUsuario.SCtaUsuario + "] no ha iniciado Turno[" + objTurno.SCodTurno + "].", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (turno.VerificarCuadre(lobjListaMontos, objUsuario.SCodUsuario, this.objTurno.SCodTurno) != 0)
                {
                    if (turno.Flg_ExcesoDescuadre)
                    {
                        if (!Flg_OKCierreTurno)
                        {
                            MessageBox.Show(turno.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Flg_OKCierreTurno = false;
                            Supervisor frmSupervisor = new Supervisor(this);
                            frmSupervisor.Show();
                            return;
                        }
                    }
                    if (!turno.CierreTurnoDescuadre())
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["cierre.msgFlagDescuadre"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        FlgCierreDescuadre = true;
                    }

                    if (MessageBox.Show(turno.SErrorMessage + "\n¿Está seguro de realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                }
                else
                {
                    if (MessageBox.Show((string)LabelConfig.htLabels["cierre.msgConfirm"], "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
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


                if (turno.CerrarTurno(lobjListaMontos, objUsuario.SCodUsuario, this.objTurno.SCodTurno))
                {
                    listaCuadre = new List<CuadreTurno>();
                    int intTicketEfeInt=0;
                    decimal decTicketEfeInt=0;
                    int intTicketTraInt=0;
                    decimal decTicketTraInt=0;
                    int intTicketDebInt=0;
                    decimal decTicketDebInt=0;
                    int intTicketCreInt=0;
                    decimal decTicketCreInt=0;
                    int intTicketCheInt=0;
                    
                    int intTicketEfeNac=0;
                    decimal decTicketEfeNac=0;
                    int intTicketTraNac=0;
                    decimal decTicketTraNac=0;
                    int intTicketDebNac=0;
                    decimal decTicketDebNac=0;
                    int intTicketCreNac=0;
                    decimal decTicketCreNac=0;
                    int intTicketCheNac=0;
                    decimal decTicketCheNac=0;
                    decimal decRecaudadoFin=0;

                    for (int k = 0; k < listaMoneda.Count; k++)
                    {
                        decimal decTicketCheInt = 0;
                        CuadreTurno objCuadre = new CuadreTurno();
                        objCuadre.Cod_Moneda = listaMoneda[k].SCodMoneda;
                        objCuadre.Dsc_Moneda = listaMoneda[k].SDscMoneda;
                        objCuadre.Dsc_Simbolo = listaMoneda[k].SDscSimbolo;
                        turno.ListarCuadreTurno(objCuadre.Cod_Moneda, objTurno.SCodTurno, ref  Imp_EfectivoIni, ref  Can_TicketInt, ref  Can_TicketNac, ref  Imp_TicketInt, ref  Imp_TicketNac, ref  Can_IngCaja, ref  Imp_IngCaja, ref  Can_VentaMoneda, ref  Imp_VentaMoneda, ref  Can_EgreCaja, ref  Imp_EgreCaja, ref  Can_CompraMoneda, ref  Imp_CompraMoneda, ref  Imp_EfectivoFinal, ref Can_AnulaInt, ref  Can_AnulaNac, ref Can_InfanteInt, ref  Can_InfanteNac, ref  Can_CreditoInt, ref  Can_CreditoNac, ref  Imp_CreditoInt, ref  Imp_CreditoNac,
                              ref  intTicketEfeInt, ref  decTicketEfeInt, ref  intTicketTraInt, ref  decTicketTraInt, ref  intTicketDebInt, ref  decTicketDebInt, ref  intTicketCreInt, ref  decTicketCreInt, ref  intTicketCheInt, ref  decTicketCheInt,
                              ref  intTicketEfeNac, ref  decTicketEfeNac, ref  intTicketTraNac, ref  decTicketTraNac, ref  intTicketDebNac, ref  decTicketDebNac, ref  intTicketCreNac, ref  decTicketCreNac, ref  intTicketCheNac, ref  decTicketCheNac,
                              ref  decRecaudadoFin
                            );
                        objCuadre.Can_AnulaInt = Can_AnulaInt;
                        objCuadre.Can_AnulaNac = Can_AnulaNac;
                        objCuadre.Can_CompraMoneda = Can_CompraMoneda;
                        objCuadre.Can_CreditoInt = Can_CreditoInt;
                        objCuadre.Can_CreditoNac = Can_CreditoNac;
                        objCuadre.Can_EgreCaja = Can_EgreCaja;
                        objCuadre.Can_InfanteInt = Can_InfanteInt;
                        objCuadre.Can_InfanteNac = Can_InfanteNac;
                        objCuadre.Can_IngCaja = Can_IngCaja;
                        objCuadre.Can_TicketInt = Can_TicketInt;
                        objCuadre.Can_TicketNac = Can_TicketNac;
                        objCuadre.Imp_CompraMoneda = Imp_CompraMoneda;
                        objCuadre.Imp_CreditoInt = Imp_CreditoInt;
                        objCuadre.Imp_CreditoNac = Imp_CreditoNac;
                        objCuadre.Imp_EfectivoFinal = Imp_EfectivoFinal;
                        objCuadre.Imp_EfectivoIni = Imp_EfectivoIni;
                        objCuadre.Imp_EgreCaja = Imp_EgreCaja;
                        objCuadre.Imp_TicketInt = Imp_TicketInt;
                        objCuadre.Imp_TicketNac = Imp_TicketNac;
                        objCuadre.Imp_IngCaja = Imp_IngCaja;
                        objCuadre.Can_VentaMoneda = Can_VentaMoneda;
                        objCuadre.Imp_VentaMoneda = Imp_VentaMoneda;

                        objCuadre.Can_Ticket_EfeInt = intTicketEfeInt;
                        objCuadre.Imp_Ticket_EfeInt = decTicketEfeInt;
                        objCuadre.Can_Ticket_TraInt = intTicketTraInt;
                        objCuadre.Imp_Ticket_TraInt = decTicketTraInt;
                        objCuadre.Can_Ticket_DebInt = intTicketDebInt;
                        objCuadre.Imp_Ticket_DebInt = decTicketDebInt;
                        objCuadre.Can_Ticket_CreInt = intTicketCreInt;
                        objCuadre.Imp_Ticket_CreInt = decTicketCreInt;
                        objCuadre.Can_Ticket_CheInt = intTicketCheInt;
                        objCuadre.Imp_Ticket_CheInt = decTicketCheInt;
                        objCuadre.Can_Ticket_EfeNac = intTicketEfeNac;
                        objCuadre.Imp_Ticket_EfeNac = decTicketEfeNac;
                        objCuadre.Can_Ticket_TraNac = intTicketTraNac;
                        objCuadre.Imp_Ticket_TraNac = decTicketTraNac;
                        objCuadre.Can_Ticket_DebNac = intTicketDebNac;
                        objCuadre.Imp_Ticket_DebNac = decTicketDebNac;
                        objCuadre.Can_Ticket_CreNac = intTicketCreNac;
                        objCuadre.Imp_Ticket_CreNac = decTicketCreNac;
                        objCuadre.Can_Ticket_CheNac = intTicketCheNac;
                        objCuadre.Imp_Ticket_CheNac = decTicketCheNac;
                        objCuadre.Imp_Recaudado_Fin = decRecaudadoFin;

                        for (int l = 0; l < lobjListaMontos.Count; l++)
                        {
                            if (lobjListaMontos[l].SCodMoneda == objCuadre.Cod_Moneda)
                            {
                                objCuadre.Imp_EfecFaltante = Function.FormatDecimal((Imp_EfectivoFinal - lobjListaMontos[l].DImpMontoFinal > 0 ? Imp_EfectivoFinal - lobjListaMontos[l].DImpMontoFinal : 0) * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                                objCuadre.Imp_EfecSobrante = Function.FormatDecimal((lobjListaMontos[l].DImpMontoFinal - Imp_EfectivoFinal > 0 ? lobjListaMontos[l].DImpMontoFinal - Imp_EfectivoFinal : 0) * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                                break;
                            }
                        }
                        listaCuadre.Add(objCuadre);
                    }

                    //--EAG 30/01/2010
                    Imprimir();
                    //--EAG 30/01/2010

                    MessageBox.Show("Turno [" + this.objTurno.SCodTurno + "] cerrado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    formMyParent.Flg_Salir = true;
                    this.formMyParent.Close();
                    this.formLogueo.Limpiar();
                    this.formLogueo.Show();

                    IPHostEntry IPs = Dns.GetHostByName("");
                    IPAddress[] Direcciones = IPs.AddressList;
                    String IpClient = Direcciones[0].ToString();

                    if (FlgCierreDescuadre)
                    {
                        //GeneraAlarma - Cierre de turno con descuadre
                        GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000065", "I21", IpClient, "2", "Alerta W0000065", "Cierre de turno con Descuadre (Monto final esperado por el sistema es diferente al monto real en Caja), usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);
                    }
                }
                else
                {
                    MessageBox.Show(turno.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //EAG 11/02/2010
        private int VerificacionImpresora()
        {
            try
            {
                // 1ro: ************************************** Testear VOUCHER **************************************
                // obtiene el nodo segun el nombre
                XmlElement nodo = impresion.ObtenerNodo(xml, Define.ID_PRINTER_DOCUM_CIERRETURNO_MN);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, listaParamConfig, Define.ID_PRINTER_DOCUM_CIERRETURNO_MN);

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
                Console.WriteLine("Excepcion Cierre.VerificacionImpresora() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgErrorVerifVoucher"], ex);
            }
            return 0;
        }
        //EAG 11/02/2010


        private void Imprimir()
        {
            try
            {
                // 1ro: ************************************** Imprimir VOUCHER CUADRE M.N.**************************************
                // obtiene el nodo segun el nombre
                String nombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_MN;
                XmlElement nodo = impresion.ObtenerNodo(this.xml, nombre);

                //int copias = impresion.ObtenerCopiasVoucher(nodo);

                // obtiene la configuracion de la impresora a utilizar
                string configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);

                if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                {
                    configImpVoucher = Property.puertoVoucher.Trim() + "," +
                                       configImpVoucher.Split(new char[] { ',' }, 2)[1];
                }

                CargarParametrosComunesImpresion();
                for (int i = 0; i < listaCuadre.Count; i++)
                {
                    string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
                    if (listaCuadre[i].Cod_Moneda.Equals(strMonNac))
                    {
                        CargarParametros_MN_Impresion(listaCuadre[i]);
                        break;
                    }
                }


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

                    //MostrarVistaPrevia(configImpVoucher, dataVoucher);

                    //MessageBox.Show((string) LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (resultado.Contains("Salir"))
                {
                    MessageBox.Show((string)LabelConfig.htLabels["cierreturno.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //----------EAG
                    listaParamImp.Clear();
                    return;
                    //----------EAG
                }
                else
                {
                    MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //----------EAG
                    listaParamImp.Clear();
                    return;
                    //----------EAG
                }
                listaParamImp.Clear();


                // 2do: ************************************** Imprimir VOUCHER CUADRE M.E.**************************************

                nombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_ME;
                nodo = impresion.ObtenerNodo(this.xml, nombre);

                //copias = impresion.ObtenerCopiasVoucher(nodo);

                configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodo, this.listaParamConfig, nombre);


                //String[] monedasExtranjeras = new string[1]{"Dolares"};


                for (int i = 0; i < listaCuadre.Count; i++)
                {
                    string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
                    if (listaCuadre[i].Cod_Moneda.Equals(strMonNac))
                    {
                        continue;
                    }

                    if (Property.puertoVoucher != null && !Property.puertoVoucher.Trim().Equals(String.Empty))
                    {
                        configImpVoucher = Property.puertoVoucher.Trim() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
                    }

                    CargarParametrosComunesImpresion();
                    CargarParametros_ME_Impresion(listaCuadre[i]);

                    dataVoucher = impresion.ObtenerDataFormateada(listaParamImp, nodo);

                    parametros = new String[]
                                     {
                                         "flagImpSticker=0",
                                         "flagImpVoucher=1",
                                         "dataVoucher=" + dataVoucher,
                                         "configImpVoucher=" + configImpVoucher
                                         //"copiasVoucher=" + copias
                                     };

                    frmPrintNet = new frmPrintNet(parametros);
                    frmPrintNet.ShowDialog(this);

                    //********************************
                    resultado = frmPrintNet.Resultado;

                    if (resultado.Contains("Impresion"))
                    {
                        Property.puertoVoucher = frmPrintNet.PuertoSerial.getConfigImpVoucher()[0];

                        //MostrarVistaPrevia(configImpVoucher, dataVoucher);

                        //MessageBox.Show((string)LabelConfig.htLabels["ingreso.msgTrxOK"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (resultado.Contains("Salir"))
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["cierreturno.msgCancel"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //----------EAG
                        listaParamImp.Clear();
                        return;
                        //----------EAG
                    }
                    else
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //----------EAG
                        listaParamImp.Clear();
                        return;
                        //----------EAG
                    }
                    listaParamImp.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show((string)LabelConfig.htLabels["impresion.msgError"], "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Excepcion Cierre.Imprimir() : " + ex.Message);
                ErrorHandler.Trace((string)LabelConfig.htLabels["impresion.msgError"], ex);
            }
            listaParamImp.Clear();
        }

        ////-------METODO PARA VISUALIZAR EL VOUCHER EN PANTALLA -------\\
        //private void MostrarVistaPrevia(string dataSticker, string dataVoucher)
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
        //        ImprimirDocumento(dataSticker, dataVoucher);
        //    }
        //}

        //private string FormatearVoucher(string dataVoucher)
        //{
        //    return dataVoucher.Replace("@", "\n").Replace(":", ": ").Replace("  ", " ");
        //}
        //private void ImprimirDocumento(string dataSticker, string dataVoucher)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(dataSticker))
        //        {
        //            String nombreSticker = Define.ID_PRINTER_DOCUM_CIERRETURNO_ME;
        //            XmlElement nodoSticker = impresion.ObtenerNodo(this.xml, nombreSticker);
        //            String configImpSticker = impresion.ObtenerConfiguracionImpresora(nodoSticker, this.listaParamConfig, nombreSticker);

        //            if (Property.puertoSticker != null && !Property.puertoSticker.Trim().Equals(String.Empty))
        //            {
        //                configImpSticker = Property.puertoSticker.Trim() + "," + configImpSticker.Split(new char[] { ',' }, 2)[1];
        //            }

        //            // Enviar data del sticker a la impresora
        //            String[] parametrosSticker = new String[]
        //            {
        //                "configImpSticker=" + configImpSticker,
        //                "dataSticker=" + dataSticker
        //            };

        //            // Llamada para imprimir el sticker
        //            frmPrintNet frmPrintSticker = new frmPrintNet(parametrosSticker, listaParamImp);
        //            frmPrintSticker.ShowDialog(this);

        //            if (!frmPrintSticker.Resultado.Contains("Impresion"))
        //            {
        //                throw new Exception("Error al imprimir el sticker.");
        //            }
        //        }

        //        // Imprime el voucher
        //        if (!string.IsNullOrEmpty(dataVoucher))
        //        {
        //            // Obtener la configuración de la impresora de voucher
        //            String nombreVoucher = Define.ID_PRINTER_DOCUM_CIERRETURNO_ME;
        //            XmlElement nodoVoucher = impresion.ObtenerNodo(this.xml, nombreVoucher);
        //            String configImpVoucher = impresion.ObtenerConfiguracionImpresora(nodoVoucher, this.listaParamConfig, nombreVoucher);

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

        private void CargarParametrosComunesImpresion()
        {
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, this.objUsuario.SNomUsuario + " " + this.objUsuario.SApeUsuario);
            this.listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, this.objTurno.SCodTurno);
        }

        private void CargarParametros_MN_Impresion(CuadreTurno cuadreTurno)
        {
            //listaParamImp.Add("nombre_cajero", "Juan Lara");
            //listaParamImp.Add("codigo_turno", "TU0001");
            listaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
            listaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
            listaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
            listaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);

            listaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
            listaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
            listaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
            listaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
            listaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
            listaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
            listaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
            listaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
            listaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
            listaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);


            listaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
            listaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

            listaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
            listaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
            listaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
            listaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
            listaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
            listaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
            listaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
            listaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
            listaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
            listaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

            listaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
            listaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

            listaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
            listaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

            listaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
            listaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

            listaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
            listaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

            listaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
            listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
            listaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
            listaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
            listaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
            listaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

            listaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
            listaParamImp.Add("efectivo_faltante", "(-)"+cuadreTurno.Imp_EfecFaltante.ToString());

            listaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
            listaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
            listaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
            listaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
        }

        private void CargarParametros_ME_Impresion(CuadreTurno cuadreTurno)
        {
            listaParamImp.Add("moneda_internacional", cuadreTurno.Dsc_Moneda);

            //listaParamImp.Add("nombre_cajero", "Juan Lara");
            //listaParamImp.Add("codigo_turno", "TU0001");
            listaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
            listaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
            listaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
            listaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);

            listaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
            listaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
            listaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
            listaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
            listaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
            listaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
            listaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
            listaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
            listaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
            listaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);

            listaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
            listaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

            listaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
            listaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
            listaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
            listaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
            listaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
            listaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
            listaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
            listaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
            listaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
            listaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

            listaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
            listaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

            listaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
            listaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

            listaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
            listaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

            listaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
            listaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

            listaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
            listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
            listaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
            listaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
            listaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
            listaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

            listaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
            listaParamImp.Add("efectivo_faltante", "(-)" + cuadreTurno.Imp_EfecFaltante.ToString());

            listaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
            listaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
            listaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
            listaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
        }

        public bool valido()
        {
            ClearErrProvider();
            if (this.txtUserCode.Text == "")
            {
                this.erpUser.SetError(this.txtUserCode, (string)LabelConfig.htLabels["cierre.erpUser"]);
                return false;
            }
            if (this.txtClave.Text == "")
            {
                this.erpClave.SetError(this.txtClave, (string)LabelConfig.htLabels["cierre.erpClave"]);
                return false;
            }
            lobjListaMontos = new List<TurnoMonto>();

            for (int i = 0; i < dgwCierre.Rows.Count; i++)
            {
                try
                {
                    TurnoMonto objMonMoneda = new TurnoMonto();
                    objMonMoneda.SCodMoneda = (string)dgwCierre.Rows[i].Cells[0].Value;
                    objMonMoneda.DImpMontoFinal = Decimal.Parse((string)dgwCierre.Rows[i].Cells[2].Value);
                    objMonMoneda.Imp_Transferencia = Decimal.Parse((string)dgwCierre.Rows[i].Cells[3].Value);
                    objMonMoneda.Imp_Cheque = Decimal.Parse((string)dgwCierre.Rows[i].Cells[4].Value);
                    objMonMoneda.Imp_Tarjeta = Decimal.Parse((string)dgwCierre.Rows[i].Cells[5].Value);
                    if (objMonMoneda.DImpMontoFinal < 0)
                    {
                        erpMontos.SetError(dgwCierre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                        return false;
                    }
                    if (objMonMoneda.Imp_Transferencia < 0)
                    {
                        erpMontos.SetError(dgwCierre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                        return false;
                    }
                    if (objMonMoneda.Imp_Cheque < 0)
                    {
                        erpMontos.SetError(dgwCierre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                        return false;
                    }
                    if (objMonMoneda.Imp_Tarjeta < 0)
                    {
                        erpMontos.SetError(dgwCierre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                        return false;
                    }
                    objMonMoneda.DImpMontoInicial = objMonMoneda.DImpMontoFinal;
                    lobjListaMontos.Add(objMonMoneda);
                }
                catch (Exception ex)
                {
                    erpMontos.SetError(dgwCierre, (string)LabelConfig.htLabels["cierre.erpMontos"]);
                    return false;
                }
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            VerificarKeys(e);
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            VerificarKeys(e);
        }

        public void VerificarKeys(KeyPressEventArgs e)
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

        private void dgwCierre_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgwCierre.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
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

        private void Cierre_Load(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PE");
        }

        private void CargarLabels()
        {
            lblCuenta.Text = (string)LabelConfig.htLabels["cierre.lblUsuario"];
            lblClave.Text = (string)LabelConfig.htLabels["cierre.lblClave"];
        }

        private void ClearErrProvider()
        {
            erpClave.Clear();
            erpMontos.Clear();
            erpUser.Clear();
        }

        private void HabilitarFormaPago()
        {
            //cambios configurable forma de pago
            if (Property.htProperty["CREDITO"].ToString() != Define.TIP_ACTIVO && Property.htProperty["DEBITO"].ToString() != Define.TIP_ACTIVO)
            {
                dgvtbxTar.ReadOnly = true;
            }
            if (Property.htProperty["TRANSFERENCIA"].ToString() != Define.TIP_ACTIVO)
            {
                dgvtbxTran.ReadOnly = true;
            }
            else
            {
                dgvtbxTran.ReadOnly = false;
            }
            if (Property.htProperty["CHEQUE"].ToString() != Define.TIP_ACTIVO)
            {
                dgvtbxCheq.ReadOnly = true;
            }
            //fin configurable forma de pago
        }
    }
}