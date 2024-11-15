using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using LAP.TUUA.UTIL;

namespace LAP.TUUA.PRINTER
{
    public class Print
    {

        // comunicacion
        private PuertoSerial comPort;
        // xml
        private Xml documento;

        public Print()
        {
            // comunicacion
            comPort = new PuertoSerial();

            // xml
            documento = new Xml();
        }

        public XmlElement ObtenerNodo(XmlDocument xml, string nombre)
        {
            return documento.ObtenerNodo(xml, nombre);
        }

        public string ObtenerConfiguracionImpresora(XmlElement nodo, Hashtable listaParamConfig, string nombre)
        {
            return documento.ObtenerConfiguracionImpresora(nodo, listaParamConfig, nombre);
        }

        //----------------------------- SOLO PARA IMPRESION DESKTOP ------------------------------//

        public string[] ObtenerData(Hashtable parametros, XmlElement nodo)
        {
            return documento.obtenerDocumento(parametros, nodo);
        }

        public void configurarImpresora(string configuracion)
        {
            // configurar impresora
            comPort.Configurar(configuracion);
        }

        public int verificarEstadoImpresora(int tipo)
        {
            int estado = 0;
            try
            {
                // verificar estado segun el tipo de impresora
                switch (tipo)
                {
                    case 0:    // voucher
                        estado = comPort.VerEstadoImpresora(tipo, char.ConvertFromUtf32(16) + char.ConvertFromUtf32(4) + char.ConvertFromUtf32(4));
                        break;
                    case 1:     // sticker
                        estado = comPort.VerEstadoImpresora(tipo, "~HS");
                        break;
                }

            }
            catch (Exception e)
            {

            }
            return estado;
        }

        public int imprimir(int tipo, string[] data)
        {
            int estado = 0;
            try
            {
                estado = comPort.EnviarData(tipo, data);
            }
            catch (Exception e)
            {

            }
            return estado;
        }

        //----------------------------- SOLO PARA IMPRESION WEB ------------------------------//

        #region No se usa
        //public string ObtenerConfiguracionImpresoraDefault(Hashtable listaParamConfig, string nombre)
        //{
        //    return documento.ObtenerConfiguracionImpresoraDefault(listaParamConfig, nombre);
        //}
        #endregion

        public string ObtenerDataFormateada(Hashtable parametros, XmlElement nodo)
        {
            string[] data = documento.obtenerDocumento(parametros, nodo);

            return formatearData(parametros, data);
        }

        private string formatearData(Hashtable parametros, string[] data)
        {
            string cadena = "";

            for (int i = 0; i < data.Length; i++)
            {
                cadena += data[i] + "@";
            }

            //La funcion getParameter de Java, hace un trim() implicito, entonces tengo que añadir un @ al final.
            //Ademas el ultimo @, no afecta, pues el split de Java es distinto al split de C#.

            //if (cadena.Length > 0)
            //{
            //    cadena = cadena.Substring(0, cadena.Length - 1);
            //}

            return "@" + cadena;//Le agrego @ pues el getParameter de Java hace trim y borra los caracteres vacios al inicio.(ejem si es un titulo es centrado)
        }

        public int ObtenerCopiasVoucher(XmlElement nodo)
        {
            return documento.ObtenerCopiasVoucher(nodo);
        }


        #region No se usa
        /*

        /// <summary>
        /// Metodo que obtiene el nombre de la operacion a realizar
        /// <param name="vez">flag que indica si se imprimira un ticket=0 o un voucher=1</param>
        /// </summary>
        public string obtenerOperacion(int vez, Hashtable listaParamConfig,string strMoneda)
        {
            string codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETVOUCHER;

            // recorrer la lista parametros de configuracion para obtener la lista de los codigos de monedas
            string[] listaCodigoMonedas = null;
            IDictionaryEnumerator iteraccion = listaParamConfig.GetEnumerator();
            while (iteraccion.MoveNext())
            {
                if (iteraccion.Key.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                {
                    listaCodigoMonedas = ((string)iteraccion.Value).Split(',');
                    break;
                }
            }
            if (listaCodigoMonedas == null || listaCodigoMonedas.Length == 0)
            {
                throw new Exception("La lista de Parametros Configuración no tiene las lista de Codigos Monedas.");
            }

            // buscar el codigo de moneda elegido por el usuario
            int indice;
            for (int i = 0; i < listaCodigoMonedas.Length; i++)
            {
                indice = listaCodigoMonedas[i].IndexOf('-');
                if (listaCodigoMonedas[i].Substring(indice + 1, listaCodigoMonedas[i].Length - indice - 1).Equals(strMoneda))
                {
                    // obtener llave 
                    string llave = listaCodigoMonedas[i].Substring(0, indice);
                    if (llave.Equals("0"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKERSOLES;
                        }
                    }
                    else if (llave.Equals("1"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKERDOLARES;
                        }
                    }
                    else if (llave.Equals("2"))
                    {
                        if (vez == 0)
                        {
                            codigoMoneda = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKEREUROS;
                        }
                    }
                    break;
                }
            }

            return codigoMoneda;
        }

        /// <summary>
        /// Metodo que obtiene el nombre de la operacion a realizar
        /// <param name="listaParamConfig">Lista parametros de cponfiguracion</param>
        /// </summary>
        public string[] obtenerListaCodigoMoneda(Hashtable listaParamConfig)
        {
            string[] listaCodigoMonedas = null;

            // recorrer la lista parametros de configuracion para obtener la lista de los codigos de monedas
            IDictionaryEnumerator iteraccion = listaParamConfig.GetEnumerator();
            while (iteraccion.MoveNext())
            {
                if (iteraccion.Key.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                {
                    listaCodigoMonedas = ((string)iteraccion.Value).Split(',');
                    break;
                }
            }
            if (listaCodigoMonedas == null || listaCodigoMonedas.Length == 0)
            {
                throw new Exception("La lista de Parametros Configuración no tiene las lista de Codigos Monedas.");
            }

            return listaCodigoMonedas;
        }

        */
        #endregion


    }
}
