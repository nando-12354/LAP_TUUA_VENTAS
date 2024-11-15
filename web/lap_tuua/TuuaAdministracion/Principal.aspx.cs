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

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["TimeLife"] != null)
        {
            if ((Int32)Session["TimeLife"] >= 0)
            {
                hvida.Value = "yes";
            }
            else
            {
                Session["TimeLife"] = null;
            }
        }
        else
        {
            hvida.Value = "no";
        }
    }

   
}
