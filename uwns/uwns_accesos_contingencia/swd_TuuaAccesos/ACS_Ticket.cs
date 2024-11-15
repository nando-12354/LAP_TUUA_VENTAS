///File: ACS_Ticket.cs
///Proposito: Valida Ticket y Opera con Molinete
///Metodos: 
///void ValidarTicket()
///Version:1.0
///Creado por:Ramiro Salinas
///Fecha de Creación:04/08/2009
///Modificado por: Ramiro Salinas
///Fecha de Modificación:04/08/2009

using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.DAO;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ACCESOS;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Diagnostics;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Globalization;


//namespace uwd_TuuaAccesos
namespace LAP.TUUA.ACCESOS
{
    class ACS_Ticket
    {
        private ACS_Util Obj_Util;
        private List<ListaDeCampo> Lst_ListaDeCampo;
        private List<TipoTicket> Lst_TipoTicket;
        public int estado = 0;
        //private Semaforo frmSemaforo;
        private ACS_Resolver Obj_Resolver;
        public string Tip_Ingreso;
        public Usuario Obj_Usuario;
        private ACS_FormContingenciaDesktop frmContingencia;

        public ACS_Ticket(List<ListaDeCampo> Lst_ListaDeCampo, List<TipoTicket> Lst_TipoTicket, ACS_Resolver Obj_Resolver,
                          ACS_FormContingenciaDesktop frmContingencia)
        {
            
            this.Obj_Util = new ACS_Util();
            this.Lst_ListaDeCampo = Lst_ListaDeCampo;
            this.Lst_TipoTicket = Lst_TipoTicket;
            this.Obj_Resolver = Obj_Resolver;
            this.frmContingencia = frmContingencia;
        }

        private void MsgConcurrente(string strCodTrans, string strMsg)
        {

        }

        private void MostrarMsgConcurrente()
        {

        }

        private void MostrarMsgConcurrente(string strCodTrans, string strMsg)
        {

        }

