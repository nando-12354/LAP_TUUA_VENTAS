using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CLIENTEWS
{
    public static class CWS_Manager
    {
        public static CWS_Conexion ObtenerConexion(string strConectionCode)
        {
            string strConectionKey = (string)Property.htProperty["conexion"] + strConectionCode;
            string strClase = (string)Property.htProperty[strConectionKey];
            Type objType = Type.GetType(strClase);
            return (CWS_Conexion)Activator.CreateInstance(objType);
        }
    }
}
