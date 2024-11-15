///File: ACS_Boarding.cs
///Proposito: Valida Boarding y Opera con Molinete
///Metodos: 
///void ValidarBCBP()
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:04/08/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:04/08/2009


using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.DAO;
using LAP.TUUA.ACCESOS;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Globalization;

namespace LAP.TUUA.ACCESOS
{
    class ACS_Boarding
    {
        #region Fields
        private Hashtable Ht_Boarding;
        private List<ListaDeCampo> Lst_ListaDeCampo;
        private List<Compania> Lst_Compania;
        private BoardingBcbpErr Obj_BoardingBcbpErr;
        private ACS_InterfazPinPad Obj_IntzPinPad;
        private ACS_Util Obj_Util;
        public int estado = 0;
        private ACS_Resolver Obj_Resolver;
        public Usuario Obj_Usuario;
        public Hashtable HT_AirlineUse;
        private LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete;
        private LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados;

        private ACS_FormContingencia frmContingencia;
        public Hashtable Lst_WSBcbp;
        public TimeSpan Time_Start;
        #endregion

        public ACS_Boarding(Hashtable htBoarding, List<Compania> Lst_Compania, List<ListaDeCampo> Lst_ListaDeCampo,
                            ACS_InterfazPinPad Obj_IntzPinPad, ACS_Util Obj_Util, ACS_Resolver Obj_Resolver, LAP.TUUA.AccesoMolinete.Molinete Obj_Molinete,
                            LAP.TUUA.AccesoMolinete.MolineteDiscapacitados Obj_MolineteDiscapacitados, ACS_FormContingencia frmContingencia)
        {
            this.Ht_Boarding = htBoarding;
            this.Obj_Util = Obj_Util;
            this.Lst_Compania = Lst_Compania;
            this.Lst_ListaDeCampo = Lst_ListaDeCampo;
            this.Obj_IntzPinPad = Obj_IntzPinPad;
            this.Obj_Resolver = Obj_Resolver;
            this.Obj_Molinete = Obj_Molinete;
            this.Obj_MolineteDiscapacitados = Obj_MolineteDiscapacitados;
            this.frmContingencia = frmContingencia;
        }


        //private string ValidarBCBPRelacionado( BoardingBcbp Obj_BoardingBcbp)
        //{

        //      if (!Convert.ToBoolean((string)Property.htProperty["BCBPRELACIONADO"]))
        //            return "";

        //      Obj_Resolver.CrearDAOBoardingBcbp();
        //      Reader objReader = new Reader();
        //      if (Ht_Boarding["individual_airline"] != null)
        //      {
        //            Hashtable htObjRel = objReader.Parse_Airline_ACS((string)Ht_Boarding["airline_designator"], (string)Ht_Boarding["individual_airline"]);
        //            if (htObjRel == null)
        //                  return "XML DE BCBP RELACIONADO, NO VALIDO";

        //            if (htObjRel["Flg_BCBP"] != null)
        //            {
        //                  if (((string)htObjRel["Flg_BCBP"]).Equals("1"))
        //                  {
        //                        if (htObjRel["Cod_Unico_BCBP"] != null)
        //                        {
        //                              Obj_BoardingBcbp.StrCodUnicoBcbp = ((string)htObjRel["Cod_Unico_BCBP"]).Trim();
        //                        }

        //                        if (htObjRel["Cod_Unico_BCBP_Rel"] != null)
        //                        {
        //                              Obj_BoardingBcbp.StrCodUnicoBcbpRel =((string)htObjRel["Cod_Unico_BCBP_Rel"]).Trim();
        //                              BoardingBcbp ObjBCBPRel = Obj_Resolver.obtenerDAOBoardingBcbpxCodUnicoBCBP(Obj_BoardingBcbp.SCodCompania,Obj_BoardingBcbp.StrCodUnicoBcbpRel);

        //                              if (ObjBCBPRel != null && ObjBCBPRel.StrTip_Estado.Trim().Equals(ACS_Define.Cod_EstTicRehabilit))
        //                              {
        //                                    Obj_BoardingBcbp.StrTip_Estado = ACS_Define.Cod_EstTicReusado;
        //                                    Obj_BoardingBcbp.StrDsc_Boarding_Estado = ACS_Define.Dsc_EstTicReusado;
        //                              }
        //                              else
        //                              {
        //                                    Obj_BoardingBcbp.StrTip_Estado = ACS_Define.Cod_EstTicUsado;
        //                                    Obj_BoardingBcbp.StrDsc_Boarding_Estado = ACS_Define.Dsc_EstTicUsado;
        //                              }
        //                              return "";
        //                        }
        //                  }
        //            }
        //      }
        //      return "BCBP INVALIDO";
        //}

        private string ValidarBCBPRelacionado(BoardingBcbp Obj_BoardingBcbp)
        {
            if (Obj_BoardingBcbp.Num_Secuencial_Bcbp_Rel > 0)
            {
                //Ingresa Boarding Pass Relacionados HIJOS
                BoardingBcbp objBCBPBase = Obj_Resolver.obtenerDAOBoardingBcbp(null, null, null, null, null, Obj_BoardingBcbp.Num_Secuencial_Bcbp_Rel.ToString(), null);
                if (!objBCBPBase.StrTip_Estado.Trim().Equals(ACS_Define.Cod_EstTicRehabilit))
                {
                    return "BCBP BASE " + objBCBPBase.Dsc_Bcbp_Estado;
                }
            }
            //Ingresa Boarding Pass Relacionados PADRE
            if (Obj_BoardingBcbp.Num_Vuelo_Rel == "")
            {
                return "";
            }
            VueloProgramado ObjVueloProgramado = Obj_Resolver.obtenerVueloProgramado(Obj_BoardingBcbp.Num_Vuelo_Rel.Substring(0, 8), Obj_BoardingBcbp.Num_Vuelo_Rel.Substring(8));
            if (ObjVueloProgramado == null)
            {
                return "VUELO NO PROGRAMADO PARA BCBP RELACIONADO.";
            }
            return "";
        }

