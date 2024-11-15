using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LAP.Tuua.Web.Model
{
    public class Saa_Token
    {
        public System.Guid id_token { get; set; }
        public string dsc_token { get; set; }
        public int cod_usuario { get; set; }
        public DateTime fch_acceso { get; set; }
        public DateTime log_fch_cre { get; set; }
        public DateTime log_fch_mod { get; set; }
	}
}