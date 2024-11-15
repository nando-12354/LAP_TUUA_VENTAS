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
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;


public partial class Ope_PostAtm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hlPostImpresion.Text = "Regresar Pagina Anterior.";
            hlPostImpresion.NavigateUrl = "Ope_TicketATM.aspx";
            lblMensaje.Text = (string)LabelConfig.htLabels["opeTicketAtm.msgTrxOK"];
        }
    }

    private void GenerarArchivoATM(string strTickets, string strCompania)
    {
        bool hError = true;

        try
        {
            if (strTickets != null && strCompania != null)
            {
                string strFecha = DateTime.Now.ToShortDateString();
                string strTime = DateTime.Now.ToLongTimeString();
                string strDateTime = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2) + "_" + strTime.Substring(0, 2) + strTime;
                string strFileName = strCompania + "_ATM.dat";
                Response.ContentType = "text/plain";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName);
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                Response.BinaryWrite(encoding.GetBytes(strTickets));
                Response.End();
                hError = false;
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (hError)
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000022", "004", IpClient, "3", "Alerta W0000022", "Se genero un error en la generacion del archivo de Tickets ATM, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }
        }

    }
    protected void ibtnFile_Click(object sender, ImageClickEventArgs e)
    {
        GenerarArchivoATM((string)Session["Tickets_ATM"], (string)Session["Cod_Compania_ATM"]);
    }
}
