using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaWSInterbank.Entidades
{
    public class TUATasaCambioHistorial
    {
        public string Cod_Tasa_Cambio { get; set; }

        public string Tip_Cambio { get; set; }

        public string Cod_Moneda { get; set; }

        public decimal? Imp_Valor { get; set; }

        public string Cod_Moneda2 { get; set; }

        public decimal? Imp_Valor2 { get; set; }

        public DateTime? Fch_Ini { get; set; }

        public DateTime? Fch_Fin { get; set; }

        public string Log_Usuario_Mod { get; set; }

        public string Log_Fecha_Mod { get; set; }

        public string Log_Hora_Mod { get; set; }

    }
}
