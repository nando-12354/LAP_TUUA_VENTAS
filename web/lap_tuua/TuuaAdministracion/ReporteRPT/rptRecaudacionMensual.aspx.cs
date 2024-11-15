using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;

public partial class ReporteRPT_rptRecaudacionMensual : System.Web.UI.Page
{
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    BO_Reportes objConsulta = new BO_Reportes();

    protected void Page_Load(object sender, EventArgs e)
    {
        String sAnio = Request.QueryString["anio"];

        if (Session["ConsultaRecaudacionMensual"] != null)
        {
            dt_consulta = (DataTable)Session["ConsultaRecaudacionMensual"];
            generarReporte(sAnio);
        }
        else
        {
            consultarDatos(sAnio);
            generarReporte(sAnio);
        }

    }

    public void generarReporte(String sAnio)
    {
        try
        {

            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
            {
                //Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptRecaudacionMensual.rpt");
                //rptDetalleVentaCompania rptRecaudacionMensual
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFecha", sAnio);

                crptvRecaudacionMensual.ReportSource = obRpt;
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

    public void consultarDatos(String sAnio)
    {
        dt_consulta = objConsulta.obtenerRecaudacionMensual(sAnio);
    }
}
