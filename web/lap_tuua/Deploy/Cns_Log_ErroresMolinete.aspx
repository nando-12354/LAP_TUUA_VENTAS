<%@ page language="C#" autoeventwireup="true" inherits="Rpt_TicketBoardingRehabilitados, App_Web_7ctknflu" responseencoding="utf-8" %>

<%@ Register Assembly="PaginGridView" Namespace="Pagin.Web.Control" TagPrefix="cc2" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register src="UserControl/CnsLogMolinete.ascx" tagname="CnsLogMolinete" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Consulta - Log Errores Molinetes</title>
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

            document.getElementById('lblMensajeErrorData').innerHTML = "";

            cleanGrilla();

            if (isValidoRangoFecha(document.getElementById('txtDesde').value,
                                   document.getElementById('txtHoraDesde').value,
                                   document.getElementById('txtHasta').value,
                                   document.getElementById('txtHoraHasta').value
                                   )) {
                return true;
            } else {
                document.getElementById('lblMensajeError').innerHTML = "Error. Rango de Fechas ingresado es inválido";
                return false;
            }
        }

        function cleanGrilla() {
            if (document.getElementById("grvErrorNoFuncional") != null) {
                document.getElementById("grvErrorNoFuncional").style.display = "none";
            }
//            if (document.getElementById("grvResumenRehab") != null) {
//                document.getElementById("grvResumenRehab").style.display = "none";
//            }
//            if (document.getElementById("grvResumenGeneral") != null) {
//                document.getElementById("grvResumenGeneral").style.display = "none";
//            }
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
                <!-- HEADER -->
                <td>
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <td>
                    <!-- FILTER ZONE -->
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 100%">
                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 16px;">
                                            &nbsp;
                                        </td>
                                        <td colspan="15" height="20">
                                            <asp:Label ID="lblTitulo" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>
                                        </td>
                                        <td style="width: 16px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td width="65">
                                            <asp:Label ID="lblDesde" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td width="80">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imbCalDesde" TargetControlID="txtDesde" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFormato" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="30" valign="top">
                                            <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td width="80">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <%--<asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="5" onKeyDown="JavaScript:controlaHoraDesde();"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px">
                                                        </asp:TextBox>--%>
                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" Mask="99:99:99" TargetControlID="txtHoraDesde"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFormatoHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td width="60">
                                            <asp:Label ID="lblTipoError" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td width="200" rowspan="2" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td height="40" valign="top">
                                                                <div style="padding-top: 3px">
                                                                    <asp:RadioButtonList ID="rbError" runat="server" RepeatDirection="Horizontal" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="rbError_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Value="1">No Funcional</asp:ListItem>
                                                                        <asp:ListItem Value="2">Funcional</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList ID="ddlTipoError" runat="server" CssClass="combo2" Width="100%">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td width="20">
                                            &nbsp;
                                        </td>
                                        <td width="55">
                                            <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td width="300">
                                            <asp:DropDownList ID="ddlAerolinea" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="20">
                                            &nbsp;
                                        </td>
                                        <td width="65">
                                            <asp:Label ID="lblFechVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td width="100">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFechVuelo" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtFechVuelo_CalendarExtender" runat="server" Enabled="True"
                                                            PopupButtonID="imgbtnFechaVuelo" TargetControlID="txtFechVuelo" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td width="28">
                                                        <asp:ImageButton ID="imgbtnFechaVuelo" ImageUrl="~/Imagenes/Calendar.bmp" runat="server"
                                                            Width="22px" Height="22px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFechaDesde1" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10">
                                            &nbsp;
                                        </td>
                                        <td rowspan="3" align="right">
                                            <asp:LinkButton ID="lbExportar" runat="server" OnClick="lbExportar_Click" OnClientClick="return validarExcel();">[Exportar a Excel]</asp:LinkButton></b>
                                            <br />
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:Button ID="Button1" runat="server" CssClass="Boton" Text="Consultar" Style="cursor: hand;"
                                                        OnClick="btnConsultar_Click" />&nbsp;&nbsp;&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
                                                <ProgressTemplate>
                                                    <div id="processMessage">
                                                        &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                        <br />
                                                        <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblHasta" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="imbCalHasta"
                                                            TargetControlID="txtHasta" Format="dd/MM/yyyy">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFormato2" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign="top">
                                            <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                Width="22px" />
                                        </td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                        <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" Mask="99:99:99" TargetControlID="txtHoraHasta"
                                                            ClearMaskOnLostFocus="False" MaskType="Time" UserTimeFormat="TwentyFourHour"
                                                            CultureName="es-ES" PromptCharacter="_" AutoComplete="False">
                                                        </cc1:MaskedEditExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblFormatoHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblError" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMolinete" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlMolinete" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                onkeypress="JavaScript: Tecla('NumeroyLetra');" Width="95%"></asp:TextBox>
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
                                            &nbsp;
                                        </td>
                                        <td height="30">
                                            <asp:Label ID="lblAsiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAsiento" runat="server" CssClass="textbox" Height="16px" MaxLength="3"
                                                onkeypress="JavaScript: Tecla('Alphanumeric');"
                                                Width="141px"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPasajero" runat="server" CssClass="textbox" Height="16px" MaxLength="10"
                                                onkeypress="JavaScript: Tecla('NumeroyLetra');" Width="98%"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoBP" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoBP" runat="server" CssClass="combo2" Width="50%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoIngreso" runat="server" CssClass="TextoFiltro"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoIngreso" runat="server" CssClass="combo2" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
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
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td class="CenterGrid">
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <cc2:PagingGridView ID="grvErrorNoFuncional" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                VirtualItemCount="-1" GroupingDepth="0" OnPageIndexChanging="grvErrorNoFuncional_PageIndexChanging"
                                                OnSorting="grvErrorNoFuncional_Sorting" 
                                                onrowcommand="grvErrorNoFuncional_RowCommand" DataKeyNames="Log_Error">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Identificador" DataField="Num_Secuencial" SortExpression="Num_Secuencial">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Fecha Error" SortExpression="Fch_Error">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaError" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Error")),Convert.ToString(Eval("Hora_Error"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Molinete" DataField="Dsc_Molinete" SortExpression="Dsc_Molinete">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Descripcion Error" DataField="Dsc_Error" SortExpression="Dsc_Error">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo Ingreso" DataField="Dsc_TipoIngreso" SortExpression="Dsc_TipoIngreso">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo BP" DataField="Tip_Boarding" SortExpression="Tip_Boarding" />
                                                    <asp:BoundField HeaderText="Aerolinea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                    <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# Eval("Fch_Vuelo").ToString().Contains("-") ? "-" : LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null)%> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Nro Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                    <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbMostrarLog" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowLog">Ver Log</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Log_Error" Visible="False" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                            
                                            
                                            <cc2:PagingGridView ID="grvErrorFuncional" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="100%" AllowPaging="True" CssClass="grilla" AllowSorting="True"
                                                VirtualItemCount="-1" GroupingDepth="0" 
                                                onpageindexchanging="grvErrorFuncional_PageIndexChanging" 
                                                onsorting="grvErrorFuncional_Sorting">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <PagerSettings Mode="NumericFirstLast" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Identificador" DataField="Num_Secuencial" SortExpression="Num_Secuencial">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Fecha Error" SortExpression="Log_Fecha_mod">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaError" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Molinete" DataField="Dsc_Molinete" SortExpression="Dsc_Molinete">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Descripcion Error" DataField="Dsc_Campo" 
                                                        SortExpression="Dsc_Campo">
                                                        <ItemStyle VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo Ingreso" DataField="Dsc_TipoIngreso" SortExpression="Dsc_TipoIngreso">
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Tipo BP" DataField="Tip_Boarding" SortExpression="Tip_Boarding" />
                                                    <asp:BoundField HeaderText="Aerolinea" DataField="Dsc_Compania" SortExpression="Dsc_Compania" />
                                                    <asp:TemplateField HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFechaVuelo" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Nro Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                                                    <asp:BoundField HeaderText="Nro Asiento" DataField="Num_Asiento" SortExpression="Num_Asiento" />
                                                    <asp:BoundField HeaderText="Pasajero" DataField="Nom_Pasajero" SortExpression="Nom_Pasajero" />
                                                    <asp:BoundField DataField="Log_Error" SortExpression="Log_Error" 
                                                        HeaderText="Log Error" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </cc2:PagingGridView>
                                            <br />
                                            <div>
                                                <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                                <asp:HiddenField ID="lblTotalRows" runat="server" />
                                                <asp:HiddenField ID="lblMaxRegistros" runat="server" />
                                            </div>
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="100">
                                        <ProgressTemplate>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
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
                            <td style="height: 11px">
                                <uc2:PiePagina ID="PiePagina1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <uc3:CnsLogMolinete ID="CnsLogMolinete1" runat="server" />
    </form>
</body>
</html>
