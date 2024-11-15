<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_AnulacionBCBP.aspx.cs"
    Inherits="Ope_AnulacionBCBP" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Src="UserControl/ConsBCBP.ascx" TagName="ConsBCBP" TagPrefix="uc5" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Anulación de BCBP</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->

    <script language="javascript" type="text/javascript">


        var NumTotal = 0;

        function SetCheckBoxHeaderGrilla(control) {
            try {
                var frm = document.forms[0];
                if (control.checked) {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    //var numSelTotal = NumTotal;
                    var j = 1;
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                            if (!frm.elements[i].checked) {
                                j = 0;
                                break;
                            }
                        }
                    }
                    if (j == 1) {
                        frm.chkAll.checked = true;
                    }
                } else {
                    frm.chkAll.checked = false;

                }

                if (control.checked) {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    // var numSelTotal = NumTotal;
                    numSelTotal += 1;
                    frm.hdNumSelTotal.value = numSelTotal;
                }
                else {
                    //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                    var numSelTotal = parseInt(document.getElementById("hdNumSelTotal").value);
                    //var numSelTotal = NumTotal;
                    numSelTotal -= 1;
                    frm.hdNumSelTotal.value = numSelTotal;
                }
                NumTotal = parseInt(document.getElementById("hdNumSelTotal").value); //parseInt(frm.hdNumSelTotal.value);
                document.getElementById('lblTxtSeleccionados').innerHTML = document.getElementById("hdNumSelTotal").value; //frm.hdNumSelTotal.value;

            } catch (Exception) {
            }

        }


        function CheckBoxHeaderGrilla() {
            try {
                var frm = document.forms[0];

                if (frm.chkAll != null) {//Esto para cuando oculte el pnlPrincipal
                    var j = 1;
                    var count = 0;
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                            count = count + 1;
                            if (!frm.elements[i].checked) {
                                j = 0;
                                break;
                            }
                        }
                    }
                    if (j == 1 && count > 0) {
                        frm.chkAll.checked = true;
                    }
                    else {
                        frm.chkAll.checked = false;
                    }
                    if (count > 0)
                        frm.chkAll.disabled = false;
                    else
                        frm.chkAll.disabled = true;
                }

                //frm.hdNumSelTotal.value = "0";

            } catch (Exception) {
            }
        }

        function SetNumTotal() {
            try {
                var frm = document.forms[0];
                frm.hdNumSelTotal.value = "0";
            }
            catch (Exception) {
            }
        }

        function SetCheck(control) {
            try {

                var frm = document.forms[0];
                var checkbox;
                var observacion;
                //var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                var numSelTotal = NumTotal;
                if (control.checked) {
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && !frm.elements[i].checked) {//
                            checkbox = frm.elements[i];
                            numSelTotal += 1;
                            frm.hdNumSelTotal.value = numSelTotal;
                            checkbox.checked = true;
                        }
                    }
                }
                else {
                    for (i = 0; i < frm.elements.length; i++) {
                        if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].checked) {
                            checkbox = frm.elements[i];
                            numSelTotal -= 1;
                            checkbox.checked = false;
                        }
                    }
                }
                frm.hdNumSelTotal.value = numSelTotal;
                NumTotal = numSelTotal;
                document.getElementById('lblTxtSeleccionados').innerHTML = frm.hdNumSelTotal.value;
            } catch (Exception) {
            }

        }

        function controlaFechaDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtDesde.value = form1.txtDesde.value.substring(0, form1.txtDesde.value.length - 2);
            }
            else {

                if (form1.txtDesde.value.length == 2) {
                    form1.txtDesde.value = form1.txtDesde.value + '/';
                }
                if (form1.txtDesde.value.length == 5) {
                    form1.txtDesde.value = form1.txtDesde.value + '/';
                }
            }
        }



        function controlaFechaHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHasta.value = form1.txtHasta.value.substring(0, form1.txtHasta.value.length - 2);
            }
            else {

                if (form1.txtHasta.value.length == 2) {
                    form1.txtHasta.value = form1.txtHasta.value + '/';
                }
                if (form1.txtHasta.value.length == 5) {
                    form1.txtHasta.value = form1.txtHasta.value + '/';
                }
            }
        }


        function controlaHoraDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraDesde.value = form1.txtHoraDesde.value.substring(0, form1.txtHoraDesde.value.length - 2);
            }
            else {

                if (form1.txtHoraDesde.value.length == 2) {
                    form1.txtHoraDesde.value = form1.txtHoraDesde.value + ':';
                }
                if (form1.txtHoraDesde.value.length == 5) {
                    form1.txtHoraDesde.value = form1.txtHoraDesde.value + ':';
                }
            }
        }

        function controlaHoraHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                form1.txtHoraHasta.value = form1.txtHoraHasta.value.substring(0, form1.txtHoraHasta.value.length - 2);
            }
            else {

                if (form1.txtHoraHasta.value.length == 2) {
                    form1.txtHoraHasta.value = form1.txtHoraHasta.value + ':';
                }
                if (form1.txtHoraHasta.value.length == 5) {
                    form1.txtHoraHasta.value = form1.txtHoraHasta.value + ':';
                }
            }

        }

        var accionSave = false;

        function ValidarAnulacion() {
            accionSave = true;
        }

        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAnular').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }


        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAnular').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }

        function reinicioSeleccion() {
            NumTotal = 0;
        }
        
        
    </script>

    <style type="text/css">
        .style3
        {
        }
        .style4
        {
            width: 502px;
        }
        .style5
        {
            width: 5px;
        }
        .style9
        {
            width: 1px;
        }
        .style10
        {
            height: 13px;
            width: 168px;
        }
        .style11
        {
            height: 13px;
            width: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="right" style="text-align: left">
                &nbsp;
            </td>
            <td align="right">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                    <asp:Button ID="btnAnular" runat="server" CssClass="Boton" CausesValidation="False"
                    OnClick="btnAnular_Click" OnClientClick="ValidarAnulacion()" />
                <cc1:ConfirmButtonExtender ID="cbeAnular" runat="server" ConfirmText="" Enabled="True"
                    TargetControlID="btnAnular">
                    </cc1:ConfirmButtonExtender>
                    </ContentTemplate>
                    
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                
                
            </td>
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
                                <table style="width: 100%; left: 0px; top: 0px;" class="alineaderecha">
                                    <tr>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <%--<td style="text-align: left;" class="style1">
                                            &nbsp;
                                        </td>--%>
                                        <td style="text-align: left;">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                            <table style="width: 99%; left: 4px; top: 0px;" class="alineaderecha">
                                                <tr>
                                                    <td colspan="4" class="style3">
                                                        <asp:Label ID="lblFechaLectura" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" class="style4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="1" class="style3">
                                                        <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;" class="style10">
                                                        <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalDesde" TargetControlID="txtDesde">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="text-align: left;" class="style11">
                                                        <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                            Width="22px" />
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                     <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                        onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                                        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                                    </cc1:MaskedEditExtender>
                                                        <%--<asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>--%>
                                                    </td>
                                                   <%-- <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>--%>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:DropDownList ID="ddlAerolinea" runat="server" CssClass="combo" Width="177px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblNroVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                            onblur="abcSinEspacio(this)" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                            Width="88px"></asp:TextBox>
                                                    </td>
                                                   <%-- <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" class="style4">
                                                        &nbsp;
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td class="style3" colspan="1">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style10" style="text-align: left;">
                                                        <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                    <td class="style11" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style4" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style3" colspan="1">
                                                        </span>
                                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;" class="style10">
                                                        <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalHasta" TargetControlID="txtHasta">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="text-align: left;" class="style11">
                                                        &nbsp;<asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                            Width="22px" />
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                         <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                        onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                                        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                                    </cc1:MaskedEditExtender>
                                                    </td>
                                                    <%--<td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>--%>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblNroAsiento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:TextBox ID="txtNroAsiento" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                            onkeypress="JavaScript: Tecla('Alphanumeric');" Width="88px"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 331px; text-align: right; height: 13px;">
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:ImageButton ID="btnBuscar" runat="server" CausesValidation="False" Height="20px"
                                                                    ImageUrl="~/Imagenes/Search.bmp" OnClick="btnBuscar_Click" Width="20px" />
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="text-align: left;" class="style4">
                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red" Width="120px"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style3" colspan="1">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="text-align: left;" class="style10">
                                                        <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;" class="style11">
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" class="style4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style3" colspan="1">
                                                    &nbsp;
                                                    </td>
                                                    <td class="style10" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style11" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" class="style4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                <td colspan="2">
                                                <asp:Label ID="lblMotivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                &nbsp;
                                                </td>
                                                <td>
                                                &nbsp;
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Width="248px"></asp:TextBox>
                                                </td>
                                                </tr>
                                            </table>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="width: 200px; text-align: left; height: 13px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td style="width: 331px; text-align: left; height: 13px;" colspan="4">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="CenterGrid" colspan="3" align="left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table class="alineaderecha" style="width: 100%; left: 0px; top: 0px;">
                                                        <tr>
                                                            <td align="left" class="CenterGrid">
                                                                <cc2:PagingGridView ID="gwvAnularBCBP" runat="server" AutoGenerateColumns="False"
                                                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                                    CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                                      OnPageIndexChanging="gwvAnularBCBP_PageIndexChanging"
                                                                    GroupingDepth="0" OnSorting="gwvAnularBCBP_Sorting" DataKeyNames="Num_Secuencial_Bcbp" RowStyle-HorizontalAlign="Center"
                                                                    RowStyle-VerticalAlign="Middle"  
                                                                    OnRowDataBound="gwvAnularBCBP_RowDataBound">
                                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                    <PagerSettings Mode="NumericFirstLast" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Numero" ItemStyle-Width="8%">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="TipoBP">
                                                                            <ItemTemplate>
                                                                            <asp:Label ID="lblTipoPasajero" runat="server" Text='<%# Eval("Tipo_Pasajero") %>'> </asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="lblTipoVuelo" runat="server" Text='<%#  Eval("Tipo_Vuelo") %>'></asp:Label>
                                                                            <br />
                                                                            <asp:Label ID="lblTipoTrasbordo" runat="server" Text='<%# Eval("Tipo_Trasbordo") %>'></asp:Label>
                                                                           <%--     <%# Eval("Tipo_Pasajero") + " " + Eval("Tipo_Vuelo") + " " + Eval("Tipo_Trasbordo")%>--%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="8%" />
                                                                        </asp:TemplateField>
                                                                        <%--<asp:TemplateField HeaderText="Descripcion BCBP" ItemStyle-HorizontalAlign="Left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblDscCompania" runat="server" Text='<%# rowDesc_Cia.Value + " " + Eval("Dsc_Compania") %>'> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblDscNumVuelo" runat="server" Text='<%# rowDesc_NumVuelo.Value + " " + Eval("Num_Vuelo") %>'></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblDscFechaVuelo" runat="server" Text='<%# rowDesc_FechaVuelo.Value + " " + Convert.ToString(Eval("Fch_Vuelo")) %>'></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblDscAsiento" runat="server" Text='<%# rowDesc_Asiento.Value + " " + Eval("Num_Asiento") %>'></asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblDscPasajero" runat="server" Text='<%# rowDesc_Pasajero.Value + " " + Eval("Nom_Pasajero") %>'></asp:Label>
                                                                                <asp:HiddenField ID="lblCodCompania" Value='<%# Eval("Cod_Compania") %>' runat="server" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField HeaderText="Compañía">
                                                                        <ItemTemplate>
                                                                         <asp:Label ID="lblDscCompania" runat="server" Text='<%# Eval("Dsc_Compania") %>'> </asp:Label>
                                                                         <asp:HiddenField ID="lblCodCompania" Value='<%# Eval("Cod_Compania") %>' runat="server" />
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Fecha Vuelo">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblDscFechaVuelo" runat="server" Text='<%#  Convert.ToString(Eval("Fch_Vuelo")) %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Nro. Vuelo">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblDscNumVuelo" runat="server" Text='<%# Eval("Num_Vuelo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Asiento">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblDscAsiento" runat="server" Text='<%# Eval("Num_Asiento") %>'></asp:Label>
                                                                         
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Pasajero">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblDscPasajero" runat="server" Text='<%# Eval("Nom_Pasajero") %>'></asp:Label>
                                                                         
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Estado Actual">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblTipEstado" runat="server" Text='<%#  Eval("Tip_Estado") %>'></asp:Label>
                                                                       
                                                                        </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                       
                                                                       <%-- <asp:TemplateField HeaderText="Motivo">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtMotivo" runat="server" CssClass="textbox" Height="16px" CommandArgument="Motivo"
                                                                                    MaxLength="255" Width="358px"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>
                                                                        <asp:TemplateField ItemStyle-Width="18%" SortExpression="Check">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkSeleccionar" runat="server" onclick="Javascript:SetCheckBoxHeaderGrilla(this); " />
                                                                            </ItemTemplate>
                                                                            <HeaderTemplate>
                                                                                <input disabled="true" name="chkAll" onclick="JavaScript:SetCheck(this);" type="checkbox" />
                                                                                &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="Check" CommandName="Sort"
                                                                                    Text="Seleccionar"></asp:LinkButton>
                                                                            </HeaderTemplate>
                                                                            <ItemStyle Width="18%" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="Num_Secuencial_Bcbp" Visible="True" ItemStyle-Width="0%" />
                                                                    </Columns>
                                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                                </cc2:PagingGridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="formularioTitulo" style="width: 331px; text-align: left; height: 13px;">
                                                                <asp:Label ID="lblTotalIngresados" runat="server" />
                                                                &nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblTxtIngresados" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="formularioTitulo" style="width: 331px; text-align: left; height: 13px;">
                                                                <asp:Label ID="lblTotalSeleccionados" runat="server" />
                                                                &nbsp;<asp:Label ID="lblTxtSeleccionados" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style9" style="text-align: left;">
                                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAnular" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="SpacingGrid" colspan="1" style="height: 115px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;
                            </td>
                            <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                            </td>
                        </tr>
                    </table>
                </div>
                <uc2:PiePagina ID="PiePagina3" runat="server" />
            </td>
        </tr>
    </table>
    <%--<input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
    <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />--%>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <uc3:OKMessageBox ID="omb" runat="server" />
            <uc5:ConsBCBP ID="ConsBCBP1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="rowDesc_Cia" runat="server" />
    <asp:HiddenField ID="rowDesc_NumVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_FechaVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_Asiento" runat="server" />
    <asp:HiddenField ID="rowDesc_Pasajero" runat="server" />
    <asp:HiddenField ID="hdNumSelTotal" runat="server" />
    <asp:HiddenField ID="hdNumSelConObs" runat="server" />
    </form>
</body>
</html>
