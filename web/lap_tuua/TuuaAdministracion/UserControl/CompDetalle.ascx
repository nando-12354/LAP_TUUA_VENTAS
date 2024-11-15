<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CompDetalle.ascx.cs" Inherits="UserControl_CompDetalle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

    
<cc1:ModalPopupExtender ID="mpextCompania" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>

<asp:Panel ID="pnlPopup" CssClass="modalDetalleRepresenante" Style="display: none" runat="server" Height="470px" Width="600px">
   
    <asp:Panel ID="pnlDetalle" runat="server" >

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: right">
          <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" onclick="btnAceptar_Click" Text="Aceptar" CausesValidation="False" />
          <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" Text="Cancelar" />
        </div>
        <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" CausesValidation="False" Style="display: none" />
        <asp:Label ID="lblMensajeError" runat="server" Text="" CssClass="mensaje"></asp:Label>
    </asp:Panel>
    
</asp:Panel> 


<script language="javascript" type="text/javascript">
    function onOk() {
    }
</script>
    
  
