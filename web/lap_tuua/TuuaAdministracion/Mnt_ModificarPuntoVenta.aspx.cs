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

public partial class Mnt_EditarPuntoVenta : System.Web.UI.Page
{

    protected Hashtable htLabels;
    BO_Consultas objConsultaTurnoxFiltro = new BO_Consultas();
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Administracion objWBAdministracion;// = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
    string idEquipo; 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objWBAdministracion = new BO_Administracion();
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
                this.btnActualizar.Text = (string)htLabels["madmEditarPuntoVenta.btnActualizar.Text"];

                this.lblCodigo.Text = (string)htLabels["madmEditarPuntoVenta.lblCodigo.Text"];
                this.lblDireccionIP.Text = (string)htLabels["madmEditarPuntoVenta.lblDireccionIP.Text"];
                this.lblEstado.Text = (string)htLabels["madmEditarPuntoVenta.lblEstado.Text"];
                this.lblDescripcion.Text = (string)htLabels["madmEditarPuntoVenta.lblDescripcion.Text"];
                cbeActualizar.ConfirmText = (string)htLabels["madmEditarPuntoVenta.cbeActualizar"];
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

            idEquipo = Request.QueryString["idEquipo"];
            
            if (idEquipo != null)
            {
                DataTable dt_consulta = new DataTable();
                try
                {
                    dt_consulta = objWBAdministracion.obtenerDetallePuntoVenta(idEquipo,null);

                    if (dt_consulta.Rows.Count > 0)
                    {
                        this.txtCodigo.Text = dt_consulta.Rows[0].ItemArray.GetValue(0).ToString();
                        this.txtDescripcion.Text = dt_consulta.Rows[0].ItemArray.GetValue(7).ToString();
                        string sValorIP = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
                        string[] words = sValorIP.Split('.');
                        this.txtIPBloque1.Text = words[0];
                        this.txtIPBloque2.Text = words[1];
                        this.txtIPBloque3.Text = words[2];
                        this.txtIPBloque4.Text = words[3];

                        this.ddlEstado.SelectedValue = dt_consulta.Rows[0].ItemArray.GetValue(2).ToString();
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
           
        }
    }

    protected void btnActualizar_Click(object sender, EventArgs e)
    {
        try
        {

            string IPBLoque11 = "";
            string IPBLoque22 = "";
            string IPBLoque33 = "";
            string IPBLoque44 = "";

            if (txtIPBloque1.Text == "")
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
            }
            else
            {
                if (Convert.ToInt32(txtIPBloque1.Text) > 0 && Convert.ToInt32(txtIPBloque1.Text) < 256)
                {
                    IPBLoque11 = Convert.ToString(Convert.ToInt32(txtIPBloque1.Text));
                }
                else
                {

                    lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
                    IPBLoque11 = "";
                }
            }



            if (txtIPBloque2.Text == "")
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            }
            else
            {
                if (Convert.ToInt32(txtIPBloque2.Text) >= 0 && Convert.ToInt32(txtIPBloque2.Text) < 256)
                {
                    IPBLoque22 = Convert.ToString(Convert.ToInt32(txtIPBloque2.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque22 = "";
                }
            }


            if (txtIPBloque3.Text == "")
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            }
            else
            {
                if (Convert.ToInt32(txtIPBloque3.Text) >= 0 && Convert.ToInt32(txtIPBloque3.Text) < 256)
                {
                    IPBLoque33 = Convert.ToString(Convert.ToInt32(txtIPBloque3.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque33 = "";
                }
            }



            if (txtIPBloque4.Text == "")
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
            }
            else
            {
                if (Convert.ToInt32(txtIPBloque4.Text) >= 0 && Convert.ToInt32(txtIPBloque4.Text) < 256)
                {
                    IPBLoque44 = Convert.ToString(Convert.ToInt32(txtIPBloque4.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque44 = "";
                }
            }



            if (IPBLoque11 != "" && IPBLoque22 != "" && IPBLoque33 != "" && IPBLoque44 != "")
            {
                string IPFinal = IPBLoque11 + "." + IPBLoque22 + "." + IPBLoque33 + "." + IPBLoque44;


                EstacionPtoVta objEstacionPtoVta = new EstacionPtoVta(this.txtCodigo.Text, IPFinal, ddlEstado.SelectedValue.ToString(), Session["Cod_Usuario"].ToString(), Session["Cod_Usuario"].ToString(), null, null,txtDescripcion.Text);
                objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

                    DataTable dt_consultaFiltros = new DataTable();
                    dt_consultaFiltros = objWBAdministracion.listarAllPuntoVenta();

                    DataTable dt_consultaForm = new DataTable();
                    dt_consultaForm = objWBAdministracion.obtenerDetallePuntoVenta(Request.QueryString["idEquipo"], null);

                    string valorIP = dt_consultaForm.Rows[0].ItemArray.GetValue(1).ToString();
                    string codPtoVta = dt_consultaForm.Rows[0].ItemArray.GetValue(0).ToString();
                    string estPtoVta = dt_consultaForm.Rows[0].ItemArray.GetValue(2).ToString();
                    string descPtoVta = dt_consultaForm.Rows[0].ItemArray.GetValue(7).ToString();

                    if ((codPtoVta == this.txtCodigo.Text && valorIP == IPFinal && estPtoVta != ddlEstado.SelectedValue.ToString() && descPtoVta == txtDescripcion.Text) || (codPtoVta == this.txtCodigo.Text && valorIP == IPFinal && estPtoVta == ddlEstado.SelectedValue.ToString() && descPtoVta != txtDescripcion.Text) || (codPtoVta == this.txtCodigo.Text && valorIP == IPFinal && estPtoVta != ddlEstado.SelectedValue.ToString() && descPtoVta != txtDescripcion.Text))
                    {                    
                            this.lblMensaje.Text = "";

                            DataTable dt_cnsTurno = new DataTable();
                            dt_cnsTurno = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(null, null, null, null, null, null, "0");

                            DataRow[] foundRowTurno = dt_cnsTurno.Select("Num_Ip_Equipo = '" + IPFinal + "' AND (Fch_Fin is null OR Fch_Fin='')");


                            if (foundRowTurno.Length > 0)
                            {
                                this.lblMensaje.Text = "La estacion de venta tiene turno(s) abierto";
                            }
                            else
                            {
                                if (objWBAdministracion.actualizarPuntoVenta(objEstacionPtoVta) == true)
                                {
                                    if (ddlEstado.SelectedValue.ToString() == "0")
                                    {
                                        //GeneraAlarma
                                        string IpClient = Request.UserHostAddress;
                                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000011", "003", IpClient, "1", "Alerta W0000011", "Punto de venta anulada: " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                                    }

                                    omb.ShowMessage("Punto de venta actualizado correctamente", "Modificar Punto de Venta", "Mnt_PuntoVenta.aspx");
                                }
                            }
                    }
                    else
                    {
                        if ((codPtoVta == this.txtCodigo.Text && valorIP != IPFinal && estPtoVta == ddlEstado.SelectedValue.ToString()) || (codPtoVta == this.txtCodigo.Text && valorIP != IPFinal && estPtoVta != ddlEstado.SelectedValue.ToString() && descPtoVta != txtDescripcion.Text) || (codPtoVta == this.txtCodigo.Text.Trim() && valorIP == IPFinal && estPtoVta == ddlEstado.SelectedValue.ToString() && descPtoVta != txtDescripcion.Text.Trim()))
                        {
                            DataTable dt_consulta = new DataTable();
                            dt_consulta = objWBAdministracion.obtenerDetallePuntoVenta(null, IPFinal);

                            DataTable dt_cnsTurno = new DataTable();
                            dt_cnsTurno = objConsultaTurnoxFiltro.ConsultaTurnoxFiltro(null, null, null, null, null, null, "0");

                            DataRow[] foundRowTurno = dt_cnsTurno.Select("Num_Ip_Equipo = '" + IPFinal + "' AND (Fch_Fin is null OR Fch_Fin='')");


                            if (foundRowTurno.Length > 0)
                            {
                                this.lblMensaje.Text = "La estacion de venta tiene turno(s) abierto";
                            }
                            else
                            {
                                if (dt_consulta.Rows.Count < 1)
                                {
                                    this.lblMensaje.Text = "";
                                    if (objWBAdministracion.actualizarPuntoVenta(objEstacionPtoVta) == true)
                                    {
                                        omb.ShowMessage("Punto de venta actualizado correctamente", "Modificar Punto de Venta", "Mnt_PuntoVenta.aspx");
                                    }
                                }
                                else
                                {
                                    this.lblMensaje.Text = "La dirección IP ya se encuentra registrada";
                                }
                            }


                        }
                        else
                        {
                            if (codPtoVta == this.txtCodigo.Text && valorIP == IPFinal && estPtoVta == ddlEstado.SelectedValue.ToString() )                           
                            {
                                omb.ShowMessage("No se registrarón cambios", "Modificar Punto de Venta", "Mnt_PuntoVenta.aspx");
                            }
                            else
                            {
                                this.lblMensaje.Text = "La dirección IP ya se encuentra registrada";
                            }
                        }
                    }

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
}
