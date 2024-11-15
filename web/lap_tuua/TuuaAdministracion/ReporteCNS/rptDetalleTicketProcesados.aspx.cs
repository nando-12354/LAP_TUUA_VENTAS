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

public partial class ReporteCNS_rptDetalleTicketProcesados : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Consultas objBOConsulta = new BO_Consultas();

    DataTable dt_consulta = new DataTable();

    string idCajero;
    string idTurno;

    protected void Page_Load(object sender, EventArgs e)
    {
        idCajero = Request.QueryString["idCajero"];
        idTurno = Request.QueryString["idTurno"];

        string idOrdenacion = Request.QueryString["idOrdenacion"];
        string idColumna = Request.QueryString["idColumna"];
        string idPaginacion = Request.QueryString["idPaginacion"];

        if ((DataTable)Session["dt_consultaTicketProcesados"] != null)
        {
            dt_consulta = (DataTable)Session["dt_consultaTicketProcesados"];
        }
        else
        {
            dt_consulta = objBOConsulta.ListarTicketProcesado(idTurno);
        }


        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptDetalleTicketProcesados.rpt");

            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            crptvTicketProcesados.ReportSource = obRpt;

            obRpt.SetParameterValue("pTitulo", "Detalle de Tickets Procesados");
            obRpt.SetParameterValue("pCajero", "Cajero: ");
            obRpt.SetParameterValue("pTurno", "Turno: ");
            obRpt.SetParameterValue("pDscCajero", (idCajero == null || idCajero.Length == 0) ? " -TODOS- " : idCajero);
            obRpt.SetParameterValue("pDscTurno", (idTurno == null || idTurno.Length == 0) ? " -TODOS- " : idTurno);
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            Session.Contents.Remove("dt_consultaTicketProcesados");
        }              
    }
}
