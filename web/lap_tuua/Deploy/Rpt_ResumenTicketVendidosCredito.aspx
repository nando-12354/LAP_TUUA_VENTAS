<%@ page language="C#" autoeventwireup="true" inherits="Rpt_ResumenTicketVendidosCredito, App_Web_7ctknflu" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Resumen de Ticket Vendidos al Credito o Contado</title>
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
            var sDesde = document.forms[0].txtFechaInicio.value;
            var sHasta = document.forms[0].txtFechaFin.value;


            var sTicket = document.forms[0].ddlTipoTicket[document.forms[0].ddlTipoTicket.selectedIndex].value;
            var sVuelo = document.forms[0].txtNumeroVuelo.value;
            var sCompania = document.forms[0].ddlTipoAerolinea[document.forms[0].ddlTipoAerolinea.selectedIndex].value;
            var sPago = document.forms[0].ddlTipoPago[document.forms[0].ddlTipoPago.selectedIndex].value;

            //Descripciones
            var sDscCompania = (sCompania != "0") ? document.getElementById("ddlTipoAerolinea").options[document.getElementById("ddlTipoAerolinea").selectedIndex].text : "";
            var sDscTicket = (sTicket != "0") ? document.getElementById("ddlTipoTicket").options[document.getElementById("ddlTipoTicket").selectedIndex].text : "";
            var sDscPago = (sPago != "0") ? document.getElementById("ddlTipoPago").options[document.getElementById("ddlTipoPago").selectedIndex].text : "";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptResumenTicketVendidosCredito.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta + "&sTicket=" + sTicket
                                        + "&sVuelo=" + sVuelo + "&sCompania=" + sCompania + "&sPago=" + sPago
                                        + "&sDscCompania=" + sDscCompania + "&sDscTicket=" + sDscTicket + "&sDscPago=" + sDscPago
                                        , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            //ventimp.moveTo(wleft, wtop);
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

        function validarCampos() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtFechaInicio').value,
                                   '',
                                   document.getElementById('txtFechaFin').value,
                                   ''
                                   )) {
                //document.getElementById('lblMensajeError').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').innerHTML = "";
                return true;
            } else {
                //document.getElementById('divData').style.visibility = 'hidden';
                //document.getElementById('lblMensajeError').style.visibility = 'visible';
                //alert('ggfg');
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }
        }

        function cleanGrilla() {
            if (document.getElementById("grvResumenCredito") != null) {
                document.getElementById("grvResumenCredito").style.display = "none";
            }
            if (document.getElementById("grvDataResumen") != null) {
                document.getElementById("grvDataResumen").style.display = "none";
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
                    <div>
                        <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                    </div>
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 85%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="9" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Emisión:</asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- FIRST ROW FILTER -->
                                        <td style="width: 5%;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaInicial" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaInicio" runat="server" Width="72px" CssClass="textboxFecha"
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
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnCalendarInicio" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td style="width: 2%;">
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 24%;">
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 2%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblTipoAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 35%;">
                                            <asp:DropDownList ID="ddlTipoAerolinea" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
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
                                        <!-- SECOND ROW FILTER -->
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaFinal" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2" style="width: 78px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaFin" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();"></asp:TextBox>
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
                                            <asp:ImageButton ID="imgbtnCalendarFin" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoPago" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoPago" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumeroVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumeroVuelo" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                MaxLength="10" onkeypress="JavaScript: Tecla('NumeroyLetra');"></asp:TextBox>
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
                                </table>
                            </td>
                            <td align="right">
                                <asp:LinkButton ID="lbExportar" runat="server" OnClick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClientClick="return validarCampos()"
                                            OnClick="btnConsultar_Click" />
                                        &nbsp;&nbsp;&nbsp;
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvResumenCredito" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" OnPageIndexChanging="grvResumenCredito_PageIndexChanging"
                                                CssClass="grilla" GroupingDepth="4" AllowSorting="True" 
                                                VirtualItemCount="-1" onsorting="grvResumenCredito_Sorting">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Venta" DataField="Fch_Venta" SortExpression="Fecha_Venta" />
                                                    <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Dsc_Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Dsc_Tipo_Ticket" />
                                                    <asp:BoundField HeaderText="Cantidad" DataField="Can_Venta" />
                                                    <asp:BoundField DataField="Cod_Venta_Masiva" HeaderText="Código Venta Masiva" />
                                                    <asp:BoundField HeaderText="Representante" DataField="Dsc_Repte" />
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
                                                    GroupingDepth="2" ShowFooter="true">  
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                        <asp:BoundField HeaderText="Tipo Venta" DataField="Tip_Venta" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="Importe Vendido" DataField="Monto" NullDisplayText="0" ItemStyle-HorizontalAlign="Right"/>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
                                                <%--<asp:GridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False"
                                                    HorizontalAlign="Center" CssClass="grillaShort" AllowPaging="False" AllowSorting="False"
                                                    OnRowCreated="grvDataResumen_RowCreated" OnRowDataBound="grvDataResumen_RowDataBound"
                                                    GroupingDepth="2" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                        <asp:BoundField HeaderText="Tipo Venta" DataField="Tip_Venta" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField HeaderText="Importe Vendido" DataField="Monto" NullDisplayText="0"
                                                            ItemStyle-HorizontalAlign="Right" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>--%>
                                                <asp:HiddenField ID="lblTotalRows" runat="server" />
                                                <asp:HiddenField ID="lblMaxRegistros" runat="server" />
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
                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
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
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicio"
        PopupButtonID="imgbtnCalendarInicio">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin"
        PopupButtonID="imgbtnCalendarFin">
    </cc1:CalendarExtender>
    </form>
</body>
</html>
