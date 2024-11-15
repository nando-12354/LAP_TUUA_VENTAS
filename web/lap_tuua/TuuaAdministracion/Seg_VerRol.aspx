<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seg_VerRol.aspx.cs" Inherits="Modulo_Seguridad_Rol_VerRol" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Seguridad - Roles</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <!-- HEADER -->
            <td class="Espacio1FilaTabla" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <!-- WORK MENU -->
            <td align="left">
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" CssClass="Boton" />&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <!-- SPACE -->
                <hr class="EspacioLinea" color="#0099cc" />
            </td>
        </tr>
        <tr>
            <td>
                <!-- INICIO GRILLA -->
                <div>
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gwvRol" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                            CellPadding="3" Width="100%" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                            CssClass="grilla" OnPageIndexChanging="gwvRol_PageIndexChanging" AllowSorting="True"
                                            OnSorting="gwvRol_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Código" SortExpression="Cod_Rol">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlCodRol" runat="server"
                                                        NavigateUrl='<%# String.Format("Seg_ModificarRol.aspx?Cod_Rol={0}", Eval("Cod_Rol")) %>'
                                                        Text='<%# Eval("Cod_Rol") %>'
                                                        Visible='<%# Eval("Flg_Sec").ToString().Equals("0") %>'/>
                                                    <asp:Label ID="lblCodRol" runat="server" Text='<%# Eval("Cod_Rol") %>' Visible='<%# Eval("Flg_Sec").ToString().Equals("1") %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Cod_Rol" HeaderText="Código " SortExpression="Cod_Rol" />
                                                <asp:BoundField DataField="Nom_Rol" HeaderText="Nombre de Rol" SortExpression="Nom_Rol" />
                                                <asp:BoundField DataField="Nom_Padre_Rol" HeaderText="Rol Padre" SortExpression="Nom_Padre_Rol" />
                                                <asp:TemplateField HeaderText="Fecha Creación" SortExpression="Fch_Creacion">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITFecha" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Creacion")),Convert.ToString(Eval("Hor_Creacion"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nom_Usuario_Creacion" HeaderText="Usuario Creación" SortExpression="Nom_Usuario_Creacion" />
                                            </Columns>
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <asp:UpdateProgress ID="upgUsuario" runat="server">
                                            <ProgressTemplate>
                                                <asp:Label ID="lblProcesando" runat="server" Style="text-align: left" Text="Procesando"></asp:Label>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- FIN GRILLA -->
            </td>
        </tr>
        <tr>
            <!-- FOOTER -->
            <td class="Espacio1FilaTabla" style="height: 11px">
                <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsListarRol" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="listaDeRoles" TypeName="DAOService.Service"></asp:ObjectDataSource>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
