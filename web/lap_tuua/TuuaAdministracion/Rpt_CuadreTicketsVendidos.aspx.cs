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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections.Generic;


public partial class Cns_CuadreTicketsEmitidos : System.Web.UI.Page
{
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    
    protected Hashtable htLabels;
    bool Flg_Error;

    string sFechaDesde;
    string sFechaHasta;
    string sTipoDocumentoT;
    string sTipoDocumentoB;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        
        if (!IsPostBack)
        {
            try
            {
                this.lblTipoDocumento.Text = htLabels["mconsultaDetalleturno.lblTipoDocumento.Text"].ToString();
                this.lblDesde.Text = htLabels["mconsultaDetalleturno.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaDetalleturno.lblHasta.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaDetalleturno.btnConsultar.Text"].ToString();
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
                    Response.Redirect("PaginaError.aspx");                }
            }
            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();
        }

        SaveFiltros();
        IsRangoValido();
    }

    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sTipoDocumentoT", chkTicket.Checked.ToString()));
        filterList.Add(new Filtros("sTipoDocumentoB", chkBoarding.Checked.ToString()));

        ViewState.Add("Filtros", filterList);
    }

    public void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sTipoDocumentoT = newFilterList[2].Valor;
        sTipoDocumentoB = newFilterList[3].Valor;
     
    }

    public void IsRangoValido() {

        DataTable dt_parametro = objConsulta.ListarParametrosDefaultValue("LR");

        if (dt_parametro.Rows.Count > 0)
        {

            DateTime fechaF = Convert.ToDateTime(txtHasta.Text);
            DateTime fechaI = Convert.ToDateTime(txtDesde.Text);
            TimeSpan ts = fechaF.Subtract(fechaI);
            int dias = ts.Days;
            int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

            if (dias > parametro)
            {
                lblMensajeErrorData.Text = "El periodo máximo de días a consultar el reporte es" + " " + parametro.ToString() + " " + "días.";
            }
            else
            {
                CargarDataReporte();
            }
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        SaveFiltros();
        IsRangoValido();
        //CargarDataReporte();
    }

    public void CargarDataReporte()
    {
        try
        {
            RecuperarFiltros();
            //string FechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            //string FechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
            string idTipoDocumento = "";
            if (sTipoDocumentoT == "True" && sTipoDocumentoB == "False")
                idTipoDocumento = "T";
            else if (sTipoDocumentoT == "False" && sTipoDocumentoB == "True")
                idTipoDocumento = "B";
            else
                idTipoDocumento = "";

            dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(sFechaDesde, sFechaHasta, idTipoDocumento, "0");

            if (dt_consulta.Rows.Count == 0)
            {
                crptvCuadreTicketEmitidosReport.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
            }
            else
            {
                lblMensajeErrorData.Text = "";

                string fechaEstadistico = htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + objConsulta.obtenerFechaEstadistico("0");

                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteRPT\rptCuadreTicketVendidos.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", sFechaDesde);
                obRpt.SetParameterValue("pFechaFinal", sFechaHasta);
                obRpt.SetParameterValue("pFechaEstadistico", fechaEstadistico);

                if (idTipoDocumento.Trim() == "")
                {
                    obRpt.SetParameterValue("pTipoDocumento", "Ticket y Boarding");
                }
                else
                {
                    string sTipoDocu2 = idTipoDocumento == "B" ? "Boarding" : "Ticket";
                    obRpt.SetParameterValue("pTipoDocumento", sTipoDocu2);
                }
                obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
                crptvCuadreTicketEmitidosReport.Visible = true;
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
}
