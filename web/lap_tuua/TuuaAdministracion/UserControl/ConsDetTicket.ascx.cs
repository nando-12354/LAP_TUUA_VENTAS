///V.1.4.6.0
///Luz Huaman
///Copyright ( Copyright © HIPER S.A. )
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

public partial class UserControl_ConsDetTicket : System.Web.UI.UserControl
{

    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Consultas objWBConsultas = new BO_Consultas();
    int valorMaxGrilla;
    string NumTicket;
    DataTable dt_ticketEstHist = new DataTable();
    DataTable dt_consulta = new DataTable();
    UIControles objUIControles = new UIControles();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Inicio
    public void Inicio(string codigo)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            lblDetalleTicket.Text = (String)htLabels["consDetalleDocumento.lblDetalleTicket"];

            this.lblNumTicket.Text = (string)htLabels["consDetalleDocumento.lblNumTicket"];
            this.lblTipoTicket.Text = (string)htLabels["consDetalleDocumento.lblTipoTicket"];

            this.lblTipoVuelo.Text = (string)htLabels["consDetalleDocumento.lblTipoVuelo"];
            this.lblTipoPasajero.Text = (string)htLabels["consDetalleDocumento.lblTipoPasajero"];
            this.lblTipoTrasbordo.Text = (string)htLabels["consDetalleDocumento.lblTipoTrasbordo"];
            this.lblEstado.Text = (string)htLabels["consDetalleDocumento.lblEstado"];
            this.lblPrecio.Text = (string)htLabels["consDetalleDocumento.lblPrecio"];
            this.lblModalidadVenta.Text = (string)htLabels["consDetalleDocumento.lblModalidadVenta"];
           // this.lblSincronizacion.Text = (string)htLabels["consDetalleDocumento.lblSincronizacion"];

            this.lblCompania.Text = (string)htLabels["consDetalleDocumento.lblCompania"];
            this.lblNumVuelo.Text = (string)htLabels["consDetalleDocumento.lblNumVuelo"];


            this.lblFechaVencimiento.Text = (string)htLabels["consDetalleDocumento.lblFechaVencimiento"];

            this.lblFormaPago.Text = (string)htLabels["consDetalleDocumento.lblFormaPago"];
            this.lblReferencia.Text = (string)htLabels["consDetalleDocumento.lblReferencia"];
            this.lblFechaVuelo.Text = (string)htLabels["consDetalleDocumento.lblFechaVuelo"];
            this.lblTipoCobro.Text = (string)htLabels["consDetalleDocumento.lblTipoCobro"];
            this.lblExtension.Text = (string)htLabels["consDetalleDocumento.lblExtension"];
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


