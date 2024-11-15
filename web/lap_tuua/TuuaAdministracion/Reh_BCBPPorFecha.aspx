<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reh_BCBPPorFecha.aspx.cs"
    Inherits="Reh_BCBPPorFecha" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/ConsRepresentante.ascx" TagName="ConsRepresentante"
    TagPrefix="uc3" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Rehabilitacion de BCBP por Fecha</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 92px;
        }
        .style4
        {
            width: 112px;
        }
        .style6
        {
            width: 44px;
        }
        .style7
        {
            width: 80px;
        }
        .style8
        {
            width: 62px;
        }
        .style9
        {
        }
        .style10
        {
        }
        .style11
        {
            width: 47px;
        }
        .style12
        {
            width: 93px;
        }
        .style13
        {
        }
        .style14
        {
            width: 109px;
        }
        .style15
        {
            width: 76px;
        }
        </style>
</head>

<script language="javascript" type="text/javascript">

    function mostrarVoucherBtn() {

        if (document.getElementById('pnlBoucher') != null) {
            document.getElementById('pnlBoucher').style.display = "block";
        }
    }

    function openPopup(popurl) {

        winpops = window.open(popurl, "", "width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
        /*
        if(window.showModalDialog){
        winpops = window.showModalDialog(popurl,"","dialogWidth:800px;dialogHeight:200px; status: No;toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no")
        }
        else{
        winpops = window.open(popurl,"","width:800,height:200, status: No,toolbar=no, left=45, top=15, scrollbars=no, menubar=no,resizable=no,directories=no,location=no,modal=yes")
        }
        */
    }
</script>

<script type="text/javascript">
    /* We need to set the ReadOnly property here, since in VS 2.0+, setting it in the markup does not persist client-side changes.*/
    function windowOnLoad() {
        document.getElementById('lblTxtSeleccionados').readOnly = true;
        document.getElementById('lblTxtIngresados').readOnly = true;
    }

    function controlaHoraDesde() {

        if (window.event.keyCode == 8 || window.event.keyCode == 46) {
            //form1.txtHoraDesde.value = form1.txtHoraDesde.value.substring(0, form1.txtHoraDesde.value.length - 2);
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
            // form1.txtHoraHasta.value = form1.txtHoraHasta.value.substring(0, form1.txtHoraHasta.value.length - 2);
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

    window.onload = windowOnLoad;
</script>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
        function SetCheckBoxHeaderGrilla(control) {
            var frm = document.forms[0];
            if (control.checked) {
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

            var observacion = control.parentNode.previousSibling.previousSibling.innerHTML;
            if (control.checked) {
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal += 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs += 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            else {
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal -= 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs -= 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";
        }

        function SetCheckBoxHeaderGrillaBloq(control) {
            var frm = document.forms[0];
            if (control.checked) {
                var j = 1;
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].name.indexOf('chkBloquear') >= 0 && frm.elements[i].type == "checkbox") {
                        if (!frm.elements[i].checked) {
                            j = 0;
                            break;
                        }
                    }
                }
                if (j == 1) {
                    frm.chkBloq.checked = true;
                }
            } else {
                frm.chkBloq.checked = false;
            }
        }

        function CheckBoxHeaderGrilla() {
            var frm = document.forms[0];
            if (frm.chkAll != null) {//Esto para cuando oculte el pnlPrincipal
                var j = 1;
                var countSel = 0;
                var k = 1;
                var countBloq = 0;

                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].name.indexOf('chkSeleccionar') >= 0 && frm.elements[i].type == "checkbox") {
                        countSel = countSel + 1;
                        if (!frm.elements[i].checked) {
                            j = 0;
                            break;
                        }
                    }
                }
                for (i = 0; i < frm.elements.length; i++) {
                    if (frm.elements[i].name.indexOf('chkBloquear') >= 0 && frm.elements[i].type == "checkbox") {
                        countBloq = countBloq + 1;
                        if (!frm.elements[i].checked) {
                            k = 0;
                            break;
                        }
                    }
                }
                if (j == 1 && countSel > 0) { frm.chkAll.checked = true; }
                else { frm.chkAll.checked = false; }
                if (countSel > 0) { frm.chkAll.disabled = false; }
                else { frm.chkAll.disabled = true; }

                if (k == 1 && countBloq > 0) { frm.chkBloq.checked = true; }
                else { frm.chkBloq.checked = false; }
                if (countBloq > 0) { frm.chkBloq.disabled = false; }
                else { frm.chkBloq.disabled = true; }
            }
        }

        function SetCheck(control) {
            var frm = document.forms[0];
            var checkbox;
            var observacion;
            var numSelTotal = parseInt(frm.hdNumSelTotal.value);
            var numSelConObs = parseInt(frm.hdNumSelConObs.value);
            var chkBloq = "";
            j = 2;

            //var numColumnas = parseInt(frm.hdMostrarAsociacion.value);
            var observacion = "";

            if (control.checked) {
                for (i = 0; i < frm.elements.length; i++) {

                    //Valida que bloqueados no se hagan check
                    if (j < 10) chkBloq = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkBloquear";
                    else chkBloq = "gwvRehabilitarPorBCBP$ctl" + j + "$chkBloquear";
                    if (frm.elements[i].name == chkBloq) j++;
                    //Fin

                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkBloq && !frm.elements[i].checked) {//
                        checkbox = frm.elements[i];
                        observacion = checkbox.parentNode.previousSibling.previousSibling.innerHTML;

                        numSelTotal += 1;
                        frm.hdNumSelTotal.value = numSelTotal;
                        if (observacion != "-") {
                            numSelConObs += 1;
                        }
                        checkbox.checked = true;
                    }
                }
            }
            else {
                for (i = 0; i < frm.elements.length; i++) {
                    //Valida que bloqueados no se hagan check
                    if (j < 10) chkBloq = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkBloquear";
                    else chkBloq = "gwvRehabilitarPorBCBP$ctl" + j + "$chkBloquear";
                    if (frm.elements[i].name == chkBloq) j++;
                    //Fin
                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkBloq && frm.elements[i].checked) {
                        checkbox = frm.elements[i];
                        observacion = checkbox.parentNode.previousSibling.previousSibling.innerHTML;

                        numSelTotal -= 1;
                        if (observacion != "-") {
                            numSelConObs -= 1;
                        }
                        checkbox.checked = false;
                    }
                }
            }
            frm.hdNumSelTotal.value = numSelTotal;
            frm.hdNumSelConObs.value = numSelConObs;

            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);  
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);         

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";


        }

        function SetCheckBloq(control) {
            var frm = document.forms[0];
            var checkbox;
            var chkSeleccionar = "";
            j = 2;
            if (control.checked) {
                for (i = 0; i < frm.elements.length; i++) {

                    //Valida que seleccionados no se hagan check
                    if (j < 10) chkSeleccionar = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkSeleccionar";
                    else chkSeleccionar = "gwvRehabilitarPorBCBP$ctl" + j + "$chkSeleccionar";
                    if (frm.elements[i].name == chkSeleccionar) j++;
                    //Fin

                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkSeleccionar && !frm.elements[i].checked) {//
                        checkbox = frm.elements[i];
                        checkbox.checked = true;
                    }
                }
            }
            else {
                for (i = 0; i < frm.elements.length; i++) {
                    //Valida que seleccionados no se hagan check
                    if (j < 10) chkSeleccionar = "gwvRehabilitarPorBCBP$ctl0" + j + "$chkSeleccionar";
                    else chkSeleccionar = "gwvRehabilitarPorBCBP$ctl" + j + "$chkSeleccionar";
                    if (frm.elements[i].name == chkSeleccionar) j++;
                    //Fin
                    if (frm.elements[i].type == "checkbox" && frm.elements[i].name != 'chkAll' && frm.elements[i].name != 'chkBloq' && frm.elements[i].name != chkSeleccionar && frm.elements[i].checked) {
                        checkbox = frm.elements[i];
                        checkbox.checked = false;
                    }
                }
            }
        }

        function onDeleteClientClick(control) {
            var frm = document.forms[0];
            var observacion = "";

            if (control.parentNode.previousSibling.children[0].checked) {
                observacion = checkbox.parentNode.previousSibling.previousSibling.innerHTML;
                var numSelTotal = parseInt(frm.hdNumSelTotal.value);
                numSelTotal -= 1;
                frm.hdNumSelTotal.value = numSelTotal;
                if (observacion != "-") {
                    var numSelConObs = parseInt(frm.hdNumSelConObs.value);
                    numSelConObs -= 1;
                    frm.hdNumSelConObs.value = numSelConObs;
                }
            }
            else {
                //Total = Total - 1
            }
            //alert("NumSelTotal:" + frm.hdNumSelTotal.value);
            //alert("NumSelConObs:" + frm.hdNumSelConObs.value);        

            var normales = parseInt(frm.hdNumSelTotal.value) - parseInt(frm.hdNumSelConObs.value);
            frm.lblTxtSeleccionados.value = frm.hdNumSelTotal.value + " (" + frm.hdNumSelConObs.value + " Observaciones / " + normales + " Normales)";

            frm.lblTxtIngresados.value = parseInt(frm.lblTxtIngresados.value) - 1;
        }
    </script>

    <script type="text/javascript" language="javascript">
        //Script para validar campos y condiciones antes del submit.
        function btn_Rehabilitar_clientClick() {
            document.getElementById('lblErrDesde').innerHTML = "";
            document.getElementById('lblErrHasta').innerHTML = "";
            document.getElementById('lblErrorMsg').innerHTML = "";
            if (document.forms[0].hdNumSelTotal.value == 0) {
                document.getElementById('spnRehabilitar').innerHTML = "Debe de seleccionar al menos un boarding para rehabilitar.";
                return false;
            }
            else {
                document.getElementById('spnRehabilitar').innerHTML = "";
            }
            var ret = confirm("¿Desea Continuar con la Rehabilitacion?");
            return ret;
        }

        function btnBuscar_ClientClick() {
            document.getElementById('spnRehabilitar').innerHTML = "";
            var ret = true;

            if (document.forms[0].cboCompanias.value == "Seleccionar") {
                document.getElementById('lblErrComp').innerHTML = "*";
                ret = false;

                document.getElementById('lblErrorMsg').innerHTML = "Se debe seleccionar una Compañia";
            }
            else {
                ret = true;
                if (document.forms[0].txtDesde.value.length < 10) {
                    document.getElementById('lblErrDesde').innerHTML = "*";
                    ret = false;
                }
                else {
                    document.getElementById('lblErrDesde').innerHTML = "";
                }

                if (document.forms[0].txtHasta.value.length < 10) {
                    document.getElementById('lblErrHasta').innerHTML = "*";
                    ret = false;
                }
                else {
                    document.getElementById('lblErrHasta').innerHTML = "";
                }
                if (!ret) {
                    document.getElementById('lblErrorMsg').innerHTML = "Se debe completar el campo Fecha";
                }
                else {
                    var arrDate = document.forms[0].txtDesde.value.split("/");
                    var dateDesde = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);
                    arrDate = document.forms[0].txtHasta.value.split("/");
                    var dateHasta = new Date(arrDate[2], arrDate[1] - 1, arrDate[0]);

                    var difference = dateHasta - dateDesde;
                    if (difference < 0) {
                        document.getElementById('lblErrDesde').innerHTML = "*";
                        document.getElementById('lblErrHasta').innerHTML = "*";
                        document.getElementById('lblErrorMsg').innerHTML = "Rango de Fecha invalido";
                        ret = false;
                    }
                }
            }
            return ret;
        }    
    </script>

    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
        </table>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla"
                                id="tableRehabilitar" runat="server">
                                <tr class="formularioTitulo">
                                    <td align="left">
                                        <asp:Button ID="btnRehabilitar" runat="server" CssClass="Boton" OnClientClick="Javascript:return btn_Rehabilitar_clientClick();"
                                            OnClick="btnRehabilitar_Click" />
                                        &nbsp;<asp:Label ID="lblDescripcionLimite" runat="server" Text="" CssClass="msgMensaje" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr class="EspacioLinea" color="#0099cc" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlPrincipal" runat="server" Visible="true">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td>
                                <div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td style="width: 100%">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td style="width: 0%">
                                                        </td>
                                                        <td style="width: 100%">
                                                            <!-- inicio -->
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr class="formularioTitulo">
                                                                    <td width="60px" align="left">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td height="30px" width="78px">
                                                                        
                                                                        <asp:Label ID="lblMotivo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        
                                                                        <asp:DropDownList ID="cboMotivo" runat="server" CssClass="combo" Width="340px" AppendDataBoundItems="True">
                                                                        <asp:ListItem Value="">Seleccionar</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td class="style8">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style15" height="30px">
                                                                        
                                                                        <asp:Label ID="lblCia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td class="style10" colspan="5">
                                                                       <asp:DropDownList ID="cboCompanias" runat="server" AutoPostBack="True" 
                                                                            CssClass="combo" Width="340px">
                                                                        </asp:DropDownList>
                                                                        <span id="lblErrComp" style="color: Red;">
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        &nbsp;</td>
                                                                    <td class="style9" colspan="2" height="30px">
                                                                        <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                                                    </td>
                                                                    <td class="style11">
                                                                        &nbsp;</td>
                                                                    <td class="style12">
                                                                        &nbsp;</td>
                                                                    <td class="style13" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style15">
                                                                        
                                                                        <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td class="style14">
                                                                        <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="100px"></asp:TextBox>
                                                                        <span id="lblErrDesde" style="color: Red;"></span>
                                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                                            Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde">
                                                                        </cc1:CalendarExtender>
                                                                        <br />
                                                                        <asp:Label ID="Label1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label></td>
                                                                    <td class="style11" valign="top">
                                                                        <asp:ImageButton ID="imgbtnCalendar1" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                            Width="22px" /></td>
                                                                    <td class="style12">
                                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                                            ReadOnly="false" Width="56px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="False"
                                                                            ClearMaskOnLostFocus="False" CultureName="es-ES" Mask="99:99:99" MaskType="Time"
                                                                            PromptCharacter="_" TargetControlID="txtHoraDesde">
                                                                        </cc1:MaskedEditExtender>
                                                                        <br />
                                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label></td>
                                                                    <td class="style13" colspan="2">
                                                                        <a id="lnkRepresentante" runat="server" href="" onserverclick="lnkRepresentante_Click">
                                                                            <b>
                                                                                <asp:Label ID="lblConsRepresentante" runat="server" />
                                                                            </b></a>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style8">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td class="style15">
                                                                        
                                                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td class="style14">
                                                                        <asp:TextBox ID="txtHasta" runat="server" CssClass="textbox" Width="100px" onfocus="this.blur();"
                                                                            onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                                                                        <span id="lblErrHasta" style="color: Red;"></span>
                                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                                            TargetControlID="txtHasta" PopupButtonID="imgbtnCalendar2" Format="dd/MM/yyyy">
                                                                        </cc1:CalendarExtender>
                                                                        <br />
                                                                        <asp:Label ID="Label3" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label></td>
                                                                    <td class="style11" valign="top">
                                                                        <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                                            Width="22px" Height="22px" /></td>
                                                                    <td class="style12">
                                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onKeyDown="JavaScript:controlaHoraHasta();"
                                                                            onkeypress="JavaScript: Tecla('Time');" onBlur="JavaScript:CheckTime(this)" ReadOnly="false"
                                                                            Width="56px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="mee_txtHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                                            ClearMaskOnLostFocus="False" MaskType="Time" CultureName="es-ES" PromptCharacter="_"
                                                                            AutoComplete="False">
                                                                        </cc1:MaskedEditExtender>
                                                                        <br />
                                                                        <asp:Label ID="Label2" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label></td>
                                                                    <td class="style12">
                                                                         <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:ImageButton ID="btnBuscar" runat="server" OnClientClick="Javascript:return btnBuscar_ClientClick();"
                                                                                    OnClick="btnBuscar_Click" ImageUrl="~/Imagenes/Search.bmp" Height="20px" Width="20px" />
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                        <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel3" runat="server">
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
                                                                    <td>
                                                                        <asp:Label ID="lblErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:Label ID="spnRehabilitar" Style="color: Red;" runat="server" />    
                                                                    </td>

                                                                </tr>
                                                            </table>
                                                            
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="5%">
                                                                    </td>
                                                                    <td>
                                                                        <%--<div id="divGrid" style="height: 520px; overflow: auto;">--%>
                                                                        <div id="divGrid" style="min-height: 500px;">
                                                                            <asp:GridView ID="gwvRehabilitarPorBCBP" runat="server" RowStyle-VerticalAlign="Middle"
                                                                                RowStyle-HorizontalAlign="Center" AutoGenerateColumns="False" CssClass="grilla"
                                                                                Width="90%" OnRowDataBound="gwvRehabilitarPorBCBP_RowDataBound" OnRowCommand="gwvRehabilitarPorBCBP_RowCommand"
                                                                                AllowPaging="True" OnPageIndexChanging="gwvRehabilitarPorBCBP_PageIndexChanging"
                                                                                AllowSorting="True" OnSorting="gwvRehabilitarPorBCBP_Sorting">
                                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="Nro." ItemStyle-Width="5%">
                                                                                        <ItemTemplate>
                                                                                            <%# Container.DataItemIndex+1 %>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Descripcion BCBP" ItemStyle-Width="50%">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="descBCBP" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="ShowBCBP" Text='<%# rowDesc_Cia.Value + " " + Eval("Dsc_Compania") + "<br>" + rowDesc_NumVuelo.Value + " " + Eval("Num_Vuelo") + "&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;" + new StringBuilder().Append(rowDesc_FechaVuelo.Value).Append(" ").Append(LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null)).Append("&nbsp;&nbsp;&nbsp;-&nbsp;&nbsp;&nbsp;").Append(rowDesc_Asiento.Value).Append(" ").Append(Eval("Num_Asiento")).Append("<br>").Append(rowDesc_Pasajero.Value).Append(" ").Append(Eval("Nom_Pasajero")) %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Observacion" HeaderText="Observaciones" SortExpression="Observacion"
                                                                                        ItemStyle-Width="20%" />
                                                                                    <asp:TemplateField HeaderText="Bloquear">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkBloquear" runat="server" Checked='<%# ( /* Eval("Bloquear")!=DBNull.Value && */ Int32.Parse(Eval("Bloquear").ToString())==1) ? true : false  %>'
                                                                                                onclick="Javascript: SetCheckBoxHeaderGrillaBloq(this);" />
                                                                                        </ItemTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                        <HeaderTemplate>
                                                                                            Bloquear<br />
                                                                                            <input disabled="true" name="chkBloq" onclick="JavaScript:SetCheckBloq(this);" type="checkbox" />
                                                                                        </HeaderTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField SortExpression="Check">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" Checked='<%# ( /* Eval("Check")!=DBNull.Value && */ Int32.Parse(Eval("Check").ToString())==1) ? true : false  %>'
                                                                                                onclick="Javascript: SetCheckBoxHeaderGrilla(this); " />
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            Seleccionar<br />
                                                                                            <input type="checkbox" name="chkAll" disabled="true" onclick="JavaScript:SetCheck(this);" />
                                                                                            <%-- &nbsp;<asp:ImageButton ID="imageButton1" runat="server" CommandArgument="Check" 
                                                                                                CommandName="Sort" Height="20" ImageAlign="AbsMiddle" 
                                                                                                ImageUrl="~/Imagenes/icon_check.jpg" Width="20" />--%>
                                                                                        </HeaderTemplate>
                                                                                        <ItemStyle Width="5%" />
                                                                                    </asp:TemplateField>
                                                                                    <%--<asp:TemplateField SortExpression="Check" ItemStyle-Width="15%">
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkSeleccionar" runat="server" Checked='<%# ( /* Eval("Check")!=DBNull.Value && */ Int32.Parse(Eval("Check").ToString())==1) ? true : false  %>'
                                                                                                onclick="Javascript: SetCheckBoxHeaderGrilla(this); " />
                                                                                        </ItemTemplate>
                                                                                        <HeaderTemplate>
                                                                                            <input type="checkbox" name="chkAll" disabled="true" onclick="JavaScript:SetCheck(this);" />
                                                                                            &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" CommandName="Sort" CommandArgument="Check"
                                                                                                Text="Seleccionar"></asp:LinkButton>
                                                                                        </HeaderTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="10%">
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="Eliminar" ImageUrl="~/Imagenes/Delete.bmp" Width="20" Height="18" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    &nbsp;
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td width="5%" height="25px">
                                                                    </td>
                                                                    <td width="15%" class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalSeleccionados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtSeleccionados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0 (0 Observaciones / 0 Normales)</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr class="formularioTitulo">
                                                                    <td height="25px">
                                                                    </td>
                                                                    <td class="TextoEtiqueta">
                                                                        <asp:Label ID="lblTotalIngresados" runat="server" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="lblTxtIngresados" Width="500px" BackColor="#f5f5f5" CssClass="TextoEtiqueta"
                                                                            BorderColor="#f5f5f5" BorderWidth="0" BorderStyle="None" runat="server">0</asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr class="EspacioLinea" color="#0099cc" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <input type="hidden" id="hdNumSelTotal" runat="server" value="0" />
                                                            <input type="hidden" id="hdNumSelConObs" runat="server" value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlConformidad" runat="server" Visible="false">
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr class="formularioTitulo">
                            <td align="left">
                                <img alt="" src="Imagenes/flecha_back.png" onclick="Atras();" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr class="EspacioLinea" color="#0099cc" />
                            </td>
                        </tr>
                    </table>
                    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td width="35%" height="30px">
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                    <tr>
                                        <td>
                                            <br>
                                            <asp:Label ID="lblConformidad" runat="server" CssClass="titulosecundario" />
                                            <br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Resumen</span><br>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total de Boarding Pass Rehabilitados : </span>
                                            <asp:Label ID="lblTotalRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<span class="msgMensaje">Total Boarding Pass No Rehabilitados : </span>
                                            <asp:Label ID="lblTotalNoRehab" runat="server" CssClass="msgMensaje" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br>
                                            <span class="msgMensaje">Para conocer el detalle haga click en Imprimir</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton
                                                ID="btnReporte" runat="server" ImageUrl="~/Imagenes/print.jpg" class="BotonImprimir"
                                                OnClick="btnReporte_click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="35%" height="30px">
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td style="height: 11px" width="35%">
                </td>
                <td>
                </td>
                <td width="35%">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="height: 11px" width="35%">
                </td>
                <td>
                    <asp:Panel ID="pnlBoucher" runat="server" Style="display: none">
                        <span class="msgMensaje">Imprimir Voucher&nbsp;&nbsp;&nbsp;&nbsp; </span>&nbsp;<asp:ImageButton
                            ID="btnImprimirBoucher" runat="server" ImageUrl="~/Imagenes/print.jpg" class="BotonImprimir"
                            OnClick="btnImprimirBoucher_Click" />
                    </asp:Panel>
                </td>
                <td width="35%">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function Atras() {
            window.location.href = "./Reh_BCBPPorFecha.aspx";
        }
        function fnClickCerrarPopup(sender, e) {
            __doPostBack(sender, e);
        }
    </script>

    <uc3:ConsRepresentante ID="consRepre" runat="server" />
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    <asp:UpdateProgress AssociatedUpdatePanelID="UpdatePanel2" DisplayAfter="0" ID="updateProgress"
        runat="server">
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
    <asp:HiddenField ID="rowDesc_Cia" runat="server" />
    <asp:HiddenField ID="rowDesc_NumVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_FechaVuelo" runat="server" />
    <asp:HiddenField ID="rowDesc_Asiento" runat="server" />
    <asp:HiddenField ID="rowDesc_Pasajero" runat="server" />
    </form>
</body>
</html>
