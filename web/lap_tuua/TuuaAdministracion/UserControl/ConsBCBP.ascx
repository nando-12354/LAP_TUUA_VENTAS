<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsBCBP.ascx.cs" Inherits="UserControl_ConsBCBP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalDetalleTicket" Style="display: none" runat="server" Width="1000px"  Height="410px">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <br />
      <br />
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="95%">
        <tr>
          <td width="100%" align="center">
            <asp:Label ID="lblDetalleBCBP" runat="server" CssClass="titulosecundario"></asp:Label>
          </td>
        </tr>
      </table>
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="95%">
        <tr>
          <td width="100%">
            <hr color="#0099cc" style="width: 100%; height: 0px" />
          </td>
        </tr>
      </table>
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
          <td>
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="95%">
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetCompania" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetFechaVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetNumVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="18px" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetNumAsiento" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="138px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblNomPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetNomPasajero" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="137px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblTipoIngreso" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetTipoIngreso" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="137px"></asp:Label>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="95%">
        <tr>
          <td width="100%">
            <hr color="#0099cc" style="width: 100%; height: 0px" />
          </td>
        </tr>
      </table>
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
          <td width="30px">
          </td>
          <td>
            <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
              <tr>
                <td align="left">
                  <div style="height: 150px; width: 97%; overflow: auto;">
                    <asp:Label ID="msgNoHist" runat="server" />
                    <asp:GridView ID="grvBoardingEstHist" runat="server" BackColor="White" AutoGenerateColumns="False" GridLines="Both" CssClass="grilla" Width="97%" RowStyle-VerticalAlign="Bottom" AllowPaging="true" OnPageIndexChanging="grvBoardingEstHist_PageIndexChanging" AllowSorting="True" OnSorting="grvBoardingEstHist_Sorting">
                      <SelectedRowStyle CssClass="grillaFila" />
                      <PagerStyle CssClass="grillaPaginacion" />
                      <HeaderStyle CssClass="headerGridAbsolute" />
                      <AlternatingRowStyle CssClass="grillaFila" />
                      <Columns>
                        <asp:BoundField HeaderText="N&#250;m. Secuencial" DataField="Num_Secuencial" SortExpression="Num_Secuencial" />
                        <asp:BoundField HeaderText="Tipo Estado" DataField="Tip_Estado" SortExpression="Tip_Estado" />
                        <asp:BoundField HeaderText="Desc. Tipo Estado" DataField="Dsc_Boarding_Estado" SortExpression="Dsc_Boarding_Estado" />
                        <asp:BoundField HeaderText="Cód. Equipo" DataField="Cod_Equipo_Mod" SortExpression="Cod_Equipo_Mod" />
                        <asp:BoundField HeaderText="Núm. Vuelo" DataField="Num_Vuelo" SortExpression="Num_Vuelo" />
                        <asp:BoundField HeaderText="Usuario Mod." DataField="Log_Usuario_Mod" SortExpression="Log_Usuario_Mod" />
                        <asp:BoundField DataField="Log_Fecha_Mod" HeaderText="Fecha Mod." SortExpression="Log_Fecha_Mod" />
                      </Columns>
                      <EmptyDataTemplate>
                        &nbsp;
                      </EmptyDataTemplate>
                    </asp:GridView>
                  </div>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
  <br />
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

