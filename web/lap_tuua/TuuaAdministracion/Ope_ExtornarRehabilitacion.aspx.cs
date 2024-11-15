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
using System.Collections.Generic;
using LAP.TUUA.PRINTER;
using System.Xml;
using LAP.TUUA.ALARMAS;

public partial class Ope_ExtornarRehabilitacion : System.Web.UI.Page
{
    protected int Can_Tickets;
    protected string Num_Tickets;
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected Hashtable htParametro;

    protected void Page_Load(object sender, EventArgs e)
    {
        objBOOpera = new BO_Operacion();
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        if (!IsPostBack)
        {
            txtNumTicket.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
            txtNroIni.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
            txtNroFin.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
            btnExtornar.Text = htLabels["opeExtornoRehabil.btnExtornar"].ToString();
            //lblNumTicket.Text = htLabels["opeExtornoRehabil.lblNumTicket"].ToString();
            rbTicket.Text = htLabels["opeExtornoRehabil.rbTicket"].ToString();
            rbFiltro.Text = htLabels["opeExtornoRehabil.rbFiltro"].ToString();
            lblFecha.Text = htLabels["opeExtornoRehabil.lblFecha"].ToString();
            lblNumVuelo.Text = htLabels["opeExtornoRehabil.lblNumVuelo"].ToString();
            //btnExtornar_ConfirmButtonExtender.ConfirmText = htLabels["opeExtornoRehabil.msgConfirm"].ToString();
            Session["Cod_Turno"] = Request.QueryString["Cod_Turno"];
            txtDesde.Text = DateTime.Now.ToShortDateString();
            txtHasta.Text = DateTime.Now.ToShortDateString();
            txtValorMaximoGrilla.Text = (string)Session["GridViewRows"];
            ibtnAgregar.Enabled = true;
            ibtnBuscar.Enabled = false;
            //FillGridViewTickets();
        }

    }

    private void FillGridViewTickets()
    {
        string strNumTicket = txtNumTicket.Text;
        string strNumIni = txtNroIni.Text == "" ? null : txtNroIni.Text;
        string strNumFin = txtNroFin.Text == "" ? null : txtNroFin.Text;
        string strNumVuelo = txtNumVuelo.Text == "" ? null : txtNumVuelo.Text.ToUpper();
        string strHoraIni = txtHoraDesde.Text == "" ? "00:00:00" : txtHoraDesde.Text;
        lblErrorTicket.Text = "";
        string[] wordsDesde = strHoraIni.Split(':');
        strHoraIni = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        if (strHoraIni == "______")
            strHoraIni = "";

        string strHoraFin = txtHoraHasta.Text == "" ? "00:00:00" : txtHoraHasta.Text;
        wordsDesde = strHoraFin.Split(':');
        strHoraFin = wordsDesde[0] + "" + wordsDesde[1] + "" + wordsDesde[2];
        string strFecIni = txtDesde.Text.Substring(6, 4) + txtDesde.Text.Substring(3, 2) + txtDesde.Text.Substring(0, 2);
        string strFecFin = txtHasta.Text.Substring(6, 4) + txtHasta.Text.Substring(3, 2) + txtHasta.Text.Substring(0, 2);
        if (strHoraFin == "______")
            strHoraFin = "";
        DataTable dt_consulta = objBOOpera.ListarTicketsRehabilitados(strNumIni, strNumFin, strFecIni, strFecFin, strHoraIni, strHoraFin, strNumVuelo);
        if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
        {
            btnExtornar.Enabled = false;
            lblGrilla.Text = (string)htLabels["opeExtornoRehabil.msgGrilla"];
        }
        else
        {
            lblGrilla.Text = "";
            btnExtornar.Enabled = true;
        }
        ViewState["dtTicketR"] = dt_consulta;
        grvTicket.DataSource = dt_consulta;
        grvTicket.DataBind();
    }

    private bool Validar()
    {
        Num_Tickets = "";
        Can_Tickets = 0;
        for (int i = 0; i < grvTicket.Rows.Count; i++)
        {
            GridViewRow row = grvTicket.Rows[i];
            bool isChecked = ((System.Web.UI.WebControls.CheckBox)row.FindControl("ckbRegistrar")).Checked;
            if (isChecked)
            {
                Num_Tickets += row.Cells[4].Text;
                Can_Tickets++;
            }
        }
        if (Can_Tickets == 0)
        {
            lblMensajeError.Text = "No hay ticket seleccionados.";
            return false;
        }
        return true;
    }

