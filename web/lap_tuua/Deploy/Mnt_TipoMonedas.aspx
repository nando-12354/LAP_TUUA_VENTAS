<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Mantenimiento_TipoMonedas, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Mantenimiento de monedas</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left" colspan="9">
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnNuevo" runat="server" CssClass="Boton" OnClick="btnNuevo_Click" />&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvMoneda" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" OnPageIndexChanging="grvMoneda_PageIndexChanging"
                                                OnSorting="grvMoneda_Sorting" CssClass="grilla" AllowPaging="True">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:HyperLinkField Text="C&#243;digo" DataNavigateUrlFields="Cod_Moneda" DataNavigateUrlFormatString="Mnt_ModificarTipoMonedas.aspx?idMoneda={0}"
                                                        DataTextField="Cod_Moneda" HeaderText="C&#243;digo" SortExpression="Cod_Moneda" />
                                                    <asp:BoundField DataField="Cod_Moneda" HeaderText="C&#243;digo" SortExpression="Cod_Moneda" />
                                                    <asp:BoundField DataField="Dsc_Moneda" HeaderText="Descripci&#243;n" SortExpression="Dsc_Moneda" />
                                                    <asp:BoundField HeaderText="Símbolo" DataField="Dsc_Simbolo" SortExpression="Dsc_Simbolo" />
                                                    <asp:BoundField DataField="Dsc_Nemonico" HeaderText="Nemonico" SortExpression="Dsc_Nemonico" />
                                                    <asp:BoundField DataField="Tip_Estado" HeaderText="Estado" SortExpression="Tip_Estado" />
                                                    <asp:TemplateField HeaderText="Fecha Modificaci&#243;n" SortExpression="Log_Fecha_Mod,Log_Hora_Mod">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITFecha" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Log_Usuario_Mod" HeaderText="Usuario Modificaci&#243;n"
                                                        SortExpression="Log_Usuario_Mod" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
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
    </div>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
