/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Creación de Rol
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

public partial class Modulo_Seguridad_Rol_CrearRol : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    BO_Consultas objBO_Consultas = new BO_Consultas();
    bool flagError;


    protected void Page_Load(object sender, EventArgs e)
    {
        tvwRoles.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");

        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblNombre.Text = htLabels["mrol.lblNombre"].ToString();
                btnAceptar.Text = htLabels["mrol.btnAceptar"].ToString();
                btnAsignar.Text = htLabels["mrol.btnAsignar"].ToString();
                lblRolPadre.Text = htLabels["mrol.lblRolPadre"].ToString();
                lblResumenPefil.Text = htLabels["mrol.lblResumenPefil"].ToString();
                lblPerfilConfiguracion.Text = htLabels["mrol.lblPerfilConfiguracion"].ToString();
                hConfirmacion.Value = htLabels["mrol.cbeAceptar"].ToString();
//                rfvNombre.ErrorMessage = htLabels["mrol.rfvNombre"].ToString();
            }
            catch (Exception ex)
            {
                flagError = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (flagError)
                    Response.Redirect("PaginaError.aspx");
            }


            limpiar();

            try
            {
                ddlRolPadre.DataSource = odsListarRoles;
                ddlRolPadre.DataTextField = "SNomRol";
                ddlRolPadre.DataValueField = "SCodRol";
                ddlRolPadre.DataBind();
                LlenarArbol(ddlRolPadre.SelectedItem.Value);
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

            this.txtNombre.Focus();
        }
    }

    void LlenarArbol(string SCodRol)
    {
        DataTable dtRoles = new DataTable();

        dtRoles = objBOSeguridad.PermisoRolesxRol(SCodRol);

        tvwRoles.Nodes.Clear();

        LlenaArbolPadre(dtRoles);

        tvwRoles.ExpandAll();
        tvwRoles.ShowExpandCollapse = false;
    }


    protected DataTable OpcionesPadre(DataTable dtArbolRoles)
    {
        DataTable dtArbol = new DataTable();
        DataRow drArbol;
        dtArbol.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("sequence", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("flag_permitido", System.Type.GetType("System.String")));

        for (int i = 0; i < dtArbolRoles.Rows.Count; i++)
        {
            if (dtArbolRoles.Rows[i]["parent_id"].ToString() == "")
            {
                drArbol = dtArbol.NewRow();
                drArbol["id"] = dtArbolRoles.Rows[i]["id"].ToString();
                drArbol["parent_id"] = dtArbolRoles.Rows[i]["parent_id"].ToString();
                drArbol["title"] = dtArbolRoles.Rows[i]["title"].ToString();
                drArbol["sequence"] = dtArbolRoles.Rows[i]["sequence"].ToString();
                drArbol["flag_permitido"] = dtArbolRoles.Rows[i]["flag_permitido"].ToString();

                dtArbol.Rows.Add(drArbol);
            }
        }

        return dtArbol;
    }


    protected DataTable OpcionesHijo(DataTable dtArbolRoles, string sCodPadreID)
    {
        DataTable dtArbol = new DataTable();
        DataRow drArbol;
        dtArbol.Columns.Add(new DataColumn("id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("parent_id", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("title", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("sequence", System.Type.GetType("System.String")));
        dtArbol.Columns.Add(new DataColumn("flag_permitido", System.Type.GetType("System.String")));

        for (int i = 0; i < dtArbolRoles.Rows.Count; i++)
        {
            if (dtArbolRoles.Rows[i]["parent_id"].ToString() == sCodPadreID)
            {
                drArbol = dtArbol.NewRow();
                drArbol["id"] = dtArbolRoles.Rows[i]["id"].ToString();
                drArbol["parent_id"] = dtArbolRoles.Rows[i]["parent_id"].ToString();
                drArbol["title"] = dtArbolRoles.Rows[i]["title"].ToString();
                drArbol["sequence"] = dtArbolRoles.Rows[i]["sequence"].ToString();
                drArbol["flag_permitido"] = dtArbolRoles.Rows[i]["flag_permitido"].ToString();

                dtArbol.Rows.Add(drArbol);
            }
        }

        return dtArbol;
    }

    protected void LlenaArbolPadre(DataTable dtArbolRoles)
    {
        string sCodPadreID = "";
        TreeNode TPadre = null;

        DataTable dataModulo = new DataTable();

        dataModulo = OpcionesPadre(dtArbolRoles);

        for (int i = 0; i < dataModulo.Rows.Count; i++)
        {
            sCodPadreID = dataModulo.Rows[i]["id"].ToString();
            string sPadre = dataModulo.Rows[i]["title"].ToString();
            TPadre = new TreeNode(sPadre);
            TPadre.ShowCheckBox = true;
            TPadre.SelectAction =TreeNodeSelectAction.None;
            TPadre.Value = Convert.ToString(dataModulo.Rows[i]["id"]);
            TPadre.ChildNodes.Add(LlenaArbolHijo(OpcionesHijo(dtArbolRoles, sCodPadreID), TPadre, dtArbolRoles));
            tvwRoles.Nodes.Add(TPadre);
        }
    }

    protected TreeNode LlenaArbolHijo(DataTable dtOpcionesHijo, TreeNode TPadre, DataTable dtArbolRoles)
    {
        string sCodPadreID;
        TreeNode THijo = null;
        for (int i = 0; i < dtOpcionesHijo.Rows.Count; i++)
        {
            sCodPadreID = dtOpcionesHijo.Rows[i]["id"].ToString();
            string sHijo = dtOpcionesHijo.Rows[i]["title"].ToString();
            THijo = new TreeNode(sHijo);
            THijo.ShowCheckBox = true;
            THijo.SelectAction = TreeNodeSelectAction.None;
            THijo.Value = Convert.ToString(dtOpcionesHijo.Rows[i]["id"]);
            THijo.ChildNodes.Add(LlenaArbolHijo(OpcionesHijo(dtArbolRoles, sCodPadreID), THijo, dtArbolRoles));
            TPadre.ChildNodes.Add(THijo);
        }

        return TPadre;
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        if (valida() == true)
        {
            try
            {
                objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                if (objBOSeguridad.obtenerRolxnombre(this.txtNombre.Text.Trim()) == null)
                {

                    Rol objRol = new Rol("", this.ddlRolPadre.SelectedItem.Value, this.txtNombre.Text.Trim(), Convert.ToString(Session["Cod_Usuario"]),
                        "", "");
                    
                    if (objBOSeguridad.registrarRol(objRol) == true)
                    {
                        for (int i = 0; i < this.lstCodProcesoAsignados.Items.Count; i++)
                        {
                            if (lstCodProcesoAsignados.Items[i].Text != "")
                            {
                                string[] lista = new string[2];
                                lista = lstCodProcesoAsignados.Items[i].Text.Split('|');
                                objBOSeguridad.registrarPerfilRol(new PerfilRol(lista[1],lista[0],objBOSeguridad.obtenerRolxnombre(txtNombre.Text).SCodRol, "", Convert.ToString(Session["Cod_Usuario"]), "", ""));
                            }
                        }
                        omb.ShowMessage("Rol registrado correctamente", "Creacion de Rol", "Seg_VerRol.aspx");
                    }
                }
                else
                {
                    this.lblMensajeError.Text = "El Nombre de Rol ya se encuentra registrado, verifique por favor";
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

    bool valida()
    {

        if(this.txtNombre.Text.Trim()=="")
        {
            this.lblMensajeError.Text = "Ingrese el nombre del Rol";
            return false;
        }

        if (this.lstCodProcesoAsignados.Items.Count <= 1)
        {
            this.lblMensajeError.Text = "Asigne alguna opción del Perfil";
            return false;
        }

        this.lblMensajeError.Text = "";
        return true;
    }

    bool validaPadre(TreeView trvPerfil)
    {
        bool valida = false;
        for (int i = 0; i < trvPerfil.Nodes.Count; i++)
        {
            if (trvPerfil.Nodes[i].Checked == true)
            {
                valida = validaHijo(trvPerfil.Nodes[i]);
            }
        }
        return valida;
    }


    bool validaHijo(TreeNode trnHijo)
    {
        for (int i = 0; i < trnHijo.ChildNodes.Count; i++)
        {
            if (trnHijo.ChildNodes[i].Checked == true)
            {
                return true;
            }
        }
        return false;
    }


    void ResumenPadre(TreeView trvResumen)
    {
        for (int i = 0; i < trvResumen.Nodes.Count; i++)
        {
            if (trvResumen.Nodes[i].Checked == true)
            {
                string sPadre = trvResumen.Nodes[i].Text;
                TreeNode TPadre = new TreeNode(sPadre);
                TPadre.ShowCheckBox = false;
                TPadre.SelectAction = TreeNodeSelectAction.None;
                this.lstCodProcesoAsignados.Items.Add("");
                TreeNode trnAsignado = ResumenHijo(trvResumen.Nodes[i], TPadre, trvResumen.Nodes[i].Value);
                trnAsignado.Checked = false;
                trnAsignado.SelectAction = TreeNodeSelectAction.None;
                trnAsignado.Text = sPadre;
                tvwRolesAsignados.Nodes.Add(trnAsignado);
            }
        }

    }

    TreeNode ResumenHijo(TreeNode trnResumen, TreeNode TPadre, string CodModulo)
    {
        TreeNode TNodoResumenHijo = new TreeNode();
        if (trnResumen.ChildNodes.Count == 0)
        {
            TreeNode THijo = new TreeNode(trnResumen.Text);
            THijo.ShowCheckBox = false;
            THijo.SelectAction = TreeNodeSelectAction.None;
            return THijo;
        }

        else
        {
            for (int j = 0; j < trnResumen.ChildNodes.Count; j++)
            {
                if (trnResumen.ChildNodes[j].Checked == true)
                {
                    TreeNode THijo = new TreeNode(trnResumen.ChildNodes[j].Text);
                    THijo.ShowCheckBox = false;
                    THijo.SelectAction = TreeNodeSelectAction.None;
                    TPadre.ChildNodes.Add(THijo);
                    this.lstCodProcesoAsignados.Items.Add(trnResumen.ChildNodes[j].Value);
                    TreeNode trnHijoAsignado = ResumenHijo(trnResumen.ChildNodes[j], TPadre, CodModulo);
                    trnHijoAsignado.Text = trnResumen.ChildNodes[j].Text;
                    trnHijoAsignado.Checked = false;
                    trnHijoAsignado.SelectAction = TreeNodeSelectAction.None;
                    TNodoResumenHijo.ChildNodes.Add(trnHijoAsignado);
                }

            }
        }

        return TNodoResumenHijo;
    }


    void limpiar()
    {
        this.txtNombre.Text = "";
        this.ddlRolPadre.SelectedIndex = 0;
    }

    protected void ddlRolPadre_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LlenarArbol(ddlRolPadre.SelectedItem.Value);
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

    protected void btnAsignar_Click(object sender, EventArgs e)
    {
        this.lstCodProcesoAsignados.Items.Clear();
        this.tvwRolesAsignados.Nodes.Clear();

        if (validaPadre(this.tvwRoles) == false)
        {
            this.lblMensajeError.Text = "Elija alguna opción del Perfil";
        }
        else
        {
            try
            {
                ResumenPadre(this.tvwRoles);
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

            tvwRolesAsignados.ExpandAll();
            tvwRolesAsignados.ShowExpandCollapse = false;
        }
    }
}
