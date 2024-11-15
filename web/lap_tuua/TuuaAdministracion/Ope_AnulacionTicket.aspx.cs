using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using System.Collections.Generic;

public partial class Ope_AnulacionTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Operacion objBOOperacion = new BO_Operacion();
    private BO_Administracion objWBAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();

    string sMaxGrilla;

    //Filtros
    string sNumeroTicket;
    string sTicketDesde;
    string sTicketHasta;
    string sTipoTicket;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMensajeError.Text = "";
        lblErrorMsg.Text = "";


        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                btnAnular.Text = htLabels["anulacionticket.btnAnular"].ToString();
                lblDesde.Text = htLabels["anulacionticket.lblDesde"].ToString();
                lblHasta.Text = htLabels["anulacionticket.lblHasta"].ToString();
                lblTotalIngresados.Text = htLabels["anulacionticket.lblTotalIngresados"].ToString();
                this.lblTotalSeleccionados.Text = htLabels["anulacionticket.lblTotalSeleccionados"].ToString();
                this.lblTipoTicket.Text = htLabels["anulacionticket.lblTipoTicket"].ToString();
                this.rbtnNumTicket.Text = htLabels["anulacionticket.rbtnNumTicket"].ToString();
                this.rbtnRangoTicket.Text = htLabels["anulacionticket.rbtnRangoTicket"].ToString();
                this.cbeAnular.ConfirmText = htLabels["anulacionticket.cbeAnular"].ToString();
                this.lblMotivo.Text = htLabels["anulacionticket.lblMotivo"].ToString();
                txtMotivo.Enabled = false;
                rbtnNumTicket.Checked = true;
                hdNumSelTotal.Value = "0";
                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.gwvAnularTicket.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
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
                    Response.Redirect("PaginaError.aspx");

            }

            GrillaDefault();
            CargarCombos();
            ValidarRadioButton();
            SetRadioButtonEstadoTurnoIncicio();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }

    private void SetRadioButtonEstadoTurnoIncicio()
    {
        bool boActivo = FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], "004E0358");
        rbCerrado.Enabled = boActivo;
        rbSinTurno.Enabled = boActivo;
        Session["EstadoTurno"] = boActivo ? "1" : "0";
        SetRadioButtonEstadoTurno();
    }

    private void SetRadioButtonEstadoTurno()
    {
        string strEstadoTurno = (String)Session["EstadoTurno"];
        switch (strEstadoTurno)
        {
            case "0":
                rbAbierto.Checked = true;
                rbCerrado.Checked = false;
                rbSinTurno.Checked=false;
                break;
            case "1":
                rbAbierto.Checked = false;
                rbCerrado.Checked = true;
                rbSinTurno.Checked = false;
                break;
            case "2":
                rbAbierto.Checked = false;
                rbCerrado.Checked = false;
                rbSinTurno.Checked = true;
                break;
            default: 
                break;
        }
    }

    private void GetSelectedEstadoTurno()
    {
        if (rbAbierto.Checked)
        {
            Session["EstadoTurno"] = "0";
        }
        else if (rbCerrado.Checked)
        {
            Session["EstadoTurno"] = "1";
        }
        else
        {
            Session["EstadoTurno"] = "2";
        }
    }

    protected void GrillaDefault()
    {
        DataTable dtTicketAnulados = new DataTable();
        dtTicketAnulados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
        dtTicketAnulados.Columns.Add("Dsc_Tipo_Ticket", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Dsc_Num_Vuelo", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Dsc_Campo", System.Type.GetType("System.String"));
        dtTicketAnulados.Rows.Add(dtTicketAnulados.NewRow());
        this.gwvAnularTicket.DataSource = dtTicketAnulados;
        gwvAnularTicket.DataBind();
        gwvAnularTicket.Rows[0].Cells[0].Text = "";
        gwvAnularTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;
       // gwvAnularTicket.Rows[0].FindControl("txtMotivo").Visible = false;
        lblTxtIngresados.Text = "0";
    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo Ticket
            DataTable dt_tipoTicket = new DataTable();
            dt_tipoTicket = objWBAdministracion.ListaTipoTicket();
            objCargaCombo.LlenarCombo(this.ddlTipoTicket, dt_tipoTicket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket_Larga", true, false);
        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }

    protected void ValidarRadioButton()
    {
        if (rbtnNumTicket.Checked)
        {
            txtNumTicket.Enabled = true;
            txtDesde.Enabled = false;
            txtHasta.Enabled = false;
            txtNumTicket.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txtDesde.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            txtHasta.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
        }

        if (rbtnRangoTicket.Checked)
        {
            txtNumTicket.Enabled = false;
            txtDesde.Enabled = true;
            txtHasta.Enabled = true;
            txtNumTicket.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCCCCC");
            txtDesde.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            txtHasta.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
        }

    }

    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        GetSelectedEstadoTurno();
        if (validaRangoTicket() == true)
        {
            ViewState["dtSeleccionados"] = null; //para eliminar los datos anteriores
            gwvAnularTicket.Sort("Cod_Numero_Ticket", SortDirection.Ascending);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "<script language=\"javascript\">SetNumTotal();</script>", false);
            //hdNumSelTotal.Value = "0";
            SaveFiltros();
            BindPagingGrid();

            #region Creando la tabla para guardar los checkbox seleccionados
            DataTable dtTicketSeleccionados = new DataTable();
            dtTicketSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtTicketSeleccionados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Dsc_Tipo_Ticket", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Dsc_Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Dsc_Campo", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = dtTicketSeleccionados;
            #endregion
        }
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sNumeroTicket", txtNumTicket.Text.Trim()));
        filterList.Add(new Filtros("sTipoTicket", ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sTicketDesde", txtDesde.Text.Trim()));
        filterList.Add(new Filtros("sTicketHasta", txtHasta.Text.Trim()));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sNumeroTicket = newFilterList[0].Valor;
        sTipoTicket = newFilterList[1].Valor;
        sTicketDesde = newFilterList[2].Valor;
        sTicketHasta = newFilterList[3].Valor;
    }

    protected bool validaRangoTicket()
    {
        if (this.rbtnRangoTicket.Checked == true)
        {
            if (!(Convert.ToInt64(txtDesde.Text) <= Convert.ToInt64(txtHasta.Text)))
            {
                lblErrorMsg.Text = "Error en Rango de Tickets";
                ValidarRadioButton();
                return false;
            }
        }
        return true;
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objBOConsultas.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    #region Dynamic data query
    private void BindPagingGrid()
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        gwvAnularTicket.VirtualItemCount = GetRowCount();

        DataTable dt_consulta = new DataTable();

        if (gwvAnularTicket.OrderBy.Contains("Check"))
            //dt_consulta = GetDataPage(gwvAnularTicket.PageIndex, gwvAnularTicket.PageSize, null);
            dt_consulta = GetDataPageCheck(gwvAnularTicket.PageIndex, gwvAnularTicket.PageSize, gwvAnularTicket.SortDirection).Table;
        else
            dt_consulta = GetDataPage(gwvAnularTicket.PageIndex, gwvAnularTicket.PageSize, gwvAnularTicket.OrderBy);

        htLabels = LabelConfig.htLabels;
        ValidarRadioButton();
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
               
                //this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                this.lblTxtIngresados.Text = "";
                this.lblTxtSeleccionados.Text = "";
                //this.txtNumTicket.Enabled = false;
                txtMotivo.Text = "";
                this.txtMotivo.Enabled = false;
                this.btnAnular.Enabled = false;
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

            DataTable dtTicketAnulados = new DataTable();
            dtTicketAnulados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtTicketAnulados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Dsc_Tipo_Ticket", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Dsc_Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Dsc_Campo", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = null;
            #region Agregando fila vacia a la grilla por default
            dtTicketAnulados.Rows.Add(dtTicketAnulados.NewRow());

            gwvAnularTicket.PageIndex = 0;//Se sobreentiende
            gwvAnularTicket.DataSource = dtTicketAnulados;
            gwvAnularTicket.DataBind();
            gwvAnularTicket.Rows[0].Cells[0].Text = "";
            //gwvAnularTicket.Rows[0].FindControl("txtMotivo").Visible = false;
            gwvAnularTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

            lblTxtIngresados.Text = "0";
            lblTxtSeleccionados.Text = "0";
            #endregion

            lblErrorMsg.Text = "No se encontro resultado alguno.";
        }
        else
        {
            this.txtMotivo.Enabled = true;
            this.btnAnular.Enabled = true;
            htLabels = LabelConfig.htLabels;
            gwvAnularTicket.DataSource = dt_consulta;
            gwvAnularTicket.PageSize = Convert.ToInt32(this.sMaxGrilla);
            gwvAnularTicket.DataBind();
            this.lblMensajeError.Text = "";
        }


    }

    private int GetRowCount()
    {
        int count = 0;
        try
        {
            string strEstadoTurno = (String)Session["EstadoTurno"];
            DataTable dt_consulta = new DataTable();

            if (rbtnNumTicket.Checked == true)
                dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope(sNumeroTicket,
                                                                        "",
                                                                        "",
                                                                        sTipoTicket,
                                                                        strEstadoTurno,
                                                                        null,
                                                                        0, 0, "0", "1");
            else
            {
                if (this.rbtnRangoTicket.Checked == true)
                {
                    dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope("",
                                                                            sTicketDesde,
                                                                            sTicketHasta,
                                                                            sTipoTicket,
                                                                            strEstadoTurno,
                                                                            null,
                                                                            0, 0, "0", "1");
                }
            }

            if (dt_consulta.Columns.Contains("TotRows"))
            {
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
                lblTxtIngresados.Text = count.ToString();
                lblTxtSeleccionados.Text = "0";
            }
            else
                lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();


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
        DataTable dt_consulta = new DataTable();
        string strEstadoTurno = (String)Session["EstadoTurno"];
        try
        {
            if (rbtnNumTicket.Checked == true)
                dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope(sNumeroTicket, "", "",
                                                                    sTipoTicket,
                                                                    strEstadoTurno,
                                                                    sortExpression,
                                                                    pageIndex,
                                                                    pageSize, "1", "0");
            else
            {
                if (this.rbtnRangoTicket.Checked == true)
                {
                    dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope("", sTicketDesde,
                                                                            sTicketHasta,
                                                                            sTipoTicket,
                                                                            strEstadoTurno,
                                                                            sortExpression,
                                                                            pageIndex,
                                                                            pageSize, "1", "0");
                }
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
        return dt_consulta;
    }
    #endregion


    protected void gwvAnularTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ShowTicket"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            GridViewRow row = gwvAnularTicket.Rows[rowIndex];
            LinkButton addButton = (LinkButton)row.Cells[1].FindControl("codTicket");
            ConsDetTicket.Inicio(addButton.Text + "-" + addButton.Text);
            //GoPageIndex();
        }
    }

    protected void gwvAnularTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);
        gwvAnularTicket.PageIndex = e.NewPageIndex;
        BindPagingGrid();
        lblTxtSeleccionados.Text = hdNumSelTotal.Value;
    }

    protected void gwvAnularTicket_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (ViewState["dtSeleccionados"] != null)
        {
            guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);

            if (e.SortExpression == "Check")
                BindPagingGridCheck(e.SortDirection.ToString(), e.SortExpression);
            else
                BindPagingGrid();

            lblTxtSeleccionados.Text = hdNumSelTotal.Value;
        }
        else
            e.Cancel = true;
    }


    #region Dynamic data query (check)
    private void BindPagingGridCheck(string sortDirection, string sortExpression)
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        gwvAnularTicket.VirtualItemCount = GetRowCount();

        DataView dv_consulta = GetDataPageCheck(gwvAnularTicket.PageIndex, gwvAnularTicket.PageSize, gwvAnularTicket.SortDirection);

        htLabels = LabelConfig.htLabels;
        if (dv_consulta.Table.Rows.Count < 1)
        {
            try
            {
                //this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                this.lblTxtIngresados.Text = "";
                this.lblTxtSeleccionados.Text = "";
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

            DataTable dtTicketAnulados = new DataTable();
            dtTicketAnulados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtTicketAnulados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtTicketAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = null;
            #region Agregando fila vacia a la grilla por default
            dtTicketAnulados.Rows.Add(dtTicketAnulados.NewRow());

            gwvAnularTicket.PageIndex = 0;//Se sobreentiende
            gwvAnularTicket.DataSource = dtTicketAnulados;
            gwvAnularTicket.DataBind();
            gwvAnularTicket.Rows[0].Cells[0].Text = "";
            //gwvAnularTicket.Rows[0].FindControl("txtMotivo").Visible = false;
            gwvAnularTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

            lblTxtIngresados.Text = "0";
            lblTxtSeleccionados.Text = "0";
            #endregion

            lblErrorMsg.Text = "No se encontro resultado alguno.";
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            gwvAnularTicket.DataSource = dv_consulta.Table;
            gwvAnularTicket.PageSize = Convert.ToInt32(this.sMaxGrilla);
            gwvAnularTicket.DataBind();
            this.lblMensajeError.Text = "";
        }
    }

    private DataView GetDataPageCheck(int pageIndex, int pageSize, SortDirection sortDirection)
    {
        DataTable dt_consulta = new DataTable();
        DataTable dtSeleccionados = new DataTable();
        string strEstadoTurno = (String)Session["EstadoTurno"];

        if ((DataTable)ViewState["dtSeleccionados"] != null)
            dtSeleccionados = ((DataTable)ViewState["dtSeleccionados"]).Copy();

        bool datos = false;
        try
        {
            string sTicketSel = "";
            //concatenamos los tickets seleccionados
            foreach (DataRow row in dtSeleccionados.Rows)
            {
                sTicketSel += row["Cod_Numero_Ticket"] + "|";
            }

            if (sTicketSel != "")
                sTicketSel = sTicketSel.Substring(0, sTicketSel.Length - 1);
            else
                sTicketSel = "00";

            if (rbtnNumTicket.Checked == true)
            {
                dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope(sNumeroTicket, "", "",
                                                                    sTipoTicket,
                                                                    strEstadoTurno,
                                                                    "",
                                                                    pageIndex,
                                                                    pageSize, "1", "0");
                dtSeleccionados = dt_consulta;
                datos = true;
            }
            else
            {
                //LIMITES DE CONSULTA
                int _pageIndex = pageIndex;
                int _pageSize = pageSize;

                if (dtSeleccionados.Rows.Count - (pageSize * pageIndex) > 0)
                {
                    pageSize = (dtSeleccionados.Rows.Count - (pageSize * (pageIndex + 1))) * -1;
                    pageIndex = 1;
                }
                else
                {
                    pageIndex = (dtSeleccionados.Rows.Count - (pageSize * pageIndex)) * -1 + 1;
                    pageSize = pageIndex + pageSize - 1;
                }

                //Si no existe registros con check en la pagina
                if (dtSeleccionados.Rows.Count - (_pageSize * _pageIndex) < 0)
                {
                    dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope(strEstadoTurno, sTicketDesde,
                                                                            sTicketHasta,
                                                                            sTipoTicket,
                                                                            sTicketSel,
                                                                            "",
                                                                            pageIndex,
                                                                            pageSize, "1", "0");

                    dtSeleccionados = dt_consulta;
                    datos = true;
                }
                else
                {

                    if (dtSeleccionados.Rows.Count - (_pageSize * _pageIndex) < _pageSize)
                    {
                        //combinamos viewstate + consulta
                        dt_consulta = objBOConsultas.ConsultaDetalleTicket_Ope(strEstadoTurno, sTicketDesde,
                                                                            sTicketHasta,
                                                                            sTipoTicket,
                                                                            sTicketSel,
                                                                            "",
                                                                            pageIndex,
                                                                            pageSize, "1", "0");


                        int nroRegistros = dtSeleccionados.Rows.Count;
                        int inicio = (_pageSize * _pageIndex) + 1;
                        int fin = nroRegistros;

                        DataRow[] rowsPage = dtSeleccionados.Select("Numero >= " + inicio + " AND Numero<= " + fin + " ");

                        DataTable table = new DataTable();
                        table.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                        table.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                        table.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        table.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                        foreach (DataRow row in rowsPage)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = row["Numero"];
                            newRow["Cod_Numero_Ticket"] = row["Cod_Numero_Ticket"];
                            newRow["Motivo"] = row["Motivo"];
                            newRow["Check"] = row["Check"];
                            table.Rows.Add(newRow);
                        }

                        int nroFilas = table.Rows.Count;
                        foreach (DataRow row in dt_consulta.Rows)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = nroFilas;
                            newRow["Cod_Numero_Ticket"] = row["Cod_Numero_Ticket"];
                            newRow["Motivo"] = "";
                            newRow["Check"] = false;
                            table.Rows.Add(newRow);
                            nroFilas++;
                        }

                        dtSeleccionados = table;
                    }
                    else
                    {
                        //solo el viewstate 
                        int inicio = _pageIndex;
                        int fin = _pageSize;
                        DataRow[] rowsPage = dtSeleccionados.Select("Numero >= " + ((_pageIndex * _pageSize) + 1) + " AND Numero<= " + ((_pageIndex * _pageSize) + _pageSize) + " ");

                        DataTable table = new DataTable();
                        table.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                        table.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                        table.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        table.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                        foreach (DataRow row in rowsPage)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = row["Numero"];
                            newRow["Cod_Numero_Ticket"] = row["Cod_Numero_Ticket"];
                            newRow["Motivo"] = row["Motivo"];
                            newRow["Check"] = row["Check"];
                            table.Rows.Add(newRow);
                        }

                        dtSeleccionados = table;
                    }

                }
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

        DataView dv = dtSeleccionados.DefaultView;
        if (!datos)
            if (sortDirection == SortDirection.Ascending)
                dv.Sort = "Check DESC";
            else
                dv.Sort = "Check ASC";

        return dv;
    }
    #endregion

    protected void guardarSeleccionesCheckBox(DataTable dt_registros)
    {
        int pageSize = gwvAnularTicket.Rows.Count;

        //ELIMINAMOS SI EXISTEN REGISTROS CON CHECK=FALSE
        //SOLO CUANDO SE HIZO ORDENACION POR SELECCIONADO
        if (dt_registros != null)
        {
            DataRow[] foundRowCheck = dt_registros.Select("Check = false");
            foreach (DataRow row in foundRowCheck)
            {
                row.Delete();
            }

            for (int i = 0; i < pageSize; i++)
            {
                //TextBox txtMotivo = (TextBox)gwvAnularTicket.Rows[i].FindControl("txtMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvAnularTicket.Rows[i].FindControl("chkSeleccionar");
                LinkButton lbCodTicket = (LinkButton)gwvAnularTicket.Rows[i].FindControl("codTicket");

                if (chkSeleccionar.Checked)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE EL REGISTRO
                    int nroFilas = dt_registros.Rows.Count;
                    DataRow[] rows = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "");
                    int filas = rows.Count();
                    if (filas == 0)
                    {
                        //no existe el registro y lo agregamos
                        dt_registros.Rows.Add(dt_registros.NewRow());
                        dt_registros.Rows[nroFilas]["Numero"] = (nroFilas + 1).ToString();
                        dt_registros.Rows[nroFilas]["Cod_Numero_Ticket"] = lbCodTicket.Text;
                        dt_registros.Rows[nroFilas]["Motivo"] = this.txtMotivo.Text;
                        dt_registros.Rows[nroFilas]["Check"] = chkSeleccionar.Checked;
                    }
                    else
                    { //Ya existe el registro y actualizamos la informacion
                        if (filas == 1)
                        {
                            rows[0]["Motivo"] = this.txtMotivo.Text;
                                
                        }
                    }
                }
                else
                {
                    //BUSCAMOS SI EL REGISTRO SE ENCUENTRA EN LA TABLA DE SELECCIONADOS PARA ELIMINARLO
                    if (lbCodTicket.Text != "")
                    {
                        int filas = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "").Count();
                        if (filas == 1)
                        {
                            DataRow[] foundRow = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "");

                            int orden = Convert.ToInt32(foundRow[0]["Numero"]);
                            dt_registros.Rows.Remove(foundRow[0]);

                            //actualizamos indices (Numero)
                            DataRow[] rowsOver = dt_registros.Select("Numero >= " + orden + "");
                            foreach (DataRow row in rowsOver)
                            {
                                row["Numero"] = orden;
                                orden++;
                            }

                        }
                    }
                }
            }

            dt_registros.AcceptChanges();

            ViewState["dtSeleccionados"] = dt_registros;
            //return dt_registros;
        }
    }

    #region Metodo Generales para el Sort
    //Method that sorts data
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}", "LastName", "ASC");
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }


    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }
    #endregion

    protected void btnAnular_Click(object sender, EventArgs e)
    {
        if (this.txtMotivo.Text.Length == 0)
        {
            lblMensajeError.Text = "Campo Motivo obligatorio";
        }
        else 
        {
            objBOOperacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

            ValidarRadioButton();

            guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);

            if (ViewState["dtSeleccionados"] == null)
            {
                lblMensajeError.Text = "Debe de seleccionar al menos un ticket para anular.";
            }
            else
            {
                int maxAnulaciones = 0;
                DataTable dt_parametro = objBOConsultas.ListarParametros("MA");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxAnulaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxAnulaciones = 800;
                }

                DataTable dtTicketAnulados = (DataTable)ViewState["dtSeleccionados"];

                int nroSeleccionados = dtTicketAnulados.Rows.Count; //dtTicketAnulados.Select("Check=true").Count();
                if (nroSeleccionados > maxAnulaciones)
                {
                    System.Threading.Thread.Sleep(500);
                    this.lblMensajeError.Text = "Sobrepaso el maximo de Anulaciones (" + maxAnulaciones + ")";
                    return;
                }

                List<TicketEstHist> objListTicketEstHist = new List<TicketEstHist>();
                TicketEstHist objTicketEstHist = new TicketEstHist();

                for (int i = 0; i < dtTicketAnulados.Rows.Count; i++)
                {
                    //bool checkedRehabilitar = (Boolean)dtTicketAnulados.Rows[i]["Check"];
                    //if (checkedRehabilitar)
                    //{
                    objTicketEstHist = new TicketEstHist();
                    objTicketEstHist.SCodNumeroTicket = dtTicketAnulados.Rows[i]["Cod_Numero_Ticket"].ToString();
                    objTicketEstHist.SDscMotivo = dtTicketAnulados.Rows[i]["Motivo"].ToString();
                    objTicketEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);
                    objListTicketEstHist.Add(objTicketEstHist);
                    //}
                }

                if (objListTicketEstHist.Count > 0)
                {
                    bool ret = objBOOperacion.AnularTicket(objListTicketEstHist);
                    if (!ret)
                    {
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000027", "004", IpClient, "3", "Alerta W0000027", "Error en anulacion de Ticket, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                        System.Threading.Thread.Sleep(500);
                        this.lblMensajeError.Text = "Ocurrio un error en el proceso de Anulación";
                        return;
                    }
                    else
                    {
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000028", "004", IpClient, "1", "Alerta W0000028", "Se han anulado correctamente Tickets, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                        #region Realizamos la consulta nuevamente
                        ViewState["dtSeleccionados"] = null; //para eliminar los datos anteriores

                        gwvAnularTicket.Sort("Cod_Numero_Ticket", SortDirection.Ascending);
                        #region Creando la tabla para guardar los checkbox seleccionados
                        DataTable dtTickSeleccionados = new DataTable();
                        //dtTickSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                        //dtTickSeleccionados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                        //dtTickSeleccionados.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        //dtTickSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                        dtTickSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                        dtTickSeleccionados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Dsc_Tipo_Ticket", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Dsc_Num_Vuelo", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Dsc_Campo", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        dtTickSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));


                        ViewState["dtSeleccionados"] = dtTickSeleccionados;
                        #endregion
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "<script language=\"javascript\">SetNumTotal();</script>", false);
                        BindPagingGrid();
                        #endregion

                        lblErrorMsg.Text = "";
                        hdNumSelTotal.Value = "0";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Prueba", "reinicioSeleccion();", true);

                        omb.ShowMessage("Tickets se Anularon correctamente", "Anulación de Tickets", "");

                        if (gwvAnularTicket.VirtualItemCount == 0) {
                            gwvAnularTicket.VirtualItemCount = 1;
                            GrillaDefault();
                            SetRadioButtonEstadoTurnoIncicio();
                        }
                        txtMotivo.Text = "";
                       // GrillaDefault();
                        //Response.Redirect("Ope_AnulacionTicket.aspx");
                    }
                }
                else
                {
                    lblMensajeError.Text = "Debe de seleccionar al menos un ticket para anular.";
                }
            }
        }
        
    }
    protected void gwvAnularTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //BUSCAMOS EN LA TABLA SELECCIONADOS PARA VER SU ESTADO
            DataTable dt_seleccionados = (DataTable)ViewState["dtSeleccionados"];

            if ((dt_seleccionados != null) && (dt_seleccionados.Rows.Count > 0))
            {
                DataRow[] rows = dt_seleccionados.Select("Cod_Numero_Ticket = " + ((System.Data.DataRowView)(e.Row.DataItem)).Row["Cod_Numero_Ticket"].ToString() + "");
                int nroFilas = rows.Count();
                if (nroFilas == 1)
                {
                    CheckBox chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = Convert.ToBoolean(rows[0].ItemArray.GetValue(8));
                    //TextBox txtMotivo = (TextBox)e.Row.FindControl("txtMotivo");
                    //txtMotivo.Text = rows[0].ItemArray.GetValue(2).ToString();
                }
            }
        }
    }

    public bool FlagPerfilUsuarioOpcion(DataTable dtMapSite,string strCodModProceso)
    {
        foreach (DataRow drMapSite in dtMapSite.Rows)
        {
            if (Convert.ToString(drMapSite["id"]) == strCodModProceso)
            {
                return true;
            }
        }
        return false;
    }
}
