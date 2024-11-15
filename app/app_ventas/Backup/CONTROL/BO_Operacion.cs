/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Proceso de gestión de operaciones.
Especificaciones:   
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONEXION;
using LAP.TUUA.UTIL;
using LAP.TUUA.RESOLVER;
using System.Data;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.CONTROL
{
    public class BO_Operacion
    {
        protected Conexion objCnxTurno;
        protected Conexion objCnxOpera;
        protected string dsc_Message;
        public string Imp_VueltoIzq;
        public string Imp_VueltoDer;
        public List<TurnoMonto> listaTurnoMonto;

        public BO_Operacion()
        {
            objCnxTurno = Resolver.ObtenerConexion(Define.CNX_02);
            objCnxOpera = Resolver.ObtenerConexion(Define.CNX_03);
            objCnxOpera.Cod_Usuario = Property.strUsuario;
            objCnxOpera.Cod_Modulo = Property.strModulo;
            objCnxOpera.Cod_Sub_Modulo = Property.strSubModulo;
            objCnxTurno.Cod_Usuario = Property.strUsuario;
            objCnxTurno.Cod_Modulo = Property.strModulo;
            objCnxTurno.Cod_Sub_Modulo = Property.strSubModulo;
        }

        public string Dsc_Message
        {
            get
            {
                return dsc_Message;
            }
            set
            {
                dsc_Message = value;
            }
        }

        public bool RegistrarOperacionCaja(List<LogOperacCaja> objListaCaja)
        {
            if (objListaCaja != null)
            {
                if (!VerificarLimites(objListaCaja))
                {
                    return false;
                }
                if (!VerificarOperacionCaja(objListaCaja[0].SCodTipoOperacion, objListaCaja, objListaCaja[0].SCodTurno))
                {
                    return false;
                }
                if (objCnxOpera.RegistrarOperacion(objListaCaja[0]))
                {
                    for (int i = 0; i < objListaCaja.Count; i++)
                    {
                        objListaCaja[i].INumSecuencial = i + 1;
                    }
                    if (objCnxOpera.RegistrarOpeCaja(objListaCaja))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public bool VerificarLimites(List<LogOperacCaja> objListaCaja)
        {
            string smessage = "";
            char chTipo = objListaCaja[0].SCodTipoOperacion == "EC" ? '-' : '+';
            string stipoope = objListaCaja[0].SCodTipoOperacion;
            List<Limite> objListaLimites = objCnxOpera.ListarLimitesPorOperacion(stipoope);
            List<Limite> objListaMargen = objCnxOpera.ListarLimitesPorOperacion(Define.MARGEN_CAJA);
            List<TurnoMonto> listaTurMontos = objCnxTurno.ListarTurnoMontosPorTurno(objListaCaja[0].SCodTurno);
            if (objListaLimites == null)
            {
                return true;
            }
            for (int i = 0; i < objListaCaja.Count; i++)
            {
                for (int j = 0; j < objListaLimites.Count; j++)
                {
                    if (objListaCaja[i].SCodMoneda == objListaLimites[j].Cod_Moneda)
                    {
                        if (objListaCaja[i].DImpOperacion >= objListaLimites[j].Imp_LimMinimo && objListaCaja[i].DImpOperacion <= objListaLimites[j].Imp_LimMaximo)
                        {
                            break;
                        }
                        else
                        {
                            smessage += objListaCaja[i].SCodMoneda + ": Monto Inicial NO ESTÁ entre los límites " + objListaLimites[j].Imp_LimMinimo + " y " + objListaLimites[j].Imp_LimMaximo + ". \n";
                        }
                    }
                }
                if (!MargenCajaOK(objListaMargen, listaTurMontos, objListaCaja[i].SCodMoneda, objListaCaja[i].DImpOperacion, chTipo))
                {
                    dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
                    return false;
                }
            }

            if (smessage.Length > 0)
            {
                this.dsc_Message = smessage;
                return false;
            }
            return true;
        }

        public TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            return objCnxOpera.ObtenerTasaCambioPorMoneda(strMoneda, strTipo);
        }

        public int RegistrarCompraMoneda(LogCompraVenta objCompraVenta, decimal decRecibidoNac, decimal decAcambiar)
        {
            List<Limite> objListaMargen = objCnxOpera.ListarLimitesPorOperacion(Define.MARGEN_CAJA);
            List<TurnoMonto> listaTurMontos = objCnxTurno.ListarTurnoMontosPorTurno(objCompraVenta.SCodTurno);
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            if (decRecibidoNac >= decAcambiar * objCompraVenta.DImpTasaCambio)
            {
                decimal decEntregarNac = decRecibidoNac - decAcambiar * objCompraVenta.DImpTasaCambio;
                objCompraVenta.DImpIngreso = decRecibidoNac - decEntregarNac;
                objCompraVenta.DImpEntregarNac = decEntregarNac;
                objCompraVenta.DImpEntregarInter = decAcambiar;
                objCompraVenta.DImpEgreso = objCompraVenta.DImpEntregarInter;
                objCompraVenta.DImpACambiar = decAcambiar * objCompraVenta.DImpTasaCambio;
                objCompraVenta.DImpCambiado = decAcambiar;
                objCompraVenta.Tip_Pago = Define.TIP_PAGO_EFECTIVO;

                //List<Cambio> listaCambioSol = new List<Cambio>();
                //Cambio objCambioSol = new Cambio();
                //objCambioSol.Cod_Moneda = strMonNac;
                //objCambioSol.Tip_Cambio = Define.VENTA_MONEDA;
                //objCambioSol.Imp_TasaCambio = objCompraVenta.DImpTasaCambio;
                //objCambioSol.Imp_MontoInt = objCompraVenta.DImpIngreso;
                //objCambioSol.Imp_MontoNac = objCompraVenta.DImpIngreso;
                //listaCambioSol.Add(objCambioSol);

                if (!VerificarLimites(objCompraVenta.SCodTurno, decAcambiar, objCompraVenta.SCodMoneda, objCompraVenta.SCodTipoOperacion))
                {
                    return 3;
                }
                if (!MargenCajaOK(objListaMargen, listaTurMontos, strMonNac, objCompraVenta.DImpIngreso, '+'))
                {
                    dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
                    return 3;
                }

                if (!MargenCajaOK(objListaMargen, listaTurMontos, objCompraVenta.SCodMoneda, objCompraVenta.DImpEgreso, '-'))
                {
                    dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
                    return 3;
                }
                if (!objCnxOpera.RegistrarOperacion(objCompraVenta))
                {
                    dsc_Message = "Transacción no completada.";
                    return 2;
                }
                if (!objCnxOpera.RegistrarCompraVenta(objCompraVenta))
                {
                    dsc_Message = "Transacción no completada.";
                    return 2;
                }
                //CrearCodigoOperacion(listaCambioSol, objCompraVenta.SCodTurno, objCompraVenta.SCodUsuario);
                //RegistrarCambio(listaCambioSol, objCompraVenta.SCodTurno, objCompraVenta.SCodUsuario);
            }
            else
            {
                this.dsc_Message = "El monto Recibido(S./) no es suficiente para efectuar la operación.";
                return 1;
            }
            return 0;
        }

        public int RegistrarVentaMoneda(LogCompraVenta objCompraVenta, decimal decRecibidoInt)
        {
            List<Limite> objListaMargen = objCnxOpera.ListarLimitesPorOperacion(Define.MARGEN_CAJA);
            List<TurnoMonto> listaTurMontos = objCnxTurno.ListarTurnoMontosPorTurno(objCompraVenta.SCodTurno);
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            decimal decEntregarInt = decRecibidoInt - objCompraVenta.DImpACambiar;
            objCompraVenta.DImpIngreso = objCompraVenta.DImpACambiar;
            objCompraVenta.DImpEgreso = objCompraVenta.DImpACambiar * objCompraVenta.DImpTasaCambio;
            objCompraVenta.DImpEntregarNac = objCompraVenta.DImpEgreso;
            objCompraVenta.DImpEntregarInter = decEntregarInt;
            objCompraVenta.DImpCambiado = objCompraVenta.DImpEgreso;
            objCompraVenta.Tip_Pago = Define.TIP_PAGO_EFECTIVO;

            //List<Cambio> listaCambioSol = new List<Cambio>();
            //Cambio objCambioSol = new Cambio();
            //objCambioSol.Cod_Moneda = strMonNac;
            //objCambioSol.Tip_Cambio = Define.COMPRA_MONEDA;
            //objCambioSol.Imp_TasaCambio = objCompraVenta.DImpTasaCambio;
            //objCambioSol.Imp_MontoInt = objCompraVenta.DImpEgreso;
            //objCambioSol.Imp_MontoNac = objCompraVenta.DImpEgreso;
            //listaCambioSol.Add(objCambioSol);

            if (!VerificarLimites(objCompraVenta.SCodTurno, objCompraVenta.DImpEntregarNac, strMonNac, objCompraVenta.SCodTipoOperacion))
            {
                return 3;
            }

            if (!MargenCajaOK(objListaMargen, listaTurMontos, objCompraVenta.SCodMoneda, objCompraVenta.DImpIngreso, '+'))
            {
                dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
                return 3;
            }

            if (!MargenCajaOK(objListaMargen, listaTurMontos, strMonNac, objCompraVenta.DImpEgreso, '-'))
            {
                dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
                return 3;
            }

            if (!objCnxOpera.RegistrarOperacion(objCompraVenta))
            {
                dsc_Message = "Transacción no completada.";
                return 2;
            }
            if (!objCnxOpera.RegistrarCompraVenta(objCompraVenta))
            {
                dsc_Message = "Transacción no completada.";
                return 2;
            }
            //CrearCodigoOperacion(listaCambioSol, objCompraVenta.SCodTurno, objCompraVenta.SCodUsuario);
            //RegistrarCambio(listaCambioSol, objCompraVenta.SCodTurno, objCompraVenta.SCodUsuario);
            return 0;
        }


        public bool VerificarLimites(string strTurno, decimal decMonto, string strMoneda, string strTipoOpe)
        {
            List<Limite> objLista = objCnxOpera.ListarLimitesPorOperacion(strTipoOpe);
            Limite objLimite = null;
            List<TurnoMonto> listaTurMontos = objCnxTurno.ListarTurnoMontosPorTurno(strTurno);

            if (objLista == null)
            {
                return true;
            }

            for (int j = 0; j < objLista.Count; j++)
            {
                if (strMoneda == objLista[j].Cod_Moneda)
                {
                    if (decMonto >= objLista[j].Imp_LimMinimo && decMonto <= objLista[j].Imp_LimMaximo)
                    {
                        objLimite = objLista[j];
                        break;
                    }
                    else
                    {
                        dsc_Message = "Monto a cambiar NO ESTÁ entre los límites " + objLista[j].Imp_LimMinimo + " y " + objLista[j].Imp_LimMaximo + ". \n";
                        return false;
                    }
                }
            }
            if (objLimite == null)
            {
                return true;
            }
            //objLista = objCnxOpera.ListarLimitesPorOperacion(Define.MARGEN_CAJA);
            //for (int i = 0; i < listaTurMontos.Count; i++)
            //{
            //    if (listaTurMontos[i].SCodMoneda == strMoneda)
            //    {
            //        if (listaTurMontos[i].DImpMontoActual - decMonto < objLimite.Imp_LimMinimo && listaTurMontos[i].DImpMontoActual - decMonto > objLimite.Imp_LimMaximo)
            //        {
            //            dsc_Message = "Monto a cambiar sobrepasa margen de caja.";
            //            return false;
            //        }
            //    }
            //}
            return true;
        }

        public TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            TipoTicket objTipoTicket = objCnxOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objTipoTicket != null && objTipoTicket.STipEstado != "1")
            {
                objTipoTicket = null;
            }
            return objTipoTicket;
        }

        public DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            return objCnxOpera.ConsultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
        }

        public DataTable ListarVuelosxCompania(string strCompania, string strFecha, string strTipVuelo, string strModo)
        {
            strCompania = strCompania == "" ? strModo : strCompania;
            DataTable dtVuelos = objCnxOpera.ListarVuelosxCompania(strCompania, strFecha, strTipVuelo);
            return dtVuelos;
        }

        public List<Moneda> ListarMonedasInter()
        {
            return objCnxOpera.ListarMonedasInter();
        }

        public bool RegistrarVentaNormal(Hashtable htFlujoCaja, decimal decImpDialogo, string strModVenta, int intCantidad, decimal decTasaCambio, TipoTicket objTipoTicket, string strCompania, string strVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decMonto, string strMoneda, string strNumRef, string strTipPago, decimal decMontoSol, List<Cambio> listaCambio, ref string strFecVence, ref string strListaTickets, ref decimal decTCPagado)
        {
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            decimal decIngreso = 0;
            decimal decEgresoNac = 0;
            decimal decIngresoNac = 0;
            listaTurnoMonto = objCnxTurno.ListarTurnoMontosPorTurno(strTurno);
            if (!ValidarLimiteVenta(intCantidad, strModVenta, strCompania, objTipoTicket.SCodTipoTicket))
            {
                return false;
            }
            if (!ValidarVentaTicket(strTipPago, listaTurnoMonto, strMonNac, strMoneda, decMonto, decTasaCambio, intCantidad, objTipoTicket, decMontoSol, decImpDialogo, ref decTCPagado))
            {
                return false;
            }
            if (!VerificarMargenCajaTicket(listaTurnoMonto, htFlujoCaja))
            {
                return false;
            }
            strFecVence = Define.VENTAS;
            if (strTipPago == Define.TIP_PAGO_CREDEFEC || strTipPago == Define.TIP_PAGO_DEBIEFEC)
            {
                strTipPago = Define.TIP_PAGO_EFECTIVO;
            }
            return RegistrarTicket(strModVenta, intCantidad, decTasaCambio, objTipoTicket, strCompania, strVuelo, strFecVuelo, strTurno, strUsuario, decMonto, strMoneda, decIngreso, decEgresoNac, listaTurnoMonto, strNumRef, strTipPago, null, null, decIngresoNac, ref strFecVence, ref strListaTickets);
        }

        public bool RegistrarTicket(string strModVenta, int intCantidad, decimal decTasaCambio, TipoTicket objTipoTicket, string strCompania, string strVuelo, string strFecVuelo, string strTurno, string strUsuario, decimal decMonto, string strMoneda, decimal decIngreso, decimal decEgresoNac, List<TurnoMonto> listaTurnoMonto, string strNumRef, string strTipPago, string strEmpresa, string strRepte, decimal decIngresoNac, ref string strFecVence, ref string strListaTickets)
        {
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            // se guarda la lista de tickets (GGarcia-20090909)
            //objTipoTicket.ListaTicketImp = listaTickets;

            if (!objCnxOpera.RegistrarTicket(strCompania, null, strVuelo, strFecVuelo, strTurno, strUsuario, objTipoTicket.DImpPrecio, objTipoTicket.SCodMoneda, strModVenta, objTipoTicket.SCodTipoTicket, objTipoTicket.STipVuelo, intCantidad, "0", strNumRef, strTipPago, strEmpresa, strRepte, ref strFecVence, ref strListaTickets,null,null,""))
            {
                dsc_Message = "Transacción no completada.";
                return false;
            }

            //if (!ActualizarMontosTurno(listaTurnoMonto, strMoneda, strMonNac, decIngreso, decEgresoNac, decIngresoNac))
            //{
            //    dsc_Message = "Transacción no completada.";
            //    return false;
            //}
            //LogCompraVenta objLogComVenta = new LogCompraVenta();
            //objLogComVenta.SCodTipoOperacion = Define.COMPRA_MONEDA;
            //objCnxOpera.RegistrarCompraVenta();
            return true;
        }

        public bool ActualizarMontosTurno(string strMoneda, decimal decMonto, decimal decTasaCambio, int intCantidad, TipoTicket objTipoTicket, decimal decMontoSol)
        {
            decimal decVueltoTotNac;
            decimal decVueltoInter;
            decimal decVueltNac;
            decimal decInterToNac;
            decimal decTCPagado;
            decimal decIngreso;
            decimal decEgresoNac = 0;
            decimal decIngresoNac = 0;
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            if (strMonNac != strMoneda)
            {
                TasaCambio objTCPagado = objCnxOpera.ObtenerTasaCambioPorMoneda(strMoneda, objTipoTicket.SCodMoneda != strMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                decTCPagado = objTCPagado.DImpCambioActual;

                if (decMontoSol == 0)
                {
                    decVueltoTotNac = objTCPagado.DImpCambioActual * decMonto - decTasaCambio * objTipoTicket.DImpPrecio * intCantidad;
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decVueltoTotNac / objTCPagado.DImpCambioActual), 2);
                    decVueltNac = decVueltoTotNac - decVueltoInter * objTCPagado.DImpCambioActual;

                    decIngreso = Function.FormatDecimal(decMonto - decVueltoInter, 2);
                    decEgresoNac = Function.FormatDecimal(decVueltNac, 2);
                }
                else
                {
                    decVueltoTotNac = objTCPagado.DImpCambioActual * decMonto - decTasaCambio * objTipoTicket.DImpPrecio * intCantidad + decMontoSol;
                    decInterToNac = objTCPagado.DImpCambioActual * decMonto - decTasaCambio * objTipoTicket.DImpPrecio * intCantidad;
                    decVueltoInter = Function.FormatDecimal(decimal.Truncate(decInterToNac < 0 ? 0 : decInterToNac / objTCPagado.DImpCambioActual), 2);
                    decVueltNac = decVueltoTotNac - decVueltoInter * objTCPagado.DImpCambioActual - decMontoSol;

                    decIngreso = Function.FormatDecimal(decMonto - decVueltoInter, 2);
                    decEgresoNac = Function.FormatDecimal(decVueltNac > 0 ? decVueltNac : 0, 2);
                    decIngresoNac = (decMontoSol - decVueltoTotNac) > 0 ? decMontoSol - decVueltoTotNac : 0;
                }
            }
            else
            {
                decVueltoTotNac = decMonto - (decTasaCambio * objTipoTicket.DImpPrecio * intCantidad);
                decIngreso = Function.FormatDecimal(decMonto - decVueltoTotNac, 2);
            }


            for (int i = 0; i < listaTurnoMonto.Count; i++)
            {
                if (listaTurnoMonto[i].SCodMoneda == strMoneda)
                {
                    listaTurnoMonto[i].DImpMontoActual += decIngreso;
                }
                else if (listaTurnoMonto[i].SCodMoneda == strMonNac)
                {
                    listaTurnoMonto[i].DImpMontoActual -= decEgresoNac;
                    listaTurnoMonto[i].DImpMontoActual += decIngresoNac;
                }
            }
            return objCnxTurno.ActualizarTurnoMonto(listaTurnoMonto);
        }

        public bool ActualizarMontosTurno(Hashtable htFlujoCaja, string strMoneda, decimal decIngresoInt, decimal decIngresoNac, decimal decEgresoNac)
        {
            object[] keys = new object[htFlujoCaja.Count];
            htFlujoCaja.Keys.CopyTo(keys, 0);
            decimal decImporte = 0;
            List<TurnoMonto> listaMontoUpd = new List<TurnoMonto>();
            for (int i = 0; i < keys.Length; i++)
            {
                string[] arrFlujo = keys[i].ToString().Split('#');
                decImporte = decimal.Parse(htFlujoCaja[keys[i]].ToString());
                arrFlujo[1] = arrFlujo[1] == Define.TIP_PAGO_DEBITO ? Define.TIP_PAGO_CREDITO : arrFlujo[1];
                arrFlujo[1] = arrFlujo[1] == Define.TIP_PAGO_CREDEFEC || arrFlujo[1] == Define.TIP_PAGO_DEBIEFEC ? Define.TIP_PAGO_EFECTIVO : arrFlujo[1];
                listaMontoUpd.Add(ActualizarCaja(listaTurnoMonto, arrFlujo[2], decImporte, arrFlujo[1], arrFlujo[0]));
            }
            return objCnxTurno.ActualizarTurnoMonto(listaMontoUpd);
        }

        private bool VerificarMargenCajaTicket(List<TurnoMonto> listaTurnoMonto, Hashtable htFlujoCaja)
        {
            IPHostEntry IPs = Dns.GetHostByName("");
            IPAddress[] Direcciones = IPs.AddressList;
            String IpClient = Direcciones[0].ToString();
            decimal decImpFlujo = 0;
            for (int i = 0; i < listaTurnoMonto.Count; i++)
            {
                if (listaTurnoMonto[i].Tip_Pago != Define.TIP_PAGO_EFECTIVO)
                {
                    continue;
                }
                decImpFlujo = decimal.Parse(htFlujoCaja["-#E#" + listaTurnoMonto[i].SCodMoneda] == null ? "0" : htFlujoCaja["-#E#" + listaTurnoMonto[i].SCodMoneda].ToString());
                if (listaTurnoMonto[i].DImpMontoActual - decImpFlujo < 0)
                {
                    dsc_Message = (string)LabelConfig.htLabels["turno.msgVuelto"];
                    //GeneraAlarma - La caja se queda sin dinero
                    GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000072", "I20", IpClient, "2", "Alerta W0000072", "No se dispone de dinero en caja para dar vuelto, Moneda: " + listaTurnoMonto[i].SCodMoneda + ", Equipo: " + IpClient + ", Usuario: " + Property.strUsuario, Property.strUsuario);
                    return false;
                }
            }

            List<Limite> objListaMargen = objCnxOpera.ListarLimitesPorOperacion(Define.MARGEN_CAJA);
            object[] keys = new object[htFlujoCaja.Count];
            htFlujoCaja.Keys.CopyTo(keys, 0);
            decimal decImporte = 0;
            for (int i = 0; i < keys.Length; i++)
            {
                string[] arrFlujo = keys[i].ToString().Split('#');
                decImporte = decimal.Parse(htFlujoCaja[keys[i]].ToString());
                if (arrFlujo[1] != Define.TIP_PAGO_EFECTIVO)
                {
                    continue;
                }
                if (!MargenCajaOK(objListaMargen, listaTurnoMonto, arrFlujo[2], decImporte, arrFlujo[0].ToCharArray()[0]))
                {
                    dsc_Message = (string)LabelConfig.htLabels["turno.msgMargen"];
                    return false;
                }
            }
            return true;
        }

        public List<RepresentantCia> ListarRepteCia(string strCia)
        {
            List<RepresentantCia> listaRepteCia = objCnxOpera.ListarRepteCia(strCia);
            for (int i = 0; i < listaRepteCia.Count; i++)
            {
                listaRepteCia[i].SNomRepresentante += " " + listaRepteCia[i].SApeRepresentante;
            }
            return listaRepteCia;
        }

        public bool RegistrarVentaMasiva(Hashtable htFlujoCaja, decimal decImpDialogo, string strCodModVenta, VentaMasiva objVentaMasiva, string strTurno, TipoTicket objTipoTicket, decimal decMonto, string strNumero, decimal decTasaCambio, string strNumVuelo, string strFecVuelo, string strTipPago, string strEmpresa, string strRepte, decimal decMontoSol, List<Cambio> listaCambio, ref string strFecVence, ref string strListaTickets, ref decimal decTCPagado)
        {
            decimal decIngreso = 0;
            decimal decEgresoNac = 0;
            decimal decIngresoNac = 0;
            decimal decTotalAPagar = objTipoTicket.DImpPrecio * objVentaMasiva.DCanVenta;
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            string strMoneda = objVentaMasiva.SCodMoneda;

            List<Ticket> listaTickets = new List<Ticket>();

            if (!ValidarLimiteVenta(objVentaMasiva.DCanVenta, strCodModVenta, objVentaMasiva.SCodCompania, objTipoTicket.SCodTipoTicket))
            {
                return false;
            }

            listaTurnoMonto = objCnxTurno.ListarTurnoMontosPorTurno(strTurno);

            objVentaMasiva.DImpMontoVenta = decTotalAPagar;
            objVentaMasiva.SCodMoneda = objTipoTicket.SCodMoneda;
            objVentaMasiva.Num_Cheque_Trans = strNumero;

            if (!ValidarVentaTicket(strTipPago, listaTurnoMonto, strMonNac, strMoneda, decMonto, decTasaCambio, objVentaMasiva.DCanVenta, objTipoTicket, decMontoSol, decImpDialogo, ref decTCPagado))
            {
                return false;
            }
            if (!VerificarMargenCajaTicket(listaTurnoMonto, htFlujoCaja))
            {
                return false;
            }
            strFecVence = Define.VENTAS;
            if (strTipPago == Define.TIP_PAGO_CREDEFEC || strTipPago == Define.TIP_PAGO_DEBIEFEC)
            {
                strTipPago = Define.TIP_PAGO_EFECTIVO;
            }
            //if (objVentaMasiva.Tip_Pago != Define.TIP_PAGO_EFECTIVO)
            //{
            //    if (!objCnxOpera.RegistrarTicket(objVentaMasiva.SCodCompania, null, strNumVuelo, strFecVuelo, strTurno, objVentaMasiva.SCodUsuario, objTipoTicket.DImpPrecio, strMoneda, strCodModVenta, objTipoTicket.SCodTipoTicket, objTipoTicket.STipVuelo, objVentaMasiva.DCanVenta, "0", strNumero, strTipPago, strEmpresa, strRepte, ref strFecVence, ref strListaTickets))
            //    {
            //        dsc_Message = "Transacción no completada.";
            //        return false;
            //    }
            //    return true;
            //}
            return RegistrarTicket(strCodModVenta, objVentaMasiva.DCanVenta, decTasaCambio, objTipoTicket, objVentaMasiva.SCodCompania, strNumVuelo, strFecVuelo, strTurno, objVentaMasiva.SCodUsuario, decMonto, strMoneda, decIngreso, decEgresoNac, listaTurnoMonto, strNumero, strTipPago, strEmpresa, strRepte, decIngresoNac, ref strFecVence, ref strListaTickets);
        }

        public bool ActualizarVentaMasiva(VentaMasiva objVentaMasiva)
        {
            return objCnxOpera.ActualizarVentaMasiva(objVentaMasiva);
        }

        private bool ValidarVentaTicket(string strTipPago, List<TurnoMonto> listaTurnoMonto, string strMonNac, string strMoneda, decimal decMonto, decimal decTasaCambio, int intCantidad, TipoTicket objTipoTicket, decimal decMontoSol, decimal decMontoDialogo, ref decimal decTCPagado)
        {
            //decMonto += decMontoDialogo;
            if (strMoneda == objTipoTicket.SCodMoneda && strMoneda != strMonNac)
            {
                //parche de ultimo momento-error en tasa cambio compra
                decimal decTCParche = ObtenerTasaCambioPorMoneda(strMoneda, Define.TC_COMPRA).DImpCambioActual;
                decimal decTVMonPago = ObtenerTasaCambioPorMoneda(strMoneda, Define.TC_VENTA).DImpCambioActual;
                if (Function.FormatDecimal((decMonto + (decMontoSol / decTVMonPago)) * decTCParche + decMontoDialogo * decTCParche, Define.NUM_DECIMAL) < Function.FormatDecimal(decTCParche * objTipoTicket.DImpPrecio * intCantidad, Define.NUM_DECIMAL))
                {
                    dsc_Message = "Monto ingresado no cubre el costo total del TUUA.";
                    return false;
                }
            }
            else
            {
                if (strMonNac != strMoneda)
                {
                    TasaCambio objTCPagado = objCnxOpera.ObtenerTasaCambioPorMoneda(strMoneda, objTipoTicket.SCodMoneda != strMoneda ? Define.TC_COMPRA : Define.TC_VENTA);
                    decTCPagado = objTCPagado.DImpCambioActual;
                    //if (Function.FormatDecimal((objTCPagado.DImpCambioActual * decMonto) / decTasaCambio, Define.NUM_DECIMAL) == Function.FormatDecimal(objTipoTicket.DImpPrecio * intCantidad, Define.NUM_DECIMAL))
                    //{
                    //    decMonto = decTasaCambio * objTipoTicket.DImpPrecio * intCantidad / objTCPagado.DImpCambioActual;
                    //}
                    if (Function.FormatDecimal(objTCPagado.DImpCambioActual * decMonto + decMontoSol + decMontoDialogo * decTasaCambio, Define.NUM_DECIMAL) < Function.FormatDecimal(decTasaCambio * objTipoTicket.DImpPrecio * intCantidad, Define.NUM_DECIMAL))
                    {
                        if (Function.FormatDecimal(objTCPagado.DImpCambioActual * decMonto + decMontoSol + decMontoDialogo * decTasaCambio, 1) < Function.FormatDecimal(decTasaCambio * objTipoTicket.DImpPrecio * intCantidad, 1))
                        {
                            dsc_Message = "Monto ingresado no cubre el costo total del TUUA.";
                            return false;
                        }
                    }
                }
                else
                {
                    if (Function.FormatDecimal(decMonto + decMontoDialogo * decTasaCambio, Define.NUM_DECIMAL) < Function.FormatDecimal(decTasaCambio * objTipoTicket.DImpPrecio * intCantidad, Define.NUM_DECIMAL))
                    {
                        if (Function.FormatDecimal(decMonto + decMontoDialogo * decTasaCambio, 1) < Function.FormatDecimal(decTasaCambio * objTipoTicket.DImpPrecio * intCantidad, 1))
                        {
                            dsc_Message = "Monto ingresado no cubre el costo total del TUUA.";
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public List<TipoTicket> ListarTipoTickets()
        {
            List<TipoTicket> lista = objCnxOpera.ListarAllTipoTicket();
            if (lista != null)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].STipEstado != "1")
                    {
                        lista.RemoveAt(i);
                        i--;
                    }
                }
            }
            return lista;
        }

        public List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            return objCnxOpera.ListarCompaniaxModVenta(strCodModVenta, strTipComp);
        }

        public Moneda ObtenerMoneda(string strMoneda)
        {
            Conexion objCnxAdmin = Resolver.ObtenerConexion(Define.CNX_06);
            DataTable dtMoneda = objCnxAdmin.obtenerDetalleMoneda(strMoneda);
            Moneda objMoneda = new Moneda();
            if (dtMoneda != null && dtMoneda.Rows.Count > 0 && dtMoneda.Rows[0].ItemArray.GetValue(7).ToString() == (string)Property.htProperty[Define.EST_MONEDA_ACTIVO])
            {
                objMoneda.SCodMoneda = dtMoneda.Rows[0].ItemArray.GetValue(0).ToString();
                objMoneda.SDscMoneda = dtMoneda.Rows[0].ItemArray.GetValue(1).ToString();
                objMoneda.SDscSimbolo = dtMoneda.Rows[0].ItemArray.GetValue(2).ToString();
                objMoneda.STipEstado = dtMoneda.Rows[0].ItemArray.GetValue(7).ToString();
                return objMoneda;
            }
            return null;
        }

        private TurnoMonto ActualizarCaja(List<TurnoMonto> lista, string strMoneda, decimal decMonto, string strTipPago, string strOpera)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].SCodMoneda == strMoneda && strTipPago == lista[i].Tip_Pago)
                {
                    switch (strOpera)
                    {
                        case "+":
                            lista[i].DImpMontoActual += decMonto;
                            return lista[i];
                        case "-": lista[i].DImpMontoActual -= decMonto;
                            return lista[i];
                        default: break;
                    }
                    break;
                }
            }
            return null;
        }

        private bool VerificarOperacionCaja(string strTipo, List<LogOperacCaja> objListaCaja, string strTurno)
        {
            string strMessage = "";
            if (strTipo != Define.EGRESO_CAJA)
            {
                return true;
            }
            List<TurnoMonto> listaTurnoMonto = objCnxTurno.ListarTurnoMontosPorTurno(strTurno);
            for (int i = 0; i < objListaCaja.Count; i++)
            {
                for (int j = 0; j < listaTurnoMonto.Count; j++)
                {
                    if (listaTurnoMonto[j].SCodMoneda == objListaCaja[i].SCodMoneda && listaTurnoMonto[j].Tip_Pago==Define.TIP_PAGO_EFECTIVO)
                    {
                        if (listaTurnoMonto[j].DImpMontoActual - objListaCaja[i].DImpOperacion < 0)
                        {
                            IPHostEntry IPs = Dns.GetHostByName("");
                            IPAddress[] Direcciones = IPs.AddressList;
                            String IpClient = Direcciones[0].ToString();
                            //GeneraAlarma - La caja se queda sin dinero
                            GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000072", "I20", IpClient, "2", "Alerta W0000072", "No se dispone de dinero en caja para efectuar la operacion, Moneda: " + listaTurnoMonto[j].SCodMoneda + ", Equipo: " + IpClient + ", Usuario: " + Property.strUsuario, Property.strUsuario);

                            strMessage += "No se dispone de dinero en caja para efectuar la operacion." + "[" + listaTurnoMonto[j].SCodMoneda + "]\n";
                        }
                        break;
                    }
                }
            }
            if (strMessage.Length > 0)
            {
                this.dsc_Message = strMessage;
                return false;
            }
            return true;
        }

        private bool ValidarLimiteVenta(int intCantidad, string strCodModVenta, string strCompania, string strTipTicket)
        {
            decimal decIngreso = 0;
            decimal decEgresoNac = 0;
            string strAtrMinTicket = (string)Property.htProperty[Define.ATR_NRO_MIN_TICKET];
            string strAtrMaxTicket = (string)Property.htProperty[Define.ATR_NRO_MAX_TICKET];

            List<ModalidadAtrib> listaAtribxComp = objCnxOpera.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
            List<ModalidadAtrib> listaAtribDefault = objCnxOpera.ListarAtributosxModVenta(strCodModVenta);
            bool boMinTicket = false;
            bool boMaxTicket = false;
            bool boLimVenta = false;
            int intLimMinxMod = 0;
            int intLimMaxxMod = 0;
            for (int i = 0; i < listaAtribxComp.Count; i++)
            {
                if (listaAtribxComp[i].SCodAtributo == strAtrMinTicket)
                {
                    if (listaAtribxComp[i].SCodTipoTicket == strTipTicket)
                    {
                        int intMinTicket = Int32.Parse(listaAtribxComp[i].SDscValor);
                        if (intMinTicket > intCantidad)
                        {
                            Dsc_Message = "La cantidad de tickets no supera el límite mínimo [" + listaAtribxComp[i].SDscValor + "].";
                            return false;
                        }
                        boMinTicket = true;
                    }
                    else if (listaAtribxComp[i].SCodTipoTicket == null || listaAtribxComp[i].SCodTipoTicket == "")
                    {
                        intLimMinxMod = Int32.Parse(listaAtribxComp[i].SDscValor);
                    }
                }
                if (listaAtribxComp[i].SCodAtributo == strAtrMaxTicket)
                {
                    if (listaAtribxComp[i].SCodTipoTicket == strTipTicket)
                    {
                        int intMaxTicket = Int32.Parse(listaAtribxComp[i].SDscValor);
                        if (intMaxTicket < intCantidad)
                        {
                            Dsc_Message = "La cantidad de tickets supera el límite máximo [" + listaAtribxComp[i].SDscValor + "].";
                            return false;
                        }
                        boMaxTicket = true;
                    }
                    else if (listaAtribxComp[i].SCodTipoTicket == null || listaAtribxComp[i].SCodTipoTicket == "")
                    {
                        intLimMaxxMod = Int32.Parse(listaAtribxComp[i].SDscValor);
                    }
                }
            }

            if (!boMinTicket)
            {
                if (intLimMinxMod != 0)
                {
                    if (intLimMinxMod > intCantidad)
                    {
                        Dsc_Message = "La cantidad de tickets no supera el límite mínimo [" + intLimMinxMod + "].";
                        return false;
                    }
                    boMinTicket = true;
                }
                if (!boMinTicket)
                {
                    for (int i = 0; i < listaAtribDefault.Count; i++)
                    {
                        if (listaAtribDefault[i].SCodAtributo == strAtrMinTicket)
                        {
                            if (listaAtribDefault[i].SCodTipoTicket == strTipTicket)
                            {
                                int intMinTicket = Int32.Parse(listaAtribDefault[i].SDscValor);
                                if (intMinTicket > intCantidad)
                                {
                                    Dsc_Message = "La cantidad de tickets no supera el límite mínimo [" + listaAtribDefault[i].SDscValor + "].";
                                    return false;
                                }
                                boMinTicket = true;
                                intLimMinxMod = 0;
                                break;
                            }
                            else if (listaAtribDefault[i].SCodTipoTicket == null || listaAtribDefault[i].SCodTipoTicket == "")
                            {
                                intLimMinxMod = Int32.Parse(listaAtribDefault[i].SDscValor);
                            }
                        }
                    }
                    if (intLimMinxMod != 0)
                    {
                        if (intLimMinxMod > intCantidad)
                        {
                            Dsc_Message = "La cantidad de tickets no supera el límite mínimo [" + intLimMinxMod + "].";
                            return false;
                        }
                        boMinTicket = true;
                    }
                }
            }

            if (!boMaxTicket)
            {
                if (intLimMaxxMod != 0)
                {
                    if (intLimMaxxMod < intCantidad)
                    {
                        Dsc_Message = "La cantidad de tickets supera el límite máximo [" + intLimMaxxMod + "].";
                        return false;
                    }
                    boMaxTicket = true;
                }
                if (!boMaxTicket)
                {
                    for (int i = 0; i < listaAtribDefault.Count; i++)
                    {
                        if (listaAtribDefault[i].SCodAtributo == strAtrMaxTicket)
                        {
                            if (listaAtribDefault[i].SCodTipoTicket == strTipTicket)
                            {
                                int intMaxTicket = Int32.Parse(listaAtribDefault[i].SDscValor);
                                if (intMaxTicket < intCantidad)
                                {
                                    Dsc_Message = "La cantidad de tickets supera el límite máximo [" + listaAtribDefault[i].SDscValor + "].";
                                    return false;
                                }
                                boMaxTicket = true;
                                break;
                            }
                            else if (listaAtribDefault[i].SCodTipoTicket == null || listaAtribDefault[i].SCodTipoTicket == "")
                            {
                                intLimMaxxMod = Int32.Parse(listaAtribDefault[i].SDscValor);
                            }
                        }

                    }
                    if (intLimMaxxMod != 0)
                    {
                        if (intLimMaxxMod < intCantidad)
                        {
                            Dsc_Message = "La cantidad de tickets supera el límite máximo [" + intLimMaxxMod + "].";
                            return false;
                        }
                        boMaxTicket = true;
                    }
                }

            }
            return true;
        }

        private bool MargenCajaOK(List<Limite> objListaLimites, List<TurnoMonto> listaTurMontos, string strMoneda, decimal decImporte, char chTipo)
        {
            Limite objLimite = null;
            for (int i = 0; i < objListaLimites.Count; i++)
            {
                if (objListaLimites[i].Cod_Moneda == strMoneda)
                {
                    objLimite = objListaLimites[i];
                    break;
                }
            }

            if (objLimite == null)
            {
                return false;
            }

            for (int i = 0; i < listaTurMontos.Count; i++)
            {
                if (listaTurMontos[i].SCodMoneda == strMoneda && listaTurMontos[i].Tip_Pago == Define.TIP_PAGO_EFECTIVO)
                {
                    if (chTipo == '+')
                    {
                        if (listaTurMontos[i].DImpMontoActual + decImporte > objLimite.Imp_LimMaximo)
                        {
                            return false;
                        }
                    }
                    else if (listaTurMontos[i].DImpMontoActual - decImporte < objLimite.Imp_LimMinimo)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool AnularTuua(string strListaTicket, int intTicket)
        {
            return objCnxOpera.AnularTuua(strListaTicket, intTicket);
        }

        public bool RegistrarCambio(List<Cambio> listaCambio, string strTurno, string strUsuario)
        {
            for (int i = 0; i < listaCambio.Count; i++)
            {
                LogCompraVenta objLogComVenta = new LogCompraVenta();
                objLogComVenta.SCodTurno = strTurno;
                objLogComVenta.SCodUsuario = "1" + listaCambio[i].Num_Operacion;

                objLogComVenta.SCodTipoOperacion = listaCambio[i].Tip_Cambio;
                objLogComVenta.DImpTasaCambio = listaCambio[i].Imp_TasaCambio;
                objLogComVenta.SCodMoneda = listaCambio[i].Cod_Moneda;
                objLogComVenta.Tip_Pago = listaCambio[i].Tip_Pago;//listaCambio[i].Flg_TarjetaEfec ? Define.TIP_PAGO_EFECTIVO : listaCambio[i].Tip_Pago;
                if (listaCambio[i].Tip_Cambio == Define.VENTA_MONEDA)
                {
                    if (listaCambio[i].Imp_MontoNac > 0)
                    {
                        objLogComVenta.DImpACambiar = listaCambio[i].Imp_MontoNac;
                        objLogComVenta.DImpCambiado = listaCambio[i].Imp_MontoInt;
                    }
                }
                else
                {
                    if (listaCambio[i].Imp_MontoNac > 0)
                    {
                        objLogComVenta.DImpACambiar = listaCambio[i].Imp_MontoInt;
                        objLogComVenta.DImpCambiado = listaCambio[i].Imp_MontoNac;
                    }
                }
                if (!objCnxOpera.RegistrarOperacion(objLogComVenta))
                {
                    return false;
                }
                objLogComVenta.SCodUsuario = strUsuario;
                if (!objCnxOpera.RegistrarCompraVenta(objLogComVenta))
                {
                    return false;
                }
                //listaCambio[i].Num_Operacion = objLogComVenta.SNumOperacion;
            }
            return true;
        }

        public bool CrearCodigoOperacion(List<Cambio> listaCambio, string strTurno, string strUsuario)
        {
            for (int i = 0; i < listaCambio.Count; i++)
            {
                if (!(listaCambio[i].Num_Operacion != null && listaCambio[i].Num_Operacion != ""))
                {
                    LogCompraVenta objLogComVenta = new LogCompraVenta();
                    objLogComVenta.SCodTurno = strTurno;
                    objLogComVenta.SCodUsuario = strUsuario;

                    objLogComVenta.SCodTipoOperacion = null;
                    if (!objCnxOpera.RegistrarOperacion(objLogComVenta))
                    {
                        return false;
                    }
                    listaCambio[i].Num_Operacion = objLogComVenta.SNumOperacion;
                }
            }
            return true;
        }

    }
}
