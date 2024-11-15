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
using System.Globalization;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



public partial class ReporteCNS_rptOperacionCV : System.Web.UI.Page
{
    BO_Consultas objConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;


        string valorFecha = Request.QueryString["idFecha"];
        string valorUsuario = Request.QueryString["idUsuario"];


        if (valorFecha != "")
        {
            string[] wordsFechaDesde = valorFecha.Split('/');
            valorFecha = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { valorFecha = ""; }


        //DataTable stable =
        dt_consulta = objConsultas.UsuarioxFechaOperacion(valorFecha, valorUsuario, null, null);

        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            obRpt.Load(Server.MapPath("").ToString() + @"\rptOperacionCV.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            obRpt.SetParameterValue("sFch_Proceso", Request.QueryString["idFecha"] == "" ? "Sin Filtro" : Request.QueryString["idFecha"]);
            obRpt.SetParameterValue("sUsuario", Request.QueryString["idUsuario"] == "0" ? "Todos" : dt_consulta.Rows[0]["Usuario"].ToString());            
            crptvOperacionCV.ReportSource = obRpt;
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            crptvOperacionCV.DataBind();
            //Session.Contents.Remove("dt_consultaCompania");
        }
    }
    

  
}
