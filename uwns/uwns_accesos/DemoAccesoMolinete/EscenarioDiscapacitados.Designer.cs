namespace DemoAccesoMolinete
{
    partial class EscenarioDiscapacitados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EscenarioDiscapacitados));
            this.picSemaforo = new System.Windows.Forms.PictureBox();
            this.grbValido = new System.Windows.Forms.GroupBox();
            this.btnPasarTicketNormalOk = new System.Windows.Forms.Button();
            this.btnPasarTicketEspecialOk = new System.Windows.Forms.Button();
            this.lblStatusMolinete = new System.Windows.Forms.Label();
            this.grbSemaforo = new System.Windows.Forms.GroupBox();
            this.PaletaProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnPasarTicketNoOk = new System.Windows.Forms.Button();
            this.grbInvalido = new System.Windows.Forms.GroupBox();
            this.grbMolinete = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSemaforo)).BeginInit();
            this.grbValido.SuspendLayout();
            this.grbSemaforo.SuspendLayout();
            this.grbInvalido.SuspendLayout();
            this.grbMolinete.SuspendLayout();
            this.SuspendLayout();
            // 
            // picSemaforo
            // 
            this.picSemaforo.Image = ((System.Drawing.Image)(resources.GetObject("picSemaforo.Image")));
            this.picSemaforo.Location = new System.Drawing.Point(64, 20);
            this.picSemaforo.Name = "picSemaforo";
            this.picSemaforo.Size = new System.Drawing.Size(126, 303);
            this.picSemaforo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSemaforo.TabIndex = 0;
            this.picSemaforo.TabStop = false;
            // 
            // grbValido
            // 
            this.grbValido.Controls.Add(this.btnPasarTicketNormalOk);
            this.grbValido.Controls.Add(this.btnPasarTicketEspecialOk);
            this.grbValido.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbValido.Location = new System.Drawing.Point(29, 26);
            this.grbValido.Name = "grbValido";
            this.grbValido.Size = new System.Drawing.Size(256, 145);
            this.grbValido.TabIndex = 16;
            this.grbValido.TabStop = false;
            this.grbValido.Text = "Valido";
            // 
            // btnPasarTicketNormalOk
            // 
            this.btnPasarTicketNormalOk.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPasarTicketNormalOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPasarTicketNormalOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasarTicketNormalOk.Location = new System.Drawing.Point(17, 22);
            this.btnPasarTicketNormalOk.Name = "btnPasarTicketNormalOk";
            this.btnPasarTicketNormalOk.Size = new System.Drawing.Size(97, 41);
            this.btnPasarTicketNormalOk.TabIndex = 1;
            this.btnPasarTicketNormalOk.Text = "Pasar Ticket Normal Ok";
            this.btnPasarTicketNormalOk.UseVisualStyleBackColor = false;
            this.btnPasarTicketNormalOk.Click += new System.EventHandler(this.btnPasarTicketNormalOk_Click);
            // 
            // btnPasarTicketEspecialOk
            // 
            this.btnPasarTicketEspecialOk.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPasarTicketEspecialOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPasarTicketEspecialOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasarTicketEspecialOk.Location = new System.Drawing.Point(139, 22);
            this.btnPasarTicketEspecialOk.Name = "btnPasarTicketEspecialOk";
            this.btnPasarTicketEspecialOk.Size = new System.Drawing.Size(98, 41);
            this.btnPasarTicketEspecialOk.TabIndex = 11;
            this.btnPasarTicketEspecialOk.Text = "Pasar Ticket Especial Ok";
            this.btnPasarTicketEspecialOk.UseVisualStyleBackColor = false;
            this.btnPasarTicketEspecialOk.Click += new System.EventHandler(this.btnPasarTicketEspecialOk_Click);
            // 
            // lblStatusMolinete
            // 
            this.lblStatusMolinete.AutoSize = true;
            this.lblStatusMolinete.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusMolinete.ForeColor = System.Drawing.Color.Red;
            this.lblStatusMolinete.Location = new System.Drawing.Point(6, 40);
            this.lblStatusMolinete.Name = "lblStatusMolinete";
            this.lblStatusMolinete.Size = new System.Drawing.Size(62, 14);
            this.lblStatusMolinete.TabIndex = 8;
            this.lblStatusMolinete.Text = "CLOSED";
            // 
            // grbSemaforo
            // 
            this.grbSemaforo.Controls.Add(this.picSemaforo);
            this.grbSemaforo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSemaforo.Location = new System.Drawing.Point(310, 144);
            this.grbSemaforo.Name = "grbSemaforo";
            this.grbSemaforo.Size = new System.Drawing.Size(262, 342);
            this.grbSemaforo.TabIndex = 14;
            this.grbSemaforo.TabStop = false;
            this.grbSemaforo.Text = "Semaforo";
            // 
            // PaletaProgressBar
            // 
            this.PaletaProgressBar.Location = new System.Drawing.Point(80, 40);
            this.PaletaProgressBar.Name = "PaletaProgressBar";
            this.PaletaProgressBar.Size = new System.Drawing.Size(176, 23);
            this.PaletaProgressBar.TabIndex = 5;
            // 
            // btnPasarTicketNoOk
            // 
            this.btnPasarTicketNoOk.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnPasarTicketNoOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPasarTicketNoOk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasarTicketNoOk.Location = new System.Drawing.Point(77, 33);
            this.btnPasarTicketNoOk.Name = "btnPasarTicketNoOk";
            this.btnPasarTicketNoOk.Size = new System.Drawing.Size(99, 41);
            this.btnPasarTicketNoOk.TabIndex = 2;
            this.btnPasarTicketNoOk.Text = "Pasar Ticket No Ok";
            this.btnPasarTicketNoOk.UseVisualStyleBackColor = false;
            this.btnPasarTicketNoOk.Click += new System.EventHandler(this.btnPasarTicketNoOk_Click);
            // 
            // grbInvalido
            // 
            this.grbInvalido.Controls.Add(this.btnPasarTicketNoOk);
            this.grbInvalido.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInvalido.Location = new System.Drawing.Point(29, 203);
            this.grbInvalido.Name = "grbInvalido";
            this.grbInvalido.Size = new System.Drawing.Size(256, 100);
            this.grbInvalido.TabIndex = 17;
            this.grbInvalido.TabStop = false;
            this.grbInvalido.Text = "Invalido";
            // 
            // grbMolinete
            // 
            this.grbMolinete.Controls.Add(this.lblStatusMolinete);
            this.grbMolinete.Controls.Add(this.PaletaProgressBar);
            this.grbMolinete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbMolinete.Location = new System.Drawing.Point(310, 26);
            this.grbMolinete.Name = "grbMolinete";
            this.grbMolinete.Size = new System.Drawing.Size(262, 100);
            this.grbMolinete.TabIndex = 15;
            this.grbMolinete.TabStop = false;
            this.grbMolinete.Text = "Molinete";
            // 
            // EscenarioDiscapacitados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(595, 508);
            this.Controls.Add(this.grbValido);
            this.Controls.Add(this.grbSemaforo);
            this.Controls.Add(this.grbInvalido);
            this.Controls.Add(this.grbMolinete);
            this.Name = "EscenarioDiscapacitados";
            this.Text = "EscenarioDiscapacitados";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EscenarioDiscapacitados_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picSemaforo)).EndInit();
            this.grbValido.ResumeLayout(false);
            this.grbSemaforo.ResumeLayout(false);
            this.grbInvalido.ResumeLayout(false);
            this.grbMolinete.ResumeLayout(false);
            this.grbMolinete.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSemaforo;
        private System.Windows.Forms.GroupBox grbValido;
        private System.Windows.Forms.Button btnPasarTicketNormalOk;
        private System.Windows.Forms.Button btnPasarTicketEspecialOk;
        private System.Windows.Forms.Label lblStatusMolinete;
        private System.Windows.Forms.GroupBox grbSemaforo;
        private System.Windows.Forms.ProgressBar PaletaProgressBar;
        private System.Windows.Forms.Button btnPasarTicketNoOk;
        private System.Windows.Forms.GroupBox grbInvalido;
        private System.Windows.Forms.GroupBox grbMolinete;
    }
}