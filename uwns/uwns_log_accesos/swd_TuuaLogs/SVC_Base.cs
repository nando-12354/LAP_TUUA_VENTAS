using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using LAP.TUUA.UTIL;
using System.Collections;

namespace LAP.TUUA.LOGS
{
    partial class SVC_Base : ServiceBase
    {
        ACS_Controlador Obj_Controlador;
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]

        public SVC_Base()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            ACS_Util Obj_Util = new ACS_Util();
            try
            {
                ErrorHandler.CargarErrorTypes(AppDomain.CurrentDomain.BaseDirectory + "/resources");

                Obj_Controlador = new ACS_Controlador();
                Obj_Controlador.IniciarConexiones();
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
                    if (intOcurrencia > 0)
                        strSTrace = strSTrace.Substring(0, intOcurrencia + 1);
                }

                Obj_Util.EscribirLog(strSTrace, ex.GetType().Name);
                Obj_Util.EscribirLog("LAP.TUUA.LOGS.ACS_Sistema", ex.GetBaseException().Message);
                Obj_Util.EscribirLog("LAP.TUUA.LOGS.ACS_Sistema", ex.StackTrace.ToString());

            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            //            FileStream fs = new FileStream(@"D:\LOGS\accesos20101229.log",
            //FileMode.OpenOrCreate, FileAccess.Write);
            //            StreamWriter m_streamWriter = new StreamWriter(fs);
            //            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            //            m_streamWriter.WriteLine(" mcWindowsService: Service Stopped \n");
            //            m_streamWriter.Flush();
            //            m_streamWriter.Close(); 
            try
            {
                Obj_Controlador.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
