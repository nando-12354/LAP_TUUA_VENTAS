using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_Ticket
    {

        public string Cod_Numero_Ticket { get; set; }
        public string Num_Serie { get; set; }
        public string Num_Secuencial { get; set; }
        public string Cod_Compania { get; set; }
        public string Cod_Venta_Masiva { get; set; }
        public string Dsc_Num_Vuelo { get; set; }
        public string Fch_Vuelo { get; set; }
        public string Tip_Estado_Actual { get; set; }
        public string Tip_Anulacion { get; set; }
        public string Fch_Creacion { get; set; }
        public string Hor_Creacion { get; set; }
        public string Cod_Turno { get; set; }
        public decimal Imp_Precio { get; set; }
        public string Cod_Precio_Ticket { get; set; }
        public string Cod_Moneda { get; set; }
        public string Cod_Tasa_Cambio { get; set; }
        public decimal Imp_Tasa_Cambio { get; set; }
        public string Cod_Tasa_Venta { get; set; }
        public decimal Imp_Tasa_Venta { get; set; }
        public string Fch_Vencimiento { get; set; }
        public string Cod_Modalidad_Venta { get; set; }
        public int Num_Rehabilitaciones { get; set; }
        public string Cod_Tipo_Ticket { get; set; }
        public string Num_Referencia { get; set; }
        public string Flg_Contingencia { get; set; }
        public int Num_Extensiones { get; set; }
        public string Tip_Pago { get; set; }
        public string Flg_Cobro { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }
        public string Flg_Sincroniza { get; set; }
        public string Fch_Uso { get; set; }
        public string Fch_Rehabilitacion { get; set; }
        
        //atributos relacionados a otras tablas
        public string Dsc_Ticket_Estado { get; set; }
        public string Dsc_Motivo { get; set; }
        public string Cod_Equipo_Mod { get; set; }



    }
}