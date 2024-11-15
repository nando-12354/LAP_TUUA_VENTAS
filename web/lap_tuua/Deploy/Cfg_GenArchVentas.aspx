<%@ page language="C#" autoeventwireup="true" inherits="Cfg_GenArchVentas, App_Web_ehzg6gwo" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Configuración Generacion Archivo Ventas</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
       <!-- #INCLUDE file="javascript/KeyPress.js" -->
       <!-- #INCLUDE file="javascript/Functions.js" -->
       <!-- INCLUDE file="javascript/validarteclaF5.js" -->
       <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
       

<script language="javascript" type="text/javascript"> 
    
    function metodoClick()
    {        
    CadenaFinal="";
    SoloDatosGrilla="";
        for (i=0; i<260; i++)
        {    
    
        
              if (document.getElementById('idControlLista'+i) != null)
              {                
                 CadenaFinal=CadenaFinal+document.getElementById("HiddenLista"+i).value+'~'+document.getElementById('idControlLista'+i).options[document.getElementById('idControlLista'+i).selectedIndex].value + '|';
              }       
              
              if (document.getElementById('idControlListaAnidado'+i) != null)
              {              
                 CadenaFinal=CadenaFinal+document.getElementById("HiddenListaAnidada"+i).value+'~'+document.getElementById('idControlListaAnidado'+i).options[document.getElementById('idControlListaAnidado'+i).selectedIndex].value + '|';
              }              
                
                      
              if (document.getElementById("idControlTextAnidado"+i)!=null)
              {
                CadenaFinal=CadenaFinal+document.getElementById("HiddenTextAnidado"+i).value+'~'+document.getElementById("idControlTextAnidado"+i).value +'|';                
              }
              
              if (document.getElementById("idControlText"+i)!=null)
              {
                CadenaFinal=CadenaFinal+document.getElementById("HiddenText"+i).value+'~'+document.getElementById("idControlText"+i).value +'|';
              }
              
              
            if (document.getElementById("HiddenTipoGrilla"+i)!=null)
              {
               Tipo=document.getElementById("HiddenTipoGrilla"+i).value + i;              
                  if (document.getElementById("idControlTextGrilla"+ Tipo)!=null)
                  {  
                    valor=  document.getElementById("HiddenCordGrilla"+Tipo).value +'='+  document.getElementById("idControlTextGrilla"+Tipo).value;  
                    SoloDatosGrilla= SoloDatosGrilla+'|'+  valor;
                  }
              }
              
          
          
              if (document.getElementById("CheckboxAnidado"+i)!=null)
              {              
                   if (document.getElementById("CheckboxAnidado"+i).checked==true)
                   {
                    valorCheck=1;
                   }
                   else
                   {
                    valorCheck=0;
                   }                             
                CadenaFinal=CadenaFinal+document.getElementById("HiddenCheckAnidado"+i).value+'~'+ valorCheck +'|';
              }
          
              if (document.getElementById("Checkbox"+i)!=null)
              {
                   if (document.getElementById("Checkbox"+i).checked==true)
                   {
                    valorCheck=1;
                   }
                   else
                   {
                    valorCheck=0;
                   }                
                CadenaFinal=CadenaFinal+document.getElementById("HiddenCheck"+i).value+'~'+ valorCheck +'|';
              }          
        }
            
        document.getElementById("hfCadenaTotal").value=CadenaFinal;
        document.getElementById("hfCadenaGrilla").value=SoloDatosGrilla;
    }


    function validar(e) {
       
        obj=e.srcElement || e.target;
        tecla_codigo = (document.all) ? e.keyCode : e.which;
        if(tecla_codigo==8)return true;
        patron =/[\d.]/;
        tecla_valor = String.fromCharCode(tecla_codigo);
        control=(tecla_codigo==46 && (/[.]/).test(obj.value))?false:true
        return patron.test(tecla_valor) &&  control;
    }


    function Evaluar() {
        
     for (j=0; j<140; j++)
     { //begin for
     if (document.getElementById("Checkbox"+j)!=null)
     { //begin if
        //alert(document.getElementById("Checkbox"+j).checked);
        if(document.getElementById("Checkbox"+j).checked == true)
        {
            for (q = 0; q < 20; q++){            
                if (document.getElementById("idControlTextAnidado"+q)!=null)
                {
                    document.getElementById("idControlTextAnidado"+q).disabled = false;
                }
            }       
        }

        if(document.getElementById("Checkbox"+j).checked == false)
        { 
        //alert(document.getElementById("Checkbox"+j).checked);
            for (q = 0; q < 20; q++){            
                if (document.getElementById("idControlTextAnidado"+q)!=null)
                {
                    document.getElementById("idControlTextAnidado"+q).disabled = true;
                }
            }                   
        }
      }//end if   
     } //end for
    }


   
    
