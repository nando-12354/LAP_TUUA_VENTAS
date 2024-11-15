using System;
using System.Linq;
using System.Xml.Linq;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class ReporteCNS_rptCuadreTicketEmitidos : System.Web.UI.Page
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

            string sFechaDesde = Request.QueryString["sFechaDesde"];
            string sFechaHasta = Request.QueryString["sFechaHasta"];
            string sTipoDocumento = Request.QueryString["sTipoDocumento"];

            string sFechaDesde1 = sFechaDesde;
            string sFechaHasta1 = sFechaHasta;


            if (sFechaDesde != "")
            {
                string[] wordsFechaDesde = sFechaDesde.Split('/');
                sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { sFechaDesde = ""; }

            if (sFechaHasta != "")
            {
                string[] wordsFechaHasta = sFechaHasta.Split('/');
                sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { sFechaHasta = ""; }




            dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(sFechaDesde, sFechaHasta, sTipoDocumento, "0");



            if (sTipoDocumento.Equals("B"))
            {
                if (dt_consulta.Rows.Count > 0)
                {
                    var lqList = from rowConsulta in dt_consulta.AsEnumerable()
                                 where rowConsulta.Field<String>("Dsc_Campo") != "Vend." && rowConsulta.Field<String>("Dsc_Campo") != "Emit."
                                 select rowConsulta;

                    dt_consulta = lqList.CopyToDataTable();
                }
            }

            if (dt_consulta.Rows.Count == 0)
            {
                crvCuadreTicketsEmitidos.Visible = false;
                lblMensajeError.Text = "La busqueda no retorna resultado";
            }
            else
            {
                lblMensajeError.Text = "";               
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\rptCuadreTicket.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", sFechaDesde1);
                obRpt.SetParameterValue("pFechaFinal", sFechaHasta1);
                string sTipoDocu2 = sTipoDocumento == "B" ? "Boarding" : "Ticket";
                obRpt.SetParameterValue("pTipoDocumento", sTipoDocu2);

                crvCuadreTicketsEmitidos.Visible = true;
                crvCuadreTicketsEmitidos.ReportSource = obRpt;
                crvCuadreTicketsEmitidos.DataBind();

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
