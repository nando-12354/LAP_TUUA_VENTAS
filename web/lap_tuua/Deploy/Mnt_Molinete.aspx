<%@ page language="C#" autoeventwireup="true" inherits="Mnt_Molinete, App_Web_ehzg6gwo" %>

<%@ Register Src="UserControl/CabeceraMenu.ascx" TagName="CabeceraMenu" TagPrefix="uc1" %>
<%@ Register Src="UserControl/PiePagina.ascx" TagName="PiePagina" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Mantenimiento de Molinete</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="css/Style.css" rel="stylesheet" type="text/css" />
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
    <div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <table align="center" border="0" cellpadding="0" cellspacing="0" class="TamanoTabla">
            <tr>
                <td class="Espacio1FilaTabla" colspan="9" style="height: 11px">
                    <uc1:CabeceraMenu ID="CabeceraMenu2" runat="server"></uc1:CabeceraMenu>
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
                                            <asp:GridView ID="gwvMolinete" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" BackColor="White" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" CssClass="grilla" Width="100%" OnPageIndexChanging="gwvMolinete_PageIndexChanging"
                                                OnSorting="gwvMolinete_Sorting">
                                                <Columns>
                                                    <asp:HyperLinkField DataTextField="Cod_Molinete" HeaderText="C&#243;digo"
                                                        SortExpression="Cod_Molinete" DataNavigateUrlFields="Cod_Molinete"
                                                        DataNavigateUrlFormatString="Mnt_ModificarMolinete.aspx?Cod_Molinete={0}"
                                                        NavigateUrl="Mnt_Molinete.aspx" />                                                    
                                                    <asp:BoundField DataField="Dsc_Molinete" HeaderText="Descripci&#243;n" SortExpression="Dsc_Molinete" />
                                                    <asp:BoundField DataField="Tip_Vuelo" HeaderText="Tipo de Vuelo" SortExpression="Tip_Vuelo" />
                                                    <asp:BoundField DataField="Tip_Acceso" HeaderText="Tipo de Acceso" SortExpression="Tip_Acceso" />
                                                    <asp:BoundField DataField="Tip_Molinete" HeaderText="Tipo de Molinete" SortExpression="Tip_Molinete" />
                                                    <asp:BoundField DataField="Tip_Estado" HeaderText="Estado" SortExpression="Tip_Estado" />                                                    
                                                </Columns>
                                                <SelectedRowStyle CssClass="grillaFila" />
                                                <PagerStyle CssClass="grillaPaginacion" />
                                                <HeaderStyle CssClass="grillaCabecera" />
                                                <AlternatingRowStyle CssClass="grillaFila" />
                                            </asp:GridView>
                                            <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
                                            <asp:Label ID="lblTotal" runat="server" CssClass="TextoCampo"></asp:Label>
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
    </div>
    </form>
</body>
</html>
