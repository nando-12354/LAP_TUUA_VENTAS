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


public partial class Mnt_CrearTipoMonedas : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Consultas objListaCampos = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            htLabels = LabelConfig.htLabels;
            
            try
            {
                this.btnAceptar.Text = (string)htLabels["madmEditarTipoMoneda.btnAceptar.Text"];

                this.lblCodigo.Text = (string)htLabels["madmEditarTipoMoneda.lblCodigo.Text"];
                this.lblDescripcion.Text = (string)htLabels["madmEditarTipoMoneda.lblDescripcion.Text"];
                this.lblSimbolo.Text = (string)htLabels["madmEditarTipoMoneda.lblSimbolo.Text"];
                this.lblNemonico.Text = (string)htLabels["madmEditarTipoMoneda.lblNemonico.Text"];
                //cbeAceptar.ConfirmText = (string)htLabels["madmEditarTipoMoneda.cbeAceptar"];

                CargarMonedas();
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
        }
    }

    public void CargarMonedas()
    {
        try
        {
            Flg_Error = false;
            //Carga combo Codigo de Moneda
            DataTable dt_CodMoneda = objListaCampos.ListaCamposxNombre("CodMoneda");
            ViewState["CodMoneda"] = dt_CodMoneda; 
            DataTable dt_DscMoneda = objListaCampos.ListaCamposxNombre("DscMoneda");
            ViewState["DscMoneda"] = dt_DscMoneda; 
            DataTable dt_SimboloMoneda = objListaCampos.ListaCamposxNombre("SimboloMoneda");
            ViewState["SimboloMoneda"] = dt_SimboloMoneda; 
            DataTable dt_NemocMoneda = objListaCampos.ListaCamposxNombre("NemocMoneda");
            ViewState["NemocMoneda"] = dt_NemocMoneda;
            objCargaCombo.LlenarCombo(this.ddlCodMoneda, dt_CodMoneda, "Cod_Campo", "Dsc_Campo", true, true);
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
                Response.Redirect("PaginaError.aspx");

        }
    }



    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {   
            DataTable dt_consulta = objWBAdministracion.obtenerDetalleMoneda(this.ddlCodMoneda.SelectedItem.Text.Trim());

            if (dt_consulta.Rows.Count > 0)
            {
                this.lblMensaje.Text = "El código de moneda ya se encuentra registrada";

            }
            else
            {
                DataTable dt_BuscaDescripcion = objWBAdministracion.listarAllMonedas();
                DataRow[] foundRows = dt_BuscaDescripcion.Select("Dsc_Moneda = '" + this.txtDescripcion.Text.Trim() + "'");
                if (foundRows.Length > 0)
                {
                    lblMensaje.Text = "Descripcion de moneda ya se encuentra registrada";
                }
                else{
                    this.lblMensaje.Text = "";

                    Moneda objMoneda = new Moneda(this.ddlCodMoneda.SelectedItem.Text.Trim(), this.txtDescripcion.Text.Trim(), txtSimbolo.Text.Trim(), txtNemonico.Text.Trim(), "1", Session["Cod_Usuario"].ToString(), null, null, null, null);
                    objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    if (objWBAdministracion.registrarTipoMoneda(objMoneda) == true)
                    {
                        omb.ShowMessage("Moneda registrada correctamente", "Creacion de Moneda", "Mnt_TipoMonedas.aspx");
                    }
                }
            }
        }
        catch (Exception exc)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }

    protected void ddlCodMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblMensaje.Text = "";
        DropDownList ctrl = (DropDownList)sender;
       
        if (this.ddlCodMoneda.SelectedItem.Value.Trim() != "0")
        {
            string row_filter;
            row_filter = "Cod_Relativo = '" + this.ddlCodMoneda.SelectedItem.Value.Trim() + "'";
            DataTable dtTmp = (DataTable)ViewState["DscMoneda"];
            DataRow[] foundRow = dtTmp.Select(row_filter);
            this.txtDescripcion.Text = foundRow[0]["Dsc_Campo"].ToString();
            dtTmp = (DataTable)ViewState["SimboloMoneda"];
            foundRow = dtTmp.Select(row_filter);
            this.txtSimbolo.Text = foundRow[0]["Dsc_Campo"].ToString();
            
            dtTmp = (DataTable)ViewState["NemocMoneda"];
            foundRow = dtTmp.Select(row_filter);
            this.txtNemonico.Text = foundRow[0]["Dsc_Campo"].ToString();   
        }
        else
        {
            this.txtDescripcion.Text = "";
            this.txtSimbolo.Text = "";
            this.txtNemonico.Text = "";            
        }
    }
}
