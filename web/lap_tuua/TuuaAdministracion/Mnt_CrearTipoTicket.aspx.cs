/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Creación de Tipo de Ticket
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/

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
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;


public partial class Modulo_CrearTipoTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    UIControles objCargaCombo = new UIControles();
    bool flagError;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                lblNombre.Text = htLabels["mtticket.lblNombre"].ToString();
                lblTipoPasajero.Text = htLabels["mtticket.lblTipoPasajero"].ToString();
                lblTipoVuelo.Text = htLabels["mtticket.lblTipoVuelo"].ToString();
                lblTipoTransbordo.Text = htLabels["mtticket.lblTipoTransbordo"].ToString();
                btnAceptar.Text = htLabels["mtticket.btnAceptar"].ToString();
                hConfirmacion.Value = htLabels["mtticket.cbeAceptar"].ToString();

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


            limpiar();
            CargarCombos();
            this.txtNombre.Focus();
        }

    }

    void limpiar()
    {

        this.txtNombre.Text = "";

    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo Vuelo
            DataTable dt_tipoVuelo = new DataTable();
            dt_tipoVuelo = objWBConsultas.ListaCamposxNombre("TipoVuelo");
            objCargaCombo.LlenarCombo(this.ddlTipoVuelo, dt_tipoVuelo, "Cod_Campo", "Dsc_Campo", false, false);


            //Carga combo Tipo Pasajero
            DataTable dt_tipoPasajero = new DataTable();
            dt_tipoPasajero = objWBConsultas.ListaCamposxNombre("TipoPasajero");
            objCargaCombo.LlenarCombo(this.ddlTipoPasajero, dt_tipoPasajero, "Cod_Campo", "Dsc_Campo", false, false);


            //Carga combo Tipo Trasbordo
            DataTable dt_tipoTrasbordo = new DataTable();
            dt_tipoTrasbordo = objWBConsultas.ListaCamposxNombre("TipoTrasbordo");
            objCargaCombo.LlenarCombo(this.ddlTipoTransbordo, dt_tipoTrasbordo, "Cod_Campo", "Dsc_Campo", false, false);
     

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
  
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            TipoTicket objTipoTicket = new TipoTicket("", this.txtNombre.Text.Trim(), ddlTipoVuelo.SelectedValue, 0, "", "", this.ddlTipoPasajero.SelectedValue, this.ddlTipoTransbordo.SelectedValue, "", Session["Cod_Usuario"].ToString(), "");
            objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            if (objWBAdministracion.ValidarTipoTicket(ddlTipoVuelo.SelectedValue, this.ddlTipoPasajero.SelectedValue, this.ddlTipoTransbordo.SelectedValue)==true)
            {
                if (objWBAdministracion.registrarTipoTicket(objTipoTicket) == true)
                {
                    lblMensajeError.Text = "";
                    omb.ShowMessage("Tipo de Ticket registrado correctamente", "Creacion de Tipo de Ticket", "Mnt_VerTipoTicket.aspx");
                }
            }
            else
            {
                this.lblMensajeError.Text = "El Tipo de Ticket ya fue registrado, verifique por favor";
                this.ddlTipoPasajero.Focus();
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