        NumTicket = codigo;
        if (NumTicket != null)
        {
            try
            {
                string[] sValArch = NumTicket.Split('-');

                if (sValArch.Count() == 2)
                {
                    //Busca data en produccion
                    dt_consulta = objWBConsultas.ConsultaDetalleTicket_Reh(sValArch[0], "", "");
                }
                else
                {
                    //Busca data en archivamiento
                    dt_consulta = objWBConsultas.ConsultaDetalleTicket("", sValArch[0], sValArch[0]);                    
                }
                               

                if (dt_consulta.Rows.Count > 0)
                {
                    this.lblDetNumTicket.Text = dt_consulta.Rows[0]["Cod_Numero_Ticket"].ToString();
                    this.lblDetTipoTicket.Text = dt_consulta.Rows[0]["Cod_Tipo_Ticket"].ToString() +
                     " (" + dt_consulta.Rows[0]["Dsc_Tipo_Ticket"].ToString() + ")";
                    this.lblDetTipoVuelo.Text = (dt_consulta.Rows[0]["Tipo_Ticket"] != null) ? dt_consulta.Rows[0]["Tipo_Ticket"].ToString() : " - ";
                    this.lblDetTipoPasajero.Text = (dt_consulta.Rows[0]["Tip_Pasajero"] != null) ? dt_consulta.Rows[0]["Tip_Pasajero"].ToString() : " - ";
                    this.lblDetTipoTrasbordo.Text = (dt_consulta.Rows[0]["Tipo_Trasbordo"] != null) ? dt_consulta.Rows[0]["Tipo_Trasbordo"].ToString() : " - ";
                    this.lblDetEstado.Text = (dt_consulta.Rows[0]["Dsc_Estado_Actual"] != null) ? dt_consulta.Rows[0]["Dsc_Estado_Actual"].ToString() : " - ";
                    this.lblDetPrecio.Text = dt_consulta.Rows[0]["Cod_Moneda"].ToString() + " (" + dt_consulta.Rows[0]["Imp_Precio"].ToString() + ")";
                    this.lblDetModalidadVenta.Text = (dt_consulta.Rows[0]["Nom_Modalidad"] != null) ? dt_consulta.Rows[0]["Nom_Modalidad"].ToString() : " - ";
                    this.lblDetTurno.Text = (dt_consulta.Rows[0]["Cod_Turno"] != null) ? dt_consulta.Rows[0]["Cod_Turno"].ToString() : " - ";
                    this.lblDetContingencia.Text = (dt_consulta.Rows[0]["Flg_Contingencia"] != null) ? dt_consulta.Rows[0]["Flg_Contingencia"].ToString() : " - ";
                    this.lblDetCompania.Text = (dt_consulta.Rows[0]["Dsc_Compania"] != null) ? dt_consulta.Rows[0]["Dsc_Compania"].ToString() : " - ";
                    this.lblDetNumVuelo.Text = (dt_consulta.Rows[0]["Dsc_Num_Vuelo"] != null) ? dt_consulta.Rows[0]["Dsc_Num_Vuelo"].ToString() : " - ";
                    this.lblDetFchVencimiento.Text = (dt_consulta.Rows[0]["Fch_Vencimiento"] != null) ? dt_consulta.Rows[0]["Fch_Vencimiento"].ToString() : " - ";

                    this.lblDetSincronizacion.Text = (dt_consulta.Rows[0]["Flg_Sincroniza"] != null) ? dt_consulta.Rows[0]["Flg_Sincroniza"].ToString() : " - ";

                    this.lblDetFormaPago.Text = (dt_consulta.Rows[0]["Dsc_Forma_Pago"] != null) ? dt_consulta.Rows[0]["Dsc_Forma_Pago"].ToString() : " - ";
                    this.lblDetReferencia.Text = (dt_consulta.Rows[0]["Dsc_Referencia"] != null) ? dt_consulta.Rows[0]["Dsc_Referencia"].ToString() : " - ";
                    this.lblDetFechaVuelo.Text = (dt_consulta.Rows[0]["Fch_Vuelo"] != null && dt_consulta.Rows[0]["Fch_Vuelo"].ToString().Length == 8) ? Fecha.convertSQLToFecha(dt_consulta.Rows[0]["Fch_Vuelo"].ToString(), "") : " - ";
                    this.lblDetTipoCobro.Text = (dt_consulta.Rows[0]["Dsc_Tipo_Cobro"] != null) ? dt_consulta.Rows[0]["Dsc_Tipo_Cobro"].ToString() : " - ";
                    this.lblDetExtension.Text = (dt_consulta.Rows[0]["Num_Extensiones"] != null) ? dt_consulta.Rows[0]["Num_Extensiones"].ToString() : " - ";
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
                ValorMaximoGrilla();
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
                CargarDataTableDetTicket();
                //DataTable dt_ticketEstHist = objWBConsultas.ListarTicketEstHist_Arch(NumTicket);

                if (dt_ticketEstHist.Rows.Count > 0)
                {
                    grvSubDetalleTicket.DataSource = dt_ticketEstHist;
                    grvSubDetalleTicket.PageSize = valorMaxGrilla;
                    grvSubDetalleTicket.DataBind();
                    ViewState["DetalleTicket"] = dt_ticketEstHist;
                    grvSubDetalleTicket.Visible = true;
                    msgNoHist.Visible = false;
                }
                else
                {
                    grvSubDetalleTicket.Visible = false;
                    msgNoHist.Visible = true;
                    msgNoHist.Text = (string)htLabels["consDetalleDocumento.msgNoHist"];
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

    protected void ValorMaximoGrilla()
    {
        DataTable dt_parametro = objWBConsultas.ListarParametros("LG");

        if (dt_parametro.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }


    protected void CargarDataTableDetTicket()
    {
        string[] sValArch = NumTicket.Split('-');

        if (sValArch.Count() == 2)
        {
            dt_ticketEstHist = objWBConsultas.ListarTicketEstHist(sValArch[0]);            
            ViewState["DetalleTicket"] = dt_ticketEstHist;
        }
        else
        {
            dt_ticketEstHist = objWBConsultas.ListarTicketEstHist_Arch(sValArch[0]);
            ViewState["DetalleTicket"] = dt_ticketEstHist;
        }
    }



    #region grvSubDetalleTicket_PageIndexChanging
    protected void grvSubDetalleTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grvSubDetalleTicket.PageIndex = e.NewPageIndex;
        grvSubDetalleTicket.DataSource = (DataTable)ViewState["DetalleTicket"];
        grvSubDetalleTicket.DataBind();
    }
    #endregion

    protected void grvSubDetalleTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;

            this.txtOrdenacionDet.Text = "DESC";
            SortGridView(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

            this.txtOrdenacionDet.Text = "ASC";
            SortGridView(sortExpression, "ASC");
        }
        this.txtColumnaDet.Text = sortExpression;
    }


    public System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = System.Web.UI.WebControls.SortDirection.Ascending;
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
        //dt_consulta = objListarTicketxFecha.ListarTicketxFecha(sFechaDesde, sFechaHasta, sIdCompania, sTipoTicket, sEstadoTicket, sPersona, sFiltro, sTipoOperacion, sHoraDesde, sHoraHasta, null);
        ViewState["DetalleTicket"] = objUIControles.ConvertDataTable(dwConsulta((DataTable)ViewState["DetalleTicket"], sortExpression, direction));
        grvSubDetalleTicket.DataSource = (DataTable)ViewState["DetalleTicket"];
        ValorMaximoGrilla();
        grvSubDetalleTicket.PageSize = valorMaxGrilla;
        grvSubDetalleTicket.DataBind();
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacionDet.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }

        return dv;
    }


    protected void grvSubDetalleTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Tip_Estado = System.Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Tip_Estado"));
            if (Tip_Estado.Trim().ToUpper().Equals("R"))
            {
                //e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.FromArgb(254,233,194);
            }
            if (Tip_Estado.Trim().ToUpper().Equals("X"))
            {
                //e.Row.BackColor = System.Drawing.Color.Red;
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
        }      
    }
}
