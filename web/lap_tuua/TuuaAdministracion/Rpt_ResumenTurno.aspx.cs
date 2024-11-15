using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using Define = LAP.TUUA.UTIL.Define;

public partial class Rpt_ResumenTurno : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    string sFechaDesde;
    string sFechaHasta;
    string sCajero;
    string sTurno;

    protected BO_Consultas objBOConsulta = new BO_Consultas();

    Int32 valorMaxGrilla;
    DataTable dt_parametroGeneral = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblTurno.Text = htLabels["reporteResumenTurno.lblTurno"].ToString();
                this.lblRangoTurno.Text = htLabels["reporteResumenTurno.lblRangoTurno"].ToString();
                this.lblRangoDesde.Text = htLabels["reporteResumenTurno.lblRangoDesde"].ToString();
                this.lblRangoHasta.Text = htLabels["reporteResumenTurno.lblRangoHasta"].ToString();
                this.btnConsultar.Text = htLabels["reporteResumenTurno.btnConsultar.Text"].ToString();
                rbtnTurno.Checked = true;
            }

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
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CargarGrillaResumen();
    }
    /// <summary>
    /// Carga la grilla resumen diario
    /// </summary>
    protected void CargarGrillaResumen()
    {

    }
}
