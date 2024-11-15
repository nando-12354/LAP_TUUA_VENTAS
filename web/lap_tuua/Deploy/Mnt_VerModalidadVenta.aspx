<%@ page language="C#" autoeventwireup="true" inherits="Mnt_VerModalidadVenta, App_Web_tx1el90t" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Mantenimiento de Modalidad de Venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
    <!-- #INCLUDE file="javascript/validarteclaF5.js" -->
    <!-- #INCLUDE file="javascript/MantenerSesion.js" -->
    <style type="text/css">
        html, body
        {
            height: 100%;
        }
    </style>
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server"></uc1:CabeceraMenu>
                </td>
            </tr>
            <tr class="formularioTitulo">
                <td align="left" colspan="9">
                    <asp:Button ID="btnNuevo" runat="server" CssClass="Boton" OnClick="btnNuevo_Click" />&nbsp;
                </td>
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
                                    <div id="divData" class="divSizeCustom">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="gwvModalidaVenta" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grilla" Width="100%" OnPageIndexChanging="gwvModalidaVenta_PageIndexChanging"
                                                OnSorting="gwvModalidaVenta_Sorting">
                                                <Columns>
                                                    <asp:HyperLinkField DataTextField="Cod_Modalidad_Venta" HeaderText="C&#243;digo"
                                                        SortExpression="Cod_Modalidad_Venta" DataNavigateUrlFields="Cod_Modalidad_Venta"
                                                        DataNavigateUrlFormatString="Mnt_ModificarModalidadVenta.aspx?Cod_ModVenta={0}"
                                                        NavigateUrl="Mnt_ModificarModalidadVenta.aspx" />
                                                    <asp:BoundField DataField="Cod_Modalidad_Venta" HeaderText="C&#243;digo" SortExpression="Cod_Modalidad_Venta" />
                                                    <asp:BoundField DataField="Nom_Modalidad" HeaderText="Descripci&#243;n" SortExpression="Nom_Modalidad" />
                                                    <asp:BoundField DataField="Dsc_Tip_Modalidad" HeaderText="Tipo" SortExpression="Dsc_Tip_Modalidad" />
                                                    <asp:BoundField DataField="Dsc_Tip_Ticket" HeaderText="Tipo Ticket Asociados" SortExpression="Dsc_Tip_Ticket" />
                                                    <asp:BoundField DataField="Dsc_Tip_Estado" HeaderText="Estado" SortExpression="Dsc_Tip_Estado" />
                                                    <asp:BoundField DataField="Log_Fecha" HeaderText="Fecha Modificaci&#243;n" SortExpression="Log_Fecha" />
                                                    <asp:BoundField DataField="Nom_Usuario_Mod" HeaderText="Usuario Modificaci&#243;n"
                                                        SortExpression="Nom_Usuario_Mod" />
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
                                            <div id="progressBackgroundFilter">
                                            </div>
                                            <div id="processMessage">
                                                &nbsp;&nbsp;&nbsp;Procesando...<br />
                                                <br />
                                                <img alt="Loading" src="Imagenes/ajax-loader.gif" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    </div>
                                </td>
                                <td class="SpacingGrid" style="height: 115px">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc2:PiePagina ID="PiePagina2" runat="server"></uc2:PiePagina>
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
        document.getElementById("divData").style.width = docWidth;

        var docHeight = document.body.clientHeight;
        document.getElementById("divData").style.height = docHeight - 240;
                  
    </script>
    
</body>
</html>
