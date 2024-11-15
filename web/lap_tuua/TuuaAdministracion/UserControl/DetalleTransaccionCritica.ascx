<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DetalleTransaccionCritica.ascx.cs" Inherits="UserControl_DetalleTransaccionCritica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Button ID="BtnInvisible" Text="Invisible" runat="server" Style="display: none" />

<cc1:ModalPopupExtender ID="mpext" runat="server"  BackgroundCssClass="modalBackground" TargetControlID="BtnInvisible" PopupControlID="pnlPopup" OkControlID="btnCerrarPopup" OnOkScript="onOk()" DropShadow="true">
</cc1:ModalPopupExtender>
<asp:Panel ID="pnlPopup" CssClass="modalDetalleTicket"  Style="display: none"  runat="server" Width="1000px"  Height="560px">
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <br />
      <br />
      <table border="0" cellpadding="0" cellspacing="0" class="TamanoTabla" >
          <tr>
              <td align="center" class="Espacio1FilaTabla" rowspan="8">
                    &nbsp;&nbsp;</td>
              <td align="left" colspan="3">
                    <asp:Label ID="lblDetalleTicket" runat="server" CssClass="titulosecundario"></asp:Label>
                </td>
              <td align="center" class="Espacio1FilaTabla" rowspan="8">
                    &nbsp;&nbsp;</td>
          </tr>
          <tr>
              <td colspan="3">
            <hr color="#0099cc" style="width: 100%; height: 0px" />
              </td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="lblUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
              <td>
                  <asp:Label ID="lblDetUsuario" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Width="189px"></asp:Label>
                </td>
              <td rowspan="3">
<%--                  <img src="../Imagenes/print.jpg" class="BotonImprimir" id="imgPrint" 
                      language="javascript" onclick="return imgPrint_onclick()" 
                      style="height: 26px; width: 27px"/>--%>
              </td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="lblRolesUsuario" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
              <td>
                  <asp:Label ID="lblDetRolesUsuario" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Height="16px"></asp:Label>
                </td>
          </tr>
          <tr>
              <td>
                  <asp:Label ID="lblTablaMod" runat="server" CssClass="TextoFiltro"></asp:Label>
                </td>
              <td>
                  <asp:Label ID="lblDetTablaMod" runat="server" CssClass="TextoFiltro" 
                        Font-Bold="True" Font-Names="Arial" Font-Size="Small" ForeColor="#008FD5" 
                        Height="16px"></asp:Label>
                </td>
          </tr>
          <tr>
              <td colspan="3">
            <hr color="#0099cc" style="width: 100%; height: 0px" />
              </td>
          </tr>
          <tr>
              <td  colspan="3">
                    <asp:GridView ID="grvDetalleAuditoria" runat="server" BackColor="White" 
                        AutoGenerateColumns="False" CssClass="grilla" Width="97%" 
                        RowStyle-VerticalAlign="Bottom" AllowPaging="True" 
                        OnPageIndexChanging="grvDetalleAuditoria_PageIndexChanging" AllowSorting="True" 
                        OnSorting="grvDetalleAuditoria_Sorting" 
                        onrowdatabound="grvDetalleAuditoria_RowDataBound">
                      <SelectedRowStyle CssClass="grillaFila" />
                      <PagerStyle CssClass="grillaPaginacion" />
                      <HeaderStyle CssClass="headerGridAbsolute" />
                      <AlternatingRowStyle CssClass="grillaFila" />
                <RowStyle VerticalAlign="Bottom"></RowStyle>
                      <Columns>
                        <asp:BoundField HeaderText="Columna" DataField="Columna" 
                              SortExpression="Columna" >
                            <HeaderStyle Width="150px" />
                          </asp:BoundField>
                        <asp:BoundField HeaderText="Registro Anterior" DataField="ValorOriginal" 
                              SortExpression="ValorOriginal" >
                            <HeaderStyle Width="150px" />
                          </asp:BoundField>
                        <asp:BoundField HeaderText="Registro Ultimo" DataField="ValorNuevo" 
                              SortExpression="ValorNuevo" >
                            <HeaderStyle Width="150px" />
                          </asp:BoundField>
                      </Columns>
                      <EmptyDataTemplate>
                        &nbsp;
                      </EmptyDataTemplate>
                    </asp:GridView>
                  </td>
          </tr>
          <tr>
              <td align="left" colspan="3">
        
                  <asp:Label ID="msgNoHist" runat="server" />
        
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
  
   <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
   <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
   <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label> 
        
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
    
//        function imgPrint_onclick() {
////        alert(document.getElementById("txtRolesrpt"));
////            var sUsuario = document.getElementById("txtUsuariorpt").value;
////            var sRoles = document.getElementById("txtRolesrpt").value;
////            var sNombreTabla = document.getElementById("txtNomTablarpt").value;
////            var sContador = document.getElementById("txtContadorrpt").value;
//           
//	        
//	        var ventimp = window.open("ReporteCNS/rptAuditoriaDetalle.aspx" + "?sUsuario=" + sUsuario + "&sRoles=" + sRoles + "&sNombreTabla=" + sNombreTabla + "&sContador=" + sContador,"mywindow","location=0,status=0,scrollbars=1,menubar=0,width=900,height=800");
//            ventimp.focus();
//    }
</script>
