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

public partial class UserControl_CnsLogMolinete : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void CargarLog(string strLog)
    {
        string[] lineas = strLog.Split('|');
        
        blErrores.Items.Clear();

        for (int i = 0; i < lineas.Length - 1; i++)
        {
            blErrores.Items.Add(lineas[i]);
        }
        
        mpextCnsLogMolinete.Show();
    }
}
