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

public partial class Rpt_ResumenDiario : System.Web.UI.Page
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
                this.lblFecha.Text = htLabels["reporteResumenDiario.lblFecha"].ToString();
                this.lblRangoFecha.Text = htLabels["reporteResumenDiario.lblRangoFecha"].ToString();
                this.lblFechaDesde.Text = htLabels["reporteResumenDiario.lblFechaDesde"].ToString();
                this.lblFechaHasta.Text = htLabels["reporteResumenDiario.lblFechaHasta"].ToString();

                this.btnConsultar.Text = htLabels["reporteResumenDiario.btnConsultar.Text"].ToString();
                this.txtFecha.Text = DateTime.Now.ToShortDateString();

                rbtnFecha.Checked = true;                
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
    
}
