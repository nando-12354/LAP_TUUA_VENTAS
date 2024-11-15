namespace DemoAccesoMolinete
{
    partial class OptionForm
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
            this.btnMolineteNormal = new System.Windows.Forms.Button();
            this.btnMolineteDiscapacitados = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnMolineteNormal
            // 
            this.btnMolineteNormal.BackColor = System.Drawing.Color.Maroon;
            this.btnMolineteNormal.ForeColor = System.Drawing.Color.White;
            this.btnMolineteNormal.Location = new System.Drawing.Point(90, 57);
            this.btnMolineteNormal.Name = "btnMolineteNormal";
            this.btnMolineteNormal.Size = new System.Drawing.Size(107, 23);
            this.btnMolineteNormal.TabIndex = 0;
            this.btnMolineteNormal.Text = "Ingresar a Demo";
            this.btnMolineteNormal.UseVisualStyleBackColor = false;
            this.btnMolineteNormal.Click += new System.EventHandler(this.btnMolineteNormal_Click);
            // 
            // btnMolineteDiscapacitados
            // 
            this.btnMolineteDiscapacitados.BackColor = System.Drawing.Color.Maroon;
            this.btnMolineteDiscapacitados.ForeColor = System.Drawing.Color.White;
            this.btnMolineteDiscapacitados.Location = new System.Drawing.Point(90, 173);
            this.btnMolineteDiscapacitados.Name = "btnMolineteDiscapacitados";
            this.btnMolineteDiscapacitados.Size = new System.Drawing.Size(107, 23);
            this.btnMolineteDiscapacitados.TabIndex = 1;
            this.btnMolineteDiscapacitados.Text = "Ingresar a Demo";
            this.btnMolineteDiscapacitados.UseVisualStyleBackColor = false;
            this.btnMolineteDiscapacitados.Click += new System.EventHandler(this.btnMolineteDiscapacitados_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Molinete Normal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(77, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Molinete Discapacitados";
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 236);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMolineteDiscapacitados);
            this.Controls.Add(this.btnMolineteNormal);
            this.Name = "OptionForm";
            this.Text = "Opciones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnMolineteNormal;
        private System.Windows.Forms.Button btnMolineteDiscapacitados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}