<%@ control language="C#" autoeventwireup="true" inherits="UserControl_AtencionAlarma, App_Web_6uwanhbu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="OKMessageBox.ascx" tagname="OKMessageBox" tagprefix="uc1" %>


<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

     
<cc1:ModalPopupExtender ID="mpextAtencionAlarma" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>


     
<asp:Panel ID="pnlPopup" CssClass="modalDetalleRepresenante" Style="display: none" runat="server" Height="300px" Width="400px">
   
    <table style="width:386px;">
        <tr>
            <td >
                &nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblAtencionAlarma" runat="server" 
                    CssClass="Titulo"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td colspan="3">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblDescripcionAtencion" runat="server" CssClass="titulosecundario"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="3">
                <asp:TextBox ID="txtDscAtencion" runat="server" Height="166px" TextMode="MultiLine"  CssClass="textbox"
                    Width="327px"></asp:TextBox>
            </td>
            <td TableCell8="">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td class="style13" style="text-align: right">
                <asp:Button ID="btnAceptar" runat="server" CausesValidation="true" 
                    CssClass="Boton" onclick="btnAceptar_Click" />
            </td>
            <td class="style2" style="text-align: right">
                &nbsp;</td>
            <td class="style1">
                    <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" CausesValidation="False" />
            </td>
            <td class="style11">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td TableCell8 colspan="3">
                &nbsp;</td>
            <td TableCell8="">
                &nbsp;</td>
        </tr>
        </table>
          
</asp:Panel> 


<script language="javascript" type="text/javascript">
    function onOk() {
    }
    
</script>
<uc1:OKMessageBox ID="omb" runat="server" />
