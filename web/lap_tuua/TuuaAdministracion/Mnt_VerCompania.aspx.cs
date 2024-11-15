/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Mantenimiento de Compañía
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   21/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/

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
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;

public partial class Mnt_VerCompania : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Administracion objBOAdministracion = new BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    bool flagError;
    DataTable dtConsulta = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                btnNuevo.Text = htLabels["mcompania.btnNuevo"].ToString();
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");

            }


            try
            {
                btnNuevo.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntCrearCompania, Define.OPC_NUEVO);

                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                }

                CargarGrilla();
                
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
        //Globales.FlagPosPage = false;
        //Globales.FlagModal = false;
        eliminarGlobales();
    }

    public void eliminarGlobales()
    {
        Session.Remove("SuperGlobal");
    }

    void InicializarGrilla(DataTable dataCompania)
    {
        string[] data = new string[1];
        data[0] = "Cod_Compania";
        gwvCompañia.Columns.Clear();

        HyperLinkField hlfLink = new HyperLinkField();
        hlfLink.DataNavigateUrlFields = data;
        hlfLink.DataNavigateUrlFormatString = "Mnt_ModificarCompania.aspx?Cod_Compania={0}";
        hlfLink.ShowHeader = true;
        hlfLink.HeaderText = "Código";
        hlfLink.DataTextField = "Cod_Compania";
        hlfLink.NavigateUrl = "Mnt_ModificarCompania.aspx";
        hlfLink.SortExpression = "Cod_Compania";
        DataControlField dcfControl = hlfLink;
        gwvCompañia.Columns.Add(hlfLink);


        foreach (DataColumn col in dataCompania.Columns)
        {
            BoundField bf1 = new BoundField();

            switch (col.ColumnName)
            {
                case "Cod_Compania":
                    bf1.HeaderText = "Codigo";
                    break;
                case "Tip_Compania":
                    bf1.HeaderText = "Tipo";
                    break;
                case "Dsc_Compania":
                    bf1.HeaderText = "Descripción";
                    break;
                case "Fch_Creacion":
                    bf1.HeaderText = "Fecha Creación";
                    break;
                case"Est_Compania":
                    bf1.HeaderText = "Estado";
                    break;
                case "Cod_Especial_Compania":
                    bf1.HeaderText = "Código Aerolínea";
                    break;
                case "Cod_OACI":
                    bf1.HeaderText = "OACI";
                    break;
                case "Cod_IATA":
                    bf1.HeaderText = "IATA";
                    break;
                case "Dsc_Ruc":
                    bf1.HeaderText = "Ruc";
                    break;
                case "Usuario_Mod":
                    bf1.HeaderText = "Usuario Modificación";
                    break;
                case "Fecha_Mod":
                    bf1.HeaderText = "Fecha Modificación";
                    break;
                case "Cod_Aerolinea":
                    bf1.HeaderText = "Código Interno";
                    break;
                default:
                    bf1.HeaderText = col.ColumnName; 
                    break;
            }
            bf1.DataField = col.ColumnName;
            bf1.SortExpression = col.ColumnName; 
            gwvCompañia.Columns.Add(bf1);
        }
        //Additional Methods - kinzi
        BoundField bf2 = new BoundField();
        bf2.HeaderText = "Fecha Modificación";
        bf2.DataField = "Fecha_Mod";
        bf2.SortExpression = "Log_Fecha_Mod,Log_Hora_Mod";
        gwvCompañia.Columns.Add(bf2);
        bf2 = new BoundField();
        bf2.HeaderText = "Usuario Modificación";
        bf2.DataField = "Usuario_Mod";
        bf2.SortExpression = "Usuario_Mod";
        gwvCompañia.Columns.Add(bf2);        

        if (objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_MntModificarCompania, Define.OPC_MODIFICAR) == false)
        {
            gwvCompañia.Columns[0].Visible = false;
            gwvCompañia.Columns[2].Visible = true;
        }
        else
        {
            gwvCompañia.Columns[0].Visible = true;
            gwvCompañia.Columns[2].Visible = false;
        }
        
        gwvCompañia.Columns[1].Visible = false;
        gwvCompañia.Columns[11].Visible = false;
        gwvCompañia.Columns[12].Visible = false;
        gwvCompañia.Columns[13].Visible = false;
        gwvCompañia.Columns[14].Visible = false;
        gwvCompañia.Columns[15].Visible = false;
        gwvCompañia.Columns[16].Visible = false;
        gwvCompañia.Columns[17].Visible = false;
        gwvCompañia.Columns[18].Visible = false;
        gwvCompañia.Columns[19].Visible = false;
    }


    public void validagrilla(DataTable dt_consulta)
    {

        if (dt_consulta.Rows.Count < 1)
        {

            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblMensajeError.Text = htLabels["mcompania.lblMensajeError"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_CnsCompanias;
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected void CargarGrilla()
    {
        dtConsulta = objBOConsultas.ConsultaCompaniaxFiltro("0", "0", "", "ASC");
        ViewState["ConsultaCompania"] = dtConsulta;
        validagrilla(dtConsulta);
        InicializarGrilla(dtConsulta);
        this.gwvCompañia.DataSource = dtConsulta;
        this.gwvCompañia.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        gwvCompañia.DataBind();

        if (dtConsulta.Rows.Count < 1)
        {
            this.lblTotal.Text = "";
            this.lblMensajeError.Visible = true;
            this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        }
        else
        {
            this.lblMensajeError.Visible = false;
            this.lblMensajeError.Text = "";
            this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + dtConsulta.Rows.Count;
        }
    }

    protected void gwvCompañia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            InicializarGrilla((DataTable)ViewState["ConsultaCompania"]);
            this.gwvCompañia.DataSource = dwConsulta((DataTable)ViewState["ConsultaCompania"], this.txtColumna.Text, txtOrdenacion.Text);
            this.gwvCompañia.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvCompañia.PageIndex = e.NewPageIndex;
            gwvCompañia.DataBind();
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



    protected void gwvCompañia_Sorting(object sender, GridViewSortEventArgs e)
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
            InicializarGrilla((DataTable)ViewState["ConsultaCompania"]);
            this.gwvCompañia.DataSource = dwConsulta((DataTable)ViewState["ConsultaCompania"], sortExpression, direction);
            this.gwvCompañia.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvCompañia.DataBind();
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


    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mnt_CrearCompania.aspx");
    }

   
}
