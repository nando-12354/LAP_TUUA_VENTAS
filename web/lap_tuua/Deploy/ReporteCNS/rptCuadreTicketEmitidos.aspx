<%@ page language="C#" autoeventwireup="true" inherits="ReporteCNS_rptCuadreTicketEmitidos, App_Web_qpqu9x_k" %>

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
        <asp:Label ID="lblMensajeError" runat="server" CssClass="msgMensaje" Width="200px"></asp:Label>
        <CR:CrystalReportViewer ID="crvCuadreTicketsEmitidos" runat="server"   AutoDataBind="true" 
        DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" PrintMode="ActiveX" 
        EnableTheming="False" HasGotoPageButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasViewList="False" SeparatePages="False" HasPageNavigationButtons="False" />
    </div>

    </form>
</body>
</html>
