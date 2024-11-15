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

public partial class Modulo_Consultas_ReporteCNS_rptTurnos : System.Web.UI.Page
{
    protected Hashtable htLabels;

    protected void Page_Load(object sender, EventArgs e)
    {
        string FechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string FechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string HoraDesde = Fecha.convertToHoraSQL(Request.QueryString["sHoraDesde"]);
        string HoraHasta = Fecha.convertToHoraSQL(Request.QueryString["sHoraHasta"]);

        string idUsuario = Request.QueryString["idUsuario"];
        string idPtoVta = Request.QueryString["idPtoVta"];

        //Descripciones
        string idDscU = Request.QueryString["idDscU"];

        //Web bussines Consultas
        BO_Consultas objConsultaTurnoxFiltro = new BO_Consultas();
        DataTable dt_consulta = new DataTable(); 
        
        //begin_kinzi
       /* if (Session["Print_Data_Turno"] != null)
        {
            dt_consulta = (DataTable)Session["Print_Data_Turno"];
        }*/
        //else
        //{
            dt_consulta = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(FechaDesde, FechaHasta, idUsuario, idPtoVta, HoraDesde, HoraHasta, "1");
            //Session["Print_Data_Turno"] = dt_consulta;
       // }

        if (dt_consulta == null || dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");            
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptTurnos.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            crptvTurnos.ReportSource = obRpt;

            obRpt.SetParameterValue("pFechaIni", Fecha.convertSQLToFecha(FechaDesde, HoraDesde));
            obRpt.SetParameterValue("pFechaFin", Fecha.convertSQLToFecha(FechaHasta, HoraHasta));
            obRpt.SetParameterValue("pUsuario", (idDscU == null || idDscU.Length == 0) ? " -TODOS- " : idDscU);
            obRpt.SetParameterValue("pCaja", (idPtoVta == null || idPtoVta.Length == 0) ? " -TODOS- " : idPtoVta);
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
        }
    }
}
