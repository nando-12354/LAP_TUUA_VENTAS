<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_TicketsVencidos.aspx.cs"
    Inherits="Rpt_TicketsVencidos" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Tickets Vencidos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>

    <script language="JavaScript" type="text/javascript">

        function imgPrint_onclick() {
            //Filtros
            var idFechaDesde = document.getElementById("txtDesde").value;
            var idFechaHasta = document.getElementById("txtHasta").value;
            var idTipoDocumento = document.getElementById("ddlTipoTicket").value;

            //Descripciones
            var idDscT = (idTipoDocumento != "0") ? document.getElementById("ddlTipoTicket").options[document.getElementById("ddlTipoTicket").selectedIndex].text : "";

            //Dimension Ventana
            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptTicketVencidos.aspx" + "?sFechaDesde=" + idFechaDesde + "&sFechaHasta=" + idFechaHasta + "&sTipoTicket=" + idTipoDocumento + "&idDscT=" + idDscT
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.focus();
        }

        function validarExcel() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;

            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    return true;
                }
                else {
                    alert("La exportación a excel solo permite " + maxRegistros + " registros");
                    return false;
                }
            }
            else {
                alert("No existen registros para exportar \nSeleccione otros filtros");
                return false;
            }
        }
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
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
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha Creación:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
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
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
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
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo2" Width="250px">
                                            </asp:DropDownList>
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
                                <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                                    OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click"
                                                CssClass="Boton" />&nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                    DisplayAfter="10">
                                    <ProgressTemplate>
                                        <div id="progressBackgroundFilter">
                                        </div>
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
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvResumenVencidos" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Both" Width="100%" AllowPaging="true" OnPageIndexChanging="grvResumenVencidos_PageIndexChanging"
                                                CssClass="grilla" OnRowCommand="grvResumenVencidos_RowCommand">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. Ticket">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codTipoDocumento" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowTipoDocumento" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="TipoTicket" />
                                                    <asp:BoundField HeaderText="Compañía" DataField="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Fecha Vencimiento" DataField="Fch_Vencimiento" />
                                                    <asp:BoundField HeaderText="Dias Vencido" DataField="DiasVencidos" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                            </cc2:PagingGridView>
                                            <br />
                                           <div>
                                                <cc2:PagingGridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False" 
                                                    HorizontalAlign="Center" CssClass="grillaShort" AllowPaging="False" AllowSorting="False"
                                                    OnRowCreated="grvDataResumen_RowCreated" OnRowDataBound="grvDataResumen_RowDataBound"
                                                    GroupingDepth="1" ShowFooter="true">
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <asp:HiddenField ID="lblTotalRows" runat="server" />
                                            <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server"
                                        DisplayAfter="100">
                                        <ProgressTemplate>
                                            <div id="progressBackgroundFilter">
                                            </div>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <uc3:ConsDetTicket ID="ConsDetTicket1" runat="server" />
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDesde"
        PopupButtonID="imgbtnCalendar1">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtHasta"
        PopupButtonID="imgbtnCalendar2">
    </cc1:CalendarExtender>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtHasta"
        ControlToValidate="txtDesde" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtDesde"
        ControlToValidate="txtHasta" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    </form>
</body>
</html>
