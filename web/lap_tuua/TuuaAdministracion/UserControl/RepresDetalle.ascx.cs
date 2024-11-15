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

public partial class UserControl_RepresDetalle : System.Web.UI.UserControl
{
    protected Hashtable htLabels;
    BO_Consultas objWBConsultas = new BO_Consultas();
    BO_Administracion objWBAdministracion = new BO_Administracion();
    List<ListaDeCampo> objListListadeCampo = new List<ListaDeCampo>();
    UIControles objCargaCombo = new UIControles();

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


    public void CargarFormulario(string sCodRepresentante)
    {

        Sglobales objGlobales = obtenerGlobales();

        ViewState["id"] = sCodRepresentante;
        CargarCombos();
        objListListadeCampo = objWBAdministracion.obtenerListadeCampo("PermRepresentante");
        if ((string)ViewState["id"] == "AD")
        {
            limpiar();
            objGlobales.mRListaControles = ControlesPermisos(objListListadeCampo, 0);
        }
        else
        {
            PoblarRepresentante();
        }
        htLabels = LabelConfig.htLabels;
        if ((string)ViewState["id"] == "AD")
        {
            this.lblTituloRepresentante.Text = htLabels["mrepresentante.lblTituloCrearRepresentante"].ToString();
            this.lblCodigo.Visible = false;
            this.lblDscCodigo.Visible = false;
            this.lblEstado.Visible = false;
            this.ddlEstado.Visible = false;
        }
        else
        {
            this.lblTituloRepresentante.Text = htLabels["mrepresentante.lblTituloActualizarRepresentante"].ToString();

        }

        WireUpEvents(objGlobales.mRListaControles);
        Table TablePermisos = new Table();
        //TablePermisos.CssClass = "TablaModalPermisos";
        BuildHtmlTableWithControls(objGlobales.mRListaControles, TablePermisos);
        this.pnlPermisos.Controls.Clear();
        this.pnlPermisos.Controls.Add(TablePermisos);
        this.pnlPermisos.Controls.Add(new LiteralControl("<br />"));
        this.pnlPermisos.Controls.Add(new LiteralControl("<br />"));

        guardarGlobales(objGlobales);
        mpextReprsentante.Show();
    }

    private void limpiar()
    {
        txtNombre.Text = "";
        txtApellido.Text = "";
        txtCargo.Text = "";
        txtNumDocumento.Text = "";
    }


