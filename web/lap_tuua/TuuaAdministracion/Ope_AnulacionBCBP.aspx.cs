using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using System.Collections.Generic;
using LAP.TUUA.ALARMAS;

public partial class Ope_AnulacionBCBP : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Operacion objBOOperacion = new BO_Operacion();
    private BO_Administracion objWBAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();
    
    public int ValiFechas;

    string sMaxGrilla;

    //Filtros
    string sFechaDesde;
    string sFechaHasta;
    string sHoraDesde;
    string sHoraHasta;
    string sAerolinea;
    string sNumVuelo;
    string sNumAsiento;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMensajeError.Text = "";
        lblErrorMsg.Text = "";

        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                btnAnular.Text = htLabels["anulacionBCBP.btnAnular"].ToString();
                this.lblDesde.Text = htLabels["anulacionBCBP.lblDesde"].ToString();
                lblHasta.Text = htLabels["anulacionBCBP.lblHasta"].ToString();
                lblTotalIngresados.Text = htLabels["anulacionBCBP.lblTotalIngresados"].ToString();
                this.lblTotalSeleccionados.Text = htLabels["anulacionBCBP.lblTotalSeleccionados"].ToString();
                this.cbeAnular.ConfirmText = htLabels["anulacionBCBP.cbeAnular"].ToString();
                this.lblAerolinea.Text = htLabels["anulacionBCBP.lblAerolinea"].ToString();
                this.lblNroAsiento.Text = htLabels["anulacionBCBP.lblNroAsiento"].ToString();
                this.lblNroVuelo.Text = htLabels["anulacionBCBP.lblNroVuelo"].ToString();
                this.lblFechaLectura.Text = htLabels["anulacionBCBP.lblFechaLectura"].ToString();
                this.lblMotivo.Text = htLabels["anulacionticket.lblMotivo"].ToString();
                this.txtMotivo.Enabled = false;
                //rowDesc_Cia.Value = htLabels["anulacionBCBP.rowDesc_Cia"].ToString();
                //rowDesc_NumVuelo.Value = htLabels["anulacionBCBP.rowDesc_NumVuelo"].ToString();
                //rowDesc_FechaVuelo.Value = htLabels["anulacionBCBP.rowDesc_FechaVuelo"].ToString();
                //rowDesc_Asiento.Value = htLabels["anulacionBCBP.rowDesc_Asiento"].ToString();
                //rowDesc_Pasajero.Value = htLabels["anulacionBCBP.rowDesc_Pasajero"].ToString();

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.gwvAnularBCBP.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
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

            //GrillaDefault();
            AddingEmptyRow();
            CargarCombos();

            gwvAnularBCBP.Columns[9].Visible = false; //Para ocultar la columna Num_Secuencial

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }


    private void AddingEmptyRow()
    {
        DataTable dtBCBPAnulados = new DataTable();
        dtBCBPAnulados.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
        dtBCBPAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
        dtBCBPAnulados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
        //Para evitar poner: Eval("Check")!=DBNull.Value &&
        
        DataRow row = dtBCBPAnulados.NewRow();
        row["Check"] = false;

        dtBCBPAnulados.Rows.Add(row);
        gwvAnularBCBP.DataSource = dtBCBPAnulados;
        gwvAnularBCBP.DataBind();

        gwvAnularBCBP.Rows[0].Cells[0].Text = "";
       // gwvAnularBCBP.Rows[0].FindControl("descBCBP").Visible = false;
        //gwvAnularBCBP.Rows[0].FindControl("txtMotivo").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblTipoPasajero").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblTipoVuelo").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblTipoTrasbordo").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblDscCompania").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblDscNumVuelo").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblDscFechaVuelo").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblDscAsiento").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblDscPasajero").Visible = false;
        gwvAnularBCBP.Rows[0].FindControl("lblTipEstado").Visible = false;

        lblTxtIngresados.Text = "0";
        lblTxtSeleccionados.Text = "0";
        hdNumSelConObs.Value = "0";
        hdNumSelTotal.Value = "0";
    }



    public void CargarCombos()
    {
        try
        {
            //Carga combo Aerolineas

            DataTable dt_allcompania = new DataTable();
            dt_allcompania = objBOConsultas.listarAllCompania();
            objCargaCombo.LlenarCombo(this.ddlAerolinea, FiltrarAerolinea(dt_allcompania), "Cod_Compania", "Dsc_Compania", true,false);

        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }

    private DataTable FiltrarAerolinea(DataTable dtCompania)
    {
        DataTable dest = new DataTable("Result" + dtCompania.TableName);
        DataColumn dc;

        dc = new DataColumn();
        dc.ColumnName = "Cod_Compania";
        dest.Columns.Add(dc);
        dc = new DataColumn();
        dc.ColumnName = "Dsc_Compania";
        dest.Columns.Add(dc);


        DataRow[] foundRowAerolinea = dtCompania.Select("Tip_Compania = '1'");

        if (foundRowAerolinea != null && foundRowAerolinea.Length > 0)
        {
            for (int i = 0; i < foundRowAerolinea.Length; i++)
            {
                dest.Rows.Add(dest.NewRow());
                dest.Rows[i][0] = foundRowAerolinea[i]["Cod_Compania"].ToString();
                dest.Rows[i][1] = foundRowAerolinea[i]["Dsc_Compania"].ToString();
            }
        }

        dest.AcceptChanges();
        return dest;
    }

    protected bool validaBuscar()
    {

        if (txtDesde.Text == "")
        {
            this.lblErrorMsg.Text = "Ingrese la Fecha Inicial";
            return false;
        }

        if (txtHasta.Text == "")
        {
            this.lblErrorMsg.Text = "Ingrese la Fecha Final";
            return false;
        }

        if (txtDesde.Text != "" && txtHasta.Text != "")
        {
            int result = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text), Convert.ToDateTime(this.txtHasta.Text));
            if (result == 1)
            {
                this.lblErrorMsg.Text = "Rango de fecha invalido";
                return false;
            }
            else if (result != 1)
                this.lblMensajeError.Text = "";
        }
        return true;

    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL(txtDesde.Text)));
        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sAerolinea", ddlAerolinea.SelectedValue));
        filterList.Add(new Filtros("sNumVuelo", txtNroVuelo.Text));
        filterList.Add(new Filtros("sNumAsiento", txtNroAsiento.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sFechaDesde = newFilterList[0].Valor;
        sFechaHasta = newFilterList[1].Valor;
        sHoraDesde  = newFilterList[2].Valor;
        sHoraHasta  = newFilterList[3].Valor;
        sAerolinea  = newFilterList[4].Valor;
        sNumVuelo   = newFilterList[5].Valor;
        sNumAsiento = newFilterList[6].Valor;
    }


    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        if (validaBuscar())
        {
            ViewState["dtSeleccionados"] = null; //para eliminar los datos anteriores
            gwvAnularBCBP.Sort("Num_Secuencial_Bcbp", SortDirection.Ascending);
            //gwvAnularBCBP.Sort("Cod_Numero_Ticket", SortDirection.Ascending);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "<script language=\"javascript\">SetNumTotal();</script>", false);
            //hdNumSelTotal.Value = "0";
            SaveFiltros();
            BindPagingGrid();

            #region Creando la tabla para guardar los checkbox seleccionados
            DataTable dtBCBPSeleccionados = new DataTable();
            dtBCBPSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtBCBPSeleccionados.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
            dtBCBPSeleccionados.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtBCBPSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = dtBCBPSeleccionados;
            #endregion
        }
        //if (validaBuscar() == true)
        //    Buscar();
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
        gwvAnularBCBP.VirtualItemCount = GetRowCount();

        DataTable dt_consulta = new DataTable();

        if (gwvAnularBCBP.OrderBy.Contains("Check"))
            //dt_consulta = GetDataPage(gwvAnularTicket.PageIndex, gwvAnularTicket.PageSize, null);
            dt_consulta = GetDataPageCheck(gwvAnularBCBP.PageIndex, gwvAnularBCBP.PageSize, gwvAnularBCBP.SortDirection).Table;
        else
            dt_consulta = GetDataPage(gwvAnularBCBP.PageIndex, gwvAnularBCBP.PageSize, gwvAnularBCBP.OrderBy);

        htLabels = LabelConfig.htLabels;
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
                //this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                this.lblTxtIngresados.Text = "";
                this.lblTxtSeleccionados.Text = "";
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

            DataTable dtBoardingAnulados = new DataTable();
            dtBoardingAnulados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtBoardingAnulados.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
            dtBoardingAnulados.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = null;
            #region Agregando fila vacia a la grilla por default
            dtBoardingAnulados.Rows.Add(dtBoardingAnulados.NewRow());

            gwvAnularBCBP.PageIndex = 0;//Se sobreentiende
            gwvAnularBCBP.DataSource = dtBoardingAnulados;
            gwvAnularBCBP.DataBind();
            gwvAnularBCBP.Rows[0].Cells[0].Text = "";
            //gwvAnularBCBP.Rows[0].FindControl("txtMotivo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoPasajero").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoTrasbordo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscCompania").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscNumVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscFechaVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscAsiento").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscPasajero").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipEstado").Visible = false;

            lblTxtIngresados.Text = "0";
            lblTxtSeleccionados.Text = "0";
            #endregion

            lblErrorMsg.Text = "No se encontro resultado alguno.";
        }
        else
        {
            this.txtMotivo.Enabled = true;
            this.btnAnular.Enabled = true;
            //txtMotivo.Text = "ff";
            htLabels = LabelConfig.htLabels;
            gwvAnularBCBP.DataSource = dt_consulta;
            gwvAnularBCBP.PageSize = Convert.ToInt32(this.sMaxGrilla);
            gwvAnularBCBP.DataBind();
            this.lblMensajeError.Text = "";
        }
    }

    private int GetRowCount()
    {
        int count = 0;
        try
        {
            DataTable dt_consulta = new DataTable();

            dt_consulta = objBOConsultas.obtenerBoardingUsados(sFechaDesde,
                                                                sFechaHasta,
                                                                sHoraDesde,
                                                                sHoraHasta,
                                                                "",
                                                                sAerolinea,
                                                                sNumVuelo,
                                                                sNumAsiento,
                                                                null,
                                                                0,0, "0", "1");
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
        try
        {
            dt_consulta = objBOConsultas.obtenerBoardingUsados(sFechaDesde, 
                                                                sFechaHasta, 
                                                                sHoraDesde, 
                                                                sHoraHasta, 
                                                                "", 
                                                                sAerolinea, 
                                                                sNumVuelo, 
                                                                sNumAsiento, 
                                                                sortExpression, 
                                                                pageIndex,
                                                                pageSize, "1", "0");

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

    #region Dynamic data query (check)
    private void BindPagingGridCheck(string sortDirection, string sortExpression)
    {
        RecuperarFiltros();
        ValidarTamanoGrilla();
        gwvAnularBCBP.VirtualItemCount = GetRowCount();

        DataView dv_consulta = GetDataPageCheck(gwvAnularBCBP.PageIndex, gwvAnularBCBP.PageSize, gwvAnularBCBP.SortDirection);

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

            DataTable dtBoardingAnulados = new DataTable();
            dtBoardingAnulados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtBoardingAnulados.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
            dtBoardingAnulados.Columns.Add("Motivo", System.Type.GetType("System.String"));
            dtBoardingAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));


            ViewState["dtSeleccionados"] = null;
            #region Agregando fila vacia a la grilla por default
            dtBoardingAnulados.Rows.Add(dtBoardingAnulados.NewRow());

            gwvAnularBCBP.PageIndex = 0;//Se sobreentiende
            gwvAnularBCBP.DataSource = dtBoardingAnulados;
            gwvAnularBCBP.DataBind();
            gwvAnularBCBP.Rows[0].Cells[0].Text = "";
            //gwvAnularBCBP.Rows[0].FindControl("txtMotivo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("chkSeleccionar").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoPasajero").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipoTrasbordo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscCompania").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscNumVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscFechaVuelo").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscAsiento").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblDscPasajero").Visible = false;
            gwvAnularBCBP.Rows[0].FindControl("lblTipEstado").Visible = false;

            lblTxtIngresados.Text = "0";
            lblTxtSeleccionados.Text = "0";
            #endregion

            lblErrorMsg.Text = "No se encontro resultado alguno.";
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            gwvAnularBCBP.DataSource = dv_consulta.Table;
            gwvAnularBCBP.PageSize = Convert.ToInt32(this.sMaxGrilla);
            gwvAnularBCBP.DataBind();
            this.lblMensajeError.Text = "";
        }
    }

    private DataView GetDataPageCheck(int pageIndex, int pageSize, SortDirection sortDirection)
    {
        DataTable dt_consulta = new DataTable();
        DataTable dtSeleccionados = new DataTable();

        if ((DataTable)ViewState["dtSeleccionados"] != null)
            dtSeleccionados = ((DataTable)ViewState["dtSeleccionados"]).Copy();

        bool datos = false;
        try
        {
            string sBoardingSel = "";
            //concatenamos los tickets seleccionados
            foreach (DataRow row in dtSeleccionados.Rows)
            {
                sBoardingSel += row["Num_Secuencial_Bcbp"] + "|";
            }

            if (sBoardingSel != "")
                sBoardingSel = sBoardingSel.Substring(0, sBoardingSel.Length - 1);
            //else
            //    sBoardingSel = "0";

            
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
                    dt_consulta = objBOConsultas.obtenerBoardingUsados(sFechaDesde,
                                                                sFechaHasta,
                                                                sHoraDesde,
                                                                sHoraHasta,
                                                                sBoardingSel,
                                                                sAerolinea,
                                                                sNumVuelo,
                                                                sNumAsiento,
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
                        dt_consulta = objBOConsultas.obtenerBoardingUsados(sFechaDesde,
                                                                sFechaHasta,
                                                                sHoraDesde,
                                                                sHoraHasta,
                                                                sBoardingSel,
                                                                sAerolinea,
                                                                sNumVuelo,
                                                                sNumAsiento,
                                                                "",
                                                                pageIndex,
                                                                pageSize, "1", "0");


                        int nroRegistros = dtSeleccionados.Rows.Count;
                        int inicio = (_pageSize * _pageIndex) + 1;
                        int fin = nroRegistros;

                        DataRow[] rowsPage = dtSeleccionados.Select("Numero >= " + inicio + " AND Numero<= " + fin + " ");

                        DataTable table = new DataTable();
                        table.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                        table.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
                        table.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
                        table.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
                        table.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
                        table.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
                        table.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
                        table.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
                        table.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                        foreach (DataRow row in rowsPage)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = row["Numero"];
                            newRow["Tipo_Pasajero"] = row["Tipo_Pasajero"];
                            newRow["Tipo_Vuelo"] = row["Tipo_Vuelo"];
                            newRow["Tipo_Trasbordo"] = row["Tipo_Trasbordo"];
                            newRow["Cod_Compania"] = row["Cod_Compania"];
                            newRow["Dsc_Compania"] = row["Dsc_Compania"];
                            newRow["Num_Vuelo"] = row["Num_Vuelo"];
                            newRow["Fch_Vuelo"] = row["Fch_Vuelo"];
                            newRow["Num_Asiento"] = row["Num_Asiento"];
                            newRow["Nom_Pasajero"] = row["Nom_Pasajero"];
                            newRow["Num_Secuencial_Bcbp"] = row["Num_Secuencial_Bcbp"];
                            newRow["Tip_Estado"] = row["Tip_Estado"];
                            newRow["Motivo"] = row["Motivo"];
                            newRow["Check"] = row["Check"];
                            table.Rows.Add(newRow);
                        }

                        int nroFilas = table.Rows.Count;
                        foreach (DataRow row in dt_consulta.Rows)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = nroFilas;
                            newRow["Tipo_Pasajero"] = row["Tipo_Pasajero"];
                            newRow["Tipo_Vuelo"] = row["Tipo_Vuelo"];
                            newRow["Tipo_Trasbordo"] = row["Tipo_Trasbordo"];
                            newRow["Cod_Compania"] = row["Cod_Compania"];
                            newRow["Dsc_Compania"] = row["Dsc_Compania"];
                            newRow["Num_Vuelo"] = row["Num_Vuelo"];
                            newRow["Fch_Vuelo"] = row["Fch_Vuelo"];
                            newRow["Num_Asiento"] = row["Num_Asiento"];
                            newRow["Nom_Pasajero"] = row["Nom_Pasajero"];
                            newRow["Num_Secuencial_Bcbp"] = row["Num_Secuencial_Bcbp"];
                            newRow["Tip_Estado"] = row["Tip_Estado"];
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
                        table.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
                        table.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
                        table.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
                        table.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
                        table.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
                        table.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
                        table.Columns.Add("Motivo", System.Type.GetType("System.String"));
                        table.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
                        table.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                        foreach (DataRow row in rowsPage)
                        {
                            DataRow newRow = table.NewRow();
                            newRow["Numero"] = row["Numero"];
                            newRow["Tipo_Pasajero"] = row["Tipo_Pasajero"];
                            newRow["Tipo_Vuelo"] = row["Tipo_Vuelo"];
                            newRow["Tipo_Trasbordo"] = row["Tipo_Trasbordo"];
                            newRow["Cod_Compania"] = row["Cod_Compania"];
                            newRow["Dsc_Compania"] = row["Dsc_Compania"];
                            newRow["Num_Vuelo"] = row["Num_Vuelo"];
                            newRow["Fch_Vuelo"] = row["Fch_Vuelo"];
                            newRow["Num_Asiento"] = row["Num_Asiento"];
                            newRow["Nom_Pasajero"] = row["Nom_Pasajero"];
                            newRow["Num_Secuencial_Bcbp"] = row["Num_Secuencial_Bcbp"];
                            newRow["Tip_Estado"] = row["Tip_Estado"];
                            newRow["Motivo"] = row["Motivo"];
                            newRow["Check"] = row["Check"];
                            table.Rows.Add(newRow);
                        }

                        dtSeleccionados = table;
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

    
    
    

    protected void btnAnular_Click(object sender, EventArgs e)
    {

        objBOOperacion = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);

        if (ViewState["dtSeleccionados"] == null)
        {
            lblMensajeError.Text = "Debe de seleccionar al menos un BCBP para anular.";
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

            DataTable dtBCBPAnulados = (DataTable)ViewState["dtSeleccionados"];

            int nroSeleccionados = dtBCBPAnulados.Rows.Count; 

            if (nroSeleccionados > maxAnulaciones)
            {
                System.Threading.Thread.Sleep(500);
                this.lblMensajeError.Text = "Sobrepaso el maximo de Anulaciones (" + maxAnulaciones + ")";
                return;
            }


            List<BoardingBcbp> objListBoardingBcbp = new List<BoardingBcbp>();
            BoardingBcbp objBoardingBcbp = new BoardingBcbp();

            for (int i = 0; i < dtBCBPAnulados.Rows.Count; i++)
            {
                //bool checkedAnular = (Boolean)dtBCBPAnulados.Rows[i]["Check"];
                //if (checkedAnular)
                //{
                    objBoardingBcbp = new BoardingBcbp();
                    objBoardingBcbp.INumSecuencial = Convert.ToInt32(dtBCBPAnulados.Rows[i]["Num_Secuencial_Bcbp"].ToString());
                    objBoardingBcbp.SCodCompania = dtBCBPAnulados.Rows[i]["Cod_Compania"].ToString();
                    objBoardingBcbp.SNumVuelo = dtBCBPAnulados.Rows[i]["Num_Vuelo"].ToString();
                    objBoardingBcbp.StrFchVuelo = Fecha.convertToFechaSQL(dtBCBPAnulados.Rows[i]["Fch_Vuelo"].ToString());
                    //if (objBoardingBcbp.StrFchVuelo.Trim().Length == 10)
                    //{
                    //    objBoardingBcbp.StrFchVuelo = objBoardingBcbp.StrFchVuelo.Substring(6, 4) + objBoardingBcbp.StrFchVuelo.Substring(3, 2) + objBoardingBcbp.StrFchVuelo.Substring(0, 2);
                    //}
                    objBoardingBcbp.StrNumAsiento = dtBCBPAnulados.Rows[i]["Num_Asiento"].ToString();
                    objBoardingBcbp.StrNomPasajero = dtBCBPAnulados.Rows[i]["Nom_Pasajero"].ToString();
                    objBoardingBcbp.StrLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);
                    objBoardingBcbp.StrMotivo = dtBCBPAnulados.Rows[i]["Motivo"].ToString();
                    objListBoardingBcbp.Add(objBoardingBcbp);
                //}

            }

            if (objListBoardingBcbp.Count > 0)
            {
                bool ret = objBOOperacion.AnularTicket(objListBoardingBcbp);
                if (!ret)
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000029", "004", IpClient, "3", "Alerta W0000029", "Error en la anulacion de Boarding, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    System.Threading.Thread.Sleep(500);
                    this.lblMensajeError.Text = "Ocurrio un error en el proceso de Anulación";
                    return;
                }
                else
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000030", "004", IpClient, "1", "Alerta W0000030", "Se anularon correctamente los Boarding, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    #region Realizamos la consulta nuevamente
                    ViewState["dtSeleccionados"] = null; //para eliminar los datos anteriores

                    gwvAnularBCBP.Sort("Num_Secuencial_Bcbp", SortDirection.Ascending);
                    #region Creando la tabla para guardar los checkbox seleccionados
                    DataTable dtBCBPSeleccionados = new DataTable();
                    dtBCBPSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                    dtBCBPSeleccionados.Columns.Add("Tipo_Pasajero", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Tipo_Vuelo", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Tipo_Trasbordo", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Cod_Compania", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Dsc_Compania", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Num_Vuelo", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Fch_Vuelo", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Num_Asiento", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Nom_Pasajero", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Tip_Estado", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Num_Secuencial_Bcbp", System.Type.GetType("System.Int64"));
                    dtBCBPSeleccionados.Columns.Add("Motivo", System.Type.GetType("System.String"));
                    dtBCBPSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

                    ViewState["dtSeleccionados"] = dtBCBPSeleccionados;
                    #endregion

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "key", "<script language=\"javascript\">SetNumTotal();</script>", false);
                    BindPagingGrid();
                    #endregion
                    //Buscar();

                    lblErrorMsg.Text = "";
                    hdNumSelTotal.Value = "0";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Prueba", "reinicioSeleccion();", true);

                    omb.ShowMessage("BCBPs se Anularon correctamente", "Anulación de Boarding", "");
                }
            }
            else
            {
                lblMensajeError.Text = "Debe de seleccionar al menos un BCBP para anular.";
            }
        }
    }

    protected void guardarSeleccionesCheckBox(DataTable dt_registros)
    {
        int pageSize = gwvAnularBCBP.Rows.Count;

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
                //TextBox txtMotivo = (TextBox)gwvAnularBCBP.Rows[i].FindControl("txtMotivo");
                CheckBox chkSeleccionar = (CheckBox)gwvAnularBCBP.Rows[i].FindControl("chkSeleccionar");
                string sNroBoarding = gwvAnularBCBP.DataKeys.Count > i ? gwvAnularBCBP.DataKeys[i].Value.ToString() : "";// .FindControl("Num_Secuencial_Bcbp");
                HiddenField lbCodCompania = (HiddenField)gwvAnularBCBP.Rows[i].FindControl("lblCodCompania");
                Label lbDscCompania = (Label)gwvAnularBCBP.Rows[i].FindControl("lblDscCompania");
                Label lbNum_Vuelo = (Label)gwvAnularBCBP.Rows[i].FindControl("lblDscNumVuelo");
                Label lbFch_Vuelo = (Label)gwvAnularBCBP.Rows[i].FindControl("lblDscFechaVuelo");
                Label lblTipoPasajero = (Label)gwvAnularBCBP.Rows[i].FindControl("lblTipoPasajero");
                Label lblTipoVuelo = (Label)gwvAnularBCBP.Rows[i].FindControl("lblTipoVuelo");
                Label lblTipoTrasbordo = (Label)gwvAnularBCBP.Rows[i].FindControl("lblTipoTrasbordo");
                Label lbNum_Asiento = (Label)gwvAnularBCBP.Rows[i].FindControl("lblDscAsiento");
                Label lbNom_Pasajero = (Label)gwvAnularBCBP.Rows[i].FindControl("lblDscPasajero");
                Label lblTip_Estado = (Label)gwvAnularBCBP.Rows[i].FindControl("lblTipEstado");

                if (chkSeleccionar.Checked)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE EL REGISTRO
                    int nroFilas = dt_registros.Rows.Count;
                    DataRow[] rows = dt_registros.Select("Num_Secuencial_Bcbp = " + sNroBoarding + "");
                    int filas = rows.Count();
                    if (filas == 0)
                    {
                        //no existe el registro y lo agregamos
                        dt_registros.Rows.Add(dt_registros.NewRow());
                        dt_registros.Rows[nroFilas]["Numero"] = (nroFilas + 1).ToString();
                        dt_registros.Rows[nroFilas]["Tipo_Pasajero"] = lblTipoPasajero.Text;
                        dt_registros.Rows[nroFilas]["Tipo_Vuelo"] = lblTipoVuelo.Text;
                        dt_registros.Rows[nroFilas]["Tipo_Trasbordo"] = lblTipoTrasbordo.Text;
                        dt_registros.Rows[nroFilas]["Cod_Compania"] = lbCodCompania.Value;
                        /*dt_registros.Rows[nroFilas]["Num_Vuelo"] = gwvAnularBCBP.Rows[i]["Num_Vuelo"].ToString();
                        dt_registros.Rows[nroFilas]["Fch_Vuelo"] = gwvAnularBCBP.Rows[i]["Fch_Vuelo"].ToString();
                        dt_registros.Rows[nroFilas]["Num_Asiento"] = gwvAnularBCBP.Rows[i]["Num_Asiento"].ToString();
                        dt_registros.Rows[nroFilas]["Nom_Pasajero"] = gwvAnularBCBP.Rows[i]["Nom_Pasajero"].ToString();
                        dt_registros.Rows[nroFilas]["Tip_Estado"] = gwvAnularBCBP.Rows[i]["Tip_Estado"].ToString();*/
                        dt_registros.Rows[nroFilas]["Dsc_Compania"] = lbDscCompania.Text;
                        dt_registros.Rows[nroFilas]["Num_Vuelo"] = lbNum_Vuelo.Text;
                        dt_registros.Rows[nroFilas]["Fch_Vuelo"] = lbFch_Vuelo.Text;
                        dt_registros.Rows[nroFilas]["Num_Asiento"] = lbNum_Asiento.Text;
                        dt_registros.Rows[nroFilas]["Nom_Pasajero"] = lbNom_Pasajero.Text;
                        dt_registros.Rows[nroFilas]["Tip_Estado"] = lblTip_Estado.Text;
                        dt_registros.Rows[nroFilas]["Num_Secuencial_Bcbp"] = sNroBoarding;
                        dt_registros.Rows[nroFilas]["Motivo"] = txtMotivo.Text;
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
                    if (sNroBoarding != "")
                    {
                        int filas = dt_registros.Select("Num_Secuencial_Bcbp = " + sNroBoarding + "").Count();
                        if (filas == 1)
                        {
                            DataRow[] foundRow = dt_registros.Select("Num_Secuencial_Bcbp = " + sNroBoarding + "");

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

    protected void gwvAnularBCBP_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);
        gwvAnularBCBP.PageIndex = e.NewPageIndex;
        BindPagingGrid();
        lblTxtSeleccionados.Text = hdNumSelTotal.Value;
    }

    protected void gwvAnularBCBP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //BUSCAMOS EN LA TABLA SELECCIONADOS PARA VER SU ESTADO
            DataTable dt_seleccionados = (DataTable)ViewState["dtSeleccionados"];

            if ((dt_seleccionados != null) && (dt_seleccionados.Rows.Count > 0))
            {
                DataRow[] rows = dt_seleccionados.Select("Num_Secuencial_Bcbp = " + ((System.Data.DataRowView)(e.Row.DataItem)).Row["Num_Secuencial_Bcbp"].ToString() + "");
                int nroFilas = rows.Count();
                if (nroFilas == 1)
                {
                    CheckBox chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = Convert.ToBoolean(rows[0].ItemArray.GetValue(13));
                    //TextBox txtMotivo = (TextBox)e.Row.FindControl("txtMotivo");
                    //txtMotivo.Text = rows[0].ItemArray.GetValue(12).ToString();
                }
            }
        }
    }

    protected void gwvAnularBCBP_Sorting(object sender, GridViewSortEventArgs e)
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
}
