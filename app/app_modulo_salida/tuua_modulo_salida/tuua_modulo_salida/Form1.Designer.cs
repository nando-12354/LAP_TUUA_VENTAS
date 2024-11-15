namespace tuua_modulo_salida
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBP = new System.Windows.Forms.TextBox();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.semaforo = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.semaforo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(190, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(338, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Módulo de Salida de Pasajeros";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(26, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(723, 60);
            this.label2.TabIndex = 1;
            this.label2.Text = "Por favor escanee su Boarding Pass o Ticket de embarque con la lectora de código " +
    "de barras.";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 335);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 71);
            this.button1.TabIndex = 2;
            this.button1.Text = "Registrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBP
            // 
            this.txtBP.Location = new System.Drawing.Point(30, 288);
            this.txtBP.Name = "txtBP";
            this.txtBP.Size = new System.Drawing.Size(719, 20);
            this.txtBP.TabIndex = 3;
            this.txtBP.TextChanged += new System.EventHandler(this.txtBP_TextChanged);
            this.txtBP.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBP_KeyUp);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.ForeColor = System.Drawing.Color.YellowGreen;
            this.lblMensaje.Location = new System.Drawing.Point(152, 470);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(546, 89);
            this.lblMensaje.TabIndex = 5;
            // 
            // semaforo
            // 
            this.semaforo.Image = global::tuua_modulo_salida.Resource1.grey_light;
            this.semaforo.Location = new System.Drawing.Point(18, 431);
            this.semaforo.Name = "semaforo";
            this.semaforo.Size = new System.Drawing.Size(128, 128);
            this.semaforo.TabIndex = 6;
            this.semaforo.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::tuua_modulo_salida.Resource1.logo_lap;
            this.pictureBox1.Location = new System.Drawing.Point(259, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 148);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.semaforo);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblMensaje);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtBP);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 596);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(790, 620);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Módulo de Salida de Pasajeros";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.semaforo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBP;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.PictureBox semaforo;
        private System.Windows.Forms.Panel panel1;
    }
}

