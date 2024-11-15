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

public partial class UserControl_CompDetalle : System.Web.UI.UserControl
{
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    BO_Configuracion objWBConfiguracion = new BO_Configuracion();
    BO_Operacion objWBOperacion = new BO_Operacion();
    List<ParameGeneral> objListParamGeneralMV = new List<ParameGeneral>();
    List<ModalidadAtrib> objListModalidadAtrib = new List<ModalidadAtrib>();
    string lsCompania;
    string lsModalidad;


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



    public void CargarFormulario(string sCodModalidad, string sCodCompania)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (sCodModalidad != null)
        {
            ViewState["id"] = sCodModalidad;
        }

        if (sCodCompania != null)
        {
            ViewState["comp"] = sCodCompania;
        }

        lsCompania = sCodCompania;
        lsModalidad = sCodModalidad;

        lblMensajeError.Text = "";

        objListModalidadAtrib = objWBOperacion.ListarAtributosxModVenta(Convert.ToString(ViewState["id"]));

        if (sCodModalidad == "M0002")
        {
            List<ModalidadAtrib> objListModalidadAtribBCBP = new List<ModalidadAtrib>();
            objListModalidadAtribBCBP = objWBOperacion.ListarAtributosxModVenta("M0100");
            foreach (ModalidadAtrib atrib in objListModalidadAtribBCBP)
            {
                objListModalidadAtrib.Add(atrib);
            }
        }

        ArrayList Lista = new ArrayList();
        Lista = (ArrayList)objGlobales.mMVRegistrados[Convert.ToString(ViewState["id"])];

        if (Lista != null)
        {
            if (Lista.Count > 0)
            {
                objGlobales.mListaControles = ControlesAtributosMV(ControlesDisponibles((Lista)), Lista);
            }
            else
            {
                ArrayList ListaNueva = new ArrayList();
                objGlobales.mListaControles = ControlesAtributosMV(CargarAtributos(objListModalidadAtrib), ListaNueva);

            }
        }
        else
        {
            ArrayList ListaNueva = new ArrayList();
            objGlobales.mListaControles = ControlesAtributosMV(CargarAtributos(objListModalidadAtrib), ListaNueva);
        }

        WireUpEvents(objGlobales.mListaControles);
        Table TableAtributosMV = new Table();
        TableAtributosMV.CssClass = "TablaModalPopUpMV";
        creaCabeceraPoup(TableAtributosMV);
        BuildHtmlTableWithControls(objGlobales.mListaControles, TableAtributosMV);
        this.UpdatePanel1.ContentTemplateContainer.Controls.Clear();
        this.UpdatePanel1.ContentTemplateContainer.Controls.Add(TableAtributosMV);
        this.UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<br />"));
        this.UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<br />"));

        guardarGlobales(objGlobales);