        /// <summary>
        /// Validacion de TICKET en modo OffLine para un periodo
        /// LAP-TUUA-9163 - 18-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="Obj_BoardingBcbp"></param>
        /// <returns></returns>
        public bool ValidarDuplicidadTICKETOffLine(Ticket objTicket)
        {
            bool isResult = false;

            if (objTicket != null && !ACS_Property.BConRemota)
            {
                //Obtener de properties el umbral minimo en minutos para repetir pase.
                int iMinutosUmbral = Convert.ToInt32(((string)Property.htProperty["UMBRAL_OFFLINE"]).Trim());
                String sTiempoUltUso = objTicket.SLog_Fecha_Mod + objTicket.SLog_Hora_Mod;
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
            else if (objTicket == null && !ACS_Property.BConRemota)
            {
                isResult = true;
            }

            return isResult;

        }


        /// <summary>
        /// Procesar Ticket Leido para Registro OffLine
        /// LAP-TUUA-9163 - 18-06-2012 (RS) CMONTES
        /// </summary>
        /// <param name="objTicket">Entidad Ticket</param>
        /// <param name="strTicket">Nro Ticket</param>
        public void ProcesarTicketOffLine(Ticket objTicket, String strTicket)
        {            
            try
            {
                if (ValidarDuplicidadTICKETOffLine(objTicket))
                {
                    if (objTicket == null)
                    {
                        objTicket = new Ticket();
                    }

                    objTicket.SCodNumeroTicket = strTicket;
                    objTicket.STipEstadoActual = ACS_Define.Cod_EstTicUsado;
                    objTicket.SCodUsuarioVenta = ACS_Define.Usr_Acceso;
                    if (ACS_Property.modoContingencia)
                        objTicket.SCodUsuarioVenta = ACS_Define.Usr_Acceso_Cntg;
                    objTicket.Cod_Pto_Venta = (string)Property.htProperty["CODMOLINETE"];
                    Obj_Resolver.CrearDAOTicket();

                    int numMaxReintentos = Convert.ToInt32((string)Property.htProperty["NUM_REINTENTOS_BP"]);
                    int numReintentos = 0;
                    bool registroTK = false;
                    for (int i = 0; i < numMaxReintentos; i++)
                    {
                        if (Obj_Resolver.ActualizarTicketOffLine(objTicket))
                        {
                            registroTK = true;
                            numReintentos++;
                            break;
                        }
                        else
                            numReintentos++;
                        System.Threading.Thread.Sleep(100);
                    }
                    if (!registroTK)
                    {
                        string strError = "ERROR REGISTRO TK;;REINTENTE,Error al registrar ticket en base de datos";
                        MostrarError(ACS_Define.Cod_RegTickErr, strError.ToUpper());
                        return;
                    }

                    //*****************ENCENDER LUZ SEMAFORO*******************//
                    bool bRptaMolinete = true;
                    System.Threading.Thread thMsg = new System.Threading.Thread(new System.Threading.ThreadStart(MostrarMsgConcurrente));
                    thMsg.Start();
                    //No se sabe si es infante o adulto por lo tanto siempre da luz verde
                    MostrarSemaforo(ACS_Define.Cod_Verde);

                    if (numReintentos > 1)
                        Obj_Util.EscribirLog(this, "REGISTRO OK (" + numReintentos + " REINTENTOS)");
                    else
                        Obj_Util.EscribirLog(this, "REGISTRO OK");

                    Obj_Util.EscribirLog(this, "Encender Luz Verde");

                    estado = 0;

                    Obj_Util.EscribirLog(this, "Abrir Molinete");

                    if (ACS_Property.modoContingencia)
                    {
                        MostrarMsgContingencia(ACS_Define.Msg_RegTickOk.Replace(';', ' '));
                    }

                    Obj_Util.EscribirLog(this, "Cerrar Molinete");
                    thMsg.Join();
                    //*********************************************************//
                }
                else
                {
                    string strFecha = "";
                    strFecha = " " + ACS_Util.ConvertirFecha(objTicket.SLog_Fecha_Mod) + " " + ACS_Util.ConvertirHora(objTicket.SLog_Hora_Mod);
                    string strDscTicket = "USADO";
                    AlmacenarError(objTicket, ACS_Define.Cod_ErrTicketReg);
                    string strError = ";ERROR DE TICKET;;" + strTicket + ";;,";
                    MostrarError(ACS_Define.Cod_RegTickErr, strError + strDscTicket.Trim() + "" + strFecha);
                 }


            }
            catch (Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                Obj_Util.EscribirLog(this, ex.StackTrace);
            }
        }



        /// <summary>
        /// Valida Ticket Leido para Registro
        /// </summary>
        /// <param name="objTicket">Entidad Ticket</param>
        /// <param name="strTicket">Nro Ticket</param>
        public void ValidarTicket(Ticket objTicket, String strTicket)
        {
            try
            {
                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();
                string strTipVueloMolinete, strTipoVuelo;

                if (objTicket == null)  
                {
                    objTicket = new Ticket();
                    objTicket.SCodNumeroTicket = strTicket;
                    AlmacenarError(objTicket, ACS_Define.Cod_ErrTicketNoReg);
                    string strError = Obj_Util.ObtenerDscListaDeCampos(ACS_Define.Cod_ErrTicketNoReg, Lst_ListaDeCampo, "ErrorTicket");
                    strError = ";ERROR DE TICKET;;" + strTicket + ";;," + strError;
                    MostrarError(ACS_Define.Cod_RegTickErr, strError);
                    //GeneraAlarma
                    if (ACS_Property.BConRemota)
                    GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketNoReg + ", Descripcion Error: " + strError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                }
                else
                {
                    strTipVueloMolinete = (string)ACS_Property.shtMolinete["Tip_Vuelo"];
                    strTipoVuelo = Obj_Util.ObtenerTipoVuelo(objTicket.SCodTipoTicket, Lst_TipoTicket);
                    if ((strTipVueloMolinete).Equals(strTipoVuelo))
                    {
                        if (objTicket.STipEstadoActual.Trim().Equals(ACS_Define.Cod_EstTicEmitido) ||
                            objTicket.STipEstadoActual.Trim().Equals(ACS_Define.Cod_EstTicRehabilit) ||
                            objTicket.STipEstadoActual.Trim().Equals(ACS_Define.Cod_EstTicPremitido))
                        {
                            string strFechaProgramado = Obj_Util.ValidarAcceso(objTicket.DtFchVencimiento);
                            if (objTicket.STipEstadoActual.Trim().Equals(ACS_Define.Cod_EstTicPremitido))
                            {
                                strFechaProgramado = "";
                            }
                            if (strFechaProgramado.Length == 0)
                            {
                                objTicket.STipEstadoActual = ACS_Define.Cod_EstTicUsado;
                                objTicket.SCodUsuarioVenta = ACS_Define.Usr_Acceso;
                                if (ACS_Property.modoContingencia)
                                    objTicket.SCodUsuarioVenta = ACS_Define.Usr_Acceso_Cntg;
                                objTicket.Cod_Pto_Venta = (string)Property.htProperty["CODMOLINETE"];
                                
                                Obj_Resolver.CrearDAOTicket();
                                //Obj_Resolver.ActualizarTicket(objTicket);

                                #region eochoa - 11/07/2011 - Numero de reintentos para insertar Ticket
                                int numMaxReintentos = Convert.ToInt32((string)Property.htProperty["NUM_REINTENTOS_BP"]);
                                int numReintentos = 0;
                                bool registroTK = false;
                                for (int i = 0; i < numMaxReintentos; i++)
                                {
                                    if (Obj_Resolver.ActualizarTicket(objTicket))
                                    {
                                        registroTK = true;
                                        numReintentos++;
                                        break;
                                    }
                                    else
                                        numReintentos++;
                                    System.Threading.Thread.Sleep(100);
                                }
                                if (!registroTK)
                                {
                                    string strError = "ERROR REGISTRO TK;;REINTENTE,Error al registrar ticket en base de datos";
                                    MostrarError(ACS_Define.Cod_RegTickErr, strError.ToUpper());
                                    //GeneraAlarma
                                    if (ACS_Property.BConRemota)
                                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketNoReg + ", Descripcion Error: " + strError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                                    return;
                                }
                                #endregion
                                
                                #region Comentado validacion reintentos en BD 11/07/2011
                                ////Validación si el registro se insertó correctamente en la BD
                                //if (!Obj_Resolver.ActualizarTicket(objTicket))
                                //{
                                //    string strError = "ERROR DE TICKET;;,Error en registro de ticket";
                                //    MostrarError(ACS_Define.Cod_RegTickErr, strError.ToUpper());
                                //    //GeneraAlarma
                                //    if (ACS_Property.BConRemota)
                                //        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketNoReg + ", Descripcion Error: " + strError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                                //    return;
                                //}
                                ////Fin validacion
                                #endregion
                                
                                string strTipoPasaj = Obj_Util.ObtenerTipoPasajero(objTicket.SCodTipoTicket, Lst_TipoTicket);
   
                                //*****************ENCENDER LUZ SEMAFORO*******************//
                                bool bRptaMolinete = true;
                                System.Threading.Thread thMsg = new System.Threading.Thread(new System.Threading.ThreadStart(MostrarMsgConcurrente));
                                thMsg.Start();

                                if (strTipoPasaj.Equals(ACS_Define.Cod_TipPasajAdulto))
                                {
                                    MostrarSemaforo(ACS_Define.Cod_Verde);
                                    if (numReintentos > 1)
                                        Obj_Util.EscribirLog(this, "REGISTRO OK (" + numReintentos + " REINTENTOS)");
                                    else
                                        Obj_Util.EscribirLog(this, "REGISTRO OK");

                                    Obj_Util.EscribirLog(this, "Encender Luz Verde");

                                    estado = 0;
                                }
                                else
                                {
                                    MostrarSemaforo(ACS_Define.Cod_Amarillo);
                                    Obj_Util.EscribirLog(this, "Encender Luz Ambar");
                                    estado = 1;
                                }
                                Obj_Util.EscribirLog(this, "Abrir Molinete");

                                if (ACS_Property.modoContingencia)
                                {
                                    MostrarMsgContingencia(ACS_Define.Msg_RegTickOk.Replace(';', ' '));
                                }

                                Obj_Util.EscribirLog(this, "Cerrar Molinete");
                                thMsg.Join();
                                //*********************************************************//
                            }
                            else
                            {
                                AlmacenarError(objTicket, ACS_Define.Cod_ErrTicketReg);
                                MostrarError("Error " + strFechaProgramado.Trim());
                                //GeneraAlarma
                                if (ACS_Property.BConRemota)
                                GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketReg + ", Descripcion Error: " + strFechaProgramado.Trim() + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                            }
                        }
                        else
                        {
                            string strFecha = "";
                            strFecha = " " + ACS_Util.ConvertirFecha(objTicket.SLog_Fecha_Mod)+ " " + ACS_Util.ConvertirHora(objTicket.SLog_Hora_Mod);
                            string strDscTicket = objTicket.Dsc_Estado_Actual;// Obj_Util.ObtenerDscListaDeCampos(objTicket.STipEstadoActual, Lst_ListaDeCampo, "EstadoTicket");
                            if (strDscTicket.IndexOf("USADO") >= 0)
                                strDscTicket = "USADO";
                            AlmacenarError(objTicket, ACS_Define.Cod_ErrTicketReg);
                            string strError = ";ERROR DE TICKET;;" + strTicket + ";;,";
                            MostrarError(ACS_Define.Cod_RegTickErr, strError + strDscTicket.Trim() + "" + strFecha);
                            //GeneraAlarma
                            if (ACS_Property.BConRemota)
                            GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketReg + ", Descripcion Error: " + strError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                        }
                    }
                    else
                    {
                        //*********************LUZ ROJA******************************************
                        string strError = ";ERROR DE TICKET;;" + strTicket + ";;,";
                        AlmacenarError(objTicket, ACS_Define.Cod_ErrTicketTip);
                        //GeneraAlarma
                        if (ACS_Property.BConRemota)
                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000080", "M01", IpClient, "2", "Alerta W0000080", "ERROR EN REGISTRO TICKET - Codigo Error: " + ACS_Define.Cod_ErrTicketTip + ", Descripcion Error: " + strError + ", Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

                        switch (strTipVueloMolinete)
                        {
                            case "I": MostrarError(ACS_Define.Cod_RegTickErr, strError + "debe ser Internacional"); break;
                            case "N": MostrarError(ACS_Define.Cod_RegTickErr, strError + "debe ser Nacional"); break;
                            default: MostrarError(ACS_Define.Cod_RegTickErr, strError + "error tipo de ticket"); break;
                        }
                        //************************************************************************
                    }
                }
            }
            catch(Exception ex)
            {
                Obj_Util.EscribirLog(this, ex.Message);
                Obj_Util.EscribirLog(this, ex.StackTrace);
            }
        }


        public void MostrarMsgContingencia(string strMsg)
        {
            frmContingencia.lblMensaje.Text = strMsg;
            //frmContingencia.txtbMensaje.Text = strMsg;

        }

        public void MostrarSemaforo(int intColor)
        {
            if (ACS_Property.modoContingencia)
            {
                frmContingencia.MostrarSemaforo(intColor);
                frmContingencia.Invalidate();
            }
        }

        public void MostrarError(string strMsg)
        {
            System.Threading.ThreadStart starter = null;
            System.Threading.Thread thMsg = null;
            try
            {
                starter = delegate { MostrarMsgConcurrente(ACS_Define.Cod_MsgPnd, strMsg); };
                thMsg = new System.Threading.Thread(starter);
                thMsg.Start();

                //***************LUZ ROJA******************************
                if (ACS_Property.modoContingencia)
                {
                    MostrarMsgContingencia(strMsg.Replace(';', ' '));
                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Rojo);
                    frmContingencia.Invalidate();
                }

                Obj_Util.EscribirLog(this, "Encender Luz Roja");
                estado = 2;
                //*****************************************************
                Obj_Util.EscribirLog(this, strMsg);

                //if (ACS_Property.estadoPinPad)
                //{
                //      int Rpta = Obj_IntzPinPad.MostrarMsgPINPad(ACS_Define.Cod_MsgPnd, strMsg.ToUpper(), "");
                //      if (Rpta == 2)
                //      {
                //            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                //            Obj_IntzPinPad.Obj_PinPad.Serial.Close();
                //            Obj_IntzPinPad.Obj_PinPad.Serial.Open();
                //      }

                //      if (Rpta == 1)
                //      {
                //            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                //      }
                //}
                //thMsg.Join();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                thMsg.Join();
            }
        }

