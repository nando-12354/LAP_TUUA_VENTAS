<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Consultas_ConsultaTransaccionCritica, App_Web_jlql8yfo" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="UserControl/DetalleTransaccionCritica.ascx" TagName="DetalleTransaccionCritica" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Auditoria</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <meta name="keypage" content="cns_transaccioncritica" />
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <style type="text/css">
        /*html, body
        {
            height: 100%;
        }*/
    </style>

    <script language="javascript" type="text/javascript">
    <!--

        function imgPrint_onclick() {
            var sTipoOperacion = document.getElementById("ddlOperacion").value;
            var sTabla = document.getElementById("ddlTabla").value;
            var sCodModulo = document.getElementById("ddlModulo").value;
            var sCodSubModulo = document.getElementById("ddlSubModulo").value;
            var sCodUsuario = document.getElementById("ddlUsuario").value;
            var sFchDesde = document.getElementById("txtDesde").value;
            var sFchHasta = document.getElementById("txtHasta").value;
            var sHraDesde = document.getElementById("txtHoraDesde").value;
            var sHraHasta = document.getElementById("txtHoraHasta").value;
            var strModulo = document.getElementById("ddlModulo").options[document.getElementById("ddlModulo").selectedIndex].text;
            var strSubModulo = document.getElementById("ddlSubModulo").options[document.getElementById("ddlSubModulo").selectedIndex].text;
            var idOrdenacion = null;
            var idColumna = null;
            var idPaginacion = null;
            var strDscOpe = document.getElementById("ddlOperacion").options[document.getElementById("ddlOperacion").selectedIndex].text;
            var strDscUsr = document.getElementById("ddlUsuario").options[document.getElementById("ddlUsuario").selectedIndex].text;
            if (strModulo == "<Todos>")
                strModulo = "Todos";
            if (strSubModulo == "<Todos>")
                strSubModulo = "Todos";
            if (strDscOpe == "<Todos>")
                strDscOpe = "Todos";
            if (strDscUsr == "<Todos>")
                strDscUsr = "Todos";
                            
                                
            var ventimp = window.open("ReporteCNS/rptAuditoriaCritica.aspx" + "?sTipoOperacion=" + sTipoOperacion
	                + "&sTabla=" + sTabla + "&sCodModulo=" + sCodModulo
	                + "&sCodSubModulo=" + sCodSubModulo + "&sCodUsuario=" + sCodUsuario
	                + "&sFchDesde=" + sFchDesde + "&sFchHasta=" + sFchHasta
	                + "&sHraDesde=" + sHraDesde + "&sHraHasta=" + sHraHasta + "&strModulo=" + strModulo + "&strSubModulo=" + strSubModulo + "&strDscOpe=" + strDscOpe + "&strDscUsr=" + strDscUsr, "mywindow", "location=0,status=0,scrollbars=1,menubar=0,width=900,height=800");
            ventimp.focus();
        }

    // -->
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" 
            AsyncPostBackTimeout="3600">
        </asp:ScriptManager>
        <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
            <tr>
                <!-- HEADER -->
                <td>
                    <uc1:CabeceraMenu ID="CabeceraMenu1" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- TITLE -->
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                        <td colspan="16" style="height: 20px;">
                                            <asp:Label ID="lblFechaAudit" Text="Fecha" runat="server" CssClass="TextoEtiquetaBold">Fecha:</asp:Label>
                                        </td>
                                        <td style="width: 20px;" rowspan="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- FIRST ROW FILTER -->
                                        <td style="width: 4%;">
                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2" style="width: 80px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC" CssClass="textbox" Height="19px" Width="72px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rfvFchDesde" runat="server" ControlToValidate="txtDesde"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde0" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 25px;">
                                            <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td rowspan="2" style="width: 60px;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" onBlur="JavaScript:CheckTime(this)"
                                                            Height="19px" Width="60px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mee_txtHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label2" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%;">
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="lblOperacion" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:DropDownList ID="ddlOperacion" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 2%;">
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="lblModulo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 18%;">
                                            <asp:DropDownList ID="ddlModulo" runat="server" CssClass="combo2" Width="100%" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlModulo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                        <td style="width: 6%;">
                                            <asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td style="width: 18%;">
                                            <asp:DropDownList ID="ddlUsuario" runat="server" CssClass="combo2" Width="100%">
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
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- SECOND ROW FILTER -->
                                        <td>
                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;"
                                                            BackColor="#E4E2DC" CssClass="textbox" Height="19px" Width="72px"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rfvFchHasta" runat="server" ControlToValidate="txtHasta"
                                                            Display="None" ErrorMessage="Ingrese una fecha correcta" ValidationExpression="^\d{1,2}\/\d{1,2}\/\d{4}$">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td rowspan="2">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" onBlur="JavaScript:CheckTime(this)"
                                                            CssClass="textbox" Height="19px" Width="60px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="mee_txtHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="Label4" runat="server" CssClass="TextoFiltro" Text="(hh:mm)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTabla" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTabla" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSubModulo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlSubModulo" runat="server" CssClass="combo2" Width="100%">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlModulo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                        </td>
                                        <td align="right" colspan="3">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <a href="" id="lnkHabilitar" runat="server" onclick="javascript:imgPrint_onclick();"
                                                        style="cursor: hand;"><b>
                                                            <asp:Label ID="lblImprimir" runat="server">Imprimir</asp:Label>
                                                        </b></a>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnConsultar" runat="server" CssClass="Boton" OnClick="btnConsultar_Click"
                                                        Style="cursor: hand;" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel3" runat="server">
                                                <ProgressTemplate>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
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
                                </table>
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
                <td>
                    <div>
                        &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid">
                                </td>
                                <td class="CenterGrid">
                                    <div id="divData" class="divSizeCustom" style="HEIGHT: 500px">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <cc2:PagingGridView ID="grvAuditoriaPagin" runat="server" AutoGenerateColumns="False"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="True"
                                                    CssClass="grilla" AllowPaging="True" OnPageIndexChanging="grvAuditoriaPagin_PageIndexChanging"
                                                    OnRowCommand="grvAuditoriaPagin_RowCommand" OnSorting="grvAuditoriaPagin_Sorting">
                                                    <FooterStyle BackColor="Black" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Detalle" Visible="true" SortExpression="Cod_Contador">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="CodAuditoria" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="ShowDetalleAuditoria" Text='<%# Eval("Cod_Contador") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Fch_Registro" HeaderText="Fecha" SortExpression="Fch_Registro" />
                                                        <asp:BoundField DataField="Dsc_Modulo" HeaderText="Modulo" SortExpression="Dsc_Modulo"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Dsc_Proceso" HeaderText="SubModulo" SortExpression="Dsc_Proceso"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Log_Tabla_Mod" HeaderText="Tabla" SortExpression="Log_Tabla_Mod"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Dsc_Campo" HeaderText="Operación" SortExpression="Dsc_Campo"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Cta_Usuario" HeaderText="Usuario" SortExpression="Cta_Usuario"
                                                            ReadOnly="True" />
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </cc2:PagingGridView>
                                                <asp:GridView ID="grvAuditoria" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical" AllowSorting="True" CssClass="grilla"
                                                    AllowPaging="True" OnPageIndexChanging="grvAuditoria_PageIndexChanging" OnSorting="grvAuditoria_Sorting"
                                                    OnRowCommand="grvAuditoria_RowCommand">
                                                    <FooterStyle BackColor="Black" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Detalle" Visible="true" SortExpression="Cod_Contador">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="CodAuditoria" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                    CommandName="ShowDetalleAuditoria" Text='<%# Eval("Cod_Contador") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Fch_Registro" HeaderText="Fecha" SortExpression="Fch_Registro" />
                                                        <asp:BoundField DataField="Dsc_Modulo" HeaderText="Modulo" SortExpression="Dsc_Modulo"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Dsc_Proceso" HeaderText="SubModulo" SortExpression="Dsc_Proceso"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Log_Tabla_Mod" HeaderText="Tabla" SortExpression="Log_Tabla_Mod"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Dsc_Campo" HeaderText="Operación" SortExpression="Dsc_Campo"
                                                            ReadOnly="True" />
                                                        <asp:BoundField DataField="Cta_Usuario" HeaderText="Usuario" SortExpression="Cta_Usuario"
                                                            ReadOnly="True" />
                                                    </Columns>
                                                    <SelectedRowStyle CssClass="grillaFila" />
                                                    <PagerStyle CssClass="grillaPaginacion" />
                                                    <HeaderStyle CssClass="grillaCabecera" />
                                                    <AlternatingRowStyle CssClass="grillaFila" />
                                                </asp:GridView>
                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                                <br />
                                                <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                                            <ProgressTemplate>
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
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <!--Validaciones de fecha-->
    <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
        TargetControlID="txtHasta" PopupButtonID="imbCalHasta" Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
        TargetControlID="txtDesde" PopupButtonID="imbCalDesde" Format="dd/MM/yyyy">
    </cc1:CalendarExtender>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    <asp:CompareValidator ID="cvFiltroFechas" runat="server" ControlToCompare="txtHasta"
        ControlToValidate="txtDesde" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechas" runat="server" TargetControlID="cvFiltroFechas">
    </cc1:ValidatorCalloutExtender>
    <asp:CompareValidator ID="cvFiltroFechaHasta" runat="server" ControlToCompare="txtDesde"
        ControlToValidate="txtHasta" Display="None" ErrorMessage="Filtro de fechas invalido"
        Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
    <cc1:ValidatorCalloutExtender ID="vceFiltroFechaHasta" runat="server" TargetControlID="cvFiltroFechaHasta">
    </cc1:ValidatorCalloutExtender>
    <uc4:DetalleTransaccionCritica ID="DetalleAuditoria1" runat="server" />
    </form>
    <script src="javascript/resolucion.js" type="text/javascript"></script>
</body>
</html>