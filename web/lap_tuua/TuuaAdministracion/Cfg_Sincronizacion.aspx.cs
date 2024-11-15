///V.1.4.6.0
///Luz Huaman
///Copyright ( Copyright © HIPER S.A. )

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
using LAP.TUUA.ENTIDADES;
using LAP.TUUA.CONTROL;
using LAP.TUUA.UTIL;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections.Generic;
using System.Text;

public partial class Cfg_Sincronizacion : System.Web.UI.Page
{

    protected Hashtable htLabels;
    public string sName;
    BO_Consultas objBOConsultas = new BO_Consultas();
    UIControles objCargaCombo = new UIControles();
    protected bool Flg_Error;
    protected BO_Error objError;
    BO_Configuracion objBOConfiguracion = new BO_Configuracion();
    public string horas;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            
            CargarDatos("CL");
            htLabels = LabelConfig.htLabels;

            try
            {

             


            }
            catch (Exception ex)
            {
                Flg_Error = true;
                ErrorHandler.Cod_Error = Define.ERR_008;
                ErrorHandler.Trace(ErrorHandler.htErrorType[Define.ERR_008].ToString(), ex.Message);
            }
            finally
            {
                if (Flg_Error)
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            CargarFiltros();

           
            

        }
        

    }
    #region Cargar/Guardas Filtros de Consulta
    public void CargarFiltros()
    {
        try
        {

            ///<summary>Carga filtro Tipo Estado Sincronización </summary>                  
            DataTable dt_tiposincronizacion = objBOConsultas.ListaCamposxNombre("TipoSincronizacion");
            objCargaCombo.LlenarCombo(ddlestado, dt_tiposincronizacion, "Cod_Campo", "Dsc_Campo", false, false);

        }
        catch (Exception ex)
        {
            Flg_Error = true;
            ErrorHandler.Trace(ErrorHandler.Desc_Mensaje, ex.Message);
        }
        finally
        {
            if (Flg_Error)
            {
                Response.Redirect("PaginaError.aspx");
            }
        }
    }




    #endregion



    public void RegistrarProgramacion()
    {
        try
        {
            ListaDeCampo objListaDeCampo = new ListaDeCampo();
            objListaDeCampo.SNomCampo = ddlestado.Text;
            objListaDeCampo.SCodCampo = lblcodrelativo.Text;
            objListaDeCampo.SCodRelativo = lblestado.Text;


            objBOConfiguracion = new BO_Configuracion((string)Session["Cod_Usuario"],
                (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

            if (objBOConfiguracion.actualizarHoras(objListaDeCampo))
            {

                omb.ShowMessage("Configurado Correctamente", "Mensaje");

            }



        }
        catch (Exception exc)
        {
            Response.Redirect("PaginaError.aspx");
        }


    }
    #region Boton Aceptar
    protected void btnAceptar_Click1(object sender, EventArgs e)
    {

        if (ddlestado.Text.Equals("0"))
        {
            omb.ShowMessage("Elegir una opcion", "Error");

        }
        else
        {
            #region Horas
            ///<see>checkprueba - 00:00</see>

            if (CheckPrueba1.Text.Equals("00:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba1.Checked)
                    {

                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba1.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 01:00<see>

            if (CheckPrueba2.Text.Equals("01:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba2.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba2.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 02:00<see>

            if (CheckPrueba3.Text.Equals("02:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba3.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba3.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }


            ///<see>checkprueba - 03:00<see>

            if (CheckPrueba4.Text.Equals("03:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba4.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba4.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 04:00<see>

            if (CheckPrueba5.Text.Equals("04:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba5.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba5.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 05:00<see>

            if (CheckPrueba6.Text.Equals("05:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba6.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba6.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 06:00<see>

            if (CheckPrueba7.Text.Equals("06:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba7.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba7.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 07:00<see>

            if (CheckPrueba8.Text.Equals("07:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba8.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba8.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 08:00<see>

            if (CheckPrueba9.Text.Equals("08:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba9.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba9.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            
            ///<see>checkprueba - 09:00<see>

            if (CheckPrueba10.Text.Equals("09:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba10.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba10.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 10:00<see>

            if (CheckPrueba11.Text.Equals("10:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba11.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba11.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 11:00<see>

            if (CheckPrueba12.Text.Equals("11:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba12.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba12.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 12:00<see>

            if (CheckPrueba13.Text.Equals("12:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba13.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba13.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 13:00<see>

            if (CheckPrueba14.Text.Equals("13:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba14.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba14.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            
            ///<see>checkprueba - 14:00<see>

            if (CheckPrueba15.Text.Equals("14:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba15.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba15.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 15:00<see>

            if (CheckPrueba16.Text.Equals("15:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba16.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba16.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }

            ///<see>checkprueba - 16:00<see>

            if (CheckPrueba17.Text.Equals("16:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba17.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba17.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 17:00<see>

            if (CheckPrueba18.Text.Equals("17:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba18.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba18.Text;

                    RegistrarProgramacion();

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 18:00<see>

            if (CheckPrueba19.Text.Equals("18:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba19.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba19.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 19:00<see>

            if (CheckPrueba20.Text.Equals("19:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba20.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba20.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 20:00<see>

            if (CheckPrueba21.Text.Equals("20:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba21.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba21.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 21:00<see>

            if (CheckPrueba22.Text.Equals("21:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba22.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba22.Text;

                    RegistrarProgramacion();

                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 22:00<see>

            if (CheckPrueba23.Text.Equals("22:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba23.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba23.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            ///<see>checkprueba - 23:00<see>

            if (CheckPrueba24.Text.Equals("23:00"))
            {
                if (ddlestado.Text.Equals("CL") || ddlestado.Text.Equals("LC"))
                {

                    if (CheckPrueba24.Checked)
                    {
                        lblestado.Text = "1";

                    }
                    else
                    {
                        lblestado.Text = "0";

                    }

                    lblcodrelativo.Text = CheckPrueba24.Text;

                    RegistrarProgramacion();


                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Habilitar", "FinEnvio('btnAceptar');", true);

                }

                else
                {
                    Response.Redirect("PaginaError.aspx");
                }
            }
            #endregion

        }


    }
    # endregion


   

    private void CargarDatos(String Tipo)
    {

        objBOConfiguracion = new BO_Configuracion((string)Session["Cod_Usuario"],
    (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);

       CheckPrueba1.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "00:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba2.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "01:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba3.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "02:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba4.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "03:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba5.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "04:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba6.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "05:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba7.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "06:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba8.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "07:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba9.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "08:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba10.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "09:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba11.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "10:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba12.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "11:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba13.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "12:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba14.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "13:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba15.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "14:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba16.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "15:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba17.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "16:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba18.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "17:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba19.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "18:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba20.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "19:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba21.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "20:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba22.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "21:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba23.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "22:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;
       CheckPrueba24.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "23:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ? true : false;

       # region check all

    

       if ((CheckPrueba1.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "00:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba2.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "01:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba3.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "02:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba4.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "03:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba5.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "04:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" ) &&
           (CheckPrueba6.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "05:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba7.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "06:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba8.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "07:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba9.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "08:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba10.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "09:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba11.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "10:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba12.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "11:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba13.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "12:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba14.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "13:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba15.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "14:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1") &&
           (CheckPrueba16.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "15:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1")&&
           (CheckPrueba17.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "16:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba18.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "17:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba19.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "18:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba20.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "19:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba21.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "20:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba22.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "21:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba23.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "22:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1" )&&
           (CheckPrueba24.Checked = ((objBOConfiguracion.ObtenerListaDeCampo(Tipo, "23:00").Select())[0]["Cod_Relativo"]).ToString().Trim() == "1"))
       {           
           checkselec.Checked = true;
       }
       else
       {
           checkselec.Checked = false;
       }

       #endregion
    }

    # region Boton Cancelar

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        String Tipo = (String)ViewState["Tipo"];
        CargarDatos(Tipo);
    }

    #endregion

   

    protected void ddlestado_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["Tipo"] = ddlestado.SelectedValue;
        CargarDatos(ddlestado.SelectedValue);
    }

}

