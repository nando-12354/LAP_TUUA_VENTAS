using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Net;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public partial class LoginForm : Form
    {
        internal static Timer timerIdle;
        private BO_Seguridad objBSeguridad;
        public Archieving frmArchieving;
        private bool flagInactividad;

        public LoginForm()
        {
            InitializeComponent();
            CargarLabels();
            objBSeguridad = new BO_Seguridad();

            flagInactividad = false;    //EAG -> 17/12/2009
            //SetInactivity();          //EAG -> 17/12/2009

            txtUserCode.Select();
            txtUserCode.Focus();
        }

        private void SetInactivity()
        {
            flagInactividad = false;

            timerIdle = new System.Windows.Forms.Timer();
            timerIdle.Tick += new EventHandler(timerIdle_Tick);
            int intMaxInac = objBSeguridad.ObtenerMaximoInactividad();
            timerIdle.Interval = intMaxInac * 1000;
            timerIdle.Enabled = true;
        }

        private void timerIdle_Tick(object sender, EventArgs e)
        {
            if (frmArchieving != null && frmArchieving.Visible)
            {
                //objBSeguridad.NumIntentos = 0;    //No es necesario
                flagInactividad = true;
                frmArchieving.Hide();
                Limpiar();
                Show();
                //this.ShowInTaskbar = true;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!valido())
            {
                return;
            }
            try
            {
                Usuario objUsuario;

                //EAG -> 17/12/2009
                /*
                if (flagInactividad)
                {
                    if(frmArchieving.objUsuario.SCtaUsuario != txtUserCode.Text)
                    {
                        MessageBox.Show((string)LabelConfig.htLabels["logueo.IncorrectUser"], "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;                        
                    }
                    objUsuario = objBSeguridad.autenticar(txtUserCode.Text, txtClave.Text);
                    if (objUsuario == null)
                    {
                        MessageBox.Show(objBSeguridad.ErrorMessage, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    Hide();
                    frmArchieving.ShowDialog(this);
                    flagInactividad = false;//No seguro si es necesario
                    return;
                }
                */

                String codUser = "";

                objUsuario = objBSeguridad.autenticar(txtUserCode.Text, txtClave.Text, ref codUser);

                String hostIP = "";

                try
                {
                    String hostName = Dns.GetHostName();
                    IPAddress[] ipAddress = Dns.GetHostAddresses(hostName);

                    if (ipAddress.Length > 0)
                    {
                        hostIP = ipAddress[0].ToString();
                    }

                    /*
                    for (int i = 0; i < ipAddress.Length; i++)
                    {
                        if(i>0)
                        {
                            hostIP = hostIP + ";" + ipAddress[i].ToString();
                        }
                        else
                        {
                            hostIP = ipAddress[i].ToString();
                        }
                    }
                    */

                }
                catch(Exception ex)
                {
                    ErrorHandler.Trace("Error al intentar obtener la IP de la pc.", "btnAceptar_Click: Error al intentar obtener la IP de la pc. - ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                }

                String UserDomainName = "";
                String UserName = "";

                try
                {
                    UserDomainName = System.Environment.UserDomainName;
                    UserName = System.Environment.UserName;
                }
                catch(Exception ex)
                {
                    ErrorHandler.Trace("Error al intentar datos de dominio y/o usuario windows.", "btnAceptar_Click: Error al intentar datos de dominio y/o usuario windows. - ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                }

                if (objUsuario == null && !codUser.Equals(""))//Login fail, o user bloqueado, o otras razones...
                {
                    LogAcceso objLogAcceso = new LogAcceso();
                    objLogAcceso.SLogModulo = "ARC";
                    objLogAcceso.SLogUsuario = codUser;
                    objLogAcceso.SLogProceso = "LOGIN";
                    objLogAcceso.SLogTipo = "";
                    objLogAcceso.SLogEstado = "0";
                    objLogAcceso.SCodUsuario = codUser;
                    objLogAcceso.SLogUsuarioWindows = UserName;
                    objLogAcceso.SLogDominioWindows = UserDomainName;
                    objLogAcceso.SLogIp = hostIP;

                    objBSeguridad.InsertarAccesos(objLogAcceso);                    
                }
                else if (objUsuario != null) //fue correcto
                {
                    LogAcceso objLogAcceso = new LogAcceso();
                    objLogAcceso.SLogModulo = "ARC";
                    objLogAcceso.SLogUsuario = objUsuario.SCodUsuario;
                    objLogAcceso.SLogProceso = "LOGIN";
                    objLogAcceso.SLogTipo = "";
                    objLogAcceso.SLogEstado = "1";
                    objLogAcceso.SCodUsuario = objUsuario.SCodUsuario;
                    objLogAcceso.SLogUsuarioWindows = UserName;
                    objLogAcceso.SLogDominioWindows = UserDomainName;
                    objLogAcceso.SLogIp = hostIP;

                    objBSeguridad.InsertarAccesos(objLogAcceso);                    
                }

                if (objUsuario == null)
                {
                    MessageBox.Show(objBSeguridad.ErrorMessage, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CleanNumIntentos();

                objBSeguridad.InsertarAuditoria("ARC", "LOGIN", objUsuario.SCodUsuario, "1");

                frmArchieving = new Archieving(objUsuario, this, objBSeguridad.listaPerfil);
                frmArchieving.RefreshArchieving();

                Hide();        //EAG -> 21/12/2009

                frmArchieving.ShowDialog(this);
                //frmArchieving.ShowInTaskbar = false;
                //this.ShowInTaskbar = false;

                //Hide();         //EAG -> 17/12/2009

                //Close();        //EAG -> 17/12/2009 (Agregado pero luego comentado 21/12/2009)
            }
            catch (Exception ex)
            {
                String strMessage = ErrorHandler.Desc_Mensaje == null ? Define.ERR_DEFAULT : ErrorHandler.Desc_Mensaje;
                MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrorHandler.Trace(strMessage, "btnAceptar_Click: strMessage: " + strMessage + " - ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                return;
            }

        }

        public bool valido()
        {
            if (this.txtUserCode.Text == "")
            {
                errPUsuario.SetError(this.txtUserCode, (string)LabelConfig.htLabels["logueo.erpUser"]);
                return false;
            }
            if (this.txtClave.Text == "")
            {
                errPClave.SetError(this.txtClave, (string)LabelConfig.htLabels["logueo.erpClave"]);
                return false;
            }
            return true;
        }

        public void CleanNumIntentos()
        {
            this.objBSeguridad.CleanNumIntentos();
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
        }

        private void CargarLabels()
        {
            Text = (string)LabelConfig.htLabels["logueo.lblTitulo"];
            lblCuenta.Text = (string)LabelConfig.htLabels["logueo.lblUsuario"];
            lblClave.Text = (string)LabelConfig.htLabels["logueo.lblClave"];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
