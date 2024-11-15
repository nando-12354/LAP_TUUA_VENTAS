<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_TicketBoardingUsadosDiaMes.aspx.cs"
    Inherits="Rpt_TicketBoardingUsadosDiaMes" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Tickets o Boarding Pass Usados por Dia, Mes</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>

    <script language="javascript" type="text/javascript">

        function imgPrint_onclick() {
            //--------------------
            if (document.forms[0].chkbTicket.checked)
                if (document.forms[0].chkbBP.checked) {
                sTDocumento = '0';
            }
            else {
                sTDocumento = 'T';
            }
            else {
                if (document.forms[0].chkbBP.checked) {
                    sTDocumento = 'B';
                }
                else {
                    sTDocumento = 'N';
                }
            }

            if (document.forms[0].rbtnDesde.checked) {
                var sFechaDesde = document.forms[0].txtDesde.value;
                var sFechaHasta = document.forms[0].txtHasta.value;
                sMes = "";
                sAnnio = "";
            }
            else {
                if (document.forms[0].rbtnMes.checked == true) {
                    sFechaDesde = "";
                    sFechaHasta = "";
//                    var myString = document.getElementById("txtMesSeleccionado").value;
//                    var mySplitResult = myString.split("/");

                    sMes = document.getElementById("hfMes").value;
                    sAnnio = document.getElementById("hfAnnio").value; 
                }
            }


            if (sFechaDesde != "") {
                var wordsFechaDesde = sFechaDesde.split('/');
                sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
            }
            else { sFechaDesde = ""; }

            if (sFechaHasta != "") {
                var wordsFechaHasta = sFechaHasta.split('/');
                sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
            }
            else { sFechaHasta = ""; }


            var sIdCompania = document.forms[0].ddlAerolinea[document.forms[0].ddlAerolinea.selectedIndex].value;

            var sDesCompania = ""
            if (sIdCompania == "0") {
                sDesCompania = "Todos"
            }
            else {
                sDesCompania = document.forms[0].ddlAerolinea[document.forms[0].ddlAerolinea.selectedIndex].text;
            }
            //var sDestino = document.forms[0].txtDestino.value;
            var sDestino = document.forms[0].txtDestino.value;
            var sTipoTicket = document.forms[0].ddlTipoTicket[document.forms[0].ddlTipoTicket.selectedIndex].value;

            var sDesTipoTicket = ""
            if (sTipoTicket == "0") {
                sDesTipoTicket = "Todos"
            }
            else {
                sDesTipoTicket = document.forms[0].ddlTipoTicket[document.forms[0].ddlTipoTicket.selectedIndex].text;
            }

            var sNumVuelo = document.forms[0].txtNroVuelo.value;
            
            
            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;
            
            var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;

            var ventimp = window.open("ReporteRPT/rptTicketBPUsadosDiaMes.aspx" + "?sFechaDesde=" + sFechaDesde 
                                            + "&sFechaHasta=" + sFechaHasta  
                                            + "&sMes=" + sMes
                                            + "&sAnnio=" + sAnnio
                                            + "&sTDocumento=" + sTDocumento
                                            + "&sIdCompania=" + sIdCompania
                                            + "&sDesCompania=" + sDesCompania
                                            + "&sDestino=" + sDestino
                                            + "&sTipoTicket=" + sTipoTicket
                                            + "&sDesTipoTicket=" + sDesTipoTicket
                                            + "&sNumVuelo=" + sNumVuelo
                                            + "&sFechaEstadistico=" + sFechaEstadistico
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
        
         function validarImprimir() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            
            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    
                    if (document.forms[0].chkbTicket.checked)
                        if (document.forms[0].chkbBP.checked) {
                            sTDocumento = '0';
                        }
                        else {
                            sTDocumento = 'T';
                        }
                    else {
                        if (document.forms[0].chkbBP.checked) {
                            sTDocumento = 'B';
                        }
                        else {
                            sTDocumento = 'N';
                        }
                    }

                    if (document.forms[0].rbtnDesde.checked) {
                        var sFechaDesde = document.forms[0].txtDesde.value;
                        var sFechaHasta = document.forms[0].txtHasta.value;
                        sMes = "";
                        sAnnio = "";
                    }
                    else {
                        if (document.forms[0].rbtnMes.checked == true) {
                            sFechaDesde = "";
                            sFechaHasta = "";
                            sMes = document.getElementById("hfMes").value;
                            sAnnio = document.getElementById("hfAnnio").value; 
                        }
                    }


                    if (sFechaDesde != "") {
                        var wordsFechaDesde = sFechaDesde.split('/');
                        sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
                    }
                    else { sFechaDesde = ""; }

                    if (sFechaHasta != "") {
                    var wordsFechaHasta = sFechaHasta.split('/');
                    sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
                    }
                    else { sFechaHasta = ""; }


                    var sIdCompania = document.forms[0].ddlAerolinea[document.forms[0].ddlAerolinea.selectedIndex].value;

                    var sDesCompania = ""
                    if (sIdCompania == "0") {
                        sDesCompania = "Todos"
                    }
                    else {
                        sDesCompania = document.forms[0].ddlAerolinea[document.forms[0].ddlAerolinea.selectedIndex].text;
                    }
            
                    var sDestino = document.forms[0].txtDestino.value;
                    var sTipoTicket = document.forms[0].ddlTipoTicket[document.forms[0].ddlTipoTicket.selectedIndex].value;

                    var sDesTipoTicket = ""
                    if (sTipoTicket == "0") {
                        sDesTipoTicket = "Todos"
                    }
                    else {
                        sDesTipoTicket = document.forms[0].ddlTipoTicket[document.forms[0].ddlTipoTicket.selectedIndex].text;
                    }

                    var sNumVuelo = document.forms[0].txtNroVuelo.value;

                    var w = 900 + 32;
                    var h = 500 + 96;
                    var wleft = (screen.width - w) / 2;
                    var wtop = (screen.height - h) / 2;
                    
                    var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;

                    var ventimp = window.open("ReporteRPT/rptTicketBPUsadosDiaMes.aspx" + "?sFechaDesde=" + sFechaDesde 
                                            + "&sFechaHasta=" + sFechaHasta  
                                            + "&sMes=" + sMes
                                            + "&sAnnio=" + sAnnio
                                            + "&sTDocumento=" + sTDocumento
                                            + "&sIdCompania=" + sIdCompania
                                            + "&sDesCompania=" + sDesCompania
                                            + "&sDestino=" + sDestino
                                            + "&sTipoTicket=" + sTipoTicket
                                            + "&sDesTipoTicket=" + sDesTipoTicket
                                            + "&sNumVuelo=" + sNumVuelo
                                            + "&sFechaEstadistico=" + sFechaEstadistico
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
                     ventimp.focus(); 
                    
                }
                else {
                    alert("La impresión solo permite " + maxRegistros + " registros");
                    
                }
            }
            else {
                alert("No existen registros para imprimir \nSeleccione otros filtros");
                
            }
        }
        
         function validarCampos() {
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();

            HabilitaOpciones();

            if (document.forms[0].rbtnDesde.checked) {
                if (!(isValidoRangoFecha(document.getElementById('txtDesde').value, '', document.getElementById('txtHasta').value, ''))) {
                    document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                    return false;
                }
            } else {
                if (document.forms[0].txtMes.value == "") {
                    document.getElementById('lblMensajeError').innerHTML = "Ingrese el Mes";
                    return false;
                }
            }
            var ckTicket = form1.chkbTicket.checked;
            var ckBoarding = form1.chkbBP.checked;

            if (!(ckTicket || ckBoarding)) {
                document.getElementById('lblErrorMsg').innerHTML = "Seleccione al menos un Documento";
                return false;
            }
            return true;
        }

        function HabilitaOpciones() {

            if (document.getElementById("rbtnDesde").checked == true) {
                document.getElementById("txtDesde").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtHasta").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtMes").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtMes").value = '';
                document.getElementById("lblMensajeError").innerHTML = '';
                document.getElementById("imbCalDesde").disabled = false;
                document.getElementById("imbCalHasta").disabled = false;
                document.getElementById("imgbtnCalendarMes").disabled = true;
            }

            if (document.getElementById("rbtnMes").checked == true) {
                document.getElementById("txtDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtMes").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtDesde").value = '';
                document.getElementById("txtHasta").value = '';
                document.getElementById("lblMensajeError").innerHTML = '';
                document.getElementById("imbCalDesde").disabled = true;
                document.getElementById("imbCalHasta").disabled = true;
                document.getElementById("imgbtnCalendarMes").disabled = false;
            }
        }

        function onCalendarShown() {
            var cal = $find("calendar1");     //Setting the default mode to month   
            cal._switchMode("months", true);      //Iterate every month Item and attach click event to it 
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function onCalendarHidden() {
            var cal = $find("calendar1");     //Iterate every month Item and remove click event from it   
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
        function ActualizaMes() {
            var myString = document.getElementById("txtMes").value;
            var mySplitResult = myString.split("/");

            document.getElementById("hfMes").value = mySplitResult[0];
            document.getElementById("hfAnnio").value = mySplitResult[1];

            var index = mySplitResult[0] * 1;
            if (index > 0) {
                var SelValue = document.getElementById("ddlMes").options[index - 1].outerText;
                document.getElementById("txtMes").value = SelValue + " - " + mySplitResult[1];
            }
        }
        function cleanGrilla() {
            if (document.getElementById("crvrptTicketBPUsadosDiaMes") != null) {
                document.getElementById("crvrptTicketBPUsadosDiaMes").style.display = "none";
            }
        }     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
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
                                        <td style="width: 20px;" rowspan="6">
                                        </td>
                                        <td colspan="16" style="height: 20px;">
                                            <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 5%;">
                                            <asp:RadioButton ID="rbtnDesde" runat="server" CssClass="TextoFiltro" GroupName="FechaReporte"
                                                onClick="HabilitaOpciones();" Checked="True" />
                                        </td>
                                        <td rowspan="2" style="width: 102px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalDesde" TargetControlID="txtDesde">
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
                                            <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td style="width: 4%;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2" style="width: 72px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalHasta" TargetControlID="txtHasta">
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
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <td style="width: 11%;">
                                            <asp:CheckBox ID="chkbTicket" runat="server" CssClass="TextoEtiqueta" Checked="True" />
                                            <!--onclick="Javascript: SetCheckBoxTicket(this,'chkbBP','ddlTipoTicket');"-->
                                            <asp:CheckBox ID="chkbBP" runat="server" CssClass="TextoEtiqueta" />
                                            <!--onclick="Javascript: SetCheckBoxBoarding(this,'chkbTicket','ddlTipoTicket');"-->
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="ddlAerolinea" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 5%;">
                                            <asp:Label ID="lblNumeroVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 8%;">
                                            <asp:TextBox ID="txtNroVuelo" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                onkeypress="JavaScript: Tecla('NumeroyLetra');" Width="90%"></asp:TextBox>
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
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbtnMes" runat="server" CssClass="TextoFiltro" GroupName="FechaReporte"
                                                onClick="HabilitaOpciones();" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtMes" runat="server" BackColor="#E4E2DC" CssClass="textbox" onchange="ActualizaMes()"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="102px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtMes_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                            Enabled="True" Format="MM/yyyy" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown"
                                                            PopupButtonID="imgbtnCalendarMes" TargetControlID="txtMes">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label1" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendarMes" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td colspan="4">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td colspan="4" align="right">
                                         <asp:LinkButton ID="lbExportar" runat="server" 
                                 onclick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                            <br />
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                              <%--  <a href="" id="lnkExportar" runat="server" onclick="javascript:exportarExcel_onclick();"
                                                            style="cursor: hand;"><b>
                                                                <asp:Label ID="lblExportar" runat="server">[Exportar a Excel]</asp:Label>
                                                            </b></a>--%>
                                                            
                                                    &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="validarImprimir();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar" runat="server" CausesValidation="False" OnClientClick="return validarCampos();"
                                                        CssClass="Boton" Style="cursor: hand;" Text="Consultar" OnClick="btnConsultar_Click" />
                                                    &nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
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
                                            <asp:Label ID="lblDestino" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2" colspan="2">
                                            <asp:TextBox ID="txtDestino" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"></asp:TextBox>
                                        </td>
                                        <td colspan="4">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td colspan="4">
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="2" align="right">
                                            &nbsp;&nbsp;&nbsp;
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
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <asp:Label ID="lblFechaEstadistico" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <cc2:PagingGridView ID="grvTicketBPUsadosDiaMes" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" GroupingDepth="4"
                                                AllowSorting="True"   OnPageIndexChanging="grvTicketBPUsadosDiaMes_PageIndexChanging"
                                                OnRowCreated="grvTicketBPUsadosDiaMes_RowCreated" OnRowDataBound="grvTicketBPUsadosDiaMes_RowDataBound">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Uso" DataField="Fecha_Uso" />
                                                    <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" />
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Dsc_Tipo_Ticket" />
                                                    <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="BP" DataField="BP" />
                                                    <asp:BoundField HeaderText="Ticket" DataField="Ticket" />
                                                    <asp:BoundField HeaderText="Total" DataField="Total" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <asp:GridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False"
                                                    GroupingDepth="1" HorizontalAlign="Center" CssClass="grillaShort" OnRowCreated="grvDataResumen_RowCreated"
                                                    OnRowDataBound="grvDataResumen_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="Tip_Documento" />
                                                        <asp:BoundField HeaderText="Tipo Venta" DataField="Tip_Venta" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>
                                            
                                                <input type="hidden" id="lblTotalRows" runat="server" value="0" />
                                            <input type="hidden" id="lblMaxRegistros" runat="server" value="0" />  
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
    <asp:DropDownList ID="ddlMes" runat="server" Style="display: none">
        <asp:ListItem Value="1">Enero</asp:ListItem>
        <asp:ListItem Value="2">Febrero</asp:ListItem>
        <asp:ListItem Value="3">Marzo</asp:ListItem>
        <asp:ListItem Value="4">Abril</asp:ListItem>
        <asp:ListItem Value="5">Mayo</asp:ListItem>
        <asp:ListItem Value="6">Junio</asp:ListItem>
        <asp:ListItem Value="7">Julio</asp:ListItem>
        <asp:ListItem Value="8">Agosto</asp:ListItem>
        <asp:ListItem Value="9">Setiembre</asp:ListItem>
        <asp:ListItem Value="10">Octubre</asp:ListItem>
        <asp:ListItem Value="11">Noviembre</asp:ListItem>
        <asp:ListItem Value="12">Diciembre</asp:ListItem>
    </asp:DropDownList>
    <asp:HiddenField ID="hfMes" runat="server" />
    <asp:HiddenField ID="hfAnnio" runat="server" />
    </form>
</body>
</html>
