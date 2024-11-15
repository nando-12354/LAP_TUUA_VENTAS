<%@ page language="C#" autoeventwireup="true" inherits="Ope_ExtornoTicket, App_Web_ehzg6gwo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Extorno Ticket</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/ValidarFecha.js" -->
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="JavaScript" type="text/javascript">
        function controlaFechaDesde() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                //form1.txtFecIni.value = form1.txtFecIni.value.substring(0, form1.txtFecIni.value.length - 2);
            }
            else {

                if (form1.txtFecIni.value.length == 2) {
                    form1.txtFecIni.value = form1.txtFecIni.value + '/';
                }
                if (form1.txtFecIni.value.length == 5) {
                    form1.txtFecIni.value = form1.txtFecIni.value + '/';
                }
            }
        }



        function controlaFechaHasta() {

            if (window.event.keyCode == 8 || window.event.keyCode == 46) {
                //form1.txtFecFin.value = form1.txtFecFin.value.substring(0, form1.txtFecFin.value.length - 2);
            }
            else {

                if (form1.txtFecFin.value.length == 2) {
                    form1.txtFecFin.value = form1.txtFecFin.value + '/';
                }
                if (form1.txtFecFin.value.length == 5) {
                    form1.txtFecFin.value = form1.txtFecFin.value + '/';
                }
            }
        }

        function ToUpperCase() {
            var numVuelo = document.getElementById("txtCajero").value;
            document.getElementById("txtCajero").value = numVuelo.toUpperCase();
        }

        function Activo() {
            if (document.getElementById("rbActivo").checked) {
                document.getElementById("lblFecIni").disabled = true;
                document.getElementById("lblFecFin").disabled = true;
                document.getElementById("imgbtnIni").disabled = true;
                document.getElementById("imgbtnFin").disabled = true;
            }
        }
        function Cerrado() {
            if (document.getElementById("rbCerrado").checked) {
                document.getElementById("lblFecIni").disabled = false;
                document.getElementById("lblFecFin").disabled = false;
                document.getElementById("imgbtnIni").disabled = false;
                document.getElementById("imgbtnFin").disabled = false;
            }
        }         
    </script>

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
                    &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
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
                                            <table style="width: 100%; left: 0px; position: relative; top: 0px;" class="alineaderecha">
                                                <tr>
                                                    <td class="style3" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                    <td class="style3" colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;" colspan="2">
                                                        <asp:RadioButton ID="rbActivo" runat="server" Checked="True" GroupName="turno" onClick="Activo();" />
                                                    </td>
                                                    <td colspan="1" class="alineaderecha">
                                                        <asp:RadioButton ID="rbCerrado" runat="server" GroupName="turno" onClick="Cerrado();" />
                                                    </td>
                                                    <td colspan="1" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        <asp:Label ID="lblTurno" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:TextBox ID="txtTurno" runat="server" CssClass="textbox" MaxLength="6"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="txtTurno_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" FilterType="Numbers" TargetControlID="txtTurno">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td colspan="1" class="alineaderecha">
                                                        <asp:Label ID="lblUsuario" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td colspan="1" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:TextBox ID="txtCajero" runat="server" CssClass="textbox" MaxLength="30" onkeyup="JavaScript:ToUpperCase();"></asp:TextBox>
                                                        <cc2:FilteredTextBoxExtender ID="txtCajero_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtCajero" ValidChars="abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM0123456789">
                                                        </cc2:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        <asp:Label ID="lblFecIni" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFecIni" runat="server" CssClass="textboxFecha" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" Width="88px"></asp:TextBox>
                                                        <cc2:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnIni"
                                                            TargetControlID="txtFecIni">
                                                        </cc2:CalendarExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="imgbtnIni" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                            Width="22px" />
                                                    </td>
                                                    <td class="alineaderecha" colspan="1">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="1" class="alineaderecha">
                                                        <asp:Label ID="lblFecFin" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFecFin" runat="server" CssClass="textboxFecha" Height="16px"
                                                            MaxLength="10" onfocus="this.blur();" Width="88px"></asp:TextBox>
                                                        <cc2:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgbtnFin"
                                                            TargetControlID="txtFecFin">
                                                        </cc2:CalendarExtender>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="imgbtnFin" runat="server" Height="22px" ImageUrl="~/Imagenes/Calendar.bmp"
                                                            Width="22px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="5" style="width: 331px; text-align: left; height: 13px;">
                                                        <asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="~/Imagenes/Search.bmp"
                                                            OnClick="ibtnBuscar_Click" Width="16px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 331px; text-align: left; height: 13px;" colspan="5">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SpacingGrid" style="height: 115px">
                                                        &nbsp;
                                                    </td>
                                                    <td class="CenterGrid" colspan="4" align="left">
                                                        <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                                        <asp:GridView ID="grvTurno" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="3" CssClass="grilla" GridLines="Vertical" OnPageIndexChanging="grvTurno_PageIndexChanging"
                                                            OnSorting="grvTurno_Sorting" Width="100%" AllowPaging="True">
                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                            <Columns>
                                                                <asp:HyperLinkField DataNavigateUrlFields="Cod_Turno" DataNavigateUrlFormatString="Ope_ExtornarTicket.aspx?Cod_Turno={0}"
                                                                    DataTextField="Cod_Turno" HeaderText="Turno" NavigateUrl="Ope_ExtornarTicket.aspx"
                                                                    SortExpression="Cod_Turno" />
                                                                <asp:BoundField DataField="Num_Ip_Equipo" HeaderText="Número IP" SortExpression="Num_Ip_Equipo" />
                                                                <asp:BoundField DataField="Dsc_Usuario" HeaderText="Usuario" SortExpression="Dsc_Usuario" />
                                                                <asp:BoundField DataField="Fch_Inicio" HeaderText="Fecha Inicio" SortExpression="Fch_Inicio" />
                                                            </Columns>
                                                            <SelectedRowStyle CssClass="grillaFila" />
                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                            <HeaderStyle CssClass="grillaCabecera" />
                                                            <AlternatingRowStyle CssClass="grillaFila" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td class="SpacingGrid" colspan="1" style="height: 115px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style5">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="5" style="width: 331px; text-align: left; height: 13px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left;" colspan="5" class="style9">
                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label>
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
                        </asp:UpdatePanel>
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
