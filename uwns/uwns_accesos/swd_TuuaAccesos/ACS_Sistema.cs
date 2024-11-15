using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Threading;
using LAP.TUUA.UTIL;
using Microsoft.Practices.EnterpriseLibrary.Logging;
//using System.Diagnostics;
using LAP.TUUA.ACCESOS;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Security.Permissions;



namespace swd_TuuaAccesos
{
      public partial class ACS_Sistema : ServiceBase
      {
            ACS_Controlador Obj_Controlador;
            [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
            public ACS_Sistema()
            {
                  InitializeComponent();
                  
                 

            }

            

            protected override void OnStart(string[] args)
            {

                  ACS_Util Obj_Util = new ACS_Util();
                  try
                  {

                        System.Threading.Thread.Sleep(10000);
 
                        ErrorHandler.CargarErrorTypes(AppDomain.CurrentDomain.BaseDirectory + "/resources");
                        Obj_Controlador = new ACS_Controlador();
                        Obj_Controlador.Iniciar();
                        
                        IPHostEntry IPs = Dns.GetHostByName("");
                        IPAddress[] Direcciones = IPs.AddressList;
                        String IpClient = Direcciones[0].ToString();
                        //GeneraAlarma
                        if(ACS_Property.BConRemota)
                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000081", "M01", IpClient, "2", "Alerta W0000081", "ACCESOS INICIADO - Cuando se inicia el servicio, Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);

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
                              if (intOcurrencia>0)
                                  strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                        }

                        Obj_Util.EscribirLog(strSTrace, ex.GetType().Name);
                        Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.ACS_Sistema", ex.GetBaseException().Message);
                        Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.ACS_Sistema", ex.StackTrace.ToString());

                  }
            }

          
            protected override void OnStop()
            {
                  try
                  {
                        Obj_Controlador.Close();
                        IPHostEntry IPs = Dns.GetHostByName("");
                        IPAddress[] Direcciones = IPs.AddressList;
                        String IpClient = Direcciones[0].ToString();
                        //GeneraAlarma
                        if (ACS_Property.BConRemota)
                        GestionAlarma.Registrar(ACS_Define.Dsc_SPConfig, "W0000082", "M01", IpClient, "2", "Alerta W0000082", "ACCESOS DETENIDO - Cuando se detiene el servicio, Ip Servicio: " + IpClient, ACS_Define.Usr_Acceso);
                  }
                  catch (Exception )
                  { 
                  }
            }
      }
}
