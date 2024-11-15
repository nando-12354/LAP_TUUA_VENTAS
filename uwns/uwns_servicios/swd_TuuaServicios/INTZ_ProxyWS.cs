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
using System.Threading;

namespace LAP.TUUA.SERVICIOS
{
      public class INTZ_ProxyWS
      {
            public XmlSchemas schemas;
            public ServiceDescriptionImporter sdi;

            public INTZ_ProxyWS()
            {
            }

            [SecurityPermissionAttribute(SecurityAction.Demand, Unrestricted = true)]

            public object ObtenerWebService(string strWebServiceAsmxUrl, string strServiceName, 
                                                     string strMethodName, string strNomProtocolo,object[] args)
            {
                System.IO.Stream stream=null;
                object wsvcClass = null;
                try
                {
                    WebRequest webRequest = WebRequest.Create(strWebServiceAsmxUrl);
					webRequest.Timeout = 40000;
                    stream = webRequest.GetResponse().GetResponseStream();
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
                            Monitor.Enter(this);
                            Assembly assembly = AppDomain.CurrentDomain.Load(strServiceName);
                            wsvcClass = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, strServiceName).Unwrap();
                            Monitor.Exit(this);
                        }
                        else
                        { 
                            // Genera e imprime el codigo proxy en C#.
                            CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

                            // Compilar el assembly con "appropriate references"
                            string[] assemblyReferences = new string[5] { "System.dll", "System.Web.Services.dll", "System.Web.dll", "System.Xml.dll", "System.Data.dll" };

                            CompilerParameters parms = new CompilerParameters(assemblyReferences);
                            parms.TempFiles = new TempFileCollection(AppDomain.CurrentDomain.BaseDirectory, false);
                         
                            parms.OutputAssembly = strServiceName+".dll";
                            CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);
                            foreach (CompilerError oops in results.Errors)
                            {
                                INTZ_Util.EscribirLog("INTZ_ProxyWS", "========Compiler error============");
                                INTZ_Util.EscribirLog("INTZ_ProxyWS", oops.ErrorText);
                            }
                            File.Copy(results.PathToAssembly, AppDomain.CurrentDomain.BaseDirectory + "/" + strServiceName + ".dll");
                            wsvcClass = results.CompiledAssembly.CreateInstance(strServiceName);
                            parms.TempFiles.Delete();
                        }
                        wsvcClass.GetType().GetProperty("Timeout").SetValue(wsvcClass, 40000, null);
                        MethodInfo miMethodInfo = wsvcClass.GetType().GetMethod(strMethodName);
                        object r = miMethodInfo.Invoke(wsvcClass, args);
                        
                        return r;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (TimeoutException e)
                {
                    INTZ_Util.EscribirLog("INTZ_ProxyWS", "Tiempo de espera agotado al web service publicado en: " + strWebServiceAsmxUrl);
                    INTZ_Util.EscribirLog("INTZ_ProxyWS", e.StackTrace);
                    return null;
                }
                catch (Exception e)
                {
                    INTZ_Util.EscribirLog("INTZ_ProxyWS", strWebServiceAsmxUrl);
                    INTZ_Util.EscribirLog("INTZ_ProxyWS", e.StackTrace);
                    throw e;
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                }
            }

            public void CheckForImports(string baseWSDLUrl, ServiceDescriptionImporter sdi)
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
      }
}
