using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Collections;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Xml;

namespace LAP.TUUA.VENTAS
{
    public partial class Logueo : Form
    {
        internal static System.Windows.Forms.Timer timerIdle;
        protected BO_Error objBOError;
        protected BO_Turno objBOTurno;
        protected BO_Seguridad objBSeguridad;
        public Principal formPrincipal;
        protected bool Flg_Inactividad;

        public Logueo()
        {
            InitializeComponent();
            CargarLabels();
            objBSeguridad = new BO_Seguridad();
            objBOError = new BO_Error();
            //Inicio de conteo de Inactividad
            SetInactivity();
            
            txtUserCode.Select();
            txtUserCode.Focus();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Usuario objUsuario = null;
            if (!valido())
            {
                return;
            }
            try
            {
                objUsuario = objBSeguridad.autenticar(txtUserCode.Text, txtClave.Text);
                if (objUsuario == null)
                {
                    txtClave.Text = "";
                    MessageBox.Show(objBSeguridad.SErrorMessage, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (chxCambioClave.Checked)
                {
                    objBSeguridad.Num_Intentos = 0;
                    Clave formClave = new Clave(objUsuario);
                    formClave.Show();
                    return;
                }

                if (objUsuario.SFlgCambioClave == "1")
                {
                    objBSeguridad.Num_Intentos = 0;
                    Clave formClave = new Clave(objUsuario);
                    formClave.Show();
                    return;
                }
                //
                CargarParametros();
                CargarVigenciaClave(objUsuario);
                //
                objBSeguridad.InsertarAuditoria(objUsuario.SCodUsuario);
                if (Flg_Inactividad)
                {
                    if (formPrincipal.objUsuario.SCodUsuario != objUsuario.SCodUsuario)
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["logueo.msgTurnoPtoVenta"], "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    this.Hide();
                    this.formPrincipal.Show();
                    Flg_Inactividad = false;
                    return;
                }

                objBOTurno = new BO_Turno();
                string strPtoVenta = objBOTurno.ObtenerPtoVenta();
                CargarListaCampos();
                if (strPtoVenta != null && strPtoVenta.Length > 0)
                {
                    CleanNumIntentos();

                    Turno objTurno = objBOTurno.ObtenerTurnoIniciado(objUsuario.SCodUsuario);
                    if (objTurno != null && objTurno.SCodEquipo == strPtoVenta)
                    {
                        MessageBox.Show("Se abrió el turno pendiente [" + objTurno.SCodTurno + "].", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

                        // obtener parametros de impresion (GGarcia-20090907)
                        BO_ParameGeneral objBOParameGeneral = new BO_ParameGeneral();
                        XmlDocument xml = new XmlDocument();
                        Hashtable listaParamConfig = new Hashtable();
                        objBOParameGeneral.ObtenerParametrosImpresion(listaParamConfig, xml);
                        //seteando parametros de configuracion de impresora leidos de config.properties
                        if (Property.htProperty["ConfigVoucher"] != null && !Property.htProperty["ConfigVoucher"].ToString().Trim().Equals(""))
                        {
                            String configVoucher = Property.htProperty["ConfigVoucher"].ToString().Trim();
                            String[] arrConfigVoucher = configVoucher.Split(',');
                            if (arrConfigVoucher.Length == 5)
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

                        // se agrega parametros de impresion (GGarcia-20090907)
                        //Principal formVentas = new Principal(this.objUsuario, objBOTurno.ObjTurno, this.MyParent, listaPerfil);
                        formPrincipal = new Principal(objUsuario, objTurno, this, objBSeguridad.listaPerfil, listaParamConfig, xml);
                        formPrincipal.Show();
                    }
                    else
                    {
                        TurnoInicio formTurno = new TurnoInicio(objUsuario, this, objBSeguridad.listaPerfil, strPtoVenta);
                        formTurno.Show();
                    }
                }
                else
                {
                    IPHostEntry IPs = Dns.GetHostByName("");
                    IPAddress[] Direcciones = IPs.AddressList;
                    String IpClient = Direcciones[0].ToString();
                    //GeneraAlarma - Estación de Punto de Venta NO REGISTRADA o ANULADA.
                    GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000067", "I20", IpClient, "2", "Alerta W0000067", "Intento de Inicio de Session en un Punto de Ventas no registrado o Anulado, Equipo: " + IpClient + ", Usuario: " + objUsuario.SCodUsuario, objUsuario.SCodUsuario);

                    MessageBox.Show((string)LabelConfig.htLabels["logueo.msgPtoVenta"], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch(Exception ex)
            {
                ShowErrorHandler(ex.Message);
                return;
            }
        }

        private void ShowErrorHandler(string strExMessage)
        {
            try
            {
                objBOError.IsError();
                string strMessage = ErrorHandler.Desc_Mensaje == null ? Define.ERR_DEFAULT : ErrorHandler.Desc_Mensaje;
                MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.Trace(strMessage, strExMessage);
            }
            catch(Exception ex)
            {
                MessageBox.Show(Define.ERR_DEFAULT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.Trace(Define.ERR_DEFAULT, ex.Message);
            }
        }

        public bool valido()
        {
            if (this.txtUserCode.Text == "")
            {
                this.erpUser.SetError(this.txtUserCode, (string)LabelConfig.htLabels["logueo.erpUser"]);
                return false;
            }
            if (this.txtClave.Text == "")
            {
                this.erpClave.SetError(this.txtClave, (string)LabelConfig.htLabels["logueo.erpClave"]);
                return false;
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

        public void Limpiar()
        {
            txtUserCode.Text = "";
            txtClave.Text = "";
            txtUserCode.Select();
            txtUserCode.Focus();
            erpClave.Clear();
            erpUser.Clear();
        }

        private void timerIdle_Tick(object sender, EventArgs e)
        {
            if (formPrincipal != null)
            {
                objBSeguridad.Num_Intentos = 0;
                Flg_Inactividad = true;
                formPrincipal.Hide();
                Limpiar();
                this.Show();
            }
        }

        private void SetInactivity()
        {
            Flg_Inactividad = false;
            timerIdle = new System.Windows.Forms.Timer();
            timerIdle.Enabled = true;
            int intMaxInac = objBSeguridad.ObtenerMaximoInactividad();
            timerIdle.Interval = intMaxInac*1000;
            timerIdle.Tick += new EventHandler(timerIdle_Tick);
        }

        public void CleanNumIntentos()
        {
            this.objBSeguridad.Num_Intentos = 0;
        }

        private void CargarLabels()
        {
            this.Text= (string)LabelConfig.htLabels["logueo.lblTitulo"];
            lblCuenta.Text = (string)LabelConfig.htLabels["logueo.lblUsuario"];
            lblClave.Text = (string)LabelConfig.htLabels["logueo.lblClave"];
        }

        private void Logueo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    if (chxCambioClave.Checked)
                        chxCambioClave.Checked = false;
                    else chxCambioClave.Checked = true;
                    break;
                default: break;
            }
        }

        private void Logueo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void CargarParametros()
        {
            BO_ParameGeneral objBOConfigura = new BO_ParameGeneral();
            DataTable dt_parametro = objBOConfigura.ListarAllParametroGenerales();
            Hashtable htParametro = new Hashtable();
            for (int i = 0; i < dt_parametro.Rows.Count; i++)
            {
                htParametro.Add(dt_parametro.Rows[i].ItemArray.GetValue(0).ToString().Trim(), dt_parametro.Rows[i].ItemArray.GetValue(5).ToString().Trim());
            }
            Property.htParametro = htParametro;
        }

        private void CargarVigenciaClave(Usuario objUsuario)
        {
            DateTime dtFecha = objUsuario.DtFchVigencia;
            string sEstado = objUsuario.STipoEstadoActual;
            DataTable dtFechaHoy = new DataTable();

            dtFechaHoy = objBSeguridad.obtenerFecha();

            string[] sFechaHora = new string[2];

            if (dtFechaHoy.Rows.Count > 0)
            {
                sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
            }

            string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

            DateTime hoy = Convert.ToDateTime(sFecha);


            ClaveUsuHist objClaveUsuHist = new ClaveUsuHist();
            objClaveUsuHist = objBSeguridad.obtenerUsuarioHist(objUsuario.SCodUsuario);

            if (objClaveUsuHist != null)
            {
                string sFechaClave = Fecha.convertSQLToFecha(objClaveUsuHist.SLogFechaMod, objClaveUsuHist.SLogHoraMod);

                DateTime dtFechaClave = Convert.ToDateTime(sFechaClave);

                int DiasVencimiento =Int32.Parse((string)Property.htParametro["VC"]);
                DateTime diaV = dtFechaClave.AddDays(+DiasVencimiento);

                int UmbralAlertDias = Convert.ToInt32((string)Property.htParametro["AE"]);
                DataTable dt_parametro_vence = new DataTable();

                if (hoy <= diaV)
                {

                    // Diferencia de Dias, Horas, Minutos y Segundos.
                    TimeSpan tsDiff = diaV - hoy;
                    // Diferencia de Dias
                    int differenceInDays = tsDiff.Days;

                    if (differenceInDays >= 0)
                    {
                        if (UmbralAlertDias >= differenceInDays)
                        {
                            MessageBox.Show(differenceInDays + " Dias " + tsDiff.Hours + "h " + tsDiff.Minutes + "m " + tsDiff.Seconds + "s", (string)LabelConfig.htLabels["logueo.msgVigencia"], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            } 
        }

        private void CargarListaCampos()
        {
            DataTable dtCampos=objBOTurno.ListarCampos(Define.CAMPO_TIPOPAGO);
            DataTable dtCampos2 = objBOTurno.ListarCampos(Define.CAMPO_TIPOPSJERO);
            Hashtable htListaCampos=new Hashtable();
            dtCampos.Merge(dtCampos2);
            for(int i=0;i<dtCampos.Rows.Count;i++)
            {
                htListaCampos.Add(dtCampos.Rows[i].ItemArray.GetValue(0).ToString() + dtCampos.Rows[i].ItemArray.GetValue(1).ToString(), dtCampos.Rows[i].ItemArray.GetValue(3).ToString());
            }
            Property.htListaCampos = htListaCampos;
        }

    }
}