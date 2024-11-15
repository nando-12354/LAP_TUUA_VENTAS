///File: ACS_InterfazPinPad.cs
///Proposito:Inicia,lee y procesa tramas provenientes del PINPad
///Metodos: 
///void Ejecutar()
///int EnviarTransPinPad(String strCodTransaccion, String strMsgTrans)
///void ProcesarBoarding(string strMessage)
///void ProcesarTicket(String strNumTicket,String strMessage)
///void LeerPinPad() 
///int MostrarMsgPINPad(String strCodTransaccion, String strMsgDataAdicional,String strMsgDataTrack2) 
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:15/07/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:05/08/2009

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.UTIL;
using System.Collections;
using System.Data;
using System.ServiceProcess;
using LAP.TUUA.CONVERSOR;


namespace LAP.TUUA.ACCESOS
{
    /// <summary>
    /// ACCESOS: Interfaz del PindPad 
    /// </summary>
    class ACS_InterfazPinPad
    {
        #region Fields
        public Thread obj_HiloInterfazPinpad;
        private ACS_SincronizaLectura Obj_SincPinPad;
        private Ticket Obj_Ticket;
        private Usuario Obj_Usuario;
        private BoardingBcbp Obj_BoardingBcbp;
        private Compania Obj_Compania;
        public ACS_SComPINPAD Obj_PinPad;
        private ACS_Util Obj_Util;

        private List<Compania> Lst_Compania;
        private List<ListaDeCampo> Lst_ListaDeCampo;
        private List<TipoTicket> Lst_TipoTicket;

        private ACS_SCom Obj_SCom;
        private ACS_Resolver Obj_Resolver;

        private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
        private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados;


