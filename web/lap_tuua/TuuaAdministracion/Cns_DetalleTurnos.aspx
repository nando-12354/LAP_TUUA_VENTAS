<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cns_DetalleTurnos.aspx.cs"
    Inherits="Modulo_Consultas_DetalleTurnos" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CnsDetTurno.ascx" TagName="CnsDetTurno" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Consulta - Detalle de Turno</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
		html,body{
			height:100%;
		}
    </style>
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
  

    <script language="JavaScript" type="text/javascript">
        function imgPrint_onclick() {
            var idTurno = document.getElementById("hdTurno").value;
            
            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;

            var w = 900 + 32;
            var h = 500 + 96;
            var wleft = (screen.width - w) / 2;
            var wtop = (screen.height - h) / 2;

            var ventimp = window.open("ReporteCNS/rptDetalleTurno.aspx" + "?idTurno=" + idTurno + "&idOrdenacion=" + idOrdenacion + "&idColumna=" + idColumna + "&idPaginacion=" + idPaginacion, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=500,left=" + wleft + ",top=" + wtop);
            //ventimp.moveTo(wleft, wtop);
            ventimp.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td>
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- WORK MENU -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr>
                            <td align="right" style="text-align: left">
                                &nbsp;&nbsp;&nbsp; <a href="Cns_Turnos.aspx">
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
                <!-- SPACE -->
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <!-- DATA RESUMEN -->
                <td>
                    <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                        <tr class="formularioTitulo">
                            <td style="width: 5%;" rowspan="2"></td>
                            <td style="width: 10%; height: 11px">
                                <asp:Label ID="lblCodTurno" runat="server" CssClass="TextoFiltro"></asp:Label>
                            </td>
                            <td style="width: 20%; height: 11px">
                                <asp:Label ID="lblDetCodTurno" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                            </td>
                            <td style="width: 10%">
                                <asp:Label ID="lblFchHoraIni" runat="server" CssClass="TextoFiltro"></asp:Label>
                            </td>
                            <td style="width: 20%"><asp:Label ID="lblDetFchHoraIni" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                            </td>
                            <td style="width: 10%"><asp:Label ID="lblFchHoraFin" runat="server" CssClass="TextoFiltro"></asp:Label>
                            </td>
                            <td style="width: 20%"><asp:Label ID="lblDetFchHoraFin" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="100%"></asp:Label>
                            </td>
                            <td>
                            </td>                            
                        </tr>
                        <tr class="formularioTitulo">
                            <td>
                                <asp:Label ID="lblEquipo" runat="server" CssClass="TextoFiltro"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDetEquipo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                            </td>
                            <td><asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                            </td>
                            <td><asp:Label ID="lblDetUsuario" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                                    Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                            </td>
                            <td colspan="3">
                                <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                    style="cursor: hand;"><b>
                                        <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                    </b></a>
                            </td>
                        </tr>                                           
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="200px"></asp:Label>
                    <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divData" runat="server">
                        <asp:ScriptManager ID="ScriptManager2" runat="server">
                        </asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdTurno" runat="server"/>
                                <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                    <tr>
                                        <td class="SpacingGrid">
                                        </td>
                                        <td>
                                            &nbsp;
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="height: 11px; width: 33%">
                                                        <asp:Label ID="lblMoneda1" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 11px; width: 33%">
                                                        <asp:Label ID="lblMoneda2" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 11px; width: 33%">
                                                        <asp:Label ID="lblMoneda3" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda1" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda1_RowDataBound" OnRowCommand="grvMoneda1_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                            <HeaderStyle BorderStyle="None" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda2" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda2_RowDataBound" OnRowCommand="grvMoneda2_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda3" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda3_RowDataBound" OnRowCommand="grvMoneda3_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5%">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda4" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda5" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda6" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda4" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda4_RowDataBound" OnRowCommand="grvMoneda4_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda5" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda5_RowDataBound" OnRowCommand="grvMoneda5_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda6" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda6_RowDataBound" OnRowCommand="grvMoneda6_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5%">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda7" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda8" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda9" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda7" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda7_RowDataBound" OnRowCommand="grvMoneda7_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda8" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda8_RowDataBound" OnRowCommand="grvMoneda8_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda9" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda9_RowDataBound" OnRowCommand="grvMoneda9_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5%">
                                                        &nbsp; &nbsp; &nbsp;
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 11px">
                                                        <asp:Label ID="lblMoneda10" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 11px">
                                                        <asp:Label ID="lblMoneda11" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="height: 11px">
                                                        <asp:Label ID="lblMoneda12" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda10" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda10_RowDataBound" OnRowCommand="grvMoneda10_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda11" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda11_RowDataBound" OnRowCommand="grvMoneda11_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda12" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda12_RowDataBound" OnRowCommand="grvMoneda12_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 2%">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="height: 2%">
                                                    </td>
                                                    <td style="height: 2%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblMoneda13" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda14" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMoneda15" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda13" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda13_RowDataBound" OnRowCommand="grvMoneda13_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda14" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda14_RowDataBound" OnRowCommand="grvMoneda14_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td>
                                                        <asp:GridView ID="grvMoneda15" runat="server" AutoGenerateColumns="False" Width="80%"
                                                            OnRowDataBound="grvMoneda15_RowDataBound" OnRowCommand="grvMoneda15_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField ShowHeader="False">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                            CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                                    </ItemTemplate>
                                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <ItemStyle Font-Overline="False" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="cantidad" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="monto" ShowHeader="False">
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 5%">
                                                        &nbsp; &nbsp;
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                    <td style="height: 5%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="SpacingGrid">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda1" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda2" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda3" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda4" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda5" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda6" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda7" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda8" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda9" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda10" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda11" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda12" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda13" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda14" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvMoneda15" EventName="RowCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
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
            <tr>
                <td>
                    <uc2:PiePagina ID="PiePagina1" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <uc3:CnsDetTurno ID="CnsDetTurno" runat="server" />
    </form>
</body>
</html>
