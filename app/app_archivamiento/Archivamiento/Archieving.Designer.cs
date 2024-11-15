namespace LAP.TUUA.ARCHIVAMIENTO
{
    partial class Archieving
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Archieving));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.lblFechaFinal = new System.Windows.Forms.Label();
            this.lblFechaFinalTit = new System.Windows.Forms.Label();
            this.btnReprocesar = new System.Windows.Forms.Button();
            this.lblPendientedeProcesar = new System.Windows.Forms.Label();
            this.lblPendientedeProcesarTit = new System.Windows.Forms.Label();
            this.btnArchivar = new System.Windows.Forms.Button();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.lblFechaDisponible = new System.Windows.Forms.Label();
            this.lblPeriodoTit = new System.Windows.Forms.Label();
            this.lblFechaDisponibleTit = new System.Windows.Forms.Label();
            this.txtDiaInicial = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gvwHistorico = new System.Windows.Forms.DataGridView();
            this.Cod_Archivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dsc_Periodo = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Fch_Ini = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fch_Fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaProceso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EstadoDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.lnkAyuda = new System.Windows.Forms.LinkLabel();
            this.lnkSalir = new System.Windows.Forms.LinkLabel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvwHistorico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.calendar);
            this.groupBox1.Controls.Add(this.lblFechaFinal);
            this.groupBox1.Controls.Add(this.lblFechaFinalTit);
            this.groupBox1.Controls.Add(this.btnReprocesar);
            this.groupBox1.Controls.Add(this.lblPendientedeProcesar);
            this.groupBox1.Controls.Add(this.lblPendientedeProcesarTit);
            this.groupBox1.Controls.Add(this.btnArchivar);
            this.groupBox1.Controls.Add(this.cmbPeriodo);
            this.groupBox1.Controls.Add(this.lblFechaDisponible);
            this.groupBox1.Controls.Add(this.lblPeriodoTit);
            this.groupBox1.Controls.Add(this.lblFechaDisponibleTit);
            this.groupBox1.Controls.Add(this.txtDiaInicial);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(19, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(821, 239);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PROCESAR ARCHIVAMIENTO";
            // 
            // calendar
            // 
            this.calendar.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calendar.Location = new System.Drawing.Point(645, 44);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.TabIndex = 5;
            this.calendar.Visible = false;
            this.calendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendar_DateSelected);
            this.calendar.Leave += new System.EventHandler(this.calendar_Leave);
            // 
            // lblFechaFinal
            // 
            this.lblFechaFinal.AutoSize = true;
            this.lblFechaFinal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblFechaFinal.ForeColor = System.Drawing.Color.Blue;
            this.lblFechaFinal.Location = new System.Drawing.Point(208, 90);
            this.lblFechaFinal.Name = "lblFechaFinal";
            this.lblFechaFinal.Size = new System.Drawing.Size(72, 14);
            this.lblFechaFinal.TabIndex = 10;
            this.lblFechaFinal.Text = "Fecha Final";
            // 
            // lblFechaFinalTit
            // 
            this.lblFechaFinalTit.AutoSize = true;
            this.lblFechaFinalTit.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFechaFinalTit.Location = new System.Drawing.Point(42, 90);
            this.lblFechaFinalTit.Name = "lblFechaFinalTit";
            this.lblFechaFinalTit.Size = new System.Drawing.Size(70, 14);
            this.lblFechaFinalTit.TabIndex = 9;
            this.lblFechaFinalTit.Text = "Fecha Final:";
            // 
            // btnReprocesar
            // 
            this.btnReprocesar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnReprocesar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprocesar.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnReprocesar.Location = new System.Drawing.Point(439, 160);
            this.btnReprocesar.Name = "btnReprocesar";
            this.btnReprocesar.Size = new System.Drawing.Size(90, 25);
            this.btnReprocesar.TabIndex = 7;
            this.btnReprocesar.Text = "Procesar";
            this.btnReprocesar.UseVisualStyleBackColor = false;
            this.btnReprocesar.Click += new System.EventHandler(this.btnReprocesar_Click);
            // 
            // lblPendientedeProcesar
            // 
            this.lblPendientedeProcesar.AutoSize = true;
            this.lblPendientedeProcesar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPendientedeProcesar.ForeColor = System.Drawing.Color.Blue;
            this.lblPendientedeProcesar.Location = new System.Drawing.Point(208, 164);
            this.lblPendientedeProcesar.Name = "lblPendientedeProcesar";
            this.lblPendientedeProcesar.Size = new System.Drawing.Size(136, 14);
            this.lblPendientedeProcesar.TabIndex = 6;
            this.lblPendientedeProcesar.Text = "Pendiente a Procesar";
            // 
            // lblPendientedeProcesarTit
            // 
            this.lblPendientedeProcesarTit.AutoSize = true;
            this.lblPendientedeProcesarTit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblPendientedeProcesarTit.Location = new System.Drawing.Point(42, 164);
            this.lblPendientedeProcesarTit.Name = "lblPendientedeProcesarTit";
            this.lblPendientedeProcesarTit.Size = new System.Drawing.Size(148, 14);
            this.lblPendientedeProcesarTit.TabIndex = 5;
            this.lblPendientedeProcesarTit.Text = "Pendiente de Procesar:";
            // 
            // btnArchivar
            // 
            this.btnArchivar.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnArchivar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnArchivar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArchivar.Location = new System.Drawing.Point(437, 79);
            this.btnArchivar.Name = "btnArchivar";
            this.btnArchivar.Size = new System.Drawing.Size(90, 25);
            this.btnArchivar.TabIndex = 4;
            this.btnArchivar.Text = "Archivar";
            this.btnArchivar.UseVisualStyleBackColor = false;
            this.btnArchivar.Click += new System.EventHandler(this.btnArchivar_Click);
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Items.AddRange(new object[] {
            "- Seleccionar -",
            "SEMESTRE",
            "CUATRIMESTRE",
            "TRIMESTRE",
            "BIMESTRE"});
            this.cmbPeriodo.Location = new System.Drawing.Point(208, 126);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(164, 24);
            this.cmbPeriodo.TabIndex = 3;
            // 
            // lblFechaDisponible
            // 
            this.lblFechaDisponible.AutoSize = true;
            this.lblFechaDisponible.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDisponible.ForeColor = System.Drawing.Color.Blue;
            this.lblFechaDisponible.Location = new System.Drawing.Point(208, 45);
            this.lblFechaDisponible.Name = "lblFechaDisponible";
            this.lblFechaDisponible.Size = new System.Drawing.Size(108, 14);
            this.lblFechaDisponible.TabIndex = 2;
            this.lblFechaDisponible.Text = "Fecha Disponible";
            // 
            // lblPeriodoTit
            // 
            this.lblPeriodoTit.AutoSize = true;
            this.lblPeriodoTit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriodoTit.Location = new System.Drawing.Point(42, 131);
            this.lblPeriodoTit.Name = "lblPeriodoTit";
            this.lblPeriodoTit.Size = new System.Drawing.Size(109, 14);
            this.lblPeriodoTit.TabIndex = 1;
            this.lblPeriodoTit.Text = "Periodo a Archivar:";
            // 
            // lblFechaDisponibleTit
            // 
            this.lblFechaDisponibleTit.AutoSize = true;
            this.lblFechaDisponibleTit.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDisponibleTit.Location = new System.Drawing.Point(42, 44);
            this.lblFechaDisponibleTit.Name = "lblFechaDisponibleTit";
            this.lblFechaDisponibleTit.Size = new System.Drawing.Size(101, 14);
            this.lblFechaDisponibleTit.TabIndex = 0;
            this.lblFechaDisponibleTit.Text = "Fecha Disponible:";
            // 
            // txtDiaInicial
            // 
            this.txtDiaInicial.BackColor = System.Drawing.SystemColors.Control;
            this.txtDiaInicial.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.txtDiaInicial.ForeColor = System.Drawing.Color.Blue;
            this.txtDiaInicial.Location = new System.Drawing.Point(208, 45);
            this.txtDiaInicial.Name = "txtDiaInicial";
            this.txtDiaInicial.ReadOnly = true;
            this.txtDiaInicial.Size = new System.Drawing.Size(164, 22);
            this.txtDiaInicial.TabIndex = 8;
            this.txtDiaInicial.Visible = false;
            this.txtDiaInicial.Click += new System.EventHandler(this.txtDiaInicial_Click);
            this.txtDiaInicial.Leave += new System.EventHandler(this.txtDiaInicial_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.gvwHistorico);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(19, 319);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(824, 264);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ARCHIVAMIENTO HISTORICO";
            // 
            // gvwHistorico
            // 
            this.gvwHistorico.AllowUserToAddRows = false;
            this.gvwHistorico.AllowUserToDeleteRows = false;
            this.gvwHistorico.AllowUserToResizeRows = false;
            this.gvwHistorico.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvwHistorico.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvwHistorico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwHistorico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cod_Archivo,
            this.Dsc_Periodo,
            this.Fch_Ini,
            this.Fch_Fin,
            this.NombreUsuario,
            this.FechaProceso,
            this.EstadoDescripcion});
            this.gvwHistorico.DataSource = this.bindingSource1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvwHistorico.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvwHistorico.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvwHistorico.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gvwHistorico.Location = new System.Drawing.Point(3, 21);
            this.gvwHistorico.Name = "gvwHistorico";
            this.gvwHistorico.ReadOnly = true;
            this.gvwHistorico.RowHeadersWidth = 4;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvwHistorico.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gvwHistorico.Size = new System.Drawing.Size(818, 240);
            this.gvwHistorico.TabIndex = 0;
            this.gvwHistorico.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvwHistorico_CellContentClick);
            // 
            // Cod_Archivo
            // 
            this.Cod_Archivo.DataPropertyName = "Cod_Archivo";
            this.Cod_Archivo.HeaderText = "";
            this.Cod_Archivo.Name = "Cod_Archivo";
            this.Cod_Archivo.ReadOnly = true;
            this.Cod_Archivo.Visible = false;
            // 
            // Dsc_Periodo
            // 
            this.Dsc_Periodo.DataPropertyName = "Dsc_Periodo";
            this.Dsc_Periodo.HeaderText = "Periodo";
            this.Dsc_Periodo.Name = "Dsc_Periodo";
            this.Dsc_Periodo.ReadOnly = true;
            this.Dsc_Periodo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Fch_Ini
            // 
            this.Fch_Ini.DataPropertyName = "FechaInicial";
            this.Fch_Ini.HeaderText = "Fecha Inicial";
            this.Fch_Ini.Name = "Fch_Ini";
            this.Fch_Ini.ReadOnly = true;
            this.Fch_Ini.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Fch_Fin
            // 
            this.Fch_Fin.DataPropertyName = "FechaFinal";
            this.Fch_Fin.HeaderText = "Fecha Final";
            this.Fch_Fin.Name = "Fch_Fin";
            this.Fch_Fin.ReadOnly = true;
            this.Fch_Fin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NombreUsuario
            // 
            this.NombreUsuario.DataPropertyName = "NombreUsuario";
            this.NombreUsuario.HeaderText = "Usuario Proceso";
            this.NombreUsuario.Name = "NombreUsuario";
            this.NombreUsuario.ReadOnly = true;
            this.NombreUsuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FechaProceso
            // 
            this.FechaProceso.DataPropertyName = "FechaProceso";
            this.FechaProceso.HeaderText = "Fecha Proceso";
            this.FechaProceso.Name = "FechaProceso";
            this.FechaProceso.ReadOnly = true;
            this.FechaProceso.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FechaProceso.Width = 150;
            // 
            // EstadoDescripcion
            // 
            this.EstadoDescripcion.DataPropertyName = "EstadoDescripcion";
            this.EstadoDescripcion.HeaderText = "Estado";
            this.EstadoDescripcion.Name = "EstadoDescripcion";
            this.EstadoDescripcion.ReadOnly = true;
            this.EstadoDescripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EstadoDescripcion.Width = 260;
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblNombreUsuario.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreUsuario.ForeColor = System.Drawing.Color.Blue;
            this.lblNombreUsuario.Location = new System.Drawing.Point(82, 18);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(114, 17);
            this.lblNombreUsuario.TabIndex = 2;
            this.lblNombreUsuario.Text = "lblNombreUsuario";
            // 
            // lnkAyuda
            // 
            this.lnkAyuda.AutoSize = true;
            this.lnkAyuda.BackColor = System.Drawing.Color.Transparent;
            this.lnkAyuda.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAyuda.Location = new System.Drawing.Point(684, 18);
            this.lnkAyuda.Name = "lnkAyuda";
            this.lnkAyuda.Size = new System.Drawing.Size(47, 17);
            this.lnkAyuda.TabIndex = 3;
            this.lnkAyuda.TabStop = true;
            this.lnkAyuda.Text = "Ayuda";
            this.lnkAyuda.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAyuda_LinkClicked);
            // 
            // lnkSalir
            // 
            this.lnkSalir.AutoSize = true;
            this.lnkSalir.BackColor = System.Drawing.Color.Transparent;
            this.lnkSalir.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkSalir.Location = new System.Drawing.Point(768, 18);
            this.lnkSalir.Name = "lnkSalir";
            this.lnkSalir.Size = new System.Drawing.Size(32, 17);
            this.lnkSalir.TabIndex = 4;
            this.lnkSalir.TabStop = true;
            this.lnkSalir.Text = "Salir";
            this.lnkSalir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSalir_LinkClicked);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblUsuario.ForeColor = System.Drawing.Color.Blue;
            this.lblUsuario.Location = new System.Drawing.Point(22, 18);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(58, 17);
            this.lblUsuario.TabIndex = 5;
            this.lblUsuario.Text = "Usuario:";
            // 
            // Archieving
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(858, 623);
            this.ControlBox = false;
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lnkSalir);
            this.Controls.Add(this.lnkAyuda);
            this.Controls.Add(this.lblNombreUsuario);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Archieving";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ARCHIVAMIENTO";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Archieving_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvwHistorico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblFechaDisponible;
        private System.Windows.Forms.Label lblPeriodoTit;
        private System.Windows.Forms.Label lblFechaDisponibleTit;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.Button btnArchivar;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.LinkLabel lnkAyuda;
        private System.Windows.Forms.LinkLabel lnkSalir;
        private System.Windows.Forms.DataGridView gvwHistorico;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cod_Archivo;
        private System.Windows.Forms.DataGridViewLinkColumn Dsc_Periodo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fch_Ini;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fch_Fin;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaProceso;
        private System.Windows.Forms.DataGridViewTextBoxColumn EstadoDescripcion;
        private System.Windows.Forms.Label lblPendientedeProcesarTit;
        private System.Windows.Forms.Label lblPendientedeProcesar;
        private System.Windows.Forms.Button btnReprocesar;
        private System.Windows.Forms.TextBox txtDiaInicial;
        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.Label lblFechaFinal;
        private System.Windows.Forms.Label lblFechaFinalTit;
        private System.Windows.Forms.Label lblUsuario;
    }
}