<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cns_Turnos.aspx.cs" Inherits="Modulo_Consultas_ConsultaDetalleTurnos" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Detalle de Turno</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="javascript" type="text/javascript">

        function imgPrint_onclick() {
        
        var numRegistros = document.getElementById("lblTotalRows").value;
        var maxRegistros = document.getElementById("lblMaxRegistros").value;
            
        //validacion
        if (!isNaN(parseInt(numRegistros))) {
          
           if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
           var sDesde = document.getElementById("txtDesde").value;
            var sHasta = document.getElementById("txtHasta").value;
            var sHoraDesde = document.getElementById("txtHoraDesde").value;
            var sHoraHasta = document.getElementById("txtHoraHasta").value;
            var idUsuario = document.getElementById("ddlUsuario").value;
            var idPtoVta = document.getElementById("txtPtoVta").value;

            //Descripciones
            var idDscU = (idUsuario != "0") ? document.getElementById("ddlUsuario").options[document.getElementById("ddlUsuario").selectedIndex].text : "";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptTurnos.aspx" + "?sDesde=" + sDesde + "&sHasta=" + sHasta
                                        + "&idUsuario=" + idUsuario + "&idPtoVta=" + idPtoVta + "&sHoraDesde=" + sHoraDesde
                                        + "&sHoraHasta=" + sHoraHasta
                                        + "&idDscU=" + idDscU
                                        , "mywindow", "location=0,status=0,scrollbars=yes,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);

            //alert('wleft:' + wleft + '  wtop:' + wtop + '  screen.width:' + screen.width + '  screen.height:' + screen.height);
            //ventimp.moveTo(wleft, wtop);
            ventimp.focus();
           
           }else {
                    alert("La impresión solo permite " + maxRegistros + " registros");
                    //return false;
                }
                
            
         } 
                else {
                alert("No existen registros para imprimir \nSeleccione otros filtros");
                //return false;
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
        
        

    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="3600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td  align="center">
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
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Filtro Fecha Inicio:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" Width="100px" onfocus="this.blur();"
                                                            onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                                                        <span id="lblErrDesde" style="color: Red;"></span>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtDesde" PopupButtonID="imgbtnCalendarDesde" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
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
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                            onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mee_txtHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 100px;">
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:DropDownList ID="ddlUsuario" runat="server" Width="240px" CssClass="combo2">
                                            </asp:DropDownList>
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
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" CssClass="textbox" Width="100px" onfocus="this.blur();"
                                                            onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"></asp:TextBox>
                                                        <span id="lblErrHasta" style="color: Red;"></span>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="txtHasta" PopupButtonID="imgbtnCalendarHasta" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp"
                                                runat="server" Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onkeypress="JavaScript: Tecla('Time');"
                                                            onBlur="JavaScript:CheckTime(this)" ReadOnly="false" Width="56px" ValidationGroup="vgHraHasta"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mee_txtHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraFin" runat="server" CssClass="TextoEtiqueta" Text="( hh:mm:ss )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 100px;">
                                        </td>
                                        <td style="width: 100px;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPtoVenta" runat="server" CssClass="TextoFiltro"
                                                Width="75px"></asp:Label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="width: 100px;">
                                            <asp:TextBox ID="txtPtoVta" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                MaxLength="6" Width="88px"></asp:TextBox>
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
                            <asp:LinkButton ID="lbExportar" runat="server" 
                                 onclick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <%--<a href="" id="lnkExportar" runat="server" onclick="javascript:exportarExcel_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblExportar" runat="server">[Exportar a Excel]</asp:Label>
                                            </b></a>--%>
                                            <br />
                                            <br />
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
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvTurno" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" OnPageIndexChanging="grvTurno_PageIndexChanging"
                                                OnRowCommand="grvTurno_RowCommand" OnSorting="grvTurno_Sorting" OnRowCreated="grvTurno_RowCreated"
                                                OnRowDataBound="grvTurno_RowDataBound">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <input type="hidden" id="lblTotalRows" runat="server" value="0" />
                                            <input type="hidden" id="lblMaxRegistros" runat="server" value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="10">
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
                </td>
            </tr>
            <tr>
                <!-- FOOTER -->
                <td>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtHasta"
        ControlToValidate="txtDesde" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtDesde"
        ControlToValidate="txtHasta" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>    
</body>
</html>
