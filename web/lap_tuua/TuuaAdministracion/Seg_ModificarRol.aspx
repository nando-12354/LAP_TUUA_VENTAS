<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Seg_ModificarRol.aspx.cs"
    Inherits="Modulo_Seguridad_Rol_ModificarRol" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Modificar Rol</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="javascript" type="text/javascript">
        var accionSave = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnActualizar').disabled = true;
                    document.getElementById('btnEliminar').disabled = true;
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
                    document.getElementById('btnActualizar').disabled = false;
                    document.getElementById('btnEliminar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = false;
                    //                }
                }
            }
        }

        function confirmacionLlamada(valor) {
            var mConfirmacion0 = document.forms[0].hConfirmacion0.value;
            var mConfirmacion1 = document.forms[0].hConfirmacion1.value;
            var mConfirmacion = "";
            if (valor == 0) {
                mConfirmacion = mConfirmacion0;
            } else if (valor == 1) {
                mConfirmacion = mConfirmacion1;
            }

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
    <form id="form1" runat="server" defaultbutton="btnActualizar" defaultfocus="txtNombre">
    <asp:HiddenField ID="hConfirmacion0" runat="server" Value="Confirmar Accion" />
    <asp:HiddenField ID="hConfirmacion1" runat="server" Value="Confirmar Accion" />
    <div>
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
                                        &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();"
                                            id="img" />
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnActualizar" runat="server" OnClick="btnAceptar_Click" Width="96px"
                                            CssClass="Boton" OnClientClick="return confirmacionLlamada(0)" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnEliminar" runat="server" Width="96px" OnClick="btnEliminar_Click"
                                            CssClass="Boton" CausesValidation="False" OnClientClick="return confirmacionLlamada(1)" />
                                        &nbsp;&nbsp;&nbsp;
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
                                &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                    <tr>
                                        <td class="SpacingGrid" style="height: 115px">
                                        </td>
                                        <td class="CenterGrid" style="height: 115px">
                                            &nbsp;<table style="width: 100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                            <tr>
                                                                <td style="width: 25%"></td>
                                                                <td><asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                                                                <td><asp:Label ID="txtNombre" runat="server" Width="149px" CssClass="TextoCampo"></asp:Label></td>
                                                                <td><asp:Label ID="lblRolPadre" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                                                                <td><asp:Label ID="txtRolPadre" runat="server" Width="204px" CssClass="TextoCampo"></asp:Label></td>                                                                
                                                                <td style="width: 25%"></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="1" style="height: 21px; text-align: left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="CenterGrid">
                                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                            <Scripts>
                                                                <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                                            </Scripts>
                                                        </asp:ScriptManager>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <table style="width: 574px" align="center">
                                                                    <tr>
                                                                        <td style="text-align: center; width: 292px;">
                                                                            <asp:Label ID="lblPerfilConfiguracion" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                        <td style="text-align: center; width: 292px;">
                                                                            &nbsp;
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            <asp:Label ID="lblResumenPefil" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 275px;">
                                                                            <asp:TreeView ID="tvwRoles" runat="server" ShowCheckBoxes="All" ShowExpandCollapse="False"
                                                                                ShowLines="True" SkipLinkText="" CssClass="arbol" Width="244px">
                                                                                <ParentNodeStyle CssClass="nodoArbol" />
                                                                                <NodeStyle ChildNodesPadding="10px" CssClass="nodoArbol" />
                                                                            </asp:TreeView>
                                                                        </td>
                                                                        <td style="width: 52px; text-align: center">
                                                                            <asp:Button ID="btnAsignar" runat="server" OnClick="btnAsignar_Click" Width="96px"
                                                                                CssClass="Boton" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TreeView ID="tvwRolesAsignados" runat="server" CssClass="arbol" ShowExpandCollapse="False"
                                                                                ShowLines="True" SkipLinkText="" Width="280px">
                                                                                <ParentNodeStyle CssClass="nodoArbol" />
                                                                                <LevelStyles>
                                                                                    <asp:TreeNodeStyle BackColor="AliceBlue" Font-Underline="False" />
                                                                                </LevelStyles>
                                                                                <NodeStyle ChildNodesPadding="10px" CssClass="nodoArbol" />
                                                                            </asp:TreeView>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" style="text-align: center">
                                                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="599px"></asp:Label>
                                                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                                                            <asp:ListBox ID="lstCodProcesoAsignados" runat="server" Height="16px" Style="display: none"
                                                                                Width="16px"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnActualizar" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                        &nbsp;<br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
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
                &nbsp;<br />
                &nbsp;
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
