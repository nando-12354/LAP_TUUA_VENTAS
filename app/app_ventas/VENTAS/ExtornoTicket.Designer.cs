
using System.Windows.Forms;

namespace LAP.TUUA.VENTAS
{
    partial class ExtornoTicket
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCerrado = new System.Windows.Forms.RadioButton();
            this.rbActivo = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaIni = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEmpresaRecaudadora = new System.Windows.Forms.Label();
            this.txtTurno = new System.Windows.Forms.TextBox();
            //this.txtCodigoCajero = new System.Windows.Forms.TextBox(); //FL.
            this.cbxCajero = new System.Windows.Forms.ComboBox(); //FL.
            this.cbxEmpresaRecaudadora = new System.Windows.Forms.ComboBox(); //FL.
            this.dgwExtorno = new System.Windows.Forms.DataGridView();
            this.Turno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn(); //FL.
            this.lblNumeroRegistros = new System.Windows.Forms.Label();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.pnlSeleccion = new System.Windows.Forms.Panel();
            this.lblNumeroRegistrosTicketsSeleccionados = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnExtornar = new System.Windows.Forms.Button();
            this.lblNumeroRegistrosTickets = new System.Windows.Forms.Label();
            this.dgwTickets = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dsc_Tip_Vuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dsc_Compania = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dsc_Num_Vuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cod_Numero_Ticket = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fch_Vuelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtMotivoExtorno = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBuscarTickets = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRangoTicketFin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRangoTicketInicio = new System.Windows.Forms.TextBox();
            this.rbRangoTickets = new System.Windows.Forms.RadioButton();
            this.rbNumeroTicket = new System.Windows.Forms.RadioButton();
            this.txtNumeroTicket = new System.Windows.Forms.TextBox();
            this.cbxModalidadPago = new System.Windows.Forms.ComboBox(); //FL.
            this.lblModalidadPago = new System.Windows.Forms.Label(); //FL.
            this.cbxTipoVenta = new System.Windows.Forms.ComboBox(); //FL.
            this.lblTipoVenta = new System.Windows.Forms.Label(); //FL.
            this.cbxTipoTuua = new System.Windows.Forms.ComboBox(); //FL.
            this.lblTipoTuua = new System.Windows.Forms.Label(); //FL.
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExtorno)).BeginInit();
            this.pnlBusqueda.SuspendLayout();
            this.pnlSeleccion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTickets)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCerrado);
            this.groupBox1.Controls.Add(this.rbActivo);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(156, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Estado Turno";
            // 
            // rbCerrado
            // 
            this.rbCerrado.AutoSize = true;
            this.rbCerrado.Location = new System.Drawing.Point(85, 21);
            this.rbCerrado.Name = "rbCerrado";
            this.rbCerrado.Size = new System.Drawing.Size(62, 17);
            this.rbCerrado.TabIndex = 1;
            this.rbCerrado.TabStop = true;
            this.rbCerrado.Text = "Cerrado";
            this.rbCerrado.UseVisualStyleBackColor = true;
            // 
            // rbActivo
            // 
            this.rbActivo.AutoSize = true;
            this.rbActivo.Location = new System.Drawing.Point(19, 21);
            this.rbActivo.Name = "rbActivo";
            this.rbActivo.Size = new System.Drawing.Size(55, 17);
            this.rbActivo.TabIndex = 0;
            this.rbActivo.TabStop = true;
            this.rbActivo.Text = "Activo";
            this.rbActivo.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpFechaFin);
            this.groupBox2.Controls.Add(this.dtpFechaIni);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(180, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 50);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rango Fecha";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(242, 18);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaFin.TabIndex = 4;
            // 
            // dtpFechaIni
            // 
            this.dtpFechaIni.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaIni.Location = new System.Drawing.Point(80, 18);
            this.dtpFechaIni.Name = "dtpFechaIni";
            this.dtpFechaIni.Size = new System.Drawing.Size(99, 20);
            this.dtpFechaIni.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Fecha Fin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha Ini";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(466, 78);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(97, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Turno";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Cajero";
            // 
            // lblEmpresaRecaudadora
            // 
            this.lblEmpresaRecaudadora.AutoSize = true;
            this.lblEmpresaRecaudadora.Location = new System.Drawing.Point(160, 63);
            this.lblEmpresaRecaudadora.Name = "lblEmpresaRecaudadora";
            this.lblEmpresaRecaudadora.Size = new System.Drawing.Size(73, 13);
            this.lblEmpresaRecaudadora.TabIndex = 4;
            this.lblEmpresaRecaudadora.Text = "Empresa";
            // 
            // txtTurno
            // 
            this.txtTurno.Location = new System.Drawing.Point(7, 78);
            this.txtTurno.Name = "txtTurno";
            this.txtTurno.Size = new System.Drawing.Size(138, 20);
            this.txtTurno.TabIndex = 5;
            // 
            // txtCodigoCajero //FL.
            // 
            //this.txtCodigoCajero.Location = new System.Drawing.Point(131, 74);
            //this.txtCodigoCajero.Name = "txtCodigoCajero";
            //this.txtCodigoCajero.Size = new System.Drawing.Size(138, 20);
            //this.txtCodigoCajero.TabIndex = 6;
            // 
            // cbxCajero //FL.
            // 
            this.cbxCajero.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxCajero.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxCajero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCajero.FormattingEnabled = true;
            this.cbxCajero.Location = new System.Drawing.Point(313, 78);
            this.cbxCajero.Name = "txtCodigoCajero";
            this.cbxCajero.Size = new System.Drawing.Size(138, 20);
            this.cbxCajero.TabIndex = 6;
            // 
            // cbxEmpresaRecaudadora //FL.
            // 
            this.cbxEmpresaRecaudadora.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxEmpresaRecaudadora.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxEmpresaRecaudadora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEmpresaRecaudadora.FormattingEnabled = true;
            this.cbxEmpresaRecaudadora.Location = new System.Drawing.Point(160, 78);
            this.cbxEmpresaRecaudadora.Name = "cbxEmpresaRecaudadora";
            this.cbxEmpresaRecaudadora.Size = new System.Drawing.Size(138, 20);
            this.cbxEmpresaRecaudadora.TabIndex = 6;
            //this.cbxEmpresaRecaudadora.SelectedIndexChanged += new System.EventHandler(this.cbxEmpresaRecaudadora_SelectedIndexChanged);
            // 
            // dgwExtorno
            // 
            this.dgwExtorno.AllowUserToAddRows = false;
            this.dgwExtorno.AllowUserToDeleteRows = false;
            this.dgwExtorno.AllowUserToResizeRows = false;
            this.dgwExtorno.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExtorno.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Turno,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5}); //FL.
            this.dgwExtorno.Location = new System.Drawing.Point(3, 137);
            this.dgwExtorno.Name = "dgwExtorno";
            this.dgwExtorno.Size = new System.Drawing.Size(581, 197);
            this.dgwExtorno.TabIndex = 7;
            this.dgwExtorno.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwExtorno_CellContentClick);
            this.dgwExtorno.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwExtorno_CellMouseEnter);
            // 
            // Turno
            // 
            this.Turno.DataPropertyName = "Cod_Turno";
            this.Turno.HeaderText = "Turno";
            this.Turno.Name = "Turno";
            this.Turno.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Num_Ip_Equipo";
            this.Column1.HeaderText = "Numero IP";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column5.DataPropertyName = "empresa";
            this.Column5.HeaderText = "Empresa recaudadora";
            this.Column5.Name = "Column6";
            this.Column5.ReadOnly = true;
            this.Column5.DisplayIndex = 2;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Dsc_Usuario";
            this.Column2.HeaderText = "Cajero";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Fch_Inicio";
            this.Column3.HeaderText = "Fecha Inicio";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Acciones";
            this.Column4.Image = global::LAP.TUUA.VENTAS.Properties.Resources.lupa;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lblNumeroRegistros
            // 
            this.lblNumeroRegistros.AutoSize = true;
            this.lblNumeroRegistros.Location = new System.Drawing.Point(4, 365);
            this.lblNumeroRegistros.Name = "lblNumeroRegistros";
            this.lblNumeroRegistros.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroRegistros.TabIndex = 8;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Acciones";
            this.dataGridViewImageColumn1.Image = global::LAP.TUUA.VENTAS.Properties.Resources.lupa;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.Controls.Add(this.groupBox1);
            this.pnlBusqueda.Controls.Add(this.groupBox2);
            this.pnlBusqueda.Controls.Add(this.dgwExtorno);
            this.pnlBusqueda.Controls.Add(this.btnBuscar);
            //this.pnlBusqueda.Controls.Add(this.txtCodigoCajero); //FL.
            this.pnlBusqueda.Controls.Add(this.cbxCajero); //FL.
            this.pnlBusqueda.Controls.Add(this.cbxEmpresaRecaudadora); //FL.
            this.pnlBusqueda.Controls.Add(this.label3);
            this.pnlBusqueda.Controls.Add(this.txtTurno);
            this.pnlBusqueda.Controls.Add(this.label4);
            this.pnlBusqueda.Controls.Add(this.lblEmpresaRecaudadora); //FL.
            this.pnlBusqueda.Location = new System.Drawing.Point(7, 12);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(591, 549);
            this.pnlBusqueda.TabIndex = 9;
            // 
            // pnlSeleccion
            // 
            this.pnlSeleccion.Controls.Add(this.lblNumeroRegistrosTicketsSeleccionados);
            this.pnlSeleccion.Controls.Add(this.btnCancelar);
            this.pnlSeleccion.Controls.Add(this.btnExtornar);
            this.pnlSeleccion.Controls.Add(this.lblNumeroRegistrosTickets);
            this.pnlSeleccion.Controls.Add(this.dgwTickets);
            this.pnlSeleccion.Controls.Add(this.txtMotivoExtorno);
            this.pnlSeleccion.Controls.Add(this.label7);
            this.pnlSeleccion.Controls.Add(this.groupBox3);
            this.pnlSeleccion.Location = new System.Drawing.Point(7, 12);
            this.pnlSeleccion.Name = "pnlSeleccion";
            this.pnlSeleccion.Size = new System.Drawing.Size(591, 549);
            this.pnlSeleccion.TabIndex = 8;
            this.pnlSeleccion.Visible = false;
            // 
            // lblNumeroRegistrosTicketsSeleccionados
            // 
            this.lblNumeroRegistrosTicketsSeleccionados.AutoSize = true;
            this.lblNumeroRegistrosTicketsSeleccionados.Location = new System.Drawing.Point(3, 485);
            this.lblNumeroRegistrosTicketsSeleccionados.Name = "lblNumeroRegistrosTicketsSeleccionados";
            this.lblNumeroRegistrosTicketsSeleccionados.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroRegistrosTicketsSeleccionados.TabIndex = 7;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(337, 485);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 31);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnExtornar
            // 
            this.btnExtornar.Location = new System.Drawing.Point(461, 485);
            this.btnExtornar.Name = "btnExtornar";
            this.btnExtornar.Size = new System.Drawing.Size(118, 31);
            this.btnExtornar.TabIndex = 5;
            this.btnExtornar.Text = "Extornar";
            this.btnExtornar.UseVisualStyleBackColor = true;
            this.btnExtornar.Click += new System.EventHandler(this.btnExtornar_Click);
            // 
            // lblNumeroRegistrosTickets
            // 
            this.lblNumeroRegistrosTickets.AutoSize = true;
            this.lblNumeroRegistrosTickets.Location = new System.Drawing.Point(3, 459);
            this.lblNumeroRegistrosTickets.Name = "lblNumeroRegistrosTickets";
            this.lblNumeroRegistrosTickets.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroRegistrosTickets.TabIndex = 4;
            // 
            // dgwTickets
            // 
            this.dgwTickets.AllowUserToAddRows = false;
            this.dgwTickets.AllowUserToDeleteRows = false;
            this.dgwTickets.AllowUserToResizeRows = false;
            this.dgwTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwTickets.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.rownum,
            this.Dsc_Tip_Vuelo,
            this.Dsc_Compania,
            this.Dsc_Num_Vuelo,
            this.Cod_Numero_Ticket,
            this.Fch_Vuelo,
            this.Estado,
            this.Column7});
            this.dgwTickets.Location = new System.Drawing.Point(4, 193);
            this.dgwTickets.MultiSelect = false;
            this.dgwTickets.Name = "dgwTickets";
            this.dgwTickets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgwTickets.ShowCellErrors = false;
            this.dgwTickets.ShowCellToolTips = false;
            this.dgwTickets.ShowEditingIcon = false;
            this.dgwTickets.ShowRowErrors = false;
            this.dgwTickets.Size = new System.Drawing.Size(577, 263);
            this.dgwTickets.TabIndex = 3;
            this.dgwTickets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwTickets_CellContentClick);
            this.dgwTickets.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwTickets_CellMouseEnter);
            this.dgwTickets.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting); //FL.
            // 
            // Column8
            // 
            this.Column8.FalseValue = "true";
            this.Column8.HeaderText = "";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.TrueValue = "false";
            this.Column8.Width = 40;
            // 
            // rownum
            // 
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "Nro";
            this.rownum.Name = "rownum";
            this.rownum.Width = 40;
            // 
            // Dsc_Tip_Vuelo
            // 
            this.Dsc_Tip_Vuelo.DataPropertyName = "Dsc_Tip_Vuelo";
            this.Dsc_Tip_Vuelo.HeaderText = "Tipo Ticket";
            this.Dsc_Tip_Vuelo.Name = "Dsc_Tip_Vuelo";
            // 
            // Dsc_Compania
            // 
            this.Dsc_Compania.DataPropertyName = "Dsc_Compania";
            this.Dsc_Compania.HeaderText = "Compañia";
            this.Dsc_Compania.Name = "Dsc_Compania";
            // 
            // Dsc_Num_Vuelo
            // 
            this.Dsc_Num_Vuelo.DataPropertyName = "Dsc_Num_Vuelo";
            this.Dsc_Num_Vuelo.HeaderText = "Numero de Vuelo";
            this.Dsc_Num_Vuelo.Name = "Dsc_Num_Vuelo";
            this.Dsc_Num_Vuelo.Width = 120;
            // 
            // Cod_Numero_Ticket
            // 
            this.Cod_Numero_Ticket.DataPropertyName = "Cod_Numero_Ticket";
            this.Cod_Numero_Ticket.HeaderText = "Numero de Ticket";
            this.Cod_Numero_Ticket.Name = "Cod_Numero_Ticket";
            this.Cod_Numero_Ticket.Width = 120;
            // 
            // Fch_Vuelo
            // 
            this.Fch_Vuelo.DataPropertyName = "Fch_Vuelo";
            this.Fch_Vuelo.HeaderText = "Fecha Proceso";
            this.Fch_Vuelo.Name = "Fch_Vuelo";
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Acciones";
            this.Column7.Image = global::LAP.TUUA.VENTAS.Properties.Resources.lupa;
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.Width = 60;
            // 
            // txtMotivoExtorno
            // 
            this.txtMotivoExtorno.Location = new System.Drawing.Point(102, 157);
            this.txtMotivoExtorno.Name = "txtMotivoExtorno";
            this.txtMotivoExtorno.Size = new System.Drawing.Size(479, 20);
            this.txtMotivoExtorno.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Motivo del Extorno";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBuscarTickets);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtRangoTicketFin);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtRangoTicketInicio);
            this.groupBox3.Controls.Add(this.rbRangoTickets);
            this.groupBox3.Controls.Add(this.rbNumeroTicket);
            this.groupBox3.Controls.Add(this.txtNumeroTicket);
            this.groupBox3.Controls.Add(this.cbxModalidadPago); //FL.
            this.groupBox3.Controls.Add(this.lblModalidadPago); //FL.
            this.groupBox3.Controls.Add(this.cbxTipoVenta); //FL.
            this.groupBox3.Controls.Add(this.lblTipoVenta); //FL.
            this.groupBox3.Controls.Add(this.cbxTipoTuua); //FL.
            this.groupBox3.Controls.Add(this.lblTipoTuua); //FL.
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(585, 141);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnBuscarTickets
            // 
            this.btnBuscarTickets.Location = new System.Drawing.Point(461, 15);
            this.btnBuscarTickets.Name = "btnBuscarTickets";
            this.btnBuscarTickets.Size = new System.Drawing.Size(118, 31);
            this.btnBuscarTickets.TabIndex = 4;
            this.btnBuscarTickets.Text = "Buscar";
            this.btnBuscarTickets.UseVisualStyleBackColor = true;
            this.btnBuscarTickets.Click += new System.EventHandler(this.btnBuscarTickets_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(374, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Al";
            // 
            // txtRangoTicketFin
            // 
            this.txtRangoTicketFin.Enabled = false;
            this.txtRangoTicketFin.Location = new System.Drawing.Point(392, 55);
            this.txtRangoTicketFin.Name = "txtRangoTicketFin";
            this.txtRangoTicketFin.Size = new System.Drawing.Size(187, 20);
            this.txtRangoTicketFin.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Del";
            // 
            // txtRangoTicketInicio
            // 
            this.txtRangoTicketInicio.Enabled = false;
            this.txtRangoTicketInicio.Location = new System.Drawing.Point(185, 55);
            this.txtRangoTicketInicio.Name = "txtRangoTicketInicio";
            this.txtRangoTicketInicio.Size = new System.Drawing.Size(187, 20);
            this.txtRangoTicketInicio.TabIndex = 3;
            // 
            // rbRangoTickets
            // 
            this.rbRangoTickets.AutoSize = true;
            this.rbRangoTickets.Location = new System.Drawing.Point(19, 56);
            this.rbRangoTickets.Name = "rbRangoTickets";
            this.rbRangoTickets.Size = new System.Drawing.Size(129, 17);
            this.rbRangoTickets.TabIndex = 1;
            this.rbRangoTickets.Text = "Por Rango de Tickets";
            this.rbRangoTickets.UseVisualStyleBackColor = true;
            this.rbRangoTickets.CheckedChanged += new System.EventHandler(this.tipoFiltro_CheckedChanged);
            // 
            // rbNumeroTicket
            // 
            this.rbNumeroTicket.AutoSize = true;
            this.rbNumeroTicket.Checked = true;
            this.rbNumeroTicket.Location = new System.Drawing.Point(19, 22);
            this.rbNumeroTicket.Name = "rbNumeroTicket";
            this.rbNumeroTicket.Size = new System.Drawing.Size(129, 17);
            this.rbNumeroTicket.TabIndex = 0;
            this.rbNumeroTicket.TabStop = true;
            this.rbNumeroTicket.Text = "Por Numero de Ticket";
            this.rbNumeroTicket.UseVisualStyleBackColor = true;
            this.rbNumeroTicket.CheckedChanged += new System.EventHandler(this.tipoFiltro_CheckedChanged);
            // 
            // txtNumeroTicket
            // 
            this.txtNumeroTicket.Location = new System.Drawing.Point(185, 21);
            this.txtNumeroTicket.Name = "txtNumeroTicket";
            this.txtNumeroTicket.Size = new System.Drawing.Size(187, 20);
            this.txtNumeroTicket.TabIndex = 1;
            // 
            // lblModalidadPago
            // 
            this.lblModalidadPago.AutoSize = true;
            this.lblModalidadPago.Location = new System.Drawing.Point(19, 95);
            this.lblModalidadPago.Name = "lblModalidadPago";
            this.lblModalidadPago.Size = new System.Drawing.Size(23, 13);
            this.lblModalidadPago.TabIndex = 0;
            this.lblModalidadPago.Text = "Modalidad pago";
            // 
            // cbxModalidadPago
            // 
            this.cbxModalidadPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxModalidadPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxModalidadPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxModalidadPago.FormattingEnabled = true;
            this.cbxModalidadPago.AutoSize = true;
            this.cbxModalidadPago.Location = new System.Drawing.Point(19, 110);
            this.cbxModalidadPago.Name = "cbxModalidadPago";
            this.cbxModalidadPago.Size = new System.Drawing.Size(129, 17);
            this.cbxModalidadPago.TabIndex = 0;
            this.cbxModalidadPago.TabStop = true;
            //this.cbxModalidadPago.CheckedChanged += new System.EventHandler(this.tipoFiltro_CheckedChanged);
            // 
            // lblTipoVenta
            // 
            this.lblTipoVenta.AutoSize = true;
            this.lblTipoVenta.Location = new System.Drawing.Point(170, 95);
            this.lblTipoVenta.Name = "lblTipoVenta";
            this.lblTipoVenta.Size = new System.Drawing.Size(23, 13);
            this.lblTipoVenta.TabIndex = 0;
            this.lblTipoVenta.Text = "Tipo venta";
            // 
            // cbxTipoVenta
            // 
            this.cbxTipoVenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoVenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoVenta.FormattingEnabled = true;
            this.cbxTipoVenta.AutoSize = true;
            this.cbxTipoVenta.Location = new System.Drawing.Point(170, 110);
            this.cbxTipoVenta.Name = "cbxTipoVenta";
            this.cbxTipoVenta.Size = new System.Drawing.Size(129, 17);
            this.cbxTipoVenta.TabIndex = 0;
            this.cbxTipoVenta.TabStop = true;
            //this.cbxTipoVenta.CheckedChanged += new System.EventHandler(this.tipoFiltro_CheckedChanged);
            // 
            // lblTipoTuua
            // 
            this.lblTipoTuua.AutoSize = true;
            this.lblTipoTuua.Location = new System.Drawing.Point(321, 95);
            this.lblTipoTuua.Name = "lblTipoTuua";
            this.lblTipoTuua.Size = new System.Drawing.Size(23, 13);
            this.lblTipoTuua.TabIndex = 0;
            this.lblTipoTuua.Text = "Tipo TUUA";
            // 
            // cbxTipoTuua
            // 
            this.cbxTipoTuua.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxTipoTuua.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxTipoTuua.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoTuua.FormattingEnabled = true;
            this.cbxTipoTuua.AutoSize = true;
            this.cbxTipoTuua.Location = new System.Drawing.Point(321, 110);
            this.cbxTipoTuua.Name = "cbxTipoTuua";
            this.cbxTipoTuua.Size = new System.Drawing.Size(129, 17);
            this.cbxTipoTuua.TabIndex = 0;
            this.cbxTipoTuua.TabStop = true;
            //this.cbxTipoTuua.SelectedIndexChanged += new System.EventHandler(this.cbxTipoTuua_SelectedIndexChanged);
            // 
            // ExtornoTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 633);
            this.Controls.Add(this.pnlSeleccion);
            this.Controls.Add(this.pnlBusqueda);
            this.Controls.Add(this.lblNumeroRegistros);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExtornoTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExtornoTicket";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExtorno)).EndInit();
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.pnlSeleccion.ResumeLayout(false);
            this.pnlSeleccion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwTickets)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.RadioButton rbCerrado;
        private System.Windows.Forms.RadioButton rbActivo;
        private System.Windows.Forms.DateTimePicker dtpFechaIni;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEmpresaRecaudadora; //FL.
        private System.Windows.Forms.TextBox txtTurno;
        //private System.Windows.Forms.TextBox txtCodigoCajero; //FL.
        private System.Windows.Forms.ComboBox cbxCajero; //FL.
        private System.Windows.Forms.ComboBox cbxEmpresaRecaudadora; //FL.
        private System.Windows.Forms.DataGridView dgwExtorno;
        private System.Windows.Forms.Label lblNumeroRegistros;
        private System.Windows.Forms.DataGridViewTextBoxColumn Turno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5; //FL.
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlSeleccion;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnExtornar;
        private System.Windows.Forms.Label lblNumeroRegistrosTickets;
        private System.Windows.Forms.TextBox txtMotivoExtorno;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBuscarTickets;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRangoTicketFin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRangoTicketInicio;
        private System.Windows.Forms.RadioButton rbRangoTickets;
        private System.Windows.Forms.RadioButton rbNumeroTicket;
        private System.Windows.Forms.TextBox txtNumeroTicket;
        private System.Windows.Forms.ComboBox cbxModalidadPago; //FL.
        private System.Windows.Forms.Label lblModalidadPago; //FL.
        private System.Windows.Forms.ComboBox cbxTipoVenta; //FL.
        private System.Windows.Forms.Label lblTipoVenta; //FL.
        private System.Windows.Forms.ComboBox cbxTipoTuua; //FL.
        private System.Windows.Forms.Label lblTipoTuua; //FL.
        private System.Windows.Forms.DataGridView dgwTickets;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dsc_Tip_Vuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dsc_Compania;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dsc_Num_Vuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cod_Numero_Ticket;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fch_Vuelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewImageColumn Column7;
        private System.Windows.Forms.Label lblNumeroRegistrosTicketsSeleccionados;
    }
}