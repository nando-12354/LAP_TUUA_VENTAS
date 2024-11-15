using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Xml;
using LAP.TUUA.ALARMAS;
using System.IO;
using LAP.TUUA.CONVERSOR;

public partial class rptConsulta : System.Web.UI.Page
{

    protected BO_Consultas objBOConsultas;
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;

    protected void Page_Load(object sender, EventArgs e)
    {
        String sIdCompania = Request.QueryString["sIdCompania"];
        String sFechaVuelo = Request.QueryString["sFechaVuelo"];
        String sNumVuelo = Request.QueryString["sNumVuelo"];
        String sNumAsiento = Request.QueryString["sAsiento"];
        String sPasajero = Request.QueryString["sPasajero"];

        //Formateando Fecha
        sFechaVuelo = sFechaVuelo.Split('/')[2] + sFechaVuelo.Split('/')[1] + sFechaVuelo.Split('/')[0];  

        objBOConsultas = new BO_Consultas();
        DataTable dt_consulta = objBOConsultas.obtenerDetalleWebBCBP(sIdCompania, sNumVuelo, sFechaVuelo, sNumAsiento, sPasajero);

        if (dt_consulta == null || dt_consulta.Rows.Count == 0)
        {
            //Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
            DataTable dtCompanias = (DataTable)Session["dtCompanias"];
            DataRow[] foundRowCompania = dtCompanias.Select("Cod_Compania='" + sIdCompania+"'");
            if (foundRowCompania[0]["Cod_IATA"].Equals("LA") || foundRowCompania[0]["Cod_IATA"].Equals("LP"))
            {
                lblmensaje.Text = "La busqueda no retorna resultado, intente ingresando el Nro. de Boarding en el campo Nombre Pasajero";
            }
            else
            {
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
        }
        else
        {
            string sETicket = string.Empty;
            DataRow drRegistro = dt_consulta.Rows[0];
            string sTrama = drRegistro["Dsc_Trama_Bcbp"].ToString();
            Reader2 reader = new Reader2();
            Hashtable ht = reader.ParseString_Boarding(sTrama);
            if (sTrama.Length >= 88)
            {
                sETicket = (String)ht["eticket"];
            }

            //Pintar el reporte                 
            obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptConsulta.rpt");
            //rptDetalleVentaCompania rptRecaudacionMensual
            obRpt.SetDataSource(dt_consulta);
            obRpt.SetParameterValue("pETicket", sETicket);
            //CrystalReportViewer1.ReportSource = obRpt;
            ExpotacionReport("0");
        }

    }

    public void ExpotacionReport(string expExcel)
    {
        if (expExcel != "1")
        {

            MemoryStream oStream; // using System.IO 
            oStream = (MemoryStream)obRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment;filename=consulta.pdf");
            Response.BinaryWrite(oStream.ToArray());
            //Response.End();
            //crvClienAtenOperador.ReportSource = obRpt;
            //crvClienAtenOperador.DataBind();

        }

        else
        {
            MemoryStream oStream1; // using System.IO 
            oStream1 = (MemoryStream)obRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.BinaryWrite(oStream1.ToArray());

            //Response.End();
        }


    }

}
