/*
Sistema		 :   TUUA
Aplicación	 :   Administracion
Objetivo		 :   Proceso de gestión de Alarmas.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   13/10/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Data;

namespace LAP.TUUA.CONTROL
{
    public class BO_Alarmas
    {
        private Conexion objCnxAlarmas;
        private ICorreo oCorreo;

        public BO_Alarmas()
        {
            oCorreo = (ICorreo)Resolver.ObtenerConexionObject(Define.CNX_15);
        }

        public BO_Alarmas(string strUsuario,string strModulo,string strSubModulo)
        {
            objCnxAlarmas = Resolver.ObtenerConexion(Define.CNX_09);
            objCnxAlarmas.Cod_Usuario = strUsuario;
            objCnxAlarmas.Cod_Modulo = strModulo;
            objCnxAlarmas.Cod_Sub_Modulo = strSubModulo;

        }

        #region ConfigAlarma


        public bool insertarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            return objCnxAlarmas.insertarCnfgAlarma(objCnfgAlarma);
        }


        public bool actualizarCnfgAlarma(CnfgAlarma objCnfgAlarma)
        {
            return objCnxAlarmas.actualizarCnfgAlarma(objCnfgAlarma);
        }


        public bool eliminarCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            return objCnxAlarmas.eliminarCnfgAlarma(sCodAlarma, sCodModulo);
        }


        public DataTable ListarAllCnfgAlarma()
        {
            return objCnxAlarmas.ListarAllCnfgAlarma();
        }


        public CnfgAlarma obtenerCnfgAlarma(string sCodAlarma, string sCodModulo)
        {
            return objCnxAlarmas.obtenerCnfgAlarma(sCodAlarma, sCodModulo);

        }


        #endregion

        #region Alarmas Generadas

        public bool insertarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            return objCnxAlarmas.insertarAlarmaGenerada(objAlarmaGenerada);

        }

        public bool actualizarAlarmaGenerada(AlarmaGenerada objAlarmaGenerada)
        {
            return objCnxAlarmas.actualizarAlarmaGenerada(objAlarmaGenerada);

        }

        public DataTable ListarAllAlarmaGenerada()
        {
            return objCnxAlarmas.ListarAllAlarmaGenerada();

        }


        public AlarmaGenerada obtenerAlarmaGenerada(string sCodAlarmaGenerada)
        {
            return objCnxAlarmas.obtenerAlarmaGenerada(sCodAlarmaGenerada);

        }

        public DataTable ListarAlarmaGeneradaEnviadas()
        {
            return objCnxAlarmas.ListarAlarmaGeneradaEnviadas();

        }

        public DataTable ConsultaAlarmaGenerada(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sModulo, string sTipoAlarma, string sEstado)
        {
            return objCnxAlarmas.ConsultaAlarmaGenerada(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sModulo, sTipoAlarma, sEstado);
        }
        
        public DataTable obtenerAlarmasGeneradasSinEnviar()
        {
            return (DataTable)oCorreo.obtenerAlarmasGeneradasSinEnviar();
        }

        public bool EnviarCorreo(string sIdAlarma)
        {
            oCorreo.EnviarCorreo(sIdAlarma);
            return oCorreo.verificarEstadoEnvio(sIdAlarma);
        }

        #endregion

        #region Alarmas

        public DataTable ObtenerAlarmaxCodModulo(string sCodModulo)
        {
            return objCnxAlarmas.ObtenerAlarmaxCodModulo(sCodModulo);
        }
        #endregion

    }
}
