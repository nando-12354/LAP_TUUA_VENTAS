<%@ page language="C#" autoeventwireup="true" inherits="Cns_DetalleTicketProcesado, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/ConsDetTicket.ascx" TagName="ConsDetTicket" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Detalle de Tickets Procesados</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">
        function imgPrint_onclick() {
            var idCajero = document.getElementById("hdCajero").value;
            var idTurno = document.getElementById("hdTurno").value;

            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;

            var ventimp = window.open("ReporteCNS/rptDetalleTicketProcesados.aspx" + "?idCajero=" + idCajero + "&idTurno=" + idTurno + "&idOrdenacion=" + idOrdenacion + "&idColumna=" + idColumna + "&idPaginacion=" + idPaginacion, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=800");
            ventimp.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td align="center">
                    <!-- HEADER ZONE -->
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- BACK HEADER ZONE -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td align="right" style="text-align: left">
                                &nbsp;&nbsp;&nbsp; <a href="Cns_TicketProcesado.aspx?ret=1">
                                    <img alt="" src="Imagenes/flecha_back.png" border="0" />
                                </a>
                            </td>
                            <td align="right" style="text-align: right">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- LINE SEPARATION ZONE -->
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td>
                    <!-- FILTER ZONE -->
                    <div class="EspacioSubTablaPrincipal">
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td>
                                </td>
                                <td class="CenterGrid">
                                    <table style="width: 100%; left: 0px; top: 0px;" class="alineaderecha">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblTitulo" runat="server" CssClass="titulosecundario">Listado de Tickets procesados</asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblCajero" runat="server" CssClass="titulosecundario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblCajeroRpt" runat="server" CssClass="titulosecundario"></asp:Label>
                                                            <asp:HiddenField ID="hdTurno" runat="server" />
                                                            <asp:HiddenField ID="hdCajero" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right">
                                                <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                    style="cursor: hand;"><b>
                                                        <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                    </b></a>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
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
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvTicketProcesado" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" AllowSorting="True" OnSorting="grvTicketProcesado_Sorting"
                                                OnPageIndexChanging="grvTicketProcesado_PageIndexChanging" OnRowCommand="grvTicketProcesado_RowCommand"
                                                OnRowDataBound="grvTicketProcesado_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nro. Ticket" SortExpression="Cod_Numero_Ticket">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument='<%# Eval("Cod_Numero_Ticket") %>'
                                                                CommandName="ShowTicket" Text='<%# Eval("Cod_Numero_Ticket") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Secuencial" HeaderText="Secuencial" SortExpression="Secuencial" />
                                                    <asp:TemplateField HeaderText="Tipo Ticket" SortExpression="Cod_Tipo_Ticket">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITipoTicket" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Cod_Tipo_Ticket"), Eval("Dsc_Tipo_Ticket")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Dsc_Compania" HeaderText="Compa&#241;&#237;a" SortExpression="Dsc_Compania" />
                                                    <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaVuelo0" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Nro. vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                    <asp:BoundField DataField="Fch_Inicio_Format" HeaderText="Fecha Emisión" SortExpression="Fch_Inicio_Format" />
                                                    <asp:BoundField DataField="Tip_Estado_Actual_Format" HeaderText="Estado Actual" SortExpression="Tip_Estado_Actual_Format" />
                                                    <asp:BoundField DataField="Tip_Estado_Actual" Visible="false" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
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
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                    <br />
                </td>
            </tr>            
        </table>
        <uc5:ConsDetTicket ID="ConsDetTicket1" runat="server" />
    </div>    
    </form>
</body>
</html>
