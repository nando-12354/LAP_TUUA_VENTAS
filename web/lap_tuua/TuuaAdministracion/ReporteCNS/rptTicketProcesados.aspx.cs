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


public partial class ReporteCNS_rptTicketProcesados : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Consultas objBOConsulta = new BO_Consultas();
    
    DataTable dt_consulta = new DataTable();

    string idFechaDesde;
    string idFechaHasta;
    string idCajero;
    string idTurno;

    protected void Page_Load(object sender, EventArgs e)
    {
        idFechaDesde = Request.QueryString["idFechaDesde"];
        idFechaHasta = Request.QueryString["idFechaHasta"];
        idCajero = Request.QueryString["idCajero"];
        idTurno = Request.QueryString["idTurno"];

        string idOrdenacion = Request.QueryString["idOrdenacion"];
        string idColumna = Request.QueryString["idColumna"];
        string idPaginacion = Request.QueryString["idPaginacion"];

        int rsCompare = DateTime.Compare(Convert.ToDateTime(idFechaDesde), Convert.ToDateTime(idFechaHasta));

        if (rsCompare != 1)
        {
            formatearvalores(idFechaDesde, idFechaHasta, idCajero, idTurno);

            if ((DataTable)Session["dt_consultaTicketProcesados"] != null)
            {
                dt_consulta = (DataTable)Session["dt_consultaTicketProcesados"];
            }
            else
            {
                dt_consulta = objBOConsulta.ConsultaTurnoxFiltro2(idFechaDesde, idFechaHasta, idCajero, idTurno);
            }


            if (dt_consulta.Rows.Count == 0)
            {
                Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
            }
            else
            {
                //Pintar el reporte 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketProcesados.rpt");

                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                crptvTicketProcesados.ReportSource = obRpt;

                obRpt.SetParameterValue("pTitulo", "Tickets Procesados");
                obRpt.SetParameterValue("pFiltroDesde", "Fecha Desde: ");
                obRpt.SetParameterValue("pFiltroHasta", "Fecha Hasta: ");
                obRpt.SetParameterValue("pRangoInicial", Fecha.convertSQLToFecha(idFechaDesde, ""));
                obRpt.SetParameterValue("pRangoFinal", Fecha.convertSQLToFecha(idFechaHasta, ""));
                obRpt.SetParameterValue("pCajero", "Cajero: ");
                obRpt.SetParameterValue("pTurno", "Turno: ");
                obRpt.SetParameterValue("pDscCajero", (idCajero == null || idCajero.Length == 0) ? " -TODOS- " : idCajero);
                obRpt.SetParameterValue("pDscTurno", (idTurno == null || idTurno.Length == 0) ? " -TODOS- " : idTurno);
                obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                Session.Contents.Remove("dt_consultaTicketProcesados");
            }
        }
        else {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
    }

    void formatearvalores(string idFechaDesde, string idFechaHasta, string idCajero, string idTurno)
    {
        if (idFechaDesde != "")
        {
            string[] wordsFechaDesde = idFechaDesde.Split('/');
            idFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { idFechaDesde = ""; }

        if (idFechaHasta != "")
        {
            string[] wordsFechaHasta = idFechaHasta.Split('/');
            idFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        else { idFechaHasta = ""; }

        this.idFechaDesde = idFechaDesde;
        this.idFechaHasta = idFechaHasta;
        this.idCajero = idCajero;
        this.idTurno = idTurno;
    }
}
