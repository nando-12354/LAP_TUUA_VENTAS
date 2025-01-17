﻿<%@ page language="C#" autoeventwireup="true" inherits="ReporteRPT_rptTicketBPUsadosDiaMes, App_Web_fmojukce" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte Ticket BP Usados Dia Mes</title>
    <!--meta http-equiv="X-UA-Compatible" content="IE=8" /-->
    <link href="../css/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <CR:CrystalReportViewer ID="crvrptTicketBPUsadosDiaMes" runat="server" AutoDataBind="true"
        DisplayGroupTree="False" Height="50px" SeparatePages="False" Width="100%" HasCrystalLogo="False"
        PrintMode="ActiveX" HasToggleGroupTreeButton="False" HasViewList="False" PageZoomFactor="100"
        ShowAllPageIds="True" />
    <asp:Label ID="lblmensaje" runat="server" CssClass="msgMensaje" Text=""></asp:Label>
    <asp:DropDownList ID="ddlMes" runat="server" CssClass="combo" Height="16px" Style="display: none"
        Width="16px">
        <asp:ListItem Value="1">Enero</asp:ListItem>
        <asp:ListItem Value="2">Febrero</asp:ListItem>
        <asp:ListItem Value="3">Marzo</asp:ListItem>
        <asp:ListItem Value="4">Abril</asp:ListItem>
        <asp:ListItem Value="5">Mayo</asp:ListItem>
        <asp:ListItem Value="6">Junio</asp:ListItem>
        <asp:ListItem Value="7">Julio</asp:ListItem>
        <asp:ListItem Value="8">Agosto</asp:ListItem>
        <asp:ListItem Value="9">Setiembre</asp:ListItem>
        <asp:ListItem Value="10">Octubre</asp:ListItem>
        <asp:ListItem Value="11">Noviembre</asp:ListItem>
        <asp:ListItem Value="12">Diciembre</asp:ListItem>
    </asp:DropDownList>
    </form>
</body>
</html>
