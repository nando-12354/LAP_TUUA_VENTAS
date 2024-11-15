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
using CrystalDecisions.CrystalReports.Engine;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;

public partial class ReporteREH_ReporteREH_BCBP : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["dtBCBPRehabilitados"] != null)
        {
            DataTable dtBCBPRehabilitados = (DataTable)Session["dtBCBPRehabilitados"];
            obRpt.Load(Server.MapPath("").ToString() + @"\BoardingBcbp.rpt");
            //Poblar el reporte con el datatable
            obRpt.SetDataSource(dtBCBPRehabilitados);
            crptvBoardings.ReportSource = obRpt;

            Usuario objUsuario = objBOSeguridad.obtenerUsuario(Session["Cod_Usuario"].ToString());
            TextObject txtUsuario = (TextObject)obRpt.ReportDefinition.ReportObjects["txtUsuario"];
            txtUsuario.Text = objUsuario.SNomUsuario + " " + objUsuario.SApeUsuario;

            String titulo = Request.QueryString.Get("titulo");
            TextObject txtTitulo = (TextObject)obRpt.ReportDefinition.ReportObjects["txtTitulo"];
            txtTitulo.Text = titulo;

            //CrystalDecisions.CrystalReports.Engine.FormulaFieldDefinition crFormulaTextField1;
            //FormulaFieldDefinitions crFormulas;
            //crFormulas = obRpt.DataDefinition.FormulaFields;
            //crFormulaTextField1 = crFormulas[0];
            //crFormulaTextField1.Text = "Rehabilitacion";

            //obRpt.DataDefinition.FormulaFields["txtRehabilitacion"].Text = "Rehabilitacion";

        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