        mpextCompania.Show();
    }



    private void Page_Init(object sender, System.EventArgs e)
    {
        Sglobales objGlobales = obtenerGlobales();

        if (!Page.IsPostBack)
        {
            objGlobales.mListaSeleccionados = new ArrayList();
            objGlobales.mListaControles = new ArrayList();
        }

        if (objGlobales.mListaControles.Count > 0)
        {
            WireUpEvents(objGlobales.mListaControles);
            Table TableAtributosMV = new Table();
            TableAtributosMV.CssClass = "TablaModalPopUpMV";
            creaCabeceraPoup(TableAtributosMV);
            BuildHtmlTableWithControls(objGlobales.mListaControles, TableAtributosMV);
            this.UpdatePanel1.ContentTemplateContainer.Controls.Clear();
            this.UpdatePanel1.ContentTemplateContainer.Controls.Add(TableAtributosMV);
            this.UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<br />"));
            this.UpdatePanel1.ContentTemplateContainer.Controls.Add(new LiteralControl("<br />"));
        }

        guardarGlobales(objGlobales);

    }


    protected List<ParameGeneral> CargarAtributos(List<ModalidadAtrib> objListModalidadAtrib)
    {
        List<ParameGeneral> ListaParametros = new List<ParameGeneral>();
        for (int i = 0; i < objListModalidadAtrib.Count; i++)
        {
            ModalidadAtrib objModalidadAtrib = new ModalidadAtrib();
            objModalidadAtrib = objListModalidadAtrib[i];
            if (objModalidadAtrib.Tip_Atributo.Trim() == "0")
            {
                ParameGeneral objParameGeneral = new ParameGeneral();
                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(objModalidadAtrib.SCodAtributo);
                ListaParametros.Add(objParameGeneral);
            }
        }
        return ListaParametros;
    }


    protected void creaCabeceraPoup(Table Tabla)
    {
        Tabla.EnableViewState = true;
        //      Tabla.Width = 400; //set width
        Tabla.CellPadding = 4; //set padding
        Tabla.BorderWidth = 1;
        Tabla.CssClass = "TablaModalPopUpMV"; //css class for entire table
        //header row
        TableRow TableRow1 = new TableRow(); //initialize row for header
        Tabla.Rows.Add(TableRow1); //add row to table
        TableCell TableCell1 = new TableCell(); //initialize table cell
        TableCell1.ColumnSpan = 4; //set column span to 3
        TableCell1.CssClass = "titulopopupMV"; //assign css class for cell
        TableRow1.Cells.Add(TableCell1); // add cell to row
        MyLabel Label1 = new MyLabel(); //initialize new label
        Label1.Text = "Listado de Atributos Disponibles:"; //label text
        TableCell1.Controls.Add(Label1); //add label to cell

        //row 2
        TableRow TableRow2 = new TableRow(); //initialize new row
        Tabla.Rows.Add(TableRow2); //add row to table

        TableCell TableCell2x = new TableCell(); //initialize new cell
        TableRow2.Cells.Add(TableCell2x); //add cell to row
        MyLabel Label2x = new MyLabel(); //initialize new label
        Label2x.Text = "Identificador"; //set text property fopr label
        Label2x.Visible = false;
        TableCell2x.Controls.Add(Label2x); //add label to cell


        TableCell TableCell2 = new TableCell(); //initialize new cell
        TableRow2.Cells.Add(TableCell2); //add cell to row
        MyLabel Label2 = new MyLabel(); //initialize new label
        Label2.Text = "Num"; //set text property fopr label
        Label2.CssClass = "cabeceratablaMV"; //set css class for label
        TableCell2.Controls.Add(Label2); //add label to cell

        TableCell TableCell3 = new TableCell(); // initialize new cell
        TableRow2.Cells.Add(TableCell3);  //add cell to row
        MyLabel Label3 = new MyLabel(); //initialize new label
        Label3.Text = "Atributo: "; //set text property for label
        Label3.CssClass = "cabeceratablaMV"; //set css class for label
        TableCell3.Controls.Add(Label3); //add label to row
        TableCell TableCell4 = new TableCell(); //initialize new table cell
        TableRow2.Cells.Add(TableCell4);  //add cell to row
        MyLabel Label4 = new MyLabel(); //initialize new label
        Label4.Text = "Valor: "; //set text property 
        Label4.CssClass = "cabeceratablaMV"; // set css class
        TableCell4.Controls.Add(Label4);  // add label to cell

        TableCell TableCellGer = new TableCell(); //initialize new table cell
        TableRow2.Cells.Add(TableCellGer);  //add cell to row
        MyLabel LabelGer = new MyLabel(); //initialize new label
        LabelGer.Text = "Seleccionar: "; //set text property 
        LabelGer.CssClass = "cabeceratablaMV"; // set css class
        TableCellGer.Controls.Add(LabelGer);  // add label to cell
    }


    protected List<ParameGeneral> ControlesDisponibles(ArrayList ListaControl)
    {
        List<ParameGeneral> ListaDisponible = new List<ParameGeneral>();
        string[] lista;

        for (int i = 0; i < objListModalidadAtrib.Count; i++)
        {

            ModalidadAtrib objModalidadAtrib = new ModalidadAtrib();
            objModalidadAtrib = objListModalidadAtrib[i];
            if (objModalidadAtrib.Tip_Atributo.Trim() == "0")
            {
                ParameGeneral objParameGeneral = new ParameGeneral();
                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(objModalidadAtrib.SCodAtributo);

                for (int j = 0; j < ListaControl.Count; j++)
                {
                    lista = new string[2];
                    lista = Convert.ToString(ListaControl[j]).Split('|');
                    string Identificador = Convert.ToString(lista[0]); ;
                    string Valor = Convert.ToString(lista[1]);

                    if (objModalidadAtrib.SCodAtributo == Identificador)
                    {
                        objParameGeneral.SValor = Valor;
                    }
                }
                ListaDisponible.Add(objParameGeneral);
            }
        }

        return ListaDisponible;
    }

    private ArrayList ControlesAtributosMV(List<ParameGeneral> objListParamGeneralMV, ArrayList ListaControl)
    {
        ArrayList controllist = new ArrayList();

        int contTableRows = objListParamGeneralMV.Count;

        for (int i = 0; i <= contTableRows - 1; i++)
        {

            //row3
            MyLabel lblI = new MyLabel(); //initialize new label
            lblI.ID = UI.LabelPrefix + "I" + i.ToString(); //set ID property 
            lblI.Text = objListParamGeneralMV[i].SIdentificador; //set text property for label
            lblI.Visible = false;
            controllist.Add(lblI); //add label to 

            MyLabel lblII = new MyLabel(); //initialize new label
            lblII.ID = UI.LabelPrefix + "II" + i.ToString(); //set ID property 
            lblII.Text = Convert.ToString(i + 1); //set text property for label
            lblII.CssClass = "itemtablaMV"; //set css class for label
            controllist.Add(lblII); //add label to 

            MyLabel lblIII = new MyLabel(); //initialize new label
            lblIII.ID = UI.LabelPrefix + "III" + i.ToString(); //set ID property 
            lblIII.Text = objListParamGeneralMV[i].SDescripcion; //set text property for label
            lblIII.CssClass = "itemtablaMV"; //set css class for label
            controllist.Add(lblIII); //add label to cell


            if ((objListParamGeneralMV[i].STipoParametro == "C")||((objListParamGeneralMV[i].STipoParametro == "#") && (objListParamGeneralMV[i].STipoValor == "NUM") ) )
            {
                ChkBox chk = new ChkBox(); //initlaize new checkbox
                chk.ID = UI.CheckBoxPrefix + i.ToString(); //set ID property 
                chk.ID = "chkSPX" + i; //set ID property 
                chk.CssClass = "checkboxMV";
                chk.Width = 60;

                if (objListParamGeneralMV[i].SValor == "1")
                {
                    chk.Checked = true;
                }
                controllist.Add(chk); // add textbox to cell

            }
            else
            {
                if (objListParamGeneralMV[i].STipoParametro == "L")
                {
                    DropDown dd = new DropDown(); //initlaize new checkbox
                    dd.ID = UI.DropDownPrefix + i.ToString(); //set ID property 
                    dd.Width = 60;  // set width property
                    dd.CssClass = "combo"; //set css class

                    if (dd.Items.Count <= 0)
                    {
                        List<ListaDeCampo> objListListaDeCampo = new List<ListaDeCampo>();

                        objListListaDeCampo = objWBAdministracion.obtenerListadeCampo(objListParamGeneralMV[i].SCampoLista);

                        for (int j = 0; j < objListListaDeCampo.Count; j++)
                        {
                            dd.Items.Add(objListListaDeCampo[j].SDscCampo);
                        }
                    }

                    controllist.Add(dd); // add textbox to cell

                }
                else
                {
                    TxtBox txt = new TxtBox(); //initlaize new textbox
                    txt.ID = UI.TextBoxPrefix + i.ToString(); //set ID property 
                    if (lsModalidad == "M0002") txt.Width = 300;  // set width property
                    else txt.Width = 60; 
                    txt.CssClass = "textboxMV"; //set css class
                    if (objListParamGeneralMV[i].STipoValor == "NUM")
                    {
                        txt.MaxLength = 3;
                        txt.Attributes.Add("onkeypress", "JavaScript: Tecla('Double');");
                        txt.Attributes.Add("onblur", "val_int(this)");
                    }
                    else
                    {
                        txt.Attributes.Add("onkeypress", "JavaScript: Tecla('NumeroyLetra');");
                        //txt.Attributes.Add("onblur", "gDescripcionNombre(this)");
                    }
                    txt.Text = objListParamGeneralMV[i].SValor;
                    controllist.Add(txt); // add textbox to cell

                }

            }

            ChkBox chkI = new ChkBox(); //initlaize new checkbox
            chkI.ID = UI.CheckBoxPrefix + "I" + i.ToString(); //set ID property 
            chkI.Width = 60;  // set width property

            string[] lista;
            if (ListaControl.Count > 0)
            {
                for (int j = 0; j < ListaControl.Count; j++)
                {
                    lista = new string[2];
                    lista = Convert.ToString(ListaControl[j]).Split('|');
                    string Identificador = Convert.ToString(lista[0]); ;
                    string Valor = Convert.ToString(lista[1]);

                    if (objListParamGeneralMV[i].SIdentificador == Identificador)
                    {
                        chkI.Checked = true;
                    }
                }
            }
            controllist.Add(chkI); // add textbox to cell
        }

        return controllist;

    }


    private ArrayList ControlesAtributosAsignadosMV(ArrayList controlList)
    {
        ArrayList asignados = new ArrayList();
        ArrayList temporal = new ArrayList();
        int cols = 5;
        string valor = "";
        string identificador = "";

        for (int i = 0; i < controlList.Count; i++)
        {
            switch (controlList[i].GetType().FullName)
            {
                case "TxtBox":
                    TxtBox textbox = new TxtBox();
                    textbox = (TxtBox)controlList[i];
                    valor = textbox.Text;
                    break;
                case "DropDown":
                    DropDown ddl = new DropDown();
                    ddl = (DropDown)controlList[i];
                    valor = ddl.SelectedIndex.ToString();
                    break;
                case "ChkBox":
                    ChkBox chk = new ChkBox();
                    chk = (ChkBox)controlList[i];
                    if ((i + 1) % cols == 0)
                    {
                        if (chk.Checked == true)
                        {
                            asignados.Add(identificador + "|" + valor);
                        }

                    }
                    else
                    {
                        if (chk.Checked == true)
                        {
                            valor = "1";
                        }
                        else
                        {
                            valor = "0";
                        }
                    }
                    break;
                case "MyLabel":
                    MyLabel label = new MyLabel();
                    label = (MyLabel)controlList[i];
                    if ((i == 0) || (i % cols == 0))
                    {
                        identificador = label.Text;
                    }
                    break;
                default:
                    break;
            }
        }
        return asignados;
    }



    #region Build Html Table With Controls
    private void BuildHtmlTableWithControls(ArrayList controlList, Table Tabla)
    {
        TableRow trow;
        TableCell tcell;
        int cols = 5;

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
    #endregion


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
        ChkBox ctrl = (ChkBox)sender;
    }


    protected void btnAceptar_Click(object sender, EventArgs e)
    {

        Sglobales objGlobales = obtenerGlobales();

        objGlobales.mListaSeleccionados = ControlesAtributosAsignadosMV(objGlobales.mListaControles);

        bool wsValido=false;
        if (objGlobales.mListaSeleccionados.Count > 0)
        {
            //Ejecutar la validacion de Limites
            if (ValidacionSerie((String)ViewState["comp"], (String)ViewState["id"]))
            {
                //Validacion de WS en caso de BCBP
                if ((String)ViewState["id"] == "M0002")
                    wsValido = ValidacionWS();
                else
                    wsValido = true;
                   

                if (wsValido)
                {
                    if (objGlobales.mMVRegistrados.Count > 0)
                    {
                        objGlobales.mMVEliminados = ActualizarEliminados(objGlobales.mMVRegistrados, objGlobales.mListaSeleccionados, (string)ViewState["id"]);
                    }
                    objGlobales.mMVRegistrados[(string)ViewState["id"]] = objGlobales.mListaSeleccionados;

                    objGlobales.mFlagModal = true;
                    objGlobales.mFlagPosPage = false;
                }
                else
                {
                    guardarGlobales(objGlobales);
                    lblMensajeError.Text = "Debe seleccionar e ingresar todos los atributos del Web Service (6-10)";
                    mpextCompania.Show();
                }
            }
            else
            {
                guardarGlobales(objGlobales);
                lblMensajeError.Text = "Rango de serie de ticket incorrecto, revise por favor";
                mpextCompania.Show();
            }
        }
        else
        {
            if (objGlobales.mMVRegistrados.Count > 0)
            {
                objGlobales.mMVEliminados = ActualizarEliminados(objGlobales.mMVRegistrados, objGlobales.mListaSeleccionados, (string)ViewState["id"]);
                objGlobales.mMVRegistrados[(string)ViewState["id"]] = objGlobales.mListaSeleccionados;
            }

            objGlobales.mFlagModal = true;
            objGlobales.mFlagPosPage = false;
            //mpextCompania.Hide();
        }

        guardarGlobales(objGlobales);

        //string strStartUpScript = "<script language='javascript' type='text/javascript'>CloseFormOK();</script>";
        //ClientScript.RegisterStartupScript(Page.GetType(), "PopupScript", strStartUpScript);
    }

    

    //Valida WS en caso de BCBP
    protected bool ValidacionWS()
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        bool resultEstado = true;
        bool bandexistWS = false;
        bool bandexistValidarBP = false;
        string nomServicio = "", nomMetodo = "", protocolo = "", rutaWS = "";

        if (objGlobales.mListaSeleccionados.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaSeleccionados.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaSeleccionados[i]).Split('|');
                string Identificador = Convert.ToString(lista[0]);
                string valor = Convert.ToString(lista[1]);

                objParameGeneral = objWBConfiguracion.obtenerParametroGeneral(Identificador);

                if (objParameGeneral.SIdentificador == "W3") { bandexistWS = true; bandexistValidarBP = true; }
                //Recuperamos descripciones del WS
                else if (objParameGeneral.SIdentificador == "W4") { nomServicio = valor; bandexistWS = true; }
                else if (objParameGeneral.SIdentificador == "W5") { nomMetodo = valor; bandexistWS = true; }
                else if (objParameGeneral.SIdentificador == "W6") { protocolo = valor; bandexistWS = true; }
                else if (objParameGeneral.SIdentificador == "W7") { rutaWS = valor; bandexistWS = true; }
            }

            if (bandexistWS)
            {
                if (nomServicio != "" && nomMetodo != "" && protocolo != "" && rutaWS != "" && bandexistValidarBP)
                    resultEstado = true;
                else
                    resultEstado = false;
            }
        }

        guardarGlobales(objGlobales);

        return resultEstado;
    }

    //Valida Serie de Inicio y Fin
    protected bool ValidacionSerie(string compania, string modalidad)
    {
        Sglobales objGlobales = obtenerGlobales();

        ParameGeneral objParameGeneral = new ParameGeneral();
        string[] lista;
        int serieIni = 0;
        int serieFin = 0;
        bool resultEstado = true;
        bool bandexistIni = false;
        bool bandexistFin = false;

        if (objGlobales.mListaSeleccionados.Count > 0)
        {
            for (int i = 0; i < objGlobales.mListaSeleccionados.Count; i++)
            {
                lista = new string[2];
                lista = Convert.ToString(objGlobales.mListaSeleccionados[i]).Split('|');
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
                int valEstado = objWBAdministracion.validarSerieTicketCompa(serieIni, serieFin, modalidad, compania);
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


    protected Hashtable ActualizarEliminados(Hashtable ListaRegistrados, ArrayList ListaSeleccionados, string sModalidadVenta)
    {

        Sglobales objGlobales = obtenerGlobales();

        ArrayList RegistradosMV = new ArrayList();
        ArrayList EliminadosMV = new ArrayList();
        Hashtable ListaEliminadosMV = new Hashtable();
        ListaEliminadosMV = objGlobales.mMVEliminados;
        string[] lista;
        string[] lista2;

        RegistradosMV = (ArrayList)ListaRegistrados[sModalidadVenta];

        if (RegistradosMV != null)
        {
            for (int j = 0; j < RegistradosMV.Count; j++)
            {
                lista = new string[2];
                lista = Convert.ToString(RegistradosMV[j]).Split('|');
                string IdentRegis = Convert.ToString(lista[0]);

                bool encontrado = false;
                for (int k = 0; k < ListaSeleccionados.Count; k++)
                {
                    lista2 = new string[2];
                    lista2 = Convert.ToString(ListaSeleccionados[k]).Split('|');
                    string IdentEl = Convert.ToString(lista2[0]);

                    if (IdentRegis == IdentEl)
                    {
                        encontrado = true;
                    }
                }
                if (encontrado == false)
                {
                    EliminadosMV.Add(IdentRegis);

                    if (ListaEliminadosMV[sModalidadVenta] != null)
                        ListaEliminadosMV[sModalidadVenta] = EliminadosMV;
                    else
                        ListaEliminadosMV.Add(sModalidadVenta, EliminadosMV);
                }
            }
        }

        guardarGlobales(objGlobales);
        return ListaEliminadosMV;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //mpextCompania.Show();
    }
}
