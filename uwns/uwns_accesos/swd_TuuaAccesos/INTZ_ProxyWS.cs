using System;
using System.Collections.Generic;
using System.Text;

using System.Security.Permissions;
using System.Web.Services;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Web.Services.Discovery;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Net;

namespace LAP.TUUA.ACCESOS
{
    public static class INTZ_ProxyWS
    {
        public static XmlSchemas schemas;
        public static ServiceDescriptionImporter sdi;
        private static HttpWebRequest webRequest;
        private static int Num_Timeout;

        [SecurityPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]

        public static object ObtenerWebService(string strWebServiceAsmxUrl, string strServiceName,
                                                 string strMethodName, string strNomProtocolo, object[] args, Int32 intTimeOut, ref int intTipError)
        {
            System.IO.Stream stream = null;
            object wsvcClass = null;
            ACS_Util Obj_Util = new ACS_Util();
            HttpWebResponse webResponse = null;
            webRequest = null;

            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(strWebServiceAsmxUrl);
                CookieContainer cont = new CookieContainer();
                webRequest.CookieContainer = cont;
                webRequest.ReadWriteTimeout = intTimeOut;
                webRequest.Timeout = intTimeOut;
                Num_Timeout = intTimeOut;
                Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", "Inicio WEB SERVICE BP");
                Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", args[0].ToString());
                //webRequest.ServicePoint.ConnectionLeaseTimeout = intTimeOut;
                //webRequest.ServicePoint.MaxIdleTime = intTimeOut;
                if (!GetPingMS(strWebServiceAsmxUrl.Split('/')[2], intTimeOut))
                {
                    Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", "PING FAILURE - NO ES POSIBLE ESTABLECER CONEXION CON EL WEB SERVICE " + strWebServiceAsmxUrl);
                    intTipError = 0;//sin conexion
                    return null;
                }
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                stream = webResponse.GetResponseStream();

                //System.Net.WebClient wcClient = new System.Net.WebClient();
                //stream = wcClient.OpenRead(strWebServiceAsmxUrl);
                ServiceDescription.Read(stream);
                CodeNamespace cns = new CodeNamespace();

                CodeCompileUnit unit1 = new CodeCompileUnit();
                unit1.Namespaces.Add(cns);

                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                CheckForImports(strWebServiceAsmxUrl, sdi);

                sdi.ProtocolName = strNomProtocolo;
                ServiceDescriptionImportWarnings warning = sdi.Import(cns, unit1);

                if (warning == 0)
                {
                    //if(AppDomain.CurrentDomain.CreateInstance(strServiceName))
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/" + strServiceName + ".dll"))
                    {
                        Assembly assembly = AppDomain.CurrentDomain.Load(strServiceName);
                        wsvcClass = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, strServiceName).Unwrap();
                    }
                    else
                    {
                        // Genera e imprime el codigo proxy en C#.
                        CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                        // Compilar el assembly con "appropriate references"
                        string[] assemblyReferences = new string[5] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                        CompilerParameters parms = new CompilerParameters(assemblyReferences);
                        parms.TempFiles = new TempFileCollection(AppDomain.CurrentDomain.BaseDirectory, false);

                        parms.OutputAssembly = strServiceName + ".dll";

                        CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                        foreach (CompilerError oops in results.Errors)
                        {
                            //Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", "========Compiler error============");
                            //Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", oops.ErrorText);
                        }

                        File.Copy(results.PathToAssembly, AppDomain.CurrentDomain.BaseDirectory + "/" + strServiceName + ".dll");
                        wsvcClass = results.CompiledAssembly.CreateInstance(strServiceName);
                        parms.TempFiles.Delete();
                    }
                    wsvcClass.GetType().GetProperty("Timeout").SetValue(wsvcClass, intTimeOut, null);
                    MethodInfo miMethodInfo = wsvcClass.GetType().GetMethod(strMethodName);
                    object r = miMethodInfo.Invoke(wsvcClass, args);

                    return r;
                }
                else
                {
                    return null;
                }

            }
            catch (TimeoutException exto)
            {
                intTipError = 5;//time out
                throw exto;
            }
            catch (Exception ex)
            {
                //Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", e.Message);
                //Obj_Util.EscribirLog("LAP.TUUA.ACCESOS.INTZ_ProxyWS", e.StackTrace);
                throw ex;
            }
            finally
            {
                webRequest.Abort();
                if (webResponse != null)
                    webResponse.Close();
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        public static void CheckForImports(string baseWSDLUrl, ServiceDescriptionImporter sdi)
        {
            DiscoveryClientProtocol dcp = new DiscoveryClientProtocol();
            dcp.DiscoverAny(baseWSDLUrl);
            dcp.ResolveAll();

            foreach (object osd in dcp.Documents.Values)
            {
                if (osd is ServiceDescription) sdi.AddServiceDescription((ServiceDescription)osd, null, null);
                if (osd is XmlSchema)
                {
                    // store in global schemas variable
                    if (schemas == null) schemas = new XmlSchemas();
                    schemas.Add((XmlSchema)osd);

                    sdi.Schemas.Add((XmlSchema)osd);
                }
            }
        }

        private static bool GetPingMS(string hostNameOrAddress, int intTimeout)
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            if (ping.Send(hostNameOrAddress, intTimeout).Status == System.Net.NetworkInformation.IPStatus.Success)
                return true;
            else return false;
        }


    }
}
