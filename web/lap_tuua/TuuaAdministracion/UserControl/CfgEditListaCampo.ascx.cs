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

public partial class UserControl_CfgEditListaCampo : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    BO_Administracion objBOAdministracion = new BO_Administracion();

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            this.lblTituloCampo.Text = (string)htLabels["confEditListaCampo.lblTituloCampo"];

            this.lblNomCampo.Text = (string)htLabels["mLisCampo.lblNombreCampo"];
            this.lblCodCampo.Text = (string)htLabels["mLisCampo.lblCodCampo"];
            this.lblCodCampoAsoc.Text = (string)htLabels["mLisCampo.lblCodCampoAsoc"];
            this.lblDescripcion.Text = (string)htLabels["mLisCampo.lblDescripcion"];
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

    public void CargarFormulario(string pIdNomCampo, string pIdCodCampo)
    {
        try
        {
            ViewState["NomCampo"] = pIdNomCampo;
            ViewState["CodCampo"] = pIdCodCampo;
            DataTable dt_listacampo = new DataTable();
            dt_listacampo = objBOConfiguracion.ObtenerListaDeCampo(pIdNomCampo, pIdCodCampo);

            if (dt_listacampo.Rows.Count > 0)
            {
                this.lblNomCampoDesc.Text = dt_listacampo.Rows[0].ItemArray.GetValue(0).ToString();
                this.txtCodCampo.Text = dt_listacampo.Rows[0].ItemArray.GetValue(1).ToString();
                this.txtCodCampoAsoc.Text = dt_listacampo.Rows[0].ItemArray.GetValue(2).ToString();
                this.txtDescripcion.Text = dt_listacampo.Rows[0].ItemArray.GetValue(3).ToString();

                mpextCampos.Show();
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
    
    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        try
        {
            ListaDeCampo objListaDeCampo = new ListaDeCampo(ViewState["NomCampo"].ToString(),
                                                       this.txtCodCampo.Text,
                                                      ViewState["CodCampo"].ToString(),
                                                       this.txtDescripcion.Text,
                                                       Session["Cod_Usuario"].ToString(), null, null);

            objBOConfiguracion = new BO_Configuracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            if (objBOConfiguracion.RegistrarListaDeCampo(objListaDeCampo, 1) == true)
            {
                omb.ShowMessage("Lista de Campo Actualizado correctamente", "Modificar Lista de Campo", "Cfg_VerListaCampo.aspx");
            }
        }
        catch (Exception exc)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }
    
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtListaCampo = new DataTable();
            objBOConfiguracion = new BO_Configuracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            dtListaCampo = objBOConfiguracion.ObtenerListaDeCampo("", "");

            DataRow[] foundRows;
            foundRows = dtListaCampo.Select("Nom_Campo = '" + this.lblNomCampoDesc.Text + "' AND Cod_Campo='" + this.txtCodCampo.Text + "'");
            int iNumFilas = foundRows.Length;

            if (iNumFilas > 0)
            {
                if (objBOConfiguracion.EliminarListaDeCampo(this.lblNomCampoDesc.Text, this.txtCodCampo.Text) == true)
                {
                    omb.ShowMessage("Lista de Campo eliminado correctamente", "Modificar Lista de Campo", "Cfg_VerListaCampo.aspx");
                }
            }
            else
            {
                this.lblMensajeError.Text = "El código de campo no se encuentra registrado para ser eliminado";
            }
        }
        catch (Exception exc)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }
}
