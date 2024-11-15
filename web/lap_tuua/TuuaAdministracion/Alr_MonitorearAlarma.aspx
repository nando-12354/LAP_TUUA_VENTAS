<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alr_MonitorearAlarma.aspx.cs" Inherits="Alr_MonitorearAlarma" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register src="UserControl/CabeceraMenu.ascx" tagname="CabeceraMenu" tagprefix="uc1" %>
<%@ Register src="UserControl/PiePagina.ascx" tagname="PiePagina" tagprefix="uc2" %>
<%@ Register src="UserControl/OKMessageBox.ascx" tagname="okmessagebox" tagprefix="uc3" %>
<%@ Register src="UserControl/AtencionAlarma.ascx" tagname="AtencionAlarma" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>LAP - Monitorear Alarmas Generadas</title>
      <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
  <link href="css/Style.css" rel="stylesheet" type="text/css" />
  <!-- #INCLUDE file="javascript/KeyPress.js" -->
  <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
  <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
  <!-- #INCLUDE file="javascript/Functions.js" -->
  
</head>
<body>
    <form id="form1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="False">
  </asp:ScriptManager>
            <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
                <tr>
                    <td class="Espacio1FilaTabla" style="height: 11px">
                                                
                        <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                    </td>
                </tr>
                <tr class="formularioTitulo">
                    <td align="right" style="text-align: left">
                    <table border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblMensajeTotalAlarmas" runat="server" CssClass="TextoEtiqueta" Width="178px" Height="16px"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:Label ID="lblMensajeTiempoAlarmas" runat="server" CssClass="TextoEtiqueta" 
                                Width="46px" Height="16px"></asp:Label>
                        </td>
                        <td>
                             <asp:Label ID="lblMensajeHoraAlarmas" runat="server" CssClass="TextoEtiqueta" 
                                 Width="26px" Height="16px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="txtTotalAlarmas" runat="server" CssClass="TextoEtiqueta" 
                                Width="75px" Height="16px"></asp:Label>
                        </td>   
                    </tr>
                    </table>  
                    <asp:Button ID="btnPrueba" runat="server" onclick="btnPrueba_Click" Text="PruebaAlarma" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr class="EspacioLinea" color="#0099cc" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="EspacioSubTablaPrincipal">
                            &nbsp;<table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                                <tr>
                                    <td class="SpacingGrid" style="height: 115px">
                                    </td>
                                    <td class="CenterGrid" style="height: 115px">
                                    <div id="Escrrollable" style="overflow: auto; height: 410px;">
                                        <table style="width: 100%;">
                                            <tr>                                                
                                                <td class="CenterGrid" align="left">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <table class="alineaderecha" 
                                                                style="width: 100%;">
                                                                <tr>
                                                                    <td align="left" class="CenterGrid">
                                                                        <asp:GridView ID="gwvMonitorearAlarma" runat="server" AllowPaging="True" 
                                                                            AllowSorting="True" AutoGenerateColumns="False" CssClass="grilla" 
                                                                            onpageindexchanging="gwvMonitorearAlarma_PageIndexChanging" 
                                                                            onrowcommand="gwvMonitorearAlarma_RowCommand" onsorting="gwvMonitorearAlarma_Sorting" 
                                                                            RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle">
                                                                            <SelectedRowStyle CssClass="grillaFila" />
                                                                            <PagerStyle CssClass="grillaPaginacion" />
                                                                            <HeaderStyle CssClass="grillaCabecera" />
                                                                            <AlternatingRowStyle CssClass="grillaFila" />
                                                                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Cod_Alarma" HeaderText="Alarma" 
                                                                                    SortExpression="Cod_Alarma" />
                                                                                <asp:BoundField DataField="Dsc_Modulo" HeaderText="Módulo" 
                                                                                    SortExpression="Dsc_Modulo" />
                                                                                <asp:BoundField DataField="Dsc_Alarma" HeaderText="Tipo Alarma" 
                                                                                    SortExpression="Dsc_Alarma" />
                                                                                <asp:BoundField DataField="Dsc_Equipo" HeaderText="Equipo" 
                                                                                    SortExpression="Dsc_Equipo" />
                                                                                <asp:BoundField DataField="Dsc_Body" HeaderText="Mensaje" 
                                                                                    SortExpression="Dsc_Body" HtmlEncode="False" 
                                                                                    HtmlEncodeFormatString="False" />
                                                                                <asp:BoundField DataField="Fch_Generacion" HeaderText="Fecha Generación" 
                                                                                    SortExpression="Fch_Generacion2" />
                                                                                <asp:BoundField DataField="Dsc_Tip_Importancia" HeaderText="Importancia" 
                                                                                    SortExpression="Dsc_Tip_Importancia" />
                                                                                <asp:TemplateField ItemStyle-Width="8%">
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="imbAtender" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="AtencionAlarma" 
                                                                                            ImageUrl="~/Imagenes/Icon_tools_large.png" />
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle Width="8%" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Cod_AlarmaGenerada" HeaderText="AlarmaGenerada" 
                                                                                    SortExpression="Cod_AlarmaGenerada" Visible="False" />
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                &nbsp;
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="style9" style="text-align: left; ">
                                                                        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" Width="427px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </tr>
                            </table>
                        </div>
                        <uc2:PiePagina ID="PiePagina3" runat="server" />
                    </td>
                </tr>
            </table>


  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
      <ContentTemplate>
          <uc5:AtencionAlarma ID="mpeAtencionAlarma" runat="server" />
      </ContentTemplate>
 </asp:UpdatePanel>
  
 
  
    </form>



</body>
</html>
