<%@ Page Language="C#" MasterPageFile="~/tuua.Master" AutoEventWireup="true" CodeBehind="Rpt_BcbpMensual.aspx.cs"
    Inherits="LAP.Tuua.Web.pages.Rpt_BcbpMensual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
    <title>LAP - Extranet - Reporte BP Mensual</title>
    <!-- #INCLUDE file="../javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="../javascript/KeyPress.js" -->
    <!-- #INCLUDE file="../javascript/Functions.js" -->
    <!-- #INCLUDE file="../javascript/validarteclaF5.js" -->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />

    <script language="JavaScript" type="text/javascript">
        //kinzi
        function validarExcel() {
            var numRegistros = document.getElementById("ctl00_cphContenido_lblTotalRows").value;
            var maxRegistros = document.getElementById("ctl00_cphContenido_lblMaxRegistros").value;
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
            document.getElementById('ctl00_cphContenido_lblMensajeError').innerHTML = "";

            document.getElementById('ctl00_cphContenido_lblMensajeErrorDataUsado').innerHTML = "";
            document.getElementById('ctl00_cphContenido_lblMensajeErrorDataRehab').innerHTML = "";

            document.getElementById('ctl00_cphContenido_lblTitleUsado').innerHTML = "";
            document.getElementById('ctl00_cphContenido_lblTitleRehab').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('ctl00_cphContenido_txtFechaDesde').value,
                                   document.getElementById('ctl00_cphContenido_txtHoraDesde').value,
                                   document.getElementById('ctl00_cphContenido_txtFechaHasta').value,
                                   document.getElementById('ctl00_cphContenido_txtHoraHasta').value
                                   )) {
                return true;
            } else {
                document.getElementById('ctl00_cphContenido_lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }
        }
        function cleanGrilla() {
            if (document.getElementById("ctl00_cphContenido_grvResumenUsado") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenUsado").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvResumenRehab") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenRehab").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvResumenGeneral") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenGeneral").style.display = "none";
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 184px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" align="center" class="TamanoTabla">
        <tr>
            <!-- SPACE -->
            <td>
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
        </tr>
        <tr class="formularioTitulo2">
            <td>
                <!-- FILTER ZONE -->
                <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                    <tr>
                        <td colspan="2">
                            <table cellpadding="0" cellspacing="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 20px;">
                                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblReporteTitulo" runat="server" CssClass="titulosecundario"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblCompania" runat="server" CssClass="titulosecundario"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="height: 30px; width: 85%;">
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <!-- TITLE -->
                                    <td style="width: 20px;" rowspan="5">
                                    </td>
                                    <td colspan="14" style="height: 20px;">
                                        <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px;" rowspan="5">
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
                                    </td>
                                    <%--10--%>
                                    <td class="style1" align="right">
                                        <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <%--11--%>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoVuelo" runat="server" Width="120px" CssClass="combo2">
                                        </asp:DropDownList>
                                    </td>
                                    <%--12--%>
                                    <%--13--%>
                                    <%--14--%>
                                    <%--15--%>
                                    <td style="width: 1%;">
                                    </td>
                                    <%--16--%>
                                    <td>
                                        <asp:Label ID="lblTipoTrasbordo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <%--17--%>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoTrasbordo" runat="server" Width="120px" CssClass="combo2">
                                        </asp:DropDownList>
                                    </td>
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
                                    <td>
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
                                    <td>
                                        <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                            Width="22px" Height="22px" />
                                    </td>
                                    <%--4--%>
                                    <td rowspan="2">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                        onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
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
                                    <td>
                                    </td>
                                    <%--6--%>
                                    <td>
                                        <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <%--7--%>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('NumeroyLetra');"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <%--9--%>
                                    <td>
                                    </td>
                                    <%--10--%>
                                    <td class="style1" align="right">
                                        <asp:Label ID="lblTipoPersona" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                    <%--11--%>
                                    <td>
                                        <asp:DropDownList ID="ddlTipoPersona" runat="server" Width="120px" CssClass="combo2">
                                        </asp:DropDownList>
                                    </td>
                                    <%--12--%>
                                    <%--13--%>
                                    <%--14--%>
                                    <td colspan="3">
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
                                    <%--<div id="progressBackgroundFilter">
                                    </div>--%>
                                    <div id="processMessage">
                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                        <br />
                                        <img alt="Loading" src="../Imagenes/ajax-loader.gif" />
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
        <!-- DATA -->
        <tr>
            <td>
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
                                                <td valign="top">
                                                    <div>
                                                        <cc2:PagingGridView ID="grvResumenUsado" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                            GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenUsado_RowCreated"
                                                            VirtualItemCount="-1" OnRowDataBound="grvResumenUsado_RowDataBound">
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
                                                <td valign="top">
                                                    &nbsp;
                                                </td>
                                                <td valign="top">
                                                    <div>
                                                        <cc2:PagingGridView ID="grvResumenRehab" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                            GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenRehab_RowCreated"
                                                            VirtualItemCount="-1" OnRowDataBound="grvResumenRehab_RowDataBound">
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
                                                <td valign="top">
                                                    <div>
                                                        <cc2:PagingGridView ID="grvResumenAnul" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                                                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grillaShort"
                                                            GridLines="Both" GroupingDepth="5" HorizontalAlign="Center" OnRowCreated="grvResumenAnul_RowCreated"
                                                            VirtualItemCount="-1" OnRowDataBound="grvResumenAnul_RowDataBound">
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
                                                        <cc2:PagingGridView ID="grvResumenGeneral" runat="server" AutoGenerateColumns="False"
                                                            AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="0" HorizontalAlign="Center"
                                                            VirtualItemCount="-1" OnRowCreated="grvResumenGeneral_RowCreated" OnRowDataBound="grvResumenGeneral_RowDataBound"
                                                            Width="40%">
                                                            <PagerSettings Mode="NumericFirstLast" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                                <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" />
                                                                <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                                <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" />
                                                                <asp:BoundField DataField="Tipo" HeaderText="" />
                                                                <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
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
                                        <%--<div id="progressBackgroundFilter">
                                        </div>--%>
                                        <div id="processMessage">
                                            &nbsp;&nbsp;&nbsp;Procesando...<br />
                                            <br />
                                            <img alt="Loading" src="../Imagenes/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </td>
                        <td class="SpacingGrid">
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
    </table>
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
</asp:Content>
