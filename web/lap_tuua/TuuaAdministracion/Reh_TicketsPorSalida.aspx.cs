using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
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
using LAP.TUUA.ALARMAS;
using Define=LAP.TUUA.UTIL.Define;
using System.IO;

public partial class Reh_Tickets : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_TemporalTicket objBOTemporalTicket = new BO_TemporalTicket("_TemporalTicket");
    private BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();
    protected Hashtable htParametro;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMsg.Text = "";
        spnRehabilitar.Text = "";
        htParametro = (Hashtable)Session["htParametro"];
        
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                txtNroTicket.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                hdPortSerialLector.Value = (string)Property.htProperty[Define.COM_SERIAL_LECTOR];
                lblMotivo.Text = "Motivo";
                btnRehabilitar.Text = htLabels["rehabilitacionticket.btnRehabilitar"].ToString();
                lblNroTicket.Text = "Fecha de Vuelo";
                lblTotalSeleccionados.Text = htLabels["rehabilitacionticket.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionticket.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionticket.lblConformidad"].ToString();

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    gwvRehabilitarPorTicket.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                //else el default es 10.
                //icano 15-06-10
                DataTable dtActivarExporExcel = objBOConsultas.ListarParametros("EE");
                int activarExporExcel = Int32.Parse(dtActivarExporExcel.Rows[0]["Valor"].ToString());
                if (activarExporExcel != 1)
                {
                    imgExportarExcel.Visible = false;
                    pnlExcel.Visible = false;
                }
                else
                {
                    pnlExcel.Visible = true;
                    imgExportarExcel.Visible = true;
                }
                //fin
                int maxRehabilitaciones = 0;
                dt_parametro = objBOConsultas.ListarParametros("RTM");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxRehabilitaciones = 800;
                }
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }

            DataTable dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Creacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Registro", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
            gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
            gwvRehabilitarPorTicket.DataBind();

            gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
            gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;

            #region Llenando data del combo de causal
            DataTable dtCausalReh = objBOConsultas.ListaCamposxNombre("CausalRehabilitacion");
            cboMotivo.DataSource = dtCausalReh;
            cboMotivo.DataTextField = "Dsc_Campo";
            cboMotivo.DataValueField = "Cod_Campo";
            cboMotivo.DataBind();
            #endregion
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }
    #endregion

    #region btnAgregar_Click

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;
        int pageCount = gwvRehabilitarPorTicket.PageCount;
        string codigoTicket;
        codigoTicket = txtNroTicket.Text;
        txtNroTicket.Text = "";
        if (codigoTicket.Equals(String.Empty))
        {
            lblErrorMsg.Text = "Error. La fecha no puede ser vacío";
            return;
        }
        TemporalTicket objTemporalTicket = new TemporalTicket();
        objTemporalTicket.FchVuelo = Convert.ToDateTime(codigoTicket).ToString("yyyyMMdd");
        DataTable dtTicketDetalle = objBOTemporalTicket.ListarAll(objTemporalTicket);
        if (dtTicketDetalle.Rows.Count == 0)
        {
            lblErrorMsg.Text = "Error. La fecha ingresada no contiene Ticket.";
            return;
        }
        DataTable dtTicketRehabilitados;
        DataRow row;
        if (ViewState["tabla"] != null)
        {
            dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            #region Guardo las selecciones del checkbox
            pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }           
            #endregion
        }
        else
        {
            dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Creacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Registro", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            foreach (DataRow item in dtTicketDetalle.Rows)
            {
                row = dtTicketRehabilitados.NewRow();
                row["Check"] = false;
                row["Cod_Numero_Ticket"] = item["Cod_Numero_Ticket"];
                row["Fch_Creacion"] = item["Fch_Creacion"];
                row["Fch_Registro"] = item["Fch_Registro"];
                row["Num_Vuelo"] = item["Num_Vuelo"];
                dtTicketRehabilitados.Rows.Add(row);
                dtTicketRehabilitados.AcceptChanges();
            }
        }
        ViewState["tabla"] = dtTicketRehabilitados;

        #region Define el PageIndex
        if (dtTicketRehabilitados.Rows.Count == (pageCount * pageSize) + 1)
        {
            gwvRehabilitarPorTicket.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorTicket.PageIndex = pageCount - 1;
        }
        #endregion

        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        #region Actualizar Resumen
        lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) + 1);
        #endregion
    }

    #endregion

    #region gwvRehabilitarPorTicket_RowCommand

    protected void gwvRehabilitarPorTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageCount = gwvRehabilitarPorTicket.PageCount;

            #region Actualizar Resumen
            bool isChecked = ((CheckBox)(gwvRehabilitarPorTicket.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
            #endregion
            dtTicketRehabilitados.Rows.RemoveAt(rowIndex);

            #region Guardo las selecciones del combo y checkbox
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count + 1 - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
            }
            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                if (j != rowIndex - (pageIndex * pageSize))
                {
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                }
                else
                {
                    i--;
                }
            }
            #endregion
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
            }

            #region Define el PageIndex
            if (dtTicketRehabilitados.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
            {
                gwvRehabilitarPorTicket.PageIndex = pageIndex - 1;
            }
            #endregion

            if (dtTicketRehabilitados.Rows.Count == 0)
            {
                ViewState["tabla"] = null;
                #region Agregando fila vacia a la grilla por default
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());

                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                //gwvRehabilitarPorTicket.Rows[0].Cells[5].Controls.Clear();
                gwvRehabilitarPorTicket.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorTicket.Rows[0].FindControl("chkSeleccionar").Visible = false;
                #endregion
            }
            else
            {
                gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorTicket.DataBind();

                #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
                pageIndex = gwvRehabilitarPorTicket.PageIndex;
                pageCount = gwvRehabilitarPorTicket.PageCount;

                if ((pageIndex + 1) < pageCount)
                {
                    limite = pageSize;
                }
                else
                {
                    limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
                }

                for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                {
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                }
                #endregion
            }

            #region Actualizar Resumen
            if (isChecked)
            {
                hdNumSelTotal.Value = "" + (Int32.Parse(hdNumSelTotal.Value) - 1);
                int normales = Int32.Parse(hdNumSelTotal.Value) - Int32.Parse(hdNumSelConObs.Value);
                lblTxtSeleccionados.Text = hdNumSelTotal.Value + " (" + hdNumSelConObs.Value + " Observaciones / " + normales + " Normales)";
            }
            lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) - 1);
            #endregion
        }
        else if (e.CommandName.Equals("ShowTicket"))
        {
            //int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            //DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
            //String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString() + "-" + dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            //ConsDetTicket.Inicio(codTicket);
        }
    }

    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;

        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        #region Guardo las selecciones del combo y checkbox
        pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorTicket.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorTicket.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        pageIndex = gwvRehabilitarPorTicket.PageIndex;

        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //Aqui no hay condicion pues no he agregado ninguna fila.
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion
    }
    #endregion

    #region Colorear las Observaciones negativas - RowDataBound

    protected void gwvRehabilitarPorTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string FechaCreacion = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Fch_Creacion"));
            if (!FechaCreacion.Equals("-"))
            {
                e.Row.Cells[2].ForeColor = Color.Red;
            }
        }

    }
    #endregion

    #region gwvRehabilitarPorTicket_Sorting
    protected void gwvRehabilitarPorTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        if (dtTicketRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox y comboBox
        int pageIndex = gwvRehabilitarPorTicket.PageIndex;
        int pageSize = gwvRehabilitarPorTicket.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tabla"] = SortDataTable(dtTicketRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorTicket.DataSource = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorTicket.DataBind();

        #region Lleno el combo. Actualizo la selecciones del checkBox y combobox.
        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //Aqui no hay condicion pues no he agregado ninguna fila.
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion
    }
    #endregion

    #region Metodo Generales para el Sort
    //Method that sorts data
    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
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

    #region btnRehabilitar_Click
    protected void btnRehabilitar_Click(object sender, EventArgs e)
    {
        if (ViewState["tabla"] == null)
        {
            //No hay filas en la grilla
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RTM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;
            }
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            #region Guardo las selecciones del checkbox y combobox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorTicket.PageSize;
            int pageIndex = gwvRehabilitarPorTicket.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorTicket.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[j].FindControl("chkSeleccionar");
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }
            #endregion

            int nroSeleccionados = dtTicketRehabilitados.Select("Check=true").Count();
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }

            Hashtable htDscVuelo = (Hashtable)ViewState["htDscVuelo"];
            String motivo = cboMotivo.SelectedItem.Value;
            StringBuilder cod_Numero_Tickets = new StringBuilder();
            StringBuilder dsc_Num_Vuelos = new StringBuilder();
            StringBuilder causal_Rehabilitaciones = new StringBuilder();
            int iNumTicket = 0;
            List<TemporalTicket> TemporalTicketLis = new List<TemporalTicket>();
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                if (checkedRehabilitar)
                {
                    iNumTicket++;
                    string codigoTicket = dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString();
                    TemporalTicketLis.Add(new TemporalTicket { CodNumeroTicket = codigoTicket });
                    cod_Numero_Tickets.Append(codigoTicket + "|");
                }
            }
            TemporalTicket objTemporalTicket = new TemporalTicket();
            objTemporalTicket.SCodNumeroTicket = cod_Numero_Tickets.ToString();
            objTemporalTicket.SDscNumVuelo = "";
            objTemporalTicket.SCausalRehabilitacion = motivo;
            objTemporalTicket.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);
            objTemporalTicket.TemporalTicketLis = TemporalTicketLis;
            bool ret = objBOTemporalTicket.Eliminar(objTemporalTicket, 1, 4000);
            if (!ret)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = htLabels["rehabilitacionticket.lblMensajeError4.Text"].ToString();
                return;
            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000039", "005", IpClient, "1", "Alerta W0000039", "Proceso de Rehabilitacion de Ticket-Normal, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

            pnlPrincipal.Visible = false;
            pnlConformidad.Visible = true;
            tableRehabilitar.Visible = false;
            
            //See summary
            this.lblTotalRehab.Text = (iNumTicket - 0) + "";
            int iTotal = dtTicketRehabilitados.Rows.Count;
            this.lblTotalNoRehab.Text = (iTotal - (iNumTicket - 0)) + "";

            System.Threading.Thread.Sleep(500);

            String script =
                "<script language=\"javascript\">" +
                "mostrarExcelBtn();" +               
                "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
        }
    }
    #endregion

    #region btnReporte_click
    protected void btnReporte_click(object sender, ImageClickEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        String url = "ReporteREH/ReporteREH_Ticket.aspx?titulo=Rehabilitacion Tickets";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtTicketRehabilitados", dtTicketRehabilitados);
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(), "newWindow",
            string.Format("<script type='text/javascript'>window.open('{0}', 'yourWin', '{1}');</script>", url, winFeatures),
            false);
    }
    #endregion

    internal String replacePort(String mensaje)
    {
        mensaje = mensaje.Replace("<%=PORT%>", "COM" + hdPortSerialLector.Value);
        return mensaje;
    }

    #region "imgExportarExcel"
    protected void imgExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportarDataTable((DataTable)ViewState["tabla"]);        
    }
    #endregion

    #region "ExportarDataTable"
    public void ExportarDataTable(DataTable dt_General)
    {
        //DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        DataTable dtTicketRehabilitados = dt_General;
        DataTable dt = new DataTable();
        DropDownList cboMotivo;
        CheckBox chkSeleccionar;
        DataRow dr;

        bool flag;
        int numero_correlativo = 1;
        DateTime dt_fecha = DateTime.Now;

        #region "Crear Tabla"
        dt.Columns.Add(new DataColumn("Tickets", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero", System.Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("Codigo Ticket", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Observaciones", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Motivo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Salida", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Rehabilitacion", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Usuario Logeado", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Compania", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Vuelo Venta", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Vuelo Rehabilitacion", System.Type.GetType("System.String")));
        #endregion

        if (dtTicketRehabilitados.Rows.Count > 0)
        {

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {

                cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[i].FindControl("chkSeleccionar");
                flag = (bool)dtTicketRehabilitados.Rows[i]["Check"];

                if (chkSeleccionar.Checked)
                {
                    if (flag)
                    {
                        dr = dt.NewRow();
                        dr["Tickets"] = "Tickets Rehabilitados";
                        dr["Numero"] = numero_correlativo++; //dtTicketRehabilitados.Rows[i]["Numero"].ToString();
                        dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = "Ticket Rehabilitado";
                        dr["Fecha Rehabilitacion"] = dt_fecha.ToString();
                        dr["Usuario Logeado"] = Session["Cta_Usuario"];
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                        dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                        dr["Vuelo Rehabilitacion"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Rehab"].ToString() + " -";
                        dt.Rows.Add(dr);
                    }
                }

            }

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                cboMotivo = (DropDownList)gwvRehabilitarPorTicket.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorTicket.Rows[i].FindControl("chkSeleccionar");
                flag = (bool)dtTicketRehabilitados.Rows[i]["Check"];

                if (chkSeleccionar.Checked) //verifica los seleccionados para rehabilitar
                {
                    if (!flag) // pero que no han sido rehabilitados por "x" motivos
                    {
                        dr = dt.NewRow();
                        dr["Tickets"] = "Tickets No Rehabilitados";
                        dr["Numero"] = numero_correlativo++;
                        dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                        dr["Observaciones"] = "-";
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Fecha Rehabilitacion"] = "No aplica";
                        dr["Usuario Logeado"] = "No aplica";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                        dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                        dr["Vuelo Rehabilitacion"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Rehab"].ToString() + " -";
                        dt.Rows.Add(dr);
                    }
                }
                if (!chkSeleccionar.Checked) //verifica los que no han sido seleccionados para rehabilitar
                {
                    dr = dt.NewRow();
                    dr["Tickets"] = "Tickets No Rehabilitados";
                    dr["Numero"] = numero_correlativo++;
                    dr["Codigo Ticket"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString() + " -";
                    dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                    dr["Motivo"] = cboMotivo.SelectedItem.Text;
                    dr["Salida"] = "No seleccionado por DuttyOfficer";
                    dr["Fecha Rehabilitacion"] = "No aplica";
                    dr["Usuario Logeado"] = "No aplica";
                    dr["Compania"] = dtTicketRehabilitados.Rows[i]["Compania_Venta"].ToString();
                    dr["Vuelo Venta"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Venta"].ToString() + " -";
                    dr["Vuelo Rehabilitacion"] = "- " + dtTicketRehabilitados.Rows[i]["Vuelo_Rehab"].ToString() + " -";
                    dt.Rows.Add(dr);
                }
            }
            objBOConsultas.ExportarDataTableToExcel(dt, Response);
        }



    }
    #endregion
  
}


