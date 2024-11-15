using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LAP.TUUA.PRINTER
{
    public class Print
    {

        // xml
        private Xml documento;

        public Print()
        {
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

        //----------------------------- SOLO PARA IMPRESION WEB ------------------------------//

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


    }
}
