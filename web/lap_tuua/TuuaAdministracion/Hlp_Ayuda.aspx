<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hlp_Ayuda.aspx.cs" Inherits="Hlp_Ayuda" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Ayuda</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="hlp_ayuda" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body
        {
            height: 100%;
            overflow: hidden;
        }
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
            <tr>
                <!-- HEADER -->
                <td>
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr>
                <!-- CONTENT -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:Label ID="lblErrorMsg" runat="server" CssClass="mensaje"></asp:Label>
                                <div id="divData" runat="server" style="overflow: auto; width: 100%; border-left: 0px gray solid;
                                    border-bottom: 0px gray solid; padding: 0px; margin: 0px; z-index: 10000000;
                                    float: none;">
                                    <iframe id="IframeReporte" src="AyudaHLP/LAP-TUUA-4621-MUS-GE00-Manual Usuario ADMINISTRACION.pdf" width="100%" height="100%"
                                        align="center" frameborder="0" name="I1" style="z-index: 50000000;"></iframe>
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!-- FOOTER -->
                <td>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script src="javascript/resolucion.js" type="text/javascript"></script>
</body>
</html>
