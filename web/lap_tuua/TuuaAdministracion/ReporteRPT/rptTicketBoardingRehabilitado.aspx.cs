using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;

public partial class ReporteRPT_rptTicketBoardingRehabilitado : System.Web.UI.Page
{
    
    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    BO_Reportes objListarTicketContingenciaxFecha = new BO_Reportes();

    protected void Page_Load(object sender, EventArgs e)
    {
        string ls_CodTipoTicket = Request.QueryString["codtpticket"];
        string ls_DesTipoTicket = Request.QueryString["destpticket"];
        string ls_CodAerolinea = Request.QueryString["codaerolinea"];
        string ls_DesAerolinea = Request.QueryString["desaerolinea"];

        string ls_horaInicial = Fecha.convertToHoraSQL(Request.QueryString["horainicial"]);
        string ls_horaFinal = Fecha.convertToHoraSQL(Request.QueryString["horafinal"]);

        string ls_FechaInicial = Fecha.convertToFechaSQL2(Request.QueryString["fechainicial"]);
        string ls_FechaFinal = Fecha.convertToFechaSQL2(Request.QueryString["fechafinal"]);
        string ls_CodMotivo = Request.QueryString["codMotivo"];
        string ls_DesMotivo = Request.QueryString["desMotivo"];

        string ls_NumVuelo = Request.QueryString["numvuelo"];
        string ls_Documento = Request.QueryString["documento"];
        
        if (Session["ReporteTicketBoardingRehab"] != null)
        {
            dt_consulta = (DataTable)Session["ReporteTicketBoardingRehab"];
            generarReporte(ls_FechaInicial, ls_FechaFinal,
                           ls_DesMotivo, ls_DesTipoTicket,
                           ls_DesAerolinea, ls_horaInicial, ls_horaFinal, ls_NumVuelo, ls_Documento);
        }
        else
        {
            consultarDatos(ls_FechaInicial, ls_FechaFinal,
                           ls_CodMotivo, ls_CodTipoTicket,
                           ls_CodAerolinea, ls_horaInicial, ls_horaFinal, ls_NumVuelo, ls_Documento);

            generarReporte(ls_FechaInicial, ls_FechaFinal,
                           ls_DesMotivo, ls_DesTipoTicket,
                           ls_DesAerolinea, ls_horaInicial, ls_horaFinal, ls_NumVuelo, ls_Documento);
        }
    }


    public void generarReporte(string ls_FechaDesde, string ls_FechaHasta, 
                               string ls_Motivo, string ls_TipoTicket, 
                               string ls_Aerolinea, string ls_HoraInicial, 
                               string ls_HoraFinal, string ls_NumVuelo,
                               string ls_Documento)
    {
        try
        {
            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
            {
                invocarMensaje(0);
                lblmensaje.Text = "La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro";
            }
            else
            {
                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketBoardingRehabilitado.rpt");
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", ls_FechaDesde);
                obRpt.SetParameterValue("pFechaFinal", ls_FechaHasta);
                obRpt.SetParameterValue("pMotivo", ls_Motivo);
                obRpt.SetParameterValue("pTipoTicket", ls_TipoTicket);
                obRpt.SetParameterValue("pAerolinea", ls_Aerolinea);
                obRpt.SetParameterValue("pHoraInicial", ls_HoraInicial);
                obRpt.SetParameterValue("pHoraFinal", ls_HoraFinal);
                obRpt.SetParameterValue("pNumVuelo", ls_NumVuelo);

                if (ls_Documento == "0")
                {
                    obRpt.SetParameterValue("pDocumento", "TICKET Y BOARDING");
                }
                if (ls_Documento == "1")
                {
                    obRpt.SetParameterValue("pDocumento", "TICKET");
                }
                if (ls_Documento == "2")
                {
                    obRpt.SetParameterValue("pDocumento", "BOARDING");
                }

                
                crptTicketBoardingRehabilitado.ReportSource = obRpt;
                invocarMensaje(1);
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

    public void consultarDatos(string ls_FechaDesde, string ls_FechaHasta,
                                string ls_Motivo, string ls_TipoTicket,
                                string ls_Aerolinea, string ls_HoraInicial,
                                string ls_HoraFinal, string ls_NumVuelo,
                                string ls_Documento)
    {
        try
        {

            dt_consulta = objListarTicketContingenciaxFecha.obtenerTicketBoardingRehabilitados(ls_FechaDesde, ls_FechaHasta, 
                                                                                               ls_HoraInicial, ls_HoraFinal, 
                                                                                               ls_Documento, ls_TipoTicket, 
                                                                                               ls_Aerolinea, ls_NumVuelo, ls_Motivo);
                        

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

    public void invocarMensaje(int li_tipo)
    {
        hbandera.Value = Convert.ToString(li_tipo);
    }

}
