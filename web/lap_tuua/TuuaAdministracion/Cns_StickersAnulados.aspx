    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cns_StickersAnulados.aspx.cs"
    Inherits="Cns_StickersAnulados" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
    <%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Tickets Anulados</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <script language="javascript" type="text/javascript">
    
//    function imgPrint_onclick() {
//            //--------------------
//     var sFechaDesde = document.forms[0].txtDesde.value;
//     var sFechaHasta = document.forms[0].txtHasta.value;

//            if (sFechaDesde != "") {
//                var wordsFechaDesde = sFechaDesde.split('/');
//                sFechaDesde = wordsFechaDesde[2] + "" + wordsFechaDesde[1] + "" + wordsFechaDesde[0];
//            }
//            else { sFechaDesde = ""; }

//            if (sFechaHasta != "") {
//                var wordsFechaHasta = sFechaHasta.split('/');
//                sFechaHasta = wordsFechaHasta[2] + "" + wordsFechaHasta[1] + "" + wordsFechaHasta[0];
//            }
//            else { sFechaHasta = ""; }

//            var w = 900 + 32;
//            var h = 500 + 96;
//            var wleft = (screen.width - w) / 2;
//            var wtop = (screen.height - h) / 2;
//            
//            var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;
//            
//            var ventimp = window.open("ReporteCNS/rptCnsTicketAnulados.aspx" + "?sFechaDesde=" + sFechaDesde 
//                                            + "&sFechaHasta=" + sFechaHasta + "&sFechaEstadistico=" + sFechaEstadistico 
//                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
//            //ventimp.moveTo(wleft, wtop);
//            ventimp.focus();            
//        }
        
         function validarImprimir() {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            
            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                var sFechaDesde = document.forms[0].txtDesde.value;
                var sFechaHasta = document.forms[0].txtHasta.value;
                
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

                    var w = 900 + 32;
                    var h = 500 + 96;
                    var wleft = (screen.width - w) / 2;
                    var wtop = (screen.height - h) / 2;
            
                    var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;
            
                    var ventimp = window.open("ReporteCNS/rptCnsTicketAnulados.aspx" + "?sFechaDesde=" + sFechaDesde 
                                            + "&sFechaHasta=" + sFechaHasta + "&sFechaEstadistico=" + sFechaEstadistico 
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
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        @import url(css/calendar-system.css);
    </style>
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                                            &nbsp;</td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold">Fecha de Anulación:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
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
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaHasta0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                            &nbsp;
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
                             &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="validarImprimir();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                           
                            <asp:LinkButton ID="lbExportar" runat="server" 
                                 onclick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]
                                 </asp:LinkButton></b>
                                 <br />
                                 <br />
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClick="btnConsultar_Click" />
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
                                            <cc2:PagingGridView ID="grvConsultaAnulados" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" OnPageIndexChanging="grvConsultaAnulados_PageIndexChanging"
                                                CssClass="grilla" AllowSorting="True"  OnSorting="grvConsultaAnulados_Sorting"
                                                  GroupingDepth="4">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                <%--<asp:TemplateField HeaderText="Fch. Anul" SortExpression="Fch_Resumen">
                                                    <ItemTemplate>
                                                            <asp:Label ID="lblFchAnul" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha3(Convert.ToString(Eval("Fch_Resumen"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="Fch. Anul" SortExpression="Fch_Resumen" DataField="FechaResumen" />
                                                    <asp:BoundField HeaderText="Modalidad" DataField="Nom_Modalidad" />
                                                    <asp:BoundField HeaderText="Usuario" DataField="Cta_Usuario" />
                                                    <asp:BoundField HeaderText="Motivo" DataField="Dsc_Motivo" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <%--<asp:GridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False"
                                                    HorizontalAlign="Center" CssClass="grillaShort" 
                                                    onrowcreated="grvDataResumen_RowCreated" GroupingDepth="1" >
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" />
                                                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" />
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>--%>
                                                <cc2:PagingGridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False" 
                                                    HorizontalAlign="Center" CssClass="grillaShort" AllowPaging="False" AllowSorting="False"
                                                    GroupingDepth="1" OnRowDataBound="grvDataResumen_RowDataBound" OnRowCreated ="grvDataResumen_RowCreated">
                                             
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Documento" DataField="Tipo_Documento" />
                                                        <asp:BoundField HeaderText="Tipo Vuelo" DataField="Tip_Vuelo" />
                                                        <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                        <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Tip_Trasbordo" />
                                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    
                                                    <PagerSettings Mode="NumericFirstLast" />
                                                    
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
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
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar1"
        TargetControlID="txtDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar2"
        TargetControlID="txtHasta">
    </cc1:CalendarExtender>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtHasta"
        ControlToValidate="txtDesde" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtDesde"
        ControlToValidate="txtHasta" Display="None" ErrorMessage="Filtro de fechas inválido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:HiddenField ID="txtPaginacion" runat="server" />
    </form>    
</body>
</html>
