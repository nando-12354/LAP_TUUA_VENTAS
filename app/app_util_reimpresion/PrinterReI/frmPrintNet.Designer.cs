namespace LAP.TUUA.PRINTER
{
    public partial class frmPrintNet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintNet));
            this.lblTituloConfig = new System.Windows.Forms.Label();
            this.lblStatusImpresoraSticker = new System.Windows.Forms.Label();
            this.lblPuertoImpresoraSticker = new System.Windows.Forms.Label();
            this.pnlSticker = new System.Windows.Forms.GroupBox();
            this.lblStatusImageSticker = new System.Windows.Forms.PictureBox();
            this.chkPuertoSticker = new System.Windows.Forms.CheckBox();
            this.cboPtosSticker = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPuertosDisponibles = new System.Windows.Forms.Label();
            this.pnlConfigManualPuertos = new System.Windows.Forms.Panel();
            this.pnlSticker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageSticker)).BeginInit();
            this.pnlConfigManualPuertos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloConfig
            // 
            this.lblTituloConfig.AutoSize = true;
            this.lblTituloConfig.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloConfig.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTituloConfig.Location = new System.Drawing.Point(103, 29);
            this.lblTituloConfig.Name = "lblTituloConfig";
            this.lblTituloConfig.Size = new System.Drawing.Size(251, 16);
            this.lblTituloConfig.TabIndex = 0;
            this.lblTituloConfig.Text = "Informacion de Estado de Impresoras";
            // 
            // lblStatusImpresoraSticker
            // 
            this.lblStatusImpresoraSticker.AutoSize = true;
            this.lblStatusImpresoraSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusImpresoraSticker.ForeColor = System.Drawing.Color.Black;
            this.lblStatusImpresoraSticker.Location = new System.Drawing.Point(15, 52);
            this.lblStatusImpresoraSticker.Name = "lblStatusImpresoraSticker";
            this.lblStatusImpresoraSticker.Size = new System.Drawing.Size(258, 13);
            this.lblStatusImpresoraSticker.TabIndex = 5;
            this.lblStatusImpresoraSticker.Text = "Esta Inoperativa. Falta Papel o  Poco Papel. Revisar.";
            // 
            // lblPuertoImpresoraSticker
            // 
            this.lblPuertoImpresoraSticker.AutoSize = true;
            this.lblPuertoImpresoraSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuertoImpresoraSticker.ForeColor = System.Drawing.Color.Black;
            this.lblPuertoImpresoraSticker.Location = new System.Drawing.Point(15, 80);
            this.lblPuertoImpresoraSticker.Name = "lblPuertoImpresoraSticker";
            this.lblPuertoImpresoraSticker.Size = new System.Drawing.Size(194, 13);
            this.lblPuertoImpresoraSticker.TabIndex = 6;
            this.lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: COM4.";
            // 
            // pnlSticker
            // 
            this.pnlSticker.Controls.Add(this.lblStatusImageSticker);
            this.pnlSticker.Controls.Add(this.chkPuertoSticker);
            this.pnlSticker.Controls.Add(this.cboPtosSticker);
            this.pnlSticker.Controls.Add(this.lblStatusImpresoraSticker);
            this.pnlSticker.Controls.Add(this.lblPuertoImpresoraSticker);
            this.pnlSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSticker.ForeColor = System.Drawing.Color.RoyalBlue;
            this.pnlSticker.Location = new System.Drawing.Point(15, 117);
            this.pnlSticker.Name = "pnlSticker";
            this.pnlSticker.Size = new System.Drawing.Size(420, 119);
            this.pnlSticker.TabIndex = 8;
            this.pnlSticker.TabStop = false;
            this.pnlSticker.Text = "IMPRESORA STICKER";
            // 
            // lblStatusImageSticker
            // 
            this.lblStatusImageSticker.Location = new System.Drawing.Point(185, 10);
            this.lblStatusImageSticker.Name = "lblStatusImageSticker";
            this.lblStatusImageSticker.Size = new System.Drawing.Size(40, 32);
            this.lblStatusImageSticker.TabIndex = 15;
            this.lblStatusImageSticker.TabStop = false;
            // 
            // chkPuertoSticker
            // 
            this.chkPuertoSticker.AutoSize = true;
            this.chkPuertoSticker.Location = new System.Drawing.Point(272, 81);
            this.chkPuertoSticker.Name = "chkPuertoSticker";
            this.chkPuertoSticker.Size = new System.Drawing.Size(15, 14);
            this.chkPuertoSticker.TabIndex = 8;
            this.chkPuertoSticker.UseVisualStyleBackColor = true;
            this.chkPuertoSticker.CheckedChanged += new System.EventHandler(this.chkPuertoSticker_CheckedChanged);
            // 
            // cboPtosSticker
            // 
            this.cboPtosSticker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPtosSticker.FormattingEnabled = true;
            this.cboPtosSticker.Location = new System.Drawing.Point(302, 78);
            this.cboPtosSticker.Name = "cboPtosSticker";
            this.cboPtosSticker.Size = new System.Drawing.Size(110, 21);
            this.cboPtosSticker.TabIndex = 7;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.Control;
            this.btnActualizar.Location = new System.Drawing.Point(49, 267);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(108, 23);
            this.btnActualizar.TabIndex = 10;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.Location = new System.Drawing.Point(191, 267);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(85, 23);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.SystemColors.Control;
            this.btnImprimir.Location = new System.Drawing.Point(304, 267);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(89, 23);
            this.btnImprimir.TabIndex = 12;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.Location = new System.Drawing.Point(393, 63);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(30, 32);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblPuertosDisponibles
            // 
            this.lblPuertosDisponibles.Location = new System.Drawing.Point(323, 58);
            this.lblPuertosDisponibles.Name = "lblPuertosDisponibles";
            this.lblPuertosDisponibles.Size = new System.Drawing.Size(68, 42);
            this.lblPuertosDisponibles.TabIndex = 14;
            this.lblPuertosDisponibles.Text = "Puertos\r\nDisponibles";
            this.lblPuertosDisponibles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlConfigManualPuertos
            // 
            this.pnlConfigManualPuertos.BackColor = System.Drawing.Color.White;
            this.pnlConfigManualPuertos.Controls.Add(this.lblTituloConfig);
            this.pnlConfigManualPuertos.Controls.Add(this.lblPuertosDisponibles);
            this.pnlConfigManualPuertos.Controls.Add(this.pnlSticker);
            this.pnlConfigManualPuertos.Controls.Add(this.btnRefresh);
            this.pnlConfigManualPuertos.Controls.Add(this.btnImprimir);
            this.pnlConfigManualPuertos.Controls.Add(this.btnActualizar);
            this.pnlConfigManualPuertos.Controls.Add(this.btnSalir);
            this.pnlConfigManualPuertos.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigManualPuertos.Name = "pnlConfigManualPuertos";
            this.pnlConfigManualPuertos.Size = new System.Drawing.Size(450, 450);
            this.pnlConfigManualPuertos.TabIndex = 15;
            // 
            // frmPrintNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 428);
            this.ControlBox = false;
            this.Controls.Add(this.pnlConfigManualPuertos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPrintNet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Impresion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrint_FormClosing);
            this.pnlSticker.ResumeLayout(false);
            this.pnlSticker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageSticker)).EndInit();
            this.pnlConfigManualPuertos.ResumeLayout(false);
            this.pnlConfigManualPuertos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloConfig;
        private System.Windows.Forms.Label lblStatusImpresoraSticker;
        private System.Windows.Forms.Label lblPuertoImpresoraSticker;
        private System.Windows.Forms.GroupBox pnlSticker;
        private System.Windows.Forms.CheckBox chkPuertoSticker;
        private System.Windows.Forms.ComboBox cboPtosSticker;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPuertosDisponibles;
        private System.Windows.Forms.PictureBox lblStatusImageSticker;
        private System.Windows.Forms.Panel pnlConfigManualPuertos;
    }
}

