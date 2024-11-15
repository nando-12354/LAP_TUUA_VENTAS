using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;

namespace Control
{
    public class BO_Consultas
    {
        private DAO_ParameGeneral dao_ParameGeneral;
        public BO_Consultas()
        {
            dao_ParameGeneral = new DAO_ParameGeneral(Property.htSPConfig);
        }

        public DataTable ListarParametros(string as_identificador)
        {
            try
            {
                return dao_ParameGeneral.ObtenerParametroGeneral(as_identificador);
            }
            catch (Exception ex)
            {
                ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                ErrorHandler.Flg_Error = true;
                throw;
            }
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
                dtParametros = this.ListarParametros("PI");

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

    }
}
