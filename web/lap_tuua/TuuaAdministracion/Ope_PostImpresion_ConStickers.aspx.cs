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
using LAP.TUUA.ALARMAS;
using LAP.TUUA.CONTROL;

public partial class Ope_PostImpresion_ConStickers : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htParametro;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];

        String pagPreImpresion = Request.QueryString["Pagina_PreImpresion"];

        if (!IsPostBack)
        {

            String id_mensaje = Request.QueryString["id_mensaje"];
            

            String mensaje = "";
            bool hError = false;

            if (id_mensaje.Equals(Define.Error_NoControlado))
            {
                mensaje = htLabels["postImpresion.Error_NoControlado"].ToString();
                hError = true;
            }
            else if (id_mensaje.Equals(Define.Error_SoloVoucher))
            {
                mensaje = htLabels["postImpresion.Error_SoloVoucher"].ToString();
                hError = true;
            }
            else if (id_mensaje.Equals(Define.Error_SoloSticker))
            {
                mensaje = htLabels["postImpresion.Error_SoloSticker"].ToString();
                hError = true;
            }
            else if (id_mensaje.Equals(Define.Error_StickerConVoucher))
            {
                mensaje = htLabels["postImpresion.Error_StickerConVoucher"].ToString();
                hError = true;
            }
            else if (id_mensaje.Equals(Define.Salir_SoloVoucher))
            {
                mensaje = htLabels["postImpresion.Salir_SoloVoucher"].ToString();
            }
            else if (id_mensaje.Equals(Define.Salir_SoloSticker))
            {
                mensaje = htLabels["postImpresion.Salir_SoloSticker"].ToString();
            }
            else if (id_mensaje.Equals(Define.Salir_StickerConVoucher))
            {
                mensaje = htLabels["postImpresion.Salir_StickerConVoucher"].ToString();
            }
            else if (id_mensaje.Equals(Define.Impresion_SoloVoucher))
            {
                mensaje = htLabels["postImpresion.Impresion_SoloVoucher"].ToString();
                String puertoVoucher = Request.QueryString["puertos"];
                Session["PuertoVoucher"] = puertoVoucher;
            }
            else if (id_mensaje.Equals(Define.Impresion_SoloSticker))
            {
                mensaje = htLabels["postImpresion.Impresion_SoloSticker"].ToString();
                String puertoSticker = Request.QueryString["puertos"];
                Session["PuertoSticker"] = puertoSticker;
            }
            else if (id_mensaje.Equals(Define.Impresion_StickerConVoucher))
            {
                mensaje = htLabels["postImpresion.Impresion_StickerConVoucher"].ToString();
                String puertos = Request.QueryString["puertos"];
                String[] arrPuertos = puertos.Split(',');
                String puertoSticker = arrPuertos[0];
                String puertoVoucher = arrPuertos[1];
                Session["PuertoSticker"] = puertoSticker;
                Session["PuertoVoucher"] = puertoVoucher;
            }
            else
            {

            }


            if (hError)
            {

                if (pagPreImpresion == "Ope_VentaCredito.aspx")
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000031", "004", IpClient, "3", "Alerta W0000031", "Error al Imprimir Ticket(Venta Masiva): " + mensaje + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));                   
                }

                if (pagPreImpresion == "Ope_TicketContingencia.aspx")
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000020", "004", IpClient, "3", "Alerta W0000020", "Error al Imprimir Ticket Contingencia: " + mensaje + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                }

            }



            lblMensaje.Text = mensaje;
            if (pagPreImpresion == "Ope_VentaContingencia.aspx")
            {
                lblMensaje.Text += "<br><br> Operacion Exitosa ";
            }
            else
            {
                lblMensaje.Text += "<br><br> Nro de Tickets Impresos: " + Request.QueryString["nroTicketsImpresos"];
                lblMensaje.Text += "<br><br> Operacion: " + Request.QueryString["operacion"];

            }

            try
            {
                int intTicketsImpresos = Int32.Parse((string)Request.QueryString["nroTicketsImpresos"].Trim());
                string strListaTicket = (string)Session["listaCodigoTickets"];
                int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);

                if (intTicketsImpresos < strListaTicket.Length / intLongTicket)
                {
                    strListaTicket = strListaTicket.Substring(intTicketsImpresos * intLongTicket);

                    BO_Operacion objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    objBOOpera.AnularTuua(strListaTicket, strListaTicket.Length / intLongTicket);
                }
            }
            catch { }
        }
    }
}
