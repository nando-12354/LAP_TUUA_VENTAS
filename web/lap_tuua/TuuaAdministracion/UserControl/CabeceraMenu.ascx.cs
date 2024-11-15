/*
Sistema		 :   TUUA
Aplicación	 :   Seguridad
Objetivo		 :   Menu TUUA
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
using LAP.TUUA.CONTROL;
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.UTIL;
using System.Collections.Generic;
using OboutInc.TwoColorsMenu;


public partial class CabeceraMenu : System.Web.UI.UserControl
{

    public Hashtable htSPConfig;
    protected Hashtable htLabels;
    BO_Seguridad objBOSeguridad = new BO_Seguridad();
    DataTable objDtMenu = new DataTable();
    TwoColorsMenu tcmTuua = new TwoColorsMenu();


    protected void Page_Load(object sender, EventArgs e)
    {

        //string script= "<script src='../javascript/MantenerSesion.js' type='text/javascript'></script>";
        //Page.RegisterClientScriptBlock("clientScript", script);
        if (!Page.IsPostBack)
        {
            
            htLabels = LabelConfig.htLabels;
            try
            {
                this.lblUsuario.Text = htLabels["CabeceraMenu.lblUsuario"].ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Desc_Info = Define.ASPX_CabeceraMenu;
                Response.Redirect("PaginaError.aspx");
            }

            htSPConfig = new Hashtable();
            htSPConfig = (Hashtable)Session["htSPConfig"];
        }

        string CodUsuario = Convert.ToString(Session["Cod_Usuario"]);
        //string CodUsuario = Convert.ToString("U000001");
        if (CodUsuario != "")
        {
            this.lblNombreUsuario.Text = objBOSeguridad.obtenerUsuario(CodUsuario).SNomUsuario + " " + objBOSeguridad.obtenerUsuario(CodUsuario).SApeUsuario;

            DataTable dtFechaHoy = new DataTable();

            dtFechaHoy = objBOSeguridad.obtenerFecha();

            string[] sFechaHora = new string[2];

            if (dtFechaHoy.Rows.Count > 0)
            {
                sFechaHora = Convert.ToString(dtFechaHoy.Rows[0].ItemArray.GetValue(0).ToString()).Split('|');
            }

            string sFecha = Fecha.convertSQLToFecha(sFechaHora[0], sFechaHora[1]);

            DateTime hoy = Convert.ToDateTime(sFecha);

            this.lblFecha.Text = hoy.ToString("dddd") + ", " + hoy.Day.ToString() + " de " + hoy.ToString("MMMM") + " de " + hoy.ToString("yyyy");
            try
            {
                objDtMenu = (DataTable)Session["DataMenu"];
            }
            catch (Exception ex)
            {

                Response.Redirect("PaginaError.aspx");
            }

            if (objDtMenu.Rows.Count > 0)
            {
                CrearMenu(objDtMenu);
            }

        }
        else
        {
            Response.Redirect("Index.aspx");
        }

        htLabels = LabelConfig.htLabels;

        try
        {
            tcmTuua.ColorBackground = htLabels["CabeceraMenu.ColorFondoMenu"].ToString();
            tcmTuua.ColorFont = htLabels["CabeceraMenu.ColorFuenteMenu"].ToString();
            tcmTuua.ColorFontOver = htLabels["CabeceraMenu.ColorFuenteSelectMenu"].ToString();
            tcmTuua.Font = htLabels["CabeceraMenu.FuenteItemMenu"].ToString();
            tcmTuua.FontParent = htLabels["CabeceraMenu.FuentePadreItemMenu"].ToString();
            this.Panel1.Controls.Add(tcmTuua);
            if (Session["htParametro"] == null)
            {
                CargarParametros();
            }
        }
        catch (Exception ex)
        {
            ErrorHandler.Cod_Error = Define.ERR_008;
            ErrorHandler.Desc_Info = Define.ASPX_CabeceraMenu;
            Response.Redirect("PaginaError.aspx");
        }

        int s_timeout = (int)Session["TimeInactivo"] * 1000;
        string str_Script = "StartTime(" + s_timeout.ToString() + ");";
        //Property.strModulo = Define.ADMIN;
        string Modulo_SubModulo = ObtenerSubModulo();
        if (Modulo_SubModulo != "" )
        {
            Session["Cod_Modulo"] = Modulo_SubModulo.Substring(0, 3);
            string strSubModulo = Modulo_SubModulo.Substring(3, 5);
            Session["Cod_SubModulo"] = strSubModulo != "" ? strSubModulo : Property.strSubModulo;
        }        
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "CheckSessionOut", str_Script, true);
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

    protected string ObtenerSubModulo()
    {
        string strPagina = this.Page.AppRelativeVirtualPath.ToString(); 
        strPagina = strPagina.Substring(2, strPagina.Length-2); 
        //strPagina = strPagina.ToUpper(); 

        for (int i = 0; i < objDtMenu.Rows.Count; i++)
        {
            if (objDtMenu.Rows[i].ItemArray.GetValue(3).ToString() == strPagina)
            {
                return objDtMenu.Rows[i]["id"].ToString();
            }
        }
        return "";
    }

    protected void CrearMenu(DataTable dtMenu)
    {
        foreach (DataRow drMenu in dtMenu.Rows)
        {
            tcmTuua.Add(Convert.ToString(drMenu["parent_id"]), Convert.ToString(drMenu["id"]), Convert.ToString(drMenu["title"]), Convert.ToString(drMenu["url"]), null);
        }
    }

    private void CargarParametros()
    {
        BO_Configuracion objBOConfigura = new BO_Configuracion();
        DataTable dt_parametro = objBOConfigura.ListarAllParametroGenerales(null);
        Hashtable htParametro = new Hashtable();
        for (int i = 0; i < dt_parametro.Rows.Count; i++)
        {
            htParametro.Add(dt_parametro.Rows[i].ItemArray.GetValue(0).ToString().Trim(), dt_parametro.Rows[i].ItemArray.GetValue(5).ToString().Trim());
        }
        Session["htParametro"] = htParametro;
    }



}
