using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Database = Microsoft.SqlServer.Management.Smo.Database;
using Property = LAP.TUUA.UTIL.Property;
using Microsoft.SqlServer.Dts.Runtime;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace LAP.TUUA.ARCHIVAMIENTO
{
    public class BDTUUAHistorica
    {
        private BO_Configuracion objBOConfiguracion;
        private BO_Archivamiento objBOArchivamiento;

        private Archieving archieving;

        private String ipArchivamiento;
        private String dataBaseArchivamiento;
        private String userArchivamiento;
        private String passwordArchivamiento;
        private String rutaBackup;

        public Usuario objUsuario;



        public BDTUUAHistorica(BO_Configuracion objBOConfiguracion, BO_Archivamiento objBOArchivamiento, Usuario objUsuario, Archieving archieving)
        {
            this.objBOConfiguracion = objBOConfiguracion;
            this.objBOArchivamiento = objBOArchivamiento;
            this.objUsuario = objUsuario;
            this.archieving = archieving;
        }

        public void Iniciar()
        {
            DataTable dtReturn = objBOConfiguracion.ListarParametros((String)Property.htProperty[Define.ID_CONFIG_CONEXION_ARCHIVAMIENTO]);
            if (dtReturn.Rows.Count == 0)
            {
                return;
            }
            String configConexionArchivamiento = dtReturn.Rows[0]["Valor"].ToString();
            String[] configuracionCnxArch = configConexionArchivamiento.Split(';');
            if (configuracionCnxArch.Length != 5)
            {
                return;
            }

            for (int i = 0; i < configuracionCnxArch.Length; i++)
            {
                String[] split = configuracionCnxArch[i].Split('=');
                if (split[0].Equals("IP"))
                {
                    ipArchivamiento = split[1].Trim();
                }
                else if (split[0].Equals("DataBase"))
                {
                    dataBaseArchivamiento = split[1].Trim();
                }
                else if (split[0].Equals("User"))
                {
                    userArchivamiento = split[1].Trim();
                }
                else if (split[0].Equals("Password"))
                {
                    passwordArchivamiento = split[1].Trim();
                }
                else if (split[0].Equals("RutaBackup"))
                {
                    rutaBackup = split[1].Trim();
                }
            }

            ServerConnection connArchivamiento = new ServerConnection(ipArchivamiento);
            connArchivamiento.LoginSecure = false;
            connArchivamiento.Login = userArchivamiento;
            connArchivamiento.Password = passwordArchivamiento;

            //string connectionString = "Data Source=172.15.1.10;Initial Catalog=DBTUUA;User ID=sa;Password=123456";
            //SqlConnection Connection = new SqlConnection(connectionString);
            //ServerConnection connArchivamiento = new ServerConnection(Connection);

            Server srvArchivamiento = new Server(connArchivamiento);

            Database archivingDB;
            if (srvArchivamiento.Databases[dataBaseArchivamiento] == null)
            {
                archivingDB = new Database(srvArchivamiento, dataBaseArchivamiento);
                archivingDB.Create();
            }

        }

        public bool ProcesarArchiving(String Cod_Periodo, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, String tablasCopy, String tablasDepura)
        {
            int paso = 1;
            if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, Cod_Periodo, rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), "", objUsuario.SCodUsuario))
            {
                //archieving.RefreshArchieving();
                return false;
            }

            archieving.SetSetPasoHoldOnWindow(paso);

            //Thread.Sleep(3000);//Solo para efectos de probar

            if (PasoVerificacionEstadistico(rangoFechaFinal))
            {
                paso++;
                if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), "", objUsuario.SCodUsuario))
                {
                    //archieving.RefreshArchieving();
                    return false;
                }

                archieving.SetSetPasoHoldOnWindow(paso);

                //Thread.Sleep(3000);//Solo para efectos de probar

                if (PasoCopyInformation(rangoFechaInicial, rangoFechaFinal, Cod_Archivo, tablasCopy))
                {
                    paso++;
                    String Xml_Rango = ConstructXML(Cod_Archivo);
                    if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                    {
                        //archieving.RefreshArchieving();
                        return false;
                    }

                    archieving.SetSetPasoHoldOnWindow(paso);

                    //Thread.Sleep(3000);//Solo para efectos de probar

                    if (PasoDepuraInformation(rangoFechaInicial, rangoFechaFinal, Cod_Archivo, tablasDepura))
                    {
                        paso++;
                        if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }

                        archieving.SetSetPasoHoldOnWindow(paso);

                        //Thread.Sleep(3000);//Solo para efectos de probar

                        if (PasoBackUpDatos(Cod_Archivo))
                        {
                            if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "1", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                            {
                                //archieving.RefreshArchieving();
                                return false;
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
            //archieving.RefreshArchieving();
        }



        public bool

            ReprocesarArchiving(int paso, String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, String tablasCopy, String tablasDepura)
        {
            String Xml_Rango = null;
            while (true)
            {
                switch (paso)
                {
                    case 1:

                        archieving.SetSetPasoHoldOnWindow(paso);

                        //Thread.Sleep(3000);//Solo para efectos de probar

                        if (!PasoVerificacionEstadistico(rangoFechaFinal))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        paso++;
                        if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), "", objUsuario.SCodUsuario))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        break;
                    case 2:

                        archieving.SetSetPasoHoldOnWindow(paso);

                        //Thread.Sleep(3000);//Solo para efectos de probar

                        if (!PasoCopyInformation(rangoFechaInicial, rangoFechaFinal, Cod_Archivo, tablasCopy))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        paso++;
                        Xml_Rango = ConstructXML(Cod_Archivo);
                        if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        break;
                    case 3:

                        archieving.SetSetPasoHoldOnWindow(paso);

                        //Thread.Sleep(3000);//Solo para efectos de probar

                        if (!PasoDepuraInformation(rangoFechaInicial, rangoFechaFinal, Cod_Archivo, tablasDepura))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        paso++;
                        if (Xml_Rango == null)
                        {
                            Xml_Rango = ConstructXML(Cod_Archivo);
                        }
                        if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "0", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        break;
                    case 4:

                        archieving.SetSetPasoHoldOnWindow(paso);

                        //Thread.Sleep(3000);//Solo para efectos de probar

                        if (!PasoBackUpDatos(Cod_Archivo))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        if (Xml_Rango == null)
                        {
                            Xml_Rango = ConstructXML(Cod_Archivo);
                        }
                        if (!objBOArchivamiento.InsUpd_TUA_Archivo(Cod_Archivo, "", rangoFechaInicial, rangoFechaFinal, "1", paso.ToString().PadLeft(2, '0'), Xml_Rango, objUsuario.SCodUsuario))
                        {
                            //archieving.RefreshArchieving();
                            return false;
                        }
                        paso++;//OJO AQUI paso++ va despues.
                        break;
                    default:
                        break;
                }

                if (paso > 4)
                {
                    break;
                }
            }
            return true;
            //archieving.RefreshArchieving();
        }


        private String ConstructXML(String Cod_Archivo)
        {
            try
            {
                String XML = "";
                String returnString = "";


                String modalidadVenta;
                String compania;
                String quantity, min, max;
                String nodo;

                //Tickets
                String nodoTickets = "";

                nodoTickets += "<ticket>";

                //Ticket Solo por modalidad:
                String nodosTicketSoloPorModalidad = "";

                DataTable dtResult = objBOArchivamiento.ConsultaRangosPorTipo_Archivamiento("0", Cod_Archivo,
                                                                                            ipArchivamiento,
                                                                                            dataBaseArchivamiento,
                                                                                            userArchivamiento,
                                                                                            passwordArchivamiento,
                                                                                            returnString);
                if (dtResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        modalidadVenta = dtResult.Rows[i]["Cod_Modalidad_Venta"].ToString();
                        quantity = dtResult.Rows[i]["QUANTITY"].ToString();
                        min = dtResult.Rows[i]["MIN"].ToString();
                        max = dtResult.Rows[i]["MAX"].ToString();

                        nodo = "<modalidad id=\"" + modalidadVenta + "\">";
                        nodo += "<nro>" + quantity + "</nro>";
                        nodo += "<rango_ini>" + min + "</rango_ini>";
                        nodo += "<rango_fin>" + max + "</rango_fin>";
                        nodo += "</modalidad>";
                        nodosTicketSoloPorModalidad += nodo;
                    }
                }

                //Ticket por modalidad y compania
        //COMENTADO POR QUE YA NO SE SEPARA POR MODALIDAD Y COMPAÑIA
                //String nodosTicketPorModalidadYCompania = "";

                //dtResult = objBOArchivamiento.ConsultaRangosPorTipo_Archivamiento("1", Cod_Archivo, ipArchivamiento,
                //                                                                  dataBaseArchivamiento,
                //                                                                  userArchivamiento,
                //                                                                  passwordArchivamiento, returnString);
                //if (dtResult.Rows.Count > 0)
                //{
                //    String modalidadVentaAux = null;

                //    for (int i = 0; i < dtResult.Rows.Count; i++)
                //    {
                //        modalidadVenta = dtResult.Rows[i]["Cod_Modalidad_Venta"].ToString();
                //        if (!modalidadVenta.Equals(modalidadVentaAux))
                //        {
                //            if (i != 0)
                //            {
                //                nodosTicketPorModalidadYCompania += "</modalidad>";
                //            }
                //            nodosTicketPorModalidadYCompania += "<modalidad id=\"" + modalidadVenta + "\">";
                //        }
                //        modalidadVentaAux = modalidadVenta;//EAG 17/12/2009

                //        compania = dtResult.Rows[i]["Cod_Compania"].ToString();
                //        quantity = dtResult.Rows[i]["QUANTITY"].ToString();
                //        min = dtResult.Rows[i]["MIN"].ToString();
                //        max = dtResult.Rows[i]["MAX"].ToString();

                //        nodo = "<compania id=\"" + compania + "\">";
                //        nodo += "<nro>" + quantity + "</nro>";
                //        nodo += "<rango_ini>" + min + "</rango_ini>";
                //        nodo += "<rango_fin>" + max + "</rango_fin>";
                //        nodo += "</compania>";

                //        nodosTicketPorModalidadYCompania += nodo;
                //    }
                //    nodosTicketPorModalidadYCompania += "</modalidad>";
                //}

                //nodoTickets += nodosTicketSoloPorModalidad + nodosTicketPorModalidadYCompania + "</ticket>";
        //FIN
                nodoTickets += nodosTicketSoloPorModalidad + "</ticket>";

                //Boarding por 1 sola modalidad y por compania
                String nodosBoardingPor1ModalidadYPorCompania = "";
                nodosBoardingPor1ModalidadYPorCompania += "<boarding>";

                dtResult = objBOArchivamiento.ConsultaRangosPorTipo_Archivamiento("2", Cod_Archivo, ipArchivamiento,
                                                                                  dataBaseArchivamiento,
                                                                                  userArchivamiento,
                                                                                  passwordArchivamiento, returnString);
                if (dtResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            modalidadVenta = dtResult.Rows[i]["Cod_Modalidad_Venta"].ToString();
                            nodosBoardingPor1ModalidadYPorCompania += "<modalidad id=\"" + modalidadVenta + "\">";
                        }

                        //compania = dtResult.Rows[i]["Cod_Compania"].ToString();
                        quantity = dtResult.Rows[i]["QUANTITY"].ToString();
                        min = dtResult.Rows[i]["MIN"].ToString();
                        max = dtResult.Rows[i]["MAX"].ToString();

                        //nodo = "<compania id=\"" + compania + "\">";
                        nodo = "<nro>" + quantity + "</nro>";
                        nodo += "<rango_ini>" + min + "</rango_ini>";
                        nodo += "<rango_fin>" + max + "</rango_fin>";
                        //nodo += "</compania>";

                        nodosBoardingPor1ModalidadYPorCompania += nodo;
                    }
                    nodosBoardingPor1ModalidadYPorCompania += "</modalidad>";

                }
                nodosBoardingPor1ModalidadYPorCompania += "</boarding>";

                //Operacion sin agrupamiento
                String nodosOperacionSinAgrupamiento = "";
                nodosOperacionSinAgrupamiento += "<operaciones>";

                dtResult = objBOArchivamiento.ConsultaRangosPorTipo_Archivamiento("3", Cod_Archivo, ipArchivamiento,
                                                                                  dataBaseArchivamiento,
                                                                                  userArchivamiento,
                                                                                  passwordArchivamiento, returnString);
                if (dtResult.Rows.Count > 0)
                {
                    for (int i = 0; i < dtResult.Rows.Count; i++) //Solo es un solo registro
                    {
                        quantity = dtResult.Rows[i]["QUANTITY"].ToString();
                        min = dtResult.Rows[i]["MIN"].ToString();
                        max = dtResult.Rows[i]["MAX"].ToString();

                        nodo = "<nro>" + quantity + "</nro>";
                        nodo += "<rango_ini>" + min + "</rango_ini>";
                        nodo += "<rango_fin>" + max + "</rango_fin>";

                        nodosOperacionSinAgrupamiento += nodo;
                    }
                }
                nodosOperacionSinAgrupamiento += "</operaciones>";


                return "<rangos>" + nodoTickets + nodosBoardingPor1ModalidadYPorCompania + nodosOperacionSinAgrupamiento + "</rangos>";
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace(Define.ERR_DEFAULT, "ConstructXML: ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                throw new Exception("Error en el metodo ConstructXML - Detalle: " + ex.Message);
            }
        }


        private bool PasoVerificacionEstadistico(String rangoFechaFinal)
        {
            return objBOArchivamiento.VerificarEstadistico(rangoFechaFinal);
        }

        private bool PasoBackUpDatos(String Cod_Archivo)
        {
            try
            {
                ServerConnection connArchivamiento = new ServerConnection(ipArchivamiento);
                connArchivamiento.LoginSecure = false;
                connArchivamiento.Login = userArchivamiento;
                connArchivamiento.Password = passwordArchivamiento;

                Server srvArchivamiento = new Server(connArchivamiento);
                Backup back = new Backup();
                back.Devices.AddDevice(rutaBackup + "\\" + Cod_Archivo + "_" + dataBaseArchivamiento + ".bak", DeviceType.File);
                back.Database = dataBaseArchivamiento;
                back.Action = BackupActionType.Database;
                back.SqlBackup(srvArchivamiento);
            }
            catch (Exception ex)
            {
                ErrorHandler.Trace(Define.ERR_DEFAULT, "PasoBackUpDatos: ex.Message: " + ex.Message + " - ex.StackTrace: " + ex.StackTrace);
                return false;
            }
            return true;
        }

        private bool PasoCopyInformation(String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, String tablas)
        {
            //String dsc_Message = "";
            //bool ret = objBOArchivamiento.CopiaInformacionArchivamiento(ipArchivamiento, dataBaseArchivamiento, userArchivamiento,
            //                                                 passwordArchivamiento, tablas,
            //                                                 rangoFechaInicial, rangoFechaFinal, Cod_Archivo, ref dsc_Message);
            //return ret;

            try
            {

                String dsc_Message = "";
                bool ret = objBOArchivamiento.CreaTablasArchivamiento(ipArchivamiento, dataBaseArchivamiento, userArchivamiento,
                                                                passwordArchivamiento, tablas, Cod_Archivo, ref dsc_Message);

                if (ret)
                {
                    //Variables SSIS
                    Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();

                    //Cargamos el flujo de migracion
                    Package package = app.LoadPackage(AppDomain.CurrentDomain.BaseDirectory + "resources/CopiaInformacion.dtsx", null);

                    //Cargamos las cadenas de conexion
                    //conexion BD Archivamiento
                    string dbArchivamiento = "Data Source=" + ipArchivamiento + ";User ID=" + userArchivamiento + ";Password=" + passwordArchivamiento + ";Initial Catalog=" + dataBaseArchivamiento + ";Provider=SQLNCLI.1;Auto Translate=False;";
                    package.Connections[0].ConnectionString = dbArchivamiento;

                    //conexion BD central
                    DatabaseSettings dataConfiguration = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
                    string dbCentral = ConfigurationManager.ConnectionStrings[dataConfiguration.DefaultDatabase].ToString() + ";Provider=SQLNCLI.1;Auto Translate=False;";
                    package.Connections[1].ConnectionString = dbCentral;

                    //Cargamos los parametros 
                    Variables vars = package.Variables;

                    DataTable dtEstructura = new DataTable();
                    dtEstructura = objBOArchivamiento.listarEstructuraTablasArchivamiento(tablas, rangoFechaInicial, rangoFechaFinal, Cod_Archivo);

                    foreach (DataRow row in dtEstructura.Rows)
                    {
                        vars[row["Tabla"]].Value = row["TablaArch"];
                        vars["sql" + row["Tabla"] + ""].Value = row["QuerySelect"];
                    }


                    DTSExecResult result = package.Execute();

                    if (result == DTSExecResult.Success)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private bool PasoDepuraInformation(String rangoFechaInicial, String rangoFechaFinal, String Cod_Archivo, String tablas)
        {
            //String dsc_Message = "";
            //bool ret = objBOArchivamiento.DepuraInformacionArchivamiento(tablas,
            //                                                 rangoFechaInicial, rangoFechaFinal, ref dsc_Message);
            //return ret;

            //Variables SSIS
            Microsoft.SqlServer.Dts.Runtime.Application app = new Microsoft.SqlServer.Dts.Runtime.Application();

            //Cargamos el flujo de migracion
            Package package = app.LoadPackage(AppDomain.CurrentDomain.BaseDirectory + "resources/Depuracion.dtsx", null);

            //Cargamos las cadenas de conexion
            //conexion BD Archivamiento
            string dbArchivamiento = "Data Source=" + ipArchivamiento + ";User ID=" + userArchivamiento + ";Password=" + passwordArchivamiento + ";Initial Catalog=" + dataBaseArchivamiento + ";Provider=SQLNCLI.1;Auto Translate=False;";
            package.Connections[0].ConnectionString = dbArchivamiento;

            //conexion BD central
            DatabaseSettings dataConfiguration = (DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
            string dbCentral = ConfigurationManager.ConnectionStrings[dataConfiguration.DefaultDatabase].ToString() + ";Provider=SQLNCLI.1;Auto Translate=False;";
            package.Connections[1].ConnectionString = dbCentral;

            //Cargamos los parametros 
            Variables vars = package.Variables;

            DataTable dtEstructura = new DataTable();
            dtEstructura = objBOArchivamiento.listarEstructuraTablasArchivamiento(tablas, rangoFechaInicial, rangoFechaFinal, Cod_Archivo);

            foreach (DataRow row in dtEstructura.Rows)
            {
                vars["sqlDelete" + row["Tabla"] + ""].Value = row["QueryDelete"];
            }

            DTSExecResult result = package.Execute();

            if (result == DTSExecResult.Success)
                return true;
            else
                return false;

        }
    }
}