        public void MostrarError(string strCodTrans, string strMsg)
        {
            System.Threading.ThreadStart starter = null;
            System.Threading.Thread thMsg = null;
            try
            {
                starter = delegate { MostrarMsgConcurrente(strCodTrans, strMsg); };
                thMsg = new System.Threading.Thread(starter);
                thMsg.Start();


                //***************LUZ ROJA******************************
                if (ACS_Property.modoContingencia)
                {
                    MostrarMsgContingencia(strMsg.Replace(';', ' '));
                    frmContingencia.MostrarSemaforo(ACS_Define.Cod_Rojo);
                    frmContingencia.Invalidate();
                }

                Obj_Util.EscribirLog(this, "Encender Luz Roja");
                estado = 2;
                //*****************************************************
                Obj_Util.EscribirLog(this, strMsg);




                //if (ACS_Property.estadoPinPad)
                //{
                //      int Rpta = Obj_IntzPinPad.MostrarMsgPINPad(strCodTrans, strMsg.ToUpper(), "");
                //      if (Rpta == 2)
                //      {
                //            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrComPINPAD);
                //            Obj_IntzPinPad.Obj_PinPad.Serial.Close();
                //            Obj_IntzPinPad.Obj_PinPad.Serial.Open();
                //      }

                //      if (Rpta == 1)
                //      {
                //            Obj_Util.EscribirLog(this, ACS_Define.Dsc_ErrToutPINPAD);
                //      }
                //}
                //thMsg.Join();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                thMsg.Join();
            }

        }

