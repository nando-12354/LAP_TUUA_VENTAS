<%@ page language="C#" autoeventwireup="true" inherits="CambiarContrasena, App_Web_tx1el90t" %>


<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Cambiar Contraseña</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
     <link href="css/Style.css" rel="stylesheet" type="text/css" />
        <!-- #INCLUDE file="javascript/KeyPress.js" -->
         <!-- #INCLUDE file="javascript/Functions.js" -->
         <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
         <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <table align="center" class="TamanoTabla">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="tamanocabecera">
                            <tr>
                                <td class="tamanocabecera">
                                    <asp:Image ID="Image1" runat="server" CssClass="tamanocabecera" ImageUrl="~/Imagenes/head.gif" /></td>
                            </tr>
                            <tr>
                                <td class="tamanocabecera">
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanocabecera" style="height: 28px">
                                    <table class="menuDatos">
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="width: 30px; text-align: left">
                                                <asp:Label ID="Label2" runat="server" Width="360px"></asp:Label></td>
                                            <td>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblFecha" runat="server" Text="Label" Width="358px"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tamanocabecera" style="height: 28px">
                                    <asp:Label ID="lblCambiarContraseña" runat="server" CssClass="TituloBienvenida" Text="Label"
                                        Width="360px"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:ScriptManager id="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 236px; height: 21px">
                                            <uc1:OKMessageBox ID="omb" runat="server" />
                                        </td>
                                        <td style="width: 343px; height: 21px">
                                        </td>
                                        <td style="height: 21px">
                                            <table style="width: 1px">
                                                <tr>
                                                    <td style="width: 117px; height: 27px; text-align: right" colspan="2">
                                                        <asp:Label ID="lblUsuario" runat="server" CssClass="Login"></asp:Label>
                                                    </td>
                                                    <td style="width: 38px; height: 27px; text-align: left" colspan="2">
                                                        <asp:Label ID="txtUsuario" runat="server" 
                                                    onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abc(this)" 
                                                    CssClass="TextoCampo" ></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 117px; height: 40px; text-align: right" colspan="2">
                                                        <asp:Label ID="lblPassword" runat="server" CssClass="Login"></asp:Label>
                                                    </td>
                                                    <td style="width: 38px; height: 40px; text-align: left" colspan="2">
                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" MaxLength="8"
                                                    onkeypress="JavaScript: Tecla('Alphanumeric');" 
                                                    onblur="abcSinEspacio(this)" Width="76px" TextMode="Password" 
                                                            ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtPassword"
                                                    Display="None" ErrorMessage="Se requiere Password">*</asp:RequiredFieldValidator>
                                                        <cc1:validatorcalloutextender id="vextClave" runat="server" 
                                                            targetcontrolid="rfvClave">
                                                        </cc1:validatorcalloutextender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 117px; height: 40px; text-align: right" colspan="2">
                                                        <asp:Label ID="lblConfPassword" runat="server" CssClass="Login" ></asp:Label>
                                                    </td>
                                                    <td style="width: 38px; height: 40px; text-align: left" colspan="2">
                                                        <asp:TextBox ID="txtConfPassword" runat="server" CssClass="textbox" MaxLength="8"
                                                    onkeypress="JavaScript: Tecla('Alphanumeric');" 
                                                    onblur="abcSinEspacio(this)" TextMode="Password" Width="77px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator
                                                        ID="rfvConfirmaClave" runat="server" 
                                                    ControlToValidate="txtConfPassword" Display="None"
                                                        SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                        <cc1:ValidatorCalloutExtender id="vceConfirmarClaveR" runat="server" 
                                                            TargetControlID="rfvConfirmaClave">
                                                        </cc1:ValidatorCalloutExtender>
                                                        <asp:CompareValidator ID="cvdConfirmaClave"
                                                            runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfPassword"
                                                            Display="None">*</asp:CompareValidator>
                                                        <cc1:ValidatorCalloutExtender id="vceConfirmaClaveC"
                            runat="server" TargetControlID="cvdConfirmaClave">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 33px; text-align: right;">
                                                        <asp:Button ID="btnActualizar" runat="server" 
                                                    CssClass="Boton" Height="26px" OnClick="btnIngresar_Click" Width="103px"/>
                                                        <cc1:ConfirmButtonExtender ID="cbeAceptar" runat="server" ConfirmText="" 
                                                            Enabled="True" TargetControlID="btnActualizar">
                                                        </cc1:ConfirmButtonExtender>
                                                    </td>
                                                    <td colspan="2" style="height: 33px; text-align: right;">
                                                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                                                    <td style="height: 33px; text-align: right;">
                                                        <asp:Button ID="btnCancelar" runat="server" CausesValidation="False" 
                                                            CssClass="Boton" Height="26px" OnClick="btnCancelar_Click" Width="103px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 33px; text-align: left">
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="229px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                
                <tr>
                    <td style="text-align: center">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblEmpresaTuua" runat="server" CssClass="pieLogin"></asp:Label><br />
                        <asp:Label ID="lblDerechoTuua" runat="server" CssClass="pieLogin"></asp:Label></td>
                </tr>
            </table>
        </div>
        <br />
    
    </div>
    </form>
</body>
</html>