    private void PoblarRepresentante()
    {
        Sglobales objGlobales = obtenerGlobales();

        if (objGlobales.mRRegistrados.Count > 0)
        {
            RepresentantCia objRepresentantCia = new RepresentantCia();
            objRepresentantCia = (RepresentantCia)objGlobales.mRRegistrados[Convert.ToInt32(ViewState["id"])];

            if (objRepresentantCia != null)
            {
                this.lblDscCodigo.Text = Convert.ToString(objRepresentantCia.INumSecuencial);
                txtNombre.Text = objRepresentantCia.SNomRepresentante;
                txtApellido.Text = objRepresentantCia.SApeRepresentante;
                ViewState["Nombre"] = objRepresentantCia.SNomRepresentante + objRepresentantCia.SApeRepresentante;
                txtCargo.Text = objRepresentantCia.SCargoRepresentante;
                txtNumDocumento.Text = objRepresentantCia.SNDocRepresentante;
                ddlEstado.SelectedIndex = IndexListaCampo(objRepresentantCia.STipEstado, ddlEstado);
                ddlTipoDocumento.SelectedIndex = IndexListaCampo(objRepresentantCia.STDocRepresentante, ddlTipoDocumento);
                ViewState["Documento"] = objRepresentantCia.STDocRepresentante + objRepresentantCia.SNDocRepresentante;
                objGlobales.mRListaControles = ControlesPermisos(objListListadeCampo, Convert.ToInt32(objRepresentantCia.SPermRepresentante));
            }
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


    private void Page_Init(object sender, System.EventArgs e)
    {

        Sglobales objGlobales = obtenerGlobales();
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;

            try
            {
                this.lblCodigo.Text = htLabels["mrepresentante.lblCodigo"].ToString();
                this.lblNombre.Text = htLabels["mrepresentante.lblNombre"].ToString();
                this.lblApellido.Text = htLabels["mrepresentante.lblApellido"].ToString();
                this.lblCargo.Text = htLabels["mrepresentante.lblCargo"].ToString();
                this.lblNumDocumento.Text = htLabels["mrepresentante.lblNumDocumento"].ToString();
                this.lblTipoDocumento.Text = htLabels["mrepresentante.lblTipoDocumento"].ToString();
                this.lblEstado.Text = htLabels["mrepresentante.lblEstado"].ToString();
                this.btnAceptar.Text = htLabels["mrepresentante.btnAceptar"].ToString();
                this.btnCerrarPopup.Text = htLabels["mrepresentante.btnCancelar"].ToString();
                this.lblPermisosRepresentante.Text = htLabels["mrepresentante.lblPermisosRepresentante"].ToString();
                this.rfvCargo.ErrorMessage = htLabels["mrepresentante.rfvCargo"].ToString();
                this.rfvApellido.ErrorMessage = htLabels["mrepresentante.rfvApellido"].ToString();
                this.rfvNombre.ErrorMessage = htLabels["mrepresentante.rfvNombre"].ToString();
                this.rfvNumdocumento.ErrorMessage = htLabels["mrepresentante.rfvNumdocumento"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegCrearUsuario;
                Response.Redirect("PaginaError.aspx");
            }

            ArrayList ListaNueva = new ArrayList();
            objGlobales.mRListaControles = ListaNueva;
        }

        if (objGlobales.mRListaControles.Count > 0)
        {
            WireUpEvents(objGlobales.mRListaControles);
            Table TablePermisos = new Table();
            //TablePermisos.CssClass = "TablaModalPermisos";
            BuildHtmlTableWithControls(objGlobales.mRListaControles, TablePermisos);
            this.pnlPermisos.Controls.Clear();
            this.pnlPermisos.Controls.Add(TablePermisos);
            this.pnlPermisos.Controls.Add(new LiteralControl("<br />"));
            this.pnlPermisos.Controls.Add(new LiteralControl("<br />"));
        }

        guardarGlobales(objGlobales);

    }
    public void CargarCombos()
    {
        try
        {
            //Carga combo Tipo de Documento
            DataTable dt_TipoDocumento = new DataTable();
            dt_TipoDocumento = objWBConsultas.ListaCamposxNombre("TipDocPersona");
            objCargaCombo.LlenarCombo(this.ddlTipoDocumento, dt_TipoDocumento, "Cod_Campo", "Dsc_Campo", false, false);


            //Carga combo Estado
            DataTable dt_Estado = new DataTable();
            dt_TipoDocumento = objWBConsultas.ListaCamposxNombre("EstadoRegistro");
            objCargaCombo.LlenarCombo(this.ddlEstado, dt_TipoDocumento, "Cod_Campo", "Dsc_Campo", false, false);

        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

    }


    private ArrayList ControlesPermisos(List<ListaDeCampo> objListListadeCampo, int Permisos)
    {
        ArrayList controllist = new ArrayList();

        int contTableRows = objListListadeCampo.Count;

        for (int i = 0; i <= contTableRows - 1; i++)
        {
            //row3
            MyLabel lblI = new MyLabel(); //initialize new label
            lblI.ID = UI.LabelPrefix + "I" + i.ToString(); //set ID property 
            lblI.Text = objListListadeCampo[i].SCodCampo; //set text property for label
            lblI.Visible = false;
            controllist.Add(lblI); //add label to 

            MyLabel lblIII = new MyLabel(); //initialize new label
            lblIII.ID = UI.LabelPrefix + "III" + i.ToString(); //set ID property 
            lblIII.Text = objListListadeCampo[i].SDscCampo; //set text property for label
            lblIII.CssClass = "itemtablaMV"; //set css class for label
            controllist.Add(lblIII); //add label to cell

            ChkBox chkI = new ChkBox(); //initlaize new checkbox
            chkI.ID = UI.CheckBoxPrefix + "I" + i.ToString(); //set ID property 
            chkI.Width = 60;  // set width property

            string valor = "";
            if (Convert.ToInt32(Permisos) == 0)
            {
                valor = "0";
            }
            else
            {
                string conversion = DecimalToBase(Permisos, 2);
                conversion = conversion.PadLeft(objListListadeCampo.Count, '0');
                valor = conversion.Substring(i, 1);
            }

            if (valor == "1")
                chkI.Checked = true;
            else
                chkI.Checked = false;

            controllist.Add(chkI); // add textbox to cell
        }

        return controllist;

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

    private string PermisosSeleccionados(ArrayList controlList)
    {
        ArrayList asignados = new ArrayList();
        string valor = "";

        for (int i = 0; i < controlList.Count; i++)
        {
            switch (controlList[i].GetType().FullName)
            {
                case "ChkBox":
                    ChkBox chk = new ChkBox();
                    chk = (ChkBox)controlList[i];
                    if (chk.Checked == true)
                    {
                        valor = "1";
                    }
                    else
                    {
                        valor = "0";
                    }
                    asignados.Add(valor);

                    break;
                default:
                    break;
            }
        }

        return Convert.ToString(VerificarSeleccion(asignados));
    }


    protected double VerificarSeleccion(ArrayList Lista)
    {
        double valor = 0;
        double exp = Lista.Count;
        for (int i = 0; i < Lista.Count; i++)
        {
            valor = valor + System.Math.Pow(2, exp - 1) * Convert.ToInt32(Lista[i]);
            exp--;
        }

        return valor;
    }


    protected bool ValidarNombreRepresentante(string sNombre, string sApellido)
    {
        Sglobales objGlobales = obtenerGlobales();

        if ((string)ViewState["Nombre"] != "" && (string)ViewState["Nombre"] != sNombre + sApellido)
        {
            if (objGlobales.mRRegistrados.Count > 0)
            {
                object[] keys = new object[objGlobales.mRRegistrados.Keys.Count];
                objGlobales.mRRegistrados.Keys.CopyTo(keys, 0);

                for (int i = 0; i < keys.Length; i++)
                {
                    RepresentantCia objRepresentantCia = new RepresentantCia();
                    objRepresentantCia = (RepresentantCia)objGlobales.mRRegistrados[keys[i]];

                    if (objRepresentantCia != null)
                    {
                        if (objRepresentantCia.SNomRepresentante.Trim() == sNombre && objRepresentantCia.SApeRepresentante.Trim() == sApellido)
                        {
                            guardarGlobales(objGlobales);
                            return true;                            
                        }
                    }
                }

            }
        }
        guardarGlobales(objGlobales);
        return false;
    }


    protected bool ValidarDocumentoRepresentante(string sDocumento, string sTipoDocumento)
    {
        Sglobales objGlobales = obtenerGlobales();
        if ((string)ViewState["Documento"] != "" && (string)ViewState["Documento"] != sTipoDocumento + sDocumento)
        {
            if (objGlobales.mRRegistrados.Count > 0)
            {
                object[] keys = new object[objGlobales.mRRegistrados.Keys.Count];
                objGlobales.mRRegistrados.Keys.CopyTo(keys, 0);

                for (int i = 0; i < keys.Length; i++)
                {
                    RepresentantCia objRepresentantCia = new RepresentantCia();
                    objRepresentantCia = (RepresentantCia)objGlobales.mRRegistrados[keys[i]];

                    if (objRepresentantCia != null)
                    {
                        if (objRepresentantCia.SNDocRepresentante.Trim() == sDocumento && objRepresentantCia.STDocRepresentante.Trim() == sTipoDocumento)
                        {
                            guardarGlobales(objGlobales);
                            return true;
                        }
                    }
                }

            }
        }
        guardarGlobales(objGlobales);
        return false;
    }

    protected bool ValidarDocumentoRepresentanteGlobal(string sNombre, string sApellido, string sDocumento, string sTipoDocumento)
    {
        if (objWBAdministracion.validarDocumento(sNombre, sApellido, sTipoDocumento, sDocumento) == 0)
        {
            return false;
        }
        else
        {
            return true;
        }        
    }
    
    #region Build Html Table With Controls
    private void BuildHtmlTableWithControls(ArrayList controlList, Table Tabla)
    {
        TableRow trow;
        TableCell tcell;
        int cols = 3;

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
    #endregion

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
            Sglobales objGlobales = obtenerGlobales();
            if (ValidarNombreRepresentante(this.txtNombre.Text, this.txtApellido.Text) == false)
            {

                if (ValidarDocumentoRepresentante(this.txtNumDocumento.Text, this.ddlTipoDocumento.SelectedItem.Text) == false)
                {

                    if (ValidarDocumentoRepresentanteGlobal(this.txtNombre.Text, this.txtApellido.Text, this.txtNumDocumento.Text, this.ddlTipoDocumento.SelectedItem.Text) == false)
                    {

                        string permiso = PermisosSeleccionados(objGlobales.mRListaControles);

                        RepresentantCia objRepresentantCia;
                        if ((string)ViewState["id"] == "AD")
                        {
                            objRepresentantCia = new RepresentantCia("", objGlobales.mRRegistrados.Count + 1, this.txtNombre.Text, this.txtApellido.Text, txtCargo.Text, "1", Convert.ToString(Session["Cod_Usuario"]), "", "", ddlTipoDocumento.SelectedValue, txtNumDocumento.Text, permiso);
                            objGlobales.mRRegistrados.Add(objRepresentantCia.INumSecuencial, objRepresentantCia);
                        }
                        else
                        {
                            objRepresentantCia = new RepresentantCia("", Convert.ToInt32(this.lblDscCodigo.Text), this.txtNombre.Text, this.txtApellido.Text, txtCargo.Text, ddlEstado.SelectedValue, Convert.ToString(Session["Cod_Usuario"]), "", "", ddlTipoDocumento.SelectedValue, txtNumDocumento.Text, permiso);
                            objGlobales.mRRegistrados[Convert.ToInt32(ViewState["id"])] = objRepresentantCia;

                        }

                        objGlobales.mFlagModal = true;
                        objGlobales.mFlagPosPage = false;
                        mpextReprsentante.Hide();
                        objGlobales.mMjeErrorRepres = "";

                    }
                    else
                    {
                        objGlobales.mMjeErrorRepres = "El Documento del representante ya se encuentra registrado, por favor verifique";
                    }
                }
                else
                {
                    objGlobales.mMjeErrorRepres = "El Documento del representante ya se encuentra registrado en la compañía, por favor verifique";
                }
            }
            else
            {
                objGlobales.mMjeErrorRepres = "El representante ya se encuentra registrado en la compañía, por favor verifique";
            }
            objGlobales.mFlagPosPage = true;
            guardarGlobales(objGlobales);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

    
}
