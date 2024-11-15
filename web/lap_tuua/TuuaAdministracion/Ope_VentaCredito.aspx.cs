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
using LAP.TUUA.PRINTER;
using System.Collections.Generic;
using System.Xml;

public partial class Ope_VentaCredito : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected TipoTicket objTipoTicket;
    protected decimal Imp_TotPagBase;
    protected bool Flg_IsAllOk;
    protected bool Flg_Error;
    protected VentaMasiva objVentaMasiva;
    protected Hashtable htParametro;
    protected string Dsc_Aerolinea;
    private string Cod_Turno;
    private BO_Administracion objAdministracion;
        
    private string strCodTurnoActual;
    private string strFlagTurno;    
    private string strCantTickets;
    private string strFecHorTurno;
    private DataTable monedaTbl;
    private string strCodUsuario;
    private string strCodTurno;
    private Hashtable listaParamImp;
    private CuadreTurno objCuadre;
    List<CuadreTurno> listaCuadre;
   
    protected void Page_Load(object sender, EventArgs e)
    {        

        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        try
        {
            objBOOpera = new BO_Operacion();
            objAdministracion = new BO_Administracion();

            if (!IsPostBack)
            {
                
                btnAceptar.Text = htLabels["musuario.btnAceptar"].ToString();
                lblTipoTicket.Text = htLabels["opeVentaCredito.lblTipoTicket"].ToString();
                lblCantidad.Text = htLabels["opeVentaCredito.lblCantidad"].ToString();
                lblCompania.Text = htLabels["opeVentaCredito.lblCompania"].ToString();
                lblRepteCia.Text = htLabels["opeVentaCredito.lblRepteCia"].ToString();
                lblFechaActual.Text = htLabels["opeVentaCredito.lblFechaActual"].ToString();
                lblNumVuelo.Text = htLabels["opeVentaCredito.lblNumVuelo"].ToString();
                lblPrecioTicket.Text = htLabels["opeVentaCredito.lblPrecioTicket"].ToString();
                //lblPrecioTotal.Text = htLabels["opeVentaCredito.lblPrecioTotal"].ToString();

                rbAdulto.Text = htLabels["opeVentaCredito.rbAdulto"].ToString();
                rbInfante.Text = htLabels["opeVentaCredito.rbInfante"].ToString();
                rbInter.Text = htLabels["opeVentaCredito.rbInter"].ToString();
                rbNacional.Text = htLabels["opeVentaCredito.rbNacional"].ToString();
                rbNormal.Text = htLabels["opeVentaCredito.rbNormal"].ToString();
                rbTrans.Text = htLabels["opeVentaCredito.rbTrans"].ToString();

                rfvCantidad.ErrorMessage = htLabels["opeVentaCredito.rfvCantidad"].ToString();
                rfvNumVuelo.ErrorMessage = htLabels["opeVentaCredito.rfvNumVuelo"].ToString();
                rfvRteCia.ErrorMessage = htLabels["opeVentaCredito.rfvRteCia"].ToString();
                
                if (CargarCompania())
                {
                    CargarComboReptes();
                    MostrarTipoTickets();
                    lblFecActVal.Text = DateTime.Now.ToShortDateString();
                    MostrarPrecioTicketDefault();
                }

                //Validacion si el usuario tiene un turno pendiente

                bool valida = validarTurnoPendiente();

               if (!valida)
                {
                    lblInfo.Text = htLabels["opeVentaCredito.msgTurnoPendiente"].ToString();
                    //this.btnAceptar.Enabled = false;
                }
                else
                {

                //Cargar Turno del Usuario si es que tubiera uno abierto y con cantidad de registros.
                string strCodUsuario = Convert.ToString(Session["Cod_Usuario"]);
                bool blnResultado = objBOOpera.ObtenerDetalleTurnoActual(strCodUsuario, ref strCantTickets, ref strCodTurnoActual, ref strFecHorTurno);
                if (strCodTurnoActual != null && strCodTurnoActual != String.Empty)
                {
                    lblTurno.Text = strCodTurnoActual;

                    if (strFecHorTurno.Length == 14)
                    {
                        lblTurno.Text = String.Format("{0} - Aperturado el dia: {1}/{2}/{3} {4}:{5}:{6}", new String[7] { lblTurno.Text, 
                                                                                                                      strFecHorTurno.Substring(6,2), 
                                                                                                                      strFecHorTurno.Substring(4,2), 
                                                                                                                      strFecHorTurno.Substring(0,4), 
                                                                                                                      strFecHorTurno.Substring(8,2), 
                                                                                                                      strFecHorTurno.Substring(10,2), 
                                                                                                                      strFecHorTurno.Substring(12,2)});
                    }

                    lblRegistro.Text = String.Format("{0} impuestos.", strCantTickets);
                    ViewState["TurnoActual"] = strCodTurnoActual;
                }
                else
                {
                    lblTurno.Text = "Sin turno pendiente.";
                    lblRegistro.Text = "0 impuetos.";
                }

                
            }
            }
            else
            {
                if (ViewState["TurnoActual"] != null)
                {
                    strCodTurnoActual = ViewState["TurnoActual"].ToString().Trim();
                }
                this.btnAceptar.Enabled = true;
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

    private bool validarTurnoPendiente(){
        bool turno = true; 
        BO_Operacion objOpera = new BO_Operacion();
        strCodUsuario = (string)Session["Cod_Usuario"];
        Turno objTurno = new Turno();
        objTurno = objOpera.obtenerTurnoIniciado(strCodUsuario);
        
          if (objTurno != null)
          {
              if (objTurno.SCodEquipo.Equals("V00000"))
              {
                  turno = true;
                  List<TurnoMonto> lstTurnoMonto = objOpera.ListarTurnoMontoxTurno(objTurno.SCodTurno);
                  for (int i = 0; i < lstTurnoMonto.Count; i++)
                  {
                      if (!lstTurnoMonto[i].Tip_Pago.Equals("X"))
                      {
                          turno = false;
                          break;
                      }
                  }
                      
              }
              else
              {
                  turno = false;
              }
              if (!turno)
              {
                  btnAceptar.Visible = false;
                  btnAceptar.Enabled = false;
                  UpdatePanel1.Visible = false;
              }
          }
          return turno;
    }

    private bool CargarCompania()
    {
        List<ModVentaComp> listaCompania = objBOOpera.ListarCompaniaxModVenta((string)Property.htProperty[Define.MOD_VENTA_MAS_CRED], null);
        if (listaCompania != null && listaCompania.Count > 0)
        {
            UpdatePanel1.Visible = true;
            btnAceptar.Visible = true;
            btnAceptar.Enabled = true;
            ddlCompania.DataSource = listaCompania;
            ddlCompania.DataTextField = "Dsc_Compania";
            ddlCompania.DataValueField = "SCodCompania";
            ddlCompania.DataBind();
            ddlCompania.SelectedIndex = 0;
            return true;
        }
        btnAceptar.Visible = false;
        btnAceptar.Enabled = false;
        UpdatePanel1.Visible = false;
        lblInfo.Text = htLabels["opeVentaCredito.msgCompania"].ToString();
        return false;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        //EAG 24/02/2010
        if (chkCierreTurno.Checked)
        {
            strFlagTurno = "1";
        }
        else
        {
            strFlagTurno = "0";
        }

        Compania compania = objAdministracion.obtenerCompañiaxcodigo(ddlCompania.SelectedItem.Value);
        String estado = compania.STipEstado;
        if (!estado.Trim().Equals(Define.COMPANIA_ACTIVO))
        {
            lblMensajeError.Text = (string)htLabels["venta.error.companiaanulada"]; 
        }
        else
        {
            if (Validar())
            {
                string strModVenta = Property.htProperty[Define.MOD_VENTA_MAS_CRED].ToString();
                string strPtoVenta = Property.htProperty[Define.COD_PTOVENTA_WEB].ToString();
                string strNumVuelo = txtNumVuelo.Text;
                string strListaTickets = "";
                string strEmpresa = ddlCompania.SelectedItem.Text;
                string strRepte = ddlRepteCia.SelectedItem.Text;
                Dsc_Aerolinea = ddlCompania.SelectedItem.Text;
                objVentaMasiva.SCodCompania = ddlCompania.SelectedValue.ToString();
                objVentaMasiva.SCodMoneda = objTipoTicket.SCodMoneda;
                objVentaMasiva.SCodUsuario = (string)Session["Cod_Usuario"];
                objVentaMasiva.Tip_Pago = Define.TIP_PAGO_CREDITO;
                //string temp = (string)Session["Cod_SubModulo"];
                //string temp1 = (string)Session["Cod_Modulo"];
                objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                if (!objBOOpera.RegistrarVentaMasiva(strModVenta, objVentaMasiva, ref Cod_Turno, objTipoTicket, null, strNumVuelo, null, strPtoVenta, strEmpresa, strRepte, ref strListaTickets, strCodTurnoActual, strFlagTurno))
                {
                    if (objBOOpera.Dsc_Message != "")
                    {
                        lblMensajeError.Text = objBOOpera.Dsc_Message;
                    }
                    else
                    {
                        string[] cadena = objBOOpera.Dsc_Mensaje.Split('-');
                        int resultado = Convert.ToInt32(cadena[1]) - Convert.ToInt32(cadena[2]);
                        lblMensajeError.Text = string.Format((string)LabelConfig.htLabels["opeVentaCredito.msgMaxVenta"], strEmpresa, cadena[1], cadena[2], resultado.ToString());
                    }
                }
                else
                {
                    objVentaMasiva.DImpMontoVenta = objVentaMasiva.DCanVenta * objTipoTicket.DImpPrecio;

                    if (Cod_Turno != string.Empty && Cod_Turno != null)
                    {
                        ViewState["TurnoActual"] = Cod_Turno;
                    }

                    
                    // Comentado (GGarcia-20090924)
                    //omb.ShowMessage(htLabels["opeVentaCredito.msgTrxOK"].ToString(), htLabels["opeVentaCredito.lblVentaCredito"].ToString(), "Ope_VentaCredito.aspx");

                    // se guarda en sesion la pagina 
                    //Session["Pagina_PreImpresion"] = "Ope_VentaCredito.aspx";

                    // rutina de impresion (GGarcia-20090924)
                    
                   Imprimir(strListaTickets);
 

                    // invocar a la pagina de impresion (GGarcia-20090924)
                    //Response.Write("<script>window.open('Ope_Impresion.aspx','_self')</script>");
                    //RedirectTo("Ope_Impresion.aspx");
                }
            }            
        }
    }

    /// <summary>
    /// Metodo que imprime la lista de tickets y el detalle de voucher
    /// <param name="listaCodigoTickets">Lista codigo de tickets</param>
    /// </summary>
    private void Imprimir(string listaCodigoTickets)
    {
        Print objPrint = new Print();

        // 1ro: ************************************** Imprimir STICKER **************************************
        // obtener el nombre de la operacion venta ticket stickers soles[/dolares/euros 
        string nombre = Define.ID_PRINTER_DOCUM_VENTATICKETSTICKER; //objPrint.obtenerOperacion(0, (Hashtable)Session["htParamImp"], objTipoTicket.SCodMoneda);

        // obtiene el nodo segun el nombre
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);

        // configuracion de la impresora a utilizar
        String configImpSticker = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], nombre);

        //---
        if (Session["PuertoSticker"] != null && !Session["PuertoSticker"].ToString().Equals(String.Empty))
        {
            configImpSticker = Session["PuertoSticker"].ToString() + "," + configImpSticker.Split(new char[] { ',' }, 2)[1];
        }
        //---

        // carga los parametros a imprimir con la impresora de sticker
        Hashtable htPrintData = new Hashtable();
        cargarParametrosImpresion(0, htPrintData, listaCodigoTickets);

        // obtiene la data a imprimir con la impresora de sticker y guardarla en una variable de sesion
        String dataSticker = objPrint.ObtenerDataFormateada(htPrintData, nodo);

        // 2do: ************************************** Imprimir VOUCHER **************************************
        // obtiene el nodo segun el nombre
        
        nombre = Define.ID_PRINTER_DOCUM_VENTATICKETMASIVACREDITO;

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
        cargarParametrosImpresion(1, htPrintData, listaCodigoTickets);

        // obtiene la data a imprimir con la impresora de voucher y guardarla en una variable de sesion
        String dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);
        //int copias = objPrint.ObtenerCopiasVoucher(nodo);

        Session["dataVoucher"] = dataVoucher;
        Session["dataSticker"] = dataSticker;

        Session["listaCodigoTickets"] = listaCodigoTickets;
        //tam_Ticket= (string)Property.htProperty[Define.TAM_TICKET]
        Session["xmlFormatoVoucher"] = nodo.OuterXml;

        if (chkCierreTurno.Checked)
        {
            //INICIO CMONTES
            BO_Turno objBOTurno = new BO_Turno();

            //// 1ro: ************************************** Imprimir VOUCHER CUADRE M.N.**************************************
            //// obtiene el nodo segun el nombre
            String strNombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_MN;
            XmlElement xmlNodoFin = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], strNombre);
            
            listaCuadre = new List<CuadreTurno>();
            int intTicketEfeInt = 0;
            decimal decTicketEfeInt = 0;
            int intTicketTraInt = 0;
            decimal decTicketTraInt = 0;
            int intTicketDebInt = 0;
            decimal decTicketDebInt = 0;
            int intTicketCreInt = 0;
            decimal decTicketCreInt = 0;
            int intTicketCheInt = 0;
            int intTicketEfeNac = 0;
            decimal decTicketEfeNac = 0;
            int intTicketTraNac = 0;
            decimal decTicketTraNac = 0;
            int intTicketDebNac = 0;
            decimal decTicketDebNac = 0;
            int intTicketCreNac = 0;
            decimal decTicketCreNac = 0;
            int intTicketCheNac = 0;
            decimal decTicketCheNac = 0;
            decimal decRecaudadoFin = 0;

            decimal Imp_EfectivoIni = decimal.Zero;
            int Can_TicketInt = 0;
            int Can_TicketNac = 0;
            decimal Imp_TicketInt = decimal.Zero;
            decimal Imp_TicketNac = decimal.Zero;
            int Can_IngCaja = 0;
            decimal Imp_IngCaja = decimal.Zero;
            int Can_VentaMoneda = 0;
            decimal Imp_VentaMoneda = decimal.Zero;
            int Can_EgreCaja = 0;
            decimal Imp_EgreCaja = decimal.Zero;
            int Can_CompraMoneda = 0;
            decimal Imp_CompraMoneda = decimal.Zero;
            decimal Imp_EfectivoFinal = decimal.Zero;
            int Can_AnulaInt = 0;
            int Can_AnulaNac = 0;
            int Can_InfanteInt = 0;
            int Can_InfanteNac = 0;
            int Can_CreditoInt = 0;
            int Can_CreditoNac = 0;
            decimal Imp_CreditoInt = decimal.Zero;
            decimal Imp_CreditoNac = decimal.Zero;

           // List<Moneda> listaMoneda = objBOTurno.ListarMonedas();
            List<Moneda> listaMoneda = objBOTurno.ListarMonedasxTipoTicket();

            for (int k = 0; k < listaMoneda.Count; k++)
            {
                decimal decTicketCheInt = 0;
                objCuadre = new CuadreTurno();
                objCuadre.Cod_Moneda = listaMoneda[k].SCodMoneda;
                objCuadre.Dsc_Moneda = listaMoneda[k].SDscMoneda;
                objCuadre.Dsc_Simbolo = listaMoneda[k].SDscSimbolo;
                objBOTurno.ListarCuadreTurno2(objCuadre.Cod_Moneda, ViewState["TurnoActual"].ToString().Trim(), ref  Imp_EfectivoIni, ref  Can_TicketInt, ref  Can_TicketNac, ref  Imp_TicketInt, ref  Imp_TicketNac, ref  Can_IngCaja, ref  Imp_IngCaja, ref  Can_VentaMoneda, ref  Imp_VentaMoneda,
                                              ref  Can_EgreCaja, ref  Imp_EgreCaja, ref  Can_CompraMoneda, ref  Imp_CompraMoneda, ref  Imp_EfectivoFinal, ref Can_AnulaInt, ref  Can_AnulaNac, ref Can_InfanteInt, ref  Can_InfanteNac, ref  Can_CreditoInt, ref  Can_CreditoNac, ref  Imp_CreditoInt, ref  Imp_CreditoNac,
                                              ref  intTicketEfeInt, ref  decTicketEfeInt, ref  intTicketTraInt, ref  decTicketTraInt, ref  intTicketDebInt, ref  decTicketDebInt, ref  intTicketCreInt, ref  decTicketCreInt, ref  intTicketCheInt, ref  decTicketCheInt,
                                              ref  intTicketEfeNac, ref  decTicketEfeNac, ref  intTicketTraNac, ref  decTicketTraNac, ref  intTicketDebNac, ref  decTicketDebNac, ref  intTicketCreNac, ref  decTicketCreNac, ref  intTicketCheNac, ref  decTicketCheNac,
                                              ref  decRecaudadoFin
                                             );

                objCuadre.Can_AnulaInt = Can_AnulaInt;
                objCuadre.Can_AnulaNac = Can_AnulaNac;
                objCuadre.Can_CompraMoneda = Can_CompraMoneda;
                objCuadre.Can_CreditoInt = Can_CreditoInt;
                objCuadre.Can_CreditoNac = Can_CreditoNac;
                objCuadre.Can_EgreCaja = Can_EgreCaja;
                objCuadre.Can_InfanteInt = Can_InfanteInt;
                objCuadre.Can_InfanteNac = Can_InfanteNac;
                objCuadre.Can_IngCaja = Can_IngCaja;
                objCuadre.Can_TicketInt = Can_TicketInt;
                objCuadre.Can_TicketNac = Can_TicketNac;
                objCuadre.Imp_CompraMoneda = Imp_CompraMoneda;
                objCuadre.Imp_CreditoInt = Imp_CreditoInt;
                objCuadre.Imp_CreditoNac = Imp_CreditoNac;
                objCuadre.Imp_EfectivoFinal = Imp_EfectivoFinal;
                objCuadre.Imp_EfectivoIni = Imp_EfectivoIni;
                objCuadre.Imp_EgreCaja = Imp_EgreCaja;
                objCuadre.Imp_TicketInt = Imp_TicketInt;
                objCuadre.Imp_TicketNac = Imp_TicketNac;
                objCuadre.Imp_IngCaja = Imp_IngCaja;
                objCuadre.Can_VentaMoneda = Can_VentaMoneda;
                objCuadre.Imp_VentaMoneda = Imp_VentaMoneda;
                objCuadre.Can_Ticket_EfeInt = intTicketEfeInt;
                objCuadre.Imp_Ticket_EfeInt = decTicketEfeInt;
                objCuadre.Can_Ticket_TraInt = intTicketTraInt;
                objCuadre.Imp_Ticket_TraInt = decTicketTraInt;
                objCuadre.Can_Ticket_DebInt = intTicketDebInt;
                objCuadre.Imp_Ticket_DebInt = decTicketDebInt;
                objCuadre.Can_Ticket_CreInt = intTicketCreInt;
                objCuadre.Imp_Ticket_CreInt = decTicketCreInt;
                objCuadre.Can_Ticket_CheInt = intTicketCheInt;
                objCuadre.Imp_Ticket_CheInt = decTicketCheInt;
                objCuadre.Can_Ticket_EfeNac = intTicketEfeNac;
                objCuadre.Imp_Ticket_EfeNac = decTicketEfeNac;
                objCuadre.Can_Ticket_TraNac = intTicketTraNac;
                objCuadre.Imp_Ticket_TraNac = decTicketTraNac;
                objCuadre.Can_Ticket_DebNac = intTicketDebNac;
                objCuadre.Imp_Ticket_DebNac = decTicketDebNac;
                objCuadre.Can_Ticket_CreNac = intTicketCreNac;
                objCuadre.Imp_Ticket_CreNac = decTicketCreNac;
                objCuadre.Can_Ticket_CheNac = intTicketCheNac;
                objCuadre.Imp_Ticket_CheNac = decTicketCheNac;
                objCuadre.Imp_Recaudado_Fin = decRecaudadoFin;
                listaCuadre.Add(objCuadre);
            }

            Session["dataVoucher"] = dataVoucher + "" + ImprimirCierreTurno();
        }

        
        

        //FIN CMONTES

        /*
        if (objTipoTicket.Dsc_Simbolo.Equals("€"))
        {
            objTipoTicket.Dsc_Simbolo = "EuroSymbol";
        }
        */

        String compania = Dsc_Aerolinea;
        String nrovuelo = txtNumVuelo.Text;

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
            "&" + "operacion=" + Define.ESTADO_TICKET_EMITIDO +
            "&" + "compania=" + compania +
            "&" + "nrovuelo=" + nrovuelo +
            "&" + "Pagina_PreImpresion=Ope_VentaCredito.aspx", false);
    }

    private void cargarParametrosImpresion(int flag, Hashtable htPrintData, string listaCodigoTickets)
    {
        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        #region Sticker
        if (flag == 0)
        {
            // flag de tickets (se utiliza para que lea el nodo de manera diferente a la de voucher)
            //htPrintData.Add(Define.ID_PRINTER_PARAM_FLAG_TICKET, "T");

            // cantidad de ticket
            //htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, objVentaMasiva.DCanVenta.ToString());
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, objVentaMasiva.DCanVenta.ToString());
            Hashtable htCampo = (Hashtable)Session["Lista_Campo_Psjero"];
            int contador = 0;
            // recorre cada codigo de ticket
            for (int i = 0; i < objVentaMasiva.DCanVenta; i++)
            {
                // fecha de vencimiento
                htPrintData.Add(Define.ID_PRINTER_PARAM_FECHA_VENCIMIENTO + "_" + i, Function.FormatFecha(objVentaMasiva.DtFchVenta));

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
            String compania = Dsc_Aerolinea;
            String nrovuelo = txtNumVuelo.Text;
            // limpiar la lista de parametros a imprimir
            htPrintData.Clear();
            // nombre del cajero
            htPrintData.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);
            // descripcion del ticket
            htPrintData.Add(Define.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET, objTipoTicket.SNomTipoTicket);
            // cantidad de tickets vendidos
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, objVentaMasiva.DCanVenta.ToString());
            // preecio unitario del ticket vendido (con su simbolo)
            htPrintData.Add(Define.ID_PRINTER_PARAM_PRECIO_UNITARIO_TICKET, Function.FormatDecimal(objTipoTicket.DImpPrecio) + " " + objTipoTicket.Dsc_Simbolo);
            // total a pagar (con su simbolo)
            htPrintData.Add(Define.ID_PRINTER_PARAM_TOTAL_PAGAR, Function.FormatDecimal(objVentaMasiva.DImpMontoVenta) + " " + objTipoTicket.Dsc_Simbolo);
            htPrintData.Add("compania", compania);
            htPrintData.Add("nrovuelo", nrovuelo);
            htPrintData.Add("codigo_turno", Cod_Turno);

            //-- EAG 10/02/2010
            int q1 = objVentaMasiva.DCanVenta / 2;
            int q2 = objVentaMasiva.DCanVenta % 2;
            if (q2 != 0)
            {
                q1 = q1 + 1;
            }
            htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, q1.ToString());
            int contador = 0;
            // recorre cada codigo de ticket
            for (int i = 0, j = 0; i < objVentaMasiva.DCanVenta; i++)
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
    }

    protected bool Validar()
    {
        objVentaMasiva = new VentaMasiva();
        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        int intCanMaxTicket = Define.STRING_SIZE / intLongTicket;
        lblMensajeError.Text = "";

        if (txtCantidad.Text == "")
        {
            rfvCantidad.ErrorMessage = "Ingresar cantidad de tickets válido.";
            rfvCantidad.IsValid = false;
            return false;
        }
        objVentaMasiva.DCanVenta = Int32.Parse(txtCantidad.Text);
        if (objVentaMasiva.DCanVenta == 0)
        {
            rfvCantidad.ErrorMessage = "Ingresar cantidad de tickets válido.";
            rfvCantidad.IsValid = false;
            return false;
        }
        if (ddlRepteCia.SelectedItem.Text == "-Seleccione-")
        {
            rfvRteCia.ErrorMessage = "Compañía sin representantes asignado.";
            rfvRteCia.IsValid = false;
            return false;
        }

        if (txtNumVuelo.Text == "")
        {
            return false;
        }
        
        try
        {
            objVentaMasiva.DImpMontoVenta = Imp_TotPagBase;// decimal.Parse(lblPrecTotVal.Text.Trim());
        }
        catch
        {
            return false;
        }
        if (objVentaMasiva.DCanVenta > intCanMaxTicket)
        {
            lblMensajeError.Text = (string)htLabels["opeVentaCredito.msgMaxTicketSistema"];
            return false;
        }
        if (!ExisteTasaVentaMEBase())
        {
            return false;
        }
        return (bool)Session["Flg_IsAllOk"];
    }

    private bool MostrarPrecioTicketDefault()
    {
        Session["Tip_Vuelo"] = Define.NACIONAL;
        Session["Tip_Pasajero"] = Define.ADULTO;
        Session["Tip_Transbordo"] = Define.NORMAL;
        objTipoTicket = objBOOpera.ObtenerPrecioTicket(Define.NACIONAL, Define.ADULTO, Define.NORMAL);
        if (objTipoTicket == null)
        {
            lblMensajeError.Text = htLabels["opeVentaCredito.msgTipoTicket"].ToString();
            Session["Flg_IsAllOk"] = false;
            return false;
        }
        Session["objTipoTicket"] = objTipoTicket;
        Session["Flg_IsAllOk"] = true;
        //lblSimbolo.Text = objTipoTicket.Dsc_Simbolo + " ";
        this.lblPrecTicketVal.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
        //lblPrecTotVal.Text = "0.00";
        return true;
    }

    private void MostrarTipoTickets()
    {
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
        }
    }

    protected void rbInter_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInter.Checked)
        {
            Session["Tip_Vuelo"] = Define.INTERNACIONAL;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        }
    }

    protected void rbNormal_CheckedChanged(object sender, EventArgs e)
    {
        if (rbNormal.Checked)
        {
            Session["Tip_Transbordo"] = Define.NORMAL;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        }
    }

    protected void rbTrans_CheckedChanged(object sender, EventArgs e)
    {
        if (rbTrans.Checked)
        {
            Session["Tip_Transbordo"] = Define.TRANSFERENCIA;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        }
    }

    protected void rbAdulto_CheckedChanged(object sender, EventArgs e)
    {
        if (rbAdulto.Checked)
        {
            Session["Tip_Pasajero"] = Define.ADULTO;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        }
    }

    protected void rbInfante_CheckedChanged(object sender, EventArgs e)
    {
        if (rbInfante.Checked)
        {
            Session["Tip_Pasajero"] = Define.INFANTE;
            MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        }
    }

    private void MostrarPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
    {
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
            Session["objTipoTicket"] = null;
            Session["Flg_IsAllOk"] = false;
            lblMensajeError.Text = htLabels["opeVentaCredito.msgTipoTicket"].ToString();
        }
    }

    protected void ddlCompania_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompania.SelectedItem != null)
        {
            CargarComboReptes();
        }
    }

    protected void txtCantidad_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Imp_TotPagBase = decimal.Parse(txtCantidad.Text) * objTipoTicket.DImpPrecio;
            //lblPrecTotVal.Text = Function.FormatDecimal(Imp_TotPagBase, 2).ToString();
            //lblSimbolo.Text = objTipoTicket.Dsc_Simbolo + " ";
        }
        catch
        {
            //lblPrecTotVal.Text = "0.00";
        }

    }

    private void CargarComboReptes()
    {
        List<RepresentantCia> lista = objBOOpera.ListarRepteCia(ddlCompania.SelectedItem.Value.ToString());
        RepresentantCia objRepteCia = new RepresentantCia();
        objRepteCia.SNomRepresentante = "-Seleccione-";
        lista.Insert(0, objRepteCia);
        ddlRepteCia.DataSource = lista;
        ddlRepteCia.DataTextField = "sNomRepresentante";
        ddlRepteCia.DataValueField = "iNumSecuencial";
        ddlRepteCia.DataBind();
        ddlRepteCia.SelectedIndex = 0;
    }


    private bool ExisteTasaVentaMEBase()
    {
        //Tasa de cambio
        string strMonExt = Property.htProperty["COD_DOLAR"].ToString();
        TasaCambio objTasaCambio = objBOOpera.ObtenerTasaCambioPorMoneda(strMonExt, Define.TC_VENTA);
        if (objTasaCambio == null)
        {
            lblMensajeError.Text = (string)LabelConfig.htLabels["turno.msgTasaDolar"];
            return false;
        }
        return true;
    }

    private String ImprimirCierreTurno()
    {
        String dataVoucher = string.Empty;
        listaParamImp = new Hashtable();
        try
        {
            Print impresion = new Print();
            // 1ro: ************************************** Imprimir VOUCHER CUADRE M.N.**************************************
            //String nombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_WEB_MN;
            //Print impresion = new Print();
            //XmlElement nodo = impresion.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);
     
            //CargarParametrosComunesImpresion();
            //for (int i = 0; i < listaCuadre.Count; i++)
            //{
            //    string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
            //    if (listaCuadre[i].Cod_Moneda.Equals(strMonNac))
            //    {
            //        CargarParametros_MN_Impresion((CuadreTurno)listaCuadre[i]);
            //        break;
            //    }
            //}

            //dataVoucher = impresion.ObtenerDataFormateada(listaParamImp, nodo);    
            ////********************************
            //listaParamImp.Clear();


            // 2do: ************************************** Imprimir VOUCHER CUADRE M.E.**************************************
            string nombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_WEB_ME;
            XmlElement nodo = impresion.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);

            for (int i = 0; i < listaCuadre.Count; i++)
            {
                //string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
                //if (listaCuadre[i].Cod_Moneda.Equals(strMonNac))
                //if (listaCuadre[i].Cod_Moneda.Equals("DOL"))
                //{
                    
                //}

                CargarParametrosComunesImpresion();
                CargarParametros_ME_Impresion(listaCuadre[i]);
                dataVoucher += impresion.ObtenerDataFormateada(listaParamImp, nodo);

                //********************************
                listaParamImp.Clear();
            }
        }
        catch (Exception ex)
        {
            throw; 
        }

        listaParamImp.Clear();
        return dataVoucher;
    }

    private void CargarParametrosComunesImpresion()
    {
        this.listaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);
        this.listaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, ViewState["TurnoActual"].ToString().Trim());
    }

    private void CargarParametros_MN_Impresion(CuadreTurno cuadreTurno)
    {
        listaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
        listaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
        listaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
        listaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);
        listaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
        listaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
        listaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
        listaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
        listaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
        listaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
        listaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
        listaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
        listaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
        listaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);


        listaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
        listaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

        listaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
        listaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
        listaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
        listaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
        listaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
        listaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
        listaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
        listaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
        listaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
        listaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

        listaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
        listaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

        listaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
        listaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

        listaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
        listaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

        listaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
        listaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

        listaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
        //listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
        listaParamImp.Add("recaudado_final", cuadreTurno.Imp_TicketInt + cuadreTurno.Imp_TicketNac);

        listaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
        listaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
        listaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
        listaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

        listaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
        listaParamImp.Add("efectivo_faltante", "(-)" + cuadreTurno.Imp_EfecFaltante.ToString());

        listaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
        listaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
        listaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
        listaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
    }

    private void CargarParametros_ME_Impresion(CuadreTurno cuadreTurno)
    {
        listaParamImp.Add("moneda_internacional", cuadreTurno.Dsc_Moneda);
        listaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
        listaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
        listaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
        listaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);

        listaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
        listaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
        listaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
        listaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
        listaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
        listaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
        listaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
        listaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
        listaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
        listaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);

        listaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
        listaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

        listaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
        listaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
        listaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
        listaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
        listaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
        listaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
        listaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
        listaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
        listaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
        listaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

        listaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
        listaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

        listaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
        listaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

        listaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
        listaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

        listaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
        listaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

        listaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
        //listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
        listaParamImp.Add("recaudado_final", cuadreTurno.Imp_TicketInt + cuadreTurno.Imp_TicketNac);


        listaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
        listaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
        listaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
        listaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

        listaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
        listaParamImp.Add("efectivo_faltante", "(-)" + cuadreTurno.Imp_EfecFaltante.ToString());

        listaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
        listaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
        listaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
        listaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
    }



}
