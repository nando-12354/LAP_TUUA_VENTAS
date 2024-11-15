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


public partial class Modulo_Consultas_ConsultaCompanias : System.Web.UI.Page
{
    protected Hashtable htLabels;
    public string orden = "";
    protected bool Flg_Error;
    protected BO_Error objError;
    Int32 valorMaxGrilla;
    BO_Consultas objParametro = new BO_Consultas();
    BO_Consultas objListaParametro = new BO_Consultas();
    //BO_Consultas objConsultaCompaniaxFiltro = new BO_Consultas();
    BO_Consultas objConsultaUsuariosxFiltro = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    DataTable dt_estado = new DataTable();
    DataTable dt_grupo = new DataTable();
    UIControles objControles = new UIControles();
    string sIdRol;
    string sIdEstado;
    string sIdGrupo;
    string sFiltro;
    string sOrdenacion;
    bool flagError;



    

    protected void Page_Load(object sender, EventArgs e)
    {
        Flg_Error = false;
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblEstado.Text = htLabels["consUsuario.lblRol"].ToString();
                this.lblTipo.Text = htLabels["consUsuario.lblEstado"].ToString();
                this.lblGrupo.Text = htLabels["consUsuario.lblGrupo"].ToString();
                this.btnConsultar.Text = htLabels["consCompania.btnConsultar"].ToString();
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
        Flg_Error = false;
        try
        {
            //Carga data table para el listado de Rol
            dt_consulta = objListaParametro.ListarRoles();
            objControles.LlenarCombo(ddlEstado, dt_consulta, "Cod_Rol", "Nom_Rol", true, false);

            //Carga combos Estado
            dt_estado = objParametro.ListaCamposxNombre("EstadoUsuario");
            objControles.LlenarCombo(ddlTipo, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);

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


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CargarGrilla();

    }


    void cargarDataTable()
    {
        try
        {
            sIdRol = ddlEstado.SelectedValue;
            sIdEstado = ddlTipo.SelectedValue;
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
            cargarDataTable();
            htLabels = LabelConfig.htLabels;
        
            if (dt_consulta.Rows.Count < 1)
            {

                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["tuua.general.lblMensajeError.Text"].ToString();
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
                lblTotal.Text = "";
                grvCompania.DataSource = null;
                grvCompania.DataBind();
            }
            else
            {       
                BO_Consultas objParametro = new BO_Consultas();
                DataTable dt_parametro = new DataTable();
                dt_parametro = objParametro.ListarParametros("LG");

                if (dt_parametro.Rows.Count > 0)
                {
                    txtValorMaximoGrilla.Text = dt_parametro.Rows[0].ItemArray.GetValue(4).ToString();
                }
                this.lblMensajeError.Text = "";
                this.lblMensajeErrorData.Text = "";
                //Cargar datos en la grilla                  
                this.grvCompania.DataSource = dt_consulta;
                this.grvCompania.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
                //grvCompania.PageCount = 10;
                this.grvCompania.DataBind();
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


    protected void grvCompania_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         cargarDataTable();
        this.grvCompania.DataSource = dwConsulta(dt_consulta, this.txtColumna.Text, txtOrdenacion.Text);
        this.grvCompania.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        grvCompania.PageIndex = e.NewPageIndex;
        grvCompania.DataBind();

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


    protected void grvCompania_Sorting(object sender, GridViewSortEventArgs e)
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
            cargarDataTable();
            Session["dt_consultaUsuario"] = objControles.ConvertDataTable(dwConsulta(dt_consulta, sortExpression, direction));
            this.grvCompania.DataSource = dwConsulta(dt_consulta, sortExpression, direction);
            this.grvCompania.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            grvCompania.DataBind();
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


    protected void grvCompania_RowDataBound(object sender, GridViewRowEventArgs e)
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





   
}
