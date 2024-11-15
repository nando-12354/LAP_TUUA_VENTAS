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

public partial class ReporteRPT_rptResumenTicketVendidosCredito : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    DataTable dt_reporte = new DataTable();
    
    BO_Reportes objReporte = new BO_Reportes(); 
    BO_Consultas objConsulta = new BO_Consultas();

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;

        string ls_Fecha_Inicial = Fecha.convertToFechaSQL2(Request.QueryString["sDesde"]);
        string ls_Fecha_Final = Fecha.convertToFechaSQL2(Request.QueryString["sHasta"]);
        string ls_CodTipoTicket = Request.QueryString["STicket"];
        string ls_DesTipoTicket = Request.QueryString["sDscTicket"];

        string ls_NumVuelo = Request.QueryString["sVuelo"];
        string ls_CodAerolinea = Request.QueryString["sCompania"];
        string ls_DesAerolinea = Request.QueryString["sDscCompania"];

        string ls_DesPago = Request.QueryString["sDscPago"];
        string ls_CodPago = Request.QueryString["sPago"];

        //Validacion Limite de Rango de Fechas en Reportes 
        DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");

        if (dt_parametro.Rows.Count > 0)
        {
            //RecuperarFiltros();
            DateTime fechaF = Convert.ToDateTime(Request.QueryString["sHasta"]);
            DateTime fechaI = Convert.ToDateTime(Request.QueryString["sDesde"]);
            TimeSpan ts = fechaF.Subtract(fechaI);
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro)
            {
                lblmensaje.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
            }
            else
            {
                if (Session["ReportResumTicketVendCredito"] != null)
                {
                    dt_reporte = (DataTable)Session["ReportResumTicketVendCredito"];
                    generarReporte(ls_CodTipoTicket, ls_DesTipoTicket, ls_Fecha_Inicial, ls_Fecha_Final, ls_DesAerolinea, ls_NumVuelo, ls_CodPago, ls_DesPago);
                }
                else
                {
                    int iMaxReporte = GetMaximoReporte();
                    if (Fecha.getFechaElapsed(ls_Fecha_Inicial, ls_Fecha_Final) <= iMaxReporte || iMaxReporte < 0) //Validacion Rango Fecha
                    {
                        consultarDatos(ls_CodTipoTicket, ls_DesTipoTicket, ls_Fecha_Inicial, ls_Fecha_Final, ls_CodAerolinea, ls_NumVuelo, ls_CodPago);
                        generarReporte(ls_CodTipoTicket, ls_DesTipoTicket, ls_Fecha_Inicial, ls_Fecha_Final, ls_DesAerolinea, ls_NumVuelo, ls_CodPago, ls_DesPago);
                    }
                    else
                    {
                        crptvTicketVendidosCredito.Visible = false;
                        lblmensaje.Text = String.Format(htLabels["tuua.general.lblMensajeError1.Text"].ToString(), iMaxReporte);
                    }
                }
            }
        }
    }

    public void generarReporte(string sCodTipoTicket, string sDesTipoTicket, string sFechaInicial, string sFechaFinal, string sDesAerolinea, string sNumVuelo, string sCodPago, string sDesPago)
    {
        try
        {

            if (dt_reporte == null || dt_reporte.Rows.Count == 0)
            {
                //Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptResumenTicketVendidosCredito.rpt");

                obRpt.SetDataSource(dt_reporte);
                obRpt.SetParameterValue("pFechaInicial", Request.QueryString["sDesde"]);
                obRpt.SetParameterValue("pFechaFinal", Request.QueryString["sHasta"]);
                obRpt.SetParameterValue("pTipoTicket", (sDesTipoTicket == null || sDesTipoTicket.Length == 0) ? " -TODOS- " : sDesTipoTicket);
                obRpt.SetParameterValue("pAerolinea", (sDesAerolinea == null || sDesAerolinea.Length == 0) ? " -TODOS- " : sDesAerolinea);
                obRpt.SetParameterValue("pNumVuelo", (sNumVuelo == null || sNumVuelo.Length == 0) ? " -TODOS- " : sNumVuelo);
                obRpt.SetParameterValue("pTipoPago", (sDesPago == null || sDesPago.Length == 0) ? " -TODOS- " : sDesPago);
                crptvTicketVendidosCredito.ReportSource = obRpt;
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

    public void consultarDatos(string sCodTipoTicket, string sDesTipoTicket, string sFechaInicial, string sFechaFinal, string sCodAerolinea, string sNumVuelo, string sCodPago)
    {
        dt_reporte = objReporte.obtenerResumenTicketVendidosCredito(sFechaInicial, sFechaFinal, sCodTipoTicket, sNumVuelo, sCodAerolinea, sCodPago);
    }

    protected int GetMaximoReporte()
    {
        int iMaxReporte = 0;
        DataTable dt_max = objConsulta.ListarParametros("LR");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt32(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

}
