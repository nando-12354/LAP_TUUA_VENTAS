<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CnsDetBoardingPass.ascx.cs"
    Inherits="LAP.Tuua.Web.UserControl.CnsDetBoardingPass" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    var _source;
    var _popup;

    function cancelClick() {
        $get('ctl00_cphContenido_CnsDetBoardingPass1_pnlPopupBoarding').style.display = 'none';
    }
</script>

<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpextDetBoarding" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopupBoarding" OkControlID="btnCerrarPopup"
    OnOkScript="cancelClick();" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopupBoarding" CssClass="modalDetalleTurno" runat="server" Width="1000px"
    Height="500px" Style="display: none">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
                <tr>
                    <!-- TITLE - ZONE -->
                    <td align="center" class="Espacio1FilaTabla" rowspan="14">
                        &nbsp;&nbsp;
                    </td>
                    <td align="center" colspan="8">
                        <asp:Label ID="lblDetalleBoarding" runat="server" CssClass="titulosecundario"></asp:Label>
                    </td>
                    <td align="center" class="Espacio1FilaTabla" rowspan="14">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <!-- LINE - ZONE -->
                    <td colspan="8">
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <!-- FIRST ROW - ZONE -->
                    <td style="width: 2%;" rowspan="9">
                    </td>
                    <td style="width: 12%;">
                        <asp:Label ID="lblCodNumeroBCBP" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td style="width: 24%;">
                        <asp:Label ID="lblDetCodNumeroBCBP" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td style="width: 12%;">
                        <asp:Label ID="lblCorrelativo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblDetCorrelativo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td style="width: 2%;" rowspan="9">
                    </td>
                </tr>
                <tr>
                    <!-- SECOND ROW - ZONE -->
                    <td>
                        <asp:Label ID="lblTVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetTVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="18px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td style="width: 20%;">
                        <asp:Label ID="lblDetTPasajero" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoTrasbordo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetTipoTrasbordo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- THIRD ROW - ZONE -->
                    <td>
                        <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetCompania" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="18px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNumVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetFechaVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- FOURTH ROW - ZONE -->
                    <td>
                        <asp:Label ID="lblFechaVenc" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetFechaVenc" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEstadoActual" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetEstadoActual" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPrecio" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetPrecio" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- FIFTH ROW - ZONE -->
                    <td>
                        <asp:Label ID="lblNombrePasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNomPasajero" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumAsiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNumAsiento" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <!-- SIXTH ROW - ZONE -->
                    <td>
                        <asp:Label ID="lblModVenta" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetModVenta" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBCBPAsociados" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetBCBPAsociados" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoIngreso" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetTipIngreso" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- Septima Fila - ZONE -->
                    <td>
                        <asp:Label ID="lblNumeroBCBP" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNumeroBCBP" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDestino" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetDestino" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblETicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetETicket" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- Octava Fila - ZONE -->
                    <td>
                        <asp:Label ID="lblNroProcRehabilitacion" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNroProcRehabilitacion" runat="server" CssClass="TextoFiltro"
                            Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBloqueadoUso" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetBloqueadoUso" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblIncluyeTUUA" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetIncluyeTUUA" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!-- Novena Fila - ZONE -->
                    <td>
                        <asp:Label ID="lblInvocacionWBS" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetInvocacionWBS" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <div style="width: 96%;">
                            <div style="height: 250px; width: 100%; overflow: auto; text-align: left">
                                <asp:Label ID="Label1" runat="server" CssClass="titulosecundario" Text="Historial de Estados"></asp:Label>
                                <asp:GridView ID="grvBoardingEstHist" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    AllowPaging="False" CellPadding="3" CssClass="grilla" GridLines="Vertical" OnPageIndexChanging="grvBoardingEstHist_PageIndexChanging"
                                    OnRowDataBound="grvBoardingEstHist_RowDataBound" OnSorting="grvBoardingEstHist_Sorting"
                                    Width="100%">
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundField DataField="Num_Secuencial" HeaderText="Nro." SortExpression="Num_Secuencial" />
                                        <asp:BoundField DataField="FechaProceso" HeaderText="Fecha Proceso" SortExpression="FechaProceso" />
                                        <asp:BoundField DataField="Dsc_Boarding_Estado" HeaderText="Estado" SortExpression="Dsc_Boarding_Estado" />
                                        <asp:BoundField DataField="Cta_Usuario" HeaderText="Usuario Proceso" SortExpression="Cta_Usuario" />
                                        <asp:BoundField DataField="Nom_Equipo" HeaderText="Equipo" SortExpression="Nom_Equipo" />
                                        <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. de Vuelo" SortExpression="Num_Vuelo" />
                                        <asp:BoundField DataField="Dsc_Obs" HeaderText="Observaciones" SortExpression="Dsc_Obs" />
                                    </Columns>
                                    <SelectedRowStyle CssClass="grillaFila" />
                                    <PagerStyle CssClass="grillaPaginacion" />
                                    <HeaderStyle CssClass="grillaCabecera" />
                                    <AlternatingRowStyle CssClass="grillaFila" />
                                </asp:GridView>
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" />
                                <br />
                                <asp:Label ID="lblTituloAsociacos" runat="server" CssClass="titulosecundario" Text="Boarding Pass Asociados"></asp:Label>
                                <asp:GridView ID="grvBoardingAsociados" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" CssClass="grilla" GridLines="Vertical" Width="100%" AllowPaging="False">
                                    <Columns>
                                        <asp:BoundField DataField="Num_Secuencial_Bcbp_Rel_Sec" HeaderText="Nro. Asociación" />
                                        <asp:TemplateField HeaderText="Fecha Vuelo">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaVuelo0" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Fch_Vuelo")),null) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Num_Vuelo" HeaderText="Nro. Vuelo" />
                                        <asp:BoundField DataField="Num_Asiento" HeaderText="Nro. Asiento" />
                                        <asp:BoundField DataField="Nom_Pasajero" HeaderText="Pasajero" />
                                        <asp:BoundField DataField="Dsc_Campo" HeaderText="Estado" />
                                        <asp:TemplateField HeaderText="Fecha Asociación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaAsociacion" runat="server" Text='<%# LAP.EXTRANET.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMensajeError2" runat="server" CssClass="mensaje" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="95%">
        <tr>
            <td width="30px">
            </td>
            <td align="center">
                <asp:Label ID="txtOrdenacionDet" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="txtColumnaDet" runat="server" Visible="False"></asp:Label>
                <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" />
            </td>
        </tr>
    </table>
</asp:Panel>
