<%@ page language="C#" autoeventwireup="true" inherits="Ope_VerTasaCambio, App_Web_jlql8yfo" responseencoding="utf-8" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Tasa de Cambio</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    
    <script language="javascript" type="text/javascript">
        function goDetalle() {
            //            document.location.href = "Ope_CrearTasaCambio.aspx";
            //            return false;
        }
        function confirmacionLlamada() {
            if (confirm("Está seguro de eliminar la Tasa de Cambio Programada ?")) {
                return true;
            }
            else {
                return false;
            }
        }	
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="left">
                &nbsp;&nbsp;&nbsp;<asp:Button ID="btnIngresar" runat="server" CssClass="Boton" Text="Ingresar >>" OnClick="btnIngresar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <hr class="EspacioLinea" color="#0099cc" />
            </td>
        </tr>
        <tr>
            <td>
                <div>
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:Label ID="lblTCA" runat="server" CssClass="titulosecundario"></asp:Label>
                                <hr class="EspacioLinea" color="#0099cc" />
                                <div style="width: 100%; height: 150px; overflow: auto; padding: 0px; margin: 0px;">
                                    <asp:UpdatePanel ID="UpdatePanelTCA" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorTCA" runat="server" CssClass="msgMensaje" Width="200px" Visible="False"></asp:Label>
                                            <asp:GridView ID="grvTasaCambio" runat="server" CssClass="grilla" CellPadding="3"
                                                BorderStyle="None" BorderWidth="1px" GridLines="Vertical" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grvTasaCambio_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField HeaderText="C&#243;digo" DataField="Cod_Tasa_Cambio" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Tipo de Cambio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITTipoCambio0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Campo"), Eval("Tip_Cambio")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Moneda">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITMoneda0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Moneda"), Eval("Dsc_Simbolo")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Valor Tasa" DataField="Imp_Cambio_Actual" ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Fecha De Inicio De Vigencia" DataField="Fch_Proceso"
                                                        ReadOnly="True"></asp:BoundField>
                                                    <asp:BoundField HeaderText="Usuario Modificación" DataField="Nom_Usuario" ReadOnly="True">
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                            </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <br />
                                <asp:Label ID="lblTCP" runat="server" CssClass="titulosecundario"></asp:Label><br />
                                <hr class="EspacioLinea" color="#0099cc" />
                                <div style="width: 100%; height: 100px; overflow: auto;">
                                    <asp:UpdatePanel ID="UpdatePanelTCP" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorTCP" runat="server" CssClass="msgMensaje" Width="200px" Visible="False"></asp:Label>
                                            <asp:GridView ID="grvTasaCambioProg" runat="server" CssClass="grilla" CellPadding="3"
                                                BorderStyle="None" BorderWidth="1px" GridLines="Vertical" AutoGenerateColumns="False"
                                                Width="100%" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grvTasaCambioProg_PageIndexChanging"
                                                OnRowCommand="grvTasaCambioProg_RowCommand">
                                                <Columns>
                                                    <asp:BoundField HeaderText="C&#243;digo" DataField="Cod_Tasa_Cambio" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Tipo de Cambio">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITTipoCambio0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Campo"), Eval("Tip_Cambio")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Moneda">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITMoneda0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Moneda"), Eval("Dsc_Simbolo")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Valor Tasa" DataField="Imp_Cambio_Actual" ReadOnly="True">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Fecha Programada" DataField="Fch_Programacion" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Usuario Modificación" DataField="Nom_Usuario" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEliminar" runat="server" OnClientClick="return confirmacionLlamada()"
                                                                CommandArgument='<%# Eval("Cod_Tasa_Cambio") %>' CommandName="Eliminar" ImageUrl="~/Imagenes/Delete.bmp"
                                                                Width="15" Height="13" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                            </asp:GridView>
                                            <uc3:OKMessageBox ID="omb" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <br />
                                <asp:Label ID="lblTCH" runat="server" CssClass="titulosecundario" />
                                <hr class="EspacioLinea" color="#0099cc" />
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblTipoOperacion" runat="server" CssClass="TextoFiltro" />
                                            &nbsp;<asp:DropDownList ID="ddlTipoOperacion" runat="server" Width="147px" CssClass="combo2" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFchIni" runat="server" CssClass="TextoFiltro" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtFchIni" runat="server" Width="88px" CssClass="textboxFecha" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendarDesde" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFchFin" runat="server" CssClass="TextoFiltro" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtFchFin" runat="server" Width="88px" CssClass="textboxFecha" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoEtiqueta" Text="( dd/mm/yyyy )" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnCalendarHasta" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                Width="22px" Height="22px" />
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnConsultar" runat="server" CssClass="Boton"
                                                OnClick="btnConsultar_Click" ValidationGroup="grupito" />
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
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
                                </table>
                                <hr class="EspacioLinea" color="#0099cc" />
                            </td>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                        </tr>
                        <tr>
                            <td class="SpacingGrid" style="height: 115px">
                                &nbsp;
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                                <div style="width: 100%; height: 150px; overflow: auto;">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblErrorTCH" runat="server" CssClass="msgMensaje" Width="200px"></asp:Label>
                                            <asp:GridView ID="grvTasaCambioHist" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                                CssClass="grilla" GridLines="Vertical" OnPageIndexChanging="grvTasaCambioHist_PageIndexChanging"
                                                OnSorting="grvTasaCambioHist_Sorting" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Cod_Tasa_Cambio" HeaderText="Código" ReadOnly="True" SortExpression="Cod_Tasa_Cambio">
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Tipo de Cambio" SortExpression="Dsc_Campo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITTipoCambio" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Campo"), Eval("Tip_Cambio")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Moneda" SortExpression="Dsc_Moneda">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITMoneda" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Moneda"), Eval("Dsc_Simbolo")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Imp_Valor" HeaderText="Valor Tasa" ReadOnly="True" SortExpression="Imp_Valor">
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Fch_Ini" HeaderText="Fecha De Inicio De Vigencia" ReadOnly="True"
                                                        SortExpression="Fch_Ini2"></asp:BoundField>
                                                    <asp:BoundField DataField="Fch_Fin" HeaderText="Fecha De Fin De Vigencia" ReadOnly="True"
                                                        SortExpression="Fch_Fin2"></asp:BoundField>                                                   
                                                    <asp:BoundField HeaderText="Usuario Modificación" DataField="Nom_Usuario" SortExpression="Nom_Usuario" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            <td class="SpacingGrid" style="height: 115px">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <!--Declaracion de Calendarios -->
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFchIni"
        PopupButtonID="imgbtnCalendarDesde">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFchFin"
        PopupButtonID="imgbtnCalendarHasta">
    </cc1:CalendarExtender>
    <!--Segunda Validacion de las fechas -->
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtFchFin"
        ControlToValidate="txtFchIni" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date" ValidationGroup="grupito"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtFchIni"
        ControlToValidate="txtFchFin" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date" ValidationGroup="grupito"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    </form>
</body>
</html>
