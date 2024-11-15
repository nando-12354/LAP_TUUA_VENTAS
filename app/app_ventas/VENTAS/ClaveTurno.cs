using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.VENTAS
{
    public partial class ClaveTurno : Form
    {
        protected Usuario objUsuario;
        protected BO_Error objBOError;
        protected bool boError;
        protected Principal formMyParent;

        public ClaveTurno(Usuario objUsuario, Principal formMyParent)
        {
            InitializeComponent();
            this.objUsuario = objUsuario;
            boError = false;
            this.formMyParent = formMyParent;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            formMyParent.VerificarTurnoActivo();
            if (!valido())
            {
                return;
            }
            if (MessageBox.Show("¿Está seguro de realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                objBOError = new BO_Error();
                BO_Seguridad objBSeguridad = new BO_Seguridad();
                this.objUsuario.SPwdActualUsuario = txtClave.Text;
                this.objUsuario.SFlgCambioClave = "1";
                if (objBSeguridad.CambiarClave(objUsuario))
                {
                    MessageBox.Show("La nueva clave fue actualizada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(objBSeguridad.SErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool valido()
        {
            if (txtClave.Text == "")
            {
                this.erpClave.SetError(txtClave, "Ingrese su clave.");
                return false;
            }
            if (txtNuevaClave.Text == "")
            {
                this.erpNClave.SetError(txtNuevaClave, "Confirme su nueva clave.");
                return false;
            }
            if (txtClave.Text != txtNuevaClave.Text)
            {
                this.erpClave.SetError(txtClave, "Ingrese claves iguales.");
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}