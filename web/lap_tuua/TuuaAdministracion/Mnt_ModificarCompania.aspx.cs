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
using LAP.TUUA.ALARMAS;
using System.Collections.Generic;
using System.Threading;

public partial class Mnt_ModificarCompania : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion(Define.CNX_14);
    BO_Operacion objWBOperacion = new BO_Operacion();
    BO_Configuracion objWBConfiguracion = new BO_Configuracion();
    List<ModalidadVenta> objListModalidadVenta = new List<ModalidadVenta>();
    List<ParameGeneral> objListParamGeneralMV = new List<ParameGeneral>();
    UIControles objCargaCombo = new UIControles();
    ArrayList controlMV = new ArrayList();
    Hashtable controlTT = new Hashtable();
    DataTable dt_ValidarCodInt = new DataTable();
    
    bool flagError;

    
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

    protected void Page_Load(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblCodigo.Text = htLabels["mcompania.lblCodigo"].ToString();
                this.lblNombre.Text = htLabels["mcompania.lblNombre"].ToString();
                this.lblTipoCompañia.Text = htLabels["mcompania.lblTipoCompañia"].ToString();
                this.lblRuc.Text = htLabels["mcompania.lblRuc"].ToString();
                this.lblIATA.Text = htLabels["mcompania.lblIATA"].ToString();
                this.lblOACI.Text = htLabels["mcompania.lblOACI"].ToString();
                this.lblCodigoEspecial.Text = htLabels["mcompania.lblCodigoEspecial"].ToString();
                this.lblSAP.Text = htLabels["mcompania.lblSAP"].ToString();
                this.lblAerolinea.Text = htLabels["mcompania.lblAerolinea"].ToString();
                this.lblRepresentante.Text = htLabels["mcompania.lblRepresentante"].ToString();
                this.lblEstado.Text = htLabels["mcompania.lblEstado"].ToString();
                this.lblModalidadVenta.Text = htLabels["mcompania.lblModalidadVenta"].ToString();
                this.btnAceptar.Text = htLabels["mcompania.btnActualizar"].ToString();
                hConfirmacion.Value = htLabels["mcompania.cbeActualizar"].ToString();

                string tipoCompania = this.ddlTipoCompañía.SelectedItem.Text;
                //deshabilitamos el tipo de venta ATM en caso sea diferente de Banco
                for (int i = 0; i < objGlobales.mCListaControles.Count; i++)
                {
                    string valor = objGlobales.mCListaControles[i].ToString();
                    if (valor.Equals("ChkBox"))
                    {
                        ChkBox chkTipoVenta = (ChkBox)objGlobales.mCListaControles[i];
                        string codigo = chkTipoVenta.ID;
                        bool tipo = codigo.EndsWith("M0005");

                        if (tipo)
                        {
                            if (tipoCompania.Equals("BANCO"))
                            {
                                chkTipoVenta.Enabled = true;
                            }
                            else
                            {
                                chkTipoVenta.Enabled = false;
                            }
                        }

                    }
                    if (valor.EndsWith("ImageButton"))
                    {
                        ImageButton imgTipoVenta = (ImageButton)objGlobales.mCListaControles[i];
                        string codigo = imgTipoVenta.ID;
                        bool tipo = codigo.EndsWith("M0005");

                        if (tipo)
                        {
                            if (tipoCompania.Equals("BANCO"))
                            {
                                imgTipoVenta.Enabled = true;
                                imgTipoVenta.Visible = true;
                            }
                            else
                            {
                                imgTipoVenta.Enabled = false;
                                imgTipoVenta.Visible = false;
                            }
                        }

                    }

                }
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
            this.txtOACI.Visible = false;
            this.lblOACI.Visible = false;
            this.txtSAP.Visible = false;
            this.lblSAP.Visible = false;
            
            objGlobales.mTxtAerolinea = "";
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
        }

        guardarGlobales(objGlobales);

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
                    objGlobales.mRRegistrados = new Hashtable();
                    objGlobales.mMVRegistrados = new Hashtable();
                    objGlobales.mMVEliminados = new Hashtable();
                    objGlobales.mCListaControles = new ArrayList();
                    objGlobales.mValoresActuales = new Hashtable();
                    objGlobales.mMjeError = "";
                    objGlobales.mMjeErrorRepres = "";
                    objGlobales.mTxtNombre = "";
                    objGlobales.mTxtIATA = "";
                    objGlobales.mTxtOACI = "";
                    objGlobales.mTxtRuc = "";
                    objGlobales.mTxtCodigoEspecial = "";
                    objGlobales.mTxtAerolinea = "";
                    objGlobales.mTxtSAP = "";
                    objGlobales.mIndxTipoCompania = -1;
                    objGlobales.mIndxEstadoCompania = -1;
                    CargarCombos();
                    PoblarCompania(Convert.ToString(Request.QueryString["Cod_Compania"]));
                    PoblarRepresentante(Convert.ToString(Request.QueryString["Cod_Compania"]));
                    PoblarModalidadVenta(Convert.ToString(Request.QueryString["Cod_Compania"]));
                    PoblarAtributos(Convert.ToString(Request.QueryString["Cod_Compania"]));

                    this.txtNombre.Focus();
                }
            }
            CargarCombos();
           
            //this.ddlTipoCompañía.Enabled = false; 
        }

        if (objGlobales.mMjeError != "")
        {
            lblMensajeError.Text = objGlobales.mMjeError.Trim();
        }

        if (objGlobales.mMjeErrorRepres != "")
        {
            this.lblMensajeErrorRepres.Text = objGlobales.mMjeErrorRepres;
        }

        if (objGlobales.mTxtCodCompania != "")
        {
            txtCodigo.Text = objGlobales.mTxtCodCompania.Trim();
        }

        if (objGlobales.mTxtNombre != "")
        {
            txtNombre.Text = objGlobales.mTxtNombre.Trim();
        }
        if (objGlobales.mIndxTipoCompania != -1)
        {
            this.ddlTipoCompañía.SelectedIndex = objGlobales.mIndxTipoCompania;
        }

        if (objGlobales.mIndxEstadoCompania != -1)
        {
            this.ddlEstado.SelectedIndex = objGlobales.mIndxEstadoCompania;
        }

        if (objGlobales.mTxtIATA != "")
        {
            txtIATA.Text = objGlobales.mTxtIATA.Trim();
        }

        if (objGlobales.mTxtOACI != "")
        {
            txtOACI.Text = objGlobales.mTxtOACI.Trim();
        }
        if (objGlobales.mTxtRuc != "")
        {
            txtRuc.Text = objGlobales.mTxtRuc.Trim();
        }
        if (objGlobales.mTxtCodigoEspecial != "")
        {
            txtCodigoEspecial.Text = objGlobales.mTxtCodigoEspecial.Trim();
        }
        if (objGlobales.mTxtAerolinea != "")
        {
            txtAerolinea.Text = objGlobales.mTxtAerolinea.Trim();
        }
        if (objGlobales.mTxtSAP != "")
        {
            txtSAP.Text = objGlobales.mTxtSAP.Trim();
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
        //TableModVenta.CssClass = "TablaModalModVenta";
        BuildHtmlTableWithControls(objGlobales.mCListaControles, TableModVenta);
        this.pnlModalidadVenta.Controls.Clear();
        this.pnlModalidadVenta.Controls.Add(TableModVenta);
        this.pnlModalidadVenta.Controls.Add(new LiteralControl("<br />"));
        this.pnlModalidadVenta.Controls.Add(new LiteralControl("<br />"));

        guardarGlobales(objGlobales);

        

    }

    protected void PoblarCompania(string sCodCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        Compania objCompania = new Compania();

        objCompania = objWBAdministracion.obtenerCompañiaxcodigo(sCodCompania);
        Session["objCompania"] = objCompania;
        string stmp = objCompania.SCodEspecialCompania;
        objCompania.SCodEspecialCompania = objCompania.SCodAerolinea;
        objCompania.SCodAerolinea = stmp;
        if (objCompania != null)
        {
            objGlobales.mTxtCodCompania = sCodCompania;
            objGlobales.mTxtNombre = objCompania.SDscCompania.Trim();
            if (objCompania.SCodIATA != null)
                objGlobales.mTxtIATA = objCompania.SCodIATA;
            if (objCompania.SCodOACI != null)
                objGlobales.mTxtOACI = objCompania.SCodOACI;
            if (objCompania.SDscRuc != null)
                objGlobales.mTxtRuc = objCompania.SDscRuc;
            ViewState["Ruc"] = objCompania.SDscRuc;
            if (objCompania.SCodEspecialCompania != null)
                objGlobales.mTxtCodigoEspecial = objCompania.SCodEspecialCompania;
            if (objCompania.SCodAerolinea != null)
                objGlobales.mTxtAerolinea = objCompania.SCodAerolinea;
            if (objCompania.SCodSAP != null)
                objGlobales.mTxtSAP = objCompania.SCodSAP;
            if (objCompania.STipCompania != null)
                objGlobales.mIndxTipoCompania = IndexListaCampo(objCompania.STipCompania, this.ddlTipoCompañía);
            if (objCompania.STipEstado != null)
                objGlobales.mIndxEstadoCompania = IndexListaCampo(objCompania.STipEstado, this.ddlEstado);

            objGlobales.SIndxTipoCompania = objCompania.STipCompania;
        }
        guardarGlobales(objGlobales);
    }

    protected int IndexListaCampo(string CodCampo, DropDownList ddlBox)
    {
        for (int i = 0; i < ddlBox.Items.Count; i++)
        {
            if (ddlBox.Items[i].Value == CodCampo)
            {
                return i;
            }
        }
        return 0;

    }
    protected void PoblarRepresentante(string sCodCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        List<RepresentantCia> objLisRepresentante = new List<RepresentantCia>();

        objLisRepresentante = objWBOperacion.ListarRepteCia(sCodCompania);

        if (objLisRepresentante.Count > 0)
        {
            for (int i = 0; i < objLisRepresentante.Count; i++)
            {
                string[] nombre = new string[2];
                nombre = objLisRepresentante[i].SNomRepresentante.Split(' ');
                objLisRepresentante[i].SNomRepresentante = nombre[0];
                objGlobales.mRRegistrados.Add(objLisRepresentante[i].INumSecuencial, objLisRepresentante[i]);
            }
        }
    }
    protected void PoblarModalidadVenta(string sCodCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        List<ModVentaComp> objListModalidadVenta = new List<ModVentaComp>();

        objListModalidadVenta = objWBAdministracion.ListarModVentaCompxCompañia(sCodCompania);
        
        Session["ModalidadVenta"] = objListModalidadVenta;

        if (objListModalidadVenta.Count > 0)
        {
            for (int i = 0; i < objListModalidadVenta.Count; i++)
            {
                objGlobales.mValoresActuales.Add(objListModalidadVenta[i].SCodModalidadVenta, "1");
            }
        }
        guardarGlobales(objGlobales);
    }


    protected void PoblarAtributos(string sCodCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mValoresActuales.Count > 0)
        {

            object[] keys = new object[objGlobales.mValoresActuales.Keys.Count];
            objGlobales.mValoresActuales.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                List<ModVentaCompAtr> objListModVentaCompAtr = new List<ModVentaCompAtr>();
                objListModVentaCompAtr = objWBAdministracion.ObtenerModVentaCompAtr(sCodCompania, keys[i].ToString());


                if (objListModVentaCompAtr.Count > 0)
                {
                    for (int j = 0; j < objListModVentaCompAtr.Count; j++)
                    {
                        ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr();

                        objModVentaCompAtr = objListModVentaCompAtr[j];
                        string Identificador = objModVentaCompAtr.SCodAtributo;

                        ParameGeneral objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                        string valor = "";
                        if (objParameGeneral != null)
                        {
                            if (objParameGeneral.STipoParametro == "L")
                            {
                                valor = Convert.ToString(IndiceListaCampo(valor, objParameGeneral.SCampoLista));
                            }
                            else
                            {
                                valor = objModVentaCompAtr.SDscValor;
                            }
                        }
                        string parametro = Identificador + "|" + valor;



                        if (objGlobales.mMVRegistrados[keys[i].ToString()] != null)
                        {
                            ArrayList AtributosMV = new ArrayList();
                            AtributosMV = (ArrayList)objGlobales.mMVRegistrados[keys[i].ToString()];
                            AtributosMV.Add(parametro);
                            objGlobales.mMVRegistrados[keys[i].ToString()] = AtributosMV;
                        }
                        else
                        {
                            ArrayList AtributosMV = new ArrayList();
                            AtributosMV.Add(parametro);
                            objGlobales.mMVRegistrados.Add(keys[i].ToString(), AtributosMV);
                        }

                    }
                }
            }
        }
        guardarGlobales(objGlobales);
    }

    protected int IndiceListaCampo(string CodCampo, string CampoLista)
    {
        List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();
        objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(CampoLista);

        for (int i = 0; i < objListListaDeCampo.Count; i++)
        {
            if (objListListaDeCampo[i].SCodCampo == CodCampo)
            {
                return i;
            }
        }
        return 0;
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
        string codCompania = txtCodigo.Text;
        CompDetalle1.CargarFormulario(Identificador, codCompania);
    }


    private DataTable ActualizarGrilla()
    {
        Sglobales objGlobales = obtenerGlobales();

        DataTable dtRepresentante = new DataTable();
        DataRow drRepresentante;

        dtRepresentante.Columns.Add(new DataColumn("NumSecuencial", System.Type.GetType("System.String")));
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
            drRepresentante["NumSecuencial"] = objRepresentantCia.INumSecuencial;
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

        gvwRepresentante.Columns[1].Visible = false;
    }

    protected void CargarCombos()
    {
        try
        {
            //Carga combo TipoCompania
            DataTable dt_TipoCompania = new DataTable();
            dt_TipoCompania = objWBConsultas.ListaCamposxNombre("TipoCompania");
            objCargaCombo.LlenarCombo(this.ddlTipoCompañía, dt_TipoCompania, "Cod_Campo", "Dsc_Campo", false, false);

            //Carga combo Estado Compania
            DataTable dt_estado = new DataTable();
            dt_estado = objWBConsultas.ListaCamposxNombre("EstadoCompania");
            objCargaCombo.LlenarCombo(this.ddlEstado, dt_estado, "Cod_Campo", "Dsc_Campo", false, false);
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
            String codigo = dtRepresentante.Rows[rowIndex]["NumSecuencial"].ToString();
            this.RepresDetalle1.CargarFormulario(codigo);
        }
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

    protected void  btnAceptar_Click(object sender, EventArgs e)
    {

        lock(this)
        {
            Sglobales objGlobales = obtenerGlobales();
            try
            {                
                objWBAdministracion = new BO_Administracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], 
                    (string)Session["Cod_SubModulo"], Define.CNX_14);
                if (validaCompañia() == true)
                {
                    if (validaNombre(this.txtCodigo.Text.Trim(), this.txtNombre.Text.Trim()) == false)
                    {
                        if (txtSAP.Text.Trim() != "")
                        {
                            txtSAP.Text = poblarCeros(txtSAP.Text, 10);
                        }

                        if (txtCodigoEspecial.Text.Trim() != "")
                        {
                            txtCodigoEspecial.Text = poblarCeros(txtCodigoEspecial.Text, 10);
                        }

                        Compania objCompania = new Compania(this.txtCodigo.Text.Trim(), objGlobales.SIndxTipoCompania, this.txtNombre.Text.Trim().ToUpper(), DateTime.Today, ddlEstado.SelectedValue, this.txtAerolinea.Text.Trim().ToUpper(),
                        this.txtRuc.Text.Trim(), this.txtCodigoEspecial.Text.Trim(), this.txtSAP.Text.Trim(), this.txtOACI.Text.Trim().ToUpper(), this.txtIATA.Text.Trim().ToUpper(), Convert.ToString(Session["Cod_Usuario"]), "", "");

                        if (ddlTipoCompañía.SelectedItem.Value.Trim() == "1")
                        {
                            dt_ValidarCodInt = objWBConsultas.ConsultaCompaniaxFiltro("0", "0", "", "ASC");
                            DataRow[] countRegCodUnico;
                            countRegCodUnico = dt_ValidarCodInt.Select("Cod_Compania<>'" + txtCodigo.Text + "' AND Cod_Especial_Compania='" + txtAerolinea.Text + "'");
                            int NumTotCodUnico = countRegCodUnico.Length;

                            DataRow[] countRegCodIATA;
                            countRegCodIATA = dt_ValidarCodInt.Select("Cod_Compania<>'" + txtCodigo.Text + "' AND Cod_IATA='" + txtIATA.Text + "'");
                            int NumTotCodIATA = countRegCodIATA.Length;

                            DataRow[] countRegCodAerolinea;
                            countRegCodAerolinea = dt_ValidarCodInt.Select("Cod_Compania<>'" + txtCodigo.Text + "' AND Cod_Aerolinea='" + txtCodigoEspecial.Text + "'");
                            int NumTotCodAerolinea = countRegCodAerolinea.Length;

                            DataRow[] countRegCodSAP;
                            int NumTotCodSAP = 0;
                            if (txtSAP.Text.Trim() != "")
                            {
                                countRegCodSAP = dt_ValidarCodInt.Select("Cod_Compania<>'" + txtCodigo.Text + "' AND Cod_SAP='" + txtSAP.Text + "'");
                                NumTotCodSAP = countRegCodSAP.Length;
                            }

                            DataRow[] countRegCodOACI;
                            int NumTotCodOACI = 0;
                            if (txtOACI.Text.Trim() != "")
                            {
                                countRegCodOACI = dt_ValidarCodInt.Select("Cod_Compania<>'" + txtCodigo.Text + "' AND Cod_OACI='" + txtOACI.Text + "'");
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
                                                lblMensajeError.Text = "";

                                                idTxCritica = objWBAdministracion.obtenerIdTransaccionCritica();
                                                objCompania.IdTxCritica = idTxCritica;

                                                if (objWBAdministracion.actualizarCompaniaCrit(objCompania) == true)
                                                {
                                                    if (ActualizarRepresentantes(this.txtCodigo.Text) == true)
                                                    {
                                                        if (ActualizarModalidadVenta(this.txtCodigo.Text) == true)
                                                        {
                                                            if (ActualizarAtributos(this.txtCodigo.Text) == true)
                                                            {
                                                                if (ddlEstado.SelectedValue.ToString() == "2")
                                                                {
                                                                    //GeneraAlarma
                                                                    string IpClient = Request.UserHostAddress;
                                                                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000013", "003", IpClient, "1", "Alerta W0000013", "Compañia anulada: " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                                                                }
                                                                else
                                                                {
                                                                    //string pathMap = getPathMap(SiteMap.Provider.FindSiteMapNode(Request.RawUrl));
                                                                    string pathMap = "Home : Mantenimiento : Compañía : Modificar";
                                                                    string IpClient = Request.UserHostAddress;
                                                                    string strMessageA = "Compañía registrada correctamente";
                                                                    GestionAlarma.RegistrarAlarmaCrit(HttpContext.Current.Server.MapPath(""), "W0000086", "003",
                                                                        IpClient, "1", "Alerta W0000086", "<br/>" + strMessageA + "<br/> Usuario: " + Convert.ToString(Session["Cod_Usuario"]),
                                                                        Convert.ToString(Session["Cod_Usuario"]), idTxCritica, "Compania", pathMap);
                                                                    
                                                                }
                                                                omb.ShowMessage("Compañía actualizada correctamente", "Modificación de Compañía", "Mnt_VerCompania.aspx");
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //Hacemos uso de este atributo para mostrar el mensaje de validacion que se realiza en el sp
                                                    lblMensajeError.Text = objCompania.SLogFechaMod;
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
                            lblMensajeError.Text = "";
                            if (objWBAdministracion.actualizarCompania(objCompania) == true)
                            {
                                if (ActualizarRepresentantes(this.txtCodigo.Text) == true)
                                {
                                    if (ActualizarModalidadVenta(this.txtCodigo.Text) == true)
                                    {
                                        if (ActualizarAtributos(this.txtCodigo.Text) == true)
                                        {
                                            if (ddlEstado.SelectedValue.ToString() == "2")
                                            {
                                                //GeneraAlarma
                                                string IpClient = Request.UserHostAddress;
                                                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000013", "003", IpClient, "1", "Alerta W0000013", "Compañia anulada: " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                                            }
                                            //eliminarGlobales();
                                            omb.ShowMessage("Compañía actualizada correctamente", "Modificación de Compañía", "Mnt_VerCompania.aspx");

                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Hacemos uso de este atributo para mostrar el mensaje de validacion que se realiza en el sp
                                lblMensajeError.Text = objCompania.SLogFechaMod;
                            }
                        }
                        //if (NumTotCodIATA == 0)
                        //{
                        //    lblMensajeError.Text = "";
                        //    if (objWBAdministracion.actualizarCompania(objCompania) == true)
                        //    {
                        //        if (ActualizarRepresentantes(this.txtCodigo.Text) == true)
                        //        {
                        //            if (ActualizarModalidadVenta(this.txtCodigo.Text) == true)
                        //            {
                        //                if (ActualizarAtributos(this.txtCodigo.Text) == true)
                        //                {
                        //                    if (ddlEstado.SelectedValue.ToString() == "2")
                        //                    {
                        //                        //GeneraAlarma
                        //                        string IpClient = Request.UserHostAddress;
                        //                        GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000013", "003", IpClient, "1", "Alerta W0000013", "Compañia anulada: " + txtCodigo.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                        //                    }
                        //                    omb.ShowMessage("Compañía actualizada correctamente", "Modificación de Compañía", "Mnt_VerCompania.aspx");
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    lblMensajeError.Text = "El código IATA ya existe";
                        //}

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


            Compania objCompania = new Compania();

            objCompania = objWBAdministracion.obtenerCompañiaxcodigo(this.txtCodigo.Text);


            if (this.txtRuc.Text != "" && this.txtRuc.Text != objCompania.SDscRuc)
            {
                if (objWBAdministracion.validarRucCompañia(this.txtRuc.Text.Trim()) == false)
                {
                    this.lblMensajeError.Text = "El Nº de Ruc ya se encuentra Registrado, verifique por favor";
                    objGlobales.mMjeError = "El Nº de Ruc ya se encuentra Registrado, verifique por favor";
                    guardarGlobales(objGlobales);
                    return false;
                }
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

        guardarGlobales(objGlobales);
        return true;
    }



    protected bool validaNombre(string CodCompañia, string sNomCompañia)
    {
        string nombre = objWBAdministracion.obtenerCompañiaxcodigo(CodCompañia).SDscCompania;

        if (nombre.Trim() != sNomCompañia.Trim())
        {
            if (objWBAdministracion.obtenerCompañiaxnombre(this.txtNombre.Text) != null)
            {
                return true;
            }
        }
        return false;
    }

    protected bool ActualizarRepresentantes(string sCompañia)
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

                if (validaRepresentante(sCompañia, objRepresentantCia.INumSecuencial) == true)
                {
                    if (objWBAdministracion.actualizarRepresentanteCrit(objRepresentantCia) == false)
                    {
                        guardarGlobales(objGlobales);
                        return false;
                    }
                }
                else
                {
                    if (objWBAdministracion.insertarRepresentanteCrit(objRepresentantCia) == false)
                    {
                        guardarGlobales(objGlobales);
                        return false;
                    }

                }
            }
        }

        guardarGlobales(objGlobales);
        return true;
    }

    protected bool ActualizarModalidadVenta(string strCodigoCompania)
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

                    if (validaModalidadVenta(strCodigoCompania, keys[j].ToString()) == false)
                    {                        
                        if (objWBAdministracion.insertarModVentaCompCrit(objModVentaComp) == false)
                        {
                            guardarGlobales(objGlobales);
                            return false;
                        }

                        //INSERTAR SECUENCIA MODALIDAD COMPAÑIA PARA BCBP Y VENTA MASIVA DE CREDITO
                        if (keys[j].ToString() == "M0002" || keys[j].ToString() == "M0004")
                        {
                            if (!objWBAdministracion.insertarSecuenciaModVentaComp(keys[j].ToString() + "-" + strCodigoCompania))
                            {
                                guardarGlobales(objGlobales);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        objWBAdministracion.actualizarModVentaCompCrit(objModVentaComp);
                    }
                }
                else
                {
                    if (validaModalidadVenta(strCodigoCompania, keys[j].ToString()) == true)
                    {
                        ModVentaComp objModVentaComp = new ModVentaComp(strCodigoCompania, keys[j].ToString(), "");

                        objModVentaComp.IdTxCritica = idTxCritica;

                        //if (objWBAdministracion.eliminarModVentaComp(strCodigoCompania, keys[j].ToString()) == false)
                        if (objWBAdministracion.eliminarModVentaCompCrit(objModVentaComp) == false)
                        {
                            guardarGlobales(objGlobales);
                            return false;
                        }
                        else {
                            objGlobales.mMVRegistrados.Remove(keys[j].ToString());
                        }
                    }

                }

            }
        }

        guardarGlobales(objGlobales);
        return true;
    }


    protected bool validaRepresentante(string SCodCompañia, int sNumerico)
    {
        List<RepresentantCia> objLisRepresentante = new List<RepresentantCia>();

        objLisRepresentante = objWBOperacion.ListarRepteCia(SCodCompañia);

        for (int i = 0; i < objLisRepresentante.Count; i++)
        {
            RepresentantCia objRepresentantCia = new RepresentantCia();
            objRepresentantCia = objLisRepresentante[i];

            if (objRepresentantCia.INumSecuencial == sNumerico)
            {
                return true;
            }
        }

        return false;
    }

    protected bool validaModalidadVenta(string SCodCompañia, string sCodModVenta)
    {
        List<ModVentaComp> objLisModVentaComp = new List<ModVentaComp>();

        objLisModVentaComp = (List<ModVentaComp>)Session["ModalidadVenta"];

        for (int i = 0; i < objLisModVentaComp.Count; i++)
        {
            ModVentaComp objModVentaComp = new ModVentaComp();
            objModVentaComp = objLisModVentaComp[i];

            if (objModVentaComp.SCodModalidadVenta.Trim() == sCodModVenta && objModVentaComp.SCodCompania.Trim() == SCodCompañia)
            {
                return true;
            }

        }

        return false;
    }

    protected bool validaAtributoModalidadVenta(string SCodCompañia, string sCodModVenta, string sAtributo)
    {
        List<ModVentaCompAtr> objListModVentaCompAtr = new List<ModVentaCompAtr>();
        objListModVentaCompAtr = objWBAdministracion.ObtenerModVentaCompAtr(SCodCompañia, sCodModVenta);

        for (int i = 0; i < objListModVentaCompAtr.Count; i++)
        {
            if (objListModVentaCompAtr[i].SCodModalidadVenta.Trim() == sCodModVenta && objListModVentaCompAtr[i].SCodCompania.Trim() == SCodCompañia && objListModVentaCompAtr[i].SCodAtributo.Trim() == sAtributo)
            {
                return true;
            }
        }

        return false;
    }


    protected bool ActualizarAtributos(string SCodCompañia)
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        if (objGlobales.mMVRegistrados.Count > 0)
        {
            object[] keys = new object[objGlobales.mMVRegistrados.Keys.Count];
            objGlobales.mMVRegistrados.Keys.CopyTo(keys, 0);


            for (int i = 0; i < keys.Length; i++)
            {
                string ModalidadVenta = keys[i].ToString();
                ArrayList listaMV = new ArrayList();
                listaMV = (ArrayList)objGlobales.mMVRegistrados[ModalidadVenta];
                for (int j = 0; j < listaMV.Count; j++)
                {
                    lista = new string[2];
                    lista = Convert.ToString(listaMV[j]).Split('|');
                    string Identificador = Convert.ToString(lista[0]);
                    string valor = Convert.ToString(lista[1]);

                    objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                    if (objParameGeneral.STipoParametro == "L")
                    {
                        valor = ValueListaCampo(Convert.ToInt32(valor), objParameGeneral.SCampoLista);
                    }

                    ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr(
                        ModalidadVenta, 
                        valor, 
                        "", 
                        Convert.ToString(Session["Cod_Usuario"]),
                        "",
                        SCodCompañia, 
                        Identificador,
                        "");

                    objModVentaCompAtr.IdTxCritica = idTxCritica;

                    if (validaAtributoModalidadVenta(SCodCompañia, ModalidadVenta, Identificador) == true)
                    {
                        if (objWBAdministracion.actualizarModVentaCompAtrCrit(objModVentaCompAtr) == false)
                        {
                            guardarGlobales(objGlobales);
                            return false;
                        }
                    }
                    else
                    {
                        if (objWBAdministracion.insertarModVentaCompAtrCrit(objModVentaCompAtr) == false)
                        {
                            guardarGlobales(objGlobales);
                            return false;
                        }
                    }
                }
            }
        }

        if (objGlobales.mMVEliminados.Count > 0)
        {
            object[] keys = new object[objGlobales.mMVEliminados.Keys.Count];
            objGlobales.mMVEliminados.Keys.CopyTo(keys, 0);


            for (int i = 0; i < keys.Length; i++)
            {
                string ModalidadVenta = keys[i].ToString();
                ArrayList listaMV = new ArrayList();
                listaMV = (ArrayList)objGlobales.mMVEliminados[ModalidadVenta];
                for (int j = 0; j < listaMV.Count; j++)
                {
                    string Identificador = (string)listaMV[j];

                    ModVentaCompAtr objModVentaCompAtr = new ModVentaCompAtr();
                    objModVentaCompAtr.SCodCompania = SCodCompañia;
                    objModVentaCompAtr.SCodModalidadVenta = ModalidadVenta;
                    objModVentaCompAtr.SCodAtributo = Identificador;
                    objModVentaCompAtr.IdTxCritica = idTxCritica;

                    if (objWBAdministracion.eliminarModVentaCompAtrCrit(objModVentaCompAtr) == false)
                    {
                        guardarGlobales(objGlobales);
                        return false;
                    }
                }
            }
        }
        guardarGlobales(objGlobales);
        return true;
    }

    protected bool validaModalidaVenta(string sModalidadVenta)
    {
        Sglobales objGlobales = obtenerGlobales();

        bool encontrado = false;
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
        string tipoCompania = ddlTipoCompañía.SelectedItem.Text;
        objGlobales.mIndxTipoCompania = ctrl.SelectedIndex;
        objGlobales.SIndxTipoCompania = ddlTipoCompañía.SelectedValue.ToString();

        //deshabilitamos el tipo de venta ATM en caso sea diferente de Banco
        for (int i = 0; i < objGlobales.mCListaControles.Count; i++)
        {
            string valor = objGlobales.mCListaControles[i].ToString();
            if (valor.Equals("ChkBox"))
            {
                ChkBox chkTipoVenta = (ChkBox)objGlobales.mCListaControles[i];
                string codigo = chkTipoVenta.ID;
                bool tipo = codigo.EndsWith("M0005");

                if (tipo)
                {
                    if (tipoCompania.Equals("BANCO"))
                    {
                        chkTipoVenta.Enabled = true;
                    }
                    else
                    {
                        chkTipoVenta.Enabled = false;
                    }
                }

            }

            if (valor.EndsWith("ImageButton"))
            {
                ImageButton imgTipoVenta = (ImageButton)objGlobales.mCListaControles[i];
                string codigo = imgTipoVenta.ID;
                bool tipo = codigo.EndsWith("M0005");

                if (tipo)
                {
                    if (tipoCompania.Equals("BANCO"))
                    {
                        imgTipoVenta.Enabled = true;
                        imgTipoVenta.Visible = true;
                    }
                    else
                    {
                        imgTipoVenta.Enabled = false;
                        imgTipoVenta.Visible = false;
                    }
                }

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

            Compania objCompania = (Compania)Session["objCompania"];
            this.txtCodigoEspecial.Text = objCompania.SCodEspecialCompania;
            this.txtIATA.Text = objCompania.SCodIATA;
            this.txtSAP.Text = objCompania.SCodSAP;
            this.txtOACI.Text = objCompania.SCodOACI;

            objGlobales.mTxtAerolinea = "";
         }
            //en caso el tipo seleccionado es aerolinea
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
