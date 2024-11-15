using System;
using System.Collections.Generic;
namespace LAP.TUUA.ENTIDADES
{
    public class TemporalTicket
    {
        #region Fields
        public string CodNumeroTicket { get; set; }
        public string NumVuelo { get; set; }
        public string FchVuelo { get; set; }
        public string NumSerie { get; set; }
        public string FchCreacion { get; set; }
        public string HorCreacion { get; set; }
        public string FchRegistro { get; set; }
        public string Num_Ticket { get; set; }
        public string Fch_Registro { get; set; }
        public string SCodNumeroTicket { get; set; }
        public string SDscNumVuelo { get; set; }
        public string SCausalRehabilitacion { get; set; }
        public string SLogUsuarioMod { get; set; }
        public List<TemporalTicket> TemporalTicketLis { get; set; }
        #endregion
    }
}
