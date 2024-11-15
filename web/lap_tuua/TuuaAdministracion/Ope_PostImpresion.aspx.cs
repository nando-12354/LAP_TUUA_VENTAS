using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.UTIL;
using LAP.TUUA.ALARMAS;
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;

public partial class PostImpresion : System.Web.UI.Page
{

    protected Hashtable htLabels;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;

        if (!IsPostBack)
        {

            String id_mensaje = Request.QueryString["id_mensaje"];
            string pagPreImpresion = Request.QueryString["Pagina_PreImpresion"];
            bool hError = false;
            String mensaje = "";

            if(id_mensaje.Equals(Define.Error_NoControlado))
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
                if (pagPreImpresion == "Ope_ExtornoMoneda.aspx")
                {
                    List<LogOperacion> listaOperaciones = (List<LogOperacion>)Session["listaOperaciones"];
                    BO_Operacion objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    if (!objBOOpera.ExtornarCompraVenta(listaOperaciones))
                    {
                        //lblMensajeError.Text = objBOOpera.Dsc_Message;
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000023", "004", IpClient, "3", "Alerta W0000023", "Error en Extorno de Operaciones: " + objBOOpera.Dsc_Message + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    }
                }
                else if (pagPreImpresion == "Ope_ExtornoTicket.aspx")
                {
                    BO_Operacion objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    string Num_Tickets = (string)Session["Num_Tickets"];
                    int Can_Tickets=Int32.Parse(Session["Can_Tickets"].ToString());
                    string strMotivo = (string)Session["Dsc_Motivo"];
                    if (!objBOOpera.ExtornarTicket(Num_Tickets, (string)Session["Cod_turno"], Can_Tickets, "1", strMotivo))
                    {
                        //lblMensajeError.Text = objBOOpera.Dsc_Message;
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000024", "004", IpClient, "3", "Alerta W0000024", "Error en Extorno de Ticket: " + objBOOpera.Dsc_Message + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    }
                }
                else if (pagPreImpresion == "Ope_ExtornarRehabilitacion.aspx")
                {
                    BO_Operacion objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                    string Num_Tickets = (string)Session["Num_Tickets"];
                    int Can_Tickets = Int32.Parse(Session["Can_Tickets"].ToString());

                    if (!objBOOpera.ExtornarRehabilitacion(Num_Tickets, Can_Tickets, (string)Session["Cod_Usuario"], true))
                    {
                        //lblMensajeError.Text = objBOOpera.Dsc_Message;
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000025", "004", IpClient, "3", "Alerta W0000025", "Error en Extorno de Rehabilitacion: " + objBOOpera.Dsc_Message + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    }
                }
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
           

            if (hError)
            {
                if (pagPreImpresion == "Ope_ExtornoMoneda.aspx")
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000023", "004", IpClient, "3", "Alerta W0000023", "Error en Extorno de Operaciones: " + mensaje + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                }

                if (pagPreImpresion == "Ope_ExtornoTicket.aspx")
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000024", "004", IpClient, "3", "Alerta W0000024", "Error en Extorno de Ticket: " + mensaje + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                }

                if (pagPreImpresion == "Ope_ExtornarRehabilitacion.aspx")
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000025", "004", IpClient, "3", "Alerta W0000025", "Error en Extorno de Rehabilitacion: " + mensaje + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                }
            }

            lblMensaje.Text = mensaje;


        }
    }
}
