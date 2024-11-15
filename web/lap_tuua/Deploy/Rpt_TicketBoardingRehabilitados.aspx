<%@ page language="C#" autoeventwireup="true" inherits="Rpt_TicketBoardingRehabilitados, App_Web_tx1el90t" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Tickets o Boarding Pass Rehabilitados </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_ticketrehab" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">

        function generarReporte() {
            var frm = window.document.forms[0];
            var codTipoTicket = frm.ddlTipoTicket[frm.ddlTipoTicket.selectedIndex].value;
            var desTipoTicket = frm.ddlTipoTicket[frm.ddlTipoTicket.selectedIndex].text;

            var codAerolinea = frm.ddlTipoAerolinea[frm.ddlTipoAerolinea.selectedIndex].value;
            var desAerolinea = frm.ddlTipoAerolinea[frm.ddlTipoAerolinea.selectedIndex].text;
            var horaInicial = frm.txtHoraDesde.value;
            var horaFinal = frm.txtHoraHasta.value;
            var codMotivo = frm.ddlMotivo[frm.ddlMotivo.selectedIndex].value;
            var desMotivo = frm.ddlMotivo[frm.ddlMotivo.selectedIndex].text
            var fechaInicial = frm.txtFechaDesde.value;
            var fechaFinal = frm.txtFechaHasta.value;

            var numVuelo = frm.txtNumVuelo.value;

            var ckTicket = frm.ckTicket.checked;
            var ckBoarding = frm.ckBoarding.checked;

            var documento = "0";

            if (ckTicket && ckBoarding) {
                documento = "0";
            }
            else {
                if (ckTicket) {
                    documento = "1";
                }

                if (ckBoarding) {
                    documento = "2";
                }
            }

            if (codTipoTicket == "0") {
                desTipoTicket = "Todos";
            }
            if (codAerolinea == "0") {
                desAerolinea = "Todos";
            }
            if (codMotivo == "0") {
                desMotivo = "Todos";
            }

            var parametros = "codtpticket=" + codTipoTicket + "&destpticket=" + desTipoTicket + "&codaerolinea=" + codAerolinea +
                             "&desaerolinea=" + desAerolinea + "&horainicial=" + horaInicial + "&horafinal=" + horaFinal +
                             "&fechainicial=" + fechaInicial + "&fechafinal=" + fechaFinal + "&codMotivo=" + codMotivo +
                             "&desMotivo=" + desMotivo + "&numvuelo=" + numVuelo + "&documento=" + documento;

            var ventimp = window.open("ReporteRPT/rptTicketBoardingRehabilitado.aspx?" + parametros, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=800");
            document.getElementById("lblMensajeError").innerHTML = "";
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

        function llamadaReporte() {
            if (validarCamposReporte()) {
                generarReporte();
            }
        }

        function cleanGrilla() {
            if (document.getElementById("grvTicketBoardingRehabilita") != null) {
                document.getElementById("grvTicketBoardingRehabilita").style.display = "none";
            }
            if (document.getElementById("grvResumen") != null) {
                document.getElementById("grvResumen").style.display = "none";
            }
        }

        function validarCamposReporte() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            //document.getElementById('lblTotal').innerHTML = "";

            //Validate Form
            var frm = window.document.forms[0];
            var ckTicket = frm.ckTicket.checked;
            var ckBoarding = frm.ckBoarding.checked;

            if (!(ckTicket || ckBoarding)) {
                cleanGrilla();
                document.getElementById('lblMensajeError').innerHTML = "Seleccione al menos un Tipo de Documento";
                return false;
            }
            if (validaHora()) {
                cleanGrilla();
                return false;
            }
            if (validaFecha()) {
                return true;
            } else {
                cleanGrilla();
                return false;
            }
        }

        function validarCampos() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            //document.getElementById('lblTotal').innerHTML = "";

            cleanGrilla();

            //Validate Form
            var frm = window.document.forms[0];
            var ckTicket = frm.ckTicket.checked;
            var ckBoarding = frm.ckBoarding.checked;

            if (!(ckTicket || ckBoarding)) {
                document.getElementById('lblMensajeError').innerHTML = "Seleccione al menos un Tipo de Documento";
                return false;
            }

            if (validaHora()) {
                return false;
            }

            return validaFecha();

        }

        function validaFecha() {
            var compFin = form1.txtFechaHasta.value.substr(6, 4) + form1.txtFechaHasta.value.substr(3, 2) + form1.txtFechaHasta.value.substr(0, 2);
            var compIni = form1.txtFechaDesde.value.substr(6, 4) + form1.txtFechaDesde.value.substr(3, 2) + form1.txtFechaDesde.value.substr(0, 2);
            if (compFin < compIni) {
                document.getElementById('lblMensajeError').innerHTML = "Rango incorrecto de Fechas";
                return false;
            } else {
                if (compFin == compIni) {
                    var horIni = form1.txtHoraDesde.value.substr(0, 2) + form1.txtHoraDesde.value.substr(3, 2) + form1.txtHoraDesde.value.substr(6, 2);
                    var horFin = form1.txtHoraHasta.value.substr(0, 2) + form1.txtHoraHasta.value.substr(3, 2) + form1.txtHoraHasta.value.substr(6, 2);

                    if (horFin < horIni) {
                        document.getElementById('lblMensajeError').innerHTML = "Rango incorrecto de Horas";
                        return false;
                    }
                }
            }
            return true;
        }

        function validaHora() {
            if (document.getElementById('txtHoraDesde').value == "__:__:__" && document.getElementById('txtHoraHasta').value != "__:__:__") {
                document.getElementById("lblMensajeError").innerHTML = "Defina un correcto rango de Horas o haga la consulta sin ingresar un rango de horas";
                return true;
            }
            if (document.getElementById('txtHoraDesde').value != "__:__:__" && document.getElementById('txtHoraHasta').value == "__:__:__") {
                document.getElementById("lblMensajeError").innerHTML = "Defina un correcto rango de Horas o haga la consulta sin ingresar un rango de horas";
                return true;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="14" style="height: 20px;">
                                            <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2" style="width: 72px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaDesde" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
                                                            PopupButtonID="imgbtnCalendarDesde">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td rowspan="2" style="width: 60px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="lblDocumento" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:CheckBox ID="ckTicket" runat="server" CssClass="TextoFiltro" Text="Ticket" Checked="True" />
                                            <!-- onclick="Javascript: SetCheckBoxTicket(this,'ckBoarding','ddlTipoTicket');"-->
                                            <asp:CheckBox ID="ckBoarding" runat="server" CssClass="TextoFiltro" Text="Boarding" />
                                            <!--onclick="Javascript: SetCheckBoxBoarding(this, 'ckTicket','ddlTipoTicket');"-->
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:DropDownList ID="ddlTipoAerolinea" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('NumeroyLetra');"
                                                Width="100%"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaHasta" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"
                                                            PopupButtonID="imgbtnCalendarHasta">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaFin0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraFin0" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMotivo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMotivo" runat="server" Width="280px" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="3" align="right">
                                            <asp:LinkButton ID="lbExportar" runat="server" OnClick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <br />
                                                    <br />
                                                    <a href="#" id="lnkHabilitar" runat="server" onclick="llamadaReporte()" style="cursor: hand;">
                                                        <b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" OnClientClick="return validarCampos()"
                                                        CssClass="Boton" ValidationGroup="grupito" Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                <!-- CONTENT -->
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:GridView ID="grvTicketBoardingRehabilita" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" AllowSorting="True" OnSorting="grvTicketBoardingRehabilita_Sorting"
                                                OnPageIndexChanging="grvTicketBoardingRehabilita_PageIndexChanging" OnRowCommand="grvTicketBoardingRehabilita_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Fecha Venta" SortExpression="FechaVenta">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaVenta0" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("FechaVenta")),null) %> '></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha Rehab" SortExpression="FechaRehab">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaRehab0" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("FechaRehab")),null) %> '></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hora Rehab" SortExpression="HoraRehab">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHoraRehab" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToHora(Convert.ToString(Eval("HoraRehab"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Dsc_Compania" HeaderText="Compañía" SortExpression="Dsc_Compania">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NumVuelo" HeaderText="Nro Vuelo" SortExpression="NumVuelo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DesMotivo" HeaderText="Motivo" SortExpression="DesMotivo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DesDocument" HeaderText="Documento" SortExpression="DesDocument">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Nro. Documento" SortExpression="Cod_Numero_Ticket">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="CodDocumento" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Secuencial" HeaderText="Secuencial" SortExpression="Secuencial">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <!--<asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>-->
                                            <br />
                                            <asp:GridView ID="grvResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" ShowFooter="false"  GroupingDepth="1"
                                                AutoGenerateColumns="False" GridLines="Both" 
                                                HorizontalAlign="Center" CssClass="grillaShort"
                                                OnRowCreated="grvResumen_RowCreated" OnRowDataBound = "grvResumen_RowDataBound">
                                                
                                                
                                                 <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="DesDocument" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" 
                                                            ItemStyle-HorizontalAlign="Right" NullDisplayText="0" >
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                
                                               <FooterStyle BackColor="#CCCCCC" Font-Bold="true" ForeColor="Black" HorizontalAlign="Right" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:HiddenField ID="lblTotalRows" runat="server" />
                                            <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server"
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
    <uc5:ConsDetTicket ID="ConsDetTicket1" runat="server" />
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    </form>
</body>
</html>
