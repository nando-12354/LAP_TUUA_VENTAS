/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Pie de Página
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
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

public partial class UserControl_PiePagina : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    protected void Page_Load(object sender, EventArgs e)
    {

        htLabels = LabelConfig.htLabels;
        try
        {
            this.lblDerechosLap.Text = htLabels["piepagina.lblDerechosLap"].ToString();
            this.hplCerrarSesion.Text = htLabels["piepagina.hplCerrarSesion"].ToString();
            lblfecha1.Text = System.DateTime.Now.ToString("yyyy");
        }
        catch (Exception ex)
        {
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Desc_Info = Define.ASPX_PiedePagina;
            Response.Redirect("PaginaError.aspx");
        }

       


    }

}
