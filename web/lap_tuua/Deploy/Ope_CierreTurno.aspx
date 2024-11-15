<%@ page language="C#" autoeventwireup="true" inherits="Ope_CierreTurno, App_Web_7ctknflu" %>

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
    <title>Cierre de Turno</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript" language="JavaScript">
        function CalcularVenta() {
            var frm = document.forms[0];
            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox") {
                    if (frm.elements[i].checked)
                        frm.elements[i].checked = false;
                    else frm.elements[i].checked = true;
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="background-image: url(Imagenes/back.gif)">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <!-- HEADER -->
                <td align="center">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo2">
                <!-- FILTER -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td align="left" style="height: 30px; width: 90%">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <!-- FIRST ROW -->
                                        <td style="width: 20px;">
                                        </td>
                                        <td style="height: 30px;">
                                            <asp:Label ID="lblTurno" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                        </td>
                                        <td style="width: 3%;">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTurno" runat="server" CssClass="textbox" MaxLength="6"></asp:TextBox>
                                            <cc2:FilteredTextBoxExtender ID="txtTurno_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtTurno">
                                            </cc2:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 3%;">
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    &nbsp;<asp:ImageButton ID="ibtnBuscar" runat="server" ImageUrl="~/Imagenes/Search.bmp"
                                                        OnClick="ibtnBuscar_Click" Width="16px" />&nbsp;
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server">
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
                            <td align="right">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnCerrar" runat="server" CssClass="Boton" OnClick="btnCerrar_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <cc2:ConfirmButtonExtender ID="btnCerrar_ConfirmButtonExtender" runat="server" ConfirmText=""
                                            Enabled="True" TargetControlID="btnCerrar">
                                        </cc2:ConfirmButtonExtender>
                                        <uc3:OKMessageBox ID="omb" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
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
                <!-- DATA -->
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td>
                                <div id="div1" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvTurno" runat="server" CssClass="grilla" AllowPaging="True" AllowSorting="false"
                                                AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Seleccionar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbCierre" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" Width="427px"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ibtnBuscar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                            <td class="SpacingGrid">
                            </td>
                        </tr>
                    </table>
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
