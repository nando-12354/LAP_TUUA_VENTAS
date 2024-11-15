<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alr_ModificarAlarma.aspx.cs"
    Inherits="Alr_ModificarAlarma" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="okmessagebox" TagPrefix="uc3" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Alarmas - Configuración de Alarma</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/datagrid.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="javascript" type="text/javascript">

        function OnCheckEmailID(email) {
            //var email=document.getElementById(srcID);      
            var filter = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
            if (!filter.test(email.value) && !email.value == "") {
                document.getElementById('lblMensajeErrorEmail').innerHTML = "Error de Formato de Email";
                email.value = "";
                email.focus();
                return false;

            }
            else
                document.getElementById('lblMensajeErrorEmail').innerHTML = "";
            return true;

        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background-image: url(Imagenes/back.gif)">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                        <uc1:cabeceramenu id="CabeceraMenu1" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <td align="right" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" TabIndex="9" OnClick="btnAceptar_Click" />
                        <cc2:confirmbuttonextender id="cbeActualizar" runat="server" confirmtext="" enabled="True"
                            targetcontrolid="btnAceptar">
                        </cc2:confirmbuttonextender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnEliminar" runat="server" Width="96px" CssClass="Boton" CausesValidation="False"
                            OnClick="btnEliminar_Click" />
                        <cc2:confirmbuttonextender id="cbeEliminar" runat="server" targetcontrolid="btnEliminar">
                                        </cc2:confirmbuttonextender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="EspacioSubTablaPrincipal">
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px; width: 2%;">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <table style="width: 95%">
                                                                <tr>
                                                                    <td style="width: 7%; height: 25px;">
                                                                        <asp:Label ID="lblModulo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:Label ID="txtModulo" runat="server" CssClass="TextoCampo" Height="16px" Width="50%"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                    
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;">
                                                                        <asp:Label ID="lblFinMensaje" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox ID="txtFinMensaje" runat="server" CssClass="textbox" MaxLength="50"
                                                                            onkeypress="JavaScript: Tecla('Alphanumeric');" TabIndex="1" Width="50%"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height:25px;">
                                                                        <asp:Label ID="lblAsunto" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:TextBox ID="txtAsunto" runat="server" CssClass="textbox" 
                                                                            MaxLength="50" Width="50%"
                                                                            onkeypress="JavaScript: Tecla('Alphanumeric');" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 25px;">
                                                                        <asp:Label ID="lblTipoAlarma" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td colspan="4">
                                                                        <asp:Label ID="txtTipoAlarma" runat="server" CssClass="TextoCampo" Width="100%"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="5">
                                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="471px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 95%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblConfiguracionCorreo" runat="server" CssClass="Titulo"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblEnviarCorreo" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                <asp:ImageButton ID="btnConfigEmail" runat="server" CausesValidation="False" ImageUrl="~/Imagenes/Add.bmp"
                                                                    TabIndex="8" OnClick="btnConfigEmail_Click" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlPanelDestinatarios" runat="server">
                                                                    <div>
                                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:GridView ID="gvwDestinatarios" runat="server" CssClass="datagrid" AutoGenerateColumns="False"
                                                                                    Width="60%" OnRowCommand="gvwDestinatarios_RowCommand">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Usuario">
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="combo" TabIndex="3"
                                                                                                    Width="207px">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Correo Electrónico">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtEmail" runat="server" Height="17px" Width="241px" onblur="OnCheckEmailID(this)" MaxLength="50"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="imbEliminar" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                    CommandName="Eliminar" Width="20" Height="18" runat="server" ImageUrl="~/Imagenes/Delete.bmp" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                                <asp:Label ID="lblMensajeErrorEmail" runat="server" CssClass="mensaje" Width="471px"></asp:Label>
                                                                            </ContentTemplate>
                                                                            <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                                                                <asp:AsyncPostBackTrigger ControlID="btnConfigEmail" EventName="Click" />
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:piepagina id="PiePagina2" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc3:okmessagebox id="omb" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnEliminar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
