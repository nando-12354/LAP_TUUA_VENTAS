<%@ page language="C#" autoeventwireup="true" inherits="Cns_Sincronizacion, App_Web_tx1el90t" responseencoding="utf-8" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register src="UserControl/MensajeSincroniza.ascx" tagname="MensajeSincroniza" tagprefix="uc3" %>
<%@ Register src="UserControl/MensajeSincroniza_Cancelar.ascx" tagname="MensajeSincroniza_Cancelar" tagprefix="uc5" %>
<%@ Register src="UserControl/questionmessage.ascx" tagname="questionmessage" tagprefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Sincronización</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_boardingusados" />  
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    

    <style type="text/css">
        .style21
        {
            width: 31px;
            height: 16px;
        }
        .style22
        {
            width: 90px;
            height: 22px;
        }
        .style23
        {
            height: 32px;
            width: 90px;
        }
        .style35
        {
            width: 137px;
            height: 22px;
        }
        .style36
        {
            width: 137px;
            height: 32px;
        }
        .style37
        {
            width: 19px;
        }
        .style39
        {
            width: 12px;
        }
        .style43
        {
            color: #284EBB;
            height: 22px;
        }
        .style55
        {
            width: 111px;
            height: 22px;
        }
        .style56
        {
            height: 32px;
            width: 111px;
        }
        .style74
        {
            width: 19px;
            height: 32px;
        }
        .style77
        {
            height: 32px;
            width: 12px;
        }
        .style83
        {
            height: 29px;
            width: 90px;
        }
        .style84
        {
            width: 137px;
            height: 29px;
        }
        .style85
        {
            height: 29px;
            width: 111px;
        }
        .style88
        {
            height: 2px;
            width: 90px;
        }
        .style89
        {
            width: 137px;
            height: 2px;
        }
        .style90
        {
            height: 2px;
            width: 111px;
        }
        .style96
        {
            width: 31px;
            height: 18px;
        }
        .style97
        {
            height: 18px;
        }
        .style102
        {
            width: 98px;
        }
        .style103
        {
            width: 54px;
            height: 18px;
        }
        .style111
        {
            width: 54px;
            height: 32px;
        }
        .style112
        {
            width: 54px;
        }
        .style128
        {
        }
        .style143
        {
            height: 32px;
            width: 99px;
        }
        .style148
        {
            height: 2px;
            width: 12px;
        }
        .style151
        {
            height: 29px;
            width: 12px;
        }
        .style152
        {
            width: 63px;
            height: 22px;
        }
        .style153
        {
            height: 32px;
            width: 63px;
        }
        .style154
        {
            height: 29px;
            width: 63px;
        }
        .style155
        {
            height: 2px;
            width: 63px;
        }
        .style157
        {
            width: 12px;
            height: 22px;
        }
        .style158
        {
            width: 19px;
            height: 22px;
        }
        .style162
        {
            width: 54px;
            height: 34px;
        }
        .style163
        {
            height: 34px;
            width: 98px;
        }
        .style164
        {
            height: 34px;
        }
        .style165
        {
            width: 98px;
            height: 18px;
            color: #000000;
        }
        .style166
        {
            width: 31px;
        }
        .style168
        {
            width: 54px;
            height: 16px;
        }
        .style169
        {
            width: 98px;
            height: 16px;
        }
        .style170
        {
            height: 16px;
        }
        .style173
        {
            color: #000080;
            font-weight: bold;
        }
        .style174
        {
            width: 99px;
            height: 18px;
            color: #000000;
        }
        .style175
        {
            width: 99px;
        }
        </style>

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
                                        <td class="style43">
                                            <span class="style173">Fecha Inicio</span><br />
                                        </td>
    <td class="style152">
                                        </td>
    <td class="style35">
    
                                        </td>
    <td class="style157">
    
                                        </td>
    <td class="style22">
                                        </td>
    <td class="style55">
    
                                        </td>
    <td class="style158"> </td>
    <td class="style157"></span></td>
    </tr>
                                    <tr>
                                        <td class="style128" rowspan="2">
                                            <table style="width:1906%;">
            <tr>
                <td class="style162">
                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td class="style163">
    
   
                                            <b>
               
                        
        
  <asp:TextBox ID="txtDesde" runat="server" Width="72px" CssClass="textbox" Height="16px"
  MaxLength="10" onfocus="this.blur();" 
  BackColor="#E4E2DC">
   </asp:TextBox>
   
    <cc1:CalendarExtender ID="CalendarExtender1" 
    runat="server" Format="dd/MM/yyyy" 
    PopupButtonID="imgbtnCalendar1"
    TargetControlID="txtDesde">
    </cc1:CalendarExtender>
    
       
    
                    </b>
     
                            </td>
              <%--  <td class="style21">
    <asp:ImageButton
     ID="imgbtnCalendar2" 
     ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
     Width="22px" Height="22px" />
    
                </td>--%>
                 <td class="style164">
    
    
                <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                Width="22px" Height="22px" />
                                                    
                            </td>
                            <td class="style164">
                <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" 
                  ReadOnly="false" Width="56px"></asp:TextBox>
                 
                <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                 ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                 CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                </cc1:MaskedEditExtender>
    
                </td>
            </tr>
            <tr>
                <td class="style103">
                    &nbsp;</td>
                <td class="style165">
                    (dd/mm/yyyy)</td>
                <td class="style96">
                </td>
                <td class="style97">
          <asp:Label ID="lblFormatoHoraHasta0" runat="server" CssClass="TextoFiltro" 
                        Text="(hh:mm:ss)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style168">
                    </td>
                <td class="style169">
                    </td>
                <td class="style21">
                    </td>
                <td class="style170">
                    </td>
            </tr>
        </table>
                                        </td>
    <td class="style153">
    <asp:Label ID="lblMolinete" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
    </td>
    <td class="style36">
    
    <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="100%" CssClass="combo2">                                                                               
    </asp:DropDownList>
    
    </td>
    <td class="style77">
    
        &nbsp;</td>
    <td class="style23">
    <asp:Label ID="lblTablaSincronizacion" runat="server" CssClass="TextoFiltro"></asp:Label>
    </td>
    <td class="style56">
    
    <asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2" >
    </asp:DropDownList>
    
    </td>
    <td class="style74"> </td>
    <td class="style77">       
      <b>
      <asp:LinkButton ID="lbExportar" runat="server" onclick="lbExportar_Click">[Exportar 
        a Excel]</asp:LinkButton></b>
                                        </td>
    </tr>
    <tr>
    <td class="style154">
    <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro" Width="20%" Height="16px" ></asp:Label>
    </td>
    <td class="style84">
    <asp:DropDownList ID="ddlEstado" runat="server" Width="100px" CssClass="combo2"> </asp:DropDownList>
    </td>
    <td class="style151">
        </td>
    <td class="style83"></td>
    <td class="style85"> </td>
    <td class="style37" rowspan="2">       
        &nbsp;</td>
    <td class="style39" rowspan="2">       
           <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
           <br />
           <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" 
            OnClick="cmdConsultar_Click" Style="cursor: hand;" />
           <br />
           <a href="" id="lnkHabilitar" runat="server" 
             style="cursor: hand;"><b>
            </b></a>&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <br />
           <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" 
             Style="cursor: hand;" CommandName = "Cancelar" onclick="btnCancelar_Click" />
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
    </tr>
    <tr>
    <td class="style102">
        <span class="style173">Fecha Fin</span><table style="width:1871%;">
            <tr>
                <td class="style111">
    <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td class="style143">
    
  <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox" Height="16px"
  MaxLength="10" onfocus="this.blur();" 
  BackColor="#E4E2DC">
   </asp:TextBox>
   
   
                                            <b>
               
                        
        
    <cc1:CalendarExtender 
    ID="CalendarExtender2" 
    runat="server" 
    Format="dd/MM/yyyy" 
    PopupButtonID="imgbtnCalendar2"
    Enabled="True"
    TargetControlID="txtHasta">
    </cc1:CalendarExtender>
    
       
    
                    </b>
     
                            </td>
              <%--  <td class="style21">
    <asp:ImageButton
     ID="imgbtnCalendar2" 
     ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
     Width="22px" Height="22px" />
    
                </td>--%>
                 <td class="style166">
    
    
                     <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
    Width="22px" Height="22px" />
                                        
                </td>
                <td>
                    <b>
     <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" 
       ReadOnly="false" Width="56px"></asp:TextBox>
      
                    </b>
      
      <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
       ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
       CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
      </cc1:MaskedEditExtender>
      
                </td>
            </tr>
            <tr>
                <td class="style103">
                    &nbsp;</td>
                <td class="style174">
                    (dd/mm/yyyy)</td>
                <td class="style96">
                </td>
                <td class="style97">
          <asp:Label ID="lblFormatoHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style112">
                    &nbsp;</td>
                <td class="style175">
                    &nbsp;</td>
                <td class="style166">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
                                            </td>
      
      <td class="style155">
      <asp:Label ID="lblTipoSincronizacion" runat="server" CssClass="TextoFiltro"></asp:Label>
      </td>
      <td class="style89">
      <asp:DropDownList ID="ddlTipoVuelo" runat="server" Width="100%" CssClass="combo2">
      </asp:DropDownList>
      </td>
      <td class="style148">
          &nbsp;</td>
      <td class="style88">&nbsp;</td>
    
    
      <td class="style90"></td>
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
              
         <asp:GridView ID="grvSincronizacion" runat="server" 
         AutoGenerateColumns="False"
         RowStyle-VerticalAlign="Middle" 
         RowStyle-HorizontalAlign="Center"
         CssClass="grilla"
         BackColor="White"        
         BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"         
         OnPageIndexChanging="grvSincronizacion_PageIndexChanging" Width="100%" AllowSorting="True"          
         OnSorting="grvSincronizacion_Sorting" 
         OnRowCommand="grvSincronizacion_RowCommand" AllowPaging="True"  >                                                                                                                                             
                 
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
          
          <asp:BoundField DataField="Sincronizacion" HeaderText="Tabla de Sincronización" SortExpression="Sincronizacion" />
         
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
                                             
         <asp:BoundField HeaderText="Tipo de Sincronización" DataField="Tip_Sincronizacion" SortExpression="Tip_Sincronizacion">
         <ItemStyle Width="15%" />
         <HeaderStyle Width="15%" />
         </asp:BoundField>
         
         <asp:BoundField HeaderText="Número de Registro" DataField="Num_Registro" SortExpression="Num_Registro">
         <ItemStyle Width="10%" />
         <HeaderStyle Width="10%" />
         </asp:BoundField>
         
         <asp:BoundField HeaderText="Número Registro Error"  DataField="Num_RegErr" SortExpression="Num_RegErr">
         <ItemStyle Width="10%" />
         <HeaderStyle Width="10%" />
         </asp:BoundField>         
         
        <asp:TemplateField SortExpression="Check" ItemStyle-Width="18%" >
        <ItemTemplate>
        <asp:CheckBox ID="chkSeleccionar" runat="server" />
        </ItemTemplate>
        <HeaderTemplate>
        
        &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Sort" CommandArgument="Check"
        Text="Seleccionar"></asp:LinkButton>
        </HeaderTemplate>
            <ItemStyle Width="18%" />
        </asp:TemplateField>
                  
         
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
                            <td style="font-weight: 700" color="#0099cc">
                                    <uc3:MensajeSincroniza ID="omb" runat="server" />
                            </td>
                            <td style="font-weight: 700" color="#0099cc">
                                    <uc5:MensajeSincroniza_Cancelar ID="omc" runat="server" />
                            </td>
                            <td style="font-weight: 700" color="#0099cc">
                                    <uc6:questionmessage ID="omp" runat="server" />
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
