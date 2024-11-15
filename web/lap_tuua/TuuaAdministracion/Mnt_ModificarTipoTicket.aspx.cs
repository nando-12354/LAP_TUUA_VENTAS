/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Modificación de Tipo de Ticket
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
using LAP.TUUA.ALARMAS;


public partial class Mnt_ModificarTipoTicket : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Consultas objWBConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    bool flagError;
    DataTable dt_consultatickets = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            string sCodTipoticket = Convert.ToString(Request.QueryString["Cod_TipoTicket"]);
            try
            {
                lblNombre.Text = htLabels["mtticket.lblNombre"].ToString();
                this.lblCodigo.Text = htLabels["mtticket.lblCodigo"].ToString();
                lblTipoPasajero.Text = htLabels["mtticket.lblTipoPasajero"].ToString();
                lblTipoVuelo.Text = htLabels["mtticket.lblTipoVuelo"].ToString();
                lblTipoTransbordo.Text = htLabels["mtticket.lblTipoTransbordo"].ToString();
                lblEstado.Text = htLabels["mtticket.lblEstado"].ToString();
                this.btnActualizar.Text = htLabels["mtticket.btnActualizar"].ToString();
                hConfirmacion.Value = htLabels["mtticket.cbeActualizar"].ToString();
                PoblarTipoTicket(sCodTipoticket);
                this.txtNombre.Focus();
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


        }
    }

    void PoblarTipoTicket(string CodTipoTicket)
    {
        try
        {
            TipoTicket objTipoTicket = new TipoTicket();

            objTipoTicket = objWBAdministracion.obtenerTipoTicket(CodTipoTicket);
            txtCodigo.Text = CodTipoTicket;
            txtNombre.Text = objTipoTicket.SNomTipoTicket;
            CargarCombos();
            this.ddlEstado.SelectedIndex = IndexListaCampo(objTipoTicket.STipEstado, ddlEstado);
            this.ddlTipoPasajero.SelectedIndex = IndexListaCampo(objTipoTicket.STipPasajero, ddlTipoPasajero);
            this.ddlTipoTransbordo.SelectedIndex = IndexListaCampo(objTipoTicket.STipTrasbordo, ddlTipoTransbordo);
            this.ddlTipoVuelo.SelectedIndex = IndexListaCampo(objTipoTicket.STipVuelo, ddlTipoVuelo);
            this.txtTipoPasajero.Text = ddlTipoPasajero.SelectedItem.Text;
            this.txtTipoTransbordo.Text = ddlTipoTransbordo.SelectedItem.Text;
            this.txtTipoVuelo.Text = ddlTipoVuelo.SelectedItem.Text;

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

    int IndexListaCampo(string CodCampo, DropDownList ddlBox)
    {

        for (int i = 0; i < ddlBox.Items.Count; i++)
        {
            if (ddlBox.Items[i].Value == CodCampo)
            {
                return i;
            }
        }
        return 0;

    }

    public void CargarCombos()
    {
        try
        {

            //Carga combo Estado Tipo Ticket
            DataTable dt_estado = new DataTable();
            dt_estado = objWBConsultas.ListaCamposxNombre("EstadoRegistro");
            objCargaCombo.LlenarCombo(this.ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", false, false);


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


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Mnt_VerTipoTicket.aspx");
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            TipoTicket objTipoTicket = new TipoTicket(this.txtCodigo.Text.Trim(), this.txtNombre.Text.Trim(), ddlTipoVuelo.SelectedValue, 0, this.ddlEstado.SelectedValue, "", this.ddlTipoPasajero.SelectedValue, this.ddlTipoTransbordo.SelectedValue, "", Session["Cod_Usuario"].ToString(), "");
            objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

            if (ddlEstado.SelectedValue == "0")
            {
                //dt_consultatickets = objWBConsultas.ConsultaDetalleTicket(null, null, null);


                //begin_kinzi
                LAP.TUUA.DAO.DAO_BaseDatos objDAO_BaseDatos2 = new LAP.TUUA.DAO.DAO_BaseDatos();

                Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings dbSettings = (Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseSettings)ConfigurationManager.GetSection("dataConfiguration");
                string defaultConnection = dbSettings.DefaultDatabase;
                objDAO_BaseDatos2.Conexion(defaultConnection);

                DataTable dt_consultatickets;
                string sNombreSP = "usp_cns_pcs_validarTipoTicket_sel";
                dt_consultatickets = objDAO_BaseDatos2.helper.ExecuteDataSet(sNombreSP, txtCodigo.Text).Tables[0];

                int count = 0;
                if (dt_consultatickets.Columns.Contains("TotRows"))
                    count = Convert.ToInt32(dt_consultatickets.Rows[0]["TotRows"].ToString());

                //DataRow[] CountTipoTicket;
                //CountTipoTicket = dt_consultatickets.Select("Cod_Tipo_Ticket='" + txtCodigo.Text + "' AND Tip_Estado_Actual<>'X' ");
                int iTotalTipoTicket = count;

                if (iTotalTipoTicket == 0)
                {
                    lblMensajeError.Text = "";
                    if (objWBAdministracion.actualizarTipoTicket(objTipoTicket) == true)
                    {
                        if (ddlEstado.SelectedValue.ToString() == "0")
                        {
                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000015", "003", IpClient, "1", "Alerta W0000015", "Tipo de Ticket anulada: " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                        }
                        lblMensajeError.Text = "";
                        omb.ShowMessage("Tipo de Ticket actualizado correctamente", "Modificar Tipo de Ticket", "Mnt_VerTipoTicket.aspx");
                    }
                }
                else
                {
                    lblMensajeError.Text = "Error en anulación, el Tipo Ticket se encuentra en uso";
                }
            }
            else
            {
                lblMensajeError.Text = "";
                if (objWBAdministracion.actualizarTipoTicket(objTipoTicket) == true)
                {
                    if (ddlEstado.SelectedValue.ToString() == "0")
                    {
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000015", "003", IpClient, "1", "Alerta", "Tipo de Ticket anulada: " + txtCodigo.Text, Convert.ToString(Session["Cod_Usuario"]));
                    }

                    omb.ShowMessage("Tipo de Ticket actualizado correctamente", "Modificar Tipo de Ticket", "Mnt_VerTipoTicket.aspx");
                }
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
