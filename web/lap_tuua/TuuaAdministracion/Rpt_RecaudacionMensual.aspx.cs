using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.UTIL;
using System.Collections;
using LAP.TUUA.CONTROL;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Rpt_RecaudacionMensual : System.Web.UI.Page
{

    BO_Reportes objConsulta = new BO_Reportes();
    BO_Consultas objReporte = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
    bool Flg_Error;
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                lblFecha.Text = (String)htLabels["reporteResumenRecaudacionMensual.lblFecha.Text"];
                this.btnConsultar.Text = htLabels["reporteResumenRecaudacionMensual.btnConsultar.Text"].ToString();
                this.txtFecha.Text = Convert.ToString(DateTime.Now.Year);
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
        }
        CargarDataReporte();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        //CargarDataReporte();
    }

    public void CargarDataReporte()
    {
        try
        {
            string FechaAnnio = txtFecha.Text.Trim();
            
            dt_consulta = objConsulta.obtenerRecaudacionMensual(FechaAnnio);

            if (dt_consulta.Rows.Count == 0)
            {
                crptvRecaudacionMensual.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                lblMensajeErrorData.Text = "";
                string fechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + objReporte.obtenerFechaEstadistico("0");

                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptRecaudacionMensual.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFecha", FechaAnnio);
                obRpt.SetParameterValue("pFechaEstadistico", fechaEstadistico);
                
                crptvRecaudacionMensual.Visible = true;
                crptvRecaudacionMensual.ReportSource = obRpt;
                crptvRecaudacionMensual.DataBind();
               
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
