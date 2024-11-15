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
using Define = LAP.TUUA.UTIL.Define;

public partial class Rpt_MovimientoTicketContingencia : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    string sTipoTicket;
    string sEstadoTicket;
    string sRangoInicial;
    string sRangoFinal;
    string sFechaDesde;
    string sFechaHasta;
    string sEstado;
        
    BO_Reportes objListarTicketContingenciaxFecha = new BO_Reportes();
    BO_Consultas objParametro = new BO_Consultas();
    
    Int32 valorMaxGrilla;

    DataTable dt_consulta = new DataTable();
    DataTable dt_parametroTurno = new DataTable();

    UIControles objControles = new UIControles();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblFechaDesde.Text = htLabels["reporteMovimientoTicketContingencia.lblFechaDesde.Text"].ToString();
                this.lblFechaHasta.Text = htLabels["reporteMovimientoTicketContingencia.lblFechaHasta.Text"].ToString();
                this.lblTipoTicket.Text = htLabels["reporteMovimientoTicketContingencia.lblTipoTicket.Text"].ToString();
                this.btnConsultar.Text = htLabels["reporteMovimientoTicketContingencia.btnConsultar.Text"].ToString();
                this.lblEstadoTicket.Text = htLabels["reporteMovimientoTicketContingencia.lblEstadoTicket.Text"].ToString();
                this.lblTicketDesde.Text = htLabels["reporteMovimientoTicketContingencia.lblTicketDesde.Text"].ToString();
                this.lblTicketHasta.Text = htLabels["reporteMovimientoTicketContingencia.lblTicketHasta.Text"].ToString();
                this.lblFecha.Text = htLabels["reporteMovimientoTicketContingencia.lblFecha.Text"].ToString();
                this.lblRangoTicket.Text = htLabels["reporteMovimientoTicketContingencia.lblRangoTicket.Text"].ToString();
                this.lblEstTckPerido.Text = htLabels["reporteMovimientoTicketContingencia.lblEstTckPerido.Text"].ToString();
                this.txtFechaDesde.Text = DateTime.Now.ToShortDateString();
                this.txtFechaHasta.Text = DateTime.Now.ToShortDateString();
                Session["ConsultaTicketConti"] = null; //En impresion
                Session["tablaTicket"] = null; //Ordenacion y Paginacion en Grilla
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
            // carga combo Estado ticket
            BO_Consultas objListaCampos = new BO_Consultas();
            DataTable dt_estado = new DataTable();
            dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");

            //Agregamos a la tabla estado el tipo VENCIDO
            DataRow newEstado = dt_estado.NewRow();

            newEstado["Cod_Campo"] = "1";
            newEstado["Dsc_Campo"] = "VENCIDO";

            dt_estado.Rows.Add(newEstado);			
			
            UIControles objCargaComboEstTicket = new UIControles();
            objCargaComboEstTicket.LlenarCombo(ddlEstadoTicket, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);


            // carga combo Tipo ticket
            BO_Administracion objListaTipoTicket = new BO_Administracion();
            DataTable dt_tipoticket = new DataTable();
            dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
            UIControles objCargaComboTipoTicket = new UIControles();
            objCargaComboTipoTicket.LlenarCombo(ddlTipoTicket, dt_tipoticket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true, false);

            //carga de combo Estado Ticket para el periodo de consulta
            //objCargaComboEstTicket.LlenarCombo(ddlEstTckPerido, dt_estado, "Cod_Campo", "Dsc_Campo", false);
            DataTable dtEstTicket = dt_estado;
            dtEstTicket.Rows.RemoveAt(dtEstTicket.Rows.Count - 1);
            ddlEstTckPerido.DataSource = dtEstTicket;
            ddlEstTckPerido.DataTextField = "Dsc_Campo";
            ddlEstTckPerido.DataValueField = "Cod_Campo";
            ddlEstTckPerido.SelectedIndex = 0;
            ddlEstTckPerido.DataBind();
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

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;

        if (!EsValidoFormulario())
        {
            this.lblMensajeError.Text = "";
            grvMovimientoTicketCont.DataSource = null;
            grvMovimientoTicketCont.DataBind();
            grvResumen.DataSource = null;
            grvResumen.DataBind();
        }
        else {
            SaveFiltros();
            ViewState["Ordenamiento"] = null;
            CargarGrillaTicketContigencia();
        }
    }

    protected bool EsValidoFormulario()
    {
        if ((txtRangoDesde.Text.Trim() == "" && txtRangoHasta.Text.Trim() != "") || 
            (txtRangoDesde.Text.Trim() != "" && txtRangoHasta.Text.Trim() == ""))
        {
            this.lblMensajeErrorData.Text = htLabels["reporteMovimientoTicketContingencia.lblMensajeError1.Text"].ToString();
            return false;
        }
        
        int result = DateTime.Compare(Convert.ToDateTime(this.txtFechaDesde.Text), Convert.ToDateTime(this.txtFechaHasta.Text));
        if (result == 1)
        {
            this.lblMensajeErrorData.Text = htLabels["reporteMovimientoTicketContingencia.lblMensajeError2.Text"].ToString();
            return false;
        }        
        return true; //success
    }

    protected void SaveFiltros()
    {
        sFechaDesde = Fecha.convertToFechaSQL2(txtFechaDesde.Text);
        sFechaHasta = Fecha.convertToFechaSQL2(txtFechaHasta.Text);
        sTipoTicket = ddlTipoTicket.SelectedValue;
        sEstadoTicket = ddlEstadoTicket.SelectedValue;
        sEstado = ddlEstTckPerido.SelectedValue;
        sRangoInicial = txtRangoDesde.Text;
        sRangoFinal = txtRangoHasta.Text;

        if (sRangoInicial == "")    sRangoInicial = "0";
        if (sRangoFinal == "")  sRangoFinal = "0";
    }

    protected void CargarGrillaTicketContigencia()
    {
        try
        {
            dt_consulta = objListarTicketContingenciaxFecha.obtenerMovimientoTicketContingencia(sFechaDesde, sFechaHasta, sEstado, sTipoTicket, sEstadoTicket, sRangoInicial, sRangoFinal);
            
            if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
            {
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
                    this.lblMensajeError.Text = "";
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
                grvMovimientoTicketCont.DataSource = null;
                grvMovimientoTicketCont.DataBind();
                grvResumen.DataSource = null;
                grvResumen.DataBind();
            }
            else
            {
                /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                    this.lblMensajeError.Text = "";
                    grvMovimientoTicketCont.DataSource = null;
                    grvMovimientoTicketCont.DataBind();
                    grvResumen.DataSource = null;
                    grvResumen.DataBind();
                }
                else
                {
                    //Aplicar Filtros de Rangos de Ticket
                    if (!(sRangoInicial == "0" && sRangoFinal == "0"))
                    {
                        dt_consulta = EvaluarRangos(dt_consulta);
                    }

                    if (dt_consulta == null)
                    {
                        this.lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
                        this.lblMensajeError.Text = "";
                        grvMovimientoTicketCont.DataSource = null;
                        grvMovimientoTicketCont.DataBind();
                        grvResumen.DataSource = null;
                        grvResumen.DataBind();
                    }
                    else
                    {
                        Session["tablaTicket"] = dt_consulta;
                        ValidarTamanoGrilla();

                        //Cargar datos en la grilla
                        this.lblMensajeErrorData.Text = "";
                        this.lblMensajeError.Text = "";
                        this.grvMovimientoTicketCont.DataSource = dt_consulta;
                        this.grvMovimientoTicketCont.AllowPaging = true;
                        this.grvMovimientoTicketCont.PageSize = valorMaxGrilla;
                        this.grvMovimientoTicketCont.DataBind();

                        CargarDataResumen();
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

    public DataTable EvaluarRangos(DataTable dt_consulta)
    {
        var query2 = from dtRpt in dt_consulta.AsEnumerable()
                     where (dtRpt.Field<String>("Cod_Numero_Ticket").CompareTo(sRangoInicial) >= 0 && dtRpt.Field<String>("Cod_Numero_Ticket").CompareTo(sRangoFinal) <= 0)
                     select dtRpt;

        //Create a table from the query.
        if (query2.Count() > 0)
        {
            dt_consulta = query2.CopyToDataTable();
        }
        else
        {
            dt_consulta = null;
        }

        return dt_consulta;
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

    void CargarDataResumen()
    {
        BO_Consultas objListaCampos = new BO_Consultas();
        BO_Administracion objListaTipoTicket = new BO_Administracion(); 
        
        DataTable dtResumen = new DataTable();

        //Cargar Estados
        DataTable dt_estado = new DataTable();
        dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");

        //Agregamos a la tabla estado el tipo VENCIDO
        DataRow newEstado = dt_estado.NewRow();
        newEstado["Cod_Campo"] = "V";
        newEstado["Dsc_Campo"] = "VENCIDO";
        dt_estado.Rows.Add(newEstado);

        //Cargar Tipo ticket
        DataTable dt_tipoticket = new DataTable();
        dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
        
        Hashtable totTipoEstado = new Hashtable();

        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            decimal totMontoTipo = 0.0M;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];
            
            foreach (DataRow dtRowEstado in dt_estado.Rows)
            {
                decimal totMonto = 0.00M;
                int total = 0;

                String codEstadoTicket = (String)dtRowEstado["Cod_Campo"];
                
                DataRow[] dtfiltro = dt_consulta.Select("Cod_Tipo_Ticket = '" + codTipoTicket + "' AND Tip_Estado_Actual ='" + codEstadoTicket + "'");
                int tam = dtfiltro.Count();
                if (tam > 0)
                {
                    foreach (DataRow dtrow in dtfiltro)
                    {
                        total++;
                        //totMonto = totMonto + (Decimal)dtrow["Monto_Ticket"];
                    }
                    ArrayList rsultotal = new ArrayList();
                    rsultotal.Add(total);
                    rsultotal.Add(totMonto);
                    totTipoEstado.Add(codTipoTicket + "*" + codEstadoTicket, rsultotal);
                }
            }

            //PARA EL ESTADO FANTASMA DE VENDIDO
            decimal totMontoVenta = 0.00M;
            int total1 = 0;
            
            if (dt_consulta.Rows.Count > 0)
            {
                var lqList = from rowConsulta in dt_consulta.AsEnumerable()
                             where rowConsulta.Field<String>("Cod_Tipo_Ticket") == codTipoTicket && rowConsulta.Field<String>("Cod_Estado_Ticket") == "Z"
                             group rowConsulta by rowConsulta.Field<String>("Cod_Numero_Ticket") into g
                             select new { Category = g.Key, Monto = g.Min(rowConsulta => rowConsulta.Field<Decimal>("Monto_Ticket")) };

                if (lqList.Count() > 0)
                {
                    foreach (var cTicket in lqList)
                    {
                        try
                        {
                            totMontoVenta = totMontoVenta + (Decimal)cTicket.Monto;

                            //kinzi
                            decimal tmp_Amount = 0.00M;
                            tmp_Amount = (Decimal)cTicket.Monto;
                            if (tmp_Amount > 0)
                                total1++;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    ArrayList rsultotal = new ArrayList();
                    rsultotal.Add(total1);
                    rsultotal.Add(totMontoVenta);
                    totMontoTipo = totMontoTipo + totMontoVenta;
                    totTipoEstado.Add(codTipoTicket + "*" + "Z", rsultotal);
                }
            }
            totTipoEstado.Add(codTipoTicket, decimal.Round(totMontoTipo, 2));
        }

        //Crea las columnas Tipo Ticket en la tabla Resumen
        DataColumn dtColumnaTipo = new DataColumn();
        dtColumnaTipo.DataType = System.Type.GetType("System.String");
        dtColumnaTipo.ColumnName = "TIPO TICKET";
        dtResumen.Columns.Add(dtColumnaTipo);
        
        ArrayList lstStadosCod = new ArrayList();
        ArrayList lstStadosNom = new ArrayList();

        bool isbanderaOculta = true;

        DataRow[] ArrRow = dt_estado.Select("", "Dsc_Campo asc");

        //Crea las columnas Dinámicas de Estados en la tabla Resumen
        foreach (DataRow dtRow in ArrRow)
        {
            string nomEstado = (String)dtRow["Dsc_Campo"];
            string codEstado = (String)dtRow["Cod_Campo"];
            bool isbandera = true;

            foreach (DataRow dtRowConsult in dt_consulta.Rows)
            {
                String codEstadoConsult = (String)dtRowConsult["Tip_Estado_Actual"];
                if (codEstado.Equals(codEstadoConsult))
                {
                    if (isbandera)
                    {
                        DataColumn dtColumna = new DataColumn();
                        dtColumna.DataType = System.Type.GetType("System.String");
                        lstStadosCod.Add(codEstado);
                        lstStadosNom.Add(nomEstado);
                        dtColumna.ColumnName = nomEstado;
                        dtResumen.Columns.Add(dtColumna);
                        isbandera = false;
                        break;
                    }
                }
            }
        }

        //Crea la columna Vendidos en la tabla Resumen
        foreach (DataRow dtRowConsult in dt_consulta.Rows)
        {
            String codEstadoConsult = "Z";
            //Para el Estado Fatasma de Ventas
            if (codEstadoConsult.Equals("Z"))
            {
                if (isbanderaOculta)
                {
                    DataColumn dtColumna = new DataColumn();
                    dtColumna.DataType = System.Type.GetType("System.String");
                    lstStadosCod.Add("Z");
                    lstStadosNom.Add("VENDIDO");
                    dtColumna.ColumnName = "VENDIDO";
                    dtResumen.Columns.Add(dtColumna);
                    isbanderaOculta = false;
                    break;
                }
            }
        }

        //Crea la columna Montos en la tabla Resumen
        DataColumn dtColumnaTipoFin = new DataColumn();
        dtColumnaTipoFin.DataType = System.Type.GetType("System.String");
        dtColumnaTipoFin.ColumnName = "MONTO($)";
        dtResumen.Columns.Add(dtColumnaTipoFin);
        
        //Llena los registros en la tabla Resumen
        foreach (DataRow dtRowTipo in dt_tipoticket.Rows)
        {
            bool isVale = false;
            String codTipoTicket = (String)dtRowTipo["Cod_Tipo_Ticket"];
            String nomTipoTicket = (String)dtRowTipo["Dsc_Tipo_Ticket"];
            DataRow nRow;
            nRow = dtResumen.NewRow();
            nRow["TIPO TICKET"] = nomTipoTicket;
            for (int i = 0; i < lstStadosCod.Count; i++)
            {
                String nomCampo = (String)lstStadosNom[i];
                String codCampo = (String)lstStadosCod[i];
                ArrayList lstDatos = new ArrayList();
                lstDatos = (ArrayList)totTipoEstado[codTipoTicket + '*' + codCampo];
                if (lstDatos == null)
                {
                    nRow[nomCampo] = "0";
                }
                else
                {
                    nRow[nomCampo] = Convert.ToString((Int32)lstDatos[0]);
                    isVale = true;
                }
            }
            if (isVale)
            {
                decimal totMontoTipo = (Decimal)totTipoEstado[codTipoTicket];
                nRow["MONTO($)"] = Convert.ToString(totMontoTipo);
                dtResumen.Rows.Add(nRow);
            }
        }
        
        grvResumen.DataSource = dtResumen;
        grvResumen.DataBind();
    }

    protected void grvMovimientoTicketCont_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = grvMovimientoTicketCont.PageSize;
        DataTable dtTicketRehabilitados = (DataTable)Session["tablaTicket"];

        grvMovimientoTicketCont.PageIndex = e.NewPageIndex;
        
        if (ViewState["Ordenamiento"] != null)
        {
            DataView dvResultados = new DataView(dtTicketRehabilitados);
            dvResultados.Sort = (String)ViewState["Ordenamiento"];
            grvMovimientoTicketCont.DataSource = dvResultados;
            grvMovimientoTicketCont.DataBind();
        }
        else
        {
            grvMovimientoTicketCont.DataSource = dtTicketRehabilitados;
            grvMovimientoTicketCont.DataBind();
        }


    }


    protected void grvMovimientoTicketCont_Sorting(object sender, GridViewSortEventArgs e)
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

    private void SortGridView(string sortExpression, String direction)
    {
        try
        {
            if (Session["tablaTicket"] != null)
            {
                this.grvMovimientoTicketCont.DataSource = dwConsulta((DataTable)Session["tablaTicket"], sortExpression, direction);
                this.grvMovimientoTicketCont.DataBind();
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
                Response.Redirect("PaginaError.aspx");

        }
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            //dv.Sort = sortExpression + " " + direction;
            //Soporte Ordenacion con varios campos
            string[] vec = sortExpression.Split(',');
            string sortExpressionMultiple = "";
            for (int i = 0; i <= vec.Length - 1; i++)
            {
                sortExpressionMultiple += vec[i].Trim() + " " + direction;
                if (i + 1 <= vec.Length - 1)
                    sortExpressionMultiple += ",";
            }
            dv.Sort = sortExpressionMultiple;
            ViewState["Ordenamiento"] = sortExpressionMultiple; 
        }       

        return dv;
    }

    protected void grvMovimientoTicketCont_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleTicket = (DataTable)Session["tablaTicket"];
            String codTicket = dtDetalleTicket.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            ConsDetTicket1.Inicio(codTicket);
        }
    }

    #region Change Color of GridView
    protected void grvMovimientoTicketCont_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString().TrimEnd() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString().TrimEnd() == "R")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString().TrimEnd() == "V")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(176, 250, 178);
            }
        }
    }
    #endregion

    protected void grvResumen_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView oGridView = (GridView)sender;

            GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            TableCell th = new TableHeaderCell();

            th.HorizontalAlign = HorizontalAlign.Center;
            th.ColumnSpan = 1;
            th.BackColor = System.Drawing.Color.SteelBlue;
            th.ForeColor = System.Drawing.Color.White;
            th.Font.Bold = true;
            th.Text = "Resumen:";
            row.Cells.Add(th);

            row.BackColor = System.Drawing.Color.SteelBlue;
            oGridView.Controls[0].Controls.AddAt(0, row);
        }
    }
}
