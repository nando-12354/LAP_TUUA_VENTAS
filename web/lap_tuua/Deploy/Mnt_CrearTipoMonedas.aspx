<%@ page language="C#" autoeventwireup="true" inherits="Mnt_CrearTipoMonedas, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Creación de Moneda</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/BloquearBackspace.js" -->

    <script type="text/javascript">
        function numero() {
            if ((event.keyCode < 48) || (event.keyCode > 57)) event.keyCode = 0;
        }
        function val_int(o) {
            o.value = o.value.toString().replace(/([^0-9])/g, "");
        }

        var accionSave = false;

        function validarAccion() {
            if (confirm('Está seguro que desea crear el Tipo de Moneda ?')) {
                if (document.forms[0].ddlCodMoneda.selectedIndex == 0) {
                    document.getElementById('lblMensaje').innerHTML = "Error, seleccione un tipo de moneda válido";
                    accionSave = false;
                    return false;
                }
            } else {
                accionSave = false;
                return false;
            }
            accionSave = true;
            return true;
        }

        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = true;
                    document.body.style.cursor = 'wait';
                }
            }
        }

        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = false;
                    document.body.style.cursor = 'default';
                    accionSave = false;
                }
            }
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
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
                <!-- WORK MENU -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td align="right" style="text-align: left">
                                &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                            </td>
                            <td align="right" style="text-align: right">
                                <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" OnClick="btnAceptar_Click"
                                    OnClientClick="return validarAccion()" />&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="EspacioSubTablaPrincipal">
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td style="height: 115px;">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        <Scripts>
                                            <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                        </Scripts>
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="left" width="30%">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCodMoneda" runat="server" Width="200px" CssClass="combo2"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCodMoneda_SelectedIndexChanged" />
                                                    </td>
                                                    <td style="width: 30%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblDescripcion" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" Width="200px"
                                                            MaxLength="50" ReadOnly="true" BackColor="LightYellow"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblSimbolo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSimbolo" runat="server" CssClass="textbox" Width="200px" ReadOnly="true"
                                                            BackColor="LightYellow"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        &nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblNemonico" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtNemonico" runat="server" CssClass="textbox" MaxLength="3" Width="200px"
                                                            ReadOnly="true" BackColor="LightYellow"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
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
    </div>
    </form>
</body>
</html>
