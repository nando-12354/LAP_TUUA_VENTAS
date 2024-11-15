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
            this.lblStatusImpresoraVoucher = new System.Windows.Forms.Label();
            this.lblPuertoImpresoraVoucher = new System.Windows.Forms.Label();
            //this.lblPuertoUsbImpresoraVoucher = new System.Windows.Forms.Label(); //FL.
            //this.lblPuertoUsbImpresoraSticker = new System.Windows.Forms.Label(); //FL.
            this.lblStatusImpresoraSticker = new System.Windows.Forms.Label();
            this.lblPuertoImpresoraSticker = new System.Windows.Forms.Label();
            this.pnlSticker = new System.Windows.Forms.GroupBox();
            this.lblStatusImageSticker = new System.Windows.Forms.PictureBox();
            this.chkPuertoSticker = new System.Windows.Forms.CheckBox();
            //this.chkPuertoUsbSticker = new System.Windows.Forms.CheckBox(); //FL.
            this.cboPtosSticker = new System.Windows.Forms.ComboBox();
            //this.cboPtosUsbSticker = new System.Windows.Forms.ComboBox(); //FL.
            this.pnlVoucher = new System.Windows.Forms.GroupBox();
            this.lblStatusImageVoucher = new System.Windows.Forms.PictureBox();
            this.chkPuertoVoucher = new System.Windows.Forms.CheckBox();
            //this.chkPuertoUsbVoucher = new System.Windows.Forms.CheckBox(); //FL.
            this.cboPtosVoucher = new System.Windows.Forms.ComboBox();
            //this.cboPtosUsbVoucher = new System.Windows.Forms.ComboBox(); //FL.
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPuertosDisponibles = new System.Windows.Forms.Label();
            this.pnlConfigManualPuertos = new System.Windows.Forms.Panel();
            this.pnlSticker.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageSticker)).BeginInit();
            this.pnlVoucher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageVoucher)).BeginInit();
            this.pnlConfigManualPuertos.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTituloConfig
            // 
            this.lblTituloConfig.AutoSize = true;
            this.lblTituloConfig.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloConfig.ForeColor = System.Drawing.Color.RoyalBlue;
            //this.lblTituloConfig.Location = new System.Drawing.Point(137, 20); //FL.
            this.lblTituloConfig.Location = new System.Drawing.Point(137, 36); 
            this.lblTituloConfig.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTituloConfig.Name = "lblTituloConfig";
            this.lblTituloConfig.Size = new System.Drawing.Size(334, 21);
            this.lblTituloConfig.TabIndex = 0;
            this.lblTituloConfig.Text = "Informacion de Estado de Impresoras";
            // 
            // lblStatusImpresoraVoucher
            // 
            this.lblStatusImpresoraVoucher.AutoSize = true;
            this.lblStatusImpresoraVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusImpresoraVoucher.ForeColor = System.Drawing.Color.Black;
            this.lblStatusImpresoraVoucher.Location = new System.Drawing.Point(20, 64);
            this.lblStatusImpresoraVoucher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusImpresoraVoucher.Name = "lblStatusImpresoraVoucher";
            this.lblStatusImpresoraVoucher.Size = new System.Drawing.Size(171, 17);
            this.lblStatusImpresoraVoucher.TabIndex = 3;
            this.lblStatusImpresoraVoucher.Text = "Error en puerto asignado.";
            // 
            // lblPuertoImpresoraVoucher
            // 
            this.lblPuertoImpresoraVoucher.AutoSize = true;
            this.lblPuertoImpresoraVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuertoImpresoraVoucher.ForeColor = System.Drawing.Color.Black;
            this.lblPuertoImpresoraVoucher.Location = new System.Drawing.Point(20, 98);
            this.lblPuertoImpresoraVoucher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPuertoImpresoraVoucher.Name = "lblPuertoImpresoraVoucher";
            this.lblPuertoImpresoraVoucher.Size = new System.Drawing.Size(269, 17);
            this.lblPuertoImpresoraVoucher.TabIndex = 4;
            this.lblPuertoImpresoraVoucher.Text = "Puerto asignado incorrectamente: COM4.";
            // 
            // lblPuertoUsbImpresoraVoucher
            // 
            //this.lblPuertoUsbImpresoraVoucher.AutoSize = true;
            //this.lblPuertoUsbImpresoraVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.lblPuertoUsbImpresoraVoucher.ForeColor = System.Drawing.Color.Black;
            //this.lblPuertoUsbImpresoraVoucher.Location = new System.Drawing.Point(20, 136);
            //this.lblPuertoUsbImpresoraVoucher.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            //this.lblPuertoUsbImpresoraVoucher.Name = "lblPuertoUsbImpresoraVoucher";
            //this.lblPuertoUsbImpresoraVoucher.Size = new System.Drawing.Size(269, 17);
            //this.lblPuertoUsbImpresoraVoucher.TabIndex = 8;
            //this.lblPuertoUsbImpresoraVoucher.Text = "Puerto asignado incorrectamente: USB.";
            // 
            // lblStatusImpresoraSticker
            // 
            this.lblStatusImpresoraSticker.AutoSize = true;
            this.lblStatusImpresoraSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusImpresoraSticker.ForeColor = System.Drawing.Color.Black;
            this.lblStatusImpresoraSticker.Location = new System.Drawing.Point(20, 64);
            this.lblStatusImpresoraSticker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatusImpresoraSticker.Name = "lblStatusImpresoraSticker";
            this.lblStatusImpresoraSticker.Size = new System.Drawing.Size(341, 17);
            this.lblStatusImpresoraSticker.TabIndex = 5;
            this.lblStatusImpresoraSticker.Text = "Esta Inoperativa. Falta Papel o  Poco Papel. Revisar.";
            // 
            // lblPuertoImpresoraSticker
            // 
            this.lblPuertoImpresoraSticker.AutoSize = true;
            this.lblPuertoImpresoraSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuertoImpresoraSticker.ForeColor = System.Drawing.Color.Black;
            this.lblPuertoImpresoraSticker.Location = new System.Drawing.Point(20, 98);
            this.lblPuertoImpresoraSticker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPuertoImpresoraSticker.Name = "lblPuertoImpresoraSticker";
            this.lblPuertoImpresoraSticker.Size = new System.Drawing.Size(258, 17);
            this.lblPuertoImpresoraSticker.TabIndex = 6;
            this.lblPuertoImpresoraSticker.Text = "Puerto asignado correctamente: COM4.";
            // 
            // lblPuertoUsbImpresoraSticker
            // 
            //this.lblPuertoUsbImpresoraSticker.AutoSize = true;
            //this.lblPuertoUsbImpresoraSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.lblPuertoUsbImpresoraSticker.ForeColor = System.Drawing.Color.Black;
            //this.lblPuertoUsbImpresoraSticker.Location = new System.Drawing.Point(20, 136);
            //this.lblPuertoUsbImpresoraSticker.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            //this.lblPuertoUsbImpresoraSticker.Name = "lblPuertoUsbImpresoraSticker";
            //this.lblPuertoUsbImpresoraSticker.Size = new System.Drawing.Size(258, 17);
            //this.lblPuertoUsbImpresoraSticker.TabIndex = 12;
            //this.lblPuertoUsbImpresoraSticker.Text = "Puerto asignado correctamente: USB.";
            // 
            // pnlSticker
            //
            //this.pnlSticker.Controls.Add(this.lblPuertoUsbImpresoraSticker); //FL.
            this.pnlSticker.Controls.Add(this.lblStatusImageSticker);
            this.pnlSticker.Controls.Add(this.chkPuertoSticker);
            //this.pnlSticker.Controls.Add(this.chkPuertoUsbSticker); //FL.
            this.pnlSticker.Controls.Add(this.cboPtosSticker);
            //this.pnlSticker.Controls.Add(this.cboPtosUsbSticker); //FL.
            this.pnlSticker.Controls.Add(this.lblStatusImpresoraSticker);
            this.pnlSticker.Controls.Add(this.lblPuertoImpresoraSticker);
            this.pnlSticker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSticker.ForeColor = System.Drawing.Color.RoyalBlue;
            this.pnlSticker.Location = new System.Drawing.Point(20, 288);
            this.pnlSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSticker.Name = "pnlSticker";
            this.pnlSticker.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSticker.Size = new System.Drawing.Size(560, 146);
            //this.pnlSticker.Size = new System.Drawing.Size(560, 171); //FL.
            this.pnlSticker.TabIndex = 8;
            this.pnlSticker.TabStop = false;
            this.pnlSticker.Text = "IMPRESORA STICKER";
            // 
            // lblStatusImageSticker
            // 
            this.lblStatusImageSticker.Location = new System.Drawing.Point(247, 12);
            this.lblStatusImageSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblStatusImageSticker.Name = "lblStatusImageSticker";
            this.lblStatusImageSticker.Size = new System.Drawing.Size(53, 39);
            this.lblStatusImageSticker.TabIndex = 15;
            this.lblStatusImageSticker.TabStop = false;
            // 
            // chkPuertoSticker
            // 
            this.chkPuertoSticker.AutoSize = true;
            this.chkPuertoSticker.Location = new System.Drawing.Point(363, 100);
            this.chkPuertoSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPuertoSticker.Name = "chkPuertoSticker";
            this.chkPuertoSticker.Size = new System.Drawing.Size(18, 17);
            this.chkPuertoSticker.TabIndex = 8;
            this.chkPuertoSticker.UseVisualStyleBackColor = true;
            this.chkPuertoSticker.CheckedChanged += new System.EventHandler(this.chkPuertoSticker_CheckedChanged);
            // 
            // chkPuertoUsbSticker
            // 
            //this.chkPuertoUsbSticker.AutoSize = true;
            //this.chkPuertoUsbSticker.Location = new System.Drawing.Point(363, 136);
            //this.chkPuertoUsbSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.chkPuertoUsbSticker.Name = "chkPuertoUsbSticker";
            //this.chkPuertoUsbSticker.Size = new System.Drawing.Size(18, 17);
            //this.chkPuertoUsbSticker.TabIndex = 8;
            //this.chkPuertoUsbSticker.UseVisualStyleBackColor = true;
            //this.chkPuertoUsbSticker.CheckedChanged += new System.EventHandler(this.chkPuertoUsbSticker_CheckedChanged);
            // 
            // cboPtosSticker
            // 
            this.cboPtosSticker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPtosSticker.FormattingEnabled = true;
            this.cboPtosSticker.Location = new System.Drawing.Point(403, 96);
            this.cboPtosSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboPtosSticker.Name = "cboPtosSticker";
            this.cboPtosSticker.Size = new System.Drawing.Size(145, 25);
            this.cboPtosSticker.TabIndex = 7;
            // 
            // cboPtosUsbSticker
            // 
            //this.cboPtosUsbSticker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //this.cboPtosUsbSticker.FormattingEnabled = true;
            //this.cboPtosUsbSticker.Location = new System.Drawing.Point(403, 132);
            //this.cboPtosUsbSticker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.cboPtosUsbSticker.Name = "cboPtosUsbSticker";
            //this.cboPtosUsbSticker.Size = new System.Drawing.Size(145, 25);
            //this.cboPtosUsbSticker.TabIndex = 7;
            // 
            // pnlVoucher
            //
            //this.pnlVoucher.Controls.Add(this.lblPuertoUsbImpresoraVoucher); //FL.
            this.pnlVoucher.Controls.Add(this.lblStatusImageVoucher);
            this.pnlVoucher.Controls.Add(this.chkPuertoVoucher);
            //this.pnlVoucher.Controls.Add(this.chkPuertoUsbVoucher); //FL.
            this.pnlVoucher.Controls.Add(this.cboPtosVoucher);
            //this.pnlVoucher.Controls.Add(this.cboPtosUsbVoucher); //FL.
            this.pnlVoucher.Controls.Add(this.lblStatusImpresoraVoucher);
            this.pnlVoucher.Controls.Add(this.lblPuertoImpresoraVoucher);
            this.pnlVoucher.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlVoucher.ForeColor = System.Drawing.Color.RoyalBlue;
            //this.pnlVoucher.Location = new System.Drawing.Point(19, 110);
            this.pnlVoucher.Location = new System.Drawing.Point(19, 124); //FL.
            this.pnlVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlVoucher.Name = "pnlVoucher";
            this.pnlVoucher.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlVoucher.Size = new System.Drawing.Size(560, 143);
            //this.pnlVoucher.Size = new System.Drawing.Size(560, 170); //FL. 
            this.pnlVoucher.TabIndex = 9;
            this.pnlVoucher.TabStop = false;
            this.pnlVoucher.Text = "IMPRESORA VOUCHER";
            // 
            // lblStatusImageVoucher
            // 
            this.lblStatusImageVoucher.Location = new System.Drawing.Point(247, 12);
            this.lblStatusImageVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lblStatusImageVoucher.Name = "lblStatusImageVoucher";
            this.lblStatusImageVoucher.Size = new System.Drawing.Size(53, 39);
            this.lblStatusImageVoucher.TabIndex = 7;
            this.lblStatusImageVoucher.TabStop = false;
            // 
            // chkPuertoVoucher
            // 
            this.chkPuertoVoucher.AutoSize = true;
            this.chkPuertoVoucher.Location = new System.Drawing.Point(363, 100);
            this.chkPuertoVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPuertoVoucher.Name = "chkPuertoVoucher";
            this.chkPuertoVoucher.Size = new System.Drawing.Size(18, 17);
            this.chkPuertoVoucher.TabIndex = 6;
            this.chkPuertoVoucher.UseVisualStyleBackColor = true;
            this.chkPuertoVoucher.CheckedChanged += new System.EventHandler(this.chkPuertoVoucher_CheckedChanged);
            // 
            // chkPuertoUsbVoucher
            // 
            //this.chkPuertoUsbVoucher.AutoSize = true;
            //this.chkPuertoUsbVoucher.Location = new System.Drawing.Point(363, 137);
            //this.chkPuertoUsbVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.chkPuertoUsbVoucher.Name = "chkPuertoUsbVoucher";
            //this.chkPuertoUsbVoucher.Size = new System.Drawing.Size(18, 17);
            //this.chkPuertoUsbVoucher.TabIndex = 12;
            //this.chkPuertoUsbVoucher.UseVisualStyleBackColor = true;
            //this.chkPuertoUsbVoucher.CheckedChanged += new System.EventHandler(this.chkPuertoUsbVoucher_CheckedChanged);
            // 
            // cboPtosVoucher
            // 
            this.cboPtosVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPtosVoucher.FormattingEnabled = true;
            this.cboPtosVoucher.Location = new System.Drawing.Point(403, 96);
            this.cboPtosVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboPtosVoucher.Name = "cboPtosVoucher";
            this.cboPtosVoucher.Size = new System.Drawing.Size(145, 25);
            this.cboPtosVoucher.TabIndex = 5;
            // 
            // cboPtosUsbVoucher
            // 
            //this.cboPtosUsbVoucher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //this.cboPtosUsbVoucher.FormattingEnabled = true;
            //this.cboPtosUsbVoucher.Location = new System.Drawing.Point(403, 132);
            //this.cboPtosUsbVoucher.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.cboPtosUsbVoucher.Name = "cboPtosUsbVoucher";
            //this.cboPtosUsbVoucher.Size = new System.Drawing.Size(145, 25);
            //this.cboPtosUsbVoucher.TabIndex = 5;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.Control;
            this.btnActualizar.Location = new System.Drawing.Point(65, 455);
            this.btnActualizar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(144, 28);
            this.btnActualizar.TabIndex = 10;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.SystemColors.Control;
            this.btnSalir.Location = new System.Drawing.Point(255, 455);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(113, 28);
            this.btnSalir.TabIndex = 11;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click_1);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.SystemColors.Control;
            this.btnImprimir.Location = new System.Drawing.Point(405, 455);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(119, 28);
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
            //this.btnRefresh.Location = new System.Drawing.Point(524, 62); //FL.
            this.btnRefresh.Location = new System.Drawing.Point(524, 78); 
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 39);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblPuertosDisponibles
            // 
            //this.lblPuertosDisponibles.Location = new System.Drawing.Point(431, 55); //FL.
            this.lblPuertosDisponibles.Location = new System.Drawing.Point(431, 71);
            this.lblPuertosDisponibles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPuertosDisponibles.Name = "lblPuertosDisponibles";
            this.lblPuertosDisponibles.Size = new System.Drawing.Size(91, 52);
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
            this.pnlConfigManualPuertos.Controls.Add(this.pnlVoucher);
            this.pnlConfigManualPuertos.Controls.Add(this.btnImprimir);
            this.pnlConfigManualPuertos.Controls.Add(this.btnActualizar);
            this.pnlConfigManualPuertos.Controls.Add(this.btnSalir);
            this.pnlConfigManualPuertos.Location = new System.Drawing.Point(0, 0);
            this.pnlConfigManualPuertos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlConfigManualPuertos.Name = "pnlConfigManualPuertos";
            this.pnlConfigManualPuertos.Size = new System.Drawing.Size(600, 554);
            this.pnlConfigManualPuertos.TabIndex = 15;
            // 
            // frmPrintNet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 527);
            this.ControlBox = false;
            this.Controls.Add(this.pnlConfigManualPuertos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmPrintNet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Impresion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPrint_FormClosing);
            this.pnlSticker.ResumeLayout(false);
            this.pnlSticker.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageSticker)).EndInit();
            this.pnlVoucher.ResumeLayout(false);
            this.pnlVoucher.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblStatusImageVoucher)).EndInit();
            this.pnlConfigManualPuertos.ResumeLayout(false);
            this.pnlConfigManualPuertos.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTituloConfig;
        private System.Windows.Forms.Label lblStatusImpresoraVoucher;
        private System.Windows.Forms.Label lblPuertoImpresoraVoucher;
        private System.Windows.Forms.Label lblStatusImpresoraSticker;
        private System.Windows.Forms.Label lblPuertoImpresoraSticker;
        private System.Windows.Forms.GroupBox pnlSticker;
        private System.Windows.Forms.CheckBox chkPuertoSticker;
        //private System.Windows.Forms.CheckBox chkPuertoUsbSticker; //FL.
        private System.Windows.Forms.ComboBox cboPtosSticker;
        //private System.Windows.Forms.ComboBox cboPtosUsbSticker; //FL.
        private System.Windows.Forms.GroupBox pnlVoucher;
        private System.Windows.Forms.CheckBox chkPuertoVoucher;
        //private System.Windows.Forms.CheckBox chkPuertoUsbVoucher; //FL.
        private System.Windows.Forms.ComboBox cboPtosVoucher;
        //private System.Windows.Forms.ComboBox cboPtosUsbVoucher; //FL.
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblPuertosDisponibles;
        private System.Windows.Forms.PictureBox lblStatusImageSticker;
        private System.Windows.Forms.PictureBox lblStatusImageVoucher;
        private System.Windows.Forms.Panel pnlConfigManualPuertos;
        //private System.Windows.Forms.Label lblPuertoUsbImpresoraVoucher; //FL.
        //private System.Windows.Forms.Label lblPuertoUsbImpresoraSticker; //FL.
    }
}

