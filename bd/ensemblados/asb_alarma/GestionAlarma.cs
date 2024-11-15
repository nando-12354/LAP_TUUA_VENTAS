using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.Xml;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Net;


namespace LAP.TUUA.ALARMASCLR
{
    public static class GestionAlarma
    {
        private static DAO_AlarmaGenerada objDAOAlarmaGenerada = null;
        private static DAO_AlarmaGenerada objDAOCnfgAlarma = null;
        private static DAO_AlarmaGenerada objDAOParameGeneral = null;
        private static string sPathConfig;
        private static bool flagError;
        public static bool Registrar(string sPath, string sCodAlarma, string sCodModulo, string sDscEquipo, string sTipImportancia, string sDscSubject, string sDscBody, string sLogUsuarioMod, string sCodSubModulo, SqlConnection Conn)
        {
            //Obtener la Ip del Sistema
            IPHostEntry IPs = Dns.GetHostByName("");
            IPAddress[] Direcciones = IPs.AddressList;
            String IpClient = Direcciones[0].ToString(); 

            sPathConfig = sPath;
            objDAOAlarmaGenerada = new DAO_AlarmaGenerada(Conn);
            AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();
            objAlarmaGenerada.SCodAlarma = sCodAlarma;
            objAlarmaGenerada.SCodModulo = sCodModulo;
            objAlarmaGenerada.SDscEquipo = IpClient;
            objAlarmaGenerada.STipImportancia = sTipImportancia;
            objAlarmaGenerada.SDscSubject = sDscSubject;
            objAlarmaGenerada.SDscBody = sDscBody;
            objAlarmaGenerada.SLogUsuarioMod = sLogUsuarioMod;
            objAlarmaGenerada.SCodSubModulo = sCodSubModulo; 

            try
            {
                CnfgAlarma ObjCnfgAlarma = null;
                ObjCnfgAlarma = objDAOAlarmaGenerada.obtener(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo);
                if (ObjCnfgAlarma != null)
                {
                    DateTime localNow = DateTime.Now;

                    objAlarmaGenerada.SDscBody += " <br>Fecha Generada:" + localNow.ToString("G") + " <br>Ip Equipo: " + IpClient + " <br><b>" + ObjCnfgAlarma.SDscFinMensaje + "</b>"; 
                    if (objDAOAlarmaGenerada.insertar(objAlarmaGenerada) == false)
                    {
                        //SqlContext.Pipe.Send("Inserto Alarma");
                        return false;
                    }
                    //EnviarAlarma(objAlarmaGenerada);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
           
            return true;
        }


        public static void EnviarAlarma(string sIdAlarmaGenerada, SqlConnection Conn)
        {
            objDAOAlarmaGenerada = new DAO_AlarmaGenerada(Conn);
            AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();
            objAlarmaGenerada = objDAOAlarmaGenerada.obtener(sIdAlarmaGenerada);
            objAlarmaGenerada.ICodAlarmaGenerada = Convert.ToInt32(sIdAlarmaGenerada);   
            EnviarAlarma(objAlarmaGenerada);            
        }


        private static void EnviarAlarma(AlarmaGenerada objAlarmaGenerada)
        {
            CnfgAlarma ObjCnfgAlarma = new CnfgAlarma();
            ObjCnfgAlarma = objDAOAlarmaGenerada.obtener(objAlarmaGenerada.SCodAlarma, objAlarmaGenerada.SCodModulo);
            EnviarEmail(ObtenerDestinatarios(ObjCnfgAlarma.SDscDestinatarios), objAlarmaGenerada);
        }


        private static DataTable ObtenerDestinatarios(string sXmlDestinatarios)
        {
            DataTable dtTablaSeleccion;
            dtTablaSeleccion = new DataTable();
            dtTablaSeleccion.Columns.Add("Cod_Usuario", System.Type.GetType("System.String"));
            dtTablaSeleccion.Columns.Add("Dsc_Email", System.Type.GetType("System.String"));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(sXmlDestinatarios);

            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("user");

            int i = 0;
            foreach (XmlNode node in elemList)
            {
                DataRow rowSeleccion = dtTablaSeleccion.NewRow();
                dtTablaSeleccion.Rows.Add(rowSeleccion);

                dtTablaSeleccion.Rows[i]["Cod_Usuario"] = node.SelectSingleNode("code").InnerText;
                dtTablaSeleccion.Rows[i]["Dsc_Email"] = node.SelectSingleNode("mail").InnerText;
                i++;
            }

            return dtTablaSeleccion;

        }

        private static bool EnviarEmail(DataTable dtDestinatarios, AlarmaGenerada objAlarmaGenerada)
        {
            Hashtable hsConfig = new Hashtable();
            hsConfig = ConfigCorreo(objDAOAlarmaGenerada.obtenerParametros("CC"));

            string sRemitente = (string)hsConfig["remitente"];
            string sServidor = (string)hsConfig["servidor"];
            int iPuerto = Convert.ToInt32(hsConfig["puerto"]);
            string sUser = (string)hsConfig["user"];
            string sPwd = (string)hsConfig["pwd"];
            string sNombre = (string)hsConfig["nombre"];
            bool bSSL = Convert.ToBoolean(hsConfig["SSL"]);
            string sSubject = objAlarmaGenerada.SDscSubject;
            string sBody = objAlarmaGenerada.SDscBody;

            MailMessage message = new MailMessage();

            message.From = new MailAddress(sRemitente, sNombre, System.Text.Encoding.UTF8);

            for (int i = 0; i < dtDestinatarios.Rows.Count; i++)
            {
                message.To.Add(new MailAddress((string)dtDestinatarios.Rows[i]["Dsc_Email"]));
            }

            message.Subject = sSubject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = sBody;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;


            SmtpClient smtpMail = new SmtpClient();
            smtpMail.EnableSsl = bSSL;
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Host = sServidor;
            smtpMail.Port = iPuerto;
            smtpMail.Credentials = new NetworkCredential(sUser, sPwd);
                        
            try
            {
                smtpMail.Send(message);
                ConfirmarCorreo(objAlarmaGenerada, "1");
                flagError = false;
               
                //SqlContext.Pipe.Send("Alerta enviada correctamente ");
                return true;
            }
            catch (Exception ex)
            {
                SqlContext.Pipe.Send("Error en: " + ex.Message);
                //flagError = true;
            }
            finally
            { 
                if(flagError)
                    ConfirmarCorreo(objAlarmaGenerada, "2");
            }
            return true;
        }

        private static void ConfirmarCorreo(AlarmaGenerada objAlarmaGenerada, string sFlagEmail)
        {
            try
            {
                objAlarmaGenerada.SFlgEstadoMail = sFlagEmail;
                objDAOAlarmaGenerada.actualizar(objAlarmaGenerada);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private static Hashtable ConfigCorreo(string SParametros)
        {
            Hashtable Congif = new Hashtable();

            string[] ListaParametros = new string[7];

            ListaParametros = SParametros.Split(';');

            string[] Lista;
            for (int i = 0; i < ListaParametros.Length; i++)
            {
                Lista = new string[2];
                Lista = ListaParametros[i].Split('=');
                Congif.Add(Lista[0], Lista[1]);
            }
            return Congif;
        }

    }
}
