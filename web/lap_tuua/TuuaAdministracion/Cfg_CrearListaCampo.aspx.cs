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

public partial class Cfg_CrearListaCampo : System.Web.UI.Page
{
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    protected Hashtable htLabels;
    public string sName;
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    protected bool Flg_Error;
    protected BO_Error objError;


    protected void Page_Load(object sender, EventArgs e)
    {
                
    }
    

    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        /*txtNomCampo
        txtCodCampoAsoc
        txtCodCampo
        txtDescripcion*/
                

            if (!(txtNomCampo.Text.Length > 0))
                Response.Write("<script>alert('DDDD');document.location.href='Cfg_VerListaCampo.aspx';</script>");
            if (!(txtNomCampo.Text.Length > 0))
                Response.Write("<script>alert('FFFF');document.location.href='Cfg_VerListaCampo.aspx';</script>");
                     
            ListaDeCampo objListaDeCampo = new ListaDeCampo();

            objListaDeCampo.SNomCampo = txtNomCampo.Text;
            objListaDeCampo.SCodCampo = txtCodCampoAsoc.Text;
            objListaDeCampo.SCodRelativo = txtCodCampo.Text;
            objListaDeCampo.SDscCampo = txtDescripcion.Text;
            objListaDeCampo.SLogUsuarioMod = Session["Cod_Usuario"].ToString();
                
            DataTable dtListaCampo = new DataTable();

            objBOConfiguracion = new BO_Configuracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            dtListaCampo = objBOConfiguracion.ObtenerListaDeCampo("", "");

            DataRow[] foundRows;
            foundRows = dtListaCampo.Select("Nom_Campo = '" + txtNomCampo.Text + "' AND Cod_Campo='" + txtCodCampo.Text + "'");
            int iNumFilas = foundRows.Length;

            if (iNumFilas > 0)
            {
                this.lblMensajeError.Text = "El código de campo ya se encuentra registrado";
            }
            else
            {
                if (objBOConfiguracion.RegistrarListaDeCampo(objListaDeCampo, 0) == true)
                {
                    omb.ShowMessage("Lista de Campo Registrado correctamente", "Crear Lista de Campo", "Cfg_VerListaCampo.aspx");
                }
            }

            
        }
       
       
    }

