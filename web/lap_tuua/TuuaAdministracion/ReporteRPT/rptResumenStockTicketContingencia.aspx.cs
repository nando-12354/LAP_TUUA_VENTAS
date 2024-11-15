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

public partial class ReporteRPT_rptResumenStockTicketContingencia : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    DataTable dt_consulta = new DataTable();
    BO_Reportes objReporte = new BO_Reportes();

    string sDesFecha = "";
    string sFechaEstadistico = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        String sFecha = Fecha.convertToFechaSQL2(Request.QueryString["sFecha"]);
        sDesFecha = Request.QueryString["sFecha"];
        String sCodTipoTicket = Request.QueryString["idTipoTicket"];
        String sDesTipoTicket = Request.QueryString["idDscT"];
        sFechaEstadistico = Request.QueryString["sFechaEstadistico"];

        if (Session["ConsultaStockTicketConti"] != null)
        {
            dt_consulta = (DataTable)Session["ConsultaStockTicketConti"];
            generarReporte(sCodTipoTicket, sDesTipoTicket, sFecha);
        }
        else
        {
            consultarDatos(sFecha, sCodTipoTicket);
            generarReporte(sCodTipoTicket, sDesTipoTicket, sFecha);
        }
    }

    public void generarReporte(String sCodTipoTicket, String sDesTipoTicket, String sFecha)
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
                obRpt.Load(Server.MapPath("").ToString() + @"\rptStockTicketContingencia.rpt");

                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFecha", sDesFecha);
                string sTipoTicket = (sCodTipoTicket == "0") ? "Todos" : sDesTipoTicket;
                obRpt.SetParameterValue("pTipoTicket", sTipoTicket);
                obRpt.SetParameterValue("pFechaEstadistico", sFechaEstadistico);
                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                crptvStockTicketContingencia.Visible = true;
                crptvStockTicketContingencia.ReportSource = obRpt;
                crptvStockTicketContingencia.DataBind();
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

    public void consultarDatos(String sFecha, String sCodTipoTicket)
    {
        dt_consulta = objReporte.obtenerResumenStockTicketContingencia(sCodTipoTicket, sFecha,"0");
    }
}
