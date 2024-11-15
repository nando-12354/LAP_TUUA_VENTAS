<%@ page language="C#" autoeventwireup="true" inherits="Rpt_LiquidacionVenta, App_Web_7ctknflu" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Liquidacion de Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_liquidventa" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @import url(css/calendar-system.css);
    </style>
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

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td class="Espacio1FilaTabla" colspan="7">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Venta:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaInicial" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaInicio" runat="server" Width="88px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:ImageButton ID="imgbtnCalendarInicio" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaFinal" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaFin" runat="server" Width="88px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaHasta0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarFin" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" Style="cursor: hand;"
                                            OnClick="btnConsultar_Click" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                    DisplayAfter="10">
                                    <ProgressTemplate>
                                        <%--<div id="progressBackgroundFilter">
                                        </div>--%>
                                        <div id="processMessage">
                                            &nbsp;&nbsp;&nbsp;Procesando...<br />
                                            <br />
                                            <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td>
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
           
            <tr>
                <!-- DATA -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" style="overflow: auto; position: relative;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <CR:CrystalReportViewer ID="crptvLiquidacionVenta" runat="server" AutoDataBind="true"
                                                DisplayGroupTree="False" SeparatePages="False" HasCrystalLogo="False"
                                                PrintMode="ActiveX" HasToggleGroupTreeButton="False" HasViewList="False" ShowAllPageIds="True" />
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                            <asp:PostBackTrigger ControlID="crptvLiquidacionVenta" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>                                
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicio"
        PopupButtonID="imgbtnCalendarInicio">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin"
        PopupButtonID="imgbtnCalendarFin">
    </cc1:CalendarExtender>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtFechaFin"
        ControlToValidate="txtFechaInicio" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtFechaInicio"
        ControlToValidate="txtFechaFin" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    </form>
    <script src="javascript/resolucion.js" type="text/javascript"></script>
</body>
</html>
