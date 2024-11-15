package com.hiper.lap.print;

import java.text.DateFormatSymbols;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Hashtable;

/**
 *
 * @author Esteban Aliaga Geldres
 */
public class Functions {

    public static String obtenerValor(Hashtable variables, String llave) {
        String valor = "";
        if (variables.containsKey(llave)) {
            Object obj = variables.get(llave);
            if (obj != null && obj instanceof String) {
                valor = (String) obj;
            }
        }
        return valor;
    }

    public static String obtenerHHMMSS() {
//        DateFormatSymbols dateFormatSymbols = new DateFormatSymbols();
//        dateFormatSymbols.setAmPmStrings(new String[]{"a.m.", "p.m."});
//        return new SimpleDateFormat("HH:mm:ss aaa", dateFormatSymbols).format(new Date());
        return new SimpleDateFormat("HH:mm:ss").format(new Date());
    }

    public static String obtenerDDMMYYYY() {
        return new SimpleDateFormat("dd/MM/yyyy").format(new Date());
    }

    public static String alinearIzquierda(Hashtable parametros, String[] lista) {
        String valor = "";

        if (lista[0].contains("DDMMYYYY()")) {
            //ejemplo 23/12/2009
            valor = obtenerDDMMYYYY();
        } else if (lista[0].contains("HHMMSS()")) {
            //ejemplo: 10:33:58
            valor = obtenerHHMMSS();
        } else {

            valor = getValor(parametros, lista[0].trim());

            //valor = obtenerValor(parametros, lista[0]);
        }

        // obtener el tamano { por ejemplo 10 }
        int tamano = Integer.parseInt(lista[1].trim());

        // si el tamano es mayor que la longitud del valor
        if (tamano > valor.length()) {
            // alinear el valor
            int length = tamano - valor.length();
            for (int i = 0; i < length; i++) {
                valor = valor + " ";
            }

        }// de lo contrario se devuelve una parte
        else {
            valor = valor.substring(0, tamano);
        }
        return valor;
    }

    public static String alinearDerecha(Hashtable parametros, String[] lista) {
        String valor = "";

        if (lista[0].contains("DDMMYYYY()")) {
            //ejemplo 23/12/2009
            valor = obtenerDDMMYYYY();
        } else if (lista[0].contains("HHMMSS()")) {
            //ejemplo: 10:33:58
            valor = obtenerHHMMSS();
        } else {

            valor = getValor(parametros, lista[0].trim());

            //valor = obtenerValor(parametros, lista[0]);
        }

        // obtener el tamano { por ejemplo 10 }
        int tamano = Integer.parseInt(lista[1].trim());

        // si el tamano es mayor que la longitud del valor
        if (tamano > valor.length()) {
            // alinear el valor
            int length = tamano - valor.length();
            for (int i = 0; i < length; i++) {
                valor = " " + valor;
            }

        }// de lo contrario se devuelve una parte
        else {
            valor = valor.substring(0, tamano);
        }
        return valor;
    }

    public static String alinearIzquierda(Hashtable parametros, String[] lista, int indice) {
        String valor = "";

        if (lista[0].contains("DDMMYYYY()")) {
            //ejemplo 23/12/2009
            valor = obtenerDDMMYYYY();
        } else if (lista[0].contains("HHMMSS()")) {
            //ejemplo: 10:33:58
            valor = obtenerHHMMSS();
        } else {
            
            valor = getValor(parametros, lista[0].trim(), indice);

            //valor = obtenerValor(parametros, lista[0] + "_" + indice);
        }

        // obtener el tamano { por ejemplo 10 }
        int tamano = Integer.parseInt(lista[1].trim());

        // si el tamano es mayor que la longitud del valor
        if (tamano > valor.length()) {
            // alinear el valor
            int length = tamano - valor.length();
            for (int i = 0; i < length; i++) {
                valor = valor + " ";
            }

        }// de lo contrario se devuelve una parte
        else {
            valor = valor.substring(0, tamano);
        }
        return valor;
    }

    public static String alinearDerecha(Hashtable parametros, String[] lista, int indice) {
        String valor = "";

        if (lista[0].contains("DDMMYYYY()")) {
            //ejemplo 23/12/2009
            valor = obtenerDDMMYYYY();
        } else if (lista[0].contains("HHMMSS()")) {
            //ejemplo: 10:33:58
            valor = obtenerHHMMSS();
        } else {
            
            valor = getValor(parametros, lista[0].trim(), indice);

            //valor = obtenerValor(parametros, lista[0] + "_" + indice);
        }

        // obtener el tamano { por ejemplo 10 }
        int tamano = Integer.parseInt(lista[1].trim());

        // si el tamano es mayor que la longitud del valor
        if (tamano > valor.length()) {
            // alinear el valor
            int length = tamano - valor.length();
            for (int i = 0; i < length; i++) {
                valor = " " + valor;
            }

        }// de lo contrario se devuelve una parte
        else {
            valor = valor.substring(0, tamano);
        }
        return valor;
    }

    public static String centrar(String cadena) {

        int longitud = (40 - cadena.length()) / 2;
        int resto = (40 - cadena.length()) % 2;

        String auxiliar = "";

        for (int i = 0; i < longitud; i++) {
            auxiliar += " ";
        }

        auxiliar += cadena;

        for (int i = 0; i < longitud; i++) {
            auxiliar += " ";
        }

        if (resto > 0) {
            for (int i = 0; i < resto; i++) {
                auxiliar += " ";
            }

        }

        return auxiliar;
    }

    private static String getValor(Hashtable parametros, String variable)
    {
        String auxiliar = "";
        if(variable.contains("=CONC("))
        {
            variable = variable.substring(6, variable.length() - 1);
            String[] lista = variable.split("\\|");
            // luego recorro cada valor de la lista
            for (String valorInlista : lista)
            {
                String valorlista = valorInlista.trim();

                // si el valor contiene el caracter "
                if (valorlista.contains("\""))
                {
                    auxiliar += valorlista.substring(1, valorlista.length() - 1);
                } // si el valor contiene la funcion ALIGNLEFT
                else if (valorlista.contains("DDMMYYYY()"))
                {
                    auxiliar += Functions.obtenerDDMMYYYY();
                } // si el valor contiene la funcion HHMMSS
                else if (valorlista.contains("HHMMSS()"))
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
            auxiliar = obtenerValor(parametros, variable);
        }
        return auxiliar;
    }

    private static String getValor(Hashtable parametros, String variable, int i)
    {
        String auxiliar = "";
        if (variable.contains("=CONC("))
        {
            variable = variable.substring(6, variable.length() - 1);
            String[] lista = variable.split("\\|");
            // luego recorro cada valor de la lista
            for (String valorInlista : lista)
            {
                String valorlista = valorInlista.trim();

                // si el valor contiene el caracter "
                if (valorlista.contains("\""))
                {
                    auxiliar += valorlista.substring(1, valorlista.length() - 1);
                } // si el valor contiene la funcion ALIGNLEFT
                else if (valorlista.contains("DDMMYYYY()"))
                {
                    auxiliar += Functions.obtenerDDMMYYYY();
                } // si el valor contiene la funcion HHMMSS
                else if (valorlista.contains("HHMMSS()"))
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
            auxiliar = obtenerValor(parametros, variable + "_" + i);
        }
        return auxiliar;
    }


}
