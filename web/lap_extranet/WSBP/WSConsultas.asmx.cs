using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LAP.EXTRANET.DAO;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace LAP.EXTRANET.WSBP
{
    /// <summary>
    /// Summary description for WSConsultas
    /// </summary>
    [WebService(Namespace = "http://www.lap.com.pe/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSConsultas : System.Web.Services.WebService
    {

        [WebMethod]
        public DataSet ConsultaBcbpMensualLeidosMolinete(string sFchDesde, string sFchHasta, string sNumVuelo, string sTipVuelo, string sTipPersona, string sCodIATA, string sSort, int iFila, int iMaxFila, string sPaginacion, string sMostrarResumen, string sTotalRows, string sExcel)
        {
            DataSet dsConsulta = null;
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            dsConsulta = objBCBP.ConsultaBcbpMensualLeidosMolinete(sFchDesde, sFchHasta, sNumVuelo, sTipVuelo, sTipPersona, sCodIATA, sSort, iFila, iMaxFila, sPaginacion, sMostrarResumen, sTotalRows, sExcel);
            return dsConsulta;
        }

        [WebMethod]
        public DataSet ConsultarBoardingLeidosMolinete(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sNroBoarding, string sEticket, string sEstado, string sCodIATA,  string sSort, int iFila, int iMaxFila,string sPaginacion, string sMostrarResumen, string sFlagTotalRows, string sFlgExcel)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataSet dsBP = objBCBP.BoardingLeidosMolinete(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sNroBoarding, sEticket, sEstado, sCodIATA, sSort, iFila, iMaxFila, sPaginacion, sMostrarResumen, sFlagTotalRows, sFlgExcel);
            return dsBP;
        }

        [WebMethod]
        public DataTable listarCamposxNombre(string sCampos)
        {
            DAO_ListaDeCampo objListaCampos = new DAO_ListaDeCampo(HttpContext.Current.Server.MapPath(""));
            DataTable dtLC = objListaCampos.obtenerListaxNombre(sCampos);
            return dtLC;
        }

        [WebMethod]
        public DataTable DetalleBoardingHistorica(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable dtBP = objBCBP.DetalleBoardingHistorica(Num_Secuencial_Bcbp);
            return dtBP;
        }

        [WebMethod]
        public DataTable DetalleBoardingEstHist(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable dtBP = objBCBP.DetalleBoardingEstHist(Num_Secuencial_Bcbp);
            return dtBP;
        }

        [WebMethod]
        public DataTable ListarBoardingAsociados(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable dtBP = objBCBP.ListarBoardingAsociados(Num_Secuencial_Bcbp);
            return dtBP;
        }

        [WebMethod]
        public DataTable ListarParametros(string sIdentificador)
        {
            DAO_ListaDeCampo objBCBP = new DAO_ListaDeCampo(HttpContext.Current.Server.MapPath(""));
            DataTable dtBP = objBCBP.ObtenerParametroGeneral(sIdentificador);
            return dtBP;
        }

        [WebMethod]
        public DataTable ValidarCompaniaBCBP(string sCod_IATA)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable dtBP = objBCBP.ValidarCompaniaBCBP(sCod_IATA);
            return dtBP;
        }
    
        //----------------------------------------------   SERVICIOS WEB XML  ---------------------------------------//
        [WebMethod]
        public XmlDocument ConsultaBcbpMensualLeidosMolineteXML(string sFchDesde, string sFchHasta, string sNumVuelo, string sTipVuelo, string sTipPersona, string sCodIATA, string sSort, int iFila, int iMaxFila, string sPaginacion, string sMostrarResumen, string sTotalRows, string sExcel)
        {

            DataSet dsConsulta = new DataSet();
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            dsConsulta = objBCBP.ConsultaBcbpMensualLeidosMolinete(sFchDesde, sFchHasta, sNumVuelo, sTipVuelo, sTipPersona, sCodIATA, sSort, iFila, iMaxFila, sPaginacion, sMostrarResumen, sTotalRows, sExcel);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");


            foreach (DataTable table in dsConsulta.Tables)
            {
                XmlElement elem = xml.CreateElement(table.TableName);
                int numElementos = table.Columns.Count - 1;
                foreach (DataRow row in table.Rows)
                {
                    XmlElement child = xml.CreateElement("registro");
                    for (int i = 0; i <= numElementos; i++)
                    {
                        child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                    }
                    elem.AppendChild(child);
                }
                xml.DocumentElement.AppendChild(elem);
            }
            return xml;
        }

        [WebMethod]
        public XmlDocument ConsultarBoardingLeidosMolineteXML(string sFchDesde, string sFchHasta, string sHoraDesde, string sHoraHasta, string sFchVuelo, string sNumVuelo, string sNumAsiento, string sNomPasajero, string sNroBoarding, string sEticket, string sEstado, string sCodIATA, string sSort, int iFila, int iMaxFila, string sPaginacion, string sMostrarResumen, string sFlagTotalRows, string sFlgExcel)
        {
            DataSet dsConsulta = new DataSet();
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            dsConsulta = objBCBP.BoardingLeidosMolinete(sFchDesde, sFchHasta, sHoraDesde, sHoraHasta, sFchVuelo, sNumVuelo, sNumAsiento, sNomPasajero, sNroBoarding, sEticket, sEstado, sCodIATA, sSort, iFila, iMaxFila, sPaginacion, sMostrarResumen, sFlagTotalRows, sFlgExcel);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            foreach (DataTable table in dsConsulta.Tables)
            {
                XmlElement elem = xml.CreateElement(table.TableName);
                int numElementos = table.Columns.Count - 1;
                foreach (DataRow row in table.Rows)
                {
                    XmlElement child = xml.CreateElement("registro");
                    for (int i = 0; i <= numElementos; i++)
                    {
                        child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                    }
                    elem.AppendChild(child);
                }
                xml.DocumentElement.AppendChild(elem);
            }
            return xml;
        }

        [WebMethod]
        public XmlDocument listarCamposxNombreXML(string sCampos)
        {
            DAO_ListaDeCampo objListaCampos = new DAO_ListaDeCampo(HttpContext.Current.Server.MapPath(""));
            DataTable table = objListaCampos.obtenerListaxNombre(sCampos);
            table.TableName = "ListaCampos";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);
         
            return xml;
        }

        [WebMethod]
        public XmlDocument DetalleBoardingHistoricaXML(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable table = objBCBP.DetalleBoardingHistorica(Num_Secuencial_Bcbp);
            //table.TableName = "ListaCampos";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);

            return xml;
        }

        [WebMethod]
        public XmlDocument DetalleBoardingEstHistXML(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable table = objBCBP.DetalleBoardingEstHist(Num_Secuencial_Bcbp);
            //table.TableName = "ListaCampos";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);

            return xml;
        }

        [WebMethod]
        public XmlDocument ListarBoardingAsociadosXML(string Num_Secuencial_Bcbp)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable table = objBCBP.ListarBoardingAsociados(Num_Secuencial_Bcbp);
            //table.TableName = "ListaCampos";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);

            return xml;
        }

        [WebMethod]
        public XmlDocument ListarParametrosXML(string sIdentificador)
        {
            DAO_ListaDeCampo objBCBP = new DAO_ListaDeCampo(HttpContext.Current.Server.MapPath(""));
            DataTable table = objBCBP.ObtenerParametroGeneral(sIdentificador);
            //table.TableName = "ListaCampos";

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);

            return xml;
        }


        [WebMethod]
        public XmlDocument ValidarCompaniaBCBPXML(string sCod_IATA)
        {
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            DataTable table = objBCBP.ValidarCompaniaBCBP(sCod_IATA);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<?xml version=\"1.0\" encoding=\"iso-8859-1\" ?><TABLAS/>");

            XmlElement elem = xml.CreateElement(table.TableName);
            int numElementos = table.Columns.Count - 1;
            foreach (DataRow row in table.Rows)
            {
                XmlElement child = xml.CreateElement("registro");
                for (int i = 0; i <= numElementos; i++)
                {
                    child.SetAttribute(table.Columns[i].ToString(), row[i].ToString());
                }
                elem.AppendChild(child);
            }
            xml.DocumentElement.AppendChild(elem);

            return xml;
        }

        //-----------------------------------------   FIN SERVICIOS WEB XML  ------------------------------------//

        //kinzi - 20.12.2010 - 
        [WebMethod]
        public DataSet ListarBoardingDiario(string sFechaDesde,
                                                    string sFechaHasta,
                                                    string sHoraDesde,
                                                    string sHoraHasta,
                                                    string sCodCompania,
                                                    string sTipoPasajero,
                                                    string sTipoVuelo,
                                                    string sTipoTrasbordo,
                                                    string sFechaVuelo,
                                                    string sNumVuelo,
                                                    string sPasajero,
                                                    string sNumAsiento,
                                                    string sCodIata,
                                                    string sPassword,
                                                    string sTipReporte,
                                                    string sColumnSort,
                                                    int iIniRows,
                                                    int iMaxRows,
                                                    string sTotalRows)
        {
            DataSet dsConsulta = null;
            DAO_BoardingBcbp objBCBP = new DAO_BoardingBcbp(HttpContext.Current.Server.MapPath(""));
            dsConsulta = objBCBP.consultarBoardingPassDiario(sFechaDesde,
                                                sFechaHasta,
                                                sHoraDesde,
                                                sHoraHasta,
                                                sCodCompania,
                                                sTipoPasajero,
                                                sTipoVuelo,
                                                sTipoTrasbordo,
                                                sFechaVuelo,
                                                sNumVuelo,
                                                sPasajero,
                                                sNumAsiento,
                                                sCodIata,
                                                sPassword,
                                                sTipReporte,
                                                sColumnSort,
                                                iIniRows,
                                                iMaxRows,
                                                sTotalRows);
            return dsConsulta;            
        }
    }
}
