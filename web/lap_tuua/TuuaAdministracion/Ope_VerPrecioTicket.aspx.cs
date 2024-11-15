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
using System.Globalization;


using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;

public partial class Ope_VerPrecioTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    BO_Administracion objBOOperacion = new BO_Administracion(Define.CNX_13); //BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Operacion objBOOpera;
   
    DataTable dtTemp = new DataTable("dtTemporal");

    protected void Page_Load(object sender, EventArgs e)
    {
        objBOOpera = new BO_Operacion();
        htLabels = LabelConfig.htLabels;
        if (!IsPostBack)
        {
            try
            {
                lblPTA.Text = "Precio Tickets Actual";
                lblTCP.Text = "Precio Ticket Programado";
                lblPrecioTicketHist.Text = "Precio Tickets Histórico";
                lblFchIni.Text = "Desde: ";
                lblFchFin.Text = "Hasta: ";

                this.btnIngresar.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_OpeCrearPrecioTicket, Define.OPC_INGRESAR);

                this.txtFchIni.Text = DateTime.Now.ToShortDateString();
                this.txtFchFin.Text = DateTime.Now.ToShortDateString();

                //DataTable dtResultado = objBOOpera.ListarTurnosAbiertos(string.Empty);
                //if (dtResultado.Rows.Count > 0)
                //{
                //    lblInfo.Text = lblInfo.Text = htLabels["opeVentaCredito.msgTurnoPendiente"].ToString();
                //    btnIngresar.Enabled = false;
                //    PnlFormulario.Visible = false;
                //    btnIngresar.Visible = false;
                //    return;
                //}

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

            try
            {
                Flg_Error = false;
                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                }
                LoadPrecioTicket();
                LoadPrecioTicketHist();
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

    protected void LoadPrecioTicket()
    {
        DataTable dtPrecioTicket = new DataTable();
        dtPrecioTicket = objBOOperacion.ObtenerPrecioTicket("");
        //ViewState["PrecioTicket"] = dtPrecioTicket;

        DataView dvPTA = new DataView(dtPrecioTicket);
        dvPTA.RowFilter = "Tip_Estado = '1'";
        DataView dvPTP = new DataView(dtPrecioTicket);
        dvPTP.RowFilter = "Tip_Estado = '0'";
        dvPTP.Sort = "Fch_Programacion DESC";

        this.grvPrecioTicket.DataSource = dvPTA;
        ViewState["PrecioTicket"] = dtPrecioTicket;
        this.grvPrecioTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvPrecioTicket.DataBind();

        this.grvPTP.DataSource = dvPTP;
        this.grvPTP.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        this.grvPTP.DataBind();

        //Precio Ticket Actual
        if (dvPTA.Count < 1)
        {
            this.lblMensajeErrorPTA.Visible = true;
            this.lblMensajeErrorPTA.Text = htLabels["tuua.general.lblNoRegistros"].ToString();            
        }
        else
        {
            this.lblMensajeErrorPTA.Visible = false;            
        }
        //Precio Ticket Programado
        if (dvPTP.Count < 1)
        {
            this.lblMensajeErrorPTP.Visible = true;
            this.lblMensajeErrorPTP.Text = htLabels["tuua.general.lblNoRegistros"].ToString();            
        }
        else
        {
            this.lblMensajeErrorPTP.Visible = false; 
        }
    }

    protected void LoadPrecioTicketHist()
    {
        LoadDataTable();
        ViewState["PrecioTicketHist"] = dtTemp;

        this.grvPrecioTicketHist.DataSource = dtTemp;
        this.grvPrecioTicketHist.DataBind();
        if (dtTemp.Rows.Count < 1)
        {
            this.lblMensajeErrorPTH.Visible = true;
            this.lblMensajeErrorPTH.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeErrorPTH.Visible = false;
        }
    }

    protected void LoadDataTable()
    {
        DataTable dtPrecioTicketHist = dtPrecioTicketHist = objBOOperacion.ObtenerPrecioTicketHist("");
        ViewState["PrecioTicketHist"] = dtPrecioTicketHist;

        string fechaIni = "0" + this.txtFchIni.Text;
        string fechaFin = "0" + this.txtFchFin.Text;

        fechaIni = fechaIni.Substring(fechaIni.Length - 10, 10);
        fechaFin = fechaFin.Substring(fechaFin.Length - 10, 10);
        //string fechaIni = Fecha.convertToFechaSQL(this.txtFchIni.Text) + " 00:00:00";
        //string fechaFin = Fecha.convertToFechaSQL(this.txtFchFin.Text) + " 23:59:59";

        fechaIni = fechaIni.Substring(3, 2) + "/" + fechaIni.Substring(0, 2) + "/" + fechaIni.Substring(6, 4) + " 00:00:00";
        fechaFin = fechaFin.Substring(3, 2) + "/" + fechaFin.Substring(0, 2) + "/" + fechaFin.Substring(6, 4) + " 23:59:59";

        string RowFilter = "(( Fch_Ini <= '" + fechaIni + "' AND '" + fechaIni + "' <= Fch_Fin ) OR ( '" + fechaIni + "' <= Fch_Ini AND Fch_Ini <= '" + fechaFin + "' ))";

        DataRow[] foundRowMoneda = (RowFilter.Length > 0) ? dtPrecioTicketHist.Select(RowFilter) : dtPrecioTicketHist.Select();        

        foreach (DataColumn col in dtPrecioTicketHist.Columns)
        {
            if (col.ColumnName == "Imp_Precio")
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
            
            dtTemp.Rows[i][0] = dr[0].ToString();
            dtTemp.Rows[i][1] = dr[1].ToString();
            dtTemp.Rows[i][2] = dr[2].ToString();
            dtTemp.Rows[i][3] = Convert.ToDecimal(dr[3]);
            dtTemp.Rows[i][4] = dr[4].ToString();
            dtTemp.Rows[i][5] = dr[5].ToString();
            dtTemp.Rows[i][6] = dr[17].ToString();
            dtTemp.Rows[i][7] = dr[18].ToString();
            dtTemp.Rows[i][8] = dr[8].ToString();
            dtTemp.Rows[i][9] = dr[9].ToString();
            dtTemp.Rows[i][10] = dr[10].ToString();
            dtTemp.Rows[i][11] = dr[11].ToString();
            dtTemp.Rows[i][12] = dr[12].ToString();
            dtTemp.Rows[i][13] = dr[13].ToString();

            dtTemp.Rows[i][14] = dr[14].ToString();
            dtTemp.Rows[i][15] = dr[15].ToString();
            dtTemp.Rows[i][16] = dr[16].ToString();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        LoadPrecioTicketHist();
    }

    protected void btnIngresar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Ope_CrearPrecioTicket.aspx");
    }

    #region Sorting
    protected void grvPrecioTicketHist_Sorting(object sender, GridViewSortEventArgs e)
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
            //LoadDataTable();
            this.grvPrecioTicketHist.DataSource = dwConsulta((DataTable)ViewState["PrecioTicketHist"], sortExpression, direction);
            this.grvPrecioTicketHist.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            this.grvPrecioTicketHist.DataBind();
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

    #region PageIndexChanging
    protected void grvPrecioTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataTable dtPrecioTicket = (DataTable)ViewState["PrecioTicket"];
            DataView dvPTA = new DataView(dtPrecioTicket);
            dvPTA.RowFilter = "Tip_Estado = '1'";

            Flg_Error = false;
            this.grvPrecioTicket.DataSource = dwConsulta(dvPTA, this.txtColumna.Text, txtOrdenacion.Text);
            this.grvPrecioTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvPrecioTicket.PageIndex = e.NewPageIndex;
            grvPrecioTicket.DataBind();
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

    protected void grvPrecioTicketProg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataTable dtPrecioTicket = (DataTable)ViewState["PrecioTicket"];
            DataView dvPTP = new DataView(dtPrecioTicket);
            dvPTP.RowFilter = "Tip_Estado = '0'";

            Flg_Error = false;
            this.grvPTP.DataSource = dwConsulta(dvPTP, this.txtColumna.Text, txtOrdenacion.Text);
            this.grvPTP.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvPTP.PageIndex = e.NewPageIndex;
            grvPTP.DataBind();  
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
    
    protected void grvPrecioTicketHist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Flg_Error = false;
            this.grvPrecioTicketHist.DataSource = dwConsulta((DataTable)ViewState["PrecioTicketHist"], this.txtColumna.Text, txtOrdenacion.Text);
            this.grvPrecioTicketHist.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvPrecioTicketHist.PageIndex = e.NewPageIndex;
            grvPrecioTicketHist.DataBind();
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

    #region grvPrecioTicketProg_RowCommand

    protected void grvPrecioTicketProg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            string codPrecioToDelete = e.CommandArgument.ToString();

            try
            {
                Int64 idTxCritica = objBOOperacion.obtenerIdTransaccionCritica();

                objBOOperacion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], 
                    (string)Session["Cod_SubModulo"], Define.CNX_13);
                
                PrecioTicket oPrecioTicket = new PrecioTicket();
                oPrecioTicket.SCodPrecioTicket = codPrecioToDelete;
                oPrecioTicket.IdTxCritica = idTxCritica;

                if (objBOOperacion.EliminarPrecioTicketCrit(oPrecioTicket))
                {
                    string strMessage = "Se eliminó el precio " + codPrecioToDelete + " satisfactoriamente...";
                    string IpClient = Request.UserHostAddress;
                    string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));

                    GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000084", "004", IpClient, "1",
                        "Alerta W0000084", strMessage + ".<br> Usuario: " + Convert.ToString(Session["Cod_Usuario"]),
                        Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "TipoTicket", pathMap);

                    LoadPrecioTicket();
                    
                    omb.ShowMessage(strMessage, "Gestión Precio de Ticket", "Ope_VerPrecioTicket.aspx");
                }
                else {
                    string strMessage = "Error. No se pudo eliminar el precio " + codPrecioToDelete + " ...";
                    omb.ShowMessage(strMessage, "Gestión Precio de Ticket", "Ope_VerPrecioTicket.aspx");
                }
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_510;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_510].ToString(), ex.Message);
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

