using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class Modulo_Consultas_ReporteCNS_rptDetalleTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objConsultaDetalleTicket = new BO_Consultas();
    DataTable dt_consulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string idNumTicket = Request.QueryString["idNumTicket"];

        string idNumTicketDesde = Request.QueryString["idNumTicketDesde"];
        string idNumTicketHasta = Request.QueryString["idNumTicketHasta"];
        string strSortExp = Session["sortExpressionDetTicketBcbp"] == null ? null : Session["sortExpressionDetTicketBcbp"].ToString();

        if (idNumTicket != null)
        {
            if ((DataTable)Session["dt_consultaDetTicket"] != null)
            {
                dt_consulta = (DataTable)Session["dt_consultaDetTicket"];
            }
            else
            {
                dt_consulta = objConsultaDetalleTicket.ConsultaDetalleTicketPagin(idNumTicket, null, null,null,0,0,"0");
            }
        }

        if ((idNumTicketDesde != null && idNumTicketHasta != null) || (idNumTicketDesde == null && idNumTicketHasta != null) || (idNumTicketDesde != null && idNumTicketHasta == null))
        {
            if ((DataTable)Session["dt_consultaDetTicket"] != null)
            {
                dt_consulta = (DataTable)Session["dt_consultaDetTicket"];
            }
            else
            {
                dt_consulta = objConsultaDetalleTicket.ConsultaDetalleTicketPagin("", idNumTicketDesde, idNumTicketHasta, strSortExp, 0, 0, "0");
                DataTable dt_Conti_ATM = new DataTable();
                try
                {
                    dt_Conti_ATM = objConsultaDetalleTicket.ConsultaDetalleTicketPagin("", idNumTicketDesde, idNumTicketHasta, strSortExp, 0, 0, "2");
                }
                catch
                {
                }

                if (dt_Conti_ATM != null)
                {
                    foreach (DataRow row in dt_Conti_ATM.Rows)
                    {
                        dt_consulta.ImportRow(row);
                    }
                }
            }
        }

        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptDetalleTicket.rpt");
            //Poblar el reporte con el datatable
            
            obRpt.SetDataSource(dt_consulta);
            crptvDetalleTicket.ReportSource = obRpt;

            if (idNumTicket != null) {
                idNumTicketDesde = idNumTicketHasta = idNumTicket;
            }
            if (!((idNumTicketDesde != null && idNumTicketHasta != null) || (idNumTicketDesde == null && idNumTicketHasta != null) || (idNumTicketDesde != null && idNumTicketHasta == null)))
            {
                idNumTicketHasta = idNumTicketDesde = " - ";
            }
            obRpt.SetParameterValue("pTicketIni", idNumTicketDesde);
            obRpt.SetParameterValue("pTicketFin", idNumTicketHasta);
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            Session.Contents.Remove("dt_consultaDetTicket");

        }
        
        

    }
}
