<%@ control language="C#" autoeventwireup="true" inherits="UserControl_CnsDetTurno, App_Web_i29mqlr7" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalDetalleTurno" Style="display: none" runat="server"
    Width="650px" Height="410px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" align="center">
                <tr>
                    <td align="center">
                        <br />
                        <asp:Label ID="lblDetalleTurno" runat="server" CssClass="titulosecundario"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
                <tr>
                    <!-- CONTENT -->
                    <td align="center">
                        <div style="width: 96%;">
                            <div style="height: 335px; width: 100%; overflow: auto; text-align: left">
                                <asp:GridView ID="grvSubDetalleTurno1" runat="server" BackColor="White" AutoGenerateColumns="False"
                                    CssClass="grilla" RowStyle-VerticalAlign="Bottom" AllowPaging="false" AllowSorting="true"
                                    GridLines="Both" OnSorting="grvSubDetalleTurno1_Sorting">
                                    <SelectedRowStyle CssClass="grillaFila" />
                                    <PagerStyle CssClass="grillaPaginacion" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <AlternatingRowStyle CssClass="grillaFila" />
                                    <RowStyle VerticalAlign="Bottom"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Item" DataField="Item" SortExpression="Item" />
                                        <asp:BoundField HeaderText="Nro. Ticket" DataField="Cod_Numero_Ticket" SortExpression="Cod_Numero_Ticket" />
                                        <asp:TemplateField HeaderText="Fecha Creación" SortExpression="Fch_Creacion">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaVuelo0" runat="server" Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha2(Convert.ToString(Eval("Fch_Creacion"))) %> '></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Código Compañía" DataField="Cod_Compania" SortExpression="Cod_Compania" />
                                        <asp:BoundField HeaderText="Nro. Vuelo" DataField="Dsc_Num_Vuelo" SortExpression="Dsc_Num_Vuelo" />
                                        <asp:BoundField HeaderText="Precio" DataField="Imp_Precio" SortExpression="Imp_Precio">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        &nbsp;
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:GridView ID="grvSubDetalleTurno2" runat="server" BackColor="White" AutoGenerateColumns="False"
                                    GridLines="Both" CssClass="grilla" RowStyle-VerticalAlign="Bottom" AllowPaging="false"
                                    AllowSorting="True" OnSorting="grvSubDetalleTurno2_Sorting">
                                    <SelectedRowStyle CssClass="grillaFila" />
                                    <PagerStyle CssClass="grillaPaginacion" />
                                    <HeaderStyle CssClass="headerGrid" />
                                    <AlternatingRowStyle CssClass="grillaFila" />
                                    <RowStyle VerticalAlign="Bottom"></RowStyle>
                                    <Columns>
                                        <asp:BoundField HeaderText="Item" DataField="Item" SortExpression="Item" />
                                        <asp:BoundField HeaderText="Nro. Operación" DataField="Num_Operacion" SortExpression="Num_Operacion" />
                                        <asp:BoundField HeaderText="Tipo Pago" DataField="Tip_Pago" SortExpression="Tip_Pago" />
                                        <asp:BoundField HeaderText="Fecha Proceso" DataField="Fch_Proceso" SortExpression="Fch_Proceso" />
                                        <asp:BoundField HeaderText="Usuario" DataField="Cta_Usuario" SortExpression="Cta_Usuario" />
                                        <asp:BoundField HeaderText="Tasa Cambio" DataField="Imp_Tasa_Cambio" SortExpression="Imp_Tasa_Cambio" />
                                        <asp:BoundField HeaderText="Imp. a Cambiar" DataField="Imp_Operacion" SortExpression="Imp_Operacion" />
                                        <asp:BoundField HeaderText="Imp. Cambiado" DataField="Imp_Cambiado" SortExpression="Imp_Cambiado">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        &nbsp;
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr color="#0099cc" style="width: 100%; height: 0px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <table align="center" border="0" cellpadding="0" cellspacing="0" width="70%">
        <tr>
            <td align="center">
                <asp:Button ID="btnCerrarPopup" runat="server" Text="Cerrar" CssClass="Boton" />
            </td>
        </tr>
    </table>
    <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblTurno" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblMoneda" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblDetalle" runat="server" Visible="False"></asp:Label>
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

