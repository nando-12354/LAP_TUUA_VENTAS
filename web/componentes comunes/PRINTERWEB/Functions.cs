using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using LAP.TUUA.UTIL;


namespace LAP.TUUA.PRINTER
{
    public class Functions
    {
        public static Hashtable impresoras;

        public static object obtenerValor(Hashtable variables, string llave)
        {
            object valor = null;
            try
            {

                if (variables.ContainsKey(llave))
                {
                    IDictionaryEnumerator iteraccion = variables.GetEnumerator();
                    while (iteraccion.MoveNext())
                    {
                        if (iteraccion.Key.Equals(llave))
                        {
                            if (iteraccion.Value == null || iteraccion.Value.Equals(""))
                            {
                                throw new Exception("La llave tiene valor " + iteraccion.Value);
                            }
                            else
                            {
                                valor = iteraccion.Value;
                                break;
                            }
                        }
                    }

                }
                else
                {
                    valor = "";
                }

            }
            catch (Exception e)
            {
                valor = "";
            }

            return valor;

        }


        public static string obtenerHHMMSS()
        {

            //return DateTime.Now.ToString("HH:mm:ss tt", new CultureInfo("es-PE", true));
            return DateTime.Now.ToString("HH:mm:ss", new CultureInfo("es-PE", true));
        }

        public static string obtenerDDMMYYYY()
        {

            return DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("es-PE", true));

        }

        public static string alinearIzquierda(Hashtable parametros, string[] lista)
        {
            string valor = "";

            if (lista[0].Contains("DDMMYYYY()"))
            {
                // obtener el valor { por ejemplo 10/09/2009 }
                valor = obtenerDDMMYYYY();
            }
            else if (lista[0].Contains("HHMMSS()"))
            {
                // obtener el valor { por ejemplo 10:15:58 }
                valor = obtenerHHMMSS();
            }
            else
            {
                valor = getValor(parametros, lista[0].Trim());

                /*
                // obtener el valor { por ejemplo 000002 }
                object aux = obtenerValor(parametros, lista[0]);
                if (aux is string)
                {
                    valor = (string)aux;
                }
                else if (aux is decimal)
                {
                    valor = Convert.ToString(aux);
                }
                */
            }

            // obtener el tamano { por ejemplo 10 }
            int tamano = Convert.ToInt32(lista[1].Trim());

            // si el tamano es mayor que la longitud del valor
            if (tamano > valor.Length)
            {
                // alinear el valor 
                int length = tamano - valor.Length;
                for (int i = 0; i < length; i++)
                {
                    valor = valor + " ";
                }

            }// de lo contrario se devuelve una parte 
            else
            {
                valor = valor.Substring(0, tamano);
            }
            return valor;
        }

        public static string alinearIzquierda(Hashtable parametros, string[] lista, int indice)
        {
            string valor = "";
            if (lista[0].Contains("DDMMYYYY()"))
            {
                // obtener el valor { por ejemplo 10/09/2009 }
                valor = obtenerDDMMYYYY();
            }
            else if (lista[0].Contains("HHMMSS()"))
            {
                // obtener el valor { por ejemplo 10:15:58 }
                valor = obtenerHHMMSS();
            }
            else
            {
                valor = getValor(parametros, lista[0].Trim(), indice);

                /*
                // obtener el valor { por ejemplo 000002 }
                valor = (string)obtenerValor(parametros, lista[0] + "_" + indice);
                */
            }

            // obtener el tamano { por ejemplo 10 }
            int tamano = Convert.ToInt32(lista[1].Trim());

            // si el tamano es mayor que la longitud del valor
            if (tamano > valor.Length)
            {
                // alinear el valor 
                int length = tamano - valor.Length;
                for (int i = 0; i < length; i++)
                {
                    valor = valor + " ";
                }

            }// de lo contrario se devuelve una parte 
            else
            {
                valor = valor.Substring(0, tamano);
            }
            return valor;
        }


