<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTicketBPUsadosMediaHoraDiaMes.aspx.cs" Inherits="ReporteRPT_rptTicketBPUsadosMediaHoraDiaMes" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tickets TUUA y/o Boarding Pass Usados por Media Hora, Hora, Dia o Mes</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <CR:CrystalReportViewer ID="crvrptTicketBPUsadosMediaHoraDiaMes" runat="server" 
                AutoDataBind="true"  Height="50px" 
            Width="350px" HasCrystalLogo="False" 
                PrintMode="ActiveX" HasToggleGroupTreeButton="False"  
                PageZoomFactor="100" ShowAllPageIds="True"/>    
            
            <asp:Label ID="lblmensaje" runat="server" CssClass="msgMensaje" Text=""></asp:Label>
        
        </form>
</body>
</html>
