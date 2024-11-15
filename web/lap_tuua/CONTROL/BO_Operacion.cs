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
using System.Transactions;

namespace LAP.TUUA.CONTROL
{
    public class BO_Operacion
    {
        protected Conexion objCnxTurno;
        protected Conexion objCnxOpera;
        protected Conexion objCnxConsulta;
        protected string dsc_Message;
        protected string dsc_Mensaje;
        public string Imp_VueltoIzq;
        public string Imp_VueltoDer;

        public BO_Operacion()
        {
            objCnxTurno = Resolver.ObtenerConexion(Define.CNX_02);
            objCnxOpera = Resolver.ObtenerConexion(Define.CNX_03);
            objCnxConsulta = Resolver.ObtenerConexion(Define.CNX_05);
        }

        public BO_Operacion(string strUsuario, string strModulo, string strSubModulo)
        {
            objCnxTurno = Resolver.ObtenerConexion(Define.CNX_02);
            objCnxOpera = Resolver.ObtenerConexion(Define.CNX_03);
            objCnxConsulta = Resolver.ObtenerConexion(Define.CNX_05);
            objCnxOpera.Cod_Usuario = strUsuario;
            objCnxOpera.Cod_Modulo = strModulo;
            objCnxOpera.Cod_Sub_Modulo = strSubModulo;

            objCnxTurno.Cod_Usuario = strUsuario;
            objCnxTurno.Cod_Modulo = strModulo;
            objCnxTurno.Cod_Sub_Modulo = strSubModulo;
        }

        public string Dsc_Mensaje
        {
            get
            {
                return dsc_Mensaje;
            }
            set
            {
                dsc_Mensaje = value;
            }
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

        public TipoTicket ObtenerPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            TipoTicket objTipoTicket = objCnxOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objTipoTicket != null && objTipoTicket.STipEstado != Define.TIP_ACTIVO)
            {
                return null;
            }
            return objTipoTicket;
        }

        public TipoTicket ValidarTipoTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
        {
            return objCnxOpera.validarTipoTicket(strTipoVuelo, strTipoPas, strTipoTrans);
        }

        public DataTable ConsultarCompaniaxFiltro(string strEstado, string strTipo, string CadFiltro, string sOrdenacion)
        {
            return objCnxOpera.ConsultarCompaniaxFiltro(strEstado, strTipo, CadFiltro, sOrdenacion);
        }

        public DataTable ListarVuelosxCompania(string strCompania, string strFecha)
        {
            return objCnxOpera.ListarVuelosxCompania(strCompania, strFecha);
        }

        private bool ActualizarMontosTurno(List<TurnoMonto> listaTurnoMonto, string strMoneda, string strMonNac, decimal decIngreso, decimal decEgresoNac)
        {
            for (int i = 0; i < listaTurnoMonto.Count; i++)
            {
                if (listaTurnoMonto[i].SCodMoneda == strMoneda)
                {
                    if (strMoneda == strMonNac)
                    {
                        break;
                    }
                    listaTurnoMonto[i].DImpMontoActual += decIngreso;
                }
                else if (listaTurnoMonto[i].SCodMoneda == strMonNac)
                {
                    listaTurnoMonto[i].DImpMontoActual -= decEgresoNac;
                }
            }
            return objCnxTurno.ActualizarTurnoMonto(listaTurnoMonto);
        }

