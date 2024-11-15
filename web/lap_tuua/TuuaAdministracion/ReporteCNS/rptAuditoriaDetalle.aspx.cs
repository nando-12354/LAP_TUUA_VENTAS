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

public partial class ReporteCNS_rptAuditoriaDetalle : System.Web.UI.Page
{
    BO_Consultas objWBConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var sUsuario = Request.QueryString["hfUsuario"];
            var sRoles = Request.QueryString["hfRoles"];
            var sNombreTabla = Request.QueryString["hfNombreTabla"];
            var sContador = Request.QueryString["hfContador"];



            dt_consulta = objWBConsultas.obtenerdetalleAuditoria(sNombreTabla, sContador);
            if (dt_consulta.Rows.Count > 0)
            {
                msgNoHist.Text = "";
                //Pintar el reporte 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptAuditoriaDetalle.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("strUsuario", sUsuario);
                obRpt.SetParameterValue("strRoles", sRoles);
                obRpt.SetParameterValue("strTablaModificada", sNombreTabla);
                crvAuditoriaDetalle.ReportSource = obRpt;                
            }
            else
            {                
                msgNoHist.Text = "No contiene Detalle a imprimir";
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
