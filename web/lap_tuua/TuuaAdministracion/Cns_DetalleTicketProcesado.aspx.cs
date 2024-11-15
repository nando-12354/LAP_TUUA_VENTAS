using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.CONVERSOR;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using Define = LAP.TUUA.UTIL.Define;

public partial class Cns_DetalleTicketProcesado : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;

    protected BO_Consultas objBOConsulta = new BO_Consultas();
    protected BO_Seguridad objBOSeguridad = new BO_Seguridad();

    Int32 valorMaxGrilla;
    DataTable dt_parametroGeneral = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                htLabels = LabelConfig.htLabels;
                this.lblCajero.Text = htLabels["consDetalleTicketProcesado.lblCajero"].ToString();
                CargarGrillaTickets();
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

    /// <summary>
    /// Carga la grilla de resultados 
    /// </summary>
    protected void CargarGrillaTickets()
    {
        try
        {
            DataTable dt_consulta = objBOConsulta.ListarTicketProcesado(Request.QueryString["idturno"]);
            Usuario objUsuario = objBOSeguridad.obtenerUsuario(Convert.ToString(Request.QueryString["idusuario"]));
            this.hdTurno.Value = Convert.ToString(Request.QueryString["idturno"]);
            this.lblCajeroRpt.Text = objUsuario.SApeUsuario + " " + objUsuario.SNomUsuario + "  ( " + objUsuario.SCodUsuario + " )";
            this.hdCajero.Value = this.lblCajeroRpt.Text;
            if (dt_consulta.Rows.Count < 1)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeError.Text = htLabels["consDetalleTicketProcesado.lblMensajeError.Text"].ToString();
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
                grvTicketProcesado.DataSource = null;
                grvTicketProcesado.DataBind();
            }
            else
            {

                 /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeError.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                    grvTicketProcesado.DataSource = null;
                    grvTicketProcesado.DataBind();
                }
                else
                {
                    ViewState["tablaTicket"] = dt_consulta;
                    ValidarTamanoGrilla();

                    //Cargar datos en la grilla
                    this.lblMensajeError.Text = "";
                    this.lblTotal.Text = htLabels["consDetalleTicketProcesado.lblTotal"].ToString() + " " +
                                         dt_consulta.Rows.Count;
                    this.grvTicketProcesado.DataSource = dt_consulta;
                    this.grvTicketProcesado.AllowPaging = true;
                    this.grvTicketProcesado.PageSize = valorMaxGrilla;
                    this.grvTicketProcesado.DataBind();
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

    /// <summary>
    /// //Traer valor de tamaño de la grilla desde parametro general    
    /// </summary>
    void ValidarTamanoGrilla()
    {
        dt_parametroGeneral = objBOConsulta.ListarParametros("LG");
        if (dt_parametroGeneral.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroGeneral.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected void grvTicketProcesado_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            String codTicket = e.CommandArgument.ToString();
            //ConsDetTicket1.Inicio(codTicket.Trim() + "-" + codTicket.Trim());
            ConsDetTicket1.Inicio(codTicket.Trim());
        }
    }
    

    #region Ordenacion y Cambio de Pagina
    protected void grvTicketProcesado_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageSize = grvTicketProcesado.PageSize;
        DataTable dtTurnos = (DataTable)ViewState["tablaTicket"];

        grvTicketProcesado.PageIndex = e.NewPageIndex;
        grvTicketProcesado.DataSource = dtTurnos;
        grvTicketProcesado.DataBind();

    }

    protected void grvTicketProcesado_Sorting(object sender, GridViewSortEventArgs e)
    {
        DataTable dtTurnos = (DataTable)ViewState["tablaTicket"];
        if (dtTurnos == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;
        //Truco para que en la paginacion no este haciendo sort tambien. Esto porque necesito guardar el estado del checkbox..seria muy complicado.
        ViewState["tablaTicket"] = SortDataTable(dtTurnos, false).ToTable();
        //reactualizo la tabla
        dtTurnos = (DataTable)ViewState["tablaTicket"];
        grvTicketProcesado.DataSource = (DataTable)ViewState["tablaTicket"];
        grvTicketProcesado.DataBind();
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }

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
    #endregion

    #region Change Color of GridView
    protected void grvTicketProcesado_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
    }
    #endregion
}
