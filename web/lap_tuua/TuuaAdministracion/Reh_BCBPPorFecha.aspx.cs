﻿using System;
using System.Collections;
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
using System.Collections.Generic;
using Define = LAP.TUUA.UTIL.Define;
using LAP.TUUA.PRINTER;
using System.Xml;

public partial class Reh_BCBPPorFecha : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Rehabilitacion objBORehabilitacion = new BO_Rehabilitacion();

    //Filtros
    string sMotivo;
    string sCompania;
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;


    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        spnRehabilitar.Text = "";
        lblErrorMsg.Text = "";

        if (!IsPostBack)
        {
            Session["tabla"] = null;//Elimino la tabla real.        //EAG 21/10/2010
            Session["BcbpResultado"] = null;

            htLabels = LabelConfig.htLabels;
            try
            {
                btnRehabilitar.Text = htLabels["rehabilitacionBoardingPorFecha.btnRehabilitar"].ToString();
                lblMotivo.Text = htLabels["rehabilitacionBoardingPorFecha.lblMotivo"].ToString();
                lblCia.Text = htLabels["rehabilitacionBoardingPorFecha.rowDesc_Cia"].ToString();
                lblFecha.Text = htLabels["rehabilitacionBoardingPorFecha.lblFecha"].ToString();
                lblDesde.Text = htLabels["rehabilitacionBoardingPorFecha.lblDesde"].ToString();
                lblHasta.Text = htLabels["rehabilitacionBoardingPorFecha.lblHasta"].ToString();
                lblTotalSeleccionados.Text = htLabels["rehabilitacionBoardingPorFecha.lblTotalSeleccionados"].ToString();
                lblTotalIngresados.Text = htLabels["rehabilitacionBoardingPorFecha.lblTotalIngresados"].ToString();
                lblConformidad.Text = htLabels["rehabilitacionBoardingPorFecha.lblConformidad"].ToString();
                rowDesc_Cia.Value = htLabels["rehabilitacionBoardingPorFecha.rowDesc_Cia"].ToString();
                rowDesc_NumVuelo.Value = htLabels["rehabilitacionBoardingPorFecha.rowDesc_NumVuelo"].ToString();
                rowDesc_FechaVuelo.Value = htLabels["rehabilitacionBoardingPorFecha.rowDesc_FechaVuelo"].ToString();
                rowDesc_Asiento.Value = htLabels["rehabilitacionBoardingPorFecha.rowDesc_Asiento"].ToString();
                rowDesc_Pasajero.Value = htLabels["rehabilitacionBoardingPorFecha.rowDesc_Pasajero"].ToString();

                DataTable dtActivarConsRepreReh = objBOConsultas.ListarParametros("CR");
                int activarConsRepre = Int32.Parse(dtActivarConsRepreReh.Rows[0]["Valor"].ToString());
                if (activarConsRepre == 1)
                {
                    lnkRepresentante.Visible = true;
                    lblConsRepresentante.Text = htLabels["rehabilitacionBoardingPorFecha.lblConsRepresentante"].ToString();
                }
                else
                {
                    lnkRepresentante.Visible = false;
                }

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    gwvRehabilitarPorBCBP.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                //else el default es 10.


                int maxRehabilitaciones = 0;
                dt_parametro = objBOConsultas.ListarParametros("RM");
                if (dt_parametro.Rows.Count > 0)
                {
                    maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }
                else
                {
                    maxRehabilitaciones = 800;
                }


                lblDescripcionLimite.Text = String.Format("Solo es posible Rehabilitar {0} BCBP por proceso", maxRehabilitaciones); 


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

            #region Agregando fila vacia a la grilla por default

            AddingEmptyRow();

            #endregion

            #region Llenando data del combo de compania
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }
    #endregion

    #region AddingEmptyRow
    private void AddingEmptyRow()
    {
        DataTable dtBCBPRehabilitados = new DataTable();
        dtBCBPRehabilitados.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Observacion", System.Type.GetType("System.String"));
        dtBCBPRehabilitados.Columns.Add("Check", System.Type.GetType("System.Int32"));

        //eochoa
        dtBCBPRehabilitados.Columns.Add("Bloquear", System.Type.GetType("System.Int32"));

        //Para evitar poner: Eval("Check")!=DBNull.Value &&
        DataRow row = dtBCBPRehabilitados.NewRow();
        row["Check"] = 0;
        //eochoa
        row["Bloquear"] = 0;

        dtBCBPRehabilitados.Rows.Add(row);
        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        gwvRehabilitarPorBCBP.Rows[0].Cells[0].Text = "";
        gwvRehabilitarPorBCBP.Rows[0].FindControl("descBCBP").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("chkBloquear").Visible = false;
        gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;

        //
        lblTxtIngresados.Text = "0";
        lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
        hdNumSelConObs.Value = "0";
        hdNumSelTotal.Value = "0";
    }
    #endregion

    #region lnkRepresentante_Click
    protected void lnkRepresentante_Click(object sender, EventArgs e)
    {
        consRepre.Inicio();
    }
    #endregion

    public void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();
        filterList.Add(new Filtros("sMotivo", cboMotivo.SelectedItem.Value));
        filterList.Add(new Filtros("sCompania", cboCompanias.SelectedValue));
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));

        ViewState.Add("Filtros", filterList);
    }

    public void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sMotivo = newFilterList[0].Valor;
        sCompania = newFilterList[1].Valor;
        sFechaDesde = newFilterList[2].Valor;
        sFechaHasta = newFilterList[3].Valor;

        sHoraDesde = newFilterList[4].Valor;
        sHoraHasta = newFilterList[5].Valor;
    }

    #region btnBuscar_Click
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        SaveFiltros();
        RecuperarFiltros();
        htLabels = LabelConfig.htLabels;
        if (!sFechaDesde.Equals(String.Empty) && !sFechaHasta.Equals(String.Empty))
        {

            //        String fechaDesde = txtDesde.Text.Trim();
            //        String fechaHasta = txtHasta.Text.Trim();
            //        string compania = cboCompanias.SelectedValue.ToString();
            //        string[] wordsFechaDesde = fechaDesde.Split('/');
            //        fechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            //        string[] wordsFechaHasta = fechaHasta.Split('/');
            //        fechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];

            //        //String horaDesde = txtHoraDesde.Text.Trim();
            //        //String horaHasta = txtHoraHasta.Text.Trim();
            //        formatearvalores();

            if (sHoraDesde != "" || sHoraHasta != "")
            {
                if (Convert.ToInt32(sHoraDesde) > Convert.ToInt32(sHoraHasta))
                {
                    lblErrorMsg.Text = "Rango de Hora invalida."; return;
                }
            }

            DataTable dtBCBPRehabilitados = objBORehabilitacion.obteneterBoardingsByRangoFechas(sCompania, sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta);

            if (dtBCBPRehabilitados.Rows.Count == 0)
            {
                #region Agregando fila vacia a la grilla por default
                //Para evitar poner: Eval("Check")!=DBNull.Value &&
                DataRow rowTemp = dtBCBPRehabilitados.NewRow();
                rowTemp["Check"] = 0;
                //eochoa
                rowTemp["Bloquear"] = 0;

                dtBCBPRehabilitados.Rows.Add(rowTemp);

                gwvRehabilitarPorBCBP.PageIndex = 0;//Se sobreentiende
                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                gwvRehabilitarPorBCBP.Rows[0].Cells[0].Text = "";
                gwvRehabilitarPorBCBP.Rows[0].FindControl("descBCBP").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkBloquear").Visible = false;

                lblTxtIngresados.Text = "0";
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";

                #endregion

                Session["tabla"] = null;
                lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError4.Text"].ToString();
            }
            else
            {
                Session["tabla"] = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.PageIndex = 0;
                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                lblTxtIngresados.Text = dtBCBPRehabilitados.Rows.Count.ToString();
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";
            }
        }
        else
        {
            lblErrorMsg.Text = htLabels["rehabilitacionBoarding.lblMensajeError5.Text"].ToString();
        }
    }
    #endregion

    #region gwvRehabilitarPorBCBP_RowDataBound
    protected void gwvRehabilitarPorBCBP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Observacion = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Observacion"));
            if (!Observacion.Equals("-"))
            {
                e.Row.Cells[2].ForeColor = Color.Red;
            }
        }
    }
    #endregion

    #region gwvRehabilitarPorBCBP_RowCommand
    protected void gwvRehabilitarPorBCBP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageCount = gwvRehabilitarPorBCBP.PageCount;

            #region Actualizar Resumen
            Boolean isChecked = ((CheckBox)(gwvRehabilitarPorBCBP.Rows[rowIndex - (pageIndex * pageSize)].FindControl("chkSeleccionar"))).Checked;
            String observaciones = dtBCBPRehabilitados.Rows[rowIndex]["Observacion"].ToString();
            #endregion
            dtBCBPRehabilitados.Rows.RemoveAt(rowIndex);
            //dtBCBPRehabilitados.AcceptChanges();

            #region Guardo las selecciones del checkbox
            int limite;
            if ((pageIndex + 1) < pageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count + 1 - (pageIndex * pageSize);//Sumarle 1 pues lo removio.
            }
            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                if (j != rowIndex - (pageIndex * pageSize))
                {
                    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                    CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
                    dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                    dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
                    //dtTicketRehabilitados.Rows[i]["Numero"] = i + 1;
                }
                else
                {
                    i--;
                }
            }
            #endregion

            #region Define el PageIndex
            if (dtBCBPRehabilitados.Rows.Count == (pageIndex * pageSize) && pageIndex != 0)//esta ultima condicion por el eliminar el unico elemento
            {
                gwvRehabilitarPorBCBP.PageIndex = pageIndex - 1;
            }
            #endregion


            if (dtBCBPRehabilitados.Rows.Count == 0)
            {
                #region Agregando fila vacia a la grilla por default
                //Para evitar poner: Eval("Check")!=DBNull.Value &&
                DataRow rowTemp = dtBCBPRehabilitados.NewRow();
                rowTemp["Check"] = 0;

                //eochoa
                rowTemp["Bloquear"] = 0;

                dtBCBPRehabilitados.Rows.Add(rowTemp);

                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                gwvRehabilitarPorBCBP.Rows[0].Cells[0].Text = "";
                gwvRehabilitarPorBCBP.Rows[0].FindControl("descBCBP").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("btnEliminar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
                gwvRehabilitarPorBCBP.Rows[0].FindControl("chkBloquear").Visible = false;

                lblTxtIngresados.Text = "0";
                lblTxtSeleccionados.Text = "0 (0 Observaciones / 0 Normales)";
                hdNumSelConObs.Value = "0";
                hdNumSelTotal.Value = "0";

                #endregion

                Session["tabla"] = null;
            }
            else
            {
                gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
                gwvRehabilitarPorBCBP.DataBind();

                #region Comentado por actualizacion del checkbox en la grilla (aspx)
                //#region Actualizo la seleccion del check.
                //pageIndex = gwvRehabilitarPorBCBP.PageIndex;
                //pageCount = gwvRehabilitarPorBCBP.PageCount;

                //if ((pageIndex + 1) < pageCount)
                //{
                //    limite = pageSize;
                //}
                //else
                //{
                //    limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
                //}

                //for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
                //{
                //    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                //    chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a generar error
                //}
                //#endregion
                #endregion

                #region Actualizar Resumen
                if (isChecked)
                {
                    hdNumSelTotal.Value = "" + (Int32.Parse(hdNumSelTotal.Value) - 1);
                    if (!observaciones.Equals("-"))
                        hdNumSelConObs.Value = "" + (Int32.Parse(hdNumSelConObs.Value) - 1);
                    int normales = Int32.Parse(hdNumSelTotal.Value) - Int32.Parse(hdNumSelConObs.Value);
                    lblTxtSeleccionados.Text = hdNumSelTotal.Value + " (" + hdNumSelConObs.Value + " Observaciones / " + normales + " Normales)";
                }
                lblTxtIngresados.Text = "" + (Int32.Parse(lblTxtIngresados.Text) - 1);
                #endregion

            }

        }
        else if (e.CommandName.Equals("ShowBCBP"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];
            String Num_Secuencial_Bcbp = dtBCBPRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            CnsDetBoarding1.CargarDetalleBoarding(Num_Secuencial_Bcbp);
        }
    }
    #endregion

    #region Paginacion

    protected void gwvRehabilitarPorBCBP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;

        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        #region Guardo las selecciones del checkbox
        pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
            dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
        }
        #endregion

        gwvRehabilitarPorBCBP.PageIndex = e.NewPageIndex;

        gwvRehabilitarPorBCBP.DataSource = dtBCBPRehabilitados;
        gwvRehabilitarPorBCBP.DataBind();

        //La logica para actualizar el checkbox ya no es necesario debido a que se actualiza en el aspx por la condicion.
        #region Comentado por actualizacion del checkbox en la grilla (aspx)
        //#region Actualizo la seleccion del check.
        //pageIndex = gwvRehabilitarPorBCBP.PageIndex;

        //if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        //{
        //    limite = pageSize;
        //}
        //else
        //{
        //    limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        //}

        //for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        //{
        //    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
        //    chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar
        //}
        //#endregion
        #endregion


    }

    #endregion


    #region gwvRehabilitarPorBCBP_Sorting
    protected void gwvRehabilitarPorBCBP_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];
        if (dtBCBPRehabilitados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox
        int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
        int pageSize = gwvRehabilitarPorBCBP.PageSize;
        int limite;
        if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
            CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
            dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
            dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
        }
        #endregion

        //No es necesario...creo.
        //gwvRehabilitarPorTicket.PageIndex = e.NewPageIndex;

        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        Session["tabla"] = SortDataTable(dtBCBPRehabilitados, false).ToTable();
        //reactualizo la tabla
        dtBCBPRehabilitados = (DataTable)Session["tabla"];

        gwvRehabilitarPorBCBP.DataSource = (DataTable)Session["tabla"];

        gwvRehabilitarPorBCBP.DataBind();

        #region Comentado por actualizacion del checkbox en la grilla (aspx)
        //#region Actualizo la selecciones del checkBox.
        //for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        //{
        //    CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
        //    chkSeleccionar.Checked = (Boolean)dtBCBPRehabilitados.Rows[i]["Check"];//va a fallar
        //}
        //#endregion
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


    #region btnRehabilitar_Click
    protected void btnRehabilitar_Click(object sender, EventArgs e)
    {

        if (Session["tabla"] == null)
        {
            //No hay filas en la grilla
        }
        else
        {
            htLabels = LabelConfig.htLabels;

            int maxRehabilitaciones = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("RM");
            if (dt_parametro.Rows.Count > 0)
            {
                maxRehabilitaciones = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxRehabilitaciones = 800;
            }

            DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

            #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Rehabilitar
            int pageSize = gwvRehabilitarPorBCBP.PageSize;
            int pageIndex = gwvRehabilitarPorBCBP.PageIndex;
            int limite;
            if ((pageIndex + 1) < gwvRehabilitarPorBCBP.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtBCBPRehabilitados.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                CheckBox chkSeleccionar = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkSeleccionar");
                CheckBox chkBloquear = (CheckBox)gwvRehabilitarPorBCBP.Rows[j].FindControl("chkBloquear");
                dtBCBPRehabilitados.Rows[i]["Check"] = chkSeleccionar.Checked;
                dtBCBPRehabilitados.Rows[i]["Bloquear"] = chkBloquear.Checked;
            }
            #endregion

            int nroSeleccionados = dtBCBPRehabilitados.Select("Check=1").Count();
            //int nroSeleccionados2 = Int32.Parse((String)Request.Form["hdNumSelTotal"]);
            if (nroSeleccionados > maxRehabilitaciones)
            {
                System.Threading.Thread.Sleep(500);
                spnRehabilitar.Text = String.Format(htLabels["rehabilitacionticket.lblMensajeError5.Text"].ToString(), maxRehabilitaciones + "");
                return;
            }

            RecuperarFiltros();

            //String motivo = cboMotivo.SelectedItem.Value;

            StringBuilder listaBCBPs = new StringBuilder();
            StringBuilder strLstBloqueados = new StringBuilder();

            bool checkedBloquear = false;
            int iNumBoarding = 0;

            for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
            {
                bool checkedRehabilitar = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Check"].ToString()) == 1 ? true : false;
                if (checkedRehabilitar)
                {
                    iNumBoarding++;

                    //eochoa
                    checkedBloquear = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Bloquear"].ToString()) == 1 ? true : false;
                    strLstBloqueados.Append(checkedBloquear ? "1|" : "0|");

                    String Num_Secuencial_Bcbp = dtBCBPRehabilitados.Rows[i]["Num_Secuencial_Bcbp"].ToString();
                    listaBCBPs.Append(Num_Secuencial_Bcbp + "|");
                }
            }

            BoardingBcbpEstHist boardingBcbpEstHist = new BoardingBcbpEstHist();
            boardingBcbpEstHist.SListaBcbPs = listaBCBPs.ToString();
            boardingBcbpEstHist.SCausalRehabilitacion = sMotivo;
            boardingBcbpEstHist.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);
            //eochoa
            boardingBcbpEstHist.SDesCompania = cboCompanias.SelectedItem.Text;
            boardingBcbpEstHist.SDesMotivo = cboMotivo.SelectedItem.Text;
            boardingBcbpEstHist.Lst_Bloqueados = strLstBloqueados.ToString();

            objBORehabilitacion = new BO_Rehabilitacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            bool ret = objBORehabilitacion.registrarRehabilitacionBCBPAmpliacion(boardingBcbpEstHist, 1, listaBCBPs.Length);
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
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000047", "005", IpClient, "1", "Alerta W0000047", "Proceso de Rehabilitacion de Boarding por Fecha, completado correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }

            Session["BcbpResultado"] = boardingBcbpEstHist;

            int iTotalNoRehab = 0;

            //Quiere decir que hubieron tickets que estaban en estado != 'U'
            if (boardingBcbpEstHist.SListaBcbPs.Length > 0)
            {
                String bcbpsNoRehabilitados = boardingBcbpEstHist.SListaBcbPs.Substring(0, boardingBcbpEstHist.SListaBcbPs.Length - 1);
                String[] bcbpsNoReh = bcbpsNoRehabilitados.Split(new char[] { '|' });
                for (int i = 0; i < bcbpsNoReh.Length; i++)
                {
                    string strData = bcbpsNoReh[i];
                    string[] strDataBCBP = strData.Split(new char[] { ',' });

                    foreach (DataRow row in dtBCBPRehabilitados.Rows)
                    {
                        if (strDataBCBP[0].Equals(row["Num_Secuencial_Bcbp"].ToString()))
                        {
                            string strObservacion = "";

                            switch (strDataBCBP[1])
                            {
                                case "U": strObservacion = htLabels["rehabilitacionticket.lblMensajeError2.Text"].ToString(); break;
                                case "R": strObservacion = htLabels["rehabilitacionticket.lblMensajeError1.Text"].ToString(); break;
                                default: strObservacion = htLabels["rehabilitacionticket.lblMensajeError3.Text"].ToString(); break;
                            }

                             if (boardingBcbpEstHist.SListaOperBcbp.Length > 0)
                            {
                                //hubo operacion de rehabilitacion
                                row["Check"] = true;
                            }
                            else {
                                row["Check"] = false;
                                row["Bloquear"] = false;
                                iTotalNoRehab++; 
                            }
                            row["Observacion"] = strObservacion;

                            break;
                        }
                    }
                }
            }

            //pnlPrincipal.Attributes.Add("style", "display:none");//Visible=false; generaba problemas con el script de checkbox.
            pnlPrincipal.Visible = false;
            //pnlConformidad.Attributes.Add("style", "display:inherit");
            pnlConformidad.Visible = true;
            tableRehabilitar.Visible = false;

            ////See summary
            //this.lblTotalRehab.Text = (iNumBoarding - iTotalNoRehab) + "";
            //this.lblTotalNoRehab.Text = iTotalNoRehab + "";

            //System.Threading.Thread.Sleep(500);

            //See summary
            this.lblTotalRehab.Text = (iNumBoarding - iTotalNoRehab) + "";
            //this.lblTotalNoRehab.Text = iTotalNoRehab + "";

            int iTotal = dtBCBPRehabilitados.Rows.Count;
            this.lblTotalNoRehab.Text = (iTotal - (iNumBoarding - iTotalNoRehab)) + "";

            System.Threading.Thread.Sleep(500);
            int iTotalProcesados = iNumBoarding - iTotalNoRehab;

            String script = string.Empty;

            script = "<script language=\"javascript\">" +
                     "mostrarVoucherBtn();" +
                     "</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key2", script, false);
        }
    }
    #endregion


    #region btnReporte_click
    protected void btnReporte_click(object sender, ImageClickEventArgs e)
    {
        DataTable dtBCBPRehabilitados = (DataTable)Session["tabla"];

        String url = "ReporteREH/ReporteREH_BCBP.aspx?titulo=Rehabilitacion BCBP Por Fecha";
        string winFeatures = "toolbar=no,status=no,menubar=no,location=center,scrollbars=yes,resizable=yes,height=700,width=800";
        Session.Add("dtBCBPRehabilitados", dtBCBPRehabilitados);
        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(), "newWindow",
            string.Format("<script type='text/javascript'>window.open('{0}', 'yourWin', '{1}');</script>", url, winFeatures),
            false);
    }
    #endregion



    #region "Dar Formato de HORA"
    void formatearvalores()
    {
        sHoraDesde = txtHoraDesde.Text;
        sHoraHasta = txtHoraHasta.Text;

        if (sHoraDesde == "__:__:__") { sHoraDesde = ""; }
        if (sHoraHasta == "__:__:__") { sHoraHasta = ""; }

        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;
            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";

        }

        if (sHoraDesde == "00:00:00" && sHoraHasta == "00:00:00")
        {
            sHoraDesde = "";
            sHoraHasta = "";
        }

        if (sHoraDesde != "")
        {
            string[] wordsDesde = sHoraDesde.Split(':');
            sHoraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        }
        else { sHoraDesde = ""; }



        if (sHoraHasta != "")
        {
            string[] wordsHasta = sHoraHasta.Split(':');
            sHoraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
        }
        else { sHoraHasta = ""; }

    }
    #endregion

    protected void btnImprimirBoucher_Click(object sender, ImageClickEventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        Hashtable stDataFormateada = ObtenerDataBoucher((DataTable)Session["tabla"], (BoardingBcbpEstHist)Session["BcbpResultado"]);
        Imprimir(stDataFormateada);
    }

    private Hashtable ObtenerDataBoucher(DataTable dtBCBPRehabilitados, BoardingBcbpEstHist objBcbp)
    {
        RecuperarFiltros();

        Hashtable hsDataFormato = new Hashtable();
        int iNumBoarding = 0;
        String sFechaRehab = DateTime.Now.ToShortDateString();

        hsDataFormato.Add("nombre_aerolinea", objBcbp.SDesCompania);

        //string fechaDesde = txtDesde.Text.Trim();
        //string fechaHasta = txtHasta.Text.Trim();
        string fechaDesde = Fecha.convertSQLToFecha(sFechaDesde, null).Substring(0, 6) + Fecha.convertSQLToFecha(sFechaDesde, null).Substring(8, 2);
        string fechaHasta = Fecha.convertSQLToFecha(sFechaHasta, null).Substring(0, 6) + Fecha.convertSQLToFecha(sFechaHasta, null).Substring(8, 2);
        string rango = fechaDesde + " al " + fechaHasta;
        hsDataFormato.Add("rango_fechas", rango);

        hsDataFormato.Add("motivo_rehab", objBcbp.SDesMotivo.Substring(0, 21));
        hsDataFormato.Add("motivo_rehab1", objBcbp.SDesMotivo.Substring(21));
        hsDataFormato.Add("nro_rehabi", objBcbp.SListaOperBcbp);

        String sFechaHoraRehab = Fecha.formatoFechaExcel(objBcbp.SLogFechaMod); /*objBcbp.SLogFechaMod.Substring(6, 2) + "/" +
                                 objBcbp.SLogFechaMod.Substring(4, 2) + "/" +
                                 objBcbp.SLogFechaMod.Substring(0, 4) + " " +
                                 objBcbp.SLogFechaMod.Substring(8, 2) + ":" +
                                 objBcbp.SLogFechaMod.Substring(10, 2) + ":" +
                                 objBcbp.SLogFechaMod.Substring(12, 2);*/

        hsDataFormato.Add("fecha_hora_rehab", sFechaHoraRehab);

        //hsDataFormato.Add("numero_vuelo", dtBCBPRehabilitados.Rows[0]["Num_Vuelo"].ToString());
        //hsDataFormato.Add("tipo_vuelo", dtBCBPRehabilitados.Rows[0]["Des_Tipo_Vuelo"].ToString());

        //String sFecha = dtBCBPRehabilitados.Rows[0]["Fch_Vuelo"].ToString();
        //sFecha = sFecha.Substring(6, 2) + "/" + sFecha.Substring(4, 2) + "/" + sFecha.Substring(0, 4);
        //hsDataFormato.Add("fecha_vuelo", sFecha);

        //hsDataFormato.Add("numero_vuelo", "-");
        //hsDataFormato.Add("tipo_vuelo", "-");
        //hsDataFormato.Add("fecha_vuelo", "-");

        for (int i = 0; i < dtBCBPRehabilitados.Rows.Count; i++)
        {
            bool checkedRehabilitar = Int32.Parse(dtBCBPRehabilitados.Rows[i]["Check"].ToString()) == 1 ? true : false;
            if (checkedRehabilitar)
            {
                hsDataFormato.Add("numero_vuelo_" + iNumBoarding, dtBCBPRehabilitados.Rows[0]["Num_Vuelo"].ToString());
                hsDataFormato.Add("nombre_pasajero_" + iNumBoarding, dtBCBPRehabilitados.Rows[i]["Nom_Pasajero"].ToString());
                hsDataFormato.Add("nro_asiento_" + iNumBoarding, dtBCBPRehabilitados.Rows[i]["Num_Asiento"].ToString());
                iNumBoarding++;
            }
        }

        hsDataFormato.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, iNumBoarding);
        hsDataFormato.Add("cant_tuua_reh", iNumBoarding);

        return hsDataFormato;
    }

    private void Imprimir(Hashtable htPrintData)
    {
        // instanciar objeto 
        Print objPrint = new Print();

        // obtiene el nodo segun el nombre del voucher
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], Define.ID_PRINTER_DOCUM_REHABILITACIONFECHA);

        // configuracion de la impresora a utilizar
        string configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], Define.ID_PRINTER_DOCUM_REHABILITACIONFECHA);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // obtiene la data a imprimir con la impresora de voucher y guardarla en una variable de sesion
        string dataVoucher = string.Empty;
        dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);

        //int copias = objPrint.ObtenerCopiasVoucher(nodo);
        //guarda data a imprimir en sesion
        Session["dataSticker"] = "";
        Session["dataVoucher"] = dataVoucher;

        Response.Redirect("Ope_Impresion.aspx?" +
            "flagImpSticker=0" +
            "&" + "flagImpVoucher=1" +
            //"&" + "copiasVoucher=" + copias +
            "&" + "configImpSticker=" + "" +
            "&" + "configImpVoucher=" + configImpVoucher +
            "&" + "Pagina_PreImpresion=Reh_BCBPPorFecha.aspx", false);
    }
}
