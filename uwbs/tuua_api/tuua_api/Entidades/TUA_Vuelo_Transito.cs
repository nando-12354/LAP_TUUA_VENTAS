using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_Vuelo_Transito
    {
        public string Num_Vuelo { get; set; }
        public DateTime? Fch_Prog { get; set; }
        public DateTime? Hor_Prog { get; set; }
        public string Tip_Operacion { get; set; }
        public string Cod_Compania { get; set; }
        public string Tip_Vuelo { get; set; }
        public DateTime? Fch_Real { get; set; }
        public DateTime? Fch_Est { get; set; }
        public string Cod_Proc_Dest { get; set; }
        public string Cod_Escala { get; set; }
        public string Cod_Gate { get; set; }
        public string Tip_Gate_Terminal { get; set; }
        public string Cod_Faja { get; set; }
        public string Cod_Mostrador { get; set; }
        public string Dsc_Aerolinea { get; set; }
        public string Dsc_Proc_Destino { get; set; }
        public DateTime? Log_Fch_Creacion { get; set; }
        public DateTime? Log_Fch_Modificacion { get; set; }
        public string Dsc_Estado { get; set; }
        public bool Flg_Eliminado { get; set; }
        public string Cod_Iata { get; set; }
        public string Nro_Vuelo { get; set; }

    }
}