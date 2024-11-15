using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_Transito
    {
        public long Id { get; set; }
        public string Cod_Compania_Origen { get; set; }
        public string Num_Vuelo_Origen { get; set; }
        public DateTime Fch_Vuelo_Origen { get; set; }
        public DateTime Hor_Vuelo_Origen { get; set; }
        public string Tip_Vuelo_Origen { get; set; }
        public string Tip_Operacion_Origen { get; set; }
        public string Cod_Origen { get; set; }
        public string Dsc_Origen { get; set; }
        public string Cod_Compania_Destino { get; set; }
        public string Num_Vuelo_Destino { get; set; }
        public DateTime Fch_Vuelo_Destino { get; set; }
        public DateTime Hor_Vuelo_Destino { get; set; }
        public string Tip_Vuelo_Destino { get; set; }
        public string Tip_Operacion_Destino { get; set; }
        public string Cod_Destino { get; set; }
        public string Dsc_Destino { get; set; }
        public string Trama_Origen { get; set; }
        public string Trama_Destino { get; set; }
        public DateTime Log_Fch_Registro { get; set; }
        public DateTime Log_Fch_Mod { get; set; }
        public string Cod_Molinete { get; set; }


    }
}