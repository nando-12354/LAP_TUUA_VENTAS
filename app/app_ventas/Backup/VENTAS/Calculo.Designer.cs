namespace LAP.TUUA.VENTAS
{
    partial class Calculo
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
            this.lblTasaCambio = new System.Windows.Forms.Label();
            this.lblTC = new System.Windows.Forms.Label();
            this.txtCambiar = new System.Windows.Forms.TextBox();
            this.txtCambiado = new System.Windows.Forms.TextBox();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblTCVM = new System.Windows.Forms.Label();
            this.lblACambiar = new System.Windows.Forms.Label();
            this.lblCambiado = new System.Windows.Forms.Label();
            this.lblMoneda = new System.Windows.Forms.Label();
            this.lblVerMoneda = new System.Windows.Forms.Label();
            this.erpMonto = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.erpMonto)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTasaCambio
            // 
            this.lblTasaCambio.AutoSize = true;
            this.lblTasaCambio.Location = new System.Drawing.Point(24, 61);
            this.lblTasaCambio.Name = "lblTasaCambio";
            this.lblTasaCambio.Size = new System.Drawing.Size(87, 13);
            this.lblTasaCambio.TabIndex = 0;
            this.lblTasaCambio.Text = "Tasa de Cambio:";
            // 
            // lblTC
            // 
            this.lblTC.AutoSize = true;
            this.lblTC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTC.ForeColor = System.Drawing.Color.Red;
            this.lblTC.Location = new System.Drawing.Point(137, 62);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(32, 13);
            this.lblTC.TabIndex = 1;
            this.lblTC.Text = "3.25";
            // 
            // txtCambiar
            // 
            this.txtCambiar.AcceptsReturn = true;
            this.txtCambiar.Location = new System.Drawing.Point(26, 113);
            this.txtCambiar.MaxLength = 9;
            this.txtCambiar.Name = "txtCambiar";
            this.txtCambiar.Size = new System.Drawing.Size(100, 20);
            this.txtCambiar.TabIndex = 2;
            this.txtCambiar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCambiar_KeyDown);
            // 
            // txtCambiado
            // 
            this.txtCambiado.Location = new System.Drawing.Point(203, 113);
            this.txtCambiado.Name = "txtCambiado";
            this.txtCambiado.ReadOnly = true;
            this.txtCambiado.Size = new System.Drawing.Size(100, 20);
            this.txtCambiado.TabIndex = 3;
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(53, 148);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(75, 23);
            this.btnCalcular.TabIndex = 4;
            this.btnCalcular.Text = "Calcular";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(205, 148);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cerrar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(25, 14);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(31, 13);
            this.lblTipo.TabIndex = 6;
            this.lblTipo.Text = "Tipo:";
            // 
            // lblTCVM
            // 
            this.lblTCVM.AutoSize = true;
            this.lblTCVM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTCVM.ForeColor = System.Drawing.Color.Blue;
            this.lblTCVM.Location = new System.Drawing.Point(137, 14);
            this.lblTCVM.Name = "lblTCVM";
            this.lblTCVM.Size = new System.Drawing.Size(98, 13);
            this.lblTCVM.TabIndex = 7;
            this.lblTCVM.Text = "Compra Moneda";
            // 
            // lblACambiar
            // 
            this.lblACambiar.AutoSize = true;
            this.lblACambiar.Location = new System.Drawing.Point(23, 88);
            this.lblACambiar.Name = "lblACambiar";
            this.lblACambiar.Size = new System.Drawing.Size(86, 13);
            this.lblACambiar.TabIndex = 8;
            this.lblACambiar.Text = "Monto a cambiar";
            // 
            // lblCambiado
            // 
            this.lblCambiado.AutoSize = true;
            this.lblCambiado.Location = new System.Drawing.Point(203, 88);
            this.lblCambiado.Name = "lblCambiado";
            this.lblCambiado.Size = new System.Drawing.Size(87, 13);
            this.lblCambiado.TabIndex = 9;
            this.lblCambiado.Text = "Monto Cambiado";
            // 
            // lblMoneda
            // 
            this.lblMoneda.AutoSize = true;
            this.lblMoneda.Location = new System.Drawing.Point(25, 37);
            this.lblMoneda.Name = "lblMoneda";
            this.lblMoneda.Size = new System.Drawing.Size(49, 13);
            this.lblMoneda.TabIndex = 10;
            this.lblMoneda.Text = "Moneda:";
            // 
            // lblVerMoneda
            // 
            this.lblVerMoneda.AutoSize = true;
            this.lblVerMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerMoneda.ForeColor = System.Drawing.Color.Blue;
            this.lblVerMoneda.Location = new System.Drawing.Point(137, 37);
            this.lblVerMoneda.Name = "lblVerMoneda";
            this.lblVerMoneda.Size = new System.Drawing.Size(65, 13);
            this.lblVerMoneda.TabIndex = 11;
            this.lblVerMoneda.Text = "DOLARES";
            // 
            // erpMonto
            // 
            this.erpMonto.ContainerControl = this;
            // 
            // Calculo
            // 
            this.AcceptButton = this.btnCalcular;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 193);
            this.Controls.Add(this.lblVerMoneda);
            this.Controls.Add(this.lblMoneda);
            this.Controls.Add(this.lblCambiado);
            this.Controls.Add(this.lblACambiar);
            this.Controls.Add(this.lblTCVM);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.txtCambiado);
            this.Controls.Add(this.txtCambiar);
            this.Controls.Add(this.lblTC);
            this.Controls.Add(this.lblTasaCambio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.Name = "Calculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculo";
            this.Load += new System.EventHandler(this.Calculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erpMonto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTasaCambio;
        private System.Windows.Forms.Label lblTC;
        private System.Windows.Forms.TextBox txtCambiar;
        private System.Windows.Forms.TextBox txtCambiado;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblTCVM;
        private System.Windows.Forms.Label lblACambiar;
        private System.Windows.Forms.Label lblCambiado;
        private System.Windows.Forms.Label lblMoneda;
        private System.Windows.Forms.Label lblVerMoneda;
        private System.Windows.Forms.ErrorProvider erpMonto;
    }
}