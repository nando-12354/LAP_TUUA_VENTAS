using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_Compania
    {
        public string Cod_Compania { get; set; }
        public string Tip_Compania { get; set; }
        public string Dsc_Compania { get; set; }
        public DateTime Fch_Creacion { get; set; }
        public string Cod_Especial_Compania { get; set; }
        public string Cod_Aerolinea { get; set; }
        public string Cod_SAP { get; set; }
        public string Cod_OACI { get; set; }
        public string Cod_IATA { get; set; }
        public string Dsc_Ruc { get; set; }
        public string Tip_Estado { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }

    }
}