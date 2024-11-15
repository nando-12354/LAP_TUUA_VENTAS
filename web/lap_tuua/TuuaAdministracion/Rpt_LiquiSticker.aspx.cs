using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Rpt_LiquiSticker : System.Web.UI.Page
{
    BO_Reportes objConsulta = new BO_Reportes();
    BO_Consultas objReporte = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    DataSet dsLiqui = new DataSet();
    protected Hashtable htLabels;
    bool Flg_Error;
    string fechaEstadistico = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        
        if (!IsPostBack)
        {
            try
            {
                lblFecIni.Text = htLabels["reporteLiquiSticker.lblFecIni"].ToString();
                lblFecFin.Text = htLabels["reporteLiquiSticker.lblFecFin"].ToString();
                btnConsultar.Text = htLabels["reporteLiquiSticker.btnConsultar"].ToString();                
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
            txtFecIni.Text = DateTime.Now.ToShortDateString();
            txtFecFin.Text = DateTime.Now.ToShortDateString();
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
            string FechaDesde = Fecha.convertToFechaSQL2(txtFecIni.Text);
            string FechaHasta = Fecha.convertToFechaSQL2(txtFecFin.Text);

            dt_consulta = objConsulta.ConsultarLiquidacionVenta(FechaDesde, FechaHasta);
            dt_consulta.TableName = "dtLiquidVenta";
            dsLiqui.Tables.Add(dt_consulta.Copy());

            dt_consulta = objConsulta.ConsultarUsoContingencia(FechaDesde, FechaHasta);
            dt_consulta.TableName = "dtLiquiConti";
            dsLiqui.Tables.Add(dt_consulta.Copy());

            dt_consulta = objConsulta.ConsultarUsoContingenciaUsado(FechaDesde, FechaHasta);
            dt_consulta.TableName = "dtLiquiContiUsado";
            dsLiqui.Tables.Add(dt_consulta.Copy());

            if (dsLiqui.Tables[0].Rows.Count == 0 && dsLiqui.Tables[1].Rows.Count == 0)
            {
                crptvLiquiVenta.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString(); ;
            }
            else
            {
                fechaEstadistico = objReporte.obtenerFechaEstadistico("1");
                if (fechaEstadistico.Length < 1)
                {
                    fechaEstadistico = "";
                }
                else
                {
                    fechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + fechaEstadistico;
                }

                lblMensajeErrorData.Text = "";
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptLiquiSticker.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dsLiqui);
                obRpt.SetParameterValue("pFechaInicial", txtFecIni.Text);
                obRpt.SetParameterValue("pFechaFinal", txtFecFin.Text);
                obRpt.SetParameterValue("pEstadistico", fechaEstadistico);
                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                //icano 24-06-10 validar info
                if (dsLiqui.Tables[1].Rows.Count == 0)
                    obRpt.SetParameterValue("textoLiquiContiUso", "NO HAY INFORMACION DE CONTINGENCIA VENDIDA");
                else
                    obRpt.SetParameterValue("textoLiquiContiUso", "");

                if (dsLiqui.Tables[2].Rows.Count == 0)
                    obRpt.SetParameterValue("textoLiquiContiUsado", "NO HAY INFORMACION DE CONTINGENCIA USADO");
                else
                    obRpt.SetParameterValue("textoLiquiContiUsado", "");
                crptvLiquiVenta.Visible = true;
                crptvLiquiVenta.ReportSource = obRpt;
                crptvLiquiVenta.DataBind();
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
