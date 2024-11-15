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
            
            try
            {
                objBOError = new BO_Error();
                objBOError.IsError();
                string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[ErrorHandler.Cod_Error])["MESSAGE"];
                
                this.lblMensajeError.Text = strMessage ;
            }
            catch(Exception ex)
            {

                string strMessage = Define.ERR_DEFAULT;
                this.lblMensajeError.Text = strMessage ;
                ErrorHandler.Trace("", ex.Message);
            }

        }

    }
}
