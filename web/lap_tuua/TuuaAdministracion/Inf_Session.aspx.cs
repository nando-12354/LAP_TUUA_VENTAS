using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Inf_Session : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["TimeLife"] != null)
            {
                lblPeriodo.Text = Convert.ToString((Int32)Session["TimeLife"]) + " Dias ";
                lblPeriodo0.Text = Convert.ToString((Int32)Session["TimeLifeHour"]) + "h " + Convert.ToString((Int32)Session["TimeLifeMin"]) + "m " + Convert.ToString((Int32)Session["TimeLifeSeg"] + "s");
                Session["TimeLife"] = null;
                Session["TimeLifeHour"] = null;
                Session["TimeLifeMin"] = null;
                Session["TimeLifeSeg"] = null;
            }
            btnCerrar.Focus();
        }
        catch (Exception ex)
        {
            Page.Response.Write(ex.Message.ToString()); 
        }
    }
}
