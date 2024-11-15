namespace LAP.TUUA.ARCHIVAMIENTO
{
    partial class DetalleArchivamiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetalleArchivamiento));
            this.btnCerrar = new System.Windows.Forms.Button();
            this.treeViewRangos = new System.Windows.Forms.TreeView();
            this.grbRangos = new System.Windows.Forms.GroupBox();
            this.grbPasosRealizados = new System.Windows.Forms.GroupBox();
            this.grbRangos.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnCerrar.Location = new System.Drawing.Point(182, 594);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(101, 25);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // treeViewRangos
            // 
            this.treeViewRangos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewRangos.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewRangos.Indent = 30;
            this.treeViewRangos.Location = new System.Drawing.Point(16, 19);
            this.treeViewRangos.Name = "treeViewRangos";
            this.treeViewRangos.ShowPlusMinus = false;
            this.treeViewRangos.Size = new System.Drawing.Size(403, 277);
            this.treeViewRangos.TabIndex = 1;
            // 
            // grbRangos
            // 
            this.grbRangos.BackColor = System.Drawing.Color.Transparent;
            this.grbRangos.Controls.Add(this.treeViewRangos);
            this.grbRangos.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRangos.Location = new System.Drawing.Point(12, 262);
            this.grbRangos.Name = "grbRangos";
            this.grbRangos.Size = new System.Drawing.Size(442, 315);
            this.grbRangos.TabIndex = 2;
            this.grbRangos.TabStop = false;
            this.grbRangos.Text = "RANGOS";
            // 
            // grbPasosRealizados
            // 
            this.grbPasosRealizados.BackColor = System.Drawing.Color.Transparent;
            this.grbPasosRealizados.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbPasosRealizados.Location = new System.Drawing.Point(12, 15);
            this.grbPasosRealizados.Name = "grbPasosRealizados";
            this.grbPasosRealizados.Size = new System.Drawing.Size(442, 231);
            this.grbPasosRealizados.TabIndex = 3;
            this.grbPasosRealizados.TabStop = false;
            this.grbPasosRealizados.Text = "PASOS REALIZADOS";
            // 
            // DetalleArchivamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(466, 637);
            this.ControlBox = false;
            this.Controls.Add(this.grbPasosRealizados);
            this.Controls.Add(this.grbRangos);
            this.Controls.Add(this.btnCerrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "DetalleArchivamiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetalleArchivamiento";
            this.grbRangos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TreeView treeViewRangos;
        private System.Windows.Forms.GroupBox grbRangos;
        private System.Windows.Forms.GroupBox grbPasosRealizados;
    }
}