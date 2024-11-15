
using System.Windows.Forms;

namespace LAP.TUUA.VENTAS
{
    partial class DetalleTicket
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumeroTicket = new System.Windows.Forms.Label();
            this.lblTurno = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCompania = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTipoVuelo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblModalidad = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblContingencia = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblFlagSincroniza = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblTipoTrasbordo = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lblTipoCobro = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblTipoPasajero = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblEstadoActual = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.lblFormaPago = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.lblTipoTicket = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.lblNumeroVuelo = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.lblNumeroReferencia = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lblNumeroExtensiones = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lblFechaVuelo = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblEmpresa = new System.Windows.Forms.Label(); //FL.
            this.lblEmpresaValue = new System.Windows.Forms.Label(); //FL.
            this.lblCajero = new System.Windows.Forms.Label(); //FL.
            this.lblCajeroValue = new System.Windows.Forms.Label(); //FL.
            this.lblMetodoPago = new System.Windows.Forms.Label(); //FL.
            this.lblMetodoPagoValue = new System.Windows.Forms.Label(); //FL.
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dgwDetalleTicket = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(dgwDetalleTicket)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número Ticket:";
            // 
            // lblNumeroTicket
            // 
            this.lblNumeroTicket.AutoSize = true;
            this.lblNumeroTicket.Location = new System.Drawing.Point(147, 26);
            this.lblNumeroTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroTicket.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNumeroTicket.Name = "lblNumeroTicket";
            this.lblNumeroTicket.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroTicket.TabIndex = 1;
            // 
            // lblTurno
            // 
            this.lblTurno.AutoSize = true;
            this.lblTurno.Location = new System.Drawing.Point(147, 107);
            this.lblTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTurno.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(0, 13);
            this.lblTurno.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Turno:";
            // 
            // lblCompania
            // 
            this.lblCompania.AutoSize = true;
            this.lblCompania.Location = new System.Drawing.Point(147, 80);
            this.lblCompania.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompania.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCompania.Name = "lblCompania";
            this.lblCompania.Size = new System.Drawing.Size(0, 13);
            this.lblCompania.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Compañía:";
            // 
            // lblTipoVuelo
            // 
            this.lblTipoVuelo.AutoSize = true;
            this.lblTipoVuelo.Location = new System.Drawing.Point(147, 53);
            this.lblTipoVuelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoVuelo.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTipoVuelo.Name = "lblTipoVuelo";
            this.lblTipoVuelo.Size = new System.Drawing.Size(0, 13);
            this.lblTipoVuelo.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Tipo vuelo:";
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(147, 161);
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(0, 13);
            this.lblPrecio.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(27, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Precio:";
            // 
            // lblModalidad
            // 
            this.lblModalidad.AutoSize = true;
            this.lblModalidad.Location = new System.Drawing.Point(147, 188);
            this.lblModalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblModalidad.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblModalidad.Name = "lblModalidad";
            this.lblModalidad.Size = new System.Drawing.Size(0, 13);
            this.lblModalidad.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(27, 188);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(104, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Modalidad de venta:";
            // 
            // lblContingencia
            // 
            this.lblContingencia.AutoSize = true;
            this.lblContingencia.Location = new System.Drawing.Point(147, 215);
            this.lblContingencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContingencia.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblContingencia.Name = "lblContingencia";
            this.lblContingencia.Size = new System.Drawing.Size(0, 13);
            this.lblContingencia.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(27, 215);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Contingencia:";
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.AutoSize = true;
            this.lblFechaVencimiento.Location = new System.Drawing.Point(147, 134);
            this.lblFechaVencimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaVencimiento.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(0, 13);
            this.lblFechaVencimiento.TabIndex = 9;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(27, 134);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "Fecha de vencimiento:";
            // 
            // lblFlagSincroniza
            // 
            this.lblFlagSincroniza.AutoSize = true;
            this.lblFlagSincroniza.Location = new System.Drawing.Point(395, 161);
            this.lblFlagSincroniza.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFlagSincroniza.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFlagSincroniza.Name = "lblFlagSincroniza";
            this.lblFlagSincroniza.Size = new System.Drawing.Size(0, 13);
            this.lblFlagSincroniza.TabIndex = 31;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(298, 161);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 13);
            this.label18.TabIndex = 30;
            this.label18.Text = "Flag sincroniza:";
            // 
            // lblTipoTrasbordo
            // 
            this.lblTipoTrasbordo.AutoSize = true;
            this.lblTipoTrasbordo.Location = new System.Drawing.Point(395, 188);
            this.lblTipoTrasbordo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoTrasbordo.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTipoTrasbordo.Name = "lblTipoTrasbordo";
            this.lblTipoTrasbordo.Size = new System.Drawing.Size(0, 13);
            this.lblTipoTrasbordo.TabIndex = 29;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(298, 188);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 13);
            this.label20.TabIndex = 28;
            this.label20.Text = "Tipo transbordo:";
            // 
            // lblTipoCobro
            // 
            this.lblTipoCobro.AutoSize = true;
            this.lblTipoCobro.Location = new System.Drawing.Point(395, 134);
            this.lblTipoCobro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoCobro.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTipoCobro.Name = "lblTipoCobro";
            this.lblTipoCobro.Size = new System.Drawing.Size(0, 13);
            this.lblTipoCobro.TabIndex = 25;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(298, 134);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 13);
            this.label24.TabIndex = 24;
            this.label24.Text = "Tipo cobro:";
            // 
            // lblTipoPasajero
            // 
            this.lblTipoPasajero.AutoSize = true;
            this.lblTipoPasajero.Location = new System.Drawing.Point(395, 53);
            this.lblTipoPasajero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoPasajero.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTipoPasajero.Name = "lblTipoPasajero";
            this.lblTipoPasajero.Size = new System.Drawing.Size(0, 13);
            this.lblTipoPasajero.TabIndex = 23;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(298, 53);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(89, 13);
            this.label26.TabIndex = 22;
            this.label26.Text = "Tipo de pasajero:";
            // 
            // lblEstadoActual
            // 
            this.lblEstadoActual.AutoSize = true;
            this.lblEstadoActual.Location = new System.Drawing.Point(395, 80);
            this.lblEstadoActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstadoActual.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblEstadoActual.Name = "lblEstadoActual";
            this.lblEstadoActual.Size = new System.Drawing.Size(0, 13);
            this.lblEstadoActual.TabIndex = 21;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(298, 80);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(75, 13);
            this.label28.TabIndex = 20;
            this.label28.Text = "Estado actual:";
            // 
            // lblFormaPago
            // 
            this.lblFormaPago.AutoSize = true;
            this.lblFormaPago.Location = new System.Drawing.Point(395, 107);
            this.lblFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormaPago.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFormaPago.Name = "lblFormaPago";
            this.lblFormaPago.Size = new System.Drawing.Size(0, 13);
            this.lblFormaPago.TabIndex = 19;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(298, 107);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(81, 13);
            this.label30.TabIndex = 18;
            this.label30.Text = "Forma de pago:";
            // 
            // lblTipoTicket
            // 
            this.lblTipoTicket.AutoSize = true;
            this.lblTipoTicket.Location = new System.Drawing.Point(395, 26);
            this.lblTipoTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoTicket.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblTipoTicket.Name = "lblTipoTicket";
            this.lblTipoTicket.Size = new System.Drawing.Size(0, 13);
            this.lblTipoTicket.TabIndex = 17;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(298, 26);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(75, 13);
            this.label32.TabIndex = 16;
            this.label32.Text = "Tipo de ticket:";
            // 
            // lblNumeroVuelo
            // 
            this.lblNumeroVuelo.AutoSize = true;
            this.lblNumeroVuelo.Location = new System.Drawing.Point(685, 53);
            this.lblNumeroVuelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroVuelo.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNumeroVuelo.Name = "lblNumeroVuelo";
            this.lblNumeroVuelo.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroVuelo.TabIndex = 41;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(552, 53);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(91, 13);
            this.label22.TabIndex = 40;
            this.label22.Text = "Número de vuelo:";
            // 
            // lblNumeroReferencia
            // 
            this.lblNumeroReferencia.AutoSize = true;
            this.lblNumeroReferencia.Location = new System.Drawing.Point(685, 80);
            this.lblNumeroReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroReferencia.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNumeroReferencia.Name = "lblNumeroReferencia";
            this.lblNumeroReferencia.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroReferencia.TabIndex = 39;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(552, 80);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(112, 13);
            this.label34.TabIndex = 38;
            this.label34.Text = "Número de referencia:";
            // 
            // lblNumeroExtensiones
            // 
            this.lblNumeroExtensiones.AutoSize = true;
            this.lblNumeroExtensiones.Location = new System.Drawing.Point(685, 107);
            this.lblNumeroExtensiones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroExtensiones.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNumeroExtensiones.Name = "lblNumeroExtensiones";
            this.lblNumeroExtensiones.Size = new System.Drawing.Size(0, 13);
            this.lblNumeroExtensiones.TabIndex = 37;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(552, 107);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(121, 13);
            this.label36.TabIndex = 36;
            this.label36.Text = "Número de extensiones:";
            // 
            // lblEmpresa //FL.
            // 
            this.lblEmpresa.AutoSize = true;
            this.lblEmpresa.Location = new System.Drawing.Point(552, 134);
            this.lblEmpresa.Name = "lblEmpresa";
            this.lblEmpresa.Size = new System.Drawing.Size(121, 13);
            this.lblEmpresa.TabIndex = 36;
            this.lblEmpresa.Text = "Empresa recaudadora:";
            // 
            // lblEmpresaValue //FL.
            // 
            this.lblEmpresaValue.AutoSize = true;
            this.lblEmpresaValue.Location = new System.Drawing.Point(685, 134);
            this.lblEmpresaValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpresaValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblEmpresaValue.Name = "lblEmpresaValue";
            this.lblEmpresaValue.Size = new System.Drawing.Size(121, 13);
            this.lblEmpresaValue.TabIndex = 36;
            // 
            // lblCajero //FL.
            // 
            this.lblCajero.AutoSize = true;
            this.lblCajero.Location = new System.Drawing.Point(552, 161);
            this.lblCajero.Name = "lblCajero";
            this.lblCajero.Size = new System.Drawing.Size(121, 13);
            this.lblCajero.TabIndex = 36;
            this.lblCajero.Text = "Cajero emisión:";
            // 
            // lblCajeroValue //FL.
            // 
            this.lblCajeroValue.AutoSize = true;
            this.lblCajeroValue.Location = new System.Drawing.Point(685, 161);
            this.lblCajeroValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCajeroValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCajeroValue.Name = "lblCajeroValue";
            this.lblCajeroValue.Size = new System.Drawing.Size(121, 13);
            this.lblCajeroValue.TabIndex = 36;
            // 
            // lblMetodoPago //FL.
            // 
            this.lblMetodoPago.AutoSize = true;
            this.lblMetodoPago.Location = new System.Drawing.Point(552, 188);
            this.lblMetodoPago.Name = "lblMetodoPago";
            this.lblMetodoPago.Size = new System.Drawing.Size(121, 13);
            this.lblMetodoPago.TabIndex = 36;
            this.lblMetodoPago.Text = "Tipo de pago:";
            // 
            // lblMetodoPagoValue //FL.
            // 
            this.lblMetodoPagoValue.AutoSize = true;
            this.lblMetodoPagoValue.Location = new System.Drawing.Point(685, 188);
            this.lblMetodoPagoValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMetodoPagoValue.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblMetodoPagoValue.Name = "lblMetodoPagoValue";
            this.lblMetodoPagoValue.Size = new System.Drawing.Size(121, 13);
            this.lblMetodoPagoValue.TabIndex = 36;
            // 
            // lblFechaVuelo
            // 
            this.lblFechaVuelo.AutoSize = true;
            this.lblFechaVuelo.Location = new System.Drawing.Point(685, 26);
            this.lblFechaVuelo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaVuelo.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblFechaVuelo.Name = "lblFechaVuelo";
            this.lblFechaVuelo.Size = new System.Drawing.Size(0, 13);
            this.lblFechaVuelo.TabIndex = 35;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(552, 26);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(84, 13);
            this.label38.TabIndex = 34;
            this.label38.Text = "Fecha de vuelo:";
            // 
            // dgwDetalleTicket
            // 
            dgwDetalleTicket.AllowUserToAddRows = false;
            dgwDetalleTicket.AllowUserToDeleteRows = false;
            dgwDetalleTicket.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgwDetalleTicket.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            dgwDetalleTicket.Location = new System.Drawing.Point(30, 245);
            dgwDetalleTicket.Name = "dgwDetalleTicket";
            dgwDetalleTicket.Size = new System.Drawing.Size(739, 155);
            dgwDetalleTicket.TabIndex = 42;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Num_Secuencial";
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Nro.";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "FechaProceso";
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Fecha Proceso";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "Dsc_Ticket_Estado";
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "Estado";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "Nom_Usuario";
            this.Column4.Frozen = true;
            this.Column4.HeaderText = "Usuario Proces";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Nom_Equipo";
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "Equipo";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Dsc_Num_Vuelo";
            this.Column6.Frozen = true;
            this.Column6.HeaderText = "Nro. de Vuelo";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "Dsc_Obs";
            this.Column7.Frozen = true;
            this.Column7.HeaderText = "Observaciones";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // DetalleTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(dgwDetalleTicket);
            this.Controls.Add(this.lblEmpresa); //FL.
            this.Controls.Add(this.lblEmpresaValue); //FL.
            this.Controls.Add(this.lblCajero); //FL.
            this.Controls.Add(this.lblCajeroValue); //FL.
            this.Controls.Add(this.lblMetodoPago); //FL.
            this.Controls.Add(this.lblMetodoPagoValue); //FL.
            this.Controls.Add(this.lblNumeroVuelo);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.lblNumeroReferencia);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.lblNumeroExtensiones);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.lblFechaVuelo);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.lblFlagSincroniza);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblTipoTrasbordo);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.lblTipoCobro);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.lblTipoPasajero);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.lblEstadoActual);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.lblFormaPago);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.lblTipoTicket);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblModalidad);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblContingencia);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblFechaVencimiento);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lblTipoVuelo);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblCompania);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTurno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblNumeroTicket);
            this.Controls.Add(this.label1);
            this.Name = "DetalleTicket";
            this.Text = "DetalleTicket";
            ((System.ComponentModel.ISupportInitialize)(dgwDetalleTicket)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumeroTicket;
        private System.Windows.Forms.Label lblTurno;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCompania;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTipoVuelo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblModalidad;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblContingencia;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblFlagSincroniza;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblTipoTrasbordo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblTipoCobro;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblTipoPasajero;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblEstadoActual;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lblFormaPago;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label lblTipoTicket;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label lblNumeroVuelo;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblNumeroReferencia;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lblNumeroExtensiones;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lblFechaVuelo;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblEmpresa; //FL.
        private System.Windows.Forms.Label lblEmpresaValue; //FL.
        private System.Windows.Forms.Label lblCajero; //FL.
        private System.Windows.Forms.Label lblCajeroValue; //FL.
        private System.Windows.Forms.Label lblMetodoPago; //FL.
        private System.Windows.Forms.Label lblMetodoPagoValue; //FL.
        private System.Windows.Forms.DataGridView dgwDetalleTicket;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}