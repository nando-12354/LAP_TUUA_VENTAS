/*
Sistema		 :   TUUA
Aplicaci�n	 :   Seguridad
Objetivo		 :   Cerrra Sesion
Especificaciones:   Se considera aquellas marcaciones seg�n el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/

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
using LAP.TUUA.UTIL;

public partial class Logeo_Cerrar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetNoStore();
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cookies.Clear();
        Session.Abandon();
        FormsAuthentication.SignOut();
        Response.Redirect("Login.aspx");


    }
}
