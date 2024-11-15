using System;
using System.Data;
using System.Data.SqlClient;
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
using System.Text;

public partial class Modulo_Consultas_ConsultaTicketxFecha : System.Web.UI.Page
{
    //Label
    protected Hashtable htLabels;
    protected bool Flg_Error;
    Int32 valorMaxGrilla;

    BO_Consultas objConsulta = new BO_Consultas();

    DataTable dt_consulta = new DataTable();
    DataTable dt_parametroTurno = new DataTable();

    //Filtros
    private string sTipoDoc;
    string sFechaDesde, sFechaHasta;
    string sHoraDesde, sHoraHasta;
    string sIdCompania;
    string sTipoTicket;
    string sEstadoTicket;
    string sPersona;
    string sVuelo;
    string sFlgCobro;
    string sFlgMasiva;
    string sEstadoTurno;
    string sCajero;
    string sMedioAnulacion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblDesde.Text = htLabels["mconsultaTicketFecha.lblDesde.Text"].ToString();
                this.lblHasta.Text = htLabels["mconsultaTicketFecha.lblHasta.Text"].ToString();

                this.lblCompania.Text = htLabels["mconsultaTicketFecha.lblCompania.Text"].ToString();
                this.lblTipoTicket.Text = htLabels["mconsultaTicketFecha.lblTipoTicket.Text"].ToString();
                this.lblEstado.Text = htLabels["mconsultaTicketFecha.lblEstado.Text"].ToString();
                this.lblPersona.Text = htLabels["mconsultaTicketFecha.lblPersona.Text"].ToString();
                this.btnConsultar.Text = htLabels["mconsultaTicketFecha.btnConsultar.Text"].ToString();

                this.lblTipoOperacion.Text = htLabels["mconsultaTicketFecha.lblTipoOperacion"].ToString();

                this.lblHoraDesde.Text = htLabels["mconsultaDetalleturno.lblHoraDesde.Text"].ToString();
                this.lblHoraHasta.Text = htLabels["mconsultaDetalleturno.lblHoraHasta.Text"].ToString();
                //this.lblTicketVentaMasiva.Text = htLabels["mconsultaDetalleturno.chkTicketVentaMasiva.Text"].ToString();
                this.lblFlgCobro.Text = htLabels["mconsultaDetalleturno.lblFlgCobro.Text"].ToString();

                this.lblVuelo.Text = htLabels["tuua.general.lblTipoVuelo"].ToString();

                this.chkTicketVentaMasiva.Text = htLabels["mconsultaDetalleturno.chkTicketVentaMasiva.Text"].ToString();

                this.lblEstadoTurno.Text = htLabels["mconsultaTicketFecha.lblEstadoTurno.Text"].ToString();
                this.lblCajero.Text = htLabels["mconsultaTicketFecha.lblCajero.Text"].ToString();

                this.lblMedioAnulacion.Text = htLabels["mconsultaTicketFecha.lblMedioAnulacion.Text"].ToString();
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

            this.txtDesde.Text = DateTime.Now.ToShortDateString();
            this.txtHasta.Text = DateTime.Now.ToShortDateString();

            DataTable dt_parametro = objConsulta.ListarParametros("AA");

