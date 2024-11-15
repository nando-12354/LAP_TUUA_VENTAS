<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ext_Rpt_BcbpDiario.aspx.cs"
    Inherits="Ext_Rpt_BcbpDiario" ResponseEncoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CnsDetBoarding.ascx" TagName="CnsDetBoarding" TagPrefix="uc4" %>
<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Reporte de BP Diario (Usados / Rehabilitados) </title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="rpt_ticketrehab" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="JavaScript" type="text/javascript">

        function onOk() {     }
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
            document.getElementById('lblTotalUsado').innerHTML = "";
            document.getElementById('lblMensajeErrorDataRehab').innerHTML = "";
            document.getElementById('lblTotalRehab').innerHTML = "";

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
            if (document.getElementById("grvBoardingUsado") != null) {
                document.getElementById("grvBoardingUsado").style.display = "none";
            }
            if (document.getElementById("grvResumenUsado") != null) {
                document.getElementById("grvResumenUsado").style.display = "none";
            }
            if (document.getElementById("grvBoardingRehab") != null) {
                document.getElementById("grvBoardingRehab").style.display = "none";
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
                                        &nbsp;</td>
                                    <td style="width: 1%;">
                                        <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <%--10--%>
                                    <td>
                                        &nbsp;</td>
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
                                                <td>&nbsp;<asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
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
                                        &nbsp;</td>
                                    <td class="style1">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <%--10--%>
                                    <td class="style1">
                                            &nbsp;</td>
                                    <%--11--%>
                                    <td class="style1">
                                            <asp:DropDownList ID="ddlAerolinea" runat="server" Width="100%" 
                                                CssClass="combo2">
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
                                <tr style="vertical-align: top;">
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                        <td></td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumAsiento" runat="server" CssClass="textbox" onkeypress="JavaScript: Tecla('NumeroyLetra');"
                                            Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblNomPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtNomPasajero" runat="server" Width="294px" CssClass="textbox"
                                            onkeypress="JavaScript: Tecla('Alphanumeric');"  Height="16px"
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    
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
                                        <asp:Label ID="lblTitleUsado" runat="server" CssClass="titulosecundario"></asp:Label>
                                        <cc2:PagingGridView ID="grvBoardingUsado" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Both" Width="100%" AllowPaging="True" AllowSorting="True"
                                            CssClass="grilla" GroupingDepth="0"   OnPageIndexChanging="grvBoardingUsado_PageIndexChanging"
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
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
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
                                            CssClass="grilla" GroupingDepth="0"   OnPageIndexChanging="grvBoardingRehab_PageIndexChanging"
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
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Rehabilitación" SortExpression="Fch_Rehab">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaRehab" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Rehab"))) %> '></asp:Label>
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
                                        <div>
                                            <cc2:PagingGridView ID="grvResumenRehab" runat="server" AutoGenerateColumns="False"
                                                AllowPaging="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grillaShort" GridLines="Both" GroupingDepth="5" HorizontalAlign="Center"
                                                OnRowCreated="grvResumenRehab_RowCreated"   ShowFooter="true">
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
                                            CssClass="grilla" GroupingDepth="0"   OnPageIndexChanging="grvBoardingAnul_PageIndexChanging"
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
                                                        <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Nro. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                <asp:TemplateField HeaderText="Fecha Uso" SortExpression="Fch_Uso">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaUso" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Uso"))) %> '></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fecha Anulación" SortExpression="Fch_Anul">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFechaAnul" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Anul"))) %> '></asp:Label>
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
                                                OnRowCreated="grvResumenAnul_RowCreated"   ShowFooter="true">
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
    <uc4:CnsDetBoarding ID="CnsDetBoarding1" runat="server" />
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
