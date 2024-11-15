using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Data;
using System.Collections;
using System.Timers;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.SERVICIOS
{
      class SVC_ClaveUsuario:INTZ_ClienteWS
      {

            private int Num_Dias;

            public SVC_ClaveUsuario() 
            { 

            }
            private void ActualizarVigenciaClave(object source, ElapsedEventArgs e)
            {
                  try
                  {
                        INTZ_Util.EscribirLog(this, "Servicio Vigencia Clave de Usuario  ... En Ejecución");
                        if (Num_Dias>0)
                              Obj_DAO_Usuario.actualizarContrasena(Num_Dias);
                        INTZ_Util.EscribirLog(this, "Servicio Vigencia Clave de Usuario  - Terminado.");

                  }
                  catch (Exception ex)
                  {
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 0)
                        {
                              strSTrace = ex.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              if (intOcurrencia>0)
                                 strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                              

                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, ex.GetType().Name);
                        INTZ_Util.EscribirLog(this, ex.GetBaseException().Message);
                  }
            } 

            /// <summary>
            /// 
            /// </summary>
            public void EjecutarServicio()
            {
                  try
                  {
                        ErrorHandler.CargarErrorTypes(INTZ_Define.Dsc_ErrorConfig);

                        Obj_DAOParameGeneral = new DAO_ParameGeneral(INTZ_Define.Dsc_SPConfig);
                        Obj_DAO_Usuario = new DAO_Usuario(INTZ_Define.Dsc_SPConfig);
                        Lst_ParameGeneral =Obj_DAOParameGeneral.listar();

                        string strFrec = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral,"FU");
                        Num_Dias  = Int32.Parse(INTZ_Util.ObtenerParamGral(Lst_ParameGeneral, "VC"));

                        if (Int32.Parse(strFrec)>0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ActualizarVigenciaClave);
                              tmTasaCambio.Interval = 1000 * Int32.Parse(strFrec);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Vigencia Clave de Usuario  - Iniciado###");
                        }
                  }
                  catch (Exception e)
                  {
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = e.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = e.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        INTZ_Util.EscribirLog(this, strMessage);
                        INTZ_Util.EscribirLog(strSTrace, e.GetType().Name);
                        INTZ_Util.EscribirLog(this, e.GetBaseException().Message);
                        //System.Environment.Exit(0);

                  }
            }
      }
}
