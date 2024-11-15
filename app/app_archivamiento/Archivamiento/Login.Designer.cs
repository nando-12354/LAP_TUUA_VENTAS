namespace LAP.TUUA.ARCHIVAMIENTO
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pbxLogin = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxAutenticar = new System.Windows.Forms.GroupBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.errPUsuario = new System.Windows.Forms.ErrorProvider(this.components);
            this.errPClave = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
            this.gbxAutenticar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errPUsuario)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPClave)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxLogin
            // 
            this.pbxLogin.BackColor = System.Drawing.Color.Transparent;
            this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
            this.pbxLogin.Location = new System.Drawing.Point(227, 96);
            this.pbxLogin.Name = "pbxLogin";
            this.pbxLogin.Size = new System.Drawing.Size(76, 63);
            this.pbxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogin.TabIndex = 12;
            this.pbxLogin.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(228, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbxAutenticar
            // 
            this.gbxAutenticar.BackColor = System.Drawing.Color.Transparent;
            this.gbxAutenticar.Controls.Add(this.lblClave);
            this.gbxAutenticar.Controls.Add(this.lblCuenta);
            this.gbxAutenticar.Controls.Add(this.txtClave);
            this.gbxAutenticar.Controls.Add(this.txtUserCode);
            this.gbxAutenticar.Location = new System.Drawing.Point(12, 22);
            this.gbxAutenticar.Name = "gbxAutenticar";
            this.gbxAutenticar.Size = new System.Drawing.Size(197, 125);
            this.gbxAutenticar.TabIndex = 10;
            this.gbxAutenticar.TabStop = false;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(6, 74);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(64, 13);
            this.lblClave.TabIndex = 0;
            this.lblClave.Text = "Contraseña:";
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(6, 27);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(46, 13);
            this.lblCuenta.TabIndex = 5;
            this.lblCuenta.Text = "Usuario:";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(76, 71);
            this.txtClave.MaxLength = 8;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 2;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(76, 24);
            this.txtUserCode.MaxLength = 16;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(100, 20);
            this.txtUserCode.TabIndex = 1;
            this.txtUserCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserCode_KeyPress);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(228, 12);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 3;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // errPUsuario
            // 
            this.errPUsuario.ContainerControl = this;
            // 
            // errPClave
            // 
            this.errPClave.ContainerControl = this;
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(319, 173);
            this.ControlBox = false;
            this.Controls.Add(this.pbxLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.gbxAutenticar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARCHIVAMIENTO";
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
            this.gbxAutenticar.ResumeLayout(false);
            this.gbxAutenticar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errPUsuario)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errPClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbxAutenticar;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ErrorProvider errPUsuario;
        private System.Windows.Forms.ErrorProvider errPClave;
    }
}

