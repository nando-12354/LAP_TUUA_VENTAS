﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptOperacionCV.aspx.cs" Inherits="ReporteCNS_rptOperacionCV" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Reporte de cuadre de compra y venta</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <CR:CrystalReportViewer ID="crptvOperacionCV" runat="server" AutoDataBind="True"
            EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" 
                                PrintMode="ActiveX" EnableTheming="False" HasGotoPageButton="False" HasSearchButton="False" 
                                HasToggleGroupTreeButton="False" />

    </div>
        </form>
</body>
</html>
