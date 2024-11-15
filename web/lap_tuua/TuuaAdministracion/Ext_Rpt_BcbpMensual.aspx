<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ext_Rpt_BcbpMensual.aspx.cs"
    Inherits="Ext_Rpt_BcbpMensual" ResponseEncoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte de BP Mensual (Usados / Rehabilitados)</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_ticketrehab" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">

        function onOk() { }
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

        function validarCampos() {
            document.getElementById('lblMensajeError').innerHTML = "";

            document.getElementById('lblMensajeErrorDataUsado').innerHTML = "";
            document.getElementById('lblMensajeErrorDataRehab').innerHTML = "";

            document.getElementById('lblTitleUsado').innerHTML = "";
            document.getElementById('lblTitleRehab').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtFechaDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtFechaHasta').value,
                                   document.getElementById('txtHoraHasta').value
                                   )) {
                return true;
            } else {
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }
        }
        function cleanGrilla() {
            if (document.getElementById("grvResumenUsado") != null) {
                document.getElementById("grvResumenUsado").style.display = "none";
            }
            if (document.getElementById("grvResumenRehab") != null) {
                document.getElementById("grvResumenRehab").style.display = "none";
            }
            if (document.getElementById("grvResumenGeneral") != null) {
                document.getElementById("grvResumenGeneral").style.display = "none";
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 24px;
        }
        .style2
        {
            height: 212px;
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
                            <td align="left" style="height: 30px; width: 85%;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="6">
                                        </td>
                                        <td colspan="15" style="height: 20px;">
                                            <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--1--%>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <%--2--%>
                                        <td rowspan="2" style="width: 72px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaDesde" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--3--%>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <%--4--%>
                                        <td rowspan="2" style="width: 60px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraDesde0" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--5--%>
                                        <td style="width: 1%;">
                                        </td>
                                        <%--6--%>
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblFchVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <%--7--%>
                                        <td rowspan="2" style="width: 72px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaVuelo" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFchVuelo0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--8--%>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imgbtnFchVuelo" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <%--9--%>
                                        <td style="width: 1%;">
                                            &nbsp;
                                        </td>
                                        <td style="width: 1%;">
                                            <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <%--10--%>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <%--11--%>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoVuelo" runat="server" Width="120px" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <%--12--%>
                                        <td style="width: 1%;">
                                        </td>
                                        <%--13--%>
                                        <td>
                                            <asp:Label ID="lblTipoPersona" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <%--14--%>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="120px" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <%--15--%>
                                        <%--16--%>
                                        <%--17--%>
                                        <%--18--%>
                                        <%--<td>
                                        </td>--%>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--1--%>
                                        <td class="style1">
                                            <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <%--2--%>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechaHasta" runat="server" Width="72px" CssClass="textboxFecha"
                                                            Height="16px" MaxLength="10" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            onblur="JavaScript: ValidarFecha(this,'lblMensajeError');" onfocus="this.blur();"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaFin0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--3--%>
                                        <td class="style1">
                                            <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <%--4--%>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;<asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8"
                                                            onBlur="JavaScript:CheckTime(this)" onkeypress="JavaScript: Tecla('Time');" ReadOnly="false"
                                                            Width="56px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblHoraFin0" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <%--5--%>
                                        <td class="style1">
                                        </td>
                                        <%--6--%>
                                        <td class="style1">
                                            <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <%--7--%>
                                        <td colspan="2" class="style1">
                                            <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('NumeroyLetra');"
                                                Width="100px"></asp:TextBox>
                                        </td>
                                        <%--9--%>
                                        <td class="style1">
                                            &nbsp;
                                        </td>
                                        <td class="style1">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <%--10--%>
                                        <td class="style1">
                                            &nbsp;
                                        </td>
                                        <%--11--%>
                                        <td class="style1">
                                            <asp:DropDownList ID="ddlAerolinea" runat="server" Width="100%" CssClass="combo2">
                                            </asp:DropDownList>
                                        </td>
                                        <%--12--%>
                                        <td class="style1">
                                        </td>
                                        <%--13--%>
                                        <td class="style1">
                                            <asp:Label ID="lblTipoTrasbordo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <%--14--%>
                                        <td class="style1">
                                            <asp:DropDownList ID="ddlTipoTrasbordo" runat="server" Width="120px" CssClass="combo2">
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
                                </table>
                            </td>
                            <td align="center">
                                <b>
                                    <asp:LinkButton ID="lbExportar" runat="server" OnClientClick="return validarExcel();"
                                        OnClick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <br />
                                        &nbsp;
                                        <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClientClick="return validarCampos()"
                                            OnClick="btnConsultar_Click" />
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
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td valign="top" style="width: 45%;">
                                                        <asp:Label ID="lblTitleUsado" runat="server" CssClass="titulosecundario"></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                    <td valign="top" style="width: 45%;">
                                                        <asp:Label ID="lblTitleRehab" runat="server" CssClass="titulosecundario"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top" class="style2">
                                                        <div>
                                                            <cc2:PagingGridView ID="grvResumenUsado" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                                GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenUsado_RowCreated"
                                                                  OnRowDataBound="grvResumenUsado_RowDataBound">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" />
                                                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" />
                                                                    <asp:BoundField DataField="Dsc_Vuelo" HeaderText="Tipo Vuelo" />
                                                                    <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Pasajero" />
                                                                    <asp:BoundField DataField="Dsc_Trasbordo" HeaderText="Tipo Trasbordo" />
                                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                                        NullDisplayText="0">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </cc2:PagingGridView>
                                                        </div>
                                                        <asp:Label ID="lblMensajeErrorDataUsado" runat="server" CssClass="msgMensaje"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="lblTotalRows" runat="server" />
                                                        <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                                    </td>
                                                    <td valign="top" class="style2">
                                                    </td>
                                                    <td valign="top" class="style2">
                                                        <div>
                                                            <cc2:PagingGridView ID="grvResumenRehab" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                                GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenRehab_RowCreated"
                                                                  OnRowDataBound="grvResumenRehab_RowDataBound">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" />
                                                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" />
                                                                    <asp:BoundField DataField="Dsc_Vuelo" HeaderText="Tipo Vuelo" />
                                                                    <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Pasajero" />
                                                                    <asp:BoundField DataField="Dsc_Trasbordo" HeaderText="Tipo Trasbordo" />
                                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                                        NullDisplayText="0">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </cc2:PagingGridView>
                                                        </div>
                                                        <asp:Label ID="lblMensajeErrorDataRehab" runat="server" CssClass="msgMensaje"></asp:Label>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTitleAnul" runat="server" CssClass="titulosecundario"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style2" valign="top">
                                                        <div>
                                                            <cc2:PagingGridView ID="grvResumenAnul" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                                GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenAnul_RowCreated"
                                                                  OnRowDataBound="grvResumenAnul_RowDataBound">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" />
                                                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" />
                                                                    <asp:BoundField DataField="Dsc_Vuelo" HeaderText="Tipo Vuelo" />
                                                                    <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Pasajero" />
                                                                    <asp:BoundField DataField="Dsc_Trasbordo" HeaderText="Tipo Trasbordo" />
                                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                                        NullDisplayText="0">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </cc2:PagingGridView>
                                                        </div>
                                                        <asp:Label ID="lblMensajeErrorDataAnul" runat="server" CssClass="msgMensaje"></asp:Label>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        <div>
                                                            <cc2:PagingGridView ID="grvResumenGeneral" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                                GridLines="Both" GroupingDepth="0" HorizontalAlign="Center" OnRowCreated="grvResumenGeneral_RowCreated"
                                                                OnRowDataBound="grvResumenGeneral_RowDataBound"   Width="40%">
                                                                <PagerSettings Mode="NumericFirstLast" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" />
                                                                    <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" />
                                                                    <asp:BoundField DataField="Dsc_Vuelo" HeaderText="Tipo Vuelo" />
                                                                    <asp:BoundField DataField="Tip_Pasajero" HeaderText="Tipo Pasajero" />
                                                                    <asp:BoundField DataField="Dsc_Trasbordo" HeaderText="Tipo Trasbordo" />
                                                                    <asp:BoundField DataField="Tipo" HeaderText="" />
                                                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                                        NullDisplayText="0">
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <FooterStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Black" HorizontalAlign="Right" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </cc2:PagingGridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
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
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"
        PopupButtonID="imgbtnCalendarDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta"
        PopupButtonID="imgbtnCalendarHasta">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaVuelo"
        PopupButtonID="imgbtnFchVuelo">
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
    </form>
</body>
</html>
