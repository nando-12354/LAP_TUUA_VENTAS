namespace LAP.TUUA.VENTAS
{
    partial class CompraVenta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompraVenta));
            this.gbxcvm = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblEntreInter = new System.Windows.Forms.Label();
            this.lblEntreNac = new System.Windows.Forms.Label();
            this.lblTC = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblACambiar = new System.Windows.Forms.Label();
            this.lblRecibidoI = new System.Windows.Forms.Label();
            this.lblRecibidoN = new System.Windows.Forms.Label();
            this.lblEntregarI = new System.Windows.Forms.Label();
            this.lblEntregarN = new System.Windows.Forms.Label();
            this.txtInterCambio = new System.Windows.Forms.TextBox();
            this.txtInter = new System.Windows.Forms.TextBox();
            this.txtNacional = new System.Windows.Forms.TextBox();
            this.rbCompra = new System.Windows.Forms.RadioButton();
            this.rbVenta = new System.Windows.Forms.RadioButton();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.cbxMoneda = new System.Windows.Forms.ComboBox();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.erpMontoN = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpMontoI = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxcvm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontoN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontoI)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxcvm
            // 
            this.gbxcvm.Controls.Add(this.btnLimpiar);
            this.gbxcvm.Controls.Add(this.lblEntreInter);
            this.gbxcvm.Controls.Add(this.lblEntreNac);
            this.gbxcvm.Controls.Add(this.lblTC);
            this.gbxcvm.Controls.Add(this.btnCancelar);
            this.gbxcvm.Controls.Add(this.btnCalcular);
            this.gbxcvm.Controls.Add(this.btnAceptar);
            this.gbxcvm.Controls.Add(this.label1);
            this.gbxcvm.Controls.Add(this.lblACambiar);
            this.gbxcvm.Controls.Add(this.lblRecibidoI);
            this.gbxcvm.Controls.Add(this.lblRecibidoN);
            this.gbxcvm.Controls.Add(this.lblEntregarI);
            this.gbxcvm.Controls.Add(this.lblEntregarN);
            this.gbxcvm.Controls.Add(this.txtInterCambio);
            this.gbxcvm.Controls.Add(this.txtInter);
            this.gbxcvm.Controls.Add(this.txtNacional);
            this.gbxcvm.Controls.Add(this.rbCompra);
            this.gbxcvm.Controls.Add(this.rbVenta);
            this.gbxcvm.Controls.Add(this.lblMoneda);
            this.gbxcvm.Controls.Add(this.cbxMoneda);
            this.gbxcvm.Location = new System.Drawing.Point(32, 28);
            this.gbxcvm.Name = "gbxcvm";
            this.gbxcvm.Size = new System.Drawing.Size(539, 330);
            this.gbxcvm.TabIndex = 0;
            this.gbxcvm.TabStop = false;
            this.gbxcvm.Text = "Compra y Venta";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpiar.Image")));
            this.btnLimpiar.Location = new System.Drawing.Point(296, 115);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 25);
            this.btnLimpiar.TabIndex = 20;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // lblEntreInter
            // 
            this.lblEntreInter.AutoSize = true;
            this.lblEntreInter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntreInter.ForeColor = System.Drawing.Color.Blue;
            this.lblEntreInter.Location = new System.Drawing.Point(277, 286);
            this.lblEntreInter.Name = "lblEntreInter";
            this.lblEntreInter.Size = new System.Drawing.Size(32, 13);
            this.lblEntreInter.TabIndex = 19;
            this.lblEntreInter.Text = "0.00";
            // 
            // lblEntreNac
            // 
            this.lblEntreNac.AutoSize = true;
            this.lblEntreNac.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntreNac.ForeColor = System.Drawing.Color.Blue;
            this.lblEntreNac.Location = new System.Drawing.Point(24, 286);
            this.lblEntreNac.Name = "lblEntreNac";
            this.lblEntreNac.Size = new System.Drawing.Size(50, 13);
            this.lblEntreNac.TabIndex = 18;
            this.lblEntreNac.Text = "S/ 0.00";
            // 
            // lblTC
            // 
            this.lblTC.AutoSize = true;
            this.lblTC.ForeColor = System.Drawing.Color.Red;
            this.lblTC.Location = new System.Drawing.Point(277, 237);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(0, 13);
            this.lblTC.TabIndex = 17;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(296, 84);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 10;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCalcular
            // 
            this.btnCalcular.Image = ((System.Drawing.Image)(resources.GetObject("btnCalcular.Image")));
            this.btnCalcular.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCalcular.Location = new System.Drawing.Point(296, 53);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 25);
            this.btnCalcular.TabIndex = 9;
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(296, 22);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 214);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tasa de Cambio:";
            // 
            // lblACambiar
            // 
            this.lblACambiar.AutoSize = true;
            this.lblACambiar.Location = new System.Drawing.Point(21, 214);
            this.lblACambiar.Name = "lblACambiar";
            this.lblACambiar.Size = new System.Drawing.Size(87, 13);
            this.lblACambiar.TabIndex = 12;
            this.lblACambiar.Text = "Monto a Cambiar";
            // 
            // lblRecibidoI
            // 
            this.lblRecibidoI.AutoSize = true;
            this.lblRecibidoI.Location = new System.Drawing.Point(268, 158);
            this.lblRecibidoI.Name = "lblRecibidoI";
            this.lblRecibidoI.Size = new System.Drawing.Size(82, 13);
            this.lblRecibidoI.TabIndex = 11;
            this.lblRecibidoI.Text = "Monto Recibido";
            // 
            // lblRecibidoN
            // 
            this.lblRecibidoN.AutoSize = true;
            this.lblRecibidoN.Location = new System.Drawing.Point(21, 158);
            this.lblRecibidoN.Name = "lblRecibidoN";
            this.lblRecibidoN.Size = new System.Drawing.Size(109, 13);
            this.lblRecibidoN.TabIndex = 10;
            this.lblRecibidoN.Text = "Monto Recibido(SOL)";
            // 
            // lblEntregarI
            // 
            this.lblEntregarI.AutoSize = true;
            this.lblEntregarI.Location = new System.Drawing.Point(268, 269);
            this.lblEntregarI.Name = "lblEntregarI";
            this.lblEntregarI.Size = new System.Drawing.Size(88, 13);
            this.lblEntregarI.TabIndex = 9;
            this.lblEntregarI.Text = "Monto a entregar";
            // 
            // lblEntregarN
            // 
            this.lblEntregarN.AutoSize = true;
            this.lblEntregarN.Location = new System.Drawing.Point(21, 269);
            this.lblEntregarN.Name = "lblEntregarN";
            this.lblEntregarN.Size = new System.Drawing.Size(118, 13);
            this.lblEntregarN.TabIndex = 8;
            this.lblEntregarN.Text = "Monto a entregar (SOL)";
            // 
            // txtInterCambio
            // 
            this.txtInterCambio.Location = new System.Drawing.Point(24, 230);
            this.txtInterCambio.MaxLength = 15;
            this.txtInterCambio.Name = "txtInterCambio";
            this.txtInterCambio.Size = new System.Drawing.Size(100, 20);
            this.txtInterCambio.TabIndex = 7;
            this.txtInterCambio.TextChanged += new System.EventHandler(this.txtInterCambio_TextChanged);
            this.txtInterCambio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMontoCambio_KeyDown);
            // 
            // txtInter
            // 
            this.txtInter.Location = new System.Drawing.Point(271, 183);
            this.txtInter.MaxLength = 15;
            this.txtInter.Name = "txtInter";
            this.txtInter.Size = new System.Drawing.Size(100, 20);
            this.txtInter.TabIndex = 6;
            this.txtInter.TextChanged += new System.EventHandler(this.txtInter_TextChanged);
            this.txtInter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInter_KeyDown);
            // 
            // txtNacional
            // 
            this.txtNacional.Location = new System.Drawing.Point(24, 183);
            this.txtNacional.MaxLength = 15;
            this.txtNacional.Name = "txtNacional";
            this.txtNacional.Size = new System.Drawing.Size(100, 20);
            this.txtNacional.TabIndex = 5;
            this.txtNacional.TextChanged += new System.EventHandler(this.txtNacional_TextChanged);
            this.txtNacional.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNacional_KeyDown);
            // 
            // rbCompra
            // 
            this.rbCompra.AutoSize = true;
            this.rbCompra.Location = new System.Drawing.Point(24, 118);
            this.rbCompra.Name = "rbCompra";
            this.rbCompra.Size = new System.Drawing.Size(61, 17);
            this.rbCompra.TabIndex = 3;
            this.rbCompra.Text = "Compra";
            this.rbCompra.UseVisualStyleBackColor = true;
            this.rbCompra.CheckedChanged += new System.EventHandler(this.rbCompra_CheckedChanged);
            // 
            // rbVenta
            // 
            this.rbVenta.AutoSize = true;
            this.rbVenta.Checked = true;
            this.rbVenta.Location = new System.Drawing.Point(24, 80);
            this.rbVenta.Name = "rbVenta";
            this.rbVenta.Size = new System.Drawing.Size(53, 17);
            this.rbVenta.TabIndex = 2;
            this.rbVenta.TabStop = true;
            this.rbVenta.Text = "Venta";
            this.rbVenta.UseVisualStyleBackColor = true;
            this.rbVenta.CheckedChanged += new System.EventHandler(this.rbVenta_CheckedChanged);
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(21, 26);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(46, 13);
            this.lblMoneda.TabIndex = 1;
            this.lblMoneda.Text = "Moneda";
            // 
            // cbxMoneda
            // 
            this.cbxMoneda.DataSource = this.monedaBindingSource;
            this.cbxMoneda.DisplayMember = "SDscMoneda";
            this.cbxMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMoneda.FormattingEnabled = true;
            this.cbxMoneda.Location = new System.Drawing.Point(24, 42);
            this.cbxMoneda.Name = "cbxMoneda";
            this.cbxMoneda.Size = new System.Drawing.Size(128, 21);
            this.cbxMoneda.TabIndex = 1;
            this.cbxMoneda.ValueMember = "SCodMoneda";
            this.cbxMoneda.SelectedIndexChanged += new System.EventHandler(this.cbxMoneda_SelectedIndexChanged);
            // 
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(LAP.TUUA.ENTIDADES.Moneda);
            // 
            // erpMontoN
            // 
            this.erpMontoN.ContainerControl = this;
            // 
            // erpMontoI
            // 
            this.erpMontoI.ContainerControl = this;
            // 
            // CompraVenta
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.gbxcvm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CompraVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CompraVenta";
            this.Load += new System.EventHandler(this.CompraVenta_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CompraVenta_KeyDown);
            this.gbxcvm.ResumeLayout(false);
            this.gbxcvm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontoN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontoI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxcvm;
        private System.Windows.Forms.RadioButton rbCompra;
        private System.Windows.Forms.RadioButton rbVenta;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.ComboBox cbxMoneda;
        private System.Windows.Forms.TextBox txtInterCambio;
        private System.Windows.Forms.TextBox txtInter;
        private System.Windows.Forms.TextBox txtNacional;
        private System.Windows.Forms.Label lblEntregarN;
        private System.Windows.Forms.Label lblEntregarI;
        private System.Windows.Forms.Label lblACambiar;
        private System.Windows.Forms.Label lblRecibidoI;
        private System.Windows.Forms.Label lblRecibidoN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblEntreInter;
        private System.Windows.Forms.Label lblEntreNac;
        private System.Windows.Forms.Label lblTC;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.ErrorProvider erpMontoN;
        private System.Windows.Forms.ErrorProvider erpMontoI;
        private System.Windows.Forms.Button btnLimpiar;
    }
}