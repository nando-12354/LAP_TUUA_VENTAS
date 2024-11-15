<%@ control language="C#" autoeventwireup="true" inherits="UserControl_ModVentaDetalle, App_Web_i29mqlr7" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

     
<cc1:ModalPopupExtender ID="mpextModalidadVenta" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>


     
<asp:Panel ID="pnlPopup" CssClass="modalDetalleRepresenante" Style="display: none" runat="server" Height="470px" Width="400px">
   <div style="overflow: auto; height: 470px;">
    <asp:Panel ID="pnlDetalle" runat="server" >

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="text-align: right">
          <asp:Button ID="btnAceptar" runat="server" CssClass="Boton" onclick="btnAceptar_Click" Text="Aceptar" CausesValidation="False"/>
          <asp:Button ID="btnCancelar" runat="server" CssClass="Boton" Text="Cancelar" onclick="btnCancelar_Click" />
        </div>
        <br/>
          <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" CausesValidation="False" Style="display: none" />
    </asp:Panel>
    </div>
</asp:Panel> 


<script language="javascript" type="text/javascript">
    function onOk() {
    }
</script>