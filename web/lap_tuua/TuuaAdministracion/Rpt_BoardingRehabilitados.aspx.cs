using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using System.Collections.Generic;
using Define = LAP.TUUA.UTIL.Define;

public partial class Rpt_TicketBoardingRehabilitados : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    string sCompania;
    string sHoraDesde;
    string sHoraHasta;
    string sFechaDesde;
    string sFechaHasta;
    string sMotivo;
    string sTipoVuelo;
    string sTipoPersona;
    string sNumVuelo;
    
    BO_Reportes objListarBPRehabilitados = new BO_Reportes();
    BO_Consultas objConsulta = new BO_Consultas();
    BO_Consultas objParametro = new BO_Consultas();

    Int32 valorMaxGrilla;
    DataTable dt_consulta = new DataTable();
    DataTable dt_resumen = new DataTable();
    DataTable dt_parametroTurno = new DataTable();
    DataTable dt_allcompania = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblFechaDesde.Text = htLabels["reporteBoardingRehabilitado.lblFechaDesde.Text"].ToString();
                this.lblFechaHasta.Text = htLabels["reporteBoardingRehabilitado.lblFechaHasta.Text"].ToString();
                this.lblTipoVuelo.Text = htLabels["reporteBoardingRehabilitado.lblTipoVuelo.Text"].ToString();
                this.btnConsultar.Text = htLabels["reporteBoardingRehabilitado.btnConsultar"].ToString();
                this.lblAerolinea.Text = htLabels["reporteBoardingRehabilitado.lblAerolinea.Text"].ToString();
                this.lblMotivo.Text = htLabels["reporteBoardingRehabilitado.lblMotivo.Text"].ToString();
                this.lblVuelo.Text = htLabels["reporteBoardingRehabilitado.lblVuelo.Text"].ToString();
                this.lblFecha.Text = htLabels["reporteBoardingRehabilitado.lblFecha.Text"].ToString();
                this.lblTipoPersona.Text = htLabels["reporteBoardingRehabilitado.lblTipoPersona.Text"].ToString();

                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
                //Session["TicketBoardingRehabil"] = null;
                CargarCombos();
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

    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo persona
            DataTable dt_tipopersona = new DataTable();
            dt_tipopersona = objConsulta.ListaCamposxNombre("TipoPasajero");
            UIControles objCargaComboPersona = new UIControles();
            objCargaComboPersona.LlenarCombo(ddlTipoPersona, dt_tipopersona, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga combo Tipo Vuelo
            DataTable dt_tipovuelo = new DataTable();
            dt_tipovuelo = objConsulta.ListaCamposxNombre("TipoVuelo");
            UIControles objCargaComboVuelo = new UIControles();
            objCargaComboVuelo.LlenarCombo(ddlTipoVuelo, dt_tipovuelo, "Cod_Campo", "Dsc_Campo", true, false);

            // carga combo Compañia y motivo
            UIControles objCargaCombo = new UIControles();
            dt_allcompania = objConsulta.listarAllCompania();
            objCargaCombo.LlenarCombo(ddlTipoAerolinea, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            // Carga combo Motivo Rehabilitacion
            BO_Consultas objListaCampos = new BO_Consultas();
            DataTable dtCausalReh = objListaCampos.ListaCamposxNombre("CausalRehabilitacion");
            objCargaCombo.LlenarCombo(ddlMotivo, dtCausalReh, "Cod_Campo", "Dsc_Campo", true, false);
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

    protected Int64 GetMaximoExcel()
    {
        Int64 iMaxReporte = 0;
        BO_Consultas objParametro = new BO_Consultas();
        DataTable dt_max = objParametro.ListarParametros("LE");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt64(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (IsFechaValida())
        {
            SaveFiltros();
            BindPagingGrid();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
        }   
    }

    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        grvBoardingRehabilita.VirtualItemCount = GetRowCount();

        dt_consulta = GetDataPage(grvBoardingRehabilita.PageIndex, grvBoardingRehabilita.PageSize, grvBoardingRehabilita.OrderBy);

        if (dt_consulta.Rows.Count == 0)
        {
            grvBoardingRehabilita.Visible = false;
            lblMensajeErrorData.Text = "La búsqueda no retorna resultado";
            lblTotal.Text = "";
            lblTotalRows.Value = "";
        }
        else
        {
            lblMensajeErrorData.Text = "";

            grvBoardingRehabilita.Visible = true;
            grvBoardingRehabilita.DataSource = dt_consulta;
            grvBoardingRehabilita.PageSize = valorMaxGrilla;
            grvBoardingRehabilita.DataBind();

            lblTotal.Text = "Total de Registros:" + grvBoardingRehabilita.VirtualItemCount;
            lblTotalRows.Value = grvBoardingRehabilita.VirtualItemCount.ToString();
        }
    }

    #region Dynamic data query
    private int GetRowCount()
    {
        int count = 0;
        try
        {
            dt_resumen = objListarBPRehabilitados.obtenerBoardingRehabilitados(sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sCompania,
                                                                                sMotivo,
                                                                                sTipoVuelo,
                                                                                sTipoPersona,
                                                                                sNumVuelo,
                                                                                null, 0, 0, "0", "0", "1");

            if (dt_resumen.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_resumen.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_resumen.Rows[0].ItemArray.GetValue(1).ToString();

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
        return count;

    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        try
        {
            dt_consulta = objListarBPRehabilitados.obtenerBoardingRehabilitados(sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sCompania,
                                                                                sMotivo,
                                                                                sTipoVuelo,
                                                                                sTipoPersona,
                                                                                sNumVuelo,
                                                                                sortExpression, pageIndex, valorMaxGrilla, "1", "0", "0");
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

        return dt_consulta;
    }
    #endregion

    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtFechaDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtFechaHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sCompania", ddlTipoAerolinea.SelectedValue));
        filterList.Add(new Filtros("sMotivo", ddlMotivo.SelectedValue));
        filterList.Add(new Filtros("sTipoVuelo", ddlTipoVuelo.SelectedValue));
        filterList.Add(new Filtros("sTipoPersona", ddlTipoPersona.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", txtNumVuelo.Text));

        ViewState.Add("Filtros", filterList);
    }

    public void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;

        sCompania = newFilterList[4].Valor;
        sMotivo = newFilterList[5].Valor;
        sTipoVuelo = newFilterList[6].Valor;
        sTipoPersona = newFilterList[7].Valor;
        sNumVuelo = newFilterList[8].Valor;
    }

    private bool IsFechaValida()
    {
        int iValiFechas;
        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;
            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";

            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtFechaDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtFechaHasta.Text + " " + pNewHraHasta));
        }
        else
        {
            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtFechaDesde.Text + " " + sHoraDesde), Convert.ToDateTime(this.txtFechaHasta.Text + " " + sHoraHasta));
        }

        if (iValiFechas == 1)
        {
            lblMensajeError.Text = "Filtro de fecha invalido";
            lblMensajeErrorData.Text = "";
            grvBoardingRehabilita.Visible = false;
            lblTotal.Text = "";
            lblTotalRows.Value = "";
            return false;
        }
        else
        {
            this.lblMensajeError.Text = "";
            return true;
        }
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general              
        dt_parametroTurno = objParametro.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected void grvBoardingRehabilita_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvBoardingRehabilita.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvBoardingRehabilita_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());

            string codigo = grvBoardingRehabilita.DataKeys[rowIndex].Value.ToString();
            CnsDetBoarding1.CargarDetalleBoarding(codigo);
        }
    }

    protected void grvBoardingRehabilita_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindPagingGrid();
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=BPRehabilitados.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            dt_consulta = objListarBPRehabilitados.obtenerBoardingRehabilitados(sFechaDesde,
                                                                                sFechaHasta,
                                                                                sHoraDesde,
                                                                                sHoraHasta,
                                                                                sCompania,
                                                                                sMotivo,
                                                                                sTipoVuelo,
                                                                                sTipoPersona,
                                                                                sNumVuelo,
                                                                                null, 0, 0, "1", "0", "0");
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

        #endregion

        Excel Workbook = new Excel();

        #region Reporte BP Rehabilitados
        Excel.Worksheet bpRehabilitados = new Excel.Worksheet("Reporte BP Rehabilitados");
        bpRehabilitados.Columns = new string[] { "Fecha Ultimo Uso", "Nombre Pasajero", "Nro. Asiento", "Compañia","Fecha Vuelo", 
                                                    "Nro. Vuelo", "Fecha Rehab.", "Motivo", "Secuencial", "Nro. SEAE", "Codigo Rehab.", "Tipo Vuelo" };

        bpRehabilitados.WidthColumns = new int[] { 100, 170, 60, 170, 80, 60, 100, 170, 80, 90, 80, 70 };
        bpRehabilitados.DataFields = new string[] { "Fch_Uso", "Nom_Pasajero", "Num_Asiento", "Dsc_Compania", "Fch_Vuelo", 
                                                        "Num_Vuelo", "Fch_Rehabilitacion", "DesMotivo", "Secuencial", "Cod_Numero_Bcbp", "Num_Proceso_Rehab", "TipoVuelo" };
        bpRehabilitados.Source = dt_consulta;
        #endregion


        Workbook.Worksheets = new Excel.Worksheet[] { bpRehabilitados };

        return Workbook.Save();
    }
}
