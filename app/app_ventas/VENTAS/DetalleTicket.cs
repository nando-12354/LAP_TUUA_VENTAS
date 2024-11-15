using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAP.TUUA.VENTAS
{
    public partial class DetalleTicket : Form
    {
        protected BO_Turno objBOTurno;
        protected BO_Operacion objBOOperacion;
        private Usuario objUsuario;
        private string CodigoTicket = "";
        public DetalleTicket(Usuario objUsuario, Principal formMyParent, string sNumTicket)
        {
            InitializeComponent();
            objBOTurno = new BO_Turno();
            objBOOperacion = new BO_Operacion();
            dgwDetalleTicket.AutoGenerateColumns = false;
            this.objUsuario = objUsuario;

            var dtTicket = objBOTurno.consultarDetalleTicket(sNumTicket, "", "");
            var dtTicketHist = objBOTurno.consultarHistTicket(sNumTicket);


            dgwDetalleTicket.DataSource = dtTicketHist;

            lblNumeroTicket.Text = dtTicket.Rows[0]["Cod_Numero_Ticket"].ToString();
            lblTipoVuelo.Text = dtTicket.Rows[0]["Tipo_Ticket"].ToString();
            lblCompania.Text = dtTicket.Rows[0]["Dsc_Compania"].ToString();
            lblTurno.Text = dtTicket.Rows[0]["Cod_Turno"].ToString();
            lblFechaVencimiento.Text = dtTicket.Rows[0]["Fch_Vencimiento"].ToString();
            lblPrecio.Text = dtTicket.Rows[0]["Imp_Precio"].ToString();
            lblModalidad.Text = dtTicket.Rows[0]["Nom_Modalidad"].ToString();
            lblContingencia.Text = dtTicket.Rows[0]["Flg_Contingencia"].ToString();
            lblTipoTicket.Text = dtTicket.Rows[0]["Cod_Tipo_Ticket"].ToString()+" ("+ dtTicket.Rows[0]["Dsc_Tipo_Ticket"].ToString() + ")";
            lblTipoPasajero.Text = dtTicket.Rows[0]["Tip_Pasajero"].ToString();
            lblEstadoActual.Text = dtTicket.Rows[0]["Dsc_Estado_Actual"].ToString();
            lblFormaPago.Text = dtTicket.Rows[0]["Dsc_Forma_Pago"].ToString();
            lblTipoCobro.Text = dtTicket.Rows[0]["Dsc_Tipo_Cobro"].ToString();
            lblFlagSincroniza.Text = dtTicket.Rows[0]["Flg_Sincroniza"].ToString();
            lblTipoTrasbordo.Text = dtTicket.Rows[0]["Tipo_Trasbordo"].ToString();
            //lblFechaVuelo.Text = dtTicket.Rows[0]["Fch_Vuelo"].ToString();
            lblFechaVuelo.Text = DateTime.ParseExact(dtTicket.Rows[0]["Fch_Vuelo"].ToString(), "yyyyMMdd", null).ToString("dd/MM/yyyy");
            lblNumeroVuelo.Text = dtTicket.Rows[0]["Dsc_Num_Vuelo"].ToString();
            lblNumeroReferencia.Text = dtTicket.Rows[0]["Dsc_Referencia"].ToString();
            lblNumeroExtensiones.Text = dtTicket.Rows[0]["Num_Extensiones"].ToString();
            lblEmpresaValue.Text = dtTicket.Rows[0]["Empresa"].ToString(); //FL.
            lblCajeroValue.Text = dtTicket.Rows[0]["Cajero"].ToString(); //FL.
            lblMetodoPagoValue.Text = dtTicket.Rows[0]["ModalidadPago"].ToString(); //FL.
        }
    }
}
