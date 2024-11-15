<%@ control language="C#" autoeventwireup="true" inherits="UserControl_questionmessage, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        font-size: small;
        font-family: Arial, Helvetica, sans-serif;
    }
</style>
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground" TargetControlID="pnlPopup" PopupControlID="pnlPopup"></cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk"  Height="95px" Width="250px">
    <table width="100%">
        <tr class="topHandle">
            <td colspan="2" align="left" runat="server" id="tdCaption">
                &nbsp;<asp:Label ID="Label1" runat="server" style="font-weight: 700; font-size: small ; color: #264B94" Text="Label">
                Mensaje</asp:Label></td>
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Info-48x48.png"/>
            </td>
            <td valign="middle" align="left">
               <asp:Label ID="lblMessage" runat="server" CssClass="msgMensaje" 
                    BorderColor="Black">Registro Cancelado.</asp:Label>
                    </td>
        </tr>
        <tr>
            <td colspan="2" align="right" enableviewstate="True">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" onblur="this.focus()" 
                    OnClick="btnOk_Click" Text="OK" />               
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

