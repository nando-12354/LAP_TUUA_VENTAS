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

using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;

using System.Globalization;
using LAP.TUUA.ALARMAS;
public partial class Ope_VerTasaCambio : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    BO_Administracion objBOOperacion = new BO_Administracion(Define.CNX_12);//BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    DataTable dt_TipoOperacion = new DataTable();

    DataTable dtTemp = new DataTable("dtTemporal");
    
    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                lblTCA.Text = htLabels["mconsultaVerTasaCambio.lblTCA.Text"].ToString();
                lblTCH.Text = htLabels["mconsultaVerTasaCambio.lblTCH.Text"].ToString();
                lblTCP.Text = htLabels["mconsultaVerTasaCambio.lblTCP.Text"].ToString();
                lblFchIni.Text = htLabels["mconsultaVerTasaCambio.lblFchIni.Text"].ToString();
                lblFchFin.Text = htLabels["mconsultaVerTasaCambio.lblFchFin.Text"].ToString();
                lblTipoOperacion.Text = htLabels["mconsultaVerTasaCambio.lblTipoOperacion.Text"].ToString();
                btnConsultar.Text = htLabels["mconsultaVerTasaCambio.btnConsultar.Text"].ToString();

                this.txtFchIni.Text = DateTime.Now.ToShortDateString();
                this.txtFchFin.Text = DateTime.Now.ToShortDateString();
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

            //Carga combo Tipo operación     
            dt_TipoOperacion = objBOConsultas.ListaCamposxNombre("TipoTasaCambio");
            objCargaCombo.LlenarCombo(ddlTipoOperacion, dt_TipoOperacion, "Cod_Campo", "Dsc_Campo", true, false);

            if (!IsPostBack)
            {
                this.btnIngresar.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_OpeCrearTasaCambio, Define.OPC_INGRESAR);
            }

            try
            {
                Flg_Error = false;
                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                }
                LoadTasaCambio();
                LoadTasaCambioHist();
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
    }    

    protected void LoadTasaCambio()
    {
        DataTable dtTasaCambio = new DataTable();
        dtTasaCambio = objBOOperacion.ObtenerTasaCambio("");
        ViewState["TasaCambio"] = dtTasaCambio; 
        DataView dvTCA = new DataView(dtTasaCambio);
        dvTCA.RowFilter = "Tip_Estado = '1'";
        
        DataView dvTCP = new DataView(dtTasaCambio);
        dvTCP.RowFilter = "Tip_Estado = '0'";

        this.grvTasaCambio.DataSource = dvTCA;
        this.grvTasaCambio.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvTasaCambio.DataBind();

        this.grvTasaCambioProg.DataSource = dvTCP;
        this.grvTasaCambioProg.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvTasaCambioProg.DataBind();

        if (dvTCP.Count < 1)
        {
            this.lblErrorTCP.Visible = true;
            this.lblErrorTCP.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else {
            this.lblErrorTCP.Visible = false;
        }
        if (dvTCA.Count < 1)
        {
            this.lblErrorTCA.Visible = true;
            this.lblErrorTCA.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else {
            this.lblErrorTCA.Visible = false;
        }
    }
    
    protected void LoadTasaCambioHist()
    {
        LoadDataTable();
        ViewState["TasaCambioHist"] = dtTemp;
        this.grvTasaCambioHist.DataSource = dtTemp;
        this.grvTasaCambioHist.PageIndex = 0;
        this.grvTasaCambioHist.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvTasaCambioHist.DataBind();
        
        if (dtTemp.Rows.Count < 1)
        {
            this.lblErrorTCH.Visible = true;
            this.lblErrorTCH.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblErrorTCH.Visible = false;
        }
    }

    protected void LoadDataTable()
    {
        DataTable dtTasaCambioHist = dtTasaCambioHist = objBOOperacion.ObtenerTasaCambioHist("");
        //ViewState["TasaCambioHist"] = dtTasaCambioHist;

        string fechaIni =  "0"+this.txtFchIni.Text;
        string fechaFin = "0"+this.txtFchFin.Text;

        fechaIni = fechaIni.Substring(fechaIni.Length - 10, 10);
        fechaFin = fechaFin.Substring(fechaFin.Length - 10, 10);
        //string fechaIni = Fecha.convertToFechaSQL(this.txtFchIni.Text) + " 00:00:00";
        //string fechaFin = Fecha.convertToFechaSQL(this.txtFchFin.Text) + " 23:59:59";
        string tipoOperacion = this.ddlTipoOperacion.SelectedValue;

        fechaIni = fechaIni.Substring(3, 2) + "/" + fechaIni.Substring(0, 2) + "/" + fechaIni.Substring(6, 4) + " 00:00:00";
        fechaFin = fechaFin.Substring(3, 2) + "/" + fechaFin.Substring(0, 2) + "/" + fechaFin.Substring(6, 4) + " 23:59:59";

        string RowFilter = "(( Fch_Ini <= '" + fechaIni + "' AND '" + fechaIni + "' <= Fch_Fin ) OR ( '" + fechaIni + "' <= Fch_Ini AND Fch_Ini <= '" + fechaFin + "' ))";

        if (ddlTipoOperacion.SelectedValue != "0")
        {
            RowFilter += " AND Tip_Cambio = '" + ddlTipoOperacion.SelectedValue + "'";
        }

        DataRow[] foundRowMoneda = (RowFilter.Length > 0) ? dtTasaCambioHist.Select(RowFilter) : dtTasaCambioHist.Select();

        foreach (DataColumn col in dtTasaCambioHist.Columns)
        {
            if (col.ColumnName == "Imp_Valor")
            {
                dtTemp.Columns.Add(col.ColumnName, System.Type.GetType("System.Decimal"));
            }
            else
            {
                dtTemp.Columns.Add(col.ColumnName);
            }
        }

        for (int i = 0; i < foundRowMoneda.Length; i++)
            dtTemp.Rows.Add(dtTemp.NewRow());

        for (int i = 0; i < foundRowMoneda.Length; i++)
        {

            DataRow dr = foundRowMoneda[i];
            dr[0].ToString();

            dtTemp.Rows[i][0] = dr[0].ToString();
            dtTemp.Rows[i][1] = dr[1].ToString();
            dtTemp.Rows[i][2] = dr[2].ToString();
            dtTemp.Rows[i][3] = Convert.ToDecimal(dr[3]);
            dtTemp.Rows[i][4] = dr[4].ToString();
            dtTemp.Rows[i][5] = dr[5].ToString();
            dtTemp.Rows[i][6] = dr[15].ToString();
            dtTemp.Rows[i][7] = dr[16].ToString();
            dtTemp.Rows[i][8] = dr[8].ToString();
            dtTemp.Rows[i][9] = dr[9].ToString();
            dtTemp.Rows[i][10] = dr[10].ToString();
            dtTemp.Rows[i][11] = dr[11].ToString();
            dtTemp.Rows[i][12] = dr[12].ToString();
            dtTemp.Rows[i][13] = dr[13].ToString();
            dtTemp.Rows[i][14] = dr[14].ToString();
            dtTemp.Rows[i][15] = FormateoFch(dr[15].ToString());
            dtTemp.Rows[i][16] = FormateoFch(dr[16].ToString());
        }
    }

    protected string FormateoFch(string sValor)
    {
        string sRespuesta = string.Empty;
        try
        {
            sRespuesta = sValor.Substring(0, 10).Split('/')[2] + 
                         sValor.Substring(0, 10).Split('/')[1] + 
                         sValor.Substring(0, 10).Split('/')[0];

            sRespuesta += sValor.Substring(10, 9).Split(':')[0] + 
                          sValor.Substring(10, 9).Split(':')[1] + 
                          sValor.Substring(10, 9).Split(':')[2];

        }
        catch (Exception ex)
        {

        }
        return sRespuesta;

    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        LoadTasaCambioHist();
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ope_CrearTasaCambio.aspx");
    }

    #region Sorting
    protected void grvTasaCambioHist_Sorting(object sender, GridViewSortEventArgs e)
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
            this.grvTasaCambioHist.DataSource = dwConsulta((DataTable)ViewState["TasaCambioHist"], sortExpression, direction); ;
            this.grvTasaCambioHist.DataBind();

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
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }

    protected DataView dwConsulta(DataView dwConsulta, string sortExpression, String direction)
    {
        DataView dv = dwConsulta;

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
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
    #endregion

    #region Page Index Changing
    protected void grvTasaCambio_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //int pageSize = grvTCP.PageSize;
        /*DataTable dtTasaCambio = (DataTable)ViewState["TasaCambio"];
        DataView dtTCP = new DataView(dtTasaCambio);
        dtTCP.RowFilter = "Tip_Estado = '0'";
        grvTCP.DataSource = dtTCP;
        this.grvTCP.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTCP.PageIndex = e.NewPageIndex;
        grvTCP.DataBind();*/

        try
        {
            Flg_Error = false;
            DataTable dtTasaCambio = (DataTable)ViewState["TasaCambio"];
            DataView dvTCA = new DataView(dtTasaCambio);
            dvTCA.RowFilter = "Tip_Estado = '1'";
            
            this.grvTasaCambio.DataSource = dwConsulta(dvTCA, this.txtColumna.Text, txtOrdenacion.Text);
            this.grvTasaCambio.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvTasaCambio.PageIndex = e.NewPageIndex;
            grvTasaCambio.DataBind();
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

    protected void grvTasaCambioProg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //int pageSize = grvTCP.PageSize;
        /*DataTable dtTasaCambio = (DataTable)ViewState["TasaCambio"];
        DataView dtTCP = new DataView(dtTasaCambio);
        dtTCP.RowFilter = "Tip_Estado = '0'";
        grvTCP.DataSource = dtTCP;
        this.grvTCP.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTCP.PageIndex = e.NewPageIndex;
        grvTCP.DataBind();*/

        try
        {
            Flg_Error = false;
            DataTable dtTasaCambio = (DataTable)ViewState["TasaCambio"];
            DataView dvTCP = new DataView(dtTasaCambio);
            dvTCP.RowFilter = "Tip_Estado = '0'";
            
            this.grvTasaCambioProg.DataSource = dwConsulta(dvTCP, this.txtColumna.Text, txtOrdenacion.Text);
            this.grvTasaCambioProg.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvTasaCambioProg.PageIndex = e.NewPageIndex;
            grvTasaCambioProg.DataBind();
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

    protected void grvTasaCambioHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Flg_Error = false;
            this.grvTasaCambioHist.DataSource = dwConsulta((DataTable)ViewState["TasaCambioHist"], this.txtColumna.Text, txtOrdenacion.Text);
            this.grvTasaCambioHist.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text); 
            grvTasaCambioHist.PageIndex = e.NewPageIndex;
            grvTasaCambioHist.DataBind();
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
    #endregion

    #region grvTasaCambioProg_RowCommand

    protected void grvTasaCambioProg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            string codTasaToDelete = e.CommandArgument.ToString();

            try
            {
                Int64 idTxCritica = objBOOperacion.obtenerIdTransaccionCritica();

                objBOOperacion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"],
                    (string)Session["Cod_SubModulo"], Define.CNX_12);

                TasaCambio objTasaCambio = new TasaCambio();
                objTasaCambio.SCodTasaCambio = codTasaToDelete;
                objTasaCambio.IdTxCritica = idTxCritica;


                //if (objBOOperacion.EliminarTasaCambioCrit(codTasaToDelete))
                if (objBOOperacion.EliminarTasaCambioCrit(objTasaCambio))
                {
                    string strMessage = "Se eliminó la tasa " + codTasaToDelete + " satisfactoriamente...";
                    string IpClient = Request.UserHostAddress;
                    string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));

                    GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000083", "004", IpClient, "1",
                        "Alerta W0000083", strMessage + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]),
                        Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "TasaCambio", pathMap);

                    LoadTasaCambio();
                    
                    omb.ShowMessage(strMessage, "Gestión Tasa de Cambio", "Ope_VerTasaCambio.aspx");
                }
                else
                {
                    string strMessage = "Error. No se pudo eliminar la tasa " + codTasaToDelete + " ...";
                    omb.ShowMessage(strMessage, "Gestión Tasa de Cambio", "Ope_VerTasaCambio.aspx");
                }
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_511;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_511].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                    Response.Redirect("PaginaError.aspx");
            }
        }
    }

    private string getPathMap(SiteMapNode oSMN)
    {
        string pathMap = string.Empty;

        if (oSMN.ParentNode == null)
        {
            pathMap = oSMN.Title;
        }
        else
        {
            pathMap = getPathMap(oSMN.ParentNode) + " : " + oSMN.Title;
        }
        return pathMap;
    }
    #endregion
}
