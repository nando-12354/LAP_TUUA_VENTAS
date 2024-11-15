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
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using System.Collections.Generic;
using LAP.TUUA.ALARMAS;

public partial class Mnt_CrearCompania : System.Web.UI.Page
{

    protected Hashtable htLabels;
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion(Define.CNX_14); //BO_Administracion();
    BO_Operacion objWBOperacion = new BO_Operacion();
    BO_Configuracion objWBConfiguracion = new BO_Configuracion();
    List<ModalidadVenta> objListModalidadVenta = new List<ModalidadVenta>();
    List<ParameGeneral> objListParamGeneralMV = new List<ParameGeneral>();
    UIControles objCargaCombo = new UIControles();
    ArrayList controlMV = new ArrayList();
    Hashtable controlTT = new Hashtable();
    DataTable dt_ValidarCodInt = new DataTable();

    bool flagError;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblNombre.Text = htLabels["mcompania.lblNombre"].ToString();
                this.lblTipoCompañia.Text = htLabels["mcompania.lblTipoCompañia"].ToString();
                this.lblRuc.Text = htLabels["mcompania.lblRuc"].ToString();
                this.lblIATA.Text = htLabels["mcompania.lblIATA"].ToString();
                this.lblOACI.Text = htLabels["mcompania.lblOACI"].ToString();
                this.lblCodigoEspecial.Text = htLabels["mcompania.lblCodigoEspecial"].ToString();
                this.lblSAP.Text = htLabels["mcompania.lblSAP"].ToString();
                this.lblAerolinea.Text = htLabels["mcompania.lblAerolinea"].ToString();
                this.lblRepresentante.Text = htLabels["mcompania.lblRepresentante"].ToString();
                this.lblModalidadVenta.Text = htLabels["mcompania.lblModalidadVenta"].ToString();
                this.btnAceptar.Text = htLabels["mcompania.btnAceptar"].ToString();
                hConfirmacion.Value = htLabels["mcompania.cbeAceptar"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegCrearUsuario;
                Response.Redirect("PaginaError.aspx");
            }
        }

        if (ddlTipoCompañía.SelectedItem.Value.Trim() != "1")
        {
            this.lblAerolinea.Visible = false;
            this.txtAerolinea.Visible = false;
            this.txtCodigoEspecial.Visible = false;
            this.lblCodigoEspecial.Visible = false;
            this.txtIATA.Visible = false;
            this.lblIATA.Visible = false;
            this.txtSAP.Visible = false;
            this.lblSAP.Visible = false;
            this.txtOACI.Visible = false;
            this.lblOACI.Visible = false;
        }
        else
        {
            this.lblAerolinea.Visible = true;
            this.txtAerolinea.Visible = true;
            this.txtCodigoEspecial.Visible = true;
            this.lblCodigoEspecial.Visible = true;
            this.txtIATA.Visible = true;
            this.lblIATA.Visible = true;
            this.txtSAP.Visible = true;
            this.lblSAP.Visible = true;
            this.txtOACI.Visible = true;
            this.lblOACI.Visible = true;
        }

    }

    public Sglobales obtenerGlobales()
    {
        Sglobales mGlobales;

        if (Session["SuperGlobal"] == null)
        {
            mGlobales = new Sglobales();
        }
        else
        {
            mGlobales = (Sglobales)Session["SuperGlobal"];
        }

        return mGlobales;
    }

    public void guardarGlobales(Sglobales mGlobales)
    {
        Session["SuperGlobal"] = mGlobales;
    }

    public void eliminarGlobales()
    {
        Session.Remove("SuperGlobal");
    }


    private void Page_Init(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (!Page.IsPostBack)
        {
            if (objGlobales.mFlagModal != true)
            {
                if (objGlobales.mFlagPosPage != true)
                {
                    limpiar();
                    objGlobales.mRRegistrados = new Hashtable();
                    objGlobales.mMVRegistrados = new Hashtable();
                    objGlobales.mMVEliminados = new Hashtable();
                    objGlobales.mCListaControles = new ArrayList();
                    objGlobales.mValoresActuales = new Hashtable();
                    objGlobales.mMjeError = "";
                    objGlobales.mTxtNombre = "";
                    objGlobales.mTxtIATA = "";
                    objGlobales.mTxtOACI = "";
                    objGlobales.mTxtRuc = "";
                    objGlobales.mTxtCodigoEspecial = "";
                    objGlobales.mTxtAerolinea = "";
                    objGlobales.mTxtSAP = "";
                    objGlobales.mIndxTipoCompania = -1;
                    objGlobales.mMjeErrorRepres = "";

                    this.txtNombre.Focus();
                }
            }
            CargarCombos();
        }

        if (objGlobales.mMjeError != "")
        {
            lblMensajeError.Text = objGlobales.mMjeError;
        }

        if (objGlobales.mMjeErrorRepres != "")
        {
            this.lblMensajeErrorRepres.Text = objGlobales.mMjeErrorRepres;
        }

        if (objGlobales.mTxtNombre != "")
        {
            txtNombre.Text = objGlobales.mTxtNombre;
        }
        if (objGlobales.mIndxTipoCompania != -1)
        {
            this.ddlTipoCompañía.SelectedIndex = objGlobales.mIndxTipoCompania;
        }

        if (objGlobales.mTxtIATA != "")
        {
            txtIATA.Text = objGlobales.mTxtIATA;
        }

        if (objGlobales.mTxtOACI != "")
        {
            txtOACI.Text = objGlobales.mTxtOACI;
        }
        if (objGlobales.mTxtRuc != "")
        {
            txtRuc.Text = objGlobales.mTxtRuc;
        }
        if (objGlobales.mTxtCodigoEspecial != "")
        {
            txtCodigoEspecial.Text = objGlobales.mTxtCodigoEspecial;
        }
        if (objGlobales.mTxtAerolinea != "")
        {
            txtAerolinea.Text = objGlobales.mTxtAerolinea;
        }
        if (objGlobales.mTxtSAP != "")
        {
            txtSAP.Text = objGlobales.mTxtSAP;
        }


        if (objGlobales.mRRegistrados.Count > 0)
        {
            this.pnlPanelRepresentante.Visible = true;
            DataTable dtRepresentante = new DataTable();
            dtRepresentante = ActualizarGrilla();
            InicializarGrilla(dtRepresentante);
            gvwRepresentante.DataSource = dtRepresentante;
            gvwRepresentante.DataBind();
            objGlobales.mFlagPosPage = false;
            objGlobales.mFlagModal = false;
        }
        else
        {
            this.pnlPanelRepresentante.Visible = false;

        }
        objListParamGeneralMV = objWBAdministracion.listarAtributosGenerales();
        objListModalidadVenta = objWBAdministracion.listarModalidadVenta();

        objGlobales.mCListaControles = ControlesPermisos(objListModalidadVenta);


        WireUpEvents(objGlobales.mCListaControles);
        Table TableModVenta = new Table();
        BuildHtmlTableWithControls(objGlobales.mCListaControles, TableModVenta);
        this.pnlModalidadVenta.Controls.Clear();
        this.pnlModalidadVenta.Controls.Add(TableModVenta);
        this.pnlModalidadVenta.Controls.Add(new LiteralControl("<br />"));
        this.pnlModalidadVenta.Controls.Add(new LiteralControl("<br />"));

        guardarGlobales(objGlobales); 

    }

    private ArrayList ControlesPermisos(List<ModalidadVenta> objListModalidadVenta)
    {

        Sglobales objGlobales = obtenerGlobales();

        ArrayList controllist = new ArrayList();

        int contTableRows = objListModalidadVenta.Count;

        for (int i = 0; i <= contTableRows - 1; i++)
        {
            ModalidadVenta objModalidadVenta = new ModalidadVenta();
            objModalidadVenta = (ModalidadVenta)objListModalidadVenta[i];

            //if (objModalidadVenta.STipModalidad == "1")
            //{

                //row3
                MyLabel lblI = new MyLabel(); //initialize new label
                lblI.ID = UI.LabelPrefix + "I" + i.ToString(); //set ID property 
                lblI.Text = objModalidadVenta.SCodModalidadVenta; //set text property for label
                lblI.Visible = false;
                controllist.Add(lblI); //add label to 

                ChkBox chkI = new ChkBox(); //initlaize new checkbox
                chkI.ID = UI.CheckBoxPrefix + "I" + i.ToString() + "_" + objModalidadVenta.SCodModalidadVenta; ; //set ID property 
                chkI.Width = 60;  // set width property
                chkI.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);

                if (objGlobales.mValoresActuales.Count > 0)
                {
                    object[] keys = new object[objGlobales.mValoresActuales.Keys.Count];
                    objGlobales.mValoresActuales.Keys.CopyTo(keys, 0);

                    for (int j = 0; j < keys.Length; j++)
                    {
                        if (keys[j].ToString() == objModalidadVenta.SCodModalidadVenta)
                        {
                            if ((string)objGlobales.mValoresActuales[keys[j].ToString()] == "1")
                            {
                                chkI.Checked = true;
                            }
                        }
                    }
                }

                controllist.Add(chkI); // add textbox to cell

                //row3
                MyLabel lblII = new MyLabel(); //initialize new label
                lblII.ID = UI.LabelPrefix + "II" + i.ToString(); //set ID property 
                lblII.Text = objModalidadVenta.SNomModalidad; //set text property for label
                controllist.Add(lblII); //add label to 

                ImageButton imgbtn = new ImageButton();
                imgbtn.ID = "btn_" + objModalidadVenta.SCodModalidadVenta;
                imgbtn.CausesValidation = false;
                imgbtn.Click += new ImageClickEventHandler(this.btnAgregarTT_Click);
                imgbtn.ImageUrl = "Imagenes/btn_edit.gif";
                controllist.Add(imgbtn); //add label to 

            //}
        }
        guardarGlobales(objGlobales);
        return controllist;

    }

    protected void btnAgregarTT_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ctrl = (ImageButton)sender;
        string[] lista;
        lista = new string[2];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        CompDetalle1.CargarFormulario(Identificador, "");
    }


    private DataTable ActualizarGrilla()
    {
        Sglobales objGlobales = obtenerGlobales();

        DataTable dtRepresentante = new DataTable();
        DataRow drRepresentante;

        dtRepresentante.Columns.Add(new DataColumn("Nro", System.Type.GetType("System.String")));
        dtRepresentante.Columns.Add(new DataColumn("Nombres", System.Type.GetType("System.String")));
        dtRepresentante.Columns.Add(new DataColumn("Cargo", System.Type.GetType("System.String")));
        dtRepresentante.Columns.Add(new DataColumn("Tip.Docum.", System.Type.GetType("System.String")));
        dtRepresentante.Columns.Add(new DataColumn("Num.Docum.", System.Type.GetType("System.String")));
        dtRepresentante.Columns.Add(new DataColumn("Estado", System.Type.GetType("System.String")));

        List<ListaDeCampo> objListListadeCampo = new List<ListaDeCampo>();

        objListListadeCampo = objWBAdministracion.obtenerListadeCampo("PermRepresentante");

        for (int i = 0; i < objListListadeCampo.Count; i++)
        {
            dtRepresentante.Columns.Add(new DataColumn(objListListadeCampo[i].SDscCampo, System.Type.GetType("System.String")));
        }


        object[] keys = new object[objGlobales.mRRegistrados.Keys.Count];
        objGlobales.mRRegistrados.Keys.CopyTo(keys, 0);


        for (int i = 0; i < keys.Length; i++)
        {
            RepresentantCia objRepresentantCia = new RepresentantCia();
            objRepresentantCia = (RepresentantCia)objGlobales.mRRegistrados[keys[i]];
            drRepresentante = dtRepresentante.NewRow();
            drRepresentante["Nro"] = i + 1;
            drRepresentante["Nombres"] = objRepresentantCia.SNomRepresentante + " " + objRepresentantCia.SApeRepresentante;
            drRepresentante["Cargo"] = objRepresentantCia.SCargoRepresentante;
            drRepresentante["Tip.Docum."] = objRepresentantCia.STDocRepresentante;
            drRepresentante["Num.Docum."] = objRepresentantCia.SNDocRepresentante;
            string sEstado = ObtenerEstado(objRepresentantCia.STipEstado);
            drRepresentante["Estado"] = sEstado;

            string valor;
            for (int j = 0; j < objListListadeCampo.Count; j++)
            {
                if (Convert.ToInt32(objRepresentantCia.SPermRepresentante) == 0)
                {
                    valor = "0";
                }
                else
                {
                    string conversion = DecimalToBase(Convert.ToInt32(objRepresentantCia.SPermRepresentante), 2);
                    conversion = conversion.PadLeft(objListListadeCampo.Count, '0');
                    valor = conversion.Substring(j, 1);
                }

                if (valor == "1")
                    drRepresentante[objListListadeCampo[j].SDscCampo] = "Si";
                else
                    drRepresentante[objListListadeCampo[j].SDscCampo] = "No";
            }

            dtRepresentante.Rows.Add(drRepresentante);
        }
        guardarGlobales(objGlobales);
        return dtRepresentante;
    }

    protected string ObtenerEstado(string CodCampo)
    {
        List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();
        objListListaDeCampo = objWBAdministracion.obtenerListadeCampo("EstadoRegistro");

        for (int i = 0; i < objListListaDeCampo.Count; i++)
        {
            if (objListListaDeCampo[i].SCodCampo == CodCampo)
            {
                return objListListaDeCampo[i].SDscCampo;
            }
        }
        return "";
    }


    string DecimalToBase(int iDec, int numbase)
    {
        const int base10 = 10;
        char[] cHexa = new char[] { 'A', 'B', 'C', 'D', 'E', 'F' };

        string strBin = "";
        int[] result = new int[32];
        int MaxBit = 32;
        for (; iDec > 0; iDec /= numbase)
        {
            int rem = iDec % numbase;
            result[--MaxBit] = rem;
        }
        for (int i = 0; i < result.Length; i++)
            if ((int)result.GetValue(i) >= base10)
                strBin += cHexa[(int)result.GetValue(i) % base10];
            else
                strBin += result.GetValue(i);
        strBin = strBin.TrimStart(new char[] { '0' });
        return strBin;
    }


    void InicializarGrilla(DataTable dataRepresentante)
    {
        ViewState["tablaRepresentante"] = dataRepresentante;
        foreach (DataColumn col in dataRepresentante.Columns)
        {
            BoundField bf1 = new BoundField();

            bf1.HeaderText = col.ColumnName;
            bf1.DataField = col.ColumnName;
            bf1.SortExpression = col.ColumnName;
            gvwRepresentante.Columns.Add(bf1);
        }
    }


    private void limpiar()
    {
        this.txtNombre.Text = "";
        this.txtIATA.Text = "";
        this.txtOACI.Text = "";
        this.txtRuc.Text = "";
        this.txtCodigoEspecial.Text = "";
        this.txtSAP.Text = "";
        this.txtAerolinea.Text = "";
    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo TipoCompania
            DataTable dt_TipoCompania = new DataTable();
            dt_TipoCompania = objWBConsultas.ListaCamposxNombre("TipoCompania");
            objCargaCombo.LlenarCombo(this.ddlTipoCompañía, dt_TipoCompania, "Cod_Campo", "Dsc_Campo", false, false);
        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

    }
    protected void btnAddRepresentante_Click(object sender, ImageClickEventArgs e)
    {
        string parametro = "AD";
        RepresDetalle1.CargarFormulario(parametro);
    }

    protected void gvwRepresentante_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ShowRepresentante")
        {
            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtRepresentante = (DataTable)ViewState["tablaRepresentante"];
            String codigo = dtRepresentante.Rows[rowIndex]["Nro"].ToString();
            this.RepresDetalle1.CargarFormulario(codigo);
        }
    }



    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        objGlobales.mFlagPosPage = true;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
        guardarGlobales(objGlobales); 
    }

    #region Build Html Table With Controls
    private void BuildHtmlTableWithControls(ArrayList controlList, Table Tabla)
    {
        Sglobales objGlobales = obtenerGlobales();
        TableRow trow;
        TableCell tcell;
        int cols = 4;

        try
        {
            trow = new TableRow();

            Tabla.Rows.Add(trow);

            for (int i = 0; i < controlList.Count; i++)
            {
                tcell = new TableCell();

                UI.AddControlToCell(tcell, controlList, i);

                trow.Cells.Add(tcell);

                if ((i + 1) % cols == 0)
                {
                    Tabla.Rows.Add(trow);
                    trow = new TableRow();
                }
            }
            objGlobales.mFlagPosPage = false;
            objGlobales.mFlagModal = false;
            guardarGlobales(objGlobales);
        }

        catch (Exception) { throw; }
    }
    #endregion

    #region WireUpEvents
    private void WireUpEvents(ArrayList controlList)
    {

        TxtBox txt;
        DropDown dd;
        ChkBox chk;

        for (int i = 0; i < controlList.Count; i++)
        {
            switch (controlList[i].GetType().FullName)
            {

                case "TxtBox":

                    txt = (TxtBox)controlList[i];
                    txt.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
                    break;

                case "DropDown":

                    dd = (DropDown)controlList[i];
                    dd.SelectedIndexChanged += new System.EventHandler(this.DropDownList_SelectedIndexChanged);
                    break;

                case "ChkBox":

                    chk = (ChkBox)controlList[i];
                    chk.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
                    break;

                default:
                    break;

            }
        }

    }


    private void TextBox_TextChanged(object sender, System.EventArgs e)
    {
        TxtBox ctrl = (TxtBox)sender;
    }

    private void DropDownList_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        DropDown ctrl = (DropDown)sender;

    }

    private void CheckBox_CheckedChanged(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        ChkBox ctrl = (ChkBox)sender;
        string[] lista;
        lista = new string[2];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string valor;
        if (ctrl.Checked == true)
        {
            valor = "1";
        }
        else
        {
            valor = "0";
        }
        if (objGlobales.mValoresActuales[Identificador] != null)
        {
            objGlobales.mValoresActuales[Identificador] = valor;
        }
        else
        {
            objGlobales.mValoresActuales.Add(Identificador, valor);
        }

        guardarGlobales(objGlobales);
    }
    #endregion

    private Int64 idTxCritica = -1;

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        try
        {
            if (validaCompañia() == true)
            {              
                objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], 
                    (string)Session["Cod_SubModulo"], Define.CNX_14);
                if (objWBAdministracion.obtenerCompañiaxnombre(this.txtNombre.Text) == null)
                {
                    if (txtSAP.Text.Trim() != "")
                    {
                        txtSAP.Text = poblarCeros(txtSAP.Text, 10);
                    }

                    if (txtCodigoEspecial.Text.Trim() != "")
                    {
                        txtCodigoEspecial.Text = poblarCeros(txtCodigoEspecial.Text, 10);
                    }

                    Compania objCompania = new Compania("", this.ddlTipoCompañía.SelectedValue, this.txtNombre.Text.Trim().ToUpper(), DateTime.Today, "", this.txtAerolinea.Text.Trim().ToUpper(),
                    this.txtRuc.Text.Trim(), this.txtCodigoEspecial.Text.Trim(), this.txtSAP.Text.Trim(), this.txtOACI.Text.Trim().ToUpper(), this.txtIATA.Text.Trim().ToUpper(), Convert.ToString(Session["Cod_Usuario"]), "", "");

                    if (ddlTipoCompañía.SelectedItem.Value.Trim() == "1")
                    {

                        dt_ValidarCodInt = objWBConsultas.ConsultaCompaniaxFiltro("0", "0", "", "ASC");
                        DataRow[] countRegCodUnico;
                        countRegCodUnico = dt_ValidarCodInt.Select("Cod_Especial_Compania='" + txtAerolinea.Text + "'");
                        int NumTotCodUnico = countRegCodUnico.Length;

                        DataRow[] countRegCodIATA;
                        countRegCodIATA = dt_ValidarCodInt.Select("Cod_IATA='" + txtIATA.Text + "'");
                        int NumTotCodIATA = countRegCodIATA.Length;

                        DataRow[] countRegCodAerolinea;
                        countRegCodAerolinea = dt_ValidarCodInt.Select("Cod_Aerolinea='" + txtCodigoEspecial.Text + "'");
                        int NumTotCodAerolinea = countRegCodAerolinea.Length;

                        DataRow[] countRegCodSAP;
                        int NumTotCodSAP = 0;
                        if (txtSAP.Text.Trim() != "")
                        {
                            countRegCodSAP = dt_ValidarCodInt.Select("Cod_SAP='" + txtSAP.Text + "'");
                            NumTotCodSAP = countRegCodSAP.Length;
                        }

                        DataRow[] countRegCodOACI;
                        int NumTotCodOACI = 0;
                        if (txtOACI.Text.Trim() != "")
                        {
                            countRegCodOACI = dt_ValidarCodInt.Select("Cod_OACI='" + txtOACI.Text + "'");
                            NumTotCodOACI = countRegCodOACI.Length;
                        }

                        if (NumTotCodOACI == 0)
                        {
                            if (NumTotCodAerolinea == 0)
                            {
                                if (NumTotCodSAP == 0)
                                {
                                    if (NumTotCodUnico == 0)
                                    {
                                        if (NumTotCodIATA == 0)
                                        {
                                            idTxCritica = objWBAdministracion.obtenerIdTransaccionCritica();
                                            objCompania.IdTxCritica = idTxCritica;

                                            if (objWBAdministracion.insertarCompaniaCrit(objCompania) == true)
                                            {
                                                string strCodigoCompania = objWBAdministracion.obtenerCompañiaxnombre(this.txtNombre.Text).SCodCompania;

                                                if (RegistrarRepresentantes(strCodigoCompania) == true)
                                                {
                                                    if (RegistrarModalidadVenta(strCodigoCompania) == true)
                                                    {
                                                        if (RegistrarAtributos(strCodigoCompania) == true)
                                                        {
                                                            string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));
                                                            string IpClient = Request.UserHostAddress;
                                                            string strMessageA = "Compañía registrada correctamente";
                                                            GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000085", "003",
                                                                IpClient, "1", "Alerta W0000085", "<br/>" + strMessageA + "<br/> Usuario: " + Convert.ToString(Session["Cod_Usuario"]),
                                                                Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "Compania", pathMap);
                                                            
                                                            //eliminarGlobales();
                                                            omb.ShowMessage("Compañía registrada correctamente", "Creacion de Compañía", "Mnt_VerCompania.aspx");
                                                        }
                                                    }
                                                }                                                
                                            }
                                        }
                                        else
                                        {
                                            lblMensajeError.Text = "El código IATA ya existe";
                                        }
                                    }
                                    else
                                    {
                                        lblMensajeError.Text = "El código de Aerolinea ya existe";
                                    }
                                }
                                else
                                {
                                    lblMensajeError.Text = "El código SAP ya existe";
                                }
                            }
                            else
                            {
                                lblMensajeError.Text = "El código interno ya existe";
                            }
                        }
                        else
                        {
                            lblMensajeError.Text = "El código de OACI ya existe";
                        }
                    }
                    else
                    {
                        if (objWBAdministracion.insertarCompania(objCompania) == true)
                        {
                            string strCodigoCompania = objWBAdministracion.obtenerCompañiaxnombre(this.txtNombre.Text).SCodCompania;

                            if (RegistrarRepresentantes(strCodigoCompania) == true)
                            {
                                if (RegistrarModalidadVenta(strCodigoCompania) == true)
                                {
                                    if (RegistrarAtributos(strCodigoCompania) == true)
                                    {
                                        //eliminarGlobales();
                                        omb.ShowMessage("Compañía registrada correctamente", "Creacion de Compañía", "Mnt_VerCompania.aspx");
                                    }
                                }
                            }                           
                        }
                    }

                }
                else
                {
                    this.lblMensajeError.Text = "El Nombre de Compañía ya se encuentra registrado, verifique por favor";
                    objGlobales.mMjeError = "El Nombre de Compañía ya se encuentra registrado, verifique por favor";
                    guardarGlobales(objGlobales);
                }
            }
        }
        catch (Exception ex)
        {
            flagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            
            if (flagError)
                Response.Redirect("PaginaError.aspx");

        }
    }

    private string getPathMap(SiteMapNode oSMN)
    {
        string pathMap = string.Empty;

        if (oSMN.ParentNode == null)
        {
            pathMap = oSMN.Title;
        }
        else
        {
            pathMap = getPathMap(oSMN.ParentNode) + " : " + oSMN.Title;
        }
        return pathMap;
    }

    protected bool validaCompañia()
    {
        Sglobales objGlobales = obtenerGlobales();

        if (txtRuc.Text != "")
        {
            if (this.txtRuc.Text.Length != Define.DigitosRuc)
            {
                this.lblMensajeError.Text = "El Ruc debe tener " + Define.DigitosRuc + " Dígitos";
                objGlobales.mMjeError = "El Ruc debe tener " + Define.DigitosRuc + " Dígitos";
                guardarGlobales(objGlobales);
                return false;
            }

            if (objWBAdministracion.validarRucCompañia(this.txtRuc.Text.Trim()) == false)
            {
                this.lblMensajeError.Text = "El Nº de Ruc ya se encuentra Registrado, verifique por favor";
                objGlobales.mMjeError = "El Nº de Ruc ya se encuentra Registrado, verifique por favor";
                guardarGlobales(objGlobales);
                return false;
            }

        }
        
        if (txtNombre.Text.Trim() == "")
        {
            this.lblMensajeError.Text = "Ingrese el nombre de la Compañia";
            return false;
        }

        if (ddlTipoCompañía.SelectedItem.Value.Trim() == "1")
        {
            if (txtIATA.Text.Trim() == "")
            {
                this.lblMensajeError.Text = "Ingrese el codigo IATA";
                return false;
            }

            if (txtCodigoEspecial.Text.Trim() == "")
            {
                this.lblMensajeError.Text = "Ingrese el Codigo Interno";
                return false;
            }

            if (txtAerolinea.Text.Trim() == "")
            {
                this.lblMensajeError.Text = "Ingrese el Codigo Aerolinea";
                return false;
            }
        }

        this.lblMensajeError.Text = "";
        objGlobales.mMjeError = "";
        guardarGlobales(objGlobales); 
        return true;
    }


    protected bool RegistrarRepresentantes(string sCompañia)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mRRegistrados.Count > 0)
        {
            object[] keys = new object[objGlobales.mRRegistrados.Keys.Count];
            objGlobales.mRRegistrados.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                RepresentantCia objRepresentantCia = new RepresentantCia();
                objRepresentantCia = (RepresentantCia)objGlobales.mRRegistrados[keys[i]];
                objRepresentantCia.SCodCompania = sCompañia;
                objRepresentantCia.IdTxCritica = idTxCritica;
                if (objWBAdministracion.insertarRepresentanteCrit(objRepresentantCia) == false)
                {
                    return false;
                }
            }

        }

        guardarGlobales(objGlobales);
        return true;
    }

    protected bool RegistrarModalidadVenta(string strCodigoCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mValoresActuales.Count > 0)
        {
            object[] keys = new object[objGlobales.mValoresActuales.Keys.Count];
            objGlobales.mValoresActuales.Keys.CopyTo(keys, 0);

            for (int j = 0; j < keys.Length; j++)
            {
                if ((string)objGlobales.mValoresActuales[keys[j].ToString()] == "1")
                {
                    ModVentaComp objModVentaComp = new ModVentaComp(strCodigoCompania, keys[j].ToString(), "");
                    objModVentaComp.IdTxCritica = idTxCritica;
                    if (objWBAdministracion.insertarModVentaCompCrit(objModVentaComp) == false)
                    {
                        guardarGlobales(objGlobales);
                        return false;
                    }
                    //INSERTAR SECUENCIA MODALIDAD COMPAÑIA PARA BCBP Y VENTA MASIVA DE CREDITO
                    if (keys[j].ToString() == "M0002" || keys[j].ToString() == "M0004")
                    {
                        if (!objWBAdministracion.insertarSecuenciaModVentaComp(keys[j].ToString()+"-"+strCodigoCompania))
                        {
                            guardarGlobales(objGlobales);
                            return false;
                        }
                    }

                }

            }
        }

        guardarGlobales(objGlobales);
        return true;
    }


    protected bool RegistrarAtributos(string strCodigoCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mMVRegistrados.Count > 0)
        {
            object[] keys = new object[objGlobales.mMVRegistrados.Keys.Count];
            objGlobales.mMVRegistrados.Keys.CopyTo(keys, 0);


            for (int i = 0; i < keys.Length; i++)
            {
                string[] lista;
                string sModalidadVenta = keys[i].ToString();

                if (validaModalidaVenta(sModalidadVenta) == true)
                {
                    ArrayList listaMV = new ArrayList();
                    listaMV = (ArrayList)objGlobales.mMVRegistrados[sModalidadVenta];
                    for (int j = 0; j < listaMV.Count; j++)
                    {
                        lista = new string[2];
                        lista = Convert.ToString(listaMV[j]).Split('|');
                        string Identificador = Convert.ToString(lista[0]);
                        string valor = Convert.ToString(lista[1]);

                        ParameGeneral objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                        if (objParameGeneral.STipoParametro == "L")
                        {
                            valor = ValueListaCampo(Convert.ToInt32(valor), objParameGeneral.SCampoLista);
                        }

                        ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr(sModalidadVenta, valor, "", Convert.ToString(Session["Cod_Usuario"]), "", strCodigoCompania, Identificador, "");
                        objModVentaCompAtr.IdTxCritica = idTxCritica;
                        if (objWBAdministracion.insertarModVentaCompAtrCrit(objModVentaCompAtr) == false)
                        {
                            guardarGlobales(objGlobales); 
                            return false;
                        }

                    }
                }
            }

        }

        guardarGlobales(objGlobales); 
        return true;
    }

    protected bool validaModalidaVenta(string sModalidadVenta)
    {
        bool encontrado = false;
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mValoresActuales.Count > 0)
        {
            object[] keys = new object[objGlobales.mValoresActuales.Keys.Count];
            objGlobales.mValoresActuales.Keys.CopyTo(keys, 0);

            for (int j = 0; j < keys.Length; j++)
            {
                if (keys[j].ToString() == sModalidadVenta)
                {
                    encontrado = true;
                }

            }
        }
        guardarGlobales(objGlobales); 
        return encontrado;
    }


    protected string ValueListaCampo(int IndexCampo, string CampoLista)
    {
        List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();
        objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(CampoLista);

        return objListListaDeCampo[IndexCampo].SCodCampo;
    }
    protected void ddlTipoCompañía_SelectedIndexChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        DropDownList ctrl = (DropDownList)sender;
        objGlobales.mIndxTipoCompania = ctrl.SelectedIndex;
        if (ddlTipoCompañía.SelectedItem.Value.Trim() != "1")
        {
            this.lblAerolinea.Visible = false;
            this.txtAerolinea.Visible = false;
            this.txtCodigoEspecial.Visible = false;
            this.lblCodigoEspecial.Visible = false;
            this.txtIATA.Visible = false;
            this.lblIATA.Visible = false;
            this.txtSAP.Visible = false;
            this.lblSAP.Visible = false;
            this.txtOACI.Visible = false;
            this.lblOACI.Visible = false;

            this.txtCodigoEspecial.Text = "";
            this.txtIATA.Text = "";
            this.txtSAP.Text = "";
            this.txtOACI.Text = "";
        }
        else
        {
            this.lblAerolinea.Visible = true;
            this.txtAerolinea.Visible = true;
            this.txtCodigoEspecial.Visible = true;
            this.lblCodigoEspecial.Visible = true;
            this.txtIATA.Visible = true;
            this.lblIATA.Visible = true;
            this.txtSAP.Visible = true;
            this.lblSAP.Visible = true;
            this.txtOACI.Visible = true;
            this.lblOACI.Visible = true;

        }
        guardarGlobales(objGlobales);
    }
    protected void txtNombre_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtNombre = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtIATA_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtIATA = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtOACI_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtOACI = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtSAP_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtSAP = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtAerolinea_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtAerolinea = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtCodigoEspecial_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtCodigoEspecial = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void txtRuc_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        TextBox ctrl = (TextBox)sender;
        objGlobales.mTxtRuc = ctrl.Text;
        guardarGlobales(objGlobales);
    }

    public string poblarCeros(string valor, int tamanio)
    {
        while (tamanio > valor.Length)
        {
            valor = "0" + valor;
        }

        return valor;
    }


}


