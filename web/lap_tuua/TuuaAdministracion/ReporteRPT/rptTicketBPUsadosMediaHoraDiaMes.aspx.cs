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
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;
using System.IO;
using LAP.TUUA.CONVERSOR;

public partial class ReporteRPT_rptTicketBPUsadosMediaHoraDiaMes : System.Web.UI.Page
{
    protected bool Flg_Error;
    BO_Reportes objBOReportes = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    protected DataTable dtReporteDetalle = new DataTable();
    protected DataTable dtResumenReporte = new DataTable();
    DataSetHelper dsHelper = new DataSetHelper();
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;
    string sDestino = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
        string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
        string sHoraDesde = convertToHoraSQL(Request.QueryString["sHoraDesde"]);
        string sHoraHasta = convertToHoraSQL(Request.QueryString["sHoraHasta"]);
        string sTipoRango = Request.QueryString["sTipoRango"];
        string sAerolinea = Request.QueryString["sAerolinea"];
        string sTipoTicket = Request.QueryString["sTipoTicket"];
        string sNumVuelo = Request.QueryString["sNumVuelo"];
        string sTDocumento = Request.QueryString["sTDocumento"];
        string sDestino = Request.QueryString["sDestino"];

        //Descripciones
        string idDscR = Request.QueryString["idDscR"];
        string idDscA = Request.QueryString["idDscA"];
        string idDscT = Request.QueryString["idDscT"];

        try
        {

            DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");
            if (dt_parametro.Rows.Count > 0)
            {
                DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFechaHasta"]);
                DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFechaDesde"]);
                TimeSpan ts = fechaF.Subtract(fechaI);
                int dias = ts.Days;
                int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

                if (dias > parametro)
                {
                    lblmensaje.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
                }
                else {
                    consultarDatos(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDestino, sTipoRango, sAerolinea, sTipoTicket, sNumVuelo, sTDocumento);
                    //cargarResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDestino, idDscR, idDscA, idDscT, sNumVuelo, sNumVuelo);
                    generarReporte(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDestino, idDscR, idDscA, idDscT, sNumVuelo, sNumVuelo);
                }
            }
            
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            dtReporteDetalle.Dispose();
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
                
            }
        }
    }

    public string convertToHoraSQL(string hora)
    {
        if (hora != null && hora.Length == 5 && hora != "__:__")
        {

            return hora.Substring(0, 2) +
                   hora.Substring(3, 2) +
                   "00";
        }
        else
        {
            return "";
        }
    }
    public void consultarDatos(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sDestino,
                                string sTipoRango, string sAerolinea, string sTipoTicket, string sNumVuelo, string sTDocumento)
    {
        try
        {
            dtReporteDetalle = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                            sNumVuelo,
                                                                            sTDocumento,
                                                                            sTipoTicket,
                                                                            sTipoRango,
                                                                            sFechaDesde,
                                                                            sFechaHasta,
                                                                            sHoraDesde,
                                                                            sHoraHasta,
                                                                            sDestino,
                                                                            null, 0, 0, "1", "0", "0");

            if(dtReporteDetalle.Rows.Count > 0){
                dtResumenReporte = cargarResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sDestino,
                                                    sTipoRango,sAerolinea,sTipoTicket, sNumVuelo,sTDocumento );
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

    public void generarReporte(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sDestino,
                                string sTipoRango, string sAerolinea, string sTipoTicket, string sNumVuelo, string sTDocumento)
    {
        try
        {
            DataTable dtReporte = new DataTable();
            dtReporte = dtReporteDetalle;

            if (dtReporte == null || dtReporte.Rows.Count == 0)
            {
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
                DataTable dtResumenReporteDetalle = new DataTable();
                dtResumenReporteDetalle = dtResumenReporte;

                //Pintar el reporte                 
                obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketBPUsadosMediaHoraDiaMes.rpt");

                obRpt.Subreports[0].SetDataSource(dtResumenReporteDetalle);

                obRpt.SetDataSource(dtReporte);
                obRpt.SetParameterValue("pDesde", Fecha.convertSQLToFecha(sFechaDesde,null));
                obRpt.SetParameterValue("pHasta", Fecha.convertSQLToFecha(sFechaHasta,null));
                obRpt.SetParameterValue("pHoraInicio", Fecha.convertSQLToHora(sHoraDesde));
                obRpt.SetParameterValue("pHoraFin", Fecha.convertSQLToHora(sHoraHasta));
                obRpt.SetParameterValue("pTipoDocumento", sTDocumento);
                obRpt.SetParameterValue("pAerolinea", sAerolinea);
                obRpt.SetParameterValue("pNroVuelo", sNumVuelo);
                obRpt.SetParameterValue("pDestino", sDestino);
                obRpt.SetParameterValue("pTipoTicket", sTipoTicket);

                crvrptTicketBPUsadosMediaHoraDiaMes.ReportSource = obRpt;
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            dtReporteDetalle.Dispose();
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
                
            }
        }

    }

    public DataTable cargarResumen(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sDestino,
                                string sTipoRango, string sAerolinea, string sTipoTicket, string sNumVuelo, string sTDocumento)
    {
        DataTable dtResumen = new DataTable();
        try
        {
            //DataTable dtPrueba = new DataTable();
            dtResumen = objBOReportes.consultarTicketBoardingUsados(sAerolinea,
                                                                                sNumVuelo,
                                                                                sTDocumento,
                                                                                sTipoTicket,
                                                                                sTipoRango,
                                                                                sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sDestino,
                                                                                null, 0, 0, "0", "2", "0");
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

        return dtResumen;
    }
}
