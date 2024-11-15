<%@ page language="C#" autoeventwireup="true" inherits="Cns_Depuracion, App_Web_7ctknflu" responseencoding="utf-8" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Depuración</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_boardingusados" />  
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">
    
        //lhuaman              
      
        function validarExcel() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    return true;
                }
                else {
                    alert("La exportación a excel solo permite " + maxRegistros + " registros");
                    return false;
                }
            }
            else {
                alert("No existen registros para exportar \nSeleccione otros filtros");
                return false;
            }
        }
        
        function validarCampos() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            document.getElementById('lblTotal').innerHTML = "";

            document.getElementById("hlinkResumen").style.display = "none";
            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtHasta').value,
                                   document.getElementById('txtHoraHasta').value,
                                   document.getElementById('txtDesde1').value,
                                   document.getElementById('txtHoraDesde1').value,
                                   document.getElementById('txtHasta1').value,
                                   document.getElementById('txtHoraHasta1').value)){
                  return true;
            } else {
                  document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }

        }
    }
    </script>



</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />                    
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%">
                                <table style="width:100%;">
                                    <tr>
                                        <td >
                                            Fecha Inicio:</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td >
                                        
  <asp:TextBox ID="txtDesde" runat="server" Width="72px" CssClass="textbox" Height="16px"
  MaxLength="10" 
  BackColor="#E4E2DC">
   </asp:TextBox>
   
    <cc1:CalendarExtender ID="CalendarExtender1" 
    runat="server" Format="dd/MM/yyyy" 
    PopupButtonID="imgbtnCalendar1"
    TargetControlID="txtDesde">
    </cc1:CalendarExtender>
    </td>
    <td class="style21">
    <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
    Width="22px" Height="22px" />
    </td>
    <td class="style9">
    <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" 
      ReadOnly="false" Width="56px"></asp:TextBox>
     
    <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
     ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
     CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
    </cc1:MaskedEditExtender>
    
    </td>
    <td class="style1">
        &nbsp;</td>
    <td class="style22">
    <asp:Label ID="lblMolinete" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
    </td>
    <td class="style67">
    
    <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="100%" CssClass="combo2">                                                                               
    </asp:DropDownList>
    
    </td>
    <td class="style22">
        &nbsp;</td>
    <td class="style22">
    <asp:Label ID="lblTablaSincronizacion" runat="server" CssClass="TextoFiltro"></asp:Label>
    </td>
    <td class="style33">
    
    <asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2" >
    </asp:DropDownList>
    
    </td>
    <td class="style37"> &nbsp;</td>
    <td class="style39">      
      <b>
      <asp:LinkButton ID="lbExportar" runat="server" onclick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                                        </td>
    <td>&nbsp;</td>
    </tr>
    <tr>
    <td ></td>
    <td >
    <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
    </td>
    <td ></td>
    <td >
    <asp:Label ID="lblFormatoHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
    </td>
    <td ></td>
    <td >
    <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro" Width="20%" Height="16px" ></asp:Label>
    </td>
    <td >
    <asp:DropDownList ID="ddlEstado" runat="server" Width="100px" CssClass="combo2"> </asp:DropDownList>
    </td>
    <td >&nbsp;</td>
    <td ></td>
    <td > </td>
    <td > </td>
    <td > 
           <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
           <br />
           <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" 
            OnClick="cmdConsultar_Click" Style="cursor: hand;" />
           <br />
           <a href="" id="lnkHabilitar" runat="server" 
             style="cursor: hand;"><b>
            </b></a>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <br />
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
    <td > </td>
    </tr>
    <tr>
    <td >
     Fecha Fin:</td>
    <td class="style20">
    
        &nbsp;</td>
    <td class="style17">
        &nbsp;</td>
     
     <td class="style16">
         &nbsp;</td>
      <td class="style25">
          &nbsp;</td>
      
      <td class="style23">
          &nbsp;</td>
      <td class="style69">
          &nbsp;</td>
      <td class="style23">&nbsp;</td>
      <td class="style23">&nbsp;</td>
      <td class="style34">&nbsp;</td>
      <td class="style38">      
          &nbsp;</td>
       <td class="style40">  &nbsp;</td>
       <td class="style18">  </td>
       </tr>
       <tr>
        <td class="style71">
    <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
           </td>
        <td class="style19">
    
    <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox" Height="16px"
     MaxLength="10"
     BackColor="#E4E2DC" ></asp:TextBox>
     
    <cc1:CalendarExtender 
    ID="CalendarExtender2" 
    runat="server" 
    Format="dd/MM/yyyy" 
    PopupButtonID="imgbtnCalendar2"
    TargetControlID="txtHasta">
    </cc1:CalendarExtender>
    
           </td>
        <td class="style21">
    <asp:ImageButton
     ID="imgbtnCalendar2" 
     ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
     Width="22px" Height="22px" />
           </td>
         <td class="style9">
     <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" 
       ReadOnly="false" Width="56px"></asp:TextBox>
      
      <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
       ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
       CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
      </cc1:MaskedEditExtender>
      
           </td>
          <td class="style1">&nbsp;</td>
          
          <td class="style22">&nbsp;</td>
          <td class="style67">&nbsp;</td>
          <td class="style22"> &nbsp;</td>
          <td class="style22"> &nbsp;</td>
          <td class="style33"> &nbsp;</td>
          <td class="style37"> &nbsp;</td>
            <td class="style39"> &nbsp;</td>
            <td> &nbsp;</td>
            </tr> <tr>
            <td class="style73">
                                            </td>
                                        <td class="style44">
        <asp:Label ID="Label1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                            </td>
                                        <td class="style45">
                                            </td>
                                        <td class="style46">
          <asp:Label ID="lblFormatoHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                            </td>
                                        <td class="style47">
                                            </td>
                                        <td class="style48">
                                            </td>
                                        <td class="style70">
                                            </td>
                                        <td class="style48">
                                            &nbsp;</td>
                                        <td class="style48">
                                            </td>
                                        <td class="style51">
                                            </td>
                                        <td class="style52">
                                            </td>
                                        <td class="style53">
                                            </td>
                                        <td class="style54"> </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje" Width="120px" Height="16px"></asp:Label>
        &nbsp;&nbsp;<br />
              
         <asp:GridView ID="grvDepuracion" runat="server" 
         AutoGenerateColumns="False"
         RowStyle-VerticalAlign="Middle" 
         RowStyle-HorizontalAlign="Center"
         CssClass="grilla"
         BackColor="White"        
         BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"         
         OnPageIndexChanging="grvDepuracion_PageIndexChanging" Width="100%" AllowSorting="True"          
         OnSorting="grvDepuracion_Sorting" 
         OnRowCommand="grvDepuracion_RowCommand" AllowPaging="True"  >                                                                                                                                             
                 
         <SelectedRowStyle CssClass="grillaFila" />
         <PagerStyle CssClass="grillaPaginacion" />
         <HeaderStyle CssClass="grillaCabecera" />
         <AlternatingRowStyle CssClass="grillaFila" />
         
         
         
         <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
         
         
         
         <Columns>
         
          <asp:BoundField DataField="Cod_Sincronizacion" HeaderText="Código"  SortExpression="Cod_Sincronizacion">
           <HeaderStyle Width="10%" />
           <ItemStyle Width="10%" />
          </asp:BoundField>
          
          <asp:BoundField DataField="Sincronizacion" HeaderText="Tabla de Depuración" SortExpression="Sincronizacion" />
         
          <asp:BoundField HeaderText="Código de Molinete" DataField="Cod_Molinete" SortExpression="Cod_Molinete">
          <HeaderStyle Width="10%" />
          <ItemStyle Width="10%" />
          </asp:BoundField>
                                                   
          <asp:BoundField HeaderText="Tipo de Estado" DataField="Tip_Estado"  SortExpression="Tip_Estado" />
           
          
          
          <asp:BoundField HeaderText="Descripción del Estado" DataField="Dsc_Estado" SortExpression="Dsc_Estado">
          <HeaderStyle Width="10%" />
          <ItemStyle Width="10%" />
          </asp:BoundField>
          
          <asp:BoundField HeaderText="Fecha Inicio" DataField="Fecha_Inicio" SortExpression="Fch_Inicio_Registro" >
          <ItemStyle Width="25%" />
          <HeaderStyle Width="25%" />
          </asp:BoundField>
          
              
         <asp:BoundField  HeaderText="Fecha Fin" DataField="Fch_Fin_Registro" SortExpression="Fch_Fin_Registro" >
         <ItemStyle Width="25%" />
         <HeaderStyle Width="25%" />
         </asp:BoundField>       
                                             
         <asp:BoundField HeaderText="Tipo de Depuración" DataField="Tip_Sincronizacion" SortExpression="Tip_Sincronizacion">
         <ItemStyle Width="15%" />
         <HeaderStyle Width="15%" />
         </asp:BoundField>
         
         <asp:BoundField HeaderText="Número de Registro" DataField="Num_Registro" SortExpression="Num_Registro">
         <ItemStyle Width="10%" />
         <HeaderStyle Width="10%" />
         </asp:BoundField>
         
     
                  
         
         </Columns>
         
                     <SelectedRowStyle CssClass="grillaFila" />
                     <PagerStyle CssClass="grillaPaginacion" />
                     <HeaderStyle CssClass="grillaCabecera" />
                     <AlternatingRowStyle CssClass="grillaFila" />
                                                
         </asp:GridView>
         
         <br />
         <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                          
         </ContentTemplate>
                      <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                      </Triggers>
         </asp:UpdatePanel>
         
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr>
                <td>
                    <!-- DATA RESULT ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                            
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
                                        <div id="progressBackgroundFilter">
                                            </div>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="SpacingGrid">
                                &nbsp;</td>
                            <td>
                                    <uc3:OKMessageBox ID="omb" runat="server" />
                            </td>
                            <td class="SpacingGrid">
                                &nbsp;</td>
                            
                        </tr>
                    </table>
                    
                    <%--<asp:BoundField DataField="Tipo_Tabla" HeaderText="Tipo de Tabla" SortExpression="Tipo_Tabla" />--%> <%--<asp:BoundField DataField="Tipo_Tabla" HeaderText="Tipo de Tabla" SortExpression="Tipo_Tabla" />--%>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <!--Declaracion de Calendarios -->
    <!--Declaracion Control Hora -->
    
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