        public void AlmacenarError(Ticket objTicket, string strCodErrorTicket)
        {
            TicketErr objTicketErr = new TicketErr();
            objTicketErr.SCodNumeroTicket = objTicket.SCodNumeroTicket;

            if (Property.htProperty["CODMOLINETE"] != null)
                objTicketErr.SCodMolinete = (string)Property.htProperty["CODMOLINETE"];

            objTicketErr.SCodTipoTicket = objTicket.SCodTipoTicket;
            objTicketErr.STipIngreso = Tip_Ingreso;
            string strDscError = Obj_Util.ObtenerDscListaDeCampos(strCodErrorTicket, Lst_ListaDeCampo, "ErrorTicket");
            objTicketErr.STipError = strCodErrorTicket;
            objTicketErr.SDscError = strDscError;

            if (Obj_Usuario != null)
                objTicketErr.SLogUsuarioMod = Obj_Usuario.SCodUsuario;
            else
            {
                objTicketErr.SLogUsuarioMod = ACS_Define.Usr_Acceso;
                if (ACS_Property.modoContingencia)
                    objTicketErr.SLogUsuarioMod = ACS_Define.Usr_Acceso_Cntg;
            }

            Obj_Resolver.CrearDAOTicketErr();
            Obj_Resolver.InsertarTicketErr(objTicketErr);

        }

    }
}