</script>
</head>
<body>

    <form id="form1" runat="server" name="fomulario" >
    <div>
        
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right">
                    <asp:Button ID="btnGrabar" runat="server" CssClass="Boton"  OnClientClick="javascript:metodoClick();" OnClick="btnGrabar_Click" CausesValidation="False" EnableTheming="True" /></td>
            </tr>
            <tr>
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" >
                                </td>
                                <td class="CenterGrid" >                               
                                
                               
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <DIV STYLE="overflow: auto; width: 850px; height: 420px; 
                                            border-left: 0px gray solid; border-bottom: 0px gray solid; 
                                            padding:0px; margin: 0px">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <%  try 
                                                            {
                                                                System.Globalization.CultureInfo culturaPeru = new System.Globalization.CultureInfo("es-PE");
                                                                System.Threading.Thread.CurrentThread.CurrentCulture = culturaPeru;
                                                                
                                                                string lista;
                                                                string lista2 = "";
                                                                string namecolumn = "";
                                                                string sCadena;
                                                                string sCadenaDetalle1;
                                                                string sCadenaDetalle2 = " ";
                                                                string sCadenaDetalle3 = " ";
                                                                int cordenadafinal = 0; 

                                                                LAP.TUUA.CONTROL.BO_Configuracion objConfiguracion = new LAP.TUUA.CONTROL.BO_Configuracion();
                                                                System.Data.DataTable dt_parametrosgenerales = new System.Data.DataTable();
                                                                dt_parametrosgenerales = objConfiguracion.ListarAllParametroGenerales("1");



                                                                //-----------------------
                                                                LAP.TUUA.CONTROL.BO_Configuracion objDetalleParamxId = new LAP.TUUA.CONTROL.BO_Configuracion();
                                                                System.Data.DataTable dt_detalleparametro = new System.Data.DataTable();
                                                                //dt_detalleparametro = objDetalleParamxId.DetalleParametroGeneralxId("LM");

                                                           
                                                            
                                                         for(int i = 0; i < dt_parametrosgenerales.Rows.Count; i++) 
                                                         {                                          
                                                    
                                                    
                                                            switch (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(2).ToString())
                                                            {
                                                                case "I": 
                                                                    //Si contiene valores en Parametros de Detalle
                                                                    if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(3).ToString() == "1")
                                                                    {
                                                                        dt_detalleparametro = objDetalleParamxId.DetalleParametroGeneralxId(dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString());
                                                                       
                                                                        //Obteniendo los nombres de la columna
                                                                        foreach (System.Data.DataColumn myProperty in dt_detalleparametro.Columns)
                                                                        {
                                                                            namecolumn = namecolumn + "<td bgcolor='#ccccff'>" + myProperty.ColumnName.ToString() + "</td>";
                                                                        }
                                                                        
                                                                        //Obteniendo los detalles de la matriz
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
                                                                                sCadenaDetalle1 = "<td><input id='idControlTextGrilla" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + cordenada + "' type='text' value='" + dt_detalleparametro.Rows[y].ItemArray.GetValue(k).ToString() + "' onkeypress='return validar(event)'  style='width: 75px; text-align:right' maxlength='11' /> <input id='HiddenTipoGrilla" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/> <input id='HiddenCordGrilla" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + '=' + dt_detalleparametro.Rows[y].ItemArray.GetValue(0).ToString() + '=' + dt_detalleparametro.Columns[k].ColumnName.ToString() + "'/></td>";
                                                                                sCadenaDetalle2 = sCadenaDetalle2 + sCadenaDetalle1;
                                                                                
                                                                            }
                                                                                                                                              
                                                                            sCadenaDetalle3 = sCadenaDetalle3 + sCadenaDetalle2 + "</tr>";
                                                                        }

                                                                        cordenadafinal = cordenada;

                                                                        Response.Write("<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</p></td><td><table border='1' cellpadding='0' cellspacing='0'>" + "<tr>" + namecolumn + "</tr>" + sCadenaDetalle3 + "</table><table><tr><td style='height: 3px'></td></tr></table></td></tr>");
                                                                        sCadenaDetalle3 = "";
                                                                        namecolumn = "";
                                                                            
                                                                    }
                                                                        
                                                                    //En caso que no contenga Detalle
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
                                                                            //cordenada = i;
                                                                            //cordenadafinal = 0;
                                                                        }
                                                                        
                                                                        if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                                                                        {
                                                                            sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControlTextAnidado" + cordenada + "' type='text' name='anidado' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 320px' /> <input id='HiddenTextAnidado" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() == "KI")
                                                                            {
                                                                                sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' READONLY MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 272px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                                                                            }
                                                                            else
                                                                            {
                                                                                sCadena = "<input id='idControlText" + cordenada + "' " + "type='text' MAXLENGTH=150 value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 372px' /><input id='HiddenText" + cordenada + "' type='hidden' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(0).ToString() + "'/>";
                                                                            }
                                                                        }

                                                                        Response.Write("<tr><td width='380px'><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "  </p></td><td>" + sCadena + "</td></tr>");
                                                                    }

                                                                    
                                                                    break;
                                                                case "C":
                                                                            //Activa el input Check como 1
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


                                                                            Response.Write("<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + " </p></td><td>" + sCadena + "</td></tr>");
                                                                            break;
                                                                case "R":
                                                                            //Si contiene un identificadorPadre
                                                                            if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(7).ToString() != "")
                                                                            {
                                                                                sCadena = "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<input id='idControl" + (i + 1) + "' " + "type='radio' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' style='width: 222px' />";
                                                                            }
                                                                            else
                                                                            {
                                                                                sCadena = "<input id='idControl" + (i + 1) + "' " + "type='radio' value='" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(5).ToString() + "' />";
                                                                            }

                                                                            Response.Write("<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + " </p></td><td>" + sCadena + "</td></tr>");
                                                                    
                                                                            break;

                                                                case "L":
                                                                    //Valida que la columna campolista este llena
                                                                    lista2 = "";
                                                                    if (dt_parametrosgenerales.Rows[i].ItemArray.GetValue(6).ToString() != null)
                                                                    {
                                                                        //Establecer datatable con datos del estado de compañia
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


                                                                        Response.Write("<tr><td><p class='TextoFiltro'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</p></td><td>" + sCadena + "</td></tr>");
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

                                                                        Response.Write("<tr align='left'><td><br /><br />" + dt_parametrosgenerales.Rows[i].ItemArray.GetValue(1).ToString() + "</td><td>     </td></tr>");
                                                                        break;
                                                              }

                                                            
                                                        }

                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                Response.Redirect("PaginaError.aspx");
                                                            }
                                                     
                                                     %>
                                                    </table>
                                                </DIV>
                                            </ContentTemplate>                                            
                                        </asp:UpdatePanel>
                                    
        
                                
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfCadenaTotal" runat="server" />
        <asp:HiddenField ID="hfCadenaGrilla" runat="server" />
    </div>
    <uc3:OKMessageBox ID="omb" runat="server" />
    </form>

</body>
</html>
