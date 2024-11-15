<%@ page language="C#" autoeventwireup="true" inherits="ReporteRPT_rptBoardingMolinete, App_Web_fmojukce" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
      <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" ></asp:Label>
        <asp:Label ID="lblMensajeErrorData" runat="server" CssClass="mensaje" Width="355px"></asp:Label>
        <CR:CrystalReportViewer ID="crvBoardingMolinete" runat="server" AutoDataBind="true" 
        DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" HasCrystalLogo="False" PrintMode="ActiveX" 
        EnableTheming="False" HasSearchButton="False" HasToggleGroupTreeButton="False" 
            HasViewList="False" Height="50px" Width="350px" />
    
    </div>
    </form>
</body>
</html>
