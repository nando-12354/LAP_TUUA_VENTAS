<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ope_ExtenderVigencia.aspx.cs"
    Inherits="Ope_ExtenderVigencia" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register Src="UserControl/OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LAP - Operaciones - Extensión Fecha Vigencia Tickets</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/KeyPress.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    
    <style type="text/css">
        .ajax__calendar_container
        {
            z-index: 1000;
        }
    </style>
    
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
        function Activo() {
            if (document.getElementById("rbRango").checked == true) {
                document.getElementById("txtNroIni").disabled = false;
                document.getElementById("txtNroFin").disabled = false;
                document.getElementById("txtNumTicket").disabled = true;
                document.getElementById("txtNumTicket").value = "";

                document.getElementById("txtNumTicket").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNroIni").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNroFin").style.backgroundColor = '#FFFFFF';
            } else {
                document.getElementById("txtNroIni").disabled = true;
                document.getElementById("txtNroFin").disabled = true;
                document.getElementById("txtNumTicket").disabled = false;
                document.getElementById("txtNroIni").value = "";
                document.getElementById("txtNroFin").value = "";

                document.getElementById("txtNumTicket").style.backgroundColor = '#FFFFFF';
                document.getElementById("txtNroIni").style.backgroundColor = '#CCCCCC';
                document.getElementById("txtNroFin").style.backgroundColor = '#CCCCCC';
            }
        }  
    </script>

    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body onload="javascript:Activo()">
    <form id="form1" runat="server">
    <div style="background-image: url(Imagenes/back.gif)">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </asp:ScriptManager>
        <table align="center" cellpadding="0" cellspacing="0" class="TamanoTabla">
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
                                        <td style="width: 20px;" rowspan="3">
                                        </td>
                                        <td style="height: 30px;">
                                            <asp:RadioButton ID="rbRango" runat="server" GroupName="ext" onClick="Activo();"
                                                CssClass="TextoEtiquetaBold" />
                                        </td>
                                        <td style="width: 3%;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDelNro" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNroIni" runat="server" CssClass="textbox"></asp:TextBox>
                                            <cc2:FilteredTextBoxExtender ID="txtNroIni_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtNroIni">
                                            </cc2:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 3%;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAlNro" runat="server" CssClass="TextoEtiqueta"></asp:Label>&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNroFin" runat="server" CssClass="textbox"></asp:TextBox>
                                            <cc2:FilteredTextBoxExtender ID="txtNroFin_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtNroFin">
                                            </cc2:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 10%;">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExt" runat="server" CssClass="TextoEtiquetaBold"></asp:Label>&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="txtDias" runat="server" CssClass="textbox" MaxLength="5"></asp:TextBox>
                                            <cc2:FilteredTextBoxExtender ID="txtDias_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtDias">
                                            </cc2:FilteredTextBoxExtender>
                                            <asp:Label ID="lblDias" runat="server" CssClass="TextoEtiquetaBold" Text="días"></asp:Label>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <!-- SECOND ROW -->
                                        <td style="height: 30px;">
                                            <asp:RadioButton ID="rbTicket" runat="server" GroupName="ext" onClick="Activo();"
                                                CssClass="TextoEtiquetaBold" />
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNumTicket" runat="server" CssClass="textbox" Enabled="False"></asp:TextBox>
                                            <cc2:FilteredTextBoxExtender ID="txtNumTicket_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtNumTicket">
                                            </cc2:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    &nbsp;<asp:ImageButton ID="ibtnBuscar" runat="server" OnClientClick="Javascript:return Activo();"
                                                        ImageUrl="~/Imagenes/Search.bmp" OnClick="ibtnBuscar_Click" Width="16px" />
                                                    &nbsp;
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
                                        <asp:Button ID="btnExtender" runat="server" CssClass="Boton" OnClick="btnExtender_Click" />
                                        &nbsp;&nbsp;&nbsp;
                                        <cc2:ConfirmButtonExtender ID="btnExtender_ConfirmButtonExtender" runat="server"
                                            ConfirmText="" Enabled="True" TargetControlID="btnExtender">
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
                                <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvTicket" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" OnSorting="grvTicket_Sorting" Width="100%"
                                                CssClass="grilla" AllowPaging="True" PageSize="3" OnPageIndexChanging="grvTicket_PageIndexChanging">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Cod_Numero_Ticket" HeaderText="Num. Ticket" SortExpression="Cod_Numero_Ticket" />
                                                    <asp:TemplateField HeaderText="Fecha Emisión" SortExpression="Fch_Creacion">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecCreacion" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Creacion")),null) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Tipo_Ticket" HeaderText="Tipo Ticket" SortExpression="Tipo_Ticket" />
                                                    <asp:TemplateField HeaderText="Fec. Vencimiento" SortExpression="Fch_Vencimiento">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFecVence" runat="server" Text='<%# Convert.ToString(Eval("Fch_Vencimiento")) %> '></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Num_Extensiones" HeaderText="Numero de Veces" SortExpression="Num_Extensiones">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Dias_xVencer" HeaderText="Numero de Dias Por Vencer" SortExpression="Dias_xVencer">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Dsc_Estado_Actual" HeaderText="Estado" SortExpression="Dsc_Estado_Actual" />
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbRegistrar" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:ImageButton ID="ibtnCheckAll" ImageUrl="~/Imagenes/check.gif" OnClientClick="JavaScript:CalcularVenta();"
                                                                runat="server" />
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
                                            <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
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
