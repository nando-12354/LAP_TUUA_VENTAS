/*
Sistema		    :   TUUA
Aplicación		:   Ventas
Objetivo		:   Proceso de gestión de turnos.
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	:   11/07/2009	
Programador		:	JCISNEROS
Observaciones	:	
*/
using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using LAP.TUUA.CONEXION;
using LAP.TUUA.UTIL;
using LAP.TUUA.RESOLVER;
using System.Data;
using LAP.TUUA.ALARMAS;
using System.Net;

namespace LAP.TUUA.CONTROL
{
    public class BO_Turno
    {
        protected Turno objTurno;
        protected Conexion objCnxTurno;
        protected Conexion objCnxOpera;
        protected Conexion objCnxConsulta;
        private string sErrorMessage;
        public bool Flg_ExcesoDescuadre;

        public BO_Turno()
        {
            objCnxTurno = Resolver.ObtenerConexion(Define.CNX_02);
            objCnxOpera = Resolver.ObtenerConexion(Define.CNX_03);
            objCnxConsulta = Resolver.ObtenerConexion(Define.CNX_05);
            Flg_ExcesoDescuadre = false;

            objCnxTurno.Cod_Usuario = Property.strUsuario;
            objCnxTurno.Cod_Modulo = Property.strModulo;
            objCnxTurno.Cod_Sub_Modulo = Property.strSubModulo;
        }

        public Turno ObjTurno
        {
            get { return objTurno; }
            set { objTurno = value; }
        }

        public string SErrorMessage
        {
            get
            {
                return sErrorMessage;
            }
            set
            {
                sErrorMessage = value;
            }
        }

