<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_DetalleLineaVuelo.aspx.cs"
    Inherits="Rpt_DetalleLineaVuelo" ResponseEncoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte - Detalle por Linea de Vuelo </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_ticketrehab" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">

        function onOk() { }
        function imgPrint_onclick() {

            var sFechaDesde = document.getElementById("txtDesde").value;
            var sFechaHasta = document.getElementById("txtHasta").value;
            var sCompania = document.getElementById("ddlCompania").value;
            var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;
            
            //Descripciones
            var idDscC = (sCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : " -TODOS- ";

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/Rpt_DetalleLineaVuelo.aspx" + "?sFechaDesde=" + sFechaDesde + "&sFechaHasta=" + sFechaHasta + "&sCompania=" + sCompania
                                        + "&idDscC=" + idDscC + "&sFechaEstadistico=" + sFechaEstadistico
                                        , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            ventimp.focus();
        }
        
         function validarImprimir(control) {
            var numRegistros = document.getElementById("lblTotalRows").value;
            var maxRegistros = document.getElementById("lblMaxRegistros").value;
            
            

            if (!isNaN(parseInt(numRegistros))) {
                if (parseInt(numRegistros) <= parseInt(maxRegistros)) {
                    var sFechaDesde = document.getElementById("txtDesde").value;
                    var sFechaHasta = document.getElementById("txtHasta").value;
                    var sCompania = document.getElementById("ddlCompania").value;
                    var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;
                    var tipoImp = control;
                          
                    //Descripciones
                    var idDscC = (sCompania != "0") ? document.getElementById("ddlCompania").options[document.getElementById("ddlCompania").selectedIndex].text : " -TODOS- ";

                    var w = 900 + 32;
                    var h = 500 + 96;
                    var wleft = (screen.width - w) / 2;
                    var wtop = (screen.height - h) / 2;

                    var ventimp = window.open("ReporteRPT/Rpt_DetalleLineaVuelo.aspx" + "?sFechaDesde=" + sFechaDesde + "&sFechaHasta=" + sFechaHasta + "&sCompania=" + sCompania
                                        + "&idDscC=" + idDscC + "&sFechaEstadistico=" + sFechaEstadistico + "&sTipoImp=" + tipoImp
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

    <style type="text/css">
        .style1
        {
            width: 6%;
        }
        .style2
        {
            width: 1%;
        }
        .style3
        {
            height: 20px;
        }
        .style4
        {
            width: 4%;
            height: 24px;
        }
        .style5
        {
            width: 25px;
            height: 24px;
        }
        .style6
        {
            width: 1%;
            height: 24px;
        }
        .style7
        {
            width: 6%;
            height: 24px;
        }
        .style8
        {
            width: 15%;
            height: 24px;
        }
        .style9
        {
            width: 10%;
            height: 24px;
        }
        .style10
        {
            width: 5%;
            height: 24px;
        }
    </style>
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
                <td>
                    <!-- FILTER ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 80%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="4" style="height: 20px;">
                                            <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True">Fecha de Vuelo:</asp:Label>
                                        </td>
                                        <td style="width: 100px;" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" BackColor="#E4E2DC"
                                                            Width="88px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar1" TargetControlID="txtDesde" Format="dd/MM/yyyy">
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
                                        <td class="style1">
                                            &nbsp;
                                            <asp:ImageButton ID="imgbtnCalendar1" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td class="style1">
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" Width="88px" CssClass="textbox" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnCalendar2" TargetControlID="txtHasta" Format="dd/MM/yyyy">
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
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar2" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />&nbsp;
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"
                                                Width="50px"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="ddlCompania" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlCompania" runat="server" Width="380px" CssClass="combo2">
                                            </asp:DropDownList>
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
                                        <a href="#" id="lnkHabilitar" runat="server" onclick="validarImprimir('D');"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimirDetalle" runat="server">Imprimir Detalle</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                            <br />
                                            <br />
                                         <a href="#" id="lnkHabilitarResumen" runat="server" onclick="validarImprimir('R');"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimirResumen" runat="server">Imprimir Resumen</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" OnClick="btnConsultar_Click" CssClass="Boton"
                                            Style="cursor: hand;" />&nbsp;&nbsp;&nbsp;
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
                <!-- CONTENT -->
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblFechaEstadistico" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <cc1:PagingGridView ID="grvDetalleLineaVuelo" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" CssClass="grilla" GridLines="Vertical" GroupingDepth="2"
                                                OnPageIndexChanging="grvDetalleLineaVuelo_PageIndexChanging" OnSorting="grvDetalleLineaVuelo_Sorting"
                                                  Width="100%" OnRowDataBound="grvDetalleLineaVuelo_RowDataBound">
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <EditRowStyle BorderColor="#FF0066" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <RowStyle BorderStyle="solid" BorderColor="#E6E6E6" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:BoundField DataField="Dsc_Compania" HeaderText="Aerolinea" />
                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo"/>
                                                    <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Nro. Vuelo"  />
                                                    <asp:BoundField DataField="Tip_Documento" HeaderText="Tipo Documento" />
                                                    <asp:BoundField DataField="Dsc_Tipo_Ticket" HeaderText="Tipo Ticket"/>
                                                    <asp:BoundField DataField="Cnt_Utilizada" HeaderText="Utilizados">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cnt_Vendida" HeaderText="Vendidos">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </cc1:PagingGridView>
                                            <br />
                                            <br />
                                            <asp:GridView ID="grvResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                BorderWidth="1px" CellPadding="3" HorizontalAlign="Center" CssClass="grillaShort"
                                                OnRowCreated="grvResumen_RowCreated" OnRowDataBound="grvResumen_RowDataBound"
                                                AutoGenerateColumns="False" ShowFooter="false">
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <Columns>
                                                    <asp:BoundField DataField="Tip_Documento" HeaderText="Tipo Documento">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tip_Vuelo" HeaderText="Tipo Vuelo">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Pasajero">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Tip_Trasbordo" HeaderText="Tipo Trasbordo">
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cnt_Utilizada" HeaderText="Utilizados">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Cnt_Vendida" HeaderText="Vendidos">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Black" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <asp:HiddenField ID="lblTotalRows" runat="server" />
                                            <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                            <br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel2" runat="server"
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
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
    </form>
</body>
</html>
