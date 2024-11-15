
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
using System.Globalization;
using LAP.TUUA.ALARMAS;

public partial class Cfg_ParametroGeneral : System.Web.UI.Page
{
    protected Hashtable htLabels;
    protected bool Flg_Error;
    protected BO_Error objError;
    public string sCadena;
    System.Data.DataTable dt_detalleparametro = new System.Data.DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        CultureInfo culturaPeru = new CultureInfo("es-PE");
        System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;

        if (!IsPostBack)
        {
            this.btnGrabar.Text = "Grabar >>";
            cargarControles();

            

        }
    }


    protected void btnGrabar_Click(object sender, EventArgs e)
    {

        string sDatosFormularios = this.hfCadenaTotal.Value;
        string sDatosGrilla = this.hfCadenaGrilla.Value;

        BO_Configuracion objConfigParamGeneral = new BO_Configuracion((string)Session["Cod_Usuario"], (string)Session["Cod_Modulo"], (string)Session["Cod_SubModulo"]);
        try
        {
            if (sDatosFormularios != "" )
            {
                if (objConfigParamGeneral.GrabarParametroGeneral(sDatosFormularios, sDatosGrilla, "0"))
                {
                    lblMensaje.Text = "";
                    ///<summary>GeneraAlarma</summary>
                    string IpClient = Request.UserHostAddress;
                    GestionAlarma.Registrar(HttpContext.Current.Server.MapPath(""), "W0000009", "002", IpClient, "1", "Alerta W0000009", "Parametros Generales Actualizados, Usuario: " + Convert.ToString(Session["Cod_Usuario"]), Convert.ToString(Session["Cod_Usuario"]));

                    omb.ShowMessage("Parametros registrados correctamente", "Parametros generales", "Cfg_ParametroGeneral.aspx");
                    cargarControles();
                }
                else
                {

                }
            }
            else
            {
                lblMensaje.Text = "Error en pagina";
            }
 
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

    public void cargarControles()
    {
        try 
        {
            System.Globalization.CultureInfo culturaPeru = new System.Globalization.CultureInfo("es-PE");
            System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
            
            string lista;
            string lista2 = "";
            string namecolumn = "";
            
            string sCadenaDetalle1;
            string sCadenaDetalle2 = " ";
            string sCadenaDetalle3 = " ";
            int cordenadafinal = 0; 

            LAP.TUUA.CONTROL.BO_Configuracion objConfiguracion = new LAP.TUUA.CONTROL.BO_Configuracion();
            System.Data.DataTable dt_parametrosgenerales = new System.Data.DataTable();
            dt_parametrosgenerales = objConfiguracion.ListarAllParametroGenerales(null);

        

            
            LAP.TUUA.CONTROL.BO_Configuracion objDetalleParamxId = new LAP.TUUA.CONTROL.BO_Configuracion();
            
            

       
        
     for(int i = 0; i < dt_parametrosgenerales.Rows.Count; i++) 
     {                                          


        switch (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(2).ToString())
        {
            case "I":
                ///<summary>Si contiene valores en Parametros de Detalle</summary>
                if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(3).ToString() == "1")
                {
                    dt_detalleparametro = objDetalleParamxId.DetalleParametroGeneralxId(dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString());

                    ///<summary>Obteniendo los nombres de la columna</summary>
                    foreach (System.Data.DataColumn myProperty in dt_detalleparametro.Columns)
                    {
                        namecolumn = namecolumn + "<td bgcolor='#ccccff'>" + myProperty.ColumnName.ToString() + "</td>";
                    }

                    ///<summary>Obteniendo los detalles de la matriz</summary>
                   int cordenada = i;
                   
                   if (cordenadafinal != 0)
                   {
                       cordenada=cordenadafinal;
                       cordenadafinal = 0;
                   }
                   
                        
                    for (int y = 0; y < dt_detalleparametro.Rows.Count; y++)
                    {
                        cordenada = (Convert.ToInt32(cordenada) + cordenadafinal);//Convert.ToInt32(y));

                        sCadenaDetalle2 = "<tr><td><p class='TextoFiltro'>" + dt_detalleparametro.Rows[y].ItemArray.GetValue(0).ToString() + "</p></td>";
                        
                        for (int k = 1; k < dt_detalleparametro.Columns.Count; k++)
                        {
                            cordenada = cordenada + 1;
                            sCadenaDetalle1 = "<td><input id='idControlTextGrilla" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + cordenada + "' type='text' value='" + dt_detalleparametro.Rows[y].ItemArray.GetValue(k).ToString() + "' onkeypress='return validar(event)' onblur='gDecimal(this)' style='width: 75px; text-align:right' maxlength='11' /> <input id='HiddenTipoGrilla" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> <input id='HiddenCordGrilla" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + '=' + dt_detalleparametro.Rows[y].ItemArray.GetValue(0).ToString() + '=' + dt_detalleparametro.Columns[k].ColumnName.ToString() + "'/></td>";
                            sCadenaDetalle2 = sCadenaDetalle2 + sCadenaDetalle1;
                            
                        }
                                                                                          
                        sCadenaDetalle3 = sCadenaDetalle3 + sCadenaDetalle2 + "</tr>";
                    }

                    cordenadafinal = cordenada;
                    
                    lblcontroles.Text = lblcontroles.Text + "<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</p></td><td><table border='1' cellpadding='0' cellspacing='0'>" + "<tr>" + namecolumn + "</tr>" + sCadenaDetalle3 + "</table><table><tr><td style='height: 3px'></td></tr></table></td></tr>";
                    sCadenaDetalle3 = "";
                    namecolumn = "";
                        
                }

                ///<see>En caso que no contenga Detalle</see>
                else
                {
                    int cordenada = i;
                    if (cordenadafinal != 0)
                    {
                        cordenada = cordenadafinal+1;
                        cordenadafinal = cordenadafinal + 1;
                    }
                    else
                    {
                        ///<see>cordenada = i;</see>
                       ///<see>cordenadafinal = 0;</see>
                    }
                    
                    if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                    {

                        Boolean isValidarRehabilitado = false;
                        String sLimiteTotal = string.Empty;

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "RTM")
                        {
                            isValidarRehabilitado = true;
                            sLimiteTotal = Define.REH_TICKET_CANT_LIMIT;
                        }

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "RM")
                        {
                            isValidarRehabilitado = true;
                            sLimiteTotal = Define.REH_BCBP_CANT_LIMIT;
                        }
                        ///<summary>lhuaman - cambios<summary>

                        Boolean isValidarEstadistico = false;
                        String sLimite = string.Empty;

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "LD")
                        {
                            isValidarEstadistico = true;
                            sLimite = Define.EST_DIAS_CANT_LIMIT;
                        }
                        
                        ///<summary>lhuaman - cambios<summary>

                        Boolean isfecha = false;
                        String svalor = string.Empty;

                      

                        if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "NUM")
                        {
                            if (isValidarRehabilitado)
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='return ValidarRehabilitacion(this,\"" + sLimiteTotal + "\")' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> <input id='HiddenValidacion" + cordenada + "' type='hidden' value='" + sLimiteTotal + "'/>";
                            }
                            else
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='gDecimal(this)'/> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }

                            if (isValidarEstadistico)
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='return ValidarEstadistico(this,\"" + sLimite + "\")' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> <input id='HiddenValidacion" + cordenada + "' type='hidden' value='" + sLimite + "'/>";
                            }
                            else
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='gDecimal(this)'/> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                           

                        }
                        else
                        {
                            if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "TXT")
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  style='width: 320px' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            else
                            {
                                if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "DEC")
                                {
                                    sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onkeypress='return validar(event)' style='width: 320px' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                                }
                            }                          

                        }

                         
                      
                        
                    }
                    else
                    {
                       
                        
                        Boolean isValidarRehabilitado = false;
                        String sLimiteTotal = string.Empty;

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "RTM")
                        {
                            isValidarRehabilitado = true;
                            sLimiteTotal = Define.REH_TICKET_CANT_LIMIT;
                        }

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "RM")
                        {
                            isValidarRehabilitado = true;
                            sLimiteTotal = Define.REH_BCBP_CANT_LIMIT;
                        }
                        ///<summary>lhuaman - cambios<summary>

                        Boolean isValidarEstadistico = false;
                        String sLimite = string.Empty;

                        if (dt_parametrosgenerales.Rows[i]["Identificador"].ToString() == "LD")
                        {
                            isValidarEstadistico = true;
                            sLimite = Define.EST_DIAS_CANT_LIMIT;
                        }

                        
                        ///<summary>lhuaman - cambios<summary>

                        Boolean isfecha = false;
                        String svalor = string.Empty;

                       

                 
                        if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "NUM")
                        {
                            if (isValidarRehabilitado)
                            {
                                sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' onKeyPress='return ValidarRehabilitacion(event, \"" + sLimiteTotal + "\")' style='width: 372px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/><input id='HiddenValidacion" + cordenada + "' type='hidden' value='" + sLimiteTotal  + "'/>";
                            }
                            else
                            {
                                sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' onKeyPress='return ValidarEnteros(event)' style='width: 372px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            ///<summary>lhuaman - cambios<summary>
                            if (isValidarEstadistico)
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='return ValidarEstadistico(this,\"" + sLimite + "\")' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> <input id='HiddenValidacion" + cordenada + "' type='hidden' value='" + sLimite + "'/>";
                            }
                            else
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "'  onKeyPress='return ValidarEnteros(event)' style='width: 320px' onblur='gDecimal(this)'/> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            
                         
                        }
                        else
                        {
                            if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "TXT")
                            {
                                sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 372px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            else
                            {
                                if (dt_parametrosgenerales.Rows[i]["TipoValor"].ToString() == "DEC")
                                {
                                    sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' onkeypress='return validar(event)' style='width: 372px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                                }
                            }
                 
                        }

                       
                    }

                    lblcontroles.Text = lblcontroles.Text + "<tr><td width='380px'><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "  </p></td><td>" + sCadena + "</td></tr>";
                }

                
                break;
            case "C":
                        ///<summary>Activa el input Check como 1 </summary>
                        if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() == "1")
                        {
                            if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='CheckboxAnidado" + i + "' type='checkbox' name='checkAnidado" + i + "' checked='checked'  /><input id='HiddenCheckAnidado" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            else
                            {
                                sCadena = "<input id='Checkbox" + i + "' type='checkbox' checked='checked' name='check" + i + "'  onclick='Evaluar()'/><input id='HiddenCheck" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                        }
                        else
                        {
                            if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                            {
                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='CheckboxAnidado" + i + "' type='checkbox' name='checkAnidado" + i + "'  /><input id='HiddenCheckAnidado" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                            else
                            {
                                sCadena = "<input id='Checkbox" + i + "' type='checkbox' name='check" + i + "' onclick='Evaluar()' /><input id='HiddenCheck" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                            }
                        }


                        lblcontroles.Text = lblcontroles.Text + "<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + " </p></td><td>" + sCadena + "</td></tr>";
                        break;
            case "R":
                        ///<summary>Si contiene un identificadorPadre</summary>
                        if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                        {
                            sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControl" + (i + 1) + "' " + "type='radio' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 222px' />";
                        }
                        else
                        {
                            sCadena = "<input id='idControl" + (i + 1) + "' " + "type='radio' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' />";
                        }

                        lblcontroles.Text = lblcontroles.Text  + "<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + " </p></td><td>" + sCadena + "</td></tr>";
                
                        break;

            case "L":
                        ///<summary>Valida que la columna campolista este llena</summary>
                lista2 = "";
                if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(6).ToString() != null)
                {
                    ///<summary>Establecer datatable con datos del estado de compañia</summary>
                    LAP.TUUA.CONTROL.BO_Consultas objListaCampos = new LAP.TUUA.CONTROL.BO_Consultas();
                    System.Data.DataTable dt_listaCombo = new System.Data.DataTable();
                    dt_listaCombo = objListaCampos.ListaCamposxNombre(dt_parametrosgenerales.Rows[i].ItemArray.GetValue(6).ToString());
                    
                    for (int j = 0; j < dt_listaCombo.Rows.Count; j++)
                    {
                        if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() == dt_listaCombo.Rows[j].ItemArray.GetValue(1).ToString())
                        {

                            lista = "<option selected value='" + dt_listaCombo.Rows[j].ItemArray.GetValue(1).ToString() + "'>" + dt_listaCombo.Rows[j].ItemArray.GetValue(3).ToString() + "</option>";
                            lista2 = lista + lista2;
                        }
                        else
                        {

                            lista = "<option value='" + dt_listaCombo.Rows[j].ItemArray.GetValue(1).ToString() + "'>" + dt_listaCombo.Rows[j].ItemArray.GetValue(3).ToString() + "</option> ";
                            lista2 = lista2 + lista ;
                        }
                    }
                }
                    if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                    {
                        sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<select id='idControlListaAnidado" + i + "' name='ListaAnidada'  style='width: 160px' >" + lista2 + "</select>  <input id='HiddenListaAnidada" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> ";
                    }
                    else
                    {
                        sCadena = "<select id='idControlLista" + i + "'  name='Lista' style='width: 160px' >" + lista2 + "</select><input id='HiddenLista" + i + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> ";
                    }


                    lblcontroles.Text = lblcontroles.Text + "<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</p></td><td>" + sCadena + "</td></tr>";
                    break;

            case "X":
                    if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() == "")
                    {
                        sCadena = "<input id='idControl" + (i + 1) + "' " + "type='text' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' />";
                    }
                    else
                    {
                        sCadena = "<input id='idControl" + (i + 1) + "' " + "type='text' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' />";
                    }

                    lblcontroles.Text = lblcontroles.Text + "<tr align='left'><td><br /><br />" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</td><td>     </td></tr>";
                    break;
          }

        
    }
       dt_parametrosgenerales = null;

    }
    catch (Exception ex)
    {
        Response.Redirect("PaginaError.aspx");
    }
                                                     




    }
}


