<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_ResumenDiario.aspx.cs"
    Inherits="Rpt_ResumenDiario" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Resumen Diario</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        html, body
        {
            height: 100%; /*overflow: hidden;*/
            margin: 0;
            padding: 0;
        }
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>

    <script language="JavaScript" type="text/javascript">
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

        function validar() {
            document.getElementById("lblMensajeError").innerHTML = '';
            if (document.getElementById("rbtnFecha").checked == true) {
                //UP
                document.getElementById("txtFecha").disabled = false;
                //document.getElementById("txtFecha").style.backgroundColor = '#FFFFFF';
                document.getElementById("imgbtnCalendarFecha").disabled = false;
                //DOWN
                document.getElementById("txtFechaDesde").disabled = true;
                document.getElementById("imgbtnCalendarDesde").disabled = true;
                document.getElementById("txtHoraDesde").disabled = true;
                document.getElementById("txtFechaHasta").disabled = true;
                document.getElementById("imgbtnCalendarHasta").disabled = true;
                document.getElementById("txtHoraHasta").disabled = true;
                document.getElementById("txtFechaDesde").value = '';
                document.getElementById("imgbtnCalendarDesde").value = '';
                document.getElementById("txtHoraDesde").value = '';
                document.getElementById("txtFechaHasta").value = '';
                document.getElementById("imgbtnCalendarHasta").value = '';
                document.getElementById("txtHoraHasta").value = '';
                document.getElementById("txtFechaDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtHoraDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtFechaHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtHoraHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("lblMensajeError").innerHTML = '';
            }
            if (document.getElementById("rbtnRangoFecha").checked == true) {
                //UP
                document.getElementById("txtFecha").disabled = true;
                document.getElementById("txtFecha").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtFecha").value = '';
                document.getElementById("imgbtnCalendarFecha").disabled = true;
                //DOWN
                document.getElementById("txtFechaDesde").disabled = false;
                document.getElementById("imgbtnCalendarDesde").disabled = false;
                document.getElementById("txtHoraDesde").disabled = false;
                document.getElementById("txtFechaHasta").disabled = false;
                document.getElementById("imgbtnCalendarHasta").disabled = false;
                document.getElementById("txtHoraHasta").disabled = false;
                //document.getElementById("txtFechaDesde").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtHoraDesde").style.backgroundColor = '#FFFFFF';
                //document.getElementById("txtFechaHasta").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtHoraHasta").style.backgroundColor = '#FFFFFF';
            }
        }
        function llamadaReporte() {
        }
        function consultar() {
            document.getElementById("lblMensajeError").innerHTML = '';
            document.getElementById("IframeReporte").style.display = '';
            if (document.getElementById("rbtnFecha").checked == true) {
                if (form1.txtFecha.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Fecha Requerida';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
            }
            if (document.getElementById("rbtnRangoFecha").checked == true) {
                if (form1.txtFechaDesde.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Fecha Desde Requerida';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
                if (form1.txtFechaHasta.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Fecha Hasta Requerida';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
            }

            var fechaDesde, fechaHasta, horaDesde, horaHasta, tipo;
            if (document.getElementById("rbtnFecha").checked == true) {
                fechaDesde = document.forms[0].txtFecha.value.substr(6, 4) + document.forms[0].txtFecha.value.substr(3, 2) + document.forms[0].txtFecha.value.substr(0, 2);
                fechaHasta = fechaDesde;
                horaDesde = "";
                horaHasta = "";
                tipo = "3";
            }
            if (document.getElementById("rbtnRangoFecha").checked == true) {
                fechaDesde = document.forms[0].txtFechaDesde.value.substr(6, 4) + document.forms[0].txtFechaDesde.value.substr(3, 2) + document.forms[0].txtFechaDesde.value.substr(0, 2);
                fechaHasta = document.forms[0].txtFechaHasta.value.substr(6, 4) + document.forms[0].txtFechaHasta.value.substr(3, 2) + document.forms[0].txtFechaHasta.value.substr(0, 2);
                horaDesde = document.forms[0].txtHoraDesde.value.substr(0, 2) + document.forms[0].txtHoraDesde.value.substr(3, 2) + document.forms[0].txtHoraDesde.value.substr(6, 2);
                horaHasta = document.forms[0].txtHoraHasta.value.substr(0, 2) + document.forms[0].txtHoraHasta.value.substr(3, 2) + document.forms[0].txtHoraHasta.value.substr(6, 2);
                if (horaDesde == "______") { horaDesde = ""; }
                if (horaHasta == "______") { horaHasta = ""; }
                if (horaDesde == "" && horaHasta == "")
                    tipo = "3";
                else
                    tipo = "1";
            }

            var urlReporte = "ReporteRPT/rptResumenDiario.aspx?iTipo=" + tipo + "&fechaDesde=" + fechaDesde + "&fechaHasta=" + fechaHasta + "&horaDesde=" + horaDesde + "&horaHasta=" + horaHasta;

            console.log(urlReporte);

            document.getElementById('IframeReporte').src = urlReporte;

            return false;
        }
    </script>

</head>
<body onload="validar()">
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td align="center">
                    <!-- HEADER ZONE -->
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo3">
                <td>
                    <!-- FILTER ZONE -->
                    <div class="EspacioSubTablaPrincipal">
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr class="formularioTitulo">
                                <td class="SpacingGrid">
                                </td>
                                <td class="CenterGrid">
                                    <table style="width: 100%;" class="alineaderecha">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton ID="rbtnFecha" runat="server" GroupName="TipoConsulta" onClick="validar();"
                                                                CssClass="TextoFiltro" AutoPostBack="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFecha" runat="server" Width="88px" CssClass="textboxFecha" Height="16px"
                                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblFecha0" runat="server" CssClass="TextoEtiqueta" Text="(dd/mm/yyyy)"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgbtnCalendarFecha" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                                Width="22px" Height="22px" ValidationGroup="RangoFechaVaildacion2" />
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
                                                            <asp:RadioButton ID="rbtnRangoFecha" runat="server" GroupName="TipoConsulta" onClick="validar();"
                                                                CssClass="TextoFiltro" AutoPostBack="False" />
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblRangoFecha" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaDesde" runat="server" Width="88px" CssClass="textboxFecha"
                                                                            Height="16px" MaxLength="10" onblur="JavaScript: ValidarFecha(this,'lblMensajeError');"
                                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>
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
                                                                Width="22px" Height="22px" ValidationGroup="RangoFechaVaildacion" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td rowspan="2">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                            onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                        </td>
                                                        <td rowspan="2">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFechaHasta" runat="server" Width="88px" CssClass="textboxFecha"
                                                                            Height="16px" MaxLength="10" onblur="JavaScript: ValidarFecha(this,'lblMensajeError');"
                                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoEtiqueta" Text="(dd/mm/yyyy)"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                                Width="22px" Height="22px" ValidationGroup="RangoFechaVaildacion" />
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td rowspan="2">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                                            onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Label ID="lblHoraHasta0" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                            <td align="right">
                                                <asp:Button ID="btnConsultar" runat="server" OnClientClick="return consultar()" CssClass="Boton" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="SpacingGrid" valign="bottom">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- LINE SEPARATION ZONE -->
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <!-- DATA RESULT ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                <br />
                                <div style="overflow: auto; width: 100%; height: 420px; border-left: 0px gray solid;
                                    border-bottom: 0px gray solid; padding: 0px; margin: 0px; z-index: auto; float: none;">
                                    <iframe id="IframeReporte" src="Prueba.aspx" width="100%" height="100%" align="middle" frameborder="0">
                                    </iframe>
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- FOOTER PAGE ZONE -->
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
    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha"
        PopupButtonID="imgbtnCalendarFecha">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
        PopupButtonID="imgbtnCalendarDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"
        PopupButtonID="imgbtnCalendarHasta">
    </cc1:CalendarExtender>
    <!--Declaracion Control Hora -->
    <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
    </cc1:MaskedEditExtender>
    <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
        ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
        CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
    </cc1:MaskedEditExtender>
    <!--Primera validacion de las fechas -->
    <!--Segunda Validacion de las fechas -->
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtFechaHasta"
        ControlToValidate="txtFechaDesde" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date" ValidationGroup="RangoFechaVaildacion"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtFechaDesde"
        ControlToValidate="txtFechaHasta" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date" ValidationGroup="RangoFechaVaildacion"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    </form>
</body>
</html>
