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

public partial class ReporteCNS_rptBoarding : System.Web.UI.Page
{
    BO_Consultas objWBConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string idCompania = Request.QueryString["idCompania"];
        string idNumVuelo = Request.QueryString["idNumVuelo"];
        string idFechaVuelo = Request.QueryString["idFechaVuelo"];
        string idNumAsiento = Request.QueryString["idNumAsiento"];
        string idPasajero = Request.QueryString["idPasajero"];
        string idDscCompania = Request.QueryString["idDscCompania"];

        //Obtiene Datatable con el resultado del store procedure        
        idFechaVuelo = idFechaVuelo.Substring(6, 4) + idFechaVuelo.Substring(3, 2) + idFechaVuelo.Substring(0, 2);
        string strSortExp = Session["sortExpressionDetTicketBcbp"] == null ? null : Session["sortExpressionDetTicketBcbp"].ToString();
        if ((DataTable)Session["dt_consultaBoarding"] != null)
        {
            dt_consulta = (DataTable)Session["dt_consultaBoarding"];
        }
        else
        {
            dt_consulta = objWBConsultas.DetalleBoardingPagin(idCompania, idNumVuelo, idFechaVuelo, idNumAsiento, idPasajero, null, null, null, strSortExp, 0, 0, "0");
        }

        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptBoarding.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            crptvBoarding.ReportSource = obRpt;

            obRpt.SetParameterValue("pCompania", (idCompania != null) ? ((idCompania == "0") ? " - TODOS - " : idDscCompania) : " - ");
            obRpt.SetParameterValue("pNumVuelo", idNumVuelo ?? " - ");
            obRpt.SetParameterValue("pFchVuelo", Request.QueryString["idFechaVuelo"] ?? " - ");
            obRpt.SetParameterValue("pNumAsiento", idNumAsiento ?? " - ");
            obRpt.SetParameterValue("pPasajero", idPasajero ?? " - ");
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            Session.Contents.Remove("dt_consultaBoarding");
        }
    }
}
