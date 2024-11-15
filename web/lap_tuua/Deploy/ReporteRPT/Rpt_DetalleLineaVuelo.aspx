<%@ page language="C#" autoeventwireup="true" inherits="Rpt_DetalleLineaVuelo, App_Web_fmojukce" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <CR:CrystalReportViewer ID="crvDetalleLineaVuelo" runat="server" AutoDataBind="true"
        DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False"
        HasCrystalLogo="False" PrintMode="ActiveX" EnableTheming="False" HasGotoPageButton="False"
        HasSearchButton="False" HasToggleGroupTreeButton="False" HasViewList="False"
        SeparatePages="False" HasPageNavigationButtons="False"  />
    <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje"></asp:Label>
    </form>
</body>
</html>
