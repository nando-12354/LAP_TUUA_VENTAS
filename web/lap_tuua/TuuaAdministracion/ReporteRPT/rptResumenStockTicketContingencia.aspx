<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptResumenStockTicketContingencia.aspx.cs" Inherits="ReporteRPT_rptResumenStockTicketContingencia" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <CR:CrystalReportViewer ID="crptvStockTicketContingencia" runat="server" 
                AutoDataBind="true"  Height="50px" 
                SeparatePages="False" Width="100%" HasCrystalLogo="False" 
                PrintMode="ActiveX" HasToggleGroupTreeButton="False"  
                PageZoomFactor="100" ShowAllPageIds="True"/>    
    </div>
    <br />
    <br />
    <asp:Label ID="lblmensaje" runat="server" Text="" CssClass="msgMensaje"></asp:Label>
    </form>
</body>
</html>
