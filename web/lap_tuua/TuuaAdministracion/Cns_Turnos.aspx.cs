using System;
using System.Windows.Forms;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

public partial class Modulo_Consultas_ConsultaDetalleTurnos : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Consultas objConsultaTurnoxFiltro = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    BO_Consultas objListAllUsuario = new BO_Consultas();
    DataTable dt_usuario = new DataTable();
    public int ValiFechas;

    string sFechaDesde = "";
    string sFechaHasta = "";
    string sHoraDesde = "";
    string sHoraHasta = "";
    string sIdUsuario = "";
    string sIdPtoVta = "";

    UIControles objControles = new UIControles();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            //Session.Contents.Remove("Print_Data_Turno");
            try
            {
                this.lblDesde.Text = htLabels["mconsultaDetalleturno.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaDetalleturno.lblHasta.Text"].ToString();
                this.lblUsuario.Text = htLabels["mconsultaDetalleturno.lblUsuario.Text"].ToString();
                this.lblPtoVenta.Text = htLabels["mconsultaDetalleturno.lblPtoVenta.Text"].ToString();
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
                    Response.Redirect("PaginaError.aspx");
                }
            }
            this.txtDesde.Text = DateTime.Now.ToShortDateString();//.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year;
            this.txtHasta.Text = DateTime.Now.ToShortDateString(); //DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;

            CargarCombos();
            CargarGrillaDinamica();
            lblMensajeErrorData.Text = "";
        }
        Session["Print_Data_Turno"] = null;
    }

    public void CargarCombos()
    {
        try
        {
            //Carga datatable con todos los usuarios
            dt_usuario = objListAllUsuario.ListarAllUsuario();

            //Cargar combo Usuarios
            UIControles objCargaComboUsuario = new UIControles();
            objCargaComboUsuario.LlenarCombo(ddlUsuario, dt_usuario, "Cod_Usuario", "Usuario", true, false);
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
        SaveFiltros();

        if (txtHoraDesde.Text == "__:__:__") { txtHoraDesde.Text = ""; }
        if (txtHoraHasta.Text == "__:__:__") { txtHoraHasta.Text = ""; }

        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;
            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";

            ValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));
        }
        else
        {
            ValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + txtHoraDesde.Text), Convert.ToDateTime(this.txtHasta.Text + " " + txtHoraHasta.Text));
        }

        if (ValiFechas == 1)
        {
            this.lblMensajeError.Text = "Filtro de fecha invalido";
            lblMensajeErrorData.Text = "";
            this.txtHasta.Text = "";
            grvTurno.DataSource = null;
            grvTurno.DataBind();
        }
        else
        {
            if (ValiFechas != 1)
            {
                lblMaxRegistros.Value = GetMaximoExcel().ToString();
                RecuperarFiltros();
                CargarGrillaDinamica();
            }
        }
    }

    protected void CargarGrillaDinamica()
    {
        try
        {
            try
            {
                htLabels = LabelConfig.htLabels;
                dt_consulta = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(sFechaDesde, sFechaHasta, sIdUsuario, sIdPtoVta, sHoraDesde, sHoraHasta, "0");
                //CargarDataTable();
            }
            catch (Exception ex)
            {
                BO_Error objBOError = new BO_Error();
                objBOError.IsError();
                Response.Redirect("PaginaError.aspx");
            }

            if (dt_consulta.Rows.Count == 0)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
                lblTotal.Text = "";
                lblTotalRows.Value = "";
                grvTurno.DataSource = null;
                grvTurno.DataBind();
            }
            else
            {
                CrearGrillaDinamica();

                //Traer valor de tamaño de la grilla desde parametro general
                BO_Consultas objParametro = new BO_Consultas();
                DataTable dt_parametroTurno = new DataTable();
                dt_parametroTurno = objParametro.ListarParametros("LG");
                if (dt_parametroTurno.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
                }
                //Cargar datos en la grilla  
                ViewState["tablaTurno"] = dt_consulta;
                grvTurno.DataSource = dt_consulta;
                grvTurno.AllowPaging = true;
                grvTurno.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                grvTurno.PageIndex = 0;
                grvTurno.DataBind();
                lblMensajeError.Text = "";
                lblMensajeErrorData.Text = "";
                lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dt_consulta.Rows.Count;
                lblTotalRows.Value = dt_consulta.Rows.Count.ToString();
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

    void CrearGrillaDinamica()
    {
        string[,] data = { {"Cod_Turno", "Dsc_Usuario", "", "Fch_Inicio", "Fch_Fin", "Dsc_Estacion"}, 
                           {"Código","Usuario","", "Fecha Apertura", "Fecha Cierre", "Nro. Caja"} };

        grvTurno.Columns.Clear();

        //Hyperlink Column
        HyperLinkField hlfLink = new HyperLinkField();
        hlfLink.DataNavigateUrlFields = new String[] { data[0, 0] };
        hlfLink.DataNavigateUrlFormatString = "Cns_DetalleTurnos.aspx?id={0}";
        hlfLink.ShowHeader = true;
        hlfLink.HeaderText = data[1, 0];
        hlfLink.DataTextField = data[0, 0];
        hlfLink.NavigateUrl = "Cns_DetalleTurnos.aspx";
        hlfLink.SortExpression = data[0, 0];
        DataControlField dcfControl = hlfLink;
        grvTurno.Columns.Add(hlfLink);

        //User Column
        BoundField bf_left = new BoundField();
        bf_left.DataField = data[0, 1];
        bf_left.SortExpression = data[0, 1];
        bf_left.HeaderText = data[1, 1];
        grvTurno.Columns.Add(bf_left);

        //Caja Column
        /*TemplateField bfield = new TemplateField();
        //bfield.HeaderTemplate = new GridViewTemplate(ListItemType.Header, data[1, 5]);        
        bfield.SortExpression = "Dsc_Estacion";
        bfield.ItemTemplate = new  GridViewTemplate(ListItemType.Item, "Dsc_Estacion", "Cod_Equipo");
        bfield.HeaderText = "Nro. Caja";
        grvTurno.Columns.Add(bfield);*/
        bf_left = new BoundField();
        bf_left.DataField = data[0, 5];
        bf_left.SortExpression = data[0, 5];
        bf_left.HeaderText = data[1, 5];
        grvTurno.Columns.Add(bf_left);

        //Fecha Apertura Column
        bf_left = new BoundField();
        bf_left.DataField = data[0, 3];
        bf_left.SortExpression = data[0, 3];
        bf_left.HeaderText = data[1, 3];
        grvTurno.Columns.Add(bf_left);

        //Fecha Cierre Column
        bf_left = new BoundField();
        bf_left.DataField = data[0, 4];
        bf_left.SortExpression = data[0, 4];
        bf_left.HeaderText = data[1, 4];
        bf_left.NullDisplayText = "ABIERTO";
        grvTurno.Columns.Add(bf_left);

        foreach (DataColumn col in dt_consulta.Columns)
        {
            BoundField bf1 = new BoundField();

            switch (col.ColumnName)
            {
                case "Cod_Turno":
                case "Dsc_Usuario":
                case "Cod_Equipo":
                case "Num_Ip_Equipo":
                case "Fch_Inicio":
                case "Fch_Fin":
                case "Dsc_Estacion":
                case "Cta_Usuario":
                    continue;
                default:
                    string sHeader = (col.ColumnName.Contains("_ImporteIni")) ? col.ColumnName.Replace("_ImporteIni", "") : col.ColumnName.Replace("_ImporteFin", "");
                    bf1.HeaderText = sHeader;
                    bf1.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
                    break;
            }
            bf1.DataField = col.ColumnName;
            bf1.SortExpression = col.ColumnName;

            grvTurno.Columns.Add(bf1);
        }
    }

    protected void grvTurno_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            int tam = (grvTurno.Columns.Count - 5) / 2;

            //agrego una columna sin nombre
            TableCell oTableCell = new TableCell();
            oTableCell.ColumnSpan = 5;
            oTableCell.BackColor = System.Drawing.Color.White;
            oGridViewRow.Cells.Add(oTableCell);

            //agrego una columna moneda inicio
            oTableCell = new TableCell();
            oTableCell.Text = "Moneda Inicio";
            oTableCell.Style.Add("font-size", "larger");
            oTableCell.Style.Add("font-family", "Verdana");
            oTableCell.Style.Add("font-weight", "bold");
            oTableCell.Style.Add("color", "#000000");
            oTableCell.ColumnSpan = tam;
            oGridViewRow.Cells.Add(oTableCell);

            //agrego una columna moneda fin
            oTableCell = new TableCell();
            oTableCell.Text = "Moneda Fin";
            oTableCell.Style.Add("font-size", "larger");
            oTableCell.Style.Add("font-family", "Verdana");
            oTableCell.Style.Add("font-weight", "bold");
            oTableCell.Style.Add("color", "#000000");
            oTableCell.ColumnSpan = tam;

            oGridViewRow.Cells.Add(oTableCell);
            oGridView.Controls[0].Controls.AddAt(0, oGridViewRow);
        }
    }

    #region Cargar/Guardas Filtros de Consulta
    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(this.txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(this.txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(this.txtHasta.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(this.txtHasta.Text)));
        filterList.Add(new Filtros("sIdUsuario", ddlUsuario.SelectedValue));
        filterList.Add(new Filtros("sIdPtoVta", txtPtoVta.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde = newFilterList[2].Valor;
        sHoraHasta = newFilterList[3].Valor;
        sIdUsuario = newFilterList[4].Valor;
        sIdPtoVta = newFilterList[5].Valor;
    }
    #endregion

    #region Pagin
    protected void grvTurno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvTurno.DataSource = (DataTable)ViewState["tablaTurno"];
        grvTurno.PageIndex = e.NewPageIndex;
        grvTurno.DataBind();
    }
    #endregion

    #region Sorting
    protected void grvTurno_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");

        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");

        }
        this.txtColumna.Text = sortExpression;
    }
    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            DataView dv = dwConsulta((DataTable)ViewState["tablaTurno"], sortExpression, direction);
            grvTurno.DataSource = dv;
            grvTurno.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvTurno.DataBind();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
                Response.Redirect("PaginaError.aspx");
        }
    }
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["SortDirection"];
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);
        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }
    #endregion

    protected void grvTurno_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetalleTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleCodTurno = (DataTable)ViewState["tablaTurno"];
            string codTurno = dtDetalleCodTurno.Rows[rowIndex]["Código"].ToString();
            //DetalleTurnoPrincipal1.IniciarDetalleTurno(codTurno);
            CargarGrillaDinamica();
        }
    }

    protected void grvTurno_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=Turnos.xls");
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
            dt_consulta = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(sFechaDesde,
                sFechaHasta, sIdUsuario, sIdPtoVta, sHoraDesde, sHoraHasta, "0");
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

        #region Consulta de Turnos
        Excel.Worksheet Consulta = new Excel.Worksheet("Turnos");
        Consulta.Columns = new string[] { "Turno", "Usuario", "Nro. Caja", "Fecha Apertura", "Fecha Cierre", "Moneda Inicio SOL", "Moneda Inicio DOL", "Moneda Fin SOL", "Moneda Fin DOL" };
        Consulta.WidthColumns = new int[] { 60, 170, 160, 120, 120, 70, 70, 70, 70 };
        Consulta.DataFields = new string[] { "Cod_Turno", "Dsc_Usuario", "Dsc_Estacion", "Fch_Inicio", "Fch_Fin", "SOL_ImporteIni", "DOL_ImporteIni", "SOL_ImporteFin", "DOL_ImporteFin" };
        Consulta.Source = dt_consulta;
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { Consulta };

        return Workbook.Save();
    }
}
