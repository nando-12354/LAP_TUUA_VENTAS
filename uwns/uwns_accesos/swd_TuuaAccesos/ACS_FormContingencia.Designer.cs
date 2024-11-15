namespace LAP.TUUA.ACCESOS
{
      partial class ACS_FormContingencia
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
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ACS_FormContingencia));
                this.pbxSemaforo = new System.Windows.Forms.PictureBox();
                this.groupBox1 = new System.Windows.Forms.GroupBox();
                this.btnEnviar = new System.Windows.Forms.Button();
                this.chkTrama = new System.Windows.Forms.CheckBox();
                this.txtbTicket = new System.Windows.Forms.TextBox();
                this.label1 = new System.Windows.Forms.Label();
                this.groupBox2 = new System.Windows.Forms.GroupBox();
                this.btnAgregar = new System.Windows.Forms.Button();
                this.lblMensaje = new System.Windows.Forms.Label();
                this.pbxAmarillo = new System.Windows.Forms.PictureBox();
                this.pbxVerde = new System.Windows.Forms.PictureBox();
                this.groupBox3 = new System.Windows.Forms.GroupBox();
                this.label6 = new System.Windows.Forms.Label();
                this.label5 = new System.Windows.Forms.Label();
                this.label4 = new System.Windows.Forms.Label();
                this.label3 = new System.Windows.Forms.Label();
                this.txtbPasajero = new System.Windows.Forms.TextBox();
                this.txtbNroAsiento = new System.Windows.Forms.TextBox();
                this.txtbFechaVuelo = new System.Windows.Forms.TextBox();
                this.txtbVuelo = new System.Windows.Forms.TextBox();
                this.label2 = new System.Windows.Forms.Label();
                this.txtbAerolinea = new System.Windows.Forms.TextBox();
                this.groupBox4 = new System.Windows.Forms.GroupBox();
                this.dataGridView1 = new System.Windows.Forms.DataGridView();
                this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.lblTotal = new System.Windows.Forms.Label();
                this.tmrUploadTest = new System.Windows.Forms.Timer(this.components);
                ((System.ComponentModel.ISupportInitialize)(this.pbxSemaforo)).BeginInit();
                this.groupBox1.SuspendLayout();
                this.groupBox2.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pbxAmarillo)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.pbxVerde)).BeginInit();
                this.groupBox3.SuspendLayout();
                this.groupBox4.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
                this.SuspendLayout();
                // 
                // pbxSemaforo
                // 
                this.pbxSemaforo.Image = ((System.Drawing.Image)(resources.GetObject("pbxSemaforo.Image")));
                this.pbxSemaforo.Location = new System.Drawing.Point(35, 85);
                this.pbxSemaforo.Name = "pbxSemaforo";
                this.pbxSemaforo.Size = new System.Drawing.Size(99, 95);
                this.pbxSemaforo.TabIndex = 1;
                this.pbxSemaforo.TabStop = false;
                // 
                // groupBox1
                // 
                this.groupBox1.Controls.Add(this.btnEnviar);
                this.groupBox1.Controls.Add(this.chkTrama);
                this.groupBox1.Controls.Add(this.txtbTicket);
                this.groupBox1.Controls.Add(this.label1);
                this.groupBox1.Location = new System.Drawing.Point(12, 12);
                this.groupBox1.Name = "groupBox1";
                this.groupBox1.Size = new System.Drawing.Size(385, 84);
                this.groupBox1.TabIndex = 2;
                this.groupBox1.TabStop = false;
                this.groupBox1.Text = "Ticket";
                // 
                // btnEnviar
                // 
                this.btnEnviar.Enabled = false;
                this.btnEnviar.Location = new System.Drawing.Point(304, 19);
                this.btnEnviar.Name = "btnEnviar";
                this.btnEnviar.Size = new System.Drawing.Size(75, 23);
                this.btnEnviar.TabIndex = 102;
                this.btnEnviar.Text = "Enviar";
                this.btnEnviar.UseVisualStyleBackColor = true;
                this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
                // 
                // chkTrama
                // 
                this.chkTrama.AutoSize = true;
                this.chkTrama.Enabled = false;
                this.chkTrama.Location = new System.Drawing.Point(21, 20);
                this.chkTrama.Name = "chkTrama";
                this.chkTrama.Size = new System.Drawing.Size(87, 17);
                this.chkTrama.TabIndex = 101;
                this.chkTrama.Text = "Pegar Trama";
                this.chkTrama.UseVisualStyleBackColor = true;
                this.chkTrama.CheckedChanged += new System.EventHandler(this.chkTrama_CheckedChanged);
                // 
                // txtbTicket
                // 
                this.txtbTicket.BackColor = System.Drawing.SystemColors.Window;
                this.txtbTicket.Location = new System.Drawing.Point(113, 52);
                this.txtbTicket.Name = "txtbTicket";
                this.txtbTicket.ReadOnly = true;
                this.txtbTicket.Size = new System.Drawing.Size(266, 20);
                this.txtbTicket.TabIndex = 100;
                this.txtbTicket.TextChanged += new System.EventHandler(this.txtbTicket_TextChanged);
                // 
                // label1
                // 
                this.label1.AutoSize = true;
                this.label1.Location = new System.Drawing.Point(18, 52);
                this.label1.Name = "label1";
                this.label1.Size = new System.Drawing.Size(40, 13);
                this.label1.TabIndex = 0;
                this.label1.Text = "Codigo";
                // 
                // groupBox2
                // 
                this.groupBox2.Controls.Add(this.btnAgregar);
                this.groupBox2.Controls.Add(this.lblMensaje);
                this.groupBox2.Controls.Add(this.pbxAmarillo);
                this.groupBox2.Controls.Add(this.pbxVerde);
                this.groupBox2.Controls.Add(this.pbxSemaforo);
                this.groupBox2.Location = new System.Drawing.Point(417, 12);
                this.groupBox2.Name = "groupBox2";
                this.groupBox2.Size = new System.Drawing.Size(463, 315);
                this.groupBox2.TabIndex = 3;
                this.groupBox2.TabStop = false;
                this.groupBox2.Text = "Indicador de Lectura";
                // 
                // btnAgregar
                // 
                this.btnAgregar.Location = new System.Drawing.Point(600, 28);
                this.btnAgregar.Name = "btnAgregar";
                this.btnAgregar.Size = new System.Drawing.Size(26, 20);
                this.btnAgregar.TabIndex = 5;
                this.btnAgregar.TabStop = false;
                this.btnAgregar.Text = "Agregar";
                this.btnAgregar.UseVisualStyleBackColor = true;
                this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
                // 
                // lblMensaje
                // 
                this.lblMensaje.AutoSize = true;
                this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblMensaje.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
                this.lblMensaje.Location = new System.Drawing.Point(6, 211);
                this.lblMensaje.Name = "lblMensaje";
                this.lblMensaje.Size = new System.Drawing.Size(0, 16);
                this.lblMensaje.TabIndex = 4;
                // 
                // pbxAmarillo
                // 
                this.pbxAmarillo.Image = ((System.Drawing.Image)(resources.GetObject("pbxAmarillo.Image")));
                this.pbxAmarillo.Location = new System.Drawing.Point(281, 83);
                this.pbxAmarillo.Name = "pbxAmarillo";
                this.pbxAmarillo.Size = new System.Drawing.Size(99, 95);
                this.pbxAmarillo.TabIndex = 3;
                this.pbxAmarillo.TabStop = false;
                // 
                // pbxVerde
                // 
                this.pbxVerde.Image = ((System.Drawing.Image)(resources.GetObject("pbxVerde.Image")));
                this.pbxVerde.Location = new System.Drawing.Point(161, 83);
                this.pbxVerde.Name = "pbxVerde";
                this.pbxVerde.Size = new System.Drawing.Size(99, 95);
                this.pbxVerde.TabIndex = 2;
                this.pbxVerde.TabStop = false;
                // 
                // groupBox3
                // 
                this.groupBox3.Controls.Add(this.label6);
                this.groupBox3.Controls.Add(this.label5);
                this.groupBox3.Controls.Add(this.label4);
                this.groupBox3.Controls.Add(this.label3);
                this.groupBox3.Controls.Add(this.txtbPasajero);
                this.groupBox3.Controls.Add(this.txtbNroAsiento);
                this.groupBox3.Controls.Add(this.txtbFechaVuelo);
                this.groupBox3.Controls.Add(this.txtbVuelo);
                this.groupBox3.Controls.Add(this.label2);
                this.groupBox3.Controls.Add(this.txtbAerolinea);
                this.groupBox3.Location = new System.Drawing.Point(12, 112);
                this.groupBox3.Name = "groupBox3";
                this.groupBox3.Size = new System.Drawing.Size(385, 215);
                this.groupBox3.TabIndex = 4;
                this.groupBox3.TabStop = false;
                this.groupBox3.Text = "Boarding Pas";
                // 
                // label6
                // 
                this.label6.AutoSize = true;
                this.label6.Location = new System.Drawing.Point(18, 166);
                this.label6.Name = "label6";
                this.label6.Size = new System.Drawing.Size(88, 13);
                this.label6.TabIndex = 9;
                this.label6.Text = "Nombre Pasajero";
                // 
                // label5
                // 
                this.label5.AutoSize = true;
                this.label5.Location = new System.Drawing.Point(18, 137);
                this.label5.Name = "label5";
                this.label5.Size = new System.Drawing.Size(62, 13);
                this.label5.TabIndex = 8;
                this.label5.Text = "Nro Asiento";
                // 
                // label4
                // 
                this.label4.AutoSize = true;
                this.label4.Location = new System.Drawing.Point(18, 102);
                this.label4.Name = "label4";
                this.label4.Size = new System.Drawing.Size(67, 13);
                this.label4.TabIndex = 7;
                this.label4.Text = "Fecha Vuelo";
                // 
                // label3
                // 
                this.label3.AutoSize = true;
                this.label3.Location = new System.Drawing.Point(18, 66);
                this.label3.Name = "label3";
                this.label3.Size = new System.Drawing.Size(54, 13);
                this.label3.TabIndex = 6;
                this.label3.Text = "Nro Vuelo";
                // 
                // txtbPasajero
                // 
                this.txtbPasajero.BackColor = System.Drawing.SystemColors.Window;
                this.txtbPasajero.Location = new System.Drawing.Point(113, 159);
                this.txtbPasajero.Name = "txtbPasajero";
                this.txtbPasajero.ReadOnly = true;
                this.txtbPasajero.Size = new System.Drawing.Size(266, 20);
                this.txtbPasajero.TabIndex = 5;
                // 
                // txtbNroAsiento
                // 
                this.txtbNroAsiento.BackColor = System.Drawing.SystemColors.Window;
                this.txtbNroAsiento.Location = new System.Drawing.Point(113, 130);
                this.txtbNroAsiento.Name = "txtbNroAsiento";
                this.txtbNroAsiento.ReadOnly = true;
                this.txtbNroAsiento.Size = new System.Drawing.Size(266, 20);
                this.txtbNroAsiento.TabIndex = 4;
                // 
                // txtbFechaVuelo
                // 
                this.txtbFechaVuelo.BackColor = System.Drawing.SystemColors.Window;
                this.txtbFechaVuelo.Location = new System.Drawing.Point(113, 95);
                this.txtbFechaVuelo.Name = "txtbFechaVuelo";
                this.txtbFechaVuelo.ReadOnly = true;
                this.txtbFechaVuelo.Size = new System.Drawing.Size(266, 20);
                this.txtbFechaVuelo.TabIndex = 3;
                // 
                // txtbVuelo
                // 
                this.txtbVuelo.BackColor = System.Drawing.SystemColors.Window;
                this.txtbVuelo.ForeColor = System.Drawing.Color.Black;
                this.txtbVuelo.Location = new System.Drawing.Point(113, 59);
                this.txtbVuelo.Name = "txtbVuelo";
                this.txtbVuelo.ReadOnly = true;
                this.txtbVuelo.Size = new System.Drawing.Size(266, 20);
                this.txtbVuelo.TabIndex = 2;
                // 
                // label2
                // 
                this.label2.AutoSize = true;
                this.label2.Location = new System.Drawing.Point(18, 35);
                this.label2.Name = "label2";
                this.label2.Size = new System.Drawing.Size(51, 13);
                this.label2.TabIndex = 1;
                this.label2.Text = "Aerolinea";
                // 
                // txtbAerolinea
                // 
                this.txtbAerolinea.BackColor = System.Drawing.SystemColors.Window;
                this.txtbAerolinea.Location = new System.Drawing.Point(113, 28);
                this.txtbAerolinea.Name = "txtbAerolinea";
                this.txtbAerolinea.ReadOnly = true;
                this.txtbAerolinea.Size = new System.Drawing.Size(266, 20);
                this.txtbAerolinea.TabIndex = 0;
                // 
                // groupBox4
                // 
                this.groupBox4.Controls.Add(this.dataGridView1);
                this.groupBox4.Controls.Add(this.lblTotal);
                this.groupBox4.Location = new System.Drawing.Point(12, 344);
                this.groupBox4.Name = "groupBox4";
                this.groupBox4.Size = new System.Drawing.Size(868, 267);
                this.groupBox4.TabIndex = 6;
                this.groupBox4.TabStop = false;
                this.groupBox4.Text = "Listado Ticket/Boarding  Usados";
                // 
                // dataGridView1
                // 
                this.dataGridView1.AllowUserToDeleteRows = false;
                this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
                this.dataGridView1.Location = new System.Drawing.Point(21, 28);
                this.dataGridView1.Name = "dataGridView1";
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.Size = new System.Drawing.Size(831, 188);
                this.dataGridView1.TabIndex = 8;
                // 
                // Column1
                // 
                this.Column1.HeaderText = "Hora";
                this.Column1.Name = "Column1";
                this.Column1.ReadOnly = true;
                this.Column1.Width = 118;
                // 
                // Column2
                // 
                this.Column2.HeaderText = "Documento";
                this.Column2.Name = "Column2";
                this.Column2.ReadOnly = true;
                // 
                // Column3
                // 
                this.Column3.HeaderText = "Codigo";
                this.Column3.Name = "Column3";
                this.Column3.ReadOnly = true;
                this.Column3.Width = 250;
                // 
                // Column4
                // 
                this.Column4.HeaderText = "Detalle";
                this.Column4.Name = "Column4";
                this.Column4.ReadOnly = true;
                this.Column4.Width = 320;
                // 
                // lblTotal
                // 
                this.lblTotal.AutoSize = true;
                this.lblTotal.Location = new System.Drawing.Point(6, 230);
                this.lblTotal.Name = "lblTotal";
                this.lblTotal.Size = new System.Drawing.Size(65, 13);
                this.lblTotal.TabIndex = 7;
                this.lblTotal.Text = "Total Leidos";
                // 
                // tmrUploadTest
                // 
                this.tmrUploadTest.Interval = 30000;
                this.tmrUploadTest.Tick += new System.EventHandler(this.tmrUploadTest_Tick);
                // 
                // ACS_FormContingencia
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ClientSize = new System.Drawing.Size(895, 613);
                this.Controls.Add(this.groupBox4);
                this.Controls.Add(this.groupBox3);
                this.Controls.Add(this.groupBox2);
                this.Controls.Add(this.groupBox1);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                this.Name = "ACS_FormContingencia";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Modulo Accesos- Contingencia";
                this.Load += new System.EventHandler(this.ACS_FormContingencia_Load);
                this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ACS_FormContingencia_KeyPress);
                ((System.ComponentModel.ISupportInitialize)(this.pbxSemaforo)).EndInit();
                this.groupBox1.ResumeLayout(false);
                this.groupBox1.PerformLayout();
                this.groupBox2.ResumeLayout(false);
                this.groupBox2.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.pbxAmarillo)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.pbxVerde)).EndInit();
                this.groupBox3.ResumeLayout(false);
                this.groupBox3.PerformLayout();
                this.groupBox4.ResumeLayout(false);
                this.groupBox4.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
                this.ResumeLayout(false);

            }

            #endregion

            private System.Windows.Forms.PictureBox pbxSemaforo;
            private System.Windows.Forms.GroupBox groupBox1;
            private System.Windows.Forms.GroupBox groupBox2;
            private System.Windows.Forms.GroupBox groupBox3;
            public System.Windows.Forms.TextBox txtbTicket;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Label label2;
            public System.Windows.Forms.TextBox txtbAerolinea;
            private System.Windows.Forms.Label label6;
            private System.Windows.Forms.Label label5;
            private System.Windows.Forms.Label label4;
            private System.Windows.Forms.Label label3;
            public System.Windows.Forms.TextBox txtbPasajero;
            public System.Windows.Forms.TextBox txtbNroAsiento;
            public System.Windows.Forms.TextBox txtbFechaVuelo;
            public System.Windows.Forms.TextBox txtbVuelo;
            private System.Windows.Forms.PictureBox pbxAmarillo;
            private System.Windows.Forms.PictureBox pbxVerde;
            public System.Windows.Forms.Label lblMensaje;
            private System.Windows.Forms.GroupBox groupBox4;
            public System.Windows.Forms.Label lblTotal;
            public System.Windows.Forms.DataGridView dataGridView1;
            private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
            private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
            private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
            private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
            private System.Windows.Forms.Timer tmrUploadTest;
            private System.Windows.Forms.Button btnAgregar;
            private System.Windows.Forms.CheckBox chkTrama;
            private System.Windows.Forms.Button btnEnviar;
      }
}