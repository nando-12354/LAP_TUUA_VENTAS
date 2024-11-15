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

public partial class UserControl_IngBCBPAsociado : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void CargarFormulario(string idControl)
    {
        mpextIngBoarding.Show();
    }
    protected void btnAceptarPopup_Click(object sender, EventArgs e)
    {
        mpextIngBoarding.Hide();
    }
}
