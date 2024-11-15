namespace LAP.TUUA.VENTAS
{
    partial class Supervisor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Supervisor));
            this.lblClave = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gbxAutenticar = new System.Windows.Forms.GroupBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbxLogin = new System.Windows.Forms.PictureBox();
            this.erpUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxAutenticar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            this.SuspendLayout();
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
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(238, 27);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // gbxAutenticar
            // 
            this.gbxAutenticar.Controls.Add(this.lblClave);
            this.gbxAutenticar.Controls.Add(this.lblCuenta);
            this.gbxAutenticar.Controls.Add(this.txtClave);
            this.gbxAutenticar.Controls.Add(this.txtUserCode);
            this.gbxAutenticar.Location = new System.Drawing.Point(15, 23);
            this.gbxAutenticar.Name = "gbxAutenticar";
            this.gbxAutenticar.Size = new System.Drawing.Size(197, 146);
            this.gbxAutenticar.TabIndex = 10;
            this.gbxAutenticar.TabStop = false;
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
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(238, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbxLogin
            // 
            this.pbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
            this.pbxLogin.Location = new System.Drawing.Point(238, 110);
            this.pbxLogin.Name = "pbxLogin";
            this.pbxLogin.Size = new System.Drawing.Size(75, 59);
            this.pbxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogin.TabIndex = 12;
            this.pbxLogin.TabStop = false;
            // 
            // erpUser
            // 
            this.erpUser.ContainerControl = this;
            // 
            // erpClave
            // 
            this.erpClave.ContainerControl = this;
            // 
            // Supervisor
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(329, 193);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbxAutenticar);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbxLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "Supervisor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supervisor";
            this.gbxAutenticar.ResumeLayout(false);
            this.gbxAutenticar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gbxAutenticar;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbxLogin;
        private System.Windows.Forms.ErrorProvider erpUser;
        private System.Windows.Forms.ErrorProvider erpClave;
    }
}