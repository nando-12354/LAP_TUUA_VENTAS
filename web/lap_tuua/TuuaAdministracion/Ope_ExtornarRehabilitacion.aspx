<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_ExtornarRehabilitacion.aspx.cs"
    Inherits="Ope_ExtornarRehabilitacion" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Extornar Rehabilitacion</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript" language="JavaScript">
    
        function CalcularVenta() {
            var frm = document.forms[0];
            for (i=0;i<frm.elements.length;i++) {
                if (frm.elements[i].type == "checkbox") {
                    if(frm.elements[i].checked)
                    frm.elements[i].checked=false;
                    else frm.elements[i].checked=true;
                }
            }
        }
        function CheckTicket(){
        document.forms[0].ibtnAgregar.Enabled=false;
        }
        function CheckFiltro(){
        document.forms[0].ibtnAgregar.Enabled=true;
        }
        
        function confirmacionLlamada()
		{
		   if (confirm("Está seguro de realizar esta operacion.")) {
		      accionSave = true;
		      return true;
		   }
		   else {
		          accionSave = false;
		          return false;
		        }
		}
		
		 var accionSave = false;
        function beginRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnExtornar').disabled = true;
                    document.body.style.cursor = 'wait';

                }
            }
        }

        function endRequest(sender, args)
        {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnExtornar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                }
            }
        }      	
    </script>

    <style type="text/css">
        .style1
        {
            height: 13px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
    </asp:ScriptManager>
    <div style="background-image: url(Imagenes/back.gif)">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="right" style="text-align: left">
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btnExtornar" runat="server" CssClass="Boton" OnClick="btnExtornar_Click"
                        OnClientClick="return confirmacionLlamada()" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="EspacioSubTablaPrincipal">
                        &nbsp;
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                    <tr>
                                        <td class="SpacingGrid" style="height: 115px">
                                        </td>
                                        <td class="CenterGrid" style="height: 115px">
                                            <table>
                                                <tr>
                                                    <td class="style3" colspan="3">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3" colspan="8">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:RadioButton ID="rbTicket" runat="server" CssClass="TextoEtiqueta" Checked="True"
                                                            GroupName="extorno" OnCheckedChanged="rbTicket_CheckedChanged" AutoPostBack="True" />
                                                    </td>
                                                    <td colspan="4">
                                                        <asp:TextBox ID="txtNumTicket" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="txtNumTicket_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtNumTicket">
                                                        </cc1:FilteredTextBoxExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:ImageButton ID="ibtnAgregar" runat="server" ImageUrl="~/Imagenes/Add.bmp" OnClick="ibtnAgregar_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="3">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblErrorTicket" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                    </td>
                                                    <td colspan="3">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style1" colspan="6" style="text-align: left;">
                                                        <asp:RadioButton ID="rbFiltro" runat="server" CssClass="TextoEtiqueta" GroupName="extorno"
                                                            OnCheckedChanged="rbFiltro_CheckedChanged" AutoPostBack="True" />
                                                    </td>
                                                    <td colspan="3">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style1" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style1" style="text-align: left;">
                                                        <fieldset style="height: 58px; width: 258px;">
                                                            <legend>
                                                                <asp:Label ID="Label2" runat="server" CssClass="TextoFiltro"> Rango de Tickets</asp:Label>
                                                            </legend>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="lblRango" runat="server" CssClass="TextoEtiqueta" Text="Del Nro:"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txtNroIni" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txtNroIni_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtNroIni">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 100px">
                                                                        <asp:Label ID="lblAlNro" runat="server" CssClass="TextoEtiqueta" Text="Al Nro:"></asp:Label>
                                                                    </td>
                                                                    <td style="width: 100px">
                                                                        <asp:TextBox ID="txtNroFin" runat="server" CssClass="textbox" MaxLength="12"></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="txtNroFin_FilteredTextBoxExtender" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtNroFin">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <fieldset style="width: 383px; height: 66px; margin-top: 0px">
                                                            <legend>
                                                                <asp:Label ID="lblFecha" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </legend>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDesde" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtDesde" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtDesde_CalendarExtender" runat="server" Enabled="True"
                                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalDesde" TargetControlID="txtDesde">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imbCalDesde" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                            Width="22px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFechaDesde" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHoraDesde" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="valHoraDesde" runat="server" AutoComplete="False" ClearMaskOnLostFocus="False"
                                                                            CultureName="es-ES" Mask="99:99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtHoraDesde"
                                                                            UserTimeFormat="none">
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblHoraDesde" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblHasta" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHasta" runat="server" BackColor="#E4E2DC" CssClass="textbox"
                                                                            onfocus="this.blur();" onkeypress="JavaScript: window.event.keyCode = 0;" Width="72px"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="txtHasta_CalendarExtender" runat="server" Enabled="True"
                                                                            Format="dd/MM/yyyy" PopupButtonID="imbCalHasta" TargetControlID="txtHasta">
                                                                        </cc1:CalendarExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imbCalHasta" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                                            Width="22px" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblFechaHasta" runat="server" CssClass="TextoFiltro" Text="(dd/mm/yyyy)"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtHoraHasta" runat="server" CssClass="textbox" MaxLength="8" onBlur="JavaScript:CheckTime(this)"
                                                                            onkeypress="JavaScript: Tecla('Time');" ReadOnly="false" Width="56px"></asp:TextBox>
                                                                        <cc1:MaskedEditExtender ID="valHoraHasta" runat="server" AutoComplete="False" ClearMaskOnLostFocus="False"
                                                                            CultureName="es-ES" Mask="99:99:99" MaskType="Time" PromptCharacter="_" TargetControlID="txtHoraHasta"
                                                                            UserTimeFormat="none"  >
                                                                        </cc1:MaskedEditExtender>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblHoraHasta" runat="server" CssClass="TextoFiltro" Text="(hh:mm:ss)"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="2">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="1" style="width: 331px; text-align: left; height: 13px;">
                                                        <table>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 100px">
                                                                    <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" MaxLength="10"
                                                                        Width="152px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="txtNumVuelo_FilteredTextBoxExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtNumVuelo" ValidChars="abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                        FilterType="Numbers" TargetControlID="txtNroIni">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="width: 100px">
                                                                    <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="~/Imagenes/Search.bmp"
                                                                        OnClick="ibtnBuscar_Click" Width="16px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style1" colspan="6" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                    <td class="alineaderecha" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="1" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid" style="height: 115px">
                                                        &nbsp;
                                                    </td>
                                                    <td class="CenterGrid" colspan="9" align="left">
                                                        <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                                        <div style="height: 170px; overflow: auto;">
                                                            <asp:GridView ID="grvTicket" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                                CellPadding="3" CssClass="grilla" GridLines="Vertical" OnSorting="grvTicket_Sorting"
                                                                Width="98%">
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <Columns>
                                                                    <asp:BoundField DataField="Dsc_Tipo_Ticket" HeaderText="Tipo Ticket" SortExpression="Dsc_Tipo_Ticket" />
                                                                    <asp:BoundField DataField="Dsc_Compania" HeaderText="Compania" SortExpression="Dsc_Compania" />
                                                                    <asp:BoundField DataField="Dsc_Num_Vuelo" HeaderText="Num. Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                                                    <asp:BoundField DataField="Fch_Vuelo" HeaderText="Fecha Vuelo" SortExpression="Fch_Vuelo" />
                                                                    <asp:BoundField DataField="Cod_Numero_Ticket" HeaderText="Num. Ticket" SortExpression="Cod_Numero_Ticket" />
                                                                    <asp:BoundField DataField="FHCreacion" HeaderText="Fecha Proceso" SortExpression="FHCreacion" />
                                                                    <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado" SortExpression="Dsc_Campo" />
                                                                    <asp:TemplateField HeaderText="">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ckbRegistrar" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderTemplate>
                                                                            <asp:ImageButton ID="ibtnCheckAll" runat="server" ImageUrl="~/Imagenes/check.gif"
                                                                                OnClientClick="JavaScript:CalcularVenta();" />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                    <td class="SpacingGrid" colspan="1" style="height: 115px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="10" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" colspan="7" class="style9">
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
                                                    </td>
                                                    <td class="style9" colspan="3" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;
                                                    </td>
                                                    <td align="center" class="style9" colspan="7">
                                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                                    </td>
                                                    <td class="style9" colspan="3" style="text-align: left;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            &nbsp;
                                        </td>
                                        <td class="SpacingGrid" style="height: 115px; width: 2%;" valign="bottom">
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnExtornar" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                            DisplayAfter="100">
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
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <br />
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
        <br />
    </div>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
