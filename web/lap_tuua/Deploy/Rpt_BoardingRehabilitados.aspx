<%@ page language="C#" autoeventwireup="true" inherits="Rpt_TicketBoardingRehabilitados, App_Web_ehzg6gwo" responseencoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Boarding Pass Rehabilitados </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_ticketrehab" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">

        function onOk() { }
        function imgPrint_onclick() {

            var sFechaDesde = document.getElementById("txtFechaDesde").value;
            var sFechaHasta = document.getElementById("txtFechaHasta").value;
            var sHoraDesde = document.getElementById("txtHoraDesde").value;
            var sHoraHasta = document.getElementById("txtHoraHasta").value;
            var sCompania = document.getElementById("ddlTipoAerolinea").value;
            var sMotivo = document.getElementById("ddlMotivo").value;
            var sTipoVuelo = document.getElementById("ddlTipoVuelo").value;
            var sTipoPersona = document.getElementById("ddlTipoPersona").value;
            var sNumVuelo = document.getElementById("txtNumVuelo").value;

            //Descripciones
            var idDscC = (sCompania != "0") ? document.getElementById("ddlTipoAerolinea").options[document.getElementById("ddlTipoAerolinea").selectedIndex].text : " -TODOS- ";
            var idDscM = (sMotivo != "0") ? document.getElementById("ddlMotivo").options[document.getElementById("ddlMotivo").selectedIndex].text : " -TODOS- ";
            var idDscV = (sTipoVuelo != "0") ? document.getElementById("ddlTipoVuelo").options[document.getElementById("ddlTipoVuelo").selectedIndex].text : " -TODOS- ";
            var idDscP = (sTipoPersona != "0") ? document.getElementById("ddlTipoPersona").options[document.getElementById("ddlTipoPersona").selectedIndex].text : " -TODOS- ";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptBoardingRehabilitados.aspx" + "?sFechaDesde=" + sFechaDesde + "&sFechaHasta=" + sFechaHasta + "&sHoraDesde=" +
                                        sHoraDesde + "&sHoraHasta=" + sHoraHasta + "&sCompania=" + sCompania + "&sMotivo=" +
                                        sMotivo + "&sTipoVuelo=" + sTipoVuelo + "&sTipoPersona=" + sTipoPersona + "&sNumVuelo=" + sNumVuelo
                                        + "&idDscC=" + idDscC
                                        + "&idDscM=" + idDscM
                                        + "&idDscV=" + idDscV
                                        + "&idDscP=" + idDscP
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

    <style type="text/css">
        .style1
        {
            width: 6%;
        }
        .style2
        {
            width: 1%;
        }
        .style3
        {
            height: 24px;
        }
        .style4
        {
            width: 1%;
            height: 24px;
        }
        .style5
        {
            width: 6%;
            height: 24px;
        }
    </style>
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
                                        <td rowspan="4" align="right">
                                             <b>
                        <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                            OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <br />
                            <br />
                            <a href="#" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                style="cursor: hand;"><b>
                                    <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                </b></a>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
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
                    </asp:UpdateProgress></td>
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
                                        <td class="style2">
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 15%;">
                                            <!-- onclick="Javascript: SetCheckBoxTicket(this,'ckBoarding','ddlTipoTicket');"-->
                                            <!--onclick="Javascript: SetCheckBoxBoarding(this, 'ckTicket','ddlTipoTicket');"-->
                                            <asp:DropDownList ID="ddlTipoAerolinea" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:DropDownList ID="ddlTipoVuelo" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 10%;">
                                            <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" onkeypress="" Width="99%"></asp:TextBox>
                                        </td>
                                        <td rowspan="2">
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style2">
                                            &nbsp;
                                        </td>
                                        <td class="style1">
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                &nbsp;
                            </td>
                        </tr>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">
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
                <td class="style3">
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
                <td class="style4">
                </td>
                <td class="style5">
                    <asp:Label ID="lblMotivo" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlMotivo" runat="server" Width="280px" CssClass="combo2">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                </td>
                <td class="style3">
                    <asp:Label ID="lblTipoPersona" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlTipoPersona" runat="server" CssClass="combo2" Width="100%">
                    </asp:DropDownList>
                </td>
                <td class="style3">
                </td>
                <td colspan="3" align="right" class="style3">
                   
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
        </td> </tr> </table> </td> </tr>
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
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <cc1:PagingGridView ID="grvBoardingRehabilita" runat="server" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grilla" GridLines="Vertical"
                                            GroupingDepth="0" OnPageIndexChanging="grvBoardingRehabilita_PageIndexChanging"
                                            OnRowCommand="grvBoardingRehabilita_RowCommand" OnSorting="grvBoardingRehabilita_Sorting"
                                            VirtualItemCount="-1" Width="100%" DataKeyNames="Num_Secuencial_Bcbp">
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <EditRowStyle BorderColor="#FF0066" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <RowStyle BorderStyle="solid" BorderColor="#E6E6E6" BorderWidth="1px" />
                                            <Columns>
                                                <asp:BoundField DataField="Fch_Uso" HeaderText="Fecha Ultimo Uso" SortExpression="Fch_Uso">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Nom_Pasajero" HeaderText="Nombre Pasajero" SortExpression="Nom_Pasajero" />
                                                <asp:BoundField DataField="Num_Asiento" HeaderText="Nro. Asiento" SortExpression="Num_Asiento" />
                                                <asp:BoundField DataField="Dsc_Compania" HeaderText="Compañía" SortExpression="Dsc_Compania" />
                                                <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo" />
                                                <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:BoundField DataField="Fch_Rehabilitacion" HeaderText="Fecha Rehab." SortExpression="Fch_Rehabilitacion" />
                                                <asp:BoundField DataField="DesMotivo" HeaderText="Motivo" SortExpression="DesMotivo" />
                                                <asp:BoundField DataField="Secuencial" HeaderText="Secuencial" SortExpression="Secuencial">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Nro. SEAE" SortExpression="Cod_Numero_Bcbp">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Num_Secuencial_Bcbp" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ShowBoarding" Text='<%# Eval("Cod_Numero_Bcbp") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Num_Secuencial_Bcbp" HeaderText="Nro. Boarding" Visible="False" />
                                                <asp:BoundField DataField="Num_Proceso_Rehab" HeaderText="Codigo Rehab." SortExpression="Num_Proceso_Rehab" />
                                                <asp:BoundField DataField="TipoVuelo" HeaderText="Tipo Vuelo" SortExpression="TipoVuelo" />
                                            </Columns>
                                        </cc1:PagingGridView>
                                        <br />
                                        <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <asp:HiddenField ID="lblTotalRows" runat="server" />
                                        <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        <br />
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
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    </form>
</body>
</html>
