using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;
using System.Collections;

public partial class ReporteRPT_rptResumenCompania : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    BO_Reportes objConsulta = new BO_Reportes();
    string sFechaEstadistico = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        String sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        String sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["idHoraDesde"]);
        String sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["idHoraHasta"]);
        sFechaEstadistico = Request.QueryString["sFechaEstadistico"];



        if (Session["ConsultaDetalleVentaCompania"] != null)
        {
            dt_consulta = (DataTable)Session["ConsultaDetalleVentaCompania"];
            generarReporte(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta);
        }
        else
        {
            consultarDatos(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta);
            generarReporte(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta);
        }
    }

    public void generarReporte(String sFechaDesde, String sFechaHasta, String sHoraDesde, String sHoraHasta)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
            {
                this.lblmensaje.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
            }
            else
            {
                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //rptDetalleVentaCompania.rpt
                //rptResumenCompania.rpt
                obRpt.Load(Server.MapPath("").ToString() + @"\rptDetalleVentaCompania.rpt");

                obRpt.SetDataSource(dt_consulta);
                if (sHoraDesde == "")
                {
                    sHoraDesde = "00:00:00";
                }
                if (sHoraHasta == "")
                {
                    sHoraHasta = "23:59:59";
                }
                obRpt.SetParameterValue("pHoraDesde", sHoraDesde);
                obRpt.SetParameterValue("pHoraHasta", sHoraHasta);
                obRpt.SetParameterValue("pFechaDesde", sFechaDesde);
                obRpt.SetParameterValue("pFechaHasta", sFechaHasta);
                obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);

                crptvResumenCompania.ReportSource = obRpt;
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

    public void consultarDatos(String sFechaDesde, String sFechaHasta, String sHoraDesde, String sHoraHasta)
    {
        dt_consulta = objConsulta.obtenerDetalleVentaCompania(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta);
    }
}
