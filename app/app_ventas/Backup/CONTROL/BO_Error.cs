/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Proceso de gestión de operaciones.
Especificaciones:   
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.CONEXION;
using LAP.TUUA.UTIL;
using LAP.TUUA.RESOLVER;
using System.Collections;

namespace LAP.TUUA.CONTROL
{
    public class BO_Error
    {
        public Conexion objCnxError;
        public BO_Error()
        {
            objCnxError = Resolver.ObtenerConexion(Define.CNX_04);
        }

        public bool IsError()
        {
            try
            {
                bool isError = objCnxError.IsError();
                if (isError)
                {
                    ErrorHandler.Flg_Error = isError;
                    ErrorHandler.Cod_Error = objCnxError.GetErrorCode();
                    string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];
                    ErrorHandler.Desc_Mensaje = strMessage;
                }
                return isError;
            }
            catch
            {
                ErrorHandler.Desc_Mensaje = "SERVICIOS WEB NO DISPONIBLE";
                return true;
            }
        }
    }
}
