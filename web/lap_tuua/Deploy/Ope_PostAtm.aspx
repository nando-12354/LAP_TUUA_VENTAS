<%@ page language="C#" autoeventwireup="true" inherits="Ope_PostAtm, App_Web_7ctknflu" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Generacion Archivo Ventas</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
        <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

</head>
<body>
    <form id="form1" runat="server" >
        <div style="background-image: url(Imagenes/back.gif)">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                                                
                        <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <td align="right" class="style1" style="text-align: left">
                     </td>
                    <td align="right">
                                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="EspacioSubTablaPrincipal">
                            &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    <table style="width: 100%; left: 0px; position: relative; top: 0px;" 
                                            class="alineaderecha">
                                    
                                        <tr>
                                            <td class="style3">
                                                &nbsp;</td>
                                            <td style="text-align: left;" align="right" valign="bottom">
                                                &nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Label ID="lblMensaje" runat="server" CssClass="msgMensaje"></asp:Label>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:ImageButton ID="ibtnFile" runat="server" ImageUrl="~/Imagenes/file.gif" 
                                                    onclick="ibtnFile_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5" align="left">
                                                <asp:HyperLink ID="hlPostImpresion" runat="server">HyperLink</asp:HyperLink>
                                                </td>
                                            <td style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                        </tr>
                                        
                                    </table>
                                    &nbsp;</td>
                                    <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina2" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <br />
            <br />
        </div>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>
