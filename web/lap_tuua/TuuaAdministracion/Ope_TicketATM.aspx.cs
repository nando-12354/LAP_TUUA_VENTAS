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

public partial class Ope_TicketATM : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected TipoTicket objTipoTicket;
    protected bool Flg_IsAllOk;
    protected decimal Imp_TotPagBase;
    protected bool Flg_Error;
    protected int Can_Ticket;
    protected Hashtable htParametro;
   

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                btnGenerar.Text = htLabels["opeTicketAtm.btnGenerar"].ToString();
                lblTipoTicket.Text = htLabels["opeTicketAtm.lblTipoTicket"].ToString();
                lblCantidad.Text = htLabels["opeTicketAtm.lblCantidad"].ToString();
                lblCompania.Text = htLabels["opeTicketAtm.lblCompania"].ToString();
                lblRepteCia.Text = htLabels["opeTicketAtm.lblRepteCia"].ToString();
                lblFechaActual.Text = htLabels["opeTicketAtm.lblFechaActual"].ToString();
                lblPrecioTicket.Text = htLabels["opeTicketAtm.lblPrecioTicket"].ToString();
                //lblPrecioTotal.Text = htLabels["opeTicketAtm.lblPrecioTotal"].ToString();

                rbAdulto.Text = htLabels["opeTicketAtm.rbAdulto"].ToString();
                rbInfante.Text = htLabels["opeTicketAtm.rbInfante"].ToString();
                rbInter.Text = htLabels["opeTicketAtm.rbInter"].ToString();
                rbNacional.Text = htLabels["opeTicketAtm.rbNacional"].ToString();
                rbNormal.Text = htLabels["opeTicketAtm.rbNormal"].ToString();
                rbTrans.Text = htLabels["opeTicketAtm.rbTrans"].ToString();

                rfvCantidad.ErrorMessage = htLabels["opeTicketAtm.rfvCantidad"].ToString();
                rfvRepteCia.ErrorMessage = htLabels["opeTicketAtm.rfvRteCia"].ToString();
                btnGenerar_ConfirmButtonExtender.ConfirmText = htLabels["opeTicketAtm.msgConfirm"].ToString();

                if (CargarCompanias())
                {
                    CargarComboReptes();
                    MostrarTipoTickets();
                    lblFecActVal.Text = DateTime.Now.ToShortDateString();
                    MostrarPrecioTicketDefault();
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

    private bool CargarCompanias()
    {

        List<ModVentaComp> listaCompania = objBOOpera.ListarCompaniaxModVenta((string)Property.htProperty[Define.MOD_VENTA_ATM], LAP.TUUA.UTIL.Define.TIPO_BANCO);

                
        if (listaCompania != null && listaCompania.Count > 0)
        {
            UpdatePanel1.Visible = true;
            btnGenerar.Visible = true;
            btnGenerar.Enabled = true;
            ddlCompania.DataSource = listaCompania;
            ddlCompania.DataTextField = "Dsc_Compania";
            ddlCompania.DataValueField = "SCodCompania";
            ddlCompania.DataBind();
            ddlCompania.SelectedIndex = 0;
            return true;
        }

        btnGenerar.Visible = false;
        btnGenerar.Enabled = false;
        UpdatePanel1.Visible = false;
        lblInfo.Text = htLabels["opeTicketAtm.msgCompania"].ToString();
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
            lblMensajeError.Text = htLabels["opeTicketAtm.msgTipoTicket"].ToString();
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
        try
        {
            objTipoTicket = objBOOpera.ObtenerPrecioTicket(strTipoVuelo, strTipoPas, strTipoTrans);
            if (objTipoTicket != null)
            {
                this.lblPrecTicketVal.Text = objTipoTicket.Dsc_Simbolo + " " + objTipoTicket.DImpPrecio.ToString();
                lblMensajeError.Text = "";
                Session["objTipoTicket"] = objTipoTicket;
                Session["Flg_IsAllOk"] = true;
            }
            else
            {
                Session["Flg_IsAllOk"] = false;
                lblMensajeError.Text = htLabels["opeTicketAtm.msgTipoTicket"].ToString();
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

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
            string strCompania = ddlCompania.SelectedItem.Value;
            string strUsuario = (string)Session["Cod_Usuario"];
            string strTickets = "";
            string strVueloDefault = "";
            if (!Validar())
            {
                return;
            }
            try
            {
                Hashtable htParametro = (Hashtable)Session["htParametro"];
                strVueloDefault=(string)htParametro[Property.htProperty[Define.ID_PARAM_VUELO_DEFAULT]];
                string strEmpresa = ddlCompania.SelectedItem.Text;
                string strRepte = ddlRepteCia.SelectedItem.Text;
                objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                if (!objBOOpera.GenerarTicketATM(Can_Ticket, objTipoTicket, strCompania, strUsuario, strVueloDefault, strEmpresa, strRepte, ref strTickets))
                {
                    lblMensajeError.Text = objBOOpera.Dsc_Message;
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
                else
                {
                    if (strTickets != null && strTickets != "")
                    {
                        //omb.ShowMessage((string)htLabels["opeTicketAtm.msgTrxOK"], (string)htLabels["opeTicketAtm.lblTicketATM"], "Ope_TicketATM.aspx");
                        //GenerarArchivoATM(strTickets, strCompania);
                        Session["Tickets_ATM"] = strTickets;
                        Session["Cod_Compania_ATM"] = strCompania;
                        Response.Redirect("Ope_PostAtm.aspx");
                    }
                }
            }
               
    }

    protected bool Validar()
    {
        int intLongTicket = Int32.Parse((string)htParametro[Define.ID_PARAM_LONG_TICKET]);
        int intCanMaxTicket = Define.STRING_SIZE / intLongTicket;
        lblMensajeError.Text = "";
        Can_Ticket = Int32.Parse(txtCantidad.Text);
        if (Can_Ticket > intCanMaxTicket)
        {
            lblMensajeError.Text = (string)htLabels["opeTicketAtm.msgMaxTicketSistema"];
            return false;
        }
        if (Can_Ticket == 0)
        {
            rfvCantidad.ErrorMessage = htLabels["opeTicketAtm.rfvCantidad"].ToString();
            rfvCantidad.IsValid = false;
            return false;
        }
        if (ddlRepteCia.SelectedItem.ToString().ToUpper().Trim() == "-SELECCIONE-")
        {
            rfvRepteCia.IsValid = false;
            return false;
        }
        return (bool)Session["Flg_IsAllOk"];
    }

    protected void ddlCompania_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompania.SelectedItem != null)
        {
            CargarComboReptes();
        }
    }

    private void GenerarArchivoATM(string strTickets, string strCompania)
    {
        string strFecha = DateTime.Now.ToShortDateString();
        string strTime = DateTime.Now.ToLongTimeString();
        string strDateTime = strFecha.Substring(6, 4) + strFecha.Substring(3, 2) + strFecha.Substring(0, 2) + "_" + strTime.Substring(0, 2) + strTime;
        string strFileName = strCompania +"_ATM.dat";
        Response.ContentType = "text/plain";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName);
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        Response.BinaryWrite(encoding.GetBytes(strTickets));
        Response.End();
    }

    protected void txtCantidad_TextChanged(object sender, EventArgs e)
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
        //lblSimbolo.Text = objTipoTicket.Dsc_Simbolo + " ";
    }

    private void Limpiar()
    {
        txtCantidad.Text = "";
        objBOOpera = new BO_Operacion();
        MostrarPrecioTicketDefault();
    }

}
