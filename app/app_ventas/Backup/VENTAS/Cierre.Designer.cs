namespace LAP.TUUA.VENTAS
{
    partial class Cierre
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cierre));
            this.dgwCierre = new System.Windows.Forms.DataGridView();
            this.sCodMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDscMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxEfec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxTran = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxCheq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvtbxTar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.erpMontos = new System.Windows.Forms.ErrorProvider(this.components);
            this.pbxLogin = new System.Windows.Forms.PictureBox();
            this.gbxAutenticar = new System.Windows.Forms.GroupBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblCuenta = new System.Windows.Forms.Label();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUserCode = new System.Windows.Forms.TextBox();
            this.erpUser = new System.Windows.Forms.ErrorProvider(this.components);
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgwCierre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).BeginInit();
            this.gbxAutenticar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwCierre
            // 
            this.dgwCierre.AllowUserToAddRows = false;
            this.dgwCierre.AllowUserToDeleteRows = false;
            this.dgwCierre.AllowUserToResizeColumns = false;
            this.dgwCierre.AllowUserToResizeRows = false;
            this.dgwCierre.AutoGenerateColumns = false;
            this.dgwCierre.BackgroundColor = System.Drawing.Color.White;
            this.dgwCierre.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwCierre.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sCodMonedaDataGridViewTextBoxColumn,
            this.sDscMonedaDataGridViewTextBoxColumn,
            this.dgvtbxEfec,
            this.dgvtbxTran,
            this.dgvtbxCheq,
            this.dgvtbxTar});
            this.dgwCierre.DataSource = this.monedaBindingSource;
            this.dgwCierre.Location = new System.Drawing.Point(68, 222);
            this.dgwCierre.MultiSelect = false;
            this.dgwCierre.Name = "dgwCierre";
            this.dgwCierre.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwCierre.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgwCierre.Size = new System.Drawing.Size(464, 129);
            this.dgwCierre.TabIndex = 3;
            this.dgwCierre.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwCierre_EditingControlShowing);
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
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(LAP.TUUA.ENTIDADES.Moneda);
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(457, 78);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(457, 42);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 4;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // erpMontos
            // 
            this.erpMontos.ContainerControl = this;
            // 
            // pbxLogin
            // 
            this.pbxLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbxLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbxLogin.Image")));
            this.pbxLogin.Location = new System.Drawing.Point(457, 116);
            this.pbxLogin.Name = "pbxLogin";
            this.pbxLogin.Size = new System.Drawing.Size(73, 35);
            this.pbxLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxLogin.TabIndex = 13;
            this.pbxLogin.TabStop = false;
            // 
            // gbxAutenticar
            // 
            this.gbxAutenticar.Controls.Add(this.lblClave);
            this.gbxAutenticar.Controls.Add(this.lblCuenta);
            this.gbxAutenticar.Controls.Add(this.txtClave);
            this.gbxAutenticar.Controls.Add(this.txtUserCode);
            this.gbxAutenticar.Location = new System.Drawing.Point(68, 35);
            this.gbxAutenticar.Name = "gbxAutenticar";
            this.gbxAutenticar.Size = new System.Drawing.Size(359, 146);
            this.gbxAutenticar.TabIndex = 10;
            this.gbxAutenticar.TabStop = false;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Location = new System.Drawing.Point(15, 71);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(37, 13);
            this.lblClave.TabIndex = 0;
            this.lblClave.Text = "Clave:";
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Location = new System.Drawing.Point(6, 24);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(46, 13);
            this.lblCuenta.TabIndex = 5;
            this.lblCuenta.Text = "Usuario:";
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(68, 64);
            this.txtClave.MaxLength = 8;
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(100, 20);
            this.txtClave.TabIndex = 2;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // txtUserCode
            // 
            this.txtUserCode.Location = new System.Drawing.Point(68, 19);
            this.txtUserCode.MaxLength = 16;
            this.txtUserCode.Name = "txtUserCode";
            this.txtUserCode.Size = new System.Drawing.Size(100, 20);
            this.txtUserCode.TabIndex = 1;
            this.txtUserCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserCode_KeyPress);
            // 
            // erpUser
            // 
            this.erpUser.ContainerControl = this;
            // 
            // erpClave
            // 
            this.erpClave.ContainerControl = this;
            // 
            // Cierre
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.pbxLogin);
            this.Controls.Add(this.gbxAutenticar);
            this.Controls.Add(this.dgwCierre);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Cierre";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Cierre";
            this.Load += new System.EventHandler(this.Cierre_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgwCierre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogin)).EndInit();
            this.gbxAutenticar.ResumeLayout(false);
            this.gbxAutenticar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erpUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwCierre;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.ErrorProvider erpMontos;
        private System.Windows.Forms.PictureBox pbxLogin;
        private System.Windows.Forms.GroupBox gbxAutenticar;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUserCode;
        private System.Windows.Forms.ErrorProvider erpUser;
        private System.Windows.Forms.ErrorProvider erpClave;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCodMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDscMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxEfec;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxTran;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxCheq;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvtbxTar;
    }
}