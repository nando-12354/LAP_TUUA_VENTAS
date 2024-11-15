/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Modificación de Rol
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
using LAP.TUUA.ALARMAS;

public partial class Modulo_Seguridad_Rol_ModificarRol : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected Hashtable htSPConfig;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    PerfilRol objPerfilRol = new PerfilRol();
    string CodModulo;
    bool Eliminar;

    protected void Page_Load(object sender, EventArgs e)
    {
        tvwRoles.Attributes.Add("onclick", "OnCheckBoxCheckChanged(event)");

        if (!Page.IsPostBack)
        {
            htLabels = LabelConfig.htLabels;
            try
            {
                lblNombre.Text = htLabels["mrol.lblNombre"].ToString();
                btnActualizar.Text = htLabels["mrol.btnActualizar"].ToString();
                btnAsignar.Text = htLabels["mrol.btnAsignar"].ToString();
                btnEliminar.Text = htLabels["mrol.btnEliminar"].ToString();
                btnEliminar.Enabled = objBOSeguridad.FlagPerfilUsuarioOpcion((DataTable)Session["DataMapSite"], Define.ASPX_SegEliminarRol, Define.OPC_ELIMINAR);
                lblRolPadre.Text = htLabels["mrol.lblRolPadre"].ToString();
                lblResumenPefil.Text = htLabels["mrol.lblResumenPefil"].ToString();
                lblPerfilConfiguracion.Text = htLabels["mrol.lblPerfilConfiguracion"].ToString();
                hConfirmacion1.Value = htLabels["mrol.cbeEliminar"].ToString(); 
                hConfirmacion0.Value = htLabels["mrol.cbeActualizar"].ToString();

            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_SegModificarRol;
                Response.Redirect("PaginaError.aspx");
            }

            try
            {
                PoblarRol(Convert.ToString(Request.QueryString["Cod_Rol"]));
                LlenarArbol(Convert.ToString(Request.QueryString["Cod_Rol"]));
                ResumenPadre(this.tvwRoles);
            }
            catch (Exception ex)
            {
                Response.Redirect("PaginaError.aspx");
            }
            this.tvwRolesAsignados.ExpandAll();
            this.tvwRolesAsignados.ShowExpandCollapse = false;
            txtNombre.Focus();
        }
    }

    void PoblarRol(string NomRol)
    {
        Rol objRol = new Rol();

        objRol = objBOSeguridad.obtenerRolxcodigo(Request.QueryString["Cod_Rol"]);
        txtNombre.Text = objRol.SNomRol;

        if (objRol.SCodPadreRol != null)
        {
            txtRolPadre.Text = objBOSeguridad.obtenerRolxcodigo(objRol.SCodPadreRol).SNomRol;
        }
        else
        {
            lblRolPadre.Visible = false;
            txtRolPadre.Text = "";
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
            TPadre.SelectAction = TreeNodeSelectAction.None;
            if (dataModulo.Rows[i]["flag_permitido"].ToString() == "1")
            TPadre.Checked = true;
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
            if (dtOpcionesHijo.Rows[i]["flag_permitido"].ToString() == "1")
            THijo.Checked = true;
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
                ActualizarPadre(this.tvwRoles, this.lstCodProcesoAsignados);
                //GeneraAlarma
                string IpClient = Request.UserHostAddress;
                GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000005", "001", IpClient, "1", "Alerta W0000005", "Modificacion de Rol " + txtNombre.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

            }
            catch (Exception ex)
            {
                Response.Redirect("PaginaError.aspx");
            }
            omb.ShowMessage("Rol actualizado correctamente", "Modificar Rol", "Seg_VerRol.aspx");
        }
    }

    bool valida()
    {

        if (this.lstCodProcesoAsignados.Items.Count <= 1)
        {
            this.lblMensajeError.Text = "Asigne alguna opción del Perfil";
            return false;
        }
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

    TreeNode ResumenHijo(TreeNode trnResumen, TreeNode TPadre,string CodModulo)
    {
        TreeNode TNodoResumenHijo = new TreeNode();
        if (trnResumen.ChildNodes.Count == 0)
        {
            TreeNode THijo = new TreeNode(trnResumen.Text);
            THijo.SelectAction = TreeNodeSelectAction.None;
            THijo.ShowCheckBox = false;
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
                    trnHijoAsignado.SelectAction = TreeNodeSelectAction.None;
                    trnHijoAsignado.Checked = false;
                    TNodoResumenHijo.ChildNodes.Add(trnHijoAsignado);
                }

            }
        }


        return TNodoResumenHijo;
    }




    void ActualizarPadre(TreeView trvPerfil, ListBox lstResumen)
    {
        string CodModulo;
        try
        {
            for (int i = 0; i < trvPerfil.Nodes.Count; i++)
            {
                CodModulo = trvPerfil.Nodes[i].Value;
                ActualizarHijo(trvPerfil.Nodes[i], CodModulo, lstResumen);
            }
        }
        catch (Exception ex)
        {

            throw;
        }

    }

    void ActualizarHijo(TreeNode trnPerfil, String CodModulo, ListBox lstResumen)
    {
        try
        {
            for (int i = 0; i < trnPerfil.ChildNodes.Count; i++)
            {
                bool encontro = false;
                for (int j = 0; j < lstResumen.Items.Count; j++)
                {
                    if (trnPerfil.ChildNodes[i].Value == lstCodProcesoAsignados.Items[j].Text)
                    {
                        encontro = true;
                        break;
                    }
                }

                string[] lista = new string[2];
                lista = trnPerfil.ChildNodes[i].Value.Split('|');

                if (encontro == true)
                {
                    objPerfilRol = new PerfilRol(lista[1], CodModulo, Request.QueryString["Cod_Rol"], "1", Convert.ToString(Session["Cod_Usuario"]), "", "");
                }
                else
                {
                    objPerfilRol = new PerfilRol(lista[1], CodModulo, Request.QueryString["Cod_Rol"], "0", Convert.ToString(Session["Cod_Usuario"]), "", "");
                }
                objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                objBOSeguridad.actualizarPerfilRol(objPerfilRol);
                ActualizarHijo(trnPerfil.ChildNodes[i], CodModulo, lstResumen);
            }
        }
        catch (Exception ex)
        {
            throw;

        }

    }


    void limpiar()
    {
        this.txtNombre.Text = "";
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
                Response.Redirect("PaginaError.aspx");
            }
            tvwRolesAsignados.ExpandAll();
            tvwRolesAsignados.ShowExpandCollapse = false;
        }

    }


    protected void btnEliminar_Click(object sender, EventArgs e)
    {

        if (validaElimina() == true)
        {
            try
            {
                  objBOSeguridad = new BO_Seguridad((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
                Eliminar = objBOSeguridad.eliminarRol(Request.QueryString["Cod_Rol"]);
                if (Eliminar)
                {
                    //GeneraAlarma
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000006", "001", IpClient, "1", "Alerta W0000006", "Eliminacion de Rol " + txtNombre.Text + ", Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("PaginaError.aspx");
            }
            if (Eliminar == true)
            {
                omb.ShowMessage("Rol eliminado correctamente", "Eliminar Rol", "Seg_VerRol.aspx");
            }
        }

    }

    protected bool validaElimina() 
    {
        UsuarioRol objWBUsRol = new UsuarioRol();
        bool rolPadre=false;
        try
        {
            objWBUsRol = objBOSeguridad.obtenerRolUsuarioxCodRol(Request.QueryString["Cod_Rol"]);
            rolPadre = objBOSeguridad.obtenerRolHijo(Request.QueryString["Cod_Rol"]);
        }
        catch (Exception ex)
        {
            Response.Redirect("PaginaError.aspx");
        }

        if (objWBUsRol != null)
        {
            this.lblMensajeError.Text = "El rol no puede ser eliminado, se encuentra asignado a un Usuario";
            return false;
        }

        if (rolPadre==true)
        {
            this.lblMensajeError.Text = "El rol no puede ser eliminado, es Padre de otro Rol";
            return false;
        }
        return true;

    }
}
