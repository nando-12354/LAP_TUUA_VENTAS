<%@ page language="C#" autoeventwireup="true" inherits="Mnt_ModificarCompania, App_Web_7ctknflu" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="okmessagebox" TagPrefix="uc3" %>
<%@ Register Src="UserControl/CompDetalle.ascx" TagName="CompDetalle" TagPrefix="uc4" %>
<%@ Register Src="UserControl/RepresDetalle.ascx" TagName="RepresDetalle" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Modificación de Compañía</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <link href="css/datagrid.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->

    <script language="javascript" type="text/javascript">
        function validateLength(oSrc, args) {

            if (document.getElementById("RepresDetalle1_ddlTipoDocumento").value == 'RUC') {
                document.getElementById(oSrc.id).innerText = "El RUC debe tener 11 dígitos";
                args.IsValid = (args.Value.length == 11);
            }
            else {
                if (document.getElementById("RepresDetalle1_ddlTipoDocumento").value == 'DNI') {
                    document.getElementById(oSrc.id).innerText = "El DNI debe tener 8 dígitos";
                    args.IsValid = (args.Value.length == 8);
                }
            }
        }

        function Atras() {
            window.location.href = "./Mnt_VerCompania.aspx";
        }

        var accionSave = false;
        function beginRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = true;
                    document.body.style.cursor = 'wait';
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = true;
                    //                }
                }
            }
        }


        function endRequest(sender, args) {
            if (!sender.get_isInAsyncPostBack()) {
                if (accionSave) {
                    document.getElementById('btnAceptar').disabled = false;
                    document.body.style.cursor = 'default'

                    accionSave = false;
                    //                var xc = 0;
                    //                for (xc = 0; xc < document.forms[0].length; xc++) {
                    //                    document.forms[0].elements[xc].disabled = false;
                    //                }
                }
            }
        }

        function confirmacionLlamada() {
            var mConfirmacion = document.forms[0].hConfirmacion.value;

            if (confirm(mConfirmacion)) {
                accionSave = true;
                return true;
            }
            else {
                accionSave = false;
                return false;
            }
        }	
  
    
    </script>

    <style type="text/css">
        .style1
        {
            width: 119px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hConfirmacion" runat="server" Value="Confirmar Accion" />
    <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
        <tr>
            <td class="Espacio1FilaTabla" colspan="2" style="height: 11px">
                <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
            </td>
        </tr>
        <tr class="formularioTitulo">
            <td align="right" style="text-align: left">
                <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: Atras();" />
            </td>
            <td align="right">
                <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" OnClick="btnAceptar_Click"
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
                    <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid" style="height: 115px; width: 2%;">
                            </td>
                            <td class="CenterGrid" style="height: 115px">
                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Path="~/javascript/jSManager.js" />
                                    </Scripts>
                                </asp:ScriptManager>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align: left;" class="style8">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" class="style12">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" rowspan="5">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <table style="width: 80%">
                                                        <tr>
                                                            <td style="text-align: left; width:15%">
                                                                <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width:30%">
                                                                <asp:Label ID="txtCodigo" runat="server" Width="149px" CssClass="TextoCampo"></asp:Label>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td style="text-align: left; width:20%">
                                                                <asp:Label ID="lblAerolinea" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left; width:20%">
                                                                <asp:TextBox ID="txtAerolinea" runat="server" CssClass="textboxUpperCase" MaxLength="3" onkeypress="soloIATA()" onblur="gIATA(this)"  Width="110px" OnTextChanged="txtAerolinea_TextChanged"
                                                                    TabIndex="8"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left;">
                                                                <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left;">
                                                                <asp:TextBox ID="txtNombre" runat="server" Width="223px" MaxLength="50" onkeypress="soloDescripcion()" onblur="gDescripcion(this)" CssClass="textboxUpperCase" OnTextChanged="txtNombre_TextChanged" TabIndex="1"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: left">
                                                                &nbsp;
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblIATA" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtIATA" runat="server" CssClass="textboxUpperCase" MaxLength="3" onkeypress="soloIATA()" onblur="gIATA(this)" Width="110px" OnTextChanged="txtIATA_TextChanged" TabIndex="5"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblTipoCompañia" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:DropDownList ID="ddlTipoCompañía" runat="server" CssClass="combo2" Width="225px"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlTipoCompañía_SelectedIndexChanged"
                                                                    TabIndex="1">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblOACI" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtOACI" runat="server" CssClass="textboxUpperCase" MaxLength="3" onkeypress="soloOACI()" onblur="gOACI(this)" 
                                                                    Width="110px" OnTextChanged="txtOACI_TextChanged" TabIndex="6"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblRuc" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtRuc" runat="server" CssClass="textbox" MaxLength="11" onkeypress="JavaScript: Tecla('Integer');"
                                                                    onblur="val_int(this)" Width="223px" OnTextChanged="txtRuc_TextChanged" TabIndex="2"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblSAP" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtSAP" runat="server" CssClass="textbox" MaxLength="10" onblur="val_int(this)" onkeypress="JavaScript: Tecla('Integer');" Width="110px" OnTextChanged="txtSAP_TextChanged" TabIndex="7"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo2" Width="225px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlTipoCompañía_SelectedIndexChanged" TabIndex="3">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td style="text-align: left">
                                                                <asp:Label ID="lblCodigoEspecial" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <asp:TextBox ID="txtCodigoEspecial" runat="server" CssClass="textbox" MaxLength="10" onblur="val_int(this)" onkeypress="JavaScript: Tecla('Integer');" Width="110px" OnTextChanged="txtCodigoEspecial_TextChanged" TabIndex="4">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: left">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="4" style="text-align: left">
                                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="471px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddRepresentante" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td style="text-align: left;" class="style23">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" class="style8">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left" class="style12">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left" class="style23">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left" class="style8">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td style="text-align: left" class="style12">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left" class="style23">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td class="style12" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td class="style23" style="text-align: left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td class="style12" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td class="style23" style="text-align: left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left;" class="style8">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" class="style12">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: left;" class="style7">
                                            <div class="CompaniaDIV">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="text-align: left;" class="style9">
                                                            <asp:Label ID="lblRepresentante" runat="server" CssClass="Titulo"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:ImageButton ID="btnAddRepresentante" runat="server" CausesValidation="False"
                                                                ImageUrl="~/Imagenes/Add.bmp" OnClick="btnAddRepresentante_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left;" class="style8">
                                                            <asp:Panel ID="pnlPanelRepresentante" runat="server">
                                                                <div class="divGrillaRepresentante">
                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="gvwRepresentante" runat="server" CssClass="datagrid" AutoGenerateColumns="False"
                                                                                OnRowCommand="gvwRepresentante_RowCommand">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imbRepresentante" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                                                                CommandName="ShowRepresentante" Text='<%# Eval("NumSecuencial") %>' CausesValidation="False"
                                                                                                ImageUrl="~/Imagenes/btn_edit.gif" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <EmptyDataTemplate>
                                                                                    <asp:Button ID="Button2" runat="server" OnClientClick="ScriptOpenModalDialog, &quot;Mnt_RepresentanteDetalle.aspx?id=111&quot;, &quot;imbEditar&quot;, &quot;410&quot;, &quot;369&quot;"
                                                                                        Text="Button" />
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="btnAddRepresentante" EventName="Click" />
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlTipoCompañía" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style12" style="text-align: left;">
                                                            &nbsp;
                                                                            <asp:Label ID="lblMensajeErrorRepres" runat="server" CssClass="mensaje" 
                                                                                    Width="471px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style10" style="text-align: left;">
                                                            <asp:Label ID="lblModalidadVenta" runat="server" CssClass="Titulo" Width="200px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style11" style="text-align: left;">
                                                            <div class="divMVCompañia" style="width: 300px; height: 200px; overflow: auto;">
                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="pnlModalidadVenta" runat="server">
                                                                        </asp:Panel>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnAceptar" EventName="Click" />
                                                                        <asp:AsyncPostBackTrigger ControlID="ddlTipoCompañía" EventName="SelectedIndexChanged" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <td class="style23" style="text-align: left">
                                            &nbsp;
                                        </td>
                                        <td style="height: 24px; text-align: left">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="SpacingGrid" style="height: 115px">
                            </td>
                        </tr>
                    </table>
                </div>
                <uc2:PiePagina ID="PiePagina2" runat="server" />
            </td>
        </tr>
    </table>
    <div>
        <div style="background-image: url(Imagenes/back.gif)">
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc3:okmessagebox ID="omb" runat="server" />
            <uc4:CompDetalle ID="CompDetalle1" runat="server" />
            <uc5:RepresDetalle ID="RepresDetalle1" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
