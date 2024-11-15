<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seg_VerUsuario.aspx.cs" Inherits="Modulo_Seguridad_Usuario_VerUsuario" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Seguridad - Usuarios</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnConsultar">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="left">
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click"
                    CssClass="Boton" />
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
                            <td class="CenterGrid" style="height: 115px" valign="top">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                    <tr class="formularioTitulo">
                                        <td class="style3">
                                            <asp:Label ID="lblCuenta" runat="server" CssClass="TextoEtiqueta" Width="40px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuenta" runat="server" MaxLength="30" Width="158px" CssClass="textbox"
                                                onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="193px" CssClass="textbox"
                                                onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRol" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRol" runat="server" Width="177px" CssClass="combo">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                        </td>
                                        <td class="style2">
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" Width="101px" OnClick="btnConsultar_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <hr class="EspacioLinea" color="#0099cc" />
                                <asp:ScriptManager ID="smgUsuario" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="upnUsuario" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gwvUsuario" runat="server" AutoGenerateColumns="False" CellPadding="3"
                                            Width="100%" BackColor="White" BorderStyle="None" BorderWidth="1px" CssClass="grilla"
                                            AllowPaging="True" OnPageIndexChanging="gwvUsuario_PageIndexChanging" AllowSorting="True"
                                            OnSorting="gwvUsuario_Sorting" PageSize="5" OnRowDataBound="gwvUsuario_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Código" SortExpression="Cod_Usuario">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hlCodUsuario" runat="server" NavigateUrl='<%# String.Format("Seg_ModificarUsuario.aspx?Cod_Usuario={0}&amp;Cod_Grupo={1}", Eval("Cod_Usuario"), Eval("Cod_Grupo_Padre")) %>'
                                                            Text='<%# Eval("Cod_Usuario") %>' Visible='<%# Eval("Flg_Sec").ToString().Equals("0") %>' />
                                                        <asp:Label ID="lblCodUsuario" runat="server" Text='<%# Eval("Cod_Usuario") %>' Visible='<%# Eval("Flg_Sec").ToString().Equals("1") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cod_Usuario" HeaderText="Código " SortExpression="Cod_Usuario" />
                                                <asp:BoundField DataField="Nom_Usuario" HeaderText="Nombre y Apellidos" SortExpression="Nom_Usuario" />
                                                <asp:BoundField DataField="Ape_Usuario" HeaderText="Apellidos" SortExpression="Ape_Usuario"
                                                    Visible="False" />
                                                <asp:BoundField DataField="Cta_Usuario" HeaderText="Cuenta" SortExpression="Cta_Usuario" />
                                                <asp:TemplateField HeaderText="Fecha Creación" SortExpression="FH_Creacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITFechaC" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Creacion")),Convert.ToString(Eval("Hor_Creacion"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nom_Usuario_Creacion" HeaderText="Usuario Creación" SortExpression="Nom_Usuario_Creacion" />
                                                <asp:BoundField DataField="Fch_Vigencia" HeaderText="Fecha Vigencia" SortExpression="Fch_Vigencia"
                                                    DataFormatString="{0:dd/M/yyyy}">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nom_Estado" HeaderText="Estado" SortExpression="Nom_Estado" />
                                                <asp:BoundField DataField="Nom_Grupo" HeaderText="Grupo" SortExpression="Nom_Grupo" />
                                                <asp:BoundField DataField="Nom_Rol" HeaderText="Roles Asociados" SortExpression="Nom_Rol" />
                                                <asp:TemplateField HeaderText="Fecha Modificación" SortExpression="FH_Proceso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITFecha" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Mod")),Convert.ToString(Eval("Hor_Mod"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nom_Usuario_Mod" HeaderText="Usuario Modificación" SortExpression="Nom_Usuario_Mod" />
                                            </Columns>
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" Visible="False"></asp:Label>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <asp:UpdateProgress ID="upgUsuario" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="lblProcesando" runat="server" Style="text-align: left" Text="Procesando"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                        </tr>
                    </table>
                </div>
                <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsUsuario" runat="server" SelectMethod="listarUsuario"
        TypeName="DAOService.Service"></asp:ObjectDataSource>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
