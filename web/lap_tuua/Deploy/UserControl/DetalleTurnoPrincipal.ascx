<%@ control language="C#" autoeventwireup="true" inherits="UserControl_DetalleTurnoPrincipal, App_Web_6uwanhbu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="CnsDetTurno.ascx" tagname="CnsDetTurno" tagprefix="uc1" %>

<asp:Button ID="BtnInvisibleTurno" Text="Invisible" runat="server" Style="display: none" />


<cc1:ModalPopupExtender ID="mpextDetalleTurno" runat="server" BackgroundCssClass="modalBackground" TargetControlID="BtnInvisibleTurno" PopupControlID="pnlPopupTurno" OkControlID="btnCerrarPopupTurnoPrincipal" OnOkScript="onOkTurnoPrincipal()" DropShadow="true">
</cc1:ModalPopupExtender>

<asp:Panel ID="pnlPopupTurno" CssClass="modalDetalleTicket" Style="display: none" runat="server" Width="1000px"  Height="410px">
  <asp:UpdatePanel ID="UpdatePanelTurno" runat="server">
    <ContentTemplate>



      <br />
      <br />
      <table align="center" border="0" cellpadding="0" cellspacing="0" width="95%">

            <tr>
                <td colspan="11">
                    
                </td>
            </tr>
            <tr >
                <td colspan="11">
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td style="width: 80px; height: 11px">
                    <asp:Label ID="lblCodTurno" runat="server" CssClass="TextoFiltro"></asp:Label></td>
                <td style="width: 84px; height: 11px">
                    <asp:Label ID="lblDetCodTurno" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Width="205px"></asp:Label></td>
                <td style="height: 11px">
                    &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
                <td style="width: 54px; height: 11px">
                </td>
                <td style="width: 83px; height: 11px">
                </td>
                <td style="height: 11px">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
                <td style="width: 49px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="height: 11px">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                </td>
                <td align="center" style="height: 11px">
                </td>
                <td style="width: 100px; height: 11px">
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td style="width: 80px; height: 11px">
                    <asp:Label ID="lblEquipo" runat="server" CssClass="TextoFiltro"></asp:Label></td>
                <td style="width: 84px; height: 11px">
                    <asp:Label ID="lblDetEquipo" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Height="16px" Width="205px"></asp:Label></td>
                <td style="height: 11px">
                </td>
                <td style="width: 54px; height: 11px">
                </td>
                <td style="width: 83px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="width: 49px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td align="center" style="height: 11px">
                </td>
                <td style="width: 100px; height: 11px">
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td style="width: 80px; height: 11px">
                    <asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label></td>
                <td style="width: 84px; height: 11px">
                    <asp:Label ID="lblDetUsuario" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Height="18px" Width="352px"></asp:Label></td>
                <td style="height: 11px">
                </td>
                <td style="width: 54px; height: 11px">
                </td>
                <td style="width: 83px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="width: 49px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td align="center" style="height: 11px">
                </td>
                <td style="width: 100px; height: 11px">
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td style="width: 80px; height: 11px">
                    <asp:Label ID="lblFchHoraIni" runat="server" CssClass="TextoFiltro" ></asp:Label></td>
                <td style="width: 84px; height: 11px">
                    <asp:Label ID="lblDetFchHoraIni" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Width="138px"></asp:Label></td>
                <td style="height: 11px">
                </td>
                <td style="width: 54px; height: 11px">
                </td>
                <td style="width: 83px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td>
                </td>
                <td style="height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td >
                </td>
                <td>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td >
                    <asp:Label ID="lblFchHoraFin" runat="server" CssClass="TextoFiltro"></asp:Label></td>
                <td>
                    <asp:Label ID="lblDetFchHoraFin" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Width="136px"></asp:Label></td>
                <td style="height: 11px">
                </td>
                <td style="width: 54px; height: 11px">
                </td>
                <td style="width: 83px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="width: 49px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td align="center" style="height: 11px">
                </td>
                <td style="width: 100px; height: 11px">
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <hr color="#0099cc" style="width: 100%; height: 0px" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="11">
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="mensaje" Width="200px"></asp:Label>
                                            
                                                <asp:Label ID="lblMensajeErrorData" runat="server" 
                        CssClass="msgMensaje"></asp:Label>
                                            
                                                </td>
            </tr>
            <tr>
                <td colspan="11">
                
                <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td >
                                &nbsp;

                     <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="height: 11px">
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                            <td style="height: 11px">
                                            <asp:Label ID="Label2" runat="server"></asp:Label></td>
                            <td style="height: 11px">
                                <asp:Label ID="Label3" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                     </table>
                
                
                <DIV STYLE="overflow: auto; width: 970px; height: 380px; 
            border-left: 0px gray solid; border-bottom: 0px gray solid; 
            padding:0px; margin: 0px">

                    <table border="0" cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                        <tr>
                            <td class="SpacingGrid">
                            </td>
                            <td >
                                &nbsp;

                     <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="height: 11px">
                                <asp:Label ID="lblMoneda1" runat="server"></asp:Label></td>
                            <td style="height: 11px">
                                            <asp:Label ID="lblMoneda2" runat="server"></asp:Label></td>
                            <td style="height: 11px">
                                <asp:Label ID="lblMoneda3" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                    <asp:GridView ID="grvMoneda1" runat="server" AutoGenerateColumns="False" Width="80%" 
                                    OnRowDataBound="grvMoneda1_RowDataBound" onrowcommand="grvMoneda1_RowCommand">
                        
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                              </ItemTemplate>
                                <ControlStyle Font-Names="Verdana" Font-Size="X-Small" />
                                <HeaderStyle BorderStyle="None" />
                                <ItemStyle Font-Overline="False" />
                            </asp:TemplateField>
                                        
                            <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" ShowHeader="False" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>                           
                        </Columns>

                        <PagerStyle  CssClass="grillaPaginacion"/>
                        <HeaderStyle BorderStyle="None" />


                    </asp:GridView>
                            </td>
                            <td>
                    <asp:GridView ID="grvMoneda2" runat="server" AutoGenerateColumns="False" Width="80%" 
                                    OnRowDataBound="grvMoneda2_RowDataBound" onrowcommand="grvMoneda2_RowCommand">
                        <Columns>
                            <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                              </ItemTemplate>
                                <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                <HeaderStyle BorderStyle="None" />
                                <ItemStyle Font-Overline="False" />
                            </asp:TemplateField>
                                                          
                            <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="monto" ShowHeader="False" >
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle BorderStyle="None" />
                            </asp:BoundField>
                        </Columns>
                        
                        <PagerStyle  CssClass="grillaPaginacion"/>
         
       
                    </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="grvMoneda3" runat="server" AutoGenerateColumns="False" 
                                    Width="80%" OnRowDataBound="grvMoneda3_RowDataBound" 
                                    onrowcommand="grvMoneda3_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                          </ItemTemplate>
                                            <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                            <HeaderStyle BorderStyle="None" />
                                            <ItemStyle Font-Overline="False" />
                                        </asp:TemplateField>                                  
                                    
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                   
                                    <PagerStyle  CssClass="grillaPaginacion"/>
                                    
                                   
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5%">
                                &nbsp;&nbsp;
                            </td>
                            <td style="height: 5%">
                            </td>
                            <td style="height: 5%">
                            </td>
                        </tr>
                                    <tr>
                                        <td>
                                <asp:Label ID="lblMoneda4" runat="server"></asp:Label></td>
                                        <td>
                                <asp:Label ID="lblMoneda5" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblMoneda6" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                <asp:GridView ID="grvMoneda4" runat="server" AutoGenerateColumns="False" Width="80%" 
                                                OnRowDataBound="grvMoneda4_RowDataBound" onrowcommand="grvMoneda4_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                          </ItemTemplate>
                                            <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                            <HeaderStyle BorderStyle="None" />
                                            <ItemStyle Font-Overline="False" />
                                        </asp:TemplateField>                                        
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                   
                                    <PagerStyle  CssClass="grillaPaginacion"/>
                                    
                                   
                                </asp:GridView>
                                        </td>
                                        <td>
                                 <asp:GridView ID="grvMoneda5" runat="server" AutoGenerateColumns="False" Width="80%" 
                                                OnRowDataBound="grvMoneda5_RowDataBound" onrowcommand="grvMoneda5_RowCommand">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                          </ItemTemplate>
                                            <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                            <HeaderStyle BorderStyle="None" />
                                            <ItemStyle Font-Overline="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                    
                                    <PagerStyle  CssClass="grillaPaginacion"/>
                                    
                                  
                                </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:GridView ID="grvMoneda6" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda6_RowDataBound" 
                                                onrowcommand="grvMoneda6_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5%">
                                            &nbsp;&nbsp;
                                        </td>
                                        <td style="height: 5%">
                                        </td>
                                        <td style="height: 5%">
                                        </td>
                                    </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMoneda7" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblMoneda8" runat="server"></asp:Label></td>
                            <td>
                                <asp:Label ID="lblMoneda9" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td><asp:GridView ID="grvMoneda7" runat="server" AutoGenerateColumns="False" 
                                    Width="80%" OnRowDataBound="grvMoneda7_RowDataBound" 
                                    onrowcommand="grvMoneda7_RowCommand">
                                 <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                          </ItemTemplate>
                                            <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                            <HeaderStyle BorderStyle="None" />
                                            <ItemStyle Font-Overline="False" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                <PagerStyle  CssClass="grillaPaginacion"/>
                            </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="grvMoneda8" runat="server" AutoGenerateColumns="False" 
                                    Width="80%" OnRowDataBound="grvMoneda8_RowDataBound" 
                                    onrowcommand="grvMoneda8_RowCommand">
                                     <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                          </ItemTemplate>
                                            <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                            <HeaderStyle BorderStyle="None" />
                                            <ItemStyle Font-Overline="False" />
                                        </asp:TemplateField>                                                                               
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle  CssClass="grillaPaginacion"/>
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="grvMoneda9" runat="server" AutoGenerateColumns="False" 
                                    Width="80%" OnRowDataBound="grvMoneda9_RowDataBound" 
                                    onrowcommand="grvMoneda9_RowCommand">
                                    <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                      <ItemTemplate>
                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                      </ItemTemplate>
                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                        <HeaderStyle BorderStyle="None" />
                                        <ItemStyle Font-Overline="False" />
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="monto" ShowHeader="False" >
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle BorderStyle="None" />
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle  CssClass="grillaPaginacion"/>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5%">
                                &nbsp; &nbsp; &nbsp;
                            </td>
                            <td style="height: 5%">
                            </td>
                            <td style="height: 5%">
                            </td>
                        </tr>
                                    <tr>
                                        <td style="height: 11px">
                                            <asp:Label ID="lblMoneda10" runat="server"></asp:Label></td>
                                        <td style="height: 11px">
                                            <asp:Label ID="lblMoneda11" runat="server"></asp:Label></td>
                                        <td style="height: 11px">
                                            <asp:Label ID="lblMoneda12" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:GridView ID="grvMoneda10" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda10_RowDataBound" 
                                                onrowcommand="grvMoneda10_RowCommand">
                                             <Columns>
                                                <asp:TemplateField ShowHeader="False">
                                                  <ItemTemplate>
                                                    <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                  </ItemTemplate>
                                                    <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <ItemStyle Font-Overline="False" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle BorderStyle="None" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="monto" ShowHeader="False" >
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle BorderStyle="None" />
                                                </asp:BoundField>
                                            </Columns>
                                            <PagerStyle  CssClass="grillaPaginacion"/>
                                        </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:GridView ID="grvMoneda11" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda11_RowDataBound" 
                                                onrowcommand="grvMoneda11_RowCommand">
                                                 <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                            </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:GridView ID="grvMoneda12" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda12_RowDataBound" 
                                                onrowcommand="grvMoneda12_RowCommand">
                                                 <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                        <tr>
                            <td style="height: 2%">
                                &nbsp;&nbsp;
                                </td>
                            <td style="height: 2%">
                            </td>
                            <td style="height: 2%">
                            </td>
                        </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMoneda13" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblMoneda14" runat="server"></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblMoneda15" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:GridView ID="grvMoneda13" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda13_RowDataBound" 
                                                onrowcommand="grvMoneda13_RowCommand">
                                             <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                            <PagerStyle  CssClass="grillaPaginacion"/>
                                        </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:GridView ID="grvMoneda14" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda14_RowDataBound" 
                                                onrowcommand="grvMoneda14_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                            </asp:GridView>
                                        </td>
                                        <td>
                                            <asp:GridView ID="grvMoneda15" runat="server" AutoGenerateColumns="False" 
                                                Width="80%" OnRowDataBound="grvMoneda15_RowDataBound" 
                                                onrowcommand="grvMoneda15_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField ShowHeader="False">
                                                      <ItemTemplate>
                                                        <asp:LinkButton ID="codTicket" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowDetTurno" Text='<%# Eval("Descripcion") %>' />
                                                      </ItemTemplate>
                                                        <ControlStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <ItemStyle Font-Overline="False" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="cantidad" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="monto" ShowHeader="False" >
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle BorderStyle="None" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 5%">
                                            &nbsp; &nbsp;
                                        </td>
                                        <td style="height: 5%">
                                        </td>
                                        <td style="height: 5%">
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
            <tr>
                <td>
                    
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
        <asp:Button ID="btnCerrarPopupTurnoPrincipal" runat="server" Text="Cerrar" CssClass="Boton" />
      </td>
    </tr>
  </table>
</asp:Panel>

<uc1:CnsDetTurno ID="CnsDetTurno" runat="server" />


<script language="javascript" type="text/javascript">
    function onOkTurnoPrincipal() {
        //document.getElementById('pnlPopup').display="none";

        //document.getElementById('pnlPopup').visible="false";
        //      //document.getElementById('pnlPopup').style="display:none";
        //      //document.getElementById('mpext').display="none";
        //      //document.getElementById('mpext').visible="false";
        //      //__doPostBack('btnCerrarPopup','');
    }
</script>
