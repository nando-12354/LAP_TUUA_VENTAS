using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class ReporteRPT_rptCuadreTicketVendidos : System.Web.UI.Page
{
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
    bool Flg_Error;
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string FechaDesde = Request.QueryString["sFechaDesde"];
            string FechaHasta = Request.QueryString["sFechaHasta"];
            string idTipoDocumento = Request.QueryString["sTipoDocumento"];

            string sFechaDesde1 = FechaDesde;
            string sFechaHasta1 = FechaHasta;

            if (FechaDesde != "")
            {
                string[] wordsFechaDesde = FechaDesde.Split('/');
                FechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { FechaDesde = ""; }

            if (FechaHasta != "")
            {
                string[] wordsFechaHasta = FechaHasta.Split('/');
                FechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { FechaHasta = ""; }



            dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde, FechaHasta, idTipoDocumento, "0");

            if (dt_consulta.Rows.Count == 0)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
            else
            {                
                //Pintar el reporte   
                lblMensajeError.Text = "";
                obRpt.Load(Server.MapPath("").ToString() + @"\rptCuadreTicketVendidos.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", sFechaDesde1);
                obRpt.SetParameterValue("pFechaFinal", sFechaHasta1);
                if (idTipoDocumento.Trim() == "")
                {
                    obRpt.SetParameterValue("pTipoDocumento", "Ticket y Boarding");
                }
                else
                {
                    string sTipoDocu2 = idTipoDocumento == "B" ? "Boarding" : "Ticket";
                    obRpt.SetParameterValue("pTipoDocumento", sTipoDocu2);
                }

                crptvCuadreTicketEmitidosReport.ReportSource = obRpt;
                crptvCuadreTicketEmitidosReport.DataBind();

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


    //protected void crptvCuadreTicketEmitidos_Unload(object sender, EventArgs e)
    //{
    //    obRpt.Close();
    //    obRpt.Dispose();
    //}
}
