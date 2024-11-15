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
using LAP.TUUA.ALARMAS;


public partial class Modulo_Mantenimiento_EditarTipoMonedas : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Administracion objWBAdministracion = new BO_Administracion();
    DataTable dtPrecioTicket = new DataTable();
    DataTable dtTasaCambio = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            htLabels = LabelConfig.htLabels;


            try
            {
                //Establecer datatable con datos del estado de compañia
                BO_Consultas objListaCampos = new BO_Consultas();
                DataTable dt_estado = new DataTable();
                dt_estado = objListaCampos.ListaCamposxNombre("EstadoRegistro");

                //Cargar combo del estado
                UIControles objCargaComboEstado = new UIControles();
                objCargaComboEstado.LlenarCombo(this.ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", false, false);

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

            try
            {
                this.btnActualizar.Text = (string)htLabels["madmEditarTipoMoneda.btnActualizar.Text"];
                this.lblCodigo.Text = (string)htLabels["madmEditarTipoMoneda.lblCodigo.Text"];
                this.lblDescripcion.Text = (string)htLabels["madmEditarTipoMoneda.lblDescripcion.Text"];
                this.lblSimbolo.Text = (string)htLabels["madmEditarTipoMoneda.lblSimbolo.Text"];
                this.lblNemonico.Text = (string)htLabels["madmEditarTipoMoneda.lblNemonico.Text"];
                this.lblEstado.Text = (string)htLabels["madmEditarTipoMoneda.lblEstado.Text"];
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
            


            string idMoneda = Request.QueryString["idMoneda"];
            if (idMoneda != null)
            {
                txtCodigo.Visible = true;
                DataTable dt_consulta = new DataTable();
                try
                {
                    dt_consulta = objWBAdministracion.obtenerDetalleMoneda(idMoneda);
                    
                    if (dt_consulta.Rows.Count > 0)
                    {
                        txtCodigo.Text = dt_consulta.Rows[0]["Cod_Moneda"].ToString();
                        txtDescripcion.Text = dt_consulta.Rows[0]["Dsc_Moneda"].ToString();
                        txtSimbolo.Text = dt_consulta.Rows[0]["Dsc_Simbolo"].ToString();
                        txtNemonico.Text = dt_consulta.Rows[0]["Dsc_Nemonico"].ToString();
                        ddlEstado.SelectedValue = dt_consulta.Rows[0]["Tip_Estado"].ToString();
                    }

                }
                catch (Exception ex)
                {
                       Response.Redirect("PaginaError.aspx");
                }

            }
            else
            {
                btnActualizar.Visible = false;
                txtCodigo.Visible = false;
                lblEstado.Visible = false;
                ddlEstado.Visible = false;
            }            
        }
    }



   
    /*--------------------------------------------------------------
     PERMITE ACTUALIZAR LOS DATOS DE LA MONEDA CON UNA PREVIA 
     VALIDACION EN LA DESCRIPCION EN CASO YA EXISTA
    ----------------------------------------------------------------*/
    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            string sDescripcion = txtDescripcion.Text; 
            string sFecha = DateTime.Now.ToShortDateString();
            string sHora = DateTime.Now.ToLocalTime().ToLongTimeString();
            Moneda objMoneda = new Moneda(this.txtCodigo.Text, sDescripcion, txtSimbolo.Text, txtNemonico.Text, ddlEstado.SelectedValue.ToString(), Session["Cod_Usuario"].ToString(), sFecha, sHora, null, null);
            
            DataTable dt_consulta = new DataTable();
            objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);         
            dt_consulta = objWBAdministracion.listarAllMonedas();
                                                    
            DataRow[] foundRows;
            foundRows = dt_consulta.Select("Dsc_Moneda = '" + sDescripcion.Trim() +  "'");
            int iNumFilas=foundRows.Length;                    


            if (iNumFilas > 0)
            {
                if (foundRows[0]["Cod_Moneda"].ToString() == txtCodigo.Text && foundRows[0]["Dsc_Moneda"].ToString() == txtDescripcion.Text.Trim())
                {                  

                    if (ddlEstado.SelectedValue == "0")
                    {
                        dtPrecioTicket = objWBAdministracion.ObtenerPrecioTicket("");
                        DataRow[] CountMonedasPT;
                        CountMonedasPT = dtPrecioTicket.Select("Cod_Moneda='" + txtCodigo.Text + "'");
                        int iTotMonedasPT = CountMonedasPT.Length;

                        dtTasaCambio = objWBAdministracion.ObtenerTasaCambio("");
                        DataRow[] CountMonedasTC;
                        CountMonedasTC = dtTasaCambio.Select("Cod_Moneda='" + txtCodigo.Text + "'");
                        int iTotMonedasTC = CountMonedasTC.Length;

                        if (iTotMonedasPT == 0 && iTotMonedasTC == 0)
                        {
                            this.lblMensaje.Text = "";
                            objWBAdministracion.actualizarTipoMoneda(objMoneda);

                            if (ddlEstado.SelectedValue.ToString() == "0")
                            {
                                //GeneraAlarma
                                string IpClient = Request.UserHostAddress;
                                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000012", "003", IpClient, "1", "Alerta W0000012", "Tipo de moneda anulada: " + txtCodigo.Text + " - " + txtDescripcion.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                            }
                            omb.ShowMessage("Moneda actualizada correctamente", "Creacion de Moneda", "Mnt_TipoMonedas.aspx");
                        }
                        else
                        {
                            this.lblMensaje.Text = "Error en anulación, moneda se encuentra en uso";
                        }
                    }
                    else
                    {
                        this.lblMensaje.Text = "";
                        objWBAdministracion.actualizarTipoMoneda(objMoneda);

                        if (ddlEstado.SelectedValue.ToString() == "0")
                        {
                            //GeneraAlarma
                            string IpClient = Request.UserHostAddress;
                            GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000012", "003", IpClient, "1", "Alerta", "Tipo de moneda anulada: " + txtCodigo.Text + " - " + txtDescripcion.Text, Convert.ToString(Session["Cod_Usuario"]));
                        }
                        omb.ShowMessage("Moneda actualizada correctamente", "Creacion de Moneda", "Mnt_TipoMonedas.aspx");
                    }
                    

                }
                else
                {
                    lblMensaje.Text = "Descripcion de moneda ya se encuentra registrada";
                }
            }
            else
            {
                this.lblMensaje.Text = "";
                if (objWBAdministracion.actualizarTipoMoneda(objMoneda) == true)
                {
                    omb.ShowMessage("Moneda actualizada correctamente", "Creacion de Moneda", "Mnt_TipoMonedas.aspx");
                }
            }
        }
        catch (Exception exc)
        {
            Response.Redirect("PaginaError.aspx");
        }
    }
}
