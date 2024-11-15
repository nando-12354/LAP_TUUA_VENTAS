<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Seguridad_Rol_CrearRol, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Creación de Rol</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="javascript/common.js" />
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <style type="text/css">
        .CenterGrid
        {
            text-align: center;
        }
    </style>

    <script language="javascript" type="text/javascript">
        var accionSave = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = true;
                    document.body.style.cursor = 'wait';
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = true;
                    //                }
                }
            }
        }


        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = false;
                    //                }
                }
            }
        }

        function confirmacionLlamada() {
            var mConfirmacion = document.forms[0].hConfirmacion.value;

            if (confirm(mConfirmacion)) {
                accionSave = true;
                return true;
            }
            else {
                accionSave = false;
                return false;
            }
        }	
    </script>

</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnAceptar">
    <asp:HiddenField ID="hConfirmacion" runat="server" Value="Confirmar Accion" />
    <div>
        <div style="background-image: url(Imagenes/back.gif)">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <!-- HEADER -->
                    <td style="height: 11px">
                        <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <!-- WORK MENU -->
                    <td>
                        <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                            <tr>
                                <td align="right" class="style1" style="text-align: left">
                                    &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" id="img" />
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Width="96px"
                                        CssClass="Boton" OnClientClick="return confirmacionLlamada()" />&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <!-- SPACE -->
                    <td>
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>
                <tr>
                    <!-- CONTENT -->
                    <td>
                        <div class="EspacioSubTablaPrincipal">
                            &nbsp;
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid">
                                    </td>
                                    <td class="CenterGrid" align="center">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                            <Scripts>
                                                <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                            </Scripts>
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table align="center">
                                                    <tr>
                                                        <td colspan="1" style="height: 21px; text-align: left">
                                                            <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            &nbsp;
                                                            <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="177px" CssClass="textbox"
                                                                onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abc(this)"></asp:TextBox>
                                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                            <asp:Label ID="lblRolPadre" runat="server" Width="55px" CssClass="TextoEtiqueta"></asp:Label>
                                                            &nbsp;&nbsp;
                                                            <asp:DropDownList ID="ddlRolPadre" runat="server" Width="177px" OnSelectedIndexChanged="ddlRolPadre_SelectedIndexChanged"
                                                                AutoPostBack="True" CssClass="combo">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="CenterGrid">
                                                            &nbsp;<table>
                                                                <tr>
                                                                    <td style="text-align: center; width: 229px; height: 13px;">
                                                                        <asp:Label ID="lblPerfilConfiguracion" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 52px; text-align: center; height: 13px;">
                                                                    </td>
                                                                    <td style="text-align: center; height: 13px; width: 229px;">
                                                                        <asp:Label ID="lblResumenPefil" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="tvwRoles" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="False"
                                                                            ShowLines="True" SkipLinkText="" CssClass="arbol" Width="244px">
                                                                            <NodeStyle ChildNodesPadding="10px" CssClass="nodoArbol" Font-Size="X-Small" />
                                                                            <ParentNodeStyle CssClass="nodoArbol" Font-Size="X-Small" />
                                                                            <LevelStyles>
                                                                                <asp:TreeNodeStyle BackColor="AliceBlue" Font-Underline="False" />
                                                                            </LevelStyles>
                                                                        </asp:TreeView>
                                                                    </td>
                                                                    <td style="width: 52px; text-align: center">
                                                                        <asp:Button ID="btnAsignar" runat="server" Width="96px" OnClick="btnAsignar_Click"
                                                                            CssClass="Boton" CausesValidation="False" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TreeView ID="tvwRolesAsignados" runat="server" ShowExpandCollapse="False" ShowLines="True"
                                                                            SkipLinkText="" CssClass="arbol" Width="280px">
                                                                            <ParentNodeStyle CssClass="nodoArbol" />
                                                                            <LevelStyles>
                                                                                <asp:TreeNodeStyle BackColor="AliceBlue" Font-Underline="False" />
                                                                            </LevelStyles>
                                                                            <NodeStyle ChildNodesPadding="10px" CssClass="nodoArbol" />
                                                                        </asp:TreeView>
                                                                        &nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            &nbsp;<asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="502px"></asp:Label>
                                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                                            <asp:ListBox ID="lstCodProcesoAsignados" runat="server" Height="16px" Style="display: none"
                                                                Width="16px"></asp:ListBox>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnAsignar" EventName="Click" />
                                                <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <!-- FOOTER -->
                    <td class="Espacio1FilaTabla" style="height: 11px">
                        <uc2:PiePagina ID="PiePagina2" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            &nbsp;
            <asp:ObjectDataSource ID="odsListarRoles" runat="server" SelectMethod="listaDeRoles"
                TypeName="LAP.TUUA.CONTROL.BO_Seguridad"></asp:ObjectDataSource>
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
