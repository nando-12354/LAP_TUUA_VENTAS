<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IngBCBPAsociado.ascx.cs" Inherits="UserControl_IngBCBPAsociado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<style type="text/css">
    .style1
    {
        width: 125px;
    }
</style>



<asp:Button ID="BtnInvisible2" Text="Invisible" runat="server" Style="display: none" />
<cc1:ModalPopupExtender ID="mpextIngBoarding" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="BtnInvisible2" PopupControlID="pnlPopupIngBoarding" OkControlID="btnCerrarPopup"
    OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopupIngBoarding" CssClass="modalDetalleTurno" runat="server" 
    Width="470px" Height="200px" HorizontalAlign="Center" Style="display: none">
    <br />
    <br />
    <br />
    <table width="100%">
        <tr>
            <td colspan="3">
            <span class="TituloBienvenida">Asociación Manual BCBP</span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <hr color="#0099cc" style="width: 100%; height: 0px" />
            </td>
            <td width="10px">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td >
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblNumVuelo" runat="server" CssClass="TextoFiltro">(*)Nro. Vuelo:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNumVuelo" runat="server" CssClass="textbox" MaxLength="10" onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblFechaVuelo" runat="server" CssClass="TextoFiltro">(*)Fecha Vuelo:</asp:Label>
                        </td>
                        <td style="text-align: left">
                        
                            <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                            <td class="style1">
                                    <asp:TextBox ID="txtFechaVuelo" runat="server" BackColor="#E4E2DC" 
                                    CssClass="textbox" onfocus="this.blur();" 
                                    onkeypress="JavaScript: window.event.keyCode = 0;"></asp:TextBox>                            

                                    <cc1:CalendarExtender ID="txtFechaVuelo_CalendarExtender" runat="server" 
                                        Enabled="True" PopupButtonID="imgbtnCalendar" 
                                        TargetControlID="txtFechaVuelo" Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>                                    
                            </td>
                            <td>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" Height="22px" 
                                    ImageUrl="~/Imagenes/Calendar.bmp" Width="22px" />                            
                            </td>                            
                            </tr>                            
                            </table>
                        


                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <asp:Label ID="lblAsiento" runat="server" CssClass="TextoFiltro" Text="Asiento:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAsiento" runat="server" CssClass="textbox" MaxLength="10"  onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                        </td>
                        <td style="text-align: left">
                            <asp:Label ID="lblPasajero" runat="server" CssClass="TextoFiltro" Text="Pasajero:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtPasajero" runat="server" CssClass="textbox" Width="150px" 
                                MaxLength="50"  onkeypress="JavaScript: Tecla('Alphanumeric');"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblError" runat="server" CssClass="mensaje"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <table cellpadding="0" cellspacing="0" width="50%" align="center">
                    <tr>
                        <td width="50%" align="center" >
                            <asp:Button ID="btnAceptarPopup" CssClass="Boton" runat="server"
                                OnClientClick="return mandarValores()" Text="Aceptar" Width="80px" />
                        </td>
                        <td width="50%" align="center">  
                            <asp:Button ID="btnCerrarPopup" runat="server" CssClass="Boton" Text="Cancelar" 
                                OnClientClick="reinicarControlPopup()" Width="80px" />        
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    
</asp:Panel>
