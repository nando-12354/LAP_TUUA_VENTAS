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



public partial class Modulo_Consultas_DetalleTurnos : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;

    protected void Page_Load(object sender, EventArgs e)
    {        
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

        htLabels = LabelConfig.htLabels;
        try
        {            
            this.lblCodTurno.Text = htLabels["mconsultaDetTurno.lblCodTurno.Text"].ToString();
            this.lblEquipo.Text = htLabels["mconsultaDetTurno.lblEquipo.Text"].ToString();
            this.lblUsuario.Text = htLabels["mconsultaDetTurno.lblUsuario.Text"].ToString();
            this.lblFchHoraIni.Text = htLabels["mconsultaDetTurno.lblFchHoraIni.Text"].ToString();
            this.lblFchHoraFin.Text = htLabels["mconsultaDetTurno.lblFchHoraFin.Text"].ToString();
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

       
        //Establecer datatable con datos del estado de compañia
        BO_Consultas objListaAllTurno = new BO_Consultas();
        DataTable dt_listaturno = new DataTable();
        try
        {
            dt_listaturno = objListaAllTurno.ConsultaAllTurno(Request.QueryString["id"]);
        }
        catch (Exception ex)
        {
              Response.Redirect("PaginaError.aspx");
        }

        this.lblDetCodTurno.Text = dt_listaturno.Rows[0].ItemArray.GetValue(0).ToString();
        this.lblDetEquipo.Text = dt_listaturno.Rows[0].ItemArray.GetValue(6).ToString();
        this.lblDetUsuario.Text = dt_listaturno.Rows[0].ItemArray.GetValue(1).ToString();
        this.lblDetFchHoraIni.Text = dt_listaturno.Rows[0].ItemArray.GetValue(2).ToString();
        this.lblDetFchHoraFin.Text = dt_listaturno.Rows[0].ItemArray.GetValue(3).ToString();
        this.hdTurno.Value = dt_listaturno.Rows[0].ItemArray.GetValue(0).ToString();
        CargarGrillas();

    }

    private void CargarGrillas()
    {
        try
        {

            //Establecer datatable con datos del estado de compañia
            BO_Consultas objCantidadMonedasTurno = new BO_Consultas();
            DataTable dt_cantidadmonedaturno = new DataTable();
            dt_cantidadmonedaturno = objCantidadMonedasTurno.CantidadMonedasTurno(Request.QueryString["id"]);

            BO_Consultas objDetalleMonedaTurno = new BO_Consultas();
            DataTable dtDetalleMoneda1 = new DataTable();
            DataTable dtDetalleMoneda2 = new DataTable();
            DataTable dtDetalleMoneda3 = new DataTable();
            DataTable dtDetalleMoneda4 = new DataTable();
            DataTable dtDetalleMoneda5 = new DataTable();
            DataTable dtDetalleMoneda6 = new DataTable();
            DataTable dtDetalleMoneda7 = new DataTable();
            DataTable dtDetalleMoneda8 = new DataTable();
            DataTable dtDetalleMoneda9 = new DataTable();
            DataTable dtDetalleMoneda10 = new DataTable();
            DataTable dtDetalleMoneda11 = new DataTable();
            DataTable dtDetalleMoneda12 = new DataTable();
            DataTable dtDetalleMoneda13 = new DataTable();
            DataTable dtDetalleMoneda14 = new DataTable();
            DataTable dtDetalleMoneda15 = new DataTable();

            if (dt_cantidadmonedaturno.Rows.Count > 0)
            {

                for (int i = 0; i < dt_cantidadmonedaturno.Rows.Count; i++)
                {
                    string TipMoneda = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(0).ToString();

                    if (i == 0)
                    {
                        lblMoneda1.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda1 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda,null);
                        ViewState["Moneda1"] = dtDetalleMoneda1;
                    }
                    if (i == 1)
                    {
                        lblMoneda2.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda2 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda2"] = dtDetalleMoneda2;
                    }

                    if (i == 2)
                    {
                        lblMoneda3.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda3 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda3"] = dtDetalleMoneda3;
                    }

                    if (i == 3)
                    {
                        lblMoneda4.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda4 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda4"] = dtDetalleMoneda4;
                    }

                    if (i == 4)
                    {
                        lblMoneda5.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda5 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda5"] = dtDetalleMoneda5;
                    }

                    if (i == 5)
                    {
                        lblMoneda6.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda6 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda6"] = dtDetalleMoneda6;
                    }

                    if (i == 6)
                    {
                        lblMoneda7.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda7 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda7"] = dtDetalleMoneda7;
                    }

                    if (i == 7)
                    {
                        lblMoneda8.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda8 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda8"] = dtDetalleMoneda8;
                    }

                    if (i == 8)
                    {
                        lblMoneda9.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda9 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda9"] = dtDetalleMoneda9;
                    }

                    if (i == 9)
                    {
                        lblMoneda10.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda10 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda10"] = dtDetalleMoneda10;
                    }

                    if (i == 10)
                    {
                        lblMoneda11.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda11 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda11"] = dtDetalleMoneda11;
                    }

                    if (i == 11)
                    {
                        lblMoneda12.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda12 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda12"] = dtDetalleMoneda12;
                    }

                    if (i == 12)
                    {
                        lblMoneda13.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda13 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda13"] = dtDetalleMoneda13;
                    }

                    if (i == 13)
                    {
                        lblMoneda14.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda14 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda14"] = dtDetalleMoneda14;
                    }

                    if (i == 14)
                    {
                        lblMoneda15.Text = dt_cantidadmonedaturno.Rows[i].ItemArray.GetValue(1).ToString();
                        dtDetalleMoneda15 = objDetalleMonedaTurno.DetalleMonedasTurno(Request.QueryString["id"], TipMoneda, null);
                        ViewState["Moneda15"] = dtDetalleMoneda15;
                    }

                }

                grvMoneda1.DataSource = dtDetalleMoneda1;
                grvMoneda1.DataBind();

                grvMoneda2.DataSource = dtDetalleMoneda2;
                grvMoneda2.DataBind();

                grvMoneda3.DataSource = dtDetalleMoneda3;
                grvMoneda3.DataBind();


                grvMoneda4.DataSource = dtDetalleMoneda4;
                grvMoneda4.DataBind();

                grvMoneda5.DataSource = dtDetalleMoneda5;
                grvMoneda5.DataBind();

                grvMoneda6.DataSource = dtDetalleMoneda6;
                grvMoneda6.DataBind();

                grvMoneda7.DataSource = dtDetalleMoneda7;
                grvMoneda7.DataBind();

                grvMoneda8.DataSource = dtDetalleMoneda8;
                grvMoneda8.DataBind();

                grvMoneda9.DataSource = dtDetalleMoneda9;
                grvMoneda9.DataBind();

                grvMoneda10.DataSource = dtDetalleMoneda10;
                grvMoneda10.DataBind();

                grvMoneda11.DataSource = dtDetalleMoneda11;
                grvMoneda11.DataBind();

                grvMoneda12.DataSource = dtDetalleMoneda12;
                grvMoneda12.DataBind();

                grvMoneda13.DataSource = dtDetalleMoneda13;
                grvMoneda13.DataBind();

                grvMoneda14.DataSource = dtDetalleMoneda14;
                grvMoneda14.DataBind();

                grvMoneda15.DataSource = dtDetalleMoneda15;
                grvMoneda15.DataBind();
            }
            else
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaDetalleTurnoNoData.lblMensajeError.Text"].ToString();
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


    protected void grvMoneda1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8" )
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/

        }
    }

    protected void grvMoneda2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/

        }
    }

    protected void grvMoneda3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/

        }
    }

    protected void grvMoneda4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/

        }
    }

    protected void grvMoneda5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/
        }
    }

    protected void grvMoneda6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "1" ||
               ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "8")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFFFF");

            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "0" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "90")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFF96");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "81")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFCC66");
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "82" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[3].ToString() == "83")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#CCFF99");
            }
            /*if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }*/

        }
    }

    protected void grvMoneda7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda8_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda9_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda10_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda11_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda12_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }

    protected void grvMoneda13_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }


    protected void grvMoneda14_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }


    protected void grvMoneda15_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final (Descuadrado)")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Inicio" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Efectivo Final")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#f5f5f5");
            }

            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al contado" || ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[0].ToString() == "Venta al credito")
            {
                e.Row.BackColor = System.Drawing.Color.FromName("#FFFFBB");
            }
        }
    }


    protected void grvMoneda1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {            

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda1"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
            
        }

    }
    protected void grvMoneda2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda2"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }

        }
    }
    protected void grvMoneda3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda3"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }

        }
    }
    protected void grvMoneda4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda4"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }

        }
    }
    protected void grvMoneda5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda5"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }

        }
    }
    protected void grvMoneda6_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda1 = (DataTable)ViewState["Moneda6"];
            string codTurno = dtMoneda1.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda1.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda1.Rows[rowIndex]["idDetalle"].ToString();

            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }

        }
    }
    protected void grvMoneda7_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda7 = (DataTable)ViewState["Moneda7"];
            string codTurno = dtMoneda7.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda7.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda7.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda8_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda8 = (DataTable)ViewState["Moneda8"];
            string codTurno = dtMoneda8.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda8.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda8.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda9_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda9 = (DataTable)ViewState["Moneda9"];
            string codTurno = dtMoneda9.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda9.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda9.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda10_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda10 = (DataTable)ViewState["Moneda10"];
            string codTurno = dtMoneda10.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda10.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda10.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda11_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda11 = (DataTable)ViewState["Moneda11"];
            string codTurno = dtMoneda11.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda11.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda11.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda12_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda12 = (DataTable)ViewState["Moneda12"];
            string codTurno = dtMoneda12.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda12.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda12.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda13_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda13 = (DataTable)ViewState["Moneda13"];
            string codTurno = dtMoneda13.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda13.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda13.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
    protected void grvMoneda14_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda14 = (DataTable)ViewState["Moneda14"];
            string codTurno = dtMoneda14.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda14.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda14.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }

    protected void grvMoneda15_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetTurno")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtMoneda15 = (DataTable)ViewState["Moneda15"];
            string codTurno = dtMoneda15.Rows[rowIndex]["Turno"].ToString();
            string codMoneda = dtMoneda15.Rows[rowIndex]["Moneda"].ToString();
            string codDetalle = dtMoneda15.Rows[rowIndex]["idDetalle"].ToString();
            if (codDetalle == "0" || codDetalle == "1" || codDetalle == "8" || codDetalle == "81" || codDetalle == "82"
                || codDetalle == "83" || codDetalle == "90" || codDetalle == "71" || codDetalle == "72" || codDetalle == "73"
                || codDetalle == "74" || codDetalle == "75")
            {
            }
            else
            {
                CnsDetTurno.Inicio(codTurno, codMoneda, codDetalle);
            }
        }
    }
}