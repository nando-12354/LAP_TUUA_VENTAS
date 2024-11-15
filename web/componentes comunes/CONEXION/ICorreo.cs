using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LAP.TUUA.CONEXION
{
    public interface ICorreo
    {
        DataTable obtenerAlarmasGeneradasSinEnviar();

        void EnviarCorreo(string sIdAlarma);

        bool verificarEstadoEnvio(string sIdAlarma);
    }
}
