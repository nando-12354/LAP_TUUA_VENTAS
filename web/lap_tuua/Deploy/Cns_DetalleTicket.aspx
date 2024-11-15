<%@ page language="C#" autoeventwireup="true" inherits="Cns_DetalleTicket, App_Web_ehzg6gwo" responseencoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Detalle Ticket o Boarding Pass</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_detalleticket" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">


        function validar() {

            if (document.getElementById("rbtnNumTicket").checked == true) {

                document.getElementById("txtNumTicket").disabled = false;
                document.getElementById("txtTicketDesde").disabled = true;
                document.getElementById("txtTicketHasta").disabled = true;
                document.getElementById("ddlCompania").disabled = true;
                document.getElementById("txtNumVuelo").disabled = true;
                document.getElementById("txtFechaVuelo").disabled = true;
                document.getElementById("txtNumAsiento").disabled = true;
                document.getElementById("txtPasajero").disabled = true;

                document.getElementById("txtNumTicket").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtTicketDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTicketHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNumVuelo").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtFechaVuelo").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNumAsiento").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtPasajero").style.backgroundColor = '#CCCCCC';


                document.getElementById("txtTicketDesde").value = '';
                document.getElementById("txtTicketHasta").value = '';
                document.getElementById("txtNumVuelo").value = '';
                document.getElementById("txtFechaVuelo").value = '';
                document.getElementById("txtNumAsiento").value = '';
                document.getElementById("txtPasajero").value = '';


                document.getElementById("lblNumTicket").disabled = false;
                document.getElementById("lblTicketDesde").disabled = true;
                document.getElementById("lblTicketHasta").disabled = true;
                document.getElementById("lblCompania").disabled = true;
                document.getElementById("lblNumVuelo").disabled = true;
                document.getElementById("lblFechaVuelo").disabled = true;
                document.getElementById("lblNumAsiento").disabled = true;
                document.getElementById("lblPasajero").disabled = true;

                document.getElementById("imgbtnCalendar1").disabled = true;

            }

            if (document.getElementById("rbtnRangoTicket").checked == true) {

                document.getElementById("txtNumTicket").disabled = true;
                document.getElementById("txtTicketDesde").disabled = false;
                document.getElementById("txtTicketHasta").disabled = false;
                document.getElementById("ddlCompania").disabled = true;
                document.getElementById("txtNumVuelo").disabled = true;
                document.getElementById("txtFechaVuelo").disabled = true;
                document.getElementById("txtNumAsiento").disabled = true;
                document.getElementById("txtPasajero").disabled = true;

                document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTicketDesde").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtTicketHasta").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNumVuelo").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtFechaVuelo").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNumAsiento").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtPasajero").style.backgroundColor = '#CCCCCC';

                document.getElementById("txtNumTicket").value = '';
                document.getElementById("txtNumVuelo").value = '';
                document.getElementById("txtFechaVuelo").value = '';
                document.getElementById("txtNumAsiento").value = '';
                document.getElementById("txtPasajero").value = '';


                document.getElementById("lblNumTicket").disabled = true;
                document.getElementById("lblTicketDesde").disabled = false;
                document.getElementById("lblTicketHasta").disabled = false;
                document.getElementById("lblCompania").disabled = true;
                document.getElementById("lblNumVuelo").disabled = true;
                document.getElementById("lblFechaVuelo").disabled = true;
                document.getElementById("lblNumAsiento").disabled = true;
                document.getElementById("lblPasajero").disabled = true;

                document.getElementById("imgbtnCalendar1").disabled = true;

            }

            if (document.getElementById("rbtnBoarding").checked == true) {

                document.getElementById("txtNumTicket").disabled = true;
                document.getElementById("txtTicketDesde").disabled = true;
                document.getElementById("txtTicketHasta").disabled = true;
                document.getElementById("ddlCompania").disabled = false;
                document.getElementById("txtNumVuelo").disabled = false;
                document.getElementById("txtFechaVuelo").disabled = false;
                document.getElementById("txtNumAsiento").disabled = false;
                document.getElementById("txtPasajero").disabled = false;


                document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTicketDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTicketHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNumVuelo").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtFechaVuelo").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNumAsiento").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtPasajero").style.backgroundColor = '#FFFFFF';


                document.getElementById("txtNumTicket").value = '';
                document.getElementById("txtTicketDesde").value = '';
                document.getElementById("txtTicketHasta").value = '';


                document.getElementById("lblNumTicket").disabled = true;
                document.getElementById("lblTicketDesde").disabled = true;
                document.getElementById("lblTicketHasta").disabled = true;
                document.getElementById("lblCompania").disabled = false;
                document.getElementById("lblNumVuelo").disabled = false;
                document.getElementById("lblFechaVuelo").disabled = false;
                document.getElementById("lblNumAsiento").disabled = false;
                document.getElementById("lblPasajero").disabled = false;

                document.getElementById("imgbtnCalendar1").disabled = false;
            }

        }

        function imgPrint_onclick() {
            var NumTicket = document.getElementById("txtNumTicket").value;

            var NumTicketDesde = document.getElementById("txtTicketDesde").value;
            var NumTicketHasta = document.getElementById("txtTicketHasta").value;

            var idCompania = document.getElementById("ddlCompania").value;
            var idNumVuelo = document.getElementById("txtNumVuelo").value;
            var idFechaVuelo = document.getElementById("txtFechaVuelo").value;
            var idNumAsiento = document.getElementById("txtNumAsiento").value;
            var idPasajero = document.getElementById("txtPasajero").value;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            if (document.getElementById("rbtnNumTicket").checked == true) {

                if (NumTicket == "") {
                    alert("Ingrese número de ticket a imprimir");
                }
                else {
                    var ventimp = window.open("ReporteCNS/rptDetalleTicket.aspx" + "?idNumTicket=" + NumTicket, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
                    ventimp.focus();
                }
            }


            if (document.getElementById("rbtnRangoTicket").checked == true) {
                if ((NumTicketDesde != "" && NumTicketHasta != "") || (NumTicketDesde == "" && NumTicketHasta != "") || (NumTicketDesde != "" && NumTicketHasta == "")) {
                    var ventimp = window.open("ReporteCNS/rptDetalleTicket.aspx" + "?idNumTicketDesde=" + NumTicketDesde + "&idNumTicketHasta=" + NumTicketHasta, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
                    ventimp.focus();
                }
                else { alert("Ingrese los números de ticket a imprimir"); }
            }


            if (document.getElementById("rbtnBoarding").checked == true) {

                var iPOS = document.getElementById("ddlCompania").selectedIndex;
                var selected_text = document.getElementById("ddlCompania").options[iPOS].text;
                if (iPOS == 0)
                    selected_text = "";

                if (idCompania != "" && idFechaVuelo != "") {
                    var ventimp = window.open("ReporteCNS/rptBoarding.aspx" + "?idCompania=" + idCompania + "&idNumVuelo=" + idNumVuelo + "&idFechaVuelo=" + idFechaVuelo + "&idNumAsiento=" + idNumAsiento + "&idPasajero=" + idPasajero + "&idDscCompania=" + selected_text, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
                    ventimp.focus();
                }
                else { alert("Ingrese datos del Boarding a imprimir"); }
            }
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

        function controlaFecha() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                //form1.txtFechaVuelo.value = form1.txtFechaVuelo.value.substring(0, form1.txtFechaVuelo.value.length - 2);
            }
            else {

                if (form1.txtFechaVuelo.value.length == 2) {
                    form1.txtFechaVuelo.value = form1.txtFechaVuelo.value + '/';
                }
                if (form1.txtFechaVuelo.value.length == 5) {
                    form1.txtFechaVuelo.value = form1.txtFechaVuelo.value + '/';
                }
            }
        }

        function metodoClick() {

            if (document.getElementById("rbtnNumTicket").checked == true) {

                if (form1.txtNumTicket.value == '') {
                    document.getElementById('txtMensajeError').innerHTML = 'Ingrese número de ticket';
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'hidden';
                    return false;
                }
                if (form1.txtNumTicket.value.length < 16) {
                    document.getElementById('txtMensajeError').innerHTML = 'El numero Ticket tiene un formato no válido, debe contener 16 caracteres.';
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'hidden';
                    return false;
                }
                else {
                    //document.getElementById('grvDetalleTicketPagin').innerHTML = '';
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'visible';
                    document.getElementById('txtMensajeError').innerHTML = '';
                }

            }

            if (document.getElementById("rbtnRangoTicket").checked == true) {
                if (form1.txtTicketDesde.value == '' && form1.txtTicketHasta.value == '') {
                    document.getElementById('txtMensajeError').innerHTML = 'Ingrese rango de ticketss';
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'hidden';
                    return false;
                }
                else {
                    if (form1.txtTicketDesde.value > form1.txtTicketHasta.value) {
                        document.getElementById('txtMensajeError').innerHTML = 'Rangos ticket invalidos';
                        document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'hidden';
                        return false;
                    }
                    else {
                        document.getElementById('txtMensajeError').innerHTML = '';
                    }
                }
                document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'visible';

            }

            if (document.getElementById("rbtnBoarding").checked == true) {
                if (form1.txtFechaVuelo.value == '') {
                    document.getElementById('txtMensajeError').innerHTML = 'Ingrese la fecha de vuelo';
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'hidden';
                    return false;
                }
                else {
                    document.getElementById('pnlPanelDetalleDocumento').style.visibility = 'visible';
                    document.getElementById('txtMensajeError').innerHTML = '';
                }

            }
            return true;
        }

    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:validar()">
    <form id="form1" runat="server" onsubmit="return metodoClick()">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td class="Espacio1FilaTabla" colspan="3" align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td style="height: 40px">
                    &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnNumTicket" runat="server" GroupName="TipoConsulta"
                        onClick="validar();" CssClass="TextoFiltro" Text="Por Nro. Ticket" Width="100px" />
                </td>
                <td align="left">
                    <asp:Label ID="lblNumTicket" runat="server" CssClass="TextoFiltro" Width="85px"></asp:Label>
                    <asp:TextBox ID="txtNumTicket" runat="server" CssClass="textbox" MaxLength="16" onkeypress="JavaScript: Tecla('Integer');"
                        onblur="gDecimal(this)"></asp:TextBox>
                    &nbsp;
                </td>
                <td align="right">
                    <b>
                        <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                            OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                style="cursor: hand;"><b>
                                    <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                </b></a>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                        <ProgressTemplate>
                            <div id="processMessage">
                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                <br />
                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td width="8%">
                    &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnRangoTicket" runat="server" GroupName="TipoConsulta"
                        onClick="validar();" CssClass="TextoFiltro" Text="Por Rango Ticket" />
                </td>
                <td align="left">
                    <asp:Label ID="lblTicketDesde" runat="server" CssClass="TextoFiltro" Width="85px"></asp:Label>
                    <asp:TextBox ID="txtTicketDesde" runat="server" CssClass="textbox" MaxLength="16"
                        onkeypress="JavaScript: Tecla('Integer');" onblur="gDecimal(this)"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTicketHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtTicketHasta" runat="server" CssClass="textbox" MaxLength="16"
                        onkeypress="JavaScript: Tecla('Integer');" onblur="gDecimal(this)"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td width="8%">
                    &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbtnBoarding" runat="server" GroupName="TipoConsulta"
                        onClick="validar();" CssClass="TextoFiltro" Text="Por Boarding" />
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro" Width="85px"></asp:Label>
                    <asp:DropDownList ID="ddlCompania" runat="server" Width="273px">
                    </asp:DropDownList>
                    &nbsp;
                    <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtFechaVuelo" runat="server" CssClass="textbox" MaxLength="10"
                        onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                        Width="22px" Height="22px" />
                    <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                        PopupButtonID="imgbtnCalendar1" TargetControlID="txtFechaVuelo" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:Label ID="lblFechaVuelo1" runat="server" CssClass="TextoFiltro">(dd/mm/yyyy)</asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                    <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" MaxLength="10" onkeypress="JavaScript: Tecla('Alphanumeric');"
                        onblur="gDescripcion(this)" Width="63px"></asp:TextBox>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td width="8%">
                    &nbsp;
                </td>
                <td align="left">
                    <asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro" Width="85px"></asp:Label>
                    <asp:TextBox ID="txtNumAsiento" runat="server" CssClass="textbox" Height="17px" MaxLength="10"
                        onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)" Width="58px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                    &nbsp;
                    <asp:TextBox ID="txtPasajero" runat="server" CssClass="textbox" MaxLength="50" onkeypress="JavaScript: Tecla('Alphanumeric');"
                        onblur="gDescripcion(this)" Width="100px"></asp:TextBox>
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <!-- SPACE -->
                <td colspan="3">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 20%">
                                </td>
                                <td class="CenterGrid">
                                    <asp:Label ID="txtMensajeError" runat="server" BorderStyle="None" CssClass="mensaje"
                                        Width="150px"></asp:Label>
                                    <asp:Panel ID="pnlPanelDetalleDocumento" runat="server">
                                        <div id="divData" class="divSizeCustom">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                    <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                                    <cc2:PagingGridView ID="grvDetalleBoardingPagin" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" CssClass="grilla" GridLines="Vertical" Width="100%" AllowSorting="True"
                                                        OnPageIndexChanging="grvDetalleBoardingPagin_PageIndexChanging" OnRowCommand="grvDetalleBoardingPagin_RowCommand"
                                                        OnSorting="grvDetalleBoardingPagin_Sorting" OnRowDataBound="grvDetalleBoardingPagin_RowDataBound"
                                                        AllowPaging="True" VirtualItemCount="-1">
                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                        <PagerSettings Mode="NumericFirstLast" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Nro. Boarding" SortExpression="Cod_Numero_Bcbp">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="NumSecuencial" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                        CommandName="ShowDetBoarding" Text='<%# Eval("Cod_Numero_Bcbp") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Correlativo" HeaderText="Secuencial" SortExpression="Correlativo" />
                                                            <asp:BoundField DataField="Dsc_Compania" HeaderText="Compañía" SortExpression="Dsc_Compania" />
                                                            <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFechaVueloB" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" SortExpression="Num_Vuelo" />
                                                            <asp:BoundField DataField="Num_Asiento" HeaderText="Nro. Asiento" SortExpression="Num_Asiento" />
                                                            <asp:BoundField DataField="Nom_Pasajero" HeaderText="Pasajero" SortExpression="Nom_Pasajero" />
                                                            <asp:BoundField DataField="Dsc_Tipo_Ingreso" HeaderText="Tipo Ingreso" SortExpression="Dsc_Tipo_Ingreso" />
                                                            <asp:BoundField DataField="Usuario" HeaderText="Usuario Último Proceso" SortExpression="Usuario" />
                                                            <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Último Proceso" SortExpression="FechaModificacion" />
                                                            <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado Actual" SortExpression="Dsc_Campo" />
                                                            <asp:TemplateField HeaderText="Asociado" SortExpression="Flg_Tipo_Bcbp">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEstadoAsociacion" runat="server" Text='<%# ( /* Eval("Flg_Tipo_Bcbp")!=DBNull.Value && */ Int32.Parse(Eval("Flg_Tipo_Bcbp").ToString())==1) ? "Si" : "No"  %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </cc2:PagingGridView>
                                                    <cc2:PagingGridView ID="grvDetalleTicketPagin" runat="server" BackColor="White" BorderColor="#999999"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                        Width="100%" CssClass="grilla" OnPageIndexChanging="grvDetalleTicketPagin_PageIndexChanging"
                                                        OnRowCommand="grvDetalleTicketPagin_RowCommand" OnSorting="grvDetalleTicketPagin_Sorting"
                                                        OnRowDataBound="grvDetalleTicketPagin_RowDataBound">
                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                        CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Correlativo" HeaderText="Secuencial" SortExpression="Correlativo" />
                                                            <asp:BoundField DataField="Dsc_Tipo_Ticket" HeaderText="Tipo Ticket" SortExpression="Dsc_Tipo_Ticket" />
                                                            <asp:BoundField DataField="Dsc_Compania" HeaderText="Compa&#241;&#237;a" SortExpression="Dsc_Compania" />
                                                            <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo" />
                                                            <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Nro. Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                            <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Último Proceso" SortExpression="FechaModificacion" />
                                                            <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado Actual" SortExpression="Dsc_Campo" />
                                                            <asp:BoundField DataField="Tip_Estado_Actual" Visible="false" />
                                                            <asp:BoundField DataField="Fch_Vencimiento" HeaderText="Fecha Vencimiento" SortExpression="Fch_Vencimiento" />
                                                            <asp:BoundField DataField="Cod_Turno" HeaderText="Turno" SortExpression="Cod_Turno" />
                                                            <asp:BoundField DataField="Cta_Usuario" HeaderText="Usuario Último Proceso" SortExpression="Cta_Usuario" />
                                                            <asp:TemplateField HeaderText="Precio" SortExpression="Imp_Precio">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecio" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Cod_Moneda"), Eval("Imp_Precio")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Num_Rehabilitaciones" HeaderText="Nro. Rehabilitaciones"
                                                                SortExpression="Num_Rehabilitaciones" />
                                                            <asp:BoundField DataField="Tipo" HeaderText="Dato" />
                                                        </Columns>
                                                    </cc2:PagingGridView>
                                                    <br />
                                                    <br />
                                                    <cc2:PagingGridView ID="grvDetalleTicketConti" runat="server" BackColor="White" BorderColor="#999999"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                                        Width="100%" CssClass="grilla" OnPageIndexChanging="grvDetalleTicketConti_PageIndexChanging"
                                                        OnRowCommand="grvDetalleTicketConti_RowCommand" DataKeyNames="Cod_Numero_Ticket"
                                                        OnSorting="grvDetalleTicketConti_Sorting" OnRowDataBound="grvDetalleTicketConti_RowDataBound">
                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                        <AlternatingRowStyle CssClass="grillaFila" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                        CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Correlativo" HeaderText="Secuencial" SortExpression="Correlativo" />
                                                            <asp:BoundField DataField="Dsc_Tipo_Ticket" HeaderText="Tipo Ticket" SortExpression="Dsc_Tipo_Ticket" />
                                                            <asp:BoundField DataField="Dsc_Compania" HeaderText="Compa&#241;&#237;a" SortExpression="Dsc_Compania" />
                                                            <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo" />
                                                            <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Nro. Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                            <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha Último Proceso" SortExpression="FechaModificacion" />
                                                            <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado Actual" SortExpression="Dsc_Campo" />
                                                            <asp:BoundField DataField="Tip_Estado_Actual" Visible="false" />
                                                            <asp:BoundField DataField="Fch_Vencimiento" HeaderText="Fecha Vencimiento" SortExpression="Fch_Vencimiento" />
                                                            <asp:BoundField DataField="Cod_Turno" HeaderText="Turno" SortExpression="Cod_Turno" />
                                                            <asp:BoundField DataField="Cta_Usuario" HeaderText="Usuario Último Proceso" SortExpression="Cta_Usuario" />
                                                            <asp:TemplateField HeaderText="Precio" SortExpression="Imp_Precio">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPrecio" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Cod_Moneda"), Eval("Imp_Precio")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Num_Rehabilitaciones" HeaderText="Nro. Rehabilitaciones"
                                                                SortExpression="Num_Rehabilitaciones" />
                                                            <asp:BoundField DataField="Tipo" HeaderText="Dato" />
                                                        </Columns>
                                                    </cc2:PagingGridView>
                                                    <br />
                                                    <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                                    <input type="hidden" id="lblTotalRows" runat="server" value="0" />
                                                    <input type="hidden" id="lblMaxRegistros" runat="server" value="0" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                                DisplayAfter="500">
                                                <ProgressTemplate>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <!-- FOOTER -->
                <td colspan="3">
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    </br>
                </td>
            </tr>
        </table>
    </div>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    <uc5:ConsDetTicket ID="ConsDetTicket1" runat="server" />
    </form>
</body>
</html>
