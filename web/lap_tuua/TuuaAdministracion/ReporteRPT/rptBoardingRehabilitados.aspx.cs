using System;
using System.Collections.Generic;
using System.Collections;
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
    protected Hashtable htLabels;

    DataTable dt_consulta = new DataTable();
    BO_Reportes objListarBPRehab = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            string sFechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
            string sFechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
            string sHoraDesde = Fecha.convertToHoraSQL(Request.QueryString["sHoraDesde"]);
            string sHoraHasta = Fecha.convertToHoraSQL(Request.QueryString["sHoraHasta"]);

            string sCompania = Request.QueryString["sCompania"];
            string sMotivo = Request.QueryString["sMotivo"];
            string sTipoVuelo = Request.QueryString["sTipoVuelo"];
            string sTipoPersona = Request.QueryString["sTipoPersona"];
            string sNumVuelo = Request.QueryString["sNumVuelo"];

            //Descripciones
            string idDscC = Request.QueryString["idDscC"];
            string idDscM = Request.QueryString["idDscM"];
            string idDscV = Request.QueryString["idDscV"];
            string idDscP = Request.QueryString["idDscP"];

            int iMaxReporte = GetMaximoReporte();
            if (Fecha.getFechaElapsed(sFechaDesde, sFechaHasta) <= iMaxReporte || iMaxReporte < 0) //Validacion Rango Fecha
            {
                consultarDatos(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCompania, sMotivo, sTipoVuelo, sTipoPersona, sNumVuelo);
                generarReporte(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, idDscC, idDscM, idDscV, idDscP, sNumVuelo);
            }
            else
            {
                crptBoardingRehabilitado.Visible = false;
                lblMensajeError.Text = String.Format(htLabels["tuua.general.lblMensajeError1.Text"].ToString(), iMaxReporte);
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
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_000;
                lblMensajeError.Text = "";
                lblMensajeErrorData.Text = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];

            }
        }
    }


    public void generarReporte(string sFechaDesde, string sFechaHasta, string sHoraDesde ,string sHoraHasta,
                                string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            if (dt_consulta == null || dt_consulta.Rows.Count == 0)
            {
                this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
            }
            else
            {
                //Pintar el reporte                 
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                obRpt.Load(Server.MapPath("").ToString() + @"\rptBoardingRehabilitados.rpt");
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", sFechaDesde);
                obRpt.SetParameterValue("pFechaFinal", sFechaHasta);
                obRpt.SetParameterValue("pMotivo", sMotivo);
                obRpt.SetParameterValue("pTipoVuelo", sTipoVuelo);
                obRpt.SetParameterValue("pAerolinea", sCompania);
                obRpt.SetParameterValue("pHoraInicial", sHoraDesde);
                obRpt.SetParameterValue("pHoraFinal", sHoraHasta);
                obRpt.SetParameterValue("pNumVuelo", sNumVuelo);
                obRpt.SetParameterValue("pTipoPasajero", sTipoPersona);

                crptBoardingRehabilitado.ReportSource = obRpt;
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

    public void consultarDatos(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta,
                                string sCompania, string sMotivo, string sTipoVuelo, string sTipoPersona, string sNumVuelo)
    {
        try
        {
            dt_consulta = objListarBPRehab.obtenerBoardingRehabilitados(sFechaDesde, 
                                                                        sFechaHasta, 
                                                                        sHoraDesde, 
                                                                        sHoraHasta, 
                                                                        sCompania, 
                                                                        sMotivo, 
                                                                        sTipoVuelo, 
                                                                        sTipoPersona, 
                                                                        sNumVuelo, 
                                                                        null, 0, 0, "1", "0", "0");
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

    protected int GetMaximoReporte()
    {
        int iMaxReporte = 0;
        DataTable dt_max = objConsulta.ListarParametrosDefaultValue("LR");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt32(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

}
