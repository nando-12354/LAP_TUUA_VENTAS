<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRecaudacionMensual.aspx.cs" Inherits="ReporteRPT_rptRecaudacionMensual" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
    <title></title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <CR:CrystalReportViewer ID="crptvRecaudacionMensual" runat="server" 
                AutoDataBind="true"  Height="50px" 
                SeparatePages="False" Width="100%" HasCrystalLogo="False" 
                PrintMode="ActiveX" HasToggleGroupTreeButton="False"  
                PageZoomFactor="100" ShowAllPageIds="True"/>
    <br/>
    <br/>
    <asp:Label ID="lblmensaje" runat="server" CssClass="msgMensaje" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
