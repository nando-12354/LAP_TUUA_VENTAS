using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tuua_api.Entidades
{
    public class TUA_Molinete
    {
        public string Cod_Molinete { get; set; }
        public string Dsc_Ip { get; set; }
        public string Dsc_Molinete { get; set; }
        public string Tip_Documento { get; set; }
        public string Tip_Vuelo { get; set; }
        public string Tip_Acceso { get; set; }
        public string Tip_Estado { get; set; }
        public string Log_Usuario_Mod { get; set; }
        public string Log_Fecha_Mod { get; set; }
        public string Log_Hora_Mod { get; set; }
        public string Fch_Sincroniza { get; set; }
        public string Est_Master { get; set; }
        public string Dsc_DBName { get; set; }
        public string Dsc_DBUser { get; set; }
        public string Dsc_DBPassword { get; set; }
        public string Tip_Molinete { get; set; }
        public string Flg_Sincroniza { get; set; }
        public string cod_grupo { get; set; }

    }
}