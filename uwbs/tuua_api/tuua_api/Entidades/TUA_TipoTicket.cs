using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_TipoTicket
    {
        public string Cod_Tipo_Ticket { get; set; }    
        public string Dsc_Tipo_Ticket { get; set; }
        public string Tip_Vuelo { get; set; }
        public string Tip_Pasajero { get; set; }
        public string Tip_Trasbordo { get; set; }
        public string Cod_Moneda { get; set; }
        public decimal Imp_Precio { get; set; }
	    public string Tip_Estado { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }
    }
}