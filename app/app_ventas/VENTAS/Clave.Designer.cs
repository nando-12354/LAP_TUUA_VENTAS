namespace LAP.TUUA.VENTAS
{
    partial class Clave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clave));
            this.gbxClave = new System.Windows.Forms.GroupBox();
            this.lblNuevaClave = new System.Windows.Forms.Label();
            this.lblClave = new System.Windows.Forms.Label();
            this.txtNuevaClave = new System.Windows.Forms.TextBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.pbxLogin = new System.Windows.Forms.PictureBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpNClave = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxClave.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNClave)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxClave
            // 
            this.gbxClave.Controls.Add(this.lblNuevaClave);
            this.gbxClave.Controls.Add(this.lblClave);
            this.gbxClave.Controls.Add(this.txtNuevaClave);
            this.gbxClave.Controls.Add(this.txtClave);
            this.gbxClave.Location = new System.Drawing.Point(22, 28);
            this.gbxClave.Name = "gbxClave";
            this.gbxClave.Size = new System.Drawing.Size(200, 140);
            this.gbxClave.TabIndex = 0;
            this.gbxClave.TabStop = false;
            // 
            // lblNuevaClave
            // 
            this.lblNuevaClave.AutoSize = true;
            this.lblNuevaClave.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblNuevaClave.Location = new System.Drawing.Point(28, 73);
            this.lblNuevaClave.Name = "lblNuevaClave";
            this.lblNuevaClave.Size = new System.Drawing.Size(108, 13);
            this.lblNuevaClave.TabIndex = 3;
            this.lblNuevaClave.Text = "Confirmar Contraseña";
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblClave.Location = new System.Drawing.Point(29, 17);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(96, 13);
            this.lblClave.TabIndex = 2;
            this.lblClave.Text = "Nueva Contraseña";
            // 
            // txtNuevaClave
            // 
            this.txtNuevaClave.Location = new System.Drawing.Point(31, 89);
            this.txtNuevaClave.MaxLength = 100;
            this.txtNuevaClave.Name = "txtNuevaClave";
            this.txtNuevaClave.PasswordChar = '*';
            this.txtNuevaClave.Size = new System.Drawing.Size(124, 20);
            this.txtNuevaClave.TabIndex = 1;
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(32, 33);
            this.txtClave.MaxLength = 100;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(124, 20);
            this.txtClave.TabIndex = 0;
            // 
            // pbxLogin
            // 
            this.pbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
            this.pbxLogin.Location = new System.Drawing.Point(244, 117);
            this.pbxLogin.Name = "pbxLogin";
            this.pbxLogin.Size = new System.Drawing.Size(73, 51);
            this.pbxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogin.TabIndex = 9;
            this.pbxLogin.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAceptar.Location = new System.Drawing.Point(244, 33);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // erpClave
            // 
            this.erpClave.ContainerControl = this;
            // 
            // erpNClave
            // 
            this.erpNClave.ContainerControl = this;
            // 
            // Clave
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 196);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.pbxLogin);
            this.Controls.Add(this.gbxClave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Clave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambio de Contraseña";
            this.gbxClave.ResumeLayout(false);
            this.gbxClave.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpNClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxClave;
        private System.Windows.Forms.TextBox txtNuevaClave;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Label lblNuevaClave;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.PictureBox pbxLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ErrorProvider erpClave;
        private System.Windows.Forms.ErrorProvider erpNClave;
    }
}