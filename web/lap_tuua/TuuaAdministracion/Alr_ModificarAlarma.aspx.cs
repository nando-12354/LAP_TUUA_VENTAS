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
using System.Xml;
using LAP.TUUA.UTIL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using System.Collections.Generic;
using System.Text;

public partial class Alr_ModificarAlarma : System.Web.UI.Page
{
    protected Hashtable htLabels;
    BO_Consultas objBOConsultas = new BO_Consultas();
    BO_Administracion objBOAdministracion = new BO_Administracion();
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Alarmas objBOAlarmas;
    UIControles objCargaCombo = new UIControles();
    bool flagError;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblModulo.Text = htLabels["malarma.lblModulo"].ToString();
                this.lblConfiguracionCorreo.Text = htLabels["malarma.lblConfiguracionCorreo"].ToString();
                this.lblEnviarCorreo.Text = htLabels["malarma.lblEnviarCorreo"].ToString();
                this.lblFinMensaje.Text = htLabels["malarma.lblFinMensaje"].ToString();
                this.lblAsunto.Text = "Asunto de correo";
                this.lblTipoAlarma.Text = htLabels["malarma.lblTipoAlarma"].ToString();
                btnEliminar.Text = htLabels["malarma.btnEliminar"].ToString();
                btnEliminar.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_AlrEliminarAlarma, Define.OPC_ELIMINAR);
                this.btnAceptar.Text = htLabels["malarma.btnActualizar"].ToString();
                cbeEliminar.ConfirmText = htLabels["malarma.cbeEliminar"].ToString();
                cbeActualizar.ConfirmText = htLabels["malarma.cbeActualizar"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegCrearUsuario;
                Response.Redirect("PaginaError.aspx");
            }
            CargarCombos();
            PoblarConfigAlarma(Request.QueryString["Cod_Alarma"], Request.QueryString["Cod_Modulo"]);
            RefreshDestinatarioRow();
        }
    }

    public void CargarCombos()
    {
        try
        {
            //Carga combo Modulo
            DataTable dt_Usuario = new DataTable();
            dt_Usuario = objBOConsultas.ListarAllUsuario();
            //ViewState["ConsultaUsuario"] = dt_Usuario;

            DataTable dest = new DataTable();
            DataColumn dc;
            dc = new DataColumn();
            dc.ColumnName = "Cod_Usuario";
            dest.Columns.Add(dc);
            dc = new DataColumn();
            dc.ColumnName = "Cta_Usuario";
            dest.Columns.Add(dc);
            DataRow[] foundRowTipoTicket = dt_Usuario.Select("Tip_Estado_Actual = 'V'", "Cta_Usuario ASC");

            if (foundRowTipoTicket != null && foundRowTipoTicket.Length > 0)
            {
                for (int i = 0; i < foundRowTipoTicket.Length; i++)
                {
                    dest.Rows.Add(dest.NewRow());
                    dest.Rows[i][0] = foundRowTipoTicket[i]["Cod_Usuario"].ToString();
                    dest.Rows[i][1] = foundRowTipoTicket[i]["Cta_Usuario"].ToString();
                }
                dest.AcceptChanges();
            }

            ViewState["ConsultaUsuario"] = dest;

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

    protected void PoblarConfigAlarma(string sCodAlarma, string sCodModulo)
    {
        DataTable dtAlarma = new DataTable();
        objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        dtAlarma = objBOAlarmas.ListarAllCnfgAlarma();

        DataRow[] foundRowAlarma = dtAlarma.Select("Cod_Alarma = '" + sCodAlarma + "' AND Cod_Modulo= '" + sCodModulo + "'");

        if (foundRowAlarma != null && foundRowAlarma.Length > 0)
        {
            this.txtModulo.Text = (string)foundRowAlarma[0]["Dsc_Modulo"].ToString();
            this.txtTipoAlarma.Text = (string)foundRowAlarma[0]["Dsc_Alarma"].ToString();
            if (foundRowAlarma[0]["Dsc_Fin_Mensaje"] != DBNull.Value)
                this.txtFinMensaje.Text = (string)foundRowAlarma[0]["Dsc_Fin_Mensaje"].ToString();
            if (foundRowAlarma[0]["Dsc_Subject"] != DBNull.Value)
                this.txtAsunto.Text = (string)foundRowAlarma[0]["Dsc_Subject"].ToString();
            if (foundRowAlarma[0]["Xml_Destinatarios"] != DBNull.Value)
                ViewState["TablaSeleccion"] = PoblarDestinatarios((string)foundRowAlarma[0]["Xml_Destinatarios"].ToString());
        }
    }


    protected DataTable PoblarDestinatarios(string sXmlDestinatarios)
    {

        DataTable dtTablaSeleccion;
        dtTablaSeleccion = new DataTable();
        dtTablaSeleccion.Columns.Add("Cod_Usuario", System.Type.GetType("System.String"));
        dtTablaSeleccion.Columns.Add("Dsc_Email", System.Type.GetType("System.String"));
        dtTablaSeleccion.Columns.Add("Indx_Usuario", System.Type.GetType("System.Int32"));


        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(sXmlDestinatarios);

        XmlElement root = xmlDoc.DocumentElement;
        XmlNodeList elemList = root.GetElementsByTagName("user");

        int i = 0;
        foreach (XmlNode node in elemList)
        {
            DataRow rowSeleccion = dtTablaSeleccion.NewRow();
            dtTablaSeleccion.Rows.Add(rowSeleccion);

            dtTablaSeleccion.Rows[i]["Cod_Usuario"] = node.SelectSingleNode("code").InnerText;
            dtTablaSeleccion.Rows[i]["Dsc_Email"] = node.SelectSingleNode("mail").InnerText;
            dtTablaSeleccion.Rows[i]["Indx_Usuario"] = IndiceListaUsuario(node.SelectSingleNode("code").InnerText);
            i++;

        }

        return dtTablaSeleccion;

    }

    protected int IndiceListaUsuario(string CodUsuario)
    {
        DataTable dtUsuario = new DataTable();

        if (ViewState["ConsultaUsuario"] != null)
            dtUsuario = (DataTable)ViewState["ConsultaUsuario"];

        if (dtUsuario.Rows.Count > 0)
        {
            for (int i = 0; i < dtUsuario.Rows.Count; i++)
            {
                if ((string)dtUsuario.Rows[i]["Cod_Usuario"] == CodUsuario)
                {
                    return i;
                }
            }
        }
        return 0;
    }



    private void AddingEmptyRow()
    {
        DataTable dtDestinatarios = new DataTable();
        dtDestinatarios.Columns.Add("Cod_Usuario", System.Type.GetType("System.String"));
        dtDestinatarios.Columns.Add("Dsc_Email", System.Type.GetType("System.String"));
        DataRow row = dtDestinatarios.NewRow();

        dtDestinatarios.Rows.Add(row);
        this.gvwDestinatarios.DataSource = dtDestinatarios;
        gvwDestinatarios.DataBind();

        gvwDestinatarios.Rows[0].FindControl("txtEmail").Visible = false;
        gvwDestinatarios.Rows[0].FindControl("ddlUsuario").Visible = false;
        gvwDestinatarios.Rows[0].FindControl("imbEliminar").Visible = false;

    }

    protected void LecturaControles()
    {
        int pageSize = 0;
        int pageIndex = 0;
        int limite = 0;

        DataTable dtDestinatarios = new DataTable();

        if (ViewState["TablaSeleccion"] != null)
            dtDestinatarios = (DataTable)ViewState["TablaSeleccion"];

        DataTable dtTablaSeleccion;
        dtTablaSeleccion = new DataTable();
        dtTablaSeleccion.Columns.Add("Cod_Usuario", System.Type.GetType("System.String"));
        dtTablaSeleccion.Columns.Add("Dsc_Email", System.Type.GetType("System.String"));
        dtTablaSeleccion.Columns.Add("Indx_Usuario", System.Type.GetType("System.Int32"));

        pageSize = gvwDestinatarios.PageSize;
        pageIndex = gvwDestinatarios.PageIndex;
        if ((pageIndex + 1) < gvwDestinatarios.PageCount)
        {
            limite = pageSize;
        }
        else
        {
            limite = dtDestinatarios.Rows.Count - (pageIndex * pageSize);
        }

        for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
        {
            DataRow rowSeleccion = dtTablaSeleccion.NewRow();
            dtTablaSeleccion.Rows.Add(rowSeleccion);
            DropDownList ddlUsuario = (DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario");
            TextBox txtEmail = (TextBox)gvwDestinatarios.Rows[j].FindControl("txtEmail");
            dtTablaSeleccion.Rows[i]["Cod_Usuario"] = ddlUsuario.SelectedValue;
            dtTablaSeleccion.Rows[i]["Dsc_Email"] = txtEmail.Text;
            dtTablaSeleccion.Rows[i]["Indx_Usuario"] = ddlUsuario.SelectedIndex;
        }

        ViewState["TablaSeleccion"] = dtTablaSeleccion;

    }

    protected void AddingDestinatarioRow()
    {
        int pageSize = 0;
        int pageIndex = 0;
        int limite = 0;

        LecturaControles();

        DataTable dtTablaSeleccion = new DataTable();

        if (ViewState["TablaSeleccion"] != null)
            dtTablaSeleccion = (DataTable)ViewState["TablaSeleccion"];

        //Carga combo Modulo
        DataTable dt_Usuario = new DataTable();
        dt_Usuario = (DataTable)ViewState["ConsultaUsuario"];

        if (dt_Usuario.Rows.Count > 0)
        {
            DataRow row = dtTablaSeleccion.NewRow();

            dtTablaSeleccion.Rows.Add(row);
            this.gvwDestinatarios.DataSource = dtTablaSeleccion;
            gvwDestinatarios.DataBind();
            pageSize = gvwDestinatarios.PageSize;
            pageIndex = gvwDestinatarios.PageIndex;
            if ((pageIndex + 1) < gvwDestinatarios.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTablaSeleccion.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                objCargaCombo.LlenarCombo((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), dt_Usuario, "Cod_Usuario", "Cta_Usuario", false,false);

                if (dtTablaSeleccion.Rows.Count > 0 && j < dtTablaSeleccion.Rows.Count)
                {
                    if (dtTablaSeleccion.Rows[j]["Dsc_Email"] != DBNull.Value)
                        ActualizaEmail((TextBox)gvwDestinatarios.Rows[j].FindControl("txtEmail"), (string)dtTablaSeleccion.Rows[j]["Dsc_Email"]);

                    if (dtTablaSeleccion.Rows[j]["Cod_Usuario"] != DBNull.Value)
                        ActualizaIndxUsuario((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), (int)dtTablaSeleccion.Rows[j]["Indx_Usuario"]);
                }
            }
        }
        else
        {
            ViewState["TablaSeleccion"] = null;
            this.lblMensajeErrorEmail.Text = "No se registraron Usuarios.";
        }

    }


    protected void AddingDestinatarioRow2()
    {

        LecturaControles();

        DataTable dtTablaSeleccion = new DataTable();

        if (ViewState["TablaSeleccion"] != null)
            dtTablaSeleccion = (DataTable)ViewState["TablaSeleccion"];

        //Carga combo Modulo
        DataTable dt_Usuario = new DataTable();
        dt_Usuario = (DataTable)ViewState["ConsultaUsuario"];

    }

    protected bool validaEmailRepetidos()
    {
        DataTable dtSeleccion = new DataTable();
        string email = "";

        LecturaControles();

        if ((DataTable)ViewState["TablaSeleccion"] != null)
            dtSeleccion = (DataTable)ViewState["TablaSeleccion"];


        if (dtSeleccion.Rows.Count > 0)
        {
            for (int i = 0; i < dtSeleccion.Rows.Count; i++)
            {
                if (dtSeleccion.Rows[i]["Dsc_Email"] != DBNull.Value)
                    email = (string)dtSeleccion.Rows[i]["Dsc_Email"];

                DataRow[] foundRowEmail = dtSeleccion.Select("Dsc_Email = '" + email + "'");

                if (email != "")
                {
                    if (foundRowEmail.Length > 1)
                    {
                        this.lblMensajeErrorEmail.Text = "Email " + email + " Repetido,verifique porfavor";
                        return false;
                    }
                }
            }
        }
        this.lblMensajeErrorEmail.Text = "";
        return true;
    }


    protected void ActualizaEmail(TextBox txtEmail, string sText)
    {
        txtEmail.Text = sText;
    }


    protected void ActualizaIndxUsuario(DropDownList ddlUsuario, int iIndex)
    {
        ddlUsuario.SelectedIndex = iIndex;
    }

    protected void btnConfigEmail_Click(object sender, ImageClickEventArgs e)
    {
        if (validaEmailRepetidos() == true)
            AddingDestinatarioRow();
    }
    protected void gvwDestinatarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            if (validaEmailRepetidos() == true)
            {
                AddingDestinatarioRow2();
            }

            int rowIndex = Int32.Parse(e.CommandArgument.ToString());
            DataTable dtDestinatarios = (DataTable)ViewState["TablaSeleccion"];
            dtDestinatarios.Rows.RemoveAt(rowIndex);
            if (dtDestinatarios.Rows.Count == 0)
            {
                AddingEmptyRow();
            }
            else
            {
                ViewState["TablaSeleccion"] = dtDestinatarios;
                RefreshDestinatarioRow2();
            }
        }

    }

    protected void RefreshDestinatarioRow()
    {
        int pageSize = 0;
        int pageIndex = 0;
        int limite = 0;
        if (gvwDestinatarios.Rows.Count > 0)
            LecturaControles();

        DataTable dtTablaSeleccion = new DataTable();

        if (ViewState["TablaSeleccion"] != null)
            dtTablaSeleccion = (DataTable)ViewState["TablaSeleccion"];


        //Carga combo Modulo
        DataTable dt_Usuario = new DataTable();
        dt_Usuario = (DataTable)ViewState["ConsultaUsuario"];

        if (dt_Usuario.Rows.Count > 0)
        {
            this.gvwDestinatarios.DataSource = dtTablaSeleccion;
            ViewState["TablaSeleccion"] = dtTablaSeleccion;
            gvwDestinatarios.DataBind();
            pageSize = gvwDestinatarios.PageSize;
            pageIndex = gvwDestinatarios.PageIndex;
            if ((pageIndex + 1) < gvwDestinatarios.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTablaSeleccion.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                objCargaCombo.LlenarCombo((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), dt_Usuario, "Cod_Usuario", "Cta_Usuario", false,false);

                if (dtTablaSeleccion.Rows.Count > 0)
                {
                    if (dtTablaSeleccion.Rows[j]["Dsc_Email"] != DBNull.Value)
                        ActualizaEmail((TextBox)gvwDestinatarios.Rows[j].FindControl("txtEmail"), (string)dtTablaSeleccion.Rows[j]["Dsc_Email"]);

                    if (dtTablaSeleccion.Rows[j]["Indx_Usuario"] != DBNull.Value)
                        ActualizaIndxUsuario((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), (int)dtTablaSeleccion.Rows[j]["Indx_Usuario"]);
                }
            }
        }
        else
        {
            ViewState["TablaSeleccion"] = null;
            this.lblMensajeErrorEmail.Text = "No se registraron Usuarios.";
        }

    }

    protected void RefreshDestinatarioRow2()
    {
        int pageSize = 0;
        int pageIndex = 0;
        int limite = 0;
        //if (gvwDestinatarios.Rows.Count > 0)
        //    LecturaControles();

        DataTable dtTablaSeleccion = new DataTable();

        if (ViewState["TablaSeleccion"] != null)
            dtTablaSeleccion = (DataTable)ViewState["TablaSeleccion"];


        //Carga combo Modulo
        DataTable dt_Usuario = new DataTable();
        dt_Usuario = (DataTable)ViewState["ConsultaUsuario"];

        if (dt_Usuario.Rows.Count > 0)
        {
            this.gvwDestinatarios.DataSource = dtTablaSeleccion;
            ViewState["TablaSeleccion"] = dtTablaSeleccion;
            gvwDestinatarios.DataBind();
            pageSize = gvwDestinatarios.PageSize;
            pageIndex = gvwDestinatarios.PageIndex;
            if ((pageIndex + 1) < gvwDestinatarios.PageCount)
            {
                limite = pageSize;
            }
            else
            {
                limite = dtTablaSeleccion.Rows.Count - (pageIndex * pageSize);
            }

            for (int i = (pageIndex * pageSize), j = 0; j < limite; i++, j++)
            {
                objCargaCombo.LlenarCombo((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), dt_Usuario, "Cod_Usuario", "Cta_Usuario", false, false);

                if (dtTablaSeleccion.Rows.Count > 0)
                {
                    if (dtTablaSeleccion.Rows[j]["Dsc_Email"] != DBNull.Value)
                        ActualizaEmail((TextBox)gvwDestinatarios.Rows[j].FindControl("txtEmail"), (string)dtTablaSeleccion.Rows[j]["Dsc_Email"]);

                    if (dtTablaSeleccion.Rows[j]["Indx_Usuario"] != DBNull.Value)
                        ActualizaIndxUsuario((DropDownList)gvwDestinatarios.Rows[j].FindControl("ddlUsuario"), (int)dtTablaSeleccion.Rows[j]["Indx_Usuario"]);
                }
            }
        }
        else
        {
            ViewState["TablaSeleccion"] = null;
            this.lblMensajeErrorEmail.Text = "No se registraron Usuarios.";
        }

    }

    protected string ObtenerDestinatarios()
    {

        DataTable dtTablaSeleccion = new DataTable();
        StringBuilder Destinatarios = new StringBuilder();


        if (ViewState["TablaSeleccion"] != null)
            dtTablaSeleccion = (DataTable)ViewState["TablaSeleccion"];

        if (dtTablaSeleccion.Rows.Count > 0)
        {
            Destinatarios.Append("<users>");

            for (int i = 0; i < dtTablaSeleccion.Rows.Count; i++)
            {
                string SCodUsuario = "";
                string sEmail = "";
                if (dtTablaSeleccion.Rows[i]["Cod_Usuario"] != DBNull.Value)
                    SCodUsuario = (string)dtTablaSeleccion.Rows[i]["Cod_Usuario"];
                if (dtTablaSeleccion.Rows[i]["Dsc_Email"] != DBNull.Value)
                    sEmail = (string)dtTablaSeleccion.Rows[i]["Dsc_Email"];

                if (SCodUsuario != "" && sEmail != "")
                {
                    Destinatarios.Append("<user>");
                    Destinatarios.Append("<code>" + SCodUsuario + "</code>");
                    Destinatarios.Append("<mail>" + sEmail + "</mail>");
                    Destinatarios.Append("</user>");
                }

            }
            Destinatarios.Append("</users>");

        }

        return Destinatarios.ToString();
    }


    protected bool validaConfiguracion()
    {
        if (validaEmailRepetidos() == false)
            return false;

        DataTable dtSeleccion = new DataTable();

        if ((DataTable)ViewState["TablaSeleccion"] != null)
            dtSeleccion = (DataTable)ViewState["TablaSeleccion"];

        DataRow[] foundRowEmail = dtSeleccion.Select("Dsc_Email <> ''");

        if (!(foundRowEmail.Length > 0))
        {
            this.lblMensajeError.Text = "Debe Registrar un Correo Electrónico";
            return false;

        }
        return true;

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (validaConfiguracion() == true)
        {
            objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            CnfgAlarma objCnfgAlarma = new CnfgAlarma(Request.QueryString["Cod_Alarma"], Request.QueryString["Cod_Modulo"], this.txtFinMensaje.Text, this.txtAsunto.Text, ObtenerDestinatarios(), Convert.ToString(Session["Cod_Usuario"]), "", "");
            if (objBOAlarmas.actualizarCnfgAlarma(objCnfgAlarma) == true)
                omb.ShowMessage("Configuración de Alarma actualizada correctamente", "Configuración de Alarma", "Alr_VerAlarma.aspx");
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        bool Eliminar=false;
        try
        {
            objBOAlarmas = new BO_Alarmas((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
            Eliminar = objBOAlarmas.eliminarCnfgAlarma(Request.QueryString["Cod_Alarma"], Request.QueryString["Cod_Modulo"]);
        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }
        if (Eliminar == true)
        {
            omb.ShowMessage("Configuración de Alarma eliminada correctamente", "Configuración de Alarma", "Alr_VerAlarma.aspx");
        }

    }
}