        private ACS_FormContingencia frmContingencia;
        public bool Flg_Ejecutar;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Obj_Sinc"></param>
        public ACS_InterfazPinPad(ACS_SincronizaLectura Obj_Sinc, ACS_SComPINPAD Obj_PinPad, ACS_SCom Obj_SCom,
                                  ACS_Resolver Obj_Resolver, LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete,
                                  LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados,
                                  ACS_FormContingencia frmContingencia)
        {
            this.obj_HiloInterfazPinpad = new Thread(new ThreadStart(Ejecutar));
            this.Obj_SincPinPad = Obj_Sinc;
            this.Obj_Util = new ACS_Util();
            this.Obj_PinPad = Obj_PinPad;
            this.Obj_SCom = Obj_SCom;
            this.Obj_Resolver = Obj_Resolver;
            this.Obj_Molinete = Obj_Molinete;
            this.Obj_MolineteDiscapacitados = Obj_MolineteDiscapacitados;
            this.frmContingencia = frmContingencia;
            this.Flg_Ejecutar = true;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Inicia hilo de lectura Pin Pad
        /// </summary>
        public void Ejecutar()
        {
            do
            {
                try
                {
                    Monitor.Enter(Obj_SincPinPad);
                    if (!Obj_SincPinPad.Flg_LecturaScaner)
                    {
                        if (ACS_Property.estadoPinPad)
                            //  LeerPinPad();
                            Thread.Sleep(500);
                        else
                            Thread.Sleep(500);

                        if (ACS_Property.estadoPinPad)
                            Obj_PinPad.Clear();

                        Obj_SincPinPad.Flg_LecturaScaner = true;


                    }
                }
                catch (Exception ex)
                {
                    string strTipError = ErrorHandler.ObtenerCodigoExcepcion(ex.GetType().Name);
                    string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                    String strSTrace = "";
                    int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                    if (intOcurrencia > 0)
                    {
                        strSTrace = ex.StackTrace.Substring(intOcurrencia);
                        intOcurrencia = strSTrace.IndexOf(")");
                        strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                    }

                    Obj_Util.EscribirLog(this, strMessage);
                    Obj_Util.EscribirLog(strSTrace, ex.GetType().Name);
                    Obj_Util.EscribirLog(this, ex.GetBaseException().Message);

                    Obj_SincPinPad.Flg_LecturaScaner = true;
                }
                finally
                {
                    Monitor.Exit(Obj_SincPinPad);
                }

            } while ((true) && Flg_Ejecutar);
        }

        /// <summary>
        ///  Envia transaccion al Pin Pad
        /// </summary>
        /// <param name="strCodTransaccion"> Codigo de Transacciom</param>
        /// <param name="strMsgTrans">Mensje de la Transaccion</param>
        /// <returns>El estado de ejecucion del metodo</returns>

        public int EnviarTransPinPad(String strCodTransaccion, String strMsgTrans)
        {
            string strResponseCode = "";
            Obj_PinPad.Clear();
            Obj_PinPad.SetField(ACS_Define.Cod_Aplicacion, "LAP");
            Obj_PinPad.SetField(ACS_Define.Cod_Transaccion, strCodTransaccion);
            Obj_PinPad.SetField(ACS_Define.Dsc_Data_Adicional, strMsgTrans);
            int intRpta = Obj_PinPad.SendTran();
            Obj_PinPad.GetField(ACS_Define.Cod_Response, out strResponseCode);
            //Console.WriteLine(DateTime.Now.Minute + ":" + DateTime.Now.Second+"rpta ="+ intRpta.ToString() + " rescode "+ strResponseCode);
            return intRpta;

        }

        /// <summary>
        /// Procesa boarding
        /// </summary>
        /// <param name="strMessage">Mensaje Pin Pad</param>
        public void ProcesarBoarding(string strMessage)
        {
            String strNumVuelo, strFechaVuelo, strNumAsiento, strPasajero;
            Hashtable htBoarding = new Hashtable();

            //LAP-TUUA-9163 - 05-07-2012 (RS) CMONTES - BEGIN
            //EL AEROPUERTO ORIGEN PARA INGRESO MANUAL BP ES EL CONFIGURADO
            htBoarding.Add(ACS_Define.FROM_CITY_AIRPORT_CODE, (string)Property.htProperty[ACS_Define.FROM_CITY_AIRPORT_CODE.ToUpper()]);
            //Obj_Util.EscribirLog(this, "Aeropuerto: " + (String)htBoarding[ACS_Define.FROM_CITY_AIRPORT_CODE]);
            //LAP-TUUA-9163 - 05-07-2012 (RS) CMONTES - END

            //obtener datos enviados desde el PinPad
            Obj_PinPad.GetField("numvuelo", out strNumVuelo);//AA23
            Obj_PinPad.GetField("fecha", out strFechaVuelo);//0609
            Obj_PinPad.GetField("nasiento", out strNumAsiento);//L45
            Obj_PinPad.GetField("pasajero", out strPasajero);//pasajero

            //strFechaVuelo = DateTime.Now.Year.ToString() + strFechaVuelo.Substring(2, 2) + strFechaVuelo.Substring(0, 2);
            //LAP-TUUA-9163 - 21-06-2012 (RS) CMONTES - BEGIN 
            ACS_Util objUtil = new ACS_Util();
            string strFechaHoy = objUtil.ConvertirDosDigitos(DateTime.Now.Month) + objUtil.ConvertirDosDigitos(DateTime.Now.Day);

            if (strFechaHoy.Equals(ACS_Define.DiaCambioAnio) &&
                strFechaVuelo.Equals(ACS_Define.DiaInicioAnio))
            {
                strFechaVuelo = (DateTime.Today.Year + 1).ToString() + strFechaVuelo.Substring(2, 2) + strFechaVuelo.Substring(0, 2);
            }
            else
            {
                strFechaVuelo = DateTime.Now.Year.ToString() + strFechaVuelo.Substring(2, 2) + strFechaVuelo.Substring(0, 2);
            }
            //LAP-TUUA-9163 - 21-06-2012 (RS) CMONTES - BEGIN 

            
            htBoarding.Add("date_flight", strFechaVuelo);
            htBoarding.Add("seat_number", strNumAsiento);
            htBoarding.Add("passenger_name", strPasajero);

            if (strNumVuelo.Trim().Length > 2)
            {
                htBoarding.Add(LAP.TUUA.CONVERSOR.Define.NroVuelo, strNumVuelo.Substring(2));
                htBoarding.Add(LAP.TUUA.CONVERSOR.Define.Compania, strNumVuelo.Trim().Substring(0,2));
            }

            //INICIO CAMPOS NUEVOS:NRO BOARDING,ETICKET,DESTINO
            htBoarding.Add(ACS_Define.CHECKIN_SEQUENCE_NUMBER, "");
            htBoarding.Add(ACS_Define.DESTINATION, "");
            htBoarding.Add(ACS_Define.ELECTRONIC_TICKET_INDICATOR, "");
            //FIN CAMPOS NUEVOS

            //INICIO asiento vacio LAN1D->INFANTE
            Reader objReader = new Reader();
            objReader.ParseHashtable(htBoarding);
            htBoarding[LAP.TUUA.CONVERSOR.Define.NroVuelo] = strNumVuelo;
            if (strNumAsiento.Trim() == "" && strNumVuelo.Length > 2 && (strNumVuelo.Substring(0, 2).Equals(ACS_Define.COD_AERO_LA) || strNumVuelo.Substring(0, 2).Equals(ACS_Define.COD_AERO_LP)))
            {
                //strNumAsiento = ACS_Define.COD_EMPTY_SEAT_NUMBER;
                htBoarding.Add("passenger_description", ACS_Define.TYPE_PASS_INFANT);
            }
            //FIN asiento vacio LAN1D
            
            Obj_Resolver.CrearDAOBoardingBcbp();
            Obj_BoardingBcbp = Obj_Resolver.obtenerDAOBoardingBcbp(strNumVuelo, (String)htBoarding["date_flight"],
                                                                  (String)htBoarding["seat_number"], (String)htBoarding["passenger_name"]);
            if (strMessage == ACS_Define.Dsc_CONSULTABOARDING)
            {
                if (Obj_BoardingBcbp != null)
                {
                    Obj_Resolver.CrearDAOCompania();
                    Obj_Resolver.CrearDAOBoardingBcbpHist();

                    DataTable dtBCBPHist = Obj_Resolver.obtenerDAOBoardingBcbpHist(Obj_BoardingBcbp.SCodCompania.Trim(), Obj_BoardingBcbp.SNumVuelo.Trim(),
                                                                                   Obj_BoardingBcbp.StrFchVuelo, Obj_BoardingBcbp.StrNumAsiento.Trim(),
                                                                                   Obj_BoardingBcbp.StrNomPasajero.Trim());
                    Obj_Compania = Obj_Resolver.obtenerxcodigoDAOCompania(Obj_BoardingBcbp.SCodCompania);

                    string strEstado = ObtenerEstadoBoarding(dtBCBPHist).Trim();
                    string strFechaVcto = ObtenerFechaVctoBoarding(dtBCBPHist, Obj_BoardingBcbp);
                    string strMsgCabecera = "";
                    string strMsgHist = "";
                    string strEspacioDetalle = ";;;;";
                    string strCompania = Obj_Compania.SDscCompania.Trim();
                    string strNumSecuencial = Obj_BoardingBcbp.INumSecuencial.ToString();
                    string strNoVuelo = Obj_BoardingBcbp.SNumVuelo.Trim();
                    string strFecVuelo = ACS_Util.ConvertirFecha(Obj_BoardingBcbp.StrFchVuelo.Trim());

                    if (strCompania.Length > 9)
                        strCompania = strCompania.Substring(0, 9);
                    if (strEstado.Trim().Length > 4)
                        strEstado = strEstado.Substring(0, 4);
                    if (strNumSecuencial.Length > 9)
                        strNumSecuencial = strNumSecuencial.Substring(0, 9);
                    if (strNoVuelo.Length > 8)
                        strNoVuelo = strNoVuelo.Substring(0, 8);

                    int i = 1;

                    strMsgCabecera = strMsgCabecera + strNumSecuencial + ";" + strCompania + ";" +
                                     strNoVuelo + ";" + strFecVuelo + ";" + strEstado + ";" + strFechaVcto +
                                     strEspacioDetalle;

                    foreach (DataRow dr in dtBCBPHist.Rows)
                    {
                        if (i < 6)
                        {
                            string strEst = dr["Dsc_Boarding_Estado"].ToString().Trim();
                            if (strEst.Trim().Length > 4)
                                strEst = strEst.Substring(0, 4);

                            string strFecha = dr["Log_Fecha_Mod"].ToString().Trim().Substring(6, 2) + "/" +
                                              dr["Log_Fecha_Mod"].ToString().Trim().Substring(4, 2) + "/" +
                                              dr["Log_Fecha_Mod"].ToString().Trim().Substring(2, 2) + " ";
                            string strHora = dr["Log_Hora_Mod"].ToString().Trim().Substring(0, 2) + ":" + dr["Log_Hora_Mod"].ToString().Trim().Substring(2, 2);
                            if (i < 3)
                                strMsgCabecera = strMsgCabecera + ";" + strEst + " " + strFecha + strHora;
                            else
                                strMsgHist = strMsgHist + ";" + strEst + " " + strFecha + strHora;
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (MostrarMsgPINPad(ACS_Define.Cod_ConBoarding, strMsgCabecera, strMsgHist) != 0)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                            Obj_Util.EscribirLog(this, "Error de Comunicación con el Pin Pad ");
                    }
                }
                else
                {
                    if (MostrarMsgPINPad(ACS_Define.Cod_ConBoardingErr, "Boarding Ingresado;;No Existe".ToUpper(), "") != 0)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                            Obj_Util.EscribirLog(this, "Error de Comunicación con el Pin Pad ");
                    }
                    Obj_Util.EscribirLog(this, "Boarding Ingresado No Existe");
                }
            }
            else
            {
                ACS_Boarding objACSBoarding = new ACS_Boarding(htBoarding, Lst_Compania, Lst_ListaDeCampo, this, Obj_Util,
                                                               Obj_Resolver, Obj_Molinete, Obj_MolineteDiscapacitados, frmContingencia);

                
                objACSBoarding.ValidarBCBP(Obj_BoardingBcbp, "");
                
            }
        }

        /// <summary>
        /// obtiene fecha de vencimiento
        /// </summary>
        /// <param name="dtHist"></param>
        /// <returns></returns>
        string ObtenerFechaVctoBoarding(DataTable dtHist, BoardingBcbp objBoardingBCBP)
        {
            string strRpta = "";
            string strFecha = "";
            //obtiene ultima fecha de modificacion de rehabilitacion
            foreach (DataRow dr in dtHist.Rows)
            {
                if (dr["Tip_Estado"].ToString().Trim().ToUpper().Equals("R"))
                {
                    strFecha = dr["Log_Fecha_Mod"].ToString().Trim() + dr["Log_Hora_Mod"].ToString().Trim();
                    break;
                };
            }

            // obtiene fecha de modificacion
            if (strFecha.Length > 0)
            {
                ////
                string strDscValor = "";
                bool flgEncontrado = false;


                DAO_BoardingBcbp Obj_DAOBoarding = new DAO_BoardingBcbp(ACS_Define.Dsc_SPConfig);
                DAO_ParameGeneral Obj_DAOParameGeneral = new DAO_ParameGeneral(ACS_Define.Dsc_SPConfig);
                DAO_ModVentaCompAtr Obj_DAOModVentaCompAtr = new DAO_ModVentaCompAtr(ACS_Define.Dsc_SPConfig);
                DAO_ModalidadAtrib Obj_DAOModalidadAtrib = new DAO_ModalidadAtrib(ACS_Define.Dsc_SPConfig);
                DAO_ModalidadVenta Obj_DAOModVenta = new DAO_ModalidadVenta(ACS_Define.Dsc_SPConfig);

                //obtener modalidad venta comptra atrib
                List<ModVentaCompAtr> Lst_ModVentaCompAtr = Obj_DAOModVentaCompAtr.listar();
                //obtener modalidad atrib
                List<ModalidadAtrib> Lst_ModalidadAtrib = Obj_DAOModalidadAtrib.Listar();
                //obtener parametros general
                List<ParameGeneral> Lst_ParameGeneral = Obj_DAOParameGeneral.listar();
                //obtener modalida bcbp
                ModalidadVenta objModalidadVenta = Obj_DAOModVenta.obtenerxNombre("BCBP");

                //busca en mod venta compra atrib
                int i = 0;
                while ((!flgEncontrado) && (i < Lst_ModVentaCompAtr.Count))
                {
                    if (!flgEncontrado && objBoardingBCBP.SCodCompania != null && objBoardingBCBP.SCodCompania.Equals(Lst_ModVentaCompAtr[i].SCodCompania) &&
                        objModalidadVenta.SCodModalidadVenta != null && objModalidadVenta.SCodModalidadVenta.Equals(Lst_ModVentaCompAtr[i].SCodModalidadVenta)
                        )
                    {
                        if (Lst_ModVentaCompAtr[i].SCodAtributo != null && Lst_ModVentaCompAtr[i].SCodAtributo.Equals("DB"))
                        {
                            strDscValor = Lst_ModVentaCompAtr[i].SDscValor;
                            flgEncontrado = true;
                            break;
                        }
                    }
                    i++;
                }
                //busca en mod atrib
                i = 0;
                while ((!flgEncontrado) && (i < Lst_ModalidadAtrib.Count))
                {
                    if (!flgEncontrado && objModalidadVenta.SCodModalidadVenta != null &&
                         objModalidadVenta.SCodModalidadVenta.CompareTo(Lst_ModalidadAtrib[i].SCodModalidadVenta) == 0
                       )
                    {
                        if (Lst_ModalidadAtrib[i].SCodAtributo.Equals("DB"))
                        {
                            strDscValor = Lst_ModalidadAtrib[i].SDscValor;
                            flgEncontrado = true;
                            break;
                        }
                    }
                    i++;
                }
                //busca en parametro general
                if (!flgEncontrado)
                {
                    strDscValor = ACS_Util.ObtenerParamGral(Lst_ParameGeneral, "DB");
                }

                DateTime dtTickethist = new DateTime(Int32.Parse(strFecha.Substring(0, 4)),
                                                     Int32.Parse(strFecha.Substring(4, 2)),
                                                     Int32.Parse(strFecha.Substring(6, 2)),
                                                     Int32.Parse(strFecha.Substring(8, 2)),
                                                     Int32.Parse(strFecha.Substring(10, 2)),
                                                     Int32.Parse(strFecha.Substring(12, 2))
                                                    );

                DateTime dtVencimiento = dtTickethist.AddDays(+Int32.Parse(strDscValor));
                ////
                strRpta = ACS_Util.ConvertirDigitos(dtVencimiento.Day) + "/" +
                          ACS_Util.ConvertirDigitos(dtVencimiento.Month) + "/" +
                          dtVencimiento.Year.ToString().Substring(2, 2);
            }
            return strRpta;
        }

        /// <summary>
        /// Obtiene estado actual  de Boarding
        /// </summary>
        /// <param name="dtHist"></param>
        /// <returns></returns>
        string ObtenerEstadoBoarding(DataTable dtHist)
        {
            string strRpta = "";

            //Obtiene el ultimo estado
            foreach (DataRow dr in dtHist.Rows)
            {
                strRpta = dr["Dsc_Boarding_Estado"].ToString();
                break;
            }
            return strRpta;
        }
        /// <summary>
        /// Procesa ticket ingresado desde PinPad
        /// </summary>
        /// <param name="strNumTicket">Numero de Ticket</param>
        /// <param name="strMessage">Tipo de proceso</param>

        public void ProcesarTicket(String strNumTicket, String strMessage)
        {
            string[] msgRespuesta;
            int iLongTicket = 16;
            if (ACS_Property.shtMolinete["Long_Ticket"] != null)
            {
                iLongTicket = Convert.ToInt32(ACS_Property.shtMolinete["Long_Ticket"]);
            }

            if (strNumTicket != null && (strNumTicket.Trim()).Length == iLongTicket)
            {
                Obj_Resolver.CrearDAOTicket();
                Obj_Ticket = Obj_Resolver.obtenerTicket(strNumTicket);

                if (strMessage == ACS_Define.Dsc_CONSULTATICKET)
                {
                    Obj_Util.EscribirLog(this, "Inicia consulta de Ticket");
                    Obj_Util.EscribirLog(this, "Ticket:" + strNumTicket);

                    if (Obj_Ticket != null)
                    {
                        DAO_TicketEstHist objDAOTicketHist = new DAO_TicketEstHist(ACS_Define.Dsc_SPConfig);
                        DataTable dtTicketEstHist = objDAOTicketHist.listarTicketEstHist(strNumTicket);

                        msgRespuesta = Obj_Util.GenerarDetalleTicket(Obj_Ticket, Lst_TipoTicket, Lst_Compania,
                                                                     dtTicketEstHist);
                        int Rpta = MostrarMsgPINPad(ACS_Define.Cod_MsgDetPnd, msgRespuesta[0], msgRespuesta[1]);
                        if (Rpta != 0)
                        {
                            if (Rpta == 2)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                                //JAC
                                //Obj_PinPad.Serial.Close();
                                //Obj_PinPad.Serial.Open();
                                //JAC
                            }

                            if (Rpta == 1)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                            }
                        }
                        Obj_Util.EscribirLog(this, "Consulta OK");
                    }
                    else
                    {
                        string msgRpta = "TICKET INGRESADO;;" + strNumTicket + ";;NO EXISTE";
                        int Rpta = EnviarMsgPINPAD(ACS_Define.Cod_ConTickErr, msgRpta, "");
                        if (Rpta != 0)
                        {
                            if (Rpta == 2)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                                //JAC
                                //Obj_PinPad.Serial.Close();
                                //Obj_PinPad.Serial.Open();
                                //JAC
                            }

                            if (Rpta == 1)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                            }
                        }
                        Obj_Util.EscribirLog(this, msgRpta);
                    }
                    Obj_Util.EscribirLog(this, "Finaliza consulta de Ticket");
                }
                else
                {
                    Obj_Util.EscribirLog(this, "Inicia registro de Ticket");
                    Obj_Util.EscribirLog(this, "Ticket:" + strNumTicket);

                    ACS_Ticket objACSTicket = new ACS_Ticket(this, Lst_ListaDeCampo, Lst_TipoTicket, Obj_Resolver, Obj_Molinete,
                                                             Obj_MolineteDiscapacitados, frmContingencia);
                    objACSTicket.Obj_Usuario = Obj_Usuario;
                    objACSTicket.Tip_Ingreso = ACS_Define.Cod_TipIngMan;

                    if (ACS_Property.BConRemota)
                    {
                        objACSTicket.ValidarTicket(Obj_Ticket, strNumTicket);
                    }
                    else
                    {
                        objACSTicket.ProcesarTicketOffLine(Obj_Ticket, strNumTicket);
                    }


                    Obj_Util.EscribirLog(this, "Finaliza registro de Ticket");
                }
            }
            else
            {
                string msgRpta = "ERROR DE FORMATO;;" + strNumTicket + ";;NO ES VALIDO";
                int Rpta = EnviarMsgPINPAD(ACS_Define.Cod_ConTickErr, msgRpta, "");
                if (Rpta != 0)
                {
                    if (Rpta == 2)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                    }

                    if (Rpta == 1)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                    }
                }
                Obj_Util.EscribirLog(this, msgRpta);
            }
        }



        public void IniciarPinPad()
        {
            string strResponseCode, strMessage, strNumTicket;
            DateTime dtInicio;
            dtInicio = DateTime.Now;
            int Rpta;

            //if (ACS_Property.estadoLector)
            //      if (Obj_SCom.serial.IsOpen)
            //            Obj_SCom.serial.Close();

            Obj_Util.EscribirLog(this, "Inicia Flujo Manual desde el Pin Pad".ToUpper());

            Rpta = EnviarTransPinPad(ACS_Define.Cod_IniModAccesoPnd, "Iniciar Modulo de Acceso");
            if (Rpta != 0)
            {
                if (Rpta == 2)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                    {
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                    }
                    Obj_PinPad.Serial.Close();
                    Obj_PinPad.Serial.Open();
                }

                if (Rpta == 1)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                }
            }
            else
            {
                CargarTablasTemp();
                Obj_PinPad.GetField(ACS_Define.Cod_Response, out strResponseCode);
                Obj_PinPad.GetField(ACS_Define.Dsc_Message, out strMessage);
                if (strMessage == ACS_Define.Dsc_CONSULTATICKET || strMessage == ACS_Define.Dsc_REGISTROTICKET)
                {
                    Obj_PinPad.GetField(ACS_Define.Cod_NumeroTicket, out strNumTicket);
                    ProcesarTicket(strNumTicket, strMessage);
                }
                if (strMessage == ACS_Define.Dsc_CONSULTABOARDING || strMessage == ACS_Define.Dsc_REGISTROBOARDING)
                {
                    ProcesarBoarding(strMessage);
                }
                if (strMessage == ACS_Define.Dsc_CANCELADO)
                {
                    Obj_Util.EscribirLog(this, "Flujo Cancelado");
                }
            }
            Obj_Util.EscribirLog(this, "Finaliza Flujo Manual Desde el Pin Pad".ToUpper());
            Obj_Util.EscribirLog(this, "=========================================");

            //if (ACS_Property.estadoLector)
            //      if (!Obj_SCom.serial.IsOpen)
            //            Obj_SCom.serial.Open();
        }

        /// <summary>
        /// Lee Puerto del Pin Pad
        /// </summary>
        public void LeerPinPad()
        {
            string strResponseCode, strMessage, strCodAuthent, strNumTicket, strCodUsuario = "";
            DateTime dtInicio;
            dtInicio = DateTime.Now;

            int Rpta = EnviarTransPinPad(ACS_Define.Cod_IniComPnd, "Iniciar Comunicacion");
            if (Rpta != 0)
            {
                if (Rpta == 2)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                    {
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                    }
                    Obj_PinPad.Serial.Close();
                    Obj_PinPad.Serial.Open();
                }

                if (Rpta == 1)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                }
            }
            else
            {
                Obj_PinPad.GetField(ACS_Define.Cod_Response, out strResponseCode);
                Obj_PinPad.GetField(ACS_Define.Dsc_Message, out strMessage);
                Obj_PinPad.GetField(ACS_Define.Cod_Authent, out strCodAuthent);

                Obj_Resolver.CrearDAOUsuario();
                Obj_Usuario = Obj_Resolver.autenticar(strCodAuthent);

                if (Obj_Usuario != null)
                {
                    strCodUsuario = Obj_Usuario.SCodUsuario;
                }

                if (strMessage == ACS_Define.Dsc_KEYPRESS)
                {
                    if (ACS_Property.estadoLector)
                        if (Obj_SCom.serial.IsOpen)
                            Obj_SCom.serial.Close();

                    Obj_Util.EscribirLog(this, "Inicia Flujo Manual desde el Pin Pad".ToUpper());

                    if (strCodUsuario != null && strCodUsuario.Length > 0)
                    {
                        Rpta = EnviarTransPinPad(ACS_Define.Cod_IniModAccesoPnd, "Iniciar Modulo de Acceso");
                        if (Rpta != 0)
                        {
                            if (Rpta == 2)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                {
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                                }
                                Obj_PinPad.Serial.Close();
                                Obj_PinPad.Serial.Open();
                            }

                            if (Rpta == 1)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                            }
                        }
                        else
                        {
                            CargarTablasTemp();
                            Obj_PinPad.GetField(ACS_Define.Cod_Response, out strResponseCode);
                            Obj_PinPad.GetField(ACS_Define.Dsc_Message, out strMessage);
                            if (strMessage == ACS_Define.Dsc_CONSULTATICKET || strMessage == ACS_Define.Dsc_REGISTROTICKET)
                            {
                                Obj_PinPad.GetField(ACS_Define.Cod_NumeroTicket, out strNumTicket);
                                ProcesarTicket(strNumTicket, strMessage);
                            }
                            if (strMessage == ACS_Define.Dsc_CONSULTABOARDING || strMessage == ACS_Define.Dsc_REGISTROBOARDING)
                            {
                                ProcesarBoarding(strMessage);
                            }
                            if (strMessage == ACS_Define.Dsc_CANCELADO)
                            {
                                Obj_Util.EscribirLog(this, "Flujo Cancelado");
                            }
                        }
                    }
                    else
                    {
                        Rpta = MostrarMsgPINPad(ACS_Define.Cod_MsgPnd, "Usuario no valido".ToUpper(), "");
                        if (Rpta != 0)
                        {
                            if (Rpta == 2)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                {
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                                }
                                Obj_PinPad.Serial.Close();
                                Obj_PinPad.Serial.Open();
                            }

                            if (Rpta == 1)
                            {
                                //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                                if (ACS_Property.bWriteErrorLog)
                                    Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                            }
                        }
                        Obj_Util.EscribirLog(this, "Usuario no valido");
                    }

                    Obj_Util.EscribirLog(this, "Finaliza Flujo Manual Desde el Pin Pad".ToUpper());
                    Obj_Util.EscribirLog(this, "=========================================");

                    if (ACS_Property.estadoLector)
                        if (!Obj_SCom.serial.IsOpen)
                            Obj_SCom.serial.Open();
                }
            }
        }

        /// <summary>
        /// Muestra mensaje en el PinPad
        /// </summary>
        /// <param name="strCodTransaccion">Cod. de Trans.</param>
        /// <param name="strMsgDataAdicional">Mensaje Enviado</param>
        /// <param name="strMsgDataTrack2">Mensaje Adicional Enviado</param>
        /// <returns></returns>

        public int MostrarMsgPINPad(String strCodTransaccion, String strMsgDataAdicional,
                                    String strMsgDataTrack2)
        {
            try
            {
                Obj_PinPad.Clear();
                Obj_PinPad.SetField(ACS_Define.Cod_Aplicacion, "LAP");
                Obj_PinPad.SetField(ACS_Define.Cod_Transaccion, strCodTransaccion);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Adicional, strMsgDataAdicional);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Track2, strMsgDataTrack2);
                int Rpta = Obj_PinPad.SendTran();
                return Rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EnviarMsgPINPAD(String strCodTransaccion, String strMsgDataAdicional,
                                    String strMsgDataTrack2)
        {
            try
            {
                Obj_PinPad.Clear();
                Obj_PinPad.SetField(ACS_Define.Cod_Aplicacion, "LAP");
                Obj_PinPad.SetField(ACS_Define.Cod_Transaccion, strCodTransaccion);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Adicional, strMsgDataAdicional);
                Obj_PinPad.SetField(ACS_Define.Dsc_Data_Track2, strMsgDataTrack2);
                if (Obj_PinPad.SendTran() == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CargarTablasTemp()
        {
            try
            {
                Obj_Resolver.CrearDAOListaDeCampos();
                Obj_Resolver.CrearDAOCompania();
                Obj_Resolver.CrearDAOTipoTicket();

                this.Lst_ListaDeCampo = Obj_Resolver.listarListaDeCampos();
                this.Lst_Compania = Obj_Resolver.listarCompania();
                this.Lst_TipoTicket = Obj_Resolver.listarTipoTicket();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

}
