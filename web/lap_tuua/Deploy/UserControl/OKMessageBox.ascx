<%@ control language="C#" autoeventwireup="true" inherits="UserControls_OKMessageBox, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground" TargetControlID="pnlPopup" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk"  Height="95px" Width="250px">
    <table width="100%">
        <tr class="topHandle">
            <td colspan="2" align="left" runat="server" id="tdCaption">
                &nbsp; <asp:Label ID="lblCaption" runat="server" CssClass="msgTitulo"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Info-48x48.png"/>
            </td>
            <td valign="middle" align="left">
                <asp:Label ID="lblMessage" runat="server" CssClass="msgMensaje"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" onblur="this.focus()" />
            </td>
        </tr>
    </table>
</asp:Panel>

<asp:HiddenField ID="hflUrl" runat="server" />


<script type="text/javascript">
        function fnClickOK(sender, e)
        {
            __doPostBack(sender,e);
        }
</script>