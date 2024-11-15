using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Windows.Forms;
using System.Drawing;


public partial class Mnt_PuntoVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_TemporalBoardingPass objTemporalBoardingPass = new BO_TemporalBoardingPass("_TemporalBoardingPass");
    BO_TemporalTicket objTemporalTicket = new BO_TemporalTicket("_TemporalTicket");
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBOConsultas = new BO_Consultas();
    DataTable dt_consulta = new DataTable();
    bool flagError;
    protected BO_Error objError;
    public static System.Web.UI.WebControls.TextBox txtResultado2 = new System.Web.UI.WebControls.TextBox();

    protected void Page_Load(object sender, EventArgs e)
    {
        Session.Timeout = 30;
        if (!IsPostBack)
        {
            txtResultado.Enabled = false;

            if(Session["MostrarIndicacion"] == null)
                txtResultado.Text = "Ingrese el dato y presione 'Enter'";
            txtDatoIngresado.Focus();
            htLabels = LabelConfig.htLabels;
            try
            {

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
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            try
            {
                DataTable dt_parametro = new DataTable();
                dt_parametro = objBOConsultas.ListarParametros("LG");
                if (dt_parametro.Rows.Count > 0)
                {
                    this.txtValorMaximoGrilla.Text = Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString());
                }
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

    #region Metodos

    void txtDatoIngresado_Leave(object sender, EventArgs e)
    {
        if (txtDatoIngresado.Text.Length == 0)
        {
            txtDatoIngresado.Text = "Please Enter Your Name";
            txtDatoIngresado.ForeColor = SystemColors.GrayText;
        }
    }

    void txtDatoIngresado_Enter(object sender, EventArgs e)
    {
        if (txtDatoIngresado.Text == "Please Enter Your Name")
        {
            txtDatoIngresado.Text = "";
            txtDatoIngresado.ForeColor = SystemColors.WindowText;
        }
    }

    bool IsTicket(string str)
    {
        if (str.Length == 16)
        { 
            foreach (char c in str)
            {
            if (c < '0' || c > '9')
                return false;
            }

            return true;
        }

        return false;        
    }

    bool IsBoardingPass(string str)
    {
        if ((str.Length > 16) && (str.Length > 50))
            return true;
        else
            return false;
    }

    #endregion

    protected void CargarGrillaTickets()
    {
        //DataTable dttQuery = objTemporalTicket.listarAll();
        //ViewState["ConsultaTop5"] = dttQuery;
        //this.gwvTickets.DataSource = dttQuery;
        //this.gwvTickets.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
        //gwvTickets.DataBind();
        //if (dttQuery.Rows.Count < 1)
        //{
        //    this.lblTotal.Text = "";
        //    this.lblMensajeError.Visible = true;
        //    this.lblMensajeError.Text = htLabels["tuua.general.lblNoRegistros"].ToString();
        //}
        //else
        //{
        //    this.lblMensajeError.Visible = false;
        //    this.lblMensajeError.Text = "";
        //    //this.lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + lisQuery.Count;
        //}
    }

    protected void gwvTickets_Sorting(object sender, GridViewSortEventArgs e)
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
            this.gwvTickets.DataSource = dwConsulta((DataTable)ViewState["ConsultaTop5"], sortExpression, direction);
            this.gwvTickets.PageSize = Convert.ToInt32(txtValorMaximoGrilla.Text);
            gwvTickets.DataBind();
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
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }

    protected void btnProcesarDatos_Click(object sender, EventArgs e)
    {
        string datoIngresado = txtDatoIngresado.Text;
        int resultado = 0;
        string numCheckin = "";
        string numVueloIATA = "";
        string numVuelo = "";
        string numAsiento = "";
        string nomPasajero = "";
        int numDias = 0;
        string fchVuelo = "";

        TemporalBoardingPass entTemporalBoardingPass = new TemporalBoardingPass();
        TemporalTicket entTemporalTicket = new TemporalTicket();

        Session["MostrarIndicacion"] = 0;

        if (datoIngresado.Equals(""))
        {
            txtResultado.Text = "Por favor, ingrese un valor válido";
        }
        else if (IsTicket(datoIngresado))
        {
            entTemporalTicket.CodNumeroTicket = datoIngresado.Trim();
            resultado = objTemporalTicket.Ingresar(entTemporalTicket);
            if (resultado == 0)
            {
                txtResultado.Text = "Registro exitoso";
                txtDatoIngresado.Text = string.Empty;
            }
            if (resultado == 1)
            {
                txtResultado.Text = "El ticket ya ha sido registrado.";
            }
            if (resultado == 2)
            {
                txtResultado.Text = "El ticket no existe.";
            }
            //CargarGrillaTickets();
        }
        else if (IsBoardingPass(datoIngresado))
        {
            numDias = Convert.ToInt32(datoIngresado.Substring(44, 3));
            DateTime dteFecha = new DateTime(DateTime.Now.Year - 1, 12, 31);
            dteFecha = dteFecha.AddDays(numDias);
            fchVuelo = dteFecha.ToString("yyyyMMdd");
            nomPasajero = datoIngresado.Substring(2, 20).Trim().Replace("-", " ").Replace("/", " ");
            numAsiento = Function.Right("0000" + datoIngresado.Substring(48, 4).Trim(), 4);
            numVueloIATA = datoIngresado.Substring(36, 2).Trim();
            numVuelo = Convert.ToString(Convert.ToInt32(datoIngresado.Substring(39, 4)));

            if (numVuelo.Length == 4)
            {
                numVuelo = numVueloIATA + numVuelo;
            }
            else 
            {
                numVuelo = Function.Right("000" + numVuelo, 3);
                numVuelo = numVueloIATA + numVuelo;
            }
                
            numCheckin = Function.Right("00000" + datoIngresado.Substring(53, 4).Trim(), 5);
            entTemporalBoardingPass.NumCheckin = numCheckin;
            entTemporalBoardingPass.NumVuelo = numVuelo;
            entTemporalBoardingPass.NumAsiento = numAsiento;
            entTemporalBoardingPass.NomPasajero = nomPasajero;
            entTemporalBoardingPass.FchVuelo = fchVuelo;
            resultado = objTemporalBoardingPass.Ingresar(entTemporalBoardingPass);
            if (resultado == 0)
            {
                txtResultado.Text = "Registro exitoso";
                txtDatoIngresado.Text = string.Empty;
            }
            if (resultado == 1)
            {
                txtResultado.Text = "El Boarding Pass ya ha sido registrado.";
            }
            if (resultado == 2)
            {
                txtResultado.Text = "El Boarding Pass no existe.";
            }
            //CargarGrillaBoarding();
        }
        else
        {
            txtResultado.Text = "Por favor, ingrese un valor válido";
        }
    }
}
