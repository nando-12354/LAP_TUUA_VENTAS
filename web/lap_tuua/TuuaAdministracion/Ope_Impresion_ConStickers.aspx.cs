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

public partial class Ope_Impresion_ConStickers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblFecha.Text = DateTime.Now.ToString("dddd") + ", " + DateTime.Now.Day.ToString() + " de " + DateTime.Now.ToString("MMMM") + " de " + DateTime.Now.ToString("yyyy");

    }
}
