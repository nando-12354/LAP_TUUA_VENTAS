using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class Cambio
    {
        private string cod_Moneda;
        private string tip_Cambio;
        private string dsc_Tipo;
        private string dsc_MonedaInt;
        private decimal imp_MontoInt;
        private string dsc_SimboloInt;
        private decimal imp_MontoNac;
        private decimal imp_TasaCambio;
        private int flg_Cambio;
        private string num_Operacion;
        private string tip_Pago;
        private bool flg_TarjetaEfec;

        public string Dsc_Tipo
        {
            get
            {
                return dsc_Tipo;
            }
            set
            {
                dsc_Tipo = value;
            }
        }

        public string Dsc_MonedaInt
        {
            get
            {
                return dsc_MonedaInt;
            }
            set
            {
                dsc_MonedaInt = value;
            }
        }

        public decimal Imp_MontoInt
        {
            get
            {
                return imp_MontoInt;
            }
            set
            {
                imp_MontoInt = value;
            }
        }

        public string Dsc_SimboloInt
        {
            get
            {
                return dsc_SimboloInt;
            }
            set
            {
                dsc_SimboloInt = value;
            }
        }

        public decimal Imp_MontoNac
        {
            get
            {
                return imp_MontoNac;
            }
            set
            {
                imp_MontoNac = value;
            }
        }

        public decimal Imp_TasaCambio
        {
            get
            {
                return imp_TasaCambio;
            }
            set
            {
                imp_TasaCambio = value;
            }
        }

        public string Tip_Cambio
        {
            get
            {
                return tip_Cambio;
            }
            set
            {
                tip_Cambio = value;
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

        public int Flg_Cambio
        {
            get
            {
                return flg_Cambio;
            }
            set
            {
                flg_Cambio = value;
            }
        }

        public string Num_Operacion
        {
            get
            {
                return num_Operacion;
            }
            set
            {
                num_Operacion = value;
            }
        }

        public string Tip_Pago
        {
            get
            {
                return tip_Pago;
            }
            set
            {
                tip_Pago = value;
            }
        }

        public bool Flg_TarjetaEfec
        {
            get
            {
                return flg_TarjetaEfec;
            }
            set
            {
                flg_TarjetaEfec = value;
            }
        }

    }
}
