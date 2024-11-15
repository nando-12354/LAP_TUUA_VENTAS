<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_ResumenTurno.aspx.cs" Inherits="Rpt_ResumenTurno" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Resumen Diario por Turnos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/StyleDarck.css" rel="stylesheet" type="text/css" />
    <style type="text/css">@import url(css/calendar-system.css);</style>     
     
    <script language="JavaScript" type="text/javascript">
        function validar() {
            document.getElementById("lblMensajeError").innerHTML = '';
            if (document.getElementById("rbtnTurno").checked == true) {
                //UP
                document.getElementById("txtTurno").disabled = false;
                document.getElementById("txtTurno").style.backgroundColor = '#FFFFFF';
                //DOWN
                document.getElementById("txtTurnoDesde").disabled = true;
                document.getElementById("txtTurnoHasta").disabled = true;
                document.getElementById("txtTurnoDesde").value = '';
                document.getElementById("txtTurnoHasta").value = '';
                document.getElementById("txtTurnoDesde").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTurnoHasta").style.backgroundColor = '#CCCCCC';
                document.getElementById("lblMensajeError").innerHTML = '';                                
            }
            if (document.getElementById("rbtnRangoTurno").checked == true) {
                //UP
                document.getElementById("txtTurno").disabled = true;
                document.getElementById("txtTurno").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtTurno").value = '';
                //DOWN
                document.getElementById("txtTurnoDesde").disabled = false;
                document.getElementById("txtTurnoHasta").disabled = false;
                document.getElementById("txtTurnoDesde").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtTurnoHasta").style.backgroundColor = '#FFFFFF';                
            }
        }
        function llamadaReporte() {
        }
        function consultar() {
            document.getElementById("lblMensajeError").innerHTML = '';
            document.getElementById("IframeReporte").style.display = '';
            if (document.getElementById("rbtnTurno").checked == true) {
                if (form1.txtTurno.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Número de Turno Requerido';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
                //else if (form1.txtTurno.value)
            }
            if (document.getElementById("rbtnRangoTurno").checked == true) {
                if (form1.txtTurnoDesde.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Número de Turno Desde Requerido';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
                if (form1.txtTurnoHasta.value == '') {
                    document.getElementById("lblMensajeError").innerHTML = 'Número de Turno Hasta Requerido';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
                if (form1.txtTurnoHasta.value < form1.txtTurnoDesde.value) {
                    document.getElementById("lblMensajeError").innerHTML = 'Número de Turno Hasta debe ser menor al Turno Desde';
                    document.getElementById("IframeReporte").style.display = 'none';
                    return false;
                }
            }
        
            var turnoDesde, turnoHasta;
            if (document.getElementById("rbtnTurno").checked == true) {
                turnoDesde = document.forms[0].txtTurno.value;
                turnoHasta = document.forms[0].txtTurno.value;                
            }
            if (document.getElementById("rbtnRangoTurno").checked == true) {
                turnoDesde = document.forms[0].txtTurnoDesde.value;
                turnoHasta = document.forms[0].txtTurnoHasta.value;
            }
            var urlReporte = "ReporteRPT/rptResumenDiario.aspx?iTipo=2&turnoDesde=" + turnoDesde + "&turnoHasta=" + turnoHasta;

            console.log(urlReporte);

            document.getElementById('IframeReporte').src = urlReporte;


            return false;
        }

        //Deshabilitar boton Aceptar
        var accionSave = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnConsultar').disabled = true;
                    document.body.style.cursor = 'wait';
                }
            }
        }

        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnConsultar').disabled = false;
                    document.body.style.cursor = 'default';
                    accionSave = false;
                }
            }
        }
    </script>
</head>
<body onload="validar()">
<form id="form1" runat="server">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla">
                <!-- HEADER ZONE -->
                <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
            </td>
        </tr>
        <tr>
            <td><!-- FILTER ZONE -->
            <div class="EspacioSubTablaPrincipal">
                <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                    <tr class="formularioTitulo">
                    <td class="SpacingGrid"></td>
                    <td class="CenterGrid">
                        <table style="width: 100%; left: 0px; position: relative; top: 0px;" class="alineaderecha">
                        <tr>
                        <td>
                            <table>
                            <tr><td><asp:RadioButton ID="rbtnTurno" runat="server" GroupName="TipoConsulta" 
                                    onClick="validar();" CssClass="TextoFiltro" /></td>
                                <td><asp:Label ID="lblTurno" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                                <td><asp:TextBox ID="txtTurno" runat="server" CssClass="textbox" MaxLength="6" onkeypress="numero()" onblur="val_int(this)"></asp:TextBox></td>
                            </tr>
                            <tr><td><asp:RadioButton ID="rbtnRangoTurno" runat="server" GroupName="TipoConsulta" 
                                    onClick="validar();" CssClass="TextoFiltro" /></td>
                                <td><asp:Label ID="lblRangoTurno" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                            </tr>
                            <tr><td></td>
                                <td><asp:Label ID="lblRangoDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                                <td><asp:TextBox ID="txtTurnoDesde" runat="server" CssClass="textbox" MaxLength="6" onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox></td>
                                <td>&nbsp;</td>
                                <td><asp:Label ID="lblRangoHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label></td>
                                <td><asp:TextBox ID="txtTurnoHasta" runat="server" CssClass="textbox" MaxLength="6" onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox></td>
                            </tr>                                                        
                            </table>
                        </td>                        
                        <td>
                            <table>
                            <tr>
                            <td>&nbsp;</td>
                            </tr>
                            <tr>
                            <td><asp:Button ID="btnConsultar" runat="server" OnClientClick="return consultar()" CssClass="Boton" /></td>
                            </tr> 
                            </table>
                        </td>                                   
                        </tr>                                                                                
                        </table>                        
                    </td>
                    <td class="SpacingGrid" valign="bottom"></td>
                    </tr>                    
                </table>
            </div>                    
            </td>
        </tr>  
        <tr>
            <td><!-- LINE SEPARATION ZONE -->
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
        </tr> 
        <tr>
            <td valign="top"><!-- DATA RESULT ZONE -->
            <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
            <tr>
                <td class="SpacingGrid"></td>
                <td class="CenterGrid">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                        <Scripts>
                            <asp:ScriptReference Path="~/javascript/jSManager.js" />
                        </Scripts>
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                            <br/>    
                            <div style="overflow:auto; width: 980px; height: 420px; 
                                        border-left: 0px gray solid; border-bottom: 0px gray solid; 
                                        padding:0px; margin: 0px;  z-index: auto; float: none;">   
                            <iframe id="IframeReporte" src="Prueba.aspx" width="100%" height="100%" align="middle" frameborder="0"></iframe>                                        
                        </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td class="SpacingGrid"></td>
            </tr>
            </table>
            </td>
        </tr>        
        <tr>
            <td><!-- FOOTER PAGE ZONE -->
                <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td style="height: 11px">
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    </td>
                </tr>
                </table>
            </td>
        </tr>         
    </table>
</form>    
</body>
</html>


