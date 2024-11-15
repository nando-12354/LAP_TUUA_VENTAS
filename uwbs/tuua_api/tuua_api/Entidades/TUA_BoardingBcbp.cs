using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_BoardingBcbp
    {
        public long Num_Secuencial_Bcbp { get; set; }
        public string Cod_Compania { get; set; }
        public string Num_Vuelo { get; set; }
        public string Fch_Vuelo { get; set; }
        public string Num_Asiento { get; set; }
        public string Nom_Pasajero { get; set; }
        public string Dsc_Trama_Bcbp { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }
        public string Tip_Ingreso { get; set; }
        public string  Tip_Estado { get; set; }
        public int Num_Rehabilitaciones { get; set; }
        public string Cod_Unico_Bcbp { get; set; }
        public DateTime Fch_Vencimiento { get; set; }
        public string Fch_Creacion { get; set; }
        public string Hor_Creacion { get; set; }
        public string Cod_Unico_Bcbp_Rel { get; set; }
        public string Flg_Sincroniza { get; set; }
        public string Tip_Pasajero { get; set; }
        public string Tip_Vuelo { get; set; }
        public string Tip_Trasbordo { get; set; }
        public string Tip_Anulacion { get; set; }
        public string Cod_Numero_Bcbp { get; set; }
        public string Num_Serie { get; set; }
        public string Num_Secuencial { get; set; }
        public string Flg_Tipo_Bcbp { get; set; }
        public long Num_Secuencial_Bcbp_Rel { get; set; }
        public long Num_Secuencial_Bcbp_Rel_Sec { get; set; }       
        public string Nro_Boarding { get; set; }
        public string Dsc_Destino { get; set; }
        public string Cod_Eticket { get; set; }
        public string Cod_Moneda { get; set; }
        public decimal Imp_Precio { get; set; }
        public decimal Imp_Tasa_Compra { get; set; }
        public decimal Imp_Tasa_Venta { get; set; }
        public string Num_Proceso_Rehab { get; set; }
        public string Flg_Bloqueado { get; set; }
        public string Flg_WSError { get; set; }
        public string Flg_Incluye_Tuua { get; set; }
        public string Fch_Rehabilitacion { get; set; }
        public string Num_Airline_Code { get; set; }
        public string Num_Document_Form { get; set; }
        public string Num_Checkin { get; set; }
    }
}