<%@ Page Language="C#" MasterPageFile="~/tuua.Master" AutoEventWireup="true" CodeBehind="Rpt_BcbpDiario.aspx.cs"
    Inherits="LAP.Tuua.Web.pages.Rpt_BcbpDiario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="../UserControl/CnsDetBoardingPass.ascx" TagName="CnsDetBoardingPass"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecera" runat="server">
    <title>LAP - Extranet - Reporte BP Diario</title>
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
            document.getElementById('ctl00_cphContenido_lblTotalUsado').innerHTML = "";
            document.getElementById('ctl00_cphContenido_lblMensajeErrorDataRehab').innerHTML = "";
            document.getElementById('ctl00_cphContenido_lblTotalRehab').innerHTML = "";

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
            if (document.getElementById("ctl00_cphContenido_grvBoardingUsado") != null) {
                document.getElementById("ctl00_cphContenido_grvBoardingUsado").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvResumenUsado") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenUsado").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvBoardingRehab") != null) {
                document.getElementById("ctl00_cphContenido_grvBoardingRehab").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvResumenRehab") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenRehab").style.display = "none";
            }
            if (document.getElementById("ctl00_cphContenido_grvResumenGeneral") != null) {
                document.getElementById("ctl00_cphContenido_grvResumenGeneral").style.display = "none";
            }
        }
    </script>

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
                                    <td colspan="17" style="height: 20px;">
                                        <asp:Label ID="lblFechaTitulo" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                    </td>
                                    <td style="width: 20px;" rowspan="5">
                                    </td>
                                </tr>
                                <tr>
                                    <%--1--%>
                                    <td style="width: 4%;">
                                        <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                        <asp:Label ID="lblFchVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td>
                                        <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
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
                                        <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
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
                                    <td>
                                        <asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <%--11--%>
                                    <td>
                                        <asp:TextBox ID="txtNumAsiento" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('NumeroyLetra');"
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <%--12--%>
                                    <td>
                                    </td>
                                    <%--13--%>
                                    <td>
                                        <asp:Label ID="lblNomPasajero" runat="server" CssClass="TextoFiltro">Pasajero:</asp:Label>
                                    </td>
                                    <%--14--%>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtNomPasajero" runat="server" Width="294px" CssClass="textbox"
                                            onkeypress="JavaScript: Tecla('Alphanumeric');" onblur="gDescripcion(this)" Height="16px"
                                            MaxLength="50"></asp:TextBox>
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
                        <asp:LinkButton ID="lbExportar" runat="server" 
                                OnClientClick="return validarExcel();" onclick="lbExportar_Click">[Exportar a Excel]</asp:LinkButton></b>
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
                                        <asp:Label ID="lblTitleUsado" runat="server" CssClass="titulosecundario"></asp:Label>
                                        <cc2:PagingGridView ID="grvBoardingUsado" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Both" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grilla" GroupingDepth="0"  OnPageIndexChanging="grvBoardingUsado_PageIndexChanging"
                                            OnRowCommand="grvBoardingUsado_RowCommand"
                                            OnSorting="grvBoardingUsado_Sorting">
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nro. Correlativo" SortExpression="Num_Secuencial_Bcbp">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Num_Secuencial_Bcbp" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ShowBoarding" Text='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Correlativo" />
                                                <asp:BoundField HeaderText="Tipo Documento" DataField="Tip_Documento" SortExpression="Tip_Documento" />
                                                <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                <asp:BoundField HeaderText="Nro. Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" SortExpression="Dsc_Vuelo" />
                                                <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" SortExpression="Tip_Pasajero" />
                                                <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" SortExpression="Dsc_Trasbordo" />
                                                <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Tip_Estado" SortExpression="Dsc_Tip_Estado" />
                                                <asp:BoundField HeaderText="Nro. Boarding" DataField="Nro_Boarding" SortExpression="Nro_Boarding" />
                                                <asp:BoundField HeaderText="ETicket" DataField="Cod_Eticket" SortExpression="Cod_Eticket" />
                                            </Columns>
                                        </cc2:PagingGridView>
                                        <asp:Label ID="lblMensajeErrorDataUsado" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblTotalUsado" runat="server" CssClass="TextoCampo"></asp:Label>
                                        
                                        <asp:HiddenField ID="lblTotalRows" runat="server" />
                                        <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                        <div>
                                            
                                            <cc2:PagingGridView ID="grvResumenUsado" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="5" HorizontalAlign="Center"
                                                OnRowCreated="grvResumenUsado_RowCreated" 
                                                ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                    <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" />
                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblTitleRehab" runat="server" CssClass="titulosecundario"></asp:Label>
                                        <cc2:PagingGridView ID="grvBoardingRehab" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Both" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grilla" GroupingDepth="0"  OnPageIndexChanging="grvBoardingRehab_PageIndexChanging"
                                            OnRowCommand="grvBoardingRehab_RowCommand"
                                            OnSorting="grvBoardingRehab_Sorting">
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nro. Correlativo" SortExpression="Num_Secuencial_Bcbp">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Num_Secuencial_Bcbp" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ShowBoarding" Text='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Correlativo" />
                                                <asp:BoundField HeaderText="Tipo Documento" DataField="Tip_Documento" SortExpression="Tip_Documento" />
                                                <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                <asp:BoundField HeaderText="Nro. Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Rehabilitación" SortExpression="Fch_Rehab">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaRehab" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Rehab"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Motivo" DataField="Dsc_Causal_Rehab" />
                                                <asp:BoundField HeaderText="Nro. Rehabilitación" DataField="Num_Proceso_Rehab"
                                                    SortExpression="Num_Proceso_Rehab" />
                                                <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" SortExpression="Dsc_Vuelo" />
                                                <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" SortExpression="Tip_Pasajero" />
                                                <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" SortExpression="Dsc_Trasbordo" />
                                                <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Tip_Estado" SortExpression="Dsc_Tip_Estado" />
                                                <asp:BoundField HeaderText="Nro. Boarding" DataField="Nro_Boarding" SortExpression="Nro_Boarding" />
                                                <asp:BoundField HeaderText="ETicket" DataField="Cod_Eticket" SortExpression="Cod_Eticket" />
                                            </Columns>
                                        </cc2:PagingGridView>
                                        <asp:Label ID="lblMensajeErrorDataRehab" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblTotalRehab" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <asp:Label ID="Label4" runat="server" CssClass="TextoCampo" ForeColor="White"></asp:Label>
                                        <asp:Label ID="Label5" runat="server" CssClass="TextoCampo" ForeColor="White"></asp:Label>
                                        <div>
                                            <cc2:PagingGridView ID="grvResumenRehab" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="5" HorizontalAlign="Center"
                                                OnRowCreated="grvResumenRehab_RowCreated"  ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                    <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" />
                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                        </div>
                                        <br />
                                        <asp:Label ID="lblTitleAnul" runat="server" CssClass="titulosecundario"></asp:Label>
                                        <cc2:PagingGridView ID="grvBoardingAnul" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Both" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grilla" GroupingDepth="0"  OnPageIndexChanging="grvBoardingAnul_PageIndexChanging"
                                            OnRowCommand="grvBoardingAnul_RowCommand"
                                            OnSorting="grvBoardingAnul_Sorting">
                                            <SelectedRowStyle CssClass="grillaFila" />
                                            <PagerStyle CssClass="grillaPaginacion" />
                                            <HeaderStyle CssClass="grillaCabecera" />
                                            <AlternatingRowStyle CssClass="grillaFila" />
                                            <PagerSettings Mode="NumericFirstLast" />
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nro. Correlativo" SortExpression="Num_Secuencial_Bcbp">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Num_Secuencial_Bcbp" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                            CommandName="ShowBoarding" Text='<%# Eval("Num_Secuencial_Bcbp") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Secuencial" DataField="Correlativo" SortExpression="Correlativo" />
                                                <asp:BoundField HeaderText="Tipo Documento" DataField="Tip_Documento" SortExpression="Tip_Documento" />
                                                <asp:BoundField HeaderText="Aerolínea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                <asp:BoundField HeaderText="Nro. Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Anulación" SortExpression="Fch_Anul">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaAnul" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Anul"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Motivo" DataField="Dsc_Motivo" />
                                                <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" SortExpression="Dsc_Vuelo" />
                                                <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" SortExpression="Tip_Pasajero" />
                                                <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" SortExpression="Dsc_Trasbordo" />
                                                <asp:BoundField HeaderText="Estado Actual" DataField="Dsc_Tip_Estado" SortExpression="Dsc_Tip_Estado" />
                                                <asp:BoundField HeaderText="Nro. Boarding" DataField="Nro_Boarding" SortExpression="Nro_Boarding" />
                                                <asp:BoundField HeaderText="ETicket" DataField="Cod_Eticket" SortExpression="Cod_Eticket" />
                                            </Columns>
                                        </cc2:PagingGridView>
                                        <asp:Label ID="lblMensajeErrorDataAnul" runat="server" CssClass="msgMensaje"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblTotalAnul" runat="server" CssClass="TextoCampo"></asp:Label>
                                        <div>
                                            <cc2:PagingGridView ID="grvResumenAnul" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="5" HorizontalAlign="Center"
                                                OnRowCreated="grvResumenAnul_RowCreated"  ShowFooter="true">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                    <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" />
                                                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" NullDisplayText="0" ItemStyle-HorizontalAlign="Right" />
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                        </div>
                                        <br />
                                        <br />
                                        <div>
                                            <cc2:PagingGridView ID="grvResumenGeneral" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="0" HorizontalAlign="Center"
                                                 ShowFooter="True" OnRowCreated="grvResumenGeneral_RowCreated">
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Fecha Vuelo" DataField="Fch_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Vuelo" DataField="Dsc_Vuelo" />
                                                    <asp:BoundField HeaderText="Tipo Pasajero" DataField="Tip_Pasajero" />
                                                    <asp:BoundField HeaderText="Tipo Trasbordo" DataField="Dsc_Trasbordo" />
                                                    <asp:BoundField HeaderText="" DataField="Tipo" />
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
    <uc2:CnsDetBoardingPass ID="CnsDetBoardingPass1" runat="server" />
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
