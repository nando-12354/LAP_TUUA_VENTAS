package com.hiper.lap.print;

import java.util.Hashtable;
import java.util.List;
import org.jdom.Attribute;
import org.jdom.Element;

/**
 *
 * @author Esteban Aliaga Geldres
 */
public class Xml {

    public String[] obtenerDocumento(Hashtable parametros, Element nodo) {
        String[] data = null;
        try {

            List<Element> lista = nodo.getChildren();

            data = new String[lista.size()];

            for (int contador = 0, row=0; contador < lista.size(); contador++, row++) {
                // hijo title
                if (lista.get(contador).getName().equals("title")) {
                    data[row] = obtenerTitulo(parametros, lista.get(contador));
                } // hijo line
                else if (lista.get(contador).getName().equals("line")) {
                    data[row] = obtenerLinea();
                } // hijo body
                else if (lista.get(contador).getName().equals("body")) {
                    // obtener los hijos del body en una lista
                    String[] cuerpo = obtenerCuerpo(parametros, lista.get(contador));

                    String[] dataAuxiliar = new String[cuerpo.length + data.length - 1];

                    System.arraycopy(data, 0, dataAuxiliar, 0, row);
                    System.arraycopy(cuerpo, 0, dataAuxiliar, row, cuerpo.length);

                    data = dataAuxiliar;
                    row += cuerpo.length - 1;
                }
            }
        } catch (Exception ex) {
            System.err.println(ex.getMessage());
        }
        return data;
    }

