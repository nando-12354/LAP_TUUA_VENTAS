using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace LAP.TUUA.PRINTER
{
    public class Xml
    {
        public Xml()
        {
        }

        #region ObtenerNodo
        public XmlElement ObtenerNodo(XmlDocument xml, string nombre)
        {
            XmlElement nodo = null;
            // validar si el xml tiene nodos hijos
            if (xml.HasChildNodes)
            {
                // buscar el nombre en los nodos "document"
                XmlNodeList documentos = xml.GetElementsByTagName("document");
                foreach (XmlElement documento in documentos)
                {
                    if (documento.GetAttribute("name").Equals(nombre))
                    {
                        nodo = documento;
                        break;
                    }
                }
                // se valida el nodo encontrado
                if (nodo == null || nodo.Equals(""))
                {
                    throw new Exception("El archivo xml no tiene el nodo " + nombre);
                }
            }
            else
            {
                throw new Exception("Formato xml inválido.");
            }
            return nodo;
        }
        #endregion

        #region ObtenerCopiasVoucher - Solo para Modulo Web
        public int ObtenerCopiasVoucher(XmlElement nodo)
        {
            int copias = 0;
            if(nodo.HasAttribute("copias"))
            {
                String valor = nodo.GetAttribute("copias");
                try
                {
                    copias = Int32.Parse(valor);
                }
                catch(Exception ex)
                {
                    copias = 0;
                }
            }
            return copias;
        }
        #endregion

        /// <summary>
        /// Metodo que agrega la configuracion de la impresora a la lista de parametros a imprimir.
        /// </summary>
        public string ObtenerConfiguracionImpresora(XmlElement nodo, Hashtable listaParamConfig, string nombreNodo)
        {
            string configuracion = "";

            // validar el atributo print
            if (nodo.HasAttribute("print"))
            {
                // obtener valor del atributo
                string valor = nodo.GetAttribute("print");
                if (valor == null || valor.Equals(""))
                {
                    throw new Exception("El atributo print del nodo " + nombreNodo + " no tiene valor.");

                }

                // si encontro el valor del atributo buscarlo en la lista 
                IDictionaryEnumerator iteraccion = listaParamConfig.GetEnumerator();
                while (iteraccion.MoveNext())
                {
                    if (iteraccion.Key.Equals(valor))
                    {
                        if (iteraccion.Value == null || iteraccion.Value.Equals(""))
                        {
                            throw new Exception("La llave " + valor + " no tiene valor.");
                        }
                        else
                        {
                            configuracion = (string)iteraccion.Value;
                            break;
                        }
                    }
                }
            }
            else
            {
                throw new Exception("El nodo " + nombreNodo + " no tiene el atributo print.");
            }
            return configuracion;
        }

        #region No se usa
        //public string ObtenerConfiguracionImpresoraDefault(Hashtable listaParamConfig, string nombre)
        //{
        //    string configuracion = "";

        //    IDictionaryEnumerator iteraccion = listaParamConfig.GetEnumerator();
        //    while (iteraccion.MoveNext())
        //    {
        //        if (iteraccion.Key.Equals(nombre))
        //        {
        //            configuracion = (string)iteraccion.Value;
        //            break;
        //        }
        //    }
        //    return configuracion;
        //}
        #endregion

        public string[] obtenerDocumento(Hashtable parametros, XmlElement nodo)
        {
            string[] data = null;

            try
            {
                // se obtiene los hijos del nodo
                XmlNodeList lista = nodo.ChildNodes;

                // se crea una lista teniendo en cuenta el numero de hijos del nodo
                data = new string[lista.Count];

                // recorrer cada hijo del nodo y guardar su valor
                for (int contador = 0, row=0; contador < lista.Count; contador++, row++)
                {
                    // hijo title
                    if (lista[contador].Name.Equals("title"))
                    {
                        data[row] = obtenerTitulo(parametros, (XmlElement)lista[contador]);
                    }
                    // hijo line
                    else if (lista[contador].Name.Equals("line"))
                    {
                        data[row] = obtenerLinea();
                    }
                    // hijo body
                    else if (lista[contador].Name.Equals("body"))
                    {
                        // obtener los hijos del body en una lista
                        string[] cuerpo = obtenerCuerpo(parametros, (XmlElement)lista[contador]);

                        string[] dataAuxiliar = new string[cuerpo.Length + data.Length - 1];
                        Array.Copy(data, 0, dataAuxiliar, 0, row);
                        Array.Copy(cuerpo, 0, dataAuxiliar, row, cuerpo.Length);

                        data = dataAuxiliar;
                        row += cuerpo.Length - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        private string obtenerTitulo(Hashtable parametros, XmlElement elemento)
        {
            String valor = elemento.InnerText;
            String auxiliar;

            if (valor.Contains("=CONC"))
            {
                auxiliar = "";

                valor = valor.Substring(6, valor.Length - 7);

                // se obtiene una lista de valores segun el indicador ","
                //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                string[] lista = valor.Split(',');
                // luego recorro cada valor de la lista

                foreach (string valorInlista in lista)
                {
                    String valorlista = valorInlista.Trim();

                    // si el valor contiene el caracter "
                    if (valorlista.Substring(0, 1).Equals("\""))
                    {
                        auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                    } // si el valor contiene la funcion ALIGNLEFT
                    else if (valorlista.Contains("ALIGNLEFT"))
                    {
                        // se obtiene el valor y el indicador { por ejemplo listaAux}
                        string auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                        string[] listaAux = auxiliar3.Split(';');
                        auxiliar += Functions.alinearIzquierda(parametros, listaAux);

                    } // si el valor contiene la funcion ALIGNRIGHT
                    else if (valorlista.Contains("ALIGNRIGHT"))
                    {
                        // se obtiene el valor y el indicador { por ejemplo listaAux}
                        string auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                        string[] listaAux = auxiliar3.Split(';');
                        auxiliar += Functions.alinearDerecha(parametros, listaAux);
                    } // si el valor contiene la funcion DDMMYYYY
                    else if (valorlista.Contains("DDMMYYYY()"))
                    {
                        auxiliar += Functions.obtenerDDMMYYYY();
                    } // si el valor contiene la funcion HHMMSS
                    else if (valorlista.Contains("HHMMSS()"))
                    {
                        auxiliar += Functions.obtenerHHMMSS();
                    } // si el valor contiene la variable { por ejemplo codigo_cajero }
                    else
                    {
                        auxiliar += Functions.obtenerValor(parametros, valorlista);
                    }
                }
            }
            else
            {
                auxiliar = valor;
            }


            return Functions.centrar(auxiliar);
        }

        private string obtenerLinea()
        {
            return " ";
        }

        #region obtenerCuerpo_
        private string[] obtenerCuerpo_(Hashtable parametros, XmlElement elemento)
        {
            string[] cuerpo = null;
            string[] subdetalle = null;
            string auxiliar = "";
            int contadorAuxiliar = 0;
            try
            {

                // si contiene la variable flag_ticket se asume que el body tiene dos details
                if (parametros.ContainsKey(Defines.ID_PRINTER_PARAM_FLAG_TICKET))
                {
                    // se lee los hijos del nodo body
                    XmlNodeList detalles = elemento.ChildNodes;
                    string auxiliar2;
                    string[] detalles2 = new string[detalles.Count];
                    for (int contador = 0; contador < detalles.Count; contador++)
                    {
                        auxiliar2 = "";
                        // nodo detail
                        if (detalles[contador].Name.Equals("detail"))
                        {
                            // se obtiene el valor del nodo
                            string valor = detalles[contador].InnerText;

                            // si el nodo no tiene ningun valor
                            if (valor.Equals(""))
                            {
                                auxiliar2 += "";
                            }
                            else
                            {
                                // si el valor contiene un caracter = quiere decir que es una funcion
                                if (valor.Contains("="))
                                {

                                    // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                    valor = valor.Substring(1, valor.Length - 1);
                                    // si la funcion es CONC
                                    if (valor.Contains("CONC"))
                                    {
                                        // recorro 6 posiciones { por ejemplo "^XA^MD10^XFR:HIPERSOL.ZPL^FS^FN1^FDVálido hasta el ",DDMMYYYY(),"^FS^FN2^FD>;",codigo_ticket,"^FS^FN3^FD",monto_soles,"^FS^FN4^FDNUEVOS^FS^FN5^FDSOLES^FS^XZ")
                                        valor = valor.Substring(5, valor.Length - 5 - 1);

                                        // se obtiene una lista de valores 
                                        //{ por ejemplo lista[0]="^XA^MD10^XFR:HIPERSOL.ZPL^FS^FN1^FDVálido hasta el " lista[1]=DDMMYYYY() lista[2]=^FS^FN2^FD>; lista[3]=codigo_ticket }
                                        string[] lista = valor.Split(',');

                                        // obtener el numero de tickets
                                        int cantidad = Convert.ToInt32(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET));

                                        cuerpo = new string[cantidad];

                                        // recorrer cada ticket y buscar las funciones en cada elemento de la lista
                                        for (int i = 0; i < cantidad; i++)
                                        {
                                            auxiliar = "";
                                            // recorrer y buscar las funciones en cada elemento de la lista
                                            for (int j = 0; j < lista.Length; j++)
                                            {

                                                if (lista[j].Equals(Defines.ID_PRINTER_PARAM_FECHA_VENCIMIENTO))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_PAGADO))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_PAGADO + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_SOLES))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_SOLES + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_DOLARES))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_DOLARES + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_EUROS))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_EUROS + "_" + i);

                                                }
                                                else
                                                {
                                                    // quitamos la comilla de adelante y la de atras
                                                    auxiliar += lista[j].Substring(1, lista[j].Length - 2);
                                                }

                                            }
                                            cuerpo[i] = auxiliar;
                                        }

                                    }
                                    else
                                    {

                                        detalles2[contador] = valor;

                                    }

                                }
                            }

                        }
                    }

                    string[] dataAuxiliar2 = new string[detalles2.Length - 1 + cuerpo.Length];

                    Array.Copy(detalles2, 0, dataAuxiliar2, 0, detalles2.Length - 1);
                    Array.Copy(cuerpo, 0, dataAuxiliar2, detalles2.Length - 1, cuerpo.Length);

                    cuerpo = null;

                    cuerpo = dataAuxiliar2;
                       
                }
                // si no contiene la variable flag_ticket 
                else
                {

                    // se obtienen los hijos del nodo body
                    XmlNodeList detalles = elemento.ChildNodes;

                    // se crea una lista para guardar los datos de los hijos de body
                    cuerpo = new string[detalles.Count];

                    // recorrer cada hijo del nodo body
                    string auxiliar2;
                    for (int contador = 0; contador < detalles.Count; contador++)
                    {
                        auxiliar2 = "";
                        // hijo detail
                        if (detalles[contador].Name.Equals("detail"))
                        {
                            // se obtiene el valor de detail
                            string valor = detalles[contador].InnerText;

                            // si detail no tiene ningun valor
                            if (valor.Equals(""))
                            {
                                auxiliar2 += "";
                            }
                            else
                            {
                                // si el valor contiene un caracter = quiere decir que es una funcion
                                if (valor.Contains("="))
                                {
                                    // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                    valor = valor.Substring(1, valor.Length - 1);

                                    // si la funcion es CONC
                                    if (valor.Contains("CONC"))
                                    {
                                        // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                        valor = valor.Substring(5, valor.Length - 6);

                                        // se obtiene una lista de valores segun el indicador ","
                                        //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                        string[] lista = valor.Split(',');

                                        string auxiliar3;
                                        string[] listaAux;
                                        // luego recorro cada valor de la lista
                                        foreach (string valorlista in lista)
                                        {

                                            // si el valor contiene el caracter "
                                            if (valorlista.Contains("\""))
                                            {
                                                auxiliar2 += valorlista.Substring(1, valorlista.Length - 2);

                                            } // si el valor contiene la funcion ALIGNLEFT
                                            else if (valorlista.Contains("ALIGNLEFT"))
                                            {
                                                // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                                listaAux = auxiliar3.Split(';');
                                                auxiliar2 += Functions.alinearIzquierda(parametros, listaAux);

                                            } // si el valor contiene la funcion ALIGNRIGHT
                                            else if (valorlista.Contains("ALIGNRIGHT"))
                                            {
                                                // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                                listaAux = auxiliar3.Split(';');
                                                auxiliar2 += Functions.alinearDerecha(parametros, listaAux);

                                            } // si el valor contiene la funcion DDMMYYYY
                                            else if (valorlista.Contains("DDMMYYYY()"))
                                            {
                                                auxiliar2 += Functions.obtenerDDMMYYYY();
                                            } // si el valor contiene la funcion HHMMSS
                                            else if (valorlista.Contains("HHMMSS()"))
                                            {
                                                auxiliar2 += Functions.obtenerHHMMSS();

                                            } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                            else
                                            {
                                                if (valorlista.Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                {

                                                }
                                                else
                                                {
                                                    auxiliar2 += Functions.obtenerValor(parametros, valor);

                                                }

                                            }

                                        }
                                    } // si la funcion es DDMMYYYY
                                    else if (valor.Contains("DDMMYYYY"))
                                    {
                                        auxiliar2 += Functions.obtenerDDMMYYYY();
                                    } // si la funcion es HHMMSS
                                    else if (valor.Contains("HHMMSS"))
                                    {
                                        auxiliar2 += Functions.obtenerHHMMSS();
                                    }
                                }
                            }
                        }
                        else if (detalles[contador].Name.Equals("line"))
                        {
                            auxiliar2 += "";

                        }
                        else if (detalles[contador].Name.Equals("subdetail"))
                        {
                            // 
                            contadorAuxiliar = contador;

                            // se obtiene el valor del nodo
                            string valor = detalles[contador].InnerText;

                            // obtener el numero de tickets
                            int cantidad = Convert.ToInt32(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET));

                            subdetalle = new string[cantidad];
                            // recorrer cada ticket y buscar las funciones en cada elemento de la lista
                            for (int i = 0; i < cantidad; i++)
                            {
                                auxiliar2 = "";

                                // si el nodo no tiene ningun valor
                                if (valor.Equals(""))
                                {
                                    auxiliar2 += "";
                                }
                                else
                                {
                                    // si el valor contiene un caracter = quiere decir que es una funcion
                                    if (valor.Contains("="))
                                    {
                                        // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                        string valorAuxiliar = valor.Substring(1, valor.Length - 1);
                                        // si la funcion es CONC
                                        if (valorAuxiliar.Contains("CONC"))
                                        {
                                            // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                            valorAuxiliar = valor.Substring(6, valor.Length - 7);

                                            // se obtiene una lista de valores 
                                            //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                            string[] lista = valorAuxiliar.Split(',');

                                            string auxiliar3;
                                            string[] listaAux;
                                            // luego recorro cada valor de la lista
                                            foreach (string valorlista in lista)
                                            {

                                                // si el valor contiene el caracter "
                                                if (valorlista.Contains("\""))
                                                {
                                                    auxiliar2 += valorlista.Substring(1, valorlista.Length - 2);

                                                } // si el valor contiene la funcion ALIGNLEFT
                                                else if (valorlista.Contains("ALIGNLEFT"))
                                                {
                                                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                    auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                                    listaAux = auxiliar3.Split(';');
                                                    auxiliar2 += Functions.alinearIzquierda(parametros, listaAux, i);

                                                } // si el valor contiene la funcion ALIGNRIGHT
                                                else if (valorlista.Contains("ALIGNRIGHT"))
                                                {
                                                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                    auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                                    listaAux = auxiliar3.Split(';');
                                                    auxiliar2 += Functions.alinearDerecha(parametros, listaAux, i);

                                                } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                                else
                                                {
                                                    if (valorlista.Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                    {

                                                    }
                                                    else
                                                    {
                                                        auxiliar2 += Functions.obtenerValor(parametros, valor);

                                                    }

                                                }

                                            }
                                        } // si la funcion es DDMMYYYY
                                        else if (valor.Contains("DDMMYYYY"))
                                        {
                                            auxiliar2 += Functions.obtenerDDMMYYYY();
                                        } // si la funcion es HHMMSS
                                        else if (valor.Contains("HHMMSS"))
                                        {
                                            auxiliar2 += Functions.obtenerHHMMSS();
                                        }
                                    }
                                }
                                // agregar sub detalle
                                subdetalle[i] = auxiliar2;

                            }

                        }

                        cuerpo[contador] = auxiliar + auxiliar2;
                    }
                
                }




            }
            catch (Exception e)
            {

            }

            return cuerpo;
        }
        #endregion


        #region _obtenerCuerpo
        private string[] _obtenerCuerpo(Hashtable parametros, XmlElement elemento)
        {
            string[] cuerpo = null;

            try
            {
                #region "Para Ticket"
                // si contiene la variable flag_ticket se asume que el body tiene dos details
                if (parametros.ContainsKey(Defines.ID_PRINTER_PARAM_FLAG_TICKET))
                {
                    string auxiliar = "";

                    // se lee los hijos del nodo body
                    XmlNodeList detalles = elemento.ChildNodes;
                    string auxiliar2;
                    string[] detalles2 = new string[detalles.Count];
                    for (int contador = 0; contador < detalles.Count; contador++)
                    {
                        auxiliar2 = "";
                        // nodo detail
                        if (detalles[contador].Name.Equals("detail"))
                        {
                            // se obtiene el valor del nodo
                            string valor = detalles[contador].InnerText;

                            // si el nodo no tiene ningun valor
                            if (valor.Equals(""))
                            {
                                auxiliar2 += "";
                            }
                            else
                            {
                                // si el valor contiene un caracter = quiere decir que es una funcion
                                if (valor.Contains("="))
                                {

                                    // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                    valor = valor.Substring(1, valor.Length - 1);
                                    // si la funcion es CONC
                                    if (valor.Contains("CONC"))
                                    {
                                        // recorro 6 posiciones { por ejemplo "^XA^MD10^XFR:HIPERSOL.ZPL^FS^FN1^FDVálido hasta el ",DDMMYYYY(),"^FS^FN2^FD>;",codigo_ticket,"^FS^FN3^FD",monto_soles,"^FS^FN4^FDNUEVOS^FS^FN5^FDSOLES^FS^XZ")
                                        valor = valor.Substring(5, valor.Length - 5 - 1);

                                        // se obtiene una lista de valores 
                                        //{ por ejemplo lista[0]="^XA^MD10^XFR:HIPERSOL.ZPL^FS^FN1^FDVálido hasta el " lista[1]=DDMMYYYY() lista[2]=^FS^FN2^FD>; lista[3]=codigo_ticket }
                                        string[] lista = valor.Split(',');

                                        // obtener el numero de tickets
                                        int cantidad = Convert.ToInt32(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET));

                                        cuerpo = new string[cantidad];

                                        // recorrer cada ticket y buscar las funciones en cada elemento de la lista
                                        for (int i = 0; i < cantidad; i++)
                                        {
                                            auxiliar = "";
                                            // recorrer y buscar las funciones en cada elemento de la lista
                                            for (int j = 0; j < lista.Length; j++)
                                            {

                                                if (lista[j].Equals(Defines.ID_PRINTER_PARAM_FECHA_VENCIMIENTO))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_PAGADO))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_PAGADO + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_SOLES))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_SOLES + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_DOLARES))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_DOLARES + "_" + i);

                                                }
                                                else if (lista[j].Equals(Defines.ID_PRINTER_PARAM_MONTO_EUROS))
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_MONTO_EUROS + "_" + i);

                                                }
                                                else
                                                {
                                                    // quitamos la comilla de adelante y la de atras
                                                    auxiliar += lista[j].Substring(1, lista[j].Length - 2);
                                                }

                                            }
                                            cuerpo[i] = auxiliar;
                                        }

                                    }
                                    else
                                    {

                                        detalles2[contador] = valor;

                                    }

                                }
                            }

                        }
                    }

                    string[] dataAuxiliar = new string[detalles2.Length - 1 + cuerpo.Length];

                    Array.Copy(detalles2, 0, dataAuxiliar, 0, detalles2.Length - 1);
                    Array.Copy(cuerpo, 0, dataAuxiliar, detalles2.Length - 1, cuerpo.Length);

                    cuerpo = null;

                    cuerpo = dataAuxiliar;

                }
                #endregion
                #region "Para Voucher"
                // si no contiene la variable flag_ticket 
                else
                {
                    // se obtienen los hijos del nodo body
                    XmlNodeList detalles = elemento.ChildNodes;

                    // se crea una lista para guardar los datos de los hijos de body
                    cuerpo = new string[detalles.Count];

                    // recorrer cada hijo del nodo body
                    string auxiliar;
                    for (int contador = 0, row=0; contador < detalles.Count; contador++, row++)
                    {
                        auxiliar = "";

                        #region "detail"
                        if (detalles[contador].Name.Equals("detail"))
                        {
                            // se obtiene el valor de detail
                            string valor = detalles[contador].InnerText;

                            // si detail no tiene ningun valor
                            if (valor.Equals(""))
                            {
                                auxiliar = "";
                            }
                            else
                            {
                                // si el valor contiene un caracter = quiere decir que es una funcion
                                if (valor.Contains("="))
                                {
                                    // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                    valor = valor.Substring(1, valor.Length - 1);

                                    // si la funcion es CONC
                                    if (valor.Contains("CONC"))
                                    {
                                        // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                        valor = valor.Substring(5, valor.Length - 6);

                                        // se obtiene una lista de valores segun el indicador ","
                                        //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                        string[] lista = valor.Split(',');
                                        // luego recorro cada valor de la lista
                                        foreach (string valorlista in lista)
                                        {
                                            // si el valor contiene el caracter "
                                            if (valorlista.Contains("\""))
                                            {
                                                auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                                            } // si el valor contiene la funcion ALIGNLEFT
                                            else if (valorlista.Contains("ALIGNLEFT"))
                                            {
                                                // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                string auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                                string[] listaAux = auxiliar3.Split(';');
                                                auxiliar += Functions.alinearIzquierda(parametros, listaAux);

                                            } // si el valor contiene la funcion ALIGNRIGHT
                                            else if (valorlista.Contains("ALIGNRIGHT"))
                                            {
                                                // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                string auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                                string[] listaAux = auxiliar3.Split(';');
                                                auxiliar += Functions.alinearDerecha(parametros, listaAux);
                                            } // si el valor contiene la funcion DDMMYYYY
                                            else if (valorlista.Contains("DDMMYYYY()"))
                                            {
                                                auxiliar += Functions.obtenerDDMMYYYY();
                                            } // si el valor contiene la funcion HHMMSS
                                            else if (valorlista.Contains("HHMMSS()"))
                                            {
                                                auxiliar += Functions.obtenerHHMMSS();
                                            } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                            else
                                            {
                                                if (valorlista.Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                {
                                                }
                                                else
                                                {
                                                    auxiliar += Functions.obtenerValor(parametros, valor);
                                                }
                                            }
                                        }
                                    } // si la funcion es DDMMYYYY
                                    else if (valor.Contains("DDMMYYYY"))
                                    {
                                        auxiliar = Functions.obtenerDDMMYYYY();
                                    } // si la funcion es HHMMSS
                                    else if (valor.Contains("HHMMSS"))
                                    {
                                        auxiliar = Functions.obtenerHHMMSS();
                                    }
                                }
                            }
                        }
                        #endregion
                        #region "line"
                        else if (detalles[contador].Name.Equals("line"))
                        {
                            auxiliar = obtenerLinea();

                        }
                        #endregion
                        #region "subdetail"
                        else if (detalles[contador].Name.Equals("subdetail"))
                        {
                            string[] subdetalle = null;

                            // se obtiene el valor del nodo
                            string valor = detalles[contador].InnerText;

                            // obtener el numero de tickets
                            int cantidad = Convert.ToInt32(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_TICKET));

                            subdetalle = new string[cantidad];
                            // recorrer cada ticket y buscar las funciones en cada elemento de la lista
                            for (int i = 0; i < cantidad; i++)
                            {
                                auxiliar = "";

                                #region "recorro cada ticket"
                                // si el nodo no tiene ningun valor
                                if (valor.Equals(""))
                                {
                                    auxiliar = "";
                                }
                                else
                                {
                                    // si el valor contiene un caracter = quiere decir que es una funcion
                                    if (valor.Contains("="))
                                    {
                                        // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                        string valorAuxiliar = valor.Substring(1, valor.Length - 1);
                                        // si la funcion es CONC
                                        if (valorAuxiliar.Contains("CONC"))
                                        {
                                            // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                            valorAuxiliar = valor.Substring(6, valor.Length - 7);
                                            // se obtiene una lista de valores 
                                            //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                            string[] lista = valorAuxiliar.Split(',');
                                            // luego recorro cada valor de la lista
                                            foreach (string valorlista in lista)
                                            {
                                                // si el valor contiene el caracter "
                                                if (valorlista.Contains("\""))
                                                {
                                                    auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                                                } // si el valor contiene la funcion ALIGNLEFT
                                                else if (valorlista.Contains("ALIGNLEFT"))
                                                {
                                                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                    string auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                                    string[] listaAux = auxiliar3.Split(';');
                                                    auxiliar += Functions.alinearIzquierda(parametros, listaAux, i);
                                                } // si el valor contiene la funcion ALIGNRIGHT
                                                else if (valorlista.Contains("ALIGNRIGHT"))
                                                {
                                                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                                                    string auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                                    string[] listaAux = auxiliar3.Split(';');
                                                    auxiliar += Functions.alinearDerecha(parametros, listaAux, i);
                                                } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                                else
                                                {
                                                    if (valorlista.Equals(Defines.ID_PRINTER_PARAM_CODIGO_TICKET))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        auxiliar += Functions.obtenerValor(parametros, valor);
                                                    }
                                                }
                                            }
                                        } // si la funcion es DDMMYYYY
                                        else if (valor.Contains("DDMMYYYY"))
                                        {
                                            auxiliar = Functions.obtenerDDMMYYYY();
                                        } // si la funcion es HHMMSS
                                        else if (valor.Contains("HHMMSS"))
                                        {
                                            auxiliar = Functions.obtenerHHMMSS();
                                        }
                                    }
                                }
                                #endregion

                                // agregar sub detalle
                                subdetalle[i] = auxiliar;
                            }

                            string[] dataAuxiliar = new string[subdetalle.Length + cuerpo.Length - 1];
                            Array.Copy(cuerpo, 0, dataAuxiliar, 0, row);
                            Array.Copy(subdetalle, 0, dataAuxiliar, row, subdetalle.Length);

                            cuerpo = dataAuxiliar;
                            row += cantidad - 1;//o row += subdetalle.Length - 1;
                        }
                        #endregion

                        if (!detalles[contador].Name.Equals("subdetail"))//Aunque da igual si pasa adentro si es subdetail...
                        {
                            cuerpo[row] = auxiliar;
                        }
                    }

                }
                #endregion

            }
            catch (Exception e)
            {

            }

            return cuerpo;
        }
        #endregion


        private string[] obtenerCuerpo(Hashtable parametros, XmlElement elemento)
        {
            string[] cuerpo = null;

            try
            {
                // se obtienen los hijos del nodo body
                XmlNodeList detalles = elemento.ChildNodes;

                // se crea una lista para guardar los datos de los hijos de body
                cuerpo = new string[detalles.Count];

                // recorrer cada hijo del nodo body
                string auxiliar;
                for (int contador = 0, row = 0; contador < detalles.Count; contador++, row++)
                {

                    #region Condicion para ver si imprimio la linea
                    //EAG 29/01/2010 ---> Condicion para ver si imprimio esta linea
                    XmlAttribute condicion = detalles[contador].Attributes["condicion"];
                    if(condicion!=null)
                    {
                        String condicional = condicion.Value;
                        String[] condArray = condicional.Split(',');
                        if(condArray.Length==3)
                        {
                            String campo = condArray[0].Trim();
                            String cond = condArray[1].Trim();
                            String valor = condArray[2].Trim();

                            bool res = evaluarCondicion(campo, cond, valor, parametros);
                            if(!res)
                            {
                                //--EAG 29/01/2010 Se debe de disminuir en uno el detalles.Count
                                string[] dataAuxiliar = new string[cuerpo.Length - 1];
                                Array.Copy(cuerpo, 0, dataAuxiliar, 0, row);

                                cuerpo = dataAuxiliar;
                                row = row - 1;
                                //
                                continue;
                            }
                        }
                    }
                    //EAG 29/01/2010
                    #endregion

                    auxiliar = "";

                    #region "detail"
                    if (detalles[contador].Name.Equals("detail"))
                    {
                        // se obtiene el valor de detail
                        string valor = detalles[contador].InnerText;

                        // si detail no tiene ningun valor
                        if (valor.Equals(""))
                        {
                            auxiliar = "";
                        }
                        else
                        {
                            // si el valor contiene un caracter = quiere decir que es una funcion
                            if (valor.Contains("="))
                            {
                                // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                valor = valor.Substring(1, valor.Length - 1);

                                // si la funcion es CONC
                                if (valor.Contains("CONC"))
                                {
                                    // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                    valor = valor.Substring(5, valor.Length - 6);

                                    // se obtiene una lista de valores segun el indicador ","
                                    //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                    string[] lista = valor.Split(',');
                                    // luego recorro cada valor de la lista
                                    foreach (string valorInlista in lista)
                                    {
                                        String valorlista = valorInlista.Trim();

                                        // si el valor contiene el caracter "
                                        if (valorlista.Substring(0, 1).Equals("\""))
                                        {
                                            auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                                        } // si el valor contiene la funcion ALIGNLEFT
                                        else if (valorlista.Contains("ALIGNLEFT"))
                                        {
                                            // se obtiene el valor y el indicador { por ejemplo listaAux}
                                            string auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                            string[] listaAux = auxiliar3.Split(';');
                                            auxiliar += Functions.alinearIzquierda(parametros, listaAux);

                                        } // si el valor contiene la funcion ALIGNRIGHT
                                        else if (valorlista.Contains("ALIGNRIGHT"))
                                        {
                                            // se obtiene el valor y el indicador { por ejemplo listaAux}
                                            string auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                            string[] listaAux = auxiliar3.Split(';');
                                            auxiliar += Functions.alinearDerecha(parametros, listaAux);
                                        } // si el valor contiene la funcion DDMMYYYY
                                        else if (valorlista.Contains("DDMMYYYY()"))
                                        {
                                            auxiliar += Functions.obtenerDDMMYYYY();
                                        } // si el valor contiene la funcion HHMMSS
                                        else if (valorlista.Contains("HHMMSS()"))
                                        {
                                            auxiliar += Functions.obtenerHHMMSS();
                                        } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                        else
                                        {
                                            auxiliar += Functions.obtenerValor(parametros, valorlista);
                                        }
                                    }
                                } // si la funcion es DDMMYYYY
                                else if (valor.Contains("DDMMYYYY"))
                                {
                                    auxiliar = Functions.obtenerDDMMYYYY();
                                } // si la funcion es HHMMSS
                                else if (valor.Contains("HHMMSS"))
                                {
                                    auxiliar = Functions.obtenerHHMMSS();
                                }
                            }
                            else
                            {
                                auxiliar = valor;
                            }
                        }
                    }
                    #endregion
                    #region "line"
                    else if (detalles[contador].Name.Equals("line"))
                    {
                        auxiliar = obtenerLinea();

                    }
                    #endregion
                    #region "title"
                    else if (detalles[contador].Name.Equals("title"))
                    {
                        auxiliar = obtenerTitulo(parametros, (XmlElement)detalles[contador]);

                    }
                    #endregion
                    #region "subdetail"
                    else if (detalles[contador].Name.Equals("subdetail"))
                    {
                        string[] subdetalle = null;

                        // se obtiene el valor del nodo
                        string valor = detalles[contador].InnerText;

                        // obtener el numero de iteraciones para el subdetail
                        int cantidad = Convert.ToInt32(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL));

                        subdetalle = new string[cantidad];

                        // recorrer cada iteracion y buscar las funciones en cada elemento de la lista
                        for (int i = 0; i < cantidad; i++)
                        {
                            auxiliar = "";

                            #region "recorro cada iteracion"
                            // si el nodo no tiene ningun valor
                            if (valor.Equals(""))
                            {
                                auxiliar = "";
                            }
                            else
                            {
                                // si el valor contiene un caracter = quiere decir que es una funcion
                                if (valor.Contains("="))
                                {
                                    // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                    string valorAuxiliar = valor.Substring(1, valor.Length - 1);
                                    // si la funcion es CONC
                                    if (valorAuxiliar.Contains("CONC"))
                                    {
                                        // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                        valorAuxiliar = valor.Substring(6, valor.Length - 7);
                                        // se obtiene una lista de valores 
                                        //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                        string[] lista = valorAuxiliar.Split(',');
                                        // luego recorro cada valor de la lista
                                        foreach (string valorInlista in lista)
                                        {
                                            String valorlista = valorInlista.Trim();

                                            // si el valor contiene el caracter "
                                            if (valorlista.Substring(0, 1).Equals("\""))
                                            {
                                                auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                                            } // si el valor contiene la funcion ALIGNLEFT
                                            else if (valorlista.Contains("ALIGNLEFT"))
                                            {
                                                string auxiliar3 = valorlista.Substring(10, valorlista.Length - 11);
                                                string[] listaAux = auxiliar3.Split(';');
                                                auxiliar += Functions.alinearIzquierda(parametros, listaAux, i);
                                            } // si el valor contiene la funcion ALIGNRIGHT
                                            else if (valorlista.Contains("ALIGNRIGHT"))
                                            {
                                                string auxiliar3 = valorlista.Substring(11, valorlista.Length - 12);
                                                string[] listaAux = auxiliar3.Split(';');
                                                auxiliar += Functions.alinearDerecha(parametros, listaAux, i);
                                            } // si el valor contiene la variable { por ejemplo codigo_ticket }
                                            else
                                            {
                                                auxiliar += Functions.obtenerValor(parametros, valorlista + "_" + i);
                                            }
                                        }
                                    } // si la funcion es DDMMYYYY
                                    else if (valor.Contains("DDMMYYYY"))
                                    {
                                        auxiliar = Functions.obtenerDDMMYYYY();
                                    } // si la funcion es HHMMSS
                                    else if (valor.Contains("HHMMSS"))
                                    {
                                        auxiliar = Functions.obtenerHHMMSS();
                                    }
                                }
                            }
                            #endregion

                            // agregar sub detalle
                            subdetalle[i] = auxiliar;
                        }

                        string[] dataAuxiliar = new string[subdetalle.Length + cuerpo.Length - 1];
                        Array.Copy(cuerpo, 0, dataAuxiliar, 0, row);
                        Array.Copy(subdetalle, 0, dataAuxiliar, row, subdetalle.Length);

                        cuerpo = dataAuxiliar;
                        row += cantidad - 1;//o row += subdetalle.Length - 1;
                    }
                    #endregion

                    if (!detalles[contador].Name.Equals("subdetail"))//Aunque da igual si pasa adentro si es subdetail...
                    {
                        cuerpo[row] = auxiliar;
                    }
                }
            }
            catch (Exception e)
            {

            }

            return cuerpo;
        }

        private bool evaluarCondicion(String campo, String cond, String valor, Hashtable parametros)
        {
            campo = Functions.obtenerValor(parametros, campo).ToString(); 
            int iCond = GetSignoComparacion(cond);
            if(valor.Substring(0,1).Equals("#"))
            {
                valor = valor.Substring(1);
            }
            else
            {
                valor = Functions.obtenerValor(parametros, valor).ToString(); 
            }
            return GetResult(campo, valor, iCond);
        }

        public int GetSignoComparacion(String signo)
        {
            if (signo.ToUpper().CompareTo(Defines.EQ.ToUpper()) == 0)
                return 0;
            if (signo.ToUpper().CompareTo(Defines.NE.ToUpper()) == 0)
                return 1;
            if (signo.ToUpper().CompareTo(Defines.LE.ToUpper()) == 0)
                return 2;
            if (signo.ToUpper().CompareTo(Defines.GE.ToUpper()) == 0)
                return 3;
            if (signo.ToUpper().CompareTo(Defines.LT.ToUpper()) == 0)
                return 4;
            if (signo.ToUpper().CompareTo(Defines.GT.ToUpper()) == 0)
                return 5;
            return -1;
        }

        public bool GetResult(String szCampo, String szValor, int iValor)
        {
            switch (iValor)
            {
                case 0: return (szCampo.CompareTo(szValor) == 0);
                case 1: return (szCampo.CompareTo(szValor) != 0);
                case 2: return (szCampo.CompareTo(szValor) <= 0);
                case 3: return (szCampo.CompareTo(szValor) >= 0);
                case 4: return (szCampo.CompareTo(szValor) < 0);
                case 5: return (szCampo.CompareTo(szValor) > 0);
            }
            return false;
        }



    }
}
