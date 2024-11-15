<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cfg_CrearListaCampo.aspx.cs"
    Inherits="Cfg_CrearListaCampo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Configuración de Lista de Campos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script type="text/javascript">
        function numero() {
            if ((event.keyCode < 48) || (event.keyCode > 57)) event.keyCode = 0;
        }
        function val_int(o) {
            o.value = o.value.toString().replace(/([^0-9])/g, "");
        }
    </script>

    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 203px;
        }
        .style3
        {
            width: 40%;
        }
        .style4
        {
            width: 203px;
            height: 6px;
        }
        .style5
        {
            height: 6px;
        }
        .style6
        {
            width: 40%;
            height: 6px;
        }
        .style7
        {
            width: 203px;
            height: 7px;
        }
        .style8
        {
            height: 7px;
        }
        .style9
        {
            width: 40%;
            height: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    &nbsp;
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu3" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <!-- WORK MENU -->
            <td>
                <table cellpadding="0" cellspacing="0" class="TamanoTabla">
                    <tr>
                        <td align="right" class="style1" style="text-align: left">
                            &nbsp;&nbsp;&nbsp;<img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnGrabar" runat="server" CssClass="Boton" Text="Grabar >>" OnClick="btnGrabar_Click"
                                Width="100px" />&nbsp;&nbsp;&nbsp;
                            <cc1:ConfirmButtonExtender ID="cbeAceptar" runat="server" ConfirmText="Está seguro de crear la Lista de Campo"
                                Enabled="True" TargetControlID="btnGrabar">
                            </cc1:ConfirmButtonExtender>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr class="EspacioLinea" color="#0099cc" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="EspacioSubTablaPrincipal">
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <%--<asp:RequiredFieldValidator ID="rfvDescripcion" runat="server" ControlToValidate="txtDescripcion"
                                            Display="None" ErrorMessage="Ingrese descripción del valor"></asp:RequiredFieldValidator>--%>
                                        <%--<cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="rfvDescripcion">
                                        </cc1:ValidatorCalloutExtender>--%>
                                        &nbsp;&nbsp;
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <!-- FOOTER -->
            <td class="Espacio1FilaTabla" style="height: 11px">
                &nbsp;</td>
        </tr>
    </table>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td class="style1">
                                                    <asp:Label ID="lblNomCampo" runat="server" Text="Nombre Campo:" CssClass="TextoFiltro"></asp:Label>
                                                </td>
                                                <td class="style3">
                                                    <asp:TextBox ID="txtNomCampo" runat="server" MaxLength="50" CssClass="textbox" onkeypress="soloDescripcion()"
                                                        onblur="gDescripcion(this);" Width="221px"></asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="width: 20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                                                    &nbsp;
                                                </td>
                                                <td class="style5">
                                                    <%--<asp:Label ID="lblCodCampo" runat="server" Text="Código Campo:" CssClass="TextoFiltro"></asp:Label>--%>
                                                    Cod Campo Asociado</td>
                                                <td class="style6">
                                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="textbox" 
                                                        MaxLength="80" onblur="gDescripcion(this);" onkeypress="soloDescripcion()" 
                                                        Width="221px"></asp:TextBox>
                                                    </td>
                                                <td class="style5">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style7">
                                                    &nbsp;
                                                </td>
                                                <td class="style8">
                                                    <%--<asp:Label ID="lblCodCampoAsociado" runat="server" Text="Código Campo Asociado:"
                                                        CssClass="TextoFiltro"></asp:Label>--%>
                                                    Cod Campo</td>
                                                <td class="style9">
                                                    <asp:TextBox ID="txtCodCampo" runat="server" MaxLength="5" CssClass="textbox" onkeypress="soloDescripcion()"
                                                        onblur="gDescripcion(this);" Width="221px"></asp:TextBox>
                                                    </td>
                                                <td class="style8">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td class="style1">
                                                    Descripción</td>
                                                <td class="style3">
                                                    <asp:TextBox ID="txtCodCampoAsoc" runat="server" MaxLength="5" CssClass="textbox"
                                                        onkeypress="soloDescripcion()" onblur="gDescripcion(this);" Width="215px" 
                                                        Height="16px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td class="style1" colspan="2">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                <uc2:PiePagina ID="PiePagina2" runat="server" />
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc3:OKMessageBox ID="omb" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGrabar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
