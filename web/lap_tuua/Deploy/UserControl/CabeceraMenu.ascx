<%@ control language="C#" autoeventwireup="true" inherits="CabeceraMenu, App_Web_i29mqlr7" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<link href="../css/Style.css" rel="stylesheet" type="text/css" />
<div class="CabeceraDIV">
    <contenttemplate>
<table border="0" class="tamanocabecera" cellpadding="0" cellspacing="0">
    <tr>
        <td colspan="3">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/head.gif" CssClass="tamanocabecera" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Panel ID="Panel1" runat="server" class="tamanocabecera" 
                            BackColor="#26519f" style="text-align: left">
            </asp:Panel>
        </td>
     </tr>
     <tr>
     <td>
        <table style="width: 100%" cellpadding="0" cellspacing="0" border="0">
            <tr class="menuDatos" >
                            <td><asp:Label ID="lblUsuario" runat="server" Width="46px"></asp:Label> </td>
                            <td style="width: 30px; text-align: left;"><asp:Label ID="lblNombreUsuario" runat="server" Width="360px"></asp:Label></td>
                            <td></td>
                            <td style="text-align: right"><asp:Label ID="lblFecha" runat="server" Width="410px" ></asp:Label>
                                &nbsp;&nbsp;&nbsp;
                            </td>
            </tr>
            <tr>
                            <td colspan="4" style="text-align: left">&nbsp;&nbsp;
                                <asp:SiteMapPath ID="stmTuua" runat="server" CssClass="sitemap" 
                                    Font-Names="Verdana" Font-Size="0.8em" PathSeparator=" : ">
                                    <PathSeparatorStyle Font-Bold="True" ForeColor="#507CD1" />
                                    <CurrentNodeStyle ForeColor="#333333" CssClass="siteRootNode" />
                                    <NodeStyle Font-Bold="True" ForeColor="#284E98" CssClass="sitemap" />
                                    <RootNodeStyle Font-Bold="True" ForeColor="#507CD1" CssClass="sitemap" />
                                </asp:SiteMapPath>
                        <hr class="EspacioLinea" color="#0099cc" width="0px" />
                            </td>
            </tr>
            </table>
    </td>
    </tr>
</table>

</contenttemplate>
</div>
