<%@ control language="C#" autoeventwireup="true" inherits="UserControl_ConsTicket, App_Web_i29mqlr7" %>
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
            <asp:Label ID="lblDetalleTicket" runat="server" CssClass="titulosecundario"></asp:Label>
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
                  <asp:Label ID="lblNumTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetNumTicket" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetTipoTicket" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetCompania" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="18px" Width="205px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetNumVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="138px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblFechaVencimiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetFchVencimiento" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="137px"></asp:Label>
                </td>
              </tr>
              <tr class="formularioTitulo">
                <td width="30px">
                </td>
                <td style="width: 140px; height: 11px">
                  <asp:Label ID="lblTipoPersona" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
                <td>
                  <asp:Label ID="lblDetTipPersona" runat="server" CssClass="TextoFiltro" Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Width="137px"></asp:Label>
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
                    <asp:GridView ID="grvSubDetalleTicket" runat="server" BackColor="White" AutoGenerateColumns="False" GridLines="Both" CssClass="grilla" Width="97%" RowStyle-VerticalAlign="Bottom" AllowPaging="true" OnPageIndexChanging="grvSubDetalleTicket_PageIndexChanging" AllowSorting="True" OnSorting="grvSubDetalleTicket_Sorting" OnRowDataBound="grvSubDetalleTicket_RowDataBound">
                      <SelectedRowStyle CssClass="grillaFila" />
                      <PagerStyle CssClass="grillaPaginacion" />
                      <HeaderStyle CssClass="headerGridAbsolute" />
                      <AlternatingRowStyle CssClass="grillaFila" />
                      <Columns>
                        <asp:BoundField HeaderText="N&#250;m. Ticket" DataField="Cod_Numero_Ticket" SortExpression="Cod_Numero_Ticket" Visible="false" />
                        <asp:BoundField HeaderText="Nro." DataField="Num_Secuencial" SortExpression="Num_Secuencial"/>
                        <asp:TemplateField HeaderText="Fecha Proceso" SortExpression="Log_Fecha_Mod">
                             <ItemTemplate >
                                <asp:Label ID="lblFechaVuelo0" runat="server"                            
                                    Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> ' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado" SortExpression="Dsc_Ticket_Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblITEstado0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Dsc_Ticket_Estado"), Eval("Tip_Estado")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Usuario Proceso" DataField="Nom_Usuario" SortExpression="Nom_Equipo" />
                        <asp:TemplateField HeaderText="Equipo" SortExpression="Dsc_Ticket_Estado">
                            <ItemTemplate>
                                <asp:Label ID="lblITEquipo0" runat="server" Text='<%# String.Format("{0} ( {1} )", Eval("Nom_Equipo"), Eval("Cod_Equipo_Mod")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField HeaderText="Nro. de Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                        <asp:BoundField HeaderText="Observaciones" DataField="Dsc_Obs" SortExpression="Dsc_Obs" />
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

