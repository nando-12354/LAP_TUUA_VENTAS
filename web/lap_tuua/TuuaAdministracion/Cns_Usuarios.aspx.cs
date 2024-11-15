using System;
using System.Collections.Generic;
using System.Text;
using LAP.TUUA.ENTIDADES;
using System.Collections;
using System.Data;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Modulo_Consultas_ConsultaUsuarios : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    public string orden = "";
    BO_Consultas objParametro = new BO_Consultas();
    BO_Consultas objListaParametro = new BO_Consultas();
    BO_Consultas objConsultaUsuariosxFiltro = new BO_Consultas();
    UIControles objControles = new UIControles();
    DataTable dt_parametroTurno = new DataTable();
    DataTable dt_estado = new DataTable();
    DataTable dt_grupo = new DataTable();
    DataTable dt_parametro = new DataTable();
    DataTable dt_consulta = new DataTable();
    string sIdRol;
    string sIdEstado;
    string sIdGrupo;
    string sFiltro;
    string sOrdenacion;
    bool flagError;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

              htLabels = LabelConfig.htLabels;
            try
            {                
                this.lblRol.Text = htLabels["consUsuario.lblRol"].ToString();
                this.lblEstado.Text = htLabels["consUsuario.lblEstado"].ToString();
                this.lblGrupo.Text = htLabels["consUsuario.lblGrupo"].ToString();
                this.btnConsultar.Text = htLabels["consUsuario.btnConsultar"].ToString();
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
            CargarCombos();
        }
    }

    public void CargarCombos()
    {
        try
        {
            //Carga data table para el listado de Rol
            dt_parametro = objListaParametro.ListarRoles();
            objControles.LlenarCombo(ddlRol, dt_parametro, "Cod_Rol", "Nom_Rol", true, false);

            //Carga combos Estado
            dt_estado = objParametro.ListaCamposxNombre("EstadoUsuario");
            objControles.LlenarCombo(ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga Combos Grupo
            dt_grupo = objParametro.ListaCamposxNombre("GrupoUsuario");
            objControles.LlenarCombo(ddlGrupo, dt_grupo, "Cod_Campo", "Dsc_Campo", true, false);

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



    protected void cmdConsultar_Click(object sender, EventArgs e)
    {
        CargarGrilla();
    }

    void CargarDataTableGrilla()
    {
        try
        {
            sIdRol = ddlRol.SelectedValue;
            sIdEstado = ddlEstado.SelectedValue;
            sIdGrupo = ddlGrupo.SelectedValue;
            sFiltro = null;
            sOrdenacion = null;

            dt_consulta = objConsultaUsuariosxFiltro.ConsultaUsuarioxFiltro(sIdRol, sIdEstado, sIdGrupo, sFiltro, sOrdenacion);
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


    protected void CargarGrilla()
    {
        try
        {
            htLabels = LabelConfig.htLabels;
            CargarDataTableGrilla();

            if (dt_consulta.Rows.Count < 1)
            {
                 htLabels = LabelConfig.htLabels;
                 try
                 {
                     this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                     grvUsuarios.DataSource = null;
                     grvUsuarios.DataBind();
                     lblTotal.Text = "";
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

                //Traer valor de tamaño de la grilla desde parametro general
               
                dt_parametroTurno = objParametro.ListarParametros("LG");
                if (dt_parametroTurno.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString();
                }
                //Cargar datos en la grilla
                this.lblMensajeErrorData.Text = "";
                this.grvUsuarios.DataSource = dt_consulta;
                this.grvUsuarios.AllowPaging = true;
                this.grvUsuarios.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text); 
                this.grvUsuarios.DataBind();
                lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dt_consulta.Rows.Count;

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


    protected void grvUsuarios_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        CargarDataTableGrilla();
        this.grvUsuarios.DataSource = dwConsulta(dt_consulta, this.txtColumna.Text, txtOrdenacion.Text);
        this.grvUsuarios.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvUsuarios.PageIndex = e.NewPageIndex;
        grvUsuarios.DataBind();
    
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            //dv.Sort = sortExpression + " " + direction;
            //Soporte Ordenacion con varios campos
            string[] vec = sortExpression.Split(',');
            string sortExpressionMultiple = "";
            for (int i = 0; i <= vec.Length - 1; i++)
            {   
                sortExpressionMultiple += vec[i].Trim() + " " + direction;
                if (i + 1 <= vec.Length - 1)
                    sortExpressionMultiple += ",";
            }
            dv.Sort = sortExpressionMultiple;
        }

        return dv;
    }


    
    protected void grvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "ANULADO" ||
                ((System.Data.DataRowView)(e.Row.DataItem)).Row.ItemArray[5].ToString() == "BLOQUEADO")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);                
            }
        }
    }

    protected void grvUsuarios_Sorting(object sender, GridViewSortEventArgs e)
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
            CargarDataTableGrilla();
            Session["dt_consultaUsuario"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            this.grvUsuarios.DataSource = dwConsulta(dt_consulta, sortExpression, direction);
            this.grvUsuarios.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvUsuarios.DataBind();
        }
        catch (Exception ex)
        {
            flagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (flagError)
                Response.Redirect("PaginaError.aspx");

        }

    }


}
