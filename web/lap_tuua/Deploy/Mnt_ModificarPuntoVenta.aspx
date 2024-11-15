<%@ page language="C#" autoeventwireup="true" inherits="Mnt_EditarPuntoVenta, App_Web_nfcc8hqf" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Modificar Punto de Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            width: 264px;
        }
        .style4
        {
            width: 79px;
        }
        .style5
        {
            width: 375px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <script language="javascript" type="text/javascript">
        function validateIp(idForm) {

            //Creamos un objeto 
            object = document.getElementById(idForm);
            valueForm = object.value;

            // Patron para la ip
            var patronIp = new RegExp("^([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3}).([0-9]{1,3})$");
            //window.alert(valueForm.search(patronIp));
            // Si la ip consta de 4 pares de números de máximo 3 dígitos
            if (valueForm.search(patronIp) == 0) {
                // Validamos si los números no son superiores al valor 255
                valores = valueForm.split(".");
                if (valores[0] <= 255 && valores[1] <= 255 && valores[2] <= 255 && valores[3] <= 255) {
                    //Ip correcta
                    object.style.color = "#000";
                    return;
                }
            }
            //Ip incorrecta
            object.style.color = "#f00";
        }
    </script>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server"></uc1:CabeceraMenu>
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
                                <asp:Button ID="btnActualizar" runat="server" CssClass="Boton" OnClick="btnActualizar_Click" />&nbsp;&nbsp;&nbsp;
                                <cc1:ConfirmButtonExtender ID="cbeActualizar" runat="server" ConfirmText="" Enabled="True"
                                    TargetControlID="btnActualizar">
                                </cc1:ConfirmButtonExtender>
                            </td>
                        </tr>
                    </table>
                </td>
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
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px; text-align: center;">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td align="left" class="style3">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left" class="style4">
                                                            <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:Label ID="txtCodigo" runat="server" CssClass="TextoCampo" Width="134px" Font-Names="Verdana"
                                                                ForeColor="#008FD5"></asp:Label>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="style3">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left" class="style4">
                                                            <asp:Label ID="lblDescripcion" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" MaxLength="50"
                                                                onkeypress="soloDescripcion()" onblur="gDescripcion(this)" Width="197px"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="style3">
                                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                                        </td>
                                                        <td align="left" class="style4">
                                                            <asp:Label ID="lblDireccionIP" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:TextBox ID="txtIPBloque1" runat="server" CssClass="textbox" MaxLength="3" Width="40px"
                                                                onkeypress="JavaScript: Tecla('DireccionIP');"></asp:TextBox>
                                                            .
                                                            <asp:TextBox ID="txtIPBloque2" runat="server" CssClass="textbox" MaxLength="3" Width="40px"
                                                                onkeypress="JavaScript: Tecla('DireccionIP');"></asp:TextBox>
                                                            .
                                                            <asp:TextBox ID="txtIPBloque3" runat="server" CssClass="textbox" MaxLength="3" Width="40px"
                                                                onkeypress="JavaScript: Tecla('DireccionIP');"></asp:TextBox>
                                                            .
                                                            <asp:TextBox ID="txtIPBloque4" runat="server" CssClass="textbox" MaxLength="3" Width="40px"
                                                                onkeypress="JavaScript: Tecla('DireccionIP');"></asp:TextBox>
                                                            &nbsp;<br />
                                                            &nbsp;<asp:RequiredFieldValidator ID="rfvDireccionIP1" runat="server" ErrorMessage="Ingrese dirección IP"
                                                                ControlToValidate="txtIPBloque1" Display="None"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="rfvDireccionIP1">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="rfvDireccionIP1">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <asp:RequiredFieldValidator ID="rfvDireccionIP2" runat="server" ErrorMessage="Ingrese dirección IP"
                                                                ControlToValidate="txtIPBloque2" Display="None"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="rfvDireccionIP2">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="rfvDireccionIP2">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <asp:RequiredFieldValidator ID="rfvDireccionIP3" runat="server" ErrorMessage="Ingrese dirección IP"
                                                                ControlToValidate="txtIPBloque3" Display="None"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="rfvDireccionIP3">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="rfvDireccionIP3">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <asp:RequiredFieldValidator ID="rfvDireccionIP4" runat="server" ErrorMessage="Ingrese dirección IP"
                                                                ControlToValidate="txtIPBloque4" Display="None"></asp:RequiredFieldValidator>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvDireccionIP4">
                                                            </cc1:ValidatorCalloutExtender>
                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="rfvDireccionIP4">
                                                            </cc1:ValidatorCalloutExtender>
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="style3">
                                                            &nbsp;
                                                        </td>
                                                        <td align="left" class="style4">
                                                            <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td align="left" class="style5">
                                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo" Width="169px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 100px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje"></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfvDescripcionIp" runat="server" ControlToValidate="txtDescripcion"
                                                    Display="None" ErrorMessage="Ingrese la descripción"></asp:RequiredFieldValidator>
                                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="rfvDescripcionIp">
                                                </cc1:ValidatorCalloutExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnActualizar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina2" runat="server"></uc2:PiePagina>
                    </td>
                </tr>
        </table>
    </div>
    </form>
</body>
</html>
