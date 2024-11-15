namespace LAP.TUUA.VENTAS
{
    partial class Cuadre
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cuadre));
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gbxCuadre = new System.Windows.Forms.GroupBox();
            this.dgvCuadre = new System.Windows.Forms.DataGridView();
            this.sCodMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDscMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxEfec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxTran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxCheq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxTar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erpMontos = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            this.gbxCuadre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuadre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).BeginInit();
            this.SuspendLayout();
            // 
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(LAP.TUUA.ENTIDADES.Moneda);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(362, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(178, 280);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // gbxCuadre
            // 
            this.gbxCuadre.Controls.Add(this.dgvCuadre);
            this.gbxCuadre.Location = new System.Drawing.Point(43, 65);
            this.gbxCuadre.Name = "gbxCuadre";
            this.gbxCuadre.Size = new System.Drawing.Size(513, 163);
            this.gbxCuadre.TabIndex = 10;
            this.gbxCuadre.TabStop = false;
            this.gbxCuadre.Text = "Cuadre de Turno";
            // 
            // dgvCuadre
            // 
            this.dgvCuadre.AllowUserToAddRows = false;
            this.dgvCuadre.AllowUserToDeleteRows = false;
            this.dgvCuadre.AllowUserToResizeColumns = false;
            this.dgvCuadre.AllowUserToResizeRows = false;
            this.dgvCuadre.AutoGenerateColumns = false;
            this.dgvCuadre.BackgroundColor = System.Drawing.Color.White;
            this.dgvCuadre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCuadre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sCodMonedaDataGridViewTextBoxColumn,
            this.sDscMonedaDataGridViewTextBoxColumn,
            this.dgvtbxEfec,
            this.dgvtbxTran,
            this.dgvtbxCheq,
            this.dgvtbxTar});
            this.dgvCuadre.DataSource = this.monedaBindingSource;
            this.dgvCuadre.Location = new System.Drawing.Point(24, 19);
            this.dgvCuadre.MultiSelect = false;
            this.dgvCuadre.Name = "dgvCuadre";
            this.dgvCuadre.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCuadre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvCuadre.Size = new System.Drawing.Size(464, 129);
            this.dgvCuadre.TabIndex = 11;
            // 
            // sCodMonedaDataGridViewTextBoxColumn
            // 
            this.sCodMonedaDataGridViewTextBoxColumn.DataPropertyName = "SCodMoneda";
            this.sCodMonedaDataGridViewTextBoxColumn.HeaderText = "SCodMoneda";
            this.sCodMonedaDataGridViewTextBoxColumn.Name = "sCodMonedaDataGridViewTextBoxColumn";
            this.sCodMonedaDataGridViewTextBoxColumn.Visible = false;
            this.sCodMonedaDataGridViewTextBoxColumn.Width = 10;
            // 
            // sDscMonedaDataGridViewTextBoxColumn
            // 
            this.sDscMonedaDataGridViewTextBoxColumn.DataPropertyName = "SDscMoneda";
            this.sDscMonedaDataGridViewTextBoxColumn.HeaderText = "Moneda";
            this.sDscMonedaDataGridViewTextBoxColumn.Name = "sDscMonedaDataGridViewTextBoxColumn";
            this.sDscMonedaDataGridViewTextBoxColumn.ReadOnly = true;
            this.sDscMonedaDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgvtbxEfec
            // 
            this.dgvtbxEfec.HeaderText = "Efectivo";
            this.dgvtbxEfec.MaxInputLength = 10;
            this.dgvtbxEfec.Name = "dgvtbxEfec";
            this.dgvtbxEfec.Width = 80;
            // 
            // dgvtbxTran
            // 
            this.dgvtbxTran.HeaderText = "Transferencia";
            this.dgvtbxTran.MaxInputLength = 10;
            this.dgvtbxTran.Name = "dgvtbxTran";
            this.dgvtbxTran.Width = 80;
            // 
            // dgvtbxCheq
            // 
            this.dgvtbxCheq.HeaderText = "Cheque";
            this.dgvtbxCheq.MaxInputLength = 10;
            this.dgvtbxCheq.Name = "dgvtbxCheq";
            this.dgvtbxCheq.Width = 80;
            // 
            // dgvtbxTar
            // 
            this.dgvtbxTar.HeaderText = "Tarjeta Credito/Debito";
            this.dgvtbxTar.MaxInputLength = 10;
            this.dgvtbxTar.Name = "dgvtbxTar";
            this.dgvtbxTar.Width = 80;
            // 
            // erpMontos
            // 
            this.erpMontos.ContainerControl = this;
            // 
            // Cuadre
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.gbxCuadre);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Cuadre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cuadre";
            this.Load += new System.EventHandler(this.Cuadre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            this.gbxCuadre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCuadre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.GroupBox gbxCuadre;
        private System.Windows.Forms.ErrorProvider erpMontos;
        private System.Windows.Forms.DataGridView dgvCuadre;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCodMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDscMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxEfec;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxTran;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxCheq;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxTar;
    }
}