using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAP.TUUA.SERVICIOS
{
      class Remoto
      {
            public Remoto() 
            { 

            }
            public void conectar()
            {
                  //INTZ_Util.EscribirLog(DateTime.Now.ToString(), DateTime.Now.ToString());
                  //string[] MyChar = { "" };
                  ////string xmlTasa = (string)INTZ_ProxyWS.ObtenerWebService("http://cvillanueva/LecturaCliente/Service.asmx?wsdl",
                  ////                                   "Service", "HelloWorld", "Soap", MyChar);
                  ////string[] MyChar = { "" }; 

                  //string xmlTasa = (string)INTZ_ProxyWS.ObtenerWebService("http://ecordova:8080/WebServiceHost/BMaticHostService?wsdl",
                  //                                   "BMaticHostService", "ProcesarTransaccion", "Soap", MyChar);
      
                  //INTZ_Util.EscribirLog(DateTime.Now.ToString(), "Resultado"+xmlTasa);
                  LAP.TUUA.DAO.DAO_BoardingBcbp A = new LAP.TUUA.DAO.DAO_BoardingBcbp(INTZ_Define.Dsc_SPConfig);
                  A.listarxEstado();

                  

            }
      }
}
