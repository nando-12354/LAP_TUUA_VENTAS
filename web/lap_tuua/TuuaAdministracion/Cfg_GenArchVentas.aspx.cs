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
using System.Globalization;
using LAP.TUUA.ALARMAS;

public partial class Cfg_GenArchVentas : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;


    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

        if (!IsPostBack)
        {
            this.btnGrabar.Text = "Grabar";

        }
    }
    protected void btnGrabar_Click(object sender, EventArgs e)
    {

        string sDatosFormularios = this.hfCadenaTotal.Value;
        string sDatosGrilla = this.hfCadenaGrilla.Value;


        BO_Configuracion objConfigParamGeneral = new BO_Configuracion();
        try
        {
            if (objConfigParamGeneral.GrabarParametroGeneral(sDatosFormularios, null,"1"))
            {
                //Response.Write("<script>alert('Parametros registrados correctamente');</script>");
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000010", "002", IpClient, "1", "Alerta W0000010", "Actualizacion del Archivo de Ventas, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                
                omb.ShowMessage("Parametros registrados correctamente", "Parametros generales", "Cfg_GenArchVentas.aspx");
            }
            else
            {

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
