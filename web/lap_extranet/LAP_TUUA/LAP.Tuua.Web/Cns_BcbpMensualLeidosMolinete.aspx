<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cns_BcbpMensualLeidosMolinete.aspx.cs" Inherits="TuuaExtranet.Cns_BcbpMensualLeidosMolinete" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Extranet LAP - Reporte Mensual BP leidos por el molinete </title>
    <meta name="keypage" content="cns_ticketxfecha" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" --> 
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->   
     <link href="css/Style.css" rel="stylesheet" type="text/css" />
              
    <style type="text/css">
        .style2
        {
            width: 100%;
            height: 30px;
        }
    </style>
              
</head>
<body>
    <form id="form2" runat="server">
        <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="6">
                                        </td>
                                        <td colspan="11" height="35">
                                            <asp:Label ID="lblTitulo" runat="server" 
                                                Text="REPORTE MENSUAL DE BP LEIDOS POR EL MOLINETE" CssClass="TituloBienvenida"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblCompania" runat="server" CssClass="Titulo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td colspan="12">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Creación:</asp:Label>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaLecturaIni" runat="server" CssClass="TextoFiltro">Desde:</asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro">(dd/mm/yyyy)</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td rowspan="2">
                                            
                                        </td>
                                        <td rowspan="2" style="width: 60px;" >
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="Label1" runat="server" CssClass="TextoFiltro">Nro Vuelo:</asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                            <asp:TextBox ID="txtNumVuelo" runat="server" Width="88px" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                onblur="gDescripcion(this)" Height="16px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td rowspan="2" style="width: 60px;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipVuelo" runat="server" CssClass="TextoFiltro" 
                                                Width="100%">Tipo Vuelo:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipVuelo" runat="server" Width="100%" 
                                                CssClass="combo2">
                                            </asp:DropDownList>
                                          </td>
                                        <td style="width:80px">
                                        </td>
                                        <td rowspan="4">
                                                    &nbsp; 
                                                    <b>
                                                        <asp:LinkButton ID="lblExportar" runat="server" onclick="lblExportar_Click">[Exportar a Excel]</asp:LinkButton>
                                                    </b>
                                                    <br />
                                                    <br />
                                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Button ID="btnConsultar" runat="server"
                                                        CssClass="Boton" Style="cursor: hand;" 
                                                        Text="Consultar" onclick="btnConsultar_Click" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
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
                                            
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td colspan="2">
                                            &nbsp;</td>
                                            <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaLecturaFin" runat="server" CssClass="TextoFiltro">Hasta:</asp:Label>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar2" TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>     
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Width="50px">(dd/mm/yyyy)</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipPersona" runat="server" CssClass="TextoFiltro">Tipo Persona:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipPersona" runat="server" Width="100%" 
                                                CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    
                                    </table>
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" class="style2">
                                <hr color="#0099cc" style="width: 100%; height: 0px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="height: 30px; width: 100%;">
                            
                                    
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <cc1:PagingGridView ID="grvPaginacionBoarding" runat="server" AutoGenerateColumns="False"
                                                 BackColor="White" BorderColor="#999999" BorderStyle="None" 
                                                 BorderWidth="1px" CssClass="grilla"
                                                 CellPadding="3" Width="100%" AllowPaging="True" GroupingDepth="3" AllowSorting="True" 
                                                            VirtualItemCount="-1" PageSize="10"
                                                onpageindexchanging="grvPaginacionBoarding_PageIndexChanging" 
                                                onsorting="grvPaginacionBoarding_Sorting" GridLines="Vertical">
                                                <PagerSettings Mode="NumericFirstLast"></PagerSettings>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                <RowStyle BorderStyle="solid" BorderColor="#e6e6e6" BorderWidth="1px" />
                                                <Columns>
                                                     <asp:BoundField HeaderText="Fecha" DataField="Fch_Resumen" 
                                                         SortExpression="Fch_Resumen" >
                                                     <ItemStyle VerticalAlign="Top" />
                                                     </asp:BoundField>
                                                     <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" 
                                                         SortExpression="Tip_Vuelo" >
                                                     <ItemStyle VerticalAlign="Top" />
                                                     </asp:BoundField>
                                                     <asp:BoundField HeaderText="Tipo Persona" DataField="Tip_Pasajero" 
                                                        SortExpression="Tip_Pasajero" >
                                                     <ItemStyle VerticalAlign="Top" />
                                                     </asp:BoundField>
                                                     <asp:BoundField HeaderText="Nro Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                     <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" SortExpression="Cantidad" />
                                                 </Columns>
                                            </cc1:PagingGridView>
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                    <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                                    <br />
                                                    <br />
                                                        <asp:GridView ID="grvResumen" runat="server" BackColor="White" BorderColor="#999999" 
                                                                  BorderStyle="None" BorderWidth="1px" CssClass="grilla" 
                                                        Width="40%" AutoGenerateColumns="False" 
                                                onrowcreated="grvResumen_RowCreated" ShowFooter="True" 
                                                HorizontalAlign="Center" Caption="RESUMEN:" >
                                                        <Columns>
                                                            <asp:BoundField DataField="Tip_Vuelo" HeaderText="Boarding Pass" />
                                                            <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Persona" />
                                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#D1D1D1" Font-Bold="True" />
                                                    </asp:GridView>
                                                    
                                                
                                    </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                             </asp:UpdatePanel>
                             <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="100">
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
                                    
                             </td>
                        </tr>
                    </table>
    </div>
    </form>
</body>
</html>