        public bool IniciarTurno(List<TurnoMonto> listaMonedas, string strUsuario, string strEquipo)
        {
            List<TurnoMonto> listaMontos = new List<TurnoMonto>();
            DataTable dtConsulta;
            string strTurnoError = null;
            sErrorMessage = "";
            if (!objCnxTurno.verificarTurnoCerradoxUsuario(strUsuario))
            {
                dtConsulta = objCnxConsulta.ConsultaTurnoxFiltro(null, null, strUsuario, null, null, null);
                string strCodTurno = "";
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    //if (!(dtConsulta.Rows[i].ItemArray.GetValue(5) != null && dtConsulta.Rows[i].ItemArray.GetValue(5).ToString() != "") && dtConsulta.Rows[i].ItemArray.GetValue(2).ToString() == strEquipo)
                    //{
                    //    strCodTurno = dtConsulta.Rows[i].ItemArray.GetValue(0).ToString();
                    //    sErrorMessage = "Se iniciará un turno pendiente. [" + strCodTurno + "]";
                    //    return true;
                    //}

                    if (!(dtConsulta.Rows[i].ItemArray.GetValue(6) != null && dtConsulta.Rows[i].ItemArray.GetValue(6).ToString() != ""))
                    {
                        strCodTurno = dtConsulta.Rows[i].ItemArray.GetValue(0).ToString();
                        break;
                    }
                }
                //Usuario Intenta Iniciar turno, teniendo uno ya abierto
                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();
                //GeneraAlarma - Usuario Intenta Iniciar turno, teniendo uno ya habierto
                GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000075", "I20", IpClient, "2", "Alerta W0000075", "Usuario intenta iniciar turno, teniendo uno ya habierto, Turno: " + strCodTurno + ", Usuario: " + strUsuario, strUsuario);

                sErrorMessage = (string)LabelConfig.htLabels["turno.msgUsuario"] + " [" + strCodTurno + "]";
                return false;
            }
            if (!objCnxTurno.verificarTurnoCerradoxPtoVenta(strEquipo))
            {
                dtConsulta = objCnxConsulta.ConsultaTurnoxFiltro(null, null, null, strEquipo, null, null);
                string strCodTurno = "";
                for (int i = 0; i < dtConsulta.Rows.Count; i++)
                {
                    if (!(dtConsulta.Rows[i].ItemArray.GetValue(5) != null && dtConsulta.Rows[i].ItemArray.GetValue(5).ToString() != ""))
                    {
                        strCodTurno = dtConsulta.Rows[i].ItemArray.GetValue(0).ToString();
                        break;
                    }
                }
                sErrorMessage = (string)LabelConfig.htLabels["turno.msgPtoVenta"] + " [" + strCodTurno + "]";
                return false;
            }
            if (!VerificarLimites(listaMonedas, "IT"))
            {
                return false;
            }
            objCnxTurno.Cod_Modulo = (string)Property.htProperty[Define.VEN_TURNO];
            objCnxTurno.Cod_Sub_Modulo = "E8012";
            objCnxTurno.Cod_Usuario = strUsuario;
            if (objCnxTurno.CrearTurno("TURNO", strUsuario, strEquipo, ref strTurnoError))
            {
                if (strTurnoError.Trim() != "")
                {
                    sErrorMessage = (string)LabelConfig.htLabels["turno.msgUsuario"] + " [" + strTurnoError + "]";
                    return false;
                }
                Turno objTurno = objCnxTurno.ObtenerTurnoIniciado(strUsuario);
                if (objTurno != null)
                {
                    for (int i = 0; i < listaMonedas.Count; i++)
                    {
                        TurnoMonto objTMonto = new TurnoMonto();
                        objTMonto.SCodTurno = objTurno.SCodTurno;
                        objTMonto.SCodMoneda = listaMonedas[i].SCodMoneda;
                        objTMonto.DImpMontoInicial = listaMonedas[i].DImpMontoInicial;
                        objTMonto.DImpMontoActual = listaMonedas[i].DImpMontoActual;
                        listaMontos.Add(objTMonto);
                    }
                    if (objCnxTurno.RegistrarTurnoMonto(listaMontos))
                    {
                        this.objTurno = new Turno(objTurno.SCodTurno, objTurno.DtFchInicio, objTurno.DtFchFin, objTurno.SCodUsuarioCierre, objTurno.SCodUsuario, objTurno.SCodEquipo, objTurno.SHoraInicio, objTurno.SHoraFin);
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;

        }

        public bool CerrarTurno(List<TurnoMonto> auo_monedas, string susuario, string sturno)
        {
            List<TurnoMonto> listaMontos = new List<TurnoMonto>();

            Turno objTurno = objCnxTurno.ObtenerTurnoIniciado(susuario);
            if (objTurno != null && sturno == objTurno.SCodTurno)
            {
                objTurno.SCodUsuarioCierre = susuario;
                List<TurnoMonto> listaAux = new List<TurnoMonto>();
                for (int i = 0; i < auo_monedas.Count; i++)
                {
                    TurnoMonto objTMonto = new TurnoMonto();
                    objTMonto.SCodTurno = objTurno.SCodTurno;
                    objTMonto.SCodMoneda = auo_monedas[i].SCodMoneda;
                    objTMonto.DImpMontoInicial = auo_monedas[i].DImpMontoFinal;
                    objTMonto.DImpMontoFinal = auo_monedas[i].DImpMontoFinal;
                    listaAux.Add(objTMonto);
                }
                if (!VerificarLimites(listaAux, "CT"))
                {
                    return false;
                }

                if (objCnxTurno.ActualizarTurno(objTurno))
                {
                    for (int i = 0; i < auo_monedas.Count; i++)
                    {
                        TurnoMonto objTMonto = new TurnoMonto();
                        objTMonto.SCodTurno = objTurno.SCodTurno;
                        objTMonto.SCodMoneda = auo_monedas[i].SCodMoneda;
                        objTMonto.DImpMontoFinal = auo_monedas[i].DImpMontoFinal;
                        objTMonto.DImpMontoActual = -1;
                        objTMonto.Imp_Transferencia = auo_monedas[i].Imp_Transferencia;
                        objTMonto.Imp_Cheque = auo_monedas[i].Imp_Cheque;
                        objTMonto.Imp_Tarjeta = auo_monedas[i].Imp_Tarjeta;
                        objTMonto.Tip_Pago = Define.TIP_PAGO_EFECTIVO;
                        listaMontos.Add(objTMonto);
                    }
                    if (!objCnxTurno.ActualizarTurnoMonto(listaMontos))
                    {
                        return false;
                    }
                    MontoCierrexPago(listaMontos,Define.TIP_PAGO_TRANSF);
                    if (!objCnxTurno.ActualizarTurnoMonto(listaMontos))
                    {
                        return false;
                    }
                    MontoCierrexPago(listaMontos, Define.TIP_PAGO_CHEQUE);
                    if (!objCnxTurno.ActualizarTurnoMonto(listaMontos))
                    {
                        return false;
                    }
                    MontoCierrexPago(listaMontos, Define.TIP_PAGO_CREDITO);
                    if (!objCnxTurno.ActualizarTurnoMonto(listaMontos))
                    {
                        return false;
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public int VerificarCuadre(List<TurnoMonto> auo_monedas, string susuario, string sturno)
        {
            string smessage = "";
            Flg_ExcesoDescuadre = false;
            Conexion objConexion = Resolver.ObtenerConexion(Define.CNX_01);
            DataTable dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_MAX_DESCUADRE]);
            decimal decMaxDescuadre = decimal.Parse(dtConsulta.Rows[0].ItemArray.GetValue(4).ToString());
            List<TurnoMonto> listaTurMontos = objCnxTurno.ListarTurnoMontosPorTurno(sturno);
            List<Limite> objListaLimite = objCnxOpera.ListarLimitesPorOperacion(Define.TIP_OPE_MAX_DESCUADRE);
            Limite objLimite = null;
            string strDescPago;
            TurnoMonto objTurnoMon = null;
            if (listaTurMontos != null)
            {
                for (int i = 0; i < auo_monedas.Count; i++)
                {
                    objTurnoMon = BuscaTurnoMonto(listaTurMontos, auo_monedas[i], Define.TIP_PAGO_EFECTIVO);
                    objLimite = ObtenerLimite(objListaLimite, auo_monedas[i].SCodMoneda);
                    if (auo_monedas[i].DImpMontoFinal != objTurnoMon.DImpMontoActual)
                    {
                        //strDescPago = Property.htListaCampos[Define.CAMPO_TIPOPAGO + Define.TIP_PAGO_EFECTIVO].ToString();
                        if (Math.Abs(auo_monedas[i].DImpMontoFinal - objTurnoMon.DImpMontoActual) > objLimite.Imp_LimMaximo)
                        {
                            Flg_ExcesoDescuadre = true;
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Efectivo) Excede MAXIMO DESCUADRE PERMITIDO. \n";
                        }
                        else
                        {
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Efectivo) no cuadra. \n";
                        }
                    }
                    objTurnoMon = BuscaTurnoMonto(listaTurMontos, auo_monedas[i], Define.TIP_PAGO_TRANSF);
                    if (auo_monedas[i].Imp_Transferencia != objTurnoMon.DImpMontoActual)
                    {
                        if (Math.Abs(auo_monedas[i].Imp_Transferencia - objTurnoMon.DImpMontoActual) > objLimite.Imp_LimMaximo)
                        {
                            Flg_ExcesoDescuadre = true;
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Transferencia) Excede MAXIMO DESCUADRE PERMITIDO. \n";
                        }
                        else
                        {
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Transferencia) no cuadra. \n";
                        }
                    }
                    objTurnoMon = BuscaTurnoMonto(listaTurMontos, auo_monedas[i], Define.TIP_PAGO_CHEQUE);
                    if (auo_monedas[i].Imp_Cheque != objTurnoMon.DImpMontoActual)
                    {
                        if (Math.Abs(auo_monedas[i].Imp_Cheque - objTurnoMon.DImpMontoActual) > objLimite.Imp_LimMaximo)
                        {
                            Flg_ExcesoDescuadre = true;
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Cheque) Excede MAXIMO DESCUADRE PERMITIDO. \n";
                        }
                        else
                        {
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Cheque) no cuadra. \n";
                        }
                    }
                    objTurnoMon = BuscaTurnoMonto(listaTurMontos, auo_monedas[i], Define.TIP_PAGO_CREDITO);
                    if (auo_monedas[i].Imp_Tarjeta != objTurnoMon.DImpMontoActual)
                    {
                        if (Math.Abs(auo_monedas[i].Imp_Tarjeta - objTurnoMon.DImpMontoActual) > objLimite.Imp_LimMaximo)
                        {
                            Flg_ExcesoDescuadre = true;
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Tarjeta) Excede MAXIMO DESCUADRE PERMITIDO. \n";
                        }
                        else
                        {
                            smessage += "El monto en " + auo_monedas[i].SCodMoneda + "(Tarjeta) no cuadra. \n";
                        }
                    }
                }
            }
            if (smessage.Length > 0)
            {
                this.sErrorMessage = smessage;
                return 1;
            }
            return 0;
        }

        public TurnoMonto BuscaTurnoMonto(List<TurnoMonto> lstTMontos, TurnoMonto objTurnoMonto,string strTipPago)
        {
            for (int i = 0; i < lstTMontos.Count; i++)
            {
                if (lstTMontos[i].SCodMoneda == objTurnoMonto.SCodMoneda && lstTMontos[i].Tip_Pago == strTipPago)
                {
                    //lstTMontos[i].DImpMontoInicial = objTurnoMonto.DImpMontoInicial;
                    //lstTMontos[i].DImpMontoActual = objTurnoMonto.DImpMontoActual;
                    return lstTMontos[i];
                }
            }
            return null;
        }

        public bool VerificarLimites(List<TurnoMonto> lstTMontos, string stipoope)
        {
            string smessage = "";
            List<Limite> objLista = objCnxOpera.ListarLimitesPorOperacion(stipoope);

            if (objLista == null)
            {
                return true;
            }
            for (int i = 0; i < lstTMontos.Count; i++)
            {
                for (int j = 0; j < objLista.Count; j++)
                {
                    if (lstTMontos[i].SCodMoneda == objLista[j].Cod_Moneda)
                    {
                        if (lstTMontos[i].DImpMontoInicial >= objLista[j].Imp_LimMinimo && lstTMontos[i].DImpMontoInicial <= objLista[j].Imp_LimMaximo)
                        {
                            break;
                        }
                        else
                        {
                            smessage += lstTMontos[i].SCodMoneda + ": Monto NO ESTÁ entre los límites " + objLista[j].Imp_LimMinimo + " y " + objLista[j].Imp_LimMaximo + ". \n";
                        }
                    }
                }
            }
            if (smessage.Length > 0)
            {
                this.sErrorMessage = smessage;
                return false;
            }
            return true;
        }

        public List<Moneda> ListarMonedas()
        {
            return objCnxTurno.ListarMonedas();
        }

        public string ObtenerPtoVenta()
        {
            string strCodPtoVenta;
            string strIP = (string)Property.htProperty[Define.IP_PTO_VENTA];
            if (strIP == "")
            {
                return null;
            }
            DataTable dtPtoVenta = objCnxTurno.obtenerDetallePuntoVenta(null, strIP);
            if (dtPtoVenta != null && dtPtoVenta.Rows.Count > 0)
            {
                if (dtPtoVenta.Rows[0].ItemArray.GetValue(2).ToString() == "1")
                {
                    return dtPtoVenta.Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            return null;
        }

        public bool CierreTurnoDescuadre()
        {
            bool boRetorno;
            DataTable dtConsulta = new DataTable();
            Conexion objConexion = Resolver.ObtenerConexion(Define.CNX_01);
            dtConsulta = objConexion.ListarParametros((string)Property.htProperty[Define.ID_PARAM_CIERRE_TURNO]);
            boRetorno = dtConsulta.Rows[0].ItemArray.GetValue(4).ToString() == "1" ? true : false;
            return boRetorno;
        }

        public bool EstaTurnoActivo(string strUsuario)
        {
            return !objCnxTurno.verificarTurnoCerradoxUsuario(strUsuario);
        }

        public Turno ObtenerTurnoIniciado(string susuario)
        {
            Turno objTurno = objCnxTurno.ObtenerTurnoIniciado(susuario);
            return objTurno;
        }

        public void ListarCuadreTurno(string strMoneda, string strTurno, ref decimal decEfectivoIni, ref int intTicketInt, ref int intTicketNac, ref decimal decTicketInt, ref decimal decTicketNac, ref int intIngCaja, ref decimal decIngCaja, ref int intVentaMoneda, ref decimal decVentaMoneda, ref int intEgreCaja, ref decimal decEgreCaja, ref int intCompraMoneda, ref decimal decCompraMoneda, ref decimal decEfectivoFinal, ref int intAnulaInt, ref int intAnulaNac, ref int intInfanteInt, ref int intInfanteNac, ref int intCreditoInt, ref int intCreditoNac, ref decimal decCreditoInt, ref decimal decCreditoNac,
            ref int intTicketEfeInt, ref decimal decTicketEfeInt, ref int intTicketTraInt, ref decimal decTicketTraInt, ref int intTicketDebInt, ref decimal decTicketDebInt, ref int intTicketCreInt, ref decimal decTicketCreInt, ref int intTicketCheInt, ref decimal decTicketCheInt,
            ref int intTicketEfeNac, ref decimal decTicketEfeNac, ref int intTicketTraNac, ref decimal decTicketTraNac, ref int intTicketDebNac, ref decimal decTicketDebNac, ref int intTicketCreNac, ref decimal decTicketCreNac, ref int intTicketCheNac, ref decimal decTicketCheNac,
            ref decimal decRecaudadoFin)
        {
            DataTable dtCuadre = objCnxTurno.ListarCuadreTurno(strMoneda, strTurno, ref  decEfectivoIni, ref  intTicketInt, ref  intTicketNac, ref  decTicketInt, ref  decTicketNac, ref  intIngCaja, ref  decIngCaja, ref  intVentaMoneda, ref  decVentaMoneda, ref  intEgreCaja, ref  decEgreCaja, ref  intCompraMoneda, ref  decCompraMoneda, ref  decEfectivoFinal, ref  intAnulaInt, ref  intAnulaNac, ref  intInfanteInt, ref  intInfanteNac, ref  intCreditoInt, ref  intCreditoNac, ref  decCreditoInt, ref  decCreditoNac);
            decEfectivoIni = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(0).ToString());
            intTicketInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(1).ToString());
            intTicketNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(2).ToString());
            decTicketInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(3).ToString());
            decTicketNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(4).ToString());
            intIngCaja = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(5).ToString());
            decIngCaja = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(6).ToString());
            intVentaMoneda = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(7).ToString());
            decVentaMoneda = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(8).ToString());
            intEgreCaja = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(9).ToString());
            decEgreCaja = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(10).ToString());
            intCompraMoneda = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(11).ToString());
            decCompraMoneda = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(12).ToString());
            decEfectivoFinal = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(13).ToString());
            intAnulaInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(14).ToString());
            intAnulaNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(15).ToString());
            intInfanteInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(16).ToString());
            intInfanteNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(17).ToString());
            intCreditoInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(18).ToString());
            intCreditoNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(19).ToString());
            decCreditoInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(20).ToString());
            decCreditoNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(21).ToString());


            intTicketEfeInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(22).ToString());
            decTicketEfeInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(23).ToString());
            intTicketTraInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(24).ToString());
            decTicketTraInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(25).ToString());
            intTicketDebInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(26).ToString());
            decTicketDebInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(27).ToString());
            intTicketCreInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(28).ToString());
            decTicketCreInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(29).ToString());
            intTicketCheInt = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(30).ToString());
            decTicketCheInt = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(31).ToString());
            intTicketEfeNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(32).ToString());
            decTicketEfeNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(33).ToString());
            intTicketTraNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(34).ToString());
            decTicketTraNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(35).ToString());
            intTicketDebNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(36).ToString());
            decTicketDebNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(37).ToString());
            intTicketCreNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(38).ToString());
            decTicketCreNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(39).ToString());
            intTicketCheNac = Int32.Parse(dtCuadre.Rows[0].ItemArray.GetValue(40).ToString());
            decTicketCheNac = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(41).ToString());

            decRecaudadoFin = decimal.Parse(dtCuadre.Rows[0].ItemArray.GetValue(42).ToString());

        }

        private Limite ObtenerLimite(List<Limite> objLista, string strMoneda)
        {
            for (int j = 0; j < objLista.Count; j++)
            {
                if (strMoneda == objLista[j].Cod_Moneda)
                {
                    return objLista[j];
                }
            }
            return null;
        }

        public DataTable ListarTasaCambio()
        {
            return objCnxTurno.ListarTasaCambio();
        }

        public DataTable ListarCampos(string strFieldName)
        {
            return objCnxTurno.ListaCamposxNombre(strFieldName);
        }

        private void MontoCierrexPago(List<TurnoMonto> listaMonto,string strTipPago)
        {
            for (int i = 0; i < listaMonto.Count; i++)
            {
                listaMonto[i].Tip_Pago = strTipPago;
                switch (strTipPago)
                {
                    case Define.TIP_PAGO_EFECTIVO: break;
                    case Define.TIP_PAGO_TRANSF:
                        listaMonto[i].DImpMontoActual = -1;
                        listaMonto[i].DImpMontoFinal = listaMonto[i].Imp_Transferencia;
                        break;
                    case Define.TIP_PAGO_CHEQUE:
                        listaMonto[i].DImpMontoActual = -1;
                        listaMonto[i].DImpMontoFinal = listaMonto[i].Imp_Cheque;
                        break;
                    case Define.TIP_PAGO_CREDITO:
                        listaMonto[i].DImpMontoActual = -1;
                        listaMonto[i].DImpMontoFinal = listaMonto[i].Imp_Tarjeta;
                        break;
                    default: break;
                }
            }
        }
        
    }
}
