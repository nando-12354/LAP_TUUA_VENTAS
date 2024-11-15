<%@ page language="C#" autoeventwireup="true" inherits="Modulo_Consultas_ReporteCNS_rptTicketxFecha, App_Web_qpqu9x_k" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LAP - Reporte consulta de ticket por fecha</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="crptvTicketxFecha" runat="server" AutoDataBind="true"
            PrintMode="ActiveX" HasCrystalLogo="False" HasGotoPageButton="False" HasSearchButton="False" 
            HasToggleGroupTreeButton="False" HasViewList="False" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" 
            EnableParameterPrompt="False" EnableTheming="False" />
            <asp:Label ID="lblmensaje" runat="server" CssClass="msgMensaje" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
