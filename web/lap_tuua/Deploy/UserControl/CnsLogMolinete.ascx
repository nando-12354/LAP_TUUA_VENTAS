<%@ control language="C#" autoeventwireup="true" inherits="UserControl_CnsLogMolinete, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 19px;
    }
    
</style>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpextCnsLogMolinete" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopupBoarding" OkControlID="btnCerrarPopup"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopupBoarding" CssClass="modalDetalleTurno" runat="server" Width="900px"
    Height="380px" Style="display: none">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <div style="min-height: 290px;">
                <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center"
                    width="100%">
                    <tr>
                        <!-- TITLE - ZONE -->
                        <td align="center" class="Espacio1FilaTabla" rowspan="5" width="2%">
                            &nbsp;&nbsp;
                        </td>
                        <td align="center" width="96%">
                            <asp:Label ID="TituloError" runat="server" CssClass="titulosecundario">Detalle Pista de Error</asp:Label>
                        </td>
                        <td align="center" class="Espacio1FilaTabla" rowspan="5" width="2%">
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <!-- LINE - ZONE -->
                        <td>
                            <hr color="#0099cc" style="width: 100%; height: 0px" />
                        </td>
                    </tr>
                    <tr>
                        <!-- FIRST ROW - ZONE -->
                        <td>
                            <div style="height: 265px; width: 100%; overflow: auto;">
                                <asp:BulletedList ID="blErrores" runat="server" Font-Size="9pt" BulletImageUrl="~/Imagenes/icono.gif"
                                    BulletStyle="CustomImage">
                                </asp:BulletedList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <!-- FOURTH ROW - ZONE -->
                        <td class="style1">
                            <hr color="#0099cc" style="width: 100%; height: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="95%">
        <tr>
            <td width="30px">
            </td>
            <td>
                <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" />
            </td>
        </tr>
    </table>
</asp:Panel>
