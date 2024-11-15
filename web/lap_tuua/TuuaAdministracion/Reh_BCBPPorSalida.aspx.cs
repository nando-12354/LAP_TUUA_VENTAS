using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.ALARMAS;
using LAP.TUUA.PRINTER;
using System.Xml;
using Define = LAP.TUUA.UTIL.Define;
using Fecha = LAP.TUUA.UTIL.Fecha;

public partial class Reh_BCBP : System.Web.UI.Page
{

    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    BO_TemporalBoardingPass objBOTemporalBoardingPass = new BO_TemporalBoardingPass("_TemporalBoardingPass");
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

                btnRehabilitar.Text = htLabels["rehabilitacionticket.btnRehabilitar"].ToString();
                lblNroTicket.Text = "Fecha de Vuelo";
                lblNroVuelo.Text = "Nro de Vuelo";
                lblCia.Text = "Compañia";
                lblMotivo.Text = htLabels["rehabilitacionticketPorVuelo.lblMotivo"].ToString();
                lblTotalSeleccionados.Text = htLabels["rehabilitacionticket.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionticket.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionticket.lblConformidad"].ToString();

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    gwvRehabilitarPorBCBP.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
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
            dtTicketRehabilitados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Bcbp", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Creacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Registro", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());
            gwvRehabilitarPorBCBP.DataSource = dtTicketRehabilitados;
            gwvRehabilitarPorBCBP.DataBind();

            gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
            gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;

            #region Llenando combos de busqueda: Lleno las companias y vacio el cboVuelo
            DataTable dtCompanias = objBOConsultas.listarAllCompania();
            cboCompanias.DataSource = dtCompanias;
            cboCompanias.DataTextField = "Dsc_Compania";
            cboCompanias.DataValueField = "Cod_Compania";
            cboCompanias.DataBind();
            cboCompanias.Items.Insert(0, "Seleccionar");
            #endregion

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

    #region cboCompanias_SelectedIndexChanged
    protected void cboCompanias_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Agregando fila vacia a la grilla por default y eliminado la tabla viewstate.
        //AddingEmptyRow();
        //Session["tabla"] = null;//Elimino la tabla real.
        #endregion

        //Borro la fecha ingresada
        txtNroTicket.Text = "";
    }
    #endregion

    #region btnAgregar_Click

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int pageCount = gwvRehabilitarPorBCBP.PageCount;
        string codigoTicket;
        string numeroVuelo;
        codigoTicket = txtNroTicket.Text;
        numeroVuelo = txtNroVuelo.Text;
        txtNroTicket.Text = "";
        txtNroVuelo.Text = "";
        if (codigoTicket.Equals(String.Empty))
        {
            lblErrorMsg.Text = "Error. La fecha no puede ser vacío";
            return;
        }
        TemporalBoardingPass objTemporalBoardingPass = new TemporalBoardingPass();
        objTemporalBoardingPass.CodCompania = cboCompanias.SelectedItem.Value;
        objTemporalBoardingPass.FchVuelo = Convert.ToDateTime(codigoTicket).ToString("yyyyMMdd");
        objTemporalBoardingPass.NumVuelo = numeroVuelo;
        DataTable dtTicketDetalle = objBOTemporalBoardingPass.ListarAll(objTemporalBoardingPass);
        if (dtTicketDetalle.Rows.Count == 0)
        {
            lblErrorMsg.Text = "Error. La fecha ingresada no contiene BoardingPass.";
            return;
        }
        DataTable dtTicketRehabilitados;
        DataRow row;
        if (ViewState["tabla"] != null)
        {
            dtTicketRehabilitados = (DataTable)ViewState["tabla"];

            #region Guardo las selecciones del checkbox
            pageIndex = gwvRehabilitarPorBCBP.PageIndex;
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
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            }
            #endregion
        }
        else
        {
            dtTicketRehabilitados = new DataTable();
            dtTicketRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
            dtTicketRehabilitados.Columns.Add("Cod_Numero_Bcbp", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Creacion", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Fch_Registro", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtTicketRehabilitados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
            foreach (DataRow item in dtTicketDetalle.Rows)
            {
                row = dtTicketRehabilitados.NewRow();
                row["Check"] = false;
                row["Num_Secuencial_Bcbp"] = item["Num_Secuencial_Bcbp"];
                row["Cod_Numero_Bcbp"] = item["Cod_Numero_Bcbp"];
                row["Nom_Pasajero"] = item["Nom_Pasajero"];
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
            gwvRehabilitarPorBCBP.PageIndex = pageCount;
        }
        else
        {
            gwvRehabilitarPorBCBP.PageIndex = pageCount - 1;
        }
        #endregion

        gwvRehabilitarPorBCBP.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        #region Actualizar Resumen
        lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) + 1);
        #endregion
    }

    #endregion

    #region gwvRehabilitarPorTicket_RowCommand
    protected void gwvRehabilitarPorBCBP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];

            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageCount = gwvRehabilitarPorBCBP.PageCount;

            #region Actualizar Resumen
            bool isChecked = ((CheckBox)(gwvRehabilitarPorBCBP.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
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
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
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
                gwvRehabilitarPorBCBP.PageIndex = pageIndex - 1;
            }
            #endregion


            if (dtTicketRehabilitados.Rows.Count == 0)
            {
                ViewState["tabla"] = null;
                #region Agregando fila vacia a la grilla por default
                dtTicketRehabilitados.Rows.Add(dtTicketRehabilitados.NewRow());

                gwvRehabilitarPorBCBP.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                //gwvRehabilitarPorTicket.Rows[0].Cells[5].Controls.Clear();
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
                #endregion
            }
            else
            {
                gwvRehabilitarPorBCBP.DataSource = dtTicketRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
                pageIndex = gwvRehabilitarPorBCBP.PageIndex;
                pageCount = gwvRehabilitarPorBCBP.PageCount;

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
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                }
                #endregion
            }

        }
        else if (e.CommandName.Equals("ShowTicket"))
        {
            //int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            //DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
            //String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString() + "-" + dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            //CnsDetBoarding.Inicio(codTicket);
        }
    }
    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorBCBP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;

        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        #region Guardo las selecciones del combo y checkbox
        pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        gwvRehabilitarPorBCBP.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorBCBP.DataSource = dtTicketRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        #region Lleno el combo. Ademas actualizo la seleccion del combo y el check.
        pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
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
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            chkSeleccionar.Checked = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
        }
        #endregion
    }
    #endregion

    #region Colorear las Observaciones negativas - RowDataBound

    protected void gwvRehabilitarPorBCBP_RowDataBound(object sender, GridViewRowEventArgs e)
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

    #region gwvRehabilitarPorBCBP_Sorting
    protected void gwvRehabilitarPorBCBP_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTicketRehabilitados = (DataTable)ViewState["tabla"];
        if (dtTicketRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox y comboBox
        int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            dtTicketRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tabla"] = SortDataTable(dtTicketRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtTicketRehabilitados = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorBCBP.DataSource = (DataTable)ViewState["tabla"];

        gwvRehabilitarPorBCBP.DataBind();

        #region Lleno el combo. Actualizo la selecciones del checkBox y combobox.
        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            //Aqui no hay condicion pues no he agregado ninguna fila.
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
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
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTicketRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
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
            StringBuilder listaBCBPs = new StringBuilder();
            StringBuilder strLstBloqueados = new StringBuilder();
            StringBuilder dsc_Num_Vuelos = new StringBuilder();
            StringBuilder causal_Rehabilitaciones = new StringBuilder();
            int iNumTicket = 0;
            List<TemporalBoardingPass> TemporalTicketLis = new List<TemporalBoardingPass>();
            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = (Boolean)dtTicketRehabilitados.Rows[i]["Check"];
                if (checkedRehabilitar)
                {
                    iNumTicket++;
                    string secuencialBcbp = dtTicketRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString();
                    string codigoBcbp = dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString();
                    TemporalTicketLis.Add(new TemporalBoardingPass { CodNumeroBcbp = codigoBcbp });
                    listaBCBPs.Append(secuencialBcbp + "|");
                    strLstBloqueados.Append("0|");
                }
            }
            TemporalBoardingPass objTemporalTicket = new TemporalBoardingPass();
            objTemporalTicket.SListaBCBPs = listaBCBPs.ToString();
            objTemporalTicket.SCausalRehabilitacion = motivo;
            objTemporalTicket.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);
            objTemporalTicket.SDesCompania = cboCompanias.SelectedItem.Text;
            objTemporalTicket.SDesMotivo = cboMotivo.SelectedItem.Text;
            objTemporalTicket.LstBloqueados = strLstBloqueados.ToString();
            objTemporalTicket.CodUsuario = (string)Session["Cod_Usuario"];
            objTemporalTicket.CodModulo = (string)Session["Cod_Modulo"];
            objTemporalTicket.CodSubModulo = (string)Session["Cod_SubModulo"];
            objTemporalTicket.TemporalBoardingPassLis = TemporalTicketLis;
            bool ret = objBOTemporalBoardingPass.Eliminar(objTemporalTicket, 1, 4000);
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
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        String url = "ReporteREH/ReporteREH_BCBP.aspx?titulo=Rehabilitacion BCBP";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtBCBPRehabilitados", dtBCBPRehabilitados);
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

    #region "btnExportarExcel"
    protected void imgExportarExcel_Click(object sender, ImageClickEventArgs e)
    {
        ExportarDataTable((DataTable)Session["tabla"]);
    }
    #endregion

    #region "ExportarDataTable"
    public void ExportarDataTable(DataTable dt_General)
    {
        //DataTable dtTicketRehabilitados = (DataTable)Session["tabla"];
        DataTable dtTicketRehabilitados = dt_General;
        DataTable dt = new DataTable();
        DropDownList cboMotivo;
        CheckBox chkSeleccionar;
        DataRow dr;

        Int32 flag;
        int numero_correlativo = 1;
        DateTime dt_fecha = DateTime.Now;
        string str_fecha;

        #region "Crear Tabla"
        dt.Columns.Add(new DataColumn("Boarding", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero", System.Type.GetType("System.Int32")));
        dt.Columns.Add(new DataColumn("Numero Boarding", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Compania", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero Vuelo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Vuelo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Numero Asiento", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Pasajero", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Observaciones", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Motivo", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Salida", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Fecha Rehabilitacion", System.Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Usuario Logeado", System.Type.GetType("System.String")));
        #endregion

        if (dtTicketRehabilitados.Rows.Count > 0)
        {

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {

                cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[i].FindControl("chkSeleccionar");
                flag = Convert.ToInt32(dtTicketRehabilitados.Rows[i]["Check"]);
                str_fecha = dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(6, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(4, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(0, 4);

                if (chkSeleccionar.Checked)
                {
                    if (flag == 1)
                    {
                        dr = dt.NewRow();
                        dr["Boarding"] = "Boarding Pass Rehabilitados";
                        dr["Numero"] = numero_correlativo++; //dtTicketRehabilitados.Rows[i]["Numero"].ToString();
                        dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                        dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                        dr["Fecha Vuelo"] = str_fecha;
                        dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                        dr["Pasajero"] = "- " + dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString() + " -";
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = "Boarding Pass Rehabilitado";
                        dr["Fecha Rehabilitacion"] = dt_fecha.ToString();
                        dr["Usuario Logeado"] = Session["Cta_Usuario"];
                        
                        dt.Rows.Add(dr);
                    }
                }

            }

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                cboMotivo = (DropDownList)gwvRehabilitarPorBCBP.Rows[i].FindControl("cboMotivo");
                chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[i].FindControl("chkSeleccionar");
                flag = Convert.ToInt32(dtTicketRehabilitados.Rows[i]["Check"]);
                str_fecha = dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(6, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(4, 2) + "/" + dtTicketRehabilitados.Rows[i]["Fch_Vuelo"].ToString().Substring(0, 4);

                if (chkSeleccionar.Checked) //verifica los seleccionados para rehabilitar
                {
                    if (flag != 1) // pero que no han sido rehabilitados por "x" motivos
                    {
                        dr = dt.NewRow();
                        dr["Boarding"] = "Boarding Pass No Rehabilitados";
                        dr["Numero"] = numero_correlativo++;
                        dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                        dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                        dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                        dr["Fecha Vuelo"] = str_fecha;
                        dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                        dr["Pasajero"] = dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString();
                        dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                        dr["Motivo"] = cboMotivo.SelectedItem.Text;
                        dr["Salida"] = "Boarding Pass No Rehabilitado";
                        dr["Fecha Rehabilitacion"] = "No Aplica";
                        dr["Usuario Logeado"] = "No Aplica";
                        dt.Rows.Add(dr);
                    }
                }
                if (!chkSeleccionar.Checked) //verifica los que no han sido seleccionados para rehabilitar
                {
                    dr = dt.NewRow();
                    dr["Boarding"] = "Boarding Pass No Rehabilitados";
                    dr["Numero"] = numero_correlativo++;
                    dr["Numero Boarding"] = "- " + dtTicketRehabilitados.Rows[i]["Cod_Numero_Bcbp"].ToString() + " -";
                    dr["Compania"] = dtTicketRehabilitados.Rows[i]["Dsc_Compania"].ToString();
                    dr["Numero Vuelo"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Vuelo"].ToString() + " -";
                    dr["Fecha Vuelo"] = str_fecha;
                    dr["Numero Asiento"] = "- " + dtTicketRehabilitados.Rows[i]["Num_Asiento"].ToString() + " -";
                    dr["Pasajero"] = dtTicketRehabilitados.Rows[i]["Nom_Pasajero"].ToString();
                    dr["Observaciones"] = dtTicketRehabilitados.Rows[i]["Observacion"].ToString();
                    dr["Motivo"] = cboMotivo.SelectedItem.Text;
                    dr["Salida"] = "No seleccionado por DuttyOfficer";
                    dr["Fecha Rehabilitacion"] = "No Aplica";
                    dr["Usuario Logeado"] = "No Aplica";
                    dt.Rows.Add(dr);
                }
            }
            objBOConsultas.ExportarDataTableToExcel(dt, Response);
        }
    }
    #endregion

}
