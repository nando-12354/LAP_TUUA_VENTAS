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
using LAP.TUUA.UTIL;
using LAP.TUUA.CONTROL;
using System.Collections.Generic;
using System.Xml;
using LAP.TUUA.PRINTER;


public partial class Ope_TicketContingencia : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected TipoTicket objTipoTicket;
    protected decimal Imp_TotPagBase;
    protected bool Flg_IsAllOk;
    protected VentaMasiva objVentaMasiva;
    protected int Can_Ticket;
    protected bool Flg_Error;
    protected Hashtable htParametro;
    protected BO_Administracion objBOOperacion;
    protected DataTable dtPrecioTicket;
    protected String sCodPrecioActual;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        try
        {
            if (!IsPostBack)
            {
                btnGenerar.Text = htLabels["opeGenContingencia.btnGenerar"].ToString();
                lblTipoTicket.Text = htLabels["opeGenContingencia.lblTipoTicket"].ToString();
                lblCantidad.Text = htLabels["opeGenContingencia.lblCantidad"].ToString();
                lblPrecioTicket.Text = htLabels["opeGenContingencia.lblPrecioTicket"].ToString();
                //lblPrecioTotal.Text = htLabels["opeGenContingencia.lblPrecioTotal"].ToString();
                lblFechaActual.Text = htLabels["opeGenContingencia.lblFechaActual"].ToString();

                rbAdulto.Text = htLabels["opeGenContingencia.rbAdulto"].ToString();
                rbInfante.Text = htLabels["opeGenContingencia.rbInfante"].ToString();
                rbInter.Text = htLabels["opeGenContingencia.rbInter"].ToString();
                rbNacional.Text = htLabels["opeGenContingencia.rbNacional"].ToString();
                rbNormal.Text = htLabels["opeGenContingencia.rbNormal"].ToString();
                rbTrans.Text = htLabels["opeGenContingencia.rbTrans"].ToString();

                btnGenerar_ConfirmButtonExtender.ConfirmText = htLabels["opeGenContingencia.msgConfirm"].ToString();

                MostrarTipoTickets();
                lblFecActVal.Text = DateTime.Now.ToShortDateString();
                MostrarPrecioTicketDefault();
                MostrarPrecioTicket();
            }
            objTipoTicket = (TipoTicket)Session["objTipoTicket"] != null ? (TipoTicket)Session["objTipoTicket"] : null;
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

    private void MostrarPrecioTicket()
    {
        objBOOperacion = new BO_Administracion();        
        dtPrecioTicket = objBOOperacion.ObtenerPrecioTicket(String.Empty);

        if (objTipoTicket != null && dtPrecioTicket != null && dtPrecioTicket.Rows.Count > 0)
        {
            dtPrecioTicket.Columns.Add(new DataColumn("DesPrecio"));

            var lqList = from rowConsulta in dtPrecioTicket.AsEnumerable()
                         where rowConsulta.Field<String>("Cod_Tipo_Ticket") == objTipoTicket.SCodTipoTicket
                         select formatearRegistro(rowConsulta);
            
            dtPrecioTicket = lqList.CopyToDataTable();
            cmbPrecioTicket.DataSource = dtPrecioTicket;
            cmbPrecioTicket.DataTextField = "DesPrecio";
            cmbPrecioTicket.DataValueField = "Cod_Precio_Ticket";
            cmbPrecioTicket.DataBind();
            cmbPrecioTicket.SelectedValue = sCodPrecioActual;
            obtenerPrecioTicket(dtPrecioTicket, cmbPrecioTicket.SelectedValue);
            ViewState["PreTicket"] = dtPrecioTicket;
        }
        else
        {
            cmbPrecioTicket.Items.Clear();
            cmbPrecioTicket.Items.Insert(0, new ListItem("- No Disponible -", String.Empty));
            cmbPrecioTicket.DataBind();
        }
    }

    private DataRow formatearRegistro(DataRow objRow)
    {
        String strDesEstado = string.Empty;

        if (objRow["Tip_Estado"].ToString().Trim().Equals("1"))
        {
            strDesEstado = "(Actual)";
            sCodPrecioActual = objRow["Cod_Precio_Ticket"].ToString().Trim();
        }
        else
        {
            strDesEstado = "(Programado)";
            
        }
        
        objRow["DesPrecio"] = String.Format("{0} {1} {2} ", new String[3] { objRow["Dsc_Simbolo"].ToString().Trim(), objRow["Imp_Precio"].ToString().Trim(), strDesEstado }); 
        //objRow["DesPrecio"] = String.Concat(new String[5] { objRow["Dsc_Simbolo"].ToString().Trim(), " ", objRow["Imp_Precio"].ToString().Trim(), " ", sDesEstado });

        return objRow; 
    }

    private void obtenerPrecioTicket(DataTable dtPrecioTicket, String sCodPrecio)
    {
        DataTable dtRegistro = new DataTable();

        if (objTipoTicket != null && txtCantidad.Text != string.Empty)
        {
            var lqList = from rowConsulta in dtPrecioTicket.AsEnumerable()
                         where rowConsulta.Field<String>("Cod_Precio_Ticket") == sCodPrecio
                         select formatearRegistro(rowConsulta);

            dtRegistro = lqList.CopyToDataTable();

            foreach (DataRow drRegistro in dtRegistro.Rows)
            {
                objTipoTicket.DImpPrecio = Convert.ToDecimal(drRegistro["Imp_Precio"]);
                objTipoTicket.SCodMoneda = drRegistro["Cod_Moneda"].ToString().Trim();
                objTipoTicket.Dsc_Simbolo = drRegistro["Dsc_Simbolo"].ToString().Trim();
            }

            Imp_TotPagBase = decimal.Parse(txtCantidad.Text) * objTipoTicket.DImpPrecio; 
        }       
    }


    private bool MostrarPrecioTicketDefault()
    {
        Session["Tip_Vuelo"] = Define.NACIONAL;
        Session["Tip_Pasajero"] = Define.ADULTO;
        Session["Tip_Transbordo"] = Define.NORMAL;
        objBOOpera = new BO_Operacion();
        objTipoTicket = objBOOpera.ObtenerPrecioTicket(Define.NACIONAL, Define.ADULTO, Define.NORMAL);
        if (objTipoTicket == null)
        {
            lblMensajeError.Text = htLabels["opeVentaCredito.msgTipoTicket"].ToString();
            Session["Flg_IsAllOk"] = false;
            return false;
        }
        Session["Flg_IsAllOk"] = true;
        Session["objTipoTicket"] = objTipoTicket;
        //lblSimbolo.Text = objTipoTicket.Dsc_Simbolo + " ";
        this.lblPrecTicketVal.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
        //lblPrecTotVal.Text = "0.00";
        return true;
    }

    private void MostrarTipoTickets()
    {
        objBOOpera = new BO_Operacion();
        List<TipoTicket> lista = objBOOpera.ListarTipoTickets();
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i].STipTrasbordo == Define.TRANSFERENCIA)
            {
                pnlTiptrasbordo.Visible = true;
                return;
            }
        }
        pnlTiptrasbordo.Visible = false;
    }

    private void Limpiar()
    {
        rbAdulto.Checked = true;
        rbNacional.Checked = true;
        rbNormal.Checked = true;
        rbInfante.Checked = false;
        rbInter.Checked = false;
        rbTrans.Checked = false;
    }

    protected void rbNacional_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNacional.Checked)
        {
            Session["Tip_Vuelo"] = Define.NACIONAL;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
            
        }
    }

    protected void rbInter_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInter.Checked)
        {
            Session["Tip_Vuelo"] = Define.INTERNACIONAL;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
        }
    }

    protected void rbNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNormal.Checked)
        {
            Session["Tip_Transbordo"] = Define.NORMAL;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
        }
    }

    protected void rbTrans_CheckedChanged(object sender, EventArgs e)
    {
        if (rbTrans.Checked)
        {
            Session["Tip_Transbordo"] = Define.TRANSFERENCIA;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
        }
    }

    protected void rbAdulto_CheckedChanged(object sender, EventArgs e)
    {
        if (rbAdulto.Checked)
        {
            Session["Tip_Pasajero"] = Define.ADULTO;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
        }
    }

    protected void rbInfante_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInfante.Checked)
        {
            Session["Tip_Pasajero"] = Define.INFANTE;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
            MostrarPrecioTicket();
        }
    }

    private void MostrarPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
    {
        objBOOpera = new BO_Operacion();
        objTipoTicket = objBOOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
        if (objTipoTicket != null)
        {
            this.lblPrecTicketVal.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
            if (txtCantidad.Text != "")
            {
                try
                {
                    Imp_TotPagBase = decimal.Parse(txtCantidad.Text) * objTipoTicket.DImpPrecio;
                    //lblPrecTotVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
                }
                catch
                {
                    //lblPrecTotVal.Text = "0.00";
                }
            }
            lblMensajeError.Text = "";
            Session["objTipoTicket"] = objTipoTicket;
            Session["Flg_IsAllOk"] = true;
        }
        else
        {
            Session["Flg_IsAllOk"] = false;
            lblMensajeError.Text = htLabels["opeGenContingencia.msgTipoTicket"].ToString();
        }
    }

    //protected void txtCantidad_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Imp_TotPagBase = decimal.Parse(txtCantidad.Text) * objTipoTicket.DImpPrecio;
    //        //lblPrecTotVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
    //    }
    //    catch
    //    {
    //        //lblPrecTotVal.Text = "0.00";
    //    }
    //    //lblSimbolo.Text = objTipoTicket.Dsc_Simbolo + " ";
    //}

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (!Validar())
        {
            return;
        }
        try
        {
            dtPrecioTicket = (DataTable)ViewState["PreTicket"];
            String strCodPrecioTicket = cmbPrecioTicket.SelectedValue;
            obtenerPrecioTicket(dtPrecioTicket, strCodPrecioTicket);

            string strUsuario = (string)Session["Cod_Usuario"];
            objBOOpera = new BO_Operacion();
            string strListaTickets = "";
            string strFecVence = "";
            Hashtable htParametro = (Hashtable)Session["htParametro"];
            string strVueloDefault = (string)htParametro[Property.htProperty[Define.ID_PARAM_VUELO_DEFAULT]];
            objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            if (!objBOOpera.EmitirContingencia(Can_Ticket, objTipoTicket, strUsuario, null, ref strListaTickets, ref strFecVence, strCodPrecioTicket))
            {
                lblMensajeError.Text = objBOOpera.Dsc_Message;
            }
            else
            {
                // Comentado (GGarcia-20090924)
                //omb.ShowMessage(htLabels["opeGenContingencia.msgTrxOK"].ToString(), "Emision Contingencia", "Ope_TicketContingencia.aspx");

                // se guarda en sesion la pagina 
                //Session["Pagina_PreImpresion"] = "Ope_TicketContingencia.aspx";

                // rutina de impresion (GGarcia-20090924)
                Imprimir(strListaTickets, strFecVence);

                // invocar a la pagina de impresion (GGarcia-20090924)
                //Response.Write("<script>window.open('Ope_Impresion.aspx','_self')</script>");
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
    /// Metodo que imprime la lista de tickets y voucher
    /// <param name="listaCodigoTickets">Lista codigo de tickets</param>
    /// </summary>
    private void Imprimir(string listaCodigoTickets, string fechaVencimientoTicket)
    {
        Print objPrint = new Print();

        // 1ro: ************************************** Imprimir STICKER **************************************
        // obtener el nombre de la operacion venta ticket stickers soles[/dolares/euros 
        string nombre = Define.ID_PRINTER_DOCUM_VENTATICKETCONTINGENCIASTICKER; //objPrint.obtenerOperacion(0, (Hashtable)Session["htParamImp"], objTipoTicket.SCodMoneda);

        // obtiene el nodo segun el nombre
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);

        // configuracion de la impresora a utilizar
        String configImpSticker = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], nombre);

        //---
        if(Session["PuertoSticker"]!=null && !Session["PuertoSticker"].ToString().Equals(String.Empty))
        {
            configImpSticker = Session["PuertoSticker"].ToString() + "," + configImpSticker.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // carga los parametros a imprimir con la impresora de sticker
        Hashtable htPrintData = new Hashtable();
        cargarParametrosImpresion(0, htPrintData, listaCodigoTickets, fechaVencimientoTicket);

        // obtiene la data a imprimir con la impresora de sticker y guardarla en una variable de sesion
        String dataSticker = objPrint.ObtenerDataFormateada(htPrintData, nodo);

        // 2do: ************************************** Imprimir VOUCHER **************************************
        // obtiene el nodo segun el nombre

        nombre = Define.ID_PRINTER_DOCUM_VENTATICKETCONTINGENCIA;

        nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);

        // configuracion de la impresora a utilizar
        String configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], nombre);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // carga los parametros a imprimir con la impresora de voucher
        cargarParametrosImpresion(1, htPrintData, listaCodigoTickets, "");

        // obtiene la data a imprimir con la impresora de voucher 
        String dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);

        //int copias = objPrint.ObtenerCopiasVoucher(nodo);

        Session["dataVoucher"] = dataVoucher;
        Session["dataSticker"] = dataSticker;

        Session["listaCodigoTickets"] = listaCodigoTickets;
        //tam_Ticket= (string)Property.htProperty[Define.TAM_TICKET]
        Session["xmlFormatoVoucher"] = nodo.OuterXml;

        /*
        //Con Session se obtiene el € y el Request.QueryString no lo hace (?), sin embargo al pasarlo al applet, lo obtiene como (?).
        //Session["dsc_Simbolo"] = objTipoTicket.Dsc_Simbolo;
        if (objTipoTicket.Dsc_Simbolo.Equals("€"))
        {
            objTipoTicket.Dsc_Simbolo = "EuroSymbol";
        }
        */

        Response.Redirect("Ope_Impresion_ConStickers.aspx?" +
            "flagImpSticker=1" +
            "&" + "flagImpVoucher=1" +
            //"&" + "copiasVoucher=" + copias +
            "&" + "configImpSticker=" + configImpSticker +
            "&" + "configImpVoucher=" + configImpVoucher +
            "&" + Define.ID_PRINTER_PARAM_NOMBRE_CAJERO + "=" + Session["Nombre_Usuario"] +
            "&" + Define.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET + "=" + objTipoTicket.SNomTipoTicket +
            "&" + "imp_Precio=" + objTipoTicket.DImpPrecio +
            "&" + "dsc_Simbolo=" + objTipoTicket.Dsc_Simbolo +
            "&" + "operacion=" + Define.ESTADO_TICKET_PREEMITIDO +
            "&" + "Pagina_PreImpresion=Ope_TicketContingencia.aspx", false);
    }

    private void cargarParametrosImpresion(int flag, Hashtable htPrintData, string listaCodigoTickets, string fechaVencimientoTicket)
    {
        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        #region Sticker
        if (flag == 0)
        {
            // flag de tickets (se utiliza para que lea el nodo de manera diferente a la de voucher)         
            //htPrintData.Add(Define.ID_PRINTER_PARAM_FLAG_TICKET, "T");

            // cantidad de ticket
            //htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Ticket);
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, Can_Ticket);

            int contador = 0;
            Hashtable htCampo = (Hashtable)Session["Lista_Campo_Psjero"];
            // recorre cada codigo de ticket
            for (int i = 0; i < Can_Ticket; i++)
            {
                // fecha de vencimiento
                //htPrintData.Add(Define.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i, Function.FormatFecha(fechaVencimientoTicket));

                // codigo de ticket                
                htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, listaCodigoTickets.Substring(contador, intLongTicket));
                contador += intLongTicket;

                // monto 
                htPrintData.Add(Define.ID_PRINTER_PARAM_MONTO_PAGADO + "_" + i, Function.FormatDecimal(objTipoTicket.DImpPrecio));

                //EAG 29/12/2009
                htPrintData.Add("desc_moneda_1" + "_" + i, "");
                htPrintData.Add("desc_moneda_2" + "_" + i, objTipoTicket.Dsc_Moneda);
                //EAG
                //JCISNEROS
                htPrintData.Add("dsc_pasajero_" + i, htCampo["TipoPasajero" + objTipoTicket.STipPasajero].ToString());
            }
        }
        #endregion
        // si se utiliza la impresora de voucher
        else if (flag == 1)
        {
            // limpiar la lista de parametros a imprimir
            htPrintData.Clear();
            // nombre del cajero
            htPrintData.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);
            // descripcion del ticket
            htPrintData.Add(Define.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET, objTipoTicket.SNomTipoTicket);
            // cantidad de tickets vendidos
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Ticket.ToString());
            // preecio unitario del ticket vendido (con su simbolo)
            htPrintData.Add(Define.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET, Function.FormatDecimal(objTipoTicket.DImpPrecio) + " " + objTipoTicket.Dsc_Simbolo);
            // total a pagar (con su simbolo)
            htPrintData.Add(Define.ID_PRINTER_PARAM_TOTAL_PAGAR, Function.FormatDecimal((decimal)(objTipoTicket.DImpPrecio * Can_Ticket)) + " " + objTipoTicket.Dsc_Simbolo);

            //-- EAG 10/02/2010
            int q1 = Can_Ticket / 2;
            int q2 = Can_Ticket % 2;
            if (q2 != 0)
            {
                q1 = q1 + 1;
            }
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, q1.ToString());
            int contador = 0;
            // recorre cada codigo de ticket
            for (int i = 0, j = 0; i < Can_Ticket; i++)
            {
                /*
                htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, Num_Tickets.Substring(contador, intLongTicket));
                contador += intLongTicket;
                */
                if ((i + 1) % 2 == 0)//Par
                {
                    htPrintData.Add("codigo_ticket_par" + "_" + j, listaCodigoTickets.Substring(contador, intLongTicket));
                    contador += intLongTicket;
                    j++;
                }
                else//Impar
                {
                    htPrintData.Add("codigo_ticket_impar" + "_" + j, listaCodigoTickets.Substring(contador, intLongTicket));
                    contador += intLongTicket;
                }
            }
        }
    }

    private void RedirectTo(string pagina)
    {
        string redirectURL = Page.ResolveClientUrl(pagina);
        string script = "window.location = '" + redirectURL + "';";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
        //Response.Redirect("");

    }

    protected bool Validar()
    {
        htParametro = (Hashtable)Session["htParametro"];
        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        int intCanMaxTicket = Define.STRING_SIZE / intLongTicket;
        lblMensajeError.Text = "";

        if (cmbPrecioTicket.SelectedValue == string.Empty)
        {
            lblMensajeError.Text = htLabels["opeGenContingencia.msgTipoTicket"].ToString();
            return false;
        }

        if (txtCantidad.Text == "")
        {
            rfvCantidad.ErrorMessage = (string)htLabels["opeGenContingencia.rfvCantidad"];
            rfvCantidad.IsValid = false;
            return false;
        }
        Can_Ticket = Int32.Parse(txtCantidad.Text);
        if (Can_Ticket == 0)
        {
            rfvCantidad.ErrorMessage = (string)htLabels["opeGenContingencia.rfvCantidad"];
            rfvCantidad.IsValid = false;
            return false;
        }
        if (Can_Ticket > intCanMaxTicket)
        {
            lblMensajeError.Text = (string)htLabels["opeGenContingencia.msgMaxTicketSistema"];
            return false;
        }

        return (bool)Session["Flg_IsAllOk"];
    }

    protected void cmbPrecioTicket_SelectedIndexChanged(object sender, EventArgs e)
    {
        dtPrecioTicket = (DataTable)ViewState["PreTicket"];
        String strCodPrecioTicket = cmbPrecioTicket.SelectedValue;
        obtenerPrecioTicket(dtPrecioTicket, strCodPrecioTicket);
    }
}
