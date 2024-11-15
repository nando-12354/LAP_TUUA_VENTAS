using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using System.Collections.Generic;
using LAP.TUUA.PRINTER;
using System.Xml;
using LAP.TUUA.ALARMAS;


public partial class Ope_ExtornarTicket : System.Web.UI.Page
{
    protected int Can_Tickets;
    protected string Num_Tickets;
    protected Hashtable htLabels;
    protected BO_Operacion objBOOpera;
    private BO_Consultas objBOConsultas = new BO_Consultas();
    protected bool Flg_Error;
    protected Hashtable htParametro;
    protected decimal Imp_Total_Extorno;
    DataTable dt_consulta;
    string sMaxGrilla;

    //tickets nacionales
    protected string Num_Tickets_Nac;
    protected decimal Imp_Total_Extorno_Nac;
    protected int Can_Tickets_Nac;

    //tickets internacionales
    protected string Num_Tickets_Int;
    protected decimal Imp_Total_Extorno_Int;
    protected int Can_Tickets_Int;

    //Filtros
    string sNumeroTicket;
    string sTicketDesde;
    string sTicketHasta;
    string sTurno;

    protected void Page_Load(object sender, EventArgs e)
    {
        htLabels = LabelConfig.htLabels;
        htParametro = (Hashtable)Session["htParametro"];
        try
        {
            objBOOpera = new BO_Operacion();
            if (!IsPostBack)
            {

                btnExtornar.Text = htLabels["opeExtornoTicket.btnExtornar"].ToString();
                lblMotivo.Text = htLabels["opeExtornoTicket.lblMotivo"].ToString();
                btnExtornar_ConfirmButtonExtender.ConfirmText = htLabels["opeExtornoTicket.msgConfirm"].ToString();
                Session["Cod_Turno"] = Request.QueryString["Cod_Turno"];
                //FillGridViewTickets((string)Session["Cod_Turno"]);
                hdNumSelTotal.Value = "0";

                #region Creando la tabla para guardar los checkbox seleccionados
                DataTable dtTicketSeleccionados = new DataTable();
                dtTicketSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
                dtTicketSeleccionados.Columns.Add("Tipo_Ticket", System.Type.GetType("System.String"));
                dtTicketSeleccionados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
                dtTicketSeleccionados.Columns.Add("Imp_Precio_Dol", System.Type.GetType("System.Decimal"));
                dtTicketSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));
                
                ViewState["dtSeleccionados"] = dtTicketSeleccionados;
                #endregion
                SaveFiltros();
                BindPagingGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">CheckBoxHeaderGrilla();</script>", false);

                if (rbTicket.Checked)// txtNumTicket.Text != "")
                {
                    txtNumTicket.Enabled = true;
                    txtNumTicket.BackColor = System.Drawing.Color.White;
                    txtNroIni.BackColor = System.Drawing.Color.FromArgb(0xCCCCCC);
                    txtNroFin.BackColor = System.Drawing.Color.FromArgb(0xCCCCCC);
                }
                else
                {
                    txtNroIni.BackColor = System.Drawing.Color.White;
                    txtNroFin.BackColor = System.Drawing.Color.White;
                    txtNumTicket.Enabled = false;
                    txtNumTicket.BackColor = System.Drawing.Color.FromArgb(0xCCCCCC);
                }
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


    protected void btnExtornar_Click(object sender, EventArgs e)
    {
        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);

