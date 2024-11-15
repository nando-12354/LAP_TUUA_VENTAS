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

public partial class Mnt_CrearPuntoVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Administracion objWBAdministracion = new BO_Administracion();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            htLabels = LabelConfig.htLabels;
            
            try
            {
                this.btnAceptar.Text = (string)htLabels["madmEditarPuntoVenta.btnAceptar.Text"];
                this.lblDescripcion.Text = (string)htLabels["madmEditarPuntoVenta.lblDescripcion.Text"];
                this.lblDireccionIP.Text = (string)htLabels["madmEditarPuntoVenta.lblDireccionIP.Text"];
                cbeAceptar.ConfirmText = (string)htLabels["madmEditarPuntoVenta.cbeAceptar"];

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
    



    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            string IPBLoque1 = "";
            string IPBLoque2 = "";
            string IPBLoque3 = "";
            string IPBLoque4 = "";

            if (txtIPBloque1.Text == "")
            {
                lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
            }
            else
            {
                if (Convert.ToInt32(txtIPBloque1.Text) > 0 && Convert.ToInt32(txtIPBloque1.Text) < 256)
                {
                    IPBLoque1 = Convert.ToString(Convert.ToInt32(txtIPBloque1.Text));
                }
                else
                {

                    lblMensaje.Text = "Ip incorrecta (Rango de 1 a 255)";
                    IPBLoque1 = "";
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
                    IPBLoque2 = Convert.ToString(Convert.ToInt32(txtIPBloque2.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque2 = "";
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
                    IPBLoque3 = Convert.ToString(Convert.ToInt32(txtIPBloque3.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque3 = "";
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
                    IPBLoque4 = Convert.ToString(Convert.ToInt32(txtIPBloque4.Text));
                }
                else
                {
                    lblMensaje.Text = "Ip incorrecta (Rango de 0 a 255)";
                    IPBLoque4 = "";
                }
            }


            if (IPBLoque1 != "" && IPBLoque2 != "" && IPBLoque3 != "" && IPBLoque4 != "")
            {
                string IPFinal = IPBLoque1 + "." + IPBLoque2 + "." + IPBLoque3 + "." + IPBLoque4;




                EstacionPtoVta objEstacionPtoVta = new EstacionPtoVta(null, IPFinal, "1", Session["Cod_Usuario"].ToString(), Session["Cod_Usuario"].ToString(), null, null, txtDescripcion.Text.Trim());

                DataTable dt_consulta = new DataTable();
                objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                dt_consulta = objWBAdministracion.obtenerDetallePuntoVenta(null, IPFinal);

                if (dt_consulta.Rows.Count > 0)
                {
                    this.lblMensaje.Text = "La dirección IP ya se encuentra registrada";

                }
                else
                {

                    this.lblMensaje.Text = "";
                    if (objWBAdministracion.registrarPuntoVenta(objEstacionPtoVta) == true)
                    {
                        omb.ShowMessage("Punto de venta registrada correctamente", "Creacion de Punto de Venta", "Mnt_PuntoVenta.aspx");
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
