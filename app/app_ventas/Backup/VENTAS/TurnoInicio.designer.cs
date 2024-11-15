using System.Collections.Generic;
using LAP.TUUA.UTIL;
using System.Collections;
using System.IO;

namespace LAP.TUUA.VENTAS
{
    partial class TurnoInicio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TurnoInicio));
            this.dgwIniMontos = new System.Windows.Forms.DataGridView();
            this.sCodMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sDscMonedaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMontos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SDscSimbolo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.monedaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.erpMontos = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgwIniMontos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgwIniMontos
            // 
            this.dgwIniMontos.AllowUserToAddRows = false;
            this.dgwIniMontos.AllowUserToDeleteRows = false;
            this.dgwIniMontos.AllowUserToResizeColumns = false;
            this.dgwIniMontos.AllowUserToResizeRows = false;
            this.dgwIniMontos.AutoGenerateColumns = false;
            this.dgwIniMontos.BackgroundColor = System.Drawing.Color.White;
            this.dgwIniMontos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwIniMontos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sCodMonedaDataGridViewTextBoxColumn,
            this.sDscMonedaDataGridViewTextBoxColumn,
            this.txtMontos,
            this.SDscSimbolo});
            this.dgwIniMontos.DataSource = this.monedaBindingSource;
            this.dgwIniMontos.Location = new System.Drawing.Point(41, 26);
            this.dgwIniMontos.MultiSelect = false;
            this.dgwIniMontos.Name = "dgwIniMontos";
            this.dgwIniMontos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgwIniMontos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgwIniMontos.Size = new System.Drawing.Size(312, 103);
            this.dgwIniMontos.TabIndex = 6;
            this.dgwIniMontos.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwIniMontos_EditingControlShowing);
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
            // txtMontos
            // 
            this.txtMontos.HeaderText = "Montos";
            this.txtMontos.MaxInputLength = 8;
            this.txtMontos.Name = "txtMontos";
            // 
            // SDscSimbolo
            // 
            this.SDscSimbolo.DataPropertyName = "SDscSimbolo";
            this.SDscSimbolo.HeaderText = "Símbolo";
            this.SDscSimbolo.MaxInputLength = 4;
            this.SDscSimbolo.Name = "SDscSimbolo";
            this.SDscSimbolo.ReadOnly = true;
            this.SDscSimbolo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.SDscSimbolo.Width = 50;
            // 
            // monedaBindingSource
            // 
            this.monedaBindingSource.DataSource = typeof(LAP.TUUA.ENTIDADES.Moneda);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Image = ((System.Drawing.Image)(resources.GetObject("btnAceptar.Image")));
            this.btnAceptar.Location = new System.Drawing.Point(66, 154);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 25);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(239, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // erpMontos
            // 
            this.erpMontos.ContainerControl = this;
            // 
            // TurnoInicio
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(396, 189);
            this.Controls.Add(this.dgwIniMontos);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "TurnoInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inicio de Turno";
            this.Load += new System.EventHandler(this.GUITurno_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TurnoInicio_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgwIniMontos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monedaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.erpMontos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgwIniMontos;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider erpMontos;
        private System.Windows.Forms.BindingSource monedaBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn sCodMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sDscMonedaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtMontos;
        private System.Windows.Forms.DataGridViewTextBoxColumn SDscSimbolo;
    }
}

