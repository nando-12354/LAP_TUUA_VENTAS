using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using LAP.TUUA.ALARMAS;
using System.Net;
using System.Threading;

namespace LAP.TUUA.SERVICIOS
{
    public partial class SVC_Base : ServiceBase
    {
        public INTZ_VueloProgramado objIntzVueloProgramado;

        public SVC_Base()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //  Thread.Sleep(10000);
            try
            {
                INTZ_TasaCambio objIntzTasaCambio = new INTZ_TasaCambio();
                objIntzVueloProgramado = new INTZ_VueloProgramado();
                INTZ_VueloTemporada objIntzVueloTemporada = new INTZ_VueloTemporada();
                SVC_ClaveUsuario objSVCClaveUsuario = new SVC_ClaveUsuario();
                SVC_PermisoUsuario objSVCPermisoUsuario = new SVC_PermisoUsuario();
                SVC_VencimientoTicket objSVCVencimientoTicket = new SVC_VencimientoTicket();
                INTZ_Compania objIntzCompania = new INTZ_Compania();
                SVC_VencimientoBoarding objSVCBoarding = new SVC_VencimientoBoarding();
                SVC_TasaCambio objSVCTasaCambio = new SVC_TasaCambio();
                SVC_PrecioTicket objSVCPrecioTicket = new SVC_PrecioTicket();

                objIntzTasaCambio.EjecutarServicio();
                objIntzVueloProgramado.EjecutarServicio();

                objIntzVueloTemporada.EjecutarServicio();
                objSVCClaveUsuario.EjecutarServicio();
                objSVCPermisoUsuario.EjecutarServicio();
                objSVCVencimientoTicket.EjecutarServicio();
                objIntzCompania.EjecutarServicio();
                objSVCBoarding.EjecutarServicio();
                objSVCTasaCambio.EjecutarServicio();
                objSVCPrecioTicket.EjecutarServicio();

                IPHostEntry IPs = Dns.GetHostByName("");
                IPAddress[] Direcciones = IPs.AddressList;
                String IpClient = Direcciones[0].ToString();
                //GeneraAlarma
                //GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000062", "S01", IpClient, "3", "Alerta W0000062", "SERVICIOS INICIADOS - Cuando se inicia el servicio ", INTZ_Define.Cod_UServicio);
            }
            catch (Exception e)
            {
                //EventLog.WriteEntry("MySource", e.Message);
                //EventLog.WriteEntry("MySource", e.StackTrace);
            }
        }

        protected override void OnStop()
        {
            IPHostEntry IPs = Dns.GetHostByName("");
            IPAddress[] Direcciones = IPs.AddressList;
            String IpClient = Direcciones[0].ToString();
            //GeneraAlarma
            //GestionAlarma.Registrar(INTZ_Define.Dsc_SPConfig, "W0000063", "S01", IpClient, "3", "Alerta W0000063", "SERVICIOS DETENIDOS - Cuando se detienen los servicios ", INTZ_Define.Cod_UServicio);
        }
    }
}
