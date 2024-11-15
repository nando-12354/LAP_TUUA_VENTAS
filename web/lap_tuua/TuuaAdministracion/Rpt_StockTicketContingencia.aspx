<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_StockTicketContingencia.aspx.cs"
    Inherits="Rpt_StockTicketContingencia" %>

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
    <title>LAP - Reporte - Stock de Tickets de Contingencia</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
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
    
    <script language="JavaScript" type="text/javascript">
        function imgPrint_onclick() {
            var sFecha = document.getElementById("txtFecha").value;
            var idTipoTicket = document.getElementById("ddlTipoTicket").value;
            var sFechaEstadistico = document.getElementById("lblFechaEstadistico").innerText;
            //Descripciones
            var idDscT = (idTipoTicket != "0") ? document.getElementById("ddlTipoTicket").options[document.getElementById("ddlTipoTicket").selectedIndex].text : "";
            
            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteRPT/rptResumenStockTicketContingencia.aspx" + "?sFecha=" + sFecha + "&idTipoTicket=" + idTipoTicket + "&sFechaEstadistico=" + sFechaEstadistico + "&idDscT=" + idDscT
                                            , "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop );
            //ventimp.moveTo(wleft, wtop);
            ventimp.focus();
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
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFecha" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;<asp:ImageButton ID="imgbtnCalendar" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                            &nbsp;
                                        </td>
                                        <td style="width: 50px;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoTicket" runat="server" Width="240px" CssClass="combo2">
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
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        &nbsp;<a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                            style="cursor: hand;"><b>
                                                <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                            </b></a>&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClick="btnConsultar_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel3" runat="server"
                                    DisplayAfter="10">
                                    <ProgressTemplate>
                                        <%--<div id="progressBackgroundFilter">
                                        </div>--%>
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
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                        <asp:Label ID="lblFechaEstadistico" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <cc2:PagingGridView ID="grvStockTicketContingencia" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="False" 
                                                CssClass="grilla" GroupingDepth="1" AllowSorting="False" 
                                                  onrowcreated="grvStockTicketContingencia_RowCreated" 
                                                ShowFooter="true" onrowdatabound="grvStockTicketContingencia_RowDataBound">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Creación" DataField="Fecha_Resumen" />
                                                    <asp:BoundField HeaderText="Tipo Ticket" DataField="Des_Tipo_Ticket" />
                                                    <asp:BoundField HeaderText="Rango Inicial" DataField="Rango_Inicial" />
                                                    <asp:BoundField HeaderText="Rango Final" DataField="Rango_Final" />
                                                    <asp:BoundField HeaderText="Nacional" DataField="Num_Ticket_Nac" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField HeaderText="Internacional" DataField="Num_Ticket_Int" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField HeaderText="Nacional" DataField="Monto_Nac" ItemStyle-HorizontalAlign="Right"/>
                                                    <asp:BoundField HeaderText="Internacional" DataField="Monto_Int" ItemStyle-HorizontalAlign="Right"/>
                                                </Columns>                                                
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />                                                
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <asp:GridView ID="grvDataResumen" runat="server" BorderColor="#999999" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Both" AutoGenerateColumns="False"
                                                    HorizontalAlign="Center" CssClass="grillaShort" ShowFooter="true" 
                                                    onrowcreated="grvDataResumen_RowCreated" onrowdatabound="grvDataResumen_RowDataBound" 
                                                    >
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo Ticket" DataField="Des_Tipo_Ticket"  />
                                                        <asp:BoundField HeaderText="Cantidad de Tickets" DataField="Num_Ticket" NullDisplayText="0" ItemStyle-HorizontalAlign="Right"/>
                                                        <asp:BoundField HeaderText="Monto Total" DataField="Monto" NullDisplayText="0" ItemStyle-HorizontalAlign="Right"/>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
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
    <!--Validaciones de fecha-->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgbtnCalendar"
        Format="dd/MM/yyyy" TargetControlID="txtFecha">
    </cc1:CalendarExtender>
    </form>
</body>
</html>
