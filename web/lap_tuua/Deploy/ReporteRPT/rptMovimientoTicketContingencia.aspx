﻿<%@ page language="C#" autoeventwireup="true" inherits="ReporteRPT_rptMovimientoTicketContingencia, App_Web_fmojukce" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Movimiento Ticket Contingencia</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <CR:CrystalReportViewer ID="crptvTicketContingencia" runat="server" 
            AutoDataBind="true" DisplayGroupTree="False" HasCrystalLogo="False" 
            HasToggleGroupTreeButton="False" PrintMode="ActiveX" Height="50px" 
            SeparatePages="False" ShowAllPageIds="True" Width="350px" />
    </div>
    <asp:HiddenField ID="hbandera" runat="server" />
    <br/>
    <br/>
    <asp:Label ID="lblmensaje" runat="server" CssClass="msgMensaje" Text=""></asp:Label>
    </form>
</body>
</html>
<script type="text/javascript" language="JavaScript">

//Modo Ajax
//    var bandera = document.forms[0].hbandera.value;
//    if (bandera == "0") {
//        window.parent.invocarMensajeJS(0)
//    } else if (bandera == "1") {
//        window.parent.invocarMensajeJS(1) 
//    }
</script> 