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


public partial class Modulo_Consultas_frmReporteCR : System.Web.UI.Page
{
    BO_Consultas objConsultaCompaniaxFiltro = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
    bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            string valorEstado = Request.QueryString["idEstado"];
            string valorTipo = Request.QueryString["idTipo"];

            //Descripciones
            string idDscE = Request.QueryString["idDscE"];
            string idDscT = Request.QueryString["idDscT"];

            string idOrdenacion = Request.QueryString["idOrdenacion"];
            string idColumna = Request.QueryString["idColumna"];
            string idPaginacion = Request.QueryString["idPaginacion"];



            if ((DataTable)Session["dt_consultaCompania"] != null)
            {
                dt_consulta = (DataTable)Session["dt_consultaCompania"];
            }
            else
            {
                dt_consulta = objConsultaCompaniaxFiltro.ConsultaCompaniaxFiltro(valorEstado, valorTipo, idColumna, idOrdenacion);
            }


            if (dt_consulta.Rows.Count == 0)
            {
                Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
            }
            else
            {

                //Pintar el reporte 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptCompania.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                crptvCompania.ReportSource = obRpt;
                obRpt.SetParameterValue("pEstado", (idDscE == null || idDscE.Length == 0) ? " -TODOS- " : idDscE);
                obRpt.SetParameterValue("pTipo", (idDscT == null || idDscT.Length == 0) ? " -TODOS- " : idDscT);
                obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                Session.Contents.Remove("dt_consultaCompania");
            }

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }


    }
}
