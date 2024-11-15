using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using LAP.TUUA.UTIL;
using System.Net;
using System.Net.NetworkInformation;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Diagnostics;

namespace LAP.TUUA.VENTAS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process ThisProcess = Process.GetCurrentProcess();

            Process[] AllProcesses = Process.GetProcessesByName(ThisProcess.ProcessName);

            if (AllProcesses.Length > 1)
            {
                MessageBox.Show(ThisProcess.ProcessName + " se esta ejecutando.", ThisProcess.ProcessName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string strpath = AppDomain.CurrentDomain.BaseDirectory + "resources/";
            string strIP = "";

            NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
            //Después, las recorremos y las tratamos.
            if (ni[0].GetIPProperties().UnicastAddresses.Count > 0)
            {
                strIP = ni[0].GetIPProperties().UnicastAddresses[0].Address.ToString();
                if (strIP.Split('.').Length == 0)
                {
                    strIP = ni[0].GetIPProperties().UnicastAddresses[1].Address.ToString();
                }
            }
            else
            {
                strIP = Define.IP_LOCAL_HOST;
            }

            if (!ErrorHandler.CargarErrorTypes(strpath))
            {
                MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Property.CargarPropiedades(strpath))
            {
                MessageBox.Show(ErrorHandler.Desc_Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Property.htProperty.Add(Define.IP_PTO_VENTA, strIP.Trim());
            Property.htProperty.Add("PATHRECURSOS", AppDomain.CurrentDomain.BaseDirectory);

            if (!LabelConfig.LoadData())
            {
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[Define.ERR_008])["MESSAGE"];
                MessageBox.Show(strMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Application.AddMessageFilter(new MessageFilter());
                ErrorHandler.Trace("Inicio", "Inicio de Aplicacion Ventas");
                Application.Run(new Logueo());
            }
            catch (Exception ex)
            {
                string source = ex.Source;

                if (source == "UTIL")
                {
                    MessageBox.Show("Error en configuracion de ruta del log");
                }
                else
                {
                    MessageBox.Show("ERROR EN CONEXION A BASE DE DATOS.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    IPHostEntry IPs = Dns.GetHostByName("");
                    IPAddress[] Direcciones = IPs.AddressList;
                    String IpClient = Direcciones[0].ToString();
                    ////GeneraAlarma - Error al Iniciar una aplicacion de Ventas
                    //GestionAlarma.Registrar((string)Property.htProperty["PATHRECURSOS"], "W0000066", "I20", IpClient, "3", "Alerta W0000066", "Error al Iniciar la aplicacion de ventas  error: " + ex.Message, "");

                    //MessageBox.Show(Define.ERR_DEFAULT, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ErrorHandler.Trace(Define.ERR_DEFAULT, ex);
                }
            }
        }
    }
}