using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;
using System;

public partial class PaginaError : System.Web.UI.Page
{
    protected BO_Error objBOError;
    int s_timeout;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblTituloMensaje.Text = "Mensaje de error";
            this.lblDerechoTuua.Text = Define.LAP_lblDerechos;
            this.lblEmpresaTuua.Text = Define.LAP_lblEmpresa;

            this.lblFecha.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.Day.ToString() + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");

            try
            {
                objBOError = new BO_Error();
                objBOError.IsError();
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];
                if (Session["TimeInactivo"] != null)
                {
                    s_timeout = (int)Session["TimeInactivo"] * 1000;
                }
                this.lblMensajeError.Text = strMessage;
                
            }
            catch(Exception ex)
            {

                string strMessage = Define.ERR_DEFAULT;
                this.lblMensajeError.Text = strMessage ;
                ErrorHandler.Trace("", ex.Message);
            }

        }

        if (s_timeout > 0)
        {
            string str_Script = "StartTime(" + s_timeout.ToString() + ");";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CheckSessionOut", str_Script, true);
        }

    }
}
