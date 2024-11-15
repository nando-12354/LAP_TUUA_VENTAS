<%@ page language="C#" autoeventwireup="true" inherits="ReporteREH_ReporteREH_BCBP, App_Web_olfo8i98" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LAP - Reporte rehabilitacion de Boardings</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportViewer ID="crptvBoardings" runat="server" AutoDataBind="true" Width="800px"
    PrintMode="ActiveX" HasCrystalLogo="False" HasGotoPageButton="False" HasSearchButton="False" HasViewList="False" DisplayGroupTree="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" EnableTheming="False" HasToggleGroupTreeButton="False"  />    
    </div>

    </form>
</body>
</html>
