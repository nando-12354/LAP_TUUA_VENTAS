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
using LAP.TUUA.ALARMAS;
using System.Collections.Generic;

public partial class Ope_ExtenderVigencia : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera = new BO_Operacion();
    protected List<Ticket> listaTicket;
    protected int Can_Dias;
    protected bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            if (!IsPostBack)
            {
                rbRango.Checked = true;
                Hashtable htParametro = (Hashtable)Session["htParametro"];
                txtNroIni.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                txtNroFin.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                txtNumTicket.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                btnExtender.Text = htLabels["opeExtension.btnExtender"].ToString();
                rbRango.Text = htLabels["opeExtension.rbRango"].ToString();
                rbTicket.Text = htLabels["opeExtension.rbTicket"].ToString();
                lblDelNro.Text = htLabels["opeExtension.lblDelNro"].ToString();
                lblAlNro.Text = htLabels["opeExtension.lblAlNro"].ToString();
                lblExt.Text = htLabels["opeExtension.lblExtender"].ToString();
                btnExtender_ConfirmButtonExtender.ConfirmText = htLabels["opeExtension.msgConfirm"].ToString();
                SetValorGrilla();     
                FillGridViewTickets();
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    private void SetValorGrilla()
    {
        this.txtValorMaximoGrilla.Text = (string)Session["GridViewRows"];
    }

    private void FillGridViewTickets()
    {
        try
        {
            string strNumTicket = txtNumTicket.Text != "" ? txtNumTicket.Text : null;
            string strDelNro = txtNroIni.Text != "" ? txtNroIni.Text : null;
            string strAlNro = txtNroFin.Text != "" ? txtNroFin.Text : null;
            Session["Filter_NumTicket"] = strNumTicket;
            Session["Filter_DelNro"] = strDelNro;
            Session["Filter_AlNro"] = strAlNro;
            DataTable dtConsulta = objBOOpera.ListarTicketsExtension(strNumTicket, strDelNro, strAlNro);
            if (dtConsulta != null && dtConsulta.Rows.Count > 0)
            {
                btnExtender.Enabled = true;
                lblMensajeErrorData.Text = "";
                this.lblMensajeError.Text = "";
                /*lblDias.Visible = true;
                lblExt.Visible = true;
                txtDias.Visible = true;*/
            }
            else
            {
                btnExtender.Enabled = false;
                lblMensajeErrorData.Text = (string)htLabels["opeExtension.msgGrilla"];
                this.lblMensajeError.Text = "";
                /*lblDias.Visible = false;
                lblExt.Visible = false;
                txtDias.Visible = false;*/
            }
            grvTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvTicket.DataSource = dtConsulta;
            Session["TicketsExtension"] = dtConsulta;
            //ViewState["TicketsExtension"] = dtConsulta;
            grvTicket.DataBind();
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected void rbRango_CheckedChanged(object sender, EventArgs e)
    {
        txtNumTicket.Text = "";
        txtNumTicket.Enabled = false;
        txtNroIni.Enabled = true;
        txtNroFin.Enabled = true;
    }

    protected void rbTicket_CheckedChanged(object sender, EventArgs e)
    {
        txtNroIni.Text = "";
        txtNroFin.Text = "";
        txtNroIni.Enabled = false;
        txtNroFin.Enabled = false;
        txtNumTicket.Enabled = true;
    }

    protected void grvTicket_Sorting(object sender, GridViewSortEventArgs e)
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
        //string strNumTicket = (string)Session["Filter_NumTicket"] != "" ? (string)Session["Filter_NumTicket"] : null;
        //string strDelNro = (string)Session["Filter_DelNro"] != "" ? (string)Session["Filter_DelNro"] : null;
        //string strAlNro = (string)Session["Filter_AlNro"] != "" ? (string)Session["Filter_AlNro"] : null;
        //DataTable dtConsulta = ViewState["TicketsExtension"];
        grvTicket.DataSource = dwConsulta((DataTable)Session["TicketsExtension"], sortExpression, direction);
        grvTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTicket.DataBind();
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

    protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        FillGridViewTickets();
    }

    protected bool Validar()
    {
        listaTicket = new List<Ticket>();
        for (int i = 0; i < grvTicket.Rows.Count; i++)
        {
            GridViewRow row = grvTicket.Rows[i];
            bool isChecked = ((System.Web.UI.WebControls.CheckBox)row.FindControl("ckbRegistrar")).Checked;
            if (isChecked)
            {
                Ticket objTicket = new Ticket();
                objTicket.SCodNumeroTicket = row.Cells[1].Text.Trim();
                DataTable dtConsulta = (DataTable)Session["TicketsExtension"];
                DataRow[] foundRowTipoTicket = dtConsulta.Select("Cod_Numero_Ticket = '" + objTicket.SCodNumeroTicket + "'");
                objTicket.STipEstadoActual = foundRowTipoTicket[0].ItemArray.GetValue(16).ToString().Trim();
                objTicket.DtFchVencimiento = Fecha.convertToFechaSQL(((System.Web.UI.WebControls.Label)row.FindControl("lblFecVence")).Text);
                listaTicket.Add(objTicket);
            }
        }
        if (listaTicket.Count == 0)
        {
            lblMensajeError.Text = (string)htLabels["opeExtension.msgTicketSelect"];
            return false;
        }
        if (txtDias.Text == "")
        {
            /*rfvDias.ErrorMessage = htLabels["opeExtension.rfvDias"].ToString();
            rfvDias.IsValid = false;
            rfvDias.Enabled = true;*/
            lblMensajeError.Text = "Ingrese un número de días válido.";
            return false;
        }
        Can_Dias = Int32.Parse(txtDias.Text);
        if (Can_Dias == 0)
        {
            lblMensajeError.Text = "Ingrese un número de días válido.";
            return false;
        }        
        return true;
    }

    protected void btnExtender_Click(object sender, EventArgs e)
    {
        if (!Validar())
        {
            return;
        }
        try
        {
            objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            if (!objBOOpera.ExtenderVigencia(listaTicket, Can_Dias, (string)Session["Cod_Usuario"]))
            {
                lblMensajeError.Text = (string)htLabels["opeExtension.msgTrxFail"];
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000034", "004", IpClient, "3", "Alerta W0000034", "Error en la Extencion de la fecha de vigencia de Tickets, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000033", "004", IpClient, "1", "Alerta W0000033", "Extencion en la fecha de vigencia de Ticket, realizada correctamente, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                omb.ShowMessage((string)htLabels["opeExtension.msgTrxOK"], (string)htLabels["opeExtension.lblTitulo"], "Ope_ExtenderVigencia.aspx");
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected void grvTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //string strNumTicket = (string)Session["Filter_NumTicket"] != "" ? (string)Session["Filter_NumTicket"] : null;
        //string strDelNro = (string)Session["Filter_DelNro"] != "" ? (string)Session["Filter_DelNro"] : null;
        //string strAlNro = (string)Session["Filter_AlNro"] != "" ? (string)Session["Filter_AlNro"] : null;
        //DataTable dtConsulta = objBOOpera.ListarTicketsExtension(strNumTicket, strDelNro, strAlNro);
        DataTable dtConsulta = (DataTable)Session["TicketsExtension"];
        grvTicket.DataSource = dwConsulta(dtConsulta,  this.txtColumna.Text, txtOrdenacion.Text);
        grvTicket.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTicket.PageIndex = e.NewPageIndex;
        grvTicket.DataBind();
    }
}