    protected void btnExtornar_Click(object sender, EventArgs e)
    {
        bool Flg_Error = false;
        if (!Validar())
        {
            return;
        }
        try
        {

            objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

            Session["Num_Tickets"] = Num_Tickets;
            Session["Can_Tickets"] = Can_Tickets;

            if (!objBOOpera.ExtornarRehabilitacion(Num_Tickets, Can_Tickets, (string)Session["Cod_Usuario"], false))
            {
                lblMensajeError.Text = objBOOpera.Dsc_Message;
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000025", "004", IpClient, "3", "Alerta W0000025", "Error en Extorno de Rehabilitacion: " + objBOOpera.Dsc_Message + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

            }
            else
            {
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000026", "004", IpClient, "1", "Alerta W0000026", "Extorno de Rehabilitacion: Cuando se cambia los Ticket seleccionados a Anulados, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                // Comentado (GGarcia-20090924)
                //omb.ShowMessage((string)htLabels["opeExtornoRehabil.msgTrxOK"], (string)htLabels["opeExtornoRehabil.lblTitulo"], "Ope_ExtornarRehabilitacion.aspx");

                // se guarda en sesion la pagina 
                //Session["Pagina_PreImpresion"] = "Ope_ExtornarRehabilitacion.aspx";

                // rutina de impresion (GGarcia-20090924)
                Imprimir();

                // invocar a la pagina de impresion (GGarcia-20090924)
                //RedirectTo("Ope_Impresion.aspx");
            }

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    /// <summary>
    /// Metodo que imprime el voucher de extorno
    /// </summary>
    private void Imprimir()
    {
        // instanciar objeto 
        Print objPrint = new Print();

        // obtiene el nodo segun el nombre del voucher
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], Define.ID_PRINTER_DOCUM_EXTORNOREHABILITACION);

        // configuracion de la impresora a utilizar
        string configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], Define.ID_PRINTER_DOCUM_EXTORNOREHABILITACION);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // carga los parametros a imprimir con la impresora de voucher
        Hashtable htPrintData = new Hashtable();
        cargarParametrosImpresion(htPrintData);

        // obtiene la data a imprimir con la impresora de voucher y guardarla en una variable de sesion
        string dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);

        //int copias = objPrint.ObtenerCopiasVoucher(nodo);

        // guarda data a imprimir en sesion
        Session["dataSticker"] = "";
        Session["dataVoucher"] = dataVoucher;

        // guarda configuracion a utilizar en sesion
        //String configImpSticker = objPrint.ObtenerConfiguracionImpresoraDefault((Hashtable)Session["htParamImp"], Define.ID_PRINTER_KEY_STICKER);
        //Session["configImpSticker"] = configImpSticker;
        //Session["configImpVoucher"] = configImpVoucher;
        //Session["flagImpSticker"] = "0";
        //Session["flagImpVoucher"] = "1";

        Response.Redirect("Ope_Impresion.aspx?" +
            "flagImpSticker=0" +
            "&" + "flagImpVoucher=1" +
            //"&" + "copiasVoucher=" + copias +
            "&" + "configImpSticker=" + "" +
            "&" + "configImpVoucher=" + configImpVoucher +
            "&" + "Pagina_PreImpresion=Ope_ExtornarRehabilitacion.aspx", false);

    }

    /// <summary>
    /// Metodo que carga la lista de parametros a imprimir en el voucher
    /// </summary>
    private void cargarParametrosImpresion(Hashtable htPrintData)
    {
        
        // nombre y apellido del cajero
        htPrintData.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);

        // cantidad de tickets
        htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Tickets.ToString());

        //-- EAG 10/02/2010
        int q1 = Can_Tickets / 2;
        int q2 = Can_Tickets % 2;
        if (q2 != 0)
        {
            q1 = q1 + 1;
        }
        htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, q1.ToString());

        //htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, Can_Tickets.ToString());

        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        int contador = 0;
        // recorre cada codigo de ticket
        for (int i = 0, j = 0; i < Can_Tickets; i++)
        {

            /*
            htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, Num_Tickets.Substring(contador, intLongTicket));
            contador += intLongTicket;
            */

            if ((i + 1) % 2 == 0)//Par
            {
                htPrintData.Add("codigo_ticket_par" + "_" + j, Num_Tickets.Substring(contador, intLongTicket));
                contador += intLongTicket;
                j++;
            }
            else//Impar
            {
                htPrintData.Add("codigo_ticket_impar" + "_" + j, Num_Tickets.Substring(contador, intLongTicket));
                contador += intLongTicket;
            }
        }
        //cantidad de Tickets ya fue seteado.
        //-- EAG 10/02/2010

    }

    /// <summary>
    /// Metodo que redirecciona la pagina.
    /// <param name="pagina">nombre de la pagina</param>
    /// </summary>
    private void RedirectTo(string pagina)
    {
        string redirectURL = Page.ResolveClientUrl(pagina);
        string script = "window.location = '" + redirectURL + "';";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
    }

    protected void grvTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;

        if (GridViewSortDirection == SortDirection.Ascending)
        {
            GridViewSortDirection = SortDirection.Descending;
            this.txtOrdenacion.Text = "DESC";
            SortGridView(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = SortDirection.Ascending;
            this.txtOrdenacion.Text = "ASC";
            SortGridView(sortExpression, "ASC");
        }
        this.txtColumna.Text = sortExpression;
    }

    public SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState["SortDirection"] == null)
            {
                ViewState["SortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["SortDirection"];
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }


    private void SortGridView(string sortExpression, String direction)
    {
        grvTicket.DataSource = dwConsulta((DataTable)ViewState["dtTicketR"], sortExpression, direction);
        grvTicket.DataBind();
    }

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }

    protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        FillGridViewTickets();
    }

    protected void ibtnAgregar_Click(object sender, ImageClickEventArgs e)
    {
        int pageSize = grvTicket.PageSize;
        int pageCount = grvTicket.PageCount;
        string codigoTicket;
        lblErrorTicket.Text = "";
        codigoTicket = txtNumTicket.Text;

        if (codigoTicket.Equals(String.Empty))
        {
            lblErrorTicket.Text = "Codigo de Ticket no puede ser vacio";
            return;
        }
        DataTable dtTicketDetalle = objBOOpera.ListarTicketsRehabilitados(codigoTicket, codigoTicket, "", "", "", "", null);
        if (dtTicketDetalle.Rows.Count == 0)
        {
            lblErrorTicket.Text = "Ticket no valido para extorno";
            return;
        }
        String estado = dtTicketDetalle.Rows[0]["Tip_Estado_Actual"].ToString();
        if (!estado.Trim().ToUpper().Equals(Define.ESTADO_TICKET_REHABILITADO))
        {
            lblErrorTicket.Text = "Ticket no valido para extorno";
            return;
        }

        DataTable dtTicketRehabilitados;
        DataRow row;
        if (ViewState["dtTicketR"] != null)
        {
            dtTicketRehabilitados = (DataTable)ViewState["dtTicketR"];

            for (int i = 0; i < dtTicketRehabilitados.Rows.Count; i++)
            {
                if (codigoTicket.ToUpper().Trim().Equals(dtTicketRehabilitados.Rows[i]["Cod_Numero_Ticket"].ToString().ToUpper().Trim()))
                {
                    lblErrorTicket.Text = "Codigo de Ticket ya ingresado";
                    return;
                }
            }


            row = dtTicketRehabilitados.NewRow();
            row["Cod_Numero_Ticket"] = dtTicketDetalle.Rows[0].ItemArray[0];
            row["Dsc_Tipo_Ticket"] = dtTicketDetalle.Rows[0].ItemArray[17];
            row["Dsc_Compania"] = dtTicketDetalle.Rows[0].ItemArray[1];
            row["Dsc_Num_Vuelo"] = dtTicketDetalle.Rows[0].ItemArray[2];
            row["Fch_Vuelo"] = dtTicketDetalle.Rows[0].ItemArray[6];
            row["FHCreacion"] = dtTicketDetalle.Rows[0]["FHReh"];
            row["Dsc_Campo"] = dtTicketDetalle.Rows[0].ItemArray[7];

            dtTicketRehabilitados.Rows.Add(row);
            dtTicketRehabilitados.AcceptChanges();
            //row["Numero"] = dtTicketRehabilitados.Rows.Count + 1;
        }
        else
        {
            dtTicketRehabilitados = dtTicketDetalle;
        }

        
        ViewState["dtTicketR"] = dtTicketRehabilitados;

        grvTicket.DataSource = dtTicketRehabilitados;
        grvTicket.DataBind();
    }

    protected void rbTicket_CheckedChanged(object sender, EventArgs e)
    {
        if (rbTicket.Checked)
        {
            ibtnBuscar.Enabled = false;
            ibtnAgregar.Enabled = true;
        }
    }

    protected void rbFiltro_CheckedChanged(object sender, EventArgs e)
    {
        if (rbFiltro.Checked)
        {
            ibtnBuscar.Enabled = true;
            ibtnAgregar.Enabled = false;
        }
    }
}
