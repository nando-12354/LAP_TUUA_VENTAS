<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mnt_VerCompania.aspx.cs" Inherits="Mnt_VerCompania" %>

<%@ Register src="UserControl/PiePagina.ascx" tagname="piepagina" tagprefix="uc2" %>
<%@ Register src="UserControl/CabeceraMenu.ascx" tagname="cabeceramenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Mantenimiento de Compañía</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" style="height: 11px">
                    <uc1:cabeceramenu id="CabeceraMenu2" runat="server"></uc1:cabeceramenu>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="Boton" OnClick="btnNuevo_Click" />&nbsp;</td>
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
                                <div id="grillita" style="overflow:auto; height:400px; width:100%;">
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gwvCompañia" runat="server" AllowPaging="True" 
                                                AllowSorting="True" AutoGenerateColumns="False" BorderStyle="None" 
                                                BorderWidth="1px" CellPadding="3" CssClass="grilla" GridLines="Vertical" 
                                                OnPageIndexChanging="gwvCompañia_PageIndexChanging" 
                                                 OnSorting="gwvCompañia_Sorting">
                                                <FooterStyle BackColor="Black" ForeColor="Black" />
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                            <asp:UpdateProgress ID="upgUsuario" runat="server">
                                                <progresstemplate>
                                                    <asp:Label ID="lblProcesando" runat="server" style="text-align: left" 
                                                        Text="Procesando"></asp:Label>
                                                </progresstemplate>
                                            </asp:UpdateProgress>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:piepagina id="PiePagina2" runat="server"></uc2:piepagina>
                </td>
            </tr>
        </table>
    
    </div>
        <br />
        <asp:Label ID="txtOrdenacion" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtColumna" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="txtValorMaximoGrilla" runat="server" Visible="False"></asp:Label>
    </form>
    <script language="javascript" type="text/javascript">
      
     var docWidth = document.body.clientWidth; 
     document.getElementById("grillita").style.width= docWidth;
          
    </script>
</body>
</html>
