/*
Sistema		 :   TUUA
Aplicación	 :   Administración
Objetivo		 :   Creación de Modalidad de Venta
Especificaciones:   Se considera aquellas marcaciones según el rango programado.
Fecha Creacion	 :   11/07/2009	
Programador	 :	GCHAVEZ
Observaciones:	
*/
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
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using System.Collections.Generic;

public partial class Mnt_CrearModalidadVenta : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Operacion objWBOperacion = new BO_Operacion();
    BO_Configuracion objWBConfiguracion = new BO_Configuracion();
    List<ParameGeneral> objListParamGeneralMV = new List<ParameGeneral>();
    UIControles objCargaCombo = new UIControles();
    ArrayList controlMV = new ArrayList();
    Hashtable controlTT = new Hashtable();
    bool flagError;
  
    protected void Page_Load(object sender, EventArgs e)
    {
        this.lblMensajeError.Text = ""; 
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblDescripcion.Text = htLabels["mmodalidadventa.lblDescripcion"].ToString();
                this.lblTipoModalidad.Text = htLabels["mmodalidadventa.lblTipoModalidad"].ToString();
                this.lblTipoTicket.Text = htLabels["mmodalidadventa.lblTipoTicket"].ToString();
                this.lblTituloAtributosModalidadVenta.Text = htLabels["mmodalidadventa.lblTituloAtributosModalidadVenta"].ToString();
                this.lblTituloAtributosxTipoTicket.Text = htLabels["mmodalidadventa.lblTituloAtributosxTipoTicket"].ToString();
                btnAceptar.Text = htLabels["mmodalidadventa.btnAceptar"].ToString();
                hConfirmacion.Value = htLabels["mmodalidadventa.cbeAceptar"].ToString();

            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegCrearUsuario;
                Response.Redirect("PaginaError.aspx");
            }
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
                    this.txtDescripcion.Focus();
                    objGlobales.mListaRegistradosMV = new ArrayList();
                    objGlobales.mListaRegistradosTT = new Hashtable();
                    objGlobales.mListaEliminadosMV = new ArrayList();
                    objGlobales.mListaEliminadosTT = new Hashtable();
                    objGlobales.mListaActualesTT = new Hashtable();
                    objGlobales.mListaActualesMV = new Hashtable();
                    objGlobales.mDscModalidadVenta = "";
                    objGlobales.mIndxTipoModalidad = -1;
                    objGlobales.mMjeError = "";
                }
            }
            CargarCombos();
        }
        objListParamGeneralMV = objWBAdministracion.listarAtributosGenerales();

        if (objGlobales.mMjeError != "")
        {
            lblMensajeError.Text = objGlobales.mMjeError;
        }

        if (objGlobales.mDscModalidadVenta != "")
        {
            txtDescripcion.Text = objGlobales.mDscModalidadVenta;
        }
        if (objGlobales.mIndxTipoModalidad != -1)
        {
            this.ddlTipoModalidad.SelectedIndex = objGlobales.mIndxTipoModalidad;
        }

        if (objGlobales.mListaRegistradosMV.Count > 0)
        {
            ActualizarValoresActualesMV(objGlobales.mListaActualesMV);
            CrearPanelAtributosAsignadosMV(objGlobales.mListaRegistradosMV, pnlAtributosAsignadosMV);
        }
        else
        {
            this.pnlAtributosAsignadosMV.Visible = false;
        }


        if (objGlobales.mListaRegistradosTT.Count > 0)
        {
            ActualizarValoresActualesTT(objGlobales.mListaActualesTT);
            CrearPanelAtributosAsignadosTT(objGlobales.mListaRegistradosTT, pnlAtributosAsignadosTT);
        }
        else
        {
            this.pnlAtributosAsignadosTT.Visible = false;
        }
        guardarGlobales(objGlobales);
    }

    private void CrearPanelAtributosAsignadosMV(ArrayList asignados, Panel Panel)
    {

        Sglobales objGlobales = obtenerGlobales();

        if (asignados.Count > 0)
        {
            Panel.Controls.Clear();
            Table TableAtributosMV = new Table();
            TableAtributosMV.EnableViewState = true;
            TableAtributosMV.CssClass = "TablaModVenta";
            TableAtributosMV.CellPadding = 4; //set padding
            TableAtributosMV.BorderWidth = 1;

            ////header row
            TableRow TableRow1 = new TableRow(); //initialize new row
            TableAtributosMV.Rows.Add(TableRow1); //add row to table

            TableCell TableCell1 = new TableCell(); //initialize new cell
            TableRow1.Cells.Add(TableCell1); //add cell to row
            MyLabel Label1 = new MyLabel(); //initialize new label
            Label1.Text = "Identificador"; //set text property fopr label
            Label1.ID = Panel.ID + UI.LabelPrefix + "I";
            Label1.Visible = false;
            TableCell1.CssClass = "centrado";
            TableCell1.Controls.Add(Label1); //add label to cell

            TableCell TableCell2 = new TableCell(); //initialize new cell
            TableRow1.Cells.Add(TableCell2); //add cell to row
            MyLabel Label2 = new MyLabel(); //initialize new label
            Label2.Text = "Num"; //set text property fopr label
            Label2.CssClass = "cabeceratablaMV"; //set css class for label
            Label2.ID = Panel.ID + UI.LabelPrefix + "II";
            TableCell2.CssClass = "centrado";
            TableCell2.Controls.Add(Label2); //add label to cell

            TableCell TableCell3 = new TableCell(); // initialize new cell
            TableRow1.Cells.Add(TableCell3);  //add cell to row
            MyLabel Label3 = new MyLabel(); //initialize new label
            Label3.ID = Panel.ID + UI.LabelPrefix + "III";
            Label3.Text = "Atributo: "; //set text property for label
            Label3.CssClass = "cabeceratablaMV"; //set css class for label
            TableCell3.Controls.Add(Label3); //add label to row

            TableCell TableCell4 = new TableCell(); //initialize new table cell
            TableRow1.Cells.Add(TableCell4);  //add cell to row
            MyLabel Label4 = new MyLabel(); //initialize new label
            Label4.ID = Panel.ID + UI.LabelPrefix + "IV";
            Label4.Text = "Valor: "; //set text property 
            Label4.CssClass = "cabeceratablaMV"; // set css class
            TableCell4.CssClass = "centrado";
            TableCell4.Controls.Add(Label4);  // add label to cell

            TableCell TableCell5 = new TableCell(); //initialize new table cell
            TableRow1.Cells.Add(TableCell5);  //add cell to row
            MyLabel Label5 = new MyLabel(); //initialize new label
            Label5.Text = ""; //set text property 
            Label5.ID = Panel.ID + UI.LabelPrefix + "V";
            TableCell5.Controls.Add(Label5);  // add label to cell


            TableRow TableRow2;
            TableCell TableCell9;
            string[] lista;
            TableRow2 = new TableRow();
            ParameGeneral objParameGeneral;
            TableAtributosMV.Rows.Add(TableRow2);

            for (int i = 0; i < asignados.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(asignados[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                string Valor = Convert.ToString(lista[1]);
                objParameGeneral = new ParameGeneral();
                objParameGeneral = ParametroElegido(Identificador);

                TableCell TableCell11 = new TableCell(); //initialize new table cell
                TableRow2.Cells.Add(TableCell11);  //add cell to row
                MyLabel Label8 = new MyLabel(); //initialize new label
                Label8.ID = Panel.ID + UI.LabelPrefix + "I" + i.ToString(); //set ID property 
                Label8.Text = Identificador; //set text property for label
                Label8.Visible = false;
                TableCell11.Controls.Add(Label8);  // add label to cell

                TableCell TableCell7 = new TableCell(); //initialize new table cell
                TableRow2.Cells.Add(TableCell7);  //add cell to row
                MyLabel Label6 = new MyLabel(); //initialize new label
                Label6.Text = Convert.ToString(i + 1); //set text property 
                Label6.CssClass = "itemtablaMV"; // set css class
                Label6.ID = Panel.ID + UI.LabelPrefix + "II" + i.ToString();
                TableCell7.CssClass = "centrado";
                TableCell7.Controls.Add(Label6);  // add label to cell

                TableCell TableCell8 = new TableCell(); //initialize new table cell
                TableRow2.Cells.Add(TableCell8);  //add cell to row
                MyLabel Label7 = new MyLabel(); //initialize new label
                Label7.Text = objParameGeneral.SDescripcion; //set text property 
                Label7.CssClass = "itemtablaMV"; // set css class
                Label7.ID = Panel.ID + UI.LabelPrefix + "III" + i.ToString();
                TableCell8.Controls.Add(Label7);  // add label to cell

                TableCell9 = new TableCell();
                switch (objParameGeneral.STipoParametro)
                {
                    case "I":
                        TxtBox textbox = new TxtBox();
                        textbox.Text = (string)Valor;
                        textbox.ID = Panel.ID + UI.TextBoxPrefix + i.ToString() + "_" + Identificador;
                        textbox.CssClass = "textboxMV";
                        textbox.TextChanged += new System.EventHandler(this.TextBox_TextChangedMV);
                        if (objParameGeneral.STipoValor == "NUM")
                        {
                            textbox.Attributes.Add("onkeypress", "JavaScript: Tecla('Double');");
                            textbox.Attributes.Add("onblur", "val_int(this)");
                        }
                        else
                        {
                            textbox.Attributes.Add("onkeypress", "JavaScript: Tecla('Character');");
                            textbox.Attributes.Add("onblur", "gDescripcionNombre(this)");
                        }
                        TableCell9.Controls.Add(textbox);
                        break;

                    case "L":
                        DropDown ddl = new DropDown();
                        ddl.CssClass = "combo";
                        ddl.ID = Panel.ID + UI.DropDownPrefix + i.ToString() + "_" + Identificador;
                        ddl.SelectedIndexChanged += new System.EventHandler(this.DropDownList_SelectedIndexChangedMV);

                        if (ddl.Items.Count <= 0)
                        {
                            List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();

                            objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(objParameGeneral.SCampoLista);


                            for (int j = 0; j < objListListaDeCampo.Count; j++)
                            {
                                ddl.Items.Add(objListListaDeCampo[j].SDscCampo);
                            }
                        }

                        ddl.SelectedIndex = Convert.ToInt32(Valor);

                        TableCell9.Controls.Add(ddl);

                        break;

                    case "C":
                        ChkBox chk = new ChkBox();
                        if (Valor == "1")
                            chk.Checked = true;
                        else
                            chk.Checked = false;
                        chk.ID = Panel.ID + UI.CheckBoxPrefix + i.ToString() + "_" + Identificador;
                        chk.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChangedMV);
                        TableCell9.CssClass = "checkboxMV";
                        TableCell9.Controls.Add(chk);
                        break;

                    default:
                        break;
                }
                TableRow2.Cells.Add(TableCell9);

                TableCell TableCell10 = new TableCell(); //initialize new table cell
                TableRow2.Cells.Add(TableCell10);  //add cell to row
                ImageButton imgbtn = new ImageButton();
                imgbtn.ID = Panel.ID + "_" + "btn_" + Identificador;
                imgbtn.Click += new ImageClickEventHandler(this.BtnEliminaMV_Click);
                imgbtn.CausesValidation = false;
                imgbtn.ImageUrl = "Imagenes/Delete.bmp";
                TableCell10.CssClass = "centrado";
                TableCell10.Controls.Add(imgbtn);  // add label to cell

                TableAtributosMV.Rows.Add(TableRow2);
                TableRow2 = new TableRow();
            }

            Panel.Controls.Add(TableAtributosMV);

        }
        objGlobales.mFlagModal = false;
        objGlobales.mFlagPosPage = false;
        guardarGlobales(objGlobales);
    }



    private void CrearPanelAtributosAsignadosTT(Hashtable registrados, Panel Panel)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (registrados.Count > 0)
        {
            Panel.Controls.Clear();

            object[] keys = new object[registrados.Keys.Count];
            registrados.Keys.CopyTo(keys, 0);


            for (int i = 0; i < keys.Length; i++)
            {
                Table TableAtributosMV = new Table();
                TableAtributosMV.EnableViewState = true;
                TableAtributosMV.CssClass = "TablaModVenta";
                TableAtributosMV.CellPadding = 4; //set padding
                TableAtributosMV.BorderWidth = 1;
                TableAtributosMV.ID = Panel.ID + i.ToString();
                //header row
                TableRow TableRow1 = new TableRow(); //initialize row for header
                TableAtributosMV.Rows.Add(TableRow1); //add row to table
                TableCell TableCell1 = new TableCell(); //initialize table cell
                TableCell1.ColumnSpan = 3; //set column span to 3
                TableCell1.CssClass = "titulotablaMV"; //assign css class for cell
                MyLabel Label1 = new MyLabel(); //initialize new label

                string sDscTipoTicket = objWBAdministracion.obtenerTipoTicket(keys[i].ToString()).SNomTipoTicket;

                Label1.Text = keys[i].ToString() + " " + sDscTipoTicket; //label text
                Label1.ID = TableAtributosMV.ID + UI.LabelPrefix + "I" + i.ToString();
                TableCell1.Controls.Add(Label1); //add label to cell
                TableRow1.Cells.Add(TableCell1); // add cell to row


                TableCell TableCell13 = new TableCell(); //initialize new table cell
                TableRow1.Cells.Add(TableCell13);  //add cell to row
                ImageButton imgbtn = new ImageButton();
                imgbtn.ID = TableAtributosMV.ID + "_" + "btn_" + keys[i].ToString();
                imgbtn.CausesValidation = false;
                imgbtn.ImageUrl = "Imagenes/Add.bmp";
                imgbtn.Click += new ImageClickEventHandler(this.btnAgregarTT_Click);
                ArrayList ListaRegistrado = new ArrayList();
                ListaRegistrado = (ArrayList)registrados[keys[i]];
                TableCell13.Controls.Add(imgbtn);  // add label to cell


                if (ListaRegistrado.Count > 0)
                {
                    //row 2
                    TableRow TableRow2 = new TableRow(); //initialize new row
                    TableAtributosMV.Rows.Add(TableRow2); //add row to table

                    TableCell TableCell10 = new TableCell(); //initialize new cell
                    TableRow2.Cells.Add(TableCell10); //add cell to row
                    MyLabel Label8 = new MyLabel(); //initialize new label
                    Label8.Text = "Identificador"; //set text property fopr label
                    Label8.ID = TableAtributosMV.ID + UI.LabelPrefix + "II";
                    Label8.Visible = false;
                    TableCell10.CssClass = "centrado";
                    TableCell10.Controls.Add(Label8); //add label to cell

                    TableCell TableCell2 = new TableCell(); //initialize new cell
                    TableRow2.Cells.Add(TableCell2); //add cell to row
                    MyLabel Label2 = new MyLabel(); //initialize new label
                    Label2.ID = TableAtributosMV.ID + UI.LabelPrefix + "III" + i.ToString();
                    Label2.Text = "Num"; //set text property fopr label
                    Label2.CssClass = "cabeceratablaMV"; //set css class for label
                    TableCell2.CssClass = "centrado";
                    TableCell2.Controls.Add(Label2); //add label to cell

                    TableCell TableCell3 = new TableCell(); // initialize new cell
                    TableRow2.Cells.Add(TableCell3);  //add cell to row
                    MyLabel Label3 = new MyLabel(); //initialize new label
                    Label3.ID = TableAtributosMV.ID + UI.LabelPrefix + "IV" + i.ToString();
                    Label3.Text = "Atributo: "; //set text property for label
                    Label3.CssClass = "cabeceratablaMV"; //set css class for label
                    TableCell3.Controls.Add(Label3); //add label to row

                    TableCell TableCell4 = new TableCell(); //initialize new table cell
                    TableRow2.Cells.Add(TableCell4);  //add cell to row
                    MyLabel Label4 = new MyLabel(); //initialize new label
                    Label4.ID = TableAtributosMV.ID + UI.LabelPrefix + "V" + i.ToString();
                    Label4.Text = "Valor: "; //set text property 
                    Label4.CssClass = "cabeceratablaMV"; // set css class
                    TableCell4.CssClass = "centrado";
                    TableCell4.Controls.Add(Label4);  // add label to cell

                    TableCell TableCell5 = new TableCell(); //initialize new table cell
                    TableRow2.Cells.Add(TableCell5);  //add cell to row
                    MyLabel Label5 = new MyLabel(); //initialize new label
                    Label5.ID = TableAtributosMV.ID + UI.LabelPrefix + "VI" + i.ToString();
                    Label5.Text = ""; //set text property 
                    TableCell5.Controls.Add(Label5);  // add label to cell


                    TableRow TableRow3;
                    TableCell TableCell8;
                    string[] lista;
                    TableRow3 = new TableRow();
                    ParameGeneral objParameGeneral;
                    TableAtributosMV.Rows.Add(TableRow3);



                    for (int j = 0; j < ListaRegistrado.Count; j++)
                    {
                        lista = new string[2];
                        lista = Convert.ToString(ListaRegistrado[j]).Split('|');
                        string Identificador = Convert.ToString(lista[0]);
                        string Valor = Convert.ToString(lista[1]);
                        objParameGeneral = new ParameGeneral();
                        objParameGeneral = ParametroElegido(Identificador);

                        TableCell TableCell12 = new TableCell(); //initialize new table cell
                        TableRow3.Cells.Add(TableCell12);  //add cell to row
                        MyLabel Label9 = new MyLabel(); //initialize new label
                        Label9.ID = TableAtributosMV.ID + UI.LabelPrefix + "VII" + j.ToString(); //set ID property 
                        Label9.Text = Identificador; //set text property for label
                        Label9.Visible = false;
                        TableCell12.Controls.Add(Label9);  // add label to cell


                        TableCell TableCell6 = new TableCell(); //initialize new table cell
                        TableRow3.Cells.Add(TableCell6);  //add cell to row
                        MyLabel Label6 = new MyLabel(); //initialize new label
                        Label6.ID = TableAtributosMV.ID + UI.LabelPrefix + "VIII" + j.ToString();
                        Label6.Text = Convert.ToString(j + 1); //set text property 
                        Label6.CssClass = "itemtablaMV"; // set css class
                        TableCell6.CssClass = "centrado";
                        TableCell6.Controls.Add(Label6);  // add label to cell

                        TableCell TableCell7 = new TableCell(); //initialize new table cell
                        TableRow3.Cells.Add(TableCell7);  //add cell to row
                        MyLabel Label7 = new MyLabel(); //initialize new label
                        Label7.ID = TableAtributosMV.ID + UI.LabelPrefix + "IX" + j.ToString();
                        Label7.Text = objParameGeneral.SDescripcion; //set text property 
                        Label7.CssClass = "itemtablaMV"; // set css class
                        TableCell7.Controls.Add(Label7);  // add label to cell

                        TableCell8 = new TableCell();
                        switch (objParameGeneral.STipoParametro)
                        {
                            case "I":
                                TxtBox textbox = new TxtBox();
                                textbox.Text = (string)Valor;
                                textbox.CssClass = "textboxMV";
                                textbox.TextChanged += new System.EventHandler(this.TextBox_TextChangedTT);
                                textbox.ID = TableAtributosMV.ID + UI.TextBoxPrefix + j.ToString() + "_" + Identificador + "_" + keys[i].ToString(); ;

                                if (objParameGeneral.STipoValor == "NUM")
                                {
                                    textbox.Attributes.Add("onkeypress", "JavaScript: Tecla('Double');");
                                    textbox.Attributes.Add("onblur", "val_int(this)");
                                }
                                else
                                {
                                    textbox.Attributes.Add("onkeypress", "JavaScript: Tecla('Character');");
                                    textbox.Attributes.Add("onblur", "gDescripcionNombre(this)");
                                }
                                TableCell8.Controls.Add(textbox);
                                break;

                            case "L":
                                DropDown ddl = new DropDown();
                                ddl.CssClass = "combo";
                                ddl.ID = TableAtributosMV.ID + UI.DropDownPrefix + j.ToString() + "_" + Identificador + "_" + keys[i].ToString(); ;
                                ddl.SelectedIndexChanged += new System.EventHandler(this.DropDownList_SelectedIndexChangedTT);
                                if (ddl.Items.Count <= 0)
                                {
                                    List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();

                                    objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(objParameGeneral.SCampoLista);

                                    for (int k = 0; k < objListListaDeCampo.Count; k++)
                                    {
                                        ddl.Items.Add(objListListaDeCampo[k].SDscCampo);
                                    }
                                }

                                ddl.SelectedIndex = Convert.ToInt32(Valor);

                                TableCell8.Controls.Add(ddl);

                                break;

                            case "C":
                                ChkBox chk = new ChkBox();
                                chk.ID = TableAtributosMV.ID + UI.CheckBoxPrefix + j.ToString() + "_" + Identificador + "_" + keys[i].ToString(); ;
                                chk.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChangedTT);
                                if(Valor=="1")
                                    chk.Checked = true;
                                else
                                    chk.Checked = false;
                                TableCell8.CssClass = "checkboxMV";
                                TableCell8.Controls.Add(chk);
                                break;

                            default:
                                break;
                        }
                        TableRow3.Cells.Add(TableCell8);

                        TableCell TableCell9 = new TableCell(); //initialize new table cell
                        TableRow3.Cells.Add(TableCell9);  //add cell to row
                        ImageButton imgbtnII = new ImageButton();
                        imgbtnII.ID = Panel.ID + "_" + "btnElimina" + j.ToString() + "_" + keys[i].ToString() + "_" + Identificador; ;
                        imgbtnII.Click += new ImageClickEventHandler(this.BtnEliminaTT_Click);
                        imgbtnII.CausesValidation = false;
                        imgbtnII.ImageUrl = "Imagenes/Delete.bmp";
                        TableCell9.CssClass = "centrado";
                        TableCell9.Controls.Add(imgbtnII);  // add label to cell
                        TableAtributosMV.Rows.Add(TableRow3);
                        TableRow3 = new TableRow();
                    }
                }
                Panel.Controls.Add(TableAtributosMV);
                Panel.Controls.Add(new LiteralControl("<br />"));
                Panel.Controls.Add(new LiteralControl("<br />"));
            }

        }
        objGlobales.mFlagModal = false;
        objGlobales.mFlagPosPage = false;
        guardarGlobales(objGlobales);
    }



    protected ParameGeneral ParametroElegido(string identificador)
    {
        for (int i = 0; i < objListParamGeneralMV.Count; i++)
        {
            if (objListParamGeneralMV[i].SIdentificador == identificador)
            {
                return objListParamGeneralMV[i];
            }
        }
        return null;
    }


    void limpiar()
    {

        this.txtDescripcion.Text = "";

    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Nombre de Modalidad de Venta
            DataTable dt_NomModalidad = new DataTable();
            dt_NomModalidad = objWBConsultas.ListaCamposxNombre("TipoModalidad");
            objCargaCombo.LlenarCombo(this.ddlTipoModalidad, dt_NomModalidad, "Cod_Campo", "Dsc_Campo", false, false);


            //Carga combo Tipo Ticket
            DataTable dt_tipoTicket = new DataTable();
            dt_tipoTicket = objWBAdministracion.ListaTipoTicket();
            DataTable dest = new DataTable("Result" + dt_tipoTicket.TableName);
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "Cod_Tipo_Ticket";
            dest.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Dsc_Tipo_Ticket";
            dest.Columns.Add(dc);
            DataRow[] foundRowTipoTicket = dt_tipoTicket.Select("Tip_Estado = '1'");

            if (foundRowTipoTicket != null && foundRowTipoTicket.Length > 0)
            {
                for (int i = 0; i < foundRowTipoTicket.Length; i++)
                    dest.Rows.Add(dest.NewRow());

                int j = 0;
                foreach (DataRow r in foundRowTipoTicket) {
                    dest.Rows[j][0] = r["Cod_Tipo_Ticket"].ToString();
                    dest.Rows[j][1] = r["Dsc_Tipo_Ticket_Larga"].ToString();
                    j++;
                }
            }
            objCargaCombo.LlenarCombo(this.ddlTipoTicket, dest, "Cod_Tipo_Ticket", "Dsc_Tipo_Ticket", false, false);
        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

    }



    protected void BtnEliminaMV_Click(object sender, ImageClickEventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        string[] lista;
        ImageButton boton = (ImageButton)sender;
        lista = new string[2];
        lista = Convert.ToString(boton.ID).Split('_');
        string Identificador = Convert.ToString(lista[2]);
        objGlobales.mListaRegistradosMV = EliminaRegistroMV(Identificador, objGlobales.mListaRegistradosMV);
        objGlobales.mFlagPosPage = true;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
        guardarGlobales(objGlobales);
    }

    protected void BtnEliminaTT_Click(object sender, ImageClickEventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();
        string[] lista;
        ImageButton boton = (ImageButton)sender;
        lista = new string[3];
        lista = Convert.ToString(boton.ID).Split('_');
        string tipoticket = Convert.ToString(lista[2]);
        string Identificador = Convert.ToString(lista[3]);
        objGlobales.mListaRegistradosTT = EliminaRegistroTT(tipoticket, Identificador, objGlobales.mListaRegistradosTT);
        objGlobales.mFlagPosPage = true;
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
        guardarGlobales(objGlobales);
    }


    protected ArrayList EliminaRegistroMV(string identificador, ArrayList ListaRegistradosMV)
    {
        ArrayList ListaTemporal = new ArrayList();
        ListaTemporal = ListaRegistradosMV;
        string[] lista;
        for (int i = 0; i < ListaRegistradosMV.Count; i++)
        {
            lista = new string[2];
            lista = Convert.ToString(ListaRegistradosMV[i]).Split('|');
            string IdentRegis = Convert.ToString(lista[0]);

            if (IdentRegis == identificador)
            {
                ListaTemporal.Remove(ListaRegistradosMV[i]);
            }
        }
        return ListaTemporal;
    }

    protected Hashtable EliminaRegistroTT(string tipoticket, string identificador, Hashtable ListaRegistradosTT)
    {
        Hashtable ListaTipoTicket = (Hashtable)ListaRegistradosTT;
        ArrayList ListaTemporal = new ArrayList();
        ArrayList ListaTT = new ArrayList();

        ListaTT = (ArrayList)ListaRegistradosTT[tipoticket];
        ListaTemporal = (ArrayList)ListaRegistradosTT[tipoticket];
        string[] lista;
        for (int i = 0; i < ListaTT.Count; i++)
        {
            lista = new string[2];
            lista = Convert.ToString(ListaTT[i]).Split('|');
            string IdentRegis = Convert.ToString(lista[0]);

            if (IdentRegis == identificador)
            {
                ListaTemporal.Remove(ListaTT[i]);
            }
        }
        ListaTipoTicket[tipoticket] = ListaTemporal;

        return ListaTipoTicket;
    }

    protected void btnAtributosMV_Click(object sender, ImageClickEventArgs e)
    {
        string parametro = "MV";
        this.ModVentaDetalle1.CargarFormulario(parametro, ddlTipoModalidad.SelectedValue);
    }

    protected void btnAtributosTT_Click(object sender, ImageClickEventArgs e)
    {
          if (this.ddlTipoTicket.SelectedValue != null && this.ddlTipoTicket.SelectedValue != "")
          {
                Sglobales objGlobales = obtenerGlobales();
                ArrayList Lista = new ArrayList();

                if (verificarTipoTicket(objGlobales.mListaRegistradosTT, this.ddlTipoTicket.SelectedValue) == false)
                {
                      objGlobales.mListaRegistradosTT.Add(this.ddlTipoTicket.SelectedValue, Lista);
                }
                objGlobales.mFlagPosPage = true;
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
                guardarGlobales(objGlobales);
          }
    }

    protected bool verificarTipoTicket(Hashtable registrados, string Valor)
    {
        string[] keys = new string[registrados.Keys.Count];
        registrados.Keys.CopyTo(keys, 0);

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].ToString() == Valor)
                return true;
        }

        return false;
    }


    protected void btnAgregarTT_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ctrl = (ImageButton)sender;
        string[] lista;
        lista = new string[3];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[2]);
        ModVentaDetalle1.CargarFormulario(Identificador, ddlTipoModalidad.SelectedValue);
    }



    private void TextBox_TextChangedMV(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        TxtBox ctrl = (TxtBox)sender;
        string[] lista;
        lista = new string[2];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string valor = ctrl.Text;
        if (objGlobales.mListaActualesMV[Identificador] != null)
        {
            objGlobales.mListaActualesMV[Identificador] = valor;
        }
        else
        {
            objGlobales.mListaActualesMV.Add(Identificador, valor);
        }

        guardarGlobales(objGlobales);
    }

    private void DropDownList_SelectedIndexChangedMV(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        DropDown ctrl = (DropDown)sender;
        string[] lista;
        lista = new string[2];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string valor = Convert.ToString(ctrl.SelectedIndex);
        if (objGlobales.mListaActualesMV[Identificador] != null)
        {
            objGlobales.mListaActualesMV[Identificador] = valor;
        }
        else
        {
            objGlobales.mListaActualesMV.Add(Identificador, valor);
        }

        guardarGlobales(objGlobales);
    }

    private void CheckBox_CheckedChangedMV(object sender, System.EventArgs e)
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
        if (objGlobales.mListaActualesMV[Identificador] != null)
        {
            objGlobales.mListaActualesMV[Identificador] = valor;
        }
        else
        {
            objGlobales.mListaActualesMV.Add(Identificador, valor);
        }

        guardarGlobales(objGlobales);
    }

    private void TextBox_TextChangedTT(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        TxtBox ctrl = (TxtBox)sender;
        string[] lista;
        lista = new string[3];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string TipoTicket = Convert.ToString(lista[2]);
        string valor = ctrl.Text;
        string clave = TipoTicket + "|" + Identificador;
        if (objGlobales.mListaActualesTT[clave] != null)
        {
            objGlobales.mListaActualesTT[clave] = valor;
        }
        else
        {
            objGlobales.mListaActualesTT.Add(clave, valor);
        }

        guardarGlobales(objGlobales);
    }

    private void DropDownList_SelectedIndexChangedTT(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        DropDown ctrl = (DropDown)sender;
        string[] lista;
        lista = new string[3];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string TipoTicket = Convert.ToString(lista[2]);
        string valor = Convert.ToString(ctrl.SelectedIndex);
        string clave = TipoTicket + "|" + Identificador;
        if (objGlobales.mListaActualesTT[clave] != null)
        {
            objGlobales.mListaActualesTT[clave] = valor;
        }
        else
        {
            objGlobales.mListaActualesTT.Add(clave, valor);
        }

        guardarGlobales(objGlobales);
    }

    private void CheckBox_CheckedChangedTT(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        ChkBox ctrl = (ChkBox)sender;
        string[] lista;
        lista = new string[3];
        lista = ctrl.ID.Split('_');
        string Identificador = Convert.ToString(lista[1]);
        string TipoTicket = Convert.ToString(lista[2]);
        string valor;
        if (ctrl.Checked == true)
        {
            valor = "1";
        }
        else
        {
            valor = "0";
        }
        string clave = TipoTicket + "|" + Identificador;
        if (objGlobales.mListaActualesTT[clave] != null)
        {
            objGlobales.mListaActualesTT[clave] = valor;
        }
        else
        {
            objGlobales.mListaActualesTT.Add(clave, valor);
        }

        guardarGlobales(objGlobales);
    }

    protected void ActualizarValoresActualesMV(Hashtable ListaMV)
    {
        Sglobales objGlobales = obtenerGlobales();
        string[] lista;
        if (ListaMV.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaRegistradosMV.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaRegistradosMV[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                if (ListaMV[Identificador] != null)
                {
                    string valor = Convert.ToString(ListaMV[Identificador]);
                    string parametro = Identificador + "|" + valor;
                    objGlobales.mListaRegistradosMV[i] = parametro;
                }
            }

        }
        guardarGlobales(objGlobales);
    }

    protected void ActualizarValoresActualesTT(Hashtable ListaTT)
    {
        Sglobales objGlobales = obtenerGlobales();
        string[] lista1;
        string[] lista2;

        if (ListaTT.Count > 0)
        {
            string[] keys = new string[ListaTT.Keys.Count];
            ListaTT.Keys.CopyTo(keys, 0);

            for (int i = 0; i < keys.Length; i++)
            {
                lista1 = new string[2];
                lista1 = Convert.ToString(keys[i]).Split('|');
                string TipoTicket = Convert.ToString(lista1[0]);
                string Identificador = Convert.ToString(lista1[1]);
                string Valor = (string)ListaTT[keys[i].ToString()];
                ArrayList ListaSeleccion = new ArrayList();
                Hashtable Parametro = new Hashtable();
                Parametro.Add(Identificador, Valor);
                ListaSeleccion = (ArrayList)objGlobales.mListaRegistradosTT[TipoTicket];

                for (int j = 0; j < ListaSeleccion.Count; j++)
                {
                    lista2 = new string[2];
                    lista2 = Convert.ToString(ListaSeleccion[j]).Split('|');
                    string Identif = Convert.ToString(lista2[0]);
                    if (Parametro[Identif] != null)
                    {
                        string val = Convert.ToString(Parametro[Identif]);
                        string parametro = Identif + "|" + val;
                        ListaSeleccion[j] = parametro;
                    }
                }
            }
        }
        guardarGlobales(objGlobales);
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        ActualizarValoresActualesMV(objGlobales.mListaActualesMV);
        ActualizarValoresActualesTT(objGlobales.mListaActualesTT);

        try
        {
            //Validar que no haya otra modalidad de venta del mismo tipo, solo se puede crear una modalidad para c/u
            //de los tipos, excepto para la modadidad de tipo masiva credito
            if (!validarTipoModalidad())
            {
                if (objWBAdministracion.obtenerModalidadVentaxNombre(this.txtDescripcion.Text.Trim()) == null)
                {
                    if (ValidacionSerie())
                    {
                        if (ValidacionFechaUsoPorVentaMasivaCredito())
                        {
                            ModalidadVenta objModalidadVenta = new ModalidadVenta("", this.txtDescripcion.Text.Trim(), this.ddlTipoModalidad.SelectedValue, "", Convert.ToString(Session["Cod_Usuario"]), "", "");

                            if (objWBAdministracion.insertarModalidadVenta(objModalidadVenta) == true)
                            {
                                string strCodigoModalidadVenta = objWBAdministracion.obtenerModalidadVentaxNombre(this.txtDescripcion.Text.Trim()).SCodModalidadVenta;
                                if (RegistrarAtributos(strCodigoModalidadVenta) == true)
                                {
                                    objGlobales.mMjeError = "";
                                    lblMensajeError.Text = "";
                                    omb.ShowMessage("Modalidad de Venta registrado correctamente", "Creacion de Modalidad de Venta", "Mnt_VerModalidadVenta.aspx");
                                }
                            }
                        }
                        else
                        {
                            this.lblMensajeError.Text = "El atributo de (Por Fecha de Uso) solo puede ser asignado a Tipos de Modalidad de Venta Masiva Credito";
                        }
                    }
                    else
                    {
                        this.lblMensajeError.Text = "Serie de numero de Ticket, rango incorrecto, verifique por favor";
                    }
                }
                else
                {
                    this.lblMensajeError.Text = "La Descripción de Modalidad de Venta ya se encuentra registrado, verifique por favor";
                    objGlobales.mMjeError = "La Descripción de Modalidad de Venta ya se encuentra registrado, verifique por favor";
                }
            }
            else {
                this.lblMensajeError.Text = "No se puede crear la modalidad del tipo escogido, ya se encuentra registrado";
                objGlobales.mMjeError = "No se puede crear la modalidad del tipo escogido, ya se encuentra registrado";
            }
        }
        catch (Exception ex)
        {
            flagError = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            guardarGlobales(objGlobales);
            if (flagError)
                Response.Redirect("PaginaError.aspx");

        }
    }


    //Valida Serie de Inicio y Fin
    protected bool ValidacionSerie()
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        int serieIni = 0;
        int serieFin = 0;
        bool resultEstado = true;
        bool bandexistIni = false;
        bool bandexistFin = false;

        if (objGlobales.mListaRegistradosMV.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaRegistradosMV.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaRegistradosMV[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                string valor = Convert.ToString(lista[1]);

                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                if (objParameGeneral.SIdentificador == "ZE")
                {
                    serieFin = Convert.ToInt32(valor);
                    bandexistIni = true;
                }

                if (objParameGeneral.SIdentificador == "ZD")
                {
                    serieIni = Convert.ToInt32(valor);
                    bandexistFin = true;
                }
            }


            if (bandexistIni && bandexistFin)
            {
                //Evaluamos en el StoreProcedure 0-Valido 1-Invalido
                int valEstado = objWBAdministracion.validarSerieTicket(serieIni, serieFin, "");
                //Respondemos con true o false para generar un mensajito sauu
                if (valEstado == 0)
                {
                    resultEstado = true;
                }
                else
                {
                    resultEstado = false;
                }
            }
            else
            {
                //Necesario registrar los dos parametros
                if (bandexistIni || bandexistFin)
                {
                    resultEstado = false;
                }
                else
                {
                    resultEstado = true;
                }
            }

        }

        guardarGlobales(objGlobales);
        return resultEstado;
    }



    protected bool ValidacionFechaUsoPorVentaMasivaCredito()
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        bool bandex = false;

        if (objGlobales.mListaRegistradosMV.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaRegistradosMV.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaRegistradosMV[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                string valor = Convert.ToString(lista[1]);

                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                if (objParameGeneral.SIdentificador == "ZC")
                {
                    bandex = true;
                }

            }

            if (bandex)
            {
                if (ddlTipoModalidad.SelectedValue == "4")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }

        guardarGlobales(objGlobales);
        return true; ;
    }



    protected bool RegistrarAtributos(string strCodigoModalidadVenta)
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        if (objGlobales.mListaRegistradosMV.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaRegistradosMV.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaRegistradosMV[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                string valor = Convert.ToString(lista[1]);


                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                if(objParameGeneral.STipoParametro=="L")
                {
                    valor = ValueListaCampo(Convert.ToInt32(valor), objParameGeneral.SCampoLista);
                }

                ModalidadAtrib objModalidadAtrib = new ModalidadAtrib(strCodigoModalidadVenta, Identificador, valor, null, "0","", Convert.ToString(Session["Cod_Usuario"]), "");
                if (objWBAdministracion.insertarModVentaAtributo(objModalidadAtrib) == false)
                {
                    guardarGlobales(objGlobales);
                    return false;
                }
            }

        }


        if (objGlobales.mListaRegistradosTT.Count > 0)
        {
            object[] keys = new object[objGlobales.mListaRegistradosTT.Keys.Count];
            objGlobales.mListaRegistradosTT.Keys.CopyTo(keys, 0);


            for (int i = 0; i < keys.Length; i++)
            {
                string TipoTicket = keys[i].ToString();
                ArrayList listaTT = new ArrayList();
                listaTT = (ArrayList)objGlobales.mListaRegistradosTT[TipoTicket];
                for (int j = 0; j < listaTT.Count; j++)
                {
                    lista = new string[2];
                    lista = Convert.ToString(listaTT[j]).Split('|');
                    string Identificador = Convert.ToString(lista[0]);
                    string valor = Convert.ToString(lista[1]);

                    objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                    if (objParameGeneral.STipoParametro == "L")
                    {
                        valor = ValueListaCampo(Convert.ToInt32(valor), objParameGeneral.SCampoLista);
                    }

                    ModalidadAtrib objModalidadAtrib = new ModalidadAtrib(strCodigoModalidadVenta, Identificador, valor, TipoTicket,"1", "", Convert.ToString(Session["Cod_Usuario"]), "");
                    if (objWBAdministracion.insertarModVentaAtributo(objModalidadAtrib) == false)
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

    protected string ValueListaCampo(int IndexCampo, string CampoLista)
    {

        List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();
        objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(CampoLista);

        return objListListaDeCampo[IndexCampo].SCodCampo;
    }

    protected void txtDescripcion_TextChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        TextBox ctrl = (TextBox)sender;
        objGlobales.mDscModalidadVenta = ctrl.Text;
        guardarGlobales(objGlobales);
    }
    protected void ddlTipoModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        DropDownList ctrl = (DropDownList)sender;
        objGlobales.mIndxTipoModalidad = ctrl.SelectedIndex;
        guardarGlobales(objGlobales);
    }

    #region Additional Methods - kinzi
    private bool validarTipoModalidad()
    {
        bool bResponse = false;
        if (this.ddlTipoModalidad.SelectedValue.Trim() == Define.ID_TIPO_MODALIDAD_MASIVA_CREDITO)
            return bResponse;

        string row_filter;
        row_filter = "Tip_Modalidad ='" + this.ddlTipoModalidad.SelectedValue.Trim() + "'";

        DataTable dtModalidad = objWBAdministracion.ListarAllModalidadVenta();
        DataRow[] foundRow = dtModalidad.Select(row_filter);

        if (foundRow != null && foundRow.Length > 0)
        {
            bResponse = true;
        }
        return bResponse;
    }
    #endregion

}
