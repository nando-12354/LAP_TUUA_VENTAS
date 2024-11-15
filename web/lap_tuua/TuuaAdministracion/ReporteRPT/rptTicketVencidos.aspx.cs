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

public partial class ReporteRPT_rptTicketVencidos : System.Web.UI.Page
{
    //Capa Control
    BO_Reportes objReporte = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    //Labels
    protected Hashtable htLabels;
    //Error
    bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Filtros
            string FechaDesde = Fecha.convertToFechaSQL2(Request.QueryString["sFechaDesde"]);
            string FechaHasta = Fecha.convertToFechaSQL2(Request.QueryString["sFechaHasta"]);
            string sTipoTicket = Request.QueryString["sTipoTicket"];
            //Descripciones
            string sNombreTipoTicket = Request.QueryString["idDscT"];//Tipo Ticket 

            //Validacion Limite de Rango de Fechas en Reportes 
            DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");

            if (dt_parametro.Rows.Count > 0)
            {
                //RecuperarFiltros();
                DateTime fechaF = Convert.ToDateTime(Request.QueryString["sFechaHasta"]);
                DateTime fechaI = Convert.ToDateTime(Request.QueryString["sFechaDesde"]);
                TimeSpan ts = fechaF.Subtract(fechaI);
                int dias = ts.Days;
                int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

                if (dias > parametro)
                {
                    lblMensajeError.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
                }
                else
                {
                    DataTable dt_consulta = objReporte.ConsultarTicketVencidos(FechaDesde, FechaHasta, sTipoTicket);

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
                        /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                        if (dt_consulta.Columns[0].ColumnName == "MsgError")
                        {
                            this.lblMensajeError.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                            crptvTicketVencidos.ReportSource = null;
                            crptvTicketVencidos.DataBind();
                        }
                        else
                        {
                            //Pintar el reporte   
                            lblMensajeError.Text = "";

                            CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                            obRpt.Load(Server.MapPath("").ToString() + @"\rptTicketVencidos.rpt");
                            //Poblar el reporte con el datatable
                            obRpt.SetDataSource(dt_consulta);

                            obRpt.SetParameterValue("sFechaInicio", Request.QueryString["sFechaDesde"] == "" ? "Sin Filtro" : Request.QueryString["sFechaDesde"]);
                            obRpt.SetParameterValue("sFechaFinal", Request.QueryString["sFechaHasta"] == "" ? "Sin Filtro" : Request.QueryString["sFechaHasta"]);
                            obRpt.SetParameterValue("sTipoTicket", sTipoTicket == "0" ? "Todos" : sNombreTipoTicket);
                            obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                            crptvTicketVencidos.ReportSource = obRpt;
                            crptvTicketVencidos.DataBind();
                        }
                    }
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
}
