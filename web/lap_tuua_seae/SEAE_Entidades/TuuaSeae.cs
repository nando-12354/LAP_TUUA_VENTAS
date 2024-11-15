using System;

namespace SEAE_Entidades
{
    public class TuuaSeae
    {
        public string Cod_Numero_Bcbp { get; set; }
        public string Num_Serie { get; set; }
        public string Num_Secuencial { get; set; }
        public string Cod_Compania { get; set; }
        public string Fch_Vuelo { get; set; }
        public string Fch_Creacion { get; set; }
        
        public DateTime FechaHoraCreacion {get; set; }
        
        public string StrFecha {get; set; }
        public string StrHora {get; set;}
        
        public string Hor_Creacion { get; set; }
        public string Num_Vuelo { get; set; }
        public string Dsc_Destino { get; set; }
        public string Num_Asiento { get; set; }
        public string Dsc_Compania { get; set; }
        public string Nom_Pasajero { get; set; }
        public string Tip_Vuelo { get; set; }

        public decimal Imp_Precio { get; set; }

        public string Cod_Eticket { get; set; }
        
        public string QRCode {get; set;}
            
        
            
    }
}