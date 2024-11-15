namespace LAP.TUUA.VENTAS
{
    partial class CajaEgreso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CajaEgreso));
            this.dgvEgreso = new System.Windows.Forms.DataGridView();
            this.sCodMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDscMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDscSimbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.gbxEgCaja = new System.Windows.Forms.GroupBox();
            this.erpMontos = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEgreso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            this.gbxEgCaja.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEgreso
            // 
            this.dgvEgreso.AllowUserToAddRows = false;
            this.dgvEgreso.AllowUserToDeleteRows = false;
            this.dgvEgreso.AllowUserToResizeColumns = false;
            this.dgvEgreso.AllowUserToResizeRows = false;
            this.dgvEgreso.AutoGenerateColumns = false;
            this.dgvEgreso.BackgroundColor = System.Drawing.Color.White;
            this.dgvEgreso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEgreso.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sCodMonedaDataGridViewTextBoxColumn,
            this.sDscMonedaDataGridViewTextBoxColumn,
            this.txtMonto,
            this.SDscSimbolo});
            this.dgvEgreso.DataSource = this.monedaBindingSource;
            this.dgvEgreso.Location = new System.Drawing.Point(25, 37);
            this.dgvEgreso.MultiSelect = false;
            this.dgvEgreso.Name = "dgvEgreso";
            this.dgvEgreso.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEgreso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvEgreso.Size = new System.Drawing.Size(301, 111);
            this.dgvEgreso.TabIndex = 11;
            this.dgvEgreso.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvEgreso_EditingControlShowing);
            // 
            // sCodMonedaDataGridViewTextBoxColumn
            // 
            this.sCodMonedaDataGridViewTextBoxColumn.DataPropertyName = "SCodMoneda";
            this.sCodMonedaDataGridViewTextBoxColumn.HeaderText = "SCodMoneda";
            this.sCodMonedaDataGridViewTextBoxColumn.Name = "sCodMonedaDataGridViewTextBoxColumn";
            this.sCodMonedaDataGridViewTextBoxColumn.Visible = false;
            // 
            // sDscMonedaDataGridViewTextBoxColumn
            // 
            this.sDscMonedaDataGridViewTextBoxColumn.DataPropertyName = "SDscMoneda";
            this.sDscMonedaDataGridViewTextBoxColumn.HeaderText = "Moneda";
            this.sDscMonedaDataGridViewTextBoxColumn.Name = "sDscMonedaDataGridViewTextBoxColumn";
            this.sDscMonedaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // txtMonto
            // 
            this.txtMonto.HeaderText = "Montos";
            this.txtMonto.MaxInputLength = 8;
            this.txtMonto.Name = "txtMonto";
            // 
            // SDscSimbolo
            // 
            this.SDscSimbolo.DataPropertyName = "SDscSimbolo";
            this.SDscSimbolo.HeaderText = "SDscSimbolo";
            this.SDscSimbolo.Name = "SDscSimbolo";
            this.SDscSimbolo.Visible = false;
            // 
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(LAP.TUUA.ENTIDADES.Moneda);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(342, 264);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 13;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(174, 264);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 12;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // gbxEgCaja
            // 
            this.gbxEgCaja.Controls.Add(this.dgvEgreso);
            this.gbxEgCaja.Location = new System.Drawing.Point(127, 65);
            this.gbxEgCaja.Name = "gbxEgCaja";
            this.gbxEgCaja.Size = new System.Drawing.Size(355, 163);
            this.gbxEgCaja.TabIndex = 14;
            this.gbxEgCaja.TabStop = false;
            this.gbxEgCaja.Text = "Egreso de Caja";
            // 
            // erpMontos
            // 
            this.erpMontos.ContainerControl = this;
            // 
            // CajaEgreso
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.gbxEgCaja);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CajaEgreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GUICajaEgreso";
            this.Load += new System.EventHandler(this.CajaEgreso_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CajaEgreso_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEgreso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            this.gbxEgCaja.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEgreso;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.GroupBox gbxEgCaja;
        private System.Windows.Forms.ErrorProvider erpMontos;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCodMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDscMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDscSimbolo;
    }
}