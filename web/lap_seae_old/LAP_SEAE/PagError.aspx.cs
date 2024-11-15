using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using LAP.TUUA.UTIL;

public partial class PagError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string cod_error = Request.QueryString["cod"];
        cod_error = (cod_error == null || cod_error.Length <= 0) ? Define.ERR_000 : cod_error;
        string strMessage = (string)((Hashtable)ErrorHandler.htErrorType[cod_error])["MESSAGE"];
        lblMensajeError.Text = strMessage;
    }
}
