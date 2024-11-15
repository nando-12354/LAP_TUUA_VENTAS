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

public partial class UserControl_ModVentaDetalle : System.Web.UI.UserControl
{
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    List<ParameGeneral> objListParamGeneralMV = new List<ParameGeneral>();
    bool bandLimitSerieTicket = true;
    bool bandFechaUso = false;

    public void CargarFormulario(string sCodModalidad)
    {
        Sglobales objGlobales = obtenerGlobales();

        //CMontes Agregado para limitar los controles de Limite de Serie de Ticket
        if (sCodModalidad != null && sCodModalidad.Length >= 1)
        {
            string valor = sCodModalidad.Substring(0,1);
            if (valor == "T")
            {
                bandLimitSerieTicket = false;
            }
        }

        if (sCodModalidad != null)
        {
            ViewState["id"] = sCodModalidad;
        }

        objListParamGeneralMV = objWBAdministracion.listarAtributosGenerales();

        /*foreach (ParameGeneral param in objListParamGeneralMV)
        {
           if (param.
            //objListParamGeneralMV.Remove()
        }*/

        if ((string)ViewState["id"] == "MV")
        {
            
            if (objGlobales.mListaRegistradosMV.Count > 0)
            {
                objGlobales.mListaControles = ControlesAtributosMV(ControlesDisponibles(objGlobales.mListaRegistradosMV));
            }
            else
            {
                objGlobales.mListaControles = ControlesAtributosMV(objListParamGeneralMV);
            }
        }
        else
        {
            if (objGlobales.mListaRegistradosTT.Count > 0)
            {
                objGlobales.mListaControles = ControlesAtributosMV(ControlesDisponibles((ArrayList)objGlobales.mListaRegistradosTT[(string)ViewState["id"]]));
            }
            else
            {
                objGlobales.mListaControles = ControlesAtributosMV(objListParamGeneralMV);
            }
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

        this.mpextModalidadVenta.Show();
    }


    public void CargarFormulario(string sCodModalidad, string sIdentificador)
    {
        Sglobales objGlobales = obtenerGlobales();

        //CMontes Agregado para limitar los controles de Limite de Serie de Ticket
        if (sCodModalidad != null && sCodModalidad.Length >= 1)
        {
            string valor = sCodModalidad.Substring(0, 1);
            if (valor == "T")
            {
                bandLimitSerieTicket = false;
            }

            if (sIdentificador == "4")
            {
                bandFechaUso = true;
            }

        }

        if (sCodModalidad != null)
        {
            ViewState["id"] = sCodModalidad;
        }

        objListParamGeneralMV = objWBAdministracion.listarAtributosGenerales();

        if ((string)ViewState["id"] == "MV")
        {
            if (objGlobales.mListaRegistradosMV.Count > 0)
            {
                objGlobales.mListaControles = ControlesAtributosMV(ControlesDisponibles(objGlobales.mListaRegistradosMV));
            }
            else
            {
                objGlobales.mListaControles = ControlesAtributosMV(objListParamGeneralMV);
            }
        }
        else
        {
            if (objGlobales.mListaRegistradosTT.Count > 0)
            {
                objGlobales.mListaControles = ControlesAtributosMV(ControlesDisponibles((ArrayList)objGlobales.mListaRegistradosTT[(string)ViewState["id"]]));
            }
            else
            {
                objGlobales.mListaControles = ControlesAtributosMV(objListParamGeneralMV);
            }
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

        this.mpextModalidadVenta.Show();
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
            ArrayList ListaNueva = new ArrayList();
            objGlobales.mListaSeleccionados = ListaNueva;
            objGlobales.mListaControles = ListaNueva;
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


    protected List<ParameGeneral> ControlesDisponibles(ArrayList ListaControl)
    {
        List<ParameGeneral> ListaDisponible = new List<ParameGeneral>();
        string[] lista;


        for (int i = 0; i < objListParamGeneralMV.Count; i++)
        {
            bool encontrado = false;
            for (int j = 0; j < ListaControl.Count; j++)
            {
                lista = new string[2];
                lista = Convert.ToString(ListaControl[j]).Split('|');
                string Identificador = Convert.ToString(lista[0]); ;
                string Valor = Convert.ToString(lista[1]);

                if (objListParamGeneralMV[i].SIdentificador == Identificador)
                {
                    encontrado = true;
                }
            }
            if (encontrado == false)
            {
                ListaDisponible.Add(objListParamGeneralMV[i]);
            }
        }

        return ListaDisponible;
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

    private ArrayList ControlesAtributosMV(List<ParameGeneral> objListParamGeneralMV)
    {
        ArrayList controllist = new ArrayList();

        int contTableRows = objListParamGeneralMV.Count;

        for (int i = 0; i <= contTableRows - 1; i++)
        {

            //CMontes Agregado para no permitir el ingreso de Limite de Ticket para los atributos de Tickets
            if (!((objListParamGeneralMV[i].SIdentificador == "ZD" || objListParamGeneralMV[i].SIdentificador == "ZE") && !bandLimitSerieTicket))
            {

                if (!((objListParamGeneralMV[i].SIdentificador == "ZC") && !bandFechaUso))
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


                    if (objListParamGeneralMV[i].STipoParametro == "C")
                    {
                        ChkBox chk = new ChkBox(); //initlaize new checkbox
                        chk.ID = UI.CheckBoxPrefix + i.ToString(); //set ID property 
                        chk.ID = "chkSPX" + i; //set ID property 
                        chk.CssClass = "checkboxMV";
                        chk.Width = 60;  // set width property
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
                            txt.Width = 60;  // set width property
                            txt.CssClass = "textboxMV"; //set css class
                            if (objListParamGeneralMV[i].STipoValor == "NUM")
                            {

                                txt.Attributes.Add("onkeypress", "JavaScript: Tecla('Double');");
                                txt.Attributes.Add("onblur", "val_int(this)");

                                if (objListParamGeneralMV[i].SIdentificador == "ZD" || objListParamGeneralMV[i].SIdentificador == "ZE")
                                {
                                    txt.MaxLength = 3;
                                    //txt.Attributes.Add("onkeypress", "JavaScript: valLength(this);");
                                    //txt.Attributes.Add("onblur", "valLength(this)");
                                }

                            }
                            else
                            {
                                txt.Attributes.Add("onkeypress", "JavaScript: Tecla('Character');");
                                txt.Attributes.Add("onblur", "gDescripcionNombre(this)");
                            }


                            txt.Text = objListParamGeneralMV[i].SValor;
                            controllist.Add(txt); // add textbox to cell

                        }

                    }

                    ChkBox chkI = new ChkBox(); //initlaize new checkbox
                    chkI.ID = UI.CheckBoxPrefix + "I" + i.ToString(); //set ID property 
                    chkI.Width = 60;  // set width property
                    controllist.Add(chkI); // add textbox to cell                
                }
            }
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

        if ((string)ViewState["id"] == "MV")
        {
            if (objGlobales.mListaSeleccionados.Count > 0)
            {
                if (objGlobales.mListaRegistradosMV.Count > 0)
                {

                    objGlobales.mListaRegistradosMV = ActualizarRegistrados(objGlobales.mListaRegistradosMV, objGlobales.mListaSeleccionados);
                    if (objGlobales.mListaEliminadosMV.Count > 0)
                    {
                        objGlobales.mListaEliminadosMV = ActualizarEliminadosMV(objGlobales.mListaEliminadosMV, objGlobales.mListaRegistradosMV);
                    }
                }
                else
                {
                    objGlobales.mListaRegistradosMV = objGlobales.mListaSeleccionados;
                    if (objGlobales.mListaEliminadosMV.Count > 0)
                    {
                        objGlobales.mListaEliminadosMV = ActualizarEliminadosMV(objGlobales.mListaEliminadosMV, objGlobales.mListaRegistradosMV);
                    }
                }
            }
        }
        else
        {
            if (objGlobales.mListaSeleccionados.Count > 0)
            {
                ArrayList Lista = new ArrayList();
                Lista = (ArrayList)objGlobales.mListaRegistradosTT[(string)ViewState["id"]];

                if (Lista != null)
                {
                    if (Lista.Count > 0)
                    {
                        objGlobales.mListaRegistradosTT[(string)ViewState["id"]] = ActualizarRegistrados((ArrayList)objGlobales.mListaRegistradosTT[(string)ViewState["id"]], objGlobales.mListaSeleccionados);
                        if (objGlobales.mListaEliminadosTT.Count > 0)
                        {
                            objGlobales.mListaEliminadosTT = ActualizarEliminadosTT(objGlobales.mListaEliminadosTT, objGlobales.mListaRegistradosTT);
                        }
                    }
                    else
                    {
                        objGlobales.mListaRegistradosTT[(string)ViewState["id"]] = objGlobales.mListaSeleccionados;
                        if (objGlobales.mListaEliminadosTT.Count > 0)
                        {
                            objGlobales.mListaEliminadosTT = ActualizarEliminadosTT(objGlobales.mListaEliminadosTT, objGlobales.mListaRegistradosTT);
                        }
                    }
                }
            }
        }

        objGlobales.mFlagModal = true;
        objGlobales.mFlagPosPage = true;
        guardarGlobales(objGlobales);
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
        //string strStartUpScript = "<script language='javascript' type='text/javascript'>CloseFormOK();</script>";
        //ClientScript.RegisterStartupScript(Page.GetType(), "PopupScript", strStartUpScript);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {

    }

    protected ArrayList ActualizarEliminadosMV(ArrayList ListaEliminadosMV, ArrayList ListaRegistradosMV)
    {
        string[] lista;
        bool encontrado = false;
        for (int i = 0; i < ListaRegistradosMV.Count; i++)
        {
            lista = new string[2];
            lista = Convert.ToString(ListaRegistradosMV[i]).Split('|');
            string IdentRegis = Convert.ToString(lista[0]);

            for (int j = 0; j < ListaEliminadosMV.Count; j++)
            {
                if (IdentRegis == (string)ListaEliminadosMV[j])
                {
                    encontrado = true;
                }
            }
            if (encontrado == true)
            {
                ListaEliminadosMV.Remove(IdentRegis);
            }
        }

        return ListaEliminadosMV;
    }

    protected Hashtable ActualizarEliminadosTT(Hashtable ListaEliminadosTT, Hashtable ListaRegistradosTT)
    {
        ArrayList RegistradosTT;
        ArrayList EliminadosTT;
        string[] lista;

        object[] keys = new object[ListaRegistradosTT.Keys.Count];
        ListaRegistradosTT.Keys.CopyTo(keys, 0);

        for (int i = 0; i < keys.Length; i++)
        {
            if (ListaEliminadosTT[keys[i].ToString()] != null)
            {
                RegistradosTT = new ArrayList();
                EliminadosTT = new ArrayList();
                RegistradosTT = (ArrayList)ListaRegistradosTT[keys[i].ToString()];
                EliminadosTT = (ArrayList)ListaEliminadosTT[keys[i].ToString()];

                for (int j = 0; j < RegistradosTT.Count; j++)
                {
                    lista = new string[2];
                    lista = Convert.ToString(RegistradosTT[j]).Split('|');
                    string IdentRegis = Convert.ToString(lista[0]);

                    bool encontrado = false;
                    for (int k = 0; k < EliminadosTT.Count; k++)
                    {
                        if (IdentRegis == (string)EliminadosTT[k])
                        {
                            encontrado = true;
                        }
                    }
                    if (encontrado == true)
                    {
                        EliminadosTT.Remove(IdentRegis);
                        ListaEliminadosTT[keys[i].ToString()] = EliminadosTT;
                    }
                }
            }
        }

        return ListaEliminadosTT;
    }



    protected ArrayList ActualizarRegistrados(ArrayList Lista1, ArrayList Lista2)
    {
        for (int i = 0; i < Lista2.Count; i++)
        {
            Lista1.Add(Lista2[i]);
        }

        return Lista1;
    }
}