        private bool VerificarMargenCajaTicket(List<TurnoMonto> listaTurnoMonto, string strMoneda, string strMonNac, decimal decEgreVuelto, decimal decEgresoNac)
        {
            for (int i = 0; i < listaTurnoMonto.Count; i++)
            {
                if (listaTurnoMonto[i].SCodMoneda == strMoneda)
                {
                    if (strMoneda == strMonNac)
                    {
                        if (listaTurnoMonto[i].DImpMontoActual - decEgresoNac < 0)
                        {
                            dsc_Message = "No se dispone de vuelto.";
                            return false;
                        }
                        return true;
                    }
                    if (listaTurnoMonto[i].DImpMontoActual - decEgreVuelto < 0)
                    {
                        dsc_Message = "No se dispone de vuelto.";
                        return false;
                    }
                }
                else if (listaTurnoMonto[i].SCodMoneda == strMonNac)
                {
                    if (listaTurnoMonto[i].DImpMontoActual - decEgresoNac < 0)
                    {
                        dsc_Message = "No se dispone de vuelto.";
                        return false;
                    }
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

        public bool RegistrarVentaMasiva(string strCodModVenta, VentaMasiva objVentaMasiva,ref string strTurno, TipoTicket objTipoTicket, string strNumero, string strNumVuelo,
                                         string strFecVuelo, string strPtoVenta, string strEmpresa, string strRepte, ref string strListaTickets, string strCodTurnoIng, string strFlgCierreTurno)
        {
            string strMoneda = objVentaMasiva.SCodMoneda;
            string strAtrMinTicket = (string)Property.htProperty[Define.ATR_NRO_MIN_TICKET];
            string strAtrMaxTicket = (string)Property.htProperty[Define.ATR_NRO_MAX_TICKET];
            string strAtrLimVenta = (string)Property.htProperty[Define.ATR_LIM_VENTA];
            string strFecVence = "";

            List<Ticket> listaTickets = new List<Ticket>();

            if (!VerificarLimiteTicketXModalidad(objVentaMasiva.DCanVenta, strCodModVenta, strAtrMinTicket, strAtrMaxTicket, objTipoTicket.SCodTipoTicket, objVentaMasiva.SCodCompania))
            {
                return false;
            }

            strFecVence = Define.ADMIN;
            if (!objCnxOpera.RegistrarTicket(objVentaMasiva.SCodCompania, null, strNumVuelo, strFecVuelo, strTurno, objVentaMasiva.SCodUsuario, objTipoTicket.DImpPrecio, 
                                             objTipoTicket.SCodMoneda, strCodModVenta, objTipoTicket.SCodTipoTicket, objTipoTicket.STipVuelo, objVentaMasiva.DCanVenta, "0", 
                                             null, Define.TIP_PAGO_CREDITO, strEmpresa, strRepte, ref strFecVence, ref strListaTickets, strCodTurnoIng, strFlgCierreTurno, string.Empty))
            {
                dsc_Message = (string)LabelConfig.htLabels[Define.opeVentaCredito_msgTrxFail];
                return false;
            }
            if (strListaTickets == Define.OPE_VENTA_VAL00)
            {
                dsc_Message = (string)LabelConfig.htLabels["opeVentaCredito.msgTurnoPendiente"];
                return false;
            }

            /*
             * ESTRUCTURA A-B-C
             * a: 01(supero el limite de venta por semana
             * b: cantidad restringida por configuracion desde la opcion de compañia
             * c: cantidad acumulada por empresa                           
             */

            string[] vec = strListaTickets.Split('-'); 
            if (vec[0] == Define.OPE_VENTA_VAL01)
            {
                //dsc_Message = (string)LabelConfig.htLabels["opeVentaCredito.msgMaxVenta"];
                //icano 22/07/10 se necesita mandar la estructura pero como cadena
                dsc_Message = "";
                dsc_Mensaje = strListaTickets;
                //fin
                return false;
            }
            // fecha de vencimiento para la impresion de tickets (GGarcia-20090925)
            strTurno = strFecVence.Split('#')[1];
            objVentaMasiva.DtFchVenta = strFecVence.Split('#')[0];
            return true;
        }

        public List<TipoTicket> ListarTipoTickets()
        {
            return objCnxOpera.ListarAllTipoTicket();
        }

        public List<ModVentaComp> ListarCompaniaxModVenta(string strCodModVenta, string strTipComp)
        {
            return objCnxOpera.ListarCompaniaxModVenta(strCodModVenta, strTipComp);
        }

        public DataTable ListarTurnosAbiertos(string strTurno)
        {
            DataTable dtTurnos = objCnxConsulta.ConsultaTurnoxFiltro(null, null, "0", null, null, null, "0");
            for (int i = 0; i < dtTurnos.Rows.Count; i++)
            {
                if (strTurno != "" && dtTurnos.Rows[i].ItemArray.GetValue(0).ToString().Trim() != strTurno)
                {
                    dtTurnos.Rows.RemoveAt(i--);
                    continue;
                }
                if (dtTurnos.Rows[i].ItemArray.GetValue(5).ToString().Trim() != "")
                {
                    dtTurnos.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtTurnos;
        }

        public bool CerrarTurno(List<Turno> listaTurnos)
        {
            for (int i = 0; i < listaTurnos.Count; i++)
            {
                if (objCnxTurno.ActualizarTurno(listaTurnos[i]))
                {
                    List<TurnoMonto> listaMontos = new List<TurnoMonto>();
                    TurnoMonto objTurnoMonto = new TurnoMonto();
                    objTurnoMonto.SCodMoneda = null;
                    objTurnoMonto.SCodTurno = listaTurnos[i].SCodTurno;
                    listaMontos.Add(objTurnoMonto);
                    if (!objCnxTurno.ActualizarTurnoMonto(listaMontos))
                    {
                        dsc_Message = (string)LabelConfig.htLabels[Define.opeCierreTurno_msgTrxFail];
                        return false;
                    }
                }
                else
                {
                    dsc_Message = (string)LabelConfig.htLabels[Define.opeCierreTurno_msgTrxFail];
                    return false;
                }
            }
            return true;
        }

        public Turno obtenerTurnoIniciado(string strUsuario)
        {
            //DAO_Turno objDAOTurno = new DAO_Turno(Dsc_PathSPConfig);
            return objCnxTurno.ObtenerTurnoIniciado(strUsuario);
                //.obtenerTurnoIniciado(strUsuario);
        }

        public List<TurnoMonto> ListarTurnoMontoxTurno(string strCodTurno)
        {
            return objCnxTurno.ListarTurnoMontosPorTurno(strCodTurno);
        }

        public bool EmitirContingencia(int intCantidad, TipoTicket objTipoTicket, string strUsuario, string strVueloDefault, ref string strTickets, ref string strFecVence, string strCodPrecio)
        {
            string strAtrMinTicket = (string)Property.htProperty[Define.ATR_NRO_MIN_CONTINGENCIA];
            string strAtrMaxTicket = (string)Property.htProperty[Define.ATR_NRO_MAX_CONTINGENCIA];
            string strCiaDefault = (string)Property.htProperty[Define.COD_COMPANIA_DEFAULT];
            int intAtrNumTicket;
            DataTable dtConsulta = new DataTable();
            dtConsulta = objCnxConsulta.ListarParametros(strAtrMinTicket);
            intAtrNumTicket = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());

            if (intAtrNumTicket > intCantidad)
            {
                Dsc_Message = (string)LabelConfig.htLabels[Define.opeGenContingencia_msgMinTicket] + "[" + intAtrNumTicket.ToString() + "]";
                return false;
            }
            dtConsulta = objCnxConsulta.ListarParametros(strAtrMaxTicket);
            intAtrNumTicket = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            if (intAtrNumTicket < intCantidad)
            {
                Dsc_Message = (string)LabelConfig.htLabels[Define.opeGenContingencia_msgMaxTicket] + "[" + intAtrNumTicket.ToString() + "]";
                return false;
            }
            strFecVence = Define.ADMIN;
            if (!objCnxOpera.RegistrarTicket(strCiaDefault, null, strVueloDefault, null, null, strUsuario, objTipoTicket.DImpPrecio, objTipoTicket.SCodMoneda, null, objTipoTicket.SCodTipoTicket, objTipoTicket.STipVuelo, intCantidad, "1", null, null, null, null, ref strFecVence, ref strTickets, string.Empty, "1", strCodPrecio))
            {
                dsc_Message = (string)LabelConfig.htLabels[Define.opeGenContingencia_msgTrxFail];
                return false;
            }
            return true;
        }

        public DataTable ListarContingencia(string strTipoTicket, string strFlgConti, string strUsuario, string strRangoIni, string strRangoFin)
        {
            DataTable dtConsulta = objCnxOpera.ListarContingencia(strTipoTicket, strFlgConti, strRangoIni, strRangoFin, strUsuario);
            return dtConsulta;
        }

        public bool RegistrarContingencia(int intTicket, string strCompania, string strNumVuelo, string strUsuario, string strMoneda, string strTipTicket, string strFecVenta, string strListaTickets, string strCodTurno, string strFlagTurno, ref string strCodTurnoCreado, ref string strCodError)
        {
            return objCnxOpera.RegistrarVentaContingencia(strCompania, strNumVuelo, strUsuario, strMoneda, strTipTicket, strFecVenta, intTicket, strListaTickets, strCodTurno, strFlagTurno, ref strCodTurnoCreado, ref strCodError);
        }

        public bool verificarTurnoCerradoxUsuario(string strUsuario)
        {
            return objCnxTurno.verificarTurnoCerradoxUsuario(strUsuario);
        }

        public bool GenerarTicketATM(int intCantidad, TipoTicket objTipoTicket, string strCompania, string strUsuario, string strVueloDefault, string strEmpresa, string strRepte, ref string strTickets)
        {
            string strAtrMinTicket = (string)Property.htProperty[Define.ATR_NRO_MIN_TICKET];
            string strAtrMaxTicket = (string)Property.htProperty[Define.ATR_NRO_MAX_TICKET];
            string strCodModVenta = (string)Property.htProperty[Define.MOD_VENTA_ATM];
            string strFecVence = "";
            List<Ticket> listaTicket = new List<Ticket>();

            if (!VerificarLimiteTicketXModalidad(intCantidad, strCodModVenta, strAtrMinTicket, strAtrMaxTicket, objTipoTicket.SCodTipoTicket, strCompania))
            {
                return false;
            }
            strFecVence = Define.ADMIN;
            if (!objCnxOpera.RegistrarTicket(strCompania, null, strVueloDefault, null, null, strUsuario, objTipoTicket.DImpPrecio, objTipoTicket.SCodMoneda, strCodModVenta, objTipoTicket.SCodTipoTicket, objTipoTicket.STipVuelo, intCantidad, "0", null, Define.TIP_PAGO_CREDITO, strEmpresa, strRepte, ref strFecVence, ref strTickets, string.Empty, "1", string.Empty))
            {
                dsc_Message = (string)LabelConfig.htLabels[Define.opeGenContingencia_msgTrxFail];
                return false;
            }
            strTickets = EncriptarTickets(strTickets, strCodModVenta, strCompania);

            return true;
        }

        private bool VerificarLimiteTicketXModalidad(int intCantidad, string strCodModVenta, string strAtrMinTicket, string strAtrMaxTicket, string strTipTicket, string strCompania)
        {
            List<ModalidadAtrib> listaAtribxComp = objCnxOpera.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
            List<ModalidadAtrib> listaAtribDefault = objCnxOpera.ListarAtributosxModVenta(strCodModVenta);
            bool boMinTicket = false;
            bool boMaxTicket = false;

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

            if (!boMinTicket)
            {
                DataTable dtConsulta = objCnxConsulta.ListarParametros(strAtrMinTicket);
                int intMinTicket = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                if (intMinTicket > intCantidad)
                {
                    Dsc_Message = (string)LabelConfig.htLabels[Define.opeVentaCredito_msgLimMinTicket] + " [" + intMinTicket + "].";
                    return false;
                }
                boMinTicket = true;
            }
            if (!boMaxTicket)
            {
                DataTable dtConsulta = objCnxConsulta.ListarParametros(strAtrMaxTicket);
                int intMaxTicket = Int32.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
                if (intMaxTicket < intCantidad)
                {
                    Dsc_Message = (string)LabelConfig.htLabels[Define.opeVentaCredito_msgLimMaxTicket] + " [" + intMaxTicket + "].";
                    return false;
                }
                boMaxTicket = true;
            }
            return true;
        }

        private string EncriptarTickets(string strTickets, string strCodModVenta, string strCompania)
        {
            string strEncriptado = "";
            string strKey = "";
            Cryptografia objCrypto = new Cryptografia();
            List<ModalidadAtrib> listaAtribxComp = objCnxOpera.ListarAtributosxModVentaCompania(strCodModVenta, strCompania);
            for (int i = 0; i < listaAtribxComp.Count; i++)
            {
                if (listaAtribxComp[i].SCodAtributo == (string)Property.htProperty[Define.ATR_KEY_ENCRIPTA_ATM])
                {
                    strKey = listaAtribxComp[i].SDscValor;
                    break;
                }
            }
            if (!(strKey != null && strKey.Trim() != ""))
            {
                strKey = strCompania;
            }
            strEncriptado = objCrypto.Encriptar(strTickets, strKey);
            return strEncriptado;
        }

        public DataTable ListarTurnosXFiltro(int intTipo, string strTurno, string strCajero, string strInicio, string strFin)
        {
            DataTable dtConsulta = objCnxConsulta.ConsultaTurnoxFiltro(strInicio, strFin, null, "1", "", "", "0");

            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                if (strTurno != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim() != strTurno)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (intTipo == 0 && dtConsulta.Rows[i].ItemArray.GetValue(5).ToString() != "")//abierto
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (intTipo == 1 && dtConsulta.Rows[i].ItemArray.GetValue(5).ToString() == "")//cerrado
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strCajero != null && dtConsulta.Rows[i].ItemArray.GetValue(7).ToString().Trim().ToUpper() != strCajero.Trim().ToUpper())
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtConsulta;
        }

        public DataTable ListarCompraVentaXFiltro(string strTurno, string strTipo, string strCodOpera)
        {
            DataTable dtConsulta = objCnxConsulta.obtenerUsuarioxFechaOperacion(null, null, strTipo, null);
            string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                if (strTurno != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim() != strTurno)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strCodOpera != "" && dtConsulta.Rows[i].ItemArray.GetValue(11).ToString().Trim() != strCodOpera)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (dtConsulta.Rows[i].ItemArray.GetValue(12).ToString().Trim() == Define.ESTADO_TICKET_EXTORNADO)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (dtConsulta.Rows[i].ItemArray.GetValue(8).ToString().Trim() == strMonNac)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtConsulta;
        }

        public bool ExtornarCompraVenta(List<LogOperacion> listaLogOpera)
        {
            bool boRetorno;
            string strListaOpera = "";
            string strMessage = "";
            for (int i = 0; i < listaLogOpera.Count; i++)
            {
                strListaOpera += listaLogOpera[i].SNumOperacion;
            }
            boRetorno = objCnxOpera.ExtornarCompraVenta(strListaOpera, listaLogOpera[0].SCodTurno, listaLogOpera.Count, ref strMessage);
            if (strMessage.Trim().Length != 0)
            {
                Dsc_Message = strMessage;
                boRetorno = false;
            }
            return boRetorno;
        }

        public bool ExtornarTicket(string strListaTickets, string strTurno, int intCantidad, string strUsuario, string strMotivo)
        {
            bool boRetorno;
            string strMessage = "";
            boRetorno = objCnxOpera.ExtornarTicket(strListaTickets, strTurno, intCantidad, strUsuario, strMotivo, ref strMessage);
            if (strMessage.Trim().Length != 0)
            {
                Dsc_Message = strMessage;
                boRetorno = false;
            }
            return boRetorno;
        }

        public DataTable ListarTicketsExtorno(string sCod_Ticket, string sTicket_Desde, string sTicket_Hasta, string sCod_Turno, string sColumnaSort, int iStartRows, int iMaxRows, string sPaginacion, string sFlgTotalRows)
        {
            DataTable dtConsulta = objCnxConsulta.ListarTicketsExtorno(sCod_Ticket, sTicket_Desde, sTicket_Hasta, sCod_Turno, sColumnaSort, iStartRows, iMaxRows, sPaginacion, sFlgTotalRows);
            return dtConsulta;
        }

        public DataTable ListarTicketsExtorno(string strTurno, string strTicket, string strDelNro, string strAlNro)
        {
            DataTable dtConsulta = objCnxConsulta.ListarTicketxFecha("", "", "0", "0", "0", "0", "0", "T", "", "", strTurno);
            string strEmitido = Define.ESTADO_TICKET_EMITIDO;

            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                if (strTicket != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim() != strTicket)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strDelNro != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim().CompareTo(strDelNro)<0)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strAlNro != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim().CompareTo(strAlNro) > 0)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (dtConsulta.Rows[i].ItemArray.GetValue(10).ToString().Trim() != strEmitido)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtConsulta;
        }

