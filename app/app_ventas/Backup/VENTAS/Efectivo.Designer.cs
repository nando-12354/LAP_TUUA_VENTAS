namespace LAP.TUUA.VENTAS
{
    partial class Efectivo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Efectivo));
            this.lblMoneda = new System.Windows.Forms.Label();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.lblTotAPagar = new System.Windows.Forms.Label();
            this.lblSimbAPagar = new System.Windows.Forms.Label();
            this.lblImpApagar = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.erpMonto = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(13, 36);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(73, 13);
            this.lblMoneda.TabIndex = 20;
            this.lblMoneda.Text = "Tipo Moneda:";
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.DisplayMember = "SDscMoneda";
            this.cbxMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(14, 64);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(101, 21);
            this.cbxMoneda.TabIndex = 22;
            this.cbxMoneda.ValueMember = "SCodMoneda";
            this.cbxMoneda.SelectedIndexChanged += new System.EventHandler(this.cbxMoneda_SelectedIndexChanged);
            // 
            // txtMonto
            // 
            this.txtMonto.Location = new System.Drawing.Point(164, 64);
            this.txtMonto.MaxLength = 8;
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.Size = new System.Drawing.Size(80, 20);
            this.txtMonto.TabIndex = 23;
            this.txtMonto.TextChanged += new System.EventHandler(this.txtMonto_TextChanged);
            this.txtMonto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMonto_KeyDown);
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(165, 36);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(40, 13);
            this.lblMonto.TabIndex = 21;
            this.lblMonto.Text = "Monto:";
            // 
            // lblTotAPagar
            // 
            this.lblTotAPagar.AutoSize = true;
            this.lblTotAPagar.Location = new System.Drawing.Point(34, 16);
            this.lblTotAPagar.Name = "lblTotAPagar";
            this.lblTotAPagar.Size = new System.Drawing.Size(74, 13);
            this.lblTotAPagar.TabIndex = 24;
            this.lblTotAPagar.Text = "Total a Pagar:";
            // 
            // lblSimbAPagar
            // 
            this.lblSimbAPagar.AutoSize = true;
            this.lblSimbAPagar.Location = new System.Drawing.Point(131, 16);
            this.lblSimbAPagar.Name = "lblSimbAPagar";
            this.lblSimbAPagar.Size = new System.Drawing.Size(30, 13);
            this.lblSimbAPagar.TabIndex = 25;
            this.lblSimbAPagar.Text = "USD";
            // 
            // lblImpApagar
            // 
            this.lblImpApagar.AutoSize = true;
            this.lblImpApagar.Location = new System.Drawing.Point(187, 16);
            this.lblImpApagar.Name = "lblImpApagar";
            this.lblImpApagar.Size = new System.Drawing.Size(28, 13);
            this.lblImpApagar.TabIndex = 26;
            this.lblImpApagar.Text = "0.00";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMoneda);
            this.groupBox1.Controls.Add(this.lblMonto);
            this.groupBox1.Controls.Add(this.txtMonto);
            this.groupBox1.Controls.Add(this.cbxMoneda);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(22, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 105);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Importe A pagar";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.lblImpApagar);
            this.groupBox2.Controls.Add(this.lblTotAPagar);
            this.groupBox2.Controls.Add(this.lblSimbAPagar);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBox2.Location = new System.Drawing.Point(21, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 164);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(247, 189);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 25);
            this.btnCancelar.TabIndex = 30;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(21, 189);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(96, 25);
            this.btnAceptar.TabIndex = 29;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // erpMonto
            // 
            this.erpMonto.ContainerControl = this;
            // 
            // Efectivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 225);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Efectivo";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Efectivo";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpMonto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblTotAPagar;
        private System.Windows.Forms.Label lblSimbAPagar;
        private System.Windows.Forms.Label lblImpApagar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ErrorProvider erpMonto;

    }
}
