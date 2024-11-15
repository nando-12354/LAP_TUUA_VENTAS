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

public partial class ReporteCNS_rptAuditoria : System.Web.UI.Page
{
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        string sTipoOperacion = Request.QueryString["sTipoOperacion"];
        string sTabla = Request.QueryString["sTabla"];
        string sCodModulo = Request.QueryString["sCodModulo"];
        string sCodSubModulo = Request.QueryString["sCodSubModulo"];
        string sCodUsuario = Request.QueryString["sCodUsuario"];
        string sFchDesde = Request.QueryString["sFchDesde"];
        string sFchHasta = Request.QueryString["sFchHasta"];
        string sHraDesde = Request.QueryString["sHraDesde"];
        string sHraHasta = Request.QueryString["sHraHasta"];
        string strModulo = Request.QueryString["strModulo"];
        string strSubModulo = Request.QueryString["strSubModulo"];
        string strDscOpe = Request.QueryString["strDscOpe"];
        string strDscUsr = Request.QueryString["strDscUsr"];

        if (sHraDesde == "__:__:__") { sHraDesde = ""; }
        if (sHraHasta == "__:__:__") { sHraHasta = ""; }


        if (sFchDesde != "")
        {
            string[] wordsFechaDesde = sFchDesde.Split('/');
            sFchDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { sFchDesde = ""; }



        if (sFchHasta != "")
        {
            string[] wordsFechaHasta = sFchHasta.Split('/');
            sFchHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        else { sFchHasta = ""; }


        if (sHraDesde != "")
        {
            string[] wordsDesde = sHraDesde.Split(':');
            sHraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        }
        else { sHraDesde = ""; }



        if (sHraHasta != "")
        {
            string[] wordsHasta = sHraHasta.Split(':');
            sHraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
        }
        else { sHraHasta = ""; }

        //Validacion Limite de Rango de Fechas en Reportes 
        DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");

        if (dt_parametro.Rows.Count > 0)
        {
            //RecuperarFiltros();
            DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFchHasta"]);
            DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFchDesde"]);
            TimeSpan ts = fechaF.Subtract(fechaI);
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro)
            {
                Response.Write("El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.");
            }
            else
            {

                dt_consulta = objConsulta.obtenerconsultaAuditoriasPagin(sTipoOperacion, sTabla, sCodModulo, sCodSubModulo, sCodUsuario, sFchDesde, sFchHasta, sHraDesde, sHraHasta, null, 0, 0, "0");


                if (dt_consulta.Rows.Count == 0)
                {
                    Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                }
                else
                {


                    if (sTipoOperacion == "0") { strDscOpe = "Todos"; }
                    if (sTabla == "0") { sTabla = "Todos"; }
                    if (sCodModulo == "0") { sCodModulo = "Todos"; }
                    if (sCodSubModulo == "0") { sCodSubModulo = "Todos"; }
                    if (sCodUsuario == "0") { strDscUsr = "Todos"; }


                    //Pintar el reporte 
                    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    obRpt.Load(Server.MapPath("").ToString() + @"\rptAuditoria.rpt");
                    //Poblar el reporte con el datatable
                    obRpt.SetDataSource(dt_consulta);
                    obRpt.SetParameterValue("sOperacion", strDscOpe);
                    obRpt.SetParameterValue("sTabla", sTabla);
                    obRpt.SetParameterValue("sModulo", strModulo);
                    obRpt.SetParameterValue("sSubModulo", strSubModulo);
                    obRpt.SetParameterValue("sUsuario", strDscUsr);
                    obRpt.SetParameterValue("sFchDesde", Request.QueryString["sFchDesde"]);
                    obRpt.SetParameterValue("sFchHasta", Request.QueryString["sFchHasta"]);
                    obRpt.SetParameterValue("sHraDesde", Request.QueryString["sHraDesde"] == "__:__:__" ? "" : Request.QueryString["sHraDesde"]);
                    obRpt.SetParameterValue("sHraHasta", Request.QueryString["sHraHasta"] == "__:__:__" ? "" : Request.QueryString["sHraHasta"]);
                    crvAuditoria.ReportSource = obRpt;
                    //Session.Contents.Remove("dt_consultaBoarding");
                }
            }
        }
    }
}
