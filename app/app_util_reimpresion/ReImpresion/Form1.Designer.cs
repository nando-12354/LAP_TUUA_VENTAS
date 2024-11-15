namespace REImpresion
{
    partial class frmREImpresion
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
            this.gvTickets = new System.Windows.Forms.DataGridView();
            this.Column_CodigoTicket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCodigoTicket = new System.Windows.Forms.TextBox();
            this.lblCodTicket = new System.Windows.Forms.Label();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.txtFechaVencimiento = new System.Windows.Forms.TextBox();
            this.txtImporte = new System.Windows.Forms.TextBox();
            this.txtDescMoneda = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // gvTickets
            // 
            this.gvTickets.AllowUserToDeleteRows = false;
            this.gvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvTickets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_CodigoTicket});
            this.gvTickets.Location = new System.Drawing.Point(12, 208);
            this.gvTickets.Name = "gvTickets";
            this.gvTickets.ReadOnly = true;
            this.gvTickets.Size = new System.Drawing.Size(271, 233);
            this.gvTickets.TabIndex = 0;
            // 
            // Column_CodigoTicket
            // 
            this.Column_CodigoTicket.HeaderText = "TICKET";
            this.Column_CodigoTicket.Name = "Column_CodigoTicket";
            this.Column_CodigoTicket.ReadOnly = true;
            this.Column_CodigoTicket.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column_CodigoTicket.Width = 200;
            // 
            // txtCodigoTicket
            // 
            this.txtCodigoTicket.Location = new System.Drawing.Point(94, 30);
            this.txtCodigoTicket.MaxLength = 16;
            this.txtCodigoTicket.Name = "txtCodigoTicket";
            this.txtCodigoTicket.Size = new System.Drawing.Size(134, 20);
            this.txtCodigoTicket.TabIndex = 2;
            this.txtCodigoTicket.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoTicket_KeyPress);
            // 
            // lblCodTicket
            // 
            this.lblCodTicket.AutoSize = true;
            this.lblCodTicket.Location = new System.Drawing.Point(12, 33);
            this.lblCodTicket.Name = "lblCodTicket";
            this.lblCodTicket.Size = new System.Drawing.Size(76, 13);
            this.lblCodTicket.TabIndex = 3;
            this.lblCodTicket.Text = "Codigo Ticket:";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Location = new System.Drawing.Point(109, 459);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(75, 23);
            this.btnImprimir.TabIndex = 4;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // txtFechaVencimiento
            // 
            this.txtFechaVencimiento.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtFechaVencimiento.Location = new System.Drawing.Point(128, 80);
            this.txtFechaVencimiento.Name = "txtFechaVencimiento";
            this.txtFechaVencimiento.ReadOnly = true;
            this.txtFechaVencimiento.Size = new System.Drawing.Size(100, 20);
            this.txtFechaVencimiento.TabIndex = 5;
            this.txtFechaVencimiento.Click += new System.EventHandler(this.txtFechaVencimiento_Click);
            this.txtFechaVencimiento.Leave += new System.EventHandler(this.txtFechaVencimiento_Leave);
            // 
            // txtImporte
            // 
            this.txtImporte.Location = new System.Drawing.Point(128, 121);
            this.txtImporte.Name = "txtImporte";
            this.txtImporte.Size = new System.Drawing.Size(100, 20);
            this.txtImporte.TabIndex = 6;
            // 
            // txtDescMoneda
            // 
            this.txtDescMoneda.Location = new System.Drawing.Point(98, 165);
            this.txtDescMoneda.Name = "txtDescMoneda";
            this.txtDescMoneda.Size = new System.Drawing.Size(130, 20);
            this.txtDescMoneda.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Fecha Vencimiento:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Importe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Desc. Moneda:";
            // 
            // btnAgregar
            // 
            this.btnAgregar.AutoSize = true;
            this.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAgregar.Image = global::REImpresion.Properties.Resources.Add;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAgregar.Location = new System.Drawing.Point(246, 26);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(26, 26);
            this.btnAgregar.TabIndex = 12;
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // calendar
            // 
            this.calendar.Location = new System.Drawing.Point(128, 80);
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 13;
            this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateSelected);
            this.calendar.Leave += new System.EventHandler(this.calendar_Leave);
            // 
            // frmREImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 494);
            this.Controls.Add(this.calendar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDescMoneda);
            this.Controls.Add(this.txtImporte);
            this.Controls.Add(this.txtFechaVencimiento);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.lblCodTicket);
            this.Controls.Add(this.txtCodigoTicket);
            this.Controls.Add(this.gvTickets);
            this.Name = "frmREImpresion";
            this.Text = "REImpresion";
            ((System.ComponentModel.ISupportInitialize)(this.gvTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvTickets;
        private System.Windows.Forms.TextBox txtCodigoTicket;
        private System.Windows.Forms.Label lblCodTicket;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_CodigoTicket;
        private System.Windows.Forms.TextBox txtFechaVencimiento;
        private System.Windows.Forms.TextBox txtImporte;
        private System.Windows.Forms.TextBox txtDescMoneda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.MonthCalendar calendar;
    }
}

