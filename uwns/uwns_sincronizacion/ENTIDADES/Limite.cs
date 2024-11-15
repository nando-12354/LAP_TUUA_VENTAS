/*
Sistema		     : TUUA
Aplicación		 : VENTAS
Objetivo		 : Describir la entidad Limite.
Especificaciones :
Fecha Creacion	 : 14/07/2009
Programador		 :	JCISNEROS
Observaciones	 :	--
*/ 

using System;
using System.Collections.Generic;
using System.Text;

namespace LAP.TUUA.ENTIDADES
{
    public class Limite
    {
        private string cod_Moneda;
        private string cod_TipoOpera;
        private decimal imp_LimMaximo;
        private decimal imp_LimMinimo;
        private decimal imp_MargenCaja;

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

        public string Cod_TipoOpera
        {
            get
            {
                return cod_TipoOpera;
            }
            set
            {
                cod_TipoOpera = value;
            }
        }

        public decimal Imp_LimMaximo
        {
            get
            {
                return imp_LimMaximo;
            }
            set
            {
                imp_LimMaximo = value;
            }
        }

        public decimal Imp_LimMinimo
        {
            get
            {
                return imp_LimMinimo;
            }
            set
            {
                imp_LimMinimo = value;
            }
        }

        public decimal Imp_MargenCaja
        {
            get
            {
                return imp_MargenCaja;
            }
            set
            {
                imp_MargenCaja = value;
            }
        }

    }
}