        private void MostrarMsgConcurrente()
        {
            if (ACS_Property.estadoPinPad)
            {
                int Rpta = Obj_IntzPinPad.MostrarMsgPINPad(ACS_Define.Cod_RegBCBPOk, ACS_Define.Msg_RegBCBPOk, "");
                if (Rpta != 0)
                {
                    if (Rpta == 2)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                        {
                            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                        }
                        //jcisneros - al igual que ticket - 03-08-2010
                        //Obj_IntzPinPad.Obj_PinPad.Serial.Close();
                        //Obj_IntzPinPad.Obj_PinPad.Serial.Open();
                    }
                    if (Rpta == 1)
                    {
                        //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                        if (ACS_Property.bWriteErrorLog)
                            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                    }
                }
            }
        }

        private void MostrarMsgConcurrente(string strCodTrans, string strMsg)
        {
            if (ACS_Property.estadoPinPad)
            {
                int Rpta = Obj_IntzPinPad.MostrarMsgPINPad(strCodTrans, strMsg.ToUpper(), "");
                if (Rpta == 2)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                    {
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                    }
                    //jcisneros - al igual que ticket - 03-08-2010
                    //Obj_IntzPinPad.Obj_PinPad.Serial.Close();
                    //Obj_IntzPinPad.Obj_PinPad.Serial.Open();
                }

                if (Rpta == 1)
                {
                    //ESilva - Comentado a solicitud de RSalazar - 27-07-2010
                    if (ACS_Property.bWriteErrorLog)
                        Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                }
            }
        }

        /// <summary>
        /// Validacion de BCBP en modo OffLine para un periodo
        /// LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="Obj_BoardingBcbp"></param>
        /// <returns></returns>
        public bool ValidarDuplicidadBCBPOffLine(BoardingBcbp Obj_BoardingBcbp)
        {
            bool isResult = false;

            if (Obj_BoardingBcbp != null && !ACS_Property.BConRemota)
            {
                //Obtener de properties el umbral minimo en minutos para repetir el uso del documento en OFFLINE.
                int iMinutosUmbral = Convert.ToInt32(((string)Property.htProperty["UMBRAL_OFFLINE"]).Trim()); 
                String sTiempoUltUso = Obj_BoardingBcbp.StrLogFechaMod + Obj_BoardingBcbp.StrLogHoraMod;
                DateTime dtTiempo = DateTime.ParseExact(sTiempoUltUso, "yyyyMMddHHmmss", new DateTimeFormatInfo());
                dtTiempo = dtTiempo.AddMinutes(iMinutosUmbral);
                if (DateTime.Now.CompareTo(dtTiempo) > 0)
                {
                    isResult = true;
                }
                else
                {
                    isResult = false;
                }
            }
            else if (Obj_BoardingBcbp == null && !ACS_Property.BConRemota)
            {
                isResult = true;
            }

            return isResult;

        }



        /// <summary>
        /// Valida campos de BCBP 
        /// </summary>
        /// <param name="Obj_BoardingBcbp">Entidad Boarding</param>
        /// <param name="strTrama">Cadena trama</param>
        public void ValidarBCBP(BoardingBcbp Obj_BoardingBcbp, String strTrama)
        {
            IPHostEntry IPs = Dns.GetHostByName("");
            IPAddress[] Direcciones = IPs.AddressList;
            String IpClient = Direcciones[0].ToString();
            
            string strTipVueloMol = (string)ACS_Property.shtMolinete["Tip_Vuelo"];
            bool flagRehab = true;
            string strCodComp = "";
            string strTipVuelo = "";
            string strAirDesig = "";
            string strOrigenAerop = String.Empty; //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES

            if (Obj_BoardingBcbp == null || 
                Obj_BoardingBcbp.StrTip_Estado.Trim().Equals(ACS_Define.Cod_EstTicRehabilit) ||
                ValidarDuplicidadBCBPOffLine(Obj_BoardingBcbp)) //LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
            {
                if (Obj_BoardingBcbp == null)
                {
                    flagRehab = false;
                    Obj_BoardingBcbp = new BoardingBcbp();
                    Obj_BoardingBcbp.Tip_Pasajero = ObtenerTipoPasajero((string)Ht_Boarding["passenger_description"]);
                    Obj_BoardingBcbp.StrFchVuelo = (String)Ht_Boarding["date_flight"];
                    Obj_BoardingBcbp.SNumVuelo = (String)Ht_Boarding["flight_number"];
                    Obj_BoardingBcbp.Cod_Eticket = null;
                    Obj_BoardingBcbp.Dsc_Destino = (String)Ht_Boarding[ACS_Define.DESTINATION];
                    Obj_BoardingBcbp.Nro_boarding = (String)Ht_Boarding[ACS_Define.CHECKIN_SEQUENCE_NUMBER];
                    Obj_BoardingBcbp.Num_Airline_Code = (String)Ht_Boarding[ACS_Define.AIRLINE_NUMERIC_CODE];
                    Obj_BoardingBcbp.Num_Document_Form = (String)Ht_Boarding[ACS_Define.DOCUMENT_FORM_SERIAL_NUMBER];

                    //CMONTES Nuevo campo para validar la unicidad para infante-padre
                    Obj_BoardingBcbp.Num_Checkin = "0";
                    if (Ht_Boarding["checkin_sequence_number"] != null && (string)Ht_Boarding["checkin_sequence_number"] != String.Empty)
                    {
                        Obj_BoardingBcbp.Num_Checkin = (string)Ht_Boarding["checkin_sequence_number"];
                    }
                }

                //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES - BEGIN
                if (!ACS_Property.BConRemota)
                {
                    flagRehab = false;
                }
                strOrigenAerop = (String)Ht_Boarding[ACS_Define.FROM_CITY_AIRPORT_CODE];
                Obj_Util.EscribirLog(this, "Aeropuerto: " + (String)Ht_Boarding[ACS_Define.FROM_CITY_AIRPORT_CODE]);
                if (strOrigenAerop == String.Empty ||
                    strOrigenAerop != (string)Property.htProperty[ACS_Define.FROM_CITY_AIRPORT_CODE.ToUpper()])
                {
                    string strDscError = "AEROPUERTO NO VALIDO";
                    strDscError = "ERROR DE BOARDING;;," + strDscError;
                    Obj_Util.EscribirLog(this, "AEROPUERTO NO VALIDO");
                    MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                    return;
                }
                else
                {
                    Obj_Util.EscribirLog(this, "AEROPUERTO VALIDO");
                }
                
                //LAP-TUUA-9163 - 19-06-2012 (RS) CMONTES - END

                //INICIO validacion de bloqueo de BCBP
                if (flagRehab && Obj_BoardingBcbp.Flg_Bloqueado.Equals(ACS_Define.FLG_ACTIVO))
                {
                    string strDscError = "BP BLOQUEADO";
                    strDscError = "ERROR DE BOARDING;;," + strDscError;
                    MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                    return;
                }
                //FIN validacion de bloqueo de BCBP
                Obj_Resolver.CrearDAOVueloProgramado();
                VueloProgramado ObjVueloProgramado = Obj_Resolver.obtenerVueloProgramado(Obj_BoardingBcbp.StrFchVuelo, (String)Ht_Boarding["flight_number"]);

                if (ObjVueloProgramado != null || flagRehab || !ACS_Property.BConRemota) //LAP-TUUA-9163 - 15-06-2012 (RS) CMONTES
                {
                    strTipVuelo = flagRehab ? Obj_BoardingBcbp.Tip_Vuelo : ObjVueloProgramado.Tip_Vuelo;

                    Obj_Util.EscribirLog(this, "TIPO MOL:" + strTipVueloMol);
                    Obj_Util.EscribirLog(this, "TIPO VUELO:" + strTipVuelo + " - ObjvueloProgram:" + ObjVueloProgramado.Tip_Vuelo);

                    if (strTipVueloMol != strTipVuelo)
                    {
                        string strDscError = "ERROR DE BOARDING;;," + "TIP. VUELO";
                        MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip, strDscError.ToUpper());
                        //GeneraAlarma
                        if (ACS_Property.BConRemota)
                            GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPNoValido + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                        return;
                    }
                    Obj_BoardingBcbp.Tip_Vuelo = strTipVuelo;
                    strAirDesig = flagRehab ? Obj_BoardingBcbp.SCodCompania : (String)Ht_Boarding["airline_designator"];
                    //strAirDesig = flagRehab ? Obj_BoardingBcbp.SCodCompania : ObjVueloProgramado != null?ObjVueloProgramado.Cod_Compania:"";



                    //DC 2022: validar estado de vuelo
                    if (ObjVueloProgramado.Dsc_Estado.Trim().ToUpper() == "CANCELADO" && !flagRehab)
                    {
                        string strDscError = "VUELO CANCELADO";
                        MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip,
                            strDscError.ToUpper());

                        return;
                    }
                    


                    //DC 2019: validar grupo de sala de embarque
                    string cod_mol = (string)Property.htProperty["CODMOLINETE"];
                    //obtener molinete
                    DAO_Molinete daoMolinete = new DAO_Molinete();
                    Molinete molinete = daoMolinete.obtenerMolineteCodigo(cod_mol);
                    if (!string.IsNullOrEmpty(ObjVueloProgramado.Num_Puerta) && !string.IsNullOrEmpty(molinete.Cod_grupo))
                    {
                        //validar puerta de embarque
                        //buscar numero de puerta en grupos de puertas de embarque
                        TUA_Puerta_Grupo grupo = daoMolinete.getGrupoPorNumPuerta(ObjVueloProgramado.Num_Puerta);
                        if (grupo != null)
                        {
                            if (grupo.cod_grupo.Trim().ToLower() != molinete.Cod_grupo.Trim().ToLower())
                            {
                                string strDscError = "PUERTA DE EMBARQUE INCORRECTA.";
                                MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip, strDscError.ToUpper());

                                return;
                            }
                        }
                    }
                    // Fin de validacion de grupo de sala de embarque.

                    //DC 2022: validar ventana de tiempo de 12 horas con respecto a mejor fecha (fech_real, fch_estimada, fch_programada)

                    //obtener mejor hora
                    DateTime? mejorHoraProgramada = (ObjVueloProgramado.Fch_Real ?? ObjVueloProgramado.Fch_Est) ??
                                                    ObjVueloProgramado.Fch_Prog;

                    if (mejorHoraProgramada != null)
                    {
                        DateTime horaActual = DateTime.Now;
                        //validar si el vuelo ha cerrado
                        if (ObjVueloProgramado.Dsc_Estado == "FIN EMBARQ" && mejorHoraProgramada.Value < horaActual.AddHours(-2) && !flagRehab)
                        {
                            string strDscError = "VUELO CERRADO";
                            MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip,
                                strDscError.ToUpper());

                            return;

                        }

                        if (mejorHoraProgramada.Value < horaActual.AddHours(-12) && ObjVueloProgramado.Dsc_Estado != "DEMORADO" && !flagRehab)
                        {
                            //fecha de vuelo expirada
                            string strDscError = "HORA DE VUELO EXPIRADA (-12H).";
                            MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip,
                                strDscError.ToUpper());

                            return;
                        }

                        if (mejorHoraProgramada.Value > horaActual.AddHours(12))
                        {
                            //fecha de vuelo expirada
                            string strDscError = "HORA DE VUELO FUERA DE RANGO (12H).";
                            MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPTip,
                                strDscError.ToUpper());

                            return;
                        }

                    }
                    //FIN validación de ventana de tiempo

                    strCodComp = Obj_Util.ObtenerCodCompania(strAirDesig, Lst_Compania);
                    if (strCodComp.Length > 0)
                    {
                        Obj_Resolver.CrearDAOModVentaComp();
                        ModVentaComp ObjModaVentaComp = Obj_Resolver.obtenerModVentaComp(strCodComp, ACS_Define.Dsc_ModBCBP);
                        if (ObjModaVentaComp != null)
                        {
                            string strFechaProgramado = string.Empty;
                            if (ACS_Property.BConRemota)
                            {
                                strFechaProgramado = flagRehab ? "" : Obj_Util.ValidarVueloProgramado(ObjVueloProgramado, (string)Property.htProperty["UMBRAL_HORAS"], (string)Property.htProperty["ESTADO_DEMORADO"]);
                            }
                            else
                            {
                                strFechaProgramado = "0"; //CMONTES 05/10/2012 para forsar validacion de vigencia de fecha
                            }
                             
                            if ((strFechaProgramado.Trim().Length < 1) || flagRehab || Obj_Util.ValidarVueloExpiradoOffline(Obj_BoardingBcbp.StrFchVuelo)) //LAP-TUUA-9163 - 04-10-2012 (RS) CMONTES
                            {
                                bool bRptaMolinete = true;
                                Obj_BoardingBcbp.SCodCompania = strCodComp.Trim();
                                Obj_BoardingBcbp.SNumVuelo = (String)Ht_Boarding["flight_number"];
                                Obj_BoardingBcbp.StrNomPasajero = (String)Ht_Boarding["passenger_name"];
                                Obj_BoardingBcbp.StrNumAsiento = (String)Ht_Boarding["seat_number"];
                                Obj_BoardingBcbp.StrTrama = strTrama;
                                Obj_BoardingBcbp.StrLogFechaMod = Obj_BoardingBcbp.StrFchVuelo;
                                Obj_BoardingBcbp.StrLogHoraMod = Obj_Util.ObtenerHora();
                                Obj_BoardingBcbp.StrTip_Estado = ACS_Define.Cod_EstTicUsado;
                                Obj_BoardingBcbp.StrDsc_Boarding_Estado = ACS_Define.Dsc_EstTicUsado;
                                Obj_BoardingBcbp.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                                if (ACS_Property.modoContingencia)
                                    Obj_BoardingBcbp.StrLogUsuarioMod = ACS_Define.Usr_Acceso_Cntg;
                                Obj_BoardingBcbp.StrCod_Equipo_Mod = (string)Property.htProperty["CODMOLINETE"];
                                Obj_BoardingBcbp.StrRol = ACS_Define.Rol_Acceso;
                                Obj_BoardingBcbp.StrCodModulo = ACS_Define.Cod_Modulo;
                                Obj_BoardingBcbp.StrCodSubModulo = ACS_Define.Cod_SModRegBoarding;

                                //string strRptaBCBPRel = flagRehab ? ValidarBCBPRelacionado(Obj_BoardingBcbp) : "";
                                //if (!strRptaBCBPRel.Equals(""))
                                //{
                                //    string strDscError = "ERROR DE BOARDING;;," + strRptaBCBPRel;
                                //    MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPNoValido, strDscError.ToUpper());
                                //    //GeneraAlarma
                                //    if (ACS_Property.BConRemota)
                                //        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPNoValido + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                                //    return;
                                //}

                                if (strTrama.Trim().Length > 0)
                                    Obj_BoardingBcbp.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                                else
                                    Obj_BoardingBcbp.StrTipIngreso = ACS_Define.Cod_TipIngMan;
                                if (Obj_Usuario != null)
                                    Obj_BoardingBcbp.StrLogUsuarioMod = Obj_Usuario.SCodUsuario;

                                VerificarPassengerStatus(Obj_BoardingBcbp);
                                Obj_Util.EscribirLog(this, "Fin Validaciones Internas");

                                #region INICIO VALIDACION WS
                                if (!flagRehab)
                                {
                                    CargarAtributosWSEntidadBcbp(Obj_BoardingBcbp);
                                    if (Obj_BoardingBcbp.Flg_Val_WS)
                                    {
                                        Int32 intSleep = Int32.Parse((string)Property.htProperty["TIMEOUT_WSLAN"]);
                                        TimeSpan tsEnd = new TimeSpan(DateTime.Now.Ticks);
                                        Int32 intProcesoVal = Convert.ToInt32(tsEnd.Subtract(Time_Start).Milliseconds);

                                        System.Threading.Thread.Sleep((!(bool)Lst_WSBcbp[ACS_Define.KEY_WS_RESPONSE]) && (intSleep - intProcesoVal > 0) ? intSleep - intProcesoVal : 0);
                                        Obj_BoardingBcbp.Flg_WSError = ACS_Define.FLG_ACTIVO;
                                        if ((bool)Lst_WSBcbp[ACS_Define.KEY_WS_RESPONSE])
                                        {
                                            Obj_BoardingBcbp.Flg_WSError = ACS_Define.FLG_INACTIVO;
                                            Obj_BoardingBcbp.Dsc_Observacion = "WEB SERVICE - " + Lst_WSBcbp[ACS_Define.KEY_WS_TRAMA_RESPONSE].ToString();
                                            System.Threading.Thread.Sleep(100);
                                            CargarDataWSBcbp(Obj_BoardingBcbp);

                                            if (Obj_BoardingBcbp.Tip_Transferencia.Equals(LAP.TUUA.UTIL.Define.NORMAL))
                                            {
                                                Obj_BoardingBcbp.Tip_Transferencia = Obj_BoardingBcbp.Flg_Transito.Equals(ACS_Define.FLG_ACTIVO) ? LAP.TUUA.UTIL.Define.TRANSITO : LAP.TUUA.UTIL.Define.NORMAL;
                                            }

                                            if (Obj_BoardingBcbp.Flg_Val_Tuua)
                                            {
                                                if (Obj_BoardingBcbp.Flg_Pago.Equals(ACS_Define.FLG_INACTIVO))
                                                {
                                                    string strDscError = "BP NO INCLUYE TUUA";
                                                    if (Obj_BoardingBcbp.Flg_Val_Transito && !Obj_BoardingBcbp.Flg_Transito.Equals(ACS_Define.FLG_ACTIVO))
                                                    {
                                                        strDscError = "BP NO INCLUYE TUUA TRANSITO";
                                                    }
                                                    strDscError = "ERROR DE BOARDING;;," + strDscError;
                                                    MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                                                    return;
                                                }
                                            }

                                            if (!Obj_BoardingBcbp.Flg_Val_Tuua && Obj_BoardingBcbp.Flg_Val_Transito)
                                            {
                                                if (Obj_BoardingBcbp.Flg_Transito.Equals(ACS_Define.FLG_ACTIVO))
                                                {
                                                    string strDscError = "PASAJERO EN TRANSITO";
                                                    strDscError = "ERROR DE BOARDING;;," + strDscError;
                                                    MostrarErrorEspecial(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Obj_BoardingBcbp.Dsc_Observacion = Lst_WSBcbp[ACS_Define.KEY_WS_ERROR_MSG].ToString();
                                        }
                                    }
                                    else if (Obj_BoardingBcbp.Flg_Val_Transito && Obj_BoardingBcbp.Tip_Transferencia.Equals(LAP.TUUA.UTIL.Define.TRANSITO))
                                    {
                                        string strDscError = "PASAJERO EN TRANSITO";
                                        strDscError = "ERROR DE BOARDING;;," + strDscError;
                                        MostrarErrorEspecial(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                                        return;
                                    }
                                }
                                #endregion FIN: VALIDACION WS

                                //if (bRptaMolinete)
                                //{
                                //if (flagRehab)
                                //{
                                //    Obj_Resolver.actualizarDAOBoardingBcbp(Obj_BoardingBcbp);
                                //}
                                //else
                                //{


                                #region eochoa - 11/07/2011 - Numero de reintentos para insertar BP
                                int numMaxReintentos = Convert.ToInt32((string)Property.htProperty["NUM_REINTENTOS_BP"]);
                                int numReintentos = 0;
                                bool registroBP = false;
                                Obj_Util.EscribirLog(this, "Inicio Registro BP");
                                for (int i = 0; i < numMaxReintentos; i++)
                                {
                                    if (Obj_Resolver.insertarDAOBoardingBcbp(Obj_BoardingBcbp))
                                    {
                                        if (numReintentos > 1)
                                            Obj_Util.EscribirLog(this, "REGISTRO OK (" + numReintentos + " REINTENTOS)");
                                        //else
                                        //    Obj_Util.EscribirLog(this, "REGISTRO OK");

                                        registroBP = true;
                                        numReintentos++;
                                        break;
                                    }
                                    else
                                        numReintentos++;
                                    System.Threading.Thread.Sleep(100);
                                }
                                if (!registroBP)
                                {
                                    string strDscError = "ERROR REGISTRO BP;;REINTENTE,Error al registrar boarding en base de datos";
                                    MostrarError(strTrama, ACS_Define.Cod_ErrBCBPNoReg, strDscError.ToUpper());
                                    //GeneraAlarma
                                    if (ACS_Property.BConRemota)
                                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPNoReg + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                                    return;
                                }
                                Obj_Util.EscribirLog(this, "Fin Registro BP");
                                #endregion

                                #region Comentado validacion reintentos en BD 11/07/2011
                                ////Validación si el registro se insertó correctamente en la BD
                                //if (!Obj_Resolver.insertarDAOBoardingBcbp(Obj_BoardingBcbp)) 
                                //{
                                //    string strDscError = "ERROR DE BOARDING;;,Error en registro de boading";
                                //    MostrarError(strTrama, ACS_Define.Cod_ErrBCBPNoReg, strDscError.ToUpper());
                                //    //GeneraAlarma
                                //    if (ACS_Property.BConRemota)
                                //        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPNoReg + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                                //    return;
                                //}
                                ////Fin validacion
                                #endregion



                                //************************LUZ VERDE****************************************//
                                System.Threading.Thread thMsg = new System.Threading.Thread(new System.Threading.ThreadStart(MostrarMsgConcurrente));
                                thMsg.Start();

                                
                                estado = 0;

                                if (ACS_Property.modoContingencia)
                                {
                                    frmContingencia.lblMensaje.Text = ACS_Define.Msg_RegBCBPOk.Replace(';', ' ');
                                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Verde);
                                    frmContingencia.Invalidate();
                                }
                                Obj_Util.EscribirLog(this, "Abrir Molinete");
                                Obj_Util.EscribirLog(this, "Encender Luz Verde");
                                if (ACS_Property.estadoMolinete)
                                {
                                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("NORMAL"))
                                        bRptaMolinete = Obj_Molinete.MolineteAprobadoNormal();
                                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                                    {
                                        Obj_MolineteDiscapacitados.MolineteDiscapacitadosAprobadoNormal();
                                        bRptaMolinete = true;
                                    }
                                }
                                //Obj_Util.EscribirLog(this, "Encender Luz Verde");
                                Obj_Util.EscribirLog(this, "Molinete Cerrado");
                                thMsg.Join();
                                //*************************************************************************//
                            }
                            else
                            {
                                string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPFecha, Lst_ListaDeCampo, "ErrorBCBP");
                                strDscError = "ERROR DE BOARDING;;," + strDscError;
                                MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPFecha, strDscError.ToUpper());
                                //GeneraAlarma
                                if (ACS_Property.BConRemota)
                                    GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPMod + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                            }
                        }
                        else
                        {
                            string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPMod, Lst_ListaDeCampo, "ErrorBCBP");
                            strDscError = "ERROR DE BOARDING;;," + strDscError;
                            MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPMod, strDscError.ToUpper());
                            //GeneraAlarma
                            if (ACS_Property.BConRemota)
                                GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPMod + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                        }
                    }
                    else
                    {
                        string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPAero, Lst_ListaDeCampo, "ErrorBCBP");
                        strDscError = "ERROR DE BOARDING;;," + strDscError;
                        MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPAero, strDscError.ToUpper());
                        //GeneraAlarma
                        if (ACS_Property.BConRemota)
                            GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPAero + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                    }
                }
                else
                {
                    string strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBVuelo, Lst_ListaDeCampo, "ErrorBCBP");
                    strDscError = "ERROR DE BOARDING;;," + strDscError;
                    MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBVuelo, strDscError.ToUpper());
                    //GeneraAlarma
                    if (ACS_Property.BConRemota)
                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBVuelo + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                }
            }
            else
            {
                string strDscError = "";
                if (Obj_BoardingBcbp.StrTip_Estado.Trim() == "X")
                {
                    strDscError = Obj_BoardingBcbp.Dsc_Bcbp_Estado;
                }
                else
                {
                    strDscError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrBCBPReg, Lst_ListaDeCampo, "ErrorBCBP");
                    //LAP-TUUA-9163 - 20-06-2012 (RS) CMONTES - BEGIN
                    string strFecha = " " + ACS_Util.ConvertirFecha(Obj_BoardingBcbp.StrLogFechaMod) + " " + ACS_Util.ConvertirHora(Obj_BoardingBcbp.StrLogHoraMod);
                    strDscError = strDscError + ";;," + strFecha;
                    //LAP-TUUA-9163 - 20-06-2012 (RS) CMONTES - END
                }
                strDscError = "ERROR DE BOARDING;;," + strDscError;
                MostrarError(ACS_Define.Cod_RegBCBPErr, strTrama, ACS_Define.Cod_ErrBCBPReg, strDscError.ToUpper());
                //GeneraAlarma
                if (ACS_Property.BConRemota)
                    GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO BOARDING - Codigo Error: " + ACS_Define.Cod_ErrBCBPReg + ", Descripcion Error: " + strDscError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

            }

        }

        /// <summary>
        /// Muestra Error en PinPad
        /// </summary>
        /// <param name="strTrama"></param>
        /// <param name="strTipoError"></param>
        /// <param name="strMsg"></param>
        public void MostrarError(string strTrama, string strTipoError, string strMsg)
        {
            System.Threading.ThreadStart starter = null;
            System.Threading.Thread thMsg = null;
            try
            {
                starter = delegate { MostrarMsgConcurrente(ACS_Define.Cod_MsgPnd, strMsg); };
                thMsg = new System.Threading.Thread(starter);
                thMsg.Start();

                Obj_BoardingBcbpErr = new BoardingBcbpErr();

                Obj_BoardingBcbpErr.SDscTramaBcbp = strTrama;

                Obj_BoardingBcbpErr.Cod_Tip_Error = strTipoError;

                Obj_BoardingBcbpErr.StrLogFechaMod = Obj_Util.ObtenerFecha();
                Obj_BoardingBcbpErr.StrLogHoraMod = Obj_Util.ObtenerHora();

                if (strTrama.Trim().Length > 0)
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                else
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngMan;

                if (Obj_Usuario != null)
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = Obj_Usuario.SCodUsuario;
                else
                {
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                    if (ACS_Property.modoContingencia)
                        Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso_Cntg;
                }

                //Obj_Resolver.CrearDAOBoardingBcbpErr();
                //Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                //*****************Luz Roja***********************//
                Obj_Util.EscribirLog(this, "Encender Luz Roja");

                if (ACS_Property.modoContingencia)
                {
                    frmContingencia.lblMensaje.Text = strMsg.Replace(';', ' ');
                    //frmContingencia.txtbMensaje.Text = strMsg.Replace(';', ' ');
                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Rojo);
                    //frmContingencia.Invalidate();
                }

                if (ACS_Property.estadoMolinete)
                {
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("NORMAL"))
                        Obj_Molinete.MolineteDenegado();
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                        Obj_MolineteDiscapacitados.MolineteDiscapacitadosDenegado();
                }

                //*****************Mostrar Luz Roja****************//
                Obj_Util.EscribirLog(this, strMsg);
            }
            finally
            {
                thMsg.Join();
            }
        }

        public void MostrarError(string strCodTransaccion, string strTrama, string strTipoError, string strMsg)
        {
            System.Threading.ThreadStart starter = null;
            System.Threading.Thread thMsg = null;
            try
            {
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 0");
                starter = delegate { MostrarMsgConcurrente(strCodTransaccion, strMsg); };
                thMsg = new System.Threading.Thread(starter);
                thMsg.Start();
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 1");

                Obj_BoardingBcbpErr = new BoardingBcbpErr();

                Obj_BoardingBcbpErr.SDscTramaBcbp = strTrama;

                Obj_BoardingBcbpErr.Cod_Tip_Error = strTipoError;

                Obj_BoardingBcbpErr.StrLogFechaMod = Obj_Util.ObtenerFecha();
                Obj_BoardingBcbpErr.StrLogHoraMod = Obj_Util.ObtenerHora();

                if (strTrama.Trim().Length > 0)
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                else
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngMan;

                if (Obj_Usuario != null)
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = Obj_Usuario.SCodUsuario;
                else
                {
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                    if (ACS_Property.modoContingencia)
                        Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso_Cntg;
                }
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 2");
                #region INICIO CAMPOS PARA LOG BCBP
                Obj_BoardingBcbpErr.Cod_Equipo_Mod = (string)Property.htProperty["CODMOLINETE"];
                Obj_BoardingBcbpErr.Nom_Pasajero = (String)Ht_Boarding[ACS_Define.PASSENGER_NAME];
                Obj_BoardingBcbpErr.Num_Asiento = (String)Ht_Boarding[ACS_Define.SEAT_NUMBER];
                Obj_BoardingBcbpErr.Fch_Vuelo = (String)Ht_Boarding[ACS_Define.DATE_FLIGHT];
                Obj_BoardingBcbpErr.Num_Vuelo = (String)Ht_Boarding[ACS_Define.FLIGHT_NUMBER];
                Obj_BoardingBcbpErr.Tip_Boarding = (String)Ht_Boarding[ACS_Define.BAR_CODE_FORMAT];
                Obj_BoardingBcbpErr.Log_Error = strMsg.Replace(';', ' ');
                Obj_BoardingBcbpErr.Cod_Compania = Obj_Util.ObtenerCodCompania((String)Ht_Boarding[ACS_Define.AIRLINE_DESIGNATOR], Lst_Compania);
                #endregion FIN CAMPOS PARA LOG BCBP
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 3");
                Obj_Resolver.CrearDAOBoardingBcbpErr();
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 4");
                Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);
                //Obj_Util.EscribirLog(this, "Encender Luz Roja 5");

                //*****************Luz Roja***********************//
                Obj_Util.EscribirLog(this, "Encender Luz Roja");
                estado = 2;
                Obj_Util.EscribirLog(this, strMsg);


                if (ACS_Property.modoContingencia)
                {
                    frmContingencia.lblMensaje.Text = strMsg.Replace(';', ' ');
                    //frmContingencia.txtbMensaje.Text = strMsg.Replace(';', ' ');
                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Rojo);
                    frmContingencia.Invalidate();
                }
                if (ACS_Property.estadoMolinete)
                {
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("NORMAL"))
                        Obj_Molinete.MolineteDenegado();
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                        Obj_MolineteDiscapacitados.MolineteDiscapacitadosDenegado();
                }
            }
            finally
            {
                thMsg.Join();
            }
        }

        public void MostrarErrorEspecial(string strCodTransaccion, string strTrama, string strTipoError, string strMsg)
        {
            System.Threading.ThreadStart starter = null;
            System.Threading.Thread thMsg = null;
            try
            {
                starter = delegate { MostrarMsgConcurrente(strCodTransaccion, strMsg); };
                thMsg = new System.Threading.Thread(starter);
                thMsg.Start();

                Obj_BoardingBcbpErr = new BoardingBcbpErr();

                Obj_BoardingBcbpErr.SDscTramaBcbp = strTrama;

                Obj_BoardingBcbpErr.Cod_Tip_Error = strTipoError;

                Obj_BoardingBcbpErr.StrLogFechaMod = Obj_Util.ObtenerFecha();
                Obj_BoardingBcbpErr.StrLogHoraMod = Obj_Util.ObtenerHora();

                if (strTrama.Trim().Length > 0)
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngAuto;
                else
                    Obj_BoardingBcbpErr.StrTipIngreso = ACS_Define.Cod_TipIngMan;

                if (Obj_Usuario != null)
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = Obj_Usuario.SCodUsuario;
                else
                {
                    Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso;
                    if (ACS_Property.modoContingencia)
                        Obj_BoardingBcbpErr.StrLogUsuarioMod = ACS_Define.Usr_Acceso_Cntg;
                }
                #region INICIO CAMPOS PARA LOG BCBP
                Obj_BoardingBcbpErr.Cod_Equipo_Mod = (string)Property.htProperty["CODMOLINETE"];
                Obj_BoardingBcbpErr.Nom_Pasajero = (String)Ht_Boarding[ACS_Define.PASSENGER_NAME];
                Obj_BoardingBcbpErr.Num_Asiento = (String)Ht_Boarding[ACS_Define.SEAT_NUMBER];
                Obj_BoardingBcbpErr.Fch_Vuelo = (String)Ht_Boarding[ACS_Define.DATE_FLIGHT];
                Obj_BoardingBcbpErr.Num_Vuelo = (String)Ht_Boarding[ACS_Define.FLIGHT_NUMBER];
                Obj_BoardingBcbpErr.Tip_Boarding = (String)Ht_Boarding[ACS_Define.BAR_CODE_FORMAT];
                Obj_BoardingBcbpErr.Log_Error = strMsg.Replace(';', ' ');
                Obj_BoardingBcbpErr.Cod_Compania = Obj_Util.ObtenerCodCompania((String)Ht_Boarding[ACS_Define.AIRLINE_DESIGNATOR], Lst_Compania);
                #endregion FIN CAMPOS PARA LOG BCBP

                Obj_Resolver.CrearDAOBoardingBcbpErr();
                Obj_Resolver.InsertarBoardingBcbpErr(Obj_BoardingBcbpErr);

                //*****************Luz Roja***********************//
                Obj_Util.EscribirLog(this, "Encender Luz Ambar");
                estado = 2;
                Obj_Util.EscribirLog(this, strMsg);


                if (ACS_Property.modoContingencia)
                {
                    frmContingencia.lblMensaje.Text = strMsg.Replace(';', ' ');
                    //frmContingencia.txtbMensaje.Text = strMsg.Replace(';', ' ');
                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Rojo);
                    frmContingencia.Invalidate();
                }
                if (ACS_Property.estadoMolinete)
                {
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("NORMAL"))
                        Obj_Molinete.MolineteDenegadoPasajeroTransito();
                    if (((string)ACS_Property.shtMolinete["Tip_Molinete"]).ToUpper().Equals("DISCAPACITADO"))
                        Obj_MolineteDiscapacitados.MolineteDiscapacitadosDenegadoTransito();
                }
            }
            finally
            {
                thMsg.Join();
            }
        }

        private string ObtenerTipoPasajero(string strTipPasajero)
        {
            switch (strTipPasajero)
            {
                case ACS_Define.TYPE_PASS_CHILD: return LAP.TUUA.UTIL.Define.INFANTE;
                case ACS_Define.TYPE_PASS_INFANT: return LAP.TUUA.UTIL.Define.INFANTE;
                default: return LAP.TUUA.UTIL.Define.ADULTO;
            }
        }

        private void CargarDataWSBcbp(BoardingBcbp objBoarding)
        {
            objBoarding.Flg_Transito = (string)Lst_WSBcbp["flg_transfer_passenger"];
            objBoarding.Flg_Pago = (string)Lst_WSBcbp["flg_paid_tuua"];
            objBoarding.Cod_Eticket = (string)Lst_WSBcbp["NroTicket"];
        }

        private void CargarAtributosWSEntidadBcbp(BoardingBcbp objBcbp)
        {
            if (Lst_WSBcbp != null)
            {
                objBcbp.Flg_Val_WS = Lst_WSBcbp[ACS_Define.ID_VAL_WS] != null && Lst_WSBcbp[ACS_Define.ID_VAL_WS].ToString().Trim().Equals(ACS_Define.FLG_SI_PAGO_TUUA) ? true : false;
                objBcbp.Flg_Val_Tuua = Lst_WSBcbp[ACS_Define.ID_VAL_PAGO_TUUA] != null && Lst_WSBcbp[ACS_Define.ID_VAL_PAGO_TUUA].ToString().Trim().Equals(ACS_Define.FLG_SI_PAGO_TUUA) ? true : false;
                objBcbp.Flg_Val_Transito = Lst_WSBcbp[ACS_Define.ID_VAL_TRANSITO] != null && Lst_WSBcbp[ACS_Define.ID_VAL_TRANSITO].ToString().Trim().Equals(ACS_Define.FLG_SI_PAGO_TUUA) ? true : false;
            }
            else
            {
                objBcbp.Flg_Val_WS = false;
            }
        }

        private void VerificarPassengerStatus(BoardingBcbp objBoarding)
        {
            String strStatusTransito = (string)Property.htProperty["STATUS_TRANSITO"];
            String strPassStatus = (String)Ht_Boarding[ACS_Define.PASSENGER_STATUS];
            objBoarding.Tip_Transferencia = LAP.TUUA.UTIL.Define.NORMAL;

            if (strPassStatus != null)//Modificacion por problema de registro manual de boarding en pinpad 24/03/2011 eochoa
            {
                if (Lst_WSBcbp[ACS_Define.ID_VAL_TRANSITO] != null && Lst_WSBcbp[ACS_Define.ID_VAL_TRANSITO].ToString().Trim().Equals(ACS_Define.FLG_ACTIVO) && strPassStatus != "" && strPassStatus.Equals(strStatusTransito))
                {
                    objBoarding.Tip_Transferencia = LAP.TUUA.UTIL.Define.TRANSITO;
                }
            }
        }
    }
}