            if (dt_parametro.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt_parametro.Rows[0].ItemArray.GetValue(4).ToString()) == 0)
                {


                    grvPaginacionBoarding.Columns[12].Visible = false;

                }

            }

            CargarCombos();
            ddlTipoDocumento.SelectedValue = "T";
            this.ddlMedioAnulacion.Enabled = false;
            SaveFiltros();
            

            //grvPaginacion.VirtualItemCount = GetRowCount();
            //BindPagingGrid();
            CargarGrillaTicket();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
           // ValidarTipoOperacion();

            //lblMensajeError.Text = "";
            //lblMensajeErrorData.Text = "";
        }
        //SetFiltros();
        Session["Print_TicketxFecha"] = null;
    }

    public void CargarCombos()
    {
        try
        {
            BO_Consultas objConsulta = new BO_Consultas();
            BO_Administracion objListaTipoTicket = new BO_Administracion();

            //Carga combo Tipo documento                  
            DataTable dt_tipodocumento = new DataTable();
            dt_tipodocumento = objConsulta.ListaCamposxNombre("TipoDocumento");
            dt_tipodocumento.DefaultView.Sort = "Dsc_Campo DESC";
            UIControles objCargaCombo = new UIControles();
            objCargaCombo.LlenarCombo(ddlTipoDocumento, dt_tipodocumento, "Cod_Campo", "Dsc_Campo", false, false);

            CargarEstados();

            //Carga combo Tipo ticket
            DataTable dt_tipoticket = new DataTable();
            dt_tipoticket = objListaTipoTicket.ListaTipoTicket();
            UIControles objCargaComboTipoTicket = new UIControles();
            objCargaComboTipoTicket.LlenarCombo(ddlTipoTicket, dt_tipoticket, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", true, false);

            //Carga combo Tipo persona y Tipo Vuelo
            DataTable dt_tipopersona = new DataTable();
            DataTable dt_tipovuelo = new DataTable();
            dt_tipopersona = objConsulta.ListaCamposxNombre("TipoPasajero");
            dt_tipovuelo = objConsulta.ListaCamposxNombre("TipoVuelo");
            UIControles objCargaComboPersona = new UIControles();
            objCargaComboPersona.LlenarCombo(ddlPersona, dt_tipopersona, "Cod_Campo", "Dsc_Campo", true, false);
            UIControles objCargaComboVuelo = new UIControles();
            objCargaComboVuelo.LlenarCombo(ddlVuelo, dt_tipovuelo, "Cod_Campo", "Dsc_Campo", true, false);
           
            //Carga combo Flag Cobro            
            DataTable dt_FlgCobro = new DataTable();
            dt_FlgCobro = objConsulta.ListaCamposxNombre("FlgCobro");
            UIControles objCargaComboFlgCobro = new UIControles();
            objCargaComboFlgCobro.LlenarComboSinValue(ddlFlgCobro, dt_FlgCobro, "Cod_Campo", "Dsc_Campo", true, false);

            //Carga combo compañia
            DataTable dt_allcompania = new DataTable();
            dt_allcompania = objConsulta.listarAllCompania();
            UIControles objCargaComboCompania = new UIControles();
            objCargaComboCompania.LlenarCombo(ddlCompania, dt_allcompania, "Cod_Compania", "Dsc_Compania", true, false);

            //Cargar ComboBox EstadoTurno

            DataTable dt_estadoturno = new DataTable();
            dt_estadoturno = objConsulta.ListaCamposxNombre("EstadoTurno");
            UIControles objCargaComboEstadoTurno = new UIControles();
            objCargaComboEstadoTurno.LlenarCombo(ddlEstadoTurno, dt_estadoturno, "Cod_Campo", "Dsc_Campo", true, false);
            

            //Cargar combobox cajeros
            objConsulta = new BO_Consultas();
           // DataTable dtUsuarios = objConsulta.ListarUsuarioxRol("R0005");
            DataTable dtUsuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL]);
            DataRow dtrTodos = dtUsuarios.NewRow();
            dtrTodos["Usuario"] = Define.ID_FILTRO_TODOS;
            dtUsuarios.Rows.InsertAt(dtrTodos, 0);
                        
            ddlCajero.DataSource = dtUsuarios;
            ddlCajero.DataTextField = "Usuario";
            ddlCajero.DataValueField = "Cod_Usuario";
            ddlCajero.DataBind();

            //CargarMedioAnulacion(); 
            //Cargar combobox medioAnulacion
            DataTable dt_MedioAnulacion = new DataTable();
            dt_MedioAnulacion = objConsulta.ListaCamposxNombre("MedAnulacion");
            UIControles objCargaComboMedAnulacion = new UIControles();
            objCargaComboMedAnulacion.LlenarCombo(ddlMedioAnulacion, dt_MedioAnulacion, "Cod_Campo", "Dsc_Campo", true, false);
        
        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }

    protected void CargarEstados()
    {
        //Carga combo Estado ticket
        BO_Consultas objListaCampos = new BO_Consultas();
        DataTable dt_estado = new DataTable();
        string strTipDoc = ddlTipoDocumento.SelectedValue.ToString();
        if (strTipDoc == Define.TIP_DOC_BOARDING)
        {
            dt_estado = objListaCampos.ListaCamposxNombre("EstadoBcbp");
        }
        else
        {
            dt_estado = objListaCampos.ListaCamposxNombre("EstadoTicket");
        }

        //Agregamos a la tabla estado el tipo VENCIDO
        DataRow newEstado = dt_estado.NewRow();

        newEstado["Cod_Campo"] = "1";
        newEstado["Dsc_Campo"] = "VENCIDO";

        dt_estado.Rows.Add(newEstado);

        //Cargar combo Estado de ticket
        UIControles objCargaComboEstTicket = new UIControles();
        objCargaComboEstTicket.LlenarCombo(ddlEstadoTicket, dt_estado, "Cod_Campo", "Dsc_Campo", true, false);
    }

    /*protected void CargarMedioAnulacion() {
        BO_Consultas objListaCampos = new BO_Consultas();
        DataTable dt_MedioAnulacion = new DataTable();

        string strTipDoc = ddlTipoDocumento.SelectedValue.ToString();
        if (strTipDoc == Define.TIP_DOC_BOARDING)
        {
            dt_MedioAnulacion = objConsulta.ListaCamposxNombre("MedAnulacion");
            dt_MedioAnulacion.Rows.RemoveAt(1);
        }
        else
        {
            dt_MedioAnulacion = objConsulta.ListaCamposxNombre("MedAnulacion");
        }
        UIControles objCargaComboMedAnulacion = new UIControles();
        objCargaComboMedAnulacion.LlenarCombo(ddlMedioAnulacion, dt_MedioAnulacion, "Cod_Campo", "Dsc_Campo", true, false);
    }*/

    protected Int64 GetMaximoExcel()
    {
        Int64 iMaxReporte = 0;
        BO_Consultas objParametro = new BO_Consultas();
        DataTable dt_max = objParametro.ListarParametros("LE");

        if (dt_max.Rows.Count > 0)
            iMaxReporte = Convert.ToInt64(dt_max.Rows[0].ItemArray.GetValue(4).ToString());

        return iMaxReporte;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (IsFechaValida())
        {
            SaveFiltros();
            CargarGrillaTicket();
            //ValidarTipoOperacion();
            lblMaxRegistros.Value = GetMaximoExcel().ToString();
        }
    }

    protected void CargarGrillaTicket()
    {
        RecuperarFiltros();
        if (ddlTipoDocumento.SelectedValue == "T")
        {
            grvPaginacion.Visible = true;
            grvPaginacionBoarding.Visible = false;
            lblMensajeError.Text = "";
            lblMensajeErrorData.Text = "";
        }
        else
        {
         
            grvPaginacion.Visible = false;
            grvPaginacionBoarding.Visible = true;

          
           
            lblMensajeError.Text = "";
            lblMensajeErrorData.Text = "";
        }
        BindPagingGrid();
    }

    private void BindPagingGrid()
    {
        ValidarTamanoGrilla();
        if (ddlTipoDocumento.SelectedValue == "T")
        {
            
            grvPaginacion.VirtualItemCount = GetRowCount();
            dt_consulta = GetDataPage(grvPaginacion.PageIndex, grvPaginacion.PageSize, grvPaginacion.OrderBy);

            if (dt_consulta.Rows.Count == 0)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
                grvPaginacion.Visible = false;
                grvPaginacionBoarding.Visible = false;
                lblTotal.Text = "";
                lblTotalRows.Value = "";
            }
            else
            {
                /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();
                    this.lblMensajeError.Text = "";
                    lblTotal.Text = "";
                    lblTotalRows.Value = "";
                    grvPaginacion.Visible = false;
                    grvPaginacionBoarding.Visible = false;
                }
                else
                {
                    lblMensajeErrorData.Text = "";
                    htLabels = LabelConfig.htLabels;
                    grvPaginacion.Visible = true;
                    grvPaginacionBoarding.PageSize = valorMaxGrilla;
                    grvPaginacion.PageSize = valorMaxGrilla;
                    grvPaginacion.DataSource = dt_consulta;
                    grvPaginacion.DataBind();
                    lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + grvPaginacion.VirtualItemCount;
                    lblTotalRows.Value = grvPaginacion.VirtualItemCount.ToString();
                }
            }
        }
        else
        {
            grvPaginacionBoarding.VirtualItemCount = GetRowCount();
            dt_consulta = GetDataPage(grvPaginacionBoarding.PageIndex, grvPaginacionBoarding.PageSize, grvPaginacionBoarding.OrderBy);

            if (dt_consulta.Rows.Count == 0)
            {
                htLabels = LabelConfig.htLabels;
                try
                {
                    this.lblMensajeErrorData.Text = htLabels["mconsultaNoData.lblMensajeError.Text"].ToString();
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
                grvPaginacion.Visible = false;
                grvPaginacionBoarding.Visible = false;
                lblTotal.Text = "";
                lblTotalRows.Value = "";
            }
            else
            {
                /*---------BUSCANDO DATA EN ARCHIVAMIENTO----------*/
                if (dt_consulta.Columns[0].ColumnName == "MsgError")
                {
                    this.lblMensajeErrorData.Text = dt_consulta.Rows[0]["Descripcion"].ToString();

                    this.lblMensajeError.Text = "";
                    lblTotal.Text = "";
                    lblTotalRows.Value = "";
                    grvPaginacion.Visible = false;
                    grvPaginacionBoarding.Visible = false;
                }
                else
                {
                    lblMensajeErrorData.Text = "";
                    htLabels = LabelConfig.htLabels;
                    grvPaginacionBoarding.Visible = true;
                    grvPaginacionBoarding.DataSource = dt_consulta;
                    grvPaginacionBoarding.PageSize = valorMaxGrilla;
                    grvPaginacionBoarding.DataBind();
                    lblTotal.Text = ((htLabels["tuua.general.lblTotal"] == null) ? "" : htLabels["tuua.general.lblTotal"].ToString()) + " " + grvPaginacionBoarding.VirtualItemCount;
                    lblTotalRows.Value = grvPaginacionBoarding.VirtualItemCount.ToString();
                }
            }
        }
    }

    #region Validaciones
    public void ValidarTipoOperacion()
    {
       /* if (ddlTipoDocumento.SelectedValue == "B")
        {
            ddlPersona.Enabled = false;
            ddlTipoTicket.Enabled = false;
        }
        if (ddlTipoDocumento.SelectedValue == "T")
        {
            ddlPersona.Enabled = true;
            ddlTipoTicket.Enabled = true;
        }*/
    }

    private void SaveFiltros()
    {
        //sIdCompania = ddlCompania.SelectedValue;
        //sTipoTicket = ddlTipoTicket.SelectedValue;
        //sEstadoTicket = ddlEstadoTicket.SelectedValue;
        //sPersona = ddlPersona.SelectedValue;
        //sVuelo = ddlVuelo.SelectedValue;
        //sFlgCobro = (ddlFlgCobro.SelectedValue == "-") ? "" : ddlFlgCobro.SelectedValue;
        //sFlgMasiva = (chkTicketVentaMasiva.Checked == true) ? "1" : "";
        //sTipoDoc = ddlTipoDocumento.SelectedValue;

        //sFechaDesde = Fecha.convertToFechaSQL2(txtDesde.Text);
        //sFechaHasta = Fecha.convertToFechaSQL2(txtHasta.Text);
        //sHoraDesde = Fecha.convertToHoraSQL(txtHoraDesde.Text);
        //sHoraHasta = Fecha.convertToHoraSQL(txtHoraHasta.Text);

        List<Filtros> filterList = new List<Filtros>();

        filterList.Add(new Filtros("sIdCompania", ddlCompania.SelectedValue));
        filterList.Add(new Filtros("sTipoTicket", ddlTipoTicket.SelectedValue));
        filterList.Add(new Filtros("sEstadoTicket", ddlEstadoTicket.SelectedValue));
        filterList.Add(new Filtros("sPersona", ddlPersona.SelectedValue));
        filterList.Add(new Filtros("sVuelo", ddlVuelo.SelectedValue));

        filterList.Add(new Filtros("sFlgCobro", (ddlFlgCobro.SelectedValue == "-") ? "" : ddlFlgCobro.SelectedValue));
        filterList.Add(new Filtros("sFlgMasiva", (chkTicketVentaMasiva.Checked == true) ? "1" : ""));
        filterList.Add(new Filtros("sTipoDoc", ddlTipoDocumento.SelectedValue));
        filterList.Add(new Filtros("sFechaDesde", Fecha.convertToFechaSQL2(txtDesde.Text)));

        filterList.Add(new Filtros("sFechaHasta", Fecha.convertToFechaSQL2(txtHasta.Text)));
        filterList.Add(new Filtros("sHoraDesde", Fecha.convertToHoraSQL(txtHoraDesde.Text)));
        filterList.Add(new Filtros("sHoraHasta", Fecha.convertToHoraSQL(txtHoraHasta.Text)));
        filterList.Add(new Filtros("sEstadoTurno", ddlEstadoTurno.SelectedValue));
        filterList.Add(new Filtros("sCajero", ddlCajero.SelectedValue));
        filterList.Add(new Filtros("sMedioAnulacion", ddlMedioAnulacion.SelectedValue));

        ViewState.Add("Filtros", filterList);
    }

    private void RecuperarFiltros()
    {
        List<Filtros> newFilterList = (List<Filtros>)ViewState["Filtros"];

        //sFechaDesde = newFilterList[0].Valor;
        sIdCompania = newFilterList[0].Valor;
        sTipoTicket = newFilterList[1].Valor;
        sEstadoTicket = newFilterList[2].Valor;
        sPersona = newFilterList[3].Valor;
        sVuelo = newFilterList[4].Valor;
        sFlgCobro = newFilterList[5].Valor;
        sFlgMasiva = newFilterList[6].Valor;
        sTipoDoc = newFilterList[7].Valor;

        sFechaDesde = newFilterList[8].Valor;
        sFechaHasta = newFilterList[9].Valor;
        sHoraDesde = newFilterList[10].Valor;
        sHoraHasta = newFilterList[11].Valor;
        sEstadoTurno = newFilterList[12].Valor;
        sCajero = newFilterList[13].Valor;
        sMedioAnulacion = newFilterList[14].Valor;

    }
    void ValidarTamanoGrilla()
    {
        //Traer valor de tamaño de la grilla desde parametro general              
        dt_parametroTurno = objConsulta.ListarParametros("LG");

        if (dt_parametroTurno.Rows.Count > 0)
        {
            valorMaxGrilla = Convert.ToInt32(dt_parametroTurno.Rows[0].ItemArray.GetValue(4).ToString());
        }
    }

    //private void DeshabilitarFiltros() {

      

    //    if (ddlTipoDocumento.SelectedIndex == 1)
    //    {
    //        ddlEstadoTurno.Enabled = false;
    //        ddlCajero.Enabled = false;
    //        ddlFlgCobro.Enabled = false;
          
    //    }
    //    else {

    //        ddlEstadoTurno.Enabled = true;
    //        ddlCajero.Enabled = true;
    //        ddlFlgCobro.Enabled = true;
            
        
    //    }
    
    //}

    private bool IsFechaValida()
    {
        int iValiFechas;
        if (txtHoraDesde.Text != "" && txtHoraHasta.Text == "")
        {
            string pNewHraDesde = txtHoraDesde.Text;
            string pNewHraHasta = txtHoraHasta.Text;
            pNewHraDesde = "23:59:59";
            pNewHraHasta = "23:59:59";

            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + pNewHraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + pNewHraHasta));
        }
        else
        {
            iValiFechas = DateTime.Compare(Convert.ToDateTime(this.txtDesde.Text + " " + sHoraDesde), Convert.ToDateTime(this.txtHasta.Text + " " + sHoraHasta));
        }

        if (iValiFechas == 1)
        {
            this.lblMensajeError.Text = "Filtro de fecha invalido";
            lblMensajeErrorData.Text = "";
            //this.txtHasta.Text = "";
            grvPaginacionBoarding.Visible = false;
            grvPaginacion.Visible = false;
            return false;
        }
        else
        {
            this.lblMensajeError.Text = "";
            return true;

        }
    }
    #endregion

    
    private string MedioAnulacion() { 
    
    DataTable dt_usuarios = new DataTable();
     string usuarios = "";

     //obtener usuario por rol
     if (sMedioAnulacion == "SUP")
     {
         dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_SUPER]);
     }
     else if (sMedioAnulacion == "ADM")
     {
         dt_usuarios = objConsulta.ListarUsuarioxRol((string)Property.htProperty[Define.ID_ROL_ADMIN]);
     }
 
     if (dt_usuarios.Rows.Count > 0)
     {
         for (int i = 0; i < dt_usuarios.Rows.Count; i++)
         {
             if (i == 0)
             {
                 usuarios = dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
             }
             else
             {
                 usuarios += "," + dt_usuarios.Rows[i]["Cod_Usuario"].ToString();
             }
         }
     }
     else if (sMedioAnulacion == "0")
     {
         usuarios = ""; //quiere decir Todos
     }
     else if (sMedioAnulacion == "AUT")
     {
         if (ddlTipoDocumento.SelectedValue == "T")
         {
             usuarios = (string)Property.htProperty[Define.ID_ROL_AUTO];
         }
         else {
             usuarios = "NONE";
         }   
     }
     else
     {
         usuarios = "";
     }
        return usuarios;
    }


    #region Dynamic data query
    private int GetRowCount()
    {
        int count = 0;
        string usuarios = "";
        if (ddlTipoDocumento.SelectedValue == "T")
        {
            if (ddlEstadoTicket.SelectedValue == "X")
            {
                //obtener usuarios
                usuarios = this.MedioAnulacion();
            }
            else {
                usuarios = "";
            }
            

            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        "0",
                                                                        null,
                                                                        sFlgCobro,
                                                                        sFlgMasiva,
                                                                        sEstadoTurno,
                                                                        sCajero,
                                                                        usuarios,
                                                                        null,
                                                                        0,
                                                                        0, "1");
            if (dt_consulta.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
            return count;
        }
        else
        {
            if (ddlEstadoTicket.SelectedValue == "X")
            {
                //obtener usuarios
                usuarios = this.MedioAnulacion();
            }
            else
            {
                usuarios = "";
            }

            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        "",
                                                                        "",
                                                                        usuarios,
                                                                        null,
                                                                        0,
                                                                        0, "1");

            if (dt_consulta.Columns.Contains("TotRows"))
                count = Convert.ToInt32(dt_consulta.Rows[0]["TotRows"].ToString());
            else
                lblMensajeError.Text = dt_consulta.Rows[0].ItemArray.GetValue(1).ToString();
            return count;
        }
    }

    private DataTable GetDataPage(int pageIndex, int pageSize, string sortExpression)
    {
        //ValidarTamanoGrilla();

        Session["sortExpressionTicketBPXFecha"] = sortExpression;
        string usuarios = "";

        if (ddlTipoDocumento.SelectedValue == "T")
        {

            if (ddlEstadoTicket.SelectedValue == "X")
            {
                //obtener usuarios
                usuarios = this.MedioAnulacion();
                
            }else
            {
                usuarios = "";
            }

            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        "0",
                                                                        null,
                                                                        sFlgCobro,
                                                                        sFlgMasiva,
                                                                        sEstadoTurno,
                                                                        sCajero,
                                                                        usuarios,
                                                                        sortExpression,
                                                                        pageIndex,
                                                                        valorMaxGrilla, "0");

            ViewState["tablaTicket"] = dt_consulta;
        }
        else
        {
            if (ddlEstadoTicket.SelectedValue == "X")
            {
                //obtener usuarios
                usuarios = this.MedioAnulacion();
            }
            else {
                usuarios = "";
            }

            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        "",
                                                                        "",
                                                                        usuarios,
                                                                        sortExpression,
                                                                        pageIndex,
                                                                        valorMaxGrilla, "0");

            ViewState["tablaBoarding"] = dt_consulta;
        }
        return dt_consulta;
    }
    #endregion

    #region grvPaginacion EVENTS
    protected void ddlTipoDoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarEstados();
       // CargarMedioAnulacion();
        //DeshabilitarFiltros();

    }
    #endregion

    #region grvPaginacion TICKET
    protected void grvPaginacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //SaveFiltros();
        RecuperarFiltros();
        grvPaginacion.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvPaginacion_Sorting(object sender, GridViewSortEventArgs e)
    {
        //SaveFiltros();
        RecuperarFiltros();
        BindPagingGrid();
    }

    protected void grvPaginacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowTicket")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tablaTicket"];
            String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Cod_Numero_Ticket"].ToString();
            ConsDetTicket1.Inicio(codTicket);
        }

    }

    protected void grvPaginacion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString().TrimEnd() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado_Actual"].ToString().TrimEnd() == "R")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
            }
        }
    }
    #endregion

    #region grvPaginacion BOARDING
    protected void grvPaginacionBoarding_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //SaveFiltros();
        RecuperarFiltros();
        grvPaginacionBoarding.PageIndex = e.NewPageIndex;
        BindPagingGrid();
    }

    protected void grvPaginacionBoarding_Sorting(object sender, GridViewSortEventArgs e)
    {
        //SaveFiltros();
        RecuperarFiltros();
        BindPagingGrid();
    }

    protected void grvPaginacionBoarding_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowBoarding")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtTicketRehabilitados = (DataTable)ViewState["tablaBoarding"];
            String codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();

            String codSecuenciaBase = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            String codBoarding = String.Empty;
            if (codSecuenciaBase != null && codSecuenciaBase != "" && codSecuenciaBase != "0")
            {
                codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp_Rel"].ToString();
            }
            else
            {
                codTicket = dtTicketRehabilitados.Rows[rowIndex]["Num_Secuencial_Bcbp"].ToString();
            }

            DataTable dt_DetalleBcbp = new DataTable();
            dt_DetalleBcbp = (DataTable)ViewState["tablaBoarding"];
            CnsDetBoarding1.CargarDetalleBoardingHistorico(codTicket);
        }
    }

    protected void grvPaginacionBoarding_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "X")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(253, 234, 234);
            }
            if (((System.Data.DataRowView)(e.Row.DataItem)).Row["Tip_Estado"].ToString().TrimEnd() == "R")
            {
                e.Row.BackColor = System.Drawing.Color.FromArgb(254, 233, 194);
            }
        }
    }
    #endregion

    protected void lbExportar_Click(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment; filename=TicketBPxFecha.xls");
        this.EnableViewState = false;
        Response.Charset = string.Empty;
        System.IO.StringWriter myTextWriter = new System.IO.StringWriter();
        myTextWriter = exportarExcel();
        Response.Write(myTextWriter.ToString());
        Response.End();
    }

    public System.IO.StringWriter exportarExcel()
    {
        DataTable dt_consulta_u = new DataTable();
        string usuarios;
        RecuperarFiltros();

        if (ddlTipoDocumento.SelectedValue == "T")
        {
            if (ddlEstadoTicket.SelectedValue == "X")
            {
                usuarios = this.MedioAnulacion();
            }
            else {
                usuarios = "";
            }
            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        "0",
                                                                        null,
                                                                        sFlgCobro,
                                                                        sFlgMasiva,
                                                                        sEstadoTurno,
                                                                        sCajero,
                                                                        usuarios,
                                                                        null,
                                                                        0,
                                                                        0, "0");

            
        }
        else
        {
            if (ddlEstadoTicket.SelectedValue == "X")
            {
                usuarios = this.MedioAnulacion();
            }
            else
            {
                usuarios = "";
            }
            dt_consulta = objConsulta.ListarTicketxFechaPagin(sTipoDoc,
                                                                        sFechaDesde,
                                                                        sFechaHasta,
                                                                        sHoraDesde,
                                                                        sHoraHasta,
                                                                        sIdCompania,
                                                                        sEstadoTicket,
                                                                        sTipoTicket,
                                                                        sPersona,
                                                                        sVuelo,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        null,
                                                                        "",
                                                                        "",
                                                                        usuarios,
                                                                        null,
                                                                        0,
                                                                        0, "0");
            
        }

       

        System.IO.StringWriter excelDoc;

        excelDoc = new System.IO.StringWriter();
        StringBuilder ExcelXML = new StringBuilder();

        #region CABECERA DEL ARCHIVO
        ExcelXML.Append("<?xml version=\"1.0\"?>\n");
        ExcelXML.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
        ExcelXML.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
        ExcelXML.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n");
        ExcelXML.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n");
        ExcelXML.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
        ExcelXML.Append(" xmlns:html=\"http://www.w3.org/TR/REC-html40\">\n");
        ExcelXML.Append("<DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\">\n");
        ExcelXML.Append("<Author>Katherine Omonte</Author>\n");
        ExcelXML.Append("<LastAuthor>Katherine Omonte</LastAuthor>\n");
        ExcelXML.Append("<Created>2011-01-10T16:34:47Z</Created>\n");
        ExcelXML.Append("<Version>12.00</Version>\n");
        ExcelXML.Append(" </DocumentProperties>\n");
        ExcelXML.Append(" <ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
        ExcelXML.Append("<WindowHeight>7935</WindowHeight>\n");
        ExcelXML.Append("<WindowWidth>20055</WindowWidth>\n");
        ExcelXML.Append("<WindowTopX>240</WindowTopX>\n");
        ExcelXML.Append("<WindowTopY>75</WindowTopY>\n");
        ExcelXML.Append(" <ProtectStructure>False</ProtectStructure>\n");
        ExcelXML.Append("<ProtectWindows>False</ProtectWindows>\n");
        ExcelXML.Append("</ExcelWorkbook>\n");
        ExcelXML.Append(" <Styles>\n");
        ExcelXML.Append("<Style ss:ID=\"s66\">\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
        ExcelXML.Append(" ss:Underline=\"Single\"/>\n");
        ExcelXML.Append("</Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s76\">\n");
        ExcelXML.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:WrapText=\"1\"/>\n");
        ExcelXML.Append("<Borders>\n");
        ExcelXML.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>\n");
        ExcelXML.Append("</Borders>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"\n");
        ExcelXML.Append(" ss:Bold=\"1\" ss:Underline=\"Single\"/>\n");
        ExcelXML.Append("<Interior ss:Color=\"#D8D8D8\" ss:Pattern=\"Solid\"/>\n");
        ExcelXML.Append("</Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s77\">\n");
        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
        ExcelXML.Append("<Borders/>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
        ExcelXML.Append("<Interior ss:Color=\"#F2F2F2\" ss:Pattern=\"Solid\"/>\n");
        ExcelXML.Append("<NumberFormat/>\n");
        ExcelXML.Append("<Protection/>\n");
        ExcelXML.Append(" </Style>\n");
        ExcelXML.Append("<Style ss:ID=\"s78\">\n");
        ExcelXML.Append("<Alignment ss:Vertical=\"Bottom\"/>\n");
        ExcelXML.Append("<Borders/>\n");
        ExcelXML.Append("<Font ss:FontName=\"Arial\" x:Family=\"Swiss\" ss:Size=\"9\" ss:Color=\"#000000\"/>\n");
        ExcelXML.Append("<Interior />\n");
        ExcelXML.Append("<NumberFormat/>\n");
        ExcelXML.Append("<Protection/>\n");
        ExcelXML.Append(" </Style>\n");
        ExcelXML.Append(" </Styles>\n");
        #endregion

        string startExcelXML = ExcelXML.ToString();

        excelDoc.Write(startExcelXML);

        
        if (ddlTipoDocumento.SelectedValue == "T")
        {
            #region Consulta Ticket
            excelDoc.Write("<Worksheet ss:Name=\"CnsTicketBPFecha\">\n");
            excelDoc.Write("<Table ss:ExpandedColumnCount=\"" + dt_consulta.Columns.Count + "\" ss:ExpandedRowCount=\"" + (dt_consulta.Rows.Count + 1) + "\" x:FullColumns=\"1\"\n");
            excelDoc.Write(" x:FullRows=\"1\" ss:DefaultColumnWidth=\"60\" ss:DefaultRowHeight=\"15\">\n");

            #region TAMAÑO DE LAS COLUMNAS
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"150\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"160\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"60\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"70\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"190\"/>\n");
            #endregion

            #region CABECERA
            excelDoc.Write("<Row ss:AutoFitHeight=\"0\" ss:Height=\"30\">\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Ticket</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Secuencial</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Ticket</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Compañía</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Creación</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Emisión</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Uso</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Rehab.</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Estado Turno</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Cajero Emisión</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Estado Actual</Data></Cell>\n");
            excelDoc.Write("</Row>\n");
            #endregion

            //DATA
            int fila = 0;
            string estilo = "";
            int num = 0;

            foreach (DataRow row in dt_consulta.Rows)
            {
                excelDoc.Write("<Row ss:AutoFitHeight=\"0\">");

                if (fila % 2 != 0)
                    estilo = "s78";
                else
                    estilo = "s77";

                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Cod_Numero_Ticket"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Correlativo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Tipo_Ticket"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Compania"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Fch_Creacion"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Fch_Vuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Num_Vuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["FHEmision"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["FHUso"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["FHReh"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["EstadoTurno"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Cta_Usuario"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Campo"] + "</Data></Cell>");
                excelDoc.Write("</Row>");

                fila++;
            }

            excelDoc.Write("</Table>\n");
            excelDoc.Write("</Worksheet>\n");
            #endregion
        }

        else {
            #region Consulta Boarding
            excelDoc.Write("<Worksheet ss:Name=\"CnsTicketBPFecha\">\n");
            excelDoc.Write("<Table>\n");

            #region TAMAÑO DE LAS COLUMNAS
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"60\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"70\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"50\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"150\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"50\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"80\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"65\"/>\n");
            excelDoc.Write("<Column ss:AutoFitWidth=\"0\" ss:Width=\"50\"/>\n");
           
            #endregion

            #region CABECERA
            excelDoc.Write("<Row ss:AutoFitHeight=\"0\" ss:Height=\"30\">\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. SEAE</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Secuencial</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Compañía</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Vuelo</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Nro. Asiento</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Pasajero</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Tipo Ingreso</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Usuario Proceso</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Fch. Creación</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Estado Actual</Data></Cell>\n");
            excelDoc.Write("<Cell ss:StyleID=\"s76\"><Data ss:Type=\"String\">Asociado</Data></Cell>\n");

            excelDoc.Write("</Row>\n");
            #endregion

            //DATA
            int fila = 0;
            string estilo = "";
            int num = 0;
            foreach (DataRow row in dt_consulta.Rows)
            {
                excelDoc.Write("<Row ss:AutoFitHeight=\"0\">");

                if (fila % 2 != 0)
                    estilo = "s78";
                else
                    estilo = "s77";

                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Cod_Numero_Bcbp"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Correlativo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Compania"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Fch_Vuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Num_Vuelo"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Num_Asiento"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Nom_Pasajero"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Tip_Ingreso"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Cta_Usuario"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + (dt_consulta.Rows[fila]["Fch_Creacion"].ToString()) + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + dt_consulta.Rows[fila]["Dsc_Tip_Estado"] + "</Data></Cell>");
                excelDoc.Write("<Cell ss:StyleID=\"" + estilo + "\"><Data ss:Type=\"String\">" + (dt_consulta.Rows[fila]["Flg_Tipo_Bcbp"].ToString() == "0" ? "No" : "Si")  + "</Data></Cell>");
                      

                excelDoc.Write("</Row>");

                fila++;
            }

            excelDoc.Write("</Table>\n");
            excelDoc.Write("</Worksheet>\n");
            #endregion
        }
     
     
        excelDoc.Write("</Workbook>\n");

        return excelDoc;
    }

    protected void txtDesde_TextChanged(object sender, EventArgs e)
    {

    }
}