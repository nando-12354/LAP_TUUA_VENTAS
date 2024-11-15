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
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class ReporteCNS_rptCnsTicketAnulados : System.Web.UI.Page
{
    BO_Consultas objWBConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();

    //filtros
    string sFechaDesde, sFechaHasta;

    //label
    string sFechaEstadistico = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        sFechaDesde = Request.QueryString["sFechaDesde"];
        sFechaHasta = Request.QueryString["sFechaHasta"];
        sFechaEstadistico = Request.QueryString["sFechaEstadistico"];

        //Validacion Limite de Rango de Fechas en Reportes 
        //DataTable dt_parametro = objWBConsultas.ListarParametrosDefaultValue("LR");

        //if (dt_parametro.Rows.Count > 0)
        //{
        //    //RecuperarFiltros();
        //    DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFechaHasta"]);
        //    DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFechaDesde"]);
        //    TimeSpan ts = fechaF.Subtract(fechaI);
        //    int dias = ts.Days;
        //    int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

        //    if (dias > parametro)
        //    {
        //        Response.Write("El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.");
        //    }
        //    else
        //    {

                dt_consulta = objWBConsultas.obtenerTicketsAnulados(sFechaDesde, sFechaHasta);
                if (dt_consulta.Rows.Count == 0)
                {
                    Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                }

                else
                {
                    //obtenemos fecha estadistico
                    //sFechaEstadistico = objWBConsultas.obtenerFechaEstadistico("0");

                    //Pintar el reporte 
                    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketsAnulados.rpt");
                    //Poblar el reporte con el datatable
                    obRpt.SetDataSource(dt_consulta);
                    obRpt.SetParameterValue("sFechaInicio", sFechaDesde);
                    obRpt.SetParameterValue("sFechaFinal", sFechaHasta);
                    obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                    obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);
                    crptvTicketsAnulados.ReportSource = obRpt;
                }
        //    }
        //}   
        //Session.Contents.Remove("dt_consultaBoarding");
    }
    

 }
