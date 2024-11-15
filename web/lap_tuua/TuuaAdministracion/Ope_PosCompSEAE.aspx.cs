using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using LAP.TUUA.UTIL;

public partial class Ope_PosCompSEAE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hlPostImpresion.Text = "Regresar Pagina Anterior.";
            hlPostImpresion.NavigateUrl = "Ope_ComprobanteSEAE.aspx";
            lblMensaje.Text = (string)LabelConfig.htLabels["opeComprobanteSEAE.msgTrxOK"];
        }
    }
}
