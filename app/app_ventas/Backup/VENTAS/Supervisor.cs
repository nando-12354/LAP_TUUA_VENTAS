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

namespace LAP.TUUA.VENTAS
{
    public partial class Supervisor : Form
    {
        protected BO_Error objBOError;
        protected BO_Turno objBOTurno;
        protected BO_Seguridad objBSeguridad;
        protected bool Flg_Inactividad;
        protected Cierre frmCierre;

        public Supervisor(Cierre frmCierre)
        {
            InitializeComponent();
            CargarLabels();
            objBSeguridad = new BO_Seguridad();
            objBOError = new BO_Error();
            this.frmCierre =frmCierre;
            
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
                objUsuario = objBSeguridad.AutenticarSupervisor(txtUserCode.Text, txtClave.Text);
                if (objUsuario == null)
                {
                    MessageBox.Show(objBSeguridad.SErrorMessage, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                frmCierre.Flg_OKCierreTurno = true;
                Close();
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
            this.txtUserCode.Text = "";
            this.txtClave.Text = "";
        }

        public void CleanNumIntentos()
        {
            this.objBSeguridad.Num_Intentos = 0;
        }

        private void CargarLabels()
        {
            this.Text= (string)LabelConfig.htLabels["supervisor.lblTitulo"];
            lblCuenta.Text = (string)LabelConfig.htLabels["logueo.lblUsuario"];
            lblClave.Text = (string)LabelConfig.htLabels["logueo.lblClave"];
        }

    }
}