        if (ViewState["dtSeleccionados"] == null || ((DataTable)ViewState["dtSeleccionados"]).Rows.Count <= 0)
        {
            lblMensajeError.Text = "Debe de seleccionar al menos un ticket para extornar.";
            return;
        }
        //if (!Validar())
        //{
        //    return;
        //}
        try
        {
            objBOOpera = new BO_Operacion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

            DataTable dt = ((DataTable)ViewState["dtSeleccionados"]).Copy();

            Num_Tickets = "";
            Imp_Total_Extorno = 0;
            Can_Tickets = 0;

            string tipoTicket = "";

            //tickets nacionales
            Num_Tickets_Nac = "";
            Imp_Total_Extorno_Nac = 0;
            Can_Tickets_Nac = 0;

            //tickets internacionales
            Num_Tickets_Int = "";
            Imp_Total_Extorno_Int = 0;
            Can_Tickets_Int = 0;

            foreach (DataRow row in dt.Select("Check = true"))
            {
                Num_Tickets += row["Cod_Numero_Ticket"].ToString();
                Imp_Total_Extorno +=  Convert.ToDecimal(row["Imp_Precio_Dol"]);
                Can_Tickets++;

                tipoTicket = row["Tipo_Ticket"].ToString();
                //validamos por TipoTicket
                if(tipoTicket.Equals("INTERNACIONAL")){
                Num_Tickets_Int += row["Cod_Numero_Ticket"].ToString();
                Imp_Total_Extorno_Int += Convert.ToDecimal(row["Imp_Precio_Dol"]);
                Can_Tickets_Int++;
                }
                if (tipoTicket.Equals("NACIONAL"))
                {
                Num_Tickets_Nac += row["Cod_Numero_Ticket"].ToString();
                Imp_Total_Extorno_Nac += Convert.ToDecimal(row["Imp_Precio_Dol"]);
                Can_Tickets_Nac++;
                }
            }

            Session["Num_Tickets"] = Num_Tickets;
            Session["Can_Tickets"] = Can_Tickets;
            Session["Dsc_Motivo"] = txtMotivo.Text;

            if (Can_Tickets_Int > 0)
            {
                Session["Num_Tickets_Int"] = Num_Tickets_Int;
                Session["Can_Tickets_Int"] = Can_Tickets_Int;
                //Session["Dsc_Motivo_Int"] = txtMotivo.Text;
            }
            
            if (Can_Tickets_Nac > 0)
            {
                Session["Num_Tickets_Nac"] = Num_Tickets_Nac;
                Session["Can_Tickets_Nac"] = Can_Tickets_Nac;
            }

            int maxExtornos = 0;
            DataTable dt_parametro = objBOConsultas.ListarParametros("ME");
            if (dt_parametro.Rows.Count > 0)
            {
                maxExtornos = Int32.Parse(Convert.ToString(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()));
            }
            else
            {
                maxExtornos = 500;
            }

            if (Can_Tickets > maxExtornos)
            {
                System.Threading.Thread.Sleep(500);
                this.lblMensajeError.Text = "Sobrepaso el maximo de Extornos (" + maxExtornos + ")";
                return;
            }

            if (!objBOOpera.ExtornarTicket(Num_Tickets, (string)Session["Cod_turno"], Can_Tickets, "0", txtMotivo.Text))
            {
                lblMensajeError.Text = objBOOpera.Dsc_Message;
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000024", "004", IpClient, "3", "Alerta W0000024", "Error en Extorno de Ticket: " + lblMensajeError.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
            }
            else
            {
                // Comentado (GGarcia-20090924)
                //omb.ShowMessage((string)htLabels["opeExtornoTicket.msgTrxOK"], (string)htLabels["opeExtornoTicket.lblTitulo"], "Ope_ExtornoTicket.aspx");

                // se guarda en sesion la pagina 
                //Session["Pagina_PreImpresion"] = "Ope_ExtornarTicket.aspx";

                // rutina de impresion (GGarcia-20090924)
                Imprimir();

                //RedirectTo();
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

    //Ya no se usa. EAG
    private void RedirectTo()
    {
        //string redirectURL = Page.ResolveClientUrl("Ope_Impresion.aspx");
        //string script = "window.location = '" + redirectURL + "';";
        //ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);

        Response.Redirect("Ope_Impresion.aspx", false);
        //Se pone false pues el Response.End bota un excepcion (comportamiento normal). 
        //Si se deseara usar este metodo sin el 2do argumento, deberia estar fuera del try{}catch{}, y asi el redirect seria inmediato (acaba la ejecucion una vez ejecutado el redirect).
        //En cambio con el parametro false, uno puedo seguir con la ejecucion hasta el final del metodo. Ojo, se puede volver a ejecutar otro redirect cuantas veces haya, y cambiaria.
    } 

    private void Imprimir()
    {
        // instanciar objeto 
        Print objPrint = new Print();

        // obtiene el nodo segun el nombre del voucher
        XmlElement nodo = objPrint.ObtenerNodo((XmlDocument)Session["xmlDoc"], Define.ID_PRINTER_DOCUM_EXTORNOTICKET);

        // configuracion de la impresora a utilizar
        string configImpVoucher = objPrint.ObtenerConfiguracionImpresora(nodo, (Hashtable)Session["htParamImp"], Define.ID_PRINTER_DOCUM_EXTORNOTICKET);

        //---
        if (Session["PuertoVoucher"] != null && !Session["PuertoVoucher"].ToString().Equals(String.Empty))
        {
            configImpVoucher = Session["PuertoVoucher"].ToString() + "," + configImpVoucher.Split(new char[] { ',' }, 2)[1];
        }
        //---

        //Lista de Tipo_Tickets
        ArrayList lista = new ArrayList();
        if(Can_Tickets_Int > 0 && Can_Tickets_Nac > 0){
            lista.Add("Nacional");
            lista.Add("Internacional");
        }
        else if (Can_Tickets_Int > 0)
        {
            lista.Add("Internacional");
        }
        else if (Can_Tickets_Nac > 0)
        {
            lista.Add("Nacional");
        }

        // carga los parametros a imprimir con la impresora de voucher
        Hashtable htPrintData;
        string dataVoucher = "";
        //recorremos la lista
        for (int k = 0; k < lista.Count; k++ )
        {
            if (lista[k].Equals("Nacional"))
            {
               htPrintData = new Hashtable();
               cargarParametrosImpresion(htPrintData, lista[k].ToString(), Can_Tickets_Nac, Num_Tickets_Nac, Imp_Total_Extorno_Nac);
               dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);
            }
            else {
               htPrintData = new Hashtable();
               cargarParametrosImpresion(htPrintData, lista[k].ToString(), Can_Tickets_Int, Num_Tickets_Int, Imp_Total_Extorno_Int);
               dataVoucher += objPrint.ObtenerDataFormateada(htPrintData, nodo);
            }
        }

       
       // ArrayList listaImp = new ArrayList();
       // cargarParametrosImpresion(htPrintData);
       // cargarParametrosImpresion(listaImp, lista);

        // obtiene la data a imprimir con la impresora de voucher y guardarla en una variable de sesion
        //string dataVoucher = objPrint.ObtenerDataFormateada(htPrintData, nodo);
        
        //int copias = objPrint.ObtenerCopiasVoucher(nodo);

        Session["dataSticker"] = "";
        Session["dataVoucher"] = dataVoucher;
        
        //String configImpSticker = objPrint.ObtenerConfiguracionImpresoraDefault((Hashtable)Session["htParamImp"], Define.ID_PRINTER_KEY_STICKER);

        //Session["configImpSticker"] = configImpSticker;
        //Session["configImpVoucher"] = configImpVoucher;
        //Session["flagImpSticker"] = "0";
        //Session["flagImpVoucher"] = "1";
        //Session["copiasVoucher"] = copias;

        //String codigo_turno = (String)Session["Cod_Turno"];

        Response.Redirect("Ope_Impresion.aspx?" + 
            "flagImpSticker=0" + 
            "&" + "flagImpVoucher=1" + 
            //"&" + "copiasVoucher=" + copias +
            "&" + "configImpSticker=" + "" + 
            "&" + "configImpVoucher=" + configImpVoucher +
            "&" + "id_mensaje=Impresion_SoloVoucher"+
            "&" + "Pagina_PreImpresion=Ope_ExtornoTicket.aspx", false);

    }

    /// <summary>
    /// Metodo que carga la lista de parametros a imprimir en el voucher
    /// </summary>
    private void cargarParametrosImpresion(Hashtable htPrintData, string tipo_ticket, int Can_Tickets, string Num_Tickets, decimal Imp_Total_Extorno)
    //private void cargarParametrosImpresion(ArrayList listaPrint, ArrayList lista)
    {
       //titulo del voucher
        htPrintData.Add("titulo_voucher", tipo_ticket);
       // nombre y apellido del cajero
        htPrintData.Add(Define.ID_PRINTER_PARAM_NOMBRE_CAJERO, (string)Session["Nombre_Usuario"]);

        // cantidad de tickets
        htPrintData.Add(Define.ID_PRINTER_PARAM_CANTIDAD_TICKET, Can_Tickets.ToString());
        
        //importe total de extorno
        htPrintData.Add("imp_extorno",Function.FormatDecimal(Imp_Total_Extorno,Define.NUM_DECIMAL).ToString());
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
   //         htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TICKET + "_" + i, Num_Tickets.Substring(contador, intLongTicket));
   //         contador += intLongTicket;*/
            

            if((i+1)%2==0)//Par
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

        //EAG 29/01/2010
        String codigo_turno = (String)Session["Cod_Turno"];
        htPrintData.Add(Define.ID_PRINTER_PARAM_CODIGO_TURNO, codigo_turno);
        //EAG 29/01/2010
    }

    protected void grvTicket_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);
        grvTicket.PageIndex = e.NewPageIndex;
        BindPagingGrid();
        lblTxtSeleccionados.Text = hdNumSelTotal.Value;
    }

    protected void grvTicket_Sorting(object sender, GridViewSortEventArgs e)
    {
        guardarSeleccionesCheckBox((DataTable)ViewState["dtSeleccionados"]);
        BindPagingGrid();
        lblTxtSeleccionados.Text = hdNumSelTotal.Value;
    }

    void ValidarTamanoGrilla()
    {
        //Traer valor de Tamaño Grilla desde Congifuracion Parametros Generales
        DataTable dt_parametrogeneral = objBOConsultas.ListarParametros("LG");

        if (dt_parametrogeneral.Rows.Count > 0)
        {
            this.sMaxGrilla = dt_parametrogeneral.Rows[0].ItemArray.GetValue(4).ToString();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (validarRangos())
        {
            ViewState["dtSeleccionados"] = null; //para eliminar los datos anteriores
            lblError.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script language=\"javascript\">SetNumTotal();</script>", false);
            hdNumSelTotal.Value = "0";

            SaveFiltros();
            BindPagingGrid();

            #region Creando la tabla para guardar los checkbox seleccionados
            DataTable dtTicketSeleccionados = new DataTable();
            dtTicketSeleccionados.Columns.Add("Numero", System.Type.GetType("System.Int32"));
            dtTicketSeleccionados.Columns.Add("Tipo_Ticket", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Cod_Numero_Ticket", System.Type.GetType("System.String"));
            dtTicketSeleccionados.Columns.Add("Imp_Precio_Dol", System.Type.GetType("System.Decimal"));
            dtTicketSeleccionados.Columns.Add("Check", System.Type.GetType("System.Boolean"));

            ViewState["dtSeleccionados"] = dtTicketSeleccionados;
            #endregion
        }
    }

    #region Dynamic data query
    private void BindPagingGrid()
    {
        ValidarTamanoGrilla();
        RecuperarFiltros();
        grvTicket.VirtualItemCount = GetRowCount();

        DataTable dt_consulta = new DataTable();

        dt_consulta = GetDataPage(grvTicket.PageIndex, grvTicket.PageSize, grvTicket.OrderBy);

        htLabels = LabelConfig.htLabels;
        if (dt_consulta.Rows.Count < 1)
        {
            try
            {
                //this.lblMensajeError.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
                this.lblTxtIngresados.Text = "";
                this.lblTxtSeleccionados.Text = "";
            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ViewState["dtSeleccionados"] = null;
            
            lblMensajeError.Text = "No se encontro resultado alguno.";
            grvTicket.DataSource = null;
            grvTicket.DataBind();
        }
        else
        {
            htLabels = LabelConfig.htLabels;
            grvTicket.DataSource = dt_consulta;
            grvTicket.PageSize = Convert.ToInt32(this.sMaxGrilla);
            grvTicket.DataBind();
            this.lblMensajeError.Text = "";
        }
    }

    private void SaveFiltros()
    {
        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sNumeroTicket", txtNumTicket.Text.Trim()));
        filterList.Add(new Filtros("sTicketDesde", txtNroIni.Text.Trim()));
        filterList.Add(new Filtros("sTicketHasta", txtNroFin.Text.Trim()));
        filterList.Add(new Filtros("sTurno",(string)Session["Cod_Turno"]));
        
        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        sNumeroTicket = newFilterList[0].Valor;
        sTicketDesde = newFilterList[1].Valor;
        sTicketHasta = newFilterList[2].Valor;
        sTurno = newFilterList[3].Valor;
    }

    private int GetRowCount()
    {
        int count = 0;
        try
        {
            DataTable dt_consulta = new DataTable();

            if (rbTicket.Checked == true)
                //objBOOpera.ListarTicketsExtorno(strNumTicket, strDelNro, strAlNro, strTurno, null, 0, 30, "1", "0");
                dt_consulta = objBOOpera.ListarTicketsExtorno(sNumeroTicket,
                                                                        "",
                                                                        "",
                                                                        sTurno,
                                                                        null,
                                                                        0, 0, "0", "1");
            else
            {
                if (this.rbRango.Checked == true)
                {
                    dt_consulta = objBOOpera.ListarTicketsExtorno("",
                                                                        sTicketDesde,
                                                                        sTicketHasta,
                                                                        sTurno,
                                                                        null,
                                                                        0, 0, "0", "1");
                }
            }

            if (dt_consulta.Columns.Contains("TotRows"))
            {
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
                lblTxtIngresados.Text = count.ToString();
                lblTxtSeleccionados.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
        return count;
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        DataTable dt_consulta = new DataTable();
        try
        {
            if (rbTicket.Checked == true)
                dt_consulta = objBOOpera.ListarTicketsExtorno(sNumeroTicket,
                                                                       "",
                                                                       "",
                                                                       sTurno,
                                                                       sortExpression,
                                                                       pageIndex, Convert.ToInt32(sMaxGrilla), "1", "0");
            else
            {
                if (this.rbRango.Checked == true)
                {
                    dt_consulta = objBOOpera.ListarTicketsExtorno("",
                                                                       sTicketDesde,
                                                                       sTicketHasta,
                                                                       sTurno,
                                                                       sortExpression,
                                                                       pageIndex, Convert.ToInt32(sMaxGrilla), "1", "0");
                }
            }
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
        return dt_consulta;
    }
    #endregion

    public bool validarRangos()
    {
        if (rbRango.Checked)
        {
            if ((txtNroIni.Text == "" && txtNroFin.Text != "") || (txtNroIni.Text != "" && txtNroFin.Text == ""))
            {
                lblError.Text = "Ingrese ambos rangos";
                return false;
            }
            else
            {
                if (txtNroIni.Text != "" && txtNroFin.Text != "" && (Convert.ToInt64(txtNroFin.Text) < Convert.ToInt64(txtNroIni.Text)))
                {
                    lblError.Text = "Error en Rango de Tickets";
                    return false;
                }
            }
        }
     
        return true;
    }
    
    

    protected void guardarSeleccionesCheckBox(DataTable dt_registros)
    {
        int pageSize = grvTicket.Rows.Count;

        //ELIMINAMOS SI EXISTEN REGISTROS CON CHECK=FALSE
        if (dt_registros != null)
        {
            DataRow[] foundRowCheck = dt_registros.Select("Check = false");
            foreach (DataRow row in foundRowCheck)
            {
                row.Delete();
            }

            for (int i = 0; i < pageSize; i++)
            {
                CheckBox chkSeleccionar = (CheckBox)grvTicket.Rows[i].FindControl("chkSeleccionar");
                decimal monto = 0;
                try { monto = Convert.ToDecimal(grvTicket.DataKeys[i].Value.ToString()); }
                catch { }
                LinkButton lbCodTicket = (LinkButton)grvTicket.Rows[i].FindControl("codTicket");
                String TipoTicket = grvTicket.Rows[i].Cells[1].Text;
                if (chkSeleccionar.Checked)
                {
                    //BUSCAMOS EN LA TABLA SI EXISTE EL REGISTRO
                    int nroFilas = dt_registros.Rows.Count;
                    DataRow[] rows = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "");
                    int filas = rows.Length;
                    if (filas == 0)
                    {
                        //no existe el registro y lo agregamos
                        dt_registros.Rows.Add(dt_registros.NewRow());
                        dt_registros.Rows[nroFilas]["Numero"] = (nroFilas + 1).ToString();
                        dt_registros.Rows[nroFilas]["Tipo_Ticket"] = TipoTicket;
                        dt_registros.Rows[nroFilas]["Cod_Numero_Ticket"] = lbCodTicket.Text;
                        dt_registros.Rows[nroFilas]["Imp_Precio_Dol"] = monto;
                        dt_registros.Rows[nroFilas]["Check"] = chkSeleccionar.Checked;
                    }
                    //else
                    //{ //Ya existe el registro y actualizamos la informacion
                    //    if (filas == 1)
                    //    {
                    //        rows[0]["Motivo"] = txtMotivo.Text;
                    //    }
                    //}
                }
                else
                {
                    //BUSCAMOS SI EL REGISTRO SE ENCUENTRA EN LA TABLA DE SELECCIONADOS PARA ELIMINARLO
                    if (lbCodTicket.Text != "")
                    {
                        int filas = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "").Length;
                        if (filas == 1)
                        {
                            DataRow[] foundRow = dt_registros.Select("Cod_Numero_Ticket = " + lbCodTicket.Text + "");

                            int orden = Convert.ToInt32(foundRow[0]["Numero"]);
                            dt_registros.Rows.Remove(foundRow[0]);

                            //actualizamos indices (Numero)
                            DataRow[] rowsOver = dt_registros.Select("Numero >= " + orden + "");
                            foreach (DataRow row in rowsOver)
                            {
                                row["Numero"] = orden;
                                orden++;
                            }

                        }
                    }
                }
            }

            dt_registros.AcceptChanges();

            ViewState["dtSeleccionados"] = dt_registros;
            //return dt_registros;
        }
    }

    protected void grvTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //BUSCAMOS EN LA TABLA SELECCIONADOS PARA VER SU ESTADO
            DataTable dt_seleccionados = (DataTable)ViewState["dtSeleccionados"];

            if ((dt_seleccionados != null) && (dt_seleccionados.Rows.Count > 0))
            {
                DataRow[] rows = dt_seleccionados.Select("Cod_Numero_Ticket = " + ((System.Data.DataRowView)(e.Row.DataItem)).Row["Cod_Numero_Ticket"].ToString() + "");
                int nroFilas = rows.Length;
                if (nroFilas == 1)
                {
                    CheckBox chkSeleccionar = (CheckBox)e.Row.FindControl("chkSeleccionar");
                    chkSeleccionar.Checked = Convert.ToBoolean(rows[0].ItemArray.GetValue(3));
                }
            }
        }
    }

    protected void grvTicket_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("ShowTicket"))
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            GridViewRow row = grvTicket.Rows[rowIndex];
            LinkButton addButton = (LinkButton)row.Cells[1].FindControl("codTicket");
            ConsDetTicket.Inicio(addButton.Text + "-" + addButton.Text);
        }
    }
}
