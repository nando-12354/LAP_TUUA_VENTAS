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
using System.Collections.Generic;
using System.Text;

public partial class Cns_DetalleTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Consultas objWBConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    DataTable dt_consultaATM_CONT = new DataTable();
    DataTable dt_parametro = new DataTable();

    UIControles objConverDataTable = new UIControles();
    protected int currentPageNumber = 1;
    public int count;
    public int valorMaxGrilla;

    string sNumTicket;
    string stNumTicket;
    string sRangoTicket;
    string sTicketDesde;
    string sTicketHasta;
    string sBoarding;
    string sCompania;
    string sFechaVuelo;
    string sNumVuelo;
    string sNumAsiento;
    string sPasajero;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rbtnNumTicket.Checked = true;
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblNumTicket.Text = htLabels["consDetalleTicket.lblNumTicket"].ToString();
                this.btnConsultar.Text = htLabels["consDetalleTicket.btnConsultar"].ToString();

                this.lblTicketDesde.Text = htLabels["consDetalleTicket.lblTicketDesde"].ToString();
                this.lblTicketHasta.Text = htLabels["consDetalleTicket.lblTicketHasta"].ToString();
                this.lblCompania.Text = htLabels["consDetalleTicket.lblCompania"].ToString();
                this.lblNumVuelo.Text = htLabels["consDetalleTicket.lblNumVuelo"].ToString();
                this.lblFechaVuelo.Text = htLabels["consDetalleTicket.lblFechaVuelo"].ToString();
                this.lblNumAsiento.Text = htLabels["consDetalleTicket.lblNumAsiento"].ToString();
                this.lblPasajero.Text = htLabels["consDetalleTicket.lblPasajero"].ToString();
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

            try
            {
                //Carga combo compañia                       
                DataTable dt_allcompania = new DataTable();
                dt_allcompania = objWBConsultas.listarAllCompania();

                //Cargar combo tipo de persona
                UIControles objCargaComboCompania = new UIControles();
                objCargaComboCompania.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);
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

            // katy 11-01-2010
            DataTable dt_parametro = objWBConsultas.ListarParametros("AA");

            if (dt_parametro.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()) == 0)
                {

                    grvDetalleBoardingPagin.Columns[11].Visible = false;
                }

            }//katy 11-01-2011

            /*grvDetalleTicketPagin.VirtualItemCount = GetRowCount();
            CargarGrilla();

            grvDetalleBoardingPagin.VirtualItemCount = GetRowCount();
            CargarGrilla();  */

            this.lblMensajeError.Text = "";
            this.lblMensajeErrorData.Text = "";
        }
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sNumTicket", rbtnNumTicket.Checked.ToString()));
        filterList.Add(new Filtros("stNumTicket", txtNumTicket.Text));
        filterList.Add(new Filtros("sRangoTicket", rbtnRangoTicket.Checked.ToString()));
        filterList.Add(new Filtros("sTicketDesde", txtTicketDesde.Text));
        filterList.Add(new Filtros("sTicketHasta", txtTicketHasta.Text));

        filterList.Add(new Filtros("sBoarding", rbtnBoarding.Checked.ToString()));
        filterList.Add(new Filtros("sCompania", ddlCompania.SelectedValue));
        filterList.Add(new Filtros("sFechaVuelo", txtFechaVuelo.Text));
        filterList.Add(new Filtros("sNumVuelo", txtNumVuelo.Text));

        filterList.Add(new Filtros("sNumAsiento", txtNumAsiento.Text));
        filterList.Add(new Filtros("sPasajero", txtPasajero.Text));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        //sFechaDesde = newFilterList[0].Valor;
        sNumTicket = newFilterList[0].Valor;
        stNumTicket = newFilterList[1].Valor;
        sRangoTicket = newFilterList[2].Valor;
        sTicketDesde = newFilterList[3].Valor;
        sTicketHasta = newFilterList[4].Valor;
        sBoarding = newFilterList[5].Valor;
        sCompania = newFilterList[6].Valor;
        sFechaVuelo = newFilterList[7].Valor;
        sNumVuelo = newFilterList[8].Valor;
        sNumAsiento = newFilterList[9].Valor;
        sPasajero = newFilterList[10].Valor;
     }
    
    protected Int64 GetMaximoExcel()
    {
        Int64 iMaxReporte = 0;
        BO_Consultas objParametro = new BO_Consultas();
        DataTable dt_max = objParametro.ListarParametros("LE");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt64(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

    public void CargarFormulario()
    {

        RecuperarFiltros();

        this.lblMensajeErrorData.Text = "";
        this.lblMensajeError.Text = "";
        this.lblTotal.Text = "";


        if (sRangoTicket == "True" || sNumTicket == "True")
        {
            grvDetalleTicketPagin.VirtualItemCount = GetRowCount(false);
            int numFilas = 0;
            numFilas = count;
            int numFilasATM_CONTI = 0;
            numFilasATM_CONTI = GetRowCount(true);

            grvDetalleTicketConti.VirtualItemCount = numFilasATM_CONTI;

            htLabels = LabelConfig.htLabels;
            lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + (numFilas + numFilasATM_CONTI);
            lblTotalRows.Value = (numFilas + numFilasATM_CONTI).ToString();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
            CargarGrilla();
        }
        else
        {
            grvDetalleBoardingPagin.VirtualItemCount = GetRowCount(false);
            htLabels = LabelConfig.htLabels;
            lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + count;
            lblTotalRows.Value = count.ToString();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
            CargarGrilla();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        SaveFiltros();
        if (validarRangos())
        {
            CargarFormulario();
            CargarGrillaATM_CONTI();
        }
        //this.pnlPanelDetalleDocumento.Style.Add("visibility", "visible");
        //this.pnlPanelDetalleDocumento.Attributes.Add("style", "visibility:visible");
    }

    public bool validarRangos()
    {
        RecuperarFiltros();
        if (rbtnRangoTicket.Checked)
        {
            if (sTicketDesde.Length != 16 || sTicketHasta.Length != 16)
            {
                grvDetalleTicketPagin.DataSource = null;
                grvDetalleTicketPagin.DataBind();
                grvDetalleBoardingPagin.DataSource = null;
                grvDetalleBoardingPagin.DataBind();
                grvDetalleTicketConti.DataSource = null;
                grvDetalleTicketConti.DataBind();

                lblTotal.Text = "";
                this.lblTotalRows.Value = "";
                lblMensajeErrorData.Text = "";
                lblMensajeError.Visible = true;
                lblMensajeError.Text = "Ticket inválido";
                return false;
            }
            else {
                if (sTicketDesde.Substring(0,3) != sTicketHasta.Substring(0,3))
                {
                    grvDetalleTicketPagin.DataSource = null;
                    grvDetalleTicketPagin.DataBind();
                    grvDetalleBoardingPagin.DataSource = null;
                    grvDetalleBoardingPagin.DataBind();
                    grvDetalleTicketConti.DataSource = null;
                    grvDetalleTicketConti.DataBind();

                    lblTotal.Text = "";
                    this.lblTotalRows.Value = "";
                    lblMensajeErrorData.Text = "";
                    lblMensajeError.Visible = true;
                    lblMensajeError.Text = "La serie en el rango de tickets deben ser iguales";
                    return false;
                }
            }
        }

        return true;
    }

    protected void CargarGrilla()
    {

        RecuperarFiltros();
        try
        {
            if (sNumTicket == "True"|| sRangoTicket == "True")
            {
                dt_consulta = GetDataPage(grvDetalleTicketPagin.PageIndex, grvDetalleTicketPagin.PageSize,
                                          grvDetalleTicketPagin.OrderBy,false);
            }
            else
            {
                dt_consulta = GetDataPage(grvDetalleBoardingPagin.PageIndex, grvDetalleBoardingPagin.PageSize,
                                          grvDetalleBoardingPagin.OrderBy,false);
            }

            if (dt_consulta.Rows.Count < 1)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    Session["Data_Cns_DetTicketBP"] = null;
                    grvDetalleTicketPagin.DataSource = null;
                    grvDetalleTicketPagin.DataBind();
                    grvDetalleBoardingPagin.DataSource = null;
                    grvDetalleBoardingPagin.DataBind();

                    //grvDetalleTicket.DataSource = null;
                    //grvDetalleTicket.DataBind();

                    //grvDetalleBoarding.DataSource = null;
                    //grvDetalleBoarding.DataBind();

                    lblMensajeErrorData.Visible = true;
                    lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                    lblMensajeError.Visible = false;
                    this.lblTotal.Text = "";
                    this.lblTotalRows.Value = "";
                    
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
            else
            {
                /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                    this.lblMensajeError.Text = "";
                    this.lblTotal.Text = "";
                    this.lblTotalRows.Value = "";
                    Session["Data_Cns_DetTicketBP"] = null;
                    grvDetalleTicketPagin.DataSource = null;
                    grvDetalleTicketPagin.DataBind();

                    grvDetalleBoardingPagin.DataSource = null;
                    grvDetalleBoardingPagin.DataBind();
                }
                else
                {
                    ValidarTamanoGrilla();
                    if (rbtnBoarding.Checked)
                    {
                        grvDetalleTicketConti.DataSource = null;
                        grvDetalleTicketConti.Visible = false;

                        //ViewState["tablaBoarding"] = dt_consulta;
                        Session["Data_Cns_DetTicketBP"] = dt_consulta;
                        Session["tablaBoarding"] = dt_consulta;
                        //Cargar datos en la grilla
                        //lblMensajeErrorData.Visible = false;
                        lblMensajeError.Visible = false;
                        lblMensajeErrorData.Text = "";

                        grvDetalleTicketPagin.DataSource = null;
                        grvDetalleTicketPagin.DataBind();

                        grvDetalleBoardingPagin.DataSource = dt_consulta;
                        grvDetalleBoardingPagin.AllowPaging = true;
                        grvDetalleBoardingPagin.PageSize = valorMaxGrilla;
                        grvDetalleBoardingPagin.DataBind();

                        
                    }
                    else
                    {
                        lblMensajeErrorData.Text = "";
                        Session["Data_Cns_DetTicketBP"] = dt_consulta;
                        grvDetalleBoardingPagin.DataSource = null;
                        grvDetalleBoardingPagin.DataBind();

                        grvDetalleTicketPagin.DataSource = dt_consulta;
                        grvDetalleTicketPagin.AllowPaging = true;
                        grvDetalleTicketPagin.PageSize = valorMaxGrilla;
                        grvDetalleTicketPagin.DataBind();

                        //grvDetalleBoarding.Visible = false;

                        //ViewState["tablaTicket"] = dt_consulta;
                        Session["tablaTicket"] = dt_consulta;
                        //Cargar datos en la grilla
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

    protected void CargarGrillaATM_CONTI()
    {
        RecuperarFiltros();
        try
        {
            if (sNumTicket == "True" || sRangoTicket == "True")
            {
                //TICKETS ATM&CONTINGENCIA
                if (sRangoTicket == "True" && sTicketDesde != sTicketHasta)
                    dt_consultaATM_CONT = GetDataPage(grvDetalleTicketConti.PageIndex, grvDetalleTicketConti.PageSize,
                                              grvDetalleTicketConti.OrderBy, true);


                if (dt_consultaATM_CONT == null || dt_consultaATM_CONT.Rows.Count < 1)
                {
                    htLabels = LabelConfig.htLabels;
                    try
                    {
                        grvDetalleTicketConti.DataSource = null;
                        grvDetalleTicketConti.Visible = false;
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
                else
                {
                    if (dt_consultaATM_CONT.Columns[0].ColumnName == "MsgError")
                    {
                        //this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                        //this.lblMensajeError.Text = "";
                        //lblTicketsATMConti.Text = "";
                        lblTotal.Text = "";
                        grvDetalleTicketConti.DataSource = null;
                        grvDetalleTicketConti.Visible = false;
                    }
                    else
                    {
                        ValidarTamanoGrilla();
                        if (dt_consultaATM_CONT != null || dt_consultaATM_CONT.Rows.Count > 0)
                        {
                            if (lblMensajeErrorData.Text != "") lblMensajeErrorData.Text = "";
                            if (lblTotalRows.Value == "")
                            {
                                int numTicket = grvDetalleTicketConti.VirtualItemCount;
                                lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + (numTicket);
                                lblTotalRows.Value = numTicket.ToString();
                            }
                            grvDetalleTicketConti.Visible = true;
                            grvDetalleTicketConti.DataSource = dt_consultaATM_CONT;
                            grvDetalleTicketConti.PageSize = valorMaxGrilla;
                            grvDetalleTicketConti.DataBind();
                        }
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

    protected void grvDetalleBoarding_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RecuperarFiltros();
        string fechavuelo = sFechaVuelo;
        fechavuelo = fechavuelo.Substring(6, 4) + fechavuelo.Substring(3, 2) + fechavuelo.Substring(0, 2);

        dt_consulta = objWBConsultas.DetalleBoarding(sCompania, sNumVuelo, fechavuelo, sNumAsiento, sPasajero, null, null, null);
        //this.grvDetalleBoarding.DataSource = dwConsulta(dt_consulta, this.txtColumna.Text, txtOrdenacion.Text);
        //this.grvDetalleBoarding.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        //grvDetalleBoarding.PageIndex = e.NewPageIndex;
        //grvDetalleBoarding.DataBind();
    }

    protected void grvDetalleTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "ANULADO")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }
    }

    protected void grvDetalleTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleTicket = (DataTable)Session["tablaTicket"];// ViewState["tablaTicket"];
            String codTicket = dtDetalleTicket.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            ConsDetTicket1.Inicio(codTicket);
        }
    }

    protected void grvDetalleBoarding_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowDetBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleBoarding = (DataTable)Session["tablaBoarding"];//ViewState["tablaBoarding"];
            //Num_Secuencial_Bcbp_Rel
            String codSecuenciaBase = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            String codBoarding = String.Empty;
            if (codSecuenciaBase != null && codSecuenciaBase != "")
            {
                codBoarding = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            }
            else
            {
                codBoarding = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            }

            CnsDetBoarding1.CargarDetalleBoarding(codBoarding);
        }
    }


    #region Dynamic data query
    private int GetRowCount(bool ATM_CONTI)
    {
        count = 0;
        if (ATM_CONTI)
        {
            if (ATM_CONTI && sTicketDesde != sTicketHasta)
            {
                try
                {
                    return objWBConsultas.ConsultaDetalleTicketPagin("", sTicketDesde, sTicketHasta, null, 0, 0, "2").Rows.Count;
                }
                catch
                {
                }
            }
            return 0;
        }
        else
        {
            if (rbtnNumTicket.Checked)
            {
                dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin(stNumTicket, "", "", null, 0, 0, "1");
            }
            if (rbtnRangoTicket.Checked)
            {
                dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin("", sTicketDesde, sTicketHasta, null, 0, 0, "1");
            }

            if (rbtnBoarding.Checked)
            {
                string fechavuelo = txtFechaVuelo.Text;
                fechavuelo = fechavuelo.Substring(6, 4) + fechavuelo.Substring(3, 2) + fechavuelo.Substring(0, 2);
                dt_consulta = objWBConsultas.DetalleBoardingPagin(sCompania, sNumVuelo, fechavuelo, sNumAsiento, sPasajero, null, null, null, null, 0, 0, "1");
            }
            if (dt_consulta.Columns.Contains("TotRows"))
            {
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
            }
            else
            {
                lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
            }
            return count;
        }
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression, bool ATM_CONTI)
    {
        ValidarTamanoGrilla();

        //Ordenacion por Fecha de Modificacion ASCENDENTE
        if (sortExpression.CompareTo("FechaModificacion ") == 0)
        {
            sortExpression = "FechaModificacion ASC, Correlativo ASC";
        }
        //Ordenacion por Fecha de Modificacion DESCENDENTE
        if (sortExpression.CompareTo("FechaModificacion DESC") == 0)
        {
            sortExpression = "FechaModificacion DESC, Correlativo DESC";
        }

        Session["sortExpressionDetTicketBcbp"] = sortExpression;



        if (rbtnNumTicket.Checked)
        {
            dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin(stNumTicket, "", "", sortExpression, pageIndex, valorMaxGrilla, "0");
        }

        if (rbtnRangoTicket.Checked)
        {
            //TICKETS ATM&CONTINGENCIA
            if (ATM_CONTI)
            {
                try
                {
                    return objWBConsultas.ConsultaDetalleTicketPagin("", sTicketDesde, sTicketHasta, sortExpression, 0, 0, "2");
                }
                catch
                {
                    DataTable dtResultado = new DataTable();
                    return dtResultado;
                }
            }
            else
            {
                dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin("", sTicketDesde, sTicketHasta, sortExpression, pageIndex, valorMaxGrilla, "0");
            }
        }

        if (sBoarding == "True")
        {
            lblMensajeError.Text = "";
            lblMensajeErrorData.Text = "";
            string fechavuelo = sFechaVuelo;
            fechavuelo = fechavuelo.Substring(6, 4) + fechavuelo.Substring(3, 2) + fechavuelo.Substring(0, 2);
            dt_consulta = objWBConsultas.DetalleBoardingPagin(sCompania, sNumVuelo, fechavuelo, sNumAsiento, sPasajero, null, null, null, sortExpression, pageIndex, valorMaxGrilla, "0");
            ViewState["tablaBoarding"] = dt_consulta;
        }
        Session["Data_Cns_DetTicketBP"] = dt_consulta;
        return dt_consulta;
    }
    #endregion

    public void ValidarTamanoGrilla()
    {
        dt_parametro = objWBConsultas.ListarParametros("LG");

        if (dt_parametro.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    protected void grvDetalleTicketPagin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetalleTicketPagin.PageIndex = e.NewPageIndex;
        CargarGrilla();
    }

    protected void grvDetalleTicketPagin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            ConsDetTicket1.Inicio(((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).Text);
                        
        }
    }

    protected void grvDetalleTicketPagin_Sorting(object sender, GridViewSortEventArgs e)
    {
        CargarGrilla();
    }

    protected void grvDetalleBoardingPagin_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetalleBoardingPagin.PageIndex = e.NewPageIndex;
        CargarGrilla();
    }

    protected void grvDetalleBoardingPagin_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "ShowDetBoarding")
        //{
        //    CnsDetBoarding1.CargarDetalleBoarding(((System.Web.UI.WebControls.LinkButton)(e.CommandSource)).Text);
        //}




        if (e.CommandName == "ShowDetBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDetalleBoarding = (DataTable)Session["tablaBoarding"];//ViewState["tablaBoarding"];
            //Num_Secuencial_Bcbp_Rel
            String codSecuenciaBase = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            String codBoarding = String.Empty;
            if (codSecuenciaBase != null && codSecuenciaBase != "" && codSecuenciaBase != "0")
            {
                codBoarding = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            }
            else
            {
                codBoarding = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            }
            //int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            //DataTable dtDetalleBoarding = (DataTable)Session["tablaBoarding"];//ViewState["tablaBoarding"];
            //String codBoarding = dtDetalleBoarding.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            CnsDetBoarding1.CargarDetalleBoarding(codBoarding);
        }
    }

    protected void grvDetalleBoardingPagin_Sorting(object sender, GridViewSortEventArgs e)
    {
        CargarGrilla();
    }

    protected void grvDetalleTicketPagin_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void grvDetalleBoardingPagin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "R")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
            }
        }
    }

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=Detalle_TicketBP.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta = new DataTable();
        DataTable dt_Conti_ATM = new DataTable();

        RecuperarFiltros();

        #region Consultas
        try
        {
            if (sRangoTicket == "True" || sNumTicket == "True")
            {
                dt_consulta = objWBConsultas.ConsultaDetalleTicketPagin(stNumTicket, sTicketDesde, sTicketHasta, null, 0, 0, "0");
                try
                {
                    dt_Conti_ATM = objWBConsultas.ConsultaDetalleTicketPagin("", sTicketDesde, sTicketHasta, null, 0, 0, "2");
                }
                catch
                {
                }

                if (dt_Conti_ATM != null)
                {
                    foreach (DataRow row in dt_Conti_ATM.Rows)
                    {
                        dt_consulta.ImportRow(row);
                    }
                }
            }
            else
            {
                string fechavuelo = sFechaVuelo;
                fechavuelo = fechavuelo.Substring(6, 4) + fechavuelo.Substring(3, 2) + fechavuelo.Substring(0, 2);

                dt_consulta = objWBConsultas.DetalleBoardingPagin(sCompania, sNumVuelo, fechavuelo, sNumAsiento, sPasajero, null, null, null, null, 0, 0, "0");
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

        #endregion

        Excel Workbook = new Excel();

        #region Detalle Ticket/BP
        Excel.Worksheet detalleTicketBP;
        if (sRangoTicket == "True" || sNumTicket == "True")
        {
            detalleTicketBP = new Excel.Worksheet("Detalle Ticket BP");
            detalleTicketBP.Columns = new string[] { "Nro. Ticket", "Secuencial", "Tipo Ticket", "Compañía","Fch. Vuelo", 
                                                    "Nro. Vuelo", "Fch. Último Proc.", "Estado Actual", "Fch. vencimiento", "Turno", "Usuario Último Proc.", "", "Precio","Nro. Rehabilitaciones","Dato" };

            detalleTicketBP.WidthColumns = new int[] { 100, 80, 150, 180, 60, 60, 120, 70, 100, 80, 120, 10, 40, 80,80 };
            detalleTicketBP.DataFields = new string[] { "Cod_Numero_Ticket", "Correlativo", "Dsc_Tipo_Ticket", "Dsc_Compania", "Fch_Vuelo", 
                                                        "Dsc_Num_Vuelo", "FechaModificacion", "Dsc_Campo", "Fch_Vencimiento", "Cod_Turno", "Cta_Usuario","Cod_Moneda","Imp_Precio","Num_Rehabilitaciones","Tipo" };
            detalleTicketBP.Source = dt_consulta;
        }
        else
        {
            detalleTicketBP = new Excel.Worksheet("Detalle Ticket BP");
            detalleTicketBP.Columns = new string[] { "Nro. Boarding", "Secuencial", "Compañía", "Fch. vuelo","Nro. Vuelo", 
                                                    "Nro. Asiento", "Pasajero", "Tipo Ingreso", "Usuario Último Proc.", "Fch. Último Proc.", "Estado Actual" };

            detalleTicketBP.WidthColumns = new int[] { 100, 80, 80, 60, 70, 50, 150, 50, 80, 100, 65 };
            detalleTicketBP.DataFields = new string[] { "Cod_Numero_Bcbp", "Correlativo", "Dsc_Compania", "@Fch_Vuelo", "Num_Vuelo", 
                                                        "Num_Asiento", "Nom_Pasajero", "Dsc_Tipo_Ingreso", "Usuario", "FechaModificacion", "Dsc_Campo" };
            detalleTicketBP.Source = dt_consulta;
        }
        #endregion

        Workbook.Worksheets = new Excel.Worksheet[] { detalleTicketBP };

        return Workbook.Save();
    }

    protected void grvDetalleTicketConti_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvDetalleTicketConti.PageIndex = e.NewPageIndex;
        CargarGrillaATM_CONTI();
    }

    protected void grvDetalleTicketConti_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            ValidarTamanoGrilla();
            int rowIndex = Int32.Parse(e.CommandArgument.ToString()) - (valorMaxGrilla * grvDetalleTicketConti.PageIndex);

            string strNumTicket = grvDetalleTicketConti.DataKeys[rowIndex].Value.ToString();
            ConsDetTicket1.Inicio(strNumTicket);
        }
    }

    protected void grvDetalleTicketConti_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "ANULADO")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }
    }

    protected void grvDetalleTicketConti_Sorting(object sender, GridViewSortEventArgs e)
    {
        CargarGrillaATM_CONTI();
    }
}
