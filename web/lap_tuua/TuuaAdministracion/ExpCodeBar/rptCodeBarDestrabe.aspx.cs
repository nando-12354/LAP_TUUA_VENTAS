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
using System.Xml.Linq;
//using LAP.TUUA.ENTIDADES;
//using LAP.TUUA.CONTROL;
//using LAP.TUUA.UTIL;
using System.Xml;
//using LAP.TUUA.ALARMAS;
using System.IO;
//using LAP.TUUA.CONVERSOR;

public partial class ExpCodeBar_rptCodeBarDestrabe : System.Web.UI.Page
{

    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt;

    protected void Page_Load(object sender, EventArgs e)
    {
        String sCodDestrabe = Request.QueryString["codDestrabe"];
        String sCodMolinete = Request.QueryString["codMolinete"];

        DataTable dtCodeBar = new DataTable();
        dtCodeBar.Columns.Add("CodDestrabe");
        dtCodeBar.Columns.Add("CodMolinete");
        dtCodeBar.Columns.Add("DesDestrabe");
        dtCodeBar.Columns.Add("DesMolinete");
        DataRow dr = dtCodeBar.NewRow();
        dr["CodDestrabe"] = sCodDestrabe;
        dr["CodMolinete"] = sCodMolinete;
        dr["DesDestrabe"] = string.IsNullOrEmpty(sCodDestrabe) ? "" : "Codigo destrabe";
        dr["DesMolinete"] = string.IsNullOrEmpty(sCodMolinete) ? "" : "Codigo molinete";
        dtCodeBar.Rows.Add(dr);

        if (dtCodeBar.Rows[0]["CodDestrabe"].ToString().Equals("") && dtCodeBar.Rows[0]["CodMolinete"].ToString().Equals(""))
        {            
            lblMensaje.Text = "No hay datos que mostrar.";            
        }
        else
        {
            //Pintar el reporte                 
            obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            obRpt.Load(Server.MapPath("").ToString() + @"\rptCodeBar.rpt");
            //rptDetalleVentaCompania rptRecaudacionMensual
            obRpt.SetDataSource(dtCodeBar);
            //obRpt.SetParameterValue("pETicket", sETicket);
            //CrystalReportViewer1.ReportSource = obRpt;
            ExpotacionReport();
        }

    }

    public void ExpotacionReport()
    {
       
            MemoryStream oStream; 
            oStream = (MemoryStream)obRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=destrabe.pdf");
            Response.BinaryWrite(oStream.ToArray());
            
            Response.Flush();
            Response.End();
            
    }

}
