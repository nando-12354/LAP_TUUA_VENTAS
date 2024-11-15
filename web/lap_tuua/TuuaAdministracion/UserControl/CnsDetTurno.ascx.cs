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
using System.Globalization;

public partial class UserControl_CnsDetTurno : System.Web.UI.UserControl
{

    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    protected BO_Consultas objWBConsultas = new BO_Consultas();
    
    DataTable dtDetalleMoneda11 = new DataTable();


    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
    }


    private void CargarDataTableGrillas(string as_Turno, string as_Moneda, string as_Detalle)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            if (as_Detalle == "4") this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetOpeCompra.Text"];
            else if (as_Detalle == "5") this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetOpeVenta.Text"];
            else if (as_Detalle == "6") this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetOpeIngreso.Text"];
            else if (as_Detalle == "7") this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetOpeEgreso.Text"];
            else if (as_Detalle == "2" || as_Detalle == "21" || as_Detalle == "22" || as_Detalle == "23" || as_Detalle == "24" || as_Detalle == "25"
                    || as_Detalle == "3" || as_Detalle == "31" || as_Detalle == "32" || as_Detalle == "33" || as_Detalle == "34" || as_Detalle == "35"
                    || as_Detalle == "9" || as_Detalle == "10" || as_Detalle == "91" || as_Detalle == "92"
                    || as_Detalle == "11" || as_Detalle == "12" || as_Detalle == "14" || as_Detalle == "15")
            {
                this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetTicket.Text"];
            }
            else
                this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetalleTurno.Text"];

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }

        try
        {
           dtDetalleMoneda11 = objWBConsultas.DetalleMonedasTurno(as_Turno, as_Moneda, as_Detalle);        
        }
        catch(Exception ex)
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

    #region Inicio
    public void Inicio(string sTurno, string sMoneda, string sDetalle)
    {
        lblTurno.Text  = sTurno;
        lblMoneda.Text = sMoneda;
        lblDetalle.Text = sDetalle;

        if (sTurno != null || sMoneda != null || sDetalle != null)
        {          
            try
            {
                DataTable dt_parametro = objWBConsultas.ListarParametros("LG");

                if (dt_parametro.Rows.Count > 0)
                {
                    txtValorMaximoGrilla.Text = dt_parametro.Rows[0].ItemArray.GetValue(4).ToString();
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

            try
            {
                CargarDataTableGrillas(sTurno, sMoneda, sDetalle);

                if (dtDetalleMoneda11.Rows.Count > 0)
                {
                    if (sDetalle == "2" || sDetalle == "21" || sDetalle == "22" || sDetalle == "23" || sDetalle == "24" || sDetalle == "25"
                        || sDetalle == "3" || sDetalle == "31" || sDetalle == "32" || sDetalle == "33" || sDetalle == "34" || sDetalle == "35"
                        || sDetalle == "9" || sDetalle == "10" || sDetalle == "91" || sDetalle == "92" 
                        || sDetalle == "11" || sDetalle == "12" || sDetalle == "14" || sDetalle == "15")
                    {
                        this.lblDetalleTurno.Text = (string)htLabels["mconsultaSubDetalleTurno.lblDetTicket.Text"];
                        this.lblMensajeError.Text = "";
                        grvSubDetalleTurno1.DataSource = dtDetalleMoneda11;
                        grvSubDetalleTurno1.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                        grvSubDetalleTurno1.DataBind();
                        ViewState["DetalleTurno"] = dtDetalleMoneda11;
                        grvSubDetalleTurno1.Visible = true;
                        grvSubDetalleTurno2.Visible = false;
                    }
                    else
                    {
                        if (sDetalle == "4" || sDetalle == "5" || sDetalle == "6" || sDetalle == "7")
                        {

                            if (sDetalle == "4" || sDetalle == "5")
                            {
                                //Compra y Venta de Moneda
                                this.lblMensajeError.Text = "";
                                grvSubDetalleTurno2.DataSource = dtDetalleMoneda11;
                                
                                grvSubDetalleTurno2.Columns[2].Visible = true;
                                grvSubDetalleTurno2.Columns[5].Visible = true;
                                grvSubDetalleTurno2.Columns[4].Visible = true;
                                grvSubDetalleTurno2.Columns[6].HeaderText = "Imp. a Cambiar";
                                grvSubDetalleTurno2.Columns[6].Visible = true;
                                grvSubDetalleTurno2.Columns[7].Visible = true;
                                ViewState["DetalleTurnoSub"] = dtDetalleMoneda11;
                                grvSubDetalleTurno2.Visible = true;
                                grvSubDetalleTurno1.Visible = false;
                            }
                            else
                            {
                                //Ingreso y Egreso de Caja
                                this.lblMensajeError.Text = "";
                                grvSubDetalleTurno2.DataSource = dtDetalleMoneda11;
                                
                                grvSubDetalleTurno2.Columns[2].Visible=false;
                                grvSubDetalleTurno2.Columns[5].Visible = false;
                                grvSubDetalleTurno2.Columns[6].HeaderText = "Imp. Operación";
                                grvSubDetalleTurno2.Columns[7].Visible = false;
                                
                                ViewState["DetalleTurnoSub"] = dtDetalleMoneda11;
                                grvSubDetalleTurno2.Visible = true;
                                grvSubDetalleTurno1.Visible = false;
                            }
                            grvSubDetalleTurno2.DataBind();
                        }
                        else
                        {
                            lblMensajeError.Text = "No contiene detalle";
                            grvSubDetalleTurno1.Visible = false;
                            grvSubDetalleTurno2.Visible = false;
                        }
                    } 
                }
                else
                {
                    this.lblMensajeError.Text = "No contiene detalle";
                    grvSubDetalleTurno1.Visible = false;
                    grvSubDetalleTurno2.Visible = false;
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
        mpext.Show();
    }
    #endregion



    /*protected void grvSubDetalleTurno1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        this.grvSubDetalleTurno1.DataSource = dwConsulta((DataTable)ViewState["DetalleTurno"], this.txtColumna.Text, txtOrdenacion.Text);
        this.grvSubDetalleTurno1.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvSubDetalleTurno1.PageIndex = e.NewPageIndex;
        grvSubDetalleTurno1.DataBind();
    }*/

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }


    protected void grvSubDetalleTurno1_Sorting(object sender, GridViewSortEventArgs e)
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
            this.grvSubDetalleTurno1.DataSource = dwConsulta((DataTable)ViewState["DetalleTurno"], sortExpression, direction);
            this.grvSubDetalleTurno1.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvSubDetalleTurno1.DataBind();
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


    /*protected void grvSubDetalleTurno2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grvSubDetalleTurno2.DataSource = dwConsulta((DataTable)ViewState["DetalleTurnoSub"], this.txtColumna.Text, txtOrdenacion.Text);
        grvSubDetalleTurno2.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvSubDetalleTurno2.PageIndex = e.NewPageIndex;
        grvSubDetalleTurno2.DataBind();
    }*/

    protected void grvSubDetalleTurno2_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;

            this.txtOrdenacion.Text = "DESC";
            SortGridViewSub(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;

            this.txtOrdenacion.Text = "ASC";
            SortGridViewSub(sortExpression, "ASC");
        }
        this.txtColumna.Text = sortExpression;
    }


    private void SortGridViewSub(string sortExpression, String direction)
    {
        try
        {
            grvSubDetalleTurno2.DataSource = dwConsulta((DataTable)ViewState["DetalleTurnoSub"], sortExpression, direction);
            grvSubDetalleTurno2.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvSubDetalleTurno2.DataBind();
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
