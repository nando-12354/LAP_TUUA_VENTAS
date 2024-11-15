using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LAP.TUUA.CONEXION;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using System.Data;

namespace LAP.TUUA.NATIVO
{
    public class NAT_Correo : ICorreo
    {
        public string Dsc_PathSPConfig;
        DAO_AlarmaGenerada objDAOAlarmaGenerada;

        public NAT_Correo()
        {
            Dsc_PathSPConfig = (string)Property.htProperty["PATHRECURSOS"];
            objDAOAlarmaGenerada = new DAO_AlarmaGenerada(Dsc_PathSPConfig);            
        }

        public DataTable obtenerAlarmasGeneradasSinEnviar()
        {
            return objDAOAlarmaGenerada.obtenerAlarmasGeneradasSinEnviar();
        }

        public void EnviarCorreo(string sIdAlarma)
        {
            objDAOAlarmaGenerada.EnviarCorreo(sIdAlarma);
        }

        public bool verificarEstadoEnvio(string sIdAlarma)
        {
            return objDAOAlarmaGenerada.verificarEstadoEnvio(sIdAlarma);
        }
    }
}
