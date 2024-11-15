<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alr_VerAlarma.aspx.cs" Inherits="Alr_VerAlarma" %>

<%@ Register src="UserControl/PiePagina.ascx" tagname="PiePagina" tagprefix="uc2" %>
<%@ Register src="UserControl/CabeceraMenu.ascx" tagname="CabeceraMenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>LAP - Configuración de Alarma</title>
     <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    

</head>
<body>
    <form id="form1" runat="server">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server" />
                </td>
            </tr>
            
            <tr class="formularioTitulo">
                <td align="left">
                    <asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" CssClass="Boton" />&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <hr class="EspacioLinea" color="#0099cc" />
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table cellpadding="0" cellspacing="0" class="EspacioSubTablaPrincipal">
                            <tr>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                                <td class="CenterGrid" style="height: 115px">
                                             <asp:ScriptManager ID="smgAlarma" runat="server">
                                             </asp:ScriptManager>
                                            <asp:UpdatePanel ID="upnUsuario" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gwvAlarma" runat="server" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" CssClass="grilla" 
                                                        OnPageIndexChanging="gwvAlarma_PageIndexChanging" OnSorting="gwvAlarma_Sorting" 
                                                        Width="100%">
                                                        <Columns>
                                                            <asp:HyperLinkField DataNavigateUrlFields="Cod_Alarma, Cod_Modulo" 
                                                                DataNavigateUrlFormatString="Alr_ModificarAlarma.aspx?Cod_Alarma={0}&amp;Cod_Modulo={1}" 
                                                                DataTextField="Cod_Alarma" HeaderText="Código " 
                                                                NavigateUrl="Alr_ModificarAlarma.aspx" SortExpression="Cod_Alarma" />
                                                            <asp:BoundField DataField="Cod_Alarma" HeaderText="Código " 
                                                                SortExpression="Cod_Alarma" />
                                                            <asp:BoundField DataField="Dsc_Alarma" HeaderText="Tipo de Alarma" 
                                                                SortExpression="Dsc_Alarma" />
                                                            <asp:BoundField DataField="Dsc_Modulo" HeaderText="Modulo" 
                                                                SortExpression="Dsc_Modulo" />
                                                            <asp:BoundField DataField="Cod_Modulo" HeaderText="Cod_Modulo" 
                                                                Visible="False" />
                                                        </Columns>
                                                        <SelectedRowStyle CssClass="grillaFila" />
                                                        <PagerStyle CssClass="grillaPaginacion" />
                                                        <HeaderStyle CssClass="grillaCabecera" />
                                                        <AlternatingRowStyle CssClass="grillaFila" />
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
        <p>
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtPaginacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
        </p>
        </form>
</body>
</html>
