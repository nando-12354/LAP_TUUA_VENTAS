<%@ page language="C#" autoeventwireup="true" inherits="Cns_TicketProcesado, App_Web_ehzg6gwo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta de Tickets Procesados</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @import url(css/calendar-system.css);
    </style>

    <script language="JavaScript" type="text/javascript">
        function controlaFechaDesde() {
            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtFechaDesde.value = form1.txtFechaDesde.value.substring(0, form1.txtFechaDesde.value.length - 2);
            }
            else {
                if (form1.txtFechaDesde.value.length == 2) {
                    form1.txtFechaDesde.value = form1.txtFechaDesde.value + '/';
                }
                if (form1.txtFechaDesde.value.length == 5) {
                    form1.txtFechaDesde.value = form1.txtFechaDesde.value + '/';
                }
            }
        }
        function controlaFechaHasta() {
            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtFechaHasta.value = form1.txtFechaHasta.value.substring(0, form1.txtFechaHasta.value.length - 2);
            }
            else {
                if (form1.txtFechaHasta.value.length == 2) {
                    form1.txtFechaHasta.value = form1.txtFechaHasta.value + '/';
                }
                if (form1.txtFechaHasta.value.length == 5) {
                    form1.txtFechaHasta.value = form1.txtFechaHasta.value + '/';
                }
            }
        }
        function validaFecha() {
            var compFin = form1.txtFechaHasta.value.substr(6, 4) + form1.txtFechaHasta.value.substr(3, 2) + form1.txtFechaHasta.value.substr(0, 2);
            var compIni = form1.txtFechaDesde.value.substr(6, 4) + form1.txtFechaDesde.value.substr(3, 2) + form1.txtFechaDesde.value.substr(0, 2);
            if (compFin < compIni) {

                document.getElementById("imgbtnCalendarDesde").focus();
                alert("Rango incorrecto de Fechas");

                return false
            }
            return true;
        }
        function imgPrint_onclick() {
            var idFechaDesde = document.getElementById("txtFechaDesde").value;
            var idFechaHasta = document.getElementById("txtFechaHasta").value;
            var idCajero = document.getElementById("ddlCajero").value;
            var idTurno = document.getElementById("txtTurno").value;

            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;

            var ventimp = window.open("ReporteCNS/rptTicketProcesados.aspx" + "?idFechaDesde=" + idFechaDesde + "&idFechaHasta=" + idFechaHasta + "&idCajero=" + idCajero + "&idTurno=" + idTurno + "&idOrdenacion=" + idOrdenacion + "&idColumna=" + idColumna + "&idPaginacion=" + idPaginacion, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=800");
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

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <table style="width: 100%; left: 0px; top: 0px;" class="alineaderecha">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan="3">
                                                        <!-- TITLE -->
                                                        <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td rowspan="2">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechaDesde" runat="server" Width="88px" CssClass="textboxFecha"
                                                                        Height="16px" MaxLength="10" onKeyDown="JavaScript:controlaFechaDesde();" onkeypress="JavaScript: Tecla('Time');"
                                                                        onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
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
                                                        <asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                            Width="22px" Height="22px" />
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
                                                            PopupButtonID="imgbtnCalendarDesde">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td rowspan="2">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtFechaHasta" runat="server" Width="88px" CssClass="textboxFecha"
                                                                        Height="16px" MaxLength="10" onKeyDown="JavaScript:controlaFechaHasta();" onkeypress="JavaScript: Tecla('Time');"
                                                                        onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                            Width="22px" Height="22px" />
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"
                                                            PopupButtonID="imgbtnCalendarHasta">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCajero" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCajero" runat="server" Width="300px" CssClass="combo2">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTurno" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTurno" runat="server" CssClass="textbox" MaxLength="6"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txtTurno_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtTurno">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td align="right">
                                            <asp:LinkButton ID="lbExportar" runat="server" OnClick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%-- <a href="" id="lnkExportar" runat="server" onclick="javascript:exportarExcel_onclick();"
                                                            style="cursor: hand;"><b>
                                                                <asp:Label ID="lblExportar" runat="server">[Exportar a Excel]</asp:Label>
                                                            </b></a>--%>
                                                    <br />
                                                    <br />
                                                    <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar8" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                                        Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
                            <td class="SpacingGrid" valign="bottom">
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvTurno" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                Width="100%" AllowSorting="True" OnSorting="grvTurno_Sorting" OnPageIndexChanging="grvTurno_PageIndexChanging">
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="Cod_Turno, Cod_Usuario" DataNavigateUrlFormatString="Cns_DetalleTicketProcesado.aspx?idturno={0}&amp;idusuario={1}"
                                                        DataTextField="Cod_Turno" HeaderText="Turno" NavigateUrl="Cns_DetalleTicketProcesado.aspx"
                                                        SortExpression="Cod_Turno" />
                                                    <asp:TemplateField HeaderText="Cajero" SortExpression="ape_usuario">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTPCajero" runat="server" Text='<%# String.Format("{0} {1} ( {2} )", Eval("Ape_Usuario"), Eval("Nom_Usuario"), Eval("Cod_Usuario")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Dsc_Estacion" HeaderText="Equipo" SortExpression="Dsc_Estacion" />
                                                    <asp:TemplateField HeaderText="Fecha de Apertura" SortExpression="Fch_Inicio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaApertura" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Inicio")),Convert.ToString(Eval("Hor_Inicio"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Ticket_Proc" HeaderText="Nro. Ticket Vendidos" SortExpression="Ticket_Proc" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <asp:HiddenField ID="lblTotalRows" runat="server" />
                                            <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar8" EventName="Click" />
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
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <!--Declaracion de Calendarios -->
    <!--Primera validacion de las fechas -->
    <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ErrorMessage="Ingrese fecha inicial"
        ControlToValidate="txtFechaDesde" Display="None" ForeColor="White">*</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ErrorMessage="Ingrese fecha final"
        ControlToValidate="txtFechaHasta" Display="None" ForeColor="White">*</asp:RequiredFieldValidator>
    <cc1:ValidatorCalloutExtender ID="vceFechaDesde" runat="server" TargetControlID="rfvFechaDesde">
    </cc1:ValidatorCalloutExtender>
    <cc1:ValidatorCalloutExtender ID="vceFechaHasta" runat="server" TargetControlID="rfvFechaHasta">
    </cc1:ValidatorCalloutExtender>
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
    </form>
</body>
</html>
