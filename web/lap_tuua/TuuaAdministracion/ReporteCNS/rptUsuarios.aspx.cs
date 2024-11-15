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

public partial class Modulo_Consultas_ReporteCNS_rptUsuarios : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objConsultaUsuariosxFiltro = new BO_Consultas();
    DataTable dt_consulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        string idRol = Request.QueryString["idRol"];
        string idEstado = Request.QueryString["idEstado"];
        string idGrupo = Request.QueryString["idGrupo"];

        //Descripciones
        string idDscR = Request.QueryString["idDscR"];
        string idDscE = Request.QueryString["idDscE"];
        string idDscG = Request.QueryString["idDscG"];

        string idOrdenacion = Request.QueryString["idOrdenacion"];
        string idColumna = Request.QueryString["idColumna"];
        string idPaginacion = Request.QueryString["idPaginacion"];
        
        if ((DataTable)Session["dt_consultaUsuario"] != null)
        {
            dt_consulta = (DataTable)Session["dt_consultaUsuario"];
        }
        else
        {
            dt_consulta = objConsultaUsuariosxFiltro.ConsultaUsuarioxFiltro(idRol, idEstado, idGrupo, idColumna, idOrdenacion);
        }


        if (dt_consulta.Rows.Count == 0)
        {
            Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
        }
        else
        {
            //Pintar el reporte 
            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptUsuarios.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dt_consulta);
            crptvUsuarios.ReportSource = obRpt;
            obRpt.SetParameterValue("pRol", (idDscR == null || idDscR.Length == 0) ? " -TODOS- " : idDscR);
            obRpt.SetParameterValue("pEstado", (idDscE == null || idDscE.Length == 0) ? " -TODOS- " : idDscE);
            obRpt.SetParameterValue("pGrupo", (idDscG == null || idDscG.Length == 0) ? " -TODOS- " : idDscG);
            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
            Session.Contents.Remove("dt_consultaUsuario");
        }


    }
}
