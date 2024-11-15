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

public partial class ReporteRPT_rptLiquidacionVenta : System.Web.UI.Page
{

    protected bool Flg_Error;
    DataTable dt_consulta = new DataTable();
    DataTable dt_consulta_resumen = new DataTable();
    BO_Reportes objConsulta = new BO_Reportes();
    protected Hashtable htLabels;

    protected void Page_Load(object sender, EventArgs e)
    {
        string sFechaDesde = Request.QueryString["fechainicial"];
        string sFechaHasta = Request.QueryString["fechafinal"];

        if (ViewState["ConsultaLiquidacionVenta"] != null)
        {
            dt_consulta = (DataTable)ViewState["ConsultaLiquidacionVenta"];
            dt_consulta_resumen = (DataTable)ViewState["ConsultaLiquidacionVentaResumen"];
            generarReporte(sFechaDesde, sFechaHasta);
        }
        else
        {
            consultarDatos(sFechaDesde, sFechaHasta);
            generarReporte(sFechaDesde, sFechaHasta);
        }
    }

    public void generarReporte(string sFechaDesde, string sFechaHasta)
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
                obRpt.Load(Server.MapPath("").ToString() + @"\rptLiquidacionVenta.rpt");

                DataSet dtset = new DataSet();

                dt_consulta.TableName = "dtLiquidacionVenta";
                dtset.Tables.Add(dt_consulta.Copy());
                dt_consulta_resumen.TableName = "dtLiquidacionVentaResumen";                
                dtset.Tables.Add(dt_consulta_resumen.Copy());

                obRpt.SetDataSource(dtset);
                obRpt.SetParameterValue("pFechaInicial", sFechaDesde);
                obRpt.SetParameterValue("pFechaFinal", sFechaHasta);

                crptvLiquidacionVenta.ReportSource = obRpt;
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

    public void consultarDatos(string sFechaDesde, string sFechaHasta)
    {

        if (sFechaDesde != "" && sFechaDesde != null)
        {
            string[] wordsFechaDesde = sFechaDesde.Split('/');
            sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }

        if (sFechaHasta != "" && sFechaHasta != null)
        {
            string[] wordsFechaHasta = sFechaHasta.Split('/');
            sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        dt_consulta = objConsulta.obtenerLiquidacionVenta(sFechaDesde, sFechaHasta);
        dt_consulta_resumen = objConsulta.obtenerLiquidacionVentaResumen(sFechaDesde, sFechaHasta);

        if (dt_consulta == null || dt_consulta.Rows.Count == 0)
        {
            ViewState["ConsultaLiquidacionVentaResumen"] = dt_consulta_resumen;
            ViewState["ConsultaLiquidacionVenta"] = dt_consulta;
        }

    }
}
