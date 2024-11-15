<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_ResumenCompania.aspx.cs"
    Inherits="Rpt_ResumenCompania" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Detalle de Compañia</title>
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
            var sDesde = document.getElementById("txtFechaInicio").value;
            var sHasta = document.getElementById("txtFechaFin").value;
            var idHoraDesde = document.getElementById("txtHoraDesde").value;
            var idHoraHasta = document.getElementById("txtHoraHasta").value;
            var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptResumenCompania.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta  +"&idHoraDesde=" + idHoraDesde
                                            + "&idHoraHasta=" + idHoraHasta + "&sFechaEstadistico=" + sFechaEstadistico
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
                    var sDesde = document.getElementById("txtFechaInicio").value;
                    var sHasta = document.getElementById("txtFechaFin").value;
                    var idHoraDesde = document.getElementById("txtHoraDesde").value;
                    var idHoraHasta = document.getElementById("txtHoraHasta").value;
                    var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;

                    var w = 900 + 32;
                    var h = 500 + 96;
                    var wleft = (screen.width - w) / 2;
                    var wtop = (screen.height - h) / 2;

                    var ventimp = window.open("ReporteRPT/rptResumenCompania.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta  +"&idHoraDesde=" + idHoraDesde
                                            + "&idHoraHasta=" + idHoraHasta + "&sFechaEstadistico=" + sFechaEstadistico
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            //ventimp.moveTo(wleft, wtop);
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
            //Clean screen
            document.getElementById('lblMensajeError').innerHTML = "";
            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtFechaInicio').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtFechaFin').value,
                                   document.getElementById('txtHoraHasta').value
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
            if (document.getElementById("grvResumenCompania") != null) {
                document.getElementById("grvResumenCompania").style.display = "none";
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
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Venta:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaInicial" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaInicio" runat="server" Width="88px" CssClass="textboxFecha"
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
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarInicio" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime2(this)"
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
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFechaFinal" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaFin" runat="server" Width="88px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaFin0" runat="server" CssClass="TextoFiltro" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarFin" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime2(this)"
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
                            </td>
                            <td align="right">
                                <b>
                                    <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                                        OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <br />
                                        <br />
                                        <a href="" id="lnkHabilitar" runat="server" onclick="validarImprimir();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClientClick="return validarCampos()"
                                            Style="cursor: hand;" OnClick="btnConsultar_Click" />
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
                                        <asp:Label ID="lblFechaEstadistico" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <cc2:PagingGridView ID="grvResumenCompania" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" OnPageIndexChanging="grvResumenCompania_PageIndexChanging"
                                                CssClass="grilla" GroupingDepth="4" AllowSorting="True"  >
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Venta" DataField="Fecha_Venta" />
                                                    <asp:BoundField HeaderText="Compañía" DataField="Dsc_Compania" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" />
                                                    <asp:BoundField HeaderText="Vendido" DataField="Vendido" />
                                                    <asp:BoundField HeaderText="Usado" DataField="Usado" />
                                                    <asp:BoundField HeaderText="Emitido" DataField="Emitido" />
                                                    <asp:BoundField HeaderText="Rehabilitado" DataField="Rehabilitado" />
                                                    <asp:BoundField DataField="Vencido" HeaderText="Vencido" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <asp:GridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False"
                                                    HorizontalAlign="Center" CssClass="grillaShort" OnRowCreated="grvDataResumen_RowCreated">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="Dsc_Campo" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" />
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>
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
