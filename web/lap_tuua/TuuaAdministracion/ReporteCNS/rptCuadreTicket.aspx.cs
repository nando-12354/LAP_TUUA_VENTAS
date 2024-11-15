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

public partial class ReporteCNS_rptCuadreTicket : System.Web.UI.Page
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
            string FechaDesde = Request.QueryString["idFechaDesde"];
            string FechaHasta = Request.QueryString["idFechaHasta"];
            string idTipoDocumento = Request.QueryString["idTipoDocumento"];

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



            dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(FechaDesde,idTipoDocumento, FechaHasta,"0");

            if (dt_consulta.Rows.Count == 0)
            {
                Response.Write("La busqueda no retorna resultado, regrese a la consulta para un nuevo filtro");
            }
            else
            {                
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\rptCuadreTicketReport.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
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
