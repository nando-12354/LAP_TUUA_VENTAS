<%@ page language="C#" autoeventwireup="true" inherits="Rpt_MovimientoTicketContingencia, App_Web_7ctknflu" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Movimiento Tickets de Contingencia</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">

        function imgPrint_onclick() {
            var frm = window.document.forms[0];
            var codTipoTicket = frm.ddlTipoTicket[frm.ddlTipoTicket.selectedIndex].value;
            var desTipoTicket = frm.ddlTipoTicket[frm.ddlTipoTicket.selectedIndex].text;

            var codEstadoTicket = frm.ddlEstadoTicket[frm.ddlEstadoTicket.selectedIndex].value;
            var desEstadoTicket = frm.ddlEstadoTicket[frm.ddlEstadoTicket.selectedIndex].text;
            var ranInicialTicket = frm.txtRangoDesde.value;
            var ranFinalTicket = frm.txtRangoHasta.value;
            var codEstadoInicialTicket = frm.ddlEstTckPerido[frm.ddlEstTckPerido.selectedIndex].value;
            var desEstadoInicialTicket = frm.ddlEstTckPerido[frm.ddlEstTckPerido.selectedIndex].text
            var fechaInicial = frm.txtFechaDesde.value;
            var fechaFinal = frm.txtFechaHasta.value;

            if (codTipoTicket == "0") {
                desTipoTicket = "Todos";
            }
            if (codEstadoTicket == "0") {
                desEstadoTicket = "Todos";
            }
            if (codEstadoInicialTicket == "0") {
                desEstadoInicialTicket = "Todos";
            }

            if (ranInicialTicket == "") {
                ranInicialTicket = "0"
            }

            if (ranFinalTicket == "") {
                ranFinalTicket = "0"
            }

            if (validarCampos(false)) {
                var w = 900 + 32;
                var h = 500 + 96;
                var wleft = (screen.width - w) / 2;
                var wtop = (screen.height - h) / 2;

                var parametros = "codtpticket=" + codTipoTicket + "&destpticket=" + desTipoTicket + "&codesticket=" + codEstadoTicket +
                             "&desesticket=" + desEstadoTicket + "&raninicial=" + ranInicialTicket + "&ranfinal=" + ranFinalTicket +
                             "&fechainicial=" + fechaInicial + "&fechafinal=" + fechaFinal + "&codesiniticket=" + codEstadoInicialTicket +
                             "&desesiniticket=" + desEstadoInicialTicket;

                var ventimp = window.open("ReporteRPT/rptMovimientoTicketContingencia.aspx?" + parametros, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
                ventimp.focus();
            } else {
                cleanGrilla();
            }
        }

        function validarCampos(tip) {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            if (tip) {
                cleanGrilla();
            }
            //Valida Rango Fecha
            if (!(isValidoRangoFecha(document.getElementById('txtFechaDesde').value, '',
                                   document.getElementById('txtFechaHasta').value, ''))) {
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }

            //Valida Rango Nro. Ticket
            if (document.getElementById('txtRangoHasta').value < document.getElementById('txtRangoDesde').value) {
                document.getElementById("imgbtnCalendarDesde").focus();
                document.getElementById("lblMensajeError").innerHTML = "No se puede imprimir, ingrese un rango correcto de Tickets";
                return false
            }
            var frm = window.document.forms[0];
            var ranInicialTicket = frm.txtRangoDesde.value;
            var ranFinalTicket = frm.txtRangoHasta.value;

            if (ranFinalTicket != "" && ranInicialTicket == "") {
                document.getElementById("lblMensajeError").innerHTML = "No se puede imprimir, ingrese un rango correcto de Tickets";
                return false;
            }
            if (ranFinalTicket == "" && ranInicialTicket != "") {
                document.getElementById("lblMensajeError").innerHTML = "No se puede imprimir, ingrese un rango correcto de Tickets";
                return false;
            }
            return true;
        }

        function cleanGrilla() {
            if (document.getElementById("grvMovimientoTicketCont") != null) {
                document.getElementById("grvMovimientoTicketCont").style.display = "none";
            }
            if (document.getElementById("grvResumen") != null) {
                document.getElementById("grvResumen").style.display = "none";
            }
        }        
    </script>

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
                                            <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="6">
                                        </td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblRangoTicket" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEstTckPerido" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlEstTckPerido" runat="server" Width="160px" CssClass="combo">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEstadoTicket" runat="server" CssClass="TextoFiltro" Width="70px"></asp:Label>                                            
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:DropDownList ID="ddlEstadoTicket" runat="server" Width="200px" CssClass="combo">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblTicketDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRangoDesde" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('Integer');" onblur="gDecimal(this)"
                                                MaxLength="16"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaDesde" runat="server" Width="88px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaHasta" runat="server" Width="88px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>                                            
                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>                                            
                                        </td>
                                        <td style="width: 200px;">
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" Width="200px" CssClass="combo">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40px;">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Label ID="lblTicketHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRangoHasta" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('Integer');" onblur="gDecimal(this)"
                                                MaxLength="16"></asp:TextBox>
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
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                            OnClientClick="return validarCampos(true)" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel2" runat="server"
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
                <td>
                    <!-- DATA RESULT ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div>
                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label></div>
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvMovimientoTicketCont" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" AllowSorting="True" OnSorting="grvMovimientoTicketCont_Sorting"
                                                OnPageIndexChanging="grvMovimientoTicketCont_PageIndexChanging" OnRowCommand="grvMovimientoTicketCont_RowCommand"
                                                OnRowDataBound="grvMovimientoTicketCont_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Fecha" DataField="Fch_Modificacion" SortExpression="Fch_Modificacion2" />
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Dsc_Tipo_Ticket" SortExpression="Dsc_Tipo_Ticket" />
                                                    <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Estado_Ticket" SortExpression="Dsc_Estado_Ticket" />
                                                    <asp:BoundField DataField="Cod_Estado_Ticket" Visible="false" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <br />
                                            <div>
                                                <asp:GridView ID="grvResumen" runat="server" CellPadding="3" BorderColor="#999999"
                                                    BorderStyle="None" BorderWidth="1px" GridLines="Both" HorizontalAlign="Center"
                                                    CssClass="grillaShort" OnRowCreated="grvResumen_RowCreated">
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td>
                                <uc2:PiePagina ID="PiePagina1" runat="server" />
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
        PopupButtonID="imgbtnCalendarDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"
        PopupButtonID="imgbtnCalendarHasta">
    </cc1:CalendarExtender>
    <!--Segunda Validacion de las fechas -->
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtFechaHasta"
        ControlToValidate="txtFechaDesde" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtFechaDesde"
        ControlToValidate="txtFechaHasta" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    <!-- Mensajes de segunda validacion en el rango -->
    <!--Tercera Validacion de rangos -->
    <asp:CompareValidator ID="cvFiltroRangoDesde" runat="server" ControlToCompare="txtRangoHasta"
        ControlToValidate="txtRangoDesde" Display="None" ErrorMessage="Filtro de Rangos invalido"
        Operator="LessThanEqual" Type="Integer"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroRangoDesde" runat="server" TargetControlID="cvFiltroRangoDesde">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroRangoHasta" runat="server" ControlToCompare="txtRangoDesde"
        ControlToValidate="txtRangoHasta" Display="None" ErrorMessage="Filtro de Rangos invalido"
        Operator="GreaterThanEqual" Type="Integer"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroRangoHasta" runat="server" TargetControlID="cvFiltroRangoHasta">
    </cc1:ValidatorCalloutExtender>
    <uc5:ConsDetTicket ID="ConsDetTicket1" runat="server" />
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    </form>    
</body>
</html>
