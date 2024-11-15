// Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and custimized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
namespace LAP.TUUA.VENTAS
{
    partial class Principal
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
            System.Windows.Forms.Timer timer1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tickets Procesados");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Compra/Venta de moneda");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Consultas", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Cuadre de Turno (F7)");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Cierre de Turno (F8)");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Cambio de Contraseña (F9)", 2, 2);
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Turno", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Ingreso Caja (F2)");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Egreso Caja (F3)");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Venta Normal (F4)");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Compra-Venta (F5)");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Venta Masiva (F6)");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Operaciones", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            OutlookStyleControls.OutlookBarButton outlookBarButton1 = new OutlookStyleControls.OutlookBarButton();
            OutlookStyleControls.OutlookBarButton outlookBarButton2 = new OutlookStyleControls.OutlookBarButton();
            OutlookStyleControls.OutlookBarButton outlookBarButton3 = new OutlookStyleControls.OutlookBarButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imglOperacion = new System.Windows.Forms.ImageList(this.components);
            this.imglTurno = new System.Windows.Forms.ImageList(this.components);
            this.imglConsulta = new System.Windows.Forms.ImageList(this.components);
            this.trwConsulta = new System.Windows.Forms.TreeView();
            this.trwTurno = new System.Windows.Forms.TreeView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.trwOperacion = new System.Windows.Forms.TreeView();
            this.lblMenu = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDol = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEur = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblComDol = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVenDol = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblComEur = new System.Windows.Forms.Label();
            this.lblVenEur = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tsslPtoventa = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.sstPrincipal = new System.Windows.Forms.StatusStrip();
            this.olkbVentas = new OutlookStyleControls.OutlookBar();
            timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.sstPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 704);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // imglOperacion
            // 
            this.imglOperacion.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglOperacion.ImageStream")));
            this.imglOperacion.TransparentColor = System.Drawing.Color.Transparent;
            this.imglOperacion.Images.SetKeyName(0, "add.ico");
            this.imglOperacion.Images.SetKeyName(1, "remove.ico");
            this.imglOperacion.Images.SetKeyName(2, "proppage.ico");
            this.imglOperacion.Images.SetKeyName(3, "moneda.jpg");
            this.imglOperacion.Images.SetKeyName(4, "PENS03.ICO");
            // 
            // imglTurno
            // 
            this.imglTurno.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglTurno.ImageStream")));
            this.imglTurno.TransparentColor = System.Drawing.Color.Transparent;
            this.imglTurno.Images.SetKeyName(0, "SECUR03.ICO");
            this.imglTurno.Images.SetKeyName(1, "ImpExcel.ico");
            this.imglTurno.Images.SetKeyName(2, "SECUR08.ICO");
            this.imglTurno.Images.SetKeyName(3, "TIMER01.ICO");
            // 
            // imglConsulta
            // 
            this.imglConsulta.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglConsulta.ImageStream")));
            this.imglConsulta.TransparentColor = System.Drawing.Color.Transparent;
            this.imglConsulta.Images.SetKeyName(0, "MAIL03.ICO");
            this.imglConsulta.Images.SetKeyName(1, "moneda.ico");
            this.imglConsulta.Images.SetKeyName(2, "PROVIDER.ICO");
            // 
            // trwConsulta
            // 
            this.trwConsulta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trwConsulta.ImageIndex = 0;
            this.trwConsulta.ImageList = this.imglConsulta;
            this.trwConsulta.ItemHeight = 20;
            this.trwConsulta.Location = new System.Drawing.Point(3, 3);
            this.trwConsulta.Name = "trwConsulta";
            treeNode1.ImageKey = "MAIL03.ICO";
            treeNode1.Name = "Node1";
            treeNode1.SelectedImageKey = "MAIL03.ICO";
            treeNode1.Text = "Tickets Procesados";
            treeNode2.ImageKey = "moneda.ico";
            treeNode2.Name = "Node2";
            treeNode2.SelectedImageKey = "moneda.ico";
            treeNode2.Text = "Compra/Venta de moneda";
            treeNode3.ImageKey = "PROVIDER.ICO";
            treeNode3.Name = "Node0";
            treeNode3.SelectedImageKey = "PROVIDER.ICO";
            treeNode3.Text = "Consultas";
            this.trwConsulta.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.trwConsulta.SelectedImageIndex = 0;
            this.trwConsulta.Size = new System.Drawing.Size(220, 467);
            this.trwConsulta.TabIndex = 6;
            // 
            // trwTurno
            // 
            this.trwTurno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trwTurno.ImageIndex = 0;
            this.trwTurno.ImageList = this.imglTurno;
            this.trwTurno.Indent = 19;
            this.trwTurno.ItemHeight = 20;
            this.trwTurno.Location = new System.Drawing.Point(3, 3);
            this.trwTurno.Name = "trwTurno";
            treeNode4.ImageKey = "ImpExcel.ico";
            treeNode4.Name = "cndCuadre";
            treeNode4.SelectedImageKey = "ImpExcel.ico";
            treeNode4.Text = "Cuadre de Turno (F7)";
            treeNode5.ImageKey = "SECUR03.ICO";
            treeNode5.Name = "cndCierre";
            treeNode5.Text = "Cierre de Turno (F8)";
            treeNode6.ImageIndex = 2;
            treeNode6.Name = "cndClave";
            treeNode6.SelectedImageIndex = 2;
            treeNode6.Text = "Cambio de Contraseña (F9)";
            treeNode7.ImageKey = "TIMER01.ICO";
            treeNode7.Name = "rndTurno";
            treeNode7.SelectedImageKey = "TIMER01.ICO";
            treeNode7.Text = "Turno";
            this.trwTurno.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.trwTurno.SelectedImageIndex = 0;
            this.trwTurno.Size = new System.Drawing.Size(220, 467);
            this.trwTurno.TabIndex = 6;
            this.trwTurno.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trwTurno_AfterSelect);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(3, 465);
            this.splitter2.MinExtra = 20;
            this.splitter2.MinSize = 32;
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(220, 5);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // trwOperacion
            // 
            this.trwOperacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trwOperacion.ImageIndex = 0;
            this.trwOperacion.ImageList = this.imglOperacion;
            this.trwOperacion.Indent = 19;
            this.trwOperacion.ItemHeight = 20;
            this.trwOperacion.Location = new System.Drawing.Point(3, 3);
            this.trwOperacion.Name = "trwOperacion";
            treeNode8.ImageKey = "add.ico";
            treeNode8.Name = "cndIgreso";
            treeNode8.SelectedImageKey = "add.ico";
            treeNode8.Text = "Ingreso Caja (F2)";
            treeNode9.ImageKey = "remove.ico";
            treeNode9.Name = "cndEgreso";
            treeNode9.SelectedImageKey = "remove.ico";
            treeNode9.Text = "Egreso Caja (F3)";
            treeNode10.ImageKey = "proppage.ico";
            treeNode10.Name = "cndVenta";
            treeNode10.SelectedImageKey = "proppage.ico";
            treeNode10.Text = "Venta Normal (F4)";
            treeNode11.ImageKey = "moneda.jpg";
            treeNode11.Name = "cndCompraVenta";
            treeNode11.SelectedImageKey = "moneda.jpg";
            treeNode11.Text = "Compra-Venta (F5)";
            treeNode12.ImageKey = "proppage.ico";
            treeNode12.Name = "cndMasivo";
            treeNode12.SelectedImageKey = "proppage.ico";
            treeNode12.Text = "Venta Masiva (F6)";
            treeNode13.ImageKey = "PENS03.ICO";
            treeNode13.Name = "rndOperacion";
            treeNode13.NodeFont = new System.Drawing.Font("Arial", 8.5F);
            treeNode13.SelectedImageIndex = 4;
            treeNode13.Text = "Operaciones";
            this.trwOperacion.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13});
            this.trwOperacion.SelectedImageIndex = 0;
            this.trwOperacion.Size = new System.Drawing.Size(220, 462);
            this.trwOperacion.TabIndex = 3;
            this.trwOperacion.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trwOperacion_AfterSelect);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblMenu.Location = new System.Drawing.Point(41, 442);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(135, 18);
            this.lblMenu.TabIndex = 2;
            this.lblMenu.Text = "Menu Principal";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblMenu);
            this.panel1.Controls.Add(this.trwOperacion);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.trwTurno);
            this.panel1.Controls.Add(this.trwConsulta);
            this.panel1.Controls.Add(this.olkbVentas);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(228, 639);
            this.panel1.TabIndex = 4;
            // 
            // lblDol
            // 
            this.lblDol.AutoSize = true;
            this.lblDol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDol.Location = new System.Drawing.Point(383, 22);
            this.lblDol.Name = "lblDol";
            this.lblDol.Size = new System.Drawing.Size(32, 13);
            this.lblDol.TabIndex = 12;
            this.lblDol.Text = "DOL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(770, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Venta";
            // 
            // lblEur
            // 
            this.lblEur.AutoSize = true;
            this.lblEur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEur.Location = new System.Drawing.Point(623, 22);
            this.lblEur.Name = "lblEur";
            this.lblEur.Size = new System.Drawing.Size(33, 13);
            this.lblEur.TabIndex = 13;
            this.lblEur.Text = "EUR";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Compra";
            // 
            // lblComDol
            // 
            this.lblComDol.AutoSize = true;
            this.lblComDol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComDol.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblComDol.Location = new System.Drawing.Point(442, 22);
            this.lblComDol.Name = "lblComDol";
            this.lblComDol.Size = new System.Drawing.Size(46, 13);
            this.lblComDol.TabIndex = 14;
            this.lblComDol.Text = "2.8400";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(541, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Venta";
            // 
            // lblVenDol
            // 
            this.lblVenDol.AutoSize = true;
            this.lblVenDol.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenDol.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblVenDol.Location = new System.Drawing.Point(535, 22);
            this.lblVenDol.Name = "lblVenDol";
            this.lblVenDol.Size = new System.Drawing.Size(46, 13);
            this.lblVenDol.TabIndex = 15;
            this.lblVenDol.Text = "2.9200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(444, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Compra";
            // 
            // lblComEur
            // 
            this.lblComEur.AutoSize = true;
            this.lblComEur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComEur.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblComEur.Location = new System.Drawing.Point(674, 22);
            this.lblComEur.Name = "lblComEur";
            this.lblComEur.Size = new System.Drawing.Size(46, 13);
            this.lblComEur.TabIndex = 16;
            this.lblComEur.Text = "2.8400";
            // 
            // lblVenEur
            // 
            this.lblVenEur.AutoSize = true;
            this.lblVenEur.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenEur.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblVenEur.Location = new System.Drawing.Point(767, 22);
            this.lblVenEur.Name = "lblVenEur";
            this.lblVenEur.Size = new System.Drawing.Size(46, 13);
            this.lblVenEur.TabIndex = 17;
            this.lblVenEur.Text = "2.9200";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Beige;
            this.panel2.Controls.Add(this.lblVenEur);
            this.panel2.Controls.Add(this.lblComEur);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lblVenDol);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblComDol);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblEur);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblDol);
            this.panel2.Location = new System.Drawing.Point(3, 641);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(833, 40);
            this.panel2.TabIndex = 16;
            // 
            // tsslPtoventa
            // 
            this.tsslPtoventa.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tsslPtoventa.Margin = new System.Windows.Forms.Padding(0, 50, 0, 2);
            this.tsslPtoventa.Name = "tsslPtoventa";
            this.tsslPtoventa.Size = new System.Drawing.Size(175, 13);
            this.tsslPtoventa.Text = "Estación de Venta: 192.168.61.40";
            // 
            // tsslTime
            // 
            this.tsslTime.AutoSize = false;
            this.tsslTime.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tsslTime.Margin = new System.Windows.Forms.Padding(180, 50, 0, 2);
            this.tsslTime.Name = "tsslTime";
            this.tsslTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tsslTime.RightToLeftAutoMirrorImage = true;
            this.tsslTime.Size = new System.Drawing.Size(400, 13);
            this.tsslTime.Text = "asasasasas";
            this.tsslTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslTime.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // sstPrincipal
            // 
            this.sstPrincipal.AutoSize = false;
            this.sstPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslPtoventa,
            this.tsslTime});
            this.sstPrincipal.Location = new System.Drawing.Point(3, 639);
            this.sstPrincipal.Name = "sstPrincipal";
            this.sstPrincipal.Size = new System.Drawing.Size(833, 65);
            this.sstPrincipal.TabIndex = 3;
            this.sstPrincipal.Text = "sstPrincipal";
            // 
            // olkbVentas
            // 
            this.olkbVentas.BackColor = System.Drawing.SystemColors.Highlight;
            this.olkbVentas.ButtonHeight = 50;
            outlookBarButton1.Enabled = true;
            outlookBarButton1.Image = null;
            outlookBarButton1.Tag = null;
            outlookBarButton1.Text = "Operaciones";
            outlookBarButton2.Enabled = true;
            outlookBarButton2.Image = null;
            outlookBarButton2.Tag = null;
            outlookBarButton2.Text = "Turno";
            outlookBarButton3.Enabled = true;
            outlookBarButton3.Image = null;
            outlookBarButton3.Tag = null;
            outlookBarButton3.Text = "Ayuda";
            this.olkbVentas.Buttons.Add(outlookBarButton1);
            this.olkbVentas.Buttons.Add(outlookBarButton2);
            this.olkbVentas.Buttons.Add(outlookBarButton3);
            this.olkbVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.olkbVentas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.olkbVentas.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.olkbVentas.GradientButtonHoverDark = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(192)))), ((int)(((byte)(91)))));
            this.olkbVentas.GradientButtonHoverLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.olkbVentas.GradientButtonNormalDark = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(193)))), ((int)(((byte)(140)))));
            this.olkbVentas.GradientButtonNormalLight = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(240)))), ((int)(((byte)(207)))));
            this.olkbVentas.GradientButtonSelectedDark = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(150)))), ((int)(((byte)(21)))));
            this.olkbVentas.GradientButtonSelectedLight = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            this.olkbVentas.Location = new System.Drawing.Point(3, 470);
            this.olkbVentas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.olkbVentas.Name = "olkbVentas";
            this.olkbVentas.SelectedButton = outlookBarButton1;
            this.olkbVentas.Size = new System.Drawing.Size(220, 164);
            this.olkbVentas.TabIndex = 0;
            this.olkbVentas.Click += new OutlookStyleControls.OutlookBar.ButtonClickEventHandler(this.outlookBar1_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(836, 704);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.sstPrincipal);
            this.Controls.Add(this.splitter1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VENTAS";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Principal_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.sstPrincipal.ResumeLayout(false);
            this.sstPrincipal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imglOperacion;
        private System.Windows.Forms.ImageList imglTurno;
        private System.Windows.Forms.ImageList imglConsulta;
        private OutlookStyleControls.OutlookBar olkbVentas;
        private System.Windows.Forms.TreeView trwConsulta;
        private System.Windows.Forms.TreeView trwTurno;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.TreeView trwOperacion;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDol;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblComDol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVenDol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblComEur;
        private System.Windows.Forms.Label lblVenEur;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripStatusLabel tsslPtoventa;
        private System.Windows.Forms.ToolStripStatusLabel tsslTime;
        private System.Windows.Forms.StatusStrip sstPrincipal;

    }
}

