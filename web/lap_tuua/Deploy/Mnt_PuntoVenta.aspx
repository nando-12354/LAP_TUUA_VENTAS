<%@ page language="C#" autoeventwireup="true" inherits="Mnt_PuntoVenta, App_Web_jlql8yfo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP-Mantenimiento de Punto de Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnNuevo" runat="server" CssClass="Boton" OnClick="btnNuevo_Click" />&nbsp;
                </td>
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
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvPuntoVenta" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grilla" GridLines="Vertical" OnPageIndexChanging="grvPuntoVenta_PageIndexChanging"
                                                OnSorting="grvPuntoVenta_Sorting" Width="100%">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="Cod_Equipo" DataNavigateUrlFormatString="Mnt_ModificarPuntoVenta.aspx?idEquipo={0}"
                                                        DataTextField="Cod_Equipo" HeaderText="C&#243;digo" Text="C&#243;digo" SortExpression="Cod_Equipo" />
                                                    <asp:BoundField DataField="Cod_Equipo" HeaderText="C&#243;digo" SortExpression="Cod_Equipo" />
                                                    <asp:BoundField DataField="Num_Ip_Equipo" HeaderText="N&#250;mero IP" SortExpression="Num_Ip_Equipo" />
                                                    <asp:BoundField DataField="Dsc_Estacion" HeaderText="Descripción" SortExpression="Dsc_Estacion" />
                                                    <asp:BoundField DataField="Usuario_Logeado" HeaderText="Usuario Logueado" SortExpression="Usuario_Logeado" />
                                                    <asp:BoundField DataField="Flg_Estado" HeaderText="Estado" SortExpression="Flg_Estado" />
                                                    <asp:TemplateField HeaderText="Fecha Modificaci&#243;n" SortExpression="Log_Fecha_Mod">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITFecha" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Nom_Usuario" HeaderText="Usuario Modificaci&#243;n" SortExpression="Log_Usuario_Mod" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <asp:UpdateProgress ID="upgPuntoVenta" runat="server">
                                                <ProgressTemplate>
                                                    <asp:Label ID="lblProcesando" runat="server" Style="text-align: left" Text="Procesando"></asp:Label>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
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
