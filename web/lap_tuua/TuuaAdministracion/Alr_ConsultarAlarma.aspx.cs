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
using System.Collections.Generic;

public partial class Alr_ConsultarAlarma : System.Web.UI.Page
{
    protected Hashtable htLabels;
    bool flagError;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    private BO_Seguridad objBOSeguridad = new BO_Seguridad();
    private BO_Alarmas objBOAlarmas;
    UIControles objCargaCombo = new UIControles();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMensajeError.Text = "";
        lblErrorMsg.Text = "";

        objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblDesde.Text = htLabels["malarma.lblDesde"].ToString();
                lblHasta.Text = htLabels["malarma.lblHasta"].ToString();
                lblTotalIngresados.Text = htLabels["malarma.lblTotalIngresados"].ToString();
                this.lblModulo.Text = htLabels["malarma.lblModulo"].ToString();
                this.lblTipoAlarma.Text = htLabels["malarma.lblTipoAlarma"].ToString();
                this.lblEstado.Text = htLabels["malarma.lblEstado"].ToString();
                this.lblFechaGeneracion.Text = htLabels["malarma.lblFechaGeneracion"].ToString();

                this.txtDesde.Text = DateTime.Now.ToShortDateString();
                this.txtHasta.Text = DateTime.Now.ToShortDateString();

                DataTable dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.gwvAlarmasGeneradas.PageSize = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
                }

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

            CargarCombos();

        }
        else
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);
        }
    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Modulo
            DataTable dt_Modulo = new DataTable();
            dt_Modulo = objBOSeguridad.ListarAllModulo();
            objCargaCombo.LlenarCombo(this.ddlModulo, dt_Modulo, "Cod_Modulo", "Dsc_Modulo", true,false);

            //Carga combo Alarma
            DataTable dt_Alarma = new DataTable();
            dt_Alarma = objBOAlarmas.ObtenerAlarmaxCodModulo("");
            objCargaCombo.LlenarCombo(this.ddlTipoAlarma, dt_Alarma, "Cod_Alarma", "Dsc_Alarma", true,false);

            //Carga combo Estado
            DataTable dt_estado = new DataTable();
            dt_estado = objBOConsultas.ListaCamposxNombre("EstadoAlarma");
            objCargaCombo.LlenarCombo(this.ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", true,false);
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

    #region Paginacion

    protected void gwvAlarmasGeneradas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
        gwvAlarmasGeneradas.PageIndex = e.NewPageIndex;
        gwvAlarmasGeneradas.DataSource = dtAlarmaGenerada;
        gwvAlarmasGeneradas.DataBind();
    }

    #endregion

    #region gwvAlarmasGeneradas_Sorting
    protected void gwvAlarmasGeneradas_Sorting(object sender, GridViewSortEventArgs e)
    {

        DataTable dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];
        if (dtAlarmaGenerada == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        ViewState["CAlarmaGenerada"] = SortDataTable(dtAlarmaGenerada, false).ToTable();
        //reactualizo la tabla
        dtAlarmaGenerada = (DataTable)ViewState["CAlarmaGenerada"];

        gwvAlarmasGeneradas.DataSource = (DataTable)ViewState["CAlarmaGenerada"];

        gwvAlarmasGeneradas.DataBind();

    }
    #endregion

    #region Metodo Generales para el Sort
    //Method that sorts data
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

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }
    #endregion



    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        if (validaBuscar() == true)
            Buscar();
    }


    protected void Buscar()
    {

        DataTable dtAlarmaGenerada = new DataTable();
        dtAlarmaGenerada = cargarDatatable();

        if(dtAlarmaGenerada.Rows.Count == 0 || dtAlarmaGenerada == null)
        {
            lblTotalIngresados.Visible = true;
        }

        ViewState["CAlarmaGenerada"] = dtAlarmaGenerada;
        gwvAlarmasGeneradas.PageIndex = 0;
        gwvAlarmasGeneradas.DataSource = dtAlarmaGenerada;
        gwvAlarmasGeneradas.DataBind();
        this.lblTxtIngresados.Text = dtAlarmaGenerada.Rows.Count.ToString();
    
    }

    protected bool validaBuscar()
    {

        if (txtDesde.Text == "")
        {
            this.lblErrorMsg.Text = "Ingrese la Fecha Inicial";
            return false;
        }

        if (txtHasta.Text == "")
        {
            this.lblErrorMsg.Text = "Ingrese la Fecha Final";
            return false;
        }

        if (txtDesde.Text != "" && txtHasta.Text != "")
        {
            int result = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text), Convert.ToDateTime(this.txtHasta.Text));
            if (result == 1)
            {
                this.lblErrorMsg.Text = "Rango de fecha invalido";
                return false;
            }
            else if (result != 1)
                this.lblMensajeError.Text = "";
        }
        return true;

    }

    protected DataTable cargarDatatable()
    {
        DataTable dt_Consulta = new DataTable();
        string sFechaDesde;
        string sFechaHasta;
        string sHoraDesde;
        string sHoraHasta;
        string sIdModulo;
        string sIdAlarma;
        string sEstado;

        sFechaDesde = txtDesde.Text;
        sFechaHasta = txtHasta.Text;
        sHoraDesde = txtHoraDesde.Text;
        sHoraHasta = txtHoraHasta.Text;
        sIdModulo = ddlModulo.SelectedValue;
        sIdAlarma = ddlTipoAlarma.SelectedValue;
        sEstado = ddlEstado.SelectedValue;

        if (sHoraDesde != "")
        {
            string[] wordsDesde = sHoraDesde.Split(':');
            sHoraDesde = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        }
        else { sHoraDesde = ""; }



        if (sHoraHasta != "")
        {
            string[] wordsHasta = sHoraHasta.Split(':');
            sHoraHasta = wordsHasta[0] + "" + wordsHasta[1] + "" + wordsHasta[2];
        }
        else { sHoraHasta = ""; }



        if (sFechaDesde != "")
        {
            string[] wordsFechaDesde = sFechaDesde.Split('/');
            sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
        }
        else { sFechaDesde = ""; }



        if (sFechaHasta != "")
        {
            string[] wordsFechaHasta = sFechaHasta.Split('/');
            sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
        }
        else { sFechaHasta = ""; }

        dt_Consulta = objBOAlarmas.ConsultaAlarmaGenerada(sFechaDesde, sFechaHasta, sHoraDesde, sHoraHasta, sIdModulo, sIdAlarma, sEstado);

        return dt_Consulta;

    }

   
}
