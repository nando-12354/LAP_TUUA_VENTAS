using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_Error
    {
        protected string cod_Error;
        private bool flg_Error;
        private Conexion objCnxError;

        public BO_Error(){
            objCnxError = Resolver.ObtenerConexion(Define.CNX_04);
        }
        public bool IsError()
        {
            try
            {
                flg_Error = objCnxError.IsError();
                if (flg_Error)
                {
                    ErrorHandler.Flg_Error = flg_Error;
                    ErrorHandler.Cod_Error = objCnxError.GetErrorCode();
                }
                return flg_Error;
            }
            catch(Exception ex)
            {
                throw;
            
            }
        }
    }
}
