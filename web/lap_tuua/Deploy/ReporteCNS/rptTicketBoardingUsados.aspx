﻿<%@ page language="C#" autoeventwireup="true" inherits="ReporteCNS_rptTicketBoardingUsados, App_Web_qpqu9x_k" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>LAP - Reporte de consulta de ticket usados</title>
 <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <CR:CrystalReportViewer ID="crvTicketUsados" runat="server" 
        AutoDataBind="true" DisplayGroupTree="False" 
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
        HasCrystalLogo="False" PrintMode="ActiveX" EnableTheming="False" 
        HasGotoPageButton="False" HasSearchButton="False" 
        HasToggleGroupTreeButton="False" HasViewList="False" 
        onunload="crvTicketUsados_Unload"/>     
          
    <br />
    </form>
</body>
</html>
