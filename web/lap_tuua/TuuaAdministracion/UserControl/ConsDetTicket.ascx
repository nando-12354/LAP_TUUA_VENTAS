<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConsDetTicket.ascx.cs"
    Inherits="UserControl_ConsDetTicket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 105px;
    }
    .style2
    {
        color: #000080;
    }
</style>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalDetalleTicket" Style="display: none" runat="server"
    Width="1000px" Height="460px">
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
                        <asp:Label ID="lblDetalleTicket" runat="server" CssClass="titulosecundario"></asp:Label>
                    </td>
                    <td align="center" class="Espacio1FilaTabla" rowspan="14">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 2%;" rowspan="8">
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="lblNumTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetNumTicket" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="lblTipoTicket" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblDetTipoTicket" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px"></asp:Label>
                    </td>
                    <td style="width: 2%;" rowspan="8">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetTipoVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoPasajero" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetTipoPasajero" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px"></asp:Label>
                    </td>
                    <td style="width: 8%;">
                        <asp:Label ID="lblTipoTrasbordo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td style="width: 24%;">
                        <asp:Label ID="lblDetTipoTrasbordo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Height="16px" Style="width: 100%;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCompania" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblDetCompania" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetFechaVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTurno" runat="server" CssClass="TextoFiltro">Turno:</asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetTurno" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEstado" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetEstado" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetNumVuelo" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFechaVencimiento" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetFchVencimiento" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPrecio" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetPrecio" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblFormaPago" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetFormaPago" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblReferencia" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetReferencia" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblModalidadVenta" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetModalidadVenta" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" Style="width: 100%;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoCobro" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetTipoCobro" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExtension" runat="server" CssClass="TextoFiltro"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDetExtension" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblContingencia" runat="server" CssClass="TextoFiltro">Contingencia:</asp:Label>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblDetContingencia" runat="server" CssClass="TextoFiltro" Font-Bold="True"
                            Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td class="style2">
                        Flag_Sincroniza:</td>
                    <td>
                        &nbsp;
                        <asp:Label ID="lblDetSincronizacion" runat="server" CssClass="TextoFiltro" 
                            Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="msgNoHist" runat="server" />
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <div style="width: 96%;">
                            <div style="height: 200px; width: 100%; overflow: auto; text-align: left">
                                <asp:GridView ID="grvSubDetalleTicket" runat="server" BackColor="White" AutoGenerateColumns="False"
                                    CssClass="grilla" Width="97%" RowStyle-VerticalAlign="Bottom" AllowPaging="False"
                                    OnPageIndexChanging="grvSubDetalleTicket_PageIndexChanging" AllowSorting="True"
                                    OnSorting="grvSubDetalleTicket_Sorting" OnRowDataBound="grvSubDetalleTicket_RowDataBound">
                                    <SelectedRowStyle CssClass="grillaFila" />
                                    <PagerStyle CssClass="grillaPaginacion" />
                                    <HeaderStyle CssClass="headerGridAbsolute" />
                                    <AlternatingRowStyle CssClass="grillaFila" />
                                    <RowStyle VerticalAlign="Bottom"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="N&#250;m. Ticket" DataField="Cod_Numero_Ticket" SortExpression="Cod_Numero_Ticket"
                                            Visible="false" />
                                        <asp:BoundField HeaderText="Nro." DataField="Num_Secuencial" SortExpression="Num_Secuencial" />
                                        <asp:BoundField HeaderText="Fecha Proceso" DataField="FechaProceso" SortExpression="FechaProceso" />
                                        <asp:BoundField HeaderText="Estado" DataField="Dsc_Estado_Add" SortExpression="Dsc_Estado_Add" />
                                        <asp:BoundField HeaderText="Usuario Proceso" DataField="Nom_Usuario" SortExpression="Nom_Usuario" />
                                        <asp:BoundField HeaderText="Equipo" DataField="Nom_Equipo" SortExpression="Nom_Equipo" />
                                        <%--<asp:BoundField HeaderText="Sincronizacion" DataField="Flg_Sincroniza" SortExpression="Flg_Sincroniza" />--%>
                                        <asp:BoundField HeaderText="Nro. de Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                        <asp:BoundField HeaderText="Observaciones" DataField="Dsc_Obs" SortExpression="Dsc_Obs" />
                                    </Columns> 
                                    <EmptyDataTemplate>
                                        &nbsp;
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8">
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
                <asp:Label ID="txtOrdenacionDet" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="txtColumnaDet" runat="server" Visible="False"></asp:Label>
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

<p>
    &nbsp;</p>


