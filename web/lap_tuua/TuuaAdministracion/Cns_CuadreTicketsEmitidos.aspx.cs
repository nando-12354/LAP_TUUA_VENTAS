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
using System.Text;


public partial class Cns_CuadreTicketsEmitidos : System.Web.UI.Page
{
    BO_Consultas objConsulta = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    protected Hashtable htLabels;
    bool Flg_Error;
    CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    string sFechaDesde;
    string sFechaHasta;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {                
                lblTipoDocumento.Text = htLabels["mconsultaDetalleturno.lblTipoDocumento.Text"].ToString();
                lblDesde.Text = htLabels["mconsultaDetalleturno.lblDesde.Text"].ToString();
                lblHasta.Text = htLabels["mconsultaDetalleturno.lblHasta.Text"].ToString();                
                btnConsultar.Text = htLabels["mconsultaDetalleturno.btnConsultar.Text"].ToString();
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


            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();
        }

        SaveFiltros();
        IsRangoValido();
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
       

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
 
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        SaveFiltros();
        IsRangoValido();

        //SaveFiltros();

        //DataTable dt_parametro = objConsulta.ListarParametros("LR");

        //if (dt_parametro.Rows.Count > 0)
        //{

        //        DateTime fechaF = Convert.ToDateTime(txtHasta.Text);
        //        DateTime fechaI = Convert.ToDateTime(txtDesde.Text);
        //        TimeSpan ts = fechaF.Subtract(fechaI);
        //        int dias = ts.Days;
        //        int parametro = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());

        //        if (dias > parametro)
        //        {
        //            lblMensajeErrorData.Text = "El periodo máximo de días a imprimir el reporte es" + " " + parametro.ToString() + " " + "días.";
        //        }
        //        else
        //        {
        //            CargarDataReporte();
        //        }

        //}
    }

    public void IsRangoValido()
    {

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

    public void CargarDataReporte()
    {
        try
        {
            //string FechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
            //string FechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
            RecuperarFiltros();

            string sTipoDocumento = (rbtBoarding.Checked)?"B":"T";
           
            dt_consulta = objConsulta.obtenerCuadreTickesEmitidos(sFechaDesde, sFechaHasta, sTipoDocumento, "0");
 
            if (sTipoDocumento.Equals("B"))
            {
                if (dt_consulta.Rows.Count > 0)
                {
                    DataView consulta = new DataView();
                    consulta = dt_consulta.DefaultView;

                    consulta.RowFilter = "Dsc_Campo <> 'Vend.' and Dsc_Campo <> 'Emit.'";

                    //var lqList = from rowConsulta in dt_consulta.AsEnumerable()
                    //             where rowConsulta.Field<String>("Dsc_Campo") != "Vend." && rowConsulta.Field<String>("Dsc_Campo") != "Emit."
                    //             select rowConsulta;

                    //dt_consulta = lqList.CopyToDataTable();

                    dt_consulta = consulta.Table;
                }
            }

            if (dt_consulta.Rows.Count == 0)
            {
                crvCuadreTicketsEmitidos.Visible = false;
                lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString(); 
            }
            else
            {
                lblMensajeErrorData.Text = "";
                string fechaEstadistico =  htLabels["mconsultaCuadreTicket.lblFechaEstadistico"].ToString() + " " + objConsulta.obtenerFechaEstadistico("0");
                
                CrystalDecisions.CrystalReports.Engine.ReportDocument obRpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //Pintar el reporte                 
                obRpt.Load(Server.MapPath("").ToString() + @"\ReporteCNS\rptCuadreTicket.rpt");
                //Poblar el reporte con el datatable
                obRpt.SetDataSource(dt_consulta);
                obRpt.SetParameterValue("pFechaInicial", txtDesde.Text);
                obRpt.SetParameterValue("pFechaFinal", txtHasta.Text);
                string sTipoDocu2 = sTipoDocumento == "B" ? "Boarding" : "Ticket";
                obRpt.SetParameterValue("pTipoDocumento", sTipoDocu2);
                obRpt.SetParameterValue("pFechaEstadistico", fechaEstadistico);
                //obRpt.SetParameterValue("pCuenta", (string)Session["Cta_Usuario"]);
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

    protected void crvCuadreTicketsEmitidos_Unload(object sender, EventArgs e)
    {
        obRpt.Close();
        obRpt.Dispose();
    }
}
