﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCuadreTicket.aspx.cs" Inherits="ReporteCNS_rptCuadreTicket" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <title>LAP - Reporte de cuadre de ticket emitidos</title>
        <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
    <CR:CrystalReportViewer ID="crptvCuadreTicketEmitidosReport" runat="server" runat="server" AutoDataBind="true" 
       EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" PrintMode="ActiveX" 
        EnableTheming="False" HasGotoPageButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False"  SeparatePages="False" HasPageNavigationButtons="False"  />
    </form>
</body>
</html>
