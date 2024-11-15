using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using System.Collections.Generic;

public partial class UserControl_AtencionAlarma : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected BO_Alarmas objBOAlarmas;
    protected bool flagError;

    private void Page_Init(object sender, System.EventArgs e)
    {
        objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblAtencionAlarma.Text = htLabels["malarma.lblAtencionAlarma"].ToString();
                this.lblDescripcionAtencion.Text = htLabels["malarma.lblDescripcionAtencion"].ToString();
                this.btnAceptar.Text = htLabels["malarma.btnAceptar"].ToString();
                this.btnCerrarPopup.Text = htLabels["malarma.btnCancelar"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegCrearUsuario;
                Response.Redirect("PaginaError.aspx");
            }
        }
        
    }

    
    public void CargarFormulario(string sCodAlarmaGenerada)
    {
        ViewState["id"] = sCodAlarmaGenerada;
        this.txtDscAtencion.Text = "";
        mpextAtencionAlarma.Show();
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            AlarmaGenerada objAlarmaGenerada = new AlarmaGenerada();

            objAlarmaGenerada.ICodAlarmaGenerada = Convert.ToInt32(ViewState["id"]);
            objAlarmaGenerada.SDscAtencion = this.txtDscAtencion.Text;
            objAlarmaGenerada.STipEstado = Define.ALR_EstadoAtendido;
            objAlarmaGenerada.SLogUsuarioMod = Convert.ToString(Session["Cod_Usuario"]);

            if (objBOAlarmas.actualizarAlarmaGenerada(objAlarmaGenerada) == true)
            {
                omb.ShowMessage("Atención realizada correctamente", "Atención de Alarma", "Alr_MonitorearAlarma.aspx");
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
