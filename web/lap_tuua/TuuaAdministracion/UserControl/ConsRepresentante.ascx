<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsRepresentante.ascx.cs" Inherits="UserControl_ConsRepresentante" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()"  DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalPopupRepresentante" Style="display: none" runat="server" Width="400px" Height="350px">
  <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
      <td>
        &nbsp;
      </td>
      <td>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
              <tr>
                <td colspan="2" align="center">
                  <br />
                  <br />
                  <asp:Label ID="lblRpteCompania" runat="server" CssClass="titulosecundario"></asp:Label>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <hr class="EspacioLinea" color="#0099cc" />
                </td>
              </tr>
              <tr>
                <td>
                </td>
                <td>
                  <br />
                  <asp:Label ID="lblCompania" runat="server" CssClass="TextoEtiqueta"></asp:Label>
                  &nbsp;&nbsp;&nbsp;
                  <asp:DropDownList ID="cboCompania" runat="server" CssClass="combo" OnSelectedIndexChanged="cboCompania_SelectedIndexChanged" Width="180px" AutoPostBack="True">
                  </asp:DropDownList>
                  <br />
                  <br />
                  <br />
                  <asp:Label ID="lblRepresentante" runat="server" CssClass="TextoEtiqueta"></asp:Label><br />
                  <br />
                  <div style="height: 150px; width: 100%; overflow: auto;">
                  <asp:GridView ID="grvDetalleRepresentantes" runat="server" BackColor="White" BorderColor="#999999" AutoGenerateColumns="False" CssClass="grilla" Width="96%" OnRowDataBound="grvDetalleRepresentantes_RowDataBound" AllowPaging="true" OnPageIndexChanging="grvDetalleRepresentantes_PageIndexChanging">
                    <SelectedRowStyle CssClass="grillaFila" />
                    <PagerStyle CssClass="grillaPaginacion" />
                    <HeaderStyle CssClass="headerGridAbsolute" />
                    <AlternatingRowStyle CssClass="grillaFila" />
                    <Columns>
                      <asp:BoundField DataField="Numero" HeaderText="Numero" ItemStyle-Width="15%"/>
                      <asp:BoundField DataField="Nombres" HeaderText="Nombres" ItemStyle-Width="60%"/>
                      <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-Width="25%"/>
                    </Columns>
                  </asp:GridView>
                  </div>
                </td>
              </tr>
            </table>
          </ContentTemplate>
          <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCerrarPopup" EventName="Click" />
          </Triggers>
        </asp:UpdatePanel>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="90%">
          <tr>
            <td colspan="2">
              <br />
              <br />
              <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" OnClick="btnCerrarPopup_Click" />
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</asp:Panel>

<script language="javascript" type="text/javascript">
  function onOk()
  {
      //document.getElementById('pnlPopup').display="none";
      
      //document.getElementById('pnlPopup').visible="false";
//      //document.getElementById('pnlPopup').style="display:none";
//      //document.getElementById('mpext').display="none";
//      //document.getElementById('mpext').visible="false";
//      //__doPostBack('btnCerrarPopup','');
  }
  
  
  
  
</script>

