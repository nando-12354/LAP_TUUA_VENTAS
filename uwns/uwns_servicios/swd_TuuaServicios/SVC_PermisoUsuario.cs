using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.UTIL;
using LAP.TUUA.DAO;
using System.Timers;
using System.Collections;
using LAP.TUUA.ENTIDADES;

namespace LAP.TUUA.SERVICIOS
{
      class SVC_PermisoUsuario: INTZ_ClienteWS
      {
            public SVC_PermisoUsuario() 
            { 

            }

            public  void ActualizarPermisosVencidos(object source, ElapsedEventArgs e)
            {
                  try
                  {
                       INTZ_Util.EscribirLog(this, "Servicio Vencimiento Permiso Usuario  ... En Ejecución");
                       Obj_DAO_Usuario.actualizarPermiso();
                       INTZ_Util.EscribirLog(this, "Servicio Vencimiento Permiso Usuario  - Terminado.");
                  }
                  catch (Exception ex)
                  {
                        string strTipError = ErrorHandler.ObtenerCodigoExcepcion(e.GetType().Name);
                        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[strTipError])["MESSAGE"];

                        int intOcurrencia = ex.StackTrace.IndexOf("LAP");
                        String strSTrace = "";
                        if (intOcurrencia > 1)
                        {
                              strSTrace = ex.StackTrace.Substring(intOcurrencia);
                              intOcurrencia = strSTrace.IndexOf(")");
                              if (intOcurrencia>1)
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

                        string strFrec = INTZ_Util.ObtenerParamGral(Lst_ParameGeneral,"FP");

                        if (Int32.Parse(strFrec)>0)
                        {
                              Timer tmTasaCambio = new Timer();
                              tmTasaCambio.Elapsed += new ElapsedEventHandler(ActualizarPermisosVencidos);
                              tmTasaCambio.Interval = 1000 * Int32.Parse(strFrec);
                              tmTasaCambio.Start();
                              INTZ_Util.EscribirLog(this, "###Servicio Vencimiento Permiso Usuario  - Iniciado###");
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
