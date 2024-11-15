using LAP.TUUA.UTIL;
namespace LAP.TUUA.VENTAS
{
    partial class Logueo
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Logueo));
            this.gbxAutenticar = new System.Windows.Forms.GroupBox();
            this.chxCambioClave = new System.Windows.Forms.CheckBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.pbxLogin = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.erpUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxAutenticar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxAutenticar
            // 
            this.gbxAutenticar.Controls.Add(this.chxCambioClave);
            this.gbxAutenticar.Controls.Add(this.lblClave);
            this.gbxAutenticar.Controls.Add(this.lblCuenta);
            this.gbxAutenticar.Controls.Add(this.txtClave);
            this.gbxAutenticar.Controls.Add(this.txtUserCode);
            this.gbxAutenticar.Location = new System.Drawing.Point(12, 23);
            this.gbxAutenticar.Name = "gbxAutenticar";
            this.gbxAutenticar.Size = new System.Drawing.Size(197, 146);
            this.gbxAutenticar.TabIndex = 5;
            this.gbxAutenticar.TabStop = false;
            // 
            // chxCambioClave
            // 
            this.chxCambioClave.AutoSize = true;
            this.chxCambioClave.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.chxCambioClave.Location = new System.Drawing.Point(9, 113);
            this.chxCambioClave.Name = "chxCambioClave";
            this.chxCambioClave.Size = new System.Drawing.Size(141, 17);
            this.chxCambioClave.TabIndex = 9;
            this.chxCambioClave.Text = "Cambiar contraseña (F2)";
            this.chxCambioClave.UseVisualStyleBackColor = true;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(6, 71);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(64, 13);
            this.lblClave.TabIndex = 0;
            this.lblClave.Text = "Contraseña:";
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(6, 24);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(46, 13);
            this.lblCuenta.TabIndex = 5;
            this.lblCuenta.Text = "Usuario:";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(76, 64);
            this.txtClave.MaxLength = 8;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 2;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(76, 17);
            this.txtUserCode.MaxLength = 16;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(100, 20);
            this.txtUserCode.TabIndex = 1;
            this.txtUserCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserCode_KeyPress);
            // 
            // pbxLogin
            // 
            this.pbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
            this.pbxLogin.Location = new System.Drawing.Point(235, 110);
            this.pbxLogin.Name = "pbxLogin";
            this.pbxLogin.Size = new System.Drawing.Size(75, 59);
            this.pbxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogin.TabIndex = 8;
            this.pbxLogin.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(235, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(235, 27);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // erpUser
            // 
            this.erpUser.ContainerControl = this;
            // 
            // erpClave
            // 
            this.erpClave.ContainerControl = this;
            // 
            // Logueo
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(331, 198);
            this.Controls.Add(this.pbxLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbxAutenticar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Logueo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VENTAS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Logueo_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Logueo_KeyDown);
            this.gbxAutenticar.ResumeLayout(false);
            this.gbxAutenticar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxAutenticar;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.PictureBox pbxLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ErrorProvider erpUser;
        private System.Windows.Forms.ErrorProvider erpClave;
        private System.Windows.Forms.CheckBox chxCambioClave;
    }
}