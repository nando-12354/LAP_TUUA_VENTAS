using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.CLIENTEWS;
using LAP.TUUA.CONEXION;
using LAP.TUUA.NATIVO;
using System.Reflection;

namespace LAP.TUUA.RESOLVER
{
    public static class Resolver
    {
        public static Conexion ObtenerConexion(string strConectionCode)
        {
           Conexion objConexion=null;
           try
           {
               string strConectionKey = (string)Property.htProperty[Define.CONEXION];
               Property.htProperty[Define.CONEXION] = strConectionKey.ToUpper();
               if (strConectionKey.ToUpper() == Define.CLIENTEWS)
               {
                   objConexion = CWS_Manager.ObtenerConexion(strConectionCode);
               }
               else if (strConectionKey.ToUpper() == Define.NATIVO)
               {
                   objConexion = NAT_Manager.ObtenerConexion(strConectionCode);
               }
               return objConexion;
           }
           catch (Exception e)
           {
               ErrorHandler.Cod_Error = Define.ERR_007;
               ErrorHandler.Flg_Error = true;
               throw;
           }
        }

        public static object ObtenerConexionObject(string strConectionCode)
        {
            object objConexion = null;
            try
            {
                string strConectionKey = (string)Property.htProperty[Define.CONEXION];
                Property.htProperty[Define.CONEXION] = strConectionKey.ToUpper();
                if (strConectionKey.ToUpper() == Define.CLIENTEWS)
                {
                    objConexion = CWS_Manager.ObtenerConexion(strConectionCode);
                }
                else if (strConectionKey.ToUpper() == Define.NATIVO)
                {
                    objConexion = NAT_Manager.ObtenerInstanciaClase(strConectionCode);
                }
                return objConexion;
            }
            catch (Exception e)
            {
                ErrorHandler.Cod_Error = Define.ERR_007;
                ErrorHandler.Flg_Error = true;
                throw;
            }
        }
    }
}
