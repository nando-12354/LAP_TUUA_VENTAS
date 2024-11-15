using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tuua_modulo_salida.Entidades
{
    public class TemporalBoardingPass
    {
        public string CodCompania { get; set; }
        public long NumSecuencialBcbp { get; set; }
        public string CodNumeroBcbp { get; set; }
        public string NumVuelo { get; set; }
        public string FchVuelo { get; set; }
        public string NumAsiento { get; set; }
        public string NomPasajero { get; set; }
        public string DscTramaBcbp { get; set; }
        public string FchCreacion { get; set; }
        public string FchRegistro { get; set; }
        public string NumCheckin { get; set; }
        public string Num_Ticket { get; set; }
        public string Fch_Registro { get; set; }
        public string SListaBCBPs { get; set; }
        public string SCodNumeroBcbp { get; set; }
        public string SDscNumVuelo { get; set; }
        public string SCausalRehabilitacion { get; set; }
        public string SLogUsuarioMod { get; set; }
        public string SCausal_Rehabilitacion { get; set; }
        public string SCausal_Rehabilitaciones { get; set; }
        public string SFlg_Tipo { get; set; }
        public string SLog_Usuario_Mod { get; set; }
        public List<TemporalBoardingPass> TemporalBoardingPassLis { get; set; }
    }
}
