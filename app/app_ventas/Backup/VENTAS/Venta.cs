using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.PRINTER;
using LAP.TUUA.UTIL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;

namespace LAP.TUUA.VENTAS
{
    public abstract class Venta : Form
    {
        public TasaCambio objTasaCambio;
        public BO_Operacion objBOOpera;
        public int Can_Tickets;
        public TipoTicket objTipoTicket;
        //INICIO propiedades del dialogo: TARJETA-EFECTIVO
        public decimal Imp_Dialogo_Pagar;
        public decimal Imp_Dialogo_Pagado;
        public decimal Imp_Dialogo_Restante;
        public Hashtable Lista_Dialogo_Tasa;
        public List<Cambio> Lista_Dialogo_Cambio;
        public string Cod_Moneda_Dialogo;
        public string Cod_Moneda_Sol;
        public string Cod_Moneda_Dol;
        public Hashtable Lista_Flujo_Importe;
        //FIN propiedades del dialogo: TARJETA-EFECTIVO
        public List<Cambio> Lista_Cambio_Vuelto;
        public Hashtable Lista_Flujo_Vuelto;

        public abstract void ActualizarVuelto();
        public abstract void CargarLineaResumen(string strTipPago, string strMoneda, decimal decMonto, int intFlg);
        public abstract void ConvertirTarjetaContado();
        public abstract void SetearCheckRadioButtonVuelto(Define.centavos intCentavo, Moneda objMoneda);
        public abstract void MostrarVueltoCentavos(decimal decVuelTotal, decimal decVuelIzq, decimal decVuelCen, Define.centavos intCentavo);
        public abstract void Centavos(string strMoneda);
    }
}
