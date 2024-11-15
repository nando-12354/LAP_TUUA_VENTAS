using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaWSInterbank.Entidades
{
    public class TUATasaCambio
    {

        public string Cod_Tasa_Cambio { get; set; }

        public string Tip_Cambio { get; set; }

        public string Cod_Moneda { get; set; }

        public decimal Imp_Cambio_Actual { get; set; }

        public DateTime Fch_Proceso { get; set; }

        public string Tip_Ingreso { get; set; }

        public string Tip_Estado { get; set; }

        public DateTime? Fch_Programacion { get; set; }

        public string Log_Usuario_Mod { get; set; }

        public string Log_Fecha_Mod { get; set; }

        public string Log_Hora_Mod { get; set; }
    }
}
