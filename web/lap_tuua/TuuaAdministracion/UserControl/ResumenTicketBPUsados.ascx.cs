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
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;

public partial class UserControl_ResumenTicketBPUsados : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    BO_Consultas objBOConsultas = new BO_Consultas();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void Inicio()
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblResumen.Text = htLabels["cnsBCBPUsado.lblTituloResumen"].ToString();
            CargarResumen();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
       mpext.Show();
    }

    
    public void CargarResumen()
    {
        //Pintar el reporte resumen
        DataTable dt_Consulta = (DataTable)Session["Data_TicketBPUsado"];
        CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        obRpt.Load(Server.MapPath("").ToString() + @"\ReporteCNS\rptCnsTicketUsados.rpt");
        //Poblar el reporte con el datatable
        
        obRpt.SetDataSource(dt_Consulta);
        crvResTipoDocumento.Visible = true;
        crvResTipoDocumento.Zoom(140);
        crvResTipoDocumento.ReportSource = obRpt;
    }

    public void Inicio(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo, string sEstado)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblResumen.Text = htLabels["cnsBCBPUsado.lblTituloResumen"].ToString();
            CargarResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero, sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
       mpext.Show();
    }

    public void CargarResumen(string sFechaDesde, string sFechaHasta, string sHoraDesde, string sHoraHasta, string sCodCompania, 
        string sTipoVuelo, string sNumVuelo, string sTipoPasajero, string sTipoDocumento, string sTipoTrasbordo, string sFechaVuelo,
        string sEstado)
    {
        DataTable dt_consulta = new DataTable();
        dt_consulta = objBOConsultas.ListarTicketBoardingUsadosResumen(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta,
            sCodCompania, sTipoVuelo, sNumVuelo, sTipoPasajero,
            sTipoDocumento, sTipoTrasbordo, sFechaVuelo, sEstado);

        //Pintar el reporte resumen
        //DataTable dt_Consulta = (DataTable)Session["Data_TicketBPUsado"];
        CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        obRpt.Load(Server.MapPath("").ToString() + @"\ReporteCNS\rptCnsTicketUsados.rpt");
        //Poblar el reporte con el datatable

        obRpt.SetDataSource(dt_consulta);
        //crvResTipoDocumento.Zoom(140);
        crvResTipoDocumento.ReportSource = obRpt;

    }
}
