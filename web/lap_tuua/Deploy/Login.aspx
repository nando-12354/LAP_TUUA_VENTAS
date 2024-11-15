<%@ page language="C#" autoeventwireup="true" inherits="Logeo, App_Web_7ctknflu" %>


<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Sistema TUUA </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
     <!-- #INCLUDE file="javascript/KeyPress.js" -->
     <!-- #INCLUDE file="javascript/Functions.js" -->
     <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <style type="text/css">
		html,body{
			height:100%;	
			overflow: hidden;
			margin-left: 0px;
	        margin-right: 0px;		
		}
    </style>
    
    <script language="javascript" type="text/javascript">
        function setBrowserData() {
            document.getElementById("hdSizeH").value = document.body.clientHeight;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnIngresar" defaultfocus="btnIngresar">
    <div style="height: 100%">
        <table class="TamanoTabla" align="center" style="height: 100%;">
            <tr>
                <td style="min-height: 100%;" valign="top">
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
                                            <asp:Label ID="lblFecha" runat="server" Text="Label" Width="358px"></asp:Label>&nbsp;&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tamanocabecera" style="height: 28px">
                                &nbsp;&nbsp;&nbsp;<asp:Label ID="lblBienvenida" runat="server" Text="Label" Width="360px" CssClass="TituloBienvenida"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td style="width: 236px; height: 21px">
                            </td>
                            <td style="width: 343px; height: 21px">
                            </td>
                            <td style="height: 21px">
                                <table style="width: 269px">
                                    <tr>
                                        <td style="width: 203px; height: 27px; text-align: right">
                                            <asp:Label ID="lblUsuario" runat="server" CssClass="Login"></asp:Label></td>
                                        <td style="width: 34px; height: 27px; text-align: left">
                                            <asp:TextBox ID="txtUsuario" runat="server"  Width="120px" 
                                                onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abc(this)" 
                                                MaxLength="30" Height="16px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUser" runat="server" ControlToValidate="txtUsuario"
                                                Display="None" ErrorMessage="Se requiere Usuario">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 203px; height: 40px; text-align: right">
                                            <asp:Label ID="lblPassword" runat="server" CssClass="Login"  onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="abcSinEspacio(this)"></asp:Label></td>
                                        <td style="width: 34px; height: 40px; text-align: left">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                                Width="120px" onkeypress="JavaScript: Tecla('Alphanumeric');" 
                                                MaxLength="8" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtPassword"
                                                Display="None" ErrorMessage="Se requiere Password">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 203px; height: 33px">
                                            </td>
                                        <td style="width: 34px; height: 33px; text-align: left;">
                                            <asp:Button ID="btnIngresar" runat="server" CssClass="Boton" Height="26px" OnClientClick="setBrowserData()" OnClick="btnIngresar_Click"
                                                Text="Ingresar" Width="88px" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 203px; height: 33px">
                                        </td>
                                        <td style="width: 34px; height: 33px; text-align: left">
                                            <asp:LinkButton ID="lkbCambiarContraseña" runat="server" Height="7px" Width="145px" OnClick="lkbCambiarContraseña_Click" BorderStyle="None"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 203px; height: 33px">
                                        </td>
                                        <td style="width: 34px; height: 33px; text-align: left;">
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="261px"></asp:Label></td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
                    </table>
                    <cc1:validatorcalloutextender id="vextUser" runat="server" targetcontrolid="rfvUser"  Width="200px ">
                     </cc1:validatorcalloutextender>
                    <cc1:validatorcalloutextender id="vextClave" runat="server" targetcontrolid="rfvClave"  Width="200px">
                    </cc1:validatorcalloutextender>
                    <asp:Label ID="lblNumIntentos" runat="server" Height="1px" Visible="False" Width="3px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height : 15%; text-align: center;" valign="bottom">
                    <asp:Label ID="lblEmpresaTuua" runat="server" CssClass="pieLogin"></asp:Label><br />
                    <asp:Label ID="lblDerechoTuua" runat="server" CssClass="pieLogin"></asp:Label>
                </td>
            </tr> 
            <tr><td style="height :5%;">&nbsp;</td>
            </tr>           
        </table>
    
    </div>
        <asp:HiddenField ID="hdSizeH" runat="server" />
    </form>    
</body>
</html>
