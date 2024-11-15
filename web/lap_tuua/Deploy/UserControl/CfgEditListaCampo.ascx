<%@ control language="C#" autoeventwireup="true" inherits="UserControl_CfgEditListaCampo, App_Web_6uwanhbu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="OKMessageBox.ascx" TagName="OKMessageBox" TagPrefix="uc1" %>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpextCampos" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopupCampo" OkControlID="btnCerrarPopupCampo"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopupCampo" Style="display: none" runat="server" Width="700px"
    Height="350px" BackColor="White">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 680px; height: 190px;">
                <tr>
                    <td>
                        <div class="EspacioSubTablaPrincipal">
                            <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                        <table style="width: 670px">
                                            <tr>
                                                <td colspan="3">
                                                    <asp:Label ID="lblTituloCampo" runat="server" CssClass="titulosecundario"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr class="EspacioLinea" color="#0099cc" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td align="right" colspan="2">
                                                    <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" CausesValidation="False"
                                                        Width="96px" CssClass="Boton" Text="Eliminar" />
                                                    <asp:Button ID="btnGrabar" runat="server" OnClick="btnGrabar_Click" ValidationGroup="ListaCampo"
                                                        Width="96px" CssClass="Boton" Text="Grabar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr class="EspacioLinea" color="#0099cc" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblNomCampo" runat="server" Text="Nombre Campo:" CssClass="TextoFiltro"></asp:Label>
                                                </td>
                                                <td style="width: 50%">
                                                    <asp:Label ID="lblNomCampoDesc" runat="server" Text="Label" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="X-Small" ForeColor="#008FD5"></asp:Label>
                                                </td>
                                                <td style="width: 20%">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCodCampo" runat="server" CssClass="TextoFiltro" Text="Código Campo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtCodCampo" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="X-Small" ForeColor="#008FD5"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblCodCampoAsoc" runat="server" CssClass="TextoFiltro" Text="Código Campo Asociado:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtCodCampoAsoc" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                        Font-Size="X-Small" ForeColor="#008FD5"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del Valor:" CssClass="TextoFiltro"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="80" Width="262px" ValidationGroup="ListaCampo"
                                                        onkeypress="return soloDescripcion(this,event);" onblur="gDescripcion(this);"
                                                        CssClass="textbox"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescripcion"
                                                        ErrorMessage="Ingrese la Descripción de Campo " ValidationGroup="ListaCampo"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="SpacingGrid">
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table>
        <tr>
            <td class="SpacingGrid" style="height: 115px">
                <asp:Button ID="btnCerrarPopupCampo" runat="server" Text="Cerrar" CssClass="Boton" />
            </td>
        </tr>
    </table>
</asp:Panel>
<uc1:OKMessageBox ID="omb" runat="server" />

<script language="javascript" type="text/javascript">
    function onOk() {
        //document.getElementById('pnlPopup').display="none";

        //document.getElementById('pnlPopup').visible="false";
        //      //document.getElementById('pnlPopup').style="display:none";
        //      //document.getElementById('mpext').display="none";
        //      //document.getElementById('mpext').visible="false";
        //      //__doPostBack('btnCerrarPopup','');
    }    
    
</script>

