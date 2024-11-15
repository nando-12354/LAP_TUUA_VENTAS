using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_VueloProgramado
    {
        public string Cod_Compania { get; set; }
        public string Num_Vuelo { get; set; }
        public string Fch_Vuelo { get; set; }
        public string Hor_Vuelo { get; set; }
        public string Dsc_Vuelo { get; set; }
        public string Tip_Vuelo { get; set; }
        public string Tip_Estado { get; set; }
        public string Dsc_Destino { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }
        public string Flg_Programado { get; set; }
        public string Num_Puerta { get; set; }
        
        public DateTime? Fch_Est { get; set; }
        public DateTime? Fch_Real { get; set; }
        public DateTime? Fch_Prog { get; set; }
        public string Dsc_Estado { get; set; }

    }
}