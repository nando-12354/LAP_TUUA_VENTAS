using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class CuadreTurno
    {
        private string cod_Moneda;
        private string dsc_Moneda;
        private decimal imp_EfectivoIni;
        private int can_TicketInt;
        private int can_TicketNac;
        private decimal imp_TicketInt;
        private decimal imp_TicketNac;
        private int can_IngCaja;
        protected decimal imp_IngCaja;
        protected int can_VentaMoneda;
        private decimal imp_VentaMoneda;
        private int can_EgreCaja;
        private decimal imp_EgreCaja;
        private int can_CompraMoneda;
        private decimal imp_CompraMoneda;
        private decimal imp_EfectivoFinal;
        private int can_AnulaInt;
        private int can_AnulaNac;
        private int can_InfanteInt;
        private int can_InfanteNac;
        private int can_CreditoInt;
        private int can_CreditoNac;
        private decimal imp_CreditoInt;
        private decimal imp_CreditoNac;
        private string dsc_Simbolo;
        private decimal imp_EfecSobrante;
        private decimal imp_EfecFaltante;

        private int can_Ticket_EfeInt;
        private decimal imp_Ticket_EfeInt;
        private int can_Ticket_TraInt;
        private decimal imp_Ticket_TraInt;
        private int can_Ticket_DebInt;
        private decimal imp_Ticket_DebInt;
        private int can_Ticket_CreInt;
        private decimal imp_Ticket_CreInt;
        private int can_Ticket_CheInt;
        private decimal imp_Ticket_CheInt;

        private int can_Ticket_EfeNac;
        private decimal imp_Ticket_EfeNac;
        private int can_Ticket_TraNac;
        private decimal imp_Ticket_TraNac;
        private int can_Ticket_DebNac;
        private decimal imp_Ticket_DebNac;
        private int can_Ticket_CreNac;
        private decimal imp_Ticket_CreNac;
        private int can_Ticket_CheNac;
        private decimal imp_Ticket_CheNac;

        private decimal imp_Recaudado_Fin;

        public int Can_VentaMoneda
        {
            get
            {
                return can_VentaMoneda;
            }
            set
            {
                can_VentaMoneda = value;
            }
        }

        public decimal Imp_IngCaja
        {
            get
            {
                return imp_IngCaja;
            }
            set
            {
                imp_IngCaja = value;
            }
        }

        public string Cod_Moneda
        {
            get
            {
                return cod_Moneda;
            }
            set
            {
                cod_Moneda = value;
            }
        }

        public decimal Imp_EfectivoIni
        {
            get
            {
                return imp_EfectivoIni;
            }
            set
            {
                imp_EfectivoIni = value;
            }
        }

        public int Can_TicketInt
        {
            get
            {
                return can_TicketInt;
            }
            set
            {
                can_TicketInt = value;
            }
        }

        public decimal Imp_CreditoNac
        {
            get
            {
                return imp_CreditoNac;
            }
            set
            {
                imp_CreditoNac = value;
            }
        }

        public int Can_TicketNac
        {
            get
            {
                return can_TicketNac;
            }
            set
            {
                can_TicketNac = value;
            }
        }

        public decimal Imp_TicketInt
        {
            get
            {
                return imp_TicketInt;
            }
            set
            {
                imp_TicketInt = value;
            }
        }

        public decimal Imp_TicketNac
        {
            get
            {
                return imp_TicketNac;
            }
            set
            {
                imp_TicketNac = value;
            }
        }

        public int Can_IngCaja
        {
            get
            {
                return can_IngCaja;
            }
            set
            {
                can_IngCaja = value;
            }
        }

        public decimal Imp_VentaMoneda
        {
            get
            {
                return imp_VentaMoneda;
            }
            set
            {
                imp_VentaMoneda = value;
            }
        }

        public int Can_EgreCaja
        {
            get
            {
                return can_EgreCaja;
            }
            set
            {
                can_EgreCaja = value;
            }
        }

        public decimal Imp_EgreCaja
        {
            get
            {
                return imp_EgreCaja;
            }
            set
            {
                imp_EgreCaja = value;
            }
        }

        public int Can_CompraMoneda
        {
            get
            {
                return can_CompraMoneda;
            }
            set
            {
                can_CompraMoneda = value;
            }
        }

        public decimal Imp_CompraMoneda
        {
            get
            {
                return imp_CompraMoneda;
            }
            set
            {
                imp_CompraMoneda = value;
            }
        }

        public decimal Imp_EfectivoFinal
        {
            get
            {
                return imp_EfectivoFinal;
            }
            set
            {
                imp_EfectivoFinal = value;
            }
        }

        public int Can_AnulaInt
        {
            get
            {
                return can_AnulaInt;
            }
            set
            {
                can_AnulaInt = value;
            }
        }

        public int Can_AnulaNac
        {
            get
            {
                return can_AnulaNac;
            }
            set
            {
                can_AnulaNac = value;
            }
        }

        public int Can_InfanteInt
        {
            get
            {
                return can_InfanteInt;
            }
            set
            {
                can_InfanteInt = value;
            }
        }

        public int Can_InfanteNac
        {
            get
            {
                return can_InfanteNac;
            }
            set
            {
                can_InfanteNac = value;
            }
        }

        public int Can_CreditoInt
        {
            get
            {
                return can_CreditoInt;
            }
            set
            {
                can_CreditoInt = value;
            }
        }

        public int Can_CreditoNac
        {
            get
            {
                return can_CreditoNac;
            }
            set
            {
                can_CreditoNac = value;
            }
        }

        public decimal Imp_CreditoInt
        {
            get
            {
                return imp_CreditoInt;
            }
            set
            {
                imp_CreditoInt = value;
            }
        }

        public string Dsc_Moneda
        {
            get
            {
                return dsc_Moneda;
            }
            set
            {
                dsc_Moneda = value;
            }
        }

        public string Dsc_Simbolo
        {
            get
            {
                return dsc_Simbolo;
            }
            set
            {
                dsc_Simbolo = value;
            }
        }

        public decimal Imp_EfecSobrante
        {
            get
            {
                return imp_EfecSobrante;
            }
            set
            {
                imp_EfecSobrante = value;
            }
        }

        public decimal Imp_EfecFaltante
        {
            get
            {
                return imp_EfecFaltante;
            }
            set
            {
                imp_EfecFaltante = value;
            }
        }

        public int Can_Ticket_EfeInt
        {
            get
            {
                return can_Ticket_EfeInt;
            }
            set
            {
                can_Ticket_EfeInt = value;
            }
        }

        public decimal Imp_Ticket_EfeInt
        {
            get
            {
                return imp_Ticket_EfeInt;
            }
            set
            {
                imp_Ticket_EfeInt = value;
            }
        }

        public int Can_Ticket_TraInt
        {
            get
            {
                return can_Ticket_TraInt;
            }
            set
            {
                can_Ticket_TraInt = value;
            }
        }

        public decimal Imp_Ticket_TraInt
        {
            get
            {
                return imp_Ticket_TraInt;
            }
            set
            {
                imp_Ticket_TraInt = value;
            }
        }

        public int Can_Ticket_DebInt
        {
            get
            {
                return can_Ticket_DebInt;
            }
            set
            {
                can_Ticket_DebInt = value;
            }
        }

        public decimal Imp_Ticket_DebInt
        {
            get
            {
                return imp_Ticket_DebInt;
            }
            set
            {
                imp_Ticket_DebInt = value;
            }
        }

        public int Can_Ticket_CreInt
        {
            get
            {
                return can_Ticket_CreInt;
            }
            set
            {
                can_Ticket_CreInt = value;
            }
        }

        public decimal Imp_Ticket_CreInt
        {
            get
            {
                return imp_Ticket_CreInt;
            }
            set
            {
                imp_Ticket_CreInt = value;
            }
        }

        public int Can_Ticket_CheInt
        {
            get
            {
                return can_Ticket_CheInt;
            }
            set
            {
                can_Ticket_CheInt = value;
            }
        }

        public decimal Imp_Ticket_CheInt
        {
            get
            {
                return imp_Ticket_CheInt;
            }
            set
            {
                imp_Ticket_CheInt = value;
            }
        }

        public int Can_Ticket_EfeNac
        {
            get
            {
                return can_Ticket_EfeNac;
            }
            set
            {
                can_Ticket_EfeNac = value;
            }
        }

        public decimal Imp_Ticket_EfeNac
        {
            get
            {
                return imp_Ticket_EfeNac;
            }
            set
            {
                imp_Ticket_EfeNac = value;
            }
        }

        public int Can_Ticket_TraNac
        {
            get
            {
                return can_Ticket_TraNac;
            }
            set
            {
                can_Ticket_TraNac = value;
            }
        }

        public decimal Imp_Ticket_TraNac
        {
            get
            {
                return imp_Ticket_TraNac;
            }
            set
            {
                imp_Ticket_TraNac = value;
            }
        }

        public int Can_Ticket_DebNac
        {
            get
            {
                return can_Ticket_DebNac;
            }
            set
            {
                can_Ticket_DebNac = value;
            }
        }

        public decimal Imp_Ticket_DebNac
        {
            get
            {
                return imp_Ticket_DebNac;
            }
            set
            {
                imp_Ticket_DebNac = value;
            }
        }

        public int Can_Ticket_CreNac
        {
            get
            {
                return can_Ticket_CreNac;
            }
            set
            {
                can_Ticket_CreNac = value;
            }
        }

        public decimal Imp_Ticket_CreNac
        {
            get
            {
                return imp_Ticket_CreNac;
            }
            set
            {
                imp_Ticket_CreNac = value;
            }
        }

        public int Can_Ticket_CheNac
        {
            get
            {
                return can_Ticket_CheNac;
            }
            set
            {
                can_Ticket_CheNac = value;
            }
        }

        public decimal Imp_Ticket_CheNac
        {
            get
            {
                return imp_Ticket_CheNac;
            }
            set
            {
                imp_Ticket_CheNac = value;
            }
        }

        public decimal Imp_Recaudado_Fin
        {
            get
            {
                return imp_Recaudado_Fin;
            }
            set
            {
                imp_Recaudado_Fin = value;
            }
        }

    }
}
