using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using System.Data;
using LAP.TUUA.CONEXION;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_Configuracion
    {
        protected Conexion objCnxConfiguracion;

        public BO_Configuracion()
        {
            objCnxConfiguracion = Resolver.ObtenerConexion(Define.CNX_07);
        }

        public BO_Configuracion(string strUsuario, string strModulo, string strSubModulo)
        {
            objCnxConfiguracion = Resolver.ObtenerConexion(Define.CNX_07);
            objCnxConfiguracion.Cod_Usuario = strUsuario;
            objCnxConfiguracion.Cod_Modulo = strModulo;
            objCnxConfiguracion.Cod_Sub_Modulo = strSubModulo;
        }


        #region Parametros Generales
        public DataTable ListarAllParametroGenerales(string strParametro)
        {
            return objCnxConfiguracion.ListarAllParametroGenerales(strParametro);
        }

        public DataTable DetalleParametroGeneralxId(string sIdentificador)
        {
            return objCnxConfiguracion.DetalleParametroGeneralxId(sIdentificador);
        }


        public bool GrabarParametroGeneral(string sValoresFormulario, string sValoresGrilla, string sParametroVenta)
        {
            return objCnxConfiguracion.GrabarParametroGeneral(sValoresFormulario, sValoresGrilla, sParametroVenta);
        }


        public ParameGeneral obtenerParametroGeneral(string sCodParam)
        {
            return objCnxConfiguracion.obtenerParametroGeneral(sCodParam);
        }


        #endregion

        #region ListaDeCampo
        public bool RegistrarListaDeCampo(ListaDeCampo objListaCampo, int tipo)
        {
            return objCnxConfiguracion.RegistrarListaDeCampo(objListaCampo, tipo);
        }
        public DataTable ObtenerListaDeCampo(string strNomCampo, string strCodCampo)
        {
            DataTable dtListaDeCampo;
            dtListaDeCampo = objCnxConfiguracion.ObtenerListaDeCampo(strNomCampo, strCodCampo);
            return dtListaDeCampo;
        }

        public bool EliminarListaDeCampo(string strNomCampo, string strCodCampo)
        {
            return objCnxConfiguracion.EliminarListaDeCampo(strNomCampo, strCodCampo);
        }

        public bool actualizarHoras(ListaDeCampo objListaCampo)
        {
            return objCnxConfiguracion.actualizarHoras(objListaCampo); 
        }


        #endregion

        public bool actualizarestado(Sincronizacion objListaSincronizacion)
        {
            return objCnxConfiguracion.actualizarestado(objListaSincronizacion);
        }


    }
}
