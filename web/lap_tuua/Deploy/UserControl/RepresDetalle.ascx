<%@ control language="C#" autoeventwireup="true" inherits="UserControl_RepresDetalle, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

     
<cc1:ModalPopupExtender ID="mpextReprsentante" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>


     
<asp:Panel ID="pnlPopup" CssClass="modalDetalleRepresenante" Style="display: none" runat="server" Height="470px" Width="400px">
   
    <table style="width:386px;">
        <tr>
            <td >
                &nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblTituloRepresentante" runat="server" 
                    CssClass="titulosecundario"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="lblCodigo" runat="server" CssClass="TextoEtiqueta" Visible="False"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDscCodigo" runat="server" CssClass="TextoCampo" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblNombre" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="textbox" ValidationGroup="Representante" Height="20px" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)" 
                    Width="198px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ValidationGroup="Representante" ControlToValidate="txtNombre" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblApellido" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="textbox" ValidationGroup="Representante" Height="20px" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"    Width="198px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ValidationGroup="Representante" ControlToValidate="txtApellido" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblCargo" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCargo" runat="server" CssClass="textbox" Height="20px" ValidationGroup="Representante" onkeypress="JavaScript: Tecla('Character');" onblur="gDescripcionNombre(this)"
                    Width="153px"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvCargo" runat="server" ValidationGroup="Representante" ControlToValidate="txtCargo" 
                    Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblTipoDocumento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="combo" ValidationGroup="Representante">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblNumDocumento" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNumDocumento" runat="server" CssClass="textbox" ValidationGroup="Representante"
                    onkeypress="JavaScript: Tecla('Double');" onblur="val_int(this)" MaxLength="11"></asp:TextBox>
                <br />
                <asp:RequiredFieldValidator ID="rfvNumdocumento" runat="server" ValidationGroup="Representante"
                    ControlToValidate="txtNumDocumento" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td TableCell8 colspan="2">
                <asp:Label ID="lblEstado" runat="server" CssClass="TextoEtiqueta"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="combo">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="2">
                &nbsp;</td>
            <td >
            <asp:CustomValidator ID="cvtTipoDocumento" runat="server" 
                ClientValidationFunction="validateLength" ValidationGroup="Representante"
                    ControlToValidate="txtNumDocumento" Display="Dynamic" ErrorMessage="*"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="2">
                &nbsp;</td>
            <td >
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td class="style1" colspan="3">
                <asp:Label ID="lblPermisosRepresentante" runat="server" 
                    CssClass="TextoEtiqueta"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8>
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                <asp:Panel ID="pnlPermisos" runat="server" Height="106px" Width="163px">
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8>
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8>
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td class="style11">
                    <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" 
              onclick="btnAceptar_Click" Text="Aceptar" CausesValidation="true" ValidationGroup="Representante"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="3">
                &nbsp;</td>
        </tr>
        </table>
          
</asp:Panel> 


<script language="javascript" type="text/javascript">
    function onOk() {
    }
    
</script>

