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
using System.Globalization;
using LAP.TUUA.ALARMAS;
using LAP.TUUA.PRINTER;
using System.Xml;

public partial class Ope_VentaContingencia : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected BO_Consultas objBOConsulta;
    protected TipoTicket objTipoTicket;
    protected decimal Imp_TotPagBase;
    protected bool Flg_IsAllOk;
    protected DataTable dt_consulta;
    protected List<Ticket> listaTickets;
    protected Hashtable htContingencia;
    protected bool Flg_Error;
    protected Hashtable htParametro;

    private BO_Administracion objAdministracion;
    private string strCodTurno;
    private string strCodTurnoActual;
    private string strFlagTurno;
    private string strCodUsuario;
    private string strCantTickets;
    private string strFecHorTurno;

    private Hashtable hsListaParamImp;
    private CuadreTurno objCuadre;
    List<CuadreTurno> listaCuadre;


    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        try
        {

            objAdministracion = new BO_Administracion();
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                txtNroIni.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                txtNroFin.MaxLength = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                btnAceptar.Text = htLabels["opeVentaContingencia.btnAceptar"].ToString();

                lblCompania.Text = htLabels["opeVentaContingencia.lblCompania"].ToString();
                lblFechaActual.Text = htLabels["opeVentaContingencia.lblFechaActual"].ToString();
                lblNumVuelo.Text = htLabels["opeVentaContingencia.lblNumVuelo"].ToString();

                rbAdulto.Text = htLabels["opeVentaContingencia.rbAdulto"].ToString();
                rbInfante.Text = htLabels["opeVentaContingencia.rbInfante"].ToString();
                rbInter.Text = htLabels["opeVentaContingencia.rbInter"].ToString();
                rbNacional.Text = htLabels["opeVentaContingencia.rbNacional"].ToString();
                rbNormal.Text = htLabels["opeVentaContingencia.rbNormal"].ToString();
                rbTrans.Text = htLabels["opeVentaContingencia.rbTrans"].ToString();

                //rfvFecha.ErrorMessage = htLabels["opeVentaContingencia.rfvFecha"].ToString();
                //btnAceptar_ConfirmButtonExtender.ConfirmText = htLabels["opeVentaContingencia.msgConfirm"].ToString();

                if (CargarCompania())
                {
                    CargarComboUsuario();
                    MostrarTipoTickets();
                    txtFecha.Text = DateTime.Now.ToShortDateString();
                    MostrarPrecioTicketDefault();
                    FillGridViewContingencia();
                }
                SetHoraActual();
                SetTamanoGrilla();
                strFlagTurno = "0";
                strCodTurnoActual = null;

                //Validacion si el usuario tiene un turno pendiente
                bool valida = validarTurnoPendiente();

                if (!valida)
                {
                    lblInfo.Text = htLabels["opeVentaContingencia.msgTurnoPendiente"].ToString();
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
                        strCantTickets = "0";
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
    private bool validarTurnoPendiente()
    {
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
                    if (!lstTurnoMonto[i].Tip_Pago.Equals("E"))
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
        //DataTable dtCompania = objBOOpera.ConsultarCompaniaxFiltro(Define.TIPO_AEROLINEA, Define.COMPANIA_ACTIVO, "", "ASC");
        //if (dtCompania != null && dtCompania.Rows.Count > 0)
        //{
        //    UpdatePanel1.Visible = true;
        //    btnAceptar.Visible = true;
        //    btnAceptar.Enabled = true;
        //    ddlCompania.DataSource = dtCompania;
        //    ddlCompania.DataTextField = "Dsc_Compania";
        //    ddlCompania.DataValueField = "Cod_Compania";
        //    ddlCompania.DataBind();
        //    ddlCompania.SelectedIndex = 0;
        //    return true;
        //}
        //btnAceptar.Visible = false;
        //btnAceptar.Enabled = false;
        //UpdatePanel1.Visible = false;

        List<ModVentaComp> listaCompania = objBOOpera.ListarCompaniaxModVenta(null, "");
        if (listaCompania != null && listaCompania.Count > 0)
        {
            UpdatePanel1.Visible = true;
            btnAceptar.Visible = true;
            btnAceptar.Enabled = true;
            ddlCompania.DataSource = listaCompania;
            ddlCompania.DataTextField = "Dsc_Compania";
            ddlCompania.DataValueField = "SCodCompania";
            ddlCompania.DataBind();
            ddlCompania.SelectedIndex = GetDefaultAerolinea(listaCompania);
            return true;
        }
        btnAceptar.Visible = false;
        btnAceptar.Enabled = false;
        UpdatePanel1.Visible = false;

        lblInfo.Text = htLabels["opeVentaContingencia.msgCompania"].ToString();
        return false;
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

    private bool MostrarPrecioTicketDefault()
    {
        Session["Tip_Vuelo"] = Define.NACIONAL;
        Session["Tip_Pasajero"] = Define.ADULTO;
        Session["Tip_Transbordo"] = Define.NORMAL;

        objTipoTicket = objBOOpera.ObtenerPrecioTicket(Define.NACIONAL, Define.ADULTO, Define.NORMAL);
        if (objTipoTicket == null)
        {
            lblMensajeError.Text = htLabels["opeVentaContingencia.msgTipoTicket"].ToString();
            Session["Flg_IsAllOk"] = false;
            return false;
        }
        Session["objTipoTicket"] = objTipoTicket;
        Session["Flg_IsAllOk"] = true;
        return true;
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

    protected bool Validar()
    {
        lblMensajeError.Text = "";

        if (grvContingencia.Rows.Count == 0)
        {
            lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgPreemitido"];
            return false;
        }

        bool boOkFecEmsion = SeleccionarTicketsVenta();

        if (htContingencia == null || htContingencia.Count <= 0)
        {
            lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgTicketSelect"];
            return false;
        }

        if (!boOkFecEmsion)
        {
            lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgFecEmision"];
            return false;
        }

        CultureInfo culturaPeru = new CultureInfo("es-ES");
        objBOOpera = new BO_Operacion();
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
        string strFecVenta = txtFecha.Text.Substring(6, 4) + txtFecha.Text.Substring(3, 2) + txtFecha.Text.Substring(0, 2) + ddlHour.SelectedValue + ddlMinute.SelectedValue;
        string strFecActual = objBOOpera.ObtenerFechaActual();//DateTime.Now.ToShortDateString();
        //strFecActual = strFecActual.Substring(6, 4) + strFecActual.Substring(3, 2) + strFecActual.Substring(0, 2) + DateTime.Now.ToLongTimeString().Substring(0, 2) + DateTime.Now.ToLongTimeString().Substring(3,2);

        if (strFecVenta.CompareTo(strFecActual) > 0)
        {
            lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgFecEmision"];
            return false;
        }
        if (!ExisteTasaVentaMEBase())
        {
            return false;
        }
        if (!ValidoAerolinea(ddlCompania.SelectedItem.Value))
        {
            lblMensajeError.Text = "La compañia no tiene alguna modalidad de venta contado asignada.";
            return false;
        }
        return true;
    }

    private void MostrarPrecioTicket(string strTipoVuelo, string strTipoPas, string strTipoTrans)
    {
        try
        {
            objBOOpera = new BO_Operacion();
            objTipoTicket = objBOOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objTipoTicket != null)
            {
                lblMensajeError.Text = "";
                Session["objTipoTicket"] = objTipoTicket;
                Session["Flg_IsAllOk"] = true;
            }
            else
            {
                Session["objTipoTicket"] = null;
                Session["Flg_IsAllOk"] = false;
                lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgTipoTicket"];
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

    private void FillGridViewContingencia()
    {
        SetTamanoGrilla();
        string strNroSerieIni = txtNroIni.Text;
        string strNroSerieFin = txtNroFin.Text;
        string strUsuario = null;
        string strTipoTicket = "";
        objBOOpera = new BO_Operacion();
        if (ddlUsuario.SelectedItem != null && ddlUsuario.SelectedItem.Text != "Todos")
        {
            strUsuario = ddlUsuario.SelectedItem.Value;
        }

        strTipoTicket = objTipoTicket != null ? objTipoTicket.SCodTipoTicket : null;
        strNroSerieIni = strNroSerieIni == "" ? null : strNroSerieIni;
        strNroSerieFin = strNroSerieFin == "" ? null : strNroSerieFin;
        dt_consulta = objBOOpera.ListarContingencia(strTipoTicket, "1", strUsuario, strNroSerieIni, strNroSerieFin);
        if (!(dt_consulta != null && dt_consulta.Rows.Count > 0))
        {
            lblGrilla.Text = (string)htLabels["opeVentaContingencia.msgGrilla"];
        }
        else
        {
            lblGrilla.Text = "";
        }
        //grvContingencia.DataSource = dt_consulta;
        Session["TicketsConti"] = dt_consulta;
        //grvContingencia.DataBind();
        BuildTableConti();
    }

    protected void grvPuntoVenta_Sorting(object sender, GridViewSortEventArgs e)
    {
        //string sortExpression = e.SortExpression;
        MostrarPrecioTicket((string)Session["Tip_Vuelo"], (string)Session["Tip_Pasajero"], (string)Session["Tip_Transbordo"]);
        //if (GridViewSortDirection == SortDirection.Ascending)
        //{
        //    GridViewSortDirection = SortDirection.Descending;
        //    this.txtOrdenacion.Text = "DESC";
        //    SortGridView(sortExpression, "DESC");
        //}
        //else
        //{
        //    GridViewSortDirection = SortDirection.Ascending;
        //    this.txtOrdenacion.Text = "ASC";
        //    SortGridView(sortExpression, "ASC");
        //}
        //this.txtColumna.Text = sortExpression;

        DataTable dtTicketAnulados = (DataTable)Session["tabla"];
        if (dtTicketAnulados == null)
        {
            return;
        }

        GridViewSortExpression = e.SortExpression;

        #region Guardo las selecciones del checkbox
        int pageIndex = grvContingencia.PageIndex;
        int pageSize = grvContingencia.PageSize;
        int limite;
        if ((pageIndex + 1) < grvContingencia.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketAnulados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)grvContingencia.Rows[j].FindControl("ckbRegistrar");
            dtTicketAnulados.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        Session["tabla"] = SortDataTable(dtTicketAnulados, false).ToTable();
        //reactualizo la tabla
        dtTicketAnulados = (DataTable)Session["tabla"];

        grvContingencia.DataSource = (DataTable)Session["tabla"];

        grvContingencia.DataBind();

        #region Actualizo la selecciones del checkBox.
        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)grvContingencia.Rows[j].FindControl("ckbRegistrar");
            chkSeleccionar.Checked = (Boolean)dtTicketAnulados.Rows[i]["Check"];
        }

        #endregion
    }

    //public SortDirection GridViewSortDirection
    //{
    //    get
    //    {
    //        if (ViewState["SortDirection"] == null)
    //        {
    //            ViewState["SortDirection"] = SortDirection.Ascending;
    //        }
    //        return (SortDirection)ViewState["SortDirection"];
    //    }
    //    set
    //    {
    //        ViewState["SortDirection"] = value;
    //    }
    //}


    //private void SortGridView(string sortExpression, String direction)
    //{
    //    //string strNroSerieIni = txtNroIni.Text;
    //    //string strNroSerieFin = txtNroFin.Text;
    //    //string strUsuario = "";
    //    //if (ddlUsuario.SelectedItem != null && ddlUsuario.SelectedItem.Text != "Todos")
    //    //{
    //    //    strUsuario = ddlUsuario.SelectedItem.Value;
    //    //}
    //    //dt_consulta = objBOOpera.ListarContingencia(objTipoTicket.SCodTipoTicket, strUsuario, strNroSerieIni, strNroSerieFin);
    //    grvContingencia.DataSource = dwConsulta((DataTable)ViewState["TicketsConti"], sortExpression, direction);
    //    grvContingencia.DataBind();
    //}

    protected DataView dwConsulta(DataTable dtConsulta, string sortExpression, String direction)
    {
        DataView dv = new DataView(dtConsulta);

        if (txtOrdenacion.Text.CompareTo("") != 0)
        {
            dv.Sort = sortExpression + " " + direction;
        }
        return dv;
    }

    protected void CargarComboUsuario()
    {
        objBOConsulta = new BO_Consultas();
        DataTable dtUsuarios = objBOConsulta.ListarAllUsuario();
        DataRow dtrTodos = dtUsuarios.NewRow();
        dtrTodos["Usuario"] = "Todos";
        dtUsuarios.Rows.InsertAt(dtrTodos, 0);

        ddlUsuario.DataSource = dtUsuarios;
        ddlUsuario.DataTextField = "Usuario";
        ddlUsuario.DataValueField = "Cod_Usuario";
        ddlUsuario.DataBind();
    }

    protected void Limpiar()
    {
        txtNroIni.Text = "";
        txtNroFin.Text = "";
        txtNumVuelo.Text = "";
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        //EAG 24/02/2010
        Compania compania = objAdministracion.obtenerCompañiaxcodigo(ddlCompania.SelectedItem.Value);
        String estado = compania.STipEstado;
        
        if (chkCierreTurno.Checked)
        {
            strFlagTurno = "1";
        }
        else
        {
            strFlagTurno = "0";
        }

        if (!estado.Trim().Equals(Define.COMPANIA_ACTIVO))
        {
            lblMensajeError.Text = (string)htLabels["venta.error.companiaanulada"];
        }
        else
        {
            if (!Validar())
            {
                return;
            }
            try
            {
                int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
                int intCanMaxTicket = Define.STRING_SIZE / intLongTicket;

                objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                int intCanTicket = 0;
                int inTotalTicket = 0;
                string strCodError = null;
                string strCompania = ddlCompania.SelectedValue.ToString();
                string strNumVuelo = txtNumVuelo.Text == "" ? (string)htParametro[Property.htProperty[Define.ID_PARAM_VUELO_DEFAULT]] : txtNumVuelo.Text;
                string strFecVenta = txtFecha.Text.Substring(6, 4) + txtFecha.Text.Substring(3, 2) + txtFecha.Text.Substring(0, 2) + ddlHour.SelectedValue + ddlMinute.SelectedValue + "00";
                object[] keys = new object[htContingencia.Count];
                htContingencia.Keys.CopyTo(keys, 0);
                for (int i = 0; i < keys.Length; i++)
                {
                    intCanTicket = ((string)htContingencia[keys[i]]).Length / intLongTicket;
                    inTotalTicket += intCanTicket;
                    if (intCanTicket > intCanMaxTicket)
                    {
                        lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgMaxTicketSistema"];
                        return;
                    }
                    if (!objBOOpera.RegistrarContingencia(intCanTicket, strCompania, strNumVuelo, (string)Session["Cod_Usuario"], (string)keys[i], objTipoTicket.SCodTipoTicket, strFecVenta, (string)htContingencia[keys[i]], strCodTurnoActual, strFlagTurno, ref strCodTurno, ref strCodError))
                    {
                        lblMensajeError.Text = objBOOpera.Dsc_Message;
                        return;
                    }
                    else
                    {
                        if (strCodError.Trim() == Define.OPE_VENTA_VAL00)
                        {
                            lblMensajeError.Text = (string)htLabels["opeVentaContingencia.msgTurnoPendiente"];
                            return;
                        }
                        //GeneraAlarma
                        string IpClient = Request.UserHostAddress;
                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000021", "004", IpClient, "1", "Alerta W0000021", "Registro de Tickets Contingencia; Cantidad: " + Convert.ToString(intCanTicket) + ", Compania: " + strCompania + ", Tipo Ticket: " + objTipoTicket.SCodTipoTicket + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                    }
                }
                //strCodTurnoActual = strCodTurno;
                //lblTurno.Text = strCodTurnoActual;                
                Limpiar();
                //MostrarPrecioTicketDefault();
                FillGridViewContingencia();                
                omb.ShowMessage((string)htLabels["opeVentaContingencia.msgTrxOK"], (string)htLabels["opeVentaContingencia.lblVentaContigencia"], "Ope_VentaContingencia.aspx");

                if (strCodTurno != string.Empty)
                {
                    ViewState["TurnoActual"] = strCodTurno;
                }

                //this.strCantTickets = (Int32.Parse(strCantTickets.Trim()) + inTotalTicket).ToString();
                Imprimir();

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

    }


    /// <summary>
    /// Metodo que imprime la lista de tickets y el detalle de voucher
    /// <param name="listaCodigoTickets">Lista codigo de tickets</param>
    /// </summary>
    private void Imprimir()
    {
        Print objPrint = new Print();

        // 1do: ************************************** Imprimir VOUCHER **************************************
        // obtiene el nodo segun el nombre
        
        String nombre = Define.ID_PRINTER_DOCUM_VENTATICKETMASIVACREDITO;

        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], nombre);

        // configuracion de la impresora a utilizar
        String configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], nombre);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }

        String dataVoucher = String.Empty;

        Session["dataVoucher"] = dataVoucher;        

        Session["xmlFormatoVoucher"] = nodo.OuterXml;

        if (chkCierreTurno.Checked)
        {
            //INICIO CMONTES
            BO_Turno objBOTurno = new BO_Turno();         
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

            //List<Moneda> listaMoneda = objBOTurno.ListarMonedas();
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

            String compania = "";//Dsc_Aerolinea;
            String nrovuelo = txtNumVuelo.Text;

            Response.Redirect("Ope_Impresion_ConStickers.aspx?" +
                "flagImpSticker=0" +
                "&" + "flagImpVoucher=1" +
                "&" + "configImpVoucher=" + configImpVoucher +
                //"&" + "nroTicketsImpresos=" + this.strCantTickets +
                //"&" + "operacion=" + Define.ESTADO_TICKET_EMITIDO +
                "&" + Define.ID_PRINTER_PARAM_NOMBRE_CAJERO + "=" + Session["Nombre_Usuario"] +
                "&" + Define.ID_PRINTER_PARAM_DESCRIPCION_TIPOTICKET + "=" + objTipoTicket.SNomTipoTicket +
                "&" + "Pagina_PreImpresion=Ope_VentaContingencia.aspx", false);

        }
        //FIN CMONTES
    }


    private bool SeleccionarTicketsVenta()
    {
        DataTable dtTicket = (DataTable)Session["tabla"];//(DataTable)Session["TicketsConti"];
        string strMoneda = "";
        string strFecEmision = "";
        string strFecVenta = txtFecha.Text.Substring(6, 4) + txtFecha.Text.Substring(3, 2) + txtFecha.Text.Substring(0, 2) + ddlHour.SelectedValue + ddlMinute.SelectedValue + "00";
        htContingencia = new Hashtable();


        #region Guardo las selecciones del checkbox en la pagina donde se le dio click en Anular
        int pageSize = grvContingencia.PageSize;
        int pageIndex = grvContingencia.PageIndex;
        int limite;
        if ((pageIndex + 1) < grvContingencia.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicket.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)grvContingencia.Rows[j].FindControl("ckbRegistrar");
            dtTicket.Rows[i]["Check"] = chkSeleccionar.Checked;
        }
        #endregion

        for (int i = 0; i < dtTicket.Rows.Count; i++)
        {
            //GridViewRow row = grvContingencia.Rows[i];
            bool isChecked = (Boolean)dtTicket.Rows[i]["Check"];//((System.Web.UI.WebControls.CheckBox)row.FindControl("ckbRegistrar")).Checked;
            if (isChecked)
            {
                strFecEmision = dtTicket.Rows[i].ItemArray.GetValue(5).ToString(); //+ dtTicket.Rows[i].ItemArray.GetValue(2).ToString();
                if (strFecEmision.CompareTo(strFecVenta) > 0)
                {
                    strMoneda = dtTicket.Rows[i].ItemArray.GetValue(4).ToString();
                    htContingencia[strMoneda] = (string)htContingencia[strMoneda] + dtTicket.Rows[i].ItemArray.GetValue(1).ToString();
                    return false;
                }

                strMoneda = dtTicket.Rows[i].ItemArray.GetValue(4).ToString();
                if (htContingencia.ContainsKey(strMoneda))
                {
                    htContingencia[strMoneda] = (string)htContingencia[strMoneda] + dtTicket.Rows[i].ItemArray.GetValue(1).ToString();
                }
                else
                {
                    htContingencia.Add(strMoneda, dtTicket.Rows[i].ItemArray.GetValue(1).ToString());
                }
            }
        }
        return true;
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

    private bool ValidoAerolinea(string strAerolinea)
    {
        List<ModVentaComp> listaCompania = objBOOpera.ListarCompaniaxModVenta(null, "");
        for (int i = 0; i < listaCompania.Count; i++)
        {
            if (listaCompania[i].SCodCompania.Trim() == strAerolinea.Trim())
            {
                return true;
            }
        }
        return false;
    }

    private void SetHoraActual()
    {
        string strFecActual = objBOOpera.ObtenerFechaActual();
        string strHora = strFecActual.Substring(8, 2);
        string strMin = strFecActual.Substring(10, 2);
        int index = strHora.Substring(0, 1) == "0" ? Int32.Parse(strHora.Substring(1, 1)) : Int32.Parse(strHora);
        ddlHour.SelectedIndex = index;
        index = strMin.Substring(0, 1) == "0" ? Int32.Parse(strMin.Substring(1, 1)) : Int32.Parse(strMin);
        ddlMinute.SelectedIndex = index;
    }

    private int GetDefaultAerolinea(List<ModVentaComp> listaCompania)
    {
        for (int i = 0; i < listaCompania.Count; i++)
        {
            if (listaCompania[i].SCodCompania == Property.htProperty[Define.COD_COMPANIA_DEFAULT].ToString())
            {
                return i;
            }
        }
        return 0;
    }

    private void SetTamanoGrilla()
    {
        int intGridSize = Int32.Parse((string)htParametro["LG"]);
        this.grvContingencia.PageSize = intGridSize;
    }

    protected void grvContingencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        int pageIndex;
        int pageSize = grvContingencia.PageSize;
        //ValidarRadioButton();

        DataTable dtTicketAnulados = (DataTable)Session["tabla"];

        #region Guardo las selecciones del checkbox
        pageIndex = grvContingencia.PageIndex;
        int limite;
        if ((pageIndex + 1) < grvContingencia.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketAnulados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)grvContingencia.Rows[j].FindControl("ckbRegistrar");
            dtTicketAnulados.Rows[i]["Check"] = chkSeleccionar.Checked;

            //TextBox txtMotivo = (TextBox)grvContingencia.Rows[j].FindControl("txtMotivo");
            //dtTicketAnulados.Rows[i]["Motivo"] = txtMotivo.Text;
        }
        #endregion

        grvContingencia.PageIndex = e.NewPageIndex;

        grvContingencia.DataSource = dtTicketAnulados;
        grvContingencia.DataBind();

        #region Actualizo la selecciones del checkBox.

        pageIndex = grvContingencia.PageIndex;

        if ((pageIndex + 1) < grvContingencia.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtTicketAnulados.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            CheckBox chkSeleccionar = (CheckBox)grvContingencia.Rows[j].FindControl("ckbRegistrar");
            chkSeleccionar.Checked = (Boolean)dtTicketAnulados.Rows[i]["Check"];
            //TextBox txtMotivo = (TextBox)grvContingencia.Rows[j].FindControl("txtMotivo");
            //txtMotivo.Text = (String)dtTicketAnulados.Rows[i]["Motivo"];
        }

        //int nroSeleccionados = dtTicketAnulados.Select("Check=true").Count();
        //lblTxtSeleccionados.Text = Convert.ToString(nroSeleccionados);
        #endregion
    }

    protected void BuildTableConti()
    {

        DataTable dtTicketConti = (DataTable)Session["TicketsConti"];

        DataTable dtTicketAnulados = new DataTable();
        dtTicketAnulados.Columns.Add("Numero", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Fch_Creacion", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
        dtTicketAnulados.Columns.Add("Cod_Moneda", System.Type.GetType("System.String"));
        dtTicketAnulados.Columns.Add("Column1", System.Type.GetType("System.String"));

        if (dtTicketConti.Rows.Count == 0)
        {
            Session["tabla"] = null;
            #region Agregando fila vacia a la grilla por default
            dtTicketAnulados.Rows.Add(dtTicketAnulados.NewRow());

            grvContingencia.PageIndex = 0;//Se sobreentiende
            grvContingencia.DataSource = dtTicketAnulados;
            grvContingencia.DataBind();
            grvContingencia.Rows[0].Cells[0].Text = "";
            grvContingencia.Rows[0].FindControl("ckbRegistrar").Visible = false;
            #endregion

            //lblErrorMsg.Text = "No se encontro resultado alguno.";
        }
        else
        {
            DataRow row;

            for (int i = 0; i < dtTicketConti.Rows.Count; i++)
            {
                row = dtTicketAnulados.NewRow();
                row["Numero"] = i + 1;
                row["Cod_Numero_Ticket"] = dtTicketConti.Rows[i]["Cod_Numero_Ticket"];
                row["Fch_Creacion"] = dtTicketConti.Rows[i]["Fch_Creacion"];
                row["Check"] = false;
                row["Cod_Moneda"] = dtTicketConti.Rows[i]["Cod_Moneda"];
                row["Column1"] = dtTicketConti.Rows[i]["Column1"];
                dtTicketAnulados.Rows.Add(row);
            }
            Session["tabla"] = dtTicketAnulados;
            grvContingencia.PageIndex = 0;
            grvContingencia.DataSource = dtTicketAnulados;
            grvContingencia.DataBind();
        }
    }

    protected DataView SortDataTable(DataTable dataTable, bool isPageIndexChanging)
    {
        if (dataTable != null)
        {
            DataView dataView = new DataView(dataTable);
            if (GridViewSortExpression != string.Empty)
            {
                if (isPageIndexChanging)
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GridViewSortDirection);
                }
                else
                {
                    dataView.Sort = string.Format("{0} {1}", GridViewSortExpression, GetSortDirection());
                }
            }
            else
            {
                dataView.Sort = string.Format("{0} {1}", "LastName", "ASC");
            }
            return dataView;
        }
        else
        {
            return new DataView();
        }
    }

    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "ASC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string GetSortDirection()
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;
            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    private string GridViewSortExpression
    {
        get { return ViewState["SortExpression"] as string ?? string.Empty; }
        set { ViewState["SortExpression"] = value; }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        FillGridViewContingencia();
    }


    private String ImprimirCierreTurno()
    {
        String strDataVoucher = string.Empty;
        hsListaParamImp = new Hashtable();
        Print objImpresion = new Print();
        try
        {
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
            string strNombre = Define.ID_PRINTER_DOCUM_CIERRETURNO_WEB_ME;
            XmlElement nodo = objImpresion.ObtenerNodo((XmlDocument)Session["xmlDoc"], strNombre);

            for (int i = 0; i < listaCuadre.Count; i++)
            {
                //string strMonNac = Property.htProperty[Define.MONEDANAC].ToString();
                //if (listaCuadre[i].Cod_Moneda.Equals(strMonNac))
                //{
                //    continue;
                //}

                CargarParametrosComunesImpresion();
                CargarParametros_ME_Impresion(listaCuadre[i]);
                strDataVoucher += objImpresion.ObtenerDataFormateada(hsListaParamImp, nodo);

                //********************************
                hsListaParamImp.Clear();
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        hsListaParamImp.Clear();
        return strDataVoucher;
    }

    private void CargarParametrosComunesImpresion()
    {
        this.hsListaParamImp.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);
        this.hsListaParamImp.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, ViewState["TurnoActual"].ToString().Trim());
    }

    private void CargarParametros_MN_Impresion(CuadreTurno cuadreTurno)
    {
        hsListaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
        hsListaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
        hsListaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
        hsListaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);
        hsListaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
        hsListaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
        hsListaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
        hsListaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
        hsListaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
        hsListaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
        hsListaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
        hsListaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
        hsListaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
        hsListaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);


        hsListaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
        hsListaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

        hsListaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
        hsListaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
        hsListaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
        hsListaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
        hsListaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
        hsListaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
        hsListaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
        hsListaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
        hsListaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
        hsListaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

        hsListaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
        hsListaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

        hsListaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
        hsListaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

        hsListaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
        hsListaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

        hsListaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
        hsListaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

        hsListaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
        //listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
        hsListaParamImp.Add("recaudado_final", cuadreTurno.Imp_TicketInt + cuadreTurno.Imp_TicketNac);

        hsListaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
        hsListaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
        hsListaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
        hsListaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

        hsListaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
        hsListaParamImp.Add("efectivo_faltante", "(-)" + cuadreTurno.Imp_EfecFaltante.ToString());

        hsListaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
        hsListaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
        hsListaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
        hsListaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
    }

    private void CargarParametros_ME_Impresion(CuadreTurno cuadreTurno)
    {
        hsListaParamImp.Add("moneda_internacional", cuadreTurno.Dsc_Moneda);
        hsListaParamImp.Add("efectivo_inicio", cuadreTurno.Imp_EfectivoIni);
        hsListaParamImp.Add("simbolo_moneda", cuadreTurno.Dsc_Simbolo);
        hsListaParamImp.Add("q_cobro_tasa_int_al_contado", cuadreTurno.Can_TicketInt);
        hsListaParamImp.Add("cobro_tasa_int_al_contado", cuadreTurno.Imp_TicketInt);

        hsListaParamImp.Add("q_contado_int", cuadreTurno.Can_Ticket_EfeInt);
        hsListaParamImp.Add("cobro_contado_int", cuadreTurno.Imp_Ticket_EfeInt);
        hsListaParamImp.Add("q_trans_int", cuadreTurno.Can_Ticket_TraInt);
        hsListaParamImp.Add("cobro_trans_int", cuadreTurno.Imp_Ticket_TraInt);
        hsListaParamImp.Add("q_debito_int", cuadreTurno.Can_Ticket_DebInt);
        hsListaParamImp.Add("cobro_debito_int", cuadreTurno.Imp_Ticket_DebInt);
        hsListaParamImp.Add("q_credito_int", cuadreTurno.Can_Ticket_CreInt);
        hsListaParamImp.Add("cobro_credito_int", cuadreTurno.Imp_Ticket_CreInt);
        hsListaParamImp.Add("q_cheque_int", cuadreTurno.Can_Ticket_CheInt);
        hsListaParamImp.Add("cobro_cheque_int", cuadreTurno.Imp_Ticket_CheInt);

        hsListaParamImp.Add("q_cobro_tasa_nac_al_contado", cuadreTurno.Can_TicketNac);
        hsListaParamImp.Add("cobro_tasa_nac_al_contado", cuadreTurno.Imp_TicketNac);

        hsListaParamImp.Add("q_contado_nac", cuadreTurno.Can_Ticket_EfeNac);
        hsListaParamImp.Add("cobro_contado_nac", cuadreTurno.Imp_Ticket_EfeNac);
        hsListaParamImp.Add("q_trans_nac", cuadreTurno.Can_Ticket_TraNac);
        hsListaParamImp.Add("cobro_trans_nac", cuadreTurno.Imp_Ticket_TraNac);
        hsListaParamImp.Add("q_debito_nac", cuadreTurno.Can_Ticket_DebNac);
        hsListaParamImp.Add("cobro_debito_nac", cuadreTurno.Imp_Ticket_DebNac);
        hsListaParamImp.Add("q_credito_nac", cuadreTurno.Can_Ticket_CreNac);
        hsListaParamImp.Add("cobro_credito_nac", cuadreTurno.Imp_Ticket_CreNac);
        hsListaParamImp.Add("q_cheque_nac", cuadreTurno.Can_Ticket_CheNac);
        hsListaParamImp.Add("cobro_cheque_nac", cuadreTurno.Imp_Ticket_CheNac);

        hsListaParamImp.Add("q_ingreso_caja", cuadreTurno.Can_IngCaja);
        hsListaParamImp.Add("ingreso_caja", cuadreTurno.Imp_IngCaja);

        hsListaParamImp.Add("q_compra", cuadreTurno.Can_CompraMoneda);
        hsListaParamImp.Add("compra", cuadreTurno.Imp_CompraMoneda);

        hsListaParamImp.Add("q_egreso_caja", cuadreTurno.Can_EgreCaja);
        hsListaParamImp.Add("egreso_caja", cuadreTurno.Imp_EgreCaja);

        hsListaParamImp.Add("q_venta", cuadreTurno.Can_VentaMoneda);
        hsListaParamImp.Add("venta", cuadreTurno.Imp_VentaMoneda);

        hsListaParamImp.Add("efectivo_final", cuadreTurno.Imp_EfectivoFinal);
        //listaParamImp.Add("recaudado_final", cuadreTurno.Imp_Recaudado_Fin);
        hsListaParamImp.Add("recaudado_final", cuadreTurno.Imp_TicketInt + cuadreTurno.Imp_TicketNac);


        hsListaParamImp.Add("q_tickets_anul_inter", cuadreTurno.Can_AnulaInt);
        hsListaParamImp.Add("q_tickets_anul_nac", cuadreTurno.Can_AnulaNac);
        hsListaParamImp.Add("q_tickets_infa_inter", cuadreTurno.Can_InfanteInt);
        hsListaParamImp.Add("q_tickets_infa_nac", cuadreTurno.Can_InfanteNac);

        hsListaParamImp.Add("efectivo_sobrante", cuadreTurno.Imp_EfecSobrante);
        hsListaParamImp.Add("efectivo_faltante", "(-)" + cuadreTurno.Imp_EfecFaltante.ToString());

        hsListaParamImp.Add("q_cobro_tasa_int_al_credito", cuadreTurno.Can_CreditoInt);
        hsListaParamImp.Add("cobro_tasa_int_al_credito", cuadreTurno.Imp_CreditoInt);
        hsListaParamImp.Add("q_cobro_tasa_nac_al_credito", cuadreTurno.Can_CreditoNac);
        hsListaParamImp.Add("cobro_tasa_nac_al_credito", cuadreTurno.Imp_CreditoNac);
    }

}
