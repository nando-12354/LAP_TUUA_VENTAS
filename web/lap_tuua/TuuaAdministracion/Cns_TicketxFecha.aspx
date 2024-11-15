<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cns_TicketxFecha.aspx.cs"
    Inherits="Modulo_Consultas_ConsultaTicketxFecha" ResponseEncoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Ticket o Boarding Pass por Fecha</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_ticketxfecha" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="JavaScript" type="text/javascript">
        //Impresion Reporte
        function imgPrint_onclick() {

            var sDesde = document.getElementById("txtDesde").value;
            var sHasta = document.getElementById("txtHasta").value;
            var idHoraDesde = document.getElementById("txtHoraDesde").value;
            var idHoraHasta = document.getElementById("txtHoraHasta").value;
            var idCompania = document.getElementById("ddlCompania").value;
            var idEstadoTicket = document.getElementById("ddlEstadoTicket").value;
            var idPersona = document.getElementById("ddlPersona").value;
            var idTipoTicket = document.getElementById("ddlTipoTicket").value;
            var idVuelo = document.getElementById("ddlVuelo").value;
            var idFlgCobro = "";
            if (document.getElementById("ddlFlgCobro").value != "-") {
                idFlgCobro = document.getElementById("ddlFlgCobro").value;
            }
            var idTipoDoc = document.getElementById("ddlTipoDocumento").value;
            
            var idEstadoTurno= document.getElementById("ddlEstadoTurno").value;
            var idCajero= document.getElementById("ddlCajero").value;

            var idChkMasiva = "0";
            if (document.getElementById("chkTicketVentaMasiva").checked == true) {
                idChkMasiva = "1";
            } else {
                idChkMasiva = "0";
            }
            
            var idMedioAnulacion = document.getElementById("ddlMedioAnulacion").value;

            //Descripciones
            var idDscT = (idTipoTicket != "0") ? document.getElementById("ddlTipoTicket").options[document.getElementById("ddlTipoTicket").selectedIndex].text : "";
            var idDscC = (idCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : "";
            var idDscE = (idEstadoTicket != "0") ? document.getElementById("ddlEstadoTicket").options[document.getElementById("ddlEstadoTicket").selectedIndex].text : "";
            var idDscP = (idPersona != "0") ? document.getElementById("ddlPersona").options[document.getElementById("ddlPersona").selectedIndex].text : "";
            var idDscK = (idFlgCobro != "") ? document.getElementById("ddlFlgCobro").options[document.getElementById("ddlFlgCobro").selectedIndex].text : "";
            var idDscV = (idVuelo != "0") ? document.getElementById("ddlVuelo").options[document.getElementById("ddlVuelo").selectedIndex].text : "";
            var idDscET = (idEstadoTurno != "0") ? document.getElementById("ddlEstadoTurno").options[document.getElementById("ddlEstadoTurno").selectedIndex].text : "";
            var idDscCA = (idCajero != "0") ? document.getElementById("ddlCajero").options[document.getElementById("ddlCajero").selectedIndex].text : "";
            
            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptTicketxFecha.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta + "&idHoraDesde=" +
                                        idHoraDesde + "&idHoraHasta=" + idHoraHasta + "&idCompania=" + idCompania + "&idEstadoTicket=" +
                                        idEstadoTicket + "&idPersona=" + idPersona + "&idTipoTicket=" + idTipoTicket + "&idFlgCobro=" +
                                        idFlgCobro + "&idTipoDoc=" + idTipoDoc + "&idChkMasiva=" + idChkMasiva + "&idEstadoTurno=" + idEstadoTurno
                                        + "&idCajero=" + idCajero
                                        + "&idMedioAnulacion=" + idMedioAnulacion
                                        + "&idDscT=" + idDscT
                                        + "&idDscC=" + idDscC
                                        + "&idDscE=" + idDscE
                                        + "&idDscP=" + idDscP
                                        + "&idDscK=" + idDscK
                                        + "&idDscV=" + idDscV
                                        + "&idVuelo=" + idVuelo
                                        + "&idDscET=" + idDscET
                                        + "&idDscCA=" + idDscCA
                                        , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.focus();
        }

        //Impresion Reporte
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
        

        function ControlarDropDownList(obj) {
            if (obj[obj.selectedIndex].value == "B") {
                //document.getElementById("ddlPersona").disabled = true;
                //document.getElementById("ddlTipoTicket").disabled = false;
                document.getElementById("ddlFlgCobro").disabled = true;
                document.getElementById("chkTicketVentaMasiva").disabled = true;
                document.getElementById("ddlEstadoTurno").disabled = true;
                document.getElementById("ddlEstadoTurno").value = "0"
                document.getElementById("ddlCajero").disabled = true;
                document.getElementById("ddlCajero").value = ""
                document.getElementById("ddlMedioAnulacion").disabled = true;                
            }

            if (obj[obj.selectedIndex].value == "T") {
                //document.getElementById("ddlPersona").disabled = false;
                //document.getElementById("ddlTipoTicket").disabled = false;
                //document.getElementById("ddlFlgCobro").disabled = false;
                document.getElementById("chkTicketVentaMasiva").disabled = false;
                document.getElementById("ddlEstadoTurno").disabled = false;
                document.getElementById("ddlCajero").disabled = false;
                document.getElementById("ddlFlgCobro").disabled = false;
                document.getElementById("ddlMedioAnulacion").disabled = true;
            }
        }

        function ControlarDropDownListEstado(obj) 
        {
            if (obj[obj.selectedIndex].value == "X") {
                document.getElementById("ddlMedioAnulacion").disabled = false;
            }
            else {
                document.getElementById("ddlMedioAnulacion").disabled = true;
            }

            if (obj[obj.selectedIndex].value == "P") {
                document.getElementById("ddlEstadoTurno").value = "S";
                document.getElementById("ddlEstadoTurno").disabled = true;
            }
            else if (document.getElementById("ddlTipoDocumento").value == "T") {
                document.getElementById("ddlEstadoTurno").value = "0";
                document.getElementById("ddlEstadoTurno").disabled = false;
            }
        }

       function validarFiltros() {
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";
            document.getElementById('lblTotal').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtHasta').value,
                                   document.getElementById('txtHoraHasta').value
                                   )) {
                return true;
            } else {
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }
       }

       function cleanGrilla() 
       {
            if (document.getElementById("grvPaginacion") != null) {
                document.getElementById("grvPaginacion").style.display = "none";
            }
            if (document.getElementById("grvPaginacionBoarding") != null) {
                document.getElementById("grvPaginacionBoarding").style.display = "none";
            }
       }
    </script>

    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style6
        {
            width: 60px;
        }
        .style10
        {
            width: 15%;
        }
        .style11
        {
            height: 30px;
        }
        .style12
        {
            width: 60px;
            height: 30px;
        }
        .style13
        {
            width: 15%;
            height: 30px;
        }
        .style14
        {
            width: 1%;
            height: 30px;
        }
        .style15
        {
            width: 5%;
            height: 30px;
        }
        .style17
        {
            width: 10%;
            height: 30px;
        }
        .style18
        {
            width: 57px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">    
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360">
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
                            <td align="left" style="height: 30px; width: 100%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 80%;">
                                            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <!-- TITLE -->
                                                    <td style="width: 10px;" rowspan="5">
                                                    </td>
                                                    <td colspan="15" style="height: 20px;">
                                                        <asp:Label ID="lblDesde0" runat="server" CssClass="TextoFiltro" Font-Bold="True">Fecha Creación:</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <!-- FIRST ROW FILTER -->
                                                    <td style="width: 4%;">
                                                        <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td rowspan="2" style="width: 75px;">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtDesde" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                                        MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                                        BackColor="#E4E2DC" ontextchanged="txtDesde_TextChanged"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                                        PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 25px;">
                                                        <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                            Width="22px" Height="22px" />
                                                    </td>
                                                    <td rowspan="2" class="style6">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                        onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                                        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblHoraIni" runat="server" Text="(hh:mm:ss)" CssClass="TextoFiltro"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 5%;">
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:Label ID="lblTipoOperacion" runat="server" Width="100%" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td style="width: 27%">
                                                      
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" Width="100%" CssClass="combo2"
                                                                                onChange="javascript:ControlarDropDownList(this);" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoDoc_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                        <td style="width: 15%;">
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 35%;">
                                                                            <asp:DropDownList ID="ddlEstadoTicket" runat="server" Width="100px" CssClass="combo2"
                                                                                onChange="javascript:ControlarDropDownListEstado(this);" 
                                                                                AutoPostBack="False">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td style="width: 5%;">
                                                    </td>
                                                    <td style="width: 5%;">
                                                        <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro" Width="100%"></asp:Label>
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width: 1%;">
                                                    </td>
                                                    <!--<td style="width: 10%;  border: 1px inset #dcdcdc;">                                            
                                        </td>
                                        -->
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: top;">
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td height="25px">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <!-- SECOND ROW FILTER -->
                                                    <td valign="top" class="style11">
                                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td valign="top" class="style11">
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtHasta" runat="server" Width="72px" CssClass="textbox" Height="16px"
                                                                        MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                                        BackColor="#E4E2DC"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                                        PopupButtonID="imgbtnCalendar2" TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Width="50px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top" class="style11">
                                                        <asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                            Width="22px" Height="22px" />
                                                    </td>
                                                    <td valign="top" class="style12">
                                                        <table cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                        onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                                        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                                        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                                    </cc1:MaskedEditExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Label ID="lblHoraIniHasta" runat="server" Text="(hh:mm:ss)" CssClass="TextoFiltro"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td class="style11">
                                                    </td>
                                                    <td valign="top" class="style11">
                                                        <asp:Label ID="lblTipoTicket" runat="server" Width="100%" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td valign="top" class="style11">
                                                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                                        <table cellpadding="0" cellspacing="0" style="width: 100%; height: 30px;">
                                                            <tr style="vertical-align: top;">
                                                                <td class="style18" width="50">
                                                                    <asp:DropDownList ID="ddlTipoTicket" runat="server" Width="180px" CssClass="combo2"
                                                                        Height="18px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:Label ID="lblFlgCobro" runat="server" Width="100%" CssClass="TextoFiltro" Height="16px"></asp:Label>
                                                                </td>
                                                                <td style="width: 30%;">
                                                                    <asp:DropDownList ID="ddlFlgCobro" runat="server" Width="100px" CssClass="combo2">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                                    </td>
                                                    <td class="style11">
                                                    </td>
                                                    <td valign="top" class="style11">
                                                        <asp:Label ID="lblPersona" runat="server" Width="54px" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td valign="top" class="style13">
                                                        <asp:DropDownList ID="ddlPersona" runat="server" CssClass="combo2" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style14">
                                                    </td>
                                                    <td valign="top" class="style15">
                                                        <asp:Label ID="lblVuelo" runat="server" Width="54px" CssClass="TextoFiltro">Tipo Vuelo:</asp:Label>
                                                    </td>
                                                    <td valign="top" class="style17">
                                                        <asp:DropDownList ID="ddlVuelo" runat="server" CssClass="combo2" Width="105%">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style11">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="40px">
                                                        <asp:Label ID="lblEstadoTurno" runat="server" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:DropDownList ID="ddlEstadoTurno" runat="server" Width="100%" CssClass="combo2">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblCajero" runat="server" Width="100%" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td style="width: 40%;">
                                                        <asp:DropDownList ID="ddlCajero" runat="server" Width="100%" CssClass="combo2">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMedioAnulacion" runat="server" Width="100%" CssClass="TextoFiltro"></asp:Label>
                                                    </td>
                                                    <td class="style10">
                                                        <asp:DropDownList ID="ddlMedioAnulacion" runat="server" Width="100%" CssClass="combo2">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td colspan="2">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%--</ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoDocumento" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                        </td>
                                        <td align="right" style="width: 20%;">
                                            <asp:CheckBox ID="chkTicketVentaMasiva" runat="server" CssClass="TextoFiltro" />
                                            <asp:Label ID="lblTicketVentaMasiva" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <b>
                                                <asp:LinkButton ID="lbExportar" runat="server" OnClick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar 
                                            a Excel]</asp:LinkButton></b>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <br />
                                                    <br />
                                                    &nbsp; <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" OnClientClick="return validarFiltros()"
                                                        CssClass="Boton" Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvPaginacion" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%"
                                                CssClass="grilla" OnPageIndexChanging="grvPaginacion_PageIndexChanging" OnSorting="grvPaginacion_Sorting"
                                                AutoGenerateColumns="False" OnRowCommand="grvPaginacion_RowCommand" OnRowDataBound="grvPaginacion_RowDataBound"
                                                AllowPaging="True" AllowSorting="True"   GroupingDepth="0">
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <EditRowStyle BorderColor="#FF0066" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Num_Secuencial" />
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Dsc_Tipo_Ticket" SortExpression="Dsc_Tipo_Ticket" />
                                                    <asp:BoundField HeaderText="Compañía" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Fch. Creación" DataField="Fch_Creacion" SortExpression="Fch_Creacion" />
                                                    <asp:BoundField HeaderText="Fch. Vuelo" DataField="Fch_Vuelo" SortExpression="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Fch. Emisión" DataField="FHEmision" SortExpression="FHEmision" />
                                                    <asp:BoundField HeaderText="Fch. Uso" DataField="FHUso" SortExpression="FHUso" />
                                                    <asp:BoundField HeaderText="Fch. Rehab." DataField="FHReh" SortExpression="FHReh" />
                                                    <asp:BoundField HeaderText="Estado Turno" DataField="EstadoTurno" SortExpression="EstadoTurno" />
                                                    <asp:BoundField HeaderText="Cajero Emisión" DataField="Cta_Usuario" SortExpression="Cta_Usuario" />
                                                    <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Campo" SortExpression="Dsc_Campo" />
                                                    <asp:BoundField DataField="Tip_Estado_Actual" Visible="false" />
                                                </Columns>
                                            </cc2:PagingGridView>
                                            <cc2:PagingGridView ID="grvPaginacionBoarding" runat="server" BackColor="White" BorderColor="#999999"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%"
                                                CssClass="grilla" AutoGenerateColumns="False" OnPageIndexChanging="grvPaginacionBoarding_PageIndexChanging"
                                                OnRowCommand="grvPaginacionBoarding_RowCommand" OnSorting="grvPaginacionBoarding_Sorting"
                                                OnRowDataBound="grvPaginacionBoarding_RowDataBound" AllowPaging="True" AllowSorting="True"
                                                 >
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <EditRowStyle BorderColor="#FF0066" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. SEAE" SortExpression="Cod_Numero_Bcbp">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codBoarding" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                CommandName="ShowBoarding" Text='<%# Eval("Cod_Numero_Bcbp") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Correlativo" />
                                                    <asp:BoundField HeaderText="Compa&#241;&#237;a" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Fch. Vuelo" DataField="Fch_Vuelo" SortExpression="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                    <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                    <asp:BoundField HeaderText="Tipo Ingreso" DataField="Dsc_Tip_Ingreso" SortExpression="Dsc_Tip_Ingreso" />
                                                    <asp:BoundField HeaderText="Usuario Proceso" DataField="Cta_Usuario" SortExpression="Cta_Usuario" />
                                                    <asp:BoundField HeaderText="Fch. Creación" DataField="Fch_Creacion" SortExpression="Fch_Creacion" />
                                                    <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Tip_Estado" SortExpression="Dsc_Tip_Estado" />
                                                    <asp:BoundField DataField="Tip_Estado" Visible="false" />
                                                    <asp:TemplateField HeaderText="Asociado" SortExpression="Flg_Tipo_Bcbp">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEstadoAsociacion" runat="server" Text='<%# ( /* Eval("Flg_Tipo_Bcbp")!=DBNull.Value && */ Int32.Parse(Eval("Flg_Tipo_Bcbp").ToString())==1) ? "Si" : "No"  %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </cc2:PagingGridView>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label></div>
                                            <br />
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <input type="hidden" id="lblTotalRows" runat="server" value="0" />
                                            <input type="hidden" id="lblMaxRegistros" runat="server" value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="100">
                                        <ProgressTemplate>
                                            
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
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                    <br>
                    
                    </br>
                </td>
            </tr>
        </table>
    </div>
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
    <uc5:ConsDetTicket ID="ConsDetTicket1" runat="server" />
    </form>
</body>
</html>
