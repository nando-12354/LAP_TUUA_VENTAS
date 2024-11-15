using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Collections;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.UTIL
{
    public class Function
    {
        public static decimal FormatDecimal(decimal decNumero, int intCantDecimal)
        {
            return decimal.Round(decNumero, intCantDecimal);
        }

        public static String ConvertirDosDigitos(int intValor)
        {
            String strRpta = "";
            if (intValor < 10)
                strRpta = "0" + intValor.ToString();
            else
                strRpta = intValor.ToString();

            return strRpta;
        }

        //parametro DD/MM/YYYY
        //Formatear a YYYYMMDD
        public static string FormatStringDate(string strFecha)
        {
            if (strFecha != null && strFecha.Trim() != "")
            {
                return strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
            }
            return null;
        }

        //parametro YYYYMMDD
        public static DateTime ConvertToDateTime(string strFecha)
        {
            DateTime dtFecha = new DateTime(Int32.Parse(strFecha.Substring(0, 4)), Int32.Parse(strFecha.Substring(4, 2)), Int32.Parse(strFecha.Substring(6, 2)));
            return dtFecha;
        }

        /// <summary>
        /// Formatea el numero con dos decimales.
        /// </summary>
        /// <param name="numero"> Numero decimal a formatear</param>
        public static string FormatDecimal(decimal numero)
        {
            return String.Format("{0:0.00}", numero);
        }

        /// <summary>
        /// Formatea la fecha al formato DD/MM/YYYY
        /// </summary>
        /// <param name="fecha">Fecha en el formato YYYYMMDD</param>
        public static string FormatFecha(string fecha)
        {
            return fecha.Substring(6, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4);
        }


        /// <summary>
        /// Obtiene y valida cada elemento de la lista parametros de configuracion.
        /// </summary>
        /// <param name="listaParamConfig">
        /// Lista parametros de configuracion.</param>
        /// <param name="lista">
        /// Contiene la lista parametros de configuracion.
        /// por ejemplo voucher=COM3,9600,N,8,1;sticker=COM1,9600,N,8,1;codMoneda=0-SOL,1-DOL,2-EUR</param>
        public static void ObtenerLista(Hashtable listaParamConfig, string[] lista)
        {
            string llave;
            string valor;

            // validar la lista de parametros
            if (lista.Length < 2)
            {
                throw new Exception("La lista de parámetros no contiene todas las llaves.");
            }
            else
            {
                // recorrer cada elemento de la lista
                for (int i = 0; i < lista.Length; i++)
                {
                    // obtener la llave
                    int indice = lista[i].IndexOf("=");

                    // validar si cumple con el formato
                    if (indice != -1)
                    {
                        // obtener la llave
                        llave = lista[i].Substring(0, indice);

                        // validar el valor de cada llave
                        if (llave.Equals(Define.ID_PRINTER_KEY_VOUCHER) || llave.Equals(Define.ID_PRINTER_KEY_STICKER))
                        {
                            // obtener el valor de la llave
                            valor = lista[i].Substring(indice + 1, lista[i].Length - indice - 1);

                            // validar [ por ejemplo voucher=COM3,9600,N,8,1 ] 
                            if (valor.Split(',').Length != 5)
                            {
                                throw new Exception("La cadena " + lista[i] + " no cumple con el formato.");
                            }
                        }
                        else if (llave.Equals(Define.ID_PRINTER_KEY_CODMONEDA))
                        {
                            // obtener el valor de la llave
                            valor = lista[i].Substring(indice + 1, lista[i].Length - indice - 1);

                            // validar [ por ejemplo codMoneda=0-SOL,1-DOL,2-EUR ]
                            if (valor.Split(',').Length == 0)
                            {
                                throw new Exception("La cadena " + lista[i] + " no cumple con el formato.");
                            }
                            for (int j = 0; j < valor.Split(',').Length; j++)
                            {
                                if (!valor.Split(',')[j].Contains("-"))
                                {
                                    throw new Exception("La cadena " + lista[i] + " no cumple con el formato.");

                                }
                            }
                        }
                        else
                        {

                            throw new Exception("La llave " + llave + " no esta en la lista de parametros.");

                        }
                        listaParamConfig.Add(llave, valor);

                    }
                    else
                    {
                        throw new Exception("La cadena " + lista[i] + " no cumple con el formato.");

                    }

                }
            }
        }





        /// <summary>
        /// Convierte fecha jualiana a fecha calendario
        /// </summary>
        /// <param name="jd"></param>
        /// <returns></returns>

        public static String ConvertirJulianoCalendario(int jd)
        {
            try
            {
                string strRpta = "";
                int intAnio = DateTime.Today.Year;
                Hashtable htMes = new Hashtable();

                htMes.Add("1", 31);
                if ((intAnio % 4) == 0)
                    htMes.Add("2", 29);
                else
                    htMes.Add("2", 28);
                htMes.Add("3", 31);
                htMes.Add("4", 30);
                htMes.Add("5", 31);
                htMes.Add("6", 30);
                htMes.Add("7", 31);
                htMes.Add("8", 31);
                htMes.Add("9", 30);
                htMes.Add("10", 31);
                htMes.Add("11", 30);
                htMes.Add("12", 31);

                int intMes = 0;
                int intDia = 0;
                do
                {
                    intMes++;
                    intDia = jd;
                    jd = jd - (int)htMes[intMes.ToString()];
                } while (jd > 0);

                strRpta = DateTime.Today.Year.ToString() + ConvertirDosDigitos(intMes) +
                         ConvertirDosDigitos(intDia);
                return strRpta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static String GetAbbreviatedMonthName(String cultureName, int mm)
        {
            CultureInfo ci = new CultureInfo(cultureName);// en-US / es-PE
            DateTimeFormatInfo dtfi = ci.DateTimeFormat;
            if (mm > 0 && mm < 13)
            {
                return dtfi.AbbreviatedMonthGenitiveNames[mm - 1].ToUpper();
            }
            return "";

            //switch (mm)
            //{
            //    case 1: return "ENE";
            //    case 2: return "FEB";
            //    case 3: return "MAR";
            //    case 4: return "ABR";
            //    case 5: return "MAY";
            //    case 6: return "JUN";
            //    case 7: return "JUL";
            //    case 8: return "AGO";
            //    case 9: return "SET";
            //    case 10: return "OCT";
            //    case 11: return "NOV";
            //    case 12: return "DIC";
            //    default: return "";
            //}

        }

        public static void RehacerVoucherTuua(Hashtable htParametro, int intTicket, String listaCodigoTickets)
        {
            decimal decVueltoTotNac;
            decimal decVueltoInter;
            decimal decVueltNac;
            decimal decEgresoNac;
            string Imp_VueltoIzq="";
            string Imp_VueltoDer="";
            decimal decTotAPagar;
            decimal decTCPagado = decimal.Parse(htParametro["Imp_TCPagado"].ToString());
            TipoTicket objTipoTicket = (TipoTicket)htParametro["objTipoTicket"];
            decimal decTasaTuua = decimal.Parse(htParametro["Imp_TasaCambio"].ToString());
            string strMonNac = (string)Property.htProperty[Define.MONEDANAC];
            decimal decMonto = decimal.Parse(htParametro["Imp_MontoPagado"].ToString());
            string strMonPago = (string)htParametro["Mon_Pagado"];
            List<Cambio> listaCambio = new List<Cambio>();
            decimal decMontoSol = decimal.Parse(htParametro["Imp_MontoSol"].ToString());
            decimal decNacional;
            string strTipPago = (string)htParametro["Tip_Pago"];
            int intTotalTicket = Int32.Parse(htParametro["Can_Tickets_Ini"].ToString());
            string strDscMonPago = (string)htParametro["Dsc_MonPago"];
            string strMonSimbolo = (string)htParametro["Dsc_MonSimbolo"];

            if (strMonPago == objTipoTicket.SCodMoneda && strMonPago != strMonNac)
            {
                //parche de ultimo momento-errror en tasa cambio compra
                decimal decTCParche = decimal.Parse(htParametro["Imp_TCParche"].ToString());
                if (strTipPago == Define.TIP_PAGO_EFECTIVO)
                {
                    decNacional = decTCParche * decMonto - decTCParche * objTipoTicket.DImpPrecio * intTicket;
                    decVueltoTotNac = decNacional + decMontoSol;
                    if (decMontoSol == 0)
                    {
                        decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional / decTCParche), Define.NUM_DECIMAL);
                    }
                    else
                    {
                        decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional < 0 ? 0 : decNacional / decTCParche), Define.NUM_DECIMAL);
                    }
                    decVueltNac = decVueltoTotNac - (decVueltoInter * decTCParche);
                    decEgresoNac = Function.FormatDecimal(decVueltNac * Define.FACTOR_DECIMAL, 2);

                    Imp_VueltoIzq = decVueltoInter == 0 ? "0.00" : (decimal.Round(decVueltoInter * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL)).ToString();
                    Imp_VueltoDer = decEgresoNac == 0 ? "0.00" : decEgresoNac.ToString();
                }
            }
            else
            {
                if (strTipPago == Define.TIP_PAGO_EFECTIVO)
                {
                    if (strMonNac != strMonPago)
                    {
                        if (decMontoSol == 0)
                        {
                            decVueltoTotNac = (decTCPagado * decMonto) - (decTasaTuua * objTipoTicket.DImpPrecio * intTicket);
                            decVueltoInter = Function.FormatDecimal(decimal.Truncate(decVueltoTotNac / decTCPagado), Define.NUM_DECIMAL);
                        }
                        else
                        {
                            decNacional = decTCPagado * decMonto - decTasaTuua * objTipoTicket.DImpPrecio * intTicket;
                            decVueltoTotNac = decNacional + decMontoSol;
                            decVueltoInter = Function.FormatDecimal(decimal.Truncate(decNacional < 0 ? 0 : decNacional / decTCPagado), Define.NUM_DECIMAL);
                        }
                        decVueltNac = decVueltoTotNac - (decVueltoInter * decTCPagado);

                        decEgresoNac = Function.FormatDecimal(decVueltNac * Define.FACTOR_DECIMAL, 2);
                        Imp_VueltoIzq = decVueltoInter == 0 ? "0.00" : (decVueltoInter * Define.FACTOR_DECIMAL).ToString();
                        Imp_VueltoDer = decEgresoNac == 0 ? "0.00" : decEgresoNac.ToString();
                    }
                    else
                    {
                        decVueltoTotNac = (decMonto - (decTasaTuua * objTipoTicket.DImpPrecio * intTicket)) * Define.FACTOR_DECIMAL;

                        decimal decVueltoIzq = Function.FormatDecimal(decVueltoTotNac, Define.NUM_DECIMAL);
                        Imp_VueltoIzq = decVueltoIzq == 0 ? "0.00" : decVueltoIzq.ToString();
                        Imp_VueltoDer = "0.00";
                    }
                }
                else
                {
                    Imp_VueltoIzq = "0.00";
                    Imp_VueltoDer = "0.00";
                }
            }
            decTotAPagar = objTipoTicket.DImpPrecio * intTicket;
            //// cantidad de tickets vendidos
            htParametro.Remove(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET);
            htParametro.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, intTicket.ToString());

            //// total a pagar
            htParametro.Remove(Define.ID_PRINTER_PARAM_TOTAL_PAGAR);
            htParametro.Add(Define.ID_PRINTER_PARAM_TOTAL_PAGAR, decTotAPagar.ToString());// + " " + objTipoTicket.Dsc_Simbolo);

            htParametro.Remove("vuelto_Nac");
            htParametro.Remove("vuelto_Int");
            if (strMonNac.Trim().ToUpper().Equals(strMonPago.Trim().ToUpper()))
            {
                htParametro["moneda_internacional"] = "";
                htParametro["vuelto_Nac"] = Function.FormatDecimal(decimal.Parse(Imp_VueltoIzq));
            }
            else
            {
                htParametro["moneda_internacional"] = strDscMonPago;
                htParametro["vuelto_Nac"] = Function.FormatDecimal(decimal.Parse(Imp_VueltoDer));

                htParametro["vuelto_Int"] = Function.FormatDecimal(decimal.Parse(Imp_VueltoIzq));
            }

            //------ ESTEBAN ALIAGA GELDRES
            int q1 = intTicket / 2;
            int q2 = intTicket % 2;
            if (q2 != 0)
            {
                q1 = q1 + 1;
                htParametro["codigo_ticket_par" + "_" + (q1 - 1)] = "";
            }
            htParametro[Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL] = q1;

            if (intTicket > 0 && strTipPago == Define.TIP_PAGO_EFECTIVO)
            {
                if (strMonPago == objTipoTicket.SCodMoneda && strMonPago != strMonNac)
                {
                    //parche de ultimo momento-errror en tasa cambio compra
                    decimal decTCParche = decimal.Parse(htParametro["Imp_TCParche"].ToString());
                    if (objTipoTicket.DImpPrecio * intTicket * decTCParche < Function.FormatDecimal(decTCParche * decMonto, Define.NUM_DECIMAL))
                    {
                        Cambio objCambioCM = new Cambio();
                        objCambioCM.Cod_Moneda = strMonPago;
                        objCambioCM.Tip_Cambio = Define.COMPRA_MONEDA;
                        objCambioCM.Dsc_MonedaInt = strDscMonPago;
                        objCambioCM.Imp_TasaCambio = decTCParche;
                        objCambioCM.Dsc_SimboloInt = strMonSimbolo;
                        objCambioCM.Imp_MontoInt = Function.FormatDecimal(decMonto - (objTipoTicket.DImpPrecio * intTicket) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket)), Define.NUM_DECIMAL);
                        objCambioCM.Imp_MontoNac = Function.FormatDecimal((decMonto - (objTipoTicket.DImpPrecio * intTicket) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket))) * decTCParche, Define.NUM_DECIMAL);
                        listaCambio.Add(objCambioCM);
                    }
                }
                else
                {
                    if (strMonNac != strMonPago)
                    {
                        if (strMonPago != objTipoTicket.SCodMoneda)
                        {
                            Cambio objCambio = new Cambio();
                            objCambio.Tip_Cambio = Define.COMPRA_MONEDA;
                            objCambio.Cod_Moneda = strMonPago;
                            objCambio.Dsc_MonedaInt = strDscMonPago;
                            objCambio.Imp_TasaCambio = decTCPagado;
                            objCambio.Dsc_SimboloInt = strMonSimbolo;
                            //objCambio.Imp_PagadoNac = decTCPagado * decMonto;
                            if (objTipoTicket.DImpPrecio * intTicket * decTasaTuua < decTCPagado * decMonto)
                            {
                                objCambio.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * decTasaTuua / decTCPagado, Define.NUM_DECIMAL);
                                objCambio.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * decTasaTuua, Define.NUM_DECIMAL);
                            }
                            else
                            {
                                objCambio.Imp_MontoInt = Function.FormatDecimal(decMonto * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                                objCambio.Imp_MontoNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
                            }

                            listaCambio.Add(objCambio);

                            if (objTipoTicket.SCodMoneda != strMonNac)
                            {
                                Cambio objCambioVM = new Cambio();
                                objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
                                objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
                                objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
                                objCambioVM.Imp_TasaCambio = decTasaTuua;
                                objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
                                if (objTipoTicket.DImpPrecio * intTicket * decTasaTuua < decTCPagado * decMonto)
                                {
                                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket, Define.NUM_DECIMAL);
                                    objCambioVM.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * decTasaTuua, Define.NUM_DECIMAL);
                                }
                                else
                                {
                                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(decMonto * decTCPagado / decTasaTuua, Define.NUM_DECIMAL);
                                    objCambioVM.Imp_MontoNac = Function.FormatDecimal(decMonto * decTCPagado, Define.NUM_DECIMAL);
                                }
                                listaCambio.Add(objCambioVM);
                            }
                        }

                        if (objTipoTicket.DImpPrecio * intTicket * decTasaTuua < decTCPagado * decMonto)
                        {
                            Cambio objCambioCM = new Cambio();
                            objCambioCM.Cod_Moneda = strMonPago;
                            objCambioCM.Tip_Cambio = Define.COMPRA_MONEDA;
                            objCambioCM.Dsc_MonedaInt = strDscMonPago;
                            objCambioCM.Imp_TasaCambio = decTCPagado;
                            objCambioCM.Dsc_SimboloInt = strMonSimbolo;
                            //objCambioCM.Imp_Pagado = decMonto;
                            //objCambioCM.Imp_TotNac = objTipoTicket.DImpPrecio * intTicket * decTasaTuua;
                            objCambioCM.Imp_MontoInt = Function.FormatDecimal(decMonto - (objTipoTicket.DImpPrecio * intTicket * decTasaTuua / decTCPagado) - decimal.Truncate(decMonto - (objTipoTicket.DImpPrecio * intTicket * decTasaTuua / decTCPagado)), Define.NUM_DECIMAL);
                            objCambioCM.Imp_MontoNac = Function.FormatDecimal(objCambioCM.Imp_MontoInt * decTCPagado, Define.NUM_DECIMAL);
                            listaCambio.Add(objCambioCM);
                        }

                        if (decMontoSol > 0 && objTipoTicket.DImpPrecio * intTicket * decTasaTuua > decTCPagado * decMonto)
                        {
                            if (strMonNac != objTipoTicket.SCodMoneda)
                            {
                                Cambio objCambioVM = new Cambio();
                                objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
                                objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
                                objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
                                objCambioVM.Imp_TasaCambio = decTasaTuua;
                                objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;

                                if (strMonPago == objTipoTicket.SCodMoneda)
                                {
                                    //objCambioVM.Imp_TotPagar = objTipoTicket.DImpPrecio * intTicket;
                                    //objCambioVM.Imp_Pagado = decMonto;
                                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket - decMonto, Define.NUM_DECIMAL);
                                    objCambioVM.Imp_MontoNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * intTicket - decMonto) * decTasaTuua, Define.NUM_DECIMAL);
                                }
                                else
                                {
                                    //objCambioVM.Imp_TotNac = objTipoTicket.DImpPrecio * intTicket * decTasaTuua;
                                    //objCambioVM.Imp_PagadoNac = decMonto * decTCPagado;
                                    objCambioVM.Imp_MontoInt = Function.FormatDecimal(((objTipoTicket.DImpPrecio * intTicket * decTasaTuua) - (decMonto * decTCPagado)) / decTasaTuua, Define.NUM_DECIMAL);
                                    objCambioVM.Imp_MontoNac = Function.FormatDecimal((objTipoTicket.DImpPrecio * intTicket * decTasaTuua) - (decMonto * decTCPagado), Define.NUM_DECIMAL);
                                }
                                //objCambioVM.Imp_Pagado = decMonto;
                                listaCambio.Add(objCambioVM);
                            }
                        }
                    }
                    else
                    {
                        if (objTipoTicket.SCodMoneda != strMonNac)
                        {
                            Cambio objCambioVM = new Cambio();
                            objCambioVM.Cod_Moneda = objTipoTicket.SCodMoneda;
                            objCambioVM.Tip_Cambio = Define.VENTA_MONEDA;
                            objCambioVM.Dsc_MonedaInt = objTipoTicket.Dsc_Moneda;
                            objCambioVM.Imp_TasaCambio = decTasaTuua;
                            objCambioVM.Dsc_SimboloInt = objTipoTicket.Dsc_Simbolo;
                            objCambioVM.Imp_MontoInt = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket, Define.NUM_DECIMAL);
                            objCambioVM.Imp_MontoNac = Function.FormatDecimal(objTipoTicket.DImpPrecio * intTicket * decTasaTuua, Define.NUM_DECIMAL);
                            listaCambio.Add(objCambioVM);
                        }
                    }
                }
            }
            ValidarCambios(listaCambio);
            //******************* COMPRA/VENTA MONEDA INTERNACIONAL
            switch (listaCambio.Count)
            {
                case 0:
                    htParametro["CV_1"] = "0";
                    htParametro["CV_2"] = "0";
                    htParametro["CV_3"] = "0";
                    break;
                case 1:
                    htParametro["CV_1"] = "1";
                    htParametro["CV_2"] = "0";
                    htParametro["CV_3"] = "0";
                    FillCompraVenta(0, htParametro, listaCambio);
                    break;
                case 2:
                    htParametro["CV_1"] = "1";
                    htParametro["CV_2"] = "1";
                    htParametro["CV_3"] = "0";
                    FillCompraVenta(0, htParametro, listaCambio);
                    FillCompraVenta(1, htParametro, listaCambio);
                    break;
                case 3:
                    htParametro["CV_1"] = "1";
                    htParametro["CV_2"] = "1";
                    htParametro["CV_3"] = "1";
                    FillCompraVenta(0, htParametro, listaCambio);
                    FillCompraVenta(1, htParametro, listaCambio);
                    FillCompraVenta(2, htParametro, listaCambio);
                    break;
                default:
                    break;
            }
            //******************* COMPRA/VENTA MONEDA INTERNACIONAL

        }

        private static void FillCompraVenta(int i, Hashtable htParametro, List<Cambio> listaCambio)
        {
            if (listaCambio[i].Tip_Cambio.Equals(Define.COMPRA_MONEDA))
                htParametro["tipo_compraventa_" + (i + 1)]="Compra";
            else
                htParametro["tipo_compraventa_" + (i + 1)]= "Venta";
            htParametro["moneda_internacional_" + (i + 1)] = listaCambio[i].Dsc_MonedaInt;
            htParametro["monto_inter_" + (i + 1)] = listaCambio[i].Imp_MontoInt;
            htParametro["simbolo_mon_internacional_" + (i + 1)] = listaCambio[i].Dsc_SimboloInt;
            htParametro["monto_soles_" + (i + 1)] = listaCambio[i].Imp_MontoNac;
            htParametro["tipo_cambio_" + (i + 1)] = listaCambio[i].Imp_TasaCambio;
        }

        private static void ValidarCambios(List<Cambio> listaCambio)
        {
            for (int i = 0; i < listaCambio.Count; i++)
            {
                listaCambio[i].Imp_MontoInt = Function.FormatDecimal(listaCambio[i].Imp_MontoInt * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                listaCambio[i].Imp_MontoNac = Function.FormatDecimal(listaCambio[i].Imp_MontoNac * Define.FACTOR_DECIMAL, Define.NUM_DECIMAL);
                if (listaCambio[i].Imp_MontoInt == 0)
                {
                    listaCambio.Remove(listaCambio[i--]);
                }
            }
        }

        public static string Right(string param, int lenght)
        {
            string result = param.Substring(param.Length - lenght, lenght);
            return result;
        }
    }

}
