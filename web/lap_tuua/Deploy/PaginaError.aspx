<%@ page language="C#" autoeventwireup="true" inherits="PaginaError, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Pagina Error</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    &nbsp;<asp:Image ID="Image1" runat="server" CssClass="tamanocabecera" ImageUrl="~/Imagenes/head.gif" /></td>
            </tr>
            <tr>
                <td class="menuDatos" style="height: 11px; text-align: right">
                    <asp:Label ID="lblFecha" runat="server" Text="Label" Width="410px"></asp:Label></td>
            </tr>
            <tr class="header">
                <td class="header">
                    <asp:Label ID="lblTituloMensaje" runat="server" CssClass="titulosecundario">Mensaje de Error</asp:Label>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                    <td align="right" class="style1" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    </tr>
            <tr>
                <td style="text-align: center">
                    <div class="EspacioSubTablaPrincipal">
                        &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>&nbsp;</td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="lblEmpresaTuua" runat="server" CssClass="pieLogin"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label
                        ID="lblDerechoTuua" runat="server" CssClass="pieLogin"></asp:Label></td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