    private String obtenerTitulo(Hashtable parametros, Element elemento) {
        //return Functions.centrar(elemento.getText());
        
        String valor = elemento.getText();
        String auxiliar;
        
        if (valor.contains("=CONC"))
        {
            auxiliar = "";

            valor = valor.substring(6, valor.length() - 1);

            // se obtiene una lista de valores segun el indicador ","
            //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
            String[] lista = valor.split(",");
            // luego recorro cada valor de la lista

            for (String valorInlista : lista)
            {
                String valorlista = valorInlista.trim();

                // si el valor contiene el caracter "
                if (valorlista.substring(0, 1).equals("\""))
                {
                    auxiliar += valorlista.substring(1, valorlista.length() - 1);
                } // si el valor contiene la funcion ALIGNLEFT
                else if (valorlista.contains("ALIGNLEFT"))
                {
                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                    String auxiliar3 = valorlista.substring(10, valorlista.length() - 1);
                    String[] listaAux = auxiliar3.split(";");
                    auxiliar += Functions.alinearIzquierda(parametros, listaAux);

                } // si el valor contiene la funcion ALIGNRIGHT
                else if (valorlista.contains("ALIGNRIGHT"))
                {
                    // se obtiene el valor y el indicador { por ejemplo listaAux}
                    String auxiliar3 = valorlista.substring(11, valorlista.length() - 1);
                    String[] listaAux = auxiliar3.split(";");
                    auxiliar += Functions.alinearDerecha(parametros, listaAux);
                } // si el valor contiene la funcion DDMMYYYY
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
            auxiliar = valor;
        }

        return Functions.centrar(auxiliar);
    }

    private String obtenerLinea() {
        return " ";
    }

    private String[] obtenerCuerpo(Hashtable parametros, Element elemento) {
        String[] cuerpo = null;
        
        try {
            // se obtienen los hijos del nodo body
            List<Element> detalles = elemento.getChildren();

            // se crea una lista para guardar los datos de los hijos de body
            cuerpo = new String[detalles.size()];

            // recorrer cada hijo del nodo body
            String auxiliar;
            for (int contador = 0, row=0; contador < detalles.size(); contador++, row++) {

                // <editor-fold defaultstate="collapsed" desc="Condicion para ver si imprimio la linea">
                //EAG 16/02/2010 ---> Condicion para ver si imprimio esta linea
                Attribute condicion = detalles.get(contador).getAttribute("condicion");
                if(condicion!=null)
                {
                    String condicional = condicion.getValue();
                    String[] condArray = condicional.split(",");
                    if(condArray.length==3)
                    {
                        String campo = condArray[0].trim();
                        String cond = condArray[1].trim();
                        String valor = condArray[2].trim();

                        boolean res = evaluarCondicion(campo, cond, valor, parametros);
                        if(!res)
                        {
                            //--EAG 29/01/2010 Se debe de disminuir en uno el detalles.Count
                            String[] dataAuxiliar = new String[cuerpo.length - 1];
                            System.arraycopy(cuerpo, 0, dataAuxiliar, 0, row);

                            cuerpo = dataAuxiliar;
                            row = row - 1;
                            //
                            continue;
                        }
                    }
                }
                // </editor-fold>

                auxiliar = "";

                // <editor-fold defaultstate="collapsed" desc="detail">
                if (detalles.get(contador).getName().equals("detail")) {
                    // se obtiene el valor de detail
                    String valor = detalles.get(contador).getText();

                    // si detail no tiene ningun valor
                    if (valor.equals("")) {
                        auxiliar = "";
                    } else {
                        // si el valor contiene un caracter = quiere decir que es una funcion
                        if (valor.contains("=")) {
                            // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                            valor = valor.substring(1, valor.length());

                            // si la funcion es CONC
                            if (valor.contains("CONC")) {
                                // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                valor = valor.substring(5, valor.length() - 1);

                                // se obtiene una lista de valores segun el indicador ","
                                //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                String[] lista = valor.split(",");

                                String auxiliar3;
                                String[] listaAux;
                                // luego recorro cada valor de la lista
                                for (String valorInlista : lista) {

                                    String valorlista = valorInlista.trim();

                                    // si el valor contiene el caracter "
                                    if (valorlista.substring(0, 1).equals("\"")) {
                                        auxiliar += valorlista.substring(1, valorlista.length() - 1);

                                    } // si el valor contiene la funcion ALIGNLEFT
                                    else if (valorlista.contains("ALIGNLEFT")) {
                                        // se obtiene el valor y el indicador { por ejemplo listaAux}
                                        auxiliar3 = valorlista.substring(10, valorlista.length() - 1);
                                        listaAux = auxiliar3.split(";");
                                        auxiliar += Functions.alinearIzquierda(parametros, listaAux);

                                    } // si el valor contiene la funcion ALIGNRIGHT
                                    else if (valorlista.contains("ALIGNRIGHT")) {
                                        // se obtiene el valor y el indicador { por ejemplo listaAux}
                                        auxiliar3 = valorlista.substring(11, valorlista.length() - 1);
                                        listaAux = auxiliar3.split(";");
                                        auxiliar += Functions.alinearDerecha(parametros, listaAux);

                                    } // si el valor contiene la funcion DDMMYYYY
                                    else if (valorlista.contains("DDMMYYYY()")) {
                                        auxiliar += Functions.obtenerDDMMYYYY();
                                    } // si el valor contiene la funcion HHMMSS
                                    else if (valorlista.contains("HHMMSS()")) {
                                        auxiliar += Functions.obtenerHHMMSS();

                                    } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                    else {
                                        auxiliar += Functions.obtenerValor(parametros, valorlista);
                                    }
                                }
                            } // si la funcion es DDMMYYYY
                            else if (valor.contains("DDMMYYYY")) {
                                auxiliar = Functions.obtenerDDMMYYYY();
                            } // si la funcion es HHMMSS
                            else if (valor.contains("HHMMSS")) {
                                auxiliar = Functions.obtenerHHMMSS();
                            }
                        }
                        else
                        {
                            auxiliar = valor;
                        }
                    }
                }
                // </editor-fold>
                // <editor-fold defaultstate="collapsed" desc="line">
                else if (detalles.get(contador).getName().equals("line")) {
                    auxiliar = obtenerLinea();
                }
                // </editor-fold>
                // <editor-fold defaultstate="collapsed" desc="title">
                else if (detalles.get(contador).getName().equals("title")) {
                    auxiliar = obtenerTitulo(parametros, detalles.get(contador));
                }
                // </editor-fold>
                // <editor-fold defaultstate="collapsed" desc="subdetail">
                else if (detalles.get(contador).getName().equals("subdetail")) {
                    String[] subdetalle = null;

                    // se obtiene el valor del nodo
                    String valor = detalles.get(contador).getText();

                    // obtener el numero de tickets
                    int cantidad = Integer.parseInt(Functions.obtenerValor(parametros, Defines.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL));

                    subdetalle = new String[cantidad];
                    // recorrer cada iteracion y buscar las funciones en cada elemento de la lista
                    for (int i = 0; i < cantidad; i++) {
                        auxiliar = "";

                        // <editor-fold defaultstate="collapsed" desc="recorro cada iteracion">
                        // si el nodo no tiene ningun valor
                        if (valor.equals("")) {
                            auxiliar = "";
                        } else {
                            // si el valor contiene un caracter = quiere decir que es una funcion
                            if (valor.contains("=")) {
                                // recorro 1 posicion { por ejemplo CONC("Cajero :",codigo_cajero,DDMMYYYY() }
                                String valorAuxiliar = valor.substring(1, valor.length());
                                // si la funcion es CONC
                                if (valorAuxiliar.contains("CONC")) {
                                    // recorro 5 posiciones { por ejemplo "Cajero :",codigo_cajero,DDMMYYYY() }
                                    valorAuxiliar = valor.substring(6, valor.length() - 1);

                                    // se obtiene una lista de valores
                                    //{ por ejemplo lista[0]="Cajero :" lista[1]=codigo_cajero lista[2]=DDMMYYYY() }
                                    String[] lista = valorAuxiliar.split(",");

                                    // luego recorro cada valor de la lista
                                    for (String valorInlista : lista) {

                                        String valorlista = valorInlista.trim();

                                        // si el valor contiene el caracter "
                                        if (valorlista.substring(0, 1).equals("\"")) {
                                            auxiliar += valorlista.substring(1, valorlista.length() - 1);

                                        } // si el valor contiene la funcion ALIGNLEFT
                                        else if (valorlista.contains("ALIGNLEFT")) {
                                            // se obtiene el valor y el indicador { por ejemplo listaAux}
                                            String auxiliar3 = valorlista.substring(10, valorlista.length() - 1);
                                            String[] listaAux = auxiliar3.split(";");
                                            auxiliar += Functions.alinearIzquierda(parametros, listaAux, i);

                                        } // si el valor contiene la funcion ALIGNRIGHT
                                        else if (valorlista.contains("ALIGNRIGHT")) {
                                            // se obtiene el valor y el indicador { por ejemplo listaAux}
                                            String auxiliar3 = valorlista.substring(11, valorlista.length() - 1);
                                            String[] listaAux = auxiliar3.split(";");
                                            auxiliar += Functions.alinearDerecha(parametros, listaAux, i);

                                        } // si el valor contiene la variable { por ejemplo codigo_cajero }
                                        else {
                                            auxiliar += Functions.obtenerValor(parametros, valorlista + "_" + i);
                                        }
                                    }
                                } // si la funcion es DDMMYYYY
                                else if (valor.contains("DDMMYYYY")) {
                                    auxiliar = Functions.obtenerDDMMYYYY();
                                } // si la funcion es HHMMSS
                                else if (valor.contains("HHMMSS")) {
                                    auxiliar = Functions.obtenerHHMMSS();
                                }
                            }
                        }
                        // </editor-fold>
                        
                        // agregar sub detalle
                        subdetalle[i] = auxiliar;
                    }

                    String[] dataAuxiliar = new String[subdetalle.length + cuerpo.length - 1];
                    System.arraycopy(cuerpo, 0, dataAuxiliar, 0, row);
                    System.arraycopy(subdetalle, 0, dataAuxiliar, row, subdetalle.length);

                    cuerpo = dataAuxiliar;
                    row += cantidad - 1;//o row += subdetalle.length - 1;
                }
                // </editor-fold>

                if (!detalles.get(contador).getName().equals("subdetail")) {//Aunque da igual si pasa adentro si es subdetail...
                    cuerpo[row] = auxiliar;
                }
            }

        } catch (Exception ex) {
            System.err.println("Error en obtenerCuerpo() - Error: " + ex.getMessage());
            ex.printStackTrace();
        }

        return cuerpo;

    }

    private boolean evaluarCondicion(String campo, String cond, String valor, Hashtable parametros)
    {
        campo = Functions.obtenerValor(parametros, campo).toString();
        int iCond = GetSignoComparacion(cond);
        if(valor.substring(0,1).equals("#"))
        {
            valor = valor.substring(1);
        }
        else
        {
            valor = Functions.obtenerValor(parametros, valor).toString();
        }
        return GetResult(campo, valor, iCond);
    }

    public int GetSignoComparacion(String szSigno) {
        if (szSigno.compareToIgnoreCase(Defines.EQ) == 0) {
            return 0;
        }
        if (szSigno.compareToIgnoreCase(Defines.NE) == 0) {
            return 1;
        }
        if (szSigno.compareToIgnoreCase(Defines.LE) == 0) {
            return 2;
        }
        if (szSigno.compareToIgnoreCase(Defines.GE) == 0) {
            return 3;
        }
        if (szSigno.compareToIgnoreCase(Defines.LT) == 0) {
            return 4;
        }
        if (szSigno.compareToIgnoreCase(Defines.GT) == 0) {
            return 5;
        }
        return -1;
    }

    public boolean GetResult(String szCampo, String szValor, int iValor) {

        switch (iValor) {
            case 0:
                return (szCampo.compareTo(szValor) == 0);
            case 1:
                return (szCampo.compareTo(szValor) != 0);
            case 2:
                return (szCampo.compareTo(szValor) <= 0);
            case 3:
                return (szCampo.compareTo(szValor) >= 0);
            case 4:
                return (szCampo.compareTo(szValor) < 0);
            case 5:
                return (szCampo.compareTo(szValor) > 0);
        }
        return false;
    }

}
