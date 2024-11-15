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

public partial class Ope_ExtornoTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected BO_Seguridad objBOSeguridad = new BO_Seguridad();
    protected TipoTicket objTipoTicket;
    protected bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                lblUsuario.Text = htLabels["opeExtornoTicket.lblUsuario"].ToString();
                lblTurno.Text = htLabels["opeExtornoTicket.lblTurno"].ToString();
                lblFecIni.Text = htLabels["opeExtornoTicket.lblFecIni"].ToString();
                lblFecFin.Text = htLabels["opeExtornoTicket.lblFecFin"].ToString();
                rbActivo.Text = htLabels["opeExtornoTicket.rbActivo"].ToString();
                rbCerrado.Text = htLabels["opeExtornoTicket.rbCerrado"].ToString();

                txtFecIni.Text = DateTime.Now.ToShortDateString();
                txtFecFin.Text = DateTime.Now.ToShortDateString();
                SetValorGrilla();
                DesabledFiltroFechas();
                FillGridViewTurno();

                this.rbCerrado.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], "Ope_ExtornoTicket.aspx", "Ver Turno Cerrado");
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

    private void FillGridViewTurno()
    {
        int intTipo;
        string strFecIni;
        string strFecFin;
        this.lblMensajeError.Text = "";
        if (rbActivo.Checked)
        {
            intTipo = 0;
            strFecIni = null;
            strFecFin = null;
        }
        else
        {
            intTipo = 1;
            int result = DateTime.Compare(Convert.ToDateTime(txtFecIni.Text), Convert.ToDateTime(txtFecFin.Text));
            if (result == 1)
            {
                this.lblMensajeError.Text = "Filtro de fecha invalido";
                grvTurno.DataSource = null;
                grvTurno.DataBind();
                return;
            }
            strFecIni = Function.FormatStringDate(txtFecIni.Text);
            strFecFin = Function.FormatStringDate(txtFecFin.Text);
        }

        string strUsuario = txtCajero.Text == "" ? null : txtCajero.Text;
        string strTurno = txtTurno.Text;
        Session["Filter_Tipo"] = intTipo;
        Session["Filter_FecIni"] = strFecIni;
        Session["Filter_FecFin"] = strFecFin;
        Session["Filter_Usuario"] = strUsuario;
        Session["Filter_Turno"] = strTurno;

        DataTable dt_consulta = objBOOpera.ListarTurnosXFiltro(intTipo, strTurno, strUsuario, strFecIni, strFecFin);
        lblGrilla.Text = !(dt_consulta != null && dt_consulta.Rows.Count != 0) ? (string)htLabels["opeExtornoTicket.msgGrilla"] : "";
        grvTurno.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTurno.DataSource = dt_consulta;
        Session["TurnosExtorno"] = dt_consulta;
        grvTurno.DataBind();
    }

    protected void grvTurno_Sorting(object sender, GridViewSortEventArgs e)
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
        //string strFecIni = (string)Session["Filter_FecIni"];
        //string strFecFin = (string)Session["Filter_FecFin"];
        //string strUsuario = Session["Filter_Usuario"] == null ? null : (string)Session["Filter_Usuario"];
        //string strTurno = (string)Session["Filter_Turno"];
        ////if (ddlUsuario.SelectedItem != null && ddlUsuario.SelectedItem.Text != "Todos")
        ////{
        ////    strUsuario = ddlUsuario.SelectedItem.Value;
        ////}
        //int intTipo = (int)Session["Filter_Tipo"];
        //DataTable dt_consulta = objBOOpera.ListarTurnosXFiltro(intTipo, strTurno, strUsuario, strFecIni, strFecFin);
        grvTurno.DataSource = dwConsulta((DataTable)Session["TurnosExtorno"], sortExpression, direction);
        grvTurno.DataBind();
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

    protected void grvTurno_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string strFecIni = (string)Session["Filter_FecIni"];
        string strFecFin = (string)Session["Filter_FecFin"];
        string strUsuario = Session["Filter_Usuario"] == null ? null : (string)Session["Filter_Usuario"];
        string strTurno = (string)Session["Filter_Turno"];
        int intTipo = (int)Session["Filter_Tipo"];
        DataTable dtConsulta = objBOOpera.ListarTurnosXFiltro(intTipo, strTurno, strUsuario, strFecIni, strFecFin);
        grvTurno.DataSource = dwConsulta(dtConsulta, this.txtColumna.Text, txtOrdenacion.Text);
        grvTurno.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvTurno.PageIndex = e.NewPageIndex;
        grvTurno.DataBind();

    }

    //protected void rbCerrado_CheckedChanged(object sender, EventArgs e)
    //{
    //    lblFecIni.Enabled = true;
    //    lblFecFin.Enabled = true;
    //    imgbtnIni.Enabled = true;
    //    imgbtnFin.Enabled = true;
    //}

    //protected void rbActivo_CheckedChanged(object sender, EventArgs e)
    //{
    //    DesabledFiltroFechas();
    //}

    private void DesabledFiltroFechas()
    {
        if (rbActivo.Checked == true)
        {
            lblFecIni.Enabled = false;
            lblFecFin.Enabled = false;
            imgbtnIni.Enabled = false;
            imgbtnFin.Enabled = false;
        }
        else {
            lblFecIni.Enabled = true;
            lblFecFin.Enabled = true;
            imgbtnIni.Enabled = true;
            imgbtnFin.Enabled = true;
        }
    }

    protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DesabledFiltroFechas();
            FillGridViewTurno();
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

}
