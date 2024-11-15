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
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.VENTAS
{
    public partial class Clave : Form
    {
        protected Usuario objUsuario;
        protected BO_Error objBOError;
        protected bool boError;

        public Clave(Usuario objUsuario)
        {
            InitializeComponent();
            this.objUsuario = objUsuario;
            boError = false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!valido())
            {
                return;
            }
            if (MessageBox.Show("¿Está seguro de realizar esta acción?", "Confirmación", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                objBOError = new BO_Error();
                BO_Seguridad objBSeguridad = new BO_Seguridad();
                
                this.objUsuario.SPwdActualUsuario = txtClave.Text;
                //this.objUsuario.SFlgCambioClave = "1";
                if (objBSeguridad.CambiarClave(objUsuario))
                {
                    IPHostEntry IPs = Dns.GetHostByName("");
                    IPAddress[] Direcciones = IPs.AddressList;
                    String IpClient = Direcciones[0].ToString();
                    //GeneraAlarma - Cambio de contraseña desde el modulo de Ventas
                    GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000073", "I20", IpClient, "1", "Alerta W0000073", "Cambio de contraseña desde el modulo de ventas, usuario: " + objUsuario.SCodUsuario, "");
                
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