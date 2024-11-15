<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cfg_VerListaCampo.aspx.cs" Inherits="Cfg_VerListaCampo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu"  TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<%@ Register src="UserControl/CfgEditListaCampo.ascx" tagname="CfgEditListaCampo" tagprefix="uc3" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Configuración de Lista de Campos</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
       
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <!-- #INCLUDE file="javascript/Functions.js" -->
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
</head>
<body>
    <form id="form1" runat="server">
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu3" runat="server" />
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click" 
                        CssClass="Boton" />&nbsp;</td>
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
                                            <asp:UpdatePanel ID="upnUsuario" runat="server">
                                                <ContentTemplate>
                                                    <asp:ScriptManager ID="smgListaCampo" runat="server">
                                                    </asp:ScriptManager>
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="grvListaCampo" runat="server" AllowPaging="True" 
                                                                AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
                                                                BorderWidth="1px" CellPadding="3" CssClass="grilla" GridLines="Vertical" 
                                                                OnPageIndexChanging="grvListaCampo_PageIndexChanging" 
                                                                OnSorting="grvListaCampo_Sorting" Width="100%" 
                                                                onrowcommand="grvListaCampo_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Campo" SortExpression="Nom_Campo" ItemStyle-Width="20%">
                                                                      <ItemTemplate>
                                                                        <asp:LinkButton ID="NomCampo" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="ShowCampo" Text='<%# Eval("Nom_Campo") %>' />
                                                                      </ItemTemplate>
                                                                    </asp:TemplateField>       
                                                                                                                                
                                                                    <asp:BoundField DataField="Nom_Campo" HeaderText="Campo" 
                                                                        SortExpression="Nom_Campo" />
                                                                  
                                                          
                                                                    <asp:BoundField DataField="Cod_Campo" HeaderText="Código" ReadOnly="True" 
                                                                        SortExpression="Cod_Campo" />
                                                                    <asp:BoundField DataField="Cod_Relativo" HeaderText="Código Asociado" 
                                                                        ReadOnly="True" SortExpression="Cod_Relativo" />
                                                                    <asp:BoundField DataField="Dsc_Campo" HeaderText="Descripción Valor" 
                                                                        ReadOnly="True" SortExpression="Dsc_Campo" />
                                                                    <asp:TemplateField HeaderText="Fecha Modificación" 
                                                                        SortExpression="Log_Fecha_Mod">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblITFecha" runat="server" 
                                                                                Text='<%# LAP.TUUA.UTIL.Fecha.convertSQLToFecha(Convert.ToString(Eval("Log_Fecha_Mod")),Convert.ToString(Eval("Log_Hora_Mod"))) %> '></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Nom_Usuario" HeaderText="Usuario Modificación" 
                                                                        ReadOnly="True" SortExpression="Nom_Usuario" />
                                                                </Columns>
                                                                <SelectedRowStyle CssClass="grillaFila" />
                                                                <PagerStyle CssClass="grillaPaginacion" />
                                                                <HeaderStyle CssClass="grillaCabecera" />
                                                                <AlternatingRowStyle CssClass="grillaFila" />
                                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                                            </asp:GridView>
                                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                                            <uc3:CfgEditListaCampo ID="CfgEditListaCampo1" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
                    <uc2:PiePagina ID="PiePagina3" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    
    </div>
 
         
        
 
        
    </form>
</body>
</html>
