using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

using LAP.TUUA.CONEXION;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.RESOLVER;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.CONTROL
{
    public class BO_ParameGeneral
    {
        private Conexion objConexion;
        protected ParameGeneral objEmpresa;

        /// <summary>
        /// Constructor de la clase BO_ParameGeneral. 
        /// Inicializa la conexion a la base de datos.
        /// </summary>
        public BO_ParameGeneral()
        {
            objConexion = Resolver.ObtenerConexion(Define.CNX_01);
        }
        //Este método recién lo agregué
        public ParameGeneral ObjEmpresa
        {
            get { return objEmpresa; }
            set { objEmpresa = value; }
        }

        /// <summary>
        /// Obtiene los Parametros de configuracion que se usaran en la impresion.
        /// </summary>
        /// <param name="listaParamConfig">lista parametros de configuracion</param>
        /// <param name="xml">xml</param>
        public void ObtenerParametrosImpresion(Hashtable listaParamConfig, XmlDocument xml)
        {
            DataTable dtParametros = new DataTable();
            try
            {
                // obtener parametros de configuracion
                // por ejemplo voucher=COM1,9600,N,8,1;sticker=COM3,9600,N,8,1;codMoneda=0-SOL,1-DOl,2-EUR
                dtParametros = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_IMPRESION]);

                // impresora y codigo de moneda
                // por ejemplo voucher=COM1,9600,N,8,1;sticker=COM3,9600,N,8,1;codMoneda=0-SOL,1-DOl,2-EUR
                object parametros = dtParametros.Rows[0].ItemArray.GetValue(4);
                if (parametros == null || parametros.Equals(""))
                {
                    throw new Exception("No existen parámetros de configuración para la impresión.");
                }
                // obtener la lista
                Function.ObtenerLista(listaParamConfig, parametros.ToString().Split(';'));

                // xml
                parametros = dtParametros.Rows[0].ItemArray.GetValue(10);
                if (parametros == null || parametros.Equals(""))
                {
                    throw new Exception("No existe archivo xml para la impresión.");
                }
                xml.LoadXml(parametros.ToString());

            }
            catch (Exception e)
            {
                listaParamConfig.Clear();
                throw e;
            }

        }

        public DataTable ListarAllParametroGenerales()
        {
            return objConexion.ListarAllParametroGenerales(null);
        }

        public DataTable ListarParametros(string sIdentificador)
        {
            return objConexion.ListarParametros(sIdentificador);
        }
        public DataTable ObtenerEmpresaPorIdentificadorPadre(string identificador)
        {
            return objConexion.ObtenerEmpresaPorIdentificadorPadre(identificador);
        }
    }
}