        public bool ExtornarRehabilitacion(string strListaTickets, int intCantidad, string strUsuario, bool transaccion)
        {
            string strEstado = Define.ESTADO_TICKET_USADO;
            return objCnxOpera.ExtornoRehabilitacion(strListaTickets, intCantidad, strUsuario, strEstado, transaccion);
        }

        public DataTable ListarTicketsRehabilitados(string strNumIni, string strNumFin, string strFecIni, string strFecFin, string strHoraIni, string strHoraFin, string strNumVuelo)
        {
            string strEstado = Define.ESTADO_TICKET_REHABILITADO;
            DataTable dtConsulta = objCnxConsulta.ListarTicketxFecha_Reh(strFecIni, strFecFin, "0", "0", strEstado, "0", "0", "T", strHoraIni, strHoraFin, null);

            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                //if (strTicket != "" && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim() != strTicket)
                //{
                //    dtConsulta.Rows.RemoveAt(i--);
                //    continue;
                //}
                if (strNumVuelo != null && dtConsulta.Rows[i].ItemArray.GetValue(2).ToString().Trim() != strNumVuelo)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strNumIni != null && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim().CompareTo(strNumIni) < 0)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (strNumFin != null && dtConsulta.Rows[i].ItemArray.GetValue(0).ToString().Trim().CompareTo(strNumFin) > 0)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtConsulta;
        }

        public DataTable ListarTicketsExtension(string strTicket, string strDelNro, string strAlNro)
        {
            string strEmitido = "V";
            if (strTicket == null)
            {
                strTicket = strEmitido;
            }
            DataTable dtConsulta = objCnxConsulta.ConsultaDetalleTicket_Reh(strTicket, strDelNro, strAlNro);

            for (int i = 0; i < dtConsulta.Rows.Count; i++)
            {
                if (dtConsulta.Rows[i].ItemArray.GetValue(16).ToString().Trim() != Define.ESTADO_TICKET_EMITIDO && dtConsulta.Rows[i].ItemArray.GetValue(16).ToString().Trim() != Define.ESTADO_TICKET_ANULADO)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
                if (dtConsulta.Rows[i].ItemArray.GetValue(16).ToString().Trim() == Define.ESTADO_TICKET_ANULADO && dtConsulta.Rows[i].ItemArray.GetValue(31).ToString().Trim() != Define.TIP_ANULA_VENCIDO)
                {
                    dtConsulta.Rows.RemoveAt(i--);
                    continue;
                }
            }
            return dtConsulta;
        }

        public bool ExtenderVigencia(List<Ticket> listaTicket, int intDias, string strUsuario)
        {
            string strListaTickets = "";
            string strListaFecExt = "";
            string strFechaExt = "";
            string strFecha = "";
            string strFechaActual = ObtenerFechaActual();
            for (int i = 0; i < listaTicket.Count; i++)
            {
                strListaTickets += listaTicket[i].SCodNumeroTicket;
                if (listaTicket[i].STipEstadoActual == Define.ESTADO_TICKET_ANULADO)
                {
                    strFecha = Function.ConvertToDateTime(strFechaActual.Substring(0, 8)).AddDays(intDias).ToShortDateString();
                }
                else
                {
                    strFecha = Function.ConvertToDateTime(listaTicket[i].DtFchVencimiento).AddDays(intDias).ToShortDateString();
                }
                strFechaExt = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2);
                strListaFecExt += strFechaExt;
            }
            return objCnxOpera.ExtenderVigenciaTicket(strListaTickets, strListaFecExt, strUsuario);
        }

        public List<ModalidadAtrib> ListarAtributosxModVenta(string strCodModVenta)
        {
            return objCnxOpera.ListarAtributosxModVenta(strCodModVenta);
        }

        public bool AnularTicket(List<TicketEstHist> objLisTicket)
        {
            bool resul = false;
            TicketEstHist objTicketEstHist;

            try
            {
                using (TransactionScope ambito = new TransactionScope())
                {
                    for (int i = 0; i < objLisTicket.Count; i++)
                    {
                        objTicketEstHist = new TicketEstHist();
                        objTicketEstHist = objLisTicket[i];
                        if (objCnxOpera.AnularTicket(objTicketEstHist.SCodNumeroTicket, objTicketEstHist.SDscMotivo, objTicketEstHist.SLogUsuarioMod) == true)
                            resul = true;
                        else
                        {
                            resul = false;
                            break;
                        }
                    }
                    if (resul == true)
                    {
                        // Completar la transacción
                        ambito.Complete();
                    }
                    return resul;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool AnularTicket(List<BoardingBcbp> objLisBoardingBcbp)
        {
            bool resul = false;
            BoardingBcbp objBoardingBcbp;

            try
            {
                using (TransactionScope ambito = new TransactionScope())
                {
                    for (int i = 0; i < objLisBoardingBcbp.Count; i++)
                    {
                        objBoardingBcbp = new BoardingBcbp();
                        objBoardingBcbp = objLisBoardingBcbp[i];
                        if (objCnxOpera.AnularBCBP(objBoardingBcbp) == true)
                            resul = true;
                        else
                        {
                            resul = false;
                            break;
                        }
                    }
                    if (resul == true)
                    {
                        // Completar la transacción
                        ambito.Complete();
                    }
                    return resul;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable ObtenerVentaTicket(string strFecIni, string strFecFin, string strTipVenta, string strFlgAero)
        {
            DataTable dtVentaTicket = objCnxOpera.ObtenerVentaTicket(strFecIni, strFecFin, strTipVenta, strFlgAero);
            return dtVentaTicket;
        }

        public DataTable ObtenerComprobanteSEAE(string sAnio, string sMes,string sTDocumento, string strTipVenta, string strFlgAero)
        {
            DataTable dtComprobante = objCnxOpera.ObtenerComprobanteSEAE(sAnio, sMes, sTDocumento,strTipVenta, strFlgAero);
            return dtComprobante;
        }

        private string ObtieneTipoDocVenta(string strModVenta, string strFlgCobro)
        {
            if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_NORMAL] || strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CONT])
            {
                return Define.TIP_VENTA_CAJA;
            }
            if (strModVenta == (string)Property.htProperty[Define.MOD_VENTA_ATM] || strModVenta == (string)Property.htProperty[Define.MOD_VENTA_MAS_CRED])
            {
                if (strFlgCobro == "0")
                    return Define.TIP_VENTA_CREDITO;
            }
            return Define.TIP_VENTA_USO;
        }

        public bool AnularTuua(string strListaTicket, int intTicket)
        {
            return objCnxOpera.AnularTuua(strListaTicket, intTicket);
        }

        public TasaCambio ObtenerTasaCambioPorMoneda(string strMoneda, string strTipo)
        {
            return objCnxOpera.ObtenerTasaCambioPorMoneda(strMoneda, strTipo);
        }

        #region Molinete
        public DataTable ListarMolinetes(string strCodMolinete, string strDscIp)
        {
            return objCnxOpera.ListarMolinetes(strCodMolinete, strDscIp);
        }


        public bool actualizarMolinete(Molinete objMolinete)
        {
            return objCnxOpera.actualizarMolinete(objMolinete);
        }

        public bool actualizarUnMolinete(Molinete objMolinete)
        {
            return objCnxOpera.actualizarUnMolinete(objMolinete);
        }

        public DataTable ListarAllMolinetes()
        {
            return objCnxOpera.ListarAllMolinetes();
        }

        public DataTable obtenerMolinete(string strCodMolinete)
        {
            return objCnxOpera.obtenerMolinete(strCodMolinete);
        }

        #endregion

        public string ObtenerFechaActual()
        {
            return objCnxOpera.ObtenerFechaActual();
        }

        public DataTable ListarBcbpxConciliar(string sCodCompania, string strFchVuelo, string strNumVuelo, string strNumAsiento, string strPasajero, string strFecUsoIni, string strFecUsoFin, string strFlg)
        {
            return objCnxOpera.ListarBcbpxConciliar(sCodCompania, strFchVuelo, strNumVuelo, strNumAsiento, strPasajero, strFecUsoIni, strFecUsoFin, strFlg);
        }

        public bool RegistrarBcbpxConciliar(string sBcbpBase, string sBcbpUlt, string sBcbpAsoc)
        {
            return objCnxOpera.RegistrarBcbpxConciliar(sBcbpBase, sBcbpUlt, sBcbpAsoc);
        }

        public bool ObtenerDetalleTurnoActual(string strCodUsuario, ref string strCantTickets, ref string strCodTurno, ref string strFecHorTurno)
        {
            return objCnxOpera.ObtenerDetalleTurnoActual(strCodUsuario, ref strCantTickets, ref strCodTurno, ref strFecHorTurno); 
        }

    }

}
