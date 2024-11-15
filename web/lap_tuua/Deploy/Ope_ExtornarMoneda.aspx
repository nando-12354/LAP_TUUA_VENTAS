<%@ page language="C#" autoeventwireup="true" inherits="Ope_ExtornarMoneda, App_Web_jlql8yfo" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc3" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register assembly="System.Web.Extensions" namespace="System.Web.UI" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Extorno Operaciones</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
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
    </script>
</head>
<body>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeOut="600">
        </asp:ScriptManager>
          
        <div style="background-image: url(Imagenes/back.gif)">
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                                                
                        <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <div class="EspacioSubTablaPrincipal">
                            &nbsp;
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
<tr class="formularioTitulo">
                    <td align="right" class="style1" style="text-align: left">
                        <img alt="" src="Imagenes/flecha_back.png" onclick="JavaScript: FnVolver();" /></td>
                    <td align="right" colspan="2">
                                    <asp:Button ID="btnExtornar" runat="server" CssClass="Boton" 
                                        onclick="btnExtornar_Click" />
                                    <cc2:ConfirmButtonExtender ID="btnExtornar_ConfirmButtonExtender" 
                                        runat="server" ConfirmText="" Enabled="True" TargetControlID="btnExtornar">
                                    </cc2:ConfirmButtonExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>                            
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    <table style="width: 100%; left: 0px; position: relative; top: 0px;" 
                                            class="alineaderecha">
                                    
                                        <tr>
                                            <td class="style3" colspan="2">
                                                &nbsp;</td>
                                            <td class="style3" colspan="5">
                                                &nbsp;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="style5">
                                                <asp:Label ID="lblTipOpera" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td style="width: 331px; text-align: left; height: 13px;" colspan="2">
                                                <asp:RadioButton ID="rbCompra" runat="server" Checked="True" 
                                                    GroupName="turno" oncheckedchanged="rbCompra_CheckedChanged" />
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:RadioButton ID="rbVenta" runat="server" GroupName="turno" 
                                                    oncheckedchanged="rbVenta_CheckedChanged" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                <asp:Label ID="lblCodOpera" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                                            </td>
                                            <td colspan="5" style="text-align: left; height: 13px;">
                                                <asp:TextBox ID="txtOperacion" runat="server" CssClass="textbox" MaxLength="10"></asp:TextBox>
                                                <cc2:FilteredTextBoxExtender ID="txtOperacion_FilteredTextBoxExtender" 
                                                    runat="server" Enabled="True" FilterType="Numbers" 
                                                    TargetControlID="txtOperacion">
                                                </cc2:FilteredTextBoxExtender>
                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                <asp:ImageButton ID="ibtnBuscar" runat="server" 
                                                    ImageUrl="~/Imagenes/Search.bmp" Width="16px" onclick="ibtnBuscar_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                &nbsp;</td>
                                            <td colspan="2" style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                            <td class="alineaderecha" colspan="2">
                                                &nbsp;</td>
                                            <td colspan="1" style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                &nbsp;</td>
                                            <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="SpacingGrid" style="height: 115px">
                                                &nbsp;</td>
                                            <td class="CenterGrid" style="height: 115px" colspan="5" align="left">
                                                <asp:Label ID="lblGrilla" runat="server" CssClass="msgMensaje"></asp:Label>
                                                <div style="height: 170px; overflow: auto;">
                                                <asp:GridView ID="grvOperaciones" runat="server" AllowSorting="True" 
                                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grilla" 
                                                    GridLines="Vertical" Width="100%" onsorting="grvOperaciones_Sorting">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                               <%# Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Num_Operacion" HeaderText="Operacion" 
                                                            SortExpression="Num_Operacion" />   
                                                        <asp:BoundField DataField="Nom_Tipo_Operacion" HeaderText="Tipo Operacion" 
                                                            SortExpression="Nom_Tipo_Operacion" />
                                                        <asp:BoundField DataField="Tip_Estado_Actual" HeaderText="Estado" 
                                                            SortExpression="Tip_Estado_Actual" />
                                                        <asp:BoundField DataField="Fec_Opera" HeaderText="Fecha Proceso" 
                                                            SortExpression="Fec_Opera" />
                                                        <asp:BoundField DataField="Imp_ACambiar" HeaderText="Imp. Cambiar" 
                                                            SortExpression="Imp_ACambiar" /> 
                                                        <asp:BoundField DataField="Imp_Cambiado" HeaderText="Imp. Cambiado" 
                                                            SortExpression="Imp_Cambiado" />     
                                                        <asp:BoundField DataField="Dsc_Moneda" HeaderText="Moneda" 
                                                            SortExpression="Dsc_Moneda" />
                                                        <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                        <asp:CheckBox ID="ckbRegistrar" runat="server" />
                                                        </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:ImageButton ID="ibtnCheckAll" runat="server"
                                                                    ImageUrl="~/Imagenes/check.gif" OnClientClick="JavaScript:CalcularVenta();" />
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
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style5">
                                                &nbsp;</td>
                                            <td colspan="6" style="width: 331px; text-align: left; height: 13px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td style="text-align: left; " colspan="6" class="style9">
                                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="427px"></asp:Label></td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="style4">
                                                &nbsp;</td>
                                            <td align="center" colspan="3">
                                                <uc3:OKMessageBox ID="omb" runat="server" />
                                            </td>
                                            <td class="style9" colspan="3" style="text-align: left; ">
                                                &nbsp;</td>
                                        </tr>
                                        
                                    </table>
                                    &nbsp;</td>
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