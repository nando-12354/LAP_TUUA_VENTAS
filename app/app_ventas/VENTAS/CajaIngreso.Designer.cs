namespace LAP.TUUA.VENTAS
{
    partial class CajaIngreso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CajaIngreso));
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dgvIngreso = new System.Windows.Forms.DataGridView();
            this.sCodMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDscMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDscSimbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.erpMontos = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbxIngCaja = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngreso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).BeginInit();
            this.gbxIngCaja.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(174, 264);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.Location = new System.Drawing.Point(342, 264);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 25);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // dgvIngreso
            // 
            this.dgvIngreso.AllowUserToAddRows = false;
            this.dgvIngreso.AllowUserToDeleteRows = false;
            this.dgvIngreso.AllowUserToResizeColumns = false;
            this.dgvIngreso.AllowUserToResizeRows = false;
            this.dgvIngreso.AutoGenerateColumns = false;
            this.dgvIngreso.BackgroundColor = System.Drawing.Color.White;
            this.dgvIngreso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIngreso.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sCodMonedaDataGridViewTextBoxColumn,
            this.sDscMonedaDataGridViewTextBoxColumn,
            this.txtMonto,
            this.SDscSimbolo});
            this.dgvIngreso.DataSource = this.monedaBindingSource;
            this.dgvIngreso.Location = new System.Drawing.Point(25, 37);
            this.dgvIngreso.MultiSelect = false;
            this.dgvIngreso.Name = "dgvIngreso";
            this.dgvIngreso.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvIngreso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvIngreso.Size = new System.Drawing.Size(301, 111);
            this.dgvIngreso.TabIndex = 10;
            this.dgvIngreso.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvIngreso_EditingControlShowing);
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
            // erpMontos
            // 
            this.erpMontos.ContainerControl = this;
            // 
            // gbxIngCaja
            // 
            this.gbxIngCaja.Controls.Add(this.dgvIngreso);
            this.gbxIngCaja.Location = new System.Drawing.Point(127, 65);
            this.gbxIngCaja.Name = "gbxIngCaja";
            this.gbxIngCaja.Size = new System.Drawing.Size(355, 163);
            this.gbxIngCaja.TabIndex = 11;
            this.gbxIngCaja.TabStop = false;
            this.gbxIngCaja.Text = "Ingreso de Caja";
            // 
            // CajaIngreso
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.gbxIngCaja);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CajaIngreso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "GUICajaIngreso";
            this.Load += new System.EventHandler(this.CajaIngreso_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CajaIngreso_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIngreso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).EndInit();
            this.gbxIngCaja.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dgvIngreso;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.ErrorProvider erpMontos;
        private System.Windows.Forms.GroupBox gbxIngCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCodMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDscMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtMonto;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDscSimbolo;

    }
}