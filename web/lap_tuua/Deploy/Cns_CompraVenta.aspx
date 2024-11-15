<%@ page language="C#" autoeventwireup="true" inherits="Cns_CompraVenta, App_Web_ehzg6gwo" %>

<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Compra y Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    
    <script language="javascript" type="text/javascript">
        function imgPrint_onclick() {
            var idFecha = document.getElementById("txtFecha").value;
            var idUsuario = document.getElementById("ddlUsuario").value;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptOperacionCV.aspx" + "?idFecha=" + idFecha + "&idUsuario=" + idUsuario, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.focus();
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600"/>
        <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <!-- TITLE -->
                                                <td style="width: 20px;" rowspan="5">
                                                </td>
                                                <td colspan="4" style="height: 20px;">
                                                </td>
                                                <td style="width: 100px;" colspan="3">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFecha" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td rowspan="2" style="width: 75px;">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="textbox" MaxLength="10" ReadOnly="false"
                                                                    Width="72px" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                                    BackColor="#E4E2DC" AutoPostBack="True" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha"
                                                                    PopupButtonID="imgbtnCalendar1">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center">
                                                                <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    &nbsp;<asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                        Width="22px" Height="22px" />
                                                    &nbsp;
                                                </td>
                                                <td style="width: 40px;">
                                                </td>
                                                <td>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td style="width: 300px;">
                                                    <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="combo2" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr style="vertical-align: top;">
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td style="width: 100px;" colspan="3">
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtFecha" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                            Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr>
                <td>
                    <!-- DATA RESULT ZONE -->
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda1" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion11" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda11" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda11_RowCreated" OnRowDataBound="grvMoneda11_RowDataBound"
                                                                        AllowSorting="True" OnSorting="grvMoneda11_Sorting">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion12" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda12" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda12_RowCreated" OnRowDataBound="grvMoneda12_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:BoundField>
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                                <FooterStyle BackColor="#D9D9D9" HorizontalAlign="Right" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                            </asp:BoundField>
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda2" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion21" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda21" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda21_RowCreated" OnRowDataBound="grvMoneda21_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion22" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda22" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda22_RowCreated" OnRowDataBound="grvMoneda22_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda3" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion31" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda31" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda31_RowCreated" OnRowDataBound="grvMoneda31_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion32" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda32" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda32_RowCreated" OnRowDataBound="grvMoneda32_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda4" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion41" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda41" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda41_RowCreated" OnRowDataBound="grvMoneda41_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion42" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda42" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda42_RowCreated" OnRowDataBound="grvMoneda42_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda5" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion51" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda51" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda51_RowCreated" OnRowDataBound="grvMoneda51_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion52" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda52" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda52_RowCreated" OnRowDataBound="grvMoneda52_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda6" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion61" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda61" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda61_RowCreated" OnRowDataBound="grvMoneda61_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion62" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda62" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda62_RowCreated" OnRowDataBound="grvMoneda62_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda7" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion71" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda71" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda71_RowCreated" OnRowDataBound="grvMoneda71_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}") %>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="95%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td bgcolor="#d9d9d9" style="text-align: center">
                                                                    <asp:Label ID="lblTipoOperacion72" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:GridView ID="grvMoneda72" runat="server" AutoGenerateColumns="False" Width="100%"
                                                                        ShowFooter="True" OnRowCreated="grvMoneda72_RowCreated" OnRowDataBound="grvMoneda72_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Turno" FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Cod_Turno", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTotal" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Hora" DataField="Hora" FooterStyle-BackColor="#d9d9d9" />
                                                                            <asp:BoundField HeaderText="Tasa cambio" DataField="Imp_Tasa_Cambio" FooterStyle-BackColor="#d9d9d9">
                                                                                <FooterStyle BackColor="#D9D9D9" />
                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                            </asp:BoundField>
                                                                            <asp:TemplateField HeaderText="Monto Moneda" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_Cambiado", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_ACambiarLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Nuevos Soles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                                                                FooterStyle-BackColor="#d9d9d9">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Imp_ACambiar", "{0}")%>
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="Imp_CambiadoLabel" runat="server" />
                                                                                </FooterTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Usuario" DataField="Usuario" FooterStyle-BackColor="#d9d9d9" />
                                                                        </Columns>
                                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>                                                
                                            </table>
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
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>    
</body>
</html>
