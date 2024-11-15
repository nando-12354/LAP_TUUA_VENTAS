using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.CONEXION
{
    public interface IAdministracion
    {
        
        string Cod_Usuario
        {
            get;
            set;
        }

        string Cod_Modulo
        {
            get;
            set;
        }

        string Cod_Sub_Modulo
        {
            get;
            set;
        }

        object Registrar(params object[] objeto);
        object Actualizar(params object[] objeto);
        object Eliminar(params object[] objeto);
    }
}
