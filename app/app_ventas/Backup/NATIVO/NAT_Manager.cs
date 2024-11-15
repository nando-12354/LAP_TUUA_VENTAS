using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.NATIVO
{
    public static class NAT_Manager
    {
        public static NAT_Conexion ObtenerConexion(string strConectionCode)
        {
            string strConectionKey = (string)Property.htProperty["conexion"] + strConectionCode;
            string strClase = (string)Property.htProperty[strConectionKey];
            Type objType = Type.GetType(strClase);
            return (NAT_Conexion)Activator.CreateInstance(objType);
        }
    }
}
