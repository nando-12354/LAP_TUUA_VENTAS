<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mnt_VerTipoTicket.aspx.cs" Inherits="Modulo_VerTipoTicket" %>
<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Mantenimiento de Tipo Ticket</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
     <!-- #INCLUDE file="javascript/Functions.js" -->
     <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
  
</head>
<body>
    <form id="form1" runat="server">
    
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left" colspan="9">
                    &nbsp;<asp:Button ID="btnNuevo" runat="server" CssClass="Boton" OnClick="btnNuevo_Click"  /></td>
            </tr>
            <tr>
                <td colspan="9">
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td colspan="9">
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gwvTipoTicket" 
    runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="3"
                                   Width="100%" BackColor="White" 
    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CssClass="grilla" 
    OnPageIndexChanging="gwvTipoTicket_PageIndexChanging" AllowSorting="True" 
    OnSorting="gwvTipoTicket_Sorting">
                                                <SelectedRowStyle  CssClass="grillaFila"  />
                                                <PagerStyle  CssClass="grillaPaginacion"/>
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                <Columns>
                                                    <asp:HyperLinkField DataNavigateUrlFields="Cod_Tipo_Ticket" DataNavigateUrlFormatString="Mnt_ModificarTipoTicket.aspx?Cod_TipoTicket={0}"
                                            DataTextField="Cod_Tipo_Ticket" HeaderText="Código " 
                                            SortExpression="Cod_Tipo_Ticket" />
                                                    <asp:BoundField DataField="Cod_Tipo_Ticket" HeaderText="Código " 
                                            SortExpression="Cod_Tipo_Ticket" />
                                                    <asp:BoundField DataField="Dsc_Tipo_Ticket" HeaderText="Descripci&#243;n" 
                                            SortExpression="Dsc_Tipo_Ticket" />
                                                    <asp:BoundField DataField="Dsc_Tip_Vuelo" HeaderText="Tipo de Vuelo" 
                                            SortExpression="Dsc_Tip_Vuelo" />
                                                    <asp:BoundField DataField="Dsc_Tip_Pasajero" HeaderText="Tipo de Pasajero" 
                                            SortExpression="Dsc_Tip_Pasajero" />
                                                    <asp:BoundField DataField="Dsc_Tip_Trasbordo" HeaderText="Tipo Trasbordo" 
                                            SortExpression="Dsc_Tip_Trasbordo" />
                                                    <asp:BoundField DataField="Dsc_Moneda" HeaderText="Moneda" 
                                            SortExpression="Dsc_Moneda" />
                                                    <asp:BoundField DataField="Imp_Precio" HeaderText="Precio" 
                                            SortExpression="Imp_Precio" />
                                                    <asp:BoundField DataField="Dsc_Tip_Estado" HeaderText="Estado" 
                                            SortExpression="Dsc_Tip_Estado" />
                                                     <asp:TemplateField HeaderText="Fecha Modificación" 
                                                                SortExpression="Log_Fecha_Hora_Mod">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITFecha" runat="server" 
                                                                        Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                                                </ItemTemplate>
                                                      </asp:TemplateField>
                                                    <asp:BoundField DataField="Nom_Usuario" 
                                                        HeaderText="Usuario Modificación" SortExpression="Nom_Usuario" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:UpdateProgress ID="upgUsuario" runat="server">
                                                <progresstemplate>
                                                    <asp:Label ID="lblProcesando" runat="server" style="text-align: left" 
                                                        Text="Procesando"></asp:Label>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server" />
                </td>
            </tr>
        </table>
                                <asp:ObjectDataSource ID="odsListarTipoTicket" runat="server" SelectMethod="listaDeTipoTicket"
                                    TypeName="DAOService.Service"></asp:ObjectDataSource>
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
        &nbsp;&nbsp;
    </form>
</body>
</html>
