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

public partial class Rpt_LiquidacionVenta : System.Web.UI.Page
{

    BO_Reportes objReporte = new BO_Reportes();
    DataTable dt_reporte = new DataTable();
    DataTable dt_reporte_resumen = new DataTable();
    BO_Consultas objConsulta = new BO_Consultas();
    
    protected Hashtable htLabels;
    bool Flg_Error;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                this.lblFechaInicial.Text = (String)htLabels["reporteLiquidacionVenta.lblFechaInicial.Text"];
                this.lblFechaFinal.Text = (String)htLabels["reporteLiquidacionVenta.lblFechaFinal.Text"];
                this.btnConsultar.Text = (String)htLabels["reporteLiquidacionVenta.btnConsultar.Text"];
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
            this.txtFechaInicio.Text = DateTime.Now.ToShortDateString();
            this.txtFechaFin.Text = DateTime.Now.ToShortDateString();
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
            string FechaDesde = Fecha.convertToFechaSQL2(txtFechaInicio.Text);
            string FechaHasta = Fecha.convertToFechaSQL2(txtFechaFin.Text);
            string fechaEstadistico = "";

            dt_reporte = objReporte.obtenerLiquidacionVenta(FechaDesde, FechaHasta);
            dt_reporte_resumen = objReporte.obtenerLiquidacionVentaResumen(FechaDesde, FechaHasta);

            if (dt_reporte.Rows.Count == 0)
            {
                crptvLiquidacionVenta.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                fechaEstadistico = objConsulta.obtenerFechaEstadistico("1");
                if (fechaEstadistico.Length < 1)
                {
                    fechaEstadistico = "";
                }
                else {
                    fechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico; 
                }
                
                lblMensajeErrorData.Text = "";
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptLiquidacionVenta.rpt");
                //Poblar el reporte con el datatable
                DataSet dtset = new DataSet();
                dt_reporte.TableName = "dtLiquidacionVenta";
                dtset.Tables.Add(dt_reporte.Copy());
                dt_reporte_resumen.TableName = "dtLiquidacionVentaResumen";
                dtset.Tables.Add(dt_reporte_resumen.Copy());

                obRpt.SetDataSource(dtset);
                obRpt.SetParameterValue("pFechaInicial", txtFechaInicio.Text);
                obRpt.SetParameterValue("pFechaFinal", txtFechaFin.Text);
                obRpt.SetParameterValue("pFechaEstadistico",fechaEstadistico);
                
                crptvLiquidacionVenta.Visible = true;
                crptvLiquidacionVenta.ReportSource = obRpt;
                crptvLiquidacionVenta.DataBind();
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
