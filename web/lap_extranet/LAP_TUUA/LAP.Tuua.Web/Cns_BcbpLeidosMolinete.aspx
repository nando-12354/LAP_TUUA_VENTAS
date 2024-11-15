<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cns_BcbpLeidosMolinete.aspx.cs"
    Inherits="TuuaExtranet.Cns_BcbpLeidosMolinete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CnsDetBoardingPass.ascx" TagName="CnsDetBoardingPass"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Extranet LAP - Reporte BP leidos por el molinete </title>
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
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
            <tr>
                <td align="left" style="height: 30px; width: 100%;">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 20px;" rowspan="6">
                            </td>
                            <td colspan="13" height="35">
                                <asp:Label ID="lblTitulo" runat="server" Text="REPORTE DE BP LEIDOS POR EL MOLINETE"
                                    CssClass="TituloBienvenida"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="lblCompania" runat="server" CssClass="Titulo"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <!-- TITLE -->
                            <td colspan="14">
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
                                            <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"
                                                Width="72px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro">Hora Inicio:</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 25px;">
                                <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                    Width="22px" Height="22px" />
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                    onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblHoraIni" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AutoComplete="False"
                                    ClearMaskOnLostFocus="False" CultureName="es-ES" Mask="99:99:99" MaskType="Time"
                                    PromptCharacter="_" TargetControlID="txtHoraDesde" UserTimeFormat="TwentyFourHour">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td rowspan="2" style="width: 60px;">
                            </td>
                            <td style="width: 6%;">
                                <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro" Width="110%">Fecha Vuelo:</asp:Label>
                            </td>
                            <td rowspan="2" style="width: 80px;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtFechaVuelo" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                BackColor="#E4E2DC"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" PopupButtonID="imgbtnFechaVuelo"
                                                TargetControlID="txtFechaVuelo" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblFormaFchVuelo" runat="server" Text="(dd/mm/yyyy)" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnFechaVuelo" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                    Width="22px" Height="22px" />
                            </td>
                            <td rowspan="2" style="width: 60px;">
                            </td>
                            <td>
                                <asp:Label ID="lblNumBoarding" runat="server" CssClass="TextoFiltro" Width="100%">Nro Boarding:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumBoarding" runat="server" Width="123px" CssClass="textbox"
                                    onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)" Height="16px"
                                    MaxLength="5"></asp:TextBox>
                            </td>
                            <td style="width: 80px">
                            </td>
                            <td>
                                <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro">Estado:</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEstado" runat="server" Width="100%" CssClass="combo2">
                                </asp:DropDownList>
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
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="5">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaLecturaFin" runat="server" CssClass="TextoFiltro">Hasta:</asp:Label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                BackColor="#E4E2DC"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                PopupButtonID="imgbtnCalendar2" TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Width="50px">Hora Fin:</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                    Width="22px" Height="22px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                    onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                <br />
                                <asp:Label ID="lblHoraIniHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" AutoComplete="False"
                                    ClearMaskOnLostFocus="False" CultureName="es-ES" Mask="99:99:99" MaskType="Time"
                                    PromptCharacter="_" TargetControlID="txtHoraHasta" UserTimeFormat="TwentyFourHour">
                                </cc1:MaskedEditExtender>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro">Nro Vuelo:</asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtNumVuelo" runat="server" Width="88px" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                    onblur="gDescripcion(this)" Height="16px" MaxLength="10"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblETicket" runat="server" CssClass="TextoFiltro">e-Ticket:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtETicket" runat="server" Width="123px" CssClass="textbox" Height="16px"
                                    MaxLength="20" onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td colspan="2" align="right" rowspan="2">
                                &nbsp; <b>
                                    <asp:LinkButton ID="lblExportar" runat="server" OnClick="lblExportar_Click">[Exportar a Excel]</asp:LinkButton>
                                </b>
                                <br />
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" Style="cursor: hand;"
                                            OnClick="btnConsultar_Click" Text="Consultar" />&nbsp;&nbsp;&nbsp;
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
                        <tr>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro">Nro Asiento:</asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtNumAsiento" runat="server" Width="160px" CssClass="textbox" Height="16px"
                                    MaxLength="10" onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblNomPasajero" runat="server" CssClass="TextoFiltro">Pasajero:</asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtNomPasajero" runat="server" Width="294px" CssClass="textbox"
                                    onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)" Height="16px"
                                    MaxLength="50"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    &nbsp;
                </td>
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
                            <cc1:PagingGridView ID="grvPaginacionBoarding" runat="server" AllowPaging="True"
                                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grilla" GridLines="Vertical"
                                GroupingDepth="0" OnPageIndexChanging="grvPaginacionBoarding_PageIndexChanging"
                                OnRowCommand="grvPaginacionBoarding_RowCommand" OnRowDataBound="grvPaginacionBoarding_RowDataBound"
                                OnSorting="grvPaginacionBoarding_Sorting" VirtualItemCount="-1" Width="100%">
                                <SelectedRowStyle CssClass="grillaFila" />
                                <PagerStyle CssClass="grillaPaginacion" />
                                <HeaderStyle CssClass="grillaCabecera" />
                                <EditRowStyle BorderColor="#FF0066" />
                                <AlternatingRowStyle CssClass="grillaFila" />
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BorderStyle="solid" BorderColor="#E6E6E6" BorderWidth="1px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Nro. Correlativo" SortExpression="Num_Secuencial_Bcbp">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Num_Secuencial_Bcbp" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                CommandName="ShowBoarding" Text='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro Vuelo" SortExpression="Num_Vuelo" />
                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo" />
                                    <asp:BoundField DataField="Num_Asiento" HeaderText="Nro Asiento" SortExpression="Num_Asiento" />
                                    <asp:BoundField DataField="Dsc_Molinete" HeaderText="Molinete" SortExpression="Dsc_Molinete" />
                                    <asp:BoundField DataField="UltimaMod" HeaderText="Fecha Modificación" SortExpression="UltimaMod" />
                                    <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado" SortExpression="Dsc_Campo">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrimerUso" HeaderText="Fecha 1er Uso" SortExpression="PrimerUso" />
                                    <asp:BoundField DataField="UltimoUso" HeaderText="Fecha Ultimo Uso" SortExpression="UltimoUso" />
                                    <asp:BoundField DataField="Nro_Boarding" HeaderText="Nro Boarding Pass" SortExpression="Nro_Boarding" />
                                    <asp:BoundField DataField="Dsc_Destino" HeaderText="Destino" SortExpression="Dsc_Destino" />
                                    <asp:BoundField DataField="Nom_Pasajero" HeaderText="Pasajero" SortExpression="Nom_Pasajero" />
                                    <asp:BoundField DataField="Cod_Eticket" HeaderText="eTicket" SortExpression="Cod_Eticket" />
                                </Columns>
                            </cc1:PagingGridView>
                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                            <br />
                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                            <br />
                            <br />
                            <cc2:PagingGridView ID="grvResumen" runat="server" AutoGenerateColumns="False" BackColor="White"
                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" CssClass="grilla" GridLines="Vertical" GroupingDepth="1" VirtualItemCount="-1"
                                Width="40%" Caption="RESUMEN:" HorizontalAlign="Center" OnRowCreated="grvResumen_RowCreated"
                                ShowFooter="True">
                                <SelectedRowStyle CssClass="grillaFila" />
                                <HeaderStyle CssClass="grillaCabecera" />
                                <EditRowStyle BorderColor="#FF0066" />
                                <AlternatingRowStyle CssClass="grillaFila" />
                                <FooterStyle BackColor="#D1D1D1" Font-Bold="True" />
                                <RowStyle BorderStyle="solid" BorderColor="#e6e6e6" BorderWidth="1px" />
                                <Columns>
                                    <asp:BoundField DataField="Fch_Creacion" HeaderText="Fecha Lectura" />
                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro Vuelo" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                </Columns>
                            </cc2:PagingGridView>
                            <br />
                            <br />
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
    <uc2:CnsDetBoardingPass ID="CnsDetBoardingPass1" runat="server" />
    </form>
</body>
</html>
