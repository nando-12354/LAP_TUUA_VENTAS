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


public partial class Ope_ExtornarMoneda : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    protected List<LogOperacion> listaOperaciones;
    protected bool Flg_Error;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {
                btnExtornar.Text = htLabels["opeExtornoMoneda.btnExtornar"].ToString();
                lblTipOpera.Text = htLabels["opeExtornoMoneda.lblTipOpera"].ToString();
                lblCodOpera.Text = htLabels["opeExtornoMoneda.lblCodOpera"].ToString();
                rbCompra.Text = htLabels["opeExtornoMoneda.rbCompra"].ToString();
                rbVenta.Text = htLabels["opeExtornoMoneda.rbVenta"].ToString();
                btnExtornar_ConfirmButtonExtender.ConfirmText = htLabels["opeExtornoMoneda.msgConfirm"].ToString();
                Session["Cod_Turno"] = Request.QueryString["Cod_Turno"];
                Session["Tip_Operacion"] = Define.COMPRA_MONEDA;
                FillGridViewOperaciones((string)Session["Cod_Turno"]);
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

    private void FillGridViewOperaciones(string strTurno)
    {
        string strCodOpera = txtOperacion.Text;
        try
        {
            DataTable dt_consulta = objBOOpera.ListarCompraVentaXFiltro(strTurno, (string)Session["Tip_Operacion"], strCodOpera);
            if (!(dt_consulta != null && dt_consulta.Rows.Count != 0))
            {
                btnExtornar.Enabled = false;
                lblGrilla.Text = (string)htLabels["opeExtornoMoneda.msgGrilla"];
            }
            else
            {
                lblGrilla.Text = "";
                btnExtornar.Enabled = true;
            }
            Session["Data_Opera_Extorno"] = dt_consulta;
            grvOperaciones.DataSource = dt_consulta;
            ViewState["OperacionesExt"] = dt_consulta;
            grvOperaciones.DataBind();
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

    private bool Validar()
    {
        listaOperaciones = new List<LogOperacion>();
        for (int i = 0; i <grvOperaciones.Rows.Count; i++)
        {
            GridViewRow row = grvOperaciones.Rows[i];
            bool isChecked = ((System.Web.UI.WebControls.CheckBox)row.FindControl("ckbRegistrar")).Checked;
            if (isChecked)
            {
                LogOperacion objLogOpera = new LogOperacion();
                objLogOpera.SNumOperacion = row.Cells[1].Text;
                objLogOpera.SCodTurno = (string)Session["Cod_Turno"];
                ObtenerOperacionExtorno(objLogOpera);
                listaOperaciones.Add(objLogOpera);
            }
        }
        if (listaOperaciones.Count == 0)
        {
            lblMensajeError.Text = "No hay ticket seleccionados.";
            return false;
        }
        return true;
    }

    protected void btnExtornar_Click(object sender, EventArgs e)
    {
        if (!Validar())
        {
            return;
        }
        try
        {
            objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            Session["listaOperaciones"] = listaOperaciones;
            string strTurno = listaOperaciones[0].SCodTurno;
            listaOperaciones[0].SCodTurno = strTurno + "0";
            if (!objBOOpera.ExtornarCompraVenta(listaOperaciones))
            {
                lblMensajeError.Text = objBOOpera.Dsc_Message;
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000023", "004", IpClient, "3", "Alerta W0000023", "Error en Extorno de Operaciones: " + lblMensajeError.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }
            else
            {
                // comentado (GGarcia-20090924)
                //omb.ShowMessage((string)htLabels["opeExtornoMoneda.msgTrxOK"], (string)htLabels["opeExtornoMoneda.lblTitulo"], "Ope_ExtornoMoneda.aspx");

                // se guarda en sesion la pagina 
                //Session["Pagina_PreImpresion"] = "Ope_ExtornarMoneda.aspx";

                // rutina de impresion (GGarcia-20090924)
                listaOperaciones[0].SCodTurno = strTurno + "1";
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
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], Define.ID_PRINTER_DOCUM_EXTORNOOPERACIONES);

        // configuracion de la impresora a utilizar
        string configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], Define.ID_PRINTER_DOCUM_EXTORNOOPERACIONES);

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
            "&" + "Pagina_PreImpresion=Ope_ExtornoMoneda.aspx", false);

    }

    /// <summary>
    /// Metodo que carga la lista de parametros a imprimir en el voucher
    /// </summary>
    private void cargarParametrosImpresion(Hashtable htPrintData)
    {
        // nombre y apellido del cajero
        htPrintData.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);

        // cantidad de operaciones
        htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, listaOperaciones.Count.ToString());

        //-- EAG 10/02/2010
        int q1 = listaOperaciones.Count / 2;
        int q2 = listaOperaciones.Count % 2;
        if (q2 != 0)
        {
            q1 = q1 + 1;
        }
        htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, listaOperaciones.Count.ToString());

        //htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_SUBDETAIL, listaOperaciones.Count.ToString());

        // numero de operaciones
        for (int i = 0, j = 0; i < listaOperaciones.Count; i++)
        {

            /*
            htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, listaOperaciones[i].SNumOperacion);
            */

            //if((i+1)%2==0)//Par
            //{
            htPrintData.Add("num_operacion" + "_" + j, listaOperaciones[i].SNumOperacion);
                htPrintData.Add("imp_mon_nac" + "_" + j, listaOperaciones[i].Imp_Mon_Nac);
                htPrintData.Add("simb_mon_int" + "_" + j, listaOperaciones[i].Simb_Moneda);
                htPrintData.Add("imp_mon_int" + "_" + j, listaOperaciones[i].Imp_Mon_Int);
                htPrintData.Add("tasa_cambio" + "_" + j,Function.FormatDecimal(listaOperaciones[i].Imp_Tasa_Cambio * Define.FACTOR_DECIMAL * Define.FACTOR_DECIMAL,4).ToString());
                j++;
            //}
            //else//Impar
            //{
            //    htPrintData.Add("codigo_ticket_impar" + "_" + j, listaOperaciones[i].SNumOperacion);
            //}

        }
        String codigo_turno = (String)Session["Cod_Turno"];
        htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, codigo_turno);
        htPrintData.Add("tipo_opera", Session["Tip_Operacion"].ToString().Trim());
    }

    private void RedirectTo(string pagina)
    {
        string redirectURL = Page.ResolveClientUrl(pagina);
        string script = "window.location = '" + redirectURL + "';";
        ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);
    }

    protected void ibtnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        FillGridViewOperaciones((string)Session["Cod_Turno"]);
    }

    protected void rbCompra_CheckedChanged(object sender, EventArgs e)
    {
        Session["Tip_Operacion"] =Define.COMPRA_MONEDA;
    }

    protected void rbVenta_CheckedChanged(object sender, EventArgs e)
    {
        Session["Tip_Operacion"] = Define.VENTA_MONEDA;
    }

    protected void grvOperaciones_Sorting(object sender, GridViewSortEventArgs e)
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
        //string strCodOpera = txtOperacion.Text;
        //DataTable dt_consulta = objBOOpera.ListarCompraVentaXFiltro((string)Session["Cod_Turno"], (string)Session["Tip_Operacion"], strCodOpera);
        //grvOperaciones.DataSource = dt_consulta;
        grvOperaciones.DataSource = dwConsulta((DataTable)ViewState["OperacionesExt"], sortExpression, direction);
        grvOperaciones.DataBind();
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

    private void ObtenerOperacionExtorno(LogOperacion objLogOpera)
    {
        DataTable dtOperaciones = (DataTable)Session["Data_Opera_Extorno"];
        DataRow[] foundRowOpera = dtOperaciones.Select("Num_Operacion = '" + objLogOpera.SNumOperacion + "'");
        string strTipOpera = foundRowOpera[0].ItemArray.GetValue(6).ToString();
        if (strTipOpera.Trim() == Define.COMPRA_MONEDA)
        {
            objLogOpera.Imp_Mon_Int = decimal.Parse(foundRowOpera[0].ItemArray.GetValue(3).ToString());
            objLogOpera.Imp_Mon_Nac = decimal.Parse(foundRowOpera[0].ItemArray.GetValue(4).ToString());
        }
        else
        {
            objLogOpera.Imp_Mon_Int = decimal.Parse(foundRowOpera[0].ItemArray.GetValue(4).ToString());
            objLogOpera.Imp_Mon_Nac = decimal.Parse(foundRowOpera[0].ItemArray.GetValue(3).ToString());
        }
        objLogOpera.Simb_Moneda = foundRowOpera[0].ItemArray.GetValue(15).ToString();
        objLogOpera.Imp_Tasa_Cambio = decimal.Parse(foundRowOpera[0].ItemArray.GetValue(2).ToString());
    }
}
