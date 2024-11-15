<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PiePagina.ascx.cs" Inherits="UserControl_PiePagina" %>

<link href="../css/Style.css" rel="stylesheet" type="text/css" />

<div class="tamanocabecera">
<table border="0" cellpadding="0" cellspacing="0" class="tamanocabecera">
    <tr >
        <td  colspan="3" style="height: 25px">
            &nbsp;&nbsp;&nbsp; &nbsp;</td>
    </tr>
    <tr>
        <td>&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblDerechosLap" runat="server" Width="98px" Height="16px"></asp:Label>
            <asp:Label ID="lblfecha1" runat="server"></asp:Label>
        </td>
        <td >
        </td>
        <td align="right">
            <asp:HyperLink ID="hplCerrarSesion" runat="server" NavigateUrl="~/CerrarSesion.aspx"
                Target="_top" CssClass="link">[hplCerrarSesion]</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
</div>
