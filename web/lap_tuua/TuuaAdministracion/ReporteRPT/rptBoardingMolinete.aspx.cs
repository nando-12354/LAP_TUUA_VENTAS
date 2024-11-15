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

public partial class ReporteRPT_rptBoardingMolinete : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    DataTable dt_BoardingLeidos = new DataTable();
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();

    string idCompania, sFechaVuelo, sNumVuelo, sFechaLecturaIni, sFechaLecturaFin, idEstado, sNumBoarding, pCompaniaTexto, pEstadoTexto;


    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            idCompania = Request.QueryString["idCompania"];
            sFechaVuelo = Fecha.convertToFechaSQL2(Request.QueryString["sFechaVuelo"]);
            sNumVuelo = Request.QueryString["sNumVuelo"];
            sFechaLecturaIni = Fecha.convertToFechaSQL2(Request.QueryString["sFechaLecturaIni"]);
            sFechaLecturaFin = Fecha.convertToFechaSQL2(Request.QueryString["sFechaLecturaFin"]);
            idEstado = Request.QueryString["idEstado"];
            sNumBoarding = Request.QueryString["sNumBoarding"];

            pCompaniaTexto = Request.QueryString["pCompaniaTexto"];
            pEstadoTexto = Request.QueryString["pEstadoTexto"];

            DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");
            if (dt_parametro.Rows.Count > 0)
            {
                //RecuperarFiltros();
                DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFechaLecturaFin"]);
                DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFechaLecturaIni"]);
                TimeSpan ts = fechaF.Subtract(fechaI);
                int dias = ts.Days;
                int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

                if (dias > parametro)
                {
                    lblMensajeError.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
                }
                else
                {

                    consultarDatos(idCompania, sFechaVuelo, sNumVuelo, sFechaLecturaIni, sFechaLecturaFin, idEstado, sNumBoarding);
                    generarReporte(idCompania, sFechaVuelo, sNumVuelo, sFechaLecturaIni, sFechaLecturaFin, idEstado, sNumBoarding, pCompaniaTexto, pEstadoTexto);
                }

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
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_000;
                lblMensajeError.Text = "";
                lblMensajeErrorData.Text = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];

            }
        }
    }
    
    

    public void generarReporte(string idCompania, string sFechaVuelo, string sNumVuelo, string sFechaLecturaIni, string sFechaLecturaFin,
                               string idEstado, string sNumBoarding, string pCompaniaTexto, string pEstadoTexto)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            if (dt_BoardingLeidos == null || dt_BoardingLeidos.Rows.Count == 0)
            {
                this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
            }
            else
            {
                /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_BoardingLeidos.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeError.Text = dt_BoardingLeidos.Rows[0]["Descripcion"].ToString();
                    crvBoardingMolinete.ReportSource = null;
                    crvBoardingMolinete.DataBind();

                }
                else
                {
                    //Pintar el reporte 
                    lblMensajeError.Text = "";
                    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt =
                        new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    obRpt.Load(Server.MapPath("").ToString() + @"\rptBoardingLeidosMolinete.rpt");
                    //Poblar el reporte con el datatable
                    obRpt.SetDataSource(dt_BoardingLeidos);
                    obRpt.SetParameterValue("sCompania", pCompaniaTexto == "0" ? "Todos" : pCompaniaTexto);
                    obRpt.SetParameterValue("sFechaVuelo",
                                            Request.QueryString["sFechaVuelo"] == ""
                                                ? "Sin Filtro"
                                                : Request.QueryString["sFechaVuelo"]);
                    obRpt.SetParameterValue("sNumVuelo", sNumVuelo == "" ? "Sin Filtro" : sNumVuelo);
                    obRpt.SetParameterValue("sFechaLecturaIni",
                                            Request.QueryString["sFechaLecturaIni"] == ""
                                                ? "Sin Filtro"
                                                : Request.QueryString["sFechaLecturaIni"]);
                    obRpt.SetParameterValue("sFechaLecturaFin",
                                            Request.QueryString["sFechaLecturaFin"] == ""
                                                ? "Sin Filtro"
                                                : Request.QueryString["sFechaLecturaFin"]);
                    //obRpt.SetParameterValue("sEstado", pEstadoTexto == "0" ? "Todos" : pEstadoTexto);
                    obRpt.SetParameterValue("sNumBoarding", sNumBoarding == "" ? "Sin Filtro" : sNumBoarding);
                    //obRpt.SetParameterValue("sFechaEstadistico", sFechaEstadistico == "" ? "Sin Filtro" : sFechaEstadistico);

                    crvBoardingMolinete.Visible = true;
                    crvBoardingMolinete.ReportSource = obRpt;
                    crvBoardingMolinete.DataBind();
                }
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

    public void consultarDatos(string idCompania, string sFechaVuelo, string sNumVuelo, string sFechaLecturaIni, string sFechaLecturaFin,
                               string idEstado, string sNumBoarding)
    {

        dt_BoardingLeidos = objReporte.ListarBoardingLeidosMolinetePagin(idCompania, 
                                                                        sFechaVuelo, 
                                                                        sNumVuelo, 
                                                                        sFechaLecturaIni, 
                                                                        sFechaLecturaFin, 
                                                                        "0", 
                                                                        sNumBoarding, 
                                                                        "0", null, 0, 0, "0");
        
    }
}