        public static string alinearDerecha(Hashtable parametros, string[] lista)
        {
            string valor = "";

            if (lista[0].Contains("DDMMYYYY()"))
            {
                // obtener el valor { por ejemplo 10/09/2009 }
                valor = obtenerDDMMYYYY();

            }
            else if (lista[0].Contains("HHMMSS()"))
            {
                // obtener el valor { por ejemplo 10:15:58 }
                valor = obtenerHHMMSS();

            }
            else
            {
                valor = getValor(parametros, lista[0].Trim());

                /*
                // obtener el valor { por ejemplo 000002 }
                object aux = obtenerValor(parametros, lista[0]);
                if (aux is string)
                {
                    valor = (string)aux;

                }
                else if (aux is decimal)
                {
                    valor = Convert.ToString(aux);

                }
                */
            }

            // obtener el tamano { por ejemplo 10 }
            int tamano = Convert.ToInt32(lista[1].Trim());

            // si el tamano es mayor que la longitud del valor
            if (tamano > valor.Length)
            {
                // alinear el valor 
                int length = tamano - valor.Length;
                for (int i = 0; i < length; i++)
                {
                    valor = " " + valor;
                }

            }// de lo contrario se devuelve una parte 
            else
            {
                valor = valor.Substring(0, tamano);
            }

            return valor;
        }

        public static string alinearDerecha(Hashtable parametros, string[] lista, int indice)
        {
            string valor = "";

            if (lista[0].Contains("DDMMYYYY()"))
            {
                // obtener el valor { por ejemplo 10/09/2009 }
                valor = obtenerDDMMYYYY();

            }
            else if (lista[0].Contains("HHMMSS()"))
            {
                // obtener el valor { por ejemplo 10:15:58 }
                valor = obtenerHHMMSS();

            }
            else
            {
                valor = getValor(parametros, lista[0].Trim(), indice);

                /*
                // obtener el valor { por ejemplo 000002 }
                object aux = obtenerValor(parametros, lista[0] + "_" + indice);
                if (aux is string)
                {
                    valor = (string)aux;
                }
                else if (aux is decimal)
                {
                    valor = Convert.ToString(aux);
                }
                */

            }

            // obtener el tamano { por ejemplo 10 }
            int tamano = Convert.ToInt32(lista[1].Trim());

            // si el tamano es mayor que la longitud del valor
            if (tamano > valor.Length)
            {
                // alinear el valor 
                int length = tamano - valor.Length;
                for (int i = 0; i < length; i++)
                {
                    valor = " " + valor;
                }

            }// de lo contrario se devuelve una parte 
            else
            {
                valor = valor.Substring(0, tamano);
            }

            return valor;
        }


        public static string centrar(string cadena)
        {

            int longitud = (40 - cadena.Length) / 2;
            int resto = (40 - cadena.Length) % 2;

            string auxiliar = "";

            for (int i = 0; i < longitud; i++)
            {
                auxiliar += " ";
            }

            auxiliar += cadena;

            for (int i = 0; i < longitud; i++)
            {
                auxiliar += " ";
            }

            if (resto > 0)
            {
                for (int i = 0; i < resto; i++)
                {
                    auxiliar += " ";
                }

            }

            return auxiliar;
        }

        private static string getValor(Hashtable parametros, String variable)
        {
            string auxiliar = "";
            if(variable.Contains("=CONC("))
            {
                variable = variable.Substring(6, variable.Length - 7);
                string[] lista = variable.Split('|');
                // luego recorro cada valor de la lista
                foreach (string valorInlista in lista)
                {
                    String valorlista = valorInlista.Trim();

                    // si el valor contiene el caracter "
                    if (valorlista.Contains("\""))
                    {
                        auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                    } // si el valor contiene la funcion ALIGNLEFT
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
                auxiliar = Convert.ToString(obtenerValor(parametros, variable));
            }
            return auxiliar;
        }

        private static string getValor(Hashtable parametros, String variable, int i)
        {
            string auxiliar = "";
            if (variable.Contains("=CONC("))
            {
                variable = variable.Substring(6, variable.Length - 7);
                string[] lista = variable.Split('|');
                // luego recorro cada valor de la lista
                foreach (string valorInlista in lista)
                {
                    String valorlista = valorInlista.Trim();

                    // si el valor contiene el caracter "
                    if (valorlista.Contains("\""))
                    {
                        auxiliar += valorlista.Substring(1, valorlista.Length - 2);
                    } // si el valor contiene la funcion ALIGNLEFT
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
                        auxiliar += Functions.obtenerValor(parametros, valorlista + "_" + i);
                    }
                }

            }
            else
            {
                auxiliar = Convert.ToString(obtenerValor(parametros, variable + "_" + i));
            }
            return auxiliar;
        }


    }
}
